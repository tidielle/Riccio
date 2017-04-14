Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDVEGNBF
  Inherits CLD__BASE

  Public Overridable Function GetData(ByVal strDitta As String, ByVal lIIGegnbf As Integer, _
                                      ByVal strSebpQuery As String, ByRef ds As DataSet) As Boolean
    '-------------------
    'ottengo l'elenco dei documenti da generare
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Try
      strSQL = "SELECT GEGNBF.* "

      '------------------------
      'compongo la where
      If Not GetData_CreaWhere(strDitta, strWhere) Then Return False

      '------------------------
      'compone la select
      strSQL += " FROM GEGNBF " & _
        " where instid = " & lIIGegnbf & _
        strWhere & _
        " order by fd_conto, fd_anno, fd_serie, fd_numdoc, fd_ortipo, fd_oranno, fd_orserie, fd_ornum "

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "GEGNBF")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetData_CreaWhere(ByVal strDitta As String, ByRef strWhereOut As String) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ComponiQuerySebp(ByVal stredAnnoBolle As String, ByVal bckTutto As Boolean, _
                                           ByVal bopTuttiClienti As Boolean, ByVal stredDalcliente As String, _
                                           ByVal stredAlcliente As String, ByVal bopTutteBolle As Boolean, _
                                           ByVal stredTipobf As String, ByVal bopTuttiAgenti As Boolean, _
                                           ByVal stredCodcage As String, ByVal bopTutteZone As Boolean, _
                                           ByVal stredCodzona As String, ByVal bckVistate As Boolean, _
                                           ByVal stredNumpar As String, ByVal bopTutteDate As Boolean, _
                                           ByVal bckAnchealtre As Boolean, ByVal stredDalladata As String, _
                                           ByVal stredAlladata As String, ByVal bckConssoloasa As Boolean, _
                                           ByRef bSebpConssoloasa As Boolean, ByRef strSebpQuery As String) As Boolean
    Try
      Return ComponiQuerySebp(stredAnnoBolle, bckTutto, bopTuttiClienti, stredDalcliente, stredAlcliente, bopTutteBolle, _
                              stredTipobf, bopTuttiAgenti, stredCodcage, bopTutteZone, stredCodzona, bckVistate, stredNumpar, bopTutteDate, _
                              bckAnchealtre, stredDalladata, stredAlladata, bckConssoloasa, bSebpConssoloasa, strSebpQuery, 0, 9999, 0, 9999)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ComponiQuerySebp(ByVal stredAnnoBolle As String, ByVal bckTutto As Boolean, _
                                               ByVal bopTuttiClienti As Boolean, ByVal stredDalcliente As String, _
                                               ByVal stredAlcliente As String, ByVal bopTutteBolle As Boolean, _
                                               ByVal stredTipobf As String, ByVal bopTuttiAgenti As Boolean, _
                                               ByVal stredCodcage As String, ByVal bopTutteZone As Boolean, _
                                               ByVal stredCodzona As String, ByVal bckVistate As Boolean, _
                                               ByVal stredNumpar As String, ByVal bopTutteDate As Boolean, _
                                               ByVal bckAnchealtre As Boolean, ByVal stredDalladata As String, _
                                               ByVal stredAlladata As String, ByVal bckConssoloasa As Boolean, _
                                               ByRef bSebpConssoloasa As Boolean, ByRef strSebpQuery As String, _
                                               ByVal lDaVett1 As Integer, ByVal lAVett1 As Integer, _
                                               ByVal lDaVett2 As Integer, ByVal lAVett2 As Integer) As Boolean
    Dim strW1 As String = ""
    Dim strW2 As String = ""
    Dim strSQL As String = ""

    '----------------
    'per compatibilita' con funzioni ereditate da rive in versioni precedenti
    Dim oOut As Object = Nothing
    Dim oIn As New ArrayList(New Object() {stredAnnoBolle, bckTutto, bopTuttiClienti, stredDalcliente, stredAlcliente, bopTutteBolle, _
                                           stredTipobf, bopTuttiAgenti, stredCodcage, bopTutteZone, stredCodzona, bckVistate, _
                                           stredNumpar, bopTutteDate, bckAnchealtre, stredDalladata, stredAlladata, bckConssoloasa, _
                                           bSebpConssoloasa, strSebpQuery, lDaVett1, lAVett1, lDaVett2, lAVett2})
    If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      bSebpConssoloasa = CType(oIn(19), Boolean)
      strSebpQuery = CType(oIn(20), String)
      Return CBool(oOut)
    End If
    '----------------

    Try
      strW1 = " AND tm_tipork = 'W'"
      strW1 += " AND tm_anno = " & stredAnnoBolle
      'numero partita / viaggio
      If NTSCInt(stredNumpar) <> 0 Then strW1 += " AND tm_numpar = " & NTSCInt(stredNumpar)
      If bckTutto = False Then
        'DAL CLIENTE AL CLIENTE
        If bopTuttiClienti = False Then
          strW1 += " AND tm_conto >= " & stredDalcliente
          strW1 += " AND tm_conto <= " & stredAlcliente
        End If
        'tipobf
        If bopTutteBolle = False Then
          strW1 += " AND tm_tipobf = " & stredTipobf
        End If
        'agente
        If bopTuttiAgenti = False Then
          strW1 += " AND tm_codagen = " & stredCodcage
        End If
        'zona
        If bopTutteZone = False Then
          strW1 += " AND an_zona = " & stredCodzona
        End If
        strW1 += " AND (tm_vistato = 'S'"
        If bckVistate = False Then
          strW1 += " OR tm_vistato = 'N'"
        End If
        strW1 += ") "

        If lDaVett1 > 0 Or lAVett1 < 9999 Then strW1 &= " AND tm_vettor BETWEEN " & lDaVett1 & " AND " & lAVett1
        If lDaVett2 > 0 Or lAVett2 < 9999 Then strW1 &= " AND tm_vettor2 BETWEEN " & lDaVett2 & " AND " & lAVett2

        'Data bolla
        If bopTutteDate = False Then
          ' qui fa una sub query se anche quelle altre pregresse a parità di clienti
          If bckAnchealtre = False Then
            strW2 = " AND tm_datdoc >= " & CDataSQL(stredDalladata)
            strW2 += " AND tm_datdoc <= " & CDataSQL(stredAlladata)
          Else
            strW2 = " AND tm_conto in ( select tm_conto from testprb where " & Mid(strW1, 5) & " )"
          End If
        Else
          strW2 = ""
        End If
      End If
      If bckConssoloasa = True Then
        bSebpConssoloasa = True
      Else
        bSebpConssoloasa = False
      End If
      ' compone il pezzo della query finale ...
      strSebpQuery = strW1 & strW2

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CreaGegnbmm(ByVal strDitta As String, ByVal lIIGegnbmm As Integer, _
                                          ByVal strSebpQuery As String) As Boolean
    Dim strSQL As String = ""
    Dim strSQL1 As String = ""
    Dim strSQL2 As String = ""
    ' in quetsa fase in GEGNBMM vengono riportati solo le righe delle note di prelievo coì come sono,
    ' solo con le variazioni di valore e vprovv e vprovv2; solo in fase di generazione verranno rinumerate le righe
    ' con i numeri di bolla attribuiti e raggruppando se possibile le note di prelievo ...
    ' adesso crea la seconda tabella GEGNBMM con i dettagli nel formato in cui andarnno in MOVMAG bola/fattura
    ' (cioè come si comporta in Importanote di Veboll !!! )
    Try
      strSQL1 = "insert into GEGNBMM (codditt, instid, mm_tipork , mm_anno, mm_serie, mm_numdoc, mm_riga, mm_magaz, mm_magaz2, mm_causale, mm_causale2, mm_codart, mm_unmis, mm_descr, mm_colli, mm_quant, mm_prezzo, mm_scont1, mm_scont2, mm_scont3, mm_codiva, mm_preziva, mm_prezvalc, " & _
        " mm_controp, mm_provv, mm_commen, mm_flelab, mm_flcom, mm_flstat, mm_ortipo, mm_oranno, mm_orserie, mm_ornum, mm_orriga, mm_salcon, mm_matric, mm_stasino, mm_prelist, mm_codnomc, mm_massakg, mm_massaum2, mm_valstat, mm_proorig, mm_provv2, mm_percvst, mm_codcfam, mm_commeca, " & _
        " mm_subcommeca, mm_valore, mm_qtadisimp, mm_coldisimp, mm_valdisimp, mm_lotto, mm_qtafatt, mm_contocontr, mm_codcena, mm_desint, mm_codvuo, mm_vprovv, mm_vprovv2, mm_ump, mm_note, mm_numpac, mm_codclie, mm_prtipo, mm_pranno, mm_prserie, mm_prnum, mm_prriga, mm_cltipo, mm_clanno, " & _
        " mm_clserie, mm_clnum, mm_clriga, mm_nptipo, mm_npanno, mm_npserie, mm_npnum, mm_npriga, mm_npqtadis, mm_npcoldis, mm_npvaldis, mm_npsalcon, mm_nprcoleva, mm_nprquaeva,    mm_nprflevas,  mm_nprvalore,  mm_misura1,  mm_misura2,  mm_misura3,  mm_ultagg , mm_perqta, mm_scont4, mm_scont5, mm_scont6, " & _
        " mm_pmtaskid, mm_pmsalcon, mm_pmqtadis, mm_pmvaldis, mm_ubicaz, mm_fase, mm_flkit, mm_codtpro, mm_flprznet, mm_datini, mm_datfin, mm_scontp, mm_scontv, mm_valorev)"

      strSQL = "SELECT " & CStrSQL(strDitta) & ", " & lIIGegnbmm & ", movprb.mm_tipork , movprb.mm_anno, movprb.mm_serie, movprb.mm_numdoc, movprb.mm_riga, movprb.mm_magaz," & _
              "movprb.mm_magaz2 , movprb.mm_causale, movprb.mm_causale2, movprb.mm_codart, movprb.mm_unmis, movprb.mm_descr, " & _
          "movprb.mm_colli , movprb.mm_quant, movprb.mm_prezzo, movprb.mm_scont1, movprb.mm_scont2, movprb.mm_scont3, movprb.mm_codiva, " & _
          "movprb.mm_preziva , movprb.mm_prezvalc, movprb.mm_controp, movprb.mm_provv, movprb.mm_commen, movprb.mm_flelab, " & _
          "movprb.mm_flcom , movprb.mm_flstat, movprb.mm_ortipo, movprb.mm_oranno, movprb.mm_orserie, movprb.mm_ornum, " & _
          "movprb.mm_orriga , movprb.mm_salcon, movprb.mm_matric, movprb.mm_stasino, movprb.mm_prelist, movprb.mm_codnomc, " & _
          "movprb.mm_massakg , movprb.mm_massaum2, movprb.mm_valstat, movprb.mm_proorig, movprb.mm_provv2, movprb.mm_percvst, "
      strSQL = strSQL & " movprb.mm_codcfam , movprb.mm_commeca, movprb.mm_subcommeca, movprb.mm_valore, movprb.mm_qtadisimp, movprb.mm_coldisimp, " & _
          "movprb.mm_valdisimp , movprb.mm_lotto, movprb.mm_qtafatt, movprb.mm_contocontr, movprb.mm_codcena, movprb.mm_desint, " & _
          "movprb.mm_codvuo, movprb.mm_vprovv, movprb.mm_vprovv2, movprb.mm_ump, movprb.mm_note, movprb.mm_numpac, movprb.mm_codclie, " & _
          "movprb.mm_prtipo , movprb.mm_pranno, movprb.mm_prserie, movprb.mm_prnum, movprb.mm_prriga, movprb.mm_cltipo, movprb.mm_clanno, " & _
          "movprb.mm_clserie , movprb.mm_clnum, movprb.mm_clriga, movprb.mm_nptipo, movprb.mm_npanno, movprb.mm_npserie, movprb.mm_npnum, " & _
          " movprb.mm_npriga , movprb.mm_npqtadis, movprb.mm_npcoldis, movprb.mm_npvaldis, movprb.mm_npsalcon, movprb.mm_nprcoleva, " & _
          " movprb.mm_nprquaeva ,  movprb.mm_nprflevas, movprb.mm_nprvalore, movprb.mm_misura1, movprb.mm_misura2, " & _
           " movprb.mm_misura3 , movprb.mm_ultagg, movprb.mm_perqta, movprb.mm_scont4, movprb.mm_scont5, movprb.mm_scont6, "
      strSQL = strSQL & "  movprb.mm_pmtaskid, movprb.mm_pmsalcon, movprb.mm_pmqtadis, movprb.mm_pmvaldis," & _
          " movprb.mm_ubicaz, movprb.mm_fase, movprb.mm_flkit, movprb.mm_codtpro, movprb.mm_flprznet," & _
          " movprb.mm_datini, movprb.mm_datfin, movprb.mm_scontp, movprb.mm_scontv, movprb.mm_valorev"

      strSQL = strSQL & " from (((((testprb inner join anagra on testprb.codditt = anagra.codditt AND testprb.tm_conto = anagra.an_conto)  " & _
              " INNER JOIN TABPAGA on testprb.tm_codpaga = tabpaga.tb_codpaga)" & _
              " INNER JOIN TABTPBF on testprb.codditt = tabtpbf.codditt AND testprb.tm_tipobf = tabtpbf.tb_codtpbf)"
      strSQL = strSQL & " INNER JOIN movprb on testprb.codditt = movprb.codditt AND testprb.tm_tipork=movprb.mm_tipork and  testprb.tm_anno=movprb.mm_anno and testprb.tm_serie=movprb.mm_serie and testprb.tm_numdoc=movprb.mm_numdoc  )" & _
            " LEFT JOIN TESTORD on movprb.codditt = testord.codditt AND movprb.mm_ortipo=testord.td_tipork and movprb.mm_oranno=testord.td_anno and movprb.mm_orserie=testord.td_serie and movprb.mm_ornum=testord.td_numord  )" & _
            " WHERE testprb.codditt = " & CStrSQL(strDitta) & _
            strSebpQuery & _
            " AND mm_nprflevas = 'C'" & _
            " order by tm_conto, tm_anno, tm_serie, tm_numdoc"
      strSQL2 = strSQL1 & strSQL
      Execute(strSQL2, CLE__APP.DBTIPO.DBAZI)

      ' adesso con una update aggiorna i valori mm_valore, mm_vprovv e mm_vprovv2,
      ' setta inoltre quant e colli e pulisce i campi tipici della nota di prelievo e setta invece
      ' i campi di riferimento alla nota di prelievo di origine
      strSQL = "UPDATE GEGNBMM SET mm_quant = mm_quant - mm_nprquaeva, mm_colli = mm_colli - mm_nprcoleva , " & _
               " mm_nptipo= mm_tipork, mm_npanno = mm_anno, mm_npserie = mm_serie, mm_npnum = mm_numdoc, mm_npriga=mm_riga, " & _
               " mm_npqtadis = mm_quant - mm_nprquaeva, mm_npcoldis = mm_colli - mm_nprcoleva, mm_npvaldis = mm_nprvalore, " & _
               " mm_npsalcon = 'S' , " & _
               " mm_nprcoleva = 0, mm_nprquaeva = 0, mm_nprvalore = 0, mm_nprflevas = 'C' " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND GEGNBMM.instid = " & lIIGegnbmm
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SettaOrdiniNonASaldo(ByVal strDitta As String, ByVal lIIGegnbmm As Integer, _
                                                   ByVal strSebpQuery As String, ByVal lIIGegnbtd As Integer, _
                                                   ByVal lIIGegnbf As Integer, ByVal lIIGegnbmo As Integer, _
                                                   ByVal bSebpConssoloasa As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strSQL1 As String = ""
    Dim strSQL2 As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    ' elimina dai due temporanei principali gli ordini non a saldo ...
    Try
      ' prima individua e inserisce in una prima tabella temporanea gli ordini coinvolti
      strSQL = "INSERT INTO GEGNBTD (codditt, instid, fdo_tipork, fdo_anno, fdo_serie, fdo_numord, fdo_soloasa, fdo_tdflevas ) " & _
             " SELECT DISTINCT " & CStrSQL(strDitta) & ", " & lIIGegnbtd & ", fd_ortipo, fd_oranno, fd_orserie, fd_ornum, fd_soloasa, fd_tdflevas" & _
             " FROM GEGNBF" & _
             " WHERE codditt = " & CStrSQL(strDitta) & _
             " AND instid = " & lIIGegnbf & _
             " AND fd_ornum <> 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      ' adesso inserisce in GEGNBMO le righe di movord relative
      strSQL = "INSERT INTO GEGNBMO (codditt, instid, mo_tipork,  mo_anno,  mo_serie,  mo_numord,  mo_riga,  mo_codart,  mo_datcons,  mo_magaz,  mo_magaz2,  " & _
               "mo_unmis,  mo_descr,  mo_colli,  mo_coleva,  mo_quant,  mo_quaeva,  mo_flevas,  mo_colpre,  mo_quapre,  mo_flevapre,  mo_prezzo,  mo_scont1, " & _
               "mo_scont2,  mo_scont3,  mo_provv,  mo_codiva,  mo_preziva,  mo_prezvalc,  mo_commen,  mo_note,  mo_controp,  mo_stasino,  mo_provv2,  mo_tiporkor, " & _
               "mo_annoor,  mo_serieor,  mo_numordor,  mo_rigaor,  mo_prelist,  mo_codcfam,  mo_commeca,  mo_subcommeca,  mo_valore,  mo_contocontr,  mo_codcena, " & _
               "mo_desint,  mo_codvuo,  mo_vprovv,  mo_vprovv2,  mo_ump,  mo_confermato,  mo_lotto,  mo_rilasciato,  mo_aperto,  mo_ricimp,  mo_datconsor,  mo_codclie, " & _
               "mo_misura1,  mo_misura2,  mo_misura3,  mo_ultagg, mo_perqta, mo_scont4, mo_scont5, mo_scont6, " & _
               "mo_datini, mo_datfin, mo_scontp, mo_scontv)"
      strSQL = strSQL & " SELECT " & CStrSQL(strDitta) & ", " & lIIGegnbmo & ", mo_tipork,  mo_anno,  mo_serie,  mo_numord,  mo_riga,  mo_codart,  mo_datcons,  mo_magaz,  mo_magaz2, " & _
               "mo_unmis,  mo_descr,  mo_colli,  mo_coleva,  mo_quant,  mo_quaeva,  mo_flevas,  mo_colpre,  mo_quapre,  mo_flevapre,  mo_prezzo,  mo_scont1, " & _
               "mo_scont2,  mo_scont3,  mo_provv,  mo_codiva,  mo_preziva,  mo_prezvalc,  mo_commen,  mo_note,  mo_controp,  mo_stasino,  mo_provv2,  mo_tiporkor, " & _
               "mo_annoor,  mo_serieor,  mo_numordor,  mo_rigaor,  mo_prelist,  mo_codcfam,  mo_commeca,  mo_subcommeca,  mo_valore,  mo_contocontr,  mo_codcena, " & _
               "mo_desint,  mo_codvuo,  mo_vprovv,  mo_vprovv2,  mo_ump,  mo_confermato,  mo_lotto,  mo_rilasciato,  mo_aperto,  mo_ricimp,  mo_datconsor,  mo_codclie, " & _
               "mo_misura1,  mo_misura2,  mo_misura3,  mo_ultagg, mo_perqta, mo_scont4, mo_scont5, mo_scont6, " & _
               "mo_datini, mo_datfin, mo_scontp, mo_scontv"
      strSQL = strSQL & " FROM MOVORD INNER JOIN GEGNBTD ON MOVORD.codditt = GEGNBTD.codditt AND MOVORD.mo_tipork = GEGNBTD.fdo_tipork AND MOVORD.mo_anno = GEGNBTD.fdo_anno AND MOVORD.mo_serie = GEGNBTD.fdo_serie AND MOVORD.mo_numord = GEGNBTD.fdo_numord " & _
               " WHERE GEGNBTD.codditt = " & CStrSQL(strDitta) & _
               " AND GEGNBTD.instid = " & lIIGegnbtd & " "
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      'Adesso fa un update come se avesse evaso quegli ordini ...
      'Aggiorna le quantità evase sull'ordine
      strSQL = "UPDATE GEGNBMO " & _
               "SET mo_flevas = mm_salcon, mo_quaeva = mo_quaeva + mm_quant, mo_coleva = mo_coleva + mm_colli, mo_valore = mo_valore - mm_valdisimp, " & _
               "mo_ultagg = " & CDataSQL(Now.ToShortDateString) & _
               " FROM GEGNBMM INNER JOIN GEGNBMO ON (GEGNBMM.codditt = GEGNBMO.codditt) AND (GEGNBMM.mm_ortipo = GEGNBMO.mo_tipork) AND (GEGNBMM.mm_oranno = GEGNBMO.mo_anno) AND (GEGNBMM.mm_orserie = GEGNBMO.mo_serie) AND (GEGNBMM.mm_ornum = GEGNBMO.mo_numord) AND (GEGNBMM.mm_orriga = GEGNBMO.mo_riga)" & _
               " WHERE GEGNBMM.codditt = " & CStrSQL(strDitta) & _
               " AND GEGNBMM.instid = " & lIIGegnbmm & " and GEGNBMO.instid = " & lIIGegnbmo & " and mm_salcon = 'C' AND mm_ornum > 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "UPDATE GEGNBMO " & _
               "SET mo_flevas = mm_salcon, mo_quaeva = mo_quaeva + mm_quant, mo_coleva = mo_coleva + mm_colli, mo_valore = mo_valore - mm_valdisimp, " & _
               "mo_ultagg = " & CDataOraSQL(Now.ToShortDateString) & _
               " FROM GEGNBMM INNER JOIN GEGNBMO ON (GEGNBMM.codditt = GEGNBMO.codditt) AND (GEGNBMM.mm_ortipo = GEGNBMO.mo_tipork) AND (GEGNBMM.mm_oranno = GEGNBMO.mo_anno) AND (GEGNBMM.mm_orserie = GEGNBMO.mo_serie) AND (GEGNBMM.mm_ornum = GEGNBMO.mo_numord) AND (GEGNBMM.mm_orriga = GEGNBMO.mo_riga)" & _
               " WHERE GEGNBMM.codditt = " & CStrSQL(strDitta) & _
               " AND GEGNBMM.instid = " & lIIGegnbmm & " and GEGNBMO.instid = " & lIIGegnbmo & " and mm_salcon = 'S' AND mm_ornum > 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      ' adesso per ogni odine controlla che sia evaso totalmente ...
      strSQL = "SELECT * from GEGNBTD" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIIGegnbtd
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "GEGNBTD")
      If dsTmp.Tables("GEGNBTD").Rows.Count = 0 Then
        Return True
      End If
      ' per ogni ordine coivolto, faccio la stessa cosa che farebbe veboll in aggmovord su TESTORD
      ' considerando però come se fosse già evaso con quelle righe di note di prelievo che diventeranno bolle
      For i = 0 To dsTmp.Tables("GEGNBTD").Rows.Count - 1
        With dsTmp.Tables("GEGNBTD").Rows(i)
          ' faccio una join per settare in griglia fd_tdflevas a S per  gli ordini che sarebbero evasi a saldo
          strSQL = "UPDATE GEGNBF" & _
            " SET fd_tdflevas = 'S'" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIIGegnbf & _
            " AND fd_ortipo = '" & NTSCStr(!fdo_tipork) & "'" & _
            " AND fd_oranno = " & NTSCStr(!fdo_anno) & _
            " AND fd_orserie = '" & NTSCStr(!fdo_serie) & "'" & _
            " AND fd_ornum = " & NTSCStr(!fdo_numord) & _
            " AND NOT EXISTS (SELECT mo_riga FROM GEGNBMO" & _
                            " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND GEGNBMO.instid = " & lIIGegnbmo & _
                            " AND fd_ortipo = mo_tipork" & _
                            " AND fd_oranno = mo_anno" & _
                            " AND fd_orserie = mo_serie" & _
                            " AND fd_ornum = mo_numord" & _
                            " AND mo_flevas ='C')"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          ' fa la stessa cosa su GEGNBTD
          strSQL = "UPDATE GEGNBTD" & _
            " SET fdo_tdflevas = 'S'" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND GEGNBTD.instid = " & lIIGegnbtd & _
            " AND fdo_tipork = '" & NTSCStr(!fdo_tipork) & "'" & _
            " AND fdo_anno = " & NTSCStr(!fdo_anno) & _
            " AND fdo_serie = '" & NTSCStr(!fdo_serie) & "'" & _
            " AND fdo_numord = " & NTSCStr(!fdo_numord) & _
            " AND NOT EXISTS (SELECT mo_riga FROM GEGNBMO" & _
                            " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND GEGNBMO.instid = " & lIIGegnbmo & _
                            " AND fdo_tipork = mo_tipork" & _
                            " AND fdo_anno = mo_anno" & _
                            " AND fdo_serie = mo_serie" & _
                            " AND fdo_numord = mo_numord" & _
                            " AND mo_flevas ='C')"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next
      ' adesso cancella le righe in griglia relative ad ordini che non sarebbero evasi a saldo
      If bSebpConssoloasa Then
        strSQL = "DELETE GEGNBF from GEGNBF INNER JOIN GEGNBTD on GEGNBF.codditt = GEGNBTD.codditt AND GEGNBF.fd_ortipo = GEGNBTD.fdo_tipork AND " & _
        "GEGNBF.fd_oranno = GEGNBTD.fdo_anno AND GEGNBF.fd_orserie = GEGNBTD.fdo_serie AND GEGNBF.fd_ornum = GEGNBTD.fdo_numord " & _
        " WHERE GEGNBF.codditt = " & CStrSQL(strDitta) & _
        " AND GEGNBF.instid = " & lIIGegnbf & " and GEGNBTD.instid = " & lIIGegnbtd & " and fd_soloasa = 'S' and fd_tdflevas = 'N' "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        ' fa lo stesso lavoro su gegnbmm (per ora non c'e bisogno di td e  mo ...)
        strSQL = "DELETE GEGNBMM from GEGNBMM INNER JOIN GEGNBTD on GEGNBMM.codditt = GEGNBTD.codditt AND GEGNBMM.mm_ortipo = GEGNBTD.fdo_tipork AND " & _
        "GEGNBMM.mm_oranno = GEGNBTD.fdo_anno AND GEGNBMM.mm_orserie = GEGNBTD.fdo_serie AND GEGNBMM.mm_ornum = GEGNBTD.fdo_numord " & _
        " WHERE GEGNBMM.codditt = " & CStrSQL(strDitta) & _
        " AND GEGNBMM.instid = " & lIIGegnbmm & " and GEGNBTD.instid = " & lIIGegnbtd & " and fdo_soloasa = 'S' and fdo_tdflevas = 'N' "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function FileNuovo(ByVal strDitta As String, ByVal lIIGegnbf As Integer, _
                                        ByVal strSebpQuery As String) As Boolean
    Dim strSQL As String = ""
    Dim strSQL1 As String = ""
    Dim strSQL2 As String = ""
    Try
      strSQL1 = "INSERT INTO GEGNBF (codditt, instid, fd_anno, fd_serie, fd_numdoc," & _
        " fd_datdoc, fd_conto, fd_descr, fd_totdoc, fd_flsel, fd_ortipo, fd_oranno," & _
        " fd_orserie, fd_ornum, fd_soloasa, fd_tdflevas, fd_codpaga, fd_despaga," & _
        " fd_codtpbf, fd_destpbf, fd_ordata, fd_ultagg) "
      strSQL = "select " & CStrSQL(strDitta) & ", " & lIIGegnbf & ", tm_anno, tm_serie, tm_numdoc, MIN(tm_datdoc), tm_conto, MIN(an_descr1), MIN(tm_totdoc), 'S', " & _
            " CASE WHEN mm_ortipo is null THEN ' ' ELSE mm_ortipo END as ortipo, mm_oranno, CASE WHEN mm_orserie is null THEN ' ' ELSE mm_orserie END as orserie, mm_ornum, MIN(CASE WHEN td_soloasa is null THEN 'N' ELSE td_soloasa END), 'N', MIN(tm_codpaga), MIN(tb_despaga), MIN(tm_tipobf), MIN(tb_destpbf), MIN(td_ultagg), MIN(tm_ultagg) " & _
            " FROM (((((testprb inner join anagra on testprb.codditt = anagra.codditt AND testprb.tm_conto = anagra.an_conto)  " & _
            " INNER JOIN TABPAGA on testprb.tm_codpaga = tabpaga.tb_codpaga)" & _
            " INNER JOIN TABTPBF on testprb.codditt = tabtpbf.codditt AND testprb.tm_tipobf = tabtpbf.tb_codtpbf)"
      strSQL = strSQL & " INNER JOIN movprb on testprb.codditt = movprb.codditt AND testprb.tm_tipork=movprb.mm_tipork and  testprb.tm_anno=movprb.mm_anno and testprb.tm_serie=movprb.mm_serie and testprb.tm_numdoc=movprb.mm_numdoc  )" & _
            " LEFT JOIN TESTORD on movprb.codditt = testord.codditt AND movprb.mm_ortipo=testord.td_tipork and movprb.mm_oranno=testord.td_anno and movprb.mm_orserie=testord.td_serie and movprb.mm_ornum=testord.td_numord)" & _
            " WHERE testprb.codditt = " & CStrSQL(strDitta) & _
            strSebpQuery & " and mm_nprflevas = 'C'  group by tm_conto, tm_anno, tm_serie, tm_numdoc,mm_ortipo, mm_oranno, mm_orserie, mm_ornum " & _
            " order by tm_conto, tm_anno, tm_serie, tm_numdoc,mm_ortipo, mm_oranno, mm_orserie, mm_ornum "
      strSQL2 = strSQL1 & strSQL
      Execute(strSQL2, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTestateTemp(ByVal strDitta As String, ByVal lIIGegnbf As Integer, _
    ByVal lIIGegnbmm As Integer, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '-------------------------------------------------------------------------------------------------------------
      Return GetTestateTemp(strDitta, lIIGegnbf, lIIGegnbmm, dttTmp, False)
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetTestateTemp(ByVal strDitta As String, ByVal lIIGegnbf As Integer, _
    ByVal lIIGegnbmm As Integer, ByRef dttTmp As DataTable, ByVal bRaggruppa As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim str9 As String = "'" & "".PadLeft(9, "0"c) & "'"

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIIGegnbf, lIIGegnbmm, dttTmp, bRaggruppa})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTmp = CType(oIn(3), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      '-------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM GEGNBF" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIIGegnbf & _
        " AND fd_flsel <> 'S'"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-------------------------------------------------------------------------------------------------------------

      If bRaggruppa Then
        'NB: Introdotto alias [Tmp], per evitare errore in esecuzione della query su SQL2014, a causa di un bug su SQL2014 non risolto neppure con l'SP1.
        strSQL = "SELECT [Tmp].*" & _
                  " FROM (" & _
                  "SELECT DISTINCT tm_conto, tm_anno, tm_serie, tm_numdoc, tm_codagen, tm_codagen2, tm_tipobf," & _
                  " tm_codpaga, tm_codese, tm_scont1, tm_scont2, tm_scopag, tm_valuta, tm_scorpo, tm_listino," & _
                  " tm_cup, tm_cig, tm_riferimpa, " & _
                  " CASE WHEN (" & IIf(bRaggruppa = False, "tb_decpaga = 3 OR", "").ToString & " tb_decpaga = 4) THEN tm_datapag ELSE NULL END AS datapag," & _
                  " tm_flspinc, tm_flbolli, tm_coddest, tm_coddest2, MIN(tm_banc1) AS banc1, MIN(tm_banc2) AS banc2," & _
                  " RIGHT(" & str9 & " + CAST(tm_conto AS VARCHAR(9)), 9) +" & _
                  " RIGHT(" & str9 & " + CAST(tm_coddest AS VARCHAR(9)), 9) AS CHIAVE" & _
                  " FROM GEGNBMM INNER JOIN GEGNBF ON GEGNBF.codditt = GEGNBMM.codditt AND GEGNBF.fd_anno = GEGNBMM.mm_anno AND GEGNBF.fd_serie = GEGNBMM.mm_serie AND GEGNBF.fd_numdoc = GEGNBMM.mm_numdoc" & _
                  " INNER JOIN testprb ON testprb.codditt = GEGNBMM.codditt AND testprb.tm_tipork = GEGNBMM.mm_tipork AND testprb.tm_anno = GEGNBMM.mm_anno AND testprb.tm_serie = GEGNBMM.mm_serie AND testprb.tm_numdoc = GEGNBMM.mm_numdoc" & _
                  " INNER JOIN tabpaga ON tabpaga.tb_codpaga = TESTPRB.tm_codpaga" & _
                  " WHERE GEGNBMM.codditt = " & CStrSQL(strDitta) & _
                  " AND GEGNBMM.instid = " & lIIGegnbmm & _
                  " AND GEGNBF.instid = " & lIIGegnbf & _
                  " AND mm_tipork = 'W'" & _
                  " GROUP BY tm_conto, tm_anno, tm_serie, tm_numdoc, tm_codagen, tm_codagen2, tm_tipobf, tm_codpaga," & _
                  " tm_codese, tm_scont1, tm_scont2, tm_scopag, tm_valuta, tm_scorpo, tm_listino," & _
                  " tm_cup, tm_cig, tm_riferimpa, " & _
                  " CASE WHEN (" & IIf(bRaggruppa = False, "tb_decpaga = 3 OR", "").ToString & " tb_decpaga = 4) THEN tm_datapag ELSE NULL END," & _
                  " tm_flspinc, tm_flbolli, tm_coddest, tm_coddest2," & _
                  " RIGHT(" & str9 & " + CAST(tm_conto AS VARCHAR(9)), 9) +" & _
                  " RIGHT(" & str9 & " + CAST(tm_coddest AS VARCHAR(9)), 9)" & _
                  " HAVING SUM(mm_quant) > 0 ) AS Tmp" & _
                  " ORDER BY [Tmp].CHIAVE"
      Else
        strSQL = "SELECT DISTINCT tm_conto, tm_anno, tm_serie, tm_numdoc, tm_codagen, tm_codagen2, tm_tipobf," & _
                  " tm_codpaga, tm_codese, tm_scont1, tm_scont2, tm_scopag, tm_valuta, tm_scorpo, tm_listino," & _
                  " tm_cup, tm_cig, tm_riferimpa, " & _
                  " CASE WHEN (" & IIf(bRaggruppa = False, "tb_decpaga = 3 OR", "").ToString & " tb_decpaga = 4) THEN tm_datapag ELSE NULL END AS datapag," & _
                  " tm_flspinc, tm_flbolli, tm_coddest, tm_coddest2, MIN(tm_banc1) AS banc1, MIN(tm_banc2) AS banc2," & _
                  " RIGHT(" & str9 & " + CAST(tm_conto AS VARCHAR(9)), 9) +" & _
                  " RIGHT(" & str9 & " + CAST(tm_coddest AS VARCHAR(9)), 9) AS CHIAVE" & _
                  " FROM GEGNBMM INNER JOIN GEGNBF ON GEGNBF.codditt = GEGNBMM.codditt AND GEGNBF.fd_anno = GEGNBMM.mm_anno AND GEGNBF.fd_serie = GEGNBMM.mm_serie AND GEGNBF.fd_numdoc = GEGNBMM.mm_numdoc" & _
                  " INNER JOIN testprb ON testprb.codditt = GEGNBMM.codditt AND testprb.tm_tipork = GEGNBMM.mm_tipork AND testprb.tm_anno = GEGNBMM.mm_anno AND testprb.tm_serie = GEGNBMM.mm_serie AND testprb.tm_numdoc = GEGNBMM.mm_numdoc" & _
                  " INNER JOIN tabpaga ON tabpaga.tb_codpaga = TESTPRB.tm_codpaga" & _
                  " WHERE GEGNBMM.codditt = " & CStrSQL(strDitta) & _
                  " AND GEGNBMM.instid = " & lIIGegnbmm & _
                  " AND GEGNBF.instid = " & lIIGegnbf & _
                  " AND mm_tipork = 'W'" & _
                  " GROUP BY tm_conto, tm_anno, tm_serie, tm_numdoc, tm_codagen, tm_codagen2, tm_tipobf, tm_codpaga," & _
                  " tm_codese, tm_scont1, tm_scont2, tm_scopag, tm_valuta, tm_scorpo, tm_listino," & _
                  " tm_cup, tm_cig, tm_riferimpa, " & _
                  " CASE WHEN (" & IIf(bRaggruppa = False, "tb_decpaga = 3 OR", "").ToString & " tb_decpaga = 4) THEN tm_datapag ELSE NULL END," & _
                  " tm_flspinc, tm_flbolli, tm_coddest, tm_coddest2," & _
                  " RIGHT(" & str9 & " + CAST(tm_conto AS VARCHAR(9)), 9) +" & _
                  " RIGHT(" & str9 & " + CAST(tm_coddest AS VARCHAR(9)), 9)" & _
                  " HAVING SUM(mm_quant) > 0" & _
                  " ORDER BY tm_conto, tm_codagen, tm_codagen2, tm_tipobf, tm_codpaga, tm_codese, tm_scont1, tm_scont2," & _
                  " tm_scopag, tm_valuta, tm_scorpo," & _
                  " CASE WHEN (tb_decpaga = 3 Or tb_decpaga = 4) THEN tm_datapag ELSE NULL END," & _
                  " tm_flspinc, tm_flbolli, tm_coddest, tm_coddest2"
      End If
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-------------------------------------------------------------------------------------------------------------
      Return True
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetTestmag(ByVal strDitta As String, ByVal dtrNotaPre As DataRow, _
    ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT testprb.*" & _
        " FROM testprb INNER JOIN tabpaga ON tabpaga.tb_codpaga = testprb.tm_codpaga" & _
        " WHERE TESTPRB.codditt = " & CStrSQL(strDitta) & _
        " AND tm_tipork = 'W'" & _
        " AND tm_anno = " & NTSCStr(dtrNotaPre!tm_anno) & _
        " AND tm_serie = " & CStrSQL(dtrNotaPre!tm_serie) & _
        " AND tm_numdoc = " & NTSCStr(dtrNotaPre!tm_numdoc)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetMovmag(ByVal strDitta As String, ByVal dtrTestMag As DataRow, _
                                        ByVal lIIGegnbmm As Integer, ByVal bScriviRigheZero As Boolean, _
                                        ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT GEGNBMM.*, mm_coddivi " & _
        " FROM GEGNBMM inner join movprb on GEGNBMM.codditt = movprb.codditt " & _
        " AND GEGNBMM.mm_tipork = movprb.mm_tipork AND GEGNBMM.mm_anno = movprb.mm_anno" & _
        " AND GEGNBMM.mm_serie = movprb.mm_serie AND GEGNBMM.mm_numdoc = movprb.mm_numdoc" & _
        " AND GEGNBMM.mm_riga = movprb.mm_riga" & _
        " INNER JOIN GEGNBF ON GEGNBMM.codditt = GEGNBF.codditt AND GEGNBMM.instid = GEGNBF.instid " & _
        " AND GEGNBMM.mm_anno = GEGNBF.fd_anno AND GEGNBMM.mm_serie = GEGNBF.fd_serie " & _
        " AND GEGNBMM.mm_numdoc = GEGNBF.fd_numdoc " & _
        " AND ISNULL(GEGNBMM.mm_ortipo, '') = GEGNBF.fd_ortipo AND GEGNBMM.mm_oranno = GEGNBF.fd_oranno " & _
        " AND ISNULL(GEGNBMM.mm_orserie, '') = GEGNBF.fd_orserie And GEGNBMM.mm_ornum = GEGNBF.fd_ornum" & _
        " WHERE GEGNBMM.codditt = " & CStrSQL(strDitta) & _
        " AND GEGNBMM.instid = " & lIIGegnbmm & _
        " and GEGNBMM.mm_tipork = '" & NTSCStr(dtrTestMag!tm_tipork) & _
        "' AND GEGNBMM.mm_anno = " & NTSCStr(dtrTestMag!tm_anno) & _
        " AND GEGNBMM.mm_serie = '" & NTSCStr(dtrTestMag!tm_serie) & _
        "' AND GEGNBMM.mm_numdoc = " & NTSCStr(dtrTestMag!tm_numdoc)
      If Not bScriviRigheZero Then
        strSQL = strSQL & " and ( GEGNBMM.mm_quant > 0 or ( GEGNBMM.mm_codart = 'D' and GEGNBMM.mm_quant = 0 ) ) "
      Else
        strSQL = strSQL & " and ( GEGNBMM.mm_quant >= 0 or (GEGNBMM. mm_codart = 'D' and GEGNBMM.mm_quant = 0 ) ) "
      End If

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

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

  Public Overridable Function GetMovmagtc(ByVal strDitta As String, ByVal strTipork As String, _
                                           ByVal strAnno As String, ByVal strSerie As String, _
                                           ByVal strNumdoc As String, ByVal strRiga As String, _
                                           ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM movmagtc " & _
        " WHERE codditt  =" & CStrSQL(strDitta) & " AND mm_tipork = " & CStrSQL(strTipork) & _
        " AND mm_anno  =" & strAnno & " AND mm_serie = " & CStrSQL(strSerie) & _
        " AND mm_numdoc  =" & strNumdoc & " AND mm_riga = " & strRiga & _
        " ORDER BY mm_riga"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function UpdateGegnbf(ByVal strDitta As String, ByVal lIIGegnbf As Integer, _
                                           ByVal strFdTipork As String, ByVal nFdAnnoFat As String, _
                                           ByVal strFdSerieFat As String, ByVal lNumFat As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      strSQL = "SELECT DISTINCT mm_ortipo, mm_oranno, mm_orserie, mm_ornum FROM movmag" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mm_tipork = " & CStrSQL(strFdTipork) & _
        " AND mm_anno = " & nFdAnnoFat & _
        " AND mm_serie = " & CStrSQL(strFdSerieFat) & _
        " AND mm_numdoc = " & lNumFat
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVMAG")

      For i = 0 To dsTmp.Tables("MOVMAG").Rows.Count - 1
        With dsTmp.Tables("MOVMAG").Rows(i)
          strSQL = "UPDATE GEGNBF" & _
            " SET fd_ordata = td_ultagg" & _
            " FROM testord" & _
            " WHERE GEGNBF.codditt = testord.codditt" & _
            " AND GEGNBF.fd_ortipo = testord.td_tipork" & _
            " AND GEGNBF.fd_oranno = testord.td_anno" & _
            " AND GEGNBF.fd_orserie = testord.td_serie" & _
            " AND GEGNBF.fd_ornum = testord.td_numord" & _
            " AND GEGNBF.codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIIGegnbf & _
            " AND fd_ortipo = " & CStrSQL(!mm_ortipo) & _
            " AND fd_oranno = " & NTSCStr(!mm_oranno) & _
            " AND fd_orserie = " & CStrSQL(!mm_orserie) & _
            " AND fd_ornum = " & NTSCStr(!mm_ornum)
        End With
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EsisteDoc(ByVal strDitta As String, ByVal strTipoDoc As String, _
                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean

    'restituisce true se il documento esiste, diversamente false
    Dim strSQL As String = ""
    Dim bResult As Boolean = False
    Dim strWhere As String = ""
    Dim dttTmp As New DataTable
    Try
      Select Case strTipoDoc
        Case "A", "D", "E", "N", "£"
          strWhere = "(tm_tipork = 'A' OR tm_tipork = 'D' OR tm_tipork = 'E' OR tm_tipork = 'N' OR tm_tipork = '£')"
        Case "B"
          strWhere = "tm_tipork = 'B'"
        Case "C"
          strWhere = "tm_tipork = 'C'"
        Case "M"
          strWhere = "tm_tipork = 'M'"
        Case "Z"
          strWhere = "tm_tipork = 'Z'"
        Case "L", "K", "J", "("
          strWhere = "(tm_tipork = 'L' OR tm_tipork = 'K' OR tm_tipork = 'J' OR tm_tipork = '(')"
        Case "T"
          strWhere = "tm_tipork = 'T'"
        Case "U"
          strWhere = "tm_tipork = 'U'"
        Case "W"
          strWhere = "tm_tipork = 'W'"
        Case "F", "I"
          strWhere = "(tm_tipork = 'I' OR tm_tipork = 'F')"
        Case "S", "P"
          strWhere = "(tm_tipork = 'S' OR tm_tipork = 'P')"
      End Select
      strSQL = "SELECT top 1 tm_tipork FROM " & IIf(strTipoDoc = "W", "testprb", "testmag").ToString & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND " & strWhere & _
               " AND tm_anno = " & nAnno & _
               " AND tm_serie = " & CStrSQL(strSerie) & _
               " AND tm_numdoc = " & lNumdoc.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then bResult = True

      Return bResult

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


End Class
