Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLD__ANDB
  Inherits CLD__BASE

  Public Overridable Function Elabora(ByVal strDitta As String) As Boolean
    'restituisce true se nella ditta c'è almeno un conto gestito a scadenze
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      strSQL = "UPDATE tabanaz" & _
               " SET tb_azrags1 = 'Ditta n° ' + CONVERT(varchar, tb_codanaz)," & _
               " tb_azrags2 = NULL," & _
               " tb_azindir = NULL," & _
               " tb_azcodf = NULL," & _
               " tb_azpiva = NULL," & _
               " tb_azcognome = NULL," & _
               " tb_aznome = NULL," & _
               " tb_aztelef = NULL," & _
               " tb_azfaxtlx = NULL," & _
               " tb_azemail = NULL," & _
               " tb_azwebsite = NULL," & _
               " tb_azwebuid = NULL," & _
               " tb_azwebpwd = NULL," & _
               " tb_azcodfisest = NULL," & _
               " tb_azsiglaric = NULL, " & _
               " tb_azcell = NULL," & _
               " tb_azestpariva = NULL," & _
               " tb_azrifriba = NULL," & _
               " tb_azcodcc = NULL" & _
               " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE anazul" & _
               " SET ul_nomdest = 'Destinazione n° ' + CONVERT(varchar, ul_coddest)," & _
               " ul_nomdest2 = NULL," & _
               " ul_inddest = NULL," & _
               " ul_telef = NULL," & _
               " ul_codfis =  NULL," & _
               " ul_pariva = NULL," & _
               " ul_faxtlx = NULL," & _
               " ul_email = NULL," & _
               " ul_codfisest = NULL" & _
               " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE anagen" & _
              " SET ag_descr1 = 'Anagrafica n° ' + CONVERT(varchar, ag_codanag)," & _
              " ag_descr2 = NULL," & _
              " ag_indir = NULL," & _
              " ag_codfis = NULL," & _
              " ag_pariva = NULL," & _
              " ag_telef = NULL," & _
              " ag_faxtlx = NULL," & _
              " ag_note = NULL," & _
              " ag_note2 = NULL," & _
              " ag_email = NULL," & _
              " ag_website = NULL," & _
              " ag_webuid = NULL," & _
              " ag_webpwd = NULL," & _
              " ag_codfisest = NULL," & _
              " ag_cell = NULL," & _
              " ag_cognome = NULL," & _
              " ag_nome = NULL," & _
              " ag_siglaric = NULL, " & _
              " ag_estpariva = NULL"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE anasto" & _
              " SET as_descr1 = 'Anagrafica n° ' + CONVERT(varchar, as_codanag)," & _
              " as_descr2 = NULL," & _
              " as_indir = NULL," & _
              " as_codfis = NULL," & _
              " as_pariva = NULL," & _
              " as_telef = NULL," & _
              " as_faxtlx = NULL," & _
              " as_note = NULL," & _
              " as_note2 = NULL," & _
              " as_email = NULL," & _
              " as_website = NULL," & _
              " as_webuid = NULL," & _
              " as_webpwd = NULL," & _
              " as_codfisest = NULL," & _
              " as_cell = NULL," & _
              " as_cognome = NULL," & _
              " as_nome = NULL," & _
              " as_siglaric = NULL, " & _
              " as_estpariva = NULL"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE destgen" & _
              " SET dd_nomdest = 'Destinazione n° ' + CONVERT(varchar, dd_coddest)," & _
              " dd_nomdest2 = NULL," & _
              " dd_inddest = NULL," & _
              " dd_telef = NULL," & _
              " dd_codfis = NULL," & _
              " dd_pariva = NULL," & _
              " dd_faxtlx = NULL," & _
              " dd_email = NULL," & _
              " dd_codfisest = NULL"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE deststo" & _
              " SET dd_nomdest = 'Destinazione n° ' + CONVERT(varchar, dd_coddest)," & _
              " dd_nomdest2 = NULL," & _
              " dd_inddest = NULL," & _
              " dd_telef = NULL," & _
              " dd_codfis = NULL," & _
              " dd_pariva = NULL," & _
              " dd_faxtlx = NULL," & _
              " dd_email = NULL," & _
              " dd_codfisest = NULL"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE anagra" & _
              " SET an_descr1 = CASE WHEN an_tipo = 'C' THEN 'Cliente n° ' ELSE 'Fornitore n° ' END + CONVERT(varchar, an_conto)," & _
              " an_descr2 = NULL," & _
              " an_indir = NULL," & _
              " an_codfis = NULL," & _
              " an_pariva = NULL," & _
              " an_telef = NULL," & _
              " an_faxtlx = NULL," & _
              " an_rifriba = NULL," & _
              " an_note = NULL," & _
              " an_note2 = NULL," & _
              " an_email = NULL," & _
              " an_website = NULL," & _
              " an_webuid = NULL," & _
              " an_webpwd = NULL," & _
              " an_cognome = NULL," & _
              " an_nome = NULL," & _
              " an_siglaric = NULL, " & _
              " an_cell = NULL," & _
              " an_codfisest = NULL," & _
              " an_estpariva = NULL," & _
              " an_iban = NULL," & _
              " an_cin = NULL," & _
              " an_prefiban = NULL," & _
              " an_swift = NULL," & _
              " an_dtmandrid = NULL" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND an_tipo IN ('C', 'F')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE destdiv" & _
              " SET dd_nomdest = 'Destinazione n° ' + CONVERT(varchar, dd_coddest)," & _
              " dd_inddest = NULL," & _
              " dd_telef = NULL," & _
              " dd_codfis = NULL," & _
              " dd_pariva = NULL," & _
              " dd_faxtlx = NULL," & _
              " dd_email = NULL," & _
              " dd_nomdest2 = NULL," & _
              " dd_note = NULL," & _
              " dd_codfisest = NULL" & _
              " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE leads" & _
              " SET le_descr1 = 'Lead n° ' + CONVERT(varchar, le_codlead)," & _
              " le_descr2 = NULL," & _
              " le_indir = NULL," & _
              " le_codfis = NULL," & _
              " le_pariva = NULL," & _
              " le_telef = NULL," & _
              " le_faxtlx = NULL," & _
              " le_contatt = NULL," & _
              " le_note = NULL," & _
              " le_note2 = NULL," & _
              " le_email = NULL," & _
              " le_website = NULL," & _
              " le_webuid = NULL," & _
              " le_webpwd = NULL," & _
              " le_cell = NULL" & _
              " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE organig" & _
               " SET og_telef = NULL," & _
               "     og_fax = NULL," & _
               "     og_email = NULL," & _
               "     og_cell = NULL," & _
               "     og_descont = CASE WHEN og_descont <> ' ' THEN 'Contatto n° ' + CONVERT(varchar, og_progr) ELSE og_descont END," & _
               "     og_descont2 = NULL," & _
               "     og_indir = NULL," & _
               "     og_mansioni = NULL, " & _
               "     og_fbuser = NULL, " & _
               "     og_twitteruser = NULL, " & _
               "     og_skypeuser = NULL, " & _
               "     og_telefint = NULL, " & _
               "     og_telefpers = NULL, " & _
               "     og_faxpers = NULL, " & _
               "     og_emailpers = NULL, " & _
               "     og_cellpers = NULL " & _
               " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE contatti" & _
               " SET co_descont = CASE WHEN co_descont <> ' ' THEN 'Contatto n° ' + CONVERT(varchar, co_progr) ELSE co_descont END," & _
               "     co_descont2 = NULL," & _
               "     co_indir = NULL," & _
               "     co_fbuser = NULL, " & _
               "     co_twitteruser = NULL, " & _
               "     co_skypeuser = NULL, " & _
               "     co_telefpers = NULL, " & _
               "     co_faxpers = NULL, " & _
               "     co_emailpers = NULL, " & _
               "     co_cellpers = NULL "
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE repratresi" & _
               " SET pr_cognome = 'Cognome'," & _
               "     pr_nome = 'Nome'," & _
               "     pr_ragsoc = 'Ragione Sociale'," & _
               "     pr_indir = NULL, " & _
               "     pr_cap = NULL, " & _
               "     pr_citta = NULL, " & _
               "     pr_prov = NULL, " & _
               "     pr_codfis = 'CODFIS', " & _
               "     pr_pariva = 'PARIVA', " & _
               "     pr_telef = NULL, " & _
               "     pr_fax = NULL, " & _
               "     pr_email = NULL " & _
               " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE anacesp" & _
        " SET ae_descr1 = 'Cespite n° ' + CONVERT(varchar, ae_codcesp)," & _
        " ae_descr2 = NULL," & _
        " ae_descr3 = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE artico" & _
        " SET ar_descr = 'Articolo ' + SUBSTRING(ar_codart, 0, 30)," & _
        " ar_desint = NULL," & _
        " ar_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE artroot" & _
        " SET arr_descr = 'Articolo ' + SUBSTRING(arr_codroot, 0, 30) " & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE artval" & _
        " SET ax_descr = 'Articolo ' + SUBSTRING(ax_codart, 0, 30)," & _
        " ax_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE commess" & _
        " SET co_descr1 = 'Commessa n° ' + CONVERT(varchar, co_comme)," & _
        " co_descr2 = NULL," & _
        " co_note = NULL," & _
        " co_notetecn = NULL," & _
        " co_notegest = NULL," & _
        " co_notecomm = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE subcomm" & _
        " SET sco_descr = 'Comm. ' + CONVERT(varchar, sco_commeca) + ' subc. ' + CONVERT(varchar, sco_subcommeca)," & _
        " sco_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE tabcage" & _
        " SET tb_descage = 'Agente n° ' + CONVERT(varchar, tb_codcage)" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE tabcope" & _
        " SET tb_descope = 'Operaio n° ' + CONVERT(varchar, tb_codcope)" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE tabcont" & _
        " SET tb_descont = 'Contatto n° ' + CONVERT(varchar, tb_codcont)," & _
        " tb_descont2 = NULL," & _
        " tb_telef = NULL," & _
        " tb_fax = NULL," & _
        " tb_email = NULL," & _
        " tb_indir = NULL," & _
        " tb_cell = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE tabappr" & _
        " SET tb_desappr = 'Approvv. n° ' + CONVERT(varchar, tb_codappr)," & _
        " tb_appremail = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE tabvett" & _
        " SET tb_desvett = 'Vettore n° ' + CONVERT(varchar, tb_codvett)," & _
        " tb_desvet2 = NULL," & _
        " tb_ntelef = NULL," & _
        " tb_nfax = NULL," & _
        " tb_vettemail = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE ordlist" & _
        " SET ol_descr = 'Articolo ' + SUBSTRING(ol_codart, 0, 30)," & _
        " ol_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE testoff" & _
        " SET td_note = NULL," & _
        " td_riferim = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE movoff" & _
        " SET mo_descr = 'Articolo ' + SUBSTRING(mo_codart, 0, 30)," & _
        " mo_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE testord" & _
        " SET td_note = NULL," & _
        " td_riferim = NULL," & _
        " td_aspetto = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE movord" & _
        " SET mo_descr = 'Articolo ' + SUBSTRING(mo_codart, 0, 30)," & _
        " mo_desint = NULL," & _
        " mo_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE testmag" & _
        " SET tm_note = NULL," & _
        " tm_riferim = NULL," & _
        " tm_aspetto = NULL," & _
        " tm_andescr1 = NULL," & _
        " tm_andescr2 = NULL," & _
        " tm_anindir = NULL," & _
        " tm_ancodfis = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE movmag" & _
        " SET mm_descr = 'Articolo ' + SUBSTRING(mm_codart, 0, 30)," & _
        " mm_desint = NULL," & _
        " mm_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE testprb" & _
        " SET tm_note = NULL," & _
        " tm_riferim = NULL," & _
        " tm_aspetto = NULL," & _
        " tm_andescr1 = NULL," & _
        " tm_andescr2 = NULL," & _
        " tm_anindir = NULL," & _
        " tm_ancodfis = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE movprb" & _
        " SET mm_descr = 'Articolo ' + SUBSTRING(mm_codart, 0, 30)," & _
        " mm_desint = NULL," & _
        " mm_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE testrda" & _
        " SET td_note = NULL," & _
        " td_riferim = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE testrdo" & _
        " SET to_note = NULL," & _
        " to_riferim = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE movrdo" & _
        " SET rdo_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE prinot" & _
        " SET pn_descr = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE scaden" & _
        " SET sc_descr = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE pnnote" & _
        " SET py_note_1 = NULL," & _
        " py_note_2 = NULL," & _
        " py_note_3 = NULL," & _
        " py_note_4 = NULL," & _
        " py_note_5 = NULL," & _
        " py_note_6 = NULL," & _
        " py_note_7 = NULL," & _
        " py_note_8 = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE tabnote" & _
        " SET tb_desnote = ' '," & _
        " tb_testonot = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE priana" & _
        " SET pa_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE priana2" & _
        " SET pa2_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE lavmano" & _
        " SET lcm_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE movdis" & _
        " SET md_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE cicli" & _
        " SET dd_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE attivit" & _
        " SET at_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE lavcent" & _
        " SET lce_note = NULL" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE curighe" & _
               " SET cu_acodfisforn = NULL, " & _
               "     cu_bcognome = NULL, " & _
               "     cu_bnome = NULL, " & _
               "     cu_bdenom = NULL, " & _
               "     cu_bemail = NULL, " & _
               "     cu_btel = NULL, " & _
               "     cu_bcodfisappr = NULL, " & _
               "     cu_bcognrappr = NULL, " & _
               "     cu_bnomerappr = NULL, " & _
               "     cu_bcodfisentedich = NULL, " & _
               "     cu_bcodfisinter = NULL, " & _
               "     cu_dcodfis = NULL, " & _
               "     cu_dnome = NULL, " & _
               "     cu_dindir = NULL, " & _
               "     cu_dtelef = NULL, " & _
               "     cu_demail = NULL, " & _
               "     cu_dsede = NULL, " & _
               "     cu_dcodfisdip = NULL, " & _
               "     cu_dcognomedip = NULL, " & _
               "     cu_dnomedip = NULL, " & _
               "     cu_dcitnasc = NULL, " & _
               "     cu_dpronasc = NULL, " & _
               "     cu_dcodfisrapp = NULL, " & _
               "     cu_dcodfisest = NULL, " & _
               "     cu_dcitest = NULL, " & _
               "     cu_dindirest = NULL " & _
               " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE sptestate" & _
               " SET spt_email = NULL, " & _
               "     spt_tel = NULL, " & _
               "     spt_fax = NULL, " & _
               "     spt_cognome = NULL, " & _
               "     spt_nome = NULL, " & _
               "     spt_comune = NULL, " & _
               "     spt_ragsoc = NULL, " & _
               "     spt_codfisappr = NULL, " & _
               "     spt_cognrappr = NULL, " & _
               "     spt_nomerappr = NULL, " & _
               "     spt_ragsocrappr = NULL, " & _
               "     spt_codfisccaf = NULL" & _
               " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE supostt" & _
           " SET spt_testo = 'Messaggio'"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE supostd" & _
               " SET spd_path = NULL"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE tabsuli" & _
               " SET tb_dessuli = 'Lista ' + CONVERT(varchar, tb_codsuli)"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE supostco" & _
               " SET spc_testo = 'Messaggio'"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "TRUNCATE TABLE emails"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "TRUNCATE TABLE emailall" 
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "TRUNCATE TABLE emaildes"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "TRUNCATE TABLE emaildir"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "TRUNCATE TABLE emaildac"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '---------------------------------------------------------------------------------------------------------------------
      strSQL = "TRUNCATE TABLE emailconf"
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
    End Try
  End Function
End Class
