Imports System.IO
Imports NTSInformatica.CLN__STD
Imports NTSInformatica
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class CLDMGCMMA
  Inherits CLD__BASE

  Public Overridable Function LeggiDate(ByVal strDitta As String, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tabanaz.tb_dtulap, tabanaz.tb_dtulst FROM TABANAZ WHERE codditt = " & CStrSQL(strDitta)

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function SelezionaTestmag(ByVal strDitta As String, ByVal strWhere As String, ByVal strData As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT testmag.* FROM testmag" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND testmag.tm_datdoc <= " & CDataSQL(strData) & _
                    " AND " & strWhere & " ORDER BY testmag.tm_tipork"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTMAG")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function SelezionaTestmagJArtMovm(ByVal strDitta As String, ByVal i As Integer, ByVal dsTmp As DataSet, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      With dsTmp.Tables("TESTMAG").Rows(i)
        strSQL = "SELECT movmag.mm_clnum, movmag.mm_cvnum, artico.ar_gestmatr" & _
                     " FROM testmag INNER JOIN (artico INNER JOIN movmag ON (movmag.codditt = artico.codditt) AND (artico.ar_codart = movmag.mm_codart)) ON (movmag.codditt = testmag.codditt) AND (testmag.tm_tipork = movmag.mm_tipork) AND (testmag.tm_serie = movmag.mm_serie) AND (testmag.tm_anno = movmag.mm_anno) AND (testmag.tm_numdoc = movmag.mm_numdoc)" & _
                     " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                     " AND mm_tipork = " & CStrSQL(!tm_tipork) & _
                     " AND mm_anno = " & NTSCInt(!tm_anno) & _
                     " AND mm_serie = " & CStrSQL(!tm_serie) & _
                     " AND mm_numdoc = " & NTSCInt(!tm_numdoc)
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

  Public Overridable Function SelezionaTestmag2(ByVal strDitta As String, ByVal i As Integer, ByVal dsTmp As DataSet, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      With dsTmp.Tables("TESTMAG").Rows(i)
        strSQL = "SELECT testmag.* FROM testmag" & _
                            " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND tm_tipork = " & CStrSQL("T") & _
                            " AND tm_anno = " & NTSCInt(!tm_anno) & _
                            " AND tm_serie = " & CStrSQL(!tm_serie) & _
                            " AND tm_numdoc = " & NTSCInt(!tm_numdoc)
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

  Public Overridable Function SelezionaDati(ByVal strDitta As String, ByVal strData As String, ByVal strSerie As String, _
                                            ByVal lDanumord As Integer, ByVal lAnumord As Integer, ByVal lAnno As Integer, _
                                            ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try

      strSQL = "SELECT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_datdoc, tm_idpick FROM testprb" & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND testprb.tm_datdoc <= " & CDataSQL(strData) & _
                     " AND testprb.tm_flevas = " & CStrSQL("S")
      If strSerie <> "*" Then strSQL &= " AND tm_serie = " & CStrSQL(strSerie)
      If lDanumord <> 0 Or lAnumord <> 999999999 Then strSQL &= " AND tm_numdoc BETWEEN " & lDanumord & " AND " & lAnumord
      If lAnno <> 0 Then strSQL &= " AND tm_anno = " & lAnno

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTPRB")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function EseguiNote(ByVal strDitta As String, ByVal i As Integer, ByRef dsTest As DataSet) As Boolean
    Try
      Return EseguiNote(strDitta, i, dsTest, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EseguiNote(ByVal strDitta As String, ByVal i As Integer, ByRef dsTest As DataSet, _
                                         ByVal bCancellaPicking As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dbConn As DbConnection = Nothing
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, i, dsTest, bCancellaPicking})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsTest = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      With dsTest.Tables("TESTPRB").Rows(i)
        If bCancellaPicking Then
          'Seleziona il picking associato e in caso lo elimina (se richiesto)
          If NTSCInt(!tm_idpick) > 0 Then
            'Prima le righe
            strSQL = "DELETE movpic FROM movpic " & _
                     "INNER JOIN testpic ON movpic.codditt = testpic.codditt " & _
                     "      AND mp_idpick = tp_idpick AND mp_rigapick = tp_rigapick " & _
                     "WHERE movpic.codditt = " & CStrSQL(strDitta) & _
                     " AND tp_mxtipork = " & CStrSQL(!tm_tipork) & _
                     " AND tp_mxanno = " & NTSCInt(!tm_anno) & _
                     " AND tp_mxserie = " & CStrSQL(!tm_serie) & _
                     " AND tp_mxnumdoc = " & NTSCInt(!tm_numdoc)

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            'Poi le testate
            strSQL = "DELETE FROM testpic " & _
                     "WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND tp_mxtipork = " & CStrSQL(!tm_tipork) & _
                     " AND tp_mxanno = " & NTSCInt(!tm_anno) & _
                     " AND tp_mxserie = " & CStrSQL(!tm_serie) & _
                     " AND tp_mxnumdoc = " & NTSCInt(!tm_numdoc)

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            'Infine se non ci sono altre testate collegate allo stesso picking, cancella anche tabpick
            strSQL = "DELETE FROM tabpick " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND tb_codpick = " & NTSCInt(!tm_idpick) & _
                     " AND 0 = (SELECT COUNT(codditt) FROM testpic " & _
                     "         WHERE tp_idpick = tb_codpick AND testpic.codditt = tabpick.codditt)"

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        End If

        '------------------------------------------------------------------
        'Aggiornamento delle righe di MOVMAG associate a quelle di MOVORD
        strSQL = "UPDATE movmag SET mm_nptipo = " & CStrSQL(" ") & ", mm_npanno = 0, " & _
                 "mm_npserie = " & CStrSQL(" ") & ", mm_npnum = 0, mm_npriga = 0, " & _
                 "mm_npqtadis = 0, mm_npcoldis = 0, mm_npvaldis = 0, mm_npsalcon = " & CStrSQL("C") & ", " & _
                 "mm_npvaldisv = 0" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND mm_nptipo = " & CStrSQL(!tm_tipork) & _
                 " AND mm_npanno = " & NTSCInt(!tm_anno) & _
                 " AND mm_npserie = " & CStrSQL(!tm_serie) & _
                 " AND mm_npnum = " & NTSCInt(!tm_numdoc)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'in MOVORD tolgo quanto prenotato con la nota da cancellare
        strSQL = "Update movord SET mo_flevapre = " & CStrSQL("C") & " From movprb " & _
                 " Where (movprb.codditt = movord.codditt) " & _
                 " AND (movprb.mm_ortipo = movord.mo_tipork) " & _
                 " AND (movprb.mm_oranno = movord.mo_anno) " & _
                 " AND (movprb.mm_orserie = movord.mo_serie) " & _
                 " AND (movprb.mm_ornum = movord.mo_numord) " & _
                 " AND (movprb.mm_orriga = movord.mo_riga) " & _
                 " AND movord.codditt = " & CStrSQL(strDitta) & _
                 " AND mm_tipork = " & CStrSQL(!tm_tipork) & _
                 " AND mm_anno = " & NTSCInt(!tm_anno) & _
                 " AND mm_serie = " & CStrSQL(!tm_serie) & _
                 " AND mm_numdoc = " & NTSCInt(!tm_numdoc) & _
                 " AND mm_ornum > 0"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "Update movord SET mo_quapre = mo_quapre - (SELECT Sum(tab3.mm_quant - tab3.mm_nprquaeva) FROM movprb tab3 " & _
                 " Where tab3.codditt = tab2.codditt " & _
                 " AND tab3.mm_tipork = tab2.mm_tipork " & _
                 " AND tab3.mm_anno = tab2.mm_anno " & _
                 " AND tab3.mm_serie = tab2.mm_serie " & _
                 " AND tab3.mm_numdoc = tab2.mm_numdoc " & _
                 " AND tab3.mm_ortipo = tab2.mm_ortipo " & _
                 " AND tab3.mm_oranno = tab2.mm_oranno " & _
                 " AND tab3.mm_orserie = tab2.mm_orserie " & _
                 " AND tab3.mm_ornum = tab2.mm_ornum " & _
                 " AND tab3.mm_orriga = tab2.mm_orriga " & _
                 " AND tab3.mm_quant >= tab3.mm_nprquaeva) " & _
                 " FROM movord tab1, movprb tab2 " & _
                 " Where (tab1.codditt = tab2.codditt) " & _
                 " AND (tab1.mo_tipork = tab2.mm_ortipo) " & _
                 " AND (tab1.mo_anno = tab2.mm_oranno) " & _
                 " AND (tab1.mo_serie = tab2.mm_orserie) " & _
                 " AND (tab1.mo_numord = tab2.mm_ornum) " & _
                 " AND (tab1.mo_riga = tab2.mm_orriga) " & _
                 " AND tab2.codditt = " & CStrSQL(strDitta) & _
                 " AND tab2.mm_tipork = " & CStrSQL(!tm_tipork) & _
                 " AND tab2.mm_anno = " & NTSCInt(!tm_anno) & _
                 " AND tab2.mm_serie = " & CStrSQL(!tm_serie) & _
                 " AND tab2.mm_numdoc = " & NTSCInt(!tm_numdoc) & _
                 " AND tab2.mm_quant >= tab2.mm_nprquaeva  AND tab2.mm_ornum > 0"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "Update movord SET mo_colpre = mo_colpre - (SELECT Sum(tab3.mm_colli - tab3.mm_nprcoleva) FROM movprb tab3 " & _
                 " Where tab3.codditt = tab2.codditt " & _
                 " AND tab3.mm_tipork = tab2.mm_tipork " & _
                 " AND tab3.mm_anno = tab2.mm_anno " & _
                 " AND tab3.mm_serie = tab2.mm_serie " & _
                 " AND tab3.mm_numdoc = tab2.mm_numdoc " & _
                 " AND tab3.mm_ortipo = tab2.mm_ortipo " & _
                 " AND tab3.mm_oranno = tab2.mm_oranno " & _
                 " AND tab3.mm_orserie = tab2.mm_orserie " & _
                 " AND tab3.mm_ornum = tab2.mm_ornum " & _
                 " AND tab3.mm_orriga = tab2.mm_orriga " & _
                 " AND tab3.mm_quant >= tab3.mm_nprquaeva) " & _
                 " FROM movord tab1, movprb tab2 " & _
                 " Where (tab1.codditt = tab2.codditt) " & _
                 " AND (tab1.mo_tipork = tab2.mm_ortipo) " & _
                 " AND (tab1.mo_anno = tab2.mm_oranno) " & _
                 " AND (tab1.mo_serie = tab2.mm_orserie) " & _
                 " AND (tab1.mo_numord = tab2.mm_ornum) " & _
                 " AND (tab1.mo_riga = tab2.mm_orriga) " & _
                 " AND tab2.codditt = " & CStrSQL(strDitta) & _
                 " AND tab2.mm_tipork = " & CStrSQL(!tm_tipork) & _
                 " AND tab2.mm_anno = " & NTSCInt(!tm_anno) & _
                 " AND tab2.mm_serie = " & CStrSQL(!tm_serie) & _
                 " AND tab2.mm_numdoc = " & NTSCInt(!tm_numdoc) & _
                 " AND tab2.mm_colli >= tab2.mm_nprcoleva AND tab2.mm_ornum > 0"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di KEYPRB
        strSQL = "DELETE FROM keyprb" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND km_tipork = " & CStrSQL(!tm_tipork) & _
                 " AND km_anno = " & NTSCInt(!tm_anno) & _
                 " AND km_serie = " & CStrSQL(!tm_serie) & _
                 " AND km_numdoc = " & NTSCInt(!tm_numdoc)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di MOVPRB
        strSQL = "DELETE FROM movprb" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND mm_tipork = " & CStrSQL(!tm_tipork) & _
                 " AND mm_anno = " & NTSCInt(!tm_anno) & _
                 " AND mm_serie = " & CStrSQL(!tm_serie) & _
                 " AND mm_numdoc = " & NTSCInt(!tm_numdoc)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di TESTPRB
        strSQL = "DELETE FROM testprb" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND tm_tipork = " & CStrSQL(!tm_tipork) & _
                 " AND tm_anno = " & NTSCInt(!tm_anno) & _
                 " AND tm_serie = " & CStrSQL(!tm_serie) & _
                 " AND tm_numdoc = " & NTSCInt(!tm_numdoc)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '------------------------------------------------------------------
        'Eliminazione delle righe di TESTPRBC
        strSQL = "DELETE FROM testprbc" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND tm_tipork = " & CStrSQL(!tm_tipork) & _
                 " AND tm_anno = " & NTSCInt(!tm_anno) & _
                 " AND tm_serie = " & CStrSQL(!tm_serie) & _
                 " AND tm_numdoc = " & NTSCInt(!tm_numdoc)
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


  Public Overridable Function EseguiDoc(ByVal strDitta As String, ByVal i As Integer, ByRef dsTest As DataSet, _
                                        ByVal bModuloTCO As Boolean) As Boolean
    Try
      Return EseguiDoc(strDitta, i, dsTest, bModuloTCO, False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EseguiDoc(ByVal strDitta As String, ByVal i As Integer, ByRef dsTest As DataSet, _
                                        ByVal bModuloTCO As Boolean, ByVal bCa2 As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strTipork As String = NTSCStr(dsTest.Tables("TESTMAG").Rows(i)!tm_tipork)
    Dim dbConn As DbConnection = Nothing
    Dim dttTmp As New DataTable
    Dim lIITtkeys As Integer = 0
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, i, dsTest, bModuloTCO, bCa2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsTest = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      lIITtkeys = GetTblInstId("TTKEYS", False)

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      With dsTest.Tables("TESTMAG").Rows(i)

        '------------------------------------------------------------------
        If bCa2 Then
          'quando cancello le fatture devo ricreare le righe di priana e pria2comp se la fattura non era stata contabilizzata
          If NTSCInt(!tm_numregef) <> 0 And (!tm_tipork.ToString = "D" Or !tm_tipork.ToString = "M" Or _
             !tm_tipork.ToString = "(" Or !tm_tipork.ToString = "£") Then

            strSQL = "SELECT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_datdoc " & _
                     " FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND tm_tiporkfat = " & CStrSQL(!tm_tipork) & _
                     " AND tm_annfat = " & NTSCInt(!tm_anno) & _
                     " AND tm_alffat = " & CStrSQL(!tm_serie) & _
                     " AND tm_numfat = " & NTSCInt(!tm_numdoc)
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            For Each dtrT As DataRow In dttTmp.Rows
              If Not ScriviPriana2DaTestmag(NTSCStr(strDitta), _
                                            NTSCStr(dtrT!tm_tipork.ToString), _
                                            NTSCInt(dtrT!tm_anno), _
                                            NTSCStr(dtrT!tm_serie.ToString), _
                                            NTSCInt(dtrT!tm_numdoc), _
                                            NTSCStr(dtrT!tm_datdoc), _
                                            dbConn, lIITtkeys, False) Then Return False
            Next
            dttTmp.Clear()
          End If

          '---------------------------------
          'cancello priana2 e pria2comp collegato ai documenti di magazzino B, M, Z, T
          If !tm_tipork.ToString = "B" Or !tm_tipork.ToString = "M" Or _
             !tm_tipork.ToString = "Z" Or !tm_tipork.ToString = "T" Then
            strSQL = "DELETE FROM pria2comp WHERE codditt = " & CStrSQL(!codditt) & _
                     " AND pac_progr IN (SELECT pa2_progr FROM priana2 WHERE " & _
                     " codditt = " & CStrSQL(!codditt) & _
                     " AND pa2_numreg = 0 " & _
                     " AND (pa2_mmtipork = " & CStrSQL(!tm_tipork)
            If !tm_tipork.ToString = "T" Then strSQL += " OR pa2_mmtipork = 'U' "
            strSQL += ") AND pa2_mmanno = " & !tm_anno.ToString & _
                      " AND pa2_mmserie = " & CStrSQL(!tm_serie) & _
                      " AND pa2_mmnumdoc = " & !tm_numdoc.ToString & ")"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "DELETE FROM priana2 WHERE " & _
                     " codditt = " & CStrSQL(!codditt) & _
                     " AND pa2_numreg = 0 " & _
                     " AND (pa2_mmtipork = " & CStrSQL(!tm_tipork)
            If !tm_tipork.ToString = "T" Then strSQL += " OR pa2_mmtipork = 'U' "
            strSQL += ") AND pa2_mmanno = " & !tm_anno.ToString & _
                     " AND pa2_mmserie = " & CStrSQL(!tm_serie) & _
                     " AND pa2_mmnumdoc = " & !tm_numdoc.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If    'If bCa2 Then
        End If    'If bCa2 Then
        '------------------------------------------------------------------------------------------------------------
        '--- Se si tratta di Carico da Produzione, alla fine, elimina anche gli Scarichi a Produzione
        '------------------------------------------------------------------------------------------------------------
        For nGiro As Integer = 1 To 2
          '----------------------------------------------------------------------------------------------------------
          If nGiro = 2 Then strTipork = "U"
          '----------------------------------------------------------------------------------------------------------
          'Eliminazione delle righe di LAVCENT associate a quelle di movmag
          strSQL = "DELETE FROM LAVCENT" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND lce_tipork = " & CStrSQL(strTipork) & _
            " AND lce_anno = " & NTSCInt(!tm_anno) & _
            " AND lce_serie = " & CStrSQL(!tm_serie) & _
            " AND lce_numdoc = " & NTSCInt(!tm_numdoc)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '------------------------------------------------------------------
          'Eliminazione delle righe di REINCPAG
          strSQL = "DELETE FROM reincpag" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND mmp_tipork = " & CStrSQL(strTipork) & _
            " AND mmp_anno = " & NTSCInt(!tm_anno) & _
            " AND mmp_serie = " & CStrSQL(!tm_serie) & _
            " AND mmp_numdoc = " & NTSCInt(!tm_numdoc)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '------------------------------------------------------------------
          'Eliminazione delle righe di KEYMAG
          strSQL = "DELETE FROM keymag" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND km_tipork = " & CStrSQL(strTipork) & _
            " AND km_anno = " & NTSCInt(!tm_anno) & _
            " AND km_serie = " & CStrSQL(!tm_serie) & _
            " AND km_numdoc = " & NTSCInt(!tm_numdoc)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '------------------------------------------------------------------
          'Eliminazione delle righe di MOVMAGS
          strSQL = "DELETE FROM movmags" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND mms_tipork = " & CStrSQL(strTipork) & _
            " AND mms_anno = " & NTSCInt(!tm_anno) & _
            " AND mms_serie = " & CStrSQL(!tm_serie) & _
            " AND mms_numdoc = " & NTSCInt(!tm_numdoc)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '------------------------------------------------------------------
          'Eliminazione delle righe di MOVMAGTC
          If bModuloTCO = True Then
            strSQL = "DELETE FROM movmagtc" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mm_tipork = " & CStrSQL(strTipork) & _
              " AND mm_anno = " & NTSCInt(!tm_anno) & _
              " AND mm_serie = " & CStrSQL(!tm_serie) & _
              " AND mm_numdoc = " & NTSCInt(!tm_numdoc)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If

          '------------------------------------------------------------------
          'Eliminazione delle righe di MOVMAG
          strSQL = "DELETE FROM movmag" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND mm_tipork = " & CStrSQL(strTipork) & _
            " AND mm_anno = " & NTSCInt(!tm_anno) & _
            " AND mm_serie = " & CStrSQL(!tm_serie) & _
            " AND mm_numdoc = " & NTSCInt(!tm_numdoc)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '------------------------------------------------------------------
          'Eliminazione delle righe di MOVCONA
          strSQL = "DELETE FROM movcona" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND mma_tipork = " & CStrSQL(strTipork) & _
            " AND mma_anno = " & NTSCInt(!tm_anno) & _
            " AND mma_serie = " & CStrSQL(!tm_serie) & _
            " AND mma_numdoc = " & NTSCInt(!tm_numdoc)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '------------------------------------------------------------------
          'Eliminazione delle righe di TESTMAGS
          strSQL = "DELETE FROM testmags" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND tms_tipork = " & CStrSQL(strTipork) & _
            " AND tms_anno = " & NTSCInt(!tm_anno) & _
            " AND tms_serie = " & CStrSQL(!tm_serie) & _
            " AND tms_numdoc = " & NTSCInt(!tm_numdoc)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '------------------------------------------------------------------
          'Eliminazione delle righe di TESTMAG
          strSQL = "DELETE FROM testmag" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND tm_tipork = " & CStrSQL(strTipork) & _
            " AND tm_anno = " & NTSCInt(!tm_anno) & _
            " AND tm_serie = " & CStrSQL(!tm_serie) & _
            " AND tm_numdoc = " & NTSCInt(!tm_numdoc)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '------------------------------------------------------------------
          'Eliminazione delle righe di TESTMAGC
          strSQL = "DELETE FROM testmagc" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND tm_tipork = " & CStrSQL(strTipork) & _
            " AND tm_anno = " & NTSCInt(!tm_anno) & _
            " AND tm_serie = " & CStrSQL(!tm_serie) & _
            " AND tm_numdoc = " & NTSCInt(!tm_numdoc)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          '----------------------------------------------------------------------------------------------------------
          If NTSCStr(!tm_tipork) <> "T" Then Exit For
          '----------------------------------------------------------------------------------------------------------
        Next nGiro
      End With
      '--------------------------------------------------------------------------------------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()


      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      Try
        ResetTblInstId("TTKEYS", False, lIITtkeys)
      Catch
      End Try
    End Try
  End Function

  Public Overridable Function AggiornaAnaz(ByVal strDitta As String, ByVal strDatrif As String) As Boolean
    Dim strSQL As String = ""
    Dim dsAnaz As DataSet
    Try
      strSQL = "SELECT tabanaz.tb_codanaz FROM tabanaz WHERE codditt = " & CStrSQL(strDitta)

      dsAnaz = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")

      If dsAnaz.Tables("TABANAZ").Rows.Count = 0 Then
        Return False
      End If

      strSQL = "UPDATE tabanaz SET tb_dtulst = " & CDataSQL(strDatrif) & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_codanaz = " & CStrSQL(dsAnaz.Tables("TABANAZ").Rows(0)!tb_codanaz)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function
End Class