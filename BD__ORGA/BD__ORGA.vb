Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Public Class CLD__ORGA
  Inherits CLD__BASE


  Public Overridable Function CreaOperatore(ByVal dtrOrga As DataRow, ByVal strNomeOperatore As String, ByVal strPassword As String) As Boolean
    Dim strSQL As String = ""
    Try
      Dim lGruppo As Integer = NTSCInt(GetSettingBus("BS--ORGA", "OPZIONI", ".", "GruppoUtenteGuest", "0", ".", "0"))
      Dim strRuolo As String = " "

      While True
        strSQL = "SELECT ru_codruol FROM ruoli WHERE ru_gruppo = " & lGruppo

        Dim dttTmp As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
        If dttTmp.Rows.Count > 0 Then
          strRuolo = NTSCStr(dttTmp.Rows(0)!ru_codruol)
          Exit While
        Else
          lGruppo = 0
        End If
      End While

      strSQL = "INSERT INTO operat (OpNome, OpPasswd, OpIscrmus, OpDescont, OpDescont2, OpNetOnly, OpSumail, OpSutipouser, OpGruppo, OpRuolo) " & _
               " VALUES (" & CStrSQL(strNomeOperatore) & ", " & CStrSQL(strPassword) & ", 'N', " & CStrSQL(dtrOrga!og_descont) & ", " & _
               CStrSQL(dtrOrga!og_descont2) & ", 'S', " & CStrSQL(dtrOrga!og_email) & ", 'G', " & lGruppo & ", " & CStrSQL(strRuolo) & ")"

      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CaricaOrganizzazioni(ByVal strDitta As String, ByVal bIsCrmUser As Boolean, ByVal bInterna As Boolean, _
                                                   ByVal bClienti As Boolean, ByVal bFornitori As Boolean, ByVal bLeads As Boolean) As DataSet
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DISTINCT organig.*, tb_desruaz as xx_codruaz, tb_descont as xx_codcont, tb_descope as xx_codcope, " & _
               "       CASE WHEN og_conto = 0 THEN ul_nomdest ELSE dd_nomdest END AS xx_coddest, an_descr1 as xx_conto, le_descr1 as xx_lead, " & _
               "       tb_descage as xx_codcage, tb_desstat as xx_stato, an_ultagg AS xx_anultagg, le_ultagg AS xx_leultagg, " & _
               "       tb_desstco AS xx_codstco, (co_descont + ' ' + co_descont2) AS xx_referente " & _
               " FROM organig " & _
               "  LEFT JOIN tabruaz ON organig.og_codruaz = tabruaz.tb_codruaz " & _
               "  LEFT JOIN tabcont ON organig.codditt = tabcont.codditt AND organig.og_codcont = tabcont.tb_codcont " & _
               "  LEFT JOIN anazul ON organig.og_coddest = anazul.ul_coddest AND organig.codditt = anazul.codditt " & _
               "  LEFT JOIN destdiv ON destdiv.codditt = organig.codditt AND dd_conto = og_conto AND dd_coddest = og_coddest " & _
               "  LEFT JOIN anagra ON anagra.codditt = organig.codditt AND an_conto = og_conto " & _
               "  LEFT JOIN leads ON leads.codditt = organig.codditt AND le_codlead = og_codlead " & _
               "  LEFT JOIN tabcage ON tabcage.codditt = organig.codditt AND tabcage.tb_codcage = og_codcage " & _
               "  LEFT JOIN tabcope ON organig.codditt = tabcope.codditt AND organig.og_codcope = tabcope.tb_codcope " & _
               "  LEFT JOIN tabstat ON organig.og_stato = tabstat.tb_codstat " & _
               "  LEFT JOIN tabstco ON og_codstco = tb_codstco " & _
               "  LEFT JOIN contatti ON og_referente = co_progr "

      If bIsCrmUser Then
        'Se è un utente CRM può vedere solo i lead sui quali ha i permessi
        strSQL &= " LEFT JOIN acclead ON acclead.codditt = leads.codditt AND opcr_codlead = le_codlead " & _
                  " LEFT JOIN acccrm ON acccrm.codditt = acclead.codditt AND acccrm.opcr_alopnome = acclead.opcr_opnome "
      End If

      strSQL &= " WHERE organig.codditt = " & CStrSQL(strDitta)

      'Non mostro le organizzazioni interne
      If Not bInterna Then strSQL &= " AND (og_conto <> 0 OR og_codlead <> 0)"
      If Not bClienti And bLeads Then 'Non mostro i clienti
        strSQL &= " AND (ISNULL(an_tipo, 'X') <> 'C' OR og_codlead <> 0)"
      ElseIf Not bLeads And bClienti Then 'Non mostro i leads (ma posso vedere i clienti collegati ad un lead)
        strSQL &= " AND (og_codlead = 0 OR (og_conto <> 0 AND og_codlead <> 0))"
      ElseIf Not bLeads And Not bClienti Then 'Non mostro ne i clienti ne i leads
        strSQL &= " AND (ISNULL(an_tipo, 'X') <> 'C' AND og_codlead = 0)"
      End If
      'Non mostro i fornitori
      If Not bFornitori Then strSQL &= " AND ISNULL(an_tipo, 'X') <> 'F'"


      If bIsCrmUser Then
        'Se è un utente CRM può vedere solo i lead sui quali ha i permessi o le organizzazioni interne
        strSQL &= " AND (" & CStrSQL(oApp.User.Nome) & " IN (acccrm.opcr_opnome, acclead.opcr_opnome)" & _
                  "  OR (og_conto = 0 AND og_codlead = 0)" & _
                  "  OR ISNULL(an_tipo, 'X') = 'F')"
      End If

      strSQL &= " ORDER BY og_descont, og_descont2, og_progr"

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORGANIG")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return Nothing
  End Function


  Public Overridable Function OrgaSalva(ByVal dsOrga As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      'Aggiunge le nuove righe
      For Each dtrOrga As DataRow In dsOrga.Tables("ORGANIG").Select("", "", DataViewRowState.Added)
        strSQL = "INSERT INTO organig " & GetQueryInsertField(dsOrga.Tables("ORGANIG"), "og_") & _
                 " VALUES " & GetQueryInsertValue(dsOrga.Tables("ORGANIG"), dtrOrga, "og_")

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        If Not AggiornaUltAgg(NTSCStr(dtrOrga!codditt), NTSCInt(dtrOrga!og_conto), NTSCInt(dtrOrga!og_codlead), dbConn) Then Return False
      Next

      'Modifica le righe esistenti
      For Each dtrOrga As DataRow In dsOrga.Tables("ORGANIG").Select("", "", DataViewRowState.ModifiedCurrent)
        strSQL = "UPDATE organig SET " & GetQueryUpdate(dsOrga.Tables("ORGANIG"), dtrOrga, "og_") & _
                 " WHERE codditt = " & CStrSQL(dtrOrga!codditt) & _
                 "   AND og_progr = " & NTSCInt(dtrOrga!og_progr)

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        If Not AggiornaUltAgg(NTSCStr(dtrOrga!codditt), NTSCInt(dtrOrga!og_conto), NTSCInt(dtrOrga!og_codlead), dbConn) Then Return False
      Next

      'Cancella le righe eliminate
      For Each dtrOrga As DataRow In dsOrga.Tables("ORGANIG").Select("", "", DataViewRowState.Deleted)
        strSQL = "DELETE FROM organig " & _
                 " WHERE codditt = " & CStrSQL(dtrOrga("codditt", DataRowVersion.Original)) & _
                 "   AND og_progr = " & NTSCInt(dtrOrga("og_progr", DataRowVersion.Original))

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        If Not AggiornaUltAgg(NTSCStr(dtrOrga("codditt", DataRowVersion.Original)), NTSCInt(dtrOrga("og_conto", DataRowVersion.Original)), _
                              NTSCInt(dtrOrga("og_codlead", DataRowVersion.Original)), dbConn) Then Return False
      Next

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
    End Try
  End Function

  Public Overridable Function AggiornaUltAgg(ByVal strDitta As String, ByVal lConto As Integer, ByVal lLead As Integer, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      'Aggiorna la data ultimo aggiornamento sul conto\lead associato, per evitare ulteriori modifiche da parte di altri operatori.
      If lConto <> 0 Then 'Cliente\fornitore
        strSQL = "UPDATE anagra SET an_ultagg = " & CDataSQL(Now) & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 "   AND an_conto = " & lConto
      ElseIf lLead <> 0 Then ' Lead
        strSQL = "UPDATE leads SET le_ultagg = " & CDataSQL(Now) & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 "   AND le_codlead = " & lLead
      Else 'Organizzazione interna
        Return True
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function LeadCollegatoAConto(ByVal strDitta As String, ByVal lConto As Integer) As Integer
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT le_codlead FROM leads " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND le_conto = " & lConto & _
               "   AND le_coddest = 0"

      Dim dttTmp As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!le_codlead)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return 0
  End Function
  Public Overridable Function TrovaLeadDaContoDestinazione(ByVal strDitta As String, ByVal lConto As Integer, ByVal lCoddest As Integer) As Integer
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT le_codlead FROM leads " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND le_conto = " & lConto & _
               "   AND le_coddest = " & lCoddest

      Dim dttTmp As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!le_codlead)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return 0
  End Function

  Public Overridable Function EliminaAttivitàDaOperatoreOrganig(ByVal strDitta As String, ByVal strOperat As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If strOperat.Trim = "" Then Return True
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM cractopp" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND UPPER(cap_opcrmincpr) = " & CStrSQL(strOperat.ToUpper)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE cract FROM cract LEFT JOIN cractopp ON cract.codditt = cractopp.codditt" & _
                                                  " AND cract.ca_codcrac = cractopp.cap_codcrac" & _
        " WHERE cract.codditt = " & CStrSQL(strDitta) & _
        " AND cap_codcrac IS NULL"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

End Class
