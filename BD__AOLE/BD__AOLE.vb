Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports NTSInformatica.CLN__STD

Public Class CLD__AOLE
  Inherits CLD__BASE

  Public Overridable Function LeggiAllole(ByVal strditta As String, ByRef dsOut As DataSet, ByVal lProgr As Integer, _
                                        ByVal strCod As String, ByVal lcod As Integer, ByVal strMatr As String, _
                                        ByVal strTipodoc As String, ByVal strSerieDoc As String, ByVal nAnnoDoc As Integer, _
                                        ByVal lNumdoc As Integer, ByVal dRigaDoc As Integer, ByVal lCodlead As Integer, _
                                        ByVal lCodoppo As Integer, ByVal lCodchia As Integer, ByVal lNumcontr As Integer, _
                                        ByVal strTipoOgg As String, ByVal strQuery As String, ByVal strPrgParent As String) As Boolean
    Dim strSql As String = ""
    Try
      Return LeggiAllole(strditta, dsOut, lProgr, strCod, lcod, strMatr, strTipodoc, strSerieDoc, nAnnoDoc, _
                         lNumdoc, dRigaDoc, lCodlead, lCodoppo, lCodchia, lNumcontr, strTipoOgg, strQuery, _
                         strPrgParent, False, 0, False, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiAllole(ByVal strditta As String, ByRef dsOut As DataSet, ByVal lProgr As Integer, _
    ByVal strCod As String, ByVal lcod As Integer, ByVal strMatr As String, ByVal strTipodoc As String, _
    ByVal strSerieDoc As String, ByVal nAnnoDoc As Integer, ByVal lNumdoc As Integer, ByVal dRigaDoc As Integer, _
    ByVal lCodlead As Integer, ByVal lCodoppo As Integer, ByVal lCodchia As Integer, ByVal lNumcontr As Integer, _
    ByVal strTipoOgg As String, ByVal strQuery As String, ByVal strPrgParent As String, ByVal bAddConto As Boolean, _
    ByVal lConto As Integer, ByVal bAddLeads As Boolean, ByVal strLeads As String) As Boolean
    '---------------------------------------------------------------------------------------------------------------
    '--- strQuery <> "" solo se strTipoOgg = "V" ovvero se chiamato da BN__ROLE
    '---------------------------------------------------------------------------------------------------------------
    Dim strSQL As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strditta, dsOut, lProgr, strCod, lcod, strMatr, strTipodoc, strSerieDoc, _
                                             nAnnoDoc, lNumdoc, dRigaDoc, lCodlead, lCodoppo, lCodchia, lNumcontr, _
                                             strTipoOgg, strQuery, strPrgParent, bAddConto, lConto, _
                                             bAddLeads, strLeads})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsOut = CType(oIn(1), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT allole.*, allole.ao_nomedoc AS xx_nomedoc, anagra.an_descr1 AS xx_descr1," & _
        " artico.ar_descr AS xx_descr, tabcovg.tb_descovg AS xx_descovg, commess.co_descr1 AS xx_descr1_commess," & _
        " leads.le_descr1 AS xx_descr1_lead, opportun.op_oggetto AS xx_oggetto," & _
        " nnchiam.op_oggetto AS xx_oggetto_nnchiam, analotti.alo_lottox as xx_lottox, testoff.td_oggetto as xx_oggetto_off " & _
        " FROM allole LEFT JOIN anagra ON allole.codditt = anagra.codditt AND allole.ao_codice = anagra.an_conto" & _
        " LEFT JOIN artico ON allole.codditt = artico.codditt AND allole.ao_strcod = artico.ar_codart" & _
        " LEFT JOIN commess ON allole.codditt = commess.codditt AND allole.ao_commeca = commess.co_comme" & _
        " LEFT JOIN tabcovg ON allole.ao_controp = tabcovg.tb_codcovg" & _
        " LEFT JOIN leads ON allole.codditt = leads.codditt AND allole.ao_codlead = leads.le_codlead" & _
        " LEFT JOIN opportun ON allole.codditt = opportun.codditt AND allole.ao_codoppo = opportun.op_codoppo" & _
        " LEFT JOIN nnchiam ON allole.codditt = nnchiam.codditt AND allole.ao_codchia = nnchiam.op_codchia" & _
        " LEFT JOIN analotti ON allole.codditt = analotti.codditt AND allole.ao_lotto = analotti.alo_lotto AND allole.ao_strcod = analotti.alo_codart" & _
        " LEFT JOIN testoff ON allole.codditt = testoff.codditt AND allole.ao_tipo = testoff.td_tipork AND allole.ao_annodoc = testoff.td_anno " & _
        " AND allole.ao_seriedoc = testoff.td_serie AND allole.ao_numdoc = testoff.td_numord AND allole.ao_rigadoc = testoff.td_vers " & _
        " WHERE allole.codditt = " & CStrSQL(strditta)
      If lProgr <> 0 Then
        strSQL += " AND ao_progress = " & lProgr
      Else
        If strTipoOgg <> "" Then
          Select Case strTipoOgg
            Case "A", "D" : strSQL += " AND ao_strcod = " & CStrSQL(strCod)
            Case "C", "F" : strSQL += " AND ao_codice = " & lcod
            Case "K" : strSQL += " AND ao_commeca = " & lcod
            Case "L"
              strSQL += " AND ao_lotto = " & lcod & _
                " AND ao_strcod = " & CStrSQL(strCod)
            Case "N"
              strSQL += " AND ao_strcod = " & CStrSQL(strCod) & _
                " AND ao_matric = " & CStrSQL(strMatr)
            Case "M", "O"
              If (dRigaDoc > 0) Then
                'Estraggo solo gli oggetti OLE associati alla riga indicata
                strSQL += " AND ao_tipodoc = " & CStrSQL(strTipodoc) & _
                  " AND ao_annodoc = " & nAnnoDoc & _
                  " AND ao_seriedoc = " & CStrSQL(strSerieDoc) & _
                  " AND ao_numdoc = " & lNumdoc & _
                  " AND ao_rigadoc = " & dRigaDoc
              Else
                'Estraggo tutti gli oggetti OLE associati al doc indicato
                strSQL += " AND ao_tipodoc = " & CStrSQL(strTipodoc) & _
                  " AND ao_annodoc = " & nAnnoDoc & _
                  " AND ao_seriedoc = " & CStrSQL(strSerieDoc) & _
                  " AND ao_numdoc = " & lNumdoc
              End If
            Case "P"
              strSQL += " AND ao_codice = " & lcod & _
                " AND ao_annodoc = " & nAnnoDoc & _
                " AND ao_seriedoc = " & CStrSQL(strSerieDoc) & _
                " AND ao_numdoc = " & lNumdoc
            Case "V"
              If strPrgParent <> "BN__ROLE" Then strSQL += " AND ao_progress = " & lcod & " " 'se non chiamato da __ROLE
            Case "!"
              strSQL += " AND ao_tipodoc = '!'" & _
                " AND ao_annodoc = " & nAnnoDoc & _
                " AND ao_seriedoc = " & CStrSQL(strSerieDoc) & _
                " AND ao_numdoc = " & lNumdoc & _
                " AND ao_rigadoc = " & dRigaDoc
            Case "R" : strSQL += " AND ao_codlead = " & lCodlead
            Case "J"
              strSQL += " AND ao_codoppo = " & lCodoppo & _
                " AND ao_codlead = " & lCodlead & _
                " AND ao_codice = " & lcod & " "
            Case "Y"
              strSQL += " AND ao_codchia = " & lCodchia & _
                " AND ao_codlead = " & lCodlead & _
                " AND ao_codice = " & lcod
            Case "X"
              strSQL += " AND ao_numcontr = " & lNumcontr & _
                " AND ao_codice = " & lcod
          End Select
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (strTipoOgg = "R") And (lCodlead <> 0) And (bAddConto = True) And (lConto <> 0) Then
        '------------------------------------------------------------------------------------------------------------
        '--- Se è stato passato il lead, il chiamante è 'R' (ovvero bscrglea) e c'è il crm o il cs 
        '--- devo aggiungere i record relativi al conto associato al lead
        '--- lo scopo è quello di poter vedere da glea sia i record del lead che quelli del conto associato
        '------------------------------------------------------------------------------------------------------------
        strSQL += " OR (ao_codice = " & lConto & " AND ao_codlead = 0) "
        '------------------------------------------------------------------------------------------------------------
        '--- in base all'opzione di registro OPZIONIUT, devo rimuovere le righe da non far vedere
        '------------------------------------------------------------------------------------------------------------
        Select Case NTSCInt(GetSettingBus("BS--AOLE", "OPZIONIUT", ".", "FiltroRecord", "0", " ", "0"))
          Case 0  'tutto (come 4 + cgprin)
            strSQL += ""
          Case 1  'solo record di crm (lead, attività (= lead), opportunità, offerte) + conto
            strSQL += " AND ao_tipo IN ('R', 'J', '!', 'C') "
          Case 2  'come 1 + ordini
            strSQL += " AND ao_tipo IN ('R', 'J', '!', 'C', 'O') "
          Case 3  'come 2 + veboll
            strSQL += " AND ao_tipo IN ('R', 'J', '!', 'C', 'O', 'M') "
          Case 4  'come 3 + customer service csgchi e csgser, csgsco
            strSQL += " AND ao_tipo IN ('R', 'J', '!', 'C', 'O', 'M', 'Y', 'X') "
        End Select
      ElseIf strTipoOgg = "C" And lcod <> 0 And bAddLeads And strLeads.Trim <> "" Then
        '------------------------------------------------------------------------------------------------------------
        '--- se è stato passato il conto il chiamante è 'C' (ovvero bs--clie) e c'è il crm o il cs 
        '--- devo aggiungere i record relativi ai leads associati al conto (senza distinguere per destinazione diversa)
        '--- lo scopo è quello di poter vedere da bs--clie tutti i record collegati al conto e al lead
        '------------------------------------------------------------------------------------------------------------
        strSQL += " OR (ao_codlead in (" & strLeads & ") AND ao_codice = 0) "
      End If
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery, strSQL)
      '--------------------------------------------------------------------------------------------------------------
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ALLOLE")
      '--------------------------------------------------------------------------------------------------------------
      dsOut.Tables("ALLOLE").Columns.Remove("ao_ole")
      dsOut.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetProgr(ByVal strditta As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      GetProgr = 1
      strSQL = "SELECT top 1 ao_progress FROM allole WHERE codditt = " & CStrSQL(strditta) & " ORDER BY ao_progress DESC "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then GetProgr = NTSCInt(dttTmp.Rows(0)!ao_progress) + 1
      dttTmp.Clear()
      Return GetProgr

    Catch ex As Exception
      'Non eseguo la gestione errori standard, ma giro l'errore all'entity chiamante
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CercaAccessiDaDocumenti(ByVal strDitta As String, ByVal strTipork As String, _
      ByVal nAnno As Integer, ByVal strSerie As String, ByVal nNumdoc As Integer, _
      ByVal nCodcageAccdito As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tm_tipork" & _
        " FROM (testmag INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto)" & _
        " INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
        " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
        " AND tm_tipork = " & CStrSQL(strTipork) & _
        " AND tm_anno = " & nAnno & _
        " AND tm_serie = " & CStrSQL(strSerie) & _
        " AND tm_numdoc = " & nNumdoc & _
        " AND le_codlead IN " & SubQueryFiltroLeadMod(strDitta)
      If nCodcageAccdito <> 0 Then
        strSQL += " AND (tm_codagen = " & nCodcageAccdito & " OR tm_codagen2 = " & nCodcageAccdito & ")"
      End If
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function CercaAccessiDaOrdini(ByVal strDitta As String, ByVal strTipork As String, _
    ByVal nAnno As Integer, ByVal strSerie As String, ByVal nNumord As Integer, _
    ByVal nCodcageAccdito As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT td_tipork" & _
        " FROM (testord INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto)" & _
        " INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
        " WHERE testord.codditt = " & CStrSQL(strDitta) & _
        " AND td_tipork = " & CStrSQL(strTipork) & _
        " AND td_anno = " & nAnno & _
        " AND td_serie = " & CStrSQL(strSerie) & _
        " AND td_numord = " & nNumord & _
        " AND le_codlead IN " & SubQueryFiltroLeadMod(strDitta)
      If nCodcageAccdito <> 0 Then
        strSQL += " AND (td_codagen = " & nCodcageAccdito & " OR td_codagen2 = " & nCodcageAccdito & ")"
      End If
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function CercaLeadDaContoRiga(ByVal strDitta As String, ByVal nConto As Integer) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      CercaLeadDaContoRiga = 0
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 le_codlead FROM leads" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND le_conto = " & nConto & _
        " AND le_coddest = 0" & _
        " ORDER BY codditt, le_codlead"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then CercaLeadDaContoRiga = NTSCInt(dttTmp.Rows(0)!le_codlead)
      '--------------------------------------------------------------------------------------------------------------
      Return CercaLeadDaContoRiga
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function RitornaAgenteDaAccdito(ByVal strDitta As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      RitornaAgenteDaAccdito = 0
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT opdi_codcage FROM accdito" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND opdi_opnome = " & CStrSQL(oApp.User.Nome)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then RitornaAgenteDaAccdito = NTSCInt(dttTmp.Rows(0)!opdi_codcage)
      '--------------------------------------------------------------------------------------------------------------
      Return RitornaAgenteDaAccdito
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function CercaContoDaLead(ByVal strDitta As String, ByVal lLead As Integer) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      CercaContoDaLead = 0
      strSQL = "SELECT le_conto FROM leads" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND le_codlead = " & lLead

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then CercaContoDaLead = NTSCInt(dttTmp.Rows(0)!le_conto)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CercaLeadsDaConto(ByVal strDitta As String, ByVal nConto As Integer) As String
    'ritorna tutti i leads collegati al cliente/fornitore (indipendentemente dalla destinazione diversa del cliente)
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try
      '--------------------------------------------------------------------------------------------------------------
      CercaLeadsDaConto = ""
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT le_codlead FROM leads" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND le_conto = " & nConto & _
        " ORDER BY codditt, le_codlead"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For i = 0 To dttTmp.Rows.Count - 1
        CercaLeadsDaConto += dttTmp.Rows(i)!le_codlead.ToString & ", "
      Next
      If CercaLeadsDaConto <> "" Then
        CercaLeadsDaConto = CercaLeadsDaConto.Substring(0, CercaLeadsDaConto.Length - 2)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
End Class
