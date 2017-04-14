Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica

Public Class CLDMGANLO
  Inherits CLD__BASE

  Public Overridable Function CheckCodart(ByVal strDitta As String, _
    ByVal strCodart As String, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '----------------------------------------------------------------------------------
      strSQL = "SELECT ar_descr FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Return False
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CheckEsistenzaDati(ByVal strDitta As String, ByVal strCodart As String, ByVal lLotto As Integer) As Boolean
    Try
      'obsoleta
      Return CheckEsistenzaDati(strDitta, strCodart, lLotto, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckEsistenzaDati(ByVal strDitta As String, ByVal strCodart As String, ByVal lLotto As Integer, ByVal strLottox As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, lLotto, strLottox})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '----------------------------------------------------------------------------------
      strSQL = "SELECT alo_codart FROM analotti" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND alo_codart = " & CStrSQL(strCodart)
      If lLotto <> 0 Then strSQL += " AND alo_lotto = " & lLotto
      If strLottox.Trim <> "" Then strSQL += " AND alo_lottox = " & CStrSQL(strLottox)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        dttTmp.Clear()
        Return False
      End If
      dttTmp.Clear()
      '----------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Return False
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetData(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal lLotto As Integer, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""

    Try
      '----------------------------------------------------------------------------------
      strSQL = "SELECT analotti.*, tb_despqua AS xx_codpqua, tb_deslotx as xx_codlotx FROM analotti" & _
        " LEFT JOIN TABPQUA ON TABPQUA.codditt = analotti.codditt AND tb_codpqua = alo_codpqua" & _
        " LEFT JOIN tablotx ON tablotx.codditt = analotti.codditt AND tablotx.tb_codlotx = analotti.alo_codlotx" & _
        " WHERE analotti.codditt = " & CStrSQL(strDitta) & _
        " AND alo_codart = " & CStrSQL(strCodart)
      If lLotto <> 0 Then strSQL += " AND alo_lotto = " & lLotto.ToString()
      strSQL += " ORDER BY analotti.codditt, alo_codart, alo_lottox"
      '----------------------------------------------------------------------------------
      '--- Chiedo i dati al database
      '----------------------------------------------------------------------------------
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANALOTTI")
      '----------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ProgressivoLottoPerArticolo(ByVal strDitta As String, _
    ByVal strCodart As String) As Integer
    Dim lNum As Integer = 1
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '----------------------------------------------------------------------------------
      strSQL = "SELECT Max(alo_lotto) As Lotto FROM analotti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND alo_codart = " & CStrSQL(strCodart)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then lNum = (NTSCInt(dttTmp.Rows(0)!Lotto) + 1)
      dttTmp.Clear()
      '----------------------------------------------------------------------------------
      Return lNum
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function VerificaCampqua(ByVal strDitta As String, ByVal lCodpqua As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 ct_codpqua FROM campqua WHERE" & _
               " codditt = " & CStrSQL(strDitta) & " AND ct_codpqua = " & NTSCInt(lCodpqua) & _
               " ORDER BY ct_codpqua, ct_ncampo"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaVallotti(ByVal strDitta As String, ByVal strCodart As String, ByVal lLotto As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM vallotti " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND alv_codart = " & CStrSQL(strCodart) & _
               " AND alv_lotto = " & lLotto

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckVallotti(ByVal strDitta As String, ByVal strArt As String, ByVal lLotto As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 alv_codart FROM vallotti WHERE" & _
                 " codditt = " & CStrSQL(strDitta) & _
                 " AND alv_codart = " & CStrSQL(strArt) & _
                 " AND alv_lotto = " & lLotto & _
                 " ORDER BY alv_codart, alv_lotto"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "FRMSQVALO"
  Public Overridable Function ImportaDati(ByVal strDitta As String, ByVal strValoCodart As String, ByVal lValoLotto As Integer, ByVal nValoCodpqua As Integer) As Boolean
    Dim strSQL As String
    Dim dttTmp, dttTmp1 As New DataTable
    Dim lRecord As Integer = 0
    Try
      strSQL = "INSERT INTO vallotti (codditt, alv_codart, alv_lotto, alv_ncampo, alv_valdouble," & _
               " alv_valdata, alv_valtext, alv_valcombo, alv_tipcamp, alv_codpqua, alv_descamp, alv_desval)" & _
               " SELECT " & CStrSQL(strDitta) & ", " & CStrSQL(strValoCodart) & ", " & lValoLotto & ", ct_ncampo, 0, NULL, NULL," & _
               " vt_valore, ct_tipcamp, ct_codpqua, ct_descamp, NULL" & _
               " FROM campqua INNER JOIN compqua ON (campqua.ct_codpqua = compqua.vt_codpqua)" & _
               " AND (campqua.codditt = compqua.codditt)" & _
               " AND (campqua.ct_ncampo = compqua.vt_ncampo)" & _
               " WHERE campqua.codditt = " & CStrSQL(strDitta) & " AND campqua.ct_codpqua = " & nValoCodpqua & _
               " AND vt_item = (SELECT MIN(vt_item) FROM compqua AS c1 " & _
               "                WHERE c1.vt_codpqua = compqua.vt_codpqua " & _
               "	            	AND c1.vt_ncampo = compqua.vt_ncampo) " & _
               " AND NOT EXISTS (SELECT codditt FROM vallotti " & _
               "                 WHERE vallotti.codditt = campqua.codditt " & _
               "                 AND alv_ncampo = ct_ncampo AND alv_codpqua = ct_codpqua " & _
               "                 AND alv_codart = " & CStrSQL(strValoCodart) & _
               "                 AND alv_lotto = " & lValoLotto & ")"

      lRecord = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "INSERT INTO vallotti (codditt, alv_codart, alv_lotto, alv_ncampo, alv_valdouble," & _
               " alv_valdata, alv_valtext, alv_valcombo, alv_tipcamp, alv_codpqua, alv_descamp, alv_desval)" & _
               " SELECT " & CStrSQL(strDitta) & ", " & CStrSQL(strValoCodart) & ", " & lValoLotto & ", ct_ncampo, 0, NULL, NULL," & _
               " ' ', ct_tipcamp, ct_codpqua, ct_descamp, NULL" & _
               " FROM campqua " & _
               " WHERE campqua.codditt = " & CStrSQL(strDitta) & " AND campqua.ct_codpqua = " & nValoCodpqua & _
               " AND NOT EXISTS (SELECT codditt FROM vallotti " & _
               "                 WHERE vallotti.codditt = campqua.codditt " & _
               "                 AND alv_ncampo = ct_ncampo AND alv_codpqua = ct_codpqua " & _
               "                 AND alv_codart = " & CStrSQL(strValoCodart) & _
               "                 AND alv_lotto = " & lValoLotto & ")"

      lRecord += Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      If lRecord > 0 Then
        '-----------------------------------------------------------------------------------
        strSQL = "SELECT * FROM vallotti WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND alv_codart = " & CStrSQL(strValoCodart) & _
                 " AND alv_lotto = " & lValoLotto & " AND alv_tipcamp = 20" & _
                 " AND alv_desval IS NULL " & _
                 " ORDER BY alv_codart, alv_lotto, alv_ncampo"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

        For z As Integer = 0 To dttTmp.Rows.Count - 1
          strSQL = "SELECT TOP 1 vt_valore, vt_desval FROM compqua" & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND vt_codpqua = " & NTSCInt(dttTmp.Rows(z)!alv_codpqua) & _
                   " AND vt_ncampo = " & NTSCInt(dttTmp.Rows(z)!alv_ncampo) & _
                   " ORDER BY vt_codpqua, vt_ncampo, vt_item"
          dttTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

          If dttTmp1.Rows.Count > 0 Then
            strSQL = "UPDATE vallotti SET alv_valcombo = " & CStrSQL(dttTmp1.Rows(0)!vt_valore) & "," & _
                     " alv_desval = " & CStrSQL(dttTmp1.Rows(0)!vt_desval) & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND alv_codart = " & CStrSQL(dttTmp.Rows(z)!alv_codart) & _
                     " AND alv_lotto = " & NTSCInt(dttTmp.Rows(z)!alv_lotto) & _
                     " AND alv_ncampo = " & NTSCInt(dttTmp.Rows(z)!alv_ncampo)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          End If
        Next
      End If

      '-----------------------------------------------------------------------------------
      strSQL = "INSERT INTO vallotti (codditt, alv_codart, alv_lotto, alv_ncampo, alv_valdouble," & _
               " alv_valdata, alv_valtext, alv_valcombo, alv_tipcamp, alv_codpqua, alv_descamp, alv_desval)" & _
               " SELECT " & CStrSQL(strDitta) & ", " & CStrSQL(strValoCodart) & ", " & lValoLotto & ", ct_ncampo, 0, NULL, NULL," & _
               " ' ', ct_tipcamp, ct_codpqua, ct_descamp, NULL" & _
               " FROM campqua LEFT OUTER JOIN compqua ON (campqua.codditt = compqua.codditt)" & _
               " AND (campqua.ct_codpqua = compqua.vt_codpqua)" & _
               " AND (campqua.ct_ncampo = compqua.vt_ncampo)" & _
               " WHERE campqua.codditt = " & CStrSQL(strDitta) & _
               " AND campqua.ct_codpqua = " & nValoCodpqua & _
               " AND compqua.vt_codpqua Is Null AND compqua.vt_ncampo Is Null" & _
               " AND NOT EXISTS (SELECT codditt FROM vallotti " & _
               "                 WHERE vallotti.codditt = campqua.codditt AND alv_ncampo = ct_ncampo AND alv_codpqua = ct_codpqua)"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ApriVall(ByVal strDitta As String, ByVal strValoCodart As String, ByVal lValoLotto As Integer, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT vallotti.*, CAST('' AS Text) AS xx_valore, alv_valcombo + '.' + cast(alv_ncampo as varchar(9)) as xx_combo " & _
               " FROM vallotti" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND alv_codart = " & CStrSQL(strValoCodart) & _
               " AND alv_lotto = " & lValoLotto & _
               " ORDER BY alv_codart, alv_lotto, alv_ncampo"

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "VALLOTTI")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaVall(ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dtrRow() As DataRow
    Dim i As Integer
    Try
      dtrRow = ds.Tables("VALLOTTI").Select("", "", DataViewRowState.ModifiedCurrent)

      For i = 0 To dtrRow.Length - 1
        strSQL = "UPDATE vallotti SET " & GetQueryUpdate(ds.Tables("VALLOTTI"), dtrRow(i), "alv_") & _
                 " WHERE codditt = " & CStrSQL(dtrRow(i)!codditt) & _
                 " AND alv_codart = " & CStrSQL(dtrRow(i)!alv_codart) & _
                 " AND alv_lotto = " & NTSCInt(dtrRow(i)!alv_lotto) & _
                 " AND alv_ncampo = " & NTSCInt(dtrRow(i)!alv_ncampo)

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CaricaCombo(ByVal strDitta As String, ByVal nValoCodpqua As Integer, ByVal nCampo As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT vt_valore + '.' + cast(vt_ncampo as varchar(9)) AS cod, vt_desval AS val FROM compqua" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND vt_codpqua = " & nValoCodpqua

      If nCampo <> 0 Then strSQL &= " AND vt_ncampo = " & nCampo

      strSQL &= " AND vt_valore <> ' ' AND vt_desval <> ' '" & _
                " ORDER BY vt_codpqua, vt_ncampo, vt_item"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

  Public Overridable Function DeleteAnalotti(ByVal strDitta As String) As Boolean
    Try
      Return DeleteAnalotti(strDitta, "", 0, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function DeleteAnalotti(ByVal strDitta As String, ByVal strCodart As String, ByVal lLotto As Integer, ByRef nRecDeleted As Integer) As Boolean
    'cancello tutte le anagrafiche dei lotti non presenti in 
    'lotcdef, movmag, movprb, movord, ordlist, listsar, listprod, ecc
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, lLotto, nRecDeleted})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        nRecDeleted = NTSCInt(oIn(3))
        Return CBool(oOut)
      End If
      '----------------

      If strCodart <> "" Then
        strWhere = " AND xx_codart = " & CStrSQL(strCodart) & " AND xx_lotto = " & lLotto.ToString
      End If
      strSQL = "DELETE FROM analotti " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               strWhere.Replace("xx_", "alo_") & _
               " AND not exists (" & _
               " SELECT top 1 codditt FROM movmag WHERE codditt = analotti.codditt AND mm_codart = analotti.alo_codart AND mm_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM movprb WHERE codditt = analotti.codditt AND mm_codart = analotti.alo_codart AND mm_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM movord WHERE codditt = analotti.codditt AND mo_codart = analotti.alo_codart AND mo_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM ordlist WHERE codditt = analotti.codditt AND ol_codart = analotti.alo_codart AND ol_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM lotcdef WHERE codditt = analotti.codditt AND ld_codart = analotti.alo_codart AND ld_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM listsar WHERE codditt = analotti.codditt AND lsa_codart = analotti.alo_codart AND lsa_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM listprod WHERE codditt = analotti.codditt AND lp_codart = analotti.alo_codart AND lp_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM lotcdefo WHERE codditt = analotti.codditt AND ld_codart = analotti.alo_codart AND ld_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM lotcdeftc WHERE codditt = analotti.codditt AND adt_codart = analotti.alo_codart AND adt_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM lotcdeftco WHERE codditt = analotti.codditt AND adt_codart = analotti.alo_codart AND adt_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM lotcpro WHERE codditt = analotti.codditt AND lp_codart = analotti.alo_codart AND lp_lotto = analotti.alo_lotto" & _
               "         AND (lp_esist + lp_qtalif + lp_vqtalif + lp_giaini + lp_vgiaini + lp_prenot + lp_ordin + lp_impeg + lp_vprenot + lp_vordin + lp_vimpeg) <> 0" & _
               "   UNION SELECT top 1 codditt FROM lotcprotc WHERE codditt = analotti.codditt AND apt_codart = analotti.alo_codart AND apt_lotto = analotti.alo_lotto" & _
               "         AND ("
      For i As Integer = 1 To 24
        strSQL += " apt_esist" & Right("0" & i.ToString, 2) & " + " & _
                  " apt_prenot" & Right("0" & i.ToString, 2) & " + " & _
                  " apt_ordin" & Right("0" & i.ToString, 2) & " + " & _
                  " apt_impeg" & Right("0" & i.ToString, 2) & IIf(i < 24, " +", ") <> 0").ToString
      Next
      strSQL += "  UNION SELECT top 1 codditt FROM mpallet WHERE codditt = analotti.codditt AND mp_codart = analotti.alo_codart AND mp_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM mpallet WHERE codditt = analotti.codditt AND mp_codart = analotti.alo_codart AND mp_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM movubic WHERE codditt = analotti.codditt AND mu_codart = analotti.alo_codart AND mu_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM movpic WHERE codditt = analotti.codditt AND mp_codart = analotti.alo_codart AND mp_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM movpac WHERE codditt = analotti.codditt AND mp_codart = analotti.alo_codart AND mp_lotto = analotti.alo_lotto" & _
               "   UNION SELECT top 1 codditt FROM movcom WHERE codditt = analotti.codditt AND mc_codart = analotti.alo_codart AND mc_lotto = analotti.alo_lotto" & _
               ")"
      nRecDeleted = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      If nRecDeleted = 0 Then Return True

      strSQL = "DELETE FROM vallotti " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 strWhere.Replace("xx_", "alv_") & _
                 " AND codditt + '.' + alv_codart + '.' + cast(alv_lotto as varchar(9)) " & _
                 " NOT IN (SELECT DISTINCT codditt + '.' + alo_codart + '.' + cast(alo_lotto as varchar(9)) FROM analotti WHERE codditt = " & CStrSQL(strDitta) & _
                 "        )"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM allole " & _
         " WHERE codditt = " & CStrSQL(strDitta)
      If strCodart <> "" Then
        strWhere = " AND ao_strcod = " & CStrSQL(strCodart) & " AND ao_lotto = " & lLotto.ToString
      End If
      strSQL += " AND ao_tipo = 'L' AND ao_lotto <> 0" & _
         " AND codditt + '.' + ao_strcod + '.' + cast(ao_lotto as varchar(9)) " & _
         " NOT IN (SELECT DISTINCT codditt + '.' + alo_codart + '.' + cast(alo_lotto as varchar(9)) FROM analotti WHERE codditt = " & CStrSQL(strDitta) & _
         "        )"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      'Elimino tutti i lotcpro dipendenti dai lotti che non esistono più in analotti
      strSQL = " DELETE FROM lotcpro " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND lp_lotto <> 0 "
      If strCodart <> "" Then
        strSQL += " AND lp_codart = " & CStrSQL(strCodart) & " AND lp_lotto = " & lLotto.ToString
      End If
      strSQL += " AND codditt + '.' + lp_codart + '.' + cast(lp_lotto as varchar(9))" & _
                " NOT IN (SELECT DISTINCT codditt + '.' + alo_codart + '.' + cast(alo_lotto as varchar(9))" & _
                " FROM analotti" & _
                " WHERE codditt = " & CStrSQL(strDitta) & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)


      'Elimino tutti i lotcprotc dipendenti dai lotti che non esistono più in analotti
      strSQL = " DELETE FROM lotcprotc " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND apt_lotto <> 0 "
      If strCodart <> "" Then
        strSQL += " AND apt_codart = " & CStrSQL(strCodart) & " AND apt_lotto = " & lLotto.ToString
      End If
      strSQL += " AND codditt + '.' + apt_codart + '.' + cast(apt_lotto as varchar(9))" & _
                " NOT IN (SELECT DISTINCT codditt + '.' + alo_codart + '.' + cast(alo_lotto as varchar(9))" & _
                " FROM analotti" & _
                " WHERE codditt = " & CStrSQL(strDitta) & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
End Class