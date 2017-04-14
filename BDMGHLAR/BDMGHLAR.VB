Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGHLAR
  Inherits CLD__BASE


  Public Overridable Function GetTabtipa(ByVal strDitta As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT 0 AS cod, '' AS val FROM tabtipa" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_codtipa = 0"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      dttOut.Rows.Add(dttOut.NewRow)
      With dttOut.Rows(dttOut.Rows.Count - 1)
        !cod = 0
        !val = "(Tutti)"
      End With
      dttOut.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tb_codtipa, tb_destipa FROM tabtipa" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dttTmp.Rows.Count - 1)
        dttOut.Rows.Add(dttOut.NewRow)
        With dttOut.Rows(dttOut.Rows.Count - 1)
          !cod = NTSCInt(dttTmp.Rows(i)!tb_codtipa)
          !val = NTSCStr(dttTmp.Rows(i)!tb_destipa)
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      dttOut.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function


  Public Overridable Function GetData(ByRef dsHlar As DataSet, ByVal strDitta As String, _
                                      ByVal strSelect As String, ByVal strQuery As String, _
                                      ByVal strOpz4 As String, ByVal strBarcode As String, _
                                      ByVal strCodarfo As String, ByVal lContoCodarfo As Integer, _
                                      ByVal bTestmag As Boolean, ByVal nMagaz As Integer, _
                                      ByVal strSuccedanei As String, ByVal strCodartacc As String, _
                                      ByVal bPrezzi As Boolean, ByVal nListino As Integer, _
                                      ByVal strDtlistino As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return GetData(dsHlar, strDitta, strSelect, strQuery, strOpz4, strBarcode, strCodarfo, lContoCodarfo, _
                     bTestmag, nMagaz, strSuccedanei, strCodartacc, bPrezzi, nListino, strDtlistino, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetData(ByRef dsHlar As DataSet, ByVal strDitta As String, _
                                      ByVal strSelect As String, ByVal strQuery As String, _
                                      ByVal strOpz4 As String, ByVal strBarcode As String, _
                                      ByVal strCodarfo As String, ByVal lContoCodarfo As Integer, _
                                      ByVal bTestmag As Boolean, ByVal nMagaz As Integer, _
                                      ByVal strSuccedanei As String, ByVal strCodartacc As String, _
                                      ByVal bPrezzi As Boolean, ByVal nListino As Integer, _
                                      ByVal strDtlistino As String, ByVal bAbituali As Boolean) As Boolean
    '--------------------------------------
    'se sono arrivato qui sicuramente  stato impostato se devo cercare clienti/fornitori o sottoconti
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dsHlar, strDitta, strSelect, strQuery, strOpz4, strBarcode, _
                                             strCodarfo, lContoCodarfo, bTestmag, nMagaz, strSuccedanei, _
                                             strCodartacc, bPrezzi, nListino, strDtlistino, bAbituali})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsHlar = CType(oIn(0), DataSet)
        Return CBool(oOut)
      End If
      '----------------
      Return GetData(dsHlar, strDitta, strSelect, strQuery, strOpz4, strBarcode, strCodarfo, lContoCodarfo, bTestmag, nMagaz, _
                     strSuccedanei, strCodartacc, bPrezzi, nListino, strDtlistino, bAbituali, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetData(ByRef dsHlar As DataSet, ByVal strDitta As String, _
                                      ByVal strSelect As String, ByVal strQuery As String, _
                                      ByVal strOpz4 As String, ByVal strBarcode As String, _
                                      ByVal strCodarfo As String, ByVal lContoCodarfo As Integer, _
                                      ByVal bTestmag As Boolean, ByVal nMagaz As Integer, _
                                      ByVal strSuccedanei As String, ByVal strCodartacc As String, _
                                      ByVal bPrezzi As Boolean, ByVal nListino As Integer, _
                                      ByVal strDtlistino As String, ByVal bAbituali As Boolean, ByVal bCoodRoot As Boolean) As Boolean
    '--------------------------------------
    'se sono arrivato qui sicuramente  stato impostato se devo cercare clienti/fornitori o sottoconti
    Dim strSQL As String = ""
    Dim dsTmp As New DataSet
    Dim strFrom As String = ""

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dsHlar, strDitta, strSelect, strQuery, strOpz4, strBarcode, _
                                             strCodarfo, lContoCodarfo, bTestmag, nMagaz, strSuccedanei, _
                                             strCodartacc, bPrezzi, nListino, strDtlistino, bAbituali})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsHlar = CType(oIn(0), DataSet)
        Return CBool(oOut)
      End If
      '----------------
      strSelect = strSelect.Replace("xx_seleziona", "'N' as xx_seleziona")

      'aggiungo se non c'è tra i filtri il campo 'ar_gesvar' e 'ar_codtagl': serve per andare in 'gestione di bsmgarti o bsmgartv o bstcartv'
      strSelect = strSelect.Replace(", ar_gesvar", "")
      strSelect += ", ar_gesvar "

      strSelect = strSelect.Replace(", ar_codtagl", "")
      strSelect += ", ar_codtagl "

      strSelect = strSelect.Replace("xx_fase", "ar_ultfase as xx_fase")
      strSelect = strSelect.Replace("xx_descr", "af_descr as xx_descr")

      strSelect = strSelect.Replace("xx_code", "(SELECT TOP 1 bc_code FROM barcode WHERE barcode.codditt = artico.codditt AND barcode.bc_codart = artico.ar_codart AND artico.codditt = " & CStrSQL(strDitta) & ") AS xx_code")

      If lContoCodarfo > 0 Then
        strSelect = strSelect.Replace("xx_codarfo", "(SELECT TOP 1 caf_codarfo FROM codarfo WHERE codarfo.codditt = artico.codditt AND codarfo.caf_codart = artico.ar_codart AND codarfo.caf_conto = " & lContoCodarfo.ToString & " AND codarfo.codditt = " & CStrSQL(strDitta) & ") AS xx_codarfo ")
      Else
        strSelect = strSelect.Replace("xx_codarfo", "'' as xx_codarfo")
      End If

      If bPrezzi Then
        If nListino = 0 Then
          'da artprox
          strSelect = strSelect.Replace("xx_prezzo", "apx_ultcos as xx_prezzo")
        ElseIf nListino = -1 Then
          'da artprox
          strSelect = strSelect.Replace("xx_prezzo", "apx_peucos as xx_prezzo")
        Else
          'dai listini
          strSelect = strSelect.Replace("xx_prezzo", "(SELECT TOP 1 lc_prezzo FROM busvw_listini WHERE busvw_listini.codditt = artico.codditt AND busvw_listini.ar_codart = artico.ar_codart AND busvw_listini.lc_fase=artico.ar_ultfase AND busvw_listini.codditt = " & CStrSQL(strDitta) & " AND lc_listino = " & nListino.ToString & " AND lc_conto = 0 AND lc_codvalu = 0 AND lc_codtpro = 0 AND lc_datagg <= " & CDataSQL(strDtlistino) & " AND lc_datscad >= " & CDataSQL(strDtlistino) & " AND lc_daquant <= 1 AND lc_aquant >= 1 AND (lc_unmis = '' OR lc_unmis = ar_unmis) ORDER BY lc_datagg DESC) as xx_prezzo")
        End If
      Else
        strSelect = strSelect.Replace("xx_prezzo", "0.0 as xx_prezzo")
      End If

      strFrom = "artico"
      'aggiungo alla from la tabela per gestione articoli succedanei/sostitutivi
      If strSuccedanei <> "N" And strCodartacc <> "'%%'" And strCodartacc <> "'%'" Then
        strFrom = "(artico INNER JOIN artacce ON artico.codditt = artacce.codditt AND artico.ar_codart = artacce.apa_codartas)"
      End If

      If nMagaz > 0 Then
        strSelect = strSelect.Replace("xx_esist", "ap_esist as xx_esist")
        strSelect = strSelect.Replace("xx_prenot", "ap_prenot as xx_prenot")
        strSelect = strSelect.Replace("xx_ordin", "ap_ordin as xx_ordin")
        strSelect = strSelect.Replace("xx_impeg", "ap_impeg as xx_impeg")
        strSelect = strSelect.Replace("xx_dispnet", "(ap_esist - ap_prenot) as xx_dispnet")
        strSelect = strSelect.Replace("xx_dispon", "(ap_esist + ap_ordin - ap_impeg) as xx_dispon")
        strSelect = strSelect.Replace("xx_dispo2", "(ap_esist - ap_impeg) as xx_dispo2")
        strSQL = "SELECT " & strSelect & " FROM ((" & strFrom & " LEFT JOIN artpro ON artico.codditt = artpro.codditt " & _
                             " AND artico.ar_codart = artpro.ap_codart AND artico.ar_ultfase = artpro.ap_fase) " & _
                             " LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart  " & _
                             " AND artico.ar_ultfase = artfasi.af_fase) "
        If bPrezzi And (nListino = 0 Or nListino = -1) Then
          'aggiungo artprox per l'ultimo costo (e ultimo costo + costi acc.)
          strSQL += " LEFT JOIN artprox ON artico.codditt = artprox.codditt AND artico.ar_codart = artprox.apx_codart AND artico.ar_ultfase = artprox.apx_fase "
        End If
      Else
        strSelect = strSelect.Replace("xx_esist", "apx_esist as xx_esist")
        strSelect = strSelect.Replace("xx_prenot", "apx_prenot as xx_prenot")
        strSelect = strSelect.Replace("xx_ordin", "apx_ordin as xx_ordin")
        strSelect = strSelect.Replace("xx_impeg", "apx_impeg as xx_impeg")
        strSelect = strSelect.Replace("xx_dispnet", "(apx_esist - apx_prenot) as xx_dispnet")
        strSelect = strSelect.Replace("xx_dispon", "(apx_esist + apx_ordin - apx_impeg) as xx_dispon")
        strSelect = strSelect.Replace("xx_dispo2", "(apx_esist - apx_impeg) as xx_dispo2")
        strSQL = "SELECT " & strSelect & " FROM (" & strFrom & " LEFT JOIN artprox ON artico.codditt = artprox.codditt " & _
                             " AND artico.ar_codart = artprox.apx_codart AND artico.ar_ultfase = artprox.apx_fase) " & _
                             " LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart  " & _
                             " AND artico.ar_ultfase = artfasi.af_fase "
      End If


      strSQL += " WHERE artico.codditt = " & CStrSQL(strDitta) & " AND (af_fase IS NULL OR af_fase = ar_ultfase)"

      'filtro su visualizzaz. variante a 2 livelli se = 'S'
      If strOpz4 = "S" Then
        strSQL += " AND ar_codroot IS NULL AND ar_codvar1 IS NULL "
      Else
        If bCoodRoot = False Then strSQL += " AND NOT(ar_gesvar = 'S' AND ar_codroot IS NULL AND ar_codvar1 IS NULL) "
      End If
      If nMagaz > 0 Then strSQL += " AND (ap_magaz = " & nMagaz.ToString & " OR ap_magaz is null)"

      If strBarcode <> "'%%'" And strBarcode <> "'%'" Then
        strSQL += " AND ar_codart IN (SELECT bc_codart FROM barcode WHERE barcode.codditt = artico.codditt AND barcode.bc_codart = artico.ar_codart AND codditt = " & CStrSQL(strDitta) & " AND bc_code like " & strBarcode & ") "
      End If

      If strCodarfo <> "'%%'" And strCodarfo <> "'%'" Then
        strSQL += " AND ar_codart IN (SELECT caf_codart FROM codarfo WHERE codarfo.codditt = artico.codditt AND codarfo.caf_codart = artico.ar_codart AND codditt = " & CStrSQL(strDitta) & " AND caf_codarfo like " & strCodarfo & IIf(lContoCodarfo <> 0, " AND caf_conto = " & lContoCodarfo.ToString, "").ToString & ") "
      End If

      If bTestmag And lContoCodarfo <> 0 Then
        strSQL += " AND ar_codart IN (SELECT DISTINCT km_codart FROM keymag WHERE keymag.codditt = artico.codditt AND keymag.km_codart = artico.ar_codart AND keymag.codditt = " & CStrSQL(strDitta) & " AND keymag.km_conto = " & lContoCodarfo & ")"
      End If

      If strSuccedanei <> "N" And strCodartacc <> "''" Then
        strSQL += " AND apa_codart = " & strCodartacc & " AND apa_tipo = " & CStrSQL(strSuccedanei)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bAbituali = True Then
        strSQL += " AND ar_codart IN (SELECT DISTINCT TOP 50 km_codart" & _
                                   " FROM keymag INNER JOIN testmag ON keymag.codditt = testmag.codditt" & _
                                   " AND keymag.km_tipork = testmag.tm_tipork" & _
                                   " AND keymag.km_anno = testmag.tm_anno" & _
                                   " AND keymag.km_serie = testmag.tm_serie" & _
                                   " AND keymag.km_numdoc = testmag.tm_numdoc" & _
                                   " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                                   " AND tm_opnome = " & CStrSQL(oApp.User.Nome) & _
                                   " AND tm_datdoc BETWEEN " & CDataSQL(DateAdd(DateInterval.Month, -6, NTSCDate(Now.ToShortDateString))) & _
                                                     " AND " & CDataSQL(Now.ToShortDateString) & ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strQuery, strSQL)

      strSQL = strSQL & " ORDER BY ar_descr"
      dsHlar = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      '-----------------------------
      'devo togliere i record con descrizione che inzia con il prefisso indicato nella opzione di regitro
      If oApp.oGvar.strZoomScartaDescrPrefix <> "" Then
        For Each dtrT As DataRow In dsHlar.Tables("ARTICO").Rows
          If NTSCStr(dtrT!ar_descr).PadRight(oApp.oGvar.strZoomScartaDescrPrefix.Length).Substring(0, oApp.oGvar.strZoomScartaDescrPrefix.Length).ToUpper = oApp.oGvar.strZoomScartaDescrPrefix Then
            dtrT.Delete()
          End If
        Next
        dsHlar.AcceptChanges()
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetWhereHlmo(ByVal strDitta As String, ByVal nMagaz As Integer) As String
    Dim strSQL As String = ""
    Dim strTipord As String = ""
    Dim strScorpo As String = ""
    Try
      GetWhereHlmo = ""

      If nMagaz = 0 Then
        strSQL = " mo_flevas = 'C'"
      Else
        strSQL = " mo_magaz = " & nMagaz.ToString & " AND mo_flevas = 'C'"
      End If

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetQueryListe(ByVal strDitta As String, ByVal strTabella As String, ByVal nCod As Integer) As String
    Dim strSQL As String = ""
    Try
      Select Case strTabella.ToUpper
        Case "LISTSAR" : strSQL = "(SELECT lsa_codart FROM listsar WHERE codditt = " & CStrSQL(strDitta) & " AND lsa_codlsar = " & nCod.ToString & ")"
        Case "ARTPROM" : strSQL = "(SELECT apr_codart FROM artprom WHERE codditt = " & CStrSQL(strDitta) & " AND apr_codtpro = " & nCod.ToString & ")"
      End Select

      Return strSQL

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

  Public Overridable Function ListaClassificazioni(ByVal strDitta As String, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      dsOut = New DataSet

      For z As Integer = 1 To 5
        strSQL = "SELECT * FROM artclas" & z & _
                 " WHERE codditt = " & CStrSQL(strDitta)
        OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTCLAS" & z, dsOut)
      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTabAext(ByVal strDitta As String, ByRef dsTabaext As DataSet) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM tabaext" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_tipork = 'A'"
      dsTabaext = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABAEXT")
      If dsTabaext.Tables("TABAEXT").Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

End Class
