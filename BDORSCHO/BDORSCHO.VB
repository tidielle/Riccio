Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDORSCHO
  Inherits CLD__BASE

  Public Overridable Function AggiornaRilasci(ByVal strDitta As String, _
                                              ByVal strDacodart As String, ByVal strAcodart As String, _
                                              ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                              ByVal stredDaagente As String, ByVal stredAagente As String, _
                                              ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                              ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                              ByVal stredDaconto As String, ByVal stredAconto As String, _
                                              ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                              ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                              ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                              ByVal stredSottogr As String, ByVal stredSerie As String, _
                                              ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                              ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                              ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                              ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                              ByVal strQuery As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return AggiornaRilasci(strDitta, strDacodart, strAcodart, stredDamagaz, stredAmagaz, stredDaagente, stredAagente, _
                             stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                             stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, bckEvasi, stredGruppo, _
                             stredSottogr, stredSerie, bcbTipork, strTipork, strcbRilasciato, stredCodcfam, _
                             bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function AggiornaRilasci(ByVal strDitta As String, _
                                              ByVal strDacodart As String, ByVal strAcodart As String, _
                                              ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                              ByVal stredDaagente As String, ByVal stredAagente As String, _
                                              ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                              ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                              ByVal stredDaconto As String, ByVal stredAconto As String, _
                                              ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                              ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                              ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                              ByVal stredSottogr As String, ByVal stredSerie As String, _
                                              ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                              ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                              ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                              ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                              ByVal strQuery As String, _
                                              ByVal strClassificazioneLivello1 As String, _
                                              ByVal strClassificazioneLivello2 As String, _
                                              ByVal strClassificazioneLivello3 As String, _
                                              ByVal strClassificazioneLivello4 As String, _
                                              ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDacodart, strAcodart, _
                             stredDamagaz, stredAmagaz, stredDaagente, stredAagente, _
                             stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, _
                             stredDaconto, stredAconto, stredCommecaini, stredCommecafin, _
                             stredFaseini, stredFasefin, bckEvasi, stredGruppo, _
                             stredSottogr, stredSerie, bcbTipork, strTipork, _
                             strcbRilasciato, stredCodcfam, bModTCO, bckSelAnnoStag, _
                             stredAnnotco, stredCodstag, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                             strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return AggiornaRilasci(strDitta, strDacodart, strAcodart, stredDamagaz, stredAmagaz, stredDaagente, stredAagente, _
                             stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                             stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, bckEvasi, stredGruppo, _
                             stredSottogr, stredSerie, bcbTipork, strTipork, strcbRilasciato, stredCodcfam, _
                             bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, strQuery, _
                             strClassificazioneLivello1, strClassificazioneLivello2, strClassificazioneLivello3, _
                             strClassificazioneLivello4, strClassificazioneLivello5, "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function AggiornaRilasci(ByVal strDitta As String, _
                                              ByVal strDacodart As String, ByVal strAcodart As String, _
                                              ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                              ByVal stredDaagente As String, ByVal stredAagente As String, _
                                              ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                              ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                              ByVal stredDaconto As String, ByVal stredAconto As String, _
                                              ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                              ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                              ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                              ByVal stredSottogr As String, ByVal stredSerie As String, _
                                              ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                              ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                              ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                              ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                              ByVal strQuery As String, _
                                              ByVal strClassificazioneLivello1 As String, _
                                              ByVal strClassificazioneLivello2 As String, _
                                              ByVal strClassificazioneLivello3 As String, _
                                              ByVal strClassificazioneLivello4 As String, _
                                              ByVal strClassificazioneLivello5 As String, _
                                              ByVal strConto As String, ByVal nCodlsel As Integer, _
                                              ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDacodart, strAcodart, _
                             stredDamagaz, stredAmagaz, stredDaagente, stredAagente, _
                             stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, _
                             stredDaconto, stredAconto, stredCommecaini, stredCommecafin, _
                             stredFaseini, stredFasefin, bckEvasi, stredGruppo, _
                             stredSottogr, stredSerie, bcbTipork, strTipork, _
                             strcbRilasciato, stredCodcfam, bModTCO, bckSelAnnoStag, _
                             stredAnnotco, stredCodstag, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                             strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE movord" & _
        " SET mo_rilasciato = 'S'" & _
        " FROM testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_numord = movord.mo_numord AND testord.td_serie = movord.mo_serie AND testord.td_anno = movord.mo_anno AND testord.td_tipork = movord.mo_tipork" & _
        " INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_anno = keyord.ko_anno AND movord.mo_riga = keyord.ko_riga AND movord.mo_numord = keyord.ko_numord AND movord.mo_serie = keyord.ko_serie AND movord.mo_tipork = keyord.ko_tipork" & _
        " INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart" & _
        " WHERE keyord.codditt = " & CStrSQL(strDitta) & _
        IIf(bckEvasi = True, " and movord.mo_flevas = 'C'", "").ToString & _
        IIf(NTSCInt(stredGruppo) <> 0, " AND artico.ar_gruppo = " & stredGruppo, "").ToString & _
        IIf(NTSCInt(stredSottogr) <> 0, " AND artico.ar_sotgru = " & stredSottogr, "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
        IIf(stredSerie <> "", " AND keyord.ko_serie = " & CStrSQL(stredSerie), "").ToString & _
        IIf(bcbTipork = True, " AND keyord.ko_tipork = " & CStrSQL(strTipork), "").ToString & _
        IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString
      If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
        strSQL += " AND keyord.ko_magaz BETWEEN " & stredDamagaz & " AND " & stredAmagaz
      End If
      If (NTSCInt(stredDaagente) <> 0) Or (NTSCInt(stredAagente) <> 9999) Then
        strSQL += " AND testord.td_codagen BETWEEN " & stredDaagente & " AND " & stredAagente
      End If
      If (NTSCInt(stredCommecaini) <> 0) Or (NTSCInt(stredCommecafin) <> 999999999) Then
        strSQL += " AND movord.mo_commeca BETWEEN " & stredCommecaini & " AND " & stredCommecafin
      End If
      If (NTSCDate(stredDadatord) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(stredAdatord) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND testord.td_datord BETWEEN " & CDataSQL(stredDadatord) & " AND " & CDataSQL(stredAdatord)
      End If
      If (NTSCDate(stredDadatcons) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(stredAdatcons) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND keyord.ko_datcons BETWEEN " & CDataSQL(stredDadatcons) & " AND " & CDataSQL(stredAdatcons)
      End If
      Select Case strConto
        Case "A"
          If (NTSCInt(stredDaconto) <> 0) Or (NTSCInt(stredAconto) <> 999999999) Then
            strSQL += " AND keyord.ko_conto BETWEEN " & stredDaconto & " AND " & stredAconto
          End If
        Case "B"
      End Select
      Select Case strCodart
        Case "A"
          If (strDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or (strAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strSQL += " AND keyord.ko_codart BETWEEN '" & strDacodart & "' AND '" & strAcodart & "'"
          End If
          If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
            strSQL = strSQL & " AND movord.mo_fase BETWEEN " & stredFaseini & " AND " & stredFasefin
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL += _
              " AND CAST(ko_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(mo_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      Select Case strcbRilasciato
        Case "N" : strSQL += " AND movord.mo_rilasciato = 'N'"
        Case "R" : strSQL += " AND movord.mo_rilasciato = 'S'"
      End Select
      If (bModTCO = True) And (bckSelAnnoStag) Then
        If NTSCInt(stredAnnotco) > 0 Then strSQL += " AND artico.ar_anno = " & stredAnnotco
        If NTSCInt(stredCodstag) > 0 Then strSQL += " AND artico.ar_codstag = " & stredCodstag
      End If
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery, strSQL)
      '--------------------------------------------------------------------------------------------------------------
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function CheckTmpTable(ByVal strDitta As String, ByVal lIITTScho As Integer, _
                                            ByRef ds As DataSet) As Boolean
    Dim strSQL As String
    Try
      strSQL = "SELECT TOP 1 instid FROM TTSCHO" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTScho & _
        " ORDER BY instid, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_olprogr, mo_magaz"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTSCHO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function Elabora(ByVal strDitta As String, _
                                      ByVal strDacodart As String, ByVal strAcodart As String, _
                                      ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                      ByVal stredDaagente As String, ByVal stredAagente As String, _
                                      ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                      ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                      ByVal stredDaconto As String, ByVal stredAconto As String, _
                                      ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                      ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                      ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                      ByVal stredSottogr As String, ByVal stredSerie As String, _
                                      ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                      ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                      ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                      ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                      ByVal bModuloCRM As Boolean, ByVal bIsCRMUser As Boolean, _
                                      ByVal strAccvis As String, ByVal bAmm As Boolean, _
                                      ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                      ByVal lIITTScho As Integer, ByVal bckMovord As Boolean, _
                                      ByVal bModAS As Boolean, ByVal bckMovord4 As Boolean, _
                                      ByVal bckMovord1 As Boolean, ByVal bckMovord2 As Boolean, _
                                      ByVal bckMovord3 As Boolean, ByVal bckOrdlist As Boolean, _
                                      ByVal bckZzdispsca As Boolean, ByVal bckTaskPM As Boolean, _
                                      ByVal bckSolotaskril As Boolean, ByVal nMagazInternoTransito As Integer, _
                                      ByVal strQuery As String) As Boolean

    Try
      '--------------------------------------------------------------------------------------------------------------
      Return Elabora(strDitta, strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                     stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                     stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                     stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                     bckEvasi, stredGruppo, stredSottogr, stredSerie, _
                     bcbTipork, strTipork, strcbRilasciato, stredCodcfam, _
                     bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                     bModuloCRM, bIsCRMUser, strAccvis, bAmm, _
                     lCodorgaOperat, strRegvis, lIITTScho, bckMovord, _
                     bModAS, bckMovord4, bckMovord1, bckMovord2, _
                     bckMovord3, bckOrdlist, bckZzdispsca, bckTaskPM, _
                     bckSolotaskril, nMagazInternoTransito, strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Elabora(ByVal strDitta As String, _
                                      ByVal strDacodart As String, ByVal strAcodart As String, _
                                      ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                      ByVal stredDaagente As String, ByVal stredAagente As String, _
                                      ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                      ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                      ByVal stredDaconto As String, ByVal stredAconto As String, _
                                      ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                      ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                      ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                      ByVal stredSottogr As String, ByVal stredSerie As String, _
                                      ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                      ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                      ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                      ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                      ByVal bModuloCRM As Boolean, ByVal bIsCRMUser As Boolean, _
                                      ByVal strAccvis As String, ByVal bAmm As Boolean, _
                                      ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                      ByVal lIITTScho As Integer, ByVal bckMovord As Boolean, _
                                      ByVal bModAS As Boolean, ByVal bckMovord4 As Boolean, _
                                      ByVal bckMovord1 As Boolean, ByVal bckMovord2 As Boolean, _
                                      ByVal bckMovord3 As Boolean, ByVal bckOrdlist As Boolean, _
                                      ByVal bckZzdispsca As Boolean, ByVal bckTaskPM As Boolean, _
                                      ByVal bckSolotaskril As Boolean, ByVal nMagazInternoTransito As Integer, _
                                      ByVal strQuery As String, _
                                      ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                      ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                      ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                               stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                               stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                               stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                               bckEvasi, stredGruppo, stredSottogr, stredSerie, _
                               bcbTipork, strTipork, strcbRilasciato, stredCodcfam, _
                               bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                               bModuloCRM, bIsCRMUser, strAccvis, bAmm, _
                               lCodorgaOperat, strRegvis, lIITTScho, bckMovord, _
                               bModAS, bckMovord4, bckMovord1, bckMovord2, _
                               bckMovord3, bckOrdlist, bckZzdispsca, bckTaskPM, _
                               bckSolotaskril, nMagazInternoTransito, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                               strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return Elabora(strDitta, strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                     stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                     stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                     stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                     bckEvasi, stredGruppo, stredSottogr, stredSerie, _
                     bcbTipork, strTipork, strcbRilasciato, stredCodcfam, _
                     bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                     bModuloCRM, bIsCRMUser, strAccvis, bAmm, _
                     lCodorgaOperat, strRegvis, lIITTScho, bckMovord, _
                     bModAS, bckMovord4, bckMovord1, bckMovord2, _
                     bckMovord3, bckOrdlist, bckZzdispsca, bckTaskPM, _
                     bckSolotaskril, nMagazInternoTransito, strQuery, _
                     strClassificazioneLivello1, strClassificazioneLivello2, _
                     strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                     "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Elabora(ByVal strDitta As String, _
                                      ByVal strDacodart As String, ByVal strAcodart As String, _
                                      ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                      ByVal stredDaagente As String, ByVal stredAagente As String, _
                                      ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                      ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                      ByVal stredDaconto As String, ByVal stredAconto As String, _
                                      ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                      ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                      ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                      ByVal stredSottogr As String, ByVal stredSerie As String, _
                                      ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                      ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                      ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                      ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                      ByVal bModuloCRM As Boolean, ByVal bIsCRMUser As Boolean, _
                                      ByVal strAccvis As String, ByVal bAmm As Boolean, _
                                      ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                      ByVal lIITTScho As Integer, ByVal bckMovord As Boolean, _
                                      ByVal bModAS As Boolean, ByVal bckMovord4 As Boolean, _
                                      ByVal bckMovord1 As Boolean, ByVal bckMovord2 As Boolean, _
                                      ByVal bckMovord3 As Boolean, ByVal bckOrdlist As Boolean, _
                                      ByVal bckZzdispsca As Boolean, ByVal bckTaskPM As Boolean, _
                                      ByVal bckSolotaskril As Boolean, ByVal nMagazInternoTransito As Integer, _
                                      ByVal strQuery As String, _
                                      ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                      ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                      ByVal strClassificazioneLivello5 As String, _
                                      ByVal strConto As String, ByVal nCodlsel As Integer, _
                                      ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                               stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                               stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                               stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                               bckEvasi, stredGruppo, stredSottogr, stredSerie, _
                               bcbTipork, strTipork, strcbRilasciato, stredCodcfam, _
                               bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                               bModuloCRM, bIsCRMUser, strAccvis, bAmm, _
                               lCodorgaOperat, strRegvis, lIITTScho, bckMovord, _
                               bModAS, bckMovord4, bckMovord1, bckMovord2, _
                               bckMovord3, bckOrdlist, bckZzdispsca, bckTaskPM, _
                               bckSolotaskril, nMagazInternoTransito, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                               strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, strConto, nCodlsel, strCodart, nCodlsar})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return Elabora(strDitta, strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                     stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                     stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                     stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                     bckEvasi, stredGruppo, stredSottogr, stredSerie, _
                     bcbTipork, strTipork, strcbRilasciato, stredCodcfam, _
                     bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                     bModuloCRM, bIsCRMUser, strAccvis, bAmm, _
                     lCodorgaOperat, strRegvis, lIITTScho, bckMovord, _
                     bModAS, bckMovord4, bckMovord1, bckMovord2, _
                     bckMovord3, bckOrdlist, bckZzdispsca, bckTaskPM, _
                     bckSolotaskril, nMagazInternoTransito, strQuery, _
                     strClassificazioneLivello1, strClassificazioneLivello2, _
                     strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                     strConto, nCodlsel, strCodart, nCodlsar, False)

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Elabora(ByVal strDitta As String, _
                                      ByVal strDacodart As String, ByVal strAcodart As String, _
                                      ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                      ByVal stredDaagente As String, ByVal stredAagente As String, _
                                      ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                      ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                      ByVal stredDaconto As String, ByVal stredAconto As String, _
                                      ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                      ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                      ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                      ByVal stredSottogr As String, ByVal stredSerie As String, _
                                      ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                      ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                      ByVal bModTCO As Boolean, ByVal bckSelAnnoStag As Boolean, _
                                      ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                      ByVal bModuloCRM As Boolean, ByVal bIsCRMUser As Boolean, _
                                      ByVal strAccvis As String, ByVal bAmm As Boolean, _
                                      ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                      ByVal lIITTScho As Integer, ByVal bckMovord As Boolean, _
                                      ByVal bModAS As Boolean, ByVal bckMovord4 As Boolean, _
                                      ByVal bckMovord1 As Boolean, ByVal bckMovord2 As Boolean, _
                                      ByVal bckMovord3 As Boolean, ByVal bckOrdlist As Boolean, _
                                      ByVal bckZzdispsca As Boolean, ByVal bckTaskPM As Boolean, _
                                      ByVal bckSolotaskril As Boolean, ByVal nMagazInternoTransito As Integer, _
                                      ByVal strQuery As String, _
                                      ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                      ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                      ByVal strClassificazioneLivello5 As String, _
                                      ByVal strConto As String, ByVal nCodlsel As Integer, _
                                      ByVal strCodart As String, ByVal nCodlsar As Integer, ByVal bAccorpa As Boolean) As Boolean
    Dim strListaCampi As String = "(codditt, instid, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_olprogr," & _
        " mo_origine, mo_codart, mo_datcons, mo_magaz, mo_unmis, mo_descr, mo_colli, mo_coleva," & _
        " mo_quant, mo_quaeva, mo_flevas, mo_colpre, mo_quapre, mo_flevapre, mo_prezzo, mo_scont1," & _
        " mo_scont2, mo_scont3, mo_provv, mo_codiva, mo_preziva, mo_prezvalc, mo_commen," & _
        " mo_note, mo_controp, mo_stasino, mo_provv2, mo_tiporkor, mo_annoor, mo_serieor," & _
        " mo_numordor, mo_rigaor, mo_prelist, mo_codcfam, mo_commeca, mo_subcommeca, mo_valore," & _
        " mo_contocontr, mo_codcena, mo_desint, mo_codvuo, mo_vprovv, mo_vprovv2, mo_ump," & _
        " mo_confermato, mo_lotto, mo_rilasciato, mo_aperto, mo_ricimp, mo_datconsor, mo_codclie," & _
        " mo_misura1, mo_misura2, mo_misura3, mo_ultagg, mo_perqta, mo_flkit, mo_ktriga," & _
        " mo_prezivav, mo_valorev, mo_valoremm, mo_scont4, mo_scont5, mo_scont6, mo_datord," & _
        " mo_conto, mo_descr1, mo_riferim, mo_numpar, mo_alfpar, mo_annpar, mo_progrlp," & _
        " mo_stato, mo_impeg, mo_ordin, mo_fase, mo_codpadr, mo_despadr, mo_fasepadr, mo_esistenza," & _
        " mo_quant01, mo_quant02,mo_quant03,mo_quant04,mo_quant05,mo_quant06,mo_quant07,mo_quant08," & _
        " mo_quant09,mo_quant10,mo_quant11,mo_quant12,mo_quant13,mo_quant14,mo_quant15,mo_quant16," & _
        " mo_quant17,mo_quant18,mo_quant19,mo_quant20,mo_quant21,mo_quant22,mo_quant23,mo_quant24," & _
        " mo_quaeva01, mo_quaeva02,mo_quaeva03,mo_quaeva04,mo_quaeva05,mo_quaeva06,mo_quaeva07,mo_quaeva08," & _
        " mo_quaeva09,mo_quaeva10,mo_quaeva11,mo_quaeva12,mo_quaeva13,mo_quaeva14,mo_quaeva15,mo_quaeva16," & _
        " mo_quaeva17,mo_quaeva18,mo_quaeva19,mo_quaeva20,mo_quaeva21,mo_quaeva22,mo_quaeva23,mo_quaeva24, mo_tctaglia)"
    Dim strJoinMovord As String = ""
    Dim strJoinOrdlist As String = ""
    Dim strJoinZzdispsca As String = ""
    Dim strJoinTasks As String = ""
    Dim strWhereMovord As String = ""
    Dim strWhereOrdlist As String = ""
    Dim strWhereZzdispsca As String = ""
    Dim strWhereTasks As String = ""
    Dim strSQL As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim dsTmp2 As DataSet = Nothing
    Dim dEsist As Decimal = 0
    Dim dEsistTC(23) As Decimal
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim dttTmp As New DataTable, dttTmp2 As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                               stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                               stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                               stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                               bckEvasi, stredGruppo, stredSottogr, stredSerie, _
                               bcbTipork, strTipork, strcbRilasciato, stredCodcfam, _
                               bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                               bModuloCRM, bIsCRMUser, strAccvis, bAmm, _
                               lCodorgaOperat, strRegvis, lIITTScho, bckMovord, _
                               bModAS, bckMovord4, bckMovord1, bckMovord2, _
                               bckMovord3, bckOrdlist, bckZzdispsca, bckTaskPM, _
                               bckSolotaskril, nMagazInternoTransito, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                               strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, strConto, nCodlsel, _
                               strCodart, nCodlsar, bAccorpa})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Stringa JOIN su MOVORD/KEYORD/TESTORD/ARTICO/ANAGRA
      '--------------------------------------------------------------------------------------------------------------
      strJoinMovord = "anagra INNER JOIN testord ON anagra.codditt = testord.codditt AND anagra.an_conto = testord.td_conto" & _
                      " INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_serie = movord.mo_serie AND testord.td_anno = movord.mo_anno AND testord.td_numord = movord.mo_numord" & _
                      " INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_anno = keyord.ko_anno AND movord.mo_serie = keyord.ko_serie AND movord.mo_numord = keyord.ko_numord AND movord.mo_riga = keyord.ko_riga" & _
                      " INNER JOIN artico ON artico.codditt = movord.codditt AND artico.ar_codart = movord.mo_codart" & _
                      " LEFT JOIN movord As movord_1 ON movord.codditt = movord_1.codditt AND movord.mo_tiporkor = movord_1.mo_tipork AND movord.mo_annoor = movord_1.mo_anno AND movord.mo_serieor = movord_1.mo_serie AND movord.mo_numordor = movord_1.mo_numord AND movord.mo_rigaor = movord_1.mo_riga" & _
                      " LEFT JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga"
      '--------------------------------------------------------------------------------------------------------------
      '--- Stringa JOIN su ORDLIST/ARTICO
      '--------------------------------------------------------------------------------------------------------------
      strJoinOrdlist = "ordlist INNER JOIN artico ON ordlist.codditt = artico.codditt AND ordlist.ol_codart = artico.ar_codart" & _
                       " LEFT JOIN anagra ON ordlist.codditt = anagra.codditt AND ordlist.ol_conto = anagra.an_conto" & _
                       " LEFT JOIN ordlist As ordlist_1 ON ordlist.codditt = ordlist_1.codditt AND ordlist.ol_olprogr = ordlist_1.ol_progr" & _
                       " LEFT JOIN ordlisttc ON ordlist.codditt = ordlisttc.codditt AND ordlist.ol_progr = ordlisttc.ol_progr"
      '--------------------------------------------------------------------------------------------------------------
      '--- Stringa JOIN su ZZDISPSCA/ARTICO
      '--------------------------------------------------------------------------------------------------------------
      strJoinZzdispsca = " artico INNER JOIN zzdispsca ON artico.codditt = zzdispsca.codditt AND artico.ar_codart = zzdispsca.ttd_codart"
      '--------------------------------------------------------------------------------------------------------------
      '--- Stringa JOIN su tasks/ARTICO
      '--------------------------------------------------------------------------------------------------------------
      strJoinTasks = " artico INNER JOIN tasks ON artico.codditt = tasks.codditt AND artico.ar_codart = tasks.tsk_codart"
      '---------------------------------------------------------------------------------------
      '--- Compone la stringa WHERE su MOVORD/KEYORD/TESTORD/ARTICO/ANAGRA
      '--------------------------------------------------------------------------------------------------------------
      strWhereMovord = "keyord.codditt = " & CStrSQL(strDitta) & _
        IIf(NTSCInt(stredGruppo) > 0, " AND ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
        IIf(NTSCInt(stredSottogr) > 0, " AND ar_sotgru = " & stredSottogr, "").ToString & _
        IIf(stredSerie.Trim <> "", " AND ko_serie = " & CStrSQL(stredSerie), "").ToString & _
        IIf(bckEvasi = True, " AND movord.mo_flevas = 'C'", "").ToString & _
        IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
      If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
        strWhereMovord += " AND ko_magaz BETWEEN " & stredDamagaz & " AND " & stredAmagaz
      End If
      If (NTSCInt(stredDaagente) <> 0) Or (NTSCInt(stredAagente) <> 9999) Then
        strWhereMovord += " AND td_codagen BETWEEN " & stredDaagente & " AND " & stredAagente
      End If
      If (NTSCDate(stredDadatord) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(stredAdatord) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strWhereMovord += " AND td_datord BETWEEN " & CDataSQL(stredDadatord) & " AND " & CDataSQL(stredAdatord)
      End If
      If (NTSCDate(stredDadatcons) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(stredAdatcons) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strWhereMovord += " AND ko_datcons BETWEEN " & CDataSQL(stredDadatcons) & " AND " & CDataSQL(stredAdatcons)
      End If
      If (NTSCInt(stredCommecaini) <> 0) Or (NTSCInt(stredCommecafin) <> 999999999) Then
        strWhereMovord += " AND movord.mo_commeca BETWEEN " & stredCommecaini & " AND " & stredCommecafin
      End If
      Select Case strConto
        Case "A"
          If (NTSCInt(stredDaconto) <> 0) Or (NTSCInt(stredAconto) <> 999999999) Then
            strWhereMovord += " AND ko_conto BETWEEN " & stredDaconto & " AND " & stredAconto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strWhereMovord += " AND ko_conto IN (SELECT lse_conto FROM listsel" & _
                                               " WHERE codditt = " & CStrSQL(strDitta) & _
                                               " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      Select Case strCodart
        Case "A"
          If (strDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or (strAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strWhereMovord += " AND ko_codart BETWEEN " & CStrSQL(strDacodart) & " AND " & CStrSQL(strAcodart)
          End If
          If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
            strWhereMovord += " AND movord.mo_fase BETWEEN " & stredFaseini & " AND " & stredFasefin
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strWhereMovord += _
              " AND CAST(ko_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(movord.mo_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      If (bModTCO = True) And (bckSelAnnoStag = True) Then
        If NTSCInt(stredAnnotco) > 0 Then strWhereMovord += " AND artico.ar_anno = " & stredAnnotco
        If NTSCInt(stredCodstag) > 0 Then strWhereMovord += " AND artico.ar_codstag = " & stredCodstag
      End If
      If (bModuloCRM = True) And (bIsCRMUser = True) Then
        strWhereMovord += " AND ("
        If strAccvis <> "T" Then
          strWhereMovord += " (an_tipo = 'C' AND an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                                                            " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                                                            " AND leads.le_coddest = 0"
          Select Case strAccvis
            Case "P" : strWhereMovord += " AND le_opinc = " & lCodorgaOperat
            Case "C" : strWhereMovord += " AND le_opinc IN (" & strRegvis & ")"
          End Select
          strWhereMovord += "))"
        Else
          strWhereMovord += " an_tipo = 'C'"
        End If
        If bAmm = True Then strWhereMovord += " OR an_tipo <> 'C'" Else strWhereMovord += " AND an_tipo <> 'F'"
        strWhereMovord += ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiungo la where dei campi con OR e AND rimappati
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strWhereMovord, strQuery)
      '--------------------------------------------------------------------------------------------------------------
      '--- Compone la stringa WHERE su ORDLIST/ARTICO
      '--------------------------------------------------------------------------------------------------------------
      strWhereOrdlist = "ordlist.codditt = " & CStrSQL(strDitta) & _
        " AND ordlist.ol_stato <> 'T'" & _
        IIf(NTSCInt(stredGruppo) > 0, " AND ar_gruppo = " & stredGruppo, "").ToString & _
        IIf(NTSCInt(stredSottogr) > 0, " AND ar_sotgru = " & stredSottogr, "").ToString & _
        IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
      If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
        strWhereOrdlist += " AND ordlist.ol_magaz BETWEEN " & stredDamagaz & " AND " & stredAmagaz
      End If
      If (NTSCDate(stredDadatord) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(stredAdatord) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strWhereOrdlist += " AND ordlist.ol_datord BETWEEN " & CDataSQL(stredDadatord) & " AND " & CDataSQL(stredAdatord)
      End If
      If (NTSCDate(stredDadatcons) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(stredAdatcons) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strWhereOrdlist += " AND ordlist.ol_datcons BETWEEN " & CDataSQL(stredDadatcons) & " AND " & CDataSQL(stredAdatcons)
      End If
      If (NTSCInt(stredCommecaini) <> 0) Or (NTSCInt(stredCommecafin) <> 999999999) Then
        strWhereOrdlist += " AND ordlist.ol_commeca BETWEEN " & stredCommecaini & " AND " & stredCommecafin
      End If
      Select Case strConto
        Case "A"
          If (NTSCInt(stredDaconto) <> 0) Or (NTSCInt(stredAconto) <> 999999999) Then
            strWhereOrdlist += " AND ordlist.ol_conto BETWEEN " & stredDaconto & " AND " & stredAconto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strWhereOrdlist += " AND ordlist.ol_conto IN (SELECT lse_conto FROM listsel" & _
                                                        " WHERE codditt = " & CStrSQL(strDitta) & _
                                                        " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      Select Case strCodart
        Case "A"
          If (strDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or (strAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strWhereOrdlist += " AND ordlist.ol_codart BETWEEN " & CStrSQL(strDacodart) & " AND " & CStrSQL(strAcodart)
          End If
          If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
            strWhereOrdlist += " AND ordlist.ol_fase BETWEEN " & stredFaseini & " AND " & stredFasefin
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strWhereOrdlist += _
              " AND CAST(ordlist.ol_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(ordlist.ol_fase AS VARCHAR(4))" & _
                " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      If (bModTCO = True) And (bckSelAnnoStag = True) Then
        If NTSCInt(stredAnnotco) > 0 Then strWhereOrdlist += " AND artico.ar_anno = " & stredAnnotco
        If NTSCInt(stredCodstag) > 0 Then strWhereOrdlist += " AND artico.ar_codstag = " & stredCodstag
      End If
      If (bModuloCRM = True) And (bIsCRMUser = True) Then
        strWhereOrdlist += " AND (("
        If strAccvis <> "T" Then
          strWhereOrdlist += "(an_tipo = 'C'" & _
            " AND an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                             " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                             " AND le_coddest = 0"
          Select Case strAccvis
            Case "P" : strWhereOrdlist += " AND le_opinc = " & lCodorgaOperat
            Case "C" : strWhereOrdlist += " AND le_opinc IN (" & strRegvis & ")"
          End Select
          strWhereOrdlist += "))"
        Else
          strWhereOrdlist += " an_tipo = 'C'"
        End If
        If bAmm = True Then strWhereOrdlist += " OR an_tipo <> 'C'" Else strWhereOrdlist += " AND an_tipo <> 'F'"
        strWhereOrdlist += ") OR an_tipo IS NULL)"
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Compone la stringa WHERE su ZZDISPSCA/ARTICO
      '--------------------------------------------------------------------------------------------------------------
      strWhereZzdispsca = "zzdispsca.codditt = " & CStrSQL(strDitta) & _
        IIf(NTSCInt(stredGruppo) > 0, " AND ar_gruppo = " & stredGruppo, "").ToString & _
        IIf(NTSCInt(stredSottogr) > 0, " AND ar_sotgru = " & stredSottogr, "").ToString & _
        IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
        " AND (ttd_datord Is Null OR ttd_datord BETWEEN " & CDataSQL(stredDadatord) & " AND " & CDataSQL(stredAdatord) & ")"
      If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
        strWhereZzdispsca += " AND ttd_codmaga BETWEEN " & stredDamagaz & " AND " & stredAmagaz
      End If
      If (NTSCDate(stredDadatcons) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(stredAdatcons) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strWhereZzdispsca += " AND ttd_datcons BETWEEN " & CDataSQL(stredDadatcons) & " AND " & CDataSQL(stredAdatcons)
      End If
      If (NTSCInt(stredCommecaini) <> 0) Or (NTSCInt(stredCommecafin) <> 999999999) Then
        strWhereZzdispsca += " AND ttd_commeca BETWEEN " & stredCommecaini & " AND " & stredCommecafin
      End If
      Select Case strCodart
        Case "A"
          If (strDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or (strAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strWhereZzdispsca += " AND ttd_codart BETWEEN " & CStrSQL(strDacodart) & " AND " & CStrSQL(strAcodart)
          End If
          If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
            strWhereZzdispsca += " AND ttd_faseterz BETWEEN " & stredFaseini & " AND " & stredFasefin
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strWhereZzdispsca += _
              " AND CAST(ttd_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(ttd_faseterz AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      If (bModTCO = True) And (bckSelAnnoStag = True) Then
        If NTSCInt(stredAnnotco) > 0 Then strWhereZzdispsca += " AND artico.ar_anno = " & stredAnnotco
        If NTSCInt(stredCodstag) > 0 Then strWhereZzdispsca += " AND artico.ar_codstag = " & stredCodstag
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Where su tasks (PM)
      '--------------------------------------------------------------------------------------------------------------
      strWhereTasks = "tasks.codditt = " & CStrSQL(strDitta) & _
        " AND tsk_darave = 'D'" & _
        " AND tsk_flevas <> 'S'" & _
        " AND tsk_flevas <> 'Q'" & _
        " AND (tsk_ultagg Is Null OR tsk_ultagg BETWEEN " & CDataSQL(stredDadatord) & " AND " & CDataSQL(stredAdatord) & ")" & _
        " AND (tsk_deadline Is Null Or tsk_deadline BETWEEN " & CDataSQL(stredDadatcons) & " AND " & CDataSQL(stredAdatcons) & ")" & _
        IIf(NTSCInt(stredGruppo) > 0, " AND ar_gruppo = " & stredGruppo, "").ToString & _
        IIf(NTSCInt(stredSottogr) > 0, " AND ar_sotgru = " & stredSottogr, "").ToString & _
        IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
      If (NTSCInt(stredCommecaini) <> 0) Or (NTSCInt(stredCommecafin) <> 999999999) Then
        strWhereTasks += " AND tsk_commeca BETWEEN " & stredCommecaini & " AND " & stredCommecafin
      End If
      Select Case strCodart
        Case "A"
          If (strDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or (strAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strWhereTasks += " AND tsk_codart BETWEEN " & CStrSQL(strDacodart) & " AND " & CStrSQL(strAcodart)
          End If
          If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
            strWhereTasks += " AND ar_ultfase BETWEEN " & stredFaseini & " AND " & stredFasefin
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strWhereTasks += _
              " AND CAST(tsk_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(ar_ultfase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      If (bModTCO = True) And (bckSelAnnoStag = True) Then
        If NTSCInt(stredAnnotco) > 0 Then strWhereTasks += " AND artico.ar_anno = " & stredAnnotco
        If NTSCInt(stredCodstag) > 0 Then strWhereTasks += " AND artico.ar_codstag = " & stredCodstag
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Svuota la tabella temporanea
      '--------------------------------------------------------------------------------------------------------------
      ResetTblInstId("TTSCHO", False, lIITTScho)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se selezionato inserisce i dati di tipo R da MOVORD
      '--------------------------------------------------------------------------------------------------------------
      If bckMovord = True Then
        strSQL = "INSERT INTO TTSCHO " & strListaCampi & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTScho & ", movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga, 0, 'M'," & _
          " movord.mo_codart, movord.mo_datcons, ko_magaz, movord.mo_unmis, movord.mo_descr, movord.mo_colli, movord.mo_coleva, movord.mo_quant," & _
          " movord.mo_quaeva, movord.mo_flevas, movord.mo_colpre, movord.mo_quapre, movord.mo_flevapre, movord.mo_prezzo, movord.mo_scont1," & _
          " movord.mo_scont2, movord.mo_scont3, movord.mo_provv, movord.mo_codiva, movord.mo_preziva, movord.mo_prezvalc, movord.mo_commen," & _
          " movord.mo_note, movord.mo_controp, movord.mo_stasino, movord.mo_provv2, movord.mo_tiporkor, movord.mo_annoor, movord.mo_serieor," & _
          " movord.mo_numordor, movord.mo_rigaor, movord.mo_prelist, movord.mo_codcfam, movord.mo_commeca, movord.mo_subcommeca, movord.mo_valore," & _
          " movord.mo_contocontr, movord.mo_codcena, movord.mo_desint, movord.mo_codvuo, movord.mo_vprovv, movord.mo_vprovv2, movord.mo_ump," & _
          " movord.mo_confermato, movord.mo_lotto, movord.mo_rilasciato, movord.mo_aperto, movord.mo_ricimp, movord.mo_datconsor," & _
          " movord.mo_codclie, movord.mo_misura1, movord.mo_misura2, movord.mo_misura3, movord.mo_ultagg, movord.mo_perqta, movord.mo_flkit," & _
          " movord.mo_ktriga, movord.mo_prezivav, movord.mo_valorev, movord.mo_valoremm, movord.mo_scont4, movord.mo_scont5, movord.mo_scont6," & _
          " td_datord, td_conto, an_descr1, td_riferim, td_numpar, td_alfpar, td_annpar," & _
          " 0, ' ', ko_impeg, ko_ordin, ko_fase, Null, Null, Null, 'N'," & _
          " movordtc.mo_quant01, movordtc.mo_quant02,movordtc.mo_quant03,movordtc.mo_quant04,movordtc.mo_quant05,movordtc.mo_quant06,movordtc.mo_quant07,movordtc.mo_quant08," & _
          " movordtc.mo_quant09,movordtc.mo_quant10,movordtc.mo_quant11,movordtc.mo_quant12,movordtc.mo_quant13,movordtc.mo_quant14,movordtc.mo_quant15,movordtc.mo_quant16," & _
          " movordtc.mo_quant17,movordtc.mo_quant18,movordtc.mo_quant19,movordtc.mo_quant20,movordtc.mo_quant21,movordtc.mo_quant22,movordtc.mo_quant23,movordtc.mo_quant24," & _
          " movordtc.mo_quaeva01, movordtc.mo_quaeva02,movordtc.mo_quaeva03,movordtc.mo_quaeva04,movordtc.mo_quaeva05,movordtc.mo_quaeva06,mo_quaeva07,mo_quaeva08," & _
          " movordtc.mo_quaeva09,movordtc.mo_quaeva10,movordtc.mo_quaeva11,movordtc.mo_quaeva12,movordtc.mo_quaeva13,movordtc.mo_quaeva14,mo_quaeva15,movordtc.mo_quaeva16," & _
          " movordtc.mo_quaeva17,movordtc.mo_quaeva18,movordtc.mo_quaeva19,movordtc.mo_quaeva20,movordtc.mo_quaeva21,mo_quaeva22,movordtc.mo_quaeva23,movordtc.mo_quaeva24, movord.mo_tctaglia" & _
          " FROM " & strJoinMovord & _
          " WHERE ko_tipork = 'R'" & _
          " AND " & strWhereMovord
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se selezionato inserisce i dati di tipo # da MOVORD
      '--------------------------------------------------------------------------------------------------------------
      If (bModAS = True) And (bckMovord4 = True) Then
        strSQL = "INSERT INTO TTSCHO " & strListaCampi & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTScho & ", movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga, 0, 'M'," & _
          " movord.mo_codart, movord.mo_datcons, ko_magaz, movord.mo_unmis, movord.mo_descr, movord.mo_colli, movord.mo_coleva, movord.mo_quant," & _
          " movord.mo_quaeva, movord.mo_flevas, movord.mo_colpre, movord.mo_quapre, movord.mo_flevapre, movord.mo_prezzo, movord.mo_scont1," & _
          " movord.mo_scont2, movord.mo_scont3, movord.mo_provv, movord.mo_codiva, movord.mo_preziva, movord.mo_prezvalc, movord.mo_commen," & _
          " movord.mo_note, movord.mo_controp, movord.mo_stasino, movord.mo_provv2, movord.mo_tiporkor, movord.mo_annoor, movord.mo_serieor," & _
          " movord.mo_numordor, movord.mo_rigaor, movord.mo_prelist, movord.mo_codcfam, movord.mo_commeca, movord.mo_subcommeca, movord.mo_valore," & _
          " movord.mo_contocontr, movord.mo_codcena, movord.mo_desint, movord.mo_codvuo, movord.mo_vprovv, movord.mo_vprovv2, movord.mo_ump," & _
          " movord.mo_confermato, movord.mo_lotto, movord.mo_rilasciato, movord.mo_aperto, movord.mo_ricimp, movord.mo_datconsor," & _
          " movord.mo_codclie, movord.mo_misura1, movord.mo_misura2, movord.mo_misura3, movord.mo_ultagg, movord.mo_perqta, movord.mo_flkit," & _
          " movord.mo_ktriga, movord.mo_prezivav, movord.mo_valorev, movord.mo_valoremm, movord.mo_scont4, movord.mo_scont5, movord.mo_scont6," & _
          " td_datord, td_conto, an_descr1, td_riferim, td_numpar, td_alfpar, td_annpar," & _
          " 0, ' ', ko_impeg, ko_ordin, ko_fase, Null, Null, Null, 'N'," & _
          " movordtc.mo_quant01, movordtc.mo_quant02,movordtc.mo_quant03,movordtc.mo_quant04,movordtc.mo_quant05,movordtc.mo_quant06,movordtc.mo_quant07,movordtc.mo_quant08," & _
          " movordtc.mo_quant09,movordtc.mo_quant10,movordtc.mo_quant11,movordtc.mo_quant12,movordtc.mo_quant13,movordtc.mo_quant14,movordtc.mo_quant15,movordtc.mo_quant16," & _
          " movordtc.mo_quant17,movordtc.mo_quant18,movordtc.mo_quant19,movordtc.mo_quant20,movordtc.mo_quant21,movordtc.mo_quant22,movordtc.mo_quant23,movordtc.mo_quant24," & _
          " movordtc.mo_quaeva01, movordtc.mo_quaeva02,movordtc.mo_quaeva03,movordtc.mo_quaeva04,movordtc.mo_quaeva05,movordtc.mo_quaeva06,mo_quaeva07,mo_quaeva08," & _
          " movordtc.mo_quaeva09,movordtc.mo_quaeva10,movordtc.mo_quaeva11,movordtc.mo_quaeva12,movordtc.mo_quaeva13,movordtc.mo_quaeva14,mo_quaeva15,movordtc.mo_quaeva16," & _
          " movordtc.mo_quaeva17,movordtc.mo_quaeva18,movordtc.mo_quaeva19,movordtc.mo_quaeva20,movordtc.mo_quaeva21,mo_quaeva22,movordtc.mo_quaeva23,movordtc.mo_quaeva24, movord.mo_tctaglia" & _
          " FROM " & strJoinMovord & _
          " WHERE ko_tipork = '#'" & _
          " AND " & strWhereMovord
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se selezionato inserisce i dati di tipo H,Y,X,0 da MOVORD
      '--------------------------------------------------------------------------------------------------------------
      If bckMovord1 = True Then
        strSQL = "INSERT INTO TTSCHO " & strListaCampi & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTScho & ", movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga, 0, 'M'," & _
          " movord.mo_codart, movord.mo_datcons, ko_magaz, movord.mo_unmis, movord.mo_descr, movord.mo_colli, movord.mo_coleva, movord.mo_quant," & _
          " movord.mo_quaeva, movord.mo_flevas, movord.mo_colpre, movord.mo_quapre, movord.mo_flevapre, movord.mo_prezzo, movord.mo_scont1," & _
          " movord.mo_scont2, movord.mo_scont3, movord.mo_provv, movord.mo_codiva, movord.mo_preziva, movord.mo_prezvalc, movord.mo_commen," & _
          " movord.mo_note, movord.mo_controp, movord.mo_stasino, movord.mo_provv2, movord.mo_tiporkor, movord.mo_annoor, movord.mo_serieor," & _
          " movord.mo_numordor, movord.mo_rigaor, movord.mo_prelist, movord.mo_codcfam, movord.mo_commeca, movord.mo_subcommeca, movord.mo_valore," & _
          " movord.mo_contocontr, movord.mo_codcena, movord.mo_desint, movord.mo_codvuo, movord.mo_vprovv, movord.mo_vprovv2, movord.mo_ump," & _
          " movord.mo_confermato, movord.mo_lotto, movord.mo_rilasciato, movord.mo_aperto, movord.mo_ricimp, movord.mo_datconsor," & _
          " movord.mo_codclie, movord.mo_misura1, movord.mo_misura2, movord.mo_misura3, movord.mo_ultagg, movord.mo_perqta, movord.mo_flkit," & _
          " movord.mo_ktriga, movord.mo_prezivav, movord.mo_valorev, movord.mo_valoremm, movord.mo_scont4, movord.mo_scont5, movord.mo_scont6," & _
          " td_datord, td_conto, an_descr1, td_riferim, td_numpar, td_alfpar, td_annpar," & _
          " 0, ' ', ko_impeg, ko_ordin, ko_fase, movord_1.mo_codart, movord_1.mo_descr, movord_1.mo_fase,'N'," & _
          " movordtc.mo_quant01, movordtc.mo_quant02,movordtc.mo_quant03,movordtc.mo_quant04,movordtc.mo_quant05,movordtc.mo_quant06,movordtc.mo_quant07,movordtc.mo_quant08," & _
          " movordtc.mo_quant09,movordtc.mo_quant10,movordtc.mo_quant11,movordtc.mo_quant12,movordtc.mo_quant13,movordtc.mo_quant14,movordtc.mo_quant15,movordtc.mo_quant16," & _
          " movordtc.mo_quant17,movordtc.mo_quant18,movordtc.mo_quant19,movordtc.mo_quant20,movordtc.mo_quant21,movordtc.mo_quant22,movordtc.mo_quant23,movordtc.mo_quant24," & _
          " movordtc.mo_quaeva01, movordtc.mo_quaeva02,movordtc.mo_quaeva03,movordtc.mo_quaeva04,movordtc.mo_quaeva05,movordtc.mo_quaeva06,mo_quaeva07,mo_quaeva08," & _
          " movordtc.mo_quaeva09,movordtc.mo_quaeva10,movordtc.mo_quaeva11,movordtc.mo_quaeva12,movordtc.mo_quaeva13,movordtc.mo_quaeva14,mo_quaeva15,movordtc.mo_quaeva16," & _
          " movordtc.mo_quaeva17,movordtc.mo_quaeva18,movordtc.mo_quaeva19,movordtc.mo_quaeva20,movordtc.mo_quaeva21,mo_quaeva22,movordtc.mo_quaeva23,movordtc.mo_quaeva24, movord.mo_tctaglia" & _
          " FROM " & strJoinMovord & _
          " WHERE ko_tipork IN ('H','O','X','Y')" & _
          " AND " & strWhereMovord
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se selezionato inserisce i dati di tipo $ da MOVORD
      '--------------------------------------------------------------------------------------------------------------
      If bckMovord2 = True Then
        strSQL = "INSERT INTO TTSCHO " & strListaCampi & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTScho & ", movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga, 0, 'M'," & _
          " movord.mo_codart, movord.mo_datcons, ko_magaz, movord.mo_unmis, movord.mo_descr, movord.mo_colli, movord.mo_coleva, movord.mo_quant," & _
          " movord.mo_quaeva, movord.mo_flevas, movord.mo_colpre, movord.mo_quapre, movord.mo_flevapre, movord.mo_prezzo, movord.mo_scont1," & _
          " movord.mo_scont2, movord.mo_scont3, movord.mo_provv, movord.mo_codiva, movord.mo_preziva, movord.mo_prezvalc, movord.mo_commen," & _
          " movord.mo_note, movord.mo_controp, movord.mo_stasino, movord.mo_provv2, movord.mo_tiporkor, movord.mo_annoor, movord.mo_serieor," & _
          " movord.mo_numordor, movord.mo_rigaor, movord.mo_prelist, movord.mo_codcfam, movord.mo_commeca, movord.mo_subcommeca, movord.mo_valore," & _
          " movord.mo_contocontr, movord.mo_codcena, movord.mo_desint, movord.mo_codvuo, movord.mo_vprovv, movord.mo_vprovv2, movord.mo_ump," & _
          " movord.mo_confermato, movord.mo_lotto, movord.mo_rilasciato, movord.mo_aperto, movord.mo_ricimp, movord.mo_datconsor," & _
          " movord.mo_codclie, movord.mo_misura1, movord.mo_misura2, movord.mo_misura3, movord.mo_ultagg, movord.mo_perqta, movord.mo_flkit," & _
          " movord.mo_ktriga, movord.mo_prezivav, movord.mo_valorev, movord.mo_valoremm, movord.mo_scont4, movord.mo_scont5, movord.mo_scont6," & _
          " td_datord, td_conto, an_descr1, td_riferim, td_numpar, td_alfpar, td_annpar," & _
          " 0, ' ', ko_impeg, ko_ordin, ko_fase, Null, Null, Null,'N'," & _
          " movordtc.mo_quant01, movordtc.mo_quant02,movordtc.mo_quant03,movordtc.mo_quant04,movordtc.mo_quant05,movordtc.mo_quant06,movordtc.mo_quant07,movordtc.mo_quant08," & _
          " movordtc.mo_quant09,movordtc.mo_quant10,movordtc.mo_quant11,movordtc.mo_quant12,movordtc.mo_quant13,movordtc.mo_quant14,movordtc.mo_quant15,movordtc.mo_quant16," & _
          " movordtc.mo_quant17,movordtc.mo_quant18,movordtc.mo_quant19,movordtc.mo_quant20,movordtc.mo_quant21,movordtc.mo_quant22,movordtc.mo_quant23,movordtc.mo_quant24," & _
          " movordtc.mo_quaeva01, movordtc.mo_quaeva02,movordtc.mo_quaeva03,movordtc.mo_quaeva04,movordtc.mo_quaeva05,movordtc.mo_quaeva06,mo_quaeva07,mo_quaeva08," & _
          " movordtc.mo_quaeva09,movordtc.mo_quaeva10,movordtc.mo_quaeva11,movordtc.mo_quaeva12,movordtc.mo_quaeva13,movordtc.mo_quaeva14,mo_quaeva15,movordtc.mo_quaeva16," & _
          " movordtc.mo_quaeva17,movordtc.mo_quaeva18,movordtc.mo_quaeva19,movordtc.mo_quaeva20,movordtc.mo_quaeva21,mo_quaeva22,movordtc.mo_quaeva23,movordtc.mo_quaeva24, movord.mo_tctaglia" & _
          " FROM " & strJoinMovord & _
          " WHERE ko_tipork = '$'" & _
          " AND " & strWhereMovord
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se selezionato inserisce i dati di tipo V da MOVORD
      '--------------------------------------------------------------------------------------------------------------
      If bckMovord3 = True Then
        strSQL = "INSERT INTO TTSCHO " & strListaCampi & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTScho & ", movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord, movord.mo_riga, 0, 'M'," & _
          " movord.mo_codart, movord.mo_datcons, ko_magaz, movord.mo_unmis, movord.mo_descr, movord.mo_colli, movord.mo_coleva, movord.mo_quant," & _
          " movord.mo_quaeva, movord.mo_flevas, movord.mo_colpre, movord.mo_quapre, movord.mo_flevapre, movord.mo_prezzo, movord.mo_scont1," & _
          " movord.mo_scont2, movord.mo_scont3, movord.mo_provv, movord.mo_codiva, movord.mo_preziva, movord.mo_prezvalc, movord.mo_commen," & _
          " movord.mo_note, movord.mo_controp, movord.mo_stasino, movord.mo_provv2, movord.mo_tiporkor, movord.mo_annoor, movord.mo_serieor," & _
          " movord.mo_numordor, movord.mo_rigaor, movord.mo_prelist, movord.mo_codcfam, movord.mo_commeca, movord.mo_subcommeca, movord.mo_valore," & _
          " movord.mo_contocontr, movord.mo_codcena, movord.mo_desint, movord.mo_codvuo, movord.mo_vprovv, movord.mo_vprovv2, movord.mo_ump," & _
          " movord.mo_confermato, movord.mo_lotto, movord.mo_rilasciato, movord.mo_aperto, movord.mo_ricimp, movord.mo_datconsor," & _
          " movord.mo_codclie, movord.mo_misura1, movord.mo_misura2, movord.mo_misura3, movord.mo_ultagg, movord.mo_perqta, movord.mo_flkit," & _
          " movord.mo_ktriga, movord.mo_prezivav, movord.mo_valorev, movord.mo_valoremm, movord.mo_scont4, movord.mo_scont5, movord.mo_scont6," & _
          " td_datord, td_conto, an_descr1, td_riferim, td_numpar, td_alfpar, td_annpar," & _
          " 0, ' ', ko_impeg, ko_ordin, ko_fase, Null, Null, Null,'N'," & _
          " movordtc.mo_quant01, movordtc.mo_quant02,movordtc.mo_quant03,movordtc.mo_quant04,movordtc.mo_quant05,movordtc.mo_quant06,movordtc.mo_quant07,movordtc.mo_quant08," & _
          " movordtc.mo_quant09,movordtc.mo_quant10,movordtc.mo_quant11,movordtc.mo_quant12,movordtc.mo_quant13,movordtc.mo_quant14,movordtc.mo_quant15,movordtc.mo_quant16," & _
          " movordtc.mo_quant17,movordtc.mo_quant18,movordtc.mo_quant19,movordtc.mo_quant20,movordtc.mo_quant21,movordtc.mo_quant22,movordtc.mo_quant23,movordtc.mo_quant24," & _
          " movordtc.mo_quaeva01, movordtc.mo_quaeva02,movordtc.mo_quaeva03,movordtc.mo_quaeva04,movordtc.mo_quaeva05,movordtc.mo_quaeva06,mo_quaeva07,mo_quaeva08," & _
          " movordtc.mo_quaeva09,movordtc.mo_quaeva10,movordtc.mo_quaeva11,movordtc.mo_quaeva12,movordtc.mo_quaeva13,movordtc.mo_quaeva14,mo_quaeva15,movordtc.mo_quaeva16," & _
          " movordtc.mo_quaeva17,movordtc.mo_quaeva18,movordtc.mo_quaeva19,movordtc.mo_quaeva20,movordtc.mo_quaeva21,mo_quaeva22,movordtc.mo_quaeva23,movordtc.mo_quaeva24, movord.mo_tctaglia" & _
          " FROM " & strJoinMovord & _
          " WHERE ko_tipork = 'V'" & _
          " AND " & strWhereMovord
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se selezionato inserisce i dati di tipo H,Y,X,0 da ORDLIST
      '--------------------------------------------------------------------------------------------------------------
      If bckOrdlist = True Then
        '------------------------------------------------------------------------------------------------------------
        '--- Copia i dati da ORDLIST/ARTICO per i record di tipo X, Y negli Impegni
        '------------------------------------------------------------------------------------------------------------
        strSQL = "INSERT INTO TTSCHO " & strListaCampi & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTScho & ", ordlist.ol_tipork, 0, ' ', 0, 0, ordlist.ol_progr, 'O', ordlist.ol_codart," & _
          " ordlist.ol_datcons, ordlist.ol_magaz, ordlist.ol_unmis, ordlist.ol_descr, ordlist.ol_colli, 0, ordlist.ol_quant, 0, 'C', 0, 0, 'C'," & _
          " ordlist.ol_prezzo, ordlist.ol_scont1, ordlist.ol_scont2, ordlist.ol_scont3, 0, ordlist.ol_codiva, 0, ordlist.ol_prezvalc, ordlist.ol_commen," & _
          " ordlist.ol_note, ordlist.ol_controp, ordlist.ol_stasino, 0, NULL, 0, NULL, 0, 0, ordlist.ol_prelist, ordlist.ol_codcfam," & _
          " ordlist.ol_commeca, ordlist.ol_subcommeca, ordlist.ol_valore, ordlist.ol_contocontr, ordlist.ol_codcena, ordlist.ol_desint, ordlist.ol_codvuo," & _
          " 0, 0, ordlist.ol_ump, case when ordlist.ol_stato = 'S' Then 'S' Else 'N' End, ordlist.ol_lotto, 'N', 'N', ordlist.ol_ricimp, " & _
          CDataSQL(IntSetDate("01/01/1900")) & ", ordlist.ol_codclie, ordlist.ol_misura1, ordlist.ol_misura2, ordlist.ol_misura3, " & _
          CDataSQL(IntSetDate("01/01/1900")) & ", ordlist.ol_perqta, ' ', 0, 0, ordlist.ol_valorev, 0," & _
          " ordlist.ol_scont4, ordlist.ol_scont5, ordlist.ol_scont6, ordlist.ol_datord, ordlist.ol_conto, an_descr1, NULL, 0, ' ', 0," & _
          " 0, ordlist.ol_stato, 1, 0, ordlist.ol_fase, ordlist_1.ol_codart, ordlist_1.ol_descr, ordlist_1.ol_fase, 'N'," & _
          " ordlisttc.ol_quant01, ordlisttc.ol_quant02,ordlisttc.ol_quant03,ordlisttc.ol_quant04,ordlisttc.ol_quant05,ordlisttc.ol_quant06,ordlisttc.ol_quant07,ordlisttc.ol_quant08," & _
          " ordlisttc.ol_quant09,ordlisttc.ol_quant10,ordlisttc.ol_quant11,ordlisttc.ol_quant12,ordlisttc.ol_quant13,ordlisttc.ol_quant14,ordlisttc.ol_quant15,ordlisttc.ol_quant16," & _
          " ordlisttc.ol_quant17,ordlisttc.ol_quant18,ordlisttc.ol_quant19,ordlisttc.ol_quant20,ordlisttc.ol_quant21,ordlisttc.ol_quant22,ordlisttc.ol_quant23,ordlisttc.ol_quant24," & _
          " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, ordlist.ol_tctaglia" & _
          " FROM " & strJoinOrdlist & _
          " WHERE ordlist.ol_tipork IN ('X','Y')" & _
          " AND " & strWhereOrdlist
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        '------------------------------------------------------------------------------------------------------------
        '--- Crea una seconda riga da ORDLIST prendendo ol_magaz2 per i record X negli Ordini
        '------------------------------------------------------------------------------------------------------------
        strSQL = "INSERT INTO TTSCHO " & strListaCampi & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTScho & ", ordlist.ol_tipork, 0, ' ', 0, 0, ordlist.ol_progr, 'O', ordlist.ol_codart," & _
          " ordlist.ol_datcons, ordlist.ol_magaz2, ordlist.ol_unmis, ordlist.ol_descr, ordlist.ol_colli, 0, ordlist.ol_quant, 0, 'C', 0, 0, 'C'," & _
          " ordlist.ol_prezzo, ordlist.ol_scont1, ordlist.ol_scont2, ordlist.ol_scont3, 0, ordlist.ol_codiva, 0, ordlist.ol_prezvalc, ordlist.ol_commen," & _
          " ordlist.ol_note, ordlist.ol_controp, ordlist.ol_stasino, 0, NULL, 0, NULL, 0, 0, ordlist.ol_prelist, ordlist.ol_codcfam," & _
          " ordlist.ol_commeca, ordlist.ol_subcommeca, ordlist.ol_valore, ordlist.ol_contocontr, ordlist.ol_codcena, ordlist.ol_desint, ordlist.ol_codvuo," & _
          " 0, 0, ordlist.ol_ump, Case When ordlist.ol_stato = 'S' Then 'S' Else 'N' End, ordlist.ol_lotto, 'N', 'N', ordlist.ol_ricimp, " & _
          CDataSQL(IntSetDate("01/01/1900")) & ", ordlist.ol_codclie, ordlist.ol_misura1, ordlist.ol_misura2, ordlist.ol_misura3, " & _
          CDataSQL(IntSetDate("01/01/1900")) & ", ordlist.ol_perqta, ' ', 0, 0, ordlist.ol_valorev, 0," & _
          " ordlist.ol_scont4, ordlist.ol_scont5, ordlist.ol_scont6, ordlist.ol_datord, ordlist.ol_conto, an_descr1, NULL, 0, ' ', 0," & _
          " 0, ordlist.ol_stato, 0, 1, ordlist.ol_fase, Null, Null, Null, 'N'," & _
          " ordlisttc.ol_quant01, ordlisttc.ol_quant02,ordlisttc.ol_quant03,ordlisttc.ol_quant04,ordlisttc.ol_quant05,ordlisttc.ol_quant06,ordlisttc.ol_quant07,ordlisttc.ol_quant08," & _
          " ordlisttc.ol_quant09,ordlisttc.ol_quant10,ordlisttc.ol_quant11,ordlisttc.ol_quant12,ordlisttc.ol_quant13,ordlisttc.ol_quant14,ordlisttc.ol_quant15,ordlisttc.ol_quant16," & _
          " ordlisttc.ol_quant17,ordlisttc.ol_quant18,ordlisttc.ol_quant19,ordlisttc.ol_quant20,ordlisttc.ol_quant21,ordlisttc.ol_quant22,ordlisttc.ol_quant23,ordlisttc.ol_quant24," & _
          " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, ordlist.ol_tctaglia" & _
          " FROM " & strJoinOrdlist & _
          " WHERE ordlist.ol_tipork = 'X'" & _
          " AND " & strWhereOrdlist
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        '------------------------------------------------------------------------------------------------------------
        '--- Copia i dati da ORDLIST/ARTICO per i record di tipo H, O negli Ordini
        '------------------------------------------------------------------------------------------------------------
        strSQL = "INSERT INTO TTSCHO" & strListaCampi & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTScho & ", ordlist.ol_tipork, 0, ' ', 0, 0, ordlist.ol_progr, 'O', ordlist.ol_codart," & _
          " ordlist.ol_datcons, ordlist.ol_magaz, ordlist.ol_unmis, ordlist.ol_descr, ordlist.ol_colli, 0, ordlist.ol_quant, 0, 'C', 0, 0, 'C'," & _
          " ordlist.ol_prezzo, ordlist.ol_scont1, ordlist.ol_scont2, ordlist.ol_scont3, 0, ordlist.ol_codiva, 0, ordlist.ol_prezvalc, ordlist.ol_commen," & _
          " ordlist.ol_note, ordlist.ol_controp, ordlist.ol_stasino, 0, NULL, 0, NULL, 0, 0, ordlist.ol_prelist, ordlist.ol_codcfam," & _
          " ordlist.ol_commeca, ordlist.ol_subcommeca, ordlist.ol_valore, ordlist.ol_contocontr, ordlist.ol_codcena, ordlist.ol_desint, ordlist.ol_codvuo," & _
          " 0, 0, ordlist.ol_ump, case when ordlist.ol_stato = 'S' Then 'S' Else 'N' End, ordlist.ol_lotto, 'N', 'N', ordlist.ol_ricimp, " & _
          CDataSQL(IntSetDate("01/01/1900")) & ", ordlist.ol_codclie, ordlist.ol_misura1, ordlist.ol_misura2, ordlist.ol_misura3, " & _
          CDataSQL(IntSetDate("01/01/1900")) & ", ordlist.ol_perqta, ' ', 0, 0, ordlist.ol_valorev, 0," & _
          " ordlist.ol_scont4, ordlist.ol_scont5, ordlist.ol_scont6, ordlist.ol_datord, ordlist.ol_conto, an_descr1, NULL, 0, ' ', 0," & _
          " 0, ordlist.ol_stato, 0, 1, ordlist.ol_fase, Null, Null, Null, 'N'," & _
          " ordlisttc.ol_quant01, ordlisttc.ol_quant02,ordlisttc.ol_quant03,ordlisttc.ol_quant04,ordlisttc.ol_quant05,ordlisttc.ol_quant06,ordlisttc.ol_quant07,ordlisttc.ol_quant08," & _
          " ordlisttc.ol_quant09,ordlisttc.ol_quant10,ordlisttc.ol_quant11,ordlisttc.ol_quant12,ordlisttc.ol_quant13,ordlisttc.ol_quant14,ordlisttc.ol_quant15,ordlisttc.ol_quant16," & _
          " ordlisttc.ol_quant17,ordlisttc.ol_quant18,ordlisttc.ol_quant19,ordlisttc.ol_quant20,ordlisttc.ol_quant21,ordlisttc.ol_quant22,ordlisttc.ol_quant23,ordlisttc.ol_quant24," & _
          " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, ordlist.ol_tctaglia" & _
          " FROM " & strJoinOrdlist & _
          " WHERE ordlist.ol_tipork IN ('H','O')" & _
          " AND " & strWhereOrdlist
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se selezionato inserisce i dati di tipo S da ZZDISPSCA
      '--------------------------------------------------------------------------------------------------------------
      If bckZzdispsca = True Then
        strSQL = "INSERT INTO TTSCHO " & strListaCampi & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTScho & ", 'S', ttd_anno, ttd_serie, ttd_numord, ttd_riga, ttd_progr," & _
          " 'Z', ttd_codart, ttd_datcons, ttd_codmaga, ttd_unmis, NULL, ttd_colli, 0, ttd_quant," & _
          " 0, 'C', 0, 0, 'C', 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, ' ', 0, NULL, 0, NULL," & _
          " 0, 0, 0, ' ', ttd_commecaord, ttd_subcommeca, 0, 0, 0, NULL, 0, 0, 0, ttd_ump, 'S'," & _
          " ttd_lotto, 'N', 'N', 'N', " & CDataSQL(IntSetDate("01/01/1900")) & ", ttd_codclie," & _
          " ttd_misura1, ttd_misura2, ttd_misura3, " & CDataSQL(IntSetDate("01/01/1900")) & "," & _
          " 0, ' ', 0, 0, 0, 0, 0, 0, 0, ttd_datord, 0, NULL, cast(ttd_commecaord as varchar(9)) + ' / ' + cast(ttd_taskid as varchar(9)), 0, ' ', 0, ttd_progrlp," & _
          " ' ', 0, 1, ttd_faseterz, Null, Null, Null, 'N'," & _
          " Case When ttd_tcindtagl = 1 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 2 Then ttd_quant Else 0 End , Case When ttd_tcindtagl = 3 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 4 Then ttd_quant Else 0 End, " & _
          " Case When ttd_tcindtagl = 5 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 6 Then ttd_quant Else 0 End , Case When ttd_tcindtagl = 7 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 8 Then ttd_quant Else 0 End, " & _
          " Case When ttd_tcindtagl = 9 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 10 Then ttd_quant Else 0 End , Case When ttd_tcindtagl = 11 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 12Then ttd_quant Else 0 End, " & _
          " Case When ttd_tcindtagl = 13 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 14 Then ttd_quant Else 0 End , Case When ttd_tcindtagl = 15 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 16 Then ttd_quant Else 0 End, " & _
          " Case When ttd_tcindtagl = 17 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 18 Then ttd_quant Else 0 End , Case When ttd_tcindtagl = 19 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 20 Then ttd_quant Else 0 End, " & _
          " Case When ttd_tcindtagl = 21 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 22 Then ttd_quant Else 0 End , Case When ttd_tcindtagl = 23 Then ttd_quant Else 0 End, Case When ttd_tcindtagl = 24 Then ttd_quant Else 0 End, " & _
          " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, ' '" & _
          " FROM " & strJoinZzdispsca & _
          " WHERE ttd_tipo = 'S'" & _
          " AND (NOT (ttd_tipork = 'X' OR ttd_tipork = 'Y'))" & _
          " AND " & strWhereZzdispsca
        '------------------------------------------------------------------------------------------------------------
        '--- Sopra prende solo i fabb primari ma  non raddoppia in LLC i fabb. di movord e ordlist relativi a impegni
        '--- collegati  e la parte impegni degli impegni di trasferimento
        '------------------------------------------------------------------------------------------------------------
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se selezionato inserisce i dati di tipo T da attivit (PM)
      '--------------------------------------------------------------------------------------------------------------
      If bckTaskPM = True Then
        strSQL = "INSERT INTO TTSCHO " & strListaCampi & _
          " SELECT " & CStrSQL(strDitta) & ", " & lIITTScho & ", 'T', 0, ' ', tsk_commeca, 0, tsk_taskid, " & _
          " 'T', tsk_codart, Case When tsk_deadline is null Then " & CDataSQL(Now) & " Else tsk_deadline End, Case When ar_magstock <>0 then ar_magstock Else " & nMagazInternoTransito & " End, tsk_ump, NULL, 0, 0, tsk_remqta," & _
          " 0, 'C', 0, 0, 'C', 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, ' ', 0, NULL, 0, NULL," & _
          " 0, 0, 0, ' ', tsk_commeca, ' ', tsk_remcost, tsk_contoca, tsk_codcena, NULL, 0, 0, 0, tsk_ump, tsk_rilasciato," & _
          " 0, tsk_rilasciato, 'N', 'N', case when tsk_basestart is null Then " & CDataSQL(Now) & " Else tsk_basestart End, 0," & _
          " 0, 0, 0, tsk_ultagg, tsk_perqta, ' ', 0, 0, 0, 0, 0, 0, 0, tsk_ultagg, 0, NULL, NULL, 0, ' ', 0, 0," & _
          " ' ', 1, 0, ar_ultfase, Null, Null, Null, 'N'," & _
          " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0," & _
          " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, ' '" & _
          " FROM " & strJoinTasks & _
          " WHERE tsk_tipotask IN ('5', 'B', 'C')" & _
          " AND " & strWhereTasks
        If bckSolotaskril = True Then
          strSQL = strSQL & " and tsk_rilasciato = 'S' "
        End If
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Inserisce i dati di giacenza iniziale A da ARTPRO
      '--------------------------------------------------------------------------------------------------------------

      strSQL = " SELECT mo_codart, mo_fase, mo_magaz " & _
               " FROM ttscho " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITTScho & _
               " GROUP BY mo_codart, mo_fase, mo_magaz"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTSCHO")
      '--------------------------------------------------------------------------------------------------------------
      For j = 0 To dsTmp.Tables("TTSCHO").Rows.Count - 1
        strSQL = " SELECT ap_esist, apt_esist01, apt_esist02, apt_esist03, apt_esist04, apt_esist05, apt_esist06, " & _
               " apt_esist07,apt_esist08,apt_esist09,apt_esist10,apt_esist11,apt_esist12,apt_esist13,apt_esist14," & _
               " apt_esist15,apt_esist16,apt_esist17,apt_esist18,apt_esist19,apt_esist20,apt_esist21,apt_esist22," & _
               " apt_esist23, apt_esist24 " & _
               " FROM artpro LEFT JOIN artprotc ON artpro.codditt = artprotc.codditt AND artpro.ap_codart = artprotc.apt_codart " & _
               " AND artpro.ap_fase = artprotc.apt_fase AND artpro.ap_magaz = artprotc.apt_magaz " & _
               " WHERE artpro.codditt = " & CStrSQL(strDitta) & _
               " AND ap_codart = " & CStrSQL(dsTmp.Tables("TTSCHO").Rows(j)!mo_codart) & _
               " AND ap_fase = " & NTSCStr(dsTmp.Tables("TTSCHO").Rows(j)!mo_fase) & _
               " AND ap_magaz = " & NTSCStr(dsTmp.Tables("TTSCHO").Rows(j)!mo_magaz)
        dsTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTSCHO")
        '------------------------------------------------------------------------------------------------------------
        dEsist = 0
        For i = 0 To 23
          dEsistTC(i) = 0
        Next i
        If dsTmp2.Tables("TTSCHO").Rows().Count > 0 Then
          dEsist = NTSCDec(dsTmp2.Tables("TTSCHO").Rows(0)!ap_esist)
          If NTSCStr(dsTmp2.Tables("TTSCHO").Rows(0)!apt_esist01).Trim <> "" Then
            For i = 0 To 23
              dEsistTC(i) = NTSCDec(dsTmp2.Tables("TTSCHO").Rows(0)("apt_esist" & Right("00" & (i + 1), 2)))
            Next i
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        strSQL = " INSERT INTO TTSCHO (codditt, instid, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_olprogr, " & _
                 " mo_origine, mo_codart, mo_datcons, mo_magaz, mo_fase, mo_quant, mo_esistenza, " & _
                 " mo_quant01, mo_quant02,mo_quant03,mo_quant04,mo_quant05,mo_quant06,mo_quant07,mo_quant08," & _
                 " mo_quant09,mo_quant10,mo_quant11,mo_quant12,mo_quant13,mo_quant14,mo_quant15,mo_quant16," & _
                 " mo_quant17,mo_quant18,mo_quant19,mo_quant20,mo_quant21,mo_quant22,mo_quant23,mo_quant24)" & _
                 " VALUES (" & CStrSQL(strDitta) & ", " & lIITTScho & ", 'A', 0, ' ', 0, 0, 0" & _
                 " , 'A', " & CStrSQL(dsTmp.Tables("TTSCHO").Rows(j)!mo_codart) & ", " & CDataSQL(Now) & _
                 " , " & NTSCStr(dsTmp.Tables("TTSCHO").Rows(j)!mo_magaz) & ", " & NTSCStr(dsTmp.Tables("TTSCHO").Rows(j)!mo_fase) & ", " & CDblSQL(dEsist) & ", 'S'" & _
                 " , " & CDblSQL(dEsistTC(0)) & ", " & CDblSQL(dEsistTC(1)) & ", " & CDblSQL(dEsistTC(2)) & _
                 " , " & CDblSQL(dEsistTC(3)) & ", " & CDblSQL(dEsistTC(4)) & ", " & CDblSQL(dEsistTC(5)) & _
                 " , " & CDblSQL(dEsistTC(6)) & ", " & CDblSQL(dEsistTC(7)) & ", " & CDblSQL(dEsistTC(8)) & _
                 " , " & CDblSQL(dEsistTC(9)) & ", " & CDblSQL(dEsistTC(10)) & ", " & CDblSQL(dEsistTC(11)) & _
                 " , " & CDblSQL(dEsistTC(12)) & ", " & CDblSQL(dEsistTC(13)) & ", " & CDblSQL(dEsistTC(14)) & _
                 " , " & CDblSQL(dEsistTC(15)) & ", " & CDblSQL(dEsistTC(16)) & ", " & CDblSQL(dEsistTC(17)) & _
                 " , " & CDblSQL(dEsistTC(18)) & ", " & CDblSQL(dEsistTC(19)) & ", " & CDblSQL(dEsistTC(20)) & _
                 " , " & CDblSQL(dEsistTC(21)) & ", " & CDblSQL(dEsistTC(22)) & ", " & CDblSQL(dEsistTC(23)) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next
      '------------------------------------------------------------------------------------------------------------

      If bAccorpa Then
        'Prendo tutti i magazzini associati a quelli prelevati (ovviamente non già presenti in elenco)..., 
        'e che siano di tipo 'Considera in MRP'
        strSQL = " SELECT mo_codart, mo_fase, mo_magaz " & _
                 " FROM ttscho " & _
                 " WHERE 0 = 1"
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTSCHO")

        strSQL = " SELECT tb_codmaga " & _
                 " FROM tabmaga" & _
                 " WHERE tabmaga.codditt = " & CStrSQL(strDitta) & _
                 " AND tabmaga.tb_codmaga NOT IN (SELECT mo_magaz " & _
                                                " FROM ttscho " & _
                                                " WHERE codditt = " & CStrSQL(strDitta) & _
                                                " AND instid = " & lIITTScho & ")" & _
                 " AND tabmaga.tb_magass IN (SELECT mo_magaz " & _
                                           " FROM ttscho " & _
                                           " WHERE codditt = " & CStrSQL(strDitta) & _
                                           " AND instid = " & lIITTScho & ")" & _
                 " AND tabmaga.tb_consmrp = 'S'"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          For Each dtrRow As DataRow In dttTmp.Rows
            strSQL = " SELECT mo_codart, mo_fase" & _
                     " FROM ttscho " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND instid = " & lIITTScho & _
                     " GROUP BY mo_codart, mo_fase"
            dttTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            If dttTmp2.Rows.Count > 0 Then
              For Each dtrRow2 As DataRow In dttTmp2.Rows
                dsTmp.Tables("TTSCHO").Rows.Add(dsTmp.Tables("TTSCHO").NewRow)
                dsTmp.Tables("TTSCHO").Rows(dsTmp.Tables("TTSCHO").Rows.Count - 1)!mo_codart = dtrRow2!mo_codart
                dsTmp.Tables("TTSCHO").Rows(dsTmp.Tables("TTSCHO").Rows.Count - 1)!mo_fase = dtrRow2!mo_fase
                dsTmp.Tables("TTSCHO").Rows(dsTmp.Tables("TTSCHO").Rows.Count - 1)!mo_magaz = dtrRow!tb_codmaga
                dsTmp.Tables("TTSCHO").AcceptChanges()
              Next
            End If
          Next
        End If

        For j = 0 To dsTmp.Tables("TTSCHO").Rows.Count - 1
          strSQL = " SELECT ap_esist, apt_esist01, apt_esist02, apt_esist03, apt_esist04, apt_esist05, apt_esist06, " & _
                   " apt_esist07,apt_esist08,apt_esist09,apt_esist10,apt_esist11,apt_esist12,apt_esist13,apt_esist14," & _
                   " apt_esist15,apt_esist16,apt_esist17,apt_esist18,apt_esist19,apt_esist20,apt_esist21,apt_esist22," & _
                   " apt_esist23, apt_esist24 " & _
                   " FROM artpro LEFT JOIN artprotc ON artpro.codditt = artprotc.codditt AND artpro.ap_codart = artprotc.apt_codart " & _
                   " AND artpro.ap_fase = artprotc.apt_fase AND artpro.ap_magaz = artprotc.apt_magaz " & _
                   " WHERE artpro.codditt = " & CStrSQL(strDitta) & _
                   " AND ap_codart = " & CStrSQL(dsTmp.Tables("TTSCHO").Rows(j)!mo_codart) & _
                   " AND ap_fase = " & NTSCStr(dsTmp.Tables("TTSCHO").Rows(j)!mo_fase) & _
                   " AND ap_magaz = " & NTSCStr(dsTmp.Tables("TTSCHO").Rows(j)!mo_magaz)
          dsTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTSCHO")
          '------------------------------------------------------------------------------------------------------------
          dEsist = 0
          For i = 0 To 23
            dEsistTC(i) = 0
          Next i
          If dsTmp2.Tables("TTSCHO").Rows().Count > 0 Then
            dEsist = NTSCDec(dsTmp2.Tables("TTSCHO").Rows(0)!ap_esist)
            If NTSCStr(dsTmp2.Tables("TTSCHO").Rows(0)!apt_esist01).Trim <> "" Then
              For i = 0 To 23
                dEsistTC(i) = NTSCDec(dsTmp2.Tables("TTSCHO").Rows(0)("apt_esist" & Right("00" & (i + 1), 2)))
              Next i
            End If
            'SE L'ESISTENZA E' 0 NON CREO IL RECORD
            If dEsist = 0 Then
              For i = 0 To 23
                If dEsistTC(i) <> 0 Then
                  GoTo Prosegui
                End If
              Next i
              GoTo Salta
            End If
Prosegui:
            '------------------------------------------------------------------------------------------------------------
            strSQL = " INSERT INTO TTSCHO (codditt, instid, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, mo_olprogr, " & _
                     " mo_origine, mo_codart, mo_datcons, mo_magaz, mo_fase, mo_quant, mo_esistenza, " & _
                     " mo_quant01, mo_quant02,mo_quant03,mo_quant04,mo_quant05,mo_quant06,mo_quant07,mo_quant08," & _
                     " mo_quant09,mo_quant10,mo_quant11,mo_quant12,mo_quant13,mo_quant14,mo_quant15,mo_quant16," & _
                     " mo_quant17,mo_quant18,mo_quant19,mo_quant20,mo_quant21,mo_quant22,mo_quant23,mo_quant24)" & _
                     " VALUES (" & CStrSQL(strDitta) & ", " & lIITTScho & ", 'A', 0, ' ', 0, 0, 0" & _
                     " , 'A', " & CStrSQL(dsTmp.Tables("TTSCHO").Rows(j)!mo_codart) & ", " & CDataSQL(Now) & _
                     " , " & NTSCStr(dsTmp.Tables("TTSCHO").Rows(j)!mo_magaz) & ", " & NTSCStr(dsTmp.Tables("TTSCHO").Rows(j)!mo_fase) & ", " & CDblSQL(dEsist) & ", 'S'" & _
                     " , " & CDblSQL(dEsistTC(0)) & ", " & CDblSQL(dEsistTC(1)) & ", " & CDblSQL(dEsistTC(2)) & _
                     " , " & CDblSQL(dEsistTC(3)) & ", " & CDblSQL(dEsistTC(4)) & ", " & CDblSQL(dEsistTC(5)) & _
                     " , " & CDblSQL(dEsistTC(6)) & ", " & CDblSQL(dEsistTC(7)) & ", " & CDblSQL(dEsistTC(8)) & _
                     " , " & CDblSQL(dEsistTC(9)) & ", " & CDblSQL(dEsistTC(10)) & ", " & CDblSQL(dEsistTC(11)) & _
                     " , " & CDblSQL(dEsistTC(12)) & ", " & CDblSQL(dEsistTC(13)) & ", " & CDblSQL(dEsistTC(14)) & _
                     " , " & CDblSQL(dEsistTC(15)) & ", " & CDblSQL(dEsistTC(16)) & ", " & CDblSQL(dEsistTC(17)) & _
                     " , " & CDblSQL(dEsistTC(18)) & ", " & CDblSQL(dEsistTC(19)) & ", " & CDblSQL(dEsistTC(20)) & _
                     " , " & CDblSQL(dEsistTC(21)) & ", " & CDblSQL(dEsistTC(22)) & ", " & CDblSQL(dEsistTC(23)) & ")"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
Salta:
          End If
        Next
        '------------------------------------------------------------------------------------------------------------
      End If

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

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
                                                ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                                ByVal strDaDatord As String, ByVal strADatord As String, _
                                                ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                                ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                                ByVal nDaGruppo As Integer, ByVal nAGruppo As Integer, _
                                                ByVal nDaSottogr As Integer, ByVal nASottogr As Integer, _
                                                ByVal strDaFlevas As String, ByVal strAFlevas As String, _
                                                ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                                ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                                ByVal dEsist As Decimal, ByRef dDisp() As Decimal, _
                                                ByVal strQuery As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return ComponiStringaSQL(strDitta, dtrTmp, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                               strDaDatord, strADatord, strDaDatcons, strADatcons, strSchoTipork, strSchoSerie, _
                               nDaGruppo, nAGruppo, nDaSottogr, nASottogr, strDaFlevas, strAFlevas, _
                               nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, dEsist, dDisp, _
                               strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function ComponiStringaSQL(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                                ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                                ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                                ByVal strDaDatord As String, ByVal strADatord As String, _
                                                ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                                ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                                ByVal nDaGruppo As Integer, ByVal nAGruppo As Integer, _
                                                ByVal nDaSottogr As Integer, ByVal nASottogr As Integer, _
                                                ByVal strDaFlevas As String, ByVal strAFlevas As String, _
                                                ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                                ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                                ByVal dEsist As Decimal, ByRef dDisp() As Decimal, _
                                                ByVal strQuery As String, _
                                                ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                                             strDaDatord, strADatord, strDaDatcons, strADatcons, strSchoTipork, _
                                             strSchoSerie, nDaGruppo, nAGruppo, nDaSottogr, nASottogr, _
                                             strDaFlevas, strAFlevas, nSchoDaagente, nSchoAagente, _
                                             strSchoRilasciato, strSchoOrdin, dEsist, dDisp, strQuery, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        dDisp = CType(oIn(24), Decimal())
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return ComponiStringaSQL(strDitta, dtrTmp, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                               strDaDatord, strADatord, strDaDatcons, strADatcons, strSchoTipork, strSchoSerie, _
                               nDaGruppo, nAGruppo, nDaSottogr, nASottogr, strDaFlevas, strAFlevas, _
                               nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, dEsist, dDisp, _
                               strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                               strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                               "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function ComponiStringaSQL(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                                ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                                ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                                ByVal strDaDatord As String, ByVal strADatord As String, _
                                                ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                                ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                                ByVal nDaGruppo As Integer, ByVal nAGruppo As Integer, _
                                                ByVal nDaSottogr As Integer, ByVal nASottogr As Integer, _
                                                ByVal strDaFlevas As String, ByVal strAFlevas As String, _
                                                ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                                ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                                ByVal dEsist As Decimal, ByRef dDisp() As Decimal, _
                                                ByVal strQuery As String, _
                                                ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                ByVal strClassificazioneLivello5 As String, _
                                                ByVal strConto As String, ByVal nCodlsel As Integer, _
                                                ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Dim i As Integer = 0
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                                             strDaDatord, strADatord, strDaDatcons, strADatcons, strSchoTipork, _
                                             strSchoSerie, nDaGruppo, nAGruppo, nDaSottogr, nASottogr, _
                                             strDaFlevas, strAFlevas, nSchoDaagente, nSchoAagente, _
                                             strSchoRilasciato, strSchoOrdin, dEsist, dDisp, strQuery, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, strConto, nCodlsel, strCodart, nCodlsar})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        dDisp = CType(oIn(24), Decimal())
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT ko_codart, ko_fase, ko_conto, ko_magaz, mo_unmis, td_datord, ko_tipork, ko_anno, ko_serie," & _
        " ko_numord, ko_riga, td_riferim, td_alfpar, td_numpar, td_datpar, mo_quant, mo_quaeva, mo_flevas, mo_quapre," & _
        " mo_flevapre, CASE mo_flevas WHEN 'S' THEN 0 ELSE (mo_quant - mo_quaeva) END As Quaeva," & _
        " ko_datcons, mo_prezzo, mo_valore, ko_impeg, ko_ordin, mo_scont1, mo_scont2, mo_scont3, mo_prelist," & _
        " mo_prezvalc, mo_colli, mo_coleva, mo_colpre, mo_misura1, mo_misura2, mo_misura3, mo_controp, mo_commeca," & _
        " mo_codcena, mo_codcfam, mo_datconsor, mo_provv, mo_provv2, mo_vprovv, mo_vprovv2, an_descr1," & _
        " (mo_prezzo * (100 - mo_scont1) / 100 * (100 - mo_scont2) / 100 * (100 - mo_scont3) / 100 * (100 - mo_scont4) / 100 * (100 - mo_scont5) / 100 * (100 - mo_scont6) / 100) As PRZNET," & _
        " mo_scont4, mo_scont5, mo_scont6, mo_ubicaz,"
      For i = 1 To 24
        strSQL += " movordtc.mo_quant" & Right("0" & i.ToString, 2) & " as T" & i.ToString & "o,"
      Next
      For i = 1 To 24
        strSQL += " movordtc.mo_quaeva" & Right("0" & i.ToString, 2) & " as T" & i.ToString & "c,"
      Next
      For i = 1 To 24
        strSQL += " CASE mo_flevas WHEN 'S' THEN 0 ELSE (movordtc.mo_quant" & Right("0" & i.ToString, 2) & " - movordtc.mo_quaeva" & Right("0" & i.ToString, 2) & ") END As T" & i.ToString & "r" & IIf(i < 24, ",", "").ToString
      Next
      strSQL += " FROM testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord" & _
        " INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_anno = keyord.ko_anno AND movord.mo_serie = keyord.ko_serie AND movord.mo_numord = keyord.ko_numord AND movord.mo_riga = keyord.ko_riga" & _
        " INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart" & _
        " LEFT JOIN anagra ON keyord.codditt = anagra.codditt AND keyord.ko_conto = anagra.an_conto" & _
        " LEFT JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga" & _
        " WHERE keyord.codditt = " & CStrSQL(strDitta) & _
        " AND ko_codart = " & CStrSQL(NTSCStr(dtrTmp!ko_codart)) & _
        " AND ko_magaz = " & NTSCInt(dtrTmp!ko_magaz) & _
        " AND ko_fase = " & NTSCInt(dtrTmp!ko_fase) & _
        " AND mo_flevas BETWEEN " & CStrSQL(strDaFlevas) & " AND " & CStrSQL(strAFlevas) & _
        IIf(strSchoRilasciato <> "E", " AND mo_rilasciato = " & CStrSQL(strSchoRilasciato), "").ToString & _
        IIf(strSchoSerie.Trim <> "", " AND ko_serie = " & CStrSQL(strSchoSerie), "").ToString & _
        IIf(strSchoTipork.Trim <> "", " AND ko_tipork = " & CStrSQL(strSchoTipork), "").ToString
      If (lDaCommessa <> 0) Or (lACommessa <> 999999999) Then
        strSQL += " AND mo_commeca BETWEEN " & lDaCommessa & " AND " & lACommessa
      End If
      If (NTSCDate(strDaDatord) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strADatord) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND td_datord BETWEEN " & CDataSQL(strDaDatord) & " AND " & CDataSQL(strADatord)
      End If
      If NTSCDate(strDaDatcons) <> NTSCDate(IntSetDate("01/01/1900")) Or _
         NTSCDate(strADatcons) <> NTSCDate(IntSetDate("31/12/2099")) Then
        strSQL += " AND ko_datcons BETWEEN " & CDataSQL(strDaDatcons) & " AND " & CDataSQL(strADatcons)
      End If
      If (nSchoDaagente <> 0) Or (nSchoAagente <> 9999) Then
        strSQL += " AND td_codagen BETWEEN " & nSchoDaagente & " AND " & nSchoAagente
      End If
      If (nDaGruppo <> 0) Or (nAGruppo <> 999) Then
        strSQL += " AND ar_gruppo BETWEEN " & nDaGruppo & " AND " & nAGruppo
      End If
      If (nDaSottogr <> 0) Or (nASottogr <> 9999) Then
        strSQL += " AND ar_sotgru BETWEEN " & nDaSottogr & " AND " & nASottogr
      End If
      If strClassificazioneLivello1.Trim <> "" Then strSQL = strSQL & " AND ar_codcla1 = " & CStrSQL(strClassificazioneLivello1)
      If strClassificazioneLivello2.Trim <> "" Then strSQL = strSQL & " AND ar_codcla2 = " & CStrSQL(strClassificazioneLivello2)
      If strClassificazioneLivello3.Trim <> "" Then strSQL = strSQL & " AND ar_codcla3 = " & CStrSQL(strClassificazioneLivello3)
      If strClassificazioneLivello4.Trim <> "" Then strSQL = strSQL & " AND ar_codcla4 = " & CStrSQL(strClassificazioneLivello4)
      If strClassificazioneLivello5.Trim <> "" Then strSQL = strSQL & " AND ar_codcla5 = " & CStrSQL(strClassificazioneLivello5)
      Select Case strConto
        Case "A"
          If (lDaConto <> 0) Or (lAConto <> 999999999) Then
            strSQL += " AND ko_conto BETWEEN " & lDaConto & " AND " & lAConto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND ko_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      strSQL += " ORDER BY ko_codart, ko_magaz, ko_datcons, ko_tipork, ko_anno, ko_serie, ko_numord"
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORD")
      '--------------------------------------------------------------------------------------------------------------
      ds.Tables("MOVORD").Columns.Add(New DataColumn("xx_disp", GetType(Decimal)))
      '--------------------------------------------------------------------------------------------------------------
      If Trim(strQuery) <> "" Then
        '------------------------------------------------------------------------------------------------------------
        '--- Aggiungo la where dei campi con OR e AND rimappati
        '------------------------------------------------------------------------------------------------------------
        Dim strSelect As String = ""
        If strQuery <> "" Then strSelect &= " AND " & strQuery
        '------------------------------------------------------------------------------------------------------------
        If ds.Tables("MOVORD").Rows.Count > 0 Then
          For cont As Integer = 0 To ds.Tables("MOVORD").Rows.Count - 1
            With ds.Tables("MOVORD").Rows(cont)
              If VerificaRiga(strDitta, NTSCStr(!ko_tipork), NTSCInt(!ko_anno), NTSCStr(!ko_serie), _
                NTSCInt(!ko_numord), strSelect) Then .Delete()
            End With
          Next
        End If
        '------------------------------------------------------------------------------------------------------------
        ds.Tables("MOVORD").AcceptChanges()
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
      If strSchoOrdin <> "C" Then
        '------------------------------------------------------------------------------------------------------------
        '--- Riempie l'array col le disponibilità scalari
        '------------------------------------------------------------------------------------------------------------
        If ds.Tables("MOVORD").Rows.Count > 0 Then
          ReDim dDisp(ds.Tables("MOVORD").Rows.Count)
          If Not (NTSCStr(ds.Tables("MOVORD").Rows(0)!mo_flevas) = "S") Then
            If NTSCInt(ds.Tables("MOVORD").Rows(0)!ko_ordin) = 1 Then
              dDisp(0) = dEsist + (NTSCDec(ds.Tables("MOVORD").Rows(0)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(0)!mo_quaeva))
            Else
              dDisp(0) = dEsist + ((NTSCDec(ds.Tables("MOVORD").Rows(0)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(0)!mo_quaeva)) * (-1))
            End If
            If (NTSCInt(ds.Tables("MOVORD").Rows(0)!ko_ordin) = 0) And (NTSCInt(ds.Tables("MOVORD").Rows(0)!ko_impeg) = 0) Then
              dDisp(0) = dEsist
            End If
          Else
            dDisp(0) = dEsist
          End If
          For i = 1 To ds.Tables("MOVORD").Rows.Count - 1
            If Not (NTSCStr(ds.Tables("MOVORD").Rows(i)!mo_flevas) = "S") Then
              If NTSCInt(ds.Tables("MOVORD").Rows(i)!ko_ordin) = 1 Then
                dDisp(i) = dDisp(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva))
              End If
              If NTSCInt(ds.Tables("MOVORD").Rows(i)!ko_impeg) = 1 Then
                dDisp(i) = dDisp(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva)) * (-1))
              End If
              If (NTSCInt(ds.Tables("MOVORD").Rows(i)!ko_ordin) = 0) And (NTSCInt(ds.Tables("MOVORD").Rows(i)!ko_impeg) = 0) Then
                dDisp(i) = dDisp(i - 1)
              End If
            Else
              dDisp(i) = dDisp(i - 1)
            End If
          Next
        End If
      End If
      '------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function VerificaRiga(ByVal strDitta As String, ByVal strTipork As String, _
                                           ByVal nAnno As Integer, ByVal strSerie As String, _
                                           ByVal nNumord As Integer, ByVal strQuery As String) As Boolean
    Dim strSQL As String
    Try
      strSQL = "SELECT movord.* FROM movord " & _
               " LEFT JOIN testord ON testord.codditt = movord.codditt AND td_tipork = mo_tipork AND td_anno = mo_anno AND td_serie = mo_serie AND td_numord = mo_numord " & _
               " LEFT JOIN artico ON movord.codditt = artico.codditt AND mo_codart = ar_codart " & _
               " LEFT JOIN anagra ON testord.codditt = anagra.codditt AND an_conto = td_conto " & _
               " WHERE movord.codditt = " & CStrSQL(strDitta) & _
               "   AND mo_tipork=" & CStrSQL(strTipork) & _
               "   AND mo_anno =" & nAnno & _
               "   AND mo_serie=" & CStrSQL(strSerie) & _
               "   AND mo_numord=" & nNumord & strQuery
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count = 0 Then Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                              ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                              ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                              ByVal strDaDatord As String, ByVal strADatord As String, _
                                              ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                              ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                              ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                              ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                              ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                              ByVal dEsist As Decimal, _
                                              ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                              ByVal bSchoEvasi As Boolean, ByVal bModuloCRM As Boolean, _
                                              ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                              ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                              ByVal bAmm As Boolean, ByVal strQuery As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return ComponiStringa(strDitta, dtrTmp, ds, lDaConto, lAConto, _
                            lDaCommessa, lACommessa, strDaDatord, strADatord, _
                            strDaDatcons, strADatcons, strSchoTipork, strSchoSerie, _
                            nGruppo, nSottogr, nSchoDaagente, nSchoAagente, _
                            strSchoRilasciato, strSchoOrdin, dEsist, _
                            nSchoFaseini, nSchoFasefin, bSchoEvasi, bModuloCRM, _
                            bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                            bAmm, strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                              ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                              ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                              ByVal strDaDatord As String, ByVal strADatord As String, _
                                              ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                              ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                              ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                              ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                              ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                              ByVal dEsist As Decimal, _
                                              ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                              ByVal bSchoEvasi As Boolean, ByVal bModuloCRM As Boolean, _
                                              ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                              ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                              ByVal bAmm As Boolean, ByVal strQuery As String, _
                                              ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                              ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                              ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lDaConto, lAConto, _
                            lDaCommessa, lACommessa, strDaDatord, strADatord, _
                            strDaDatcons, strADatcons, strSchoTipork, strSchoSerie, _
                            nGruppo, nSottogr, nSchoDaagente, nSchoAagente, _
                            strSchoRilasciato, strSchoOrdin, dEsist, _
                            nSchoFaseini, nSchoFasefin, bSchoEvasi, bModuloCRM, _
                            bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                            bAmm, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                            strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return ComponiStringa(strDitta, dtrTmp, ds, lDaConto, lAConto, lDaCommessa, lACommessa, strDaDatord, strADatord, _
                            strDaDatcons, strADatcons, strSchoTipork, strSchoSerie, nGruppo, nSottogr, _
                            nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, dEsist, _
                            nSchoFaseini, nSchoFasefin, bSchoEvasi, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                            strRegvis, bAmm, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                            strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                            "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                             ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                             ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                             ByVal strDaDatord As String, ByVal strADatord As String, _
                                             ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                             ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                             ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                             ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                             ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                             ByVal dEsist As Decimal, _
                                             ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                             ByVal bSchoEvasi As Boolean, ByVal bModuloCRM As Boolean, _
                                             ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                             ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                             ByVal bAmm As Boolean, ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String, _
                                             ByVal strConto As String, ByVal nCodlsel As Integer, _
                                             ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Dim strInnerJoin As String = "testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord" & _
      " INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_anno = keyord.ko_anno AND movord.mo_serie = keyord.ko_serie AND movord.mo_numord = keyord.ko_numord AND movord.mo_riga = keyord.ko_riga" & _
      " INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart" & _
      " INNER JOIN anagra ON keyord.codditt = anagra.codditt AND keyord.ko_conto = anagra.an_conto"
    Dim strSuf As String = ""
    Dim strPref As String = ""
    Dim strTaglia As String = ""
    Dim strScontV As String = "CASE WHEN td_scorpo = 'S' AND mo_scontv <> 0 " & _
                              " THEN (100 * mo_scontv) / (100 + (SELECT tb_aliq FROM tabciva WHERE tb_codciva = mo_codiva)) " & _
                              " ELSE mo_scontv END"
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lDaConto, lAConto, _
                                            lDaCommessa, lACommessa, strDaDatord, strADatord, _
                                            strDaDatcons, strADatcons, strSchoTipork, strSchoSerie, _
                                            nGruppo, nSottogr, nSchoDaagente, nSchoAagente, _
                                            strSchoRilasciato, strSchoOrdin, dEsist, nSchoFaseini, nSchoFasefin, _
                                            bSchoEvasi, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                            bAmm, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                            strClassificazioneLivello3, strClassificazioneLivello4, _
                                            strClassificazioneLivello5, strConto, nCodlsel, strCodart, nCodlsar})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT ko_codart, ko_fase, ko_conto, ko_magaz, mo_unmis, td_datord, ko_tipork, ko_anno," & _
        " ko_serie, ko_numord, ko_riga, td_riferim, td_alfpar, td_numpar, td_datpar, mo_quant, mo_quaeva, mo_flevas," & _
        " mo_quapre, mo_flevapre," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (mo_quant - mo_quaeva) END As Quaeva," & _
        " ko_datcons, mo_prezzo, mo_valore, ko_impeg, ko_ordin, mo_scont1, mo_scont2, mo_scont3, mo_prelist," & _
        " mo_prezvalc, mo_colli, mo_coleva, mo_colpre, mo_misura1, mo_misura2, mo_misura3, mo_controp, mo_commeca," & _
        " mo_codcena, mo_codcfam, mo_datconsor, mo_provv, mo_provv2, mo_vprovv, mo_vprovv2, an_descr1," & _
        " ((mo_prezzo*(100-mo_scont1)/100*(100-mo_scont2)/100*(100-mo_scont3)/100*(100-mo_scont4)/100*(100-mo_scont5)/100*(100-mo_scont6)/100*(100-mo_scontp)/100)-" & strScontV & ") As PRZNET, mo_scont4, mo_scont5, mo_scont6," & _
        " movordtc.mo_quant01 as T1o, movordtc.mo_quant02 as T2o, movordtc.mo_quant03 as T3o, movordtc.mo_quant04 as T4o, movordtc.mo_quant05 as T5o," & _
        " movordtc.mo_quant06 as T6o, movordtc.mo_quant07 as T7o, movordtc.mo_quant08 as T8o, movordtc.mo_quant09 as T9o, movordtc.mo_quant10 as T10o," & _
        " movordtc.mo_quant11 as T11o, movordtc.mo_quant12 as T12o, movordtc.mo_quant13 as T13o, movordtc.mo_quant14 as T14o, movordtc.mo_quant15 as T15o," & _
        " movordtc.mo_quant16 as T16o, movordtc.mo_quant17 as T17o, movordtc.mo_quant18 as T18o, movordtc.mo_quant19 as T19o, movordtc.mo_quant20 as T20o," & _
        " movordtc.mo_quant21 as T21o, movordtc.mo_quant22 as T22o, movordtc.mo_quant23 as T23o, movordtc.mo_quant24 as T24o," & _
        " movordtc.mo_quaeva01 as T1c, movordtc.mo_quaeva02 as T2c, movordtc.mo_quaeva03 as T3c, movordtc.mo_quaeva04 as T4c, movordtc.mo_quaeva05 as T5c," & _
        " movordtc.mo_quaeva06 as T6c, movordtc.mo_quaeva07 as T7c, movordtc.mo_quaeva08 as T8c, movordtc.mo_quaeva09 as T9c, movordtc.mo_quaeva10 as T10c," & _
        " movordtc.mo_quaeva11 as T11c, movordtc.mo_quaeva12 as T12c, movordtc.mo_quaeva13 as T13c, movordtc.mo_quaeva14 as T14c, movordtc.mo_quaeva15 as T15c," & _
        " movordtc.mo_quaeva16 as T16c, movordtc.mo_quaeva17 as T17c, movordtc.mo_quaeva18 as T18c, movordtc.mo_quaeva19 as T19c, movordtc.mo_quaeva20 as T20c," & _
        " movordtc.mo_quaeva21 as T21c, movordtc.mo_quaeva22 as T22c, movordtc.mo_quaeva23 as T23c, movordtc.mo_quaeva24 as T24c," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant01 - movordtc.mo_quaeva01) END As T1r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant02 - movordtc.mo_quaeva02) END as T2r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant03 - movordtc.mo_quaeva03) END As T3r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant04 - movordtc.mo_quaeva04) END As T4r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant05 - movordtc.mo_quaeva05) END As T5r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant06 - movordtc.mo_quaeva06) END As T6r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant07 - movordtc.mo_quaeva07) END As T7r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant08 - movordtc.mo_quaeva08) END As T8r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant09 - movordtc.mo_quaeva09) END As T9r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant10 - movordtc.mo_quaeva10) END As T10r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant11 - movordtc.mo_quaeva11) END As T11r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant12 - movordtc.mo_quaeva12) END As T12r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant13 - movordtc.mo_quaeva13) END As T13r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant14 - movordtc.mo_quaeva14) END As T14r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant15 - movordtc.mo_quaeva15) END As T15r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant16 - movordtc.mo_quaeva16) END As T16r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant17 - movordtc.mo_quaeva17) END As T17r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant18 - movordtc.mo_quaeva18) END As T18r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant19 - movordtc.mo_quaeva19) END As T19r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant20 - movordtc.mo_quaeva20) END As T20r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant21 - movordtc.mo_quaeva21) END As T21r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant22 - movordtc.mo_quaeva22) END As T22r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant23 - movordtc.mo_quaeva23) END As T23r," & _
        " CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (movordtc.mo_quant24 - movordtc.mo_quaeva24) END As T24r," & _
        " ko_commecap"
      '--------------------------------------------------------------------------------------------------------------
      '--- ko_commecap aggiunto alla select perche c'e' nella order by se no da errore
      '--------------------------------------------------------------------------------------------------------------
      strSQL += " FROM " & strInnerJoin & _
        " LEFT JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga" & _
        " WHERE keyord.codditt = " & CStrSQL(strDitta) & _
        " AND ko_codart = " & CStrSQL(dtrTmp!ko_codart) & _
        " AND ko_fase = " & NTSCStr(dtrTmp!ko_fase) & _
        " AND ko_magaz = " & NTSCStr(dtrTmp!ko_magaz) & _
        IIf(bSchoEvasi = True, " AND mo_flevas = 'C'", "").ToString & _
        IIf(strSchoTipork <> "", " AND ko_tipork = " & CStrSQL(strSchoTipork), "").ToString & _
        IIf(strSchoRilasciato <> "E", " AND mo_rilasciato = " & CStrSQL(strSchoRilasciato), "").ToString & _
        IIf(nGruppo > 0, " AND ar_gruppo = " & nGruppo, "").ToString & _
        IIf(nSottogr > 0, " AND ar_sotgru = " & nSottogr, "").ToString & _
        IIf(strSchoSerie <> "", " AND ko_serie = " & CStrSQL(strSchoSerie), "").ToString
      If (NTSCInt(nSchoDaagente) <> 0) Or (NTSCInt(nSchoAagente) <> 9999) Then
        strSQL += " AND td_codagen BETWEEN " & nSchoDaagente & " AND " & nSchoAagente
      End If
      If (NTSCDate(strDaDatord) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strADatord) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND td_datord BETWEEN " & CDataSQL(strDaDatord) & " AND " & CDataSQL(strADatord)
      End If
      If (NTSCDate(strDaDatcons) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strADatcons) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND ko_datcons BETWEEN " & CDataSQL(strDaDatcons) & " AND " & CDataSQL(strADatcons)
      End If
      If strSchoOrdin <> "X" Then
        If (lDaCommessa <> 0) Or (lACommessa <> 999999999) Then
          strSQL += " AND mo_commeca BETWEEN " & lDaCommessa & " AND " & lACommessa
        End If
      Else
        strSQL += " AND ko_commecap = " & NTSCStr(dtrTmp!ko_commecap)
      End If
      If strSchoOrdin = "C" Then
        strSQL += " AND ko_conto = " & NTSCStr(dtrTmp!ko_conto)
      Else
        Select Case strConto
          Case "A"
            If (lDaConto <> 0) Or (lAConto <> 999999999) Then
              strSQL += " AND ko_conto BETWEEN " & lDaConto & " AND " & lAConto
            End If
          Case "B"
            If nCodlsel <> 0 Then
              strSQL += " AND ko_conto IN (SELECT lse_conto FROM listsel" & _
                                         " WHERE codditt = " & CStrSQL(strDitta) & _
                                         " AND lse_codlsel = " & nCodlsel & ")"
            End If
        End Select
        Select Case strCodart
          Case "A"
            If (nSchoFaseini <> 0) Or (nSchoFasefin <> 9999) Then
              strSQL += " AND mo_fase BETWEEN " & nSchoFaseini & " AND " & nSchoFasefin
            End If
          Case "B"
            If nCodlsar <> 0 Then
              strSQL += _
                " AND CAST(mo_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(mo_fase AS VARCHAR(4))" & _
                " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lsa_codlsar = " & nCodlsar & _
                " AND lsa_codart = " & CStrSQL(dtrTmp!ko_codart) & ")"
            End If
        End Select
        If (bModuloCRM = True) And (bIsCRMUser = True) Then
          strSQL += " AND ("
          If strAccvis <> "T" Then
            strSQL += "(ko_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
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
      End If
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery, strSQL)
      '--------------------------------------------------------------------------------------------------------------
      Select Case strSchoOrdin
        Case "A"
          strSQL += " ORDER BY ko_codart, ko_fase, ko_magaz, ko_datcons," & _
            " ko_tipork, ko_anno, ko_serie, ko_numord"
        Case "C"
          strSQL += " ORDER BY ko_conto, ko_codart,  ko_fase,  ko_magaz," & _
            " ko_datcons, ko_tipork, ko_anno, ko_serie, ko_numord"
        Case "X"
          strSQL += " ORDER BY ko_codart,  ko_fase,  ko_magaz, ko_commecap, ko_datcons," & _
            " ko_tipork, ko_anno, ko_serie, ko_numord"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORD")
      '--------------------------------------------------------------------------------------------------------------
      ds.Tables("MOVORD").Columns.Add(New DataColumn("xx_disp", GetType(Decimal)))
      ds.Tables("MOVORD").Columns.Add(New DataColumn("mo_ubicaz", GetType(Integer)))
      '--------------------------------------------------------------------------------------------------------------
      strPref = "T"
      For i = 1 To 24
        strSuf = "o"
        strTaglia = "xx_" & strPref & i & strSuf
        ds.Tables("MOVORD").Columns.Add(New DataColumn(strTaglia, GetType(Decimal)))
        '------------------------------------------------------------------------------------------------------------
        strSuf = "c"
        strTaglia = "xx_" & strPref & i & strSuf
        ds.Tables("MOVORD").Columns.Add(New DataColumn(strTaglia, GetType(Decimal)))
        '------------------------------------------------------------------------------------------------------------
        strSuf = "r"
        strTaglia = "xx_" & strPref & i & strSuf
        ds.Tables("MOVORD").Columns.Add(New DataColumn(strTaglia, GetType(Decimal)))
      Next
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

  Public Overridable Function Apri(ByVal strDitta As String, ByRef ds As DataSet, _
                                   ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                   ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                   ByVal strDaDatord As String, ByVal strADatord As String, _
                                   ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                   ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                   ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                   ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                   ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                   ByVal nSchoDamagaz As Integer, ByVal nSchoAmagaz As Integer, _
                                   ByVal strSchoDacodart As String, ByVal strSchoAcodart As String, _
                                   ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                   ByVal bSchoEvasi As Boolean, ByVal strGrsoCodcfam As String, _
                                   ByVal bGrsoModTCO As Boolean, ByVal nGrsoAnnotco As Integer, _
                                   ByVal nGrsoCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                   ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                   ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                   ByVal bAmm As Boolean, ByVal strQuery As String) As Boolean
   
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return Apri(strDitta, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                  strDaDatord, strADatord, strDaDatcons, strADatcons, _
                  strSchoTipork, strSchoSerie, nGruppo, nSottogr, _
                  nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, _
                  nSchoDamagaz, nSchoAmagaz, strSchoDacodart, strSchoAcodart, _
                  nSchoFaseini, nSchoFasefin, bSchoEvasi, strGrsoCodcfam, _
                  bGrsoModTCO, nGrsoAnnotco, nGrsoCodstag, bModuloCRM, _
                  bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                  bAmm, strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Apri(ByVal strDitta As String, ByRef ds As DataSet, _
                                   ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                   ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                   ByVal strDaDatord As String, ByVal strADatord As String, _
                                   ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                   ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                   ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                   ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                   ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                   ByVal nSchoDamagaz As Integer, ByVal nSchoAmagaz As Integer, _
                                   ByVal strSchoDacodart As String, ByVal strSchoAcodart As String, _
                                   ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                   ByVal bSchoEvasi As Boolean, ByVal strGrsoCodcfam As String, _
                                   ByVal bGrsoModTCO As Boolean, ByVal nGrsoAnnotco As Integer, _
                                   ByVal nGrsoCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                   ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                   ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                   ByVal bAmm As Boolean, ByVal strQuery As String, _
                                   ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                   ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                   ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                                    strDaDatord, strADatord, strDaDatcons, strADatcons, _
                                    strSchoTipork, strSchoSerie, nGruppo, nSottogr, _
                                    nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, _
                                    nSchoDamagaz, nSchoAmagaz, strSchoDacodart, strSchoAcodart, _
                                    nSchoFaseini, nSchoFasefin, bSchoEvasi, strGrsoCodcfam, _
                                    bGrsoModTCO, nGrsoAnnotco, nGrsoCodstag, bModuloCRM, _
                                    bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                    bAmm, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                    strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return Apri(strDitta, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                  strDaDatord, strADatord, strDaDatcons, strADatcons, _
                  strSchoTipork, strSchoSerie, nGruppo, nSottogr, _
                  nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, _
                  nSchoDamagaz, nSchoAmagaz, strSchoDacodart, strSchoAcodart, _
                  nSchoFaseini, nSchoFasefin, bSchoEvasi, strGrsoCodcfam, _
                  bGrsoModTCO, nGrsoAnnotco, nGrsoCodstag, bModuloCRM, _
                  bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                  bAmm, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                  strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                  "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Apri(ByVal strDitta As String, ByRef ds As DataSet, _
                                   ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                   ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                   ByVal strDaDatord As String, ByVal strADatord As String, _
                                   ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                   ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                   ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                   ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                   ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                   ByVal nSchoDamagaz As Integer, ByVal nSchoAmagaz As Integer, _
                                   ByVal strSchoDacodart As String, ByVal strSchoAcodart As String, _
                                   ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                   ByVal bSchoEvasi As Boolean, ByVal strGrsoCodcfam As String, _
                                   ByVal bGrsoModTCO As Boolean, ByVal nGrsoAnnotco As Integer, _
                                   ByVal nGrsoCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                   ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                   ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                   ByVal bAmm As Boolean, ByVal strQuery As String, _
                                   ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                   ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                   ByVal strClassificazioneLivello5 As String, _
                                   ByVal strConto As String, ByVal nCodlsel As Integer, _
                                   ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strInnerJoin As String = "testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord" & _
        " INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_anno = keyord.ko_anno AND movord.mo_serie = keyord.ko_serie AND movord.mo_numord = keyord.ko_numord AND movord.mo_riga = keyord.ko_riga" & _
        " INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart" & _
        " INNER JOIN anagra ON keyord.codditt = anagra.codditt AND keyord.ko_conto = anagra.an_conto"

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                                    strDaDatord, strADatord, strDaDatcons, strADatcons, _
                                    strSchoTipork, strSchoSerie, nGruppo, nSottogr, _
                                    nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, _
                                    nSchoDamagaz, nSchoAmagaz, strSchoDacodart, strSchoAcodart, _
                                    nSchoFaseini, nSchoFasefin, bSchoEvasi, strGrsoCodcfam, _
                                    bGrsoModTCO, nGrsoAnnotco, nGrsoCodstag, bModuloCRM, _
                                    bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                    bAmm, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                    strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case strSchoOrdin
        Case "A" : strSQL = "SELECT DISTINCT ko_codart, ko_fase, ko_magaz, mo_ump"
        Case "C" : strSQL = "SELECT DISTINCT ko_conto, ko_codart, ko_fase, ko_magaz, mo_ump"
        Case "X" : strSQL = "SELECT DISTINCT ko_codart, ko_fase, ko_magaz, ko_commecap, mo_ump"
      End Select
      strSQL += " FROM " & strInnerJoin & _
        " WHERE keyord.codditt = " & CStrSQL(strDitta) & _
        IIf(nGruppo > 0, " AND ar_gruppo =  " & nGruppo, "").ToString & _
        IIf(nSottogr > 0, " AND ar_sotgru = " & nSottogr, "").ToString & _
        IIf(strGrsoCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(strGrsoCodcfam), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
        IIf(strSchoSerie <> "", " AND ko_serie = " & CStrSQL(strSchoSerie), "").ToString & _
        IIf(strSchoTipork <> "", " AND ko_tipork = " & CStrSQL(strSchoTipork), "").ToString & _
        IIf(strSchoRilasciato <> "E", " AND mo_rilasciato = " & CStrSQL(strSchoRilasciato), "").ToString & _
        IIf(bSchoEvasi = True, " AND mo_flevas = 'C'", "").ToString & _
        IIf(strSchoOrdin = "X", " AND ar_gescomm = 'S'", "").ToString
      If (NTSCInt(nSchoDamagaz) <> 0) Or (NTSCInt(nSchoAmagaz) <> 9999) Then
        strSQL += " AND ko_magaz BETWEEN " & nSchoDamagaz & " AND " & nSchoAmagaz
      End If
      If (NTSCInt(nSchoDaagente) <> 0) Or (NTSCInt(nSchoAagente) <> 9999) Then
        strSQL += " AND td_codagen BETWEEN " & nSchoDaagente & " AND " & nSchoAagente
      End If
      If (NTSCInt(lDaCommessa) <> 0) Or (NTSCInt(lACommessa) <> 999999999) Then
        strSQL += " AND mo_commeca BETWEEN " & lDaCommessa & " AND " & lACommessa
      End If
      If (NTSCDate(strDaDatord) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strADatord) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND td_datord BETWEEN " & CDataSQL(strDaDatord) & " AND " & CDataSQL(strADatord)
      End If
      If (NTSCDate(strDaDatcons) <> NTSCDate(IntSetDate("01/01/1900"))) Or _
         (NTSCDate(strADatcons) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL += " AND ko_datcons BETWEEN " & CDataSQL(strDaDatcons) & " AND " & CDataSQL(strADatcons)
      End If
      Select Case strConto
        Case "A"
          If (NTSCInt(lDaConto) <> 0) Or (NTSCInt(lAConto) <> 999999999) Then
            strSQL += " AND ko_conto BETWEEN " & lDaConto & " AND " & lAConto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND ko_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      Select Case strCodart
        Case "A"
          If (strSchoDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or (strSchoAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strSQL += " AND ko_codart BETWEEN '" & strSchoDacodart & "' AND '" & strSchoAcodart & "'"
          End If
          If (nSchoFaseini <> 0) Or (nSchoFasefin <> 9999) Then
            strSQL += " AND mo_fase BETWEEN " & nSchoFaseini & " AND " & nSchoFasefin
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL += _
              " AND CAST(mo_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(mo_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      If bGrsoModTCO = True Then
        If nGrsoAnnotco > 0 Then strSQL += " AND artico.ar_anno = " & nGrsoAnnotco
        If nGrsoCodstag > 0 Then strSQL += " AND artico.ar_codstag = " & nGrsoCodstag
      End If
      If (bModuloCRM = True) And (bIsCRMUser = True) Then
        strSQL += " AND ("
        If strAccvis <> "T" Then
          strSQL += "(an_tipo = 'C'" & _
            " AND an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
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
      TraduciWhere(strQuery, strSQL)
      '--------------------------------------------------------------------------------------------------------------
      Select Case strSchoOrdin
        Case "A"
          strSQL += " GROUP BY ko_codart, ko_fase, ko_magaz, mo_ump" & _
            " ORDER BY ko_codart, ko_fase, ko_magaz, mo_ump"
        Case "C"
          strSQL += " GROUP BY ko_conto, ko_codart, ko_fase, ko_magaz, mo_ump" & _
            " ORDER BY ko_conto, ko_codart, ko_fase,  ko_magaz, mo_ump"
        Case "X"
          strSQL += " GROUP BY ko_codart, ko_fase, ko_magaz, ko_commecap, mo_ump" & _
            " ORDER BY ko_codart, ko_fase, ko_magaz, ko_commecap, mo_ump"
      End Select
      '--------------------------------------------------------------------------------------------------------------
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORD")
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

  Public Overridable Function GetQueryStampaWord(ByVal strDitta As String, ByVal bStampaWordRaggruppata As Boolean, _
                                                 ByVal bckEvasi As Boolean, ByVal stredDamagaz As String, _
                                                 ByVal stredAmagaz As String, ByVal stredDaagente As String, _
                                                 ByVal stredAagente As String, ByVal stredDadatord As String, _
                                                 ByVal stredAdatord As String, ByVal stredDadatcons As String, _
                                                 ByVal stredAdatcons As String, ByVal stredDaconto As String, _
                                                 ByVal stredAconto As String, ByVal stredDacodart As String, _
                                                 ByVal stredAcodart As String, ByVal stredCommecaini As String, _
                                                 ByVal stredCommecafin As String, ByVal stredFaseini As String, _
                                                 ByVal stredFasefin As String, ByVal stredGruppo As String, _
                                                 ByVal stredSottogr As String, ByVal stredSerie As String, _
                                                 ByVal strcbRilasciato As String, ByVal bckTipork As Boolean, _
                                                 ByVal stredCodcfam As String, ByVal bModTCO As Boolean, _
                                                 ByVal bckSelAnnoStag As Boolean, ByVal stredAnnotco As String, _
                                                 ByVal stredCodstag As String, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal strcbTipork As String, _
                                                 ByVal strQuery As String) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return GetQueryStampaWord(strDitta, bStampaWordRaggruppata, bckEvasi, stredDamagaz, _
                                stredAmagaz, stredDaagente, stredAagente, stredDadatord, _
                                stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, _
                                stredAconto, stredDacodart, stredAcodart, stredCommecaini, _
                                stredCommecafin, stredFaseini, stredFasefin, stredGruppo, _
                                stredSottogr, stredSerie, strcbRilasciato, bckTipork, _
                                stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, _
                                stredCodstag, bModuloCRM, bIsCRMUser, strAccvis, _
                                lCodorgaOperat, strRegvis, bAmm, strcbTipork, strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetQueryStampaWord(ByVal strDitta As String, ByVal bStampaWordRaggruppata As Boolean, _
                                                 ByVal bckEvasi As Boolean, ByVal stredDamagaz As String, _
                                                 ByVal stredAmagaz As String, ByVal stredDaagente As String, _
                                                 ByVal stredAagente As String, ByVal stredDadatord As String, _
                                                 ByVal stredAdatord As String, ByVal stredDadatcons As String, _
                                                 ByVal stredAdatcons As String, ByVal stredDaconto As String, _
                                                 ByVal stredAconto As String, ByVal stredDacodart As String, _
                                                 ByVal stredAcodart As String, ByVal stredCommecaini As String, _
                                                 ByVal stredCommecafin As String, ByVal stredFaseini As String, _
                                                 ByVal stredFasefin As String, ByVal stredGruppo As String, _
                                                 ByVal stredSottogr As String, ByVal stredSerie As String, _
                                                 ByVal strcbRilasciato As String, ByVal bckTipork As Boolean, _
                                                 ByVal stredCodcfam As String, ByVal bModTCO As Boolean, _
                                                 ByVal bckSelAnnoStag As Boolean, ByVal stredAnnotco As String, _
                                                 ByVal stredCodstag As String, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal strcbTipork As String, _
                                                 ByVal strQuery As String, _
                                                 ByVal strClassificazioneLivello1 As String, _
                                                 ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, _
                                                 ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bStampaWordRaggruppata, bckEvasi, stredDamagaz, _
                                             stredAmagaz, stredDaagente, stredAagente, stredDadatord, _
                                             stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, _
                                             stredAconto, stredDacodart, stredAcodart, stredCommecaini, _
                                             stredCommecafin, stredFaseini, stredFasefin, stredGruppo, _
                                             stredSottogr, stredSerie, strcbRilasciato, bckTipork, _
                                             stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, _
                                             stredCodstag, bModuloCRM, bIsCRMUser, strAccvis, _
                                             lCodorgaOperat, strRegvis, bAmm, strcbTipork, strQuery, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CStr(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return GetQueryStampaWord(strDitta, bStampaWordRaggruppata, bckEvasi, stredDamagaz, _
                                stredAmagaz, stredDaagente, stredAagente, stredDadatord, _
                                stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, _
                                stredAconto, stredDacodart, stredAcodart, stredCommecaini, _
                                stredCommecafin, stredFaseini, stredFasefin, stredGruppo, _
                                stredSottogr, stredSerie, strcbRilasciato, bckTipork, _
                                stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, _
                                stredCodstag, bModuloCRM, bIsCRMUser, strAccvis, _
                                lCodorgaOperat, strRegvis, bAmm, strcbTipork, strQuery, _
                                strClassificazioneLivello1, strClassificazioneLivello2, strClassificazioneLivello3, _
                                strClassificazioneLivello4, strClassificazioneLivello5, "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetQueryStampaWord(ByVal strDitta As String, ByVal bStampaWordRaggruppata As Boolean, _
                                                 ByVal bckEvasi As Boolean, ByVal stredDamagaz As String, _
                                                 ByVal stredAmagaz As String, ByVal stredDaagente As String, _
                                                 ByVal stredAagente As String, ByVal stredDadatord As String, _
                                                 ByVal stredAdatord As String, ByVal stredDadatcons As String, _
                                                 ByVal stredAdatcons As String, ByVal stredDaconto As String, _
                                                 ByVal stredAconto As String, ByVal stredDacodart As String, _
                                                 ByVal stredAcodart As String, ByVal stredCommecaini As String, _
                                                 ByVal stredCommecafin As String, ByVal stredFaseini As String, _
                                                 ByVal stredFasefin As String, ByVal stredGruppo As String, _
                                                 ByVal stredSottogr As String, ByVal stredSerie As String, _
                                                 ByVal strcbRilasciato As String, ByVal bckTipork As Boolean, _
                                                 ByVal stredCodcfam As String, ByVal bModTCO As Boolean, _
                                                 ByVal bckSelAnnoStag As Boolean, ByVal stredAnnotco As String, _
                                                 ByVal stredCodstag As String, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal strcbTipork As String, _
                                                 ByVal strQuery As String, _
                                                 ByVal strClassificazioneLivello1 As String, _
                                                 ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, _
                                                 ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String, _
                                                 ByVal strConto As String, ByVal nCodlsel As Integer, _
                                                 ByVal strCodart As String, ByVal nCodlsar As Integer) As String
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim strJoin As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bStampaWordRaggruppata, bckEvasi, stredDamagaz, _
                                             stredAmagaz, stredDaagente, stredAagente, stredDadatord, _
                                             stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, _
                                             stredAconto, stredDacodart, stredAcodart, stredCommecaini, _
                                             stredCommecafin, stredFaseini, stredFasefin, stredGruppo, _
                                             stredSottogr, stredSerie, strcbRilasciato, bckTipork, _
                                             stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, _
                                             stredCodstag, bModuloCRM, bIsCRMUser, strAccvis, _
                                             lCodorgaOperat, strRegvis, bAmm, strcbTipork, strQuery, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, strConto, nCodlsel, strCodart, nCodlsar})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CStr(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strWhere = ComponiWhereStampaWord(strDitta, strQuery)
      '--------------------------------------------------------------------------------------------------------------
      If bStampaWordRaggruppata = False Then
        strSQL = "SELECT testord.*, movord.mo_riga, movord.mo_codart, movord.mo_quant, movord.mo_datcons," & _
          " movord.mo_note, movord_1.mo_codart As CODART, movord_1.mo_descr As DESCR, movord_1.mo_desint AS desint," & _
          " movord_1.mo_quant As QUANT, movord_1.mo_colli As COLLI, movord_1.mo_unmis As UNMIS, movord_1.mo_ump As UMP," & _
          " case when td_serie = ' ' Then Str(td_numord) Else Str(td_numord) + '/' + td_serie End AS xx_numord," & _
          " artico.ar_contriva, artico.ar_sostituito, artico.ar_codnomc, artico.ar_sostit, artico.ar_ubicaz," & _
          " artico.ar_note, artico.ar_gif1, artico.ar_gif2, artico.ar_codappr, artico.ar_gesvar, artico_1.ar_codroot," & _
          " artico.ar_codalt, valvari.vv_desvvar, tabcfam.tb_descfam, tabmarc.tb_desmarc, tabappr.tb_desappr," & _
          " anagra_1.an_descr1 As DESFORN, artico_2.ar_note As NOTEARTICOLO, artico_1.ar_gesvar As GESVAR," & _
          " anagra.an_descr1, anagra.an_faxtlx, anagra.an_email, anagra.an_usaem, anagra_2.an_conto As ContoAgente1," & _
          " anagra_2.an_descr1 As DescrAgente1, anagra_2.an_faxtlx As FaxtlxAgente1, anagra_2.an_email As EmailAgente1," & _
          " anagra_2.an_usaem As UsaemAgente1, anagra_3.an_conto As ContoAgente2, anagra_3.an_descr1 As DescrAgente2," & _
          " anagra_3.an_faxtlx As FaxtlxAgente2, anagra_3.an_email As EmailAgente2, anagra_3.an_usaem As UsaemAgente2" & _
          " FROM testord INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto" & _
          " INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord" & _
          " INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_anno = keyord.ko_anno AND movord.mo_serie = keyord.ko_serie AND movord.mo_numord = keyord.ko_numord AND movord.mo_riga = keyord.ko_riga" & _
          " INNER JOIN artico ON artico.codditt = movord.codditt AND artico.ar_codart = movord.mo_codart" & _
          " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam" & _
          " LEFT JOIN artico AS artico_2 ON artico.codditt = artico_2.codditt AND artico.ar_codroot = artico_2.ar_codart" & _
          " LEFT JOIN tabmarc ON artico.codditt = tabmarc.codditt AND artico.ar_codmarc = tabmarc.tb_codmarc" & _
          " LEFT JOIN tabappr ON artico.codditt = tabappr.codditt AND artico.ar_codappr = tabappr.tb_codappr" & _
          " LEFT JOIN valvari ON artico.codditt = valvari.codditt AND artico.ar_codvar2 = valvari.vv_valvari" & _
          " LEFT JOIN movord AS movord_1 ON movord.codditt = movord_1.codditt AND movord.mo_tipork = movord_1.mo_tiporkor AND movord.mo_anno = movord_1.mo_annoor AND movord.mo_serie = movord_1.mo_serieor AND movord.mo_numord = movord_1.mo_numordor AND movord.mo_riga = movord_1.mo_rigaor" & _
          " LEFT JOIN artico AS artico_1 ON movord_1.codditt = artico_1.codditt AND movord_1.mo_codart = artico_1.ar_codart" & _
          " LEFT JOIN anagra AS anagra_1 ON artico_1.codditt = anagra_1.codditt AND artico_1.ar_forn = anagra_1.an_conto" & _
          " LEFT JOIN tabcage ON testord.codditt = tabcage.codditt AND testord.td_codagen = tabcage.tb_codcage" & _
          " LEFT JOIN anagra AS anagra_2 ON tabcage.codditt = anagra_2.codditt AND tabcage.tb_codforn = anagra_2.an_conto" & _
          " LEFT JOIN tabcage AS tabcage_1 ON testord.codditt = tabcage_1.codditt AND testord.td_codagen2 = tabcage_1.tb_codcage" & _
          " LEFT JOIN anagra AS anagra_3 ON tabcage_1.codditt = anagra_3.codditt AND tabcage_1.tb_codforn = anagra_3.an_conto"
      Else
        strSQL = "SELECT td_conto, td_anno, td_serie, td_numord, an_descr1, mo_codart, Sum(mo_quant) As Quantita," & _
          " ar_contriva, ar_sostituito, ar_codnomc, ar_sostit, ar_ubicaz, ar_codroot, ar_gif1, ar_gif2, ar_codappr," & _
          " ar_codalt, an_descr1, tb_descfam, tb_desappr, tb_desmarc, vv_desvvar" & _
          " FROM testord INNER JOIN anagra ON testord.td_conto = anagra.an_conto AND testord.codditt = anagra.codditt" & _
          " INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord" & _
          " INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_anno = keyord.ko_anno AND movord.mo_serie = keyord.ko_serie AND movord.mo_numord = keyord.ko_numord AND movord.mo_riga = keyord.ko_riga" & _
          " INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart" & _
          " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam" & _
          " LEFT JOIN tabappr ON artico.codditt = tabappr.codditt AND artico.ar_codappr = tabappr.tb_codappr" & _
          " LEFT JOIN tabmarc ON artico.codditt = tabmarc.codditt AND artico.ar_codmarc = tabmarc.tb_codmarc" & _
          " LEFT JOIN valvari ON artico.codditt = valvari.codditt AND artico.ar_codvar2 = valvari.vv_valvari" & _
          " LEFT JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto"
      End If
      strSQL += " WHERE testord.codditt = " & CStrSQL(strDitta) & _
        " AND td_tipork <> 'Y'" & _
        IIf(bckEvasi = True, " AND movord.mo_flevas = 'C'", "").ToString & _
        IIf(NTSCInt(stredGruppo) > 0, " AND artico.ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
        IIf(NTSCInt(stredSottogr) > 0, " AND artico.ar_sotgru = " & NTSCInt(stredSottogr), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
        IIf(stredSerie.Trim <> "", " AND keyord.ko_serie = " & CStrSQL(stredSerie), "").ToString & _
        IIf(bckTipork = True, " AND testord.td_tipork = " & CStrSQL(strcbTipork), "").ToString & _
        IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString
      If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
        strSQL += " AND keyord.ko_magaz BETWEEN " & NTSCInt(stredDamagaz) & " AND " & NTSCInt(stredAmagaz)
      End If
      If (NTSCInt(stredDaagente) <> 0) Or (NTSCInt(stredAagente) <> 9999) Then
        strSQL += " AND testord.td_codagen BETWEEN " & NTSCInt(stredDaagente) & " AND " & NTSCInt(stredAagente)
      End If
      If (NTSCDate(stredDadatord) <> NTSCDate("01/01/1900")) Or (NTSCDate(stredAdatord) <> NTSCDate("31/12/2099")) Then
        strSQL += " AND testord.td_datord BETWEEN " & CDataSQL(stredDadatord) & " AND " & CDataSQL(stredAdatord)
      End If
      If (NTSCDate(stredDadatcons) <> NTSCDate("01/01/1900")) Or (NTSCDate(stredAdatcons) <> NTSCDate("31/12/2099")) Then
        strSQL += " AND keyord.ko_datcons BETWEEN " & CDataSQL(stredDadatcons) & " AND " & CDataSQL(stredAdatcons)
      End If
      If (NTSCInt(stredCommecaini) <> 0) Or (NTSCInt(stredCommecafin) <> 999999999) Then
        strSQL += " AND movord.mo_commeca BETWEEN " & NTSCInt(stredCommecaini) & " AND " & NTSCInt(stredCommecafin)
      End If
      Select Case strcbRilasciato
        Case "N" : strSQL += " AND movord.mo_rilasciato = 'N'"
        Case "S" : strSQL += " AND movord.mo_rilasciato = 'S'"
      End Select

      Select Case strConto
        Case "A"
          If (NTSCInt(stredDaconto) <> 0) Or (NTSCInt(stredAconto) <> 999999999) Then
            strSQL += " AND keyord.ko_conto BETWEEN " & NTSCInt(stredDaconto) & " AND " & NTSCInt(stredAconto)
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND keyord.ko_conto IN (SELECT lse_conto FROM listsel" & _
                                              " WHERE codditt = " & CStrSQL(strDitta) & _
                                              " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      Select Case strCodart
        Case "A"
          If (stredDacodart <> "".PadLeft(CLN__STD.CodartMaxLen)) Or (stredAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strSQL += " AND keyord.ko_codart BETWEEN " & CStrSQL(stredDacodart) & " AND " & CStrSQL(stredAcodart)
          End If
          If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
            strSQL += " AND movord.mo_fase BETWEEN " & NTSCInt(stredFaseini) & " AND " & NTSCInt(stredFasefin)
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL += _
              " AND CAST(keyord.ko_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(movord.mo_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      If (bModTCO = True) And (bckSelAnnoStag = True) Then
        If NTSCInt(stredAnnotco) > 0 Then strSQL += " AND artico.ar_anno = " & NTSCInt(stredAnnotco)
        If NTSCInt(stredCodstag) > 0 Then strSQL += " AND artico.ar_codstag = " & NTSCInt(stredCodstag)
      End If
      If (bModuloCRM = True) And (bIsCRMUser = True) Then
        strSQL += " AND ("
        If strAccvis <> "T" Then
          strSQL += "(anagra.an_tipo = 'C' AND anagra.an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
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
        If bAmm = False Then strSQL += " AND anagra.an_tipo <> 'F'" Else strSQL += " OR anagra.an_tipo <> 'C'"
        strSQL += ")"
      End If
      strSQL += strWhere
      If bStampaWordRaggruppata = False Then
        strSQL += " ORDER BY testord.td_conto, testord.td_anno, testord.td_serie, testord.td_numord, movord.mo_tipork, movord.mo_anno," & _
          " movord.mo_serie, movord.mo_numord, movord.mo_riga, artico_1.ar_gesvar DESC," & _
          " movord_1.mo_quant DESC"
      Else
        strSQL += " GROUP BY td_conto, td_anno, td_serie, td_numord, an_descr1, mo_codart," & _
          " ar_contriva, ar_sostituito, ar_codnomc, ar_sostit, ar_ubicaz, ar_codroot," & _
          " ar_gif1, ar_gif2, ar_codappr, ar_codalt, an_descr1," & _
          " tb_descfam, tb_desappr, tb_desmarc, vv_desvvar" & _
          " ORDER BY td_conto, mo_codart"
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ComponiWhereStampaWord(ByVal strDitta As String, ByVal strQuery As String) As String
    Dim strSQL As String = ""
    Try

      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strQuery, strSQL)

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Function Grs1GetTaglie(ByVal strDitta As String, ByVal strCodart As String, _
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

  Public Overridable Function Grs1GetRelease(ByRef ds As DataSet) As Boolean
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

  Public Overridable Function Grs1GetOrdList(ByVal strDitta As String, ByVal lOlprogr As Integer, _
                                             ByVal strTipork As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String
    Try
      strSQL = "SELECT ol_progr FROM ordlist" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ol_progr = (SELECT ol_olprogr FROM ordlist" & _
                          " WHERE codditt = " & CStrSQL(strDitta) & _
                          " AND ol_tipork = 'Y'" & _
                          " AND ol_progr = " & lOlprogr & ")"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORDLIST")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function Grs1ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                                 ByVal lIITtscho As Integer) As Boolean
    Try
      Return Grs1ComponiStringa(strDitta, dtrTmp, ds, lIITtscho, False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function Grs1ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                                 ByVal lIITtscho As Integer, ByVal bAccorpa As Boolean) As Boolean
    Dim strSQL As String
    Dim i As Integer
    Dim strSuf As String
    Dim strPref As String
    Dim strTaglia As String
    Try

      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lIITtscho, bAccorpa})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If

      '---------------------------------
      'lancio la query
      If bAccorpa Then
        strSQL = "SELECT TTSCHO.*, tb_magass " & _
                " FROM TTSCHO INNER JOIN tabmaga ON ttscho.codditt = tabmaga.codditt AND ttscho.mo_magaz = tabmaga.tb_codmaga" & _
                " WHERE ttscho.codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITtscho & _
                " AND mo_codart = " & CStrSQL(dtrTmp!mo_codart) & _
                " AND (mo_magaz = " & NTSCInt(dtrTmp!mo_magaz) & " OR tb_magass = " & NTSCInt(dtrTmp!mo_magaz) & ")" & _
                " AND mo_fase = " & NTSCInt(dtrTmp!mo_fase) & _
                " ORDER BY mo_codart, mo_fase, CASE WHEN tb_magass = 0 THEN mo_magaz ELSE tb_magass END, mo_esistenza DESC, mo_datcons"

      Else
        strSQL = "SELECT TTSCHO.* FROM TTSCHO" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIITtscho & _
          " AND mo_codart = " & CStrSQL(dtrTmp!mo_codart) & _
          " AND mo_magaz = " & NTSCInt(dtrTmp!mo_magaz) & _
          " AND mo_fase = " & NTSCInt(dtrTmp!mo_fase) & _
          " ORDER BY mo_codart, mo_fase, mo_magaz, mo_esistenza DESC, mo_datcons"
      End If

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORD")

      ds.Tables("MOVORD").Columns.Add(New DataColumn("xx_disp", GetType(Decimal)))
      ds.Tables("MOVORD").Columns.Add(New DataColumn("xx_origine", GetType(String)))
      ds.Tables("MOVORD").Columns.Add(New DataColumn("xx_tipork", GetType(String)))
      ds.Tables("MOVORD").Columns.Add(New DataColumn("xx_numord", GetType(String)))
      ds.Tables("MOVORD").Columns.Add(New DataColumn("xx_consolidato", GetType(String)))
      ds.Tables("MOVORD").Columns.Add(New DataColumn("xx_proposto", GetType(String)))

      strPref = "t"
      For i = 1 To 24
        strSuf = "o"
        strTaglia = strPref & i & strSuf
        ds.Tables("MOVORD").Columns.Add(New DataColumn(strTaglia, GetType(Decimal)))

        strSuf = "p"
        strTaglia = strPref & i & strSuf
        ds.Tables("MOVORD").Columns.Add(New DataColumn(strTaglia, GetType(Decimal)))

        strSuf = "d"
        strTaglia = strPref & i & strSuf
        ds.Tables("MOVORD").Columns.Add(New DataColumn(strTaglia, GetType(Decimal)))
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function Grs1Apri(ByVal strDitta As String, ByRef ds As DataSet, _
                                       ByVal lIITtscho As Integer) As Boolean
    Try
      Return Grs1Apri(strDitta, ds, lIITtscho, False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function Grs1Apri(ByVal strDitta As String, ByRef ds As DataSet, _
                                       ByVal lIITtscho As Integer, ByVal bAccorpa As Boolean) As Boolean
    Dim strSQL As String = ""
    Try

      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, ds, lIITtscho, bAccorpa})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        Return CBool(oOut)
      End If

      If bAccorpa Then
        strSQL = " SELECT DISTINCT mo_codart, mo_fase, CASE WHEN tb_magass = 0 THEN mo_magaz ELSE tb_magass END As mo_magaz" & _
                 " FROM TTSCHO INNER JOIN tabmaga ON ttscho.codditt = tabmaga.codditt AND ttscho.mo_magaz = tabmaga.tb_codmaga" & _
                 " LEFT JOIN anagra ON TTSCHO.codditt = anagra.codditt AND TTSCHO.mo_conto = anagra.an_conto" & _
                 " LEFT JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                 " WHERE TTSCHO.codditt = " & CStrSQL(strDitta) & _
                 " AND TTSCHO.instid = " & lIITtscho & _
                 " ORDER BY mo_codart, mo_fase, CASE WHEN tb_magass = 0 THEN mo_magaz ELSE tb_magass END"

      Else
        strSQL = " SELECT DISTINCT mo_codart, mo_fase, mo_magaz" & _
                 " FROM (TTSCHO LEFT JOIN anagra ON TTSCHO.codditt = anagra.codditt AND TTSCHO.mo_conto = anagra.an_conto)" & _
                 " LEFT JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                 " WHERE TTSCHO.codditt = " & CStrSQL(strDitta) & _
                 " AND TTSCHO.instid = " & lIITtscho & _
                 " ORDER BY mo_codart, mo_fase, mo_magaz"
      End If

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORD")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function RipartisciTaglieQuantitaRagg(ByVal strDitta As String, ByVal bckEvasi As Boolean, _
                                                           ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                           ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                           ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                           ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                           ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                           ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                           ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                           ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                           ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                           ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                           ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                           ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                           ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                           ByVal bModTCO As Boolean, ByRef strSQLRipartisciTaglieRagg As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return RipartisciTaglieQuantitaRagg(strDitta, bckEvasi, stredDamagaz, stredAmagaz, stredDaagente, stredAagente, _
                                          stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, _
                                          stredDaconto, stredAconto, stredDacodart, stredAcodart, _
                                          stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                                          stredGruppo, stredSottogr, stredSerie, strcbRilasciato, _
                                          bckTipork, strcbTipork, stredCodcfam, bckSelAnnoStag, _
                                          stredAnnotco, stredCodstag, bModTCO, strSQLRipartisciTaglieRagg, _
                                          "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RipartisciTaglieQuantitaRagg(ByVal strDitta As String, ByVal bckEvasi As Boolean, _
                                                           ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                           ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                           ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                           ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                           ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                           ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                           ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                           ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                           ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                           ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                           ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                           ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                           ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                           ByVal bModTCO As Boolean, ByRef strSQLRipartisciTaglieRagg As String, _
                                                           ByVal strClassificazioneLivello1 As String, _
                                                           ByVal strClassificazioneLivello2 As String, _
                                                           ByVal strClassificazioneLivello3 As String, _
                                                           ByVal strClassificazioneLivello4 As String, _
                                                           ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bckEvasi, stredDamagaz, stredAmagaz, _
                                             stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                             stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                             stredDacodart, stredAcodart, stredCommecaini, stredCommecafin, _
                                             stredFaseini, stredFasefin, stredGruppo, stredSottogr, stredSerie, _
                                             strcbRilasciato, bckTipork, strcbTipork, stredCodcfam, bckSelAnnoStag, _
                                             stredAnnotco, stredCodstag, bModTCO, strSQLRipartisciTaglieRagg, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strSQLRipartisciTaglieRagg = CType(oIn(29), String)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return RipartisciTaglieQuantitaRagg(strDitta, bckEvasi, stredDamagaz, stredAmagaz, stredDaagente, stredAagente, _
                                          stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, _
                                          stredDaconto, stredAconto, stredDacodart, stredAcodart, _
                                          stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                                          stredGruppo, stredSottogr, stredSerie, strcbRilasciato, _
                                          bckTipork, strcbTipork, stredCodcfam, bckSelAnnoStag, _
                                          stredAnnotco, stredCodstag, bModTCO, strSQLRipartisciTaglieRagg, _
                                          strClassificazioneLivello1, strClassificazioneLivello2, _
                                          strClassificazioneLivello3, strClassificazioneLivello4, _
                                          strClassificazioneLivello5, "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RipartisciTaglieQuantitaRagg(ByVal strDitta As String, ByVal bckEvasi As Boolean, _
                                                           ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                           ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                           ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                           ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                           ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                           ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                           ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                           ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                           ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                           ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                           ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                           ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                           ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                           ByVal bModTCO As Boolean, ByRef strSQLRipartisciTaglieRagg As String, _
                                                           ByVal strClassificazioneLivello1 As String, _
                                                           ByVal strClassificazioneLivello2 As String, _
                                                           ByVal strClassificazioneLivello3 As String, _
                                                           ByVal strClassificazioneLivello4 As String, _
                                                           ByVal strClassificazioneLivello5 As String, _
                                                           ByVal strConto As String, ByVal nCodlsel As Integer, _
                                                           ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bckEvasi, stredDamagaz, stredAmagaz, stredDaagente, stredAagente, _
                                             stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, _
                                             stredDaconto, stredAconto, stredDacodart, stredAcodart, _
                                             stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                                             stredGruppo, stredSottogr, stredSerie, strcbRilasciato, _
                                             bckTipork, strcbTipork, stredCodcfam, bckSelAnnoStag, _
                                             stredAnnotco, stredCodstag, bModTCO, strSQLRipartisciTaglieRagg, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, strConto, nCodlsel, strCodart, nCodlsar})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strSQLRipartisciTaglieRagg = CType(oIn(29), String)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla se ci sono righe 'Y' per quel conto/articolo
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT mo_quant" & _
        " FROM testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_serie = movord.mo_serie AND testord.td_anno = movord.mo_anno AND testord.td_numord = movord.mo_numord" & _
        " INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_riga = keyord.ko_riga AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_serie = keyord.ko_serie AND movord.mo_anno = keyord.ko_anno AND movord.mo_numord = keyord.ko_numord" & _
        " INNER JOIN artico ON artico.codditt = movord.codditt AND artico.ar_codart = movord.mo_codart" & _
        " WHERE testord.codditt = " & CStrSQL(strDitta) & _
        " AND td_tipork <> 'Y'" & _
        IIf(bckEvasi = True, " AND movord.mo_flevas = 'C'", "").ToString & _
        IIf(NTSCInt(stredGruppo) > 0, " AND artico.ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
        IIf(NTSCInt(stredSottogr) > 0, " AND artico.ar_sotgru = " & NTSCInt(stredSottogr), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
        IIf(stredSerie.Trim <> "", " AND keyord.ko_serie = " & CStrSQL(stredSerie), "").ToString & _
        IIf(bckTipork = True, " AND testord.td_tipork = " & CStrSQL(strcbTipork), "").ToString
      If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
        strSQL += " AND keyord.ko_magaz BETWEEN " & NTSCInt(stredDamagaz) & " AND " & NTSCInt(stredAmagaz)
      End If
      If (NTSCInt(stredDaagente) <> 0) Or (NTSCInt(stredAagente) <> 9999) Then
        strSQL += " AND testord.td_codagen BETWEEN " & NTSCInt(stredDaagente) & " AND " & NTSCInt(stredAagente)
      End If
      If (NTSCDate(stredDadatord) <> NTSCDate("01/01/1900")) Or (NTSCDate(stredAdatord) <> NTSCDate("31/12/2099")) Then
        strSQL += " AND testord.td_datord BETWEEN " & CDataSQL(stredDadatord) & " AND " & CDataSQL(stredAdatord)
      End If
      If (NTSCDate(stredDadatcons) <> NTSCDate("01/01/1900")) Or (NTSCDate(stredAdatcons) <> NTSCDate("31/12/2099")) Then
        strSQL += " AND keyord.ko_datcons BETWEEN " & CDataSQL(stredDadatcons) & " AND " & CDataSQL(stredAdatcons)
      End If
      If (NTSCInt(stredCommecaini) <> 0) Or (NTSCInt(stredCommecafin) <> 999999999) Then
        strSQL += " AND movord.mo_commeca BETWEEN " & NTSCInt(stredCommecaini) & " AND " & NTSCInt(stredCommecafin)
      End If
      Select Case NTSCStr(strcbRilasciato)
        Case "N" : strSQL += " AND movord.mo_rilasciato = 'N'"
        Case "S" : strSQL += " AND movord.mo_rilasciato = 'S'"
      End Select
      Select Case strConto
        Case "A"
          If (NTSCInt(stredDaconto) <> 0) Or (NTSCInt(stredAconto) <> 999999999) Then
            strSQL += " AND keyord.ko_conto BETWEEN " & NTSCInt(stredDaconto) & " AND " & NTSCInt(stredAconto)
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND ko_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      Select Case strCodart
        Case "A"
          If (stredDacodart <> "".PadLeft(CLN__STD.CodartMaxLen, " "c)) Or (stredAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strSQL += " AND keyord.ko_codart BETWEEN " & CStrSQL(stredDacodart) & " AND " & CStrSQL(stredAcodart)
          End If
          If (NTSCInt(stredFaseini) <> 0) Or (NTSCInt(stredFasefin) <> 9999) Then
            strSQL += " AND movord.mo_fase BETWEEN " & NTSCInt(stredFaseini) & " AND " & NTSCInt(stredFasefin)
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL += _
              " AND CAST(ko_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(mo_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      strSQLRipartisciTaglieRagg = strSQL
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function TaglieQuantitaRagg(ByVal strDitta As String, ByVal bckEvasi As Boolean, _
                                                 ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                 ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                 ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                 ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                 ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                 ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                 ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                 ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                 ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                 ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                 ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                 ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                 ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                 ByVal bModTCO As Boolean, ByRef strSQLTaglieRagg As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return TaglieQuantitaRagg(strDitta, bckEvasi, stredDamagaz, stredAmagaz, stredDaagente, stredAagente, _
                                stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                stredDacodart, stredAcodart, stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                                stredGruppo, stredSottogr, stredSerie, strcbRilasciato, bckTipork, strcbTipork, _
                                stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, bModTCO, strSQLTaglieRagg, _
                                "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function TaglieQuantitaRagg(ByVal strDitta As String, ByVal bckEvasi As Boolean, _
                                                 ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                 ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                 ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                 ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                 ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                 ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                 ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                 ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                 ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                 ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                 ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                 ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                 ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                 ByVal bModTCO As Boolean, ByRef strSQLTaglieRagg As String, _
                                                 ByVal strClassificazioneLivello1 As String, _
                                                 ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, _
                                                 ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bckEvasi, stredDamagaz, stredAmagaz, _
                                stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                stredDacodart, stredAcodart, stredCommecaini, stredCommecafin, _
                                stredFaseini, stredFasefin, stredGruppo, stredSottogr, _
                                stredSerie, strcbRilasciato, bckTipork, strcbTipork, _
                                stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                bModTCO, strSQLTaglieRagg, strClassificazioneLivello1, strClassificazioneLivello2, _
                                strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strSQLTaglieRagg = CType(oIn(29), String)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return TaglieQuantitaRagg(strDitta, bckEvasi, stredDamagaz, stredAmagaz, stredDaagente, stredAagente, _
                                stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                stredDacodart, stredAcodart, stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                                stredGruppo, stredSottogr, stredSerie, strcbRilasciato, bckTipork, strcbTipork, _
                                stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, bModTCO, strSQLTaglieRagg, _
                                strClassificazioneLivello1, strClassificazioneLivello2, _
                                strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function TaglieQuantitaRagg(ByVal strDitta As String, ByVal bckEvasi As Boolean, _
                                                 ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                 ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                 ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                 ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                 ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                 ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                 ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                 ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                 ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                 ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                 ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                 ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                 ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                 ByVal bModTCO As Boolean, ByRef strSQLTaglieRagg As String, _
                                                 ByVal strClassificazioneLivello1 As String, _
                                                 ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, _
                                                 ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String, _
                                                 ByVal strConto As String, ByVal nCodlsel As Integer, _
                                                 ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bckEvasi, stredDamagaz, stredAmagaz, _
                                             stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                             stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                             stredDacodart, stredAcodart, stredCommecaini, stredCommecafin, _
                                             stredFaseini, stredFasefin, stredGruppo, stredSottogr, _
                                             stredSerie, strcbRilasciato, bckTipork, strcbTipork, _
                                             stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                             bModTCO, strSQLTaglieRagg, strClassificazioneLivello1, _
                                             strClassificazioneLivello2, strClassificazioneLivello3, _
                                             strClassificazioneLivello4, strClassificazioneLivello5, _
                                             strConto, nCodlsel, strCodart, nCodlsar})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strSQLTaglieRagg = CType(oIn(29), String)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla se ci sono righe 'Y' per quel conto/articolo
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT movord_1.mo_codart As Materiale, movord_1.mo_descr As desmateriale," & _
        " movord_1.mo_desint As desmaterialeint, Sum(movord_1.mo_quant) As Quantita, artico.ar_gesvar As GESVAR" & _
        " FROM (testord INNER JOIN (movord LEFT JOIN movord AS movord_1 ON (movord.codditt = movord_1.codditt) AND (movord.mo_tipork = movord_1.mo_tiporkor) AND (movord.mo_anno = movord_1.mo_annoor) AND (movord.mo_serie = movord_1.mo_serieor) AND (movord.mo_numord = movord_1.mo_numordor) AND (movord.mo_riga = movord_1.mo_rigaor)) ON (testord.codditt = movord.codditt) AND (testord.td_tipork = movord.mo_tipork) AND (testord.td_serie = movord.mo_serie) AND (testord.td_anno = movord.mo_anno) AND (testord.td_numord = movord.mo_numord)) LEFT JOIN artico ON movord_1.codditt = artico.codditt AND movord_1.mo_codart = artico.ar_codart" & _
        " WHERE testord.codditt = " & CStrSQL(strDitta) & _
        " AND td_tipork = 'H'" & _
        " AND movord_1.mo_tipork = 'Y'" & _
        IIf(bckEvasi = True, " AND movord.mo_flevas = 'C'", "").ToString & _
        IIf(NTSCInt(stredGruppo) > 0, " AND artico.ar_gruppo = " & NTSCInt(stredGruppo), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
        IIf(stredSerie.Trim <> "", " AND movord.mo_serie = " & CStrSQL(stredSerie), "").ToString & _
        IIf(stredCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(stredCodcfam), "").ToString
      Select Case NTSCStr(strcbRilasciato)
        Case "N" : strSQL += " AND movord.mo_rilasciato = 'N'"
        Case "S" : strSQL += " AND movord.mo_rilasciato = 'S'"
      End Select
      If (NTSCInt(stredDamagaz) <> 0) Or (NTSCInt(stredAmagaz) <> 9999) Then
        strSQL += " AND movord.mo_magaz BETWEEN " & NTSCInt(stredDamagaz) & " AND " & NTSCInt(stredAmagaz)
      End If
      If (NTSCDate(stredDadatord) <> NTSCDate("01/01/1900")) Or (NTSCDate(stredAdatord) <> NTSCDate("31/12/2099")) Then
        strSQL += " AND testord.td_datord BETWEEN " & CDataSQL(stredDadatord) & " AND " & CDataSQL(stredAdatord)
      End If
      If (NTSCDate(stredDadatcons) <> NTSCDate("01/01/1900")) Or (NTSCDate(stredAdatcons) <> NTSCDate("31/12/2099")) Then
        strSQL += " AND movord.mo_datcons BETWEEN " & CDataSQL(stredDadatcons) & " AND " & CDataSQL(stredAdatcons)
      End If
      If (NTSCInt(stredCommecaini) <> 0) Or (NTSCInt(stredCommecafin) <> 999999999) Then
        strSQL += " AND movord.mo_commeca BETWEEN " & NTSCInt(stredCommecaini) & " AND " & NTSCInt(stredCommecafin)
      End If
      Select Case strConto
        Case "A"
          If (NTSCInt(stredDaconto) <> 0) Or (NTSCInt(stredAconto) <> 999999999) Then
            strSQL += " AND testord.td_conto BETWEEN " & NTSCInt(stredDaconto) & " AND " & NTSCInt(stredAconto)
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND td_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      If (bModTCO = True) And (bckSelAnnoStag = True) Then
        If NTSCInt(stredAnnotco) > 0 Then strSQL += " AND artico.ar_anno = " & NTSCInt(stredAnnotco)
        If NTSCInt(stredCodstag) > 0 Then strSQL += " AND artico.ar_codstag = " & NTSCInt(stredCodstag)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQLTaglieRagg = strSQL
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function Grs2ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                                 ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                                 ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                                 ByVal strDaDatord As String, ByVal strADatord As String, _
                                                 ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                                 ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                                 ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                                 ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                                 ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                                 ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                                 ByVal bSchoEvasi As Boolean, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal strQuery As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return Grs2ComponiStringa(strDitta, dtrTmp, ds, lDaConto, lAConto, _
                                lDaCommessa, lACommessa, strDaDatord, strADatord, _
                                strDaDatcons, strADatcons, strSchoTipork, strSchoSerie, _
                                nGruppo, nSottogr, nSchoDaagente, nSchoAagente, _
                                strSchoRilasciato, strSchoOrdin, nSchoFaseini, nSchoFasefin, _
                                bSchoEvasi, bModuloCRM, bIsCRMUser, strAccvis, _
                                lCodorgaOperat, strRegvis, bAmm, strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Grs2ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                                 ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                                 ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                                 ByVal strDaDatord As String, ByVal strADatord As String, _
                                                 ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                                 ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                                 ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                                 ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                                 ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                                 ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                                 ByVal bSchoEvasi As Boolean, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal strQuery As String, _
                                                 ByVal strClassificazioneLivello1 As String, _
                                                 ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, _
                                                 ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                                             strDaDatord, strADatord, strDaDatcons, strADatcons, strSchoTipork, _
                                             strSchoSerie, nGruppo, nSottogr, nSchoDaagente, nSchoAagente, _
                                             strSchoRilasciato, strSchoOrdin, nSchoFaseini, nSchoFasefin, _
                                             bSchoEvasi, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                             bAmm, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return Grs2ComponiStringa(strDitta, dtrTmp, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                                strDaDatord, strADatord, strDaDatcons, strADatcons, strSchoTipork, strSchoSerie, _
                                nGruppo, nSottogr, nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, _
                                nSchoFaseini, nSchoFasefin, bSchoEvasi, bModuloCRM, bIsCRMUser, strAccvis, _
                                lCodorgaOperat, strRegvis, bAmm, strQuery, strClassificazioneLivello1, _
                                strClassificazioneLivello2, strClassificazioneLivello3, strClassificazioneLivello4, _
                                strClassificazioneLivello5, "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Grs2ComponiStringa(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                                 ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                                 ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                                 ByVal strDaDatord As String, ByVal strADatord As String, _
                                                 ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                                 ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                                 ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                                 ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                                 ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                                 ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                                 ByVal bSchoEvasi As Boolean, ByVal bModuloCRM As Boolean, _
                                                 ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                                 ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                                 ByVal bAmm As Boolean, ByVal strQuery As String, _
                                                 ByVal strClassificazioneLivello1 As String, _
                                                 ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, _
                                                 ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String, _
                                                 ByVal strConto As String, ByVal nCodlsel As Integer, _
                                                 ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strInnerJoin As String = "testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_serie = movord.mo_serie AND testord.td_anno = movord.mo_anno AND testord.td_numord = movord.mo_numord" & _
      " INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_riga = keyord.ko_riga AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_serie = keyord.ko_serie AND movord.mo_anno = keyord.ko_anno AND movord.mo_numord = keyord.ko_numord" & _
      " INNER JOIN artico ON artico.codditt = movord.codditt AND artico.ar_codart = movord.mo_codart" & _
      " INNER JOIN anagra ON keyord.codditt = anagra.codditt AND keyord.ko_conto = anagra.an_conto" & _
      " LEFT JOIN tabmaga ON movord.codditt = tabmaga.codditt AND movord.mo_magaz = tabmaga.tb_codmaga" & _
      " LEFT JOIN artfasi ON movord.codditt = artfasi.codditt AND movord.mo_fase = artfasi.af_fase AND movord.mo_codart = artfasi.af_codart"
    Dim strScontV As String = "CASE WHEN td_scorpo = 'S' AND mo_scontv <> 0 " & _
                              " THEN (100 * mo_scontv) / (100 + (SELECT tb_aliq FROM tabciva WHERE tb_codciva = mo_codiva)) " & _
                              " ELSE mo_scontv END"
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dtrTmp, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                                             strDaDatord, strADatord, strDaDatcons, strADatcons, strSchoTipork, _
                                             strSchoSerie, nGruppo, nSottogr, nSchoDaagente, nSchoAagente, _
                                             strSchoRilasciato, strSchoOrdin, nSchoFaseini, nSchoFasefin, _
                                             bSchoEvasi, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                             bAmm, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, strConto, nCodlsel, strCodart, nCodlsar})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT mo_fase, af_descr, ko_datcons, td_datord, ko_tipork, ko_anno, ko_serie, ko_numord, ko_riga," & _
        " ko_magaz, mo_magaz, tb_desmaga, td_riferim, td_alfpar, td_numpar, td_datpar, mo_quant, mo_quaeva, mo_flevas," & _
        " mo_quapre, mo_flevapre, CASE WHEN (mo_flevas = 'S') THEN 0 ELSE (mo_quant - mo_quaeva) END As Quaeva," & _
        " mo_datconsor, mo_prezzo, mo_valore, an_descr1," & _
        " ((mo_prezzo*(100-mo_scont1)/100*(100-mo_scont2)/100*(100-mo_scont3)/100*(100-mo_scont4)/100*(100-mo_scont5)/100*(100-mo_scont6)/100*(100-mo_scontp)/100)-" & strScontV & ") As PRZNET," & _
        " mo_scont1, mo_scont2, mo_scont3, mo_scont4, mo_scont5, mo_scont6, mo_prelist, mo_prezvalc, mo_colli," & _
        " mo_coleva, mo_colpre, mo_misura1, mo_misura2, mo_misura3, mo_controp, mo_codcena, mo_codcfam, mo_provv," & _
        " mo_vprovv, mo_provv2, mo_vprovv2, mo_ubicaz FROM " & strInnerJoin & _
        " WHERE keyord.codditt = " & CStrSQL(strDitta) & _
        " AND ko_commecap = " & NTSCStr(dtrTmp!ko_commecap) & _
        " AND ko_codart = " & CStrSQL(dtrTmp!ko_codart) & _
        IIf(bSchoEvasi = True, " AND mo_flevas = 'C'", "").ToString & _
        IIf(strSchoTipork <> "", " AND ko_tipork = " & CStrSQL(strSchoTipork), "").ToString & _
        IIf(strSchoRilasciato <> "E", " AND mo_rilasciato = " & CStrSQL(strSchoRilasciato), "").ToString & _
        IIf(nGruppo > 0, " AND ar_gruppo = " & nGruppo, "").ToString & _
        IIf(nSottogr > 0, " AND ar_sotgru = " & nSottogr, "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
        IIf(strSchoSerie <> "", " AND ko_serie = " & CStrSQL(strSchoSerie), "").ToString
      If (NTSCDate(strDaDatord) <> NTSCDate("01/01/1900")) Or (NTSCDate(strADatord) <> NTSCDate("31/12/2099")) Then
        strSQL += " AND td_datord BETWEEN " & CDataSQL(strDaDatord) & " AND " & CDataSQL(strADatord)
      End If
      If (NTSCDate(strDaDatcons) <> NTSCDate("01/01/1900")) Or (NTSCDate(strADatcons) <> NTSCDate("31/12/2099")) Then
        strSQL += " AND ko_datcons BETWEEN " & CDataSQL(strDaDatcons) & " AND " & CDataSQL(strADatcons)
      End If
      If (nSchoDaagente <> 0) Or (nSchoAagente <> 9999) Then
        strSQL += " AND td_codagen BETWEEN " & nSchoDaagente & " AND " & nSchoAagente
      End If
      Select Case strConto
        Case "A"
          If (lDaConto <> 0) Or (lAConto <> 999999999) Then
            strSQL += " AND ko_conto BETWEEN " & lDaConto & " AND " & lAConto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND ko_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      Select Case strCodart
        Case "A"
          If (nSchoFaseini <> 0) Or (nSchoFaseini <> 9999) Then
            strSQL += " AND mo_fase BETWEEN " & nSchoFaseini & " AND " & nSchoFasefin
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL += _
              " AND CAST(ko_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(mo_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
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
          strSQL += " an_tipo = 'C'"
        End If
        If bAmm = True Then strSQL += " OR an_tipo <> 'C'" Else strSQL += " AND an_tipo <> 'F'"
        strSQL += ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery, strSQL)
      '--------------------------------------------------------------------------------------------------------------
      strSQL += " ORDER BY ko_commecap, ko_codart, ko_fase, ko_datcons"
      '--------------------------------------------------------------------------------------------------------------
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORD")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function Grs2Apri(ByVal strDitta As String, ByRef ds As DataSet, _
                                       ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                       ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                       ByVal strDaDatord As String, ByVal strADatord As String, _
                                       ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                       ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                       ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                       ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                       ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                       ByVal nSchoDamagaz As Integer, ByVal nSchoAmagaz As Integer, _
                                       ByVal strSchoDacodart As String, ByVal strSchoAcodart As String, _
                                       ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                       ByVal bSchoEvasi As Boolean, ByVal strGrsoCodcfam As String, _
                                       ByVal bGrsoModTCO As Boolean, ByVal nGrsoAnnotco As Integer, _
                                       ByVal nGrsoCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                       ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                       ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                       ByVal bAmm As Boolean, ByVal strQuery As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return Grs2Apri(strDitta, ds, lDaConto, lAConto, lDaCommessa, lACommessa, strDaDatord, strADatord, _
                      strDaDatcons, strADatcons, strSchoTipork, strSchoSerie, nGruppo, nSottogr, _
                      nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, nSchoDamagaz, nSchoAmagaz, _
                      strSchoDacodart, strSchoAcodart, nSchoFaseini, nSchoFasefin, bSchoEvasi, strGrsoCodcfam, _
                      bGrsoModTCO, nGrsoAnnotco, nGrsoCodstag, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                      strRegvis, bAmm, strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Grs2Apri(ByVal strDitta As String, ByRef ds As DataSet, _
                                       ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                       ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                       ByVal strDaDatord As String, ByVal strADatord As String, _
                                       ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                       ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                       ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                       ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                       ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                       ByVal nSchoDamagaz As Integer, ByVal nSchoAmagaz As Integer, _
                                       ByVal strSchoDacodart As String, ByVal strSchoAcodart As String, _
                                       ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                       ByVal bSchoEvasi As Boolean, ByVal strGrsoCodcfam As String, _
                                       ByVal bGrsoModTCO As Boolean, ByVal nGrsoAnnotco As Integer, _
                                       ByVal nGrsoCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                       ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                       ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                       ByVal bAmm As Boolean, ByVal strQuery As String, _
                                       ByVal strClassificazioneLivello1 As String, _
                                       ByVal strClassificazioneLivello2 As String, _
                                       ByVal strClassificazioneLivello3 As String, _
                                       ByVal strClassificazioneLivello4 As String, _
                                       ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                      strDaDatord, strADatord, strDaDatcons, strADatcons, _
                      strSchoTipork, strSchoSerie, nGruppo, nSottogr, _
                      nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, _
                      nSchoDamagaz, nSchoAmagaz, strSchoDacodart, strSchoAcodart, _
                      nSchoFaseini, nSchoFasefin, bSchoEvasi, strGrsoCodcfam, _
                      bGrsoModTCO, nGrsoAnnotco, nGrsoCodstag, bModuloCRM, _
                      bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                      bAmm, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return Grs2Apri(strDitta, ds, lDaConto, lAConto, lDaCommessa, lACommessa, strDaDatord, strADatord, _
                      strDaDatcons, strADatcons, strSchoTipork, strSchoSerie, nGruppo, nSottogr, _
                      nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, nSchoDamagaz, nSchoAmagaz, _
                      strSchoDacodart, strSchoAcodart, nSchoFaseini, nSchoFasefin, bSchoEvasi, strGrsoCodcfam, _
                      bGrsoModTCO, nGrsoAnnotco, nGrsoCodstag, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, _
                      strRegvis, bAmm, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function Grs2Apri(ByVal strDitta As String, ByRef ds As DataSet, _
                                       ByVal lDaConto As Integer, ByVal lAConto As Integer, _
                                       ByVal lDaCommessa As Integer, ByVal lACommessa As Integer, _
                                       ByVal strDaDatord As String, ByVal strADatord As String, _
                                       ByVal strDaDatcons As String, ByVal strADatcons As String, _
                                       ByVal strSchoTipork As String, ByVal strSchoSerie As String, _
                                       ByVal nGruppo As Integer, ByVal nSottogr As Integer, _
                                       ByVal nSchoDaagente As Integer, ByVal nSchoAagente As Integer, _
                                       ByVal strSchoRilasciato As String, ByVal strSchoOrdin As String, _
                                       ByVal nSchoDamagaz As Integer, ByVal nSchoAmagaz As Integer, _
                                       ByVal strSchoDacodart As String, ByVal strSchoAcodart As String, _
                                       ByVal nSchoFaseini As Integer, ByVal nSchoFasefin As Integer, _
                                       ByVal bSchoEvasi As Boolean, ByVal strGrsoCodcfam As String, _
                                       ByVal bGrsoModTCO As Boolean, ByVal nGrsoAnnotco As Integer, _
                                       ByVal nGrsoCodstag As Integer, ByVal bModuloCRM As Boolean, _
                                       ByVal bIsCRMUser As Boolean, ByVal strAccvis As String, _
                                       ByVal lCodorgaOperat As Integer, ByVal strRegvis As String, _
                                       ByVal bAmm As Boolean, ByVal strQuery As String, _
                                       ByVal strClassificazioneLivello1 As String, _
                                       ByVal strClassificazioneLivello2 As String, _
                                       ByVal strClassificazioneLivello3 As String, _
                                       ByVal strClassificazioneLivello4 As String, _
                                       ByVal strClassificazioneLivello5 As String, _
                                       ByVal strConto As String, ByVal nCodlsel As Integer, _
                                       ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strInnerJoin As String = "testord INNER JOIN movord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork AND testord.td_serie = movord.mo_serie AND testord.td_anno = movord.mo_anno AND testord.td_numord = movord.mo_numord" & _
      " INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_riga = keyord.ko_riga AND movord.mo_tipork = keyord.ko_tipork AND movord.mo_serie = keyord.ko_serie AND movord.mo_anno = keyord.ko_anno AND movord.mo_numord = keyord.ko_numord" & _
      " INNER JOIN artico ON artico.codditt = movord.codditt AND artico.ar_codart = movord.mo_codart" & _
      " INNER JOIN anagra ON keyord.codditt = anagra.codditt AND keyord.ko_conto = anagra.an_conto" & _
      " LEFT JOIN tabmaga ON movord.codditt = tabmaga.codditt AND movord.mo_magaz = tabmaga.tb_codmaga" & _
      " LEFT JOIN artfasi ON movord.codditt = artfasi.codditt AND movord.mo_fase = artfasi.af_fase AND movord.mo_codart = artfasi.af_codart"

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, ds, lDaConto, lAConto, lDaCommessa, lACommessa, _
                      strDaDatord, strADatord, strDaDatcons, strADatcons, _
                      strSchoTipork, strSchoSerie, nGruppo, nSottogr, _
                      nSchoDaagente, nSchoAagente, strSchoRilasciato, strSchoOrdin, _
                      nSchoDamagaz, nSchoAmagaz, strSchoDacodart, strSchoAcodart, _
                      nSchoFaseini, nSchoFasefin, bSchoEvasi, strGrsoCodcfam, _
                      bGrsoModTCO, nGrsoAnnotco, nGrsoCodstag, bModuloCRM, _
                      bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                      bAmm, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(1), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT DISTINCT ko_commecap, ko_codart, mo_ump FROM " & strInnerJoin & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND ar_gescomm = " & CStrSQL(strSchoOrdin) & _
        IIf(nGruppo > 0, " AND ar_gruppo =  " & nGruppo, "").ToString & _
        IIf(nSottogr > 0, " AND ar_sotgru = " & nSottogr, "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString & _
        IIf(strSchoSerie <> "", " AND ko_serie = " & CStrSQL(strSchoSerie), "").ToString & _
        IIf(strSchoTipork <> "", " AND ko_tipork = " & CStrSQL(strSchoTipork), "").ToString & _
        IIf(strSchoRilasciato <> "E", " AND mo_rilasciato = " & CStrSQL(strSchoRilasciato), "").ToString & _
        IIf(bSchoEvasi = True, " AND mo_flevas = 'C'", "").ToString & _
        IIf(strGrsoCodcfam.Trim <> "", " AND artico.ar_famprod = " & CStrSQL(strGrsoCodcfam), "").ToString
      If (nSchoDamagaz <> 0) Or (nSchoAmagaz <> 9999) Then
        strSQL += " AND ko_magaz BETWEEN " & nSchoDamagaz & " AND " & nSchoAmagaz
      End If
      If (NTSCDate(strDaDatord) <> NTSCDate("01/01/1900")) Or (NTSCDate(strADatord) <> NTSCDate("31/12/2099")) Then
        strSQL += " AND td_datord BETWEEN " & strDaDatord & " AND " & strADatord
      End If
      If (lDaCommessa <> 0) Or (lACommessa <> 999999999) Then
        strSQL += " AND mo_commeca BETWEEN " & lDaCommessa & " AND " & lACommessa
      End If
      If (NTSCDate(strDaDatcons) <> NTSCDate("01/01/1900")) Or (NTSCDate(strADatcons) <> NTSCDate("31/12/2099")) Then
        strSQL += " AND ko_datcons BETWEEN " & strDaDatcons & " AND " & strADatcons
      End If
      If (nSchoDaagente <> 0) Or (nSchoAagente <> 9999) Then
        strSQL += " AND td_codagen BETWEEN " & nSchoDaagente & " AND " & nSchoAagente
      End If
      Select Case strConto
        Case "A"
          If (lDaConto <> 0) Or (lAConto <> 999999999) Then
            strSQL += " AND ko_conto BETWEEN " & lDaConto & " AND " & lAConto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL += " AND ko_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      Select Case strCodart
        Case "A"
          If (strSchoDacodart <> "".PadLeft(CLN__STD.CodartMaxLen, " "c)) Or (strSchoAcodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strSQL += " AND ko_codart BETWEEN " & CStrSQL(strSchoDacodart) & " AND " & CStrSQL(strSchoAcodart)
          End If
          If (nSchoFaseini <> 0) Or (nSchoFasefin <> 9999) Then
            strSQL += " AND mo_fase BETWEEN " & nSchoFaseini & " AND " & nSchoFasefin
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL += _
              " AND CAST(ko_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(mo_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      If bGrsoModTCO = True Then
        If nGrsoAnnotco > 0 Then strSQL = strSQL & " AND artico.ar_anno = " & nGrsoAnnotco
        If nGrsoCodstag > 0 Then strSQL = strSQL & " AND artico.ar_codstag = " & nGrsoCodstag
      End If
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
          strSQL += " an_tipo = 'C'"
        End If
        If bAmm = True Then strSQL += " OR an_tipo <> 'C'" Else strSQL += " AND an_tipo <> 'F'"
        strSQL += ")"
      End If
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery, strSQL)
      '--------------------------------------------------------------------------------------------------------------
      strSQL += " GROUP BY ko_commecap, ko_codart, mo_ump" & _
        " ORDER BY ko_commecap, ko_codart, mo_ump"
      '--------------------------------------------------------------------------------------------------------------
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORD")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
