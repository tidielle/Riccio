Imports System
#Region "Imports"
Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
#End Region

Public Class CLDMGSTBO
  Inherits CLD__BASE

  Public Overridable Function ApriDettaglio(ByVal strDitta As String, ByVal strTabella As String, ByRef ds As DataSet, ByVal nPosition As Integer, ByVal strDettaglioTesta As String, ByVal strDettaglioRighe As String) As Boolean
    Dim strSQL As String = ""
    Dim strTabellaMov As String
    Try

      strTabellaMov = "mov" & strTabella.Substring(4)

      strSQL = "SELECT tm_riferim, tm_datdoc, tm_numpar, tm_alfpar, tm_datpar, tm_codpaga, tm_scont1, tm_scont2," & _
       " tm_scopag, tm_totdoc, tm_flfatt, tm_totomag, (tm_totdoc - tm_totomag) As tm_totdocnoomag," & _
       " an_descr1, tb_despaga" & _
       " FROM (" & strTabella & _
       " INNER JOIN anagra ON (" & strTabella & ".codditt = anagra.codditt) AND (" & strTabella & ".tm_conto = anagra.an_conto))" & _
       " INNER JOIN tabpaga ON " & strTabella & ".tm_codpaga = tabpaga.tb_codpaga" & _
       " WHERE " & strTabella & ".codditt = " & CStrSQL(strDitta) & _
       " AND tm_conto = " & CStrSQL(ds.Tables(strTabella).Rows(nPosition)!tm_conto) & _
       " AND tm_tipork = " & CStrSQL(ds.Tables(strTabella).Rows(nPosition)!tm_tipork) & _
       " AND tm_anno = " & NTSCInt(ds.Tables(strTabella).Rows(nPosition)!tm_anno) & _
       " AND tm_serie = " & CStrSQL(ds.Tables(strTabella).Rows(nPosition)!tm_serie) & _
       " AND tm_numdoc = " & NTSCInt(ds.Tables(strTabella).Rows(nPosition)!tm_numdoc)

      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strDettaglioTesta, ds)

      strSQL = "SELECT mm_causale, mm_codart, mm_descr, mm_quant, mm_prezzo, mm_scont1," & _
       " mm_scont2, mm_scont3, mm_scont4, mm_scont5, mm_scont6, mm_codiva, tb_desciva, mm_fase," & _
       " af_descr, mm_lotto, mm_commeca, mm_subcommeca, analotti.alo_lottox as xx_lottox" & _
       " FROM ((" & strTabellaMov & _
       " LEFT JOIN tabciva ON " & strTabellaMov & ".mm_codiva = tabciva.tb_codciva)" & _
       " LEFT JOIN artfasi ON " & strTabellaMov & ".codditt = artfasi.codditt AND " & strTabellaMov & ".mm_codart = artfasi.af_codart AND " & strTabellaMov & ".mm_fase = artfasi.af_fase)" & _
       " LEFT JOIN analotti ON " & strTabellaMov & ".codditt = analotti.codditt AND " & strTabellaMov & ".mm_codart = analotti.alo_codart AND " & strTabellaMov & ".mm_lotto = analotti.alo_lotto" & _
       " WHERE " & strTabellaMov & ".codditt = " & CStrSQL(strDitta) & _
       " AND mm_tipork = " & CStrSQL(ds.Tables(strTabella).Rows(nPosition)!tm_tipork) & _
       " AND mm_anno = " & NTSCInt(ds.Tables(strTabella).Rows(nPosition)!tm_anno) & _
       " AND mm_serie = " & CStrSQL(ds.Tables(strTabella).Rows(nPosition)!tm_serie) & _
       " AND mm_numdoc = " & NTSCInt(ds.Tables(strTabella).Rows(nPosition)!tm_numdoc) & _
       " ORDER BY mm_riga"

      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strDettaglioRighe, ds)

      If ds.Tables(strDettaglioTesta).Rows.Count = 0 And ds.Tables(strDettaglioRighe).Rows.Count = 0 Then Return False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckReports(ByVal strDitta As String, ByRef ds As DataSet, ByVal strTabella As String, _
    ByVal nDaconto As Integer, ByVal nAconto As Integer, _
    ByVal nAnno As Integer, ByVal strSerie As String, _
    ByVal dateDatini As Date, ByVal dateDatfin As Date, _
    ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, _
    ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
    ByVal bSeldocumenti As Boolean, ByVal strTipork As String, _
    ByVal nTipobf As Integer, ByVal nCoddest As Integer, _
    ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
    ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
    ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
    ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
    ByVal strCodcfam As String, ByVal bModTCO As Boolean, _
    ByVal bSelAnnoStag As Boolean, ByVal nAnnotco As Integer, _
    ByVal nCodstag As Integer, ByVal strQuery As String, _
    ByVal bFlevas As Boolean, ByVal nTippaga As Integer, _
    ByVal bModuloCRM As Boolean, ByVal bAmm As Boolean, _
    ByVal strAccvis As String, ByVal strAccmod As String, _
    ByVal strRegvis As String, ByVal strRegmod As String, _
    ByVal bIsCRMUser As Boolean, ByVal lCodorgaOperat As Integer, _
    ByVal nTiporeport As Integer, ByVal bCaricaDatiPerGriglia As Boolean, _
    ByVal bIs15 As Boolean, ByVal nCodcageAccdito As Integer, ByVal lIITtsubqcrm As Integer) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return CheckReports(strDitta, ds, strTabella, nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, _
        nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, bSeldocumenti, strTipork, nTipobf, nCoddest, nCodagen, _
        nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, bVistatiEntrambi, bVistati, bFlcontEntrambe, _
        bFlcont, strCodcfam, bModTCO, bSelAnnoStag, nAnnotco, nCodstag, strQuery, bFlevas, nTippaga, bModuloCRM, _
        bAmm, strAccvis, strAccmod, strRegvis, strRegmod, bIsCRMUser, lCodorgaOperat, nTiporeport, _
        bCaricaDatiPerGriglia, bIs15, nCodcageAccdito, lIITtsubqcrm, 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function CheckReports(ByVal strDitta As String, ByRef ds As DataSet, ByVal strTabella As String, _
    ByVal nDaconto As Integer, ByVal nAconto As Integer, _
    ByVal nAnno As Integer, ByVal strSerie As String, _
    ByVal dateDatini As Date, ByVal dateDatfin As Date, _
    ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, _
    ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
    ByVal bSeldocumenti As Boolean, ByVal strTipork As String, _
    ByVal nTipobf As Integer, ByVal nCoddest As Integer, _
    ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
    ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
    ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
    ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
    ByVal strCodcfam As String, ByVal bModTCO As Boolean, _
    ByVal bSelAnnoStag As Boolean, ByVal nAnnotco As Integer, _
    ByVal nCodstag As Integer, ByVal strQuery As String, _
    ByVal bFlevas As Boolean, ByVal nTippaga As Integer, _
    ByVal bModuloCRM As Boolean, ByVal bAmm As Boolean, _
    ByVal strAccvis As String, ByVal strAccmod As String, _
    ByVal strRegvis As String, ByVal strRegmod As String, _
    ByVal bIsCRMUser As Boolean, ByVal lCodorgaOperat As Integer, _
    ByVal nTiporeport As Integer, ByVal bCaricaDatiPerGriglia As Boolean, _
    ByVal bIs15 As Boolean, ByVal nCodcageAccdito As Integer, ByVal lIITtsubqcrm As Integer, _
    ByVal nCodlsel As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strTabellaMov As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, ds, strTabella, nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, _
        nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, bSeldocumenti, strTipork, nTipobf, nCoddest, nCodagen, _
        nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, bVistatiEntrambi, bVistati, bFlcontEntrambe, _
        bFlcont, strCodcfam, bModTCO, bSelAnnoStag, nAnnotco, nCodstag, strQuery, bFlevas, nTippaga, bModuloCRM, _
        bAmm, strAccvis, strAccmod, strRegvis, strRegmod, bIsCRMUser, lCodorgaOperat, nTiporeport, _
        bCaricaDatiPerGriglia, bIs15, nCodcageAccdito, lIITtsubqcrm, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strTabellaMov = "mov" & strTabella.Substring(4)
      '--------------------------------------------------------------------------------------------------------------
      If bCaricaDatiPerGriglia = True Then
        strSQL = "SELECT tm_conto, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_flagiva_1 " & _
          " FROM " & strTabella & " LEFT JOIN " & strTabellaMov & " ON " & strTabella & ".codditt = " & strTabellaMov & ".codditt AND " & strTabella & ".tm_tipork = " & strTabellaMov & ".mm_tipork AND " & strTabella & ".tm_anno = " & strTabellaMov & ".mm_anno AND " & strTabella & ".tm_serie = " & strTabellaMov & ".mm_serie AND " & strTabella & ".tm_numdoc = " & strTabellaMov & ".mm_numdoc" & _
          " INNER JOIN anagra ON " & strTabella & ".codditt = anagra.codditt AND " & strTabella & ".tm_conto = anagra.an_conto" & _
          " INNER JOIN tabpaga ON " & strTabella & ".tm_codpaga = tabpaga.tb_codpaga" & _
          " LEFT JOIN artfasi ON " & strTabellaMov & ".codditt = artfasi.codditt AND " & strTabellaMov & ".mm_codart = artfasi.af_codart AND " & strTabellaMov & ".mm_fase = artfasi.af_fase"
      Else
        'If nTippaga > 0 Then
        '  strSQL = "SELECT TOP 1 tm_conto, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_flagiva_1 FROM " & strTabella & " INNER JOIN tabpaga ON " & strTabella & ".tm_codpaga = tabpaga.tb_codpaga"
        'Else
        strSQL = "SELECT tm_conto, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_flagiva_1 FROM " & strTabella
        'End If
        strSQL &= " LEFT JOIN " & strTabellaMov & " ON " & strTabella & ".codditt = " & strTabellaMov & ".codditt AND " & strTabella & ".tm_tipork = " & strTabellaMov & ".mm_tipork AND " & strTabella & ".tm_anno = " & strTabellaMov & ".mm_anno AND " & strTabella & ".tm_serie = " & strTabellaMov & ".mm_serie AND " & strTabella & ".tm_numdoc = " & strTabellaMov & ".mm_numdoc" & _
                  " INNER JOIN anagra ON " & strTabella & ".codditt = anagra.codditt AND " & strTabella & ".tm_conto = anagra.an_conto" & _
                  " INNER JOIN tabpaga ON " & strTabella & ".tm_codpaga = tabpaga.tb_codpaga"
        'If (bModuloCRM = True) And (bIsCRMUser = True) Then
        '  strSQL += " INNER JOIN anagra ON " & strTabella & ".codditt = anagra.codditt AND " & strTabella & ".tm_conto = anagra.an_conto"
        'End If
      End If
      If (bModuloCRM = True) And (bIsCRMUser = True) And (bIs15 = True) Then
        strSQL += " LEFT JOIN TTSUBQCRM ON TTSUBQCRM.codditt = " & strTabella & ".codditt AND TTSUBQCRM.sc_conto = " & strTabella & ".tm_conto AND TTSUBQCRM.sc_coddest = " & strTabella & ".tm_coddest"
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL += " WHERE " & strTabella & ".codditt = " & CStrSQL(strDitta) & _
        " AND tm_anno = " & nAnno
      If (nDaconto <> 0) Or (nAconto <> 999999999) Then
        strSQL += " AND tm_conto BETWEEN " & nDaconto & " AND " & nAconto
      End If
      If (NTSCDate(dateDatini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(dateDatfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND tm_datdoc BETWEEN " & CDataSQL(dateDatini) & " AND " & CDataSQL(dateDatfin)
      End If
      If (nDanumdoc <> 0) Or (nAnumdoc <> 999999999) Then
        strSQL += " AND tm_numdoc BETWEEN " & nDanumdoc & " AND " & nAnumdoc
      End If
      If (nCommecaini <> 0) Or (nCommecafin <> 999999999) Then
        strSQL += " AND tm_commeca BETWEEN " & nCommecaini & " AND " & nCommecafin
      End If
      If nTippaga > 0 Then strSQL += " AND tabpaga.tb_tippaga = " & nTippaga
      If bSeldocumenti Then strSQL += " AND tm_tipork = '" & strTipork & "'"
      If strSerie <> "" Then strSQL += " AND tm_serie = '" & strSerie & "'"
      If nTipobf <> 0 Then strSQL += " AND tm_tipobf = " & nTipobf
      If nCodagen <> 0 Then strSQL += " AND tm_codagen = " & nCodagen
      If nCodagen2 <> 0 Then strSQL += " AND tm_codagen2 = " & nCodagen2
      If nCoddest <> 0 Then strSQL += " AND tm_coddest = " & nCoddest
      If bBolleFatturateEntrambe = False Then
        If opBolleFatturate = True Then strSQL += " AND tm_flfatt = 'S'" Else strSQL += " AND tm_flfatt = 'N'"
      End If
      If bVistatiEntrambi = False Then
        If bVistati = True Then strSQL += " AND tm_vistato = 'S'" Else strSQL += " AND tm_vistato = 'N'"
      End If
      If bFlevas = True Then strSQL += " AND tm_flevas = 'N'"
      If bFlcontEntrambe = False Then
        If bFlcont = True Then strSQL += " AND tm_flcont = 'S'" Else strSQL += " AND tm_flcont = 'N'"
      End If
      If bCaricaDatiPerGriglia = False Then
        Select Case nTiporeport
          Case 1    'In Lire senza scorporo
            strSQL += " AND tm_valuta = 0 AND tm_scorpo = 'N'"
          Case 2    'In valuta
            strSQL += " AND tm_valuta > 0 AND tm_scorpo = 'N'"
          Case 3    'In Lire con scorporo
            strSQL += " AND tm_valuta = 0 AND tm_scorpo = 'S'"
        End Select
      End If
      'Condizione per Linea/Famiglia
      If Trim(strCodcfam) <> "" Then strSQL += " AND tm_codcfam = " & CStrSQL(strCodcfam) 'Modulo TC
      If (bModTCO = True) And (bSelAnnoStag = True) Then
        If nAnnotco > 0 Then strSQL += " AND tm_annotco = " & nAnnotco
        If nCodstag > 0 Then strSQL += " AND tm_codstag = " & nCodstag
      End If
      If nCodlsel > 0 Then
        strSQL += " AND tm_conto IN (SELECT lse_conto FROM listsel" & _
                                   " WHERE codditt = " & CStrSQL(strDitta) & _
                                   " AND lse_codlsel = " & nCodlsel & ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se crm, allora mette anche solo i clienti che sono nel potere di visibilità dell'utente...
      '--------------------------------------------------------------------------------------------------------------
      If (bModuloCRM = True) And (bIsCRMUser = True) Then
        If bIs15 = False Then
          strSQL += " AND ("
          If strAccvis <> "T" Then
            strSQL += " (an_tipo = 'C' and an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
              " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
              " AND leads.le_coddest = 0"
            Select Case strAccvis
              Case "P" : strSQL += " AND le_opinc = " & lCodorgaOperat
              Case "C" : strSQL += " AND le_opinc IN (" & strRegvis & ")"
            End Select
            strSQL += ")) "
          Else
            strSQL += " an_tipo = 'C' "
          End If
          If bAmm Then strSQL += " OR an_tipo <> 'C'"
          strSQL += ")"
        Else
          If nCodcageAccdito <> 0 Then
            strSQL += " AND (tm_codagen = " & nCodcageAccdito & " OR tm_codagen2 = " & nCodcageAccdito & ")"
          End If
          strSQL += " AND TTSUBQCRM.instid = " & lIITtsubqcrm
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery.Replace("testmag.", "").Replace("movmag.", ""), strSQL)
      '--------------------------------------------------------------------------------------------------------------
      If bCaricaDatiPerGriglia = True Then
        strSQL += " GROUP BY tm_conto, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_flagiva_1" & _
                  " ORDER BY tm_tipork, tm_anno, tm_serie, tm_numdoc"
      Else
        strSQL += " GROUP BY tm_conto, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_flagiva_1" & _
                  " ORDER BY tm_flagiva_1 DESC"
      End If

      '--------------------------------------------------------------------------------------------------------------
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strTabella)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetQueryStampaPDF(ByVal strDitta As String, ByRef ds As DataSet, ByVal strTabella As String, _
                                                ByVal nDaconto As Integer, ByVal nAconto As Integer, _
                                                ByVal nAnno As Integer, ByVal strSerie As String, _
                                                ByVal dateDatini As Date, ByVal dateDatfin As Date, _
                                                ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, _
                                                ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
                                                ByVal bSeldocumenti As Boolean, ByVal strTipork As String, _
                                                ByVal nTipobf As Integer, ByVal nCoddest As Integer, _
                                                ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
                                                ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
                                                ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
                                                ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
                                                ByVal strCodcfam As String, ByVal bModTCO As Boolean, _
                                                ByVal bSelAnnoStag As Boolean, ByVal nAnnotco As Integer, _
                                                ByVal nCodstag As Integer, ByVal strQuery As String, _
                                                ByVal bFlevas As Boolean, ByVal nTippaga As Integer, _
                                                ByVal bModuloCRM As Boolean, ByVal bAmm As Boolean, _
                                                ByVal strAccvis As String, ByVal strAccmod As String, _
                                                ByVal strRegvis As String, ByVal strRegmod As String, _
                                                ByVal bIsCRMUser As Boolean, ByVal lCodorgaOperat As Integer, _
                                                ByRef strQueryGetDocUnico As String, _
                                                ByVal bIs15 As Boolean, ByVal nCodcageAccdito As Integer, ByVal lIITtsubqcrm As Integer) As String
    Try
      Return GetQueryStampaPDF(strDitta, ds, strTabella, nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, _
                               nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, bSeldocumenti, strTipork, nTipobf, nCoddest, _
                               nCodagen, nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, bVistatiEntrambi, bVistati, _
                               bFlcontEntrambe, bFlcont, strCodcfam, bModTCO, bSelAnnoStag, nAnnotco, nCodstag, strQuery, _
                               bFlevas, nTippaga, bModuloCRM, bAmm, strAccvis, strAccmod, strRegvis, strRegmod, _
                               bIsCRMUser, lCodorgaOperat, strQueryGetDocUnico, bIs15, nCodcageAccdito, lIITtsubqcrm, "", "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryStampaPDF(ByVal strDitta As String, ByRef ds As DataSet, ByVal strTabella As String, _
                                                ByVal nDaconto As Integer, ByVal nAconto As Integer, _
                                                ByVal nAnno As Integer, ByVal strSerie As String, _
                                                ByVal dateDatini As Date, ByVal dateDatfin As Date, _
                                                ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, _
                                                ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
                                                ByVal bSeldocumenti As Boolean, ByVal strTipork As String, _
                                                ByVal nTipobf As Integer, ByVal nCoddest As Integer, _
                                                ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
                                                ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
                                                ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
                                                ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
                                                ByVal strCodcfam As String, ByVal bModTCO As Boolean, _
                                                ByVal bSelAnnoStag As Boolean, ByVal nAnnotco As Integer, _
                                                ByVal nCodstag As Integer, ByVal strQuery As String, _
                                                ByVal bFlevas As Boolean, ByVal nTippaga As Integer, _
                                                ByVal bModuloCRM As Boolean, ByVal bAmm As Boolean, _
                                                ByVal strAccvis As String, ByVal strAccmod As String, _
                                                ByVal strRegvis As String, ByVal strRegmod As String, _
                                                ByVal bIsCRMUser As Boolean, ByVal lCodorgaOperat As Integer, _
                                                ByRef strQueryGetDocUnico As String, _
                                                ByVal bIs15 As Boolean, ByVal nCodcageAccdito As Integer, ByVal lIITtsubqcrm As Integer, _
                                                ByRef strQueryGetDocClienti As String, ByRef strQueryGetDocAgenti As String) As String
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, ds, strTabella, nDaconto, nAconto, nAnno, strSerie, _
                                             dateDatini, dateDatfin, nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, _
                                             bSeldocumenti, strTipork, nTipobf, nCoddest, nCodagen, nCodagen2, _
                                             bBolleFatturateEntrambe, opBolleFatturate, bVistatiEntrambi, bVistati, _
                                             bFlcontEntrambe, bFlcont, strCodcfam, bModTCO, bSelAnnoStag, nAnnotco, _
                                             nCodstag, strQuery, bFlevas, nTippaga, bModuloCRM, bAmm, strAccvis, _
                                             strAccmod, strRegvis, strRegmod, bIsCRMUser, lCodorgaOperat, _
                                             strQueryGetDocUnico, bIs15, nCodcageAccdito, lIITtsubqcrm, _
                                             strQueryGetDocClienti, strQueryGetDocAgenti})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        strQueryGetDocUnico = NTSCStr(oIn(41))
        strQueryGetDocClienti = NTSCStr(oIn(45))
        strQueryGetDocAgenti = NTSCStr(oIn(46))
        Return NTSCStr(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      Return GetQueryStampaPDF(strDitta, ds, strTabella, nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, _
        nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, bSeldocumenti, strTipork, nTipobf, nCoddest, _
        nCodagen, nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, bVistatiEntrambi, bVistati, _
        bFlcontEntrambe, bFlcont, strCodcfam, bModTCO, bSelAnnoStag, nAnnotco, nCodstag, strQuery, _
        bFlevas, nTippaga, bModuloCRM, bAmm, strAccvis, strAccmod, strRegvis, strRegmod, _
        bIsCRMUser, lCodorgaOperat, strQueryGetDocUnico, bIs15, nCodcageAccdito, lIITtsubqcrm, _
        strQueryGetDocClienti, strQueryGetDocAgenti, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetQueryStampaPDF(ByVal strDitta As String, ByRef ds As DataSet, ByVal strTabella As String, _
                                                ByVal nDaconto As Integer, ByVal nAconto As Integer, _
                                                ByVal nAnno As Integer, ByVal strSerie As String, _
                                                ByVal dateDatini As Date, ByVal dateDatfin As Date, _
                                                ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, _
                                                ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
                                                ByVal bSeldocumenti As Boolean, ByVal strTipork As String, _
                                                ByVal nTipobf As Integer, ByVal nCoddest As Integer, _
                                                ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
                                                ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
                                                ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
                                                ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
                                                ByVal strCodcfam As String, ByVal bModTCO As Boolean, _
                                                ByVal bSelAnnoStag As Boolean, ByVal nAnnotco As Integer, _
                                                ByVal nCodstag As Integer, ByVal strQuery As String, _
                                                ByVal bFlevas As Boolean, ByVal nTippaga As Integer, _
                                                ByVal bModuloCRM As Boolean, ByVal bAmm As Boolean, _
                                                ByVal strAccvis As String, ByVal strAccmod As String, _
                                                ByVal strRegvis As String, ByVal strRegmod As String, _
                                                ByVal bIsCRMUser As Boolean, ByVal lCodorgaOperat As Integer, _
                                                ByRef strQueryGetDocUnico As String, _
                                                ByVal bIs15 As Boolean, ByVal nCodcageAccdito As Integer, ByVal lIITtsubqcrm As Integer, _
                                                ByRef strQueryGetDocClienti As String, ByRef strQueryGetDocAgenti As String, _
                                                ByVal bUsaReportStandard As Boolean) As String

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, ds, strTabella, nDaconto, nAconto, nAnno, strSerie, _
                                             dateDatini, dateDatfin, nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, _
                                             bSeldocumenti, strTipork, nTipobf, nCoddest, nCodagen, nCodagen2, _
                                             bBolleFatturateEntrambe, opBolleFatturate, bVistatiEntrambi, bVistati, _
                                             bFlcontEntrambe, bFlcont, strCodcfam, bModTCO, bSelAnnoStag, nAnnotco, _
                                             nCodstag, strQuery, bFlevas, nTippaga, bModuloCRM, bAmm, strAccvis, _
                                             strAccmod, strRegvis, strRegmod, bIsCRMUser, lCodorgaOperat, _
                                             strQueryGetDocUnico, bIs15, nCodcageAccdito, lIITtsubqcrm, _
                                             strQueryGetDocClienti, strQueryGetDocAgenti, bUsaReportStandard})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        strQueryGetDocUnico = NTSCStr(oIn(41))
        strQueryGetDocClienti = NTSCStr(oIn(45))
        strQueryGetDocAgenti = NTSCStr(oIn(46))
        Return NTSCStr(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return GetQueryStampaPDF(strDitta, ds, strTabella, nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, _
        nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, bSeldocumenti, strTipork, nTipobf, nCoddest, _
        nCodagen, nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, bVistatiEntrambi, bVistati, _
        bFlcontEntrambe, bFlcont, strCodcfam, bModTCO, bSelAnnoStag, nAnnotco, nCodstag, strQuery, _
        bFlevas, nTippaga, bModuloCRM, bAmm, strAccvis, strAccmod, strRegvis, strRegmod, _
        bIsCRMUser, lCodorgaOperat, strQueryGetDocUnico, bIs15, nCodcageAccdito, lIITtsubqcrm, _
        strQueryGetDocClienti, strQueryGetDocAgenti, bUsaReportStandard, 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetQueryStampaPDF(ByVal strDitta As String, ByRef ds As DataSet, ByVal strTabella As String, _
                                             ByVal nDaconto As Integer, ByVal nAconto As Integer, _
                                             ByVal nAnno As Integer, ByVal strSerie As String, _
                                             ByVal dateDatini As Date, ByVal dateDatfin As Date, _
                                             ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, _
                                             ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
                                             ByVal bSeldocumenti As Boolean, ByVal strTipork As String, _
                                             ByVal nTipobf As Integer, ByVal nCoddest As Integer, _
                                             ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
                                             ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
                                             ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
                                             ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
                                             ByVal strCodcfam As String, ByVal bModTCO As Boolean, _
                                             ByVal bSelAnnoStag As Boolean, ByVal nAnnotco As Integer, _
                                             ByVal nCodstag As Integer, ByVal strQuery As String, _
                                             ByVal bFlevas As Boolean, ByVal nTippaga As Integer, _
                                             ByVal bModuloCRM As Boolean, ByVal bAmm As Boolean, _
                                             ByVal strAccvis As String, ByVal strAccmod As String, _
                                             ByVal strRegvis As String, ByVal strRegmod As String, _
                                             ByVal bIsCRMUser As Boolean, ByVal lCodorgaOperat As Integer, _
                                             ByRef strQueryGetDocUnico As String, _
                                             ByVal bIs15 As Boolean, ByVal nCodcageAccdito As Integer, ByVal lIITtsubqcrm As Integer, _
                                             ByRef strQueryGetDocClienti As String, ByRef strQueryGetDocAgenti As String, _
                                             ByVal bUsaReportStandard As Boolean, ByVal nCodlsel As Integer) As String
    Dim strSQL As String = ""
    Dim strTabellaMov As String
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, ds, strTabella, nDaconto, nAconto, nAnno, strSerie, _
                                             dateDatini, dateDatfin, nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, _
                                             bSeldocumenti, strTipork, nTipobf, nCoddest, nCodagen, nCodagen2, _
                                             bBolleFatturateEntrambe, opBolleFatturate, bVistatiEntrambi, bVistati, _
                                             bFlcontEntrambe, bFlcont, strCodcfam, bModTCO, bSelAnnoStag, nAnnotco, _
                                             nCodstag, strQuery, bFlevas, nTippaga, bModuloCRM, bAmm, strAccvis, _
                                             strAccmod, strRegvis, strRegmod, bIsCRMUser, lCodorgaOperat, _
                                             strQueryGetDocUnico, bIs15, nCodcageAccdito, lIITtsubqcrm, _
                                             strQueryGetDocClienti, strQueryGetDocAgenti, bUsaReportStandard, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        strQueryGetDocUnico = NTSCStr(oIn(41))
        strQueryGetDocClienti = NTSCStr(oIn(45))
        strQueryGetDocAgenti = NTSCStr(oIn(46))
        Return NTSCStr(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strTabellaMov = "mov" & strTabella.Substring(4)
      '--------------------------------------------------------------------------------------------------------------
      'Condizione sul pagamento
      If nTippaga > 0 Then
        strSQL = " FROM " & strTabella & " INNER JOIN tabpaga ON " & strTabella & ".tm_codpaga = tabpaga.tb_codpaga"
      Else
        strSQL = " FROM " & strTabella
      End If
      strSQL += " INNER JOIN anagra ON " & strTabella & ".codditt = anagra.codditt AND " & strTabella & ".tm_conto = anagra.an_conto"
      If bModuloCRM AndAlso bIsCRMUser Then
        strSQL += " LEFT JOIN TTSUBQCRM ON TTSUBQCRM.codditt = " & strTabella & ".codditt AND TTSUBQCRM.sc_conto = " & strTabella & ".tm_conto AND TTSUBQCRM.sc_coddest = " & strTabella & ".tm_coddest"
      End If
      If (strTipork <> "D") AndAlso (strTipork <> "K") AndAlso (strTipork <> "£") Then
        strSQL &= " INNER JOIN " & strTabellaMov & _
          " ON " & strTabellaMov & ".codditt = " & strTabella & ".codditt" & _
          " AND tm_tipork = mm_tipork" & _
          " AND mm_anno = tm_anno" & _
          " AND mm_serie = tm_serie" & _
          " AND mm_numdoc = tm_numdoc"
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL += " WHERE " & strTabella & ".codditt = " & CStrSQL(strDitta)
      If nTippaga > 0 Then strSQL += " AND tabpaga.tb_tippaga = " & nTippaga
      strSQL += " AND tm_anno = " & nAnno & _
        " AND tm_conto BETWEEN " & nDaconto & " AND " & nAconto & _
        " AND tm_datdoc BETWEEN " & CDataSQL(dateDatini) & " AND " & CDataSQL(dateDatfin) & _
        " AND tm_numdoc BETWEEN " & nDanumdoc & " AND " & nAnumdoc & _
        " AND tm_commeca BETWEEN " & nCommecaini & " AND " & nCommecafin
      If bSeldocumenti = True Then strSQL += " AND tm_tipork = '" & strTipork & "'"
      If strSerie <> "" Then strSQL += " AND tm_serie = '" & strSerie & "'"
      If nTipobf <> 0 Then strSQL += " AND tm_tipobf = " & nTipobf
      If nCodagen <> 0 Then strSQL += " AND tm_codagen = " & nCodagen
      If nCodagen2 <> 0 Then strSQL += " AND tm_codagen2 = " & nCodagen2
      If nCoddest <> 0 Then strSQL += " AND tm_coddest = " & nCoddest
      If bBolleFatturateEntrambe = False Then
        If opBolleFatturate = True Then strSQL += " AND tm_flfatt = 'S'" Else strSQL += " AND tm_flfatt = 'N'"
      End If
      If bVistatiEntrambi = False Then
        If bVistati = True Then strSQL += " AND tm_vistato = 'S'" Else strSQL += " AND tm_vistato = 'N'"
      End If
      If bFlevas = True Then strSQL += " AND tm_flevas = 'N'"
      If bFlcontEntrambe = False Then
        If bFlcont = True Then strSQL += " AND tm_flcont = 'S'" Else strSQL += " AND tm_flcont = 'N'"
      End If
      If Trim(strCodcfam) <> "" Then strSQL += " AND tm_codcfam = " & CStrSQL(strCodcfam)
      If (bModTCO = True) And (bSelAnnoStag = True) Then
        If nAnnotco > 0 Then strSQL += " AND tm_annotco = " & nAnnotco
        If nCodstag > 0 Then strSQL += " AND tm_codstag = " & nCodstag
      End If
      If nCodlsel > 0 Then
        strSQL += " AND tm_conto IN (SELECT lse_conto FROM listsel" & _
                  "                   WHERE codditt = " & CStrSQL(strDitta) & _
                  "                     AND lse_codlsel = " & nCodlsel & ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      'Se crm, allora mette anche solo i clienti che sono nel potere di visibilità dell'utente...
      If (bModuloCRM = True) And (bIsCRMUser = True) Then
        If bIs15 = False Then
          strSQL += " AND ("
          If strAccvis <> "T" Then
            strSQL += " (an_tipo = 'C' and an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
              " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
              " AND leads.le_coddest = 0"
            Select Case strAccvis
              Case "P" : strSQL += " AND le_opinc = " & lCodorgaOperat
              Case "C" : strSQL += " AND le_opinc IN (" & strRegvis & ")"
            End Select
            strSQL += "))"
          Else
            strSQL += " an_tipo = 'C'"
          End If
          If bAmm Then strSQL += " OR an_tipo <> 'C'"
          strSQL += ")"
        Else
          If nCodcageAccdito <> 0 Then
            strSQL += " AND (tm_codagen = " & nCodcageAccdito & " OR tm_codagen2 = " & nCodcageAccdito & ")"
          End If
          strSQL += " AND TTSUBQCRM.instid = " & lIITtsubqcrm
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      'Aggiungo la where dei campi con OR e AND rimappati
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(Replace(strQuery, "testmag.", ""), strSQL)

      '--------------------------------------------------------------------------------------------------------------
      'Questa serve per fare un pdf unico (in realtà i pdf potrebbero essere anche più di uno, 
      'ma uno unico per i documenti in valuta, uno unico per quelli con scorporo, uno unico per i restanti
      strQueryGetDocUnico = "SELECT" & _
            IIf(bSeldocumenti = False And bUsaReportStandard = False, " TOP 1", "").ToString & _
            NTSCStr(IIf(bSeldocumenti, " tm_tipork", " ' '")) & _
            " as tipork, 0 as anno, ' ' as serie, 0 as numero, 0 as conto," & _
            IIf(bUsaReportStandard = True, " tm_valuta", " 0").ToString & " as valuta," & _
            IIf(bUsaReportStandard = True, " tm_scorpo", " 'N'").ToString & " as scorpo," & _
            " 0 as agente " & strSQL
      If (bSeldocumenti = True) Or (bUsaReportStandard = True) Then
        strQueryGetDocUnico += " GROUP BY " & NTSCStr(IIf(bSeldocumenti, "tm_tipork", "")) & IIf(bUsaReportStandard = True, ", tm_valuta, tm_scorpo ", "").ToString & _
                               " ORDER BY " & NTSCStr(IIf(bSeldocumenti, "tm_tipork", "")) & IIf(bUsaReportStandard = True, ", tm_valuta, tm_scorpo ", "").ToString
      End If
      '--------------------------------------------------------------------------------------------------------------
      'raggruppoato per cliente
      strQueryGetDocClienti = " SELECT " & NTSCStr(IIf(bSeldocumenti, "tm_tipork", "' '")) & " as tipork, " & _
            " 0 as anno, ' ' as serie, 0 as numero, tm_conto as conto, tm_valuta as valuta, tm_scorpo as scorpo, 0 as agente" & _
            strSQL & _
            " GROUP BY " & NTSCStr(IIf(bSeldocumenti, "tm_tipork, ", "")) & "tm_conto, tm_valuta, tm_scorpo " & _
            " ORDER BY " & NTSCStr(IIf(bSeldocumenti, "tm_tipork, ", "")) & "tm_conto, tm_valuta, tm_scorpo "
      '--------------------------------------------------------------------------------------------------------------
      'raggruppato per agente
      strQueryGetDocAgenti = " SELECT " & NTSCStr(IIf(bSeldocumenti, "tm_tipork", "' '")) & " as tipork, " & _
            " 0 as anno, ' ' as serie, 0 as numero," & _
            " 0 as conto, tm_valuta as valuta, tm_scorpo as scorpo, tm_codagen as agente" & _
            strSQL & _
            " GROUP BY " & NTSCStr(IIf(bSeldocumenti, "tm_tipork, ", "")) & "tm_codagen, tm_valuta, tm_scorpo " & _
            " ORDER BY " & NTSCStr(IIf(bSeldocumenti, "tm_tipork, ", "")) & "tm_codagen, tm_valuta, tm_scorpo "
      '--------------------------------------------------------------------------------------------------------------
      'un file per documento
      Return " SELECT DISTINCT tm_tipork as tipork, tm_anno as anno, tm_serie as serie, tm_numdoc as numero," & _
             " tm_conto as conto, tm_valuta as valuta, tm_scorpo as scorpo, tm_codagen as agente" & _
             strSQL & _
             " GROUP BY tm_valuta, tm_scorpo, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_conto, tm_codagen" & _
             " ORDER BY tm_valuta, tm_scorpo, tm_tipork, tm_anno, tm_serie, tm_numdoc "
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function RiempiTTSUBQCRM(ByVal strDitta As String, ByVal lIITtsubqcrm As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      'se serve aggiungo anche i fornitori
      strSQL = "SELECT * FROM accdito WHERE codditt = " & CStrSQL(strDitta) & " AND opdi_opnome = " & CStrSQL(oApp.User.Nome)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTSUBQCRM (codditt, instid, sc_conto, sc_coddest, sc_codlead)" & _
        " SELECT " & CStrSQL(strDitta) & ", " & lIITtsubqcrm & ", le_conto, le_coddest, le_codlead" & _
        " FROM leads" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND le_codlead IN " & SubQueryFiltroLeadVis(strDitta)
      If dttTmp.Rows.Count > 0 Then
        If NTSCStr(dttTmp.Rows(0)!opdi_amm).ToUpper = "S" Then
          strSQL += " UNION select " & CStrSQL(strDitta) & ", " & lIITtsubqcrm & ", an_conto, 0, 0 " & _
                    " FROM anagra " & _
                    " WHERE codditt = " & CStrSQL(strDitta) & " AND an_tipo = 'F'"
          'se ci sono anche destinazioni diverse su fornitori ...
          strSQL += " UNION select " & CStrSQL(strDitta) & ", " & lIITtsubqcrm & ", an_conto, dd_coddest, 0 " & _
                    " FROM anagra INNER JOIN destdiv ON anagra.codditt = destdiv.codditt AND " & _
                    " anagra.an_conto = destdiv.dd_conto " & _
                    " WHERE anagra.codditt = " & CStrSQL(strDitta) & " AND an_tipo = 'F'"
        End If
      End If
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function RitornaCodcageAccidto(ByVal strDitta As String) As Integer
    Dim strSQL As String
    Dim dttTmp As New DataSet
    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT opdi_codcage FROM accdito" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND opdi_opnome = " & CStrSQL(oApp.User.Nome)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ACCDITO")
      If dttTmp.Tables("ACCDITO").Rows.Count = 0 Then Return 0
      Return NTSCInt(dttTmp.Tables("ACCDITO").Rows(0)!opdi_codcage)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
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
