Imports System.IO
Imports NTSInformatica.CLN__STD
Imports NTSInformatica
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data.Common

Public Class CLDVEPACK
  Inherits CLD__BASE

  Public Overridable Function LeggiTabellaMovPac(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movpac.*, '' as xx_lottox FROM MOVPAC WHERE mp_anno = -1"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      dttOut.TableName = "MOVPAC"
      dttOut.Columns.Add("xx_selez")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function LeggiTabellaTestPac(ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM TESTPAC WHERE tp_anno = -1"

      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTPAC", dsOut)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiDescrizioneConto(ByVal strDitta As String, ByVal lConto As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT an_descr1 FROM ANAGRA WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lConto

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function LeggiDatiConto(ByVal strDitta As String, ByVal lConto As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT an_descr1, an_descr2, an_indir, an_cap, an_citta, an_prov, an_codling, an_tipo FROM ANAGRA " & _
               "WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lConto

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SelezionaDocumento(ByVal strDitta As String, ByVal strTipork As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                                 ByVal lNumero As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      If strTipork = "W" Then
        strSQL = "SELECT codditt, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_totcoll, tm_vettor, tm_conto, tm_coddest2, tm_note, tm_datdoc," & _
                 "tm_peso, tm_coddest, tm_pesonetto, tm_riferim, tm_tipobf, tm_vettor2 FROM testprb WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND tm_tipork = " & CStrSQL(strTipork) & " AND tm_anno = " & lAnno & " AND tm_serie = " & CStrSQL(strSerie) & " AND tm_numdoc = " & lNumero
      Else
        strSQL = "SELECT codditt, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_totcoll, tm_vettor, tm_conto, tm_coddest2, tm_note, tm_datdoc," & _
                 "tm_peso, tm_coddest, tm_pesonetto, tm_riferim, tm_tipobf, tm_vettor2 FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND tm_tipork = " & CStrSQL(strTipork) & " AND tm_anno = " & lAnno & " AND tm_serie = " & CStrSQL(strSerie) & " AND tm_numdoc = " & lNumero
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelezionaMovmag(ByVal strDitta As String, ByVal strTipork As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                              ByVal lNumDoc As Integer, ByVal lNumRiga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      If strTipork = "W" Then
        strSQL = "SELECT movprb.codditt,movprb. mm_riga, movprb.mm_tipork, movprb.mm_anno, movprb.mm_serie, movprb.mm_numdoc, mm_codart, mm_unmis, mm_descr, mm_colli, " & _
                 " mm_quant, mma_note, mma_rigaa, mm_note, mm_desint, mma_matric, mma_quant, mm_ump, mm_misura1, mm_misura2, mm_misura3, mm_lotto, alo_lottox as xx_lottox, " & _
                 " mm_quant01, mm_quant02, mm_quant03, mm_quant04, mm_quant05, mm_quant06, mm_quant07, mm_quant08, mm_quant09, mm_quant10," & _
                 " mm_quant11, mm_quant12, mm_quant13, mm_quant14, mm_quant15, mm_quant16, mm_quant17, mm_quant18, mm_quant19, mm_quant20," & _
                 " mm_quant21, mm_quant22, mm_quant23, mm_quant24" & _
                 " FROM ((movprb LEFT JOIN movmatr ON movprb.codditt = movmatr.codditt AND movprb.mm_tipork = movmatr.mma_tipork " & _
                 " AND movprb.mm_anno = movmatr.mma_anno AND movprb.mm_serie = movmatr.mma_serie AND movprb.mm_numdoc = movmatr.mma_numdoc AND movprb.mm_riga = movmatr.mma_riga)" & _
                 " LEFT JOIN analotti ON movprb.codditt = analotti.codditt AND movprb.mm_codart = analotti.alo_codart AND movprb.mm_lotto = analotti.alo_lotto) " & _
                 " LEFT JOIN movmagtc ON movprb.codditt = movmagtc.codditt AND movprb.mm_tipork = movmagtc.mm_tipork AND movprb.mm_anno = movmagtc.mm_anno " & _
                 " AND movprb.mm_serie = movmagtc.mm_serie AND movprb.mm_numdoc = movmagtc.mm_numdoc AND movprb.mm_riga = movmagtc.mm_riga" & _
                 " WHERE movprb.codditt = " & CStrSQL(strDitta) & _
                 " AND movprb.mm_tipork = " & CStrSQL(strTipork) & _
                 " AND movprb.mm_anno = " & lAnno & _
                 " AND movprb.mm_serie = " & CStrSQL(strSerie) & _
                 " AND movprb.mm_numdoc = " & lNumDoc & _
                 " AND mm_codart <> 'D' " & _
                 " AND movprb.mm_riga > " & lNumRiga & _
                 " ORDER BY movprb.mm_riga"
      Else
        strSQL = "SELECT movmag.codditt, movmag.mm_riga, movmag.mm_tipork, movmag.mm_anno, movmag.mm_serie, movmag.mm_numdoc, mm_codart, mm_unmis, mm_descr, mm_colli, " & _
                   " mm_quant, mma_note, mma_rigaa, mm_note, mm_desint, mma_matric, mma_quant, mm_ump, mm_misura1, mm_misura2, mm_misura3, mm_lotto, alo_lottox as xx_lottox, " & _
                   " mm_quant01, mm_quant02, mm_quant03, mm_quant04, mm_quant05, mm_quant06, mm_quant07, mm_quant08, mm_quant09, mm_quant10," & _
                   " mm_quant11, mm_quant12, mm_quant13, mm_quant14, mm_quant15, mm_quant16, mm_quant17, mm_quant18, mm_quant19, mm_quant20," & _
                   " mm_quant21, mm_quant22, mm_quant23, mm_quant24" & _
                   " FROM movmag LEFT JOIN movmatr ON movmag.codditt = movmatr.codditt AND movmag.mm_tipork = movmatr.mma_tipork " & _
                   " AND movmag.mm_anno = movmatr.mma_anno AND movmag.mm_serie = movmatr.mma_serie AND movmag.mm_numdoc = movmatr.mma_numdoc AND movmag.mm_riga = movmatr.mma_riga" & _
                   " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto " & _
                   " LEFT JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno " & _
                   " AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
                   " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                   " AND movmag.mm_tipork = " & CStrSQL(strTipork) & _
                   " AND movmag.mm_anno = " & lAnno & _
                   " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
                   " AND movmag.mm_numdoc = " & lNumDoc & _
                   " AND mm_codart <> 'D'" & _
                   " AND movmag.mm_riga > " & lNumRiga & _
                   " ORDER BY movmag.mm_riga"
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelezionaMovmagJoinArtico(ByVal strDitta As String, ByVal strTipork As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                                        ByVal lNumdoc As Integer, ByVal lNumriga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movmag.codditt,  movmag.mm_riga, movmag.mm_tipork, movmag.mm_anno," & _
               " movmag.mm_serie, movmag.mm_numdoc, mm_codart, mm_unmis, mm_descr, mm_colli, mm_quant, mma_note," & _
               " mma_rigaa, mm_note, mm_desint, mma_matric, mma_quant, mm_ump, mm_lotto, alo_lottox as xx_lottox," & _
               " mm_quant01, mm_quant02, mm_quant03, mm_quant04, mm_quant05, mm_quant06, mm_quant07, mm_quant08, mm_quant09, mm_quant10," & _
               " mm_quant11, mm_quant12, mm_quant13, mm_quant14, mm_quant15, mm_quant16, mm_quant17, mm_quant18, mm_quant19, mm_quant20," & _
               " mm_quant21, mm_quant22, mm_quant23, mm_quant24" & _
               " FROM movmag INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart" & _
               " LEFT JOIN movmatr ON movmag.codditt = movmatr.codditt AND movmag.mm_tipork = movmatr.mma_tipork AND movmag.mm_anno = movmatr.mma_anno AND movmag.mm_serie = movmatr.mma_serie AND movmag.mm_numdoc = movmatr.mma_numdoc AND movmag.mm_riga = movmatr.mma_riga " & _
               " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto " & _
               " LEFT JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno " & _
               " AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
               " WHERE movmag.codditt = " & CStrSQL(strDitta) & " AND movmag.mm_tipork = " & CStrSQL(strTipork) & " AND movmag.mm_anno = " & lAnno & _
               " AND movmag.mm_serie = " & CStrSQL(strSerie) & " AND movmag.mm_numdoc = " & lNumdoc & " AND mm_codart <> " & CStrSQL("D") & " AND movmag.mm_riga > " & lNumriga & _
               " AND (mm_ortipo = " & CStrSQL("") & " OR mm_ortipo = " & CStrSQL(" ") & " OR mm_ortipo IS NULL)" & _
               " ORDER BY ar_ubicaz, mm_codart"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelezionaMovmagJoinMovord(ByVal strDitta As String, ByVal strTipork As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                                        ByVal lNumdoc As Integer, ByVal lNumriga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movmag.codditt, movmag.mm_riga, movmag.mm_tipork, movmag.mm_anno," & _
               " movmag.mm_serie, movmag.mm_numdoc, mm_codart, mm_unmis, mm_descr, mm_colli, mm_quant, mma_note," & _
               " mma_rigaa, mm_note, mm_desint, mma_matric, mma_quant, mm_ump, mm_lotto, alo_lottox as xx_lottox," & _
               " mm_quant01, mm_quant02, mm_quant03, mm_quant04, mm_quant05, mm_quant06, mm_quant07, mm_quant08, mm_quant09, mm_quant10," & _
               " mm_quant11, mm_quant12, mm_quant13, mm_quant14, mm_quant15, mm_quant16, mm_quant17, mm_quant18, mm_quant19, mm_quant20," & _
               " mm_quant21, mm_quant22, mm_quant23, mm_quant24" & _
               " FROM movmag INNER JOIN movord ON movmag.codditt = movord.codditt AND movmag.mm_ortipo = movord.mo_tipork AND movmag.mm_oranno = movord.mo_anno AND movmag.mm_orserie = movord.mo_serie AND movmag.mm_ornum = movord.mo_numord AND movmag.mm_orriga = movord.mo_riga" & _
               " INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart" & _
               " LEFT JOIN movmatr ON movmag.codditt = movmatr.codditt AND movmag.mm_tipork = movmatr.mma_tipork AND movmag.mm_anno = movmatr.mma_anno AND movmag.mm_serie = movmatr.mma_serie AND movmag.mm_numdoc = movmatr.mma_numdoc AND movmag.mm_riga = movmatr.mma_riga" & _
               " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto " & _
               " LEFT JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno " & _
               " AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
               " WHERE movmag.codditt = " & CStrSQL(strDitta) & " AND movmag.mm_tipork = " & CStrSQL(strTipork) & " AND movmag.mm_anno = " & lAnno & _
               " AND movmag.mm_serie = " & CStrSQL(strSerie) & " AND movmag.mm_numdoc = " & lNumdoc & " AND mm_codart <> " & CStrSQL("D") & " AND movmag.mm_riga > " & lNumriga & _
               " ORDER BY mo_tipork, mo_anno, mo_serie, mo_numord, ar_ubicaz, mm_codart, mo_riga"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SelezionaOrdine(ByVal strDitta As String, ByVal strTipork As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                              ByVal lNumdoc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT codditt, td_tipork, td_anno, td_serie, td_numord, td_totcoll, td_vettor," & _
              " td_conto, td_coddest2, td_note, td_datord, td_peso, td_coddest, td_pesonetto," & _
              " td_riferim, td_tipobf, td_vettor2 FROM testord" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND td_tipork = " & CStrSQL(strTipork) & _
              " AND td_anno = " & lAnno & _
              " AND td_serie = " & CStrSQL(strSerie) & _
              " AND td_numord = " & lNumdoc

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelezionaMovord(ByVal strDitta As String, ByVal strTipork As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                              ByVal lNumdoc As Integer, ByVal lNumriga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movord.codditt, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga, mo_codart," & _
               " mo_unmis, mo_descr, mo_colli, mo_quant, mo_note, mo_ump, mo_lotto, mo_desint, mo_misura1, mo_misura2, mo_misura3, alo_lottox as xx_lottox," & _
               " mo_quant01, mo_quant02, mo_quant03, mo_quant04, mo_quant05, mo_quant06, mo_quant07, mo_quant08, mo_quant09, mo_quant10," & _
               " mo_quant11, mo_quant12, mo_quant13, mo_quant14, mo_quant15, mo_quant16, mo_quant17, mo_quant18, mo_quant19, mo_quant20," & _
               " mo_quant21, mo_quant22, mo_quant23, mo_quant24" & _
               " FROM movord LEFT JOIN analotti ON movord.codditt = analotti.codditt AND movord.mo_codart = analotti.alo_codart AND movord.mo_lotto = analotti.alo_lotto " & _
               " LEFT JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno " & _
               " AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga" & _
               " WHERE movord.codditt = " & CStrSQL(strDitta) & _
               " AND movord.mo_tipork = " & CStrSQL(strTipork) & _
               " AND movord.mo_anno = " & lAnno & _
               " AND movord.mo_serie = " & CStrSQL(strSerie) & _
               " AND movord.mo_numord = " & lNumdoc & _
               " AND mo_codart <> " & CStrSQL("D") & _
               " AND movord.mo_riga > " & lNumriga & _
               " ORDER BY movord.mo_riga"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SelezionaPacking(ByVal strDitta As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                               ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM testpac" & _
             " WHERE codditt = " & CStrSQL(strDitta) & _
             " AND tp_anno = " & lAnno & _
             " AND tp_serie = " & CStrSQL(strSerie) & _
             " AND tp_numdoc = " & lNumdoc

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelezionaMovpac(ByVal strDitta As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                              ByVal lNumriga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movpac.*, alo_lottox as xx_lottox" & _
             " FROM movpac LEFT JOIN analotti ON movpac.codditt = analotti.codditt AND movpac.mp_codart = analotti.alo_codart AND movpac.mp_lotto = analotti.alo_lotto " & _
             " WHERE movpac.codditt = " & CStrSQL(strDitta) & " AND mp_anno = " & lAnno & " AND mp_serie = " & CStrSQL(strSerie) & _
             " AND mp_numdoc = " & lNumdoc & " AND mp_codart <> " & CStrSQL("D") & " AND mp_riga > " & lNumriga & " ORDER BY mp_riga"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ScriviSuMovPac(ByVal strDitta As String, ByRef dttMovPac As DataTable) As Boolean
    Dim dtrTmp As DataRow()
    Dim i As Integer
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim dbConn As DbConnection = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre la transazione
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      ' Salva righe cancellate
      dtrTmp = dttMovPac.Select("", "", DataViewRowState.Deleted)

      If dtrTmp.Length <> 0 Then
        For i = 0 To dtrTmp.Length - 1
          strSQL = "DELETE FROM MOVPAC "
          strWhere = "WHERE codditt = " & CStrSQL(dtrTmp(i)("codditt", DataRowVersion.Original)) & " AND mp_anno = " & NTSCInt(dtrTmp(i)("mp_anno", DataRowVersion.Original)) & _
                     " AND mp_serie = " & CStrSQL(dtrTmp(i)("mp_serie", DataRowVersion.Original)) & " AND mp_numdoc = " & NTSCInt(dtrTmp(i)("mp_numdoc", DataRowVersion.Original)) & _
                     " AND mp_riga = " & CDblSQL(NTSCDec(dtrTmp(i)("mp_riga", DataRowVersion.Original)))
          strSQL &= strWhere

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
      End If

      ' Salva righe aggiunte
      dtrTmp = dttMovPac.Select("", "", DataViewRowState.Added)

      If dtrTmp.Length <> 0 Then
        For i = 0 To dtrTmp.Length - 1
          With dtrTmp(i)
            If !mp_numpack Is Nothing Or NTSCStr(!mp_numpack) = "" Then !mp_numpack = " "
            If !mp_codvuo Is Nothing Or NTSCStr(!mp_codvuo) = "" Then !mp_codvuo = 0

            'Prima di inserire la riga esistente cancella eventuali vecchie righe con lo stesso progressivo (caso cancello una riga e la riaggiungo con lo stesso progressivo)
            strSQL = "DELETE FROM MOVPAC " & _
                     " WHERE codditt = " & CStrSQL(dtrTmp(i)!codditt) & " AND mp_anno = " & NTSCInt(dtrTmp(i)!mp_anno) & _
                     "   AND mp_serie = " & CStrSQL(dtrTmp(i)!mp_serie) & " AND mp_numdoc = " & NTSCInt(dtrTmp(i)!mp_numdoc) & _
                     "   AND mp_riga = " & CDblSQL(NTSCDec(dtrTmp(i)!mp_riga))

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "INSERT INTO MOVPAC " & GetQueryInsertField(dttMovPac, "mp_") & _
                     " VALUES " & GetQueryInsertValue(dttMovPac, dtrTmp(i), "mp_")

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End With
        Next
      End If

      ' Salva righe modificate
      dtrTmp = dttMovPac.Select("", "", DataViewRowState.ModifiedCurrent)
      If dtrTmp.Length <> 0 Then
        For i = 0 To dtrTmp.Length - 1
          With dtrTmp(i)
            If !mp_numpack Is Nothing Or NTSCStr(!mp_numpack) = "" Then !mp_numpack = " "
            If !mp_codvuo Is Nothing Or NTSCStr(!mp_codvuo) = "" Then !mp_codvuo = 0

            strSQL = "UPDATE MOVPAC SET " & GetQueryUpdate(dttMovPac, dtrTmp(i), "mp_")
            strWhere = " WHERE codditt = " & CStrSQL(!codditt) & " AND mp_anno = " & NTSCInt(!mp_anno) & " AND mp_serie = " & CStrSQL(!mp_serie) & _
                       " AND mp_numdoc = " & NTSCInt(!mp_numdoc) & " AND mp_riga = " & CDblSQL(NTSCDec(!mp_riga))

            strSQL &= strWhere

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End With
        Next
      End If

      dttMovPac.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiude la transazione
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ScriviSuTestPac(ByRef dttTestPac As DataTable, ByVal strOp As String) As Boolean
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim dbConn As DbConnection = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre la transazione
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      With dttTestPac.Rows(0)
        ' strOp: A="Aggiungi" | M="Modifica" | D="Cancella"
        If NTSCStr(dttTestPac.Rows(0)!tp_mmserie) = "" Or dttTestPac.Rows(0)!tp_mmserie Is Nothing Then dttTestPac.Rows(0)!tp_mmserie = " "
        Select Case strOp
          Case "A"
            strSQL = "INSERT INTO TESTPAC " & GetQueryInsertField(dttTestPac, "tp_") & _
                     " VALUES " & GetQueryInsertValue(dttTestPac, dttTestPac.Rows(0), "tp_")

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Case "M"
            strSQL = "UPDATE TESTPAC SET " & GetQueryUpdate(dttTestPac, dttTestPac.Rows(0), "tp_")
            strWhere = " WHERE codditt = " & CStrSQL(!codditt) & " AND tp_anno = " & NTSCInt(!tp_anno) & _
                       " AND tp_serie = " & CStrSQL(!tp_serie) & " AND tp_numdoc = " & NTSCInt(!tp_numdoc)
            strSQL &= strWhere

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Case "D"
            'Cancello sia da TESTPAC che da MOVPAC
            strSQL = "DELETE FROM TESTPAC "
            strWhere = "WHERE codditt = " & CStrSQL(!codditt) & " AND tp_anno = " & NTSCInt(!tp_anno) & _
                       " AND tp_serie = " & CStrSQL(!tp_serie) & " AND tp_numdoc = " & NTSCInt(!tp_numdoc)
            strSQL &= strWhere

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)


            strSQL = "DELETE FROM MOVPAC "
            strWhere = "WHERE codditt = " & CStrSQL(!codditt) & " AND mp_anno = " & NTSCInt(!tp_anno) & _
                       " AND mp_serie = " & CStrSQL(!tp_serie) & " AND mp_numdoc = " & NTSCInt(!tp_numdoc)
            strSQL &= strWhere

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Case Else : Return False
        End Select
      End With

      ' dttTestPac.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiude la transazione
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaPac(ByVal strDitta As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNum As Integer, _
                                        ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Try
      If strSerie = "" Then strSerie = " "

      strSQL = "SELECT movpac.*, alo_lottox as xx_lottox FROM MOVPAC " & _
              " LEFT JOIN analotti ON movpac.codditt = analotti.codditt AND movpac.mp_codart = analotti.alo_codart AND movpac.mp_lotto = analotti.alo_lotto " & _
               " WHERE mp_anno = " & lAnno & " AND mp_serie = " & CStrSQL(strSerie) & " AND mp_numdoc = " & lNum & _
               " AND MOVPAC.codditt = " & CStrSQL(strDitta) & " ORDER BY mp_riga"

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVPAC")

      dsOut.Tables("MOVPAC").Columns.Add("xx_selez")
      For i = 0 To dsOut.Tables("MOVPAC").Rows.Count - 1
        dsOut.Tables("MOVPAC").Rows(i)!xx_selez = "N"
      Next

      strSQL = "SELECT * FROM TESTPAC WHERE tp_anno = " & lAnno & " AND tp_serie = " & CStrSQL(strSerie) & " AND tp_numdoc = " & lNum & _
               " AND codditt = " & CStrSQL(strDitta) & ""

      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTPAC", dsOut)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiUltAgg(ByVal strDitta As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNum As Integer, _
                                          ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tp_ultagg FROM testpac WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tp_anno = " & lAnno & " AND tp_serie = " & CStrSQL(strSerie) & " AND tp_numdoc = " & lNum

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiBarcode(ByVal strDitta As String, ByVal strArtico As String, ByVal strBarcode As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT bc_code FROM barcode WHERE codditt = " & CStrSQL(strDitta) & _
               " AND bc_codart = " & CStrSQL(strArtico) & " AND bc_code = " & CStrSQL(strBarcode)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetQueryStampaPDF(ByVal strDitta As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNum As Integer) As String
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT testpac.codditt, testpac.tp_anno, testpac.tp_serie, testpac.tp_numdoc," & _
               " Max(anagra.an_conto) As xx_conto, Max(anagra.an_descr1) As xx_descr1," & _
               " Max(anagra.an_usaem) As xx_usaem, Max(anagra.an_email) As xx_email," & _
               " Max(anagra.an_faxtlx) As xx_faxtlx, 0 as agente" & _
               " FROM (testpac INNER JOIN anagra ON testpac.codditt = anagra.codditt AND testpac.tp_conto = anagra.an_conto)" & _
               " INNER JOIN movpac ON testpac.codditt = movpac.codditt AND testpac.tp_anno = movpac.mp_anno AND testpac.tp_serie = movpac.mp_serie AND testpac.tp_numdoc = movpac.mp_numdoc" & _
               " WHERE movpac.codditt = " & CStrSQL(strDitta) & _
               " AND movpac.mp_anno = " & lAnno & _
               " AND movpac.mp_serie = " & CStrSQL(strSerie) & _
               " AND movpac.mp_numdoc = " & lNum & _
               " GROUP BY testpac.codditt, testpac.tp_anno, testpac.tp_serie, testpac.tp_numdoc" & _
               " ORDER BY testpac.codditt, testpac.tp_anno, testpac.tp_serie, testpac.tp_numdoc"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return ""
  End Function

  Public Overridable Function ArticoloInLingua(ByVal strDitta As String, ByVal strArt As String, ByVal nTabling As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ax_descr, ax_desint, ax_note FROM ARTVAL" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ax_codart = " & CStrSQL(strArt) & _
               " AND ax_codvalu = " & nTabling

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "Sepa"
  Public Overridable Function EsistePacking(ByVal strDitta As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumero As Integer, _
                                              ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tp_anno, tp_serie, tp_numdoc FROM testpac" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tp_anno = " & lAnno & _
               " AND tp_serie = " & CStrSQL(strSerie) & " AND tp_numdoc = " & lNumero & _
               " ORDER BY tp_anno, tp_serie, tp_numdoc"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Pror"
  Public Overridable Function EsisteOrdine(ByVal strDitta As String, ByVal strTipo As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                           ByVal lNumero As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT td_tipork FROM testord" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND td_tipork = " & CStrSQL(strTipo) & _
               " AND td_anno = " & lAnno & _
               " AND td_serie = " & CStrSQL(strSerie) & _
               " AND td_numord = " & lNumero

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Seba"
  Public Overridable Function EsisteTestmag(ByVal strDitta As String, ByVal strTipo As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                            ByVal lNumero As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Try
      Select Case strTipo
        Case "A", "D", "E", "N"
          strWhere = " AND (tm_tipork = " & CStrSQL("A") & "  OR tm_tipork = " & CStrSQL("D") & "  OR tm_tipork = " & CStrSQL("E") & "  OR tm_tipork = " & CStrSQL("N") & ")"
        Case "B"
          strWhere = " AND tm_tipork = " & CStrSQL("B")
        Case "C"
          strWhere = " AND tm_tipork = " & CStrSQL("C")
        Case "M"
          strWhere = " AND tm_tipork = " & CStrSQL("M")
        Case "Z"
          strWhere = " AND tm_tipork = " & CStrSQL("Z")
        Case "L", "K", "J"
          strWhere = " AND (tm_tipork = " & CStrSQL("L") & "  OR tm_tipork = " & CStrSQL("K") & "  OR tm_tipork = " & CStrSQL("J") & ")"
        Case "T"
          strWhere = " AND tm_tipork = " & CStrSQL("T")
        Case "U"
          strWhere = " AND tm_tipork = " & CStrSQL("U")
        Case "W"
          strWhere = " AND tm_tipork = " & CStrSQL("W")
        Case "F", "I"
          strWhere = " AND (tm_tipork = " & CStrSQL("I") & "  OR tm_tipork = " & CStrSQL("F") & ")"
        Case "S", "P"
          strWhere = " AND (tm_tipork = " & CStrSQL("S") & "  OR tm_tipork = " & CStrSQL("P") & ")"
      End Select

      If strTipo = "W" Then
        strSQL = "SELECT tm_tipork FROM testprb WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND tm_numdoc = " & lNumero & " AND tm_anno = " & lAnno & _
                 " AND tm_serie = " & CStrSQL(strSerie) & strWhere
      Else
        strSQL = "SELECT tm_tipork FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND tm_numdoc = " & lNumero & " AND tm_anno = " & lAnno & _
                 " AND tm_serie = " & CStrSQL(strSerie) & strWhere
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Sed1"
  Public Overridable Function ZoomSeor(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                           ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                           ByVal strDataA As String, ByVal strDatConsDa As String, ByVal strDatConsA As String, _
                           ByVal strEvaso As String, ByVal strRiferim As String, ByVal strDitta As String) As DataSet
    Dim dsTmp As DataSet = New DataSet
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT td_tipork, td_anno, td_serie, td_numord, td_datord, td_conto, an_descr1 as xx_conto, " & _
               "td_datcons, td_totdoc, td_flevas, td_rilasciato, td_confermato, td_coddest, " & _
               "dd_nomdest as xx_destin, td_commeca, co_descr1 as xx_commeca " & _
               "FROM ((testord INNER JOIN anagra ON testord.codditt = anagra.codditt " & _
               "AND testord.td_conto = anagra.an_conto) " & _
               "LEFT JOIN destdiv ON testord.codditt = destdiv.codditt " & _
               "AND testord.td_conto = destdiv.dd_conto " & _
               "AND testord.td_coddest = destdiv.dd_coddest) " & _
               "LEFT JOIN commess ON testord.codditt = commess.codditt " & _
               "AND testord.td_commeca = commess.co_comme " & _
               "WHERE td_tipork = '" & strTipork & "' AND td_anno = " & nAnno & _
               " AND testord.codditt = " & CStrSQL(strDitta)
      If strSerie <> "" Then strSQL += " AND td_serie = '" & strSerie & "'"
      If lContoDa <> 0 Or lContoA <> 999999999 Then strSQL += " AND td_conto BETWEEN " & lContoDa.ToString & " AND " & lContoA.ToString
      If NTSCDate(strDataDa) <> NTSCDate(IntSetDate("01/01/1900")) Or NTSCDate(strDataA) <> NTSCDate(IntSetDate("31/12/2099")) Then
        strSQL += " AND td_datord BETWEEN " & CDataSQL(strDataDa) & " AND " & CDataSQL(strDataA)
      End If
      If NTSCDate(strDatConsDa) <> NTSCDate(IntSetDate("01/01/1900")) Or NTSCDate(strDatConsA) <> NTSCDate(IntSetDate("31/12/2099")) Then
        strSQL += " AND td_datcons BETWEEN " & CDataSQL(strDatConsDa) & " AND " & CDataSQL(strDatConsA)
      End If
      If strEvaso <> "" Then strSQL += " AND td_flevas = '" & strEvaso & "'"
      If strRiferim <> "" Then
        strRiferim = "%" & strRiferim.Replace("*", "%") & "%"
        strSQL += " AND td_riferim like " & CStrSQL(strRiferim.Replace("%%", "%"))
      End If

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SEOR")

      Return dsTmp
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function
#End Region

#Region "Sep1"
  Public Overridable Function ZoomSepa(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                       ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                                       ByVal strDataA As String, ByVal strEvaso As String, ByVal strRiferim As String, _
                                       ByVal strDitta As String) As DataSet
    Dim dsTmp As DataSet = New DataSet
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tp_anno, tp_serie, tp_numdoc, tp_datdoc, tp_conto, an_descr1 as xx_conto, " & _
               "tp_totcoll, tp_peso, tp_pesonetto, tp_note " & _
               "FROM (testpac INNER JOIN anagra ON testpac.codditt = anagra.codditt AND testpac.tp_conto = anagra.an_conto) " & _
               "WHERE tp_anno = " & nAnno & " AND testpac.codditt = " & CStrSQL(strDitta)
      If strSerie <> "" Then strSQL += " AND tp_serie = " & CStrSQL(strSerie)
      If lContoDa <> 0 Or lContoA <> 999999999 Then strSQL += " AND tp_conto BETWEEN " & lContoDa.ToString & " AND " & lContoA.ToString
      If NTSCDate(strDataDa) <> NTSCDate(IntSetDate("01/01/1900")) Or NTSCDate(strDataA) <> NTSCDate(IntSetDate("31/12/2099")) Then
        strSQL += " AND tp_datdoc BETWEEN " & CDataSQL(strDataDa) & " AND " & CDataSQL(strDataA)
      End If
      If strRiferim <> "" Then
        strRiferim = "%" & strRiferim.Replace("*", "%") & "%"
        strSQL += " AND tp_riferim like " & CStrSQL(strRiferim.Replace("%%", "%"))
      End If

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SEPA")

      Return dsTmp
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function
#End Region
End Class