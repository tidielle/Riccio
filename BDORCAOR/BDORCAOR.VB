Imports System.IO
Imports NTSInformatica.CLN__STD
Imports NTSInformatica
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class CLDORCAOR
  Inherits CLD__BASE

  Public Overridable Function SelezionaOrdini(ByVal strDitta As String, ByVal bFat As Boolean, ByVal strWhere As String, _
                                              ByVal strSerie As String, ByVal strDatord As String, ByVal ldanumord As Integer, _
                                              ByVal lAnumord As Integer, ByVal lanno As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      If bFat Then
        strSQL = "SELECT testord.* FROM testord INNER JOIN ((testmag INNER JOIN movmag ON " & _
                 "testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork " & _
                 "AND testmag.tm_serie = movmag.mm_serie AND " & _
                 "testmag.tm_anno = movmag.mm_anno AND testmag.tm_numdoc = movmag.mm_numdoc) " & _
                 "INNER JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND " & _
                 "testmag.tm_tipobf = tabtpbf.tb_codtpbf) ON " & _
                 "testord.codditt = movmag.codditt AND testord.td_numord = movmag.mm_ornum " & _
                 "AND testord.td_serie = movmag.mm_orserie AND " & _
                 "testord.td_anno = movmag.mm_oranno AND testord.td_tipork = movmag.mm_ortipo " & _
                 "WHERE testord.codditt = " & CStrSQL(strDitta) & _
                 " AND ((testmag.tm_flfatt = " & CStrSQL("S") & " AND tabtpbf.tb_flresocl <> " & CStrSQL("N") & _
                 ") OR (testmag.tm_flfatt <> " & CStrSQL("S") & " AND tabtpbf.tb_flresocl = " & CStrSQL("N") & _
                 ")) AND testord.td_flevas = " & CStrSQL("S") & " AND testord.td_datord <= " & _
                 CDataSQL(strDatord) & " AND " & strWhere
      Else
        strSQL = "SELECT * FROM testord WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND testord.td_flevas = " & CStrSQL("S") & " AND testord.td_datord <= " & _
                 CDataSQL(strDatord) & " AND " & strWhere
      End If ' If bFat Then

      ' Aggiungo ulteriori filtri alla query
      If strSerie <> "" Then strSQL &= " AND td_serie = " & CStrSQL(strSerie)
      If ldanumord <> 0 Or lAnumord <> 999999999 Then strSQL &= " AND td_numord BETWEEN " & ldanumord & " AND " & lAnumord
      If lanno <> 0 Then strSQL &= " AND td_anno = " & lanno

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTORD")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function SelezionaPreventivi(ByVal strDitta As String, ByVal strDatord As String, ByVal strSerie As String, _
                                                  ByVal lDanumord As Integer, ByVal lAnumord As Integer, ByVal lanno As Integer, _
                                                  ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM testord WHERE codditt = " & CStrSQL(strDitta) & _
                       " AND testord.td_datord <= " & CDataSQL(strDatord) & _
                       " AND td_tipork = " & CStrSQL("Q")

      If strSerie <> "" Then strSQL &= " AND td_serie = " & CStrSQL(strSerie)
      If lDanumord > 0 Or lAnumord < 999999999 Then strSQL &= " AND td_numord BETWEEN " & lDanumord & " AND " & lAnumord
      If lAnno <> 0 Then strSQL &= " AND td_anno = " & lAnno


      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTORD")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function SelezionaDatiPrev(ByVal strDitta As String, ByVal strTipoRk As String, ByVal dsTmp As DataSet, _
                                                ByVal i As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      With dsTmp.Tables("TESTORD").Rows(i)
        strSQL = "SELECT tm_tipork FROM ((testmag INNER JOIN movmag ON " & _
                         "testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork " & _
                         " AND testmag.tm_serie = movmag.mm_serie AND " & _
                         "testmag.tm_anno = movmag.mm_anno AND testmag.tm_numdoc = movmag.mm_numdoc) " & _
                         "INNER JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf)" & _
                         " WHERE tabtpbf.codditt = " & CStrSQL(strDitta) & _
                         " AND tm_flfatt <> " & CStrSQL("S") & " AND tabtpbf.tb_flresocl <> " & CStrSQL("N") & " AND " & _
                         "mm_oranno = " & NTSCInt(!td_anno) & " AND mm_ortipo = " & CStrSQL(strTipoRk) & " AND " & _
                         "mm_orserie = " & CStrSQL(!td_serie) & " AND mm_ornum = " & NTSCInt(!td_numord)
      End With
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTMAG")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function SelezionaDatiHY(ByVal strDitta As String, ByVal dsTmp As DataSet, ByVal i As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      With dsTmp.Tables("TESTORD").Rows(i)
        strSQL = "SELECT * FROM testord " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND td_tipork = " & CStrSQL("H") & _
                 " AND td_anno = " & NTSCInt(!td_anno) & _
                 " AND td_serie = " & CStrSQL(!td_serie) & _
                 " AND td_numord = " & NTSCInt(!td_numord)
      End With

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTORD")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function ElabNoPreventivo(ByVal strDitta As String, ByRef dsTest As DataSet, ByVal i As Integer, _
                                           ByVal strTipoRk As String, ByVal bModuloTCO As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      With dsTest.Tables("TESTORD").Rows(i)
        '-----------------------------------------------------------------
        'Aggiornamento delle righe di MOVMAG associate a quelle di MOVORD
        strSQL = "UPDATE movmag SET mm_ortipo = " & CStrSQL(" ") & ", mm_oranno = 0, " & _
                   "mm_orserie = " & CStrSQL(" ") & ", mm_ornum = 0, mm_orriga = 0, " & _
                   "mm_qtadisimp = 0, mm_coldisimp = 0, mm_valdisimp = 0, mm_salcon = " & CStrSQL("C") & ", mm_valdisimpv = 0 " & _
                   "WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND mm_ortipo = " & CStrSQL(strTipoRk) & _
                   " AND mm_oranno = " & NTSCInt(!td_anno) & _
                   " AND mm_orserie = " & CStrSQL(!td_serie) & _
                   " AND mm_ornum = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Aggiornamento delle righe di MOVMAG PROFORMA associate a quelle di MOVORD
        strSQL = "UPDATE movmag SET mm_ortipo = " & CStrSQL(" ") & ", mm_oranno = 0, " & _
                 "mm_orserie = " & CStrSQL(" ") & ", mm_commen = 0, mm_orriga = 0, " & _
                 "mm_qtadisimp = 0, mm_coldisimp = 0, mm_valdisimp = 0, mm_salcon = " & CStrSQL("C") & ", mm_valdisimpv = 0 " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND mm_tipork = " & CStrSQL("A") & " AND mm_ortipo = " & CStrSQL(strTipoRk) & _
                 " AND mm_oranno = " & NTSCInt(!td_anno) & _
                 " AND mm_orserie = " & NTSCInt(!td_serie) & _
                 " AND mm_ornum = 0 " & _
                 " AND mm_commen = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Aggiornamento delle righe di MOVPRB associate a quelle di MOVORD
        strSQL = "UPDATE movprb SET mm_ortipo = " & CStrSQL(" ") & ", mm_oranno = 0, " & _
                 "mm_orserie = " & CStrSQL(" ") & ", mm_ornum = 0, mm_orriga = 0, " & _
                 "mm_qtadisimp = 0, mm_coldisimp = 0, mm_valdisimp = 0, mm_salcon = " & CStrSQL("C") & ", mm_valdisimpv = 0 " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND mm_ortipo = " & CStrSQL(strTipoRk) & _
                 " AND mm_oranno = " & NTSCInt(!td_anno) & _
                 " AND mm_orserie = " & CStrSQL(!td_serie) & _
                 " AND mm_ornum = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di ASSRIS associate a quelle di MOVORD
        strSQL = "DELETE FROM assris " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND as_tipork = " & CStrSQL(strTipoRk) & _
                 " AND as_anno = " & NTSCInt(!td_anno) & _
                 " AND as_serie = " & CStrSQL(!td_serie) & _
                 " AND as_numord = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di ATTIVIT associate a quelle di MOVORD
        strSQL = "DELETE FROM attivit " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND at_tipork = " & CStrSQL(strTipoRk) & _
                 "AND at_anno = " & NTSCInt(!td_anno) & _
                 "AND at_serie = " & CStrSQL(!td_serie) & _
                 "AND at_numord = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Aggiornamento delle righe di LAVCENT associate a quelle di MOVORD
        strSQL = "UPDATE lavcent SET lce_ortipo = " & CStrSQL(" ") & ", lce_oranno = 0, " & _
                 "lce_orserie = " & CStrSQL(" ") & ", lce_ornum = 0, lce_orriga = 0, " & _
                 "lce_orfase = 0 WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND lce_ortipo = " & CStrSQL(strTipoRk) & _
                 " AND lce_oranno = " & NTSCInt(!td_anno) & _
                 " AND lce_orserie = " & CStrSQL(!td_serie) & _
                 " AND lce_ornum = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di REINCPAG
        strSQL = "DELETE FROM reincpag " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND mmp_tipork = " & CStrSQL(strTipoRk) & _
                 " AND mmp_anno = " & NTSCInt(!td_anno) & _
                 " AND mmp_serie = " & CStrSQL(!td_serie) & _
                 " AND mmp_numdoc = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di KEYORD
        strSQL = "DELETE FROM keyord " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND ko_tipork = " & CStrSQL(strTipoRk) & _
                 " AND ko_anno = " & NTSCInt(!td_anno) & _
                 " AND ko_serie = " & CStrSQL(!td_serie) & _
                 " AND ko_numord = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di MOVORDS
        strSQL = "DELETE FROM movords " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND mos_tipork = " & CStrSQL(strTipoRk) & _
                 " AND mos_anno = " & NTSCInt(!td_anno) & _
                 " AND mos_serie = " & CStrSQL(!td_serie) & _
                 " AND mos_numdoc = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di MOVORDTC
        If bModuloTCO Then
          strSQL = "DELETE FROM movordtc" & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND mo_tipork = " & CStrSQL(strTipoRk) & _
                   " AND mo_anno = " & NTSCInt(!td_anno) & _
                   " AND mo_serie = " & CStrSQL(!td_serie) & _
                   " AND mo_numord = " & NTSCInt(!td_numord)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If

        '------------------------------------------------------------------
        'Eliminazione delle righe di MOVORD
        strSQL = "DELETE FROM movord " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND mo_tipork = " & CStrSQL(strTipoRk) & _
                 " AND mo_anno = " & NTSCInt(!td_anno) & _
                 " AND mo_serie = " & CStrSQL(!td_serie) & _
                 " AND mo_numord = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di TESTORDS
        strSQL = "DELETE FROM testords " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND tds_tipork = " & CStrSQL(strTipoRk) & _
                 " AND tds_anno = " & NTSCInt(!td_anno) & _
                 " AND tds_serie = " & CStrSQL(!td_serie) & _
                 " AND tds_numdoc = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di TESTORD
        strSQL = "DELETE FROM testord " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND td_tipork = " & CStrSQL(strTipoRk) & _
                 " AND td_anno = " & NTSCInt(!td_anno) & _
                 " AND td_serie = " & CStrSQL(!td_serie) & _
                 " AND td_numord = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End With
      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function ElabPreventivo(ByVal strDitta As String, ByRef dsTest As DataSet, ByVal i As Integer, _
                                             ByVal strTipoRk As String, ByVal bModuloTCO As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      With dsTest.Tables("TESTORD").Rows(i)
        '------------------------------------------------------------------
        'Eliminazione delle righe di REINCPAG
        strSQL = "DELETE FROM reincpag " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND mmp_tipork = " & CStrSQL(!td_tipork) & _
                 " AND mmp_anno = " & NTSCInt(!td_anno) & _
                 " AND mmp_serie = " & CStrSQL(!td_serie) & _
                 " AND mmp_numdoc = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di KEYORD
        strSQL = "DELETE FROM keyord " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND ko_tipork = " & CStrSQL(!td_tipork) & _
                 " AND ko_anno = " & NTSCInt(!td_anno) & _
                 " AND ko_serie = " & CStrSQL(!td_serie) & _
                 " AND ko_numord = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di MOVORDS
        strSQL = "DELETE FROM movords " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND mos_tipork = " & CStrSQL(!td_tipork) & _
                 " AND mos_anno = " & NTSCInt(!td_anno) & _
                 " AND mos_serie = " & CStrSQL(!td_serie) & _
                 " AND mos_numdoc = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di MOVORD
        If bModuloTCO Then
          strSQL = "DELETE FROM movordtc" & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND mo_tipork = " & CStrSQL(!td_tipork) & _
                   " AND mo_anno = " & NTSCInt(!td_anno) & _
                   " AND mo_serie = " & CStrSQL(!td_serie) & _
                   " AND mo_numord = " & NTSCInt(!td_numord)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If

        '------------------------------------------------------------------
        'Eliminazione delle righe di MOVORD
        strSQL = "DELETE FROM movord " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND mo_tipork = " & CStrSQL(!td_tipork) & _
                 " AND mo_anno = " & NTSCInt(!td_anno) & _
                 " AND mo_serie = " & CStrSQL(!td_serie) & _
                 " AND mo_numord = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di TESTORDS
        strSQL = "DELETE FROM testords " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND tds_tipork = " & CStrSQL(!td_tipork) & _
                 " AND tds_anno = " & NTSCInt(!td_anno) & _
                 " AND tds_serie = " & CStrSQL(!td_serie) & _
                 " AND tds_numdoc = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di TESTORD
        strSQL = "DELETE FROM testord " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND td_tipork = " & CStrSQL(!td_tipork) & _
                 " AND td_anno = " & NTSCInt(!td_anno) & _
                 " AND td_serie = " & CStrSQL(!td_serie) & _
                 " AND td_numord = " & NTSCInt(!td_numord)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End With
      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
End Class