Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGSCHE
  Inherits CLD__BASE

  Public Overridable Function GetRelease(ByRef ds As DataSet) As Boolean
    Dim strSQL As String
    Try
      strSQL = "SELECT release.* FROM release" & _
        " WHERE rel_sw = 'Business'"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "RELEASE")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function ComponiStringaSQL(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                                ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                                ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                ByVal strDatainizio As String, ByVal strDatafine As String, _
                                                ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                                ByVal nDaCausale As Integer, ByVal nACausale As Integer, _
                                                ByVal nDaGruppo As Integer, ByVal nAGruppo As Integer, _
                                                ByVal nDaSottogr As Integer, ByVal nASottogr As Integer, _
                                                ByVal strScarOrdin As String, ByVal nScarCodmarcini As Integer, _
                                                ByVal nScarCodmarcfin As Integer, ByVal strQuery As String) As Boolean
    Try
      'obsoleta
      Return ComponiStringaSQL(strDitta, dtrTmp, ds, lDaConto, lAConto, lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                               strDatainizio, strDatafine, strScarTipodoc, strScarSerie, nDaCausale, nACausale, _
                               nDaGruppo, nAGruppo, nDaSottogr, nASottogr, strScarOrdin, nScarCodmarcini, _
                               nScarCodmarcfin, strQuery, "", "".PadLeft(50, "Z"c), "", "", "", "", "")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function ComponiStringaSQL(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                ByRef ds As DataSet, _
                                                ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                                ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                ByVal strDatainizio As String, ByVal strDatafine As String, _
                                                ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                                ByVal nDaCausale As Integer, ByVal nACausale As Integer, _
                                                ByVal nDaGruppo As Integer, ByVal nAGruppo As Integer, _
                                                ByVal nDaSottogr As Integer, ByVal nASottogr As Integer, _
                                                ByVal strScarOrdin As String, ByVal nScarCodmarcini As Integer, _
                                                ByVal nScarCodmarcfin As Integer, ByVal strQuery As String, _
                                                ByVal strLottoxDa As String, ByVal strLottoxA As String) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lDaConto, lAConto, lScarDalotto, lScarAlotto, _
                                             lScarDacomme, lScarAcomme, strDatainizio, strDatafine, strScarTipodoc, _
                                             strScarSerie, nDaCausale, nACausale, nDaGruppo, nAGruppo, nDaSottogr, _
                                             nASottogr, strScarOrdin, nScarCodmarcini, nScarCodmarcfin, strQuery, _
                                             strLottoxDa, strLottoxA})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return ComponiStringaSQL(strDitta, dtrTmp, ds, lDaConto, lAConto, lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                               strDatainizio, strDatafine, strScarTipodoc, strScarSerie, nDaCausale, nACausale, _
                               nDaGruppo, nAGruppo, nDaSottogr, nASottogr, strScarOrdin, nScarCodmarcini, _
                               nScarCodmarcfin, strQuery, strLottoxDa, strLottoxA, "", "", "", "", "")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function ComponiStringaSQL(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                ByRef ds As DataSet, _
                                                ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                                ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                ByVal strDatainizio As String, ByVal strDatafine As String, _
                                                ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                                ByVal nDaCausale As Integer, ByVal nACausale As Integer, _
                                                ByVal nDaGruppo As Integer, ByVal nAGruppo As Integer, _
                                                ByVal nDaSottogr As Integer, ByVal nASottogr As Integer, _
                                                ByVal strScarOrdin As String, ByVal nScarCodmarcini As Integer, _
                                                ByVal nScarCodmarcfin As Integer, ByVal strQuery As String, _
                                                ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                                ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return ComponiStringaSQL(strDitta, dtrTmp, ds, lDaConto, lAConto, lScarDalotto, lScarAlotto, _
                               lScarDacomme, lScarAcomme, strDatainizio, strDatafine, _
                               strScarTipodoc, strScarSerie, nDaCausale, nACausale, _
                               nDaGruppo, nAGruppo, nDaSottogr, nASottogr, strScarOrdin, _
                               nScarCodmarcini, nScarCodmarcfin, strQuery, strLottoxDa, strLottoxA, _
                               strClassificazioneLivello1, strClassificazioneLivello2, _
                               strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                               "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function ComponiStringaSQL(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                ByRef ds As DataSet, _
                                                ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                                ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                ByVal strDatainizio As String, ByVal strDatafine As String, _
                                                ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                                ByVal nDaCausale As Integer, ByVal nACausale As Integer, _
                                                ByVal nDaGruppo As Integer, ByVal nAGruppo As Integer, _
                                                ByVal nDaSottogr As Integer, ByVal nASottogr As Integer, _
                                                ByVal strScarOrdin As String, ByVal nScarCodmarcini As Integer, _
                                                ByVal nScarCodmarcfin As Integer, ByVal strQuery As String, _
                                                ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                                ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                ByVal strClassificazioneLivello5 As String, _
                                                ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Dim bOk As Boolean = False
    Dim nIndex As Integer = 0
    Dim strSQL As String = ""
    Dim strLottoDa As String = strLottoxDa
    Dim strLottoA As String = strLottoxA
    Dim bLottoNew As Boolean = False
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lDaConto, lAConto, lScarDalotto, lScarAlotto, _
                                             lScarDacomme, lScarAcomme, strDatainizio, strDatafine, strScarTipodoc, _
                                             strScarSerie, nDaCausale, nACausale, nDaGruppo, nAGruppo, nDaSottogr, _
                                             nASottogr, strScarOrdin, nScarCodmarcini, nScarCodmarcfin, strQuery, _
                                             strLottoxDa, strLottoxA, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                             strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      If bLottoNew = False Then
        lScarDalotto = NTSCInt(strLottoDa)
        lScarAlotto = NTSCInt(strLottoA)
        strLottoDa = ""
        strLottoA = "".PadLeft(50, "z"c)
      Else
        If strLottoDa = "0" Then strLottoDa = ""
        If strLottoA = "".PadLeft(9, "9"c) Then strLottoA = "".PadLeft(50, "z"c)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "bussp_Bsmgsche_Esegui_1 " & _
                CStrSQL(dtrTmp!km_codart.ToString) & ", " & _
                CDblSQL(dtrTmp!km_magaz.ToString) & ", " & _
                CDblSQL(lDaConto) & ", " & _
                CDblSQL(lAConto) & ", " & _
                CDblSQL(lScarDalotto) & ", " & _
                CDblSQL(lScarAlotto) & ", " & _
                CDblSQL(lScarDacomme) & ", " & _
                CDblSQL(lScarAcomme) & ", " & _
                CDataSQL(strDatainizio) & ", " & _
                CDataSQL(strDatafine) & ", " & _
                NTSCStr(IIf(strScarTipodoc = "", CStrSQL("%"), CStrSQL(strScarTipodoc))) & ", " & _
                NTSCStr(IIf(strScarSerie = "", CStrSQL("%"), CStrSQL(strScarSerie))) & ", " & _
                CDblSQL(nDaCausale) & ", " & _
                CDblSQL(nACausale) & ", " & _
                CDblSQL(nDaGruppo) & ", " & _
                CDblSQL(nAGruppo) & ", " & _
                CDblSQL(nDaSottogr) & ", " & _
                CDblSQL(nASottogr) & ", " & _
                CStrSQL(strScarOrdin) & ", " & _
                CDblSQL(dtrTmp!km_fase.ToString) & ", " & _
                CStrSQL(nScarCodmarcini) & ", " & _
                CDblSQL(nScarCodmarcfin) & ", " & _
                CStrSQL(strDitta) & ", " & _
                CStrSQL(strLottoDa.Trim) & ", " & _
                CStrSQL(strLottoA.Trim) & ", " & _
                CStrSQL(strClassificazioneLivello1.Trim) & ", " & _
                CStrSQL(strClassificazioneLivello2.Trim) & ", " & _
                CStrSQL(strClassificazioneLivello3.Trim) & ", " & _
                CStrSQL(strClassificazioneLivello4.Trim) & ", " & _
                CStrSQL(strClassificazioneLivello5.Trim)
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVMAG")
      '--------------------------------------------------------------------------------------------------------------
      If (strConto = "B") And (nCodlsel <> 0) And (ds.Tables("MOVMAG").Rows.Count > 0) Then
        strSQL = "SELECT lse_conto FROM listsel" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND lse_codlsel = " & nCodlsel
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          For i As Integer = (ds.Tables("MOVMAG").Rows.Count - 1) To 0 Step -1
            bOk = False
            For nIndex = 0 To (dttTmp.Rows.Count - 1)
              If NTSCInt(ds.Tables("MOVMAG").Rows(i)!km_conto) = NTSCInt(dttTmp.Rows(nIndex)!lse_conto) Then
                bOk = True
                Exit For
              End If
            Next nIndex
            If bOk = False Then ds.Tables("MOVMAG").Rows(i).Delete()
          Next i
        End If
        ds.Tables("MOVMAG").AcceptChanges()
      End If
      '--------------------------------------------------------------------------------------------------------------
      ds.Tables("MOVMAG").Columns.Add(New DataColumn("xx_disp", GetType(Decimal)))
      '--------------------------------------------------------------------------------------------------------------
      If Trim(strQuery) <> "" Then
        Dim strSelect As String = ""
        TraduciWhere(Replace(strQuery, "movmag.", ""), strSelect)
        If ds.Tables("MOVMAG").Rows.Count > 0 Then
          For cont As Integer = 0 To ds.Tables("MOVMAG").Rows.Count - 1
            If VerificaRiga(strDitta, NTSCStr(ds.Tables("MOVMAG").Rows(cont)!km_tipork), _
                                      NTSCInt(ds.Tables("MOVMAG").Rows(cont)!km_anno), _
                                      NTSCStr(ds.Tables("MOVMAG").Rows(cont)!km_serie), _
                                      NTSCInt(ds.Tables("MOVMAG").Rows(cont)!km_numdoc), _
                                      NTSCInt(ds.Tables("MOVMAG").Rows(cont)!km_riga), _
                                      strSelect) Then ds.Tables("MOVMAG").Rows(cont).Delete()
          Next
        End If
        ds.Tables("MOVMAG").AcceptChanges()
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, strSQL, oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function VerificaRiga(ByVal strDitta As String, ByVal strTipork As String, _
                                           ByVal nAnno As Integer, ByVal strSerie As String, _
                                           ByVal nNumord As Integer, ByVal strQuery As String) As Boolean
    Try
      Return VerificaRiga(strDitta, strTipork, nAnno, strSerie, nNumord, -1, strQuery)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function VerificaRiga(ByVal strDitta As String, ByVal strTipork As String, _
                                           ByVal nAnno As Integer, ByVal strSerie As String, _
                                           ByVal nNumord As Integer, ByVal nRiga As Integer, _
                                           ByVal strQuery As String) As Boolean
    Dim strSQL As String
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, nAnno, strSerie, nNumord, nRiga, strQuery})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT TOP 1 mm_tipork FROM MOVMAG " & _
               " INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_riga = keymag.km_riga AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_serie = keymag.km_serie AND movmag.mm_anno = keymag.km_anno AND movmag.mm_numdoc = keymag.km_numdoc " & _
               " INNER JOIN testmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_numdoc = movmag.mm_numdoc " & _
               " INNER JOIN artico ON artico.codditt = movmag.codditt AND artico.ar_codart = movmag.mm_codart " & _
               " INNER JOIN anagra ON keymag.codditt = anagra.codditt AND keymag.km_conto = anagra.an_conto " & _
               " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
               "   AND mm_tipork = " & CStrSQL(strTipork) & _
               "   AND mm_anno = " & nAnno & _
               "   AND mm_serie = " & CStrSQL(strSerie) & _
               "   AND mm_numdoc = " & nNumord
      If nRiga > -1 Then strSQL += " AND mm_riga = " & nRiga
      strSQL += strQuery
      Return (OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count = 0)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                            ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                            ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                            ByVal nScarGruppo As Integer, ByVal nScarSottogr As Integer, _
                                            ByVal strScarOrdin As String, ByVal lScarDalotto As Integer, _
                                            ByVal lScarAlotto As Integer, ByVal lScarDacomme As Integer, _
                                            ByVal lScarAcomme As Integer, ByVal strDatini As String, _
                                            ByVal strDatfin As String, ByVal nScarCausale As Integer, _
                                            ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                            ByVal bModuloCRM As Boolean, _
                                            ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                            ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                            ByVal bAmm As Boolean, ByVal strQuery As String) As Boolean
    Try
      'obsoleta
      Return ComponiStringa(strDitta, dtrTmp, ds, lScarDaconto, lScarAconto, strScarTipodoc, strScarSerie, _
                           nScarGruppo, nScarSottogr, strScarOrdin, lScarDalotto, lScarAlotto, lScarDacomme, _
                           lScarAcomme, strDatini, strDatfin, nScarCausale, nScarCodmarcini, nScarCodmarcfin, _
                           bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, strQuery, _
                           "", "".PadLeft(50, "Z"c), "", "", "", "", "")

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                             ByRef ds As DataSet, _
                                             ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                             ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                             ByVal nScarGruppo As Integer, ByVal nScarSottogr As Integer, _
                                             ByVal strScarOrdin As String, ByVal lScarDalotto As Integer, _
                                             ByVal lScarAlotto As Integer, ByVal lScarDacomme As Integer, _
                                             ByVal lScarAcomme As Integer, ByVal strDatini As String, _
                                             ByVal strDatfin As String, ByVal nScarCausale As Integer, _
                                             ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                             ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByVal strQuery As String, _
                                             ByVal strLottoxDa As String, ByVal strLottoxA As String) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lScarDaconto, lScarAconto, strScarTipodoc, _
                                             strScarSerie, nScarGruppo, nScarSottogr, strScarOrdin, lScarDalotto, _
                                             lScarAlotto, lScarDacomme, lScarAcomme, strDatini, strDatfin, _
                                             nScarCausale, nScarCodmarcini, nScarCodmarcfin, bModuloCRM, bIsCRMUser, _
                                             strAccvis, lCodorgaOperat, strRegvis, bAmm, strQuery, _
                                             strLottoxDa, strLottoxA})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return ComponiStringa(strDitta, dtrTmp, ds, lScarDaconto, lScarAconto, strScarTipodoc, strScarSerie, _
                           nScarGruppo, nScarSottogr, strScarOrdin, lScarDalotto, lScarAlotto, lScarDacomme, _
                           lScarAcomme, strDatini, strDatfin, nScarCausale, nScarCodmarcini, nScarCodmarcfin, _
                           bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, strQuery, _
                           strLottoxDa, strLottoxA, "", "", "", "", "")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                             ByRef ds As DataSet, _
                                             ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                             ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                             ByVal nScarGruppo As Integer, ByVal nScarSottogr As Integer, _
                                             ByVal strScarOrdin As String, ByVal lScarDalotto As Integer, _
                                             ByVal lScarAlotto As Integer, ByVal lScarDacomme As Integer, _
                                             ByVal lScarAcomme As Integer, ByVal strDatini As String, _
                                             ByVal strDatfin As String, ByVal nScarCausale As Integer, _
                                             ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                             ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByVal strQuery As String, _
                                             ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lScarDaconto, lScarAconto, strScarTipodoc, _
                                             strScarSerie, nScarGruppo, nScarSottogr, strScarOrdin, lScarDalotto, _
                                             lScarAlotto, lScarDacomme, lScarAcomme, strDatini, strDatfin, _
                                             nScarCausale, nScarCodmarcini, nScarCodmarcfin, bModuloCRM, bIsCRMUser, _
                                             strAccvis, lCodorgaOperat, strRegvis, bAmm, strQuery, _
                                             strLottoxDa, strLottoxA, strClassificazioneLivello1, _
                                             strClassificazioneLivello2, strClassificazioneLivello3, _
                                             strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return ComponiStringa(strDitta, dtrTmp, ds, lScarDaconto, lScarAconto, strScarTipodoc, strScarSerie, _
                            nScarGruppo, nScarSottogr, strScarOrdin, lScarDalotto, lScarAlotto, lScarDacomme, _
                            lScarAcomme, strDatini, strDatfin, nScarCausale, nScarCodmarcini, nScarCodmarcfin, _
                            bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, strQuery, _
                            strLottoxDa, strLottoxA, strClassificazioneLivello1, strClassificazioneLivello2, _
                            strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                            "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                             ByRef ds As DataSet, _
                                             ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                             ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                             ByVal nScarGruppo As Integer, ByVal nScarSottogr As Integer, _
                                             ByVal strScarOrdin As String, ByVal lScarDalotto As Integer, _
                                             ByVal lScarAlotto As Integer, ByVal lScarDacomme As Integer, _
                                             ByVal lScarAcomme As Integer, ByVal strDatini As String, _
                                             ByVal strDatfin As String, ByVal nScarCausale As Integer, _
                                             ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                             ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByVal strQuery As String, _
                                             ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String, _
                                             ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                             ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Dim strSQL As String
    Dim bLottoNew As Boolean = False
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lScarDaconto, lScarAconto, strScarTipodoc, _
                                             strScarSerie, nScarGruppo, nScarSottogr, strScarOrdin, lScarDalotto, _
                                             lScarAlotto, lScarDacomme, lScarAcomme, strDatini, strDatfin, _
                                             nScarCausale, nScarCodmarcini, nScarCodmarcfin, bModuloCRM, bIsCRMUser, _
                                             strAccvis, lCodorgaOperat, strRegvis, bAmm, strQuery, _
                                             strLottoxDa, strLottoxA, strClassificazioneLivello1, _
                                             strClassificazioneLivello2, strClassificazioneLivello3, _
                                             strClassificazioneLivello4, strClassificazioneLivello5, _
                                             strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT keymag.*, mm_quant, mm_prezzo, mm_valore, mm_ornum, mm_scont1, mm_scont2," & _
        " mm_scont3, mm_scont4, mm_scont5, mm_scont6, mm_prelist, tm_riferim, mm_prezvalc, mm_colli, mm_misura1," & _
        " mm_misura2, mm_misura3, mm_controp, mm_commeca, mm_codcena, mm_codcfam, mm_codnomc, mm_provv, mm_vprovv," & _
        " mm_provv2, mm_vprovv2, mm_perqta, tb_descaum," & _
        " CASE WHEN (keymag.km_carscar = 0 OR keymag.km_carscar = 1) THEN movmag.mm_quant ELSE 0 END As CARICHI," & _
        " CASE WHEN (keymag.km_carscar = 0 OR keymag.km_carscar = -1) THEN movmag.mm_quant ELSE 0 END As SCARICHI," & _
        " CASE WHEN (mm_quant = 0) THEN 0 ELSE ((movmag.mm_valore / movmag.mm_quant) * movmag.mm_perqta) END As PREZZO," & _
        " (mm_valore + mm_numpac + mm_numpex) As VALORECSA," & _
        " CASE WHEN (mm_quant = 0) THEN 0 ELSE ((mm_valore + mm_numpac + mm_numpex) / mm_quant) END As VALOREUCSA," & _
        " (mm_numpac + mm_numpex) As SPEACC," & _
        " CASE WHEN (mm_quant = 0) THEN 0 ELSE ((mm_numpac + mm_numpex) / mm_quant) END As SPEACCUN," & _
        " (tabport.tb_desport) As DESPORT, commess.co_descr1,"
      If strScarOrdin = "A" Then
        strSQL += " (anagra.an_descr1) As DESCONTO"
      Else
        strSQL += " ('') As DESCONTO"
      End If
      strSQL += ", analotti.alo_lottox as xx_lottox " & _
        " FROM testmag INNER JOIN movmag ON testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc AND testmag.codditt = movmag.codditt" & _
        " INNER JOIN keymag ON movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga AND movmag.codditt = keymag.codditt" & _
        " INNER JOIN anagra ON keymag.km_conto = anagra.an_conto AND keymag.codditt = anagra.codditt" & _
        " INNER JOIN artico ON keymag.km_codart = artico.ar_codart AND keymag.codditt = artico.codditt" & _
        " LEFT JOIN tabport ON testmag.codditt = tabport.codditt AND testmag.tm_codport = tabport.tb_codport" & _
        " LEFT JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum" & _
        " LEFT JOIN commess ON movmag.codditt = commess.codditt AND movmag.mm_commeca = commess.co_comme" & _
        " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto" & _
        " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
        " AND km_codart = " & CStrSQL(NTSCStr(dtrTmp!km_codart)) & _
        " AND km_magaz = " & NTSCStr(dtrTmp!km_magaz) & _
        " AND km_fase = " & NTSCStr(dtrTmp!km_fase) & _
        IIf(strScarTipodoc <> "", " AND km_tipork = " & CStrSQL(strScarTipodoc), "").ToString & _
        IIf(strScarSerie <> "", " AND km_serie = " & CStrSQL(strScarSerie), "").ToString & _
        IIf(nScarCausale > 0, " AND km_causale = " & nScarCausale, "").ToString & _
        IIf(nScarGruppo > 0, " AND ar_gruppo = " & nScarGruppo, "").ToString & _
        IIf(nScarSottogr > 0, " AND ar_sotgru = " & nScarSottogr, "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
      If (lScarDalotto <> 0) Or (lScarAlotto <> 999999999) Then
        strSQL += " AND km_lotto BETWEEN " & lScarDalotto & " AND " & lScarAlotto
      End If
      If (lScarDacomme <> 0) Or (lScarAcomme <> 999999999) Then
        strSQL += " AND km_commeca BETWEEN " & lScarDacomme & " AND " & lScarAcomme
      End If
      If (NTSCDate(strDatini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strDatfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND km_aammgg BETWEEN " & strDatini & " AND " & strDatfin
      End If
      If bLottoNew = False Then
        If NTSCInt(strLottoxDa) <> 0 Or NTSCInt(strLottoxA) <> 999999999 Then
          strSQL += " AND km_lotto BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      Else
        If strLottoxDa.Replace("0", "").Trim <> "" Or strLottoxA.Replace("999999999", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz").Trim <> "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz" Then
          strSQL += " AND analotti.alo_lottox BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      End If
      If (nScarCodmarcini <> 0) Or (nScarCodmarcfin <> 999) Then
        strSQL = strSQL & " AND ar_codmarc BETWEEN " & nScarCodmarcini & " AND " & nScarCodmarcfin
      End If
      If strScarOrdin = "C" Then
        strSQL += " AND km_conto = " & NTSCStr(dtrTmp!km_conto)
      Else
        Select Case strConto
          Case "A"
            If (lScarDaconto <> 0) Or (lScarAconto <> 999999999) Then
              strSQL += " AND km_conto BETWEEN " & lScarDaconto & " AND " & lScarAconto
            End If
          Case "B"
            If nCodlsel <> 0 Then
              strSQL += " AND km_conto IN (SELECT lse_conto FROM listsel" & _
                                         " WHERE codditt = " & CStrSQL(strDitta) & _
                                         " AND lse_codlsel = " & nCodlsel & ")"
            End If
        End Select
        If (bModuloCRM = True) And (bIsCRMUser = True) And Not (strAccvis = "T" And bAmm = True) Then
          strSQL += " AND ("
          If strAccvis <> "T" Then
            strSQL += "(an_tipo = 'C' AND an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                                                     " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                                                     " AND leads.le_coddest = 0"
            Select Case strAccvis
              Case "P" : strSQL += " AND le_opinc = " & lCodorgaOperat
              Case "C" : strSQL += " AND le_opinc IN (" & strRegvis & ")"
            End Select
            strSQL += "))"
          Else
            strSQL += " anagra.an_tipo = 'C'"
          End If
          If bAmm = False Then strSQL += " AND an_tipo <> 'F'" Else strSQL += " OR an_tipo <> 'C'"
          strSQL += ")"
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery, strSQL)
      '--------------------------------------------------------------------------------------------------------------
      strSQL += " ORDER BY km_codart, km_magaz, km_aammgg, km_carscar DESC, km_tipork, km_serie, km_numdoc, km_riga"
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVMAG")
      '--------------------------------------------------------------------------------------------------------------
      ds.Tables("MOVMAG").Columns.Add(New DataColumn("xx_disp", GetType(Decimal)))
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function GetArtpro(ByVal strDitta As String, ByVal strCodart As String, _
                                         ByVal strFase As String, ByVal strMagaz As String, _
                                         ByRef ds As DataSet) As Boolean
    Dim strSQL As String
    Try
      strSQL = "SELECT ap_esist FROM ARTPRO" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ap_codart = '" & strCodart & "'" & _
        " AND ap_fase = " & strFase & _
        " AND ap_magaz = " & strMagaz & _
        " ORDER BY ap_codart, ap_fase, ap_magaz"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function Apri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                    ByVal strDatini As String, ByVal strDatfin As String, _
                                    ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                    ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                    ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                    ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                    ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                    ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                    ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                    ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                    ByVal nScarSottogr As Integer, ByVal nScarCodmarcini As Integer, _
                                    ByVal nScarCodmarcfin As Integer, ByVal strGrscCodcfam As String, _
                                    ByVal nGrscAnnotco As Integer, ByVal nGrscCodstag As Integer, _
                                    ByVal bModuloCRM As Boolean, ByVal bIsCRMUser As Boolean, _
                                    ByVal strAccvis As String, ByVal bAmm As Boolean, _
                                    ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                    ByVal bScarDaveboll As Boolean, ByRef strError As String, _
                                    ByRef ds As DataSet, ByVal strQuery As String) As Boolean
    Try
      'obsoleta
      Return Apri(strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                 lScarDaconto, lScarAconto, strScarDacodart, strScarAcodart, lScarDacomme, lScarAcomme, _
                 lScarDalotto, lScarAlotto, nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, _
                 nScarCausale, nScarGruppo, nScarSottogr, nScarCodmarcini, nScarCodmarcfin, strGrscCodcfam, _
                 nGrscAnnotco, nGrscCodstag, bModuloCRM, bIsCRMUser, strAccvis, bAmm, lCodorgaOperat, strRegvis, _
                 bScarDaveboll, strError, ds, strQuery, "", "".PadLeft(50, "Z"c), "", "", "", "", "")

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Apri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                   ByVal strDatini As String, ByVal strDatfin As String, _
                                   ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                   ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                   ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                   ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                   ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                   ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                   ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                   ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                   ByVal nScarSottogr As Integer, ByVal nScarCodmarcini As Integer, _
                                   ByVal nScarCodmarcfin As Integer, ByVal strGrscCodcfam As String, _
                                   ByVal nGrscAnnotco As Integer, ByVal nGrscCodstag As Integer, _
                                   ByVal bModuloCRM As Boolean, ByVal bIsCRMUser As Boolean, _
                                   ByVal strAccvis As String, ByVal bAmm As Boolean, _
                                   ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                   ByVal bScarDaveboll As Boolean, ByRef strError As String, _
                                   ByRef ds As DataSet, ByVal strQuery As String, _
                                   ByVal strLottoxDa As String, ByVal strLottoxA As String) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strScarOrdin, strDatini, strDatfin, _
                                             nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                                             strScarDacodart, strScarAcodart, lScarDacomme, lScarAcomme, _
                                             lScarDalotto, lScarAlotto, nScarFaseini, nScarFasefin, _
                                             strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, nScarSottogr, _
                                             nScarCodmarcini, nScarCodmarcfin, strGrscCodcfam, _
                                             nGrscAnnotco, nGrscCodstag, bModuloCRM, bIsCRMUser, strAccvis, bAmm, _
                                             lCodorgaOperat, strRegvis, bScarDaveboll, strError, ds, strQuery, _
                                             strLottoxDa, strLottoxA})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(33))
        ds = CType(oIn(34), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return Apri(strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                 lScarDaconto, lScarAconto, strScarDacodart, strScarAcodart, lScarDacomme, lScarAcomme, _
                 lScarDalotto, lScarAlotto, nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, _
                 nScarCausale, nScarGruppo, nScarSottogr, nScarCodmarcini, nScarCodmarcfin, strGrscCodcfam, _
                 nGrscAnnotco, nGrscCodstag, bModuloCRM, bIsCRMUser, strAccvis, bAmm, lCodorgaOperat, strRegvis, _
                 bScarDaveboll, strError, ds, strQuery, strLottoxDa, strLottoxA, "", "", "", "", "")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function Apri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                   ByVal strDatini As String, ByVal strDatfin As String, _
                                   ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                   ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                   ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                   ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                   ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                   ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                   ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                   ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                   ByVal nScarSottogr As Integer, ByVal nScarCodmarcini As Integer, _
                                   ByVal nScarCodmarcfin As Integer, ByVal strGrscCodcfam As String, _
                                   ByVal nGrscAnnotco As Integer, ByVal nGrscCodstag As Integer, _
                                   ByVal bModuloCRM As Boolean, ByVal bIsCRMUser As Boolean, _
                                   ByVal strAccvis As String, ByVal bAmm As Boolean, _
                                   ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                   ByVal bScarDaveboll As Boolean, ByRef strError As String, _
                                   ByRef ds As DataSet, ByVal strQuery As String, _
                                   ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                   ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                   ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                   ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return Apri(strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                  strScarDacodart, strScarAcodart, lScarDacomme, lScarAcomme, lScarDalotto, lScarAlotto, _
                  nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, nScarSottogr, _
                  nScarCodmarcini, nScarCodmarcfin, strGrscCodcfam, nGrscAnnotco, nGrscCodstag, bModuloCRM, _
                  bIsCRMUser, strAccvis, bAmm, lCodorgaOperat, strRegvis, bScarDaveboll, strError, ds, strQuery, _
                  strLottoxDa, strLottoxA, strClassificazioneLivello1, strClassificazioneLivello2, _
                  strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, "A", 0, "A", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Apri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                   ByVal strDatini As String, ByVal strDatfin As String, _
                                   ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                   ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                   ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                   ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                   ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                   ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                   ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                   ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                   ByVal nScarSottogr As Integer, ByVal nScarCodmarcini As Integer, _
                                   ByVal nScarCodmarcfin As Integer, ByVal strGrscCodcfam As String, _
                                   ByVal nGrscAnnotco As Integer, ByVal nGrscCodstag As Integer, _
                                   ByVal bModuloCRM As Boolean, ByVal bIsCRMUser As Boolean, _
                                   ByVal strAccvis As String, ByVal bAmm As Boolean, _
                                   ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                   ByVal bScarDaveboll As Boolean, ByRef strError As String, _
                                   ByRef ds As DataSet, ByVal strQuery As String, _
                                   ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                   ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                   ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                   ByVal strClassificazioneLivello5 As String, _
                                   ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                   ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim bLottoNew As Boolean = False
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strScarOrdin, strDatini, strDatfin, _
                                             nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                                             strScarDacodart, strScarAcodart, lScarDacomme, lScarAcomme, _
                                             lScarDalotto, lScarAlotto, nScarFaseini, nScarFasefin, _
                                             strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, nScarSottogr, _
                                             nScarCodmarcini, nScarCodmarcfin, strGrscCodcfam, _
                                             nGrscAnnotco, nGrscCodstag, bModuloCRM, bIsCRMUser, strAccvis, bAmm, _
                                             lCodorgaOperat, strRegvis, bScarDaveboll, strError, ds, strQuery, _
                                             strLottoxDa, strLottoxA, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, _
                                             strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(33))
        ds = CType(oIn(34), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT km_codart, km_fase, km_magaz" & IIf(strScarOrdin = "C", ", km_conto", "").ToString & _
        " FROM testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_numdoc = movmag.mm_numdoc AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_tipork = movmag.mm_tipork" & _
        " INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga AND movmag.mm_serie = keymag.km_serie AND movmag.mm_anno = keymag.km_anno AND movmag.mm_tipork = keymag.km_tipork" & _
        " INNER JOIN anagra ON keymag.codditt = anagra.codditt AND keymag.km_conto = anagra.an_conto" & _
        " INNER JOIN artico ON keymag.codditt = artico.codditt AND keymag.km_codart = artico.ar_codart" & _
        " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto" & _
        " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
        " AND artico.ar_stasche = 'S'" & _
        IIf(strScarTipodoc <> "", " AND km_tipork = " & CStrSQL(strScarTipodoc), "").ToString & _
        IIf(strScarSerie <> "", " AND km_serie = " & CStrSQL(strScarSerie), "").ToString & _
        IIf(nScarCausale > 0, " AND km_causale = " & nScarCausale, "").ToString & _
        IIf(nScarGruppo > 0, " AND ar_gruppo =  " & nScarGruppo, "").ToString & _
        IIf(nScarSottogr > 0, " AND ar_sotgru = " & nScarSottogr, "").ToString & _
        IIf(nGrscAnnotco <> 0, " AND ar_anno = " & nGrscAnnotco, "").ToString & _
        IIf(nGrscCodstag <> 0, " AND ar_codstag = " & nGrscCodstag, "").ToString & _
        IIf(strGrscCodcfam <> "", " AND ar_famprod = " & CStrSQL(strGrscCodcfam), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
      If (lScarDacomme <> 0) Or (lScarAcomme <> 999999999) Then
        strSQL += " AND km_commeca BETWEEN " & lScarDacomme & " AND " & lScarAcomme
      End If
      If (nScarDamagaz <> 0) Or (nScarAmagaz <> 9999) Then
        strSQL += " AND km_magaz BETWEEN " & nScarDamagaz & " AND " & nScarAmagaz
      End If
      If (lScarDalotto <> 0) Or (lScarAlotto <> 999999999) Then
        strSQL += " AND km_lotto BETWEEN " & lScarDalotto & " AND " & lScarAlotto
      End If
      If (nScarCodmarcini <> 0) Or (nScarCodmarcfin <> 999) Then
        strSQL += " AND artico.ar_codmarc BETWEEN " & nScarCodmarcini & " AND " & nScarCodmarcfin
      End If
      If (NTSCDate(strDatini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strDatfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND km_aammgg BETWEEN " & strDatini & " AND " & strDatfin
      End If
      Select Case strCodart
        Case "A"
          If (strScarDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or _
             (strScarAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "Z"c)) Then
            strSQL += " AND km_codart BETWEEN " & CStrSQL(strScarDacodart) & " AND " & CStrSQL(strScarAcodart)
          End If
          If (nScarFaseini <> 0) Or (nScarFasefin <> 9999) Then
            strSQL += " AND km_fase BETWEEN " & nScarFaseini & " AND " & nScarFasefin
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL += _
              " AND CAST(km_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(km_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      Select Case strConto
        Case "A"
          If (lScarDaconto <> 0) Or (lScarAconto <> 999999999) Then
            strSQL += " AND km_conto BETWEEN " & lScarDaconto & " AND " & lScarAconto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND km_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      If bLottoNew = False Then
        If NTSCInt(strLottoxDa) <> 0 Or NTSCInt(strLottoxA) <> 999999999 Then
          strSQL += " AND km_lotto BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      Else
        If strLottoxDa.Replace("0", "").Trim <> "" Or strLottoxA.Replace("999999999", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz").Trim <> "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz" Then
          strSQL += " AND analotti.alo_lottox BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (bModuloCRM = True) And (bIsCRMUser = True) And Not (strAccvis = "T" And bAmm = True) Then
        strSQL += " AND ("
        If strAccvis <> "T" Then
          strSQL += "(an_tipo = 'C' AND an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                                                   " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                                                   " AND leads.le_coddest = 0"
          Select Case strAccvis
            Case "P" : strSQL += " AND le_opinc = " & lCodorgaOperat
            Case "C" : strSQL += " AND le_opinc IN (" & strRegvis & ")"
          End Select
          strSQL += "))"
        Else
          strSQL += " anagra.an_tipo = 'C'"
        End If
        If bAmm = False Then
          strSQL += " AND anagra.an_tipo <> 'F'"
        Else
          strSQL += " OR anagra.an_tipo <> 'C'"
        End If
        strSQL += ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery, strSQL)
      '--------------------------------------------------------------------------------------------------------------
      Select Case strScarOrdin
        Case "A" : strSQL += " GROUP BY km_codart, km_fase, km_magaz ORDER BY km_codart, km_fase, km_magaz"
        Case "C"
          strSQL += " GROUP BY km_conto, km_codart, km_fase, km_magaz" & _
            " ORDER BY km_conto, km_codart, km_magaz"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVMAG")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function GetTaglie(ByVal strDitta As String, ByVal strCodart As String, _
                                        ByRef ds As DataSet) As Boolean
    Dim strSQL As String
    Try
      strSQL = "SELECT tabtagl.* FROM artico" & _
        " INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
        " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckArticotaglie(ByVal strDitta As String, ByVal strCodart As String, _
                                        ByRef ds As DataSet) As Boolean
    Dim strSQL As String
    Try
      strSQL = "SELECT artico.ar_codart FROM artico" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart) & _
        " AND ar_codtagl <> 0"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function RiempiTTStloco(ByVal strDitta As String, ByVal bSuTTlocu As Boolean, _
                                             ByVal lIITTStlocu As Integer, ByVal lIITTStloco As Integer, _
                                             ByVal strTTStloco As String, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal strTb_dtulap As String, ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByRef strError As String, _
                                             ByVal strQuery As String) As Boolean
    
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return RiempiTTStloco(strDitta, bSuTTlocu, lIITTStlocu, lIITTStloco, _
                            strTTStloco, stredDatini, stredDatfin, stredDamagaz, _
                            stredAmagaz, stredDacodart, stredAcodart, stredDaconto, _
                            stredAconto, stredFaseini, stredFasefin, bckStorico, _
                            bopLapchiu, bopLaperti, stredInizio, stredFine, _
                            bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                            bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                            bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                            stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                            stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                            stredAnnotco, stredCodstag, strTb_dtulap, bModuloCRM, _
                            bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                            bAmm, strError, strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function RiempiTTStloco(ByVal strDitta As String, ByVal bSuTTlocu As Boolean, _
                                             ByVal lIITTStlocu As Integer, ByVal lIITTStloco As Integer, _
                                             ByVal strTTStloco As String, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal strTb_dtulap As String, ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByRef strError As String, _
                                             ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bSuTTlocu, lIITTStlocu, lIITTStloco, _
                                             strTTStloco, stredDatini, stredDatfin, stredDamagaz, _
                                             stredAmagaz, stredDacodart, stredAcodart, stredDaconto, _
                                             stredAconto, stredFaseini, stredFasefin, bckStorico, _
                                             bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                             bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                             bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                             bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                                             stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                                             stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                             stredAnnotco, stredCodstag, strTb_dtulap, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                             bAmm, strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                             "", 0, "", 0})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(49))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return RiempiTTStloco(strDitta, bSuTTlocu, lIITTStlocu, lIITTStloco, strTTStloco, stredDatini, stredDatfin, _
                            stredDamagaz, stredAmagaz, stredDacodart, stredAcodart, stredDaconto, stredAconto, _
                            stredFaseini, stredFasefin, bckStorico, bopLapchiu, bopLaperti, stredInizio, stredFine, _
                            bopCapchiu, bopCaperte, stredDacomme, stredAcomme, bopUapchiu, bopUaperte, _
                            stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                            stredCausale, stredGruppo, stredSottogr, stredCodmarcini, stredCodmarcfin, _
                            stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, strTb_dtulap, _
                            bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, strError, strQuery, _
                            strClassificazioneLivello1, strClassificazioneLivello2, strClassificazioneLivello3, _
                            strClassificazioneLivello4, strClassificazioneLivello5, "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RiempiTTStloco(ByVal strDitta As String, ByVal bSuTTlocu As Boolean, _
                                             ByVal lIITTStlocu As Integer, ByVal lIITTStloco As Integer, _
                                             ByVal strTTStloco As String, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal strTb_dtulap As String, ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByRef strError As String, _
                                             ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String, _
                                             ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                             ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    ' il parametro è true se deve lavorare su ttstlocu (per le ubicazioni)
    'Elaborazioni su lotti/commesse/ubicazioni/fase
    Dim strSQL As String
    Dim strSQL1 As String
    Dim lII As Integer
    Dim strTT As String
    Dim strInnerJoin As String
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bSuTTlocu, lIITTStlocu, lIITTStloco, _
                                             strTTStloco, stredDatini, stredDatfin, stredDamagaz, _
                                             stredAmagaz, stredDacodart, stredAcodart, stredDaconto, _
                                             stredAconto, stredFaseini, stredFasefin, bckStorico, _
                                             bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                             bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                             bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                             bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                                             stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                                             stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                             stredAnnotco, stredCodstag, strTb_dtulap, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                             bAmm, strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                             strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(49))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strInnerJoin = "testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc" & _
        " INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga" & _
        " INNER JOIN artico ON artico.codditt = movmag.codditt AND artico.ar_codart = movmag.mm_codart" & _
        " INNER JOIN anagra ON anagra.codditt = keymag.codditt AND anagra.an_conto = keymag.km_conto" & _
        " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto "
      '--------------------------------------------------------------------------------------------------------------
      If bSuTTlocu Then
        lII = lIITTStlocu
        strTT = "TTSTLOCU"
      Else
        lII = lIITTStloco
        strTT = strTTStloco
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- COMPONE LA STRINGA DI CODA
      '--------------------------------------------------------------------------------------------------------------
      strSQL1 = " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
        " AND artico.ar_stasche = 'S'" & _
        IIf(NTSCInt(stredCausale) > 0, " AND keymag.km_causale = " & NTSCInt(stredCausale), "").ToString & _
        IIf(NTSCInt(stredGruppo) > 0, " AND artico.ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
        IIf(NTSCInt(stredSottogr) > 0, " AND artico.ar_sotgru = " & NTSCInt(stredSottogr), "").ToString & _
        IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
        IIf(bckStorico = False, " AND keymag.km_aammgg > " & CDataSQL(strTb_dtulap), "").ToString
      If (NTSCDate(stredDatini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(stredDatfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL1 += " AND keymag.km_aammgg BETWEEN " & CDataSQL(stredDatini) & " AND " & CDataSQL(stredDatfin)
      End If
      If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
        strSQL1 += " AND keymag.km_magaz BETWEEN " & NTSCInt(stredDamagaz) & " AND " & NTSCInt(stredAmagaz)
      End If
      Select Case strCodart
        Case "A"
          If (stredDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or _
             (stredAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "Z"c)) Then
            strSQL1 += " AND keymag.km_codart BETWEEN " & CStrSQL(stredDacodart) & " AND " & CStrSQL(stredAcodart)
          End If
          If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
            strSQL1 += " AND keymag.km_fase BETWEEN " & NTSCInt(stredFaseini) & " AND " & NTSCInt(stredFasefin)
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL1 += _
              " AND CAST(km_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(km_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      Select Case strConto
        Case "A"
          If (NTSCInt(stredDaconto) <> 0) Or (NTSCInt(stredAconto) <> 999999999) Then
            strSQL1 += " AND keymag.km_conto BETWEEN " & NTSCInt(stredDaconto) & " AND " & NTSCInt(stredAconto)
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL1 += " AND km_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      If (bopLapchiu = True) Or (bopLaperti = True) Then
        strSQL1 += " AND analotti.alo_lottox BETWEEN " & CStrSQL(stredInizio) & " AND " & CStrSQL(stredFine) & _
          " AND artico.ar_geslotti = 'S'"
      End If
      If (bopCapchiu = True) Or (bopCaperte = True) Then
        strSQL1 += " AND artico.ar_gescomm = 'S'"
        If (NTSCInt(stredDacomme) <> 0) Or (NTSCInt(stredAcomme) <> 999999999) Then
          strSQL1 += " AND keymag.km_commeca BETWEEN " & NTSCInt(stredDacomme) & " AND " & NTSCInt(stredAcomme)
        End If
      End If
      If (bopUapchiu = True) Or (bopUaperte = True) Then
        strSQL1 = strSQL1 & " AND keymag.km_ubicaz BETWEEN " & CStrSQL(stredUbicazini) & " AND " & CStrSQL(stredUbicazfin) & _
          " AND artico.ar_gesubic = 'S'"
      End If
      If bopPrelevaSolo Then
        strSQL1 += " AND keymag.km_tipork = " & CStrSQL(strcbTipodoc.ToUpper)
        If (bckSerie = True) And (stredSerie.Length > 0) Then strSQL1 += " AND keymag.km_serie = " & CStrSQL(stredSerie)
      End If
      If (NTSCInt(stredCodmarcini) <> 0) Or (NTSCInt(stredCodmarcfin) <> 999) Then
        strSQL1 += " AND artico.ar_codmarc BETWEEN " & NTSCInt(stredCodmarcini) & " AND " & NTSCInt(stredCodmarcfin)
      End If
      If bModTCO = True Then
        If bckSelAnnoStag = True Then
          If NTSCInt(stredAnnotco) <> 0 Then strSQL1 += " AND artico.ar_anno = " & NTSCInt(stredAnnotco)
          If NTSCInt(stredCodstag) <> 0 Then strSQL1 += " AND artico.ar_codstag = " & NTSCInt(stredCodstag)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiungo la where dei campi con OR e AND rimappati
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery, strSQL1)
      '--------------------------------------------------------------------------------------------------------------
      '--- Riempie TTSTLOCO da Testmag/Keymag/Movmag/Artico
      '--------------------------------------------------------------------------------------------------------------
      If bSuTTlocu = False Then
        strSQL = "INSERT INTO " & strTTStloco & " (codditt, instid, tt_codart, tt_magaz, tt_lotto, tt_aammgg," & _
         " tt_causale, tt_conto, tt_riferim, tt_tipork, tt_anno, tt_serie, tt_numdoc, tt_riga, tt_quant, tt_prezzo," & _
         " tt_valore, tt_carscar, tt_qtacar, tt_valcar, tt_fase)" & _
         " SELECT " & CStrSQL(strDitta) & ", " & lIITTStloco & ", km_codart, km_magaz," & _
         IIf((bopLapchiu = True) Or (bopLaperti = True), " km_lotto,", "").ToString & _
         IIf((bopCapchiu = True) Or (bopCaperte = True), " km_commeca,", "").ToString & _
         " km_aammgg, km_causale, km_conto, tm_riferim, km_tipork, km_anno, km_serie, km_numdoc, km_riga, mm_quant," & _
         " CASE WHEN mm_quant <> 0 THEN mm_valore / mm_quant ELSE 0 END, mm_valore, km_carscar, 0, 0, km_fase" & _
         " FROM " & strInnerJoin & strSQL1 & _
         " AND mm_quant <> 0"
      Else 'locu, ubicazioni
        strSQL = "INSERT INTO TTSTLOCU (codditt, instid, tt_codart, tt_magaz, tt_ubicaz, tt_aammgg, tt_causale," & _
          " tt_conto, tt_riferim, tt_tipork, tt_anno, tt_serie, tt_numdoc, tt_riga, tt_quant, tt_prezzo, tt_valore," & _
          " tt_carscar, tt_qtacar, tt_valcar, tt_fase)" & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTStlocu & ", km_codart, km_magaz, km_ubicaz, km_aammgg," & _
          " km_causale, km_conto, tm_riferim, km_tipork, km_anno, km_serie, km_numdoc, km_riga, mm_quant," & _
          " CASE WHEN mm_quant <> 0 THEN mm_valore / mm_quant ELSE 0 END, mm_valore, km_carscar, 0, 0, km_fase" & _
          " FROM " & strInnerJoin & strSQL1 & _
          " AND mm_quant <> 0"
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (bModuloCRM = True) And (bIsCRMUser = True) Then
        strSQL += " AND ("
        If strAccvis <> "T" Then
          strSQL += "(an_tipo = 'C' AND an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                                                   " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                                                   " AND leads.le_coddest = 0"
          Select Case strAccvis
            Case "P" : strSQL += " AND le_opinc = " & lCodorgaOperat
            Case "C" : strSQL += " AND le_opinc IN (" & strRegvis & ")"
          End Select
          strSQL += "))"
        Else
          strSQL += " anagra.an_tipo = 'C'"
        End If
        If bAmm = False Then strSQL += " AND an_tipo <> 'F'" Else strSQL += " OR an_tipo <> 'C'"
        strSQL += ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Riempie TTSTLOCO con gli eventuali records in Lotcdef
      '--------------------------------------------------------------------------------------------------------------
      '--- visto che in ttstloco non c'è in chiave la fase articolo, la metto anche nel campo 'riga'
      '--- diversamente se ho lo stesso lotto sullo stesso articolo e su 2 fasi diverse da errore di chiave duplicata
      '--------------------------------------------------------------------------------------------------------------
      If (bopLapchiu = True) Or (bopLaperti = True) Or _
         (bopCapchiu = True) Or (bopCaperte = True) Or _
         (bopUapchiu = True) Or (bopUaperte = True) Then
        strSQL = "INSERT INTO " & strTT & " (codditt, instid, tt_codart, tt_magaz, " & _
          NTSCStr(IIf(bSuTTlocu, "tt_ubicaz", "tt_lotto")) & " , tt_aammgg, tt_causale, tt_conto, tt_riferim," & _
          " tt_tipork, tt_anno, tt_serie, tt_numdoc, tt_riga, tt_quant, tt_prezzo, tt_valore, tt_carscar," & _
          " tt_qtacar, tt_valcar, tt_fase)"
        If (bopLapchiu = True) Or (bopLaperti = True) Then
          strSQL += " SELECT " & CStrSQL(strDitta) & ", " & lIITTStloco & ", ld_codart, ld_magaz, ld_lotto, " & _
            CDataSQL(strTb_dtulap) & ", 0, 0, 'Saldo precedente', ' ', 0, ' ', 0, ld_fase, Sum(ld_quant), 0," & _
            " Sum(ld_valore), 1, 0, 0, ld_fase " & _
            " FROM lotcdef INNER JOIN artico ON lotcdef.codditt = artico.codditt AND lotcdef.ld_codart = artico.ar_codart" & _
            " LEFT JOIN analotti ON lotcdef.codditt = analotti.codditt AND lotcdef.ld_codart = analotti.alo_codart AND lotcdef.ld_lotto = analotti.alo_lotto " & _
            " WHERE lotcdef.codditt = " & CStrSQL(strDitta) & _
            " AND ld_lotto <> 0" & _
            " AND analotti.alo_lottox BETWEEN " & CStrSQL(stredInizio) & " AND " & CStrSQL(stredFine) & _
            IIf(NTSCInt(stredGruppo) > 0, " AND artico.ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
            IIf(NTSCInt(stredSottogr) > 0, " AND artico.ar_sotgru = " & NTSCInt(stredSottogr), "").ToString & _
            IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString & _
            IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
            IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
            IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
            IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
            IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
          If (NTSCInt(stredCodmarcini) <> 0) Or (NTSCInt(stredCodmarcfin) <> 999) Then
            strSQL += " AND artico.ar_codmarc BETWEEN " & NTSCInt(stredCodmarcini) & " AND " & NTSCInt(stredCodmarcfin)
          End If
          If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
            strSQL += " AND ld_magaz BETWEEN " & NTSCInt(stredDamagaz) & " AND " & NTSCInt(stredAmagaz)
          End If
          Select Case strCodart
            Case "A"
              If (stredDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or _
                 (stredAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "Z"c)) Then
                strSQL += " AND ld_codart BETWEEN '" & stredDacodart & "' AND '" & stredAcodart & "'"
              End If
              If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
                strSQL += " AND ld_fase BETWEEN " & NTSCInt(stredFaseini) & " AND " & NTSCInt(stredFasefin)
              End If
            Case "B"
              If nCodlsar <> 0 Then
                strSQL += _
                  " AND CAST(ld_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(ld_fase AS VARCHAR(4))" & _
                  " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND lsa_codlsar = " & nCodlsar & ")"
              End If
          End Select
          If bModTCO = True Then
            If bckSelAnnoStag = True Then
              If NTSCInt(stredAnnotco) <> 0 Then strSQL += " AND artico.ar_anno = " & NTSCInt(stredAnnotco)
              If NTSCInt(stredCodstag) <> 0 Then strSQL += " AND artico.ar_codstag = " & NTSCInt(stredCodstag)
            End If
          End If
          strSQL += " GROUP BY ld_codart, ld_fase, ld_magaz, ld_lotto"
        End If
        '------------------------------------------------------------------------------------------------------------
        If (bopCapchiu = True) Or (bopCaperte = True) Then
          strSQL += " SELECT " & CStrSQL(strDitta) & ", " & lIITTStloco & ",ld_codart, ld_magaz, ld_commeca, " & _
          CDataSQL(strTb_dtulap) & ", 0, 0, 'Saldo precedente', ' ', 0, ' ', 0, ld_fase, Sum(ld_quant), 0," & _
          " Sum(ld_valore), 1, 0, 0, ld_fase" & _
          " FROM lotcdef INNER JOIN artico ON lotcdef.codditt = artico.codditt AND lotcdef.ld_codart = artico.ar_codart" & _
          " WHERE lotcdef.codditt = " & CStrSQL(strDitta) & _
          " AND ld_commeca <> 0" & _
          IIf(NTSCInt(stredGruppo) > 0, " AND artico.ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
          IIf(NTSCInt(stredSottogr) > 0, " AND artico.ar_sotgru = " & NTSCInt(stredSottogr), "").ToString & _
          IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString & _
          IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
          IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
          IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
          IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
          IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
          If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
            strSQL += " AND ld_magaz BETWEEN " & NTSCInt(stredDamagaz) & " AND " & NTSCInt(stredAmagaz)
          End If
          If (NTSCInt(stredDacomme) <> 0) Or (NTSCInt(stredAcomme) <> 999999999) Then
            strSQL += " AND ld_commeca BETWEEN " & NTSCInt(stredDacomme) & " AND " & NTSCInt(stredAcomme)
          End If
          If (NTSCInt(stredCodmarcini) <> 0) Or (NTSCInt(stredCodmarcfin) <> 999) Then
            strSQL += " AND artico.ar_codmarc BETWEEN " & NTSCInt(stredCodmarcini) & " AND " & NTSCInt(stredCodmarcfin)
          End If
          Select Case strCodart
            Case "A"
              If (stredDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or _
                 (stredAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "Z"c)) Then
                strSQL += " AND ld_codart BETWEEN '" & stredDacodart & "' AND '" & stredAcodart & "'"
              End If
              If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
                strSQL += " AND ld_fase BETWEEN " & NTSCInt(stredFaseini) & " AND " & NTSCInt(stredFasefin)
              End If
            Case "B"
              If nCodlsar <> 0 Then
                strSQL += _
                  " AND CAST(ld_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(ld_fase AS VARCHAR(4))" & _
                  " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND lsa_codlsar = " & nCodlsar & ")"
              End If
          End Select
          If bModTCO = True Then
            If bckSelAnnoStag = True Then
              If NTSCInt(stredAnnotco) <> 0 Then strSQL += " AND artico.ar_anno = " & NTSCInt(stredAnnotco)
              If NTSCInt(stredCodstag) <> 0 Then strSQL += " AND artico.ar_codstag = " & NTSCInt(stredCodstag)
            End If
          End If
          strSQL = strSQL & " GROUP BY ld_codart, ld_fase, ld_magaz, ld_commeca"
        End If
        '------------------------------------------------------------------------------------------------------------
        If (bopUapchiu = True) Or (bopUaperte = True) Then
          strSQL += " SELECT " & CStrSQL(strDitta) & ", " & lIITTStlocu & ", ld_codart, ld_magaz, ld_ubicaz, " & _
            CDataSQL(strTb_dtulap) & ", 0, 0, 'Saldo precedente', ' ', 0, ' ', 0, ld_fase, Sum(ld_quant), 0," & _
            " Sum(ld_valore), 1, 0, 0, ld_fase" & _
            " FROM lotcdef INNER JOIN artico ON lotcdef.codditt = artico.codditt AND lotcdef.ld_codart = artico.ar_codart" & _
            " LEFT JOIN analotti ON lotcdef.codditt = analotti.codditt AND lotcdef.ld_codart = analotti.alo_codart AND lotcdef.ld_lotto = analotti.alo_lotto " & _
            " WHERE lotcdef.codditt = " & CStrSQL(strDitta) & _
            " AND ld_ubicaz <> ' '" & _
            IIf(NTSCInt(stredGruppo) > 0, " AND artico.ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
            IIf(NTSCInt(stredSottogr) > 0, " AND artico.ar_sotgru = " & NTSCInt(stredSottogr), "").ToString & _
            IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString & _
            IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
            IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
            IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
            IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
            IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
            " AND analotti.alo_lottox BETWEEN " & CStrSQL(stredInizio) & " AND " & CStrSQL(stredFine) & _
            " AND ld_ubicaz BETWEEN " & CStrSQL(stredUbicazini) & " AND " & CStrSQL(stredUbicazfin) & _
            " AND artico.ar_gesubic = 'S'"
          If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
            strSQL += " AND ld_magaz BETWEEN " & NTSCInt(stredDamagaz) & " AND " & NTSCInt(stredAmagaz)
          End If
          If (NTSCInt(stredCodmarcini) <> 0) Or (NTSCInt(stredCodmarcfin) <> 999) Then
            strSQL += " AND artico.ar_codmarc BETWEEN " & NTSCInt(stredCodmarcini) & " AND " & NTSCInt(stredCodmarcfin)
          End If
          Select Case strCodart
            Case "A"
              If (stredDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or _
                 (stredAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "Z"c)) Then
                strSQL += " AND ld_codart BETWEEN '" & stredDacodart & "' AND '" & stredAcodart & "'"
              End If
              If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
                strSQL += " AND ld_fase BETWEEN " & NTSCInt(stredFaseini) & " AND " & NTSCInt(stredFasefin)
              End If
            Case "B"
              If nCodlsar <> 0 Then
                strSQL += _
                  " AND CAST(ld_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(ld_fase AS VARCHAR(4))" & _
                  " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND lsa_codlsar = " & nCodlsar & ")"
              End If
          End Select
          If bModTCO = True Then
            If bckSelAnnoStag = True Then
              If NTSCInt(stredAnnotco) <> 0 Then strSQL += " AND artico.ar_anno = " & NTSCInt(stredAnnotco)
              If NTSCInt(stredCodstag) <> 0 Then strSQL += " AND artico.ar_codstag = " & NTSCInt(stredCodstag)
            End If
          End If
          strSQL += " GROUP BY ld_codart, ld_fase, ld_magaz, ld_ubicaz"
        End If
        If bckStorico = False Then Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        '------------------------------------------------------------------------------------------------------------
        '--- Determinazione prezzo per i records da Lotcdef
        '------------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE " & strTT & _
          " SET tt_prezzo = (tt_valore / tt_quant)" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lII & _
          " AND tt_tipork = ' '" & _
          " AND tt_anno = 0" & _
          " AND tt_serie = ' '" & _
          " AND tt_numdoc = 0" & _
          " AND tt_riga = 0" & _
          " AND tt_quant <> 0"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '------------------------------------------------------------------------------------------------------------
      '--- Cancella i lotti o le commesse chiuse (in caso di 'A LOTTI/COMMESSE APERTI')
      '------------------------------------------------------------------------------------------------------------
      If (bopLaperti = True) Or (bopCaperte = True) Or (bopUaperte = True) Then
        strSQL = "SELECT tt_codart, tt_fase, tt_magaz, " & NTSCStr(IIf(bSuTTlocu, "tt_ubicaz", "tt_lotto")) & _
          " FROM " & strTT & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lII & _
          " GROUP BY tt_codart, tt_fase, tt_magaz, " & NTSCStr(IIf(bSuTTlocu, "tt_ubicaz", "tt_lotto")) & _
          " HAVING Round((Sum(tt_quant * tt_carscar)), 3) = 0"
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
        For i = 0 To dsTmp.Tables("TEMP").Rows.Count - 1
          With dsTmp.Tables("TEMP").Rows(i)
            strSQL = "DELETE " & strTT & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lII & _
                " AND tt_codart = " & CStrSQL(!tt_codart) & _
                " AND tt_magaz = " & NTSCStr(!tt_magaz) & _
                " AND tt_fase = " & NTSCStr(!tt_fase)
            If bSuTTlocu Then
              strSQL += " AND tt_ubicaz = " & CStrSQL(!tt_ubicaz)
            Else
              strSQL += " AND tt_lotto = " & NTSCStr(!tt_lotto)
            End If
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          End With
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tt_codart, tt_fase, tt_magaz, " & NTSCStr(IIf(bSuTTlocu, "tt_ubicaz", "tt_lotto")) & "," & _
        " Sum(tt_quant)As Quant, Sum(tt_valore)As Valcar FROM " & strTT & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lII & _
        " AND tt_carscar = 1" & _
        " GROUP BY tt_codart, tt_fase, tt_magaz, " & NTSCStr(IIf(bSuTTlocu, "tt_ubicaz", "tt_lotto")) & _
        " ORDER BY tt_codart, tt_fase, tt_magaz, " & NTSCStr(IIf(bSuTTlocu, "tt_ubicaz", "tt_lotto"))
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
      For i = 0 To dsTmp.Tables("TEMP").Rows.Count - 1
        With dsTmp.Tables("TEMP").Rows(i)
          strSQL = "UPDATE " & strTT & _
            " SET tt_qtacar = " & CDblSQL(NTSCDec(!Quant)) & "," & _
            " tt_valcar = " & CDblSQL(NTSCDec(!Valcar)) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lII & _
            " AND tt_codart = " & CStrSQL(!tt_codart) & _
            " AND tt_magaz = " & NTSCStr(!tt_magaz) & _
            " AND tt_fase = " & NTSCStr(!tt_fase)
          If bSuTTlocu Then
            strSQL += " AND tt_ubicaz = " & CStrSQL(!tt_ubicaz)
          Else
            strSQL += " AND tt_lotto = " & NTSCStr(!tt_lotto)
          End If
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla l'esistenza di record nella tabella temporanea TTSTLOCO
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tt_codart FROM " & strTT & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lII & _
        " ORDER BY tt_codart, tt_fase, tt_magaz,  tt_aammgg, tt_tipork, tt_anno, tt_serie, tt_numdoc, tt_riga"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
      If dsTmp.Tables("TEMP").Rows.Count = 0 Then
        strError = "Non esistono dati con queste caratteristiche."
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RiempiTTStlocs(ByVal strDitta As String, ByVal lIITTStlocs As Integer, _
                                             ByVal strTTStlocs As String, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal strTb_dtulap As String, ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByVal bopLsaldo As Boolean, _
                                             ByVal bopCsaldo As Boolean, ByVal bopUsaldo As Boolean, _
                                             ByRef strError As String, ByVal strQuery As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return RiempiTTStlocs(strDitta, lIITTStlocs, strTTStlocs, stredDatini, stredDatfin, stredDamagaz, _
        stredAmagaz, stredDacodart, stredAcodart, stredDaconto, stredAconto, stredFaseini, stredFasefin, _
        bckStorico, bopLapchiu, bopLaperti, stredInizio, stredFine, bopCapchiu, bopCaperte, stredDacomme, _
        stredAcomme, bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, _
        bckSerie, stredSerie, stredCausale, stredGruppo, stredSottogr, stredCodmarcini, stredCodmarcfin, _
        stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, strTb_dtulap, bModuloCRM, _
        bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, bopLsaldo, bopCsaldo, bopUsaldo, strError, _
        strQuery, "", "", "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RiempiTTStlocs(ByVal strDitta As String, ByVal lIITTStlocs As Integer, _
                                             ByVal strTTStlocs As String, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal strTb_dtulap As String, ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByVal bopLsaldo As Boolean, _
                                             ByVal bopCsaldo As Boolean, ByVal bopUsaldo As Boolean, _
                                             ByRef strError As String, ByVal strQuery As String, _
                                             ByVal strLottoxDa As String, ByVal strLottoxA As String) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITTStlocs, strTTStlocs, stredDatini, stredDatfin, _
                                             stredDamagaz, stredAmagaz, stredDacodart, stredAcodart, _
                                             stredDaconto, stredAconto, stredFaseini, stredFasefin, _
                                             bckStorico, bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                             bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                             bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                             bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, stredCausale, _
                                             stredGruppo, stredSottogr, stredCodmarcini, stredCodmarcfin, _
                                             stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                             strTb_dtulap, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                                             strRegvis, bAmm, bopLsaldo, bopCsaldo, bopUsaldo, strError, strQuery, _
                                             strLottoxDa, strLottoxA})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(50))
        Return CBool(oOut)
      End If
      '----------------
      '--------------------------------------------------------------------------------------------------------------
      Return RiempiTTStlocs(strDitta, lIITTStlocs, strTTStlocs, stredDatini, stredDatfin, stredDamagaz, _
        stredAmagaz, stredDacodart, stredAcodart, stredDaconto, stredAconto, stredFaseini, stredFasefin, _
        bckStorico, bopLapchiu, bopLaperti, stredInizio, stredFine, bopCapchiu, bopCaperte, stredDacomme, _
        stredAcomme, bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, _
        bckSerie, stredSerie, stredCausale, stredGruppo, stredSottogr, stredCodmarcini, stredCodmarcfin, _
        stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, strTb_dtulap, bModuloCRM, _
        bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, bopLsaldo, bopCsaldo, bopUsaldo, strError, _
        strQuery, strLottoxDa, strLottoxA, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RiempiTTStlocs(ByVal strDitta As String, ByVal lIITTStlocs As Integer, _
                                             ByVal strTTStlocs As String, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal strTb_dtulap As String, ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByVal bopLsaldo As Boolean, _
                                             ByVal bopCsaldo As Boolean, ByVal bopUsaldo As Boolean, _
                                             ByRef strError As String, ByVal strQuery As String, _
                                             ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITTStlocs, strTTStlocs, stredDatini, stredDatfin, _
                                             stredDamagaz, stredAmagaz, stredDacodart, stredAcodart, _
                                             stredDaconto, stredAconto, stredFaseini, stredFasefin, _
                                             bckStorico, bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                             bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                             bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                             bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, stredCausale, _
                                             stredGruppo, stredSottogr, stredCodmarcini, stredCodmarcfin, _
                                             stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                             strTb_dtulap, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                                             strRegvis, bAmm, bopLsaldo, bopCsaldo, bopUsaldo, strError, strQuery, _
                                             strLottoxDa, strLottoxA, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(50))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return RiempiTTStlocs(strDitta, lIITTStlocs, strTTStlocs, stredDatini, stredDatfin, stredDamagaz, _
                            stredAmagaz, stredDacodart, stredAcodart, stredDaconto, stredAconto, stredFaseini, stredFasefin, _
                            bckStorico, bopLapchiu, bopLaperti, stredInizio, stredFine, bopCapchiu, bopCaperte, stredDacomme, _
                            stredAcomme, bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, _
                            bckSerie, stredSerie, stredCausale, stredGruppo, stredSottogr, stredCodmarcini, stredCodmarcfin, _
                            stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, strTb_dtulap, bModuloCRM, _
                            bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, bopLsaldo, bopCsaldo, bopUsaldo, strError, _
                            strQuery, strLottoxDa, strLottoxA, strClassificazioneLivello1, strClassificazioneLivello2, _
                            strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                            "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RiempiTTStlocs(ByVal strDitta As String, ByVal lIITTStlocs As Integer, _
                                             ByVal strTTStlocs As String, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal strTb_dtulap As String, ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByVal bopLsaldo As Boolean, _
                                             ByVal bopCsaldo As Boolean, ByVal bopUsaldo As Boolean, _
                                             ByRef strError As String, ByVal strQuery As String, _
                                             ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String, _
                                             ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                             ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Dim bLottoNew As Boolean = False
    Dim i As Integer = 0
    Dim strSQL As String = ""
    Dim strSQL1 As String = ""
    Dim strTipork As String = ""
    Dim strInnerJoin As String = ""
    Dim strCampoTmp As String = ""
    Dim dttTmp As New DataTable
    Dim dttKeymag As New DataTable
    Dim lIIttlotcpro As Integer = 0
    Dim strCampiTTSTLOCS As String = "codditt, instid, tt_codart, tt_magaz, tt_lotto, tt_ubicaz, tt_quant," & _
      " tt_valore, tt_qtacar, tt_valcar, tt_fase"

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITTStlocs, strTTStlocs, stredDatini, stredDatfin, _
                                             stredDamagaz, stredAmagaz, stredDacodart, stredAcodart, _
                                             stredDaconto, stredAconto, stredFaseini, stredFasefin, _
                                             bckStorico, bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                             bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                             bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                             bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, stredCausale, _
                                             stredGruppo, stredSottogr, stredCodmarcini, stredCodmarcfin, _
                                             stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                             strTb_dtulap, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                                             strRegvis, bAmm, bopLsaldo, bopCsaldo, bopUsaldo, strError, strQuery, _
                                             strLottoxDa, strLottoxA, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(50))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      lIIttlotcpro = GetTblInstId("TTLOTCPRO", False)
      '--------------------------------------------------------------------------------------------------------------
      ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strInnerJoin = "testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc" & _
        " INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga" & _
        " INNER JOIN artico ON artico.codditt = movmag.codditt AND artico.ar_codart = movmag.mm_codart" & _
        " INNER JOIN anagra ON anagra.codditt = keymag.codditt AND anagra.an_conto = keymag.km_conto" & _
        " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto"
      '--------------------------------------------------------------------------------------------------------------
      '--- COMPONE LA STRINGA DI CODA
      '--------------------------------------------------------------------------------------------------------------
      strSQL1 = " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
        " AND artico.ar_stasche = 'S'" & _
        IIf(bckStorico = False, " AND keymag.km_aammgg > " & CDataSQL(NTSCStr(strTb_dtulap)), "").ToString & _
        IIf(NTSCInt(stredCausale) <> 0, " AND keymag.km_causale = " & NTSCInt(stredCausale), "").ToString & _
        IIf(NTSCInt(stredGruppo) <> 0, " AND artico.ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
        IIf(NTSCInt(stredSottogr) <> 0, " AND artico.ar_sotgru = " & NTSCInt(stredSottogr), "").ToString & _
        IIf(stredCodcfam.ToUpper <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
      If (NTSCDate(stredDatini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(stredDatfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL1 += " AND keymag.km_aammgg BETWEEN " & CDataSQL(stredDatini) & " AND " & CDataSQL(stredDatfin)
      End If
      If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
        strSQL1 += " AND keymag.km_magaz BETWEEN " & NTSCInt(stredDamagaz) & " AND " & NTSCInt(stredAmagaz)
      End If
      If (NTSCInt(stredCodmarcini) <> 0) Or (NTSCInt(stredCodmarcfin) <> 999) Then
        strSQL1 += " AND artico.ar_codmarc BETWEEN " & NTSCInt(stredCodmarcini) & " AND " & NTSCInt(stredCodmarcfin)
      End If
      Select Case strCodart
        Case "A"
          If (stredDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or _
             (stredAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strSQL1 += " AND keymag.km_codart BETWEEN " & CStrSQL(stredDacodart) & " AND " & CStrSQL(stredAcodart)
          End If
          If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
            strSQL1 += " AND keymag.km_fase BETWEEN " & NTSCInt(stredFaseini) & " AND " & NTSCInt(stredFasefin)
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL1 += _
              " AND CAST(km_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(km_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      Select Case strConto
        Case "A"
          If (NTSCInt(stredDaconto) <> 0) Or (NTSCInt(stredAconto) <> 999999999) Then
            strSQL1 += " AND keymag.km_conto BETWEEN " & NTSCInt(stredDaconto) & " AND " & NTSCInt(stredAconto)
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL1 += " AND km_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      If bopLsaldo = True Then
        strSQL1 += " AND artico.ar_geslotti = 'S'"
        If bLottoNew = False Then
          If (NTSCInt(stredInizio) <> 0) Or (NTSCInt(stredFine) <> 999999999) Then
            strSQL1 += " AND keymag.km_lotto BETWEEN " & NTSCInt(stredInizio) & " AND " & NTSCInt(stredFine)
          End If
        Else
          If strLottoxDa.Replace("0", "").Trim <> "" Or strLottoxA.Replace("999999999", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz").Trim <> "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz" Then
            strSQL1 += " AND analotti.alo_lottox BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
          End If
        End If
      End If
      If bopCsaldo = True Then
        strSQL1 += " AND artico.ar_gescomm = 'S'"
        If (NTSCInt(stredDacomme) <> 0) Or (NTSCInt(stredAcomme) <> 999999999) Then
          strSQL1 += " AND keymag.km_commeca BETWEEN " & NTSCInt(stredDacomme) & " AND " & NTSCInt(stredAcomme)
        End If
      End If
      If bopUsaldo = True Then
        strSQL1 += " AND keymag.km_ubicaz BETWEEN " & CStrSQL(stredUbicazini) & " AND " & CStrSQL(stredUbicazfin) & _
          " AND artico.ar_gesubic = 'S'"
      End If
      If bopPrelevaSolo = True Then
        strSQL1 += " AND keymag.km_tipork = " & CStrSQL(strcbTipodoc.ToUpper)
        If (bckSerie = True) And (stredSerie.Length > 0) Then
          strSQL1 += " AND keymag.km_serie = " & CStrSQL(stredSerie)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiungo la where dei campi con OR e AND rimappati
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery, strSQL1)
      '--------------------------------------------------------------------------------------------------------------
      '--- INSERT INTO TTSTLOCS da Movmag/Keymag/Testmag/Artico
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTSTLOCS (" & strCampiTTSTLOCS & ")" & _
        " SELECT " & CStrSQL(strDitta) & ", " & lIITTStlocs & ", km_codart, km_magaz,"
      If bopLsaldo = True Then strSQL += " km_lotto,"
      If bopCsaldo = True Then strSQL += " km_commeca,"
      If bopUsaldo = True Then strSQL += " 0,"
      If bopUsaldo = True Then strSQL += " km_ubicaz, " Else strSQL += " ' ', "
      strSQL += " Round((Sum(mm_quant * km_carscar)), 3), 0, 0, 0, km_fase FROM " & strInnerJoin & strSQL1
      '--------------------------------------------------------------------------------------------------------------
      If (bModuloCRM = True) And (bIsCRMUser = True) Then
        strSQL += " AND ("
        If strAccvis <> "T" Then
          strSQL += "(an_tipo = 'C' AND an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                                                   " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                                                   " AND leads.le_coddest = 0"
          Select Case strAccvis
            Case "P" : strSQL += " AND le_opinc = " & lCodorgaOperat
            Case "C" : strSQL += " AND le_opinc IN (" & strRegvis & ")"
          End Select
          strSQL += "))"
        Else
          strSQL += " anagra.an_tipo = 'C'"
        End If
        If bAmm = False Then strSQL += " AND an_tipo <> 'F'" Else strSQL += " OR an_tipo <> 'C'"
        strSQL += ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bopLsaldo = True Then strSQL += " GROUP BY km_codart, km_fase, km_magaz, km_lotto"
      If bopCsaldo = True Then strSQL += " GROUP BY km_codart, km_fase, km_magaz, km_commeca"
      If bopUsaldo = True Then strSQL += " GROUP BY km_codart, km_fase, km_magaz, km_ubicaz"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiorna le quantità, il totale dei carichi e il totale del valore dei carichi
      '--- prelevando i dati da LOTCDEF
      '--------------------------------------------------------------------------------------------------------------
      If bckStorico = False Then ' caso normale, se non è previsto lo storico
        strSQL = "INSERT INTO ttlotcpro (codditt, instid, lp_codart, lp_fase, lp_magaz, lp_lotto, lp_commeca," & _
          " lp_ubicaz, lp_giaini, lp_vgiaini)" & _
          " SELECT TTSTLOCS.codditt, " & lIIttlotcpro & ", tt_codart, tt_fase, tt_magaz, " & _
          IIf(bopLsaldo = True, " ld_lotto", "0").ToString & ", " & _
          IIf(bopCsaldo = True, " ld_commeca", "0").ToString & ", " & _
          IIf(bopUsaldo = True, " ld_ubicaz", "' '").ToString & ", " & _
          " SUM(ld_quant), SUM(ld_valore) " & _
          " FROM TTSTLOCS INNER JOIN lotcdef ON TTSTLOCS.codditt = lotcdef.codditt " & _
          " AND TTSTLOCS.tt_codart = lotcdef.ld_codart AND TTSTLOCS.tt_magaz = lotcdef.ld_magaz " & _
          " AND TTSTLOCS.tt_fase = lotcdef.ld_fase" & _
          IIf(bopLsaldo = True, " AND TTStlocs.tt_lotto = lotcdef.ld_lotto", "").ToString & _
          IIf(bopCsaldo = True, " AND TTStlocs.tt_lotto = lotcdef.ld_commeca", "").ToString & _
          IIf(bopUsaldo = True, " AND TTStlocs.tt_ubicaz = lotcdef.ld_ubicaz", "").ToString & _
          " WHERE TTSTLOCS.codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTStlocs & _
          " GROUP BY TTSTLOCS.codditt, tt_codart, tt_fase, tt_magaz" & _
          IIf(bopLsaldo = True, ", ld_lotto", "").ToString & _
          IIf(bopCsaldo = True, ", ld_commeca", "").ToString & _
          IIf(bopUsaldo = True, ", ld_ubicaz", "").ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        '------------------------------------------------------------------------------------------------------------ 
        strSQL = "UPDATE TTSTLOCS SET tt_quant = tt_quant + lp_giaini," & _
          " tt_qtacar = tt_qtacar + lp_giaini," & _
          " tt_valcar = tt_valcar + lp_vgiaini" & _
          " FROM ttstlocs INNER JOIN ttlotcpro ON ttstlocs.codditt = ttlotcpro.codditt " & _
          " AND ttstlocs.tt_codart = ttlotcpro.lp_codart AND ttstlocs.tt_fase = ttlotcpro.lp_fase " & _
          " AND ttstlocs.tt_magaz = ttlotcpro.lp_magaz " & _
          " WHERE ttstlocs.codditt = " & CStrSQL(strDitta) & _
          " AND ttstlocs.instid = " & lIITTStlocs & _
          " AND ttlotcpro.instid = " & lIIttlotcpro & _
          IIf(bopLsaldo = True, " AND TTStlocs.tt_lotto = ttlotcpro.lp_lotto", "").ToString & _
          IIf(bopCsaldo = True, " AND TTStlocs.tt_lotto = ttlotcpro.lp_commeca", "").ToString & _
          IIf(bopUsaldo = True, " AND TTStlocs.tt_ubicaz = ttlotcpro.lp_ubicaz", "").ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        ResetTblInstId("TTLOTCPRO", False, lIIttlotcpro)
      End If    'If bckStorico = False Then
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiunge i carichi da Movmag/Keymag/Testmag/Artico
      '--------------------------------------------------------------------------------------------------------------
      If bopLsaldo = True Then strCampoTmp = "km_lotto"
      If bopCsaldo = True Then strCampoTmp = "km_commeca"
      If bopUsaldo = True Then strCampoTmp = "km_ubicaz"
      strSQL = "INSERT INTO ttlotcpro (codditt, instid, lp_codart, lp_fase, lp_magaz, lp_lotto, lp_commeca," & _
        " lp_ubicaz, lp_giaini, lp_vgiaini)" & _
        "SELECT " & CStrSQL(strDitta) & ", " & lIIttlotcpro & ", km_codart, km_fase, km_magaz,"
      If bopLsaldo = True Then strSQL += " km_lotto, 0, ' ',"
      If bopCsaldo = True Then strSQL += " 0, km_commeca, ' ',"
      If bopUsaldo = True Then strSQL += " 0, 0, km_ubicaz,"
      strSQL += " Sum(mm_quant) As Quant, Sum(mm_valore) As Valore" & _
        " FROM testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc" & _
        " INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga" & _
        " INNER JOIN artico ON artico.codditt = movmag.codditt AND artico.ar_codart = movmag.mm_codart" & _
        " INNER JOIN TTSTLOCS ON TTSTLOCS.codditt = keymag.codditt AND TTSTLOCS.tt_magaz = keymag.km_magaz AND TTSTLOCS.tt_fase = keymag.km_fase AND TTSTLOCS.tt_codart = keymag.km_codart" & _
        " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto"
      If bopLsaldo = True Then strSQL += " AND " & strTTStlocs & ".tt_lotto = keymag.km_lotto"
      If bopCsaldo = True Then strSQL += " AND " & strTTStlocs & ".tt_lotto = keymag.km_commeca"
      If bopUsaldo = True Then strSQL += " AND " & strTTStlocs & ".tt_ubicaz = keymag.km_ubicaz"
      strSQL += strSQL1 & _
        " AND km_carscar = 1" & _
        " AND instid = " & lIITTStlocs & _
        " GROUP BY km_codart, km_magaz, km_fase, " & strCampoTmp & _
        " HAVING Sum(mm_quant) <> 0 OR Sum(mm_valore) <> 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE TTSTLOCS SET tt_qtacar = tt_qtacar + lp_giaini," & _
        " tt_valcar = tt_valcar + lp_vgiaini" & _
        " FROM ttstlocs INNER JOIN ttlotcpro ON ttstlocs.codditt = ttlotcpro.codditt" & _
        " AND ttstlocs.tt_codart = ttlotcpro.lp_codart AND ttstlocs.tt_fase = ttlotcpro.lp_fase" & _
        " AND ttstlocs.tt_magaz = ttlotcpro.lp_magaz" & _
        " WHERE ttstlocs.codditt = " & CStrSQL(strDitta) & _
        " AND ttstlocs.instid = " & lIITTStlocs & _
        " AND ttlotcpro.instid = " & lIIttlotcpro
      If bopLsaldo = True Then strSQL += " AND TTStlocs.tt_lotto = ttlotcpro.lp_lotto"
      If bopCsaldo = True Then strSQL += " AND TTStlocs.tt_lotto = ttlotcpro.lp_commeca"
      If bopUsaldo = True Then strSQL += " AND TTStlocs.tt_ubicaz = ttlotcpro.lp_ubicaz"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      ResetTblInstId("TTLOTCPRO", False, lIIttlotcpro)
      '--------------------------------------------------------------------------------------------------------------
      '--- INSERT INTO dei dati che esistono in LOTCDEF ma non in TTSTLOCS
      '--------------------------------------------------------------------------------------------------------------
      If bckStorico = False Then ' caso normale, non tratto lo storico
        strSQL = " SELECT ld_codart, ld_magaz, ld_fase," & _
          IIf(bopLsaldo = True, " ld_lotto,", "").ToString & _
          IIf(bopCsaldo = True, " ld_commeca,", "").ToString & _
          IIf(bopUsaldo = True, " ld_ubicaz,", "").ToString & _
          " Sum(ld_quant) As Quant, Sum(ld_valore) As Valore" & _
          " FROM lotcdef INNER JOIN artico ON lotcdef.codditt = artico.codditt AND lotcdef.ld_codart = artico.ar_codart"
        If bLottoNew = True Then
          strSQL += " INNER JOIN analotti ON lotcdef.codditt = analotti.codditt AND lotcdef.ld_codart = analotti.alo_codart AND lotcdef.ld_lotto = analotti.alo_lotto"
        End If
        strSQL += " WHERE lotcdef.codditt = " & CStrSQL(strDitta) & _
          IIf(bopLsaldo = True, " AND ld_lotto <> 0", "").ToString & _
          IIf(bopCsaldo = True, " AND ld_commeca <> 0", "").ToString & _
          IIf(bopUsaldo = True, " AND ld_ubicaz <> ' '", "").ToString & _
          IIf(NTSCInt(stredGruppo) <> 0, " AND artico.ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
          IIf(NTSCInt(stredSottogr) <> 0, " AND artico.ar_sotgru = " & NTSCInt(stredSottogr), "").ToString & _
          IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString & _
          IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
          IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
          IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
          IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
          IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
        If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
          strSQL += " AND ld_magaz BETWEEN " & NTSCInt(stredDamagaz) & " AND " & NTSCInt(stredAmagaz)
        End If
        If (NTSCInt(stredCodmarcini) <> 0) Or (NTSCInt(stredCodmarcfin) <> 999) Then
          strSQL += " AND artico.ar_codmarc BETWEEN " & NTSCInt(stredCodmarcini) & " AND " & NTSCInt(stredCodmarcfin)
        End If
        If bLottoNew = False Then
          If (NTSCInt(stredInizio) <> 0) Or (NTSCInt(stredFine) <> 999999999) Then
            strSQL += " AND ld_lotto BETWEEN " & NTSCInt(stredInizio) & " AND " & NTSCInt(stredFine)
          End If
        Else
          If strLottoxDa.Replace("0", "").Trim <> "" Or strLottoxA.Replace("999999999", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz").Trim <> "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz" Then
            strSQL += " AND analotti.alo_lottox BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
          End If
        End If
        Select Case strCodart
          Case "A"
            If (stredDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or _
               (stredAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
              strSQL += " AND ld_codart BETWEEN " & CStrSQL(stredDacodart) & " AND " & CStrSQL(stredAcodart)
            End If
            If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
              strSQL += " AND ld_fase BETWEEN " & NTSCInt(stredFaseini) & " AND " & NTSCInt(stredFasefin)
            End If
          Case "B"
            If nCodlsar <> 0 Then
              strSQL1 += _
                " AND CAST(ld_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(ld_fase AS VARCHAR(4))" & _
                " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lsa_codlsar = " & nCodlsar & ")"
            End If
        End Select
        strSQL += " GROUP BY ld_codart, ld_fase, ld_magaz," & _
          IIf(bopLsaldo = True, " ld_lotto", "").ToString & _
          IIf(bopCsaldo = True, " ld_commeca", "").ToString & _
          IIf(bopUsaldo = True, " ld_ubicaz", "").ToString
        dttKeymag = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        For i = 0 To (dttKeymag.Rows.Count - 1)
          strSQL = "SELECT instid FROM TTSTLOCS" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStlocs & _
            " AND tt_codart = " & CStrSQL(dttKeymag.Rows(i)!ld_codart) & _
            " AND tt_magaz = " & NTSCInt(dttKeymag.Rows(i)!ld_magaz) & _
            " AND tt_fase = " & NTSCInt(dttKeymag.Rows(i)!ld_fase)
          If bopLsaldo = True Then strSQL += " AND tt_lotto = " & NTSCInt(dttKeymag.Rows(i)!ld_lotto)
          If bopCsaldo = True Then strSQL += " AND tt_lotto = " & NTSCInt(dttKeymag.Rows(i)!ld_commeca)
          If bopUsaldo = True Then strSQL += " AND tt_ubicaz = " & CStrSQL(dttKeymag.Rows(i)!ld_ubicaz)
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count = 0 Then
            With dttKeymag.Rows(i)
              strSQL = "INSERT INTO TTSTLOCS (" & strCampiTTSTLOCS & ")" & _
                " VALUES (" & CStrSQL(strDitta) & ", " & lIITTStlocs & ", " & CStrSQL(!ld_codart) & ", " & _
                NTSCInt(!ld_magaz) & ", "
              If bopLsaldo = True Then strSQL += NTSCInt(!ld_lotto) & ", ' ', "
              If bopCsaldo = True Then strSQL += NTSCInt(!ld_commeca) & ", ' ', "
              If bopUsaldo = True Then strSQL += "0, " & CStrSQL(!ld_ubicaz) & ", "
              strSQL += CDblSQL(NTSCDec(!Quant)) & ", " & CDblSQL(NTSCDec(!Valore)) & ", " & _
                CDblSQL(NTSCDec(!Quant)) & ", " & CDblSQL(NTSCDec(!Valore)) & ", " & NTSCInt(!ld_fase) & ")"
            End With
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          End If
          dttTmp.Clear()
          dttTmp.Dispose()
        Next
        dttKeymag.Clear()
        dttKeymag.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Determina il valore dividendo il totale del valore dei carichi per il totale dei carichi
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE TTSTLOCS SET tt_valore = (tt_valcar / tt_qtacar)" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStlocs & _
        " AND tt_qtacar <> 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "UPDATE TTSTLOCS SET tt_valore = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStlocs & _
        " AND tt_qtacar = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Cancella i lotti/commesse che sono a zero
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM TTSTLOCS" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStlocs & _
        " AND tt_quant = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla l'esistenza di record nella tabella temporanea TTSTLOCS
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tt_codart FROM TTSTLOCS" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStlocs & _
        " ORDER BY tt_codart, tt_fase, tt_magaz, tt_lotto"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        strError = "Non esistono dati con queste caratteristiche."
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      ResetTblInstId("TTLOTCPRO", False, lIIttlotcpro)
      dttTmp.Clear() : dttTmp.Dispose()
      dttKeymag.Clear() : dttKeymag.Dispose()
    End Try
  End Function

  Public Overridable Function SaldoStorico(ByVal strDitta As String, ByVal lIITTStschea As Integer, _
                                             ByVal strTTStschea As String, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal strDtulap As String, ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByVal bckSaldiIniziali As Boolean, _
                                             ByRef strError As String, ByVal strQuery As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return SaldoStorico(strDitta, lIITTStschea, strTTStschea, stredDatini, stredDatfin, stredDamagaz, _
                          stredAmagaz, stredDacodart, stredAcodart, stredDaconto, _
                          stredAconto, stredFaseini, stredFasefin, bckStorico, _
                          bopLapchiu, bopLaperti, stredInizio, stredFine, _
                          bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                          bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                          bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                          stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                          stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                          stredAnnotco, stredCodstag, strDtulap, bModuloCRM, _
                          bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                          bAmm, bckSaldiIniziali, strError, strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function SaldoStorico(ByVal strDitta As String, ByVal lIITTStschea As Integer, _
                                           ByVal strTTStschea As String, ByVal stredDatini As String, _
                                           ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                           ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                           ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                           ByVal stredAconto As String, ByVal stredFaseini As String, _
                                           ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                           ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                           ByVal stredInizio As String, ByVal stredFine As String, _
                                           ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                           ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                           ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                           ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                           ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                           ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                           ByVal stredCausale As String, ByVal stredGruppo As String, _
                                           ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                           ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                           ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                           ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                           ByVal strDtulap As String, ByVal bModuloCRM As Boolean, _
                                           ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                           ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                           ByVal bAmm As Boolean, ByVal bckSaldiIniziali As Boolean, _
                                           ByRef strError As String, ByVal strQuery As String, _
                                           ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                           ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                           ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITTStschea, strTTStschea, stredDatini, stredDatfin, stredDamagaz, _
                                             stredAmagaz, stredDacodart, stredAcodart, stredDaconto, _
                                             stredAconto, stredFaseini, stredFasefin, bckStorico, _
                                             bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                             bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                             bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                             bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                                             stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                                             stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                             stredAnnotco, stredCodstag, strDtulap, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                             bAmm, bckSaldiIniziali, strError, strQuery, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(48))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return SaldoStorico(strDitta, lIITTStschea, strTTStschea, stredDatini, stredDatfin, stredDamagaz, stredAmagaz, _
                          stredDacodart, stredAcodart, stredDaconto, stredAconto, stredFaseini, stredFasefin, _
                          bckStorico, bopLapchiu, bopLaperti, stredInizio, stredFine, bopCapchiu, bopCaperte, _
                          stredDacomme, stredAcomme, bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                          bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, stredCausale, stredGruppo, _
                          stredSottogr, stredCodmarcini, stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                          stredAnnotco, stredCodstag, strDtulap, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                          strRegvis, bAmm, bckSaldiIniziali, strError, strQuery, _
                          strClassificazioneLivello1, strClassificazioneLivello2, strClassificazioneLivello3, _
                          strClassificazioneLivello4, strClassificazioneLivello5, "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function SaldoStorico(ByVal strDitta As String, ByVal lIITTStschea As Integer, _
                                           ByVal strTTStschea As String, ByVal stredDatini As String, _
                                           ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                           ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                           ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                           ByVal stredAconto As String, ByVal stredFaseini As String, _
                                           ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                           ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                           ByVal stredInizio As String, ByVal stredFine As String, _
                                           ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                           ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                           ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                           ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                           ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                           ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                           ByVal stredCausale As String, ByVal stredGruppo As String, _
                                           ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                           ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                           ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                           ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                           ByVal strDtulap As String, ByVal bModuloCRM As Boolean, _
                                           ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                           ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                           ByVal bAmm As Boolean, ByVal bckSaldiIniziali As Boolean, _
                                           ByRef strError As String, ByVal strQuery As String, _
                                           ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                           ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                           ByVal strClassificazioneLivello5 As String, _
                                           ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                           ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    '   CLN__STD.CheckInvokeCustomFunction(
    ' riempie ttstschea
    Dim i As Integer
    Dim lGiorni As Integer
    Dim dSaldo1 As Decimal
    Dim strSQL As String
    Dim strTipork As String = ""
    Dim dEs As Decimal
    Dim strData As String
    Dim strData1 As String = ""
    Dim strData2 As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim dsArtdef As DataSet = Nothing
    Dim dsTmp1 As DataSet = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITTStschea, strTTStschea, stredDatini, stredDatfin, stredDamagaz, _
                                             stredAmagaz, stredDacodart, stredAcodart, stredDaconto, _
                                             stredAconto, stredFaseini, stredFasefin, bckStorico, _
                                             bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                             bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                             bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                             bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                                             stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                                             stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                             stredAnnotco, stredCodstag, strDtulap, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                             bAmm, bckSaldiIniziali, strError, strQuery, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, _
                                             strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(48))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strData = NTSCStr(DateAdd("d", 1, NTSCDate(strDtulap)))
      lGiorni = NTSCInt(DateDiff("d", NTSCDate(strData), NTSCDate(stredDatini)))
      If lGiorni < 0 Then
        strData1 = stredDatini
        strData2 = strDtulap
      Else
        If lGiorni > 0 Then
          strData1 = strData
          strData2 = NTSCStr(DateAdd("d", -1, NTSCDate(stredDatini)))
        End If
      End If
      If bopPrelevaSolo Then strTipork = UCase(NTSCStr(strcbTipodoc))
      '--------------------------------------------------------------------------------------------------------------
      '--- SOLO MOVIMENTATI
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT keymag.km_codart, keymag.km_fase, keymag.km_magaz" & _
        " FROM testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc" & _
        " INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga" & _
        " INNER JOIN artico ON artico.codditt = movmag.codditt AND artico.ar_codart = movmag.mm_codart" & _
        " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
        " AND keymag.km_aammgg BETWEEN " & CDataSQL(stredDatini) & " AND " & CDataSQL(stredDatfin) & _
        " AND artico.ar_stasche = 'S'" & _
        IIf(bopPrelevaSolo = True, " AND keymag.km_tipork = " & CStrSQL(strTipork), "").ToString & _
        IIf(NTSCInt(stredCausale) > 0, " AND keymag.km_causale = " & NTSCInt(stredCausale), "").ToString & _
        IIf(NTSCInt(stredGruppo) > 0, " AND artico.ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
        IIf(NTSCInt(stredSottogr) > 0, " AND artico.ar_sotgru = " & NTSCInt(stredSottogr), "").ToString & _
        IIf(Trim(stredCodcfam) <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString & _
        IIf((bckSerie = True) And (stredSerie.Length) > 0, " AND keymag.km_serie = " & CStrSQL(stredSerie), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
      If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
        strSQL = strSQL & " AND keymag.km_magaz BETWEEN " & NTSCInt(stredDamagaz) & " AND " & NTSCInt(stredAmagaz)
      End If
      If (NTSCInt(stredCodmarcini) <> 0) And (NTSCInt(stredCodmarcfin) <> 999) Then
        strSQL = strSQL & " AND artico.ar_codmarc BETWEEN " & NTSCInt(stredCodmarcini) & " AND " & NTSCInt(stredCodmarcfin)
      End If
      Select Case strCodart
        Case "A"
          If (stredDacodart <> "".PadLeft(CLN__STD.CodartMaxLen, " "c)) Or (stredAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strSQL += " AND keymag.km_codart BETWEEN " & CStrSQL(stredDacodart) & " AND " & CStrSQL(stredAcodart)
          End If
          If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
            strSQL += " AND keymag.km_fase BETWEEN " & NTSCInt(stredFaseini) & " AND " & NTSCInt(stredFasefin)
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL += _
              " AND CAST(km_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(km_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      Select Case strConto
        Case "A"
          If (NTSCInt(stredDaconto) <> 0) Or (NTSCInt(stredAconto) <> 999999999) Then
            strSQL += " AND keymag.km_conto BETWEEN " & NTSCInt(stredDaconto) & " AND " & NTSCInt(stredAconto)
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND km_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      If bModTCO = True Then
        If bckSelAnnoStag = True Then
          If NTSCInt(stredAnnotco) <> 0 Then strSQL += " AND artico.ar_anno = " & NTSCInt(stredAnnotco)
          If NTSCInt(stredCodstag) <> 0 Then strSQL += " AND artico.ar_codstag = " & NTSCInt(stredCodstag)
        End If
      End If
      TraduciWhere(strQuery, strSQL)
      strSQL += " GROUP BY keymag.km_codart, keymag.km_fase, keymag.km_magaz" & _
        " ORDER BY keymag.km_codart, keymag.km_fase, keymag.km_magaz"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
      If dsTmp.Tables("TEMP").Rows.Count = 0 Then
        strError = "Non esistono dati con queste caratteristiche."
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To dsTmp.Tables("TEMP").Rows.Count - 1
        '------------------------------------------------------------------------------------------------------------
        '--- Se il CheckBox "Calcola saldi iniziali" è selezionato (default)
        '--- lavora come al solito, altrimenti non fa il seguente calcolo
        '------------------------------------------------------------------------------------------------------------
        If bckSaldiIniziali = True Then
          '----------------------------------------------------------------------------------------------------------
          '--- Controlla se deve incrementare nRang e se si inizializza i campi
          '--- nRanges = nRang (numero totale di ranges)
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT ad_esist FROM ARTDEF" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ad_codart = " & CStrSQL(dsTmp.Tables("TEMP").Rows(i)!km_codart) & _
            " AND ad_magaz = " & NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_magaz) & _
            " AND ad_fase = " & NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_fase) & _
            " ORDER BY ad_codart, ad_fase, ad_magaz"
          dsArtdef = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
          If dsArtdef.Tables("TEMP").Rows.Count > 0 Then
            dEs = ArrDbl(NTSCDec(dsArtdef.Tables("TEMP").Rows(0)!ad_esist), 3)
          Else
            dEs = 0
          End If
          '----------------------------------------------------------------------------------------------------------
          '--- Calcola l'eventuale differenza e la soma o toglie,
          '--- se la data partenza e data ultimo aggiornamento + 1 non è la stessa
          '----------------------------------------------------------------------------------------------------------
          If lGiorni <> 0 Then
            strSQL = "SELECT Sum(keymag.km_carscar * movmag.mm_quant) As Saldop" & _
              " FROM ARTICO INNER JOIN (testmag INNER JOIN (movmag INNER JOIN keymag ON (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) AND (movmag.mm_numdoc = keymag.km_numdoc) AND (movmag.mm_serie = keymag.km_serie) AND (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_tipork = keymag.km_tipork)) ON (testmag.codditt = movmag.codditt) AND (testmag.tm_numdoc = movmag.mm_numdoc) AND (testmag.tm_serie = movmag.mm_serie) AND (testmag.tm_anno = movmag.mm_anno) AND (testmag.tm_tipork = movmag.mm_tipork)) ON artico.codditt = movmag.codditt AND artico.ar_codart = movmag.mm_codart" & _
              " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
              " AND keymag.km_aammgg BETWEEN " & CDataSQL(strData1) & " AND " & CDataSQL(strData2) & _
              " AND keymag.km_magaz = " & NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_magaz) & _
              " AND keymag.km_fase = " & NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_fase) & _
              " AND keymag.km_codart = " & CStrSQL(dsTmp.Tables("TEMP").Rows(i)!km_codart)
            dsTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
            If dsTmp1.Tables("TEMP").Rows.Count > 0 Then
              If Not (NTSCStr(dsTmp1.Tables("TEMP").Rows(0)!saldop) = "") Then
                dSaldo1 = ArrDbl(NTSCDec(dsTmp1.Tables("TEMP").Rows(0)!Saldop), 3)
              Else
                dSaldo1 = 0
              End If
            Else
              dSaldo1 = 0
            End If
            If lGiorni < 0 Then
              dEs = ArrDbl(dEs - dSaldo1, 3)
            Else
              dEs = ArrDbl(dEs + dSaldo1, 3)
            End If
          End If
        Else
          dEs = 0
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Registra sul temporaneo
        '------------------------------------------------------------------------------------------------------------
        strSQL = "INSERT INTO " & strTTStschea & " (codditt, instid, tt_codart, tt_codmaga, tt_esistp," & _
          " tt_fase, tt_commeca)" & _
          " VALUES (" & CStrSQL(strDitta) & ", " & lIITTStschea & ", " & CStrSQL(dsTmp.Tables("TEMP").Rows(i)!km_codart) & ", " & _
          NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_magaz) & ", " & CDblSQL(dEs) & " , " & NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_fase) & ", 0)"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function SaldoStoricoNonMov(ByVal strDitta As String, ByVal lIITTStschea As Integer, _
                                             ByVal strTTStschea As String, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal strDtulap As String, ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByVal bckSaldiIniziali As Boolean, _
                                             ByVal bopEsist As Boolean, ByRef strError As String, _
                                             ByVal strQuery As String) As Boolean
    
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return SaldoStoricoNonMov(strDitta, lIITTStschea, strTTStschea, stredDatini, _
                                stredDatfin, stredDamagaz, stredAmagaz, stredDacodart, _
                                stredAcodart, stredDaconto, stredAconto, stredFaseini, _
                                stredFasefin, bckStorico, bopLapchiu, bopLaperti, _
                                stredInizio, stredFine, bopCapchiu, bopCaperte, _
                                stredDacomme, stredAcomme, bopUapchiu, bopUaperte, _
                                stredUbicazini, stredUbicazfin, _
                                bopPrelevaSolo, strcbTipodoc, _
                                bckSerie, stredSerie, _
                                stredCausale, stredGruppo, _
                                stredSottogr, stredCodmarcini, _
                                stredCodmarcfin, stredCodcfam, _
                                bModTCO, bckSelAnnoStag, _
                                stredAnnotco, stredCodstag, _
                                strDtulap, bModuloCRM, _
                                bIsCRMUser, strAccvis, _
                                lCodorgaOperat, strRegvis, _
                                bAmm, bckSaldiIniziali, _
                                bopEsist, strError, _
                                strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function SaldoStoricoNonMov(ByVal strDitta As String, ByVal lIITTStschea As Integer, _
                                                 ByVal strTTStschea As String, ByVal stredDatini As String, _
                                                 ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                                 ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                                 ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                                 ByVal stredAconto As String, ByVal stredFaseini As String, _
                                                 ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                                 ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                                 ByVal stredInizio As String, ByVal stredFine As String, _
                                                 ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                                 ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                                 ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                                 ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                                 ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                                 ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                                 ByVal stredCausale As String, ByVal stredGruppo As String, _
                                                 ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                                 ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                                 ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                                 ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                 ByVal strDtulap As String, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal bckSaldiIniziali As Boolean, _
                                                 ByVal bopEsist As Boolean, ByRef strError As String, _
                                                 ByVal strQuery As String, _
                                                 ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITTStschea, strTTStschea, stredDatini, _
                                             stredDatfin, stredDamagaz, stredAmagaz, stredDacodart, _
                                             stredAcodart, stredDaconto, stredAconto, stredFaseini, _
                                             stredFasefin, bckStorico, bopLapchiu, bopLaperti, _
                                             stredInizio, stredFine, bopCapchiu, bopCaperte, _
                                             stredDacomme, stredAcomme, bopUapchiu, bopUaperte, _
                                             stredUbicazini, stredUbicazfin, _
                                             bopPrelevaSolo, strcbTipodoc, _
                                             bckSerie, stredSerie, _
                                             stredCausale, stredGruppo, _
                                             stredSottogr, stredCodmarcini, _
                                             stredCodmarcfin, stredCodcfam, _
                                             bModTCO, bckSelAnnoStag, _
                                             stredAnnotco, stredCodstag, _
                                             strDtulap, bModuloCRM, _
                                             bIsCRMUser, strAccvis, _
                                             lCodorgaOperat, strRegvis, _
                                             bAmm, bckSaldiIniziali, _
                                             bopEsist, strError, _
                                             strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(49))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return SaldoStoricoNonMov(strDitta, lIITTStschea, strTTStschea, stredDatini, stredDatfin, _
                                stredDamagaz, stredAmagaz, stredDacodart, stredAcodart, stredDaconto, stredAconto, _
                                stredFaseini, stredFasefin, bckStorico, bopLapchiu, bopLaperti, _
                                stredInizio, stredFine, bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, _
                                bckSerie, stredSerie, stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                                stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                strDtulap, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, _
                                bckSaldiIniziali, bopEsist, strError, strQuery, _
                                strClassificazioneLivello1, strClassificazioneLivello2, _
                                strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function SaldoStoricoNonMov(ByVal strDitta As String, ByVal lIITTStschea As Integer, _
                                                 ByVal strTTStschea As String, ByVal stredDatini As String, _
                                                 ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                                 ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                                 ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                                 ByVal stredAconto As String, ByVal stredFaseini As String, _
                                                 ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                                 ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                                 ByVal stredInizio As String, ByVal stredFine As String, _
                                                 ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                                 ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                                 ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                                 ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                                 ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                                 ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                                 ByVal stredCausale As String, ByVal stredGruppo As String, _
                                                 ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                                 ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                                 ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                                 ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                 ByVal strDtulap As String, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal bckSaldiIniziali As Boolean, _
                                                 ByVal bopEsist As Boolean, ByRef strError As String, _
                                                 ByVal strQuery As String, _
                                                 ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String, _
                                                 ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                                 ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    ' riempie ttstschea
    Dim i As Integer
    Dim lGiorni As Integer
    Dim j As Integer
    Dim dSaldo1 As Decimal
    Dim strSQL As String
    Dim strTipork As String = ""
    Dim dEs As Decimal
    Dim strData As String
    Dim strData1 As String = ""
    Dim strData2 As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim dsMaga As DataSet = Nothing
    Dim dsArtdef As DataSet = Nothing
    Dim dsTmp1 As DataSet = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITTStschea, strTTStschea, stredDatini, stredDatfin, _
                                             stredDamagaz, stredAmagaz, stredDacodart, stredAcodart, _
                                             stredDaconto, stredAconto, stredFaseini, stredFasefin, bckStorico, _
                                             bopLapchiu, bopLaperti, stredInizio, stredFine, bopCapchiu, bopCaperte, _
                                             stredDacomme, stredAcomme, bopUapchiu, bopUaperte, _
                                             stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, _
                                             bckSerie, stredSerie, stredCausale, stredGruppo, stredSottogr, _
                                             stredCodmarcini, stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                             stredAnnotco, stredCodstag, strDtulap, bModuloCRM, bIsCRMUser, strAccvis, _
                                             lCodorgaOperat, strRegvis, bAmm, bckSaldiIniziali, bopEsist, strError, _
                                             strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(49))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      SaldoStoricoNonMov = False
      '--------------------------------------------------------------------------------------------------------------
      strData = NTSCStr(DateAdd("d", 1, NTSCDate(strDtulap)))
      lGiorni = NTSCInt(DateDiff("d", NTSCDate(strData), NTSCDate(stredDatini)))
      If lGiorni < 0 Then
        strData1 = stredDatini
        strData2 = strDtulap
      Else
        If lGiorni > 0 Then
          strData1 = strData
          strData2 = NTSCStr(DateAdd("d", -1, NTSCDate(stredDatini)))
        End If
      End If
      If bopPrelevaSolo Then strTipork = UCase(NTSCStr(strcbTipodoc))
      '--------------------------------------------------------------------------------------------------------------
      '--- TUTTI GLI ATICOLI
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artico.ar_codart AS km_codart, artfasi.af_fase AS km_fase" & _
        " FROM artico LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND artico.ar_stasche = 'S'" & _
        IIf(NTSCInt(stredGruppo) > 0, " AND artico.ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
        IIf(NTSCInt(stredSottogr) > 0, " AND artico.ar_sotgru = " & NTSCInt(stredSottogr), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
      If (NTSCInt(stredCodmarcini) <> 0) And (NTSCInt(stredCodmarcfin) <> 999) Then
        strSQL = strSQL & " AND artico.ar_codmarc BETWEEN " & NTSCInt(stredCodmarcini) & " AND " & NTSCInt(stredCodmarcfin)
      End If
      Select Case strCodart
        Case "A"
          If (stredDacodart <> "".PadLeft(CLN__STD.CodartMaxLen, " "c)) Or (stredAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strSQL += " AND artico.ar_codart BETWEEN " & CStrSQL(stredDacodart) & " AND " & CStrSQL(stredAcodart)
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL += " AND IN (SELECT lsa_codart FROM listsar" & _
                              " WHERE codditt = " & CStrSQL(strDitta) & _
                              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      strSQL += " ORDER BY ar_codart, af_fase"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
      If dsTmp.Tables("TEMP").Rows.Count = 0 Then
        strError = "Non esistono dati con queste caratteristiche."
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To dsTmp.Tables("TEMP").Rows.Count - 1
        '------------------------------------------------------------------------------------------------------------
        '--- Per ogni magazzino selezionato
        '------------------------------------------------------------------------------------------------------------
        strSQL = "SELECT tb_codmaga FROM tabmaga" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tabmaga.tb_codmaga BETWEEN " & NTSCInt(stredDamagaz) & " AND " & NTSCInt(stredAmagaz)
        dsMaga = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
        For j = 0 To dsMaga.Tables("TEMP").Rows.Count - 1
          '----------------------------------------------------------------------------------------------------------
          '--- Se il CheckBox "Calcola saldi iniziali" è selezionato (default)
          '--- lavora come al solito, altrimenti non fa il seguente calcolo
          '----------------------------------------------------------------------------------------------------------
          If bckSaldiIniziali = True Then
            '--------------------------------------------------------------------------------------------------------
            '--- Controlla se deve incrementare nRang e se si inizializza i campi
            '--- nRanges = nRang (numero totale di ranges)
            '--------------------------------------------------------------------------------------------------------
            strSQL = "SELECT ad_esist FROM artdef" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND ad_codart = " & CStrSQL(dsTmp.Tables("TEMP").Rows(i)!km_codart) & _
              " AND ad_magaz = " & NTSCStr(dsMaga.Tables("TEMP").Rows(j)!tb_codmaga) & _
              " AND ad_fase = " & NTSCStr(IIf(NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_fase) = "", 0, NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_fase))) & _
              " ORDER BY ad_codart, ad_fase, ad_magaz"
            dsArtdef = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
            If dsArtdef.Tables("TEMP").Rows.Count > 0 Then
              dEs = ArrDbl(NTSCDec(dsArtdef.Tables("TEMP").Rows(0)!ad_esist), 3)
            Else
              dEs = 0
            End If
            '--------------------------------------------------------------------------------------------------------
            '--- Calcola l'eventuale differenza e la soma o toglie,
            '--- se la data partenza e data ultimo aggiornamento + 1 non è la stessa
            '--------------------------------------------------------------------------------------------------------
            If lGiorni <> 0 Then
              strSQL = "SELECT Sum(keymag.km_carscar * movmag.mm_quant) As Saldop" & _
                " FROM ((movmag INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga)" & _
                " INNER JOIN testmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_numdoc = movmag.mm_numdoc)" & _
                " INNER JOIN artico ON artico.codditt = movmag.codditt AND artico.ar_codart = movmag.mm_codart" & _
                " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
                " AND keymag.km_aammgg BETWEEN " & CDataSQL(strData1) & " AND " & CDataSQL(strData2) & _
                " AND keymag.km_magaz = " & NTSCStr(dsMaga.Tables("TEMP").Rows(j)!tb_codmaga) & _
                " AND keymag.km_fase = " & NTSCStr(IIf(NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_fase) = "", 0, NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_fase))) & _
                " AND keymag.km_codart = " & CStrSQL(dsTmp.Tables("TEMP").Rows(i)!km_codart)
              dsTmp1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
              If dsTmp1.Tables("TEMP").Rows.Count > 0 Then
                If Not (NTSCStr(dsTmp1.Tables("TEMP").Rows(0)!saldop) = "") Then
                  dSaldo1 = ArrDbl(NTSCDec(dsTmp1.Tables("TEMP").Rows(0)!Saldop), 3)
                Else
                  dSaldo1 = 0
                End If
              Else
                dSaldo1 = 0
              End If
              If lGiorni < 0 Then
                dEs = ArrDbl(dEs - dSaldo1, 3)
              Else
                dEs = ArrDbl(dEs + dSaldo1, 3)
              End If
            End If
          Else
            dEs = 0
          End If
          '----------------------------------------------------------------------------------------------------------
          '--- Registra sul temporaneo
          '----------------------------------------------------------------------------------------------------------
          strSQL = "INSERT INTO " & strTTStschea & " (codditt, instid, tt_codart, tt_codmaga, tt_esistp," & _
            " tt_fase, tt_commeca)" & _
            " VALUES (" & CStrSQL(strDitta) & ", " & lIITTStschea & ", " & _
            CStrSQL(dsTmp.Tables("TEMP").Rows(i)!km_codart) & ", " & NTSCStr(dsMaga.Tables("TEMP").Rows(j)!tb_codmaga) & ", " & _
            CDblSQL(dEs) & " , " & NTSCStr(IIf(NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_fase) = "", 0, NTSCStr(dsTmp.Tables("TEMP").Rows(i)!km_fase))) & ", 0)"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        Next
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Ora nel caso opEsist deve eliminare tuti gli articoli che hanno giacenza pari
      '--- a zero (in TTSTSCHEA) e NON hanno movimenti in KEYMAG per range selezionati
      '--------------------------------------------------------------------------------------------------------------
      If bopEsist Then
        strSQL = "DELETE FROM TTSTSCHEA" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTStschea & _
          " AND tt_esistp = 0 AND NOT EXISTS (SELECT km_conto FROM keymag" & _
                                            " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
                                            " AND km_magaz = tt_codmaga" & _
                                            " AND km_fase = tt_fase" & _
                                            " AND km_codart = tt_codart" & _
                                            " AND keymag.km_aammgg BETWEEN " & CDataSQL(stredDatini) & " AND " & CDataSQL(stredDatfin)
        If (NTSCInt(stredDaconto) <> 0) Or (NTSCInt(stredAconto) <> 999999999) Then
          strSQL = strSQL & " AND keymag.km_conto BETWEEN " & NTSCInt(stredDaconto) & " AND " & NTSCInt(stredAconto)
        End If
        If bopPrelevaSolo = True Then
          strSQL = strSQL & " AND keymag.km_tipork = " & CStrSQL(strTipork)
        End If
        If bckSerie = True Then
          If Len(stredSerie) > 0 Then strSQL = strSQL & " AND keymag.km_serie = " & CStrSQL(stredSerie)
        End If
        If NTSCInt(stredCausale) > 0 Then strSQL = strSQL & " AND keymag.km_causale = " & NTSCInt(stredCausale)
        strSQL = strSQL & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function GetArtdef(ByVal strDitta As String, ByVal strCodart As String, ByVal strMagaz As String, _
                                      ByVal strFase As String, ByVal nOrdin As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ad_esist FROM ARTDEF" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ad_codart = '" & strCodart & "'" & _
        " AND ad_magaz = " & strMagaz & _
        " AND ad_fase = " & strFase

      If nOrdin = 1 Then
        strSQL = strSQL & " ORDER BY ad_codart, ad_fase, ad_magaz"
      ElseIf nOrdin = 2 Then
        strSQL = strSQL & " ORDER BY ad_codart, ad_magaz"
      End If

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTDEF")

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function SaldoStorico(ByVal strDitta As String, ByVal strCodart As String, ByVal strMagaz As String, _
                                    ByVal strFase As String, ByVal strDatini As String, ByVal strData As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim strInnerJoin As String
    Try
      strInnerJoin = "(((((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_numdoc = movmag.mm_numdoc)" & _
        " INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga)" & _
        " INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart)" & _
        " INNER JOIN anagra ON keymag.codditt = anagra.codditt AND keymag.km_conto = anagra.an_conto)" & _
        " LEFT JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum)" & _
        " LEFT JOIN tabport ON testmag.codditt = tabport.codditt AND testmag.tm_porto = tabport.tb_codport"


      'ATTENZIONE: di strDataIn e strData viene fatta la CDataSQL nel BE!!!!!!!!
      strSQL = "SELECT Sum(km_carscar * mm_quant) As Saldop FROM " & strInnerJoin & _
        " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
        " AND km_aammgg BETWEEN " & strDatini & " AND " & strData & _
        " AND km_magaz = " & strMagaz & _
        " AND km_fase = " & strFase & _
        " AND km_codart = " & CStrSQL(strCodart)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVMAG")

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CalcolaRettifiche(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                              ByVal strScarDatini As String, ByVal strScarDatfin As String, _
                                              ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                              ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                              ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                              ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                              ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                              ByVal nScarSottogr As Integer, ByVal nScarCodmarcini As Integer, _
                                              ByVal nScarCodmarcfin As Integer, ByVal dtrTmp As DataRow, _
                                              ByRef ds1 As DataSet, ByRef ds2 As DataSet) As Boolean
    Try
      'obsoleta
      Return CalcolaRettifiche(strDitta, strScarOrdin, strScarDatini, strScarDatfin, lScarDalotto, lScarAlotto, _
                               lScarDacomme, lScarAcomme, lScarDaconto, lScarAconto, strScarTipodoc, strScarSerie, _
                               nScarCausale, nScarGruppo, nScarSottogr, nScarCodmarcini, nScarCodmarcfin, dtrTmp, _
                               ds1, ds2, "", "".PadLeft(50, "Z"c), "", "", "", "", "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CalcolaRettifiche(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                                ByVal strScarDatini As String, ByVal strScarDatfin As String, _
                                                ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                                ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                                ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                                ByVal nScarSottogr As Integer, ByVal nScarCodmarcini As Integer, _
                                                ByVal nScarCodmarcfin As Integer, ByVal dtrTmp As DataRow, _
                                                ByRef ds1 As DataSet, ByRef ds2 As DataSet, _
                                                ByVal strLottoxDa As String, ByVal strLottoxA As String) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strScarOrdin, strScarDatini, strScarDatfin, _
                                             lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                                             lScarDaconto, lScarAconto, strScarTipodoc, strScarSerie, _
                                             nScarCausale, nScarGruppo, nScarSottogr, _
                                             nScarCodmarcini, nScarCodmarcfin, dtrTmp, ds1, ds2, _
                                             strLottoxDa, strLottoxA})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds1 = CType(oIn(18), DataSet)
        ds2 = CType(oIn(19), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return CalcolaRettifiche(strDitta, strScarOrdin, strScarDatini, strScarDatfin, lScarDalotto, lScarAlotto, _
                                lScarDacomme, lScarAcomme, lScarDaconto, lScarAconto, strScarTipodoc, strScarSerie, _
                                nScarCausale, nScarGruppo, nScarSottogr, nScarCodmarcini, nScarCodmarcfin, dtrTmp, _
                                ds1, ds2, strLottoxDa, strLottoxA, "", "", "", "", "")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function CalcolaRettifiche(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                                ByVal strScarDatini As String, ByVal strScarDatfin As String, _
                                                ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                                ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                                ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                                ByVal nScarSottogr As Integer, ByVal nScarCodmarcini As Integer, _
                                                ByVal nScarCodmarcfin As Integer, ByVal dtrTmp As DataRow, _
                                                ByRef ds1 As DataSet, ByRef ds2 As DataSet, _
                                                ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                                ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strScarOrdin, strScarDatini, strScarDatfin, _
                                             lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                                             lScarDaconto, lScarAconto, strScarTipodoc, strScarSerie, _
                                             nScarCausale, nScarGruppo, nScarSottogr, _
                                             nScarCodmarcini, nScarCodmarcfin, dtrTmp, ds1, ds2, _
                                             strLottoxDa, strLottoxA, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds1 = CType(oIn(18), DataSet)
        ds2 = CType(oIn(19), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return CalcolaRettifiche(strDitta, strScarOrdin, strScarDatini, strScarDatfin, lScarDalotto, lScarAlotto, _
                               lScarDacomme, lScarAcomme, lScarDaconto, lScarAconto, strScarTipodoc, strScarSerie, _
                               nScarCausale, nScarGruppo, nScarSottogr, nScarCodmarcini, nScarCodmarcfin, dtrTmp, _
                               ds1, ds2, strLottoxDa, strLottoxA, strClassificazioneLivello1, _
                               strClassificazioneLivello2, strClassificazioneLivello3, strClassificazioneLivello4, _
                               strClassificazioneLivello5, "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function CalcolaRettifiche(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                                ByVal strScarDatini As String, ByVal strScarDatfin As String, _
                                                ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                                ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                                ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                                ByVal nScarSottogr As Integer, ByVal nScarCodmarcini As Integer, _
                                                ByVal nScarCodmarcfin As Integer, ByVal dtrTmp As DataRow, _
                                                ByRef ds1 As DataSet, ByRef ds2 As DataSet, _
                                                ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                                ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                ByVal strClassificazioneLivello5 As String, _
                                                ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                                ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strInnerJoin As String = ""
    Dim strWhere As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strScarOrdin, strScarDatini, strScarDatfin, _
                                             lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                                             lScarDaconto, lScarAconto, strScarTipodoc, strScarSerie, _
                                             nScarCausale, nScarGruppo, nScarSottogr, _
                                             nScarCodmarcini, nScarCodmarcfin, dtrTmp, ds1, ds2, _
                                             strLottoxDa, strLottoxA, strClassificazioneLivello1, _
                                             strClassificazioneLivello2, strClassificazioneLivello3, _
                                             strClassificazioneLivello4, strClassificazioneLivello5, _
                                             strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds1 = CType(oIn(18), DataSet)
        ds2 = CType(oIn(19), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strInnerJoin = "((((((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt AND testmag.tm_tipork = movmag.mm_tipork AND testmag.tm_serie = movmag.mm_serie AND testmag.tm_anno = movmag.mm_anno AND testmag.tm_numdoc = movmag.mm_numdoc)" & _
        " INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga)" & _
        " INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart)" & _
        " INNER JOIN anagra ON keymag.codditt = anagra.codditt AND keymag.km_conto = anagra.an_conto)" & _
        " LEFT JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum)" & _
        " LEFT JOIN tabport ON testmag.codditt = tabport.codditt AND testmag.tm_porto = tabport.tb_codport)" & _
        " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto "
      '--------------------------------------------------------------------------------------------------------------
      '--- Se l'ordinamento NON è per articolo esce dalla routine
      '--------------------------------------------------------------------------------------------------------------
      If strScarOrdin <> "A" Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Compone la stringa WHERE
      '--------------------------------------------------------------------------------------------------------------
      strWhere = " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
        " AND tb_esist = 0" & _
        " AND tb_carfor = 0" & _
        " AND tb_carpro = 0" & _
        " AND tb_carvar = 0" & _
        " AND tb_rescli = 0" & _
        " AND tb_scacli = 0" & _
        " AND tb_scapro = 0" & _
        " AND tb_scavar = 0" & _
        " AND tb_resfor = 0" & _
        " AND tb_giaini = 0" & _
        " AND km_codart = " & CStrSQL(dtrTmp!km_codart) & _
        " AND km_magaz = " & NTSCStr(dtrTmp!km_magaz) & _
        " AND km_fase = " & NTSCStr(dtrTmp!km_fase) & _
        " AND km_aammgg BETWEEN " & CDataSQL(strScarDatini) & " AND " & CDataSQL(strScarDatfin) & _
        IIf(strScarTipodoc <> "", " AND km_tipork = " & CStrSQL(strScarTipodoc), "").ToString & _
        IIf(strScarSerie <> "", " AND km_serie = " & CStrSQL(strScarSerie), "").ToString & _
        IIf(nScarCausale > 0, " AND km_causale = " & nScarCausale, "").ToString & _
        IIf(nScarGruppo > 0, " AND ar_gruppo = " & nScarGruppo, "").ToString & _
        IIf(nScarSottogr > 0, " AND ar_sotgru = " & nScarSottogr, "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
      If (lScarDalotto <> 0) Or (lScarAlotto <> 999999999) Then
        strWhere += " AND km_lotto BETWEEN " & lScarDalotto & " AND " & lScarAlotto
      End If
      If strLottoxDa.Replace("0", "").Trim <> "" Or strLottoxA.Replace("999999999", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz").Trim <> "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz" Then
        strWhere += " AND analotti.alo_lottox BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
      End If
      If (lScarDacomme <> 0) Or (lScarAcomme <> 999999999) Then
        strWhere += " AND km_commeca BETWEEN " & lScarDacomme & " AND " & lScarAcomme
      End If
      If (nScarCodmarcini <> 0) Or (nScarCodmarcfin <> 999) Then
        strWhere += " AND ar_codmarc BETWEEN " & nScarCodmarcini & " AND " & nScarCodmarcfin
      End If
      Select Case strConto
        Case "A"
          If (lScarDaconto <> 0) Or (lScarAconto <> 999999999) Then
            strWhere += " AND km_conto BETWEEN " & lScarDaconto & " AND " & lScarAconto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND km_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      '--- Calcola il valore delle rettifiche sugli acquisti
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT Sum(CASE WHEN tb_vcarfor <> 0 THEN (mm_valore * tb_vcarfor) ELSE CASE WHEN tb_vcarpro <> 0 THEN (mm_valore * tb_vcarpro) ELSE CASE WHEN tb_vcarvar <> 0 THEN (mm_valore * tb_vcarvar) ELSE CASE WHEN tb_vgiaini <> 0 THEN (mm_valore * tb_vgiaini) ELSE CASE WHEN tb_vresfor <> 0 THEN ((mm_valore * tb_vresfor) * -1) ELSE 0 END END END END END) As Valore" & _
        " FROM " & strInnerJoin & strWhere
      ds1 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTMAG")
      '--------------------------------------------------------------------------------------------------------------
      '--- Calcola il valore delle rettifiche sulle vendite
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT Sum(CASE WHEN tb_vscacli <> 0 THEN (mm_valore * tb_vscacli) ELSE CASE WHEN tb_vscapro <> 0 THEN (mm_valore * tb_vscapro) ELSE CASE WHEN tb_vscavar <> 0 THEN (mm_valore * tb_vscavar) ELSE CASE WHEN tb_vrescli <> 0 THEN ((mm_valore * tb_vrescli) * -1) ELSE 0 END END END END) AS Valore" & _
        " FROM " & strInnerJoin & strWhere
      ds2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTMAG")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function GetCollaudi(ByVal strDitta As String, ByVal strTipork As String, _
                                          ByVal strAnno As String, ByVal strSerie As String, _
                                          ByVal strNumdoc As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tm_magimp, tm_datdoc FROM testmag INNER JOIN (movmag INNER JOIN keymag ON (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) AND (movmag.mm_tipork = keymag.km_tipork) AND (movmag.mm_serie = keymag.km_serie) AND (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc)) ON (testmag.codditt = movmag.codditt) AND (testmag.tm_tipork = movmag.mm_tipork) AND (testmag.tm_serie = movmag.mm_serie) AND (testmag.tm_anno = movmag.mm_anno) AND (testmag.tm_numdoc = movmag.mm_numdoc)" & _
        " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
        " AND km_tipork = " & CStrSQL(strTipork) & _
        " AND km_anno = " & strAnno & _
        " AND km_serie = " & CStrSQL(strSerie) & _
        " AND km_numdoc = " & strNumdoc

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TESTMAG")

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GrnpComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                ByRef ds As DataSet, ByVal strScarOrdin As String, _
                                                ByVal strDatini As String, ByVal strDatfin As String, _
                                                ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                                ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                                ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                                ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                                ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                                ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                                ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                                ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                                ByVal strGrnpCodcfam As String, ByVal nGrnpAnnotco As Integer, _
                                                ByVal nGrnpCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                                ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                ByVal bAmm As Boolean, ByVal strQuery As String) As Boolean
    Try
      'obsoleta
      Return GrnpComponiStringa(strDitta, dtrTmp, ds, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                               lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                               strScarDacodart, strScarAcodart, nScarCodmarcini, nScarCodmarcfin, _
                               nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, _
                               nScarSottogr, bRigheInevase, strGrnpCodcfam, nGrnpAnnotco, nGrnpCodstag, bModuloCRM, _
                               bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, strQuery, "", "".PadLeft(50, "Z"c), _
                               "", "", "", "", "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrnpComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                 ByRef ds As DataSet, ByVal strScarOrdin As String, _
                                                 ByVal strDatini As String, ByVal strDatfin As String, _
                                                 ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                                 ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                                 ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                 ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                 ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                                 ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                                 ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                                 ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                                 ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                                 ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                                 ByVal strGrnpCodcfam As String, ByVal nGrnpAnnotco As Integer, _
                                                 ByVal nGrnpCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal strQuery As String, _
                                                 ByVal strLottoxDa As String, ByVal strLottoxA As String) As Boolean
    
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, strScarOrdin, strDatini, strDatfin, _
                                             nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                                             lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                                             strScarDacodart, strScarAcodart, nScarCodmarcini, nScarCodmarcfin, _
                                             nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, nScarCausale, _
                                             nScarGruppo, nScarSottogr, bRigheInevase, strGrnpCodcfam, nGrnpAnnotco, _
                                             nGrnpCodstag, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                                             strRegvis, bAmm, strQuery, strLottoxDa, strLottoxA})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return GrnpComponiStringa(strDitta, dtrTmp, ds, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                               lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                               strScarDacodart, strScarAcodart, nScarCodmarcini, nScarCodmarcfin, _
                               nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, _
                               nScarSottogr, bRigheInevase, strGrnpCodcfam, nGrnpAnnotco, nGrnpCodstag, bModuloCRM, _
                               bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, strQuery, strLottoxDa, strLottoxA, _
                               "", "", "", "", "")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrnpComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                 ByRef ds As DataSet, ByVal strScarOrdin As String, _
                                                 ByVal strDatini As String, ByVal strDatfin As String, _
                                                 ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                                 ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                                 ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                 ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                 ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                                 ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                                 ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                                 ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                                 ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                                 ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                                 ByVal strGrnpCodcfam As String, ByVal nGrnpAnnotco As Integer, _
                                                 ByVal nGrnpCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal strQuery As String, _
                                                 ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                                 ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, strScarOrdin, strDatini, strDatfin, _
                                             nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                                             lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                                             strScarDacodart, strScarAcodart, nScarCodmarcini, nScarCodmarcfin, _
                                             nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, nScarCausale, _
                                             nScarGruppo, nScarSottogr, bRigheInevase, strGrnpCodcfam, nGrnpAnnotco, _
                                             nGrnpCodstag, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                                             strRegvis, bAmm, strQuery, strLottoxDa, strLottoxA, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return GrnpComponiStringa(strDitta, dtrTmp, ds, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                               lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                               strScarDacodart, strScarAcodart, nScarCodmarcini, nScarCodmarcfin, _
                               nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, _
                               nScarSottogr, bRigheInevase, strGrnpCodcfam, nGrnpAnnotco, nGrnpCodstag, bModuloCRM, _
                               bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, strQuery, _
                               strLottoxDa, strLottoxA, strClassificazioneLivello1, strClassificazioneLivello2, _
                               strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                               "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function GrnpComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                 ByRef ds As DataSet, ByVal strScarOrdin As String, _
                                                 ByVal strDatini As String, ByVal strDatfin As String, _
                                                 ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                                 ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                                 ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                 ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                 ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                                 ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                                 ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                                 ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                                 ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                                 ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                                 ByVal strGrnpCodcfam As String, ByVal nGrnpAnnotco As Integer, _
                                                 ByVal nGrnpCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal strQuery As String, _
                                                 ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                                 ByVal strClassificazioneLivello1 As String, _
                                                 ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, _
                                                 ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String, _
                                                 ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                                 ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Dim strSQL As String
    Dim strInnerJoin As String = ""
    Dim bLottoNew As Boolean = False
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, strScarOrdin, strDatini, strDatfin, _
                                             nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                                             lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                                             strScarDacodart, strScarAcodart, nScarCodmarcini, nScarCodmarcfin, _
                                             nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, nScarCausale, _
                                             nScarGruppo, nScarSottogr, bRigheInevase, strGrnpCodcfam, nGrnpAnnotco, _
                                             nGrnpCodstag, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                                             strRegvis, bAmm, strQuery, strLottoxDa, strLottoxA, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strInnerJoin = "(((((testprb INNER JOIN movprb ON testprb.codditt = movprb.codditt AND testprb.tm_tipork = movprb.mm_tipork AND testprb.tm_anno = movprb.mm_anno AND testprb.tm_serie = movprb.mm_serie AND testprb.tm_numdoc = movprb.mm_numdoc)" & _
        " INNER JOIN keyprb ON movprb.codditt = keyprb.codditt AND movprb.mm_tipork = keyprb.km_tipork AND movprb.mm_anno = keyprb.km_anno AND movprb.mm_serie = keyprb.km_serie AND movprb.mm_numdoc = keyprb.km_numdoc AND movprb.mm_riga = keyprb.km_riga)" & _
        " INNER JOIN artico ON artico.codditt = movprb.codditt AND artico.ar_codart = movprb.mm_codart)" & _
        " INNER JOIN anagra ON anagra.codditt = keyprb.codditt AND anagra.an_conto = keyprb.km_conto)" & _
        " LEFT JOIN commess ON movprb.codditt = commess.codditt AND movprb.mm_commeca = commess.co_comme)" & _
        " LEFT JOIN analotti ON movprb.codditt = analotti.codditt AND movprb.mm_codart = analotti.alo_codart AND movprb.mm_lotto = analotti.alo_lotto "
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT keyprb.*, mm_quant, mm_prezzo, mm_valore, mm_nprquaeva, " & _
        " mm_ornum, mm_scont1, mm_scont2, mm_scont3, mm_prelist, tm_riferim, mm_prezvalc, " & _
        " mm_colli, mm_misura1, mm_misura2, mm_misura3, mm_controp, mm_commeca, mm_codcena, " & _
        " mm_codcfam, mm_codnomc, mm_provv, mm_vprovv, mm_provv2, mm_vprovv2, mm_perqta, co_descr1, " & _
        " analotti.alo_lottox as xx_lottox " & _
        " FROM " & strInnerJoin & _
        " WHERE keyprb.codditt = " & CStrSQL(strDitta) & _
        " AND km_codart = '" & NTSCStr(dtrTmp!km_codart) & "'" & _
        " AND km_magaz = " & NTSCStr(dtrTmp!km_magaz) & _
        " AND km_fase = " & NTSCStr(dtrTmp!km_fase) & _
        " AND km_lotto BETWEEN " & lScarDalotto & " AND " & lScarAlotto & _
        " AND km_commeca BETWEEN " & lScarDacomme & " AND " & lScarAcomme & _
        " AND km_aammgg BETWEEN " & strDatini & " AND " & strDatfin & _
        " AND km_carscar <> 0"
      If strScarOrdin = "C" Then strSQL += " AND km_conto = " & NTSCStr(dtrTmp!km_conto)
      If strScarTipodoc <> "" Then strSQL += " AND km_tipork = " & CStrSQL(strScarTipodoc)
      If strScarSerie <> "" Then strSQL += " AND km_serie = " & CStrSQL(strScarSerie)
      If nScarCausale > 0 Then strSQL += " AND km_causale = " & nScarCausale
      ''If bRigheInevase = True Then strSQL += " AND mm_nprflevas = 'C'"
      Select Case strConto
        Case "A"
          If (lScarDaconto <> 0) Or (lScarAconto <> 999999999) Then
            strSQL += " AND km_conto BETWEEN " & lScarDaconto & " AND " & lScarAconto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND km_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      If bLottoNew = False Then
        If NTSCInt(strLottoxDa) <> 0 Or NTSCInt(strLottoxA) <> 999999999 Then
          strSQL += " AND km_lotto BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      Else
        If strLottoxDa.Replace("0", "").Trim <> "" Or strLottoxA.Replace("999999999", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz").Trim <> "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz" Then
          strSQL += " AND analotti.alo_lottox BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      End If
      If (bModuloCRM = True) And (bIsCRMUser = True) And (strScarOrdin = "A") Then
        strSQL += " AND ("
        If strAccvis <> "T" Then
          strSQL += "(km_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                                 " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                                 " AND anagra.an_tipo = 'C'" & _
                                 " AND leads.le_coddest = 0"
          Select Case strAccvis
            Case "P" : strSQL += " AND le_opinc = " & lCodorgaOperat
            Case "C" : strSQL += " AND le_opinc IN (" & strRegvis & ")"
          End Select
          strSQL += "))"
        Else
          strSQL += " anagra.an_tipo = 'C'"
        End If
        If bAmm = False Then strSQL += " AND an_tipo <> 'F'" Else strSQL += " OR an_tipo <> 'C'"
        strSQL += ")"
      End If
      TraduciWhere(Replace(strQuery, "movmag", "movprb"), strSQL)
      strSQL = Replace(strSQL, "testmag", "testprb")
      strSQL = Replace(strSQL, "movmag", "movprb")
      strSQL = Replace(strSQL, "keymag", "keyprb")

      strSQL += " ORDER BY km_codart, km_fase, km_magaz, km_aammgg, km_carscar DESC, km_tipork, km_serie," & _
        " km_numdoc, km_riga"
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVPRB")
      '--------------------------------------------------------------------------------------------------------------
      ds.Tables("MOVPRB").Columns.Add(New DataColumn("xx_descr", GetType(String)))
      ds.Tables("MOVPRB").Columns.Add(New DataColumn("xx_clfor", GetType(String)))
      ds.Tables("MOVPRB").Columns.Add(New DataColumn("xx_scarichi", GetType(Decimal)))
      ds.Tables("MOVPRB").Columns.Add(New DataColumn("xx_esistenza", GetType(Decimal)))
      ds.Tables("MOVPRB").Columns.Add(New DataColumn("xx_prezzo", GetType(Decimal)))
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function GrnpApri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                    ByVal strDatini As String, ByVal strDatfin As String, _
                                    ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                    ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                    ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                    ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                    ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                    ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                    ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                    ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                    ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                    ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                    ByVal strGrnpCodcfam As String, ByVal nGrnpAnnotco As Integer, _
                                    ByVal nGrnpCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                    ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                    ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                    ByVal bAmm As Boolean, ByRef ds As DataSet, ByVal strQuery As String) As Boolean
    Try
      'obsoleta
      Return GrnpApri(strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                     lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, strScarDacodart, strScarAcodart, _
                     nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, _
                     nScarCausale, nScarGruppo, nScarSottogr, bRigheInevase, strGrnpCodcfam, nGrnpAnnotco, _
                     nGrnpCodstag, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                     bAmm, ds, strQuery, "", "".PadLeft(50, "Z"c), "", "", "", "", "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrnpApri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                       ByVal strDatini As String, ByVal strDatfin As String, _
                                       ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                       ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                       ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                       ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                       ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                       ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                       ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                       ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                       ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                       ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                       ByVal strGrnpCodcfam As String, ByVal nGrnpAnnotco As Integer, _
                                       ByVal nGrnpCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                       ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                       ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                       ByVal bAmm As Boolean, ByRef ds As DataSet, ByVal strQuery As String, _
                                       ByVal strLottoxDa As String, ByVal strLottoxA As String) As Boolean
    
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                                             lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, _
                                             lScarDacomme, lScarAcomme, strScarDacodart, strScarAcodart, _
                                             nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, _
                                             strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, nScarSottogr, _
                                             bRigheInevase, strGrnpCodcfam, nGrnpAnnotco, nGrnpCodstag, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, ds, strQuery, _
                                             strLottoxDa, strLottoxA})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(33), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return GrnpApri(strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                     lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, strScarDacodart, strScarAcodart, _
                     nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, _
                     nScarCausale, nScarGruppo, nScarSottogr, bRigheInevase, strGrnpCodcfam, nGrnpAnnotco, _
                     nGrnpCodstag, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                     bAmm, ds, strQuery, strLottoxDa, strLottoxA, "", "", "", "", "")
      
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrnpApri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                       ByVal strDatini As String, ByVal strDatfin As String, _
                                       ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                       ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                       ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                       ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                       ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                       ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                       ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                       ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                       ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                       ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                       ByVal strGrnpCodcfam As String, ByVal nGrnpAnnotco As Integer, _
                                       ByVal nGrnpCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                       ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                       ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                       ByVal bAmm As Boolean, ByRef ds As DataSet, ByVal strQuery As String, _
                                       ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                       ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                       ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                       ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                                             lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, _
                                             lScarDacomme, lScarAcomme, strScarDacodart, strScarAcodart, _
                                             nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, _
                                             strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, nScarSottogr, _
                                             bRigheInevase, strGrnpCodcfam, nGrnpAnnotco, nGrnpCodstag, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, ds, strQuery, _
                                             strLottoxDa, strLottoxA, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(33), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return GrnpApri(strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                      lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                      strScarDacodart, strScarAcodart, nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, _
                      strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, nScarSottogr, bRigheInevase, _
                      strGrnpCodcfam, nGrnpAnnotco, nGrnpCodstag, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                      strRegvis, bAmm, ds, strQuery, strLottoxDa, strLottoxA, strClassificazioneLivello1, _
                      strClassificazioneLivello2, strClassificazioneLivello3, strClassificazioneLivello4, _
                      strClassificazioneLivello5, "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function GrnpApri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                       ByVal strDatini As String, ByVal strDatfin As String, _
                                       ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                       ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                       ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                       ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                       ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                       ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                       ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                       ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                       ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                       ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                       ByVal strGrnpCodcfam As String, ByVal nGrnpAnnotco As Integer, _
                                       ByVal nGrnpCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                       ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                       ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                       ByVal bAmm As Boolean, ByRef ds As DataSet, ByVal strQuery As String, _
                                       ByVal strLottoxDa As String, ByVal strLottoxA As String, _
                                       ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                       ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                       ByVal strClassificazioneLivello5 As String, _
                                       ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                       ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strInnerJoin As String = ""
    Dim bLottoNew As Boolean = False
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                                             lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, _
                                             lScarDacomme, lScarAcomme, strScarDacodart, strScarAcodart, _
                                             nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, _
                                             strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, nScarSottogr, _
                                             bRigheInevase, strGrnpCodcfam, nGrnpAnnotco, nGrnpCodstag, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, ds, strQuery, _
                                             strLottoxDa, strLottoxA, strClassificazioneLivello1, _
                                             strClassificazioneLivello2, strClassificazioneLivello3, _
                                             strClassificazioneLivello4, strClassificazioneLivello5, _
                                             strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(33), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strInnerJoin = "(((((testprb INNER JOIN movprb ON testprb.codditt = movprb.codditt AND testprb.tm_tipork = movprb.mm_tipork AND testprb.tm_anno = movprb.mm_anno AND testprb.tm_serie = movprb.mm_serie AND testprb.tm_numdoc = movprb.mm_numdoc)" & _
        " INNER JOIN keyprb ON movprb.codditt = keyprb.codditt AND movprb.mm_tipork = keyprb.km_tipork AND movprb.mm_anno = keyprb.km_anno AND movprb.mm_serie = keyprb.km_serie AND movprb.mm_numdoc = keyprb.km_numdoc AND movprb.mm_riga = keyprb.km_riga)" & _
        " INNER JOIN artico ON artico.codditt = movprb.codditt AND artico.ar_codart = movprb.mm_codart)" & _
        " INNER JOIN anagra ON anagra.codditt = keyprb.codditt AND anagra.an_conto = keyprb.km_conto)" & _
        " LEFT JOIN commess ON movprb.codditt = commess.codditt AND movprb.mm_commeca = commess.co_comme)" & _
        " LEFT JOIN analotti ON movprb.codditt = analotti.codditt AND movprb.mm_codart = analotti.alo_codart AND movprb.mm_lotto = analotti.alo_lotto "
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT km_codart, km_fase, km_magaz" & _
        IIf(strScarOrdin = "C", ", km_conto", "").ToString & _
        " FROM " & strInnerJoin & _
        " WHERE keyprb.codditt = " & CStrSQL(strDitta) & _
        " AND km_carscar <> 0" & _
        IIf(strScarTipodoc <> "", " AND km_tipork = " & CStrSQL(strScarTipodoc), "").ToString & _
        IIf(strScarSerie <> "", " AND km_serie = " & CStrSQL(strScarSerie), "").ToString & _
        IIf(nScarCausale > 0, " AND km_causale = " & nScarCausale, "").ToString & _
        IIf(nScarGruppo > 0, " AND ar_gruppo =  " & nScarGruppo, "").ToString & _
        IIf(nScarSottogr > 0, " AND ar_sotgru = " & nScarSottogr, "").ToString & _
        IIf(strGrnpCodcfam.Trim <> "", " AND ar_famprod = " & CStrSQL(strGrnpCodcfam), "").ToString & _
        IIf(bRigheInevase = True, " AND mm_nprflevas = 'C'", "").ToString & _
        IIf(strScarOrdin = "C", " AND mm_nprflevas = 'C'", "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
        IIf(nGrnpAnnotco <> 0, " AND ar_anno = " & nGrnpAnnotco, "").ToString & _
        IIf(nGrnpCodstag <> 0, " AND ar_codstag = " & nGrnpCodstag, "").ToString
      If (NTSCDate(strDatini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strDatfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND km_aammgg BETWEEN " & strDatini & " AND " & strDatfin
      End If
      If (nScarDamagaz <> 0) Or (nScarAmagaz <> 9999) Then
        strSQL += " AND km_magaz BETWEEN " & nScarDamagaz & " AND " & nScarAmagaz
      End If
      If (lScarDalotto <> 0) Or (lScarAlotto <> 999999999) Then
        strSQL += " AND km_lotto BETWEEN " & lScarDalotto & " AND " & lScarAlotto
      End If
      If (lScarDacomme <> 0) Or (lScarAcomme <> 999999999) Then
        strSQL += " AND km_commeca BETWEEN " & lScarDacomme & " AND " & lScarAcomme
      End If
      If (nScarCodmarcini <> 0) Or (nScarCodmarcfin <> 999) Then
        strSQL += " AND ar_codmarc BETWEEN " & nScarCodmarcini & " AND " & nScarCodmarcfin
      End If
      If bLottoNew = False Then
        If NTSCInt(strLottoxDa) <> 0 Or NTSCInt(strLottoxA) <> 999999999 Then
          strSQL += " AND km_lotto BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      Else
        If strLottoxDa.Replace("0", "").Trim <> "" Or strLottoxA.Replace("999999999", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz").Trim <> "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz" Then
          strSQL += " AND analotti.alo_lottox BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      End If
      Select Case strCodart
        Case "A"
          If (strScarDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or _
             (strScarAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "Z"c)) Then
            strSQL += " AND km_codart BETWEEN '" & strScarDacodart & "' AND '" & strScarAcodart & "'"
          End If
          If (nScarFaseini <> 0) Or (nScarFasefin <> 9999) Then
            strSQL += " AND km_fase BETWEEN " & nScarFaseini & " AND " & nScarFasefin
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL += _
              " AND CAST(km_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(km_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      Select Case strConto
        Case "A"
          If (lScarDaconto <> 0) Or (lScarAconto <> 999999999) Then
            strSQL += " AND km_conto BETWEEN " & lScarDaconto & " AND " & lScarAconto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND km_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      If (bModuloCRM = True) And (bIsCRMUser = True) Then
        strSQL += " AND ("
        If strAccvis <> "T" Then
          strSQL += "(an_tipo = 'C' AND an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                                                   " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                                                   " AND leads.le_coddest = 0"
          Select Case strAccvis
            Case "P" : strSQL += " AND le_opinc = " & lCodorgaOperat
            Case "C" : strSQL += " AND le_opinc IN (" & strRegvis & ")"
          End Select
          strSQL += "))"
        Else
          strSQL += " anagra.an_tipo = 'C'"
        End If
        If bAmm = False Then strSQL += " AND an_tipo <> 'F'" Else strSQL += " OR an_tipo <> 'C'"
        strSQL += ")"
      End If
      TraduciWhere(Replace(strQuery, "movmag", "movprb"), strSQL)
      strSQL = Replace(strSQL, "testmag", "testprb")
      strSQL = Replace(strSQL, "movmag", "movprb")
      strSQL = Replace(strSQL, "keymag", "keyprb")

      Select Case strScarOrdin
        Case "A" : strSQL += " GROUP BY km_codart, km_fase, km_magaz ORDER BY km_codart, km_fase, km_magaz"
        Case "C" : strSQL += " GROUP BY km_conto, km_codart, km_fase, km_magaz" & _
                             " ORDER BY km_conto, km_codart, km_fase , km_magaz"
      End Select
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVPRB")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function GrnpGetArtpro(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                            ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ap_esist FROM ARTPRO" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ap_codart = '" & NTSCStr(dtrTmp!km_codart) & "'" & _
        " AND ap_magaz = " & NTSCStr(dtrTmp!km_magaz) & _
        " AND ap_fase = " & NTSCStr(dtrTmp!km_fase)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GrmaComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                          ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                          ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                          ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                          ByVal strDatini As String, ByVal strDatfin As String, _
                                          ByVal strScarOrdin As String, ByVal strScarTipodoc As String, _
                                          ByVal strScarSerie As String, ByVal nScarCausale As Integer, _
                                          ByVal bRigheInevase As Boolean, ByVal bModuloCRM As Boolean, _
                                          ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                          ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                          ByVal bAmm As Boolean, ByVal strQuery As String) As Boolean
    Try
      'obsoleta
      Return GrmaComponiStringa(strDitta, dtrTmp, ds, lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, _
                               lScarDacomme, lScarAcomme, strDatini, strDatfin, strScarOrdin, strScarTipodoc, _
                               strScarSerie, nScarCausale, bRigheInevase, bModuloCRM, bIsCRMUser, strAccvis, _
                               lCodorgaOperat, strRegvis, bAmm, strQuery, "", "".PadLeft(50, "Z"c))
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrmaComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                                 ByRef ds As DataSet, _
                                                 ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                                 ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                                 ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                                 ByVal strDatini As String, ByVal strDatfin As String, _
                                                 ByVal strScarOrdin As String, ByVal strScarTipodoc As String, _
                                                 ByVal strScarSerie As String, ByVal nScarCausale As Integer, _
                                                 ByVal bRigheInevase As Boolean, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal strQuery As String, _
                                                 ByVal strLottoxDa As String, ByVal strLottoxA As String) As Boolean
    Dim strSQL As String
    Dim strInnerJoin As String
    Dim bLottoNew As Boolean = False
    Dim dttTmp As New DataTable
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lScarDaconto, lScarAconto, _
                                             lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                                             strDatini, strDatfin, strScarOrdin, strScarTipodoc, strScarSerie, _
                                             nScarCausale, bRigheInevase, bModuloCRM, bIsCRMUser, strAccvis, _
                                             lCodorgaOperat, strRegvis, bAmm, strQuery, _
                                             strLottoxDa, strLottoxA})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '----------------
      ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      dttTmp.Clear()
      dttTmp.Dispose()

      strInnerJoin = "(((((testprb INNER JOIN movprb ON testprb.codditt = movprb.codditt AND testprb.tm_tipork = movprb.mm_tipork AND testprb.tm_serie = movprb.mm_serie AND testprb.tm_anno = movprb.mm_anno AND testprb.tm_numdoc = movprb.mm_numdoc)" & _
        " INNER JOIN movmatr ON movprb.codditt = movmatr.codditt AND movprb.mm_tipork = movmatr.mma_tipork AND movprb.mm_anno = movmatr.mma_anno AND movprb.mm_serie = movmatr.mma_serie AND movprb.mm_numdoc = movmatr.mma_numdoc AND movprb.mm_riga = movmatr.mma_riga)" & _
        " INNER JOIN artico ON artico.codditt = movprb.codditt AND artico.ar_codart = movprb.mm_codart)" & _
        " INNER JOIN keyprb ON movprb.codditt = keyprb.codditt AND movprb.mm_tipork = keyprb.km_tipork AND movprb.mm_anno = keyprb.km_anno AND movprb.mm_serie = keyprb.km_serie AND movprb.mm_numdoc = keyprb.km_numdoc AND movprb.mm_riga = keyprb.km_riga)" & _
        " INNER JOIN anagra ON anagra.codditt = keyprb.codditt AND anagra.an_conto = keyprb.km_conto)" & _
        " LEFT JOIN analotti ON movprb.codditt = analotti.codditt AND movprb.mm_codart = analotti.alo_codart AND movprb.mm_lotto = analotti.alo_lotto "

      '---------------------------------
      'lancio la query
      strSQL = "SELECT DISTINCTROW keyprb.*, mm_quant, mm_prezzo, mm_valore, mm_nprquaeva, " & _
        " mm_ornum, mm_scont1, mm_scont2, mm_scont3, mm_prelist, tm_riferim, mm_prezvalc, " & _
        " mm_colli, mm_misura1, mm_misura2, mm_misura3, mm_controp, mm_commeca, mm_codcena, " & _
        " mm_codcfam, mm_codnomc, mm_provv, mm_vprovv, mm_provv2, mm_vprovv2, mm_perqta, mma_matric, mma_quant, " & _
        " analotti.alo_lottox as xx_lottox " & _
        " FROM " & strInnerJoin & _
        " WHERE keyprb.codditt = " & CStrSQL(strDitta) & _
        " AND km_codart = '" & NTSCStr(dtrTmp!km_codart) & "'" & _
        " AND km_magaz = " & NTSCStr(dtrTmp!km_magaz) & _
        " AND km_fase = " & NTSCStr(dtrTmp!km_fase) & _
        " AND km_conto BETWEEN " & lScarDaconto & " AND " & lScarAconto & _
        " AND km_lotto BETWEEN " & lScarDalotto & " AND " & lScarAlotto & _
        " AND km_commeca BETWEEN " & lScarDacomme & " AND " & lScarAcomme & _
        " AND km_aammgg BETWEEN " & strDatini & " AND " & strDatfin & _
        " AND km_carscar <> 0 AND mma_matric = " & CStrSQL(dtrTmp!mma_matric)
      If bLottoNew = False Then
        If NTSCInt(strLottoxDa) <> 0 Or NTSCInt(strLottoxA) <> 999999999 Then
          strSQL += " AND km_lotto BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      Else
        If strLottoxDa.Replace("0", "").Trim <> "" Or strLottoxA.Replace("999999999", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz").Trim <> "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz" Then
          strSQL += " AND analotti.alo_lottox BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      End If
      If strScarOrdin = "C" Then strSQL = strSQL & " AND km_conto = " & NTSCStr(dtrTmp!km_conto)
      If strScarTipodoc <> "" Then strSQL = strSQL & " AND km_tipork = '" & strScarTipodoc & "'"
      If strScarSerie <> "" Then strSQL = strSQL & " AND km_serie = '" & strScarSerie & "'"
      If nScarCausale > 0 Then strSQL = strSQL & " AND km_causale = " & nScarCausale
      If bRigheInevase = True Then strSQL = strSQL & " AND mm_nprflevas = 'C'"
      If (bModuloCRM = True) And (bIsCRMUser = True) And (strScarOrdin = "A") Then
        strSQL = strSQL & " AND ("
        If strAccvis <> "T" Then
          strSQL = strSQL & "(km_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                                         " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                                         " AND anagra.an_tipo = 'C'" & _
                                         " AND leads.le_coddest = 0"
          Select Case strAccvis
            Case "P" : strSQL = strSQL & " AND le_opinc = " & lCodorgaOperat
            Case "C" : strSQL = strSQL & " AND le_opinc IN (" & strRegvis & ")"
          End Select
          strSQL = strSQL & "))"
        Else
          strSQL = strSQL & " anagra.an_tipo = 'C'"
        End If
        If bAmm = False Then
          strSQL = strSQL & " AND an_tipo <> 'F'"
        Else
          strSQL = strSQL & " OR an_tipo <> 'C'"
        End If
        strSQL = strSQL & ")"
      End If

      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(Replace(strQuery, "movmag", "movprb"), strSQL)

      strSQL = strSQL & " ORDER BY km_codart, km_fase, km_magaz, km_aammgg, km_carscar DESC, km_tipork, km_serie, km_numdoc, km_riga"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVPRB")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GrmaApri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                    ByVal strDatini As String, ByVal strDatfin As String, _
                                    ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                    ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                    ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                    ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                    ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                    ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                    ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                    ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                    ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                    ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                    ByVal strGrmaCodcfam As String, ByVal nGrmaAnnotco As Integer, _
                                    ByVal nGrmaCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                    ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                    ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                    ByVal bAmm As Boolean, ByVal strGrmaDamatric As String, _
                                    ByVal strGrmaAmatric As String, ByRef ds As DataSet, ByVal strQuery As String) As Boolean
    Try
      'obsoleta
      Return GrmaApri(strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                     lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, strScarDacodart, strScarAcodart, _
                     nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, _
                     nScarCausale, nScarGruppo, nScarSottogr, bRigheInevase, strGrmaCodcfam, nGrmaAnnotco, _
                     nGrmaCodstag, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                     bAmm, strGrmaDamatric, strGrmaAmatric, ds, strQuery, "", "".PadLeft(50, "Z"c), "", "", "", "", "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrmaApri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                       ByVal strDatini As String, ByVal strDatfin As String, _
                                       ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                       ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                       ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                       ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                       ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                       ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                       ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                       ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                       ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                       ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                       ByVal strGrmaCodcfam As String, ByVal nGrmaAnnotco As Integer, _
                                       ByVal nGrmaCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                       ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                       ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                       ByVal bAmm As Boolean, ByVal strGrmaDamatric As String, _
                                       ByVal strGrmaAmatric As String, ByRef ds As DataSet, _
                                       ByVal strQuery As String, ByVal strLottoxDa As String, _
                                       ByVal strLottoxA As String) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                                             lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, _
                                             lScarDacomme, lScarAcomme, strScarDacodart, strScarAcodart, _
                                             nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, _
                                             strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, nScarSottogr, _
                                             bRigheInevase, strGrmaCodcfam, nGrmaAnnotco, nGrmaCodstag, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, strGrmaDamatric, _
                                             strGrmaAmatric, ds, strQuery, strLottoxDa, strLottoxA})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(35), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return GrmaApri(strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                     lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, strScarDacodart, strScarAcodart, _
                     nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, _
                     nScarCausale, nScarGruppo, nScarSottogr, bRigheInevase, strGrmaCodcfam, nGrmaAnnotco, _
                     nGrmaCodstag, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                     bAmm, strGrmaDamatric, strGrmaAmatric, ds, strQuery, strLottoxDa, strLottoxA, "", "", "", "", "")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrmaApri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                       ByVal strDatini As String, ByVal strDatfin As String, _
                                       ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                       ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                       ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                       ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                       ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                       ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                       ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                       ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                       ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                       ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                       ByVal strGrmaCodcfam As String, ByVal nGrmaAnnotco As Integer, _
                                       ByVal nGrmaCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                       ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                       ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                       ByVal bAmm As Boolean, ByVal strGrmaDamatric As String, _
                                       ByVal strGrmaAmatric As String, ByRef ds As DataSet, _
                                       ByVal strQuery As String, ByVal strLottoxDa As String, _
                                       ByVal strLottoxA As String, _
                                       ByVal strClassificazioneLivello1 As String, _
                                       ByVal strClassificazioneLivello2 As String, _
                                       ByVal strClassificazioneLivello3 As String, _
                                       ByVal strClassificazioneLivello4 As String, _
                                       ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                                             lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, _
                                             lScarDacomme, lScarAcomme, strScarDacodart, strScarAcodart, _
                                             nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, _
                                             strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, nScarSottogr, _
                                             bRigheInevase, strGrmaCodcfam, nGrmaAnnotco, nGrmaCodstag, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, strGrmaDamatric, _
                                             strGrmaAmatric, ds, strQuery, strLottoxDa, strLottoxA, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(35), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return GrmaApri(strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                      lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                      strScarDacodart, strScarAcodart, nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, _
                      strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, nScarSottogr, bRigheInevase, _
                      strGrmaCodcfam, nGrmaAnnotco, nGrmaCodstag, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                      strRegvis, bAmm, strGrmaDamatric, strGrmaAmatric, ds, strQuery, strLottoxDa, strLottoxA, _
                      strClassificazioneLivello1, strClassificazioneLivello2, strClassificazioneLivello3, _
                      strClassificazioneLivello4, strClassificazioneLivello5, "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function GrmaApri(ByVal strDitta As String, ByVal strScarOrdin As String, _
                                       ByVal strDatini As String, ByVal strDatfin As String, _
                                       ByVal nScarDamagaz As Integer, ByVal nScarAmagaz As Integer, _
                                       ByVal lScarDaconto As Integer, ByVal lScarAconto As Integer, _
                                       ByVal lScarDalotto As Integer, ByVal lScarAlotto As Integer, _
                                       ByVal lScarDacomme As Integer, ByVal lScarAcomme As Integer, _
                                       ByVal strScarDacodart As String, ByVal strScarAcodart As String, _
                                       ByVal nScarCodmarcini As Integer, ByVal nScarCodmarcfin As Integer, _
                                       ByVal nScarFaseini As Integer, ByVal nScarFasefin As Integer, _
                                       ByVal strScarTipodoc As String, ByVal strScarSerie As String, _
                                       ByVal nScarCausale As Integer, ByVal nScarGruppo As Integer, _
                                       ByVal nScarSottogr As Integer, ByVal bRigheInevase As Boolean, _
                                       ByVal strGrmaCodcfam As String, ByVal nGrmaAnnotco As Integer, _
                                       ByVal nGrmaCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                       ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                       ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                       ByVal bAmm As Boolean, ByVal strGrmaDamatric As String, _
                                       ByVal strGrmaAmatric As String, ByRef ds As DataSet, _
                                       ByVal strQuery As String, ByVal strLottoxDa As String, _
                                       ByVal strLottoxA As String, _
                                       ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                       ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                       ByVal strClassificazioneLivello5 As String, _
                                       ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                       ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strInnerJoin As String
    Dim bLottoNew As Boolean = False
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strScarOrdin, strDatini, strDatfin, nScarDamagaz, nScarAmagaz, _
                                             lScarDaconto, lScarAconto, lScarDalotto, lScarAlotto, _
                                             lScarDacomme, lScarAcomme, strScarDacodart, strScarAcodart, _
                                             nScarCodmarcini, nScarCodmarcfin, nScarFaseini, nScarFasefin, _
                                             strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, nScarSottogr, _
                                             bRigheInevase, strGrmaCodcfam, nGrmaAnnotco, nGrmaCodstag, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm, strGrmaDamatric, _
                                             strGrmaAmatric, ds, strQuery, strLottoxDa, strLottoxA, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, _
                                             strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(35), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strInnerJoin = "(((((testprb INNER JOIN movprb ON testprb.codditt = movprb.codditt AND testprb.tm_tipork = movprb.mm_tipork AND testprb.tm_serie = movprb.mm_serie AND testprb.tm_anno = movprb.mm_anno AND testprb.tm_numdoc = movprb.mm_numdoc)" & _
        " INNER JOIN movmatr ON movprb.codditt = movmatr.codditt AND movprb.mm_tipork = movmatr.mma_tipork AND movprb.mm_anno = movmatr.mma_anno AND movprb.mm_serie = movmatr.mma_serie AND movprb.mm_numdoc = movmatr.mma_numdoc AND movprb.mm_riga = movmatr.mma_riga)" & _
        " INNER JOIN artico ON artico.codditt = movprb.codditt AND artico.ar_codart = movprb.mm_codart)" & _
        " INNER JOIN keyprb ON movprb.codditt = keyprb.codditt AND movprb.mm_tipork = keyprb.km_tipork AND movprb.mm_anno = keyprb.km_anno AND movprb.mm_serie = keyprb.km_serie AND movprb.mm_numdoc = keyprb.km_numdoc AND movprb.mm_riga = keyprb.km_riga)" & _
        " INNER JOIN anagra ON anagra.codditt = keyprb.codditt AND anagra.an_conto = keyprb.km_conto)" & _
        " LEFT JOIN analotti ON movprb.codditt = analotti.codditt AND movprb.mm_codart = analotti.alo_codart AND movprb.mm_lotto = analotti.alo_lotto "
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT km_codart, km_fase, km_magaz, mma_matric" & _
        IIf(strScarOrdin = "C", ", km_conto", "").ToString & _
        " FROM " & strInnerJoin & _
        " WHERE keyprb.codditt = " & CStrSQL(strDitta) & _
        " AND km_carscar <> 0 " & _
        " AND mma_matric BETWEEN '" & strGrmaDamatric & "' AND '" & strGrmaAmatric & "'" & _
        IIf(strScarTipodoc <> "", " AND km_tipork = " & CStrSQL(strScarTipodoc), "").ToString & _
        IIf(strScarSerie <> "", " AND km_serie = " & CStrSQL(strScarSerie), "").ToString & _
        IIf(nScarCausale > 0, " AND km_causale = " & nScarCausale, "").ToString & _
        IIf(nScarGruppo > 0, " AND ar_gruppo =  " & nScarGruppo, "").ToString & _
        IIf(nScarSottogr > 0, " AND ar_sotgru = " & nScarSottogr, "").ToString & _
        IIf(strGrmaCodcfam.Trim <> "", " AND ar_famprod = " & CStrSQL(strGrmaCodcfam), "").ToString & _
        IIf(bRigheInevase = True, " AND mm_nprflevas = 'C'", "").ToString & _
        IIf(strScarOrdin = "C", " AND mm_nprflevas = 'C'", "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
        IIf(nGrmaAnnotco <> 0, " AND ar_anno = " & nGrmaAnnotco, "").ToString & _
        IIf(nGrmaCodstag <> 0, " AND ar_codstag = " & nGrmaCodstag, "").ToString
      If (nScarCodmarcini <> 0) Or (nScarCodmarcfin <> 999) Then
        strSQL += " AND ar_codmarc BETWEEN " & nScarCodmarcini & " AND " & nScarCodmarcfin
      End If
      If bLottoNew = False Then
        If NTSCInt(strLottoxDa) <> 0 Or NTSCInt(strLottoxA) <> 999999999 Then
          strSQL += " AND km_lotto BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      Else
        If strLottoxDa.Replace("0", "").Trim <> "" Or strLottoxA.Replace("999999999", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz").Trim <> "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz" Then
          strSQL += " AND analotti.alo_lottox BETWEEN " & CStrSQL(strLottoxDa) & " AND " & CStrSQL(strLottoxA)
        End If
      End If
      If (NTSCDate(strDatini) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strDatfin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND km_aammgg BETWEEN " & strDatini & " AND " & strDatfin
      End If
      If (nScarDamagaz <> 0) Or (nScarAmagaz <> 9999) Then
        strSQL += " AND km_magaz BETWEEN " & nScarDamagaz & " AND " & nScarAmagaz
      End If
      If (lScarDalotto <> 0) Or (lScarAlotto <> 999999999) Then
        strSQL += " AND km_lotto BETWEEN " & lScarDalotto & " AND " & lScarAlotto
      End If
      If (lScarDacomme <> 0) Or (lScarAcomme <> 999999999) Then
        strSQL += " AND km_commeca BETWEEN " & lScarDacomme & " AND " & lScarAcomme
      End If
      Select Case strCodart
        Case "A"
          If (strScarDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or _
             (strScarAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "Z"c)) Then
            strSQL += " AND km_codart BETWEEN '" & strScarDacodart & "' AND '" & strScarAcodart & "'"
          End If
          If (nScarFaseini <> 0) Or (nScarFasefin <> 9999) Then
            strSQL += " AND km_fase BETWEEN " & nScarFaseini & " AND " & nScarFasefin
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL += _
              " AND CAST(km_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(km_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      Select Case strConto
        Case "A"
          If (lScarDaconto <> 0) Or (lScarAconto <> 999999999) Then
            strSQL += " AND km_conto BETWEEN " & lScarDaconto & " AND " & lScarAconto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND km_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      If (bModuloCRM = True) And (bIsCRMUser = True) Then
        strSQL += " AND ("
        If strAccvis <> "T" Then
          strSQL += "(an_tipo = 'C' AND an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                                                   " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                                                   " AND leads.le_coddest = 0"
          Select Case strAccvis
            Case "P" : strSQL += " AND le_opinc = " & lCodorgaOperat
            Case "C" : strSQL += " AND le_opinc IN (" & strRegvis & ")"
          End Select
          strSQL += "))"
        Else
          strSQL += " anagra.an_tipo = 'C'"
        End If
        If bAmm = False Then strSQL += " AND an_tipo <> 'F'" Else strSQL += " OR an_tipo <> 'C'"
        strSQL += ")"
      End If
      TraduciWhere(Replace(strQuery, "movmag", "movprb"), strSQL)
      Select Case strScarOrdin
        Case "A" : strSQL += " GROUP BY km_codart, km_fase, km_magaz, mma_matric" & _
                             " ORDER BY km_codart, km_fase, km_magaz, mma_matric"
        Case "C" : strSQL += " GROUP BY km_conto, km_codart, km_fase, km_magaz, mma_matric" & _
                             " ORDER BY km_conto, km_codart, km_fase , km_magaz, mma_matric"
      End Select
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVPRB")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function GrmaGetArtpro(ByVal strDitta As String, ByVal dtrTmp As DataRow, _
                                          ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strDitta = "SELECT Sum(mma_quant*tb_esist) AS Somma" & _
        " FROM tabcaum INNER JOIN ((movmag INNER JOIN movmatr ON (movmag.codditt = movmatr.codditt) AND (movmag.mm_tipork = movmatr.mma_tipork)" & _
        " AND (movmag.mm_anno = movmatr.mma_anno) AND (movmag.mm_serie = movmatr.mma_serie) AND (movmag.mm_numdoc = movmatr.mma_numdoc) AND" & _
        " (movmag.mm_riga = movmatr.mma_riga)) INNER JOIN keymag ON (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) AND" & _
        " (movmag.mm_tipork = keymag.km_tipork) AND (movmag.mm_serie = keymag.km_serie) AND (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc))" & _
        " ON tabcaum.tb_codcaum = keymag.km_causale" & _
        " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
        " AND movmag.mm_tipork <> 'W'" & _
        " AND movmatr.mma_matric = " & CStrSQL(dtrTmp!mma_matric)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GrloApri(ByVal strDitta As String, ByVal nScarOrdin As Integer, _
                                        ByVal strTTStloco As String, ByVal strTTStlocs As String, _
                                        ByVal lIITTStloco As Integer, ByVal lIITTStlocs As Integer, _
                                        ByVal lIITTStlocu As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      Select Case nScarOrdin
        Case 3, 4, 6, 7
          strSQL = "SELECT tt_codart, tt_fase, tt_magaz FROM " & strTTStloco & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStloco & _
            " GROUP BY tt_codart, tt_fase, tt_magaz" & _
            " ORDER BY tt_codart, tt_fase, tt_magaz"
        Case 5, 8, 14 ' anche saldo ubicazioni...
          strSQL = "SELECT tt_codart, tt_fase, tt_magaz FROM " & strTTStlocs & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStlocs & _
            " GROUP BY tt_codart, tt_fase, tt_magaz" & _
            " ORDER BY tt_codart, tt_fase, tt_magaz"
        Case 12, 13 ' solo per ubicaz. aperte e chiuse e ubic. aperte
          strSQL = "SELECT tt_codart, tt_fase, tt_magaz FROM TTSTLOCU" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStlocu & _
            " GROUP BY tt_codart, tt_fase, tt_magaz" & _
            " ORDER BY tt_codart, tt_fase, tt_magaz"
      End Select

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GrloComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                                 ByVal nScarOrdin As Integer, _
                                                 ByVal strTTStloco As String, ByVal strTTStlocs As String, _
                                                 ByVal lIITTStloco As Integer, ByVal lIITTStlocs As Integer, _
                                                 ByVal lIITTStlocu As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      Select Case nScarOrdin
        ' non sono gestiti qui i casi ubicazione ... (o per ora almeno..)
        'LOTTI/COMMESSE APERTI E CHIUSI - A LOTTI/COMMESSE APERTI
        Case 3, 4
          strSQL = "SELECT " & strTTStloco & ".*, alo_descr as xx_deslotto, an_descr1 As xx_clfor, " & _
            " analotti.alo_lottox as xx_lottox " & _
            " FROM (" & strTTStloco & " LEFT JOIN analotti ON " & strTTStloco & ".codditt = analotti.codditt AND " & strTTStloco & ".tt_codart = analotti.alo_codart AND " & strTTStloco & ".tt_lotto = analotti.alo_lotto)" & _
            " LEFT JOIN anagra ON " & strTTStloco & ".codditt = anagra.codditt AND " & strTTStloco & ".tt_conto = anagra.an_conto" & _
            " WHERE " & strTTStloco & ".codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStloco & _
            " AND tt_codart = " & CStrSQL(dtrTmp!tt_codart) & _
            " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
            " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
            " ORDER BY tt_codart, tt_fase, tt_magaz, tt_lotto, tt_aammgg, tt_tipork," & _
            " tt_anno, tt_serie, tt_numdoc, tt_riga"
          ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")
          ds.Tables("TTLOTTI").Columns.Add(New DataColumn("tt_ubicaz", GetType(String)))
        Case Is = 6, 7
          strSQL = "SELECT " & strTTStloco & ".*, an_descr1 As xx_clfor," & _
            " CAST(tt_lotto as VarChar(10)) AS xx_lottox" & _
            " FROM " & strTTStloco & _
            " LEFT JOIN anagra ON " & strTTStloco & ".codditt = anagra.codditt AND " & strTTStloco & ".tt_conto = anagra.an_conto" & _
            " WHERE " & strTTStloco & ".codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStloco & _
            " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
            " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
            " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
            " ORDER BY tt_codart, tt_fase, tt_magaz, tt_lotto, tt_aammgg, tt_tipork," & _
            " tt_anno, tt_serie, tt_numdoc, tt_riga"
          'SALDO LOTTI APERTI - SALDO COMMESSE APERTE -  esaldo ubicazioni aperteù
          ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")
          ds.Tables("TTLOTTI").Columns.Add(New DataColumn("tt_ubicaz", GetType(String)))
          ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_deslotto", GetType(String)))
        Case 5
          strSQL = "SELECT " & strTTStlocs & ".*, alo_descr as xx_deslotto, '' As xx_clfor, " & _
            " analotti.alo_lottox as xx_lottox " & _
            " FROM " & strTTStlocs & " LEFT JOIN analotti ON " & strTTStlocs & ".codditt = analotti.codditt AND " & strTTStlocs & ".tt_codart = analotti.alo_codart AND " & strTTStlocs & ".tt_lotto = analotti.alo_lotto" & _
            " WHERE " & strTTStlocs & ".codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStlocs & _
            " AND tt_codart = " & CStrSQL(dtrTmp!tt_codart) & _
            " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
            " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
            " ORDER BY tt_codart, tt_fase, tt_magaz, tt_lotto, tt_ubicaz"
          ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")
        Case Is = 8, 14
          strSQL = "SELECT " & strTTStlocs & ".*, '' As xx_clfor, '' as xx_lottox" & _
            " FROM " & strTTStlocs & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStlocs & _
            " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
            " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
            " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
            " ORDER BY tt_codart, tt_fase, tt_magaz, tt_lotto, tt_ubicaz"
          'solo UBICAZIONI APERTI E CHIUSI - e SOLO UBIC. APERTE
          ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")
          ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_deslotto", GetType(String)))
        Case Is = 12, 13
          strSQL = "SELECT TTSTLOCU.*, an_descr1 As xx_clfor, '' as xx_lottox" & _
            " FROM TTSTLOCU LEFT JOIN anagra ON TTSTLOCU.codditt = anagra.codditt AND TTSTLOCU.tt_conto = anagra.an_conto" & _
            " WHERE TTSTLOCU.codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStlocu & _
            " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
            " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
            " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
            " ORDER BY tt_codart, tt_fase, tt_magaz, tt_ubicaz, tt_aammgg, tt_tipork," & _
            " tt_anno, tt_serie, tt_numdoc, tt_riga"
          ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")
          ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_deslotto", GetType(String)))
      End Select

      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_descr", GetType(String)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_carichi", GetType(Decimal)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_scarichi", GetType(Decimal)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_valore", GetType(Decimal)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_desforn", GetType(String)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_rimlotto", GetType(Decimal)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_costomedio", GetType(Decimal)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_vallotto", GetType(Decimal)))
      'ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_clfor", GetType(String)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_datcarico", GetType(Date)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_tipodoc", GetType(String)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_numero", GetType(Integer)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_serie", GetType(String)))
      ds.Tables("TTLOTTI").Columns.Add(New DataColumn("xx_forn", GetType(Integer)))

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GrloGetAnalotti(ByVal strDitta As String, ByVal strCodart As String, _
                                       ByVal strLotto As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT alo_descr FROM analotti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND alo_codart = '" & strCodart & "'" & _
        " AND alo_lotto = " & strLotto

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ANALOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GrloGetRimLotto(ByVal strDitta As String, ByVal strTTStloco As String, _
                                               ByVal lIITTStloco As Integer, ByVal dtrTmp As DataRow, _
                                               ByVal lLotto As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Sum(tt_quant * tt_carscar)As Saldo FROM " & strTTStloco & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStloco & _
        " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
        " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
        " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
        " AND tt_lotto = " & lLotto

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrloGetRimLotto2(ByVal strDitta As String, ByVal lIITTStlocu As Integer, _
                                               ByVal dtrTmp As DataRow, ByVal strUbicaz As String, _
                                               ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Sum(tt_quant * tt_carscar)As Saldo FROM TTStlocu" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStlocu & _
        " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
        " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
        " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
        " AND tt_ubicaz = '" & strUbicaz & "'"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GrloGetCostoMedio(ByVal strDitta As String, ByVal strTTStloco As String, _
                                             ByVal lIITTStloco As Integer, _
                                             ByVal dtrTmp As DataRow, ByVal lLotto As Integer, _
                                             ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT (tt_valcar / tt_qtacar)As Valsaldo FROM " & strTTStloco & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStloco & _
        " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
        " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
        " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
        " AND tt_lotto = " & lLotto & _
        " AND tt_qtacar <> 0"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrloGetCostoMedio2(ByVal strDitta As String, ByVal lIITTStlocu As Integer, _
                                               ByVal dtrTmp As DataRow, ByVal strUbicaz As String, _
                                               ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT (tt_valcar / tt_qtacar)As Valsaldo FROM TTSTLOCU" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStlocu & _
        " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
        " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
        " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
        " AND tt_ubicaz = '" & strUbicaz & "'" & _
        " AND tt_qtacar <> 0"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GrloGetValLotto(ByVal strDitta As String, ByVal strTTStloco As String, _
                                           ByVal lIITTStloco As Integer, _
                                           ByVal dtrTmp As DataRow, ByVal lLotto As Integer, _
                                           ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tt_valcar, tt_qtacar, Sum(tt_quant * tt_carscar)As Quant" & _
        " FROM " & strTTStloco & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStloco & _
        " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
        " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
        " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
        " AND tt_lotto = " & lLotto & _
        " GROUP BY tt_codart, tt_fase, tt_magaz, tt_lotto, tt_valcar, tt_qtacar" & _
        " ORDER BY tt_codart, tt_fase, tt_magaz, tt_lotto"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrloGetValLotto2(ByVal strDitta As String, ByVal lIITTStlocu As Integer, _
                                               ByVal dtrTmp As DataRow, ByVal strUbicaz As String, _
                                               ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tt_valcar, tt_qtacar, Sum(tt_quant * tt_carscar)As Quant" & _
        " FROM TTStlocu" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStlocu & _
        " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
        " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
        " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
        " AND tt_ubicaz = '" & strUbicaz & "' " & _
        " GROUP BY tt_codart, tt_fase, tt_magaz, tt_ubicaz, tt_valcar, tt_qtacar" & _
        " ORDER BY tt_codart, tt_fase, tt_magaz, tt_ubicaz"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GrloTotCostoUnitario(ByVal strDitta As String, ByVal strTTStloco As String, _
                                                   ByVal lIITTStloco As Integer, _
                                                   ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Sum(tt_prezzo * tt_quant) AS Tot1, Sum(tt_quant) As Qta1" & _
        " FROM " & strTTStloco & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStloco & _
        " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
        " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
        " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
        " AND tt_carscar = 1" & _
        " GROUP BY tt_codart, tt_fase, tt_magaz" & _
        " ORDER BY tt_codart, tt_fase, tt_magaz"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrloTotCostoUnitario2(ByVal strDitta As String, ByVal strTTStlocs As String, _
                                       ByVal lIITTStlocs As Integer, _
                                       ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Sum(tt_qtacar) As Qtacar, Sum(tt_valcar) As Valcar," & _
        " Sum(tt_quant) As Quant" & _
        " FROM " & strTTStlocs & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStlocs & _
        " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
        " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
        " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
        " GROUP BY tt_codart, tt_fase, tt_magaz" & _
        " ORDER BY tt_codart, tt_fase, tt_magaz"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrloTotCostoUnitario3(ByVal strDitta As String, ByVal lIITTStlocu As Integer, _
                                       ByVal dtrTmp As DataRow, _
                                       ByRef ds As DataSet, ByRef ds2 As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT (Sum(tt_prezzo * tt_quant)) As Prezzoun," & _
        " Sum(tt_prezzo * tt_quant) As Valoreun, Sum(tt_quant) As Quantita" & _
        " FROM TTSTLOCU " & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStlocu & _
        " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
        " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
        " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
        " AND tt_carscar = 1" & _
        " GROUP BY tt_codart, tt_fase, tt_magaz" & _
        " ORDER BY tt_codart, tt_fase, tt_magaz"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      strSQL = "SELECT tt_ubicaz FROM TTSTLOCU" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStlocu & _
        " AND tt_codart = '" & NTSCStr(dtrTmp!tt_codart) & "'" & _
        " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
        " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
        " GROUP  BY tt_ubicaz, tt_codart, tt_fase, tt_magaz" & _
        " HAVING Sum(tt_quant * tt_carscar) = 0 ORDER BY tt_ubicaz"

      ds2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function GrloTotCostoUnitario4(ByVal strDitta As String, ByVal lIITTStlocu As Integer, _
                                     ByVal dtrTmp As DataRow, ByVal dtrTmp1 As DataRow, _
                                     ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT sum (tt_quant*tt_prezzo) as somma FROM TTStlocu" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTStlocu & _
        " AND tt_ubicaz = '" & NTSCStr(dtrTmp1!tt_ubicaz) & "'" & _
        " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
        " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase) & _
        " AND tt_carscar = 1" & _
        " GROUP BY tt_ubicaz, tt_codart, tt_fase, tt_magaz"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GrloTotRimanenzeArticoli(ByVal strDitta As String, ByVal nScarOrdin As Integer, _
                                                        ByVal strTTStloco As String, ByVal lIITTStloco As Integer, _
                                                        ByVal strTTStlocs As String, ByVal lIITTStlocs As Integer, _
                                                        ByVal lIITTStlocu As Integer, _
                                                        ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      Select Case nScarOrdin
        'LOTTI/COMMESSE APERTI E CHIUSI - A LOTTI/APERTI APERTI
        Case 3, 4, 6, 7
          strSQL = "SELECT Sum(tt_quant * tt_carscar)As Somma" & _
            " FROM " & strTTStloco & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStloco & _
            " AND tt_codart = " & CStrSQL(dtrTmp!tt_codart) & _
            " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
            " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase)
          'SALDO LOTTI/COMMESSE APERTE e ubicazioni saldo
        Case 5, 8, 14
          strSQL = "SELECT Sum(tt_quant)As Somma" & _
            " FROM " & strTTStlocs & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStlocs & _
            " AND tt_codart = " & CStrSQL(dtrTmp!tt_codart) & _
            " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
            " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase)
          'ubicazioni APERTI E CHIUSI -  - ubicazioni APERTE
        Case 12, 13
          strSQL = "SELECT Sum(tt_quant * tt_carscar)As Somma" & _
            " FROM TTSTLOCU " & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStlocu & _
            " AND tt_codart = " & CStrSQL(dtrTmp!tt_codart) & _
            " AND tt_magaz = " & NTSCStr(dtrTmp!tt_magaz) & _
            " AND tt_fase = " & NTSCStr(dtrTmp!tt_fase)
      End Select

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTLOTTI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckEsistCollaudo(ByVal strDitta As String, ByVal strTipork As String, _
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
      If dttTmp.Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function SoloNonMovimentati(ByVal strDitta As String, ByVal bNoteDiPrelievo As Boolean, _
    ByVal strNonMovimentati As String, ByVal bPerMagazzino As Boolean, _
    ByVal bTTSTSCHEA As Boolean, ByVal bTTSTMATR As Boolean, ByVal bTTSTLOCO As Boolean, _
    ByVal lIITTStschea As Integer, ByVal lIITTStMatr As Integer, ByVal lIITTStMats As Integer, _
    ByVal lIITTStloco As Integer, ByVal lIITTStlocs As Integer, ByVal lIITTStlocu As Integer) As Boolean
    Dim bOk As Boolean = False
    Dim strSQL As String = ""
    Dim strTabellaKey As String = IIf(bNoteDiPrelievo = True, "keyprb", "keymag").ToString
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If bTTSTSCHEA = True Then
        strSQL = "DELETE FROM TTSTSCHEA" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTStschea & _
          " AND tt_codart + CAST(tt_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(tt_codmaga AS VARCHAR(4))", "").ToString & _
          " IN (SELECT km_codart + CAST(km_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(km_magaz AS VARCHAR(4))", "").ToString & _
          " FROM " & strTabellaKey & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND km_aammgg > " & CDataSQL(strNonMovimentati) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        If bOk = False Then
          strSQL = "SELECT TOP 1 codditt FROM TTSTSCHEA" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStschea
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then bOk = True
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bTTSTMATR = True Then
        strSQL = "DELETE FROM TTSTMATR" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTStMatr & _
          " AND tt_codart + CAST(tt_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(tt_magaz AS VARCHAR(4))", "").ToString & _
          " IN (SELECT km_codart + CAST(km_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(km_magaz AS VARCHAR(4))", "").ToString & _
          " FROM " & strTabellaKey & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND km_aammgg > " & CDataSQL(strNonMovimentati) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        If bOk = False Then
          strSQL = "SELECT TOP 1 codditt FROM TTSTMATR" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStMatr
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then bOk = True
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        '------------------------------------------------------------------------------------------------------------
        strSQL = "DELETE FROM TTSTMATS" & _
          " FROM " & strTabellaKey & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTStMats & _
          " AND tt_codart + CAST(tt_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(tt_magaz AS VARCHAR(4))", "").ToString & _
          " IN (SELECT km_codart + CAST(km_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(km_magaz AS VARCHAR(4))", "").ToString & _
          " FROM " & strTabellaKey & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND km_aammgg > " & CDataSQL(strNonMovimentati) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        If bOk = False Then
          strSQL = "SELECT TOP 1 codditt FROM TTSTMATS" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStMats
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then bOk = True
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bTTSTLOCO = True Then
        strSQL = "DELETE FROM TTSTLOCO" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTStloco & _
          " AND tt_codart + CAST(tt_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(tt_magaz AS VARCHAR(4))", "").ToString & _
          " IN (SELECT km_codart + CAST(km_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(km_magaz AS VARCHAR(4))", "").ToString & _
          " FROM " & strTabellaKey & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND km_aammgg > " & CDataSQL(strNonMovimentati) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        If bOk = False Then
          strSQL = "SELECT TOP 1 codditt FROM TTSTLOCO" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStloco
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then bOk = True
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        '------------------------------------------------------------------------------------------------------------
        strSQL = "DELETE FROM TTSTLOCS" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTStlocs & _
          " AND tt_codart + CAST(tt_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(tt_magaz AS VARCHAR(4))", "").ToString & _
          " IN (SELECT km_codart + CAST(km_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(km_magaz AS VARCHAR(4))", "").ToString & _
          " FROM " & strTabellaKey & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND km_aammgg > " & CDataSQL(strNonMovimentati) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        If bOk = False Then
          strSQL = "SELECT TOP 1 codditt FROM TTSTLOCS" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStlocs
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then bOk = True
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
        '------------------------------------------------------------------------------------------------------------
        strSQL = "DELETE FROM TTSTLOCU" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITTStlocu & _
          " AND tt_codart + CAST(tt_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(tt_magaz AS VARCHAR(4))", "").ToString & _
          " IN (SELECT km_codart + CAST(km_fase AS VARCHAR(4))" & IIf(bPerMagazzino = True, " + CAST(km_magaz AS VARCHAR(4))", "").ToString & _
          " FROM " & strTabellaKey & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND km_aammgg > " & CDataSQL(strNonMovimentati) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        If bOk = False Then
          strSQL = "SELECT TOP 1 codditt FROM TTSTLOCU" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTStlocu
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then bOk = True
          dttTmp.Clear()
          dttTmp.Dispose()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return bOk
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function RitornaLISTSAR(ByVal strDitta As String, ByVal nCodlsar As Integer, _
    ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT lsa_codart, lsa_fase, 0 AS progressivo FROM listsar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lsa_codlsar = " & nCodlsar & _
        " ORDER BY lsa_codlsar, lsa_fase"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttOut.Rows.Count = 0 Then
        dttOut.Clear()
        dttOut.Dispose()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dttOut.Rows.Count - 1)
        dttOut.Rows(i)!progressivo = (i + 1)
      Next
      dttOut.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RitornaLISTSEL(ByVal strDitta As String, ByVal nCodlsel As Integer, _
    ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT lse_conto, 0 AS progressivo FROM listsel" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lse_codlsel = " & nCodlsel & _
        " ORDER BY lse_conto"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttOut.Rows.Count = 0 Then
        dttOut.Clear()
        dttOut.Dispose()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dttOut.Rows.Count - 1)
        dttOut.Rows(i)!progressivo = (i + 1)
      Next
      dttOut.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

#Region "Filtri"
  Public Overridable Function CaricaFiltri(ByVal strDitta As String, ByVal strChild As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_codifil AS cod, tb_desifil AS val FROM tabifil " & _
               " WHERE tb_child = " & CStrSQL(strChild) & _
               " AND (tb_filtriut = ' ' " & _
                     " OR (tb_filtriut = 'S' AND ';' + REPLACE(tb_users, ' ', '') + ';' LIKE " & CStrSQL("%;" & oApp.User.Nome & ";%") & ") " & _
                     " OR (tb_filtriut = 'N' AND ';' + REPLACE(tb_users, ' ', '') + ';' NOT LIKE " & CStrSQL("%;" & oApp.User.Nome & ";%") & ")) " & _
               " AND (tb_filtriditta = ' ' " & _
                     " OR (tb_filtriditta = 'S' AND ';' + REPLACE(tb_ditte, ' ', '') + ';' LIKE " & CStrSQL("%;" & strDitta & ";%") & ") " & _
                     " OR (tb_filtriditta = 'N' AND ';' + REPLACE(tb_ditte, ' ', '') + ';' NOT LIKE " & CStrSQL("%;" & strDitta & ";%") & ")) "

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiFiltro(ByVal lCod As Integer, ByVal strChild As String, ByVal strForm As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movifil.* FROM movifil " & _
               " WHERE mo_codifil = " & lCod & _
               " AND mo_child = " & CStrSQL(strChild) & _
               " AND mo_form = " & CStrSQL(strForm) & _
               " ORDER BY mo_ordin"


      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------------------------
    End Try
  End Function
#End Region

End Class
