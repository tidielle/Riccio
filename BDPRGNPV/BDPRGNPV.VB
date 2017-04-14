Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica

Public Class CLDPRGNPV
  Inherits CLD__BASE

  Public Overridable Function ApriStrutturaProvvig(ByRef dttProvvigINSERT As DataTable, ByRef dttProvvigUPDATE As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT TOP 0 * FROM provvig"
      dttProvvigINSERT = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "SELECT TOP 0 codditt, pv_codage, pv_tipdoc, pv_anno, pv_serie," & _
              " pv_numdoc, pv_numrat, pv_origine, pv_datscad, pv_totfatt, pv_impopv," & _
              " pv_provv, pv_vtotfatt, pv_totomag, pv_flag, pv_segno FROM provvig"
      dttProvvigUPDATE = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ApriProvvig(ByVal strDittaCorrente As String, _
                                          ByVal nCodcage As Integer, ByVal strTipdoc As String, ByVal nAnno As Integer, _
                                          ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal nNumrat As Integer, _
                                          ByVal strOrigine As String, ByRef bEsisteProvvig As Boolean, ByRef dImpvpag As Decimal) As Boolean
    '------------------------------------------------------------------------------------
    Dim strSQL As String = ""
    Dim dttProvvig As New DataTable

    Try
      '----------------------------------------------------------------------------------
      bEsisteProvvig = False
      dImpvpag = 0
      '----------------------------------------------------------------------------------
      strSQL = "SELECT pv_impvpag FROM provvig" & vbCrLf & _
              " WHERE codditt = " & CStrSQL(strDittaCorrente) & _
              " AND pv_codage = " & nCodcage & _
              " AND pv_tipdoc = " & CStrSQL(strTipdoc) & _
              " AND pv_anno = " & nAnno & _
              " AND pv_serie = " & CStrSQL(strSerie) & _
              " AND pv_numdoc = " & lNumdoc & _
              " AND pv_numrat = " & nNumrat & _
              " AND pv_origine = " & CStrSQL(strOrigine)
      dttProvvig = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If Not dttProvvig Is Nothing Then
        If dttProvvig.Rows.Count <> 0 Then
          bEsisteProvvig = True
          dImpvpag = NTSCDec(dttProvvig.Rows(0)!pv_impvpag)
        End If
      End If
      '----------------------------------------------------------------------------------
      dttProvvig.Clear()
      dttProvvig = Nothing
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function


  Public Overridable Function AggiornaStatoIncassatoNew(ByVal strDittaCorrente As String, _
                                            ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                            ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                            ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                            ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                            ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                            ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                            ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                            ByVal bCancellaProvvigioni As Boolean, _
                                            ByVal nTipoBF As Integer, ByRef nRec As Integer) As Boolean
    'incassato nuovo sistema: aggiorno pv_origine da 'T' a 'Z' sui record di provvig di tipo 'T', 
    'pagati senza più scaden presente
    Dim strSQL As String = ""
    Dim strElencoTipiDoc As String = ""
    Try
      If bTipoDocumentoA = True Then strElencoTipiDoc += "'A',"
      If bTipoDocumentoD = True Then strElencoTipiDoc += "'D',"
      If bTipoDocumentoE = True Then strElencoTipiDoc += "'E',"
      If bTipoDocumentoN = True Then strElencoTipiDoc += "'N',"
      If bTipoDocumentoN1 = True Then strElencoTipiDoc += "'£',"
      If bTipoDocumentoC = True Then strElencoTipiDoc += "'C',"
      If bTipoDocumentoF = True Then strElencoTipiDoc += "'F',"
      If bTipoDocumentoS = True Then strElencoTipiDoc += "'S',"
      If bTipoDocumentoP = True Then strElencoTipiDoc += "'P',"
      If bTipoDocumentoB = True Then strElencoTipiDoc += "'B',"
      strElencoTipiDoc = Mid(strElencoTipiDoc, 1, (strElencoTipiDoc.Length - 1))

      strSQL = "UPDATE provvig SET pv_origine = 'Z' " & _
              " FROM provvig INNER JOIN testmag ON provvig.codditt = testmag.codditt" & _
              " AND provvig.pv_anno = testmag.tm_anno AND provvig.pv_tipdoc = testmag.tm_tipork" & _
              " AND provvig.pv_serie = testmag.tm_serie AND provvig.pv_numdoc = testmag.tm_numdoc" & _
              " INNER JOIN tabcage ON provvig.codditt = tabcage.codditt and provvig.pv_codage = tabcage.tb_codcage " & _
              " LEFT JOIN scaden ON provvig.codditt = scaden.codditt AND provvig.pv_conto = scaden.sc_conto" & _
              " AND provvig.pv_anno = scaden.sc_annpar AND provvig.pv_serie = scaden.sc_alfpar" & _
              " AND provvig.pv_numdoc = scaden.sc_numpar AND provvig.pv_numrat = scaden.sc_numrata" & _
              " WHERE provvig.codditt = " & CStrSQL(strDittaCorrente) & _
              " AND UPPER(pv_origine) = 'T'" & _
              " AND UPPER(tabcage.tb_tippro) = 'I'" & _
              " AND pv_tipdoc IN (" & strElencoTipiDoc & ")" & _
              " AND pv_impvpag <> 0 " & _
              " AND scaden.sc_conto is null "
      If (NTSCDate(strDatdocini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strDatdocfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND pv_datdoc BETWEEN " & CDataSQL(NTSCDate(strDatdocini)) & " AND " & CDataSQL(strDatdocfin)
      End If
      If (nAgenteini <> 0) Or (nAgentefin <> 9999) Then
        strSQL += " AND pv_codage BETWEEN " & nAgenteini & " AND " & nAgentefin
      End If
      If nTipoBF <> 0 Then strSQL += " AND tm_tipobf = " & nTipoBF

      'devo scartare le note di accredito contabilizzate con flag 'compensa direttamente con fattura'
      strSQL += " AND not(tm_numpar <> 0 AND tm_tipork = 'N' AND tm_totdoc = 0)"

      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggiornaStato(ByVal strDittaCorrente As String, _
                                          ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                          ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                          ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                          ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                          ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                          ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                          ByVal bTipoDocumentoB As Boolean, _
                                          ByVal bCancellaProvvigioni As Boolean, _
                                          ByVal nTipoBF As Integer) As Boolean
    Try
      'non più usata. mantenuta solo per compatibilità con versioni precedenti
      Return AggiornaStato(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                           bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                           bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                           bTipoDocumentoB, False, bCancellaProvvigioni, nTipoBF, True, False, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggiornaStato(ByVal strDittaCorrente As String, _
                                            ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                            ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                            ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                            ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                            ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                            ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                            ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                            ByVal bCancellaProvvigioni As Boolean, ByVal nTipoBF As Integer) As Boolean

    '----------------
    'per compatibilita' con funzioni ereditate da rive in versioni precedenti
    Dim oOut As Object = Nothing
    Dim oIn As New ArrayList(New Object() {strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                                           bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                                           bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                                           bTipoDocumentoB, False, bCancellaProvvigioni, nTipoBF})
    If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return CBool(oOut)
    End If
    '----------------

    'non più usata. mantenuta solo per compatibilità con versioni precedenti
    Return AggiornaStato(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                         bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                         bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                         bTipoDocumentoB, bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, True, False, "")
  End Function
  Public Overridable Function AggiornaStato(ByVal strDittaCorrente As String, _
                                            ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                            ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                            ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                            ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                            ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                            ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                            ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                            ByVal bCancellaProvvigioni As Boolean, ByVal nTipoBF As Integer, _
                                            ByVal bAggTestmag As Boolean) As Boolean

    '----------------
    'per compatibilita' con funzioni ereditate da rive in versioni precedenti
    Dim oOut As Object = Nothing
    Dim oIn As New ArrayList(New Object() {strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                                           bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                                           bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                                           bTipoDocumentoB, bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, _
                                           nTipoBF})
    If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return CBool(oOut)
    End If
    '----------------

    'non più usata. mantenuta solo per compatibilità con versioni precedenti
    Return AggiornaStato(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                         bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                         bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                         bTipoDocumentoB, bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, True, False, "")
  End Function
  Public Overridable Function AggiornaStato(ByVal strDittaCorrente As String, _
                                          ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                          ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                          ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                          ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                          ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                          ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                          ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                          ByVal bCancellaProvvigioni As Boolean, ByVal nTipoBF As Integer, _
                                          ByVal bRielabora As Boolean, ByVal bProvvig2 As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                                             bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                                             bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                                             bTipoDocumentoB, bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, _
                                             nTipoBF, bRielabora, bProvvig2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      'non più usata. mantenuta solo per compatibilità con versioni precedenti
      Return AggiornaStato(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                           bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                           bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                           bTipoDocumentoB, bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, bRielabora, bProvvig2, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggiornaStato(ByVal strDittaCorrente As String, _
                                            ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                            ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                            ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                            ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                            ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                            ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                            ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                            ByVal bCancellaProvvigioni As Boolean, ByVal nTipoBF As Integer, _
                                            ByVal bRielabora As Boolean, ByVal bProvvig2 As Boolean, _
                                            ByVal strWhere As String) As Boolean
    'cancello la precedente elaborazione
    '------------------------------------------------------------------------------------
    Dim bOk As Boolean = False
    Dim i As Integer = 0
    Dim strSQL As String = ""
    Dim strElencoTipiDoc As String = ""
    Dim strAgentiIncTot As String = ""
    Dim dttTabcage As New DataTable
    Dim strTipork As String = "'M', 'T'"

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                                             bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                                             bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                                             bTipoDocumentoB, bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, _
                                             bRielabora, bProvvig2, strWhere})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '----------------------------------------------------------------------------------
      If bTipoDocumentoA = True Then strElencoTipiDoc += "'A',"
      If bTipoDocumentoD = True Then strElencoTipiDoc += "'D',"
      If bTipoDocumentoE = True Then strElencoTipiDoc += "'E',"
      If bTipoDocumentoN = True Then strElencoTipiDoc += "'N',"
      If bTipoDocumentoN1 = True Then strElencoTipiDoc += "'£',"
      If bTipoDocumentoC = True Then strElencoTipiDoc += "'C',"
      If bTipoDocumentoF = True Then strElencoTipiDoc += "'F',"
      If bTipoDocumentoS = True Then strElencoTipiDoc += "'S',"
      If bTipoDocumentoP = True Then strElencoTipiDoc += "'P',"
      If bTipoDocumentoB = True Then strElencoTipiDoc += "'B',"
      strElencoTipiDoc = Mid(strElencoTipiDoc, 1, (strElencoTipiDoc.Length - 1))
      '----------------------------------------------------------------------------------
      '--- Crea un elenco degli agenti su 'Incassato Totale'
      '----------------------------------------------------------------------------------
      bOk = ElencoAgenti(strDittaCorrente, nAgenteini, nAgentefin, dttTabcage)
      If Not dttTabcage Is Nothing Then
        For i = 0 To (dttTabcage.Rows.Count - 1)
          strAgentiIncTot += dttTabcage.Rows(i)!tb_codcage.ToString & ","
        Next
      End If
      If strAgentiIncTot <> "" Then
        strAgentiIncTot = Mid(strAgentiIncTot, 1, (strAgentiIncTot.Length - 1))
      End If
      dttTabcage.Clear()
      dttTabcage = Nothing
      '----------------------------------------------------------------------------------
      strSQL = "UPDATE testmag" & _
          " SET tm_flprov = 'N'" & _
          " WHERE codditt = " & CStrSQL(strDittaCorrente) & _
          " AND UPPER(tm_proforma) <> 'S'" & _
          " AND (tm_codagen <> 0 OR tm_codagen2 <> 0)" & _
          " AND UPPER(tm_flprov) = 'S'" & _
          " AND tm_tipork IN (" & strElencoTipiDoc & ")"
      If (NTSCDate(strDatdocini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strDatdocfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND tm_datdoc BETWEEN " & CDataSQL(strDatdocini) & " AND " & CDataSQL(strDatdocfin)
      End If
      If nTipoBF <> 0 Then strSQL += " AND tm_tipobf = " & nTipoBF
      If (nAgenteini <> 0) Or (nAgentefin <> 9999) Then
        strSQL += " AND (tm_codagen BETWEEN " & nAgenteini & " AND " & nAgentefin & _
                  " OR tm_codagen2 BETWEEN " & nAgenteini & " AND " & nAgentefin & ")"
      End If
      If strWhere.Trim <> "" Then
        TraduciWhere(strWhere, strSQL)
      End If

      If bRielabora Then
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Else
        If bProvvig2 Then
          'devo sempre togliere il flag sui doc di magaz con provvig disallineato con scaden per data scadenza/importo/flag saldato/tipo pagamento
          strSQL += vbCrLf & " AND tm_tipork + '.' + cast(tm_anno as varchar(4)) + '.' + tm_serie + '.' + cast(tm_numdoc as varchar(9)) IN " & _
                            "               (SELECT pv_tipdoc + '.' + cast(pv_anno as varchar(4)) + '.' + pv_serie + '.' + cast(pv_numdoc as varchar(9)) " & _
                            "                FROM provvig LEFT JOIN scaden ON provvig.codditt = scaden.codditt " & _
                            "                AND provvig.pv_conto = scaden.sc_conto AND provvig.pv_annpart = scaden.sc_annpar " & _
                            "                AND provvig.pv_alfpart = scaden.sc_alfpar AND provvig.pv_numpart = scaden.sc_numpar " & _
                            "                AND provvig.pv_numrat = scaden.sc_numrata " & _
                            "                WHERE provvig.codditt = " & CStrSQL(strDittaCorrente) & _
                            "                AND provvig.pv_origine = 'T' " & _
                            "                AND pv_codage BETWEEN " & nAgenteini & " AND " & nAgentefin & " " & _
                            "                AND ((pv_tippaga <> sc_tippaga OR pv_impscad <> sc_importo OR pv_datscad <> sc_datsca" & _
                            "                OR pv_scflsaldato <> sc_flsaldato OR pv_dtsaldato <> sc_dtsaldato) OR sc_tippaga IS NULL)" & _
                            "              )"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
      End If
      '----------------------------------------------------------------------------------
      If bProvvig2 Then
        If bRielabora Then
          'rielaboro sempre tutto
        Else
          'provvigioni su incassatno nuovo e non rielaborazione:
          'devo rielaborare sempre tutti i documenti disallineati da scaden
          strTipork = "'T'"
        End If
      Else
        'tutti gli altri casi (rielabrazioni)
        'se bProvvig2 = false e non rielaborazione non arrivo neanche in questa routine
      End If
      strSQL = "DELETE provvig" & _
              " FROM provvig INNER JOIN testmag ON provvig.codditt = testmag.codditt AND provvig.pv_anno = testmag.tm_anno " & _
              " AND provvig.pv_tipdoc = testmag.tm_tipork AND provvig.pv_serie = testmag.tm_serie AND provvig.pv_numdoc = testmag.tm_numdoc"
      strSQL += " WHERE provvig.codditt = " & CStrSQL(strDittaCorrente) & _
              " AND UPPER(pv_origine) IN (" & strTipork & ")" & _
              " AND pv_tipdoc IN (" & strElencoTipiDoc & ")"
      If (NTSCDate(strDatdocini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strDatdocfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND pv_datdoc BETWEEN " & CDataSQL(NTSCDate(strDatdocini)) & " AND " & CDataSQL(strDatdocfin)
      End If
      If (nAgenteini <> 0) Or (nAgentefin <> 9999) Then
        strSQL += " AND pv_codage BETWEEN " & nAgenteini & " AND " & nAgentefin
      End If
      '--- Le provvigioni su 'Incassato Totale' non vengono mai cancellate,
      '--- ma sempre aggiornate
      If (bCancellaProvvigioni = False) And (strAgentiIncTot.Trim <> "") Then
        strSQL += " AND pv_codage NOT IN (" & strAgentiIncTot & ") "
      End If
      If nTipoBF <> 0 Then strSQL += " AND tm_tipobf = " & nTipoBF
      strSQL += " AND not (pv_origine = 'T' AND pv_impvpag <> 0)"  'non devo mai cancellare le provvigioni su 'incassato nuovo sistema' corrisposte (visto che ogni volta che estraggo devo rielaborare anche il pregresso!!!!)
      If strWhere.Trim <> "" Then
        TraduciWhere(strWhere, strSQL)
      End If

      If bProvvig2 And bRielabora = False Then
        'provvigioni su nuovo incassato:
        'quando elaboro senza RIELABORAZIONE cancello le provvigioni esistenti nel periodo in analisi SOLO se disallineate rispetto a scaden
        'per disallineate intendo o non più presenti i record in scaden o presenti ma con tipo/data saldo/importo diversi tra provvig e scaden
        strSQL += vbCrLf & " AND tm_tipork + '.' + cast(tm_anno as varchar(4)) + '.' + tm_serie + '.' + cast(tm_numdoc as varchar(9)) IN " & _
                  "               (SELECT pv_tipdoc + '.' + cast(pv_anno as varchar(4)) + '.' + pv_serie + '.' + cast(pv_numdoc as varchar(9)) " & _
                  "                FROM provvig LEFT JOIN scaden ON provvig.codditt = scaden.codditt " & _
                  "                AND provvig.pv_conto = scaden.sc_conto AND provvig.pv_annpart = scaden.sc_annpar " & _
                  "                AND provvig.pv_alfpart = scaden.sc_alfpar AND provvig.pv_numpart = scaden.sc_numpar " & _
                  "                AND provvig.pv_numrat = scaden.sc_numrata " & _
                  "                WHERE provvig.codditt = " & CStrSQL(strDittaCorrente) & _
                  "                AND provvig.pv_origine = 'T' " & _
                  "                AND pv_codage BETWEEN " & nAgenteini & " AND " & nAgentefin & " " & _
                  "                AND ((pv_tippaga <> sc_tippaga OR pv_impscad <> sc_importo OR pv_datscad <> sc_datsca" & _
                  "                OR pv_scflsaldato <> sc_flsaldato OR pv_dtsaldato <> sc_dtsaldato) OR sc_tippaga IS NULL)" & _
                  "              )"
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function AggiornaInserisciProvvigione(ByVal dttProvvigINSERT As DataTable, _
                                                          ByVal dttProvvigUPDATE As DataTable, _
                                                          ByVal dttTestmag As DataTable, _
                                                          ByVal nRiga As Integer) As Boolean
    Dim i As Integer = 0
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing

    Try
      '------------------------
      'Apre il Database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '------------------------
      If Not dttProvvigUPDATE Is Nothing Then
        For i = 0 To (dttProvvigUPDATE.Rows.Count - 1)
          With dttProvvigUPDATE.Rows(i)
            strSQL = "UPDATE provvig" & _
              " SET pv_datscad = " & CDataSQL(!pv_datscad.ToString) & "," & _
              " pv_totfatt = " & CDblSQL(!pv_totfatt.ToString) & "," & _
              " pv_impopv = " & CDblSQL(!pv_impopv.ToString) & "," & _
              " pv_provv = " & CDblSQL(!pv_provv.ToString) & "," & _
              " pv_vtotfatt = " & CDblSQL(!pv_vtotfatt.ToString) & "," & _
              " pv_totomag = " & CDblSQL(!pv_totomag.ToString) & "," & _
              " pv_flag = " & CStrSQL(!pv_flag) & _
              " WHERE codditt = " & CStrSQL(!codditt) & _
              " AND pv_codage = " & NTSCInt(!pv_codage) & _
              " AND pv_tipdoc = " & CStrSQL(!pv_tipdoc) & _
              " AND pv_anno = " & NTSCInt(!pv_anno) & _
              " AND pv_serie = " & CStrSQL(!pv_serie) & _
              " AND pv_numdoc = " & NTSCInt(!pv_numdoc) & _
              " AND pv_numrat = " & NTSCInt(!pv_numrat) & _
              " AND pv_origine = " & CStrSQL(!pv_origine)
          End With
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
      End If
      '-----------------------
      If Not dttProvvigINSERT Is Nothing Then
        For i = 0 To (dttProvvigINSERT.Rows.Count - 1)
          With dttProvvigINSERT.Rows(i)
            strSQL = "INSERT INTO provvig (codditt, pv_codage, pv_tipdoc, pv_anno," & _
              " pv_serie, pv_numdoc, pv_numrat, pv_datdoc, pv_datscad, pv_conto," & _
              " pv_annpart, pv_alfpart, pv_numpart, pv_totfatt, pv_impopv," & _
              " pv_provv, pv_impvmat, pv_impvpag, pv_codpaga, pv_tippaga, pv_flag," & _
              " pv_origine, pv_tipotass, pv_vtotfatt, pv_vimpopv, pv_vprovv," & _
              " pv_vimpvmat, pv_vimpvpag, pv_cambio, pv_codvalu, pv_totomag," & _
              " pv_totomagv, pv_segno, pv_datscadeff, pv_impscad, pv_datmatu, pv_datcorr)" & _
              " VALUES (" & CStrSQL(!codditt) & ", " & NTSCInt(!pv_codage) & "," & _
              CStrSQL(!pv_tipdoc) & "," & NTSCInt(!pv_anno) & "," & _
              CStrSQL(!pv_serie) & "," & NTSCInt(!pv_numdoc) & "," & _
              NTSCInt(!pv_numrat) & "," & CDataSQL(!pv_datdoc.ToString) & "," & _
              CDataSQL(!pv_datscad.ToString) & "," & NTSCInt(!pv_conto) & "," & _
              NTSCInt(!pv_annpart) & "," & CStrSQL(!pv_alfpart) & "," & _
              NTSCInt(!pv_numpart) & "," & CDblSQL(!pv_totfatt.ToString) & "," & _
              CDblSQL(!pv_impopv.ToString) & "," & CDblSQL(!pv_provv.ToString) & "," & _
              CDblSQL(!pv_impvmat.ToString) & "," & CDblSQL(!pv_impvpag.ToString) & "," & _
              NTSCInt(!pv_codpaga) & "," & NTSCInt(!pv_tippaga) & "," & _
              CStrSQL(!pv_flag) & "," & CStrSQL(!pv_origine) & "," & _
              CStrSQL(!pv_tipotass) & "," & CDblSQL(!pv_vtotfatt.ToString) & "," & _
              CDblSQL(!pv_vimpopv.ToString) & "," & CDblSQL(!pv_vprovv.ToString) & "," & _
              CDblSQL(!pv_vimpvmat.ToString) & "," & CDblSQL(!pv_vimpvpag.ToString) & "," & _
              CDblSQL(!pv_cambio.ToString) & "," & NTSCInt(!pv_codvalu) & "," & _
              CDblSQL(!pv_totomag.ToString) & "," & CDblSQL(!pv_totomagv.ToString) & "," & _
              NTSCInt(!pv_segno) & ", " & CDataSQL(!pv_datscad.ToString) & ", " & _
              CDblSQL(NTSCDec(!pv_impscad)) & ", " & CDataSQL(NTSCStr(!pv_datmatu)) & ", " & _
              CDataSQL(NTSCStr(!pv_datcorr)) & ")"
          End With
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
      End If
      '---------------------
      If nRiga > -1 Then
        With dttTestmag.Rows(nRiga)
          strSQL = "UPDATE testmag" & _
                    " SET tm_flprov = 'S'" & _
                    " WHERE codditt = " & CStrSQL(!codditt) & _
                    " AND tm_tipork = " & CStrSQL(!tm_tipork) & _
                    " AND tm_anno = " & NTSCInt(!tm_anno) & _
                    " AND tm_serie = " & CStrSQL(!tm_serie) & _
                    " AND tm_numdoc = " & NTSCInt(!tm_numdoc)
        End With
        If NTSCStr(dttTestmag.Rows(nRiga)!xx_trattato) <> "N" Then Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      '----------------------
      'Chiude la transazione e il Database
      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      '--- Se si è in transazione, la annullo
      '----------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      '----------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '----------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ElencoAgenti(ByVal strDitta As String, _
        ByVal nAgenteIni As Integer, ByVal nAgenteFin As Integer, _
        ByRef dttTabcage As DataTable) As Boolean
    '------------------------------------------------------------------------------------
    Dim strSQL As String = ""

    Try
      '----------------------------------------------------------------------------------
      strSQL = "SELECT tb_codcage FROM tabcage" & _
                " WHERE tb_tippro = 'T' " & _
                " AND codditt = " & CStrSQL(strDitta) & _
                " AND tb_codcage BETWEEN " & nAgenteIni & " AND " & nAgenteFin & _
                " ORDER BY codditt, tb_codcage"
      dttTabcage = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTabcage Is Nothing Then Return False
      If dttTabcage.Rows.Count = 0 Then Return False
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function EsisteProvvigione(ByVal strDittaCorrente As String, _
                          ByVal nCodcage As Integer, ByVal strTipdoc As String, ByVal nAnno As Integer, _
                          ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal nNumrat As Integer, _
                          ByVal strOrigine As String) As Boolean
    Try
      'obsoleta
      Return EsisteProvvigione(strDittaCorrente, nCodcage, strTipdoc, nAnno, strSerie, lNumdoc, nNumrat, strOrigine, New DataTable)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EsisteProvvigione(ByVal strDittaCorrente As String, ByVal nCodcage As Integer, _
                                                ByVal strTipdoc As String, ByVal nAnno As Integer, _
                                                ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                                ByVal nNumrat As Integer, ByVal strOrigine As String, _
                                                ByRef dttProvvig As DataTable) As Boolean
    '------------------------------------------------------------------------------------
    Dim strSQL As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDittaCorrente, nCodcage, strTipdoc, nAnno, strSerie, lNumdoc, _
                                             nNumrat, strOrigine, dttProvvig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttProvvig = CType(oIn(8), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      '----------------------------------------------------------------------------------
      strSQL = "SELECT * FROM provvig" & vbCrLf & _
        " WHERE codditt = " & CStrSQL(strDittaCorrente) & _
        " AND pv_codage = " & nCodcage & _
        " AND pv_tipdoc = " & CStrSQL(strTipdoc) & _
        " AND pv_anno = " & nAnno & _
        " AND pv_serie = " & CStrSQL(strSerie) & _
        " AND pv_numdoc = " & lNumdoc & _
        " AND pv_numrat = " & nNumrat & _
        " AND pv_origine IN (" & IIf(strOrigine = "M", "'M', 'T'", "'" & strOrigine & "'").ToString & ")"
      dttProvvig = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If Not dttProvvig Is Nothing Then
        If dttProvvig.Rows.Count <> 0 Then Return True
      End If
      '----------------------------------------------------------------------------------
      Return False
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CancellaProvvigione(ByVal strDittaCorrente As String, ByVal nProgr As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM provvig" & vbCrLf & _
                " WHERE codditt = " & CStrSQL(strDittaCorrente) & _
                " AND pv_progr = " & nProgr
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function EsistonoDocumenti(ByVal strDittaCorrente As String, _
                                              ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                              ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                              ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                              ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                              ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                              ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                              ByVal bTipoDocumentoB As Boolean, ByVal nTipoBF As Integer) As Boolean
    Try
      'non più usata. mantenuta solo per compatibilità con versioni precedenti
      Return EsistonoDocumenti(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                               bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                               bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                               bTipoDocumentoB, False, nTipoBF, New DataTable, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EsistonoDocumenti(ByVal strDittaCorrente As String, _
                                                ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                                ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                                ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                                ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                                ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                                ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                                ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                                ByVal nTipoBF As Integer, ByRef dttTestmag As DataTable, _
                                                ByVal bProvvig2 As Boolean) As Boolean
    '------------------------------------------------------------------------------------
    Dim strSQL As String = ""
    Dim strElencoTipiDoc As String = ""
    dttTestmag = New DataTable

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                                             bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                                             bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                                             bTipoDocumentoB, bTipoDocumentoN1, nTipoBF, dttTestmag, bProvvig2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTestmag = CType(oIn(16), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      '----------------------------------------------------------------------------------
      If bTipoDocumentoA = True Then strElencoTipiDoc += "'A',"
      If bTipoDocumentoD = True Then strElencoTipiDoc += "'D',"
      If bTipoDocumentoE = True Then strElencoTipiDoc += "'E',"
      If bTipoDocumentoN = True Then strElencoTipiDoc += "'N',"
      If bTipoDocumentoN1 = True Then strElencoTipiDoc += "'£',"
      If bTipoDocumentoC = True Then strElencoTipiDoc += "'C',"
      If bTipoDocumentoF = True Then strElencoTipiDoc += "'F',"
      If bTipoDocumentoS = True Then strElencoTipiDoc += "'S',"
      If bTipoDocumentoP = True Then strElencoTipiDoc += "'P',"
      If bTipoDocumentoB = True Then strElencoTipiDoc += "'B',"
      strElencoTipiDoc = Mid(strElencoTipiDoc, 1, (strElencoTipiDoc.Length - 1))
      '----------------------------------------------------------------------------------
      strSQL = "SELECT TOP 20 tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_flagiva_1 FROM testmag" & _
              " WHERE codditt = " & CStrSQL(strDittaCorrente) & _
              " AND tm_tipork IN (" & strElencoTipiDoc & ")" & _
              " AND UPPER(tm_proforma) <> 'S'" & _
              " AND (tm_codagen <> 0 OR tm_codagen2 <> 0)" & _
              " AND (tm_totprov <> 0 OR tm_totprov2 <> 0)" & _
              " AND (UPPER(tm_flprov) = 'N' "
      strSQL += " AND (tm_datdoc BETWEEN " & CDataSQL(strDatdocini) & " AND " & CDataSQL(strDatdocfin)
      If bProvvig2 Then
        strSQL += " OR tm_tipork + '.' + cast(tm_anno as varchar(4)) + '.' + tm_serie + '.' + cast(tm_numdoc as varchar(9)) IN " & _
                  " (SELECT pv_tipdoc + '.' + cast(pv_anno as varchar(4)) + '.' + pv_serie + '.' + cast(pv_numdoc as varchar(9)) " & _
                  " FROM provvig INNER JOIN scaden ON provvig.codditt = scaden.codditt " & _
                  " AND provvig.pv_conto = scaden.sc_conto AND provvig.pv_annpart = scaden.sc_annpar " & _
                  " AND provvig.pv_alfpart = scaden.sc_alfpar AND provvig.pv_numpart = scaden.sc_numpar " & _
                  " AND provvig.pv_numrat = scaden.sc_numrata " & _
                  " WHERE provvig.codditt = " & CStrSQL(strDittaCorrente) & _
                  " AND provvig.pv_origine = 'T' " & _
                  " AND pv_codage BETWEEN " & nAgenteini & " AND " & nAgentefin & " " & _
                  " AND (pv_tippaga <> sc_tippaga OR pv_impscad <> sc_importo OR pv_datscad <> sc_datsca" & _
                  "      OR pv_scflsaldato <> sc_flsaldato OR pv_dtsaldato <> sc_dtsaldato) " & _
                  " )))"
      Else
        strSQL += " AND UPPER(tm_flprov) = 'N'))"
      End If

      If (nAgenteini <> 0) Or (nAgentefin <> 9999) Then
        strSQL += " AND ((tm_codagen BETWEEN " & nAgenteini & " AND " & nAgentefin & ")" & _
          " OR (tm_codagen2 BETWEEN " & nAgenteini & " and " & nAgentefin & "))"
      End If
      If nTipoBF <> 0 Then strSQL += " AND tm_tipobf = " & nTipoBF
      strSQL += "ORDER BY tm_flagiva_1 DESC"
      '----------------------------------------------------------------------------------
      dttTestmag = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTestmag.Rows.Count = 0 Then
        Return False
      End If
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ProvvigioniPagate(ByVal strDittaCorrente As String, _
                                              ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                              ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                              ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                              ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                              ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                              ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                              ByVal bTipoDocumentoB As Boolean, _
                                              ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                              ByVal nTipoBF As Integer, _
                                              ByRef dttProvvig As DataTable) As Boolean
    Try
      'non più usata. mantenuta solo per compatibilità con versioni precedenti
      Return ProvvigioniPagate(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                               bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                               bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                               bTipoDocumentoB, bRielabora, bCancellaProvvigioni, nTipoBF, dttProvvig)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ProvvigioniPagate(ByVal strDittaCorrente As String, _
                                                ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                                ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                                ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                                ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                                ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                                ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                                ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                                ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                                ByVal nTipoBF As Integer, _
                                                ByRef dttProvvig As DataTable) As Boolean
    '------------------------------------------------------------------------------------
    Dim bOk As Boolean = False
    Dim i As Integer = 0
    Dim strSQL As String = ""
    Dim strElencoTipiDoc As String = ""
    Dim strAgentiIncTot As String = ""
    Dim dttTmp As New DataTable

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                                             bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                                             bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                                             bTipoDocumentoB, bTipoDocumentoN1, bRielabora, bCancellaProvvigioni, _
                                             nTipoBF, dttProvvig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttProvvig = CType(oIn(18), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      '----------------------------------------------------------------------------------
      bOk = ElencoAgenti(strDittaCorrente, nAgenteini, nAgentefin, dttTmp)
      If Not dttTmp Is Nothing Then
        For i = 0 To (dttTmp.Rows.Count - 1)
          strAgentiIncTot += dttTmp.Rows(i)!tb_codcage.ToString & ","
        Next
      End If
      dttTmp.Clear()
      dttTmp = Nothing
      If strAgentiIncTot <> "" Then
        strAgentiIncTot = Mid(strAgentiIncTot, 1, (strAgentiIncTot.Length - 1))
      End If
      '----------------------------------------------------------------------------------
      '--- Se è stata scelta la Rielaborazione, controlla se esistono delle Provvigioni
      '--- già pagate da eliminare
      '----------------------------------------------------------------------------------
      If bTipoDocumentoA = True Then strElencoTipiDoc += "'A',"
      If bTipoDocumentoD = True Then strElencoTipiDoc += "'D',"
      If bTipoDocumentoE = True Then strElencoTipiDoc += "'E',"
      If bTipoDocumentoN = True Then strElencoTipiDoc += "'N',"
      If bTipoDocumentoN1 = True Then strElencoTipiDoc += "'£',"
      If bTipoDocumentoC = True Then strElencoTipiDoc += "'C',"
      If bTipoDocumentoF = True Then strElencoTipiDoc += "'F',"
      If bTipoDocumentoS = True Then strElencoTipiDoc += "'S',"
      If bTipoDocumentoP = True Then strElencoTipiDoc += "'P',"
      If bTipoDocumentoB = True Then strElencoTipiDoc += "'B',"
      strElencoTipiDoc = Mid(strElencoTipiDoc, 1, (strElencoTipiDoc.Length - 1))
      '----------------------------------------------------------------------------------
      strSQL = "SELECT provvig.*, tb_descage" & _
        " FROM (provvig INNER JOIN testmag ON provvig.codditt = testmag.codditt AND provvig.pv_anno = testmag.tm_anno AND provvig.pv_tipdoc = testmag.tm_tipork AND provvig.pv_serie = testmag.tm_serie AND provvig.pv_numdoc = testmag.tm_numdoc)" & _
        " INNER JOIN tabcage ON provvig.codditt = tabcage.codditt AND provvig.pv_codage = tabcage.tb_codcage" & _
        " WHERE provvig.codditt = " & CStrSQL(strDittaCorrente) & _
        " AND pv_origine IN ('M', 'm')" & _
        " AND pv_datdoc BETWEEN " & CDataSQL(NTSCDate(strDatdocini)) & " AND " & CDataSQL(strDatdocfin) & _
        " AND pv_impvpag > 0" & _
        " AND pv_tipdoc IN (" & strElencoTipiDoc & ")"
      If nTipoBF <> 0 Then strSQL += " AND tm_tipobf = " & nTipoBF
      If (nAgenteini <> 0) Or (nAgentefin <> 9999) Then
        strSQL += " AND pv_codage BETWEEN " & nAgenteini & " AND " & nAgentefin
      End If
      If (strAgentiIncTot <> "") And (bCancellaProvvigioni = False) Then
        strSQL += " AND pv_codage NOT IN (" & strAgentiIncTot & ")"
      End If
      dttProvvig = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function SelezionaDocumenti(ByVal strDittaCorrente As String, _
                                              ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                              ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                              ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                              ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                              ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                              ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                              ByVal bTipoDocumentoB As Boolean, _
                                              ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                              ByVal nTipoBF As Integer, ByVal strDesTipoBF As String, _
                                              ByRef dttTestmag As DataTable) As Boolean
    Try
      'non più usata. mantenuta solo per compatibilità con versioni precedenti
      Return SelezionaDocumenti(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                               bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                               bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                               bTipoDocumentoB, False, False, bRielabora, bCancellaProvvigioni, nTipoBF, _
                               strDesTipoBF, "M", dttTestmag, False, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelezionaDocumenti(ByVal strDittaCorrente As String, _
                                               ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                               ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                               ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                               ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                               ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                               ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                               ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                               ByVal bTipoDocumentoM As Boolean, ByVal bRielabora As Boolean, _
                                               ByVal bCancellaProvvigioni As Boolean, _
                                               ByVal nTipoBF As Integer, ByVal strDesTipoBF As String, _
                                               ByVal strOrigine As String, ByRef dttTestmag As DataTable, _
                                               ByVal bProvvig2 As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                                             bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                                             bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                                             bTipoDocumentoB, bTipoDocumentoN1, bTipoDocumentoM, bRielabora, _
                                             bCancellaProvvigioni, nTipoBF, strDesTipoBF, strOrigine, dttTestmag, _
                                             bProvvig2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTestmag = CType(oIn(21), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      Return SelezionaDocumenti(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                               bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                               bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                               bTipoDocumentoB, bTipoDocumentoN1, bTipoDocumentoM, bRielabora, _
                               bCancellaProvvigioni, nTipoBF, strDesTipoBF, strOrigine, dttTestmag, bProvvig2, "")

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelezionaDocumenti(ByVal strDittaCorrente As String, _
                                                 ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                                 ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                                 ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                                 ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                                 ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                                 ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                                 ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                                 ByVal bTipoDocumentoM As Boolean, ByVal bRielabora As Boolean, _
                                                 ByVal bCancellaProvvigioni As Boolean, _
                                                 ByVal nTipoBF As Integer, ByVal strDesTipoBF As String, _
                                                 ByVal strOrigine As String, ByRef dttTestmag As DataTable, _
                                                 ByVal bProvvig2 As Boolean, ByVal strWhere As String) As Boolean
    '------------------------------------------------------------------------------------
    Dim strSQL As String = ""
    Dim strElencoTipiDoc As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                                             bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, bTipoDocumentoN, _
                                             bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, bTipoDocumentoP, _
                                             bTipoDocumentoB, bTipoDocumentoN1, bTipoDocumentoM, bRielabora, _
                                             bCancellaProvvigioni, nTipoBF, strDesTipoBF, strOrigine, dttTestmag, _
                                             bProvvig2, strWhere})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttTestmag = CType(oIn(21), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      '----------------------------------------------------------------------------------
      If bTipoDocumentoA = True Then strElencoTipiDoc += "'A',"
      If bTipoDocumentoD = True Then strElencoTipiDoc += "'D',"
      If bTipoDocumentoE = True Then strElencoTipiDoc += "'E',"
      If bTipoDocumentoN = True Then strElencoTipiDoc += "'N',"
      If bTipoDocumentoN1 = True Then strElencoTipiDoc += "'£',"
      If bTipoDocumentoC = True Then strElencoTipiDoc += "'C',"
      If bTipoDocumentoF = True Then strElencoTipiDoc += "'F',"
      If bTipoDocumentoS = True Then strElencoTipiDoc += "'S',"
      If bTipoDocumentoP = True Then strElencoTipiDoc += "'P',"
      If bTipoDocumentoB = True Then strElencoTipiDoc += "'B',"
      strElencoTipiDoc = Mid(strElencoTipiDoc, 1, (strElencoTipiDoc.Length - 1))
      '----------------------------------------------------------------------------------
      strSQL = "SELECT testmag.*, anagra.an_stato, tabpaga.*, tabstat.tb_siglacee" & _
              " FROM ((testmag INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto)" & _
              " INNER JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga)" & _
              " LEFT JOIN tabstat ON anagra.an_stato = tabstat.tb_codstat" & _
              " WHERE testmag.codditt = " & CStrSQL(strDittaCorrente) & _
              " AND UPPER(tm_proforma) <> 'S'" & _
              " AND (tm_codagen <> 0 OR tm_codagen2 <> 0)" & _
              " AND (tm_totprov <> 0 OR tm_totprov2 <> 0)" & _
              " AND (tm_tipork IN (" & strElencoTipiDoc & ")"
      If bTipoDocumentoM Then
        strSQL += " OR tm_tipork = 'M' AND an_tipo = 'C')"    'resi da cliente: utilizzato solo in previsionale
      Else
        strSQL += ")"
      End If
      strSQL += " AND (UPPER(tm_flprov) = 'N' AND (tm_datdoc BETWEEN " & CDataSQL(strDatdocini) & " AND " & CDataSQL(strDatdocfin)
      If bProvvig2 Then
        strSQL += " OR tm_tipork + '.' + cast(tm_anno as varchar(4)) + '.' + tm_serie + '.' + cast(tm_numdoc as varchar(9)) IN " & _
                  " (SELECT pv_tipdoc + '.' + cast(pv_anno as varchar(4)) + '.' + pv_serie + '.' + cast(pv_numdoc as varchar(9)) " & _
                  " FROM provvig INNER JOIN scaden ON provvig.codditt = scaden.codditt " & _
                  " AND provvig.pv_conto = scaden.sc_conto AND provvig.pv_annpart = scaden.sc_annpar " & _
                  " AND provvig.pv_alfpart = scaden.sc_alfpar AND provvig.pv_numpart = scaden.sc_numpar " & _
                  " AND provvig.pv_numrat = scaden.sc_numrata " & _
                  " WHERE provvig.codditt = " & CStrSQL(strDittaCorrente) & _
                  " AND provvig.pv_origine = 'T' " & _
                  " AND pv_codage BETWEEN " & nAgenteini & " AND " & nAgentefin & " " & _
                  " AND (pv_tippaga <> sc_tippaga OR pv_impscad <> sc_importo OR pv_datscad <> sc_datsca" & _
                  "      OR pv_scflsaldato <> sc_flsaldato OR pv_dtsaldato <> sc_dtsaldato) " & _
                  " )))"
      Else
        strSQL += " AND UPPER(tm_flprov) = 'N'))"
      End If
      If (nAgenteini <> 0) Or (nAgentefin <> 9999) Then
        strSQL += " AND (tm_codagen BETWEEN " & nAgenteini & " AND " & nAgentefin & _
          " OR tm_codagen2 BETWEEN " & nAgenteini & " AND " & nAgentefin & ")"
      End If
      If nTipoBF <> 0 Then strSQL += " AND tm_tipobf = " & nTipoBF
      If strWhere.Trim <> "" Then
        TraduciWhere(strWhere, strSQL)
      End If
      '----------------------------------------------------------------------------------
      dttTestmag = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTestmag.Rows.Count = 0 Then
        dttTestmag.Clear()
        dttTestmag = Nothing
        Return False
      End If
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function SelezionaOrdini(ByVal strDittaCorrente As String, _
                                              ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                              ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                              ByRef dttTestord As DataTable) As Boolean
    '----------------
    'la query deve restituire gli stessi campi della 'SelezionaDocumenti', visto che viene utilizzata per lo stesso scopo
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Try

      '----------------
      strSQL = "SELECT testord.*, anagra.an_stato, tabpaga.*, tabstat.tb_siglacee, "
      For i = 1 To 8
        strSQL += "0.0 as tm_impsca_" & i.ToString & ", " & _
                  "0.0 as tm_impscav_" & i.ToString & ", " & _
                  "cast(null as datetime) as tm_datsca_" & i.ToString & ", " & _
                  "0 as tm_tippaga_" & i.ToString & ", " & _
                  "0.0 as tm_imposta_" & i.ToString & ", " & _
                  "0.0 as tm_impostav_" & i.ToString & ", "
      Next
      strSQL += "'N' as tm_flfatt, mo_valore, mo_valoremm, mo_vprovv, mo_vprovv2, mo_codiva "
      strSQL += " FROM " & strJoinTestordMovord & _
              " INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto" & _
              " LEFT JOIN tabpaga ON testord.td_codpaga = tabpaga.tb_codpaga" & _
              " LEFT JOIN tabstat ON anagra.an_stato = tabstat.tb_codstat" & _
              " WHERE testord.codditt = " & CStrSQL(strDittaCorrente) & _
              " AND (td_codagen <> 0 OR td_codagen2 <> 0)" & _
              " AND (td_totprov <> 0 OR td_totprov2 <> 0)" & _
              " AND td_tipork = 'R'" & _
              " AND mo_flevas = 'C'" & _
              " AND (mo_vprovv <> 0 or mo_vprovv2 <> 0) "
      If (NTSCDate(strDatdocini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strDatdocfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND mo_datcons BETWEEN " & CDataSQL(strDatdocini) & " AND " & CDataSQL(strDatdocfin)
      End If
      If (nAgenteini <> 0) Or (nAgentefin <> 9999) Then
        strSQL += " AND (td_codagen BETWEEN " & nAgenteini & " AND " & nAgentefin & _
                  " OR td_codagen2 BETWEEN " & nAgenteini & " AND " & nAgentefin & ")"
      End If

      dttTestord = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTestord.Rows.Count = 0 Then
        dttTestord.Clear()
        dttTestord = Nothing
        Return False
      End If

      '----------------
      'rinomino le colonne, così opera come testmag
      dttTestord.Columns("td_numord").ColumnName = "tm_numdoc"
      dttTestord.Columns("td_datord").ColumnName = "tm_datdoc"
      For Each col As DataColumn In dttTestord.Columns
        If col.ColumnName.Substring(0, 2).ToLower = "td" Or col.ColumnName.Substring(0, 2).ToLower = "mo" Then
          col.ColumnName = "tm" & col.ColumnName.Substring(2)
        End If
      Next

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function RitornaTipoAss(ByVal strDitta As String, ByVal lConto As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    RitornaTipoAss = "I"
    Try
      strSQL = "SELECT an_stato FROM anagra" & _
       " WHERE codditt = " & CStrSQL(strDitta) & _
       " AND an_conto = " & lConto.ToString & " ORDER BY an_conto"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        Return "I"
      Else
        If (NTSCStr(dttTmp.Rows(0)!an_stato).Trim = "") Or (NTSCStr(dttTmp.Rows(0)!an_stato).ToUpper = "I") Then
          Return "I"
        Else
          strSQL = "SELECT tb_siglacee FROM tabstat WHERE tb_codstat = '" & NTSCStr(dttTmp.Rows(0)!an_stato) & "' ORDER BY tb_codstat"
          dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp1.Rows.Count <> 0 Then
            If NTSCStr(dttTmp1.Rows(0)!tb_siglacee).Trim = "" Then
              Return "X"
            Else
              If NTSCStr(dttTmp1.Rows(0)!tb_siglacee).Trim = "" Then Return "X" Else Return "E"
            End If
          Else
            Return "X"
          End If
        End If
      End If

      Return "I"

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp1.Clear()
    End Try
  End Function

  Public Overridable Function GetScadenProvvig(ByVal strDitta As String, ByVal lConto As Integer, _
                                               ByVal nAnnpar As Integer, ByVal strAlfpar As String, _
                                               ByVal lNumpar As Integer, ByRef dttOut As DataTable) As Boolean
    'prelevo le scadenze collegate alla fattura
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT scaden.*, testmag.tm_numregom " & _
               " FROM scaden LEFT JOIN testmag ON scaden.codditt = testmag.codditt " & _
               " AND scaden.sc_dtsaldato = testmag.tm_datregom " & _
               " AND scaden.sc_rgsaldato = testmag.tm_numregom " & _
               " WHERE scaden.codditt = " & CStrSQL(strDitta) & _
               " AND sc_conto = " & lConto & _
               " AND sc_annpar = " & nAnnpar & _
               " AND sc_alfpar = " & CStrSQL(strAlfpar) & _
               " AND sc_numpar = " & lNumpar & _
               " ORDER BY sc_numrata"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "Simulazioni"
  Public Overridable Function SimCheck(ByVal strDitta As String, ByVal strDatdocini As String, _
                                       ByVal strDatdocfin As String, ByVal nAgenteini As Integer, _
                                       ByVal nAgentefin As Integer) As Boolean
    'ritorna true se nel periodo da elaborare sono presenti delle righe di simulazione
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT TOP 1 pv_conto FROM provvig " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND pv_origine = 'X'"
      If NTSCDate(strDatdocini) <> NTSCDate(IntSetDate("01/01/1900")) Or _
         NTSCDate(strDatdocfin) <> NTSCDate(IntSetDate("31/12/2099")) Then
        strSQL += " AND pv_datdoc BETWEEN " & CDataSQL(NTSCDate(strDatdocini)) & " AND " & CDataSQL(strDatdocfin)
      End If
      If nAgenteini <> 0 Or nAgentefin <> 9999 Then
        strSQL += " AND pv_codage BETWEEN " & nAgenteini & " AND " & nAgentefin
      End If
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
  Public Overridable Function SimDelete(ByVal strDitta As String, ByVal strDatdocini As String, _
                                        ByVal strDatdocfin As String, ByVal nAgenteini As Integer, _
                                        ByVal nAgentefin As Integer) As Boolean
    'cancello la simulazione
    Dim strSQL As String = ""
    Dim nRec As Integer = 0
    Try
      strSQL = "DELETE FROM provvig " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND pv_origine = 'X'"
      If NTSCDate(strDatdocini) <> NTSCDate(IntSetDate("01/01/1900")) Or _
         NTSCDate(strDatdocfin) <> NTSCDate(IntSetDate("31/12/2099")) Then
        strSQL += " AND pv_datdoc BETWEEN " & CDataSQL(NTSCDate(strDatdocini)) & " AND " & CDataSQL(strDatdocfin)
      End If
      If nAgenteini <> 0 Or nAgentefin <> 9999 Then
        strSQL += " AND pv_codage BETWEEN " & nAgenteini & " AND " & nAgentefin
      End If
      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#End Region

End Class