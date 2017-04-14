Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDORGNNP
  Inherits CLD__BASE

  Public Overridable Function GetData(ByVal strDitta As String, ByVal lIITTGeGnnp As Integer, _
                                      ByRef ds As DataSet) As Boolean
    '-------------------
    'ottengo l'elenco dei documenti da generare
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TTGEGNNP.*, '' as xx_seleziona, " & _
        " '' as xx_commess, '' as xx_descommess, " & _
        " '' as xx_righe, '' as xx_righemanc, " & _
        " '' as xx_dest, '' as xx_desdest " & _
        " FROM TTGEGNNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTGeGnnp & _
        " ORDER BY fd_descr, fd_conto, fd_datdoc"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTGEGNNP")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ComponiQuerySeic(ByVal strTipo As String, ByVal stredADataCons As String, _
                                             ByVal stredCodmaga As String, ByVal stredSerie As String, _
                                             ByVal bckTutto As Boolean, ByVal bopTuttiClienti As Boolean, _
                                             ByVal stredDalcliente As String, _
                                             ByVal stredAlcliente As String, ByVal bopTutteDate As Boolean, _
                                             ByVal stredDalladata As String, ByVal stredAlladata As String, _
                                             ByVal bopTutteBolle As Boolean, ByVal stredTipobf As String, _
                                             ByVal bopTuttiAgenti As Boolean, ByVal stredCodcage As String, _
                                             ByVal bopTutteZone As Boolean, ByVal stredCodzona As String, _
                                             ByVal bckConf As Boolean, ByVal strTipoConferma As String, _
                                             ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                             ByRef nSenuAnno As Integer, ByRef strSenuSerie As String, _
                                             ByVal strDitta As String, ByVal lIITTOltomo As Integer, _
                                             ByRef bDaSelezioneArticoli As Boolean, ByVal strWhereFiar As String, _
                                             ByRef strSeicQuery As String) As Boolean
    Try
      Return ComponiQuerySeic(strTipo, stredADataCons, stredCodmaga, stredSerie, bckTutto, bopTuttiClienti, _
                              stredDalcliente, stredAlcliente, bopTutteDate, stredDalladata, stredAlladata, _
                              bopTutteBolle, stredTipobf, bopTuttiAgenti, stredCodcage, bopTutteZone, stredCodzona, _
                              bckConf, strTipoConferma, stredCommecaini, stredCommecafin, nSenuAnno, strSenuSerie, _
                              strDitta, lIITTOltomo, bDaSelezioneArticoli, strWhereFiar, strSeicQuery, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ComponiQuerySeic(ByVal strTipo As String, ByVal stredADataCons As String, _
                                               ByVal stredCodmaga As String, ByVal stredSerie As String, _
                                               ByVal bckTutto As Boolean, ByVal bopTuttiClienti As Boolean, _
                                               ByVal stredDalcliente As String, _
                                               ByVal stredAlcliente As String, ByVal bopTutteDate As Boolean, _
                                               ByVal stredDalladata As String, ByVal stredAlladata As String, _
                                               ByVal bopTutteBolle As Boolean, ByVal stredTipobf As String, _
                                               ByVal bopTuttiAgenti As Boolean, ByVal stredCodcage As String, _
                                               ByVal bopTutteZone As Boolean, ByVal stredCodzona As String, _
                                               ByVal bckConf As Boolean, ByVal strTipoConferma As String, _
                                               ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                               ByRef nSenuAnno As Integer, ByRef strSenuSerie As String, _
                                               ByVal strDitta As String, ByVal lIITTOltomo As Integer, _
                                               ByRef bDaSelezioneArticoli As Boolean, ByVal strWhereFiar As String, _
                                               ByRef strSeicQuery As String, ByVal bEscudiRetail As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strQuery As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTipo, stredADataCons, stredCodmaga, stredSerie, bckTutto, _
                                             bopTuttiClienti, stredDalcliente, stredAlcliente, bopTutteDate, _
                                             stredDalladata, stredAlladata, bopTutteBolle, stredTipobf, _
                                             bopTuttiAgenti, stredCodcage, bopTutteZone, stredCodzona, _
                                             bckConf, strTipoConferma, stredCommecaini, stredCommecafin, _
                                             nSenuAnno, strSenuSerie, strDitta, lIITTOltomo, bDaSelezioneArticoli, _
                                             strWhereFiar, strSeicQuery, bEscudiRetail})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        nSenuAnno = NTSCInt(oIn(21))
        strSenuSerie = NTSCStr(oIn(22))
        bDaSelezioneArticoli = CBool(oIn(25))
        strSeicQuery = NTSCStr(oIn(27))
        Return CBool(oOut)
      End If
      '----------------

      strSeicQuery = "td_tipork = '" & strTipo & "' AND movord.mo_datcons <= " & CDataSQL(stredADataCons) & " and movord.mo_magaz = " & stredCodmaga
      If bEscudiRetail Then strSeicQuery += " AND td_retail <> 'S'" 'se ic fatto da retail con modulo promozioni abilitato, posso evaderli solo da programma di retail
      If stredSerie <> "*" Then
        strSeicQuery += " AND td_serie = '" & stredSerie & "'"
      End If
      If bckTutto = False Then
        'DAL CLIENTE AL CLIENTE
        If bopTuttiClienti = False Then
          strSeicQuery += " AND (td_conto BETWEEN " & stredDalcliente & " AND " & stredAlcliente & ")"
        End If
        'Data bolla
        If bopTutteDate = False Then
          strSeicQuery += " AND (td_datord BETWEEN " & CDataSQL(stredDalladata) & " AND " & CDataSQL(stredAlladata) & ")"
        End If
        'tipobf
        If bopTutteBolle = False Then
          strSeicQuery += " AND (td_tipobf = " & stredTipobf & ")"
        End If
        'agente
        If bopTuttiAgenti = False Then
          strSeicQuery += " AND (td_codagen = " & stredCodcage & ")"
        End If
        'zona
        If bopTutteZone = False Then
          strSeicQuery += " AND (an_zona = " & stredCodzona & ")"
        End If
        'Confermato
        If bckConf = True Then
          If strTipoConferma = "T" Then
            strSeicQuery += " AND (td_confermato = 'S')"
          Else
            strSeicQuery += " AND (movord.mo_confermato = 'S')"
          End If
        End If
        'Commessa
        If (NTSCInt(stredCommecaini) <> 0) Or (NTSCInt(stredCommecafin) <> 999999999) Then
          strSeicQuery += " AND (movord.mo_commeca BETWEEN " & NTSCInt(stredCommecaini) & " AND " & NTSCInt(stredCommecafin) & ")"
        End If
      End If

      '------------------------------------------------------------------------------------
      '--- Se è stata chiamata la selezione sul Numero Ordine aggiunge alla query
      '------------------------------------------------------------------------------------
      If (nSenuAnno > 0) And (strSenuSerie <> "") Then
        strSeicQuery += " AND movord.mo_anno = " & nSenuAnno
        strSQL = "SELECT * FROM TTOLTOMO " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIITTOltomo '& _
        '" ORDER BY instid, tt_moserie, tt_monumrod"

        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTOLTOMO")

        For i = 0 To dsTmp.Tables("TTOLTOMO").Rows.Count - 1
          strQuery = strQuery & " ( movord.mo_serie = " & CStrSQL(dsTmp.Tables("TTOLTOMO").Rows(i)!tt_moserie) & " AND " & _
                   "movord.mo_numord = " & NTSCStr(dsTmp.Tables("TTOLTOMO").Rows(i)!tt_monumord) & " ) OR "
        Next

        strQuery = Left(strQuery, (Len(strQuery) - 4))
        If Trim(strQuery) <> "" Then
          strSeicQuery += " AND (" & strQuery & ") "
        End If
        'Azzera i campi di riferimento x le prossime volte.
        nSenuAnno = 0
        strSenuSerie = " "
      End If
      '----------------------------------------------------------------------------------------
      '--- Se è stata chiamata la selezione per articolo
      '----------------------------------------------------------------------------------------
      If bDaSelezioneArticoli = True Then
        TraduciWhere(strWhereFiar, strSeicQuery)
        bDaSelezioneArticoli = False
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTestateTemp(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                              ByVal bDtacUnaNotaPerImpegno As Boolean, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DISTINCT mn_conto, mn_codagen, mn_codagen2, mn_tipobf, mn_codpaga," & _
        " mn_codese, mn_tmscont1, mn_tmscont2, mn_scopag, mn_valuta, mn_scorpo, mn_datapag," & _
        " mn_flspinc, mn_flbolli, mn_coddest, mn_coddest2, MIN(mn_banc1) As Banc1," & _
        " MIN(mn_banc2) As banc2, MIN(mn_abi) As Abi, MIN(mn_cab) As Cab, mn_magaz2," & _
        " MIN(td_listino) As Listino, MIN(td_acuradi) As Acuradi," & _
        " MIN(td_vettor) As Vettor, MIN(td_aspetto) As Aspetto," & _
        " MIN(td_caustra) As Caustra, MIN(td_porto) As Porto, MIN(td_codcena) As Codcena," & _
        " MIN(td_codaspe) As Codaspe, MIN(td_commeca) As Commeca," & _
        " MIN(td_subcommeca) As Subcommeca, MIN(td_vettor2) As Vettor2," & _
        " MIN(mn_cambio) As Cambio, MIN(td_codbanc) As Codbanc, MIN(td_annotco) As Annotco," & _
        " MIN(td_codstag) As Codstag, MIN(td_codcfam) As Codcfam, MIN(td_contfatt) As Contfatt, " & _
        " MIN(td_coddivi) as Coddivi, MIN(td_codcli) as Codcli "
      '  If bDtacUnaNotaPerImpegno = True Then
      strSQL = strSQL & ", mn_tipork, mn_anno, mn_serie, mn_numord, td_riferim As Riferim, " & _
        "td_cup, td_cig, td_riferimpa "
      ' Else
      'strSQL = strSQL & ", MIN(td_riferim) As Riferim"
      'End If
      strSQL = strSQL & " FROM TTMOPERNP INNER JOIN testord ON (TTMOPERNP.codditt = testord.codditt) AND (TTMOPERNP.mn_numord = testord.td_numord) AND (TTMOPERNP.mn_serie = testord.td_serie) AND (TTMOPERNP.mn_anno = testord.td_anno) AND (TTMOPERNP.mn_tipork = testord.td_tipork)" & _
        " WHERE TTMOPERNP.codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTMoPernp & _
        " GROUP BY mn_conto, mn_codagen, mn_codagen2, mn_tipobf, mn_codpaga, mn_codese," & _
        " mn_tmscont1, mn_tmscont2, mn_scopag, mn_valuta, mn_scorpo, mn_datapag, mn_flspinc," & _
        " mn_flbolli, mn_coddest, mn_coddest2, mn_magaz2"
      '    If bDtacUnaNotaPerImpegno = True Then
      strSQL = strSQL & ", mn_tipork, mn_anno, mn_serie, mn_numord, td_riferim, td_cup, td_cig, td_riferimpa "
      '    End If
      strSQL = strSQL & " HAVING Sum(mn_mmquant) > 0" & _
        " ORDER BY mn_conto, mn_codagen, mn_codagen2, mn_tipobf, mn_codpaga, mn_codese," & _
        " mn_tmscont1, mn_tmscont2, mn_scopag, mn_valuta, mn_scorpo, mn_datapag, mn_flspinc," & _
        " mn_flbolli,  mn_coddest, mn_coddest2"
      'If bDtacUnaNotaPerImpegno = True Then
      strSQL = strSQL & ", mn_tipork, mn_anno, mn_serie, mn_numord"
      ' End If

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetMovmag(ByVal strDitta As String, ByVal dtrTestMag As DataRow, _
                                        ByVal lIITTMoPernp As Integer, ByVal bModExtTCO As Boolean, _
                                        ByVal bDtacUnaNotaPerImpegno As Boolean, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strSQLTC As String = ""
    Dim dttTmpTC As New DataTable
    Dim i As Integer
    Dim strSQLTMBSelect As String = ""
    Dim strSQLTMBSelectTC As String = ""
    Dim strSQLTMBGroupbyTC As String = ""
    Dim strSQLTMBWhere As String = ""
    Dim strSQLTMBOrderby As String = ""
    Try
      With dtrTestMag
        strSQLTMBSelect = "SELECT TTMOPERNP.*, mo_coddivi " & _
                          " FROM TTMOPERNP INNER JOIN movord ON ttmopernp.codditt = movord.codditt " & _
                          " AND ttmopernp.mn_tipork = movord.mo_tipork AND ttmopernp.mn_anno = movord.mo_anno " & _
                          " AND ttmopernp.mn_serie = movord.mo_serie AND ttmopernp.mn_numord = movord.mo_numord " & _
                          " AND ttmopernp.mn_riga = movord.mo_riga " & _
                          " INNER JOIN testord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord "
        strSQLTMBSelectTC = "SELECT TTMOPERNP.codditt, instid, Min(TTMOPERNP.mn_conto) as mn_conto, Min(TTMOPERNP.mn_codagen) as mn_codagen, Min(TTMOPERNP.mn_codagen2) as mn_codagen2, Min(TTMOPERNP.mn_tipobf) as mn_tipobf, Min(TTMOPERNP.mn_codpaga) as mn_codpaga, Min(TTMOPERNP.mn_codese) as mn_codese, Min(TTMOPERNP.mn_tmscont1) as mn_tmscont1, Min(TTMOPERNP.mn_tmscont2) as mn_tmscont2," & _
                            " Min(TTMOPERNP.mn_scopag) as mn_scopag, Min(TTMOPERNP.mn_valuta) as mn_valuta, Min(TTMOPERNP.mn_scorpo) as mn_scorpo, Min(TTMOPERNP.mn_datapag) as mn_datapag, Min(TTMOPERNP.mn_flspinc) as mn_flspinc, Min(TTMOPERNP.mn_flbolli) as mn_flbolli, Min(TTMOPERNP.mn_abi) as mn_abi, Min(TTMOPERNP.mn_cab) as mn_cab, Min(TTMOPERNP.mn_coddest) as mn_coddest, Min(TTMOPERNP.mn_coddest2) as mn_coddest2," & _
                            " Min(TTMOPERNP.mn_datord) as mn_datord, Min(TTMOPERNP.mn_descr1) as mn_descr1, Min(TTMOPERNP.mn_tddatcons) as mn_tddatcons, Min(TTMOPERNP.mn_soloasa) as mn_soloasa, Min(TTMOPERNP.mn_despaga) as mn_despaga, Min(TTMOPERNP.mn_destpbf) as mn_destpbf, Min(TTMOPERNP.mn_rating) as mn_rating, Min(TTMOPERNP.mn_banc1) as mn_banc1, Min(TTMOPERNP.mn_banc2) as mn_banc2, Min(TTMOPERNP.mn_tipork) as mn_tipork," & _
                            " mn_anno, mn_serie, mn_numord, mn_riga, Min(TTMOPERNP.mn_codart) as mn_codart, Min(TTMOPERNP.mn_datcons) as mn_datcons, Min(TTMOPERNP.mn_magaz) as mn_magaz, Min(TTMOPERNP.mn_magaz2) as mn_magaz2, Min(TTMOPERNP.mn_unmis) as mn_unmis, Min(TTMOPERNP.mn_descr) as mn_descr," & _
                            " Min(TTMOPERNP.mn_colli) as mn_colli, Min(TTMOPERNP.mn_coleva) as mn_coleva, Min(TTMOPERNP.mn_quant) as mn_quant, Min(TTMOPERNP.mn_quaeva) as mn_quaeva, Min(TTMOPERNP.mn_flevas) as mn_flevas, Min(TTMOPERNP.mn_colpre) as mn_colpre, Min(TTMOPERNP.mn_quapre) as mn_quapre, Min(TTMOPERNP.mn_flevapre) as mn_flevapre, Min(TTMOPERNP.mn_prezzo) as mn_prezzo, Min(TTMOPERNP.mn_scont1) as mn_scont1," & _
                            " Min(TTMOPERNP.mn_scont2) as mn_scont2, Min(TTMOPERNP.mn_scont3) as mn_scont3, Min(TTMOPERNP.mn_provv) as mn_provv, Min(TTMOPERNP.mn_codiva) as mn_codiva, Min(TTMOPERNP.mn_preziva) as mn_preziva, Min(TTMOPERNP.mn_prezvalc) as mn_prezvalc, Min(TTMOPERNP.mn_commen) as mn_commen, Min(TTMOPERNP.mn_controp) as mn_controp, Min(TTMOPERNP.mn_stasino) as mn_stasino," & _
                            " Min(TTMOPERNP.mn_provv2) as mn_provv2, Min(TTMOPERNP.mn_tiporkor) as mn_tiporkor, Min(TTMOPERNP.mn_annoor) as mn_annoor, Min(TTMOPERNP.mn_serieor) as mn_serieor, Min(TTMOPERNP.mn_numordor) as mn_numordor, Min(TTMOPERNP.mn_rigaor) as mn_rigaor, Min(TTMOPERNP.mn_prelist) as mn_prelist, Min(TTMOPERNP.mn_codcfam) as mn_codcfam, Min(TTMOPERNP.mn_commeca) as mn_commeca, Min(TTMOPERNP.mn_subcommeca) as mn_subcommeca," & _
                            " Sum(TTMOPERNP.mn_valore) as mn_valore, Min(TTMOPERNP.mn_contocontr) as mn_contocontr, Min(TTMOPERNP.mn_codcena) as mn_codcena, Min(TTMOPERNP.mn_desint) as mn_desint, Min(TTMOPERNP.mn_codvuo) as mn_codvuo, Min(TTMOPERNP.mn_vprovv) as mn_vprovv, Min(TTMOPERNP.mn_vprovv2) as mn_vprovv2, Min(TTMOPERNP.mn_ump) as mn_ump, Min(TTMOPERNP.mn_confermato) as mn_confermato, Min(TTMOPERNP.mn_lotto) as mn_lotto," & _
                            " Sum(TTMOPERNP.mn_mmquant) as mn_mmquant, Min(TTMOPERNP.mn_mmcolli) as mn_mmcolli, Min(TTMOPERNP.mn_mmflevas) as mn_mmflevas, Min(TTMOPERNP.mn_mmvalore) as mn_mmvalore, Min(TTMOPERNP.mn_mmvprovv) as mn_mmvprovv, Min(TTMOPERNP.mn_mmvprovv2) as mn_mmvprovv2, Min(TTMOPERNP.mn_mmqtadisimp) as mn_mmqtadisimp, Min(TTMOPERNP.mn_mmcoldisimp) as mn_mmcoldisimp, Min(TTMOPERNP.mn_mmvaldisimp) as mn_mmvaldisimp, Min(TTMOPERNP.mn_perqta) as mn_perqta," & _
                            " Min(TTMOPERNP.mn_pagato) as mn_pagato, Min(TTMOPERNP.mn_pagatov) as mn_pagatov, Min(TTMOPERNP.mn_abbuono) as mn_abbuono, Min(TTMOPERNP.mn_abbuonov) as mn_abbuonov, Min(TTMOPERNP.mn_speacc) as mn_speacc, Min(TTMOPERNP.mn_speaccv) as mn_speaccv, Min(TTMOPERNP.mn_speimb) as mn_speimb, Min(TTMOPERNP.mn_speimbv) as mn_speimbv, Min(TTMOPERNP.mn_cambio) as mn_cambio, Min(TTMOPERNP.mn_scont4) as mn_scont4," & _
                            " Min(TTMOPERNP.mn_scont5) as mn_scont5, Min(TTMOPERNP.mn_scont6) as mn_scont6, Min(TTMOPERNP.mn_misura1) as mn_misura1, Min(TTMOPERNP.mn_misura2) as mn_misura2, Min(TTMOPERNP.mn_misura3) as mn_misura3, Min(TTMOPERNP.mn_mmmisura1) as mn_mmmisura1, Min(TTMOPERNP.mn_mmmisura2) as mn_mmmisura2, Min(TTMOPERNP.mn_mmmisura3) as mn_mmmisura3, Min(TTMOPERNP.mn_flstat) as mn_flstat, Min(TTMOPERNP.mn_pmtaskid) as mn_pmtaskid," & _
                            " Min(TTMOPERNP.mn_pmsalcon) as mn_pmsalcon, Min(TTMOPERNP.mn_pmqtadis) as mn_pmqtadis, Min(TTMOPERNP.mn_pmvaldis) as mn_pmvaldis, Min(TTMOPERNP.mn_mmvalorev) as mn_mmvalorev, Min(TTMOPERNP.mn_ubicaz) as mn_ubicaz, Min(TTMOPERNP.mn_fase) as mn_fase, Min(TTMOPERNP.mn_flkit) as mn_flkit, Min(TTMOPERNP.mn_ktriga) as mn_ktriga, mn_rigaa, Min(TTMOPERNP.mn_codtpro) as mn_codtpro," & _
                            " Min(TTMOPERNP.mn_flprznet) as mn_flprznet, Min(TTMOPERNP.mn_umprz) as mn_umprz, Min(TTMOPERNP.mn_ultagg) as mn_ultagg, Min(TTMOPERNP.mn_tctaglia) as mn_tctaglia, Min(TTMOPERNP.mn_tcindtagl) as mn_tcindtagl, " & _
                            " Min(mo_coddivi) as mo_coddivi, Min(TTMOPERNP.mn_scontp) as mn_scontp, Min(TTMOPERNP.mn_scontv) as mn_scontv" & _
                            " FROM TTMOPERNP INNER JOIN movord ON ttmopernp.codditt = movord.codditt " & _
                            " AND ttmopernp.mn_tipork = movord.mo_tipork AND ttmopernp.mn_anno = movord.mo_anno " & _
                            " AND ttmopernp.mn_serie = movord.mo_serie AND ttmopernp.mn_numord = movord.mo_numord " & _
                            " AND ttmopernp.mn_riga = movord.mo_riga " & _
                            " INNER JOIN testord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord "
        strSQLTMBGroupbyTC = " GROUP BY mn_tipork, TTMOPERNP.codditt, instid, mn_anno, mn_serie, mn_numord, mn_riga, mn_rigaa"
        strSQLTMBWhere = " WHERE mn_conto = " & NTSCStr(!mn_conto) & _
          " AND mn_codagen = " & NTSCStr(!mn_codagen) & _
          " AND mn_codagen2 = " & NTSCStr(!mn_codagen2) & _
          " AND mn_tipobf = " & NTSCStr(!mn_tipobf) & _
          " AND mn_codpaga = " & NTSCStr(!mn_codpaga) & _
          " AND mn_codese = " & NTSCStr(!mn_codese) & _
          " AND mn_tmscont1 = " & NTSCStr(!mn_tmscont1) & _
          " AND mn_tmscont2 = " & NTSCStr(!mn_tmscont2) & _
          " AND mn_scopag = " & CDblSQL(NTSCDec(!mn_scopag)) & _
          " AND mn_valuta = " & NTSCStr(!mn_valuta) & _
          " AND mn_scorpo = " & CStrSQL(!mn_scorpo) & _
          " AND mn_datapag " & NTSCStr(IIf(NTSCStr(!mn_datapag) = "", " IS ", " = ")) & CDataSQL(NTSCStr(!mn_datapag)) & _
          " AND mn_flspinc = " & CStrSQL(!mn_flspinc) & _
          " AND mn_flbolli = " & CStrSQL(!mn_flbolli) & _
          " AND mn_magaz2 = " & NTSCStr(!mn_magaz2) & _
          " AND mn_coddest = " & NTSCStr(!mn_coddest) & _
          " AND mn_coddest2 = " & NTSCStr(!mn_coddest2)
        If NTSCStr(!td_cup).Trim = "" Then
          strSQLTMBWhere += " AND td_cup IS NULL"
        Else
          strSQLTMBWhere += " AND td_cup = " & CStrSQL(NTSCStr(!td_cup))
        End If
        If NTSCStr(!td_cig).Trim = "" Then
          strSQLTMBWhere += " AND td_cig IS NULL"
        Else
          strSQLTMBWhere += " AND td_cig = " & CStrSQL(NTSCStr(!td_cig))
        End If
        If NTSCStr(!td_riferimpa).Trim = "" Then
          strSQLTMBWhere += " AND td_riferimpa IS NULL"
        Else
          strSQLTMBWhere += " AND td_riferimpa = " & CStrSQL(NTSCStr(!td_riferimpa))
        End If

        If bDtacUnaNotaPerImpegno = True Then
          strSQLTMBWhere = strSQLTMBWhere & " AND mn_anno = " & NTSCStr(!mn_anno) & _
            " AND mn_serie = " & CStrSQL(!mn_serie) & _
            " AND mn_numord = " & NTSCStr(!mn_numord)
        End If
      End With
      If NTSCDec(GetSettingBus("BSORGNNP", "Opzioni", ".", "ScriviRighe0", "0", " ", "0")) = 0 Then ' NON DOCUMENTARE
        strSQLTMBWhere = strSQLTMBWhere & " AND (mn_mmquant > 0 or (mn_codart IN ('D', 'M') and mn_mmquant = 0)) AND instid = " & lIITTMoPernp & " "
      Else
        'nella nota di prelievo scrivo anche le righe con quantità evadibile = 0
        strSQLTMBWhere = strSQLTMBWhere & " AND (mn_mmquant >= 0 or (mn_codart IN ('D', 'M') and mn_mmquant = 0)) AND instid = " & lIITTMoPernp & " "
      End If
      strSQLTMBOrderby = " ORDER BY mn_tipork, mn_anno, mn_serie, mn_numord, mn_riga, mn_rigaa"

      strSQL = strSQLTMBSelect & strSQLTMBWhere & " AND mn_tcindtagl = 0" & strSQLTMBOrderby

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se c'è modulo taglie colori aggrega per taglia
      If bModExtTCO Then
        strSQLTC = strSQLTMBSelectTC & strSQLTMBWhere & " AND mn_tcindtagl <> 0" & strSQLTMBGroupbyTC & strSQLTMBOrderby

        dttTmpTC = OpenRecordset(strSQLTC, CLE__APP.DBTIPO.DBAZI)

        For i = 0 To dttTmpTC.Rows.Count - 1
          dttTmp.ImportRow(dttTmpTC.Rows(i))
        Next
        dttTmp.AcceptChanges()
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetMatricole(ByVal strDitta As String, ByVal dtrMm As DataRow, _
                                        ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movmatr.* FROM movmatr" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mma_tipork = " & CStrSQL(dtrMm!mm_tipork) & " AND mma_anno = " & NTSCInt(dtrMm!mm_anno) & _
        " AND mma_serie = " & CStrSQL(dtrMm!mm_serie) & " AND mma_numdoc = " & NTSCInt(dtrMm!mm_numdoc) & _
        " AND mma_riga = " & NTSCInt(dtrMm!mm_riga)

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetMovmagtc(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                           ByVal strAnno As String, ByVal strSerie As String, _
                                           ByVal strNumord As String, ByVal strRiga As String, _
                                           ByVal i As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mn_mmquant as mn_mmquant" & i.ToString.PadLeft(2, "0"c) & " FROM TTMOPERNP" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTMoPernp & _
          " AND mn_anno = " & strAnno & _
          " AND mn_serie = " & CStrSQL(strSerie) & _
          " AND mn_numord = " & strNumord & _
          " AND mn_riga = " & strRiga & _
          " AND mn_tcindtagl = " & i

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataSenu(ByVal strDitta As String, ByVal lSenuInstid As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TTOLTOMO.* FROM TTOLTOMO" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lSenuInstid & _
        " ORDER BY instid, tt_moserie, tt_monumord"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTOLTOMO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function SelezionaMovord(ByVal strDitta As String, ByRef lRecordAf As Integer, _
                                              ByVal lIITTMoPernp As Integer, ByVal bModExtTCO As Boolean, _
                                              ByVal strSeicQuery As String) As Boolean
    'SELEZIONI GLI IMPEGNI CHE DOVRANNO ESSERE ASSEGNATI ALLE NOTE DI PRELIEVO
    Dim strSQL As String = ""
    Dim bClieConBlocco As Boolean
    Dim strKit As String = ""
    Dim i As Integer
    Try
      bClieConBlocco = CBool(GetSettingBus("BSORGNNP", "OPZIONI", ".", "ElaboraClientiBloccati", "0", ".", "0"))
      'Le righe dei kit vengono trattate come righe normali e il padre potrebbe andare perso se non ha esistenza.
      'Per questo motivi i legami delle righe kit vengono mantenuti solo quando si indica di ignorare la disponibilità dell'articolo.
      If CBool(GetSettingBus("BSORGNNP", "OPZIONI", ".", "IgnoraDisponibilita", "0", " ", "0")) Then
        strKit = "mo_flkit"
      Else
        strKit = "' '"
      End If


      lRecordAf = 0
      strSQL = " INSERT INTO TTMOPERNP (codditt, instid, mn_conto , mn_codagen, mn_codagen2, mn_tipobf, mn_codpaga, mn_codese, mn_tmscont1, mn_tmscont2, mn_scopag, mn_valuta, mn_scorpo, mn_datapag, mn_flspinc, mn_flbolli, mn_abi, mn_cab, mn_coddest, mn_coddest2, mn_datord, mn_descr1, mn_tddatcons, mn_soloasa, mn_despaga, mn_destpbf, mn_rating, mn_banc1, mn_banc2, " & _
        " mn_tipork, mn_anno, mn_serie, mn_numord, mn_riga, mn_codart, mn_datcons, mn_magaz, mn_magaz2, " & _
        " mn_unmis, mn_descr, mn_colli, mn_coleva, mn_quant, mn_quaeva, mn_flevas, mn_colpre, mn_quapre, mn_flevapre, mn_prezzo, mn_scont1, mn_scont2, mn_scont3, mn_provv, mn_codiva, mn_preziva, mn_prezvalc, mn_commen, mn_note, mn_controp, mn_stasino , mn_provv2, mn_tiporkor, mn_annoor, mn_serieor, mn_numordor, " & _
        " mn_rigaor, mn_prelist, mn_codcfam, mn_commeca, mn_subcommeca, mn_valore, mn_contocontr, mn_codcena, mn_desint, mn_codvuo, mn_vprovv, mn_vprovv2, mn_ump, mn_confermato, mn_lotto, mn_mmquant, mn_mmcolli, mn_mmflevas, mn_mmvalore, mn_mmvprovv, mn_mmvprovv2, mn_mmqtadisimp, mn_mmcoldisimp, mn_mmvaldisimp, " & _
        " mn_perqta, mn_pagato, mn_pagatov, mn_abbuono, mn_abbuonov, mn_speacc, mn_speaccv, mn_speimb, mn_speimbv, mn_cambio, mn_scont4, mn_scont5, mn_scont6, mn_flstat, mn_misura1, mn_misura2, mn_misura3, mn_pmtaskid, mn_pmsalcon, mn_pmqtadis, mn_pmvaldis, mn_fase, mn_ubicaz, mn_codtpro, mn_flprznet, mn_ultagg, " & _
        " mn_scontp, mn_scontv, mn_umprz, mn_flkit)"
      strSQL = strSQL & " SELECT " & CStrSQL(strDitta) & ", " & lIITTMoPernp & ", td_conto, td_codagen, td_codagen2, td_tipobf, td_codpaga, td_codese, td_scont1, td_scont2, td_scopag, td_valuta, td_scorpo, td_datapag, td_flspinc, td_flboll, td_abi, td_cab, td_coddest, td_coddest2, td_datord, an_descr1, td_datcons, td_soloasa, tb_despaga, tb_destpbf, an_rating, td_banc1, td_banc2, " & _
        " mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_codart, mo_datcons, mo_magaz, td_magaz2, " & _
        " mo_unmis, mo_descr, mo_colli, mo_coleva, mo_quant, mo_quaeva, mo_flevas, mo_colpre, mo_quapre, mo_flevapre, mo_prezzo, mo_scont1, mo_scont2, mo_scont3, mo_provv, mo_codiva, mo_preziva, mo_prezvalc, CASE WHEN artico.ar_gescomm = 'S' THEN mo_commeca ELSE 0 END, mo_note, mo_controp, mo_stasino , mo_provv2, mo_tiporkor, mo_annoor, mo_serieor, mo_numordor, " & _
        " mo_rigaor, mo_prelist, mo_codcfam, mo_commeca, mo_subcommeca, mo_valore, mo_contocontr, mo_codcena, mo_desint, mo_codvuo, mo_vprovv, mo_vprovv2, mo_ump, mo_confermato, 0, 0, 0, 'C' , 0 , 0, 0, 0, 0, 0,  " & _
        " mo_perqta, td_pagato, td_pagatov, td_abbuono, td_abbuonov, td_speacc, td_speaccv, td_speimb, td_speimbv, td_cambio, mo_scont4, mo_scont5, mo_scont6, mo_ricimp, mo_misura1, mo_misura2, mo_misura3, mo_pmtaskid, mo_pmsalcon, mo_pmqtadis, mo_pmvaldis," & _
        " mo_fase, mo_ubicaz, mo_codtpro, mo_flprznet, mo_ultagg, mo_scontp, mo_scontv, mo_umprz, " & strKit & _
        " FROM ((((testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord)" & _
        " INNER JOIN anagra ON anagra.codditt = testord.codditt AND anagra.an_conto = testord.td_conto)" & _
        " INNER JOIN tabtpbf on tabtpbf.codditt = testord.codditt AND tabtpbf.tb_codtpbf = testord.td_tipobf)" & _
        " INNER JOIN tabpaga ON tabpaga.tb_codpaga = testord.td_codpaga)" & _
        " INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart" & _
        " WHERE testord.codditt = " & CStrSQL(strDitta) & _
        " AND td_blocco = 'N' " & NTSCStr(IIf(bClieConBlocco, "", "AND an_blocco = 'N'")) & " and td_sospeso = 'N' AND mo_flevas <> 'S' AND mo_flevapre <> 'S' AND (" & strSeicQuery & ") AND ar_codtagl = 0" & _
        " ORDER BY td_conto, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga "

      lRecordAf = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se c'è modulo taglie colori disaggrega per taglia
      If bModExtTCO Then
        For i = 1 To 24
          strSQL = " INSERT INTO TTMOPERNP (codditt, instid, mn_conto , mn_codagen, mn_codagen2, mn_tipobf, mn_codpaga, mn_codese, mn_tmscont1, mn_tmscont2, mn_scopag, mn_valuta, mn_scorpo, mn_datapag, mn_flspinc, mn_flbolli, mn_abi, mn_cab, mn_coddest, mn_coddest2, mn_datord, mn_descr1, mn_tddatcons, mn_soloasa, mn_despaga, mn_destpbf, mn_rating, mn_banc1, mn_banc2, " & _
            " mn_tipork, mn_anno, mn_serie, mn_numord, mn_riga, mn_codart, mn_datcons, mn_magaz, mn_magaz2, " & _
            " mn_unmis, mn_descr, mn_colli, mn_coleva, mn_quant, mn_quaeva, mn_flevas, mn_colpre, mn_quapre, mn_flevapre, mn_prezzo, mn_scont1, mn_scont2, mn_scont3, mn_provv, mn_codiva, mn_preziva, mn_prezvalc, mn_commen, mn_note, mn_controp, mn_stasino , mn_provv2, mn_tiporkor, mn_annoor, mn_serieor, mn_numordor, " & _
            " mn_rigaor, mn_prelist, mn_codcfam, mn_commeca, mn_subcommeca, mn_valore, mn_contocontr, mn_codcena, mn_desint, mn_codvuo, mn_vprovv, mn_vprovv2, mn_ump, mn_confermato, mn_lotto, mn_mmquant, mn_mmcolli, mn_mmflevas, mn_mmvalore, mn_mmvprovv, mn_mmvprovv2, mn_mmqtadisimp, mn_mmcoldisimp, mn_mmvaldisimp, " & _
            " mn_perqta, mn_pagato, mn_pagatov, mn_abbuono, mn_abbuonov, mn_speacc, mn_speaccv, mn_speimb, mn_speimbv, mn_cambio, mn_scont4, mn_scont5, mn_scont6, mn_flstat, mn_misura1, mn_misura2, mn_misura3, mn_pmtaskid, mn_pmsalcon, mn_pmqtadis, mn_pmvaldis, mn_fase, mn_ubicaz, mn_codtpro, mn_flprznet, mn_ultagg, mn_tctaglia, mn_tcindtagl, " & _
            " mn_scontp, mn_scontv)"
          strSQL = strSQL & " SELECT " & CStrSQL(strDitta) & ", " & lIITTMoPernp & ", td_conto, td_codagen, td_codagen2, td_tipobf, td_codpaga, td_codese, td_scont1, td_scont2, td_scopag, td_valuta, td_scorpo, td_datapag, td_flspinc, td_flboll, td_abi, td_cab, td_coddest, td_coddest2, td_datord, an_descr1, td_datcons, td_soloasa, tb_despaga, tb_destpbf, an_rating, td_banc1, td_banc2, " & _
            " movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga, mo_codart, mo_datcons, mo_magaz, td_magaz2, " & _
            " mo_unmis, mo_descr, mo_colli, mo_coleva, movordtc.mo_quant" & Right("00" & i, 2) & ", movordtc.mo_quaeva" & Right("00" & i, 2) & ", mo_flevas, mo_colpre, movordtc.mo_quapre" & Right("00" & i, 2) & ", mo_flevapre, mo_prezzo, mo_scont1, mo_scont2, mo_scont3, mo_provv, mo_codiva, mo_preziva, mo_prezvalc, CASE WHEN artico.ar_gescomm = 'S' THEN mo_commeca ELSE 0 END, mo_note, mo_controp, mo_stasino , mo_provv2, mo_tiporkor, mo_annoor, mo_serieor, mo_numordor, " & _
            " mo_rigaor, mo_prelist, mo_codcfam, mo_commeca, mo_subcommeca, mo_prezzo * movordtc.mo_quant" & Right("00" & i, 2) & ", mo_contocontr, mo_codcena, mo_desint, mo_codvuo, mo_vprovv, mo_vprovv2, mo_ump, mo_confermato, 0, 0, 0, 'C' , 0 , 0, 0, 0, 0, 0,  " & _
            " mo_perqta, td_pagato, td_pagatov, td_abbuono, td_abbuonov, td_speacc, td_speaccv, td_speimb, td_speimbv, td_cambio, mo_scont4, mo_scont5, mo_scont6, mo_ricimp, mo_misura1, mo_misura2, mo_misura3, mo_pmtaskid, mo_pmsalcon, mo_pmqtadis, mo_pmvaldis," & _
            " mo_fase, mo_ubicaz, mo_codtpro, mo_flprznet, mo_ultagg, tabtagl.tb_dest" & Right("00" & i, 2) & ", " & i & ", mo_scontp, mo_scontv " & _
            " FROM ((((((testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord)" & _
            " INNER JOIN anagra ON anagra.codditt = testord.codditt AND anagra.an_conto = testord.td_conto)" & _
            " INNER JOIN tabtpbf on tabtpbf.codditt = testord.codditt AND tabtpbf.tb_codtpbf = testord.td_tipobf)" & _
            " INNER JOIN tabpaga ON tabpaga.tb_codpaga = testord.td_codpaga)" & _
            " INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart)" & _
            " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt AND artico.ar_codtagl = tabtagl.tb_codtagl)" & _
            " INNER JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga" & _
            " WHERE testord.codditt = " & CStrSQL(strDitta) & _
            " AND td_blocco = 'N' " & NTSCStr(IIf(bClieConBlocco, "", "AND an_blocco = 'N'")) & " and td_sospeso = 'N' AND mo_flevas <> 'S' AND mo_flevapre <> 'S' AND (" & strSeicQuery & ") AND ar_codtagl <> 0" & _
            " AND movordtc.mo_quant" & Right("00" & i, 2) & " <> 0" & _
            " ORDER BY td_conto, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga "

          lRecordAf = lRecordAf + Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        Next
      End If

      'td_datapag: viene utilizzata come sistma di raggruppamento nel resto del programma. 
      'inizialmente (fino net 2011 sr5) non veniva considerata. ora la considera, ma serve una query che
      'resetti il campo impostandolo = null se tabpaga.tb_decpaga <> 4 (data diversa) altrimenti spezzerebbe per ogni data documento
      If lRecordAf <> 0 Then
        strSQL = "UPDATE ttmopernp SET mn_datapag = null " & _
                 " FROM ttmopernp LEFT JOIN tabpaga ON ttmopernp.mn_codpaga = tabpaga.tb_codpaga " & _
                 " WHERE ttmopernp.codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIITTMoPernp & _
                 " AND isnull(tb_decpaga, 0) <> 4"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function AssegnaMovord(ByVal strDitta As String, ByVal bRielab As Boolean, _
                                            ByVal lIITTDispNet As Integer, ByVal lIITTMoPernp As Integer, _
                                            ByVal nSeicMagaz As Integer, ByVal bModExtTCO As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strSQL1 As String = ""
    Dim i As Integer
    Dim dsTmp55 As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se rielaborazione, ripulisce i campi
      '-----------------------------------------------------------------------------------------
      If bRielab = True Then
        strSQL1 = "UPDATE TTDISPNET" & _
          " SET dn_qtaass = 0" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTDispNet
        Execute(strSQL1, CLE__APP.DBTIPO.DBAZI)
        strSQL1 = "UPDATE TTMOPERNP" & _
          " SET mn_mmquant = 0, mn_mmcolli = 0, mn_mmflevas = 'C', mn_mmvalore = 0," & _
          " mn_mmvalorev = 0, mn_mmvprovv = 0, mn_mmvprovv2 = 0, mn_lotto = 0, " & _
          " mn_mmqtadisimp = 0, mn_mmcoldisimp = 0, mn_mmvaldisimp = 0 " & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTMoPernp
        Execute(strSQL1, CLE__APP.DBTIPO.DBAZI)
        'Accorpa le righe che ha eventualmente 'spaccato' da una assegnazione a lotti precedente
        'Le righe spaccate sono quelle che possiedono mn_rigaa > 0
        '1)Selezione le righe d'ordine doppie
        strSQL = "SELECT Count(*) AS NumeroRighe, mn_tipork, mn_anno, mn_serie, mn_numord, mn_riga " & _
                 "FROM TTMOPERNP " & _
                 "WHERE TTMOPERNP.codditt = " & CStrSQL(strDitta) & " AND TTMOPERNP.instid = " & lIITTMoPernp & " " & _
                 "AND mn_tcindtagl = 0 " & _
                 "GROUP BY mn_tipork, mn_anno, mn_serie, mn_numord, mn_riga " & _
                 "HAVING Count(*) > 1"
        dsTmp55 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTMOPERNP")
        For i = 0 To dsTmp55.Tables("TTMOPERNP").Rows.Count - 1
          With dsTmp55.Tables("TTMOPERNP").Rows(i)
            '2)Per ognuna riprende i dati di quantità/colli/valore (che ha modificato durante lo split)
            strSQL = "UPDATE TTMOPERNP " & _
                     "SET TTMOPERNP.mn_quant = movord.mo_quant, TTMOPERNP.mn_colli = movord.mo_colli, TTMOPERNP.mn_valore = movord.mo_valore " & _
                     "FROM TTMOPERNP INNER JOIN movord ON TTMOPERNP.codditt = movord.codditt AND TTMOPERNP.mn_tipork = movord.mo_tipork AND TTMOPERNP.mn_anno = movord.mo_anno AND TTMOPERNP.mn_serie = movord.mo_serie AND TTMOPERNP.mn_numord = movord.mo_numord AND TTMOPERNP.mn_riga = movord.mo_riga " & _
                     "WHERE TTMOPERNP.codditt = " & CStrSQL(strDitta) & " AND TTMOPERNP.instid = " & lIITTMoPernp & " " & _
                     "AND mn_tipork = '" & NTSCStr(!mn_tipork) & "' AND mn_anno = " & NTSCStr(!mn_anno) & " AND mn_serie = '" & NTSCStr(!mn_serie) & "' AND mn_numord = " & NTSCStr(!mn_numord) & " AND mn_riga = " & NTSCStr(!mn_riga) & " " & _
                     "AND mn_rigaa = 0 AND mn_tcindtagl = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            '3)Quindi cancella le righe con mn_rigaa > 0
            strSQL = "DELETE FROM TTMOPERNP " & _
                     "WHERE TTMOPERNP.codditt = " & CStrSQL(strDitta) & " AND TTMOPERNP.instid = " & lIITTMoPernp & " " & _
                     "AND mn_tipork = '" & NTSCStr(!mn_tipork) & "' AND mn_anno = " & NTSCStr(!mn_anno) & " AND mn_serie = '" & NTSCStr(!mn_serie) & "' AND mn_numord = " & NTSCStr(!mn_numord) & " AND mn_riga = " & NTSCStr(!mn_riga) & " " & _
                     "AND mn_rigaa > 0 AND mn_tcindtagl = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          End With
        Next
      Else
        '---------------------------------------------------------------------------------------
        '--- Adesso determina disponibilità netta articoli (Solo quelli interressati)
        '--- (TUTTE LE FASI!!!)
        '---------------------------------------------------------------------------------------
        strSQL1 = "INSERT INTO TTDISPNET (codditt, instid, dn_codart, dn_commeca, dn_dispnet," & _
          " dn_unmis, dn_confez2, dn_unmis2, dn_qtacon2, dn_conver, dn_qtaass, dn_descr," & _
          " dn_fase, dn_lotto)" & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTDispNet & ", lp_codart," & _
          " lp_commeca, Sum(lp_esist - lp_prenot) AS expr1, Min(ar_unmis), Min(ar_confez2), Min(ar_unmis2)," & _
          " Min(ar_qtacon2), Min(ar_conver), 0, Min(ar_descr), lp_fase, lp_lotto" & _
          " FROM lotcpro INNER JOIN artico ON lotcpro.codditt = artico.codditt AND lotcpro.lp_codart = artico.ar_codart" & _
          " WHERE lotcpro.codditt = " & CStrSQL(strDitta) & _
          " AND lp_codart IN (SELECT mn_codart FROM TTMOPERNP" & _
                            " WHERE TTMOPERNP.codditt = " & CStrSQL(strDitta) & _
                            " AND instid = " & lIITTMoPernp & ")" & _
          " AND lp_magaz = " & nSeicMagaz
        If CBool(GetSettingBus("BSORGNNP", "OPZIONI", ".", "IgnoraDisponibilita", "0", " ", "0")) = False Then
          strSQL1 &= " AND (lp_esist - lp_prenot) <> 0"
        Else
          'solo per articoli non gestiti a magazzino: li carico sempre se settata l'opzione di registro
          strSQL1 &= " AND ((lp_esist - lp_prenot) <> 0 OR ar_stainv = 'N')"
        End If
        strSQL1 &= " AND ar_codtagl = 0" & _
                   " GROUP BY lotcpro.codditt, lp_codart, lp_magaz, lp_commeca, lp_lotto, lp_fase" & _
                   " ORDER BY lotcpro.codditt, lp_codart, lp_magaz, lp_commeca, lp_lotto, lp_fase"
        Execute(strSQL1, CLE__APP.DBTIPO.DBAZI)
        'se c'è modulo taglie colori disaggrega per taglia
        If bModExtTCO Then
          For i = 1 To 24
            strSQL1 = "INSERT INTO TTDISPNET (codditt, instid, dn_codart, dn_commeca, dn_dispnet," & _
              " dn_unmis, dn_confez2, dn_unmis2, dn_qtacon2, dn_conver, dn_qtaass, dn_descr," & _
              " dn_fase, dn_lotto, dn_tctaglia, dn_tcindtagl)" & _
              " SELECT " & CStrSQL(strDitta) & ", " & lIITTDispNet & ", lp_codart," & _
              " lp_commeca, Sum(apt_esist" & Right("00" & i, 2) & " - apt_prenot" & Right("00" & i, 2) & ") AS expr1, Min(ar_unmis), Min(ar_confez2), Min(ar_unmis2)," & _
              " Min(ar_qtacon2), Min(ar_conver), 0, Min(ar_descr), lp_fase, lp_lotto, Min(tb_dest" & Right("00" & i, 2) & "), " & i & _
              " FROM ((lotcpro INNER JOIN artico ON lotcpro.codditt = artico.codditt AND lotcpro.lp_codart = artico.ar_codart)" & _
              " INNER JOIN lotcprotc ON lotcpro.codditt = lotcprotc.codditt AND lotcpro.lp_codart = lotcprotc.apt_codart AND lotcpro.lp_magaz = lotcprotc.apt_magaz AND lotcpro.lp_commeca = lotcprotc.apt_commeca AND lotcpro.lp_lotto = lotcprotc.apt_lotto AND lotcpro.lp_fase = lotcprotc.apt_fase AND lotcpro.lp_ubicaz = lotcprotc.apt_ubicaz)" & _
              " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
              " WHERE lotcpro.codditt = " & CStrSQL(strDitta) & _
              " AND lp_codart IN (SELECT mn_codart FROM TTMOPERNP" & _
                                " WHERE TTMOPERNP.codditt = " & CStrSQL(strDitta) & _
                                " AND instid = " & lIITTMoPernp & _
                                " AND mn_tctaglia = tb_dest" & Right("00" & i, 2) & ")" & _
              " AND lp_magaz = " & nSeicMagaz

            If CBool(GetSettingBus("BSORGNNP", "OPZIONI", ".", "IgnoraDisponibilita", "0", " ", "0")) = False Then
              strSQL1 &= " AND (apt_esist" & Right("00" & i, 2) & " - apt_prenot" & Right("00" & i, 2) & ") <> 0"
            Else
              'solo per articoli non gestiti a magazzino: li carico sempre se settata l'opzione di registro
              strSQL1 &= " AND ((apt_esist" & Right("00" & i, 2) & " - apt_prenot" & Right("00" & i, 2) & ") <> 0 OR ar_stainv = 'N')"
            End If

            strSQL1 &= " AND ar_codtagl <> 0" & _
                       " GROUP BY lotcpro.codditt, lp_codart, lp_magaz, lp_commeca, lp_lotto, lp_fase" & _
                       " ORDER BY lotcpro.codditt, lp_codart, lp_magaz, lp_commeca, lp_lotto, lp_fase"
            Execute(strSQL1, CLE__APP.DBTIPO.DBAZI)
          Next
        End If
        '---------------------------------------------------------------------------------------
        '--- Le righe che c'erano prima qui, sono nella routine (non utilizzata) "APPOGGIO"
        '--- adesso tenta di inserire in TTDISPNET tutti i record coppia articolo/commessa che ci sono
        '--- in TTMOPENP ma non ci sono in TTDISPNET (per il report, che usa una INNER JOIN, necessariamente..)
        '---------------------------------------------------------------------------------------
        strSQL = "SELECT DISTINCT mn_codart, mn_commen, mn_fase, artico.ar_unmis," & _
            " artico.ar_confez2, artico.ar_unmis2, artico.ar_qtacon2, artico.ar_conver, 0," & _
            " artico.ar_descr, mn_lotto, mn_tcindtagl, mn_tctaglia" & _
            " FROM TTMOPERNP INNER JOIN ARTICO ON TTMOPERNP.codditt = ARTICO.codditt AND TTMOPERNP.mn_codart = ARTICO.ar_codart" & _
            " WHERE TTMOPERNP.codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTMoPernp
        dsTmp55 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTMOPERNP")
        For i = 0 To dsTmp55.Tables("TTMOPERNP").Rows.Count - 1
          With dsTmp55.Tables("TTMOPERNP").Rows(i)

            strSQL = "SELECT dn_codart FROM TTDISPNET" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTDispNet & _
              " AND dn_codart = " & CStrSQL(!mn_codart) & _
              " AND dn_commeca = " & NTSCStr(!mn_commen) & _
              " AND dn_fase = " & NTSCStr(!mn_fase) & _
              " AND dn_lotto = " & NTSCStr(!mn_lotto) & _
              " AND dn_tcindtagl = " & NTSCStr(!mn_tcindtagl)
            dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTDISPNET")

            If dsTmp.Tables("TTDISPNET").Rows.Count > 0 Then Continue For

            Try 'gestisco trycatch se la chiave è gia presente in vb6 non da errore passa avanti
              strSQL = "INSERT INTO TTDISPNET (codditt, instid, dn_codart, dn_commeca, dn_dispnet," & _
                " dn_unmis, dn_confez2, dn_unmis2, dn_qtacon2, dn_conver, dn_qtaass, dn_descr," & _
                " dn_fase, dn_lotto, dn_tctaglia, dn_tcindtagl)" & _
                " VALUES (" & CStrSQL(strDitta) & ", " & lIITTDispNet & ", " & _
                CStrSQL(!mn_codart) & ", " & NTSCStr(!mn_commen) & " , 0, " & _
                CStrSQL(!ar_unmis) & ", " & CStrSQL(!ar_confez2) & ", " & _
                CStrSQL(!ar_unmis2) & ", " & CDblSQL(NTSCDec(!ar_qtacon2)) & ", " & _
                CDblSQL(NTSCDec(!ar_conver)) & ", 0, " & CStrSQL(!ar_descr) & ", " & _
                NTSCStr(!mn_fase) & ", 0, " & CStrSQL(!mn_tctaglia) & ", " & NTSCInt(!mn_tcindtagl) & ")"

              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            Catch ex As Exception
            End Try

          End With
        Next
        End If
        '-----------------------------------------------------------------------------------------
        '--- Adesso esegue una query che setta ad un valore altissimo le quantità degli
        '--- articoli non gestiti a magazzino: ARTICO.ar_stainv = 'N'
        '-----------------------------------------------------------------------------------------
        strSQL1 = "UPDATE TTDISPNET " & _
          " SET dn_dispnet = 9999999999" & _
          " FROM TTDISPNET" & _
          " INNER JOIN artico ON TTDISPNET.codditt = artico.codditt AND TTDISPNET.dn_codart = artico.ar_codart" & _
          " WHERE TTDISPNET.codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTDispNet
        If CBool(GetSettingBus("BSORGNNP", "OPZIONI", ".", "IgnoraDisponibilita", "0", " ", "0")) = False Then
          strSQL1 = strSQL1 & " AND ar_stainv = 'N'"
        End If
        Execute(strSQL1, CLE__APP.DBTIPO.DBAZI)

        Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function AssegnaMovordGetTTDispnet(ByVal strDitta As String, ByVal lIITTDispNet As Integer, _
    ByVal bMostraListaImpegniAncheSeTuttiNonEvadibili As Boolean, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim bLottoNew As Boolean = False
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Adesso comincia ad assegnare
      '--------------------------------------------------------------------------------------------------------------

      'gestione lotti alfanumerica: se è abilitata,
      'per gli articoli gestiti a lotti con evasione lotto automatica in base a lifo/fifo/... 
      'devo ordinare i record in modo che venga rispettato il criterio impostato in artico
      ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      End If
      dttTmp.Clear()

      If bLottoNew Then
        'gestione lotti alfanumerica: ovvero se l'articolo è gestito a lotti, è sempre presente analotti
        strSQL = "SELECT ttdispnet.*, ar_geslotti, ar_tipscarlotx, alo_dtscad, alo_dtcarbus, alo_lottox, cast(alo_dtcarbus as float) " & _
                  " FROM ttdispnet LEFT JOIN artico ON ttdispnet.codditt = artico.codditt " & _
                  " AND ttdispnet.dn_codart = artico.ar_codart" & _
                  " LEFT JOIN analotti ON ttdispnet.codditt = analotti.codditt " & _
                  " AND ttdispnet.dn_codart = analotti.alo_codart " & _
                  " AND ttdispnet.dn_lotto = analotti.alo_lotto" & _
                  " WHERE ttdispnet.codditt = " & CStrSQL(strDitta) & _
                  " AND instid = " & lIITTDispNet
        If bMostraListaImpegniAncheSeTuttiNonEvadibili = False Then strSQL += " AND dn_dispnet <> 0 "
        'visto che in caso di evasione per lifo/fifo devo ordinare alo_dtcarbus ASC o DESC 
        'e nella case when non è possibile farlo, ottengo i giorni dal 01/01/1900 e per la DESC
        'moltiplico per -1, così posso applicare anche in quel caso l'ordinamento ASC
        strSQL += " ORDER BY dn_codart, dn_fase, dn_commeca, " & _
                  " CASE WHEN ar_geslotti = 'N' OR ar_tipscarlotx = 'M' " & _
                  "      THEN dn_lotto " & _
                  "      ELSE  " & _
                  "         CASE ar_tipscarlotx " & _
                  "           WHEN 'D' THEN datediff(d, '1970-1-1', alo_dtscad) " & _
                  "           WHEN 'L' THEN datediff(d, '1970-1-1', alo_dtcarbus) * -1" & _
                  "           WHEN 'F' THEN datediff(d, '1970-1-1', alo_dtcarbus) " & _
                  "         END " & _
                  "      END ASC, " & _
                  "dn_lotto ASC, dn_tcindtagl"
      Else
        strSQL = "SELECT * FROM TTDISPNET" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTDispNet
        If bMostraListaImpegniAncheSeTuttiNonEvadibili = False Then strSQL += " AND dn_dispnet <> 0 "
        strSQL += " ORDER BY dn_codart, dn_fase, dn_commeca, dn_lotto, dn_tcindtagl"
      End If

      '--------------------------------------------------------------------------------------------------------------
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTDISPNET")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function AssegnaMovordGetTTMopernp(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                        ByVal lIITTMoPernp As Integer, ByVal nSeicPriorita As Integer, _
                                                        ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      '---------------------------------------------------------------------------------------
      '--- Apre dataset per righe movord trovate per quell'articolo,
      '--- in ordine di data consegna (emesse in TTMOPERNP)
      '---------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM TTMOPERNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mn_codart = " & CStrSQL(dtrTmp!dn_codart) & _
        " AND mn_fase = " & NTSCStr(dtrTmp!dn_fase) & _
        " AND mn_tcindtagl = " & NTSCStr(dtrTmp!dn_tcindtagl) & _
        " AND instid = " & lIITTMoPernp
      If NTSCInt(dtrTmp!dn_commeca) <> 0 Then strSQL = strSQL & " AND mn_commen = " & NTSCStr(dtrTmp!dn_commeca)
      If NTSCInt(dtrTmp!dn_lotto) <> 0 Then strSQL = strSQL & " AND mn_lotto = 0 "
      Select Case nSeicPriorita
        Case 3 ' data ordine
          strSQL = strSQL & " ORDER BY mn_datord, mn_datcons"
        Case 2 ' cod. cliente
          strSQL = strSQL & " ORDER BY mn_conto, mn_datcons"
        Case 4 ' affidamento
          strSQL = strSQL & " ORDER BY mn_rating, mn_datcons, mn_conto"
        Case 1 ' data consegna
          strSQL = strSQL & " ORDER BY mn_datcons, mn_conto"
      End Select
      strSQL = strSQL & ", mn_tipork, mn_anno, mn_serie, mn_numord, mn_riga, mn_rigaa, mn_tcindtagl"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTMOPERNP")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function AssegnaMovordInsertTTMopernp(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                                           ByVal dColli As Decimal, ByVal dResordine As Decimal, _
                                                           ByVal dResiduo As Decimal, ByVal dValore As Decimal, _
                                                           ByVal dtrTmp As DataRow) As Boolean
    Dim strSQL As String = ""
    Dim strCampiTTMOPERNP As String = ""
    Try
      strCampiTTMOPERNP = "codditt, instid, mn_conto, mn_codagen, mn_codagen2, mn_tipobf," & _
        " mn_codpaga, mn_codese, mn_tmscont1, mn_tmscont2, mn_scopag, mn_valuta, mn_scorpo," & _
        " mn_datapag, mn_flspinc, mn_flbolli, mn_abi, mn_cab, mn_coddest, mn_coddest2," & _
        " mn_datord, mn_descr1, mn_tddatcons, mn_soloasa, mn_despaga, mn_destpbf, mn_rating," & _
        " mn_banc1, mn_banc2, mn_tipork, mn_anno, mn_serie, mn_numord, mn_riga, mn_codart," & _
        " mn_datcons, mn_magaz, mn_magaz2, mn_unmis, mn_descr, mn_colli, mn_coleva, mn_quant," & _
        " mn_quaeva, mn_flevas, mn_colpre, mn_quapre, mn_flevapre, mn_prezzo, mn_scont1," & _
        " mn_scont2, mn_scont3, mn_provv, mn_codiva, mn_preziva, mn_prezvalc, mn_commen," & _
        " mn_note, mn_controp, mn_stasino, mn_provv2, mn_tiporkor, mn_annoor, mn_serieor," & _
        " mn_numordor, mn_rigaor, mn_prelist, mn_codcfam, mn_commeca, mn_subcommeca," & _
        " mn_valore, mn_contocontr, mn_codcena, mn_desint, mn_codvuo, mn_vprovv, mn_vprovv2," & _
        " mn_ump, mn_confermato, mn_lotto, mn_mmquant, mn_mmcolli, mn_mmflevas, mn_mmvalore," & _
        " mn_mmvprovv, mn_mmvprovv2, mn_mmqtadisimp, mn_mmcoldisimp, mn_mmvaldisimp," & _
        " mn_perqta , mn_pagato, mn_pagatov, mn_abbuono, mn_abbuonov, mn_speacc, mn_speaccv," & _
        " mn_speimb, mn_speimbv, mn_cambio, mn_scont4, mn_scont5, mn_scont6, mn_misura1," & _
        " mn_misura2, mn_misura3, mn_mmmisura1, mn_mmmisura2, mn_mmmisura3, mn_flstat," & _
        " mn_pmtaskid, mn_pmsalcon, mn_pmqtadis, mn_pmvaldis, mn_mmvalorev, mn_ubicaz," & _
        " mn_fase, mn_flkit, mn_ktriga, mn_rigaa, mn_codtpro, mn_flprznet, mn_ultagg, " & _
        " mn_scontp, mn_scontv"

      With dtrTmp
        strSQL = "INSERT INTO TTMOPERNP (" & strCampiTTMOPERNP & ")" & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTMoPernp & "," & _
          " mn_conto, mn_codagen, mn_codagen2, mn_tipobf, mn_codpaga, mn_codese," & _
          " mn_tmscont1, mn_tmscont2, mn_scopag, mn_valuta, mn_scorpo, mn_datapag," & _
          " mn_flspinc, mn_flbolli, mn_abi, mn_cab, mn_coddest, mn_coddest2," & _
          " mn_datord, mn_descr1, mn_tddatcons, mn_soloasa, mn_despaga, mn_destpbf," & _
          " mn_rating, mn_banc1, mn_banc2, mn_tipork, mn_anno, mn_serie, mn_numord," & _
          " mn_riga, mn_codart, mn_datcons, mn_magaz, mn_magaz2, mn_unmis, mn_descr," & _
          CDblSQL(dColli) & " , 0, " & CDblSQL(dResordine - dResiduo) & ", 0," & _
          " mn_flevas, 0, 0, 'C', mn_prezzo, mn_scont1, mn_scont2," & _
          " mn_scont3, mn_provv, mn_codiva, mn_preziva, mn_prezvalc, mn_commen," & _
          " mn_note, mn_controp, mn_stasino, mn_provv2, mn_tiporkor, mn_annoor," & _
          " mn_serieor, mn_numordor, mn_rigaor, mn_prelist, mn_codcfam, mn_commeca," & _
          " mn_subcommeca, " & CDblSQL(dValore) & ", mn_contocontr, mn_codcena, mn_desint," & _
          " mn_codvuo, mn_vprovv, mn_vprovv2, mn_ump, mn_confermato, 0," & _
          " 0, 0, 'C', 0, 0, 0, 0, 0, 0, mn_perqta, mn_pagato, mn_pagatov," & _
          " mn_abbuono, mn_abbuonov, mn_speacc, mn_speaccv, mn_speimb, mn_speimbv," & _
          " mn_cambio, mn_scont4, mn_scont5, mn_scont6, mn_misura1, mn_misura2," & _
          " mn_misura3, mn_mmmisura1, mn_mmmisura2, mn_mmmisura3, mn_flstat," & _
          " mn_pmtaskid, mn_pmsalcon, mn_pmqtadis, mn_pmvaldis, mn_mmvalorev," & _
          " mn_ubicaz, mn_fase, mn_flkit, mn_ktriga, " & (NTSCInt(!mn_rigaa) + 1) & "," & _
          " mn_codtpro, mn_flprznet, mn_ultagg, mn_scontp, mn_scontv"
        strSQL = strSQL & " FROM TTMOPERNP" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTMoPernp & _
          " AND mn_tipork = " & CStrSQL(!mn_tipork) & _
          " AND mn_anno = " & NTSCStr(!mn_anno) & _
          " AND mn_serie = " & CStrSQL(!mn_serie) & _
          " AND mn_numord = " & NTSCStr(!mn_numord) & _
          " AND mn_riga = " & NTSCStr(!mn_riga) & _
          " AND mn_rigaa = " & NTSCStr(!mn_rigaa)
      End With

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function NuovoInsertTTGegnnp(ByVal strDitta As String, ByVal lIITTGeGnnp As Integer, _
                                                        ByVal lIITTMoPernp As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO TTGEGNNP (codditt, instid, fd_anno, fd_serie, fd_numdoc, fd_datdoc," & _
        " fd_conto, fd_descr, fd_totdoc, fd_flsel, fd_soloasa, fd_tdflevas, fd_codpaga," & _
        " fd_despaga, fd_codtpbf, fd_destpbf, fd_datcons)" & _
        " SELECT " & CStrSQL(strDitta) & ", " & lIITTGeGnnp & ", mn_anno, mn_serie," & _
        " mn_numord, mn_datord, mn_conto, mn_descr1, Sum(mn_mmvalore), 'S'," & _
        " MIN(mn_soloasa), 'N', MIN(mn_codpaga), MIN(mn_despaga), MIN(mn_tipobf)," & _
        " MIN(mn_destpbf), MIN(mn_tddatcons)" & _
        " FROM TTMOPERNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND TTMOPERNP.instid = " & lIITTMoPernp & _
        " GROUP BY mn_conto, mn_anno, mn_serie, mn_numord , mn_datord, mn_descr1" & _
        " ORDER BY mn_conto, mn_datord"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function SettaOrdiniNonASaldo(ByVal strDitta As String, ByVal lIITTGeGnnp As Integer, _
                                                   ByVal strGndtTipork As String, ByVal lIITTMoPernp As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      ' prima di tutto metto a 'N' tutti i fd_tdflevas (possibile che sia stato rilanciato da modifica assegnazioni)
      strSQL = "UPDATE TTGEGNNP SET fd_tdflevas = 'N'" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTGeGnnp
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      ' adesso per ogni odine controlla che sia evaso totalmente ...
      strSQL = "SELECT * FROM TTGEGNNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTGeGnnp & _
        " ORDER BY fd_anno, fd_serie, fd_numdoc"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTGEGNNP")

      ' per ogni ordine coivolto, faccio la stessa cosa che farebbe veboll in aggmovord su TESTORD
      ' considerando però come se fosse già evaso con quelle righe di note di prelievo che diventeranno bolle
      For i = 0 To dsTmp.Tables("TTGEGNNP").Rows.Count - 1
        With dsTmp.Tables("TTGEGNNP").Rows(i)
          ' faccio una join per settare in griglia fd_tdflevas a S per  gli ordini che sarebbero evasi a saldo
          strSQL = "UPDATE TTGEGNNP" & _
            " SET fd_tdflevas = 'S'" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND fd_anno = " & NTSCStr(!fd_anno) & _
            " AND fd_serie = '" & NTSCStr(!fd_serie) & "'" & _
            " AND fd_numdoc = " & NTSCStr(!fd_numdoc) & _
            " AND instid = " & lIITTGeGnnp & _
            " AND NOT EXISTS (SELECT mn_riga from TTMOPERNP" & _
                            " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND '" & strGndtTipork & "' = mn_tipork  and fd_anno = mn_anno " & _
                            " and fd_serie = mn_serie  and fd_numdoc = mn_numord AND instid = " & lIITTMoPernp & " " & _
                            " GROUP BY mn_tipork, mn_anno, mn_serie, mn_numord, mn_riga, mn_tcindtagl " & _
                            " HAVING Max(mn_mmflevas) ='C')"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CalcolaValoreMerce(ByVal strDitta As String, ByVal lIITTGeGnnp As Integer, _
                                                   ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------------------------------------------------------------------------------
      '--- Calcola il totale valore merce evadibile
      '----------------------------------------------------------------------------------------
      strSQL = "SELECT Sum(fd_totdoc) As Valore FROM TTGEGNNP" & _
        " WHERE instid = " & lIITTGeGnnp

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTGEGNNP")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function EliminaOrdiniNonasaldo(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                                     ByVal lIITTGeGnnp As Integer, ByVal bCancellaNonASaldoNew As Boolean) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE TTMOPERNP FROM TTMOPERNP INNER JOIN TTGEGNNP ON TTMOPERNP.codditt = TTGEGNNP.codditt AND TTMOPERNP.mn_anno = TTGEGNNP.fd_anno AND TTMOPERNP.mn_serie = TTGEGNNP.fd_serie AND TTMOPERNP.mn_numord = TTGEGNNP.fd_numdoc" & _
        " WHERE TTGEGNNP.codditt = " & CStrSQL(strDitta) & _
        " AND fd_tdflevas = 'N'" & _
        " AND TTMOPERNP.instid = " & lIITTMoPernp & _
        " AND TTGEGNNP.instid = " & lIITTGeGnnp
      If bCancellaNonASaldoNew = False Then strSQL = strSQL & " AND fd_soloasa = 'S'"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM TTGEGNNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND fd_tdflevas = 'N'" & _
        " AND TTGEGNNP.instid = " & lIITTGeGnnp
      If bCancellaNonASaldoNew = False Then strSQL = strSQL & " AND fd_soloasa = 'S'"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function EliminaOrdiniOltremax(ByVal strDitta As String, ByVal nSeicMaxnumord As Integer, _
                                                    ByVal lIITTMoPernp As Integer, ByVal lIITTGeGnnp As Integer, _
                                                    ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim nCont As Integer
    Dim i As Integer
    Try
      nCont = 0
      'Do Until ds.Recordset.EOF
      For i = 0 To ds.Tables("TTGEGNNP").Rows.Count - 1
        With ds.Tables("TTGEGNNP").Rows(i)
          ' lo elimina solo se c'e qualcosa (totdoc <> 0
          If NTSCDec(!fd_totdoc) <> 0 Then
            nCont = nCont + 1
            If nCont > nSeicMaxnumord Then
              ' canella in ttmopernp
              strSQL = "DELETE from TTMOPERNP" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND TTMOPERNP.mn_anno = " & NTSCDec(!fd_anno) & " AND TTMOPERNP.mn_serie = '" & NTSCDec(!fd_serie) & "' AND TTMOPERNP.mn_numord = " & NTSCDec(!fd_numdoc) & " AND instid = " & lIITTMoPernp
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
              ' cancella record di gegnnp
              strSQL = "DELETE from TTGEGNNP" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND TTGEGNNP.fd_anno = " & NTSCDec(!fd_anno) & " AND TTGEGNNP.fd_serie = '" & NTSCDec(!fd_serie) & "' AND TTGEGNNP.fd_numdoc = " & NTSCDec(!fd_numdoc) & " AND instid = " & lIITTGeGnnp
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            End If
          End If
        End With
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function EliminaOrdiniAvalorezero(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                                       ByVal lIITTGeGnnp As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE TTMOPERNP FROM TTMOPERNP INNER JOIN TTGEGNNP ON TTMOPERNP.codditt = TTGEGNNP.codditt AND TTMOPERNP.mn_anno = TTGEGNNP.fd_anno AND TTMOPERNP.mn_serie = TTGEGNNP.fd_serie AND TTMOPERNP.mn_numord = TTGEGNNP.fd_numdoc" & _
        " WHERE TTGEGNNP.codditt = " & CStrSQL(strDitta) & _
        " AND fd_totdoc = 0 AND TTMOPERNP.instid = " & lIITTMoPernp & " AND TTGEGNNP.instid = " & lIITTGeGnnp
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE from TTGEGNNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND fd_totdoc = 0 AND instid = " & lIITTGeGnnp
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function FileRielabora(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                                     ByVal lIITTGeGnnp As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      ' se rielaborazione, cancella il contenuto della tabella associata alla griglia...
      strSQL = "DELETE FROM TTGEGNNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTGeGnnp
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      ' adesso visualizza le righe di testord relative ai record di movord evadibili, nella griglia ...
      strSQL = "INSERT INTO TTGEGNNP (codditt, instid, fd_anno, fd_serie, fd_numdoc, fd_datdoc, fd_conto, fd_descr, fd_totdoc, fd_flsel, " & _
                "fd_soloasa, fd_tdflevas, fd_codpaga, fd_despaga, fd_codtpbf, fd_destpbf, fd_datcons ) "
      strSQL = strSQL & " SELECT " & CStrSQL(strDitta) & ", " & lIITTGeGnnp & ", mn_anno, mn_serie, mn_numord, mn_datord, mn_conto, mn_descr1, " & _
                "sum(mn_mmvalore), 'S', " & _
                "MIN(mn_soloasa), 'N', MIN(mn_codpaga), MIN(mn_despaga), MIN(mn_tipobf), MIN(mn_destpbf), MIN(mn_tddatcons)" & _
                " FROM TTMOPERNP" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTMoPernp & _
                " GROUP BY mn_conto, mn_anno, mn_serie, mn_numord , mn_datord, mn_descr1" & _
                " ORDER BY mn_conto, mn_datord "
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function DeleteAllRighe(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                                   ByVal lIITTGeGnnp As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM TTMOPERNP" & _
         " WHERE codditt = " & CStrSQL(strDitta) & _
         " AND instid = " & lIITTMoPernp
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM TTGEGNNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTGeGnnp
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function DeleteRighe(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                          ByVal lIITTGeGnnp As Integer, ByVal dtrTmp As DataRow) As Boolean
    Dim strSQL As String = ""
    Try
      With dtrTmp
        strSQL = "DELETE FROM TTMOPERNP" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTMoPernp & _
          " AND mn_anno = " & NTSCStr(!fd_anno) & _
          " AND mn_serie = " & CStrSQL(!fd_serie) & _
          " AND mn_numord = " & NTSCStr(!fd_numdoc)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        strSQL = "DELETE FROM TTGEGNNP" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTGeGnnp & _
          " AND fd_anno = " & NTSCStr(!fd_anno) & _
          " AND fd_serie = " & CStrSQL(!fd_serie) & _
          " AND fd_numdoc = " & NTSCStr(!fd_numdoc)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End With

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function DeleteRigheInSalva(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                                 ByVal lIITTGeGnnp As Integer, ByVal dtrTmp As DataRow) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM TTMOPERNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTMoPernp & _
        " AND mn_anno = " & NTSCStr(dtrTmp("fd_anno", DataRowVersion.Original)) & _
        " AND mn_serie = " & CStrSQL(dtrTmp("fd_serie", DataRowVersion.Original)) & _
        " AND mn_numord = " & NTSCStr(dtrTmp("fd_numdoc", DataRowVersion.Original))
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM TTGEGNNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTGeGnnp & _
        " AND fd_anno = " & NTSCStr(dtrTmp("fd_anno", DataRowVersion.Original)) & _
        " AND fd_serie = " & CStrSQL(dtrTmp("fd_serie", DataRowVersion.Original)) & _
        " AND fd_numdoc = " & NTSCStr(dtrTmp("fd_numdoc", DataRowVersion.Original))
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function FileModificaSelect(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                 ByVal lIITTMoPernp As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT sum(CASE WHEN mn_quant - mn_quaeva > 0 THEN round((mn_mmquant * mn_valore / (mn_quant - mn_quaeva)),2) ELSE 0 END) as expr1" & _
        " from TTMOPERNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND TTMOPERNP.mn_anno = " & NTSCStr(dtrTmp!fd_anno) & " AND TTMOPERNP.mn_serie = '" & NTSCStr(dtrTmp!fd_serie) & "' AND TTMOPERNP.mn_numord = " & NTSCStr(dtrTmp!fd_numdoc) & " AND instid = " & lIITTMoPernp & _
        " AND mn_tcindtagl = 0"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTMOPERNP")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function FileModificaSelectTC(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                   ByVal lIITTMoPernp As Integer, ByVal k As Integer, _
                                                   ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT CASE WHEN Sum(mn_quant) - Sum(mn_quaeva) > 0 THEN round((Sum(mn_mmquant) * Sum(mn_valore) / (Sum(mn_quant) - Sum(mn_quaeva))),2) ELSE 0 END as expr1" & _
        " from TTMOPERNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND TTMOPERNP.mn_anno = " & NTSCStr(dtrTmp!fd_anno) & " AND TTMOPERNP.mn_serie = '" & NTSCStr(dtrTmp!fd_serie) & "' AND TTMOPERNP.mn_numord = " & NTSCStr(dtrTmp!fd_numdoc) & " AND instid = " & lIITTMoPernp & _
        " AND mn_tcindtagl = " & k & _
        " GROUP BY TTMOPERNP.codditt, instid, mn_anno, mn_serie, mn_numord, mn_riga"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTMOPERNP")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function FileModificaUpdate(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                 ByVal lIITTGeGnnp As Integer, ByVal dTotdoc As Decimal) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE TTGEGNNP SET fd_totdoc = " & CDblSQL(dTotdoc) & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND TTGEGNNP.fd_anno = " & NTSCStr(dtrTmp!fd_anno) & " AND TTGEGNNP.fd_serie = '" & NTSCStr(dtrTmp!fd_serie) & "' AND TTGEGNNP.fd_numdoc = " & NTSCStr(dtrTmp!fd_numdoc) & " AND instid = " & lIITTGeGnnp

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataGnda(ByVal strDitta As String, ByVal lIITTDispNet As Integer, _
                                          ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TTDISPNET.*, co_descr1 as xx_commeca, alo_descr as xx_lotto, alo_lottox as xx_lottox" & _
        " FROM (TTDISPNET LEFT JOIN commess ON TTDISPNET.codditt = commess.codditt AND TTDISPNET.dn_commeca = commess.co_comme)" & _
        " LEFT JOIN analotti ON TTDISPNET.codditt = analotti.codditt AND TTDISPNET.dn_codart = analotti.alo_codart AND TTDISPNET.dn_lotto = analotti.alo_lotto" & _
        " WHERE TTDISPNET.codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTDispNet & _
        " AND dn_dispnet <> 0" & _
        " ORDER BY dn_codart, dn_tcindtagl"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTDISPNET")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GndaModifica(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                           ByVal strCodart As String, ByVal strCommeca As String, _
                                           ByVal strLotto As String, ByVal strFase As String, _
                                           ByRef ds As DataSet) As Boolean
    Try
      Return GndaModifica(strDitta, lIITTMoPernp, strCodart, strCommeca, strLotto, strFase, 0, ds)
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GndaModifica(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                           ByVal strCodart As String, ByVal strCommeca As String, _
                                           ByVal strLotto As String, ByVal strFase As String, _
                                           ByVal nIndiceTaglia As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITTMoPernp, strCodart, strCommeca, strLotto, strFase, nIndiceTaglia, ds, "RISPETTARE l'ORDINE DI COME SONO INDICATI I PARAMETRI NELLA FIRMA DELLA FUNZIONE"})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(7), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      strSQL = " SELECT Sum(mn_mmquant) AS Expr1 " & _
               " FROM TTMOPERNP " & _
               " WHERE mn_codart = '" & strCodart & "'" & _
               " AND instid = " & lIITTMoPernp & _
               " and mn_commen = " & strCommeca & _
               " and mn_lotto =  " & strLotto & _
               " and mn_fase = " & strFase & _
               " and mn_tcindtagl = " & nIndiceTaglia

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTMOPERNP")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataGndt(ByVal strDitta As String, ByVal strGndtCodart As String, _
                                          ByVal strGndtTipork As String, ByVal nGndtAnno As Integer, _
                                          ByVal strGndtSerie As String, ByVal lGndtNumdoc As Integer, _
                                          ByVal lInstIdTTmopernp As Integer, ByVal lGndtCommeca As Integer, _
                                          ByVal lGndtLotto As Integer, ByVal nGndtFase As Integer, _
                                          ByVal nGndtTaglia As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      If strGndtCodart = "" Then
        strSQL = "SELECT TTMOPERNP.*, co_descr1 as xx_commeca, alo_descr as xx_lotto, alo_lottox as xx_lottox" & _
                 " FROM (TTMOPERNP" & _
                 " LEFT JOIN commess ON TTMOPERNP.codditt = commess.codditt AND TTMOPERNP.mn_commeca = commess.co_comme)" & _
                 " LEFT JOIN analotti ON TTMOPERNP.codditt = analotti.codditt AND TTMOPERNP.mn_codart = analotti.alo_codart AND TTMOPERNP.mn_lotto = analotti.alo_lotto" & _
                 " WHERE mn_tipork = '" & strGndtTipork & "' AND mn_anno = " & nGndtAnno & " AND mn_serie = '" & strGndtSerie & _
                 "' AND mn_numord = " & lGndtNumdoc & " AND instid = " & lInstIdTTmopernp & " ORDER BY mn_riga"
      Else
        strSQL = "SELECT TTMOPERNP.*, co_descr1 as xx_commeca, alo_descr as xx_lotto, alo_lottox as xx_lottox" & _
                 " FROM (TTMOPERNP" & _
                 " LEFT JOIN commess ON TTMOPERNP.codditt = commess.codditt AND TTMOPERNP.mn_commeca = commess.co_comme)" & _
                 " LEFT JOIN analotti ON TTMOPERNP.codditt = analotti.codditt AND TTMOPERNP.mn_codart = analotti.alo_codart AND TTMOPERNP.mn_lotto = analotti.alo_lotto" & _
          " WHERE TTMOPERNP.codditt = " & CStrSQL(strDitta) & _
          " AND mn_codart = " & CStrSQL(strGndtCodart) & _
          " AND instid = " & lInstIdTTmopernp
        If lGndtCommeca <> 0 Then
          strSQL = strSQL & " AND mn_commen = " & lGndtCommeca ' solo se è significativa la commessa...
        End If
        strSQL = strSQL & " AND (mn_lotto = " & lGndtLotto & " OR mn_lotto = 0)" & _
          " AND mn_fase = " & nGndtFase & _
          " AND mn_tcindtagl = " & nGndtTaglia & _
          " ORDER BY mn_conto, mn_riga"
      End If

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTMOPERNP")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetSumTTMopernp(ByVal strDitta As String, ByVal strCodart As String, _
                                              ByVal lIITTMoPernp As Integer, ByVal stredCommeca As String, _
                                              ByVal stredLotto As String, ByVal stredFase As String, _
                                              ByVal stredTaglia As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Sum(mn_mmquant) AS Expr1 FROM TTMOPERNP WHERE mn_codart = '" & strCodart & "'" & _
        " AND instid = " & lIITTMoPernp & _
        " and mn_commen = " & stredCommeca & _
        " and mn_lotto = " & stredLotto & _
        " and mn_fase = " & stredFase & _
        " and mn_tcindtagl = " & stredTaglia

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTMOPERNP")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetCommess(ByVal strDitta As String, ByVal strGndtTipork As String, _
                                         ByVal strFd_anno As String, ByVal strFd_serie As String, _
                                         ByVal strFd_numdoc As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT td_commeca, co_descr1 " & _
        " FROM testord" & _
        " LEFT JOIN commess ON testord.codditt = commess.codditt AND testord.td_commeca = commess.co_comme" & _
        " WHERE testord.codditt = " & CStrSQL(strDitta) & _
        " AND td_tipork = " & CStrSQL(strGndtTipork) & _
        " AND td_anno = " & strFd_anno & _
        " AND td_serie = " & CStrSQL(strFd_serie) & _
        " AND td_numord = " & strFd_numdoc

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTORD")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetRighe(ByVal strDitta As String, ByVal strGndtTipork As String, _
                                         ByVal strFd_anno As String, ByVal strFd_serie As String, _
                                         ByVal strFd_numdoc As String, ByVal lIITTMoPernp As Integer, _
                                         ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Count(*) As Records FROM TTMOPERNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTMoPernp & _
        " AND mn_tipork = " & CStrSQL(strGndtTipork) & _
        " AND mn_anno = " & strFd_anno & _
        " AND mn_serie = " & CStrSQL(strFd_serie) & _
        " AND mn_numord = " & strFd_numdoc

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTMOPERNP")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetRighemancanti(ByVal strDitta As String, ByVal strGndtTipork As String, _
                                       ByVal strFd_anno As String, ByVal strFd_serie As String, _
                                       ByVal strFd_numdoc As String, ByVal lIITTMoPernp As Integer, _
                                       ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Count(*) As Records FROM TTMOPERNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTMoPernp & _
        " AND mn_tipork = " & CStrSQL(strGndtTipork) & _
        " AND mn_anno = " & strFd_anno & _
        " AND mn_serie = " & CStrSQL(strFd_serie) & _
        " AND mn_numord = " & strFd_numdoc & _
        " AND mn_mmflevas = 'C'"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTMOPERNP")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDest(ByVal strDitta As String, ByVal strGndtTipork As String, _
                                       ByVal strFd_anno As String, ByVal strFd_serie As String, _
                                       ByVal strFd_numdoc As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT td_coddest, dd_nomdest " & _
        " FROM testord" & _
        " LEFT JOIN destdiv ON testord.codditt = destdiv.codditt AND testord.td_conto = destdiv.dd_conto AND testord.td_coddest = destdiv.dd_coddest" & _
        " WHERE testord.codditt = " & CStrSQL(strDitta) & _
        " AND td_tipork = " & CStrSQL(strGndtTipork) & _
        " AND td_anno = " & strFd_anno & _
        " AND td_serie = " & CStrSQL(strFd_serie) & _
        " AND td_numord = " & strFd_numdoc

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTORD")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetNote(ByVal strDitta As String, ByVal bDtacUnaNotaPerImpegno As Boolean, _
                                      ByVal nSeicTipo As Integer, ByVal lIITTMoPernp As Integer, _
                                      ByVal bRiportaNoteDaImpegno As Boolean, ByVal dtrImpCli As DataRow, _
                                      ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      With dtrImpCli
        If bDtacUnaNotaPerImpegno = True Then
          strSQL = "SELECT td_note as xx_note FROM testord" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND td_anno = " & NTSCStr(!mn_anno) & _
            " AND td_serie = " & CStrSQL(!mn_serie) & _
            " AND td_numord = " & NTSCStr(!mn_numord)
          Select Case nSeicTipo
            Case 1 : strSQL = strSQL & " AND td_tipork = 'R'"
            Case 2 : strSQL = strSQL & " AND td_tipork = 'X'"
            Case 3 : strSQL = strSQL & " AND td_tipork = 'Y'"
          End Select

          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        Else
          If bRiportaNoteDaImpegno = True Then
            strSQL = "SELECT TOP 1 td_note as xx_note FROM TTMOPERNP" & _
              " INNER JOIN testord ON testord.codditt = TTMOPERNP.codditt AND mn_tipork = td_tipork AND mn_anno = td_anno AND mn_serie = td_serie AND mn_numord = td_numord " & _
              " WHERE TTMOPERNP.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTMoPernp & _
              " AND mn_conto = " & NTSCStr(!mn_conto) & _
              " AND mn_codagen = " & NTSCStr(!mn_codagen) & _
              " AND mn_codagen2 = " & NTSCStr(!mn_codagen2) & _
              " AND mn_tipobf = " & NTSCStr(!mn_tipobf) & _
              " AND mn_codpaga = " & NTSCStr(!mn_codpaga) & _
              " AND mn_codese = " & NTSCStr(!mn_codese) & _
              " AND mn_tmscont1 = " & CDblSQL(NTSCDec(!mn_tmscont1)) & _
              " AND mn_tmscont2 = " & CDblSQL(NTSCDec(!mn_tmscont2)) & _
              " AND mn_scopag = " & CDblSQL(NTSCDec(!mn_scopag)) & _
              " AND mn_valuta = " & NTSCStr(!mn_valuta) & _
              " AND mn_scorpo = " & CStrSQL(!mn_scorpo) & _
              " AND mn_datapag " & NTSCStr(IIf(NTSCStr(!mn_datapag) = "", " IS ", " = ")) & CDataSQL(NTSCStr(!mn_datapag)) & _
              " AND mn_flspinc = " & CStrSQL(!mn_flspinc) & _
              " AND mn_flbolli = " & CStrSQL(!mn_flbolli) & _
              " AND mn_coddest = " & NTSCStr(!mn_coddest) & _
              " AND mn_coddest2 = " & NTSCStr(!mn_coddest2) & _
              " AND mn_magaz2 = " & NTSCStr(!mn_magaz2) & _
              " ORDER BY td_datord, td_tipork, td_anno, td_serie, td_numord "

            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          End If
        End If
      End With

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function DeleteTTMopernp(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                              ByVal dtrTmp As DataRow) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM TTMOPERNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTMoPernp & _
        " AND mn_anno = " & NTSCStr(dtrTmp!fd_anno) & _
        " AND mn_serie = " & CStrSQL(dtrTmp!fd_serie) & _
        " AND mn_numord = " & NTSCStr(dtrTmp!fd_numdoc)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDatiniDatfin(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
                                      ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      With dtrTmp
        strSQL = "SELECT movord.mo_datini, movord.mo_datfin" & _
          " FROM TTMOPERNP INNER JOIN movord ON TTMOPERNP.codditt = movord.codditt AND TTMOPERNP.mn_tipork = movord.mo_tipork AND TTMOPERNP.mn_anno = movord.mo_anno AND TTMOPERNP.mn_serie = movord.mo_serie AND TTMOPERNP.mn_numord = movord.mo_numord AND TTMOPERNP.mn_riga = movord.mo_riga" & _
          " WHERE TTMOPERNP.codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTMoPernp & _
          " AND mn_anno = " & NTSCStr(!mn_anno) & _
          " AND mn_serie = " & CStrSQL(NTSCStr(!mn_serie)) & _
          " AND mn_numord = " & NTSCStr(!mn_numord) & _
          " AND mn_riga = " & NTSCStr(!mn_riga) & _
          " AND mn_rigaa = " & NTSCStr(!mn_rigaa)
      End With

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTMOPERNP")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function SettaPiede(ByVal strDitta As String, ByVal bDtacUnaNotaPerImpegno As Boolean, _
                                         ByVal nSeicTipo As Integer, ByVal lIITTMoPernp As Integer, _
                                         ByVal dtrImpCli As DataRow, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      With dtrImpCli
        If bDtacUnaNotaPerImpegno = True Then
          strSQL = "SELECT td_speacc, td_speimb, td_speaccv, td_speimbv FROM testord" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND td_anno = " & NTSCStr(!mn_anno) & _
            " AND td_serie = " & CStrSQL(!mn_serie) & _
            " AND td_numord = " & NTSCStr(!mn_numord)
          Select Case nSeicTipo
            Case 1 : strSQL = strSQL & " AND td_tipork = 'R'"
            Case 2 : strSQL = strSQL & " AND td_tipork = 'X'"
            Case 3 : strSQL = strSQL & " AND td_tipork = 'Y'"
          End Select

          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        Else
          strSQL = "SELECT TOP 1 mn_speacc, mn_speimb," & _
              " mn_speaccv, mn_speimbv FROM ttmopernp" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTMoPernp & _
              " AND mn_conto = " & NTSCStr(!mn_conto) & _
              " AND mn_codagen = " & NTSCStr(!mn_codagen) & _
              " AND mn_codagen2 = " & NTSCStr(!mn_codagen2) & _
              " AND mn_tipobf = " & NTSCStr(!mn_tipobf) & _
              " AND mn_codpaga = " & NTSCStr(!mn_codpaga) & _
              " AND mn_codese = " & NTSCStr(!mn_codese) & _
              " AND mn_tmscont1 = " & CDblSQL(NTSCDec(!mn_tmscont1)) & _
              " AND mn_tmscont2 = " & CDblSQL(NTSCDec(!mn_tmscont2)) & _
              " AND mn_scopag = " & CDblSQL(NTSCDec(!mn_scopag)) & _
              " AND mn_valuta = " & NTSCStr(!mn_valuta) & _
              " AND mn_scorpo = " & CStrSQL(!mn_scorpo)
          If NTSCStr(!mn_datapag) = "" Then
            strSQL = strSQL & " AND mn_datapag IS NULL "
          Else
            strSQL = strSQL & " AND mn_datapag = " & CDataSQL(NTSCStr(!mn_datapag))
          End If
          strSQL = strSQL & " AND mn_flspinc = " & CStrSQL(!mn_flspinc) & _
              " AND mn_flbolli = " & CStrSQL(!mn_flbolli) & _
              " AND mn_coddest = " & NTSCStr(!mn_coddest) & _
              " AND mn_coddest2 = " & NTSCStr(!mn_coddest2) & _
              " AND mn_magaz2 = " & NTSCStr(!mn_magaz2)
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
      End With

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CancellaRigheElaborate(ByVal dttRighe As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dtrRow As DataRow
    Try
      For Each dtrRow In dttRighe.Rows
        strSQL = "DELETE FROM ttmopernp WHERE codditt = " & CStrSQL(dtrRow!codditt) & _
               " AND instid = " & NTSCInt(dtrRow!instid) & _
               " AND mn_tipork = " & CStrSQL(dtrRow!mn_tipork) & _
               " AND mn_anno = " & NTSCInt(dtrRow!mn_anno) & _
               " AND mn_serie = " & CStrSQL(dtrRow!mn_serie) & _
               " AND mn_numord = " & NTSCInt(dtrRow!mn_numord) & _
               " AND mn_riga = " & NTSCInt(dtrRow!mn_riga) & _
               " AND mn_rigaa = " & NTSCInt(dtrRow!mn_rigaa) & _
               " AND mn_tcindtagl = " & NTSCInt(dtrRow!mn_tcindtagl)

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CancellaRecordTTMOPERNP(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
    ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumord As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM TTMOPERNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTMoPernp & _
        " AND mn_anno = " & nAnno & _
        " AND mn_serie = " & CStrSQL(strSerie) & _
        " AND mn_numord = " & lNumord
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ControllaModuliPickingAnazmod(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM anazmod " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND am_modulo IN (37, 103) " & _
               " AND am_abilit = 'S'"

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EscludiOrdineDaElaborazione(ByVal strDitta As String, ByVal strTipoRk As String, ByVal lAnno As Integer, _
                                                          ByVal strSerie As String, ByVal lNum As Integer, _
                                                          ByVal lIITTMoPernp As Integer, ByVal lIITTGeGnnp As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM TTMOPERNP " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mn_tipork = " & CStrSQL(strTipoRk) & _
               " AND mn_anno = " & lAnno & _
               " AND mn_serie = " & CStrSQL(strSerie) & _
               " AND mn_numord = " & lNum & _
               " AND instid = " & lIITTMoPernp

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM TTGEGNNP " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND fd_anno = " & lAnno & _
               " AND fd_serie = " & CStrSQL(strSerie) & _
               " AND fd_numdoc = " & lNum & _
               " AND instid = " & lIITTGeGnnp

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaQtaLottoSuTTMOPERNP(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
    ByVal lLotto As Integer, ByVal dtrT As DataRow, ByRef ds As DataSet) As Boolean
    Dim nRighe As Integer = 0
    Dim dQuant As Decimal = 0
    Dim strSQL As String = ""
    Dim strChiave As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Esce se il lotto di testata è zero
      '--------------------------------------------------------------------------------------------------------------
      If lLotto = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Esce se l'articolo non è gestito a lotti
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT ar_geslotti FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(NTSCStr(dtrT!mn_codart)) & _
        " AND UPPER(ar_geslotti) = 'S'"
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      With dtrT
        nRighe = ds.Tables("TTMOPERNP").Select("mn_tipork = " & CStrSQL(NTSCStr(!mn_tipork)) & _
                                          " AND mn_anno = " & NTSCInt(!mn_anno) & _
                                          " AND mn_serie = " & CStrSQL(NTSCStr(!mn_serie)) & _
                                          " AND mn_numord = " & NTSCInt(!mn_numord) & _
                                          " AND mn_riga = " & NTSCInt(!mn_riga) & _
                                          " AND mn_tcindtagl = " & NTSCInt(!mn_tcindtagl), "").Length
        strChiave = " codditt = " & CStrSQL(strDitta) & _
                    " AND instid = " & lIITTMoPernp & _
                    " AND mn_tipork = " & CStrSQL(NTSCStr(!mn_tipork)) & _
                    " AND mn_anno = " & NTSCInt(!mn_anno) & _
                    " AND mn_serie = " & CStrSQL(NTSCStr(!mn_serie)) & _
                    " AND mn_numord = " & NTSCInt(!mn_numord) & _
                    " AND mn_riga = " & NTSCInt(!mn_riga) & _
                    " AND mn_tcindtagl = " & NTSCInt(!mn_tcindtagl)
      End With
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCDec(dtrT!mn_mmquant) <= 0) And (NTSCInt(dtrT!mn_lotto) > 0) Then
        '------------------------------------------------------------------------------------------------------------
        dQuant = RitornaQuantitaTTMOPERNP(strChiave, lLotto)
        '------------------------------------------------------------------------------------------------------------
        If nRighe > 0 Then
          strSQL = "DELETE TTMOPERNP" & _
                   " WHERE " & strChiave & _
                   " AND mn_rigaa <> " & NTSCInt(dtrT!mn_rigaa) & _
                   " AND mn_lotto = 0"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
        strSQL = "UPDATE TTMOPERNP" & _
                 " SET mn_mmquant = 0," & _
                 IIf(dQuant > 0, " mn_quant = " & CDblSQL(dQuant) & ",", "").ToString & _
                 " mn_colli = -1," & _
                 " mn_mmcolli = 0," & _
                 " mn_mmcoldisimp = 0," & _
                 " mn_mmqtadisimp = 0," & _
                 " mn_mmflevas = 'C'," & _
                 " mn_lotto = 0" & _
                 " WHERE " & strChiave & _
                 " AND mn_rigaa = " & NTSCInt(dtrT!mn_rigaa)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        Return True
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCDec(dtrT!mn_mmquant) >= NTSCDec(dtrT!mn_quant)) And (nRighe = 1) Then
        If NTSCInt(dtrT!mn_lotto) > 0 Then
          Return False
        Else
          strSQL = "UPDATE TTMOPERNP" & _
                   " SET mn_mmquant = mn_quant," & _
                   " mn_colli = -1," & _
                   " mn_mmcolli = 0," & _
                   " mn_mmcoldisimp = 0," & _
                   " mn_mmqtadisimp = mn_quant," & _
                   " mn_mmflevas = 'C'," & _
                   " mn_lotto = " & lLotto & _
                   " WHERE " & strChiave & _
                   " AND mn_rigaa = " & NTSCInt(dtrT!mn_rigaa)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCDec(dtrT!mn_mmquant) >= NTSCDec(dtrT!mn_quant)) And (NTSCInt(dtrT!mn_lotto) > 0) And (nRighe > 1) Then
        '------------------------------------------------------------------------------------------------------------
        dQuant = RitornaQuantitaTTMOPERNP(strChiave, lLotto)
        '------------------------------------------------------------------------------------------------------------
        If NTSCDec(dtrT!mn_mmquant) >= dQuant Then
          strSQL = "UPDATE TTMOPERNP" & _
                   " SET mn_quant = " & CDblSQL(dQuant) & "," & _
                   " mn_mmquant = " & CDblSQL(dQuant) & "," & _
                   " mn_colli = -1," & _
                   " mn_mmcolli = 0," & _
                   " mn_mmcoldisimp = 0," & _
                   " mn_mmqtadisimp = " & CDblSQL(dQuant) & "," & _
                   " mn_mmflevas = 'S'," & _
                   " mn_lotto = " & lLotto & _
                   " WHERE " & strChiave & _
                   " AND mn_rigaa = " & NTSCInt(dtrT!mn_rigaa)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          strSQL = "DELETE TTMOPERNP" & _
                   " WHERE " & strChiave & _
                   " AND mn_rigaa <> " & NTSCInt(dtrT!mn_rigaa) & _
                   " AND mn_lotto = 0"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        Else
          If NTSCDec(dtrT!mn_mmquant) >= NTSCDec(dtrT!mn_quant) Then
            strSQL = "UPDATE TTMOPERNP" & _
                     " SET mn_quant = " & CDblSQL(NTSCDec(dtrT!mn_mmquant)) & "," & _
                     " mn_colli = -1," & _
                     " mn_mmcolli = 0," & _
                     " mn_mmcoldisimp = 0," & _
                     " mn_mmqtadisimp = " & CDblSQL(NTSCDec(dtrT!mn_mmquant)) & "," & _
                     " mn_mmflevas = 'S'," & _
                     " mn_lotto = " & lLotto & _
                     " WHERE " & strChiave & _
                     " AND mn_rigaa = " & NTSCInt(dtrT!mn_rigaa)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            strSQL = "UPDATE TTMOPERNP" & _
                     " SET mn_quant = " & CDblSQL(dQuant - NTSCDec(dtrT!mn_mmquant)) & "," & _
                     " mn_colli = -1," & _
                     " mn_mmcolli = 0," & _
                     " mn_mmcoldisimp = 0," & _
                     " mn_mmqtadisimp = " & CDblSQL(dQuant - NTSCDec(dtrT!mn_mmquant)) & "," & _
                     " mn_mmflevas = 'C'," & _
                     " mn_lotto = 0" & _
                     " WHERE " & strChiave & _
                     " AND mn_rigaa <> " & NTSCInt(dtrT!mn_rigaa)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          End If
        End If
        Return True
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il lotto di riga è 0, esiste una sola riga e la quantità da evadere è inferiore alla quantità ordinata
      '--- crea una nuova riga con il lotto di testata
      '--------------------------------------------------------------------------------------------------------------
      With dtrT
        If NTSCInt(dtrT!mn_lotto) = 0 Then
          If nRighe = 1 Then
            If NTSCDec(!mn_mmquant) < NTSCDec(!mn_quant) Then
              strSQL = "UPDATE TTMOPERNP" & _
                       " SET mn_quant = " & CDblSQL(NTSCDec(!mn_mmquant)) & "," & _
                       " mn_colli = -1," & _
                       " mn_mmcolli = 0," & _
                       " mn_mmcoldisimp = 0," & _
                       " mn_mmqtadisimp = " & CDblSQL(NTSCDec(!mn_mmquant)) & "," & _
                       " mn_mmflevas = 'C'," & _
                       " mn_lotto = " & lLotto & _
                       " WHERE " & strChiave & _
                       " AND mn_rigaa = " & NTSCInt(!mn_rigaa)
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

              !mn_rigaa = RitornaRigaaTTMOPERNP(strChiave)
              !mn_lotto = 0
              !mn_quant = NTSCDec(dtrT!mn_quant) - NTSCDec(dtrT!mn_mmquant)
              !mn_colli = -1
              !mn_mmcolli = 0
              !mn_mmcoldisimp = 0
              !mn_mmquant = 0
              !mn_mmqtadisimp = 0
              !mn_mmflevas = "C"

              .AcceptChanges()
              strSQL = "INSERT INTO TTMOPERNP " & GetQueryInsertField(ds.Tables("TTMOPERNP"), "mn_") & _
                       " VALUES " & GetQueryInsertValue(ds.Tables("TTMOPERNP"), dtrT, "mn_")
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            Else 'If NTSCDec(!mn_mmquant) < NTSCDec(!mn_quant) Then
              strSQL = "UPDATE TTMOPERNP" & _
                       " SET mn_quant = " & CDblSQL(NTSCDec(!mn_quant)) & "," & _
                       " mn_colli = -1," & _
                       " mn_mmcolli = 0," & _
                       " mn_mmcoldisimp = 0," & _
                       " mn_mmqtadisimp = " & CDblSQL(NTSCDec(!mn_quant)) & "," & _
                       " mn_mmflevas = 'S'," & _
                       " mn_lotto = " & lLotto & _
                       " WHERE " & strChiave & _
                       " AND mn_rigaa = " & NTSCInt(!mn_rigaa)
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            End If
          Else 'If nRighe = 1 Then
            '--------------------------------------------------------------------------------------------------------
            '--- Esiste riga con lotto <> 0
            '--------------------------------------------------------------------------------------------------------
            Dim dQuantTmp As Decimal = NTSCDec(!mn_mmquant)
            If (NTSCDec(!mn_mmquant) < NTSCDec(!mn_quant)) And (NTSCDec(!mn_mmquant) > 0) Then
              strSQL = "UPDATE TTMOPERNP" & _
                       " SET mn_quant = " & CDblSQL(NTSCDec(!mn_quant) - NTSCDec(!mn_mmquant)) & "," & _
                       " mn_colli = -1," & _
                       " mn_mmcolli = 0," & _
                       " mn_mmcoldisimp = 0," & _
                       " mn_mmquant = 0," & _
                       " mn_mmqtadisimp = 0," & _
                       " mn_mmflevas = 'C'" & _
                       " WHERE " & strChiave & _
                       " AND mn_rigaa = " & NTSCInt(!mn_rigaa)
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            Else
              strSQL = "DELETE TTMOPERNP" & _
                       " WHERE " & strChiave & _
                       " AND mn_rigaa = " & NTSCInt(!mn_rigaa)
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            End If
            If NTSCDec(!mn_mmquant) > NTSCDec(!mn_quant) Then
              dQuantTmp = NTSCDec(!mn_quant)
              !mn_mmquant = NTSCDec(!mn_quant)
              .AcceptChanges()
            End If
            If NTSCDec(!mn_mmquant) = NTSCDec(!mn_quant) Then
              !mn_mmquant = 0
              .AcceptChanges()
            End If
            strSQL = "UPDATE TTMOPERNP" & _
                     " SET mn_quant = mn_quant + " & CDblSQL(dQuantTmp) & "," & _
                     " mn_mmquant = mn_mmquant + " & CDblSQL(dQuantTmp) & "," & _
                     " mn_colli = -1," & _
                     " mn_mmcolli = 0," & _
                     " mn_mmcoldisimp = 0," & _
                     " mn_mmqtadisimp = mn_mmqtadisimp + " & CDblSQL(dQuantTmp) & "," & _
                     " mn_mmflevas = 'C'" & _
                     " WHERE " & strChiave & _
                     " AND mn_rigaa <> " & NTSCInt(!mn_rigaa) & _
                     " AND mn_lotto = " & lLotto
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            strSQL = "UPDATE TTMOPERNP" & _
                     " SET mn_mmquant = mn_quant," & _
                     " mn_colli = -1," & _
                     " mn_mmcolli = 0," & _
                     " mn_mmcoldisimp = 0," & _
                     " mn_mmqtadisimp = mn_quant," & _
                     " mn_mmflevas = 'C'" & _
                     " WHERE " & strChiave & _
                     " AND mn_rigaa <> " & NTSCInt(!mn_rigaa) & _
                     " AND mn_lotto = " & lLotto & _
                     " AND mn_mmquant > mn_quant"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          End If
        Else 'If NTSCInt(dtrT!mn_lotto) = 0 Then
          If nRighe = 1 Then
            strSQL = "UPDATE TTMOPERNP" & _
                     " SET mn_quant = " & CDblSQL(NTSCDec(!mn_mmquant)) & "," & _
                     " mn_colli = -1," & _
                     " mn_mmcolli = 0," & _
                     " mn_mmcoldisimp = 0," & _
                     " mn_mmqtadisimp = " & CDblSQL(NTSCDec(!mn_mmquant)) & "," & _
                     " mn_mmflevas = 'C'," & _
                     " mn_lotto = " & lLotto & _
                     " WHERE " & strChiave & _
                     " AND mn_rigaa = " & NTSCInt(!mn_rigaa)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

            !mn_rigaa = RitornaRigaaTTMOPERNP(strChiave)
            !mn_lotto = 0
            !mn_quant = NTSCDec(dtrT!mn_quant) - NTSCDec(dtrT!mn_mmquant)
            !mn_colli = -1
            !mn_mmcolli = 0
            !mn_mmcoldisimp = 0
            !mn_mmquant = 0
            !mn_mmqtadisimp = 0
            !mn_mmflevas = "C"

            .AcceptChanges()
            strSQL = "INSERT INTO TTMOPERNP " & GetQueryInsertField(ds.Tables("TTMOPERNP"), "mn_") & _
                     " VALUES " & GetQueryInsertValue(ds.Tables("TTMOPERNP"), dtrT, "mn_")
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          Else
            '--------------------------------------------------------------------------------------------------------
            dQuant = RitornaQuantitaTTMOPERNP(strChiave, lLotto)
            '--------------------------------------------------------------------------------------------------------
            strSQL = "UPDATE TTMOPERNP" & _
                     " SET mn_quant = " & CDblSQL(NTSCDec(!mn_mmquant)) & "," & _
                     " mn_colli = -1," & _
                     " mn_mmcolli = 0," & _
                     " mn_mmcoldisimp = 0," & _
                     " mn_mmqtadisimp = " & CDblSQL(NTSCDec(!mn_mmquant)) & "," & _
                     " mn_mmflevas = 'C'," & _
                     " mn_lotto = " & lLotto & _
                     " WHERE " & strChiave & _
                     " AND mn_rigaa = " & NTSCInt(!mn_rigaa)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            strSQL = "UPDATE TTMOPERNP" & _
                     " SET mn_quant = " & CDblSQL(dQuant - NTSCDec(!mn_mmquant)) & "," & _
                     " mn_colli = -1," & _
                     " mn_mmcolli = 0," & _
                     " mn_mmcoldisimp = 0," & _
                     " mn_mmqtadisimp = " & CDblSQL(dQuant - NTSCDec(!mn_mmquant)) & "," & _
                     " mn_mmflevas = 'C'," & _
                     " mn_lotto = 0" & _
                     " WHERE " & strChiave & _
                     " AND mn_rigaa <> " & NTSCInt(!mn_rigaa) & _
                     " AND mn_lotto = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          End If 'If nRighe = 1 Then
        End If
      End With
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function RitornaColliNegativiTTMOPERNP(ByVal strDitta As String, ByVal lIITTMoPernp As Integer, _
    ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------      
      strSQL = "SELECT * FROM TTMOPERNP" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTMoPernp & _
        " AND mn_colli < 0"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function AggiornaColliNegativiTTMOPERNP(ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------      
      For i As Integer = 0 To (dttTmp.Rows.Count - 1)
        With dttTmp.Rows(i)
          strSQL = "UPDATE TTMOPERNP" & _
            " SET mn_colli = " & CDblSQL(NTSCDec(!mn_colli)) & "," & _
            " mn_mmcolli = " & CDblSQL(NTSCDec(!mn_colli)) & "," & _
            " mn_mmcoldisimp = " & CDblSQL(NTSCDec(!mn_colli)) & _
            " WHERE codditt = " & CStrSQL(NTSCStr(!codditt)) & _
            " AND instid = " & NTSCInt(!instid) & _
            " AND mn_tipork = " & CStrSQL(NTSCStr(!mn_tipork)) & _
            " AND mn_anno = " & NTSCInt(!mn_anno) & _
            " AND mn_serie = " & CStrSQL(NTSCStr(!mn_serie)) & _
            " AND mn_numord = " & NTSCInt(!mn_numord) & _
            " AND mn_riga = " & NTSCInt(!mn_riga) & _
            " AND mn_rigaa = " & NTSCInt(!mn_rigaa) & _
            " AND mn_tcindtagl = " & NTSCInt(!mn_tcindtagl)
        End With
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function RitornaQuantitaTTMOPERNP(ByVal strChiave As String, ByVal lLotto As Integer) As Decimal
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT SUM(mn_quant) AS Quant FROM TTMOPERNP" & _
        " WHERE " & strChiave & _
        " AND (mn_lotto = 0 or mn_lotto = " & lLotto & ")"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        If Not dttTmp.Rows(0)!Quant.Equals(DBNull.Value) Then Return NTSCDec(dttTmp.Rows(0)!Quant)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return 0
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return 0
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function RitornaRigaaTTMOPERNP(ByVal strChiave As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT MAX(mn_rigaa) AS Rigaa FROM TTMOPERNP" & _
        " WHERE " & strChiave
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        If Not dttTmp.Rows(0)!Rigaa.Equals(DBNull.Value) Then Return NTSCInt(dttTmp.Rows(0)!Rigaa) + 1
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return 0
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return 0
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function


  Public Overridable Function IsInTestmag(ByVal oDoc As NTSDoc) As Boolean
    Dim strSQL As String = ""
    Try
      Dim strTabella As String = "testmag"
      If oDoc.strTipoRk = "W" Then strTabella = "testprb"
      strSQL = "SELECT codditt FROM " & strTabella & _
               " WHERE " & oDoc.GeneraWhere

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

End Class
