Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLD__CIVA
  Inherits CLD__BASE

  Public Overridable Function LeggiTabCiva(ByVal strDitta As String, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try

      strSQL = "SELECT tabciva.*, tb_dessmel as xx_codtpel, tb_destacq as xx_tipacq, tabciva_1.tb_desciva As xx_mvcodiva, " & _
               "       tabciva_2.tb_desciva As xx_codindetr, tabciva_3.tb_desciva As xx_coddetr, tabciva_4.tb_desciva As xx_codagr, " & _
               "       tabciva_5.tb_desciva As xx_codteor, tb_desrear AS xx_reparto " & _
               " FROM tabciva " & _
               "  LEFT JOIN tabsmel ON " & CStrSQL(strDitta) & " = tabsmel.codditt AND tabciva.tb_tipel = tabsmel.tb_codsmel AND tabciva.tb_codtpel = tabsmel.tb_numsmel " & _
               "  LEFT JOIN tabrear ON " & CStrSQL(strDitta) & " = tabrear.codditt AND tb_reparto = tb_codrear " & _
               "  LEFT JOIN tabtacq ON tabciva.tb_tipacq = tabtacq.tb_codtacq " & _
               "  LEFT JOIN tabciva As tabciva_1 ON tabciva.tb_mvcodiva = tabciva_1.tb_codciva" & _
               "  LEFT JOIN tabciva As tabciva_2 ON tabciva.tb_codindetr = tabciva_2.tb_codciva" & _
               "  LEFT JOIN tabciva As tabciva_3 ON tabciva.tb_coddetr = tabciva_3.tb_codciva" & _
               "  LEFT JOIN tabciva As tabciva_4 ON tabciva.tb_codagr = tabciva_4.tb_codciva" & _
               "  LEFT JOIN tabciva As tabciva_5 ON tabciva.tb_codteor = tabciva_5.tb_codciva" & _
               " ORDER BY tabciva.tb_codciva"
      'Chiedo i dati al database
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABCIVA")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'Non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancella(ByVal strDitta As String, ByVal nCodciva As Integer, _
    ByRef strMsg As String) As Boolean
    Dim bDBMultiDitta As Boolean = False
    Dim i As Integer = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      bDBMultiDitta = False
      strSQL = "SELECT codditt FROM tabanaz"
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 1 Then bDBMultiDitta = True
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, ai_aanno FROM anaziva" & _
        " WHERE ai_codivapr = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, ai_aanno"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsg = oApp.Tr(Me, 130930960065252616, " . 'Anagrafica Ditta' (tabella: ANAZIVA)" & vbCrLf & _
          " --> Dati Iva" & vbCrLf & _
          "     utilizzato per: 'Codice Iva Prioritario'" & vbCrLf & _
          "     per l'anno: |" & NTSCStr(dttTmp.Rows(0)!ai_aanno) & "|" & _
          IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, ae_codartf, ae_forn, ae_codmarc FROM artest" & _
        " WHERE ae_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, ae_codartf, ae_forn, ae_codmarc"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsg = oApp.Tr(Me, 130930967181487611, " . 'Catalogo Fornitori' (tabella: ARTEST)" & vbCrLf & _
          "     Codice Articolo Fornitore: |" & NTSCStr(dttTmp.Rows(0)!ae_codartf) & "|" & vbCrLf & _
          "     Fornitore: |" & NTSCStr(dttTmp.Rows(0)!ae_forn) & "|" & vbCrLf & _
          "     Marca: |" & NTSCStr(dttTmp.Rows(0)!ae_codmarc) & "|" & _
          IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, ar_codart FROM artico" & _
        " WHERE ar_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, ar_codart"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsg = oApp.Tr(Me, 130930970136867718, " . 'Anagrafica Articoli' (tabella: ARTICO)" & vbCrLf & _
          "     Codice Articolo: |" & NTSCStr(dttTmp.Rows(0)!ar_codart) & "|" & _
          IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, mc_numcom, mc_riga FROM movcom" & _
        " WHERE mc_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, mc_numcom, mc_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsg = oApp.Tr(Me, 130930977063124952, " . 'Gestione Punti Vendita (righe)' (tabella: MOCVOM)" & vbCrLf & _
          "     Comanda N°: |" & NTSCStr(dttTmp.Rows(0)!mc_numcom) & "|" & vbCrLf & _
          "     Riga: |" & NTSCStr(dttTmp.Rows(0)!mc_riga) & "|" & _
          IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, pn_datreg, pn_numreg, pn_riga FROM prinot" & _
        " WHERE pn_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, pn_datreg, pn_numreg, pn_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsg = oApp.Tr(Me, 130931051367097066, " . 'Gestione Prima Nota' (tabella: PRINOT)" & vbCrLf & _
          "     Registrazione N°: |" & NTSCStr(dttTmp.Rows(0)!pn_numreg) & "|" & vbCrLf & _
          "     del: |" & NTSCDate(dttTmp.Rows(0)!pn_datreg).ToShortDateString & "|" & vbCrLf & _
          "     Riga: |" & NTSCStr(dttTmp.Rows(0)!pn_riga) & "|" & _
          IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, mi_datreg, mi_numreg, mi_riga FROM moviva" & _
        " WHERE mi_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, mi_datreg, mi_numreg, mi_riga, mi_rigareg"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strMsg = oApp.Tr(Me, 130930980716886895, " . 'Gestione Prima Nota' (tabella: MOVIVA)" & vbCrLf & _
          " --> Movimenti Iva" & vbCrLf & _
          "     Registrazione N°: |" & NTSCStr(dttTmp.Rows(0)!mi_numreg) & "|" & vbCrLf & _
          "     del: |" & NTSCDate(dttTmp.Rows(0)!mi_datreg).ToShortDateString & "|" & vbCrLf & _
          "     Riga: |" & NTSCStr(dttTmp.Rows(0)!mi_riga) & "|" & _
          IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga FROM movmag" & _
        " WHERE mm_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130930982117579682, " . 'Gestione Documenti di Magazzino' (tabella: MOVMAG)" & vbCrLf & _
            "     |" & DecodificaDocumento(NTSCStr(!mm_tipork)) & "|" & _
            " N°|" & NTSCStr(!mm_numdoc) & IIf(NTSCStr(!mm_serie).Trim <> "", "/", "").ToString & NTSCStr(!mm_serie).Trim & "|" & _
            " del |" & NTSCStr(!mm_anno) & "|, riga |" & NTSCStr(!mm_riga) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, mo_anno, mo_serie, mo_numord, mo_vers, mo_riga FROM movoff" & _
        " WHERE mo_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, mo_tipork, mo_anno, mo_serie, mo_numord, mo_vers, mo_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130930990033829328, " . 'Gestione Offerte (righe)' (tabella: MOVOFF)" & vbCrLf & _
            "     N°|" & NTSCStr(!mo_numord) & IIf(NTSCStr(!mo_serie).Trim <> "", "/", "").ToString & NTSCStr(!mo_serie).Trim & "|" & _
            " del |" & NTSCStr(!mo_anno) & "|, versione |" & NTSCStr(!mo_anno) & "|, riga |" & NTSCStr(!mm_riga) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga FROM movord" & _
        " WHERE mo_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130930982117579682, " . 'Gestione Ordini/Impegni (righe)' (tabella: MOVORD)" & vbCrLf & _
            "     |" & DecodificaOrdine(NTSCStr(!mo_tipork)) & "|" & _
            " N°|" & NTSCStr(!mo_numord) & IIf(NTSCStr(!mo_serie).Trim <> "", "/", "").ToString & NTSCStr(!mo_serie).Trim & "|" & _
            " del |" & NTSCStr(!mo_anno) & "|, riga |" & NTSCStr(!mo_riga) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga FROM movpar" & _
        " WHERE mm_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931033371398102, " . 'Gestione Parcelle e Notule (righe)' (tabella: MOVPAR)" & vbCrLf & _
            "     |" & DecodificaParcella(NTSCStr(!mm_tipork)) & "|" & _
            " N°|" & NTSCStr(!mm_numdoc) & IIf(NTSCStr(!mm_serie).Trim <> "", "/", "").ToString & NTSCStr(!mm_serie).Trim & "|" & _
            " del |" & NTSCStr(!mm_anno) & "|, riga |" & NTSCStr(!mm_riga) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga FROM movprb" & _
        " WHERE mm_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931034313693956, " . 'Gestione Documenti di Magazzino (righe)' (tabella: MOVPRB)" & vbCrLf & _
            "     Nota di Prelievo N°|" & NTSCStr(!mm_numdoc) & IIf(NTSCStr(!mm_serie).Trim <> "", "/", "").ToString & NTSCStr(!mm_serie).Trim & "|" & _
            " del |" & NTSCStr(!mm_anno) & "|, riga |" & NTSCStr(!mm_riga) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, mu_codusat, mu_progr FROM movusat" & _
        " WHERE mu_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, mu_progr"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931037118246707, " . 'Anagrafica Beni Usati' (tabella: MOVUSAT)" & vbCrLf & _
            " --> Movimenti Bene Usato |" & NTSCStr(!mu_progr) & "|" & vbCrLf & _
            "     per il codice Bene Usato: |" & NTSCStr(!mu_codusat) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, nnc_numcontr FROM nncontras" & _
        " WHERE nnc_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, nnc_numcontr"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931041110866513, " . 'Gestione Contratti di Assistenza' (tabella: NNCONTRAS)" & vbCrLf & _
            "     Contratto N°|" & NTSCStr(!nnc_numcontr) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, nnr_numcontr, nnr_rinnid, nnr_riga FROM nnmovctra" & _
        " WHERE nnr_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, nnr_numcontr, nnr_rinnid, nnr_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931042670552338, " . 'Gestione Contratti di Assistenza' (tabella: NNMOVCTRA)" & vbCrLf & _
            "     Contratto N°|" & NTSCStr(!nnr_numcontr) & "|, Rinnovo N°|" & NTSCStr(!nnr_rinnid) & "|, riga |" & NTSCStr(!nnr_riga) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, nnp_numprep FROM nnprepag" & _
        " WHERE nnp_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, nnp_numprep"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931044445190846, " . 'Gestione Prepagati Monte-Ore' (tabella: NNPREPAG)" & vbCrLf & _
            "     N°|" & NTSCStr(!nnp_numprep) & "|" & _
          IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, ri_codrica, ri_codchia FROM nnricambi" & _
        " WHERE ri_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, ri_codrica"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931046266458208, " . 'Gestione Chiamate' (tabella: NNRICAMBI)" & vbCrLf & _
            "     Chiamata N°|" & NTSCStr(!ri_codchia) & "|, Id Ricambio |" & NTSCStr(!ri_codrica) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, ol_progr, ol_tipork FROM ordlist" & _
        " WHERE ol_codiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, ol_progr"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931049028395202, " . 'Gestione Proposte d'Ordine' (tabella: ORDLIST)" & vbCrLf & _
            "     |" & DecodificaProposta(NTSCStr(!ol_tipork)) & "| N°|" & NTSCStr(!ol_progr) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tb_codciva FROM tabciva" & _
        " WHERE tb_mvcodiva = " & nCodciva & _
        " ORDER BY tb_codciva"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931053473150946, " . 'Codici Iva' (tabella: TABCIVA)" & vbCrLf & _
            "     Codice Iva per salto aliquota" & vbCrLf & _
            "     utilizzato per il Codice Iva |" & NTSCStr(!tb_codciva) & "|")
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tb_codpece FROM tabpece" & _
        " WHERE tb_codivafc = " & nCodciva & _
        " ORDER BY tb_codpece"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931057343988948, " . 'Personalizzazione Cespiti' (tabella: TABPECE)" & vbCrLf & _
            "     Codice Iva 'Fuori campo Iva'")
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tb_codpecg FROM tabpecg" & _
        " WHERE tb_codivafc = " & nCodciva & _
        " ORDER BY tb_codpecg"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931058972466264, " . 'Personalizzazione Contabilità Generale' (tabella: TABPECG)" & vbCrLf & _
            "     Codice Iva 'Fuori campo'")
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, tb_tiporeg, tb_numereg, tb_anno, tb_pcodiva FROM tabpriv" & _
        " WHERE tb_pcodiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, tb_tiporeg, tb_numereg, tb_anno, tb_pcodiva"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931063046558868, " . 'Progressivi Registri Iva' (tabella: TABPRIV)" & vbCrLf & _
            "     |" & DecodificaTipoRegistroIva(NTSCStr(!tb_tiporeg)) & "| N°|" & NTSCStr(!tb_numereg) & _
            "| del |" & NTSCStr(!tb_anno) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, tb_tipork, tb_utipreg, tb_unumreg, tb_annoes FROM tabriel" & _
        " WHERE tb_pcodiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, tb_tipork, tb_utipreg, tb_unumreg, tb_annoes, tb_pcodiva"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931074009901378, " . 'Dati per Rielaborazione Bollati' (tabella: TABRIEL)" & vbCrLf & _
            "     |" & DecodificaBollati(NTSCStr(!tb_tipork)) & vbCrLf & _
            "     Tipo Registro Iva |" & DecodificaTipreg(NTSCStr(!tb_utipreg)) & "| N° |" & NTSCStr(!tb_unumreg) & "|" & vbCrLf & _
            "     Anno Iva |" & NTSCStr(!tb_annoes) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tb_codtpco FROM tabtpco" & _
        " WHERE (tb_codivac = " & nCodciva & " OR tb_codivaai = " & nCodciva & ")" & _
        " ORDER BY tb_codtpco"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931080996991659, " . 'Contabilizzazione Ritenute' (tabella: TABPCO)" & vbCrLf & _
            "     Codice Iva Compenso e/o Codice Iva Altri Importi")
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, tb_codvpar FROM tabvpar" & _
        " WHERE tb_pcodiva = " & nCodciva & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, tb_codvpar"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg = oApp.Tr(Me, 130931082532623299, " . 'Voci Parcellazione' (tabella: TABVPAR)" & vbCrLf & _
            "     Codice Voce Parcellazione|" & NTSCStr(!tb_codvpar) & "|" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, tc_numcom, tc_codiva_1, tc_codiva_2, tc_codiva_3, tc_codiva_4," & _
        " tc_codiva_5, tc_codiva_6, tc_codiva_7, tc_codiva_8 FROM testcom" & _
        " WHERE (tc_codiva_1 = " & nCodciva & _
          " OR tc_codiva_2 = " & nCodciva & _
          " OR tc_codiva_3 = " & nCodciva & _
          " OR tc_codiva_4 = " & nCodciva & _
          " OR tc_codiva_5 = " & nCodciva & _
          " OR tc_codiva_6 = " & nCodciva & _
          " OR tc_codiva_7 = " & nCodciva & _
          " OR tc_codiva_8 = " & nCodciva & ")" & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, tc_numcom"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          i = 1
          If NTSCInt(!tc_codiva_1) = nCodciva Then
            i = 1
          ElseIf NTSCInt(!tc_codiva_2) = nCodciva Then
            i = 2
          ElseIf NTSCInt(!tc_codiva_3) = nCodciva Then
            i = 3
          ElseIf NTSCInt(!tc_codiva_4) = nCodciva Then
            i = 4
          ElseIf NTSCInt(!tc_codiva_5) = nCodciva Then
            i = 5
          ElseIf NTSCInt(!tc_codiva_6) = nCodciva Then
            i = 6
          ElseIf NTSCInt(!tc_codiva_7) = nCodciva Then
            i = 7
          ElseIf NTSCInt(!tc_codiva_8) = nCodciva Then
            i = 8
          End If
          strMsg = oApp.Tr(Me, 130931087136523006, " . 'Gestione Punti Vendita' (tabella: TESTCOM)" & vbCrLf & _
            "     |" & DecodificaDocumento(NTSCStr(!tc_tipork)) & "|" & vbCrLf & _
            "     Comanda N° |" & NTSCStr(!tc_numcom) & "|" & vbCrLf & _
            "     (Castelletto Iva posizione |" & i.ToString & "|)" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_codiva_1, tm_codiva_2, tm_codiva_3," & _
        " tm_codiva_4, tm_codiva_5, tm_codiva_6, tm_codiva_7, tm_codiva_8 FROM testmag" & _
        " WHERE (tm_codiva_1 = " & nCodciva & _
          " OR tm_codiva_2 = " & nCodciva & _
          " OR tm_codiva_3 = " & nCodciva & _
          " OR tm_codiva_4 = " & nCodciva & _
          " OR tm_codiva_5 = " & nCodciva & _
          " OR tm_codiva_6 = " & nCodciva & _
          " OR tm_codiva_7 = " & nCodciva & _
          " OR tm_codiva_8 = " & nCodciva & ")" & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, tm_tipork, tm_anno, tm_serie, tm_numdoc"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          i = 1
          If NTSCInt(!tm_codiva_1) = nCodciva Then
            i = 1
          ElseIf NTSCInt(!tm_codiva_2) = nCodciva Then
            i = 2
          ElseIf NTSCInt(!tm_codiva_3) = nCodciva Then
            i = 3
          ElseIf NTSCInt(!tm_codiva_4) = nCodciva Then
            i = 4
          ElseIf NTSCInt(!tm_codiva_5) = nCodciva Then
            i = 5
          ElseIf NTSCInt(!tm_codiva_6) = nCodciva Then
            i = 6
          ElseIf NTSCInt(!tm_codiva_7) = nCodciva Then
            i = 7
          ElseIf NTSCInt(!tm_codiva_8) = nCodciva Then
            i = 8
          End If
          strMsg = oApp.Tr(Me, 130931094330618884, " . 'Gestione Documenti di Magazzino' (tabella: TESTMAG)" & vbCrLf & _
            "     |" & DecodificaDocumento(NTSCStr(!tm_tipork)) & "|" & vbCrLf & _
            " N°|" & NTSCStr(!tm_numdoc) & IIf(NTSCStr(!tm_serie).Trim <> "", "/", "").ToString & NTSCStr(!tm_serie).Trim & "|" & _
            " del |" & NTSCStr(!tm_anno) & "|" & vbCrLf & _
            "     (Castelletto Iva posizione |" & i.ToString & "|)" & _
            IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 codditt, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_codiva_1, tm_codiva_2, tm_codiva_3," & _
        " tm_codiva_4, tm_codiva_5, tm_codiva_6, tm_codiva_7, tm_codiva_8 FROM testpar" & _
        " WHERE (tm_codiva_1 = " & nCodciva & _
          " OR tm_codiva_2 = " & nCodciva & _
          " OR tm_codiva_3 = " & nCodciva & _
          " OR tm_codiva_4 = " & nCodciva & _
          " OR tm_codiva_5 = " & nCodciva & _
          " OR tm_codiva_6 = " & nCodciva & _
          " OR tm_codiva_7 = " & nCodciva & _
          " OR tm_codiva_8 = " & nCodciva & ")" & _
        IIf(bDBMultiDitta = False, " AND codditt = " & CStrSQL(strDitta), "").ToString & _
        " ORDER BY codditt, tm_tipork, tm_anno, tm_serie, tm_numdoc"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          i = 1
          If NTSCInt(!tm_codiva_1) = nCodciva Then
            i = 1
          ElseIf NTSCInt(!tm_codiva_2) = nCodciva Then
            i = 2
          ElseIf NTSCInt(!tm_codiva_3) = nCodciva Then
            i = 3
          ElseIf NTSCInt(!tm_codiva_4) = nCodciva Then
            i = 4
          ElseIf NTSCInt(!tm_codiva_5) = nCodciva Then
            i = 5
          ElseIf NTSCInt(!tm_codiva_6) = nCodciva Then
            i = 6
          ElseIf NTSCInt(!tm_codiva_7) = nCodciva Then
            i = 7
          ElseIf NTSCInt(!tm_codiva_8) = nCodciva Then
            i = 8
          End If
          strMsg = oApp.Tr(Me, 130931096785309490, " . 'Gestione Parcelle e Notule' (tabella: TESTPAR)" & vbCrLf & _
            "     |" & DecodificaParcella(NTSCStr(!tm_tipork)) & "|" & vbCrLf & _
            " N°|" & NTSCStr(!tm_numdoc) & IIf(NTSCStr(!tm_serie).Trim <> "", "/", "").ToString & NTSCStr(!tm_serie).Trim & "|" & _
            " del |" & NTSCStr(!tm_anno) & "|" & vbCrLf & _
            "     (Castelletto Iva posizione |" & i.ToString & "|)" & _
          IIf(bDBMultiDitta = True, vbCrLf & "     sulla la Ditta: |" & NTSCStr(dttTmp.Rows(0)!codditt) & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function DecodificaDocumento(ByVal strTipork As String) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipork
        Case "A" : Return "Fattura Immediata Emessa"
        Case "B" : Return "D.D.T. Emesso"
        Case "C" : Return "Corrispettivo Emesso"
        Case "E" : Return "Nota di Addebito Emessa"
        Case "F" : Return "Ricevuta Fiscale Emessa"
        Case "I" : Return "Riemissione Ricevuta Fiscale"
        Case "J" : Return "Nota di Accredito Ricevuta"
        Case "L" : Return "Fattura Immediata Ricevuta"
        Case "M" : Return "D.D.T. Ricevuto"
        Case "N" : Return "Nota di Accredito Emessa"
        Case "S" : Return "Fattura/Ricevuta Fiscale Emessa"
        Case "T" : Return "Carico da Produzione"
        Case "U" : Return "Scarico a P"
        Case "Z" : Return "Bolla di Movimentazione Interna"
        Case "D" : Return "Fattura Differita Emessa"
        Case "K" : Return "Fattura Differita Ricevuta"
        Case "P" : Return "Fattura/Ricevuta Fiscale Differita"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return ""
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function
  Public Overridable Function DecodificaOrdine(ByVal strTipork As String) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipork
        Case "R" : Return "Impegno Cliente"
        Case "O" : Return "Ordine Fornitore"
        Case "H" : Return "Ordine di Produzione"
        Case "X" : Return "Impegno Trasferimento"
        Case "Q" : Return "Preventivo"
        Case "#" : Return "Impegno di Commessa"
        Case "V" : Return "Impegno Cliente Aperto"
        Case "$" : Return "Ordine Fornitore Aperto"
        Case "Y" : Return "Impegno di Produzione"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return ""
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function
  Public Overridable Function DecodificaParcella(ByVal strTipork As String) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipork
        Case "1" : Return "Fattura"
        Case "2" : Return "Nota di Accredito/Storno Fattura"
        Case "3" : Return "Avviso di Parcella/Notula"
        Case "4" : Return "Nota di Accredito"
        Case "5" : Return "Corrispettivo"
        Case "6" : Return "Fattura di Acconto"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return ""
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function
  Public Overridable Function DecodificaProposta(ByVal strTipork As String) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipork
        Case "O" : Return "Ordine a Fornitore"
        Case "H" : Return "Ordine di Produzione"
        Case "X" : Return "Impegno di Trasferimento"
        Case "Y" : Return "Impegno di Produzione"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return ""
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function
  Public Overridable Function DecodificaTipoRegistroIva(ByVal strTipork As String) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipork
        Case " " : Return "(no iva)"
        Case "T" : Return "Acquisti in Sospensione"
        Case "A" : Return "Acquisti"
        Case "C" : Return "Corrispettivi"
        Case "S" : Return "Vendite in Sospensione"
        Case "V" : Return "Vendite"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return ""
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function
  Public Overridable Function DecodificaBollati(ByVal strTipork As String) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipork
        Case "G" : Return "Libro Giornale"
        Case "D" : Return "Dati Ultimi Registri Iva"
        Case "P" : Return "Progressivi Registrazioni Iva"
        Case "M" : Return "Progressivi Definitivi Magazzino"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return ""
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function
  Public Overridable Function DecodificaTipreg(ByVal strTipork As String) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipork
        Case "A" : Return "Acquisti"
        Case "V" : Return "Vendite"
        Case "C" : Return "Corrispettivi"
        Case "S" : Return "Acquisti in Sospensione"
        Case "T" : Return "Vendite in Sospensione"
        Case "X" : Return "Tutti"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return ""
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function

End Class
