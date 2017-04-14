Imports NTSInformatica.CLN__STD
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports NTSInformatica
Imports System.IO
Imports System.Data
Imports System.Data.Common

Public Class CLDMGELAR
  Inherits CLD__BASE

  Public Overridable Function ConversioneOptionInCombo(ByVal bopGenerico As Boolean, ByVal bopClienti As Boolean, ByVal bopFornitori As Boolean, _
                                                       ByVal bopGenericoDaClienti As Boolean, ByVal bopGenericoDaFornitori As Boolean) As Integer
    Try
      If bopGenerico Then
        Return 1
      ElseIf bopClienti Then
        Return 2
      ElseIf bopFornitori Then
        Return 3
      ElseIf bopGenericoDaClienti Then
        Return 4
      ElseIf bopGenericoDaFornitori Then
        Return 5
      End If
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return 1
  End Function

  Public Overridable Function ComponiQueryMovmag(ByVal strDitta As String, ByVal strcbTipork As String, ByVal stredAnno As String, _
                                                 ByVal stredSerieini As String, ByVal stredSeriefin As String, _
                                                 ByVal stredNumdocini As String, ByVal stredNumdocfin As String, _
                                                 ByVal strTestata As String, ByVal strRighe As String, _
                                                 ByRef strSQL As String) As Boolean
    Try
      strSQL = " ar_codart IN (SELECT DISTINCT mm_codart" & _
               " FROM " & strTestata & " INNER JOIN " & strRighe & " ON " & strTestata & ".codditt = " & strRighe & ".codditt" & _
               " AND " & strTestata & ".tm_tipork = " & strRighe & ".mm_tipork" & _
               " AND " & strTestata & ".tm_anno = " & strRighe & ".mm_anno" & _
               " AND " & strTestata & ".tm_serie = " & strRighe & ".mm_serie" & _
               " AND " & strTestata & ".tm_numdoc = " & strRighe & ".mm_numdoc" & _
               " WHERE " & strTestata & ".codditt = " & CStrSQL(strDitta) & _
               " AND tm_tipork = " & CStrSQL(strcbTipork) & _
               " AND tm_anno = " & NTSCStr(stredAnno) & _
               " AND tm_serie BETWEEN " & CStrSQL(stredSerieini) & " AND " & CStrSQL(stredSeriefin) & _
               " AND tm_numdoc BETWEEN " & NTSCStr(stredNumdocini) & " AND " & NTSCStr(stredNumdocfin) & ")"
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckMovimenti(ByVal strDitta As String, ByVal strcbTipork As String, ByVal stredAnno As String, _
                                             ByVal stredSerieini As String, ByVal stredSeriefin As String, _
                                             ByVal stredNumdocini As String, ByVal stredNumdocfin As String, _
                                             ByVal strTestata As String, ByVal strRighe As String, _
                                             ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 tm_tipork FROM " & strTestata & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tm_tipork = " & CStrSQL(strcbTipork) & _
        " AND tm_anno = " & stredAnno & _
        " AND tm_serie BETWEEN " & CStrSQL(stredSerieini) & " AND " & CStrSQL(stredSeriefin) & _
        " AND tm_numdoc BETWEEN " & stredNumdocini & " AND " & stredNumdocfin

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTMAG")

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetElabora(ByVal strDitta As String, ByVal bopGenerico As Boolean, _
                                           ByVal bSeleziona As Boolean, ByVal strAdocQuery As String, _
                                           ByVal strWhereFiar As String, ByVal stredConto As String, _
                                           ByVal bopClienti As Boolean, ByVal bopGenericoDaClienti As Boolean, _
                                           ByVal bopFornitori As Boolean, ByVal bopGenericoDaFornitori As Boolean, _
                                           ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                           ByVal stredDatagg As String, ByVal stredFase As String, _
                                           ByVal stredListpar As String, ByVal bopUltimaFase As Boolean, _
                                           ByVal bopSoloFase As Boolean, ByVal stredListelab As String, _
                                           ByVal bopQualsiasiFase As Boolean, ByRef ds As DataSet) As Boolean
    Try
      Return GetElabora(strDitta, bopGenerico, bSeleziona, strAdocQuery, strWhereFiar, stredConto, bopClienti, bopGenericoDaClienti, _
                        bopFornitori, bopGenericoDaFornitori, stredCodlavo, stredValpar, stredDatagg, stredFase, stredListpar, _
                        bopUltimaFase, bopSoloFase, stredListelab, bopQualsiasiFase, "N", 0, ds)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetElabora(ByVal strDitta As String, ByVal bopGenerico As Boolean, _
                                         ByVal bSeleziona As Boolean, ByVal strAdocQuery As String, _
                                         ByVal strWhereFiar As String, ByVal stredConto As String, _
                                         ByVal bopClienti As Boolean, ByVal bopGenericoDaClienti As Boolean, _
                                         ByVal bopFornitori As Boolean, ByVal bopGenericoDaFornitori As Boolean, _
                                         ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                         ByVal stredDatagg As String, ByVal stredFase As String, _
                                         ByVal stredListpar As String, ByVal bopUltimaFase As Boolean, _
                                         ByVal bopSoloFase As Boolean, ByVal stredListelab As String, _
                                         ByVal bopQualsiasiFase As Boolean, ByVal strTipoPromo As String, _
                                         ByVal lPromo As Integer, ByRef ds As DataSet) As Boolean
    Dim lTipoElaborazione As Integer = 0
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bopGenerico, bSeleziona, strAdocQuery, strWhereFiar, stredConto, bopClienti, bopGenericoDaClienti, _
                        bopFornitori, bopGenericoDaFornitori, stredCodlavo, stredValpar, stredDatagg, stredFase, stredListpar, _
                        bopUltimaFase, bopSoloFase, stredListelab, bopQualsiasiFase, strTipoPromo, lPromo, ds})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(21), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------
      lTipoElaborazione = ConversioneOptionInCombo(bopGenerico, bopClienti, bopFornitori, bopGenericoDaClienti, bopGenericoDaFornitori)

      Return GetElaboraEx(strDitta, lTipoElaborazione, bSeleziona, strAdocQuery, strWhereFiar, stredConto, stredCodlavo, stredValpar, stredDatagg, _
                          stredFase, stredListpar, bopUltimaFase, bopSoloFase, stredListelab, bopQualsiasiFase, strTipoPromo, lPromo, ds, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetElaboraEx(ByVal strDitta As String, ByVal lTipoElaborazione As Integer, _
                                         ByVal bSeleziona As Boolean, ByVal strAdocQuery As String, _
                                         ByVal strWhereFiar As String, ByVal stredConto As String, _
                                         ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                         ByVal stredDatagg As String, ByVal stredFase As String, _
                                         ByVal stredListpar As String, ByVal bopUltimaFase As Boolean, _
                                         ByVal bopSoloFase As Boolean, ByVal stredListelab As String, _
                                         ByVal bopQualsiasiFase As Boolean, ByVal strTipoPromo As String, _
                                         ByVal lPromo As Integer, ByRef ds As DataSet) As Boolean
    Try
      'obsoleta
      Return GetElaboraEx(strDitta, lTipoElaborazione, bSeleziona, strAdocQuery, strWhereFiar, stredConto, _
                          stredCodlavo, stredValpar, stredDatagg, stredFase, stredListpar, bopUltimaFase, _
                          bopSoloFase, stredListelab, bopQualsiasiFase, strTipoPromo, lPromo, ds, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetElaboraEx(ByVal strDitta As String, ByVal lTipoElaborazione As Integer, _
                                           ByVal bSeleziona As Boolean, ByVal strAdocQuery As String, _
                                           ByVal strWhereFiar As String, ByVal stredConto As String, _
                                           ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                           ByVal stredDatagg As String, ByVal stredFase As String, _
                                           ByVal stredListpar As String, ByVal bopUltimaFase As Boolean, _
                                           ByVal bopSoloFase As Boolean, ByVal stredListelab As String, _
                                           ByVal bopQualsiasiFase As Boolean, ByVal strTipoPromo As String, _
                                           ByVal lPromo As Integer, ByRef ds As DataSet, _
                                           ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lTipoElaborazione, bSeleziona, strAdocQuery, strWhereFiar, stredConto, _
                          stredCodlavo, stredValpar, stredDatagg, stredFase, stredListpar, bopUltimaFase, _
                          bopSoloFase, stredListelab, bopQualsiasiFase, strTipoPromo, lPromo, ds, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(17), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      If lTipoElaborazione = 2 OrElse lTipoElaborazione = 3 OrElse lTipoElaborazione = 4 OrElse _
         lTipoElaborazione = 5 OrElse lTipoElaborazione = 6 OrElse lTipoElaborazione = 7 Then ' per listini cliente o fornitore
        strSQL = "SELECT ar_codart, ar_codpdon, ar_gesvar, ar_prevar, ar_codroot, ar_codvar1, ar_conver, ar_qtacon2, ar_confez2, ar_unmis2, listini.lc_codart, listini.lc_codlavo," & _
          " listini.lc_conto, listini.lc_codvalu, listini.lc_codtpro, listini.lc_listino," & _
          " listini.lc_datagg, listini.lc_tipo, listini.lc_prezzo, listini.lc_datscad," & _
          " listini.lc_daquant, listini.lc_aquant, listini.lc_perqta, listini.lc_unmis," & _
          " listini.lc_note, listini.lc_netto, listini.lc_fase, listini.lc_coddest" & _
          " FROM artico INNER JOIN listini ON (artico.codditt = listini.codditt) " & _
          " AND (((artico.ar_codart = listini.lc_codart) AND (ar_gesvar = 'N' OR ar_gesvar = 'J' OR ar_gesvar = 'H' " & _
          " OR ar_gesvar = 'K' OR ar_prevar = 'S')) " & _
          " OR ((artico.ar_codroot = listini.lc_codart) AND (ar_prevar = 'N')) " & _
          " OR ((artico.ar_codroot + artico.ar_codvar1 = listini.lc_codart) AND (ar_prevar = '1'))) " & _
          " WHERE artico.codditt = " & CStrSQL(strDitta)

        If bSeleziona = True Then
          If strAdocQuery <> "" Then strSQL = strSQL & " AND " & strAdocQuery
          TraduciWhere(strWhereFiar, strSQL)
        Else
          strSQL += strWhereFiar
        End If

        If NTSCInt(stredConto) <> 0 Then
          Select Case lTipoElaborazione
            Case 2, 3, 4, 5 : strSQL += " AND listini.lc_conto = " & stredConto
            Case Else : strSQL += " AND listini.lc_conto = 0"
          End Select
        Else
          strSQL = strSQL & " AND listini.lc_conto <> 0"
        End If

        If lCoddest <> -1 And lTipoElaborazione <> 1 And lTipoElaborazione <> 6 And lTipoElaborazione <> 7 Then strSQL += " AND listini.lc_coddest = " & lCoddest

        Select Case lTipoElaborazione
          Case 2, 4 : strSQL += " AND listini.lc_tipo = 'C'"
          Case 3, 5 : strSQL += " AND listini.lc_tipo = 'F'"
        End Select
        Select Case lTipoElaborazione
          Case 2, 3, 4, 5 : strSQL += " AND listini.lc_listino = 0"
          Case 6, 7
            If NTSCInt(stredListpar) <> 0 Then strSQL += " AND listini.lc_listino = " & stredListpar
        End Select
        strSQL = strSQL & " AND listini.lc_codlavo = " & stredCodlavo & _
          " AND listini.lc_codvalu = " & stredValpar & _
          " AND listini.lc_datagg <= " & CDataSQL(stredDatagg) & _
          " AND listini.lc_datscad >= " & CDataSQL(stredDatagg) & _
          " AND listini.lc_unmis <> ' '"

        Select Case strTipoPromo
          Case "S" : strSQL &= " AND listini.lc_codtpro = " & lPromo
          Case "N" : strSQL &= " AND listini.lc_codtpro = 0"
          Case "T" : strSQL &= " AND listini.lc_codtpro <> 0"
        End Select
        If bopUltimaFase = True Then
          '--- Se 'Solo ultima fase' prende i lisini con la fase = all'ultima fase dell'articolo
          strSQL = strSQL & " AND listini.lc_fase = artico.ar_ultfase"
        Else
          If bopSoloFase = True Then
            '--- Altrimenti se 'Solo fase' prende i listini con la fase indicata in maschera
            strSQL = strSQL & " AND listini.lc_fase = " & stredFase
          End If
        End If
        strSQL = strSQL & " ORDER BY listini.lc_codart, listini.lc_codlavo," & _
          " listini.lc_conto, listini.lc_coddest, listini.lc_listino, listini.lc_codvalu, listini.lc_codtpro," & _
          " listini.lc_datagg DESC, listini.lc_daquant, listini.lc_unmis, listini.lc_fase"

        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      Else
        ' esegue una UNION nel caso di listino partenza zero ....
        If (NTSCInt(stredListpar) = 0) Or (NTSCInt(stredListpar) = -1) Or (NTSCInt(stredListpar) = -2) Then 'listini generico prende tutti articoli + quelli del listino di destinazione già esistenti
          'Nuova query
          strSQL = "SELECT ar_codart, ar_codpdon, ar_gesvar, ar_prevar, ar_codroot, ar_codvar1, ar_conver, ar_qtacon2,  ar_confez2, ar_unmis2, ar_codart AS lc_codart, 0 AS lc_codlavo, 0 AS lc_conto, 0 AS lc_codvalu, 0 AS lc_codtpro, " & _
            stredListelab & " AS lc_listino, " & CDataSQL(IntSetDate("01/10/1900")) & " AS lc_datagg," & _
            " ' ' AS lc_tipo, 0.0000 AS lc_prezzo, " & CDataSQL(IntSetDate("31/12/2099")) & " AS lc_datscad, 0 AS lc_daquant," & _
            " 9999999999 AS lc_aquant, ar_perqta AS lc_perqta, ar_unmis AS lc_unmis, NULL AS lc_note, 'N' AS lc_netto,"
          If bopQualsiasiFase = True Then
            strSQL = strSQL & " CASE WHEN af_fase IS NULL THEN ar_ultfase ELSE af_fase END As lc_fase, 0 as lc_coddest"
            strSQL = strSQL & " FROM artico LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart"
          Else
            strSQL = strSQL & " " & NTSCStr(IIf(bopUltimaFase = True, "ar_ultfase", stredFase)) & " AS lc_fase, 0 as lc_coddest" & _
              " FROM artico"
          End If
          strSQL = strSQL & " WHERE artico.codditt = " & CStrSQL(strDitta)

          If bSeleziona = True Then
            If strAdocQuery <> "" Then strSQL = strSQL & " AND " & strAdocQuery
            TraduciWhere(strWhereFiar, strSQL)
          End If

          strSQL = strSQL & " ORDER BY ar_codart"

          ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

          For i = 0 To ds.Tables("LISTINI").Rows.Count - 1 'risetto il formato corretto
            ds.Tables("LISTINI").Rows(i)!lc_datagg = "01/10/1900"
            ds.Tables("LISTINI").Rows(i)!lc_datscad = "31/12/2099"
          Next
          ds.Tables("LISTINI").AcceptChanges()

        Else 'listini generico definito 1-999 : prende nel caso di listini generici solo quelli che hanno già un prezzo (troppo diffciele anche quelli che non ne hanno !!!)
          strSQL = "SELECT artico.ar_codart as ar_codart, artico.ar_codpdon as ar_codpdon, artico.ar_gesvar as ar_gesvar, artico.ar_prevar as ar_prevar, " & _
            " ar_codroot, ar_codvar1, ar_conver, ar_qtacon2, ar_confez2, ar_unmis2, lc_codart, " & _
            " lc_codlavo," & _
            " lc_conto, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_tipo, lc_prezzo, lc_datscad," & _
            " lc_daquant, lc_aquant, lc_perqta, lc_unmis, lc_note, lc_netto, lc_fase, lc_coddest" & _
            " FROM artico INNER JOIN busvw_listini ON (artico.codditt = busvw_listini.codditt) " & _
            " AND artico.ar_codart = busvw_listini.ar_codart " & _
            " WHERE artico.codditt = " & CStrSQL(strDitta)

          If bSeleziona = True Then
            strAdocQuery = strAdocQuery.Replace("ar_", "artico.ar_")
            If strAdocQuery <> "" Then strSQL = strSQL & " AND " & strAdocQuery
            strWhereFiar = strWhereFiar.Replace("ar_", "artico.ar_")
            'Può capitare che la riga sopra aggiunta una seconda volta la tabella (ad esempio con il filtro generale per tipo articolo)
            strWhereFiar = strWhereFiar.Replace("artico.artico", "artico")
            TraduciWhere(strWhereFiar, strSQL)
          Else
            strSQL += strWhereFiar
          End If

          strSQL = strSQL & " AND lc_conto = 0" & _
            " AND lc_codlavo = " & stredCodlavo & _
            " AND lc_codvalu = " & stredValpar & _
            " AND lc_listino = " & stredListpar & _
            " AND lc_datagg <= " & CDataSQL(stredDatagg) & _
            " AND lc_datscad >= " & CDataSQL(stredDatagg) & _
            " AND lc_unmis <> ' '"

          Select Case strTipoPromo
            Case "S" : strSQL &= " AND lc_codtpro = " & lPromo
            Case "N" : strSQL &= " AND lc_codtpro = 0"
            Case "T" : strSQL &= " AND lc_codtpro <> 0"
          End Select

          If bopUltimaFase = True Then
            '--- Se 'Solo ultima fase' prende i lisini con la fase = all'ultima fase dell'articolo
            strSQL = strSQL & " AND lc_fase = artico.ar_ultfase"
          Else
            If bopSoloFase = True Then
              '--- Altrimenti se 'Solo fase' prende i listini con la fase indicata in maschera
              strSQL = strSQL & " AND lc_fase = " & stredFase
            End If
          End If
          strSQL = strSQL & " ORDER BY lc_codart, lc_codlavo," & _
            " lc_conto, lc_coddest, lc_listino, lc_codvalu, lc_codtpro," & _
            " lc_datagg DESC, lc_daquant, lc_unmis, lc_fase"

          ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")
        End If
      End If

      If lCoddest > -1 And (lTipoElaborazione = 6 Or lTipoElaborazione = 7) Then
        'altrimenti al salva salva il listino specifico sulla destinaz. diversa sbagliata
        For Each dtrT As DataRow In ds.Tables("LISTINI").Rows
          dtrT!lc_coddest = lCoddest
        Next
      End If

      ds.AcceptChanges()

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ListinoGiaPresente(ByVal strDitta As String, _
                                             ByVal strCodart As String, ByVal strConto As String, _
                                             ByVal strCodvalu As String, ByVal strCodtpro As String, _
                                             ByVal strListino As String, ByVal strDatagg As String, _
                                             ByVal dDaQuant As Decimal, ByVal strCodlavo As String, _
                                             ByVal strUnmis As String, ByVal strFase As String, _
                                             ByRef ds As DataSet) As Boolean
    Try
      'obsoleta
      Return ListinoGiaPresente(strDitta, strCodart, strConto, strCodvalu, strCodtpro, strListino, strDatagg, dDaQuant, strCodlavo, strUnmis, strFase, ds, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ListinoGiaPresente(ByVal strDitta As String, _
                                               ByVal strCodart As String, ByVal strConto As String, _
                                               ByVal strCodvalu As String, ByVal strCodtpro As String, _
                                               ByVal strListino As String, ByVal strDatagg As String, _
                                               ByVal dDaQuant As Decimal, ByVal strCodlavo As String, _
                                               ByVal strUnmis As String, ByVal strFase As String, _
                                               ByRef ds As DataSet, ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, strConto, strCodvalu, strCodtpro, strListino, strDatagg, dDaQuant, strCodlavo, strUnmis, strFase, ds, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(10), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT lc_listino FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codlavo = " & strCodlavo & _
        " AND lc_conto = " & strConto & _
        " AND lc_coddest = " & lCoddest & _
        " AND lc_codvalu = " & strCodvalu & _
        " AND lc_codtpro = " & strCodtpro & _
        " AND lc_listino = " & strListino & _
        " AND lc_datagg = " & CDataSQL(strDatagg) & _
        " AND lc_daquant = " & CDblSQL(dDaQuant) & _
        " AND lc_unmis = " & CStrSQL(strUnmis) & _
        " AND lc_fase = " & strFase & _
        " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_listino, lc_codvalu," & _
        " lc_codtpro, lc_datagg DESC, lc_daquant, lc_unmis, lc_fase"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

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
      strSQL = "SELECT tb_aliq FROM artico INNER JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva " & _
               "WHERE artico.codditt = " & CStrSQL(strDitta) & " AND artico.ar_codart = " & CStrSQL(strCodart)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetPrezzoAttuale(ByVal strDitta As String, ByVal strCodart As String, _
                                             ByVal bopGenerico As Boolean, ByVal stredListelab As String, _
                                             ByVal stredConto As String, ByVal stredDatagg As String, _
                                             ByVal stredValelab As String, ByVal stredCodlavo As String, _
                                             ByVal dDaQuant As Decimal, ByVal strLc_unmis As String, _
                                             ByVal strLc_fase As String, ByRef ds As DataSet, _
                                             ByVal bopClienti As Boolean, ByVal bopGenericoDaClienti As Boolean, _
                                             ByVal bopFornitori As Boolean, ByVal bopGenericoDaFornitori As Boolean) As Boolean
    Try
      Return GetPrezzoAttuale(strDitta, strCodart, bopGenerico, stredListelab, stredConto, stredDatagg, stredValelab, stredCodlavo, _
                              dDaQuant, strLc_unmis, strLc_fase, ds, bopClienti, bopGenericoDaClienti, bopFornitori, bopGenericoDaFornitori, _
                              "N", 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetPrezzoAttuale(ByVal strDitta As String, ByVal strCodart As String, _
                                               ByVal bopGenerico As Boolean, ByVal stredListelab As String, _
                                               ByVal stredConto As String, ByVal stredDatagg As String, _
                                               ByVal stredValelab As String, ByVal stredCodlavo As String, _
                                               ByVal dDaQuant As Decimal, ByVal strLc_unmis As String, _
                                               ByVal strLc_fase As String, ByRef ds As DataSet, _
                                               ByVal bopClienti As Boolean, ByVal bopGenericoDaClienti As Boolean, _
                                               ByVal bopFornitori As Boolean, ByVal bopGenericoDaFornitori As Boolean, _
                                               ByVal strTipoPromo As String, ByVal lPromo As Integer) As Boolean
    Dim strSQL As String = ""
    Dim lTipoElaborazione As Integer = 0
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, bopGenerico, stredListelab, stredConto, _
                                             stredDatagg, stredValelab, stredCodlavo, dDaQuant, strLc_unmis, _
                                             strLc_fase, ds, bopClienti, bopGenericoDaClienti, bopFornitori, _
                                             bopGenericoDaFornitori, strTipoPromo, lPromo})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(12), DataSet)
        Return CBool(oOut)
      End If
      '----------------
      lTipoElaborazione = ConversioneOptionInCombo(bopGenerico, bopClienti, bopFornitori, bopGenericoDaClienti, bopGenericoDaFornitori)

      Return GetPrezzoAttualeEx(strDitta, strCodart, lTipoElaborazione, stredListelab, stredConto, stredDatagg, _
                               stredValelab, stredCodlavo, dDaQuant, strLc_unmis, strLc_fase, ds, strTipoPromo, lPromo, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetPrezzoAttualeEx(ByVal strDitta As String, ByVal strCodart As String, _
                                               ByVal lTipoElaborazione As Integer, ByVal stredListelab As String, _
                                               ByVal stredConto As String, ByVal stredDatagg As String, _
                                               ByVal stredValelab As String, ByVal stredCodlavo As String, _
                                               ByVal dDaQuant As Decimal, ByVal strLc_unmis As String, _
                                               ByVal strLc_fase As String, ByRef ds As DataSet, _
                                               ByVal strTipoPromo As String, ByVal lPromo As Integer) As Boolean
    Try
      'obsoleta
      Return GetPrezzoAttualeEx(strDitta, strCodart, lTipoElaborazione, stredListelab, stredConto, stredDatagg, stredValelab, stredCodlavo, _
                                dDaQuant, strLc_unmis, strLc_fase, ds, strTipoPromo, lPromo, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetPrezzoAttualeEx(ByVal strDitta As String, ByVal strCodart As String, _
                                                 ByVal lTipoElaborazione As Integer, ByVal stredListelab As String, _
                                                 ByVal stredConto As String, ByVal stredDatagg As String, _
                                                 ByVal stredValelab As String, ByVal stredCodlavo As String, _
                                                 ByVal dDaQuant As Decimal, ByVal strLc_unmis As String, _
                                                 ByVal strLc_fase As String, ByRef ds As DataSet, _
                                                 ByVal strTipoPromo As String, ByVal lPromo As Integer, _
                                                 ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, lTipoElaborazione, stredListelab, stredConto, stredDatagg, stredValelab, stredCodlavo, _
                                dDaQuant, strLc_unmis, strLc_fase, ds, strTipoPromo, lPromo, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(11), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------


      strSQL = "SELECT TOP 1 lc_prezzo, lc_datagg, lc_datscad FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart)
      If lTipoElaborazione = 1 Or lTipoElaborazione = 4 Or lTipoElaborazione = 5 Then
        strSQL = strSQL & " AND lc_listino = " & stredListelab & _
          " AND lc_conto = 0"
      Else
        strSQL = strSQL & " AND lc_listino = 0"
        If NTSCInt(stredConto) <> 0 Then
          strSQL = strSQL & " AND lc_conto = " & NTSCInt(stredConto)
          strSQL += " AND lc_coddest = " & lCoddest
        Else
          strSQL = strSQL & " AND lc_conto <> 0"
        End If
        Select Case lTipoElaborazione
          Case 2, 4, 6 : strSQL += " AND lc_tipo = 'C'"
          Case 3, 5, 7 : strSQL += " AND lc_tipo = 'F'"
        End Select
      End If
      strSQL = strSQL & " AND lc_datagg <= " & CDataSQL(stredDatagg) & _
        " AND lc_codvalu = " & stredValelab & _
        " AND lc_codlavo = " & stredCodlavo & _
        " AND lc_datscad >= " & CDataSQL(stredDatagg) & _
        " AND lc_daquant = " & CDblSQL(dDaQuant) & _
        " AND lc_unmis = " & CStrSQL(strLc_unmis) & _
        " AND lc_fase = " & strLc_fase

      Select Case strTipoPromo
        Case "S", "T" : strSQL &= " AND listini.lc_codtpro = " & lPromo
        Case "N" : strSQL &= " AND listini.lc_codtpro = 0"
          'Case "T" : strSQL &= " AND listini.lc_codtpro <> 0"
      End Select

      strSQL &= " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_listino, lc_codvalu," & _
                " lc_codtpro, lc_datagg DESC, lc_daquant, lc_unmis, lc_fase"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetPrezzoBase(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                               ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      With dtrTmp
        If NTSCStr(!ar_gesvar) = "S" And (NTSCStr(!ar_prevar) = "N" Or NTSCStr(!ar_prevar) = "1") Then
          'articoli a varianti N o 1
          strSQL = "SELECT TOP 1 apx_ultcos, apx_peucos, apx_qtalif, apx_vqtalif, apx_giaini, apx_vgiaini, apx_codart, apx_dtulcar FROM artprox INNER JOIN artico ON  (artico.codditt = artprox.codditt) AND (artico.ar_codart = artprox.apx_codart) " & _
            " WHERE artprox.codditt = " & CStrSQL(strDitta) & _
            " AND apx_fase  = " & NTSCStr(!lc_fase) & _
            " AND ar_codroot = " & NTSCStr(IIf(NTSCStr(!ar_codroot) <> "", CStrSQL(!ar_codroot), CStrSQL(!ar_codart))) 'faccio la IIF() xchè l'articolo root ha ar_codroot a null
          If NTSCStr(!ar_prevar) = "1" Then
            strSQL = strSQL & " AND ar_codvar1 = " & CStrSQL(!ar_codvar1)
          End If
          strSQL = strSQL & " ORDER BY apx_dtulcar DESC, apx_codart"
        Else
          'caso normale (o a varianti con S)
          strSQL = "SELECT apx_ultcos, apx_peucos, apx_qtalif, apx_vqtalif, apx_giaini, apx_vgiaini, apx_codart, apx_dtulcar FROM artprox" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND apx_codart = " & CStrSQL(!ar_codart) & _
            " AND apx_fase  = " & NTSCStr(!lc_fase)
        End If
      End With

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetPrezzoBase2(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                            ByVal strCodart As String, ByVal bopGenerico As Boolean, _
                                            ByVal stredListpar As String, ByVal stredConto As String, _
                                            ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                            ByVal stredDatagg As String, ByVal dDaQuant As Decimal, _
                                            ByRef ds As DataSet) As Boolean
    Try
      Return GetPrezzoBase2(strDitta, dtrTmp, strCodart, bopGenerico, stredListpar, stredConto, stredCodlavo, stredValpar, stredDatagg, _
                            dDaQuant, "N", 0, ds, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetPrezzoBase2(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                            ByVal strCodart As String, ByVal bopGenerico As Boolean, _
                                            ByVal stredListpar As String, ByVal stredConto As String, _
                                            ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                            ByVal stredDatagg As String, ByVal dDaQuant As Decimal, _
                                            ByVal strTipoPromo As String, ByVal lPromo As Integer, _
                                            ByRef ds As DataSet) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, strCodart, bopGenerico, stredListpar, stredConto, _
                                             stredCodlavo, stredValpar, stredDatagg, dDaQuant, strTipoPromo, _
                                             lPromo, ds})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(12), DataSet)
        Return CBool(oOut)
      End If
      '----------------
      Return GetPrezzoBase2(strDitta, dtrTmp, strCodart, bopGenerico, stredListpar, stredConto, stredCodlavo, stredValpar, stredDatagg, _
                            dDaQuant, strTipoPromo, lPromo, ds, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetPrezzoBase2(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                             ByVal strCodart As String, ByVal bopGenerico As Boolean, _
                                             ByVal stredListpar As String, ByVal stredConto As String, _
                                             ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                             ByVal stredDatagg As String, ByVal dDaQuant As Decimal, _
                                             ByVal strTipoPromo As String, ByVal lPromo As Integer, _
                                             ByRef ds As DataSet, ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, strCodart, bopGenerico, stredListpar, stredConto, _
                                             stredCodlavo, stredValpar, stredDatagg, dDaQuant, strTipoPromo, _
                                             lPromo, ds, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(12), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      With dtrTmp
        strSQL = "SELECT TOP 1 lc_prezzo, lc_datagg, lc_datscad FROM listini" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND lc_codart = " & CStrSQL(strCodart)
        If bopGenerico = True Then
          strSQL = strSQL & " AND lc_listino = " & stredListpar & _
            " AND lc_conto = 0"
        Else
          '--- Se il conto è indicato come filtro lo applica, altrimenti
          '--- lo rileva da dal recordset corrette aperto sui listini di partenza
          If NTSCInt(stredConto) = 0 Then
            strSQL = strSQL & " AND lc_listino = 0" & _
              " AND lc_conto = " & NTSCStr(!lc_conto)
          Else
            strSQL = strSQL & " AND lc_listino = 0" & _
              " AND lc_conto = " & stredConto
            strSQL += " AND lc_coddest = " & lCoddest
          End If
        End If
        strSQL = strSQL & " AND lc_codlavo = " & stredCodlavo & _
          " AND lc_codvalu = " & stredValpar & _
          " AND lc_datagg <= " & CDataSQL(stredDatagg) & _
          " AND lc_datscad >= " & CDataSQL(stredDatagg) & _
          " AND lc_daquant = " & CDblSQL(dDaQuant) & _
          " AND lc_unmis = " & CStrSQL(!lc_unmis) & _
          " AND lc_fase = " & NTSCStr(!lc_fase)

        Select Case strTipoPromo
          Case "S", "T" : strSQL &= " AND listini.lc_codtpro = " & lPromo
          Case "N" : strSQL &= " AND listini.lc_codtpro = 0"
            'Case "T" : strSQL &= " AND listini.lc_codtpro <> 0"
        End Select

        strSQL &= " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_listino, lc_codvalu," & _
                  " lc_codtpro, lc_datagg DESC, lc_daquant, lc_unmis, lc_fase"
      End With

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTabvalu(ByVal nCodVal As Integer, _
                                          ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM tabvalu WHERE tb_codvalu = " & nCodVal

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABVALU")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArticoArrotonda(ByVal strDitta As String, ByVal strCodart As String, _
                                                 ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ar_codpdon, ar_ricar1, ar_ricar2, ar_flricmar, tb_aliq" & _
        " FROM artico LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND artico.ar_codart = " & CStrSQL(strCodart)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTabpdon(ByVal strDitta As String, ByVal strCodpdon As String, _
                                         ByVal stredListelab As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      If (NTSCInt(stredListelab) >= 1) And (NTSCInt(stredListelab) <= 8) Then
        strSQL = "SELECT tb_arro_" & stredListelab & " FROM tabpdon" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tb_codpdon = " & strCodpdon
      Else
        strSQL = "SELECT tb_arro_1 FROM tabpdon" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tb_codpdon = " & strCodpdon
      End If

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABPDON")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetEsistenza(ByVal strDitta As String, ByVal stredCodart As String, _
                                         ByRef ds As DataSet, ByRef ds1 As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 ap_magaz, ap_esist FROM artpro" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ap_codart = " & CStrSQL(stredCodart) & _
        " ORDER BY codditt, ap_codart, ap_magaz"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO")

      strSQL = "SELECT apx_dtulcar FROM artprox" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND apx_codart = " & CStrSQL(stredCodart)

      ds1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPROX")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

End Class
