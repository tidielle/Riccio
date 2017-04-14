Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLD__HLAN
  Inherits CLD__BASE

  Public Overridable Function InitHlan(ByVal strDitta As String, ByRef bSemplificata As Boolean, ByRef strCodPcon As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet

    Try
      strCodPcon = ""
      '----------------------------------------------
      'ottengo se la ditta opera in contabilit semplificata nell'esercizio corrente
      bSemplificata = False
      strSQL = "SELECT tb_aztipcont FROM tabesco" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tb_dtineser <= " & CDataSQL(DateTime.Now.ToShortDateString) & _
          " AND tb_dtfieser >= " & CDataSQL(DateTime.Now.ToShortDateString)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABESCO")
      If dsTmp.Tables("TABESCO").Rows.Count > 0 Then
        If dsTmp.Tables("TABESCO").Rows(0)!tb_aztipcont.ToString = "S" Then bSemplificata = True
      End If

      '----------------------------------------------
      'ottengo il piano dei conti utilizzato dalla ditta
      strSQL = "SELECT tb_azcodpcon, tb_ventil, tb_azgestscad FROM tabanaz" & _
               " WHERE codditt = " & CStrSQL(strDitta)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")
      If dsTmp.Tables("TABANAZ").Rows.Count > 0 Then
        strCodPcon = dsTmp.Tables("TABANAZ").Rows(0)!tb_azcodpcon.ToString
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataAnagra(ByRef dsHlan As DataSet, ByVal strDitta As String, ByVal strQuery As String, _
                                 ByVal strQueryAnagen As String, ByVal bAncheAnagen As Boolean, _
                                 ByVal bSoloSemplificata As Boolean, ByVal strTipoConto As String, _
                                 ByVal strTipoSottoconto As String, ByVal strCodPcon As String, _
                                 ByVal bModuloCRM As Boolean, ByVal strAccVis As String, _
                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                 ByVal IsCrmUser As Boolean) As Boolean


    Try

      Return GetDataAnagra(dsHlan, strDitta, strQuery, strQueryAnagen, bAncheAnagen, bSoloSemplificata, strTipoConto, strTipoSottoconto, _
                      strCodPcon, bModuloCRM, strAccVis, lCodorgaOperat, strRegvis, IsCrmUser, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDataAnagra(ByRef dsHlan As DataSet, ByVal strDitta As String, ByVal strQuery As String, _
                                 ByVal strQueryAnagen As String, ByVal bAncheAnagen As Boolean, _
                                 ByVal bSoloSemplificata As Boolean, ByVal strTipoConto As String, _
                                 ByVal strTipoSottoconto As String, ByVal strCodPcon As String, _
                                 ByVal bModuloCRM As Boolean, ByVal strAccVis As String, _
                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                 ByVal IsCrmUser As Boolean, ByVal strTipologia As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return GetDataAnagra(dsHlan, strDitta, strQuery, strQueryAnagen, bAncheAnagen, bSoloSemplificata, _
        strTipoConto, strTipoSottoconto, strCodPcon, bModuloCRM, strAccVis, lCodorgaOperat, strRegvis, _
        IsCrmUser, strTipologia, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetDataAnagra(ByRef dsHlan As DataSet, ByVal strDitta As String, ByVal strQuery As String, _
                                 ByVal strQueryAnagen As String, ByVal bAncheAnagen As Boolean, _
                                 ByVal bSoloSemplificata As Boolean, ByVal strTipoConto As String, _
                                 ByVal strTipoSottoconto As String, ByVal strCodPcon As String, _
                                 ByVal bModuloCRM As Boolean, ByVal strAccVis As String, _
                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                 ByVal IsCrmUser As Boolean, ByVal strTipologia As String, _
                                 ByVal bAbituali As Boolean) As Boolean



    '--------------------------------------
    'se sono arrivato qui sicuramente  stato impostato se devo cercare clienti/fornitori o sottoconti
    Dim bRelease15 As Boolean = False
    Dim strSQL As String = ""
    Dim strTabAnagra As String = "anagra"
    Dim strTipoJoin As String = " LEFT"
    Dim dsTmp As New DataSet
    Dim dttTmp As New DataTable

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dsHlan, strDitta, strQuery, strQueryAnagen, bAncheAnagen, _
                                             bSoloSemplificata, strTipoConto, strTipoSottoconto, strCodPcon, _
                                             bModuloCRM, strAccVis, lCodorgaOperat, strRegvis, IsCrmUser, _
                                             strTipologia, bAbituali})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsHlan = CType(oIn(0), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      If strTipoConto <> "S" Then

        '------------------------------------------------
        'verifico e, se necessario, nella query includo gli attributi estesi di anagra
        If strQuery.IndexOf("anaext") > -1 Then
          'strSQL = "SELECT TOP 1 ax_conto FROM anaext" & _
          '  " WHERE codditt = " & CStrSQL(strDitta) & _
          '  " AND ax_tipork = " & CStrSQL(strTipoConto)
          'dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAEXT")
          'If dsTmp.Tables("ANAEXT").Rows.Count > 0 Then
          strTabAnagra = "anagra INNER JOIN anaext ON anagra.codditt = anaext.codditt AND anagra.an_conto = anaext.ax_conto AND anagra.an_tipo = anaext.ax_tipork"
          'End If
          'dsTmp.Tables.Clear()
          'dsTmp.Dispose()
        End If
        If (bModuloCRM = True) And (IsCrmUser = True) And (strTipoConto = "C") Then
          Dim dttRelease As New DataTable
          If GetBusRelease(dttRelease) = True Then
            If NTSCInt(dttRelease.Rows(0)!rel_maior) >= 15 Or (NTSCInt(dttRelease.Rows(0)!rel_maior) = 14 And NTSCStr(dttRelease.Rows(0)!rel_pers) = "a") Then
              bRelease15 = True
              strTipoJoin = " INNER"
            End If
          End If
          strTabAnagra += strTipoJoin & " JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto"
        End If

        'WebSoftService : mi serve NNCONTRA
        If strTipologia <> "" Then strTabAnagra += strTipoJoin & " JOIN nncontra on nnc_conto = an_conto and nncontra.codditt = anagra.codditt "

        '------------------------------------------------
        'query base 
        'non è possibile per i campi an_note, an_note2 perchè di tipo text
        strSQL = "SELECT" & _
           " an_conto, an_descr1, an_descr2, an_indir, an_cap, an_citta, an_prov, an_stato, an_codfis, an_pariva," & _
           " an_telef, an_faxtlx, an_valuta, an_codling, an_ultagg, an_destin, an_destpag, an_email, an_website, an_usaem," & _
           " an_opnome, an_webuid, an_webpwd, an_sesso, an_datnasc, an_citnasc, an_pronasc, an_stanasc, an_codfisest, an_cell," & _
           " an_titolo, an_persfg, an_profes, an_condom, an_tpsogiva, an_codcomu, an_destcorr, an_destsedel, an_destdomf, an_destresan," & _
           " an_siglaric, an_cognome, an_nome, an_codcomn, an_nazion1, an_nazion2, an_statofed, an_soggresi, an_omocodice, an_estcodiso," & _
           " an_estpariva, an_codrtac, an_contatt, ' ' as an_flci, 'N' as an_accperi " & _
           " FROM " & strTabAnagra & " WHERE anagra.codditt = " & CStrSQL(strDitta)
        'aggiungo la where dei campi con OR e AND rimappati
        TraduciWhere(strQuery, strSQL)

        dttTmp = OpenRecordset("SELECT TOP 1 * FROM esconti", CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          strSQL = strSQL & " AND an_conto NOT IN (SELECT es_conto FROM esconti " & _
                          " WHERE es_codpco = " & CStrSQL(strCodPcon) & _
                          " OR es_coddit = " & CStrSQL(strDitta) & ") "
        End If
        dttTmp.Clear()

        '------------------------------------------------
        'ulteriori filtri per CRM:
        'Se CRM, allora mette anche solo i clienti che sono nel potere di visibilit dell'utente...
        If (bModuloCRM = True) And (IsCrmUser = True) Then
          If strTipoConto = "C" Then
            If bRelease15 = True Then
              strSQL += " AND leads.le_codlead IN " & SubQueryFiltroLeadVis(strDitta)
            Else
              If (strAccVis <> "T") Then
                strSQL += " AND an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON " & _
                          " anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                          " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                          " AND leads.le_coddest = 0"
                Select Case strAccVis
                  Case "P" : strSQL += " AND le_opinc = " & lCodorgaOperat
                  Case "C" : strSQL += " AND le_opinc IN (" & strRegvis & ")"
                End Select
                strSQL += ")"
              End If
            End If
          Else
            'il blocco sui fornitori  gestito da BN__HLANFRM.VB in cmdRicerca_Click()
          End If    'If strTipoConto = "C" Then
        End If    'If bModuloCRM = True And bIsCRMUser = True Then

        '------------------------------------------------
        'query per aggiungere gli anagen non presenti in anagra
        If bAncheAnagen Then
          strSQL = strSQL & " UNION "
          strSQL = strSQL & "SELECT ag_codanag * -1 as an_conto, ag_descr1 as an_descr1, ag_descr2 as ag_descr2, ag_indir as ag_indir, ag_cap as ag_cap," & _
            " ag_citta as ag_citta, ag_prov as ag_prov, ag_stato as ag_stato, ag_codfis as ag_codfis, ag_pariva as ag_pariva," & _
            " ag_telef as ag_telef, ag_faxtlx as ag_faxtlx, ag_valuta as ag_valuta, ag_codling as ag_codling, ag_ultagg as ag_ultagg," & _
            " ag_destin as ag_destin, ag_destpag as ag_destpag, ag_email as ag_email, ag_website as ag_website, ag_usaem as ag_usaem," & _
            " ag_opnome as ag_opnome, ag_webuid as ag_webuid, ag_webpwd as ag_webpwd, ag_sesso as ag_sesso, ag_datnasc as ag_datnasc," & _
            " ag_citnasc as ag_citnasc, ag_pronasc as ag_pronasc, ag_stanasc as ag_stanasc, ag_codfisest as ag_codfisest, ag_cell as ag_cell," & _
            " ag_titolo as ag_titolo, ag_persfg as ag_persfg, ag_profes as ag_profes, ag_condom as ag_condom, ag_tpsogiva as ag_tpsogiva," & _
            " ag_codcomu as ag_codcomu, ag_destcorr as ag_destcorr, ag_destsedel as ag_destsedel, ag_destdomf as ag_destdomf, ag_destresan as ag_destresan," & _
            " ag_siglaric as ag_siglaric, ag_cognome as ag_cognome, ag_nome as ag_nome, ag_codcomn as ag_codcomn, ag_nazion1 as ag_nazion1," & _
            " ag_nazion2 as ag_nazion2, ag_statofed as ag_statofed, ag_soggresi as ag_soggresi, ag_omocodice as ag_omocodice, ag_estcodiso as ag_estcodiso," & _
            " ag_estpariva as ag_estpariva, ag_codrtac as ag_codrtac, ' ' as an_contatt, ' ' as an_flci, 'N' as an_accperi" & _
            " FROM anagen " & _
            " WHERE ag_codanag NOT IN (SELECT an_codanag FROM anagra WHERE an_codanag = ag_codanag and anagra.codditt = " & CStrSQL(strDitta) & " AND anagra.an_tipo = " & CStrSQL(strTipoConto) & ") "
          If strQueryAnagen <> "" Then strSQL = strSQL & " AND " & strQueryAnagen.Replace("§", " AND ")
        End If

      Else
        '------------------------------------------------
        'specifico per sottoconti
        strSQL = "SELECT an_conto, an_descr1, tb_desclas as an_descr2, tb_desmast as an_citta, ' ' as an_telef, " & _
            " ' ' as an_faxtlx, ' ' as an_pariva, ' ' as an_codfis, ' ' as an_contatt, an_flci, an_accperi " & _
            " FROM ((((TABPCON INNER JOIN TABGRUC ON tabpcon.tb_codpcon = tabgruc.tb_codpcon) " & _
            " INNER JOIN TABCLAS ON tabpcon.tb_codpcon = tabclas.tb_codpcon AND " & _
            " tabgruc.tb_codgruc = tabclas.tb_gruclas AND tabgruc.tb_codpcon = tabclas.tb_codpcon) " & _
            " INNER JOIN TABMAST ON tabpcon.tb_codpcon = tabmast.tb_codpcon AND " & _
            " tabclas.tb_codclas = tabmast.tb_clasmast AND tabclas.tb_codpcon = tabmast.tb_codpcon) " & _
            " INNER JOIN ANAGRA ON tabmast.tb_codmast = anagra.an_codmast AND " & _
            " tabmast.tb_codpcon = anagra.an_codpcon) INNER JOIN " & _
            " TABANAZ ON anagra.an_codpcon = tabanaz.tb_azcodpcon AND anagra.codditt = tabanaz.codditt " & _
            " WHERE anagra.codditt = " & CStrSQL(strDitta)

        'aggiungo la where dei campi con OR e AND rimappati
        TraduciWhere(strQuery, strSQL)


        If bSoloSemplificata Then strSQL = strSQL & " AND an_contrsemp <> '1' "

        dttTmp = OpenRecordset("SELECT TOP 1 * FROM esconti", CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          strSQL = strSQL & " AND an_conto NOT IN (SELECT es_conto FROM esconti " & _
                          " WHERE es_codpco = " & CStrSQL(strCodPcon) & _
                          " OR es_coddit = " & CStrSQL(strDitta) & ") "
        End If
        dttTmp.Clear()

        Select Case strTipoSottoconto
          Case "A" 'ATTIVITA'
            strSQL = strSQL & " AND CASE WHEN tabpcon.tb_struttura = 'B' AND tabmast.tb_sezbilm <> 0 THEN tabmast.tb_sezbilm ELSE tabgruc.tb_sezbil END = 1 AND (anagra.an_cksegno = '1' OR CASE WHEN anagra.an_cksegno = '0' THEN tabmast.tb_darave ELSE 'D' END = 'D') "
          Case "Q" 'PASSIVITA'
            strSQL = strSQL & " AND CASE WHEN tabpcon.tb_struttura = 'B' AND tabmast.tb_sezbilm <> 0 THEN tabmast.tb_sezbilm ELSE tabgruc.tb_sezbil END = 1 AND (anagra.an_cksegno = '2' OR CASE WHEN anagra.an_cksegno = '0' THEN tabmast.tb_darave ELSE 'A' END = 'A') "
          Case "K" 'COSTI
            strSQL = strSQL & " AND CASE WHEN tabpcon.tb_struttura = 'B' AND tabmast.tb_sezbilm <> 0 THEN tabmast.tb_sezbilm ELSE tabgruc.tb_sezbil END = 2 AND (anagra.an_cksegno = '1' OR CASE WHEN anagra.an_cksegno = '0' THEN tabmast.tb_darave ELSE 'D' END = 'D') "
          Case "R" 'RICAVI
            strSQL = strSQL & " AND CASE WHEN tabpcon.tb_struttura = 'B' AND tabmast.tb_sezbilm <> 0 THEN tabmast.tb_sezbilm ELSE tabgruc.tb_sezbil END = 2 AND (anagra.an_cksegno = '2' OR CASE WHEN anagra.an_cksegno = '0' THEN tabmast.tb_darave ELSE 'A' END = 'A') "
          Case "O" 'CONTI D'ORDINE
            strSQL = strSQL & " AND CASE WHEN tabpcon.tb_struttura = 'B' AND tabmast.tb_sezbilm <> 0 THEN tabmast.tb_sezbilm ELSE tabgruc.tb_sezbil END = 3 "
          Case "Z" 'CONTI RIEPILOGATIVI
            strSQL = strSQL & " AND CASE WHEN tabpcon.tb_struttura = 'B' AND tabmast.tb_sezbilm <> 0 THEN tabmast.tb_sezbilm ELSE tabgruc.tb_sezbil END = 4 "
          Case "P" 'CONTI PATRIMONIALI (ESCLUSI CLIENTI/FORNITORI)
            strSQL = strSQL & " AND CASE WHEN tabpcon.tb_struttura = 'B' AND tabmast.tb_sezbilm <> 0 THEN tabmast.tb_sezbilm ELSE tabgruc.tb_sezbil END = 1 "
          Case "E" 'CONTI ECONOMICI
            strSQL = strSQL & " AND CASE WHEN tabpcon.tb_struttura = 'B' AND tabmast.tb_sezbilm <> 0 THEN tabmast.tb_sezbilm ELSE tabgruc.tb_sezbil END = 2 "
        End Select

      End If    'If strTipoConto <> "S" Then


      'WebSoftService : valuto solo i contratti in corso e della tipologia passata come parametro [U/R]
      If strTipologia <> "" Then
        strSQL = strSQL & " and nnc_status = 'C'and nnc_tipoc = " & CStrSQL(strTipologia)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bAbituali = True Then
        strSQL += " AND an_conto IN (SELECT distinct TOP 50 tm_conto" & _
                                   " FROM testmag INNER JOIN anagra ON testmag.codditt = anagra.codditt" & _
                                   " AND testmag.tm_conto = anagra.an_conto" & _
                                   " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                                   " AND tm_datdoc BETWEEN " & CDataSQL(DateAdd(DateInterval.Month, -6, NTSCDate(Now.ToShortDateString))) & _
                                                     " AND " & CDataSQL(Now.ToShortDateString) & _
                                   " AND an_tipo = " & CStrSQL(strTipoConto) & ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = strSQL & " ORDER BY an_descr1"
      dsHlan = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAGRA")

      '-----------------------------
      'devo togliere i record con descrizione che inzia con il prefisso indicato nella opzione di regitro
      If oApp.oGvar.strZoomScartaDescrPrefix <> "" Then
        For Each dtrT As DataRow In dsHlan.Tables("ANAGRA").Rows
          If NTSCStr(dtrT!an_descr1).PadRight(oApp.oGvar.strZoomScartaDescrPrefix.Length).Substring(0, oApp.oGvar.strZoomScartaDescrPrefix.Length).ToUpper = oApp.oGvar.strZoomScartaDescrPrefix Then
            dtrT.Delete()
          End If
        Next
        dsHlan.AcceptChanges()
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetGestAnaext(ByVal strDitta As String, ByVal strTipoConto As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim bRes As Boolean = False
    Try
      strSQL = "SELECT TOP 1 ax_conto FROM anaext" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ax_tipork = " & CStrSQL(strTipoConto)

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAEXT")
      If dsTmp.Tables("ANAEXT").Rows.Count > 0 Then bRes = True
      dsTmp.Tables.Clear()

      Return bRes

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTabAext(ByVal strDitta As String, ByVal strTipoConto As String, ByRef dsTabaext As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim bRes As Boolean = False

    Try
      strSQL = "SELECT * FROM tabaext" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_tipork = " & CStrSQL(strTipoConto)

      dsTabaext = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABAEXT")
      If dsTabaext.Tables("TABAEXT").Rows.Count > 0 Then bRes = True

      Return bRes

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaPrimoMastroCF(ByVal strCodpCon As String, ByVal strTipoConto As String) As Integer
    Dim strSQL As String = ""
    Dim dsTmp As New DataSet
    Dim lResult As Integer = -1

    Try
      strSQL = "SELECT TOP 1 tb_codmast FROM tabmast " & _
                " WHERE tb_codpcon = " & CStrSQL(strCodpCon) & _
                " AND tb_tipomast = " & CStrSQL(strTipoConto) & _
                " ORDER BY tb_codmast ASC"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABMAST")
      If dsTmp.Tables("TABMAST").Rows.Count > 0 Then
        lResult = NTSCInt(dsTmp.Tables("TABMAST").Rows(0)!tb_codmast)
      End If
      dsTmp.Tables.Clear()

      Return lResult

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  'Public Overridable Function CreaAnagraDaAnagen(ByVal strDitta As String, ByVal lConto As Integer, ByVal lMastro As Integer, _
  '                                  ByVal lCodAnag As Integer, ByVal strTipoCF As String, ByVal bAggTabNuma As Boolean, _
  '                                  ByRef strErrorMessage As String) As Boolean
  '  Dim dbConn As DbConnection = Nothing
  '  Dim strSQL As String = ""
  '  Dim dsTmp As DataSet
  '  Dim strKpccee As String = ""
  '  Dim strKpccee2 As String = ""
  '  Dim strRifricd As String = ""
  '  Dim strRifrica As String = ""
  '  Dim lNum As Integer = 0
  '  Dim lProgr As Integer = 0

  '  Dim strGestPartite As String = "S"
  '  Dim strGestScaden As String = "S"
  '  Dim strCodpcon As String = ""

  '  Dim bGestAnaExt As Boolean = False
  '  Dim strComboC1 As String = " "
  '  Dim strComboC2 As String = " "
  '  Dim strComboC3 As String = " "
  '  Dim i As Integer

  '  Try
  '    '---------------------------------
  '    'cerco i dati in tabanaz
  '    strSQL = "SELECT tb_azcodpcon, tb_ventil, tb_azgestscad FROM tabanaz WHERE codditt = " & CStrSQL(strDitta)
  '    dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")
  '    If dsTmp.Tables("TABANAZ").Rows.Count > 0 Then
  '      strCodpcon = dsTmp.Tables("TABANAZ").Rows(0)!tb_azcodpcon.ToString
  '      Select Case dsTmp.Tables("TABANAZ").Rows(0)!tb_ventil.ToString
  '        Case "N" : strGestPartite = "N"
  '        Case "S" : strGestPartite = "S"
  '        Case "C" : If strTipoCF = "C" Then strGestPartite = "S" Else strGestPartite = "N"
  '        Case "F" : If strTipoCF = "F" Then strGestPartite = "S" Else strGestPartite = "N"
  '      End Select
  '      Select Case dsTmp.Tables("TABANAZ").Rows(0)!tb_azgestscad.ToString
  '        Case "N" : strGestScaden = "N"
  '        Case "S" : strGestScaden = "S"
  '        Case "C" : If strTipoCF = "C" Then strGestScaden = "S" Else strGestScaden = "N"
  '        Case "F" : If strTipoCF = "F" Then strGestScaden = "S" Else strGestScaden = "N"
  '      End Select
  '    End If
  '    dsTmp.Tables.Clear()

  '    '---------------------------------
  '    'Cerca i riferimenti ai conti per il bilancio riclassificato ed il bilancio C.E.E.
  '    strSQL = " SELECT tb_rifceed, tb_rifceea, tb_rifricd, tb_rifrica" & _
  '             " FROM tabmast" & _
  '             " WHERE tb_codpcon = " & CStrSQL(strCodpcon) & _
  '             " AND tb_codmast = " & CDblSQL(lMastro.ToString)
  '    dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABMAST")
  '    If dsTmp.Tables("TABMAST").Rows.Count > 0 Then
  '      strKpccee = dsTmp.Tables("TABMAST").Rows(0)!tb_rifceed.ToString
  '      strKpccee2 = dsTmp.Tables("TABMAST").Rows(0)!tb_rifceea.ToString
  '      strRifricd = dsTmp.Tables("TABMAST").Rows(0)!tb_rifricd.ToString
  '      strRifrica = dsTmp.Tables("TABMAST").Rows(0)!tb_rifrica.ToString
  '    End If
  '    dsTmp.Tables.Clear()

  '    '---------------------------------
  '    'verifico se gestire le anagrafiche estese
  '    bGestAnaExt = False
  '    strSQL = "SELECT * FROM tabaext" & _
  '        " WHERE codditt = " & CStrSQL(strDitta) & _
  '        " AND tb_tipork = " & CStrSQL(strTipoCF)
  '    dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABAEXT")
  '    If dsTmp.Tables("TABAEXT").Rows.Count > 0 Then
  '      bGestAnaExt = True
  '      If dsTmp.Tables("TABAEXT").Rows(0)!tb_combo1.ToString <> "" Then
  '        For i = 65 To 76
  '          If (i <> 74) And (i <> 75) Then
  '            If dsTmp.Tables("TABAEXT").Rows(0)("tb_helpcom1_" & Chr(i)).ToString <> "" Then
  '              strComboC1 = Chr(i)
  '              Exit For
  '            End If
  '          End If
  '        Next i
  '        For i = 65 To 69
  '          If dsTmp.Tables("TABAEXT").Rows(0)("tb_helpcom2_" & Chr(i)).ToString <> "" Then
  '            strComboC2 = Chr(i)
  '            Exit For
  '          End If
  '        Next i
  '        For i = 65 To 86
  '          If (i <> 74) And (i <> 75) Then
  '            If dsTmp.Tables("TABAEXT").Rows(0)("tb_helpcom3_" & Chr(i)).ToString <> "" Then
  '              strComboC3 = Chr(i)
  '              Exit For
  '            End If
  '          End If
  '        Next i
  '      End If
  '    End If
  '    dsTmp.Tables.Clear()

  '    '---------------------------------
  '    'apro il database e la transazione
  '    dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
  '    ApriTrans(dbConn)

  '    '---------------------------------
  '    'Aggiorna il progressivo in TABNUMA se TABINST.tb_opz_2 = blank
  '    If bAggTabNuma Then
  '      lProgr = NTSCInt(lConto.ToString.Substring(lMastro.ToString.Length))
  '      lNum = AggNuma(strDitta, IIf(strTipoCF = "C", "CC", "FF").ToString, " ", lMastro, lProgr, True, False, strErrorMessage, dbConn)
  '      If strErrorMessage <> "" Then
  '        If IsInTrans Then AnnullaTrans()
  '        Return False
  '      End If
  '      '---------------------------------------------------------------------------------------
  '      '--- Se la numerazione in TABNUMA  gi stata utilizzata annulla la transazione ed esce
  '      If lNum <> lProgr Then
  '        If IsInTrans Then AnnullaTrans()
  '        strErrorMessage = oApp.Tr(Me, 127792274055625000, "Sottoconto già inserito in Anagrafica Clienti/Fornitori." & vbCrLf & _
  '                          "Operazione annullata.")
  '        Return False
  '      End If
  '    End If

  '    '-----------------------------------
  '    'Inserisce il record da ANAGEN in ANAGRA
  '    strSQL = "INSERT INTO anagra (codditt, an_conto, an_tipo, an_descr1, an_descr2, an_indir," & _
  '      " an_cap, an_citta, an_prov, an_codfis, an_pariva, an_controp, an_alleg, an_persf," & _
  '      " an_partite, an_telef, an_faxtlx, an_contatt, an_ultagg, an_zona, an_categ, an_codese," & _
  '      " an_codpag, an_scont1, an_scont2, an_agente, an_banc1, an_banc2, an_abi, an_cab," & _
  '      " an_rifriba, an_spinc, an_bolli, an_numdic, an_datdic, an_listino, an_vuoti, an_valuta," & _
  '      " an_claprov, an_clascon, an_note, an_porto, an_vett, an_fatt, an_fido, an_destin," & _
  '      " an_scaden, an_dtaper, an_dummy, an_stato, an_agente2, an_kpccee2, an_kpccee, an_flci," & _
  '      " an_unmis, an_accperi, an_codmast, an_note2, an_blocco, an_gcons, an_email, an_website," & _
  '      " an_rifricd, an_rifrica, an_usaem, an_codling, an_rating, an_codbanc, an_agcontrop," & _
  '      " an_privacy, an_codntra, an_status, an_codcana, an_codtpbf, an_perfatt, an_contfatt," & _
  '      " an_codnscol, an_mesees1, an_mesees2, an_giofiss, an_destpag, an_vett2, an_numdicp," & _
  '      " an_datdicp, an_scaddic, an_maxdic, an_opnome, an_gescon , an_perescon, an_webuid," & _
  '      " an_webpwd, an_codanag, an_codpcon, an_pcconto, an_persfg, an_profes, an_condom," & _
  '      " an_tpsogiva, an_codcomu, an_destcorr, an_destsedel, an_destdomf, an_destresan," & _
  '      " an_siglaric, an_cognome, an_nome, an_codcomn, an_nazion1, an_nazion2, an_cell," & _
  '      " an_titolo, an_statofed, an_funzion, an_sesso, an_datnasc, an_citnasc, an_pronasc," & _
  '      " an_stanasc, an_codfisest, an_codrtac, an_tipacq, an_cksegno, an_conprof, an_totcron," & _
  '      " an_contrsemp, an_manrip, an_percman, an_colbil, an_voceirap, an_varirap, an_pervari," & _
  '      " an_cosvend, an_indiidd, an_azcom, an_ricmimp, an_ricmpro, an_stseimp, an_stsepro," & _
  '      " an_soggresi, an_omocodice, an_estcodiso, an_estpariva, an_sosppr, an_datini, an_datfin)"
  '    strSQL = strSQL & " SELECT " & CStrSQL(strDitta) & ", " & CDblSQL(lConto.ToString) & ", " & _
  '      CStrSQL(strTipoCF) & ", ag_descr1, ag_descr2, ag_indir, ag_cap," & _
  '      " ag_citta, ag_prov, ag_codfis, ag_pariva, 0, 'N', 'N', " & _
  '      CStrSQL(strGestPartite) & ", ag_telef, ag_faxtlx, Null, " & _
  '      CDataOraSQL(Now.ToString) & ", 0, 0, 0, 0, 0, 0, 0, Null, Null, 0, 0, Null, 'N'," & _
  '      " 'N', Null, Null, 0, 'N', ag_valuta, 0, 0, ag_note, Null, 0, 'R', 0, ag_destin, " & _
  '      CStrSQL(strGestScaden) & ", " & CDataSQL(Now.ToShortDateString) & ", Null," & _
  '      " ag_stato, 0, " & CStrSQL(strKpccee2) & ", " & CStrSQL(strKpccee) & ", ' ', Null, 'N', " & _
  '      CDblSQL(lMastro.ToString) & ", ag_note2, 'N', 8," & _
  '      " ag_email, ag_website, " & CStrSQL(strRifricd) & ", " & CStrSQL(strRifrica) & _
  '      ", ag_usaem, ag_codling, Null, 0, 0, ' ', 0, 'A'," & _
  '      " 0, 0, ' ', 0, Null, 0, 0, 0, ag_destpag, 0, Null, Null, Null, 0, " & _
  '      CStrSQL(oApp.User.Nome) & ", 'N', 0, ag_webuid, ag_webpwd, " & lCodAnag & ", " & _
  '      CStrSQL(strCodpcon) & ", 0, ag_persfg, ag_profes, ag_condom, ag_tpsogiva," & _
  '      " ag_codcomu, ag_destcorr, ag_destsedel, ag_destdomf, ag_destresan, ag_siglaric," & _
  '      " ag_cognome, ag_nome, ag_codcomn, ag_nazion1, ag_nazion2, ag_cell, ag_titolo," & _
  '      " ag_statofed, 0, ag_sesso, ag_datnasc, ag_citnasc, ag_pronasc, ag_stanasc," & _
  '      " ag_codfisest, ag_codrtac, ' ', '0', '0', 0, '0', '0', 0, '0', 0, '0', 0, '0'," & _
  '      " 0, 0, 0, 0, 0, 0, ag_soggresi, ag_omocodice, ag_estcodiso, ag_estpariva, 'N', " & _
  '      CDataSQL(IntSetDate("01/01/1900")) & ", " & _
  '      CDataSQL(IntSetDate("31/12/2099")) & _
  '      " FROM anagen" & _
  '      " WHERE ag_codanag = " & lCodAnag
  '    Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

  '    '-----------------------------------
  '    'inserisco i record di ANAEXT
  '    If bGestAnaExt Then
  '      strSQL = "INSERT INTO TTANAEXT (codditt, ax_conto, ax_coddest, ax_codlead, ax_tipork," & _
  '        " ax_tipo1, ax_tipo2, ax_tipo3, ax_descr1, ax_descr2, ax_descr3, ax_descr4, ax_descr5," & _
  '        " ax_descr6, ax_descr7, ax_descr8, ax_descr9, ax_descr10, ax_desext1, ax_desext2," & _
  '        " ax_desext3, ax_memo1, ax_memo2, ax_data1, ax_data2, ax_data3, ax_data4, ax_data5," & _
  '        " ax_num1, ax_num2, ax_num3, ax_num4, ax_num5, ax_num6, ax_num7, ax_num8, ax_num9," & _
  '        " ax_num10, ax_check1, ax_check2, ax_check3, ax_check4, ax_check5, ax_check6," & _
  '        " ax_check7, ax_check8, ax_check9, ax_check10, ax_combo1, ax_combo2, ax_combo3," & _
  '        " ax_ultagg, ax_opnome)" & _
  '        " VALUES (" & CStrSQL(strDitta) & ", " & CDblSQL(lConto.ToString) & "," & _
  '        " 0, 0, " & CStrSQL(strTipoCF) & ", ' ', ' ', ' ', ' ', ' ', ' ', ' '," & _
  '        " ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '," & _
  '        " NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
  '        " 'N', 'N', 'N', 'N', 'N', 'N', 'N', 'N', 'N', 'N'," & _
  '        CStrSQL(strComboC1) & " , " & CStrSQL(strComboC2) & ", " & _
  '        CStrSQL(strComboC3) & ", " & CDataOraSQL(Now.ToString) & ", " & CStrSQL(oApp.User.Nome) & ")"
  '      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
  '    End If

  '    '-----------------------------------
  '    'Inserisce i record da DESTGEN in destdiv
  '    strSQL = "INSERT INTO destdiv (codditt, dd_conto, dd_coddest, dd_nomdest, dd_inddest," & _
  '      " dd_capdest, dd_locdest, dd_prodest, dd_turno, dd_telef, dd_codzona, dd_codfis," & _
  '      " dd_pariva, dd_faxtlx, dd_agente, dd_agente2, dd_email, dd_usaem, dd_vett, dd_vett2," & _
  '      " dd_nomdest2, dd_stato, dd_note, dd_codcomu, dd_codfisest, dd_statofed)" & _
  '      " SELECT " & CStrSQL(strDitta) & ", " & CDblSQL(lConto.ToString) & ", dd_coddest, dd_nomdest," & _
  '      " dd_inddest, dd_capdest, dd_locdest, dd_prodest, dd_turno, dd_telef, 0, dd_codfis," & _
  '      " dd_pariva, dd_faxtlx, 0, 0, dd_email, dd_usaem, 0, 0, dd_nomdest2, dd_stato, Null," & _
  '      " dd_codcomu, dd_codfisest, dd_statofed FROM destgen" & _
  '      " WHERE dd_codanag = " & CDblSQL(lCodAnag.ToString)
  '    Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

  '    '---------------------------------
  '    'Inserisce i record da MASTRIC in ANATRIC
  '    strSQL = "INSERT INTO ANATRIC (codditt, ant_conto, ant_codtric, ant_kpcrd, ant_kpcra)" & _
  '      " SELECT " & CStrSQL(strDitta) & ", " & CDblSQL(lConto.ToString) & "," & _
  '      " mst_codtric, mst_kpcrd, mst_kpcra " & _
  '      " FROM mastric" & _
  '      " WHERE mst_codpcon = " & CStrSQL(strCodpcon) & _
  '      " AND mst_codmast = " & CDblSQL(lMastro.ToString)
  '    Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

  '    '----------------------------------
  '    'chiudo la transazione ed il database
  '    ChiudiTrans()
  '    dbConn.Close()

  '    Return True

  '  Catch ex As Exception
  '    '--------------------------------------------------------------
  '    'se sono in transazione la annullo
  '    If IsInTrans Then AnnullaTrans()

  '    '--------------------------------------------------------------
  '    'non eseguo la gestione errori standard ma giro l'errore 
  '    'direttamente al componente entity che mi ha chiamato
  '    Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
  '    '--------------------------------------------------------------
  '  End Try
  'End Function
End Class
