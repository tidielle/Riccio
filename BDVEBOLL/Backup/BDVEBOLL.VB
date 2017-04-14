Imports System.Data
Imports System.Data.Common
Imports NTSInformatica.CLN__STD

Imports System
Public Class CLDVEBOLL
  Inherits CLDMGDOCU

  Public Overridable Function EsisteDoc(ByVal strDitta As String, ByVal strTipoDoc As String, _
                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      Return EsisteDoc(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc, New DataTable)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EsisteDoc(ByVal strDitta As String, ByVal strTipoDoc As String, _
                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, ByRef dttTmp As DataTable) As Boolean

    'restituisce true se il documento esiste, diversamente false
    Dim strSQL As String = ""
    Dim bResult As Boolean = False
    Dim strWhere As String = ""
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipoDoc, nAnno, strSerie, lNumdoc, dttTmp})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTmp = CType(oIn(5), DataTable)
        Return CBool(oOut)
      End If
      '----------------

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
    End Try
  End Function

  Public Overridable Function CheckEsist(ByVal strDitta As String, ByVal strTipo As String, ByVal nAnno As Integer, _
                                         ByVal strSerie As String, ByVal lNumdoc As Integer, ByRef bEsiste As Boolean, _
                                         ByRef strTipoRestituito As String) As Boolean
    Dim strSQL As String = ""
    Dim strWHERE As String = ""
    Dim strMsg As String = ""
    Dim dttTmp As New DataTable
    Try
      If strSerie = "" Then strSerie = " "
      strTipoRestituito = ""

      Select Case strTipo
        Case "A", "D", "E", "N", "£"
          strWHERE = "(tm_tipork = 'A' OR tm_tipork = 'D' OR tm_tipork = 'E' OR tm_tipork = 'N' OR tm_tipork = '£')"
        Case "B"
          strWHERE = "tm_tipork = 'B'"
        Case "C"
          strWHERE = "tm_tipork = 'C'"
        Case "M"
          strWHERE = "tm_tipork = 'M'"
        Case "Z"
          strWHERE = "tm_tipork = 'Z'"
        Case "L", "K", "J", "("
          strWHERE = "(tm_tipork = 'L' OR tm_tipork = 'K' OR tm_tipork = 'J' OR tm_tipork = '(')"
        Case "T"
          strWHERE = "tm_tipork = 'T'"
        Case "U"
          strWHERE = "tm_tipork = 'U'"
        Case "W"
          strWHERE = "tm_tipork = 'W'"
        Case "F", "I"
          strWHERE = "(tm_tipork = 'I' OR tm_tipork = 'F')"
        Case "S", "P"
          strWHERE = "(tm_tipork = 'S' OR tm_tipork = 'P')"
      End Select
      strSQL = "SELECT tm_tipork FROM " & IIf(strTipo = "W", "testprb", "testmag").ToString & _
                " WHERE codditt = " & CStrSQL(strDitta) & " AND tm_numdoc = " & lNumdoc & _
                " AND tm_anno = " & nAnno & " AND tm_serie = '" & strSerie & "' AND " & strWHERE
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        bEsiste = False
      Else
        bEsiste = True
        strTipoRestituito = dttTmp.Rows(0)!tm_tipork.ToString
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

  Public Overridable Function EsisteMovmagRiga(ByVal strDitta As String, ByVal strTipoDoc As String, ByVal nAnno As Integer, _
                                            ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lRiga As Integer) As Boolean
    'ritorna true se la riga di movmag esiste
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 mm_tipork FROM movmag " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
               " AND mm_anno = " & nAnno & _
               " AND mm_serie = " & CStrSQL(strSerie) & _
               " AND mm_numdoc = " & lNumdoc.ToString & _
               " AND mm_riga = " & lRiga.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function GetDatdocLastDoc(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByRef strTiporkOut As String) As DateTime
    Try
      'obsoleta
      Return GetDatdocLastDoc(strDitta, strTipork, nAnno, strSerie, strTiporkOut, -1, "", "")

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDatdocLastDoc(ByVal strDitta As String, ByVal strTipork As String, _
                                               ByVal nAnno As Integer, ByVal strSerie As String, _
                                               ByRef strTiporkOut As String, ByVal lNumdoc As Integer, _
                                               ByRef strDatdocOut1 As String, ByRef strTiporkOut1 As String) As DateTime
    Dim strSQL As String = ""

    Dim strElencoTipork As String = ""
    Dim dttTmp As New DataTable

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, nAnno, strSerie, strTiporkOut, lNumdoc, strDatdocOut1, strTiporkOut1})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strTiporkOut = NTSCStr(oIn(4))
        strDatdocOut1 = NTSCStr(oIn(6))
        strTiporkOut1 = NTSCStr(oIn(7))
        Return NTSCDate(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      GetDatdocLastDoc = NTSCDate(IntSetDate("01/01/1900"))
      strTiporkOut = ""
      strDatdocOut1 = ""
      strTiporkOut1 = ""
      '--------------------------------------------------------------------------------------------------------------
      strElencoTipork = CStrSQL(strTipork)
      Select Case strTipork
        Case "A", "D", "E", "N", "£" : strElencoTipork = "'A', 'D', 'E', 'N', '£'"
        Case "L", "K", "J", "(" : strElencoTipork = "'L', 'K', 'J', '('"
        Case "F", "I" : strElencoTipork = "'F', 'I'"
        Case "S", "P" : strElencoTipork = "'S', 'P'"
      End Select
      '--------------------------------------------------------------------------------------------------------------

      If lNumdoc < 0 Then
        'vecchio sistema: se non indicato il num. documento, prendo su l'ultimo fatto
        strSQL = "SELECT TOP 1 tm_datdoc, tm_tipork FROM " & IIf(strTipork = "W", "testprb", "testmag").ToString & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND tm_tipork IN (" & strElencoTipork & ") " & _
                 " AND tm_anno = " & nAnno.ToString & _
                 " AND tm_serie = " & CStrSQL(strSerie) & _
                 " ORDER BY tm_numdoc DESC"

        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          GetDatdocLastDoc = NTSCDate(dttTmp.Rows(0)!tm_datdoc)
          strTiporkOut = dttTmp.Rows(0)!tm_tipork.ToString
        End If
      Else
        'nuovo sistema: prendo su data e tipo documento del documento precedente e successivo di quello che sto facendo
        strSQL = "SELECT TOP 1 tm_datdoc, tm_tipork, tm_numdoc FROM " & IIf(strTipork = "W", "testprb", "testmag").ToString & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND tm_tipork IN (" & strElencoTipork & ")" & _
                  " AND tm_anno = " & nAnno & _
                  " AND tm_serie = " & CStrSQL(strSerie) & _
                  " AND tm_numdoc < " & lNumdoc & _
                  " ORDER BY tm_numdoc DESC"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          'documento precedente (numero più basso)
          GetDatdocLastDoc = NTSCDate(dttTmp.Rows(0)!tm_datdoc)
          strTiporkOut = NTSCStr(dttTmp.Rows(0)!tm_tipork)
        End If

        strSQL = strSQL.Replace(" AND tm_numdoc < ", " AND tm_numdoc > ")
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          'documento successivo (numero più alto)
          strDatdocOut1 = NTSCStr(dttTmp.Rows(0)!tm_datdoc)
          strTiporkOut1 = NTSCStr(dttTmp.Rows(0)!tm_tipork)
        End If
      End If

    Catch ex As Exception

      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function GetNregivaPerNumprot(ByVal strDitta As String, ByVal strSerie As String, ByVal nAnno As Integer) As Integer
    Try
      'obsoleta
      Return GetNregivaPerNumprot(strDitta, strSerie, nAnno, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetNregivaPerNumprot(ByVal strDitta As String, ByVal strSerie As String, _
                                                   ByVal nAnno As Integer, ByRef strAlfpro As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strSerie, nAnno, strAlfpro})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strAlfpro = NTSCStr(oIn(3))
        Return NTSCInt(oOut)
      End If
      '----------------

      strAlfpro = ""
      strSQL = "SELECT tb_numval, tb_alfpro FROM tabnuma" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_numtipo = 'L'" & " AND tb_numserie = " & CStrSQL(strSerie) & " AND tb_numcodl = " & nAnno
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strAlfpro = NTSCStr(dttTmp.Rows(0)!tb_alfpro)
        Return NTSCInt(dttTmp.Rows(0)!tb_numval)
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


  Public Overridable Function GetMovmag(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                       ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strMovmag As String = "movmag"

    Try
      '------------------------------------
      'se sono note di prelievo
      'If strTipoDoc = "W" Then
      '  strMovmagApri = strMovmagApri.Replace("movmag", "movprb")
      '  strMovmag = "movprb"
      'Else
      '  strMovmagApri = strMovmagApri.Replace("movprb", "movmag")
      'End If
      'strSQL = strMovmagApri
      'strSQL = strSQL & " WHERE " & strMovmag & ".codditt = " & CStrSQL(strDitta) & _
      '                  " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
      '                  " AND mm_anno = " & nAnno & _
      '                  " AND mm_serie = " & CStrSQL(strSerie) & _
      '                  " AND mm_numdoc = " & lNumdoc & _
      '                  " ORDER BY mm_riga ASC "
      strSQL = "bussp_bsveboll_aprimovmag " & _
              IIf(strTipoDoc = "W", 1, 0).ToString & " , " & _
              CStrSQL(strDitta) & ", " & _
              CStrSQL(strTipoDoc) & ", " & _
              nAnno & ", " & _
              CStrSQL(strSerie) & ", " & _
              lNumdoc.ToString & ", " & _
              "0"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      dttOut.TableName = "MOVMAG"

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetMovmagRiga(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                     ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                     ByVal lRiga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strMovmag As String = "movmag"
    Try
      If strTipoDoc = "W" Then strMovmag = "movprb"
      strSQL = "SELECT * FROM " & strMovmag & " WHERE codditt = " & CStrSQL(strDitta) & _
                        " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
                        " AND mm_anno = " & nAnno & _
                        " AND mm_serie = " & CStrSQL(strSerie) & _
                        " AND mm_numdoc = " & lNumdoc & _
                        " AND mm_riga = " & lRiga
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      dttOut.TableName = "MOVMAG"

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckScaricoMovcla(ByVal strDitta As String, ByVal strTipo As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                       ByVal lRiga As Integer, ByRef dttOut As DataTable) As Boolean
    'Controlla che non sia già usato su qualche documento di scarico (in MOVCLA)
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 mc_outipo, mc_ouanno, mc_ouserie, mc_ounum, mc_ouriga FROM movcla" & _
              " WHERE codditt = " & CStrSQL(strDitta) & " AND mc_intipo = " & CStrSQL(strTipo) & _
              " AND mc_inanno = " & nAnno & " AND mc_inserie = " & CStrSQL(strSerie) & _
              " AND mc_innum = " & lNumdoc & " AND mc_inriga = " & lRiga & _
              " AND (mc_outipo <> " & CStrSQL(strTipo) & " OR mc_ouanno <> " & nAnno & _
              " OR mc_ouserie <> " & CStrSQL(strSerie) & " OR mc_ounum <> " & lNumdoc & " OR mc_ouriga = " & lRiga & ")"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckScaricoMovcla1(ByVal strDitta As String, ByVal strTipo As String, _
                                     ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                     ByVal lRiga As Integer, ByRef dttOut As DataTable) As Boolean
    'Controlla se la riga è refenziata nei campi mm_cl* (ne rileva l'estremo)
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga FROM movmag " & _
               "WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mm_cltipo = " & CStrSQL(strTipo) & _
               " AND mm_clanno = " & nAnno & _
               " AND mm_clserie = " & CStrSQL(strSerie) & _
               " AND mm_clnum = " & lNumdoc & _
               " AND mm_clriga = " & lRiga & " " & _
               "AND (mm_tipork <> " & CStrSQL(strTipo) & _
               " OR mm_anno <> " & nAnno & " OR mm_serie <> " & CStrSQL(strSerie) & _
               " OR mm_numdoc <> " & lNumdoc & " OR mm_riga <> " & lRiga & ")"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckScaricoMovcla2(ByVal strDitta As String, ByVal strTipo As String, _
                                   ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                   ByVal lRiga As Integer, ByRef dttOut As DataTable) As Boolean
    '------------------------------------------
    'Controlla se la riga è refenziata nei campi mm_cc* (ne rileva l'estremo)
    'Solo se il documento è un carico di produzione
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga FROM movmag " & _
               "WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mm_cctipo = " & CStrSQL(strTipo) & _
               " AND mm_ccanno = " & nAnno & _
               " AND mm_ccserie = " & CStrSQL(strSerie) & _
               " AND mm_ccnum = " & lNumdoc & _
               " AND mm_ccriga = " & lRiga & " " & _
               "AND (mm_tipork <> " & CStrSQL(strTipo) & _
               " OR mm_anno <> " & nAnno & " OR mm_serie <> " & CStrSQL(strSerie) & _
               " OR mm_numdoc <> " & lNumdoc & " OR mm_riga <> " & lRiga & ")"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function AggMovcla1(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                        ByVal nAnno As Integer, ByVal strSerie As String, _
                                        ByVal lNumdoc As Integer, ByVal lRiga As Integer, _
                                        ByRef dttOut As DataTable) As Boolean
    'per ogni riga di documento (as esempio tipork 'B', ottengo l'elenco dei carichi da produzione 'T' e relative righe di scarico 'U'
    'raggruppate per riga di scarico)
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Min(movmagT.mm_quant) AS QuantT, movmagU.mm_codart AS CodartU, " & _
              " Sum(movmagU.mm_quant) AS SommaQuantU, Sum(movmagU.mm_colli) AS SommaColliU, " & _
              " Min(movmagT.mm_codart) AS CodartT" & _
              " FROM (movmag AS movmagU INNER JOIN movmag AS movmagT ON (movmagU.mm_prtipo = movmagT.mm_tipork) " & _
              " AND (movmagU.mm_pranno = movmagT.mm_anno) AND (movmagU.mm_prserie = movmagT.mm_serie) " & _
              " AND (movmagU.mm_prnum = movmagT.mm_numdoc) AND (movmagU.mm_prriga = movmagT.mm_riga) " & _
              " AND (movmagU.codditt = movmagT.codditt)) LEFT JOIN tabmaga AS tabmaga1 ON " & _
              " (movmagU.mm_magaz = tabmaga1.tb_codmaga) AND (movmagU.codditt = tabmaga1.codditt)" & _
              " WHERE movmagT.codditt = " & CStrSQL(strDitta) & _
              " AND movmagT.mm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND movmagT.mm_anno = " & nAnno.ToString & _
              " AND movmagT.mm_serie = " & CStrSQL(strSerie) & _
              " AND movmagT.mm_numdoc = " & lNumdoc.ToString & _
              " AND movmagT.mm_riga = " & lRiga.ToString & _
              " AND tabmaga1.tb_flclavo = 'C'" & _
              " AND movmagT.mm_fase = 0" & _
              " GROUP BY movmagT.mm_riga, movmagU.mm_codart " & _
              " ORDER BY movmagT.mm_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggMovcla2(ByVal strDitta As String, ByVal strTipoT As String, _
                                        ByVal nAnno As Integer, ByVal strSerie As String, _
                                        ByVal lNumdoc As Integer, ByVal lRiga As Integer, _
                                        ByVal strCodartU As String, ByRef dttOut As DataTable) As Boolean
    'Ora apre lo scarico di prod. per vedere in dettaglio i riferimenti
    'alle bolle di carico (mm_cl ...)
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mm_quant, mm_colli, mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga, mm_note " & _
              " FROM movmag " & _
              " WHERE codditt = " & CStrSQL(strDitta) & " " & _
              " AND mm_prtipo = '" & strTipoT & "' AND mm_pranno = " & nAnno & _
              " AND mm_prserie = '" & strSerie & "' AND mm_prnum = " & lNumdoc & _
              " AND mm_prriga = " & lRiga & " AND mm_codart = " & CStrSQL(strCodartU) & _
              " AND mm_clnum <> 0 " & _
              " ORDER BY mm_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggMovcla3(ByVal strDitta As String, ByVal strTipo As String, _
                                      ByVal nAnno As Integer, ByVal strSerie As String, _
                                      ByVal lNumdoc As Integer, ByVal strClTipo As String, _
                                      ByVal nClAnno As Integer, ByVal strClSerie As String, _
                                      ByVal lClNumdoc As Integer, ByVal lClRiga As Integer, _
                                      ByVal bNew As Boolean, ByRef dttOut As DataTable) As Boolean
    'Per ogni riga di scarico di prod. verifica la bolla di carico agganciata
    'Determina la parte già consumata sia sul residente (meno il doc. aperto)
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Sum(mc_inquant * mc_carscar) AS QuantCla" & _
              " FROM movcla" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mc_intipo = '" & strClTipo & "' AND mc_inanno = " & nClAnno & _
              " AND mc_inserie = '" & strClSerie & "' AND mc_innum = " & lClNumdoc & _
              " AND mc_inriga = " & lClRiga & " "
      If bNew = False Then
        strSQL += "AND NOT (mc_outipo = '" & strTipo & "' AND mc_ouanno = " & nAnno & _
                  " AND mc_ouserie = '" & strSerie & "' AND mc_ounum = " & lNumdoc & ") "
      End If
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggMovcla4(ByVal strDitta As String, ByVal strCodart As String, _
                                    ByVal nFase As Integer, ByVal lCommeca As Integer, _
                                    ByRef dttOut As DataTable) As Boolean
    'Per ogni riga di bolla/fattura apre i carichi di produzione precedenti
    '(stesso articolo/commessa) raggruppando per articolo/fase
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Sum(movmagT.mm_quant) AS QuantT, movmagT.mm_fase " & _
               " FROM (movmag AS movmagT INNER JOIN tabcaum ON movmagT.mm_causale = tabcaum.tb_codcaum) " & _
               " INNER JOIN tabmaga ON movmagT.codditt = tabmaga.codditt AND movmagT.mm_magaz = tabmaga.tb_codmaga " & _
               " WHERE movmagT.codditt = " & CStrSQL(strDitta) & " " & _
               " AND movmagT.mm_codart = " & CStrSQL(strCodart) & _
               " AND movmagT.mm_commeca = " & lCommeca & _
               " AND (tabmaga.tb_flclavo = 'C' OR tabmaga.tb_flclavo = 'X') AND tabcaum.tb_tipcaum = 'P' " & _
               " AND movmagT.mm_fase <= " & nFase & " " & _
               " GROUP BY movmagT.mm_fase " & _
               " ORDER BY movmagT.mm_fase "
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggMovcla5(ByVal strDitta As String, ByVal strTipo As String, _
                                  ByVal nAnno As Integer, ByVal strSerie As String, _
                                  ByVal lNumdoc As Integer, ByVal strCodart As String, _
                                  ByVal nFase As Integer, ByVal lCommeca As Integer, _
                                  ByVal bNew As Boolean, ByRef dttOut As DataTable) As Boolean
    'per controllare se i carichi di produzione sono sufficienti va tolto da
    'dtrM1!QuantT la quantità già 'rese' in precedenti documenti di reso
    'tenendo conto del fatto che un documento può essere aperto in modifica
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Sum(mm_quant) AS QtaGiaResa " & _
               "FROM (movmag INNER JOIN tabmaga ON movmag.codditt = tabmaga.codditt " & _
               "AND movmag.mm_magaz = tabmaga.tb_codmaga) INNER JOIN tabcaum ON " & _
               "movmag.mm_causale = tabcaum.tb_codcaum " & _
               "WHERE movmag.codditt = " & CStrSQL(strDitta) & " " & _
               "AND (tabmaga.tb_flclavo = 'C' OR tabmaga.tb_flclavo = 'X') AND tabcaum.tb_tipcaum = 'E' " & _
               "AND movmag.mm_codart = " & CStrSQL(strCodart) & " " & _
               "AND movmag.mm_fase = " & nFase & " " & _
               "AND movmag.mm_commeca = " & lCommeca & " "
      If bNew = False Then strSQL = strSQL & "AND NOT (mm_tipork = '" & strTipo & "' AND mm_anno = " & nAnno & _
                " AND mm_serie = '" & strSerie & "' AND mm_numdoc = " & lNumdoc & ") "
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggMovcla6(ByVal strDitta As String, ByVal strCodart As String, _
                                  ByVal nFase As Integer, ByVal lCommeca As Integer, _
                                  ByRef dttOut As DataTable) As Boolean
    'Ora determina il consumo dei figli (non a fasi) x ciascuna fase
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movmagU.mm_codart, Sum(movmagU.mm_quant) AS QuantU " & _
             "FROM (((movmag AS movmagU INNER JOIN movmag AS movmagT ON (movmagU.codditt = movmagT.codditt) " & _
             "AND (movmagU.mm_prtipo = movmagT.mm_tipork) AND (movmagU.mm_pranno = movmagT.mm_anno) " & _
             "AND (movmagU.mm_prserie = movmagT.mm_serie) AND (movmagU.mm_prnum = movmagT.mm_numdoc) " & _
             "AND (movmagU.mm_prriga = movmagT.mm_riga)) INNER JOIN tabmaga AS tabmaga1 ON " & _
             "movmagU.codditt = tabmaga1.codditt AND movmagU.mm_magaz = tabmaga1.tb_codmaga) " & _
             "INNER JOIN tabcaum ON movmagT.mm_causale = tabcaum.tb_codcaum) " & _
             "INNER JOIN tabmaga ON movmagT.codditt = tabmaga.codditt AND movmagT.mm_magaz = tabmaga.tb_codmaga " & _
             "WHERE movmagT.codditt = " & CStrSQL(strDitta) & " " & _
             "AND movmagT.mm_codart = " & CStrSQL(strCodart) & _
             " AND movmagT.mm_commeca = " & lCommeca.ToString & _
             " AND (tabmaga.tb_flclavo = 'C' OR tabmaga.tb_flclavo = 'X') AND tabcaum.tb_tipcaum = 'P' " & _
             " AND (tabmaga1.tb_flclavo = 'C' OR tabmaga1.tb_flclavo = 'X') " & _
             "AND movmagT.mm_fase = " & nFase.ToString & " AND movmagU.mm_fase = 0 " & _
             "GROUP BY movmagU.mm_codart " & _
             "ORDER BY movmagU.mm_codart "
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggMovcla7(ByVal strDitta As String, ByVal strTipo As String, _
                              ByVal nAnno As Integer, ByVal strSerie As String, _
                              ByVal lNumdoc As Integer, ByVal strCodart As String, _
                              ByVal lCommeca As Integer, _
                              ByVal strDatdoc As String, ByVal bAggMovClaFasiPerCommessa As Boolean, _
                              ByVal bNew As Boolean, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      Return AggMovcla7(strDitta, strTipo, nAnno, strSerie, lNumdoc, strCodart, _
                        lCommeca, strDatdoc, bAggMovClaFasiPerCommessa, bNew, dttOut, False, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function
  Public Overridable Function AggMovcla7(ByVal strDitta As String, ByVal strTipo As String, _
                                ByVal nAnno As Integer, ByVal strSerie As String, _
                                ByVal lNumdoc As Integer, ByVal strCodart As String, _
                                ByVal lCommeca As Integer, _
                                ByVal strDatdoc As String, ByVal bAggMovClaFasiPerCommessa As Boolean, _
                                ByVal bNew As Boolean, ByRef dttOut As DataTable, _
                                ByVal bAggMovClaFasiFiltroCliente As Boolean, ByVal lConto As Integer) As Boolean
    'Ora seleziona gli articoli 'aperti' in MOVCLA, scartando
    'l'eventuale documento aperto,
    'aggiunta INNER JOIN su MOVMAG per filtro sulla commessa
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipo, nAnno, strSerie, lNumdoc, strCodart, _
                                              lCommeca, strDatdoc, bAggMovClaFasiPerCommessa, bNew, dttOut, _
                                              bAggMovClaFasiFiltroCliente, lConto})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(10), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT mc_intipo, mc_inanno, mc_inserie, mc_innum, mc_inriga, " & _
         " Sum(mc_inquant * mc_carscar) AS QuantCar "
      If bAggMovClaFasiPerCommessa Then
        strSQL += "FROM testmag INNER JOIN (movcla INNER JOIN movmag ON (movcla.mc_inriga = movmag.mm_riga) " & _
                  " AND (movcla.mc_innum = movmag.mm_numdoc) AND (movcla.mc_inserie = movmag.mm_serie) " & _
                  " AND (movcla.mc_inanno = movmag.mm_anno) AND (movcla.mc_intipo = movmag.mm_tipork) " & _
                  " AND (movcla.codditt = movmag.codditt)) ON (testmag.codditt = movmag.codditt) " & _
                  " AND (testmag.tm_tipork = movmag.mm_tipork) AND (testmag.tm_serie = movmag.mm_serie) " & _
                  " AND (testmag.tm_anno = movmag.mm_anno) AND (testmag.tm_numdoc = movmag.mm_numdoc) "
      Else
        strSQL += "FROM movcla INNER JOIN testmag ON (movcla.codditt = testmag.codditt) AND " & _
                  " (movcla.mc_innum = testmag.tm_numdoc) AND (movcla.mc_inserie = testmag.tm_serie) " & _
                  " AND (movcla.mc_inanno = testmag.tm_anno) AND (movcla.mc_intipo = testmag.tm_tipork) "
      End If
      If bAggMovClaFasiFiltroCliente Then strSQL += "AND tm_conto = " & lConto & " "
      strSQL += "WHERE movcla.codditt = " & CStrSQL(strDitta) & " " & _
               "AND mc_incodart = " & CStrSQL(strCodart) & _
               " AND tm_datdoc <= " & CDataSQL(strDatdoc) & " "
      If bNew = False Then strSQL += "AND NOT (mc_outipo = '" & strTipo & _
                                     "' AND mc_ouanno = " & nAnno & _
                                     " AND mc_ouserie = '" & strSerie & _
                                     "' AND mc_ounum = " & lNumdoc & ") "
      If bAggMovClaFasiPerCommessa Then strSQL += "AND (mm_commeca = " & lCommeca.ToString & " OR mm_commeca = 0) "
      strSQL += "GROUP BY mc_intipo, mc_inanno, mc_inserie, mc_innum, mc_inriga "
      strSQL += "HAVING Sum(mc_inquant * mc_carscar) > 0.0001 "  '/* tolleranza */"
      If bAggMovClaFasiPerCommessa Then
        strSQL += "ORDER BY Min(mm_commeca) DESC, Min(testmag.tm_datdoc), mc_intipo, mc_inanno, mc_inserie, mc_innum, mc_inriga "
      Else
        strSQL += "ORDER BY Min(testmag.tm_datdoc), mc_intipo, mc_inanno, mc_inserie, mc_innum, mc_inriga "
      End If
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckAcconti(ByVal strDitta As String, ByVal bDocemesso As Boolean, ByVal lConto As Integer, _
                                          ByVal bScorp As Boolean, ByVal nValuta As Integer, ByVal lCommeca As Integer, _
                                          ByVal strDatdoc As String) As Boolean
    'Controlla se ci sono acconti in essere (solo per nuovi documenti)
    'restituisce true se ci sono acconti
    Dim strSQL As String = ""
    Dim bCommeca As Boolean = False
    Dim dttTmp As New DataTable
    Try
      bCommeca = CBool(GetSettingBusDitt(strDitta, "Bsveboll", "Opzioni", ".", "SelAccontiCommeca", "0", " ", "0"))
      'Controlla se ci sono acconti in essere
      If bDocemesso Then
        strSQL = " tm_conto = " & lConto.ToString & " AND (tm_tipork = 'A' OR tm_tipork = 'F' OR tm_tipork = 'S') AND " & _
                 " tm_salacceva = 'N' AND tm_totmerce >= tm_totacceva AND tb_flacconto = 'S' AND tm_datdoc <= " & _
                 CDataSQL(strDatdoc) & " "
      Else
        strSQL = " tm_conto = " & lConto.ToString & " AND tm_tipork = 'L' AND tm_salacceva = 'N' AND " & _
                 " tm_totmerce >= tm_totacceva AND tb_flacconto = 'S' AND tm_datdoc <= " & _
                 CDataSQL(strDatdoc) & " "
      End If
      If bCommeca And lCommeca <> 0 Then strSQL += " AND tm_commeca = " & lCommeca.ToString
      If bScorp Then
        'scorporo
        strSQL += " AND tm_scorpo = 'S' "
      Else
        If nValuta <> 0 Then
          'valuta
          strSQL += " AND tm_valuta = " & nValuta.ToString & " "
        Else
          'normale
          strSQL += " AND tm_scorpo = 'N' AND tm_valuta = 0 "
        End If
      End If
      'Controlla se c'è qualcosa
      strSQL = "SELECT top 1 tm_conto FROM testmag INNER JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND " & _
                " testmag.tm_tipobf = tabtpbf.tb_codtpbf" & _
                " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                " AND " & strSQL
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

  Public Overridable Function CheckAltriDocNelMese_SpeseIncSoloSuPrimaFatt( _
                                                   ByVal strDitta As String, ByVal lConto As Integer, _
                                                   ByVal strDatini As String, ByVal strDatfin As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT TOP 1 tm_tipork FROM testmag" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND tm_tipork = 'A'" & _
                " AND tm_conto = " & lConto & _
                " AND tm_datdoc BETWEEN " & CDataSQL(strDatini) & " AND " & CDataSQL(strDatfin)
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

  Public Overrides Function ChekEsistenzaFinale(ByVal strDitta As String, ByVal strCodart As String, _
                                                ByVal nFase As Integer, ByVal nMagaz As Integer, _
                                                ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      Return MyBase.ChekEsistenzaFinale(strDitta, strCodart, nFase, nMagaz, dttOut)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckCancelDocRiga_movcla(ByVal strDitta As String, ByVal strTipo As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                       ByVal lRiga As Integer, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      '------------------------
      'Controlla che non sia già usato su qualche documento di scarico (in MOVCLA)
      strSQL = "SELECT TOP 1 mc_outipo, mc_ouanno, mc_ouserie, mc_ounum, mc_ouriga FROM movcla" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mc_intipo = " & CStrSQL(strTipo) & _
              " AND mc_inanno = " & nAnno & _
              " AND mc_inserie = " & CStrSQL(strSerie) & _
              " AND mc_innum = " & lNumdoc & IIf(lRiga > 0, " AND mc_inriga = " & lRiga, "").ToString & _
              " AND (mc_outipo <> " & CStrSQL(strTipo) & _
                " OR mc_ouanno <> " & nAnno & _
                " OR mc_ouserie <> " & CStrSQL(strSerie) & _
                " OR mc_ounum <> " & lNumdoc & IIf(lRiga > 0, " OR mc_ouriga <> " & lRiga, "").ToString & ")"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckCancelDocRiga_movcla2(ByVal strDitta As String, ByVal strTipo As String, _
                                     ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                     ByVal lRiga As Integer, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      '------------------------
      'Controlla che non sia già usato su qualche documento di scarico (campo mm_cl*)
      strSQL = "SELECT TOP 1 mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga FROM movmag" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mm_cltipo = " & CStrSQL(strTipo) & _
              " AND mm_clanno = " & nAnno & _
              " AND mm_clserie = " & CStrSQL(strSerie) & _
              " AND mm_clnum = " & lNumdoc & IIf(lRiga > 0, " AND mm_clriga = " & lRiga, "").ToString & _
              " AND (mm_tipork <> " & CStrSQL(strTipo) & _
                " OR mm_anno <> " & nAnno & _
                " OR mm_serie <> " & CStrSQL(strSerie) & _
                " OR mm_numdoc <> " & lNumdoc & IIf(lRiga > 0, " OR mm_riga <> " & lRiga, "").ToString & ")"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckCancelDocRiga_movcla3(ByVal strDitta As String, ByVal strTipo As String, _
                                   ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                   ByVal lRiga As Integer, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      '------------------------
      'Se trattasi di una carico di produzione controlla che non sia già usato su qualche
      'documento (campo mm_cc*)
      strSQL = "SELECT TOP 1 mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga FROM movmag" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mm_cctipo = " & CStrSQL(strTipo) & _
              " AND mm_ccanno = " & nAnno & _
              " AND mm_ccserie = " & CStrSQL(strSerie) & _
              " AND mm_ccnum = " & lNumdoc & IIf(lRiga > 0, " AND mm_ccriga = " & lRiga, "").ToString & _
              " AND (mm_tipork <> " & CStrSQL(strTipo) & _
                " OR mm_anno <> " & nAnno & _
                " OR mm_serie <> " & CStrSQL(strSerie) & _
                " OR mm_numdoc <> " & lNumdoc & IIf(lRiga > 0, " OR mm_riga <> " & lRiga, "").ToString & ")"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TCOOrdiniCollegati(ByVal dtrRow As DataRow, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      With dtrRow
        strSQL = " SELECT mo_quant01, mo_quant02, mo_quant03, mo_quant04, mo_quant05, mo_quant06, mo_quant07, mo_quant08, mo_quant09, mo_quant10, mo_quant11, mo_quant12, mo_quant13, mo_quant14, mo_quant15, mo_quant16, mo_quant17, mo_quant18, mo_quant19, mo_quant20, mo_quant21, mo_quant22, mo_quant23, mo_quant24," & _
                 " mo_quapre01, mo_quapre02, mo_quapre03, mo_quapre04, mo_quapre05, mo_quapre06, mo_quapre07, mo_quapre08, mo_quapre09, mo_quapre10, mo_quapre11, mo_quapre12, mo_quapre13, mo_quapre14, mo_quapre15, mo_quapre16, mo_quapre17, mo_quapre18, mo_quapre19, mo_quapre20, mo_quapre21, mo_quapre22, mo_quapre23, mo_quapre24," & _
                 " mo_quaeva01, mo_quaeva02, mo_quaeva03, mo_quaeva04, mo_quaeva05, mo_quaeva06, mo_quaeva07, mo_quaeva08, mo_quaeva09, mo_quaeva10, mo_quaeva11, mo_quaeva12, mo_quaeva13, mo_quaeva14, mo_quaeva15, mo_quaeva16, mo_quaeva17, mo_quaeva18, mo_quaeva19, mo_quaeva20, mo_quaeva21, mo_quaeva22, mo_quaeva23, mo_quaeva24" & _
                 " FROM movordtc " & _
                 " WHERE codditt = " & CStrSQL(!codditt) & _
                 " AND mo_tipork = " & CStrSQL(!ec_ortipo) & _
                 " AND mo_anno = " & NTSCInt(!ec_oranno) & _
                 " AND mo_serie = " & CStrSQL(!ec_orserie) & _
                 " AND mo_numord = " & NTSCInt(!ec_ornum) & _
                 " AND mo_riga = " & NTSCInt(!ec_orriga)
      End With

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


#Region "APRI / SALVA / CANCELLA DOCUMENTO"
  Public Overridable Function ApriDocumento(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                       ByRef ds As DataSet, ByVal bModTCO As Boolean, ByVal bModPM As Boolean) As Boolean
    Return ApriDocumento(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, ds, bModTCO, bModPM, 0)
  End Function
  Public Overridable Function ApriDocumento(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                       ByRef ds As DataSet, ByVal bModTCO As Boolean, ByVal bModPM As Boolean, ByVal nOrdrig As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim strTestmag As String = "testmag"
    Dim strMovmag As String = "movmag"

    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, _
                                             ds, bModTCO, bModPM, nOrdrig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(6), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      '------------------------------------
      'se sono note di prelievo
      If strTipoDoc = "W" Then
        strTestmag = "testprb"
        strMovmag = "movprb"
      End If

      strSQL = "bussp_bsveboll_apritestmag " & _
                IIf(strTipoDoc = "W", 1, 0).ToString & " , " & _
                CStrSQL(strDitta) & ", " & _
                CStrSQL(strTipoDoc) & ", " & _
                nAnno & ", " & _
                CStrSQL(strSerie) & ", " & _
                IIf(bNew, "-1", lNumdoc).ToString
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTMAG")

      'aggiungo le colonne di descrizione non presenti nella stored procedure
      ds.Tables("TESTMAG").Columns.Add("xx_codmodi", GetType(String))
      ds.Tables("TESTMAG").Columns.Add("xx_coderog", GetType(String))
      ds.Tables("TESTMAG").Columns.Add("xx_totpunti", GetType(Integer))

      If ds.Tables("TESTMAG").Rows.Count > 0 Then
        If NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_contfatt) <> 0 Then
          strSQL = "SELECT an_descr1 FROM anagra WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_contfatt).ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then ds.Tables("TESTMAG").Rows(0)!xx_contfatt = NTSCStr(dttTmp.Rows(0)!an_descr1)
          dttTmp.Clear()
        End If

        If NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_codcli) <> 0 Then
          strSQL = "SELECT an_descr1 FROM anagra WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_codcli).ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then ds.Tables("TESTMAG").Rows(0)!xx_codcli = NTSCStr(dttTmp.Rows(0)!an_descr1)
          dttTmp.Clear()
        End If

        If NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_controp) <> 0 Then
          strSQL = "SELECT an_descr1 " & _
                   " FROM tabcove INNER JOIN anagra ON tabcove.codditt = anagra.codditt " & _
                   " AND tabcove.tb_concove = anagra.an_conto " & _
                   " WHERE tabcove.codditt = " & CStrSQL(strDitta) & _
                   " AND tb_codcove = " & NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_controp).ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then ds.Tables("TESTMAG").Rows(0)!xx_controp = NTSCStr(dttTmp.Rows(0)!an_descr1)
          dttTmp.Clear()
        End If

        'intrastat servizi: modalità di erogazione e modalità di incasso
        If NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_coderog) <> 0 Then
          strSQL = "SELECT tb_deserog " & _
                   " FROM taberog WHERE tb_coderog = " & CStrSQL(ds.Tables("TESTMAG").Rows(0)!tm_coderog)
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then ds.Tables("TESTMAG").Rows(0)!xx_coderog = NTSCStr(dttTmp.Rows(0)!tb_deserog)
          dttTmp.Clear()
        End If
        If NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_codmodi) <> 0 Then
          strSQL = "SELECT tb_desmodi " & _
                   " FROM tabmodi WHERE tb_codmodi = " & CStrSQL(ds.Tables("TESTMAG").Rows(0)!tm_codmodi)
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then ds.Tables("TESTMAG").Rows(0)!xx_codmodi = NTSCStr(dttTmp.Rows(0)!tb_desmodi)
          dttTmp.Clear()
        End If
        '------------------------------------------------------------------------------------------------------------
        If NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_coddest) <> 0 Then
          With ds.Tables("TESTMAG").Rows(0)
            strSQL = "SELECT * FROM destdiv" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND dd_conto = " & NTSCInt(!tm_conto).ToString & _
              " AND dd_coddest = " & NTSCInt(!tm_coddest).ToString
          End With
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then
            strSQL = ""
            With dttTmp.Rows(0)
              strSQL = NTSCStr(!dd_nomdest).Trim & _
                IIf(NTSCStr(!dd_inddest).Trim <> "", " - " & NTSCStr(!dd_inddest).Trim, "").ToString & _
                IIf(NTSCStr(!dd_locdest).Trim <> "", " - " & NTSCStr(!dd_locdest).Trim, "").ToString
            End With
            If strSQL.Trim <> "" Then ds.Tables("TESTMAG").Rows(0)!xx_coddest = strSQL
          End If
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        If NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_coddest2) <> 0 Then
          With ds.Tables("TESTMAG").Rows(0)
            strSQL = "SELECT * FROM destdiv" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND dd_conto = " & NTSCInt(!tm_conto).ToString & _
              " AND dd_coddest = " & NTSCInt(!tm_coddest2).ToString
          End With
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then
            strSQL = ""
            With dttTmp.Rows(0)
              strSQL = NTSCStr(!dd_nomdest).Trim & _
                IIf(NTSCStr(!dd_inddest).Trim <> "", " - " & NTSCStr(!dd_inddest).Trim, "").ToString & _
                IIf(NTSCStr(!dd_locdest).Trim <> "", " - " & NTSCStr(!dd_locdest).Trim, "").ToString
            End With
            If strSQL.Trim <> "" Then ds.Tables("TESTMAG").Rows(0)!xx_coddest2 = strSQL
          End If
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        '------------------------------------------------------------------------------------------------------------
      End If    'If ds.Tables("TESTMAG").Rows.Count > 0 Then
      
      'testmagc
      strSQL = "SELECT * FROM " & strTestmag & "c" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND tm_anno = " & nAnno & _
              " AND tm_serie = " & CStrSQL(strSerie)
      If bNew Then
        strSQL = strSQL & " AND tm_numdoc = -1"
      Else
        strSQL = strSQL & " AND tm_numdoc = " & lNumdoc
      End If
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTMAGC", ds)

      'Aggiunge le colonne xx per la gestione delle promozioni
      For z As Integer = 1 To 6
        Dim strPost As String = z.ToString.Trim("1"c)
        ds.Tables("TESTMAGC").Columns.Add("xx_codtes" & strPost, GetType(String))
      Next

      If ds.Tables("TESTMAGC").Rows.Count > 0 Then
        For z As Integer = 1 To 6
          Dim strPost As String = z.ToString.Trim("1"c)

          If NTSCInt(ds.Tables("TESTMAGC").Rows(0)("tm_codtes" & strPost)) <> 0 Then
            strSQL = "SELECT ret_destes FROM refites " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     "   AND ret_codtes = " & NTSCStr(ds.Tables("TESTMAGC").Rows(0)("tm_codtes" & strPost))
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            If dttTmp.Rows.Count > 0 Then ds.Tables("TESTMAGC").Rows(0)("xx_codtes" & strPost) = NTSCStr(dttTmp.Rows(0)!ret_destes)
            dttTmp.Clear()
          End If
        Next
      End If

      '------------------------------------
      'movmag (occhio: aggiungo la tabella ad dataset precedentemente creato con testmag)
      strSQL = "bussp_bsveboll_aprimovmag " & _
          IIf(strTipoDoc = "W", 1, 0).ToString & " , " & _
          CStrSQL(strDitta) & ", " & _
          CStrSQL(strTipoDoc) & ", " & _
          nAnno & ", " & _
          CStrSQL(strSerie) & ", " & _
          IIf(bNew, "-1", lNumdoc).ToString & ", " & _
          nOrdrig.ToString
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVMAG", ds)

      '--------------------------------------
      If Not ApriDocumentoProd(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, ds, bModTCO, bModPM) Then Return False

      ds.Tables("MOVMAG").Columns.Add("xxo_valoreiva", GetType(Decimal))

      If ds.Tables("MOVMAG").Rows.Count > 0 Then
        '------------------------------------
        'caso della fattura proforma:
        'se la fattura proforma ha delle righe collegate ad ordini, al saltaggio per non far evadere l'ordine sposto mm_ornum in mm_commen
        'ora per rifar vedere i riferimenti all'ordine risposto mm_commen in mm_ornum
        'al salvataggio rispostero mm_ornum in mm_commen
        If ds.Tables("TESTMAG").Rows(0)!tm_proforma.ToString = "S" And strTipoDoc = "A" Then
          dtrT = ds.Tables("MOVMAG").Select("mm_commen <> 0")
          For i = 0 To dtrT.Length - 1
            dtrT(i)!mm_ornum = dtrT(i)!mm_commen
          Next
        End If

        '------------------------------------
        'Carica il TimeStamp corrente delle righe in MOVORD/MOVPRB
        If Not ApriDocumentoCaricaTimestamp(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc, ds, strMovmag) Then Return False

        '----------------------
        'aggiorno xxo_codarfo e xxo_valrigaic
        strSQL = "SELECT caf_codart, caf_codarfo FROM codarfo WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND caf_conto = " & ds.Tables("TESTMAG").Rows(0)!tm_conto.ToString & _
                 " AND caf_codart IN (SELECT mm_codart FROM " & strMovmag & _
                 " WHERE " & strMovmag & ".codditt = " & CStrSQL(strDitta) & _
                 " AND " & strMovmag & ".mm_tipork = " & CStrSQL(strTipoDoc) & _
                 " AND " & strMovmag & ".mm_anno = " & nAnno & _
                 " AND " & strMovmag & ".mm_serie = " & CStrSQL(strSerie) & _
                 " GROUP BY mm_codart)"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          For i = 0 To ds.Tables("MOVMAG").Rows.Count - 1
            dtrT = dttTmp.Select("caf_codart = " & CStrSQL(ds.Tables("MOVMAG").Rows(i)!mm_codart))
            If dtrT.Length > 0 Then
              ds.Tables("MOVMAG").Rows(i)!xxo_codarfo = NTSCStr(dtrT(0)!caf_codarfo)
            End If
          Next
        End If
        dttTmp.Clear()

        If ds.Tables("TESTMAG").Rows(0)!tm_scorpo.ToString = "S" Then
          For Each dtrEC As DataRow In ds.Tables("MOVMAG").Rows
            dtrEC!xxo_valrigaic = ArrDbl(ArrDbl(NTSCDec(dtrEC!mm_preziva) * NTSCDec(dtrEC!mm_quant) / _
                                  NTSCDec(dtrEC!mm_perqta) * (100 - NTSCDec(dtrEC!mm_scont1)) / 100 * _
                                  (100 - NTSCDec(dtrEC!mm_scont2)) / 100 * _
                                  (100 - NTSCDec(dtrEC!mm_scont3)) / 100 * _
                                  (100 - NTSCDec(dtrEC!mm_scont4)) / 100 * _
                                  (100 - NTSCDec(dtrEC!mm_scont5)) / 100 * _
                                  (100 - NTSCDec(dtrEC!mm_scont6)) / 100 * _
                                  (100 - NTSCDec(dtrEC!mm_scontp)) / 100 - NTSCDec(dtrEC!mm_scontv), TrovaNdec(0)) * _
                                  (100 - NTSCDec(ds.Tables("TESTMAG").Rows(0)!tm_scont1)) / 100 * _
                                  (100 - NTSCDec(ds.Tables("TESTMAG").Rows(0)!tm_scont2)) / 100 * _
                                  (100 - NTSCDec(ds.Tables("TESTMAG").Rows(0)!tm_scopag)) / 100, TrovaNdec(0))
          Next
        End If

      End If    'If ds.Tables("MOVMAG").Rows.Count > 0 Then

      '----------------------
      'movmatr (tira su anche gli 'U')
      strSQL = "SELECT * FROM movmatr" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND (mma_tipork = " & CStrSQL(strTipoDoc) & IIf(strTipoDoc = "T", " OR mma_tipork = 'U')", ")").ToString & _
              " AND mma_anno = " & nAnno & _
              " AND mma_serie = " & CStrSQL(strSerie) & _
              " AND mma_numdoc = " & IIf(bNew, -1, lNumdoc).ToString & _
              " ORDER BY mma_riga ASC, mma_rigaa ASC "
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVMATR", ds)

      '----------------------
      'movcla
      strSQL = "SELECT * FROM movcla" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mc_outipo = " & CStrSQL(strTipoDoc) & _
              " AND mc_ouanno = " & nAnno & _
              " AND mc_ouserie = " & CStrSQL(strSerie) & _
              " AND mc_ounum = " & IIf(bNew, -1, lNumdoc).ToString
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVCLA", ds)

      '----------------------
      'movcona
      strSQL = "SELECT movcona.*, ar_descr as xx_codart, ar_unmis as xx_unmis " & _
              " FROM movcona LEFT JOIN artico ON movcona.codditt = artico.codditt AND movcona.mma_codart = artico.ar_codart " & _
              " WHERE movcona.codditt = " & CStrSQL(strDitta) & _
              " AND mma_tipork = " & CStrSQL(strTipoDoc) & _
              " AND mma_anno = " & nAnno & _
              " AND mma_serie = " & CStrSQL(strSerie) & _
              " AND mma_numdoc = " & IIf(bNew, -1, lNumdoc).ToString & _
              " ORDER BY mma_riga ASC, mma_rigaa ASC "
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVCONA", ds)

      '----------------------
      If Not ApriDocumentoTCO(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, ds, bModTCO, bModPM) Then Return False

      If bModPM Or lNumdoc = 0 Then 'per evitare i problemi, in form_load riempio il datatable ugualmente ... non si sa mai dopo 
        strSQL = "SELECT * FROM proesec WHERE proesec.codditt = " & CStrSQL(strDitta) & " AND pes_commeca = -1"
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PROESEC", ds)
      End If

      '------------------------------------
      'schetrasp
      strSQL = "SELECT schetrasp.*, '' as xx_vettcod, '' as xx_commstat," & _
              " '' as xx_caristat, '' as xx_propstat," & _
              " '' as xx_lcstat, '' as xx_lsstat," & _
              " '' as xx_datcomp, '' as xx_riferim" & _
              " FROM schetrasp " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND sct_tipork = " & CStrSQL(strTipoDoc) & _
              " AND sct_anno = " & nAnno & _
              " AND sct_serie = " & CStrSQL(strSerie)
      If bNew Then
        strSQL = strSQL & " AND sct_numdoc = -1"
      Else
        strSQL = strSQL & " AND sct_numdoc = " & lNumdoc
      End If
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SCHETRASP", ds)

      ds.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ApriDocumentoGetOrdSalcon(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                                    ByVal lNumdoc As Integer, ByRef dttOrdOut As DataTable, _
                                                    ByRef dttNotaOut As DataTable, ByRef dttLavcentOut As DataTable) As Boolean
    '-----------------------------
    'con una join tra movmag e movord preleva le mm_orsalcon di movmag con valore diversi da or_flevas di movord
    Dim strSQL As String = ""
    Dim strMovmag As String = "movmag"

    Try
      '------------------------------------
      'se sono note di prelievo
      If strTipoDoc = "W" Then
        strMovmag = "movprb"
      End If

      'ordine
      strSQL = "SELECT " & strMovmag & ".mm_riga, " & strMovmag & ".mm_salcon, MOVORD.mo_flevas " & _
               " FROM " & strMovmag & " INNER JOIN MOVORD ON " & strMovmag & ".codditt = MOVORD.codditt " & _
               " AND " & strMovmag & ".mm_orriga = MOVORD.mo_riga AND " & strMovmag & ".mm_ornum = MOVORD.mo_numord " & _
               " AND " & strMovmag & ".mm_orserie = MOVORD.mo_serie AND " & strMovmag & ".mm_oranno = MOVORD.mo_anno " & _
               " AND " & strMovmag & ".mm_ortipo = MOVORD.mo_tipork " & _
               " WHERE " & strMovmag & ".codditt = " & CStrSQL(strDitta) & _
               " AND " & strMovmag & ".mm_tipork = " & CStrSQL(strTipoDoc) & _
               " AND " & strMovmag & ".mm_anno = " & nAnno & _
               " AND " & strMovmag & ".mm_serie = " & CStrSQL(strSerie) & _
               " AND " & strMovmag & ".mm_numdoc = " & lNumdoc & _
               " AND " & strMovmag & ".mm_salcon <> MOVORD.mo_flevas " & _
               " GROUP BY " & strMovmag & ".mm_riga, " & strMovmag & ".mm_salcon, MOVORD.mo_flevas "
      dttOrdOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'nota di prelievo
      If strTipoDoc <> "W" Then
        strSQL = "SELECT MOVMAG.mm_riga, MOVMAG.mm_salcon, movprb.mm_salcon as xx_salcon" & _
                 " FROM MOVMAG INNER JOIN movprb ON MOVMAG.codditt = movprb.codditt " & _
                 " AND MOVMAG.mm_npriga = movprb.mm_riga AND MOVMAG.mm_npnum = movprb.mm_numdoc " & _
                 " AND MOVMAG.mm_npserie = movprb.mm_serie AND MOVMAG.mm_npanno = movprb.mm_anno " & _
                 " AND MOVMAG.mm_nptipo = movprb.mm_tipork " & _
                 " WHERE MOVMAG.codditt = " & CStrSQL(strDitta) & _
                 " AND MOVMAG.mm_tipork = " & CStrSQL(strTipoDoc) & _
                 " AND MOVMAG.mm_anno = " & nAnno & _
                 " AND MOVMAG.mm_serie = " & CStrSQL(strSerie) & _
                 " AND MOVMAG.mm_numdoc = " & lNumdoc & _
                 " AND MOVMAG.mm_salcon <> movprb.mm_salcon " & _
                 " GROUP BY MOVMAG.mm_riga, MOVMAG.mm_salcon, movprb.mm_salcon "
        dttNotaOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      'lavorazioni collegate
      If strTipoDoc = "T" Then
        strSQL = "SELECT lavcent.lce_riga, lavcent.lce_rigaa, lavcent.lce_flevas, attivit.at_flevas " & _
                 " FROM lavcent INNER JOIN attivit ON (lavcent.codditt = attivit.codditt) AND " & _
                 " (lavcent.lce_orfase = attivit.at_fase) AND (lavcent.lce_orriga = attivit.at_riga) AND " & _
                 " (lavcent.lce_ornum = attivit.at_numord) AND (lavcent.lce_orserie = attivit.at_serie) AND " & _
                 " (lavcent.lce_oranno = attivit.at_anno) AND (lavcent.lce_ortipo = attivit.at_tipork) " & _
                 " WHERE lavcent.codditt = " & CStrSQL(strDitta) & _
                 " AND lavcent.lce_tipork = " & CStrSQL(strTipoDoc) & _
                 " AND lavcent.lce_anno = " & nAnno & _
                 " AND lavcent.lce_serie = " & CStrSQL(strSerie) & _
                 " AND lavcent.lce_numdoc = " & lNumdoc & _
                 " AND lavcent.lce_flevas <> attivit.at_flevas " & _
                 " GROUP BY lavcent.lce_riga, lavcent.lce_rigaa, lavcent.lce_flevas, attivit.at_flevas "
        dttLavcentOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ApriDocumentoProd(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                       ByRef ds As DataSet, ByVal bModTCO As Boolean, ByVal bModPM As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strTestmag As String = "testmag"
    Dim strMovmag As String = "movmag"
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing

    Try
      '------------------------------------
      'se sono note di prelievo
      If strTipoDoc = "W" Then
        strTestmag = "testprb"
        strMovmag = "movprb"
      End If

      '------------------------------------
      'scarichi collegati: testmag è identico a testmag Documento di produzione. 
      'movmatr è stato caricato prima insieme a quello delle righe 'T'

      '------------------------------------
      'strSQL = strMovmagApri
      'strSQL = strSQL & " WHERE " & strMovmag & ".codditt = " & CStrSQL(strDitta) & _
      '            " AND mm_tipork = " & CStrSQL(IIf(strTipoDoc = "T", "U", ".")) & _
      '            " AND mm_anno = " & nAnno & _
      '            " AND mm_serie = " & CStrSQL(strSerie)
      'If bNew Or strTipoDoc <> "T" Then
      '  strSQL = strSQL & " AND mm_numdoc = -1"
      'Else
      '  strSQL = strSQL & " AND mm_numdoc = " & lNumdoc
      'End If
      'strSQL += " ORDER BY mm_riga ASC "
      strSQL = "bussp_bsveboll_aprimovmag " & _
              "0 , " & _
              CStrSQL(strDitta) & ", " & _
              CStrSQL(IIf(strTipoDoc = "T", "U", ".")) & ", " & _
              nAnno & ", " & _
              CStrSQL(strSerie) & ", " & _
              IIf(bNew Or strTipoDoc <> "T", -1, lNumdoc).ToString & ", " & _
              "0"
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVMAGIMP", ds)

      '------------------------------------
      'Carica il TimeStamp corrente delle righe in MOVORD/MOVPRB
      If Not ApriDocumentoCaricaTimestamp(strDitta, "U", nAnno, strSerie, lNumdoc, ds, strMovmag) Then Return False

      '------------------------------------
      'carico lavcent
      strSQL = "SELECT lavcent.*, tb_deslavo as xx_codlavo, " & _
               " 0.0 as xx_tempatte, 0.0 as xx_tempese, " & _
               " 0.0 as xx_tempattem, 0.0 as xx_tempesem, " & _
               " tb_magterz as xx_magterz, tb_descent as xx_codcent, " & _
               " tb_descove as xx_controp, tb_descena as xx_codcena, co_descr1 as xx_commeca, " & _
               " tb_descfam as xx_codcfam, ar_descr as xx_codart, tb_descope as xx_codcope " & _
               " FROM (((((((lavcent LEFT JOIN tablavo ON lavcent.codditt = tablavo.codditt AND lavcent.lce_codlavo = tablavo.tb_codlavo) " & _
               " LEFT JOIN tabcent ON lavcent.codditt = tabcent.codditt AND lavcent.lce_codcent = tabcent.tb_codcent) " & _
               " LEFT JOIN tabcove ON lavcent.codditt = tabcove.codditt AND lavcent.lce_controp = tabcove.tb_codcove) " & _
               " LEFT JOIN tabcena ON lavcent.codditt = tabcena.codditt AND lavcent.lce_codcena = tabcena.tb_codcena) " & _
               " LEFT JOIN tabcfam ON lavcent.codditt = tabcfam.codditt AND lavcent.lce_codcfam = tabcfam.tb_codcfam) " & _
               " LEFT JOIN artico ON lavcent.codditt = artico.codditt AND lavcent.lce_codart = artico.ar_codart) " & _
               " LEFT JOIN tabcope ON lavcent.codditt = tabcope.codditt AND lavcent.lce_codcope = tabcope.tb_codcope) " & _
               " LEFT JOIN commess ON lavcent.codditt = commess.codditt AND lavcent.lce_commeca = commess.co_comme"
      strSQL += " WHERE lavcent.codditt = " & CStrSQL(strDitta) & _
                " AND lce_tipork = 'T' AND lce_anno = " & nAnno & _
                " AND lce_serie = " & CStrSQL(strSerie)
      If bNew Or strTipoDoc <> "T" Then
        strSQL = strSQL & " AND lce_numdoc = -1"
      Else
        strSQL = strSQL & " AND lce_numdoc = " & lNumdoc
      End If
      strSQL += " ORDER BY lce_riga ASC, lce_rigaa ASC "
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LAVCENT", ds)
      For Each dtrT1 As DataRow In ds.Tables("LAVCENT").Rows
        dtrT1!xx_tempatte = ConvOra100Ora60(NTSCDec(dtrT1!lce_tempatte))
        dtrT1!xx_tempese = ConvOra100Ora60(NTSCDec(dtrT1!lce_tempese))
        dtrT1!xx_tempattem = ConvOra60Minuti(NTSCDec(dtrT1!xx_tempatte))
        dtrT1!xx_tempesem = ConvOra60Minuti(NTSCDec(dtrT1!xx_tempese))
      Next

      '--------------------------
      'Aggiorna il flag di evaso
      strSQL = "SELECT lavcent.lce_riga, lavcent.lce_rigaa, at_flevas " & _
              " FROM lavcent INNER JOIN attivit ON (lavcent.codditt = attivit.codditt) AND " & _
              " (lavcent.lce_orfase = attivit.at_fase) AND (lavcent.lce_orriga = attivit.at_riga) AND " & _
              " (lavcent.lce_ornum = attivit.at_numord) AND (lavcent.lce_orserie = attivit.at_serie) AND " & _
              " (lavcent.lce_oranno = attivit.at_anno) AND (lavcent.lce_ortipo = attivit.at_tipork) " & _
              " WHERE lavcent.codditt = " & CStrSQL(strDitta) & _
              " AND lce_tipork = 'T' AND lce_anno = " & nAnno & _
              " AND lce_serie = " & CStrSQL(strSerie) & _
              " AND lce_numdoc = " & lNumdoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For Each dtrTmp As DataRow In dttTmp.Rows
        dtrT = ds.Tables("LAVCENT").Select("lce_riga = " & dtrTmp!lce_riga.ToString & " AND lce_rigaa = " & dtrTmp!lce_rigaa.ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i)!lce_flevas = dtrTmp!at_flevas
        Next
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ApriDocumentoCaricaTimestamp(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                       ByRef ds As DataSet, ByVal strMovmag As String) As Boolean
    Dim strSQL As String = ""
    Dim strTable As String = "MOVMAG"
    Dim dttTmp As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strNumord As String = "mm_ornum"
    Dim strKeymag As String = "keymag"

    Try
      If strTipoDoc = "U" Then
        strTable = "MOVMAGIMP"
        strMovmag = "movmag"
      End If
      If strTipoDoc = "W" Then strKeymag = "keyprb"

      If ds.Tables("TESTMAG").Rows.Count > 0 Then
        If NTSCStr(ds.Tables("TESTMAG").Rows(0)!tm_proforma).ToUpper = "S" Then
          'se la fattura è proforma il numero d'ordine è stato spostato in ec_commen
          strNumord = "mm_commen"
        End If
      Else
        'il documento in modifica non è stato trovaoto. più avanti darò il messaggio di documento inesistente
        Return True
      End If

      If ds.Tables("MOVMAG").Rows.Count > 0 Then
        'se sono in 'nuovo da import export non devo fare nulla, altrimenti se continuo in questa routine riporto in mm_salcon lo stato dell'ordine che risulta ancora aperto)
        strSQL = "SELECT top 1 km_riga FROM " & strKeymag & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND km_tipork = " & CStrSQL(strTipoDoc) & _
                " AND km_anno = " & nAnno & _
                " AND km_serie = " & CStrSQL(strSerie) & _
                " AND km_numdoc = " & lNumdoc
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count = 0 Then Return True
      End If

      '------------------------------------
      'Carica il TimeStamp corrente delle righe in MOVORD/MOVPRB
      If strTipoDoc <> "W" Then
        strSQL = "SELECT " & strMovmag & ".mm_riga, movprb.mm_ultagg, movprb.mm_nprflevas " & _
                " FROM " & strMovmag & " INNER JOIN movprb ON (" & strMovmag & ".codditt = movprb.codditt) AND (" & _
                strMovmag & ".mm_npriga = movprb.mm_riga) AND (" & strMovmag & ".mm_npnum = movprb.mm_numdoc) AND (" & _
                strMovmag & ".mm_npserie = movprb.mm_serie) AND (" & strMovmag & ".mm_npanno = movprb.mm_anno) AND (" & _
                strMovmag & ".mm_nptipo = movprb.mm_tipork) " & _
                " WHERE " & strMovmag & ".codditt = " & CStrSQL(strDitta) & _
                " AND " & strMovmag & ".mm_tipork = " & CStrSQL(strTipoDoc) & _
                " AND " & strMovmag & ".mm_anno = " & nAnno & _
                " AND " & strMovmag & ".mm_serie = " & CStrSQL(strSerie) & _
                " AND " & strMovmag & ".mm_numdoc = " & lNumdoc
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        For Each dtrTmp As DataRow In dttTmp.Rows
          dtrT = ds.Tables(strTable).Select("mm_riga = " & dtrTmp!mm_riga.ToString)
          For i = 0 To dtrT.Length - 1
            dtrT(i)!xxo_npultagg = dtrTmp!mm_ultagg
            dtrT(i)!mm_npsalcon = dtrTmp!mm_nprflevas
          Next
        Next
      End If

      strSQL = "SELECT " & strMovmag & ".mm_riga, mo_ultagg, mo_flevapre, mo_flevas " & _
              " FROM " & strMovmag & " INNER JOIN movord ON (" & strMovmag & ".codditt = movord.codditt) AND (" & _
              strMovmag & ".mm_orriga = movord.mo_riga) AND (" & strMovmag & "." & strNumord & " = movord.mo_numord) AND (" & _
              strMovmag & ".mm_orserie = movord.mo_serie) AND (" & strMovmag & ".mm_oranno = movord.mo_anno) AND (" & _
              strMovmag & ".mm_ortipo = movord.mo_tipork) " & _
              " WHERE " & strMovmag & ".codditt = " & CStrSQL(strDitta) & _
              " AND " & strMovmag & ".mm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND " & strMovmag & ".mm_anno = " & nAnno & _
              " AND " & strMovmag & ".mm_serie = " & CStrSQL(strSerie) & _
              " AND " & strMovmag & ".mm_numdoc = " & lNumdoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For Each dtrTmp As DataRow In dttTmp.Rows
        dtrT = ds.Tables(strTable).Select("mm_riga = " & dtrTmp!mm_riga.ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i)!xxo_orultagg = dtrTmp!mo_ultagg
          If strTipoDoc = "W" Then
            dtrT(i)!mm_salcon = dtrTmp!mo_flevapre.ToString
          Else
            If strNumord = "mm_commen" Then
              'se è un documento proforma non devo aggiornare flevas. l'ordine non risulta essere evaso...
            Else
              dtrT(i)!mm_salcon = dtrTmp!mo_flevas.ToString
            End If
          End If
        Next
      Next

      If strTipoDoc = "I" Then
        'Carica il TimeStamp corrente della RF riemessa
        strSQL = "SELECT " & strMovmag & ".mm_riga, testmag.tm_ultagg " & _
              " FROM " & strMovmag & " INNER JOIN testmag ON (" & strMovmag & ".codditt = testmag.codditt) AND (" & _
              strMovmag & ".mm_prnum = testmag.tm_numdoc) AND (" & strMovmag & ".mm_prserie = testmag.tm_serie) AND (" & _
              strMovmag & ".mm_pranno = testmag.tm_anno) AND (" & strMovmag & ".mm_prtipo = testmag.tm_tipork) " & _
              " WHERE " & strMovmag & ".codditt = " & CStrSQL(strDitta) & _
              " AND " & strMovmag & ".mm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND " & strMovmag & ".mm_anno = " & nAnno & _
              " AND " & strMovmag & ".mm_serie = " & CStrSQL(strSerie) & _
              " AND " & strMovmag & ".mm_numdoc = " & lNumdoc
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        For Each dtrTmp As DataRow In dttTmp.Rows
          dtrT = ds.Tables("MOVMAG").Select("mm_riga = " & dtrTmp!mm_riga.ToString)
          For i = 0 To dtrT.Length - 1
            dtrT(i)!xxo_orultagg = dtrTmp!tm_ultagg
          Next
        Next
      End If    'If strTipoDoc = "I" Then



      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ApriDocumentoTCO(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, _
                                     ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                     ByRef ds As DataSet, ByVal bModTCO As Boolean, ByVal bModPM As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strTestmag As String = "testmag"
    Dim strMovmag As String = "movmag"
    Dim nTcIndTaglF As Integer
    Dim nRiga As Integer
    Dim dtrRow2 As DataRow()
    Dim dttDestagl As New DataTable
    Try
      If strTipoDoc = "W" Then
        strTestmag = "testmag"
        strMovmag = "movmag"
      End If

      '-----------------------------------
      If bModTCO Or lNumdoc = 0 Then 'per evitare i problemi, in form_load riempio il datatable ugualmente ... non si sa mai dopo 
        strSQL = "SELECT * FROM " & strMovmag & "tc"
        strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                          " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
                          " AND mm_anno = " & nAnno & _
                          " AND mm_serie = " & CStrSQL(strSerie)
        If bNew Then
          strSQL = strSQL & " AND mm_numdoc = -1"
        Else
          strSQL = strSQL & " AND mm_numdoc = " & lNumdoc
        End If
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVMAGTC", ds)

        strSQL = "SELECT * FROM " & strMovmag & "tc"
        strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND mm_tipork = " & CStrSQL(IIf(strTipoDoc = "T", "U", ".")) & _
                    " AND mm_anno = " & nAnno & _
                    " AND mm_serie = " & CStrSQL(strSerie)
        If bNew Or strTipoDoc <> "T" Then
          strSQL = strSQL & " AND mm_numdoc = -1"
        Else
          strSQL = strSQL & " AND mm_numdoc = " & lNumdoc
        End If
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVMAGIMPTC", ds)

        If bModTCO Then 'Riempimento campo di MOVMAGIMP
          'xxo_tcindtaglf
          nTcIndTaglF = 0
          For Each dtrRow As DataRow In ds.Tables("MOVMAGIMPTC").Rows
            nRiga = NTSCInt(dtrRow!mm_riga)
            If NTSCDec(dtrRow!mm_quant01) <> 0 Then
              nTcIndTaglF = 1
            ElseIf NTSCDec(dtrRow!mm_quant02) <> 0 Then
              nTcIndTaglF = 2
            ElseIf NTSCDec(dtrRow!mm_quant03) <> 0 Then
              nTcIndTaglF = 3
            ElseIf NTSCDec(dtrRow!mm_quant04) <> 0 Then
              nTcIndTaglF = 4
            ElseIf NTSCDec(dtrRow!mm_quant05) <> 0 Then
              nTcIndTaglF = 5
            ElseIf NTSCDec(dtrRow!mm_quant06) <> 0 Then
              nTcIndTaglF = 6
            ElseIf NTSCDec(dtrRow!mm_quant07) <> 0 Then
              nTcIndTaglF = 7
            ElseIf NTSCDec(dtrRow!mm_quant08) <> 0 Then
              nTcIndTaglF = 8
            ElseIf NTSCDec(dtrRow!mm_quant09) <> 0 Then
              nTcIndTaglF = 9
            ElseIf NTSCDec(dtrRow!mm_quant10) <> 0 Then
              nTcIndTaglF = 10
            ElseIf NTSCDec(dtrRow!mm_quant11) <> 0 Then
              nTcIndTaglF = 11
            ElseIf NTSCDec(dtrRow!mm_quant12) <> 0 Then
              nTcIndTaglF = 12
            ElseIf NTSCDec(dtrRow!mm_quant13) <> 0 Then
              nTcIndTaglF = 13
            ElseIf NTSCDec(dtrRow!mm_quant14) <> 0 Then
              nTcIndTaglF = 14
            ElseIf NTSCDec(dtrRow!mm_quant15) <> 0 Then
              nTcIndTaglF = 15
            ElseIf NTSCDec(dtrRow!mm_quant16) <> 0 Then
              nTcIndTaglF = 16
            ElseIf NTSCDec(dtrRow!mm_quant17) <> 0 Then
              nTcIndTaglF = 17
            ElseIf NTSCDec(dtrRow!mm_quant18) <> 0 Then
              nTcIndTaglF = 18
            ElseIf NTSCDec(dtrRow!mm_quant19) <> 0 Then
              nTcIndTaglF = 19
            ElseIf NTSCDec(dtrRow!mm_quant20) <> 0 Then
              nTcIndTaglF = 20
            ElseIf NTSCDec(dtrRow!mm_quant21) <> 0 Then
              nTcIndTaglF = 21
            ElseIf NTSCDec(dtrRow!mm_quant22) <> 0 Then
              nTcIndTaglF = 22
            ElseIf NTSCDec(dtrRow!mm_quant23) <> 0 Then
              nTcIndTaglF = 23
            ElseIf NTSCDec(dtrRow!mm_quant24) <> 0 Then
              nTcIndTaglF = 24
            End If

            dtrRow2 = ds.Tables("MOVMAGIMP").Select("mm_riga = " & nRiga)
            If dtrRow2.Length > 0 Then
              dtrRow2(0)!xxo_tcindtaglf = NTSCInt(IIf(nTcIndTaglF = 0, dtrRow2(0)!mm_tcindtagl, nTcIndTaglF))
              ValCodiceDb(NTSCInt(dtrRow2(0)!xxo_codtagl).ToString, strDitta, "TABTAGL", "N", "", dttDestagl)
              dtrRow2(0)!xxo_tctagliaf = NTSCStr(dttDestagl.Rows(0)("tb_dest" & dtrRow2(0)!xxo_tcindtaglf.ToString.PadLeft(2, "0"c))).ToUpper.Trim
              dtrRow2(0).AcceptChanges()
            End If
          Next
        End If
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  '------------------
  Public Overridable Function SalvaDocumento(ByRef ds As DataSet, ByVal strState As String, _
                                          ByVal bModPM As Boolean, ByVal bModTCO As Boolean, ByVal bProgrCambiato As Boolean, _
                                          ByVal bNuovoDaImportExport As Boolean, _
                                          ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                          ByVal bAggnumprot As Boolean, ByRef strError As String) As Boolean
    Try
      Return SalvaDocumento(ds, strState, bModPM, bModTCO, bProgrCambiato, bNuovoDaImportExport, dtInizEser, _
                            bCarichiProdDaAvmg, bAggnumprot, strError, False, False, False, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumento(ByRef ds As DataSet, ByVal strState As String, _
                                          ByVal bModPM As Boolean, ByVal bModTCO As Boolean, ByVal bProgrCambiato As Boolean, _
                                          ByVal bNuovoDaImportExport As Boolean, _
                                          ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                          ByVal bAggnumprot As Boolean, ByRef strError As String, _
                                          ByVal bCa2 As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, strState, bModPM, bModTCO, bProgrCambiato, bNuovoDaImportExport, _
                                             dtInizEser, bCarichiProdDaAvmg, bAggnumprot, strError, bCa2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        strError = NTSCStr(oIn(9))
        Return CBool(oOut)
      End If
      '----------------

      Return SalvaDocumento(ds, strState, bModPM, bModTCO, bProgrCambiato, bNuovoDaImportExport, dtInizEser, _
                            bCarichiProdDaAvmg, bAggnumprot, strError, bCa2, False, False, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumento(ByRef ds As DataSet, ByVal strState As String, _
                                        ByVal bModPM As Boolean, ByVal bModTCO As Boolean, ByVal bProgrCambiato As Boolean, _
                                        ByVal bNuovoDaImportExport As Boolean, _
                                        ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                        ByVal bAggnumprot As Boolean, ByRef strError As String, _
                                        ByVal bCa2 As Boolean, ByVal bModSQ As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, strState, bModPM, bModTCO, bProgrCambiato, bNuovoDaImportExport, _
                                             dtInizEser, bCarichiProdDaAvmg, bAggnumprot, strError, bCa2, bModSQ})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        strError = NTSCStr(oIn(9))
        Return CBool(oOut)
      End If
      '----------------

      Return SalvaDocumento(ds, strState, bModPM, bModTCO, bProgrCambiato, bNuovoDaImportExport, dtInizEser, _
                            bCarichiProdDaAvmg, bAggnumprot, strError, bCa2, bModSQ, False, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumento(ByRef ds As DataSet, ByVal strState As String, _
                                          ByVal bModPM As Boolean, ByVal bModTCO As Boolean, ByVal bProgrCambiato As Boolean, _
                                          ByVal bNuovoDaImportExport As Boolean, _
                                          ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                          ByVal bAggnumprot As Boolean, ByRef strError As String, _
                                          ByVal bCa2 As Boolean, ByVal bModSQ As Boolean, ByVal bTerzista As Boolean, _
                                          ByVal strNetProDB As String) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, strState, bModPM, bModTCO, bProgrCambiato, bNuovoDaImportExport, _
                                             dtInizEser, bCarichiProdDaAvmg, bAggnumprot, strError, bCa2, bModSQ, _
                                             bTerzista, strNetProDB})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        strError = NTSCStr(oIn(9))
        Return CBool(oOut)
      End If
      '----------------

      Return SalvaDocumento(ds, strState, bModPM, bModTCO, bProgrCambiato, bNuovoDaImportExport, dtInizEser, _
        bCarichiProdDaAvmg, bAggnumprot, strError, bCa2, bModSQ, bTerzista, strNetProDB, True)

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function SalvaDocumento(ByRef ds As DataSet, ByVal strState As String, _
                                          ByVal bModPM As Boolean, ByVal bModTCO As Boolean, ByVal bProgrCambiato As Boolean, _
                                          ByVal bNuovoDaImportExport As Boolean, _
                                          ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                          ByVal bAggnumprot As Boolean, ByRef strError As String, _
                                          ByVal bCa2 As Boolean, ByVal bModSQ As Boolean, ByVal bTerzista As Boolean, _
                                          ByVal strNetProDB As String, _
                                          ByVal bFlagRielabFattDiffSuVariazioneDDT As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, strState, bModPM, bModTCO, bProgrCambiato, bNuovoDaImportExport, _
                                             dtInizEser, bCarichiProdDaAvmg, bAggnumprot, strError, bCa2, bModSQ, _
                                             bTerzista, strNetProDB, bFlagRielabFattDiffSuVariazioneDDT})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        strError = NTSCStr(oIn(9))
        Return CBool(oOut)
      End If
      '----------------

      Return SalvaDocumento(ds, strState, bModPM, bModTCO, bProgrCambiato, bNuovoDaImportExport, dtInizEser, _
        bCarichiProdDaAvmg, bAggnumprot, strError, bCa2, bModSQ, bTerzista, strNetProDB, bFlagRielabFattDiffSuVariazioneDDT, False, False)

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function SalvaDocumento(ByRef ds As DataSet, ByVal strState As String, _
                                          ByVal bModPM As Boolean, ByVal bModTCO As Boolean, ByVal bProgrCambiato As Boolean, _
                                          ByVal bNuovoDaImportExport As Boolean, _
                                          ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                          ByVal bAggnumprot As Boolean, ByRef strError As String, _
                                          ByVal bCa2 As Boolean, ByVal bModSQ As Boolean, ByVal bTerzista As Boolean, _
                                          ByVal strNetProDB As String, _
                                          ByVal bFlagRielabFattDiffSuVariazioneDDT As Boolean, ByVal bModDII As Boolean, _
                                          ByVal bCollegaMG_DI As Boolean) As Boolean
    'bSetStatoOrdlist: se impostata a true cancellando l'Documento non verrà riaperta l'eventuale RDA collegata
    'strState: N = new, D = delete, U = update

    Dim strSQL As String
    Dim strSQLVal As String
    Dim lNewProgr As Integer

    'Dim factory As DbProviderFactory = Nothing
    'Dim sqlCmd As DbCommand
    Dim lResult As Integer

    Dim strDesogglog As String = ""
    Dim strSqlWhere As String = ""
    Dim strDesogglogU As String = ""
    Dim strSqlWhereU As String = ""

    Dim dbConn As DbConnection = Nothing
    Dim strNote As String = ""
    Dim dttFieldAlias As New DataTable
    Dim strTipoProg As String = ""

    Dim strTesta As String = "testmag"
    Dim strCorpo As String = "movmag"
    Dim lIITtkeys As Integer = 0
    Dim strAlfpro As String = ""

    Dim bIsFatturaFigurativa As Boolean = False

    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, strState, bModPM, bModTCO, bProgrCambiato, bNuovoDaImportExport, _
                                             dtInizEser, bCarichiProdDaAvmg, bAggnumprot, strError, bCa2, bModSQ, _
                                             bTerzista, strNetProDB})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        strError = NTSCStr(oIn(9))
        Return CBool(oOut)
      End If
      '----------------

      With ds.Tables("TESTA").Rows(0)
        If NTSCStr(!et_tipork) = "W" Then
          strTesta = "testprb"
          strCorpo = "movprb"
        ElseIf IsFatturaFigurativa(NTSCStr(!codditt), NTSCStr(!et_tipork), NTSCInt(!et_anno), NTSCStr(!et_serie)) Then
          bIsFatturaFigurativa = True
        End If
      End With



      '---------------------------------
      'serve per rinominare le colonne precedentemenre standardizzate
      dttFieldAlias.Columns.Add("datatable", GetType(String))
      dttFieldAlias.Columns.Add("database", GetType(String))
      dttFieldAlias.Rows.Add(New Object() {"et_flboll", "tm_flbolli"})
      dttFieldAlias.Rows.Add(New Object() {"ec_ricimp", "mm_flstat"})
      dttFieldAlias.AcceptChanges()

      '---------------------------------
      'scrivo actlog
      If strState <> "N" Then
        Select Case ds.Tables("TESTA").Rows(0)!et_tipork.ToString.ToUpper
          Case "A" : strDesogglog = "Fattura Imm. emessa"
          Case "B" : strDesogglog = "DDT emesso"
          Case "C" : strDesogglog = "Corrispettivo emesso"
          Case "E" : strDesogglog = "Nota di Addeb. emessa"
          Case "F" : strDesogglog = "Ric.Fiscale Emessa"
          Case "I" : strDesogglog = "Riemissione Ric.Fiscali"
          Case "J" : strDesogglog = "Nota Accr. ricevuta"
          Case "L" : strDesogglog = "Fattura Imm. ricevuta"
          Case "M" : strDesogglog = "DDT ricevuto"
          Case "N" : strDesogglog = "Nota Accr. emessa"
          Case "S" : strDesogglog = "Fatt.Ric.Fisc. Emessa"
          Case "T" : strDesogglog = "Carico da produz."
          Case "U" : strDesogglog = "Scarico a produz."
          Case "W" : strDesogglog = "Nota di prelievo"
          Case "Z" : strDesogglog = "Bolla di mov. interna"
        End Select
        strDesogglog += " n. " & NTSCStr(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        If ds.Tables("TESTA").Rows(0)!et_serie.ToString.Trim <> "" Then
          strDesogglog += "/" & ds.Tables("TESTA").Rows(0)!et_serie.ToString
        End If
        strDesogglog += " del " & NTSCDate(ds.Tables("TESTA").Rows(0)("et_datdoc", DataRowVersion.Original)).ToShortDateString
        strNote = strDesogglog
        strDesogglog = IIf(strState = "D", "Cancella", "Modifica").ToString() & " " & strDesogglog

        strSqlWhere = "'" & ds.Tables("TESTA").Rows(0)!codditt.ToString & "', " & _
                      "'" & ds.Tables("TESTA").Rows(0)!et_tipork.ToString & "', " & _
                      ds.Tables("TESTA").Rows(0)!et_anno.ToString & ", " & _
                      "'" & ds.Tables("TESTA").Rows(0)!et_serie.ToString & "', " & _
                      ds.Tables("TESTA").Rows(0)!et_numdoc.ToString

        'per scarichi a produzione
        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString.ToUpper = "T" Then
          strSqlWhereU = "'" & ds.Tables("TESTA").Rows(0)!codditt.ToString & "','U'," & _
                          ds.Tables("TESTA").Rows(0)!et_anno.ToString & ", " & _
                          "'" & ds.Tables("TESTA").Rows(0)!et_serie.ToString & "', " & _
                          ds.Tables("TESTA").Rows(0)!et_numdoc.ToString
          strDesogglogU = IIf(strState = "D", "Cancella", "Modifica").ToString()
          strDesogglogU += " scarico a produzione" & " n. " & NTSCStr(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          If ds.Tables("TESTA").Rows(0)!et_serie.ToString.Trim <> "" Then
            strDesogglogU += "/" & ds.Tables("TESTA").Rows(0)!et_serie.ToString
          End If
          strDesogglogU += " del " & ds.Tables("TESTA").Rows(0)("et_datdoc", DataRowVersion.Original).ToString
        End If
      End If    ' If strState <> "N" Then

      '---------------------------------
      'se è un nuovo documento da import/export (ovvero ho scritto a parte testmag e movmag e ora devo riaggiornare
      'lo tratto come se fosse una update, solo che in fase di cancellazione documento non storno artpro/artprox/keymag ma cancello solo testmag e movmag
      If bNuovoDaImportExport Then strState = "U"

      If bCa2 Then lIITtkeys = GetTblInstId("TTKEYS", False)

      '---------------------------------
      'apro il database e la transazione
      'factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      If strDesogglog <> "" Then ScriviActLog(ds.Tables("TESTA").Rows(0)!codditt.ToString, "BSVEBOLL", _
                                              strTesta, _
                                              strTesta, _
                                              strSqlWhere, IIf(strState = "D", "A", "M").ToString, "D", strDesogglog, False)
      If strDesogglogU <> "" Then ScriviActLog(ds.Tables("TESTA").Rows(0)!codditt.ToString, "BSVEBOLL", _
                                              strTesta, _
                                              strTesta, _
                                              strSqlWhereU, IIf(strState = "D", "A", "M").ToString, "D", strDesogglogU, False)

      '---------------------------------
      'per personalizzaz. 
      If Not SalvaDocumentoBeforeDelete(ds, strState, strError, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        Return False
      End If

      '----------------------------------
      ' qui, adesso, sia per Access che per SQL Server aggiorna proeseb
      ' (ho bisogno qui di avere sia il contenuto di MOTRANS che il conteuto di movmag, documento precedente alla modifica...)
      If bModPM And ds.Tables("TESTA").Rows(0)!et_tipork.ToString <> "W" Then
        With ds.Tables("TESTA").Rows(0)
          If Not AggProesebMovmag(dbConn, !codditt.ToString, !et_tipork.ToString, _
                                  NTSCInt(!et_anno), !et_serie.ToString, NTSCInt(!et_numdoc), _
                                  NTSCDate(!et_datdoc).ToShortDateString, ds.Tables("CORPO"), strState) Then
            If IsInTrans Then AnnullaTrans()
            Return False
          End If
        End With
      End If

      '---------------------------------
      'prima cancello il documento (non lo faccio se è una nuovo doc)
      If strState <> "N" Then
        If Not DeleteDoc(strState, ds, dbConn, bModPM, bModTCO, dtInizEser, bCarichiProdDaAvmg, bNuovoDaImportExport, _
                         bCa2, bModSQ, bTerzista, strNetProDB, bFlagRielabFattDiffSuVariazioneDDT, bModDII, bCollegaMG_DI) Then
          If IsInTrans Then AnnullaTrans()
          Return False
        End If
      End If

      If strState = "D" Then
        'Se ho stampato le etichette del ricevimento merci, devo cancellare alla cancellazione del documento (stato etichette stampate)
        'Altrimenti rimangono presenti, e creando un nu ovo documento con lo stesso anno\serie\numero, 
        'il ricevimento mostrerà le etichette del documento precedente
        If NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork) = "M" Or NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork) = "L" Then
          If NTSCStr(ds.Tables("TESTA").Rows(0)!et_vistato) = "E" Then
            strSQL = "DELETE tpallet FROM tpallet " & _
                     " INNER JOIN mpallet ON tp_idpall = mp_idpall AND mpallet.codditt = tpallet.codditt" & _
                     " WHERE mpallet.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt) & _
                     " AND mp_mxtipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork) & _
                     " AND mp_mxanno = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno) & _
                     " AND mp_mxserie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie) & _
                     " AND mp_mxnumdoc = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc)

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "DELETE FROM mpallet " & _
                     " WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt) & _
                     " AND mp_mxtipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork) & _
                     " AND mp_mxanno = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno) & _
                     " AND mp_mxserie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie) & _
                     " AND mp_mxnumdoc = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc)

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        End If

        'Se è una nota di prelievo con picking collegato, su opzione cancello il picking
        If NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork) = "W" And NTSCInt(ds.Tables("TESTA").Rows(0)!et_idpick) > 0 And _
           CBool(GetSettingBus("BSVEBOLL", "OPZIONI", ".", "CancellaAnchePicking", "0", ".", "0")) Then

          'Prima le righe
          strSQL = "DELETE movpic FROM movpic " & _
                   "INNER JOIN testpic ON movpic.codditt = testpic.codditt " & _
                   "      AND mp_idpick = tp_idpick AND mp_rigapick = tp_rigapick " & _
                   "WHERE movpic.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt) & _
                   " AND tp_mxtipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork) & _
                   " AND tp_mxanno = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno) & _
                   " AND tp_mxserie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie) & _
                   " AND tp_mxnumdoc = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc)

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'Poi le testate
          strSQL = "DELETE FROM testpic " & _
                   "WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt) & _
                   " AND tp_mxtipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork) & _
                   " AND tp_mxanno = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno) & _
                   " AND tp_mxserie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie) & _
                   " AND tp_mxnumdoc = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc)

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'Infine se non ci sono altre testate collegate allo stesso picking, cancella anche tabpick
          strSQL = "DELETE FROM tabpick " & _
                   " WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt) & _
                   " AND tb_codpick = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_idpick) & _
                   " AND 0 = (SELECT COUNT(codditt) FROM testpic " & _
                   "         WHERE tp_idpick = tb_codpick AND testpic.codditt = tabpick.codditt)"

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If

        'Fatturazione figurativa (quando scatta viene creata una colonna XX con i riferimenti al documento collegato)
        If bIsFatturaFigurativa Then
          If Not SganciaDocConFattFigurativa(ds, dbConn) Then
            If IsInTrans Then AnnullaTrans()
            Return False
          End If
        End If

        GoTo FINE
      End If    'If strState = "D" Then

      '---------------------------------
      'aggiorno il numero del documento
      Select Case ds.Tables("TESTA").Rows(0)!et_tipork.ToString
        Case "A", "D", "E", "N", "£" : strTipoProg = "A"
        Case "B" : strTipoProg = "B"
        Case "C" : strTipoProg = "C"
        Case "M" : strTipoProg = "M"
        Case "Z" : strTipoProg = "Z"
        Case "L", "K", "J", "(" : strTipoProg = "L"
        Case "W" : strTipoProg = "W"
        Case "T" : strTipoProg = "T"
        Case "U" : strTipoProg = "U"
        Case "F", "I" : strTipoProg = "F"
        Case "S" : strTipoProg = "S"
      End Select
      If bNuovoDaImportExport And bProgrCambiato = False Then
        'se non importo i documenti in ordine di numero documento non aggiorno la numerazione
        If NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) <> LegNuma(ds.Tables("TESTA").Rows(0)!codditt.ToString, strTipoProg, ds.Tables("TESTA").Rows(0)!et_serie.ToString, NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), False, dbConn) Then
          bProgrCambiato = True
        End If
      End If
      If (strState = "N" Or bNuovoDaImportExport) And bProgrCambiato = False Then
        lNewProgr = AggNuma(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                strTipoProg, _
                ds.Tables("TESTA").Rows(0)!et_serie.ToString, _
                NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), _
                True, True, strError, dbConn)
        '----------------------------
        'si è verificato un errore
        If lNewProgr = 0 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(strError))
        End If

        If lNewProgr <> NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) Or strError <> "" Then
          If IsInTrans Then AnnullaTrans()
          '----------------------------
          'aggiorno il numero del documento con il nuovo numero, visto che quello che avevo impostato è già stato utilizzato
          strError = "*N*" & strError   'aggiungo il marcatore che permetterà all'entity di cambiare il num doc su tutti i datatable
          ds.Tables("TESTA").Rows(0)!et_numdoc = lNewProgr

          Return False
        End If

        '----------------------------
        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then
          AggNuma(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                  "U", _
                  ds.Tables("TESTA").Rows(0)!et_serie.ToString, _
                  NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                  NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), _
                  False, False, strError, dbConn)
        End If
      End If

      '---------------------------------
      'Aggiorna il protocollo doc. ricevuto
      If bAggnumprot Then
        strAlfpro = NTSCStr(ds.Tables("TESTA").Rows(0)!et_alfpro)
        If strAlfpro = "" Then strAlfpro = " "

        lNewProgr = AggNuma(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                            "P", _
                            "A" & strAlfpro & NTSCInt(ds.Tables("TESTA").Rows(0)!et_nregiva).ToString("000"), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_numprot), _
                            True, False, strError, dbConn)
        If lNewProgr = 0 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(strError))
        End If
        If lNewProgr <> NTSCInt(ds.Tables("TESTA").Rows(0)!et_numprot) Then
          ds.Tables("TESTA").Rows(0)!et_numprot = lNewProgr
        End If
      End If

      '---------------------------------
      'insert into TESTA
      strSQL = "INSERT INTO " & strTesta & " " & GetQueryInsertField(ds.Tables("TESTA"), "tm_", "", "et_", dttFieldAlias)
      strSQLVal = GetQueryInsertValue(ds.Tables("TESTA"), ds.Tables("TESTA").Rows(0), "tm_", "", "et_")
      lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 127791221369687500, "Si è verificato un errore in fase di scrittura testata Documento: Documento non salvato")))
      End If

      strSQL = "INSERT INTO " & strTesta & "c " & GetQueryInsertField(ds.Tables("TESTAC"), "tm_")
      strSQLVal = GetQueryInsertValue(ds.Tables("TESTAC"), ds.Tables("TESTAC").Rows(0), "tm_")
      lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 128656110249687500, "Si è verificato un errore in fase di scrittura testata Documento - dettaglio contropartite: Documento non salvato")))
      End If

      '---------------------------------
      If Not SalvaDocumentoMOVMAG(ds, strState, strError, dttFieldAlias, dbConn) Then Return False

      '---------------------------------
      If bModTCO Then
        If Not SalvaDocumentoMOVMAGTC(ds, strState, strError, dttFieldAlias, dbConn) Then Return False
      End If
      '---------------------------------
      If Not SalvaDocumentoMOVCLA(ds, strState, strError, dttFieldAlias, dbConn) Then Return False

      '---------------------------------
      If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then
        If Not SalvaDocumentoTestmagIMP(ds, strState, strError, dttFieldAlias, dbConn) Then Return False
        If Not SalvaDocumentoMOVMAGIMP(ds, strState, strError, dttFieldAlias, dbConn) Then Return False
        If bModTCO Then
          If Not SalvaDocumentoMOVMAGIMPTC(ds, strState, strError, dttFieldAlias, dbConn) Then Return False
        End If
        If Not SalvaDocumentoLAVCENT(ds, strState, strError, dbConn) Then Return False
      End If

      '--------------------------------------------------------------------------------------------------------------
      '--- Nel caso sia stata richiamata la creazione di un Carico da Produzione, dopo aver salvato uno scarico,
      '--- il DataSet relativo al dettaglio matricole, non veniva svuotato con gli eventuali dati del
      '--- documento di origine
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = (ds.Tables("MOVMATR").Rows.Count - 1) To 0 Step -1
        With ds.Tables("MOVMATR").Rows(i)
          If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then
            If (NTSCStr(!mma_tipork) <> "T") And (NTSCStr(!mma_tipork) <> "U") Then .Delete()
          Else
            If NTSCStr(!mma_tipork) <> ds.Tables("TESTA").Rows(0)!et_tipork.ToString Then .Delete()
          End If
        End With
      Next i
      ds.Tables("MOVMATR").AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      'scrive sia i tipork T  che U contemporaneamente
      If Not SalvaDocumentoMOVMATR(ds, strState, strError, dttFieldAlias, dbConn) Then Return False

      '---------------------------------
      'aggiorno tm_ultagg
      strSQL = "UPDATE " & strTesta & " SET tm_ultagg = " & CDataOraSQL(DateTime.Now.ToString) & _
              " WHERE (tm_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
              IIf(ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T", " OR tm_tipork = 'U') ", ") ").ToString & _
              " AND tm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
              " AND tm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
              " AND tm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & _
              " AND codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString)
      lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 127791221370000000, "Si è verificato un errore in fase aggiornamento data ultimo aggiornamento in testata Documento: Documento non salvato")))
      End If

      '---------------------------------
      'lancio la stored procedure per l'aggiornamento di keymag/artpro/artprox/...
      strSQL = "bussp_bsveboll9_faggiorn2 " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & ", " & _
                CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & ", " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & ", " & _
                CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & ", " & _
                "0, " & _
                CDataSQL(dtInizEser) & ", " & _
                TrovaNdecSuPrzUn(0) & ", " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString)
      lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 128735426945666801, "Si è verificato un errore in fase di inserimento/modifica Documento (bussp_bsveboll9_faggiorn2): Documento non aggiornato")))
      End If

      If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then
        strSQL = "bussp_bsveboll9_faggiorn2 " & _
                "'U', " & _
                CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & ", " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & ", " & _
                CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & ", " & _
                "0, " & _
                CDataSQL(dtInizEser) & ", " & _
                TrovaNdecSuPrzUn(0) & ", " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 127791221370156250, "Si è verificato un errore in fase di inserimento/modifica Documento (bussp_bsveboll9_faggiorn2 - scarichi collegati): Documento non aggiornato")))
        End If
      End If

      '---------------------------------
      If Not SalvaDocumentoMOVCONA(ds, strState, strError, dttFieldAlias, dbConn) Then Return False

      '---------------------------------
      'Update per il Picking: non serve: l'ID è già impostato in testprb

      '---------------------------------
      'TCO: AggArtproTC
      If bModTCO And Not (ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "A" And ds.Tables("TESTA").Rows(0)!et_proforma.ToString = "S") Then
        If Not AggArtproTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                          NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString), _
                          NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                          NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                          NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 0, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If

        If Not AggMovOrdTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                          NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString), _
                          NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                          NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                          NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 0, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If

        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then
          If Not AggArtproTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, "U", _
                                     NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                                     NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                                     NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 0, dbConn) Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Return False
          End If

          If Not AggMovOrdTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, "U", _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 0, dbConn) Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Return False
          End If
        End If
      End If    'If bModTCO And Not (ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "A" And ds.Tables("TESTA").Rows(0)!et_proforma.ToString = "S") Then

      '-------------------------------
      'query come da [bussp_bsveboll9_faggiorna]
      'Aggiornamenti per il conto lavoro
      With ds.Tables("TESTA").Rows(0)
        strSQL = "UPDATE " & strCorpo & _
                " SET mm_cltipo = mm_tipork, mm_clanno = mm_anno, mm_clserie = mm_serie, mm_clnum = mm_numdoc, mm_clriga = mm_riga" & _
                " FROM (" & strCorpo & " INNER JOIN tabcaum ON " & strCorpo & ".mm_causale = tabcaum.tb_codcaum)" & _
                " INNER JOIN tabmaga ON " & strCorpo & ".codditt = tabmaga.codditt AND " & strCorpo & ".mm_magaz = tabmaga.tb_codmaga" & _
                " WHERE " & strCorpo & ".codditt = " & CStrSQL(!codditt) & _
                " AND " & strCorpo & ".mm_tipork = " & CStrSQL(!et_tipork) & _
                " AND " & strCorpo & ".mm_anno = " & !et_anno.ToString & _
                " AND " & strCorpo & ".mm_serie = " & CStrSQL(!et_serie) & _
                " AND " & strCorpo & ".mm_numdoc = " & !et_numdoc.ToString & _
                " AND tabmaga.tb_flclavo <> ' '" & _
                " AND (tabcaum.tb_tipcaum = 'C' OR tabcaum.tb_tipcaum = 'K')"
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "UPDATE " & strCorpo & _
                " SET mm_cltipo = mm_tipork, mm_clanno = mm_anno, mm_clserie = mm_serie, mm_clnum = mm_numdoc, mm_clriga = mm_riga" & _
                " FROM (" & strCorpo & " INNER JOIN tabcaum ON " & strCorpo & ".mm_causale2 = tabcaum.tb_codcaum)" & _
                " INNER JOIN tabmaga ON " & strCorpo & ".codditt = tabmaga.codditt AND " & strCorpo & ".mm_magaz2 = tabmaga.tb_codmaga" & _
                " WHERE " & strCorpo & ".codditt = " & CStrSQL(!codditt) & _
                " AND " & strCorpo & ".mm_tipork = " & CStrSQL(!et_tipork) & _
                " AND " & strCorpo & ".mm_anno = " & !et_anno.ToString & _
                " AND " & strCorpo & ".mm_serie = " & CStrSQL(!et_serie) & _
                " AND " & strCorpo & ".mm_numdoc = " & !et_numdoc.ToString & _
                " AND tabmaga.tb_flclavo <> ' '" & _
                " AND tabcaum.tb_tipcaum = 'C'"
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'Aggiornamenti per il conto visione (no note di prelievo)
        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString <> "W" Then
          strSQL = "UPDATE " & strCorpo & _
                  " SET mm_cvtipo = mm_tipork, mm_cvanno = mm_anno, mm_cvserie = mm_serie, mm_cvnum = mm_numdoc, mm_cvriga = mm_riga" & _
                  " FROM (" & strCorpo & " INNER JOIN tabcaum ON " & strCorpo & ".mm_causale = tabcaum.tb_codcaum)" & _
                  " INNER JOIN tabmaga ON " & strCorpo & ".codditt = tabmaga.codditt AND " & strCorpo & ".mm_magaz = tabmaga.tb_codmaga" & _
                  " WHERE " & strCorpo & ".codditt = " & CStrSQL(!codditt) & _
                  " AND " & strCorpo & ".mm_tipork = " & CStrSQL(!et_tipork) & _
                  " AND " & strCorpo & ".mm_anno = " & !et_anno.ToString & _
                  " AND " & strCorpo & ".mm_serie = " & CStrSQL(!et_serie) & _
                  " AND " & strCorpo & ".mm_numdoc = " & !et_numdoc.ToString & _
                  " AND tabmaga.tb_flclavo <> ' '" & _
                  " AND tabcaum.tb_tipcaum = 'V'"
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          strSQL = "UPDATE " & strCorpo & _
                  " SET mm_cvtipo = mm_tipork, mm_cvanno = mm_anno, mm_cvserie = mm_serie, mm_cvnum = mm_numdoc, mm_cvriga = mm_riga" & _
                  " FROM (" & strCorpo & " INNER JOIN tabcaum ON " & strCorpo & ".mm_causale2 = tabcaum.tb_codcaum)" & _
                  " INNER JOIN tabmaga ON " & strCorpo & ".codditt = tabmaga.codditt AND " & strCorpo & ".mm_magaz2 = tabmaga.tb_codmaga" & _
                  " WHERE " & strCorpo & ".codditt = " & CStrSQL(!codditt) & _
                  " AND " & strCorpo & ".mm_tipork = " & CStrSQL(!et_tipork) & _
                  " AND " & strCorpo & ".mm_anno = " & !et_anno.ToString & _
                  " AND " & strCorpo & ".mm_serie = " & CStrSQL(!et_serie) & _
                  " AND " & strCorpo & ".mm_numdoc = " & !et_numdoc.ToString & _
                  " AND tabmaga.tb_flclavo <> ' '" & _
                  " AND tabcaum.tb_tipcaum = 'V'"
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If    'If ds.Tables("TESTA").Rows(0)!et_tipork.ToString <> "W" Then

        If !et_tipork.ToString = "T" Then
          strSQL = "UPDATE " & strCorpo & _
                  " SET mm_cctipo = mm_tipork, mm_ccanno = mm_anno, mm_ccserie = mm_serie, mm_ccnum = mm_numdoc, mm_ccriga = mm_riga" & _
                  " FROM (" & strCorpo & " INNER JOIN tabcaum ON " & strCorpo & ".mm_causale = tabcaum.tb_codcaum)" & _
                  " INNER JOIN tabmaga ON " & strCorpo & ".codditt = tabmaga.codditt AND " & strCorpo & ".mm_magaz = tabmaga.tb_codmaga" & _
                  " WHERE " & strCorpo & ".codditt = " & CStrSQL(!codditt) & _
                  " AND " & strCorpo & ".mm_tipork = " & CStrSQL(!et_tipork) & _
                  " AND " & strCorpo & ".mm_anno = " & !et_anno.ToString & _
                  " AND " & strCorpo & ".mm_serie = " & CStrSQL(!et_serie) & _
                  " AND " & strCorpo & ".mm_numdoc = " & !et_numdoc.ToString & _
                  " AND (tabmaga.tb_flclavo = 'C' OR tabmaga.tb_flclavo = 'X')" & _
                  " AND tabcaum.tb_tipcaum = 'P' AND mm_fase = 0"
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      End With    'With ds.Tables("TESTA").Rows(0)

      '-------------------------------
      'non serve chiamare la bussp_bsveboll9_faggiorn3: scrive testmagc e testprbc (già fatto sopra)
      'non serve chiamare la bussp_bsveboll9_faggiorn4: scrive MOVCONA (già fatto sopra)

      '-------------------------------
      'Se l'eventuale aggiornamento dell'anagrafica delle matricole (NNMATRICS)
      'non è anadata a buon fine esce
      If Not SalvaDocumentoAggiornaAnagraficaMatricole( _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!codditt.ToString), _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno), _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc), dbConn) Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Return False
      End If

      '--------------------------------
      'Gestione salvataggio documenti modificati dal nuovo ricm
      If ds.Tables.Contains("MOVPIC") Then
        If Not SalvaDocumentoMOVPIC(ds, strState, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If
      End If

      '---------------------------------
      'insert into SCHETRASP
      If ds.Tables("SCHETRASP").Rows.Count > 0 Then
        strSQL = "INSERT INTO schetrasp " & GetQueryInsertField(ds.Tables("SCHETRASP"), "sct_", "", "et_")
        strSQLVal = GetQueryInsertValue(ds.Tables("SCHETRASP"), ds.Tables("SCHETRASP").Rows(0), "sct_", "", "et_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128988829130475181, "Si è verificato un errore in fase di scrittura schetrasp Documento: Documento non salvato")))
        End If
      End If

      If bCa2 Then
        '---------------------------------
        'scrivo priana2 da testmag/movmag
        'tutti i test per essere sicuri che la scrittura vada a buon fine sono stati eseguiti in 
        'beveboll.TestPreSalvaTestmag_CheckCa2()

        'non devo scrivere priana2 se il ddt risulta già fatturato, visto che fadi/fdin nella creazione fattura 
        'cancellano priana2 del ddt: ricorda: con la ca2 una fattura registrata in CG non può essere modifica/cancellata
        Select Case NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString)
          Case "B", "M", "Z", "T"
            If NTSCStr(ds.Tables("TESTA").Rows(0)!et_flfatt) = "N" Then
              If Not ScriviPriana2DaTestmag(NTSCStr(ds.Tables("TESTA").Rows(0)!codditt.ToString), _
                                          NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString), _
                                          NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno), _
                                          NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                                          NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc), _
                                          NTSCStr(ds.Tables("TESTA").Rows(0)!et_datdoc), _
                                          dbConn, lIITtkeys, False) Then Return False
            End If
          Case Else
            'da non fare
        End Select
      End If    'If bCa2 Then

      If Not SalvaDocumentoCOLLAUDI(strState, ds, bModSQ, dbConn) Then Return False

      '---------------------------------
      'se sto evadendo IC, OF, OP esterni devo aggiornare anche netpro
      If (bTerzista = False And ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T") Or ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "W" Or strNetProDB = "" Then
        'gli op interni no, visto che è netpro che li avanza ed ha già tutta la situazione allineata
      Else
        If Not SalvaOrdineAggNetPro(ds, strState, strNetProDB, strError, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If
      End If

      'Dichiarazioni di intento
      If bModDII And bCollegaMG_DI And _
         ds.Tables("TESTA").Rows(0)!et_tipork.ToString <> "Z" And _
         ds.Tables("TESTA").Rows(0)!et_tipork.ToString <> "W" Then
        'In inserimento, controlllo e passo i nuovi valori della dichiarazione di intento
        If NTSCInt(ds.Tables("TESTAC").Rows(0)!tm_dianno) <> 0 And NTSCInt(ds.Tables("TESTAC").Rows(0)!tm_dinumero) <> 0 Then
          If Not GestisciFlagChiusura("I", ds.Tables("TESTA").Rows(0)!codditt.ToString, ds.Tables("TESTA").Rows(0)!xx_tipo.ToString, NTSCInt(ds.Tables("TESTAC").Rows(0)!tm_dianno), NTSCInt(ds.Tables("TESTAC").Rows(0)!tm_dinumero), dbConn) Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Return False
          End If
        End If
      End If

      'Fatturazione figurativa (quando viene evaso un corrispettivo viene creata una colonna XX con i riferimenti al documento collegato)
      If bIsFatturaFigurativa AndAlso ds.Tables("CORPO").Columns.Contains("xx_rifcorr") Then
        If Not EvadiDocConFattFigurativa(ds, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If
      End If

FINE:

      '--------------------------------
      'per personalizzaz. 
      If Not SalvaDocumentoBeforeChiudiTrans(ds, strState, strError, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Return False
      End If

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()
      ds.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      If bCa2 Then
        Try
          ResetTblInstId("TTKEYS", False, lIITtkeys)
        Catch
        End Try
      End If
    End Try
  End Function
  Public Overridable Function AggProesebMovmag(ByRef dbConn As DbConnection, ByVal strDitta As String, _
                                               ByVal strTipork As String, ByVal nAnno As Integer, _
                                               ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                               ByVal strDatdoc As String, ByRef dttCorpo As DataTable, _
                                               ByVal strState As String) As Boolean
    Dim strSQL As String = ""
    Dim strSQL1 As String = ""
    Dim dttTmp As New DataTable
    Dim strTmp As String = ""
    Dim strIdNew As String = ""     'sono le nuove righe di ttpriana che non c'erano in priana all'apertura della registrazione
    Dim strTBuild As New System.Text.StringBuilder

    Try
      'se è una delete devo solo cancellare tutto !!!
      If strState = "D" Then
        strTmp = "'0'"
        strIdNew = ""
        GoTo CANCELLA
      End If

      ' cerca i record modificati
AGGIORNA:
      For Each dtrT As DataRow In dttCorpo.Rows
        strTmp += "'" & dtrT!codditt.ToString & "." & dtrT!ec_tipork.ToString & "." & dtrT!ec_anno.ToString & "." & _
                  dtrT!ec_serie.ToString & "." & dtrT!ec_numdoc.ToString & "." & dtrT!ec_riga.ToString & "', "   'mi servirà dopo ...

        strSQL = "SELECT tm_datdoc, mm_commeca, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, mm_quant, " & _
                 " mm_valore, mm_perqta, mm_pmtaskid, mm_pmsalcon, mm_pmqtadis, mm_pmvaldis " & _
                 " FROM " & strJoinTestmagMovmag & _
                 " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                 " AND mm_tipork = " & CStrSQL(strTipork) & _
                 " AND mm_anno = " & nAnno.ToString & _
                 " AND mm_serie = " & CStrSQL(strSerie.ToString) & _
                 " AND mm_numdoc = " & lNumdoc.ToString & _
                 " AND mm_riga = " & dtrT!ec_riga.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn) 'ottengo priana di origine con ec_progr = a quello in memoria
        If dttTmp.Rows.Count = 0 Then strIdNew += "'" & dtrT!codditt.ToString & "." & dtrT!ec_tipork.ToString & "." & dtrT!ec_anno.ToString & "." & _
                                                   dtrT!ec_serie.ToString & "." & dtrT!ec_numdoc.ToString & "." & dtrT!ec_riga.ToString & "', "

        'se i dati di pm sono cambiati tra record di origine e quello in memoria, storno e riaggiorno PROESEB
        For Each dtrP As DataRow In dttTmp.Rows
          If (Not (NTSCInt(dtrT!ec_commeca) = NTSCInt(dtrP!mm_commeca) And _
                   NTSCInt(dtrT!ec_pmtaskid) = NTSCInt(dtrP!mm_pmtaskid) And _
                   NTSCDec(dtrT!ec_quant) = NTSCDec(dtrP!mm_quant) And _
                   NTSCDec(dtrT!ec_valore) = NTSCDec(dtrP!mm_valore) And _
                   NTSCDec(dtrT!ec_pmvaldis) = NTSCDec(dtrP!mm_pmvaldis) And _
                   NTSCStr(dtrT!ec_pmsalcon) = NTSCStr(dtrP!mm_pmsalcon) And _
                   NTSCDec(dtrT!ec_pmqtadis) = NTSCDec(dtrP!mm_pmqtadis))) And _
                  (NTSCInt(dtrT!ec_pmtaskid) <> 0 Or NTSCInt(dtrP!mm_pmtaskid) <> 0) Then
            ' ora crea proeseb storno
            If NTSCInt(dtrP!mm_pmtaskid) <> 0 Then
              AggProesebDaMovmagDao(strDitta, 1, "MOVMAG", 0, "PROESEB", 0, strTipork, nAnno, strSerie, lNumdoc, _
                                    NTSCInt(dtrP!mm_riga), NTSCDate(dtrP!tm_datdoc).ToShortDateString, dbConn, Nothing)
            End If
            ' ora crea proeseb dati nuovi
            If NTSCInt(dtrT!ec_pmtaskid) <> 0 Then
              AggProesebDaMovmagDao(strDitta, 0, "MMTRANS", 0, "PROESEB", 0, strTipork, nAnno, strSerie, lNumdoc, _
                                    NTSCInt(dtrP!mm_riga), NTSCDate(dtrP!tm_datdoc).ToShortDateString, dbConn, dttCorpo)
            End If
          End If
        Next    'For Each dtrP As DataRow In dttPriaOrig.Rows

        dttTmp.Clear()
      Next    'For Each dtrT As DataRow In dttPriana.Rows
      If strTmp.Length > 0 Then strTmp = strTmp.Substring(0, strTmp.Length - 2)

      '--------------------------------------------
      'ora cancella i record in priana che non ci sono più in ttpriana
      'se dalla registraz. originaria di CG ho cancellato l'unica riga che rimandava alla CA,
      'dttPriana è vuota... devo comunque eseguire la delete 
CANCELLA:
      strSQL = "SELECT mm_riga, tm_datdoc FROM " & strJoinTestmagMovmag & _
        " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
        " AND mm_tipork = " & CStrSQL(strTipork) & _
        " AND mm_anno = " & nAnno.ToString & _
        " AND mm_serie = " & CStrSQL(strSerie) & _
        " AND mm_numdoc = " & lNumdoc.ToString & _
        " AND mm_pmtaskid <> 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      'ora tolgo tutte le righe che non devono esserci
      If strTmp <> "'0'" Then
        Dim colt(1) As System.Data.DataColumn
        colt(0) = dttCorpo.Columns("ec_riga")
        dttCorpo.PrimaryKey = colt
        For Each dtrT As DataRow In dttTmp.Rows
          If dttCorpo.Select("ec_riga = " & dtrT!mm_riga.ToString).Length > 0 Then
            dtrT.Delete()
          End If
        Next
        dttCorpo.PrimaryKey = Nothing
        dttTmp.AcceptChanges()
      End If
      For Each dtrT As DataRow In dttTmp.Rows
        AggProesebDaMovmagDao(strDitta, 1, "MOVMAG", 0, "PROESEB", 0, strTipork, nAnno, strSerie, lNumdoc, _
          NTSCInt(dtrT!mm_riga), NTSCDate(dtrT!tm_datdoc).ToShortDateString, dbConn, Nothing)
      Next 'For Each dtrT As DataRow In dttPriana.Rows
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
INSERISCI:
      ' ora inserisce i record nuovi di ttpriana che non c'erano in priana
      If strIdNew.Length > 0 Then
        strIdNew = strIdNew.Substring(0, strIdNew.Length - 2)
        For Each dtrT As DataRow In dttCorpo.Select("ec_pmtaskid <> 0 AND (codditt + '.' + ec_tipork + '.' + ec_anno + '.' + " & _
                  "ec_serie + '.' + ec_numdoc + '.' + ec_riga) IN (" & strIdNew & ")")
          AggProesebDaMovmagDao(strDitta, 0, "MMTRANS", 0, "PROESEB", 0, strTipork, nAnno, strSerie, lNumdoc, _
                      NTSCInt(dtrT!ec_riga), strDatdoc, dbConn, dttCorpo)
        Next
      End If    'If strIdNew.Length > 0 Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      If Not dttCorpo Is Nothing Then dttCorpo.PrimaryKey = Nothing
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function SalvaDocumentoTestDataComp(ByVal strDitta As String, ByVal nControp As Integer, _
                                                         ByVal bCa As Boolean, ByRef bGestDateComp As Boolean) As Boolean
    '------------------
    'ritorna bGestDateComp = true se il conto do CG o il conto di CA gestiscono le date di competenza economica
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      bGestDateComp = False

      strSQL = "SELECT an_accperi, ac_accperi" & _
        " FROM (tabcove LEFT JOIN anagra ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto)" & _
        " LEFT JOIN anagca ON tabcove.codditt = anagca.codditt AND tabcove.tb_concova = anagca.ac_conto" & _
        " WHERE tabcove.codditt = " & CStrSQL(strDitta) & _
        " AND tb_codcove = " & nControp & _
        " AND (an_accperi = 'S' " & IIf(bCa, "OR ac_accperi = 'S'", "").ToString & ")"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        If dttTmp.Rows(0)!an_accperi.ToString = "S" Or dttTmp.Rows(0)!ac_accperi.ToString = "D" Then
          bGestDateComp = True
        End If
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
  Public Overridable Function SalvaDocumentoTestmagIMP(ByRef ds As DataSet, ByVal strState As String, _
                                                    ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                                    ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0
    Dim nMagimp As Integer = 0

    Try
      '---------------------------------
      'cambio solo il tipo documento e tolgo i riferimenti alla fatturazione
      ds.Tables("TESTA").Rows(0).AcceptChanges()
      ds.Tables("TESTA").Rows(0)!et_tipork = "U"
      ds.Tables("TESTA").Rows(0)!et_flfatt = "N"
      ds.Tables("TESTA").Rows(0)!et_alffat = " "
      ds.Tables("TESTA").Rows(0)!et_numfat = 0
      ds.Tables("TESTA").Rows(0)!et_datfat = DBNull.Value
      ds.Tables("TESTA").Rows(0)!et_annfat = 0
      ds.Tables("TESTA").Rows(0)!et_tiporkfat = " "
      strSQL = "INSERT INTO testmag " & GetQueryInsertField(ds.Tables("TESTA"), "tm_", "", "et_", dttFieldAlias)
      strSQLVal = GetQueryInsertValue(ds.Tables("TESTA"), ds.Tables("TESTA").Rows(0), "tm_", "", "et_")
      ds.Tables("TESTA").Rows(0).RejectChanges()
      lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 128523812467798000, "Si è verificato un errore in fase di scrittura testata Documento impegno di produzione: Documento non salvato")))
      End If

      ds.Tables("TESTAC").Rows(0).AcceptChanges()
      ds.Tables("TESTAC").Rows(0)!tm_tipork = "U"
      strSQL = "INSERT INTO testmagc " & GetQueryInsertField(ds.Tables("TESTAC"), "tm_")
      strSQLVal = GetQueryInsertValue(ds.Tables("TESTAC"), ds.Tables("TESTAC").Rows(0), "tm_")
      ds.Tables("TESTAC").Rows(0).RejectChanges()
      lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 128656111896562500, "Si è verificato un errore in fase di scrittura testata Documento scarico a produzione: Documento non salvato")))
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoMOVMAG(ByRef ds As DataSet, ByVal strState As String, _
                                                ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                                ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0
    Dim strCorpo As String = "movmag"

    Try
      If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "W" Then
        strCorpo = "movprb"
      End If

      '---------------------------------
      strSQL = "INSERT INTO " & strCorpo & " " & GetQueryInsertField(ds.Tables("CORPO"), "mm_", "", "ec_", dttFieldAlias)
      For i = 0 To ds.Tables("CORPO").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("CORPO"), ds.Tables("CORPO").Rows(i), "mm_", "", "ec_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128735427036032801, "Si è verificato un errore in fase di scrittura riga Documento: Documento non salvato")))
        End If
        ds.Tables("CORPO").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoMOVMATR(ByRef ds As DataSet, ByVal strState As String, _
                                              ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                              ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO movmatr " & GetQueryInsertField(ds.Tables("MOVMATR"), "mma_")
      For i = 0 To ds.Tables("MOVMATR").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("MOVMATR"), ds.Tables("MOVMATR").Rows(i), "mma_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128735427054752801, "Si è verificato un errore in fase di scrittura riga matricole: Documento non salvato")))
        End If
        ds.Tables("MOVMATR").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoMOVCLA(ByRef ds As DataSet, ByVal strState As String, _
                                            ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                            ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO movcla " & GetQueryInsertField(ds.Tables("MOVCLA"), "mc_")
      For i = 0 To ds.Tables("MOVCLA").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("MOVCLA"), ds.Tables("MOVCLA").Rows(i), "mc_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128735427070820801, "Si è verificato un errore in fase di scrittura riga movimenti conto lavoro attivo: Documento non salvato")))
        End If
        ds.Tables("MOVCLA").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoMOVCONA(ByRef ds As DataSet, ByVal strState As String, _
                                            ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                            ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO movcona " & GetQueryInsertField(ds.Tables("MOVCONA"), "mma_")
      For i = 0 To ds.Tables("MOVCONA").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("MOVCONA"), ds.Tables("MOVCONA").Rows(i), "mma_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 127791221369843750, "Si è verificato un errore in fase di scrittura riga CONAI: Documento non salvato")))
        End If
        ds.Tables("MOVCONA").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoMOVMAGIMP(ByRef ds As DataSet, ByVal strState As String, _
                                                    ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                                    ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO movmag " & GetQueryInsertField(ds.Tables("CORPOIMP"), "mm_", "", "ec_", dttFieldAlias)
      For i = 0 To ds.Tables("CORPOIMP").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("CORPOIMP"), ds.Tables("CORPOIMP").Rows(i), "mm_", "", "ec_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128523786078922000, "Si è verificato un errore in fase di scrittura riga Documento impegno di produzione: Documento non salvato")))
        End If
        ds.Tables("CORPOIMP").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoLAVCENT(ByRef ds As DataSet, ByVal strState As String, ByRef strError As String, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO lavcent " & GetQueryInsertField(ds.Tables("LAVCENT"), "lce_")
      For i = 0 To ds.Tables("LAVCENT").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("LAVCENT"), ds.Tables("LAVCENT").Rows(i), "lce_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128523787185586000, "Si è verificato un errore in fase di scrittura riga LAVORAZIONI: Documento non salvato")))
        End If
        ds.Tables("LAVCENT").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoMOVMAGTC(ByRef ds As DataSet, ByVal strState As String, _
                                                  ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                                  ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO movmagtc " & GetQueryInsertField(ds.Tables("CORPOTC"), "mm_", "", "ec_", dttFieldAlias)
      For i = 0 To ds.Tables("CORPOTC").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("CORPOTC"), ds.Tables("CORPOTC").Rows(i), "mm_", "", "ec_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128523786354886000, "Si è verificato un errore in fase di scrittura riga documento TC: Documento non salvato")))
        End If
        ds.Tables("CORPOTC").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoMOVMAGIMPTC(ByRef ds As DataSet, ByVal strState As String, _
                                                  ByRef strError As String, ByRef dttFieldAlias As DataTable, _
                                                  ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0

    Try
      '---------------------------------
      strSQL = "INSERT INTO movmagtc " & GetQueryInsertField(ds.Tables("CORPOIMPTC"), "mm_", "", "ec_", dttFieldAlias)
      For i = 0 To ds.Tables("CORPOIMPTC").Rows.Count - 1
        strSQLVal = GetQueryInsertValue(ds.Tables("CORPOIMPTC"), ds.Tables("CORPOIMPTC").Rows(i), "mm_", "", "ec_")
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 128523786566422000, "Si è verificato un errore in fase di scrittura riga Documento TC scarico a produzione: Documento non salvato")))
        End If
        ds.Tables("CORPOIMPTC").Rows(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoMOVPIC(ByRef ds As DataSet, ByVal strState As String, ByRef strError As String, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      'Aggiona lo stato a completato
      With ds.Tables("ESTREMI").Rows(0)
        strSQL = "UPDATE tabpick SET tb_chiuso = " & CStrSQL("S") & " WHERE tb_codpick IN (SELECT testpic.tp_idpick FROM testpic " & _
                 "WHERE tp_mxanno = " & NTSCInt(!tm_anno) & " AND tp_mxserie = " & CStrSQL(!tm_serie) & _
                 " AND tp_mxnumdoc = " & NTSCInt(!tm_numdoc) & " AND tp_mxtipork = " & CStrSQL(!tm_tipork) & _
                 " AND codditt = " & CStrSQL(ds.Tables("DITTA").Rows(0)!xx_ditta) & ") And codditt = " & CStrSQL(ds.Tables("DITTA").Rows(0)!xx_ditta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "UPDATE testpic SET tp_vistato = " & CStrSQL("S") & " WHERE tp_mxanno = " & NTSCInt(!tm_anno) & " AND tp_mxserie = " & CStrSQL(!tm_serie) & _
                 " AND tp_mxnumdoc = " & NTSCInt(!tm_numdoc) & " AND tp_mxtipork = " & CStrSQL(!tm_tipork) & _
                 " AND codditt = " & CStrSQL(ds.Tables("DITTA").Rows(0)!xx_ditta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'Aggiorno testpic
        strSQL = "UPDATE testpic SET tp_totcoll = (SELECT SUM(mp_colliriscr) FROM movpic WHERE mp_mxanno = " & NTSCInt(!tm_anno) & _
                 " AND mp_mxserie = " & CStrSQL(!tm_serie) & " AND mp_mxnumdoc = " & NTSCInt(!tm_numdoc) & _
                 " AND mp_mxtipork = " & CStrSQL(!tm_tipork) & " AND codditt = " & CStrSQL(ds.Tables("DITTA").Rows(0)!xx_ditta) & ") WHERE tp_mxanno = " & NTSCInt(!tm_anno) & _
                 " AND tp_mxserie = " & CStrSQL(!tm_serie) & " AND tp_mxnumdoc = " & NTSCInt(!tm_numdoc) & _
                 " AND tp_mxtipork = " & CStrSQL(!tm_tipork) & " AND codditt = " & CStrSQL(ds.Tables("DITTA").Rows(0)!xx_ditta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoCOLLAUDI(ByVal strState As String, ByRef ds As DataSet, _
                                                     ByVal bModSQ As Boolean, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim lProg As Integer = 0
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim i As Integer = 0

    Dim nCodtcon As Integer = 0     'tipo di controllo
    Dim nCodpcam As Integer = 0     'piano di campionamento
    Dim nCodclaq As Integer = 0     'classe di qualità
    Dim nCodlivq As Integer = 0     'livello di controllo
    Dim dQta As Decimal = 0         'quantità da controllare

    Dim strValcombo As String = " "
    Dim strValdesval As String = " "
    Dim strValresult As String = "C"
    Try
      'sistema qualità: creo i collaudi (se è un aggiornamento solo sulle righe dove non ci sono i collaudi xchè cancellati nella delete prima di questa salva)
      If Not bModSQ Then Return True

      'righe da collaudare
      For Each dtrT As DataRow In ds.Tables("CORPO").Select("ec_flcom = 'S'")
        If strState = "U" Then
          'in modifica devo generare i collaudi solo per le righe aggiunte o modificate
          strSQL = "SELECT cm_annocoll, cm_progrcoll FROM keycoll" & _
                   " WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND cm_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                   " AND cm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND cm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND cm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & _
                   " AND cm_riga = " & dtrT!ec_riga.ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count > 0 Then GoTo PASSA
          dttTmp.Clear()
        End If

        '--------------------------------------------
        'creo il collaudo
        'determino tipo di campionamento, clase forniture, ecc...
        If Not CercaContrda(ds.Tables("TESTA").Rows(0)!codditt.ToString, "BSVEBOLL", ds.Tables("TESTA").Rows(0)!et_tipork.ToString, _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_magimp), NTSCInt(ds.Tables("TESTA").Rows(0)!et_conto), _
                            0, NTSCStr(dtrT!ec_codart), NTSCInt(dtrT!ec_causale), NTSCInt(dtrT!ec_fase), _
                            NTSCDate(ds.Tables("TESTA").Rows(0)!et_datdoc).ToShortDateString, True, _
                            nCodtcon, nCodpcam, nCodclaq, nCodlivq, dbConn) Then Return False

        lProg = LegNuma(ds.Tables("TESTA").Rows(0)!codditt.ToString, "CL", " ", NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno), True, dbConn)
        AggNuma(ds.Tables("TESTA").Rows(0)!codditt.ToString, "CL", " ", NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno), lProg, False, False, "", dbConn)

        'determino la qta da controllare
        dQta = NTSCDec(dtrT!ec_quant)
        strSQL = "SELECT TOP 1 cc_qtadacon FROM compcam" & _
                " WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                " AND cc_codpcam = " & nCodpcam & _
                " AND cc_codclaq = " & nCodclaq & _
                " AND cc_codlivq = " & nCodlivq & _
                " AND cc_finoaqta >= " & CDblSQL(dQta) & _
                " ORDER BY cc_codpcam, cc_codclaq, cc_codlivq, cc_finoaqta"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then
          If NTSCDec(dtrT!ec_quant) >= NTSCDec(dttTmp.Rows(0)!cc_qtadacon) Then dQta = NTSCDec(dttTmp.Rows(0)!cc_qtadacon)
        End If
        dttTmp.Clear()
        If dQta = 0 Then dQta = 1

        'insert into collaudi
        strSQL = "INSERT INTO collaudi (codditt, ce_annocoll, ce_progrcoll, ce_codtcon, ce_codpcam," & _
                 " ce_codclaq, ce_codlivq, ce_dtcontr, ce_qtadacon, ce_qtacontr, ce_result," & _
                 " ce_qtanconf, ce_insdati, ce_trattato, ce_rilev, ce_codtdif, ce_numdif)" & _
                 " VALUES (" & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & ", " & _
                 NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno) & ", " & lProg & ", " & _
                 nCodtcon & ", " & nCodpcam & ", " & nCodclaq & ", " & nCodlivq & ", " & _
                 "null, " & CDblSQL(dQta) & ", 0, 'C', " & _
                 "0, 'N', 'N', null, 0, 0)"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'insert into collsotl
        strSQL = "INSERT INTO COLLSOTL (codditt, csl_annocoll, csl_progrcoll, csl_sottolotto," & _
                 " csl_result, csl_qtacontr, csl_codtdif)" & _
                 " VALUES (" & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & ", " & _
                 NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno) & ", " & lProg & ", 0," & _
                 " 'C', 0, 0)"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'insert into valcoll (valori dei controlli da fare collegati al sottolotto 0 - ovvero sottolotto standard)
        strSQL = "SELECT DISTINCT ct_codtcon, ct_ncampo, ct_tipcamp, ct_descamp" & _
                " FROM camtcon INNER JOIN comtcon ON camtcon.codditt = comtcon.codditt " & _
                " AND camtcon.ct_codtcon = comtcon.vt_codtcon" & _
                " AND camtcon.ct_ncampo = comtcon.vt_ncampo" & _
                " WHERE comtcon.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                " AND camtcon.ct_codtcon = " & nCodtcon & _
                " UNION " & _
                "SELECT DISTINCT ct_codtcon, ct_ncampo, ct_tipcamp, ct_descamp" & _
                " FROM camtcon LEFT OUTER JOIN comtcon ON camtcon.codditt = comtcon.codditt AND camtcon.ct_codtcon = comtcon.vt_codtcon" & _
                " AND camtcon.ct_ncampo = comtcon.vt_ncampo" & _
                " WHERE camtcon.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                " AND camtcon.ct_codtcon = " & nCodtcon & _
                " AND vt_codtcon Is Null AND vt_ncampo is Null" & _
                " ORDER BY ct_codtcon, ct_ncampo"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        For i = 0 To dttTmp.Rows.Count - 1
          strValcombo = " "
          strValdesval = " "
          strValresult = "C"
          If NTSCInt(dttTmp.Rows(i)!ct_tipcamp) = 7 Or NTSCInt(dttTmp.Rows(i)!ct_tipcamp) = 20 Then
            strSQL = "SELECT TOP 1 vt_valore, vt_desval, vt_result " & _
                    " FROM camtcon INNER JOIN comtcon ON camtcon.codditt = comtcon.codditt " & _
                    " AND camtcon.ct_codtcon = comtcon.vt_codtcon AND camtcon.ct_ncampo = comtcon.vt_ncampo" & _
                    " WHERE camtcon.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                    " AND camtcon.ct_codtcon = " & nCodtcon & _
                    " AND ct_ncampo = " & dttTmp.Rows(i)!ct_ncampo.ToString & _
                    " AND ct_tipcamp = " & dttTmp.Rows(i)!ct_tipcamp.ToString & _
                    " ORDER BY vt_codtcon, vt_ncampo, vt_item"
            dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            If dttTmp1.Rows.Count > 0 Then
              If NTSCInt(dttTmp.Rows(i)!ct_tipcamp) = 7 Then
                strValresult = NTSCStr(dttTmp1.Rows(0)!vt_result)
              Else
                strValcombo = NTSCStr(dttTmp1.Rows(0)!vt_valore)
                strValdesval = NTSCStr(dttTmp1.Rows(0)!vt_desval)
                strValresult = NTSCStr(dttTmp1.Rows(0)!vt_result)
              End If
            End If
            dttTmp1.Clear()
          End If    'If NTSCInt(dttTmp.Rows(i)!ct_tipcamp) = 7 Or NTSCInt(dttTmp.Rows(i)!ct_tipcamp) = 20 Then

          strSQL = "INSERT INTO VALCOLL (codditt, alv_annocoll, alv_progrcoll, alv_ncampo," & _
                  " alv_valdouble, alv_valdata, alv_valtext, alv_valcombo, alv_tipcamp, alv_result," & _
                  " alv_codtcon, alv_desval, alv_descamp, avl_sottolotto)" & _
                  " VALUES (" & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & ", " & _
                  NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno) & ", " & lProg & ", " & _
                  dttTmp.Rows(i)!ct_ncampo.ToString & "," & _
                  " 0, NULL, NULL, " & CStrSQL(strValcombo) & ", " & dttTmp.Rows(i)!ct_tipcamp.ToString & ", " & _
                  CStrSQL(strValresult) & ", " & nCodtcon & ", " & CStrSQL(strValdesval) & ", " & _
                  CStrSQL(dttTmp.Rows(i)!ct_descamp) & ", 0)"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next    'For i = 0 To dttTmp.Rows.Count - 1
        dttTmp.Clear()

        'insert into keycoll
        strSQL = "INSERT INTO KEYCOLL (codditt, cm_annocoll, cm_progrcoll, cm_tipork, cm_anno," & _
                 " cm_serie, cm_numdoc, cm_riga, cm_rigaa, cm_fase, cm_primario)" & _
                 " VALUES (" & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & ", " & _
                 NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno) & ", " & lProg & ", " & _
                 CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & ", " & _
                 ds.Tables("TESTA").Rows(0)!et_anno.ToString & ", " & _
                 CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & ", " & _
                 ds.Tables("TESTA").Rows(0)!et_numdoc.ToString & ", " & dtrT!ec_riga.ToString & ", " & _
                 "0, " & dtrT!ec_fase.ToString & ", 'S')"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

PASSA:
      Next    'For Each dtrT As DataRow In ds.Tables("CORPO").Select("ec_flcom = 'S'")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp1.Clear()
    End Try
  End Function

  Public Overridable Function SalvaDocumentoStornaAnagraficaMatricole(ByVal strDitta As String, ByVal strTipork As String, _
                                              ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                              ByVal bDelete As Boolean, ByRef ds As DataSet, ByRef dbConn As DbConnection) As Boolean
    'in vb6 StornaAnagraficaMatricole
    'in fase di modifica/cancellazione documenti di magazzino aggiorno le anagrafiche matricole
    'se è una cancellazione storno le anagrafiche matricole
    'se è un aggiornamento e la matricola nel nuovo documento no ci sarà cancello
    'se è una modifica e nel nuovo documento la matricola sarà presente: aggiorno sempre la matricola: non cancello mai il record anche se potrei (tanto andrebbe reinserito durante il risalvataggio)
    'questa routine sbaglia se il nuovo documento di magazzino ha una causale che non è più di tipo tabcauc.tb_carfor = 1 o tabcauc.tb_scacli = 1

    Dim strSQL As String = ""
    Dim bCreaAggiornaAnMatricole As Boolean = False
    Dim dttTmp As New DataTable
    Dim dtrTTm() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim bOk As Boolean = False
    Dim bUpdate As Boolean = False
    Dim i As Integer = 0
    Dim nTipobf As Integer = 0

    Try
      '----------------------------------
      'Se il documento è un a 'Nota di prelievo', esce
      If strTipork = "W" Then Return True
      '---------------------------------
      strSQL = "SELECT tm_tipobf FROM testmag" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tm_tipork = " & CStrSQL(strTipork) & _
        " AND tm_anno = " & nAnno & _
        " AND tm_serie = " & CStrSQL(strSerie) & _
        " AND tm_numdoc = " & lNumdoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      nTipobf = NTSCInt(dttTmp.Rows(0)!tm_tipobf)
      dttTmp.Clear()
      dttTmp.Dispose()
      '----------------------------------
      'Legge se impostato il flag in 'Personalizzazione Customer Service' (TABPECS)
      strSQL = "SELECT tb_agganmatr, tb_agganmatrtpbf FROM tabpecs" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_codpecs = 1" & _
        " AND tb_agganmatr = 'S'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then
        If NTSCStr(dttTmp.Rows(0)!tb_agganmatrtpbf).Trim = "" Then
          bCreaAggiornaAnMatricole = True
        Else
          Dim strTmp() As String = NTSCStr(dttTmp.Rows(0)!tb_agganmatrtpbf).Trim.Split(";"c)
          For i = 0 To (strTmp.Length - 1)
            If NTSCInt(strTmp(i)) = nTipobf Then
              bCreaAggiornaAnMatricole = True
              Exit For
            End If
          Next
        End If
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '----------------------------------
      'Se non è settato il flag in 'Personlizzazione Customer Service', esce
      If bCreaAggiornaAnMatricole = False Then Return True

      '----------------------------------
      'inizia la routine vera e propria
      'ottengo l'elenco degli articoli e matricola del nuodo documento
      If bDelete = False Then
        dtrTTm = ds.Tables("MOVMATR").Select
      End If

      strSQL = "SELECT nnmatrics.*, tb_carfor, tb_scacli " & _
               " FROM ((keymag INNER JOIN movmatr ON keymag.codditt = movmatr.codditt AND " & _
               " keymag.km_tipork = movmatr.mma_tipork AND keymag.km_anno = movmatr.mma_anno AND " & _
               " keymag.km_serie = movmatr.mma_serie AND keymag.km_numdoc = movmatr.mma_numdoc AND " & _
               " keymag.km_riga = movmatr.mma_riga) " & _
               " INNER JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum) " & _
               " INNER JOIN nnmatrics ON movmatr.codditt = nnmatrics.codditt AND " & _
               " movmatr.mma_matric = nnmatrics.rl_matric AND " & _
               " keymag.km_codart = nnmatrics.rl_codart " & _
               " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
               " AND km_tipork = " & CStrSQL(strTipork) & _
               " AND km_anno = " & nAnno & _
               " AND km_serie = " & CStrSQL(strSerie) & _
               " AND km_numdoc = " & lNumdoc & _
               " AND (tb_carfor = 1 OR tb_scacli = 1)"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      For Each dtrT As DataRow In dttTmp.Rows
        bOk = False
        bUpdate = True
        'se devo annullare/aggiornare un documento da fornitore
        If NTSCInt(dtrT!tb_carfor) = 1 Then
          If NTSCInt(dtrT!rl_conto) <> 0 Then
            'posso solo aggiornare
            bUpdate = True
          Else
            'se sono in cancellazione posso cancellare
            If bDelete Then
              bUpdate = False
            Else
              'se sono in aggiornamento cancello solo se la matricola non è presente in ttmovmatr
              For i = 0 To dtrTTm.Length - 1
                dtrT1 = ds.Tables("CORPO").Select("ec_riga = " & dtrTTm(i)!mma_riga.ToString)
                If dtrT1.Length > 0 Then
                  If NTSCStr(dtrTTm(i)!mma_matric) = NTSCStr(dtrT!rl_matric) And NTSCStr(dtrT!rl_codart) = NTSCStr(dtrT1(0)!ec_codart) Then
                    bOk = True
                    Exit For
                  End If
                End If
              Next
              bUpdate = bOk
            End If
          End If

          If bUpdate Then
            strSQL = "UPDATE nnmatrics SET rl_dtacqf = null," & _
                    " rl_ultagg = " & CDataOraSQL(Now) & "," & _
                    " rl_opnome = " & CStrSQL(oApp.User.Nome) & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND rl_codart = " & CStrSQL(dtrT!rl_codart) & _
                    " AND rl_matric = " & CStrSQL(dtrT!rl_matric)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Else
            strSQL = "DELETE FROM nnmatrics " & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND rl_codart = " & CStrSQL(dtrT!rl_codart) & _
                    " AND rl_matric = " & CStrSQL(dtrT!rl_matric)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            strSQL = "DELETE FROM anaext" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND ax_conto = 0" & _
                " AND ax_coddest = 0" & _
                " AND ax_codlead = 0" & _
                " AND ax_tipork = 'M'" & _
                " AND ax_codart = " & CStrSQL(dtrT!rl_codart) & _
                " AND ax_matric = " & CStrSQL(dtrT!rl_matric)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If    'If bUpdate Then
        End If    'If dtrT!tb_carfor = 1 Then

        'se devo annullare/aggiornare un documento da cliente
        If NTSCInt(dtrT!tb_scacli) = 1 Then
          If dtrT!rl_dtacqf.Equals(DBNull.Value) = False Then
            'posso solo aggiornare
            bUpdate = True
          Else
            'se sono in cancellazione posso cancellare
            If bDelete Then
              bUpdate = False
            Else
              'se sono in aggiornamento cancello solo se la matricola non è presente in ttmovmatr
              For i = 0 To dtrTTm.Length - 1
                dtrT1 = ds.Tables("CORPO").Select("ec_riga = " & dtrTTm(i)!mma_riga.ToString)
                If dtrT1.Length > 0 Then
                  If NTSCStr(dtrTTm(i)!mma_matric) = NTSCStr(dtrT!rl_matric) And NTSCStr(dtrT!rl_codart) = NTSCStr(dtrT1(0)!ec_codart) Then
                    bOk = True
                    Exit For
                  End If
                End If
              Next
              bUpdate = bOk
            End If
          End If

          If bUpdate Then
            strSQL = "UPDATE nnmatrics SET rl_dtrilascio = null," & _
                    " rl_conto = 0, " & _
                    " rl_coddest = 0, " & _
                    " rl_ultagg = " & CDataOraSQL(Now) & "," & _
                    " rl_opnome = " & CStrSQL(oApp.User.Nome) & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND rl_codart = " & CStrSQL(dtrT!rl_codart) & _
                    " AND rl_matric = " & CStrSQL(dtrT!rl_matric)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Else
            strSQL = "DELETE FROM nnmatrics " & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND rl_codart = " & CStrSQL(dtrT!rl_codart) & _
                    " AND rl_matric = " & CStrSQL(dtrT!rl_matric)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            strSQL = "DELETE FROM anaext" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND ax_conto = 0" & _
                " AND ax_coddest = 0" & _
                " AND ax_codlead = 0" & _
                " AND ax_tipork = 'M'" & _
                " AND ax_codart = " & CStrSQL(dtrT!rl_codart) & _
                " AND ax_matric = " & CStrSQL(dtrT!rl_matric)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If    'If bUpdate Then
        End If    'If dtrT!tb_scacli = 1 Then

      Next    'For Each dtrT As DataRow In dttTmp.Rows
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoAggiornaAnagraficaMatricole(ByVal strDitta As String, _
                                                  ByVal strTipork As String, ByVal nAnno As Integer, _
                                                  ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                                  ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Dim bCreaAggiornaAnMatricole As Boolean = False
    Dim dttTmp As New DataTable
    Dim dttMovmag As New DataTable
    Dim dttNnmatrics As New DataTable
    Dim strCampiNnmatrics As String = ""
    Dim strCampiTabaext As String = ""
    Dim strCampiAnaext As String = ""
    Dim nTipobf As Integer = 0

    Try

      '---------------------------------
      'Se il documento è un a 'Nota di prelievo', esce
      If strTipork = "W" Then Return True
      '---------------------------------
      strSQL = "SELECT tm_tipobf FROM testmag" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tm_tipork = " & CStrSQL(strTipork) & _
        " AND tm_anno = " & nAnno & _
        " AND tm_serie = " & CStrSQL(strSerie) & _
        " AND tm_numdoc = " & lNumdoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      nTipobf = NTSCInt(dttTmp.Rows(0)!tm_tipobf)
      dttTmp.Clear()
      dttTmp.Dispose()
      '---------------------------------
      'Legge se impostato il flag in 'Personalizzazione Customer Service' (TABPECS)
      strSQL = "SELECT tb_agganmatr, tb_agganmatrtpbf FROM tabpecs" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_codpecs = 1" & _
        " AND tb_agganmatr = 'S'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then
        If NTSCStr(dttTmp.Rows(0)!tb_agganmatrtpbf).Trim = "" Then
          bCreaAggiornaAnMatricole = True
        Else
          Dim strTmp() As String = NTSCStr(dttTmp.Rows(0)!tb_agganmatrtpbf).Trim.Split(";"c)
          For i = 0 To (strTmp.Length - 1)
            If NTSCInt(strTmp(i)) = nTipobf Then
              bCreaAggiornaAnMatricole = True
              Exit For
            End If
          Next
        End If
      End If
      dttTmp.Clear()
      dttTmp.Dispose()

      '---------------------------------
      'Se non è settato il flag in 'Personlizzazione Customer Service', esce
      If bCreaAggiornaAnMatricole = False Then Return True

      '---------------------------------
      'Considera solo le righe di carico/scarico (KEYMAG.km_carscar <> 0)
      'che hanno almeno una riga in MOVMATR
      strSQL = "SELECT tm_coddest, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, mm_codart," & _
              " km_conto, km_aammgg, mma_quant, mma_matric, tb_scacli, tb_carfor" & _
              " FROM (((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc)" & _
              " INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga)" & _
              " INNER JOIN movmatr ON movmag.codditt = movmatr.codditt AND movmag.mm_tipork = movmatr.mma_tipork AND movmag.mm_anno = movmatr.mma_anno AND movmag.mm_serie = movmatr.mma_serie AND movmag.mm_numdoc = movmatr.mma_numdoc AND movmag.mm_riga = movmatr.mma_riga)" & _
              " INNER JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum" & _
              " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
              " AND km_tipork = " & CStrSQL(strTipork) & _
              " AND km_anno = " & nAnno & _
              " AND km_serie = " & CStrSQL(strSerie) & _
              " AND km_numdoc = " & lNumdoc & _
              " AND km_carscar <> 0" & _
              " AND (tb_scacli = 1 OR tb_carfor = 1)" & _
              " ORDER BY movmag.codditt, mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga"
      dttMovmag = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '---------------------------------
      'Cicla sulle righe di MOVMAG che hanno corrispondenze in MOVMATR
      If dttMovmag.Rows.Count > 0 Then
        '---------------------------------
        'Stringa contenente l'elenco dei campi di NNMATRICS
        strCampiNnmatrics = "codditt, rl_codart, rl_matric, rl_quant, rl_conto, rl_coddest," & _
                          " rl_dtrilascio, rl_datman1, rl_datman2, rl_codtipa, rl_incarass, rl_note," & _
                          " rl_ultagg, rl_opnome, rl_matrprod, rl_numcont, rl_dtacqf"
        '---------------------------------
        'Stringa contenente l'elenco dei campi di TABAEXT
        strCampiTabaext = "codditt, tb_tipork, tb_tipo1, tb_tipo2, tb_tipo3, tb_descr1," & _
                          " tb_descr2, tb_descr3, tb_descr4, tb_descr5, tb_descr6, tb_descr7, tb_descr8," & _
                          " tb_descr9, tb_descr10, tb_desext1, tb_desext2, tb_desext3, tb_memo1, tb_memo2," & _
                          " tb_data1, tb_data2, tb_data3, tb_data4, tb_data5, tb_num1, tb_num2, tb_num3," & _
                          " tb_num4, tb_num5, tb_num6, tb_num7, tb_num8, tb_num9, tb_num10, tb_check1," & _
                          " tb_check2, tb_check3, tb_check4, tb_check5, tb_check6, tb_check7, tb_check8," & _
                          " tb_check9, tb_check10, tb_combo1, tb_combo2, tb_combo3, tb_helptipo1, tb_helptipo2," & _
                          " tb_helptipo3, tb_helpcom1_A, tb_helpcom1_B, tb_helpcom1_C, tb_helpcom1_D," & _
                          " tb_helpcom1_E, tb_helpcom1_F, tb_helpcom1_G, tb_helpcom1_H, tb_helpcom1_I," & _
                          " tb_helpcom1_L, tb_helpcom2_A, tb_helpcom2_B, tb_helpcom2_C, tb_helpcom2_D," & _
                          " tb_helpcom2_E, tb_helpcom3_A, tb_helpcom3_B, tb_helpcom3_C, tb_helpcom3_D," & _
                          " tb_helpcom3_E, tb_helpcom3_F, tb_helpcom3_G, tb_helpcom3_H, tb_helpcom3_I," & _
                          " tb_helpcom3_L, tb_helpcom3_M, tb_helpcom3_N, tb_helpcom3_O, tb_helpcom3_P," & _
                          " tb_helpcom3_Q, tb_helpcom3_R, tb_helpcom3_S, tb_helpcom3_T , tb_helpcom3_U," & _
                          " tb_helpcom3_V, tb_destab1, tb_destab2, tb_destab3, tb_destab4, tb_destab5," & _
                          " tb_destab6"
        '---------------------------------
        'Stringa contenente l'elenco dei campi di ANAEXT
        strCampiAnaext = "codditt, ax_conto, ax_coddest, ax_codlead, ax_tipork, ax_tipo1," & _
                          " ax_tipo2, ax_tipo3, ax_descr1, ax_descr2, ax_descr3, ax_descr4, ax_descr5," & _
                          " ax_descr6, ax_descr7, ax_descr8, ax_descr9, ax_descr10, ax_desext1, ax_desext2," & _
                          " ax_desext3, ax_memo1, ax_memo2, ax_data1, ax_data2, ax_data3, ax_data4, ax_data5," & _
                          " ax_num1, ax_num2, ax_num3, ax_num4, ax_num5, ax_num6, ax_num7, ax_num8, ax_num9," & _
                          " ax_num10, ax_check1, ax_check2, ax_check3, ax_check4, ax_check5, ax_check6," & _
                          " ax_check7, ax_check8, ax_check9, ax_check10, ax_combo1, ax_combo2, ax_combo3," & _
                          " ax_ultagg, ax_opnome, ax_codart, ax_matric"
        '---------------------------------

        For Each dtrMM As DataRow In dttMovmag.Rows
          '-------------------------------
          'Controlla se esiste il record in NNMATRICS
          strSQL = "SELECT rl_codart, rl_matric FROM nnmatrics" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND rl_codart = " & CStrSQL(dtrMM!mm_codart) & _
                    " AND rl_matric = " & CStrSQL(dtrMM!mma_matric)
          dttNnmatrics = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          If dttNnmatrics.Rows.Count > 0 Then
            '---------------------------------
            'Se in NNMATRICS lo aggiorna
            'aggiorno la quatità solo per i documenti ricevuti, per poter gestire il caso:
            'prima regsitro il ddt emesso di vendita di 1 pezzo, po iregsitro il ddt ricevuto di 5 pezzi dove c'è anche il pezzo venduto
            If NTSCInt(dtrMM!tb_carfor) = 1 Then
              strSQL = "UPDATE nnmatrics" & _
                  " SET rl_quant = " & CDblSQL(NTSCDec(dtrMM!mma_quant)) & "," & _
                  " rl_ultagg = " & CDataOraSQL(Now) & "," & _
                  " rl_opnome = " & CStrSQL(oApp.User.Nome) & ", " & _
                  " rl_dtacqf = " & CDataSQL(NTSCDate(dtrMM!km_aammgg).ToShortDateString)
            End If
            If NTSCInt(dtrMM!tb_scacli) = 1 Then
              strSQL = "UPDATE nnmatrics" & _
                        " SET rl_conto = " & dtrMM!km_conto.ToString & "," & _
                        " rl_coddest = " & dtrMM!tm_coddest.ToString & "," & _
                        " rl_ultagg = " & CDataOraSQL(Now) & "," & _
                        " rl_opnome = " & CStrSQL(oApp.User.Nome) & ", " & _
                        " rl_dtrilascio = " & CDataSQL(NTSCDate(dtrMM!km_aammgg).ToShortDateString)
            End If
            strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                        " AND rl_codart = " & CStrSQL(dtrMM!mm_codart) & _
                        " AND rl_matric = " & CStrSQL(dtrMM!mma_matric)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Else
            '---------------------------------
            'Se in NNMATRICS non esiste:
            'Se è uno scarico, crea NNMATRICS impostando conto ed eventuale destinazione
            'Se è uno carico, crea NNMATRICS impostando conto e destinazione a zero
            If NTSCInt(dtrMM!tb_scacli) = 1 Or NTSCInt(dtrMM!tb_carfor) = 1 Then
              strSQL = "INSERT INTO nnmatrics (" & strCampiNnmatrics & ")" & _
                        " VALUES (" & CStrSQL(strDitta) & ", " & _
                        CStrSQL(dtrMM!mm_codart) & ", " & _
                        CStrSQL(dtrMM!mma_matric) & ", " & _
                        CDblSQL(NTSCDec(dtrMM!mma_quant)) & ", " & _
                        IIf(NTSCInt(dtrMM!tb_scacli) = 1, dtrMM!km_conto, 0).ToString & ", " & _
                        IIf(NTSCInt(dtrMM!tb_scacli) = 1, dtrMM!tm_coddest, 0).ToString & ", " & _
                        CDataSQL(IIf(NTSCInt(dtrMM!tb_scacli) = 1, NTSCDate(dtrMM!km_aammgg).ToShortDateString, "").ToString) & "," & _
                        " NULL, NULL, 0, 0, NULL, " & CDataOraSQL(Now) & ", " & _
                        CStrSQL(oApp.User.Nome) & ", NULL, 0, " & _
                        CDataSQL(IIf(NTSCInt(dtrMM!tb_carfor) = 1, NTSCDate(dtrMM!km_aammgg).ToShortDateString, "").ToString) & ")"
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              '----------------------------
              'Crea, se non esiste, il record in TABAEXT
              strSQL = "SELECT tb_tipork FROM tabaext" & _
                      " WHERE codditt = " & CStrSQL(strDitta) & _
                      " AND tb_tipork = 'M'"
              dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

              If dttTmp.Rows.Count = 0 Then
                strSQL = "INSERT INTO tabaext (" & strCampiTabaext & ")" & _
                          " VALUES (" & CStrSQL(strDitta) & ", 'M', "
                For i = 1 To 23
                  strSQL = strSQL & "NULL, "
                Next
                For i = 1 To 10
                  strSQL = strSQL & "0, "
                Next
                For i = 1 To 57
                  strSQL = strSQL & "NULL" & IIf(i < 57, ", ", ")").ToString
                Next
                Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              End If    'If dttTmp.Rows.Count > 0 Then
              dttTmp.Clear()

              '-----------------------------
              'Crea, se non esiste, il record in ANAEXT
              strSQL = "SELECT ax_tipork FROM anaext" & _
                        " WHERE codditt = " & CStrSQL(strDitta) & _
                        " AND ax_conto = 0" & _
                        " AND ax_coddest = 0" & _
                        " AND ax_codlead = 0" & _
                        " AND ax_tipork = 'M'" & _
                        " AND ax_codart = " & CStrSQL(dtrMM!mm_codart) & _
                        " AND ax_matric = " & CStrSQL(dtrMM!mma_matric)
              dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              If dttTmp.Rows.Count = 0 Then
                strSQL = "INSERT INTO anaext (" & strCampiAnaext & ")" & _
                        " VALUES (" & CStrSQL(strDitta) & ", 0, 0, 0, 'M',"
                For i = 1 To 16
                  strSQL = strSQL & " ' ',"
                Next
                For i = 1 To 7
                  strSQL = strSQL & " NULL,"
                Next
                For i = 1 To 10
                  strSQL = strSQL & " 0,"
                Next
                For i = 1 To 10
                  strSQL = strSQL & " 'N',"
                Next
                For i = 1 To 3
                  strSQL = strSQL & " ' ',"
                Next
                strSQL = strSQL & " " & CDataOraSQL(Now) & ", " & _
                          CStrSQL(oApp.User.Nome) & ", " & CStrSQL(dtrMM!mm_codart) & ", " & _
                          CStrSQL(dtrMM!mma_matric) & ")"
                Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              End If    'If dttTmp.Rows.Count > 0 Then
              dttTmp.Clear()

            End If
          End If    'If dttNnmatrics.Rows.Count > 0 Then
          dttNnmatrics.Clear()

        Next    'For Each dtrMM As DataRow In dttMovmag.Rows
      End If    'If dttMovmag.Rows.Count > 0 Then

      dttMovmag.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttMovmag.Clear()
      dttNnmatrics.Clear()
    End Try
  End Function


  '------------------

  Public Overridable Function DeleteDoc(ByVal strState As String, ByRef ds As DataSet, ByRef dbConn As DbConnection, _
                                      ByVal bModPM As Boolean, ByVal bModTCO As Boolean, _
                                      ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                      ByVal bNuovoDaImportExport As Boolean) As Boolean
    Try
      Return DeleteDoc(strState, ds, dbConn, bModPM, bModTCO, dtInizEser, bCarichiProdDaAvmg, bNuovoDaImportExport, _
                       False, False, False, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function
  Public Overridable Function DeleteDoc(ByVal strState As String, ByRef ds As DataSet, ByRef dbConn As DbConnection, _
                                        ByVal bModPM As Boolean, ByVal bModTCO As Boolean, _
                                        ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                        ByVal bNuovoDaImportExport As Boolean, ByVal bCa2 As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strState, ds, dbConn, bModPM, bModTCO, dtInizEser, bCarichiProdDaAvmg, _
                                             bNuovoDaImportExport, bCa2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        dbConn = CType(oIn(2), DbConnection)
        Return CBool(oOut)
      End If
      '----------------

      Return DeleteDoc(strState, ds, dbConn, bModPM, bModTCO, dtInizEser, bCarichiProdDaAvmg, bNuovoDaImportExport, _
                       bCa2, False, False, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function
  Public Overridable Function DeleteDoc(ByVal strState As String, ByRef ds As DataSet, ByRef dbConn As DbConnection, _
                                      ByVal bModPM As Boolean, ByVal bModTCO As Boolean, _
                                      ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                      ByVal bNuovoDaImportExport As Boolean, ByVal bCa2 As Boolean, ByVal bModSQ As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strState, ds, dbConn, bModPM, bModTCO, dtInizEser, bCarichiProdDaAvmg, _
                                             bNuovoDaImportExport, bCa2, bModSQ})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        dbConn = CType(oIn(2), DbConnection)
        Return CBool(oOut)
      End If
      '----------------

      Return DeleteDoc(strState, ds, dbConn, bModPM, bModTCO, dtInizEser, bCarichiProdDaAvmg, bNuovoDaImportExport, _
                       bCa2, bModSQ, False, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function
  Public Overridable Function DeleteDoc(ByVal strState As String, ByRef ds As DataSet, ByRef dbConn As DbConnection, _
                                        ByVal bModPM As Boolean, ByVal bModTCO As Boolean, _
                                        ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                        ByVal bNuovoDaImportExport As Boolean, ByVal bCa2 As Boolean, _
                                        ByVal bModSQ As Boolean, ByVal bTerzista As Boolean, ByVal strNetProDB As String) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strState, ds, dbConn, bModPM, bModTCO, dtInizEser, bCarichiProdDaAvmg, _
                                             bNuovoDaImportExport, bCa2, bModSQ, bTerzista, strNetProDB})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        dbConn = CType(oIn(2), DbConnection)
        Return CBool(oOut)
      End If
      '----------------

      Return DeleteDoc(strState, ds, dbConn, bModPM, bModTCO, dtInizEser, bCarichiProdDaAvmg, bNuovoDaImportExport, _
        bCa2, bModSQ, bTerzista, strNetProDB, True)

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function DeleteDoc(ByVal strState As String, ByRef ds As DataSet, ByRef dbConn As DbConnection, _
                                        ByVal bModPM As Boolean, ByVal bModTCO As Boolean, _
                                        ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                        ByVal bNuovoDaImportExport As Boolean, ByVal bCa2 As Boolean, _
                                        ByVal bModSQ As Boolean, ByVal bTerzista As Boolean, ByVal strNetProDB As String, _
                                        ByVal bFlagRielabFattDiffSuVariazioneDDT As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strState, ds, dbConn, bModPM, bModTCO, dtInizEser, bCarichiProdDaAvmg, _
                                             bNuovoDaImportExport, bCa2, bModSQ, bTerzista, strNetProDB, bFlagRielabFattDiffSuVariazioneDDT})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        dbConn = CType(oIn(2), DbConnection)
        Return CBool(oOut)
      End If
      '----------------

      Return DeleteDoc(strState, ds, dbConn, bModPM, bModTCO, dtInizEser, bCarichiProdDaAvmg, bNuovoDaImportExport, _
        bCa2, bModSQ, bTerzista, strNetProDB, bFlagRielabFattDiffSuVariazioneDDT, False, False)

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function DeleteDoc(ByVal strState As String, ByRef ds As DataSet, ByRef dbConn As DbConnection, _
                                        ByVal bModPM As Boolean, ByVal bModTCO As Boolean, _
                                        ByVal dtInizEser As DateTime, ByVal bCarichiProdDaAvmg As Boolean, _
                                        ByVal bNuovoDaImportExport As Boolean, ByVal bCa2 As Boolean, _
                                        ByVal bModSQ As Boolean, ByVal bTerzista As Boolean, ByVal strNetProDB As String, _
                                        ByVal bFlagRielabFattDiffSuVariazioneDDT As Boolean, ByVal bModDII As Boolean, _
                                        ByVal bCollegaMG_DI As Boolean) As Boolean
    'bSetStatoOrdlist: se impostata a true cancellando l'Documento non verrà riaperta l'eventuale RDA collegata
    'strState: N = new, D = delete, U = update
    Dim strSQL As String = ""
    'Dim factory As DbProviderFactory = Nothing
    'Dim sqlCmd As DbCommand = Nothing
    Dim lResult As Integer
    Dim strTipoProg As String = ""
    Dim dsMes As New DataSet
    Dim dttTmp As New DataTable

    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strState, ds, dbConn, bModPM, bModTCO, dtInizEser, bCarichiProdDaAvmg, _
                                             bNuovoDaImportExport, bCa2, bModSQ, bTerzista, strNetProDB})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        dbConn = CType(oIn(2), DbConnection)
        Return CBool(oOut)
      End If
      '----------------

      '---------------------------
      If strState = "D" Then
        '------------------------
        'Cancellazione eventuali records in ALLOLE
        If Not DeleteDocAllole(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                              ds.Tables("TESTA").Rows(0)!et_tipork.ToString, _
                              NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                              ds.Tables("TESTA").Rows(0)!et_serie.ToString, _
                              NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), False, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If

        '------------------------
        'Customer service: se posso porto indietro la situazione nella tabella dei rinnovi
        If Not DeleteDocCustomerServiceContratti(strState, ds, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If


        '------------------------
        'se posso porto il progressivo di tabnuma indietro di un numero
        'solo se il numero documento è uguale a quello di tabnuma ..)
        Select Case ds.Tables("TESTA").Rows(0)!et_tipork.ToString
          Case "A", "D", "E", "N", "£" : strTipoProg = "A"
          Case "B" : strTipoProg = "B"
          Case "C" : strTipoProg = "C"
          Case "M" : strTipoProg = "M"
          Case "Z" : strTipoProg = "Z"
          Case "L", "K", "J", "(" : strTipoProg = "L"
          Case "W" : strTipoProg = "W"
          Case "T" : strTipoProg = "T"
          Case "U" : strTipoProg = "U"
          Case "F", "I" : strTipoProg = "F"
          Case "S" : strTipoProg = "S"
        End Select
        If Not DelNuma(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                       strTipoProg, _
                       ds.Tables("TESTA").Rows(0)!et_serie.ToString, _
                       NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                       NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), _
                       dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If

        '------------------------
        'se il num. di protocollo è <> 0, cerco di tornare indietro di 1 (esattamente come per il num. documento)
        'se è contabilizzato non posso fare nulla, perchè se torno indietro sul numeratore senza cancellare la reg. incg
        'come faccio una nuova reg. di acquisto mi proporrebbe il protocollo già usato in CG!
        With ds.Tables("TESTA").Rows(0)
          If (!et_tipork.ToString = "L" Or !et_tipork.ToString = "J") And NTSCInt(!et_numprot) <> 0 And NTSCInt(!et_nregiva) <> 0 And _
             NTSCStr(!et_flcont) = "N" Then
            If Not DelNuma(!codditt.ToString, _
                           "P", _
                           "A" & NTSCStr(!et_alfpro) & NTSCInt(!et_nregiva).ToString("000"), _
                           NTSCInt(!et_anno), _
                           NTSCInt(!et_numprot), _
                           dbConn) Then
              If IsInTrans Then AnnullaTrans()
              dbConn.Close()
              Return False
            End If
          End If
        End With

        '-----------------------------------------------------------------------
        'cancello i riferimenti in avlavp
        If bCarichiProdDaAvmg And ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then
          'in vb6 DeleteAvlavp
          strSQL = "UPDATE avlavp SET " & _
                  "lce_anno = 0, " & _
                  "lce_serie = ' ', " & _
                  "lce_numdoc = 0, " & _
                  "lce_riga = 0, " & _
                  "lce_rigaa = 0, " & _
                  "lce_flevas = CASE WHEN lce_qtaes = 0 THEN  'C' ELSE lce_flevas END, " & _
                  "lce_stato = CASE WHEN lce_qtaes = 0 THEN  'S' ELSE 'C' END " & _
                  " WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                  " AND lce_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                  " AND lce_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                  " AND lce_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                  " AND lce_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      End If    'If strState = "D" Then

      If strState <> "N" Then
        If Not SalvaDocumentoStornaAnagraficaMatricole(NTSCStr(ds.Tables("TESTA").Rows(0)!codditt.ToString), _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno), _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc), _
                            CBool(IIf(strState = "D", True, False)), ds, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If
      End If    'If strState <> "N" Then

      '---------------------------
      'cancello il documento con la Stored procedure
      If bNuovoDaImportExport Then
        'è un documento creato da import/export ed in fase di salvataggio
        'devo solo cancellare testmag e movmag, visto che nessuna ha mai scritto keymag o aggiornato artpro/artprox/ ...
        strSQL = "DELETE FROM movmatr WHERE" & _
                 " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND mma_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND mma_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND mma_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND mma_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE FROM movcona WHERE" & _
                 " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND mma_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND mma_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND mma_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND mma_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE FROM movcla WHERE" & _
                 " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND mc_outipo = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND mc_ouanno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND mc_ouserie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND mc_ounum = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE FROM " & IIf(ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "W", "movprb", "movmag").ToString & " WHERE" & _
                 " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND mm_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND mm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND mm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND mm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE FROM movmagtc WHERE" & _
                 " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND mm_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND mm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND mm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND mm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then
          strSQL = "DELETE FROM movmatr WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND mma_tipork = " & CStrSQL("U") & _
                   " AND mma_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND mma_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND mma_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM movcona WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND mma_tipork = " & CStrSQL("U") & _
                   " AND mma_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND mma_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND mma_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM movcla WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND mc_outipo = " & CStrSQL("U") & _
                   " AND mc_ouanno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND mc_ouserie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND mc_ounum = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM " & IIf(ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "W", "movprb", "movmag").ToString & " WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND mm_tipork = " & CStrSQL("U") & _
                   " AND mm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND mm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND mm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM movmagtc WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND mm_tipork = " & CStrSQL("U") & _
                   " AND mm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND mm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND mm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM lavcent WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND lce_tipork = " & CStrSQL("T") & _
                   " AND lce_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND lce_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND lce_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM " & IIf(ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "W", "testprbc", "testmagc").ToString & " WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND tm_tipork = " & CStrSQL("U") & _
                   " AND tm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND tm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND tm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          strSQL = "DELETE FROM " & IIf(ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "W", "testprb", "testmag").ToString & " WHERE" & _
                   " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                   " AND tm_tipork = " & CStrSQL("U") & _
                   " AND tm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                   " AND tm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                   " AND tm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If    'If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then

        strSQL = "DELETE FROM " & IIf(ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "W", "testprbc", "testmagc").ToString & " WHERE" & _
                 " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND tm_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND tm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND tm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND tm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE FROM " & IIf(ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "W", "testprb", "testmag").ToString & " WHERE" & _
                 " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND tm_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND tm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND tm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND tm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Else
        'cancellazione normale

        '-------------------------------
        'netpro: devo memorizzare quali ordini erano stati evasi dal documento corrente per poi riaggiornare il MES 
        If (bTerzista = False And ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T") Or ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "W" Or strNetProDB = "" Then
          'gli op interni no, visto che è netpro che li avanza ed ha già tutta la situazione allineata
        Else
          dsMes.Tables.Add(ds.Tables("TESTA").Copy)
          With ds.Tables("TESTA").Rows(0)
            strSQL = "SELECT DISTINCT mm_ortipo, mm_oranno, mm_orserie, mm_ornum, mm_orriga" & _
                     " FROM movmag " & _
                     " WHERE codditt = " & CStrSQL(!codditt) & _
                     " AND mm_tipork = " & CStrSQL(!et_tipork) & _
                     " AND mm_anno = " & !et_anno.ToString & _
                     " AND mm_serie = " & CStrSQL(!et_serie) & _
                     " AND mm_numdoc = " & !et_numdoc.ToString & _
                     " AND mm_ornum <> 0 " & _
                     " AND mm_ortipo IN ('R', 'O', 'H')"
          End With
          OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CORPO_DEL", dsMes, dbConn)
        End If

        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then
          If bModTCO Then
            If Not AggArtproTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, "U", _
                              NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                              NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                              NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 1, dbConn) Then
              If IsInTrans Then AnnullaTrans()
              dbConn.Close()
              Return False
            End If
            If Not AggMovOrdTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, "U", _
                              NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                              NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                              NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 1, dbConn) Then
              If IsInTrans Then AnnullaTrans()
              dbConn.Close()
              Return False
            End If
          End If    'If bModTCO Then

          strSQL = "bussp_bsveboll9_fcancella " & _
                "'U', " & _
                CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & ", " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & ", " & _
                CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & ", " & _
                "1, " & _
                CDataSQL(dtInizEser) & ", " & _
                TrovaNdecSuPrzUn(0) & ", " & _
                CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & ", " & _
                CStrSQL(IIf(bModTCO = True, "S", "N").ToString)
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If lResult = -1 Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Throw (New NTSException(oApp.Tr(Me, 128526341060870000, "Si è verificato un errore in fase di cancellazione scarico a produzione: Documento non cancellato")))
          End If
        End If    'If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then

        If bModTCO And Not (ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "A" And ds.Tables("TESTA").Rows(0)!et_proforma.ToString = "S") Then
          If Not AggArtproTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 1, dbConn) Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Return False
          End If
          If Not AggMovOrdTC(ds.Tables("TESTA").Rows(0)!codditt.ToString, _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_anno.ToString), _
                            NTSCStr(ds.Tables("TESTA").Rows(0)!et_serie.ToString), _
                            NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString), 1, dbConn) Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Return False
          End If
        End If    'If bModTCO And Not (ds.Tables("TESTA").Rows(0)!et_tipork.T ...

        If Not DeleteDocCOLLAUDI(strState, ds, bModSQ, dbConn) Then Return False

        '--------------------------------------------------------------------------------------------------------------
        'Dichiarazioni di intento
        If bModDII And bCollegaMG_DI And _
           ds.Tables("TESTA").Rows(0)!et_tipork.ToString <> "Z" And _
           ds.Tables("TESTA").Rows(0)!et_tipork.ToString <> "W" Then

          'Passo i vecchi valori della dichiarazione...
          'In cancellazione, controlllo e passo i vecchi valori della dichiarazione di intento
          Dim dttOldDich As New DataTable
          strSQL = " SELECT tm_dianno, tm_dinumero, an_tipo" & _
                   " FROM (testmagc INNER JOIN testmag ON testmagc.codditt = testmag.codditt AND testmagc.tm_tipork = testmag.tm_tipork AND testmagc.tm_anno = testmag.tm_anno AND testmagc.tm_serie = testmag.tm_serie AND testmagc.tm_numdoc = testmag.tm_numdoc) " & _
                   " INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto" & _
                   " WHERE testmagc.codditt = " & CStrSQL(ds.Tables("TESTAC").Rows(0)!codditt) & _
                   " AND testmagc.tm_tipork = " & CStrSQL(ds.Tables("TESTAC").Rows(0)!tm_tipork) & _
                   " AND testmagc.tm_anno = " & NTSCInt(ds.Tables("TESTAC").Rows(0)!tm_anno) & _
                   " AND testmagc.tm_serie = " & CStrSQL(ds.Tables("TESTAC").Rows(0)!tm_serie) & _
                   " AND testmagc.tm_numdoc = " & NTSCInt(ds.Tables("TESTAC").Rows(0)!tm_numdoc)
          dttOldDich = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If NTSCInt(dttOldDich.Rows(0)!tm_dianno) <> 0 And NTSCInt(dttOldDich.Rows(0)!tm_dinumero) <> 0 Then
            If Not GestisciFlagChiusura("C", ds.Tables("TESTA").Rows(0)!codditt.ToString, dttOldDich.Rows(0)!an_tipo.ToString, NTSCInt(dttOldDich.Rows(0)!tm_dianno), NTSCInt(dttOldDich.Rows(0)!tm_dinumero), dbConn) Then
              If IsInTrans Then AnnullaTrans()
              dbConn.Close()
              Return False
            End If
          End If
        End If
        '--------------------------------------------------------------------------------------------------------------


        strSQL = "bussp_bsveboll9_fcancella " & _
                  CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & ", " & _
                  CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & ", " & _
                  CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & ", " & _
                  CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & ", " & _
                  "1, " & _
                  CDataSQL(dtInizEser) & ", " & _
                  TrovaNdecSuPrzUn(0) & ", " & _
                  CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & ", " & _
                  CStrSQL(IIf(bModTCO = True, "S", "N").ToString)
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = -1 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(oApp.Tr(Me, 127791221369375000, "Si è verificato un errore in fase di cancellazione Documento: Documento non cancellato")))
        End If
        'se ho il modulo di CA2 abilitato, rimarco le fatture differite come 'da non rielaborare' vsito che tanto se c'è la ca2 non posso modificare/cancellare i ddt
        If bCa2 And NTSCInt(ds.Tables("TESTA").Rows(0)!et_numfat) <> 0 Then
          strSQL = "UPDATE testmag SET tm_flagiva_1 = null " & _
                   " WHERE testmag.tm_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tiporkfat.ToString) & _
                   " AND testmag.tm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_annfat.ToString) & _
                   " AND testmag.tm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_alffat.ToString) & _
                   " AND testmag.tm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numfat.ToString) & _
                   " AND testmag.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If    'If bCa2 Then

        '----------------
        'se sto evadendo IC, OF, OP esterni devo aggiornare anche netpro
        If (bTerzista = False And ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T") Or _
            ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "W" Then
          'gli op interni no, visto che è netpro che li avanza ed ha già tutta la situazione allineata
        Else
          'If Not DeleteDocAggNetPro(dsMes, strState, strNetProDB, dbConn) Then NOOOO: la DeleteDocAggNetPro cancella gli ordini: io devo solo aggiornare l'ordinato e l'evaso!!!!
          If Not SalvaOrdineAggNetPro(dsMes, strState, strNetProDB, "", dbConn) Then
            If IsInTrans Then AnnullaTrans()
            dbConn.Close()
            Return False
          End If
        End If

      End If    'If bNuovoDaImportExport Then

      'cancello schetrasp
      strSQL = "DELETE FROM schetrasp WHERE" & _
         " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
         " AND sct_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
         " AND sct_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
         " AND sct_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
         " AND sct_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
      lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'cancello priana2 e pria2comp
      If bCa2 Then
        strSQL = "DELETE FROM pria2comp WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
         " AND pac_progr IN (SELECT pa2_progr FROM priana2 WHERE " & _
         " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
         " AND pa2_numreg = 0 " & _
         " AND (pa2_mmtipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork)
        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then strSQL += " OR pa2_mmtipork = 'U' "
        strSQL += ") AND pa2_mmanno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND pa2_mmserie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND pa2_mmnumdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "DELETE FROM priana2 WHERE " & _
                 " codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND pa2_numreg = 0 " & _
                 " AND (pa2_mmtipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork)
        If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "T" Then strSQL += " OR pa2_mmtipork = 'U' "
        strSQL += ") AND pa2_mmanno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND pa2_mmserie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND pa2_mmnumdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bFlagRielabFattDiffSuVariazioneDDT = False Then
        Select Case NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork)
          Case "B", "M", "F", "T"
            With ds.Tables("TESTA").Rows(0)
              strSQL = "UPDATE testmag" & _
                " SET tm_flagiva_1 = NULL" & _
                " WHERE codditt = " & CStrSQL(NTSCStr(!codditt)) & _
                " AND tm_tipork = " & CStrSQL(NTSCStr(!et_tiporkfat)) & _
                " AND tm_anno = " & NTSCInt(!et_annfat) & _
                " AND tm_serie = " & CStrSQL(NTSCStr(!et_alffat)) & _
                " AND tm_numdoc = " & NTSCInt(!et_numfat)
            End With
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End Select
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True

    Catch ex As Exception
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dsMes.Clear()
    End Try
  End Function
  Public Overridable Function DeleteDocCOLLAUDI(ByVal strState As String, ByRef ds As DataSet, _
                                                ByVal bModSQ As Boolean, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim strRigheDaCanc As String = ""
    Dim strWhere As String = ""
    Dim dttTmp As New DataTable
    Dim dtrT1() As DataRow = Nothing
    Dim bOk As Boolean = False
    Try
      'sistema qualità: cancello i collaudi (solo se non è un aggiornamento)
      If Not bModSQ Then Return True

      strWhere = " WHERE keycoll.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND cm_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND cm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND cm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND cm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString)

      If strState = "D" Then
        'cancellazione definitiva
      Else
        'cancellazione per risalvataggio: 
        'devo cancellare solo le righe cancellate o con codice articolo o quantità diversa
        'da quella del documento aperto: cerco le righe da trattare
        strSQL = "SELECT mm_riga, mm_codart, mm_fase, mm_quant" & _
                 " FROM movmag WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                 " AND mm_tipork = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_tipork.ToString) & _
                 " AND mm_anno = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_anno.ToString) & _
                 " AND mm_serie = " & CStrSQL(ds.Tables("TESTA").Rows(0)!et_serie.ToString) & _
                 " AND mm_numdoc = " & CDblSQL(ds.Tables("TESTA").Rows(0)!et_numdoc.ToString) & _
                 " ORDER BY mm_riga"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        For Each dtrT As DataRow In dttTmp.Rows
          dtrT1 = ds.Tables("CORPO").Select("ec_riga = " & NTSCInt(dtrT!mm_riga))
          If dtrT1.Length = 0 Then
            strRigheDaCanc += NTSCInt(dtrT!mm_riga).ToString & ", "
          Else
            If NTSCStr(dtrT1(0)!ec_codart) <> NTSCStr(dtrT!mm_codart) Or _
               NTSCInt(dtrT1(0)!ec_fase) <> NTSCInt(dtrT!mm_fase) Or _
               NTSCDec(dtrT1(0)!ec_quant) <> NTSCDec(dtrT!mm_quant) Then
              strRigheDaCanc += NTSCInt(dtrT!mm_riga).ToString & ", "
            End If
          End If
        Next
        dttTmp.Clear()

        If strRigheDaCanc.Trim = "" Then
          'nel documento non sono state cancellate righe o cambiati articoli: mantengo tutti i collaudi
          Return True
        Else
          strRigheDaCanc = strRigheDaCanc.Substring(0, strRigheDaCanc.Length - 2)
          strWhere += " AND cm_riga IN (" & strRigheDaCanc & ")"
        End If
      End If

      'se è una bolla di movimentaz. interna con causale doppia non devo cancellare valcoll/collsotl/collaudi, ma solo keycoll
      'questo perchè la bolla di movim interna generata da BNSQGBTC crea un record in keycoll ceh punta al collaudo già esistente
      'e se cancello questa bolla di movim. interna cancellerei anche il collaudo (che invece deve essere cancellato
      'solo dal documento che effettivametnet lo ha generato (ddt ricevuto/carico da prod/ecc)
      bOk = True
      If Not ValCodiceDb(ds.Tables("TESTA").Rows(0)!et_causale.ToString, ds.Tables("TESTA").Rows(0)!codditt.ToString, "TABCAUM", "N", "", dttTmp) Then Return False
      If dttTmp.Rows.Count > 0 Then
        If NTSCInt(dttTmp.Rows(0)!tb_causec) <> 0 Then bOk = False
      End If
      dttTmp.Clear()

      If bOk Then
        strSQL = "DELETE valcoll " & _
                 " FROM valcoll INNER JOIN keycoll ON valcoll.codditt = keycoll.codditt " & _
                 " AND valcoll.alv_annocoll = keycoll.cm_annocoll " & _
                 " AND valcoll.alv_progrcoll = keycoll.cm_progrcoll " & _
                 strWhere
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE collsotl " & _
                 " FROM collsotl INNER JOIN keycoll ON collsotl.codditt = keycoll.codditt " & _
                 " AND collsotl.csl_annocoll = keycoll.cm_annocoll " & _
                 " AND collsotl.csl_progrcoll = keycoll.cm_progrcoll " & _
                 strWhere
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        strSQL = "DELETE collaudi " & _
                 " FROM collaudi INNER JOIN keycoll ON collaudi.codditt = keycoll.codditt " & _
                 " AND collaudi.ce_annocoll = keycoll.cm_annocoll " & _
                 " AND collaudi.ce_progrcoll = keycoll.cm_progrcoll " & _
                 strWhere
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      strSQL = "DELETE FROM keycoll" & strWhere
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function DeleteDocCustomerServiceContratti(ByVal strState As String, ByRef ds As DataSet, _
                                                                ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dPrezzo As Decimal = 0
    Dim strDtUltFatt As String = ""
    Dim nMesi As Integer = 0
    Dim bAggNnmovctra As Boolean = False   'se true devo aggiornare anche Nnmovctra perchè fatturazione analitica
    Dim dRappo As Decimal = 0
    Try
      If NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc) < 0 Then Return True 'fatture di prova!!!

      '------------------------
      'customer service: se è l'ultima fattura fatta di un rinnovo, 
      'porto indietro i dati di fatturazione del rinnovo
      If NTSCInt(ds.Tables("TESTA").Rows(0)!et_numcontr.ToString) <> 0 And _
         NTSCInt(ds.Tables("TESTA").Rows(0)!et_rinnid.ToString) <> 0 And _
         NTSCInt(ds.Tables("TESTA").Rows(0)!et_codchia) = 0 Then
        'si può fare
      Else
        Return True
      End If

      strSQL = "SELECT tm_rinnid FROM testmag " & _
               " WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt) & _
               " AND tm_codchia = 0" & _
               " AND tm_numcontr = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_numcontr) & _
               " AND tm_rinnid > " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_rinnid)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count > 0 Then Return True 'non è l'ultima fattura fatta: l'operatore dovrà sistemarsi il rinnovo a mano

      dttTmp.Clear()
      'devo capire se stornare in NNRINNOVI o in NNMOVCTRA

      '----------------------
      'calcolo il prezzo totale
      For Each dtrT As DataRow In ds.Tables("CORPO").Rows
        dPrezzo += NTSCDec(dtrT!ec_prezzo)
      Next

      '----------------------
      'calcolo la data fine precedente
      strSQL = "SELECT nnr_canonefat, nnc_fatratei, nnr_tpdetcan, nnr_datadec, nnr_dtfcultf, nnr_periodfc " & _
               " FROM nncontras INNER JOIN nnrinnovi ON nncontras.codditt = nnrinnovi.codditt AND nncontras.nnc_numcontr = nnrinnovi.nnr_numcontr " & _
               " WHERE nncontras.codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt) & _
               " AND nnc_numcontr = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_numcontr) & _
               " AND nnr_rinnid = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_rinnid)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttTmp.Rows.Count = 0 Then Return True
      If NTSCStr(dttTmp.Rows(0)!nnr_dtfcultf).Trim = "" Then Return True
      If NTSCDec(dttTmp.Rows(0)!nnr_canonefat) = 0 Then Return True
      If NTSCStr(dttTmp.Rows(0)!nnr_tpdetcan) <> "S" Then
        bAggNnmovctra = True
        dRappo = dPrezzo / NTSCDec(dttTmp.Rows(0)!nnr_canonefat)
      End If

      If NTSCStr(dttTmp.Rows(0)!nnc_fatratei) = "S" Then
        Select Case NTSCStr(dttTmp.Rows(0)!nnr_periodfc)
          Case "U"   '--- Semestre
            Select Case Month(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf))
              Case 12
                strDtUltFatt = IntSetDate("30/06/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)))
              Case Else
                strDtUltFatt = IntSetDate("31/12/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)) - 1)
            End Select
          Case "R"   '--- Quadrimestre
            Select Case Month(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf))
              Case 12
                strDtUltFatt = IntSetDate("31/08/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)))
              Case 8
                strDtUltFatt = IntSetDate("30/04/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)))
              Case Else
                strDtUltFatt = IntSetDate("31/12/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)) - 1)
            End Select
          Case "T"   '--- Ttrimestre
            Select Case Month(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf))
              Case 12
                strDtUltFatt = IntSetDate("30/09/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)))
              Case 9
                strDtUltFatt = IntSetDate("30/06/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)))
              Case 6
                strDtUltFatt = IntSetDate("31/03/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)))
              Case Else
                strDtUltFatt = IntSetDate("31/12/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)) - 1)
            End Select
          Case "B"   '--- Bimestre
            Select Case Month(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf))
              Case 12
                strDtUltFatt = IntSetDate("31/10/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)))
              Case 10
                strDtUltFatt = IntSetDate("31/08/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)))
              Case 8
                strDtUltFatt = IntSetDate("30/06/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)))
              Case 6
                strDtUltFatt = IntSetDate("30/04/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)))
              Case 4
                strDtUltFatt = IntSetDate("28/02/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)))
                strDtUltFatt = FineMese(strDtUltFatt)
              Case Else
                strDtUltFatt = IntSetDate("31/12/" & Year(NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf)) - 1)
            End Select
          Case "M"   '--- Mese
            strDtUltFatt = NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf).AddMonths(-1).ToShortDateString
            strDtUltFatt = FineMese(NTSCStr(strDtUltFatt))
        End Select
      Else
        Select Case NTSCStr(dttTmp.Rows(0)!nnr_periodfc)
          Case "A" : nMesi = 12
          Case "U" : nMesi = 6
          Case "R" : nMesi = 4
          Case "T" : nMesi = 3
          Case "B" : nMesi = 2
          Case "M" : nMesi = 1
        End Select
        strDtUltFatt = NTSCDate(dttTmp.Rows(0)!nnr_dtfcultf).AddMonths(nMesi * -1).ToShortDateString
        strDtUltFatt = FineMese(NTSCStr(strDtUltFatt))
      End If

      If NTSCDate(strDtUltFatt) < NTSCDate(dttTmp.Rows(0)!nnr_datadec).AddDays(-1) Then
        strDtUltFatt = NTSCDate(dttTmp.Rows(0)!nnr_datadec).AddDays(-1).ToShortDateString
      End If

      If NTSCStr(dttTmp.Rows(0)!nnr_periodfc) = "X" Then strDtUltFatt = "" 'fatturazione con rata unica
      dttTmp.Clear()

      '-------------------
      'aggiorno nnmovctra
      'visto che non c'è un legame tra articoli fatturati ed articoli del corpo del contratto,
      'diminuisco l'importo fatturato in proporzione
      If bAggNnmovctra Then
        strSQL = "UPDATE nnmovctra SET "
        'se non ci sono altre fatture, tolgo il flag di 'fatturato'
        If NTSCInt(ds.Tables("TESTA").Rows(0)!et_rinnid.ToString) = 1 Then
          strSQL += " nnr_fatturato = 'N', "
        End If
        strSQL += " nnr_canonefat = nnr_canonefat - (nnr_canonefat * " & CDblSQL(dRappo) & ") " & _
                  " WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                  " AND nnr_numcontr = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_numcontr.ToString).ToString & _
                  " AND nnr_rinnid = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_rinnid.ToString).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'arrotondo
        strSQL = "UPDATE nnmovctra SET nnr_canonefat = round(nnr_canonefat, " & TrovaNdecSuPrzUn(0) & ") " & _
                  " WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                  " AND nnr_numcontr = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_numcontr.ToString).ToString & _
                  " AND nnr_rinnid = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_rinnid.ToString).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      '-------------------
      'aggiorno nnrinnovi
      strSQL = "UPDATE nnrinnovi SET "
      'se non ci sono altre fatture, tolgo il flag di 'fatturato'
      If NTSCInt(ds.Tables("TESTA").Rows(0)!et_rinnid.ToString) = 1 Then
        strSQL += " nnr_fatturato = 'N', "
      End If
      strSQL += " nnr_dtfcultf = " & CDataSQL(strDtUltFatt) & ", " & _
                " nnr_canonefat = nnr_canonefat - " & CDblSQL(dPrezzo) & " " & _
                " WHERE codditt = " & CStrSQL(ds.Tables("TESTA").Rows(0)!codditt.ToString) & _
                " AND nnr_numcontr = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_numcontr.ToString).ToString & _
                " AND nnr_rinnid = " & NTSCInt(ds.Tables("TESTA").Rows(0)!et_rinnid.ToString).ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

#End Region


  Public Overridable Function CalcolaScontiTestataTrovaSconti(ByVal strDitta As String, ByVal lConto As Integer, _
                                                              ByVal strDatdoc As String, ByVal dQta As Decimal, _
                                                              ByRef dScont1 As Decimal, ByRef dScont2 As Decimal, _
                                                              ByVal lClaSconCli As Integer, ByRef bTrovati As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      dScont1 = 0
      dScont2 = 0
      bTrovati = False

      strSQL = "SELECT so_scont1, so_scont2 FROM sconti" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND so_conto = " & lConto.ToString & _
              " AND so_coddest = 0" & _
              " AND so_clscar = 999 AND so_datagg <= " & CDataSQL(strDatdoc) & _
              " AND so_datscad >= " & CDataSQL(strDatdoc) & _
              " AND so_daquant <= " & CDblSQL(dQta) & " AND so_aquant >= " & CDblSQL(dQta) & _
              " ORDER BY so_datagg ASC "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dScont1 = NTSCDec(dttTmp.Rows(0)!so_scont1)
        dScont2 = NTSCDec(dttTmp.Rows(0)!so_scont2)
        bTrovati = True
      Else
        strSQL = "SELECT so_scont1, so_scont2 FROM sconti" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND so_clscan = " & lClaSconCli.ToString & _
                " AND so_clscar = 999 AND so_datagg <= " & CDataSQL(strDatdoc) & _
                " AND so_datscad >= " & CDataSQL(strDatdoc) & _
                " AND so_daquant <= " & CDblSQL(dQta) & " AND so_aquant >= " & CDblSQL(dQta) & _
                " ORDER BY so_datagg ASC "
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          dScont1 = NTSCDec(dttTmp.Rows(0)!so_scont1)
          dScont2 = NTSCDec(dttTmp.Rows(0)!so_scont2)
          bTrovati = True
        End If
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function SalvaDocumentoBeforeDelete(ByRef ds As DataSet, ByVal strState As String, ByRef strError As String, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaDocumentoBeforeChiudiTrans(ByRef ds As DataSet, ByVal strState As String, ByRef strError As String, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  '------------------

  Public Overridable Function GetQueryStampaPdf(ByVal strDitta As String, ByVal strTipoDoc As String, _
                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As String
    Dim strTmp As String = ""
    Try

      strTmp = " SELECT tm_tipork as tipork, tm_anno as anno, tm_serie as serie, tm_numdoc as numero, " & _
             "tm_conto as conto, tm_valuta as valuta, tm_scorpo as scorpo, tm_codagen as agente, tm_coddest AS destin " & _
             " FROM testmag " & _
             " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
             " AND tm_tipork = " & CStrSQL(strTipoDoc) & _
             " AND tm_anno = " & nAnno & _
             " AND tm_serie = " & CStrSQL(strSerie) & _
             " AND tm_numdoc = " & lNumdoc.ToString & _
             " ORDER BY tm_valuta, tm_scorpo, tm_tipork, tm_anno, tm_serie, tm_numdoc "
      If strTipoDoc = "W" Then strTmp = strTmp.Replace("testmag", "testprb")
      Return strTmp

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTebmagaTerzFormTabcent(ByVal strDitta As String, ByVal nCodcent As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      strSQL = "SELECT tabmaga.* " & _
               " FROM tabcent LEFT JOIN TABMAGA ON tabcent.codditt = TABMAGA.codditt " & _
               " AND tabcent.tb_magterz = TABMAGA.tb_codmaga" & _
               " WHERE tabcent.codditt  =" & CStrSQL(strDitta) & _
               " AND tb_codcent = " & nCodcent.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArticoFigliconai(ByVal strDitta As String, ByVal strCodart As String, _
                                              ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artcona.*, ar_perqta as xx_perqta " & _
              " FROM artcona INNER JOIN artico ON artcona.codditt = artico.codditt AND artcona.ak_codart = artico.ar_codart" & _
              " WHERE artcona.codditt = " & CStrSQL(strDitta) & _
              " AND ak_codart = " & CStrSQL(strCodart) & " ORDER BY ak_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function



  Public Overridable Function GetQuaevaORD(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                           ByVal strTipoDocORD As String, _
                                           ByVal nAnnoORD As Integer, ByVal strSerieORD As String, ByVal lNumdocORD As Integer, _
                                           ByVal nRigaORD As Integer, ByRef QuaEvasa As Decimal, ByRef QuaDisEvasa As Decimal, _
                                           ByRef ColDisEvasa As Decimal, ByRef ValDisEvasa As Decimal, ByRef dttOut As DataTable) As Boolean
    'rileva la quantità evasa degli ordini fornitori/impegni clienti collegati all'Documento in corso
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strTabella As String = ""
    Try
      If strTipoDoc <> "W" Then
        strTabella = "movmag"
      Else
        strTabella = "movprb"
      End If

      strSQL = "SELECT mo_quant - mo_quaeva " & IIf(strTipoDoc = "W", "- mo_quapre", "").ToString & " as QuaDiff, " & _
               " mo_colli - mo_coleva " & IIf(strTipoDoc = "W", "- mo_colpre", "").ToString & " AS ColDiff, " & _
               " mo_valore, mo_flevas, " & _
               " mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga " & _
               " FROM movord WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mo_tipork = " & CStrSQL(strTipoDocORD) & _
               " AND mo_anno = " & nAnnoORD.ToString & _
               " AND mo_serie = " & CStrSQL(strSerieORD) & _
               " AND mo_numord = " & lNumdocORD.ToString & _
               " AND mo_riga = " & nRigaORD.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se è un documento in modifica determino quanta qta era stata evasa con questo documento
      If lNumdoc <> 0 Then
        strSQL = "SELECT Sum(mm_quant) AS QuaEvasa, Sum(mm_qtadisimp) AS QuaDisEvasa, Sum(mm_coldisimp) AS ColDisEvasa, " & _
              " Sum(mm_valdisimp) AS ValDisEvasa " & _
              " FROM " & strTabella & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND mm_anno = " & nAnno.ToString & _
              " AND mm_serie = " & CStrSQL(strSerie) & _
              " AND mm_numdoc = " & lNumdoc.ToString & _
              " AND mm_ortipo = " & CStrSQL(strTipoDocORD) & _
              " AND mm_oranno = " & nAnnoORD.ToString & _
              " AND mm_orserie = " & CStrSQL(strSerieORD) & _
              " AND mm_ornum = " & lNumdocORD.ToString & _
              " AND mm_orriga = " & nRigaORD.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          QuaEvasa = NTSCDec(dttTmp.Rows(0)!QuaEvasa)
          QuaDisEvasa = NTSCDec(dttTmp.Rows(0)!QuaDisEvasa)
          ColDisEvasa = NTSCDec(dttTmp.Rows(0)!ColDisEvasa)
          ValDisEvasa = NTSCDec(dttTmp.Rows(0)!ValDisEvasa)
        End If
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
  Public Overridable Function GetQuaevaNOTA(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                         ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                         ByVal strTipoDocNOTA As String, _
                                         ByVal nAnnoNOTA As Integer, ByVal strSerieNOTA As String, ByVal lNumdocNOTA As Integer, _
                                         ByVal nRigaNOTA As Integer, ByRef QuaEvasa As Decimal, ByRef QuaDisEvasa As Decimal, _
                                         ByRef ColDisEvasa As Decimal, ByRef ValDisEvasa As Decimal, ByRef dttOut As DataTable) As Boolean
    'rileva la quantità evasa della nota di prelievo collegati all'Documento in corso
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT mm_quant - mm_nprquaeva as QuaDiff, " & _
               " mm_colli - mm_nprcoleva AS ColDiff, mm_nprvalore, mm_nprflevas, " & _
               " mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga " & _
               " FROM movprb WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(strTipoDocNOTA) & _
               " AND mm_anno = " & nAnnoNOTA.ToString & _
               " AND mm_serie = " & CStrSQL(strSerieNOTA) & _
               " AND mm_numdoc = " & lNumdocNOTA.ToString & _
               " AND mm_riga = " & nRigaNOTA.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se è un documento in modifica determino quanta qta era stata evasa con questo documento
      If lNumdoc <> 0 Then
        strSQL = "SELECT Sum(mm_quant) AS QuaEvasa, Sum(mm_npqtadis) AS QuaDisEvasa, " & _
              " Sum(mm_npcoldis) AS ColDisEvasa, Sum(mm_npvaldis) AS ValDisEvasa " & _
              " FROM movmag " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND mm_anno = " & nAnno.ToString & _
              " AND mm_serie = " & CStrSQL(strSerie) & _
              " AND mm_numdoc = " & lNumdoc.ToString & _
              " AND mm_nptipo = " & CStrSQL(strTipoDocNOTA) & _
              " AND mm_npanno = " & nAnnoNOTA.ToString & _
              " AND mm_npserie = " & CStrSQL(strSerieNOTA) & _
              " AND mm_npnum = " & lNumdocNOTA.ToString & _
              " AND mm_npriga = " & nRigaNOTA.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          QuaEvasa = NTSCDec(dttTmp.Rows(0)!QuaEvasa)
          QuaDisEvasa = NTSCDec(dttTmp.Rows(0)!QuaDisEvasa)
          ColDisEvasa = NTSCDec(dttTmp.Rows(0)!ColDisEvasa)
          ValDisEvasa = NTSCDec(dttTmp.Rows(0)!ValDisEvasa)
        End If
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
  Public Overridable Function GetQuaevaAttivit(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                         ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                         ByVal strTipoDocORD As String, _
                                         ByVal nAnnoORD As Integer, ByVal strSerieORD As String, ByVal lNumdocORD As Integer, _
                                         ByVal nRigaORD As Integer, ByVal nFaseORD As Integer, ByRef QuaEvasa As Decimal, _
                                         ByRef QuaDisEvasa As Decimal, ByRef dttOut As DataTable) As Boolean
    'rileva la quantità evasa degli ordini fornitori/impegni clienti collegati all'Documento in corso
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT at_qtapr - at_qtaes AS QuantDiff, at_flevas " & _
               " FROM attivit WHERE codditt = " & CStrSQL(strDitta) & _
               " AND at_tipork = " & CStrSQL(strTipoDocORD) & _
               " AND at_anno = " & nAnnoORD.ToString & _
               " AND at_serie = " & CStrSQL(strSerieORD) & _
               " AND at_numord = " & lNumdocORD.ToString & _
               " AND at_riga = " & nRigaORD.ToString & _
               " AND at_fase = " & nFaseORD.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se è un documento in modifica determino quanta qta era stata evasa con questo documento
      If lNumdoc <> 0 Then
        strSQL = "SELECT Sum(lce_qtaes) AS QuantBuonaEva, Sum(lce_qtascart) AS QuantScartEva,  Sum(lce_qtadisimp) AS QuantEvasa " & _
              " FROM lavcent " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lce_tipork = " & CStrSQL(strTipoDoc) & _
              " AND lce_anno = " & nAnno.ToString & _
              " AND lce_serie = " & CStrSQL(strSerie) & _
              " AND lce_numdoc = " & lNumdoc.ToString & _
              " AND lce_ortipo = " & CStrSQL(strTipoDocORD) & _
              " AND lce_oranno = " & nAnnoORD.ToString & _
              " AND lce_orserie = " & CStrSQL(strSerieORD) & _
              " AND lce_ornum = " & lNumdocORD.ToString & _
              " AND lce_orriga = " & nRigaORD.ToString & _
              " AND lce_orfase = " & nFaseORD.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          QuaEvasa = NTSCDec(dttTmp.Rows(0)!QuantBuonaEva) + NTSCDec(dttTmp.Rows(0)!QuantScartEva)
          QuaDisEvasa = NTSCDec(dttTmp.Rows(0)!QuantEvasa)
        End If
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
  Public Overridable Function GetQuaevaLavcentG(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                       ByVal strTipoDocG As String, _
                                       ByVal nAnnoG As Integer, ByVal strSerieG As String, ByVal lNumdocG As Integer, _
                                       ByVal nRigaG As Integer, ByVal nRigaaG As Integer, ByRef dttOut As DataTable, _
                                       ByRef QuaEva As Decimal, ByRef QuascaEva As Decimal, _
                                       ByRef TattEva As Decimal, ByRef TeseEva As Decimal) As Boolean
    'rileva la quantità evasa degli ordini fornitori/impegni clienti collegati all'Documento in corso
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT lce_qtaes - lce_grqtaeva AS QuaDiff, " & _
               "lce_qtascart - lce_grqtaevasca AS QuascaDiff, " & _
               "lce_tempatte - lce_grtatteva AS TattDiff, " & _
               "lce_tempese - lce_grteseeva AS TeseDiff " & _
               " FROM lavcent WHERE codditt = " & CStrSQL(strDitta) & _
               " AND lce_tipork = " & CStrSQL(strTipoDocG) & _
               " AND lce_anno = " & nAnnoG.ToString & _
               " AND lce_serie = " & CStrSQL(strSerieG) & _
               " AND lce_numdoc = " & lNumdocG.ToString & _
               " AND lce_riga = " & nRigaG.ToString & _
               " AND lce_rigaa = " & nRigaaG.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se è un documento in modifica determino quanta qta era stata evasa con questo documento
      If lNumdoc <> 0 Then
        strSQL = "SELECT Sum(lce_qtaes) AS QuaEva, Sum(lce_qtascart) AS QuascaEva, " & _
              " Sum(lce_tempatte) AS TattEva, Sum(lce_tempese) AS TeseEva " & _
              " FROM lavcent " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lce_tipork = " & CStrSQL(strTipoDoc) & _
              " AND lce_anno = " & nAnno.ToString & _
              " AND lce_serie = " & CStrSQL(strSerie) & _
              " AND lce_numdoc = " & lNumdoc.ToString & _
              " AND lce_gtipork = " & CStrSQL(strTipoDocG) & _
              " AND lce_ganno = " & nAnnoG.ToString & _
              " AND lce_gserie = " & CStrSQL(strSerieG) & _
              " AND lce_gnumdoc = " & lNumdocG.ToString & _
              " AND lce_griga = " & nRigaG.ToString & _
              " AND lce_grigaa = " & nRigaaG.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          QuaEva = NTSCDec(dttTmp.Rows(0)!QuaEva)
          QuascaEva = NTSCDec(dttTmp.Rows(0)!QuascaEva)
          TattEva = NTSCDec(dttTmp.Rows(0)!TattEva)
          TeseEva = NTSCDec(dttTmp.Rows(0)!TeseEva)
        End If
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
  Public Overridable Function GetValevaAcconti(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                     ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                     ByVal strTipoDocA As String, _
                                     ByVal nAnnoA As Integer, ByVal strSerieA As String, ByVal lNumdocA As Integer, _
                                     ByRef dttOut As DataTable, _
                                     ByRef ConsidOld As Decimal, ByRef ConsidOldv As Decimal) As Boolean
    'rileva la quantità evasa degli acconti legati al Documento in corso
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT tm_totmerce - tm_totacceva AS Resid, " & _
               " tm_totmercev - tm_totaccevav AS Residv " & _
               " FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tm_tipork = " & CStrSQL(strTipoDocA) & _
               " AND tm_anno = " & nAnnoA.ToString & _
               " AND tm_serie = " & CStrSQL(strSerieA) & _
               " AND tm_numdoc = " & lNumdocA.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se è un documento in modifica determino quanta qta era stata evasa con questo documento
      If lNumdoc <> 0 Then
        strSQL = "SELECT Sum(mm_valore) AS ConsidOld, Sum(mm_valorev) AS ConsidOldv " & _
              " FROM movmag " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND mm_anno = " & nAnno.ToString & _
              " AND mm_serie = " & CStrSQL(strSerie) & _
              " AND mm_numdoc = " & lNumdoc.ToString & _
              " AND mm_actipo = " & CStrSQL(strTipoDocA) & _
              " AND mm_acanno = " & nAnnoA.ToString & _
              " AND mm_acserie = " & CStrSQL(strSerieA) & _
              " AND mm_acnum = " & lNumdocA.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          ConsidOld = NTSCDec(dttTmp.Rows(0)!ConsidOld)
          ConsidOldv = NTSCDec(dttTmp.Rows(0)!ConsidOldv)
        End If
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
  Public Overridable Function GetQuaevaORDtc(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                         ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                         ByVal strTipoDocORD As String, _
                                         ByVal nAnnoORD As Integer, ByVal strSerieORD As String, ByVal lNumdocORD As Integer, _
                                         ByVal nRigaORD As Integer, ByRef QuaEvasa() As Decimal, ByRef dttOut As DataTable) As Boolean
    'rileva la quantità evasa degli ordini fornitori/impegni clienti collegati all'Documento in corso
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strTabella As String = ""
    Dim i As Integer = 0
    Try
      If strTipoDoc = "W" Then
        strTabella = "movprb"
        strSQL = "SELECT mo_flevapre AS Flevas"
      Else
        strTabella = "movmag"
        strSQL = "SELECT mo_flevas AS Flevas"
      End If
      For i = 1 To 24
        strSQL += ", mo_quant" & i.ToString("00") & " - mo_quaeva" & i.ToString("00") & IIf(strTipoDoc = "W", " - mo_quapre" & i.ToString("00"), "").ToString & " as QuaDiff" & i.ToString("00")
      Next
      strSQL += " FROM movord INNER JOIN movordtc ON movordtc.codditt = movord.codditt " & _
               " AND movordtc.mo_tipork = movord.mo_tipork AND movordtc.mo_anno = movord.mo_anno " & _
               " AND movordtc.mo_serie = movord.mo_serie AND movordtc.mo_numord = movord.mo_numord " & _
               " AND movordtc.mo_riga = movord.mo_riga " & _
               " WHERE movord.codditt = " & CStrSQL(strDitta) & _
               " AND movord.mo_tipork = " & CStrSQL(strTipoDocORD) & _
               " AND movord.mo_anno = " & nAnnoORD.ToString & _
               " AND movord.mo_serie = " & CStrSQL(strSerieORD) & _
               " AND movord.mo_numord = " & lNumdocORD.ToString & _
               " AND movord.mo_riga = " & nRigaORD.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se è un documento in modifica determino quanta qta era stata evasa con questo documento
      If lNumdoc <> 0 Then
        strSQL = ""
        For i = 1 To 24
          strSQL += ", SUM(mm_qtadis" & i.ToString("00") & ") as QuaEvasa" & i.ToString("00")
        Next
        strSQL = "SELECT" & strSQL.Substring(1)
        strSQL += " FROM " & strTabella & " INNER JOIN movmagtc ON " & strTabella & ".codditt = movmagtc.codditt " & _
              " AND " & strTabella & ".mm_tipork = movmagtc.mm_tipork AND " & strTabella & ".mm_anno = movmagtc.mm_anno " & _
              " AND " & strTabella & ".mm_serie = movmagtc.mm_serie AND " & strTabella & ".mm_numdoc = movmagtc.mm_numdoc " & _
              " AND " & strTabella & ".mm_riga = movmagtc.mm_riga " & _
              " WHERE " & strTabella & ".codditt = " & CStrSQL(strDitta) & _
              " AND " & strTabella & ".mm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND " & strTabella & ".mm_anno = " & nAnno.ToString & _
              " AND " & strTabella & ".mm_serie = " & CStrSQL(strSerie) & _
              " AND " & strTabella & ".mm_numdoc = " & lNumdoc.ToString & _
              " AND " & strTabella & ".mm_ortipo = " & CStrSQL(strTipoDocORD) & _
              " AND " & strTabella & ".mm_oranno = " & nAnnoORD.ToString & _
              " AND " & strTabella & ".mm_orserie = " & CStrSQL(strSerieORD) & _
              " AND " & strTabella & ".mm_ornum = " & lNumdocORD.ToString & _
              " AND " & strTabella & ".mm_orriga = " & nRigaORD.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          For i = 1 To 24
            QuaEvasa(i) = NTSCDec(dttTmp.Rows(0)("QuaEvasa" & i.ToString("00")))
          Next
        End If
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
  Public Overridable Function GetQuaevaNOTAtc(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                         ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                         ByVal strTipoDocNOTA As String, _
                                         ByVal nAnnoNOTA As Integer, ByVal strSerieNOTA As String, ByVal lNumdocNOTA As Integer, _
                                         ByVal nRigaNOTA As Integer, ByRef QuaEvasa() As Decimal, ByRef dttOut As DataTable) As Boolean
    'rileva la quantità evasa degli ordini fornitori/impegni clienti collegati all'Documento in corso
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try
      strSQL = "SELECT mm_nprflevas"
      For i = 1 To 24
        strSQL += ", mm_quant" & i.ToString("00") & " - mm_nprquae" & i.ToString("00") & " as QuaDiff" & i.ToString("00")
      Next
      strSQL += " FROM movprb INNER JOIN movmagtc ON movprb.codditt = movmagtc.codditt " & _
               " AND movprb.mm_tipork = movmagtc.mm_tipork AND movprb.mm_anno = movmagtc.mm_anno " & _
               " AND movprb.mm_serie = movmagtc.mm_serie AND movprb.mm_numdoc = movmagtc.mm_numdoc " & _
               " AND movprb.mm_riga = movmagtc.mm_riga " & _
               " WHERE movprb.codditt = " & CStrSQL(strDitta) & _
               " AND movprb.mm_tipork = " & CStrSQL(strTipoDocNOTA) & _
               " AND movprb.mm_anno = " & nAnnoNOTA.ToString & _
               " AND movprb.mm_serie = " & CStrSQL(strSerieNOTA) & _
               " AND movprb.mm_numdoc = " & lNumdocNOTA.ToString & _
               " AND movprb.mm_riga = " & nRigaNOTA.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'se è un documento in modifica determino quanta qta era stata evasa con questo documento
      If lNumdoc <> 0 Then
        strSQL = ""
        For i = 1 To 24
          strSQL += ", SUM(mm_qtadis" & i.ToString("00") & ") as QuaEvasa" & i.ToString("00")
        Next
        strSQL = "SELECT" & strSQL.Substring(1)
        strSQL += " FROM movmag INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt " & _
              " AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno " & _
              " AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc " & _
              " AND movmag.mm_riga = movmagtc.mm_riga " & _
              " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
              " AND movmag.mm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND movmag.mm_anno = " & nAnno.ToString & _
              " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
              " AND movmag.mm_numdoc = " & lNumdoc.ToString & _
              " AND movmag.mm_nptipo = " & CStrSQL(strTipoDocNOTA) & _
              " AND movmag.mm_npanno = " & nAnnoNOTA.ToString & _
              " AND movmag.mm_npserie = " & CStrSQL(strSerieNOTA) & _
              " AND movmag.mm_npnum = " & lNumdocNOTA.ToString & _
              " AND movmag.mm_npriga = " & nRigaNOTA.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          For i = 1 To 24
            QuaEvasa(i) = NTSCDec(dttTmp.Rows(0)("QuaEvasa" & i.ToString("00")))
          Next

        End If
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

  Public Overridable Function GetTimeStampTestmag(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                                  ByVal nAnno As Integer, ByVal strSerie As String, _
                                                  ByVal lNumdoc As Integer) As DateTime
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      GetTimeStampTestmag = NTSCDate(IntSetDate("01/01/1900"))
      strSQL = "SELECT tm_ultagg FROM " & IIf(strTipoDoc = "W", "testprb", "testmag").ToString & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND tm_anno = " & nAnno & _
              " AND tm_serie = " & CStrSQL(strSerie) & _
              " AND tm_numdoc = " & lNumdoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then

        GetTimeStampTestmag = NTSCDate(dttTmp.Rows(0)!tm_ultagg)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetTimeStampTestmagOrdin(ByVal strDitta As String, ByVal dttEc As DataTable, _
                                                ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strSQLbase As String = ""
    Dim strTmp As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strTBuild As New System.Text.StringBuilder

    Dim nStart As Integer = 0

    Try
      'prendo su la prima riga di ordine con mo_ultagg <> mo_ultagg di quando ho aperto il documento che devo salvare...
      'eseguo una query ogni 100 righe, altrimenti il motore di sql si arrabbia
      dtrT = dttEc.Select("ec_ornum > 0")
      If dtrT.Length = 0 Then Return True

      strSQLbase = "SELECT mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_ultagg " & _
                   " FROM movord WHERE codditt = " & CStrSQL(strDitta)

RESTART:
      For i = 0 To dtrT.Length - 1

        If i + nStart >= dtrT.Length Then Return True

        If NTSCStr(dtrT(i + nStart)!xxo_orultagg).Trim <> "" And NTSCDate(dtrT(i + nStart)!xxo_orultagg) <> New Date(1900, 1, 1) Then
          strTBuild.Append(vbCrLf & "(mo_tipork = " & CStrSQL(dtrT(i + nStart)!ec_ortipo) & _
                    " AND mo_anno = " & dtrT(i + nStart)!ec_oranno.ToString & _
                    " AND mo_serie = " & CStrSQL(dtrT(i + nStart)!ec_orserie) & _
                    " AND mo_numord = " & dtrT(i + nStart)!ec_ornum.ToString & _
                    " AND mo_riga = " & dtrT(i + nStart)!ec_orriga.ToString & ") OR ")
          If i = 50 Or i + nStart = dtrT.Length - 1 Then
            strSQL = strSQLbase & " AND (" & strTBuild.ToString.Substring(0, strTBuild.Length - 4) & ")"
            strTBuild = New System.Text.StringBuilder
            GoTo ELABORA
          End If
        End If
      Next
      If strTBuild.Length = 0 Then Return True

      strSQL = strSQLbase & " AND (" & strTBuild.ToString.Substring(0, strTBuild.Length - 4) & ")"

ELABORA:
      dttOut.Clear()
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'contronto se il timestamp è cambiato
      For i = 0 To dtrT.Length - 1

        If i + nStart >= dtrT.Length Then Return True

        If NTSCStr(dtrT(i + nStart)!xxo_orultagg).Trim <> "" And NTSCDate(dtrT(i + nStart)!xxo_orultagg) <> New Date(1900, 1, 1) Then
          dtrT1 = dttOut.Select("mo_tipork = " & CStrSQL(dtrT(i + nStart)!ec_ortipo) & _
                               " AND mo_anno = " & dtrT(i + nStart)!ec_oranno.ToString & _
                               " AND mo_serie = " & CStrSQL(dtrT(i + nStart)!ec_orserie) & _
                               " AND mo_numord = " & dtrT(i + nStart)!ec_ornum.ToString & _
                               " AND mo_riga = " & dtrT(i + nStart)!ec_orriga.ToString)
          If dtrT1.Length > 0 Then
            If Math.Abs(NTSCDate(dtrT(i + nStart)!xxo_orultagg).Subtract(NTSCDate(dtrT1(0)!mo_ultagg)).Seconds) > 3 Then
              Return False
            End If
          End If
          If i + nStart = dtrT.Length - 1 Then
            Exit For
          End If

          If i = 50 Then
            If i + nStart < dtrT.Length - 1 Then
              nStart += i
              GoTo RESTART
            End If
          End If
        End If
      Next

      dttOut.Clear()
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttOut.Clear()
      dttOut.Dispose()
    End Try
  End Function
  Public Overridable Function GetTimeStampTestmagNotePrel(ByVal strDitta As String, ByVal dttEc As DataTable, _
                                              ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strSQLbase As String = ""
    Dim strTmp As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strTBuild As New System.Text.StringBuilder

    Dim nStart As Integer = 0

    Try
      'prendo su la prima riga di ordine con mo_ultagg <> mo_ultagg di quando ho aperto il documento che devo salvare...
      'eseguo una query ogni 500 righe, altrimenti il motore di sql si arrabbia
      dtrT = dttEc.Select("ec_npnum > 0")
      If dtrT.Length = 0 Then Return True

      strSQLbase = "SELECT Top 1 mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, mm_ultagg " & _
                   " FROM movprb WHERE codditt = " & CStrSQL(strDitta)

RESTART:
      For i = 0 To dtrT.Length - 1

        If i + nStart >= dtrT.Length Then Return True

        If NTSCStr(dtrT(i + nStart)!xxo_npultagg).Trim <> "" And NTSCDate(dtrT(i + nStart)!xxo_npultagg) <> New Date(1900, 1, 1) Then
          strTBuild.Append(vbCrLf & "(mm_tipork = " & CStrSQL(dtrT(i + nStart)!ec_nptipo) & _
          " AND mm_anno = " & dtrT(i + nStart)!ec_npanno.ToString & _
          " AND mm_serie = " & CStrSQL(dtrT(i + nStart)!ec_npserie) & _
          " AND mm_numdoc = " & dtrT(i + nStart)!ec_npnum.ToString & _
          " AND mm_riga = " & dtrT(i + nStart)!ec_npriga.ToString & ") OR ")
          If i = 50 Or i + nStart = dtrT.Length - 1 Then
            strSQL = strSQLbase & " AND (" & strTBuild.ToString.Substring(0, strTBuild.Length - 4) & ")"
            strTBuild = New System.Text.StringBuilder
            GoTo ELABORA
          End If
        End If
      Next
      If strTBuild.Length = 0 Then Return True

      strSQL = strSQLbase & " AND (" & strTBuild.ToString.Substring(0, strTBuild.Length - 4) & ")"

ELABORA:
      dttOut.Clear()
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'contronto se il timestamp è cambiato
      For i = 0 To dtrT.Length - 1

        If i + nStart >= dtrT.Length Then Return True

        If NTSCStr(dtrT(i + nStart)!xxo_npultagg).Trim <> "" And NTSCDate(dtrT(i + nStart)!xxo_npultagg) <> New Date(1900, 1, 1) Then
          dtrT1 = dttOut.Select("mm_tipork = " & CStrSQL(dtrT(i + nStart)!ec_nptipo) & _
                               " AND mm_anno = " & dtrT(i + nStart)!ec_npanno.ToString & _
                               " AND mm_serie = " & CStrSQL(dtrT(i + nStart)!ec_npserie) & _
                               " AND mm_numdoc = " & dtrT(i + nStart)!ec_npnum.ToString & _
                               " AND mm_riga = " & dtrT(i + nStart)!ec_npriga.ToString)
          If dtrT1.Length > 0 Then
            If Math.Abs(NTSCDate(dtrT(i + nStart)!xxo_npultagg).Subtract(NTSCDate(dtrT1(0)!mm_ultagg)).Seconds) > 3 Then
              Return False
            End If
          End If
          If i + nStart = dtrT.Length - 1 Then
            Exit For
          End If

          If i = 50 Then
            If i + nStart < dtrT.Length - 1 Then
              nStart += i
              GoTo RESTART
            End If
          End If
        End If
      Next

      dttOut.Clear()
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttOut.Clear()
      dttOut.Dispose()
    End Try
  End Function
  Public Overridable Function GetTimeStampTestmagRicFisc(ByVal strDitta As String, ByVal dttEc As DataTable, _
                                            ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strSQLbase As String = ""
    Dim strTmp As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strTBuild As New System.Text.StringBuilder
    Dim nRec As Integer = 0
    Dim nItem As Integer = 0
    Try
      'prendo su la prima riga di ordine con mo_ultagg <> mo_ultagg di quando ho aperto il documento che devo salvare...
      'eseguo una query ogni 500 righe, altrimenti il motore di sql si arrabbia
      dtrT = dttEc.Select("ec_prnum > 0")
      If dtrT.Length = 0 Then Return True

      strSQLbase = "SELECT Top 1 tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_ultagg " & _
                   " FROM testmag WHERE codditt = " & CStrSQL(strDitta)

      For i = 0 To dtrT.Length - 1
        If NTSCStr(dtrT(i)!xxo_orultagg).Trim <> "" Then
          nRec += 1
          If NTSCDate(dtrT(i)!xxo_orultagg).Year < 2000 Then
            'caso strano: ci sono i riferimenti a nota di prelievo, ma nota di prelievo cancellata (non dovrebbe neanche essere possibile!!!)
            strTmp = CDataOraSQL(NTSCDate(IntSetDate("01/01/2000")))
          Else
            strTmp = CDataOraSQL(NTSCDate(dtrT(i)!xxo_orultagg))
          End If
          strTBuild.Append(vbCrLf & "(tm_tipork = " & CStrSQL(dtrT(i)!ec_prtipo) & _
                    " AND tm_anno = " & dtrT(i)!ec_pranno.ToString & _
                    " AND tm_serie = " & CStrSQL(dtrT(i)!ec_prserie) & _
                    " AND tm_numdoc = " & dtrT(i)!ec_prnum.ToString & _
                    " AND abs(DATEDIFF ( s, tm_ultagg, " & strTmp & ")) > 3) OR ")
          strTmp = ""
          If nRec = 500 Then
            strTmp = strTBuild.ToString.Substring(0, strTBuild.Length - 4)
            strSQL = strSQLbase & " AND (" & strTmp & ")"

            dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            If dttOut.Rows.Count > 0 Then
              Return True
            End If
            nRec = 0
            strTBuild = New System.Text.StringBuilder
            strTmp = ""
          End If
        End If
      Next
      If strTBuild.Length = 0 Then Return True

      strTmp = strTBuild.ToString.Substring(0, strTBuild.Length - 4)
      strSQL = strSQLbase & " AND (" & strTmp & ")"

RIPROVA:
      Try
        dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Catch ex As Exception
        'CON UN DATABASE CON MILIONI DI RECORD ogni tanto questa query da errore
        'se la si fa rieseguire l'errore non viene dato
        'se si verfica l'errore specifico provo a rieseguirla
        If ex.Message.ToLower.Contains("datediff function resulted in an overflow") Then
          If nItem < 3 Then
            nItem += 1
            System.Threading.Thread.Sleep(100)
            GoTo RIPROVA
          End If
          Throw ex
        Else
          Throw ex
        End If
      End Try

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckCancelDocRiga_Ctolavoro(ByVal strDitta As String, ByVal strTipo As String, _
                                                ByVal nAnno As Integer, ByVal strSerie As String, _
                                                ByVal lNumdoc As Integer, ByVal lRiga As Integer, _
                                                ByRef dttMcla As DataTable, ByRef dttOut As DataTable, _
                                                ByRef dttOutT As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      '------------------------------
      'Controlla che non sia già usato su qualche documento di scarico (in MOVCLA)
      strSQL = "SELECT TOP 1 mc_outipo, mc_ouanno, mc_ouserie, mc_ounum, mc_ouriga FROM movcla" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mc_intipo = " & CStrSQL(strTipo) & _
        " AND mc_inanno = " & nAnno & _
        " AND mc_inserie = " & CStrSQL(strSerie) & _
        " AND mc_innum = " & lNumdoc & IIf(lRiga > 0, " AND mc_inriga = " & lRiga, "").ToString & _
        " AND (mc_outipo <> " & CStrSQL(strTipo) & _
          " OR mc_ouanno <> " & nAnno & _
          " OR mc_ouserie <> " & CStrSQL(strSerie) & _
          " OR mc_ounum <> " & lNumdoc & IIf(lRiga > 0, " OR mc_ouriga <> " & lRiga, "").ToString & ")"
      dttMcla = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      '------------------------------
      'Controlla che non sia già usato su qualche documento di scarico (campo mm_cl*)
      strSQL = "SELECT TOP 1 mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga FROM movmag" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mm_cltipo = " & CStrSQL(strTipo) & _
        " AND mm_clanno = " & nAnno & _
        " AND mm_clserie = " & CStrSQL(strSerie) & _
        " AND mm_clnum = " & lNumdoc & IIf(lRiga > 0, " AND mm_clriga = " & lRiga, "").ToString & _
        " AND (mm_tipork <> " & CStrSQL(strTipo) & _
          " OR mm_anno <> " & nAnno & _
          " OR mm_serie <> " & CStrSQL(strSerie) & _
          " OR mm_numdoc <> " & lNumdoc & IIf(lRiga > 0, " OR mm_riga <> " & lRiga, "").ToString & ")"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      '------------------------------
      'Se trattasi di una carico di produzione controlla che non sia già usato su qualche
      'documento (campo mm_cc*)
      If strTipo = "T" Then
        strSQL = "SELECT TOP 1 mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga FROM movmag" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND mm_cctipo = " & CStrSQL(strTipo) & _
          " AND mm_ccanno = " & nAnno & _
          " AND mm_ccserie = " & CStrSQL(strSerie) & _
          " AND mm_ccnum = " & lNumdoc & IIf(lRiga > 0, " AND mm_ccriga = " & lRiga, "").ToString & _
          " AND (mm_tipork <> " & CStrSQL(strTipo) & _
            " OR mm_anno <> " & nAnno & _
            " OR mm_serie <> " & CStrSQL(strSerie) & _
            " OR mm_numdoc <> " & lNumdoc & IIf(lRiga > 0, " OR mm_riga <> " & lRiga, "").ToString & ")"
        dttOutT = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function BloccaEvasione(ByVal strDitta As String, ByVal strOrtipo As String, ByVal nOranno As Integer, _
                                             ByVal strOrserie As String, ByVal lOrnum As Integer, ByVal lOrriga As Integer, _
                                             ByRef dttOut As DataTable) As Boolean
    '-------------------------------
    'restituisce gli estremi della nota di prelievo evasa in conto collegata alla riga d'Documento passata in input
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 mm_anno, mm_serie, mm_numdoc, mm_riga FROM movprb" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND mm_ortipo = " & CStrSQL(strOrtipo) & _
                " AND mm_oranno = " & nOranno & _
                " AND mm_orserie = " & CStrSQL(strOrserie) & _
                " AND mm_ornum = " & lOrnum & _
                " AND mm_orriga = " & lOrriga & _
                " AND mm_nprflevas = 'C'"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function BloccaEvasioneY(ByVal strDitta As String, ByVal strOrtipo As String, ByVal nOranno As Integer, _
                                           ByVal strOrserie As String, ByVal lOrnum As Integer, ByVal lOrriga As Integer, _
                                           ByRef dttOut As DataTable) As Boolean
    '-------------------------------
    'restituisce gli estremi della nota di prelievo evasa in conto collegata alla riga degli scarichi collegati della riga d'Documento passata in input
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 mm_anno, mm_serie, mm_numdoc" & _
              " FROM (movprb INNER JOIN movmag ON movprb.codditt = movmag.codditt AND movprb.mm_ortipo = movmag.mm_tipork AND movprb.mm_oranno = movmag.mm_anno AND movprb.mm_orserie = movmag.mm_serie AND movprb.mm_ornum = movmag.mm_numdoc AND movprb.mm_orriga = movmag.mm_riga)" & _
              " INNER JOIN movmag As movmag_1 ON movmag.codditt = movmag_1.codditt AND movmag.mm_tiporkor = movmag_1.mm_tipork AND movmag.mm_annoor = movmag_1.mm_anno AND movmag.mm_serieor = movmag_1.mm_serie AND movmag.mm_numdocor = movmag_1.mm_numdoc AND movmag.mm_rigaor = movmag_1.mm_riga" & _
              " WHERE movprb.codditt = " & CStrSQL(strDitta) & _
              " AND movmag.mm_rigaor = " & lOrriga & _
              " AND mm_ortipo = 'Y'" & _
              " AND mm_oranno = " & nOranno & _
              " AND mm_orserie = " & CStrSQL(strOrserie) & _
              " AND mm_ornum = " & lOrnum & _
              " AND mm_nprflevas = 'C'"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function ScriviMotransPerCreaNota(ByVal strDitta As String, ByVal lIIMotrans As Integer, ByVal dttEc As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Dim lResult As Integer = 0
    Dim dttMotrans As New DataTable

    Try
      '---------------------------------
      'riempio motrans solo per scrivere la nota di prelievo: una volta che bsorgnno verrà riscritto questa routine non servirà più
      dttMotrans = dttEc.Clone
      For i = 0 To dttEc.Rows.Count - 1
        dttMotrans.ImportRow(dttEc.Rows(i))
      Next
      dttMotrans.Columns.Remove("ec_tipork")
      dttMotrans.Columns.Remove("ec_anno")
      dttMotrans.Columns.Remove("ec_serie")
      dttMotrans.Columns.Remove("ec_numdoc")
      dttMotrans.Columns("xxo_qtadaass").ColumnName = "ec_qtadaass"
      dttMotrans.Columns("xxo_flevasass").ColumnName = "ec_flevasass"
      strSQL = "INSERT INTO motrans " & GetQueryInsertField(dttMotrans, "mm_", "", "ec_")
      strSQL = strSQL.Trim.Substring(0, strSQL.Trim.Length - 1) & ", instid) "
      For i = 0 To dttMotrans.Rows.Count - 1
        strSQLVal = GetQueryInsertValue(dttMotrans, dttMotrans.Rows(i), "mm_", "", "ec_")
        strSQLVal = strSQLVal.Trim.Substring(0, strSQLVal.Trim.Length - 1) & ", " & lIIMotrans & ")"
        lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttMotrans.Clear()
      dttEc.AcceptChanges()
    End Try
  End Function

#Region "Import note prelievo / Ordini ..."
  Public Overridable Function GetMovprb(ByVal strDitta As String, ByVal bEstremiOrdine As Boolean, _
                                        ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                        ByVal lnumdoc As Integer, ByVal nRiga As Integer, _
                                        ByRef dttOut As DataTable, ByRef dttOutTc As DataTable) As Boolean
    Try
      Return GetMovprb(strDitta, bEstremiOrdine, strTipork, nAnno, strSerie, lnumdoc, nRiga, dttOut, dttOutTc, True)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetMovprb(ByVal strDitta As String, ByVal bEstremiOrdine As Boolean, _
                                        ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                        ByVal lnumdoc As Integer, ByVal nRiga As Integer, _
                                        ByRef dttOut As DataTable, ByRef dttOutTc As DataTable, ByVal bModTco As Boolean) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bEstremiOrdine, strTipork, nAnno, strSerie, lnumdoc, nRiga, dttOut, dttOutTc, bModTco})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(7), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT movprb.*, movord.mo_ultagg, movord.mo_tctaglia, movord.mo_tcindtagl, 0 as xxo_tcindtaglf, ' ' as xxo_tctagliaf, " & _
                  " ar_unmis, ar_codnomc, ar_percvst, ar_prorig," & _
                  " ar_pesolor, ar_pesonet, ar_codiva, ar_gesfasi, ar_codtagl " & _
                  " FROM ((((tabmaga INNER JOIN movprb ON tabmaga.codditt = movprb.codditt AND tabmaga.tb_codmaga = movprb.mm_magaz)" & _
                  " INNER JOIN artico ON movprb.codditt = artico.codditt AND movprb.mm_codart = artico.ar_codart)" & _
                  " LEFT JOIN tabciva ON movprb.mm_codiva = tabciva.tb_codciva)" & _
                  " LEFT JOIN movord ON (movprb.codditt = movord.codditt) AND (movprb.mm_ortipo = movord.mo_tipork) AND (movprb.mm_oranno = movord.mo_anno) AND (movprb.mm_orserie = movord.mo_serie) AND (movprb.mm_ornum = movord.mo_numord) AND (movprb.mm_orriga = movord.mo_riga))" & _
                  " WHERE movprb.codditt = " & CStrSQL(strDitta)
      If bEstremiOrdine Then
        strSQL += " AND mm_ortipo = " & CStrSQL(strTipork) & _
                  " AND mm_oranno = " & nAnno & _
                  " AND mm_orserie = " & CStrSQL(strSerie) & _
                  " AND mm_ornum = " & lnumdoc
        If nRiga <> 0 Then strSQL += " AND mm_orriga = " & nRiga.ToString
        strSQL += "  AND mm_nprflevas <> 'S'"
      Else
        strSQL += " AND mm_tipork = " & CStrSQL(strTipork) & _
                  " AND mm_anno = " & nAnno & _
                  " AND mm_serie = " & CStrSQL(strSerie) & _
                  " AND mm_numdoc = " & lnumdoc
        If nRiga <> 0 Then strSQL += " AND mm_riga = " & nRiga.ToString
        'Esclude le note provenienti da ordini Y
        strSQL += " AND mm_ortipo <> 'Y' " & _
                 " AND mm_nprflevas <> 'S'"
      End If
      strSQL += " ORDER BY mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)


      '-----------------------------
      If bModTco Then
        strSQL = "SELECT movmagtc.* " & _
                " FROM movprb INNER JOIN movmagtc ON movprb.codditt = movmagtc.codditt " & _
                " AND movprb.mm_tipork = movmagtc.mm_tipork AND movprb.mm_anno = movmagtc.mm_anno " & _
                " AND movprb.mm_serie = movmagtc.mm_serie AND movprb.mm_numdoc = movmagtc.mm_numdoc " & _
                " AND movprb.mm_riga = movmagtc.mm_riga " & _
                " WHERE movprb.codditt = " & CStrSQL(strDitta)
        If bEstremiOrdine Then
          strSQL += " AND mm_ortipo = " & CStrSQL(strTipork) & _
                    " AND mm_oranno = " & nAnno & _
                    " AND mm_orserie = " & CStrSQL(strSerie) & _
                    " AND mm_ornum = " & lnumdoc
          If nRiga <> 0 Then strSQL += " AND mm_orriga = " & nRiga.ToString
          strSQL += "  AND mm_nprflevas <> 'S'"
        Else
          strSQL += " AND movprb.mm_tipork = " & CStrSQL(strTipork) & _
                    " AND movprb.mm_anno = " & nAnno & _
                    " AND movprb.mm_serie = " & CStrSQL(strSerie) & _
                    " AND movprb.mm_numdoc = " & lnumdoc
          If nRiga <> 0 Then strSQL += " AND movprb.mm_riga = " & nRiga.ToString
          'Esclude le note provenienti da ordini Y
          strSQL += " AND mm_ortipo <> 'Y' AND mm_nprflevas <> 'S'"
        End If
        'Esclude le note provenienti da ordini Y
        dttOutTc = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If Not GetMovmagU_TC(dttOut) Then Return False
      End If    'If bModTco Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetTestmag(ByVal strDitta As String, ByVal strTipork As String, _
                                      ByVal nAnno As Integer, ByVal strSerie As String, _
                                      ByVal lnumdoc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM testmag " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tm_tipork = " & CStrSQL(strTipork) & _
               " AND tm_anno = " & nAnno & _
               " AND tm_serie = " & CStrSQL(strSerie) & _
               " AND tm_numdoc = " & lnumdoc
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetTestord(ByVal strDitta As String, ByVal strTipo As String, _
                                     ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                     ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      strSQL = "SELECT * FROM testord " & _
               "WHERE codditt = " & CStrSQL(strDitta) & " " & _
               "AND td_tipork = '" & strTipo & "' AND td_anno = " & nAnno & _
               " AND td_serie = '" & strSerie & "' AND td_numord = " & lNumdoc
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetMovord(ByVal strDitta As String, ByVal strTipork As String, _
                                      ByVal nAnno As Integer, ByVal strSerie As String, _
                                      ByVal lnumdoc As Integer, ByVal nRiga As Integer, _
                                      ByRef dttOut As DataTable, ByRef dttOutTc As DataTable) As Boolean
    'se si aggiungono delle colonne nella select aggiungerle anche nella 'GetMovordY' qui sotto
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movord.*, ar_gesfasi, ar_codiva, 0 as xxo_tcindtaglf, ' ' as xxo_tctagliaf " & _
                  " FROM movord INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart " & _
                  " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                  " AND mo_tipork = " & CStrSQL(strTipork) & _
                  " AND mo_anno = " & nAnno & _
                  " AND mo_serie = " & CStrSQL(strSerie) & _
                  " AND mo_numord = " & lnumdoc
      If nRiga <> 0 Then strSQL += " AND mo_riga = " & nRiga.ToString
      strSQL += " AND mo_flevas <> 'S' ORDER BY mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "SELECT movordtc.* " & _
              " FROM movordtc INNER JOIN movord ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork " & _
              " AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord " & _
              " AND movord.mo_riga = movordtc.mo_riga " & _
              " WHERE movord.codditt = " & CStrSQL(strDitta) & _
              " AND movord.mo_tipork = " & CStrSQL(strTipork) & _
              " AND movord.mo_anno = " & nAnno & _
              " AND movord.mo_serie = " & CStrSQL(strSerie) & _
              " AND movord.mo_numord = " & lnumdoc
      If nRiga <> 0 Then strSQL += " AND movord.mo_riga = " & nRiga.ToString
      strSQL += " AND movord.mo_flevas <> 'S'"
      dttOutTc = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetMovordY(ByVal strDitta As String, ByVal strTipork As String, _
                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                    ByVal lnumdoc As Integer, ByVal nRigaOr As Integer, _
                                    ByRef dttOut As DataTable, ByRef dttLav As DataTable) As Boolean
    'se si aggiungono delle colonne nella select aggiungerle anche nella 'GetMovord' qui sopra
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movord.*, ar_gesfasi, ar_codiva, ar_codtagl, 0 as xxo_tcindtaglf, ' ' as xxo_tctagliaf" & _
                  " FROM movord INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart " & _
                  " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                  " AND mo_tipork = " & CStrSQL(strTipork) & _
                  " AND mo_anno = " & nAnno & _
                  " AND mo_serie = " & CStrSQL(strSerie) & _
                  " AND mo_numord = " & lnumdoc.ToString & _
                  " AND mo_rigaor = " & nRigaOr.ToString & _
                  " AND mo_flevas <> 'S' ORDER BY mo_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "SELECT attivit.*, assris.*" & _
              " FROM (attivit LEFT JOIN assris ON attivit.codditt = assris.codditt AND " & _
              " attivit.at_fase = assris.as_fase AND attivit.at_riga = assris.as_riga AND " & _
              " attivit.at_numord = assris.as_numord AND attivit.at_serie = assris.as_serie AND " & _
              " attivit.at_anno = assris.as_anno AND attivit.at_tipork = assris.as_tipork)" & _
              " WHERE attivit.codditt = " & CStrSQL(strDitta) & _
              " AND at_tipork = 'H'" & _
              " AND at_anno = " & nAnno & _
              " AND at_serie = " & CStrSQL(strSerie) & _
              " AND at_numord = " & lnumdoc.ToString & _
              " AND at_riga = " & nRigaOr.ToString & _
              " ORDER BY at_riga, at_fase"
      dttLav = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetLavcentFromOrd(ByVal strDitta As String, ByVal strTipork As String, _
                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                    ByVal lnumdoc As Integer, ByVal nRiga As Integer, _
                                    ByVal nFase As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM lavcent " & _
               "WHERE codditt = " & CStrSQL(strDitta) & _
               " AND lce_ortipo = " & CStrSQL(strTipork) & _
               " AND lce_oranno = " & nAnno & _
               " AND lce_orserie = " & CStrSQL(strSerie) & _
               " AND lce_ornum = " & lnumdoc & _
               " AND lce_orriga = " & nRiga & _
               " AND lce_orfase = " & nFase & _
               " AND lce_grflevas <> 'S' AND lce_tipork = 'G' " & _
               "ORDER BY lce_numdoc, lce_riga, lce_rigaa"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetMovmatrRigaNotaPrel(ByVal strDitta As String, _
                                                ByVal nNpAnno As Integer, ByVal strNpSerie As String, _
                                                ByVal lNpnumdoc As Integer, ByVal nNpRiga As Integer, _
                                                ByVal strTipork As String, ByVal nAnno As Integer, _
                                                ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                                ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      'prelevo le matricole collegata alla riga di documento passato (nota di preleivo
      strSQL = "SELECT * FROM movmatr WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mma_tipork  = 'W'" & _
               " AND mma_anno = " & nNpAnno.ToString & _
               " AND mma_serie = " & CStrSQL(strNpSerie) & _
               " AND mma_numdoc = " & lNpnumdoc.ToString & _
               " AND mma_riga = " & nNpRiga.ToString & _
               " ORDER BY mma_rigaa"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttOut.Rows.Count = 0 Then Return True

      'tolgo le matricole già evase in altri documenti, escluso quello in elaborazione
      For Each dtrT As DataRow In dttOut.Rows
        strSQL = "SELECT movmatr.* " & _
                 " FROM movmatr INNER JOIN movmag ON movmatr.codditt = movmag.codditt " & _
                 " AND movmatr.mma_tipork = movmag.mm_tipork AND movmatr.mma_anno = movmag.mm_anno " & _
                 " AND movmatr.mma_serie = movmag.mm_serie AND movmatr.mma_numdoc = movmag.mm_numdoc " & _
                 " AND movmatr.mma_riga = movmag.mm_riga " & _
                 " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                 " AND mm_nptipo  = 'W'" & _
                 " AND mm_npanno = " & dtrT!mma_anno.ToString & _
                 " AND mm_npserie = " & CStrSQL(dtrT!mma_serie) & _
                 " AND mm_npnum = " & dtrT!mma_numdoc.ToString & _
                 " AND mm_npriga = " & dtrT!mma_riga.ToString & _
                 " AND mma_matric  =" & CStrSQL(dtrT!mma_matric) & _
                 " AND NOT (mm_tipork = " & CStrSQL(strTipork) & _
                 " AND mm_anno = " & nAnno.ToString & _
                 " AND mm_serie = " & CStrSQL(strSerie) & _
                 " AND mm_numdoc = " & lNumdoc.ToString & ") " & _
                 " ORDER BY mma_rigaa"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        For Each dtrT1 As DataRow In dttTmp.Rows
          dtrT!mma_quant = NTSCDec(dtrT!mma_quant) - NTSCDec(dtrT1!mma_quant)
          If NTSCDec(dtrT!mma_quant) <= 0 Then
            dtrT.Delete()
            Exit For
          End If
        Next
        dttTmp.Clear()
      Next
      dttOut.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function GetMovordY_TC(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim nTcIndTaglF As Integer
    Dim dttDestagl As New DataTable
    Try


      For Each dtrRow As DataRow In dttOut.Rows

        strSQL = " SELECT * FROM movordtc" & _
                 " WHERE codditt = " & CStrSQL(dtrRow!codditt) & _
                 " AND mo_tipork = " & CStrSQL(dtrRow!mo_tipork) & _
                 " AND mo_anno = " & NTSCInt(dtrRow!mo_anno) & _
                 " AND mo_serie = " & CStrSQL(dtrRow!mo_serie) & _
                 " AND mo_numord = " & NTSCInt(dtrRow!mo_numord) & _
                 " AND mo_riga = " & NTSCInt(dtrRow!mo_riga)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then

          'Riempimento campo di MOVORDIMP
          'xxo_tcindtaglf
          nTcIndTaglF = 0

          '    nRiga = NTSCInt(dtrRow!mo_riga)
          If NTSCDec(dttTmp.Rows(0)!mo_quant01) <> 0 Then
            nTcIndTaglF = 1
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant02) <> 0 Then
            nTcIndTaglF = 2
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant03) <> 0 Then
            nTcIndTaglF = 3
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant04) <> 0 Then
            nTcIndTaglF = 4
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant05) <> 0 Then
            nTcIndTaglF = 5
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant06) <> 0 Then
            nTcIndTaglF = 6
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant07) <> 0 Then
            nTcIndTaglF = 7
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant08) <> 0 Then
            nTcIndTaglF = 8
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant09) <> 0 Then
            nTcIndTaglF = 9
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant10) <> 0 Then
            nTcIndTaglF = 10
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant11) <> 0 Then
            nTcIndTaglF = 11
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant12) <> 0 Then
            nTcIndTaglF = 12
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant13) <> 0 Then
            nTcIndTaglF = 13
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant14) <> 0 Then
            nTcIndTaglF = 14
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant15) <> 0 Then
            nTcIndTaglF = 15
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant16) <> 0 Then
            nTcIndTaglF = 16
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant17) <> 0 Then
            nTcIndTaglF = 17
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant18) <> 0 Then
            nTcIndTaglF = 18
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant19) <> 0 Then
            nTcIndTaglF = 19
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant20) <> 0 Then
            nTcIndTaglF = 20
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant21) <> 0 Then
            nTcIndTaglF = 21
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant22) <> 0 Then
            nTcIndTaglF = 22
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant23) <> 0 Then
            nTcIndTaglF = 23
          ElseIf NTSCDec(dttTmp.Rows(0)!mo_quant24) <> 0 Then
            nTcIndTaglF = 24
          End If

          dtrRow!xxo_tcindtaglf = NTSCInt(IIf(nTcIndTaglF = 0, dtrRow!mo_tcindtagl, nTcIndTaglF))
          ValCodiceDb(NTSCInt(dtrRow!ar_codtagl).ToString, NTSCStr(dtrRow!codditt), "TABTAGL", "N", "", dttDestagl)
          dtrRow!xxo_tctagliaf = NTSCStr(dttDestagl.Rows(0)("tb_dest" & dtrRow!xxo_tcindtaglf.ToString.PadLeft(2, "0"c))).ToUpper.Trim
          dtrRow.AcceptChanges()
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetMovmagU_TC(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim nTcIndTaglF As Integer
    Dim dttDestagl As New DataTable
    Try


      For Each dtrRow As DataRow In dttOut.Rows

        strSQL = " SELECT * FROM movmagtc" & _
                 " WHERE codditt = " & CStrSQL(dtrRow!codditt) & _
                 " AND mm_tipork = " & CStrSQL(dtrRow!mm_tipork) & _
                 " AND mm_anno = " & NTSCInt(dtrRow!mm_anno) & _
                 " AND mm_serie = " & CStrSQL(dtrRow!mm_serie) & _
                 " AND mm_numdoc = " & NTSCInt(dtrRow!mm_numdoc) & _
                 " AND mm_riga = " & NTSCInt(dtrRow!mm_riga)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then

          'Riempimento campo di MOVORDIMP
          'xxo_tcindtaglf
          nTcIndTaglF = 0

          '    nRiga = NTSCInt(dtrRow!mo_riga)
          If NTSCDec(dttTmp.Rows(0)!mm_quant01) <> 0 Then
            nTcIndTaglF = 1
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant02) <> 0 Then
            nTcIndTaglF = 2
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant03) <> 0 Then
            nTcIndTaglF = 3
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant04) <> 0 Then
            nTcIndTaglF = 4
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant05) <> 0 Then
            nTcIndTaglF = 5
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant06) <> 0 Then
            nTcIndTaglF = 6
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant07) <> 0 Then
            nTcIndTaglF = 7
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant08) <> 0 Then
            nTcIndTaglF = 8
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant09) <> 0 Then
            nTcIndTaglF = 9
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant10) <> 0 Then
            nTcIndTaglF = 10
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant11) <> 0 Then
            nTcIndTaglF = 11
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant12) <> 0 Then
            nTcIndTaglF = 12
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant13) <> 0 Then
            nTcIndTaglF = 13
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant14) <> 0 Then
            nTcIndTaglF = 14
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant15) <> 0 Then
            nTcIndTaglF = 15
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant16) <> 0 Then
            nTcIndTaglF = 16
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant17) <> 0 Then
            nTcIndTaglF = 17
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant18) <> 0 Then
            nTcIndTaglF = 18
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant19) <> 0 Then
            nTcIndTaglF = 19
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant20) <> 0 Then
            nTcIndTaglF = 20
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant21) <> 0 Then
            nTcIndTaglF = 21
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant22) <> 0 Then
            nTcIndTaglF = 22
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant23) <> 0 Then
            nTcIndTaglF = 23
          ElseIf NTSCDec(dttTmp.Rows(0)!mm_quant24) <> 0 Then
            nTcIndTaglF = 24
          End If

          dtrRow!xxo_tcindtaglf = NTSCInt(IIf(nTcIndTaglF = 0, dtrRow!mm_tcindtagl, nTcIndTaglF))
          ValCodiceDb(NTSCInt(dtrRow!ar_codtagl).ToString, NTSCStr(dtrRow!codditt), "TABTAGL", "N", "", dttDestagl)
          dtrRow!xxo_tctagliaf = NTSCStr(dttDestagl.Rows(0)("tb_dest" & dtrRow!xxo_tcindtaglf.ToString.PadLeft(2, "0"c))).ToUpper.Trim
          dtrRow.AcceptChanges()
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetOrdiniPerAutoevasione(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                                       ByVal lConto As Integer, ByVal nValuta As Integer, ByVal strScorpo As String, _
                                                       ByRef dttOut As DataTable) As Boolean
    'cerca gli ordini da evadere privilegiando quelli con data consegna uguale alla data del documento
    'e destinazione diversa = a quella impostata
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim strT1() As String = Nothing
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      strSQL = "SELECT TOP 1 td_conto " & _
              " FROM " & strJoinTestordMovord & _
              " WHERE testord.codditt = " & CStrSQL(strDitta) & _
              " AND mo_codart = " & CStrSQL(strCodart) & _
              " AND mo_fase = " & nFase & _
              " AND testord.td_sospeso = 'N'" & _
              " AND testord.td_conto = " & lConto & _
              " AND mo_flevas <> 'S'" & _
              " AND td_valuta = " & nValuta & _
              " AND td_scorpo = " & CStrSQL(strScorpo) & _
              " AND (testord.td_tipork = 'H' OR testord.td_tipork = 'O' Or testord.td_tipork = 'R' Or testord.td_tipork = 'X') "
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

  Public Overridable Function GetWhereHlri(ByVal strDitta As String, ByVal nValuta As Integer, ByVal lConto As Integer, _
                                           ByVal strScorpo As String, ByVal strDatdoc As String, ByVal strPrestServ As String) As String
    'zoom ricevute fiscali da riemettere
    Dim strSQL As String = ""
    Try
      'Costruisce la where
      strSQL = " tm_conto = " & lConto.ToString & _
               " AND testmag.tm_tipork = 'F' " & _
               " AND tm_salriemrf = 'N' " & _
               " AND tm_totdoc - tm_totomag <> 0 AND (tm_totdoc - tm_totomag) > (tm_pagato + tm_abbuono) " & _
               " AND tm_imponib_1 + tm_imponib_2 + tm_imponib_3 + tm_imponib_4 + tm_imponib_5 + tm_imponib_6 + tm_imponib_7 + tm_imponib_8 >= tm_totriemrf " & _
               " AND tb_flresocl = 'N' " & _
               " AND tm_datdoc <= " & CDataSQL(strDatdoc) & _
               " AND tb_prestserv = " & CStrSQL(strPrestServ)
      If strScorpo = "S" Then
        'scorporo
        strSQL = strSQL & " AND tm_scorpo = 'S' "
      Else
        If nValuta <> 0 Then
          'valuta
          strSQL = strSQL & " AND tm_valuta = " & nValuta.ToString
        Else
          'normale
          strSQL = strSQL & " AND tm_scorpo = 'N' AND tm_valuta = 0 "
        End If
      End If

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetWhereHltmAcconti(ByVal strDitta As String, ByVal bDocemesso As Boolean, ByVal nValuta As Integer, ByVal lConto As Integer, _
                                                 ByVal strScorpo As String, ByVal lCommeca As Integer, ByVal strDatdoc As String) As String
    Dim strSQL As String = ""
    Dim bCommeca As Boolean = False
    Try

      'Costruisce la where
      bCommeca = CBool(GetSettingBusDitt(strDitta, "Bsveboll", "Opzioni", ".", "SelAccontiCommeca", "0", " ", "0"))
      If bDocemesso Then
        strSQL = " tm_conto = " & lConto.ToString & " AND (testmag.tm_tipork = 'A' OR testmag.tm_tipork = 'F' OR testmag.tm_tipork = 'S') AND tm_salacceva = 'N' AND tm_totmerce >= tm_totacceva AND tb_flacconto = 'S' AND tm_datdoc <= " & CDataSQL(strDatdoc) & " "
      Else
        strSQL = " tm_conto = " & lConto.ToString & " AND testmag.tm_tipork = 'L' AND tm_salacceva = 'N' AND tm_totmerce >= tm_totacceva AND tb_flacconto = 'S' AND tm_datdoc <= " & CDataSQL(strDatdoc) & " "
      End If
      If bCommeca And lCommeca <> 0 Then strSQL = strSQL & " AND tm_commeca = " & lCommeca
      If strScorpo = "S" Then
        'scorporo
        strSQL = strSQL & " AND tm_scorpo = 'S' "
      Else
        If nValuta <> 0 Then
          'valuta
          strSQL = strSQL & " AND tm_valuta = " & nValuta.ToString & " "
        Else
          'normale
          strSQL = strSQL & " AND tm_scorpo = 'N' AND tm_valuta = 0 "
        End If
      End If
      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetContropAccontiPrimaRiga(ByVal strDitta As String, ByVal strTipoDoc As String, _
                         ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                         ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT top 1 movmag.*, tm_codagen, tm_codagen2 " & _
               " FROM " & strJoinTestmagMovmag & _
               " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
               " AND mm_anno = " & nAnno & _
               " AND mm_serie = " & CStrSQL(strSerie) & _
               " AND mm_numdoc = " & lNumdoc.ToString & _
               " ORDER BY mm_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetWhereHltmFatturaFigurativa(ByVal strDitta As String, ByVal lConto As Integer, ByVal lAnno As Integer) As String
    Try
      Return GetWhereHltmFatturaFigurativa(strDitta, lConto, lAnno)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetWhereHltmFatturaFigurativa(ByVal strDitta As String, ByVal lConto As Integer, ByVal lAnno As Integer, _
                                                            ByVal strScorporo As String) As String
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lConto, lAnno, strScorporo})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '----------------


      strSQL = " tm_tipork = 'C' " & _
               " AND tm_conto = " & lConto & _
               " AND tm_anno = " & lAnno & _
               " AND tm_valuta = 0 " & _
               " AND tm_codagen = 0 " & _
               " AND tm_codagen2 = 0 " & _
               " AND tm_flfatt = 'N' " & _
               " AND tm_scorpo = " & CStrSQL(strScorporo) & _
               " AND tm_totdoc - tm_abbuono - tm_totomag + tm_resto = tm_pagato + tm_pagato2 " 'Solo i documenti totalmente incassati 
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return strSQL
  End Function



  Public Overridable Function GetMovordValoremmQuant(ByVal strDitta As String, ByVal strTipork As String, _
                                                     ByVal nAnno As Integer, ByVal strSerie As String, _
                                                     ByVal lnumdoc As Integer, ByVal nRiga As Integer, _
                                                     ByRef dQtaOrig As Decimal, ByRef dColliOrig As Decimal, _
                                                     ByRef strUnmis As String, ByRef dValoreOrig As Decimal) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strUnmis = ""
      dColliOrig = 0
      dQtaOrig = 0
      dValoreOrig = 0

      strSQL = "SELECT mo_colli, mo_unmis, mo_quant, mo_valoremm " & _
      "FROM movord " & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND mo_tipork = " & CStrSQL(strTipork) & _
                  " AND mo_anno = " & nAnno & _
                  " AND mo_serie = " & CStrSQL(strSerie) & _
                  " AND mo_numord = " & lnumdoc & _
                  " AND mo_riga = " & nRiga.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If Not dttTmp.Rows.Count = 0 Then
        dColliOrig = NTSCDec(dttTmp.Rows(0)!mo_colli)
        dQtaOrig = NTSCDec(dttTmp.Rows(0)!mo_quant)
        dValoreOrig = NTSCDec(dttTmp.Rows(0)!mo_valoremm)
        strUnmis = NTSCStr(dttTmp.Rows(0)!mo_unmis)
      End If

      If strTipork = "H" Then
        'per i carichi da prod devo togliere dal valore totale il costo dei materiali, visto che in mo_valore c'è solo il costo della lavorazione
        strSQL = "SELECT sum(mo_valoremm) as valorescarichi " & _
                  "FROM movord " & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND mo_tiporkor = " & CStrSQL(strTipork) & _
                  " AND mo_annoor = " & nAnno & _
                  " AND mo_serieor = " & CStrSQL(strSerie) & _
                  " AND mo_numordor = " & lnumdoc & _
                  " AND mo_rigaor = " & nRiga.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        dValoreOrig = dValoreOrig - NTSCDec(dttTmp.Rows(0)!valorescarichi)
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


  Public Overridable Function GetFirstTabcove(ByVal strDitta As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try

      strSQL = "SELECT TOP 1 * FROM tabcove WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      GetFirstTabcove = NTSCInt(dttTmp.Rows(0)!tb_codcove)
      dttTmp.Clear()

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetNnchiam(ByVal strDitta As String, ByVal nCodchia As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM nnchiam" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND op_codchia = " & nCodchia
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetPrezzoNetto(ByVal strDitta As String, ByVal strTipoDoc As String, _
                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                           ByVal lRiga As Integer) As Decimal

    'restituisce true se il documento esiste, diversamente false
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dOut As Decimal = 0

    Try

      strSQL = "SELECT mm_quant, mm_valore, mm_perqta FROM " & IIf(strTipoDoc = "W", "movprb", "movmag").ToString & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
               " AND mm_anno = " & nAnno & _
               " AND mm_serie = " & CStrSQL(strSerie) & _
               " AND mm_numdoc = " & lNumdoc.ToString & _
               " AND mm_riga = " & lRiga.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        If NTSCDec(dttTmp.Rows(0)!mm_quant) <> 0 Then
          dOut = ArrDbl((NTSCDec(dttTmp.Rows(0)!mm_valore) / NTSCDec(dttTmp.Rows(0)!mm_quant)) * NTSCDec(dttTmp.Rows(0)!mm_perqta), TrovaNdecSuPrzUn(0))
        End If
      End If
      dttTmp.Clear()

      Return dOut
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function ScaricaMagazGetEsist(ByVal strDitta As String, ByVal nMagazScar As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strSelectTaglie As String = ""
    Dim strSelectSomma As String = ""
    Dim strSelectWhere As String = ""
    Dim strSelectNoTaglie As String = ""
    Try
      For i As Integer = 1 To 24
        Dim strTaglia As String = i.ToString("00")

        strSelectWhere &= "apt_esist" & strTaglia & " <> 0 OR "
        strSelectSomma &= "apt_esist" & strTaglia & " + "
        strSelectTaglie &= "apt_esist" & strTaglia & " , "
        strSelectNoTaglie &= "0 AS lp_esist" & strTaglia & " , "
      Next
      strSelectWhere = "(" & strSelectWhere.Remove(strSelectWhere.Length - 3) & ")"
      strSelectSomma = strSelectSomma.Remove(strSelectSomma.Length - 3)
      strSelectTaglie = strSelectTaglie.Remove(strSelectTaglie.Length - 3)
      strSelectNoTaglie = strSelectNoTaglie.Remove(strSelectNoTaglie.Length - 3)

      'Espone sempre la lista delle taglie, poi in base a se è o meno un articolo gestito a taglie il programma decide come gestirlo.
      strSQL = "SELECT lp_codart, lp_magaz, lp_commeca, lp_lotto, lp_fase, lp_ubicaz, lp_esist, ar_codtagl, " & strSelectNoTaglie & _
               " FROM lotcpro " & _
               "  INNER JOIN artico ON lotcpro.codditt = artico.codditt AND lotcpro.lp_codart = artico.ar_codart" & _
               " WHERE lotcpro.codditt = " & CStrSQL(strDitta) & _
               "   AND ar_codtagl = 0 " & _
               "   AND lp_esist <> 0 " & _
               "   AND lp_magaz = " & nMagazScar.ToString & _
               " UNION ALL " & _
               "SELECT apt_codart, apt_magaz, apt_commeca, apt_lotto, apt_fase, apt_ubicaz, " & strSelectSomma & " AS apt_esist, ar_codtagl, " & strSelectTaglie & _
               " FROM lotcprotc " & _
               "  INNER JOIN artico ON lotcprotc.codditt = artico.codditt AND lotcprotc.apt_codart = artico.ar_codart" & _
               " WHERE lotcprotc.codditt = " & CStrSQL(strDitta) & _
               "   AND ar_codtagl <> 0" & _
               "   AND " & strSelectWhere & _
               "   AND apt_magaz = " & nMagazScar.ToString & _
               " ORDER BY lp_codart, lp_fase, lp_lotto, lp_commeca, lp_ubicaz "

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetDettaglioEvasioneNota(ByVal strDitta As String, ByVal strTipork As String, _
                                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                                    ByVal lNumdoc As Integer, ByVal lRiga As Integer, _
                                                    ByRef dttOut As DataTable) As Boolean
    '----------------------------------
    'restituisce con quali documenti è stato evasa la nota di prelievo
    Dim strSQL As String = ""
    Try

      strSQL = "SELECT mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, mm_quant, mm_npqtadis " & _
               " FROM movmag " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mm_nptipo = " & CStrSQL(strTipork) & _
               " AND mm_npanno = " & nAnno & _
               " AND mm_npserie = " & CStrSQL(strSerie) & _
               " AND mm_npnum = " & lNumdoc.ToString & _
               " AND mm_npriga = " & lRiga.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


#Region "Picking/Ricevimento"
  Public Overridable Function CancellaTabellePickingRicevimento(ByVal strDitta As String, ByVal lIdPIck As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      ' cancella il documento di picking
      strSQL = "DELETE FROM movpic WHERE codditt = " & CStrSQL(strDitta) & " AND mp_idpick = " & lIdPIck

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM testpic WHERE codditt = " & CStrSQL(strDitta) & " AND tp_idpick = " & lIdPIck

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM tabpick WHERE codditt = " & CStrSQL(strDitta) & " AND tb_codpick = " & lIdPIck

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function PickingConPiuNote(ByVal strDitta As String, ByVal lIdPick As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT testpic.* FROM testpic WHERE codditt = " & CStrSQL(strDitta) & " AND tp_idpick = " & lIdPick

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function StatoPickingRicevimento(ByVal strDitta As String, ByVal lIdPick As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_chiuso FROM tabpick WHERE codditt = " & CStrSQL(strDitta) & " AND tb_codpick = " & lIdPick

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckNotaPrelVistatoDB(ByVal strDitta As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNum As Integer, _
                                                     ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_chiuso FROM tabpick " & _
                " INNER JOIN testprb ON tm_idpick = tb_codpick AND testprb.codditt = tabpick.codditt " & _
                " WHERE testprb.codditt = " & CStrSQL(strDitta) & _
                " AND tm_tipork = 'W' " & _
                " AND tm_anno = " & lAnno & _
                " AND tm_serie = " & CStrSQL(strSerie) & _
                " AND tm_numdoc = " & lNum

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RicevimentoEtichetteGenerate(ByVal strDitta As String, ByVal dttIn As DataTable, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mpallet.* FROM mpallet " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mp_mxtipork = " & CStrSQL(dttIn.Rows(0)!et_tipork) & _
               " AND mp_mxanno = " & NTSCInt(dttIn.Rows(0)!et_anno) & _
               " AND mp_mxserie = " & CStrSQL(dttIn.Rows(0)!et_serie) & _
               " AND mp_mxnumdoc = " & NTSCInt(dttIn.Rows(0)!et_numdoc)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaEtichetteUdC(ByVal dttEt As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE tpallet FROM tpallet " & _
                          " INNER JOIN mpallet ON tp_idpall = mp_idpall AND mpallet.codditt = tpallet.codditt" & _
                          " WHERE mpallet.codditt = " & CStrSQL(dttEt.Rows(0)!codditt) & _
                          " AND mp_mxtipork = " & CStrSQL(dttEt.Rows(0)!et_tipork) & _
                          " AND mp_mxanno = " & NTSCInt(dttEt.Rows(0)!et_anno) & _
                          " AND mp_mxserie = " & CStrSQL(dttEt.Rows(0)!et_serie) & _
                          " AND mp_mxnumdoc = " & NTSCInt(dttEt.Rows(0)!et_numdoc)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM mpallet " & _
               " WHERE codditt = " & CStrSQL(dttEt.Rows(0)!codditt) & _
               " AND mp_mxtipork = " & CStrSQL(dttEt.Rows(0)!et_tipork) & _
               " AND mp_mxanno = " & NTSCInt(dttEt.Rows(0)!et_anno) & _
               " AND mp_mxserie = " & CStrSQL(dttEt.Rows(0)!et_serie) & _
               " AND mp_mxnumdoc = " & NTSCInt(dttEt.Rows(0)!et_numdoc)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

  Public Overridable Function TestCa2InCG(ByVal strDitta As String, ByVal strTipork As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumdoc As Integer) As Boolean
    '----------------------------
    'restituisce true se la le righe di priana2 collegate al doc di magaz sono collegate anche a righe di CG
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 pa2_progr FROM priana2 WHERE " & _
               " codditt = " & CStrSQL(strDitta) & _
               " AND pa2_numreg <> 0 " & _
               " AND pa2_mmtipork = " & CStrSQL(strTipork) & _
               " AND pa2_mmanno = " & nAnno.ToString & _
               " AND pa2_mmserie = " & CStrSQL(strSerie) & _
               " AND pa2_mmnumdoc = " & lNumdoc.ToString

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

  Public Overridable Function GetTipcenaCa2(ByVal strDitta As String, ByVal nCodcena As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      GetTipcenaCa2 = "*"
      strSQL = "SELECT tb_ttipcena FROM tabcena INNER JOIN tabtcdc ON tabcena.tb_codtcdc = tabtcdc.tb_codtcdc " & _
               " WHERE tabcena.codditt = " & CStrSQL(strDitta) & " AND tb_codcena = " & nCodcena
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCStr(dttTmp.Rows(0)!tb_ttipcena).ToUpper

      Return "*"

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CheckRkinTabtcon(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT TOP 1 * FROM tabtcon WHERE codditt = " & CStrSQL(strDitta) & " ORDER BY tb_codtcon"
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

#Region "DocStornaImportExport"
  Public Overridable Function DataInizioEsercizio(ByVal strDitta As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_dtineser" & _
                " FROM tabesco INNER JOIN tabanaz ON tabesco.codditt = tabanaz.codditt AND tabesco.tb_codesco = tabanaz.tb_escomp" & _
                " WHERE tabanaz.codditt = " & CStrSQL(strDitta)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckPM(ByVal strDitta As String, ByVal strtipoRk As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                      ByVal lNumDoc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Count(*) As Records FROM " & NTSCStr(IIf(strtipoRk = "W", "movprb", "movmag")) & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(strtipoRk) & _
               " AND mm_anno = " & nAnno & _
               " AND mm_serie = " & CStrSQL(strSerie) & _
               " AND mm_numdoc = " & lNumDoc & _
               " AND mm_pmtaskid <> 0"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DocStornaImportExport(ByVal strDitta As String, ByVal bModTco As Boolean, ByVal strInizioEsercizio As String, ByVal strTipoRk As String, _
                                                    ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumDoc As Integer, ByRef strMsg As String) As Boolean
    Dim dbConn As DbConnection = Nothing
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- Apre la transazione
      '-----------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '-----------------------------------------------------------------------------------------
      '--- Ora storna KEYMAG, MOVORD, ARTPRO
      '-----------------------------------------------------------------------------------------
      'SQL Server
      If bModTco Then
        'Se c'è il modulo TC prima storno progressivi e ordini (se preso in modific)
        'ARTPROTC
        If Not AggArtproTC(strDitta, strTipoRk, nAnno, strSerie, lNumDoc, 1, dbConn) Then
          strMsg = oApp.Tr(Me, 129381109519036898, "Si sono verificati degli errori nello storno progressivi TC (AggArtProTC)." & vbCrLf & _
                                                   "    Elaborazione non effettuata.")
          Return False
        End If
        'MOVORDTC
        If Not AggMovOrdTC(strDitta, strTipoRk, nAnno, strSerie, lNumDoc, 1, dbConn) Then
          strMsg = oApp.Tr(Me, 129381110124017538, "Si sono verificati degli errori nello storno ordini TC (AggMovOrdTC)." & vbCrLf & _
                                                   "    Elaborazione non effettuata.")
          Return False
        End If
        If strTipoRk = "T" Then
          'ARTPROTC U
          If Not AggArtproTC(strDitta, "U", nAnno, strSerie, lNumDoc, 1, dbConn) Then
            strMsg = oApp.Tr(Me, 129381110452132038, "Si sono verificati degli errori nello storno progressivi TC U (AggArtProTC)." & vbCrLf & _
                                                     "    Elaborazione non effettuata.")
            Return False
          End If
          'MOVORDTC U
          If Not AggMovOrdTC(strDitta, "U", nAnno, strSerie, lNumDoc, 1, dbConn) Then
            strMsg = oApp.Tr(Me, 129381111076174568, "Si sono verificati degli errori nello storno ordini TC U (AggMovOrdTC)." & vbCrLf & _
                                                     "    Elaborazione non effettuata.")
            Return False
          End If
        End If 'strTipork=U
      End If 'bModTco

      '--------------------------------------------------------------------------------
      'Poi lancia la sp per stornare gli ordini
      strSQL = "bussp_aggmovord9 " & CStrSQL(strTipoRk) & ", " & nAnno & ", " & CStrSQL(strSerie) & ", " & lNumDoc & ", 1, " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '--------------------------------------------------------------------------------
      'Poi lancia la sp per stornare i progressivi
      strSQL = "bussp_aggartpro9 " & CStrSQL(strTipoRk) & ", " & nAnno & ", " & CStrSQL(strSerie) & ", " & lNumDoc & ", 1, " & _
               CDataSQL(strInizioEsercizio) & ", " & oApp.NDecPrzUn & ", " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If strTipoRk = "T" Then
        'per i carichi di produzione (previsto anche se sopra è inibito
        '--------------------------------------------------------------------------------
        'Poi lancia la sp per stornare gli ordini
        strSQL = "bussp_aggmovord9 'U', " & nAnno & ", " & CStrSQL(strSerie) & ", " & lNumDoc & ", 1, " & CStrSQL(strDitta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '--------------------------------------------------------------------------------
        'Poi lancia la sp per stornare i progressivi
        strSQL = "bussp_aggartpro9 'U', " & nAnno & ", " & CStrSQL(strSerie) & ", " & lNumDoc & ", 1, " & _
                 CDataSQL(strInizioEsercizio) & ", " & oApp.NDecPrzUn & ", " & CStrSQL(strDitta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If 'strtipork="U"

      '-----------------------------------------------------------------------------------------
      '--- Chiude la transazione
      '-----------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
    End Try
  End Function

#End Region

  Public Overridable Function IsClientePrivato(ByVal strDitta As String, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      'cerca nella personalizzazione retail se è un cliente privato o meno.
      strSQL = "SELECT * FROM repers " & _
               " WHERE re_tipo = 'RetailClieGenerico'" & _
               " AND re_valnum = " & lConto & _
               " AND codditt = " & CStrSQL(strDitta)

      'Se non lo trova, non è un cliente privato e ritorna false
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count = 0 Then Return False

      'C'è, è un cliente privato
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CreaSaldiCtoVisione(ByVal strDitta As String, ByVal strTipork As String, _
                                                  ByVal nAnno As Integer, ByVal strSerie As String, _
                                                  ByVal lNumdoc As Integer, ByVal nMagaz As Integer, _
                                                  ByVal lConto As Integer, ByVal lCoddest As Integer, _
                                                  ByVal strCodart As String, ByVal nFase As Integer, _
                                                  ByRef dsOut As DataSet) As Boolean
    'ottiene i saldi di c/to visione sul magazzino indicato per il cliente / destinazione diversa, 
    'escludendo i movimenti contenuti nel documento corrente
    'tb_tipcaum = 'V' = CARICO, tb_tipcaum = 'U' = SCARICO
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Try
      strSQL = "SELECT 'N' as xx_selez, mm_codart, mm_fase, min(mm_descr) as mm_descr, min(af_descr) as mm_desfase, min(mm_ump) as mm_ump, " & _
               " sum(mm_quant * km_carscar) as mm_qtares, 0.0 as mm_quadaeva, " & _
               " max(alo_lottox) as mm_lotto, min(mm_commeca) as mm_commeca, min(mm_subcommeca) as mm_subcommeca, " & _
               " ar_codtagl as xx_codtagl, mm_cvtipo, mm_cvanno, mm_cvserie, mm_cvnum, mm_cvriga," & _
               " min(mm_desint) as mm_desint, min(mm_note) as mm_note, "
      For i = 1 To 24
        strSQL += "sum(mm_quant" & i.ToString("00") & " * km_carscar) as mm_qtares_" & i.ToString("00") & ", 0.0 as mm_quadaeva_" & i.ToString("00") & ", "
      Next
      strSQL += nMagaz.ToString & " as km_magaz, " & lConto.ToString & " as tm_conto, " & lCoddest.ToString & " as tm_coddest " & _
               " FROM " & strJoinTestmagMovmag & " INNER JOIN keymag ON movmag.codditt = keymag.codditt " & _
               " AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno " & _
               " AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc " & _
               " AND movmag.mm_riga = keymag.km_riga " & _
               " LEFT JOIN movmagtc ON movmag.codditt = movmagtc.codditt " & _
               " AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno " & _
               " AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc " & _
               " AND movmag.mm_riga = movmagtc.mm_riga " & _
               " INNER JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum " & _
               " INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart " & _
               " LEFT JOIN artfasi ON movmag.codditt = artfasi.codditt AND movmag.mm_codart = artfasi.af_codart AND movmag.mm_fase = artfasi.af_fase " & _
               " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto " & _
               " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
               " AND (tb_tipcaum = 'V' OR tb_tipcaum = 'U')" & _
               " AND tm_conto = " & lConto.ToString & _
               " AND tm_coddest = " & lCoddest.ToString & _
               " AND km_magaz = " & nMagaz.ToString & _
               " AND km_codart <> 'D' AND km_codart <> 'M'"
      If strCodart <> "" Then strSQL += " AND km_codart = " & CStrSQL(strCodart) & _
                                        " AND km_fase = " & nFase.ToString
      strSQL += " AND NOT(km_tipork = " & CStrSQL(strTipork) & _
               "         AND km_anno = " & nAnno.ToString & _
               "         AND km_serie = " & CStrSQL(strSerie) & _
               "         AND km_numdoc = " & lNumdoc.ToString & ")" & _
               " GROUP BY mm_codart, mm_fase, ar_codtagl, mm_cvtipo, mm_cvanno, mm_cvserie, mm_cvnum, mm_cvriga " & _
               " HAVING sum(mm_quant * km_carscar) > 0"
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SALDI")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetRigaOrdineDaEvadereLikeHLMO(ByVal strDitta As String, ByVal strRipork As String, _
                                                     ByVal nAnno As Integer, ByVal strSerie As String, _
                                                     ByVal lNumord As Integer, ByVal lRiga As Integer, _
                                                     ByRef dttOut As DataTable) As Boolean
    'la query è uguale a quella di BNORHLMO per selezionare le righe d'ordine
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Try
      strWhere = "movord.mo_tipork = " & CStrSQL(strRipork) & _
                 " AND movord.mo_anno = " & nAnno & _
                 " AND movord.mo_serie = " & CStrSQL(strSerie) & _
                 " AND movord.mo_numord = " & lNumord & _
                 " AND movord.mo_riga = " & lRiga
      strSQL = "SELECT movord.*, td_conto as xx_conto, td_datord as xx_datord, td_annpar as xx_annpar, " & _
               "       td_alfpar as xx_alfpar, td_numpar as xx_numpar, td_riferim as xx_riferim, td_codagen as xx_agente, " & _
               "       ar_codappr as xx_codappr, an_descr1 as xx_desconto, tb_descage as xx_desagente, tb_desappr as xx_desappr, " & _
               "       tb_desmaga as xx_magaz, co_descr1 as xx_commeca, " & _
               "       mo_prezzo as xx_prezzonetto, '' as xx_articopadre, '' as xx_desarticopadre, " & _
               "       (mo_quant - mo_quaeva) as xx_quadaeva, " & _
               "       (mo_quant - mo_quaeva) as xx_qtares, " & _
               "       tb_desmaga as xx_desmaga, " & _
               "       'C' as xx_flevasdaeva, 'S' as xx_seleziona, ar_codtagl as xx_codtagl "

      For i As Integer = 1 To 24
        Dim strSuffix As String = i.tostring("00")
        strSQL &= ", (mo_quant" & strSuffix & " - mo_quaeva" & strSuffix & ") as xx_quadaeva" & strSuffix
      Next

      strSQL &= " FROM movord " & _
                "  INNER JOIN testord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork " & _
                "                    AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord" & _
                "  INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto" & _
                "  INNER JOIN artico ON artico.codditt = movord.codditt AND artico.ar_codart = movord.mo_codart" & _
                "   LEFT JOIN commess ON movord.codditt = commess.codditt AND movord.mo_commeca = commess.co_comme" & _
                "   LEFT JOIN tabappr ON artico.codditt = tabappr.codditt AND artico.ar_codappr = tabappr.tb_codappr" & _
                "   LEFT JOIN tabcage ON testord.codditt = tabcage.codditt AND testord.td_codagen = tabcage.tb_codcage" & _
                "   LEFT JOIN tabmaga ON movord.codditt = tabmaga.codditt AND movord.mo_magaz = tabmaga.tb_codmaga" & _
                "   LEFT JOIN movordtc ON movordtc.codditt = movord.codditt AND movordtc.mo_tipork = movord.mo_tipork AND movordtc.mo_anno = movord.mo_anno " & _
                "                     AND movordtc.mo_serie = movord.mo_serie AND movordtc.mo_numord	= movord.mo_numord AND movordtc.mo_riga = movord.mo_riga " & _
                " WHERE " & strWhere & _
                "   AND testord.codditt = " & CStrSQL(strDitta) & _
                " ORDER BY movord.mo_datcons, movord.mo_codart, movord.mo_fase, movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EsisteCollaudo(ByVal strDitta As String, ByVal strTipork As String, _
    ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lRiga As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 cm_annocoll FROM keycoll" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND cm_tipork = " & CStrSQL(strTipork) & _
        " AND cm_anno = " & nAnno & _
        " AND cm_serie = " & CStrSQL(strSerie) & _
        " AND cm_numdoc = " & lNumdoc & _
        " AND cm_riga = " & lRiga
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function CheckInAvlavp(ByVal strDitta As String, ByVal strTipo As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                            ByVal lNum As Integer, ByVal lRiga As Integer, ByVal lRigaa As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM avlavp " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND lce_tipork = " & CStrSQL(strTipo) & _
               " AND lce_anno = " & lAnno & _
               " AND lce_serie = " & CStrSQL(strSerie) & _
               " AND lce_numdoc = " & lNum
      If lRiga <> 0 Then strSQL += " AND lce_riga = " & lRiga & " AND lce_rigaa = " & lRigaa

      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return True

      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetMovmagPerAutoCarprod(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                                       ByVal nAnno As Integer, ByVal strSerie As String, _
                                                       ByVal lNumdoc As Integer, ByVal bTestEsist As Boolean, _
                                                       ByRef dttMMOut As DataTable, ByRef dttMatrOut As DataTable) As Boolean

    Try
      Return GetMovmagPerAutoCarprod(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc, bTestEsist, dttMMOut, dttMatrOut, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetMovmagPerAutoCarprod(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                                  ByVal nAnno As Integer, ByVal strSerie As String, _
                                                  ByVal lNumdoc As Integer, ByVal bTestEsist As Boolean, _
                                                  ByRef dttMMOut As DataTable, ByRef dttMatrOut As DataTable, _
                                                  ByRef dttTCOut As DataTable) As Boolean
    'è stata settata l'opzione di registro per creare un carico da prod al salva del doc evemsso, 
    'contenente gli articoli appena venduti (solo quelli che hanno artico_ar_coddb <> '' e ar_makeorbuy <> 'B')
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipoDoc, nAnno, strSerie, lNumdoc, bTestEsist, dttMMOut, dttMatrOut, dttTCOut})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttMMOut = CType(oIn(6), DataTable)
        dttMatrOut = CType(oIn(7), DataTable)
        dttTCOut = CType(oIn(8), DataTable)
        Return CBool(oOut)
      End If
      '----------------
      strSQL = "SELECT movmag.* " & _
               "FROM movmag INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart" & _
               " INNER JOIN artpro ON movmag.codditt = artpro.codditt AND movmag.mm_codart = artpro.ap_codart " & _
               " AND movmag.mm_magaz = artpro.ap_magaz AND movmag.mm_fase = artpro.ap_fase " & _
               " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
               " AND mm_anno = " & nAnno & _
               " AND mm_serie = " & CStrSQL(strSerie) & _
               " AND mm_numdoc = " & lNumdoc.ToString & _
               " AND ar_coddb <> ''" & _
               " AND ar_makebuy <> 'B'"
      If bTestEsist Then strSQL += " AND ap_esist < 0"
      dttMMOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If dttMMOut.Rows.Count = 0 Then Return True

      strSQL = "SELECT movmatr.* " & _
               "FROM movmag INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart" & _
               " INNER JOIN artpro ON movmag.codditt = artpro.codditt AND movmag.mm_codart = artpro.ap_codart " & _
               " AND movmag.mm_magaz = artpro.ap_magaz AND movmag.mm_fase = artpro.ap_fase " & _
               " INNER JOIN movmatr ON movmag.codditt = movmatr.codditt AND movmag.mm_tipork = movmatr.mma_tipork " & _
               " AND movmag.mm_anno = movmatr.mma_anno AND movmag.mm_serie = movmatr.mma_serie " & _
               " AND movmag.mm_numdoc = movmatr.mma_numdoc AND movmag.mm_riga = movmatr.mma_riga " & _
               " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
               " AND mm_anno = " & nAnno & _
               " AND mm_serie = " & CStrSQL(strSerie) & _
               " AND mm_numdoc = " & lNumdoc.ToString & _
               " AND ar_coddb <> ''" & _
               " AND ar_makebuy <> 'B'"
      If bTestEsist Then strSQL += " AND ap_esist < 0"
      dttMatrOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "SELECT movmagtc.* " & _
             " FROM movmag " & _
             "  INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart" & _
             "  INNER JOIN artpro ON movmag.codditt = artpro.codditt AND movmag.mm_codart = artpro.ap_codart " & _
             "                   AND movmag.mm_magaz = artpro.ap_magaz AND movmag.mm_fase = artpro.ap_fase " & _
             "  INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork " & _
             "                   AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie " & _
             "                   AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga " & _
             " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
             "   AND movmag.mm_tipork = " & CStrSQL(strTipoDoc) & _
             "   AND movmag.mm_anno = " & nAnno & _
             "   AND movmag.mm_serie = " & CStrSQL(strSerie) & _
             "   AND movmag.mm_numdoc = " & lNumdoc.ToString & _
             "   AND ar_coddb <> ''" & _
             "   AND ar_makebuy <> 'B'"
      If bTestEsist Then strSQL += " AND ap_esist < 0"
      dttTCOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTipoBfProdInterna(ByVal strDitta As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    GetTipoBfProdInterna = 0

    Try
      strSQL = "SELECT tb_codtpbf FROM tabtpbf INNER JOIN tabmaga ON " & _
               " tabtpbf.codditt = tabmaga.codditt AND tabtpbf.tb_tmagazz = tabmaga.tb_codmaga" & _
               " WHERE tabtpbf.codditt = " & CStrSQL(strDitta) & _
               " AND tabmaga.tb_flclavo = ' ' " & _
               " AND tabtpbf.tb_tmagimp <> 0" & _
               " ORDER BY tb_codtpbf ASC"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!tb_codtpbf)

      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


  Public Overridable Function VerificaMovimentoGiftCard(ByVal strDitta As String, ByVal strTipo As String, ByVal lAnno As Integer, _
                                                        ByVal strSerie As String, ByVal lNum As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT codditt FROM movgift " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mg_tipork = " & CStrSQL(strTipo) & _
               " AND mg_anno = " & NTSCInt(lAnno) & _
               " AND mg_serie = " & CStrSQL(strSerie) & _
               " AND mg_numdoc = " & lNum

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count = 0
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function VerificaMovimentoPunti(ByVal strDitta As String, ByVal strTipo As String, ByVal lAnno As Integer, _
                                                        ByVal strSerie As String, ByVal lNum As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT codditt FROM movpunti " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mp_tipork = " & CStrSQL(strTipo) & _
               " AND mp_anno = " & NTSCInt(lAnno) & _
               " AND mp_serie = " & CStrSQL(strSerie) & _
               " AND mp_numdoc = " & lNum

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count = 0
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetNoteDiPrelievoDaOrdine(ByVal strDitta As String, ByVal strOrTipo As String, ByVal lOrAnno As Integer, _
                                                          ByVal strOrSerie As String, ByVal lOrNum As Integer, ByVal lOrRiga As Integer, _
                                                          ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movprb.*, CASE WHEN movmag.codditt IS null THEN 'N' ELSE 'S' END AS xx_evaso FROM movprb " & _
               " LEFT JOIN movmag ON movmag.codditt = movprb.codditt AND movprb.mm_tipork = movmag.mm_nptipo AND movprb.mm_anno = movmag.mm_npanno " & _
               " AND movprb.mm_serie = movmag.mm_npserie AND movprb.mm_numdoc = movmag.mm_npnum AND movprb.mm_riga = movmag.mm_npriga " & _
               " WHERE movprb.codditt = " & CStrSQL(strDitta) & _
               " AND movprb.mm_ortipo = " & CStrSQL(strOrTipo) & _
               " AND movprb.mm_oranno = " & lOrAnno & _
               " AND movprb.mm_orserie = " & CStrSQL(strOrSerie) & _
               " AND movprb.mm_ornum = " & lOrNum & _
               " AND movprb.mm_orriga = " & lOrRiga

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckDatiIntra(ByVal strDitta As String, ByVal strTipo As String, _
                                             ByVal nAnno As Integer, ByVal strSerie As String, _
                                             ByVal lNumdoc As Integer) As Boolean
    'restituisco true se il documento è presente tra i dati intrastat
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 codditt " & _
               " FROM intrac " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ik_tmtipork = " & CStrSQL(strTipo) & _
               " AND ik_tmanno = " & nAnno & _
               " AND ik_tmserie = " & CStrSQL(strSerie) & _
               " AND ik_tmnumdoc = " & lNumdoc
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

  Public Overridable Function GetTabduri(ByVal strDitta As String, ByVal strDatreg As String, _
                                         ByVal lNumreg As Integer, ByRef dttOut As DataTable) As Boolean
    'ritorno dei record se la registraz. iva è stata stampata sui registri IVA
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DISTINCT tb_utipreg, tb_udatreg, tb_unureg " & _
               " FROM tabduri INNER JOIN moviva ON tabduri.codditt = moviva.codditt" & _
               " AND tabduri.tb_utipreg = moviva.mi_tregiva AND tabduri.tb_unumreg = moviva.mi_nregiva" & _
               " AND tabduri.tb_anno = year(moviva.mi_datreg)" & _
               " WHERE moviva.codditt = " & CStrSQL(strDitta) & _
               " AND moviva.mi_datreg = " & CDataSQL(strDatreg) & _
               " AND moviva.mi_numreg = " & lNumreg.ToString & _
               " AND moviva.mi_nregiva <> 0"
      strSQL += " AND (tb_udatreg > " & CDataSQL(strDatreg) & " OR (tb_udatreg = " & CDataSQL(strDatreg) & " AND tb_unureg >= " & lNumreg.ToString & "))"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetScadEffetti(ByVal strDitta As String, ByVal lContoEff As Integer, _
                                             ByVal nAnnpar As Integer, ByVal strAlfpar As String, _
                                             ByVal lNumpar As Integer, ByVal nRata As Integer, _
                                             ByRef dttOut As DataTable) As Boolean
    'dato il cliente e gli estremi partita, ritorno le scadenze di emissione effetti
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT sc_datreg, sc_numreg, sc_flsaldato " & _
               "FROM scaden WHERE codditt = " & CStrSQL(strDitta) & _
               " AND sc_conto = " & lContoEff.ToString & _
               " AND sc_annpar = " & nAnnpar.ToString & _
               " AND sc_alfpar = " & CStrSQL(strAlfpar) & _
               " AND sc_numpar = " & lNumpar.ToString & _
               " AND sc_numrata = " & nRata.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestDichint(ByVal strDitta As String, ByVal strTipo As String, ByVal nAnno As Integer, ByVal lNumdoc As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = " SELECT * FROM dichint" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND di_tipo = " & CStrSQL(strTipo) & _
               " AND di_anno = " & nAnno & _
               " AND di_numero = " & lNumdoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function


  Public Overridable Function RitornaCodicePagamentoPerScaglioni(ByVal strDitta As String, ByVal strTipork As String, ByVal nCodtpbf As Integer, _
                                                                 ByVal lConto As Integer, ByVal lImportoIn As Decimal, ByRef lImportoOut As Decimal, _
                                                                 ByVal nCodpagaIn As Integer, ByRef nCodpagaOut As Integer, ByRef strDespagaOut As String, _
                                                                 ByRef bInferioreAImportoMinimo As Boolean) As Boolean
    Try
      'obsoleta
      Return RitornaCodicePagamentoPerScaglioni(strDitta, strTipork, nCodtpbf, lConto, lImportoIn, lImportoOut, _
                                                nCodpagaIn, nCodpagaOut, strDespagaOut, bInferioreAImportoMinimo, 0, False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function RitornaCodicePagamentoPerScaglioni(ByVal strDitta As String, ByVal strTipork As String, ByVal nCodtpbf As Integer, _
                                                                 ByVal lConto As Integer, ByVal lImportoIn As Decimal, ByRef lImportoOut As Decimal, _
                                                                 ByVal nCodpagaIn As Integer, ByRef nCodpagaOut As Integer, ByRef strDespagaOut As String, _
                                                                 ByRef bInferioreAImportoMinimo As Boolean, ByRef lImportoOut2 As Decimal, _
                                                                 ByRef bInferioreAImportoMassimo As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, nCodtpbf, lConto, lImportoIn, lImportoOut, nCodpagaIn, nCodpagaOut, strDespagaOut, bInferioreAImportoMinimo, lImportoOut2, bInferioreAImportoMassimo})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        lImportoOut = NTSCDec(oIn(5))
        nCodpagaOut = NTSCInt(oIn(7))
        strDespagaOut = NTSCStr(oIn(8))
        bInferioreAImportoMinimo = CBool(oIn(9))
        lImportoOut2 = NTSCDec(oIn(10))
        bInferioreAImportoMassimo = CBool(oIn(11))
        Return CBool(oOut)
      End If

      'obsoleta
      Return RitornaCodicePagamentoPerScaglioni(strDitta, strTipork, nCodtpbf, lConto, lImportoIn, lImportoOut, nCodpagaIn, nCodpagaOut, _
                                                strDespagaOut, bInferioreAImportoMinimo, lImportoOut2, bInferioreAImportoMassimo, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function RitornaCodicePagamentoPerScaglioni(ByVal strDitta As String, ByVal strTipork As String, ByVal nCodtpbf As Integer, _
                                                                 ByVal lConto As Integer, ByVal lImportoIn As Decimal, ByRef lImportoOut As Decimal, _
                                                                 ByVal nCodpagaIn As Integer, ByRef nCodpagaOut As Integer, ByRef strDespagaOut As String, _
                                                                 ByRef bInferioreAImportoMinimo As Boolean, ByRef lImportoOut2 As Decimal, _
                                                                 ByRef bInferioreAImportoMassimo As Boolean, ByVal bDocArtDeterior As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strCampiSelect As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, nCodtpbf, lConto, lImportoIn, lImportoOut, nCodpagaIn, nCodpagaOut, strDespagaOut, bInferioreAImportoMinimo, lImportoOut2, bInferioreAImportoMassimo, bDocArtDeterior})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        lImportoOut = NTSCDec(oIn(5))
        nCodpagaOut = NTSCInt(oIn(7))
        strDespagaOut = NTSCStr(oIn(8))
        bInferioreAImportoMinimo = CBool(oIn(9))
        lImportoOut2 = NTSCDec(oIn(10))
        bInferioreAImportoMassimo = CBool(oIn(11))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il documento tratta solo articoli deteriorabili il programma deve prendere le forme di pagamento specifiche Art62
      '--------------------------------------------------------------------------------------------------------------
      If bDocArtDeterior Then
        strCampiSelect = "an_codpagadet AS an_codpag, an_codpagadet2 AS an_codpaga2, an_codpagadet3 AS an_codpaga3,"
      Else
        strCampiSelect = "an_codpag, an_codpaga2, an_codpaga3,"
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Funziona solo su determinati tipi documento
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipork
        Case "B", "M", "I", "T", "U", "W", "Z" : Return False
        Case "F"
          strSQL = "SELECT tb_flresocl FROM tabtpbf" & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   "   AND tb_codtpbf = " & nCodtpbf & _
                   "   AND tb_flresocl = 'N'"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then Return False
      End Select
      '--------------------------------------------------------------------------------------------------------------
      '--- Per il conto di indicato in testata, guarda se la Rimanenza rientra negli scaglioni
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT " & strCampiSelect & " an_codpagscagl1, an_codpagscagl2," & _
               "       tabpaga_1.tb_despaga AS Despaga1, tabpaga_2.tb_despaga AS Despaga2, tabpaga_3.tb_despaga AS Despaga " & _
               " FROM anagra " & _
               "  LEFT JOIN tabpaga AS tabpaga_1 ON anagra.an_codpaga2 = tabpaga_1.tb_codpaga" & _
               "  LEFT JOIN tabpaga AS tabpaga_2 ON anagra.an_codpaga3 = tabpaga_2.tb_codpaga" & _
               "  LEFT JOIN tabpaga AS tabpaga_3 ON anagra.an_codpag = tabpaga_3.tb_codpaga" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND an_conto = " & lConto & _
               "   AND (an_codpagscagl1 <> 0 OR an_codpagscagl2 <> 0)"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      With dttTmp.Rows(0)
        If (NTSCInt(!an_codpaga2) <> 0) And (NTSCInt(!an_codpaga2) <> nCodpagaIn) And _
           (NTSCDec(!an_codpagscagl1) <> 0) And (lImportoIn <= NTSCDec(!an_codpagscagl1)) Then
          nCodpagaOut = NTSCInt(!an_codpaga2)
          bInferioreAImportoMinimo = True
          bInferioreAImportoMassimo = True
          lImportoOut = NTSCDec(!an_codpagscagl1)
          lImportoOut2 = 0
          strDespagaOut = NTSCStr(!Despaga1)
          Return True
        End If
        If (NTSCInt(!an_codpaga3) <> 0) And (NTSCInt(!an_codpaga3) <> nCodpagaIn) And _
           (NTSCDec(!an_codpagscagl2) <> 0) And (lImportoIn >= NTSCDec(!an_codpagscagl2)) Then
          nCodpagaOut = NTSCInt(!an_codpaga3)
          bInferioreAImportoMinimo = False
          bInferioreAImportoMassimo = False
          lImportoOut = NTSCDec(!an_codpagscagl2)
          lImportoOut2 = 0
          strDespagaOut = NTSCStr(!Despaga2)
          Return True
        End If
        If (NTSCInt(!an_codpag) <> 0) And (NTSCInt(!an_codpag) <> nCodpagaIn) And _
           (NTSCDec(!an_codpagscagl1) <> 0) And (lImportoIn >= NTSCDec(!an_codpagscagl1)) And _
           (NTSCDec(!an_codpagscagl2) <> 0) And (lImportoIn < NTSCDec(!an_codpagscagl2)) Then
          nCodpagaOut = NTSCInt(!an_codpag)
          bInferioreAImportoMinimo = False
          bInferioreAImportoMassimo = True
          lImportoOut = NTSCDec(!an_codpagscagl1)
          lImportoOut2 = NTSCDec(!an_codpagscagl2)
          strDespagaOut = NTSCStr(!Despaga)
          Return True
        End If
      End With
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function GetRigheMovmagMovord(ByVal strDitta As String, ByVal dtrT As DataRow, _
      ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT mm_riga, mm_codart, mm_descr," & _
        " ROUND(mm_prezzo*(100-mm_scont1)/100*(100-mm_scont2)/100*(100-mm_scont3)/100*(100-mm_scont4)/100*(100-mm_scont5)/100*(100-mm_scont6)/100*(100-mm_scontP)/100, " & TrovaNdecSuPrzUn(0) & ") AS PrezzoDoc," & _
        " mo_riga, mo_codart, mo_descr," & _
        " ROUND(mo_prezzo*(100-mo_scont1)/100*(100-mo_scont2)/100*(100-mo_scont3)/100*(100-mo_scont4)/100*(100-mo_scont5)/100*(100-mo_scont6)/100*(100-mo_scontP)/100, " & TrovaNdecSuPrzUn(0) & ") AS PrezzoOrd" & _
        " FROM movmag INNER JOIN movord ON movmag.codditt = movord.codditt" & _
        " AND movmag.mm_ortipo = movord.mo_tipork" & _
        " AND movmag.mm_oranno = movord.mo_anno" & _
        " AND movmag.mm_orserie = movord.mo_serie" & _
        " AND movmag.mm_ornum = movord.mo_numord" & _
        " AND movmag.mm_orriga = movord.mo_riga" & _
        " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
        " AND mm_tipork = " & CStrSQL(NTSCStr(dtrT!et_tipork)) & _
        " AND mm_anno = " & NTSCInt(dtrT!et_anno) & _
        " AND mm_serie = " & CStrSQL(NTSCStr(dtrT!et_serie)) & _
        " AND mm_numdoc = " & NTSCInt(dtrT!et_numdoc) & _
        " AND ROUND(mm_prezzo*(100-mm_scont1)/100*(100-mm_scont2)/100*(100-mm_scont3)/100*(100-mm_scont4)/100*(100-mm_scont5)/100*(100-mm_scont6)/100*(100-mm_scontP)/100, " & TrovaNdecSuPrzUn(0) & ")" & _
        " <>" & _
        " ROUND(mo_prezzo*(100-mo_scont1)/100*(100-mo_scont2)/100*(100-mo_scont3)/100*(100-mo_scont4)/100*(100-mo_scont5)/100*(100-mo_scont6)/100*(100-mo_scontP)/100, " & TrovaNdecSuPrzUn(0) & ")" & _
        " ORDER BY mm_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows.Count = 0 Then
        dttOut.Clear()
        dttOut.Dispose()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
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
        'Se non movimentata in CG la riapro..., altrimenti non la posso riaprire...
        strSQL = " SELECT TOP 1 * " & _
                 " FROM prinot INNER JOIN anagra ON prinot.codditt = anagra.codditt AND prinot.pn_conto = anagra.an_conto" & _
                 " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
                 " AND an_tipo = " & CStrSQL(strTipo) & _
                 " AND pn_dianno = " & nAnno & _
                 " AND pn_dinumero = " & nNumero
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count = 0 Then
          strSQL = " UPDATE dichint SET di_chiusa = 'N'" & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND di_tipo = " & CStrSQL(strTipo) & _
                   " AND di_anno = " & nAnno & _
                   " AND di_numero = " & nNumero
          lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If lResult = 0 Then Return False
        End If
      Else
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

  Public Overridable Function EvadiDocConFattFigurativa(ByVal ds As DataSet, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      'Scorre le righe e aggiorna i riferimenti di fatturazione dei corrispettivi, oltre alla data ultimo aggiornamento.
      For Each dtrEc As DataRow In ds.Tables("CORPO").Rows
        Dim strPart() As String = NTSCStr(dtrEc!xx_rifcorr).Split("|"c)
        If strPart.Length = 5 Then 'contiene i riferimenti di testata del corrispettivo più il codice iva utilizzato
          strSQL = "UPDATE testmag SET tm_ultagg = " & CDataOraSQL(Now) & ", " & _
                   "                   tm_flfatt = 'S', " & _
                   "                   tm_tiporkfat = " & CStrSQL(dtrEc!ec_tipork) & ", " & _
                   "                   tm_annfat = " & NTSCInt(dtrEc!ec_anno) & ", " & _
                   "                   tm_alffat = " & CStrSQL(dtrEc!ec_serie) & ", " & _
                   "                   tm_numfat = " & NTSCInt(dtrEc!ec_numdoc) & ", " & _
                   "                   tm_datfat = " & CDataSQL(NTSCDate(ds.Tables("TESTA").Rows(0)!et_datdoc)) & _
                   " WHERE codditt = " & CStrSQL(dtrEc!codditt) & _
                   "   AND tm_tipork = " & CStrSQL(strPart(0)) & _
                   "   AND tm_anno = " & strPart(1) & _
                   "   AND tm_serie = " & CStrSQL(strPart(2)) & _
                   "   AND tm_numdoc = " & strPart(3)

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SganciaDocConFattFigurativa(ByVal ds As DataSet, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      With ds.Tables("TESTA").Rows(0)
        'Toglie il flag fatturata  a tutte le bolle
        strSQL = "UPDATE testmag SET tm_flfatt = 'N', " & _
                 "                   tm_alffat = ' ', " & _
                 "                   tm_numfat = 0, " & _
                 "                   tm_datfat = Null, " & _
                 "                   tm_annfat = 0, " & _
                 "                   tm_tiporkfat = ' ', " & _
                 "                   tm_ultagg = " & CDataOraSQL(DateTime.Now) & " " & _
                 " WHERE codditt = " & CStrSQL(!codditt) & _
                 "   AND tm_flfatt = 'S' " & _
                 "   AND tm_tiporkfat = " & CStrSQL(!et_tipork) & _
                 "   AND tm_annfat = " & NTSCInt(!et_anno) & _
                 "   AND tm_alffat = " & CStrSQL(!et_serie) & _
                 "   AND tm_numfat = " & NTSCInt(!et_numdoc)

      End With

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function VerificaInEvasioneFattFigurativa(ByVal strDitta As String, ByVal strTipo As String, ByVal lAnno As Integer, _
                                                               ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 codditt FROM testmag " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND tm_tiporkfat = " & CStrSQL(strTipo) & _
               "   AND tm_annfat = " & lAnno & _
               "   AND tm_alffat = " & CStrSQL(strSerie) & _
               "   AND tm_numfat = " & lNumdoc

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function IsFatturaFigurativa(ByVal strDitta As String, ByVal strTipork As String, _
    ByVal nAnno As Integer, ByVal strSerie As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tb_numcodl, tb_numval, tb_numflag" & _
        " FROM tabnuma INNER JOIN tabduri ON tabnuma.codditt = tabduri.codditt" & _
        " AND tabnuma.tb_numcodl = tabduri.tb_anno" & _
        " AND tabnuma.tb_numflag = tabduri.tb_utipreg" & _
        " AND tabnuma.tb_numval = tabduri.tb_unumreg" & _
        " WHERE tabnuma.codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo = " & CStrSQL(strTipork) & _
        " AND tb_numserie = " & CStrSQL(strSerie) & _
        " AND tb_numcodl = " & nAnno & _
        " AND tb_ftfig = 'S'"
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    End Try
  End Function
  Public Overridable Function IsCausaleNulla(ByVal nCausale As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tb_codcaum FROM tabcaum" & _
        " WHERE tb_codcaum = " & nCausale & _
        " AND tb_esist = 0"
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    End Try
  End Function


  Public Overridable Function EstremiCorrispettivo(ByVal oDoc As NTSDoc) As DataTable
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tm_numsco, tm_datsco " & _
               " FROM testmagc " & _
               " WHERE " & oDoc.GeneraWhere.Replace("testmag", "testmagc")

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetAnaCona(ByVal strDitta As String, ByVal lConto As Integer, ByVal lSotgru As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT acn_perescon FROM anacona " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND acn_conto = " & lConto.ToString & _
              " AND acn_sotgru = " & lSotgru.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function PartitaGiàEsistente(ByVal strDitta As String, ByRef dtrT As DataRow, _
    ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tm_anno, tm_serie, tm_numdoc, tm_annpar, tm_alfpar, tm_numpar, tm_conto, an_descr1" & _
        " FROM testmag LEFT JOIN anagra ON testmag.codditt = anagra.codditt" & _
                                     " AND testmag.tm_conto = anagra.an_conto" & _
        " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
        " AND tm_tipork = " & CStrSQL(NTSCStr(dtrT!et_tipork)) & _
        " AND tm_conto = " & NTSCInt(dtrT!et_conto) & _
        " AND tm_annpar = " & NTSCInt(dtrT!et_annpar) & _
        " AND tm_alfpar = " & CStrSQL(NTSCStr(dtrT!et_alfpar)) & _
        " AND tm_numpar = " & NTSCInt(dtrT!et_numpar) & _
        " ORDER BY tm_tipork, tm_anno, tm_serie, tm_numdoc"
      dttOut = (OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI))
      If dttOut.Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

End Class