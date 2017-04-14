Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDVEFDIN
  Inherits CLD__BASE

  Public strTestmagApri As String = "SELECT testmag.*, tm_dianno as xx_dianno, tm_dinumero as xx_dinumero, tb_destpbf as xx_tipobf, tb_desvalu as xx_valuta, tabpaga.tb_despaga as xx_codpaga, " & _
                                 "co_descr1 as xx_commeca, tb_descfam as xx_codcfam, tb_descena as xx_codcena,tb_desbanc as xx_codbanc, " & _
                                 "tabvett.tb_desvett as xx_vettor,tabvett2.tb_desvett as xx_vettor2,tabmagai.tb_desmaga as xx_magimp, " & _
                                 "tabmaga2.tb_desmaga as xx_magaz2, tb_desciva as xx_codese, tabmaga.tb_desmaga as xx_magaz, " & _
                                 "tabcage.tb_descage as xx_codagen, tabcage2.tb_descage as xx_codagen2, tabtpbf.tb_przbol as xx_przbol, " & _
                                 "tabcaum.tb_descaum as xx_causale, tabcaum2.tb_descaum as xx_causcap, tb_desport as xx_porto, " & _
                                 "tabcaum3.tb_descaum as xx_caustra, destdiv.dd_nomdest as xx_coddest, destdiv2.dd_nomdest as xx_coddest2, " & _
                                 "tb_desntra as xx_codntra, tb_desmpor as xx_codport, tb_desrsta as xx_codrsta, " & _
                                 " '' as xx_conto, '' as xx_tipo, '' as xx_controp, '' as xx_contfatt, tb_desstag as xx_codstag, " & _
                                 " tabpaga2.tb_despaga as xx_codpaga2 " & _
                                 " FROM ((((((((((((((((((((((((((testmag INNER JOIN testmagc ON testmag.codditt = testmagc.codditt AND testmag.tm_tipork = testmagc.tm_tipork AND testmag.tm_anno = testmagc.tm_anno AND testmag.tm_serie = testmagc.tm_serie AND testmag.tm_numdoc = testmagc.tm_numdoc) " & _
                                 " LEFT JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga) " & _
                                 " LEFT JOIN tabvalu ON testmag.tm_valuta = tabvalu.tb_codvalu) " & _
                                 " LEFT JOIN tabciva ON testmag.tm_codese = tabciva.tb_codciva) " & _
                                 " LEFT JOIN tabcaum AS tabcaum3 ON testmag.tm_caustra = tabcaum3.tb_codcaum) " & _
                                 " LEFT JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf) " & _
                                 " LEFT JOIN commess ON testmag.codditt = commess.codditt AND testmag.tm_commeca = commess.co_comme) " & _
                                 " LEFT JOIN tabcfam ON testmag.codditt = tabcfam.codditt AND testmag.tm_codcfam = tabcfam.tb_codcfam) " & _
                                 " LEFT JOIN tabcena ON testmag.codditt = tabcena.codditt AND testmag.tm_codcena = tabcena.tb_codcena) " & _
                                 " LEFT JOIN tabbanc ON testmag.codditt = tabbanc.codditt AND testmag.tm_codbanc = tabbanc.tb_codbanc) " & _
                                 " LEFT JOIN tabvett ON testmag.codditt = tabvett.codditt AND testmag.tm_vettor = tabvett.tb_codvett) " & _
                                 " LEFT JOIN tabvett as tabvett2 ON testmag.codditt = tabvett2.codditt AND testmag.tm_vettor2 = tabvett2.tb_codvett) " & _
                                 " LEFT JOIN tabcaum ON testmag.tm_causale = tabcaum.tb_codcaum) " & _
                                 " LEFT JOIN tabntra ON testmag.tm_codntra = tabntra.tb_codntra)" & _
                                 " LEFT JOIN tabmpor ON testmag.tm_codport = tabmpor.tb_codmpor)" & _
                                 " LEFT JOIN tabrsta ON testmag.tm_codrsta = tabrsta.tb_codrsta)" & _
                                 " LEFT JOIN tabcaum as tabcaum2 ON testmag.tm_causcap = tabcaum2.tb_codcaum) " & _
                                 " LEFT JOIN tabmaga ON testmag.codditt = tabmaga.codditt AND testmag.tm_magaz = tabmaga.tb_codmaga) " & _
                                 " LEFT JOIN tabmaga as tabmaga2 ON testmag.codditt = tabmaga2.codditt AND testmag.tm_magaz2 = tabmaga2.tb_codmaga) " & _
                                 " LEFT JOIN tabmaga as tabmagai ON testmag.codditt = tabmagai.codditt AND testmag.tm_magimp = tabmagai.tb_codmaga) " & _
                                 " LEFT JOIN tabcage ON testmag.codditt = tabcage.codditt AND testmag.tm_codagen = tabcage.tb_codcage) " & _
                                 " LEFT JOIN tabcage as tabcage2 ON testmag.codditt = tabcage2.codditt AND testmag.tm_codagen2 = tabcage2.tb_codcage) " & _
                                 " LEFT JOIN tabport ON testmag.codditt = tabport.codditt AND testmag.tm_porto = tabport.tb_codport) " & _
                                 " LEFT JOIN tabstag ON testmag.codditt = tabstag.codditt AND testmag.tm_codstag = tabstag.tb_codstag) " & _
                                 " LEFT JOIN destdiv ON testmag.codditt = destdiv.codditt AND testmag.tm_conto = destdiv.dd_conto AND testmag.tm_coddest = destdiv.dd_coddest) " & _
                                 " LEFT JOIN destdiv as destdiv2 ON testmag.codditt = destdiv2.codditt AND testmag.tm_conto = destdiv2.dd_conto AND testmag.tm_coddest2 = destdiv2.dd_coddest) " & _
                                 " LEFT JOIN tabpaga as tabpaga2 ON testmag.tm_codpaga2 = tabpaga2.tb_codpaga "

  Public Overridable Function EsisteDoc(ByVal strDitta As String, ByVal strTipoDoc As String, _
                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean

    'restituisce true se il documento esiste, diversamente false
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim bResult As Boolean = False
    Dim strWhere As String = ""
    Try
      Select Case strTipoDoc
        Case "D", "£"
          'fatt diff emesse, nota accred diff. emesse
          strWhere = "(tm_tipork = 'A' OR tm_tipork = 'D' OR tm_tipork = 'E' OR tm_tipork = 'N' OR tm_tipork = '£')"
        Case "K", "("
          'fatt diff ricevute, note accred diff ricevute
          strWhere = "(tm_tipork = 'L' OR tm_tipork = 'K' OR tm_tipork = 'J' OR tm_tipork = '(')"
        Case "P"
          'Fatt.Ric.Fisc.Differita
          strWhere = "(tm_tipork = 'S' OR tm_tipork = 'P')"
      End Select
      strSQL = "SELECT top 1 tm_numdoc FROM testmag " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND " & strWhere & _
               " AND tm_anno = " & nAnno & _
               " AND tm_serie = " & CStrSQL(strSerie) & _
               " AND tm_numdoc = " & lNumdoc.ToString
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEST")
      If dsTmp.Tables("TEST").Rows.Count > 0 Then bResult = True
      dsTmp.Dispose()

      Return bResult

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ApriDocumento(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, _
                                      ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                      ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      '------------------------------------
      'testmag
      strSQL = strTestmagApri
      strSQL = strSQL & " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                        " AND testmag.tm_tipork = " & CStrSQL(strTipoDoc) & _
                        " AND testmag.tm_anno = " & nAnno & _
                        " AND testmag.tm_serie = " & CStrSQL(strSerie)
      If bNew Then
        strSQL = strSQL & " AND testmag.tm_numdoc = -1"
      Else
        strSQL = strSQL & " AND testmag.tm_numdoc = " & lNumdoc
      End If

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTMAG")
      If ds.Tables("TESTMAG").Rows.Count > 0 Then
        If NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_contfatt) <> 0 Then
          strSQL = "SELECT an_descr1 FROM anagra WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_contfatt).ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then ds.Tables("TESTMAG").Rows(0)!xx_contfatt = NTSCStr(dttTmp.Rows(0)!an_descr1)
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
      End If
      '--------------------------------------------------------------------------------------------------------------
      'testmagc
      strSQL = "SELECT * FROM testmagc" & _
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

      '------------------------------------
      'elenco ddt collegati alla fattura
      strSQL = "SELECT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_datdoc, tm_totdoc, tm_annpar," & _
              " tm_alfpar, tm_numpar, tm_datpar, tm_ultagg, tm_valuta, tm_cambio, tm_scopag, " & _
              " tm_coddest, tm_codagen, tm_conto, tm_bolli, tm_bolliv, tm_speimb, tm_speimbv, " & _
              " tm_speacc, tm_speaccv, tm_abbuono, tm_abbuonov, tm_totcoll, tm_peso, tm_pesonetto, " & _
              " tm_pagato, tm_pagatov, tm_totomag, tm_totomagv, tm_speinc, tm_speincv,  " & _
              " tm_impprov, tm_totprov, tm_totprov2, tm_riferim, tm_pagato2, tm_resto, tm_flspinc, " & _
              " tm_cup, tm_cig, tm_riferimpa" & _
              " FROM testmag" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tm_tiporkfat = " & CStrSQL(strTipoDoc) & _
              " AND tm_annfat = " & nAnno & _
              " AND tm_alffat = " & CStrSQL(strSerie) & _
              " AND tm_numfat = " & lNumdoc & _
              " ORDER BY tm_serie, tm_numdoc"
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ELENCODDT", ds)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function Salvadocumento(ByRef ds As DataSet, ByVal strState As String, ByVal bProgrCambiato As Boolean, _
                                        ByVal bAggnumprot As Boolean, ByVal bConsentiModifCodPagaSc As Boolean, _
                                        ByVal bNoUpdateTipobf As Boolean, ByRef strError As String) As Boolean
    Try
      Return Salvadocumento(ds, strState, bProgrCambiato, bAggnumprot, bConsentiModifCodPagaSc, bNoUpdateTipobf, _
                            strError, False, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Salvadocumento(ByRef ds As DataSet, ByVal strState As String, ByVal bProgrCambiato As Boolean, _
                                        ByVal bAggnumprot As Boolean, ByVal bConsentiModifCodPagaSc As Boolean, _
                                        ByVal bNoUpdateTipobf As Boolean, ByRef strError As String, ByVal bCa2 As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, bConsentiModifCodPagaSc, strState, bProgrCambiato, bAggnumprot, bConsentiModifCodPagaSc, bNoUpdateTipobf, strError, bCa2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        strError = NTSCStr(oIn(6))
        Return CBool(oOut)
      End If
      '----------------

      Return Salvadocumento(ds, strState, bProgrCambiato, bAggnumprot, bConsentiModifCodPagaSc, bNoUpdateTipobf, _
                            strError, False, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Salvadocumento(ByRef ds As DataSet, ByVal strState As String, ByVal bProgrCambiato As Boolean, _
                                          ByVal bAggnumprot As Boolean, ByVal bConsentiModifCodPagaSc As Boolean, _
                                          ByVal bNoUpdateTipobf As Boolean, ByRef strError As String, ByVal bCa2 As Boolean, _
                                          ByVal bConsentiModifFlspinc As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, bConsentiModifCodPagaSc, strState, bProgrCambiato, bAggnumprot, bConsentiModifCodPagaSc, bNoUpdateTipobf, strError, bCa2, bConsentiModifFlspinc})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        strError = NTSCStr(oIn(6))
        Return CBool(oOut)
      End If
      '----------------
      Return Salvadocumento(ds, strState, bProgrCambiato, bAggnumprot, bConsentiModifCodPagaSc, bNoUpdateTipobf, _
        strError, bCa2, bConsentiModifFlspinc, False)
      '---------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Salvadocumento(ByRef ds As DataSet, ByVal strState As String, ByVal bProgrCambiato As Boolean, _
                                          ByVal bAggnumprot As Boolean, ByVal bConsentiModifCodPagaSc As Boolean, _
                                          ByVal bNoUpdateTipobf As Boolean, ByRef strError As String, ByVal bCa2 As Boolean, _
                                          ByVal bConsentiModifFlspinc As Boolean, _
                                          ByVal bSostituisciCodicePagamento As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim lResult As Integer = 0
    Dim strDesogglog As String = ""
    Dim strSqlWhere As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim lNewProgr As Integer = 0
    Dim strTipoProg As String = ""
    Dim lIITtkeys As Integer = 0
    Dim strAlfpro As String = ""

    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, bConsentiModifCodPagaSc, strState, bProgrCambiato, bAggnumprot, _
        bConsentiModifCodPagaSc, bNoUpdateTipobf, strError, bCa2, bConsentiModifFlspinc, bSostituisciCodicePagamento})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        strError = NTSCStr(oIn(6))
        Return CBool(oOut)
      End If
      '----------------

      '---------------------------------
      'scrivo actlog
      If strState <> "N" Then
        Select Case ds.Tables("TESTMAG").Rows(0)!tm_tipork.ToString.ToUpper
          Case "D" : strDesogglog = "Fattura differita emessa"
          Case "K" : strDesogglog = "Fattura differita ricevuta"
          Case "P" : strDesogglog = "Fattura/ricevuta fiscale differita"
          Case "£" : strDesogglog = "Nota accred. differita emessa"
          Case "(" : strDesogglog = "Nota accred. differita ricevuta"
        End Select
        strDesogglog += " n. " & NTSCStr(ds.Tables("TESTMAG").Rows(0)!tm_numdoc.ToString)
        If ds.Tables("TESTMAG").Rows(0)!tm_serie.ToString.Trim <> "" Then
          strDesogglog += "/" & ds.Tables("TESTMAG").Rows(0)!tm_serie.ToString
        End If
        strDesogglog += " del " & NTSCDate(ds.Tables("TESTMAG").Rows(0)("tm_datdoc", DataRowVersion.Original)).ToShortDateString
        strDesogglog = IIf(strState = "D", "Cancella", "Modifica").ToString() & " " & strDesogglog

        strSqlWhere = "'" & ds.Tables("TESTMAG").Rows(0)!codditt.ToString & "', " & _
                      "'" & ds.Tables("TESTMAG").Rows(0)!tm_tipork.ToString & "', " & _
                      ds.Tables("TESTMAG").Rows(0)!tm_anno.ToString & ", " & _
                      "'" & ds.Tables("TESTMAG").Rows(0)!tm_serie.ToString & "', " & _
                      ds.Tables("TESTMAG").Rows(0)!tm_numdoc.ToString
      End If    ' If strState <> "N" Then

      If bCa2 Then
        lIITtkeys = GetTblInstId("TTKEYS", False)
      End If

      '---------------------------------
      'apro il database e la transazione
      'factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      If strDesogglog <> "" Then ScriviActLog(ds.Tables("TESTMAG").Rows(0)!codditt.ToString, "BSVEFDIN", _
                                              "TESTMAG", "TESTMAG", _
                                              strSqlWhere, IIf(strState = "D", "A", "M").ToString, "D", strDesogglog, False)

      '---------------------------------
      'prima cancello il documento (non lo faccio se è una nuovo doc)
      If strState <> "N" Then
        With ds.Tables("TESTMAG").Rows(0)
          If Not CancellaDocumento(!codditt.ToString, !tm_tipork.ToString, NTSCInt(!tm_anno), !tm_serie.ToString, _
                                   NTSCInt(!tm_numdoc), strState, dbConn, bCa2, lIITtkeys, ds) Then
            If IsInTrans Then AnnullaTrans()
            Return False
          End If
        End With
      End If

      If strState = "D" Then GoTo FINE

      '---------------------------------
      'aggiorno il numero del documento
      If strState = "N" And bProgrCambiato = False Then
        Select Case ds.Tables("TESTMAG").Rows(0)!tm_tipork.ToString
          Case "D", "£" : strTipoProg = "A"
          Case "K", "(" : strTipoProg = "L"
          Case "P" : strTipoProg = "S"
        End Select
        lNewProgr = AggNuma(ds.Tables("TESTMAG").Rows(0)!codditt.ToString, _
                strTipoProg, _
                ds.Tables("TESTMAG").Rows(0)!tm_serie.ToString, _
                NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_anno.ToString), _
                NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_numdoc.ToString), _
                True, True, strError, dbConn)
        '----------------------------
        'si è verificato un errore
        If lNewProgr = 0 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(strError))
        End If

        If lNewProgr <> NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_numdoc.ToString) Or strError <> "" Then
          If IsInTrans Then AnnullaTrans()
          '----------------------------
          'aggiorno il numero del documento con il nuovo numero, visto che quello che avevo impostato è già stato utilizzato
          strError = "*N*" & strError   'aggiungo il marcatore che permetterà all'entity di cambiare il num doc su tutti i datatable
          ds.Tables("TESTMAG").Rows(0)!tm_numdoc = lNewProgr

          Return False
        End If
      End If    'If strState = "N" And bProgrCambiato = False Then

      '---------------------------------
      'Aggiorna il protocollo doc. ricevuto
      If bAggnumprot Then
        strAlfpro = NTSCStr(ds.Tables("TESTMAG").Rows(0)!tm_alfpro)
        If strAlfpro = "" Then strAlfpro = " "
        lNewProgr = AggNuma(ds.Tables("TESTMAG").Rows(0)!codditt.ToString, _
                            "P", _
                            "A" & strAlfpro & NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_nregiva).ToString("000"), _
                            NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_anno.ToString), _
                            NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_numprot), _
                            True, False, strError, dbConn)
        If lNewProgr = 0 Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Throw (New NTSException(strError))
        End If
        If lNewProgr <> NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_numprot) Then
          ds.Tables("TESTMAG").Rows(0)!tm_numprot = lNewProgr
          strError = oApp.Tr(Me, 128928328832648230, "Il protocollo acquisti assegnato a questo documento ricevuto è già stato utilizzato da un altro utente; il nuovo numero di protocollo assegnato è |" & lNewProgr.ToString & "|")
        End If
      End If    'If bAggnumprot Then

      '---------------------------------
      'insert into TESTA
      strSQL = "INSERT INTO testmag " & GetQueryInsertField(ds.Tables("TESTMAG"), "tm_")
      strSQLVal = GetQueryInsertValue(ds.Tables("TESTMAG"), ds.Tables("TESTMAG").Rows(0), "tm_")
      lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 127791221369687500, "Si è verificato un errore in fase di scrittura testata Documento: Documento non salvato")))
      End If

      strSQL = "INSERT INTO testmagc " & GetQueryInsertField(ds.Tables("TESTMAGC"), "tm_")
      strSQLVal = GetQueryInsertValue(ds.Tables("TESTMAGC"), ds.Tables("TESTMAGC").Rows(0), "tm_")
      lResult = Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI, dbConn)
      If lResult = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(oApp.Tr(Me, 128656110249687500, "Si è verificato un errore in fase di scrittura testata Documento - dettaglio contropartite: Documento non salvato")))
      End If


      '---------------------------------
      'aggiorno i riferimenti sui DDT
      If Not SalvaDocumentoAggiornaBoll(ds, bConsentiModifCodPagaSc, bNoUpdateTipobf, dbConn, bCa2, bConsentiModifFlspinc, bSostituisciCodicePagamento) Then Return False

FINE:

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
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      Try
        If bCa2 Then
          ResetTblInstId("TTKEYS", False, lIITtkeys)
        End If
      Catch
      End Try
    End Try
  End Function

  Public Overridable Function SalvaDocumentoAggiornaBoll(ByRef ds As DataSet, ByVal bConsentiModifCodPagaSc As Boolean, _
                                                        ByVal bNoUpdateTipobf As Boolean, ByRef dbConn As DbConnection) As Boolean
    Try
      Return SalvaDocumentoAggiornaBoll(ds, bConsentiModifCodPagaSc, bNoUpdateTipobf, dbConn, False, False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoAggiornaBoll(ByRef ds As DataSet, ByVal bConsentiModifCodPagaSc As Boolean, _
                                                      ByVal bNoUpdateTipobf As Boolean, ByRef dbConn As DbConnection, _
                                                      ByVal bCa2 As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, bConsentiModifCodPagaSc, bNoUpdateTipobf, dbConn, bCa2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return SalvaDocumentoAggiornaBoll(ds, bConsentiModifCodPagaSc, bNoUpdateTipobf, dbConn, bCa2, False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumentoAggiornaBoll(ByRef ds As DataSet, ByVal bConsentiModifCodPagaSc As Boolean, _
                                                        ByVal bNoUpdateTipobf As Boolean, ByRef dbConn As DbConnection, _
                                                        ByVal bCa2 As Boolean, ByVal bConsentiModifFlspinc As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, bConsentiModifCodPagaSc, bNoUpdateTipobf, dbConn, bCa2, bConsentiModifFlspinc})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return SalvaDocumentoAggiornaBoll(ds, bConsentiModifCodPagaSc, bNoUpdateTipobf, dbConn, bCa2, bConsentiModifFlspinc, False)

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function SalvaDocumentoAggiornaBoll(ByRef ds As DataSet, ByVal bConsentiModifCodPagaSc As Boolean, _
                                                        ByVal bNoUpdateTipobf As Boolean, ByRef dbConn As DbConnection, _
                                                        ByVal bCa2 As Boolean, ByVal bConsentiModifFlspinc As Boolean, _
                                                        ByVal bSostituisciCodicePagamento As Boolean) As Boolean
    Dim bStornaDDTResoForn As Boolean = CBool(GetSettingBus("BSVEFDIN", "OPZIONI", ".", "StornaDDTResoForn", "0", " ", "0"))
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {ds, bConsentiModifCodPagaSc, bNoUpdateTipobf, dbConn, bCa2, _
        bConsentiModifFlspinc, bSostituisciCodicePagamento})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(0), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      With ds.Tables("TESTMAG").Rows(0)
        For Each dtrT As DataRow In ds.Tables("ELENCODDT").Rows
          strSQL = "UPDATE testmag SET tm_flfatt = 'S'," & _
                  " tm_tiporkfat = " & CStrSQL(!tm_tipork) & "," & _
                  " tm_alffat = " & CStrSQL(!tm_serie) & "," & _
                  " tm_numfat = " & !tm_numdoc.ToString & "," & _
                  " tm_annfat = " & !tm_anno.ToString & "," & _
                  " tm_datfat = " & CDataSQL(NTSCDate(!tm_datdoc)) & "," & _
                  " tm_datapag = " & CDataSQL(NTSCDate(!tm_datapag)) & "," & _
                  " tm_abi = " & !tm_abi.ToString & "," & _
                  " tm_cab = " & !tm_cab.ToString & "," & _
                  " tm_banc1 = " & CStrSQL(!tm_banc1) & "," & _
                  " tm_banc2 = " & CStrSQL(!tm_banc2) & "," & _
                  " tm_codbanc = " & !tm_codbanc.ToString & "," & _
                  " tm_codpaga2 = " & CStrSQL(!tm_codpaga2) & "," & _
                  " tm_ultagg = " & CDataOraSQL(DateTime.Now)
          If bConsentiModifCodPagaSc Or bSostituisciCodicePagamento Then  
            strSQL += " , tm_codpaga = " & !tm_codpaga.ToString
          End If
          If bConsentiModifFlspinc Then
            strSQL += " , tm_flspinc = " & CStrSQL(!tm_flspinc.ToString)
          End If
          If (NTSCStr(ds.Tables("TESTMAG").Rows(0)!tm_tipork).ToUpper = "K") And _
             (NTSCStr(dtrT!tm_tipork).ToUpper = "B") And _
             (bStornaDDTResoForn = True) Then
          Else
            If bNoUpdateTipobf = False Then strSQL += " ,tm_tipobf = " & !tm_tipobf.ToString
          End If
          strSQL += " WHERE codditt = " & CStrSQL(!codditt) & _
                    " AND tm_tipork = " & CStrSQL(dtrT!tm_tipork) & _
                    " AND tm_anno = " & dtrT!tm_anno.ToString & _
                    " AND tm_serie = " & CStrSQL(dtrT!tm_serie) & _
                    " AND tm_numdoc = " & dtrT!tm_numdoc.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '---------------------------------
          'cancello priana2 e pria2comp collegato ai documenti di magazzino
          If bCa2 Then
            strSQL = "DELETE FROM pria2comp WHERE codditt = " & CStrSQL(!codditt) & _
                   " AND pac_progr IN (SELECT pa2_progr FROM priana2 WHERE " & _
                   " codditt = " & CStrSQL(!codditt) & _
                   " AND pa2_numreg = 0 " & _
                   " AND (pa2_mmtipork = " & CStrSQL(dtrT!tm_tipork)
            If dtrT!tm_tipork.ToString = "T" Then strSQL += " OR pa2_mmtipork = 'U' "
            strSQL += ") AND pa2_mmanno = " & dtrT!tm_anno.ToString & _
                      " AND pa2_mmserie = " & CStrSQL(dtrT!tm_serie) & _
                      " AND pa2_mmnumdoc = " & dtrT!tm_numdoc.ToString & ")"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            strSQL = "DELETE FROM priana2 WHERE " & _
                     " codditt = " & CStrSQL(!codditt) & _
                     " AND pa2_numreg = 0 " & _
                     " AND (pa2_mmtipork = " & CStrSQL(dtrT!tm_tipork)
            If dtrT!tm_tipork.ToString = "T" Then strSQL += " OR pa2_mmtipork = 'U' "
            strSQL += ") AND pa2_mmanno = " & dtrT!tm_anno.ToString & _
                     " AND pa2_mmserie = " & CStrSQL(dtrT!tm_serie) & _
                     " AND pa2_mmnumdoc = " & dtrT!tm_numdoc.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If    'If bCa2 Then
        Next

      End With    'With ds.Tables("TESTMAG").Rows(0)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaDocumentoAggiornaBollAfterAll(ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il database e la transazione factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      With ds.Tables("TESTMAG").Rows(0)
        strSQL = "UPDATE testmag" & _
          " SET tm_flagiva_1 = NULL," & _
          " tm_ultagg = " & CDataOraSQL(DateTime.Now) & _
          " WHERE codditt = " & CStrSQL(!codditt) & _
          " AND tm_tipork = " & CStrSQL(!tm_tipork) & _
          " AND tm_anno = " & NTSCInt(!tm_anno) & _
          " AND tm_serie = " & CStrSQL(!tm_serie) & _
          " AND tm_numdoc = " & NTSCInt(!tm_numdoc)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End With
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiudo la transazione ed il database
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      ds.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CancellaDocumento(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumdoc As Integer, ByVal strState As String, _
                                          ByRef dbConn As DbConnection) As Boolean
    Try
      Return CancellaDocumento(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc, strState, dbConn, False, 0, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CancellaDocumento(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumdoc As Integer, ByVal strState As String, _
                                          ByRef dbConn As DbConnection, ByVal bCa2 As Boolean, _
                                          ByVal lIITtkeys As Integer) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipoDoc, nAnno, strSerie, lNumdoc, strState, dbConn, bCa2, lIITtkeys})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      Return CancellaDocumento(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc, strState, dbConn, False, lIITtkeys, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CancellaDocumento(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumdoc As Integer, ByVal strState As String, _
                                          ByRef dbConn As DbConnection, ByVal bCa2 As Boolean, _
                                          ByVal lIITtkeys As Integer, ByRef ds As DataSet) As Boolean

    Dim strSQL As String = ""
    Dim strTipoProg As String = ""
    Dim dttTmp As New DataTable
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipoDoc, nAnno, strSerie, lNumdoc, strState, dbConn, bCa2, lIITtkeys, ds})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      If bCa2 Then
        '---------------------------------
        'riscrivo priana2 da testmag/movmag, visto che cancellando la fattura devo ricreare le righe di priana2 che la fattura aveva cancellato
        'non lo devo fare se è una UPDATE, visto che poi dovrei ricancellare le righe di priana2. NO: 
        'lo devo fare sempre, visto che nella fattura potrei aver rimosso dei ddt!!!

        'tutti i test per essere sicuri che la scrittura vada a buon fine sono stati eseguiti in 
        'beveboll.TestPreSalvaTestmag_CheckCa2()
        strSQL = "SELECT tm_tipork, tm_anno, tm_serie, tm_numdoc, tm_datdoc " & _
                 " FROM testmag WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND tm_tiporkfat = " & CStrSQL(strTipoDoc) & _
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

      'Toglie il flag fatturata a tutte le bolle
      strSQL = "UPDATE testmag " & _
               " SET tm_flfatt = 'N', tm_alffat = ' ', tm_numfat = 0, tm_datfat = Null, tm_annfat = 0, " & _
               " tm_tiporkfat = ' ', tm_ultagg = " & CDataOraSQL(Now) & " " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tm_flfatt = 'S' AND tm_tiporkfat = " & CStrSQL(strTipoDoc) & _
               " AND tm_annfat = " & nAnno & _
               " AND tm_numfat = " & lNumdoc & _
               " AND tm_alffat = " & CStrSQL(strSerie)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE testmag" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND tm_anno = " & nAnno & _
              " AND tm_numdoc = " & lNumdoc & _
              " AND tm_serie = " & CStrSQL(strSerie)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE testmagc" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND tm_anno = " & nAnno & _
              " AND tm_numdoc = " & lNumdoc & _
              " AND tm_serie = " & CStrSQL(strSerie)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '------------------------
      'se posso porto il progressivo di tabnuma indietro di un numero
      'solo se il numero documento è uguale a quello di tabnuma ..)
      If strState = "D" Or strState = "E" Then
        strTipoProg = strTipoDoc
        Select Case strTipoDoc
          Case "D", "£" : strTipoProg = "A"
          Case "K", "(" : strTipoProg = "L"
          Case "P" : strTipoProg = "S"
        End Select
        If Not DelNuma(strDitta, strTipoProg, strSerie, nAnno, lNumdoc, dbConn) Then
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If
        '------------------------
        'se il num. di protocollo è <> 0, cerco di tornare indietro di 1 (esattamente come per il num. documento)
        'se è contabilizzato non posso fare nulla, perchè se torno indietro sul numeratore senza cancellare la reg. incg
        'come faccio una nuova reg. di acquisto mi proporrebbe il protocollo già usato in CG!
        If Not ds Is Nothing Then
          With ds.Tables("TESTMAG").Rows(0)
            If (strTipoDoc = "K" Or strTipoDoc = "(") And NTSCInt(!tm_numprot) <> 0 And NTSCInt(!tm_nregiva) <> 0 And _
              NTSCStr(!tm_flcont) = "N" Then
              If Not DelNuma(!codditt.ToString, _
                             "P", _
                             "A" & NTSCStr(!tm_alfpro) & NTSCInt(!tm_nregiva).ToString("000"), _
                             NTSCInt(!tm_anno), _
                             NTSCInt(!tm_numprot), _
                             dbConn) Then
                If IsInTrans Then AnnullaTrans()
                dbConn.Close()
                Return False
              End If
            End If
          End With
        End If
      End If    'If strState = "D" Or strState = "E" Then

      Return True

    Catch ex As Exception
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
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

  Public Overridable Function GetTimeStamp(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                            ByVal nAnno As Integer, ByVal strSerie As String, _
                                            ByVal lNumdoc As Integer) As DateTime
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      GetTimeStamp = NTSCDate(IntSetDate("01/01/1900"))
      strSQL = "SELECT tm_ultagg FROM " & IIf(strTipoDoc = "W", "testprb", "testmag").ToString & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(strTipoDoc) & _
              " AND tm_anno = " & nAnno & _
              " AND tm_serie = " & CStrSQL(strSerie) & _
              " AND tm_numdoc = " & lNumdoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        GetTimeStamp = NTSCDate(dttTmp.Rows(0)!tm_ultagg)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetWhereDDT(ByVal strDitta As String, ByVal bDocemesso As Boolean, ByVal strUsaContoFattDoc As String, ByVal lConto As Integer, _
                                          ByVal bUsaContoFatt As Boolean, ByVal strTipoDoc As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                          ByVal bIgnoraSegueFatturaSuDocRic As Boolean, ByRef dttDTT As DataTable) As String
    Dim bStornaDDTResoForn As Boolean = CBool(GetSettingBus("BSVEFDIN", "OPZIONI", ".", "StornaDDTResoForn", "0", " ", "0"))
    Dim strSQL As String = ""

    Try
      If strTipoDoc = "£" Then
        'note accred differite emesse o ricevute
        strSQL = " (tm_tipork = 'M') "
      ElseIf strTipoDoc = "(" Then
        'note accred differite  ricevute
        strSQL = " (tm_tipork = 'B') "
      Else
        'casi standard
        If bDocemesso Then
          strSQL = " (tm_tipork = 'B' OR tm_tipork = 'F') "
        Else
          If (strTipoDoc = "K") And (bStornaDDTResoForn = True) Then
            strSQL = " ((tm_tipork = 'M' OR tm_tipork = 'T')" & _
                  " OR (tm_tipork = 'B'" & _
                  " AND tm_conto = " & lConto & _
                  " AND tb_flresocl <> 'N'))"
          Else
            strSQL = " (tm_tipork = 'M' OR tm_tipork = 'T') "
          End If
        End If
      End If

      strSQL += " AND ((tm_conto = " & lConto
      If (strUsaContoFattDoc = "-1" Or strUsaContoFattDoc = "2") Then
        strSQL += " AND tm_contfatt = 0) "
      Else
        strSQL += ") "
      End If
      If (strUsaContoFattDoc = "-1" Or strUsaContoFattDoc = "2") = True Then
        strSQL += " OR tm_contfatt = " & lConto
      Else
        If bUsaContoFatt = True Then strSQL = strSQL & " OR tm_conto IN (SELECT an_conto FROM anagra WHERE codditt = " & CStrSQL(strDitta) & " AND an_contfatt = " & lConto & ")" '" OR an_contfatt = " & lConto
      End If
      strSQL += ")" & _
              " AND (tm_flfatt <> 'S' OR (tm_flfatt = 'S' AND tm_tiporkfat = '" & strTipoDoc & _
              "' AND tm_annfat = " & nAnno & " AND tm_alffat = '" & strSerie & "' AND tm_numfat = " & lNumdoc & "))"

      'tabtpbf.tb_flresocl (Da fatturare) N=no S=Segue fattura F=segue fatt./ric.fisc.
      'fattura differita emessa o not accred diff. emessa
      If strTipoDoc = "D" Or strTipoDoc = "£" Then
        strSQL += " AND tb_flresocl ='S'"
      Else
        If strTipoDoc = "P" Then
          strSQL += " AND tb_flresocl ='F'"
        Else
          'è una fattura differita ricevuta ('K') o 
          'nota di accredi differita ricevuta ('(')
          'il controllo sul 'Segue Fattura' viene fatto sempre (escluso su opzione)
          If Not bIgnoraSegueFatturaSuDocRic Then
            strSQL += " AND tb_flresocl ='S'"
          End If
        End If
      End If
      'tolgo i documenti già collegati alla fattura
      If dttDTT.Rows.Count > 0 Then
        strSQL += " AND NOT ("
        For Each dtrT As DataRow In dttDTT.Rows
          strSQL += "(tm_tipork = " & CStrSQL(dtrT!tm_tipork) & _
                   " AND tm_anno = " & dtrT!tm_anno.ToString & _
                   " AND tm_serie = " & CStrSQL(dtrT!tm_serie) & _
                   " AND tm_numdoc = " & dtrT!tm_numdoc.ToString & ") OR "
        Next
        strSQL = strSQL.Substring(0, strSQL.Length - 4) & " )"
      End If    'If dttDTT.rows.count > 0 Then

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTestmagDDT(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                            ByVal nAnno As Integer, ByVal strSerie As String, _
                                            ByVal lNumdoc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = " SELECT testmag.*, testmagc.tm_dianno, testmagc.tm_dinumero " & _
               " FROM testmag INNER JOIN testmagc ON testmag.codditt = testmagc.codditt AND testmag.tm_tipork = testmagc.tm_tipork AND testmag.tm_anno = testmagc.tm_anno AND testmag.tm_serie = testmagc.tm_serie AND testmag.tm_numdoc = testmagc.tm_numdoc" & _
               " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
               " AND testmag.tm_tipork = " & CStrSQL(strTipoDoc) & _
               " AND testmag.tm_anno = " & nAnno & _
               " AND testmag.tm_serie = " & CStrSQL(strSerie) & _
               " AND testmag.tm_numdoc = " & lNumdoc
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetQueryStampaPdf(ByVal strDitta As String, ByVal strTipoDoc As String, _
                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As String
    Dim strSQL As String = ""
    Try

      strSQL = " SELECT tm_tipork as tipork, tm_anno as anno, tm_serie as serie, tm_numdoc as numero, " & _
             "tm_conto as conto, tm_valuta as valuta, tm_scorpo as scorpo, tm_codagen as agente, " & _
             "tm_coddest AS destin" & _
             " FROM testmag " & _
             " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
             " AND tm_tipork = " & CStrSQL(strTipoDoc) & _
             " AND tm_anno = " & nAnno & _
             " AND tm_serie = " & CStrSQL(strSerie) & _
             " AND tm_numdoc = " & lNumdoc.ToString & _
             " ORDER BY tm_valuta, tm_scorpo, tm_tipork, tm_anno, tm_serie, tm_numdoc "
      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function DocConDataAnteriore(ByVal strDitta As String, ByVal strTipork As String, _
   ByVal nAnno As Integer, ByVal strSerie As String, _
   ByRef strDatdocOut As String, ByRef strTiporkOut As String) As Boolean
    Try
      'obsoleta
      Return DocConDataAnteriore(strDitta, strTipork, nAnno, strSerie, strDatdocOut, strTiporkOut, -1, "", "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function DocConDataAnteriore(ByVal strDitta As String, ByVal strTipork As String, _
                                                  ByVal nAnno As Integer, ByVal strSerie As String, _
                                                  ByRef strDatdocOut As String, ByRef strTiporkOut As String, _
                                                  ByVal lNumdoc As Integer, _
                                                  ByRef strDatdocOut1 As String, ByRef strTiporkOut1 As String) As Boolean
    Dim strSQL As String = ""
    Dim strElencoTipork As String = ""
    Dim dttTmp As New DataTable

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, nAnno, strSerie, strDatdocOut, strTiporkOut, lNumdoc, strDatdocOut1, strTiporkOut1})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strDatdocOut = NTSCStr(oIn(4))
        strTiporkOut = NTSCStr(oIn(5))
        strDatdocOut1 = NTSCStr(oIn(7))
        strTiporkOut1 = NTSCStr(oIn(8))
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strDatdocOut = ""
      strTiporkOut = ""
      strDatdocOut1 = ""
      strTiporkOut1 = ""
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTipork
        Case "A", "D", "E", "N", "£" : strElencoTipork = "'A', 'D', 'E', 'N', '£'"
        Case "L", "K", "J", "(" : strElencoTipork = "'L', 'K', 'J', '('"
        Case "F", "I" : strElencoTipork = "'F', 'I'"
        Case "S", "P" : strElencoTipork = "'S', 'P'"
      End Select
      '--------------------------------------------------------------------------------------------------------------

      If lNumdoc < 0 Then
        'vecchio sistema: se non indicato il num. documento, prendo su l'ultimo fatto
        strSQL = "SELECT TOP 1 tm_datdoc, tm_tipork FROM testmag" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tm_tipork IN (" & strElencoTipork & ")" & _
          " AND tm_anno = " & nAnno & _
          " AND tm_serie = " & CStrSQL(strSerie) & _
          " ORDER BY tm_numdoc DESC"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          strDatdocOut = NTSCStr(dttTmp.Rows(0)!tm_datdoc)
          strTiporkOut = NTSCStr(dttTmp.Rows(0)!tm_tipork)
          Return True
        End If
      Else
        'nuovo sistema: prendo su data e tipo documento del documento precedente e successivo di quello che sto facendo
        strSQL = "SELECT TOP 1 tm_datdoc, tm_tipork, tm_numdoc FROM testmag" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND tm_tipork IN (" & strElencoTipork & ")" & _
                  " AND tm_anno = " & nAnno & _
                  " AND tm_serie = " & CStrSQL(strSerie) & _
                  " AND tm_numdoc < " & lNumdoc & _
                  " ORDER BY tm_numdoc DESC"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          'documento precedente (numero più basso)
          strDatdocOut = NTSCStr(dttTmp.Rows(0)!tm_datdoc)
          strTiporkOut = NTSCStr(dttTmp.Rows(0)!tm_tipork)
        End If

        strSQL = strSQL.Replace(" AND tm_numdoc < ", " AND tm_numdoc > ")
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          'documento successivo (numero più alto)
          strDatdocOut1 = NTSCStr(dttTmp.Rows(0)!tm_datdoc)
          strTiporkOut1 = NTSCStr(dttTmp.Rows(0)!tm_tipork)
        End If
        Return True
      End If

      Return False

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function CalcolaSommaQtaDTTCollegati(ByVal strDitta As String, ByRef dttDTT As DataTable) As Decimal
    Dim dQuant As Decimal = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dttDTT.Rows.Count - 1)
        With dttDTT.Rows(i)
          strSQL = "SELECT SUM(mm_quant) AS QUANT FROM movmag" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND mm_tipork = " & CStrSQL(NTSCStr(!tm_tipork)) & _
            " AND mm_anno = " & NTSCInt(!tm_anno) & _
            " AND mm_serie = " & CStrSQL(NTSCStr(!tm_serie)) & _
            " AND mm_numdoc = " & NTSCInt(!tm_numdoc)
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
        If dttTmp.Rows.Count > 0 Then dQuant += NTSCDec(dttTmp.Rows(0)!QUANT)
        dttTmp.Clear()
        dttTmp.Dispose()
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return dQuant
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
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

  Public Overridable Function RitornaCodicePagamentoPerScaglioni(ByVal strDitta As String, _
  ByVal lConto As Integer, ByVal lImportoIn As Decimal, ByRef lImportoOut As Decimal, _
  ByVal nCodpagaIn As Integer, ByRef nCodpagaOut As Integer, ByRef strDespagaOut As String, _
  ByRef bInferioreAImportoMinimo As Boolean) As Boolean
    Try
      'obsoleta
      Return RitornaCodicePagamentoPerScaglioni(strDitta, lConto, lImportoIn, lImportoOut, nCodpagaIn, nCodpagaOut, strDespagaOut, bInferioreAImportoMinimo, 0, False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function
  Public Overridable Function RitornaCodicePagamentoPerScaglioni(ByVal strDitta As String, _
    ByVal lConto As Integer, ByVal lImportoIn As Decimal, ByRef lImportoOut As Decimal, _
    ByVal nCodpagaIn As Integer, ByRef nCodpagaOut As Integer, ByRef strDespagaOut As String, _
    ByRef bInferioreAImportoMinimo As Boolean, ByRef lImportoOut2 As Decimal, _
    ByRef bInferioreAImportoMassimo As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lConto, lImportoIn, lImportoOut, nCodpagaIn, nCodpagaOut, strDespagaOut, bInferioreAImportoMinimo, lImportoOut2, bInferioreAImportoMassimo})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        lImportoOut = NTSCDec(oIn(3))
        nCodpagaOut = NTSCInt(oIn(5))
        strDespagaOut = NTSCStr(oIn(6))
        bInferioreAImportoMinimo = CBool(oIn(7))
        lImportoOut2 = NTSCDec(oIn(8))
        bInferioreAImportoMassimo = CBool(oIn(9))
        Return CBool(oOut)
      End If
      '----------------


      '--------------------------------------------------------------------------------------------------------------
      '--- Per il conto di indicato in testata, guarda se la Rimanenza rientra negli scaglioni
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT an_codpag, an_codpaga2, an_codpaga3, an_codpagscagl1, an_codpagscagl2," & _
        " tabpaga_1.tb_despaga AS Despaga1, tabpaga_2.tb_despaga AS Despaga2, tabpaga_3.tb_despaga AS Despaga " & _
        " FROM anagra LEFT JOIN tabpaga AS tabpaga_1 ON anagra.an_codpaga2 = tabpaga_1.tb_codpaga" & _
        " LEFT JOIN tabpaga AS tabpaga_2 ON anagra.an_codpaga3 = tabpaga_2.tb_codpaga" & _
        " LEFT JOIN tabpaga AS tabpaga_3 ON anagra.an_codpag = tabpaga_3.tb_codpaga" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND an_conto = " & lConto & _
        " AND (an_codpagscagl1 <> 0 OR an_codpagscagl2 <> 0)"
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

End Class