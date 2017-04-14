Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

'i campi ttn_order, ttn_attrib in TTFLDONOD non sono utilizzati

Public Class CLDXXFLDO
  Inherits CLD__BASE

  'ESEMPIO BUONO: IC n. 1/2009 di PR14

  Public Overridable Function GetOfferteNodo0(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                         ByVal strTipork As String, ByVal nAnno As Integer, _
                                         ByVal strSerie As String, ByVal lNumero As Integer, _
                                         ByVal nVers As Integer, ByRef lNewProgressivo As Integer, _
                                         ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
              " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
              " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
              " ttn_order, ttn_attrib)" & _
              " SELECT testoff.codditt, " & lIITTFldonod & ", " & lNewProgressivo & ", td_tipork, td_anno," & _
              " td_serie, td_numord, td_codlead, Left(le_descr1, 40), td_datord, td_vers, 0," & _
              " 0, '', 0, 0, '', -3, 7, 0, 0" & _
              " FROM testoff INNER JOIN leads ON testoff.codditt = leads.codditt " & _
              " AND testoff.td_codlead = leads.le_codlead " & _
              " WHERE testoff.codditt = " & CStrSQL(strDitta) & _
              " AND td_tipork = " & CStrSQL(strTipork) & _
              " AND td_anno = " & nAnno.ToString & _
              " AND td_serie = " & CStrSQL(strSerie) & _
              " AND td_numord = " & lNumero.ToString & _
              " AND td_vers = " & nVers.ToString
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetOfferteDaOrd(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                              ByVal lIITTFldolin As Integer, ByRef lNewProgressivo As Integer, _
                                              ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      strSQL = "SELECT DISTINCT td_tipork, td_anno, td_serie, td_numord, td_vers, td_codlead," & _
              " td_datord, le_descr1, ttn_nodoid" & _
              " FROM ((testoff INNER JOIN movord ON testoff.codditt = movord.codditt " & _
              " AND testoff.td_tipork = movord.mo_oqtipo AND testoff.td_anno = movord.mo_oqanno " & _
              " AND testoff.td_serie = movord.mo_oqserie AND testoff.td_numord = movord.mo_oqnum " & _
              " AND testoff.td_vers = movord.mo_oqvers)" & _
              " INNER JOIN leads ON testoff.codditt = leads.codditt AND testoff.td_codlead = leads.le_codlead)" & _
              " INNER JOIN TTFLDONOD ON movord.codditt = TTFLDONOD.codditt AND movord.mo_tipork = TTFLDONOD.ttn_tipork " & _
              " AND movord.mo_anno = TTFLDONOD.ttn_anno AND movord.mo_serie = TTFLDONOD.ttn_serie " & _
              " AND movord.mo_numord = TTFLDONOD.ttn_numero " & _
              " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
              " AND ttn_tipoogg = 1"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!td_tipork), NTSCInt(dtrT!td_anno), NTSCStr(dtrT!td_serie), NTSCInt(dtrT!td_numord), NTSCInt(dtrT!td_vers), "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  CStrSQL(dtrT!td_tipork) & ", " & dtrT!td_anno.ToString & " ," & _
                  CStrSQL(dtrT!td_serie) & ", " & dtrT!td_numord.ToString & " ," & _
                  dtrT!td_codlead.ToString & ", " & CStrSQL(Left(dtrT!le_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!td_datord)) & ", " & dtrT!td_vers.ToString & _
                  ", 0, 0, '', 0, 0, '', -3, 7, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, lNodoid, NTSCInt(dtrT!ttn_nodoid), -3)
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetOrdiniNodo0(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                         ByVal strTipork As String, ByVal nAnno As Integer, _
                                         ByVal strSerie As String, ByVal lNumero As Integer, _
                                         ByRef lNewProgressivo As Integer, _
                                         ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
              " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
              " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
              " ttn_order, ttn_attrib)" & _
              " SELECT testord.codditt, " & lIITTFldonod & ", " & lNewProgressivo & ", td_tipork, td_anno," & _
              " td_serie, td_numord, td_conto, Left(an_descr1, 40), td_datord, 0, 0," & _
              " 0, '', 0, 0, '', " & NTSCInt(IIf((strTipork = "V") Or (strTipork = "$"), "-4", "-2")) & ", 1, 0, 0" & _
              " FROM testord INNER JOIN anagra ON testord.codditt = anagra.codditt " & _
              " AND testord.td_conto = anagra.an_conto " & _
              " WHERE testord.codditt = " & CStrSQL(strDitta) & _
              " AND td_tipork = " & CStrSQL(strTipork) & _
              " AND td_anno = " & nAnno.ToString & _
              " AND td_serie = " & CStrSQL(strSerie) & _
              " AND td_numord = " & lNumero.ToString
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetOrdiniDaOfferte(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                                 ByVal lIITTFldolin As Integer, ByRef lNewProgressivo As Integer, _
                                                 ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      strSQL = "SELECT DISTINCT td_tipork, td_anno, td_serie, td_numord, td_conto," & _
              " td_datord, an_descr1, ttn_nodoid" & _
              " FROM ((testord INNER JOIN movord ON testord.codditt = movord.codditt " & _
              " AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno " & _
              " AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord) " & _
              " INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto)" & _
              " INNER JOIN TTFLDONOD ON movord.codditt = TTFLDONOD.codditt AND movord.mo_oqtipo = TTFLDONOD.ttn_tipork " & _
              " AND movord.mo_oqanno = TTFLDONOD.ttn_anno AND movord.mo_oqserie = TTFLDONOD.ttn_serie " & _
              " AND movord.mo_oqnum = TTFLDONOD.ttn_numero AND movord.mo_oqvers = TTFLDONOD.ttn_numreg " & _
              " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
              " AND ttn_tipoogg = 7"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!td_tipork), NTSCInt(dtrT!td_anno), NTSCStr(dtrT!td_serie), NTSCInt(dtrT!td_numord), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  CStrSQL(dtrT!td_tipork) & ", " & dtrT!td_anno.ToString & " ," & _
                  CStrSQL(dtrT!td_serie) & ", " & dtrT!td_numord.ToString & " ," & _
                  dtrT!td_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!td_datord)) & ", 0" & _
                  ", 0, 0, '', 0, 0, '', -2, 1, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, NTSCInt(dtrT!ttn_nodoid), lNodoid, -2)
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetOrdiniDaNote(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                              ByVal lIITTFldolin As Integer, ByRef lNewProgressivo As Integer, _
                                              ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      'devo prendere solo gli ordini collegati a note di prelievo
      strSQL = "SELECT DISTINCT td_tipork, td_anno, td_serie, td_numord, td_conto," & _
              " td_datord, an_descr1, ttn_nodoid" & _
              " FROM (((testord INNER JOIN movord ON testord.codditt = movord.codditt " & _
              " AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno " & _
              " AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord)" & _
              " INNER JOIN movprb ON movord.codditt = movprb.codditt " & _
              " AND movord.mo_tipork = movprb.mm_ortipo AND movord.mo_anno = movprb.mm_oranno " & _
              " AND movord.mo_serie = movprb.mm_orserie AND movord.mo_numord = movprb.mm_ornum " & _
              " AND movord.mo_riga = movprb.mm_orriga) " & _
              " INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto)" & _
              " INNER JOIN TTFLDONOD ON movprb.codditt = TTFLDONOD.codditt AND movprb.mm_tipork = TTFLDONOD.ttn_tipork " & _
              " AND movprb.mm_anno = TTFLDONOD.ttn_anno AND movprb.mm_serie = TTFLDONOD.ttn_serie " & _
              " AND movprb.mm_numdoc = TTFLDONOD.ttn_numero " & _
              " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
              " AND ttn_tipoogg = 8"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!td_tipork), NTSCInt(dtrT!td_anno), NTSCStr(dtrT!td_serie), NTSCInt(dtrT!td_numord), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  CStrSQL(dtrT!td_tipork) & ", " & dtrT!td_anno.ToString & " ," & _
                  CStrSQL(dtrT!td_serie) & ", " & dtrT!td_numord.ToString & " ," & _
                  dtrT!td_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!td_datord)) & ", 0" & _
                  ", 0, 0, '', 0, 0, '', -2, 1, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, lNodoid, NTSCInt(dtrT!ttn_nodoid), -2)
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetOrdiniDaDDTeFattImm(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                                     ByVal lIITTFldolin As Integer, ByVal bDDT As Boolean, ByVal bFatture As Boolean, _
                                                     ByRef lNewProgressivo As Integer, ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      'devo prendere gli ordini evasi non collegati a note di prelievo
      strSQL = "SELECT DISTINCT td_tipork, td_anno, td_serie, td_numord, td_conto," & _
              " td_datord, an_descr1, ttn_nodoid" & _
              " FROM (((testord INNER JOIN movord ON testord.codditt = movord.codditt " & _
              " AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno " & _
              " AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord)" & _
              " INNER JOIN movmag ON movord.codditt = movmag.codditt " & _
              " AND movord.mo_tipork = movmag.mm_ortipo AND movord.mo_anno = movmag.mm_oranno " & _
              " AND movord.mo_serie = movmag.mm_orserie AND movord.mo_numord = movmag.mm_ornum " & _
              " AND movord.mo_riga = movmag.mm_orriga) " & _
              " INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto)" & _
              " INNER JOIN TTFLDONOD ON movmag.codditt = TTFLDONOD.codditt AND movmag.mm_tipork = TTFLDONOD.ttn_tipork " & _
              " AND movmag.mm_anno = TTFLDONOD.ttn_anno AND movmag.mm_serie = TTFLDONOD.ttn_serie " & _
              " AND movmag.mm_numdoc = TTFLDONOD.ttn_numero " & _
              " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
              " AND (ttn_tipoogg = 2 OR ttn_tipoogg = 3)" & _
              " AND movmag.mm_npnum = 0"
      If bDDT = False Or bFatture = False Then
        If bDDT Then strSQL += " AND (movmag.mm_tipork = 'B' OR movmag.mm_tipork = 'M' OR movmag.mm_tipork = 'T')"
        If bFatture Then strSQL += " AND NOT (movmag.mm_tipork = 'B' OR movmag.mm_tipork = 'M' OR movmag.mm_tipork = 'T')"
      End If
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!td_tipork), NTSCInt(dtrT!td_anno), NTSCStr(dtrT!td_serie), NTSCInt(dtrT!td_numord), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  CStrSQL(dtrT!td_tipork) & ", " & dtrT!td_anno.ToString & " ," & _
                  CStrSQL(dtrT!td_serie) & ", " & dtrT!td_numord.ToString & " ," & _
                  dtrT!td_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!td_datord)) & ", 0" & _
                  ", 0, 0, '', 0, 0, '', -2, 1, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, lNodoid, NTSCInt(dtrT!ttn_nodoid), -2)
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetOrdiniApertiDaOrdini(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
    ByVal lIITTFldolin As Integer, ByRef lNewProgressivo As Integer, ByRef nRec As Integer) As Boolean
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT td_tipork, td_anno, td_serie, td_numord, td_conto, td_datord, an_descr1, ttn_nodoid" & _
        " FROM testord INNER JOIN movord ON testord.codditt = movord.codditt" & _
        " AND testord.td_tipork = movord.mo_oatipo AND testord.td_anno = movord.mo_oaanno" & _
        " AND testord.td_serie = movord.mo_oaserie AND testord.td_numord = movord.mo_oanum" & _
        " INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto" & _
        " INNER JOIN TTFLDONOD ON movord.codditt = TTFLDONOD.codditt AND movord.mo_tipork = TTFLDONOD.ttn_tipork" & _
        " AND movord.mo_anno = TTFLDONOD.ttn_anno AND movord.mo_serie = TTFLDONOD.ttn_serie" & _
        " AND movord.mo_numord = TTFLDONOD.ttn_numero" & _
        " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTFldonod.ToString & _
        " AND ttn_tipoogg = 1"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!td_tipork), NTSCInt(dtrT!td_anno), NTSCStr(dtrT!td_serie), NTSCInt(dtrT!td_numord), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno, ttn_serie," & _
            " ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg, ttn_annpar, ttn_alfpar," & _
            " ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg, ttn_order, ttn_attrib)" & _
            " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
            CStrSQL(dtrT!td_tipork) & ", " & dtrT!td_anno.ToString & ", " & CStrSQL(dtrT!td_serie) & ", " & _
            dtrT!td_numord.ToString & ", " & dtrT!td_conto.ToString & ", " & _
            CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & CDataSQL(NTSCDate(dtrT!td_datord)) & "," & _
            " 0, 0, 0, '', 0, 0, '', -4, 1, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, lNodoid, NTSCInt(dtrT!ttn_nodoid), -3)
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetOrdiniDaOrdiniAperti(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
    ByVal lIITTFldolin As Integer, ByRef lNewProgressivo As Integer, ByRef nRec As Integer) As Boolean
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT td_tipork, td_anno, td_serie, td_numord, td_conto, td_datord, an_descr1, ttn_nodoid" & _
        " FROM testord INNER JOIN movord ON testord.codditt = movord.codditt" & _
        " AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno" & _
        " AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord" & _
        " INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto" & _
        " INNER JOIN TTFLDONOD ON movord.codditt = TTFLDONOD.codditt AND movord.mo_oatipo = TTFLDONOD.ttn_tipork" & _
        " AND movord.mo_oaanno = TTFLDONOD.ttn_anno AND movord.mo_oaserie = TTFLDONOD.ttn_serie" & _
        " AND movord.mo_oanum = TTFLDONOD.ttn_numero" & _
        " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTFldonod.ToString & _
        " AND ttn_tipoogg = 1"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!td_tipork), NTSCInt(dtrT!td_anno), NTSCStr(dtrT!td_serie), NTSCInt(dtrT!td_numord), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno, ttn_serie," & _
            " ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg, ttn_annpar, ttn_alfpar," & _
            " ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg, ttn_order, ttn_attrib)" & _
            " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
            CStrSQL(dtrT!td_tipork) & ", " & dtrT!td_anno.ToString & ", " & CStrSQL(dtrT!td_serie) & ", " & _
            dtrT!td_numord.ToString & ", " & dtrT!td_conto.ToString & ", " & _
            CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & CDataSQL(NTSCDate(dtrT!td_datord)) & "," & _
            " 0, 0, 0, '', 0, 0, '', -2, 1, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, lNodoid, NTSCInt(dtrT!ttn_nodoid), -3)
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetNoteNodo0(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                         ByVal strTipork As String, ByVal nAnno As Integer, _
                                         ByVal strSerie As String, ByVal lNumero As Integer, _
                                         ByRef lNewProgressivo As Integer, _
                                         ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
              " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
              " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
              " ttn_order, ttn_attrib)" & _
              " SELECT testprb.codditt, " & lIITTFldonod & ", " & lNewProgressivo & ", tm_tipork, tm_anno," & _
              " tm_serie, tm_numdoc, tm_conto, Left(an_descr1, 40), tm_datdoc, 0, 0," & _
              " 0, '', 0, 0, '', -1, 8, 0, 0" & _
              " FROM testprb INNER JOIN anagra ON testprb.codditt = anagra.codditt " & _
              " AND testprb.tm_conto = anagra.an_conto " & _
              " WHERE testprb.codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(strTipork) & _
              " AND tm_anno = " & nAnno.ToString & _
              " AND tm_serie = " & CStrSQL(strSerie) & _
              " AND tm_numdoc = " & lNumero.ToString
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetNoteDaDDTeFattImm(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                                   ByVal lIITTFldolin As Integer, ByVal bDDT As Boolean, ByVal bFatture As Boolean, _
                                                   ByRef lNewProgressivo As Integer, ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      'devo prendere solo gli ordini non evasi da ddt o fatture
      'visto che gli ordini evasi verranno tirati su da GetOrdiniDaDDT e/o GetOrdiniDaFatture 
      strSQL = "SELECT DISTINCT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_conto," & _
              " tm_datdoc, an_descr1, ttn_nodoid" & _
              " FROM (((testprb INNER JOIN movprb ON testprb.codditt = movprb.codditt " & _
              " AND testprb.tm_tipork = movprb.mm_tipork AND testprb.tm_anno = movprb.mm_anno " & _
              " AND testprb.tm_serie = movprb.mm_serie AND testprb.tm_numdoc = movprb.mm_numdoc)" & _
              " INNER JOIN movmag ON movprb.codditt = movmag.codditt " & _
              " AND movprb.mm_tipork = movmag.mm_nptipo AND movprb.mm_anno = movmag.mm_npanno " & _
              " AND movprb.mm_serie = movmag.mm_npserie AND movprb.mm_numdoc = movmag.mm_npnum " & _
              " AND movprb.mm_riga = movmag.mm_npriga) " & _
              " INNER JOIN anagra ON testprb.codditt = anagra.codditt AND testprb.tm_conto = anagra.an_conto)" & _
              " INNER JOIN TTFLDONOD ON movmag.codditt = TTFLDONOD.codditt AND movmag.mm_tipork = TTFLDONOD.ttn_tipork " & _
              " AND movmag.mm_anno = TTFLDONOD.ttn_anno AND movmag.mm_serie = TTFLDONOD.ttn_serie " & _
              " AND movmag.mm_numdoc = TTFLDONOD.ttn_numero " & _
              " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
              " AND (ttn_tipoogg = 2 OR ttn_tipoogg = 3)"
      If bDDT = False Or bFatture = False Then
        If bDDT Then strSQL += " AND (movmag.mm_tipork = 'B' OR movmag.mm_tipork = 'M' OR movmag.mm_tipork = 'T')"
        If bFatture Then strSQL += " AND NOT (movmag.mm_tipork = 'B' OR movmag.mm_tipork = 'M' OR movmag.mm_tipork = 'T')"
      End If
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!tm_tipork), NTSCInt(dtrT!tm_anno), NTSCStr(dtrT!tm_serie), NTSCInt(dtrT!tm_numdoc), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  CStrSQL(dtrT!tm_tipork) & ", " & dtrT!tm_anno.ToString & " ," & _
                  CStrSQL(dtrT!tm_serie) & ", " & dtrT!tm_numdoc.ToString & " ," & _
                  dtrT!tm_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!tm_datdoc)) & ", 0" & _
                  ", 0, 0, '', 0, 0, '', -1, 8, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, lNodoid, NTSCInt(dtrT!ttn_nodoid), -1)
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetNoteDaOrdini(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                              ByVal lIITTFldolin As Integer, ByRef lNewProgressivo As Integer, _
                                              ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      strSQL = "SELECT DISTINCT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_conto," & _
              " tm_datdoc, an_descr1, ttn_nodoid" & _
              " FROM ((testprb INNER JOIN movprb ON testprb.codditt = movprb.codditt " & _
              " AND testprb.tm_tipork = movprb.mm_tipork AND testprb.tm_anno = movprb.mm_anno " & _
              " AND testprb.tm_serie = movprb.mm_serie AND testprb.tm_numdoc = movprb.mm_numdoc)" & _
              " INNER JOIN anagra ON testprb.codditt = anagra.codditt AND testprb.tm_conto = anagra.an_conto)" & _
              " INNER JOIN TTFLDONOD ON movprb.codditt = TTFLDONOD.codditt AND movprb.mm_ortipo = TTFLDONOD.ttn_tipork " & _
              " AND movprb.mm_oranno = TTFLDONOD.ttn_anno AND movprb.mm_orserie = TTFLDONOD.ttn_serie " & _
              " AND movprb.mm_ornum = TTFLDONOD.ttn_numero " & _
              " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
              " AND ttn_tipoogg = 1"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!tm_tipork), NTSCInt(dtrT!tm_anno), NTSCStr(dtrT!tm_serie), NTSCInt(dtrT!tm_numdoc), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  CStrSQL(dtrT!tm_tipork) & ", " & dtrT!tm_anno.ToString & " ," & _
                  CStrSQL(dtrT!tm_serie) & ", " & dtrT!tm_numdoc.ToString & " ," & _
                  dtrT!tm_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!tm_datdoc)) & ", 0" & _
                  ", 0, 0, '', 0, 0, '', -1, 8, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, NTSCInt(dtrT!ttn_nodoid), lNodoid, -1)
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetDDTNodo0(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                       ByVal strTipork As String, ByVal nAnno As Integer, _
                                       ByVal strSerie As String, ByVal lNumero As Integer, _
                                       ByRef lNewProgressivo As Integer, _
                                       ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
              " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
              " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
              " ttn_order, ttn_attrib)" & _
              " SELECT testmag.codditt, " & lIITTFldonod & ", " & lNewProgressivo & ", tm_tipork, tm_anno," & _
              " tm_serie, tm_numdoc, tm_conto, Left(an_descr1, 40), tm_datdoc, 0, 0," & _
              " 0, '', 0, 0, '', 0, 2, 0, 0" & _
              " FROM testmag INNER JOIN anagra ON testmag.codditt = anagra.codditt " & _
              " AND testmag.tm_conto = anagra.an_conto " & _
              " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(strTipork) & _
              " AND (tm_tipork = 'B' OR tm_tipork = 'M' OR tm_tipork = 'T')" & _
              " AND tm_anno = " & nAnno.ToString & _
              " AND tm_serie = " & CStrSQL(strSerie) & _
              " AND tm_numdoc = " & lNumero.ToString
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDDTeFatimmDaNote(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                           ByVal lIITTFldolin As Integer, ByVal bFatimm As Boolean, _
                                           ByRef lNewProgressivo As Integer, ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      strSQL = "SELECT DISTINCT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_conto," & _
              " tm_datdoc, an_descr1, ttn_nodoid" & _
              " FROM ((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt " & _
              " AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno " & _
              " AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc)" & _
              " INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto)" & _
              " INNER JOIN TTFLDONOD ON movmag.codditt = TTFLDONOD.codditt AND movmag.mm_nptipo = TTFLDONOD.ttn_tipork " & _
              " AND movmag.mm_npanno = TTFLDONOD.ttn_anno AND movmag.mm_npserie = TTFLDONOD.ttn_serie " & _
              " AND movmag.mm_npnum = TTFLDONOD.ttn_numero " & _
              " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
               " AND ttn_tipoogg = 8 AND tm_tipork " & IIf(bFatimm, " NOT ", "").ToString & " IN ('B', 'M', 'T')"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!tm_tipork), NTSCInt(dtrT!tm_anno), NTSCStr(dtrT!tm_serie), NTSCInt(dtrT!tm_numdoc), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  CStrSQL(dtrT!tm_tipork) & ", " & dtrT!tm_anno.ToString & " ," & _
                  CStrSQL(dtrT!tm_serie) & ", " & dtrT!tm_numdoc.ToString & " ," & _
                  dtrT!tm_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!tm_datdoc)) & ", 0" & _
                  ", 0, 0, '', 0, 0, '', " & IIf(bFatimm = False, "0, 2", "1, 3").ToString & ", 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, NTSCInt(dtrT!ttn_nodoid), lNodoid, NTSCInt(IIf(bFatimm = False, 0, 1)))
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetDDTeFatimmDaOrdini(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                                    ByVal lIITTFldolin As Integer, ByVal bFatimm As Boolean, _
                                                    ByRef lNewProgressivo As Integer, ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      'devo escludere gli ordini collegati a note di prelievo (prendo i ddt non collegati a note)
      strSQL = "SELECT DISTINCT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_conto," & _
                " tm_datdoc, an_descr1, ttn_nodoid" & _
                " FROM ((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt " & _
                " AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno " & _
                " AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc)" & _
                " INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto)" & _
                " INNER JOIN TTFLDONOD ON movmag.codditt = TTFLDONOD.codditt AND movmag.mm_ortipo = TTFLDONOD.ttn_tipork " & _
                " AND movmag.mm_oranno = TTFLDONOD.ttn_anno AND movmag.mm_orserie = TTFLDONOD.ttn_serie " & _
                " AND movmag.mm_ornum = TTFLDONOD.ttn_numero " & _
                " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTFldonod.ToString & _
                " AND ttn_tipoogg = 1" & _
                " AND mm_npnum = 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!tm_tipork), NTSCInt(dtrT!tm_anno), NTSCStr(dtrT!tm_serie), NTSCInt(dtrT!tm_numdoc), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  CStrSQL(dtrT!tm_tipork) & ", " & dtrT!tm_anno.ToString & " ," & _
                  CStrSQL(dtrT!tm_serie) & ", " & dtrT!tm_numdoc.ToString & " ," & _
                  dtrT!tm_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!tm_datdoc)) & ", 0" & _
                  ", 0, 0, '', 0, 0, '', " & IIf(dtrT!tm_tipork.ToString = "B" Or dtrT!tm_tipork.ToString = "M" Or dtrT!tm_tipork.ToString = "T", "0, 2", "1, 3").ToString & ", 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, NTSCInt(dtrT!ttn_nodoid), lNodoid, NTSCInt(IIf(dtrT!tm_tipork.ToString = "B" Or dtrT!tm_tipork.ToString = "M" Or dtrT!tm_tipork.ToString = "T", 0, 1)))
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetDDTDaFatture(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                              ByVal lIITTFldolin As Integer, ByRef lNewProgressivo As Integer, _
                                              ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      strSQL = "SELECT DISTINCT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_conto," & _
              " tm_datdoc, an_descr1, ttn_nodoid" & _
              " FROM ((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt " & _
              " AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno " & _
              " AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc)" & _
              " INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto)" & _
              " INNER JOIN TTFLDONOD ON movmag.codditt = TTFLDONOD.codditt AND testmag.tm_tiporkfat = TTFLDONOD.ttn_tipork " & _
              " AND testmag.tm_annfat = TTFLDONOD.ttn_anno AND testmag.tm_alffat = TTFLDONOD.ttn_serie " & _
              " AND testmag.tm_numfat = TTFLDONOD.ttn_numero " & _
              " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
              " AND (ttn_tipoogg = 3 OR ttn_tipoogg = 4)"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!tm_tipork), NTSCInt(dtrT!tm_anno), NTSCStr(dtrT!tm_serie), NTSCInt(dtrT!tm_numdoc), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  CStrSQL(dtrT!tm_tipork) & ", " & dtrT!tm_anno.ToString & " ," & _
                  CStrSQL(dtrT!tm_serie) & ", " & dtrT!tm_numdoc.ToString & " ," & _
                  dtrT!tm_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!tm_datdoc)) & ", 0" & _
                  ", 0, 0, '', 0, 0, '', 0, 2, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, lNodoid, NTSCInt(dtrT!ttn_nodoid), 0)
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetFattureNodo0(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                             ByVal strTipork As String, ByVal nAnno As Integer, _
                                             ByVal strSerie As String, ByVal lNumero As Integer, _
                                             ByRef lNewProgressivo As Integer, _
                                             ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
              " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
              " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
              " ttn_order, ttn_attrib)" & _
              " SELECT testmag.codditt, " & lIITTFldonod & ", " & lNewProgressivo & ", tm_tipork, tm_anno," & _
              " tm_serie, tm_numdoc, tm_conto, Left(an_descr1, 40), tm_datdoc, 0, 0," & _
              " 0, '', 0, 0, '', 1, CASE WHEN tm_tipork IN ('D', 'P', 'K') THEN 4 ELSE 3 END, 0, 0" & _
              " FROM testmag INNER JOIN anagra ON testmag.codditt = anagra.codditt " & _
              " AND testmag.tm_conto = anagra.an_conto " & _
              " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(strTipork) & _
              " AND tm_tipork <> 'B' AND tm_tipork <> 'M' AND tm_tipork <> 'T'" & _
              " AND tm_anno = " & nAnno.ToString & _
              " AND tm_serie = " & CStrSQL(strSerie) & _
              " AND tm_numdoc = " & lNumero.ToString
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetFattureDaDDT(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                            ByVal lIITTFldolin As Integer, ByRef lNewProgressivo As Integer, _
                                            ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      strSQL = "SELECT DISTINCT testmag.tm_tipork, testmag.tm_anno, testmag.tm_serie, testmag.tm_numdoc, testmag.tm_conto," & _
              " testmag.tm_datdoc, an_descr1, ttn_nodoid" & _
              " FROM ((testmag INNER JOIN testmag as testmagDDT ON testmag.codditt = testmagDDT.codditt " & _
              " AND testmag.tm_tipork = testmagDDT.tm_tiporkfat AND testmag.tm_anno = testmagDDT.tm_annfat " & _
              " AND testmag.tm_serie = testmagDDT.tm_alffat AND testmag.tm_numdoc = testmagDDT.tm_numfat)" & _
              " INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto)" & _
              " INNER JOIN TTFLDONOD ON testmagDDT.codditt = TTFLDONOD.codditt AND testmagDDT.tm_tipork = TTFLDONOD.ttn_tipork " & _
              " AND testmagDDT.tm_anno = TTFLDONOD.ttn_anno AND testmagDDT.tm_serie = TTFLDONOD.ttn_serie " & _
              " AND testmagDDT.tm_numdoc = TTFLDONOD.ttn_numero " & _
              " WHERE TTFLDONOD.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
              " AND ttn_tipoogg = 2"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!tm_tipork), NTSCInt(dtrT!tm_anno), NTSCStr(dtrT!tm_serie), NTSCInt(dtrT!tm_numdoc), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  CStrSQL(dtrT!tm_tipork) & ", " & dtrT!tm_anno.ToString & " ," & _
                  CStrSQL(dtrT!tm_serie) & ", " & dtrT!tm_numdoc.ToString & " ," & _
                  dtrT!tm_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!tm_datdoc)) & ", 0" & _
                  ", 0, 0, '', 0, 0, '', 1, " & IIf(NTSCStr(dtrT!tm_tipork) = "D" Or NTSCStr(dtrT!tm_tipork) = "P" Or NTSCStr(dtrT!tm_tipork) = "K" Or NTSCStr(dtrT!tm_tipork) = "£" Or NTSCStr(dtrT!tm_tipork) = "(", "4", "3").ToString & ", 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, NTSCInt(dtrT!ttn_nodoid), lNodoid, NTSCInt(IIf(NTSCStr(dtrT!tm_tipork) = "D" Or NTSCStr(dtrT!tm_tipork) = "P" Or NTSCStr(dtrT!tm_tipork) = "K" Or NTSCStr(dtrT!tm_tipork) = "£" Or NTSCStr(dtrT!tm_tipork) = "(", 4, 3)))
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetFattureDaPrinot(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                                 ByVal lIITTFldolin As Integer, ByVal strUsaContoFattDoc As String, _
                                                 ByRef lNewProgressivo As Integer, ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      strSQL = "SELECT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_conto, tm_datdoc, an_descr1, ttn_nodoid" & _
                " FROM ((testmag INNER JOIN prinot ON testmag.codditt = prinot.codditt AND testmag.tm_anno = prinot.pn_annpar AND testmag.tm_serie = prinot.pn_alfpar AND testmag.tm_numdoc = prinot.pn_numpar)" & _
                " INNER JOIN anagra ON anagra.codditt = prinot.codditt AND anagra.an_conto = prinot.pn_conto)" & _
                " INNER JOIN TTFLDONOD ON prinot.codditt = TTFLDONOD.codditt AND prinot.pn_datreg = TTFLDONOD.ttn_datreg " & _
                " AND prinot.pn_numreg = TTFLDONOD.ttn_numreg  AND prinot.pn_riga = TTFLDONOD.ttn_rigareg " & _
                " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTFldonod.ToString & _
                " AND ttn_tipoogg = 5" & _
                " AND testmag.tm_numpar = 0" & _
                " AND testmag.tm_tipork <> 'B'" & _
                " AND testmag.tm_tipork <> 'U'" & _
                " AND testmag.tm_tipork <> 'Z'" & _
                " AND testmag.tm_tipork <> 'M'" 'Esclusi anche i DTT Ricevuti
      strSQL += " AND testmag.tm_tipork <> 'T'"  'per carichi da produzione non posso avere documenti attivi, per cui per il link con prinot posso passare solo per tm_numpar
      If strUsaContoFattDoc = "1" Then
        strSQL += " AND CASE WHEN testmag.tm_contfatt <> 0 THEN testmag.tm_contfatt ELSE testmag.tm_conto END = prinot.pn_conto "
      ElseIf strUsaContoFattDoc = "2" Then
        strSQL += " AND CASE WHEN testmag.tm_contfatt <> 0 and tm_tipork <> 'D' and tm_tipork <> 'K' THEN testmag.tm_contfatt ELSE testmag.tm_conto END = prinot.pn_conto "
      Else
        strSQL += " AND testmag.tm_conto = prinot.pn_conto "
      End If
      strSQL += " UNION" & _
                " SELECT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_conto, tm_datdoc, an_descr1, ttn_nodoid" & _
                " FROM ((testmag INNER JOIN prinot ON testmag.codditt = prinot.codditt AND testmag.tm_annpar = prinot.pn_annpar AND testmag.tm_alfpar = prinot.pn_alfpar AND testmag.tm_numpar = prinot.pn_numpar)" & _
                " INNER JOIN anagra ON anagra.codditt = prinot.codditt AND anagra.an_conto = prinot.pn_conto)" & _
                " INNER JOIN TTFLDONOD ON prinot.codditt = TTFLDONOD.codditt AND prinot.pn_datreg = TTFLDONOD.ttn_datreg " & _
                " AND prinot.pn_numreg = TTFLDONOD.ttn_numreg  AND prinot.pn_riga = TTFLDONOD.ttn_rigareg " & _
                " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTFldonod.ToString & _
                " AND ttn_tipoogg = 5" & _
                " AND testmag.tm_numpar <> 0" & _
                " AND testmag.tm_tipork <> 'B'" & _
                " AND testmag.tm_tipork <> 'U'" & _
                " AND testmag.tm_tipork <> 'Z'" & _
                " AND testmag.tm_tipork <> 'M'" 'Esclusi anche i DTT Ricevuti altrimenti vengono iseriti nel flusso come contabilizzati se esiste un altro documento (es. fattura immediata ricevuta) contabilizzato con la stessa partita
      If strUsaContoFattDoc = "1" Then
        strSQL += " AND CASE WHEN testmag.tm_contfatt <> 0 THEN testmag.tm_contfatt ELSE testmag.tm_conto END = prinot.pn_conto "
      ElseIf strUsaContoFattDoc = "2" Then
        strSQL += " AND CASE WHEN testmag.tm_contfatt <> 0 and tm_tipork <> 'D' and tm_tipork <> 'K' THEN testmag.tm_contfatt ELSE testmag.tm_conto END = prinot.pn_conto "
      Else
        strSQL += " AND testmag.tm_conto = prinot.pn_conto "
      End If
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 1, NTSCStr(dtrT!tm_tipork), NTSCInt(dtrT!tm_anno), NTSCStr(dtrT!tm_serie), NTSCInt(dtrT!tm_numdoc), 0, "", 0, 0, 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  CStrSQL(dtrT!tm_tipork) & ", " & dtrT!tm_anno.ToString & " ," & _
                  CStrSQL(dtrT!tm_serie) & ", " & dtrT!tm_numdoc.ToString & " ," & _
                  dtrT!tm_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!tm_datdoc)) & ", 0" & _
                  ", 0, 0, '', 0, 0, '', 1, " & IIf(NTSCStr(dtrT!tm_tipork) = "D" Or NTSCStr(dtrT!tm_tipork) = "P" Or NTSCStr(dtrT!tm_tipork) = "K" Or NTSCStr(dtrT!tm_tipork) = "£" Or NTSCStr(dtrT!tm_tipork) = "(", "4", "3").ToString & ", 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, lNodoid, NTSCInt(dtrT!ttn_nodoid), NTSCInt(IIf(NTSCStr(dtrT!tm_tipork) = "D" Or NTSCStr(dtrT!tm_tipork) = "P" Or NTSCStr(dtrT!tm_tipork) = "K" Or NTSCStr(dtrT!tm_tipork) = "£" Or NTSCStr(dtrT!tm_tipork) = "(", 4, 3)))
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetPrinotNodo0(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                           ByVal strDatreg As String, ByVal lNumreg As Integer, _
                                           ByVal nRigareg As Integer, ByRef lNewProgressivo As Integer, _
                                           ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
              " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
              " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
              " ttn_order, ttn_attrib)" & _
              " SELECT prinot.codditt, " & lIITTFldonod & ", " & lNewProgressivo & ", '', 0," & _
              " '', 0, pn_conto, Left(an_descr1, 40), pn_datreg, pn_numreg, pn_riga," & _
              " 0, '', 0, 0, '', 2, 5, 0, 0" & _
              " FROM prinot INNER JOIN anagra ON prinot.codditt = anagra.codditt " & _
              " AND prinot.pn_conto = anagra.an_conto " & _
              " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
              " AND pn_datreg = " & CDataSQL(strDatreg) & _
              " AND pn_numreg = " & lNumreg.ToString & _
              " AND pn_riga = " & nRigareg.ToString
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetPrinotDaFatture(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                               ByVal lIITTFldolin As Integer, ByVal strUsaContoFattDoc As String, _
                                               ByRef lNewProgressivo As Integer, ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      'ttn_tipoogg = 2 AND ttn_tipork IN ('M', 'T')) : ovvero in prinot cerco anche i ddt ric con indicato negli 
      'estremi partita la stessa partita della fattura (per chi non registra le fatture differite ricevute ma 
      'registra in CG la fattura e riapre i ddt ric per inserire gli estremi della partita della fattura)
      strSQL = "SELECT pn_conto, pn_numreg, pn_riga, pn_annpar, pn_alfpar, pn_numpar, pn_datreg, an_descr1, ttn_nodoid" & _
               " FROM ((testmag INNER JOIN prinot ON testmag.codditt = prinot.codditt " & _
               " AND testmag.tm_anno = prinot.pn_annpar AND testmag.tm_serie = prinot.pn_alfpar " & _
               " AND testmag.tm_numdoc = prinot.pn_numpar)" & _
               " INNER JOIN anagra ON anagra.codditt = prinot.codditt AND anagra.an_conto = prinot.pn_conto)" & _
               " INNER JOIN TTFLDONOD ON testmag.codditt = TTFLDONOD.codditt AND testmag.tm_tipork = TTFLDONOD.ttn_tipork " & _
               " AND testmag.tm_anno = TTFLDONOD.ttn_anno AND testmag.tm_serie = TTFLDONOD.ttn_serie " & _
               " AND testmag.tm_numdoc = TTFLDONOD.ttn_numero " & _
               " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTFldonod.ToString & _
               " AND ((ttn_tipoogg = 2 AND ttn_tipork IN ('M', 'T')) OR ttn_tipoogg = 3 OR ttn_tipoogg = 4)" & _
               " AND testmag.tm_numpar = 0" & _
               " AND testmag.tm_tipork <> 'Z'"
      If strUsaContoFattDoc = "1" Then
        strSQL = strSQL & " AND CASE WHEN testmag.tm_contfatt <> 0 THEN testmag.tm_contfatt ELSE testmag.tm_conto END = prinot.pn_conto "
      Else
        strSQL = strSQL & " AND testmag.tm_conto = prinot.pn_conto "
      End If
      strSQL = strSQL & " UNION" & _
               " SELECT pn_conto, pn_numreg, pn_riga, pn_annpar, pn_alfpar, pn_numpar, pn_datreg, an_descr1, ttn_nodoid" & _
               " FROM ((testmag INNER JOIN prinot ON testmag.codditt = prinot.codditt " & _
               " AND testmag.tm_annpar = prinot.pn_annpar AND testmag.tm_alfpar = prinot.pn_alfpar " & _
               " AND testmag.tm_numpar = prinot.pn_numpar)" & _
               " INNER JOIN anagra ON anagra.codditt = prinot.codditt AND anagra.an_conto = prinot.pn_conto)" & _
               " INNER JOIN TTFLDONOD ON testmag.codditt = TTFLDONOD.codditt AND testmag.tm_tipork = TTFLDONOD.ttn_tipork " & _
               " AND testmag.tm_anno = TTFLDONOD.ttn_anno AND testmag.tm_serie = TTFLDONOD.ttn_serie " & _
               " AND testmag.tm_numdoc = TTFLDONOD.ttn_numero " & _
               " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTFldonod.ToString & _
               " AND ((ttn_tipoogg = 2 AND ttn_tipork IN ('M', 'T')) OR ttn_tipoogg = 3 OR ttn_tipoogg = 4)" & _
               " AND testmag.tm_numpar <> 0"
      If strUsaContoFattDoc = "1" Then
        strSQL = strSQL & " AND CASE WHEN testmag.tm_contfatt <> 0 THEN testmag.tm_contfatt ELSE testmag.tm_conto END = prinot.pn_conto "
      Else
        strSQL = strSQL & " AND testmag.tm_conto = prinot.pn_conto "
      End If
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 2, "", 0, "", 0, 0, NTSCDate(dtrT!pn_datreg).ToShortDateString, NTSCInt(dtrT!pn_numreg), NTSCInt(dtrT!pn_riga), 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  "'', 0 ,'', 0 ," & _
                  dtrT!pn_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!pn_datreg)) & ", " & dtrT!pn_numreg.ToString & ", " & _
                  dtrT!pn_riga.ToString & ", 0, '', 0, 0, '', 2, 5, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, NTSCInt(dtrT!ttn_nodoid), lNodoid, 2)
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetPrinotDaScadenze(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                                  ByVal lIITTFldolin As Integer, ByRef lNewProgressivo As Integer, _
                                                  ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      strSQL = "SELECT pn_conto, an_descr1, pn_datreg, pn_numreg, pn_riga, pn_annpar, pn_alfpar, pn_numpar, ttn_nodoid" & _
              " FROM ((prinot INNER JOIN scaden ON prinot.codditt = scaden.codditt " & _
              " AND prinot.pn_numpar = scaden.sc_numpar AND prinot.pn_alfpar = scaden.sc_alfpar " & _
              " AND prinot.pn_annpar = scaden.sc_annpar AND prinot.pn_datreg = scaden.sc_datreg " & _
              " AND prinot.pn_numreg = scaden.sc_numreg AND prinot.pn_conto = scaden.sc_conto)" & _
              " INNER JOIN anagra ON anagra.codditt = prinot.codditt AND anagra.an_conto = prinot.pn_conto)" & _
              " INNER JOIN TTFLDONOD ON scaden.codditt = TTFLDONOD.codditt " & _
              " AND scaden.sc_conto = TTFLDONOD.ttn_conto" & _
              " AND scaden.sc_annpar = TTFLDONOD.ttn_annpar" & _
              " AND scaden.sc_alfpar = TTFLDONOD.ttn_alfpar" & _
              " AND scaden.sc_numpar = TTFLDONOD.ttn_numpar" & _
              " AND scaden.sc_numrata = TTFLDONOD.ttn_numrata" & _
              " WHERE scaden.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
              " AND ttn_tipoogg = 6" & _
              " UNION" & _
              " SELECT pn_conto, an_descr1, pn_datreg, pn_numreg, pn_riga, pn_annpar, pn_alfpar, pn_numpar, ttn_nodoid" & _
              " FROM ((prinot INNER JOIN scaden ON prinot.codditt = scaden.codditt " & _
              " AND prinot.pn_numpar = scaden.sc_numpar AND prinot.pn_alfpar = scaden.sc_alfpar " & _
              " AND prinot.pn_annpar = scaden.sc_annpar AND prinot.pn_numreg = scaden.sc_rgsaldato " & _
              " AND prinot.pn_conto = scaden.sc_conto)" & _
              " INNER JOIN anagra ON anagra.codditt = prinot.codditt AND anagra.an_conto = prinot.pn_conto)" & _
              " INNER JOIN TTFLDONOD ON scaden.codditt = TTFLDONOD.codditt " & _
              " AND scaden.sc_conto = TTFLDONOD.ttn_conto" & _
              " AND scaden.sc_annpar = TTFLDONOD.ttn_annpar" & _
              " AND scaden.sc_alfpar = TTFLDONOD.ttn_alfpar" & _
              " AND scaden.sc_numpar = TTFLDONOD.ttn_numpar" & _
              " AND scaden.sc_numrata = TTFLDONOD.ttn_numrata" & _
              " WHERE scaden.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
              " AND ttn_tipoogg = 6" & _
              " GROUP BY pn_conto, an_descr1, pn_datreg, pn_numreg, pn_riga, pn_annpar, pn_alfpar, pn_numpar, ttn_nodoid "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 2, "", 0, "", 0, 0, NTSCDate(dtrT!pn_datreg).ToShortDateString, NTSCInt(dtrT!pn_numreg), NTSCInt(dtrT!pn_riga), 0, 0, "", 0, 0, lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  "'', 0 ,'', 0 ," & _
                  dtrT!pn_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!pn_datreg)) & ", " & dtrT!pn_numreg.ToString & ", " & _
                  dtrT!pn_riga.ToString & ", 0, '', 0, 0, '', 2, 5, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, lNodoid, NTSCInt(dtrT!ttn_nodoid), 2)
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetScadenNodo0(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                           ByVal lConto As Integer, ByVal nAnnpar As Integer, _
                                           ByVal strAlfpar As String, ByVal lNumpar As Integer, _
                                           ByVal nNumrata As Integer, ByRef lNewProgressivo As Integer, _
                                           ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
              " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
              " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
              " ttn_order, ttn_attrib)" & _
              " SELECT scaden.codditt, " & lIITTFldonod & ", " & lNewProgressivo & ", '', 0," & _
              " '', 0, sc_conto, Left(an_descr1, 40), sc_datsca, 0, 0," & _
              " sc_annpar, sc_alfpar, sc_numpar, sc_numrata, '', 3, 6, 0, 0" & _
              " FROM scaden INNER JOIN anagra ON scaden.codditt = anagra.codditt " & _
              " AND scaden.sc_conto = anagra.an_conto " & _
              " WHERE scaden.codditt = " & CStrSQL(strDitta) & _
              " AND sc_conto = " & lConto & _
              " AND sc_annpar = " & nAnnpar & _
              " AND sc_alfpar = " & CStrSQL(strAlfpar) & _
              " AND sc_numpar = " & lNumpar & _
              " AND sc_numrata = " & nNumrata
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetScadenzeDaPrinot(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                                  ByVal lIITTFldolin As Integer, ByRef lNewProgressivo As Integer, _
                                                  ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lNodoid As Integer = 0
    Dim nRecAdd As Integer = 0
    Try
      strSQL = "SELECT sc_conto, sc_annpar, sc_alfpar, sc_numpar, sc_numrata, an_descr1, sc_datsca, ttn_nodoid" & _
              " FROM ((prinot INNER JOIN scaden ON prinot.codditt = scaden.codditt " & _
              " AND prinot.pn_numpar = scaden.sc_numpar AND prinot.pn_alfpar = scaden.sc_alfpar " & _
              " AND prinot.pn_annpar = scaden.sc_annpar AND prinot.pn_conto = scaden.sc_conto)" & _
              " INNER JOIN anagra ON anagra.codditt = prinot.codditt AND anagra.an_conto = prinot.pn_conto)" & _
              " INNER JOIN TTFLDONOD ON prinot.codditt = TTFLDONOD.codditt " & _
              " AND prinot.pn_datreg = TTFLDONOD.ttn_datreg AND prinot.pn_numreg = TTFLDONOD.ttn_numreg" & _
              " AND prinot.pn_riga = TTFLDONOD.ttn_rigareg " & _
              " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTFldonod.ToString & _
              " AND ttn_tipoogg = 5" & _
              " AND ((pn_datreg = sc_datreg AND pn_numreg = sc_numreg) OR (pn_datreg = sc_dtsaldato AND pn_numreg = sc_rgsaldato)) "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      nRec = 0
      For Each dtrT As DataRow In dttTmp.Rows
        If Not Exist(strDitta, lIITTFldonod, 3, "", 0, "", 0, 0, "", 0, 0, NTSCInt(dtrT!sc_conto), NTSCInt(dtrT!sc_annpar), NTSCStr(dtrT!sc_alfpar), NTSCInt(dtrT!sc_numpar), NTSCInt(dtrT!sc_numrata), lNodoid) Then
          lNewProgressivo += 1
          strSQL = "INSERT INTO TTFLDONOD (codditt, instid, ttn_nodoid, ttn_tipork, ttn_anno," & _
                  " ttn_serie, ttn_numero, ttn_conto, ttn_desconto, ttn_datreg, ttn_numreg, ttn_rigareg," & _
                  " ttn_annpar, ttn_alfpar, ttn_numpar, ttn_numrata, ttn_note, ttn_livello, ttn_tipoogg," & _
                  " ttn_order, ttn_attrib)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldonod & ", " & lNewProgressivo & ", " & _
                  "'', 0 ,'', 0 ," & _
                  dtrT!sc_conto.ToString & ", " & CStrSQL(Left(dtrT!an_descr1.ToString, 40)) & ", " & _
                  CDataSQL(NTSCDate(dtrT!sc_datsca)) & ", 0, 0, " & dtrT!sc_annpar.ToString & ", " & CStrSQL(dtrT!sc_alfpar) & ", " & _
                  dtrT!sc_numpar.ToString & ", " & dtrT!sc_numrata.ToString & ", '', 3, 6, 0, 0)"
          nRecAdd += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          nRec += nRecAdd
          If nRecAdd <> 0 Then lNodoid = lNewProgressivo
        End If
        If lNodoid <> 0 Then
          AddLink(strDitta, lIITTFldolin, NTSCInt(dtrT!ttn_nodoid), lNodoid, 3)
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function AddLink(ByVal strDitta As String, ByVal lIITTFldolin As Integer, _
                                      ByVal lNodoLeft As Integer, ByVal lNodoRight As Integer, _
                                      ByVal nTipoogg As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO TTFLDOLIN (codditt, instid, ttl_nodoidl, ttl_nodoidr, ttl_attrib)" & _
               " VALUES (" & CStrSQL(strDitta) & ", " & lIITTFldolin & ", " & lNodoLeft & ", " & lNodoRight & ", " & nTipoogg & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      'ignoro l'errore: il link potrebbe già esserci (collegamento tra IC , NOTA e DDT ...)
    End Try
  End Function

  Public Overridable Function AggiornaCampoNote(ByVal strDitta As String, ByVal lIITTFldonod As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strNote As String = ""
    Try
      strSQL = "SELECT * FROM TTFLDONOD" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTFldonod.ToString & _
                " ORDER BY ttn_tipoogg"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For Each dtrT As DataRow In dttTmp.Rows
        strNote = ""
        Select Case NTSCInt(dtrT!ttn_tipoogg)
          Case 1  'ordine
            strNote = AggiornaCampoNote_Ordine(strDitta, lIITTFldonod, dtrT)
          Case 2, 3, 4  'ddt, ft imm, ft diff
            strNote = AggiornaCampoNote_DDTeFatture(strDitta, lIITTFldonod, dtrT)
          Case 5  'cg
            strNote = AggiornaCampoNote_CG(strDitta, lIITTFldonod, dtrT)
          Case 6  'scad
            strNote = AggiornaCampoNote_Scad(strDitta, lIITTFldonod, dtrT)
          Case 7  'offerta
            strNote = AggiornaCampoNote_Offerta(strDitta, lIITTFldonod, dtrT)
          Case 8  'nota prel
            strNote = AggiornaCampoNote_NotaPrel(strDitta, lIITTFldonod, dtrT)
        End Select

        If strNote.Trim <> "" Then
          strSQL = "UPDATE TTFLDONOD" & _
                  " SET ttn_note = " & CStrSQL(strNote) & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND instid = " & lIITTFldonod & _
                  " AND ttn_nodoid = " & NTSCInt(dtrT!ttn_nodoid)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If

      Next    'For Each dtrT As DataRow In dttTmp.Rows

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function AggiornaCampoNote_Offerta(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                                     ByRef dtrT As DataRow) As String
    Dim strSQL As String = ""
    Dim strNote As String = ""
    Dim dttTmp1 As New DataTable
    Try
      strSQL = "SELECT testoff.*, le_descr1, tb_destpbf" & _
              " FROM (testoff INNER JOIN leads ON testoff.codditt = leads.codditt AND testoff.td_codlead = leads.le_codlead)" & _
              " INNER JOIN tabtpbf ON testoff.codditt = tabtpbf.codditt AND testoff.td_tipobf = tabtpbf.tb_codtpbf" & _
              " WHERE testoff.codditt = " & CStrSQL(strDitta) & _
              " AND td_tipork = " & CStrSQL(dtrT!ttn_tipork) & _
              " AND td_anno = " & NTSCInt(dtrT!ttn_anno) & _
              " AND td_serie = " & CStrSQL(dtrT!ttn_serie) & _
              " AND td_numord = " & NTSCInt(dtrT!ttn_numero) & _
              " AND td_vers = " & dtrT!ttn_numreg.ToString
      dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp1.Rows.Count > 0 Then
        strNote = "Lead: " & NTSCStr(dttTmp1.Rows(0)!le_descr1) & vbCrLf
        If NTSCInt(dttTmp1.Rows(0)!td_tipobf) <> 0 Then strNote = strNote & "Operazione: " & NTSCStr(dttTmp1.Rows(0)!tb_destpbf) & vbCrLf
      End If
      Return strNote
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    Finally
      dttTmp1.Clear()
    End Try
  End Function
  Public Overridable Function AggiornaCampoNote_Ordine(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                                       ByRef dtrT As DataRow) As String
    Dim strSQL As String = ""
    Dim strNote As String = ""
    Dim dttTmp1 As New DataTable
    Try
      strSQL = "SELECT testord.*, an_descr1, tb_destpbf FROM ((testord INNER JOIN tabmaga ON (testord.td_magaz = tabmaga.tb_codmaga)" & _
              " AND (testord.codditt = tabmaga.codditt)) INNER JOIN tabtpbf ON (testord.td_tipobf = tabtpbf.tb_codtpbf) AND (testord.codditt = tabtpbf.codditt))" & _
              " INNER JOIN anagra ON (testord.td_conto = anagra.an_conto) AND (testord.codditt = anagra.codditt)" & _
              " WHERE testord.codditt = " & CStrSQL(strDitta) & _
              " AND td_tipork = " & CStrSQL(dtrT!ttn_tipork) & _
              " AND td_anno = " & NTSCInt(dtrT!ttn_anno) & _
              " AND td_numord = " & NTSCInt(dtrT!ttn_numero) & _
              " AND td_serie = " & CStrSQL(dtrT!ttn_serie)
      dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp1.Rows.Count > 0 Then
        strNote = "Conto: " & NTSCStr(dttTmp1.Rows(0)!an_descr1) & vbCrLf
        If NTSCInt(dttTmp1.Rows(0)!td_tipobf) <> 0 Then strNote = strNote & "Tipo BF: " & NTSCStr(dttTmp1.Rows(0)!tb_destpbf) & vbCrLf
        If NTSCInt(dttTmp1.Rows(0)!td_magaz) <> 0 Then strNote = strNote & "Mag: " & NTSCStr(dttTmp1.Rows(0)!td_magaz) & vbCrLf
        If NTSCStr(dttTmp1.Rows(0)!td_riferim) <> "" Then strNote = strNote & "Riferim: " & NTSCStr(dttTmp1.Rows(0)!td_riferim) & vbCrLf
        If NTSCStr(dttTmp1.Rows(0)!td_datcons) <> "" Then strNote = strNote & "Dt cons: " & NTSCDate(dttTmp1.Rows(0)!td_datcons).ToShortDateString & vbCrLf
        strNote = strNote & "Tot doc: " & NTSCStr(dttTmp1.Rows(0)!td_totdoc)
      End If
      Return strNote
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    Finally
      dttTmp1.Clear()
    End Try
  End Function
  Public Overridable Function AggiornaCampoNote_NotaPrel(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                             ByRef dtrT As DataRow) As String
    Dim strSQL As String = ""
    Dim strNote As String = ""
    Dim dttTmp1 As New DataTable
    Try
      strSQL = "SELECT testprb.*, an_descr1, tb_destpbf, tb_desmaga FROM ((testprb INNER JOIN anagra ON (testprb.codditt = anagra.codditt)" & _
             " AND (testprb.tm_conto = anagra.an_conto)) INNER JOIN tabtpbf ON (testprb.codditt = tabtpbf.codditt)" & _
             " AND (testprb.tm_tipobf = tabtpbf.tb_codtpbf)) INNER JOIN tabmaga ON (testprb.tm_magaz = tabmaga.tb_codmaga)" & _
             " AND (testprb.codditt = tabmaga.codditt)" & _
             " WHERE testprb.codditt = " & CStrSQL(strDitta) & _
             " AND tm_tipork = " & CStrSQL(dtrT!ttn_tipork) & _
             " AND tm_anno = " & NTSCInt(dtrT!ttn_anno) & _
             " AND tm_numdoc = " & NTSCInt(dtrT!ttn_numero) & _
             " AND tm_serie = " & CStrSQL(dtrT!ttn_serie)
      dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp1.Rows.Count > 0 Then
        strNote = "Conto: " & NTSCStr(dttTmp1.Rows(0)!an_descr1) & vbCrLf
        If NTSCInt(dttTmp1.Rows(0)!tm_tipobf) <> 0 Then strNote = strNote & "Tipo BF: " & NTSCStr(dttTmp1.Rows(0)!tb_destpbf) & vbCrLf
        If NTSCInt(dttTmp1.Rows(0)!tm_magaz) <> 0 Then strNote = strNote & "Mag: " & NTSCStr(dttTmp1.Rows(0)!tb_desmaga) & vbCrLf
        If NTSCStr(dttTmp1.Rows(0)!tm_riferim) <> "" Then strNote = strNote & "Riferim: " & NTSCStr(dttTmp1.Rows(0)!tm_riferim) & vbCrLf
        strNote = strNote & "Tot doc: " & NTSCStr(dttTmp1.Rows(0)!tm_totdoc)
      End If
      Return strNote
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    Finally
      dttTmp1.Clear()
    End Try
  End Function
  Public Overridable Function AggiornaCampoNote_DDTeFatture(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                                   ByRef dtrT As DataRow) As String
    Dim strSQL As String = ""
    Dim strNote As String = ""
    Dim dttTmp1 As New DataTable
    Try
      strSQL = "SELECT testmag.*, an_descr1, tb_destpbf, tb_desmaga FROM ((testmag INNER JOIN anagra ON (testmag.codditt = anagra.codditt)" & _
             " AND (testmag.tm_conto = anagra.an_conto)) INNER JOIN tabtpbf ON (testmag.codditt = tabtpbf.codditt)" & _
             " AND (testmag.tm_tipobf = tabtpbf.tb_codtpbf)) INNER JOIN tabmaga ON (testmag.tm_magaz = tabmaga.tb_codmaga)" & _
             " AND (testmag.codditt = tabmaga.codditt)" & _
             " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
             " AND tm_tipork = " & CStrSQL(dtrT!ttn_tipork) & _
             " AND tm_anno = " & NTSCInt(dtrT!ttn_anno) & _
             " AND tm_numdoc = " & NTSCInt(dtrT!ttn_numero) & _
             " AND tm_serie = " & CStrSQL(dtrT!ttn_serie)
      dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp1.Rows.Count > 0 Then
        If NTSCInt(dttTmp1.Rows(0)!tm_numpar) <> 0 Then
          strNote = "Partita: n." & NTSCStr(dttTmp1.Rows(0)!tm_numpar) & IIf(dttTmp1.Rows(0)!tm_alfpar.ToString <> " ", "/" & dttTmp1.Rows(0)!tm_alfpar.ToString, "").ToString & " del " & NTSCDate(dttTmp1.Rows(0)!tm_datpar).ToShortDateString & vbCrLf
        End If
        strNote = strNote & "Conto: " & NTSCStr(dttTmp1.Rows(0)!an_descr1) & vbCrLf
        If NTSCInt(dttTmp1.Rows(0)!tm_tipobf) <> 0 Then strNote = strNote & "Tipo BF: " & NTSCStr(dttTmp1.Rows(0)!tb_destpbf) & vbCrLf
        If NTSCInt(dttTmp1.Rows(0)!tm_magaz) <> 0 Then strNote = strNote & "Mag: " & NTSCStr(dttTmp1.Rows(0)!tb_desmaga) & vbCrLf
        If NTSCStr(dttTmp1.Rows(0)!tm_riferim) <> "" Then strNote = strNote & "Riferim: " & NTSCStr(dttTmp1.Rows(0)!tm_riferim) & vbCrLf
        strNote = strNote & "Tot doc: " & NTSCStr(dttTmp1.Rows(0)!tm_totdoc)
      End If
      Return strNote
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    Finally
      dttTmp1.Clear()
    End Try
  End Function
  Public Overridable Function AggiornaCampoNote_CG(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                                 ByRef dtrT As DataRow) As String
    Dim strSQL As String = ""
    Dim strNote As String = ""
    Dim dttTmp1 As New DataTable
    Try
      strSQL = "SELECT prinot.*, tb_descauc FROM tabcauc INNER JOIN prinot ON tabcauc.tb_codcauc = prinot.pn_causale" & _
              " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
              " AND pn_datreg = " & CDataSQL(NTSCDate(dtrT!ttn_datreg)) & _
              " AND pn_numreg = " & NTSCInt(dtrT!ttn_numreg) & _
              " AND pn_riga = " & NTSCInt(dtrT!ttn_rigareg)
      dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp1.Rows.Count > 0 Then
        strNote = "Causale: " & NTSCStr(dttTmp1.Rows(0)!tb_descauc) & vbCrLf & _
                  "Es comp: " & NTSCStr(dttTmp1.Rows(0)!pn_escomp)
      End If
      Return strNote
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    Finally
      dttTmp1.Clear()
    End Try
  End Function
  Public Overridable Function AggiornaCampoNote_Scad(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                               ByRef dtrT As DataRow) As String
    Dim strSQL As String = ""
    Dim strNote As String = ""
    Dim dttTmp1 As New DataTable
    Try
      strSQL = "SELECT scaden.*, an_descr1, tb_despaga" & _
              " FROM (anagra INNER JOIN scaden ON (anagra.codditt = scaden.codditt) AND (anagra.an_conto = scaden.sc_conto))" & _
              " LEFT JOIN tabpaga ON scaden.sc_codpaga = tabpaga.tb_codpaga" & _
              " WHERE scaden.codditt = " & CStrSQL(strDitta) & _
              " AND sc_conto = " & NTSCInt(dtrT!ttn_conto) & _
              " AND sc_annpar = " & NTSCInt(dtrT!ttn_annpar) & _
              " AND sc_alfpar = " & CStrSQL(dtrT!ttn_alfpar) & _
              " AND sc_numpar = " & NTSCInt(dtrT!ttn_numpar) & _
              " AND sc_numrata = " & NTSCInt(dtrT!ttn_numrata)
      dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp1.Rows.Count > 0 Then
        strNote = "Partita nr." & NTSCStr(dttTmp1.Rows(0)!sc_numpar) & IIf(dttTmp1.Rows(0)!sc_alfpar.ToString <> " ", "/" & dttTmp1.Rows(0)!sc_alfpar.ToString, "").ToString & " anno " & dttTmp1.Rows(0)!sc_annpar.ToString & vbCrLf & _
          "Conto: " & NTSCStr(dttTmp1.Rows(0)!an_descr1) & vbCrLf
        If NTSCStr(dttTmp1.Rows(0)!sc_descr) <> "" Then strNote = strNote & "Desc supp: " & NTSCStr(dttTmp1.Rows(0)!sc_descr) & vbCrLf
        strNote = strNote & "Importo: " & NTSCStr(dttTmp1.Rows(0)!sc_importoda) & vbCrLf & _
          "Saldata: " & NTSCStr(dttTmp1.Rows(0)!sc_flsaldato) & " Autoriz: " & NTSCStr(dttTmp1.Rows(0)!sc_fldis) & vbCrLf & _
          "Pagam: " & IIf(NTSCStr(dttTmp1.Rows(0)!tb_despaga) <> "", NTSCStr(dttTmp1.Rows(0)!tb_despaga), "(nessun pagamento)").ToString
      End If
      Return strNote
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    Finally
      dttTmp1.Clear()
    End Try
  End Function

  Public Overridable Function Exist(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                   ByVal nTipo As Integer, ByVal strTipork As String, ByVal nAnno As Integer, _
                                   ByVal strSerie As String, ByVal lNumero As Integer, _
                                   ByVal nVers As Integer, ByVal strDatreg As String, _
                                   ByVal lNumreg As Integer, ByVal nRigareg As Integer, _
                                   ByVal lConto As Integer, ByVal nAnnpar As Integer, _
                                   ByVal strAlfpar As String, ByVal lNumpar As Integer, _
                                   ByVal lNumrata As Integer, ByRef lNodoid As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      lNodoid = 0
      strSQL = "SELECT TOP 1 ttn_nodoid FROM TTFLDONOD " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTFldonod
      Select Case nTipo
        Case 1
          'magazzino, ordini, offerte
          strSQL += " AND ttn_tipork = " & CStrSQL(strTipork) & _
               " AND ttn_anno = " & nAnno & _
               " AND ttn_serie = " & CStrSQL(strSerie) & _
               " AND ttn_numero = " & lNumero & _
               " AND ttn_numreg = " & nVers
        Case 2
          'prinot
          strSQL += " AND ttn_datreg = " & CDataSQL(strDatreg) & _
               " AND ttn_numreg = " & lNumreg
        Case 3
          'scaden
          strSQL += " AND ttn_conto = " & lConto & _
               " AND ttn_annpar = " & nAnnpar & _
               " AND ttn_alfpar = " & CStrSQL(strAlfpar) & _
               " AND ttn_numpar = " & lNumpar & _
               " AND ttn_numrata = " & lNumrata
      End Select

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      lNodoid = NTSCInt(dttTmp.Rows(0)!ttn_nodoid)
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

  Public Overridable Function GetFlusso(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                        ByVal lIITTFldolin As Integer, ByRef dttOut As DataTable, _
                                        ByRef dttOutLink As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM TTFLDONOD" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTFldonod & _
                " ORDER BY ttn_livello, ttn_tipork, ttn_nodoid DESC"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "SELECT * FROM TTFLDOLIN" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTFldolin
      dttOutLink = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetRigaFlusso(ByVal strDitta As String, ByVal lIITTFldonod As Integer, _
                                        ByVal lNodoid As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM TTFLDONOD" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITTFldonod & _
                " AND ttn_nodoid = " & lNodoid
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetValutaScorpo(ByVal strDitta As String, ByVal strTipork As String, _
                                              ByVal nAnno As Integer, ByVal strSerie As String, _
                                              ByVal lNum As Integer, ByVal nRev As Integer, _
                                              ByRef nCodvalu As Integer, ByRef bScorpo As Boolean, _
                                              ByRef strPrzBoll As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    nCodvalu = 0
    bScorpo = False
    Try
      Select Case strTipork
        Case "!"  'offerta 
          strSQL = "SELECT td_valuta, td_scorpo, 'N' as xx_przbol FROM testoff WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND td_tipork = " & CStrSQL(strTipork) & _
                   " AND td_anno = " & nAnno.ToString & _
                   " AND td_serie = " & CStrSQL(strSerie) & _
                   " AND td_numord = " & lNum.ToString

        Case "R", "O", "H", "X", "Q", "#", "V", "$", "Y" 'ordini
          strSQL = "SELECT td_valuta, td_scorpo, 'N' as xx_przbol FROM testord WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND td_tipork = " & CStrSQL(strTipork) & _
                   " AND td_anno = " & nAnno.ToString & _
                   " AND td_serie = " & CStrSQL(strSerie) & _
                   " AND td_numord = " & lNum.ToString

        Case Else 'documenti di magazzino e fatture differite
          strSQL = "SELECT tm_valuta as td_valuta, tm_scorpo as td_scorpo, tb_przbol as xx_przbol " & _
                   " FROM testmag LEFT JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf " & _
                   " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                   " AND tm_tipork = " & CStrSQL(strTipork) & _
                   " AND tm_anno = " & nAnno.ToString & _
                   " AND tm_serie = " & CStrSQL(strSerie) & _
                   " AND tm_numdoc = " & lNum.ToString
          If strTipork = "W" Then strSQL = strSQL.Replace("testmag", "testprb")
      End Select

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        nCodvalu = NTSCInt(dttTmp.Rows(0)!td_valuta)
        bScorpo = NTSCStr(dttTmp.Rows(0)!td_scorpo).ToUpper = "S"
        strPrzBoll = NTSCStr(dttTmp.Rows(0)!xx_przbol).ToUpper
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

End Class
