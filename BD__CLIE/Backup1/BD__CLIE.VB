Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Imports System
Public Class CLD__CLIE
  Inherits CLD__BASE

  Public Overridable Function GetData(ByVal strDitta As String, ByVal lConto As Integer, ByVal strQuery As String, _
                                      ByVal bModuloCRM As Boolean, ByVal strAccVis As String, ByVal lCodorgaOperat As Integer, _
                                      ByVal strRegvis As String, ByVal IsCrmUser As Boolean, ByVal strTipoConto As String, _
                                      ByRef dsClie As DataSet) As Boolean

    Try
      Return GetData(strDitta, lConto, strQuery, bModuloCRM, strAccVis, lCodorgaOperat, strRegvis, IsCrmUser, strTipoConto, dsClie, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetData(ByVal strDitta As String, ByVal lConto As Integer, ByVal strQuery As String, _
                                      ByVal bModuloCRM As Boolean, ByVal strAccVis As String, ByVal lCodorgaOperat As Integer, _
                                      ByVal strRegvis As String, ByVal IsCrmUser As Boolean, ByVal strTipoConto As String, _
                                      ByRef dsClie As DataSet, ByVal strOrderBy As String) As Boolean
    Dim strSQL As String = ""
    Dim strAnd() As String
    Dim strOr() As String
    Dim i As Integer = 0
    Dim e As Integer = 0
    Dim strOrdinamento As String = NTSCStr(GetSettingBus("BS--CLIE", "OPZIONI", ".", "OrdinamentoSelezione", "D", " ", "D"))
    Dim bGestAnaext As Boolean = CBool(GetSettingBus("BS--CLIE", "OPZIONI", ".", "GestAnaext", "0", " ", "0"))

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lConto, strQuery, bModuloCRM, strAccVis, lCodorgaOperat, strRegvis, IsCrmUser, strTipoConto, dsClie, strOrderBy})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsClie = CType(oIn(9), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT anagra.*, 'xx' as xx_destin, 'xx' as xx_destpag, 'xx' as xx_codrtac," & _
        " tabstat.tb_desstat as xx_stato, comuni.co_denom as xx_codcomu, comuninasc.co_denom as xx_codcomn," & _
        " tabstatnasc.tb_desstat as xx_stanasc, tabstatnaz1.tb_desstat as xx_nazion1," & _
        " tabstatnaz2.tb_desstat as xx_nazion2, tabmast.tb_desmast as xx_codmast, 'xx' as xx_codling," & _
        " 'xx' as xx_valuta, 'xx' as xx_zona, 'xx' as xx_categ, 'xx' as xx_agente, 'xx' as xx_agente2," & _
        " 'xx' as xx_codcana, 'xx' as xx_codbanc, 'xx' as xx_listino, 'xx' as xx_clascon, 'xx' as xx_claprov," & _
        " 'xx' as xx_contfatt, 'xx' as xx_controp, 'xx' as xx_codpag, 'xx' as xx_porto, 'xx' as xx_codtpbf, 'xx' AS xx_codvfde, " & _
        " 'xx' as xx_vett, 'xx' as xx_vett2, 'xx' as xx_codntra, 'xx' as xx_codese, 'xx' as xx_codtcdc, 'xx' as xx_coddica, 'xx' as xx_coddicv," & _
        " 'xx' as xx_codpaga2, 'xx' as xx_codpaga3, 'xx' as xx_codpagadet, 'xx' as xx_codpagadet2, 'xx' as xx_codpagadet3" & _
        " FROM anagra INNER JOIN tabmast ON anagra.an_codmast = tabmast.tb_codmast AND anagra.an_codpcon = tabmast.tb_codpcon" & _
        " LEFT JOIN tabstat ON anagra.an_stato = tabstat.tb_codstat" & _
        " LEFT JOIN comuni ON anagra.an_codcomu = comuni.co_codcomu" & _
        " LEFT JOIN comuni AS comuninasc ON anagra.an_codcomn = comuninasc.co_codcomu" & _
        " LEFT JOIN tabstat AS tabstatnasc ON anagra.an_stanasc = tabstatnasc.tb_codstat" & _
        " LEFT JOIN tabstat AS tabstatnaz1 ON anagra.an_nazion1 = tabstatnaz1.tb_codstat" & _
        " LEFT JOIN tabstat AS tabstatnaz2 ON anagra.an_nazion2 = tabstatnaz2.tb_codstat"
      If (bModuloCRM = True) And (IsCrmUser = True) And (strTipoConto.ToUpper = "C") Then
        strSQL += " INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto AND 0 = leads.le_coddest"
      End If
      If (bGestAnaext = True) And (InStr(strQuery.ToLower, "ax_") <> 0) Then
        strSQL += " INNER JOIN anaext ON anagra.codditt = anaext.codditt AND anagra.an_conto = anaext.ax_conto"
      End If
      strSQL += " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
        " AND an_tipo = " & CStrSQL(strTipoConto)
      If lConto <> 0 Then
        strSQL += " AND an_conto = " & lConto.ToString
      Else
        '------------------------------------------------------------------------------------------------------------
        '--- Devo selezionare + di un soggetto
        '------------------------------------------------------------------------------------------------------------
        If (bGestAnaext = True) And (InStr(strQuery.ToLower, "ax_") <> 0) Then
          strSQL += " AND ax_coddest = 0" & _
            " AND ax_codlead = 0" & _
            " AND ax_tipork = " & CStrSQL(strTipoConto.ToUpper) & _
            " AND ax_codart = ' '" & _
            " AND ax_matric = ' '"
        End If
        strAnd = strQuery.Split(CType("§", Char))
        For i = 0 To strAnd.Length - 1
          strSQL += " AND ("
          strOr = strAnd(i).Split(CType("|", Char))
          strSQL += strOr(0)
          For e = 1 To strOr.Length - 1
            strSQL += strOr(e) & " OR " & strOr(0) & " "
          Next
          If strOr.Length > 1 Then strSQL = strSQL.Substring(0, strSQL.Length - (4 + strOr(0).Length))
          strSQL += ")"
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      If strOrderBy.Trim = "" Then
        Select Case strOrdinamento
          Case "C" : strSQL += " ORDER BY an_conto"
          Case "D" : strSQL += " ORDER BY an_descr1"
        End Select
      Else
        strSQL &= "ORDER BY " & strOrderBy
      End If
      '--------------------------------------------------------------------------------------------------------------
      dsClie = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAGRA")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetDataDestdiv(ByVal strDitta As String, ByVal lConto As Integer, ByRef dsClie As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return GetDataDestdiv(strDitta, lConto, dsClie, False, False, "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetDataDestdiv(ByVal strDitta As String, ByVal lConto As Integer, _
    ByRef dsClie As DataSet, ByVal bModuloCRM As Boolean, ByVal IsCrmUser As Boolean, ByVal strTipoConto As String) As Boolean
    Dim strSQL As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lConto, dsClie, bModuloCRM, IsCrmUser, strTipoConto})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsClie = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      If dsClie.Tables.Contains("DESTDIV") Then
        dsClie.Tables("DESTDIV").Clear()
        dsClie.Tables.Remove("DESTDIV")
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT destdiv.*, co_denom AS xx_codcomu, tb_desstat AS xx_stato, tb_deszone AS xx_codzona," & _
        " tabvett.tb_desvett AS xx_vett, tabvett2.tb_desvett AS xx_vett2, tabcage.tb_descage AS xx_agente," & _
        " tabcage2.tb_descage AS xx_agente2, tb_deslist AS xx_listino, " & _
         IIf(bModuloCRM AndAlso strTipoConto.ToUpper = "C", " le_status", " ''").ToString & " AS xx_lestatus," & _
        " '' AS xx_modificato, tb_desport as xx_porto" & _
        " FROM destdiv LEFT JOIN comuni ON destdiv.dd_codcomu = comuni.co_codcomu" & _
        " LEFT JOIN tabstat ON destdiv.dd_stato = tabstat.tb_codstat" & _
        " LEFT JOIN tabzone ON destdiv.codditt = tabzone.codditt AND destdiv.dd_codzona = tabzone.tb_codzone" & _
        " LEFT JOIN tabvett ON destdiv.codditt = tabvett.codditt AND destdiv.dd_vett = tabvett.tb_codvett" & _
        " LEFT JOIN tabvett as tabvett2 ON destdiv.codditt = tabvett2.codditt AND destdiv.dd_vett2 = tabvett2.tb_codvett" & _
        " LEFT JOIN tabcage ON destdiv.codditt = tabcage.codditt AND destdiv.dd_agente = tabcage.tb_codcage" & _
        " LEFT JOIN tabcage as tabcage2 ON destdiv.codditt = tabcage2.codditt AND destdiv.dd_agente2 = tabcage2.tb_codcage " & _
        " LEFT JOIN tablist ON tablist.codditt = destdiv.codditt AND tb_codlist = dd_listino " & _
        " LEFT JOIN tabport ON tabport.codditt = destdiv.codditt AND tabport.tb_codport = destdiv.dd_porto "
      If bModuloCRM AndAlso strTipoConto.ToUpper = "C" Then
        strSQL += " INNER JOIN leads ON destdiv.codditt = leads.codditt AND destdiv.dd_conto = leads.le_conto AND destdiv.dd_coddest = leads.le_coddest"
      End If
      strSQL += " WHERE destdiv.codditt = " & CStrSQL(strDitta) & _
                "   AND dd_conto = " & lConto.ToString & _
                " ORDER BY dd_coddest"
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DESTDIV", dsClie)
      '--------------------------------------------------------------------------------------------------------------
      dsClie.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetDataAnaext(ByVal strDitta As String, ByVal lConto As Integer, _
                                            ByVal nCoddest As Integer, ByVal lCodlead As Integer, _
                                            ByVal strTipo As String, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM anaext " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ax_conto = " & lConto.ToString & _
               " AND ax_codlead = " & lCodlead.ToString & _
               " AND ax_codart = ' '" & _
               " AND ax_matric = ' '" & _
               " AND ax_tipork = " & CStrSQL(strTipo)
      If nCoddest <> -1 Then
        strSQL += " AND ax_coddest = " & nCoddest.ToString
      Else
        'tutte le destinazioni diverse
        strSQL += " AND ax_coddest <> 0"
      End If

      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, IIf(nCoddest = -1, "ANAEXTDD", "ANAEXT").ToString, dsOut)
      SetTableDefaultValueFromDB("ANAEXT", IIf(nCoddest = -1, "ANAEXTDD", "ANAEXT").ToString, dsOut)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetAnaextGestiti(ByVal strDitta As String) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strOut As String = ""
    Dim i As Integer = 0
    Try
      strOut = ""

      strSQL = "SELECT tb_tipork FROM tabaext WHERE codditt = " & CStrSQL(strDitta)

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For i = 0 To dttTmp.Rows.Count - 1
        strOut += dttTmp.Rows(i)!tb_tipork.ToString
      Next
      dttTmp.Clear()

      Return strOut

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataTabelleCollegate(ByVal strDitta As String, ByVal lConto As Integer, _
                                                      ByVal bGesttabcont As Boolean, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Try
      '------------------------
      'CLITIPB
      If dsOut.Tables.Contains("CLITIPB") Then
        dsOut.Tables("CLITIPB").Clear()
        dsOut.Tables.Remove("CLITIPB")
      End If
      strSQL = "SELECT clitipb.*, tb_despaga as xx_codpaga, tb_destpbf as xx_codtpbf " & _
               " FROM (clitipb LEFT JOIN tabpaga ON clitipb.ctp_codpaga = tabpaga.tb_codpaga) " & _
               " LEFT JOIN tabtpbf ON clitipb.codditt = tabtpbf.codditt AND clitipb.ctp_codtpbf = tabtpbf.tb_codtpbf " & _
               " WHERE clitipb.codditt = " & CStrSQL(strDitta) & _
               " AND ctp_conto = " & lConto.ToString
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CLITIPB", dsOut)

      '------------------------
      'TRKTPBF
      If dsOut.Tables.Contains("TRKTPBF") Then
        dsOut.Tables("TRKTPBF").Clear()
        dsOut.Tables.Remove("TRKTPBF")
      End If
      strSQL = "SELECT TRKTPBF.*, tb_destpbf as xx_codtpbf " & _
               " FROM TRKTPBF " & _
               " LEFT JOIN tabtpbf ON TRKTPBF.codditt = tabtpbf.codditt AND TRKTPBF.tkt_tipobf = tabtpbf.tb_codtpbf " & _
               " WHERE TRKTPBF.codditt = " & CStrSQL(strDitta) & _
               " AND tkt_conto = " & lConto.ToString
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TRKTPBF", dsOut)

      '------------------------
      'CLIBANC
      If dsOut.Tables.Contains("CLIBANC") Then
        dsOut.Tables("CLIBANC").Clear()
        dsOut.Tables.Remove("CLIBANC")
      End If
      strSQL = "SELECT clibanc.*, tb_desvalu as xx_codvalu, '' as xx_abi, '' as xx_cab " & _
               " FROM (clibanc LEFT JOIN tabvalu ON clibanc.cba_codvalu = tabvalu.tb_codvalu) " & _
               " WHERE clibanc.codditt = " & CStrSQL(strDitta) & _
               " AND cba_conto = " & lConto.ToString
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CLIBANC", dsOut)
      For i = 0 To dsOut.Tables("CLIBANC").Rows.Count - 1
        strTmp = ""
        ValCodiceDb(dsOut.Tables("CLIBANC").Rows(i)!cba_abi.ToString, strDitta, "ABI", "N", strTmp)
        dsOut.Tables("CLIBANC").Rows(i)!xx_abi = strTmp

        strTmp = ""
        ValCodiceDb(dsOut.Tables("CLIBANC").Rows(i)!cba_cab.ToString, strDitta, "CAB", "N", strTmp, Nothing, dsOut.Tables("CLIBANC").Rows(i)!cba_abi.ToString)
        dsOut.Tables("CLIBANC").Rows(i)!xx_cab = strTmp
      Next
      dsOut.Tables("CLIBANC").AcceptChanges()

      '------------------------
      'CLISTPG
      If dsOut.Tables.Contains("CLISTPG") Then
        dsOut.Tables("CLISTPG").Clear()
        dsOut.Tables.Remove("CLISTPG")
      End If
      strSQL = "SELECT clistpg.*, tb_desstpg as xx_desstpg" & _
               " FROM (clistpg LEFT JOIN tabstpg ON clistpg.cts_codstpg = tabstpg.tb_codstpg) " & _
               " WHERE clistpg.codditt = " & CStrSQL(strDitta) & _
               " AND cts_conto = " & lConto.ToString
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CLISTPG", dsOut)
      dsOut.Tables("CLISTPG").AcceptChanges()

      '------------------------
      'ANACONA
      If dsOut.Tables.Contains("ANACONA") Then
        dsOut.Tables("ANACONA").Clear()
        dsOut.Tables.Remove("ANACONA")
      End If
      strSQL = "SELECT anacona.*, tb_dessgme as xx_sotgru" & _
               " FROM anacona INNER JOIN tabsgme ON anacona.codditt = tabsgme.codditt " & _
               " AND anacona.acn_sotgru = tabsgme.tb_codsgme " & _
               " WHERE anacona.codditt = " & CStrSQL(strDitta) & _
               " AND acn_conto = " & lConto.ToString
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANACONA", dsOut)
      dsOut.Tables("ANACONA").AcceptChanges()

      '------------------------
      'ORGANIG
      If dsOut.Tables.Contains("ORGANIG") Then
        dsOut.Tables("ORGANIG").Clear()
        dsOut.Tables.Remove("ORGANIG")
      End If
      If bGesttabcont Then
        strSQL = " SELECT organig.*, tb_desruaz as xx_codruaz, tb_descont as xx_codcont, an_descr1 as xx_conto, dd_nomdest as xx_coddest, " & _
                  " tb_descage as xx_codcage, tb_descope as xx_codcope, tb_desstat as xx_stato " & _
                  " FROM ((((((organig LEFT JOIN tabruaz ON organig.og_codruaz = tabruaz.tb_codruaz)" & _
                  " LEFT JOIN tabcont ON organig.og_codcont = tabcont.tb_codcont AND organig.codditt = tabcont.codditt)" & _
                  " LEFT JOIN anagra ON organig.og_conto = anagra.an_conto AND organig.codditt = anagra.codditt)" & _
                  " LEFT JOIN destdiv ON organig.og_coddest = destdiv.dd_coddest AND organig.og_conto = destdiv.dd_conto AND organig.codditt = destdiv.codditt)" & _
                  " LEFT JOIN tabcage ON organig.codditt = tabcage.codditt AND organig.og_codcage = tabcage.tb_codcage)" & _
                  " LEFT JOIN tabcope ON organig.codditt = tabcope.codditt AND organig.og_codcope = tabcope.tb_codcope)" & _
                  " LEFT JOIN tabstat ON organig.og_stato = tabstat.tb_codstat" & _
                  " WHERE organig.codditt = " & CStrSQL(strDitta) & _
                  " AND organig.og_conto = " & lConto.ToString
      Else
        strSQL = " SELECT organig.*, tb_desruaz as xx_codruaz, '' as xx_codcont, an_descr1 as xx_conto, dd_nomdest as xx_coddest, " & _
                  " tb_descage as xx_codcage, tb_descope as xx_codcope, tb_desstat as xx_stato " & _
                  " FROM (((((organig LEFT JOIN tabruaz ON organig.og_codruaz = tabruaz.tb_codruaz)" & _
                  " LEFT JOIN anagra ON organig.og_conto = anagra.an_conto AND organig.codditt = anagra.codditt)" & _
                  " LEFT JOIN destdiv ON organig.og_coddest = destdiv.dd_coddest AND organig.og_conto = destdiv.dd_conto AND organig.codditt = destdiv.codditt)" & _
                  " LEFT JOIN tabcage ON organig.codditt = tabcage.codditt AND organig.og_codcage = tabcage.tb_codcage)" & _
                  " LEFT JOIN tabcope ON organig.codditt = tabcope.codditt AND organig.og_codcope = tabcope.tb_codcope)" & _
                  " LEFT JOIN tabstat ON organig.og_stato = tabstat.tb_codstat" & _
                  " WHERE organig.codditt = " & CStrSQL(strDitta) & _
                  " AND organig.og_conto = " & lConto.ToString 
      End If
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORGANIG", dsOut)

      '------------------------
      'CODARFO
      If dsOut.Tables.Contains("CODARFO") Then
        dsOut.Tables("CODARFO").Clear()
        dsOut.Tables.Remove("CODARFO")
      End If
      strSQL = "SELECT * FROM codarfo WHERE codditt = " & CStrSQL(strDitta) & _
               " AND caf_conto = " & lConto.ToString
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CODARFO", dsOut)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetOrganigFromLead(ByVal strDitta As String, ByVal lLead As Integer, ByVal bGesttabcont As Boolean, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      '------------------------
      'ORGANIG caricato da lead: questa funzione viene chiamta solo da 'nuovo da lead'
      If dsOut.Tables.Contains("ORGANIG") Then
        dsOut.Tables("ORGANIG").Clear()
        dsOut.Tables.Remove("ORGANIG")
      End If
      If bGesttabcont Then
        strSQL = " SELECT organig.*, tb_desruaz as xx_codruaz, tb_descont as xx_codcont, an_descr1 as xx_conto, dd_nomdest as xx_coddest, " & _
                  " tb_descage as xx_codcage, tb_descope as xx_codcope, tb_desstat as xx_stato " & _
                  " FROM ((((((organig LEFT JOIN tabruaz ON organig.og_codruaz = tabruaz.tb_codruaz)" & _
                  " LEFT JOIN tabcont ON organig.og_codcont = tabcont.tb_codcont AND organig.codditt = tabcont.codditt)" & _
                  " LEFT JOIN anagra ON organig.og_conto = anagra.an_conto AND organig.codditt = anagra.codditt)" & _
                  " LEFT JOIN destdiv ON organig.og_coddest = destdiv.dd_coddest AND organig.og_conto = destdiv.dd_conto AND organig.codditt = destdiv.codditt)" & _
                  " LEFT JOIN tabcage ON organig.codditt = tabcage.codditt AND organig.og_codcage = tabcage.tb_codcage)" & _
                  " LEFT JOIN tabcope ON organig.codditt = tabcope.codditt AND organig.og_codcope = tabcope.tb_codcope)" & _
                  " LEFT JOIN tabstat ON organig.og_stato = tabstat.tb_codstat" & _
                  " WHERE organig.codditt = " & CStrSQL(strDitta) & _
                  " AND organig.og_codlead = " & lLead.ToString & _
                  " AND og_conto = 0"
      Else
        strSQL = " SELECT organig.*, tb_desruaz as xx_codruaz, '' as xx_codcont, an_descr1 as xx_conto, dd_nomdest as xx_coddest, " & _
                  " tb_descage as xx_codcage, tb_descope as xx_codcope, tb_desstat as xx_stato " & _
                  " FROM (((((organig LEFT JOIN tabruaz ON organig.og_codruaz = tabruaz.tb_codruaz)" & _
                  " LEFT JOIN anagra ON organig.og_conto = anagra.an_conto AND organig.codditt = anagra.codditt)" & _
                  " LEFT JOIN destdiv ON organig.og_coddest = destdiv.dd_coddest AND organig.og_conto = destdiv.dd_conto AND organig.codditt = destdiv.codditt)" & _
                  " LEFT JOIN tabcage ON organig.codditt = tabcage.codditt AND organig.og_codcage = tabcage.tb_codcage)" & _
                  " LEFT JOIN tabcope ON organig.codditt = tabcope.codditt AND organig.og_codcope = tabcope.tb_codcope)" & _
                  " LEFT JOIN tabstat ON organig.og_stato = tabstat.tb_codstat" & _
                  " WHERE organig.codditt = " & CStrSQL(strDitta) & _
                  " AND organig.og_codlead = " & lLead.ToString & _
                  " AND og_conto = 0"
      End If
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORGANIG", dsOut)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ContoMovimPrinot(ByVal strDitta As String, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim bOut As Boolean = False
    Try

      strSQL = "SELECT top 1 pn_conto " & _
         "FROM anagra INNER JOIN prinot ON anagra.codditt = prinot.codditt AND anagra.an_conto = prinot.pn_conto " & _
         "WHERE anagra.codditt = " & CStrSQL(strDitta) & _
         " AND an_conto = " & lConto.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then bOut = True
      dttTmp.Clear()

      Return bOut

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ContoPresenteCfPi(ByVal strDitta As String, ByVal lConto As Integer, ByVal strPi As String, ByVal strCF As String, ByVal strTipoConto As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim bOut As Boolean = False
    Try
      If strPi <> "" Then strCF = ""
      strSQL = "SELECT top 1 an_conto FROM anagra WHERE codditt = " & CStrSQL(strDitta) & _
               " AND an_conto <> " & lConto.ToString & _
               " AND an_tipo = " & CStrSQL(strTipoConto)
      If strPi <> "" Then strSQL += " AND an_pariva = " & CStrSQL(strPi)
      If strCF <> "" Then strSQL += " AND an_codfis = " & CStrSQL(strCF)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then bOut = True
      dttTmp.Clear()
      Return bOut

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetUltaggAnagra(ByVal strDitta As String, ByVal lConto As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strOut As String = NTSCDate("01/01/1900").ToString
    Try

      strSQL = " SELECT an_ultagg FROM anagra WHERE codditt = " & CStrSQL(strDitta) & _
               " AND an_conto = " & lConto.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then strOut = NTSCDate(dttTmp.Rows(0)!an_ultagg).ToString
      dttTmp.Clear()

      Return strOut

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return strOut
    End Try
  End Function

  Public Overridable Function Salva(ByRef ds As DataSet, ByVal lConto As Integer, ByVal bNew As Boolean, _
                                    ByVal bNuovoContoProposto As Boolean, ByVal strOpz_2 As String, _
                                    ByVal strDtIniAnagen As String, ByVal strGestAnaext As String, _
                                    ByVal bRiscriviDestdiv As Boolean, ByVal lLead As Integer, _
                                    ByVal lAgenteFileApri As Integer, ByVal bModuloAnagen As Boolean, _
                                    ByVal bModuloCRM As Boolean, ByVal bModuloAS As Boolean, _
                                    ByRef dttDestdivDeleted As DataTable, _
                                    ByVal bMantieniLeadsDestdivDeleted As Boolean, _
                                    ByVal bNuovoDaAnagen As Boolean, ByVal bRiscriviClitipb As Boolean, _
                                    ByVal bRiscriviClibanc As Boolean, ByVal bRiscriviOrganig As Boolean, _
                                    ByRef dttOrganigDeleted As DataTable, ByVal bScriviActlog As Boolean, _
                                    ByVal bRiscriviCodarfo As Boolean, ByVal strUserCrm As String) As Boolean
    Try
      Return Salva(ds, lConto, bNew, _
                   bNuovoContoProposto, strOpz_2, _
                   strDtIniAnagen, strGestAnaext, _
                   bRiscriviDestdiv, lLead, _
                   lAgenteFileApri, bModuloAnagen, _
                   bModuloCRM, bModuloAS, _
                   dttDestdivDeleted, _
                   bMantieniLeadsDestdivDeleted, _
                   bNuovoDaAnagen, bRiscriviClitipb, _
                   bRiscriviClibanc, bRiscriviOrganig, _
                   dttOrganigDeleted, bScriviActlog, _
                   bRiscriviCodarfo, strUserCrm, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Salva(ByRef ds As DataSet, ByVal lConto As Integer, ByVal bNew As Boolean, _
                                    ByVal bNuovoContoProposto As Boolean, ByVal strOpz_2 As String, _
                                    ByVal strDtIniAnagen As String, ByVal strGestAnaext As String, _
                                    ByVal bRiscriviDestdiv As Boolean, ByVal lLead As Integer, _
                                    ByVal lAgenteFileApri As Integer, ByVal bModuloAnagen As Boolean, _
                                    ByVal bModuloCRM As Boolean, ByVal bModuloAS As Boolean, _
                                    ByRef dttDestdivDeleted As DataTable, _
                                    ByVal bMantieniLeadsDestdivDeleted As Boolean, _
                                    ByVal bNuovoDaAnagen As Boolean, ByVal bRiscriviClitipb As Boolean, _
                                    ByVal bRiscriviClibanc As Boolean, ByVal bRiscriviOrganig As Boolean, _
                                    ByRef dttOrganigDeleted As DataTable, ByVal bScriviActlog As Boolean, _
                                    ByVal bRiscriviCodarfo As Boolean, ByVal strUserCrm As String, _
                                    ByVal bRiscriviClistpg As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, lConto, bNew, bNuovoContoProposto, strOpz_2, strDtIniAnagen, _
                                             strGestAnaext, bRiscriviDestdiv, lLead, lAgenteFileApri, _
                                             bModuloAnagen, bModuloCRM, bModuloAS, dttDestdivDeleted, _
                                             bMantieniLeadsDestdivDeleted, bNuovoDaAnagen, bRiscriviClitipb, _
                                             bRiscriviClibanc, bRiscriviOrganig, dttOrganigDeleted, bScriviActlog, _
                                             bRiscriviCodarfo, strUserCrm, bRiscriviClistpg})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        dttDestdivDeleted = CType(oIn(13), DataTable)
        dttOrganigDeleted = CType(oIn(19), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      Return Salva(ds, lConto, bNew, _
                         bNuovoContoProposto, strOpz_2, _
                         strDtIniAnagen, strGestAnaext, _
                         bRiscriviDestdiv, lLead, _
                         lAgenteFileApri, bModuloAnagen, _
                         bModuloCRM, bModuloAS, _
                         dttDestdivDeleted, _
                         bMantieniLeadsDestdivDeleted, _
                         bNuovoDaAnagen, bRiscriviClitipb, _
                         bRiscriviClibanc, bRiscriviOrganig, _
                         dttOrganigDeleted, bScriviActlog, _
                         bRiscriviCodarfo, strUserCrm, False, False)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Salva(ByRef ds As DataSet, ByVal lConto As Integer, ByVal bNew As Boolean, _
                                    ByVal bNuovoContoProposto As Boolean, ByVal strOpz_2 As String, _
                                    ByVal strDtIniAnagen As String, ByVal strGestAnaext As String, _
                                    ByVal bRiscriviDestdiv As Boolean, ByVal lLead As Integer, _
                                    ByVal lAgenteFileApri As Integer, ByVal bModuloAnagen As Boolean, _
                                    ByVal bModuloCRM As Boolean, ByVal bModuloAS As Boolean, _
                                    ByRef dttDestdivDeleted As DataTable, _
                                    ByVal bMantieniLeadsDestdivDeleted As Boolean, _
                                    ByVal bNuovoDaAnagen As Boolean, ByVal bRiscriviClitipb As Boolean, _
                                    ByVal bRiscriviClibanc As Boolean, ByVal bRiscriviOrganig As Boolean, _
                                    ByRef dttOrganigDeleted As DataTable, ByVal bScriviActlog As Boolean, _
                                    ByVal bRiscriviCodarfo As Boolean, ByVal strUserCrm As String, _
                                    ByVal bRiscriviClistpg As Boolean, ByVal IsCrmUser As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, lConto, bNew, bNuovoContoProposto, strOpz_2, strDtIniAnagen, _
                                             strGestAnaext, bRiscriviDestdiv, lLead, lAgenteFileApri, _
                                             bModuloAnagen, bModuloCRM, bModuloAS, dttDestdivDeleted, _
                                             bMantieniLeadsDestdivDeleted, bNuovoDaAnagen, bRiscriviClitipb, _
                                             bRiscriviClibanc, bRiscriviOrganig, dttOrganigDeleted, bScriviActlog, _
                                             bRiscriviCodarfo, strUserCrm, bRiscriviClistpg, IsCrmUser})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        dttDestdivDeleted = CType(oIn(13), DataTable)
        dttOrganigDeleted = CType(oIn(19), DataTable)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return Salva(ds, lConto, bNew, bNuovoContoProposto, strOpz_2, strDtIniAnagen, strGestAnaext, _
        bRiscriviDestdiv, lLead, lAgenteFileApri, bModuloAnagen, bModuloCRM, bModuloAS, dttDestdivDeleted, _
        bMantieniLeadsDestdivDeleted, bNuovoDaAnagen, bRiscriviClitipb, bRiscriviClibanc, bRiscriviOrganig, _
        dttOrganigDeleted, bScriviActlog, bRiscriviCodarfo, strUserCrm, bRiscriviClistpg, IsCrmUser, False, "", "")
      '--------------------------------------------------------------------------------------------------------------

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Salva(ByRef ds As DataSet, ByVal lConto As Integer, ByVal bNew As Boolean, _
                                    ByVal bNuovoContoProposto As Boolean, ByVal strOpz_2 As String, _
                                    ByVal strDtIniAnagen As String, ByVal strGestAnaext As String, _
                                    ByVal bRiscriviDestdiv As Boolean, ByVal lLead As Integer, _
                                    ByVal lAgenteFileApri As Integer, ByVal bModuloAnagen As Boolean, _
                                    ByVal bModuloCRM As Boolean, ByVal bModuloAS As Boolean, _
                                    ByRef dttDestdivDeleted As DataTable, _
                                    ByVal bMantieniLeadsDestdivDeleted As Boolean, _
                                    ByVal bNuovoDaAnagen As Boolean, ByVal bRiscriviClitipb As Boolean, _
                                    ByVal bRiscriviClibanc As Boolean, ByVal bRiscriviOrganig As Boolean, _
                                    ByRef dttOrganigDeleted As DataTable, ByVal bScriviActlog As Boolean, _
                                    ByVal bRiscriviCodarfo As Boolean, ByVal strUserCrm As String, _
                                    ByVal bRiscriviClistpg As Boolean, ByVal IsCrmUser As Boolean, _
                                    ByVal bScollegaOperatoriAssociati As Boolean, _
                                    ByVal strOperatore1 As String, ByVal strOperatore2 As String) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, lConto, bNew, bNuovoContoProposto, strOpz_2, strDtIniAnagen, _
                                             strGestAnaext, bRiscriviDestdiv, lLead, lAgenteFileApri, _
                                             bModuloAnagen, bModuloCRM, bModuloAS, dttDestdivDeleted, _
                                             bMantieniLeadsDestdivDeleted, bNuovoDaAnagen, bRiscriviClitipb, _
                                             bRiscriviClibanc, bRiscriviOrganig, dttOrganigDeleted, bScriviActlog, _
                                             bRiscriviCodarfo, strUserCrm, bRiscriviClistpg, IsCrmUser, _
                                             bScollegaOperatoriAssociati, strOperatore1, strOperatore2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        dttDestdivDeleted = CType(oIn(13), DataTable)
        dttOrganigDeleted = CType(oIn(19), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      Return Salva(ds, lConto, bNew, bNuovoContoProposto, strOpz_2, strDtIniAnagen, strGestAnaext, _
        bRiscriviDestdiv, lLead, lAgenteFileApri, bModuloAnagen, bModuloCRM, bModuloAS, dttDestdivDeleted, _
        bMantieniLeadsDestdivDeleted, bNuovoDaAnagen, bRiscriviClitipb, bRiscriviClibanc, bRiscriviOrganig, _
        dttOrganigDeleted, bScriviActlog, bRiscriviCodarfo, strUserCrm, bRiscriviClistpg, IsCrmUser, _
        bScollegaOperatoriAssociati, strOperatore1, strOperatore2, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Salva(ByRef ds As DataSet, ByVal lConto As Integer, ByVal bNew As Boolean, _
                                      ByVal bNuovoContoProposto As Boolean, ByVal strOpz_2 As String, _
                                      ByVal strDtIniAnagen As String, ByVal strGestAnaext As String, _
                                      ByVal bRiscriviDestdiv As Boolean, ByVal lLead As Integer, _
                                      ByVal lAgenteFileApri As Integer, ByVal bModuloAnagen As Boolean, _
                                      ByVal bModuloCRM As Boolean, ByVal bModuloAS As Boolean, _
                                      ByRef dttDestdivDeleted As DataTable, _
                                      ByVal bMantieniLeadsDestdivDeleted As Boolean, _
                                      ByVal bNuovoDaAnagen As Boolean, ByVal bRiscriviClitipb As Boolean, _
                                      ByVal bRiscriviClibanc As Boolean, ByVal bRiscriviOrganig As Boolean, _
                                      ByRef dttOrganigDeleted As DataTable, ByVal bScriviActlog As Boolean, _
                                      ByVal bRiscriviCodarfo As Boolean, ByVal strUserCrm As String, _
                                      ByVal bRiscriviClistpg As Boolean, ByVal IsCrmUser As Boolean, _
                                      ByVal bScollegaOperatoriAssociati As Boolean, _
                                      ByVal strOperatore1 As String, ByVal strOperatore2 As String, _
                                      ByVal bScollegaOperatoriAssociatiDest As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim strSQLInsert As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dtrA() As DataRow = Nothing
    Dim strMsg As String = ""

    Dim strDataAgg As String = DateTime.Now.ToShortDateString
    Dim dttTmp, dttAext, dttDestdivPrec As New DataTable
    Dim dtrChange() As DataRow = Nothing
    Dim dtrTmp() As DataRow = Nothing
    Dim i As Integer = 0
    Dim nRec As Integer = 0

    Dim strSqlWhere As String = ""
    Dim strDesogglog As String = ""
    Dim bScollegaDest As Boolean = False
    Dim strOperatore1Dest As String = ""
    Dim strOperatore2Dest As String = ""

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, lConto, bNew, bNuovoContoProposto, strOpz_2, strDtIniAnagen, _
                                             strGestAnaext, bRiscriviDestdiv, lLead, lAgenteFileApri, _
                                             bModuloAnagen, bModuloCRM, bModuloAS, dttDestdivDeleted, _
                                             bMantieniLeadsDestdivDeleted, bNuovoDaAnagen, bRiscriviClitipb, _
                                             bRiscriviClibanc, bRiscriviOrganig, dttOrganigDeleted, bScriviActlog, _
                                             bRiscriviCodarfo, strUserCrm, bRiscriviClistpg, IsCrmUser, _
                                             bScollegaOperatoriAssociati, strOperatore1, strOperatore2, _
                                             bScollegaOperatoriAssociatiDest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        dttDestdivDeleted = CType(oIn(13), DataTable)
        dttOrganigDeleted = CType(oIn(19), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      dtrA = ds.Tables("ANAGRA").Select("an_conto = " & lConto.ToString)

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      '----------------------------------
      'aggiorno ANAGRA
      If bNew Then
        '------------------------------------------------------------------------------------------------------------
        '--- Se si è in salvataggio di un "Cliente" duplicato, nel quale è stata scelta la duplicazione 
        '--- dell'Organizzazione e l'opzione di registro "BS--CLIE/OPZIONI/SpostaOrganizzazioneInDuplica"
        '--- è attiva, cancella i records in ORGANIG, del conto di origine
        '--- N.B.: Il progressivo del conto di origine, è salvato nel campo non più utilizzato ORGANIG.og_nomeserv
        '--- e, prima di essere salvato nel DataBase, quest'ulimo viene svuotato
        '------------------------------------------------------------------------------------------------------------
        If (dtrA(0)!an_tipo.ToString = "C") And (ds.Tables("ORGANIG").Rows.Count > 0) Then
          If CBool(GetSettingBus("BS--CLIE", "OPZIONI", ".", "SpostaOrganizzazioneInDuplica", "0", " ", "0")) = True Then
            For i = 0 To ds.Tables("ORGANIG").Rows.Count - 1
              If NTSCStr(ds.Tables("ORGANIG").Rows(i)!og_nomeserv).Trim <> "" Then
                If IsNumeric(ds.Tables("ORGANIG").Rows(i)!og_nomeserv) Then
                  strSQL = "DELETE FROM organig " & _
                    " WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                    " AND og_progr = " & NTSCInt(ds.Tables("ORGANIG").Rows(i)!og_nomeserv)
                  Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
                  ds.Tables("ORGANIG").Rows(i)!og_nomeserv = ""
                End If
              End If
            Next
            ds.Tables("ORGANIG").AcceptChanges()
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        'se serve aggiorno il progressivo
        If strOpz_2 = "S" And bNuovoContoProposto Then
          i = NTSCInt(NTSCInt(dtrA(0)!an_conto).ToString.Substring(NTSCInt(dtrA(0)!an_codmast).ToString.Length))
          Select Case NTSCStr(dtrA(0)!an_tipo)
            Case "C" : AggNuma(dtrA(0)!codditt.ToString, "CC", "", NTSCInt(dtrA(0)!an_codmast), i, False, True, strMsg, dbConn)
            Case "F" : AggNuma(dtrA(0)!codditt.ToString, "FF", "", NTSCInt(dtrA(0)!an_codmast), i, False, True, strMsg, dbConn)
          End Select
          'devo sostituire il nuovo conto cli/for in anagra e tutte le tabelle collegate (destdiv, leads, ...)
          'non può succedere: è già stato fatto un test in entity in TESTPRESALVA
          If strMsg <> "" Then Throw New NTSException(oApp.Tr(Me, 128825541428676000, "Numerazione clienti/fornitori:") & " " & strMsg)
        End If

        If strOpz_2 = "S" And NTSCInt(dtrA(0)!an_codanag) <> 0 And bNuovoDaAnagen = False Then
          AggNumg("AG", "", 0, NTSCInt(dtrA(0)!an_codanag), True, True, strMsg, dbConn)
          'devo sostituire il nuovo codice di anagen in anagra e tutte le tabelle collegate (destdiv, ...)
          If strMsg <> "" Then Throw New NTSException(oApp.Tr(Me, 128383122186132000, "Numerazione anagrafiche generali:") & " " & strMsg)
        End If

        '---------------------------------------------------------------------------------------------------------
        'inserisco anagra
        '---------------------------------------------------------------------------------------------------------
        strSQL = "INSERT INTO anagra " & GetQueryInsertField(ds.Tables("ANAGRA"), "an_") & " VALUES "
        strSQL += GetQueryInsertValue(ds.Tables("ANAGRA"), dtrA(0), "an_")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'inserisco anatric da mastric
        strSQL = "INSERT INTO anatric (codditt, ant_conto, ant_codtric, ant_kpcrd, ant_kpcra)" & _
                  " SELECT " & CStrSQL(dtrA(0)!codditt.ToString) & ", " & lConto.ToString & "," & _
                  " mst_codtric, mst_kpcrd, mst_kpcra FROM mastric" & _
                  " WHERE mst_codpcon = " & CStrSQL(dtrA(0)!an_codpcon.ToString) & _
                  " AND mst_codmast = " & dtrA(0)!an_codmast.ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)


        If dtrA(0)!an_tipo.ToString = "C" And lLead <> 0 Then
          'se nuovo da lead aggiorno allole memorizzando il codice di anagra
          strSQL = "UPDATE allole" & _
                  " SET ao_codice = " & lConto.ToString & _
                  " WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                  " AND ao_tipo = 'R'" & _
                  " AND ao_codlead = " & lLead.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'cancello organig collegato a lead ed inserisco quello nuovo
          strSQL = "DELETE FROM organig " & _
                  " WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                  " AND og_codlead = " & lLead.ToString & _
                  " AND og_conto = 0"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          For Each dtrT As DataRow In ds.Tables("ORGANIG").Rows
            strSQL = "INSERT INTO organig " & GetQueryInsertField(ds.Tables("ORGANIG"), "og_") & " VALUES "
            strSQL += GetQueryInsertValue(ds.Tables("ORGANIG"), dtrT, "og_")
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
        End If

      Else
        '---------------------------------
        'scrivo il log
        If bScriviActlog Then
          strSqlWhere = CStrSQL(dtrA(0)!codditt.ToString) & "," & lConto.ToString
          strDesogglog = oApp.Tr(Me, 128388116730382000, "Modifica dati relativi all'anagrafica '|" & lConto.ToString & " - " & NTSCStr(dtrA(0)!an_descr1) & "|'")
          If Not ScriviActLog(dtrA(0)!codditt.ToString, "BS--CLIE", "anagra", "anagra", strSqlWhere, "M", "D", strDesogglog, False) Then
            If IsInTrans Then AnnullaTrans()
            Return False
          End If
        End If

        '---------------------------------------------------------------------------------------------------------
        'aggiorno anagra
        '---------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE anagra SET " & GetQueryUpdate(ds.Tables("ANAGRA"), dtrA(0), "an_") & _
                 " WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                 " AND an_conto = " & NTSCInt(dtrA(0)!an_conto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
        '--- Leggo i dati di DESTIV prima della modifica
        '------------------------------------------------------------------------------------------------------------
        strSQL = "SELECT * FROM destdiv" & _
          " WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
          " AND dd_conto = " & NTSCInt(dtrA(0)!an_conto) & _
          " AND (dd_agente <> 0 OR dd_agente2 <> 0)"
        dttDestdivPrec = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
      End If

      '----------------------------------
      'inserisco / aggiorno ANAEXT di anagra
      If strGestAnaext.IndexOf(dtrA(0)!an_tipo.ToString) > -1 Then
        If bNew Then
          strSQL = "INSERT INTO anaext " & GetQueryInsertField(ds.Tables("ANAEXT"), "ax_") & " VALUES "
          strSQL += GetQueryInsertValue(ds.Tables("ANAEXT"), ds.Tables("ANAEXT").Rows(0), "ax_")
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Else
          strSQL = " SELECT * FROM anaext " & _
                   " WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                   " AND ax_conto = " & NTSCInt(dtrA(0)!an_conto).ToString & _
                   " AND ax_coddest = 0 " & _
                   " AND ax_codlead = 0 " & _
                   " AND ax_tipork = " & CStrSQL(dtrA(0)!an_tipo.ToString)

          dttAext = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

          If dttAext.Rows.Count = 0 Then
            strSQL = "INSERT INTO anaext " & GetQueryInsertField(ds.Tables("ANAEXT"), "ax_") & " VALUES "
            strSQL += GetQueryInsertValue(ds.Tables("ANAEXT"), ds.Tables("ANAEXT").Rows(0), "ax_")
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Else
            strSQL = "UPDATE anaext SET " & GetQueryUpdate(ds.Tables("ANAEXT"), ds.Tables("ANAEXT").Rows(0), "ax_") & _
                     " WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                     " AND ax_conto = " & NTSCInt(dtrA(0)!an_conto).ToString & _
                     " AND ax_coddest = 0 " & _
                     " AND ax_codlead = 0 " & _
                     " AND ax_tipork = " & CStrSQL(dtrA(0)!an_tipo.ToString)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        End If
      End If

      '----------------------------------
      'DESTINAZIONI DIVERSE: insert / update / delete
      If bRiscriviDestdiv Then
        'non posso sapere quali sono aggiunte/modificate e/o cancellate: cancello tutto e riscrivo
        If strGestAnaext.IndexOf(IIf(dtrA(0)!an_tipo.ToString = "C", "D", "E").ToString) > -1 Then
          strSQL = "DELETE FROM anaext WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                   " AND ax_tipork = " & CStrSQL(IIf(dtrA(0)!an_tipo.ToString = "C", "D", "E").ToString) & _
                   " AND ax_codlead = 0" & _
                   " AND ax_conto = " & NTSCInt(dtrA(0)!an_conto).ToString & _
                   " AND ax_coddest <> 0"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If

        strSQL = "DELETE FROM destdiv WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                 " AND dd_conto = " & NTSCInt(dtrA(0)!an_conto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'insert
        strSQL = "INSERT INTO destdiv " & GetQueryInsertField(ds.Tables("DESTDIV"), "dd_")
        For i = 0 To ds.Tables("DESTDIV").Rows.Count - 1
          strSQLVal = GetQueryInsertValue(ds.Tables("DESTDIV"), ds.Tables("DESTDIV").Rows(i), "dd_")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next

        If strGestAnaext.IndexOf(IIf(dtrA(0)!an_tipo.ToString = "C", "D", "E").ToString) > -1 Then
          strSQL = "INSERT INTO anaext " & GetQueryInsertField(ds.Tables("ANAEXTDD"), "ax_")
          For i = 0 To ds.Tables("ANAEXTDD").Rows.Count - 1
            strSQLVal = GetQueryInsertValue(ds.Tables("ANAEXTDD"), ds.Tables("ANAEXTDD").Rows(i), "ax_")
            Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
        End If
      End If

      '----------------------------------
      'Aggiorno CLITIPB
      If bRiscriviClitipb Then
        strSQL = "DELETE FROM clitipb WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                 " AND ctp_conto = " & NTSCInt(dtrA(0)!an_conto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "INSERT INTO clitipb " & GetQueryInsertField(ds.Tables("CLITIPB"), "ctp_")
        For i = 0 To ds.Tables("CLITIPB").Rows.Count - 1
          strSQLVal = GetQueryInsertValue(ds.Tables("CLITIPB"), ds.Tables("CLITIPB").Rows(i), "ctp_")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
      End If

      '----------------------------------
      'Aggiorno TRKTPBF
      strSQL = "DELETE FROM TRKTPBF WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                 " AND tkt_conto = " & NTSCInt(dtrA(0)!an_conto).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      For Each dtrT As DataRow In ds.Tables("TRKTPBF").Rows
        If NTSCStr(dtrT!tkt_serie) = "" Then dtrT!tkt_serie = " "
      Next
      strSQL = "INSERT INTO TRKTPBF " & GetQueryInsertField(ds.Tables("TRKTPBF"), "tkt_")
      For i = 0 To ds.Tables("TRKTPBF").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("TRKTPBF"), ds.Tables("TRKTPBF").Rows(i), "tkt_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      '----------------------------------
      'Aggiorno CLIBANC
      If bRiscriviClibanc Then
        strSQL = "DELETE FROM clibanc WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                 " AND cba_conto = " & NTSCInt(dtrA(0)!an_conto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "INSERT INTO clibanc " & GetQueryInsertField(ds.Tables("CLIBANC"), "cba_")
        For i = 0 To ds.Tables("CLIBANC").Rows.Count - 1
          strSQLVal = GetQueryInsertValue(ds.Tables("CLIBANC"), ds.Tables("CLIBANC").Rows(i), "cba_")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
      End If

      '----------------------------------
      'Aggiorno CLISTPG
      '--- Ora permette di modificare la tabella anche se si è in modifica di un conto
      'If bRiscriviClistpg Then
      strSQL = "DELETE FROM clistpg WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
               " AND cts_conto = " & NTSCInt(dtrA(0)!an_conto).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "INSERT INTO clistpg " & GetQueryInsertField(ds.Tables("CLISTPG"), "cts_")
      For i = 0 To ds.Tables("CLISTPG").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("CLISTPG"), ds.Tables("CLISTPG").Rows(i), "cts_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next
      'End If

      '----------------------------------
      'aggiorno codarfo (dovrebbe succedere solo quando si duplica un conto ...)
      If bRiscriviCodarfo Then
        strSQL = "DELETE FROM codarfo WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                 " AND caf_conto = " & NTSCInt(dtrA(0)!an_conto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "INSERT INTO codarfo " & GetQueryInsertField(ds.Tables("CODARFO"), "caf_")
        For i = 0 To ds.Tables("CODARFO").Rows.Count - 1
          strSQLVal = GetQueryInsertValue(ds.Tables("CODARFO"), ds.Tables("CODARFO").Rows(i), "caf_")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
      End If

      '----------------------------------
      strSQL = "DELETE FROM anacona WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
               " AND acn_conto = " & NTSCInt(dtrA(0)!an_conto).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "INSERT INTO anacona " & GetQueryInsertField(ds.Tables("ANACONA"), "acn_")
      For i = 0 To ds.Tables("ANACONA").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ANACONA"), ds.Tables("ANACONA").Rows(i), "acn_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      '----------------------------------
      'aggiorno leads
      If dtrA(0)!an_tipo.ToString = "C" Then
        If Not SalvaLeads(dtrA, ds, strGestAnaext, lLead, lAgenteFileApri, bModuloCRM, bModuloAS, bRiscriviDestdiv, _
                          dttDestdivDeleted, bMantieniLeadsDestdivDeleted, strUserCrm, dbConn, IsCrmUser) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
      End If

      '----------------------------------
      'Aggiorno ORGANIG: se ho crm o cs ed è un cliente: faccio solo update per annullare voce di organig
      If bRiscriviOrganig Then
        For i = 0 To ds.Tables("ORGANIG").Rows.Count - 1
          ds.Tables("ORGANIG").Rows(i)!og_servdtua = DataGMT()
        Next
        ds.Tables("ORGANIG").AcceptChanges()

        If bModuloCRM Then
          For i = 0 To dttOrganigDeleted.Rows.Count - 1
            strSQL = "UPDATE organig SET og_conto = 0, og_coddest = 0, og_codlead = -1 " & _
                     " WHERE codditt = " & CStrSQL(dtrA(0)!codditt) & _
                     "   AND og_progr = " & NTSCInt(dttOrganigDeleted.Rows(i)!og_progr) & _
                     "   AND NOT EXISTS (SELECT TOP 1 co_progr FROM contatti " & _
                     "                   INNER JOIN organig AS organig2 ON co_progr = organig2.og_contatto " & _
                     "                   WHERE co_progr = organig.og_contatto " & _
                     "                     AND organig2.og_progr <> organig.og_progr) "
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
        End If

        strSQL = "DELETE FROM organig " & _
                 " WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                 " AND og_conto = " & NTSCInt(dtrA(0)!an_conto).ToString

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "INSERT INTO organig " & GetQueryInsertField(ds.Tables("ORGANIG"), "og_")
        For i = 0 To ds.Tables("ORGANIG").Rows.Count - 1
          strSQLVal = GetQueryInsertValue(ds.Tables("ORGANIG"), ds.Tables("ORGANIG").Rows(i), "og_")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
      End If

      '----------------------------------
      'aggiorno anagen, anasto, destgen, deststo: se anagen non c'è lo creo, diversamente aggiorno
      If NTSCInt(dtrA(0)!an_codanag) <> 0 And bModuloAnagen Then
        If Not SalvaAnagen(dtrA, strDataAgg, strDtIniAnagen, ds.Tables("DESTDIV"), dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bScollegaOperatoriAssociati Then
        If Not Salva_CancellaAccLead(NTSCStr(dtrA(0)!codditt), strOperatore1, lLead, dbConn) Then Return False
        If Not Salva_CancellaAccLead(NTSCStr(dtrA(0)!codditt), strOperatore2, lLead, dbConn) Then Return False

        If Not Salva_AggiungiAccLeadDefault(NTSCStr(dtrA(0)!codditt), lLead, dbConn) Then Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bScollegaOperatoriAssociatiDest Then
        For i = 0 To (dttDestdivPrec.Rows.Count - 1)
          bScollegaDest = False
          strOperatore1Dest = ""
          strOperatore2Dest = ""
          dtrTmp = ds.Tables("DESTDIV").Select("dd_coddest = " & NTSCInt(dttDestdivPrec.Rows(i)!dd_coddest), "")
          If dtrTmp.Length = 0 Then
            bScollegaDest = True
          Else
            If (NTSCInt(dttDestdivPrec.Rows(i)!dd_agente) <> 0) And (NTSCInt(dttDestdivPrec.Rows(i)!dd_agente) <> NTSCInt(dtrTmp(0)!dd_agente)) And (NTSCInt(dttDestdivPrec.Rows(i)!dd_agente) <> NTSCInt(dtrTmp(0)!dd_agente)) Then
              strSQL = "SELECT le_codlead FROM leads" & _
                " WHERE codditt = " & CStrSQL(NTSCStr(dttDestdivPrec.Rows(i)!codditt)) & _
                " AND le_conto = " & NTSCInt(dttDestdivPrec.Rows(i)!dd_conto) & _
                " AND le_coddest = " & NTSCInt(dttDestdivPrec.Rows(i)!dd_coddest)
              dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              If dttTmp.Rows.Count > 0 Then
                lLead = NTSCInt(dttTmp.Rows(0)!le_codlead)
                dttTmp.Clear()
                dttTmp.Dispose()
                strSQL = "SELECT TOP 1 opdi_opnome FROM accdito" & _
                  " WHERE codditt = " & CStrSQL(NTSCStr(dttDestdivPrec.Rows(i)!codditt)) & _
                  " AND opdi_codcage = " & NTSCInt(dttDestdivPrec.Rows(i)!dd_agente)
                dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
                If dttTmp.Rows.Count > 0 Then
                  strOperatore1Dest = NTSCStr(dttTmp.Rows(0)!opdi_opnome)
                  bScollegaDest = True
                  dttTmp.Clear()
                  dttTmp.Dispose()
                End If
              End If
            End If
            If (NTSCInt(dttDestdivPrec.Rows(i)!dd_agente2) <> 0) And (NTSCInt(dttDestdivPrec.Rows(i)!dd_agente2) <> NTSCInt(dttDestdivPrec.Rows(i)!dd_agente)) And (NTSCInt(dttDestdivPrec.Rows(i)!dd_agente2) <> NTSCInt(dtrTmp(0)!dd_agente2)) Then
              strSQL = "SELECT le_codlead FROM leads" & _
                " WHERE codditt = " & CStrSQL(NTSCStr(dttDestdivPrec.Rows(i)!codditt)) & _
                " AND le_conto = " & NTSCInt(dttDestdivPrec.Rows(i)!dd_conto) & _
                " AND le_coddest = " & NTSCInt(dttDestdivPrec.Rows(i)!dd_coddest)
              dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              If dttTmp.Rows.Count > 0 Then
                lLead = NTSCInt(dttTmp.Rows(0)!le_codlead)
                dttTmp.Clear()
                dttTmp.Dispose()
                strSQL = "SELECT TOP 1 opdi_opnome FROM accdito" & _
                  " WHERE codditt = " & CStrSQL(NTSCStr(dttDestdivPrec.Rows(i)!codditt)) & _
                  " AND opdi_codcage = " & NTSCInt(dttDestdivPrec.Rows(i)!dd_agente2)
                dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
                If dttTmp.Rows.Count > 0 Then
                  strOperatore2Dest = NTSCStr(dttTmp.Rows(0)!opdi_opnome)
                  bScollegaDest = True
                  dttTmp.Clear()
                  dttTmp.Dispose()
                End If
              End If
            End If
          End If
          If bScollegaDest Then
            If Not Salva_CancellaAccLead(NTSCStr(dtrA(0)!codditt), strOperatore1Dest, lLead, dbConn) Then Return False
            If Not Salva_CancellaAccLead(NTSCStr(dtrA(0)!codditt), strOperatore2Dest, lLead, dbConn) Then Return False

            If Not Salva_AggiungiAccLeadDefault(NTSCStr(dtrA(0)!codditt), lLead, dbConn) Then Return False
          End If
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      dtrA(0)!an_ultagg = GetUltaggAnagra(NTSCStr(dtrA(0)!codditt), NTSCInt(dtrA(0)!an_conto))

      ds.AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()

      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttDestdivPrec.Clear()
      dttDestdivPrec.Dispose()
    End Try
  End Function
  Public Overridable Function Salva_CancellaAccLead(ByVal strDitta As String, ByVal strOpNome As String, ByVal lLead As Integer, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      If strOpNome.Trim <> "" Then
        strSQL = "DELETE FROM acclead " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 "   AND opcr_opnome = " & CStrSQL(strOpNome) & _
                 "   AND opcr_codlead = " & lLead
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Salva_AggiungiAccLeadDefault(ByVal strDitta As String, ByVal lLead As Integer, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      'Controlla se c'è almeno una riga in acclead non deve fare nulla
      strSQL = "SELECT codditt FROM acclead " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND opcr_codlead = " & lLead

      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn).Rows.Count > 0 Then Return True

      'Non ci sono righe in acclead, lo riassegna al lead di default.
      strSQL = "INSERT INTO acclead (codditt, opcr_opnome, opcr_codlead, opcr_crmvis, opcr_crmmod) " & _
               " VALUES (" & CStrSQL(strDitta) & ", " & _
                         CStrSQL(GetSettingBus("BS--CLIE", "OPZIONI", ".", "UtentePredefinitoCrm", "Admin", " ", "Admin")) & ", " & _
                         lLead & ", 'S', 'S')"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function SalvaLeads(ByVal dtrA() As DataRow, ByRef ds As DataSet, ByVal strGestAnaext As String, _
                                    ByVal lLead As Integer, ByVal lAgenteFileApri As Integer, _
                                    ByVal bModuloCRM As Boolean, ByVal bModuloAS As Boolean, _
                                    ByVal bRiscriviDestdiv As Boolean, ByRef dttDestdivDeleted As DataTable, _
                                    ByVal bMantieniLeadsDestdivDeleted As Boolean, ByVal strUserCrm As String, _
                                    ByRef dbConn As DbConnection) As Boolean
    Try
      Return SalvaLeads(dtrA, ds, strGestAnaext, lLead, lAgenteFileApri, bModuloCRM, bModuloAS, bRiscriviDestdiv, _
        dttDestdivDeleted, bMantieniLeadsDestdivDeleted, strUserCrm, dbConn, False)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function SalvaLeads(ByVal dtrA() As DataRow, ByRef ds As DataSet, ByVal strGestAnaext As String, _
                                    ByVal lLead As Integer, ByVal lAgenteFileApri As Integer, _
                                    ByVal bModuloCRM As Boolean, ByVal bModuloAS As Boolean, _
                                    ByVal bRiscriviDestdiv As Boolean, ByRef dttDestdivDeleted As DataTable, _
                                    ByVal bMantieniLeadsDestdivDeleted As Boolean, ByVal strUserCrm As String, _
                                    ByRef dbConn As DbConnection, ByVal IsCrmUser As Boolean) As Boolean
    'creo/aggiorno/cancello leads collegati ad anagra/destdiv
    Dim strSQL As String = ""
    Dim bChangeopinc As Boolean = False
    Dim lOpincTmp As Integer = -1
    Dim lcod As Integer = 0
    Dim i As Integer = 0
    Dim nRec As Integer = 0
    Dim bNewLead As Boolean = False
    Dim strMsg As String = ""
    Dim strSiglaricDest As String = ""
    Dim dttTmp As New DataTable
    Dim nAgente1 As Integer = NTSCInt(dtrA(0)!an_agente)
    Dim nAgente2 As Integer = NTSCInt(dtrA(0)!an_agente2)
    Dim strDitta As String = NTSCStr(dtrA(0)!codditt)
    Dim strOperatoreTmp As String = ""
    Dim oDttgr As CLEGROUPBY = Nothing
    Dim dttGr As New DataTable

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrA, ds, strGestAnaext, lLead, lAgenteFileApri, bModuloCRM, bModuloAS, _
        bRiscriviDestdiv, dttDestdivDeleted, bMantieniLeadsDestdivDeleted, strUserCrm, dbConn, IsCrmUser})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        dttDestdivDeleted = CType(oIn(8), DataTable)
        dbConn = CType(oIn(11), DbConnection)
        Return CBool(oOut)
      End If
      '----------------

      'solo se c'è modulo CRM o CUSTOMER SERVICE
      If bModuloCRM = False And bModuloAS = False Then Return True

      '--------------------------------
      'se il lead collegato ad anagra esiste già lo aggiorno, diversamente lo creo
      If lLead <> 0 Then
        bNewLead = True
        With dtrA(0)
          'Controllo se è stato modificato il codice agente
          'Se il cod. operatore incaricato sul lead era agganciato all'agente variato, modifico
          'il cod. operatore incaricato sul lead
          If lAgenteFileApri <> NTSCInt(!an_agente) Then
            lOpincTmp = GetOpincaricato(!codditt.ToString, NTSCInt(!an_agente), dbConn)
          End If
          strSQL = "UPDATE leads" & _
                    " SET le_descr1 = " & CStrSQL(!an_descr1) & "," & _
                    " le_descr2 = " & CStrSQL(!an_descr2) & ", " & _
                    " le_indir = " & CStrSQL(!an_indir) & "," & _
                    " le_cap = " & CStrSQL(!an_cap) & "," & _
                    " le_citta = " & CStrSQL(!an_citta) & "," & _
                    " le_prov = " & CStrSQL(!an_prov) & "," & _
                    " le_codfis = " & CStrSQL(!an_codfis) & "," & _
                    " le_pariva = " & CStrSQL(!an_pariva) & "," & _
                    " le_telef = " & CStrSQL(!an_telef) & "," & _
                    " le_faxtlx = " & CStrSQL(!an_faxtlx) & "," & _
                    " le_contatt = " & CStrSQL(!an_contatt) & "," & _
                    " le_ultagg = " & CDataOraSQL(DateTime.Now) & "," & _
                    " le_zona = " & NTSCInt(!an_zona) & "," & _
                    " le_categ = " & NTSCInt(!an_categ) & "," & _
                    " le_agente = " & NTSCInt(!an_agente) & "," & _
                    " le_banc1 = " & CStrSQL(!an_banc1) & "," & _
                    " le_banc2 = " & CStrSQL(!an_banc2) & "," & _
                    " le_abi = " & NTSCInt(!an_abi) & "," & _
                    " le_cab = " & NTSCInt(!an_cab) & "," & _
                    " le_note = " & CStrSQL(Left(NTSCStr(!an_note), 30)) & "," & _
                    " le_stato = " & CStrSQL(!an_stato) & "," & _
                    " le_agente2 = " & NTSCInt(!an_agente2) & "," & _
                    " le_note2 = " & CStrSQL(!an_note2) & "," & _
                    " le_email = " & CStrSQL(!an_email) & ","
          If lOpincTmp <> -1 Then strSQL += " le_opinc = " & lOpincTmp.ToString & ", "
          strSQL += " le_website = " & CStrSQL(!an_website) & "," & _
                    " le_usaem = " & CStrSQL(!an_usaem) & "," & _
                    " le_codling = " & NTSCInt(!an_codling) & "," & _
                    " le_status = " & CStrSQL(!an_status) & "," & _
                    " le_codcana = " & NTSCInt(!an_codcana) & "," & _
                    " le_opnome = " & CStrSQL(oApp.User.Nome) & "," & _
                    " le_webuid = " & CStrSQL(!an_webuid) & "," & _
                    " le_webpwd = " & CStrSQL(!an_webpwd) & "," & _
                    " le_siglaric = " & CStrSQL(!an_siglaric) & "," & _
                    " le_cell = " & CStrSQL(!an_cell) & "," & _
                    " le_titolo = " & CStrSQL(!an_titolo) & "," & _
                    " le_clascon = " & NTSCInt(!an_clascon) & "," & _
                    " le_listino = " & NTSCInt(!an_listino) & "," & _
                    " le_servdtua = " & CDataOraSQL(DataGMT) & "," & _
                    " le_clirdtua = " & CDataOraSQL(NTSCDate(IntSetDate("01/01/1900"))) & "," & _
                    " le_privacy = " & CStrSQL(!an_privacy) & "," & _
                    " le_latitud = " & CStrSQL(!an_latitud) & "," & _
                    " le_longitud = " & CStrSQL(!an_longitud) & "," & _
                    " le_conto = " & !an_conto.ToString
          strSQL += " WHERE codditt = " & CStrSQL(!codditt.ToString) & _
                " AND le_codlead = " & lLead.ToString

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '----------------------------------
          'aggiorno ANAEXT di leads collegato ad anagra (solo se anagra ha una anaext): ora cancello; più in basso inserisco
          If strGestAnaext.IndexOf("L") > -1 And strGestAnaext.IndexOf(!an_tipo.ToString) > -1 Then
            strSQL = "DELETE FROM anaext WHERE codditt = " & CStrSQL(!codditt.ToString) & _
                     " AND ax_tipork = 'L'" & _
                     " AND ax_codlead = " & lLead.ToString & _
                     " AND ax_conto = 0" & _
                     " AND ax_coddest = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        End With
        lcod = lLead
      Else
        With dtrA(0)
          lcod = LegNuma(!codditt.ToString, "C8", " ", 0, True, dbConn)
          lcod = AggNuma(!codditt.ToString, "C8", " ", 0, lcod, True, True, strMsg, dbConn)
          If strMsg <> "" Then Throw New NTSException(strMsg)
          lOpincTmp = 0
          strSQL = "INSERT INTO leads (codditt, le_codlead, le_descr1, le_descr2, le_indir, le_cap, le_citta," & _
                    " le_prov, le_codfis, le_pariva, le_telef, le_faxtlx, le_contatt, le_ultagg, le_zona," & _
                    " le_categ, le_agente, le_banc1, le_banc2, le_abi, le_cab, le_note, le_stato," & _
                    " le_agente2, le_note2, le_email, le_website, le_usaem, le_codling, le_status," & _
                    " le_codcana, le_opnome, le_webuid, le_webpwd, le_conto, le_siglaric, le_cell," & _
                    " le_titolo, le_libstr1, le_libint1, le_codcptr, le_codpartn, le_opinc, le_codperv," & _
                    " le_coddest, le_clascon, le_listino, le_servdtua, le_servdeleted, le_clircodlprov," & _
                    " le_clirdeleted, le_clirupdated, le_clirdtua, le_idoutlo, le_privacy, le_contattato," & _
                    " le_nonint, le_latitud, le_longitud)" & _
                    " VALUES (" & CStrSQL(!codditt) & ", " & lcod & ", " & _
                    CStrSQL(!an_descr1) & ", " & CStrSQL(!an_descr2) & ", " & _
                    CStrSQL(!an_Indir) & ", " & CStrSQL(!an_cap) & ", " & _
                    CStrSQL(!an_citta) & ", " & CStrSQL(!an_Prov) & ", " & _
                    CStrSQL(!an_Codfis) & ", " & CStrSQL(!an_Pariva) & ", " & _
                    CStrSQL(!an_Telef) & ", " & CStrSQL(!an_Faxtlx) & ", " & _
                    CStrSQL(!an_Contatt) & ", " & CDataOraSQL(DateTime.Now) & ", " & _
                    NTSCInt(!an_Zona) & ", " & NTSCInt(!an_Categ) & ", " & CInt(!an_Agente) & ", " & _
                    CStrSQL(!an_Banc1) & ", " & CStrSQL(!an_Banc2) & ", " & _
                    NTSCInt(!an_Abi) & ", " & NTSCInt(!an_Cab) & ", " & CStrSQL(!an_Note) & ", " & _
                    CStrSQL(!an_Stato) & ", " & NTSCInt(!an_Agente2) & ", " & _
                    CStrSQL(!an_Note2) & ", " & CStrSQL(!an_Email) & ", " & _
                    CStrSQL(!an_Website) & ", " & CStrSQL(!an_usaem) & ", " & _
                    NTSCInt(!an_Codling) & ", " & CStrSQL(!an_status) & ", " & _
                    NTSCInt(!an_Codcana) & ", " & CStrSQL(oApp.User.Nome) & ", " & _
                    CStrSQL(!an_Webuid) & ", " & CStrSQL(!an_Webpwd) & ", " & _
                    !an_conto.ToString & ", " & CStrSQL(!an_Siglaric) & ", " & CStrSQL(!an_Cell) & ", " & _
                    CStrSQL(!an_Titolo) & ", NULL, 0, 0, 0, " & lOpincTmp & ", 0, 0, " & _
                    NTSCInt(!an_Clascon) & ", " & CInt(!an_Listino) & ", " & _
                    CDataOraSQL(DataGMT) & ", 'N', 0, 'N', 'N', " & _
                    CDataOraSQL(NTSCDate(IntSetDate("01/01/1900"))) & ", 0, " & _
                    CStrSQL(!an_Privacy) & ", 'S','N', " & CStrSQL(!an_latitud) & ", " & CStrSQL(!an_longitud) & ")"

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)


          strSQL = "INSERT INTO acclead(codditt, opcr_opnome, opcr_codlead, opcr_crmvis, opcr_crmmod) " & _
                   "VALUES (" & CStrSQL(!codditt) & ", " & CStrSQL(strUserCrm) & ", " & lcod & ", 'S', 'S')"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End With
      End If

       If Not SalvaLeads_CreaAnaxet(NTSCStr(dtrA(0)!codditt), NTSCInt(dtrA(0)!an_conto), 0, lcod, NTSCStr(dtrA(0)!an_tipo), strGestAnaext, dbConn) Then Return False

      '---------------------------------------------------
      'destdiv da collegare a leads (potrei avere anche dei record cancellati nel dataset da rimuovere dal DB (solo se non gestisco anagen)
      If bRiscriviDestdiv Then
        'prima devo cancellare i destdiv rimossi
        For i = 0 To dttDestdivDeleted.Rows.Count - 1
          If NTSCInt(dttDestdivDeleted.Rows(i)!dd_codlead) <> 0 Then    'lead = 0: sono destdiv create non da leads e cancellate prima di salvare il tutto
            If bMantieniLeadsDestdivDeleted Then
              strSQL = "UPDATE leads SET le_conto = 0, le_coddest = 0 " & _
                       " WHERE codditt = " & CStrSQL(dttDestdivDeleted.Rows(i)!codditt) & _
                       " AND le_codlead = " & NTSCInt(dttDestdivDeleted.Rows(i)!dd_codlead).ToString
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            Else
              If strGestAnaext.IndexOf("L") > -1 Then
                'prima cancello anaext
                strSQL = "DELETE anaext FROM anaext INNER JOIN leads ON " & _
                         " anaext.codditt = leads.codditt AND anaext.ax_codlead = leads.le_codlead " & _
                         " WHERE anaext.codditt = " & CStrSQL(dttDestdivDeleted.Rows(i)!codditt) & _
                         " AND le_codlead = " & NTSCInt(dttDestdivDeleted.Rows(i)!dd_codlead).ToString & _
                         " AND ax_tipork = 'L'" & _
                         " AND ax_conto = 0" & _
                         " AND ax_coddest = 0"
                Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              End If

              strSQL = "DELETE FROM leads " & _
                        " WHERE codditt = " & CStrSQL(dttDestdivDeleted.Rows(i)!codditt) & _
                        " AND le_codlead = " & NTSCInt(dttDestdivDeleted.Rows(i)!dd_codlead).ToString
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            End If
          End If
        Next

        If lOpincTmp = -1 Then
          If bNewLead Then
            lOpincTmp = 0
          Else
            lOpincTmp = GetOpincaricato(dtrA(0)!codditt.ToString, NTSCInt(dtrA(0)!an_agente), dbConn)
          End If
        End If

        'ora aggiorno quelli esistenti (se non presenti li creo)
        For i = 0 To ds.Tables("DESTDIV").Rows.Count - 1
          If NTSCInt(ds.Tables("DESTDIV").Rows(i)!dd_codlead) > 0 Then
            With ds.Tables("DESTDIV").Rows(i)
              '------------------------------------------------------------------------------------------------------
              strSiglaricDest = NTSCStr(dtrA(0)!an_siglaric)
              strSQL = "SELECT le_siglaric FROM leads" & _
                " WHERE codditt = " & CStrSQL(NTSCStr(dtrA(0)!codditt)) & _
                " AND le_codlead = " & NTSCInt(!dd_codlead)
              dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              If dttTmp.Rows.Count > 0 Then strSiglaricDest = NTSCStr(dttTmp.Rows(0)!le_siglaric)
              dttTmp.Clear()
              dttTmp.Dispose()
              '------------------------------------------------------------------------------------------------------
              strSQL = "UPDATE leads" & _
                        " SET le_descr1 = " & CStrSQL(IIf(!dd_nomdest.Equals(DBNull.Value), " ", !dd_nomdest)) & "," & _
                        " le_descr2 = " & CStrSQL(Left(Trim(NTSCStr(!dd_nomdest2)), 50)) & "," & _
                        " le_conto = " & NTSCInt(!dd_conto) & "," & _
                        " le_coddest = " & NTSCInt(!dd_coddest) & "," & _
                        " le_indir = " & CStrSQL(!dd_inddest) & "," & _
                        " le_cap = " & CStrSQL(!dd_capdest) & "," & _
                        " le_citta = " & CStrSQL(!dd_locdest) & "," & _
                        " le_prov = " & CStrSQL(!dd_prodest) & "," & _
                        " le_codfis = " & CStrSQL(!dd_codfis) & "," & _
                        " le_pariva = " & CStrSQL(!dd_pariva) & "," & _
                        " le_telef = " & CStrSQL(!dd_telef) & "," & _
                        " le_faxtlx = " & CStrSQL(!dd_faxtlx) & "," & _
                        " le_contatt = " & CStrSQL(dtrA(0)!an_Contatt) & "," & _
                        " le_ultagg = " & CDataOraSQL(DateTime.Now) & "," & _
                        " le_zona = " & NTSCInt(!dd_codzona).ToString & "," & _
                        " le_categ = " & NTSCInt(dtrA(0)!an_Categ).ToString & "," & _
                        " le_agente = " & NTSCInt(!dd_agente).ToString & "," & _
                        " le_banc1 = " & CStrSQL(dtrA(0)!an_Banc1) & "," & _
                        " le_banc2 = " & CStrSQL(dtrA(0)!an_Banc2) & "," & _
                        " le_abi = " & NTSCInt(dtrA(0)!an_Abi) & "," & _
                        " le_cab = " & NTSCInt(dtrA(0)!an_Cab) & "," & _
                        " le_note = " & CStrSQL(Left(NTSCStr(dtrA(0)!an_note), 30)) & "," & _
                        " le_stato = " & CStrSQL(!dd_stato) & "," & _
                        " le_agente2 = " & NTSCInt(!dd_agente2).ToString & "," & _
                        " le_note2 = " & CStrSQL(!dd_note) & "," & _
                        " le_email = " & CStrSQL(!dd_email) & ","
              If NTSCInt(!dd_agente) = 0 Then strSQL += "le_opinc = " & NTSCInt(lOpincTmp) & ", "
              strSQL += " le_website = " & CStrSQL(dtrA(0)!an_Website) & "," & _
                        " le_usaem = " & CStrSQL(!dd_usaem) & "," & _
                        " le_codling = " & NTSCInt(dtrA(0)!an_Codling) & ","
              If (bModuloCRM = True) And NTSCStr(!xx_lestatus) <> "" Then
                strSQL += " le_status = " & CStrSQL(!xx_lestatus) & ","
              Else
                strSQL += " le_status = " & CStrSQL(dtrA(0)!an_status) & ","
              End If
              strSQL += " le_codcana = " & NTSCInt(dtrA(0)!an_Codcana) & "," & _
                        " le_opnome = " & CStrSQL(oApp.User.Nome) & "," & _
                        " le_webuid = " & CStrSQL(dtrA(0)!an_Webuid) & "," & _
                        " le_webpwd = " & CStrSQL(dtrA(0)!an_Webpwd) & "," & _
                        " le_siglaric = " & CStrSQL(strSiglaricDest) & "," & _
                        " le_cell = " & CStrSQL(dtrA(0)!an_Cell) & "," & _
                        " le_titolo = " & CStrSQL(dtrA(0)!an_Titolo) & "," & _
                        " le_clascon = " & NTSCInt(dtrA(0)!an_Clascon) & "," & _
                        " le_listino = " & NTSCInt(dtrA(0)!an_Listino) & "," & _
                        " le_servdtua = " & CDataOraSQL(DataGMT) & "," & _
                        " le_privacy = " & CStrSQL(dtrA(0)!an_Privacy) & ", " & _
                        " le_latitud = " & CStrSQL(!dd_latitud) & "," & _
                        " le_longitud = " & CStrSQL(!dd_longitud) & _
                        " WHERE codditt = " & CStrSQL(!codditt) & _
                        " AND le_codlead = " & NTSCInt(!dd_codlead).ToString
            End With
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          Else
            'nuovo leads: devo fare update anche di destdiv
            With ds.Tables("DESTDIV").Rows(i)
              lcod = LegNuma(!codditt.ToString, "C8", " ", 0, True, dbConn)
              lcod = AggNuma(!codditt.ToString, "C8", " ", 0, lcod, True, True, strMsg, dbConn)
              If strMsg <> "" Then Throw New NTSException(strMsg)

              strSQL = "UPDATE destdiv SET dd_codlead = " & lcod.ToString & _
                        " WHERE codditt = " & CStrSQL(!codditt) & _
                        " AND dd_conto = " & NTSCInt(!dd_conto).ToString & _
                        " AND dd_coddest = " & NTSCInt(!dd_coddest).ToString
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

              strSQL = "INSERT INTO leads (codditt, le_codlead, le_descr1, le_descr2, le_indir, le_cap, le_citta," & _
                        " le_prov, le_codfis, le_pariva, le_telef, le_faxtlx, le_contatt, le_ultagg, le_zona," & _
                        " le_categ, le_agente, le_banc1, le_banc2, le_abi, le_cab, le_note, le_stato," & _
                        " le_agente2, le_note2, le_email, le_website, le_usaem, le_codling, le_status," & _
                        " le_codcana, le_opnome, le_webuid, le_webpwd, le_conto, le_siglaric, le_cell," & _
                        " le_titolo, le_libstr1, le_libint1, le_codcptr, le_codpartn, le_opinc, le_codperv," & _
                        " le_coddest, le_clascon, le_listino, le_servdtua, le_servdeleted, le_clircodlprov," & _
                        " le_clirdeleted, le_clirupdated, le_clirdtua, le_idoutlo, le_privacy, le_contattato," & _
                        " le_nonint, le_latitud, le_longitud)" & _
                        " VALUES (" & CStrSQL(!codditt) & ", " & lcod.ToString & ", " & _
                        CStrSQL(IIf(!dd_nomdest.Equals(DBNull.Value), " ", !dd_nomdest)) & ", " & _
                        CStrSQL(Left(Trim(NTSCStr(!dd_nomdest2)), 50)) & ", " & _
                        CStrSQL(!dd_inddest) & ", " & CStrSQL(!dd_capdest) & ", " & _
                        CStrSQL(!dd_locdest) & ", " & CStrSQL(!dd_prodest) & ", " & _
                        CStrSQL(!dd_codfis) & ", " & CStrSQL(!dd_pariva) & ", " & _
                        CStrSQL(!dd_telef) & ", " & CStrSQL(!dd_faxtlx) & ", " & _
                        CStrSQL(dtrA(0)!an_contatt) & ", " & CDataOraSQL(DateTime.Now) & ", " & _
                        NTSCInt(!dd_codzona).ToString & ", " & NTSCInt(dtrA(0)!an_categ) & ", " & NTSCInt(!dd_agente).ToString & ", " & _
                        CStrSQL(dtrA(0)!an_banc1) & ", " & CStrSQL(dtrA(0)!an_banc2) & ", " & _
                        NTSCInt(dtrA(0)!an_abi) & ", " & NTSCInt(dtrA(0)!an_cab) & ", " & CStrSQL(Left(NTSCStr(!dd_note), 30)) & ", " & _
                        CStrSQL(!dd_stato) & ", " & NTSCInt(!dd_agente2).ToString & ", " & _
                        CStrSQL(!dd_note) & ", " & CStrSQL(!dd_email) & ", " & _
                        CStrSQL(dtrA(0)!an_website) & ", " & CStrSQL(!dd_usaem) & ", " & _
                        NTSCInt(dtrA(0)!an_Codling) & ", " & CStrSQL(dtrA(0)!an_Status) & ", " & _
                        NTSCInt(dtrA(0)!an_Codcana) & ", " & CStrSQL(oApp.User.Nome) & ", " & _
                        CStrSQL(dtrA(0)!an_Webuid) & ", " & CStrSQL(dtrA(0)!an_Webpwd) & ", " & _
                        CStrSQL(dtrA(0)!an_conto) & ", " & CStrSQL(dtrA(0)!an_Siglaric) & ", " & _
                        CStrSQL(dtrA(0)!an_Cell) & ", " & CStrSQL(dtrA(0)!an_Titolo) & "," & _
                        " NULL, 0, 0, 0, " & lOpincTmp & ", 0, " & NTSCInt(!dd_coddest) & ", " & NTSCInt(dtrA(0)!an_Clascon) & ", " & _
                        NTSCInt(dtrA(0)!an_Listino) & ", " & CDataOraSQL(DataGMT) & "," & _
                        " 'N', 0, 'N', 'N', " & CDataOraSQL(NTSCDate(IntSetDate("01/01/1900"))) & "," & _
                        " 0, " & CStrSQL(dtrA(0)!an_privacy) & ", 'S', 'N', " & CStrSQL(!dd_latitud) & ", " & CStrSQL(!dd_longitud) & ")"
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

              strSQL = "INSERT INTO acclead(codditt, opcr_opnome, opcr_codlead, opcr_crmvis, opcr_crmmod) " & _
                       "VALUES (" & CStrSQL(!codditt) & ", " & CStrSQL(strUserCrm) & ", " & lcod & ", 'S', 'S')"
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              ds.Tables("DESTDIV").Rows(i)!dd_codlead = lcod
              ds.Tables("DESTDIV").Rows(i).AcceptChanges()
              If Not SalvaLeads_CreaAnaxet(NTSCStr(dtrA(0)!codditt), NTSCInt(dtrA(0)!an_conto), NTSCInt(!dd_coddest), lcod, _
                                           "D", strGestAnaext, dbConn) Then Return False
            End With
          End If    'If NTSCInt(dttDestdivDeleted.Rows(i)!dd_codlead) > 0 Then
        Next    'For i = 0 To ds.Tables("DESTDIV").Rows.Count - 1        
      End If    'If bRiscriviDestdiv Then

      'Necessario per coprire il caso:
      ' - Nuovo cliente con CRM attivo
      ' - Prima di salvare vado nell'organizzazione e aggiungo i contatti
      ' - Salvo, i contatti sono presenti sono nel cliente e non nel lead generato di conseguenza.
      For i = 0 To ds.Tables("ORGANIG").Rows.Count - 1
        If NTSCInt(ds.Tables("ORGANIG").Rows(i)!og_codlead) = 0 Then 'Il contatto potrebbe essere già assegnato al lead principale e qui sto salvando una destinazione diversa
          ds.Tables("ORGANIG").Rows(i)!og_codlead = lcod
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      If lLead <> 0 Then
        '------------------------------------------------------------------------------------------------------------
        '--- Aggiorno lo status di tutti i lead collegati alle destinazioni diverse
        '------------------------------------------------------------------------------------------------------------
        If bModuloCRM Then
          For Each dtrTmp As DataRow In ds.Tables("DESTDIV").Select("xx_modificato = 'S'")
            With dtrTmp
              If NTSCStr(!xx_lestatus) = "" Then Continue For
              strSQL = "UPDATE leads" & _
                       " SET le_status = " & CStrSQL(!xx_lestatus) & _
                       " WHERE codditt = " & CStrSQL(!codditt) & _
                       "   AND le_conto = " & NTSCInt(!dd_conto) & _
                       "   AND le_coddest = " & NTSCInt(!dd_coddest)
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

              !xx_modificato = ""
            End With
          Next
          ds.Tables("DESTDIV").AcceptChanges()
        Else
          strSQL = "UPDATE leads" & _
                   " SET le_status = " & CStrSQL(dtrA(0)!an_status) & _
                   " WHERE codditt = " & CStrSQL(dtrA(0)!codditt) & _
                   "   AND le_conto = " & NTSCInt(dtrA(0)!an_conto)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--------------------------------------------------------------------------------------------------------------
      '--- Se indicati Agente 1 e/o 2, preleva gli operatori presenti in ACCDITO, con gli agenti indicati
      '--- e aggiunge, se ancora non presenti, gli operatori in ACCLEAD
      '--------------------------------------------------------------------------------------------------------------
      If lLead = 0 Then lLead = lcod
      If nAgente1 > 0 Then
        strOperatoreTmp = ""
        strSQL = "SELECT opdi_opnome FROM accdito" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND opdi_codcage = " & nAgente1
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then strOperatoreTmp = NTSCStr(dttTmp.Rows(0)!opdi_opnome)
        dttTmp.Clear()
        dttTmp.Dispose()
        If strOperatoreTmp.Trim <> "" Then
          strSQL = "SELECT opcr_opnome FROM acclead" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND UPPER(opcr_opnome) = " & CStrSQL(strOperatoreTmp.ToUpper) & _
            " AND opcr_codlead = " & lLead
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count = 0 Then InserisciAcclead(strDitta, strOperatoreTmp, lLead, dbConn)
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
      End If
      If nAgente2 > 0 Then
        strOperatoreTmp = ""
        strSQL = "SELECT opdi_opnome FROM accdito" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND opdi_codcage = " & nAgente2
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then strOperatoreTmp = NTSCStr(dttTmp.Rows(0)!opdi_opnome)
        dttTmp.Clear()
        dttTmp.Dispose()
        If strOperatoreTmp.Trim <> "" Then
          strSQL = "SELECT opcr_opnome FROM acclead" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND UPPER(opcr_opnome) = " & CStrSQL(strOperatoreTmp.ToUpper) & _
            " AND opcr_codlead = " & lLead
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count = 0 Then InserisciAcclead(strDitta, strOperatoreTmp, lLead, dbConn)
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      oDttgr = New CLEGROUPBY
      dttGr.Clear()
      If oDttgr.NTSGroupBy(ds.Tables("DESTDIV"), dttGr, "dd_codlead, dd_agente, dd_agente2", "", "dd_codlead, dd_agente, dd_agente2") = True Then
        For i = 0 To (dttGr.Rows.Count - 1)
          nAgente1 = NTSCInt(dttGr.Rows(i)!dd_agente)
          nAgente2 = NTSCInt(dttGr.Rows(i)!dd_agente2)
          If nAgente1 > 0 Then
            strOperatoreTmp = ""
            strSQL = "SELECT opdi_opnome FROM accdito" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND opdi_codcage = " & nAgente1
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            If dttTmp.Rows.Count > 0 Then strOperatoreTmp = NTSCStr(dttTmp.Rows(0)!opdi_opnome)
            dttTmp.Clear()
            dttTmp.Dispose()
            If strOperatoreTmp.Trim <> "" Then
              strSQL = "SELECT opcr_opnome FROM acclead" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND UPPER(opcr_opnome) = " & CStrSQL(strOperatoreTmp.ToUpper) & _
                " AND opcr_codlead = " & NTSCInt(dttGr.Rows(i)!dd_codlead)
              dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              If dttTmp.Rows.Count = 0 Then InserisciAcclead(strDitta, strOperatoreTmp, _
                NTSCInt(dttGr.Rows(i)!dd_codlead), dbConn)
              dttTmp.Clear()
              dttTmp.Dispose()
            End If
          End If
          If nAgente2 > 0 Then
            strOperatoreTmp = ""
            strSQL = "SELECT opdi_opnome FROM accdito" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND opdi_codcage = " & nAgente2
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            If dttTmp.Rows.Count > 0 Then strOperatoreTmp = NTSCStr(dttTmp.Rows(0)!opdi_opnome)
            dttTmp.Clear()
            dttTmp.Dispose()
            If strOperatoreTmp.Trim <> "" Then
              strSQL = "SELECT opcr_opnome FROM acclead" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND UPPER(opcr_opnome) = " & CStrSQL(strOperatoreTmp.ToUpper) & _
                " AND opcr_codlead = " & NTSCInt(dttGr.Rows(i)!dd_codlead)
              dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              If dttTmp.Rows.Count = 0 Then InserisciAcclead(strDitta, strOperatoreTmp, _
                NTSCInt(dttGr.Rows(i)!dd_codlead), dbConn)
              dttTmp.Clear()
              dttTmp.Dispose()
            End If
          End If
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaLeads_CreaAnaxet(ByVal strDitta As String, ByVal lConto As Integer, ByVal lDest As Integer, ByVal lLead As Integer, _
                                                   ByVal strTipo As String, ByVal strGestAnaext As String, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------------------------
      'inserisco ANAEXT di leads collegato ad anagra (solo se anagra gestisce anaext)
      If strGestAnaext.IndexOf("L") > -1 Then
        If strGestAnaext.IndexOf(strTipo) > -1 Then
          strSQL = "INSERT INTO anaext (codditt, ax_conto, ax_coddest, ax_codlead, ax_tipork, ax_tipo1, ax_tipo2, ax_tipo3, " & _
                   "                    ax_descr1, ax_descr2, ax_descr3, ax_descr4, ax_descr5, ax_descr6, ax_descr7, ax_descr8, " & _
                   "                    ax_descr9, ax_descr10, ax_desext1, ax_desext2, ax_desext3, ax_memo1, ax_memo2, ax_data1, " & _
                   "                    ax_data2, ax_data3, ax_data4, ax_data5, ax_num1, ax_num2, ax_num3, ax_num4, " & _
                   "                    ax_num5, ax_num6, ax_num7, ax_num8, ax_num9, ax_num10, ax_check1, ax_check2, " & _
                   "                    ax_check3, ax_check4, ax_check5, ax_check6, ax_check7, ax_check8, ax_check9, ax_check10, " & _
                   "                    ax_combo1, ax_combo2, ax_combo3, ax_ultagg, ax_opnome, ax_codart, ax_matric)" & _
                   " SELECT anaext.codditt, 0, 0, " & lLead & ", 'L', ax_tipo1, ax_tipo2, ax_tipo3, " & _
                   "        ax_descr1, ax_descr2, ax_descr3, ax_descr4, ax_descr5, ax_descr6, ax_descr7, ax_descr8, " & _
                   "        ax_descr9, ax_descr10, ax_desext1, ax_desext2, ax_desext3, ax_memo1, ax_memo2, ax_data1, " & _
                   "        ax_data2, ax_data3, ax_data4, ax_data5, ax_num1, ax_num2, ax_num3, ax_num4, " & _
                   "        ax_num5, ax_num6, ax_num7, ax_num8, ax_num9, ax_num10, ax_check1, ax_check2, " & _
                   "        ax_check3, ax_check4, ax_check5, ax_check6, ax_check7, ax_check8, ax_check9, ax_check10, " & _
                   "        ax_combo1, ax_combo2, ax_combo3, ax_ultagg, ax_opnome, ax_codart, ax_matric" & _
                   " FROM anaext " & _
                   " WHERE anaext.codditt = " & CStrSQL(strDitta) & _
                   "   AND ax_tipork = " & CStrSQL(strTipo) & _
                   "   AND ax_codlead = 0" & _
                   "   AND ax_conto = " & lConto & _
                   "   AND ax_coddest = " & lDest
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Else
          'anagra non gestisce anaext: inserisco anaext vuoto
          If strGestAnaext.IndexOf("C") > -1 Then
            strSQL = "INSERT INTO anaext (codditt, ax_conto, ax_coddest, ax_codlead, ax_tipork, ax_tipo1, ax_tipo2, ax_tipo3, " & _
                     "                    ax_descr1, ax_descr2, ax_descr3, ax_descr4, ax_descr5, ax_descr6, ax_descr7, ax_descr8, " & _
                     "                    ax_descr9, ax_descr10, ax_desext1, ax_desext2, ax_desext3, ax_memo1, ax_memo2, ax_data1, " & _
                     "                    ax_data2, ax_data3, ax_data4, ax_data5, ax_num1, ax_num2, ax_num3, ax_num4, " & _
                     "                    ax_num5, ax_num6, ax_num7, ax_num8, ax_num9, ax_num10, ax_check1, ax_check2, " & _
                     "                    ax_check3, ax_check4, ax_check5, ax_check6, ax_check7, ax_check8, ax_check9, ax_check10, " & _
                     "                    ax_combo1, ax_combo2, ax_combo3, ax_ultagg, ax_opnome, ax_codart, ax_matric)" & _
                     " SELECT codditt, 0, 0, le_codlead, 'L', ' ', ' ', ' ', " & _
                     "        ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', " & _
                     "        ' ', ' ', ' ', ' ', ' ', null, null, null, " & _
                     "        null, null, null, null, 0, 0, 0, 0, " & _
                     "        0, 0, 0, 0, 0, 0, 'N', 'N', " & _
                     "        'N', 'N', 'N', 'N', 'N', 'N', 'N', 'N', " & _
                     "        'A', 'A', 'A', le_ultagg, " & CStrSQL(oApp.User.Nome) & ", ' ', ' '" & _
                     " FROM leads " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     "   AND le_coddest = " & lDest & _
                     "   AND le_conto = " & lConto
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        End If
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function InserisciAcclead(ByVal strCodditt As String, ByVal strOpnome As String, _
    ByVal lCodlead As Integer, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO acclead (codditt, opcr_opnome, opcr_codlead, opcr_crmvis, opcr_crmmod)" & _
        " VALUES (" & CStrSQL(strCodditt) & ", " & CStrSQL(strOpnome) & ", " & lCodlead & ", 'S', 'S')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function SalvaAnagen(ByVal dtrA() As DataRow, ByVal strDataAgg As String, ByVal strDtIniAnagen As String, _
                                          ByRef dttDestdiv As DataTable, ByVal dbConn As DbConnection) As Boolean
    'aggiorno anagen e anasto: poi chiamo AggiornaAnagen per sincronizzare anagra, destdiv, tabanaz, anazul, leads
    Dim strSQL As String = ""
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim bNewAnagen As Boolean = True
    Dim i As Integer = 0
    Dim bOk As Boolean = True

    Try
      strSQL = "SELECT * FROM anagen WHERE ag_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then
        bNewAnagen = False

        'se non ho modificato nulla tra anagra e anagen o tra destdiv e desgen, non aggiorno nulla
        For i = 0 To dttTmp.Columns.Count - 1
          If dttTmp.Columns(i).ToString.ToLower <> "ts" And _
             dttTmp.Columns(i).ToString.ToLower <> "codditt" And _
             dttTmp.Columns(i).ToString.ToLower <> "ag_ultagg" And _
             dttTmp.Columns(i).ToString.ToLower <> "ag_intgen" Then
            If NTSCStr(dtrA(0)("an_" & dttTmp.Columns(i).ColumnName.Substring(3))) <> NTSCStr(dttTmp.Rows(0)(dttTmp.Columns(i).ColumnName)) Then
              bOk = False
              GoTo SALTA
            End If
          End If
        Next
        dttTmp.Clear()
        dttTmp = New DataTable
        If bOk Then
          'cerco eventuali modifiche alle destinazioni diverse
          If dttDestdiv.Rows.Count > 0 Then
            strSQL = "SELECT * FROM destgen WHERE dd_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            If dttTmp.Rows.Count <> dttDestdiv.Rows.Count Then
              bOk = False
              GoTo SALTA
            Else
              For Each dtrS As DataRow In dttDestdiv.Rows
                Dim dtrD() As DataRow = dttTmp.Select("dd_coddest = " & dtrS!dd_coddest.ToString)
                If dtrD.Length = 0 Then
                  bOk = False
                  GoTo SALTA
                Else
                  For i = 0 To dttTmp.Columns.Count - 1
                    If dttTmp.Columns(i).ToString.ToLower <> "ts" And _
                       dttTmp.Columns(i).ToString.ToLower <> "dd_codanag" Then
                      If NTSCStr(dtrS(dttTmp.Columns(i).ColumnName)) <> NTSCStr(dtrD(0)(dttTmp.Columns(i).ColumnName)) Then
                        bOk = False
                        GoTo SALTA
                      End If
                    End If
                  Next
                End If
              Next    'For Each dtrS As DataRow In dttDestdiv.Rows
            End If
          End If    'If dttDestdiv.Rows.Count > 0 Then
        End If    'If bOk Then
      Else
        bOk = False
      End If    'If dttTmp.Rows.Count > 0 Then
SALTA:
      dttTmp.Clear()
      If bOk Then Return True 'non è cambiato nulla tra anagra e anagen e tra destdiv e desgen: non aggiorno

      If bNewAnagen Then
        strSQL = "INSERT INTO anagen (ag_codanag, ag_descr1, ag_descr2, ag_indir, ag_cap, ag_citta, ag_prov, ag_stato, ag_codfis, ag_pariva, ag_telef, ag_faxtlx, ag_valuta," & _
                  " ag_codling, ag_ultagg, ag_destin, ag_destpag, ag_note, ag_note2, ag_email, ag_website, ag_usaem, ag_opnome, ag_webuid, ag_webpwd, ag_sesso, ag_datnasc," & _
                  " ag_citnasc, ag_pronasc, ag_stanasc, ag_codfisest, ag_cell, ag_titolo, ag_persfg, ag_profes, ag_condom, ag_tpsogiva, ag_codcomu, ag_destcorr, ag_destsedel," & _
                  " ag_destdomf, ag_destresan, ag_siglaric, ag_cognome, ag_nome, ag_codcomn," & _
                  " ag_nazion1, ag_nazion2, ag_statofed, ag_soggresi, ag_omocodice," & _
                  " ag_estcodiso, ag_estpariva, ag_codrtac, ag_latitud, ag_longitud) " & _
                  " SELECT an_codanag, an_descr1, an_descr2, an_indir, an_cap, an_citta, an_prov, an_stato, an_codfis, an_pariva, an_telef, an_faxtlx, an_valuta," & _
                  " an_codling, an_ultagg, an_destin, an_destpag, an_note, an_note2, an_email, an_website, an_usaem, an_opnome, an_webuid, an_webpwd, an_sesso, an_datnasc," & _
                  " an_citnasc, an_pronasc, an_stanasc, an_codfisest, an_cell, an_titolo, an_persfg, an_profes, an_condom, an_tpsogiva, an_codcomu, an_destcorr, an_destsedel," & _
                  " an_destdomf, an_destresan, an_siglaric, an_cognome, an_nome, an_codcomn," & _
                  " an_nazion1, an_nazion2, an_statofed, an_soggresi, an_omocodice," & _
                  " an_estcodiso, an_estpariva, an_codrtac, an_latitud, an_longitud" & _
                  " FROM anagra " & _
                  " WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                  " AND an_conto = " & NTSCInt(dtrA(0)!an_conto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = " INSERT INTO anasto (as_codanag, as_datini, as_datfin, as_descr1, as_descr2, as_indir, as_cap, " & _
                 " as_citta, as_prov, as_stato, as_codfis, as_pariva, as_telef, as_faxtlx, " & _
                 " as_valuta, as_codling, as_destin, as_destpag, as_note, as_note2, as_email, " & _
                 " as_website, as_usaem, as_webuid, as_webpwd, as_sesso, as_datnasc, as_citnasc, " & _
                 " as_pronasc, as_stanasc, as_codfisest, as_cell, as_titolo, as_persfg, as_profes, " & _
                 " as_condom, as_tpsogiva, as_codcomu, as_destcorr, " & _
                 " as_destsedel, as_destdomf, as_destresan, as_siglaric, as_cognome, as_nome, as_codcomn, " & _
                 " as_nazion1, as_nazion2, as_intgen, as_statofed, as_soggresi, as_omocodice, as_estcodiso, " & _
                 " as_estpariva, as_codrtac, as_latitud, as_longitud)" & _
                 " SELECT ag_codanag, " & CDataSQL(strDtIniAnagen) & " , " & CDataSQL(IntSetDate("31/12/2099")) & ", ag_descr1, ag_descr2, ag_indir, ag_cap, " & _
                 " ag_citta, ag_prov, ag_stato, ag_codfis, ag_pariva, ag_telef, ag_faxtlx, " & _
                 " ag_valuta, ag_codling, ag_destin, ag_destpag, ag_note, ag_note2, ag_email, " & _
                 " ag_website, ag_usaem, ag_webuid, ag_webpwd, ag_sesso, ag_datnasc, ag_citnasc, " & _
                 " ag_pronasc, ag_stanasc, ag_codfisest, ag_cell, ag_titolo, ag_persfg, ag_profes, ag_condom, ag_tpsogiva, ag_codcomu, ag_destcorr, " & _
                 " ag_destsedel, ag_destdomf, ag_destresan, ag_siglaric, ag_cognome, ag_nome, ag_codcomn, ag_nazion1, ag_nazion2, ag_intgen, ag_statofed, " & _
                 " ag_soggresi, ag_omocodice, ag_estcodiso, ag_estpariva, ag_codrtac, " & _
                 " ag_latitud, ag_longitud " & _
                 " FROM anagen " & _
                 " WHERE ag_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Else
        strSQL = "UPDATE anagen SET anagen.ag_descr1 = anagra.an_descr1, anagen.ag_descr2 = anagra.an_descr2, anagen.ag_indir = anagra.an_indir, anagen.ag_cap = anagra.an_cap, " & _
                 " anagen.ag_citta = anagra.an_citta, anagen.ag_prov = anagra.an_prov, anagen.ag_stato = anagra.an_stato, anagen.ag_codfis = anagra.an_codfis, " & _
                 " anagen.ag_pariva = anagra.an_pariva, anagen.ag_telef = anagra.an_telef, anagen.ag_faxtlx = anagra.an_faxtlx, anagen.ag_valuta = anagra.an_valuta, " & _
                 " anagen.ag_codling = anagra.an_codling, anagen.ag_ultagg = " & CDataOraSQL(DateTime.Now) & ", anagen.ag_destin = anagra.an_destin, anagen.ag_destpag = anagra.an_destpag, " & _
                 " anagen.ag_note = anagra.an_note, anagen.ag_note2 = anagra.an_note2, anagen.ag_email = anagra.an_email, anagen.ag_website = anagra.an_website, " & _
                 " anagen.ag_usaem = anagra.an_usaem, anagen.ag_opnome = " & CStrSQL(oApp.User.Nome) & ", anagen.ag_webuid = anagra.an_webuid, anagen.ag_webpwd = anagra.an_webpwd, " & _
                 " anagen.ag_sesso = anagra.an_sesso, anagen.ag_datnasc = anagra.an_datnasc, anagen.ag_citnasc = anagra.an_citnasc, anagen.ag_pronasc = anagra.an_pronasc, " & _
                 " anagen.ag_stanasc = anagra.an_stanasc, anagen.ag_codfisest = anagra.an_codfisest, anagen.ag_cell = anagra.an_cell, anagen.ag_titolo = anagra.an_titolo, " & _
                 " anagen.ag_persfg = anagra.an_persfg, anagen.ag_profes = anagra.an_profes, anagen.ag_condom = anagra.an_condom, anagen.ag_tpsogiva = anagra.an_tpsogiva, " & _
                 " anagen.ag_codcomu = anagra.an_codcomu, anagen.ag_destcorr = anagra.an_destcorr, anagen.ag_destsedel = anagra.an_destsedel, anagen.ag_destdomf = anagra.an_destdomf, " & _
                 " anagen.ag_destresan = anagra.an_destresan, anagen.ag_siglaric = anagra.an_siglaric, anagen.ag_cognome = anagra.an_cognome, anagen.ag_nome = anagra.an_nome, " & _
                 " anagen.ag_codcomn = anagra.an_codcomn, anagen.ag_nazion1 = anagra.an_nazion1, anagen.ag_nazion2 = anagra.an_nazion2, " & _
                 " anagen.ag_statofed = anagra.an_statofed, anagen.ag_soggresi = anagra.an_soggresi, anagen.ag_omocodice = anagra.an_omocodice, " & _
                 " anagen.ag_estcodiso = anagra.an_estcodiso, anagen.ag_estpariva = anagra.an_estpariva, anagen.ag_codrtac = anagra.an_codrtac, " & _
                 " anagen.ag_latitud = anagra.an_latitud, anagen.ag_longitud = anagra.an_longitud " & _
                 " FROM anagen, anagra " & _
                 " WHERE anagen.ag_codanag = anagra.an_codanag " & _
                 " And anagen.ag_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString & _
                 " And anagra.codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                 " And anagra.an_conto = " & NTSCInt(dtrA(0)!an_conto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = " SELECT as_datini " & _
                 " FROM anasto " & _
                 " WHERE as_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString & _
                 " And as_datini = " & CDataSQL(strDataAgg)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count = 0 Then
          strSQL = " INSERT INTO anasto (as_codanag, as_datini, as_datfin, as_descr1, as_descr2, as_indir, as_cap, " & _
                   " as_citta, as_prov, as_stato, as_codfis, as_pariva, as_telef, as_faxtlx, " & _
                   " as_valuta, as_codling, as_destin, as_destpag, as_note, as_note2, as_email, " & _
                   " as_website, as_usaem, as_webuid, as_webpwd, as_sesso, as_datnasc, as_citnasc, " & _
                   " as_pronasc, as_stanasc, as_codfisest, as_cell, as_titolo, as_persfg, as_profes, as_condom, as_tpsogiva, as_codcomu, as_destcorr, " & _
                   " as_destsedel, as_destdomf, as_destresan, as_siglaric, as_cognome, as_nome, as_codcomn, as_nazion1, as_nazion2, as_intgen, as_statofed, " & _
                   " as_soggresi, as_omocodice, as_estcodiso, as_estpariva, as_codrtac, as_latitud, as_longitud )" & _
                   " SELECT ag_codanag, " & CDataSQL(strDataAgg) & " , " & CDataSQL(IntSetDate("31/12/2099")) & ", ag_descr1, ag_descr2, ag_indir, ag_cap, " & _
                   " ag_citta, ag_prov, ag_stato, ag_codfis, ag_pariva, ag_telef, ag_faxtlx, " & _
                   " ag_valuta, ag_codling, ag_destin, ag_destpag, ag_note, ag_note2, ag_email, " & _
                   " ag_website, ag_usaem, ag_webuid, ag_webpwd, ag_sesso, ag_datnasc, ag_citnasc, " & _
                   " ag_pronasc, ag_stanasc, ag_codfisest, ag_cell, ag_titolo, ag_persfg, ag_profes, ag_condom, ag_tpsogiva, ag_codcomu, ag_destcorr, " & _
                   " ag_destsedel, ag_destdomf, ag_destresan, ag_siglaric, ag_cognome, ag_nome, ag_codcomn, " & _
                   " ag_nazion1, ag_nazion2, ag_intgen, ag_statofed, ag_soggresi, ag_omocodice, ag_estcodiso, " & _
                   " ag_estpariva, ag_codrtac, ag_latitud, ag_longitud " & _
                   " FROM anagen " & _
                   " WHERE ag_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'aggiorno la data di fine del record precedente
          strSQL = "SELECT top 1 as_datini FROM anasto " & _
                   " WHERE as_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString & _
                   " And as_datini < " & CDataSQL(strDataAgg) & _
                   " ORDER BY as_datini DESC "
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = " UPDATE anasto SET as_datfin = " & CDataSQL(DateAdd("d", -1, DateTime.Now).ToShortDateString) & _
                   " WHERE as_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString & _
                   " AND as_datini = " & CDataSQL(NTSCDate(dttTmp.Rows(0)!as_datini).ToShortDateString)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          dttTmp.Clear()
        Else
          strSQL = " UPDATE anasto " & _
                   " SET anasto.as_descr1 = anagen.ag_descr1, anasto.as_descr2 = anagen.ag_descr2, anasto.as_indir = anagen.ag_indir, " & _
                   " anasto.as_cap = anagen.ag_cap, anasto.as_citta = anagen.ag_citta, " & _
                   " anasto.as_prov = anagen.ag_prov, anasto.as_stato = anagen.ag_stato, " & _
                   " anasto.as_codfis = anagen.ag_codfis, anasto.as_pariva = anagen.ag_pariva, " & _
                   " anasto.as_telef = anagen.ag_telef, anasto.as_faxtlx = anagen.ag_faxtlx, " & _
                   " anasto.as_valuta = anagen.ag_valuta, anasto.as_codling = anagen.ag_codling, " & _
                   " anasto.as_destin = anagen.ag_destin, anasto.as_destpag = anagen.ag_destpag, " & _
                   " anasto.as_note = anagen.ag_note, anasto.as_note2 = anagen.ag_note2, " & _
                   " anasto.as_email = anagen.ag_email, anasto.as_website = anagen.ag_website, anasto.as_usaem = anagen.ag_usaem, " & _
                   " anasto.as_webuid = anagen.ag_webuid, anasto.as_webpwd = anagen.ag_webpwd, anasto.as_sesso = anagen.ag_sesso, " & _
                   " anasto.as_datnasc = anagen.ag_datnasc, anasto.as_citnasc = anagen.ag_citnasc, anasto.as_pronasc = anagen.ag_pronasc, " & _
                   " anasto.as_stanasc = anagen.ag_stanasc, anasto.as_codfisest = anagen.ag_codfisest, " & _
                   " anasto.as_cell = anagen.ag_cell, anasto.as_titolo = anagen.ag_titolo, anasto.as_persfg = anagen.ag_persfg, " & _
                   " anasto.as_profes = anagen.ag_profes, anasto.as_condom = anagen.ag_condom, anasto.as_tpsogiva = anagen.ag_tpsogiva, anasto.as_codcomu = anagen.ag_codcomu, anasto.as_destcorr = anagen.ag_destcorr, " & _
                   " anasto.as_destsedel = anagen.ag_destsedel, anasto.as_destdomf = anagen.ag_destdomf, anasto.as_destresan = anagen.ag_destresan, anasto.as_siglaric = anagen.ag_siglaric, anasto.as_cognome = anagen.ag_cognome, anasto.as_nome = anagen.ag_nome, " & _
                   " anasto.as_codcomn = anagen.ag_codcomn, anasto.as_nazion1 = anagen.ag_nazion1, anasto.as_nazion2 = anagen.ag_nazion2, anasto.as_intgen = anagen.ag_intgen, anasto.as_statofed = anagen.ag_statofed, " & _
                   " anasto.as_soggresi = anagen.ag_soggresi, anasto.as_omocodice = anagen.ag_omocodice, anasto.as_estcodiso = anagen.ag_estcodiso, anasto.as_estpariva = anagen.ag_estpariva, anasto.as_codrtac = anagen.ag_codrtac, " & _
                   " anasto.as_latitud = anagen.ag_latitud, anasto.as_longitud = anagen.ag_longitud " & _
                   " FROM anasto, anagen " & _
                   " WHERE anasto.as_codanag = anagen.ag_codanag " & _
                   " And anasto.as_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString & _
                   " And as_datini = " & CDataSQL(NTSCDate(dttTmp.Rows(0)!as_datini).ToShortDateString)
          dttTmp.Clear()
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If    'If dttTmp.Rows.Count > 0 Then
      End If

      '--------------------------------------------------------------------------
      'adesso stessa cosa per destgen / deststo: inserisco o aggiorno
      For Each dtrD As DataRow In dttDestdiv.Rows
        'devo sapere se la destinazione è già esistente
        strSQL = "SELECT top 1 dd_coddest FROM destgen WHERE dd_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString & " AND dd_coddest = " & NTSCInt(dtrD!dd_coddest).ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count = 0 Then
          'è una nuova destinazione diversa
          strSQL = "INSERT INTO destgen (dd_codanag, dd_coddest, dd_nomdest, dd_nomdest2," & _
                    " dd_inddest, dd_capdest, dd_locdest, dd_prodest, dd_turno, dd_telef, dd_codfis," & _
                    " dd_pariva, dd_faxtlx, dd_email, dd_usaem, dd_stato, dd_codcomu, dd_codfisest," & _
                    " dd_statofed, dd_latitud, dd_longitud)" & _
                    " SELECT " & NTSCInt(dtrA(0)!an_codanag).ToString & ", dd_coddest, dd_nomdest, dd_nomdest2, dd_inddest," & _
                    " dd_capdest, dd_locdest, dd_prodest, dd_turno, dd_telef, dd_codfis, dd_pariva," & _
                    " dd_faxtlx, dd_email, dd_usaem, dd_stato, dd_codcomu, dd_codfisest, " & _
                    " dd_statofed, dd_latitud, dd_longitud" & _
                    " FROM destdiv" & _
                    " WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                    " AND dd_conto = " & NTSCInt(dtrA(0)!an_conto).ToString & _
                    " AND dd_coddest = " & NTSCInt(dtrD!dd_coddest).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          strSQL = "INSERT INTO deststo (dd_codanag, dd_coddest, dd_datini, dd_datfin," & _
                    " dd_nomdest, dd_nomdest2, dd_inddest, dd_capdest, dd_locdest, dd_prodest," & _
                    " dd_turno, dd_telef, dd_codfis, dd_pariva, dd_faxtlx, dd_email, dd_usaem," & _
                    " dd_stato, dd_codcomu, dd_codfisest, dd_statofed, dd_latitud, dd_longitud)" & _
                    " SELECT " & NTSCInt(dtrA(0)!an_codanag).ToString & ", dd_coddest, " & _
                    CDataSQL(strDtIniAnagen) & ", " & _
                    CDataSQL(IntSetDate("31/12/2099")) & ", " & _
                    " dd_nomdest, dd_nomdest2, dd_inddest," & _
                    " dd_capdest, dd_locdest, dd_prodest, dd_turno, dd_telef, dd_codfis, dd_pariva," & _
                    " dd_faxtlx, dd_email, dd_usaem, dd_stato, dd_codcomu, dd_codfisest, dd_statofed, " & _
                    " dd_latitud, dd_longitud" & _
                    " FROM destdiv" & _
                    " WHERE codditt = " & CStrSQL(dtrA(0)!codditt.ToString) & _
                    " AND dd_conto = " & NTSCInt(dtrA(0)!an_conto).ToString & _
                    " AND dd_coddest = " & NTSCInt(dtrD!dd_coddest).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Else
          dttTmp.Clear()

          'destinazione diversa già esistente
          strSQL = "UPDATE destgen SET " & _
                    " destgen.dd_nomdest = " & CStrSQL(NTSCStr(dtrD!dd_nomdest)) & ", " & _
                    " destgen.dd_nomdest2 = " & CStrSQL(NTSCStr(dtrD!dd_nomdest2)) & ", " & _
                    " destgen.dd_inddest = " & CStrSQL(NTSCStr(dtrD!dd_inddest)) & ", " & _
                    " destgen.dd_capdest = " & CStrSQL(NTSCStr(dtrD!dd_capdest)) & ", " & _
                    " destgen.dd_locdest = " & CStrSQL(NTSCStr(dtrD!dd_locdest)) & ", " & _
                    " destgen.dd_prodest = " & CStrSQL(NTSCStr(dtrD!dd_prodest)) & ", " & _
                    " destgen.dd_turno = " & CStrSQL(NTSCStr(dtrD!dd_turno)) & ", " & _
                    " destgen.dd_telef = " & CStrSQL(NTSCStr(dtrD!dd_telef)) & ", " & _
                    " destgen.dd_codfis = " & CStrSQL(NTSCStr(dtrD!dd_codfis)) & "," & _
                    " destgen.dd_pariva = " & CStrSQL(NTSCStr(dtrD!dd_pariva)) & ", " & _
                    " destgen.dd_faxtlx = " & CStrSQL(NTSCStr(dtrD!dd_faxtlx)) & ", " & _
                    " destgen.dd_email = " & CStrSQL(NTSCStr(dtrD!dd_email)) & ", " & _
                    " destgen.dd_usaem = " & CStrSQL(NTSCStr(dtrD!dd_usaem)) & ", " & _
                    " destgen.dd_stato = " & CStrSQL(NTSCStr(dtrD!dd_stato)) & ", " & _
                    " destgen.dd_codcomu = " & CStrSQL(NTSCStr(dtrD!dd_codcomu)) & ", " & _
                    " destgen.dd_codfisest = " & CStrSQL(NTSCStr(dtrD!dd_codfisest)) & ", " & _
                    " destgen.dd_statofed = " & CStrSQL(NTSCStr(dtrD!dd_statofed)) & ", " & _
                    " destgen.dd_latitud = " & CStrSQL(NTSCStr(dtrD!dd_latitud)) & ", " & _
                    " destgen.dd_longitud = " & CStrSQL(NTSCStr(dtrD!dd_longitud)) & _
                    " WHERE destgen.dd_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString & _
                    " AND destgen.dd_coddest = " & NTSCInt(dtrD!dd_coddest).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          strSQL = " SELECT dd_datini " & _
                   " FROM deststo " & _
                   " WHERE dd_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString & _
                   " AND dd_coddest = " & NTSCInt(dtrD!dd_coddest).ToString & _
                   " And dd_datini = " & CDataSQL(strDataAgg)
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          If dttTmp.Rows.Count = 0 Then
            strSQL = "INSERT INTO deststo (dd_codanag, dd_coddest, dd_datini, dd_datfin," & _
                      " dd_nomdest, dd_nomdest2, dd_inddest, dd_capdest, dd_locdest, dd_prodest," & _
                      " dd_turno, dd_telef, dd_codfis, dd_pariva, dd_faxtlx, dd_email, dd_usaem," & _
                      " dd_stato, dd_codcomu, dd_codfisest, dd_statofed, dd_latitud, dd_longitud)" & _
                      " SELECT " & NTSCInt(dtrA(0)!an_codanag).ToString & ", dd_coddest, " & _
                      CDataSQL(strDataAgg) & ", " & _
                      CDataSQL(IntSetDate("31/12/2099")) & ", " & _
                      " dd_nomdest, dd_nomdest2, dd_inddest," & _
                      " dd_capdest, dd_locdest, dd_prodest, dd_turno, dd_telef, dd_codfis, dd_pariva," & _
                      " dd_faxtlx, dd_email, dd_usaem, dd_stato, dd_codcomu, dd_codfisest, " & _
                      " dd_statofed, dd_latitud, dd_longitud" & _
                      " FROM destgen" & _
                      " WHERE dd_codanag = " & CStrSQL(dtrA(0)!an_codanag.ToString) & _
                      " AND dd_coddest = " & NTSCInt(dtrD!dd_coddest).ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            'aggiorno la data di fine del record precedente
            strSQL = "SELECT top 1 dd_datini FROM deststo " & _
                     " WHERE dd_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString & _
                     " AND dd_coddest = " & NTSCInt(dtrD!dd_coddest).ToString & _
                     " And dd_datini < " & CDataSQL(strDataAgg) & _
                     " ORDER BY dd_datini DESC "
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            strSQL = " UPDATE deststo SET dd_datfin = " & CDataSQL(DateAdd("d", -1, DateTime.Now).ToShortDateString) & _
                     " WHERE dd_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString & _
                     " AND dd_coddest = " & NTSCInt(dtrD!dd_coddest).ToString & _
                     " AND dd_datini = " & CDataSQL(NTSCDate(dttTmp.Rows(0)!dd_datini).ToShortDateString)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            dttTmp.Clear()

          Else
            strSQL = "UPDATE deststo SET " & _
                      " deststo.dd_nomdest = destgen.dd_nomdest, " & _
                      " deststo.dd_nomdest2 = destgen.dd_nomdest2 ," & _
                      " deststo.dd_inddest = destgen.dd_inddest, " & _
                      " deststo.dd_capdest = destgen.dd_capdest, " & _
                      " deststo.dd_locdest = destgen.dd_locdest, " & _
                      " deststo.dd_prodest = destgen.dd_prodest, " & _
                      " deststo.dd_turno = destgen.dd_turno, " & _
                      " deststo.dd_telef = destgen.dd_telef, " & _
                      " deststo.dd_codfis = destgen.dd_codfis," & _
                      " deststo.dd_pariva = destgen.dd_pariva, " & _
                      " deststo.dd_faxtlx = destgen.dd_faxtlx, " & _
                      " deststo.dd_email = destgen.dd_email, " & _
                      " deststo.dd_usaem = destgen.dd_usaem, " & _
                      " deststo.dd_stato = destgen.dd_stato, " & _
                      " deststo.dd_codcomu = destgen.dd_codcomu, " & _
                      " deststo.dd_codfisest = destgen.dd_codfisest, " & _
                      " deststo.dd_statofed = destgen.dd_statofed, " & _
                      " deststo.dd_latitud = destgen.dd_latitud, " & _
                      " deststo.dd_longitud = destgen.dd_longitud " & _
                      " FROM deststo, destgen " & _
                      " WHERE destgen.dd_codanag = deststo.dd_codanag " & _
                      " AND destgen.dd_coddest = deststo.dd_coddest" & _
                      " AND destgen.dd_codanag = " & NTSCInt(dtrA(0)!an_codanag).ToString & _
                      " AND destgen.dd_coddest = " & NTSCInt(dtrD!dd_coddest).ToString & _
                      " AND deststo.dd_datini = " & CDataSQL(NTSCDate(dttTmp.Rows(0)!dd_datini).ToShortDateString)
            dttTmp.Clear()
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        End If
      Next    'For Each dtrD As DataRow In dttDestdiv.Rows

      'adesso sincronizzo tutto (tabanaz, le altre anagen, i leads, destdiv, ...) da anagen/destgen
      If Not AggiornaAnagen(CStrSQL(dtrA(0)!codditt.ToString), "ANAGRA", NTSCInt(dtrA(0)!an_codanag), NTSCInt(dtrA(0)!an_conto), dbConn) Then Return False

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function Cancella(ByVal strDitta As String, ByVal lConto As Integer, ByVal strTipo As String, _
                                      ByVal bModuloCRM As Boolean, ByVal bModuloAS As Boolean, _
                                      ByVal bSganciaOrganig As Boolean, ByVal strGestAnaext As String, _
                                      ByVal bScriviActlog As Boolean, ByVal strDesconto As String) As Boolean
    Dim strSQL As String = ""
    Dim strDesogglog As String = ""
    Dim strSqlWhere As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dttTmp As New DataTable

    Try

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      strSQL = "DELETE FROM anatric" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND ant_conto = " & lConto.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)


      strSQL = "DELETE FROM listini" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_conto = " & lConto.ToString & _
                " AND lc_listino = 0" & _
                " AND lc_codcas = ' '"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM sconti" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND so_conto = " & lConto.ToString & _
                " AND so_clscan = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM perprov" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND per_conto = " & lConto.ToString & _
              " AND per_clpran = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM codarfo" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND caf_conto = " & lConto.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM clibanc" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND cba_conto = " & lConto.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM clistpg" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND cts_conto = " & lConto.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM clitipb" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND ctp_conto = " & lConto.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM TRKTPBF" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND tkt_conto = " & lConto.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM destdiv" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND dd_conto = " & lConto.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '''''''--------- Ricontrollo per logiche ORGANIZZAZIONE - LEAD -1
      If Not bModuloCRM AndAlso Not bModuloAS Then
        strSQL = "DELETE FROM organig" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 "   AND og_conto = " & lConto.ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Else
        '---------------------------------------------------------------------------------------
        '--- Cancella fisicamente i records di ORGANIG che sono agganciati al conto
        '--- ma NON ad un LEAD
        '---------------------------------------------------------------------------------------
        If bModuloCRM Then
          strSQL = "UPDATE organig SET og_conto = 0, og_coddest = 0, og_codlead = -1 " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND og_conto = " & lConto.ToString & _
                   " AND og_codlead = 0 " & _
                   "   AND NOT EXISTS (SELECT TOP 1 co_progr FROM contatti " & _
                   "                   INNER JOIN organig AS organig2 ON co_progr = organig2.og_contatto " & _
                   "                   WHERE co_progr = organig.og_contatto " & _
                   "                     AND organig2.og_progr <> organig.og_progr) "
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
        '---------------------------------------------------------------------------------------
        strSQL = "DELETE FROM organig" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND og_conto = " & lConto.ToString & _
                " AND og_codlead = 0"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '---------------------------------------------------------------------------------------
        '--- Azzera il campo conto di ORGANIG e il setta tipork = 'L' per i records
        '--- agganciati ad un LEAD
        '---------------------------------------------------------------------------------------
        If Not bSganciaOrganig Then
          strSQL = "UPDATE organig" & _
                   " SET og_conto = 0," & _
                   "     og_tipork = 'L'," & _
                   "     og_servdtua = " & CDataOraSQL(DataGMT) & "," & _
                   "     og_clirdtua = " & CDataOraSQL(New Date(1900, 1, 1)) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   "   AND og_conto = " & lConto.ToString & _
                   "   AND og_codlead <> 0"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      End If
      If (bModuloCRM OrElse bModuloAS) AndAlso strTipo.ToUpper = "C" Then
        '------------------------------------------------------------------------------------------------------------
        If bSganciaOrganig Then
          strSQL = "SELECT le_codlead FROM leads" & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   "   AND le_conto = " & lConto.ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          For i As Integer = 0 To (dttTmp.Rows.Count - 1)
            If bModuloCRM Then
              strSQL = "UPDATE organig SET og_conto = 0, og_coddest = 0, og_codlead = -1 " & _
                       " WHERE codditt = " & CStrSQL(strDitta) & _
                       "   AND og_codlead = " & NTSCInt(dttTmp.Rows(i)!le_codlead) & _
                       "   AND NOT EXISTS (SELECT TOP 1 co_progr FROM contatti " & _
                       "                   INNER JOIN organig AS organig2 ON co_progr = organig2.og_contatto " & _
                       "                   WHERE co_progr = organig.og_contatto " & _
                       "                     AND organig2.og_progr <> organig.og_progr) "
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            End If
            strSQL = "DELETE organig " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     "   AND og_codlead = " & NTSCInt(dttTmp.Rows(i)!le_codlead)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "DELETE FROM allole" & _
                     " WHERE ao_tipo = 'R'" & _
                     "   AND ao_codlead = " & NTSCInt(dttTmp.Rows(i)!le_codlead)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "DELETE FROM acclead" & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     "   AND opcr_codlead = " & NTSCInt(dttTmp.Rows(i)!le_codlead)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "DELETE FROM leads " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     "   AND le_codlead = " & NTSCInt(dttTmp.Rows(i)!le_codlead)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
        Else
          strSQL = "UPDATE leads" & _
                   " SET le_conto = 0," & _
                   "     le_coddest = 0," & _
                   "     le_servdtua = " & CDataOraSQL(DataGMT) & "," & _
                   "     le_clirdtua = " & CDataOraSQL(New Date(1900, 1, 1)) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   "   AND le_conto = " & lConto.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      End If

      If strGestAnaext <> "" Then
        strSQL = "DELETE FROM anaext" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                "   AND ax_conto = " & lConto.ToString & _
                "   AND ax_codlead = 0" & _
                "   AND ax_tipork IN (" & CStrSQL(strTipo) & ", " & CStrSQL(IIf(strTipo = "C", "D", "E").ToString) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      strSQL = "DELETE FROM allole" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND ao_tipo = 'C'" & _
               "   AND ao_codice = " & lConto.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM anacona" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND acn_conto = " & lConto.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '---------------------------------
      'scrivo il log
      If bScriviActlog Then
        strSqlWhere = CStrSQL(strDitta) & "," & lConto.ToString
        strDesogglog = oApp.Tr(Me, 128388105998858000, "Cancellaz. dati relativi all'anagrafica '|" & lConto.ToString & " - " & strDesconto & "|'")
        If Not ScriviActLog(strDitta, "BS--CLIE", "anagra", "anagra", strSqlWhere, "A", "D", strDesogglog, False) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
      End If

      strSQL = "DELETE FROM anagra" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND an_conto = " & lConto.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

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
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function




  Public Overridable Function CheckWebUserID(ByVal strDitta As String, ByVal lconto As Integer, ByVal strUserID As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT an_conto FROM anagra WHERE an_webuid = " & CStrSQL(strUserID) & " AND an_conto <> " & lconto.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return False
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckWebUserPwd(ByVal strDitta As String, ByVal lconto As Integer, ByVal strUserPWD As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT an_conto FROM anagra WHERE an_webpwd = " & CStrSQL(strUserPWD) & " AND an_conto <> " & lconto.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return False
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetOpincaricato(ByVal strDitta As String, ByVal nCodagente As Integer, ByVal dbConn As DbConnection) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim nOut As Integer = 0
    Try
      strSQL = "SELECT TOP 1 og_progr FROM organig" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND og_tipork <> 'L' AND og_conto = 0" & _
              " AND og_codcage = " & CInt(nCodagente) & _
              " ORDER BY codditt, og_progr"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then nOut = NTSCInt(dttTmp.Rows(0)!og_progr)
      dttTmp.Clear()

      Return nOut

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetDestgen(ByVal lCodanag As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT destgen.*, co_denom as xx_codcomu, tb_desstat as xx_stato " & _
               " FROM (destgen LEFT JOIN comuni ON destgen.dd_codcomu = comuni.co_codcomu) " & _
               " LEFT JOIN tabstat ON destgen.dd_stato = tabstat.tb_codstat " & _
               " WHERE dd_codanag = " & lCodanag.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function DuplicaListiniScontiProvv(ByVal strDitta As String, ByVal lConto As Integer, _
                                                        ByVal lContoNew As Integer, ByVal bListini As Boolean, _
                                                        ByVal bSconti As Boolean, ByVal bProvv As Boolean, _
                                                        ByVal strNewTipo As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      If bListini Then
        strSQL = "SELECT TOP 0 * FROM listini"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = GetQueryInsertField(dttTmp, "lc_", "lc_progr")
        strSQL = " INSERT INTO listini " & strSQL & _
                 " SELECT " & strSQL.Replace("lc_conto,", lContoNew.ToString & ",").Replace("(", "").Replace(")", "").Replace("lc_tipo, ", "'" & strNewTipo & "', ") & _
                 " FROM listini WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND lc_conto = " & lConto.ToString & _
                 " AND lc_listino = 0 " & _
                 " AND lc_codcas = ' '"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        dttTmp.Clear()
      End If

      If bSconti Then
        strSQL = "SELECT TOP 0 * FROM sconti"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = GetQueryInsertField(dttTmp, "so_")
        strSQL = " INSERT INTO sconti " & strSQL & _
                 " SELECT " & strSQL.Replace("so_conto,", lContoNew.ToString & ",").Replace("(", "").Replace(")", "") & _
                 " FROM sconti WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND so_conto = " & lConto.ToString & _
                 " AND so_clscan = 0"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        dttTmp.Clear()
      End If

      If bProvv Then
        strSQL = "SELECT TOP 0 * FROM perprov"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = GetQueryInsertField(dttTmp, "per_")
        strSQL = " INSERT INTO perprov " & strSQL & _
                 " SELECT " & strSQL.Replace("per_conto,", lContoNew.ToString & ",").Replace("(", "").Replace(")", "") & _
                 " FROM perprov WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND per_conto = " & lConto.ToString & _
                 " AND per_clpran = 0"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        dttTmp.Clear()
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetWhereHlmo(ByVal strDitta As String, ByVal lConto As Integer) As String
    Dim strSQL As String = ""
    Dim strTipord As String = ""
    Dim strScorpo As String = ""
    Try
      GetWhereHlmo = ""

      strSQL = " td_conto = " & lConto.ToString & " AND mo_flevas = 'C'"

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RiempiTTDESTDIVPerStampa(ByVal strDitta As String, ByVal lInstid As Integer, _
      ByRef dttTmp As DataTable) As Boolean
    Dim strElencoCampi As String = "instid, codditt, dd_conto, dd_coddest, dd_nomdest, dd_inddest, dd_capdest," & _
                                   " dd_locdest, dd_prodest, dd_turno, dd_telef, dd_codzona, dd_codfis, dd_pariva," & _
                                   " dd_faxtlx, dd_agente, dd_agente2, dd_email, dd_usaem, dd_vett, dd_vett2," & _
                                   " dd_deszona, dd_descage, dd_descage2, dd_desvett, dd_desvett2, dd_nomdest2," & _
                                   " dd_stato, dd_note, dd_codcomu, dd_codfisest, dd_statofed, dd_codlead, " & _
                                   " dd_latitud, dd_longitud"
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM TTDESTDIV" & _
        " WHERE instid = " & lInstid & _
        " AND codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dttTmp.Rows.Count - 1)
        With dttTmp.Rows(i)
          strSQL = "INSERT INTO TTDESTDIV (" & strElencoCampi & ")" & _
            " VALUES (" & lInstid & ", " & CStrSQL(strDitta) & ", " & NTSCInt(!dd_conto) & ", " & _
            NTSCInt(!dd_coddest) & ", " & CStrSQL(!dd_nomdest) & ", " & CStrSQL(!dd_inddest) & ", " & _
            CStrSQL(!dd_capdest) & ", " & CStrSQL(!dd_locdest) & ", " & CStrSQL(!dd_prodest) & ", " & _
            CStrSQL(!dd_turno) & ", " & CStrSQL(!dd_telef) & ", " & NTSCInt(!dd_codzona) & ", " & _
            CStrSQL(!dd_codfis) & ", " & CStrSQL(!dd_pariva) & ", " & CStrSQL(!dd_faxtlx) & ", " & _
            NTSCInt(!dd_agente) & ", " & NTSCInt(!dd_agente2) & ", " & CStrSQL(!dd_email) & ", " & _
            CStrSQL(!dd_usaem) & ", " & NTSCInt(!dd_vett) & ", " & NTSCInt(!dd_vett2) & ", " & _
            CStrSQL(!xx_codzona) & ", " & CStrSQL(!xx_agente) & ", " & CStrSQL(!xx_agente2) & ", " & _
            CStrSQL(!xx_vett) & ", " & CStrSQL(!xx_vett2) & ", " & CStrSQL(!dd_nomdest2) & ", " & _
            CStrSQL(!dd_stato) & ", " & CStrSQL(!dd_note) & ", " & CStrSQL(!dd_codcomu) & ", " & _
            CStrSQL(!dd_codfisest) & ", " & CStrSQL(!dd_statofed) & ", " & NTSCInt(!dd_codlead) & ", " & _
            CStrSQL(!dd_latitud) & ", " & CStrSQL(!dd_longitud) & ")"
        End With
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetBusUserFromAccditoAgente(ByVal strDitta As String, ByVal nAgente As Integer, _
                                                        ByRef strOut As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    strOut = ""
    Try
      strSQL = "SELECT * FROM accdito WHERE codditt = " & CStrSQL(strDitta) & _
               " AND opdi_codcage = " & nAgente.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then strOut = NTSCStr(dttTmp.Rows(0)!opdi_opnome).Trim

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function PrimoMagazzino(ByVal strDitta As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tb_codmaga FROM tabmaga" & _
        " ORDER BY codditt, tb_codmaga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return 0
      '--------------------------------------------------------------------------------------------------------------
      Return NTSCInt(dttTmp.Rows(0)!tb_codmaga)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function ProgressiviMagazzino(ByVal strDitta As String, ByVal strCodart As String, _
      ByVal nMagaz As Integer, ByVal nFase As Integer, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM artpro" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ap_codart = " & CStrSQL(strCodart) & _
        " AND ap_magaz = " & nMagaz & _
        " AND ap_fase = " & nFase
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function RitornaOperatoriLead(ByVal strDitta As String, ByVal lConto As Integer, _
    ByVal nAgente As Integer, ByRef strOperatore As String) As Boolean
    Dim lCodlead As Integer = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strOperatore = ""
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT le_codlead FROM leads" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND le_conto = " & lConto & _
        " AND le_coddest = 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      lCodlead = NTSCInt(dttTmp.Rows(0)!le_codlead)
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 opdi_opnome FROM accdito" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND opdi_codcage = " & nAgente
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strOperatore = NTSCStr(dttTmp.Rows(0)!opdi_opnome)
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function CancellaOperatoreLead(ByVal strDitta As String, ByVal lConto As Integer, _
    ByVal strOperatore As String) As Boolean
    Dim lCodlead As Integer = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT le_codlead FROM leads" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND le_conto = " & lConto & _
        " AND le_coddest = 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      lCodlead = NTSCInt(dttTmp.Rows(0)!le_codlead)
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM accdlead" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND opcr_opnome = " & NTSCStr(strOperatore) & _
        " AND opcr_codlead = " & lCodlead
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
  Public Overridable Function DestinazioniPreModifica(ByVal strDitta As String, ByVal lConto As Integer, _
      ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM destdiv" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND dd_conto = " & lConto & _
        " AND (dd_agente <> 0 OR dd_agente2 <> 0)"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
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

  Public Overridable Function LeggiDichint(ByVal strDitta As String, ByVal strTipo As String, ByVal nAnno As Integer, ByVal nNumero As Integer, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = " SELECT * FROM dichint" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND di_tipo = " & CStrSQL(strTipo) & _
               " AND di_anno = " & nAnno & _
               " AND di_numero = " & nNumero
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function TestDichint(ByVal strDitta As String, ByVal strTipo As String, ByVal nAnno As Integer, ByVal lNumdoc As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = " SELECT * FROM dichint" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND di_tipo = " & CStrSQL(strTipo) & _
               " AND di_anno = " & nAnno & _
               " AND di_numero = " & lNumdoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function VerificaPresenzaLeads(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 1 " & _
               " FROM leads " & _
               "  WHERE codditt = " & CStrSQL(strDitta) & _
               "    AND le_codlead > -1"

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function VerificaPresenzaClienti(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 1 " & _
               " FROM anagra " & _
               "  WHERE codditt = " & CStrSQL(strDitta) & _
               "    AND an_tipo = 'C'"

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ArticoliDeteriorabili(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 rp_liv1 FROM REGPROP " & _
               " WHERE rp_liv1 = 'BSVEBOLL' " & _
               "   AND rp_liv2 IN ('OPZIONI', 'OPZIONIDOC')  " & _
               "   AND rp_idaz IN ('', " & CStrSQL(strDitta) & ") " & _
               "   AND rp_nomprop = 'SpezzaDocArt62Deterior' " & _
               "   AND rp_valprop <> '0' "

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC).Rows.Count > 0
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  
End Class
