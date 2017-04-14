Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLD__SPAF
  Inherits CLD__BASE

  Public Overridable Function AggiungiFiltri(ByVal strDitta As String, _
    ByRef dtrFiltri As DataRow, ByRef dtrFiltriX As DataRow, _
    ByRef strSQL As String, ByRef strData As String, _
    ByRef lIITTArtpro As Integer, ByRef lIITTArtprox As Integer) As Boolean
    Dim lPos As Integer = 0
    Dim strTmp As String = ""
    Dim str1 As String = ""
    Dim str2 As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(dtrFiltri!pf_codquery)
        Case 1, 2, 3 '--- Anagra
          With dtrFiltri
            strSQL += " anagra.codditt = " & CStrSQL(strDitta)
            If (NTSCInt(!pf_contoini) <> 0) Or (NTSCInt(!pf_contofin) <> 999999999) Then
              strSQL += " AND anagra.an_conto BETWEEN " & NTSCInt(!pf_contoini) & " AND " & NTSCInt(!pf_contofin)
            End If
            If NTSCStr(!pf_1tipo).Trim <> "" Then strSQL += " AND anagra.an_tipo = " & CStrSQL(!pf_1tipo)
            If (NTSCStr(!pf_provini).Trim <> "") Or (NTSCStr(!pf_provfin).ToLower <> "".PadLeft(2, "z"c)) Then
              strSQL += " AND anagra.an_prov BETWEEN " & CStrSQL(!pf_provini) & " AND " & CStrSQL(!pf_provfin)
            End If
            If (Not (!pf_capini) Is System.DBNull.Value) Or (NTSCStr(!pf_capfin).ToLower <> "".PadLeft(5, "z"c)) Then
              If (NTSCStr(!pf_capini) <> "".PadLeft(1) And NTSCStr(!pf_capini) <> "".PadLeft(5)) Then
                strSQL += " AND anagra.an_cap BETWEEN " & CStrSQL(!pf_capini) & " AND " & CStrSQL(!pf_capfin)
              End If
            End If
            If (NTSCInt(!pf_zonaini) <> 0) Or (NTSCInt(!pf_zonafin) <> 999) Then
              strSQL += " AND anagra.an_zona BETWEEN " & NTSCInt(!pf_zonaini) & " AND " & NTSCInt(!pf_zonafin)
            End If
            If (NTSCInt(!pf_categini) <> 0) Or (NTSCInt(!pf_categfin) <> 999) Then
              strSQL += " AND anagra.an_categ BETWEEN " & NTSCInt(!pf_categini) & " AND " & NTSCInt(!pf_categfin)
            End If
            If (NTSCInt(!pf_agenteini) <> 0) Or (NTSCInt(!pf_agentefin) <> 9999) Then
              strSQL += " AND anagra.an_agente BETWEEN " & NTSCInt(!pf_agenteini) & " AND " & NTSCInt(!pf_agentefin)
            End If
            If (NTSCInt(!pf_codpagini) <> 0) Or (NTSCInt(!pf_codpagfin) <> 999) Then
              strSQL += " AND anagra.an_codpag BETWEEN " & NTSCInt(!pf_codpagini) & " AND " & NTSCInt(!pf_codpagfin)
            End If
            If (NTSCInt(!pf_codcand) <> 0) Or (NTSCInt(!pf_codcana) <> 999) Then
              strSQL += " AND anagra.an_codcana BETWEEN " & NTSCInt(!pf_codcand) & " AND " & NTSCInt(!pf_codcana)
            End If
            If (NTSCInt(!pf_listd) <> -2) Or (NTSCInt(!pf_lista) <> 9999) Then
              strSQL += " AND anagra.an_listino BETWEEN " & NTSCInt(!pf_listd) & " AND " & NTSCInt(!pf_lista)
            End If
            If (NTSCInt(!pf_provvd) <> 0) Or (NTSCInt(!pf_provva) <> 999) Then
              strSQL += " AND anagra.an_claprov BETWEEN " & NTSCInt(!pf_provvd) & " AND " & NTSCInt(!pf_provva)
            End If
            If (NTSCInt(!pf_scontid) <> 0) Or (NTSCInt(!pf_scontia) <> 999) Then
              strSQL += " AND anagra.an_clascon BETWEEN " & NTSCInt(!pf_scontid) & " AND " & NTSCInt(!pf_scontia)
            End If
            If (NTSCInt(!pf_codesed) <> 0) Or (NTSCInt(!pf_codesea) <> 9999) Then
              strSQL += " AND anagra.an_codese BETWEEN " & NTSCInt(!pf_codesed) & " AND " & NTSCInt(!pf_codesea)
            End If
            If (NTSCInt(!pf_lingd) <> 0) Or (NTSCInt(!pf_linga) <> 999) Then
              strSQL += " AND anagra.an_codling BETWEEN " & NTSCInt(!pf_lingd) & " AND " & NTSCInt(!pf_linga)
            End If
            If (NTSCInt(!pf_vald) <> 0) Or (NTSCInt(!pf_vala) <> 999) Then
              strSQL += " AND anagra.an_valuta BETWEEN " & NTSCInt(!pf_vald) & " AND " & NTSCInt(!pf_vala)
            End If
            If (NTSCDate(!pf_dtaperini) <> NTSCDate(IntSetDate("01/01/1900"))) Or (NTSCDate(!pf_dtaperfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
              strSQL += " AND anagra.an_dtaper BETWEEN " & CDataSQL(NTSCStr(!pf_dtaperini)) & " AND " & CDataSQL(NTSCStr(!pf_dtaperfin))
            End If
            If (NTSCDate(!pf_ultaggini) <> NTSCDate(IntSetDate("01/01/1900"))) Or (NTSCDate(!pf_ultaggfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
              strSQL += " AND anagra.an_ultagg BETWEEN " & CDataSQL(NTSCStr(!pf_ultaggini)) & " AND " & CDataSQL(NTSCStr(!pf_ultaggfin))
            End If
            If NTSCStr(!pf_stato).Trim <> "" Then strSQL += " AND anagra.an_stato = " & CStrSQL(!pf_stato)
            Select Case NTSCInt(!pf_testiva)
              Case 0 : strSQL += " AND anagra.an_pariva IS NOT NULL AND anagra.an_pariva <> '0'"
              Case 1 : strSQL += " AND (anagra.an_pariva IS NULL OR anagra.an_pariva = '0')"
            End Select
            If (Not (!pf_ragd) Is System.DBNull.Value) Or (NTSCStr(!pf_raga).ToLower <> "".PadLeft(30, "z"c)) Then
              If (NTSCStr(!pf_ragd) <> "".PadLeft(1) And NTSCStr(!pf_ragd) <> "".PadLeft(30)) Then
                strSQL += " AND anagra.an_descr1 BETWEEN " & CStrSQL(!pf_ragd) & " AND " & CStrSQL(!pf_raga)
              End If
            End If
            If NTSCStr(!pf_blocco).Trim <> "" Then strSQL += " AND anagra.an_blocco = " & CStrSQL(!pf_blocco)
            If NTSCStr(!pf_privacy).Trim <> "" Then strSQL += " AND anagra.an_privacy = " & CStrSQL(!pf_privacy)
            If NTSCStr(!pf_status).Trim <> "" Then strSQL += " AND anagra.an_status = " & CStrSQL(!pf_status)
            If NTSCStr(!pf_perfatt).Trim <> "" Then strSQL += " AND anagra.an_perfatt = " & CStrSQL(!pf_perfatt)
            Select Case NTSCInt(!pf_pcons)
              Case 0, 8
              Case Else : strSQL += " AND anagra.an_gcons = " & NTSCInt(!pf_pcons)
            End Select
          End With
          '----------------------------------------------------------------------------------------------------------
          AggiungiFiltriEstensioni(dtrFiltri, dtrFiltriX, strSQL)
          '----------------------------------------------------------------------------------------------------------
          '--- ANAGRAFICA CLIENTI/FORNITORI/SOTTOCONTI CON DATI CONTABILI
          '----------------------------------------------------------------------------------------------------------
          If NTSCInt(dtrFiltri!pf_codquery) = 2 Then
            strSQL += " AND (prinot.pn_escomp = " & NTSCInt(dtrFiltri!pf_escomp) & " OR prinot.pn_escomp IS NULL)" & _
              " AND (prinot.pn_datreg <= " & CDataSQL(strData) & " OR prinot.pn_datreg IS NULL)" & _
              " GROUP BY " & NTSCStr(dtrFiltri!pf_order)
            strTmp = ""
            Select Case NTSCInt(dtrFiltri!pf_saldo)
              Case 1 : strTmp = ">"
              Case 2 : strTmp = "<"
              Case 3 : strTmp = "="
              Case 4 : strTmp = "<>"
              Case 5 : strTmp = ">="
              Case 6 : strTmp = "<="
            End Select
            If strTmp <> "" Then strSQL += " HAVING (Sum(prinot.pn_dare) - Sum(prinot.pn_avere)) " & strTmp & " 0"
            Select Case NTSCInt(dtrFiltri!pf_movimentato)
              Case 1, 2
                strSQL += IIf(NTSCInt(dtrFiltri!pf_saldo) > 0, " AND", " HAVING").ToString & " Sum(prinot.pn_importo) IS " & IIf(NTSCInt(dtrFiltri!pf_movimentato) = 1, "NOT", "").ToString & " NULL"
            End Select
          End If
          '----------------------------------------------------------------------------------------------------------
        Case Else          'Artico
          With dtrFiltri
            strSQL += " artico.codditt = " & CStrSQL(strDitta)
            If (NTSCInt(!pf_gruppoini) <> 0) Or (NTSCInt(!pf_gruppofin) <> 99) Then
              strSQL += " AND artico.ar_gruppo BETWEEN " & NTSCInt(!pf_gruppoini) & " AND " & NTSCInt(!pf_gruppofin)
            End If
            If (NTSCInt(!pf_fornini) <> 0) Or (NTSCInt(!pf_fornfin) <> 999999999) Then
              strSQL += " AND artico.ar_forn BETWEEN " & NTSCInt(!pf_fornini) & " AND " & NTSCInt(!pf_fornfin)
            End If
            If (NTSCInt(!pf_sottd) <> 0) Or (NTSCInt(!pf_sotta) <> 9999) Then
              strSQL += " AND artico.ar_sotgru BETWEEN " & NTSCInt(!pf_sottd) & " AND " & NTSCInt(!pf_sotta)
            End If
            If (NTSCInt(!pf_marcad) <> 0) Or (NTSCInt(!pf_marcaa) <> 999) Then
              strSQL += " AND artico.ar_codmarc BETWEEN " & NTSCInt(!pf_marcad) & " AND " & NTSCInt(!pf_marcaa)
            End If
            If (NTSCInt(!pf_claprovd) <> 0) Or (NTSCInt(!pf_claprova) <> 999) Then
              strSQL += " AND artico.ar_claprov BETWEEN " & NTSCInt(!pf_claprovd) & " AND " & NTSCInt(!pf_claprova)
            End If
            If (NTSCInt(!pf_clascond) <> 0) Or (NTSCInt(!pf_clascona) <> 999) Then
              strSQL += " AND artico.ar_clascon BETWEEN " & NTSCInt(!pf_clascond) & " AND " & NTSCInt(!pf_clascona)
            End If
            If (NTSCInt(!pf_approvda) <> 0) Or (NTSCInt(!pf_approva) <> 999) Then
              strSQL += " AND artico.ar_codappr BETWEEN " & NTSCInt(!pf_approvda) & " AND " & NTSCInt(!pf_approva)
            End If
            If (NTSCInt(!pf_codivad) <> 0) Or (NTSCInt(!pf_codivaa) <> 9999) Then
              strSQL += " AND artico.ar_codiva BETWEEN " & NTSCInt(!pf_codivad) & " AND " & NTSCInt(!pf_codivaa)
            End If
            If (NTSCInt(!pf_codtipad) <> 0) Or (NTSCInt(!pf_codtipaa) <> 999) Then
              strSQL += " AND artico.ar_codtipa BETWEEN " & NTSCInt(!pf_codtipad) & " AND " & NTSCInt(!pf_codtipaa)
            End If
            If (NTSCInt(!pf_magstockini) <> 0) Or (NTSCInt(!pf_magstockfin) <> 9999) Then
              strSQL += " AND artico.ar_magstock BETWEEN " & NTSCInt(!pf_magstockini) & " AND " & NTSCInt(!pf_magstockfin)
            End If
            If (NTSCInt(!pf_magprodini) <> 0) Or (NTSCInt(!pf_magprodfin) <> 9999) Then
              strSQL += " AND artico.ar_magprod BETWEEN " & NTSCInt(!pf_magprodini) & " AND " & NTSCInt(!pf_magprodfin)
            End If
            If NTSCStr(!pf_stalist).Trim <> "" Then strSQL += " AND artico.ar_stalist = " & CStrSQL(!pf_stalist)
            If NTSCStr(!pf_2tipo).Trim <> "" Then strSQL += " AND artico.ar_tipo = " & CStrSQL(!pf_2tipo)
            If (NTSCStr(!pf_codartini) <> "".PadLeft(1) And NTSCStr(!pf_codartini) <> "".PadLeft(18)) Or (NTSCStr(!pf_codartfin).ToLower <> "".PadLeft(18, "z"c)) Then
              strSQL += " AND artico.ar_codart BETWEEN " & CStrSQL(!pf_codartini) & " AND " & CStrSQL(!pf_codartfin)
            End If
            If (NTSCStr(!pf_codaltini) <> "".PadLeft(1) And NTSCStr(!pf_codaltini) <> "".PadLeft(18)) Or (NTSCStr(!pf_codaltfin).ToLower <> "".PadLeft(18, "z"c)) Then
              strSQL += " AND artico.ar_codalt BETWEEN " & CStrSQL(!pf_codaltini) & " AND " & CStrSQL(!pf_codaltfin)
            End If
            If (NTSCStr(!pf_famprodini) <> "".PadLeft(1) And NTSCStr(!pf_famprodini) <> "".PadLeft(4)) Or (NTSCStr(!pf_famprodfin).ToLower <> "".PadLeft(4, "z"c)) Then
              strSQL += " AND artico.ar_famprod BETWEEN " & CStrSQL(!pf_famprodini) & " AND " & CStrSQL(!pf_famprodfin)
            End If
            If NTSCStr(!pf_codcla1).Trim <> "" Then strSQL += " AND artico.ar_codcla1 = " & CStrSQL(!pf_codcla1)
            If NTSCStr(!pf_codcla2).Trim <> "" Then strSQL += " AND artico.ar_codcla2 = " & CStrSQL(!pf_codcla2)
            If NTSCStr(!pf_codcla3).Trim <> "" Then strSQL += " AND artico.ar_codcla3 = " & CStrSQL(!pf_codcla3)
            If NTSCStr(!pf_codcla4).Trim <> "" Then strSQL += " AND artico.ar_codcla4 = " & CStrSQL(!pf_codcla4)
            If NTSCStr(!pf_codcla5).Trim <> "" Then strSQL += " AND artico.ar_codcla5 = " & CStrSQL(!pf_codcla5)

            If NTSCStr(!pf_filtro).Trim <> "" Then
              strTmp = NTSCStr(!pf_filtro).Trim
              If Microsoft.VisualBasic.Right(strTmp, 1) = "*" Then strTmp = Mid(strTmp, 1, strTmp.Length - 1) & "%"
              strSQL += " AND artico.ar_descr LIKE " & CStrSQL(strTmp)
            End If
            If (NTSCStr(!pf_descrd) <> "".PadLeft(1) And NTSCStr(!pf_descrd) <> "".PadLeft(40)) Or (NTSCStr(!pf_descra).ToLower <> "".PadLeft(40, "z"c)) Then
              strSQL += " AND artico.ar_descr BETWEEN " & CStrSQL(!pf_descrd) & " AND " & CStrSQL(!pf_descra)
            End If
            If NTSCStr(!pf_filtrodb).Trim <> "" Then
              strTmp = NTSCStr(!pf_filtrodb).Trim
              If Microsoft.VisualBasic.Right(strTmp, 1) = "*" Then strTmp = Mid(strTmp, 1, strTmp.Length - 1) & "%"
              strSQL += " AND artico.ar_coddb LIKE " & CStrSQL(strTmp)
            End If
            If NTSCStr(!pf_filtroubic).Trim <> "" Then
              strTmp = NTSCStr(!pf_filtroubic).Trim
              If Microsoft.VisualBasic.Right(strTmp, 1) = "*" Then strTmp = Mid(strTmp, 1, strTmp.Length - 1) & "%"
              strSQL += " AND artico.ar_ubicaz LIKE " & CStrSQL(strTmp)
            End If
            If (NTSCDate(!pf_dataultagd) <> NTSCDate(IntSetDate("01/01/1900"))) Or (NTSCDate(!pf_dataultaga) <> NTSCDate(IntSetDate("31/12/2099"))) Then
              strSQL += " AND artico.ar_ultagg BETWEEN " & CDataSQL(NTSCStr(!pf_dataultagd)) & " AND " & CDataSQL(NTSCStr(!pf_dataultaga))
            End If
            If NTSCStr(!pf_lotti).Trim <> "" Then strSQL += " AND artico.ar_geslotti = " & CStrSQL(!pf_lotti)
            If NTSCStr(!pf_commessa).Trim <> "" Then strSQL += " AND artico.ar_gescomm = " & CStrSQL(!pf_commessa)
            If NTSCStr(!pf_esaurito).Trim <> "" Then strSQL += " AND artico.ar_inesaur = " & CStrSQL(!pf_esaurito)
            If NTSCStr(!pf_varianti).Trim <> "" Then strSQL += " AND artico.ar_gesvar = " & CStrSQL(!pf_varianti)
            If NTSCStr(!pf_matricole).Trim <> "" Then strSQL += " AND artico.ar_gestmatr = " & CStrSQL(!pf_matricole)
            If NTSCStr(!pf_gesubic).Trim <> "" Then strSQL += " AND artico.ar_gesubic = " & CStrSQL(!pf_gesubic)
            If NTSCStr(!pf_gesfasi).Trim <> "" Then strSQL += " AND artico.ar_gesfasi = " & CStrSQL(!pf_gesfasi)
            If NTSCStr(!pf_critico).Trim <> "" Then strSQL += " AND artico.ar_critico = " & CStrSQL(!pf_critico)
            If (NTSCInt(!pf_annod) <> 0) Or (NTSCInt(!pf_annoa) <> 9999) Then
              strSQL += " AND artico.ar_anno BETWEEN " & NTSCInt(!pf_annod) & " AND " & NTSCInt(!pf_annoa)
            End If
            If (NTSCInt(!pf_codstagd) <> 0) Or (NTSCInt(!pf_codstaga) <> 9999) Then
              strSQL += " AND artico.ar_codstag BETWEEN " & NTSCInt(!pf_codstagd) & " AND " & NTSCInt(!pf_codstaga)
            End If
            If (NTSCInt(!pf_codtagld) <> 0) Or (NTSCInt(!pf_codtagla) <> 9999) Then
              strSQL += " AND artico.ar_codtagl BETWEEN " & NTSCInt(!pf_codtagld) & " AND " & NTSCInt(!pf_codtagla)
            End If
          End With
          Select Case NTSCInt(dtrFiltri!pf_codquery)
            Case 12 '--- ARTICO CON SINGOLI MAGAZZINI AD OGGI
              Select Case NTSCInt(dtrFiltri!pf_scorta)
                Case 1 : strSQL += " AND artpro.ap_esist < artico.ar_scomin"
                Case 2 : strSQL += " AND ((artpro.ap_esist + artpro.ap_ordin) - artpro.ap_impeg) < artico.ar_scomin"
                Case 3 : strSQL += " AND (artpro.ap_esist - artpro.ap_impeg) < artico.ar_scomin"
                Case 4 : strSQL += " AND (artpro.ap_esist - artpro.ap_prenot) < artico.ar_scomin"
              End Select
              strTmp = ""
              Select Case NTSCInt(dtrFiltri!pf_esistenza)
                Case 1 : strTmp = ">"
                Case 2 : strTmp = "<"
                Case 3 : strTmp = "="
                Case 4 : strTmp += "<>"
                Case 5 : strTmp = ">="
                Case 6 : strTmp = "<="
              End Select
              If strTmp <> "" Then strSQL += " AND artpro.ap_esist " & strTmp & " 0"
              If (NTSCInt(dtrFiltri!pf_magazini) <> 0) Or (NTSCInt(dtrFiltri!pf_magazfin) <> 9999) Then
                strSQL += " AND artpro.ap_magaz BETWEEN " & NTSCInt(dtrFiltri!pf_magazini) & " AND " & NTSCInt(dtrFiltri!pf_magazfin)
              End If
            Case 13 '--- ARTICO CON TUTTI I MAGAZZINI AD OGGI
              Select Case NTSCInt(dtrFiltri!pf_scorta)
                Case 1 : strSQL += " AND artprox.apx_esist < artico.ar_scomin"
                Case 2 : strSQL += " AND ((artprox.apx_esist + artprox.apx_ordin) - artprox.apx_impeg) < artico.ar_scomin"
                Case 3 : strSQL += " AND (artprox.apx_esist - artprox.apx_impeg) < artico.ar_scomin"
                Case 4 : strSQL += " AND (artprox.apx_esist - artprox.apx_prenot) < artico.ar_scomin"
              End Select
              strTmp = ""
              Select Case NTSCInt(dtrFiltri!pf_esistenza)
                Case 1 : strTmp = ">"
                Case 2 : strTmp = "<"
                Case 3 : strTmp = "="
                Case 4 : strTmp = "<>"
                Case 5 : strTmp = ">="
                Case 6 : strTmp = "<="
              End Select
              If strTmp <> "" Then strSQL += " AND artprox.apx_esist " & strTmp & " 0"
              Select Case NTSCInt(dtrFiltri!pf_ultvendita)
                Case 1, 2
                  strSQL += " AND artprox.apx_dtulsca" & IIf(NTSCInt(dtrFiltri!pf_ultvendita) = 1, " < ", " >= ").ToString & CDataSQL(NTSCStr(dtrFiltri!pf_dtultvendita))
              End Select
              strSQL += " AND artprox.apx_fase BETWEEN 0 AND 9999"
            Case 14 '--- ARTICO CON SINGOLI MAGAZZINI A DATA
              Select Case NTSCInt(dtrFiltri!pf_scorta)
                Case 1 : strSQL += " AND TTARTPRO.ap_esist < artico.ar_scomin"
                Case 2 : strSQL += " AND ((TTARTPRO.ap_esist + TTARTPRO.ap_ordin) - TTARTPRO.ap_impeg) < artico.ar_scomin"
                Case 3 : strSQL += " AND (TTARTPRO.ap_esist - TTARTPRO.ap_impeg) < artico.ar_scomin"
                Case 4 : strSQL += " AND (TTARTPRO.ap_esist - TTARTPRO.ap_prenot) < artico.ar_scomin"
              End Select
              strTmp = ""
              Select Case NTSCInt(dtrFiltri!pf_esistenza)
                Case 1 : strTmp = ">"
                Case 2 : strTmp = "<"
                Case 3 : strTmp = "="
                Case 4 : strTmp = "<>"
                Case 5 : strTmp = ">="
                Case 6 : strTmp = "<="
              End Select
              If strTmp <> "" Then strSQL += " AND TTARTPRO.ap_esist " & strTmp & " 0"
              If (NTSCInt(dtrFiltri!pf_magazini) <> 0) Or (NTSCInt(dtrFiltri!pf_magazfin) <> 9999) Then
                strSQL += " AND TTARTPRO.ap_magaz BETWEEN " & NTSCInt(dtrFiltri!pf_magazini) & " AND " & NTSCInt(dtrFiltri!pf_magazfin)
              End If
              '--- Aggiunge instid
              strSQL += " AND ttartpro.instid = " & lIITTArtpro & _
                " AND ttartprox.instid = " & lIITTArtprox
            Case 15 '--- ARTICO CON TUTTI I MAGAZZINI A DATA
              Select Case NTSCInt(dtrFiltri!pf_scorta)
                Case 1 : strSQL += " AND TTARTPROX.apx_esist < artico.ar_scomin"
                Case 2 : strSQL += " AND ((TTARTPROX.apx_esist + TTARTPROX.apx_ordin) - TTARTPROX.apx_impeg) < artico.ar_scomin"
                Case 3 : strSQL += " AND (TTARTPROX.apx_esist - TTARTPROX.apx_impeg) < artico.ar_scomin"
                Case 4 : strSQL += " AND (TTARTPROX.apx_esist - TTARTPROX.apx_prenot) < artico.ar_scomin"
              End Select
              strTmp = ""
              Select Case NTSCInt(dtrFiltri!pf_esistenza)
                Case 1 : strTmp = ">"
                Case 2 : strTmp = "<"
                Case 3 : strTmp = "="
                Case 4 : strTmp = "<>"
                Case 5 : strTmp = ">="
                Case 6 : strTmp = "<="
              End Select
              If strTmp <> "" Then strSQL += " AND TTARTPROX.apx_esist " & strTmp & " 0"
              Select Case NTSCInt(dtrFiltri!pf_ultvendita)
                Case 1, 2
                  strSQL += " AND TTARTPROX.apx_dtulsca " & IIf(NTSCInt(dtrFiltri!pf_ultvendita) = 1, "< ", ">= ").ToString & CDataSQL(NTSCStr(dtrFiltri!pf_dtultvendita))
              End Select
              '--- Aggiunge instid
              strSQL += " AND TTARTPROX.instid = " & lIITTArtprox & _
                " AND TTARTPROX.apx_fase BETWEEN 0 AND 9999"
            Case 19 '--- ARTICO + LISTINI
              strSQL += " AND listini.lc_datagg <= " & CDataSQL(strData) & _
                " AND listini.lc_datscad >= " & CDataSQL(strData) & _
                " AND listini.lc_codvalu = " & NTSCInt(dtrFiltri!pf_codvalu)
              Select Case NTSCInt(dtrFiltri!pf_tipolistino)
                Case 0
                  strSQL += " AND listini.lc_tipo = ' '" & _
                    " AND listini.lc_listino = " & NTSCInt(dtrFiltri!pf_listino)
                Case 1
                  strSQL += " AND listini.lc_tipo = 'C'" & _
                    " AND listini.lc_listino = 0"
                  If (NTSCInt(dtrFiltri!pf_listconini) <> 0) Or (NTSCInt(dtrFiltri!pf_listconfin) <> 999999999) Then
                    strSQL += " AND listini.lc_conto BETWEEN " & NTSCInt(dtrFiltri!pf_listconini) & " AND " & NTSCInt(dtrFiltri!pf_listconfin)
                  End If
                Case 2
                  strSQL += " AND listini.lc_tipo = 'F'" & _
                    " AND listini.lc_listino = 0"
                  If (NTSCInt(dtrFiltri!pf_listconini) <> 0) Or (NTSCInt(dtrFiltri!pf_listconfin) <> 999999999) Then
                    strSQL += " AND listini.lc_conto BETWEEN " & NTSCInt(dtrFiltri!pf_listconini) & " AND " & NTSCInt(dtrFiltri!pf_listconfin)
                  End If
              End Select
              If NTSCInt(dtrFiltri!pf_promozione) = 1 Then
                strSQL += " AND listini.lc_codtpro = " & NTSCInt(dtrFiltri!pf_codtpro)
              Else
                strSQL += " AND listini.lc_codtpro = 0"
              End If
          End Select
      End Select
      '--------------------------------------------------------------------------------------------------------------
      '--- strSQL = "SELECT (elenco campi) FROM " & strInnerJoin & " WHERE campo_1 BETWEEN 0 AND 9999999"...
      '--------------------------------------------------------------------------------------------------------------
      '--- Se Articoli + Listini e Flag Primo Livello = True cambia i nomi dei campi
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(dtrFiltri!pf_codquery) = 19 Then
        If NTSCInt(dtrFiltri!pf_primoliv) = 1 Then
          lPos = InStr(1, strSQL, "SELECT")
          str1 = Mid(strSQL, (lPos + 6))
          strSQL = "SELECT DISTINCT" & str1
          str1 = "SELECT pfc_nomcampo FROM PARSTCA" & _
            " WHERE pfc_codform = " & NTSCInt(dtrFiltri!pf_codform) & _
            " AND pfc_nomcampo = 'artico.ar_codart'" & _
            " ORDER BY pfc_codform, pfc_riga"
          dttTmp = OpenRecordset(str1, CLE__APP.DBTIPO.DBPRC)
          If dttTmp.Rows.Count > 0 Then
            lPos = InStr(1, strSQL, "artico.ar_codart")
            If lPos <> 0 Then
              str1 = Mid(strSQL, 1, (lPos - 1))
              str2 = Mid(strSQL, (lPos + 17))
              strSQL = str1 & "(artico.ar_codroot + artico.ar_codvar1)," & str2
            End If
          End If
          dttTmp.Clear()
          dttTmp.Dispose()
          lPos = InStr(1, strSQL, "artico.ar_codart = listini.lc_codart")
          If lPos <> 0 Then
            str1 = Mid(strSQL, 1, (lPos - 1))
            str2 = Mid(strSQL, (lPos + 36))
            strSQL = str1 & "(artico.ar_codroot + artico.ar_codvar1) = listini.lc_codart" & str2
          End If
          lPos = InStr(1, strSQL, "artico.ar_codart BETWEEN")
          If lPos <> 0 Then
            str1 = Mid(strSQL, 1, (lPos - 1))
            str2 = Mid(strSQL, (lPos + 16))
            strSQL = str1 & "(artico.ar_codroot + artico.ar_codvar1)" & str2
          End If
          If NTSCStr(dtrFiltri!pf_order) = "artico.ar_codart" Then
            strSQL += " ORDER BY (artico.ar_codroot + artico.ar_codvar1)"
          End If
        Else
          strSQL += " ORDER BY " & NTSCStr(dtrFiltri!pf_order)
        End If
      Else
        strSQL += " ORDER BY " & NTSCStr(dtrFiltri!pf_order)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- strSQL = "SELECT (elenco campi) FROM " & strInnerJoin & " WHERE" & _
      '---   " campo_1 BETWEEN 0 AND 9999999"... & _
      '---   " ORDER BY (elenco campi)"
      '--------------------------------------------------------------------------------------------------------------
      '--- Se articolo + listini, accoda alla ORDER BY:
      '--- lc_fase, lc_codlavo, lc_conto, lc_listino, lc_codvalu, lc_codtpro, lc_daquant, lc_datagg
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(dtrFiltri!pf_codquery) = 19 Then
        'strSQL += ", lc_fase, lc_codlavo, lc_conto, lc_listino, lc_codvalu, lc_codtpro, lc_daquant, lc_datagg"
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function AggiungiFiltriEstensioni(ByRef dtrFiltri As DataRow, ByRef dtrFiltriX As DataRow, _
    ByRef strSQL As String) As Boolean
    Dim bOkCombo1 As Boolean = False
    Dim bOkCombo2 As Boolean = False
    Dim bOkCombo3 As Boolean = False
    Dim i As Integer = 0
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If CBool(GetSettingBus("BS--CLIE", "OPZIONI", ".", "GestAnaExt", "0", " ", "0")) = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il tipo di stampa è su Clienti/Fornitori/Sottoconti,
      '--- se è attiva la gestione delle estensioni,
      '--- se il tipo indicato nell'impostazione filtri è 'Cliente' o 'Fornitore',
      '--- aggiunge il FILTRO sul tipo di ANAEXT x non duplicare i dati
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(dtrFiltri!pf_1tipo)
        Case "C", "F" : strSQL += " AND anaext.ax_tipork IN ('C', 'F')"
        Case Else : Return False
      End Select
      '--------------------------------------------------------------------------------------------------------------   
      If Not dtrFiltriX Is Nothing Then
        For i = 1 To 3
          If NTSCStr(dtrFiltriX("Tipo" & i.ToString)) <> "*" Then
            strSQL += " AND anaext.ax_tipo" & i.ToString & " LIKE " & CStrSQL(dtrFiltriX("Tipo" & i.ToString))
          End If
        Next
        For i = 1 To 10
          If NTSCStr(dtrFiltriX("Descr" & i.ToString)) <> "*" Then
            strTmp = NTSCStr(dtrFiltriX("Descr" & i.ToString))
            If strTmp.Length < 30 Then
              If (Left(strTmp, 1) <> "*") Or (Left(strTmp, 1) <> "?") Then
                If (Right(strTmp, 1) <> "*") And (Right(strTmp, 1) <> "?") Then strTmp += "*"
              End If
              strTmp = strTmp.Replace("*", "%")
              strTmp = strTmp.Replace("?", "_")
            End If
            strSQL += " AND anaext.ax_descr" & i.ToString & " LIKE " & CStrSQL(strTmp)
          End If
        Next
        For i = 1 To 3
          If NTSCStr(dtrFiltriX("Desext" & i.ToString)) <> "*" Then
            strTmp = NTSCStr(dtrFiltriX("Desext" & i.ToString))
            If strTmp.Length < 255 Then
              If (Left(strTmp, 1) <> "*") Or (Left(strTmp, 1) <> "?") Then
                If (Right(strTmp, 1) <> "*") And (Right(strTmp, 1) <> "?") Then strTmp += "*"
              End If
              strTmp = strTmp.Replace("*", "%")
              strTmp = strTmp.Replace("?", "_")
            End If
            strSQL += " AND anaext.ax_desext" & i.ToString & " LIKE " & CStrSQL(strTmp)
          End If
        Next
        For i = 1 To 5
          If NTSCStr(dtrFiltriX("Seldata" & i.ToString)) = "S" Then
            strSQL += " AND ax_data" & i.ToString & " BETWEEN " & CDataSQL(NTSCStr(dtrFiltriX("DataI" & i.ToString))) & " AND " & CDataSQL(NTSCStr(dtrFiltriX("DataF" & i.ToString)))
          End If
        Next
        For i = 1 To 10
          If NTSCStr(dtrFiltriX("SelNum" & i.ToString)) = "S" Then
            strSQL += " AND ax_num" & i.ToString & " BETWEEN " & CDblSQL(NTSCDec(dtrFiltriX("NumI" & i.ToString))) & " AND " & CDblSQL(NTSCDec(dtrFiltriX("NumF" & i.ToString)))
          End If
        Next
        For i = 1 To 10
          If NTSCStr(dtrFiltriX("Check" & i.ToString)) <> "E" Then
            strSQL += " AND ax_check" & i.ToString & " = " & CStrSQL(dtrFiltriX("Check" & i.ToString))
          End If
        Next
        For i = 65 To 76
          If (i <> 74) And (i <> 75) Then
            If NTSCStr(dtrFiltriX("Combo1" & Chr(i))) <> "N" Then
              bOkCombo1 = True
              Exit For
            End If
          End If
        Next
        If bOkCombo1 = True Then
          For i = 65 To 76
            If (i <> 74) And (i <> 75) Then
              If NTSCStr(dtrFiltriX("Combo1" & Chr(i))) = "N" Then strSQL += " AND ax_combo1 <> " & CStrSQL(Chr(i))
            End If
          Next
        End If
        For i = 65 To 69
          If NTSCStr(dtrFiltriX("Combo2" & Chr(i))) <> "N" Then
            bOkCombo2 = True
            Exit For
          End If
        Next
        If bOkCombo2 = True Then
          For i = 65 To 69
            If NTSCStr(dtrFiltriX("Combo2" & Chr(i))) = "N" Then strSQL += " AND ax_combo2 <> " & CStrSQL(Chr(i))
          Next
        End If
        For i = 65 To 86
          If (i <> 74) And (i <> 75) Then
            If NTSCStr(dtrFiltriX("Combo3" & Chr(i))) <> "N" Then
              bOkCombo3 = True
              Exit For
            End If
          End If
        Next
        If bOkCombo3 = True Then
          For i = 65 To 86
            If (i <> 74) And (i <> 75) Then
              If NTSCStr(dtrFiltriX("Combo3" & Chr(i))) = "N" Then strSQL += " AND ax_combo3 <> " & CStrSQL(Chr(i))
            End If
          Next
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ApriParstaf(ByVal lCodform As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM PARSTAF" & _
        " WHERE pf_codform = " & lCodform & _
        " ORDER BY pf_codform"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      If dttOut.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function ApriParstafx(ByVal lCodform As Integer, ByRef dttOut As DataTable) As Boolean
    Dim bResult As Boolean = False
    Dim i As Integer = 0
    Dim strSQL As String = ""
    Dim strNum As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM PARSTAFX" & _
        " WHERE px_codform = " & lCodform
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      If dttOut.Rows.Count = 0 Then bResult = False Else bResult = True
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To (dttOut.Columns.Count - 1)
        With dttOut.Columns(i)
          '--------------------------------------------------------------------------------------------------------
          strNum = Right(.ColumnName, 1)
          If NTSCInt(strNum) = 0 Then strNum = "10"
          '--------------------------------------------------------------------------------------------------------
          If Mid(.ColumnName.ToLower, 1, 6) = "px_num" Then
            strNum = IIf(Mid(.ColumnName.ToLower, 1, 8) = "px_num10", "10", Mid(.ColumnName, 7, 1)).ToString
            Select Case Right(.ColumnName.ToLower, 1)
              Case "i" : .ColumnName = "NumI" & strNum
              Case "f" : .ColumnName = "NumF" & strNum
            End Select
            GoTo Salta
          End If
          If Mid(.ColumnName.ToLower, 1, 7) = "px_tipo" Then
            .ColumnName = "Tipo" & strNum
            GoTo Salta
          End If
          If Mid(.ColumnName.ToLower, 1, 7) = "px_data" Then
            Select Case Right(.ColumnName.ToLower, 1)
              Case "i" : .ColumnName = "DataI" & Mid(.ColumnName, 8, 1)
              Case "f" : .ColumnName = "DataF" & Mid(.ColumnName, 8, 1)
            End Select
            GoTo Salta
          End If
          If Mid(.ColumnName.ToLower, 1, 8) = "px_descr" Then
            .ColumnName = "Descr" & strNum
            GoTo Salta
          End If
          If Mid(.ColumnName.ToLower, 1, 8) = "px_check" Then
            .ColumnName = "Check" & strNum
            GoTo Salta
          End If
          If Mid(.ColumnName.ToLower, 1, 8) = "px_combo" Then
            .ColumnName = "Combo" & Mid(.ColumnName, 9, 1) & Right(.ColumnName, 1).ToUpper
            GoTo Salta
          End If
          If Mid(.ColumnName.ToLower, 1, 9) = "px_desext" Then
            .ColumnName = "Desext" & strNum
            GoTo salta
          End If
          If Mid(.ColumnName.ToLower, 1, 9) = "px_selnum" Then
            .ColumnName = "SelNum" & strNum
            GoTo Salta
          End If
          If Mid(.ColumnName.ToLower, 1, 10) = "px_seldata" Then
            .ColumnName = "SelData" & strNum
            GoTo Salta
          End If
          '----------------------------------------------------------------------------------------------------------
Salta:
          '----------------------------------------------------------------------------------------------------------
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return False
    End Try
  End Function
  Public Overridable Function ApriParstca(ByVal lCodform As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM PARSTCA" & _
        " WHERE pfc_codform = " & lCodform & _
        " AND LTRIM(RTRIM(pfc_nomcampo)) <> ''" & _
        " ORDER BY pfc_codform, pfc_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      If dttOut.Rows.Count = 0 Then
        dttOut.Clear()
        dttOut.Dispose()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function ApriTTSTAF(ByVal strDitta As String, ByRef lIITTstaf As Integer, _
    ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM TTSTAF" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTstaf & _
        " ORDER BY codditt, instid, tt_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttOut.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function AggiornaTTSTAF(ByVal strDitta As String, ByRef lIITTstaf As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM TTSTAF" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTstaf
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dttTmp.Rows.Count - 1)
        strSQL = "UPDATE TTSTAF" & _
          " SET tt_desstampa = " & CStrSQL(Mid(NTSCStr(dttTmp.Rows(i)!tt_desstampa), 10)) & _
          " WHERE codditt = " & CStrSQL(dttTmp.Rows(i)!codditt) & _
          " AND instid = " & NTSCInt(dttTmp.Rows(i)!instid) & _
          " AND tt_riga = " & NTSCInt(dttTmp.Rows(i)!tt_riga)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function AggiornaTTSTAFMarcatori(ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dttTmp.Rows.Count - 1)
        strSQL = "UPDATE TTSTAF" & _
          " SET tt_desstampa = " & CStrSQL(dttTmp.Rows(i)!tt_desstampa) & _
          " WHERE codditt = " & CStrSQL(dttTmp.Rows(i)!codditt) & _
          " AND instid = " & NTSCInt(dttTmp.Rows(i)!instid) & _
          " AND tt_riga = " & NTSCInt(dttTmp.Rows(i)!tt_riga)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function AggiungiRigaTotali(ByVal strDitta As String, ByRef lIITTstaf As Integer, _
    ByRef dtrParstaf As DataRow, ByRef strSomma As String, ByRef strTemp As String, _
    ByRef dTOTSaldoContabile As Decimal, ByRef dTOTRDinDistinta As Decimal, ByRef dTOTRBrischio As Decimal, _
    ByRef dTOTRischioPrimo As Decimal, ByRef dTOTFattnoncont As Decimal, ByRef dTOTBolledaFatt As Decimal, _
    ByRef dTOTOrdinidaEvad As Decimal, ByRef dTOTRischioTotale As Decimal, ByRef dTOTSforamento As Decimal, _
    ByRef dTOTRDScadute As Decimal, ByRef dTOTImpinsol12mesi As Decimal, ByRef dTOTImpinsolinessere As Decimal, _
    ByRef dTOTFattTotale As Decimal, ByRef lTOTNinsol12mesi As Integer, ByRef lTOTNinsolinessere As Integer) As Boolean
    Dim nCol As Integer = 0
    Dim ii As Integer = 0
    Dim lFROM As Integer = 0
    Dim lORDER As Integer = 0
    Dim lUltimaRiga As Integer = 1
    Dim strSQL As String = ""
    Dim strNomeCampo As String = ""
    Dim FORMAT_QTA As String = "#,##0.000"
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se è stato richiesto il totale fa un ciclo per riempire un'ulteriore stringa (finale)
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(dtrParstaf!pf_statot) <> "S") Or (strSomma.Trim = "") Then Return True
      '--------------------------------------------------------------------------------------------------------------
      lFROM = InStr(1, strTemp, "FROM")
      If NTSCInt(dtrParstaf!pf_codquery) = 2 Then
        lORDER = InStr(1, strTemp, "GROUP")
      Else
        lORDER = InStr(1, strTemp, "ORDER")
      End If
      strSomma += " " & Mid(strTemp, lFROM, (lORDER - lFROM)).Trim
      '--------------------------------------------------------------------------------------------------------------
      '--- Se ci sono campi calcolati (es: #dSaldoContabile) passa il valore accumulato
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(dtrParstaf!pf_codquery) = 3 Then
        strSomma = SostituisciCampi(strSomma, dTOTSaldoContabile, dTOTRDinDistinta, dTOTRBrischio, _
          dTOTRischioPrimo, dTOTFattnoncont, dTOTBolledaFatt, dTOTOrdinidaEvad, dTOTRischioTotale, _
          dTOTSforamento, dTOTRDScadute, dTOTImpinsol12mesi, dTOTImpinsolinessere, dTOTFattTotale, _
          lTOTNinsol12mesi, lTOTNinsolinessere)
      End If
      '--------------------------------------------------------------------------------------------------------------
      dttTmp = OpenRecordset(strSomma, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows(0)(0).Equals(DBNull.Value) Then Return True
      '--------------------------------------------------------------------------------------------------------------
      For nRow As Integer = 0 To (dttTmp.Rows.Count - 1)
        For nCol = 0 To (dttTmp.Columns.Count - 1)
          strNomeCampo = NTSCStr(dttTmp.Columns(nCol).ColumnName)
          Select Case Left(strNomeCampo, 1)
            Case "Q", "V" : ii = NTSCInt(Mid(strNomeCampo, InStr(strNomeCampo, "_") + 1))
            Case Else : ii = 0
          End Select
          Select Case Left(strNomeCampo, 1)
            Case "Q"
              If nCol = 0 Then
                strSomma = Right("".PadLeft(ii) & Format(dttTmp.Rows(nRow)(nCol), FORMAT_QTA), ii) & " "
              Else
                strSomma += Right("".PadLeft(ii) & Format(dttTmp.Rows(nRow)(nCol), FORMAT_QTA), ii) & " "
              End If
            Case "V"
              If nCol = 0 Then
                strSomma = Right("".PadLeft(ii) & Format(dttTmp.Rows(nRow)(nCol), FORMAT_LIREUR), ii) & " "
              Else
                strSomma += Right("".PadLeft(ii) & Format(dttTmp.Rows(nRow)(nCol), FORMAT_LIREUR), ii) & " "
              End If
            Case Else
              strSomma = IIf(nCol = 0, dttTmp.Rows(nRow)(nCol), strSomma & NTSCStr(dttTmp.Rows(nRow)(nCol))).ToString & " "
          End Select
        Next
      Next
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT MAX(tt_riga) As Riga FROM TTSTAF" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTstaf
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count <> 0 Then
        If Not dttTmp.Rows(0)!Riga.Equals(DBNull.Value) Then lUltimaRiga = (NTSCInt(dttTmp.Rows(0)!Riga) + 1)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strTemp = ""
      For ii = 1 To strSomma.Length
        strTemp += "_"
      Next
      '--------------------------------------------------------------------------------------------------------------
      For ii = 1 To 2
        strSQL = "INSERT INTO TTSTAF (codditt, instid, tt_riga, tt_desstampa)" & _
          " VALUES (" & CStrSQL(strDitta) & ", " & lIITTstaf & ", " & _
          lUltimaRiga + NTSCInt(IIf(ii = 1, 0, 1)) & ", " & _
          CStrSQL(IIf(ii = 1, "{@@@}", strSomma).ToString) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function Aggzzdcst(ByVal strDitta As String, ByVal lConto As Integer, _
    ByVal dSaldocont As Decimal, ByVal dRdindist As Decimal, ByVal dRbrischio As Decimal, _
    ByVal dRischiopr As Decimal, ByVal dFattnonc As Decimal, ByVal dBolledaf As Decimal, _
    ByVal dOrddaev As Decimal, ByVal dRischiotot As Decimal, ByVal dSforam As Decimal, _
    ByVal dRdscadute As Decimal, ByVal dImpinsol12 As Decimal, ByVal dImpinsoles As Decimal, _
    ByVal dFatttot As Decimal, ByVal lNinsol12 As Integer, ByVal lNinsoles As Integer, ByVal lTmpagam As Integer, _
    ByVal lTmritard As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO zzdcst (codditt, tts_conto, tts_saldocont, tts_rdindist, tts_rbrischio, tts_rischiopr," & _
        " tts_fattnonc, tts_bolledaf, tts_orddaev, tts_rischiotot, tts_sforam, tts_rdscadute, tts_impinsol12," & _
        " tts_impinsoles, tts_fatttot, tts_ninsol12, tts_ninsoles, tts_tmpagam, tts_tmritard)" & _
        " VALUES (" & CStrSQL(strDitta) & ", " & lConto & ", " & CDblSQL(dSaldocont) & ", " & _
        CDblSQL(dRdindist) & ", " & CDblSQL(dRbrischio) & ", " & CDblSQL(dRischiopr) & ", " & _
        CDblSQL(dFattnonc) & ", " & CDblSQL(dBolledaf) & ", " & CDblSQL(dOrddaev) & ", " & _
        CDblSQL(dRischiotot) & ", " & CDblSQL(dSforam) & ", " & CDblSQL(dRdscadute) & ", " & _
        CDblSQL(dImpinsol12) & ", " & CDblSQL(dImpinsoles) & ", " & CDblSQL(dFatttot) & ", " & _
        lNinsol12 & ", " & lNinsoles & ", " & lTmpagam & ", " & lTmritard & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CaricaCbParstaf(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strElencoCodici As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Legge l'opzione di registro che contiene l'elenco dei codici che possono apparire
      '--- nel ComboBox della lista delle stampe impostate
      '--------------------------------------------------------------------------------------------------------------
      strElencoCodici = GetSettingBus("BS--SPAF", "OPZIONIUT", ".", "Canexecute", "", " ", "").Trim
      '--------------------------------------------------------------------------------------------------------------
      If strElencoCodici <> "" Then
        '--- Sostituisce i punto e virgola con virgole
        strElencoCodici = strElencoCodici.Replace(";", ",").Trim
        '--- Toglie l'eventuale virgola finale
        If strElencoCodici.Length > 0 Then
          If Microsoft.VisualBasic.Right(strElencoCodici, 1) = "," Then
            strElencoCodici = Mid(strElencoCodici, 1, (Len(strElencoCodici) - 1)).Trim
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT pf_codform, (CONVERT(VARCHAR, pf_codform) + ' - ' + pf_titstam) As Titolo" & _
        " FROM PARSTAF" & _
        IIf(strElencoCodici <> "", " WHERE pf_codform IN (" & strElencoCodici & ")", "").ToString & _
        " ORDER BY pf_codform"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
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
    End Try
  End Function

  Public Overridable Function ContoMovimentato(ByVal strDitta As String, ByRef lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      ContoMovimentato = False
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 pn_conto FROM prinot" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND pn_conto = " & lConto & _
        " ORDER BY codditt, pn_datreg, pn_numreg, pn_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 sc_conto FROM scaden" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND sc_conto = " & lConto & _
        " ORDER BY codditt, sc_conto, sc_annpar, sc_alfpar, sc_numpar, sc_numrata, sc_integr"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 mi_contocf FROM moviva" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mi_contocf = " & lConto & _
        " ORDER BY codditt, mi_datreg, mi_numreg, mi_riga, mi_rigareg"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tm_conto FROM testmag" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tm_conto = " & lConto & _
        " ORDER BY codditt, tm_tipork, tm_anno, tm_serie, tm_numdoc"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 td_conto FROM testord" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND td_conto = " & lConto & _
        " ORDER BY codditt, td_tipork, td_anno, td_serie, td_numord"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      Return False
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function GetQueryPredefinita(ByRef dtrParstaf As DataRow, ByRef dtrFiltri As DataRow, _
    ByRef bGestAnaext As Boolean, ByRef strQuery As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strQuery = ""
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(dtrParstaf!pf_codquery)
        Case 1
          strQuery = "anagra LEFT JOIN anagra AS anagra_1 ON anagra.codditt = anagra_1.codditt AND anagra.an_controp = anagra_1.an_conto" & _
            " LEFT JOIN tabzone ON anagra.codditt = tabzone.codditt AND anagra.an_zona = tabzone.tb_codzone" & _
            " LEFT JOIN tabcate ON anagra.codditt = tabcate.codditt AND anagra.an_categ = tabcate.tb_codcate" & _
            " LEFT JOIN tabciva ON anagra.an_codese = tabciva.tb_codciva" & _
            " LEFT JOIN tabpaga ON anagra.an_codpag = tabpaga.tb_codpaga" & _
            " LEFT JOIN tabcage ON anagra.codditt = tabcage.codditt AND anagra.an_agente = tabcage.tb_codcage" & _
            " LEFT JOIN tabvalu ON anagra.an_valuta = tabvalu.tb_codvalu" & _
            " LEFT JOIN tabport ON anagra.codditt = tabport.codditt AND anagra.an_porto = tabport.tb_codport" & _
            " LEFT JOIN tabvett ON anagra.codditt = tabvett.codditt AND anagra.an_vett = tabvett.tb_codvett" & _
            " LEFT JOIN destdiv ON anagra.codditt = destdiv.codditt AND anagra.an_conto = destdiv.dd_conto AND anagra.an_destin = destdiv.dd_coddest" & _
            " LEFT JOIN tabstat ON anagra.an_stato = tabstat.tb_codstat" & _
            " LEFT JOIN tabcage AS tabcage_1 ON anagra.codditt = tabcage_1.codditt AND anagra.an_agente2 = tabcage_1.tb_codcage" & _
            " LEFT JOIN tabmast ON anagra.an_codmast = tabmast.tb_codmast and anagra.an_codpcon = tabmast.tb_codpcon" & _
            " LEFT JOIN tabling ON anagra.an_codling = tabling.tb_codling" & _
            " LEFT JOIN tabbanc ON anagra.codditt = tabbanc.codditt AND anagra.an_codbanc = tabbanc.tb_codbanc"
        Case 2
          strQuery = "anagra LEFT JOIN anagra AS anagra_1 ON anagra.codditt = anagra_1.codditt AND anagra.an_controp = anagra_1.an_conto" & _
            " LEFT JOIN tabzone ON anagra.codditt = tabzone.codditt AND anagra.an_zona = tabzone.tb_codzone" & _
            " LEFT JOIN tabcate ON anagra.codditt = tabcate.codditt AND anagra.an_categ = tabcate.tb_codcate" & _
            " LEFT JOIN tabciva ON anagra.an_codese = tabciva.tb_codciva" & _
            " LEFT JOIN tabpaga ON anagra.an_codpag = tabpaga.tb_codpaga" & _
            " LEFT JOIN tabcage ON anagra.codditt = tabcage.codditt AND anagra.an_agente = tabcage.tb_codcage" & _
            " LEFT JOIN tabvalu ON anagra.an_valuta = tabvalu.tb_codvalu" & _
            " LEFT JOIN tabport ON anagra.codditt = tabport.codditt AND anagra.an_porto = tabport.tb_codport" & _
            " LEFT JOIN tabvett ON anagra.codditt = tabvett.codditt AND anagra.an_vett = tabvett.tb_codvett" & _
            " LEFT JOIN destdiv ON anagra.codditt = destdiv.codditt AND anagra.an_conto = destdiv.dd_conto AND anagra.an_destin = destdiv.dd_coddest" & _
            " LEFT JOIN tabstat ON anagra.an_stato = tabstat.tb_codstat" & _
            " LEFT JOIN tabcage AS tabcage_1 ON anagra.codditt = tabcage_1.codditt AND anagra.an_agente2 = tabcage_1.tb_codcage" & _
            " LEFT JOIN tabmast ON anagra.an_codmast = tabmast.tb_codmast and anagra.an_codpcon = tabmast.tb_codpcon" & _
            " LEFT JOIN tabling ON anagra.an_codling = tabling.tb_codling" & _
            " LEFT JOIN tabbanc ON anagra.codditt = tabbanc.codditt AND anagra.an_codbanc = tabbanc.tb_codbanc" & _
            " LEFT JOIN prinot ON anagra.codditt = prinot.codditt AND anagra.an_conto = prinot.pn_conto"
        Case 3
          strQuery = "anagra LEFT JOIN anagra AS anagra_1 ON anagra.codditt = anagra_1.codditt AND anagra.an_controp = anagra_1.an_conto" & _
            " LEFT JOIN tabzone ON anagra.codditt = tabzone.codditt AND anagra.an_zona = tabzone.tb_codzone" & _
            " LEFT JOIN tabcate ON anagra.codditt = tabcate.codditt AND anagra.an_categ = tabcate.tb_codcate" & _
            " LEFT JOIN tabciva ON anagra.an_codese = tabciva.tb_codciva" & _
            " LEFT JOIN tabpaga ON anagra.an_codpag = tabpaga.tb_codpaga" & _
            " LEFT JOIN tabcage ON anagra.codditt = tabcage.codditt AND anagra.an_agente = tabcage.tb_codcage" & _
            " LEFT JOIN tabvalu ON anagra.an_valuta = tabvalu.tb_codvalu" & _
            " LEFT JOIN tabport ON anagra.codditt = tabport.codditt AND anagra.an_porto = tabport.tb_codport" & _
            " LEFT JOIN tabvett ON anagra.codditt = tabvett.codditt AND anagra.an_vett = tabvett.tb_codvett" & _
            " LEFT JOIN destdiv ON anagra.codditt = destdiv.codditt AND anagra.an_destin = destdiv.dd_coddest AND anagra.an_conto = destdiv.dd_conto" & _
            " LEFT JOIN tabstat ON anagra.an_stato = tabstat.tb_codstat" & _
            " LEFT JOIN tabcage AS tabcage_1 ON anagra.codditt = tabcage_1.codditt AND anagra.an_agente2 = tabcage_1.tb_codcage" & _
            " LEFT JOIN tabmast ON anagra.an_codmast = tabmast.tb_codmast AND anagra.an_codpcon = tabmast.tb_codpcon" & _
            " LEFT JOIN tabling ON anagra.an_codling = tabling.tb_codling" & _
            " LEFT JOIN tabbanc ON anagra.codditt = tabbanc.codditt  AND anagra.an_codbanc = tabbanc.tb_codbanc"
        Case 11
          strQuery = "artico LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva" & _
            " LEFT JOIN tabpdon ON artico.codditt = tabpdon.codditt AND artico.ar_codpdon = tabpdon.tb_codpdon" & _
            " LEFT JOIN tabcove ON artico.codditt = tabcove.codditt AND artico.ar_controa = tabcove.tb_codcove" & _
            " LEFT JOIN tabcove AS tabcove_1 ON artico.codditt = tabcove_1.codditt AND artico.ar_controp = tabcove_1.tb_codcove" & _
            " LEFT JOIN tabcove AS tabcove_2 ON artico.codditt = tabcove_2.codditt AND artico.ar_contros = tabcove_2.tb_codcove" & _
            " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam" & _
            " LEFT JOIN anagra ON artico.codditt = anagra.codditt AND artico.ar_forn = anagra.an_conto" & _
            " LEFT JOIN anagra AS anagra_1 ON artico.codditt = anagra_1.codditt  AND artico.ar_forn2 = anagra_1.an_conto" & _
            " LEFT JOIN tabgmer ON artico.codditt = tabgmer.codditt AND artico.ar_gruppo = tabgmer.tb_codgmer" & _
            " LEFT JOIN tabsgme ON artico.codditt = tabsgme.codditt  AND artico.ar_sotgru = tabsgme.tb_codsgme" & _
            " LEFT JOIN artico AS artico_1 ON artico.codditt = artico_1.codditt AND artico.ar_sostit = artico_1.ar_codart" & _
            " LEFT JOIN artico AS artico_2 ON artico.codditt= artico_2.codditt AND artico.ar_sostituito = artico_2.ar_codart" & _
            " LEFT JOIN tabtpro ON artico.codditt = tabtpro.codditt AND artico.ar_tipprom = tabtpro.tb_codtpro" & _
            " LEFT JOIN tabtipa ON artico.codditt = tabtipa.codditt AND artico.ar_codtipa = tabtipa.tb_codtipa" & _
            " LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart"
        Case 12
          strQuery = "artico LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva" & _
            " LEFT JOIN tabpdon ON artico.codditt = tabpdon.codditt AND artico.ar_codpdon = tabpdon.tb_codpdon" & _
            " LEFT JOIN tabcove ON artico.codditt = tabcove.codditt AND artico.ar_controa = tabcove.tb_codcove" & _
            " LEFT JOIN tabcove AS tabcove_1 ON artico.codditt = tabcove_1.codditt AND artico.ar_controp = tabcove_1.tb_codcove" & _
            " LEFT JOIN tabcove AS tabcove_2 ON artico.codditt = tabcove_2.codditt AND artico.ar_contros = tabcove_2.tb_codcove" & _
            " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam" & _
            " LEFT JOIN anagra ON artico.codditt = anagra.codditt AND artico.ar_forn = anagra.an_conto" & _
            " LEFT JOIN anagra AS anagra_1 ON artico.codditt = anagra_1.codditt AND artico.ar_forn2 = anagra_1.an_conto" & _
            " LEFT JOIN tabgmer ON artico.codditt = tabgmer.codditt AND artico.ar_gruppo = tabgmer.tb_codgmer" & _
            " LEFT JOIN tabsgme ON artico.codditt = tabsgme.codditt AND  artico.ar_sotgru = tabsgme.tb_codsgme" & _
            " LEFT JOIN artico AS artico_1 ON artico.codditt = artico_1.codditt AND  artico.ar_sostit = artico_1.ar_codart" & _
            " LEFT JOIN artico AS artico_2 ON artico.codditt = artico_2.codditt AND artico.ar_sostituito = artico_2.ar_codart" & _
            " LEFT JOIN tabtpro ON artico.codditt= tabtpro.codditt AND artico.ar_tipprom = tabtpro.tb_codtpro" & _
            " LEFT JOIN artprox ON artico.codditt = artprox.codditt AND artico.ar_codart = artprox.apx_codart" & _
            " LEFT JOIN artfasi ON artprox.codditt = artfasi.codditt AND artprox.apx_codart = artfasi.af_codart AND artprox.apx_fase = artfasi.af_fase" & _
            " LEFT JOIN artpro ON artprox.codditt = artpro.codditt AND artprox.apx_fase = artpro.ap_fase AND artprox.apx_codart = artpro.ap_codart" & _
            " LEFT JOIN tabmaga ON artpro.ap_magaz = tabmaga.tb_codmaga AND artpro.codditt = tabmaga.codditt"
        Case 13
          strQuery = "artico LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva" & _
            " LEFT JOIN tabpdon ON artico.codditt = tabpdon.codditt AND artico.ar_codpdon = tabpdon.tb_codpdon" & _
            " LEFT JOIN tabcove ON artico.codditt = tabcove.codditt AND artico.ar_controa = tabcove.tb_codcove" & _
            " LEFT JOIN tabcove AS tabcove_1 ON artico.codditt = tabcove_1.codditt AND artico.ar_controp = tabcove_1.tb_codcove" & _
            " LEFT JOIN tabcove AS tabcove_2 ON artico.codditt = tabcove_2.codditt AND artico.ar_contros = tabcove_2.tb_codcove" & _
            " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam" & _
            " LEFT JOIN anagra ON artico.codditt = anagra.codditt AND artico.ar_forn = anagra.an_conto" & _
            " LEFT JOIN anagra AS anagra_1 ON artico.codditt = anagra_1.codditt AND artico.ar_forn2 = anagra_1.an_conto" & _
            " LEFT JOIN tabgmer ON artico.codditt = tabgmer.codditt AND artico.ar_gruppo = tabgmer.tb_codgmer" & _
            " LEFT JOIN tabsgme ON artico.codditt = tabsgme.codditt AND artico.ar_sotgru = tabsgme.tb_codsgme" & _
            " LEFT JOIN artico AS artico_1 ON artico.codditt = artico_1.codditt AND artico.ar_sostit = artico_1.ar_codart" & _
            " LEFT JOIN artico AS artico_2 ON artico.codditt = artico_2.codditt AND artico.ar_sostituito = artico_2.ar_codart" & _
            " LEFT JOIN tabtpro ON artico.codditt = tabtpro.codditt AND artico.ar_tipprom = tabtpro.tb_codtpro" & _
            " LEFT JOIN tabtipa ON artico.codditt = tabtipa.codditt AND artico.ar_codtipa = tabtipa.tb_codtipa" & _
            " LEFT JOIN artprox ON artico.codditt = artprox.codditt AND artico.ar_codart = artprox.apx_codart" & _
            " LEFT JOIN artfasi ON artprox.codditt = artfasi.codditt AND artprox.apx_codart = artfasi.af_codart AND artprox.apx_fase = artfasi.af_fase"
        Case 14
          strQuery = "artico LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva" & _
            " LEFT JOIN tabpdon ON artico.codditt = tabpdon.codditt AND artico.ar_codpdon = tabpdon.tb_codpdon" & _
            " LEFT JOIN tabcove ON artico.codditt = tabcove.codditt AND artico.ar_controa = tabcove.tb_codcove" & _
            " LEFT JOIN tabcove AS tabcove_1 ON artico.codditt = tabcove_1.codditt AND artico.ar_controp = tabcove_1.tb_codcove" & _
            " LEFT JOIN tabcove AS tabcove_2 ON artico.codditt = tabcove_2.codditt AND artico.ar_contros = tabcove_2.tb_codcove" & _
            " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam" & _
            " LEFT JOIN anagra ON artico.codditt = anagra.codditt AND artico.ar_forn = anagra.an_conto" & _
            " LEFT JOIN anagra AS anagra_1 ON artico.codditt = anagra_1.codditt AND artico.ar_forn2 = anagra_1.an_conto" & _
            " LEFT JOIN tabgmer ON artico.codditt = tabgmer.codditt AND artico.ar_gruppo = tabgmer.tb_codgmer" & _
            " LEFT JOIN tabsgme ON artico.codditt = tabsgme.codditt AND artico.ar_sotgru = tabsgme.tb_codsgme" & _
            " LEFT JOIN artico AS artico_1 ON artico.codditt = artico_1.codditt AND artico.ar_sostit = artico_1.ar_codart" & _
            " LEFT JOIN artico AS artico_2 ON artico.codditt = artico_2.codditt AND artico.ar_sostituito = artico_2.ar_codart" & _
            " LEFT JOIN tabtpro ON artico.codditt = tabtpro.codditt AND artico.ar_tipprom = tabtpro.tb_codtpro" & _
            " LEFT JOIN TTARTPROX ON artico.codditt = TTARTPROX.codditt AND artico.ar_codart = TTARTPROX.apx_codart" & _
            " LEFT JOIN TTARTPRO ON TTARTPROX.codditt = TTARTPRO.codditt AND TTARTPROX.apx_codart = TTARTPRO.ap_codart AND TTARTPROX.apx_fase = TTARTPRO.ap_fase" & _
            " LEFT JOIN tabmaga ON TTARTPRO.codditt = tabmaga.codditt AND TTARTPRO.ap_magaz = tabmaga.tb_codmaga" & _
            " LEFT JOIN artfasi ON TTARTPROX.codditt = artfasi.codditt AND TTARTPROX.apx_codart = artfasi.af_codart AND TTARTPROX.apx_fase = artfasi.af_fase"
        Case 15
          strQuery = "artico LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva" & _
            " LEFT JOIN tabpdon ON artico.codditt = tabpdon.codditt AND artico.ar_codpdon = tabpdon.tb_codpdon" & _
            " LEFT JOIN tabcove ON artico.codditt = tabcove.codditt AND artico.ar_controa = tabcove.tb_codcove" & _
            " LEFT JOIN tabcove AS tabcove_1 ON artico.codditt = tabcove_1.codditt AND artico.ar_controp = tabcove_1.tb_codcove" & _
            " LEFT JOIN tabcove AS tabcove_2 ON artico.codditt = tabcove_2.codditt AND artico.ar_contros = tabcove_2.tb_codcove" & _
            " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam" & _
            " LEFT JOIN anagra ON artico.codditt = anagra.codditt AND artico.ar_forn = anagra.an_conto" & _
            " LEFT JOIN anagra AS anagra_1 ON artico.codditt = anagra_1.codditt AND artico.ar_forn2 = anagra_1.an_conto" & _
            " LEFT JOIN tabgmer ON artico.codditt = tabgmer.codditt AND artico.ar_gruppo = tabgmer.tb_codgmer" & _
            " LEFT JOIN tabsgme ON artico.codditt = tabsgme.codditt AND artico.ar_sotgru = tabsgme.tb_codsgme" & _
            " LEFT JOIN artico AS artico_1 ON artico.codditt = artico_1.codditt AND artico.ar_sostit = artico_1.ar_codart" & _
            " LEFT JOIN artico AS artico_2 ON artico.codditt = artico_2.codditt AND artico.ar_sostituito = artico_2.ar_codart" & _
            " LEFT JOIN tabtpro ON artico.codditt = tabtpro.codditt AND artico.ar_tipprom = tabtpro.tb_codtpro" & _
            " LEFT JOIN tabtipa ON artico.codditt = tabtipa.codditt AND artico.ar_codtipa = tabtipa.tb_codtipa" & _
            " LEFT JOIN TTARTPROX ON artico.codditt = TTARTPROX.codditt AND artico.ar_codart = TTARTPROX.apx_codart" & _
            " LEFT JOIN artfasi ON TTARTPROX.codditt = artfasi.codditt AND TTARTPROX.apx_codart = artfasi.af_codart AND TTARTPROX.apx_fase = artfasi.af_fase"
        Case 19
          strQuery = "artico LEFT JOIN listini ON artico.codditt = listini.codditt AND artico.ar_codart = listini.lc_codart" & _
            " LEFT JOIN artfasi ON listini.codditt = artfasi.codditt AND listini.lc_codart = artfasi.af_codart AND listini.lc_fase = artfasi.af_fase"
          strSQL = "SELECT * FROM PARSTCA" & _
            " WHERE pfc_codform = " & NTSCInt(dtrParstaf!pf_codform) & _
            " ORDER BY pfc_codform, pfc_riga"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
          For i As Integer = 0 To (dttTmp.Rows.Count - 1)
            Select Case NTSCStr(dttTmp.Rows(i)!pfc_nomcampo).ToLower
              Case "anagra.an_descr1"
                strQuery += " LEFT JOIN anagra ON artico.codditt = anagra.codditt AND artico.ar_forn = anagra.an_conto"
              Case "anagra_1.an_descr1"
                strQuery += " LEFT JOIN anagra As anagra_1 ON artico.codditt = anagra_1.codditt AND artico.ar_forn2 = anagra_1.an_conto"
              Case "artico_1.ar_descr"
                strQuery += " LEFT JOIN artico As artico_1 ON artico.codditt = artico_1.codditt AND artico.ar_sostit = artico_1.ar_codart"
              Case "artico_2.ar_descr"
                strQuery += " LEFT JOIN artico As artico_2 ON artico.codditt = artico_2.codditt AND artico.ar_sostituito = artico_2.ar_codart"
              Case "tabcfam.tb_descfam"
                strQuery += " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam"
              Case "tabciva.tb_desciva"
                strQuery += " LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva"
              Case "tabcove.tb_descove"
                strQuery += " LEFT JOIN tabcove ON artico.codditt = tabcove.codditt AND artico.ar_controa = tabcove.tb_codcove"
              Case "tabcove_1.tb_descove"
                strQuery += " LEFT JOIN tabcove As tabcove_1 ON artico.codditt = tabcove_1.codditt AND artico.ar_controp = tabcove_1.tb_codcove"
              Case "tabcove_2.tb_descove"
                strQuery += " LEFT JOIN tabcove As tabcove_2 ON artico.codditt = tabcove_2.codditt AND artico.ar_controp = tabcove_2.tb_codcove"
              Case "tabgmer.tb_desgmer"
                strQuery += " LEFT JOIN tabgmer ON artico.codditt = tabgmer.codditt AND artico.ar_gruppo = tabgmer.tb_codgmer"
              Case "tabpdon.tb_despdon"
                strQuery += " LEFT JOIN tabpdon ON artico.codditt = tabpdon.codditt AND artico.ar_codpdon = tabpdon.tb_codpdon"
              Case "tabsgme.tb_dessgme"
                strQuery += " LEFT JOIN tabsgme ON artico.codditt = tabsgme.codditt AND artico.ar_sotgru = tabsgme.tb_codsgme"
              Case "tabtipa.tb_destipa"
                strQuery += " LEFT JOIN tabtipa ON artico.codditt = tabtipa.codditt AND artico.ar_codtipa = tabtipa.tb_codtipa"
              Case "tabtpro.tb_destpro"
                strQuery += " LEFT JOIN tabtpro ON artico.codditt = tabtpro.codditt AND artico.ar_tipprom = tabtpro.tb_codtpro"
              Case "anagra_2.an_descr1"
                strQuery += " LEFT JOIN anagra As anagra_2 ON listini.codditt = anagra_2.codditt AND listini.lc_conto = anagra_2.an_conto"
              Case "destdiv.dd_nomdest"
                strQuery += " LEFT JOIN destdiv ON listini.codditt = destdiv.codditt AND listini.lc_conto = destdiv.dd_conto AND listini.lc_coddest = destdiv.dd_coddest"
            End Select
          Next
          dttTmp.Clear()
          dttTmp.Dispose()
        Case Else : Return False
      End Select
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il tipo di stampa è su Clienti/Fornitori/Sottoconti,
      '--- se è attiva la gestione delle estensioni,
      '--- se il tipo indicato nell'impostazione filtri è 'Cliente' o 'Fornitore',
      '--- aggiunge in INNER JOIN la tabella ANAEXT con ANAGRA
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(dtrParstaf!pf_codquery)
        Case 1, 2, 3
          If (bGestAnaext = True) And (NTSCStr(dtrFiltri!pf_1tipo).Trim <> "") Then
            strQuery += " INNER JOIN anaext ON anagra.codditt = anaext.codditt AND anagra.an_conto = anaext.ax_conto"
          End If
        Case 14
          strQuery = strQuery.Replace("#", "TTARTPRO")
          strQuery = strQuery.Replace("$", "TTARTPROX")
        Case 15
          strQuery = strQuery.Replace("#", "TTARTPROX")
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function MarcatoreCampoSomma(ByVal strNomeCampo As String, ByVal nSize As Integer) As String
    Dim strTmp As String = "=0"

    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case strNomeCampo.ToLower
        Case "#dbolledafatt" : strTmp += "A"
        Case "#dfattnoncont" : strTmp += "B"
        Case "#dfatttotale" : strTmp += "C"
        Case "#dimpinsol12mesi" : strTmp += "D"
        Case "#dimpinsolinessere" : strTmp += "E"
        Case "#dordinidaevad" : strTmp += "F"
        Case "#drbrischio" : strTmp += "G"
        Case "#drdindistinta" : strTmp += "H"
        Case "#drdscadute" : strTmp += "I"
        Case "#drischioprimo" : strTmp += "J"
        Case "#drischiototale" : strTmp += "K"
        Case "#dsaldocontabile" : strTmp += "L"
        Case "#dsforamento" : strTmp += "M"
        Case "#lninsol12mesi" : strTmp += "N"
        Case "#lninsolinessere" : strTmp += "O"
        Case Else : Return ""
      End Select
      '--------------------------------------------------------------------------------------------------------------
      strTmp += Right("00" & NTSCStr(nSize), 2) & "".PadLeft(nSize - 5, "="c)
      '--------------------------------------------------------------------------------------------------------------
      Return strTmp
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return ""
    End Try
  End Function

  Public Overridable Function RiempiTmpTable(ByVal strDitta As String, ByRef dtrParstaf As DataRow, _
    ByRef dttParstca As DataTable, ByRef dtrFiltri As DataRow, ByRef dtrFiltriX As DataRow, _
    ByRef strIntestazioneCampi As String, ByRef bGestAnaext As Boolean, ByRef strData As String, _
    ByRef lIITTstaf As Integer, ByRef lIITTArtpro As Integer, ByRef lIITTArtprox As Integer, _
    ByRef bAggzzdcst As Boolean, ByRef nDecSuPrzUn As Integer, ByRef strSomma As String, _
    ByRef strTemp As String, ByRef bEsistonoTotaliDcst As Boolean) As Boolean
    Dim bResult As Boolean = False
    Dim nCampo As Integer = 1
    Dim i As Integer = 0
    Dim nIndice As Integer = 0
    Dim strSQL As String = ""
    Dim strInnerJoin As String = ""
    Dim strCampo As String = ""
    Dim FORMAT_QTA As String = "#,##0.000"
    Dim vNum As Object = Nothing
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSomma = "" : bEsistonoTotaliDcst = False
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre un DataTable su tabelle/campi selezionati
      '--------------------------------------------------------------------------------------------------------------
      bResult = SelezionaDati(strDitta, dtrParstaf, dttParstca, dtrFiltri, dtrFiltriX, strIntestazioneCampi, _
        bGestAnaext, strData, lIITTArtpro, lIITTArtprox, strSomma, strSQL, dttTmp)
      If bResult = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      strTemp = ""
      If (NTSCStr(dtrParstaf!pf_statot) = "S") And (strSomma <> "") Then strTemp = strSQL
      '--------------------------------------------------------------------------------------------------------------
      '--- Se variabile di registro bAggzzdcst = True svuota la tabella ZZDCST
      '--------------------------------------------------------------------------------------------------------------
      If bAggzzdcst = True Then Svuotazzdcst(strDitta)
      '--------------------------------------------------------------------------------------------------------------
      '--- Compone la stringa della tabella temporanea per la stampa
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To (dttTmp.Rows.Count - 1)
        strSQL = "INSERT INTO TTSTAF (codditt, instid, tt_riga, tt_desstampa)" & _
          " VALUES (" & CStrSQL(strDitta) & ", " & lIITTstaf & ", " & (i + 1) & ", "
        '------------------------------------------------------------------------------------------------------------
        '--- Se Anagrafiche Clienti/Forn/Sottoconti con dati sintetici/statistici,
        '--- antempone, SEMPRE, il codice conto che, nell'Entity servirà per calcolare i dati sintetici/statistici
        '------------------------------------------------------------------------------------------------------------
        strCampo = ""
        If NTSCInt(dtrParstaf!pf_codquery) = 3 Then
          strCampo += Microsoft.VisualBasic.Right("".PadLeft(9, "0"c) & NTSCStr(dttTmp.Rows(i)!an_conto), 9) & " "
        End If
        '------------------------------------------------------------------------------------------------------------
        For nIndice = 0 To (dttParstca.Rows.Count - 1)
          With dttParstca.Rows(nIndice)
            If Mid(NTSCStr(!pfc_nomcampo), 1, 1) = "#" Then
              bEsistonoTotaliDcst = True
              strCampo += MarcatoreCampoSomma(NTSCStr(!pfc_nomcampo), NTSCInt(!pfc_size)) & " "
            Else
              Select Case NTSCInt(!pfc_tipcampo)
                Case 8, 10, 12 '8: dbText, 10: dbMemo, 12: dbDate
                  If dttTmp.Rows(i)(nIndice).Equals(DBNull.Value) Then
                    strCampo += "".PadLeft(NTSCInt(!pfc_size)) & " "
                  Else
                    strCampo += Left(NTSCStr(dttTmp.Rows(i)(nIndice)) & "".PadLeft(NTSCInt(!pfc_size)), NTSCInt(!pfc_size)) & " "
                  End If
                Case Else
                  If dttTmp.Columns(nIndice).ColumnName.ToUpper = "FASE" Then
                    vNum = dttTmp.Rows(i)(nIndice)
                  Else
                    If IsDate(NTSCStr(dttTmp.Rows(i)(nIndice))) Then
                      vNum = dttTmp.Rows(i)(nIndice)
                    Else
                      If NTSCDec(dttTmp.Rows(i)(nIndice)) <> 0 Then vNum = dttTmp.Rows(i)(nIndice) Else vNum = " "
                    End If
                  End If
                  If !pfc_format.Equals(DBNull.Value) Then
                    strCampo += Right("".PadLeft(NTSCInt(!pfc_size)) & CType(vNum, String), NTSCInt(!pfc_size)) & " "
                  Else
                    If NTSCStr(!pfc_format) = "#,##0" Then
                      If IsNumeric(vNum) Then
                        strCampo += Right("".PadLeft(NTSCInt(!pfc_size)) & Format(CType(vNum, Decimal), FORMAT_LIREUR), NTSCInt(!pfc_size)) & " "
                      Else
                        strCampo += "".PadLeft(NTSCInt(!pfc_size)) & " "
                      End If
                    Else
                      If NTSCStr(!pfc_format) = "#.##0" Then
                        If IsNumeric(vNum) Then
                          If NTSCStr(!pfc_nomcampo).ToLower = "artprox.apx_qtalif" Then
                            strCampo += Right("".PadLeft(NTSCInt(!pfc_size)) & Format(CType(vNum, Decimal), FORMAT_QTA), NTSCInt(!pfc_size)) & " "
                          Else
                            strCampo += Right("".PadLeft(NTSCInt(!pfc_size)) & Format(CType(vNum, Decimal), FORMAT_LIREUR), NTSCInt(!pfc_size)) & " "
                          End If
                        Else
                          strCampo += "".PadLeft(NTSCInt(!pfc_size)) & " "
                        End If
                      Else
                        If NTSCStr(!pfc_nomcampo).ToLower = "listini.lc_prezzo" Then
                          If IsNumeric(vNum) Then
                            Select Case nDecSuPrzUn
                              Case 2 : strCampo += Right("".PadLeft(NTSCInt(!pfc_size)) & Format(CType(vNum, Decimal), "#,##0.00"), NTSCInt(!pfc_size)) & " "
                              Case 3 : strCampo += Right("".PadLeft(NTSCInt(!pfc_size)) & Format(CType(vNum, Decimal), "#,##0.000"), NTSCInt(!pfc_size)) & " "
                              Case 4 : strCampo += Right("".PadLeft(NTSCInt(!pfc_size)) & Format(CType(vNum, Decimal), "#,##0.0000"), NTSCInt(!pfc_size)) & " "
                              Case Else : strCampo += Right("".PadLeft(NTSCInt(!pfc_size)) & Format(CType(vNum, Decimal), NTSCStr(!pfc_format)), NTSCInt(!pfc_size)) & " "
                            End Select
                          Else
                            strCampo += "".PadLeft(NTSCInt(!pfc_size)) & " "
                          End If
                        Else
                          If IsNumeric(vNum) Then
                            strCampo += Right("".PadLeft(NTSCInt(!pfc_size)) & Format(CType(vNum, Decimal), NTSCStr(!pfc_format)), NTSCInt(!pfc_size)) & " "
                          Else
                            strCampo += "".PadLeft(NTSCInt(!pfc_size)) & " "
                          End If
                        End If
                      End If
                    End If
                  End If
              End Select
            End If
          End With
        Next
        '------------------------------------------------------------------------------------------------------------
        '--- Se la lunghezza è maggiore di 255, la tronca
        '--- Se è arrivato a questo punto, vuol dire che è stato fatto un controllo a monte ed è stato
        '--- confermato il troncamento del campo
        '------------------------------------------------------------------------------------------------------------
        If (strCampo.Length - 1) > 255 Then strCampo = Mid(strCampo, 1, 256)
        '------------------------------------------------------------------------------------------------------------
        strSQL += CStrSQL(Mid(strCampo, 1, strCampo.Length - 1)) & ")"
        '------------------------------------------------------------------------------------------------------------
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        '------------------------------------------------------------------------------------------------------------
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function SelezionaDati(ByVal strDitta As String, ByRef dtrParstaf As DataRow, _
    ByRef dttParstca As DataTable, ByRef dtrFiltri As DataRow, ByRef dtrFiltriX As DataRow, _
    ByRef strIntestazioneCampi As String, ByRef bGestAnaext As Boolean, ByRef strData As String, _
    ByRef lIITTArtpro As Integer, ByRef lIITTArtprox As Integer, ByRef strSomma As String, _
    ByRef strSQL As String, ByRef dttTmp As DataTable) As Boolean
    Dim nCampo As Integer = 1
    Dim i As Integer = 0
    Dim strInnerJoin As String = ""
    Dim str1, str2, str3, str4, str5, str6, str7, str8 As String

    Try
      '--------------------------------------------------------------------------------------------------------------
      str1 = "" : str2 = "" : str3 = "" : str4 = "" : str5 = "" : str6 = "" : str7 = "" : str8 = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Determina la JOIN a seconda del tipo di stampa
      '--------------------------------------------------------------------------------------------------------------
      GetQueryPredefinita(dtrParstaf, dtrFiltri, bGestAnaext, strInnerJoin)
      '--------------------------------------------------------------------------------------------------------------
      '--- Compone la stringa SQL
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT "
      strIntestazioneCampi = ""
      strSomma = "SELECT "
      For i = 0 To (dttParstca.Rows.Count - 1)
        With dttParstca.Rows(i)
          Select Case NTSCInt(dtrParstaf!pf_codquery)
            Case 2
              If Not Mid(NTSCStr(!pfc_nomcampo), 1, 3).ToUpper = "SUM" Then
                strSQL += " MIN(" & NTSCStr(!pfc_nomcampo) & "), "
              Else
                strSQL += NTSCStr(!pfc_nomcampo) & ", "
              End If
            Case 3
              If Not Mid(NTSCStr(!pfc_nomcampo), 1, 1) = "#" Then
                strSQL += NTSCStr(!pfc_nomcampo) & ", "
              Else
                strSQL += "'', "
              End If
            Case 14
              If Mid(NTSCStr(!pfc_nomcampo), 1, 1) = "." Then
                strSQL += "TTARTPRO" & NTSCStr(!pfc_nomcampo) & ", "
              Else
                If Left(NTSCStr(!pfc_nomcampo), 3).ToLower = "iif" Then
                  strSQL += SostituisciIIf(NTSCStr(!pfc_nomcampo)) & ", "
                Else
                  strSQL += NTSCStr(!pfc_nomcampo) & ", "
                End If
              End If
            Case 15
              If Mid(NTSCStr(!pfc_nomcampo), 1, 1) = "." Then
                strSQL += "TTARTPROX" & NTSCStr(!pfc_nomcampo) & ", "
              Else
                If Left(NTSCStr(!pfc_nomcampo), 3).ToLower = "iif" Then
                  strSQL += SostituisciIIf(NTSCStr(!pfc_nomcampo)) & ", "
                Else
                  strSQL += NTSCStr(!pfc_nomcampo) & ", "
                End If
              End If
            Case Else
              If Left(NTSCStr(!pfc_nomcampo), 3).ToLower = "iif" Then
                strSQL += SostituisciIIf(NTSCStr(!pfc_nomcampo)) & ", "
              Else
                strSQL += NTSCStr(!pfc_nomcampo) & ", "
              End If
          End Select
          If NTSCStr(dtrParstaf!pf_statot) = "S" Then
            If NTSCInt(!pfc_tipcampo) = 7 Then
              If Mid(NTSCStr(!pfc_nomcampo), 1, 3).ToUpper = "SUM" Then
                str7 = Mid(NTSCStr(!pfc_nomcampo), 1, (InStr(1, NTSCStr(!pfc_nomcampo), " As ") - 1))
                'strSomma += "RIGHT('" & "".PadLeft(NTSCInt(!pfc_size)) & "' + CAST(" & str7 & " AS VARCHAR(20)), " & NTSCStr(!pfc_size) & "), "
                strSomma += "RIGHT('" & "".PadLeft(NTSCInt(!pfc_size)) & "' + CONVERT(VARCHAR, CONVERT(MONEY, " & str7 & "), 1), " & NTSCStr(!pfc_size) & "), "
              Else
                If Mid(NTSCStr(!pfc_nomcampo), 1, 1) = "#" Then
                  strSomma += "RIGHT('" & "".PadLeft(NTSCInt(!pfc_size)) & "' + CAST(" & NTSCStr(!pfc_nomcampo) & "), " & NTSCStr(!pfc_size) & "), "
                Else
                  Select Case NTSCInt(dtrParstaf!pf_codquery)
                    Case 14
                      If Mid(NTSCStr(!pfc_nomcampo), 1, 1) = "." Then
                        str8 = Mid(NTSCStr(!pfc_format), (InStr(1, NTSCStr(!pfc_format), ".") + 1))
                        strSomma += "Sum(" & "TTARTPRO" & NTSCStr(!pfc_nomcampo) & ") As " & IIf(str8.Length = 2, "V", "Q").ToString & NTSCStr(nCampo) & "_" & NTSCStr(!pfc_size) & ", "
                        nCampo += 1
                      Else
                        str8 = Mid(NTSCStr(!pfc_format), (InStr(1, NTSCStr(!pfc_format), ".") + 1))
                        If Left(NTSCStr(!pfc_nomcampo), 3).ToLower = "iif" Then
                          strSomma += "Sum(" & SostituisciIIf(NTSCStr(!pfc_nomcampo)) & ") As " & IIf(str8.Length = 2, "V", "Q").ToString & NTSCStr(nCampo) & "_" & NTSCStr(!pfc_size) & ", "
                        Else
                          strSomma += "Sum(" & NTSCStr(!pfc_nomcampo) & ") As " & IIf(str8.Length = 2, "V", "Q").ToString & NTSCStr(nCampo) & "_" & NTSCStr(!pfc_size) & ", "
                        End If
                        nCampo += 1
                      End If
                    Case 15
                      If Mid(NTSCStr(!pfc_nomcampo), 1, 1) = "." Then
                        str8 = Mid(NTSCStr(!pfc_format), (InStr(1, NTSCStr(!pfc_format), ".") + 1))
                        strSomma += " Sum(" & "TTARTPROX" & NTSCStr(!pfc_nomcampo) & ") As " & IIf(str8.Length = 2, "V", "Q").ToString & NTSCStr(nCampo) & "_" & NTSCStr(!pfc_size) & ", "
                        nCampo += 1
                      Else
                        str8 = Mid(NTSCStr(!pfc_format), (InStr(1, NTSCStr(!pfc_format), ".") + 1))
                        If Left(NTSCStr(!pfc_nomcampo), 3).ToLower = "iif" Then
                          strSomma += " Sum(" & SostituisciIIf(NTSCStr(!pfc_nomcampo)) & ") As " & IIf(str8.Length = 2, "V", "Q").ToString & NTSCStr(nCampo) & "_" & NTSCStr(!pfc_size) & ", "
                        Else
                          strSomma += " Sum(" & NTSCStr(!pfc_nomcampo) & ") As " & IIf(str8.Length = 2, "V", "Q").ToString & NTSCStr(nCampo) & "_" & NTSCStr(!pfc_size) & ", "
                        End If
                        nCampo += 1
                      End If
                    Case Else
                      str8 = Mid(NTSCStr(!pfc_format), (InStr(1, NTSCStr(!pfc_format), ".") + 1))
                      If Left(NTSCStr(!pfc_nomcampo), 3).ToLower = "iif" Then
                        strSomma += "Sum(" & SostituisciIIf(NTSCStr(!pfc_nomcampo)) & ") As " & IIf(str8.Length = 2, "V", "Q").ToString & NTSCStr(nCampo) & "_" & NTSCStr(!pfc_size) & ", "
                      Else
                        strSomma += "Sum(" & NTSCStr(!pfc_nomcampo) & ") As " & IIf(str8.Length = 2, "V", "Q").ToString & NTSCStr(nCampo) & "_" & NTSCStr(!pfc_size) & ", "
                      End If
                      nCampo += 1
                  End Select
                End If
              End If
            Else
              If NTSCInt(!pfc_tipcampo) = 4 Then
                If (Mid(NTSCStr(!pfc_nomcampo), 1, 1) = "#") And ((NTSCStr(!pfc_nomcampo) = "#lNinsol12mesi") Or (NTSCStr(!pfc_nomcampo) = "#lNinsolinessere")) Then
                  strSomma += "RIGHT('" & "".PadLeft(NTSCInt(!pfc_size)) & "' + STR(" & NTSCStr(!pfc_nomcampo) & "), " & NTSCStr(!pfc_size) & "), "
                Else
                  strSomma += "'" & "".PadLeft(NTSCInt(!pfc_size)) & "', "
                End If
              Else
                strSomma += "'" & "".PadLeft(NTSCInt(!pfc_size)) & "', "
              End If
            End If
          End If
          Select Case NTSCInt(!pfc_tipcampo)
            Case 8, 10, 12 '--- 8: dbDate, 10: dbText, 12: dbMemo
              strIntestazioneCampi += Left(NTSCStr(!pfc_desstamp) & "".PadLeft(NTSCInt(!pfc_size)), NTSCInt(!pfc_size)) & " "
            Case Else
              strIntestazioneCampi += Right("".PadLeft(NTSCInt(!pfc_size)) & NTSCStr(!pfc_desstamp), NTSCInt(!pfc_size)) & " "
          End Select
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      strIntestazioneCampi = Mid(strIntestazioneCampi, 1, strIntestazioneCampi.Length - 1)
      strSQL = Left(strSQL, strSQL.Length - 2) & " FROM " & strInnerJoin & " WHERE"
      If strSomma.Trim.Length = 6 Then
        strSomma = ""
      Else
        strSomma = Mid(strSomma, 1, (strSomma.Length - 2))
      End If
      '--------------------------------------------------------------------------------------------------------------
      AggiungiFiltri(strDitta, dtrFiltri, dtrFiltriX, strSQL, strData, lIITTArtpro, lIITTArtprox)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla se esistono record nell'intervallo di selezione
      '--------------------------------------------------------------------------------------------------------------      
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function SostituisciCampi(ByVal strSomma As String, _
    ByRef dTOTSaldoContabile As Decimal, ByRef dTOTRDinDistinta As Decimal, ByRef dTOTRBrischio As Decimal, _
    ByRef dTOTRischioPrimo As Decimal, ByRef dTOTFattnoncont As Decimal, ByRef dTOTBolledaFatt As Decimal, _
    ByRef dTOTOrdinidaEvad As Decimal, ByRef dTOTRischioTotale As Decimal, ByRef dTOTSforamento As Decimal, _
    ByRef dTOTRDScadute As Decimal, ByRef dTOTImpinsol12mesi As Decimal, ByRef dTOTImpinsolinessere As Decimal, _
    ByRef dTOTFattTotale As Decimal, ByRef lTOTNinsol12mesi As Integer, ByRef lTOTNinsolinessere As Integer) As String
    Dim lPos1 As Integer = 0
    Dim lPos2 As Integer = 0
    Dim lPos3 As Integer = 0
    Dim lStart As Integer = 1
    Dim dNuovoCampo As Decimal = 0
    Dim strSQL As String = ""
    Dim strNomeCampo As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      SostituisciCampi = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non ci sono campi da sostituire non fa niente ed esce, restituendo il valore in entrata
      '--------------------------------------------------------------------------------------------------------------
      lPos1 = InStr(1, strSomma, "#")
      If lPos1 = 0 Then Return strSomma
      '--------------------------------------------------------------------------------------------------------------
      '--- Altrimenti sostituisce i campi "#" con i totali corrispondenti
      '--------------------------------------------------------------------------------------------------------------
      lPos1 = InStr(lStart, strSomma, "#")
      lPos2 = InStr(lPos1, strSomma, ",")
      strSQL = Mid(strSomma, lStart, (lPos1 - 1))
      '--------------------------------------------------------------------------------------------------------------
Ritorna:
      '--------------------------------------------------------------------------------------------------------------
      strNomeCampo = Mid(strSomma, lPos1, (lPos2 - lPos1))
      If Right(strNomeCampo, 1) = ")" Then strNomeCampo = Mid(strNomeCampo, 1, (strNomeCampo.Length - 1))
      Select Case strNomeCampo.ToLower
        Case "#dsaldocontabile" : dNuovoCampo = dTOTSaldoContabile
        Case "#drdindistinta" : dNuovoCampo = dTOTRDinDistinta
        Case "#drbrischio" : dNuovoCampo = dTOTRBrischio
        Case "#drischioprimo" : dNuovoCampo = dTOTRischioPrimo
        Case "#dfattnoncont" : dNuovoCampo = dTOTFattnoncont
        Case "#dbolledafatt" : dNuovoCampo = dTOTBolledaFatt
        Case "#dordinidaevad" : dNuovoCampo = dTOTOrdinidaEvad
        Case "#drischiototale" : dNuovoCampo = dTOTRischioTotale
        Case "#dsforamento" : dNuovoCampo = dTOTSforamento
        Case "#drdscadute" : dNuovoCampo = dTOTRDScadute
        Case "#dimpinsol12mesi" : dNuovoCampo = dTOTImpinsol12mesi
        Case "#dimpinsolinessere" : dNuovoCampo = dTOTImpinsolinessere
        Case "#dfatttotale" : dNuovoCampo = dTOTFattTotale
        Case "#lninsol12mesi" : dNuovoCampo = lTOTNinsol12mesi
        Case "#lninsolinessere" : dNuovoCampo = lTOTNinsolinessere
      End Select
      strSQL += CStrSQL(Format(dNuovoCampo, FORMAT_LIREUR)) & " AS VARCHAR(16)" & ")"
      '--------------------------------------------------------------------------------------------------------------
      lStart = (lPos2 + 14)
      '--------------------------------------------------------------------------------------------------------------
      If InStr(lStart, strSomma, "#") <> 0 Then
        lPos3 = InStr(lStart, strSomma, "#")
        strSQL = strSQL & Mid(strSomma, lPos2, (lPos3 - lPos2))
        lPos1 = InStr(lStart, strSomma, "#")
        lPos2 = InStr(lPos1, strSomma, ",")
        GoTo Ritorna
      Else
        strSQL += Mid(strSomma, lPos2)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return strSQL
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return ""
    End Try
  End Function

  Public Overridable Function SostituisciIIf(ByVal strIn As String) As String
    Dim lIIf As Integer = 0
    Dim lVirgola As Integer = 0
    Dim lParentesi As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      SostituisciIIf = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Sostituisce la "IIf(" con "CASE WHEN"
      '--------------------------------------------------------------------------------------------------------------
      lIIf = InStr(1, strIn.ToLower, "iif(")
      SostituisciIIf = "CASE WHEN " & Mid(strIn, (lIIf + 4))
      '--------------------------------------------------------------------------------------------------------------
      '--- Sostituisce la prima virgola con "THEN"
      '--------------------------------------------------------------------------------------------------------------
      lVirgola = InStr(1, SostituisciIIf, ",")
      SostituisciIIf = Mid(SostituisciIIf, 1, (lVirgola - 1)).Trim & " THEN " & _
        Mid(SostituisciIIf, (lVirgola + 1)).Trim
      '--------------------------------------------------------------------------------------------------------------
      '--- Sostituisce la seconda virgola con "ELSE"
      '--------------------------------------------------------------------------------------------------------------
      lVirgola = InStr(1, SostituisciIIf, ",")
      SostituisciIIf = Mid(SostituisciIIf, 1, (lVirgola - 1)).Trim & " ELSE " & _
        Mid(SostituisciIIf, (lVirgola + 1)).Trim
      '--------------------------------------------------------------------------------------------------------------
      '--- Sostituisce la parentesi finale con "END"
      '--------------------------------------------------------------------------------------------------------------
      lParentesi = InStr(1, StrReverse(SostituisciIIf), ")")
      SostituisciIIf = Mid(StrReverse(SostituisciIIf), 1, (lParentesi - 1)).Trim & " DNE " & _
        Mid(StrReverse(SostituisciIIf), (lParentesi + 1)).Trim
      '--------------------------------------------------------------------------------------------------------------
      SostituisciIIf = StrReverse(SostituisciIIf)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function Svuotazzdcst(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM zzdcst" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
    End Try
  End Function

  Public Overridable Function InsTTARTPROdaARTDEF(ByVal strDitta As String, ByVal lIITTArtpro As Integer, _
    ByRef strWhereTTARTPRO As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTARTPRO (codditt, instid, ap_codart, ap_magaz, ap_esist, ap_prenot, ap_ordin," & _
        " ap_impeg, ap_carfor, ap_carpro, ap_carvar, ap_rescli, ap_scacli, ap_scapro, ap_scavar, ap_resfor," & _
        " ap_giaini, ap_vprenot, ap_vordin, ap_vimpeg, ap_vcarfor, ap_vcarpro, ap_vcarvar, ap_vrescli, ap_vscacli," & _
        " ap_vscapro, ap_vscavar, ap_vresfor, ap_vgiaini, ap_sommat, ap_daordi, ap_vdaordi, ap_fase)" & _
        " SELECT " & CStrSQL(strDitta) & ", " & lIITTArtpro & ", ad_codart, ad_magaz, ad_esist, 0, 0, 0, ad_carfor," & _
        " ad_carpro, ad_carvar, ad_rescli, ad_scacli, ad_scapro, ad_scavar, ad_resfor, ad_giaini, 0, 0, 0," & _
        " ad_vcarfor, ad_vcarpro, ad_vcarvar, ad_vrescli, ad_vscacli, ad_vscapro, ad_vscavar, ad_vresfor," & _
        " ad_vgiaini, ad_sommat, 0, 0, ad_fase" & _
        " FROM artdef INNER JOIN artico ON artdef.codditt = artico.codditt AND artdef.ad_codart = artico.ar_codart" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        strWhereTTARTPRO
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function InsTTARTPROdaKEYMAG(ByVal strDitta As String, ByVal lIITTArtpro As Integer, _
    ByRef strWhereTTARTPRO As String, ByRef strData As String, ByRef strDtulap As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTARTPRO (codditt, instid, ap_codart, ap_magaz, ap_esist, ap_prenot, ap_ordin," & _
        " ap_impeg, ap_carfor, ap_carpro, ap_carvar, ap_rescli, ap_scacli, ap_scapro, ap_scavar, ap_resfor," & _
        " ap_giaini, ap_vprenot, ap_vordin, ap_vimpeg, ap_vcarfor, ap_vcarpro, ap_vcarvar, ap_vrescli, ap_vscacli," & _
        " ap_vscapro, ap_vscavar, ap_vresfor, ap_vgiaini, ap_sommat, ap_daordi, ap_vdaordi, ap_fase)" & _
        " SELECT DISTINCT " & CStrSQL(strDitta) & ", " & lIITTArtpro & ", km_codart, km_magaz," & _
        " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, km_fase" & _
        " FROM artico INNER JOIN keymag ON artico.codditt = keymag.codditt AND artico.ar_codart = keymag.km_codart" & _
        " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
        " AND km_aammgg > " & CDataSQL(strDtulap) & _
        " AND km_aammgg <= " & CDataSQL(strData) & _
        " AND NOT EXISTS (SELECT TOP 1 0 " & _
        "               FROM TTARTPRO" & _
        "              WHERE codditt = " & CStrSQL(strDitta) & _
        "                AND instid = " & lIITTArtpro & _
        "                AND ap_codart = km_codart " & _
        "                AND ap_magaz = km_magaz " & _
        "                AND ap_fase = km_fase)" & _
        strWhereTTARTPRO

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function InsTTARTPROdaKEYPRB(ByVal strDitta As String, ByVal lIITTArtpro As Integer, _
    ByRef strWhereTTARTPRO As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTARTPRO (codditt, instid, ap_codart, ap_magaz, ap_esist, ap_prenot, ap_ordin," & _
        " ap_impeg, ap_carfor, ap_carpro, ap_carvar, ap_rescli, ap_scacli, ap_scapro, ap_scavar, ap_resfor," & _
        " ap_giaini, ap_vprenot, ap_vordin, ap_vimpeg, ap_vcarfor, ap_vcarpro, ap_vcarvar, ap_vrescli, ap_vscacli," & _
        " ap_vscapro, ap_vscavar, ap_vresfor, ap_vgiaini, ap_sommat, ap_daordi, ap_vdaordi, ap_fase)" & _
        " SELECT DISTINCT " & CStrSQL(strDitta) & ", " & lIITTArtpro & ", km_codart, km_magaz," & _
        " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, km_fase" & _
        " FROM artico INNER JOIN keyprb ON artico.codditt = keyprb.codditt AND artico.ar_codart = keyprb.km_codart" & _
        " WHERE keyprb.codditt = " & CStrSQL(strDitta) & _
        " AND NOT EXISTS (SELECT TOP 1 0 " & _
        "                  FROM TTARTPRO" & _
        "                 WHERE codditt = " & CStrSQL(strDitta) & _
        "                   AND instid = " & lIITTArtpro & _
        "                   AND ap_codart = km_codart " & _
        "                   AND ap_magaz = km_magaz " & _
        "                   AND ap_fase = km_fase)" & _
        strWhereTTARTPRO
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function InsTTARTPROdaKEYORD(ByVal strDitta As String, ByVal lIITTArtpro As Integer, _
    ByRef strWhereTTARTPRO As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTARTPRO (codditt, instid, ap_codart, ap_magaz, ap_esist, ap_prenot, ap_ordin," & _
        " ap_impeg, ap_carfor, ap_carpro, ap_carvar, ap_rescli, ap_scacli, ap_scapro, ap_scavar, ap_resfor," & _
        " ap_giaini, ap_vprenot, ap_vordin, ap_vimpeg, ap_vcarfor, ap_vcarpro, ap_vcarvar, ap_vrescli, ap_vscacli," & _
        " ap_vscapro, ap_vscavar, ap_vresfor, ap_vgiaini, ap_sommat, ap_daordi, ap_vdaordi, ap_fase)" & _
        " SELECT DISTINCT " & CStrSQL(strDitta) & ", " & lIITTArtpro & ", ko_codart, ko_magaz," & _
        " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ko_fase" & _
        " FROM artico INNER JOIN keyord ON artico.codditt = keyord.codditt AND artico.ar_codart = keyord.ko_codart" & _
        " AND keyord.codditt = " & CStrSQL(strDitta) & _
        " AND NOT EXISTS (SELECT TOP 1 0 " & _
        "                  FROM TTARTPRO" & _
        "                   WHERE codditt = " & CStrSQL(strDitta) & _
        "                     AND instid = " & lIITTArtpro & _
        "                     AND ap_codart = ko_codart " & _
        "                     AND ap_magaz = ko_magaz " & _
        "                     AND ap_fase = ko_fase)" & _
        strWhereTTARTPRO
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function UpdTTARTPROdaKEYMAG(ByVal strDitta As String, ByVal lIITTArtpro As Integer, ByRef strWhereTTARTPRO As String, _
                                                  ByRef strData As String, ByRef strDtulap As String, ByRef dEserc As Date) As Boolean
    Dim strSQL As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT km_codart, km_magaz, km_fase, SUM(mm_quant * tb_esist) AS esist, SUM(mm_quant * tb_carfor) AS carfor," & _
               "       SUM(mm_quant * tb_carpro) AS carpro, SUM(mm_quant * tb_carvar) AS carvar, SUM(mm_quant * tb_rescli) AS rescli, " & _
               "       SUM(mm_quant * tb_scacli) AS scacli, SUM(mm_quant * tb_scapro) AS scapro, SUM(mm_quant * tb_scavar) AS scavar, " & _
               "       SUM(mm_quant * tb_resfor) AS resfor, SUM(mm_quant * tb_giaini) AS giaini, SUM(mm_valore * tb_vcarfor) AS vcarfor, " & _
               "       SUM(mm_valore * tb_vcarpro) AS vcarpro, SUM(mm_valore * tb_vcarvar) AS vcarvar, SUM(mm_valore * tb_vrescli) AS vrescli, " & _
               "       SUM(mm_valore * tb_vscacli) AS vscacli, SUM(mm_valore * tb_vscapro) AS vscapro, SUM(mm_valore * tb_vscavar) AS vscavar, " & _
               "       SUM(mm_valore * tb_vresfor) AS vresfor, SUM(mm_valore * tb_vgiaini) AS vgiaini, " & _
               "      SUM(mm_quant * DateDiff(d, " & CDataSQL(dEserc) & ", km_aammgg) * -tb_esist) AS sommat " & _
               " FROM movmag " & _
               "  INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga" & _
               "  INNER JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum" & _
               "  INNER JOIN artico ON keymag.codditt = artico.codditt AND keymag.km_codart = artico.ar_codart" & _
               "  INNER JOIN TTARTPRO ON TTARTPRO.codditt = keymag.codditt AND TTARTPRO.ap_codart = keymag.km_codart AND TTARTPRO.ap_magaz = keymag.km_magaz AND TTARTPRO.ap_fase = keymag.km_fase" & _
               " WHERE artico.codditt = " & CStrSQL(strDitta) & _
               "   AND km_aammgg > " & CDataSQL(strDtulap) & _
               "   AND km_aammgg <= " & CDataSQL(strData) & _
               "   AND TTARTPRO.instid = " & lIITTArtpro & _
               strWhereTTARTPRO & _
               " GROUP BY km_codart, km_magaz, km_fase "
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrRow As DataRow In OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows
        With dtrRow
          strSQL = "UPDATE TTARTPRO" & _
            " SET ap_esist = ap_esist + " & CDblSQL(NTSCDec(!esist)) & "," & _
            " ap_carfor = ap_carfor + " & CDblSQL(NTSCDec(!carfor)) & "," & _
            " ap_carpro = ap_carpro + " & CDblSQL(NTSCDec(!carpro)) & "," & _
            " ap_carvar = ap_carvar + " & CDblSQL(NTSCDec(!carvar)) & "," & _
            " ap_rescli = ap_rescli + " & CDblSQL(NTSCDec(!rescli)) & "," & _
            " ap_scacli = ap_scacli + " & CDblSQL(NTSCDec(!scacli)) & "," & _
            " ap_scapro = ap_scapro + " & CDblSQL(NTSCDec(!scapro)) & "," & _
            " ap_scavar = ap_scavar + " & CDblSQL(NTSCDec(!scavar)) & "," & _
            " ap_resfor = ap_resfor + " & CDblSQL(NTSCDec(!resfor)) & "," & _
            " ap_giaini = ap_giaini + " & CDblSQL(NTSCDec(!giaini)) & "," & _
            " ap_vcarfor = ap_vcarfor + " & CDblSQL(NTSCDec(!vcarfor)) & "," & _
            " ap_vcarpro = ap_vcarpro + " & CDblSQL(NTSCDec(!vcarpro)) & "," & _
            " ap_vcarvar = ap_vcarvar + " & CDblSQL(NTSCDec(!vcarvar)) & "," & _
            " ap_vrescli = ap_vrescli + " & CDblSQL(NTSCDec(!vrescli)) & "," & _
            " ap_vscacli = ap_vscacli + " & CDblSQL(NTSCDec(!vscacli)) & "," & _
            " ap_vscapro = ap_vscapro + " & CDblSQL(NTSCDec(!vscapro)) & "," & _
            " ap_vscavar = ap_vscavar + " & CDblSQL(NTSCDec(!vscavar)) & "," & _
            " ap_vresfor = ap_vresfor + " & CDblSQL(NTSCDec(!vresfor)) & "," & _
            " ap_vgiaini = ap_vgiaini + " & CDblSQL(NTSCDec(!vgiaini)) & "," & _
            " ap_sommat = ap_sommat + " & CDblSQL(NTSCDec(!sommat)) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            "   AND instid = " & lIITTArtpro & _
            "   AND ap_magaz = " & NTSCInt(!km_magaz) & _
            "   AND ap_codart = " & CStrSQL(!km_codart) & _
            "   AND ap_fase = " & NTSCInt(!km_fase)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function UpdTTARTPROdaKEYPRB(ByVal strDitta As String, ByVal lIITTArtpro As Integer, ByRef strWhereTTARTPRO As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT km_codart, km_magaz, km_fase, SUM(mm_quant) AS mm_quant, SUM(mm_valore) AS mm_valore" & _
        " FROM keyprb INNER JOIN movprb ON movprb.codditt = keyprb.codditt AND movprb.mm_tipork = keyprb.km_tipork AND movprb.mm_anno = keyprb.km_anno AND movprb.mm_serie = keyprb.km_serie AND movprb.mm_numdoc = keyprb.km_numdoc AND movprb.mm_riga = keyprb.km_riga" & _
        " INNER JOIN artico ON artico.codditt = movprb.codditt AND artico.ar_codart = movprb.mm_codart" & _
        " INNER JOIN TTARTPRO ON TTARTPRO.codditt = keyprb.codditt AND TTARTPRO.ap_codart = keyprb.km_codart AND TTARTPRO.ap_magaz = keyprb.km_magaz AND TTARTPRO.ap_fase = keyprb.km_fase" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        "   AND mm_nprflevas = 'C'" & _
        "   AND TTARTPRO.instid = " & lIITTArtpro & _
        strWhereTTARTPRO & _
        "GROUP BY km_codart, km_magaz, km_fase "
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrRow As DataRow In OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows
        With dtrRow
          strSQL = "UPDATE TTARTPRO" & _
            " SET ap_prenot = ap_prenot + " & CDblSQL(NTSCDec(!mm_quant)) & "," & _
            " ap_vprenot = ap_vprenot + " & CDblSQL(NTSCDec(!mm_valore)) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTArtpro & _
            " AND ap_magaz = " & NTSCInt(!km_magaz) & _
            " AND ap_codart = " & CStrSQL(!km_codart) & _
            " AND ap_fase = " & NTSCInt(!km_fase)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function UpdTTARTPROdaKEYORD(ByVal strDitta As String, ByVal lIITTArtpro As Integer, ByRef strWhereTTARTPRO As String) As Boolean
    Dim strSQL As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT ko_codart, ko_magaz, ko_fase, SUM((mo_quant - mo_quaeva) * ko_ordin) AS ordin, " & _
        "      SUM((mo_quant - mo_quaeva) * ko_impeg) AS impeg, SUM(mo_valore * ko_ordin) AS vordin, SUM(mo_valore * ko_impeg) AS vimpeg " & _
        " FROM keyord INNER JOIN movord ON movord.codditt = keyord.codditt AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_anno = keyord.ko_anno AND movord.mo_serie = keyord.ko_serie AND movord.mo_numord = keyord.ko_numord AND movord.mo_riga = keyord.ko_riga" & _
        " INNER JOIN artico ON artico.codditt = movord.codditt AND artico.ar_codart = movord.mo_codart" & _
        " INNER JOIN TTARTPRO ON TTARTPRO.codditt = keyord.codditt AND TTARTPRO.ap_codart = keyord.ko_codart AND TTARTPRO.ap_magaz = keyord.ko_magaz AND TTARTPRO.ap_fase = keyord.ko_fase" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND mo_flevas = 'C'" & _
        " AND TTARTPRO.instid = " & lIITTArtpro & _
        strWhereTTARTPRO & _
        " GROUP BY ko_codart, ko_magaz, ko_fase "
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrRow As DataRow In OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows
        With dtrRow
          strSQL = "UPDATE TTARTPRO" & _
            " SET ap_ordin = ap_ordin + " & CDblSQL(NTSCDec(!ordin)) & "," & _
            " ap_impeg = ap_impeg + " & CDblSQL(NTSCDec(!impeg)) & "," & _
            " ap_vordin = ap_vordin + " & CDblSQL(NTSCDec(!vordin)) & "," & _
            " ap_vimpeg = ap_vimpeg + " & CDblSQL(NTSCDec(!vimpeg)) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTArtpro & _
            " AND ap_magaz = " & NTSCInt(!ko_magaz) & _
            " AND ap_codart = " & CStrSQL(!ko_codart) & _
            " AND ap_fase = " & NTSCInt(!ko_fase)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function InsTTARTPROXdaARTDEFX(ByVal strDitta As String, ByVal lIITTArtprox As Integer, _
    ByRef strWhereTTARTPRO As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTARTPROX (codditt, instid, apx_codart, apx_esist, apx_prenot, apx_ordin, apx_impeg," & _
        " apx_giaini, apx_vprenot, apx_vordin, apx_vimpeg, apx_vgiaini, apx_qtalif, apx_vqtalif, apx_ultcos," & _
        " apx_peucos, apx_ultpre, apx_dtulcar, apx_dtulsca, apx_daordi, apx_vdaordi, apx_fase)" & _
        " SELECT " & CStrSQL(strDitta) & ", " & lIITTArtprox & ", adx_codart, adx_esist, 0, 0, 0, adx_giaini," & _
        " 0, 0, 0, adx_vgiaini, adx_qtalif, adx_vqtalif, adx_ultcos, adx_peucos, adx_ultpre, adx_dtulcar," & _
        " adx_dtulsca, 0, 0, adx_fase" & _
        " FROM artdefx INNER JOIN artico ON artdefx.codditt = artico.codditt AND artdefx.adx_codart = artico.ar_codart" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        strWhereTTARTPRO
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
    End Try
  End Function
  Public Overridable Function InsTTARTPROXdaKEYMAG(ByVal strDitta As String, ByVal lIITTArtprox As Integer, _
    ByRef strWhereTTARTPRO As String, ByRef strData As String, ByRef strDtulap As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTARTPROX (codditt, instid, apx_codart, apx_esist, apx_prenot, apx_ordin, apx_impeg," & _
        " apx_giaini, apx_vprenot, apx_vordin, apx_vimpeg, apx_vgiaini, apx_qtalif, apx_vqtalif, apx_ultcos," & _
        " apx_peucos, apx_ultpre, apx_dtulcar, apx_dtulsca, apx_daordi, apx_vdaordi, apx_fase)" & _
        " SELECT DISTINCT " & CStrSQL(strDitta) & ", " & lIITTArtprox & ", km_codart," & _
        " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, km_fase" & _
        " FROM artico INNER JOIN keymag ON artico.codditt = keymag.codditt AND artico.ar_codart = keymag.km_codart" & _
        " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
        " AND km_aammgg > " & CDataSQL(strDtulap) & _
        " AND km_aammgg <= " & CDataSQL(strData) & _
        " AND NOT EXISTS (SELECT TOP 1 0" & _
        "                 FROM TTARTPROX" & _
        "                 WHERE codditt = " & CStrSQL(strDitta) & _
        "                   AND instid = " & lIITTArtprox & _
        "                   AND apx_codart = km_codart " & _
        "                   AND apx_fase = km_fase)" & _
       strWhereTTARTPRO

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function InsTTARTPROXdaKEYPRB(ByVal strDitta As String, ByVal lIITTArtprox As Integer, _
    ByRef strWhereTTARTPRO As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTARTPROX (codditt, instid, apx_codart, apx_esist, apx_prenot, apx_ordin, apx_impeg," & _
        " apx_giaini, apx_vprenot, apx_vordin, apx_vimpeg, apx_vgiaini, apx_qtalif, apx_vqtalif, apx_ultcos," & _
        " apx_peucos, apx_ultpre, apx_dtulcar, apx_dtulsca, apx_daordi, apx_vdaordi, apx_fase)" & _
        " SELECT DISTINCT " & CStrSQL(strDitta) & ", " & lIITTArtprox & ", km_codart," & _
        " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, km_fase" & _
        " FROM artico INNER JOIN keyprb ON artico.codditt = keyprb.codditt AND artico.ar_codart = keyprb.km_codart" & _
        " WHERE keyprb.codditt = " & CStrSQL(strDitta) & _
        " AND NOT EXISTS (SELECT TOP 1 0" & _
        "                 FROM TTARTPROX" & _
        "                 WHERE codditt = " & CStrSQL(strDitta) & _
        "                   AND instid = " & lIITTArtprox & _
        "                   AND apx_codart = km_codart " & _
        "                   AND apx_fase = km_fase)" & _
        strWhereTTARTPRO
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
    End Try
  End Function
  Public Overridable Function InsTTARTPROXdaKEYORD(ByVal strDitta As String, ByVal lIITTArtprox As Integer, _
    ByRef strWhereTTARTPRO As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTARTPROX (codditt, instid, apx_codart, apx_esist, apx_prenot, apx_ordin, apx_impeg," & _
        " apx_giaini, apx_vprenot, apx_vordin, apx_vimpeg, apx_vgiaini, apx_qtalif, apx_vqtalif, apx_ultcos," & _
        " apx_peucos, apx_ultpre, apx_dtulcar, apx_dtulsca, apx_daordi, apx_vdaordi, apx_fase)" & _
        " SELECT DISTINCT " & CStrSQL(strDitta) & ", " & lIITTArtprox & ", ko_codart," & _
        " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ko_fase" & _
        " FROM artico INNER JOIN keyord ON artico.codditt = keyord.codditt AND artico.ar_codart = keyord.ko_codart" & _
        " WHERE keyord.codditt = " & CStrSQL(strDitta) & _
        " AND NOT EXISTS (SELECT TOP 1 0" & _
        "                 FROM TTARTPROX" & _
        "                 WHERE codditt = " & CStrSQL(strDitta) & _
        "                   AND instid = " & lIITTArtprox & _
        "                   AND apx_codart = ko_codart " & _
        "                   AND apx_fase = ko_fase)" & _
        strWhereTTARTPRO
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
    End Try
  End Function
  Public Overridable Function UpdTTARTPROXdaKEYMAG(ByVal strDitta As String, ByVal lIITTArtprox As Integer, _
    ByRef strWhereTTARTPRO As String, ByRef strData As String, ByRef strDtulap As String) As Boolean
    Dim strSQL As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT km_aammgg, km_codart, km_fase, SUM(mm_quant * tb_esist) AS esist, SUM(mm_quant * tb_giaini) AS giaini, " & _
               "       SUM(mm_valore * tb_vgiaini) AS vgiaini, SUM(mm_quant * tb_valoriz) AS qtalif,  " & _
               "       SUM((mm_valore + mm_numpac + mm_numpex) * tb_vvaloriz) AS vqtalif,  " & _
               "       SUM(CASE WHEN tb_ultcos = 'S' AND mm_quant <> 0 THEN (mm_valore + mm_numpac + mm_numpex) / mm_quant ELSE 0 END) AS peucos, " & _
               "       SUM(CASE WHEN tb_ultcos = 'S' AND mm_quant <> 0 THEN mm_valore / mm_quant * mm_perqta ELSE 0 END) AS ultcos,   " & _
               "       SUM(CASE WHEN tb_ultpre = 'S' AND mm_quant <> 0 THEN mm_valore / mm_quant * mm_perqta ELSE 0 END) AS ultpre,  " & _
               "       MAX(CASE WHEN tb_dtulcar = 'S' AND mm_valore <> 0 THEN km_aammgg ELSE NULL END) AS dtulcar, " & _
               "       MAX(CASE WHEN tb_dtulsca = 'S' AND mm_valore <> 0 THEN km_aammgg ELSE NULL END) AS dtulsca" & _
               " FROM movmag " & _
               "   INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga" & _
               "   INNER JOIN artico ON artico.codditt = movmag.codditt AND artico.ar_codart = movmag.mm_codart" & _
               "   INNER JOIN tabcaum ON tabcaum.tb_codcaum = keymag.km_causale" & _
               "   INNER JOIN TTARTPROX ON TTARTPROX.codditt = keymag.codditt AND TTARTPROX.apx_codart = keymag.km_codart AND TTARTPROX.apx_fase = keymag.km_fase" & _
               " WHERE artico.codditt = " & CStrSQL(strDitta) & _
               "   AND km_aammgg > " & CDataSQL(strDtulap) & _
               "   AND km_aammgg <= " & CDataSQL(strData) & _
               "   AND TTARTPROX.instid = " & lIITTArtprox & _
               strWhereTTARTPRO & _
               " GROUP BY km_aammgg, km_codart, km_fase "
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrRow As DataRow In OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows
        With dtrRow
          '----------------------------------------------------------------------------------------------------------
          strSQL = "UPDATE TTARTPROX" & _
                   " SET apx_esist = apx_esist + " & CDblSQL(NTSCDec(!esist)) & "," & _
                   " apx_giaini = apx_giaini + " & CDblSQL(NTSCDec(!giaini)) & "," & _
                   " apx_vgiaini = apx_vgiaini + " & CDblSQL(NTSCDec(!vgiaini)) & "," & _
                   " apx_qtalif = apx_qtalif + " & CDblSQL(NTSCDec(!qtalif)) & "," & _
                   " apx_vqtalif = apx_vqtalif + " & CDblSQL(NTSCDec(!vqtalif)) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND instid = " & lIITTArtprox & _
                   " AND apx_codart = " & CStrSQL(!km_codart) & _
                   " AND apx_fase = " & NTSCInt(!km_fase)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          '----------------------------------------------------------------------------------------------------------
          strSQL = "UPDATE TTARTPROX" & _
                   " SET apx_peucos = " & CDblSQL(NTSCDec(!peucos)) & "," & _
                   "     apx_ultcos = " & CDblSQL(NTSCDec(!ultcos)) & "," & _
                   "     apx_dtulcar = " & NTSCStr(IIf(NTSCStr(!km_aammgg) = "", "apx_dtulcar", CDataSQL(NTSCDate(!km_aammgg)))) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND instid = " & lIITTArtprox & _
                   " AND apx_codart = " & CStrSQL(!km_codart) & _
                   " AND apx_fase = " & NTSCInt(!km_fase) & _
                   " AND apx_dtulcar <= " & CDataSQL(NTSCStr(!km_aammgg))
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          '----------------------------------------------------------------------------------------------------------
          strSQL = "UPDATE TTARTPROX" & _
                     " SET apx_ultpre = " & CDblSQL(NTSCDec(!ultpre)) & "," & _
                     "     apx_dtulsca = " & NTSCStr(IIf(NTSCStr(!km_aammgg) = "", "apx_dtulsca", CDataSQL(NTSCDate(!km_aammgg)))) & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND instid = " & lIITTArtprox & _
                     " AND apx_codart = " & CStrSQL(!km_codart) & _
                     " AND apx_fase = " & NTSCInt(!km_fase) & _
                     " AND apx_dtulsca <= " & CDataSQL(NTSCStr(!km_aammgg))
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          '----------------------------------------------------------------------------------------------------------
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function UpdTTARTPROXdaKEYPRB(ByVal strDitta As String, ByVal lIITTArtprox As Integer, _
                                                   ByRef strWhereTTARTPRO As String) As Boolean
    Dim strSQL As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT km_codart, km_fase, SUM(mm_quant) AS mm_quant, SUM(mm_valore) AS mm_valore " & _
               " FROM movprb " & _
               " INNER JOIN keyprb ON movprb.codditt = keyprb.codditt AND movprb.mm_tipork = keyprb.km_tipork AND movprb.mm_anno = keyprb.km_anno AND movprb.mm_serie = keyprb.km_serie AND movprb.mm_numdoc = keyprb.km_numdoc AND movprb.mm_riga = keyprb.km_riga" & _
               " INNER JOIN artico ON artico.codditt = movprb.codditt AND artico.ar_codart = movprb.mm_codart" & _
               " INNER JOIN TTARTPROX ON TTARTPROX.codditt = keyprb.codditt AND TTARTPROX.apx_codart = keyprb.km_codart AND TTARTPROX.apx_fase = keyprb.km_fase" & _
               " WHERE artico.codditt = " & CStrSQL(strDitta) & _
               "   AND mm_nprflevas = 'C'" & _
               "   AND TTARTPROX.instid = " & lIITTArtprox & _
               strWhereTTARTPRO & _
               " GROUP BY km_codart, km_fase "
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrRow As DataRow In OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows
        With dtrRow
          strSQL = "UPDATE TTARTPROX" & _
            " SET apx_prenot = apx_prenot + " & CDblSQL(NTSCDec(!mm_quant)) & "," & _
            " apx_vprenot = apx_vprenot + " & CDblSQL(NTSCDec(!mm_valore)) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTArtprox & _
            " AND apx_codart = " & CStrSQL(!km_codart) & _
            " AND apx_fase = " & NTSCInt(!km_fase)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function UpdTTARTPROXdaKEYORD(ByVal strDitta As String, ByVal lIITTArtprox As Integer, _
                                                   ByRef strWhereTTARTPRO As String) As Boolean
    Dim strSQL As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT ko_codart, ko_fase, SUM((mo_quant - mo_quaeva) * ko_ordin) AS ordin, " & _
               "       SUM((mo_quant - mo_quaeva) * ko_impeg) AS impeg, SUM(mo_valore * ko_ordin) AS vordin, " & _
               "       SUM(mo_valore * ko_impeg) AS vimpeg " & _
               " FROM movord " & _
               "  INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_anno = keyord.ko_anno AND movord.mo_serie = keyord.ko_serie AND movord.mo_numord = keyord.ko_numord AND movord.mo_riga = keyord.ko_riga" & _
               "  INNER JOIN artico ON artico.codditt = movord.codditt AND artico.ar_codart = movord.mo_codart" & _
               "  INNER JOIN TTARTPROX ON TTARTPROX.codditt = keyord.codditt AND TTARTPROX.apx_codart = keyord.ko_codart AND TTARTPROX.apx_fase = keyord.ko_fase" & _
               " WHERE artico.codditt = " & CStrSQL(strDitta) & _
               "   AND mo_flevas = 'C'" & _
               "   AND TTARTPROX.instid = " & lIITTArtprox & _
               strWhereTTARTPRO & _
               " GROUP BY ko_codart, ko_fase "
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrRow As DataRow In OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows
        With dtrRow
          strSQL = "UPDATE TTARTPROX" & _
            " SET apx_ordin = apx_ordin + " & CDblSQL(NTSCDec(!ordin)) & "," & _
            " apx_impeg = apx_impeg + " & CDblSQL(NTSCDec(!impeg)) & "," & _
            " apx_vordin = apx_vordin + " & CDblSQL(NTSCDec(!vordin)) & "," & _
            " apx_vimpeg = apx_vimpeg + " & CDblSQL(NTSCDec(!vimpeg)) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTArtprox & _
            " AND apx_codart = " & CStrSQL(!ko_codart) & _
            " AND apx_fase = " & NTSCInt(!ko_fase)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function LunghezzaCampiSuperioriA255(ByRef lCodform As Integer) As Boolean
    Dim nSize As Integer = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT pf_codquery FROM PARSTAF" & _
        " WHERE pf_codform = " & lCodform & _
        " AND pf_codquery = 3"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      If dttTmp.Rows.Count > 0 Then nSize = 10
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT SUM(pfc_size) + COUNT(pfc_size) AS Size FROM PARSTCA" & _
        " WHERE pfc_codform = " & lCodform & _
        " GROUP BY pfc_codform"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      If dttTmp.Rows.Count > 0 Then
        If Not dttTmp.Rows(0)!Size.Equals(DBNull.Value) Then nSize += NTSCInt(dttTmp.Rows(0)!Size)
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      If (nSize - 1) > 255 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

End Class