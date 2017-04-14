Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDVEFADI
  Inherits CLD__BASE

  Public strSelectFadi As String = "SELECT testmag.*, an_descr1 as xx_conto, 'S' as xx_seleziona, 'N' as xx_rielab" & _
                                   " FROM testmag INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto "

  Public Overridable Function CalcolaWhere(ByVal strDitta As String, ByVal strWhereTestmag As String, _
                                        ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                        ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                        ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String) As String
    Dim strSQL As String = ""
    Try
      If nWhereLista <> 0 Then
        'se la lista è negativa devo prendere tutti i clienti eccetto quelli della lista selezionata
        strSQL += " AND (tm_conto " & IIf(nWhereLista < 0, " NOT ", "").ToString & " IN (SELECT lse_conto FROM listsel" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND lse_codlsel = " & Math.Abs(nWhereLista).ToString & "))"
      End If

      If strWhereSerieDa.ToString = " " And strWhereSerieA.ToString = "Z" And lWhereNumdocDa = 0 And lWhereNumdocA = 999999999 Then
        'dati standard: non li applico
      Else
        If strWhereSerieDa.ToUpper = strWhereSerieA.ToUpper Then
          strSQL += " AND (testmag.tm_numdoc BETWEEN " & lWhereNumdocDa & _
                    " AND " & lWhereNumdocA & ") AND testmag.tm_serie = " & CStrSQL(strWhereSerieDa.ToUpper)
        Else
          strSQL += " AND ((testmag.tm_serie = " & CStrSQL(strWhereSerieDa.ToUpper) & _
                            " AND testmag.tm_numdoc >= " & lWhereNumdocDa & ") OR (testmag.tm_serie = " & CStrSQL(strWhereSerieA.ToUpper) & _
                            " AND testmag.tm_numdoc <= " & lWhereNumdocA & ") OR (testmag.tm_serie > " & CStrSQL(strWhereSerieDa.ToUpper) & _
                            " AND testmag.tm_serie < " & CStrSQL(strWhereSerieA.ToUpper) & "))"
        End If
      End If

      'filtri standard su testmag
      MyBase.TraduciWhere(strWhereTestmag, strSQL)

      'filtri standard su anagra
      MyBase.TraduciWhere(strWhereAnagra, strSQL)

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaDoc(ByVal strDitta As String, ByVal strWhereTestmag As String, _
                                      ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                      ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                      ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                      ByRef dsOut As DataSet, ByVal bProva As Boolean) As Boolean
    Try
      'obsoleta
      Return CaricaDoc(strDitta, strWhereTestmag, strWhereAnagra, nWhereLista, _
                       lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                       dsOut, bProva, "T")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaDoc(ByVal strDitta As String, ByVal strWhereTestmag As String, _
                                        ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                        ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                        ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                        ByRef dsOut As DataSet, ByVal bProva As Boolean, ByVal strContab As String) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strWhereTestmag, strWhereAnagra, nWhereLista, _
                                             lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                                             dsOut, bProva, strContab})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsOut = CType(oIn(8), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      strSQL = strSelectFadi & _
               " WHERE testmag.codditt = " & CStrSQL(strDitta)
      If bProva Then
        strSQL += " AND tm_numdoc < 0 "
      Else
        strSQL += CalcolaWhere(strDitta, strWhereTestmag, strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA)
      End If
      If strContab <> "T" Then
        'filtro documenti contabilizzati S(si), N(no), t(tutti)
        strSQL += " AND tm_flcont = " & CStrSQL(strContab)
      End If
      strSQL += " ORDER BY tm_tipork, tm_anno, tm_serie, tm_numdoc "

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "FADI")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CaricaDocFileNuovoRielab(ByVal strDitta As String, ByVal strTipork As String, _
                                                      ByVal nAnno As Integer, ByVal strSerie As String, _
                                                      ByVal strNumdocSplit As String, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      strSQL = strSelectFadi & _
               " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
               " AND tm_tipork = " & CStrSQL(strTipork) & _
               " AND tm_anno = " & nAnno.ToString & _
               " AND tm_serie = " & CStrSQL(strSerie) & _
               " AND ("
      strT = strNumdocSplit.Split(","c)
      For i = 0 To strT.Length - 1
        If i > 0 Then strSQL += " OR "
        strSQL += " tm_numdoc = " & strT(i).ToString
      Next
      strSQL += ")"
      strSQL += " ORDER BY tm_tipork, tm_anno, tm_serie, tm_numdoc "

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "FADI")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetRighePrezziZeroNewDoc(ByVal strDitta As String, ByVal strWhereTestmag As String, _
                                        ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                        ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                        ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                        ByVal strCheckRighe As String, ByRef dttOut As DataTable) As Boolean
    '--------------------------
    'in fase di creazione nuovi documenti ritorna le righe con prezzi = 0
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga, mm_codart, mm_descr" & _
              " FROM ((((" & strJoinTestmagMovmag & ")" & _
              " INNER JOIN anagra ON anagra.codditt = testmag.codditt AND anagra.an_conto = testmag.tm_conto)" & _
              " INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart) " & _
              " LEFT JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga)" & _
              " LEFT JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf" & _
              " WHERE testmag.codditt = " & CStrSQL(strDitta)
      strSQL += CalcolaWhere(strDitta, strWhereTestmag, strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA)

      strSQL += " AND movmag.mm_prezzo = 0" & _
                " AND movmag.mm_stasino <> 'N' AND movmag.mm_stasino <> 'B' " & _
                " AND movmag.mm_flkit NOT IN ('A', 'T')"
      Select Case strCheckRighe
        Case "1"   '--- Su tutte le righe tranne articolo 'D'
          strSQL = strSQL & " AND movmag.mm_codart <> 'D'"
        Case "2"   '--- Su tutte le righe tranne articoli non a magazzino
          strSQL = strSQL & " AND artico.ar_stainv <> 'N'"
        Case "3"   '--- Su tutte le righe con quantità diversa da zero
          strSQL = strSQL & " AND movmag.mm_quant <> 0"
      End Select

      strSQL += " ORDER BY tm_tipork, tm_anno, tm_serie, tm_numdoc, mm_riga "

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetDDTDaElab(ByVal strDitta As String, ByVal strWhereTestmag As String, _
                                    ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                    ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                    ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                    ByVal strCheckRighe As String, ByRef dttOut As DataTable) As Boolean
    'ottengo l'elenco dei ddt da riepilogare
    Dim strSQL As String = ""
    Dim strCampiRotturaPers As String = ""   'campi personalizzati di testmag da utilizzare come elementi di rottura tra più fatture
    Try
      strCampiRotturaPers = GetSettingBus("Bsvefadi", "Opzioni", ".", "CampiRotturaPers", "", " ", "").Trim 'campi personalizzati per spezzare tra fatture (separati da ',', esempio 'tm_codstag, tm_listino')

      strSQL = "SELECT 'N' as xx_seleziona, testmag.*, anagra.an_descr1 as xx_conto, tb_despaga as xx_codpaga, " & _
              " tb_destpbf as xx_codtpbf, tb_desbanc as xx_codbanc, tabcage.tb_descage as xx_codagen, " & _
              " tabcage2.tb_descage as xx_codagen2, tb_desciva as xx_codese, anagraf.an_descr1 as xx_contfatt, " & _
              " dd_nomdest as xx_coddest, tm_cup, tm_cig, tm_riferimpa, tm_dianno, tm_dinumero "
      If strCampiRotturaPers.Trim <> "" Then strSQL += ", " & strCampiRotturaPers
      strSQL += " FROM testmag INNER JOIN testmagc ON testmag.codditt = testmagc.codditt AND testmag.tm_tipork = testmagc.tm_tipork AND testmag.tm_anno = testmagc.tm_anno AND testmag.tm_serie = testmagc.tm_serie AND testmag.tm_numdoc = testmagc.tm_numdoc" & _
              " INNER JOIN anagra ON anagra.codditt = testmag.codditt AND anagra.an_conto = testmag.tm_conto " & _
              " LEFT JOIN destdiv ON destdiv.codditt = testmag.codditt AND destdiv.dd_conto = testmag.tm_conto AND destdiv.dd_coddest = testmag.tm_coddest " & _
              " LEFT JOIN anagra as anagraf ON anagraf.codditt = testmag.codditt AND anagraf.an_conto = testmag.tm_contfatt " & _
              " LEFT JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga " & _
              " LEFT JOIN tabciva ON testmag.tm_codese = tabciva.tb_codciva " & _
              " LEFT JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf" & _
              " LEFT JOIN tabcage ON testmag.codditt = tabcage.codditt AND testmag.tm_codagen = tabcage.tb_codcage" & _
              " LEFT JOIN tabcage as tabcage2 ON testmag.codditt = tabcage2.codditt AND testmag.tm_codagen2 = tabcage2.tb_codcage" & _
              " LEFT JOIN tabbanc ON testmag.codditt = tabbanc.codditt AND testmag.tm_codbanc = tabbanc.tb_codbanc" & _
              " WHERE testmag.codditt = " & CStrSQL(strDitta)
      strSQL += CalcolaWhere(strDitta, strWhereTestmag.Replace("an_", "anagra.an_"), _
        strWhereAnagra.Replace("an_", "anagra.an_"), nWhereLista, lWhereNumdocDa, lWhereNumdocA, _
        strWhereSerieDa, strWhereSerieA)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetTestateNewDoc(ByVal strDitta As String, ByVal bUsaContoFatt As Boolean, _
                                               ByVal strUsaContoFattDoc As String, ByVal bIgnoraTipobf As Boolean, _
                                               ByVal strTipoFatturazione As String, ByVal bOrdinaPerNumdoc As Boolean, _
                                               ByVal strWhereTestmag As String, ByVal strWhereAnagra As String, _
                                               ByVal nWhereLista As Integer, _
                                               ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                               ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                               ByRef dttOut As DataTable) As Boolean

    Try
      'obsoleta
      Return GetTestateNewDoc(strDitta, bUsaContoFatt, strUsaContoFattDoc, bIgnoraTipobf, _
                              strTipoFatturazione, bOrdinaPerNumdoc, strWhereTestmag, strWhereAnagra, _
                              nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                              dttOut, False, "")

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetTestateNewDoc(ByVal strDitta As String, ByVal bUsaContoFatt As Boolean, _
                                              ByVal strUsaContoFattDoc As String, ByVal bIgnoraTipobf As Boolean, _
                                              ByVal strTipoFatturazione As String, ByVal bOrdinaPerNumdoc As Boolean, _
                                              ByVal strWhereTestmag As String, ByVal strWhereAnagra As String, _
                                              ByVal nWhereLista As Integer, _
                                              ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                              ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                              ByRef dttOut As DataTable, _
                                              ByVal bForzaDataDiversa As Boolean, ByVal strDataDiversa As String) As Boolean
    Dim strSQL As String = ""
    Dim strSQLConto As String = ""
    Dim strCampiRotturaPers As String = ""   'campi personalizzati di testmag da utilizzare come elementi di rottura tra più fatture
    Try
      strCampiRotturaPers = GetSettingBus("Bsvefadi", "Opzioni", ".", "CampiRotturaPers", "", " ", "").Trim 'campi personalizzati per spezzare tra fatture (separati da ',', esempio 'tm_codstag, tm_listino')

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bUsaContoFatt, strUsaContoFattDoc, bIgnoraTipobf, _
                              strTipoFatturazione, bOrdinaPerNumdoc, strWhereTestmag, strWhereAnagra, _
                              nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                              dttOut, bForzaDataDiversa, strDataDiversa})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(142), DataTable)
        Return CBool(oOut)
      End If
      '----------------


      '------------------------------------------------------
      'se serve aggiorno la data primo pagamento in base alle impostazioni di form
      If bForzaDataDiversa Then
        strSQL = "UPDATE testmag SET tm_datapag = " & CDataSQL(strDataDiversa) & _
                 " FROM testmag INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto " & _
                 " INNER JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga " & _
                 " LEFT JOIN tabtpbf ON testmag.tm_tipobf = tabtpbf.tb_codtpbf AND testmag.codditt = tabtpbf.codditt" & _
                 " WHERE tabpaga.tb_ggcalend IN ('A', 'B') " & _
                 " AND tabpaga.tb_decpaga = 4 " & _
                 CalcolaWhere(strDitta, strWhereTestmag, strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      '----------------------
      'Conto fatturazione
      'Se l'opzione di registro BSVEFADI/OPZIONI/UsaContoFattDoc è impostata, ha priorità
      'sull'eventuale BSVEFADI/OPZIONI/UsaContoFatt...
      If strUsaContoFattDoc = "-1" Or strUsaContoFattDoc = "2" Then
        strSQLConto = "CASE WHEN tm_contfatt = 0 THEN tm_conto ELSE tm_contfatt END As xx_conto," & _
                      " CASE WHEN tm_contfatt = 0 THEN 'T' ELSE 'A' END AS xx_tipoconto"
      Else
        If bUsaContoFatt Then
          strSQLConto = "CASE WHEN an_contfatt = 0 THEN tm_conto ELSE an_contfatt END As xx_conto, " & _
                        " CASE WHEN an_contfatt = 0 THEN 'T' ELSE 'A' END AS xx_tipoconto"
        Else
          strSQLConto = "tm_conto As xx_conto, 'T' As xx_tipoconto"
        End If
      End If

      '----------------------
      'Seleziona da testmag le fatture da generare
      strSQL = "SELECT DISTINCT " & strSQLConto & ", tm_codagen, tm_codagen2, " & _
               IIf(bIgnoraTipobf, "0 as xx_tipobf, ", "tm_tipobf as xx_tipobf, ").ToString & _
               " tm_codpaga, tm_codpaga2, tm_codese, tm_scont1, tm_scont2, tm_scopag, tm_valuta, tm_cambio, " & _
               "CASE WHEN tm_scorpo='S' THEN 'S' ELSE 'N' END AS xx_scorpo, " & _
               "CASE WHEN tb_decpaga<>3 and tb_decpaga<>4 THEN Null ELSE tm_datapag END AS xx_datapag, " & _
               "CASE WHEN tm_flspinc='S' THEN 'S' ELSE 'N' END AS xx_flspinc, " & _
               "CASE WHEN tm_flbolli='S' THEN 'S' ELSE 'N' END AS xx_flbolli, "
      strSQL = strSQL & "tm_abi, tm_cab, ' ' AS xx_tipork, 0 AS xx_anno, ' ' AS xx_serie, 0 AS xx_numdoc, -1 as xx_coddest, " & _
               " tm_codbanc As xx_codbanc, " & IIf(strUsaContoFattDoc = "1", "tm_contfatt", "0").ToString & " as xx_contfatt, " & _
               " tm_cup, tm_cig, tm_riferimpa, tm_dianno, tm_dinumero "
      If strCampiRotturaPers.Trim <> "" Then strSQL += ", " & strCampiRotturaPers
      strSQL += " FROM (((anagra INNER JOIN testmag ON anagra.an_conto = testmag.tm_conto AND anagra.codditt = testmag.codditt) INNER JOIN TESTMAGC ON testmag.codditt = testmagc.codditt AND testmag.tm_tipork = testmagc.tm_tipork AND testmag.tm_anno = testmagc.tm_anno AND testmag.tm_serie = testmagc.tm_serie AND testmag.tm_numdoc = testmagc.tm_numdoc) " & _
              " LEFT JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga) " & _
              " LEFT JOIN tabtpbf ON testmag.tm_tipobf = tabtpbf.tb_codtpbf AND testmag.codditt = tabtpbf.codditt" & _
              " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
      CalcolaWhere(strDitta, strWhereTestmag, strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA) & _
      " AND an_fatt <> 'B' AND an_fatt <> 'D' "
      strSQL = strSQL & "UNION ALL "
      strSQL = strSQL & "SELECT " & strSQLConto & ", tm_codagen, tm_codagen2, " & _
               IIf(bIgnoraTipobf, "0 as xx_tipobf, ", "tm_tipobf as xx_tipobf, ").ToString & _
               " tm_codpaga, tm_codpaga2, tm_codese, tm_scont1, tm_scont2, tm_scopag, tm_valuta, tm_cambio, " & _
               "tm_scorpo AS xx_scorpo, " & _
               "CASE WHEN tb_decpaga<>3 and tb_decpaga<>4 THEN Null ELSE tm_datapag END AS xx_datapag, " & _
               "tm_flspinc AS xx_flspinc, " & _
               "tm_flbolli AS xx_flbolli, "
      strSQL = strSQL & "tm_abi, tm_cab, testmag.tm_tipork AS xx_tipork, testmag.tm_anno AS xx_anno, testmag.tm_serie AS xx_serie, " & _
               " testmag.tm_numdoc AS xx_numdoc, -1 as xx_coddest, tm_codbanc As xx_codbanc, " & _
               IIf(strUsaContoFattDoc = "1", "tm_contfatt", "0").ToString & " as xx_contfatt, " & _
               " tm_cup, tm_cig, tm_riferimpa, tm_dianno, tm_dinumero "
      If strCampiRotturaPers.Trim <> "" Then strSQL += ", " & strCampiRotturaPers
      strSQL += " FROM (((anagra INNER JOIN testmag ON anagra.an_conto = testmag.tm_conto AND anagra.codditt = testmag.codditt) INNER JOIN TESTMAGC ON testmag.codditt = testmagc.codditt AND testmag.tm_tipork = testmagc.tm_tipork AND testmag.tm_anno = testmagc.tm_anno AND testmag.tm_serie = testmagc.tm_serie AND testmag.tm_numdoc = testmagc.tm_numdoc) " & _
               " LEFT JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga) " & _
               " LEFT JOIN tabtpbf ON testmag.tm_tipobf = tabtpbf.tb_codtpbf AND testmag.codditt = tabtpbf.codditt" & _
               " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
               CalcolaWhere(strDitta, strWhereTestmag, strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA) & _
               " AND an_fatt = " & CStrSQL(strTipoFatturazione) & " "
      '-----
      'Gestione destinazione diversa
      'NB: x i conti con fatturazione x destinazione
      'ignora il conto di fatturazione!!!
      strSQL = strSQL & "UNION ALL "
      strSQL = strSQL & "SELECT DISTINCT tm_conto AS xx_conto, 'T' As xx_tipoconto, tm_codagen, tm_codagen2, " & _
               IIf(bIgnoraTipobf, "0 as xx_tipobf, ", "tm_tipobf as xx_tipobf, ").ToString & _
               " tm_codpaga, tm_codpaga2, tm_codese, tm_scont1, tm_scont2, tm_scopag, tm_valuta, tm_cambio, " & _
               "CASE WHEN tm_scorpo='S' THEN 'S' ELSE 'N' END AS xx_scorpo, " & _
               "CASE WHEN tb_decpaga<>3 and tb_decpaga<>4 THEN Null ELSE tm_datapag END AS xx_datapag, " & _
               "CASE WHEN tm_flspinc='S' THEN 'S' ELSE 'N' END AS xx_flspinc, " & _
               "CASE WHEN tm_flbolli='S' THEN 'S' ELSE 'N' END AS xx_flbolli, "
      strSQL = strSQL & "tm_abi, tm_cab, ' ' AS xx_tipork, 0 AS xx_anno, ' ' AS xx_serie, 0 AS xx_numdoc, tm_coddest as xx_coddest," & _
               " tm_codbanc As xx_codbanc, " & IIf(strUsaContoFattDoc = "1", "tm_contfatt", "0").ToString & " as xx_contfatt, " & _
               " tm_cup, tm_cig, tm_riferimpa, tm_dianno, tm_dinumero "
      If strCampiRotturaPers.Trim <> "" Then strSQL += ", " & strCampiRotturaPers
      strSQL += " FROM (((anagra INNER JOIN testmag ON anagra.an_conto = testmag.tm_conto AND anagra.codditt = testmag.codditt) INNER JOIN TESTMAGC ON testmag.codditt = testmagc.codditt AND testmag.tm_tipork = testmagc.tm_tipork AND testmag.tm_anno = testmagc.tm_anno AND testmag.tm_serie = testmagc.tm_serie AND testmag.tm_numdoc = testmagc.tm_numdoc) " & _
               " LEFT JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga) " & _
               " LEFT JOIN tabtpbf ON testmag.tm_tipobf = tabtpbf.tb_codtpbf AND testmag.codditt = tabtpbf.codditt" & _
               " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
               CalcolaWhere(strDitta, strWhereTestmag, strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA) & _
               " AND an_fatt = 'D' "

      If bOrdinaPerNumdoc Then
        strSQL = strSQL & "ORDER BY xx_anno, xx_serie, xx_numdoc, xx_conto "
      Else
        strSQL = strSQL & "ORDER BY xx_conto, xx_anno, xx_serie, xx_numdoc "
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTestateDDTfattura(ByVal strDitta As String, ByRef dtrFiltri As DataRow, _
                                                   ByVal strUsaContoFattDoc As String, ByVal bIgnoraTipobf As Boolean, _
                                                   ByVal strWhereTestmag As String, ByVal strWhereAnagra As String, _
                                                   ByVal nWhereLista As Integer, _
                                                   ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                                   ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                                   ByRef dttDDT As DataTable) As Boolean
    '-----------------------------------
    'ottengo l'elenco dei DDT da inserire nella fattura che sto creando
    Dim strSQL As String = ""
    Dim strCampiRotturaPers As String = ""   'campi personalizzati di testmag da utilizzare come elementi di rottura tra più fatture
    Try
      strCampiRotturaPers = GetSettingBus("Bsvefadi", "Opzioni", ".", "CampiRotturaPers", "", " ", "").Trim 'campi personalizzati per spezzare tra fatture (separati da ',', esempio 'tm_codstag, tm_listino')
      strSQL = " SELECT TESTMAG.* " & _
               " FROM ((testmag INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto) INNER JOIN testmagc ON testmag.codditt = testmagc.codditt AND testmag.tm_tipork = testmagc.tm_tipork AND testmag.tm_anno = testmagc.tm_anno AND testmag.tm_serie = testmagc.tm_serie AND testmag.tm_numdoc = testmagc.tm_numdoc)" & _
               " INNER JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf "
      If dtrFiltri!xx_tipork.ToString <> " " Then
        ' fattura singola bolla
        strSQL += " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                 " AND tm_flfatt <> 'S'" & _
                 " AND testmag.tm_tipork = " & CStrSQL(dtrFiltri!xx_tipork) & _
                 " AND testmag.tm_anno = " & dtrFiltri!xx_anno.ToString & _
                 " AND testmag.tm_serie = " & CStrSQL(dtrFiltri!xx_serie) & _
                 " AND testmag.tm_numdoc = " & dtrFiltri!xx_numdoc.ToString
        strSQL += CalcolaWhere(strDitta, strWhereTestmag, strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA)
      Else
        ' fatt. con riepilogo bolle
        strSQL += " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                 " AND tm_codagen = " & NTSCInt(dtrFiltri!tm_codagen).ToString & _
                 " AND tm_codagen2 = " & NTSCInt(dtrFiltri!tm_codagen2).ToString & _
                 " AND tm_codpaga = " & NTSCInt(dtrFiltri!tm_codpaga).ToString & _
                 " AND tm_codpaga2 = " & NTSCInt(dtrFiltri!tm_codpaga2).ToString & _
                 " AND tm_codese = " & NTSCInt(dtrFiltri!tm_codese).ToString & _
                 " AND tm_scont1 = " & CDblSQL(NTSCDec(dtrFiltri!tm_scont1)) & _
                 " AND tm_scont2 = " & CDblSQL(NTSCDec(dtrFiltri!tm_scont2)) & _
                 " AND tm_scopag = " & CDblSQL(NTSCDec(dtrFiltri!tm_scopag)) & _
                 " AND tm_valuta = " & NTSCInt(dtrFiltri!tm_valuta).ToString & _
                 " AND tm_abi = " & NTSCInt(dtrFiltri!tm_abi).ToString & _
                 " AND tm_cab = " & NTSCInt(dtrFiltri!tm_cab).ToString & _
                 " AND tm_cambio = " & CDblSQL(NTSCDec(dtrFiltri!tm_cambio)) & _
                 " AND tm_codbanc = " & NTSCInt(dtrFiltri!xx_codbanc).ToString & _
                 " AND isnull(tm_cup, '') = " & CStrSQL(NTSCStr(dtrFiltri!tm_cup)) & _
                 " AND isnull(tm_cig, '') = " & CStrSQL(NTSCStr(dtrFiltri!tm_cig)) & _
                 " AND isnull(tm_riferimpa, '') = " & CStrSQL(NTSCStr(dtrFiltri!tm_riferimpa)) & _
                 " AND tm_dianno = " & NTSCInt(dtrFiltri!tm_dianno).ToString & _
                 " AND tm_dinumero = " & NTSCInt(dtrFiltri!tm_dinumero).ToString
        If strCampiRotturaPers.Trim <> "" Then
          For Each strT As String In strCampiRotturaPers.Split(","c)
            strT = strT.Trim
            If strT.Trim <> "" And dtrFiltri.Table.Columns.Contains(strT) Then
              Select Case dtrFiltri.Table.Columns(strT).DataType.ToString
                Case "System.String"
                  If dtrFiltri(strT).Equals(DBNull.Value) Then
                    strSQL += " AND " & strT & " is null"
                  Else
                    strSQL += " AND " & strT & " = " & CStrSQL(dtrFiltri(strT).ToString)
                  End If
                Case "System.DateTime"
                  If dtrFiltri(strT).Equals(DBNull.Value) Then
                    strSQL += " AND " & strT & " is null"
                  Else
                    strSQL += " AND " & strT & " = " & CDataSQL(dtrFiltri(strT).ToString)
                  End If
                Case Else
                  If dtrFiltri(strT).Equals(DBNull.Value) Then
                    strSQL += " AND " & strT & " is null"
                  Else
                    strSQL += " AND " & strT & " = " & CDblSQL(dtrFiltri(strT).ToString)
                  End If
              End Select
            End If
          Next
        End If
        strSQL += CalcolaWhere(strDitta, strWhereTestmag, strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA)

        If dtrFiltri!xx_tipoconto.ToString = "T" Then
          strSQL = strSQL & " AND tm_conto = " & NTSCInt(dtrFiltri!xx_conto).ToString
        Else
          If Not (strUsaContoFattDoc = "-1" Or strUsaContoFattDoc = "2") Then
            strSQL = strSQL & " AND an_contfatt = " & NTSCInt(dtrFiltri!xx_conto).ToString
          Else
            strSQL = strSQL & " AND tm_contfatt = " & NTSCInt(dtrFiltri!xx_conto).ToString
          End If
        End If

        If NTSCInt(dtrFiltri!xx_tipobf) <> 0 Then
          strSQL = strSQL & " AND tm_tipobf = " & NTSCInt(dtrFiltri!xx_tipobf).ToString
        End If

        'Flag scorporo
        If dtrFiltri!xx_scorpo.ToString = "S" Then
          strSQL = strSQL & " AND tm_scorpo = 'S'"
        Else
          strSQL = strSQL & " AND tm_scorpo <> 'S'"
        End If
        'Data 1° pagamento
        If NTSCStr(dtrFiltri!xx_datapag).Trim <> "" Then
          strSQL = strSQL & " AND tm_datapag = " & CDataSQL(NTSCDate(dtrFiltri!xx_datapag))
        End If
        'Flag spese incasso
        If dtrFiltri!xx_flspinc.ToString = "S" Then
          strSQL = strSQL & " AND tm_flspinc = 'S'"
        Else
          strSQL = strSQL & " AND tm_flspinc <> 'S'"
        End If
        'Flag spese bolli
        If dtrFiltri!xx_flbolli.ToString = "S" Then
          strSQL = strSQL & " AND tm_flbolli = 'S'"
        Else
          strSQL = strSQL & " AND tm_flbolli <> 'S'"
        End If
        'Destinazione diversa
        If NTSCInt(dtrFiltri!xx_coddest) <> -1 Then
          strSQL = strSQL & " AND tm_coddest = " & NTSCInt(dtrFiltri!xx_coddest).ToString
        End If
        'conto fatturazione con valore = 1: devo spezzare a parità di conto fatturazione
        If strUsaContoFattDoc = "1" Then
          strSQL = strSQL & " AND tm_contfatt = " & NTSCInt(dtrFiltri!xx_contfatt).ToString
        End If
      End If    'If dtrFiltri!xx_tipork.ToString = " " Then

      strSQL += " ORDER BY tm_datdoc, testmag.tm_tipork, testmag.tm_serie, testmag.tm_numdoc "
      dttDDT = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetQueryStampaPDF(ByVal strDitta As String, ByRef dttFadi As DataTable, ByRef strQueryGetDocUnico As String) As String
    Dim strSQL As String = ""
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim lNumMin As Integer = 0
    Dim lNumMax As Integer = 0
    Dim strSerieMin As String = ""
    Dim strSerieMax As String = ""

    Try
      lNumMin = NTSCInt(dttFadi.Select("xx_seleziona = 'S'", "tm_numdoc ASC")(0)!tm_numdoc)
      lNumMax = NTSCInt(dttFadi.Select("xx_seleziona = 'S'", "tm_numdoc DESC")(0)!tm_numdoc)
      strSerieMin = NTSCStr(dttFadi.Select("xx_seleziona = 'S'", "tm_serie ASC")(0)!tm_serie)
      strSerieMax = NTSCStr(dttFadi.Select("xx_seleziona = 'S'", "tm_serie DESC")(0)!tm_serie)

      'Compone la stringa SQL: la prima è la query per fare un pdf per ogni documento
      strTmp = " SELECT tm_tipork as tipork, tm_anno as anno, tm_serie as serie, tm_numdoc as numero, " & _
               " tm_conto as conto, tm_valuta as valuta, tm_scorpo as scorpo, tm_codagen as agente, " & _
               " tm_coddest as destin "

      strSQL = " FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tm_tipork = " & CStrSQL(dttFadi.Rows(0)!tm_tipork) & _
               " AND tm_anno = " & dttFadi.Rows(0)!tm_anno.ToString & _
               " AND tm_serie >= " & CStrSQL(strSerieMin) & _
               " AND tm_serie <= " & CStrSQL(strSerieMax) & _
               " AND tm_numdoc >= " & lNumMin.ToString & _
               " AND tm_numdoc <= " & lNumMax.ToString & _
               " AND ("
      dtrT = dttFadi.Select("xx_seleziona = 'S'")
      For i = 0 To dtrT.Length - 1
        If i > 0 Then strSQL += " OR "
        strSQL += " tm_numdoc = " & dtrT(i)!tm_numdoc.ToString
      Next
      strSQL += ")"

      'questa serve per fare un pdf unico (in realtà i pdf potrebbero essere anche più di uno, 
      'ma uno unico per i documenti in valuta, uno unico per quelli con scorporo, uno unico per i restanti
      strQueryGetDocUnico = " SELECT tm_tipork as tipork, 0 as anno, ' ' as serie, 0 as numero, " & _
                           "0 as conto, tm_valuta as valuta, tm_scorpo as scorpo, 0 as agente, " & _
                           " 0 as destin " & _
                           strSQL & _
                           " GROUP BY tm_tipork, tm_valuta, tm_scorpo " & _
                           " ORDER BY tm_tipork, tm_valuta, tm_scorpo "

      strSQL += " GROUP BY tm_valuta, tm_scorpo, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_conto, tm_codagen, tm_coddest " & _
                " ORDER BY tm_valuta, tm_scorpo, tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_coddest "

      Return strTmp + strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTestmagMovmagPerFileConad(ByVal strDitta As String, ByRef dttFadi As DataTable, ByVal bEscludiArtD As Boolean, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim lNumMin As Integer = 0
    Dim lNumMax As Integer = 0
    Dim i As Integer = 0
    Try
      lNumMin = NTSCInt(dttFadi.Select("xx_seleziona = 'S'", "tm_numdoc ASC")(0)!tm_numdoc)
      lNumMax = NTSCInt(dttFadi.Select("xx_seleziona = 'S'", "tm_numdoc DESC")(0)!tm_numdoc)


      strSQL = "SELECT testmag.tm_tipork, testmag.tm_serie, testmag.tm_numdoc, testmag.tm_datdoc, testmag_1.tm_serie AS AlfBol, testmag_1.tm_numdoc AS NumBol, testmag_1.tm_datdoc AS DatBol, testmag_1.tm_coddest, movmag.mm_codart, movmag.mm_descr, movmag.mm_quant, movmag.mm_valore, movmag.mm_codiva, movmag.mm_stasino, movmag.mm_unmis, anagra.an_note, anagra.an_codnscol, tabciva.tb_tipiva, codarfo.caf_codarfo, testmag.tm_valuta, testmag.tm_scont1, testmag.tm_scont2, testmag.tm_scopag, mm_prezvalc, mm_perqta, mm_scont1, mm_scont2, mm_scont3, mm_scont4, mm_scont5, mm_scont6, tb_aliq " & _
           "FROM ((anagra INNER JOIN (((testmag AS testmag_1 INNER JOIN testmag ON (testmag_1.codditt = testmag.codditt) AND (testmag_1.tm_numfat = testmag.tm_numdoc) AND (testmag_1.tm_alffat = testmag.tm_serie) AND (testmag_1.tm_annfat = testmag.tm_anno) AND (testmag_1.tm_tiporkfat = testmag.tm_tipork)) LEFT JOIN destdiv ON (testmag_1.codditt = destdiv.codditt) AND (testmag_1.tm_coddest = destdiv.dd_coddest) AND (testmag_1.tm_conto = destdiv.dd_conto)) " & _
           "INNER JOIN (movmag LEFT JOIN tabciva ON movmag.mm_codiva = tabciva.tb_codciva) ON (testmag_1.codditt = movmag.codditt) AND (testmag_1.tm_numdoc = movmag.mm_numdoc) AND (testmag_1.tm_serie = movmag.mm_serie) AND (testmag_1.tm_anno = movmag.mm_anno) AND (testmag_1.tm_tipork = movmag.mm_tipork)) ON anagra.codditt = testmag_1.codditt AND anagra.an_conto = testmag_1.tm_conto) INNER JOIN keymag ON (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) AND " & _
           "(movmag.mm_numdoc = keymag.km_numdoc) AND (movmag.mm_serie = keymag.km_serie) AND (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_tipork = keymag.km_tipork)) LEFT JOIN codarfo ON (keymag.codditt = codarfo.codditt) AND (keymag.km_codart = codarfo.caf_codart) AND (keymag.km_conto = codarfo.caf_conto)  " & _
           "WHERE testmag.codditt = " & CStrSQL(strDitta) & _
           " AND testmag.tm_tipork= " & CStrSQL(dttFadi.Rows(0)!tm_tipork) & _
           " AND testmag.tm_anno = " & dttFadi.Rows(0)!tm_anno.ToString & _
           " AND testmag.tm_serie = " & CStrSQL(dttFadi.Rows(0)!tm_serie) & _
           " AND testmag.tm_numdoc >= " & lNumMin.ToString & _
           " AND testmag.tm_numdoc <= " & lNumMax.ToString & _
           " AND movmag.mm_stasino <> 'N' AND movmag.mm_stasino <> 'B'" & _
           " AND ("
      dtrT = dttFadi.Select("xx_seleziona = 'S'")
      For i = 0 To dtrT.Length - 1
        If i > 0 Then strSQL += " OR "
        strSQL += " testmag.tm_numdoc = " & dtrT(i)!tm_numdoc.ToString
      Next
      strSQL += ")"
      If bEscludiArtD Then strSQL += " AND movmag.mm_codart <> 'D'"
      strSQL += " AND movmag.mm_magaz = keymag.km_magaz " & _
                " ORDER BY testmag.tm_tipork, testmag.tm_datdoc, testmag_1.tm_serie, testmag_1.tm_numdoc, movmag.mm_riga"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetWhereHltm(ByVal strTipork As String, ByVal nAnno As Integer, _
                                           ByVal strSerie As String, ByVal lNumdoc As Integer) As String
    Dim strSQL As String = ""
    Try
      strSQL = " tm_tiporkfat = " & CStrSQL(strTipork) & _
              " AND tm_annfat = " & nAnno.ToString & _
              " AND tm_alffat = " & CStrSQL(strSerie) & _
              " AND tm_numfat = " & lNumdoc.ToString
      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsInTestmag(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, _
                                            ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT tm_tipork FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tm_anno = " & nAnno.ToString & " AND tm_serie = " & CStrSQL(strSerie) & _
               " AND tm_numdoc = " & lNumdoc.ToString & _
               " AND tm_tipork = " & CStrSQL(strTipork)
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

  Public Overridable Function IsInTestmagFatt(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, _
                                              ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strWhere As String = ""
    Try
      Select Case strTipork
        Case "D", "£"
          strWhere = "(tm_tipork = 'A' OR tm_tipork = 'D' OR tm_tipork = 'E' OR tm_tipork = 'N' OR tm_tipork = '£')"
        Case "P"
          strWhere = "(tm_tipork = 'S' OR tm_tipork = 'P')"
      End Select

      strSQL = "SELECT tm_tipork FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tm_anno = " & nAnno.ToString & " AND tm_serie = " & CStrSQL(strSerie) & _
               " AND tm_numdoc = " & lNumdoc.ToString & _
               " AND " & strWhere
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


  Public Overridable Function CancellaFattura(ByVal strDitta As String, ByVal strTipork As String, _
                                              ByVal nAnno As Integer, ByVal strSerie As String, _
                                              ByVal lNumdoc As Integer) As Boolean
    Try
      Return CancellaFattura(strDitta, strTipork, nAnno, strSerie, lNumdoc, False, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CancellaFattura(ByVal strDitta As String, ByVal strTipork As String, _
                                              ByVal nAnno As Integer, ByVal strSerie As String, _
                                              ByVal lNumdoc As Integer, ByVal bCa2 As Boolean, _
                                              ByVal lIITtkeys As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strDesogglog As String = ""
    Dim strTipoProg As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dttTmp As New DataTable
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, nAnno, strSerie, lNumdoc, bCa2, lIITtkeys})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      If bCa2 Then
        '---------------------------------
        'riscrivo priana2 da testmag/movmag, visto che cancellando la fattura devo ricreare le righe di priana2 che la fattura aveva cancellato
        'non lo devo fare se è una UPDATE, visto che poi dovrei ricancellare le righe di priana2. NO: 
        'lo devo fare sempre, visto che nella fattura potrei aver rimosso dei ddt!!!

        'tutti i test per essere sicuri che la scrittura vada a buon fine sono stati eseguiti in 
        'beveboll.TestPreSalvaTestmag_CheckCa2()
        strSQL = "SELECT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_datdoc " & _
                 " FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND tm_tiporkfat = " & CStrSQL(strTipork) & _
                 " AND tm_annfat = " & nAnno & _
                 " AND tm_alffat = " & CStrSQL(strSerie) & _
                 " AND tm_numfat = " & lNumdoc
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
      End If    'If bCa2 Then

      'Toglie il flag fatturata  a tutte le bolle
      strSQL = "UPDATE testmag " & _
               "SET tm_flfatt = 'N', tm_alffat = ' ', tm_numfat = 0, tm_datfat = Null, tm_annfat = 0, tm_tiporkfat = ' ', tm_ultagg = " & CDataOraSQL(DateTime.Now) & " " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tm_flfatt = 'S' AND tm_tiporkfat = '" & strTipork & "' AND tm_annfat = " & nAnno & " AND tm_numfat = " & lNumdoc & " AND tm_alffat = '" & strSerie & "'"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE testmag" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tm_tipork = '" & strTipork & "' AND tm_anno = " & nAnno & " AND tm_numdoc = " & lNumdoc & " AND tm_serie = '" & strSerie & "'"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE testmagc" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tm_tipork = '" & strTipork & "' AND tm_anno = " & nAnno & " AND tm_numdoc = " & lNumdoc & " AND tm_serie = '" & strSerie & "'"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '------------------------
      'se posso porto il progressivo di tabnuma indietro di un numero
      'solo se il numero documento è uguale a quello di tabnuma ..)
      strTipoProg = strTipork
      Select Case strTipork
        Case "D", "£" : strTipoProg = "A"
        Case "P" : strTipoProg = "S"
      End Select
      If Not DelNuma(strDitta, strTipoProg, strSerie, nAnno, lNumdoc, dbConn) Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Return False
      End If

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      'log attività
      strDesogglog = "Cancellazione fattura differita relativa alla ditta '" & strDitta & "'" & vbCrLf & vbCrLf
      Select Case strTipork
        Case "D" : strDesogglog = strDesogglog & " - Fattura differita emessa N° "
        Case "P" : strDesogglog = strDesogglog & " - Fattura/ricevuta fiscale differita N° "
        Case "£" : strDesogglog = strDesogglog & " - Nota accredito differita emessa N° "
      End Select
      strDesogglog = strDesogglog & lNumdoc & IIf(strSerie.Trim <> "", "/" & strSerie, "").ToString & " del " & nAnno.ToString & vbCrLf
      ScriviActLog(strDitta, "BSVEFADI", "BSVEFADI", "", "", "M", "E", strDesogglog, False)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

#Region "Personalizzazione CEAS"
  Public Overridable Function GetMovmagDDTCeas(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, _
                                               ByVal strSerie As String, ByVal lNumdoc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mm_prezzo, mm_preziva, mm_prezvalc, mm_quant, mm_colli, " & _
               "       mm_perqta, mm_scont1, mm_scont2, mm_scont3, mm_scont4, mm_scont5, mm_scont6, mm_scontp, mm_scontv, " & _
               "       mm_codiva, mm_controp, mm_vprovv, mm_vprovv2, mm_codart, mm_stasino, ar_codiva, tb_aliq, tm_scorpo " & _
               " FROM testmag " & _
               "  INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_numdoc = movmag.mm_numdoc AND testmag.tm_serie = movmag.mm_serie " & _
               "                   AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_tipork = movmag.mm_tipork " & _
               "  INNER JOIN ARTICO on movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart " & _
               "  INNER JOIN TABCIVA on artico.ar_codiva = tabciva.tb_codciva " & _
               " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
               "   AND tm_tiporkfat = " & CStrSQL(strTipork) & _
               "   AND tm_annfat = " & nAnno & _
               "   AND tm_alffat = " & CStrSQL(strSerie) & _
               "   AND tm_numfat = " & lNumdoc & _
               "   AND mm_stasino <> 'X'" & _
               "   AND mm_quant > 0" & _
               "   AND ar_codiva <> 0"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function UpdateTestmagCeas(ByRef dtrTm As DataRow, ByRef dttIVA As DataTable) As Boolean
    'dtrTm = testata della fattura differita da aggiornare
    'dttIVA = dettaglio IVA solo informativo da inserire nel castelletto IVA dal 5 all'8
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Try
      strSQL = "UPDATE testmag SET "
      For i = 0 To 3
        strSQL += "tm_codiva_" & (i + 5).ToString & " = " & CDblSQL(NTSCDec(dttIVA.Rows(i)!codiva)) & ", "
        strSQL += "tm_imponib_" & (i + 5).ToString & " = " & CDblSQL(NTSCDec(dttIVA.Rows(i)!imponib)) & ", "
        strSQL += "tm_imposta_" & (i + 5).ToString & " = " & CDblSQL(NTSCDec(dttIVA.Rows(i)!iva)) & ", "
      Next
      strSQL = strSQL.Substring(0, strSQL.Length - 2)
      strSQL += " WHERE codditt = " & CStrSQL(dtrTm!codditt) & _
                " AND tm_tipork = " & CStrSQL(dtrTm!tm_tipork) & _
                " AND tm_anno = " & dtrTm!tm_anno.ToString & _
                " AND tm_serie = " & CStrSQL(dtrTm!tm_serie) & _
                " AND tm_numdoc = " & dtrTm!tm_numdoc.ToString

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#End Region

  Public Overridable Function GeneraNuovaListaSelezionata(ByVal strDitta As String, _
                            ByVal nCodice As Integer, ByVal strDescr As String, _
                            ByVal dctClienti As Dictionary(Of Integer, Decimal)) As Boolean
    Dim iTransRes As Integer = 0
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Try
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      strSQL = "INSERT INTO tablsel (codditt, tb_codlsel, tb_deslsel, tb_dtcomp, tb_lselnote, tb_opnomeinc)" & _
               " VALUES( " & CStrSQL(strDitta) & ", " & nCodice & ", " & CStrSQL(strDescr) & _
               ", " & CDataSQL(Date.Now()) & ", 'Generata da Fatturazione differita', " & CStrSQL(oApp.User.Nome) & ")"
      iTransRes = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If iTransRes = -1 Then
        Return False
      End If

      For Each lConto As Integer In dctClienti.Keys
        strSQL = "INSERT INTO listsel (codditt, lse_codlsel, lse_conto, lse_note)" & _
                 " VALUES( " & CStrSQL(strDitta) & ", " & nCodice & ", " & _
                 lConto & ", " & CStrSQL(NTSCStr(dctClienti.Item(lConto)).PadLeft(20, CChar(" "))) & ")"
        iTransRes = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If iTransRes = -1 Then
          Return False
        End If
      Next

      ChiudiTrans()
      dbConn.Close()
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      If IsInTrans Then AnnullaTrans()
      If dbConn IsNot Nothing AndAlso dbConn.State = ConnectionState.Open Then dbConn.Close()
    End Try
  End Function

  Public Overridable Function GetMaxNumeroListaSel(ByVal strDitta As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As DataTable = Nothing
    Dim nMaxnum As Integer = 0
    Try
      strSQL = "SELECT MAX(tb_codlsel) AS maxnum FROM tablsel " & _
               " WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp IsNot Nothing AndAlso dttTmp.Rows.Count > 0 Then
        nMaxnum = NTSCInt(dttTmp.Rows(0)!maxnum)
      End If
      Return nMaxnum
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
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

End Class
