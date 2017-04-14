Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGINVF
  Inherits CLD__BASE

  Public Overridable Function CreaListaSelezionata(ByVal strDitta As String, ByVal strDest As String, _
                                                   ByVal nDalistaDest As Integer, ByVal nAlistaDest As Integer, _
                                                   ByVal nCodmaga As Integer, ByVal strWhereArtico As String, _
                                                   ByRef strSQLArticoliInventariati As String, _
                                                   ByRef strErr As String) As Boolean
    Try
      Return CreaListaSelezionata(strDitta, strDest, nDalistaDest, nAlistaDest, nCodmaga, strWhereArtico, strSQLArticoliInventariati, strErr, "", 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaListaSelezionata(ByVal strDitta As String, ByVal strDest As String, _
                                                 ByVal nDalistaDest As Integer, ByVal nAlistaDest As Integer, _
                                                 ByVal nCodmaga As Integer, ByVal strWhereArtico As String, _
                                                 ByRef strSQLArticoliInventariati As String, _
                                                 ByRef strErr As String, ByVal strDatainv As String, ByVal nCausInv As Integer) As Boolean
    'creo una lista selezionata con tutti gli articoli come da 'SELEZIONE DEGLI ARTICOLI INVENTARIATI' 
    'con id fisso = -1
    'OVVERO TUTTI GLI ARTICOLI DI BUS LA CUI ESISTENZA POTREBBE ESSERE DA CORREGGERE
    'Prima determino gli articoli non gestiti a matricole
    'successivamente rifaccio il giro solo per quelli a matricole
    'alla fine aggiungo gli articoli mai movimentati
    Dim strSQL As String = ""
    Dim strSQL1 As String = ""
    Dim dttTmp As New DataTable
    Dim strSelectListSel As String = ""
    Dim strSelectDoc As String = ""
    Dim nRiga As Integer = 0
    Dim strWhere As String = ""
    Dim nRec As Integer = 0
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDest, nDalistaDest, nAlistaDest, nCodmaga, strWhereArtico, strSQLArticoliInventariati, strErr, strDatainv, nCausInv})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strSQLArticoliInventariati = CType(oIn(6), String)
        strErr = CType(oIn(7), String)
        Return CBool(oOut)
      End If
      '----------------


      '--------------------
      'pulisco il temporaneo (questo programma può essere utilizzato SOLO da un operatore per volta!!!!!!!!    lsa_codlsar = -1 fisso
      strErr = ""
      strSQL = "DELETE FROM listsar WHERE codditt = " & CStrSQL(strDitta) & " AND lsa_codlsar = -1"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "SELECT ar_codart, isnull(lp_fase, 0) as lp_fase, isnull(lp_lotto, 0) as lp_lotto, " & _
               "       isnull(lp_commeca, 0) as lp_commeca, CASE WHEN isnull(lp_ubicaz, '') = '' THEN ' ' ELSE lp_ubicaz END as lp_ubicaz " & _
               " FROM artico " & _
               "  LEFT JOIN lotcpro ON lotcpro.lp_codart = artico.ar_codart AND lotcpro.codditt = artico.codditt"
      strSQLArticoliInventariati = "SELECT DISTINCT ar_codart, isnull(af_fase, 0) as xx_fase" & _
                                   " FROM artico LEFT JOIN artfasi ON artico.codditt = artfasi.codditt " & _
                                   " AND artico.ar_codart = artfasi.af_codart "
      Select Case strDest
        Case "B"
          '-------------------
          'selezione articoli come da 'ARTICOLI BLOCCATI'
          strSQL += " WHERE artico.codditt = " & CStrSQL(strDitta) & " AND artico.ar_blocco = 'S'"
          strSQLArticoliInventariati += " WHERE artico.codditt = " & CStrSQL(strDitta) & " AND artico.ar_blocco = 'S'"
        Case "A"
          '-------------------
          'selezione articoli come da 'SELEZIONE ARTICOLI'
          TraduciWhere(strWhereArtico, strWhere)
          strWhere = " WHERE artico.codditt = " & CStrSQL(strDitta) & strWhere
          strSQL += strWhere
          strSQLArticoliInventariati += strWhere
        Case "L"
          '-------------------
          'selezione articoli come da 'LISTA SELEZIONATA ARTICOLI INVENTARIATI'
          strSQL1 = "SELECT DISTINCT lsa_codart, lsa_fase, lsa_commeca, lsa_lotto, lsa_ubicaz" & _
                    " FROM listsar " & _
                    "  INNER JOIN artico ON listsar.codditt = artico.codditt AND listsar.lsa_codart = artico.ar_codart " & _
                    " WHERE listsar.codditt = " & CStrSQL(strDitta) & _
                    "   AND lsa_codlsar >= " & nDalistaDest & _
                    "   AND lsa_codlsar <= " & nAlistaDest & _
                    "   AND lsa_trattato = 'S'" & _
                    "   AND ar_tipoopz IN (' ', 'O')"

          dttTmp = OpenRecordset(strSQL1, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count = 0 Then
            strErr = oApp.Tr(Me, 128792677797192000, _
                    "Le liste selezionate per gli 'articoli inventariati' non contengono articoli. " & vbCrLf & _
                    "(Possono essere prelevati solo articoli 'Reali' marcati come 'trattati' nella liste selezionate)")
            Return False
          End If

          'Inserisce subito i dati in TTKEYS
          ResetTblInstId("TTKEYS", False, -1)
          For Each dtrTmp As DataRow In dttTmp.Rows
            strSQL = "INSERT INTO ttkeys (codditt, instid, ke_codart, ke_fase, ke_conto, ke_lead, ke_descr) " & _
                     " VALUES (" & CStrSQL(strDitta) & ", -1, " & CStrSQL(dtrTmp!lsa_codart) & ", " & NTSCInt(dtrTmp!lsa_fase) & ", " & _
                     NTSCInt(dtrTmp!lsa_lotto) & ", " & NTSCInt(dtrTmp!lsa_commeca) & ", " & CStrSQL(dtrTmp!lsa_ubicaz) & ")"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          Next

          strSelectListSel = "SELECT lsa_codart" & _
                              " FROM listsar INNER JOIN artico ON listsar.codditt = artico.codditt " & _
                              "                         AND listsar.lsa_codart = artico.ar_codart " & _
                              " WHERE listsar.codditt = " & CStrSQL(strDitta) & _
                              "   AND lsa_codlsar >= " & nDalistaDest & _
                              "   AND lsa_codlsar <= " & nAlistaDest & _
                              "   AND lsa_trattato = 'S'" & _
                              "   AND ar_tipoopz IN (' ', 'O')"
          strWhere = " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                     " AND artico.ar_codart IN (" & strSelectListSel & ")"
          strSQL += strWhere
          strSQLArticoliInventariati += strWhere
          dttTmp.Clear()
        Case "D"
          '---------------------
          'Gli stessi articoli del documento di origine
          strSelectDoc = "SELECT mm_codart FROM movmag " & _
                         " INNER JOIN testmag ON movmag.codditt = testmag.codditt AND tm_tipork = mm_tipork AND tm_anno = mm_anno " & _
                         "                   AND tm_serie = mm_serie AND tm_numdoc = mm_numdoc " & _
                         " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                         "   AND tm_datdoc = " & CDataSQL(strDatainv) & _
                         "   AND tm_causale = " & nCausInv & _
                         "   AND tm_magaz = " & nCodmaga

          strWhere = " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                     "   AND artico.ar_codart IN (" & strSelectDoc & ")"

          strSQL += strWhere
          strSQLArticoliInventariati += strWhere
          dttTmp.Clear()
      End Select

      '------------------------------------
      'riempio il temporaneo
      strSQL += " AND artico.ar_gestmatr = 'N' AND ar_tipoopz IN (' ', 'O')" & _
                " AND (lp_magaz = " & nCodmaga & " OR lp_magaz is null) " & _
                " AND (lp_lotto <> 0 OR lp_esist <> 0 OR ar_geslotti = 'N')" 'Altrimenti considerava il lotto 0 di lotcpro generato da righe di ordine dove il lotto non è obbligatorio.


      'memorizzo la query per ottenere gli articoli da elaborare (articoli inventariati) servirà successivamente nella 'esegui'
      strSQLArticoliInventariati += " AND ar_tipoopz IN (' ', 'O')"

      If strDest <> "L" Then 'L'inserimento per il caso "L" viene eseguito prima di questo punto
        ResetTblInstId("TTKEYS", False, -1)
        strSQL = "INSERT INTO ttkeys (codditt, instid, ke_codart, ke_fase, ke_conto, ke_lead, ke_descr) " & _
                 " SELECT " & CStrSQL(strDitta) & ", -1, " & strSQL.Substring(7)
        nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      strSQL = "SELECT min(ke_progr) as ke_progr FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = -1"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        nRec = NTSCInt(dttTmp.Rows(0)!ke_progr) - 1
      Else
        nRec = 0
      End If
      dttTmp.Clear()

      strSQL = "INSERT INTO listsar (codditt, lsa_codart, lsa_codlsar, lsa_riga, lsa_flag, lsa_fase," & _
               " lsa_commeca, lsa_lotto, lsa_ubicaz, lsa_matric, lsa_esist, lsa_trattato, lsa_int1)" & _
               " SELECT codditt, ke_codart, -1, ke_progr - " & nRec.ToString & ", " & _
               " 'N', ke_fase, ke_lead, ke_conto, ke_descr, ' ', 0, 'S', 0" & _
               " FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = -1"

      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      ResetTblInstId("TTKEYS", False, -1)

      'dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      'If dttTmp.Rows.Count > 0 Then
      '  nRiga = 1
      '  For Each dtrT As DataRow In dttTmp.Rows
      '    strSQL = "INSERT INTO listsar (codditt, lsa_codart, lsa_codlsar, lsa_riga, lsa_flag, lsa_fase," & _
      '              " lsa_commeca, lsa_lotto, lsa_ubicaz, lsa_matric, lsa_esist, lsa_trattato, lsa_int1)" & _
      '              " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(dtrT!ar_codart) & _
      '              ", -1, " & nRiga & ", 'N', " & _
      '              NTSCInt(dtrT!lp_fase) & "," & _
      '              NTSCInt(dtrT!lp_commeca) & ", " & _
      '              NTSCInt(dtrT!lp_lotto) & ", " & _
      '              CStrSQL(IIf(NTSCStr(dtrT!lp_ubicaz) = "", " ", NTSCStr(dtrT!lp_ubicaz)).ToString) & ", ' ', 0, 'S', 0)"
      '    Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '    nRiga = nRiga + 1
      '  Next
      '  dttTmp.Clear()
      'End If

      '---------------------
      'Ora considero gli articoli gestiti a matricola
      strSQL = "SELECT DISTINCT ar_codart, mm_fase, km_lotto, km_commeca, km_ubicaz, mma_matric" & _
               " FROM (artico INNER JOIN (movmag INNER JOIN movmatr ON movmag.mm_riga = movmatr.mma_riga" & _
               "        AND movmag.mm_numdoc = movmatr.mma_numdoc AND movmag.mm_serie = movmatr.mma_serie AND movmag.mm_anno = movmatr.mma_anno" & _
               "        AND movmag.mm_tipork = movmatr.mma_tipork AND movmag.codditt = movmatr.codditt) " & _
               "      ON artico.codditt = movmag.codditt AND artico.ar_codart = movmag.mm_codart) " & _
               "  INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork " & _
               "      AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie " & _
               "      AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga "

      strWhere = ""

      Select Case strDest
        Case "B"
          '----------------------
          'solo articoli bloccati
          strSQL += " WHERE artico.codditt = " & CStrSQL(strDitta) & " AND artico.ar_blocco = 'S'"
        Case "A"
          '----------------------
          'solo articoli da 'selezione articoli'
          TraduciWhere(strWhereArtico, strWhere)
          strWhere = " WHERE artico.codditt = " & CStrSQL(strDitta) & strWhere
          strSQL += strWhere
        Case "L"
          '---------------------
          'solo articoli da lista selez di destinazione: il filtro è già stato calcolato sopra
          strSQL += " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                    " AND artico.ar_codart IN (" & strSelectListSel & ")"
        Case "D"
          '---------------------
          'Gli stessi articoli del documento di origine
          strSQL &= " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                   "   AND artico.ar_codart IN (" & strSelectDoc & ")"
      End Select

      strSQL += " AND artico.ar_gestmatr = 'S'" & _
                " AND (movmag.mm_magaz = " & nCodmaga & " OR movmag.mm_magaz is null) "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)


      '--------------------
      ResetTblInstId("TTKEYS", False, -1)
      strSQL = "INSERT INTO ttkeys (codditt, instid, ke_codart, ke_fase, ke_conto, ke_lead, ke_descr, ke_desint) " & _
               " SELECT DISTINCT " & CStrSQL(strDitta) & ", -1, " & strSQL.Substring(16)
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "SELECT max(lsa_riga) as lsa_riga FROM listsar WHERE codditt = " & CStrSQL(strDitta) & " AND lsa_codlsar = -1"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        nRiga = NTSCInt(dttTmp.Rows(0)!lsa_riga) + 1
      Else
        nRec = 0
      End If
      dttTmp.Clear()
      strSQL = "SELECT min(ke_progr) as ke_progr FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = -1"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        nRec = NTSCInt(dttTmp.Rows(0)!ke_progr) - 1
      Else
        nRec = 0
      End If
      dttTmp.Clear()

      strSQL = "INSERT INTO listsar (codditt, lsa_codart, lsa_codlsar, lsa_riga, lsa_flag, lsa_fase," & _
               " lsa_commeca, lsa_lotto, lsa_ubicaz, lsa_matric, lsa_esist, lsa_trattato, lsa_int1)" & _
               " SELECT codditt, ke_codart, -1, ke_progr - " & nRec.ToString & " + " & nRiga.ToString & ", " & _
               " 'N', ke_fase, ke_lead, ke_conto, " & _
               " CASE WHEN isnull(ke_descr, '') = '' THEN ' ' ELSE ke_descr END, ke_desint, 0, 'S', 0" & _
               " FROM ttkeys WHERE codditt = " & CStrSQL(strDitta) & " AND instid = -1"
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      ResetTblInstId("TTKEYS", False, -1)

      'For Each dtrT As DataRow In dttTmp.Rows
      '  strSQL = "INSERT INTO listsar (codditt, lsa_codart, lsa_codlsar, lsa_riga, lsa_flag, lsa_fase," & _
      '          "   lsa_commeca, lsa_lotto, lsa_ubicaz, lsa_matric, lsa_esist, lsa_trattato, lsa_int1)" & _
      '          " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(dtrT!ar_codart) & _
      '          ", -1, " & nRiga & ", 'N', " & _
      '          NTSCInt(dtrT!mm_fase) & "," & _
      '          NTSCInt(dtrT!km_commeca) & ", " & _
      '          NTSCInt(dtrT!km_lotto) & ", " & _
      '          CStrSQL(IIf(NTSCStr(dtrT!km_ubicaz) = "", " ", NTSCStr(dtrT!km_ubicaz)).ToString) & ", " & _
      '          CStrSQL(dtrT!mma_matric) & ", " & "0, 'S', 0)"

      '  Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '  nRiga = nRiga + 1
      'Next
      dttTmp.Clear()

      strSQL = "SELECT max(lsa_riga) as lsa_riga FROM listsar WHERE codditt = " & CStrSQL(strDitta) & " AND lsa_codlsar = -1"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp IsNot Nothing AndAlso dttTmp.Rows.Count > 0 Then
        nRiga = NTSCInt(dttTmp.Rows(0)!lsa_riga) + 1
      Else
        nRiga = 1
      End If

      '---------------------
      'adesso devo aggiungere tutti gli articoli mai movimentati 
      '(dovrebbe servire solo per gli articoli gestiti a matricola)
      strSQL = strSQLArticoliInventariati & " AND not exists (SELECT lsa_codart FROM listsar" & _
                                            " WHERE lsa_codlsar = -1 " & _
                                            " AND lsa_codart = ar_codart AND lsa_fase = " & _
                                            "isnull(af_fase, 0)" & _
                                            "AND listsar.codditt = artico.codditt) "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      For Each dtrT As DataRow In dttTmp.Rows
        strSQL = "INSERT INTO listsar (codditt, lsa_codart, lsa_codlsar, lsa_riga, lsa_flag, lsa_fase," & _
                  " lsa_commeca, lsa_lotto, lsa_ubicaz, lsa_matric, lsa_esist, lsa_trattato, lsa_int1)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(dtrT!ar_codart) & _
                  ", -1, " & nRiga & ", 'N', " & dtrT!xx_fase.ToString & ", 0, 0, ' ', ' ', 0, 'S', 0)"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        nRiga = nRiga + 1
      Next

      dttTmp.Clear()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


  Public Overridable Function GetOrigineInventario(ByVal strDitta As String, ByVal strDatainv As String, _
                                                  ByVal nCodmaga As Integer, ByVal strOrig As String, _
                                                  ByVal nDalistaOrig As Integer, ByVal nAlistaOrig As Integer, _
                                                  ByVal nCausInv As Integer, ByRef dttOut As DataTable) As Boolean
    '------------------------
    'ottengo l'elenco degli articoli con l'esitenza effettiva (DA ORIGINE INVENTARIO)
    Dim strSQL As String = ""
    Try
      If strOrig = "L" Then
        '-------------------
        'da lista selezionata
        strSQL = "SELECT lsa_codart as am_codart, lsa_fase as am_fase, " & _
                 " max(ar_gescomm) as am_gescomm, max(ar_geslotti) as am_geslotti, " & _
                 " max(ar_gesubic) as am_gesubic, max(ar_gestmatr) as am_gestmatr, " & _
                 " CASE WHEN ar_gescomm = 'S' THEN lsa_commeca ELSE 0 END as am_commeca, " & _
                 " CASE WHEN ar_geslotti = 'S' THEN lsa_lotto ELSE 0 END as am_lotto, " & _
                 " CASE WHEN ar_gesubic = 'S' THEN lsa_ubicaz ELSE ' ' END as am_ubicaz, " & _
                 " CASE WHEN ar_gestmatr = 'S' THEN lsa_matric ELSE null END as am_matric, " & _
                 " sum(lsa_esist) as am_quant " & _
                 " FROM (tablsar INNER JOIN listsar ON (tablsar.tb_codlsar = listsar.lsa_codlsar) AND (tablsar.codditt = listsar.codditt)) " & _
                 " INNER JOIN artico ON listsar.codditt = artico.codditt AND listsar.lsa_codart = artico.ar_codart " & _
                 " WHERE tablsar.codditt = " & CStrSQL(strDitta) & _
                 " AND lsa_codlsar >= " & nDalistaOrig.ToString & _
                 " AND lsa_codlsar <= " & nAlistaOrig.ToString & _
                 " AND tb_codmagap = " & nCodmaga.ToString & _
                 " AND tb_status = 'C'" & _
                 " GROUP BY lsa_codart, lsa_fase, " & _
                 " CASE WHEN ar_gescomm = 'S' THEN lsa_commeca ELSE 0 END, " & _
                 " CASE WHEN ar_geslotti = 'S' THEN lsa_lotto ELSE 0 END, " & _
                 " CASE WHEN ar_gesubic = 'S' THEN lsa_ubicaz ELSE ' ' END, " & _
                 " CASE WHEN ar_gestmatr = 'S' THEN lsa_matric ELSE null END, " & _
                 " ar_gescomm, ar_geslotti, ar_gesubic, ar_gestmatr " & _
                 " ORDER BY lsa_codart, lsa_fase, " & _
                 " CASE WHEN ar_gescomm = 'S' THEN lsa_commeca ELSE 0 END, " & _
                 " CASE WHEN ar_geslotti = 'S' THEN lsa_lotto ELSE 0 END, " & _
                 " CASE WHEN ar_gesubic = 'S' THEN lsa_ubicaz ELSE ' ' END, " & _
                 " CASE WHEN ar_gestmatr = 'S' THEN lsa_matric ELSE null END "
      Else
        '-------------------
        'da documenti di magazzino
        strSQL = "SELECT mm_codart as am_codart, mm_fase as am_fase, " & _
                " max(ar_gescomm) as am_gescomm, max(ar_geslotti) as am_geslotti, " & _
                " max(ar_gesubic) as am_gesubic, max(ar_gestmatr) as am_gestmatr, " & _
                " CASE WHEN ar_gescomm = 'S' THEN mm_commeca ELSE 0 END as am_commeca, " & _
                " CASE WHEN ar_geslotti = 'S' THEN mm_lotto ELSE 0 END as am_lotto, " & _
                " CASE WHEN ar_gesubic = 'S' THEN mm_ubicaz ELSE ' ' END as am_ubicaz, " & _
                " CASE WHEN ar_gestmatr = 'S' THEN mma_matric ELSE null END as am_matric, " & _
                " sum(CASE WHEN movmatr.mma_numdoc is null THEN mm_quant ELSE mma_quant END ) as am_quant " & _
                " FROM ((testmag INNER JOIN movmag ON (testmag.codditt = movmag.codditt) AND " & _
                " (testmag.tm_tipork = movmag.mm_tipork) AND (testmag.tm_serie = movmag.mm_serie) " & _
                " AND (testmag.tm_anno = movmag.mm_anno) AND (testmag.tm_numdoc = movmag.mm_numdoc)) " & _
                " LEFT JOIN movmatr ON (movmag.mm_riga = movmatr.mma_riga) AND " & _
                " (movmag.mm_numdoc = movmatr.mma_numdoc) AND (movmag.mm_serie = movmatr.mma_serie) " & _
                " AND (movmag.mm_anno = movmatr.mma_anno) AND (movmag.mm_tipork = movmatr.mma_tipork) " & _
                " AND (movmag.codditt = movmatr.codditt))" & _
                " INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart " & _
                " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                " AND tm_datdoc = " & CDataSQL(strDatainv) & _
                " AND tm_causale = " & nCausInv.ToString & " AND tm_magaz = " & nCodmaga.ToString & _
                " GROUP BY mm_codart, mm_fase, " & _
                " CASE WHEN ar_gescomm = 'S' THEN mm_commeca ELSE 0 END, " & _
                " CASE WHEN ar_geslotti = 'S' THEN mm_lotto ELSE 0 END, " & _
                " CASE WHEN ar_gesubic = 'S' THEN mm_ubicaz ELSE ' ' END, " & _
                " CASE WHEN ar_gestmatr = 'S' THEN mma_matric ELSE null END, " & _
                " ar_gescomm, ar_geslotti, ar_gesubic, ar_gestmatr " & _
                " ORDER BY mm_codart, mm_fase, " & _
                " CASE WHEN ar_gescomm = 'S' THEN mm_commeca ELSE 0 END, " & _
                " CASE WHEN ar_geslotti = 'S' THEN mm_lotto ELSE 0 END, " & _
                " CASE WHEN ar_gesubic = 'S' THEN mm_ubicaz ELSE ' ' END, " & _
                " CASE WHEN ar_gestmatr = 'S' THEN mma_matric ELSE null END "
      End If
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RiempiTTArtinvf(ByVal strDitta As String, ByVal strDatainv As String, _
                                              ByVal nCodmaga As Integer, ByVal strDtulap As String, _
                                              ByVal lIITTArtinvf As Integer) As Boolean
    '---------------------
    'calcolo l'esistenza degli articoli in Business da artdefx/artdef/lotcdef + i movim 
    'di magazzino fino alla data fine elab
    Dim strSQL As String = ""
    Dim lIITTArtinvfTMP As Integer = 0
    Try
      'Svuota la tabella temporanea TTARTINVF
      ResetTblInstId("TTARTINVF", False, lIITTArtinvf)

      '---------------------
      'Ricopia i dati di LOTCDEF (ARTDEF)
      strSQL = "INSERT INTO TTARTINVF (codditt, instid, ap_codart, ap_fase, ap_magaz, ap_esist," & _
              " ap_prenot, ap_ordin, ap_impeg, ap_carfor, ap_carpro, ap_carvar, ap_rescli," & _
              " ap_scacli, ap_scapro, ap_scavar, ap_resfor, ap_giaini, ap_vprenot, ap_vordin," & _
              " ap_vimpeg, ap_vcarfor, ap_vcarpro, ap_vcarvar, ap_vrescli, ap_vscacli," & _
              " ap_vscapro, ap_vscavar, ap_vresfor, ap_vgiaini, ap_sommat, ap_daordi, ap_vdaordi, " & _
              " ap_commeca, ap_lotto, ap_ubicaz)" & _
              " SELECT " & CStrSQL(strDitta) & ", " & lIITTArtinvf & ", ld_codart, ld_fase, ld_magaz, ld_quant, " & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ld_commeca, ld_lotto, ld_ubicaz" & _
              " FROM (artico INNER JOIN lotcdef ON (artico.ar_codart = lotcdef.ld_codart) AND (artico.codditt = lotcdef.codditt)) " & _
              " INNER JOIN LISTSAR ON (lotcdef.ld_lotto = LISTSAR.lsa_lotto) " & _
              " AND (lotcdef.ld_commeca = LISTSAR.lsa_commeca) AND (lotcdef.ld_ubicaz = LISTSAR.lsa_ubicaz) " & _
              " AND (lotcdef.ld_fase = LISTSAR.lsa_fase) AND (lotcdef.ld_codart = LISTSAR.lsa_codart) " & _
              " AND (lotcdef.codditt = LISTSAR.codditt) " & _
              " WHERE lotcdef.codditt = " & CStrSQL(strDitta) & _
              " AND artico.ar_gestmatr = 'N'" & _
              " AND artico.ar_tipoopz IN (' ', 'O')" & _
              " AND ld_magaz = " & nCodmaga.ToString & _
              " AND lsa_codlsar = -1"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------
      'ora inserisco in TTARTINVF gli articoli non presenti in lotcdef e in keymag
      strSQL = "INSERT INTO TTARTINVF (codditt, instid, ap_codart, ap_fase, ap_magaz, ap_esist," & _
              " ap_prenot, ap_ordin, ap_impeg, ap_carfor, ap_carpro, ap_carvar, ap_rescli," & _
              " ap_scacli, ap_scapro, ap_scavar, ap_resfor, ap_giaini, ap_vprenot, ap_vordin," & _
              " ap_vimpeg, ap_vcarfor, ap_vcarpro, ap_vcarvar, ap_vrescli, ap_vscacli," & _
              " ap_vscapro, ap_vscavar, ap_vresfor, ap_vgiaini, ap_sommat, ap_daordi, ap_vdaordi, ap_commeca, ap_lotto, ap_ubicaz)" & _
              " SELECT " & CStrSQL(strDitta) & ", " & lIITTArtinvf & ", lsa_codart, lsa_fase, " & nCodmaga.ToString & _
              " , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, lsa_commeca, lsa_lotto, lsa_ubicaz" & _
              " FROM listsar INNER JOIN artico ON (listsar.lsa_codart = artico.ar_codart) AND (listsar.codditt = artico.codditt)" & _
              " WHERE listsar.codditt = " & CStrSQL(strDitta) & _
              " AND artico.ar_gestmatr = 'N'" & _
              " AND artico.ar_tipoopz IN (' ', 'O')" & _
              " AND NOT EXISTS (SELECT ap_codart FROM TTARTINVF" & _
                               " WHERE codditt = " & CStrSQL(strDitta) & _
                               " AND ap_codart = lsa_codart" & _
                               " AND ap_fase = lsa_fase" & _
                               " AND ap_commeca = lsa_commeca" & _
                               " AND ap_lotto = lsa_lotto" & _
                               " AND ap_ubicaz = lsa_ubicaz" & _
                               " AND ap_magaz = " & nCodmaga.ToString & _
                               " AND instid = " & lIITTArtinvf & ")" & _
              " AND lsa_codlsar = -1"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------
      'Aggiornamento da MOVMAG, KEYMAG, TABCAUM
      'creo una seconda serie di record (definiti con instid specifico contenenti, per gli articoli selezionati,
      'i movimenti da aggiungere ad artdef per avere l'esistenza a data
      lIITTArtinvfTMP = GetTblInstId("TTARTINVF", False)

      strSQL = "INSERT INTO TTARTINVF (codditt, instid, ap_codart, ap_fase, ap_magaz, ap_esist," & _
                " ap_prenot, ap_ordin, ap_impeg, ap_carfor, ap_carpro, ap_carvar, ap_rescli," & _
                " ap_scacli, ap_scapro, ap_scavar, ap_resfor, ap_giaini, ap_vprenot, ap_vordin," & _
                " ap_vimpeg, ap_vcarfor, ap_vcarpro, ap_vcarvar, ap_vrescli, ap_vscacli," & _
                " ap_vscapro, ap_vscavar, ap_vresfor, ap_vgiaini, ap_sommat, ap_daordi, ap_vdaordi, ap_commeca, ap_lotto, ap_ubicaz)" & _
                " SELECT " & CStrSQL(strDitta) & ", " & lIITTArtinvfTMP & ", ar_codart, km_fase, " & nCodmaga.ToString & ", sum(mm_quant * tb_esist)," & _
                lIITTArtinvf & " , 0, 0, sum(mm_quant * tb_carfor), sum(mm_quant * tb_carpro), sum(mm_quant * tb_carvar), sum(mm_quant * tb_rescli), " & _
                " sum (mm_quant * tb_scacli), sum(mm_quant * tb_scapro), sum(mm_quant * tb_scavar), sum(mm_quant * tb_resfor), sum(mm_quant * tb_giaini), 0, 0," & _
                " 0, sum(mm_valore * tb_vcarfor), sum(mm_valore * tb_vcarpro), sum(mm_valore * tb_vcarvar), sum(mm_valore * tb_vrescli), sum(mm_valore * tb_vscacli)," & _
                " sum(mm_valore * tb_vscapro), sum(mm_valore * tb_vscavar), sum(mm_valore * tb_vresfor), sum(mm_valore * tb_vgiaini)," & _
                " 0, 0, 0," & _
                " CASE WHEN ar_gescomm = 'S' THEN km_commeca ELSE 0 END as colleca, " & _
                " CASE WHEN ar_geslotti = 'S' THEN km_lotto ELSE 0 END as lotto, " & _
                " CASE WHEN ar_gesubic = 'S' THEN km_ubicaz ELSE ' ' END as ubicaz " & _
                " FROM tabcaum, movmag, artico, keymag, TTARTINVF " & _
                " WHERE tabcaum.tb_codcaum = keymag.km_causale " & _
                " AND artico.ar_codart = keymag.km_codart AND artico.codditt = keymag.codditt " & _
                " AND keymag.km_magaz = TTARTINVF.ap_magaz AND keymag.km_codart = TTARTINVF.ap_codart " & _
                " AND keymag.km_fase = TTARTINVF.ap_fase AND keymag.codditt = TTARTINVF.codditt " & _
                " AND keymag.codditt = TTARTINVF.codditt AND movmag.codditt = keymag.codditt " & _
                " AND movmag.mm_riga = keymag.km_riga AND movmag.mm_tipork = keymag.km_tipork " & _
                " AND movmag.mm_serie = keymag.km_serie AND movmag.mm_anno = keymag.km_anno " & _
                " AND movmag.mm_numdoc = keymag.km_numdoc " & _
                " AND keymag.km_ubicaz = CASE WHEN ar_gesubic = 'S' THEN TTARTINVF.ap_ubicaz ELSE keymag.km_ubicaz END " & _
                " AND keymag.km_lotto = CASE WHEN ar_geslotti = 'S' THEN TTARTINVF.ap_lotto ELSE keymag.km_lotto END " & _
                " AND keymag.km_commeca = CASE WHEN ar_gescomm = 'S' THEN TTARTINVF.ap_commeca ELSE keymag.km_commeca END " & _
                " AND TTARTINVF.codditt = " & CStrSQL(strDitta) & _
                " AND TTARTINVF.instid = " & lIITTArtinvf & _
                " AND artico.ar_gestmatr = 'N'" & _
                " AND artico.ar_tipoopz IN (' ', 'O')" & _
                " AND km_aammgg > " & CDataSQL(strDtulap) & _
                " AND km_aammgg <= " & CDataSQL(strDatainv) & _
                " AND km_magaz = " & nCodmaga.ToString & _
                " AND exists (SELECT lsa_codart FROM listsar WHERE lsa_codlsar = -1  AND " & _
                " lsa_codart = km_codart AND lsa_fase = km_fase AND " & _
                " lsa_commeca = CASE WHEN ar_gescomm = 'S' THEN km_commeca ELSE 0 END " & _
                " AND lsa_lotto = CASE WHEN ar_geslotti = 'S' THEN km_lotto ELSE 0 END " & _
                " AND lsa_ubicaz = CASE WHEN ar_gesubic = 'S' THEN km_ubicaz ELSE ' ' END " & _
                " AND listsar.codditt = keymag.codditt) " & _
                " GROUP BY ar_codart, km_fase, " & _
                " CASE WHEN ar_gescomm = 'S' THEN km_commeca ELSE 0 END, " & _
                " CASE WHEN ar_geslotti = 'S' THEN km_lotto ELSE 0 END, " & _
                " CASE WHEN ar_gesubic = 'S' THEN km_ubicaz ELSE ' ' END "
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------
      'aggiungo i valori sopra determinati ai record del primo instid per avere l'esistenza da data di tutti gli articoli
      strSQL = " UPDATE TTARTINVF SET " & _
                " TTARTINVF.ap_esist = round(TTARTINVF.ap_esist + TTARTINVF1.ap_esist,3), " & _
                " TTARTINVF.ap_carfor = round(TTARTINVF.ap_carfor + TTARTINVF1.ap_carfor,3), " & _
                " TTARTINVF.ap_carpro = round(TTARTINVF.ap_carpro + TTARTINVF1.ap_carpro,3), " & _
                " TTARTINVF.ap_carvar = round(TTARTINVF.ap_carvar + TTARTINVF1.ap_carvar,3), " & _
                " TTARTINVF.ap_rescli = round(TTARTINVF.ap_rescli + TTARTINVF1.ap_rescli,3), " & _
                " TTARTINVF.ap_scacli = round(TTARTINVF.ap_scacli + TTARTINVF1.ap_scacli,3), " & _
                " TTARTINVF.ap_scapro = round(TTARTINVF.ap_scapro + TTARTINVF1.ap_scapro,3), " & _
                " TTARTINVF.ap_scavar = round(TTARTINVF.ap_scavar + TTARTINVF1.ap_scavar,3), " & _
                " TTARTINVF.ap_resfor = round(TTARTINVF.ap_resfor + TTARTINVF1.ap_resfor,3), " & _
                " TTARTINVF.ap_giaini = round(TTARTINVF.ap_giaini + TTARTINVF1.ap_giaini,3), " & _
                " TTARTINVF.ap_vcarfor = round(TTARTINVF.ap_vcarfor + TTARTINVF1.ap_vcarfor,3), " & _
                " TTARTINVF.ap_vcarpro = round(TTARTINVF.ap_vcarpro + TTARTINVF1.ap_vcarpro,3), " & _
                " TTARTINVF.ap_vcarvar = round(TTARTINVF.ap_vcarvar + TTARTINVF1.ap_vcarvar,3), " & _
                " TTARTINVF.ap_vrescli = round(TTARTINVF.ap_vrescli + TTARTINVF1.ap_vrescli,3), " & _
                " TTARTINVF.ap_vscacli = round(TTARTINVF.ap_vscacli + TTARTINVF1.ap_vscacli,3), " & _
                " TTARTINVF.ap_vscapro = round(TTARTINVF.ap_vscapro + TTARTINVF1.ap_vscapro,3), " & _
                " TTARTINVF.ap_vscavar = round(TTARTINVF.ap_vscavar + TTARTINVF1.ap_vscavar,3), " & _
                " TTARTINVF.ap_vresfor = round(TTARTINVF.ap_vresfor + TTARTINVF1.ap_vresfor,3), " & _
                " TTARTINVF.ap_vgiaini = round(TTARTINVF.ap_vgiaini + TTARTINVF1.ap_vgiaini,3) " & _
                " FROM TTARTINVF INNER JOIN TTARTINVF AS TTARTINVF1 ON (TTARTINVF1.codditt = TTARTINVF.codditt) " & _
                " AND (TTARTINVF.instid = TTARTINVF1.ap_prenot) AND (TTARTINVF.ap_fase = TTARTINVF1.ap_fase) " & _
                " AND (TTARTINVF.ap_codart = TTARTINVF1.ap_codart) AND (TTARTINVF.ap_magaz = TTARTINVF1.ap_magaz) " & _
                " AND (TTARTINVF.ap_commeca = TTARTINVF1.ap_commeca) AND (TTARTINVF.ap_ubicaz = TTARTINVF1.ap_ubicaz) " & _
                " AND (TTARTINVF.ap_lotto = TTARTINVF1.ap_lotto)" & _
                " WHERE TTARTINVF.codditt = " & CStrSQL(strDitta) & _
                " AND TTARTINVF.instid = " & lIITTArtinvf & _
                " AND TTARTINVF1.instid = " & lIITTArtinvfTMP
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      ResetTblInstId("TTARTINVF", False, lIITTArtinvfTMP)

      '---------------------
      'Per gli articoli gestiti a matricola (KEYMAG - MOVMAG - MOVMATR)
      'in listsar con progressivo = -1 ho già tutti gli articoli a matricola da trattare !!!!!

      'ATTENZIONE: PER GLI ARTICOLI A MATRICOLA NON PARTI DA ARTDEF/LOTCDEF, PER CUI E' ASSUNTO CHE 
      'LA SOMMATORIA DELLE MATRICOLE DALL 01/01/1900 ALLA DATA FINE ELABORAZIONE DEVE ESSERE UGUALE 
      'A ARTDEF.AD_ESIST PER IL MAGAZZINO IN ELABORAZIONE
      strSQL = "INSERT INTO TTARTINVF (codditt, instid, ap_codart, ap_fase, ap_magaz, ap_esist," & _
                " ap_prenot, ap_ordin, ap_impeg, ap_carfor, ap_carpro, ap_carvar, ap_rescli," & _
                " ap_scacli, ap_scapro, ap_scavar, ap_resfor, ap_giaini, ap_vprenot, ap_vordin," & _
                " ap_vimpeg, ap_vcarfor, ap_vcarpro, ap_vcarvar, ap_vrescli, ap_vscacli," & _
                " ap_vscapro, ap_vscavar, ap_vresfor, ap_vgiaini, ap_sommat, ap_daordi, ap_vdaordi, " & _
                " ap_commeca, ap_lotto, ap_ubicaz, ap_matric)" & _
                " SELECT " & CStrSQL(strDitta) & ", " & lIITTArtinvf & ", ar_codart, km_fase, " & _
                nCodmaga.ToString & ", sum(mma_quant * tb_esist)," & _
                " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " & _
                " km_commeca, km_lotto, km_ubicaz, mma_matric" & _
                " FROM artico INNER JOIN (tabcaum INNER JOIN ((movmag INNER JOIN keymag ON " & _
                " (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) " & _
                " AND (movmag.mm_tipork = keymag.km_tipork) AND (movmag.mm_serie = keymag.km_serie) " & _
                " AND (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc)) " & _
                " INNER JOIN movmatr ON (movmag.mm_riga = movmatr.mma_riga) AND " & _
                " (movmag.mm_numdoc = movmatr.mma_numdoc) AND (movmag.mm_serie = movmatr.mma_serie) " & _
                " AND (movmag.mm_anno = movmatr.mma_anno) AND (movmag.mm_tipork = movmatr.mma_tipork) " & _
                " AND (movmag.codditt = movmatr.codditt)) ON tabcaum.tb_codcaum = keymag.km_causale) " & _
                " ON (artico.codditt = movmag.codditt) AND (artico.ar_codart = movmag.mm_codart)" & _
                " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                " AND artico.ar_gestmatr = 'S'" & _
                " AND artico.ar_tipoopz IN (' ', 'O')" & _
                " AND km_aammgg <= " & CDataSQL(strDatainv) & _
                " AND km_magaz = " & nCodmaga.ToString & _
                " AND exists (SELECT lsa_codart FROM listsar " & _
                            " WHERE lsa_codlsar = -1" & _
                            " AND lsa_codart = km_codart AND lsa_fase = km_fase AND listsar.codditt = keymag.codditt) " & _
                " GROUP BY ar_codart, km_fase, km_commeca, km_lotto, km_ubicaz, mma_matric "
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------
      'Se abilitata opzione (personalizzata elemac) bELEM_ConfrontoSuDispNettaCorrente
      'l'esistenza TTARTINVF.ap_esist viene decurtata del PRENOTATO
      '(ATTENZIONE! è quello corrente di LOTCPRO) in modo qundi che la rettifica non
      'avvenga sull'ESISTENZA ma sulla DISPONIBILITA' NETTA.
      If CBool(GetSettingBus("BSMGINVF", "OPZIONI", ".", "ELEM_ConfrontoSuDispNettaCorrente", "0", " ", "0")) Then 'NON DOCUMENTARE (personalizzazione ELEMAC, vedi documentazione) Then
        strSQL = "UPDATE TTARTINVF" & _
                " SET TTARTINVF.ap_esist = Round(TTARTINVF.ap_esist - lotcpro.lp_prenot, 3)" & _
                " FROM TTARTINVF INNER JOIN lotcpro ON TTARTINVF.codditt = lotcpro.codditt " & _
                " AND TTARTINVF.ap_codart = lotcpro.lp_codart AND TTARTINVF.ap_magaz = lotcpro.lp_magaz " & _
                " AND TTARTINVF.ap_fase = lotcpro.lp_fase AND TTARTINVF.ap_commeca = lotcpro.lp_commeca " & _
                " AND TTARTINVF.ap_lotto = lotcpro.lp_lotto AND TTARTINVF.ap_ubicaz = lotcpro.lp_ubicaz" & _
                " WHERE TTARTINVF.codditt = " & CStrSQL(strDitta) & _
                " AND TTARTINVF.instid = " & lIITTArtinvf
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function RiempiTTArtinvtc(ByVal strDitta As String, ByVal strDatainv As String, _
                                              ByVal nCodmaga As Integer, ByVal strDtulap As String, _
                                              ByVal nCausInv As Integer, ByVal lIITTArtinvtc As Integer) As Boolean
    '---------------------
    'calcolo l'esistenza degli articoli TCO in Business da artdefx/artdef/lotcdef + i movim 
    'di magazzino fino alla data fine elab
    Dim strSQL As String = ""
    Dim lIITTArtinvtcTMP As Integer = 0
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim strTTARTINVTCCampi As String = "instid, codditt, apt_codart, apt_magaz, apt_fase, apt_esist01, apt_esist02," & _
            " apt_esist03, apt_esist04, apt_esist05, apt_esist06, apt_esist07, apt_esist08, apt_esist09, apt_esist10," & _
            " apt_esist11, apt_esist12, apt_esist13, apt_esist14, apt_esist15, apt_esist16, apt_esist17, apt_esist18," & _
            " apt_esist19, apt_esist20, apt_esist21, apt_esist22, apt_esist23, apt_esist24, apt_prenot01, apt_prenot02," & _
            " apt_prenot03, apt_prenot04, apt_prenot05, apt_prenot06, apt_prenot07, apt_prenot08, apt_prenot09," & _
            " apt_prenot10, apt_prenot11, apt_prenot12, apt_prenot13, apt_prenot14, apt_prenot15, apt_prenot16," & _
            " apt_prenot17, apt_prenot18, apt_prenot19, apt_prenot20, apt_prenot21, apt_prenot22, apt_prenot23," & _
            " apt_prenot24, apt_ordin01, apt_ordin02, apt_ordin03, apt_ordin04, apt_ordin05, apt_ordin06, apt_ordin07," & _
            " apt_ordin08, apt_ordin09, apt_ordin10, apt_ordin11, apt_ordin12, apt_ordin13, apt_ordin14, apt_ordin15," & _
            " apt_ordin16, apt_ordin17, apt_ordin18, apt_ordin19, apt_ordin20, apt_ordin21, apt_ordin22, apt_ordin23," & _
            " apt_ordin24, apt_impeg01, apt_impeg02 , apt_impeg03, apt_impeg04, apt_impeg05, apt_impeg06, apt_impeg07," & _
            " apt_impeg08, apt_impeg09, apt_impeg10, apt_impeg11, apt_impeg12, apt_impeg13, apt_impeg14, apt_impeg15," & _
            " apt_impeg16, apt_impeg17, apt_impeg18, apt_impeg19, apt_impeg20, apt_impeg21, apt_impeg22, apt_impeg23," & _
            " apt_impeg24, apt_commeca, apt_lotto, apt_ubicaz"
    Try
      ResetTblInstId("TTARTINVTC", False, lIITTArtinvtc)

      '---------------------
      'Ricopia i dati di ARTDEFTC
      strSQL = "INSERT INTO TTARTINVTC (" & strTTARTINVTCCampi & ")" & _
              " SELECT " & lIITTArtinvtc & ", " & CStrSQL(strDitta) & ", adt_codart, adt_magaz, adt_fase, " & _
              " adt_esist01, adt_esist02, adt_esist03, adt_esist04, adt_esist05, adt_esist06, adt_esist07, adt_esist08," & _
              " adt_esist09, adt_esist10, adt_esist11, adt_esist12 , adt_esist13, adt_esist14, adt_esist15, adt_esist16," & _
              " adt_esist17, adt_esist18, adt_esist19, adt_esist20, adt_esist21, adt_esist22, adt_esist23, adt_esist24," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " adt_commeca, adt_lotto, adt_ubicaz" & _
              " FROM (artico INNER JOIN lotcdeftc ON (artico.ar_codart = lotcdeftc.adt_codart) AND (artico.codditt = lotcdeftc.codditt)) INNER JOIN LISTSAR ON (lotcdeftc.adt_commeca = LISTSAR.lsa_commeca) AND (lotcdeftc.adt_lotto = LISTSAR.lsa_lotto) AND (lotcdeftc.adt_ubicaz = LISTSAR.lsa_ubicaz) AND (lotcdeftc.adt_fase = LISTSAR.lsa_fase) AND (lotcdeftc.adt_codart = LISTSAR.lsa_codart) AND (lotcdeftc.codditt = LISTSAR.codditt) "
      strSQL = strSQL & " WHERE lotcdeftc.codditt = " & CStrSQL(strDitta) & _
              " AND adt_magaz = " & nCodmaga.ToString & _
              " AND lsa_codlsar = -1 " & _
              " AND ar_codtagl <> 0 " & _
              " AND artico.ar_tipoopz IN (' ', 'O')" & _
              " GROUP BY adt_codart, adt_fase, adt_commeca, adt_lotto, adt_ubicaz, adt_magaz, adt_esist01, adt_esist02, adt_esist03, adt_esist04, adt_esist05, adt_esist06, adt_esist07, adt_esist08," & _
              " adt_esist09, adt_esist10, adt_esist11, adt_esist12 , adt_esist13, adt_esist14, adt_esist15, adt_esist16," & _
              " adt_esist17, adt_esist18, adt_esist19, adt_esist20, adt_esist21, adt_esist22, adt_esist23, adt_esist24"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------
      'ora inserisco in TTARTINVTC gli articoli non presenti in artdeftc e in keymag
      strSQL = "INSERT INTO TTARTINVTC (" & strTTARTINVTCCampi & ")" & _
          " SELECT " & lIITTArtinvtc & ", " & CStrSQL(strDitta) & ", lsa_codart, " & nCodmaga.ToString & ", lsa_fase, " & _
          " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " & _
          " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " & _
          " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " & _
          " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, lsa_commeca, lsa_lotto, lsa_ubicaz " & _
          " FROM LISTSAR INNER JOIN artico ON listsar.codditt = artico.codditt AND listsar.lsa_codart = artico.ar_codart "
      strSQL = strSQL & " WHERE listsar.codditt = " & CStrSQL(strDitta) & _
          " AND  NOT EXISTS (SELECT apt_codart FROM TTARTINVTC" & _
                           " WHERE codditt = " & CStrSQL(strDitta) & _
                           " AND apt_codart = lsa_codart" & _
                           " AND apt_fase = lsa_fase" & _
                           " AND apt_commeca = lsa_commeca" & _
                           " AND apt_lotto = lsa_lotto" & _
                           " AND apt_ubicaz = lsa_ubicaz" & _
                           " AND apt_magaz = " & nCodmaga.ToString & _
                           " AND instid = " & lIITTArtinvtc & ")" & _
          " AND lsa_codlsar = -1 " & _
          " AND ar_codtagl <> 0 " & _
          " GROUP BY lsa_codart, lsa_fase, lsa_commeca, lsa_lotto, lsa_ubicaz"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------
      'Aggiornamento da MOVMAG, KEYMAG, TABCAUM
      lIITTArtinvtcTMP = GetTblInstId("TTARTINVTC", False)

      'creo una seconda serie di record (definiti con instid specifico contenenti, per gli articoli selezionati,
      'i movimenti da aggiungere ad artdef per avere l'esistenza a data
      strSQL = "INSERT INTO TTARTINVTC (" & strTTARTINVTCCampi & ")" & _
          " SELECT " & lIITTArtinvtcTMP & ", " & CStrSQL(strDitta) & ", ar_codart, " & nCodmaga.ToString & ", km_fase, " & _
          " sum(movmagtc.mm_quant01 * km_carscar), sum(movmagtc.mm_quant02 * km_carscar), sum(movmagtc.mm_quant03 * km_carscar), " & _
          " sum(movmagtc.mm_quant04 * km_carscar), sum(movmagtc.mm_quant05 * km_carscar), sum(movmagtc.mm_quant06 * km_carscar), " & _
          " sum(movmagtc.mm_quant07 * km_carscar), sum(movmagtc.mm_quant08 * km_carscar), sum(movmagtc.mm_quant09 * km_carscar), " & _
          " sum(movmagtc.mm_quant10 * km_carscar), sum(movmagtc.mm_quant11 * km_carscar), sum(movmagtc.mm_quant12 * km_carscar), " & _
          " sum(movmagtc.mm_quant13 * km_carscar), sum(movmagtc.mm_quant14 * km_carscar), sum(movmagtc.mm_quant15 * km_carscar), " & _
          " sum(movmagtc.mm_quant16 * km_carscar), sum(movmagtc.mm_quant17 * km_carscar), sum(movmagtc.mm_quant18 * km_carscar), " & _
          " sum(movmagtc.mm_quant19 * km_carscar), sum(movmagtc.mm_quant20 * km_carscar), sum(movmagtc.mm_quant21 * km_carscar), " & _
          " sum(movmagtc.mm_quant22 * km_carscar), sum(movmagtc.mm_quant23 * km_carscar), sum(movmagtc.mm_quant24 * km_carscar), " & _
          lIITTArtinvtc & ", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " & _
          " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " & _
          " CASE WHEN ar_gescomm = 'S' THEN km_commeca ELSE 0 END, " & _
          " CASE WHEN ar_geslotti = 'S' THEN km_lotto ELSE 0 END, " & _
          " CASE WHEN ar_gesubic = 'S' THEN km_ubicaz ELSE ' ' END " & _
          " FROM (tabcaum INNER JOIN ((movmag INNER JOIN movmagtc ON (movmag.mm_riga = movmagtc.mm_riga) AND (movmag.mm_numdoc = movmagtc.mm_numdoc)" & _
          " AND (movmag.mm_serie = movmagtc.mm_serie) AND (movmag.mm_anno = movmagtc.mm_anno) AND (movmag.mm_tipork = movmagtc.mm_tipork) AND (movmag.codditt = movmagtc.codditt))" & _
          " INNER JOIN (artico INNER JOIN keymag ON (artico.ar_codart = keymag.km_codart) AND (artico.codditt = keymag.codditt)) ON (movmag.codditt = keymag.codditt)" & _
          " AND (movmag.mm_riga = keymag.km_riga) AND (movmag.mm_tipork = keymag.km_tipork) AND (movmag.mm_serie = keymag.km_serie) AND (movmag.mm_anno = keymag.km_anno)" & _
          " AND (movmag.mm_numdoc = keymag.km_numdoc)) ON tabcaum.tb_codcaum = keymag.km_causale) INNER JOIN TTARTINVTC ON (keymag.km_fase = TTARTINVTC.apt_fase) " & _
          " AND (keymag.km_magaz = TTARTINVTC.apt_magaz) " & _
          " AND (keymag.km_codart = TTARTINVTC.apt_codart)" & _
          " AND (keymag.codditt = TTARTINVTC.codditt)"
      strSQL = strSQL & " WHERE TTARTINVTC.codditt = " & CStrSQL(strDitta) & _
          " AND TTARTINVTC.instid = " & lIITTArtinvtc & _
          " AND km_aammgg > " & CDataSQL(strDtulap) & _
          " AND km_aammgg <= " & CDataSQL(strDatainv) & _
          " AND km_magaz = " & nCodmaga.ToString & _
          " AND artico.ar_tipoopz IN (' ', 'O')" & _
          " AND ar_codtagl <> 0 " & _
          " AND CASE WHEN ar_gesubic = 'S' THEN keymag.km_ubicaz ELSE ' ' END = TTARTINVTC.apt_ubicaz " & _
          " AND CASE WHEN ar_geslotti = 'S' THEN keymag.km_lotto ELSE 0 END = TTARTINVTC.apt_lotto " & _
          " AND CASE WHEN ar_gescomm = 'S' THEN keymag.km_commeca ELSE 0 END = TTARTINVTC.apt_commeca " & _
          " AND exists (SELECT lsa_codart FROM listsar INNER JOIN artico ON listsar.codditt = artico.codditt " & _
          "                                                   AND listsar.lsa_codart = artico.ar_codart " & _
                      " WHERE lsa_codlsar = -1 " & _
                      " AND lsa_codart = km_codart AND lsa_fase = km_fase " & _
                      " AND lsa_commeca = CASE WHEN ar_gescomm = 'S' THEN km_commeca ELSE 0 END " & _
                      " AND lsa_lotto = CASE WHEN ar_geslotti = 'S' THEN km_lotto ELSE 0 END " & _
                      " AND lsa_ubicaz = CASE WHEN ar_gesubic = 'S' THEN km_ubicaz ELSE ' ' END  " & _
                      " AND listsar.codditt = keymag.codditt) " & _
           " GROUP BY ar_codart, ar_gescomm, ar_geslotti, ar_gesubic, km_fase, " & _
           " CASE WHEN ar_gescomm = 'S' THEN km_commeca ELSE 0 END, " & _
           " CASE WHEN ar_geslotti = 'S' THEN km_lotto ELSE  0 END, " & _
           " CASE WHEN ar_gesubic = 'S' THEN km_ubicaz ELSE ' ' END "

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------
      'aggiungo i valori sopra determinati ai record del primo instid per avere l'esistenza da data di tutti gli articoli
      strSQL = " update ttartinvtc set " & _
          " ttartinvtc.apt_esist01 = round(ttartinvtc.apt_esist01 + ttartinvtc1.apt_esist01,3), " & _
          " ttartinvtc.apt_esist02 = round(ttartinvtc.apt_esist02 + ttartinvtc1.apt_esist02,3), " & _
          " ttartinvtc.apt_esist03 = round(ttartinvtc.apt_esist03 + ttartinvtc1.apt_esist03,3), " & _
          " ttartinvtc.apt_esist04 = round(ttartinvtc.apt_esist04 + ttartinvtc1.apt_esist04,3), " & _
          " ttartinvtc.apt_esist05 = round(ttartinvtc.apt_esist05 + ttartinvtc1.apt_esist05,3), " & _
          " ttartinvtc.apt_esist06 = round(ttartinvtc.apt_esist06 + ttartinvtc1.apt_esist06,3), " & _
          " ttartinvtc.apt_esist07 = round(ttartinvtc.apt_esist07 + ttartinvtc1.apt_esist07,3), " & _
          " ttartinvtc.apt_esist08 = round(ttartinvtc.apt_esist08 + ttartinvtc1.apt_esist08,3), " & _
          " ttartinvtc.apt_esist09 = round(ttartinvtc.apt_esist09 + ttartinvtc1.apt_esist09,3), " & _
          " ttartinvtc.apt_esist10 = round(ttartinvtc.apt_esist10 + ttartinvtc1.apt_esist10,3), " & _
          " ttartinvtc.apt_esist11 = round(ttartinvtc.apt_esist11 + ttartinvtc1.apt_esist11,3), " & _
          " ttartinvtc.apt_esist12 = round(ttartinvtc.apt_esist12 + ttartinvtc1.apt_esist12,3), " & _
          " ttartinvtc.apt_esist13 = round(ttartinvtc.apt_esist13 + ttartinvtc1.apt_esist13,3), " & _
          " ttartinvtc.apt_esist14 = round(ttartinvtc.apt_esist14 + ttartinvtc1.apt_esist14,3), " & _
          " ttartinvtc.apt_esist15 = round(ttartinvtc.apt_esist15 + ttartinvtc1.apt_esist15,3), " & _
          " ttartinvtc.apt_esist16 = round(ttartinvtc.apt_esist16 + ttartinvtc1.apt_esist16,3), " & _
          " ttartinvtc.apt_esist17 = round(ttartinvtc.apt_esist17 + ttartinvtc1.apt_esist17,3), " & _
          " ttartinvtc.apt_esist18 = round(ttartinvtc.apt_esist18 + ttartinvtc1.apt_esist18,3), " & _
          " ttartinvtc.apt_esist19 = round(ttartinvtc.apt_esist19 + ttartinvtc1.apt_esist19,3), " & _
          " ttartinvtc.apt_esist20 = round(ttartinvtc.apt_esist20 + ttartinvtc1.apt_esist20,3), " & _
          " ttartinvtc.apt_esist21 = round(ttartinvtc.apt_esist21 + ttartinvtc1.apt_esist21,3), "
      strSQL = strSQL & " ttartinvtc.apt_esist22 = round(ttartinvtc.apt_esist22 + ttartinvtc1.apt_esist22,3), " & _
          " ttartinvtc.apt_esist23 = round(ttartinvtc.apt_esist23 + ttartinvtc1.apt_esist23,3), " & _
          " ttartinvtc.apt_esist24 = round(ttartinvtc.apt_esist24 + ttartinvtc1.apt_esist24,3) " & _
          " FROM TTARTINVTC INNER JOIN TTARTINVTC AS TTARTINVTC1 ON (TTARTINVTC1.codditt = TTARTINVTC.codditt) AND (TTARTINVTC.apt_fase = TTARTINVTC1.apt_fase)" & _
          " AND (TTARTINVTC.apt_commeca = TTARTINVTC1.apt_commeca) AND (TTARTINVTC.apt_lotto = TTARTINVTC1.apt_lotto) AND (TTARTINVTC.apt_ubicaz = TTARTINVTC1.apt_ubicaz)" & _
          " AND (TTARTINVTC.apt_codart = TTARTINVTC1.apt_codart) AND (TTARTINVTC.apt_magaz = TTARTINVTC1.apt_magaz)" & _
          " WHERE ttartinvtc.codditt = " & CStrSQL(strDitta) & _
          " AND ttartinvtc.instid = " & lIITTArtinvtc & _
          " AND ttartinvtc1.instid = " & lIITTArtinvtcTMP
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      ResetTblInstId("TTARTINVTC", False, lIITTArtinvtcTMP)

      '---------------------
      strSQL = "SELECT movmag.mm_codart as am_codart, movmag.mm_fase as am_fase, movmag.mm_commeca as am_commeca," & _
              " movmag.mm_lotto as am_lotto, movmag.mm_ubicaz as am_ubicaz, sum(movmagtc.mm_quant01) as am_quant01, " & _
              " sum(movmagtc.mm_quant02) as am_quant02, sum(movmagtc.mm_quant03) as am_quant03, sum(movmagtc.mm_quant04) as am_quant04," & _
              " sum(movmagtc.mm_quant05) as am_quant05, sum(movmagtc.mm_quant06) as am_quant06, sum(movmagtc.mm_quant07) as am_quant07," & _
              " sum(movmagtc.mm_quant08) as am_quant08, sum(movmagtc.mm_quant09) as am_quant09, sum(movmagtc.mm_quant10) as am_quant10," & _
              " sum(movmagtc.mm_quant11) as am_quant11, sum(movmagtc.mm_quant12) as am_quant12, sum(movmagtc.mm_quant13) as am_quant13," & _
              " sum(movmagtc.mm_quant14) as am_quant14, sum(movmagtc.mm_quant15) as am_quant15, sum(movmagtc.mm_quant16) as am_quant16," & _
              " sum(movmagtc.mm_quant17) as am_quant17, sum(movmagtc.mm_quant18) as am_quant18, sum(movmagtc.mm_quant19) as am_quant19," & _
              " sum(movmagtc.mm_quant20) as am_quant20, sum(movmagtc.mm_quant21) as am_quant21, sum(movmagtc.mm_quant22) as am_quant22," & _
              " sum(movmagtc.mm_quant23) as am_quant23, sum(movmagtc.mm_quant24) as am_quant24" & _
              " FROM ((testmag INNER JOIN movmag ON (testmag.codditt = movmag.codditt) AND (testmag.tm_tipork = movmag.mm_tipork)" & _
              " AND (testmag.tm_serie = movmag.mm_serie) AND (testmag.tm_anno = movmag.mm_anno) AND (testmag.tm_numdoc = movmag.mm_numdoc))" & _
              " INNER JOIN movmagtc ON (movmag.mm_riga = movmagtc.mm_riga) AND (movmag.mm_numdoc = movmagtc.mm_numdoc) AND (movmag.mm_serie = movmagtc.mm_serie)" & _
              " AND (movmag.mm_anno = movmagtc.mm_anno) AND (movmag.mm_tipork = movmagtc.mm_tipork) AND (movmag.codditt = movmagtc.codditt)) " & _
              " INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart " & _
              " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
              " AND tm_datdoc = " & CDataSQL(strDatainv) & _
              " AND tm_causale = " & nCausInv.ToString & " AND tm_magaz = " & nCodmaga.ToString & _
              " AND ar_codtagl <> 0 " & _
              " GROUP BY movmag.mm_codart, movmag.mm_fase, movmag.mm_commeca, movmag.mm_lotto, movmag.mm_ubicaz " & _
              " ORDER BY movmag.mm_codart, movmag.mm_fase, movmag.mm_commeca, movmag.mm_lotto, movmag.mm_ubicaz "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      For Each dtrT As DataRow In dttTmp.Rows
        'testo se l'articolo esiste in TTARTINVTC
        strSQL = "SELECT apt_codart FROM TTARTINVTC" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTArtinvtc & _
                " AND apt_codart = " & CStrSQL(dtrT!am_codart) & _
                " AND apt_magaz = " & nCodmaga.ToString & _
                " AND apt_fase = " & dtrT!am_fase.ToString & _
                " AND apt_commeca = " & dtrT!am_commeca.ToString & _
                " AND apt_lotto = " & dtrT!am_lotto.ToString & _
                " AND apt_ubicaz = " & CStrSQL(dtrT!am_ubicaz) & " "
        dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp1.Rows.Count = 0 Then 'INSERT
          strSQL = "INSERT INTO TTARTINVTC (" & strTTARTINVTCCampi & ")" & _
                    " VALUES(" & lIITTArtinvtc & ", " & CStrSQL(strDitta) & ", " & CStrSQL(dtrT!am_codart) & ", " & _
                    nCodmaga.ToString & ", " & dtrT!am_fase.ToString & "," & _
                    " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " & _
                    CDblSQL(NTSCDec(dtrT!am_quant01)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant02)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!am_quant03)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant04)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!am_quant05)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant06)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!am_quant07)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant08)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!am_quant09)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant10)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!am_quant11)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant12)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!am_quant13)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant14)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!am_quant15)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant16)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!am_quant17)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant18)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!am_quant19)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant20)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!am_quant21)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant22)) & ", " & _
                    CDblSQL(NTSCDec(dtrT!am_quant23)) & ", " & CDblSQL(NTSCDec(dtrT!am_quant24)) & ", " & _
                    " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " & _
                    " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " & _
                    dtrT!am_commeca.ToString & ", " & dtrT!am_lotto.ToString & ", " & CStrSQL(dtrT!am_ubicaz) & ")"
        Else 'UPDATE
          strSQL = "UPDATE TTARTINVTC" & _
                    " SET apt_prenot01 = " & CDblSQL(NTSCDec(dtrT!am_quant01)) & "," & _
                    " apt_prenot02 = " & CDblSQL(NTSCDec(dtrT!am_quant02)) & ", apt_prenot03 = " & CDblSQL(NTSCDec(dtrT!am_quant03)) & "," & _
                    " apt_prenot04 = " & CDblSQL(NTSCDec(dtrT!am_quant04)) & ", apt_prenot05 = " & CDblSQL(NTSCDec(dtrT!am_quant05)) & "," & _
                    " apt_prenot06 = " & CDblSQL(NTSCDec(dtrT!am_quant06)) & ", apt_prenot07 = " & CDblSQL(NTSCDec(dtrT!am_quant07)) & "," & _
                    " apt_prenot08 = " & CDblSQL(NTSCDec(dtrT!am_quant08)) & ", apt_prenot09 = " & CDblSQL(NTSCDec(dtrT!am_quant09)) & "," & _
                    " apt_prenot10 = " & CDblSQL(NTSCDec(dtrT!am_quant10)) & ", apt_prenot11 = " & CDblSQL(NTSCDec(dtrT!am_quant11)) & "," & _
                    " apt_prenot12 = " & CDblSQL(NTSCDec(dtrT!am_quant12)) & ", apt_prenot13 = " & CDblSQL(NTSCDec(dtrT!am_quant13)) & "," & _
                    " apt_prenot14 = " & CDblSQL(NTSCDec(dtrT!am_quant14)) & ", apt_prenot15 = " & CDblSQL(NTSCDec(dtrT!am_quant15)) & "," & _
                    " apt_prenot16 = " & CDblSQL(NTSCDec(dtrT!am_quant16)) & ", apt_prenot17 = " & CDblSQL(NTSCDec(dtrT!am_quant17)) & "," & _
                    " apt_prenot18 = " & CDblSQL(NTSCDec(dtrT!am_quant18)) & ", apt_prenot19 = " & CDblSQL(NTSCDec(dtrT!am_quant19)) & "," & _
                    " apt_prenot20 = " & CDblSQL(NTSCDec(dtrT!am_quant20)) & ", apt_prenot21 = " & CDblSQL(NTSCDec(dtrT!am_quant21)) & "," & _
                    " apt_prenot22 = " & CDblSQL(NTSCDec(dtrT!am_quant22)) & ", apt_prenot23 = " & CDblSQL(NTSCDec(dtrT!am_quant23)) & "," & _
                    " apt_prenot24 = " & CDblSQL(NTSCDec(dtrT!am_quant24)) & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND instid = " & lIITTArtinvtc & _
                    " AND apt_codart = " & CStrSQL(dtrT!am_codart) & _
                    " AND apt_fase = " & dtrT!am_fase.ToString & _
                    " AND apt_commeca = " & dtrT!am_commeca.ToString & _
                    " AND apt_lotto = " & dtrT!am_lotto.ToString & _
                    " AND apt_ubicaz = " & CStrSQL(dtrT!am_ubicaz) & _
                    " AND apt_magaz = " & nCodmaga.ToString
        End If
        dttTmp1.Clear()
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next    'For Each dtrT As DataRow In dttTmp.Rows

      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp1.Clear()
    End Try
  End Function

  Public Overridable Function GetTTartinvfCodart(ByVal strDitta As String, ByVal lIITTArtinvf As Integer, _
                                                ByVal nCodmaga As Integer, ByRef dtrOrig As DataRow, _
                                                ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strUbicaz As String = ""
    Dim strMatric As String = ""
    Try
      strUbicaz = NTSCStr(dtrOrig!am_ubicaz)
      If strUbicaz = "" Then strUbicaz = " "

      strMatric = NTSCStr(dtrOrig!am_matric)
      If strMatric = "" Then strMatric = " "

      strSQL = "SELECT ap_codart FROM TTARTINVF" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTArtinvf.ToString & _
              " AND ap_codart = " & CStrSQL(dtrOrig!am_codart) & _
              " AND ap_magaz = " & nCodmaga.ToString & _
              " AND ap_fase = " & dtrOrig!am_fase.ToString & _
              " AND ap_commeca = " & dtrOrig!am_commeca.ToString & _
              " AND ap_lotto = " & dtrOrig!am_lotto.ToString & _
              " AND ap_ubicaz = " & CStrSQL(strUbicaz) & _
              " AND ap_matric = " & CStrSQL(strMatric)
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function IsArticoInLista(ByVal strSQLArticoliInventariati As String, ByVal strCodart As String, ByVal nFase As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = strSQLArticoliInventariati & _
               " AND ar_codart = " & CStrSQL(strCodart)
      If nFase <> 0 Then strSQL = strSQL & " AND af_fase = " & nFase.ToString

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      dttTmp.Clear()
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function AddArticoInTTArtinvf(ByVal strDitta As String, ByVal lIITTArtinvf As Integer, _
                                                ByVal nCodmaga As Integer, ByRef dtrOrig As DataRow, _
                                                ByVal dPrezzo As Decimal) As Boolean
    Dim strSQL As String = ""
    Dim strUbicaz As String = ""
    Dim strMatric As String = ""
    Try
      strUbicaz = NTSCStr(dtrOrig!am_ubicaz)
      If strUbicaz = "" Then strUbicaz = " "

      strMatric = NTSCStr(dtrOrig!am_matric)
      If strMatric = "" Then strMatric = " "

      strSQL = "INSERT INTO TTARTINVF (codditt, instid, ap_codart, ap_fase," & _
              " ap_magaz, ap_esist, ap_sommat, ap_daordi, ap_vdaordi, ap_commeca, ap_lotto, ap_ubicaz, ap_matric)" & _
              " VALUES(" & CStrSQL(strDitta) & ", " & lIITTArtinvf & ", " & _
              CStrSQL(dtrOrig!am_codart) & ", " & dtrOrig!am_fase.ToString & ", " & _
              nCodmaga.ToString & ", 0, " & CDblSQL(dPrezzo) & ", " & _
              CDblSQL(NTSCDec(dtrOrig!am_quant)) & ", 1, " & dtrOrig!am_commeca.ToString & ", " & _
              dtrOrig!am_lotto.ToString & ", " & CStrSQL(strUbicaz) & ", " & CStrSQL(strMatric) & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggArticoInTTArtinvf(ByVal strDitta As String, ByVal lIITTArtinvf As Integer, _
                                                ByVal nCodmaga As Integer, ByRef dtrOrig As DataRow, _
                                                ByVal dPrezzo As Decimal) As Boolean
    Dim strSQL As String = ""
    Dim strUbicaz As String = ""
    Dim strMatric As String = ""
    Try
      strUbicaz = NTSCStr(dtrOrig!am_ubicaz)
      If strUbicaz = "" Then strUbicaz = " "

      strMatric = NTSCStr(dtrOrig!am_matric)
      If strMatric = "" Then strMatric = " "

      strSQL = "UPDATE TTARTINVF" & _
              " SET ap_vdaordi = 1," & _
              " ap_daordi = " & CDblSQL(NTSCDec(dtrOrig!am_quant)) & "," & _
              " ap_sommat = " & CDblSQL(dPrezzo) & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTArtinvf & _
              " AND ap_codart = " & CStrSQL(dtrOrig!am_codart) & _
              " AND ap_fase = " & dtrOrig!am_fase.ToString & _
              " AND ap_magaz = " & nCodmaga.ToString & _
              " AND ap_commeca = " & dtrOrig!am_commeca.ToString & _
              " AND ap_lotto = " & dtrOrig!am_lotto.ToString & _
              " AND ap_ubicaz = " & CStrSQL(strUbicaz) & _
              " AND ap_matric = " & CStrSQL(strMatric)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggPrezzoInTTArtinvf(ByVal strDitta As String, ByVal lIITTArtinvf As Integer, _
                                                     ByVal nCodmaga As Integer, ByRef dtrOrig As DataRow, _
                                                     ByVal dPrezzo As Decimal) As Boolean
    Dim strSQL As String = ""
    Dim strUbicaz As String = ""
    Dim strMatric As String = ""
    Try
      strUbicaz = NTSCStr(dtrOrig!am_ubicaz)
      If strUbicaz = "" Then strUbicaz = " "

      strMatric = NTSCStr(dtrOrig!am_matric)
      If strMatric = "" Then strMatric = " "

      strSQL = "UPDATE TTARTINVF" & _
              " SET ap_vdaordi = 1," & _
              " ap_sommat = " & CDblSQL(dPrezzo) & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTArtinvf & _
              " AND ap_codart = " & CStrSQL(dtrOrig!am_codart) & _
              " AND ap_fase = " & dtrOrig!am_fase.ToString & _
              " AND ap_magaz = " & nCodmaga.ToString & _
              " AND ap_commeca = " & dtrOrig!am_commeca.ToString & _
              " AND ap_lotto = " & dtrOrig!am_lotto.ToString & _
              " AND ap_ubicaz = " & CStrSQL(strUbicaz) & _
              " AND ap_matric = " & CStrSQL(strMatric)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckEsistArtMatricola(ByVal strDitta As String, ByVal nCodmaga As Integer, _
                                                     ByVal lIITTArtinvf As Integer, ByVal strDtulap As String, _
                                                     ByVal strDatainv As String, ByRef dttOut As DataTable) As Boolean
    'controllo che la sommatoria delle esistenze degli articoli a matricola calcolata su movmag-testmag-keymag dal 01/01/1900 a data fine elab
    'corrisponda a artdef + movmag-keymag-testmag , altrimenti la stampa dell'inventario non sarebbe corretta
    Dim strSQL As String = ""
    Dim lIITTTmp As Integer = 0
    Dim lIITTTmp1 As Integer = 0
    Try
      lIITTTmp = GetTblInstId("TTARTPRO", False)
      ResetTblInstId("TTARTPRO", False, lIITTTmp)
      lIITTTmp1 = GetTblInstId("TTARTPRO", False)
      ResetTblInstId("TTARTPRO", False, lIITTTmp1)

      strSQL = "INSERT INTO ttartpro (codditt, instid, ap_codart, ap_fase, ap_magaz, ap_esist)" & _
               "SELECT " & CStrSQL(strDitta) & ", " & lIITTTmp & ", ap_codart, ap_fase, ap_magaz, sum(ap_esist) " & _
               " FROM ttartinvf INNER JOIN artico ON ttartinvf.codditt = artico.codditt " & _
               " AND ttartinvf.ap_codart = artico.ar_codart " & _
               " WHERE ttartinvf.codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTArtinvf & _
               " AND ar_gestmatr = 'S'" & _
               " GROUP BY ap_codart, ap_fase, ap_magaz"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'calcolo l'esistenza a data come farebbe l'inventario e la memorizzo in ap_ordin
      strSQL = "UPDATE ttartpro SET ap_ordin = ad_esist " & _
               " FROM ttartpro INNER JOIN artdef ON ttartpro.codditt = artdef.codditt " & _
               " AND ttartpro.ap_codart = artdef.ad_codart AND ttartpro.ap_fase = artdef.ad_fase " & _
               " AND ttartpro.ap_magaz = artdef.ad_magaz " & _
               " WHERE ttartpro.codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTTmp
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "INSERT INTO ttartpro (codditt, instid, ap_codart, ap_fase, ap_magaz, ap_esist, ap_prenot)" & _
                "SELECT " & CStrSQL(strDitta) & ", " & lIITTTmp1 & ", ar_codart, km_fase, " & nCodmaga.ToString & ", sum(mm_quant * tb_esist), " & lIITTTmp & _
                " FROM tabcaum, movmag, artico, keymag, TTARTPRO " & _
                " WHERE tabcaum.tb_codcaum = keymag.km_causale " & _
                " AND artico.ar_codart = keymag.km_codart AND artico.codditt = keymag.codditt " & _
                " AND keymag.km_magaz = ttartpro.ap_magaz AND keymag.km_codart = ttartpro.ap_codart " & _
                " AND keymag.km_fase = ttartpro.ap_fase AND keymag.codditt = ttartpro.codditt " & _
                " AND keymag.codditt = ttartpro.codditt AND movmag.codditt = keymag.codditt " & _
                " AND movmag.mm_riga = keymag.km_riga AND movmag.mm_tipork = keymag.km_tipork " & _
                " AND movmag.mm_serie = keymag.km_serie AND movmag.mm_anno = keymag.km_anno " & _
                " AND movmag.mm_numdoc = keymag.km_numdoc " & _
                " AND TTARTPRO.codditt = " & CStrSQL(strDitta) & _
                " AND TTARTPRO.instid = " & lIITTTmp & _
                " AND artico.ar_gestmatr = 'S'" & _
                " AND artico.ar_tipoopz IN (' ', 'O')" & _
                " AND km_aammgg > " & CDataSQL(strDtulap) & _
                " AND km_aammgg <= " & CDataSQL(strDatainv) & _
                " AND km_magaz = " & nCodmaga.ToString & _
                " GROUP BY ar_codart, km_fase "
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = " UPDATE ttartpro SET " & _
          " ttartpro.ap_ordin = round(ttartpro.ap_ordin + ttartpro1.ap_esist,3) " & _
          " FROM ttartpro INNER JOIN ttartpro AS ttartpro1 ON (ttartpro1.codditt = ttartpro.codditt) " & _
          " AND (ttartpro.instid = ttartpro1.ap_prenot) AND (ttartpro.ap_fase = ttartpro1.ap_fase) " & _
          " AND (ttartpro.ap_codart = ttartpro1.ap_codart) AND (ttartpro.ap_magaz = ttartpro1.ap_magaz) " & _
          " WHERE ttartpro.codditt = " & CStrSQL(strDitta) & _
          " AND ttartpro.instid = " & lIITTTmp & _
          " AND ttartpro1.instid = " & lIITTTmp1
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "SELECT ap_codart, ap_fase, ap_esist, ap_ordin " & _
               " FROM ttartpro WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTTmp & _
               " AND ap_esist <> ap_ordin"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      ResetTblInstId("TTARTPRO", False, lIITTTmp)
      ResetTblInstId("TTARTPRO", False, lIITTTmp1)
    End Try
  End Function


  Public Overridable Function CountArticoliNonPresentiInOrigInventario(ByVal strDitta As String, ByVal lIITTArtinvf As Integer) As Integer
    'Ora controllo se in TTARTINVF ci sono record con il campo ap_vdaordi ancora uguale a 0
    'cioe articoli che non sono presenti nell'origine inventario a parità do lotto/matricola/....,
    Dim strSQL As String = ""
    Dim nRec As Integer = 0
    Dim dttTmp As New DataTable
    Try
      CountArticoliNonPresentiInOrigInventario = 0
      strSQL = "SELECT count(ap_vdaordi) as nrec FROM TTARTINVF" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTArtinvf & _
              " AND ap_vdaordi = 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then nRec = NTSCInt(dttTmp.Rows(0)!nrec)
      dttTmp.Clear()

      Return nRec

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function IgnoraArticoliNonPresentiInOrigInventario(ByVal strDitta As String, ByVal lIITTArtinvf As Integer) As Boolean
    Dim strSQL As String = ""
    Dim nRec As Integer = 0
    Dim dttTmp As New DataTable
    Try
      strSQL = "UPDATE TTARTINVF " & _
               " SET ap_daordi = 0," & _
               "     ap_vdaordi = 1 " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND instid = " & lIITTArtinvf & _
               "   AND ap_vdaordi = 0"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArticoliDaCorreggere(ByVal strDitta As String, ByVal lIITTArtinvf As Integer, _
                                                      ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TTARTINVF.*, ar_gestmatr " & _
                " FROM TTARTINVF INNER JOIN artico ON TTARTINVF.codditt = artico.codditt " & _
                " AND TTARTINVF.ap_codart = artico.ar_codart" & _
                " WHERE TTARTINVF.codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTArtinvf & _
                " AND ap_vdaordi = 1"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetArticoliDaCorreggereTCO(ByVal strDitta As String, ByVal lIITTArtinvtc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 * FROM ttartinvtc WHERE codditt = " & CStrSQL(strSQL) & _
               " AND instid = " & lIITTArtinvtc & _
               " AND (apt_esist01 - apt_prenot01 <> 0 OR apt_esist02 - apt_prenot02 <> 0 " & _
               "   OR apt_esist03 - apt_prenot03 <> 0 OR apt_esist04 - apt_prenot05 <> 0 " & _
               "   OR apt_esist05 - apt_prenot05 <> 0 OR apt_esist06 - apt_prenot06 <> 0 " & _
               "   OR apt_esist07 - apt_prenot07 <> 0 OR apt_esist08 - apt_prenot08 <> 0 " & _
               "   OR apt_esist09 - apt_prenot09 <> 0 OR apt_esist10 - apt_prenot10 <> 0 " & _
               "   OR apt_esist11 - apt_prenot11 <> 0 OR apt_esist12 - apt_prenot12 <> 0 " & _
               "   OR apt_esist13 - apt_prenot13 <> 0 OR apt_esist14 - apt_prenot14 <> 0 " & _
               "   OR apt_esist15 - apt_prenot15 <> 0 OR apt_esist16 - apt_prenot16 <> 0 " & _
               "   OR apt_esist17 - apt_prenot17 <> 0 OR apt_esist18 - apt_prenot18 <> 0 " & _
               "   OR apt_esist19 - apt_prenot19 <> 0 OR apt_esist20 - apt_prenot20 <> 0 " & _
               "   OR apt_esist21 - apt_prenot21 <> 0 OR apt_esist22 - apt_prenot22 <> 0 " & _
               "   OR apt_esist23 - apt_prenot23 <> 0 OR apt_esist24 - apt_prenot24 <> 0) "

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetTtartinvfTc(ByVal strDitta As String, ByVal lIITTArtinvtc As Integer, _
                                             ByVal strCodart As String, ByVal nFase As Integer, _
                                             ByVal nCodmaga As Integer, ByRef dttOut As DataTable) As Boolean

    Try
      GetTtartinvfTc(strDitta, lIITTArtinvtc, strCodart, nFase, nCodmaga, dttOut, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetTtartinvfTc(ByVal strDitta As String, ByVal lIITTArtinvtc As Integer, _
                                         ByVal strCodart As String, ByVal nFase As Integer, _
                                         ByVal nCodmaga As Integer, ByRef dttOut As DataTable, ByVal dtrArt As DataRow) As Boolean
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITTArtinvtc, strCodart, nFase, nCodmaga, dttOut, dtrArt})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(5), DataTable)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT (apt_esist01 - apt_prenot01) as quant01, (apt_esist02 - apt_prenot02) as quant02," & _
                "      (apt_esist03 - apt_prenot03) as quant03, (apt_esist04 - apt_prenot04) as quant04," & _
                "      (apt_esist05 - apt_prenot05) as quant05, (apt_esist06 - apt_prenot06) as quant06," & _
                "      (apt_esist07 - apt_prenot07) as quant07, (apt_esist08 - apt_prenot08) as quant08," & _
                "      (apt_esist09 - apt_prenot09) as quant09, (apt_esist10 - apt_prenot10) as quant10," & _
                "      (apt_esist11 - apt_prenot11) as quant11, (apt_esist12 - apt_prenot12) as quant12," & _
                "      (apt_esist13 - apt_prenot13) as quant13, (apt_esist14 - apt_prenot14) as quant14," & _
                "      (apt_esist15 - apt_prenot15) as quant15, (apt_esist16 - apt_prenot16) as quant16," & _
                "      (apt_esist17 - apt_prenot17) as quant17, (apt_esist18 - apt_prenot18) as quant18," & _
                "      (apt_esist19 - apt_prenot19) as quant19, (apt_esist20 - apt_prenot20) as quant20," & _
                "      (apt_esist21 - apt_prenot21) as quant21, (apt_esist22 - apt_prenot22) as quant22," & _
                "      (apt_esist23 - apt_prenot23) as quant23, (apt_esist24 - apt_prenot24) as quant24" & _
                " FROM ttartinvtc " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                "   AND instid = " & lIITTArtinvtc & _
                "   AND apt_codart = " & CStrSQL(strCodart) & _
                "   AND apt_fase = " & nFase.ToString & _
                "   AND apt_magaz = " & nCodmaga.ToString

      If dtrArt IsNot Nothing Then
        strSQl &= " AND apt_commeca = " & ntscint(dtrArt!ap_commeca)
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function DeleteArticoOk(ByVal strDitta As String, ByVal lIITTArtinvf As Integer, _
                                             ByVal lIITTArtinvtc As Integer, ByVal strCodart As String, _
                                             ByVal nFase As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM TTARTINVF WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ap_codart = " & CStrSQL(strCodart) & _
               " AND instid = " & lIITTArtinvf.ToString & _
               " AND ap_fase = " & nFase.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM ttartinvtc WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND apt_codart = " & CStrSQL(strCodart) & _
                     " AND instid = " & lIITTArtinvtc.ToString & _
                     " AND apt_fase = " & nFase.ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function EsisteDoc(ByVal strDitta As String, ByVal strTipork As String, _
                                        ByVal nAnno As Integer, ByVal strSerie As String, _
                                        ByVal lNumdoc As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT TOP 1 tm_conto FROM testmag " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(strTipork) & _
              " AND tm_anno = " & nAnno.ToString & _
              " AND tm_serie = " & CStrSQL(strSerie) & _
              " AND tm_numdoc = " & lNumdoc.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      dttTmp.Clear()
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CheckCodiciIvaPerElaborazione(ByVal strDitta As String, ByVal lIITTArtinvf As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT ar_codiva" & _
        " FROM TTARTINVF INNER JOIN artico ON TTARTINVF.codditt = artico.codditt AND TTARTINVF.ap_codart = artico.ar_codart" & _
        " WHERE TTARTINVF.codditt = " & CStrSQL(strDitta) & _
        " GROUP BY ar_codiva"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 8 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function GetTTartinvfVdaordiAZero(ByVal strDitta As String, ByVal lIITTArtinvf As Integer, _
      ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT ap_codart AS am_codart, ap_fase AS am_fase, ap_commeca AS am_commeca," & _
        " ap_lotto AS am_lotto, ap_ubicaz AS am_ubicaz, ap_matric AS am_matric, ap_esist AS am_quant" & _
        " FROM TTARTINVF" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTArtinvf.ToString & _
        " AND ap_vdaordi = 0"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
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

  Public Overridable Function ControllaValiditaLISTSAR(ByVal strDitta As String, _
      ByVal nCodlsarIni As Integer, ByVal nCodlsarFin As Integer, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 lsa_codlsar, lsa_codart, ar_descr, ar_gesfasi, ar_gescomm, ar_geslotti," & _
        " ar_gesubic, ar_gestmatr" & _
        " FROM listsar INNER JOIN artico ON listsar.codditt = artico.codditt AND listsar.lsa_codart = artico.ar_codart" & _
        " WHERE listsar.codditt = " & CStrSQL(strDitta) & _
        " AND lsa_codlsar BETWEEN " & nCodlsarIni & " AND " & nCodlsarFin & _
        " AND ((ar_gesfasi = 'S' AND lsa_fase = 0)" & _
        " OR (ar_gescomm = 'S' AND lsa_commeca = 0)" & _
        " OR (ar_geslotti = 'S' AND lsa_lotto = 0)" & _
        " OR (ar_gesubic = 'S' AND (lsa_ubicaz IS NULL OR LTRIM(RTRIM(lsa_ubicaz)) = ''))" & _
        " OR (ar_gestmatr = 'S' AND (lsa_matric IS NULL OR LTRIM(RTRIM(lsa_matric)) = '')))"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        dttTmp.Clear()
        dttTmp.Dispose()
        Return True
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function



  Public Overridable Function SelezioneArticoliTTARTINVF(ByVal strDitta As String, ByVal lIITTArtinvf As Integer) As DataTable
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DISTINCT ap_codart, ap_fase " & _
               " FROM TTARTINVF " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND instid = " & lIITTArtinvf

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return Nothing
  End Function

  Public Overridable Function AggPrezziSuArticoliInTTArtinvf(ByVal strDitta As String, ByVal lIITTArtinvf As Integer, ByVal dtrArtico As DataRow, _
                                                             ByVal dPrezzo As Decimal) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE TTARTINVF SET ap_sommat = " & CDblSQL(dPrezzo) & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND instid = " & lIITTArtinvf & _
               "   AND ap_codart = " & CStrSQL(dtrArtico!ap_codart) & _
               "   AND ap_fase = " & NTSCInt(dtrArtico!ap_fase)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckArticoliTCO(ByVal strDitta As String, ByVal nMagaz As Integer) As Boolean
    Dim i As Integer = 0
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 ar_codart" & _
        " FROM artico INNER JOIN artprotc ON artico.codditt = artprotc.codditt" & _
                           " AND artico.ar_codart = artprotc.apt_codart" & _
        " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                          " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
        " WHERE ar_codtagl <> 0" & _
        " AND apt_magaz = " & nMagaz & _
        " AND	("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_esist" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      strSQL += " OR ("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_prenot" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      strSQL += " OR ("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_ordin" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      strSQL += " OR ("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_impeg" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 ar_codart" & _
        " FROM artico INNER JOIN lotcprotc ON artico.codditt = lotcprotc.codditt" & _
                                        " AND artico.ar_codart = lotcprotc.apt_codart" & _
        " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                          " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
        " WHERE ar_codtagl <> 0" & _
        " AND apt_magaz = " & nMagaz & _
        " AND	("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_esist" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      strSQL += " OR ("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_prenot" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      strSQL += " OR ("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_ordin" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      strSQL += " OR ("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND apt_impeg" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 ar_codart" & _
        " FROM artico INNER JOIN artdeftc ON artico.codditt = artdeftc.codditt" & _
                                       " AND artico.ar_codart = artdeftc.adt_codart" & _
        " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                          " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
        " WHERE ar_codtagl <> 0" & _
        " AND adt_magaz = " & nMagaz & _
        " AND	("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND adt_esist" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT  TOP 1 ar_codart" & _
        " FROM artico INNER JOIN lotcdeftc ON artico.codditt = lotcdeftc.codditt" & _
                                        " AND artico.ar_codart = lotcdeftc.adt_codart" & _
        " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                          " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
        " WHERE ar_codtagl <> 0" & _
        " AND adt_magaz = " & nMagaz & _
        " AND	("
      For i = 1 To 24
        strSQL += IIf(i > 1, " OR ", "").ToString & _
          "(tb_dest" & Right("0" & i, 2) & " = ' ' AND adt_esist" & Right("0" & i, 2) & " <> 0)" & _
          IIf(i = 24, ")", "").ToString
      Next
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

End Class
