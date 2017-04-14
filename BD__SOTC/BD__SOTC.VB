Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLD__SOTC
  Inherits CLD__BASE

  Public Overridable Function GetData(ByVal strDitta As String, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      '---------------------------------
      'cancello eventuali conto con an_conto = 0
      strSQL = "DELETE FROM anagra" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND an_tipo = 'S'" & _
                " AND an_conto = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------------------
      strSQL = "SELECT anagra.*, '' as xx_codmast, '' as xx_voceirap, '' as xx_controp, '' as xx_funzion, " & _
               " '' as xx_pcconto, '' as xx_valuta, '' as xx_codvfde, 0 as xx_codcovp, '' as xx_codcovpdescr" & _
               " FROM anagra WHERE an_tipo = 'S' AND codditt = " & CStrSQL(strDitta) & _
               " ORDER BY an_conto"

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAGRA")
      dsOut.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function ContoMovimentato(ByVal strDitta As String, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 * FROM prinot WHERE codditt = " & CStrSQL(strDitta) & _
            " AND pn_conto = " & lConto.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function ValidaNuovoContoAnagra(ByVal strPdc As String, ByVal lConto As Integer) As Boolean
    '---------------------------
    'verifica se il nuovo conto è già presente in anagra: se lo è ritorna false 
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 an_conto FROM anagra" & _
              " WHERE an_conto = " & lConto.ToString & _
              " AND an_tipo = 'S'" & _
              " AND an_codpcon = " & CStrSQL(strPdc)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return False
      End If
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SaveData(ByRef dtrIn As DataRow, ByVal bNuovoContoProposto As Boolean, ByVal strOpz_2 As String) As Boolean
    'strOpz_2 = numerazione conti automatica
    '------------------------------------
    'eseguo il salvataggio : tutto in transazione
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim lProgG As Integer = 0
    Dim dttTmp As New DataTable
    Dim nRes As Integer = 0
    Dim nCodiceConto As Integer = 0

    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      '----------------------------------
      'aggiorno ANAG (se non è un sottoconto PDC, diversamente questi due dati sono già stati salvati correttamente in SaveDataPDC
      If NTSCInt(dtrIn!an_pcconto) = 0 Then
        dtrIn!an_opnome = oApp.User.Nome
        dtrIn!an_ultagg = DateTime.Now
      End If

      If dtrIn.RowState = DataRowState.Added And NTSCInt(dtrIn!an_pcconto) = 0 Then
        strSQL = "INSERT INTO anagra " & GetQueryInsertField(dtrIn.Table, "an_") & " VALUES "
        strSQL += GetQueryInsertValue(dtrIn.Table, dtrIn, "an_")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Else
        strSQL = "UPDATE anagra SET " & GetQueryUpdate(dtrIn.Table, dtrIn, "an_") & _
                 " WHERE codditt = " & CStrSQL(dtrIn!codditt.ToString) & _
                 " AND an_conto = " & NTSCInt(dtrIn!an_conto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------
        'non aggiorno mai anagpc e le altre anagra collegate ad anagpc, visto che
        'i campi collegati ad anagpc non possono essere modificati se non da BN__ANPC
      End If     'If dtrIn.RowState = DataRowState.Added Then

      '----------------------------------
      'se il conto non è gestito a ca cancello analink e anacent
      'il conto poteva essere gestito a ca in ingresso form ...
      If NTSCStr(dtrIn!an_flci).Trim = "" Then
        strSQL = "DELETE FROM analink WHERE codditt = " & CStrSQL(dtrIn!codditt.ToString) & " AND anl_anconto = " & NTSCInt(dtrIn!an_conto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "DELETE FROM anacent WHERE codditt = " & CStrSQL(dtrIn!codditt.ToString) & " AND anc_conto = " & NTSCInt(dtrIn!an_conto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      dtrIn.AcceptChanges()

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
  Public Overridable Function SaveDataPDC(ByRef dtrIn As DataRow, ByVal lCovpOld As Integer, ByVal bCreaCovp As Boolean, _
                                          ByVal bNuovoContoProposto As Boolean, ByRef strErr As String) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim lProgG As Integer = 0
    Dim dttTmp As New DataTable
    Dim nRes As Integer = 0
    Dim lContoNoMastro As Integer = 0
    Dim dttPDC As New DataTable
    Dim i As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      strErr = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiorno TABCOVP
      '--------------------------------------------------------------------------------------------------------------
      If lCovpOld <> 0 Then
        strSQL = "DELETE FROM tabcovp" & _
          " WHERE tb_codcovp = " & lCovpOld & _
          " AND tb_codpcon = " & CStrSQL(dtrIn!an_codpcon.ToString) & _
          " AND tb_concove = " & dtrIn!an_pcconto.ToString
        nRes = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      If NTSCInt(dtrIn!xx_codcovp.ToString) <> 0 Then
        strSQL = "DELETE FROM tabcovp" & _
          " WHERE tb_codcovp = " & NTSCInt(dtrIn!xx_codcovp.ToString).ToString & _
          " AND tb_codpcon = " & CStrSQL(dtrIn!an_codpcon.ToString) & _
          " AND tb_concove = " & dtrIn!an_pcconto.ToString
        nRes = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Non ho indicato il codice contropartita: creo un record di tabcovg ed uno di tabcovp
      '--------------------------------------------------------------------------------------------------------------
      If bCreaCovp = True Then
        If NTSCInt(dtrIn!xx_codcovp.ToString) = 0 Then
          lProgG = 5001
          strSQL = "SELECT max(tb_codcovg) as PROG FROM tabcovg"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count = 0 Then
            lProgG = 5001
          Else
            lProgG = NTSCInt(dttTmp.Rows(0)!PROG) + 1
            If lProgG < 5001 Then lProgG = 5001
          End If
          dttTmp.Clear()
          dttTmp.Dispose()
          If lProgG <= 9999 Then
            strSQL = "INSERT INTO tabcovg (tb_codcovg, tb_descovg) VALUES (" & lProgG & ", " & CStrSQL(dtrIn!an_descr1.ToString) & ")"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Else
            lProgG = 0
            strErr = oApp.Tr(Me, 128544694433623894, "Attenzione: non è stato possibile creare la contropartita: è già stato raggiunto il numero più alto di contropartita utilizzabile (9999)")
          End If
        Else
          lProgG = NTSCInt(dtrIn!xx_codcovp.ToString)
        End If
      Else
        '------------------------------------------------------------------------------------------------------------
        '--- Aggiorno eventualmente il codice inserito a mano
        '------------------------------------------------------------------------------------------------------------
        lProgG = NTSCInt(dtrIn!xx_codcovp.ToString)
      End If    'If bCreaCovp Then
      '--------------------------------------------------------------------------------------------------------------
      If lProgG <> 0 Then
        strSQL = "UPDATE tabcovg" & _
          " SET tb_descovg = " & CStrSQL(dtrIn!an_descr1.ToString) & _
          " WHERE tb_codcovg = " & lProgG.ToString
        nRes = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "UPDATE tabcovp" & _
          " SET tb_descovp = " & CStrSQL(dtrIn!an_descr1.ToString) & "," & _
          " tb_concove = " & dtrIn!an_pcconto.ToString & _
          " WHERE tb_codcovp = " & lProgG.ToString & _
          " AND tb_codpcon = " & CStrSQL(dtrIn!an_codpcon.ToString)
        nRes = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If nRes = 0 Then
          strSQL = "INSERT INTO tabcovp (tb_codpcon, tb_codcovp, tb_descovp, tb_concove, tb_concova)" & _
            " VALUES (" & CStrSQL(dtrIn!an_codpcon.ToString) & ", " & lProgG.ToString & ", " & _
            CStrSQL(dtrIn!an_descr1.ToString) & ", " & NTSCInt(dtrIn!an_pcconto).ToString & ", 0)"
          nRes = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiorno ANAGPC
      '--------------------------------------------------------------------------------------------------------------
      dtrIn!an_opnome = oApp.User.Nome
      dtrIn!an_ultagg = DateTime.Now

      'copio i dati in un datatable copia di ANAGPC per poter eseguire le query di insert o update
      GetTableStructure("ANAGPC", False, dttPDC)
      SetTableDefaultValueFromDB("ANAGPC", dttPDC.DataSet)
      dttPDC.Rows.Add(dttPDC.NewRow)
      For i = 0 To dttPDC.Columns.Count - 1
        If dttPDC.Columns(i).ColumnName <> "ts" Then
          dttPDC.Rows(0)(dttPDC.Columns(i).ColumnName) = dtrIn(dttPDC.Columns(i).ColumnName.Replace("pc_", "an_"))
        End If
      Next

      '--------------------------------------------------------------------------------------------------------------
      If dtrIn.RowState = DataRowState.Added Then
        strSQL = "INSERT INTO anagpc " & GetQueryInsertField(dttPDC, "pc_") & " VALUES "
        strSQL += GetQueryInsertValue(dttPDC, dttPDC.Rows(0), "pc_")
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
        '--- Aggiungo in anagra
        '------------------------------------------------------------------------------------------------------------
        Dim strRating As String = "2"
        strRating = GetSettingBus("BSTXIEDF", "OPZIONI", ".", "DefaultRating", "2", " ", "2")
        Select Case strRating
          Case "1"
          Case " "
          Case "3"
          Case "4"
          Case Else
            strRating = "2"
        End Select

        strSQL = "INSERT INTO anagra (codditt, an_conto, an_tipo, an_descr1, an_descr2, an_indir, an_cap, an_citta," & _
          " an_prov, an_codfis, an_pariva, an_controp, an_alleg, an_persf, an_partite, an_telef, an_faxtlx," & _
          " an_contatt, an_ultagg, an_zona, an_categ, an_codese, an_codpag, an_scont1, an_scont2, an_agente," & _
          " an_banc1, an_banc2, an_abi, an_cab, an_rifriba, an_spinc, an_bolli, an_numdic, an_datdic, an_listino," & _
          " an_vuoti, an_valuta, an_claprov, an_clascon, an_note, an_porto, an_vett, an_fatt, an_fido, an_destin," & _
          " an_scaden, an_dtaper, an_dummy, an_stato, an_agente2, an_kpccee2, an_kpccee, an_flci, an_unmis," & _
          " an_accperi, an_codmast, an_note2, an_blocco, an_gcons, an_email, an_website, an_rifricd, an_rifrica," & _
          " an_usaem, an_codling, an_rating, an_codbanc, an_agcontrop, an_privacy, an_codntra, an_status," & _
          " an_codcana, an_codtpbf, an_perfatt, an_contfatt, an_codnscol, an_mesees1, an_mesees2, an_giofiss," & _
          " an_destpag, an_vett2, an_numdicp, an_datdicp, an_scaddic, an_maxdic, an_opnome, an_gescon, an_perescon," & _
          " an_webuid, an_webpwd, an_codanag, an_codpcon, an_pcconto, an_persfg, an_profes, an_condom, an_tpsogiva," & _
          " an_codcomu, an_destcorr, an_destsedel, an_destdomf, an_destresan, an_siglaric, an_cognome, an_nome," & _
          " an_codcomn, an_nazion1, an_nazion2, an_cell, an_titolo, an_statofed, an_funzion, an_sesso, an_datnasc," & _
          " an_citnasc, an_pronasc, an_stanasc, an_codfisest, an_codrtac, an_tipacq, an_cksegno, an_conprof," & _
          " an_totcron, an_contrsemp, an_manrip, an_percman, an_colbil, an_voceirap, an_varirap, an_pervari," & _
          " an_cosvend, an_indiidd, an_azcom, an_ricmimp, an_ricmpro, an_stseimp, an_stsepro, an_soggresi," & _
          " an_omocodice, an_estcodiso, an_estpariva, an_sosppr, an_intragr, an_indirap, an_indiiddsit, an_datini," & _
          " an_datfin, an_opzgest, an_libstr1, an_libint1, an_trating)" & _
          " SELECT tabanaz.codditt, pc_conto, pc_tipo, pc_descr1, pc_descr2, NULL, NULL, NULL, NULL, NULL, NULL," & _
          " pc_controp, 'N', 'N', pc_partite, NULL, NULL, NULL, pc_ultagg, 0, 0, 0, 0, 0, 0, 0, NULL, NULL, 0, 0," & _
          " NULL, 'N', 'N', NULL, NULL, 0, 'N', 0, 0, 0, pc_note,  NULL, 0, 'R', 0, 0, pc_scaden, pc_dtaper," & _
          " NULL, NULL, 0, pc_kpccee2, pc_kpccee, pc_flci, NULL, pc_accperi, pc_codmast, pc_note2, 'N', 8," & _
          " NULL, NULL, pc_rifricd, pc_rifrica, 'N', 0, NULL, 0, 0, ' ', 0, 'A', 0, 0, ' ', 0, NULL, 0, 0, 0, 0, 0," & _
          " NULL, NULL, NULL, 0, pc_opnome, 'N', 0, NULL, NULL, 0, pc_codpcon, pc_conto, 'F', 'N', 'N', 'N', NULL," & _
          " 0, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, pc_funzion, 'M', NULL, NULL, NULL," & _
          " NULL, NULL, 0, pc_tipacq, pc_cksegno, pc_conprof, pc_totcron, pc_contrsemp, pc_manrip, pc_percman," & _
          " pc_colbil, pc_voceirap, pc_varirap, pc_pervari, pc_cosvend, pc_indiidd, pc_azcom, pc_ricmimp," & _
          " pc_ricmpro, pc_stseimp, pc_stsepro, 'S', 'N', NULL, NULL, pc_sosppr, pc_intragr, pc_indirap," & _
          " pc_indiiddsit, pc_datini, pc_datfin, pc_opzgest, NULL, 0, " & CStrSQL(strRating) & _
          " FROM (anagpc INNER JOIN tabanaz ON anagpc.pc_codpcon = tabanaz.tb_azcodpcon)" & _
          " LEFT JOIN anagra ON anagra.an_codpcon = anagpc.pc_codpcon AND anagra.an_pcconto = anagpc.pc_conto" & _
          " WHERE pc_codpcon = " & CStrSQL(dtrIn!an_codpcon.ToString) & _
          " AND pc_conto = " & NTSCInt(dtrIn!an_pcconto).ToString & _
          " AND an_conto is null"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
      Else
        '------------------------------------------------------------------------------------------------------------
        If NTSCStr(dtrIn!an_fldespdc).ToUpper <> "S" Then
          'non devo aggiornare descr1 e descr2
          dttPDC.Columns.Remove("pc_descr1")
          dttPDC.Columns.Remove("pc_descr2")
        End If
        strSQL = "UPDATE anagpc" & _
                " SET " & GetQueryUpdate(dttPDC, dttPDC.Rows(0), "pc_") & _
                " WHERE pc_codpcon = " & CStrSQL(dtrIn!an_codpcon.ToString) & _
                " AND pc_conto = " & NTSCInt(dtrIn!an_pcconto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
        '--- Aggiorno ANAGRA
        '------------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE anagra" & _
          " SET an_controp = pc_controp," & _
          " an_partite = pc_partite," & _
          " an_scaden = pc_scaden," & _
          " an_opnome = pc_opnome," & _
          " an_ultagg = pc_ultagg," & _
          " an_flci = pc_flci," & _
          " an_accperi = pc_accperi," & _
          " an_note = pc_note," & _
          " an_note2 = pc_note2," & _
          " an_kpccee = pc_kpccee," & _
          " an_kpccee2 = pc_kpccee2," & _
          " an_rifricd = pc_rifricd," & _
          " an_rifrica = pc_rifrica," & _
          " an_funzion = pc_funzion," & _
          " an_tipacq = pc_tipacq," & _
          " an_cksegno = pc_cksegno," & _
          " an_conprof = pc_conprof," & _
          " an_totcron = pc_totcron," & _
          " an_contrsemp = pc_contrsemp," & _
          " an_manrip = pc_manrip," & _
          " an_percman = pc_percman," & _
          " an_colbil = pc_colbil," & _
          " an_voceirap = pc_voceirap," & _
          " an_varirap = pc_varirap," & _
          " an_pervari = pc_pervari," & _
          " an_cosvend = pc_cosvend," & _
          " an_indiidd = pc_indiidd," & _
          " an_azcom = 0," & _
          " an_ricmimp = pc_ricmimp," & _
          " an_ricmpro = pc_ricmpro," & _
          " an_stseimp = pc_stseimp," & _
          " an_stsepro = pc_stsepro," & _
          " an_sosppr = pc_sosppr," & _
          " an_intragr = pc_intragr," & _
          " an_indirap = pc_indirap," & _
          " an_indiiddsit = pc_indiiddsit," & _
          " an_datini = pc_datini," & _
          " an_datfin = pc_datfin," & _
          " an_opzgest = pc_opzgest," & _
          " an_libstr1 = NULL," & _
          " an_libint1 = 0"
        strSQL += " FROM anagra INNER JOIN anagpc ON anagra.an_codpcon = anagpc.pc_codpcon AND anagra.an_pcconto = anagpc.pc_conto" & _
          " WHERE pc_codpcon = " & CStrSQL(dtrIn!an_codpcon.ToString) & _
          " AND pc_conto <> 0" & _
          " AND pc_conto = " & NTSCInt(dtrIn!an_pcconto).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "UPDATE anagra" & _
          " SET an_descr1 = pc_descr1," & _
          " an_descr2 = pc_descr2" & _
          " FROM anagra INNER JOIN anagpc ON anagra.an_codpcon = anagpc.pc_codpcon AND anagra.an_pcconto = anagpc.pc_conto" & _
          " WHERE pc_codpcon = " & CStrSQL(dtrIn!an_codpcon.ToString) & _
          " AND pc_conto <> 0" & _
          " AND pc_conto = " & NTSCInt(dtrIn!an_pcconto).ToString & _
          " AND an_fldespdc = 'S'"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If     'If dtrIn.RowState = DataRowState.Added Then
      '------------------------------------------------------------------------------------------------------------
      '--- Chiudo la transazione ed il database
      '------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '------------------------------------------------------------------------------------------------------------
      If lProgG <> 0 Then
        dtrIn!xx_codcovp = lProgG
        dtrIn!xx_codcovpdescr = dtrIn!an_descr1.ToString
      Else
        dtrIn!xx_codcovpdescr = ""
      End If
      dtrIn.AcceptChanges()
      '------------------------------------------------------------------------------------------------------------
      '--- Visto che sul database non posso avere più di una contropartita ditta per sottoconto, 
      '--- devo rimuovere dal datatable in memoria il sottoconto da qutti i record con conto diverso da 
      '--- quello in salvataggio sul database il dato è già stato sistemato con le query sopra
      '------------------------------------------------------------------------------------------------------------
      If lProgG <> 0 Then
        For Each dtrT1 As DataRow In dtrIn.Table.Select("xx_codcovp = " & lProgG.ToString & " AND an_pcconto <> " & dtrIn!an_pcconto.ToString)
          dtrT1!xx_codcovp = 0
          dtrT1!xx_codcovpdescr = ""
          dtrT1.AcceptChanges()
        Next
      End If
      '------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      '------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '------------------------------------------------------------------------------------------------------------
    Finally
      dttPDC.Clear()
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function DeleteData(ByVal strDitta As String, ByRef dtrIn As DataRow) As Boolean
    '------------------------------------
    'eseguo il salvataggio : tutto in transazione
    Dim strSQL As String = ""
    'Dim dttTmp As New DataTable
    Dim dbConn As DbConnection = Nothing
    Dim bCancellabile As Boolean = False

    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      'collegamenti con anagca
      strSQL = "DELETE FROM analink WHERE codditt = " & CStrSQL(strDitta) & " AND anl_anconto = " & NTSCInt(dtrIn!an_conto).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'ripartizioni CA
      strSQL = "DELETE FROM anacent WHERE codditt = " & CStrSQL(strDitta) & " AND anc_conto = " & NTSCInt(dtrIn!an_conto).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'riclassificati
      strSQL = "DELETE FROM anatric WHERE codditt = " & CStrSQL(strDitta) & " AND ant_conto = " & NTSCInt(dtrIn!an_conto).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM anagra WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & NTSCInt(dtrIn!an_conto).ToString
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
  Public Overridable Function DeleteDataPDC(ByRef dtrIn As DataRow, ByVal lCovpOld As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dbConn As DbConnection = Nothing
    Dim bCancellabile As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Trovo le ditte con il PDC uguale a quello in analisi,
      '--- ed in ognuna verifico se il conto da cancellare è stato utilizzato
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT codditt FROM tabanaz" & _
        " WHERE tb_azcodpcon = " & CStrSQL(dtrIn!an_codpcon.ToString)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrTmp As DataRow In dttTmp.Rows
        '------------------------------------------------------------------------------------------------------------
        '--- Verifico se il conto è stato movimentato in SCADEN, PRINO, TESTMAG, TESTORD, ...
        '------------------------------------------------------------------------------------------------------------
        bCancellabile = IsAnagDeletable(dtrTmp!codditt.ToString, NTSCInt(dtrIn!an_pcconto), "")
        '------------------------------------------------------------------------------------------------------------
        If bCancellabile = True Then
          '----------------------------------------------------------------------------------------------------------
          '--- Posso cancellare ANAGRA e ANATRIC
          '----------------------------------------------------------------------------------------------------------
          strSQL = "DELETE anatric FROM anatric INNER JOIN anagra ON anatric.codditt = anagra.codditt AND anatric.ant_conto = anagra.an_conto" & _
                    " WHERE anagra.codditt = " & CStrSQL(dtrTmp!codditt.ToString) & _
                    " AND an_pcconto = " & NTSCInt(dtrIn!an_pcconto).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          '----------------------------------------------------------------------------------------------------------
          strSQL = "DELETE FROM anagra" & _
                    " WHERE codditt = " & CStrSQL(dtrTmp!codditt.ToString) & _
                    " AND an_pcconto = " & NTSCInt(dtrIn!an_pcconto).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Else
          '----------------------------------------------------------------------------------------------------------
          '--- Posso solo sganciare il conto di ANAGRA
          '----------------------------------------------------------------------------------------------------------
          strSQL = "UPDATE anagra" & _
                    " SET an_pcconto = 0," & _
                    " an_ultagg = GETDATE()" & _
                    " WHERE codditt = " & CStrSQL(dtrTmp!codditt.ToString) & _
                    " AND an_pcconto = " & NTSCInt(dtrIn!an_pcconto).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      Next    'For Each dtrTmp As DataRow In dttTmp.Rows
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      strSQL = "DELETE FROM tabcovp" & _
                " WHERE tb_concove = " & lCovpOld.ToString & _
                " AND tb_codpcon = " & CStrSQL(dtrIn!an_codpcon.ToString)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM anptric" & _
                " WHERE pct_codpcon = " & CStrSQL(dtrIn!an_codpcon.ToString) & _
                " AND pct_conto = " & NTSCInt(dtrIn!an_pcconto).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM anagpc" & _
                " WHERE pc_codpcon = " & CStrSQL(dtrIn!an_codpcon.ToString) & _
                " AND pc_conto = " & NTSCInt(dtrIn!an_pcconto).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiudo la transazione ed il database
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function


  Public Overridable Function GetAnalink(ByVal strDitta As String, ByVal lConto As Integer, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT analink.*, ac_descr1 as xxx_acconto " & _
               " FROM analink LEFT JOIN anagca ON analink.codditt = anagca.codditt AND analink.anl_acconto = anagca.ac_conto " & _
               " WHERE analink.codditt = " & CStrSQL(strDitta) & " AND anl_anconto = " & lConto.ToString
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANALINK")
      dsOut.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetAnagCA(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Count(*) As Records FROM anagca" & _
        " WHERE codditt = " & CStrSQL(strDitta)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAGCA")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetAnacent(ByVal strDitta As String, ByVal lConto As Integer, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT anacent.*, anagca.ac_descr1 as xx_contoca, tabcena.tb_descena as xx_codcena, tabcfam.tb_descfam as xx_codcfam" & _
        " FROM anacent" & _
        " LEFT JOIN anagca ON anacent.codditt = anagca.codditt AND anacent.anc_contoca = anagca.ac_conto" & _
        " LEFT JOIN tabcena ON anacent.codditt = tabcena.codditt AND anacent.anc_codcena = tabcena.tb_codcena" & _
        " LEFT JOIN tabcfam ON anacent.codditt = tabcfam.codditt AND anacent.anc_codcfam = tabcfam.tb_codcfam" & _
        " WHERE anacent.codditt = " & CStrSQL(strDitta) & _
        " AND anc_conto = " & lConto & _
        " ORDER BY anacent.codditt, anc_conto, anc_contoca, anc_codcena, anc_codcfam"

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANACENT")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "BNCIANA2"
  Public Overridable Function IsMultiDitta() As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      strSQL = "SELECT Count(*) As Records FROM tabanaz"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        If NTSCInt(dttTmp.Rows(0)!Records) > 1 Then Return True
      End If
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function DuplicaDitta(ByVal dtrIn As DataRow) As Boolean
    Dim bOk As Boolean = False
    Dim i As Integer = 0
    Dim lContoca As Integer = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttAnaz As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre un DataTable su TABANAZ, su tutte le ditte che possiedono lo stesso Piano dei Conti e lo stesso
      '--- Piano dei Conti di CA della ditta di origine
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT codditt FROM tabanaz" & _
        " WHERE codditt <> " & CStrSQL(dtrIn!codditt) & _
        " AND tb_azcodpcon = " & CStrSQL(dtrIn!anc2_codpcon) & _
        " AND tb_azcodpcca = " & CStrSQL(dtrIn!anc2_codpcca) & _
        " ORDER BY codditt"
      dttAnaz = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non esistono dati (altre ditte), esce
      '--------------------------------------------------------------------------------------------------------------
      If dttAnaz.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To (dttAnaz.Rows.Count - 1)
        '------------------------------------------------------------------------------------------------------------
        '--- Controlla se esiste già un record in ANACENT2 con la stessa chiave
        '------------------------------------------------------------------------------------------------------------
        strSQL = "SELECT anc2_codpcon FROM anacent2" & _
          " WHERE codditt = " & CStrSQL(dttAnaz.Rows(i)!codditt) & _
          " AND anc2_codpcon = " & CStrSQL(dtrIn!anc2_codpcon) & _
          " AND anc2_conto = " & NTSCInt(dtrIn!anc2_conto) & _
          " AND anc2_codpcca = " & CStrSQL(dtrIn!anc2_codpcca) & _
          " AND anc2_contoca = " & NTSCInt(dtrIn!anc2_contoca) & _
          " AND anc2_codcena = " & NTSCInt(dtrIn!anc2_codcena) & _
          " AND anc2_codcfam = " & CStrSQL(dtrIn!anc2_codcfam) & _
          " AND anc2_coddivi = " & NTSCInt(dtrIn!anc2_coddivi) & _
          " AND anc2_codstab = " & NTSCInt(dtrIn!anc2_codstab)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then GoTo DittaSuccessiva
        dttTmp.Clear()
        dttTmp.Dispose()
        '------------------------------------------------------------------------------------------------------------
        '--- Controllo esistenza conto in ANAGCA2
        '------------------------------------------------------------------------------------------------------------
        strSQL = "SELECT ac_conto FROM anagca2" & _
          " WHERE ac_codpcca = " & CStrSQL(dtrIn!anc2_codpcca) & _
          " AND ac_conto = " & NTSCInt(dtrIn!anc2_contoca)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count = 0 Then GoTo DittaSuccessiva
        dttTmp.Clear()
        dttTmp.Dispose()
        '------------------------------------------------------------------------------------------------------------
        '--- Controllo esistenza Codice Centro (TABCENA)
        '------------------------------------------------------------------------------------------------------------
        If NTSCInt(dtrIn!anc2_codcena) <> 0 Then
          strSQL = "SELECT tb_codcena FROM tabcena" & _
            " WHERE codditt = " & CStrSQL(dttAnaz.Rows(i)!codditt) & _
            " AND tb_codcena = " & NTSCInt(dtrIn!anc2_codcena)
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count = 0 Then GoTo DittaSuccessiva
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Controllo esistenza Linea/Prodotto (TABCFAM)
        '------------------------------------------------------------------------------------------------------------
        If NTSCStr(dtrIn!anc2_codcfam).Trim <> "" Then
          strSQL = "SELECT tb_codcfam FROM tabcfam" & _
            " WHERE codditt = " & CStrSQL(dttAnaz.Rows(i)!codditt) & _
            " AND tb_codcfam = " & CStrSQL(dtrIn!anc2_codcfam)
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count = 0 Then GoTo DittaSuccessiva
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Controllo esistenza Divisione (TABDIVI)
        '------------------------------------------------------------------------------------------------------------
        If NTSCInt(dtrIn!anc2_coddivi) <> 0 Then
          strSQL = "SELECT tb_coddivi FROM tabdivi" & _
            " WHERE codditt = " & CStrSQL(dttAnaz.Rows(i)!codditt) & _
            " AND tb_coddivi = " & NTSCInt(dtrIn!anc2_coddivi)
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count = 0 Then GoTo DittaSuccessiva
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Controllo esistenza Stabilimento/Filiale/Negozio (TABSTAB)
        '------------------------------------------------------------------------------------------------------------
        If NTSCInt(dtrIn!anc2_codstab) <> 0 Then
          strSQL = "SELECT tb_codstab FROM tabstab" & _
            " WHERE codditt = " & CStrSQL(dttAnaz.Rows(i)!codditt) & _
            " AND tb_codstab = " & NTSCInt(dtrIn!anc2_codstab)
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count = 0 Then GoTo DittaSuccessiva
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        '------------------------------------------------------------------------------------------------------------
        With dtrIn
          strSQL = "INSERT INTO anacent2 (codditt, anc2_codpcon, anc2_conto, anc2_codpcca, anc2_contoca," & _
            " anc2_codcena, anc2_codcfam, anc2_perc, anc2_coddivi, anc2_codstab)" & _
            " VALUES (" & CStrSQL(dttAnaz.Rows(i)!codditt) & ", " & CStrSQL(!anc2_codpcon) & ", " & _
            NTSCInt(!anc2_conto) & ", " & CStrSQL(!anc2_codpcca) & ", " & NTSCInt(!anc2_contoca) & ", " & _
            NTSCInt(!anc2_codcena) & ", " & CStrSQL(!anc2_codcfam) & ", " & CDblSQL(NTSCDec(!anc2_perc)) & ", " & _
            NTSCInt(!anc2_coddivi) & ", " & NTSCInt(!anc2_codstab) & ")"
        End With
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        '------------------------------------------------------------------------------------------------------------
        bOk = True
        '------------------------------------------------------------------------------------------------------------
DittaSuccessiva:
        '------------------------------------------------------------------------------------------------------------
        dttTmp.Clear()
        dttTmp.Dispose()
        '------------------------------------------------------------------------------------------------------------
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return bOk
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear() : dttTmp.Dispose()
      dttAnaz.Clear() : dttAnaz.Dispose()
    End Try
  End Function
  Public Overridable Function GetAnacent2(ByVal strCodpcon As String, ByVal lConto As Integer, _
  ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT anacent2.*, anagca2.ac_descr1 as xx_contoca, tabcena.tb_descena as xx_codcena," & _
        " tabcfam.tb_descfam as xx_codcfam, tabdivi.tb_desdivi as xx_coddivi, tabstab.tb_desstab as xx_codstab" & _
        " FROM anacent2" & _
        " LEFT JOIN anagca2 ON anacent2.anc2_codpcca = anagca2.ac_codpcca AND anacent2.anc2_contoca = anagca2.ac_conto" & _
        " LEFT JOIN tabcena ON anacent2.codditt = tabcena.codditt AND anacent2.anc2_codcena = tabcena.tb_codcena" & _
        " LEFT JOIN tabcfam ON anacent2.codditt = tabcfam.codditt AND anacent2.anc2_codcfam = tabcfam.tb_codcfam" & _
        " LEFT JOIN tabdivi ON anacent2.codditt = tabdivi.codditt AND anacent2.anc2_coddivi = tabdivi.tb_coddivi" & _
        " LEFT JOIN tabstab ON anacent2.codditt = tabstab.codditt AND anacent2.anc2_codstab = tabstab.tb_codstab" & _
        " WHERE anacent2.anc2_codpcon = " & CStrSQL(strCodpcon) & _
        " AND anacent2.anc2_conto = " & lConto & _
        " ORDER BY anacent2.codditt, anacent2.anc2_codpcon, anacent2.anc2_conto, anacent2.anc2_codcena," & _
        " anacent2.anc2_codcfam, anacent2.anc2_coddivi, anacent2.anc2_codstab"
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANACENT2")
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetAnagca2(ByVal strCodpcca As String, ByVal lConto As Integer, _
    ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM anagca2" & _
        " WHERE ac_codpcca = " & CStrSQL(strCodpcca) & _
        " AND ac_conto = " & lConto & _
        " ORDER BY ac_codpcca, ac_conto"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
#End Region

#Region "BN__NUOA"
  Public Overridable Function RitornaProgressivo(ByVal strDitta As String, ByVal nCodmast As Integer, _
  ByVal bGenerico As Boolean) As Integer
    Try
      'obsoleta
      Return RitornaProgressivo(strDitta, nCodmast, bGenerico, "")

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function RitornaProgressivo(ByVal strDitta As String, ByVal nCodmast As Integer, _
                                                  ByVal bGenerico As Boolean, ByVal strPdc As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim i As Integer = 0

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, nCodmast, bGenerico, strPdc})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCInt(oOut)
      End If

      strSQL = "SELECT" & _
        " MAX(an_conto) AS CONTO" & _
        " FROM  anagra" & _
        " WHERE an_tipo = 'S'" & _
        " AND an_codmast = " & nCodmast & _
        IIf(bGenerico = False, " AND codditt = " & CStrSQL(strDitta), "").ToString
      If strPdc <> "" Then strSQL += " AND an_codpcon = " & CStrSQL(strPdc)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If dttTmp.Rows.Count > 0 AndAlso NTSCInt(dttTmp.Rows(0)!CONTO) > 0 Then
        i = NTSCStr(dttTmp.Rows(0)!CONTO).Length - NTSCStr(nCodmast).Length
        'se sono già arriato a tutti 9 per quel mastro, ritorno 1
        If NTSCStr(dttTmp.Rows(0)!CONTO).Substring(NTSCStr(nCodmast).Length) = "".PadLeft(i, "9"c) Then
          Return 1
        Else
          Return NTSCInt(NTSCStr(dttTmp.Rows(0)!CONTO).Substring(NTSCStr(nCodmast).Length)) + 1
        End If
      End If

      Return 1
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
#End Region

End Class
