Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLD__HLTB
  Inherits CLD__BASE

  Public Overridable Function GetEsclusioni(ByVal strDitta As String, ByVal strNometab As String, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try

      strSQL = "SELECT * FROM eszoom WHERE es_nome = " & CStrSQL(strNometab) & _
               " AND (es_ditta = '*' OR es_ditta = " & CStrSQL(strDitta) & ") "
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ESZOOM")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function GetData(ByVal strNomeZoom As String, ByRef dsHltb As DataSet, _
                                    ByVal strDitta As String, ByVal strDescr As String, _
                                    ByVal strCodice As String, ByVal nAnno As Integer, _
                                    ByVal nEscomp As Integer, ByVal lConto As Integer, _
                                    ByVal lCommessa As Integer, ByVal strTipork As String) As Boolean
    Try
      'obsoleta
      Return GetData(strNomeZoom, dsHltb, strDitta, strDescr, strCodice, nAnno, nEscomp, _
                     lConto, lCommessa, strTipork, True)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetData(ByVal strNomeZoom As String, ByRef dsHltb As DataSet, _
                                      ByVal strDitta As String, ByVal strDescr As String, _
                                      ByVal strCodice As String, ByVal nAnno As Integer, _
                                      ByVal nEscomp As Integer, ByVal lConto As Integer, _
                                      ByVal lCommessa As Integer, ByVal strTipork As String, _
                                      ByVal bIgnoraEsclusioni As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim strOrder As String = ""
    Dim strTabella As String = ""
    Dim bPrc As Boolean = False
    Dim strT() As String = Nothing
    Dim dttTmp As New DataTable

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strNomeZoom, dsHltb, strDitta, strDescr, strCodice, nAnno, nEscomp, _
                                             lConto, lCommessa, strTipork, bIgnoraEsclusioni})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsHltb = CType(oIn(1), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      If strDescr.Length > 0 Then strDescr = strDescr & "*"
      strDescr = strDescr.Replace("**", "*")
      strDescr = strDescr.Replace("*", "%")

      If strNomeZoom.Length = 11 AndAlso _
         strNomeZoom.Substring(0, 7) = "ZOOMTAB" AndAlso _
         strNomeZoom <> "ZOOMTABCAUC" AndAlso _
         strNomeZoom <> "ZOOMTABATTI" AndAlso _
         strNomeZoom <> "ZOOMTABANAZ" AndAlso _
         strNomeZoom <> "ZOOMTABLINGP" AndAlso _
         strNomeZoom <> "ZOOMTABSMEL" AndAlso _
         strNomeZoom <> "ZOOMTABPROC" AndAlso _
         strNomeZoom <> "ZOOMTABELLE" AndAlso _
         strNomeZoom <> "ZOOMTABCOVE" AndAlso _
         strNomeZoom <> "ZOOMTABDICV" Then
        strTabella = strNomeZoom.Substring(7, 4).ToLower

        '----------------------------------------------------------
        'ZOOM TABELLA STANDARD: COMPONGO LA SELECT
        strSQL = "SELECT tb_cod" & strTabella & " as tb_codice, " & _
                 "       tb_des" & strTabella & " as tb_descr, * " & _
                 " FROM tab" & strTabella

        '----------------------------------------------------------
        'COMPONGO LA WHERE STANDARD
        If strDescr <> "" Then
          strWhere = " WHERE tb_des" & strTabella & " LIKE " & CStrSQL(strDescr)
        End If
        'se serve aggiungo il filtro ditta
        If IsPerDitta(True, "tab" & strTabella) Then
          If strWhere = "" Then
            strWhere = " WHERE codditt = " & CStrSQL(strDitta)
          Else
            strWhere += " AND codditt = " & CStrSQL(strDitta)
          End If
        End If

        '----------------------------------------------------------
        'COMPONGO LA WHERE CON FILTRI PARTICOLARI
        'per alcune tabelle particolari devo aggiungere altri filtri

        Select Case strTabella
          Case "mast", "gruc", "clas"
            If strWhere = "" Then
              strWhere = " WHERE tb_codpcon = " & CStrSQL(strCodice)
            Else
              strWhere += " AND tb_codpcon = " & CStrSQL(strCodice)
            End If
          Case "caus"
            If strWhere = "" Then
              strWhere = " WHERE tb_anno = " & nAnno
            Else
              strWhere += " AND tb_anno = " & nAnno
            End If
          Case "usat"
            If strWhere = "" Then
              If Trim(strTipork) <> "" Then strWhere = " WHERE tb_ceduto = " & CStrSQL(strTipork)
            Else
              If Trim(strTipork) <> "" Then strWhere += " AND tb_ceduto = " & CStrSQL(strTipork)
            End If
          Case "spce"
            If strWhere = "" Then
              If lCommessa <> 0 Then strWhere = " WHERE tb_grce = " & lCommessa.ToString
            Else
              If lCommessa <> 0 Then strWhere += " AND tb_grce = " & lCommessa.ToString
            End If
          Case "puce"
            If strWhere = "" Then
              If Trim(strTipork) <> "" Then strWhere = " WHERE tb_spce = " & CStrSQL(strTipork)
            Else
              If Trim(strTipork) <> "" Then strWhere += " AND tb_spce = " & CStrSQL(strTipork)
            End If
          Case "mac2"
            If strCodice.Trim.Length <> 0 Then
              If strWhere = "" Then
                strWhere = " WHERE tb_codpcca = " & CStrSQL(strCodice)
              Else
                strWhere += " AND tb_codpcca = " & CStrSQL(strCodice)
              End If
            End If
          Case "dica"
            If strTipork.Trim.Length <> 0 Then
              If strWhere = "" Then
                strWhere = " WHERE tb_applicaa = " & CStrSQL(strTipork)
              Else
                strWhere += " AND tb_applicaa = " & CStrSQL(strTipork)
              End If
            End If
          Case "tcdc"
            If strTipork.Trim.Length <> 0 Then
              If strWhere = "" Then
                strWhere = " WHERE tb_tipork = " & CStrSQL(strTipork)
              Else
                strWhere += " AND tb_tipork = " & CStrSQL(strTipork)
              End If
            End If
          Case "dwim"
            If strCodice.Trim.Length <> 0 Then
              If strWhere = "" Then
                strWhere = " WHERE tb_dwscenario = " & strCodice
              Else
                strWhere += " AND tb_dwscenario = " & strCodice
              End If
            End If
          Case "escg"
            If strCodice.Trim.Length <> 0 Then
              If strWhere = "" Then
                strWhere = " WHERE tb_codgrua = " & strCodice
              Else
                strWhere += " AND tb_codgrua = " & strCodice
              End If
            End If
          Case "caca"
            If strCodice.Trim.Length <> 0 Then
              Dim strSQLComune As String = _
              "(tb_codpcca = " & CStrSQL(strCodice) & " or LEN(LTRIM(tb_codpcca))=0)"
              If strWhere = "" Then
                strWhere = " WHERE " & strSQLComune
              Else
                strWhere += " AND  " & strSQLComune
              End If
            End If

            If strTipork.Trim.Length <> 0 Then
              If strWhere = "" Then
                strWhere = " WHERE tb_tipocaca = " & CStrSQL(strTipork)
              Else
                strWhere += " AND  tb_tipocaca = " & CStrSQL(strTipork)
              End If
            End If
          Case "cope"
            If strCodice.Trim.Length <> 0 Then
              If strWhere = "" Then
                strWhere = " WHERE tb_ocodqpro = " & CStrSQL(strCodice)
              Else
                strWhere += " AND tb_ocodqpro = " & CStrSQL(strCodice)
              End If
            End If
          Case "rere"
            'Usa la commessa come codice stabilimento, ed è un filtro che viene passato solo in alcuni casi
            If lCommessa <> 0 Then
              If strWhere = "" Then
                strWhere = " WHERE tb_codstab = " & lCommessa
              Else
                strWhere += " AND tb_codstab = " & lCommessa
              End If
            End If
          Case "tpbf"
            'se è stato passato il tipork devofar vedere solo i tipibf abilitati per quel tipork
            If strTipork.Trim <> "" Then
              strWhere += " AND (tb_tiporkok like '%" & strTipork & "%' OR tb_tiporkok is null OR tb_tiporkok = '')"
            End If
          Case "sezd"
            bPrc = True
          Case "stco"
            If strTipork <> "" Then
              If strWhere = "" Then
                strWhere = " WHERE tb_usatoper IN ('E', " & CStrSQL(strTipork) & ")"
              Else
                strWhere += " AND tb_usatoper IN ('E', " & CStrSQL(strTipork) & ")"
              End If
            End If
        End Select

        '----------------------------------------------------------
        'COMPONGO LA ORDER BY
        strOrder = " ORDER BY tb_cod" & strTabella

        '----------------------------------------------------------
        'MONTO TUTTA LA QUERY
        strSQL = strSQL & strWhere & strOrder
      Else
        'zoom specifici
        Select Case strNomeZoom
          Case "ZOOMCOMUNI"
            strSQL = CompilaQueryComuni(strDescr)
          Case "ZOOMABI"
            strSQL = CompilaQueryAbi(strDescr)
            bPrc = True
          Case "ZOOMTRCFLD"
            strSQL = CompilaQueryCfld(strDescr)
            bPrc = True
          Case "ZOOMTARIC"
            strSQL = CompilaQueryTaric(strDescr)
            bPrc = True
          Case "ZOOMANAGPC"
            strSQL = CompilaQueryAnagPc(strCodice, strDescr)
          Case "ZOOMTABANAZ"
            strSQL = CompilaQueryTabanaz(strDescr)
          Case "ZOOMTABCAUC"
            strSQL = CompilaQueryTabCauc(strDitta, strCodice, nEscomp, strDescr)
          Case "ZOOMDESTDIV"
            strSQL = CompilaQueryDestdiv(strDitta, lConto, strDescr)
          Case "ZOOMSUBCOMM"
            strSQL = CompilaQuerySubCommessa(strDitta, lCommessa, strDescr)
          Case "ZOOMANAGCA"
            strSQL = CompilaQueryAnagca(strDitta, strDescr)
          Case "ZOOMANALINK"
            strSQL = CompilaQueryAnalink(strDitta, lConto, strDescr)
          Case "ZOOMBUDGETCADACONTO"
            Select Case strTipork
              Case "S"
                strSQL = CompilaQueryBudgetSottocommessaDaContoca(strDitta, lConto, lCommessa, strDescr)
              Case Else
                strSQL = CompilaQueryBudgetDaContoca(strDitta, strTipork, nAnno, lConto, strDescr)
            End Select
          Case "ZOOMTABATTI"
            strSQL = CompilaQueryTabatti(strDitta, nAnno, strDescr)
          Case "ZOOMTABSMEL"
            strSQL = CompilaQueryTabsmel(strDitta, strTipork, strDescr)
          Case "ZOOMNOMPROP"
            strT = strTipork.Split(CChar("|"))
            strSQL = CompilaQueryNomprop(strT(0), strT(1), strT(2), strDescr)
            bPrc = True
          Case "ZOOMVALPROP"
            strT = strTipork.Split(CChar("|"))
            strSQL = CompilaQueryValprop(strT(0), strT(1), strT(2), strT(3))
            bPrc = True
          Case "ZOOMLEADS"
            strSQL = CompilaQueryLeads(strDitta, strDescr, CBool(nAnno))
          Case "ZOOMARTFASI"
            strSQL = CompilaQueryArtfasi(strDitta, strTipork, strDescr)
          Case "ZOOMTABLINGP"
            strSQL = CompilaQueryTablingp(strDescr)
            bPrc = True
          Case "ZOOMDISTBAS"
            strSQL = CompilaQueryDistbas(strDitta, strDescr)
          Case "ZOOMRUOLI"
            bPrc = True
            strSQL = CompilaQueryRuoli(strDitta, strDescr)
          Case "ZOOMOPERAT"
            bPrc = True
            strSQL = CompilaQueryOperat(strDitta, strDescr)
          Case "ZOOMORUOLIPERAT"
            bPrc = True
            strSQL = CompilaQueryRuoliOperat(strDitta, strDescr)
          Case "ZOOMAZIENDE"
            bPrc = True
            strSQL = CompilaQueryAziende(strDitta, strDescr)
          Case "ZOOMTIPIALERT"
            strSQL = CompilaQueryTipiAlert(strDitta, strDescr)
          Case "ZOOMTABPROC"
            strSQL = CompilaQueryTabproc(strDitta, strDescr)
            bPrc = True
          Case "ZOOMVALVARI"
            strSQL = CompilaQueryValvari(strDitta, strCodice, strDescr)
          Case "ZOOMVALVARIESPL"
            strSQL = CompilaQueryValvariEsplose(strDitta, strCodice, nAnno, strDescr)
          Case "ZOOMVALVARIESPLTC"
            strSQL = CompilaQueryValvariEsplose(strDitta, strCodice, 1, strDescr)
          Case "ZOOMTRIBUTI"
            strSQL = CompilaQueryTabtrib(nAnno, nEscomp)
          Case "ZOOMTABELLE"
            strSQL = CompilaQueryTabelle(strDescr)
            bPrc = True
          Case "ZOOMTABCOVE"
            strSQL = CompilaQueryTabcove(strDescr, strDitta)
          Case "ZOOMTABDICV"
            strSQL = CompilaQueryTabdicv(strDescr, strDitta, strCodice)
          Case "ZOOMCHIPERS"
            strSQL = CompilaQueryChipers(strDitta, strDescr, strCodice, lCommessa)
          Case "ZOOMTASK"
            strSQL = CompilaQueryComwbs(strDitta, strDescr, lCommessa, strTipork)
          Case "ZOOMARTCLAS"
            strSQL = CompilaQueryArtclas(strDitta, strDescr, lCommessa, strTipork, strCodice)
          Case "ZOOMDISTINTE"
            strSQL = CompilaQueryDistinte(strDitta, strDescr, strTipork, strCodice, nEscomp, lCommessa)
          Case "ZOOMMODRICH"
            strSQL = CompilaQueryModrich(strDitta, strDescr)
          Case "ZOOMPARSTAG"
            bPrc = True
            strSQL = CompilaQueryStampeParametriche(strDitta, strDescr, strCodice)
          Case Else
            Throw (New NTSException(oApp.Tr(Me, 127791221378125000, "BS__HLTB -> Getdata: Zoom per |" & strNomeZoom & "| non trovato")))
        End Select
      End If

      '-----------------------------
      'chiedo i dati al database

      If bPrc Then
        dsHltb = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, strNomeZoom)
      Else
        dsHltb = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strNomeZoom)
      End If

      '-----------------------------
      'devo togliere i record con descrizione che inzia con il prefisso indicato nella opzione di regitro
      If oApp.oGvar.strZoomScartaDescrPrefix <> "" Then
        For Each dtrT As DataRow In dsHltb.Tables(strNomeZoom).Rows
          If NTSCStr(dtrT!tb_descr).StartsWith(oApp.oGvar.strZoomScartaDescrPrefix, StringComparison.CurrentCultureIgnoreCase) Then dtrT.Delete()
        Next
        dsHltb.AcceptChanges()
      End If

      '-----------------------------
      'se impostate delle esclusioni, devo aggiungere alla where quanto serve per non far vedere i codici da escludere
      If strNomeZoom.StartsWith("ZOOMTAB", StringComparison.CurrentCultureIgnoreCase) AndAlso bIgnoraEsclusioni = False Then
        strSQL = "SELECT TOP 1 * FROM eszoom " & _
                 " WHERE es_nome = '" & strNomeZoom.Substring(4) & "' " & _
                 "   AND (es_ditta = '*' OR es_ditta = " & CStrSQL(strDitta) & ") " & _
                 "   AND (es_opnome = '*' OR es_opnome = " & CStrSQL(oApp.User.Nome) & ") " & _
                 "   AND (es_ruolo = '*' OR es_ruolo = " & CStrSQL(oApp.User.Gruppo) & ") " & _
                 " ORDER BY es_ditta DESC, es_opnome DESC, es_ruolo DESC, es_escludi ASC"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          If NTSCStr(dttTmp.Rows(0)!es_cods).Trim <> "" Then
            For Each dtrT As DataRow In dsHltb.Tables(strNomeZoom).Select("NOT tb_codice " & IIf(dttTmp.Rows(0)!es_escludi.ToString = "N", " IN ", " NOT IN ").ToString & _
                                                                          "(" & NTSCStr(dttTmp.Rows(0)!es_cods) & ")")
              dtrT.Delete()
            Next
            dsHltb.AcceptChanges()
          End If
        End If
      End If    'If strTabNamees <> "" Then

      Return True

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

  Public Overridable Function GetDescrTabella(ByVal strTabella As String) As String
    Dim strOut As String = ""
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      strSQL = "SELECT cb_destab FROM tabelle WHERE cb_nomtab = " & CStrSQL(strTabella)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      If dttTmp.Rows.Count > 0 Then
        strOut = dttTmp.Rows(0)!cb_destab.ToString
        If strOut.IndexOf("-") > -1 Then
          strOut = strOut.Substring(strOut.IndexOf("-") + 1).Trim
        End If
      End If
      dttTmp.Clear()

      Return strOut

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryComuni(ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""
      strSQL = "SELECT co_codcomu as tb_codice, co_denom as tb_descr FROM comuni "
      If strDescr <> "" Then strSQL += " WHERE co_denom like " & CStrSQL(strDescr)
      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryAbi(ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""
      strSQL = "SELECT abiabi as tb_codice, abibanc as tb_descr, " & _
               " CASE WHEN abiabichk = 0 THEN -1 ELSE " & Color.Salmon.ToArgb & " END AS backcolor_row FROM abi "
      If strDescr <> "" Then strSQL += " WHERE abibanc like " & CStrSQL(strDescr)
      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryCfld(ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""
      strSQL = "SELECT trc_codice as tb_codice, trc_descr as tb_descr FROM trcfld "
      If strDescr <> "" Then strSQL += " WHERE trc_descr like " & CStrSQL(strDescr)
      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryAnagPc(ByVal strCodPdc As String, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""
      strSQL = "SELECT pc_conto as tb_codice, pc_descr1 as tb_descr" & _
                    " FROM anagpc " & _
                    " WHERE pc_codpcon = " & CStrSQL(strCodPdc)
      If strDescr <> "" Then
        strSQL = strSQL & " AND anagpc.pc_descr1 like " & CStrSQL(strDescr)
      End If
      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryTabanaz(ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""
      strSQL = "SELECT codditt as tb_codice, tb_azrags1 as tb_descr" & _
                    " FROM tabanaz "
      If strDescr <> "" Then
        strSQL = strSQL & " WHERE tabanaz.tb_azrags1 like " & CStrSQL(strDescr)
      End If
      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryDestdiv(ByVal strDitta As String, ByVal lContoCf as integer, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""
      If lContoCf = -1 Then
        'destidv anagrafica ditta
        strSQL = "SELECT ul_coddest as tb_codice, ul_nomdest as tb_descr" & _
                      " FROM anazul " & _
                      " WHERE codditt = " & CStrSQL(strDitta)
        If strDescr <> "" Then strSQL = strSQL & " AND anazul.ul_nomdest like " & CStrSQL(strDescr)
      Else
        'destiv anagra
        strSQL = "SELECT dd_coddest as tb_codice, dd_nomdest as tb_descr" & _
                      " FROM destdiv " & _
                      " WHERE codditt = " & CStrSQL(strDitta) & _
                      " AND dd_conto = " & lContoCf.ToString
        If strDescr <> "" Then strSQL = strSQL & " AND destdiv.dd_nomdest like " & CStrSQL(strDescr)
      End If

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryTabCauc(ByVal strDitta As String, ByVal strCodPdc As String, ByVal nEscomp As Integer, ByVal strDescr As String) As String
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim strOrder As String = ""
    Dim strTipoCont As String = ""
    Dim strCausNotAllow As String = ""
    Dim dsTmp As DataSet
    Dim strPcon As String = ""
    Try
      '------------------------------------------------------
      'determino se la ditta è professionista
      strSQL = "SELECT tb_azprofes, tb_azcodpcon, tb_escomp FROM tabanaz WHERE codditt = " & CStrSQL(strDitta)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")
      If dsTmp.Tables("TABANAZ").Rows.Count = 0 Then
        Throw (New NTSException(oApp.Tr(Me, 127791221378281250, "BD__HLTBCLD->CompilaQueryTabCauc: nell'anagrafica ditta non è stata trovata la ditta |" & strDitta & "|")))
      End If
      If dsTmp.Tables("TABANAZ").Rows(0)!tb_azprofes.ToString = "S" Then
        strTipoCont = "P"
      Else
        strTipoCont = "A"
      End If

      '------------------------------------------------------
      'le righe qui sotto servono se lo zoom è stato chiamato senza parametri specifici (ad esempio da zoom di stampe parametriche / query)
      strPcon = dsTmp.Tables("TABANAZ").Rows(0)!tb_azcodpcon.ToString
      If nEscomp = 0 Then nEscomp = NTSCInt(dsTmp.Tables("TABANAZ").Rows(0)!tb_escomp)

      '------------------------------------------------------
      'determino il tipo contabilità
      If nEscomp <> -1 Then
        strSQL = "SELECT tb_aztipcont FROM tabesco WHERE codditt = " & CStrSQL(strDitta) & " and tb_codesco = " & nEscomp
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABESCO")
        If dsTmp.Tables("TABESCO").Rows.Count = 0 Then
          Throw (New NTSException(oApp.Tr(Me, 127791221378437500, "BD__HLTBCLD->CompilaQueryTabCauc: nell'anagrafica della ditta |" & strDitta & "| non è stato trovato l'esercizio contabile |" & nEscomp.ToString & "|")))
        Else
          strTipoCont = strTipoCont & dsTmp.Tables("TABESCO").Rows(0)!tb_aztipcont.ToString
        End If
      End If

      '------------------------------------------------------
      'ottengo l'elenco delle causali non ammesse da opzione di registro
      '  strCausNotAllow = Mid(gstrValcampofiltro, InStr(5, gstrValcampofiltro, ";", vbTextCompare) + 9)
      'Trim(objStd.GetSettingBus("BSCGPRIN", "OPZIONI", ".", "CausaliNonAmmesse", "", ""))
      strCausNotAllow = GetSettingBus("BSCGPRIN", "OPZIONI", ".", "CausaliNonAmmesse", "", "", "").Trim

      strSQL = "SELECT tabcauc.tb_codcauc as tb_codice, tabcauc.tb_descauc as tb_descr " & _
              "FROM ((tabcauc LEFT JOIN tabcauc AS tabcauc1 ON tabcauc.tb_codcause = tabcauc1.tb_codcauc) " & _
              "LEFT JOIN tabcauc AS tabcauc2 ON tabcauc.tb_codcaupse = tabcauc2.tb_codcauc) " & _
              "LEFT JOIN tabcauc AS tabcauc3 ON tabcauc.tb_codcaupor = tabcauc3.tb_codcauc "

      strOrder = " ORDER BY tabcauc.tb_codcauc "

      '------------------------------------------------------
      'se non viene passato il piano dei conto
      If strCodPdc = "" Then
        'prendo il piano dei conti della ditta passatami in input
        strCodPdc = " = " & CStrSQL(strPcon)
      ElseIf strCodPdc = "*" Then
        strCodPdc = " like '%'"
      Else
        strCodPdc = " = '" & strCodPdc & "'"
    End If

      'tipo di contabilità
      Select Case strTipoCont
        Case "AO" 'ordinaria non professionista
          strWhere = " WHERE (((tabcauc.tb_codpcon = ' ' OR tabcauc.tb_codpcon " & strCodPdc & ") AND (tabcauc.tb_tipcont = '0' OR tabcauc.tb_tipcont = '1' OR tabcauc.tb_tipcont = '4' OR tabcauc.tb_tipcont = '7')) OR " & _
            "((tabcauc1.tb_codpcon = ' ' OR tabcauc1.tb_codpcon " & strCodPdc & ") AND (tabcauc1.tb_tipcont = '0' OR tabcauc1.tb_tipcont = '1' OR tabcauc1.tb_tipcont = '4' OR tabcauc1.tb_tipcont = '7')) OR " & _
            "((tabcauc2.tb_codpcon = ' ' OR tabcauc2.tb_codpcon " & strCodPdc & ") AND (tabcauc2.tb_tipcont = '0' OR tabcauc2.tb_tipcont = '1' OR tabcauc2.tb_tipcont = '4' OR tabcauc2.tb_tipcont = '7')) OR " & _
            "((tabcauc3.tb_codpcon = ' ' OR tabcauc3.tb_codpcon " & strCodPdc & ") AND (tabcauc3.tb_tipcont = '0' OR tabcauc3.tb_tipcont = '1' OR tabcauc3.tb_tipcont = '4' OR tabcauc3.tb_tipcont = '7')))"
        Case "AS" 'semplificata non professionista
          strWhere = " WHERE (((tabcauc.tb_codpcon = ' ' OR tabcauc.tb_codpcon " & strCodPdc & ") AND (tabcauc.tb_tipcont = '0' OR tabcauc.tb_tipcont = '2' OR tabcauc.tb_tipcont = '4' OR tabcauc.tb_tipcont = '8')) OR " & _
            "((tabcauc1.tb_codpcon = ' ' OR tabcauc1.tb_codpcon " & strCodPdc & ") AND (tabcauc1.tb_tipcont = '0' OR tabcauc1.tb_tipcont = '2' OR tabcauc1.tb_tipcont = '4' OR tabcauc1.tb_tipcont = '8')) OR " & _
            "((tabcauc2.tb_codpcon = ' ' OR tabcauc2.tb_codpcon " & strCodPdc & ") AND (tabcauc2.tb_tipcont = '0' OR tabcauc2.tb_tipcont = '2' OR tabcauc2.tb_tipcont = '4' OR tabcauc2.tb_tipcont = '8')) OR " & _
            "((tabcauc3.tb_codpcon = ' ' OR tabcauc3.tb_codpcon " & strCodPdc & ") AND (tabcauc3.tb_tipcont = '0' OR tabcauc3.tb_tipcont = '2' OR tabcauc3.tb_tipcont = '4' OR tabcauc3.tb_tipcont = '8')))"
        Case "AC" 'caso non possibile: professionista con cronologico ma non professionista
          strWhere = " WHERE ((tabcauc.tb_codpcon = ' ' OR tabcauc.tb_codpcon " & strCodPdc & ") AND tabcauc.tb_tipcont = '0')"
        Case "PO" 'ordinaria professionista
          strWhere = " WHERE (((tabcauc.tb_codpcon = ' ' OR tabcauc.tb_codpcon " & strCodPdc & ") AND (tabcauc.tb_tipcont = '0' OR tabcauc.tb_tipcont = '5' OR tabcauc.tb_tipcont = '6' OR tabcauc.tb_tipcont = '7')) OR " & _
            "((tabcauc1.tb_codpcon = ' ' OR tabcauc1.tb_codpcon " & strCodPdc & ") AND (tabcauc1.tb_tipcont = '0' OR tabcauc1.tb_tipcont = '5' OR tabcauc1.tb_tipcont = '6' OR tabcauc1.tb_tipcont = '7')) OR " & _
            "((tabcauc2.tb_codpcon = ' ' OR tabcauc2.tb_codpcon " & strCodPdc & ") AND (tabcauc2.tb_tipcont = '0' OR tabcauc2.tb_tipcont = '5' OR tabcauc2.tb_tipcont = '6' OR tabcauc2.tb_tipcont = '7')) OR " & _
            "((tabcauc3.tb_codpcon = ' ' OR tabcauc3.tb_codpcon " & strCodPdc & ") AND (tabcauc3.tb_tipcont = '0' OR tabcauc3.tb_tipcont = '5' OR tabcauc3.tb_tipcont = '6' OR tabcauc3.tb_tipcont = '7')))"
        Case "PS" 'semplificata professionista
          strWhere = " WHERE (((tabcauc.tb_codpcon = ' ' OR tabcauc.tb_codpcon " & strCodPdc & ") AND (tabcauc.tb_tipcont = '0' OR tabcauc.tb_tipcont = '3' OR tabcauc.tb_tipcont = '5' OR tabcauc.tb_tipcont = '8')) OR " & _
            "((tabcauc1.tb_codpcon = ' ' OR tabcauc1.tb_codpcon " & strCodPdc & ") AND (tabcauc1.tb_tipcont = '0' OR tabcauc1.tb_tipcont = '3' OR tabcauc1.tb_tipcont = '5' OR tabcauc1.tb_tipcont = '8')) OR " & _
            "((tabcauc2.tb_codpcon = ' ' OR tabcauc2.tb_codpcon " & strCodPdc & ") AND (tabcauc2.tb_tipcont = '0' OR tabcauc2.tb_tipcont = '3' OR tabcauc2.tb_tipcont = '5' OR tabcauc2.tb_tipcont = '8')) OR " & _
            "((tabcauc3.tb_codpcon = ' ' OR tabcauc3.tb_codpcon " & strCodPdc & ") AND (tabcauc3.tb_tipcont = '0' OR tabcauc3.tb_tipcont = '3' OR tabcauc3.tb_tipcont = '5' OR tabcauc3.tb_tipcont = '8')))"
        Case "PC" 'professionista con cronologico
          strWhere = " WHERE (((tabcauc.tb_codpcon = ' ' OR tabcauc.tb_codpcon " & strCodPdc & ") AND (tabcauc.tb_tipcont = '0' OR tabcauc.tb_tipcont = '5' OR tabcauc.tb_tipcont = '6' OR tabcauc.tb_tipcont = '7')) OR " & _
            "((tabcauc1.tb_codpcon = ' ' OR tabcauc1.tb_codpcon " & strCodPdc & ") AND (tabcauc1.tb_tipcont = '0' OR tabcauc1.tb_tipcont = '5' OR tabcauc1.tb_tipcont = '6' OR tabcauc1.tb_tipcont = '7')) OR " & _
            "((tabcauc2.tb_codpcon = ' ' OR tabcauc2.tb_codpcon " & strCodPdc & ") AND (tabcauc2.tb_tipcont = '0' OR tabcauc2.tb_tipcont = '5' OR tabcauc2.tb_tipcont = '6' OR tabcauc2.tb_tipcont = '7')) OR " & _
            "((tabcauc3.tb_codpcon = ' ' OR tabcauc3.tb_codpcon " & strCodPdc & ") AND (tabcauc3.tb_tipcont = '0' OR tabcauc3.tb_tipcont = '5' OR tabcauc3.tb_tipcont = '6' OR tabcauc3.tb_tipcont = '7')))"
        Case Else
          strWhere = " WHERE (((tabcauc.tb_codpcon = ' ' OR tabcauc.tb_codpcon " & strCodPdc & ") AND tabcauc.tb_tipcont like '%'))"
      End Select
      If strDescr <> "" Then
        'If strDescr.Substring(0, 1) <> "?" Then strDescr = "?" & strDescr
        strWhere = strWhere & " AND tabcauc.tb_descauc like " & CStrSQL(strDescr)
      End If
      'passato da prima nota: sono le causali non ammesse da opzione di registro BSCGPRIN/OPZIONI/CausaliNonAmmesse
      If strCausNotAllow <> "" Then strWhere = strWhere & " AND tabcauc.tb_codcauc not in (" & strCausNotAllow & ")"

      Return strSQL & strWhere & strOrder

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try

  End Function

  Overridable Function CompilaQuerySubCommessa(ByVal strDitta As String, ByVal lCommessa as integer, ByVal strDescr As String) As String
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT sco_subcommeca as tb_codice, sco_descr as tb_descr FROM subcomm" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND sco_commeca = " & lCommessa.ToString
      If strDescr.Trim <> "" Then strSQL += " AND sco_descr LIKE " & CStrSQL(strDescr)
      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try

  End Function

  Overridable Function CompilaQueryAnagca(ByVal strDitta As String, ByVal strDescr As String) As String
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT ac_conto as tb_codice, ac_descr1 as tb_descr FROM anagca" & _
                " WHERE codditt = " & CStrSQL(strDitta)
      If strDescr.Trim <> "" Then strSQL += " AND ac_descr1 LIKE " & CStrSQL(strDescr)
      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try

  End Function

  Overridable Function CompilaQueryAnalink(ByVal strDitta As String, ByVal lcontocg As Integer, ByVal strDescr As String) As String
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT anl_acconto as tb_codice, ac_descr1 as tb_descr FROM analink" & _
                " INNER JOIN anagca ON analink.codditt = anagca.codditt " & _
                " AND analink.anl_acconto = anagca.ac_conto" & _
                " WHERE analink.codditt = " & CStrSQL(strDitta) & _
                " AND anl_anconto = " & lcontocg.ToString
      If strDescr.Trim <> "" Then strSQL += " AND ac_descr1 LIKE " & CStrSQL(strDescr)
      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try

  End Function

  Overridable Function CompilaQueryBudgetDaContoca(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, _
                                                  ByVal lcontoca As Integer, ByVal strDescr As String) As String
    Dim strSQL As String = ""
    Try
      Select Case strTipork
        Case "E" 'centro
          strSQL = "SELECT bu_codcena as tb_codice, tb_descena as tb_descr " & _
                   " FROM budget INNER JOIN tabcena ON budget.codditt = tabcena.codditt AND budget.bu_codcena = tabcena.tb_codcena" & _
                   " WHERE budget.codditt = " & CStrSQL(strDitta) & " AND budget.bu_codcena > 0" & _
                  " AND budget.bu_conto = " & lcontoca.ToString & _
                   " AND budget.bu_escomp = " & nAnno.ToString
          If strDescr.Trim <> "" Then strSQL += " AND tb_descena LIKE " & CStrSQL(strDescr)
          strSQL += " GROUP BY bu_codcena, tb_descena "
        Case "L"  'linea
          strSQL = "SELECT bu_codcfam as tb_codice, tb_descfam as tb_descr " & _
                   " FROM budget INNER JOIN tabcfam ON budget.codditt = tabcfam.codditt AND budget.bu_codcfam = tabcfam.tb_codcfam" & _
                   " WHERE budget.codditt = " & CStrSQL(strDitta) & " AND budget.bu_codcfam <> ' '" & _
                   " AND budget.bu_conto = " & lcontoca.ToString & _
                   " AND budget.bu_escomp = " & nAnno.ToString
          If strDescr.Trim <> "" Then strSQL += " AND tb_descfam LIKE " & CStrSQL(strDescr)
          strSQL += " GROUP BY bu_codcfam, tb_descfam "
        Case "C"  'commessa
          strSQL = "SELECT bu_commeca as tb_codice, co_descr1 as tb_descr" & _
                   " FROM budget INNER JOIN commess ON budget.codditt = commess.codditt AND budget.bu_commeca = commess.co_comme" & _
                   " WHERE budget.codditt = " & CStrSQL(strDitta) & " AND budget.bu_commeca > 0" & _
                   " AND budget.bu_conto = " & lcontoca.ToString
          '" AND co_dtchiu = " & CDataSQL(IntSetDate("31/12/2099")) & _
          If strDescr.Trim <> "" Then strSQL += " AND co_descr1 LIKE " & CStrSQL(strDescr)
          strSQL += " GROUP BY bu_commeca, co_descr1 "
      End Select

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try

  End Function

  Overridable Function CompilaQueryBudgetSottocommessaDaContoca(ByVal strDitta As String, _
                       ByVal lContoca As Integer, ByVal lCommess As Integer, ByVal strDescr As String) As String
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT bu_subcommeca as tb_codice, sco_descr as tb_descr" & _
               " FROM budget INNER JOIN subcomm ON budget.codditt = subcomm.codditt AND budget.bu_commeca = subcomm.sco_commeca AND budget.bu_subcommeca = subcomm.sco_subcommeca" & _
               " WHERE budget.codditt = " & CStrSQL(strDitta) & " AND budget.bu_subcommeca <> ' '" & _
               " AND budget.bu_conto = " & lContoca & _
               " AND budget.bu_commeca = " & lCommess
      '" AND co_dtchiu = " & CDataSQL(IntSetDate("31/12/2099")) & _
      If strDescr.Trim <> "" Then strSQL += " AND sco_descr LIKE " & CStrSQL(strDescr)
      strSQL += " GROUP BY bu_subcommeca, sco_descr "

      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try

  End Function


  Overridable Function CompilaQueryTabatti(ByVal strDitta As String, ByVal nAnno As Integer, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""
      strSQL = "SELECT tb_codatti as tb_codice, tb_desatti as tb_descr" & _
                    " FROM tabatti " & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND tb_anno = " & nAnno.ToString
      If strDescr <> "" Then
        strSQL = strSQL & " AND tabatti.tb_desatti like " & CStrSQL(strDescr)
      End If
      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryTabsmel(ByVal strDitta As String, ByVal strTipoElenco As String, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""
      strSQL = "SELECT tb_numsmel as tb_codice, tb_dessmel as tb_descr" & _
                    " FROM tabsmel " & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND tb_codsmel = " & CStrSQL(strTipoElenco)
      If strDescr <> "" Then
        strSQL = strSQL & " AND tabsmel.tb_dessmel like " & CStrSQL(strDescr)
      End If
      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryTaric(ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""
      strSQL = "SELECT ta_codnomc as tb_codice, ta_desnomc as tb_descr FROM taric "
      If strDescr <> "" Then strSQL += " WHERE ta_desnomc like " & CStrSQL(strDescr)
      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Function CompilaQueryNomprop(ByVal strLiv1 As String, ByVal strLiv2 As String, _
                                                  ByVal strLiv3 As String, ByVal strDescr As String) As String
    Dim strSQL As String = ""
    Dim strWnLiv1 As String = strLiv1
    Dim strWnLiv2 As String = strLiv2
    Dim strWnLiv3 As String = strLiv3

    Try
    If strLiv1.PadRight(2).Substring(0, 2).ToLower = "bs" Then strWnLiv1 = strWnLiv1.Replace("_", "-")
      If strLiv2.PadRight(2).Substring(0, 2).ToLower = "bs" Then strWnLiv2 = strWnLiv2.Replace("_", "-")
      If strLiv3.PadRight(2).Substring(0, 2).ToLower = "bs" Then strWnLiv3 = strWnLiv3.Replace("_", "-")

      strSQL = "SELECT np_nomprop as tb_codice, CAST(np_descrprop AS varchar(1000)) + '  (Default ' + isnull(np_valdef, '') + ' )' as tb_descr FROM nomprop " & _
              " WHERE (np_liv1 = '" & strLiv1 & "' or np_liv1 = '" & strWnLiv1 & "')" & _
              " and (np_liv2 = '" & strLiv2 & "' or np_liv2 = '" & strWnLiv2 & "')" & _
              " and (np_liv3 = '" & strLiv3 & "' or np_liv3 = '" & strWnLiv3 & "')"
      'se zoom sul Rep non visualizzo alcune proprietà poco utilizzate
      If strLiv3 = "Rep" Or strWnLiv3 = "Rep" Then strSQL += " and np_nomprop not in ('Color','FontItalic','FontName','FontSize','FontStruckout','FontUnderlined','FontWeight','Margx1','Margx2','Margy1','Margy2','SetMargins','Yresolution','PrintQuality','PaperLength','PaperWidth')"

      If strDescr <> "" Then
        strSQL += " AND (np_descrprop like " & CStrSQL(strDescr) & _
                  "   OR np_nomprop like " & CStrSQL(strDescr) & ")"
      End If
      strSQL += " ORDER BY np_nomprop"

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Function CompilaQueryValprop(ByVal strLiv1 As String, ByVal strLiv2 As String, _
                                                  ByVal strLiv3 As String, ByVal strNomprop As String) As String
    Dim strSQL As String = ""
    Dim strWnLiv1 As String = strLiv1
    Dim strWnLiv2 As String = strLiv2
    Dim strWnLiv3 As String = strLiv3

    Try
    If strLiv1.PadRight(2).Substring(0, 2).ToLower = "bs" Then strWnLiv1 = strWnLiv1.Replace("_", "-")
      If strLiv2.PadRight(2).Substring(0, 2).ToLower = "bs" Then strWnLiv2 = strWnLiv2.Replace("_", "-")
      If strLiv3.PadRight(2).Substring(0, 2).ToLower = "bs" Then strWnLiv3 = strWnLiv3.Replace("_", "-")

      strSQL = "SELECT vp_valore as tb_codice, vp_descrval as tb_descr FROM valprop " & _
              " WHERE (vp_liv1 = '" & strLiv1 & "' or vp_liv1 = '" & strWnLiv1 & "')" & _
              " and (vp_liv2 = '" & strLiv2 & "' or vp_liv2 = '" & strWnLiv2 & "')" & _
              " and (vp_liv3 = '" & strLiv3 & "' or vp_liv3 = '" & strWnLiv3 & "')" & _
              " and vp_nomprop = '" & strNomprop & "' ORDER BY vp_nomprop"

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Function CompilaQueryLeads(ByVal strDitta As String, ByVal strDescr As String, ByVal bSoloLeads As Boolean) As String
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT le_codlead as tb_codice, le_descr1 + ' - ' + isnull(le_citta, '') + ' - ' + isnull(le_indir, '') as tb_descr FROM leads " & _
               " WHERE codditt = " & CStrSQL(strDitta)
      If strDescr.Trim <> "" Then strSQL += " AND le_descr1 LIKE " & CStrSQL(strDescr)
      If bSoloLeads Then strSQL += " AND le_conto = 0"

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryArtfasi(ByVal strDitta As String, ByVal strCodart As String, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""

      strSQL = "SELECT af_fase as tb_codice, af_descr as tb_descr" & _
                    " FROM artfasi " & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND af_codart = " & CStrSQL(strCodart)
      If strDescr <> "" Then strSQL = strSQL & " AND af_descr like " & CStrSQL(strDescr)

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryTablingp(ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""

      '----------------------------------------------------------
      'ZOOM TABELLA STANDARD: COMPONGO LA SELECT
      strSQL = "SELECT tb_codling as tb_codice, " & _
               " tb_desling as tb_descr, * " & _
               " FROM tabling"

      '----------------------------------------------------------
      'COMPONGO LA WHERE STANDARD
      If strDescr <> "" Then
        strSQL &= " WHERE tb_desling LIKE " & CStrSQL(strDescr)
      End If

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryDistbas(ByVal strDitta As String, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""

    strSQL = "SELECT distbas.db_coddb as tb_codice, artico.ar_descr as tb_descr FROM DISTBAS " & _
        "INNER JOIN ARTICO ON (DISTBAS.codditt = ARTICO.codditt) AND (DISTBAS.db_coddb = ARTICO.ar_codart)" & _
        " WHERE distbas.codditt = " & CStrSQL(strDitta)
      If strDescr <> "" Then strSQL = strSQL & " AND ar_descr like " & CStrSQL(strDescr)
    strSQL = strSQL & " order by distbas.db_coddb"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryRuoli(ByVal strDitta As String, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""

      strSQL = "SELECT ruoli.ru_codruol as tb_codice, ruoli.ru_desruol as tb_descr FROM ruoli"
      If strDescr <> "" Then strSQL = strSQL & " WHERE ru_desruol like " & CStrSQL(strDescr)
      strSQL = strSQL & " order by ruoli.ru_codruol"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryOperat(ByVal strDitta As String, ByVal strDescr As String) As String
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT opnome AS tb_codice," & _
        " LTRIM(RTRIM((CASE WHEN OpDescont IS NOT NULL  THEN OpDescont ELSE '' END" & _
        " + ' ' +" & _
        " CASE WHEN OpDescont2 IS NOT NULL THEN OpDescont2 ELSE '' END))) AS tb_descr" & _
        " FROM OPERAT"
      If strDescr <> "" Then
        strSQL += " WHERE (opnome LIKE " & CStrSQL(strDescr) & _
          " OR LTRIM(RTRIM((CASE WHEN OpDescont IS NOT NULL  THEN OpDescont ELSE '' END" & _
          " + ' ' +" & _
          " CASE WHEN OpDescont2 IS NOT NULL THEN OpDescont2 ELSE '' END))) LIKE " & CStrSQL(strDescr) & ")"
      End If
      strSQL += " ORDER BY opnome"
      '--------------------------------------------------------------------------------------------------------------
      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryRuoliOperat(ByVal strDitta As String, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""

      strSQL = "SELECT operat.opgruppo as tb_codice, operat.Opruolo as tb_descr FROM operat"
      If strDescr <> "" Then strSQL = strSQL & " WHERE Opruolo like " & CStrSQL(strDescr)
      strSQL = strSQL & " group by operat.opgruppo, Opruolo" & _
                        " order by operat.opgruppo, Opruolo"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryAziende(ByVal strDitta As String, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""

      strSQL = "SELECT aziende.azcodaz as tb_codice, aziende.azdescr as tb_descr FROM aziende"
      If strDescr <> "" Then strSQL = strSQL & " WHERE azdescr like " & CStrSQL(strDescr)
      strSQL = strSQL & " order by aziende.azcodaz"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryTipiAlert(ByVal strDitta As String, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""

      strSQL = "SELECT alesets.als_id as tb_codice, alesets.als_descale as tb_descr FROM alesets"
      If strDescr <> "" Then strSQL = strSQL & " WHERE als_descale like " & CStrSQL(strDescr)
      strSQL = strSQL & " order by alesets.als_id"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryTabproc(ByVal strDitta As String, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""

      strSQL = "SELECT pr_codice as tb_codice, pr_nome as tb_descr FROM proced"
      If strDescr <> "" Then strSQL = strSQL & " WHERE pr_nome like " & CStrSQL(strDescr)
      strSQL = strSQL & " order by pr_codice"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryValvari(ByVal strDitta As String, ByVal strCodPdc As String, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""
      'valvari
      strSQL = "SELECT vv_valvari as tb_codice, vv_desvvar as tb_descr" & _
                    " FROM valvari " & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND vv_codvari = " & CStrSQL(strCodPdc)
      If strDescr <> "" Then strSQL = strSQL & " AND valvari.vv_desvvar like " & CStrSQL(strDescr)

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryValvariEsplose(ByVal strDitta As String, ByVal strCodPdc As String, ByVal nVar As Integer, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""
      'artvar - artico
      Select Case nVar

        Case 1
          strSQL = " SELECT arv_codvar as tb_codice, min(arv_descr) as tb_descr" & _
                   " FROM artvar INNER JOIN artico ON artvar.codditt = artico.codditt AND artvar.arv_codroot = artico.ar_codroot AND artvar.arv_codvar = artico.ar_codvar1" & _
                   " WHERE artvar.codditt = " & CStrSQL(strDitta) & _
                   " AND arv_codroot = " & CStrSQL(strCodPdc) & _
                   " AND arv_livello = 1 " & _
                   IIf(strDescr.Trim <> "", " AND arv_descr like " & CStrSQL(strDescr), "").ToString & _
                   " GROUP BY arv_codvar"
        Case 2
          strSQL = " SELECT arv_codvar as tb_codice, min(arv_descr) as tb_descr" & _
                   " FROM artvar INNER JOIN artico ON artvar.codditt = artico.codditt AND artvar.arv_codroot = artico.ar_codroot AND artvar.arv_codvar = artico.ar_codvar2" & _
                   " WHERE artvar.codditt = " & CStrSQL(strDitta) & _
                   " AND arv_codroot = " & CStrSQL(strCodPdc) & _
                   " AND arv_livello = 2 " & _
                   IIf(strDescr.Trim <> "", " AND arv_descr like " & CStrSQL(strDescr), "").ToString & _
                   " GROUP BY arv_codvar"
        Case 3
          strSQL = " SELECT arv_codvar as tb_codice, min(arv_descr) as tb_descr" & _
                   " FROM artvar INNER JOIN artico ON artvar.codditt = artico.codditt AND artvar.arv_codroot = artico.ar_codroot AND artvar.arv_codvar = artico.ar_codvar3" & _
                   " WHERE artvar.codditt = " & CStrSQL(strDitta) & _
                   " AND arv_codroot = " & CStrSQL(strCodPdc) & _
                   " AND arv_livello = 3 " & _
                   IIf(strDescr.Trim <> "", " AND arv_descr like " & CStrSQL(strDescr), "").ToString & _
                   " GROUP BY arv_codvar"
      End Select

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryTabtrib(ByVal nSezio As Integer, ByVal nSotsez As Integer) As String
    Try
      Dim strSQL As String = ""
      strSQL = "SELECT tb_codtrib As tb_codice, tb_destrib As tb_descr FROM tabtrib"
      If (nSezio <> 0) Or (nSotsez <> 0) Then
        strSQL += " WHERE" & IIf(nSezio <> 0, " tb_sezio = " & nSezio, "").ToString
        If nSotsez <> 0 Then strSQL += IIf(nSezio <> 0, " AND ", "").ToString & " tb_sotsez = " & nSotsez
      End If
    Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryTabelle(ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""

      strSQL = "SELECT cb_nomtab as tb_codice, cb_destab as tb_descr FROM tabelle"
      If strDescr <> "" Then strSQL += " WHERE cb_nomtab like " & CStrSQL(strDescr)
      strSQL += " ORDER BY cb_nomtab"
      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryTabcove(ByVal strDescr As String, ByVal strDitta As String) As String
    Try
      Dim strSQL As String = ""

      strSQL = "SELECT tb_codcove as tb_codice, (substring(isnull(tb_descove, '') + space(50), 1, 50) + " & _
                                               "substring('(' + isnull(tb_descovg, '') + ')' + space(50), 1, 50) + " & _
                                               "CASE WHEN tb_concova <> 0 THEN '(CA:' + isnull(tb_descova, '') + ')' ELSE '' END) as tb_descr " & _
               " FROM (tabcove LEFT JOIN tabcovg ON tabcove.tb_codcove = tabcovg.tb_codcovg) " & _
               " WHERE codditt = " & CStrSQL(strDitta)
      If strDescr <> "" Then strSQL += " AND substring(isnull(tb_descove, '') + space(50), 1, 50) + " & _
                                             "substring('(' + isnull(tb_descovg, '') + ')' + space(50), 1, 50) + " & _
                                             "CASE WHEN tb_concova <> 0 THEN '(CA:' + isnull(tb_descova, '') + ')' ELSE '' END like " & CStrSQL(strDescr)
      strSQL += " ORDER BY tb_codcove"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryTabdicv(ByVal strDescr As String, ByVal strDitta As String, _
  ByVal strCodice As String) As String
    Dim nLiv As Integer = 1
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------ù
      strSQL = "SELECT tb_liv FROM tabdica" & _
        " WHERE tb_coddica = " & CStrSQL(strCodice)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then nLiv = NTSCInt(dttTmp.Rows(0)!tb_liv)
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tb_coddicv as tb_codice, tb_desdicv as tb_descr FROM tabdicv" & _
        " WHERE tb_coddica = " & CStrSQL(strCodice)
      If nLiv = 1 Then
        strSQL += " AND codditt = ' '"
      Else
        strSQL += " AND codditt = " & CStrSQL(strDitta)
      End If
      If strDescr <> "" Then
        strSQL += " AND tb_desdicv LIKE " & CStrSQL(strDescr)
      End If
      strSQL += " ORDER BY codditt, tb_coddica, tb_coddicv"
      '--------------------------------------------------------------------------------------------------------------
      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Overridable Function CompilaQueryChipers(ByVal strDitta As String, ByVal strDescr As String, _
                                           ByVal strCodPdc As String, ByVal lCommessa As Integer) As String
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------ù
      If lCommessa = 1 Then
        strSQL = "SELECT cp_valchip as tb_codice, cp_desvalchip as tb_descr FROM chiperp"
      Else
        strSQL = "SELECT cp_valchia as tb_codice, cp_desvalchia as tb_descr FROM chipers"
      End If
      strSQL += " WHERE codditt = " & CStrSQL(strDitta)
      If lCommessa = 1 Then
        strSQL += " AND cp_codchip = " & CStrSQL(strCodPdc)
      Else
        strSQL += " AND cp_codchia = " & CStrSQL(strCodPdc)
      End If
      If strDescr <> "" Then
        If lCommessa = 1 Then
          strSQL = strSQL & " AND cp_desvalchip like " & CStrSQL(strDescr)
        Else
          strSQL = strSQL & " AND cp_desvalchia like " & CStrSQL(strDescr)
        End If
      End If
      strSQL += " ORDER BY codditt, tb_codice, tb_descr"
      '--------------------------------------------------------------------------------------------------------------

      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryComwbs(ByVal strDitta As String, ByVal strDescr As String, _
                                        ByVal lCommessa As Integer, ByVal strSubcommeca As String) As String
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------Ã¹
      strSQL = "SELECT cwb_idwbs as tb_codice, cwb_descr as tb_descr FROM comwbs"

      strSQL += " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND cwb_commeca = " & lCommessa.ToString & _
                " AND cwb_subcommeca = " & CStrSQL(strSubcommeca) & _
                " AND cwb_summary = 'N' " & _
                " AND cwb_idwbs <> 0"

      If strDescr <> "" Then
        strSQL = strSQL & " AND cwb_descr like " & CStrSQL(strDescr)
      End If
      strSQL += " ORDER BY codditt, tb_codice, tb_descr"
      '--------------------------------------------------------------------------------------------------------------

      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function

  Public Overridable Function CompilaQueryArtclas(ByVal strDitta As String, ByVal strDescr As String, _
                                        ByVal lCommessa As Integer, ByVal strSubcommeca As String, _
                                        ByVal strLivello As String) As String
    Dim strSQL As String = ""

    Try

      Select Case strLivello
        Case "1...."
          strSQL = "SELECT '1....' as tb_codice, acl_codcla1 + '| | | | ' as  tb_codice1, acl_descla1 as tb_descr FROM artclas1" & _
               " WHERE codditt = " & CStrSQL(strDitta)
          If strDescr <> "" Then strSQL += " AND artclas1.acl_descla1 like " & CStrSQL(strDescr)
          strSQL += " ORDER BY tb_descr, tb_codice, tb_codice1"
        Case ".2..."
          strSQL += "SELECT '.2...' as tb_codice, artclas1.acl_codcla1 + '|' + artclas2.acl_codcla2 + '| | | ' as tb_codice1, " & _
                    " artclas1.acl_descla1 + ' / ' + artclas2.acl_descla2 as tb_descr " & _
                    " FROM artclas2 INNER JOIN artclas1 ON artclas2.codditt = artclas1.codditt AND artclas2.acl_codcla1 = artclas1.acl_codcla1" & _
                    " WHERE artclas2.codditt = " & CStrSQL(strDitta)
          If strDescr <> "" Then strSQL += " AND (artclas1.acl_descla1 like " & CStrSQL(strDescr) & " OR artclas2.acl_descla2 like " & CStrSQL(strDescr) & ")"
          strSQL += " ORDER BY tb_descr, tb_codice, tb_codice1"
        Case "..3.."
          strSQL += "SELECT '..3..' as tb_codice, artclas1.acl_codcla1 + '|' + artclas2.acl_codcla2 + '|' + artclas3.acl_codcla3 + '| | ' as tb_codice1, " & _
                    " artclas1.acl_descla1 + ' / ' + artclas2.acl_descla2 + ' / ' + artclas3.acl_descla3 as tb_descr " & _
                    " FROM artclas2 INNER JOIN artclas1 ON artclas2.codditt = artclas1.codditt AND artclas2.acl_codcla1 = artclas1.acl_codcla1" & _
                    " INNER JOIN artclas3 ON artclas2.codditt = artclas3.codditt AND artclas2.acl_codcla2 = artclas3.acl_codcla2 AND artclas2.acl_codcla1 = artclas3.acl_codcla1" & _
                    " WHERE artclas3.codditt = " & CStrSQL(strDitta)
          If strDescr <> "" Then strSQL += " AND (artclas1.acl_descla1 like " & CStrSQL(strDescr) & " OR artclas2.acl_descla2 like " & CStrSQL(strDescr) & " OR artclas3.acl_descla3 like " & CStrSQL(strDescr) & ")"
          strSQL += " ORDER BY tb_descr, tb_codice, tb_codice1"
        Case "...4."
          strSQL += "SELECT '...4.' as tb_codice, artclas1.acl_codcla1 + '|' + artclas2.acl_codcla2 + '|' + artclas3.acl_codcla3 + '|' + artclas4.acl_codcla4 + '| '  as tb_codice1, " & _
                    " artclas1.acl_descla1 + ' / ' + artclas2.acl_descla2 + ' / ' + artclas3.acl_descla3 + ' / ' + artclas4.acl_descla4 as tb_descr " & _
                    " FROM artclas2 INNER JOIN artclas1 ON artclas2.codditt = artclas1.codditt AND artclas2.acl_codcla1 = artclas1.acl_codcla1" & _
                    " INNER JOIN artclas3 ON artclas2.codditt = artclas3.codditt AND artclas2.acl_codcla2 = artclas3.acl_codcla2 AND artclas2.acl_codcla1 = artclas3.acl_codcla1" & _
                    " INNER JOIN artclas4 ON artclas3.codditt = artclas4.codditt AND artclas4.acl_codcla3 = artclas3.acl_codcla3 AND artclas4.acl_codcla2 = artclas3.acl_codcla2 AND artclas4.acl_codcla1 = artclas3.acl_codcla1" & _
                    " WHERE artclas4.codditt = " & CStrSQL(strDitta)
          If strDescr <> "" Then strSQL += " AND (artclas1.acl_descla1 like " & CStrSQL(strDescr) & " OR artclas2.acl_descla2 like " & CStrSQL(strDescr) & " OR artclas3.acl_descla3 like " & CStrSQL(strDescr) & " OR artclas4.acl_descla4 like " & CStrSQL(strDescr) & ")"
          strSQL += " ORDER BY tb_descr, tb_codice, tb_codice1"
        Case "....5"
          strSQL += "SELECT '....5' as tb_codice, artclas1.acl_codcla1 + '|' + artclas2.acl_codcla2 + '|' + artclas3.acl_codcla3 + '|' + artclas4.acl_codcla4 + '|' + artclas5.acl_codcla5 as tb_codice1, " & _
                   " artclas1.acl_descla1 + ' / ' + artclas2.acl_descla2 + ' / ' + artclas3.acl_descla3 + ' / ' + artclas4.acl_descla4 + ' / ' + artclas5.acl_descla5 as tb_descr " & _
                   " FROM artclas2 INNER JOIN artclas1 ON artclas2.codditt = artclas1.codditt AND artclas2.acl_codcla1 = artclas1.acl_codcla1" & _
                   " INNER JOIN artclas3 ON artclas2.codditt = artclas3.codditt AND artclas2.acl_codcla2 = artclas3.acl_codcla2 AND artclas2.acl_codcla1 = artclas3.acl_codcla1" & _
                   " INNER JOIN artclas4 ON artclas3.codditt = artclas4.codditt AND artclas4.acl_codcla3 = artclas3.acl_codcla3 AND artclas4.acl_codcla2 = artclas3.acl_codcla2 AND artclas4.acl_codcla1 = artclas3.acl_codcla1" & _
                   " INNER JOIN artclas5 ON artclas5.codditt = artclas4.codditt AND artclas4.acl_codcla4 = artclas5.acl_codcla4 AND artclas4.acl_codcla3 = artclas5.acl_codcla3 AND artclas4.acl_codcla2 = artclas5.acl_codcla2 AND artclas4.acl_codcla1 = artclas5.acl_codcla1" & _
                   " WHERE artclas5.codditt = " & CStrSQL(strDitta)
          If strDescr <> "" Then strSQL += " AND (artclas1.acl_descla1 like " & CStrSQL(strDescr) & " OR artclas2.acl_descla2 like " & CStrSQL(strDescr) & " OR artclas3.acl_descla3 like " & CStrSQL(strDescr) & " OR artclas4.acl_descla4 like " & CStrSQL(strDescr) & " OR artclas5.acl_descla5 like " & CStrSQL(strDescr) & ")"
          strSQL += " ORDER BY tb_descr, tb_codice, tb_codice1"
        Case Else
          strSQL = "SELECT '1....' as tb_codice, acl_codcla1 + '| | | | ' as  tb_codice1, acl_descla1 as tb_descr FROM artclas1" & _
                   " WHERE codditt = " & CStrSQL(strDitta)
          If strDescr <> "" Then strSQL += " AND artclas1.acl_descla1 like " & CStrSQL(strDescr)

          strSQL += vbCrLf & " UNION ALL " & vbCrLf

          strSQL += "SELECT '.2...' as tb_codice, artclas1.acl_codcla1 + '|' + artclas2.acl_codcla2 + '| | | ' as tb_codice1, " & _
                    " artclas1.acl_descla1 + ' / ' + artclas2.acl_descla2 as tb_descr " & _
                    " FROM artclas2 INNER JOIN artclas1 ON artclas2.codditt = artclas1.codditt AND artclas2.acl_codcla1 = artclas1.acl_codcla1" & _
                    " WHERE artclas2.codditt = " & CStrSQL(strDitta)
          If strDescr <> "" Then strSQL += " AND (artclas1.acl_descla1 like " & CStrSQL(strDescr) & " OR artclas2.acl_descla2 like " & CStrSQL(strDescr) & ")"

          strSQL += vbCrLf & " UNION ALL " & vbCrLf

          strSQL += "SELECT '..3..' as tb_codice, artclas1.acl_codcla1 + '|' + artclas2.acl_codcla2 + '|' + artclas3.acl_codcla3 + '| | ' as tb_codice1, " & _
                    " artclas1.acl_descla1 + ' / ' + artclas2.acl_descla2 + ' / ' + artclas3.acl_descla3 as tb_descr " & _
                    " FROM artclas2 INNER JOIN artclas1 ON artclas2.codditt = artclas1.codditt AND artclas2.acl_codcla1 = artclas1.acl_codcla1" & _
                    " INNER JOIN artclas3 ON artclas2.codditt = artclas3.codditt AND artclas2.acl_codcla2 = artclas3.acl_codcla2 AND artclas2.acl_codcla1 = artclas3.acl_codcla1" & _
                    " WHERE artclas3.codditt = " & CStrSQL(strDitta)
          If strDescr <> "" Then strSQL += " AND (artclas1.acl_descla1 like " & CStrSQL(strDescr) & " OR artclas2.acl_descla2 like " & CStrSQL(strDescr) & " OR artclas3.acl_descla3 like " & CStrSQL(strDescr) & ")"

          strSQL += vbCrLf & " UNION ALL " & vbCrLf

          strSQL += "SELECT '...4.' as tb_codice, artclas1.acl_codcla1 + '|' + artclas2.acl_codcla2 + '|' + artclas3.acl_codcla3 + '|' + artclas4.acl_codcla4 + '| '  as tb_codice1, " & _
                    " artclas1.acl_descla1 + ' / ' + artclas2.acl_descla2 + ' / ' + artclas3.acl_descla3 + ' / ' + artclas4.acl_descla4 as tb_descr " & _
                    " FROM artclas2 INNER JOIN artclas1 ON artclas2.codditt = artclas1.codditt AND artclas2.acl_codcla1 = artclas1.acl_codcla1" & _
                    " INNER JOIN artclas3 ON artclas2.codditt = artclas3.codditt AND artclas2.acl_codcla2 = artclas3.acl_codcla2 AND artclas2.acl_codcla1 = artclas3.acl_codcla1" & _
                    " INNER JOIN artclas4 ON artclas3.codditt = artclas4.codditt AND artclas4.acl_codcla3 = artclas3.acl_codcla3 AND artclas4.acl_codcla2 = artclas3.acl_codcla2 AND artclas4.acl_codcla1 = artclas3.acl_codcla1" & _
                    " WHERE artclas4.codditt = " & CStrSQL(strDitta)
          If strDescr <> "" Then strSQL += " AND (artclas1.acl_descla1 like " & CStrSQL(strDescr) & " OR artclas2.acl_descla2 like " & CStrSQL(strDescr) & " OR artclas3.acl_descla3 like " & CStrSQL(strDescr) & " OR artclas4.acl_descla4 like " & CStrSQL(strDescr) & ")"

          strSQL += vbCrLf & " UNION ALL " & vbCrLf

          strSQL += "SELECT '....5' as tb_codice, artclas1.acl_codcla1 + '|' + artclas2.acl_codcla2 + '|' + artclas3.acl_codcla3 + '|' + artclas4.acl_codcla4 + '|' + artclas5.acl_codcla5 as tb_codice1, " & _
                    " artclas1.acl_descla1 + ' / ' + artclas2.acl_descla2 + ' / ' + artclas3.acl_descla3 + ' / ' + artclas4.acl_descla4 + ' / ' + artclas5.acl_descla5 as tb_descr " & _
                    " FROM artclas2 INNER JOIN artclas1 ON artclas2.codditt = artclas1.codditt AND artclas2.acl_codcla1 = artclas1.acl_codcla1" & _
                    " INNER JOIN artclas3 ON artclas2.codditt = artclas3.codditt AND artclas2.acl_codcla2 = artclas3.acl_codcla2 AND artclas2.acl_codcla1 = artclas3.acl_codcla1" & _
                    " INNER JOIN artclas4 ON artclas3.codditt = artclas4.codditt AND artclas4.acl_codcla3 = artclas3.acl_codcla3 AND artclas4.acl_codcla2 = artclas3.acl_codcla2 AND artclas4.acl_codcla1 = artclas3.acl_codcla1" & _
                    " INNER JOIN artclas5 ON artclas5.codditt = artclas4.codditt AND artclas4.acl_codcla4 = artclas5.acl_codcla4 AND artclas4.acl_codcla3 = artclas5.acl_codcla3 AND artclas4.acl_codcla2 = artclas5.acl_codcla2 AND artclas4.acl_codcla1 = artclas5.acl_codcla1" & _
                    " WHERE artclas5.codditt = " & CStrSQL(strDitta)
          If strDescr <> "" Then strSQL += " AND (artclas1.acl_descla1 like " & CStrSQL(strDescr) & " OR artclas2.acl_descla2 like " & CStrSQL(strDescr) & " OR artclas3.acl_descla3 like " & CStrSQL(strDescr) & " OR artclas4.acl_descla4 like " & CStrSQL(strDescr) & " OR artclas5.acl_descla5 like " & CStrSQL(strDescr) & ")"
          strSQL += " ORDER BY tb_descr, tb_codice, tb_codice1"
      End Select

      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryDistinte(ByVal strDitta As String, ByVal strDescr As String, _
                                            ByVal strTippaga As String, ByVal strPnint As String, _
                                            ByVal nAnndist As Integer, ByVal nSaldato As Integer) As String
    Try
      Dim strSQL As String = ""

      strSQL = "SELECT DISTINCT sc_numdist as tb_codice, tb_desbanc as tb_descr " & _
               " FROM scaden LEFT JOIN tabbanc ON scaden.codditt = tabbanc.codditt AND scaden.sc_codbanc = tabbanc.tb_codbanc" & _
               " WHERE scaden.codditt = " & CStrSQL(strDitta) & _
               " AND sc_integr = " & CStrSQL(strPnint) & _
               " And sc_tippaga = " & CStrSQL(strTippaga) & _
               " AND sc_anndist = " & nAnndist.ToString

      If nSaldato = 1 Then strSQL += " AND sc_flsaldato = 'S'"
      If nSaldato = 2 Then strSQL += " AND sc_flsaldato = 'N'"
      If strDescr <> "" Then strSQL += " AND tb_desbanc like " & CStrSQL(strDescr)
      strSQL += " ORDER BY sc_numdist"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Overridable Function CompilaQueryModrich(ByVal strDitta As String, ByVal strDescr As String) As String
    Try
      Dim strSQL As String = ""

      strSQL = "SELECT DISTINCT nnm_codrich as tb_codice, nnm_desrich as tb_descr " & _
               " FROM nnmodrich" & _
               " WHERE codditt = " & CStrSQL(strDitta) 
      If strDescr <> "" Then strSQL += " AND nnm_desrich like " & CStrSQL(strDescr)
      strSQL += " ORDER BY nnm_codrich"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Function CompilaQueryStampeParametriche(ByVal strDitta As String, ByVal strDescr As String, _
                                                             ByVal strElencoCodici As String) As String
    Dim strSQL As String = ""
    Try

      strSQL = " SELECT Pz_Codform As tb_codice, Pz_Titstam as tb_descr " & _
               " FROM PARSTAG " & _
               " WHERE 0=0 "

      If strDescr <> "" Then strSQL += " AND Pz_Titstam LIKE " & CStrSQL(strDescr)

      If strElencoCodici <> "" Then
        strSQL = strSQL & " AND Pz_Codform IN (" & strElencoCodici & ")"
      End If

      strSQL = strSQL & " ORDER BY Pz_Codform "

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

End Class
