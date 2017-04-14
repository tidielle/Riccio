Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDORCONF
  Inherits CLD__BASE

  Public strJoinTabelle As String = " INNER JOIN anagra ON anagra.an_conto = testord.td_conto AND anagra.codditt = testord.codditt " & _
                          " LEFT JOIN tabtpbf ON testord.td_tipobf = tabtpbf.tb_codtpbf AND testord.codditt = tabtpbf.codditt " & _
                          " LEFT JOIN tabpaga ON testord.td_codpaga = tabpaga.tb_codpaga" & _
                          " LEFT JOIN tabcage ON testord.td_codagen = tabcage.tb_codcage AND testord.codditt = tabcage.codditt " & _
                          " LEFT JOIN anagra AS anagra_1 ON tabcage.tb_codforn = anagra_1.an_conto AND tabcage.codditt = anagra_1.codditt" & _
                          " LEFT JOIN tabmaga ON testord.td_magaz = tabmaga.tb_codmaga AND testord.codditt = tabmaga.codditt " & _
                          " LEFT JOIN tabvalu ON testord.td_valuta = tabvalu.tb_codvalu" & _
                          " LEFT JOIN tabvett ON testord.td_vettor = tabvett.tb_codvett AND testord.codditt = tabvett.codditt " & _
                          " LEFT JOIN destdiv ON testord.td_coddest = destdiv.dd_coddest AND testord.td_conto = destdiv.dd_conto AND testord.codditt = destdiv.codditt" & _
                          " LEFT JOIN destdiv AS destdiv2 ON testord.td_coddest2 = destdiv2.dd_coddest AND testord.td_conto = destdiv2.dd_conto AND testord.codditt = destdiv2.codditt" & _
                          " LEFT JOIN tabcage AS tabcage_1 ON testord.td_codagen2 = tabcage_1.tb_codcage AND testord.codditt = tabcage_1.codditt " & _
                          " LEFT JOIN anagra AS anagra_2 ON tabcage_1.tb_codforn = anagra_2.an_conto AND tabcage_1.codditt = anagra_2.codditt" & _
                          " LEFT JOIN tabport ON testord.td_porto = tabport.tb_codport AND testord.codditt = tabport.codditt  " & _
                          " LEFT JOIN tabciva ON testord.td_codese = tabciva.tb_codciva" & _
                          " INNER JOIN artico ON artico.ar_codart = movord.mo_codart AND artico.codditt = movord.codditt" & _
                          " LEFT JOIN commess ON movord.mo_commeca = commess.co_comme AND movord.codditt = commess.codditt " & _
                          " LEFT JOIN anagra AS anagra_3 ON commess.co_conto = anagra_3.an_conto AND commess.codditt = anagra_3.codditt" & _
                          " INNER JOIN keyord ON movord.mo_riga = keyord.ko_riga AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_serie = keyord.ko_serie AND movord.mo_anno = keyord.ko_anno AND movord.mo_numord = keyord.ko_numord AND movord.codditt = keyord.codditt" & _
                          " LEFT JOIN codarfo ON keyord.ko_codart = codarfo.caf_codart AND keyord.ko_conto = codarfo.caf_conto AND keyord.codditt = codarfo.codditt" & _
                          " LEFT JOIN analotti ON movord.codditt = analotti.codditt AND movord.mo_codart = analotti.alo_codart AND movord.mo_lotto = analotti.alo_lotto " & _
                          " LEFT JOIN tabciva AS tabciva_1 ON testord.td_codiva_1 = tabciva_1.tb_codciva" & _
                          " LEFT JOIN tabciva AS tabciva_2 ON testord.td_codiva_2 = tabciva_2.tb_codciva" & _
                          " LEFT JOIN tabciva AS tabciva_3 ON testord.td_codiva_3 = tabciva_3.tb_codciva" & _
                          " LEFT JOIN tabciva AS tabciva_4 ON testord.td_codiva_4 = tabciva_4.tb_codciva" & _
                          " LEFT JOIN tabciva AS tabciva_5 ON testord.td_codiva_5 = tabciva_5.tb_codciva" & _
                          " LEFT JOIN tabciva AS tabciva_6 ON testord.td_codiva_6 = tabciva_6.tb_codciva" & _
                          " LEFT JOIN tabciva AS tabciva_7 ON testord.td_codiva_7 = tabciva_7.tb_codciva" & _
                          " LEFT JOIN tabciva AS tabciva_8 ON testord.td_codiva_8 = tabciva_8.tb_codciva"

  Public Overridable Function InglobaQuerySQL(ByVal strQuery As String) As String
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT testord.*, movord.mo_riga, movord.mo_codart, movord.mo_datcons, movord.mo_unmis, movord.mo_descr, movord.mo_colli, movord.mo_coleva, " & _
               "       movord.mo_quant, movord.mo_quaeva, movord.mo_flevas, movord.mo_colpre, movord.mo_quapre, movord.mo_flevapre, movord.mo_prezzo, movord.mo_scont1, " & _
               "       movord.mo_scont2, movord.mo_scont3, movord.mo_scont4, movord.mo_scont5, movord.mo_scont6, movord.mo_scontv, movord.mo_scontp, movord.mo_provv, movord.mo_provv2, movord.mo_codiva, " & _
               "       movord.mo_preziva, movord.mo_prezvalc, movord.mo_note, movord.mo_stasino, movord.mo_prelist, movord.mo_codcfam, " & _
               "       movord.mo_commeca, movord.mo_subcommeca, movord.mo_valore, movord.mo_codcena, movord.mo_desint, movord.mo_codvuo, movord.mo_vprovv, movord.mo_vprovv2, movord.mo_ump, movord.mo_valoremm, " & _
               "       movord.mo_confermato, movord.mo_prezivav, movord.mo_valorev, movord.mo_datconsor, movord.mo_codclie, movord.mo_misura1, movord.mo_misura2, movord.mo_misura3, " & _
               "       movord.mo_ultagg, movord.mo_flkit, movord.mo_ktriga, movord.mo_perqta, movord.mo_fase," & _
               "       (Str(commess.co_conto)+'  '+(anagra_3.an_descr1)) AS xx_cliecom, " & _
               "       tabport.tb_desport, anagra.an_descr1, anagra.an_descr2, anagra.an_indir, anagra.an_cap, anagra.an_citta, anagra.an_prov, anagra.an_stato, " & _
               "       anagra.an_telef, anagra.an_cell, anagra.an_faxtlx, anagra.an_email, anagra.an_contatt, anagra.an_website, anagra.an_usaem, anagra.an_pariva, anagra.an_codfis, tabtpbf.tb_destpbf, tabpaga.tb_despaga, tabcage.tb_descage, anagra_1.an_conto AS an_conto_1, " & _
               "       anagra_1.an_descr1 AS an_descr1_1, anagra_1.an_descr2 AS an_descr2_1, anagra_1.an_indir AS an_indir_1, anagra_1.an_cap AS an_cap_1, anagra_1.an_citta AS an_citta_1, anagra_1.an_prov AS an_prov_1, " & _
               "       anagra_1.an_stato AS an_stato_1, anagra_1.an_telef AS an_telef_1, anagra_1.an_cell AS an_cell_1, anagra_1.an_faxtlx AS an_faxtlx_1, anagra_1.an_email AS an_email_1, anagra_1.an_website AS an_website_1, anagra_1.an_usaem AS an_usaem_1, " & _
               "       tabcage_1.tb_descage AS tb_descage_1, anagra_2.an_conto AS an_conto_2, anagra_2.an_descr1 AS an_descr1_2, anagra_2.an_descr2 AS an_descr2_2, anagra_2.an_indir AS an_indir_2, anagra_2.an_cap AS an_cap_2, " & _
               "       anagra_2.an_citta AS an_citta_2, anagra_2.an_prov AS an_prov_2, anagra_2.an_stato AS an_stato_2, anagra_2.an_telef AS an_telef_2, anagra_2.an_cell AS an_cell_2, anagra_2.an_faxtlx AS an_faxtlx_2, anagra_2.an_email AS an_email_2, " & _
               "       anagra_2.an_website AS an_website_2, anagra_2.an_usaem AS an_usaem_2,tabmaga.tb_desmaga, tabvalu.tb_desvalu, tabvalu.tb_nomvalu, tabvett.tb_desvett, tabvett.tb_desvet2, destdiv.dd_nomdest, " & _
               "       destdiv.dd_inddest, destdiv.dd_capdest, destdiv.dd_locdest, destdiv.dd_prodest, destdiv.dd_telef, artico.ar_note, destdiv.dd_stato, artico.ar_descr, artico.ar_desint, artico.ar_ubicaz, artico.ar_sostit, artico.ar_codalt, artico.ar_gif1, artico.ar_gif2," & _
               "       codarfo.caf_codarfo, analotti.alo_lottox as mo_lotto, artico.ar_flmod," & _
               "       CASE WHEN testord.td_serie=' ' THEN Str(testord.td_numord) ELSE Str(testord.td_numord)+'/'+testord.td_serie END AS xx_numdoc, tabciva.tb_desciva As tb_desciva, " & _
               "       tabciva_1.tb_aliq As Aliq1, tabciva_1.tb_desciva As Desciva1," & _
               "       tabciva_2.tb_aliq As Aliq2, tabciva_2.tb_desciva As Desciva2," & _
               "       tabciva_3.tb_aliq As Aliq3, tabciva_3.tb_desciva As Desciva3," & _
               "       tabciva_4.tb_aliq As Aliq4, tabciva_4.tb_desciva As Desciva4," & _
               "       tabciva_5.tb_aliq As Aliq5, tabciva_5.tb_desciva As Desciva5," & _
               "       tabciva_6.tb_aliq As Aliq6, tabciva_6.tb_desciva As Desciva6," & _
               "       tabciva_7.tb_aliq As Aliq7, tabciva_7.tb_desciva As Desciva7," & _
               "       tabciva_8.tb_aliq As Aliq8, tabciva_8.tb_desciva As Desciva8," & _
               " destdiv2.dd_nomdest AS dd_nomdest2, destdiv2.dd_inddest AS dd_inddest2," & _
               " destdiv2.dd_capdest AS dd_capdest2, destdiv2.dd_locdest AS dd_locdest2," & _
               " destdiv2.dd_prodest AS dd_prodest2, destdiv2.dd_telef AS dd_telef2, " & _
               " destdiv2.dd_stato AS dd_stato2" & _
               " FROM testord " & _
               "  INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno " & _
               "                   AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord " & _
               "  INNER JOIN (" & strQuery & ") AS tmp ON testord.codditt = tmp.codditt" & _
                                                    " AND testord.td_tipork = tmp.td_tipork" & _
                                                    " AND testord.td_anno = tmp.td_anno" & _
                                                    " AND testord.td_serie = tmp.td_serie" & _
                                                    " AND testord.td_numord = tmp.td_numord" & _
                                                    " AND movord.mo_riga = tmp.Riga" & _
               strJoinTabelle

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetQuerySelectFromStampaWord() As String
    Dim strSQL As String = ""
    Try
      'MovOrd è stato spostato in ingloba query, per mostrare tutte le righe di un ordine filtrato. non solo quelle al quale applicare il filtro.
      strSQL = "SELECT DISTINCT testord.codditt, testord.td_tipork, testord.td_anno, testord.td_serie," & _
        " testord.td_numord, movord.mo_riga AS Riga"

      strSQL += " FROM testord " & _
                " INNER JOIN movord ON testord.td_tipork = movord.mo_tipork AND testord.td_serie = movord.mo_serie " & _
                "                  AND testord.td_anno = movord.mo_anno AND testord.td_numord = movord.mo_numord AND testord.codditt = movord.codditt " & _
                strJoinTabelle

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GsorGetQuerySelectFromStampaWord() As String

    Dim strSQL As String = ""
    Try
      strSQL = "SELECT testord.*, movord.mo_riga, movord.mo_codart, movord.mo_datcons, movord.mo_unmis, movord.mo_descr, movord.mo_colli, movord.mo_coleva, " & _
               " movord.mo_quant, movord.mo_quaeva, movord.mo_flevas, movord.mo_colpre, movord.mo_quapre, movord.mo_flevapre, movord.mo_prezzo, movord.mo_scont1, " & _
               " movord.mo_scont2, movord.mo_scont3, movord.mo_scont4, movord.mo_scont5, movord.mo_scont6, movord.mo_scontv, movord.mo_scontp, movord.mo_provv, movord.mo_provv2, movord.mo_codiva, " & _
               " movord.mo_preziva, movord.mo_prezvalc, movord.mo_note, movord.mo_stasino, movord.mo_prelist, movord.mo_codcfam, (Str(commess.co_conto)+'  '+(anagra_3.an_descr1)) AS xx_cliecom, " & _
               " movord.mo_commeca, movord.mo_subcommeca, movord.mo_valore, movord.mo_codcena, movord.mo_desint, movord.mo_codvuo, movord.mo_vprovv, movord.mo_vprovv2, movord.mo_ump, movord.mo_valoremm, " & _
               " movord.mo_confermato, analotti.alo_lottox as mo_lotto, movord.mo_prezivav, movord.mo_valorev, movord.mo_datconsor, movord.mo_codclie, movord.mo_misura1, movord.mo_misura2, movord.mo_misura3, " & _
               " movord.mo_ultagg, movord.mo_flkit, movord.mo_ktriga, movord.mo_perqta, movord.mo_fase, tabport.tb_desport, anagra.an_descr1, anagra.an_descr2, anagra.an_indir, anagra.an_cap, anagra.an_citta, anagra.an_prov, anagra.an_stato, " & _
               " anagra.an_telef, anagra.an_cell, anagra.an_faxtlx, anagra.an_email, anagra.an_contatt, anagra.an_website, anagra.an_usaem, anagra.an_pariva, anagra.an_codfis, tabtpbf.tb_destpbf, tabpaga.tb_despaga, tabcage.tb_descage, anagra_1.an_conto AS an_conto_1, " & _
               " anagra_1.an_descr1 AS an_descr1_1, anagra_1.an_descr2 AS an_descr2_1, anagra_1.an_indir AS an_indir_1, anagra_1.an_cap AS an_cap_1, anagra_1.an_citta AS an_citta_1, anagra_1.an_prov AS an_prov_1, " & _
               " anagra_1.an_stato AS an_stato_1, anagra_1.an_telef AS an_telef_1, anagra_1.an_cell AS an_cell_1, anagra_1.an_faxtlx AS an_faxtlx_1, anagra_1.an_email AS an_email_1, anagra_1.an_website AS an_website_1, anagra_1.an_usaem AS an_usaem_1, " & _
               " tabcage_1.tb_descage AS tb_descage_1, anagra_2.an_conto AS an_conto_2, anagra_2.an_descr1 AS an_descr1_2, anagra_2.an_descr2 AS an_descr2_2, anagra_2.an_indir AS an_indir_2, anagra_2.an_cap AS an_cap_2, " & _
               " anagra_2.an_citta AS an_citta_2, anagra_2.an_prov AS an_prov_2, anagra_2.an_stato AS an_stato_2, anagra_2.an_telef AS an_telef_2, anagra_2.an_cell AS an_cell_2, anagra_2.an_faxtlx AS an_faxtlx_2, anagra_2.an_email AS an_email_2, " & _
               " anagra_2.an_website AS an_website_2, anagra_2.an_usaem AS an_usaem_2,tabmaga.tb_desmaga, tabvalu.tb_desvalu, tabvalu.tb_nomvalu, tabvett.tb_desvett, tabvett.tb_desvet2, destdiv.dd_nomdest, " & _
               " destdiv.dd_inddest, destdiv.dd_capdest, destdiv.dd_locdest, destdiv.dd_prodest, destdiv.dd_telef, artico.ar_note, destdiv.dd_stato, artico.ar_descr, artico.ar_desint, artico.ar_ubicaz, artico.ar_sostit, artico.ar_codalt, artico.ar_gif1, artico.ar_gif2," & _
               " codarfo.caf_codarfo, artico.ar_flmod," & _
               " CASE WHEN td_serie=' ' THEN Str(td_numord) ELSE Str(td_numord)+'/'+td_serie END AS xx_numdoc, tabciva.tb_desciva As tb_desciva, " & _
               " tabciva_1.tb_aliq As Aliq1, tabciva_1.tb_desciva As Desciva1," & _
               " tabciva_2.tb_aliq As Aliq2, tabciva_2.tb_desciva As Desciva2," & _
               " tabciva_3.tb_aliq As Aliq3, tabciva_3.tb_desciva As Desciva3," & _
               " tabciva_4.tb_aliq As Aliq4, tabciva_4.tb_desciva As Desciva4," & _
               " tabciva_5.tb_aliq As Aliq5, tabciva_5.tb_desciva As Desciva5," & _
               " tabciva_6.tb_aliq As Aliq6, tabciva_6.tb_desciva As Desciva6," & _
               " tabciva_7.tb_aliq As Aliq7, tabciva_7.tb_desciva As Desciva7," & _
               " tabciva_8.tb_aliq As Aliq8, tabciva_8.tb_desciva As Desciva8," & _
               " destdiv2.dd_nomdest AS dd_nomdest2, destdiv2.dd_inddest AS dd_inddest2," & _
               " destdiv2.dd_capdest AS dd_capdest2, destdiv2.dd_locdest AS dd_locdest2," & _
               " destdiv2.dd_prodest AS dd_prodest2, destdiv2.dd_telef AS dd_telef2, " & _
               " destdiv2.dd_stato AS dd_stato2" & _
               " FROM anagra INNER JOIN testord LEFT JOIN tabtpbf ON testord.td_tipobf = tabtpbf.tb_codtpbf AND testord.codditt = tabtpbf.codditt LEFT JOIN tabpaga ON testord.td_codpaga = tabpaga.tb_codpaga" & _
               " LEFT JOIN tabcage ON testord.td_codagen = tabcage.tb_codcage AND testord.codditt = tabcage.codditt LEFT JOIN anagra AS anagra_1 ON tabcage.tb_codforn = anagra_1.an_conto AND tabcage.codditt = anagra_1.codditt" & _
               " LEFT JOIN tabmaga ON testord.td_magaz = tabmaga.tb_codmaga AND testord.codditt = tabmaga.codditt LEFT JOIN tabvalu ON testord.td_valuta = tabvalu.tb_codvalu" & _
               " LEFT JOIN tabvett ON testord.td_vettor = tabvett.tb_codvett AND testord.codditt = tabvett.codditt LEFT JOIN destdiv ON testord.td_coddest = destdiv.dd_coddest AND testord.td_conto = destdiv.dd_conto AND testord.codditt = destdiv.codditt" & _
               " LEFT JOIN tabcage AS tabcage_1 ON testord.td_codagen2 = tabcage_1.tb_codcage AND testord.codditt = tabcage_1.codditt LEFT JOIN anagra AS anagra_2 ON tabcage_1.tb_codforn = anagra_2.an_conto AND tabcage_1.codditt = anagra_2.codditt" & _
               " LEFT JOIN tabport ON testord.td_porto = tabport.tb_codport AND testord.codditt = tabport.codditt ON anagra.an_conto = testord.td_conto AND anagra.codditt = testord.codditt LEFT JOIN tabciva ON testord.td_codese = tabciva.tb_codciva" & _
               " INNER JOIN artico INNER JOIN movord LEFT JOIN commess ON movord.mo_commeca = commess.co_comme AND movord.codditt = commess.codditt " & _
               " LEFT JOIN anagra AS anagra_3 ON commess.co_conto = anagra_3.an_conto AND commess.codditt = anagra_3.codditt" & _
               " INNER JOIN keyord LEFT JOIN codarfo ON keyord.ko_codart = codarfo.caf_codart AND keyord.ko_conto = codarfo.caf_conto AND keyord.codditt = codarfo.codditt" & _
               " ON movord.mo_riga = keyord.ko_riga AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_serie = keyord.ko_serie AND movord.mo_anno = keyord.ko_anno AND movord.mo_numord = keyord.ko_numord AND movord.codditt = keyord.codditt" & _
               " ON artico.ar_codart = movord.mo_codart AND artico.codditt = movord.codditt" & _
               " ON testord.td_tipork = movord.mo_tipork AND testord.td_serie = movord.mo_serie AND testord.td_anno = movord.mo_anno AND testord.td_numord = movord.mo_numord AND testord.codditt = movord.codditt" & _
               " LEFT JOIN analotti ON movord.codditt = analotti.codditt AND movord.mo_codart = analotti.alo_codart AND movord.mo_lotto = analotti.alo_lotto " & _
               " LEFT JOIN tabciva AS tabciva_1 ON testord.td_codiva_1 = tabciva_1.tb_codciva" & _
               " LEFT JOIN tabciva AS tabciva_2 ON testord.td_codiva_2 = tabciva_2.tb_codciva" & _
               " LEFT JOIN tabciva AS tabciva_3 ON testord.td_codiva_3 = tabciva_3.tb_codciva" & _
               " LEFT JOIN tabciva AS tabciva_4 ON testord.td_codiva_4 = tabciva_4.tb_codciva" & _
               " LEFT JOIN tabciva AS tabciva_5 ON testord.td_codiva_5 = tabciva_5.tb_codciva" & _
               " LEFT JOIN tabciva AS tabciva_6 ON testord.td_codiva_6 = tabciva_6.tb_codciva" & _
               " LEFT JOIN tabciva AS tabciva_7 ON testord.td_codiva_7 = tabciva_7.tb_codciva" & _
               " LEFT JOIN tabciva AS tabciva_8 ON testord.td_codiva_8 = tabciva_8.tb_codciva" & _
               " LEFT JOIN destdiv AS destdiv2 ON testord.td_coddest2 = destdiv2.dd_coddest AND testord.td_conto = destdiv2.dd_conto AND testord.codditt = destdiv2.codditt"

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GsofGetQuerySelectFromStampaWord() As String
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT testoff.*, movoff.*, organig.*," & _
               "       leads.le_descr1, leads.le_telef, leads.le_cell, leads.le_faxtlx, leads.le_email, leads.le_website," & _
               "       leads.le_usaem, leads.le_contatt, leads.le_pariva, leads.le_codfis, leads.le_descr1, leads.le_descr2," & _
               "       leads.le_indir, leads.le_cap, leads.le_citta, leads.le_prov, leads.le_stato," & _
               "       opportun.op_oggetto, (organig.og_descont2 + ' ' + organig.og_descont) As Desopinc," & _
               "       tabtpbf.tb_destpbf, tabpaga.tb_despaga, tabcage.tb_descage," & _
               "       tabvalu.tb_desvalu, tabvalu.tb_nomvalu," & _
               "       tabport.tb_desport, anagra.an_descr1," & _
               "       destdiv.dd_nomdest, destdiv.dd_inddest, destdiv.dd_capdest, destdiv.dd_locdest, destdiv.dd_prodest, destdiv.dd_telef, destdiv.dd_stato," & _
               "       destdiv_1.dd_nomdest AS dd_nomdest_1, destdiv_1.dd_inddest AS dd_inddest_1, destdiv_1.dd_capdest AS dd_capdest_1, " & _
               "       destdiv_1.dd_locdest AS dd_locdest_1, destdiv_1.dd_prodest AS dd_prodest_1, destdiv_1.dd_telef AS dd_telef_1, destdiv_1.dd_stato AS dd_stato_1, " & _
               "       tabciva.tb_desciva, tabcage_1.tb_descage AS tb_descage_1," & _
               "       anagra_1.an_conto As an_conto_1, anagra_1.an_descr1 As an_descr1_1, anagra_1.an_descr2 As an_descr2_2, anagra_1.an_indir As an_indir_1, anagra_1.an_cap As an_cap_1, anagra_1.an_citta As an_citta_1, anagra_1.an_prov As an_prov_1, anagra_1.an_stato As an_stato_1, anagra_1.an_faxtlx As an_faxtlx_1, anagra_1.an_email As an_email_1, anagra_1.an_usaem As an_usaem_1, anagra_1.an_telef As an_telef_1, anagra_1.an_cell As an_cell_1, anagra_1.an_website As an_website_1, " & _
               "       anagra_2.an_conto As an_conto_2, anagra_2.an_descr1 As an_descr1_2, anagra_2.an_descr2 As an_descr2_3, anagra_2.an_indir As an_indir_2, anagra_2.an_cap As an_cap_2, anagra_2.an_citta As an_citta_2, anagra_2.an_prov As an_prov_2, anagra_2.an_stato As an_stato_2, anagra_2.an_faxtlx As an_faxtlx_2, anagra_2.an_email As an_email_2, anagra_2.an_usaem As an_usaem_2, anagra_2.an_telef As an_telef_2, anagra_2.an_cell As an_cell_2, anagra_2.an_website As an_website_2, " & _
               "       artico.ar_note, artico.ar_descr, artico.ar_desint, artico.ar_ubicaz, artico.ar_sostit," & _
               "       artico.ar_flmod, artico.ar_codalt, artico.ar_gif1, artico.ar_gif2, tabciva.tb_desciva," & _
               "       movoff.mo_prezzo * movoff.mo_quant * (100 - movoff.mo_scont1) / 100 * (100 - movoff.mo_scont2) / 100 * (100 - movoff.mo_scont3) / 100 * (100 - testoff.td_scont1) / 100 * (100 - testoff.td_scont2) / 100 AS xx_valore," & _
               "       movoff.mo_prezvalc * movoff.mo_quant * (100 - movoff.mo_scont1) / 100 * (100 - movoff.mo_scont2) / 100 * (100 - movoff.mo_scont3) / 100*(100 - testoff.td_scont1) / 100 * (100 - testoff.td_scont2) / 100 AS xx_valorev," & _
               "       movoff.mo_preziva * movoff.mo_quant * (100 - movoff.mo_scont1) / 100 * (100 - movoff.mo_scont2) / 100 * (100 - movoff.mo_scont3) / 100*(100 - testoff.td_scont1) / 100 * (100 - testoff.td_scont2) / 100 AS xx_valoreic," & _
               "       CASE WHEN td_serie = ' ' THEN  cast(td_numord as varchar) + ' rev. ' + cast(td_vers as varchar) ELSE cast(td_numord as varchar) + '/' + td_serie + ' rev. ' + cast(td_vers as varchar) END AS xx_numdoc," & _
               "       tb_desruaz, " & _
               "       tabciva_1.tb_aliq As Aliq1, tabciva_1.tb_desciva As Desciva1," & _
               "       tabciva_2.tb_aliq As Aliq2, tabciva_2.tb_desciva As Desciva2," & _
               "       tabciva_3.tb_aliq As Aliq3, tabciva_3.tb_desciva As Desciva3," & _
               "       tabciva_4.tb_aliq As Aliq4, tabciva_4.tb_desciva As Desciva4," & _
               "       tabciva_5.tb_aliq As Aliq5, tabciva_5.tb_desciva As Desciva5," & _
               "       tabciva_6.tb_aliq As Aliq6, tabciva_6.tb_desciva As Desciva6," & _
               "       tabciva_7.tb_aliq As Aliq7, tabciva_7.tb_desciva As Desciva7," & _
               "       tabciva_8.tb_aliq As Aliq8, tabciva_8.tb_desciva As Desciva8 " & _
               " FROM testoff " & _
               "   INNER JOIN movoff ON testoff.codditt = movoff.codditt AND testoff.td_tipork = movoff.mo_tipork AND testoff.td_anno = movoff.mo_anno AND testoff.td_serie = movoff.mo_serie AND testoff.td_numord = movoff.mo_numord AND testoff.td_vers = movoff.mo_vers" & _
               "   LEFT JOIN leads ON testoff.codditt = leads.codditt AND testoff.td_codlead = leads.le_codlead" & _
               "   LEFT JOIN opportun ON testoff.codditt = opportun.codditt AND testoff.td_codoppo = opportun.op_codoppo" & _
               "   LEFT JOIN organig ON testoff.codditt = organig.codditt AND testoff.td_opnomeinc = organig.og_coperat AND og_codlead >= 0" & _
               "   LEFT JOIN tabtpbf ON testoff.codditt = tabtpbf.codditt AND testoff.td_tipobf = tabtpbf.tb_codtpbf" & _
               "   LEFT JOIN tabpaga ON testoff.td_codpaga = tabpaga.tb_codpaga" & _
               "   LEFT JOIN tabcage ON testoff.codditt = tabcage.codditt AND testoff.td_codagen = tabcage.tb_codcage" & _
               "   LEFT JOIN tabvalu ON testoff.td_valuta = tabvalu.tb_codvalu" & _
               "   LEFT JOIN tabport ON testoff.codditt = tabport.codditt AND testoff.td_porto = tabport.tb_codport" & _
               "   LEFT JOIN anagra ON testoff.codditt = anagra.codditt AND testoff.td_conto = anagra.an_conto" & _
               "   LEFT JOIN destdiv ON testoff.codditt = destdiv.codditt AND testoff.td_conto = destdiv.dd_conto AND testoff.td_coddest = destdiv.dd_coddest" & _
               "   LEFT JOIN destdiv As destdiv_1 ON testoff.codditt = destdiv_1.codditt AND testoff.td_conto = destdiv_1.dd_conto AND testoff.td_coddest2 = destdiv_1.dd_coddest" & _
               "   LEFT JOIN tabciva ON testoff.td_codese = tabciva.tb_codciva" & _
               "   LEFT JOIN tabcage As tabcage_1 ON testoff.codditt = tabcage_1.codditt AND testoff.td_codagen2 = tabcage_1.tb_codcage" & _
               "   LEFT JOIN anagra As anagra_1 ON tabcage.codditt = anagra_1.codditt AND tabcage.tb_codforn = anagra_1.an_conto" & _
               "   LEFT JOIN anagra As anagra_2 ON tabcage_1.codditt = anagra_2.codditt AND tabcage_1.tb_codforn = anagra_2.an_conto" & _
               "   LEFT JOIN artico ON movoff.codditt = artico.codditt AND movoff.mo_codart = artico.ar_codart" & _
               "   LEFT JOIN tabruaz ON organig.og_codruaz = tabruaz.tb_codruaz" & _
               "   LEFT JOIN tabciva AS tabciva_1 ON testoff.td_codiva_1 = tabciva_1.tb_codciva" & _
               "   LEFT JOIN tabciva AS tabciva_2 ON testoff.td_codiva_2 = tabciva_2.tb_codciva" & _
               "   LEFT JOIN tabciva AS tabciva_3 ON testoff.td_codiva_3 = tabciva_3.tb_codciva" & _
               "   LEFT JOIN tabciva AS tabciva_4 ON testoff.td_codiva_4 = tabciva_4.tb_codciva" & _
               "   LEFT JOIN tabciva AS tabciva_5 ON testoff.td_codiva_5 = tabciva_5.tb_codciva" & _
               "   LEFT JOIN tabciva AS tabciva_6 ON testoff.td_codiva_6 = tabciva_6.tb_codciva" & _
               "   LEFT JOIN tabciva AS tabciva_7 ON testoff.td_codiva_7 = tabciva_7.tb_codciva" & _
               "   LEFT JOIN tabciva AS tabciva_8 ON testoff.td_codiva_8 = tabciva_8.tb_codciva"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetQueryStampaWord(ByVal strDitta As String, ByVal strTipork As String, _
                                                    ByVal stredSerie As String, ByVal stredNumordini As String, _
                                                    ByVal stredNumordfin As String, ByVal stredContoini As String, _
                                                    ByVal stredContofin As String, ByVal strDatordini As String, _
                                                    ByVal strDatordfin As String, ByVal strConsOrdini As String, _
                                                    ByVal strConsOrdFin As String, ByVal stredCommecaini As String, _
                                                    ByVal stredCommecafin As String, ByVal strRiga As String, _
                                                    ByVal strRigafin As String, ByVal stredAnno As String, _
                                                    ByVal bedCoddestini As Boolean, ByVal stredCoddestini As String, _
                                                    ByVal stredCoddestfin As String, ByVal stredCodagen As String, _
                                                    ByVal stredZona As String, ByVal bopStorico0 As Boolean, _
                                                    ByVal bopStorico1 As Boolean, ByVal bopStampati0 As Boolean, _
                                                    ByVal bopStampati1 As Boolean, ByVal bopRiga0 As Boolean, _
                                                    ByVal bopRiga1 As Boolean, ByVal stredCodcfam As String, _
                                                    ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                                    ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                    ByVal bUsaKeyOrdWord As Boolean, ByVal bckCliForn As Boolean, _
                                                    ByVal strQuery As String, _
                                                    ByVal bNonConfermato As Boolean, ByVal bConfermato As Boolean) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return GetQueryStampaWord(strDitta, strTipork, stredSerie, stredNumordini, stredNumordfin, stredContoini, _
                                stredContofin, strDatordini, strDatordfin, strConsOrdini, strConsOrdFin, _
                                stredCommecaini, stredCommecafin, strRiga, strRigafin, stredAnno, bedCoddestini, _
                                stredCoddestini, stredCoddestfin, stredCodagen, stredZona, bopStorico0, _
                                bopStorico1, bopStampati0, bopStampati1, bopRiga0, bopRiga1, stredCodcfam, _
                                bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, bUsaKeyOrdWord, bckCliForn, _
                                strQuery, bNonConfermato, bConfermato, 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetQueryStampaWord(ByVal strDitta As String, ByVal strTipork As String, _
                                                    ByVal stredSerie As String, ByVal stredNumordini As String, _
                                                    ByVal stredNumordfin As String, ByVal stredContoini As String, _
                                                    ByVal stredContofin As String, ByVal strDatordini As String, _
                                                    ByVal strDatordfin As String, ByVal strConsOrdini As String, _
                                                    ByVal strConsOrdFin As String, ByVal stredCommecaini As String, _
                                                    ByVal stredCommecafin As String, ByVal strRiga As String, _
                                                    ByVal strRigafin As String, ByVal stredAnno As String, _
                                                    ByVal bedCoddestini As Boolean, ByVal stredCoddestini As String, _
                                                    ByVal stredCoddestfin As String, ByVal stredCodagen As String, _
                                                    ByVal stredZona As String, ByVal bopStorico0 As Boolean, _
                                                    ByVal bopStorico1 As Boolean, ByVal bopStampati0 As Boolean, _
                                                    ByVal bopStampati1 As Boolean, ByVal bopRiga0 As Boolean, _
                                                    ByVal bopRiga1 As Boolean, ByVal stredCodcfam As String, _
                                                    ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                                    ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                    ByVal bUsaKeyOrdWord As Boolean, ByVal bckCliForn As Boolean, _
                                                    ByVal strQuery As String, _
                                                    ByVal bNonConfermato As Boolean, ByVal bConfermato As Boolean, _
                                                    ByVal nCodlsel As Integer) As String
    'prepara la query, ma non la esegue: la passerà a BN__WORD che estrarrà i dati e li stamperà
    Dim strSQL As String = ""
    Dim strWhere As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, stredSerie, stredNumordini, stredNumordfin, _
                                             stredContoini, stredContofin, strDatordini, strDatordfin, _
                                             strConsOrdini, strConsOrdFin, stredCommecaini, stredCommecafin, _
                                             strRiga, strRigafin, stredAnno, bedCoddestini, stredCoddestini, _
                                             stredCoddestfin, stredCodagen, stredZona, bopStorico0, bopStorico1, _
                                             bopStampati0, bopStampati1, bopRiga0, bopRiga1, stredCodcfam, _
                                             bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, bUsaKeyOrdWord, _
                                             bckCliForn, strQuery, bNonConfermato, bConfermato, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strWhere = ComponiWhereStampaWord(strDitta, strTipork, stredSerie, stredNumordini, _
                                       stredNumordfin, stredContoini, stredContofin, strDatordini, _
                                       strDatordfin, strConsOrdini, strConsOrdFin, stredCommecaini, _
                                       stredCommecafin, strRiga, strRigafin, stredAnno, _
                                       bedCoddestini, stredCoddestini, stredCoddestfin, stredCodagen, _
                                       stredZona, bopStorico0, bopStorico1, bopStampati0, _
                                       bopStampati1, bopRiga0, bopRiga1, stredCodcfam, _
                                       bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                       bUsaKeyOrdWord, strQuery, bNonConfermato, bConfermato, nCodlsel)
      '--------------------------------------------------------------------------------------------------------------
      strSQL += GetQuerySelectFromStampaWord() & " WHERE  " & strWhere
      strSQL = InglobaQuerySQL(strSQL)
      If bckCliForn = False Then
        strSQL += " ORDER BY testord.td_serie, testord.td_numord, mo_riga, mo_datcons"
      Else
        strSQL += " ORDER BY testord.td_conto, testord.td_serie, testord.td_numord, mo_riga"
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ComponiWhereStampaWord(ByVal strDitta As String, ByVal strTipork As String, _
                                                    ByVal stredSerie As String, ByVal stredNumordini As String, _
                                                    ByVal stredNumordfin As String, ByVal stredContoini As String, _
                                                    ByVal stredContofin As String, ByVal strDatordini As String, _
                                                    ByVal strDatordfin As String, ByVal strConsOrdini As String, _
                                                    ByVal strConsOrdFin As String, ByVal stredCommecaini As String, _
                                                    ByVal stredCommecafin As String, ByVal strRiga As String, _
                                                    ByVal strRigafin As String, ByVal stredAnno As String, _
                                                    ByVal bedCoddestini As Boolean, ByVal stredCoddestini As String, _
                                                    ByVal stredCoddestfin As String, ByVal stredCodagen As String, _
                                                    ByVal stredZona As String, ByVal bopStorico0 As Boolean, _
                                                    ByVal bopStorico1 As Boolean, ByVal bopStampati0 As Boolean, _
                                                    ByVal bopStampati1 As Boolean, ByVal bopRiga0 As Boolean, _
                                                    ByVal bopRiga1 As Boolean, ByVal stredCodcfam As String, _
                                                    ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                                    ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                    ByVal bUsaKeyOrdWord As Boolean, ByVal strQuery As String, _
                                                    ByVal bNonConfermato As Boolean, ByVal bConfermato As Boolean) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return ComponiWhereStampaWord(strDitta, strTipork, stredSerie, stredNumordini, stredNumordfin, _
                                    stredContoini, stredContofin, strDatordini, strDatordfin, strConsOrdini, _
                                    strConsOrdFin, stredCommecaini, stredCommecafin, strRiga, strRigafin, _
                                    stredAnno, bedCoddestini, stredCoddestini, stredCoddestfin, stredCodagen, _
                                    stredZona, bopStorico0, bopStorico1, bopStampati0, bopStampati1, bopRiga0, _
                                    bopRiga1, stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                    bUsaKeyOrdWord, strQuery, bNonConfermato, bConfermato, 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function
  Public Overridable Function ComponiWhereStampaWord(ByVal strDitta As String, ByVal strTipork As String, _
                                                     ByVal stredSerie As String, ByVal stredNumordini As String, _
                                                     ByVal stredNumordfin As String, ByVal stredContoini As String, _
                                                     ByVal stredContofin As String, ByVal strDatordini As String, _
                                                     ByVal strDatordfin As String, ByVal strConsOrdini As String, _
                                                     ByVal strConsOrdFin As String, ByVal stredCommecaini As String, _
                                                     ByVal stredCommecafin As String, ByVal strRiga As String, _
                                                     ByVal strRigafin As String, ByVal stredAnno As String, _
                                                     ByVal bedCoddestini As Boolean, ByVal stredCoddestini As String, _
                                                     ByVal stredCoddestfin As String, ByVal stredCodagen As String, _
                                                     ByVal stredZona As String, ByVal bopStorico0 As Boolean, _
                                                     ByVal bopStorico1 As Boolean, ByVal bopStampati0 As Boolean, _
                                                     ByVal bopStampati1 As Boolean, ByVal bopRiga0 As Boolean, _
                                                     ByVal bopRiga1 As Boolean, ByVal stredCodcfam As String, _
                                                     ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                                     ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                     ByVal bUsaKeyOrdWord As Boolean, ByVal strQuery As String, _
                                                     ByVal bNonConfermato As Boolean, ByVal bConfermato As Boolean, _
                                                     ByVal nCodlsel As Integer) As String
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, stredSerie, stredNumordini, stredNumordfin, _
                                             stredContoini, stredContofin, strDatordini, strDatordfin, _
                                             strConsOrdini, strConsOrdFin, stredCommecaini, stredCommecafin, _
                                             strRiga, strRigafin, stredAnno, bedCoddestini, stredCoddestini, _
                                             stredCoddestfin, stredCodagen, stredZona, bopStorico0, bopStorico1, _
                                             bopStampati0, bopStampati1, bopRiga0, bopRiga1, stredCodcfam, _
                                             bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, bUsaKeyOrdWord, _
                                             strQuery, bNonConfermato, bConfermato, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = " testord.codditt = " & CStrSQL(strDitta) & _
        " AND td_tipork = " & CStrSQL(strTipork) & _
        IIf(stredSerie <> "", " AND td_serie = " & CStrSQL(stredSerie), "").ToString & _
        IIf(NTSCInt(stredAnno) <> 0, " AND td_anno = " & stredAnno, "").ToString & _
        IIf(NTSCInt(stredCodagen) > 0, " AND td_codagen = " & stredCodagen, "").ToString & _
        IIf(NTSCInt(stredZona) > 0, " AND an_zona = " & stredZona, "").ToString & _
        IIf(bopStorico0, " AND td_flevas = 'N'", "").ToString & _
        IIf(bopStorico1, " AND td_flevas = 'S'", "").ToString & _
        IIf(bopStampati0, " AND td_flstam = 'N'", "").ToString & _
        IIf(bopStampati1, " AND td_flstam = 'S'", "").ToString & _
        IIf(bopRiga0, " AND mo_flevas = 'C'", "").ToString & _
        IIf(bopRiga1, " AND mo_flevas = 'S'", "").ToString & _
        IIf(bNonConfermato = True, " AND td_confermato = 'N'", "").ToString & _
        IIf(bConfermato = True, " AND td_confermato = 'S'", "").ToString & _
        IIf(stredCodcfam <> "", " AND td_codcfam = " & CStrSQL(stredCodcfam), "").ToString & _
        IIf(bUsaKeyOrdWord = True, " AND ko_magaz = mo_magaz", "").ToString
      If (NTSCInt(stredNumordini) <> 0) Or (NTSCInt(stredNumordfin) <> 999999999) Then
        strSQL += " AND td_numord between " & stredNumordini & " AND " & stredNumordfin
      End If
      If (NTSCInt(stredContoini) <> 0) Or (NTSCInt(stredContofin) <> 999999999) Then
        strSQL += " AND td_conto BETWEEN " & stredContoini & " AND " & stredContofin
      End If
      If (NTSCInt(stredCommecaini) <> 0) Or (NTSCInt(stredCommecafin) <> 999999999) Then
        strSQL += " AND td_commeca BETWEEN " & stredCommecaini & " AND " & stredCommecafin
      End If
      If (NTSCDate(strDatordini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strDatordfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND td_datord BETWEEN " & CDataSQL(strDatordini) & " AND " & CDataSQL(strDatordfin)
      End If
      If (NTSCDate(strConsOrdini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strConsOrdFin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND td_datcons BETWEEN " & CDataSQL(strConsOrdini) & " AND " & CDataSQL(strConsOrdFin)
      End If
      If (NTSCDate(strRiga) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strRigafin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND movord.mo_datcons BETWEEN " & CDataSQL(strRiga) & " AND " & CDataSQL(strRigafin)
      End If
      If bedCoddestini = True Then
        If (NTSCInt(stredCoddestini) <> 0) Or (NTSCInt(stredCoddestfin) <> 999999999) Then
          strSQL += " AND td_coddest BETWEEN " & stredCoddestini & " AND " & stredCoddestfin
        End If
      End If
      If nCodlsel > 0 Then
        strSQL += " AND td_conto IN (SELECT lse_conto FROM listsel" & _
                                   " WHERE codditt = " & CStrSQL(strDitta) & _
                                   " AND lse_codlsel = " & nCodlsel & ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se è attivo il modulo 'Taglie e colori' controlla se considerare i filtri specifici
      '--- su anno e/o codice stagione
      '--------------------------------------------------------------------------------------------------------------
      If (bModTCO = True) And (bckSelAnnoStag) Then
        If NTSCInt(stredAnnotco) > 0 Then strSQL += " AND td_annotco = " & stredAnnotco
        If NTSCInt(stredCodstag) > 0 Then strSQL += " AND td_codstag = " & stredCodstag
      End If
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery, strSQL)
      '--------------------------------------------------------------------------------------------------------------
      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function

  Public Overridable Function ReportExist(ByVal strDitta As String, ByVal stredDatordini As String, _
                                          ByVal stredDatordfin As String, ByVal stredDataCons As String, _
                                          ByVal stredDataConsfin As String, ByVal stredRiga As String, _
                                          ByVal stredRigafin As String, ByVal strcbTipork As String, _
                                          ByVal stredSerie As String, ByVal stredNumordini As String, _
                                          ByVal stredNumordfin As String, ByVal stredContoini As String, _
                                          ByVal stredContofin As String, ByVal stredCommecaini As String, _
                                          ByVal stredCommecafin As String, ByVal stredAnno As String, _
                                          ByVal bedCoddestini As Boolean, ByVal stredCoddestini As String, _
                                          ByVal stredCoddestfin As String, ByVal nTipostampa As Integer, _
                                          ByVal stredCodagen As String, ByVal stredZona As String, _
                                          ByVal bopStorico0 As Boolean, ByVal bopStorico1 As Boolean, _
                                          ByVal bopStampati0 As Boolean, ByVal bopStampati1 As Boolean, _
                                          ByVal bopRiga0 As Boolean, ByVal bopRiga1 As Boolean, _
                                          ByVal stredCodcfam As String, ByVal bModTCO As Boolean, _
                                          ByVal bckSelAnnoStag As Boolean, ByVal stredAnnotco As String, _
                                          ByVal stredCodstag As String, ByVal strQuery As String, _
                                          ByRef ds As DataSet, _
                                          ByVal bNonConfermato As Boolean, ByVal bConfermato As Boolean) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return ReportExist(strDitta, stredDatordini, stredDatordfin, stredDataCons, stredDataConsfin, stredRiga, _
                         stredRigafin, strcbTipork, stredSerie, stredNumordini, stredNumordfin, stredContoini, _
                         stredContofin, stredCommecaini, stredCommecafin, stredAnno, bedCoddestini, stredCoddestini, _
                         stredCoddestfin, nTipostampa, stredCodagen, stredZona, bopStorico0, bopStorico1, _
                         bopStampati0, bopStampati1, bopRiga0, bopRiga1, stredCodcfam, bModTCO, bckSelAnnoStag, _
                         stredAnnotco, stredCodstag, strQuery, ds, bNonConfermato, bConfermato, 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function ReportExist(ByVal strDitta As String, ByVal stredDatordini As String, _
                                          ByVal stredDatordfin As String, ByVal stredDataCons As String, _
                                          ByVal stredDataConsfin As String, ByVal stredRiga As String, _
                                          ByVal stredRigafin As String, ByVal strcbTipork As String, _
                                          ByVal stredSerie As String, ByVal stredNumordini As String, _
                                          ByVal stredNumordfin As String, ByVal stredContoini As String, _
                                          ByVal stredContofin As String, ByVal stredCommecaini As String, _
                                          ByVal stredCommecafin As String, ByVal stredAnno As String, _
                                          ByVal bedCoddestini As Boolean, ByVal stredCoddestini As String, _
                                          ByVal stredCoddestfin As String, ByVal nTipostampa As Integer, _
                                          ByVal stredCodagen As String, ByVal stredZona As String, _
                                          ByVal bopStorico0 As Boolean, ByVal bopStorico1 As Boolean, _
                                          ByVal bopStampati0 As Boolean, ByVal bopStampati1 As Boolean, _
                                          ByVal bopRiga0 As Boolean, ByVal bopRiga1 As Boolean, _
                                          ByVal stredCodcfam As String, ByVal bModTCO As Boolean, _
                                          ByVal bckSelAnnoStag As Boolean, ByVal stredAnnotco As String, _
                                          ByVal stredCodstag As String, ByVal strQuery As String, _
                                          ByRef ds As DataSet, _
                                          ByVal bNonConfermato As Boolean, ByVal bConfermato As Boolean, _
                                          ByVal nCodlsel As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strWhere As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, stredDatordini, stredDatordfin, stredDataCons, _
                                             stredDataConsfin, stredRiga, stredRigafin, strcbTipork, _
                                             stredSerie, stredNumordini, stredNumordfin, stredContoini, _
                                             stredContofin, stredCommecaini, stredCommecafin, stredAnno, _
                                             bedCoddestini, stredCoddestini, stredCoddestfin, nTipostampa, _
                                             stredCodagen, stredZona, bopStorico0, bopStorico1, _
                                             bopStampati0, bopStampati1, bopRiga0, bopRiga1, stredCodcfam, _
                                             bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, strQuery, _
                                             ds, bNonConfermato, bConfermato, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(34), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strWhere = ComponiWhereStampaWord(strDitta, strcbTipork, stredSerie, stredNumordini, _
                                       stredNumordfin, stredContoini, stredContofin, stredDatordini, _
                                       stredDatordfin, stredDataCons, stredDataConsfin, stredCommecaini, _
                                       stredCommecafin, stredRiga, stredRigafin, stredAnno, _
                                       bedCoddestini, stredCoddestini, stredCoddestfin, stredCodagen, _
                                       stredZona, bopStorico0, bopStorico1, bopStampati0, _
                                       bopStampati1, bopRiga0, bopRiga1, stredCodcfam, _
                                       bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                       False, strQuery, bNonConfermato, bConfermato, nCodlsel)
      Select Case nTipostampa
        Case 1 : strWhere += " AND TESTORD.td_valuta = 0 AND TESTORD.td_scorpo = 'N'"
        Case 2 : strWhere += " AND TESTORD.td_valuta = 0 AND TESTORD.td_scorpo = 'S'"
        Case 3 : strWhere += " AND TESTORD.td_valuta <> 0 AND TESTORD.td_scorpo = 'N'"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT testord.codditt, td_tipork, td_anno, td_serie, td_numord " & _
               " FROM testord " & _
               " INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord" & _
               " INNER JOIN anagra ON anagra.codditt = testord.codditt AND anagra.an_conto = testord.td_conto" & _
               " WHERE " & strWhere & _
               " ORDER BY td_tipork, td_anno, td_serie, td_numord"
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANAGRA")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetQueryStampaPDF(ByVal strDitta As String, ByVal strTipork As String, _
                                                    ByVal stredSerie As String, ByVal stredNumordini As String, _
                                                    ByVal stredNumordfin As String, ByVal stredContoini As String, _
                                                    ByVal stredContofin As String, ByVal strDatordini As String, _
                                                    ByVal strDatordfin As String, ByVal strConsOrdini As String, _
                                                    ByVal strConsOrdFin As String, ByVal stredCommecaini As String, _
                                                    ByVal stredCommecafin As String, ByVal strRiga As String, _
                                                    ByVal strRigafin As String, ByVal stredAnno As String, _
                                                    ByVal bedCoddestini As Boolean, ByVal stredCoddestini As String, _
                                                    ByVal stredCoddestfin As String, ByVal stredCodagen As String, _
                                                    ByVal stredZona As String, ByVal bopStorico0 As Boolean, _
                                                    ByVal bopStorico1 As Boolean, ByVal bopStampati0 As Boolean, _
                                                    ByVal bopStampati1 As Boolean, ByVal bopRiga0 As Boolean, _
                                                    ByVal bopRiga1 As Boolean, ByVal stredCodcfam As String, _
                                                    ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                                    ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                    ByVal bUsaKeyOrdWord As Boolean, ByVal bckCliForn As Boolean, _
                                                    ByRef strQueryGetDocUnico As String, ByVal strQuery As String, _
                                                    ByVal bNonConfermato As Boolean, ByVal bConfermato As Boolean) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim strQueryResult As String = GetQueryStampaPDF(strDitta, strTipork, stredSerie, stredNumordini, stredNumordfin, stredContoini, _
                                                       stredContofin, strDatordini, strDatordfin, strConsOrdini, strConsOrdFin, _
                                                       stredCommecaini, stredCommecafin, strRiga, strRigafin, stredAnno, bedCoddestini, _
                                                       stredCoddestini, stredCoddestfin, stredCodagen, stredZona, bopStorico0, bopStorico1, _
                                                       bopStampati0, bopStampati1, bopRiga0, bopRiga1, stredCodcfam, bModTCO, _
                                                       bckSelAnnoStag, stredAnnotco, stredCodstag, bUsaKeyOrdWord, bckCliForn, _
                                                       strQueryGetDocUnico, strQuery, bNonConfermato, bConfermato, 0)
      '--------------------------------------------------------------------------------------------------------------
      strQueryResult = "SELECT testord.*, movord.*, tmp.* " & _
                       " "
      '--------------------------------------------------------------------------------------------------------------
      Return strQueryResult
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetQueryStampaPDF(ByVal strDitta As String, ByVal strTipork As String, _
                                                ByVal stredSerie As String, ByVal stredNumordini As String, _
                                                ByVal stredNumordfin As String, ByVal stredContoini As String, _
                                                ByVal stredContofin As String, ByVal strDatordini As String, _
                                                ByVal strDatordfin As String, ByVal strConsOrdini As String, _
                                                ByVal strConsOrdFin As String, ByVal stredCommecaini As String, _
                                                ByVal stredCommecafin As String, ByVal strRiga As String, _
                                                ByVal strRigafin As String, ByVal stredAnno As String, _
                                                ByVal bedCoddestini As Boolean, ByVal stredCoddestini As String, _
                                                ByVal stredCoddestfin As String, ByVal stredCodagen As String, _
                                                ByVal stredZona As String, ByVal bopStorico0 As Boolean, _
                                                ByVal bopStorico1 As Boolean, ByVal bopStampati0 As Boolean, _
                                                ByVal bopStampati1 As Boolean, ByVal bopRiga0 As Boolean, _
                                                ByVal bopRiga1 As Boolean, ByVal stredCodcfam As String, _
                                                ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                                ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                ByVal bUsaKeyOrdWord As Boolean, ByVal bckCliForn As Boolean, _
                                                ByRef strQueryGetDocUnico As String, ByVal strQuery As String, _
                                                ByVal bNonConfermato As Boolean, ByVal bConfermato As Boolean, _
                                                ByVal nCodlsel As Integer) As String
    'prepara la query, ma non la esegue: la passerà a BNPDGENP che estrarrà i dati e li stamperà
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, stredSerie, stredNumordini, stredNumordfin, _
                                             stredContoini, stredContofin, strDatordini, strDatordfin, _
                                             strConsOrdini, strConsOrdFin, stredCommecaini, stredCommecafin, _
                                             strRiga, strRigafin, stredAnno, bedCoddestini, stredCoddestini, _
                                             stredCoddestfin, stredCodagen, stredZona, bopStorico0, bopStorico1, _
                                             bopStampati0, bopStampati1, bopRiga0, bopRiga1, stredCodcfam, _
                                             bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, bUsaKeyOrdWord, _
                                             bckCliForn, strQueryGetDocUnico, strQuery, bNonConfermato, _
                                             bConfermato, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strQueryGetDocUnico = NTSCStr(oIn(34))
        Return NTSCStr(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strWhere = ComponiWhereStampaWord(strDitta, strTipork, stredSerie, stredNumordini, _
                                       stredNumordfin, stredContoini, stredContofin, strDatordini, _
                                       strDatordfin, strConsOrdini, strConsOrdFin, stredCommecaini, _
                                       stredCommecafin, strRiga, strRigafin, stredAnno, _
                                       bedCoddestini, stredCoddestini, stredCoddestfin, stredCodagen, _
                                       stredZona, bopStorico0, bopStorico1, bopStampati0, _
                                       bopStampati1, bopRiga0, bopRiga1, stredCodcfam, _
                                       bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                       bUsaKeyOrdWord, strQuery, bNonConfermato, bConfermato, nCodlsel)
      '--------------------------------------------------------------------------------------------------------------
      'Compone la stringa SQL: la prima è la query per fare un pdf per ogni documento
      strTmp = " SELECT td_tipork as tipork, td_anno as anno, td_serie as serie, td_numord as numero, " & _
             "td_conto as conto, td_valuta as valuta, td_scorpo as scorpo, td_codagen as agente " & _
             GetQuerySelectFromStampaWord().Substring(GetQuerySelectFromStampaWord.IndexOf(" FROM "))
      strSQL += strTmp & " WHERE  " & strWhere & _
             " GROUP BY td_valuta, td_scorpo, td_tipork, td_anno, td_serie, td_numord, td_conto, td_codagen " & _
             " ORDER BY td_valuta, td_scorpo, td_tipork, td_anno, td_serie, td_numord "
      '--------------------------------------------------------------------------------------------------------------
      'questa serve per fare un pdf unico (in realtà i pdf potrebbero essere anche più di uno, 
      'ma uno unico per i documenti in valuta, uno unico per quelli con scorporo, uno unico per i restanti
      strQueryGetDocUnico = " SELECT td_tipork as tipork, 0 as anno, ' ' as serie, 0 as numero, " & _
             "0 as conto, td_valuta as valuta, td_scorpo as scorpo, 0 as agente " & _
             GetQuerySelectFromStampaWord().Substring(GetQuerySelectFromStampaWord.IndexOf(" FROM "))
      strQueryGetDocUnico += " WHERE  " & strWhere & _
             " GROUP BY td_tipork, td_valuta, td_scorpo " & _
             " ORDER BY td_tipork, td_valuta, td_scorpo "
      '--------------------------------------------------------------------------------------------------------------
      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  'sembra che non la utilizzi nessuno ...
  'Public Overridable Function ComponiFormulaSQL(ByVal strDitta As String, ByVal stredDatordini As String, _
  '                                              ByVal stredDatordfin As String, ByVal stredDataCons As String, _
  '                                              ByVal stredDataConsfin As String, ByVal stredRiga As String, _
  '                                              ByVal stredRigafin As String, ByVal strcbTipork As String, _
  '                                              ByVal stredSerie As String, ByVal stredNumordini As String, _
  '                                              ByVal stredNumordfin As String, ByVal stredContoini As String, _
  '                                              ByVal stredContofin As String, ByVal stredCommecaini As String, _
  '                                              ByVal stredCommecafin As String, ByVal stredAnno As String, _
  '                                              ByVal bedCoddestini As Boolean, ByVal stredCoddestini As String, _
  '                                              ByVal stredCoddestfin As String, ByVal nTipostampa As Integer, _
  '                                              ByVal stredCodagen As String, ByVal stredZona As String, _
  '                                              ByVal bopStorico0 As Boolean, ByVal bopStorico1 As Boolean, _
  '                                              ByVal bopStampati0 As Boolean, ByVal bopStampati1 As Boolean, _
  '                                              ByVal bopRiga0 As Boolean, ByVal bopRiga1 As Boolean, _
  '                                              ByVal stredCodcfam As String, ByVal bModTCO As Boolean, _
  '                                              ByVal bckSelAnnoStag As Boolean, ByVal stredAnnotco As String, _
  '                                              ByVal stredCodstag As String) As String
  '  Dim strSQL As String = ""
  '  Try

  '    ComponiFormulaSQL = ""

  '    strSQL = " TESTORD.td_conto between " & stredContoini & " and " & stredContofin & _
  '      " and TESTORD.td_tipork = '" & strcbTipork & "'" & _
  '      " and TESTORD.td_serie = '" & stredSerie & "'" & _
  '      " and TESTORD.td_numord between " & stredNumordini & " and " & stredNumordfin & _
  '      " and TESTORD.td_datord between " & CDataSQL(stredDatordini) & " and " & CDataSQL(stredDatordfin) & _
  '      " and TESTORD.td_magaz2 <> KEYORD.ko_magaz" & _
  '      " and TESTORD.td_commeca between " & stredCommecaini & " and " & stredCommecafin & _
  '      " and MOVORD.mo_stasino <> 'N'" & _
  '      " and MOVORD.mo_datcons between " & CDataSQL(stredRiga) & " and " & CDataSQL(stredRigafin) & _
  '      " and TESTORD.td_datcons between " & CDataSQL(stredDataCons) & " and " & CDataSQL(stredDataConsfin)
  '    If NTSCInt(stredAnno) <> 0 Then strSQL = strSQL & " and TESTORD.td_anno = " & stredAnno
  '    If bedCoddestini Then
  '      strSQL = strSQL & " and TESTORD.td_coddest between " & stredCoddestini & " and " & stredCoddestfin
  '    End If
  '    Select Case nTipostampa
  '      Case 1 : strSQL = strSQL & " and TESTORD.td_valuta = 0 and TESTORD.td_scorpo = 'N'"
  '      Case 2 : strSQL = strSQL & " and TESTORD.td_valuta = 0 and TESTORD.td_scorpo = 'S'"
  '      Case 3 : strSQL = strSQL & " and TESTORD.td_valuta <> 0 and TESTORD.td_scorpo = 'N'"
  '    End Select
  '    If NTSCInt(stredCodagen) > 0 Then strSQL = strSQL & " and TESTORD.td_codagen = " & stredCodagen
  '    If NTSCInt(stredZona) > 0 Then strSQL = strSQL & " and ANAGRA.an_zona = " & stredZona
  '    If bopStorico0 = True Then strSQL = strSQL & " and TESTORD.td_flevas = 'N'"
  '    If bopStorico1 = True Then strSQL = strSQL & " and TESTORD.td_flevas = 'S'"
  '    If bopStampati0 = True Then strSQL = strSQL & " and TESTORD.td_flstam = 'N'"
  '    If bopStampati1 = True Then strSQL = strSQL & " and TESTORD.td_flstam = 'S'"
  '    If bopRiga0 = True Then strSQL = strSQL & " and MOVORD.mo_flevas = 'C'"
  '    If bopRiga1 = True Then strSQL = strSQL & " and MOVORD.mo_flevas = 'S'"
  '    strSQL = strSQL & " And testord.codditt = '" & strDitta & "'"
  '    If stredCodcfam <> "" Then
  '      strSQL = strSQL & " AND testord.td_codcfam = " & CStrSQL(stredCodcfam)
  '    End If
  '    '-----------------------------------------------------------------------------------------
  '    '--- Se è attivo il modulo 'Taglie e colori' controlla se considerare i filtri specifici
  '    '--- su anno e/o codice stagione
  '    '-----------------------------------------------------------------------------------------
  '    If (bModTCO = True) And (bckSelAnnoStag) Then
  '      If NTSCInt(stredAnnotco) > 0 Then
  '        strSQL = strSQL & " AND testord.td_annotco = " & stredAnnotco
  '      End If
  '      If NTSCInt(stredCodstag) > 0 Then
  '        strSQL = strSQL & " AND testord.td_codstag = " & stredCodstag
  '      End If
  '    End If
  '    '-----------------------------------------------------------------------------------------
  '    ComponiFormulaSQL = strSQL

  '    Return ComponiFormulaSQL

  '  Catch ex As Exception
  '    '--------------------------------------------------------------
  '    Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
  '    '--------------------------------------------------------------
  '    Return ""
  '  End Try
  'End Function

  Public Overridable Function AggiornaTestord(ByVal strDitta As String, ByVal stredDatordini As String, _
                                        ByVal stredDatordfin As String, ByVal stredDataCons As String, _
                                        ByVal stredDataConsfin As String, ByVal stredRiga As String, _
                                        ByVal stredRigafin As String, ByVal strcbTipork As String, _
                                        ByVal stredSerie As String, ByVal stredNumordini As String, _
                                        ByVal stredNumordfin As String, ByVal stredContoini As String, _
                                        ByVal stredContofin As String, ByVal stredCommecaini As String, _
                                        ByVal stredCommecafin As String, ByVal stredAnno As String, _
                                        ByVal bedCoddestini As Boolean, ByVal stredCoddestini As String, _
                                        ByVal stredCoddestfin As String, ByVal nTipostampa As Integer, _
                                        ByVal stredCodagen As String, ByVal stredZona As String, _
                                        ByVal bopStorico0 As Boolean, ByVal bopStorico1 As Boolean, _
                                        ByVal bopStampati0 As Boolean, ByVal bopStampati1 As Boolean, _
                                        ByVal bopRiga0 As Boolean, ByVal bopRiga1 As Boolean, _
                                        ByVal stredCodcfam As String, ByVal bModTCO As Boolean, _
                                        ByVal bckSelAnnoStag As Boolean, ByVal stredAnnotco As String, _
                                        ByVal stredCodstag As String) As Boolean
    Dim strSQL As String = ""
    Dim strTipork As String
    Dim strDatordini As String
    Dim strDatordfin As String
    Try
      '---------------------------------------------------------------
      '
      '---Aggiorna il campo td_flstam per i record ancora non stampati
      '
      '---Case nTipostampa:
      '---1: In Lire senza scorporo
      '---2: In Lire con scorporo
      '---3: In valuta (senza scorporo)
      '---------------------------------------------------------------
      strDatordini = CDataSQL(stredDatordini)
      strDatordfin = CDataSQL(stredDatordfin)
      strTipork = strcbTipork
      strSQL = "UPDATE testord" & _
        " SET td_flstam = 'S'" & _
        " FROM testord INNER JOIN anagra ON testord.td_conto = anagra.an_conto AND testord.codditt = anagra.codditt" & _
        " WHERE testord.codditt = " & CStrSQL(strDitta) & _
        " AND td_flstam = 'N'" & _
        " AND td_conto BETWEEN " & stredContoini & " AND " & stredContofin & _
        " AND td_tipork = '" & strTipork & "'" & _
        " AND td_numord BETWEEN " & stredNumordini & " AND " & stredNumordfin & _
        " AND td_datord BETWEEN " & strDatordini & " AND " & strDatordfin
      If stredSerie <> "" Then strSQL += " AND td_serie = '" & stredSerie & "'"
      If NTSCInt(stredAnno) <> 0 Then strSQL = strSQL & " AND td_anno = " & stredAnno
      If bedCoddestini Then
        strSQL = strSQL & " AND td_coddest BETWEEN " & stredCoddestini & " AND " & stredCoddestfin
      End If
      Select Case nTipostampa
        Case 1 : strSQL = strSQL & " AND td_valuta = 0 AND td_scorpo = 'N'"
        Case 2 : strSQL = strSQL & " AND td_valuta = 0 AND td_scorpo = 'S'"
        Case 3 : strSQL = strSQL & " AND td_valuta <> 0 AND td_scorpo = 'N'"
      End Select
      If NTSCInt(stredCodagen) > 0 Then strSQL = strSQL & " AND td_codagen = " & stredCodagen
      If NTSCInt(stredZona) > 0 Then strSQL = strSQL & " AND an_zona = " & stredZona
      If bopStorico0 = True Then strSQL = strSQL & " AND td_flevas = 'N'"
      If bopStorico1 = True Then strSQL = strSQL & " AND td_flevas = 'S'"
      If stredCodcfam <> "" Then
        strSQL = strSQL & " AND td_codcfam = " & CStrSQL(stredCodcfam)
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se è attivo il modulo 'Taglie e colori' controlla se considerare i filtri specifici
      '--- su anno e/o codice stagione
      '-----------------------------------------------------------------------------------------
      If (bModTCO = True) And (bckSelAnnoStag = True) Then
        If NTSCInt(stredAnnotco) > 0 Then
          strSQL = strSQL & " AND td_annotco = " & stredAnnotco
        End If
        If NTSCInt(stredCodstag) > 0 Then
          strSQL = strSQL & " AND td_codstag = " & stredCodstag
        End If
      End If
      '-----------------------------------------------------------------------------------------
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function RitornaLISTSEL(ByVal strDitta As String, ByVal nCodlsel As Integer, _
    ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT lse_conto, 0 AS progressivo FROM listsel" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lse_codlsel = " & nCodlsel & _
        " ORDER BY lse_conto"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttOut.Rows.Count = 0 Then
        dttOut.Clear()
        dttOut.Dispose()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dttOut.Rows.Count - 1)
        dttOut.Rows(i)!progressivo = (i + 1)
      Next
      dttOut.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

#Region "Filtri"
  Public Overridable Function CaricaFiltri(ByVal strDitta As String, ByVal strChild As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_codifil AS cod, tb_desifil AS val FROM tabifil " & _
               " WHERE tb_child = " & CStrSQL(strChild) & _
               " AND (tb_filtriut = ' ' " & _
                     " OR (tb_filtriut = 'S' AND ';' + REPLACE(tb_users, ' ', '') + ';' LIKE " & CStrSQL("%;" & oApp.User.Nome & ";%") & ") " & _
                     " OR (tb_filtriut = 'N' AND ';' + REPLACE(tb_users, ' ', '') + ';' NOT LIKE " & CStrSQL("%;" & oApp.User.Nome & ";%") & ")) " & _
               " AND (tb_filtriditta = ' ' " & _
                     " OR (tb_filtriditta = 'S' AND ';' + REPLACE(tb_ditte, ' ', '') + ';' LIKE " & CStrSQL("%;" & strDitta & ";%") & ") " & _
                     " OR (tb_filtriditta = 'N' AND ';' + REPLACE(tb_ditte, ' ', '') + ';' NOT LIKE " & CStrSQL("%;" & strDitta & ";%") & ")) "

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiFiltro(ByVal lCod As Integer, ByVal strChild As String, ByVal strForm As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movifil.* FROM movifil " & _
               " WHERE mo_codifil = " & lCod & _
               " AND mo_child = " & CStrSQL(strChild) & _
               " AND mo_form = " & CStrSQL(strForm) & _
               " ORDER BY mo_ordin"


      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------------------------
    End Try
  End Function
#End Region

End Class
