Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLD__ANAZ
  Inherits CLD__BASE

  Private bIsToSyncAnagcaTabs As Boolean = False
  Public Property IsToSyncAnagcaTabs() As Boolean
    Get
      Return bIsToSyncAnagcaTabs
    End Get
    Set(ByVal value As Boolean)
      bIsToSyncAnagcaTabs = value
    End Set
  End Property


  Public Overridable Function GetData(ByVal strDitta As String, ByVal bGestTabCont As Boolean, ByRef dsAnaz As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dttInsg As New DataTable
    Dim i As Integer = 0
    Dim dttRelease As New DataTable

    Try
      strSQL = "SELECT tabanaz.*, tabstat.tb_desstat as xx_azstato, " & _
                "comuni.co_denom as xx_azcodcomu, comuninasc.co_denom as xx_azcodcomn, " & _
                "tabstatnasc.tb_desstat as xx_azstanasc, tabinst.tb_nflogo as xx_nflogo, " & _
                "tabstatnaz1.tb_desstat as xx_aznazion1, tabstatnaz2.tb_desstat as xx_aznazion2, " & _
                "tabesco1.tb_desesco as xx_escomp, tabesco2.tb_desesco as xx_escompp, " & _
                "tb_despcon as xx_azcodpcon, 'xx' as xx_azcodabi, 'xx' as xx_azcodcab, " & _
                "'xx' as xx_natura, 'xx' as xx_azcodrtac, 'xx' as xx_azcodstud, " & _
                "'xx' as xx_azcodgrua, 'xx' as xx_mascli_1, 'xx' as xx_masfor_1, " & _
                "'xx' as xx_azcoddpr, tb_destcdc AS xx_codtcdc, tb_desbanc as xx_azcodcaf " & _
                "FROM ((((((((((tabanaz INNER JOIN tabinst ON tabanaz.codditt = tabinst.codditt) " & _
                "LEFT JOIN tabstat ON tabanaz.tb_azstato = tabstat.tb_codstat) " & _
                "LEFT JOIN comuni ON tabanaz.tb_azcodcomu = comuni.co_codcomu) " & _
                "LEFT JOIN comuni AS comuninasc ON tabanaz.tb_azcodcomn = comuninasc.co_codcomu) " & _
                "LEFT JOIN tabstat AS tabstatnasc ON tabanaz.tb_azstanasc = tabstatnasc.tb_codstat) " & _
                "LEFT JOIN tabstat AS tabstatnaz1 ON tabanaz.tb_aznazion1 = tabstatnaz1.tb_codstat) " & _
                "LEFT JOIN tabstat AS tabstatnaz2 ON tabanaz.tb_aznazion2 = tabstatnaz2.tb_codstat) " & _
                "LEFT JOIN tabesco AS tabesco1 ON tabanaz.codditt = tabesco1.codditt AND tabanaz.tb_escomp = tabesco1.tb_codesco) " & _
                "LEFT JOIN tabesco AS tabesco2 ON tabanaz.codditt = tabesco2.codditt AND tabanaz.tb_escompp = tabesco2.tb_codesco) " & _
                "LEFT JOIN tabpcon ON tabanaz.tb_azcodpcon = tabpcon.tb_codpcon) " & _
                "LEFT JOIN tabtcdc ON tabanaz.tb_codtcdc = tabtcdc.tb_codtcdc " & _
                "LEFT JOIN tabbanc ON tabanaz.codditt = tabbanc.codditt AND tabanaz.tb_azcodcaf = tabbanc.tb_codbanc " & _
                "WHERE tabanaz.codditt = " & CStrSQL(strDitta)
      dsAnaz = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")

      '---------------------------------
      'leggo le tabelle aggiuntive
      strSQL = "SELECT anaziva.*, tb_desciva AS xx_codivapr, tb_azrags1 AS xx_codditcg " & _
              " FROM (anaziva LEFT JOIN tabciva ON anaziva.ai_codivapr = tabciva.tb_codciva) " & _
              " LEFT JOIN tabanaz ON anaziva.ai_codditcg = tabanaz.codditt " & _
              " WHERE anaziva.codditt = " & CStrSQL(strDitta)                   'dati iva per anno
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAZIVA", dsAnaz)

      strSQL = "SELECT tabatti.*, tb_desatec AS xx_acodista, tb_desusat AS xx_codusatdef " & _
               " FROM (tabatti LEFT JOIN tabatec ON tabatti.tb_acodista = tabatec.tb_codatec) " & _
               " LEFT JOIN tabusat ON tabatti.codditt = tabusat.codditt AND tabatti.tb_codusatdef = tabusat.tb_codusat" & _
               " WHERE tabatti.codditt = " & CStrSQL(strDitta)                  'attività iva
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABATTI", dsAnaz)

      strSQL = "SELECT tabduri.*, tb_desatti AS xx_ucodatti, ul_nomdest AS xx_ucoddest " & _
               " FROM (tabduri LEFT JOIN anazul ON tabduri.codditt = anazul.codditt AND tabduri.tb_ucoddest = anazul.ul_coddest) " & _
               " LEFT JOIN tabatti ON tabduri.codditt = tabatti.codditt AND tabduri.tb_anno = tabatti.tb_anno AND tabduri.tb_ucodatti = tabatti.tb_codatti " & _
               " WHERE tabduri.codditt = " & CStrSQL(strDitta)     'dati ultimi reg. iva
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABDURI", dsAnaz)

      'occhio a bGestTabCont
      strSQL = "SELECT ORGANIG.*, tabruaz.tb_desruaz as xx_codruaz, tabcont.tb_descont as xx_codcont, tb_descope as xx_codcope, " & _
               "       anazul.ul_nomdest as xx_coddest, '' as xx_conto, tb_desstat as xx_stato, tb_descage AS xx_codcage " & _
               " FROM organig " & _
               "  LEFT JOIN tabruaz ON organig.og_codruaz = tabruaz.tb_codruaz " & _
               "  LEFT JOIN tabcont ON organig.codditt = tabcont.codditt AND organig.og_codcont = tabcont.tb_codcont " & _
               "  LEFT JOIN anazul ON organig.og_coddest = anazul.ul_coddest AND organig.codditt = anazul.codditt " & _
               "  LEFT JOIN tabcope ON organig.codditt = tabcope.codditt AND organig.og_codcope = tabcope.tb_codcope " & _
               "  LEFT JOIN tabstat ON organig.og_stato = tabstat.tb_codstat" & _
               "  LEFT JOIN tabcage ON organig.codditt = tabcage.codditt AND organig.og_codcage = tabcage.tb_codcage " & _
               " WHERE organig.codditt = " & CStrSQL(strDitta) & _
               "   AND og_tipork <> 'L' " & _
               "   AND og_conto = 0 " & _
               "   AND og_codlead = 0"
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORGANIG", dsAnaz)
      If bGestTabCont Then
        For Each dtrT As DataRow In dsAnaz.Tables("ORGANIG").Rows
          If NTSCStr(dtrT!xx_codcont).Trim <> "" Then dtrT!og_descont = NTSCStr(dtrT!xx_codcont)
        Next
      End If
      dsAnaz.Tables("ORGANIG").AcceptChanges()

      strSQL = "SELECT anazmod.*, 'xx' AS xx_desmodulo, 'S' AS xx_abinsg, 'S' AS xx_abchiave  FROM anazmod WHERE codditt = " & CStrSQL(strDitta)
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAZMOD", dsAnaz)           'moduli abilitati

      '--- Acccessi utente/ditta
      strSQL = "SELECT accdito.*, tb_descage As xx_codcage" & _
               " FROM accdito LEFT JOIN tabcage ON accdito.codditt = tabcage.codditt AND accdito.opdi_codcage = tabcage.tb_codcage" & _
               " WHERE accdito.codditt = " & CStrSQL(strDitta)

      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ACCDITO", dsAnaz)
      dttRelease.Clear()
      dttRelease.Dispose()

      strSQL = "SELECT accditt.*, 'xx' as xx_nomprog, 0 as xx_modulo FROM accditt " & _
               " WHERE codditt = " & CStrSQL(strDitta)     'accesso utente/ditta/programma
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ACCDITT", dsAnaz)

      strSQL = "SELECT accditm.*, 'xx' as xx_modulo FROM accditm WHERE codditt = " & CStrSQL(strDitta)     'accesso utente/ditta/modulo
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ACCDITM", dsAnaz)

      strSQL = "SELECT anaditac.*, tb_desciva AS xx_cdtivapri " & _
               " FROM anaditac LEFT JOIN tabciva ON anaditac.ac_cdtivapri = tabciva.tb_codciva " & _
               " WHERE anaditac.codditt = " & CStrSQL(strDitta)                 'dati aggiuntivi contabilità
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANADITAC", dsAnaz)

      strSQL = "SELECT anaditacs.*, tb_desspce as xx_gruspedi, tb_despuce as xx_codpuman " & _
               " FROM (anaditacs LEFT JOIN tabpuce ON anaditacs.acs_codpuman = tabpuce.tb_codpuce) " & _
               " LEFT JOIN tabspce ON anaditacs.acs_gruspedi = tabspce.tb_codspce " & _
               " WHERE anaditacs.codditt = " & CStrSQL(strDitta)                'dati aggiuntivi cespiti
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANADITACS", dsAnaz)

      strSQL = "SELECT tabesco.*, t1.tb_desesco AS xx_numestr, tb_desdfor AS xx_forfctre, tb_dessiso AS xx_cdtsitso " & _
               " FROM ((tabesco LEFT JOIN tabesco as t1 ON tabesco.codditt = t1.codditt AND tabesco.tb_numestr = t1.tb_codesco) " & _
               " LEFT JOIN tabsiso ON tabesco.tb_cdtsitso = tabsiso.tb_codsiso) " & _
               " LEFT JOIN tabdfor ON tabesco.tb_forfctre = tabdfor.tb_coddfor " & _
               " WHERE tabesco.codditt = " & CStrSQL(strDitta)
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABESCO", dsAnaz)

      strSQL = "SELECT * FROM anaditva WHERE codditt = " & CStrSQL(strDitta)    'variaz campi capitale/natura/
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANADITVA", dsAnaz)

      strSQL = "SELECT anaditpa.*, tb_destpbf AS xx_codtpbf, tb_desrtac AS xx_codrtacp, " & _
              " t1.tb_desvpar AS xx_codvpar, t2.tb_desvpar AS xx_codvparsg, t3.tb_desvpar AS xx_codvparsa " & _
              " FROM ((((anaditpa LEFT JOIN tabtpbf ON anaditpa.codditt = tabtpbf.codditt AND anaditpa.acs_codtpbf = tabtpbf.tb_codtpbf) " & _
              " LEFT JOIN tabrtac ON anaditpa.acs_codrtacp = tabrtac.tb_codrtac) " & _
              " LEFT JOIN tabvpar as t1 ON anaditpa.codditt = t1.codditt AND anaditpa.acs_codvpar = t1.tb_codvpar) " & _
              " LEFT JOIN tabvpar as t2 ON anaditpa.codditt = t2.codditt AND anaditpa.acs_codvparsg = t2.tb_codvpar) " & _
              " LEFT JOIN tabvpar as t3 ON anaditpa.codditt = t3.codditt AND anaditpa.acs_codvparsa = t3.tb_codvpar " & _
              " WHERE anaditpa.codditt = " & CStrSQL(strDitta)    'dati aggiuntivi parcellazione
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANADITPA", dsAnaz)

      '--- vecchi programmi fatti da genesys
      strSQL = "SELECT * FROM anaditace WHERE codditt = " & CStrSQL(strDitta)   'dati aggiuntivi contablitità/studi
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANADITACE", dsAnaz)

      strSQL = "SELECT anaditasd.*, ul_nomdest as xx_coddest, tb_desatec as xx_ascodatt " & _
              " FROM (anaditasd LEFT JOIN tabatec ON anaditasd.asd_ascodatt = tabatec.tb_codatec) " & _
              " LEFT JOIN anazul ON anaditasd.codditt = anazul.codditt AND anaditasd.asd_coddest = anazul.ul_coddest " & _
              " WHERE anaditasd.codditt = " & CStrSQL(strDitta)   'dettaglio unità produtt
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANADITASD", dsAnaz)

      strSQL = "SELECT anaditasm.*, ul_nomdest as xx_coddest, tb_desatec as xx_stucodat " & _
              " FROM (anaditasm LEFT JOIN tabatec ON anaditasm.asm_stucodat = tabatec.tb_codatec) " & _
              " LEFT JOIN anazul ON anaditasm.codditt = anazul.codditt AND anaditasm.asm_coddest = anazul.ul_coddest " & _
              " WHERE anaditasm.codditt = " & CStrSQL(strDitta)   'dettaglio righe quadro M
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANADITASM", dsAnaz)
      '---

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataAnazul(ByVal strDitta As String, ByRef dsAnaz As DataSet) As Boolean
    Dim strSQL As String = ""
    Try

      If dsAnaz.Tables.Contains("ANAZUL") Then
        dsAnaz.Tables("ANAZUL").Clear()
        dsAnaz.Tables.Remove("ANAZUL")
      End If

      strSQL = "SELECT anazul.*, co_denom as xx_codcomu, tb_desstat as xx_stato " & _
               " FROM (anazul LEFT JOIN comuni ON anazul.ul_codcomu = comuni.co_codcomu) " & _
               " LEFT JOIN tabstat ON anazul.ul_stato = tabstat.tb_codstat " & _
               " WHERE anazul.codditt = " & CStrSQL(strDitta)
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAZUL", dsAnaz)

      dsAnaz.AcceptChanges()

      Return True

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

  Public Overridable Function CheckDitteAnagen(ByVal lCodanag As Integer) As Boolean
    'ritorna true se esistono ditte collegate all'anagrafica generale passatami in input
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT Count(*) as Records FROM tabanaz WHERE tb_azcodanag = " & lCodanag.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DittaPresenteCfPi(ByVal strDitta As String, ByVal strPi As String, ByVal strCF As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim bOut As Boolean = False
    Try
      If strPi <> "" Then strCF = ""
      strSQL = "SELECT top 1 codditt FROM tabanaz WHERE codditt <> " & CStrSQL(strDitta)
      If strPi <> "" Then strSQL += " AND tb_azpiva = " & CStrSQL(strPi)
      If strCF <> "" Then strSQL += " AND tb_azcodf = " & CStrSQL(strCF)
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

  Public Overridable Function CheckCoerenzaGestTabcont(ByVal strDitta As String, ByVal bGesttabcont As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try

      If bGesttabcont = True Then
        strSQL = "SELECT top 1 og_progr FROM organig" & _
          " WHERE organig.codditt = " & CStrSQL(strDitta) & _
          " AND og_codcont = 0"
      Else
        strSQL = "SELECT top 1 og_progr FROM organig" & _
          " WHERE organig.codditt = " & CStrSQL(strDitta) & _
          " AND og_codcont <> 0"
      End If
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetUltaggTabanaz(ByVal strDitta As String) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strOut As String = NTSCDate("01/01/1900").ToString
    Try

      strSQL = " SELECT tb_azultagg FROM tabanaz WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then strOut = NTSCDate(dttTmp.Rows(0)!tb_azultagg).ToString
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

  Public Overridable Function Salva(ByRef ds As DataSet, ByVal bNew As Boolean, ByVal bModuloAnagen As Boolean, _
    ByVal bScriviActlog As Boolean) As Boolean
    Try
      '-------------------------------------------------------------------------------------------------------------
      Return Salva(ds, bNew, bModuloAnagen, bScriviActlog, False)
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Salva(ByRef ds As DataSet, ByVal bNew As Boolean, ByVal bModuloAnagen As Boolean, _
                                    ByVal bScriviActlog As Boolean, ByVal bCreaCAREVIS As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, bNew, bModuloAnagen, bScriviActlog, bCreaCAREVIS})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return Salva(ds, bNew, bModuloAnagen, bScriviActlog, bCreaCAREVIS, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function Salva(ByRef ds As DataSet, ByVal bNew As Boolean, ByVal bModuloAnagen As Boolean, _
                                    ByVal bScriviActlog As Boolean, ByVal bCreaCAREVIS As Boolean, ByVal bModuloCRM As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim strSQLInsert As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dtrA As DataRow = Nothing
    Dim strMsg As String = ""

    Dim strDataAgg As String = DateTime.Now.ToShortDateString
    Dim dttTmp As New DataTable
    Dim dtrChange() As DataRow = Nothing
    Dim i As Integer = 0
    Dim nRec As Integer = 0

    Dim strSqlWhere As String = ""
    Dim strDesogglog As String = ""
    Dim bDbMultiditta As Boolean = False

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, bNew, bModuloAnagen, bScriviActlog, bCreaCAREVIS, bModuloCRM})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        Return CBool(oOut)
      End If
      '----------------
      dtrA = ds.Tables("TABANAZ").Rows(0)
      bDbMultiditta = IsDbMultiDitta("")

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      If bNew Then
        '---------------------------------
        'inserisco tabanaz
        strSQL = "INSERT INTO tabanaz " & GetQueryInsertField(ds.Tables("TABANAZ"), "tb_") & " VALUES "
        strSQL += GetQueryInsertValue(ds.Tables("TABANAZ"), dtrA, "tb_")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '---------------------------------
        'Crea in ANAGRA tutti i records di tipo "S" da ANAGPC
        strSQL = "INSERT INTO anagra (codditt, an_conto, an_tipo, an_descr1, an_descr2," & _
                " an_indir, an_cap, an_citta, an_prov, an_codfis, an_pariva, an_controp, an_alleg," & _
                " an_persf, an_partite, an_telef, an_faxtlx, an_contatt, an_ultagg, an_zona," & _
                " an_categ, an_codese, an_codpag, an_scont1, an_scont2, an_agente, an_banc1," & _
                " an_banc2, an_abi, an_cab, an_rifriba, an_spinc, an_bolli, an_numdic, an_datdic," & _
                " an_listino, an_vuoti, an_valuta, an_claprov, an_clascon, an_note, an_porto," & _
                " an_vett, an_fatt, an_fido, an_destin, an_scaden, an_dtaper, an_dummy, an_stato," & _
                " an_agente2, an_kpccee2, an_kpccee, an_flci, an_unmis, an_accperi, an_codmast," & _
                " an_note2, an_blocco, an_gcons, an_email, an_website, an_rifricd, an_rifrica," & _
                " an_usaem, an_codling, an_rating, an_codbanc, an_agcontrop, an_privacy, an_codntra," & _
                " an_status, an_codcana, an_codtpbf, an_perfatt, an_contfatt, an_codnscol," & _
                " an_mesees1, an_mesees2, an_giofiss, an_destpag, an_vett2, an_numdicp, an_datdicp," & _
                " an_scaddic, an_maxdic, an_opnome, an_gescon, an_perescon, an_webuid, an_webpwd," & _
                " an_codanag, an_codpcon, an_pcconto, an_persfg, an_profes, an_condom, an_tpsogiva," & _
                " an_codcomu, an_destcorr, an_destsedel, an_destdomf, an_destresan," & _
                " an_siglaric, an_cognome, an_nome, an_codcomn, an_nazion1, an_nazion2, an_cell," & _
                " an_titolo, an_statofed, an_funzion, an_sesso, an_datnasc, an_citnasc, an_pronasc," & _
                " an_stanasc, an_codfisest, an_codrtac, an_tipacq, an_cksegno, an_conprof," & _
                " an_totcron, an_contrsemp, an_manrip, an_percman, an_colbil, an_voceirap," & _
                " an_varirap, an_pervari, an_cosvend, an_indiidd, an_azcom, an_ricmimp," & _
                " an_ricmpro, an_stseimp, an_stsepro, an_soggresi, an_omocodice, an_estcodiso," & _
                " an_estpariva, an_sosppr, an_iban, an_intragr, an_indirap, an_indiiddsit," & _
                " an_datini, an_datfin, an_opzgest, an_libstr1, an_libint1)"
        strSQL = strSQL & " SELECT " & CStrSQL(dtrA!codditt.ToString) & ", pc_conto, pc_tipo," & _
                " pc_descr1, pc_descr2, Null, Null, Null, Null, Null, Null, pc_controp, 'N', 'N'," & _
                " pc_partite, Null, Null, Null, " & CDataOraSQL(Now) & "," & _
                " 0, 0, 0, 0, 0, 0, 0, Null, Null, 0, 0, Null, 'N', 'N', Null, Null, 0, 'N', 0, 0, 0," & _
                " pc_note, Null, 0, 'R', 0, 0, pc_scaden, pc_dtaper, Null, Null, 0, pc_kpccee2," & _
                " pc_kpccee, pc_flci, Null, pc_accperi, pc_codmast, pc_note2, 'N', 8, Null, Null," & _
                " pc_rifricd, pc_rifrica, 'N', 0, Null, 0, 0, ' ', 0, 'A', 0, 0, ' ', 0, Null," & _
                " 0, 0, 0, 0, 0, Null, Null, Null, 0, " & CStrSQL(oApp.User.Nome) & ", 'N', 0," & _
                " Null, Null, 0, " & CStrSQL(dtrA!tb_azcodpcon) & ", pc_conto, 'F', 'N', 'N'," & _
                " 'N', Null, 0, 0, 0, 0, Null, Null, Null, Null, Null, Null, Null, Null, Null," & _
                " pc_funzion, 'M', Null, Null, Null, Null, Null, 0, pc_tipacq, pc_cksegno," & _
                " pc_conprof, pc_totcron, pc_contrsemp, pc_manrip, pc_percman, pc_colbil," & _
                " pc_voceirap, pc_varirap, pc_pervari, pc_cosvend, pc_indiidd, pc_azcom," & _
                " pc_ricmimp, pc_ricmpro, pc_stseimp, pc_stsepro, 'S', 'N', Null, Null, pc_sosppr," & _
                " Null, pc_intragr, pc_indirap, pc_indiiddsit, pc_datini, pc_datfin, pc_opzgest," & _
                " pc_libstr1, pc_libint1" & _
                " FROM anagpc" & _
                " WHERE pc_codpcon = " & CStrSQL(dtrA!tb_azcodpcon)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '---------------------------------
        'Copia i dati da TABNUMG in TABNUMA per quella ditta/sottoconto
        strSQL = "INSERT INTO tabnuma (codditt, tb_numtipo, tb_numserie, tb_numcodl," & _
                  " tb_numprog, tb_numprog2, tb_numdata, tb_numdata2, tb_numflag, tb_numflag2," & _
                  " tb_numval, tb_numval2)" & _
                  " SELECT " & CStrSQL(dtrA!codditt.ToString) & ", tb_numtipog, ' ', tb_numcodlg," & _
                  " tb_numprogg, 0, Null, Null, Null, Null, 0, 0" & _
                  " FROM tabnumg" & _
                  " WHERE tb_numtipog = 'SS'" & _
                  " AND tb_numserieg = " & CStrSQL(dtrA!tb_azcodpcon)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '---------------------------------
        'Se si è in aggiornamento di una nuova ditta
        'crea un record in TABINST prelevando i dati da TABINSG
        strSQL = "INSERT INTO tabinst (codditt, tb_codinst, tb_denom, tb_mod_1, tb_mod_2," & _
                " tb_mod_3, tb_mod_4, tb_mod_5, tb_mod_6, tb_mod_7, tb_mod_8, tb_mod_9, tb_mod_10," & _
                " tb_mod_11, tb_mod_12, tb_mod_13, tb_mod_14, tb_mod_15, tb_opz_1, tb_opz_2," & _
                " tb_opz_3, tb_opz_4, tb_opz_5, tb_opz_6, tb_opz_7, tb_opz_8, tb_opz_9, tb_opz_10," & _
                " tb_opz_11, tb_opz_12, tb_opz_13, tb_opz_14, tb_opz_15, tb_opz_16, tb_opz_17," & _
                " tb_opz_18, tb_opz_19, tb_opz_20, tb_opz_21, tb_opz_22, tb_opz_23, tb_opz_24," & _
                " tb_nfeffe, tb_nfcorr, tb_nfmenu, tb_nfboll, tb_nffata, tb_nffatd, tb_nfordi," & _
                " tb_nfrapp, tb_nfpcas, tb_nfofax, tb_mod2_1, tb_mod2_2, tb_mod2_3, tb_mod2_4," & _
                " tb_mod2_5, tb_mod2_6, tb_mod2_7, tb_mod2_8, tb_mod2_9, tb_mod2_10, tb_mod2_11," & _
                " tb_mod2_12, tb_mod2_13, tb_mod2_14, tb_mod2_15, tb_opz2_1, tb_opz2_2, tb_opz2_3," & _
                " tb_opz2_4, tb_opz2_5, tb_opz2_6, tb_opz2_7, tb_opz2_8, tb_opz2_9, tb_opz2_10," & _
                " tb_nffati, tb_nfimpe, tb_nfprev, tb_nfprb, tb_nfcorrv, tb_nfbollv, tb_nffatav," & _
                " tb_nffatdv, tb_nfordiv, tb_nffativ, tb_nfimpev, tb_nfprevv, tb_nfprbv, tb_nfcorric," & _
                " tb_nfbollic, tb_nffataic, tb_nffatdic, tb_nfordiic, tb_nffatiic, tb_nfimpeic," & _
                " tb_nfprevic, tb_nfprbic, tb_nflogo, tb_xlscee, tb_xlsric, tb_mod3_1, tb_mod3_2," & _
                " tb_mod3_3, tb_mod3_4, tb_mod3_5, tb_mod3_6, tb_mod3_7, tb_mod3_8, tb_mod3_9," & _
                " tb_mod3_10, tb_mod3_11, tb_mod3_12, tb_mod3_13, tb_mod3_14, tb_mod3_15, tb_mod4_1," & _
                " tb_mod4_2, tb_mod4_3, tb_mod4_4, tb_mod4_5, tb_mod4_6, tb_mod4_7, tb_mod4_8," & _
                " tb_mod4_9, tb_mod4_10, tb_mod4_11, tb_mod4_12, tb_mod4_13, tb_mod4_14, tb_mod4_15)"
        strSQL = strSQL & "SELECT " & CStrSQL(dtrA!codditt.ToString) & ", 1, " & _
                CStrSQL(Left(Trim(dtrA!tb_azrags1.ToString), 30)) & "," & _
                " tb_mod_1, tb_mod_2, tb_mod_3, tb_mod_4, tb_mod_5, tb_mod_6, tb_mod_7, tb_mod_8," & _
                " tb_mod_9, tb_mod_10, tb_mod_11, tb_mod_12, tb_mod_13, tb_mod_14, tb_mod_15," & _
                " ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '9', ' ', ' ', ' ', ' ', ' ', ' '," & _
                " ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '," & _
                " Null, Null, Null, Null, Null, Null, Null, Null, Null, Null," & _
                " tb_mod2_1, tb_mod2_2, tb_mod2_3, tb_mod2_4, tb_mod2_5, tb_mod2_6, tb_mod2_7," & _
                " tb_mod2_8, tb_mod2_9, tb_mod2_10, tb_mod2_11, tb_mod2_12, tb_mod2_13, tb_mod2_14," & _
                " tb_mod2_15," & _
                " ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '," & _
                " Null, Null, Null, Null, Null, Null, Null, Null, Null, Null, Null, Null, Null," & _
                " Null, Null, Null, Null, Null, Null, Null, Null, Null, " & CStrSQL(dtrA!xx_nflogo) & ", Null, Null," & _
                " tb_mod3_1, tb_mod3_2, tb_mod3_3, tb_mod3_4, tb_mod3_5, tb_mod3_6, tb_mod3_7," & _
                " tb_mod3_8, tb_mod3_9, tb_mod3_10, tb_mod3_11, tb_mod3_12, tb_mod3_13, tb_mod3_14," & _
                " tb_mod3_15," & _
                " tb_mod4_1, tb_mod4_2, tb_mod4_3, tb_mod4_4, tb_mod4_5, tb_mod4_6, tb_mod4_7," & _
                " tb_mod4_8, tb_mod4_9, tb_mod4_10, tb_mod4_11, tb_mod4_12, tb_mod4_13, tb_mod4_14," & _
                " tb_mod4_15" & _
                " FROM tabinsg" & _
                " WHERE tb_codinsg = 1"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '---------------------------------
        strSQL = "INSERT INTO anatric (codditt, ant_conto, ant_codtric, ant_kpcrd, ant_kpcra)" & _
                " SELECT " & CStrSQL(dtrA!codditt.ToString) & ", pct_conto, pct_codtric, pct_kpcrd," & _
                " pct_kpcra" & _
                " FROM anptric" & _
                " WHERE pct_codpcon = " & CStrSQL(dtrA!tb_azcodpcon)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Else
        '---------------------------------
        'scrivo il log
        If bScriviActlog Then
          strSqlWhere = CStrSQL(dtrA!codditt)
          strDesogglog = oApp.Tr(Me, 128865252281790133, "Modifica dati relativi all'anagrafica ditta '|" & NTSCStr(dtrA!codditt) & "|'")
          If Not ScriviActLog(NTSCStr(dtrA!codditt), "BS--ANAZ", "tabanaz", "tabanaz", strSqlWhere, "M", "D", strDesogglog, False) Then
            If IsInTrans Then AnnullaTrans()
            Return False
          End If
        End If

        '---------------------------------
        'aggiorno tabanaz
        strSQL = "UPDATE tabanaz SET " & GetQueryUpdate(ds.Tables("TABANAZ"), dtrA, "tb_") & _
                 " WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '---------------------------------
        'Aggiorna TABINST per il campo "Logo Azienda (bmp)"
        strSQL = "UPDATE tabinst" & _
                " SET tb_nflogo = " & CStrSQL(dtrA!xx_nflogo) & _
                " WHERE codditt = " & CStrSQL(dtrA!codditt.ToString) & _
                " AND tb_codinst = 1"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If    'If bNew Then

      If bIsToSyncAnagcaTabs Then
        '---------------------------------
        'Aggiunge in ANAGCA tutti i records di ANAGCA2 per 
        strSQL = "INSERT INTO anagca (codditt, ac_conto, ac_tipo, ac_descr1, ac_descr2, ac_controp, ac_ultagg," & _
                 " ac_valuta, ac_dtaper, ac_flci, ac_unmis, ac_accperi, ac_codmaca, ac_note, ac_richcena," & _
                 " ac_richcfam, ac_richcomm, ac_richdivi, ac_richstab, ac_codpcca, ac_iscontocoll, ac_richarti, ac_richcli)" & _
                 " SELECT " & CStrSQL(dtrA!codditt.ToString) & ", anagca2.ac_conto, anagca2.ac_tipo, anagca2.ac_descr1," & _
                 " anagca2.ac_descr2, anagca2.ac_controp," & _
                 " anagca2.ac_ultagg, anagca2.ac_valuta, anagca2.ac_dtaper, anagca2.ac_flci, anagca2.ac_unmis," & _
                 " anagca2.ac_accperi, anagca2.ac_codmac2, anagca2.ac_note, anagca2.ac_richcena, anagca2.ac_richcfam," & _
                 " anagca2.ac_richcomm, anagca2.ac_richdivi, anagca2.ac_richstab, anagca2.ac_codpcca, " & _
                 " anagca2.ac_iscontocoll, anagca2.ac_richarti, anagca2.ac_richcli" & _
                 " from anagca2 where ac_codpcca = " & CStrSQL(dtrA!tb_azcodpcca)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If


      '----------------------------------
      'Cancella gli eventuali dati nelle tabelle correlate
      strSQL = "DELETE FROM anazul WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM tabatti WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM tabduri WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM anaziva WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      EliminaRigheOrganig(ds.Tables("ORGANIG"), dtrA!codditt.ToString, bModuloCRM, dbConn) 'Per gestire il caso modulo CRM e eliminazione dell'ultima riga che genera riga con lead -1
      strSQL = "DELETE FROM anazmod WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM accdito WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM accditm WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM accditt WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM tabesco WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM anaditac WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM anaditacs WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM anaditpa WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM anaditace WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM anaditasd WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM anaditasm WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      strSQL = "DELETE FROM anaditva WHERE codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '----------------------------------
      'adesso inserisco
      strSQL = "INSERT INTO anazul " & GetQueryInsertField(ds.Tables("ANAZUL"), "ul_")
      For i = 0 To ds.Tables("ANAZUL").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ANAZUL"), ds.Tables("ANAZUL").Rows(i), "ul_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO anaziva " & GetQueryInsertField(ds.Tables("ANAZIVA"), "ai_")
      For i = 0 To ds.Tables("ANAZIVA").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ANAZIVA"), ds.Tables("ANAZIVA").Rows(i), "ai_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO tabatti " & GetQueryInsertField(ds.Tables("TABATTI"), "tb_")
      For i = 0 To ds.Tables("TABATTI").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("TABATTI"), ds.Tables("TABATTI").Rows(i), "tb_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO tabduri" & GetQueryInsertField(ds.Tables("TABDURI"), "tb_")
      For i = 0 To ds.Tables("TABDURI").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("TABDURI"), ds.Tables("TABDURI").Rows(i), "tb_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO organig " & GetQueryInsertField(ds.Tables("ORGANIG"), "og_")
      For i = 0 To ds.Tables("ORGANIG").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ORGANIG"), ds.Tables("ORGANIG").Rows(i), "og_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO anazmod " & GetQueryInsertField(ds.Tables("ANAZMOD"), "am_")
      For i = 0 To ds.Tables("ANAZMOD").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ANAZMOD"), ds.Tables("ANAZMOD").Rows(i), "am_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO accdito " & GetQueryInsertField(ds.Tables("ACCDITO"), "opdi_")
      For i = 0 To ds.Tables("ACCDITO").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ACCDITO"), ds.Tables("ACCDITO").Rows(i), "opdi_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO accditm " & GetQueryInsertField(ds.Tables("ACCDITM"), "opdi_")
      For i = 0 To ds.Tables("ACCDITM").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ACCDITM"), ds.Tables("ACCDITM").Rows(i), "opdi_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO accditt " & GetQueryInsertField(ds.Tables("ACCDITT"), "opdi_")
      For i = 0 To ds.Tables("ACCDITT").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ACCDITT"), ds.Tables("ACCDITT").Rows(i), "opdi_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO anaditac " & GetQueryInsertField(ds.Tables("ANADITAC"), "ac_")
      For i = 0 To ds.Tables("ANADITAC").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ANADITAC"), ds.Tables("ANADITAC").Rows(i), "ac_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO tabesco " & GetQueryInsertField(ds.Tables("TABESCO"), "tb_")
      For i = 0 To ds.Tables("TABESCO").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("TABESCO"), ds.Tables("TABESCO").Rows(i), "tb_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Se NON attivo il modulo supplementare "Budget e controllo di gestione", se non esiste, crea la riga
      '--- in tabella CAREVIS per ogni records di TABESCO
      '--------------------------------------------------------------------------------------------------------------
      If bCreaCAREVIS = True Then
        For i = 0 To (ds.Tables("TABESCO").Rows.Count - 1)
          strSQL = "INSERT INTO carevis (rev_codgrua, rev_escompg, rev_rev, rev_datatt, rev_datchiub," & _
            " rev_datchiuc, rev_status, rev_note)" & _
            " VALUES (1, " & NTSCInt(ds.Tables("TABESCO").Rows(i)!tb_codesco) & ", 0, " & _
            IntSetDate("01/01/1900") & ", NULL, NULL, 'A', NULL)"
          Try
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Catch ex As Exception
          End Try
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO anaditacs " & GetQueryInsertField(ds.Tables("ANADITACS"), "acs_")
      For i = 0 To ds.Tables("ANADITACS").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ANADITACS"), ds.Tables("ANADITACS").Rows(i), "acs_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO anaditpa " & GetQueryInsertField(ds.Tables("ANADITPA"), "acs_")
      For i = 0 To ds.Tables("ANADITPA").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ANADITPA"), ds.Tables("ANADITPA").Rows(i), "acs_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO anaditace " & GetQueryInsertField(ds.Tables("ANADITACE"), "ae_")
      For i = 0 To ds.Tables("ANADITACE").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ANADITACE"), ds.Tables("ANADITACE").Rows(i), "ae_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO anaditasd " & GetQueryInsertField(ds.Tables("ANADITASD"), "asd_")
      For i = 0 To ds.Tables("ANADITASD").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ANADITASD"), ds.Tables("ANADITASD").Rows(i), "asd_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "INSERT INTO anaditasm " & GetQueryInsertField(ds.Tables("ANADITASM"), "asm_")
      For i = 0 To ds.Tables("ANADITASM").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("ANADITASM"), ds.Tables("ANADITASM").Rows(i), "asm_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      'ANADITVA gestito in modo particolare (per le variazioni)
      If bNew = False Then
        strSQL = "INSERT INTO anaditva " & GetQueryInsertField(ds.Tables("ANADITVA"), "tb_")
        For i = 0 To ds.Tables("ANADITVA").Rows.Count - 1
          strSQLVal = GetQueryInsertValue(ds.Tables("ANADITVA"), ds.Tables("ANADITVA").Rows(i), "tb_")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
      Else
        strSQL = "INSERT INTO anaditva (codditt, tb_datini, tb_datfin, tb_tipovar, tb_vcapdelco," & _
                  " tb_vcapsotco, tb_vcapverco, tb_vcapvalaz, tb_vcapnumaz, tb_vcodattx, tb_vdescatt," & _
                  " tb_vnatura)" & _
                  " VALUES (" & CStrSQL(dtrA!codditt.ToString) & ", " & _
                  CDataSQL(NTSCDate(dtrA!tb_dtulvng)) & ", " & _
                  CDataSQL(IntSetDate("31/12/2099")) & "," & _
                  " 'N', 0, 0, 0, 0, 0, ' ', Null, " & NTSCInt(dtrA!tb_natura) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "INSERT INTO anaditva (codditt, tb_datini, tb_datfin, tb_tipovar, tb_vcapdelco," & _
                  " tb_vcapsotco, tb_vcapverco, tb_vcapvalaz, tb_vcapnumaz, tb_vcodattx, tb_vdescatt," & _
                  " tb_vnatura)" & _
                  " VALUES (" & CStrSQL(dtrA!codditt.ToString) & ", " & _
                  CDataSQL(NTSCDate(dtrA!tb_dtulvat)) & ", " & _
                  CDataSQL(IntSetDate("31/12/2099")) & "," & _
                  " 'A', 0, 0, 0, 0, 0, " & CStrSQL(dtrA!tb_codattx) & ", " & _
                  CStrSQL(dtrA!tb_descatt) & ", 0)"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "INSERT INTO anaditva (codditt, tb_datini, tb_datfin, tb_tipovar, tb_vcapdelco," & _
                  " tb_vcapsotco, tb_vcapverco, tb_vcapvalaz, tb_vcapnumaz, tb_vcodattx, tb_vdescatt," & _
                  " tb_vnatura)" & _
                  " VALUES (" & CStrSQL(dtrA!codditt.ToString) & ", " & _
                  CDataSQL(NTSCDate(dtrA!tb_dtulvcs)) & ", " & _
                  CDataSQL(IntSetDate("31/12/2099")) & "," & _
                  " 'C'," & CDblSQL(NTSCDec(dtrA!tb_capdelco)) & ", " & _
                  CDblSQL(NTSCDec(dtrA!tb_capsotco)) & ", " & CDblSQL(NTSCDec(dtrA!tb_capverco)) & ", " & _
                  CDblSQL(NTSCDec(dtrA!tb_capvalaz)) & ", " & CDblSQL(NTSCDec(dtrA!tb_capnumaz)) & "," & _
                  " ' ', Null, 0)"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If    'If bNew = False Then

      '----------------------------------
      'aggiorno anagen, anasto, destgen, deststo: se anagen non c'è lo creo, diversamente aggiorno
      If NTSCInt(dtrA!tb_azcodanag) <> 0 And bModuloAnagen Then
        If Not SalvaAnagen(dtrA, ds.Tables("ANAZUL"), dbConn) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
      End If

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      ds.AcceptChanges()

      '-----------------------
      'segnalo che le tabelle in cache devono essere rilette
      If oApp.bDSShared Then
        DBCacheAggDBCacheDate("TABANAZ")
      End If

      '-----------------------
      'Se in tabanaz c'è solo una ditta,
      'imposto il codice ditta come default in arcproc.aziende.azext
      strSQL = "SELECT DISTINCT codditt FROM tabanaz"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 1 Then
        strSQL = "UPDATE aziende" & _
                  " SET azext = " & CStrSQL(dtrA!codditt.ToString) & _
                  " WHERE azcodaz = " & CStrSQL(oApp.Db.Nome)
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Al salvataggio di una nuova ditta, crea alcuni records in:
      '--- TABTIPA
      '--- ARTICO
      '--- ANAGRA
      '--- ARTICO
      '--------------------------------------------------------------------------------------------------------------
      If bNew = True Then
        '------------------------------------------------------------------------------------------------------------
        '--- Crea TABTIPA
        '------------------------------------------------------------------------------------------------------------
        If ValCodiceDb("1", dtrA!codditt.ToString, "TABTIPA", "N") = False Then
          strSQL = "INSERT INTO tabtipa (codditt, tb_codtipa, tb_destipa)" & _
            " VALUES (" & CStrSQL(dtrA!codditt.ToString) & ", 1, 'Articoli normali')"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Crea ARTICO
        '------------------------------------------------------------------------------------------------------------
        strMsg = Right("0" & NTSCDate(Now.ToShortTimeString).Hour.ToString, 2) & _
          Right("0" & NTSCDate(Now.ToShortTimeString).Minute.ToString, 2)
        For i = 1 To 2
          strSQL = "INSERT INTO artico (codditt, ar_codart, ar_descr, ar_unmis, ar_stalist, ar_pesoca, ar_stainv," & _
            " ar_consmrp, ar_stasche, ar_datins, ar_orins, ar_ultagg, ar_oragg, ar_opnome)" & _
            " VALUES (" & CStrSQL(dtrA!codditt.ToString) & ", " & CStrSQL(IIf(i = 1, "D", "M").ToString) & "," & _
            " ' ', ' ', 'N', 0, 'N', 'N', 'N', " & CDataSQL(Now.ToShortDateString) & ", " & _
            NTSCInt(strMsg) & ", " & CDataSQL(Now.ToShortDateString) & ", " & NTSCInt(strMsg) & ", " & _
            CStrSQL(oApp.User.Nome) & ")"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        Next
        '------------------------------------------------------------------------------------------------------------
        '--- Preleva il primo codice pagamento con 'Tipo pagamento' = 'Rimessa diretta' o 'Contanti'
        '------------------------------------------------------------------------------------------------------------
        i = 0
        strSQL = "SELECT TOP 1 tb_codpaga FROM tabpaga" & _
          " WHERE tb_tippaga IN (3, 4)" & _
          " ORDER BY tb_codpaga"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then i = NTSCInt(dttTmp.Rows(0)!tb_codpaga)
        dttTmp.Clear()
        dttTmp.Dispose()
        '------------------------------------------------------------------------------------------------------------
        '--- Preleva il primo ocidce mastro 'Fornitore' e il PDC indicato in Anagrafica Ditta appena creata
        '------------------------------------------------------------------------------------------------------------
        Dim lConto As Integer = 0
        Dim dMoltiplicatore As Decimal = 10000
        strSQL = "SELECT TOP 1 tb_struttura, tb_codmast" & _
          " FROM tabmast INNER JOIN tabpcon ON tabmast.tb_codpcon = tabpcon.tb_codpcon" & _
          " WHERE tabmast.tb_codpcon = " & CStrSQL(dtrA!tb_azcodpcon) & _
          " AND tabmast.tb_tipomast = 'F'" & _
          " ORDER BY tabmast.tb_codpcon, tabmast.tb_codmast"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          Select Case NTSCStr(dttTmp.Rows(0)!tb_struttura)
            Case "A", "D" : dMoltiplicatore = 100000
            Case "B", "C" : dMoltiplicatore = 1000000
          End Select
          lConto = ((NTSCInt(NTSCInt(dttTmp.Rows(0)!tb_codmast) * dMoltiplicatore)) + 1)
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Crea TABMAGA
        '------------------------------------------------------------------------------------------------------------
        strSQL = "INSERT INTO tabmaga (codditt, tb_codmaga, tb_desmaga, tb_flclavo)" & _
          " VALUES (" & CStrSQL(dtrA!codditt.ToString) & ", 1, 'Ns. stabilimento', ' ')"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        '------------------------------------------------------------------------------------------------------------
        '--- Crea ANAGRA
        '------------------------------------------------------------------------------------------------------------
        If lConto <> 0 Then
          strSQL = "INSERT INTO anagra (codditt, an_conto, an_tipo, an_descr1, an_codmast, an_codpcon, an_opnome," & _
            " an_ultagg, an_codpag, an_partite, an_scaden)" & _
            " VALUES (" & CStrSQL(dtrA!codditt.ToString) & ", " & lConto & ", 'F', 'Ns. stabilimento', " & _
            NTSCInt(dttTmp.Rows(0)!tb_codmast) & ", " & CStrSQL(dtrA!tb_azcodpcon) & ", " & _
            CStrSQL(oApp.User.Nome) & ", " & CDataOraSQL(Now) & ", " & i & ", "
          Select Case NTSCStr(dtrA!tb_ventil)
            Case "F", "S" : strSQL += "'S', "
            Case Else : strSQL += "'N', "
          End Select
          Select Case NTSCStr(dtrA!tb_azgestscad)
            Case "F", "S" : strSQL += "'S')"
            Case Else : strSQL += "'N')"
          End Select
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

          strSQL = "INSERT INTO tabnuma (codditt, tb_numtipo, tb_numserie, tb_numcodl, tb_numprog, " & _
                   "tb_numprog2, tb_numdata, tb_numdata2, tb_numflag, tb_numflag2,  tb_numval, tb_numval2, tb_alfpro) " & _
                   " VALUES (" & CStrSQL(dtrA!codditt) & ", 'FF', ' ', " & NTSCInt(dttTmp.Rows(0)!tb_codmast) & _
                   ", 1, 0, '1900-1-1', '1900-1-1', '', '', 0, 0, '')"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

          dttTmp.Clear()
          dttTmp.Dispose()
          '----------------------------------------------------------------------------------------------------------
          '--- Aggiorna TABMAGA
          '----------------------------------------------------------------------------------------------------------
          strSQL = "UPDATE tabmaga" & _
            " SET tb_magconto = " & lConto & _
            " WHERE codditt = " & CStrSQL(dtrA!codditt.ToString) & _
            " AND tb_codmaga = 1"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
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
  Public Overridable Function SalvaAnagen(ByVal dtrA As DataRow, ByRef dttAnazul As DataTable, ByVal dbConn As DbConnection) As Boolean
    'aggiorno anagen e anasto: poi chiamo AggiornaAnagen per sincronizzare anagra, destdiv, tabanaz, anazul, leads
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try

      strSQL = " UPDATE anagen " & _
              " SET anagen.ag_descr1 = tabanaz.tb_azrags1, anagen.ag_descr2 = tabanaz.tb_azrags2, anagen.ag_indir = tabanaz.tb_azindir, anagen.ag_cap = tabanaz.tb_azcap, " & _
              " anagen.ag_citta = tabanaz.tb_azcitta, anagen.ag_prov = tabanaz.tb_azprov, anagen.ag_stato = tabanaz.tb_azstato, anagen.ag_codfis = tabanaz.tb_azcodf, " & _
              " anagen.ag_pariva = tabanaz.tb_azpiva, anagen.ag_telef = tabanaz.tb_aztelef, anagen.ag_faxtlx = tabanaz.tb_azfaxtlx, " & _
              " anagen.ag_ultagg = " & CDataOraSQL(DateTime.Now) & ", anagen.ag_email = tabanaz.tb_azemail, anagen.ag_website = tabanaz.tb_azwebsite, " & _
              " anagen.ag_opnome = " & CStrSQL(oApp.User.Nome) & ", anagen.ag_usaem = tabanaz.tb_azusaem, anagen.ag_webuid = tabanaz.tb_azwebuid, anagen.ag_webpwd = tabanaz.tb_azwebpwd, " & _
              " anagen.ag_sesso = tabanaz.tb_sesso, anagen.ag_datnasc = tabanaz.tb_datnasc, anagen.ag_citnasc = tabanaz.tb_locnasc, anagen.ag_pronasc = tabanaz.tb_pronasc, " & _
              " anagen.ag_stanasc = tabanaz.tb_azstanasc, anagen.ag_codfisest = tabanaz.tb_azcodfisest, anagen.ag_cell = tabanaz.tb_azcell, anagen.ag_titolo = tabanaz.tb_aztitolo, " & _
              " anagen.ag_persfg = tabanaz.tb_azpersfg, anagen.ag_profes = tabanaz.tb_azprofes, anagen.ag_condom = tabanaz.tb_azcondom, " & _
              " anagen.ag_codcomu = tabanaz.tb_azcodcomu, anagen.ag_destcorr = tabanaz.tb_uldestcorr, anagen.ag_destsedel = tabanaz.tb_uldestsedel, anagen.ag_destdomf = tabanaz.tb_uldestdomf, " & _
              " anagen.ag_destresan = tabanaz.tb_uldestresan, anagen.ag_siglaric = tabanaz.tb_azsiglaric, anagen.ag_cognome = tabanaz.tb_azcognome, anagen.ag_nome = tabanaz.tb_aznome, " & _
              " anagen.ag_codcomn = tabanaz.tb_azcodcomn, anagen.ag_nazion1 = tabanaz.tb_aznazion1, anagen.ag_nazion2 = tabanaz.tb_aznazion2, " & _
              " anagen.ag_statofed = tabanaz.tb_azstatofed, anagen.ag_soggresi = tabanaz.tb_azsoggresi, anagen.ag_omocodice = tabanaz.tb_azomocodice, anagen.ag_estcodiso = tabanaz.tb_azestcodiso, " & _
              " anagen.ag_estpariva = tabanaz.tb_azestpariva, anagen.ag_codrtac = tabanaz.tb_azcodrtac, " & _
              " anagen.ag_latitud = tabanaz.tb_latitud, anagen.ag_longitud = tabanaz.tb_longitud " & _
              " FROM anagen, tabanaz " & _
              " WHERE anagen.ag_codanag = tabanaz.tb_azcodanag " & _
              " And anagen.ag_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString & _
              " And tabanaz.codditt = " & CStrSQL(dtrA!codditt.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = " SELECT as_datini " & _
               " FROM anasto " & _
               " WHERE as_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString & _
               " And as_datini = " & CDataSQL(NTSCDate(DateTime.Now.ToShortDateString))
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count = 0 Then
        strSQL = " INSERT INTO anasto (as_codanag, as_datini, as_datfin, as_descr1, as_descr2, as_indir, as_cap, " & _
               " as_citta, as_prov, as_stato, as_codfis, as_pariva, as_telef, as_faxtlx, " & _
               " as_valuta, as_codling, as_destin, as_destpag, as_note, as_note2, as_email, " & _
               " as_website, as_usaem, as_webuid, as_webpwd, as_sesso, as_datnasc, as_citnasc, " & _
               " as_pronasc, as_stanasc, as_codfisest, as_cell, as_titolo, as_persfg, as_profes, " & _
               " as_condom, as_tpsogiva, as_codcomu, as_destcorr, " & _
               " as_destsedel, as_destdomf, as_destresan, as_siglaric, as_cognome, as_nome, as_codcomn, " & _
               " as_nazion1, as_nazion2, as_intgen, as_statofed, " & _
               " as_soggresi, as_omocodice, as_estcodiso, as_estpariva, as_codrtac, as_latitud, as_longitud)" & _
               " SELECT ag_codanag, " & CDataSQL(NTSCDate(DateTime.Now.ToShortDateString)) & " , " & CDataSQL(IntSetDate("31/12/2099")) & ", ag_descr1, ag_descr2, ag_indir, ag_cap, " & _
               " ag_citta, ag_prov, ag_stato, ag_codfis, ag_pariva, ag_telef, ag_faxtlx, " & _
               " ag_valuta, ag_codling, ag_destin, ag_destpag, ag_note, ag_note2, ag_email, " & _
               " ag_website, ag_usaem, ag_webuid, ag_webpwd, ag_sesso, ag_datnasc, ag_citnasc, " & _
               " ag_pronasc, ag_stanasc, ag_codfisest, ag_cell, ag_titolo, ag_persfg, ag_profes, ag_condom, " & _
               " ag_tpsogiva, ag_codcomu, ag_destcorr, " & _
               " ag_destsedel, ag_destdomf, ag_destresan, ag_siglaric, ag_cognome, ag_nome, ag_codcomn, " & _
               " ag_nazion1, ag_nazion2, ag_intgen, ag_statofed, ag_soggresi, ag_omocodice, ag_estcodiso, " & _
               " ag_estpariva, ag_codrtac, ag_latitud, ag_longitud " & _
               " FROM anagen " & _
               " WHERE ag_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'aggiorno la data di fine del record precedente
        strSQL = "SELECT top 1 as_datini FROM anasto " & _
                 " WHERE as_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString & _
                 " And as_datini < " & CDataSQL(NTSCDate(DateTime.Now.ToShortDateString)) & _
                 " ORDER BY as_datini DESC "
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = " UPDATE anasto SET as_datfin = " & CDataSQL(DateAdd("d", -1, DateTime.Now).ToShortDateString) & _
                 " WHERE as_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString & _
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
                 " And anasto.as_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString & _
                 " And as_datini = " & CDataSQL(NTSCDate(dttTmp.Rows(0)!as_datini).ToShortDateString)
        dttTmp.Clear()
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If    'If dttTmp.Rows.Count > 0 Then

      '--------------------------------------------------------------------------
      'adesso stessa cosa per destgen / deststo: inserisco o aggiorno
      For Each dtrD As DataRow In dttAnazul.Rows
        'devo sapere se la destinazione è già esistente
        strSQL = "SELECT top 1 dd_coddest FROM destgen WHERE dd_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString & " AND dd_coddest = " & NTSCInt(dtrD!ul_coddest).ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count = 0 Then
          'è una nuova destinazione diversa
          strSQL = "INSERT INTO destgen (dd_codanag, dd_coddest, dd_nomdest, dd_nomdest2," & _
                    " dd_inddest, dd_capdest, dd_locdest, dd_prodest, dd_turno, dd_telef, dd_codfis," & _
                    " dd_pariva, dd_faxtlx, dd_email, dd_usaem, dd_stato, dd_codcomu, dd_codfisest," & _
                    " dd_statofed, dd_latitud, dd_longitud)" & _
                    " SELECT " & NTSCInt(dtrA!tb_azcodanag).ToString & ", ul_coddest, ul_nomdest, ul_nomdest2, ul_inddest," & _
                    " ul_capdest, ul_locdest, ul_prodest, ul_turno, ul_telef, ul_codfis, ul_pariva," & _
                    " ul_faxtlx, ul_email, ul_usaem, ul_stato, ul_codcomu, ul_codfisest, ul_statofed, " & _
                    " ul_latitud, ul_longitud " & _
                    " FROM anazul" & _
                    " WHERE codditt = " & CStrSQL(dtrA!codditt.ToString) & _
                    " AND ul_coddest = " & NTSCInt(dtrD!ul_coddest).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          strSQL = "INSERT INTO deststo (dd_codanag, dd_coddest, dd_datini, dd_datfin," & _
                    " dd_nomdest, dd_nomdest2, dd_inddest, dd_capdest, dd_locdest, dd_prodest," & _
                    " dd_turno, dd_telef, dd_codfis, dd_pariva, dd_faxtlx, dd_email, dd_usaem," & _
                    " dd_stato, dd_codcomu, dd_codfisest, dd_statofed, dd_latitud, dd_longitud)" & _
                    " SELECT " & NTSCInt(dtrA!tb_azcodanag).ToString & ", ul_coddest, " & _
                    CDataSQL(NTSCDate(DateTime.Now.ToShortDateString)) & ", " & _
                    CDataSQL(IntSetDate("31/12/2099")) & ", " & _
                    " ul_nomdest, ul_nomdest2, ul_inddest," & _
                    " ul_capdest, ul_locdest, ul_prodest, ul_turno, ul_telef, ul_codfis, ul_pariva," & _
                    " ul_faxtlx, ul_email, ul_usaem, ul_stato, ul_codcomu, ul_codfisest, ul_statofed," & _
                    " ul_latitud, ul_longitud " & _
                    " FROM anazul" & _
                    " WHERE codditt = " & CStrSQL(dtrA!codditt.ToString) & _
                    " AND ul_coddest = " & NTSCInt(dtrD!ul_coddest).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Else
          dttTmp.Clear()

          'destinazione diversa già esistente
          strSQL = "UPDATE destgen SET " & _
                    " destgen.dd_nomdest = " & CStrSQL(NTSCStr(dtrD!ul_nomdest)) & ", " & _
                    " destgen.dd_nomdest2 = " & CStrSQL(NTSCStr(dtrD!ul_nomdest2)) & ", " & _
                    " destgen.dd_inddest = " & CStrSQL(NTSCStr(dtrD!ul_inddest)) & ", " & _
                    " destgen.dd_capdest = " & CStrSQL(NTSCStr(dtrD!ul_capdest)) & ", " & _
                    " destgen.dd_locdest = " & CStrSQL(NTSCStr(dtrD!ul_locdest)) & ", " & _
                    " destgen.dd_prodest = " & CStrSQL(NTSCStr(dtrD!ul_prodest)) & ", " & _
                    " destgen.dd_turno = " & CStrSQL(NTSCStr(dtrD!ul_turno)) & ", " & _
                    " destgen.dd_telef = " & CStrSQL(NTSCStr(dtrD!ul_telef)) & ", " & _
                    " destgen.dd_codfis = " & CStrSQL(NTSCStr(dtrD!ul_codfis)) & "," & _
                    " destgen.dd_pariva = " & CStrSQL(NTSCStr(dtrD!ul_pariva)) & ", " & _
                    " destgen.dd_faxtlx = " & CStrSQL(NTSCStr(dtrD!ul_faxtlx)) & ", " & _
                    " destgen.dd_email = " & CStrSQL(NTSCStr(dtrD!ul_email)) & ", " & _
                    " destgen.dd_usaem = " & CStrSQL(NTSCStr(dtrD!ul_usaem)) & ", " & _
                    " destgen.dd_stato = " & CStrSQL(NTSCStr(dtrD!ul_stato)) & ", " & _
                    " destgen.dd_codcomu = " & CStrSQL(NTSCStr(dtrD!ul_codcomu)) & ", " & _
                    " destgen.dd_codfisest = " & CStrSQL(NTSCStr(dtrD!ul_codfisest)) & ", " & _
                    " destgen.dd_latitud = " & CStrSQL(NTSCStr(dtrD!ul_latitud)) & ", " & _
                    " destgen.dd_longitud = " & CStrSQL(NTSCStr(dtrD!ul_longitud)) & ", " & _
                    " destgen.dd_statofed = " & CStrSQL(NTSCStr(dtrD!ul_statofed)) & _
                    " WHERE destgen.dd_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString & _
                    " AND destgen.dd_coddest = " & NTSCInt(dtrD!ul_coddest).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          strSQL = " SELECT dd_datini " & _
                   " FROM deststo " & _
                   " WHERE dd_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString & _
                   " AND dd_coddest = " & NTSCInt(dtrD!ul_coddest).ToString & _
                   " And dd_datini = " & CDataSQL(NTSCDate(DateTime.Now.ToShortDateString))
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          If dttTmp.Rows.Count = 0 Then
            strSQL = "INSERT INTO deststo (dd_codanag, dd_coddest, dd_datini, dd_datfin," & _
                      " dd_nomdest, dd_nomdest2, dd_inddest, dd_capdest, dd_locdest, dd_prodest," & _
                      " dd_turno, dd_telef, dd_codfis, dd_pariva, dd_faxtlx, dd_email, dd_usaem," & _
                      " dd_stato, dd_codcomu, dd_codfisest, dd_statofed, dd_latitud, dd_longitud)" & _
                      " SELECT " & NTSCInt(dtrA!tb_azcodanag).ToString & ", dd_coddest, " & _
                      CDataSQL(NTSCDate(DateTime.Now.ToShortDateString)) & ", " & _
                      CDataSQL(IntSetDate("31/12/2099")) & ", " & _
                      " dd_nomdest, dd_nomdest2, dd_inddest," & _
                      " dd_capdest, dd_locdest, dd_prodest, dd_turno, dd_telef, dd_codfis, dd_pariva," & _
                      " dd_faxtlx, dd_email, dd_usaem, dd_stato, dd_codcomu, dd_codfisest, dd_statofed, " & _
                      " dd_latitud, dd_longitud " & _
                      " FROM destgen" & _
                      " WHERE dd_codanag = " & CStrSQL(dtrA!tb_azcodanag.ToString) & _
                      " AND dd_coddest = " & NTSCInt(dtrD!ul_coddest).ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            'aggiorno la data di fine del record precedente
            strSQL = "SELECT top 1 dd_datini FROM deststo " & _
                     " WHERE dd_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString & _
                     " AND dd_coddest = " & NTSCInt(dtrD!ul_coddest).ToString & _
                     " And dd_datini < " & CDataSQL(NTSCDate(DateTime.Now.ToShortDateString)) & _
                     " ORDER BY dd_datini DESC "
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            strSQL = " UPDATE deststo SET dd_datfin = " & CDataSQL(DateAdd("d", -1, DateTime.Now).ToShortDateString) & _
                     " WHERE dd_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString & _
                     " AND dd_coddest = " & NTSCInt(dtrD!ul_coddest).ToString & _
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
                      " deststo.dd_latitud = destgen.dd_latitud, " & _
                      " deststo.dd_longitud = destgen.dd_longitud, " & _
                      " deststo.dd_statofed = destgen.dd_statofed " & _
                      " FROM deststo, destgen " & _
                      " WHERE destgen.dd_codanag = deststo.dd_codanag " & _
                      " AND destgen.dd_coddest = deststo.dd_coddest" & _
                      " AND destgen.dd_codanag = " & NTSCInt(dtrA!tb_azcodanag).ToString & _
                      " AND destgen.dd_coddest = " & NTSCInt(dtrD!ul_coddest).ToString & _
                      " AND deststo.dd_datini = " & CDataSQL(NTSCDate(DateTime.Now.ToShortDateString))
            dttTmp.Clear()
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        End If
      Next    'For Each dtrD As DataRow In dttDestdiv.Rows

      'adesso sincronizzo tutto (tabanaz, le altre anagen, i leads, destdiv, ...) da anagen/destgen
      If Not AggiornaAnagen(CStrSQL(dtrA!codditt.ToString), "TABANAZ", NTSCInt(dtrA!tb_azcodanag), 0, dbConn) Then Return False

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EliminaRigheOrganig(ByVal dttOrganig As DataTable, ByVal strDitta As String, ByVal bModuloCRM As Boolean, ByVal dbConn As DbConnection) As Boolean
    Dim dttOrganigOld As DataTable = Nothing
    Dim strSQL As String = ""
    Try
      If bModuloCRM Then
        'Cerca le righe cancellate: prende tutte le righe presenti sul database e le confronta con quelle che sono rimaste. 
        'Quelle sparite decide se spostarle sul lead -1
        strSQL = "SELECT og_progr FROM organig " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 "   AND og_tipork <> 'L'" & _
                 "   AND og_conto = 0" & _
                 "   AND og_codlead = 0"

        dttOrganigOld = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        For Each dtrOld As DataRow In dttOrganigOld.Rows
          'Organizzaizone cancellata, la posto sul lead -1 nel caso sia l'ultima rimasta per quel contatto
          If dttOrganig.Select("og_progr = " & NTSCInt(dtrOld!og_progr)).Length = 0 Then
            strSQL = "UPDATE organig SET og_conto = 0, og_coddest = 0, og_codlead = -1 " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     "   AND og_progr = " & NTSCInt(dtrOld!og_progr) & _
                     "   AND NOT EXISTS (SELECT TOP 1 co_progr FROM contatti " & _
                     "                   INNER JOIN organig AS organig2 ON co_progr = organig2.og_contatto " & _
                     "                   WHERE co_progr = organig.og_contatto " & _
                     "                     AND organig2.og_progr <> organig.og_progr) "
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        Next
      End If

      strSQL = "DELETE FROM organig" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND og_tipork <> 'L'" & _
               "   AND og_conto = 0" & _
               "   AND og_codlead = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function Cancella(ByVal strDitta As String, ByVal bScriviActlog As Boolean) As Boolean
    Try
      Return Cancella(strDitta, bScriviActlog, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Cancella(ByVal strDitta As String, ByVal bScriviActlog As Boolean, ByVal bModuloCRM As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strDesogglog As String = ""
    Dim strSqlWhere As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dttTabDel As New DataTable

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bScriviActlog, bModuloCRM})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------


      strSQL = "SELECT ot_nometab FROM ordertbl" & _
              " WHERE ot_perditta = 'S'" & _
              " AND ot_nometab NOT IN ('tabduri', 'tabatti', 'anaziva', 'organig', 'anazul'," & _
                                     " 'accditt', 'accditm', 'accdito', 'anazmod', 'tabesco'," & _
                                     " 'anaditac', 'anaditpa', 'tabinst', 'tabanaz')" & _
              " ORDER BY ot_order DESC"
      dttTabDel = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      For Each dtrT As DataRow In dttTabDel.Rows
        strSQL = "DELETE FROM " & NTSCStr(dtrT!ot_nometab) & " WHERE codditt = " & CStrSQL(strDitta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      strSQL = "DELETE FROM tabduri WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM tabatti WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM anaziva WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'Gestione Organizzazione 2015, se modulo CRM collego organig con lead -1 se è l'ultimo organig del contatto
      If bModuloCRM Then
        strSQL = "UPDATE organig SET og_conto = 0, og_coddest = 0, og_codlead = -1 " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND og_conto = 0 " & _
                 " AND og_codlead = 0 " & _
                 "   AND NOT EXISTS (SELECT TOP 1 co_progr FROM contatti " & _
                 "                   INNER JOIN organig AS organig2 ON co_progr = organig2.og_contatto " & _
                 "                   WHERE co_progr = organig.og_contatto " & _
                 "                     AND organig2.og_progr <> organig.og_progr) "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      strSQL = "DELETE FROM organig" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND og_conto = 0" & _
                " AND og_codlead = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '-------------------------------------------------------------

      strSQL = "DELETE FROM anazul WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM accditt WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM accditm WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM accdito WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM anazmod WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM tabesco WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM anaditac WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM anaditacs WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM anaditva WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM tabinst WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM anaditpa WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM tabanaz WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '---------------------------------
      'scrivo il log
      If bScriviActlog Then
        strSqlWhere = CStrSQL(strDitta)
        strDesogglog = oApp.Tr(Me, 128865252336790133, "Cancellazione dati relativi all'anagrafica ditta '|" & strDitta & "|'")
        If Not ScriviActLog(strDitta, "BS--ANAZ", "tabanaz", "tabanaz", strSqlWhere, "A", "D", strDesogglog, False) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
      End If

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


  Public Overridable Function AggRiclassif(ByVal strDitta As String, ByVal strCodpcon As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM anatric" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND ant_conto IN (SELECT an_conto FROM anagra" & _
                                  " WHERE codditt = " & CStrSQL(strDitta) & _
                                  " AND an_pcconto <> 0)"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '-----------------------------
      'Inserisce i records di ANPTRIC in ANATRIC
      strSQL = "INSERT INTO anatric (codditt, ant_conto, ant_codtric, ant_kpcrd, ant_kpcra)" & _
                " SELECT " & CStrSQL(strDitta) & ", pct_conto, pct_codtric, pct_kpcrd," & _
                " pct_kpcra" & _
                " FROM anptric" & _
                " WHERE pct_codpcon = " & CStrSQL(strCodpcon)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CespitiMovimentati(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 * FROM movcesp WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RitenuteMovimentate(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 * FROM ritenut WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ProvvigioniNonPagate(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 * FROM provvig WHERE codditt = " & CStrSQL(strDitta) & _
               " AND pv_impvpag = 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IvaMovimentata(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 * FROM moviva WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetDescrProgramma(ByVal strChild As String, ByRef nModuloPadre As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      GetDescrProgramma = ""
      strSQL = "SELECT mnDescr, mnmd_modulo FROM menupop INNER JOIN accmoduli ON menupop.mnprogr = accmoduli.mnmd_nomprog " & _
               " WHERE mnProgr = " & CStrSQL(strChild)
      If nModuloPadre <> 0 Then strSQL += " AND mnmd_modulo = " & nModuloPadre.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      If dttTmp.Rows.Count > 0 Then
        nModuloPadre = NTSCInt(dttTmp.Rows(0)!mnmd_modulo)
        GetDescrProgramma = NTSCStr(dttTmp.Rows(0)!mnDescr)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetProgrammiFromModulo(ByVal nModulo As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT accmoduli.* " & _
               " FROM menupop INNER JOIN accmoduli ON menupop.mnprogr = accmoduli.mnmd_nomprog " & _
               " WHERE mnmd_modulo = " & nModulo.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetRelease(ByVal strDitta As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT rel_maior, rel_pers FROM release" & _
        " WHERE UPPER(rel_sw) = 'BUSINESS'"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function


  Public Overridable Function LeggiInsg(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tabinsg.* FROM tabinsg"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetMoviva(ByVal strDitta As String, ByVal nAnnoTabattiAperto As Integer, _
                                      ByVal nCodattiDeleted As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "select top 1 * from moviva inner join tabduri on moviva.codditt = tabduri.codditt and moviva.mi_tregiva = tabduri.tb_utipreg and moviva.mi_nregiva = tabduri.tb_unumreg and year(moviva.mi_datreg3) = tabduri.tb_anno" & _
        " INNER JOIN tabatti ON tabduri.codditt = tabatti.codditt AND tabduri.tb_ucodatti = tabatti.tb_codatti AND tabduri.tb_anno = tabatti.tb_anno" & _
        " where tabatti.codditt = " & CStrSQL(strDitta) & " and tabatti.tb_codatti = " & nCodattiDeleted & " and tabatti.tb_anno = " & nAnnoTabattiAperto

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABATTI")

      Return True
    Catch ex As Exception
      '--------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------
    End Try
  End Function

  Public Overridable Function TipoProtCambiato(ByVal strDitta As String, ByRef dtrT As DataRow) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      TipoProtCambiato = False
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 mi_datreg FROM moviva" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND YEAR(mi_datreg) = " & NTSCInt(dtrT!tb_anno) & _
        " AND mi_tregiva = " & CStrSQL(NTSCStr(dtrT!tb_utipreg)) & _
        " AND mi_nregiva = " & NTSCInt(dtrT!tb_unumreg)
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    End Try
  End Function

End Class
