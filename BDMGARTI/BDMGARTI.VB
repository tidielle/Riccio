Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGARTI
  Inherits CLD__BASE

  Public Overridable Function GetDataApri(ByVal strDitta As String, ByVal strQuery As String, _
                                          ByVal strCodArt As String, ByRef ds As DataSet) As Boolean

    Try
      Return GetDataApri(strDitta, strQuery, strCodArt, ds, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDataApri(ByVal strDitta As String, ByVal strQuery As String, _
                                          ByVal strCodArt As String, ByRef ds As DataSet, ByVal strOrderBy As String) As Boolean
    Dim strSQL As String = ""
    Dim strSQLTmp As String = ""
    Dim strOrdinamento As String = NTSCStr(GetSettingBus("BSMGARTI", "OPZIONI", ".", "OrdinamentoSelezione", "C", " ", "C"))
    Dim strTmp As String = ""

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strQuery, strCodArt, ds, strOrderBy})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(3), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT artico.*, tabmarc.tb_desmarc as xx_codmarc, tabpdon.tb_despdon as xx_codpdon," & _
        " tabappr.tb_desappr as xx_codappr, tabcena.tb_descena as xx_numecr," & _
        " anagra.an_descr1 as xx_forn, anagra2.an_descr1 as xx_forn2," & _
        " tabciva.tb_desciva as xx_codiva, tabgmer.tb_desgmer as xx_gruppo," & _
        " tabsgme.tb_dessgme as xx_sotgru, tabcove.tb_descove as xx_controp," & _
        " tabcove2.tb_descove as xx_controa, tabcove3.tb_descove as xx_contros," & _
        " tabcfam.tb_descfam as xx_famprod, tabcvuo.tb_descvuo as xx_codvuo," & _
        " tabmaga.tb_desmaga as xx_magstock, tabmaga2.tb_desmaga as xx_magprod," & _
        " tabimba.tb_desimba as xx_codimba, '' AS xx_cartric, '' AS xx_cartcanas, '' AS xx_cartcanol," & _
        " tabcsar.tb_descsar as xx_clascon, tabcpar.tb_descpar as xx_claprov, " & _
        " tabtcdc.tb_destcdc AS xx_codtcdc, tabdica.tb_desdica AS xx_coddica, " & _
        " tabdicv.tb_desdicv AS xx_coddicv, tablotx.tb_deslotx AS xx_codtlox, tabrear.tb_desrear AS xx_reparto," & _
        " tabseat.tb_desseat as xx_codseat" & _
        " FROM artico" & _
        " LEFT JOIN tabmarc ON artico.codditt = tabmarc.codditt AND artico.ar_codmarc = tabmarc.tb_codmarc" & _
        " LEFT JOIN tabpdon ON artico.codditt = tabpdon.codditt AND artico.ar_codpdon = tabpdon.tb_codpdon" & _
        " LEFT JOIN tabappr ON artico.codditt = tabappr.codditt AND artico.ar_codappr = tabappr.tb_codappr" & _
        " LEFT JOIN tabcena ON artico.codditt = tabcena.codditt AND artico.ar_numecr = tabcena.tb_codcena" & _
        " LEFT JOIN anagra ON artico.codditt = anagra.codditt AND artico.ar_forn = anagra.an_conto" & _
        " LEFT JOIN anagra as anagra2 ON artico.codditt = anagra2.codditt AND artico.ar_forn2 = anagra2.an_conto" & _
        " LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva" & _
        " LEFT JOIN tabgmer ON artico.codditt = tabgmer.codditt AND artico.ar_gruppo = tabgmer.tb_codgmer" & _
        " LEFT JOIN tabsgme ON artico.codditt = tabsgme.codditt AND artico.ar_sotgru = tabsgme.tb_codsgme" & _
        " LEFT JOIN tabcove ON artico.codditt = tabcove.codditt AND artico.ar_controp = tabcove.tb_codcove" & _
        " LEFT JOIN tabcove as tabcove2 ON artico.codditt = tabcove2.codditt AND artico.ar_controa = tabcove2.tb_codcove" & _
        " LEFT JOIN tabcove as tabcove3 ON artico.codditt = tabcove3.codditt AND artico.ar_contros = tabcove3.tb_codcove" & _
        " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam" & _
        " LEFT JOIN tabcvuo ON artico.codditt = tabcvuo.codditt AND artico.ar_codvuo = tabcvuo.tb_codcvuo" & _
        " LEFT JOIN tabmaga ON artico.codditt = tabmaga.codditt AND artico.ar_magstock = tabmaga.tb_codmaga" & _
        " LEFT JOIN tabmaga as tabmaga2 ON artico.codditt = tabmaga2.codditt AND artico.ar_magprod = tabmaga2.tb_codmaga" & _
        " LEFT JOIN tabimba ON artico.codditt = tabimba.codditt AND artico.ar_codimba = tabimba.tb_codimba" & _
        " LEFT JOIN tabcsar ON artico.codditt = tabcsar.codditt AND artico.ar_clascon = tabcsar.tb_codcsar" & _
        " LEFT JOIN tabcpar ON artico.codditt = tabcpar.codditt AND artico.ar_claprov = tabcpar.tb_codcpar" & _
        " LEFT JOIN tablotx ON artico.codditt = tablotx.codditt AND artico.ar_codtlox = tablotx.tb_codlotx" & _
        " LEFT JOIN tabtcdc ON artico.ar_codtcdc = tabtcdc.tb_codtcdc" & _
        " LEFT JOIN tabdica ON artico.ar_coddica = tabdica.tb_coddica" & _
        " LEFT JOIN tabdicv ON artico.codditt = tabdicv.codditt AND artico.ar_coddica = tabdicv.tb_coddica AND artico.ar_coddicv = tabdicv.tb_coddicv" & _
        " LEFT JOIN tabrear ON artico.codditt = tabrear.codditt AND artico.ar_reparto = tabrear.tb_codrear " & _
        " LEFT JOIN tabseat ON artico.codditt = tabseat.codditt AND artico.ar_codseat = tabseat.tb_codseat "


      If strQuery <> "" Then

        strSQL = strSQL & " WHERE artico.codditt = " & CStrSQL(strDitta) & _
          " AND artico.ar_gesvar <> 'S'"
        'aggiungo la where dei campi con OR e AND rimappati
        TraduciWhere(strQuery, strSQLTmp)

        strSQL = strSQL & Replace(strSQLTmp, " (ar_", " (artico.ar_")
      Else
        strSQL = strSQL & " WHERE artico.codditt = " & CStrSQL(strDitta) & _
          " AND artico.ar_codart = " & CStrSQL(strCodArt)
      End If
      If strOrderBy.Trim = "" Then
        Select Case strOrdinamento
          Case "C" : strSQL += " ORDER BY artico.ar_codart"
          Case "D" : strSQL += " ORDER BY artico.ar_descr"
        End Select
      Else
        strSQL &= "ORDER BY " & strOrderBy
      End If


      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      '--------------------------------------------------------------------------------------------------------------
      If ds.Tables("ARTICO").Rows.Count > 0 Then
        For i As Integer = 0 To (ds.Tables("ARTICO").Rows.Count - 1)
          With ds.Tables("ARTICO").Rows(i)
            If Not !ar_cartric.Equals(DBNull.Value) Then
              If ValCodiceDb(NTSCStr(!ar_cartric), strDitta, "ARTICO", "S", strTmp) = True Then !xx_cartric = strTmp
            End If
            If Not !ar_cartcanas.Equals(DBNull.Value) Then
              If ValCodiceDb(NTSCStr(!ar_cartcanas), strDitta, "ARTICO", "S", strTmp) = True Then !xx_cartcanas = strTmp
            End If
            If Not !ar_cartcanol.Equals(DBNull.Value) Then
              If ValCodiceDb(NTSCStr(!ar_cartcanol), strDitta, "ARTICO", "S", strTmp) = True Then !xx_cartcanol = strTmp
            End If
          End With
        Next
        ds.Tables("ARTICO").AcceptChanges()
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataNuovo(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artico.*, tabmarc.tb_desmarc as xx_codmarc, tabpdon.tb_despdon as xx_codpdon," & _
        " tabappr.tb_desappr as xx_codappr, tabcena.tb_descena as xx_numecr," & _
        " anagra.an_descr1 as xx_forn, anagra2.an_descr1 as xx_forn2," & _
        " tabciva.tb_desciva as xx_codiva, tabgmer.tb_desgmer as xx_gruppo," & _
        " tabsgme.tb_dessgme as xx_sotgru, tabcove.tb_descove as xx_controp," & _
        " tabcove2.tb_descove as xx_controa, tabcove3.tb_descove as xx_contros," & _
        " tabcfam.tb_descfam as xx_famprod, tabcvuo.tb_descvuo as xx_codvuo," & _
        " tabmaga.tb_desmaga as xx_magstock, tabmaga2.tb_desmaga as xx_magprod," & _
        " tabimba.tb_desimba as xx_codimba, artico2.ar_descr as xx_cartric," & _
        " artico3.ar_descr as xx_cartcanas, artico4.ar_descr as xx_cartcanol," & _
        " tabcsar.tb_descsar as xx_clascon, tabcpar.tb_descpar as xx_claprov," & _
        " tabtcdc.tb_destcdc AS xx_codtcdc, tabdica.tb_desdica AS xx_coddica, " & _
        " tabdicv.tb_desdicv AS xx_coddicv, tablotx.tb_deslotx AS xx_codtlox, tabrear.tb_desrear AS xx_reparto," & _
        " tabseat.tb_desseat as xx_codseat" & _
        " FROM (((((((((((((((((((((artico" & _
        " LEFT JOIN tabmarc ON artico.codditt = tabmarc.codditt AND artico.ar_codmarc = tabmarc.tb_codmarc)" & _
        " LEFT JOIN tabpdon ON artico.codditt = tabpdon.codditt AND artico.ar_codpdon = tabpdon.tb_codpdon)" & _
        " LEFT JOIN tabappr ON artico.codditt = tabappr.codditt AND artico.ar_codappr = tabappr.tb_codappr)" & _
        " LEFT JOIN tabcena ON artico.codditt = tabcena.codditt AND artico.ar_numecr = tabcena.tb_codcena)" & _
        " LEFT JOIN anagra ON artico.codditt = anagra.codditt AND artico.ar_forn = anagra.an_conto)" & _
        " LEFT JOIN anagra as anagra2 ON artico.codditt = anagra2.codditt AND artico.ar_forn2 = anagra2.an_conto)" & _
        " LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva)" & _
        " LEFT JOIN tabgmer ON artico.codditt = tabgmer.codditt AND artico.ar_gruppo = tabgmer.tb_codgmer)" & _
        " LEFT JOIN tabsgme ON artico.codditt = tabsgme.codditt AND artico.ar_sotgru = tabsgme.tb_codsgme)" & _
        " LEFT JOIN tabcove ON artico.codditt = tabcove.codditt AND artico.ar_controp = tabcove.tb_codcove)" & _
        " LEFT JOIN tabcove as tabcove2 ON artico.codditt = tabcove2.codditt AND artico.ar_controa = tabcove2.tb_codcove)" & _
        " LEFT JOIN tabcove as tabcove3 ON artico.codditt = tabcove3.codditt AND artico.ar_contros = tabcove3.tb_codcove)" & _
        " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam)" & _
        " LEFT JOIN tabcvuo ON artico.codditt = tabcvuo.codditt AND artico.ar_codvuo = tabcvuo.tb_codcvuo)" & _
        " LEFT JOIN tabmaga ON artico.codditt = tabmaga.codditt AND artico.ar_magstock = tabmaga.tb_codmaga)" & _
        " LEFT JOIN tabmaga as tabmaga2 ON artico.codditt = tabmaga2.codditt AND artico.ar_magprod = tabmaga2.tb_codmaga)" & _
        " LEFT JOIN tabimba ON artico.codditt = tabimba.codditt AND artico.ar_codimba = tabimba.tb_codimba)" & _
        " LEFT JOIN artico as artico2 ON artico.codditt = artico2.codditt AND artico.ar_cartric = artico2.ar_codart)" & _
        " LEFT JOIN artico as artico3 ON artico.codditt = artico3.codditt AND artico.ar_cartcanas = artico3.ar_codart)" & _
        " LEFT JOIN artico as artico4 ON artico.codditt = artico4.codditt AND artico.ar_cartcanol = artico4.ar_codart)" & _
        " LEFT JOIN tabcsar ON artico.codditt = tabcsar.codditt AND artico.ar_clascon = tabcsar.tb_codcsar)" & _
        " LEFT JOIN tabcpar ON artico.codditt = tabcpar.codditt AND artico.ar_claprov = tabcpar.tb_codcpar" & _
        " LEFT JOIN tablotx ON artico.codditt = tablotx.codditt AND artico.ar_codtlox = tablotx.tb_codlotx" & _
        " LEFT JOIN tabtcdc ON artico.ar_codtcdc = tabtcdc.tb_codtcdc" & _
        " LEFT JOIN tabdica ON artico.ar_coddica = tabdica.tb_coddica" & _
        " LEFT JOIN tabdicv ON artico.codditt = tabdicv.codditt AND artico.ar_coddica = tabdicv.tb_coddica AND artico.ar_coddicv = tabdicv.tb_coddicv " & _
        " LEFT JOIN tabrear ON artico.codditt = tabrear.codditt AND artico.ar_reparto = tabrear.tb_codrear " & _
        " LEFT JOIN tabseat ON artico.codditt = tabseat.codditt AND artico.ar_codseat = tabseat.tb_codseat "

      strSQL = strSQL & " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND artico.ar_codart = 'zzzzzzzzzzzzzzzzzz'"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetTabTipa1(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_codtipa FROM tabtipa" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_codtipa = 1" & _
        " ORDER BY codditt, tb_codtipa"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABTIPA")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTabTipa(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_codtipa, tb_destipa FROM tabtipa" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " ORDER BY codditt, tb_codtipa"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABTIPA")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArticoCodarfo(ByVal strDitta As String, ByVal strSecfCodarfo As String, _
                                               ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 codarfo.caf_codart" & _
        " FROM codarfo INNER JOIN artico ON (codarfo.codditt = artico.codditt) AND (codarfo.caf_codart = artico.ar_codart)" & _
        " WHERE codarfo.codditt = " & CStrSQL(strDitta) & _
        " AND caf_codarfo = " & CStrSQL(strSecfCodarfo) & _
        " ORDER BY codarfo.codditt, caf_conto, caf_codart"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckCodbar(ByVal strDitta As String, ByVal strCodbar As String, _
                                             ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT bc_codart FROM barcode" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND bc_code = " & CStrSQL(strCodbar)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "BARCODE")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTabNuma(ByVal strDitta As String, ByVal strCodart As String, _
                                         ByVal nLungRoot As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_numprog FROM tabnuma" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo = 'AA'" & _
        " AND tb_numserie = '" & Left(strCodart, nLungRoot) & "'" & _
        " AND tb_numcodl = 0" & _
        " ORDER BY codditt, tb_numtipo, tb_numserie, tb_numcodl"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMA")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArtico(ByVal strDitta As String, ByVal strCodart As String, _
                                        ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart) & _
        " ORDER BY codditt, ar_codart"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArtest(ByVal strDitta As String, ByVal strCodart As String, _
                                      ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM artest" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ae_codartf = " & CStrSQL(strCodart) & _
        " ORDER BY codditt, ae_codartf, ae_forn, ae_codmarc"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTEST")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ControllaConfigurazioni(ByVal strDitta As String, ByVal strCodart As String, _
                                    ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT co_codart FROM composix" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND co_codconf = " & CStrSQL(strCodart) & _
        " ORDER BY codditt, co_codart"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "COMPOSIX")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ControllaArticolo(ByVal strDitta As String, ByVal strCodart As String, _
                                  ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT db_coddb FROM distbas" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND db_coddb = " & CStrSQL(strCodart) & _
        " ORDER BY codditt, db_coddb"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DISTBAS")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaListini(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      'Per i listini (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      strSQL = "SELECT DISTINCT lc_conto " & _
        " FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codcas = ' '" & _
        " AND lc_conto > 0 " & _
        " ORDER BY lc_conto"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      For i = 0 To dsTmp.Tables("LISTINI").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("LISTINI").Rows(i)!lc_conto))
      Next

      strSQL = "DELETE FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codcas = ' '"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function UpdateTimeStampConto(ByVal strDitta As String, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      'Aggiorna il timestamp di anagra (an_ultagg) con la data di sistema
      'Opera su gdb o gcn a seconda della connessione
      strSQL = "UPDATE anagra SET an_ultagg = GETDATE() " & _
               "WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lConto
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaSconti(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      'Per gli sconti (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      strSQL = "SELECT DISTINCT so_conto " & _
        " FROM sconti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND so_codart = " & CStrSQL(strCodart) & _
        " AND so_clscar = 0 " & _
        " AND so_conto > 0 " & _
        " ORDER BY so_conto"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SCONTI")

      For i = 0 To dsTmp.Tables("SCONTI").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("SCONTI").Rows(i)!so_conto))
      Next

      strSQL = "DELETE FROM sconti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND so_codart = " & CStrSQL(strCodart) & _
        " AND so_codart <> ' '" & _
        " AND so_clscar = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaProvvigioni(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      'Per le provvigioni (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      strSQL = "SELECT DISTINCT per_conto " & _
        " FROM perprov" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND per_codart = " & CStrSQL(strCodart) & _
        " AND per_conto > 0 " & _
        " ORDER BY per_conto"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PERPROV")

      For i = 0 To dsTmp.Tables("PERPROV").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("PERPROV").Rows(i)!per_conto))
      Next

      strSQL = "DELETE FROM perprov" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND per_codart = " & CStrSQL(strCodart) & _
        " AND per_codart <> ' '" & _
        " AND per_clprar = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaCodarfo(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      'Per i listini (da cancellare) speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      strSQL = "SELECT DISTINCT caf_conto " & _
        " FROM codarfo" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND caf_codart = " & CStrSQL(strCodart) & _
        " AND caf_conto > 0 " & _
        " ORDER BY caf_conto"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CODARFO")

      For i = 0 To dsTmp.Tables("CODARFO").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("CODARFO").Rows(i)!caf_conto))
      Next

      strSQL = "DELETE FROM codarfo" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND caf_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaBarcode(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM barcode" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND bc_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaArtmaga(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artmaga" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND am_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaKit(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artkit" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ak_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaConai(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artcona" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ak_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaTipologia(ByVal strDitta As String, ByVal nCodtipa As Integer, _
                                                ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      '--- Controlla se esiste una tabella associata al codice tipologia
      strSQL = " SELECT tb_nomtabcs FROM tabtipa" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_codtipa = " & nCodtipa & _
        " ORDER BY codditt, tb_codtipa"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABTIPA")

      For i = 0 To dsTmp.Tables("TABTIPA").Rows.Count - 1
        With dsTmp.Tables("TABTIPA").Rows(i)
          If Not (!tb_nomtabcs Is System.DBNull.Value) Then
            If Not (Trim(NTSCStr(!tb_nomtabcs)) = "") Then
              strSQL = "DELETE FROM " & NTSCStr(!tb_nomtabcs) & _
                " WHERE arcs_codart = " & CStrSQL(strCodart)
              If NTSCStr(!tb_nomtabcs) <> "" Then
                If IsPerDitta(True, NTSCStr(!tb_nomtabcs)) = True Then
                  strSQL = strSQL & " AND codditt = " & CStrSQL(strDitta)
                End If
              End If
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            End If
          End If
        End With
      Next
      '--- Cancella i record da VALARTI
      strSQL = "DELETE FROM valarti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND alv_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaArtfasi(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artfasi" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND af_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaConfigurazioniCP2(ByVal strDitta As String, ByVal strCodart As String, _
                                                        ByVal strTipoOpz As String) As Boolean
    Dim strSQL As String = ""
    Try
      Select Case strTipoOpz
        Case "B", "Q"
          strSQL = " DELETE FROM listini" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " And lc_codart = " & CStrSQL(strCodart) & _
            " And lc_codcas <> ' '"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

          strSQL = "DELETE FROM casellx" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ca_codconf = " & CStrSQL(strCodart)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

          strSQL = "DELETE FROM artconf" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ac_codart = " & CStrSQL(strCodart)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        Case Else
          strSQL = "DELETE FROM compcax" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND cca_codart = " & CStrSQL(strCodart)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

          strSQL = "DELETE FROM composix" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND co_codart = " & CStrSQL(strCodart)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End Select

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaConfigurazioniCP3(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM compchp" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND cch_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM composip" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND co_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM movdimot " & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mdc_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM composis " & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND cs_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaLotcpro(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM lotcpro" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lp_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaLotcdef(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM lotcdef" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ld_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaArtacce(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artacce" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND apa_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM artacce" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND apa_codart <> " & CStrSQL(strCodart) & _
        " AND apa_codartas = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaArtprom(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artprom" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND apr_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaArtval(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artval" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ax_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function UpdateArtest(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE artest" & _
        " SET ae_codart = NULL," & _
        " ae_ultagg = " & CDataOraSQL(DateTime.Now.ToString) & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ae_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaListini(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      strSQL = "INSERT INTO listini (codditt, lc_codart, lc_codlavo, lc_conto, lc_codvalu," & _
        " lc_codtpro, lc_listino, lc_datagg, lc_tipo, lc_prezzo, lc_datscad, lc_daquant," & _
        " lc_aquant, lc_perqta, lc_unmis, lc_note, lc_netto, lc_fase, lc_ultagg, lc_codcas, lc_coddest)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & "," & _
        " lc_codlavo, lc_conto, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_tipo," & _
        " lc_prezzo, lc_datscad, lc_daquant, lc_aquant, lc_perqta, lc_unmis, lc_note," & _
        " lc_netto, lc_fase, " & CDataOraSQL(Now) & ", ' ', lc_coddest" & _
        " FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodartOld) & _
        " AND lc_codcas = ' '" & _
        " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_listino, lc_codvalu, lc_codtpro," & _
        " lc_datagg DESC, lc_daquant, lc_fase"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'Per i listini aggiunti speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      strSQL = "SELECT DISTINCT lc_conto " & _
        " FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodartOld) & _
        " AND lc_codcas = ' '" & _
        " AND lc_conto > 0 " & _
        " ORDER BY lc_conto"

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      For i = 0 To dsTmp.Tables("LISTINI").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("LISTINI").Rows(i)!lc_conto))
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function EliminaListiniNonValidiPerUnMis(ByVal strDitta As String, ByVal strCodart As String, ByVal strUnMis As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM listini " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND lc_codart = " & CStrSQL(strCodart) & _
               "   AND lc_unmis = " & CStrSQL(strUnMis)

      Return Execute(strSQL, CLE__APP.DBTIPO.DBAZI) > 0 'Ritorna true solo se ha effettivamente cancellato dei record.
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function AggiornaSconti(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      strSQL = "INSERT INTO sconti (codditt, so_codart, so_conto, so_coddest, so_clscan, so_clscar," & _
        " so_scont1, so_scont2, so_scont3, so_datagg, so_datscad, so_codtpro, so_daquant," & _
        " so_aquant, so_scont4, so_scont5, so_scont6, so_unmis, so_fase, so_ultagg)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & ", so_conto, so_coddest, so_clscan," & _
        " so_clscar, so_scont1, so_scont2, so_scont3, so_datagg, so_datscad, so_codtpro," & _
        " so_daquant, so_aquant, so_scont4, so_scont5, so_scont6, so_unmis, so_fase, " & _
        CDataOraSQL(Now) & _
        " FROM sconti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND so_codart = " & CStrSQL(strCodartOld) & _
        " AND so_clscar = 0" & _
        " ORDER BY codditt, so_codart, so_conto, so_coddest, so_clscan, so_clscar, so_datagg," & _
        " so_codtpro, so_daquant"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'Per i listini aggiunti speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      strSQL = "SELECT DISTINCT so_conto " & _
        " FROM sconti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND so_codart = " & CStrSQL(strCodartOld) & _
        " AND so_clscar = 0" & _
        " AND so_conto > 0 " & _
        " ORDER BY so_conto"

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SCONTI")

      For i = 0 To dsTmp.Tables("SCONTI").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("SCONTI").Rows(i)!so_conto))
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaProvvigioni(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      strSQL = "INSERT INTO perprov (codditt, per_codcage, per_codart, per_conto, per_coddest, per_clpran," & _
       " per_clprar, per_provv, per_datagg, per_datscad, per_codtpro, per_vprovv, per_unmis," & _
       " per_ultagg)" & _
       " SELECT codditt, per_codcage, " & CStrSQL(strCodartNew) & ", per_conto,per_coddest," & _
       " per_clpran, per_clprar, per_provv, per_datagg, per_datscad, per_codtpro, per_vprovv," & _
       " per_unmis, " & CDataOraSQL(Now) & _
       " FROM perprov" & _
       " WHERE codditt = " & CStrSQL(strDitta) & _
       " AND per_codart = " & CStrSQL(strCodartOld) & _
       " AND per_clprar = 0" & _
       " ORDER BY codditt, per_codcage, per_codart, per_conto, per_coddest, per_clpran, per_clprar," & _
       " per_datagg, per_codtpro"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'Per i listini aggiunti speciali per cliente aggiorna il timestamp su anagrta.an_ultagg
      strSQL = "SELECT DISTINCT per_conto " & _
        " FROM perprov" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND per_codart = " & CStrSQL(strCodartOld) & _
        " AND per_clprar = 0" & _
        " AND per_conto > 0 " & _
        " ORDER BY per_conto"

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PERPROV")

      For i = 0 To dsTmp.Tables("PERPROV").Rows.Count - 1
        UpdateTimeStampConto(strDitta, NTSCInt(dsTmp.Tables("PERPROV").Rows(i)!per_conto))
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaArtval(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO artval (codditt, ax_codart, ax_codvalu, ax_descr, ax_desint," & _
        " ax_ultagg, ax_note)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & ", ax_codvalu, ax_descr," & _
        " ax_desint, " & CDataOraSQL(Now) & ", ax_note" & _
        " FROM artval" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ax_codart = " & CStrSQL(strCodartOld) & _
        " ORDER BY codditt, ax_codart, ax_codvalu"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaKit(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO artkit (codditt, ak_codart, ak_riga, ak_codfigli, ak_unmis," & _
        " ak_colli, ak_ump, ak_quant, ak_desfigli)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & ", ak_riga, ak_codfigli," & _
        " ak_unmis, ak_colli, ak_ump, ak_quant, ak_desfigli" & _
        " FROM artkit" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ak_codart = " & CStrSQL(strCodartOld) & _
        " ORDER BY codditt, ak_codart, ak_riga, ak_codfigli"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaConai(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO artcona (codditt, ak_codart, ak_riga, ak_codfigli, ak_ump," & _
        " ak_quant, ak_desfigli)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & ", ak_riga, ak_codfigli," & _
        " ak_ump, ak_quant, ak_desfigli" & _
        " FROM artcona" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ak_codart = " & CStrSQL(strCodartOld) & _
        " ORDER BY codditt, ak_codart, ak_riga, ak_codfigli"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaArtMaga(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO artmaga (codditt, am_codart, am_codmaga, am_ubicaz, " & _
               "      am_scomin, am_scomax, am_minord, am_polriord, " & _
               "      am_ultagg, am_opnome, am_sublotto, am_maxlotto,   " & _
               "      am_ggragg, am_perragg, am_scosic, am_fase,  " & _
               "      am_ubicst, am_ubicpr, am_ubicri, am_ubicus,  " & _
               "      am_indrot, am_scominpk) " & _
               " SELECT codditt, " & CStrSQL(strCodartNew) & ", am_codmaga, am_ubicaz, am_scomin,  " & _
               "	   am_scomax, am_minord, am_polriord, am_ultagg, am_opnome,  " & _
               "	   am_sublotto, am_maxlotto, am_ggragg, am_perragg, am_scosic, " & _
               "	   am_fase, am_ubicst, am_ubicpr, am_ubicri, am_ubicus,  " & _
               "     am_indrot, am_scominpk  " & _
               " FROM artmaga " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND am_codart = " & CStrSQL(strCodartOld)


      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function AggiornaArtfasi(ByVal strDitta As String, ByVal strCodartNew As String, ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO artfasi (codditt, af_codart, af_fase, af_descr, af_codlavo," & _
        " af_ultagg, af_opnome, af_livmindb, af_rrfence, af_fcorrlt, af_makebuy)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & ", af_fase, af_descr," & _
        " af_codlavo, " & CDataOraSQL(Now) & ", af_opnome, af_livmindb," & _
        " af_rrfence, af_fcorrlt, af_makebuy" & _
        " FROM artfasi" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND af_codart = " & CStrSQL(strCodartOld) & _
        " ORDER BY codditt, af_codart, af_fase"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiornaValarti(ByVal strDitta As String, ByVal strCodartNew As String, _
    ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO valarti (codditt, alv_codart, alv_ncampo, alv_index, alv_valdouble, alv_tipcamp," & _
        " alv_codtipa, alv_desval, alv_descamp, alv_multi, alv_codvari, alv_linkpr, alv_linkric, alv_gestling," & _
        " alv_codling, alv_valvari, alv_desvvar, alv_codartl, alv_flcamporic, alv_flurl)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & ", alv_ncampo, alv_index, 0, alv_tipcamp, alv_codtipa," & _
        " NULL, alv_descamp, alv_multi, alv_codvari, alv_linkpr, alv_linkric, alv_gestling, 0, NULL," & _
        " NULL, alv_codartl, alv_flcamporic, alv_flurl" & _
        " FROM valarti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND alv_codart = " & CStrSQL(strCodartOld) & _
        " ORDER BY codditt, alv_codart, alv_ncampo, alv_index"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function AggiornaValarti_ParametriValori(ByVal strDitta As String, ByVal strCodartNew As String, _
  ByVal strCodartOld As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO valarti (codditt, alv_codart, alv_ncampo, alv_index, alv_valdouble, alv_valdata," & _
        " alv_valtext, alv_valcombo, alv_tipcamp, alv_codtipa, alv_desval, alv_descamp, alv_multi, alv_codvari," & _
        " alv_linkpr, alv_linkric, alv_gestling, alv_codling, alv_valvari, alv_desvvar, alv_codartl, alv_flcamporic," & _
        " alv_flurl)" & _
        " SELECT codditt, " & CStrSQL(strCodartNew) & ", alv_ncampo, alv_index, alv_valdouble, alv_valdata," & _
        " alv_valtext, alv_valcombo, alv_tipcamp, alv_codtipa, alv_desval, alv_descamp, alv_multi, alv_codvari," & _
        " alv_linkpr, alv_linkric, alv_gestling, alv_codling, alv_valvari, alv_desvvar, alv_codartl, alv_flcamporic," & _
        " alv_flurl" & _
        " FROM valarti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND alv_codart = " & CStrSQL(strCodartOld) & _
        " ORDER BY codditt, alv_codart, alv_ncampo, alv_index"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetDataApro(ByVal strDitta As String, ByVal strAproCodart As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artprom.*, tabtpro.tb_destpro as xx_codtpro, tabdpro.tb_desdpro as xx_coddpro" & _
        " FROM (artprom " & _
        " LEFT JOIN tabtpro ON artprom.codditt = tabtpro.codditt AND artprom.apr_codtpro = tabtpro.tb_codtpro)" & _
        " LEFT JOIN tabdpro ON artprom.codditt = tabdpro.codditt AND artprom.apr_coddpro = tabdpro.tb_coddpro" & _
        " WHERE artprom.codditt = " & CStrSQL(strDitta) & _
        " AND apr_codart = " & CStrSQL(strAproCodart) & _
        " ORDER BY artprom.codditt, apr_codart, apr_codtpro"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPROM")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataArta(ByVal strDitta As String, ByVal strArtaCodart As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artacce.*, artico.ar_descr as xx_codartas" & _
        " FROM artacce " & _
        " LEFT JOIN artico ON artacce.codditt = artico.codditt AND artacce.apa_codartas = artico.ar_codart" & _
        " WHERE artacce.codditt = " & CStrSQL(strDitta) & _
        " AND apa_codart = " & CStrSQL(strArtaCodart) & _
        " ORDER BY artacce.codditt, apa_codart, apa_codartas"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTACCE")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataCona(ByVal strDitta As String, ByVal strConaCodart As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artcona.*, artico.ar_descr as xx_codfigli" & _
        " FROM artcona " & _
        " LEFT JOIN artico ON artcona.codditt = artico.codditt AND artcona.ak_codfigli = artico.ar_codart" & _
        " WHERE artcona.codditt = " & CStrSQL(strDitta) & _
        " AND ak_codart = " & CStrSQL(strConaCodart) & _
        " ORDER BY artcona.codditt, ak_riga"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTCONA")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetMaxArtcona(ByVal strDitta As String, ByVal strCodcona As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Max(ak_riga) As Riga FROM artcona" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ak_codart = " & CStrSQL(strCodcona)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTCONA")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetArtfasi(ByVal strDitta As String, ByVal strCodart As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 af_fase FROM artfasi" & _
        " WHERE artfasi.codditt = " & CStrSQL(strDitta) & _
        " AND af_codart = " & CStrSQL(strCodart) & _
        " ORDER BY artfasi.codditt, af_codart, af_fase"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTFASI")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CheckAttributi(ByVal strDitta As String, ByVal strCodtipa As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ct_codtipa, ct_ncampo FROM camtipa" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND camtipa.ct_codtipa = " & strCodtipa

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CAMTIPA")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataVala(ByVal strDitta As String, ByVal strValaCodart As String, _
                                        ByVal nValaCodtipa As Integer, ByRef strError As String, _
                                        ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim strSQLSelect As String = ""
    Dim i As Integer
    Dim strTmp As String = ""
    Dim dsTmp As DataSet = Nothing
    Try
      '***************************************************************************
      ' Riempio la tab. temp. TTVALARTI per la gestione degli attributi multipli

      strSQLSelect = " SELECT valarti.codditt, alv_codart, alv_ncampo, alv_index, alv_valdouble," & _
          " alv_valdata, alv_valtext, alv_valcombo, alv_tipcamp, alv_codtipa, alv_desval," & _
          " alv_descamp, alv_multi, alv_codvari, alv_linkpr, alv_linkric, alv_gestling," & _
          " alv_codling, alv_valvari, alv_desvvar, alv_codartl, ar_descr as xx_codartl, tb_desvari as xx_codvari," & _
          " alv_flcamporic, alv_flurl" & _
          " FROM (valarti LEFT JOIN artico ON (valarti.codditt = artico.codditt) AND (valarti.alv_codartl = artico.ar_codart)) LEFT JOIN tabvari ON (valarti.codditt = tabvari.codditt) AND (valarti.alv_codvari = tabvari.tb_codvari)" & _
          " WHERE valarti.codditt = " & CStrSQL(strDitta) & _
          " AND alv_codart = " & CStrSQL(strValaCodart) & _
          " AND alv_codtipa = " & nValaCodtipa

      ds = OpenRecordset(strSQLSelect, CLE__APP.DBTIPO.DBAZI, "VALARTI")

      'se non presente record in valvari lo crea
      If ds.Tables("VALARTI").Rows.Count = 0 Then
        strSQL = "INSERT INTO valarti (codditt, alv_codart, alv_ncampo," & _
            " alv_index, alv_valdouble, alv_tipcamp, alv_codtipa, alv_descamp, alv_multi," & _
            " alv_codvari, alv_linkpr, alv_linkric, alv_gestling, alv_codling, " & _
            " alv_flcamporic, alv_flurl)" & _
            " SELECT " & CStrSQL(strDitta) & ", " & _
            CStrSQL(strValaCodart) & ", ct_ncampo, 0, 0, ct_tipcamp, " & _
            nValaCodtipa & ", ct_descamp, ct_multi, ct_codvari, ct_linkpr, ct_linkric," & _
            " ct_gestling, 0, ct_flcamporic, ct_flurl" & _
            " FROM camtipa LEFT JOIN tabvari ON (camtipa.codditt = tabvari.codditt) AND (camtipa.ct_codvari = tabvari.tb_codvari)" & _
            " WHERE camtipa.codditt = " & CStrSQL(strDitta) & _
            " AND camtipa.ct_codtipa = " & nValaCodtipa & _
            " AND camtipa.ct_multi = 'N'"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        strSQL = "INSERT INTO valarti (codditt, alv_codart, alv_ncampo," & _
          " alv_index, alv_valdouble, alv_tipcamp, alv_codtipa, alv_descamp, alv_multi," & _
          " alv_codvari, alv_linkpr, alv_linkric, alv_gestling, alv_codling," & _
          " alv_flcamporic, alv_flurl)" & _
          " SELECT " & CStrSQL(strDitta) & ", " & _
          CStrSQL(strValaCodart) & ", ct_ncampo, 0, 0, ct_tipcamp, " & _
          nValaCodtipa & ", ct_descamp, 'F', ct_codvari, ct_linkpr, ct_linkric," & _
          " ct_gestling, 0, ct_flcamporic, ct_flurl" & _
          " FROM camtipa LEFT JOIN tabvari ON (camtipa.codditt = tabvari.codditt) AND (camtipa.ct_codvari = tabvari.tb_codvari)" & _
          " WHERE camtipa.codditt = " & CStrSQL(strDitta) & _
          " AND camtipa.ct_codtipa = " & nValaCodtipa & _
          " AND camtipa.ct_multi = 'S'"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        ds = OpenRecordset(strSQLSelect, CLE__APP.DBTIPO.DBAZI, "VALARTI")
      End If

      'aggiungo colonne unbound
      ds.Tables("VALARTI").Columns.Add(New DataColumn("xx_codling", GetType(String)))
      ds.Tables("VALARTI").Columns.Add(New DataColumn("xx_tipovalore", GetType(String)))
      ds.Tables("VALARTI").Columns.Add(New DataColumn("xx_valore", GetType(String)))
      ds.Tables("VALARTI").Columns.Add(New DataColumn("xx_valorecmb", GetType(String)))
      ds.Tables("VALARTI").Columns.Add(New DataColumn("xx_destipa", GetType(String)))
      ds.Tables("VALARTI").Columns.Add(New DataColumn("xx_valore2", GetType(String))) 'utilizzata in val2
      ds.Tables("VALARTI").Columns.Add(New DataColumn("xx_valcombo", GetType(String)))

      'controlla la presenza del codice lingua e aggiorna la descrizione lingua
      For i = 0 To ds.Tables("VALARTI").Rows.Count - 1
        strSQL = "SELECT tabling.* FROM tabling WHERE tb_codling = " & NTSCStr(ds.Tables("VALARTI").Rows(i)!alv_codling)
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "TABLING")
        If dsTmp.Tables("TABLING").Rows.Count = 0 Then
          strError = "Attenzione! Nessun dato trovato nella tabella tabling di ARCPROC. Inserire almeno una riga con descrizione in lingua."
          Return False
        End If

        ds.Tables("VALARTI").Rows(i)!xx_codling = strTmp
        ds.Tables("VALARTI").Rows(i)!xx_valcombo = NTSCStr(ds.Tables("VALARTI").Rows(i)!alv_ncampo) & "." & NTSCStr(ds.Tables("VALARTI").Rows(i)!alv_valcombo)
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetMaxValarti(ByVal strDitta As String, ByVal strCampo As String, ByRef ds As DataSet) As Boolean
    Try
      Return GetMaxValarti(strDitta, "", strCampo, ds)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetMaxValarti(ByVal strDitta As String, ByVal strCodart As String, _
                                            ByVal strCampo As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, strCampo, ds})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(3), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT Max(alv_index) as Indice FROM valarti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND alv_codart = " & CStrSQL(strCodart) & _
        " AND alv_ncampo = " & strCampo & _
        " AND alv_multi = 'S'"
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "VALARTI")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetCamtipa(ByVal strDitta As String, ByVal strCampo As String, _
                                            ByVal strValaCodtipa As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT camtipa.* FROM camtipa" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ct_codtipa = " & strValaCodtipa & _
        " AND ct_ncampo = " & strCampo

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CAMTIPA")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function


  Public Overridable Function ValidaValVari(ByVal strDitta As String, ByVal strValVari As String, ByVal strCodVari As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT vv_desvvar FROM valvari " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND vv_codvari = " & CStrSQL(strCodVari) & _
               " AND vv_valvari = " & CStrSQL(strValVari)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function ElencoAttributi(ByVal strDitta As String, ByVal nValaCodtipa As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      'Seleziono da camtipa i record relativi al codice tipologia articoli dell'articolo
      strSQL = "SELECT * FROM camtipa" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ct_codtipa = " & nValaCodtipa

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CaricaTipoCombo(ByVal strDitta As String, ByVal lTipa As Integer, ByRef dttOut As datatable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT vt_valore as codex, vt_desval as val, cast(vt_ncampo  as varchar(5)) + '.' + vt_valore as cod FROM comtipa " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND vt_codtipa = " & lTipa

      dttOut = openrecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function GetArticoCmb(ByVal strDitta As String, ByVal lCampo As Integer, ByVal lTipa As Integer, ByRef dttOut As datatable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT vt_valore as codex, vt_desval as val, cast(vt_ncampo  as varchar(5)) + '.' + vt_valore as cod FROM comtipa " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND vt_ncampo = " & lCampo & _
               " AND vt_codtipa = " & lTipa

      dttOut = openrecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

#Region "Logistica su palmare"
  Public Overridable Function GetRelease(ByVal strDitta As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT rel_maior, rel_pers FROM release" & _
        " WHERE UPPER(rel_sw) = 'BUSINESS'"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
#End Region

  Public Overridable Function GetComtipa(ByVal strDitta As String, ByVal lCampo As Integer, _
                                         ByVal lTipa As Integer, ByVal strCodVal As String, _
                                         ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 vt_desval, vt_valore FROM comtipa" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND vt_codtipa = " & lTipa & _
        " AND vt_ncampo = " & lCampo & _
        " AND vt_valore = " & CStrSQL(strCodVal)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetComtipa2(ByVal strDitta As String, ByVal lCampo As Integer, _
                                          ByVal lTipa As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 vt_desval, vt_valore FROM comtipa" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND vt_codtipa = " & lTipa & _
        " AND vt_ncampo = " & lCampo

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function RicreaSuccedaneiAccessori(ByVal strDitta As String, ByVal strCodart As String, ByVal bSucc As Boolean, ByVal bAcc As Boolean, _
                                                        ByVal dttIn As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dtrRow() As DataRow
    Dim dttTmp As New DataTable
    Dim strQuery As String = ""
    Dim i As Integer = 0
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      strSQL = "DELETE FROM artacce WHERE codditt = " & CStrSQL(strDitta) & " AND apa_codartas = " & CStrSQL(strCodart)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      For z As Integer = 0 To dttIn.Rows.Count - 1
        If (NTSCStr(dttIn.Rows(z)!apa_tipo) = "A" And bAcc) Or (NTSCStr(dttIn.Rows(z)!apa_tipo) = "S" And bSucc) Then
          strSQL = "DELETE FROM artacce " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & " AND apa_codart = " & CStrSQL(dttIn.Rows(z)!apa_codartas) & _
                   " AND apa_tipo = " & CStrSQL(dttIn.Rows(z)!apa_tipo)

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          dtrRow = dttIn.Select("apa_tipo = '" & NTSCStr(dttIn.Rows(z)!apa_tipo) & "'")

          For i = 0 To dtrRow.Length - 1
            If NTSCStr(dtrRow(i)!apa_codartas) <> NTSCStr(dttIn.Rows(z)!apa_codartas) Then
              strSQL = "INSERT INTO artacce (codditt, apa_codart, apa_codartas, apa_tipo, apa_note, apa_ultagg, apa_opnome) VALUES " & _
                       "(" & CStrSQL(dttIn.Rows(z)!codditt) & ", " & CStrSQL(dttIn.Rows(z)!apa_codartas) & ", " & CStrSQL(dtrRow(i)!apa_codartas) & ", " & _
                       CStrSQL(dttIn.Rows(z)!apa_tipo) & ", " & CStrSQL(dttIn.Rows(z)!apa_note) & ", " & CDataSQL(Now) & ", " & CStrSQL(oApp.User.Nome) & ")"

              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            End If
          Next

          strSQL = "INSERT INTO artacce (codditt, apa_codart, apa_codartas, apa_tipo, apa_note, apa_ultagg, apa_opnome) VALUES " & _
                "(" & CStrSQL(dttIn.Rows(z)!codditt) & ", " & CStrSQL(dttIn.Rows(z)!apa_codartas) & ", " & CStrSQL(strCodart) & ", " & _
                CStrSQL(dttIn.Rows(z)!apa_tipo) & ", " & CStrSQL(dttIn.Rows(z)!apa_note) & ", " & CDataSQL(Now) & ", " & CStrSQL(oApp.User.Nome) & ")"

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If

        strQuery &= CStrSQL(dttIn.Rows(z)!apa_codartas) & ", "
      Next

      strQuery &= CStrSQL(strCodart)

      strSQL = "DELETE FROM artacce " & _
               "WHERE codditt = " & CStrSQL(strDitta) & " AND apa_codart NOT IN (" & strQuery & ") AND apa_codartas IN (" & strQuery & ")"

      If Not (bAcc And bSucc) Then
        If bAcc Then
          strSQL &= " AND apa_tipo = 'A'"
        ElseIf bSucc Then
          strSQL &= " AND apa_tipo = 'S'"
        End If
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()

      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CompletaSuccedaneiAccessori(ByVal strDitta As String, ByVal strQuery As String, ByVal bAcc As Boolean, ByVal bSucc As Boolean, _
                                                          ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DISTINCT codditt, apa_codart, apa_codartas, apa_tipo FROM artacce " & _
               " WHERE codditt = " & CStrSQL(strDitta) & " AND apa_codartas IN (" & strQuery & ")"

      If Not (bAcc And bSucc) Then
        If bAcc Then
          strSQL &= " AND apa_tipo = 'A'"
        ElseIf bSucc Then
          strSQL &= " AND apa_tipo = 'S'"
        End If
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetArtclasDescr(ByVal strDitta As String, ByVal strClas1 As String, _
                                              ByVal strClas2 As String, ByVal strClas3 As String, _
                                              ByVal strClas4 As String, ByVal strClas5 As String) As String
    GetArtclasDescr = ""
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strDescr As String = ""
    Try
      If strClas1 <> "" Then
        strSQL = "SELECT acl_descla1 FROM artclas1 WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND acl_codcla1 = " & CStrSQL(strClas1)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then strDescr = NTSCStr(dttTmp.Rows(0)!acl_descla1)
        dttTmp.Clear()
      End If

      If strClas2 <> "" Then
        strSQL = "SELECT acl_descla2 FROM artclas2 WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND acl_codcla1 = " & CStrSQL(strClas1) & " AND acl_codcla2 = " & CStrSQL(strClas2)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then strDescr += " / " & NTSCStr(dttTmp.Rows(0)!acl_descla2)
        dttTmp.Clear()
      End If

      If strClas3 <> "" Then
        strSQL = "SELECT acl_descla3 FROM artclas3 WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND acl_codcla1 = " & CStrSQL(strClas1) & " AND acl_codcla2 = " & CStrSQL(strClas2) & _
                 " AND acl_codcla3 = " & CStrSQL(strClas3)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then strDescr += " / " & NTSCStr(dttTmp.Rows(0)!acl_descla3)
        dttTmp.Clear()
      End If

      If strClas4 <> "" Then
        strSQL = "SELECT acl_descla4 FROM artclas4 WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND acl_codcla1 = " & CStrSQL(strClas1) & " AND acl_codcla2 = " & CStrSQL(strClas2) & _
                 " AND acl_codcla3 = " & CStrSQL(strClas3) & " AND acl_codcla4 = " & CStrSQL(strClas4)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then strDescr += " / " & NTSCStr(dttTmp.Rows(0)!acl_descla4)
        dttTmp.Clear()
      End If

      If strClas5 <> "" Then
        strSQL = "SELECT acl_descla5 FROM artclas5 WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND acl_codcla1 = " & CStrSQL(strClas1) & " AND acl_codcla2 = " & CStrSQL(strClas2) & _
                 " AND acl_codcla3 = " & CStrSQL(strClas3) & " AND acl_codcla4 = " & CStrSQL(strClas4) & _
                 " AND acl_codcla5 = " & CStrSQL(strClas5)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then strDescr += " / " & NTSCStr(dttTmp.Rows(0)!acl_descla5)
        dttTmp.Clear()
      End If

      Return strDescr

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ListaArticoliModello(ByVal strDitta As String) As DataTable
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ar_codart AS cod, (ar_codart + ' - ' + ar_descr) AS val " & _
               " FROM artico " & _
               " WHERE ar_codart LIKE 'MODELLO_%'"

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return Nothing
  End Function

  Public Overridable Function GeneraBarcode(ByVal strDitta As String, ByVal strCodart As String, _
                                        ByVal strUnmis As String, ByVal strPrefixEAN13 As String, _
                                        ByVal bIndicod As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim ds As DataSet = Nothing
    Dim lOrins As Integer
    Dim strOra As String
    Dim strCodeTmp As String = ""
    Dim dbConn As DbConnection = Nothing
    Try
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      If LegNuma(strDitta, "BC", " ", 0, True, dbConn) <= 99999 Then
        strSQL = "SELECT bc_codart FROM barcode" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND bc_codart = " & CStrSQL(strCodart) & _
          " ORDER BY codditt, bc_code"
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "BARCODE", , dbConn)

        If ds.Tables("BARCODE").Rows.Count = 0 Then
          strOra = Format(Now.ToShortTimeString, "hh:mm:ss")
          lOrins = NTSCInt(Mid(strOra, 1, 2)) * 10000
          lOrins = lOrins + NTSCInt(Mid$(strOra, 4, 2)) * 100
          lOrins = lOrins + NTSCInt(Mid$(strOra, 7, 2))
          '----------------------------------------------------------------------------------------------------------
          strCodeTmp = GeneraEAN13(strDitta, strPrefixEAN13, bIndicod, dbConn)
          If strCodeTmp = "" Then Return False
          '----------------------------------------------------------------------------------------------------------
          strSQL = "INSERT INTO barcode (codditt, bc_codart, bc_datins," & _
            " bc_code, bc_unmis, bc_quant, bc_orins, bc_datagg, bc_oragg, bc_tipo)" & _
            " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & _
            CDataSQL(Now.ToShortDateString) & ", " & CStrSQL(strCodeTmp) & ", " & CStrSQL(strUnmis) & ", 1, " & _
            lOrins & ", " & CDataSQL(Now.ToShortDateString) & ", " & lOrins & ", 'E')"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      End If

      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se sono in transazione la annullo
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If dbConn.State = ConnectionState.Open Then dbConn.Close()

      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GeneraEAN13(ByVal strDitta As String, ByVal strPrefixEAN13 As String, _
                                          ByVal bIndicod As Boolean, ByVal dbConn As DbConnection) As String
    Dim strSQL As String = ""
    Dim strDummy As String
    Dim lProgr As Integer
    Dim lProgrTmp As Integer
    Dim strLastDigit As String
    Dim strPrefissoEAN13 As String
    Try
      '-----------------------------------------------------------------------------------------
      GeneraEAN13 = ""
      '-----------------------------------------------------------------------------------------
      '--- Legge l'ultimo progressivo da TABNUMA
      '-----------------------------------------------------------------------------------------
      lProgr = LegNuma(strDitta, "BC", " ", 0, True, dbConn)
      lProgrTmp = lProgr
      '-----------------------------------------------------------------------------------------
      strPrefissoEAN13 = strPrefixEAN13
      '-----------------------------------------------------------------------------------------
      '--- Se non c'è l'ipostazione di registro per la creazione da Indicod funziona come prima
      '-----------------------------------------------------------------------------------------
      If bIndicod = False Then
        strDummy = "2" & CStr(Format(lProgr, "00000")) & "000000"
      Else
        If Len(strPrefissoEAN13) < 12 Then
          If Len(strPrefissoEAN13 & CStr(lProgrTmp)) < 12 Then
            Do
              strPrefissoEAN13 = strPrefissoEAN13 & "0"
            Loop Until Len(strPrefissoEAN13 & CStr(lProgrTmp)) = 12
            strDummy = strPrefissoEAN13 & CStr(lProgrTmp)
          Else
            If Len(strPrefissoEAN13 & CStr(lProgrTmp)) > 12 Then
              If Len(CStr(lProgrTmp)) > 1 Then
                Do
                  lProgrTmp = NTSCInt(Right(CStr(lProgrTmp), (Len(CStr(lProgrTmp)) - 1)))
                  If lProgrTmp = 0 Then Return ""
                Loop Until Len(strPrefissoEAN13 & CStr(lProgrTmp)) = 12
                strDummy = strPrefissoEAN13 & CStr(lProgrTmp)
              Else
                strDummy = Left(strPrefissoEAN13, 12)
              End If
            Else
              strDummy = strPrefissoEAN13 & CStr(lProgrTmp)
            End If
          End If
        Else
          If Len(strPrefissoEAN13) > 12 Then
            strDummy = Left(strPrefissoEAN13, 12)
          Else
            strDummy = strPrefissoEAN13
          End If
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Determina il CheckDigit finale
      '-----------------------------------------------------------------------------------------
      strLastDigit = CheckDigit(strDummy & "0")
      '-----------------------------------------------------------------------------------------
      '--- Se andato a buon fine aggiorna il numeratore in TABNUMA
      '-----------------------------------------------------------------------------------------
      If strLastDigit <> "" Then
        strDummy = strDummy & strLastDigit
        GeneraEAN13 = strDummy
        AggNuma(strDitta, "BC", " ", 0, lProgr, True, True, "", dbConn)
      End If
      '-----------------------------------------------------------------------------------------

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CheckDigit(ByVal strCodice As String) As String
    Dim strSQL As String = ""
    Dim nI As Integer
    Dim nCurNum As Integer
    Dim nSumDisp As Integer
    Dim nSumPari As Integer
    Dim nSumTot As Integer
    Try
      CheckDigit = ""
      nSumPari = 0 : nSumDisp = 0
      For nI = 1 To Len(strCodice) - 1
        nCurNum = NTSCInt(Mid(strCodice, nI, 1))
        '--- Verifico che il numero corrente sia pari o dispari
        If (nI Mod 2) = 0 Then nSumPari = (nSumPari + nCurNum) Else nSumDisp = (nSumDisp + nCurNum)
      Next
      nSumPari = (nSumPari * 3) : nSumTot = (nSumPari + nSumDisp)
      CheckDigit = Right(NTSCStr(10 - NTSCInt(Right(NTSCStr(nSumTot), 1))), 1)

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

#Region "BNMGANEX.VB"
  Public Overridable Function GetTabaext(ByVal strDitta As String, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM tabaext" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_tipork = 'A'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        dttTmp.Clear()
        dttTmp.Dispose()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
#End Region

  Public Overridable Function ArticoliDeteriorabili(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 rp_liv1 FROM REGPROP " & _
               " WHERE rp_liv1 = 'BSVEBOLL' " & _
               "   AND rp_liv2 IN ('OPZIONI', 'OPZIONIDOC')  " & _
               "   AND rp_idaz IN ('', " & CStrSQL(strDitta) & ") " & _
               "   AND rp_nomprop = 'SpezzaDocArt62Deterior' " & _
               "   AND rp_valprop <> '0' "

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC).Rows.Count > 0
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

End Class
