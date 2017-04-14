Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Imports System
Public Class CLDVEPNFA
  Inherits CLD__BASE

  Public Overridable Function GetModuliAcquistati(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT am_abilit from anazmod where codditt = " & CStrSQL(strDitta) & _
               "AND am_abilit = 'S' AND (am_modulo = 1 OR am_modulo = 41)"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CalcolaDataFineStampaLG(ByVal strDitta As String, ByVal nEscomp As Integer, _
                                                       ByRef strDtFieser As String) As Boolean
    Try
      Return CalcolaDataFineStampaLG(strDitta, nEscomp, strDtFieser, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CalcolaDataFineStampaLG(ByVal strDitta As String, ByVal nEscomp As Integer, _
                                                      ByRef strDtFieser As String, ByRef strDtulaca As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strFlgiobol As String = "S"
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, nEscomp, strDtFieser, strDtulaca})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strDtFieser = NTSCStr(oIn(2))
        strDtulaca = NTSCStr(oIn(3))
        Return CBool(oOut)
      End If
      '----------------

      strDtFieser = IntSetDate("01/01/1900")

      strSQL = "SELECT ac_flgiobol FROM anaditac WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then strFlgiobol = dttTmp.Rows(0)!ac_flgiobol.ToString
      dttTmp.Clear()

      strSQL = "SELECT * FROM tabanaz WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      strDtulaca = NTSCDate(dttTmp.Rows(0)!tb_dtulaca).ToShortDateString
      If strFlgiobol <> "S" And nEscomp <> NTSCInt(dttTmp.Rows(0)!tb_escomp) Then
        strDtFieser = NTSCDate(dttTmp.Rows(0)!tb_dtullgp).ToShortDateString
      Else
        strDtFieser = NTSCDate(dttTmp.Rows(0)!tb_dtullg).ToShortDateString
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function CalcolaDataFineStampaRI(ByVal strDitta As String, ByVal nAnno As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 * FROM tabduri " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_anno = " & nAnno & _
               " ORDER BY tb_udatreg DESC "
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDocumentiDaContab(ByVal strDitta As String, ByVal strTipork As String, _
                                                   ByVal strUsaContoFattDoc As String, ByVal nAnno As Integer, _
                                                   ByVal strDatini As String, ByVal strDatfin As String, _
                                                   ByVal strSerini As String, ByVal strSerfin As String, _
                                                   ByVal lNumini As Integer, ByVal lNumfin As Integer, _
                                                   ByVal bRielab As Boolean, ByVal nCodtpbf As Integer, _
                                                   ByRef dttOut As DataTable) As Boolean
    Try
      'obsoleta
      Return GetDocumentiDaContab(strDitta, strTipork, strUsaContoFattDoc, nAnno, strDatini, strDatfin, _
                                  strSerini, strSerfin, lNumini, lNumfin, bRielab, nCodtpbf, dttOut, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDocumentiDaContab(ByVal strDitta As String, ByVal strTipork As String, _
                                                   ByVal strUsaContoFattDoc As String, ByVal nAnno As Integer, _
                                                   ByVal strDatini As String, ByVal strDatfin As String, _
                                                   ByVal strSerini As String, ByVal strSerfin As String, _
                                                   ByVal lNumini As Integer, ByVal lNumfin As Integer, _
                                                   ByVal bRielab As Boolean, ByVal nCodtpbf As Integer, _
                                                   ByRef dttOut As DataTable, _
                                                   ByVal bBnrepnco As Boolean) As Boolean
    Dim strSQL As String = "SELECT DISTINCT 0 as xx_dtttm, 'N' as xx_raggr,"

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, strUsaContoFattDoc, nAnno, strDatini, strDatfin, _
                                             strSerini, strSerfin, lNumini, lNumfin, bRielab, nCodtpbf, dttOut, _
                                             bBnrepnco})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(12), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      ' ricrea la stringa di selezione in base a cio' che e stato ritornato da fmSesc (aggiungendo il filtro sul tipo..)
      Select Case strUsaContoFattDoc
        Case "0", "-1" : strSQL += " testmag.tm_conto as tm_conto"
        Case "2"
          If strTipork = "A" Or strTipork = "C" Or strTipork = "N" Or _
             strTipork = "E" Or strTipork = "L" Or strTipork = "J" Or strTipork = "B" Then
            'non documenti differiti
            strSQL += " CASE WHEN testmag.tm_contfatt <> 0 THEN testmag.tm_contfatt ELSE testmag.tm_conto END as tm_conto"
          Else
            strSQL += " testmag.tm_conto as tm_conto"
          End If
        Case "1"
          If strTipork = "A" Or strTipork = "C" Or strTipork = "N" Or strTipork = "D" Or _
             strTipork = "E" Or strTipork = "L" Or strTipork = "J" Or strTipork = "K" Or _
             strTipork = "£" Or strTipork = "(" Or strTipork = "B" Then
            strSQL += " CASE WHEN testmag.tm_contfatt <> 0 THEN testmag.tm_contfatt ELSE testmag.tm_conto END as tm_conto"
          Else
            strSQL += " testmag.tm_conto as tm_conto"
          End If
      End Select
      strSQL += ", tm_flscdb, tb_flacconto, tb_prestserv, testmag.tm_tipork, testmag.tm_anno, testmag.tm_serie, testmag.tm_numdoc, " & _
               " tm_datdoc, tm_tipobf, tm_codpaga, tm_codpaga2, tm_datapag, tm_codntra, tm_codagen, tm_scont1, tm_scont2, " & _
               " tm_scopag, tm_bolli, tm_speinc, tm_speacc, tm_speaccv, tm_valuta, tm_cambio, tm_scorpo, tm_flcont, tm_totdoc, tm_pagato, tm_abbuono, tm_totomag, tm_codiva_1, tm_codiva_2, tm_codiva_3, tm_codiva_4, tm_codiva_5, tm_codiva_6, tm_codiva_7, tm_codiva_8, " & _
               " tm_imponib_1, tm_imponib_2, tm_imponib_3, tm_imponib_4, tm_imponib_5, tm_imponib_6, tm_imponib_7, tm_imponib_8, tm_imposta_1, tm_imposta_2, tm_imposta_3, tm_imposta_4, tm_imposta_5, tm_imposta_6, tm_imposta_7, tm_imposta_8, " & _
               " tm_numprot, tm_alfpro, tm_nregiva, tm_datfat, tm_nregiva, tm_annpar, tm_alfpar, tm_numpar, tm_codese, tm_coddest, tm_flspinc, tm_flbolli, tm_proforma, tm_fleleff, tm_imponibv_1, tm_imponibv_2, tm_imponibv_3, tm_imponibv_4, tm_imponibv_5, tm_imponibv_6, tm_imponibv_7, tm_imponibv_8, " & _
               " tm_impostav_1, tm_impostav_2 , tm_impostav_3, tm_impostav_4, tm_impostav_5, tm_impostav_6, tm_impostav_7, tm_impostav_8, tm_totdocv, tm_datregef, tm_numregef, tm_datregin, tm_numrgin, tm_totlordo, tm_totmerce, tm_totlordov, tm_totmercev, " & _
               " tm_causale, tm_codcena, tm_abi, tm_cab, tm_datpar, tm_annfat, tm_tiporkfat, tm_commeca, tm_subcommeca, tm_datsca_1, tm_datsca_2, tm_datsca_3, tm_datsca_4, tm_datsca_5, tm_impsca_1, tm_impsca_2, tm_impsca_3, tm_impsca_4, tm_impsca_5, " & _
               " tm_datregom, tm_numregom, tm_banc1, tm_banc2, tm_bolliv, tm_speincv, tm_pagatov, tm_abbuonov, tm_totomagv, tm_impscav_1, tm_impscav_2, tm_impscav_3, tm_impscav_4, tm_impscav_5, tm_dtcomiva, tm_speimb, tm_speimbv, tm_diffda, tm_diffiva, " & _
               " tm_tippaga_1, tm_tippaga_2, tm_tippaga_3, tm_tippaga_4, tm_tippaga_5 , tm_ccambiati, tm_andescr2, tm_anindir, tm_autpag, tm_codbanc, tm_ultagg, " & _
               " tm_tippaga_6, tm_tippaga_7, tm_tippaga_8, tm_datsca_6, tm_datsca_7, tm_datsca_8, tm_impsca_6, tm_impsca_7, tm_impsca_8, tm_impscav_6, tm_impscav_7, tm_impscav_8, tm_riferim, tm_impvuopr, " & _
               " tm_codagen2, tm_impprov, tm_totprov, tm_totprov2, "
      strSQL += " testmagc.*, tabpaga.tb_concassp, tm_coddivi, tm_codcfam, tm_codcli, tabtpbf.tb_codcacadd, isnull(tabmaga.tb_codstab, 0) as tb_codstab, tm_flagiva_1, " & _
                " tm_numdoc2, tm_numpro2, tabpaga2.tb_concassp as tb_concassp2, testmag.tm_pagato2, testmag.tm_resto, tb_codcauc, " & _
                " tm_cup, tm_cig, tm_riferimpa"
      If bBnrepnco And (strTipork = "C" Or strTipork = "F") Then
        'devo lavorare sulla data chiusura di cassa, non sulla data del documento
        strSQL += ", movcassa.mc_datapertura, tabstab.tb_contocorrinc, tabrepc.tb_numregi "
      End If
      strSQL += " FROM (((((testmag INNER JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga) " & _
                " INNER JOIN testmagc ON testmag.codditt = testmagc.codditt AND testmag.tm_tipork = testmagc.tm_tipork " & _
                " AND testmag.tm_anno = testmagc.tm_anno AND testmag.tm_serie = testmagc.tm_serie AND testmag.tm_numdoc = testmagc.tm_numdoc) " & _
                " INNER JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf) " & _
                " LEFT JOIN tabmaga ON testmag.codditt = tabmaga.codditt AND testmag.tm_magaz = tabmaga.tb_codmaga) " & _
                " LEFT JOIN tabpaga AS tabpaga2 ON testmag.tm_codpaga2 = tabpaga2.tb_codpaga)"
      If bBnrepnco And (strTipork = "C" Or strTipork = "F") Then
        'devo lavorare sulla data chiusura di cassa, non sulla data del documento
        strSQL += "INNER JOIN movcassa ON testmagc.codditt = movcassa.codditt AND testmagc.tm_progchiu = movcassa.mc_idchiusura " & _
                  "INNER JOIN tabstab ON testmagc.codditt = tabstab.codditt AND testmagc.tm_codstab = tabstab.tb_codstab " & _
                  "INNER JOIN tabrepc ON testmagc.codditt = tabrepc.codditt AND testmagc.tm_codrepc = tabrepc.tb_codrepc "
      End If
      strSQL += " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                 " AND testmag.tm_tipork = " & CStrSQL(strTipork) & _
                 " AND tm_proforma <> 'S' " & _
                 " AND testmag.tm_numdoc > 0 "
      If bBnrepnco And (strTipork = "C" Or strTipork = "F") Then
        'devo lavorare sulla data chiusura di cassa, non sulla data del documento
        strSQL += " AND testmagc.tm_retail = 'S' " & _
                  " AND movcassa.mc_causale = 'F' " & _
                  " AND movcassa.mc_datmov BETWEEN " & CDataSQL(strDatini) & " AND " & CDataSQL(strDatfin)
      Else
        strSQL += " AND testmag.tm_datdoc BETWEEN " & CDataSQL(strDatini) & " AND " & CDataSQL(strDatfin) & _
                  " AND testmag.tm_anno = " & nAnno
      End If

      If strSerini <> " " Or strSerfin.ToUpper <> "Z".PadRight(SerieMaxLen, "Z"c) Then
        strSQL += " AND testmag.tm_serie = " & CStrSQL(strSerini)
      End If
      If lNumini <> 0 Or lNumfin <> 999999999 Then
        strSQL += " AND testmag.tm_numdoc BETWEEN " & lNumini & " AND " & lNumfin
      End If
      If bRielab = False Then strSQL += " AND tm_flcont <> 'S'"
      If nCodtpbf <> 0 Then strSQL += " AND tm_tipobf = " & nCodtpbf
      strSQL += " ORDER BY testmag.tm_datdoc, testmag.tm_serie, testmag.tm_numdoc"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDocumentiDaContabParcelle(ByVal strDitta As String, ByVal strTipork As String, _
                                                 ByVal strUsaContoFattDoc As String, ByVal nAnno As Integer, _
                                                 ByVal strDatini As String, ByVal strDatfin As String, _
                                                 ByVal strSerini As String, ByVal strSerfin As String, _
                                                 ByVal lNumini As Integer, ByVal lNumfin As Integer, _
                                                 ByVal bRielab As Boolean, ByVal nCodtpbf As Integer, _
                                                 ByRef dttOut As DataTable) As Boolean
    'ottengo i documenti / parcelle / notule da contabilizzare
    Dim strSQL As String = ""
    Try
      'di testpar tutti i campi eccetto tm_note
      strSQL = "SELECT DISTINCT 0 as xx_dtttm, 'N' as xx_raggr, 'N' as tm_flagiva_1, 0 as tm_resto, 0 as tm_pagato2, " & _
                " 0 as tm_totomag, null as tm_datregom, 0 as tm_numregom, 0 as tm_valuta, 0.0 as tm_cambio, " & _
                " 0 as tm_codntra, 'N' as tm_flscdb, tabtpbf.tb_prestserv, tabtpbf.tb_flacconto, " & _
                " 0.0 as tm_impostav_1, 0.0 as tm_impostav_2, 0.0 as tm_impostav_3, 0.0 as tm_impostav_4, " & _
                " 0.0 as tm_impostav_5, 0.0 as tm_impostav_6, 0.0 as tm_impostav_7, 0.0 as tm_impostav_8, " & _
                " 0.0 as tm_imponibv_1, 0.0 as tm_imponibv_2, 0.0 as tm_imponibv_3, 0.0 as tm_imponibv_4, " & _
                " 0.0 as tm_imponibv_5, 0.0 as tm_imponibv_6, 0.0 as tm_imponibv_7, 0.0 as tm_imponibv_8, " & _
                " 0.0 as tm_impostav_1, 0.0 as tm_impostav_2, 0.0 as tm_impostav_3, 0.0 as tm_impostav_4, " & _
                " 0.0 as tm_impostav_5, 0.0 as tm_impostav_6, 0.0 as tm_impostav_7, 0.0 as tm_impostav_8, " & _
                " 0.0 as tm_pagatov, 0.0 as tm_abbuonov, 0.0 as tm_totdocv, 0.0 as tm_totomagv, " & _
                " 0.0 as tm_diffda, 0.0 as tm_speincv, 0.0 as tm_bolliv, 0.0 as tm_speacc, 0.0 as tm_speimb, " & _
                " 0 as tm_numdoc2, 0 as tm_numpro2, " & _
                " 0.0 AS tm_impscav_1, 0.0 AS tm_impscav_2, 0.0 AS tm_impscav_3, 0.0 AS tm_impscav_4, 0.0 AS tm_impscav_5," & _
                " tabcovg.tb_codcovg, tabpaga.tb_concassp, tm_datdoc as tm_dtcomiva, tb_codcauc, " & _
                " testpar.codditt, tm_conto, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_datdoc, tm_riferim, " & _
                " tm_tipobf, tm_flfatt, tm_alffat, tm_numfat, tm_codpaga, 0 as tm_codpaga2, tm_datapag, tm_listino, " & _
                " tm_controp, tm_bolli, tm_speinc, tm_flcont, tm_totdoc, tm_pagato, tm_abbuono, " & _
                " tm_codiva_1, tm_codiva_2, tm_codiva_3, tm_codiva_4, tm_codiva_5, tm_codiva_6, " & _
                " tm_codiva_7, tm_codiva_8, tm_imponib_1, tm_imponib_2, tm_imponib_3, tm_imponib_4, " & _
                " tm_imponib_5, tm_imponib_6, tm_imponib_7, tm_imponib_8, tm_imposta_1, tm_imposta_2, " & _
                " tm_imposta_3, tm_imposta_4, tm_imposta_5, tm_imposta_6, tm_imposta_7, tm_imposta_8, " & _
                " tm_datfat, tm_numprot, ' ' as tm_alfpro, tm_nregiva, tm_annpar, tm_alfpar, tm_numpar, tm_codese, " & _
                " tm_flspinc, tm_flbolli, tm_coddest, tm_datregef, tm_numregef, tm_datregin, tm_numrgin, " & _
                " tm_totlordo, tm_totmerce, tm_codcena, tm_abi, tm_cab, tm_datpar, tm_annfat, tm_tiporkfat, " & _
                " tm_commeca, tm_subcommeca, tm_datsca_1, tm_datsca_2, tm_datsca_3, tm_datsca_4, tm_datsca_5, " & _
                " tm_impsca_1, tm_impsca_2, tm_impsca_3, tm_impsca_4, tm_impsca_5, tm_vistato, tm_banc1, " & _
                " tm_banc2, tm_ultagg, tm_andescr1, tm_andescr2, tm_anindir, tm_ancap, tm_ancitta, " & _
                " tm_anprov, tm_ancodfis, tm_tippaga_1, tm_tippaga_2, tm_tippaga_3, tm_tippaga_4, " & _
                " tm_tippaga_5, tm_codbanc, tm_opnome, tm_ritenut, tm_altriprev, tm_compens, tm_nonsogg, " & _
                " tm_nonsoggiva, tm_codcaus, tm_codrtac, tm_perrite, tm_percomp, tm_codcfam, tm_annoc, " & _
                " tm_flcascom, tm_ccambiati, tm_rtprogr, tm_cascom, tm_dtinc, tm_dittcli, tm_ftacc, " & _
                " tm_totacceva, tm_salacceva, tm_totaccscal, tm_versrt, tm_flspegen, tm_spegen, " & _
                " tm_perspegen, tm_mfprogr, '' as tm_cup, '' as tm_cig, '' as tm_riferimpa "
      strSQL += " FROM testpar INNER JOIN tabtpbf ON testpar.codditt = tabtpbf.codditt AND testpar.tm_tipobf = tabtpbf.tb_codtpbf " & _
                " INNER JOIN tabpaga ON testpar.tm_codpaga = tabpaga.tb_codpaga " & _
                " LEFT JOIN tabcovg on tabpaga.tb_concassp = tabcovg.tb_codcovg"
      strSQL += " WHERE testpar.codditt = " & CStrSQL(strDitta) & _
                " AND testpar.tm_tipork = " & CStrSQL(strTipork) & _
                " AND testpar.tm_datdoc BETWEEN " & CDataSQL(strDatini) & " AND " & CDataSQL(strDatfin) & _
                " AND testpar.tm_anno = " & nAnno
      If strSerini <> " " Or strSerfin.ToUpper <> "Z" Then
        strSQL += " AND testpar.tm_serie = '" & strSerini & "'"
      End If
      If lNumini <> 0 Or lNumfin <> 999999999 Then
        strSQL += " AND testpar.tm_numdoc BETWEEN " & lNumini & " AND " & lNumfin
      End If
      If bRielab = False Then strSQL += " AND testpar.tm_flcont <> 'S'"
      If nCodtpbf <> 0 Then strSQL += " AND testpar.tm_tipobf = " & nCodtpbf

      If strTipork = "3" Then
        'le notule devono avere come sistema di pagamento RIBA di una sola rata!
        strSQL += " AND tb_tippaga = 2 and tb_numrate = 1 "
      End If

      strSQL += " ORDER BY testpar.tm_serie, testpar.tm_numdoc"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestrielabAndRielab(ByVal strDitta As String, ByRef dtrTm As DataRow, ByVal lConto As Integer, _
                                  ByVal strAnscaden As String, ByVal nAnnpar As Integer, ByVal strAlfpar As String, _
                                  ByVal lNumpar As Integer, ByVal bInt As Boolean, ByVal bGestStanziamenti As Boolean, _
                                  ByRef lContOld As Integer, ByRef nAnnoOld As Integer, ByRef strSerieOld As String, _
                                  ByRef lNumdocOld As Integer, ByRef strErr As String) As Boolean
    Try
      'non più utilizzata. mantenuta solo per compatibilità con versioni precedenti
      Return TestrielabAndRielab(strDitta, dtrTm, lConto, strAnscaden, nAnnpar, strAlfpar, lNumpar, bInt, bGestStanziamenti, _
                                 lContOld, nAnnoOld, strSerieOld, lNumdocOld, strErr, False, False, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TestrielabAndRielab(ByVal strDitta As String, ByRef dtrTm As DataRow, ByVal lConto As Integer, _
                                ByVal strAnscaden As String, ByVal nAnnpar As Integer, ByVal strAlfpar As String, _
                                ByVal lNumpar As Integer, ByVal bInt As Boolean, ByVal bGestStanziamenti As Boolean, _
                                ByRef lContOld As Integer, ByRef nAnnoOld As Integer, ByRef strSerieOld As String, _
                                ByRef lNumdocOld As Integer, ByRef strErr As String, ByVal bIncassiAnticipatuSuDDT As Boolean) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTm, lConto, strAnscaden, nAnnpar, strAlfpar, lNumpar, _
                                             bInt, bGestStanziamenti, lContOld, nAnnoOld, strSerieOld, lNumdocOld, _
                                             strErr, bIncassiAnticipatuSuDDT})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(1), DataRow)
        lContOld = NTSCInt(oIn(9))
        nAnnoOld = NTSCInt(oIn(10))
        strSerieOld = NTSCStr(oIn(11))
        lNumdocOld = NTSCInt(oIn(12))
        strErr = NTSCStr(oIn(13))
        Return CBool(oOut)
      End If
      '----------------

      'non più utilizzata. mantenuta solo per compatibilità con versioni precedenti
      Return TestrielabAndRielab(strDitta, dtrTm, lConto, strAnscaden, nAnnpar, strAlfpar, lNumpar, bInt, bGestStanziamenti, _
                                 lContOld, nAnnoOld, strSerieOld, lNumdocOld, strErr, bIncassiAnticipatuSuDDT, False, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TestrielabAndRielab(ByVal strDitta As String, ByRef dtrTm As DataRow, ByVal lConto As Integer, _
                                  ByVal strAnscaden As String, ByVal nAnnpar As Integer, ByVal strAlfpar As String, _
                                  ByVal lNumpar As Integer, ByVal bInt As Boolean, ByVal bGestStanziamenti As Boolean, _
                                  ByRef lContOld As Integer, ByRef nAnnoOld As Integer, ByRef strSerieOld As String, _
                                  ByRef lNumdocOld As Integer, ByRef strErr As String, ByVal bIncassiAnticipatuSuDDT As Boolean, _
                                  ByVal bGiroEffettiNoChisCli As Boolean) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTm, lConto, strAnscaden, nAnnpar, strAlfpar, lNumpar, _
                                             bInt, bGestStanziamenti, lContOld, nAnnoOld, strSerieOld, lNumdocOld, _
                                             strErr, bIncassiAnticipatuSuDDT, bGiroEffettiNoChisCli})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(1), DataRow)
        lContOld = NTSCInt(oIn(9))
        nAnnoOld = NTSCInt(oIn(10))
        strSerieOld = NTSCStr(oIn(11))
        lNumdocOld = NTSCInt(oIn(12))
        strErr = NTSCStr(oIn(13))
        Return CBool(oOut)
      End If
      '----------------

      'non più utilizzata. mantenuta solo per compatibilità con versioni precedenti
      Return TestrielabAndRielab(strDitta, dtrTm, lConto, strAnscaden, nAnnpar, strAlfpar, lNumpar, bInt, bGestStanziamenti, _
                                 lContOld, nAnnoOld, strSerieOld, lNumdocOld, strErr, bIncassiAnticipatuSuDDT, bGiroEffettiNoChisCli, False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TestrielabAndRielab(ByVal strDitta As String, ByRef dtrTm As DataRow, ByVal lConto As Integer, _
                                    ByVal strAnscaden As String, ByVal nAnnpar As Integer, ByVal strAlfpar As String, _
                                    ByVal lNumpar As Integer, ByVal bInt As Boolean, ByVal bGestStanziamenti As Boolean, _
                                    ByRef lContOld As Integer, ByRef nAnnoOld As Integer, ByRef strSerieOld As String, _
                                    ByRef lNumdocOld As Integer, ByRef strErr As String, ByVal bIncassiAnticipatuSuDDT As Boolean, _
                                    ByVal bGiroEffettiNoChisCli As Boolean, ByVal bProfes As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim i As Integer = 0

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTm, lConto, strAnscaden, nAnnpar, strAlfpar, lNumpar, _
                                             bInt, bGestStanziamenti, lContOld, nAnnoOld, strSerieOld, lNumdocOld, _
                                             strErr, bIncassiAnticipatuSuDDT, bGiroEffettiNoChisCli, bProfes})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(1), DataRow)
        lContOld = NTSCInt(oIn(9))
        nAnnoOld = NTSCInt(oIn(10))
        strSerieOld = NTSCStr(oIn(11))
        lNumdocOld = NTSCInt(oIn(12))
        strErr = NTSCStr(oIn(13))
        Return CBool(oOut)
      End If
      '----------------

      strErr = ""

      'memorizzo il conto del cliente + partita prendendola dalla vecchia registrazione contabile 
      'in quanto in testmag potrebbero averli cambiati
      strSQL = "SELECT pn_conto, pn_annpar, pn_alfpar, pn_numpar FROM prinot" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND pn_riga = 1"
      If bIncassiAnticipatuSuDDT And NTSCStr(dtrTm!tm_tipork) = "B" Then
        strSQL += " AND pn_datreg = " & CDataSQL(NTSCDate(dtrTm!tm_datregin)) & _
                  " AND pn_numreg = " & dtrTm!tm_numrgin.ToString
      Else
        strSQL += " AND pn_datreg = " & CDataSQL(NTSCDate(dtrTm!tm_datregef)) & _
                  " AND pn_numreg = " & dtrTm!tm_numregef.ToString
      End If
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        lContOld = NTSCInt(dttTmp.Rows(0)!pn_conto)
        nAnnoOld = NTSCInt(dttTmp.Rows(0)!pn_annpar)
        strSerieOld = NTSCStr(dttTmp.Rows(0)!pn_alfpar)
        lNumdocOld = NTSCInt(dttTmp.Rows(0)!pn_numpar)
      Else
        'non dovrebbe mai succedere, diversamente significa che la registrazione in prinot è
        'già stata cancellata ...
        lContOld = lConto
        nAnnoOld = nAnnpar
        strSerieOld = strAlfpar
        lNumdocOld = lNumpar
      End If

      dttTmp.Clear()

      If strAnscaden = "N" Then GoTo prcCancella

      '-----------------------------
      ' guarda in scadenziari se ci sono records nuovi per quella partita: se si non rielaborabile
      strSQL = "SELECT sc_conto FROM scaden WHERE codditt = " & CStrSQL(strDitta) & _
              " AND sc_conto=" & lContOld & _
              " AND sc_annpar = " & nAnnoOld & _
              " AND sc_alfpar='" & strSerieOld & "' " & _
              " AND sc_numpar = " & lNumdocOld & _
              " AND sc_integr = '" & IIf(bInt, "S", "N").ToString & "' "
      If bIncassiAnticipatuSuDDT And NTSCStr(dtrTm!tm_tipork) = "B" Then
        strSQL += " AND not ( sc_datreg=" & CDataSQL(NTSCDate(dtrTm!tm_datregin)) & _
                  " AND sc_numreg = " & dtrTm!tm_numrgin.ToString & " )"
      Else
        strSQL += " AND not ( sc_datreg=" & CDataSQL(NTSCDate(dtrTm!tm_datregef)) & _
                  " AND sc_numreg = " & dtrTm!tm_numregef.ToString & " )"
      End If
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strErr = "9"
        Return False
      End If
      dttTmp.Clear()

      '-----------------------------
      'test se incassato e/o omaggio o no
      strSQL = "SELECT sc_conto FROM scaden WHERE codditt = " & CStrSQL(strDitta) & _
                " AND sc_conto = " & lContOld & _
                " AND sc_annpar = " & nAnnoOld & _
                " AND sc_alfpar = '" & strSerieOld & "' " & _
                " AND sc_numpar = " & lNumdocOld & _
                " AND sc_integr = '" & IIf(bInt, "S", "N").ToString & "' " & _
                " AND ((sc_rgsaldato <> 0"

      If NTSCInt(dtrTm!tm_numrgin) > 0 Or NTSCInt(dtrTm!tm_numregom) > 0 Then

        If NTSCInt(dtrTm!tm_numrgin) > 0 And NTSCInt(dtrTm!tm_numregom) = 0 Then
          ' se c'e reg.  dell'incasso  ..
          strSQL += " AND not ( sc_dtsaldato=" & CDataSQL(NTSCDate(dtrTm!tm_datregin)) & _
                    " AND sc_rgsaldato = " & dtrTm!tm_numrgin.ToString & " )"
        End If

        If NTSCInt(dtrTm!tm_numrgin) = 0 And NTSCInt(dtrTm!tm_numregom) > 0 Then
          ' se c'e reg.  dell'omaggio  ..
          strSQL += " AND not ( sc_dtsaldato=" & CDataSQL(NTSCDate(dtrTm!tm_datregom)) & _
                    " AND sc_rgsaldato = " & dtrTm!tm_numregom.ToString & " )"
        End If

        If NTSCInt(dtrTm!tm_numrgin) > 0 And NTSCInt(dtrTm!tm_numregom) > 0 Then
          ' se c'e reg. sia dell'incasso che dell'omaggio ..
          strSQL += " AND not (( sc_dtsaldato=" & CDataSQL(NTSCDate(dtrTm!tm_datregom)) & _
                    "            AND sc_rgsaldato = " & dtrTm!tm_numregom.ToString & " ) OR ( " & _
                    "            sc_dtsaldato=" & CDataSQL(NTSCDate(dtrTm!tm_datregin)) & _
                    "            AND sc_rgsaldato = " & dtrTm!tm_numrgin.ToString & " ))"
        End If
      Else
        ' adesso se non c'e ne incasso ne omaggio
        'non serve aggiungere nulla alla query sopra
      End If    'If NTSCInt(dtrTm!tm_numrgin) > 0 Or NTSCInt(dtrTm!tm_numregom) > 0 Then

      strSQL += ") OR (sc_flsaldato = 'N' AND sc_codincdiff <> 0))"    'se non è incassato/pagato ma è collegato ad un ABPD ...

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        strErr = "8"
        Return False
      Else
        dttTmp.Clear()
      End If

      '-----------------------------
      'giro clienti con emissione effetti senza chiusura RB
      'anche se la scadenza è a perta, potrei aver emesso degli effetti: in questo caso 
      'nn devo consentire la rielaborazione
      If bGiroEffettiNoChisCli Then
        'le scadenze aperte ATTIVE con flag 'distinta/autorizzato' = 'S' e numdist = 0, e tipo pagamento tratta o RB
        'sono le scadenze clienti su cui sono stati emessi gli effetti
        strSQL = "SELECT sc_conto FROM scaden WHERE codditt = " & CStrSQL(strDitta) & _
                " AND sc_conto = " & lContOld & _
                " AND sc_annpar = " & nAnnoOld & _
                " AND sc_alfpar = '" & strSerieOld & "' " & _
                " AND sc_numpar = " & lNumdocOld & _
                " AND sc_integr = '" & IIf(bInt, "S", "N").ToString & "' " & _
                " AND sc_fldis = 'S' " & _
                " AND sc_numdist = 0 " & _
                " AND sc_flsaldato = 'N' " & _
                " AND sc_darave = 'D' " & _
                " AND (sc_tippaga = 1 OR sc_tippaga = 2)"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          dttTmp.Clear()
          strErr = "3"
          Return False
        Else
          dttTmp.Clear()
        End If
      End If

prcCancella:
      '-----------------------------
      ' controlla se reg. già finita su bollato
      strSQL = "SELECT mi_prodef from MOVIVA" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND mi_prodef = 'D'" & _
                " AND mi_datreg = " & CDataSQL(NTSCDate(dtrTm!tm_datregef)) & _
                " AND mi_numreg = " & dtrTm!tm_numregef.ToString & _
                " AND mi_integr = '" & IIf(bInt, "S", "N").ToString & "' "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        strErr = "1"
        Return False
      End If
      dttTmp.Clear()

      strSQL = "SELECT pn_fllg from PRINOT" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND pn_fllg = 'S'" & _
                " AND pn_datreg = " & CDataSQL(NTSCDate(dtrTm!tm_datregef)) & _
                " AND pn_numreg = " & dtrTm!tm_numregef.ToString & _
                " AND pn_integr = '" & IIf(bInt, "S", "N").ToString & "' "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        strErr = "2"
        Return False
      End If
      dttTmp.Clear()

      'se gestione professionisti verifica se è già presente l'incasso in princpag (e se c'è che non sia l'incasso contestuale alla precedente contabilizzazione della fattura)
      'non lo faccio per le notule in quanto non toccano prfatt e princpag
      If bProfes And NTSCInt(dtrTm!tm_numregef) <> 0 And NTSCStr(dtrTm!tm_tipork) <> "3" Then
        strSQL = "SELECT rxp_datreg, rxp_numreg FROM princpag WHERE codditt = " & CStrSQL(strDitta) & _
          " AND rxp_datregr = " & CDataSQL(NTSCDate(dtrTm!tm_datregef)) & _
          " AND rxp_numregr = " & NTSCInt(dtrTm!tm_numregef)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          If NTSCDate(dttTmp.Rows(0)!rxp_datreg) = NTSCDate(dtrTm!tm_datregef) And NTSCInt(dttTmp.Rows(0)!rxp_numreg) = NTSCInt(dtrTm!tm_numregef) + 1 Then
            'è tutto ok: è l'incasso eseguito insieme alla contabilizzazione della fattura nell'elaborazione precedente
          Else
            dttTmp.Clear()
            strErr = "4"
            Return False
          End If
        End If
        dttTmp.Clear()
        'verifico se la registrazione è già stata stampata in definitivo sui registri (tabprcr)
        strSQL = "SELECT tb_dtulrc, tb_rgulrc, tb_dtulrip, tb_rgulrip, tb_dtulrpa, tb_rgulrpa " & _
                 " FROM tabprcr WHERE tb_codprcr = 1 AND codditt = " & CStrSQL(strDitta)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          If NTSCDate(dttTmp.Rows(0)!tb_dtulrc) > NTSCDate(dtrTm!tm_datregef) Or (NTSCDate(dttTmp.Rows(0)!tb_dtulrc) = NTSCDate(dtrTm!tm_datregef) And NTSCInt(dttTmp.Rows(0)!tb_rgulrc) > NTSCInt(dtrTm!tm_numregef) + 1) Then
            dttTmp.Clear()
            strErr = "5"
            Return False
          End If
          If NTSCDate(dttTmp.Rows(0)!tb_dtulrip) > NTSCDate(dtrTm!tm_datregef) Or (NTSCDate(dttTmp.Rows(0)!tb_dtulrip) = NTSCDate(dtrTm!tm_datregef) And NTSCInt(dttTmp.Rows(0)!tb_rgulrip) > NTSCInt(dtrTm!tm_numregef) + 1) Then
            dttTmp.Clear()
            strErr = "6"
            Return False
          End If
          If NTSCDate(dttTmp.Rows(0)!tb_dtulrpa) > NTSCDate(dtrTm!tm_datregef) Or (NTSCDate(dttTmp.Rows(0)!tb_dtulrpa) = NTSCDate(dtrTm!tm_datregef) And NTSCInt(dttTmp.Rows(0)!tb_rgulrpa) > NTSCInt(dtrTm!tm_numregef) + 1) Then
            dttTmp.Clear()
            strErr = "7"
            Return False
          End If
        End If
        dttTmp.Clear()
      End If    'If bProfes And NTSCInt(dtrTm!tm_nu


      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function TestNewDoc(ByVal strDitta As String, ByVal lConto As Integer, ByVal bInt As Boolean, _
                                         ByVal nAnnpar As Integer, ByVal strAlfpar As String, ByVal lNumpar As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      ' guarda in scadenziari se ci sono records nuovi per quella partita: se si non rielaborabile
      strSQL = "SELECT sc_conto FROM scaden WHERE codditt = " & CStrSQL(strDitta) & _
               " AND sc_conto = " & lConto & _
               " AND sc_annpar = " & nAnnpar & _
               " AND sc_alfpar = '" & strAlfpar & "' " & _
               " AND sc_numpar = " & lNumpar & _
               " AND sc_integr = '" & IIf(bInt, "S", "N").ToString & "' "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count <> 0 Then Return False

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function GetNumregivaFromTabnuma(ByVal strDitta As String, ByVal strTiponuma As String, _
                                                      ByVal strSerie As String, ByVal nAnno As Integer, _
                                                      ByRef nRegiva As Integer, ByRef strTregiva As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      nRegiva = 0
      strTregiva = ""
      strSQL = "SELECT tb_numval, tb_numflag FROM tabnuma" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND tb_numtipo = '" & strTiponuma & "'" & _
            " AND tb_numserie = '" & strSerie & "'" & _
            " AND tb_numcodl = " & nAnno.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        nRegiva = NTSCInt(dttTmp.Rows(0)!tb_numval)
        strTregiva = NTSCStr(dttTmp.Rows(0)!tb_numflag)
        Return True
      End If
      dttTmp.Clear()

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetSerieFromTabnuma(ByVal strDitta As String, ByVal strTregiva As String, _
                                                  ByVal nRegiva As Integer, ByVal nAnno As Integer, _
                                                  ByRef strSerie As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSerie = ""

      strSQL = "SELECT tb_numserie FROM tabnuma" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tb_numtipo = 'A'" & _
              " AND tb_numcodl = " & nAnno.ToString & _
              " AND tb_numval = " & nRegiva.ToString & _
              " AND tb_numflag = '" & strTregiva & "' "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strSerie = NTSCStr(dttTmp.Rows(0)!tb_numserie)
        Return True
      End If
      dttTmp.Clear()

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetAlfproFromTabnuma(ByVal strDitta As String, ByVal strTipork As String, _
                                                ByVal strSerie As String, ByVal nAnno As Integer) As String
    GetAlfproFromTabnuma = "" 'non settato
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try


      strSQL = "SELECT tb_alfpro FROM tabnuma" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tb_numtipo = " & CStrSQL(strTipork) & _
              " AND tb_numcodl = " & nAnno.ToString & _
              " AND tb_numserie = " & CStrSQL(strSerie)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        Return NTSCStr(dttTmp.Rows(0)!tb_alfpro)
      End If
      dttTmp.Clear()

      Return ""

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetTabduriTabatti(ByVal strDitta As String, ByVal strTregiva As String, _
                                               ByVal nRegiva As Integer, ByVal nAnno As Integer, _
                                               ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * " & _
                " FROM tabduri INNER JOIN tabatti ON tabduri.codditt = tabatti.codditt AND " & _
                " tabduri.tb_ucodatti = tabatti.tb_codatti AND tabduri.tb_anno = tabatti.tb_anno " & _
                " WHERE tabduri.codditt = " & CStrSQL(strDitta) & _
                " AND tb_utipreg = " & CStrSQL(strTregiva) & _
                " and tb_unumreg = " & nRegiva & _
                " and tabduri.tb_anno = " & nAnno
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDettaglioControp(ByVal strDitta As String, ByVal strTipork As String, _
                                                ByVal nAnno As Integer, ByVal strSerie As String, _
                                                ByVal lNumdoc As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      'obsoleta
      Return GetDettaglioControp(strDitta, strTipork, nAnno, strSerie, lNumdoc, dttOut, True)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDettaglioControp(ByVal strDitta As String, ByVal strTipork As String, _
                                                  ByVal nAnno As Integer, ByVal strSerie As String, _
                                                  ByVal lNumdoc As Integer, ByRef dttOut As DataTable, _
                                                  ByVal bGestDatecomp As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strScontV As String = "CASE WHEN tm_scorpo = 'S' AND mm_scontv <> 0 " & _
                              " THEN (100 * mm_scontv) / (100 + isnull(tb_aliq, 0)) " & _
                              " ELSE mm_scontv END"
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, nAnno, strSerie, lNumdoc, dttOut, bGestDatecomp})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(5), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      If strTipork = "D" Or strTipork = "P" Or strTipork = "£" Or strTipork = "(" Then
        ' FATTURE DIFFERITE
        strSQL = "SELECT " & _
           " sum(mm_valore) as VALORE, " & _
           " sum(mm_valorev) as VALOREV, " & _
           " mm_controp, mm_datini, mm_datfin, an_accperi " & _
           " FROM ((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_numdoc = movmag.mm_numdoc AND " & _
           " testmag.tm_serie = movmag.mm_serie AND testmag.tm_anno = movmag.mm_anno AND " & _
           " testmag.tm_tipork = movmag.mm_tipork) " & _
           " LEFT JOIN tabciva ON movmag.mm_codiva = tabciva.tb_codciva " & _
           " LEFT JOIN tabcove ON movmag.codditt = tabcove.codditt AND movmag.mm_controp = tabcove.tb_codcove)" & _
           " LEFT JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto" & _
           " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
           " AND tm_tiporkfat = '" & strTipork & "' AND tm_annfat = " & nAnno & _
           " AND tm_alffat = '" & strSerie & "' AND tm_numfat = " & lNumdoc & _
           " AND mm_stasino <> 'X'" & _
           " GROUP BY mm_controp, mm_datini, mm_datfin, an_accperi " & _
           " ORDER BY mm_controp "
      ElseIf strTipork = "K" Then
        ' FATTURE DIFFERITE RICEVUTE: potrebbero esserci dei carichi da prod: non posso prendere mm_valore
        strSQL = "SELECT " & _
          " sum(CASE WHEN mm_umprz <> 'S' THEN round(round(mm_prezzo * mm_quant / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - " & strScontV & ", " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") " & _
          " ELSE round(round(mm_prezzo * mm_colli / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - " & strScontV & ", " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") END) as VALORE, " & _
          " sum(CASE WHEN mm_umprz <> 'S' THEN round(round(mm_prezvalc * mm_quant / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - " & strScontV & ", " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") " & _
          " ELSE round(round(mm_prezvalc * mm_colli / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - " & strScontV & ", " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") END) as VALOREV, " & _
          " mm_controp, mm_datini, mm_datfin, an_accperi " & _
          " FROM ((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_numdoc = movmag.mm_numdoc AND " & _
          " testmag.tm_serie = movmag.mm_serie AND testmag.tm_anno = movmag.mm_anno AND " & _
          " testmag.tm_tipork = movmag.mm_tipork) " & _
          " LEFT JOIN tabciva ON movmag.mm_codiva = tabciva.tb_codciva " & _
          " LEFT JOIN tabcove ON movmag.codditt = tabcove.codditt AND movmag.mm_controp = tabcove.tb_codcove)" & _
          " LEFT JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto" & _
          " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
          " AND tm_tiporkfat = '" & strTipork & "' AND tm_annfat = " & nAnno & _
          " AND tm_alffat = '" & strSerie & "' AND tm_numfat = " & lNumdoc & _
          " AND mm_stasino <> 'X'" & _
          " GROUP BY mm_controp, mm_datini, mm_datfin, an_accperi " & _
          " ORDER BY mm_controp "
      ElseIf strTipork = "1" Or strTipork = "2" Or strTipork = "3" Or strTipork = "4" Or strTipork = "5" Then
        'PARCELLAZIONE (il tipork 3 non passerà mai di qui)
        strSQL = "SELECT " & _
           " sum(mm_valore) as VALORE, " & _
           " 0 as VALOREV, " & _
           " mm_controp, an_accperi, "
        If bGestDatecomp Then
          strSQL = strSQL & " mm_datini as mm_datini, mm_datfin as mm_datfin "
        Else
          strSQL = strSQL & " max(mm_datini) as mm_datini , max(mm_datfin) as mm_datfin "
        End If
        strSQL += " FROM ((testpar INNER JOIN movpar ON testpar.codditt = movpar.codditt AND testpar.tm_numdoc = movpar.mm_numdoc AND " & _
           " testpar.tm_serie = movpar.mm_serie AND testpar.tm_anno = movpar.mm_anno AND " & _
           " testpar.tm_tipork = movpar.mm_tipork) " & _
           " LEFT JOIN tabcove ON movpar.codditt = tabcove.codditt AND movpar.mm_controp = tabcove.tb_codcove)" & _
           " LEFT JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto" & _
           " WHERE testpar.codditt = " & CStrSQL(strDitta) & _
           " AND tm_tipork = '" & strTipork & "' AND tm_anno = " & nAnno & _
           " AND tm_serie = '" & strSerie & "' AND tm_numdoc = " & lNumdoc & _
           " GROUP BY mm_controp, an_accperi " & IIf(bGestDatecomp, ", mm_datini, mm_datfin", "").ToString & _
           " ORDER BY mm_controp "
      Else
        ' NON SONO FATTURE DIFFERITE
        strSQL = "SELECT " & _
           " sum(mm_valore) as VALORE, " & _
           " sum(mm_valorev) as VALOREV, " & _
           " mm_controp, mm_datini, mm_datfin, an_accperi " & _
           " FROM ((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_numdoc = movmag.mm_numdoc AND " & _
           " testmag.tm_serie = movmag.mm_serie AND testmag.tm_anno = movmag.mm_anno AND " & _
           " testmag.tm_tipork = movmag.mm_tipork) " & _
           " LEFT JOIN tabciva ON movmag.mm_codiva = tabciva.tb_codciva " & _
           " LEFT JOIN tabcove ON movmag.codditt = tabcove.codditt AND movmag.mm_controp = tabcove.tb_codcove)" & _
           " LEFT JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto" & _
           " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
           " AND tm_tipork = '" & strTipork & "' AND tm_anno = " & nAnno & _
           " AND tm_serie = '" & strSerie & "' AND tm_numdoc = " & lNumdoc & _
           " AND mm_stasino <> 'X'" & _
           " GROUP BY mm_controp, mm_datini, mm_datfin, an_accperi " & _
           " ORDER BY mm_controp "
      End If
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDettaglioContropRagg(ByVal strDitta As String, ByVal lIdRagg As Integer, ByRef dttTmOrig As DataTable, ByRef dttOut As DataTable) As Boolean
    'possono essere solo corrispettivi o ricevute fiscali emesse totalmente incassate!!!!
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Try
      For Each dtrT As DataRow In dttTmOrig.Select("xx_dtttm = " & lIdRagg.ToString)
        strWhere += " (mm_tipork = " & cstrsql(dtrT!tm_tipork) & " AND mm_anno = " & ntscint(dtrT!tm_anno) & " AND mm_serie = " & cstrsql(dtrT!tm_serie) & " AND mm_numdoc = " & ntscint(dtrT!tm_numdoc) & ") OR "
      Next
      If strWhere.Length > 0 Then strWhere = strWhere.Substring(0, strWhere.Length - 4)

      strSQL = "SELECT " & _
         " sum(mm_valore) as VALORE, " & _
         " sum(mm_valorev) as VALOREV, " & _
         " mm_controp, mm_datini, mm_datfin, an_accperi " & _
         " FROM ((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_numdoc = movmag.mm_numdoc AND " & _
         " testmag.tm_serie = movmag.mm_serie AND testmag.tm_anno = movmag.mm_anno AND " & _
         " testmag.tm_tipork = movmag.mm_tipork) " & _
         " LEFT JOIN tabcove ON movmag.codditt = tabcove.codditt AND movmag.mm_controp = tabcove.tb_codcove)" & _
         " LEFT JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto" & _
         " WHERE testmag.codditt = " & CStrSQL(strDitta)
      If strWhere <> "" Then strsql &= " AND (" & strWhere & ")"
      strsql &= " GROUP BY mm_controp, mm_datini, mm_datfin, an_accperi " & _
             " ORDER BY mm_controp "
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetRifribaFromClibanc(ByVal strDitta As String, ByVal lConto As Integer, _
                                                    ByVal lAbi As Integer, ByVal lCab As Integer, _
                                                    ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM clibanc" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND cba_conto = " & lConto & _
              " AND cba_abi = " & lAbi & _
              " AND cba_cab = " & lCab
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function DescrizioneScadenza(ByVal strDitta As String, ByVal lCoddest As Integer, ByVal lConto As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    'Se nel documento è stata indicata una destinazione diversa, ne preleva il campo
    ''Note' da inserire nella descrizione della scadenza
    'altrimenti preleva il campo 'Contatto' dall'anagrafica del conto
    Try
      If lCoddest <> 0 Then
        strSQL = "SELECT dd_note FROM destdiv" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND dd_conto = " & lConto & _
                  " AND dd_coddest = " & lCoddest
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then Return NTSCStr(dttTmp.Rows(0)!dd_note)

      Else
        strSQL = "SELECT an_contatt FROM anagra" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND an_conto = " & lConto
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then Return NTSCStr(dttTmp.Rows(0)!an_contatt)
      End If

      Return ""

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function RitornaAcFlciDitt(ByVal strDitta As String, ByVal lConto As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      RitornaAcFlciDitt = " "
      strSQL = "SELECT ac_flci FROM anagca" & _
              " WHERE ac_conto = " & lConto & " and codditt = " & CStrSQL(strDitta) & _
              " ORDER BY ac_conto "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCStr(dttTmp.Rows(0)!ac_flci)

      Return " "

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetFirstAnalinkFromContoCG(ByVal strDitta As String, ByVal lContocg As Integer) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT TOP 1 anl_acconto from analink" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND anl_anconto = " & lContocg
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!anl_acconto)

      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function GetMovmagCA(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, _
                                          ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal strFlscdb As String, _
                                          ByVal bGestDatecomp As Boolean, ByVal strAccperi As String, _
                                          ByVal nControp As Integer, ByVal dDtIniz As String, ByVal dDtFin As String, _
                                          ByRef dttOut As DataTable) As Boolean
    Try
      'obsoleta
      Return GetMovmagCA(strDitta, strTipork, nAnno, strSerie, lNumdoc, strFlscdb, bGestDatecomp, strAccperi, _
                         nControp, dDtIniz, dDtFin, dttOut, "N", 0, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetMovmagCA(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, _
                                          ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal strFlscdb As String, _
                                          ByVal bGestDatecomp As Boolean, ByVal strAccperi As String, _
                                          ByVal nControp As Integer, ByVal dDtIniz As String, ByVal dDtFin As String, _
                                          ByRef dttOut As DataTable, ByVal strRaggr As String, ByVal lIdRagg As Integer, _
                                          ByRef dttTmOrig As DataTable) As Boolean
    'se strRaggr = "S" sto contabilizznndo in modo raggruppato corrispettive o un ricevute fiscali emessi totalmente incassata
    'devo prelevare i movmag da tutti i documenti contenuti nel raggruppamento
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim strScontV As String = "CASE WHEN tm_scorpo = 'S' AND mm_scontv <> 0 " & _
                              " THEN (100 * mm_scontv) / (100 + ISNULL(tb_aliq, 0)) " & _
                              " ELSE mm_scontv END"
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, nAnno, strSerie, lNumdoc, strFlscdb, bGestDatecomp, _
                                             strAccperi, nControp, dDtIniz, dDtFin, dttOut, strRaggr, lIdRagg, _
                                             dttTmOrig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(11), DataTable)
        dttTmOrig = CType(oIn(14), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      If strTipork = "K" Or strTipork = "D" Or strTipork = "P" Or strTipork = "£" Or strTipork = "(" Then ' cerca le righe delle bolle associate
        strSQL = "SELECT mm_contocontr, mm_codcena, mm_codcfam as codcfam, mm_commeca,  mm_subcommeca as subcommeca, " & _
                 " ac_unmis, sum(mm_quant) as quant, tb_testci, " & _
                 " sum(CASE WHEN mm_umprz <> 'S' THEN round(round(mm_prezzo * mm_quant / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - " & strScontV & ", " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") ELSE " & _
                 " round(round(mm_prezzo * mm_colli / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - " & strScontV & ", " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") END ) as valore, " & _
                 " sum(CASE WHEN mm_umprz <> 'S' THEN round(round(mm_prezvalc * mm_quant / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - 0, " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") ELSE " & _
                 " round(round(mm_prezvalc * mm_colli / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - 0, " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") END ) as valorev, " & _
                 " ac_codmaca " & _
                 " from (tabcaum INNER JOIN ((testmag INNER JOIN movmag ON (testmag.codditt = movmag.codditt) AND (testmag.tm_numdoc = movmag.mm_numdoc) AND (testmag.tm_serie = movmag.mm_serie) AND (testmag.tm_anno = movmag.mm_anno) AND (testmag.tm_tipork = movmag.mm_tipork))" & _
                 " INNER JOIN keymag ON (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) AND (movmag.mm_numdoc = keymag.km_numdoc) AND (movmag.mm_serie = keymag.km_serie) AND (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_tipork = keymag.km_tipork)) ON tabcaum.tb_codcaum = keymag.km_causale) " & _
                 " INNER JOIN anagca ON movmag.codditt = anagca.codditt AND movmag.mm_contocontr = anagca.ac_conto " & _
                 " LEFT JOIN tabciva ON movmag.mm_codiva = tabciva.tb_codciva "
        strSQL += " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                 " AND not mm_codcfam is null and not mm_subcommeca is null and movmag.mm_codcena <> 0 and movmag.mm_controp = " & nControp & "  " & _
                 " AND mm_stasino <> 'X' "
        If bGestDatecomp And strFlscdb <> "S" And (strAccperi = "S" Or strAccperi = "D") Then
          strSQL += " and mm_datini = " & CDataSQL(dDtIniz) & " AND mm_datfin = " & CDataSQL(dDtFin)
        End If
        strSQL += " and tabcaum.tb_testci <> ' ' and testmag.tm_tiporkfat = '" & strTipork & "' and testmag.tm_annfat = " & nAnno & _
                 " and testmag.tm_alffat = '" & strSerie & "' and testmag.tm_numfat = " & lNumdoc & _
                 " group by mm_contocontr, mm_codcena,  mm_codcfam, mm_commeca,  mm_subcommeca, ac_unmis, tb_testci, ac_codmaca " & _
                 " order by mm_codcena,  mm_codcfam, mm_commeca,  mm_subcommeca "
        ' ora il caso delle fatture immediate qui sotto ...
      Else
        strSQL = "SELECT mm_contocontr, mm_codcena,  mm_codcfam as codcfam, mm_commeca,  mm_subcommeca as subcommeca, " & _
                 " ac_unmis, sum(mm_quant) as quant, tb_testci, " & _
                 " sum(CASE WHEN mm_umprz <> 'S' THEN round(round(mm_prezzo * mm_quant / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - " & strScontV & ", " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") ELSE " & _
                 " round(round(mm_prezzo * mm_colli / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - " & strScontV & ", " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") END ) as valore, " & _
                 " sum(CASE WHEN mm_umprz <> 'S' THEN round(round(mm_prezvalc * mm_quant / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - 0, " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") ELSE " & _
                 " round(round(mm_prezvalc * mm_colli / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100 * (100 - mm_scontp) / 100 - 0, " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") END ) as valorev, " & _
                 " ac_codmaca " & _
                 " from (((TESTMAG INNER JOIN MOVMAG ON (testmag.codditt = movmag.codditt) AND (testmag.tm_numdoc = movmag.mm_numdoc) AND " & _
                 " (testmag.tm_serie = movmag.mm_serie) AND (testmag.tm_anno = movmag.mm_anno) AND " & _
                 " (testmag.tm_tipork = movmag.mm_tipork)) " & _
                 " INNER JOIN KEYMAG ON (movmag.codditt = keymag.codditt)" & _
                 " AND (movmag.mm_riga = keymag.km_riga)" & _
                 " AND (movmag.mm_tipork = keymag.km_tipork)" & _
                 " AND (movmag.mm_serie = keymag.km_serie)" & _
                 " AND (movmag.mm_anno = keymag.km_anno)" & _
                 " AND (movmag.mm_numdoc = keymag.km_numdoc)) INNER JOIN TABCAUM ON (keymag.km_causale = tabcaum.tb_codcaum))" & _
                 " inner join ANAGCA ON (movmag.codditt = anagca.codditt)" & _
                 " AND (movmag.mm_contocontr = anagca.ac_conto) " & _
                 " LEFT JOIN tabciva ON movmag.mm_codiva = tabciva.tb_codciva "
        strSQL += " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                 " AND not mm_codcfam is null and not mm_subcommeca is null and movmag.mm_codcena <> 0 and movmag.mm_controp = " & nControp & "  " & _
                 " AND mm_stasino <> 'X' "
        If bGestDatecomp And strFlscdb <> "S" And (strAccperi = "S" Or strAccperi = "D") Then
          strSQL += " and mm_datini = " & CDataSQL(dDtIniz) & " AND mm_datfin = " & CDataSQL(dDtFin)
        End If
        strSQL = strSQL & " and tabcaum.tb_testci <> ' ' "

        If strRaggr = "N" Then
          strSQL += " and movmag.mm_tipork = '" & strTipork & "' " & _
                    " and movmag.mm_anno = " & nAnno & _
                    " and movmag.mm_serie = '" & strSerie & "' " & _
                    " And movmag.mm_numdoc = " & lNumdoc
        Else
          For Each dtrT As DataRow In dttTmOrig.Select("xx_dtttm = " & lIdRagg.ToString)
            strWhere += " (mm_tipork = " & CStrSQL(dtrT!tm_tipork) & " AND mm_anno = " & NTSCInt(dtrT!tm_anno) & " AND mm_serie = " & CStrSQL(dtrT!tm_serie) & " AND mm_numdoc = " & NTSCInt(dtrT!tm_numdoc) & ") OR "
          Next
          If strWhere.Length > 0 Then strWhere = strWhere.Substring(0, strWhere.Length - 4)

          strSQL += " AND (" & strWhere & ")"
        End If

        strSQL += " group by mm_contocontr, mm_codcena,  mm_codcfam, mm_commeca,  mm_subcommeca, ac_unmis, tb_testci, ac_codmaca " & _
                  " order by mm_codcena,   mm_codcfam, mm_commeca,  mm_subcommeca "
      End If
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetAccontiPerDifferenzeDiCambio(ByVal strDitta As String, ByVal strTipork As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                          ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tm_ccontr_1, tm_impcontv_1, mm_valorev, testmagACC.tm_cambio AS CAMBIONEW, testmag.tm_cambio AS CAMBIOOLD " & _
              "FROM ((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno " & _
              "AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc) " & _
              "INNER JOIN testmag as testmagACC ON testmagACC.codditt = movmag.codditt AND testmagACC.tm_tipork = movmag.mm_actipo AND " & _
              "testmagACC.tm_anno = movmag.mm_acanno AND testmagACC.tm_serie = movmag.mm_acserie AND " & _
              "testmagACC.tm_numdoc = movmag.mm_acnum) INNER JOIN testmagc on testmagACC.codditt = testmagc.codditt AND testmagACC.tm_tipork = testmagc.tm_tipork " & _
              "AND testmagACC.tm_anno = testmagc.tm_anno AND testmagACC.tm_serie = testmagc.tm_serie AND " & _
              "testmagACC.tm_numdoc = testmagc.tm_numdoc" & _
              " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
              " AND testmag.tm_tipork = '" & strTipork & "' AND testmag.tm_anno = " & nAnno & " AND " & _
              "testmag.tm_serie = '" & strSerie & "' AND testmag.tm_numdoc = " & lNumdoc & " " & _
              "ORDER BY tm_ccontr_1 ASC"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDdtPerStornoAcconti(ByVal strDitta As String, ByVal strTipork As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                          ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tm_fleleff, tm_datregef, tm_numregef, tm_tipork, tm_anno, tm_serie, tm_numdoc " & _
              " FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tm_tiporkfat = '" & strTipork & "'" & _
              " AND tm_annfat = " & nAnno & _
              " AND tm_alffat = '" & strSerie & "'" & _
              " AND tm_numfat = " & lNumdoc & _
              " AND tm_fleleff = 'S'" & _
              " AND tm_numregef <> 0"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function RielabGetNumpro2FromPrinot(ByVal strDitta As String, ByVal strDatreg As String, _
                                                    ByVal lNumreg As Integer, ByVal strTregiva As String, _
                                                    ByVal nRegiva As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT pn_numdoc, pn_numpro " & _
               " FROM prinot " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND pn_datreg = " & CDataSQL(strDatreg) & _
               " AND pn_numreg = " & lNumreg & _
               " AND pn_tregiva = " & CStrSQL(strTregiva) & _
               " AND pn_nregiva = " & nRegiva
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaRegistrazione(ByVal strDitta As String, ByVal strInt As String, _
                                                    ByVal strDatregf As String, ByVal lNumregf As Integer, _
                                                    ByVal strDatregin As String, ByVal lNumregin As Integer, _
                                                    ByVal strDatregom As String, ByVal lNumregom As Integer, _
                                                    ByVal bGestStanziamenti As Boolean, ByVal strTipork As String, _
                                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                                    ByVal lNumdoc As Integer, ByVal lContOld As Integer, _
                                                    ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                                    ByVal lNumdocOld As Integer, ByRef dbConn As DbConnection) As Boolean
    Try
      'obsoleta
      Return CancellaRegistrazione(strDitta, strInt, strDatregf, lNumregf, strDatregin, lNumregin, _
                                   strDatregom, lNumregom, bGestStanziamenti, strTipork, nAnno, strSerie, _
                                   lNumdoc, lContOld, nAnnoOld, strSerieOld, lNumdocOld, dbConn, Nothing, False, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CancellaRegistrazione(ByVal strDitta As String, ByVal strInt As String, _
                                                  ByVal strDatregf As String, ByVal lNumregf As Integer, _
                                                  ByVal strDatregin As String, ByVal lNumregin As Integer, _
                                                  ByVal strDatregom As String, ByVal lNumregom As Integer, _
                                                  ByVal bGestStanziamenti As Boolean, ByVal strTipork As String, _
                                                  ByVal nAnno As Integer, ByVal strSerie As String, _
                                                  ByVal lNumdoc As Integer, ByVal lContOld As Integer, _
                                                  ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                                  ByVal lNumdocOld As Integer, ByRef dbConn As DbConnection, _
                                                  ByRef dtrTm As DataRow) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strInt, strDatregf, lNumregf, strDatregin, lNumregin, _
                                             strDatregom, lNumregom, bGestStanziamenti, strTipork, nAnno, strSerie, _
                                             lNumdoc, lContOld, nAnnoOld, strSerieOld, lNumdocOld, dbConn, dtrTm})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dbConn = CType(oIn(17), DbConnection)
        dtrTm = CType(oIn(18), DataRow)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return CancellaRegistrazione(strDitta, strInt, strDatregf, lNumregf, strDatregin, lNumregin, _
                                   strDatregom, lNumregom, bGestStanziamenti, strTipork, nAnno, strSerie, _
                                   lNumdoc, lContOld, nAnnoOld, strSerieOld, lNumdocOld, dbConn, dtrTm, False, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CancellaRegistrazione(ByVal strDitta As String, ByVal strInt As String, _
                                                ByVal strDatregf As String, ByVal lNumregf As Integer, _
                                                ByVal strDatregin As String, ByVal lNumregin As Integer, _
                                                ByVal strDatregom As String, ByVal lNumregom As Integer, _
                                                ByVal bGestStanziamenti As Boolean, ByVal strTipork As String, _
                                                ByVal nAnno As Integer, ByVal strSerie As String, _
                                                ByVal lNumdoc As Integer, ByVal lContOld As Integer, _
                                                ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                                ByVal lNumdocOld As Integer, ByRef dbConn As DbConnection, _
                                                ByRef dtrTm As DataRow, ByVal bCa2 As Boolean) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strInt, strDatregf, lNumregf, strDatregin, lNumregin, _
                                             strDatregom, lNumregom, bGestStanziamenti, strTipork, nAnno, strSerie, _
                                             lNumdoc, lContOld, nAnnoOld, strSerieOld, lNumdocOld, dbConn, dtrTm, _
                                             bCa2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dbConn = CType(oIn(17), DbConnection)
        dtrTm = CType(oIn(18), DataRow)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return CancellaRegistrazione(strDitta, strInt, strDatregf, lNumregf, strDatregin, lNumregin, _
                                   strDatregom, lNumregom, bGestStanziamenti, strTipork, nAnno, strSerie, _
                                   lNumdoc, lContOld, nAnnoOld, strSerieOld, lNumdocOld, dbConn, dtrTm, bCa2, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CancellaRegistrazione(ByVal strDitta As String, ByVal strInt As String, _
                                                    ByVal strDatregf As String, ByVal lNumregf As Integer, _
                                                    ByVal strDatregin As String, ByVal lNumregin As Integer, _
                                                    ByVal strDatregom As String, ByVal lNumregom As Integer, _
                                                    ByVal bGestStanziamenti As Boolean, ByVal strTipork As String, _
                                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                                    ByVal lNumdoc As Integer, ByVal lContOld As Integer, _
                                                    ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                                    ByVal lNumdocOld As Integer, ByRef dbConn As DbConnection, _
                                                    ByRef dtrTm As DataRow, ByVal bCa2 As Boolean, _
                                                    ByVal bProfes As Boolean) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strInt, strDatregf, lNumregf, strDatregin, lNumregin, _
                                             strDatregom, lNumregom, bGestStanziamenti, strTipork, nAnno, strSerie, _
                                             lNumdoc, lContOld, nAnnoOld, strSerieOld, lNumdocOld, dbConn, dtrTm, _
                                             bCa2, bProfes})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dbConn = CType(oIn(17), DbConnection)
        dtrTm = CType(oIn(18), DataRow)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return CancellaRegistrazione(strDitta, strInt, strDatregf, lNumregf, strDatregin, lNumregin, _
                                   strDatregom, lNumregom, bGestStanziamenti, strTipork, nAnno, strSerie, _
                                   lNumdoc, lContOld, nAnnoOld, strSerieOld, lNumdocOld, dbConn, dtrTm, bCa2, bProfes, False, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CancellaRegistrazione(ByVal strDitta As String, ByVal strInt As String, _
                                                    ByVal strDatregf As String, ByVal lNumregf As Integer, _
                                                    ByVal strDatregin As String, ByVal lNumregin As Integer, _
                                                    ByVal strDatregom As String, ByVal lNumregom As Integer, _
                                                    ByVal bGestStanziamenti As Boolean, ByVal strTipork As String, _
                                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                                    ByVal lNumdoc As Integer, ByVal lContOld As Integer, _
                                                    ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                                    ByVal lNumdocOld As Integer, ByRef dbConn As DbConnection, _
                                                    ByRef dtrTm As DataRow, ByVal bCa2 As Boolean, _
                                                    ByVal bProfes As Boolean, ByVal bModDII As Boolean, _
                                                    ByVal bCollegaMG_DI As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim i As Integer = 0

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strInt, strDatregf, lNumregf, strDatregin, lNumregin, _
                                             strDatregom, lNumregom, bGestStanziamenti, strTipork, nAnno, strSerie, _
                                             lNumdoc, lContOld, nAnnoOld, strSerieOld, lNumdocOld, dbConn, dtrTm, _
                                             bCa2, bProfes, bModDII, bCollegaMG_DI})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dbConn = CType(oIn(17), DbConnection)
        dtrTm = CType(oIn(18), DataRow)
        Return CBool(oOut)
      End If
      '----------------

      If lContOld = -1 Then
        'cancellazione di documenti raggruppati: devo ricavare il conto dalla regsitrazione di CG, se c'è.
        'se non c'è vuol dire che il documento non è mai stato contabilizzato
        strSQL = "SELECT pn_conto, pn_annpar, pn_alfpar, pn_numpar FROM prinot" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND pn_riga = 1" & _
                 " AND pn_datreg = " & CDataSQL(NTSCDate(strDatregf)) & _
                 " AND pn_numreg = " & lNumregf.ToString

        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then
          lContOld = NTSCInt(dttTmp.Rows(0)!pn_conto)
          nAnnoOld = NTSCInt(dttTmp.Rows(0)!pn_annpar)
          strSerieOld = NTSCStr(dttTmp.Rows(0)!pn_alfpar)
          lNumdocOld = NTSCInt(dttTmp.Rows(0)!pn_numpar)
        End If
        dttTmp.Clear()
      End If

      If bProfes And lNumregf <> 0 And strTipork <> "3" Then
        If strTipork = "2" Then
          'correggo i riferimenti in prfatt
          strSQL = "UPDATE prfatt SET rx_flchiu = 'N', rx_nettoinc = rx_nettoinc - rxp_totincpag " & _
                  "FROM prfatt INNER JOIN princpag ON prfatt.codditt = princpag.codditt " & _
                  "AND prfatt.rx_datreg = princpag.rxp_datregr " & _
                  "AND prfatt.rx_numreg = princpag.rxp_numregr " & _
                  "AND prfatt.rx_rigareg = princpag.rxp_rigaregr " & _
                  "WHERE prfatt.codditt = " & CStrSQL(strDitta) & _
                  " AND rxp_datreg = " & CDataSQL(strDatregf) & _
                  " AND rxp_numreg = " & lNumregf & _
                  " AND rxp_rigareg = 1"
          i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'se è una nota di accred a storno fattura cancello solo princpag
          strSQL = "DELETE FROM princpag WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND rxp_datreg = " & CDataSQL(strDatregf) & _
                  " AND rxp_numreg = " & lNumregf & _
                  " AND rxp_rigareg = 1"
          i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Else
          'caso normale
          strSQL = "DELETE FROM princpag WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND rxp_datregr = " & CDataSQL(strDatregf) & _
                  " AND rxp_numregr = " & lNumregf & _
                  " AND rxp_rigareg = 1"
          i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM prfatt WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND rx_datreg = " & CDataSQL(strDatregf) & _
                  " AND rx_numreg = " & lNumregf & _
                  " AND rx_rigareg = 1"
          i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      End If   'If bProfes And dynTestmag!tm_numregef <> 0 Then

      If strTipork <> "3" Then
        strSQL = "DELETE FROM movusat WHERE mu_datregr = " & CDataSQL(strDatregf) & _
                  " AND mu_numregr = " & lNumregf.ToString & _
                  " AND codditt = " & CStrSQL(strDitta)
        i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        ' cancella le righe di moviva 1.a registrazione
        strSQL = "DELETE FROM MOVIVA WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND mi_datreg = " & CDataSQL(strDatregf) & _
                  " AND mi_numreg = " & lNumregf.ToString & _
                  " AND mi_integr = '" & strInt & "'"
        i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      '--------------------------------------------------------------------------------------------------------------
      'Dichiarazioni di intento
      If bModDII And bCollegaMG_DI Then
        'Passo i vecchi valori della dichiarazione...
        'In cancellazione, controlllo e passo i vecchi valori della dichiarazione di intento
        Dim dttOldDich As New DataTable
        strSQL = " SELECT pn_dianno, pn_dinumero, an_tipo" & _
                 " FROM prinot INNER JOIN anagra ON prinot.codditt = anagra.codditt AND prinot.pn_conto = anagra.an_conto" & _
                 " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
                 " AND pn_datreg = " & CDataSQL(strDatregf) & _
                 " AND pn_numreg = " & lNumregf.ToString & _
                 " AND pn_integr = '" & strInt & "' "
        dttOldDich = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttOldDich.Rows.Count > 0 Then
          If NTSCInt(dttOldDich.Rows(0)!pn_dianno) <> 0 And NTSCInt(dttOldDich.Rows(0)!pn_dinumero) <> 0 Then
            GestisciFlagChiusura("C", strDitta, dttOldDich.Rows(0)!an_tipo.ToString, NTSCInt(dttOldDich.Rows(0)!pn_dianno), NTSCInt(dttOldDich.Rows(0)!pn_dinumero), dbConn)
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------

      'adesso cancella reg. della prima e della seconda.. e le scadenze di quella partita
      strSQL = "DELETE princomp " & _
               " FROM princomp INNER JOIN prinot ON princomp.codditt = prinot.codditt " & _
               " AND princomp.pnc_datreg = prinot.pn_datreg " & _
               " AND princomp.pnc_numreg = prinot.pn_numreg " & _
               " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
               " AND pn_datreg = " & CDataSQL(strDatregf) & _
               " AND pn_numreg = " & lNumregf.ToString & _
               " AND pn_integr = '" & strInt & "' "
      i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = " DELETE FROM PRINOT" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND pn_datreg = " & CDataSQL(strDatregf) & _
                " AND pn_numreg = " & lNumregf.ToString & _
                " AND pn_integr = '" & strInt & "' "
      i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)


      'cancello la fattuura in analitica duplice contabile
      If bCa2 And lNumregf > 0 Then CancellaRegistrazione_CA2(dbConn, strDitta, strDatregf, lNumregf)

      'se c'e l'incasso...
      If lNumregin > 0 Then
        strSQL = "DELETE princomp " & _
                 " FROM princomp INNER JOIN prinot ON princomp.codditt = prinot.codditt " & _
                 " AND princomp.pnc_datreg = prinot.pn_datreg " & _
                 " AND princomp.pnc_numreg = prinot.pn_numreg " & _
                 " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
                 " AND pn_datreg = " & CDataSQL(strDatregin) & _
                 " AND pn_numreg = " & lNumregin.ToString & _
                 " AND pn_integr = '" & strInt & "' "

        i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "DELETE FROM PRINOT" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND pn_datreg = " & CDataSQL(strDatregin) & _
                  " AND pn_numreg = " & lNumregin.ToString & _
                  " AND pn_integr = '" & strInt & "' "
        i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'cancello l'incasso in analitica duplice contabile
        If bCa2 Then CancellaRegistrazione_CA2(dbConn, strDitta, strDatregin, lNumregin)

        'scaden: se avevo saldato delle scadenze, le riapro
        strSQL = "UPDATE scaden SET sc_flsaldato = 'N', sc_dtsaldato = null, sc_rgsaldato = 0" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND sc_flsaldato = 'S'" & _
                 " AND sc_dtsaldato = " & CDataSQL(strDatregin) & _
                 " AND sc_rgsaldato = " & lNumregin.ToString & _
                 " AND sc_integr = '" & strInt & "' "
        i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      'se c'e lo storno omaggi ...
      If lNumregom > 0 Then
        strSQL = "DELETE princomp " & _
                 " FROM princomp INNER JOIN prinot ON princomp.codditt = prinot.codditt " & _
                 " AND princomp.pnc_datreg = prinot.pn_datreg " & _
                 " AND princomp.pnc_numreg = prinot.pn_numreg " & _
                 " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
                 " AND pn_datreg = " & CDataSQL(strDatregom) & _
                 " AND pn_numreg = " & lNumregom.ToString & _
                 " AND pn_integr = '" & strInt & "' "

        i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "DELETE FROM PRINOT" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND pn_datreg = " & CDataSQL(strDatregom) & _
                  " AND pn_numreg = " & lNumregom.ToString & _
                  " AND pn_integr = '" & strInt & "' "
        i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'cancello lo storno omaggi in analitica duplice contabile
        If bCa2 Then CancellaRegistrazione_CA2(dbConn, strDitta, strDatregom, lNumregom)
      End If

      'registrazione e scadenziario
      'scadenziario.. (tutte le righe di quella partita ...
      If lContOld <> -1 Then
        strSQL = "DELETE FROM SCADEN" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND sc_conto = " & lContOld & _
                  " AND sc_annpar = " & nAnnoOld & _
                  " AND sc_alfpar = '" & strSerieOld & "' " & _
                  " AND sc_numpar = " & lNumdocOld & _
                  " AND sc_integr = '" & strInt & "' "
        i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      'scaden: se avevo saldato delle scadenze, le riapro 
      '(per ora è solo il caso di note accred emesse a storno fattura,
      'dove la compensaz. del clie nota accred con cli fattura è tutta nella stessa registraz. della nota accred emessa)
      If strTipork = "N" Then
        strSQL = "UPDATE scaden SET sc_flsaldato = 'N', sc_dtsaldato = null, sc_rgsaldato = 0" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND sc_flsaldato = 'S'" & _
                 " AND sc_dtsaldato = " & CDataSQL(strDatregf) & _
                 " AND sc_rgsaldato = " & lNumregf.ToString & _
                 " AND sc_integr = '" & strInt & "' "
        i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      'se contab. analitica, anche priana
      strSQL = "DELETE FROM PRIANA" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND pa_datreg = " & CDataSQL(strDatregf) & _
                " and pa_numreg = " & lNumregf.ToString & _
                " and pa_integr = '" & strInt & "' "
      i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'cancello l'abbuono in analitica
      If lNumregom > 0 Then
        strSQL = "DELETE FROM PRIANA" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND pa_datreg = " & CDataSQL(strDatregom) & _
                  " and pa_numreg = " & lNumregom.ToString & _
                  " and pa_integr = '" & strInt & "' "
        i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      'cancello eventuali Stanziamenti
      'per ogni fattura differita emessa/ricevuta
      'verifico se nei ddt collegati sono stati generati degli storni di stanziamenti
      'se ci sono cancello la registrazione di storno ed aggiorno testmag
      If bGestStanziamenti And (strTipork = "D" Or strTipork = "K" Or strTipork = "£" Or strTipork = "(") Then
        dttTmp.Clear()
        strSQL = "SELECT tm_datregin, tm_numrgin " & _
                " FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
                " AND tm_tiporkfat = '" & strTipork & "'" & _
                " AND tm_annfat = " & nAnno.ToString & _
                " AND tm_alffat = '" & strSerie & "'" & _
                " AND tm_numfat = " & lNumdoc.ToString & _
                " AND tm_flelvuo = 'S'" & _
                " AND tm_numrgin <> 0"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        For i = 0 To dttTmp.Rows.Count - 1
          'cancello prinot
          strSQL = "DELETE princomp " & _
                   " FROM princomp INNER JOIN prinot ON princomp.codditt = prinot.codditt " & _
                   " AND princomp.pnc_datreg = prinot.pn_datreg " & _
                   " AND princomp.pnc_numreg = prinot.pn_numreg " & _
                   " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
                   " AND pn_datreg = " & CDataSQL(strDatregin) & _
                   " AND pn_numreg = " & lNumregin.ToString & _
                   " AND pn_integr = '" & strInt & "'"

          i = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          strSQL = "DELETE FROM prinot " & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND pn_datreg = " & CDataSQL(strDatregin) & _
                  " AND pn_numreg = " & lNumregin.ToString & _
                  " AND pn_integr = '" & strInt & "'"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'cancello priana
          strSQL = "DELETE FROM priana " & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND pa_datreg = " & CDataSQL(strDatregin) & _
                  " and pa_numreg = " & lNumregin.ToString & _
                  " and pa_integr = '" & strInt & "' "
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
        dttTmp.Clear()
      End If    'If bGestStanziamenti And dtrTm!tm_tipork.ToString <> "D" And dtrTm!tm_tipork.ToString <> "K" Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function CancellaRegistrazione_CA2(ByRef dbConn As DbConnection, ByVal strDitta As String, _
                                                        ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim strProgr As String = "" 'elenco delle righe di priana2 da cancellare
    Dim lTmp As Decimal = 0
    Try

      strSQL = "SELECT DISTINCT pa2_progr, pa2_datreg, pa2_numreg, pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc " & _
               " FROM priana2 " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " And pa2_datreg = " & CDataSQL(strDatreg) & _
               " And pa2_numreg = " & lNumreg.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return True

      For Each dtrT1 As DataRow In dttTmp.Rows
        If lTmp <> NTSCDec(dtrT1!pa2_progr) Then
          'il progr delle righe di cadc è doppio: mi basta prendere quello della riga 1
          strProgr += CDblSQL(NTSCDec(dtrT1!pa2_progr)) & ", "
          lTmp = NTSCDec(dtrT1!pa2_progr)
        End If
      Next

      dtrT = dttTmp.Select("pa2_mmtipork = 'T'")
      If dtrT.Length > 0 Then
        'se la registrazione è collegata ad un carico da produzione, 
        'devo rimuovere prima le righe del carico da produzione (scarichi collegati e qta prodotta):
        'su queste righe non c'è il collegamento a pa2_datreg e pa2_numreg, mi baso su pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc
        Dim oDttgr As New CLEGROUPBY
        Dim dttGr As New DataTable
        dttGr.Clear()
        If Not oDttgr.NTSGroupBy(dttTmp, dttGr, "pa2_mmanno, pa2_mmserie, pa2_mmnumdoc", _
                                                "pa2_mmtipork = 'T'", _
                                                "pa2_mmanno, pa2_mmserie, pa2_mmnumdoc") Then
          Return False
        End If
        For Each dtrT1 As DataRow In dttGr.Rows
          strSQL = "SELECT DISTINCT pa2_progr " & _
                   " FROM priana2 " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " And pa2_mmtiporkt = 'T'" & _
                   " And pa2_mmanno = " & dtrT1!pa2_mmanno.ToString & _
                   " And pa2_mmserie = " & CStrSQL(dtrT1!pa2_mmserie) & _
                   " And pa2_mmnumdoc = " & dtrT1!pa2_mmnumdoc.ToString
          dttTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          For Each dtrT2 As DataRow In dttTmp2.Rows
            strProgr += CDblSQL(NTSCDec(dtrT2!pa2_progr)) & ", "
          Next
          dttTmp2.Clear()
        Next
      End If

      If strProgr.Trim = "" Then Return True
      strProgr = strProgr.Substring(0, strProgr.Length - 2)

      '------------------------------------
      'CANCELLO LE EVENTUALI RIGHE DI PRIA2COMP
      strSQL = "DELETE pria2comp WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND pac_progr IN (" & strProgr & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '------------------------------------
      'CANCELLO LE EVENTUALI RIGHE DI PRIANA2
      strSQL = "DELETE priana2 WHERE codditt = " & CStrSQL(strDitta) & " and pa2_progr IN (" & strProgr & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaRegistrazione(ByVal strDitta As String, ByRef dttPN As DataTable, ByRef dttMO As DataTable, _
                                              ByRef dttSC As DataTable, ByRef dttCA As DataTable, ByVal strDatreg As String, _
                                              ByRef dtrTm As DataRow, ByVal strInt As String, _
                                              ByVal strDatregfOld As String, ByVal lNumregfOld As Integer, _
                                              ByVal strDatreginOld As String, ByVal lNumreginOld As Integer, _
                                              ByVal strDatregomOld As String, ByVal lNumregomOld As Integer, _
                                              ByVal bGestStanziamenti As Boolean, ByVal lContOld As Integer, _
                                              ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                              ByVal lNumdocOld As Integer, ByVal bStornaStanziamenti As Boolean, _
                                              ByVal lNumregStanziamenti As Integer, ByVal nCauststanz As Integer, _
                                              ByVal strCausaleStornoStanz As String, ByRef strErr As String, _
                                              ByVal nAnnoDoc As Integer, ByVal strAlfdoc As String, ByVal lNumdoc As Integer, _
                                              ByVal bAggProtP As Boolean, ByVal strAlfdocP As String, ByVal lNumdocP As Integer, _
                                              ByVal bAggProtP2 As Boolean, ByVal strAlfdocP2 As String, ByVal lNumdocP2 As Integer) As Boolean
    Try
      'obsoleta
      Return CreaRegistrazione(strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, strDatregfOld, lNumregfOld, _
                               strDatreginOld, lNumreginOld, strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                               nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, lNumregStanziamenti, nCauststanz, _
                               strCausaleStornoStanz, strErr, nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                               bAggProtP2, strAlfdocP2, lNumdocP2, "A", Nothing, False, Nothing, False, "N", Nothing, False, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaRegistrazione(ByVal strDitta As String, ByRef dttPN As DataTable, ByRef dttMO As DataTable, _
                                              ByRef dttSC As DataTable, ByRef dttCA As DataTable, ByVal strDatreg As String, _
                                              ByRef dtrTm As DataRow, ByVal strInt As String, _
                                              ByVal strDatregfOld As String, ByVal lNumregfOld As Integer, _
                                              ByVal strDatreginOld As String, ByVal lNumreginOld As Integer, _
                                              ByVal strDatregomOld As String, ByVal lNumregomOld As Integer, _
                                              ByVal bGestStanziamenti As Boolean, ByVal lContOld As Integer, _
                                              ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                              ByVal lNumdocOld As Integer, ByVal bStornaStanziamenti As Boolean, _
                                              ByVal lNumregStanziamenti As Integer, ByVal nCauststanz As Integer, _
                                              ByVal strCausaleStornoStanz As String, ByRef strErr As String, _
                                              ByVal nAnnoDoc As Integer, ByVal strAlfdoc As String, ByVal lNumdoc As Integer, _
                                              ByVal bAggProtP As Boolean, ByVal strAlfdocP As String, ByVal lNumdocP As Integer, _
                                              ByVal bAggProtP2 As Boolean, ByVal strAlfdocP2 As String, ByVal lNumdocP2 As Integer, _
                                              ByVal strTipoNumerazVendPerAcqIntra As String) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, _
                                             strDatregfOld, lNumregfOld, strDatreginOld, lNumreginOld, _
                                             strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                                             nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, _
                                             lNumregStanziamenti, nCauststanz, strCausaleStornoStanz, strErr, _
                                             nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                                             bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttPN = CType(oIn(1), DataTable)
        dttMO = CType(oIn(2), DataTable)
        dttSC = CType(oIn(3), DataTable)
        dttCA = CType(oIn(4), DataTable)
        dtrTm = CType(oIn(6), DataRow)
        strErr = NTSCStr(oIn(23))
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return CreaRegistrazione(strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, strDatregfOld, lNumregfOld, _
                               strDatreginOld, lNumreginOld, strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                               nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, lNumregStanziamenti, nCauststanz, _
                               strCausaleStornoStanz, strErr, nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                               bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, Nothing, False, Nothing, _
                               False, "N", Nothing, False, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaRegistrazione(ByVal strDitta As String, ByRef dttPN As DataTable, ByRef dttMO As DataTable, _
                                            ByRef dttSC As DataTable, ByRef dttCA As DataTable, ByVal strDatreg As String, _
                                            ByRef dtrTm As DataRow, ByVal strInt As String, _
                                            ByVal strDatregfOld As String, ByVal lNumregfOld As Integer, _
                                            ByVal strDatreginOld As String, ByVal lNumreginOld As Integer, _
                                            ByVal strDatregomOld As String, ByVal lNumregomOld As Integer, _
                                            ByVal bGestStanziamenti As Boolean, ByVal lContOld As Integer, _
                                            ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                            ByVal lNumdocOld As Integer, ByVal bStornaStanziamenti As Boolean, _
                                            ByVal lNumregStanziamenti As Integer, ByVal nCauststanz As Integer, _
                                            ByVal strCausaleStornoStanz As String, ByRef strErr As String, _
                                            ByVal nAnnoDoc As Integer, ByVal strAlfdoc As String, ByVal lNumdoc As Integer, _
                                            ByVal bAggProtP As Boolean, ByVal strAlfdocP As String, ByVal lNumdocP As Integer, _
                                            ByVal bAggProtP2 As Boolean, ByVal strAlfdocP2 As String, ByVal lNumdocP2 As Integer, _
                                            ByVal strTipoNumerazVendPerAcqIntra As String, ByRef dttCa2 As DataTable, _
                                            ByVal bCa2 As Boolean) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, _
                                             strDatregfOld, lNumregfOld, strDatreginOld, lNumreginOld, _
                                             strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                                             nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, _
                                             lNumregStanziamenti, nCauststanz, strCausaleStornoStanz, strErr, _
                                             nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                                             bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, _
                                             dttCa2, bCa2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttPN = CType(oIn(1), DataTable)
        dttMO = CType(oIn(2), DataTable)
        dttSC = CType(oIn(3), DataTable)
        dttCA = CType(oIn(4), DataTable)
        dtrTm = CType(oIn(6), DataRow)
        strErr = NTSCStr(oIn(23))
        dttCa2 = CType(oIn(34), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return CreaRegistrazione(strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, strDatregfOld, lNumregfOld, _
                               strDatreginOld, lNumreginOld, strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                               nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, lNumregStanziamenti, nCauststanz, _
                               strCausaleStornoStanz, strErr, nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                               bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, dttCa2, bCa2, Nothing, _
                               False, "N", Nothing, False, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaRegistrazione(ByVal strDitta As String, ByRef dttPN As DataTable, ByRef dttMO As DataTable, _
                                          ByRef dttSC As DataTable, ByRef dttCA As DataTable, ByVal strDatreg As String, _
                                          ByRef dtrTm As DataRow, ByVal strInt As String, _
                                          ByVal strDatregfOld As String, ByVal lNumregfOld As Integer, _
                                          ByVal strDatreginOld As String, ByVal lNumreginOld As Integer, _
                                          ByVal strDatregomOld As String, ByVal lNumregomOld As Integer, _
                                          ByVal bGestStanziamenti As Boolean, ByVal lContOld As Integer, _
                                          ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                          ByVal lNumdocOld As Integer, ByVal bStornaStanziamenti As Boolean, _
                                          ByVal lNumregStanziamenti As Integer, ByVal nCauststanz As Integer, _
                                          ByVal strCausaleStornoStanz As String, ByRef strErr As String, _
                                          ByVal nAnnoDoc As Integer, ByVal strAlfdoc As String, ByVal lNumdoc As Integer, _
                                          ByVal bAggProtP As Boolean, ByVal strAlfdocP As String, ByVal lNumdocP As Integer, _
                                          ByVal bAggProtP2 As Boolean, ByVal strAlfdocP2 As String, ByVal lNumdocP2 As Integer, _
                                          ByVal strTipoNumerazVendPerAcqIntra As String, ByRef dttCa2 As DataTable, _
                                          ByVal bCa2 As Boolean, ByRef dttTmOrig As DataTable, ByVal bProfes As Boolean, _
                                          ByVal strFattRevCharge As String) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, _
                                             strDatregfOld, lNumregfOld, strDatreginOld, lNumreginOld, _
                                             strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                                             nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, _
                                             lNumregStanziamenti, nCauststanz, strCausaleStornoStanz, strErr, _
                                             nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                                             bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, _
                                             dttCa2, bCa2, dttTmOrig, bProfes, strFattRevCharge})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttPN = CType(oIn(1), DataTable)
        dttMO = CType(oIn(2), DataTable)
        dttSC = CType(oIn(3), DataTable)
        dttCA = CType(oIn(4), DataTable)
        dtrTm = CType(oIn(6), DataRow)
        strErr = NTSCStr(oIn(23))
        dttCa2 = CType(oIn(34), DataTable)
        dttTmOrig = CType(oIn(36), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return CreaRegistrazione(strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, strDatregfOld, lNumregfOld, _
                               strDatreginOld, lNumreginOld, strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                               nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, lNumregStanziamenti, nCauststanz, _
                               strCausaleStornoStanz, strErr, nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                               bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, dttCa2, bCa2, dttTmOrig, _
                               bProfes, strFattRevCharge, Nothing, False, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaRegistrazione(ByVal strDitta As String, ByRef dttPN As DataTable, ByRef dttMO As DataTable, _
                                                ByRef dttSC As DataTable, ByRef dttCA As DataTable, ByVal strDatreg As String, _
                                                ByRef dtrTm As DataRow, ByVal strInt As String, _
                                                ByVal strDatregfOld As String, ByVal lNumregfOld As Integer, _
                                                ByVal strDatreginOld As String, ByVal lNumreginOld As Integer, _
                                                ByVal strDatregomOld As String, ByVal lNumregomOld As Integer, _
                                                ByVal bGestStanziamenti As Boolean, ByVal lContOld As Integer, _
                                                ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                                ByVal lNumdocOld As Integer, ByVal bStornaStanziamenti As Boolean, _
                                                ByVal lNumregStanziamenti As Integer, ByVal nCauststanz As Integer, _
                                                ByVal strCausaleStornoStanz As String, ByRef strErr As String, _
                                                ByVal nAnnoDoc As Integer, ByVal strAlfdoc As String, ByVal lNumdoc As Integer, _
                                                ByVal bAggProtP As Boolean, ByVal strAlfdocP As String, ByVal lNumdocP As Integer, _
                                                ByVal bAggProtP2 As Boolean, ByVal strAlfdocP2 As String, ByVal lNumdocP2 As Integer, _
                                                ByVal strTipoNumerazVendPerAcqIntra As String, ByRef dttCa2 As DataTable, _
                                                ByVal bCa2 As Boolean, ByRef dttTmOrig As DataTable, ByVal bProfes As Boolean, _
                                                ByVal strFattRevCharge As String, ByRef dttScadRielab As DataTable, _
                                                ByVal bGiroEffettiNoChisCli As Boolean, ByVal lContoEff As Integer) As Boolean
    
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, _
                                             strDatregfOld, lNumregfOld, strDatreginOld, lNumreginOld, _
                                             strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                                             nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, _
                                             lNumregStanziamenti, nCauststanz, strCausaleStornoStanz, strErr, _
                                             nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                                             bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, _
                                             dttCa2, bCa2, dttTmOrig, bProfes, strFattRevCharge, dttScadRielab, bGiroEffettiNoChisCli, lContoEff})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttPN = CType(oIn(1), DataTable)
        dttMO = CType(oIn(2), DataTable)
        dttSC = CType(oIn(3), DataTable)
        dttCA = CType(oIn(4), DataTable)
        dtrTm = CType(oIn(6), DataRow)
        strErr = NTSCStr(oIn(23))
        dttCa2 = CType(oIn(34), DataTable)
        dttTmOrig = CType(oIn(36), DataTable)
        dttScadRielab = CType(oIn(39), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return CreaRegistrazione(strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, strDatregfOld, lNumregfOld, _
                               strDatreginOld, lNumreginOld, strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                               nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, lNumregStanziamenti, nCauststanz, _
                               strCausaleStornoStanz, strErr, nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                               bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, dttCa2, bCa2, dttTmOrig, _
                               bProfes, strFattRevCharge, dttScadRielab, bGiroEffettiNoChisCli, lContoEff, False, False, _
                               Nothing, 0, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaRegistrazione(ByVal strDitta As String, ByRef dttPN As DataTable, ByRef dttMO As DataTable, _
                                              ByRef dttSC As DataTable, ByRef dttCA As DataTable, ByVal strDatreg As String, _
                                              ByRef dtrTm As DataRow, ByVal strInt As String, _
                                              ByVal strDatregfOld As String, ByVal lNumregfOld As Integer, _
                                              ByVal strDatreginOld As String, ByVal lNumreginOld As Integer, _
                                              ByVal strDatregomOld As String, ByVal lNumregomOld As Integer, _
                                              ByVal bGestStanziamenti As Boolean, ByVal lContOld As Integer, _
                                              ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                              ByVal lNumdocOld As Integer, ByVal bStornaStanziamenti As Boolean, _
                                              ByVal lNumregStanziamenti As Integer, ByVal nCauststanz As Integer, _
                                              ByVal strCausaleStornoStanz As String, ByRef strErr As String, _
                                              ByVal nAnnoDoc As Integer, ByVal strAlfdoc As String, ByVal lNumdoc As Integer, _
                                              ByVal bAggProtP As Boolean, ByVal strAlfdocP As String, ByVal lNumdocP As Integer, _
                                              ByVal bAggProtP2 As Boolean, ByVal strAlfdocP2 As String, ByVal lNumdocP2 As Integer, _
                                              ByVal strTipoNumerazVendPerAcqIntra As String, ByRef dttCa2 As DataTable, _
                                              ByVal bCa2 As Boolean, ByRef dttTmOrig As DataTable, ByVal bProfes As Boolean, _
                                              ByVal strFattRevCharge As String, ByRef dttScadRielab As DataTable, _
                                              ByVal bGiroEffettiNoChisCli As Boolean, ByVal lContoEff As Integer, _
                                              ByVal bModDII As Boolean, ByVal bCollegaMG_DI As Boolean) As Boolean

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, _
                                             strDatregfOld, lNumregfOld, strDatreginOld, lNumreginOld, _
                                             strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                                             nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, _
                                             lNumregStanziamenti, nCauststanz, strCausaleStornoStanz, strErr, _
                                             nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                                             bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, _
                                             dttCa2, bCa2, dttTmOrig, bProfes, strFattRevCharge, dttScadRielab, _
                                             bGiroEffettiNoChisCli, lContoEff, bModDII, bCollegaMG_DI})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttPN = CType(oIn(1), DataTable)
        dttMO = CType(oIn(2), DataTable)
        dttSC = CType(oIn(3), DataTable)
        dttCA = CType(oIn(4), DataTable)
        dtrTm = CType(oIn(6), DataRow)
        strErr = NTSCStr(oIn(23))
        dttCa2 = CType(oIn(34), DataTable)
        dttTmOrig = CType(oIn(36), DataTable)
        dttScadRielab = CType(oIn(39), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return CreaRegistrazione(strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, strDatregfOld, lNumregfOld, _
                               strDatreginOld, lNumreginOld, strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                               nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, lNumregStanziamenti, nCauststanz, _
                               strCausaleStornoStanz, strErr, nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                               bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, dttCa2, bCa2, dttTmOrig, _
                               bProfes, strFattRevCharge, dttScadRielab, bGiroEffettiNoChisCli, lContoEff, bModDII, bCollegaMG_DI, _
                               Nothing, 0, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaRegistrazione(ByVal strDitta As String, ByRef dttPN As DataTable, ByRef dttMO As DataTable, _
                                                ByRef dttSC As DataTable, ByRef dttCA As DataTable, ByVal strDatreg As String, _
                                                ByRef dtrTm As DataRow, ByVal strInt As String, _
                                                ByVal strDatregfOld As String, ByVal lNumregfOld As Integer, _
                                                ByVal strDatreginOld As String, ByVal lNumreginOld As Integer, _
                                                ByVal strDatregomOld As String, ByVal lNumregomOld As Integer, _
                                                ByVal bGestStanziamenti As Boolean, ByVal lContOld As Integer, _
                                                ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                                ByVal lNumdocOld As Integer, ByVal bStornaStanziamenti As Boolean, _
                                                ByVal lNumregStanziamenti As Integer, ByVal nCauststanz As Integer, _
                                                ByVal strCausaleStornoStanz As String, ByRef strErr As String, _
                                                ByVal nAnnoDoc As Integer, ByVal strAlfdoc As String, ByVal lNumdoc As Integer, _
                                                ByVal bAggProtP As Boolean, ByVal strAlfdocP As String, ByVal lNumdocP As Integer, _
                                                ByVal bAggProtP2 As Boolean, ByVal strAlfdocP2 As String, ByVal lNumdocP2 As Integer, _
                                                ByVal strTipoNumerazVendPerAcqIntra As String, ByRef dttCa2 As DataTable, _
                                                ByVal bCa2 As Boolean, ByRef dttTmOrig As DataTable, ByVal bProfes As Boolean, _
                                                ByVal strFattRevCharge As String, ByRef dttScadRielab As DataTable, _
                                                ByVal bGiroEffettiNoChisCli As Boolean, ByVal lContoEff As Integer, ByVal bModDII As Boolean, _
                                                ByVal bCollegaMG_DI As Boolean, ByRef dsCove As DataSet, ByVal nAggControp As Integer) As Boolean

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, _
                                             strDatregfOld, lNumregfOld, strDatreginOld, lNumreginOld, _
                                             strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                                             nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, _
                                             lNumregStanziamenti, nCauststanz, strCausaleStornoStanz, strErr, _
                                             nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                                             bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, _
                                             dttCa2, bCa2, dttTmOrig, bProfes, strFattRevCharge, dttScadRielab, _
                                             bGiroEffettiNoChisCli, lContoEff, bModDII, bCollegaMG_DI, _
                                             dsCove, nAggControp})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttPN = CType(oIn(1), DataTable)
        dttMO = CType(oIn(2), DataTable)
        dttSC = CType(oIn(3), DataTable)
        dttCA = CType(oIn(4), DataTable)
        dtrTm = CType(oIn(6), DataRow)
        strErr = NTSCStr(oIn(23))
        dttCa2 = CType(oIn(34), DataTable)
        dttTmOrig = CType(oIn(36), DataTable)
        dttScadRielab = CType(oIn(39), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return CreaRegistrazione(strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, strDatregfOld, lNumregfOld, _
                               strDatreginOld, lNumreginOld, strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                               nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, lNumregStanziamenti, nCauststanz, _
                               strCausaleStornoStanz, strErr, nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                               bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, dttCa2, bCa2, dttTmOrig, _
                               bProfes, strFattRevCharge, dttScadRielab, bGiroEffettiNoChisCli, lContoEff, bModDII, bCollegaMG_DI, _
                               dsCove, nAggControp, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaRegistrazione(ByVal strDitta As String, ByRef dttPN As DataTable, ByRef dttMO As DataTable, _
                                                ByRef dttSC As DataTable, ByRef dttCA As DataTable, ByVal strDatreg As String, _
                                                ByRef dtrTm As DataRow, ByVal strInt As String, _
                                                ByVal strDatregfOld As String, ByVal lNumregfOld As Integer, _
                                                ByVal strDatreginOld As String, ByVal lNumreginOld As Integer, _
                                                ByVal strDatregomOld As String, ByVal lNumregomOld As Integer, _
                                                ByVal bGestStanziamenti As Boolean, ByVal lContOld As Integer, _
                                                ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                                ByVal lNumdocOld As Integer, ByVal bStornaStanziamenti As Boolean, _
                                                ByVal lNumregStanziamenti As Integer, ByVal nCauststanz As Integer, _
                                                ByVal strCausaleStornoStanz As String, ByRef strErr As String, _
                                                ByVal nAnnoDoc As Integer, ByVal strAlfdoc As String, ByVal lNumdoc As Integer, _
                                                ByVal bAggProtP As Boolean, ByVal strAlfdocP As String, ByVal lNumdocP As Integer, _
                                                ByVal bAggProtP2 As Boolean, ByVal strAlfdocP2 As String, ByVal lNumdocP2 As Integer, _
                                                ByVal strTipoNumerazVendPerAcqIntra As String, ByRef dttCa2 As DataTable, _
                                                ByVal bCa2 As Boolean, ByRef dttTmOrig As DataTable, ByVal bProfes As Boolean, _
                                                ByVal strFattRevCharge As String, ByRef dttScadRielab As DataTable, _
                                                ByVal bGiroEffettiNoChisCli As Boolean, ByVal lContoEff As Integer, ByVal bModDII As Boolean, _
                                                ByVal bCollegaMG_DI As Boolean, ByRef dsCove As DataSet, ByVal nAggControp As Integer, _
                                                ByVal bRevChargeNumdocDaPartita As Boolean) As Boolean
    '------------------------------
    'creala registrazione di prima nota in base allo schema
    'cliente in avere 
    'conto effetti attivi in dare
    'crea la scadenza sugli effetti attivi
    'dichiara saldata la scadenza sul cliente
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim dbConn As DbConnection = Nothing
    Dim lNumreg As Integer = 0              'numero reg. fattura/nota accred
    Dim lNumregInc As Integer = 0           'numero reg. incasso
    Dim dttDDT As New DataTable
    Dim dttTmp1 As New DataTable
    Dim dttAnaditac As New DataTable
    Dim lIITtkeys As Integer = 0
    Dim strWhere As String = ""
    Dim bParcellazione As Boolean = False
    Dim strT() As String = Nothing
    Dim oDttgr As CLEGROUPBY = Nothing
    Dim dttGr As DataTable = Nothing
    Dim dtrDataRow As DataRow()
    Dim nConto As Integer = 0
    Dim nAnno As Integer = 0
    Dim nNumero As Integer = 0
    Dim dttAnagra As New DataTable
    Dim strTipo As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, strInt, _
                                             strDatregfOld, lNumregfOld, strDatreginOld, lNumreginOld, _
                                             strDatregomOld, lNumregomOld, bGestStanziamenti, lContOld, _
                                             nAnnoOld, strSerieOld, lNumdocOld, bStornaStanziamenti, _
                                             lNumregStanziamenti, nCauststanz, strCausaleStornoStanz, strErr, _
                                             nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                                             bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, _
                                             dttCa2, bCa2, dttTmOrig, bProfes, strFattRevCharge, dttScadRielab, _
                                             bGiroEffettiNoChisCli, lContoEff, bModDII, bCollegaMG_DI, dsCove, _
                                             nAggControp, bRevChargeNumdocDaPartita})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttPN = CType(oIn(1), DataTable)
        dttMO = CType(oIn(2), DataTable)
        dttSC = CType(oIn(3), DataTable)
        dttCA = CType(oIn(4), DataTable)
        dtrTm = CType(oIn(6), DataRow)
        strErr = NTSCStr(oIn(23))
        dttCa2 = CType(oIn(34), DataTable)
        dttTmOrig = CType(oIn(36), DataTable)
        dttScadRielab = CType(oIn(39), DataTable)
        dsCove = CType(oIn(44), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      strErr = ""

      Select Case NTSCStr(dtrTm!tm_tipork)
        Case "1", "2", "3", "4", "5"
          'parcellazione
          bParcellazione = True
        Case Else
          'documenti di magazzino
          bParcellazione = False
      End Select

      If bCa2 Then lIITtkeys = GetTblInstId("TTKEYS", False)

      lNumreg = NTSCInt(dtrTm!tm_numregef)    'num. registraz. della fattura
      lNumregInc = NTSCInt(dtrTm!tm_numrgin)  'num. registraz. incasso/pagamento

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      '---------------------------------
      If Not dttScadRielab Is Nothing AndAlso dttScadRielab.Columns.Contains("xx_operaz") Then
        'se devo cancellare la reg. di emissione effetti (non presentati in banca) lo faccio ora
        For Each dtrT As DataRow In dttScadRielab.Select("xx_operaz like 'DEL;%'")
          strT = NTSCStr(dtrT!xx_operaz).Split(";"c)
          If strT(0) = "DEL" And strT(1) = "CG" And NTSCStr(dtrT!xx_tipo) = "EFF" Then
            If Not DeleteRegEmissEff(strDitta, strT(2), NTSCInt(strT(3)), strInt, bGiroEffettiNoChisCli, lContoEff, dbConn) Then Return False
            dtrT.Delete()
          End If
        Next
        dttScadRielab.AcceptChanges()

        'se devo cancellare le vecchie scadenze NON SALDATE perchè sostituite da nuove
        For Each dtrT As DataRow In dttScadRielab.Select("(xx_operaz = 'DEL;SC;' AND xx_mantieni = 'N') OR xx_tipo = 'INC' OR xx_tipo = 'OMA'")
          strSQL = "DELETE FROM scaden " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND sc_conto = " & dtrT!sc_conto.ToString & _
                   " AND sc_annpar = " & dtrT!sc_annpar.ToString & _
                   " AND sc_alfpar = " & CStrSQL(dtrT!sc_alfpar.ToString) & _
                   " AND sc_numpar = " & dtrT!sc_numpar.ToString & _
                   " AND sc_numrata = " & dtrT!sc_numrata.ToString & _
                   " AND sc_integr = " & CStrSQL(strInt)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          dtrT.Delete()
        Next
        dttScadRielab.AcceptChanges()
      End If    'If Not dttScadRielab Is Nothing AndAlso dttScadRielab.Columns.Contains("xx_operaz") Then


      '---------------------------------
      'se devo prima cancello la vecchia registrazione
      If (lNumregfOld <> 0 And strDatregfOld <> "") Or _
         (lNumreginOld <> 0 And strDatreginOld <> "") Or _
         (lNumregomOld <> 0 And strDatregomOld <> "") Then
        If Not CancellaRegistrazione(strDitta, strInt, strDatregfOld, lNumregfOld, strDatreginOld, lNumreginOld, _
                                     strDatregomOld, lNumregomOld, bGestStanziamenti, dtrTm!tm_tipork.ToString, _
                                     NTSCInt(dtrTm!tm_anno), dtrTm!tm_serie.ToString, NTSCInt(dtrTm!tm_numdoc), _
                                     lContOld, nAnnoOld, strSerieOld, lNumdocOld, dbConn, dtrTm, bCa2, bProfes, bModDII, _
                                     bCollegaMG_DI) Then Return False
      End If

      'se è un documento raggruppato, devo cancellare le registrazioni dei singoli corrispettivi/ricevute fiscali emesse
      If NTSCStr(dtrTm!xx_raggr) = "S" Then
        For Each dtrT As DataRow In dttTmOrig.Select("xx_dtttm = " & NTSCInt(dtrTm!xx_dtttm).ToString)
          If (NTSCInt(dtrT!tm_numregef) <> 0 And NTSCStr(dtrT!tm_datregef) <> "") Or _
             (NTSCInt(dtrT!tm_numrgin) <> 0 And NTSCStr(dtrT!tm_datregin) <> "") Or _
             (NTSCInt(dtrT!tm_numregom) <> 0 And NTSCStr(dtrT!tm_datregom) <> "") Then
            If Not CancellaRegistrazione(strDitta, strInt, NTSCStr(dtrT!tm_datregef), NTSCInt(dtrT!tm_numregef), _
                                         NTSCStr(dtrT!tm_datregin), NTSCInt(dtrT!tm_numrgin), _
                                         NTSCStr(dtrT!tm_datregom), NTSCInt(dtrT!tm_numregom), _
                                         bGestStanziamenti, dtrTm!tm_tipork.ToString, _
                                         NTSCInt(dtrTm!tm_anno), dtrTm!tm_serie.ToString, NTSCInt(dtrTm!tm_numdoc), _
                                         -1, 0, "", 0, dbConn, dtrTm, bCa2, bProfes, bModDII, _
                                         bCollegaMG_DI) Then Return False
          End If
          'ora sui documenti di origine devo mettere gli stessi estremi di registrazione del documento raggruppato
          dtrT!tm_flcont = "S"
          dtrT!tm_datregef = dtrTm!tm_datregef
          dtrT!tm_numregef = dtrTm!tm_numregef
          dtrT!tm_numprot = dtrTm!tm_numprot
          dtrT!tm_nregiva = dtrTm!tm_nregiva
          dtrT!tm_numdoc2 = 0
          dtrT!tm_numpro2 = 0
          dtrT!tm_datregin = dtrTm!tm_datregin
          dtrT!tm_numrgin = dtrTm!tm_numrgin
          dtrT!tm_datregom = dtrTm!tm_datregom
          dtrT!tm_numregom = dtrTm!tm_numregom
          dtrT!tm_ultagg = dtrTm!tm_ultagg
        Next
      End If

      '---------------------------------
      'aggiorno il protocollo acquisti 
      If (dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "(") AndAlso (NTSCInt(dtrTm!tm_codntra) <> 0 Or strFattRevCharge <> "N") Then
        If lNumregfOld <> 0 And strDatregfOld <> "" Then
          'è la ricontabilizzaz. di un documento intra o reverse charge: nella routine 'elaboratipo' ho letto il numero direttamente dalla reg. di CG: non devo aggiornare tabnuma!
        Else
          'aggiorno il numeratore VENDITE
          If bRevChargeNumdocDaPartita And NTSCInt(dtrTm!tm_numpar) <> 0 Then
            'è il caso di estremi documento emesso presi dal num. di partita fornitore (caso possibile solo con reg. vendite svincolato da protocolli vendite)
            'non aggiorno la numerazione, perchè il numero assegnato al doc. emesso non è sicuramente in progressione con quelli fino ad ora registrati!
          Else
            'caso standard
            i = AggNuma(strDitta, strTipoNumerazVendPerAcqIntra, strAlfdoc, nAnnoDoc, lNumdoc, True, True, strErr, dbConn)
            If strErr <> "" Then
              If NTSCStr(dtrTm!tm_flcont) = "S" And lNumdoc <> 0 Then
                'è la ricontabilizzaz. di un documento intra o reverse charge: nella routine 'elaboratipo' ho letto il numero direttamente dalla reg. di CG: proseguo
                strErr = ""
              Else
                strErr += oApp.Tr(Me, 129467251365742188, " - documento non contabilizzato")
                Return False
              End If
            End If
          End If
        End If
      End If
      If bAggProtP Then
        AggNuma(strDitta, "P", strAlfdocP, NTSCDate(strDatreg).Year, lNumdocP, True, True, strErr, dbConn) 'se sto contabilizzando nel 2013 dei documenti del 2013, il protocollo deve essere quello del 2013 (ovvero quello nell'anno della data di contabilizzazione)
        If strErr <> "" Then
          strErr += oApp.Tr(Me, 129467251586542969, " - documento non contabilizzato")
          Return False
        End If
      End If
      If bAggProtP2 Then
        AggNuma(strDitta, "P", strAlfdocP2, NTSCDate(strDatreg).Year, lNumdocP2, True, True, strErr, dbConn) 'se sto contabilizzando nel 2013 dei documenti del 2013, il protocollo deve essere quello del 2013 (ovvero quello nell'anno della data di contabilizzazione)
        If strErr <> "" Then
          strErr += oApp.Tr(Me, 129467251647900391, " - documento non contabilizzato")
          Return False
        End If
      End If

      '---------------------------------
      'creo prinot
      strSQL = "INSERT INTO prinot " & GetQueryInsertField(dttPN, "pn_")
      For i = 0 To dttPN.Rows.Count - 1
        strSQLVal = GetQueryInsertValue(dttPN, dttPN.Rows(i), "pn_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttAnaditac)
      If dttAnaditac.Rows.Count > 0 Then
        If NTSCStr(dttAnaditac.Rows(0)!ac_gprincomp) = "S" Then
          For i = 0 To dttPN.Rows.Count - 1
            If i = 0 Then
              strSQL = "bussp_aggprincomp " & CStrSQL(strDitta) & ", " & CDataSQL(NTSCDate(dttPN.Rows(0)!pn_datreg)) & ", " & dttPN.Rows(0)!pn_numreg.ToString & ", " & TrovaNdec(0)
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            ElseIf NTSCDate(dttPN.Rows(i)!pn_datreg) <> NTSCDate(dttPN.Rows(i - 1)!pn_datreg) Or NTSCInt(dttPN.Rows(i)!pn_numreg) <> NTSCInt(dttPN.Rows(i - 1)!pn_numreg) Then
              strSQL = "bussp_aggprincomp " & CStrSQL(strDitta) & ", " & CDataSQL(NTSCDate(dttPN.Rows(i)!pn_datreg)) & ", " & dttPN.Rows(i)!pn_numreg.ToString & ", " & TrovaNdec(0)
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            End If
          Next
        End If
      End If

      '---------------------------------
      'creo moviva
      strSQL = "INSERT INTO moviva " & GetQueryInsertField(dttMO, "mi_")
      For i = 0 To dttMO.Rows.Count - 1
        strSQLVal = GetQueryInsertValue(dttMO, dttMO.Rows(i), "mi_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

      '---------------------------------
      'creo priana
      If bCa2 Then
        Dim lProgMin As Integer = 0
        Dim lProgMax As Integer = 0
        If dttCa2.Rows.Count > 0 Then
          lProgMin = NTSCInt(dttCa2.Select("", "pa2_progr ASC")(0)!pa2_progr)
          lProgMax = NTSCInt(dttCa2.Select("", "pa2_progr DESC")(0)!pa2_progr)
        End If
        'scrivo priana2
        strSQL = "INSERT INTO priana2 " & GetQueryInsertField(dttCa2, "pa2_")
        For i = 0 To dttCa2.Rows.Count - 1
          'scrivo solo le righe con quantità e/o valore diversi da 0
          strSQLVal = GetQueryInsertValue(dttCa2, dttCa2.Rows(i), "pa2_")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
        If dttCa2.Select("pa2_mmtipork = 'T'").Length > 0 Then
          'per documenti ricevuti con carico da produzione devo rilanciare per ogni documento 
          'la ScriviPriana2DaTestmag con il parametro 'soloscarichieqtaprd' = true per 
          'riscrivere in priana2 le righe di scarico a prod e qta prodotta 
          'OCCHIO: se nella registrazioen di CG c'è il riferimento al carico da prod più volte, devo chiamare la 
          'ScriviPriana2DaTestmag solo una volta per ogni carico da prod!!!
          If oDttgr Is Nothing Then oDttgr = New CLEGROUPBY
          If dttGr Is Nothing Then dttGr = New DataTable
          dttGr.Clear()
          If Not oDttgr.NTSGroupBy(dttCa2, dttGr, "pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc, pa2_datdoc", _
                                                  "pa2_mmtipork = 'T'", _
                                                  "pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc, pa2_datdoc") Then
            Return False
          End If
          For Each dtrT As DataRow In dttGr.Rows
            ScriviPriana2DaTestmag(NTSCStr(strDitta), NTSCStr(dtrT!pa2_mmtipork), NTSCInt(dtrT!pa2_mmanno), _
                                   NTSCStr(dtrT!pa2_mmserie), NTSCInt(dtrT!pa2_mmnumdoc), NTSCStr(dtrT!pa2_datdoc), _
                                   dbConn, lIITtkeys, True)
          Next
          dttGr.Clear()
        End If

        'aggiorno pria2comp
        strSQL = "bussp_aggpria2comp " & CStrSQL(strDitta) & ", " & lProgMin.ToString & ", " & lProgMax.ToString & ", " & TrovaNdec(0)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Else
        'vecchia analitica
        strSQL = "INSERT INTO priana " & GetQueryInsertField(dttCA, "pa_")
        For i = 0 To dttCA.Rows.Count - 1
          strSQLVal = GetQueryInsertValue(dttCA, dttCA.Rows(i), "pa_")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
      End If

      '---------------------------------
      'compensazione fatture con ddt di acconto
      'se ho settato l'opzione di perdere la partita dell'acconto, ho in dttSC dei record con sc_tippaga = -1
      'queste scadenze servono solo per avere gli estremi delle registraz. dove andare a cambiare la partita
      'per ora sono solo conto gestit a partite e non scadenze
      'la partita della fattura è in sc_anndist, sc_alfdoc, sc_numdoc
      For Each dtrT As DataRow In dttSC.Select("sc_tippaga = -1")
        strSQL = "UPDATE prinot SET pn_annpar = " & dtrT!sc_anndist.ToString & ", " & _
                 " pn_alfpar = " & CStrSQL(dtrT!sc_alfdoc) & ", " & _
                 " pn_numpar = " & dtrT!sc_numdoc.ToString & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND pn_datreg = " & CDataSQL(NTSCDate(dtrT!sc_datreg)) & _
                 " AND pn_numreg = " & dtrT!sc_numreg.ToString & _
                 " AND pn_integr = " & CStrSQL(dtrT!sc_integr)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        dtrT.Delete()
      Next

      '---------------------------------
      'creo scaden
      strSQL = "INSERT INTO scaden " & GetQueryInsertField(dttSC, "sc_")
      For Each dtrT As DataRow In dttSC.Select("", "", DataViewRowState.Added)
        strSQLVal = GetQueryInsertValue(dttSC, dtrT, "sc_")
        Try
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        Catch ex As Exception
          strErr = oApp.Tr(Me, 128369099426798000, "Attenzione. Effetto non generato: esiste già scadenza |" & _
                    dtrT!sc_conto.ToString & "| anno part. |" & dtrT!sc_annpar.ToString & _
                    "| alfa part. |'" & dtrT!sc_alfpar.ToString & "'|" & _
                    " n.part. |" & dtrT!sc_numpar.ToString & "| integrativa |'" & _
                    dtrT!sc_integr.ToString & "'|" & _
                    " rata = |" & dtrT!sc_numrata.ToString & "| ")
          Return False
        End Try
      Next

      '---------------------------------
      'aggiorno scaden (per le righe saldate)
      For Each dtrT As DataRow In dttSC.Select("", "", DataViewRowState.ModifiedCurrent)
        strSQL = "UPDATE scaden SET " & GetQueryUpdate(dttSC, dtrT, "sc_")
        strSQL += " WHERE codditt = " & CStrSQL(dtrT!codditt) & _
                  " AND sc_conto = " & dtrT!sc_conto.ToString & _
                  " AND sc_annpar = " & dtrT!sc_annpar.ToString & _
                  " AND sc_alfpar = " & CStrSQL(dtrT!sc_alfpar) & _
                  " AND sc_numpar = " & dtrT!sc_numpar.ToString & _
                  " AND sc_numrata = " & dtrT!sc_numrata.ToString & _
                  " AND sc_integr = " & CStrSQL(dtrT!sc_integr)
        Try
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Catch ex As Exception
          strErr = oApp.Tr(Me, 129203721195451406, "Attenzione. scadenza non aggiornata: |" & _
                    dtrT!sc_conto.ToString & "| anno part. |" & dtrT!sc_annpar.ToString & _
                    "| alfa part. |'" & dtrT!sc_alfpar.ToString & "'|" & _
                    " n.part. |" & dtrT!sc_numpar.ToString & "| integrativa |'" & _
                    dtrT!sc_integr.ToString & "'|" & _
                    " rata = |" & dtrT!sc_numrata.ToString & "|: ") & ex.Message
          Return False
        End Try
      Next

      '---------------------------------
      'reinserisco le scadenze vecchie da mantenere (rielaborazione)
      If Not dttScadRielab Is Nothing AndAlso dttScadRielab.Columns.Contains("xx_operaz") Then
        'se devo aggiornare le vecchie scadenze con i dati delle nuove
        strSQL = "INSERT INTO scaden " & GetQueryInsertField(dttScadRielab, "sc_")
        For Each dtrT As DataRow In dttScadRielab.Select("xx_mantieni = 'S' OR xx_operaz = 'SALD'")
          strSQLVal = GetQueryInsertValue(dttScadRielab, dtrT, "sc_")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
          dtrT.Delete()
        Next
        dttScadRielab.AcceptChanges()
      End If    'If Not dttScadRielab Is Nothing AndAlso dttScadRielab.Columns.Contains("xx_operaz") Then

      '---------------------------------
      'aggiorno la fattura
      strSQL = "UPDATE " & IIf(bParcellazione, "testpar", "testmag").ToString & _
               " SET tm_flcont = 'S', " & _
               " tm_datregef = " & CDataSQL(NTSCDate(dtrTm!tm_datregef)) & ", " & _
               " tm_numregef = " & dtrTm!tm_numregef.ToString & ", " & _
               " tm_numprot = " & dtrTm!tm_numprot.ToString & ", " & _
               " tm_nregiva = " & dtrTm!tm_nregiva.ToString & ", "
      If bParcellazione = False Then
        strSQL += " tm_numdoc2 = " & dtrTm!tm_numdoc2.ToString & ", " & _
                  " tm_numpro2 = " & dtrTm!tm_numpro2.ToString & ", " & _
                  " tm_alfpro = " & CStrSQL(dtrTm!tm_alfpro) & ", "
        If NTSCInt(dtrTm!tm_numregom) <> 0 Then
          strSQL += " tm_datregom = " & CDataSQL(NTSCDate(dtrTm!tm_datregom)) & ", " & _
                    " tm_numregom = " & dtrTm!tm_numregom.ToString & ", "
        Else
          strSQL += " tm_datregom = null, tm_numregom = 0, "
        End If
      End If
      If NTSCInt(dtrTm!tm_numrgin) <> 0 Then
        strSQL += " tm_datregin = " & CDataSQL(NTSCDate(dtrTm!tm_datregin)) & ", " & _
                  " tm_numrgin = " & dtrTm!tm_numrgin.ToString & ", "
      Else
        strSQL += " tm_datregin = null, tm_numrgin = 0, "
      End If
      strSQL += " tm_ultagg = " & CDataOraSQL(NTSCDate(dtrTm!tm_ultagg)) & _
               " WHERE codditt = " & CStrSQL(dtrTm!codditt)

      If NTSCStr(dtrTm!xx_raggr) = "S" Then
        'se corrispettivo o ric fisc emessa totalmente incassati e documento generato RAGGRUPPATO, 
        'devo aggiornare tutti i dicumenti del raggruppamento
        For Each dtrT As DataRow In dttTmOrig.Select("xx_dtttm = " & NTSCInt(dtrTm!xx_dtttm).ToString)
          strWhere += " (tm_tipork = " & CStrSQL(dtrT!tm_tipork) & " AND tm_anno = " & NTSCInt(dtrT!tm_anno) & " AND tm_serie = " & CStrSQL(dtrT!tm_serie) & " AND tm_numdoc = " & NTSCInt(dtrT!tm_numdoc) & ") OR "
        Next
        If strWhere.Length > 0 Then strWhere = strWhere.Substring(0, strWhere.Length - 4)

        strSQL += " AND (" & strWhere & ")"
      Else
        strSQL += " AND tm_tipork = " & CStrSQL(dtrTm!tm_tipork) & _
                 " AND tm_anno = " & dtrTm!tm_anno.ToString & _
                 " AND tm_serie = " & CStrSQL(dtrTm!tm_serie) & _
                 " AND tm_numdoc = " & dtrTm!tm_numdoc.ToString
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '---------------------------------
      'invalido la liquidazione IVA contenente la fattura generata
      'se non c'è la data di registrazione vuol dire che la fattura non è stata contabilizzata 
      '(ad esempio xchè ricontabilizzaz con scadenze già saldate)
      'NON LO FACCIO PER GLI AVVISI DI PARCELLA/NOTULA
      If NTSCStr(dtrTm!tm_tipork) <> "3" Then
        InvalidaLiqIVA(strDitta, strDatreg, dbConn)
      End If


      '---------------------------------
      'stanziamenti
      If bStornaStanziamenti Then
        'ottengo i ddt collegati alla fattura con stanziamenti contabilizzati
        strSQL = "SELECT tm_fleleff, tm_datregef, tm_numregef, tm_tipork, tm_anno, tm_serie, tm_numdoc " & _
                  " FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND tm_tiporkfat = " & CStrSQL(dtrTm!tm_tipork) & _
                  " AND tm_annfat = " & dtrTm!tm_anno.ToString & _
                  " AND tm_alffat = " & CStrSQL(dtrTm!tm_serie) & _
                  " AND tm_numfat = " & dtrTm!tm_numdoc.ToString & _
                  " AND tm_fleleff = 'S'" & _
                  " AND tm_numregef <> 0"
        dttDDT = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        For Each dtrT As DataRow In dttDDT.Rows
          strSQL = "INSERT INTO prinot (codditt, pn_datreg, pn_numreg, pn_riga, pn_conto, pn_causale, pn_partite, " & _
                    " pn_darave, pn_importo, pn_dare, pn_avere, pn_datdoc, pn_alfdoc, pn_numdoc, pn_annpar, pn_alfpar, " & _
                    " pn_numpar, pn_descr, pn_scadenz, pn_escomp, pn_controp, pn_fllg, pn_flri, pn_flst, " & _
                    " pn_contocf, pn_tregiva, pn_nregiva, pn_codiva, pn_aliqiva, pn_indetr, pn_numpro, " & _
                    " pn_imponib, pn_imponibval, pn_tipacq, pn_codvalu, pn_impval, pn_dareval, pn_avereval, " & _
                    " pn_codccos, pn_ultdesc, pn_cambio, pn_integr, pn_dtcomiva, pn_dtvaluta, pn_datini, " & _
                    " pn_datfin, pn_ultagg, pn_opnome, pn_spunta, pn_chiusecb, pn_dtcomplaf, pn_csriga, " & _
                    " pn_movincp, pn_movcron, pn_descauc, pn_codstsa, pn_codagr, pn_flcee, pn_flrsm1, " & _
                    " pn_flrsm2, pn_rivend, pn_mista, pn_codforf, pn_topagr, pn_contodef, pn_alfpro, pn_tipmovr, " & _
                    " pn_tipmovc, pn_ieddatregr, pn_iednumregr, pn_iedrigaregr, pn_rigaiva) "
          strSQL += " SELECT codditt, " & CDataSQL(strDatreg) & ", " & lNumregStanziamenti & ", " & _
                    " pn_riga, pn_conto, " & nCauststanz & ", pn_partite, " & _
                    " CASE WHEN pn_darave = 'D' THEN 'A' ELSE 'D' END, " & _
                    " pn_importo * -1, pn_avere, pn_dare, pn_datdoc, pn_alfdoc, pn_numdoc, pn_annpar, pn_alfpar, " & _
                    " pn_numpar, pn_descr, pn_scadenz, " & dttPN.Rows(0)!pn_escomp.ToString & ", pn_controp, pn_fllg, pn_flri, pn_flst, " & _
                    " pn_contocf, pn_tregiva, pn_nregiva, pn_codiva, pn_aliqiva, pn_indetr, pn_numpro, " & _
                    " pn_imponib, pn_imponibval, pn_tipacq, pn_codvalu, pn_impval * -1, pn_avereval, pn_dareval, " & _
                    " pn_codccos, pn_ultdesc, pn_cambio, pn_integr, " & _
                    CDataSQL(strDatreg) & ", " & _
                    CDataSQL(strDatreg) & ", " & _
                    CDataSQL(strDatreg) & ", " & _
                    CDataSQL(strDatreg) & ", " & _
                    " pn_ultagg, pn_opnome, pn_spunta, pn_chiusecb, pn_dtcomplaf, pn_csriga, " & _
                    " pn_movincp, pn_movcron, " & CStrSQL(strCausaleStornoStanz) & ", pn_codstsa, pn_codagr, pn_flcee, pn_flrsm1, " & _
                    " pn_flrsm2, pn_rivend, pn_mista, pn_codforf, pn_topagr, pn_contodef, pn_alfpro, pn_tipmovr, " & _
                    " pn_tipmovc, pn_ieddatregr, pn_iednumregr, pn_iedrigaregr, pn_rigaiva " & _
                    " FROM prinot WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND pn_datreg = " & CDataSQL(NTSCDate(dtrT!tm_datregef)) & _
                    " AND pn_numreg = " & dtrT!tm_numregef.ToString & _
                    " AND pn_integr = '" & dttPN.Rows(0)!pn_integr.ToString & "'"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'adesso per priana
          'correggo pa_progr,
          'pa_escomp
          'pa_datreg, pa_numreg,
          'inverto l'importo per -1
          'pa_datini
          'pa_datfin
          strSQL = "SELECT pa_progr FROM priana WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND pa_datreg = " & CDataSQL(NTSCDate(dtrT!tm_datregef)) & _
                  " AND pa_numreg = " & dtrT!tm_numregef.ToString & _
                  " AND pa_integr = '" & dttPN.Rows(0)!pn_integr.ToString & "'"
          dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          For Each dtrT1 As DataRow In dttTmp1.Rows
            i = LegNuma(strDitta, "PA", " ", 0, True)
            i = AggNuma(strDitta, "PA", " ", 0, i, True, True, strErr, dbConn)
            strSQL = "INSERT INTO priana (codditt, pa_progr, pa_escomp, pa_conto, pa_codcena, pa_codcfam, pa_commeca, " & _
                    " pa_subcommeca, pa_unmis, pa_quant, pa_darave, pa_importo, pa_impval, pa_impolist, pa_importoda, " & _
                    " pa_impvalda, pa_impolistda, pa_controp, pa_codvalu, pa_cambio, pa_origine, pa_mmtipork, pa_mmanno, " & _
                    " pa_mmserie, pa_mmnumdoc, pa_datdoc, pa_numdoc, pa_alfdoc, pa_datreg, pa_numreg, pa_riga, pa_note, " & _
                    " pa_codart, pa_desart, pa_codcope, pa_codlavo, pa_daore, pa_aore, pa_lotto, pa_mastro, pa_datini, " & _
                    " pa_datfin, pa_integr, pa_quantda, pa_contocg, pa_livrib, pa_codcenaorig, pa_contoorig, pa_tipore, " & _
                    " pa_pmtaskid, pa_pmsalcon, pa_pmqtadis, pa_pmvaldis) "
            strSQL = strSQL & "SELECT codditt, " & i & ", " & dttPN.Rows(0)!pn_escomp.ToString & ", pa_conto, pa_codcena, pa_codcfam, pa_commeca, " & _
                    " pa_subcommeca, pa_unmis, pa_quant, " & _
                    " CASE WHEN pa_darave = 'D' THEN 'A' ELSE 'D' END, " & _
                    " pa_importo * -1, pa_impval * -1, pa_impolist * -1, pa_importoda, " & _
                    " pa_impvalda, pa_impolistda, pa_controp, pa_codvalu, pa_cambio, pa_origine, pa_mmtipork, pa_mmanno, " & _
                    " pa_mmserie, pa_mmnumdoc, pa_datdoc, pa_numdoc, pa_alfdoc, " & _
                    CDataSQL(strDatreg) & ", " & lNumreg & ", pa_riga, pa_note, " & _
                    " pa_codart, pa_desart, pa_codcope, pa_codlavo, pa_daore, pa_aore, pa_lotto, pa_mastro, " & _
                    CDataSQL(strDatreg) & " , " & _
                    CDataSQL(strDatreg) & ", pa_integr, pa_quantda, pa_contocg, pa_livrib, pa_codcenaorig, pa_contoorig, pa_tipore, " & _
                    " pa_pmtaskid, pa_pmsalcon, pa_pmqtadis, pa_pmvaldis " & _
                    " FROM priana WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND pa_progr = " & dtrT1!pa_progr.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next    'For Each dtrT1 As DataRow In dttTmp1.Rows
        Next    'For Each dtrT As DataRow In dttDDT.Rows

        If dttAnaditac.Rows.Count > 0 Then
          If NTSCStr(dttAnaditac.Rows(0)!ac_gprincomp) = "S" Then
            strSQL = "bussp_aggprincomp " & CStrSQL(strDitta) & ", " & CDataSQL(NTSCDate(strDatreg)) & ", " & lNumregStanziamenti & ", " & TrovaNdec(0)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        End If

        'aggiorno testmag di tutti i ddt collegati alle fatture
        strSQL = "UPDATE testmag SET " & _
                " tm_flelvuo = 'S', " & _
                " tm_datregin = " & CDataSQL(strDatreg) & ", " & _
                " tm_numrgin = " & lNumregStanziamenti & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND tm_tipork = '" & dtrTm!tm_tipork.ToString & "'" & _
                " AND tm_anno = " & dtrTm!tm_anno.ToString & _
                " AND tm_serie = '" & dtrTm!tm_serie.ToString & "'" & _
                " AND tm_numdoc = " & dtrTm!tm_numdoc.ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      End If    'If bStornaStanziamenti Then

      'aggiorno gli PRFATT e PRINCPAG (tabelle per contabilità professionista)
      If bParcellazione Then
        If bProfes Then
          If Not CreaRegistrazione_Profes(strDitta, strDatreg, lNumreg, lNumregInc, dtrTm, strInt, _
                                          strErr, dbConn, dsCove, nAggControp) Then
            If IsInTrans Then AnnullaTrans()
            Return False
          End If
        End If
      End If

      'Dichiarazioni di intento
      If bModDII And bCollegaMG_DI And dtrTm.Table.Columns.Contains("tm_dianno") Then
        'In inserimento, controllo e passo i nuovi valori della dichiarazione di intento
        dtrDataRow = dttPN.Select("pn_nregiva <> 0")
        If dtrDataRow.Length > 0 Then
          nConto = NTSCInt(dtrDataRow(0)!pn_contocf)
          ValCodiceDb(NTSCStr(nConto), strDitta, "ANAGRA", "N", , dttAnagra)
          strTipo = NTSCStr(dttAnagra.Rows(0)!an_tipo)
          nAnno = NTSCInt(dtrTm!tm_dianno)
          nNumero = NTSCInt(dtrTm!tm_dinumero)
        End If
        If nAnno <> 0 And nNumero <> 0 Then
          If Not GestisciFlagChiusura("I", strDitta, strTipo, NTSCInt(dttPN.Rows(0)!pn_dianno), NTSCInt(dttPN.Rows(0)!pn_dinumero), dbConn) Then
            If IsInTrans Then AnnullaTrans()
            Return False
          End If
        End If
      End If

      'routine per personalizzazioni
      If Not CreaRegistrazione_pers(strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, _
                                     dtrTm, strInt, strDatregfOld, lNumregfOld, _
                                     strDatreginOld, lNumreginOld, strDatregomOld, lNumregomOld, _
                                     bGestStanziamenti, lContOld, nAnnoOld, strSerieOld, _
                                     lNumdocOld, bStornaStanziamenti, lNumregStanziamenti, nCauststanz, _
                                     strCausaleStornoStanz, strErr, nAnnoDoc, strAlfdoc, lNumdoc, _
                                     bAggProtP, strAlfdocP, lNumdocP, bAggProtP2, strAlfdocP2, lNumdocP2, _
                                     strTipoNumerazVendPerAcqIntra, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        Return False
      End If

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      CreaRegistrazione_PersAfterTrans(strDitta, dttPN, dttMO, dttSC, dttCA, strDatreg, _
                               dtrTm, strInt, strDatregfOld, lNumregfOld, _
                               strDatreginOld, lNumreginOld, strDatregomOld, lNumregomOld, _
                               bGestStanziamenti, lContOld, nAnnoOld, strSerieOld, _
                               lNumdocOld, bStornaStanziamenti, lNumregStanziamenti, nCauststanz, _
                               strCausaleStornoStanz, strErr, nAnnoDoc, strAlfdoc, lNumdoc, _
                               bAggProtP, strAlfdocP, lNumdocP, bAggProtP2, strAlfdocP2, lNumdocP2, _
                               strTipoNumerazVendPerAcqIntra)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      If dbConn.State = ConnectionState.Open Then
        If IsInTrans Then ChiudiTrans()
        dbConn.Close()
      End If
      dbConn = Nothing
      Try
        If bCa2 Then ResetTblInstId("TTKEYS", False, lIITtkeys)
      Catch
      End Try
      dttDDT.Clear()
      dttTmp1.Clear()
    End Try
  End Function
  Public Overridable Function CreaRegistrazione_Profes(ByVal strDitta As String, ByVal strDatreg As String, _
                                                    ByVal lNumreg As Integer, ByVal lNumregInc As Integer, _
                                                    ByRef dtrTm As DataRow, ByVal strInt As String, _
                                                    ByRef strErr As String, ByRef dbConn As DbConnection) As Boolean
    Try
      'obsoleta
      Return CreaRegistrazione_Profes(strDitta, strDatreg, lNumreg, lNumregInc, dtrTm, strInt, strErr, dbConn, Nothing, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaRegistrazione_Profes(ByVal strDitta As String, ByVal strDatreg As String, _
                                                      ByVal lNumreg As Integer, ByVal lNumregInc As Integer, _
                                                      ByRef dtrTm As DataRow, ByVal strInt As String, _
                                                      ByRef strErr As String, ByRef dbConn As DbConnection, _
                                                      ByRef dsCove As DataSet, ByVal nAggControp As Integer) As Boolean
    'aggirna gli archivi per la contabilità professionsti
    'PRFATT e PRINCPAG
    Dim strSQL As String = ""
    Dim dTotiva As Decimal = 0
    Dim dIncassato As Decimal = 0
    Dim dNetto As Decimal = 0
    Dim dttTmp As New DataTable
    Dim lConto As Integer = 0
    Dim strDescr As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDatreg, lNumreg, lNumregInc, dtrTm, strInt, strErr, dbConn, dsCove, nAggControp})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(4), DataRow)
        strErr = CType(oIn(6), String)
        dbConn = CType(oIn(7), DbConnection)
        dsCove = CType(oIn(8), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      '--------------------
      'QUERY PER REGISTRAZIONE FATTURA

      ValCodiceDb("1", strDitta, "TABPEPA", "N", "", dttTmp)
      If NTSCDec(dtrTm!tm_pagato) = 0 Then
        If NTSCDec(dtrTm!tm_abbuono) > 0 Then
          lConto = TrovaContoDaCodcontr(strDitta, CoveAggControp(dsCove, NTSCInt(dttTmp.Rows(0)!tb_contrabpa), nAggControp))
        Else
          lConto = TrovaContoDaCodcontr(strDitta, CoveAggControp(dsCove, NTSCInt(dttTmp.Rows(0)!tb_contrabat), nAggControp))
        End If
      Else
        lConto = TrovaContoDaCodcontr(strDitta, CoveAggControp(dsCove, NTSCInt(dttTmp.Rows(0)!tb_contrcas), nAggControp))
      End If
      dttTmp.Clear()

      ' se professionista aggiorno princpag
      ' non serve aggiornare prfatt: è già stato chiuso nel momento in cui è stato scritto
      'attenzione: rx_compens = dtrTm!tm_compens + dtrTm!tm_nonsogg, ovvero compensi + acconti (che hanno segno negativo)
      If NTSCStr(dtrTm!tm_tipork) <> "3" Then
        If NTSCStr(dtrTm!tm_tipork) <> "2" Then
          'fattura o nota di accredito normale

          dTotiva = ArrDbl(NTSCDec(dtrTm!tm_imposta_1) + NTSCDec(dtrTm!tm_imposta_2) + NTSCDec(dtrTm!tm_imposta_3) + _
                           NTSCDec(dtrTm!tm_imposta_4) + NTSCDec(dtrTm!tm_imposta_5) + NTSCDec(dtrTm!tm_imposta_6) + _
                           NTSCDec(dtrTm!tm_imposta_7) + NTSCDec(dtrTm!tm_imposta_8), TrovaNdec(0))
          dIncassato = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut), TrovaNdec(0))
          dNetto = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut), TrovaNdec(0))

          strSQL = "INSERT INTO prfatt (codditt, rx_datreg, rx_numreg, rx_rigareg, rx_conto, rx_tipork, rx_alfdoc, " & _
                   "rx_numdoc, rx_datdoc, rx_codrtac, rx_totdoc, rx_netto, rx_compens, rx_nonsogg, rx_iva, rx_ritenut, " & _
                   "rx_perrite, rx_percomp, rx_flsta, rx_7annoc, rx_flchiu, rx_nettoinc) " & _
                   "VALUES (" & CStrSQL(strDitta) & ", " & CDataSQL(strDatreg) & ", " & lNumreg & ", " & _
                   "1, " & NTSCInt(dtrTm!tm_conto) & ", " & IIf(NTSCStr(dtrTm!tm_tipork) = "2" Or NTSCStr(dtrTm!tm_tipork) = "4", "'2'", "'1'").ToString & ", " & _
                   CStrSQL(dtrTm!tm_serie) & ", " & NTSCInt(dtrTm!tm_numdoc).ToString & ", " & CDataSQL(NTSCDate(dtrTm!tm_datdoc)) & ", " & _
                   NTSCInt(dtrTm!tm_codrtac) & ", " & CDblSQL(NTSCDec(dtrTm!tm_totdoc)) & ", " & _
                   CDblSQL(ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut), TrovaNdec(0))) & ", " & _
                   CDblSQL(ArrDbl(NTSCDec(dtrTm!tm_compens) + NTSCDec(dtrTm!tm_totaccscal) + NTSCDec(dtrTm!tm_spegen), TrovaNdec(0))) & ", " & _
                   CDblSQL(NTSCDec(dtrTm!tm_nonsogg)) & ", " & CDblSQL(dTotiva) & ", " & CDblSQL(NTSCDec(dtrTm!tm_ritenut)) & ", " & _
                   CDblSQL(NTSCDec(dtrTm!tm_perrite)) & ", " & CDblSQL(NTSCDec(dtrTm!tm_percomp)) & ", 'N', " & _
                   NTSCInt(dtrTm!tm_annoc) & ", "
          If lNumregInc <> 0 Then
            If ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut) - NTSCDec(dtrTm!tm_abbuono) - NTSCDec(dtrTm!tm_pagato), TrovaNdec(0)) = 0 Then
              'incasso totale
              strSQL = strSQL & "'S', " & CDblSQL(ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut), TrovaNdec(0))) & ")"
            Else
              'incasso parziale
              strSQL = strSQL & "'N', " & CDblSQL(ArrDbl(NTSCDec(dtrTm!tm_abbuono) + NTSCDec(dtrTm!tm_pagato), TrovaNdec(0))) & ")"
            End If
          Else
            strSQL = strSQL & "'N', 0)"
          End If
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Else
          'nota di accredito a storno fattura
          strSQL = "SELECT * FROM testpar WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND tm_conto = " & NTSCInt(dtrTm!tm_conto).ToString & _
                   " AND tm_tipork = " & CStrSQL(NTSCStr(dtrTm!tm_tiporkfat)) & _
                   " AND tm_anno = " & dtrTm!tm_annfat.ToString & _
                   " AND tm_serie = " & CStrSQL(NTSCStr(dtrTm!tm_alffat)) & _
                   " AND tm_numdoc = " & dtrTm!tm_numfat.ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count = 0 Then
            strErr = oApp.Tr(Me, 129651583226406250, "Operazione interrotta durante la registrazione nella sezione professionisti della nota di accredito " & _
                    "a storno fattura n. |" & dtrTm!tm_numdoc.ToString & "| : non è stata trovata la fattura a cui è collegata la nota di accredito")
            Return False
          End If

          strSQL = "INSERT INTO princpag(codditt, rxp_datreg, rxp_numreg, rxp_rigareg, rxp_dtincp, rxp_datregr, " & _
                "rxp_numregr, rxp_rigaregr, rxp_conto, rxp_tipork, rxp_alfdoc, rxp_numdoc, rxp_datdoc, rxp_totincpag, " & _
                "rxp_impdoc, rxp_compens, rxp_nonsogg, rxp_iva, rxp_ritenut, rxp_perrite, rxp_percomp, rxp_7annoc, " & _
                "rxp_progrreg, rxp_concasban, rxp_desconcb, rxp_impcterzi, rxp_impcasban) " & _
                "VALUES("
          With dttTmp.Rows(0)
            dIncassato = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut), TrovaNdec(0))
            dNetto = ArrDbl(NTSCDec(!tm_totdoc) - NTSCDec(!tm_ritenut), TrovaNdec(0))
            dTotiva = ArrDbl(NTSCDec(!tm_imposta_1) + NTSCDec(!tm_imposta_2) + NTSCDec(!tm_imposta_3) + _
                             NTSCDec(!tm_imposta_4) + NTSCDec(!tm_imposta_5) + NTSCDec(!tm_imposta_6) + _
                             NTSCDec(!tm_imposta_7) + NTSCDec(!tm_imposta_8), TrovaNdec(0))
            strSQL = strSQL & CStrSQL(strDitta) & ", " & CDataSQL(strDatreg) & ", " & lNumreg & ", " & _
                  "1, " & CDataSQL(strDatreg) & ", " & CDataSQL(NTSCDate(!tm_datregef)) & ", " & _
                  NTSCInt(!tm_numregef) & ", 1, " & NTSCInt(dtrTm!tm_conto) & ", 2, " & CStrSQL(!tm_serie) & ", " & _
                  NTSCInt(!tm_numdoc) & ", " & CDataSQL(NTSCDate(!tm_datdoc)) & ", " & CDblSQL(dIncassato) & ", " & _
                  "round(" & CDblSQL(NTSCDec(!tm_totdoc)) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), " & _
                  "round(" & CDblSQL(ArrDbl(NTSCDec(!tm_compens) + NTSCDec(!tm_totaccscal), TrovaNdec(0))) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), " & _
                  "round(" & CDblSQL(NTSCDec(!tm_nonsogg)) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), " & _
                  "round(" & CDblSQL(dTotiva) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), " & _
                  "round(" & CDblSQL(NTSCDec(!tm_ritenut)) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), " & _
                  CDblSQL(NTSCDec(dtrTm!tm_perrite)) & ", " & CDblSQL(NTSCDec(dtrTm!tm_percomp)) & ", " & dtrTm!tm_annoc.ToString & ", " & _
                  "0, " & lConto & ", ' ', " & _
                  "round(" & CDblSQL(NTSCDec(!tm_nonsogg)) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), 0)"
          End With
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          dNetto = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut), TrovaNdec(0))
          strSQL = "UPDATE prfatt SET rx_nettoinc = rx_nettoinc + " & CDblSQL(dNetto) & ", " & _
                   "rx_flchiu = CASE WHEN rx_nettoinc + " & CDblSQL(dNetto) & " >= rx_netto THEN 'S' ELSE 'N' END " & _
                   "WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND rx_datreg = " & CDataSQL(NTSCDate(dttTmp.Rows(0)!tm_datregef)) & _
                   " AND rx_numreg = " & NTSCInt(dttTmp.Rows(0)!tm_numregef) & " AND rx_rigareg = 1"

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          dttTmp.Clear()
        End If    'If strTipork <> "2" Then

        'esco se non ho contabilizzato gli incassi
        If lNumregInc = 0 Then Return True

        '--------------------
        'QUERY PER REGISTRAZIONE INCASSO
        '---------------------------------------------------------------------
        ' se professionista aggiorno princpag
        ' non serve aggiornare prfatt: è già stato chiuso nel momento in cui è stato scritto
        ' non faccio nulla per le note di accred a storno fatture
        If NTSCStr(dtrTm!tm_tipork) <> "2" Then
          ValCodiceDb(lConto.ToString, strDitta, "ANAGRA", "N", strDescr)

          strSQL = "INSERT INTO princpag(codditt, rxp_datreg, rxp_numreg, rxp_rigareg, rxp_dtincp, rxp_datregr, " & _
                  "rxp_numregr, rxp_rigaregr, rxp_conto, rxp_tipork, rxp_alfdoc, rxp_numdoc, rxp_datdoc, rxp_totincpag, " & _
                  "rxp_impdoc, rxp_compens, rxp_nonsogg, rxp_iva, rxp_ritenut, rxp_perrite, rxp_percomp, rxp_7annoc, " & _
                  "rxp_progrreg, rxp_concasban, rxp_desconcb, rxp_impcterzi, rxp_impcasban) " & "VALUES("

          If ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut) - NTSCDec(dtrTm!tm_abbuono) - NTSCDec(dtrTm!tm_pagato), TrovaNdec(0)) = 0 Then
            'incasso totale
            strSQL = strSQL & CStrSQL(strDitta) & ", " & CDataSQL(strDatreg) & ", " & lNumregInc & ", " & _
                  "1, " & CDataSQL(strDatreg) & ", " & CDataSQL(strDatreg) & ", " & _
                  lNumreg & ", 1, " & NTSCInt(dtrTm!tm_conto) & ", 1, " & CStrSQL(dtrTm!tm_serie) & ", " & _
                  dtrTm!tm_numdoc.ToString & ", " & CDataSQL(NTSCDate(dtrTm!tm_datdoc)) & ", " & _
                  CDblSQL(ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut), TrovaNdec(0))) & ", " & _
                  CDblSQL(NTSCDec(dtrTm!tm_totdoc)) & ", " & CDblSQL(ArrDbl(NTSCDec(dtrTm!tm_compens) + NTSCDec(dtrTm!tm_totaccscal), TrovaNdec(0))) & ", " & _
                  CDblSQL(NTSCDec(dtrTm!tm_nonsogg)) & ", " & CDblSQL(dTotiva) & ", " & CDblSQL(NTSCDec(dtrTm!tm_ritenut)) & ", " & _
                  CDblSQL(NTSCDec(dtrTm!tm_perrite)) & ", " & CDblSQL(NTSCDec(dtrTm!tm_percomp)) & ", " & dtrTm!tm_annoc.ToString & ", " & _
                  "0, " & lConto & ", " & CStrSQL(strDescr) & ", " & CDblSQL(NTSCDec(dtrTm!tm_nonsogg)) & ", " & _
                  CDblSQL(NTSCDec(dtrTm!tm_pagato)) & ")"
          Else
            'incasso parziale
            dIncassato = ArrDbl(NTSCDec(dtrTm!tm_abbuono) + NTSCDec(dtrTm!tm_pagato), TrovaNdec(0))
            dNetto = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut), TrovaNdec(0))

            strSQL = strSQL & CStrSQL(strDitta) & ", " & CDataSQL(strDatreg) & ", " & lNumregInc & ", " & _
                  "1, " & CDataSQL(strDatreg) & ", " & CDataSQL(strDatreg) & ", " & _
                  lNumreg & ", 1, " & NTSCInt(dtrTm!tm_conto) & ", 1, " & CStrSQL(dtrTm!tm_serie) & ", " & _
                  dtrTm!tm_numdoc.ToString & ", " & CDataSQL(NTSCDate(dtrTm!tm_datdoc)) & ", " & CDblSQL(dIncassato) & ", " & _
                  "round(" & CDblSQL(NTSCDec(dtrTm!tm_totdoc)) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), " & _
                  "round(" & CDblSQL(ArrDbl(NTSCDec(dtrTm!tm_compens) + NTSCDec(dtrTm!tm_totaccscal), TrovaNdec(0))) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), " & _
                  "round(" & CDblSQL(NTSCDec(dtrTm!tm_nonsogg)) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), " & _
                  "round(" & CDblSQL(dTotiva) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), " & _
                  "round(" & CDblSQL(NTSCDec(dtrTm!tm_ritenut)) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), " & _
                  CDblSQL(NTSCDec(dtrTm!tm_perrite)) & ", " & CDblSQL(NTSCDec(dtrTm!tm_percomp)) & ", " & dtrTm!tm_annoc.ToString & ", " & _
                  "0, " & lConto & ", " & CStrSQL(Left(strDescr & " ", 30)) & ", " & _
                  "round(" & CDblSQL(NTSCDec(dtrTm!tm_nonsogg)) & " * " & CDblSQL(dIncassato) & " / " & CDblSQL(dNetto) & ", 2), " & _
                  CDblSQL(NTSCDec(dtrTm!tm_pagato)) & ")"
          End If
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If

      End If    'If NTSCStr(dtrTm!tm_tipork) <> "3" Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function CreaRegistrazione_pers(ByVal strDitta As String, ByRef dttPN As DataTable, ByRef dttMO As DataTable, _
                                              ByRef dttSC As DataTable, ByRef dttCA As DataTable, ByVal strDatreg As String, _
                                              ByRef dtrTm As DataRow, ByVal strInt As String, _
                                              ByVal strDatregfOld As String, ByVal lNumregfOld As Integer, _
                                              ByVal strDatreginOld As String, ByVal lNumreginOld As Integer, _
                                              ByVal strDatregomOld As String, ByVal lNumregomOld As Integer, _
                                              ByVal bGestStanziamenti As Boolean, ByVal lContOld As Integer, _
                                              ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                              ByVal lNumdocOld As Integer, ByVal bStornaStanziamenti As Boolean, _
                                              ByVal lNumregStanziamenti As Integer, ByVal nCauststanz As Integer, _
                                              ByVal strCausaleStornoStanz As String, ByRef strErr As String, _
                                              ByVal nAnnoDoc As Integer, ByVal strAlfdoc As String, ByVal lNumdoc As Integer, _
                                              ByVal bAggProtP As Boolean, ByVal strAlfdocP As String, ByVal lNumdocP As Integer, _
                                              ByVal bAggProtP2 As Boolean, ByVal strAlfdocP2 As String, ByVal lNumdocP2 As Integer, _
                                              ByVal strTipoNumerazVendPerAcqIntra As String, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      'utilizzabile per personalizzazioni

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CreaRegistrazione_PersAfterTrans(ByVal strDitta As String, ByRef dttPN As DataTable, ByRef dttMO As DataTable, _
                                                ByRef dttSC As DataTable, ByRef dttCA As DataTable, ByVal strDatreg As String, _
                                                ByRef dtrTm As DataRow, ByVal strInt As String, _
                                                ByVal strDatregfOld As String, ByVal lNumregfOld As Integer, _
                                                ByVal strDatreginOld As String, ByVal lNumreginOld As Integer, _
                                                ByVal strDatregomOld As String, ByVal lNumregomOld As Integer, _
                                                ByVal bGestStanziamenti As Boolean, ByVal lContOld As Integer, _
                                                ByVal nAnnoOld As Integer, ByVal strSerieOld As String, _
                                                ByVal lNumdocOld As Integer, ByVal bStornaStanziamenti As Boolean, _
                                                ByVal lNumregStanziamenti As Integer, ByVal nCauststanz As Integer, _
                                                ByVal strCausaleStornoStanz As String, ByRef strErr As String, _
                                                ByVal nAnnoDoc As Integer, ByVal strAlfdoc As String, ByVal lNumdoc As Integer, _
                                                ByVal bAggProtP As Boolean, ByVal strAlfdocP As String, ByVal lNumdocP As Integer, _
                                                ByVal bAggProtP2 As Boolean, ByVal strAlfdocP2 As String, ByVal lNumdocP2 As Integer, _
                                                ByVal strTipoNumerazVendPerAcqIntra As String) As Boolean
    Dim strSQL As String = ""
    Try
      'utilizzabile per personalizzazioni

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetScadenFattura(ByVal strDitta As String, ByVal lConto As Integer, _
                                               ByVal nAnnpar As Integer, ByVal strAlfpar As String, _
                                               ByVal lNumpar As Integer, ByVal bNonSaldate As Boolean, _
                                               ByVal strIntegr As String, ByVal nCodvalu As Integer, _
                                               ByRef dttOut As DataTable, ByVal strDatregSaldo As String, _
                                               ByVal lNumregSaldo As Integer) As Boolean
    Try
      'obsoleta
      Return GetScadenFattura(strDitta, lConto, nAnnpar, strAlfpar, lNumpar, bNonSaldate, _
                              strIntegr, nCodvalu, dttOut, strDatregSaldo, lNumregSaldo, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetScadenFattura(ByVal strDitta As String, ByVal lConto As Integer, _
                                               ByVal nAnnpar As Integer, ByVal strAlfpar As String, _
                                               ByVal lNumpar As Integer, ByVal bNonSaldate As Boolean, _
                                               ByVal strIntegr As String, ByVal nCodvalu As Integer, _
                                               ByRef dttOut As DataTable, ByVal strDatregSaldo As String, _
                                               ByVal lNumregSaldo As Integer, ByVal nRata As Integer) As Boolean
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lConto, nAnnpar, strAlfpar, lNumpar, bNonSaldate, _
                              strIntegr, nCodvalu, dttOut, strDatregSaldo, lNumregSaldo, nRata})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(8), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT * FROM scaden WHERE codditt = " & CStrSQL(strDitta) & _
              " AND sc_conto = " & lConto.ToString & _
              " AND sc_annpar = " & nAnnpar.ToString & _
              " AND sc_alfpar = " & CStrSQL(strAlfpar) & _
              " AND sc_numpar = " & lNumpar.ToString & _
              " AND sc_integr = " & CStrSQL(strIntegr)
      If nRata <> 0 Then strSQL += " AND sc_numrata = " & nRata
      If nCodvalu <> -1 Then strSQL += " AND sc_codvalu = " & nCodvalu
      If bNonSaldate Then
        'le scadenze non saldate, oppure quelle saldate in una determinata registrazione (quella della nota accred a storno fattura ... è il caso della ricontabilizzazione ...)
        strSQL += " AND sc_codincdiff = 0 AND (sc_flsaldato = 'N' OR (sc_flsaldato = 'S' and sc_dtsaldato = " & CDataSQL(strDatregSaldo) & " AND sc_rgsaldato = " & lNumregSaldo & " AND sc_rgsaldato <> 0))"
      End If
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetScadenNumMaxRata(ByVal strDitta As String, ByVal lConto As Integer, ByVal nAnnpar As Integer, _
                                                  ByVal strAlfpar As String, ByVal lNumpar As Integer, ByVal strIntegr As String) As Integer
    '-----------------------------------------
    'ottengo il numero di rata più alto per la partita indicata
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim nOut As Integer = 0

    Try
      strSQL = "SELECT max(sc_numrata) as RATA FROM scaden " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND sc_conto = " & lConto & _
                " AND sc_annpar = " & nAnnpar & _
                " AND sc_alfpar = " & CStrSQL(strAlfpar) & _
                " AND sc_numpar = " & lNumpar & _
                " AND sc_integr = " & CStrSQL(strIntegr)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        nOut = NTSCInt(dttTmp.Rows(0)!RATA)
      End If
      dttTmp.Clear()

      Return nOut

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return 0
    End Try
  End Function

  Public Overridable Function GetContoPerWripn(ByVal strDitta As String, ByVal lConto As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT an_conto, an_flci, an_partite, an_scaden, an_sosppr, an_accperi, an_tpsogiva, an_tipacq, an_descr1, an_tipacq " & _
               " FROM anagra WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lConto.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetScadenFromDDTsFattDiffEm(ByVal strDitta As String, ByVal strTiporkFatt As String, _
                                                          ByVal nAnnFatt As Integer, ByVal strAlfFatt As String, _
                                                          ByVal lNumfat As Integer, ByRef dttOut As DataTable, _
                                                          ByVal strDatregSaldo As String, ByVal lNumregSaldo As Integer, _
                                                          ByVal strIntegr As String, ByVal nCodvalu As Integer) As Boolean
    Try
      'obsoleta
      Return GetScadenFromDDTsFattDiffEm(strDitta, strTiporkFatt, nAnnFatt, strAlfFatt, lNumfat, dttOut, _
                                         strDatregSaldo, lNumregSaldo, strIntegr, nCodvalu, "0", False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetScadenFromDDTsFattDiffEm(ByVal strDitta As String, ByVal strTiporkFatt As String, _
                                                           ByVal nAnnFatt As Integer, ByVal strAlfFatt As String, _
                                                           ByVal lNumfat As Integer, ByRef dttOut As DataTable, _
                                                           ByVal strDatregSaldo As String, ByVal lNumregSaldo As Integer, _
                                                           ByVal strIntegr As String, ByVal nCodvalu As Integer, _
                                                           ByVal strUsaContoFattDoc As String, ByVal bUsaContoFatt As Boolean) As Boolean
    'restituisce le scadenze non saldate (o, se ricontabilizzaz, saldate con la reg. di incasso che poi cancellerò ...)
    'collegate ai ddt collegati alla fattura da contabilizzare
    Dim strSQL As String = ""
    Dim strSQLConto As String = ""
    Dim dttDDT As New DataTable
    Dim dttScad As New DataTable
    Dim i As Integer = 0
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTiporkFatt, nAnnFatt, strAlfFatt, lNumfat, dttOut, _
                                             strDatregSaldo, lNumregSaldo, strIntegr, nCodvalu, strUsaContoFattDoc, _
                                             bUsaContoFatt})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(5), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      dttOut.Clear()

      'ottengo i ddt collegati al fattura
      If strUsaContoFattDoc = "-1" Or strUsaContoFattDoc = "2" Then
        strSQLConto = "CASE WHEN tm_contfatt = 0 THEN tm_conto ELSE tm_contfatt END As tm_conto"
      Else
        If bUsaContoFatt Then
          strSQLConto = "CASE WHEN an_contfatt = 0 THEN tm_conto ELSE an_contfatt END As tm_conto"
        Else
          strSQLConto = "tm_conto"
        End If
      End If

      strSQL = "SELECT " & strSQLConto & ", tm_anno, tm_serie, tm_numdoc, " & _
               " tm_pagato + tm_pagato2 as tm_pagato, tm_abbuono, tm_abbuonov, an_partite, an_scaden, tm_datregin, tm_numrgin " & _
               " FROM testmag INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto " & _
               " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
               " AND tm_tipork = 'B' AND tm_tiporkfat = " & CStrSQL(strTiporkFatt) & _
               " AND tm_annfat = " & nAnnFatt & " AND tm_alffat = " & CStrSQL(strAlfFatt) & _
               " AND tm_numfat = " & lNumfat
      dttDDT = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)


      For Each dtrT As DataRow In dttDDT.Rows
        'per ogni ddt trovo le scadenze non saldate
        'le scadenze non saldate, oppure quelle saldate in una determinata registrazione (quella dell'incasso Ft diff em ... è il caso della ricontabilizzazione ...)
        'devo prendere solo gli acconti (sc_importo < 0), visto che potrei avere delle fatture emesse aventi lo stesso numero del ddt di acconto!
        strSQL = "SELECT *, " & CDblSQL(NTSCDec(dtrT!tm_abbuono)) & " as tm_abbuono, " & _
               CDblSQL(NTSCDec(dtrT!tm_abbuonov)) & " as tm_abbuonov " & _
               " FROM scaden WHERE codditt = " & CStrSQL(strDitta) & _
               " AND sc_conto = " & dtrT!tm_conto.ToString & _
               " AND sc_annpar = " & dtrT!tm_anno.ToString & _
               " AND sc_alfpar = " & CStrSQL(dtrT!tm_serie) & _
               " AND sc_numpar = " & dtrT!tm_numdoc.ToString & _
               " AND sc_integr = " & CStrSQL(strIntegr) & _
               " AND sc_codvalu = " & nCodvalu & _
               " AND sc_importo < 0" & _
               " AND (sc_flsaldato = 'N' OR (sc_flsaldato = 'S' and sc_dtsaldato = " & CDataSQL(strDatregSaldo) & " AND sc_rgsaldato = " & lNumregSaldo & " AND sc_rgsaldato <> 0))"
        dttScad = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

        If dttOut.Columns.Count = 0 Then dttOut = dttScad.Clone()

        
        If NTSCStr(dtrT!an_scaden) = "N" And NTSCStr(dtrT!an_partite) = "S" Then
          'se il conto è gestito solo a partite non trovo nulla:
          'aggiungo un record per identificare la partita
          If NTSCInt(dtrT!tm_numrgin) <> 0 Then
            dttOut.Rows.Add(dttOut.NewRow)
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_tippaga = -1 'ovvero non gestito a scadenze!!!!
            dttOut.Rows(dttOut.Rows.Count - 1)!codditt = strDitta
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_integr = strIntegr
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_datreg = dtrT!tm_datregin
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_numreg = dtrT!tm_numrgin
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_conto = dtrT!tm_conto
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_annpar = dtrT!tm_anno
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_alfpar = dtrT!tm_serie
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_numpar = dtrT!tm_numdoc
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_numrata = 1
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_importo = NTSCDec(dtrT!tm_pagato) * -1  'su scaden sarebbe in AVERE!!!
            'partita della fattura
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_anndist = nAnnFatt
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_alfdoc = strAlfFatt
            dttOut.Rows(dttOut.Rows.Count - 1)!sc_numdoc = lNumfat
          End If
        Else
          'copio le scad nel datatable di uscita (un datatable unico per tutte le scadenze di tutti i ddt emessi)
          For i = 0 To dttScad.Rows.Count - 1
            dttOut.ImportRow(dttScad.Rows(i))
          Next
          dttScad.Clear()
        End If
      Next

      dttOut.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttDDT.Clear()
      dttScad.Clear()
    End Try
  End Function

  Public Overridable Function GetMovmagCA2(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, _
                                      ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal nControp As Integer, _
                                      ByVal dDtIniz As String, ByVal dDtFin As String, ByRef dttOut As DataTable) As Boolean
    Try
      'obsoleta
      Return GetMovmagCA2(strDitta, strTipork, nAnno, strSerie, lNumdoc, nControp, dDtIniz, dDtFin, dttOut, "N", 0, Nothing, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetMovmagCA2(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, _
                                        ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal nControp As Integer, _
                                        ByVal dDtIniz As String, ByVal dDtFin As String, ByRef dttOut As DataTable, _
                                        ByVal strRaggr As String, ByVal lIdRagg As Integer, ByRef dttTmOrig As DataTable, _
                                        ByVal bCaDcRaggruppaCorr As Boolean) As Boolean
    'ottengo le righe del corpo del documento da collegare alla riga di CG:
    'se documento immediato devo prendere movmag
    'se documento differito emesso devo prendere movmag collegato fattura
    'se documento differito ricevuto devo prendere o movmag o, se contiene carico da prod, lavcent. RICORDA: in lavcent c'è ala max una sola riga (carico prod terzista)!!!!
    Dim strSQL As String = ""
    Dim strWhere As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, nAnno, strSerie, lNumdoc, nControp, dDtIniz, _
                                             dDtFin, dttOut, strRaggr, lIdRagg, dttTmOrig, bCaDcRaggruppaCorr})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(8), DataTable)
        dttTmOrig = CType(oIn(11), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      If strTipork = "K" Or strTipork = "D" Or strTipork = "P" Or strTipork = "£" Or strTipork = "(" Then ' cerca le righe delle bolle associate
        'documenti differiti
        strSQL = "SELECT mm_contocontr, " & _
                " mm_codcena, " & _
                " mm_codcfam, " & _
                " mm_commeca, " & _
                " mm_subcommeca, " & _
                " mm_pmtaskid, " & _
                " mm_coddivi, tm_codcli, " & _
                " CASE WHEN tabcaum.tb_testci = 'D' OR tabcaum.tb_testci = 'E' THEN 1 ELSE CASE WHEN tabcaum.tb_testci = 'A' OR tabcaum.tb_testci = 'B' THEN -1 ELSE 0 END END as km_carscar, " & _
                " CASE WHEN tm_tipork = 'T' THEN tabcaum.tb_codcacalv ELSE tabcaum.tb_codcacadd END as tb_codcacadd, " & _
                " mm_codart, mm_fase, tabmaga.tb_codstab, mm_ump, CASE WHEN tm_tipork <> 'T' THEN mm_quant ELSE lce_qtaes END as mm_quant, " & _
                " ac_accperi, ac_codmaca, ac_richcena, ac_richcomm, ac_richcfam, ac_richdivi, ac_richstab, ac_richarti, ac_richcli, " & _
                " CASE WHEN tm_tipork = 'T' THEN " & _
                "         round(round(mm_prezzo * CASE WHEN mm_umprz <> 'S' THEN mm_quant ELSE mm_colli END / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100, " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") " & _
                "      ELSE " & _
                "         mm_valore " & _
                "      END as mm_valore, " & _
                " CASE WHEN tm_tipork = 'T' THEN " & _
                "         round(round(mm_prezvalc * CASE WHEN mm_umprz <> 'S' THEN mm_quant ELSE mm_colli END / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100, " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") " & _
                "      ELSE " & _
                "         mm_valorev " & _
                "      END as mm_valorev, " & _
                " mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, lce_rigaa, mm_magaz, mm_magaz2 " & _
                " FROM (((((" & strJoinTestmagMovmag & ") " & _
                " LEFT JOIN lavcent ON movmag.codditt = lavcent.codditt AND movmag.mm_tipork = lavcent.lce_tipork " & _
                " AND movmag.mm_anno = lavcent.lce_anno AND movmag.mm_serie = lavcent.lce_serie " & _
                " AND movmag.mm_numdoc = lavcent.lce_numdoc AND movmag.mm_riga = lavcent.lce_riga) " & _
                " INNER JOIN KEYMAG ON movmag.codditt = keymag.codditt AND movmag.mm_riga = keymag.km_riga" & _
                " AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_serie = keymag.km_serie" & _
                " AND movmag.mm_anno = keymag.km_anno AND movmag.mm_numdoc = keymag.km_numdoc) " & _
                " INNER JOIN tabmaga ON keymag.codditt = tabmaga.codditt AND keymag.km_magaz = tabmaga.tb_codmaga)" & _
                " INNER JOIN ANAGCA ON movmag.codditt = anagca.codditt AND movmag.mm_contocontr = anagca.ac_conto) " & _
                " INNER JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum "
        strSQL = strSQL & " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                " AND mm_controp = " & nControp & " AND tabcaum.tb_testci <> ' ' " & _
                " AND mm_stasino <> 'X' " & _
                " AND CASE WHEN ac_accperi <> 'N' THEN mm_datini ELSE " & CDataSQL(dDtIniz) & " END = " & CDataSQL(dDtIniz) & _
                " AND CASE WHEN ac_accperi <> 'N' THEN mm_datfin ELSE " & CDataSQL(dDtFin) & " END = " & CDataSQL(dDtFin) & _
                " and testmag.tm_tiporkfat = '" & strTipork & "' and testmag.tm_annfat = " & nAnno & _
                " and testmag.tm_alffat = '" & strSerie & "' and testmag.tm_numfat = " & lNumdoc & _
                " AND CASE WHEN tm_tipork = 'T' THEN " & _
                "         round(round(mm_prezzo * CASE WHEN mm_umprz <> 'S' THEN mm_quant ELSE mm_colli END / mm_perqta * (100 - mm_scont1) / 100 * (100 - mm_scont2) / 100 * (100 - mm_scont3) / 100 * (100 - mm_scont4) / 100 * (100 - mm_scont5) / 100 * (100 - mm_scont6) / 100, " & TrovaNdec(0) & ") * (100 - tm_scont1) / 100 * (100 - tm_scont2) / 100 * (100 - tm_scopag) / 100, " & TrovaNdec(0) & ") " & _
                "      ELSE " & _
                "         mm_valore " & _
                "      END <> 0" & _
                " ORDER BY mm_tipork, mm_serie, mm_numdoc, mm_riga, lce_rigaa "
      Else
        'documenti immediati
        strSQL = "SELECT mm_contocontr, mm_ump, tabcaum.tb_codcacadd, mm_magaz, mm_magaz2, " & _
                 " CASE WHEN tabcaum.tb_testci = 'D' OR tabcaum.tb_testci = 'E' THEN 1 ELSE CASE WHEN tabcaum.tb_testci = 'A' OR tabcaum.tb_testci = 'B' THEN -1 ELSE 0 END END as km_carscar, " & _
                 " ac_accperi, ac_codmaca, ac_richcena, ac_richcomm, ac_richcfam, ac_richdivi, ac_richstab, ac_richarti, ac_richcli, "

        If bCaDcRaggruppaCorr And strRaggr = "S" And Not dttTmOrig Is Nothing Then
          'corrispettivi o ricevute fiscali emesse da registrare in CG RAGGRUPPATI e devo raggruppare anche la CA2
          strSQL += " CASE WHEN ac_richarti = 'S' THEN mm_codart ELSE ' ' END as mm_codart, " & _
                    " CASE WHEN ac_richarti = 'S' THEN mm_fase ELSE 0 END as mm_fase, " & _
                    " CASE WHEN ac_richdivi = 'S' THEN mm_coddivi ELSE 0 END as mm_coddivi, " & _
                    " CASE WHEN ac_richstab = 'S' THEN tb_codstab ELSE 0 END as tb_codstab, " & _
                    " CASE WHEN ac_richcfam = 'S' THEN mm_codcfam ELSE ' ' END as mm_codcfam, " & _
                    " CASE WHEN ac_richcena = 'S' THEN mm_codcena ELSE 0 END as mm_codcena, " & _
                    " CASE WHEN ac_richcomm = 'S' THEN mm_commeca ELSE 0 END as mm_commeca, " & _
                    " CASE WHEN ac_richcomm = 'S' THEN mm_subcommeca ELSE ' ' END as mm_subcommeca, " & _
                    " CASE WHEN ac_richcomm = 'S' THEN mm_pmtaskid ELSE 0 END as mm_pmtaskid, " & _
                    " CASE WHEN ac_richcli = 'S' THEN tm_codcli ELSE 0 END as tm_codcli, " & _
                    " sum(mm_quant) as mm_quant, sum(mm_valore) as mm_valore, sum(mm_valorev) as mm_valorev, ' ' as mm_tipork, 0 as mm_anno, " & _
                    "' ' as mm_serie, 0 mm_numdoc, 0 as mm_riga, 0 as lce_rigaa "
        Else
          strSQL += " mm_codart, mm_fase, mm_coddivi, tb_codstab, mm_codcfam, mm_codcena, mm_commeca, mm_subcommeca, mm_pmtaskid, " & _
                    " tm_codcli, mm_quant, mm_valore, mm_valorev, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, " & _
                    " 0 as lce_rigaa "
          'caso normale: in CA2 scrivo una riga per ogni riga di movmag
        End If

        strSQL += " FROM ((((" & strJoinTestmagMovmag & ") " & _
                 " INNER JOIN KEYMAG ON movmag.codditt = keymag.codditt AND movmag.mm_riga = keymag.km_riga" & _
                 " AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_serie = keymag.km_serie" & _
                 " AND movmag.mm_anno = keymag.km_anno AND movmag.mm_numdoc = keymag.km_numdoc) " & _
                 " INNER JOIN tabmaga ON keymag.codditt = tabmaga.codditt AND keymag.km_magaz = tabmaga.tb_codmaga)" & _
                 " INNER JOIN ANAGCA ON movmag.codditt = anagca.codditt AND movmag.mm_contocontr = anagca.ac_conto)" & _
                 " INNER JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum " & _
                 " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                 " AND movmag.mm_controp = " & nControp & " AND tabcaum.tb_testci <> ' ' AND mm_contocontr <> 0 " & _
                 " AND mm_stasino <> 'X' " & _
                 " AND CASE WHEN ac_accperi <> 'N' THEN mm_datini ELSE " & CDataSQL(dDtIniz) & " END = " & CDataSQL(dDtIniz) & _
                 " AND CASE WHEN ac_accperi <> 'N' THEN mm_datfin ELSE " & CDataSQL(dDtFin) & " END = " & CDataSQL(dDtFin) & _
                 " AND (mm_valore <> 0 OR mm_quant <> 0) "

        If strRaggr = "S" And Not dttTmOrig Is Nothing Then
          'corrispettivi raggruppati MA CA2 scritta in dettaglio: devo tirare su tutti i documenti di magazzino raggruppati nella registraz. di CG
          For Each dtrT As DataRow In dttTmOrig.Select("xx_dtttm = " & lIdRagg.ToString)
            strWhere += " (mm_tipork = " & CStrSQL(dtrT!tm_tipork) & " AND mm_anno = " & NTSCInt(dtrT!tm_anno) & " AND mm_serie = " & CStrSQL(dtrT!tm_serie) & " AND mm_numdoc = " & NTSCInt(dtrT!tm_numdoc) & ") OR "
          Next
          If strWhere.Length > 0 Then strWhere = strWhere.Substring(0, strWhere.Length - 4)
          strSQL += " AND (" & strWhere & ")"
        Else
          'caso standard
          strSQL += " and movmag.mm_tipork = '" & strTipork & "' and movmag.mm_anno = " & nAnno & _
                    " and movmag.mm_serie = '" & strSerie & "' and movmag.mm_numdoc = " & lNumdoc
        End If

        If bCaDcRaggruppaCorr And strRaggr = "S" And Not dttTmOrig Is Nothing Then
          strSQL += "GROUP BY mm_contocontr, mm_ump, tabcaum.tb_codcacadd, mm_magaz, mm_magaz2, " & _
                   " CASE WHEN tabcaum.tb_testci = 'D' OR tabcaum.tb_testci = 'E' THEN 1 ELSE CASE WHEN tabcaum.tb_testci = 'A' OR tabcaum.tb_testci = 'B' THEN -1 ELSE 0 END END, " & _
                   " ac_accperi, ac_codmaca, ac_richcena, ac_richcomm, ac_richcfam, ac_richdivi, ac_richstab, ac_richarti, ac_richcli, " & _
                   " CASE WHEN ac_richarti = 'S' THEN mm_codart ELSE ' ' END, " & _
                   " CASE WHEN ac_richarti = 'S' THEN mm_fase ELSE 0 END, " & _
                   " CASE WHEN ac_richdivi = 'S' THEN mm_coddivi ELSE 0 END, " & _
                   " CASE WHEN ac_richstab = 'S' THEN tb_codstab ELSE 0 END, " & _
                   " CASE WHEN ac_richcfam = 'S' THEN mm_codcfam ELSE ' ' END, " & _
                   " CASE WHEN ac_richcena = 'S' THEN mm_codcena ELSE 0 END, " & _
                   " CASE WHEN ac_richcomm = 'S' THEN mm_commeca ELSE 0 END, " & _
                   " CASE WHEN ac_richcomm = 'S' THEN mm_subcommeca ELSE ' ' END, " & _
                   " CASE WHEN ac_richcomm = 'S' THEN mm_pmtaskid ELSE 0 END, " & _
                   " CASE WHEN ac_richcli = 'S' THEN tm_codcli ELSE 0 END "
        End If

        strSQL += " ORDER BY mm_anno, mm_serie, mm_numdoc, mm_riga "
      End If    'If strTipork = "K" Or strTipork = "D" Or strTipork = "P" Or strT

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTabPecxInnerAnagca(ByVal strDitta As String, ByRef dttPecx As DataTable, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM tabpecx WHERE codditt = " & CStrSQL(strDitta)
      dttPecx = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      With dttPecx.Rows(0)
        strSQL = "SELECT * FROM anagca WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND ac_conto IN (" & !tb_dcontocoll.ToString & ", " & !tb_dcontocolls.ToString & ", " & _
                 !tb_dcontocolld.ToString & ", " & !tb_acontocoll.ToString & ", " & !tb_acontocolls.ToString & ", " & _
                 !tb_acontocolld.ToString & ")"
      End With
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetContoCaDaContoCGFromTabcove(ByVal strDitta As String, ByVal lContoCG As Integer) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    GetContoCaDaContoCGFromTabcove = 0
    Try
      strSQL = "SELECT TOP 1 tb_concova FROM tabcove WHERE codditt = " & CStrSQL(strDitta) & " AND tb_concove = " & lContoCG.ToString & " AND tb_concova <> 0 ORDER BY tb_codcove ASC"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!tb_concova)
      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function GetAnaditpa(ByVal strDitta As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM anaditpa WHERE codditt = " & CStrSQL(strDitta)
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetProtocolloDoppi(ByVal strDitta As String, ByVal nAnno As Integer, ByRef dttOut As DataTable) As Boolean
    'devo escludere i registri collegati ad attività IVA di tipo autotrasportatore
    Dim strSQL As String = ""
    Try

      strSQL = "SELECT moviva.codditt, mi_tregiva, mi_nregiva, mi_alfpro, mi_numpro, tb_desduri, " & _
              " min(CONVERT(varchar(10), mi_datreg, 126) + '_' + cast(mi_numreg as varchar(9))), " & _
              " max(CONVERT(varchar(10), mi_datreg, 126) + '_' + cast(mi_numreg as varchar(9))) " & _
              " FROM moviva INNER JOIN tabduri ON moviva.codditt = tabduri.codditt " & _
              " AND moviva.mi_tregiva = tabduri.tb_utipreg " & _
              " AND moviva.mi_nregiva = tabduri.tb_unumreg " & _
              " INNER JOIN tabatti ON tabduri.codditt = tabatti.codditt " & _
              " AND tabduri.tb_ucodatti = tabatti.tb_codatti " & _
              " AND tabduri.tb_anno = tabatti.tb_anno " & _
              " WHERE moviva.codditt = " & CStrSQL(strDitta) & _
              " AND year(mi_datreg3) = " & nAnno & _
              " AND mi_tipoivaed <> 'P'" & _
              " AND mi_numpro <> 0" & _
              " AND tabatti.tb_regautotr = 'N'" & _
              " GROUP BY moviva.codditt, mi_tregiva, mi_nregiva, mi_alfpro, mi_numpro, tb_desduri " & _
              " HAVING min(CONVERT(varchar(10), mi_datreg, 126) + '_' + cast(mi_numreg as varchar(9))) <> max(CONVERT(varchar(10), mi_datreg, 126) + '_' + cast(mi_numreg as varchar(9)))"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetContropFromRepcpaga(ByVal strDitta As String, ByVal strCodrepc As String, ByVal nCodpaga As Integer) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT pp_contrinc FROM repcpaga " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND pp_codrepc = " & CStrSQL(strCodrepc) & _
               " AND pp_codpaga = " & nCodpaga.ToString & _
               " AND pp_codpaga <> 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        Return NTSCInt(dttTmp.Rows(0)!pp_contrinc)
      End If
      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetContoCassaRepcpaga(ByVal strDitta As String, ByVal strCodrepc As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT TOP 1 pp_contrinc " & _
               " FROM repcpaga INNER JOIN tabpaga ON repcpaga.pp_codpaga = tabpaga.tb_codpaga" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND pp_codrepc = " & CStrSQL(strCodrepc) & _
               " AND tb_tipincecr = 'C'" & _
               " AND pp_contrinc <> 0" & _
               " ORDER BY tb_codpaga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        Return NTSCInt(dttTmp.Rows(0)!pp_contrinc)
      End If
      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


  Public Overridable Function GetRegPrinot(ByVal strDitta As String, ByVal strDatreg As String, _
                                           ByVal lNumreg As Integer, ByVal strPnInt As String, _
                                           ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM prinot " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND pn_datreg = " & CDataSQL(strDatreg) & _
               " AND pn_numreg = " & lNumreg.ToString & _
               " AND pn_integr = " & CStrSQL(strPnInt)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DeleteRegEmissEff(ByVal strDitta As String, ByVal strDatreg As String, _
                                                ByVal lNumreg As Integer, ByVal strInt As String, _
                                                ByVal bGiroEffettiNoChisCli As Boolean, _
                                                ByVal lContoEff As Integer, ByRef dbConn As DbConnection) As Boolean
    'cancello la registrazione di emissione effetti
    'posso farlo perchè se sono qui vuol dire che l'effetto non è stato presentato in banca
    'può essere solo una reg. classica 'cliente a effetti attivi'
    'la scadenza sul conto effeftti la cancello sempre

    'devo riaprire la scadenza cliente se emiss effetti con chiusura cliente
    'se è di tipo emiss. effetti senza chiusura cliente  devo cancellare anche le scadenze sul conto cli c/RB

    Dim strSQL As String = ""
    Try
      'adesso cancella reg. della prima e della seconda.. e le scadenze di quella partita

      If bGiroEffettiNoChisCli Then
        strSQL = "DELETE FROM scaden " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND sc_datreg = " & CDataSQL(strDatreg) & _
                 " AND sc_numreg = " & lNumreg.ToString & _
                 " AND sc_integr = '" & strInt & "' "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Else
        strSQL = "UPDATE scaden SET sc_flsaldato = 'N', sc_dtsaldato = null, sc_rgsaldato = 0" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND sc_conto <> " & lContoEff & _
                 " AND sc_flsaldato = 'S'" & _
                 " AND sc_dtsaldato = " & CDataSQL(strDatreg) & _
                 " AND sc_rgsaldato = " & lNumreg.ToString & _
                 " AND sc_integr = '" & strInt & "' "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE FROM scaden " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND sc_conto = " & lContoEff & _
                 " AND sc_datreg = " & CDataSQL(strDatreg) & _
                 " AND sc_numreg = " & lNumreg.ToString & _
                 " AND sc_integr = '" & strInt & "' "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      strSQL = "DELETE princomp " & _
               " FROM princomp INNER JOIN prinot ON princomp.codditt = prinot.codditt " & _
               " AND princomp.pnc_datreg = prinot.pn_datreg " & _
               " AND princomp.pnc_numreg = prinot.pn_numreg " & _
               " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
               " AND pn_datreg = " & CDataSQL(strDatreg) & _
               " AND pn_numreg = " & lNumreg.ToString & _
               " AND pn_integr = '" & strInt & "' "
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM PRINOT" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND pn_datreg = " & CDataSQL(strDatreg) & _
                " AND pn_numreg = " & lNumreg.ToString & _
                " AND pn_integr = '" & strInt & "' "
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetProvvigDoc(ByVal strDitta As String, ByVal strTipork As String, _
                                            ByVal nAnno As Integer, ByVal strSerie As String, _
                                            ByVal lNumdoc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT provvig.*, tabcage.tb_tippro " & _
               " FROM provvig INNER JOIN tabcage ON provvig.codditt = tabcage.codditt " & _
               " AND provvig.pv_codage = tabcage.tb_codcage" & _
               " WHERE provvig.codditt = " & CStrSQL(strDitta) & _
               " AND pv_tipdoc = " & CStrSQL(strTipork) & _
               " AND pv_anno = " & nAnno.ToString & _
               " AND pv_serie = " & CStrSQL(strSerie) & _
               " AND pv_numdoc = " & lNumdoc.ToString & _
               " AND pv_origine IN ('M', 'T')"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ImpostaTiposeqrid(ByVal strDitta As String, ByVal lConto As Integer, _
      ByVal lAbi As Integer, ByVal strIdmandrid As String) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se esiste almeno una scadenza con stessi:
      '--- Conto;
      '--- Codice Abi;
      '--- ID-mandato;
      '--- ed è attiva l'opzione di registro: OPZIONI/RIDSepaTipoInvio (default NON attiva)
      '--- il campo "Tipo Scadenza RID" sarà impostato a "RCUR", altrimenti a "FIRST"
      '--------------------------------------------------------------------------------------------------------------
      ImpostaTiposeqrid = "P"
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 sc_conto" & _
        " FROM scaden INNER JOIN tabpaga ON scaden.sc_codpaga = tabpaga.tb_codpaga" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND sc_conto = " & lConto & _
        " AND sc_abi = " & lAbi & _
        " AND sc_idmandrid = " & CStrSQL(strIdmandrid) & _
        " AND tb_tippaga = 1" & _
        " ORDER BY codditt, sc_conto, sc_annpar, sc_alfpar, sc_numpar, sc_numrata, sc_integr"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return "R"
      '--------------------------------------------------------------------------------------------------------------
      Return "P"
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function GestisciFlagChiusura(ByVal strState As String, ByVal strDitta As String, ByVal strTipo As String, ByVal nAnno As Integer, ByVal nNumero As Integer, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lResult As Integer
    Try

      '--------------------------------------------------------------------------------------------------------------
      strSQL = " SELECT * FROM dichint" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND di_tipo = " & CStrSQL(strTipo) & _
               " AND di_anno = " & nAnno & _
               " AND di_numero = " & nNumero & _
               " AND di_tipodich = '1'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count = 0 Then
        Return True
      End If

      If strState = "C" Then
        'la riapro...
        strSQL = " UPDATE dichint SET di_chiusa = 'N'" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND di_tipo = " & CStrSQL(strTipo) & _
                 " AND di_anno = " & nAnno & _
                 " AND di_numero = " & nNumero
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = 0 Then Return False
      Else
        'la chiudo
        strSQL = " UPDATE dichint SET di_chiusa = 'S'" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND di_tipo = " & CStrSQL(strTipo) & _
                 " AND di_anno = " & nAnno & _
                 " AND di_numero = " & nNumero
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = 0 Then Return False
      End If

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function CreaFatturaFigurativa(ByVal strDitta As String, ByVal strDatreg As String, _
    ByVal strMemtregiva As String, ByVal nMemnregiva As Integer, ByVal lContoiva As Integer, _
    ByVal nCausale As Integer, ByVal lTmp As Integer, ByRef dtrTm As DataRow, _
    ByRef bFatturaFigurativaCancellabile As Boolean) As Boolean
    Dim lNumreg As Integer = 1
    Dim lNumregef As Integer = 0
    Dim dImporto As Decimal = 0
    Dim dImponib As Decimal = 0
    Dim dAliqiva As Decimal = 0
    Dim strDatregef As String = ""
    Dim strSQL As String = ""
    Dim strTipacq As String = "R"
    Dim strCampiMOVIVAFIG As String = "codditt, mi_datreg, mi_numreg, mi_riga, mi_tregiva, mi_nregiva, mi_conto," & _
                                      " mi_causale, mi_anno, mi_datdoc, mi_alfdoc, mi_numdoc, mi_darave, mi_importo," & _
                                      " mi_codiva, mi_aliqiva, mi_indetr, mi_imponib, mi_contocf, mi_tipacq, mi_flcee, " & _
                                      " mi_flrsm1, mi_flrsm2"
    Dim dttTmp As New DataTable
    Dim dttMovivafig As New DataTable
    Dim dbConn As DbConnection = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      bFatturaFigurativaCancellabile = True
      '--------------------------------------------------------------------------------------------------------------
      With dtrTm
        strSQL = "SELECT tm_flcont, tm_datregef, tm_numregef FROM testmag" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tm_tipork = " & CStrSQL(NTSCStr(!tm_tipork)) & _
          " AND tm_anno = " & NTSCInt(!tm_anno) & _
          " AND tm_serie = " & CStrSQL(NTSCStr(!tm_serie)) & _
          " AND tm_numdoc = " & NTSCInt(!tm_numdoc) & _
          " AND tm_flcont = 'S'"
      End With
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se la Fattura Immediata risulta contabilizzata, cancella l'eventuale record in MOVIVAFIG
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then
        '------------------------------------------------------------------------------------------------------------
        With dttTmp.Rows(0)
          strSQL = "SELECT * FROM movivafig" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND mi_datreg = " & CDataSQL(NTSCStr(!tm_datregef)) & _
            " AND mi_numreg = " & NTSCInt(!tm_numregef)
          dttMovivafig = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttMovivafig.Rows.Count > 0 Then
            '--------------------------------------------------------------------------------------------------------
            '--- Se è contabilizzata, controlla che la registrazione in MOVIVAFIG non sia stampata in 'Definitivo'
            '--------------------------------------------------------------------------------------------------------
            With dttMovivafig.Rows(0)
              strSQL = "SELECT TOP 1 tb_utipreg, tb_unumreg, tb_anno FROM tabduri" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND tb_utipreg = " & CStrSQL(NTSCStr(!mi_tregiva)) & _
                " AND tb_unumreg = " & NTSCInt(!mi_nregiva) & _
                " AND tb_anno = " & NTSCInt(!mi_anno) & _
                " AND (tb_udatreg > " & CDataSQL(NTSCStr(!mi_datreg)) & _
                " OR tb_udatreg = " & CDataSQL(NTSCStr(!mi_datreg)) & _
                " AND tb_unureg >= " & NTSCInt(!mi_numreg) & ")" & _
                " AND tb_ftfig = 'S'"
            End With
            If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then
              bFatturaFigurativaCancellabile = False
              Return False
            End If
            '--------------------------------------------------------------------------------------------------------
            '--- E' cancellabile
            '--------------------------------------------------------------------------------------------------------
            lNumregef = NTSCInt(!tm_numregef)
            strDatregef = NTSCStr(!tm_datregef)
            '--------------------------------------------------------------------------------------------------------
          End If
        End With
      End If
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear() : dttTmp.Dispose()
      dttMovivafig.Clear() : dttMovivafig.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se esistente, cancello MOVIVAFIG
      '--------------------------------------------------------------------------------------------------------------
      If (lNumregef <> 0) And (strDatregef.Trim <> "") Then
        strSQL = "DELETE FROM movivafig" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND mi_datreg = " & CDataSQL(strDatregef) & _
          " AND mi_numreg = " & lNumregef
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT MAX(mi_numreg) AS Numero FROM movivafig" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mi_datreg = " & CDataSQL(strDatreg)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then
        If Not dttTmp.Rows(0)!Numero.Equals(DBNull.Value) Then lNumreg = NTSCInt(dttTmp.Rows(0)!Numero) + 1
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Inserisce il record in MOVIVAFIG
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 1 To 8
        If NTSCInt(dtrTm("tm_codiva_" & i.ToString)) <> 0 Then
          '----------------------------------------------------------------------------------------------------------
          dImporto = ArrDbl(NTSCDec(dtrTm("tm_imposta_" & i.ToString)), TrovaNdec(0))
          dImponib = ArrDbl(NTSCDec(dtrTm("tm_imponib_" & i.ToString)), TrovaNdec(0))
          dAliqiva = 0
          strTipacq = "R"
          strSQL = "SELECT tb_aliq, tb_tipacq FROM tabciva" & _
                   " WHERE tb_codciva = " & NTSCInt(dtrTm("tm_codiva_" & i.ToString))
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count > 0 Then
            dAliqiva = NTSCDec(dttTmp.Rows(0)!tb_aliq)
            strTipacq = NTSCStr(dttTmp.Rows(0)!tb_tipacq)
          End If

          dttTmp.Clear()
          dttTmp.Dispose()
          '----------------------------------------------------------------------------------------------------------
          strSQL = "INSERT INTO movivafig (" & strCampiMOVIVAFIG & ")" & _
            " VALUES (" & CStrSQL(strDitta) & ", " & CDataSQL(strDatreg) & ", " & lNumreg & ", " & i & ", " & _
            CStrSQL(strMemtregiva) & ", " & nMemnregiva & ", " & lContoiva & ", " & nCausale & ", " & _
            NTSCInt(dtrTm!tm_anno) & ", " & CDataSQL(NTSCDate(dtrTm!tm_datdoc)) & ", " & _
            CStrSQL(NTSCStr(dtrTm!tm_serie)) & ", " & NTSCInt(dtrTm!tm_numdoc) & ", 'A', " & CDblSQL(dImporto * -1) & ", " & _
            NTSCInt(dtrTm("tm_codiva_" & i.ToString)) & ", " & CDblSQL(dAliqiva) & ", 0, " & CDblSQL(dImponib * -1) & ", " & _
            lTmp & ", " & CStrSQL(strTipacq) & ", " & _
            CStrSQL(IIf(dtrTm!tb_prestserv.ToString = "N", "M", dtrTm!tb_prestserv.ToString).ToString) & ", 'O', 'N')"

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          '----------------------------------------------------------------------------------------------------------
          '--- Aggiorna TESTMAG
          '----------------------------------------------------------------------------------------------------------
          With dtrTm
            strSQL = "UPDATE testmag" & _
              " SET tm_flcont = 'S'," & _
              " tm_datregef = " & CDataSQL(NTSCDate(strDatreg)) & "," & _
              " tm_numregef = " & lNumreg & "," & _
              " tm_nregiva = " & nMemnregiva & "," & _
              " tm_ultagg = " & CDataOraSQL(Now) & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(NTSCStr(!tm_tipork)) & _
              " AND tm_anno = " & NTSCInt(!tm_anno) & _
              " AND tm_serie = " & CStrSQL(NTSCStr(!tm_serie)) & _
              " AND tm_numdoc = " & NTSCInt(!tm_numdoc)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End With
          '----------------------------------------------------------------------------------------------------------
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    Finally
      '--------------------------------------------------------------------------------------------------------------
      If dbConn.State = ConnectionState.Open Then
        If IsInTrans Then ChiudiTrans()
        dbConn.Close()
      End If
      dbConn = Nothing
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear() : dttTmp.Dispose()
      dttMovivafig.Clear() : dttMovivafig.Dispose()
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetPrinotFromPartita(ByVal strDitta As String, ByVal lConto As Integer, _
                                                 ByVal nAnnpar As Integer, ByVal strAlfpar As String, _
                                                 ByVal lNumpar As Integer, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      'devo cercare la fattura. se ho più record (perchè ho degli incassi/pagametni/note accred prendo il doc con data più vecchia = la fattura)
      strSQL = "SELECT TOP 1 pn_datreg, pn_numreg, pn_ieddatregr, pn_iednumregr, pn_iedrigaregr " & _
               " FROM prinot INNER JOIN tabcauc ON prinot.pn_causale = tabcauc.tb_codcauc " & _
               " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
               " AND pn_conto = " & lConto & _
               " AND pn_annpar = " & nAnnpar & _
               " AND pn_alfpar = " & CStrSQL(strAlfpar) & _
               " AND pn_numpar = " & lNumpar & _
               " AND pn_integr = 'N'" & _
               " AND tb_tipmov = 'O'" & _
               " ORDER BY pn_datreg ASC"
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PRINOT")

      If dsOut.Tables("PRINOT").Rows.Count = 0 Then Return True

      'codici IVA disponibili
      strSQL = "SELECT mi_codiva, mi_aliqiva FROM moviva " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mi_datreg = " & CDataSQL(NTSCDate(dsOut.Tables("PRINOT").Rows(0)!pn_datreg)) & _
               " AND mi_numreg = " & NTSCInt(dsOut.Tables("PRINOT").Rows(0)!pn_numreg)
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVIVA", dsOut)

      'saldo fattura
      strSQL = "SELECT sum(pn_importo) as xx_saldo " & _
               " FROM prinot " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND pn_conto = " & lConto & _
               " AND pn_annpar = " & nAnnpar & _
               " AND pn_alfpar = " & CStrSQL(strAlfpar) & _
               " AND pn_numpar = " & lNumpar & _
               " AND pn_integr = 'N'"
      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SALDO", dsOut)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DittaConEmissioneEffettiChiusuraCliente(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT codditt FROM anaditac" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ac_gestefcc = 'S'"
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CoveAggControp(ByRef dsCove As DataSet, ByVal nCodcove As Integer, ByVal nAggControp As Integer) As Integer
    CoveAggControp = nCodcove
    Try
      If nAggControp = 0 Then Return nCodcove
      If nCodcove = 0 Then Return nCodcove
      If dsCove.Tables("TABCOVE").Rows.Count = 0 Then Return nCodcove
      If dsCove.Tables("TABCOVE").Select("tb_codcove = " & nAggControp + nCodcove).Length > 0 Then Return nAggControp + nCodcove
      Return nCodcove

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function AggSottocontiIvaInded(ByVal strDitta As String, ByRef dttControp As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim nControp As Integer = 0
    Dim lContropPrec As Integer = 0
    Dim lContoPrec As Integer = 0

    Try
      For Each dtrT As DataRow In dttControp.Select("lControp <> 0", "lControp ASC")
        If NTSCInt(dtrT!lControp) <> lContoPrec Then
          dtrT!lConto = TrovaContoDaCodcontr(strDitta, NTSCInt(dtrT!lControp))
          nControp += 1
          strSQL += NTSCInt(dtrT!lConto) & ", "
        Else
          dtrT!lConto = lContoPrec
        End If
        lContropPrec = NTSCInt(dtrT!lControp)
        lContoPrec = NTSCInt(dtrT!lConto)
      Next
      If strSQL.Length = 0 Then Return True

      strSQL = strSQL.Substring(0, strSQL.Length - 2)

      'ottengo solo i sottoconti che possono ricevere l'iva indetraibile
      strSQL = "SELECT an_conto, an_ivainded FROM anagra WHERE codditt = " & CStrSQL(strDitta) & _
               " AND an_conto in (" & strSQL & ") AND an_ivainded = 'S'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se tutti i sottoconti possono ricevere l'iva indetraibile, oppure nessun sottoconto la può ricevere, la spalmerò su tutti
      If dttTmp.Rows.Count = 0 Or dttTmp.Rows.Count = nControp Then Return True

      'prima tutti no
      For Each dtrT As DataRow In dttControp.Rows
        dtrT!strIvaInded = "N"
      Next

      'aggiungo i si
      For Each dtrT As DataRow In dttTmp.Rows
        For Each dtrT1 As DataRow In dttControp.Select("lConto = " & dtrT!an_conto.ToString)
          dtrT1!strIvaInded = "S"
        Next
      Next
      dttTmp.Clear()

      dttControp.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


  Public Overridable Function GetMaxCodivaMovmag(ByVal strDitta As String, ByVal strTipork As String, _
                                                 ByVal nAnno As Integer, ByVal strSerie As String, _
                                                 ByVal lNumdoc As Integer) As Integer
    'se è settata l'opzione di prendere la contropartita con imponibile più alto nel corpo del doc ...
    'dovrei fare una query su movmag per fare la sum(mm_valore) group by mm_codiva
    'per non rallentare troppo, ciclo su testmag per i vari cod. IVA
    'dagli imponibili dovrei scartare gli importi delle spese di piede (speinc, speacc, speinb)
    'devo prendere solo i cod con aliquota e scartare le esezioni
    GetMaxCodivaMovmag = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT TOP 1 mm_codiva, sum(mm_valore) as xx_valore " & _
               " FROM" & strJoinTestmagMovmag & _
               " INNER JOIN tabciva ON movmag.mm_codiva = tabciva.tb_codciva " & _
               " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
               " AND tabciva.tb_aliq <> 0"
      If strTipork = "D" Or strTipork = "K" Or strTipork = "P" Or strTipork = "£" Or strTipork = "(" Then
        strSQL += " AND tm_tiporkfat = " & CStrSQL(strTipork) & _
                  " AND tm_annfat = " & nAnno & " AND tm_alffat = " & CStrSQL(strSerie) & _
                  " AND tm_numfat = " & lNumdoc
      Else
        strSQL += " AND tm_tipork = " & CStrSQL(strTipork) & _
                  " AND tm_anno = " & nAnno & " AND tm_serie = " & CStrSQL(strSerie) & _
                  " AND tm_numdoc = " & lNumdoc
      End If
      strSQL += " GROUP BY mm_codiva " & _
                " ORDER BY sum(mm_valore) DESC"

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then GetMaxCodivaMovmag = NTSCInt(dttTmp.Rows(0)!mm_codiva)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


#Region "Movimenti di cassa"
  Public Overridable Function SerieNegozio(ByVal strDitta As String, ByVal strNomePC As String) As String
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_seriedef " & _
               " FROM tabstab " & _
               "  INNER JOIN tabrere ON tabstab.codditt = tabrere.codditt AND tabstab.tb_codstab = tabrere.tb_codstab " & _
               "  INNER JOIN tabrepc ON tabrere.codditt = tabrepc.codditt AND tabrere.tb_codrere = tabrepc.tb_codrere " & _
               " WHERE tabrepc.codditt = " & CStrSQL(strDitta) & _
               "  AND tb_codrepc = " & CStrSQL(strNomePC)

      Return NTSCStr(OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows(0)!tb_seriedef)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return ""
  End Function
  Public Overridable Function TrovaNuovoNumeroPartita(ByVal strDitta As String, ByVal lAnno As Integer, ByVal strSerie As String) As Integer
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ISNULL(MAX(pn_numpar), 0) + 1 AS xx_numpar " & _
               " FROM prinot " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "  AND pn_annpar = " & lAnno & _
               "  AND pn_alfpar = " & CStrSQL(strSerie)

      Return NTSCInt(OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows(0)!xx_numpar)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return 0
  End Function

  Public Overridable Function SalvaRegistrazioneMovimentoCassa(ByVal dttMovCassa As DataTable, ByVal dttPrinot As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim lCounter As Integer = 1
    Dim strQueryPrinot As String = ""
    Try
      strQueryPrinot = "INSERT INTO prinot " & GetQueryInsertField(dttPrinot, "pn_") & " VALUES "

      'Aggiorna la parte contabilizzata
      For Each dtrPrinot As DataRow In dttPrinot.Rows
        strSQL = strQueryPrinot & GetQueryInsertValue(dttPrinot, dtrPrinot, "pn_")

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        If NTSCStr(dttMovCassa.Rows(0)!xx_newcred) = "S" Then AggiungiNuovaScadenza(dtrPrinot, NTSCInt(dttMovCassa.Rows(0)!mc_codpaga), lCounter)
      Next

      For Each dtrMov As DataRow In dttMovCassa.Rows
        strSQL = "UPDATE movcassa SET mc_datreg = " & CDataSQL(NTSCDate(dtrMov!mc_datreg)) & ", " & _
                 "                    mc_numreg = " & NTSCInt(dtrMov!mc_numreg) & _
                 " WHERE codditt = " & CStrSQL(dttPrinot.Rows(0)!codditt) & _
                 "   AND mc_progr = " & NTSCInt(dtrMov!mc_progr)

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggiungiNuovaScadenza(ByVal dtrPrinot As DataRow, ByVal lCodpaga As Integer, ByRef lCounter As Integer) As Boolean
    Dim dttAnagra, dttPaga, dttScaden As New DataTable
    Dim strSQL As String = ""
    Try
      With dtrPrinot
        ValCodiceDb(NTSCStr(!pn_conto), NTSCStr(!codditt), "ANAGRA", "N", "", dttAnagra, "", "", "")

        'se è un sottoconto o se non è gestito a scadenze non devo generare la scadenza,
        If NTSCStr(dttAnagra.Rows(0)!an_tipo) = "S" OrElse NTSCStr(dttAnagra.Rows(0)!an_scaden) <> "S" Then Return True

        ValCodiceDb(NTSCStr(lCodpaga), NTSCStr(!codditt), "TABPAGA", "N", "", dttPaga, "", "", "")

        strSQL = "INSERT INTO scaden (codditt, sc_conto, sc_annpar, sc_alfpar, sc_numpar, " & _
                 " sc_numrata, sc_datsca, sc_dtprevip, sc_datscadorig, " & _
                 " sc_causale, sc_descr, sc_darave, sc_importo, sc_importoda, " & _
                 " sc_insolu, sc_codpaga, sc_codstpg, sc_tippaga, " & _
                 " sc_controp, sc_salcon, " & _
                 " sc_flsta, sc_fldis, sc_dtdist, sc_opdist, sc_coddest, sc_bolli, sc_speins, sc_anneff, sc_numeff, sc_anndist, sc_numdist, " & _
                 " sc_datreg, sc_numreg, sc_rgsaldato, sc_integr, sc_ultagg) " & _
                 " VALUES (" & CStrSQL(!codditt) & ", " & NTSCInt(!pn_conto) & ", " & NTSCInt(!pn_annpar) & ", " & CStrSQL(!pn_alfpar) & ", " & NTSCInt(!pn_numpar) & ", " & _
                 lCounter & ", " & CDataSQL(NTSCDate(!pn_datreg)) & ", " & CDataSQL(NTSCDate(!pn_datdoc)) & ", " & CDataSQL(NTSCDate(!pn_datdoc)) & ", " & _
                 NTSCInt(!pn_causale) & ", ' ', " & CStrSQL(!pn_darave) & ", " & CDblSQL(NTSCDec(!pn_importo)) & ", " & CDblSQL(Math.Abs(NTSCDec(!pn_importo))) & ", " & _
                 " 'N', " & lCodpaga & ", " & NTSCInt(dttPaga.Rows(0)!tb_codstpg) & ", " & NTSCInt(dttPaga.Rows(0)!tb_tippaga) & "," & _
                 " 0, ' ', 'N', 'N', NULL, '', 0, 0, 0, 0, 0, 0, 0, " & _
                 CDataSQL(NTSCDate(!pn_datreg)) & ", " & NTSCInt(!pn_numreg) & ", 0, 'N', " & CDataOraSQL(Now) & ") "

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End With

      lCounter += 1

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function TrovaChiusureDiCassa(ByVal strDitta As String, ByVal strDatini As String, ByVal strDatfin As String) As DataTable
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT codditt, mc_codrepc, mc_datmov, mc_oramovi FROM movcassa " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND mc_causale = 'F' " & _
               "   AND mc_datmov BETWEEN " & CDataSQL(strDatini) & " AND " & CDataSQL(strDatfin)

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return Nothing
  End Function

  Public Overridable Function TrovaMovimentiDiCassa(ByVal dtrChiusura As DataRow, ByVal bContabilizzati As Boolean) As DataTable
    Dim strSQL As String = ""
    Try
      'Partendo dalla data di chiusura calcola la data di apertura più vicina (sottoquery) e prende tutti i movimenti non contabilizzati 
      ' e che devono essere contabilizzati compresi in quelle 2 date.
      strSQL = "SELECT movcassa.* FROM movcassa " & _
               " INNER JOIN tabreto ON tabreto.codditt = movcassa.codditt AND tb_codreto = mc_codreto " & _
               " INNER JOIN (SELECT TOP 1 codditt, mc_datmov, mc_oramovi FROM movcassa " & _
               "             WHERE codditt = " & CStrSQL(dtrChiusura!codditt) & _
               "               AND mc_codrepc = " & CStrSQL(dtrChiusura!mc_codrepc) & _
               "               AND ((mc_datmov = " & CDataSQL(NTSCDate(dtrChiusura!mc_datmov)) & " AND mc_oramovi <= " & CDblSQL(NTSCDec(dtrChiusura!mc_oramovi)) & ")" & _
               "                 OR mc_datmov < " & CDataSQL(NTSCDate(dtrChiusura!mc_datmov)) & ") " & _
               "               AND mc_causale = 'V' " & _
               "             ORDER BY mc_datmov, mc_oramovi) AS tmp ON tmp.codditt = movcassa.codditt AND ((movcassa.mc_datmov = tmp.mc_datmov AND movcassa.mc_oramovi >= tmp.mc_oramovi) " & _
               "                                                                                        OR  movcassa.mc_datmov > tmp.mc_datmov)" & _
               " WHERE movcassa.codditt = " & CStrSQL(dtrChiusura!codditt) & _
               "   AND movcassa.mc_codrepc = " & CStrSQL(dtrChiusura!mc_codrepc) & _
               "   AND ((movcassa.mc_datmov = " & CDataSQL(NTSCDate(dtrChiusura!mc_datmov)) & " AND movcassa.mc_oramovi < " & CDblSQL(NTSCDec(dtrChiusura!mc_oramovi)) & ")" & _
               "      OR movcassa.mc_datmov < " & CDataSQL(NTSCDate(dtrChiusura!mc_datmov)) & ") "

      If bContabilizzati Then
        strSQL &= "   AND movcassa.mc_datreg <> " & CDataSQL(New Date(1900, 1, 1))
      Else
        strSQL &= "   AND movcassa.mc_datreg = " & CDataSQL(New Date(1900, 1, 1)) & _
                  "   AND tb_contab = 'S' "
      End If

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return Nothing
  End Function


  Public Overridable Function EliminaRegistrazioniMovimentiCassa(ByVal strDitta As String, ByVal dttMovimenti As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      For Each dtrRow As DataRow In dttMovimenti.Rows
        strSQL = "DELETE FROM prinot " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND pn_datreg = " & CDataSQL(NTSCDate(dtrRow!mc_datreg)) & _
                 " AND pn_numreg = " & NTSCInt(dtrRow!mc_numreg)

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        strSQL = "UPDATE scaden SET sc_flsaldato = 'N', sc_dtsaldato = " & CDataSQL(New Date(1900, 1, 1)) & ", sc_rgsaldato = 0 " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND sc_dtsaldato = " & CDataSQL(NTSCDate(dtrRow!mc_datreg)) & _
                 " AND sc_rgsaldato = " & NTSCInt(dtrRow!mc_numreg)

        If Execute(strSQL, CLE__APP.DBTIPO.DBAZI) = 0 Then
          'Se la query sopra non ha fatto nulla vado a cancellare la scadenza, in quanto vuol dire che la fattura che aveva generato la scadenza è stata eliminata.
          strSQL = "DELETE FROM scaden " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND sc_datreg = " & CDataSQL(NTSCDate(dtrRow!mc_datreg)) & _
                   " AND sc_numreg = " & NTSCInt(dtrRow!mc_numreg)

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#End Region
End Class
