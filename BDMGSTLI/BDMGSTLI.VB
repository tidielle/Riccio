Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGSTLI
  Inherits CLD__BASE

  Public Overridable Function GetDataElabora(ByVal strDitta As String, ByVal bopSelezione0 As Boolean, _
                                             ByVal bopSelezione1 As Boolean, ByVal strQuery As String, _
                                             ByVal stredCodlsar As String, ByRef ds As DataSet) As Boolean
    Try
      GetDataElabora(strDitta, bopSelezione0, bopSelezione1, strQuery, "", stredCodlsar, ds)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataElabora(ByVal strDitta As String, ByVal bopSelezione0 As Boolean, _
                                             ByVal bopSelezione1 As Boolean, ByVal strQuery As String, _
                                             ByVal strOrderBy As String, ByVal stredCodlsar As String, _
                                             ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, bopSelezione0, bopSelezione1, strQuery, strOrderBy, _
                                             stredCodlsar, ds})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(6), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      If bopSelezione0 Then   '--- Selezione da HLAR
        strSQL = "SELECT ar_codart, ar_descr, ar_desint, ar_note, ar_perqta, ar_unmis," & _
          " ar_clascon, ar_gesfasi, ar_ultfase, CASE WHEN (ar_gesfasi = 'S') THEN af_fase ELSE ar_ultfase END As Fase" & _
          " FROM artico LEFT JOIN artfasi ON (artico.codditt = artfasi.codditt) AND (artico.ar_codart = artfasi.af_codart)" & _
          " WHERE artico.codditt = " & CStrSQL(strDitta) & _
          " AND artico.ar_tipoopz IN (' ', 'O') "
        TraduciWhere(strQuery, strSQL)
        strSQL &= strOrderBy
        ' strSQL = strSQL & " ORDER BY artico.ar_codart" L'ordinamento gli viene passato tramite strQuery
      End If
      If bopSelezione1 Then   '--- Selezione da LISTSAR
        strSQL = "SELECT ar_codart, ar_descr, ar_desint, ar_note, ar_perqta, ar_unmis," & _
          " ar_clascon, ar_gesfasi, ar_ultfase, lsa_fase As Fase" & _
          " FROM artico INNER JOIN listsar ON (artico.codditt = listsar.codditt) AND (artico.ar_codart = listsar.lsa_codart)" & _
          " WHERE lsa_codlsar = " & NTSCInt(stredCodlsar) & _
          " AND artico.codditt = " & CStrSQL(strDitta) & _
          " AND artico.ar_tipoopz IN (' ', 'O')" & _
          " ORDER BY lsa_riga"
      End If

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTSTLI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetQueryArtElab(ByVal strDitta As String, ByVal bopSelezione0 As Boolean, ByVal strQuery As String) As String
    Dim strSQL As String = ""
    Try
      If bopSelezione0 Then TraduciWhere(strQuery, strSQL)

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function


  Public Overridable Function GetQueryStampaWord(ByVal strDitta As String, ByVal lIITtstli As Integer, _
                                                  ByVal lIITTListsar As Integer, ByVal bStampaWordRaggruppata As Boolean, _
                                                  ByVal strQuery As String) As String
    Try
      Return GetQueryStampaWord(strDitta, lIITtstli, lIITTListsar, bStampaWordRaggruppata, strQuery, "")
    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '-------------------------------------------------	
      Return ""
    End Try
  End Function

  Public Overridable Function GetQueryStampaWord(ByVal strDitta As String, ByVal lIITtstli As Integer, _
                                                 ByVal lIITTListsar As Integer, ByVal bStampaWordRaggruppata As Boolean, _
                                                 ByVal strQuery As String, ByVal strOrderBy As String) As String
    'prepara la query, ma non la esegue: la passerà a BN__WORD che estrarrà i dati e li stamperà
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim strJoin As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITtstli, lIITTListsar, bStampaWordRaggruppata, strQuery, _
                                             strOrderBy})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '----------------

      strWhere = ComponiWhereStampaWord(strDitta, strQuery)

      strSQL = "SELECT artico.*, TTSTLI.*, TTLISTSAR.lsa_descr, TTLISTSAR.lsa_desint," & _
        " TTLISTSAR.lsa_note, anagra.an_descr1 AS an_descr1," & _
        " anagra_1.an_descr1 AS an_descr1_1, tabciva.tb_desciva, tabgmer.tb_desgmer," & _
        " tabsgme.tb_dessgme, tabcpar.tb_descpar, tabcsar.tb_descsar, tabcfam.tb_descfam," & _
        " tabpdon.tb_despdon, tabappr.tb_desappr, tabmarc.tb_desmarc," & _
        " artval.ax_descr, ax_desint, ax_note, dd_nomdest as xx_coddest"
      strSQL = strSQL & " FROM (((((((((((((TTSTLI INNER JOIN TTLISTSAR ON TTSTLI.codditt = TTLISTSAR.codditt AND TTSTLI.sta_codart = TTLISTSAR.lsa_codart AND TTSTLI.sta_fase = TTLISTSAR.lsa_fase)" & _
        " INNER JOIN artico ON artico.codditt = TTSTLI.codditt AND artico.ar_codart = TTSTLI.sta_codart)" & _
        " LEFT JOIN anagra ON artico.codditt = anagra.codditt AND artico.ar_forn = anagra.an_conto)" & _
        " LEFT JOIN anagra AS anagra_1 ON artico.codditt = anagra_1.codditt AND artico.ar_forn2 = anagra_1.an_conto)" & _
        " LEFT JOIN tabciva ON artico.ar_codiva = tabciva.tb_codciva)" & _
        " LEFT JOIN tabgmer ON artico.codditt = tabgmer.codditt AND artico.ar_gruppo = tabgmer.tb_codgmer)" & _
        " LEFT JOIN tabsgme ON artico.codditt = tabsgme.codditt AND artico.ar_sotgru = tabsgme.tb_codsgme)" & _
        " LEFT JOIN tabcpar ON artico.codditt = tabcpar.codditt AND artico.ar_claprov = tabcpar.tb_codcpar)" & _
        " LEFT JOIN tabcsar ON artico.codditt = tabcsar.codditt AND artico.ar_clascon = tabcsar.tb_codcsar)" & _
        " LEFT JOIN tabcfam ON artico.codditt = tabcfam.codditt AND artico.ar_famprod = tabcfam.tb_codcfam)" & _
        " LEFT JOIN tabpdon ON artico.codditt = tabpdon.codditt AND artico.ar_codpdon = tabpdon.tb_codpdon)" & _
        " LEFT JOIN tabappr ON artico.codditt = tabappr.codditt AND artico.ar_codappr = tabappr.tb_codappr)" & _
        " LEFT JOIN tabmarc ON artico.codditt = tabmarc.codditt AND artico.ar_codmarc = tabmarc.tb_codmarc)" & _
        " LEFT JOIN artval ON TTSTLI.codditt = artval.codditt AND TTSTLI.sta_codart = artval.ax_codart AND TTSTLI.sta_codling = artval.ax_codvalu" & _
        " LEFT JOIN destdiv ON TTSTLI.codditt = destdiv.codditt AND TTSTLI.sta_conto = destdiv.dd_conto AND TTSTLI.sta_coddest = destdiv.dd_coddest"
      strSQL = strSQL & " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND TTSTLI.instid = " & lIITtstli & _
        " AND TTLISTSAR.instid = " & lIITTListsar

      '-----------------------------------------------------------------------------------------
      'aggiungo filtri where
      strSQL = strSQL & strWhere
      '-----------------------------------------------------------------------------------------

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
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

  Public Overridable Function InsertTemp(ByVal strDitta As String, ByVal lIITtstli As Integer, _
                                       ByVal lProg As Integer, ByVal strAr_codart As String, _
                                       ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, _
                                       ByVal dSc3 As Decimal, ByVal dSc4 As Decimal, _
                                       ByVal dSc5 As Decimal, ByVal dSc6 As Decimal, _
                                       ByVal dDaQuantsc As Decimal, ByVal dAQuantsc As Decimal, _
                                       ByVal strUnmissc As String, _
                                       ByVal dSc1_2 As Decimal, ByVal dSc2_2 As Decimal, _
                                       ByVal dSc3_2 As Decimal, ByVal dSc4_2 As Decimal, _
                                       ByVal dSc5_2 As Decimal, ByVal dSc6_2 As Decimal, _
                                       ByVal dDaQuantsc_2 As Decimal, ByVal dAQuantsc_2 As Decimal, _
                                       ByVal strUnmissc_2 As String, _
                                       ByVal dSc1_3 As Decimal, ByVal dSc2_3 As Decimal, _
                                       ByVal dSc3_3 As Decimal, ByVal dSc4_3 As Decimal, _
                                       ByVal dSc5_3 As Decimal, ByVal dSc6_3 As Decimal, _
                                       ByVal dDaQuantsc_3 As Decimal, ByVal dAQuantsc_3 As Decimal, _
                                       ByVal strUnmissc_3 As String, _
                                       ByVal dSc1_4 As Decimal, ByVal dSc2_4 As Decimal, _
                                       ByVal dSc3_4 As Decimal, ByVal dSc4_4 As Decimal, _
                                       ByVal dSc5_4 As Decimal, ByVal dSc6_4 As Decimal, _
                                       ByVal dDaQuantsc_4 As Decimal, ByVal dAQuantsc_4 As Decimal, _
                                       ByVal strUnmissc_4 As String, _
                                       ByVal dPrezzo1 As Decimal, _
                                       ByVal stredCodvalu1 As String, ByVal dDaQuant1 As Decimal, _
                                       ByVal dAQuant1 As Decimal, ByVal dPerqta1 As Decimal, _
                                       ByVal strUnmis1 As String, ByVal dPrezzo2 As Decimal, _
                                       ByVal stredCodvalu2 As String, ByVal dDaQuant2 As Decimal, _
                                       ByVal dAQuant2 As Decimal, ByVal dPerqta2 As Decimal, _
                                       ByVal strUnmis2 As String, ByVal dPrezzo3 As Decimal, _
                                       ByVal stredCodvalu3 As String, ByVal dDaQuant3 As Decimal, _
                                       ByVal dAQuant3 As Decimal, ByVal dPerqta3 As Decimal, _
                                       ByVal strUnmis3 As String, ByVal dPrezzo4 As Decimal, _
                                       ByVal stredCodvalu4 As String, ByVal dDaQuant4 As Decimal, _
                                       ByVal dAQuant4 As Decimal, ByVal dPerqta4 As Decimal, _
                                       ByVal strUnmis4 As String, ByVal dPrezzo5 As Decimal, _
                                       ByVal stredCodvalu5 As String, ByVal dDaQuant5 As Decimal, _
                                       ByVal dAQuant5 As Decimal, ByVal dPerqta5 As Decimal, _
                                       ByVal strUnmis5 As String, ByVal dFase As Decimal, _
                                       ByVal stredCodling As String, ByVal strPrzNet1 As String, _
                                       ByVal strPrzNet2 As String, ByVal strPrzNet3 As String, _
                                       ByVal strPrzNet4 As String, ByVal strPrzNet5 As String, ByVal lIITTListsar As Integer, _
                                       ByVal strAr_note As String, ByVal strAr_descr As String, _
                                       ByVal strAr_desint As String, ByVal strAr_unmis As String, ByVal strEdConto As String) As Boolean
    Try
      'obsoleta
      Return InsertTemp(strDitta, lIITtstli, lProg, strAr_codart, dSc1, dSc2, dSc3, dSc4, dSc5, dSc6, dDaQuantsc, dAQuantsc, _
                        strUnmissc, dSc1_2, dSc2_2, dSc3_2, dSc4_2, dSc5_2, dSc6_2, dDaQuantsc_2, dAQuantsc_2, strUnmissc_2, _
                        dSc1_3, dSc2_3, dSc3_3, dSc4_3, dSc5_3, dSc6_3, dDaQuantsc_3, dAQuantsc_3, strUnmissc_3, _
                        dSc1_4, dSc2_4, dSc3_4, dSc4_4, dSc5_4, dSc6_4, dDaQuantsc_4, dAQuantsc_4, strUnmissc_4, _
                        dPrezzo1, stredCodvalu1, dDaQuant1, dAQuant1, dPerqta1, strUnmis1, dPrezzo2, stredCodvalu2, dDaQuant2, _
                        dAQuant2, dPerqta2, strUnmis2, dPrezzo3, stredCodvalu3, dDaQuant3, dAQuant3, dPerqta3, _
                        strUnmis3, dPrezzo4, stredCodvalu4, dDaQuant4, dAQuant4, dPerqta4, strUnmis4, dPrezzo5, _
                        stredCodvalu5, dDaQuant5, dAQuant5, dPerqta5, strUnmis5, dFase, stredCodling, strPrzNet1, _
                        strPrzNet2, strPrzNet3, strPrzNet4, strPrzNet5, lIITTListsar, strAr_note, strAr_descr, _
                        strAr_desint, strAr_unmis, strEdConto, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function InsertTemp(ByVal strDitta As String, ByVal lIITtstli As Integer, _
                                         ByVal lProg As Integer, ByVal strAr_codart As String, _
                                         ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, _
                                         ByVal dSc3 As Decimal, ByVal dSc4 As Decimal, _
                                         ByVal dSc5 As Decimal, ByVal dSc6 As Decimal, _
                                         ByVal dDaQuantsc As Decimal, ByVal dAQuantsc As Decimal, _
                                         ByVal strUnmissc As String, _
                                         ByVal dSc1_2 As Decimal, ByVal dSc2_2 As Decimal, _
                                         ByVal dSc3_2 As Decimal, ByVal dSc4_2 As Decimal, _
                                         ByVal dSc5_2 As Decimal, ByVal dSc6_2 As Decimal, _
                                         ByVal dDaQuantsc_2 As Decimal, ByVal dAQuantsc_2 As Decimal, _
                                         ByVal strUnmissc_2 As String, _
                                         ByVal dSc1_3 As Decimal, ByVal dSc2_3 As Decimal, _
                                         ByVal dSc3_3 As Decimal, ByVal dSc4_3 As Decimal, _
                                         ByVal dSc5_3 As Decimal, ByVal dSc6_3 As Decimal, _
                                         ByVal dDaQuantsc_3 As Decimal, ByVal dAQuantsc_3 As Decimal, _
                                         ByVal strUnmissc_3 As String, _
                                         ByVal dSc1_4 As Decimal, ByVal dSc2_4 As Decimal, _
                                         ByVal dSc3_4 As Decimal, ByVal dSc4_4 As Decimal, _
                                         ByVal dSc5_4 As Decimal, ByVal dSc6_4 As Decimal, _
                                         ByVal dDaQuantsc_4 As Decimal, ByVal dAQuantsc_4 As Decimal, _
                                         ByVal strUnmissc_4 As String, _
                                         ByVal dPrezzo1 As Decimal, _
                                         ByVal stredCodvalu1 As String, ByVal dDaQuant1 As Decimal, _
                                         ByVal dAQuant1 As Decimal, ByVal dPerqta1 As Decimal, _
                                         ByVal strUnmis1 As String, ByVal dPrezzo2 As Decimal, _
                                         ByVal stredCodvalu2 As String, ByVal dDaQuant2 As Decimal, _
                                         ByVal dAQuant2 As Decimal, ByVal dPerqta2 As Decimal, _
                                         ByVal strUnmis2 As String, ByVal dPrezzo3 As Decimal, _
                                         ByVal stredCodvalu3 As String, ByVal dDaQuant3 As Decimal, _
                                         ByVal dAQuant3 As Decimal, ByVal dPerqta3 As Decimal, _
                                         ByVal strUnmis3 As String, ByVal dPrezzo4 As Decimal, _
                                         ByVal stredCodvalu4 As String, ByVal dDaQuant4 As Decimal, _
                                         ByVal dAQuant4 As Decimal, ByVal dPerqta4 As Decimal, _
                                         ByVal strUnmis4 As String, ByVal dPrezzo5 As Decimal, _
                                         ByVal stredCodvalu5 As String, ByVal dDaQuant5 As Decimal, _
                                         ByVal dAQuant5 As Decimal, ByVal dPerqta5 As Decimal, _
                                         ByVal strUnmis5 As String, ByVal dFase As Decimal, _
                                         ByVal stredCodling As String, ByVal strPrzNet1 As String, _
                                         ByVal strPrzNet2 As String, ByVal strPrzNet3 As String, _
                                         ByVal strPrzNet4 As String, ByVal strPrzNet5 As String, ByVal lIITTListsar As Integer, _
                                         ByVal strAr_note As String, ByVal strAr_descr As String, _
                                         ByVal strAr_desint As String, ByVal strAr_unmis As String, ByVal strEdConto As String, _
                                         ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lIITtstli, lProg, strAr_codart, dSc1, dSc2, dSc3, dSc4, dSc5, dSc6, dDaQuantsc, dAQuantsc, _
                        strUnmissc, dSc1_2, dSc2_2, dSc3_2, dSc4_2, dSc5_2, dSc6_2, dDaQuantsc_2, dAQuantsc_2, strUnmissc_2, _
                        dSc1_3, dSc2_3, dSc3_3, dSc4_3, dSc5_3, dSc6_3, dDaQuantsc_3, dAQuantsc_3, strUnmissc_3, _
                        dSc1_4, dSc2_4, dSc3_4, dSc4_4, dSc5_4, dSc6_4, dDaQuantsc_4, dAQuantsc_4, strUnmissc_4, _
                        dPrezzo1, stredCodvalu1, dDaQuant1, dAQuant1, dPerqta1, strUnmis1, dPrezzo2, stredCodvalu2, dDaQuant2, _
                        dAQuant2, dPerqta2, strUnmis2, dPrezzo3, stredCodvalu3, dDaQuant3, dAQuant3, dPerqta3, _
                        strUnmis3, dPrezzo4, stredCodvalu4, dDaQuant4, dAQuant4, dPerqta4, strUnmis4, dPrezzo5, _
                        stredCodvalu5, dDaQuant5, dAQuant5, dPerqta5, strUnmis5, dFase, stredCodling, strPrzNet1, _
                        strPrzNet2, strPrzNet3, strPrzNet4, strPrzNet5, lIITTListsar, strAr_note, strAr_descr, _
                        strAr_desint, strAr_unmis, strEdConto, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "INSERT INTO TTSTLI (codditt, instid, sta_progr, sta_codart, sta_scont1, sta_scont2," & _
        " sta_scont3, sta_scont4, sta_scont5, sta_scont6, sta_scdaquant, sta_scaquant, sta_scunmis," & _
        " sta_scont1_2, sta_scont2_2, sta_scont3_2, sta_scont4_2, sta_scont5_2, sta_scont6_2, sta_scdaquant_2, sta_scaquant_2, sta_scunmis_2," & _
        " sta_scont1_3, sta_scont2_3, sta_scont3_3, sta_scont4_3, sta_scont5_3, sta_scont6_3, sta_scdaquant_3, sta_scaquant_3, sta_scunmis_3," & _
        " sta_scont1_4, sta_scont2_4, sta_scont3_4, sta_scont4_4, sta_scont5_4, sta_scont6_4, sta_scdaquant_4, sta_scaquant_4, sta_scunmis_4," & _
        " sta_prz1, sta_codvalu1, sta_daquant1, sta_aquant1, sta_perqta1, sta_unmis1," & _
        " sta_prz2, sta_codvalu2, sta_daquant2, sta_aquant2, sta_perqta2, sta_unmis2," & _
        " sta_prz3, sta_codvalu3, sta_daquant3, sta_aquant3, sta_perqta3, sta_unmis3," & _
        " sta_prz4, sta_codvalu4, sta_daquant4, sta_aquant4, sta_perqta4, sta_unmis4," & _
        " sta_prz5, sta_codvalu5, sta_daquant5, sta_aquant5, sta_perqta5, sta_unmis5," & _
        " sta_fase, sta_codling, sta_prznet1, sta_prznet2, sta_prznet3, sta_prznet4, sta_prznet5, sta_conto, sta_coddest)" & _
        " VALUES (" & CStrSQL(strDitta) & ", " & lIITtstli & ", " & lProg & ", " & CStrSQL(strAr_codart) & ", " & _
        CDblSQL(dSc1) & ", " & CDblSQL(dSc2) & ", " & CDblSQL(dSc3) & ", " & _
        CDblSQL(dSc4) & ", " & CDblSQL(dSc5) & ", " & CDblSQL(dSc6) & ", " & _
        CDblSQL(dDaQuantsc) & ", " & CDblSQL(dAQuantsc) & ", " & CStrSQL(strUnmissc) & ", " & _
        CDblSQL(dSc1_2) & ", " & CDblSQL(dSc2_2) & ", " & CDblSQL(dSc3_2) & ", " & _
        CDblSQL(dSc4_2) & ", " & CDblSQL(dSc5_2) & ", " & CDblSQL(dSc6_2) & ", " & _
        CDblSQL(dDaQuantsc_2) & ", " & CDblSQL(dAQuantsc_2) & ", " & CStrSQL(strUnmissc_2) & ", " & _
        CDblSQL(dSc1_3) & ", " & CDblSQL(dSc2_3) & ", " & CDblSQL(dSc3_3) & ", " & _
        CDblSQL(dSc4_3) & ", " & CDblSQL(dSc5_3) & ", " & CDblSQL(dSc6_3) & ", " & _
        CDblSQL(dDaQuantsc_3) & ", " & CDblSQL(dAQuantsc_3) & ", " & CStrSQL(strUnmissc_3) & ", " & _
        CDblSQL(dSc1_4) & ", " & CDblSQL(dSc2_4) & ", " & CDblSQL(dSc3_4) & ", " & _
        CDblSQL(dSc4_4) & ", " & CDblSQL(dSc5_4) & ", " & CDblSQL(dSc6_4) & ", " & _
        CDblSQL(dDaQuantsc_4) & ", " & CDblSQL(dAQuantsc_4) & ", " & CStrSQL(strUnmissc_4) & ", " & _
        CDblSQL(dPrezzo1) & ", " & stredCodvalu1 & ", " & CDblSQL(dDaQuant1) & ", " & _
        CDblSQL(dAQuant1) & ", " & CDblSQL(dPerqta1) & ", " & CStrSQL(strUnmis1) & ", " & _
        CDblSQL(dPrezzo2) & ", " & stredCodvalu2 & ", " & CDblSQL(dDaQuant2) & ", " & _
        CDblSQL(dAQuant2) & ", " & CDblSQL(dPerqta2) & ", " & CStrSQL(strUnmis2) & ", " & _
        CDblSQL(dPrezzo3) & ", " & stredCodvalu3 & ", " & CDblSQL(dDaQuant3) & ", " & _
        CDblSQL(dAQuant3) & ", " & CDblSQL(dPerqta3) & ", " & CStrSQL(strUnmis3) & ", " & _
        CDblSQL(dPrezzo4) & ", " & stredCodvalu4 & ", " & CDblSQL(dDaQuant4) & ", " & _
        CDblSQL(dAQuant4) & ", " & CDblSQL(dPerqta4) & ", " & CStrSQL(strUnmis4) & ", " & _
        CDblSQL(dPrezzo5) & ", " & stredCodvalu5 & ", " & CDblSQL(dDaQuant5) & ", " & _
        CDblSQL(dAQuant5) & ", " & CDblSQL(dPerqta5) & ", " & CStrSQL(strUnmis5) & ", " & _
        CDblSQL(dFase) & ", " & _
        stredCodling & ", " & _
        CStrSQL(strPrzNet1) & ", " & CStrSQL(strPrzNet2) & ", " & _
        CStrSQL(strPrzNet3) & ", " & CStrSQL(strPrzNet4) & ", " & CStrSQL(strPrzNet4) & ", " & _
        NTSCInt(strEdConto) & ", " & lCoddest & ")"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "INSERT INTO TTLISTSAR (codditt, instid, lsa_codart, lsa_note, lsa_riga," & _
        " lsa_flag, lsa_descr, lsa_desint, lsa_unmis, lsa_fase, lsa_desfase)" & _
        " VALUES (" & CStrSQL(strDitta) & ", " & lIITTListsar & ", " & _
        CStrSQL(strAr_codart) & ", " & _
        CStrSQL(Left(Trim(strAr_note), 50)) & ", " & lProg & ", 'N', " & _
        CStrSQL(strAr_descr) & ", " & CStrSQL(strAr_desint) & ", " & _
        CStrSQL(strAr_unmis) & ", " & CDblSQL(dFase) & ", NULL)"

      Try
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Catch ex As Exception
        'ignoro l'errore: se stampa listini specifici per cliente/destinazione lo stesso articolo può apparire più volte
      End Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function DeleteTemp(ByVal strDitta As String, ByVal lIITtstli As Integer, _
                                         ByVal lIITTListsar As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM TTSTLI WHERE instid = " & lIITtstli & _
        " AND codditt = " & CStrSQL(strDitta) & _
        " AND sta_scont1 = 0 AND sta_scont2 = 0 AND sta_scont3 = 0" & _
        " AND sta_scont4 = 0 AND sta_scont5 = 0 AND sta_scont6 = 0" & _
        " AND sta_scont1_2 = 0 AND sta_scont2_2 = 0 AND sta_scont3_2 = 0" & _
        " AND sta_scont4_2 = 0 AND sta_scont5_2 = 0 AND sta_scont6_2 = 0" & _
        " AND sta_scont1_3 = 0 AND sta_scont2_3 = 0 AND sta_scont3_3 = 0" & _
        " AND sta_scont4_3 = 0 AND sta_scont5_3 = 0 AND sta_scont6_3 = 0" & _
        " AND sta_scont1_4 = 0 AND sta_scont2_4 = 0 AND sta_scont3_4 = 0" & _
        " AND sta_scont4_4 = 0 AND sta_scont5_4 = 0 AND sta_scont6_4 = 0" & _
        " AND sta_prz1 = 0 AND sta_prz2 = 0 AND sta_prz3 = 0 AND sta_prz4 = 0 AND sta_prz5 = 0"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM TTLISTSAR" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTListsar & _
        " AND lsa_codart NOT IN (SELECT sta_codart FROM TTSTLI" & _
                               " WHERE codditt = " & CStrSQL(strDitta) & _
                               " AND instid = " & lIITtstli & ")"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTTstli(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 sta_progr FROM TTSTLI" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " ORDER BY instid, sta_progr"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTSTLI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function DeleteTempSconti(ByVal strDitta As String, ByVal lIITtstli As Integer, _
                                       ByVal lIITTListsar As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM TTSTLI" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lIITtstli & _
               " AND (sta_scont1 <> 0 OR sta_scont2 <> 0 OR sta_scont3 <> 0" & _
                 " OR sta_scont4 <> 0 OR sta_scont5 <> 0 OR sta_scont6 <> 0" & _
                 " OR sta_scont1_2 <> 0 OR sta_scont2_2 <> 0 OR sta_scont3_2 <> 0" & _
                 " OR sta_scont4_2 <> 0 OR sta_scont5_2 <> 0 OR sta_scont6_2 <> 0" & _
                 " OR sta_scont1_3 <> 0 OR sta_scont2_3 <> 0 OR sta_scont3_3 <> 0" & _
                 " OR sta_scont4_3 <> 0 OR sta_scont5_3 <> 0 OR sta_scont6_3 <> 0" & _
                 " OR sta_scont1_4 <> 0 OR sta_scont2_4 <> 0 OR sta_scont3_4 <> 0" & _
                 " OR sta_scont4_4 <> 0 OR sta_scont5_4 <> 0 OR sta_scont6_4 <> 0)" & _
               " AND sta_prz1 = 0" & _
               " AND sta_prz2 = 0" & _
               " AND sta_prz3 = 0" & _
               " AND sta_prz4 = 0" & _
               " AND sta_prz5 = 0"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM TTLISTSAR" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTListsar & _
        " AND lsa_codart NOT IN (SELECT sta_codart FROM TTSTLI" & _
                               " WHERE codditt = " & CStrSQL(strDitta) & _
                               " AND instid = " & lIITtstli & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function ElaboraArtval(ByVal strDitta As String, ByVal lIITtstli As Integer, _
                                          ByVal lIITTListsar As Integer, ByVal stredCodling As String) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer
    Dim ds As DataSet = Nothing
    Try
      strSQL = "SELECT artval.* FROM artval INNER JOIN TTSTLI ON artval.codditt = TTSTLI.codditt AND artval.ax_codart = TTSTLI.sta_codart" & _
        " WHERE TTSTLI.codditt = " & CStrSQL(strDitta) & _
        " AND TTSTLI.instid = " & lIITtstli & _
        " AND artval.ax_codvalu = " & stredCodling

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTSTLI")

      For i = 0 To ds.Tables("TTSTLI").Rows.Count - 1
        With ds.Tables("TTSTLI").Rows(i)
          strSQL = "UPDATE TTLISTSAR" & _
            " SET lsa_descr = " & CStrSQL(!ax_descr) & "," & _
            " lsa_desint = " & CStrSQL(!ax_desint) & "," & _
            " lsa_note = " & CStrSQL(Left(Trim(NTSCStr(!ax_note)), 50)) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lIITTListsar & _
            " AND lsa_codart = " & CStrSQL(!ax_codart)
        End With

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetListsar(ByVal strDitta As String, ByVal stredCodlsar As String, _
                                         ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 lsa_codlsar FROM listsar" & _
        " WHERE lsa_codlsar = " & stredCodlsar & _
        " AND codditt = " & CStrSQL(strDitta) & _
        " ORDER BY lsa_codlsar, lsa_riga"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTSAR")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetClie(ByVal strDitta As String, ByVal strWhere As String, ByRef dttOut As DataTable) As Boolean
    Try
      GetClie(strDitta, strWhere, dttOut, False)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetClie(ByVal strDitta As String, ByVal strWhere As String, ByRef dttOut As DataTable, _
    ByVal bIsCRMUser As Boolean) As Boolean
    Dim bSelAnagra As Boolean = True
    Dim strSQL As String = ""
    Dim strWhereTMP As String = ""
    Dim dttForn As New DataTable

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strWhere, dttOut, bIsCRMUser})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(2), DataTable)
        Return CBool(oOut)
      End If
      '----------------
      If strWhere.Length > 0 Then
        If Left(strWhere, 1) = "{" Then
          strWhere = Mid(strWhere, 2)
          bSelAnagra = False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- (an_tipo = 'C' OR an_tipo = 'F') serve se uno fa una selezione per sottoconti, così non tira su niente
      '--- Se Utente CRM, aggiunge la tabella LEADS in JOIN
      '--------------------------------------------------------------------------------------------------------------
      If bIsCRMUser Then
        dttForn = PermessiFornitoriCRM(strDitta)

        If dttForn.Rows.Count = 0 OrElse NTSCStr(dttForn.Rows(0)!opdi_amm) = "N" Then
          strSQL = "SELECT DISTINCT an_conto, le_coddest as xx_coddest FROM anagra" & _
                  " INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                  " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                  " AND an_tipo IN ('C', 'F') " & _
                  ComponiWhereStampaWord(strDitta, strWhere)

          dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        Else
          If InStr(strWhere, "§") > 0 Then
            strWhereTMP = Mid(strWhere, InStr(strWhere, "§"))
            If InStr(strWhereTMP, "§an_tipo = 'C'§") > 0 Then
              strWhereTMP = Mid(strWhereTMP, 1, InStr(strWhereTMP, "§an_tipo = 'C'§") - 1) & _
                Mid(strWhereTMP, InStr(strWhereTMP, "§an_tipo = 'C'") + "§an_tipo = 'C'".Length)
            End If
            If InStr(strWhereTMP, "§le_coddest = 0§") > 0 Then
              strWhereTMP = Mid(strWhereTMP, 1, (InStr(strWhereTMP, "§le_coddest = 0§") - 1)) & _
                Mid(strWhereTMP, (InStr(strWhereTMP, "§le_coddest = 0§") + "§le_coddest = 0§".Length - 1))
              strWhereTMP = strWhereTMP.Trim
              If Right(strWhereTMP, 1) = "§" Then strWhereTMP = Mid(strWhereTMP, 1, strWhereTMP.Length - 1)
            End If
            If InStr(strWhereTMP, "§le_coddest = 0") > 0 Then
              strWhereTMP = Mid(strWhereTMP, 1, (InStr(strWhereTMP, "§le_coddest = 0") - 1)) & _
                Mid(strWhereTMP, (InStr(strWhereTMP, "§le_coddest = 0") + "§le_coddest = 0".Length + 1))
              strWhereTMP = strWhereTMP.Trim
              If Right(strWhereTMP, 1) = "§" Then strWhereTMP = Mid(strWhereTMP, 1, strWhereTMP.Length - 1)
            End If
          End If
          strSQL = "SELECT DISTINCT an_conto, le_coddest as xx_coddest FROM anagra" & _
                   " INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                   " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                   " AND an_tipo = 'C' " & _
                   ComponiWhereStampaWord(strDitta, strWhere) & _
                   " UNION ALL SELECT DISTINCT an_conto, 0 as xx_coddest FROM anagra" & _
                   " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                   " AND an_tipo = 'F'" & _
                   ComponiWhereStampaWord(strDitta, IIf(bSelAnagra = True, strWhereTMP, strWhere).ToString)
          If InStr(strSQL, " AND () ") > 0 Then
            strSQL = Mid(strSQL, 1, (InStr(strSQL, " AND () ") - 1)) & _
              Mid(strSQL, (InStr(strSQL, " AND () ") + " AND () ".Length - 1))
            strSQL = strSQL.Trim
          End If
          dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
      Else
        strSQL = "SELECT DISTINCT an_conto, 0 as xx_coddest FROM anagra" & _
                 " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                 " AND an_tipo IN ('C', 'F') " & _
                 ComponiWhereStampaWord(strDitta, strWhere)

        dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttForn.Clear()
      dttForn.Dispose()
    End Try
  End Function


  Public Overridable Function AggiungiDestdiv(ByVal strDitta As String, ByRef dttClie As DataTable) As Boolean
    'moltiplico le righe del cliente tante volte quenate sono le destinazioni diverse
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      If dttClie.Rows.Count = 0 Then Return True
      strSQL = "SELECT DISTINCT lc_conto, lc_coddest FROM listini " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND lc_conto <> 0 AND lc_coddest <> 0" & _
               " AND lc_conto IN ("
      For Each dtrT As DataRow In dttClie.Select("an_conto <> 0")
        strSQL += NTSCStr(dtrT!an_conto) & ","
      Next
      strSQL = strSQL.Substring(0, strSQL.Length - 1) & ")"

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For Each dtrT As DataRow In dttTmp.Rows
        dttClie.Rows.Add(New Object() {dtrT!lc_conto, dtrT!lc_coddest})
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


  Public Overridable Function InsertListSes(ByVal strDitta As String, ByVal lSessione As Integer, ByVal strAr_codart As String, _
                                            ByVal lClscan As Integer, ByVal lClscar As Integer, _
                                            ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, ByVal dSc3 As Decimal, ByVal dSc4 As Decimal, _
                                            ByVal dSc5 As Decimal, ByVal dSc6 As Decimal, ByVal dDaQuantsc As Decimal, ByVal dAQuantsc As Decimal, _
                                            ByVal strTiposc As String, ByVal dtDadatasc As Date, _
                                            ByVal dtAdatasc As Date, ByVal lCodtpro As Integer, ByVal strNuovoSc As String, _
                                            ByVal dSc1_2 As Decimal, ByVal dSc2_2 As Decimal, ByVal dSc3_2 As Decimal, ByVal dSc4_2 As Decimal, _
                                            ByVal dSc5_2 As Decimal, ByVal dSc6_2 As Decimal, ByVal dDaQuantsc_2 As Decimal, ByVal dAQuantsc_2 As Decimal, _
                                            ByVal strTiposc_2 As String, ByVal dtDadatasc_2 As Date, _
                                            ByVal dtAdatasc_2 As Date, ByVal lCodtpro_2 As Integer, ByVal strNuovoSc_2 As String, _
                                            ByVal dSc1_3 As Decimal, ByVal dSc2_3 As Decimal, ByVal dSc3_3 As Decimal, ByVal dSc4_3 As Decimal, _
                                            ByVal dSc5_3 As Decimal, ByVal dSc6_3 As Decimal, ByVal dDaQuantsc_3 As Decimal, ByVal dAQuantsc_3 As Decimal, _
                                            ByVal strTiposc_3 As String, ByVal dtDadatasc_3 As Date, _
                                            ByVal dtAdatasc_3 As Date, ByVal lCodtpro_3 As Integer, ByVal strNuovoSc_3 As String, _
                                            ByVal dSc1_4 As Decimal, ByVal dSc2_4 As Decimal, ByVal dSc3_4 As Decimal, ByVal dSc4_4 As Decimal, _
                                            ByVal dSc5_4 As Decimal, ByVal dSc6_4 As Decimal, ByVal dDaQuantsc_4 As Decimal, ByVal dAQuantsc_4 As Decimal, _
                                            ByVal strTiposc_4 As String, ByVal dtDadatasc_4 As Date, _
                                            ByVal dtAdatasc_4 As Date, ByVal lCodtpro_4 As Integer, ByVal strNuovoSc_4 As String, _
                                            ByVal lListino1 As Integer, ByVal dtDadata1 As Date, ByVal dtAdata1 As Date, ByVal strTipo1 As String, _
                                            ByVal lCodlavo1 As Integer, ByVal dPrezzo1 As Decimal, ByVal dDaQuant1 As Decimal, ByVal dAQuant1 As Decimal, _
                                            ByVal strUnmis1 As String, ByVal lCodValu1 As Integer, ByVal lProgr1 As Integer, _
                                            ByVal lListino2 As Integer, ByVal dtDadata2 As Date, ByVal dtAdata2 As Date, ByVal strTipo2 As String, _
                                            ByVal lCodlavo2 As Integer, ByVal dPrezzo2 As Decimal, ByVal dDaQuant2 As Decimal, ByVal dAQuant2 As Decimal, _
                                            ByVal strUnmis2 As String, ByVal lCodValu2 As Integer, ByVal lProgr2 As Integer, _
                                            ByVal lListino3 As Integer, ByVal dtDadata3 As Date, ByVal dtAdata3 As Date, ByVal strTipo3 As String, _
                                            ByVal lCodlavo3 As Integer, ByVal dPrezzo3 As Decimal, ByVal dDaQuant3 As Decimal, ByVal dAQuant3 As Decimal, _
                                            ByVal strUnmis3 As String, ByVal lCodValu3 As Integer, ByVal lProgr3 As Integer, _
                                            ByVal lListino4 As Integer, ByVal dtDadata4 As Date, ByVal dtAdata4 As Date, ByVal strTipo4 As String, _
                                            ByVal lCodlavo4 As Integer, ByVal dPrezzo4 As Decimal, ByVal dDaQuant4 As Decimal, ByVal dAQuant4 As Decimal, _
                                            ByVal strUnmis4 As String, ByVal lCodValu4 As Integer, ByVal lProgr4 As Integer, _
                                            ByVal lListino5 As Integer, ByVal dtDadata5 As Date, ByVal dtAdata5 As Date, ByVal strTipo5 As String, _
                                            ByVal lCodlavo5 As Integer, ByVal dPrezzo5 As Decimal, ByVal dDaQuant5 As Decimal, ByVal dAQuant5 As Decimal, _
                                            ByVal strUnmis5 As String, ByVal lCodValu5 As Integer, ByVal lProgr5 As Integer, _
                                            ByVal dFase As Decimal, ByVal stredCodling As String, ByVal dPerqta As Decimal, _
                                            ByVal strPrzNet1 As String, ByVal strPrzNet2 As String, ByVal strPrzNet3 As String, _
                                            ByVal strPrzNet4 As String, ByVal strPrzNet5 As String, _
                                            ByVal strAr_note As String, ByVal strAr_descr As String, _
                                            ByVal strAr_desint As String, ByVal strAr_unmis As String, ByVal strEdConto As String) As Boolean
    Try
      Return InsertListSes(strDitta, lSessione, strAr_codart, lClscan, lClscar, dSc1, dSc2, dSc3, dSc4, dSc5, _
        dSc6, dDaQuantsc, dAQuantsc, strTiposc, dtDadatasc, dtAdatasc, lCodtpro, strNuovoSc, dSc1_2, dSc2_2, _
        dSc3_2, dSc4_2, dSc5_2, dSc6_2, dDaQuantsc_2, dAQuantsc_2, strTiposc_2, dtDadatasc_2, dtAdatasc_2, _
        lCodtpro_2, strNuovoSc_2, dSc1_3, dSc2_3, dSc3_3, dSc4_3, dSc5_3, dSc6_3, dDaQuantsc_3, dAQuantsc_3, _
        strTiposc_3, dtDadatasc_3, dtAdatasc_3, lCodtpro_3, strNuovoSc_3, dSc1_4, dSc2_4, dSc3_4, dSc4_4, _
        dSc5_4, dSc6_4, dDaQuantsc_4, dAQuantsc_4, strTiposc_4, dtDadatasc_4, dtAdatasc_4, lCodtpro_4, _
        strNuovoSc_4, lListino1, dtDadata1, dtAdata1, strTipo1, lCodlavo1, dPrezzo1, dDaQuant1, dAQuant1, _
        strUnmis1, lCodValu1, lProgr1, lListino2, dtDadata2, dtAdata2, strTipo2, lCodlavo2, dPrezzo2, dDaQuant2, _
        dAQuant2, strUnmis2, lCodValu2, lProgr2, lListino3, dtDadata3, dtAdata3, strTipo3, lCodlavo3, dPrezzo3, _
        dDaQuant3, dAQuant3, strUnmis3, lCodValu3, lProgr3, lListino4, dtDadata4, dtAdata4, strTipo4, _
        lCodlavo4, dPrezzo4, dDaQuant4, dAQuant4, strUnmis4, lCodValu4, lProgr4, lListino5, dtDadata5, _
        dtAdata5, strTipo5, lCodlavo5, dPrezzo5, dDaQuant5, dAQuant5, strUnmis5, lCodValu5, lProgr5, _
        dFase, stredCodling, dPerqta, strPrzNet1, strPrzNet2, strPrzNet3, strPrzNet4, strPrzNet5, strAr_note, _
        strAr_descr, strAr_desint, strAr_unmis, strEdConto, 0, 0, 0, 0, 0, 0)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function InsertListSes(ByVal strDitta As String, ByVal lSessione As Integer, ByVal strAr_codart As String, _
                                          ByVal lClscan As Integer, ByVal lClscar As Integer, _
                                          ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, ByVal dSc3 As Decimal, ByVal dSc4 As Decimal, _
                                          ByVal dSc5 As Decimal, ByVal dSc6 As Decimal, ByVal dDaQuantsc As Decimal, ByVal dAQuantsc As Decimal, _
                                          ByVal strTiposc As String, ByVal dtDadatasc As Date, _
                                          ByVal dtAdatasc As Date, ByVal lCodtpro As Integer, ByVal strNuovoSc As String, _
                                          ByVal dSc1_2 As Decimal, ByVal dSc2_2 As Decimal, ByVal dSc3_2 As Decimal, ByVal dSc4_2 As Decimal, _
                                          ByVal dSc5_2 As Decimal, ByVal dSc6_2 As Decimal, ByVal dDaQuantsc_2 As Decimal, ByVal dAQuantsc_2 As Decimal, _
                                          ByVal strTiposc_2 As String, ByVal dtDadatasc_2 As Date, _
                                          ByVal dtAdatasc_2 As Date, ByVal lCodtpro_2 As Integer, ByVal strNuovoSc_2 As String, _
                                          ByVal dSc1_3 As Decimal, ByVal dSc2_3 As Decimal, ByVal dSc3_3 As Decimal, ByVal dSc4_3 As Decimal, _
                                          ByVal dSc5_3 As Decimal, ByVal dSc6_3 As Decimal, ByVal dDaQuantsc_3 As Decimal, ByVal dAQuantsc_3 As Decimal, _
                                          ByVal strTiposc_3 As String, ByVal dtDadatasc_3 As Date, _
                                          ByVal dtAdatasc_3 As Date, ByVal lCodtpro_3 As Integer, ByVal strNuovoSc_3 As String, _
                                          ByVal dSc1_4 As Decimal, ByVal dSc2_4 As Decimal, ByVal dSc3_4 As Decimal, ByVal dSc4_4 As Decimal, _
                                          ByVal dSc5_4 As Decimal, ByVal dSc6_4 As Decimal, ByVal dDaQuantsc_4 As Decimal, ByVal dAQuantsc_4 As Decimal, _
                                          ByVal strTiposc_4 As String, ByVal dtDadatasc_4 As Date, _
                                          ByVal dtAdatasc_4 As Date, ByVal lCodtpro_4 As Integer, ByVal strNuovoSc_4 As String, _
                                          ByVal lListino1 As Integer, ByVal dtDadata1 As Date, ByVal dtAdata1 As Date, ByVal strTipo1 As String, _
                                          ByVal lCodlavo1 As Integer, ByVal dPrezzo1 As Decimal, ByVal dDaQuant1 As Decimal, ByVal dAQuant1 As Decimal, _
                                          ByVal strUnmis1 As String, ByVal lCodValu1 As Integer, ByVal lProgr1 As Integer, _
                                          ByVal lListino2 As Integer, ByVal dtDadata2 As Date, ByVal dtAdata2 As Date, ByVal strTipo2 As String, _
                                          ByVal lCodlavo2 As Integer, ByVal dPrezzo2 As Decimal, ByVal dDaQuant2 As Decimal, ByVal dAQuant2 As Decimal, _
                                          ByVal strUnmis2 As String, ByVal lCodValu2 As Integer, ByVal lProgr2 As Integer, _
                                          ByVal lListino3 As Integer, ByVal dtDadata3 As Date, ByVal dtAdata3 As Date, ByVal strTipo3 As String, _
                                          ByVal lCodlavo3 As Integer, ByVal dPrezzo3 As Decimal, ByVal dDaQuant3 As Decimal, ByVal dAQuant3 As Decimal, _
                                          ByVal strUnmis3 As String, ByVal lCodValu3 As Integer, ByVal lProgr3 As Integer, _
                                          ByVal lListino4 As Integer, ByVal dtDadata4 As Date, ByVal dtAdata4 As Date, ByVal strTipo4 As String, _
                                          ByVal lCodlavo4 As Integer, ByVal dPrezzo4 As Decimal, ByVal dDaQuant4 As Decimal, ByVal dAQuant4 As Decimal, _
                                          ByVal strUnmis4 As String, ByVal lCodValu4 As Integer, ByVal lProgr4 As Integer, _
                                          ByVal lListino5 As Integer, ByVal dtDadata5 As Date, ByVal dtAdata5 As Date, ByVal strTipo5 As String, _
                                          ByVal lCodlavo5 As Integer, ByVal dPrezzo5 As Decimal, ByVal dDaQuant5 As Decimal, ByVal dAQuant5 As Decimal, _
                                          ByVal strUnmis5 As String, ByVal lCodValu5 As Integer, ByVal lProgr5 As Integer, _
                                          ByVal dFase As Decimal, ByVal stredCodling As String, ByVal dPerqta As Decimal, _
                                          ByVal strPrzNet1 As String, ByVal strPrzNet2 As String, ByVal strPrzNet3 As String, _
                                          ByVal strPrzNet4 As String, ByVal strPrzNet5 As String, _
                                          ByVal strAr_note As String, ByVal strAr_descr As String, _
                                          ByVal strAr_desint As String, ByVal strAr_unmis As String, ByVal strEdConto As String, _
                                          ByVal nCodpromo1 As Integer, ByVal nCodpromo2 As Integer, _
                                          ByVal nCodpromo3 As Integer, ByVal nCodpromo4 As Integer, _
                                          ByVal nCodpromo5 As Integer) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lSessione, strAr_codart, lClscan, lClscar, _
                                            dSc1, dSc2, dSc3, dSc4, dSc5, dSc6, dDaQuantsc, dAQuantsc, _
                                            strTiposc, dtDadatasc, dtAdatasc, lCodtpro, strNuovoSc, _
                                            dSc1_2, dSc2_2, dSc3_2, dSc4_2, dSc5_2, dSc6_2, _
                                            dDaQuantsc_2, dAQuantsc_2, strTiposc_2, dtDadatasc_2, dtAdatasc_2, _
                                            lCodtpro_2, strNuovoSc_2, _
                                            dSc1_3, dSc2_3, dSc3_3, dSc4_3, dSc5_3, dSc6_3, _
                                            dDaQuantsc_3, dAQuantsc_3, strTiposc_3, dtDadatasc_3, dtAdatasc_3, _
                                            lCodtpro_3, strNuovoSc_3, _
                                            dSc1_4, dSc2_4, dSc3_4, dSc4_4, dSc5_4, dSc6_4, _
                                            dDaQuantsc_4, dAQuantsc_4, strTiposc_4, dtDadatasc_4, dtAdatasc_4, _
                                            lCodtpro_4, strNuovoSc_4, lListino1, dtDadata1, dtAdata1, strTipo1, _
                                            lCodlavo1, dPrezzo1, dDaQuant1, dAQuant1, strUnmis1, lCodValu1, _
                                            lProgr1, lListino2, dtDadata2, dtAdata2, strTipo2, lCodlavo2, _
                                            dPrezzo2, dDaQuant2, dAQuant2, strUnmis2, lCodValu2, lProgr2, _
                                            lListino3, dtDadata3, dtAdata3, strTipo3, lCodlavo3, dPrezzo3, _
                                            dDaQuant3, dAQuant3, strUnmis3, lCodValu3, lProgr3, lListino4, _
                                            dtDadata4, dtAdata4, strTipo4, lCodlavo4, dPrezzo4, dDaQuant4, _
                                            dAQuant4, strUnmis4, lCodValu4, lProgr4, lListino5, dtDadata5, _
                                            dtAdata5, strTipo5, lCodlavo5, dPrezzo5, dDaQuant5, dAQuant5, _
                                            strUnmis5, lCodValu5, lProgr5, dFase, stredCodling, dPerqta, _
                                            strPrzNet1, strPrzNet2, strPrzNet3, strPrzNet4, strPrzNet5, _
                                            strAr_note, strAr_descr, strAr_desint, strAr_unmis, strEdConto, _
                                            nCodpromo1, nCodpromo2, nCodpromo3, nCodpromo4, nCodpromo5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------
      Return InsertListSes(strDitta, lSessione, strAr_codart, lClscan, lClscar, dSc1, dSc2, dSc3, dSc4, dSc5, _
        dSc6, dDaQuantsc, dAQuantsc, strTiposc, dtDadatasc, dtAdatasc, lCodtpro, strNuovoSc, dSc1_2, dSc2_2, _
        dSc3_2, dSc4_2, dSc5_2, dSc6_2, dDaQuantsc_2, dAQuantsc_2, strTiposc_2, dtDadatasc_2, dtAdatasc_2, _
        lCodtpro_2, strNuovoSc_2, dSc1_3, dSc2_3, dSc3_3, dSc4_3, dSc5_3, dSc6_3, dDaQuantsc_3, dAQuantsc_3, _
        strTiposc_3, dtDadatasc_3, dtAdatasc_3, lCodtpro_3, strNuovoSc_3, dSc1_4, dSc2_4, dSc3_4, dSc4_4, _
        dSc5_4, dSc6_4, dDaQuantsc_4, dAQuantsc_4, strTiposc_4, dtDadatasc_4, dtAdatasc_4, lCodtpro_4, _
        strNuovoSc_4, lListino1, dtDadata1, dtAdata1, strTipo1, lCodlavo1, dPrezzo1, dDaQuant1, dAQuant1, _
        strUnmis1, lCodValu1, lProgr1, lListino2, dtDadata2, dtAdata2, strTipo2, lCodlavo2, dPrezzo2, dDaQuant2, _
        dAQuant2, strUnmis2, lCodValu2, lProgr2, lListino3, dtDadata3, dtAdata3, strTipo3, lCodlavo3, dPrezzo3, _
        dDaQuant3, dAQuant3, strUnmis3, lCodValu3, lProgr3, lListino4, dtDadata4, dtAdata4, strTipo4, _
        lCodlavo4, dPrezzo4, dDaQuant4, dAQuant4, strUnmis4, lCodValu4, lProgr4, lListino5, dtDadata5, _
        dtAdata5, strTipo5, lCodlavo5, dPrezzo5, dDaQuant5, dAQuant5, strUnmis5, lCodValu5, lProgr5, _
        dFase, stredCodling, dPerqta, strPrzNet1, strPrzNet2, strPrzNet3, strPrzNet4, strPrzNet5, strAr_note, _
        strAr_descr, strAr_desint, strAr_unmis, strEdConto, nCodpromo1, nCodpromo2, nCodpromo3, nCodpromo4, nCodpromo5, 0)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function InsertListSes(ByVal strDitta As String, ByVal lSessione As Integer, ByVal strAr_codart As String, _
                                            ByVal lClscan As Integer, ByVal lClscar As Integer, _
                                            ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, ByVal dSc3 As Decimal, ByVal dSc4 As Decimal, _
                                            ByVal dSc5 As Decimal, ByVal dSc6 As Decimal, ByVal dDaQuantsc As Decimal, ByVal dAQuantsc As Decimal, _
                                            ByVal strTiposc As String, ByVal dtDadatasc As Date, _
                                            ByVal dtAdatasc As Date, ByVal lCodtpro As Integer, ByVal strNuovoSc As String, _
                                            ByVal dSc1_2 As Decimal, ByVal dSc2_2 As Decimal, ByVal dSc3_2 As Decimal, ByVal dSc4_2 As Decimal, _
                                            ByVal dSc5_2 As Decimal, ByVal dSc6_2 As Decimal, ByVal dDaQuantsc_2 As Decimal, ByVal dAQuantsc_2 As Decimal, _
                                            ByVal strTiposc_2 As String, ByVal dtDadatasc_2 As Date, _
                                            ByVal dtAdatasc_2 As Date, ByVal lCodtpro_2 As Integer, ByVal strNuovoSc_2 As String, _
                                            ByVal dSc1_3 As Decimal, ByVal dSc2_3 As Decimal, ByVal dSc3_3 As Decimal, ByVal dSc4_3 As Decimal, _
                                            ByVal dSc5_3 As Decimal, ByVal dSc6_3 As Decimal, ByVal dDaQuantsc_3 As Decimal, ByVal dAQuantsc_3 As Decimal, _
                                            ByVal strTiposc_3 As String, ByVal dtDadatasc_3 As Date, _
                                            ByVal dtAdatasc_3 As Date, ByVal lCodtpro_3 As Integer, ByVal strNuovoSc_3 As String, _
                                            ByVal dSc1_4 As Decimal, ByVal dSc2_4 As Decimal, ByVal dSc3_4 As Decimal, ByVal dSc4_4 As Decimal, _
                                            ByVal dSc5_4 As Decimal, ByVal dSc6_4 As Decimal, ByVal dDaQuantsc_4 As Decimal, ByVal dAQuantsc_4 As Decimal, _
                                            ByVal strTiposc_4 As String, ByVal dtDadatasc_4 As Date, _
                                            ByVal dtAdatasc_4 As Date, ByVal lCodtpro_4 As Integer, ByVal strNuovoSc_4 As String, _
                                            ByVal lListino1 As Integer, ByVal dtDadata1 As Date, ByVal dtAdata1 As Date, ByVal strTipo1 As String, _
                                            ByVal lCodlavo1 As Integer, ByVal dPrezzo1 As Decimal, ByVal dDaQuant1 As Decimal, ByVal dAQuant1 As Decimal, _
                                            ByVal strUnmis1 As String, ByVal lCodValu1 As Integer, ByVal lProgr1 As Integer, _
                                            ByVal lListino2 As Integer, ByVal dtDadata2 As Date, ByVal dtAdata2 As Date, ByVal strTipo2 As String, _
                                            ByVal lCodlavo2 As Integer, ByVal dPrezzo2 As Decimal, ByVal dDaQuant2 As Decimal, ByVal dAQuant2 As Decimal, _
                                            ByVal strUnmis2 As String, ByVal lCodValu2 As Integer, ByVal lProgr2 As Integer, _
                                            ByVal lListino3 As Integer, ByVal dtDadata3 As Date, ByVal dtAdata3 As Date, ByVal strTipo3 As String, _
                                            ByVal lCodlavo3 As Integer, ByVal dPrezzo3 As Decimal, ByVal dDaQuant3 As Decimal, ByVal dAQuant3 As Decimal, _
                                            ByVal strUnmis3 As String, ByVal lCodValu3 As Integer, ByVal lProgr3 As Integer, _
                                            ByVal lListino4 As Integer, ByVal dtDadata4 As Date, ByVal dtAdata4 As Date, ByVal strTipo4 As String, _
                                            ByVal lCodlavo4 As Integer, ByVal dPrezzo4 As Decimal, ByVal dDaQuant4 As Decimal, ByVal dAQuant4 As Decimal, _
                                            ByVal strUnmis4 As String, ByVal lCodValu4 As Integer, ByVal lProgr4 As Integer, _
                                            ByVal lListino5 As Integer, ByVal dtDadata5 As Date, ByVal dtAdata5 As Date, ByVal strTipo5 As String, _
                                            ByVal lCodlavo5 As Integer, ByVal dPrezzo5 As Decimal, ByVal dDaQuant5 As Decimal, ByVal dAQuant5 As Decimal, _
                                            ByVal strUnmis5 As String, ByVal lCodValu5 As Integer, ByVal lProgr5 As Integer, _
                                            ByVal dFase As Decimal, ByVal stredCodling As String, ByVal dPerqta As Decimal, _
                                            ByVal strPrzNet1 As String, ByVal strPrzNet2 As String, ByVal strPrzNet3 As String, _
                                            ByVal strPrzNet4 As String, ByVal strPrzNet5 As String, _
                                            ByVal strAr_note As String, ByVal strAr_descr As String, _
                                            ByVal strAr_desint As String, ByVal strAr_unmis As String, ByVal strEdConto As String, _
                                            ByVal nCodpromo1 As Integer, ByVal nCodpromo2 As Integer, _
                                            ByVal nCodpromo3 As Integer, ByVal nCodpromo4 As Integer, _
                                            ByVal nCodpromo5 As Integer, ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lSessione, strAr_codart, lClscan, lClscar, _
                                            dSc1, dSc2, dSc3, dSc4, dSc5, dSc6, dDaQuantsc, dAQuantsc, _
                                            strTiposc, dtDadatasc, dtAdatasc, lCodtpro, strNuovoSc, _
                                            dSc1_2, dSc2_2, dSc3_2, dSc4_2, dSc5_2, dSc6_2, _
                                            dDaQuantsc_2, dAQuantsc_2, strTiposc_2, dtDadatasc_2, dtAdatasc_2, _
                                            lCodtpro_2, strNuovoSc_2, _
                                            dSc1_3, dSc2_3, dSc3_3, dSc4_3, dSc5_3, dSc6_3, _
                                            dDaQuantsc_3, dAQuantsc_3, strTiposc_3, dtDadatasc_3, dtAdatasc_3, _
                                            lCodtpro_3, strNuovoSc_3, _
                                            dSc1_4, dSc2_4, dSc3_4, dSc4_4, dSc5_4, dSc6_4, _
                                            dDaQuantsc_4, dAQuantsc_4, strTiposc_4, dtDadatasc_4, dtAdatasc_4, _
                                            lCodtpro_4, strNuovoSc_4, lListino1, dtDadata1, dtAdata1, strTipo1, _
                                            lCodlavo1, dPrezzo1, dDaQuant1, dAQuant1, strUnmis1, lCodValu1, _
                                            lProgr1, lListino2, dtDadata2, dtAdata2, strTipo2, lCodlavo2, _
                                            dPrezzo2, dDaQuant2, dAQuant2, strUnmis2, lCodValu2, lProgr2, _
                                            lListino3, dtDadata3, dtAdata3, strTipo3, lCodlavo3, dPrezzo3, _
                                            dDaQuant3, dAQuant3, strUnmis3, lCodValu3, lProgr3, lListino4, _
                                            dtDadata4, dtAdata4, strTipo4, lCodlavo4, dPrezzo4, dDaQuant4, _
                                            dAQuant4, strUnmis4, lCodValu4, lProgr4, lListino5, dtDadata5, _
                                            dtAdata5, strTipo5, lCodlavo5, dPrezzo5, dDaQuant5, dAQuant5, _
                                            strUnmis5, lCodValu5, lProgr5, dFase, stredCodling, dPerqta, _
                                            strPrzNet1, strPrzNet2, strPrzNet3, strPrzNet4, strPrzNet5, _
                                            strAr_note, strAr_descr, strAr_desint, strAr_unmis, strEdConto, _
                                            nCodpromo1, nCodpromo2, nCodpromo3, nCodpromo4, nCodpromo5, _
                                            lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "INSERT INTO listses (codditt, ls_opnome, ls_sessione, ls_codart, " & _
               " ls_scont1, ls_scont2, ls_scont3, ls_scont4, ls_scont5, ls_scont6, ls_scdaquant, ls_scaquant, " & _
               " ls_tiposc, ls_dadatasc, ls_adatasc, ls_codtpro, ls_clscan, ls_clscar, ls_nuovosc, " & _
               " ls_scont1_2, ls_scont2_2, ls_scont3_2, ls_scont4_2, ls_scont5_2, ls_scont6_2, ls_scdaquant_2, ls_scaquant_2, " & _
               " ls_tiposc_2, ls_dadatasc_2, ls_adatasc_2, ls_codtpro_2, ls_nuovosc_2, " & _
               " ls_scont1_3, ls_scont2_3, ls_scont3_3, ls_scont4_3, ls_scont5_3, ls_scont6_3, ls_scdaquant_3, ls_scaquant_3, " & _
               " ls_tiposc_3, ls_dadatasc_3, ls_adatasc_3, ls_codtpro_3, ls_nuovosc_3, " & _
               " ls_scont1_4, ls_scont2_4, ls_scont3_4, ls_scont4_4, ls_scont5_4, ls_scont6_4, ls_scdaquant_4, ls_scaquant_4, " & _
               " ls_tiposc_4, ls_dadatasc_4, ls_adatasc_4, ls_codtpro_4, ls_nuovosc_4, " & _
               " ls_progr1, ls_listino1, ls_dadata1, ls_adata1, ls_tipo1, ls_codlavo1, ls_prz1, ls_codvalu1, ls_daquant1, ls_aquant1, ls_unmis1," & _
               " ls_progr2, ls_listino2, ls_dadata2, ls_adata2, ls_tipo2, ls_codlavo2, ls_prz2, ls_codvalu2, ls_daquant2, ls_aquant2, ls_unmis2," & _
               " ls_progr3, ls_listino3, ls_dadata3, ls_adata3, ls_tipo3, ls_codlavo3, ls_prz3, ls_codvalu3, ls_daquant3, ls_aquant3, ls_unmis3," & _
               " ls_progr4, ls_listino4, ls_dadata4, ls_adata4, ls_tipo4, ls_codlavo4, ls_prz4, ls_codvalu4, ls_daquant4, ls_aquant4, ls_unmis4," & _
               " ls_progr5, ls_listino5, ls_dadata5, ls_adata5, ls_tipo5, ls_codlavo5, ls_prz5, ls_codvalu5, ls_daquant5, ls_aquant5, ls_unmis5," & _
               " ls_fase, ls_codling, ls_perqta, ls_prznet1, ls_prznet2, ls_prznet3, ls_prznet4, ls_prznet5, ls_conto," & _
               " ls_modificato1, ls_modificato2, ls_modificato3, ls_modificato4, ls_modificato5, " & _
               " ls_modificatosc, ls_modificatosc_2, ls_modificatosc_3, ls_modificatosc_4, ls_ultagg, ls_dtorsync," & _
               " ls_codpromo1, ls_codpromo2, ls_codpromo3, ls_codpromo4, ls_codpromo5, ls_coddest)" & _
               " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(oApp.User.Nome) & ", " & lSessione & ", " & CStrSQL(strAr_codart) & ", " & _
               CDblSQL(dSc1) & ", " & CDblSQL(dSc2) & ", " & CDblSQL(dSc3) & ", " & _
               CDblSQL(dSc4) & ", " & CDblSQL(dSc5) & ", " & CDblSQL(dSc6) & ", " & _
               CDblSQL(dDaQuantsc) & ", " & CDblSQL(dAQuantsc) & ", " & _
               CStrSQL(IIf(strTiposc = "P", "A", strTiposc).ToString) & ", " & CDataSQL(dtDadatasc) & ", " & CDataSQL(dtAdatasc) & ", " & lCodtpro & ", " & _
               lClscan & "," & lClscar & ", " & CStrSQL(strNuovoSc) & "," & _
               CDblSQL(dSc1_2) & ", " & CDblSQL(dSc2_2) & ", " & CDblSQL(dSc3_2) & ", " & _
               CDblSQL(dSc4_2) & ", " & CDblSQL(dSc5_2) & ", " & CDblSQL(dSc6_2) & ", " & _
               CDblSQL(dDaQuantsc_2) & ", " & CDblSQL(dAQuantsc_2) & ", " & _
               CStrSQL(IIf(strTiposc_2 = "P", "A", strTiposc_2).ToString) & ", " & CDataSQL(dtDadatasc_2) & ", " & CDataSQL(dtAdatasc_2) & ", " & lCodtpro_2 & ", " & _
               CStrSQL(strNuovoSc_2) & "," & _
               CDblSQL(dSc1_3) & ", " & CDblSQL(dSc2_3) & ", " & CDblSQL(dSc3_3) & ", " & _
               CDblSQL(dSc4_3) & ", " & CDblSQL(dSc5_3) & ", " & CDblSQL(dSc6_3) & ", " & _
               CDblSQL(dDaQuantsc_3) & ", " & CDblSQL(dAQuantsc_3) & ", " & _
               CStrSQL(IIf(strTiposc_3 = "P", "A", strTiposc_3).ToString) & ", " & CDataSQL(dtDadatasc_3) & ", " & CDataSQL(dtAdatasc_3) & ", " & lCodtpro_3 & ", " & _
               CStrSQL(strNuovoSc_3) & "," & _
               CDblSQL(dSc1_4) & ", " & CDblSQL(dSc2_4) & ", " & CDblSQL(dSc3_4) & ", " & _
               CDblSQL(dSc4_4) & ", " & CDblSQL(dSc5_4) & ", " & CDblSQL(dSc6_4) & ", " & _
               CDblSQL(dDaQuantsc_4) & ", " & CDblSQL(dAQuantsc_4) & ", " & _
               CStrSQL(IIf(strTiposc_4 = "P", "A", strTiposc_4).ToString) & ", " & CDataSQL(dtDadatasc_4) & ", " & CDataSQL(dtAdatasc_4) & ", " & lCodtpro_4 & ", " & _
               CStrSQL(strNuovoSc_4) & "," & _
               lProgr1 & ", " & lListino1 & ", " & CDataSQL(dtDadata1) & ", " & CDataSQL(dtAdata1) & ", " & CStrSQL(strTipo1) & ", " & lCodlavo1 & ", " & _
               CDblSQL(dPrezzo1) & ", " & lCodValu1 & ", " & CDblSQL(dDaQuant1) & ", " & _
               CDblSQL(dAQuant1) & ", " & CStrSQL(strUnmis1) & ", " & _
               lProgr2 & ", " & lListino2 & ", " & CDataSQL(dtDadata2) & ", " & CDataSQL(dtAdata2) & ", " & CStrSQL(strTipo2) & ", " & lCodlavo2 & ", " & _
               CDblSQL(dPrezzo2) & ", " & lCodValu2 & ", " & CDblSQL(dDaQuant2) & ", " & _
               CDblSQL(dAQuant2) & ", " & CStrSQL(strUnmis2) & ", " & _
               lProgr3 & ", " & lListino3 & ", " & CDataSQL(dtDadata3) & ", " & CDataSQL(dtAdata3) & ", " & CStrSQL(strTipo3) & ", " & lCodlavo3 & ", " & _
               CDblSQL(dPrezzo3) & ", " & lCodValu3 & ", " & CDblSQL(dDaQuant3) & ", " & _
               CDblSQL(dAQuant3) & ", " & CStrSQL(strUnmis3) & ", " & _
               lProgr4 & ", " & lListino4 & ", " & CDataSQL(dtDadata4) & ", " & CDataSQL(dtAdata4) & ", " & CStrSQL(strTipo4) & ", " & lCodlavo4 & ", " & _
               CDblSQL(dPrezzo4) & ", " & lCodValu4 & ", " & CDblSQL(dDaQuant4) & ", " & _
               CDblSQL(dAQuant4) & ", " & CStrSQL(strUnmis4) & ", " & _
               lProgr5 & ", " & lListino5 & ", " & CDataSQL(dtDadata5) & ", " & CDataSQL(dtAdata5) & ", " & CStrSQL(strTipo5) & ", " & lCodlavo5 & ", " & _
               CDblSQL(dPrezzo5) & ", " & lCodValu5 & ", " & CDblSQL(dDaQuant5) & ", " & _
               CDblSQL(dAQuant5) & ", " & CStrSQL(strUnmis5) & ", " & _
               CDblSQL(dFase) & ", " & stredCodling & ", " & CDblSQL(dPerqta) & ", " & _
               CStrSQL(strPrzNet1) & ", " & CStrSQL(strPrzNet2) & ", " & _
               CStrSQL(strPrzNet3) & ", " & CStrSQL(strPrzNet4) & ", " & CStrSQL(strPrzNet4) & ", " & NTSCInt(strEdConto) & _
               ",'N', 'N', 'N', 'N', 'N', 'N', 'N', 'N', 'N', " & CDataOraSQL(Now) & ", " & CDataOraSQL(Now) & ", " & _
               nCodpromo1 & ", " & nCodpromo2 & ", " & nCodpromo3 & ", " & nCodpromo4 & ", " & nCodpromo5 & ", " & lCoddest & ")"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DeleteListSes(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM listses WHERE ls_opnome = " & CStrSQL(oApp.User.Nome) & _
               " AND codditt = " & CStrSQL(strDitta) & " AND ls_sessione = 1 " & _
               " AND ls_scont1 = 0 AND ls_scont2 = 0 AND ls_scont3 = 0" & _
               " AND ls_scont4 = 0 AND ls_scont5 = 0 AND ls_scont6 = 0" & _
               " AND ls_scont1_2 = 0 AND ls_scont2_2 = 0 AND ls_scont3_2 = 0" & _
               " AND ls_scont4_2 = 0 AND ls_scont5_2 = 0 AND ls_scont6_2 = 0" & _
               " AND ls_scont1_3 = 0 AND ls_scont2_3 = 0 AND ls_scont3_3 = 0" & _
               " AND ls_scont4_3 = 0 AND ls_scont5_3 = 0 AND ls_scont6_3 = 0" & _
               " AND ls_scont1_4 = 0 AND ls_scont2_4 = 0 AND ls_scont3_4 = 0" & _
               " AND ls_scont4_4 = 0 AND ls_scont5_4 = 0 AND ls_scont6_4 = 0" & _
               " AND ls_prz1 = 0 AND ls_prz2 = 0 AND ls_prz3 = 0 AND ls_prz4 = 0 AND ls_prz5 = 0"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetListSes(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 ls_opnome FROM listses" & _
               " WHERE ls_opnome = " & CStrSQL(oApp.User.Nome) & _
               " AND codditt = " & CStrSQL(strDitta) & " AND ls_sessione = 1 "

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTSTLI")

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function DeleteListSesSconti(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM listses" & _
               " WHERE ls_opnome = " & CStrSQL(oApp.User.Nome) & _
               " AND codditt = " & CStrSQL(strDitta) & " AND ls_sessione = 1 " & _
               " AND (ls_scont1 <> 0 OR ls_scont2 <> 0 OR ls_scont3 <> 0" & _
                 " OR ls_scont4 <> 0 OR ls_scont5 <> 0 OR ls_scont6 <> 0" & _
                 " OR ls_scont1_2 <> 0 OR ls_scont2_2 <> 0 OR ls_scont3_2 <> 0" & _
                 " OR ls_scont4_2 <> 0 OR ls_scont5_2 <> 0 OR ls_scont6_2 <> 0" & _
                 " OR ls_scont1_3 <> 0 OR ls_scont2_3 <> 0 OR ls_scont3_3 <> 0" & _
                 " OR ls_scont4_3 <> 0 OR ls_scont5_3 <> 0 OR ls_scont6_3 <> 0" & _
                 " OR ls_scont1_4 <> 0 OR ls_scont2_4 <> 0 OR ls_scont3_4 <> 0" & _
                 " OR ls_scont4_4 <> 0 OR ls_scont5_4 <> 0 OR ls_scont6_4 <> 0)" & _
               " AND ls_prz1 = 0" & _
               " AND ls_prz2 = 0" & _
               " AND ls_prz3 = 0" & _
               " AND ls_prz4 = 0" & _
               " AND ls_prz5 = 0"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaListSes(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM listses" & _
               " WHERE ls_opnome = " & CStrSQL(oApp.User.Nome) & _
               " AND codditt = " & CStrSQL(strDitta) & " AND ls_sessione = 1 "

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function PermessiFornitoriCRM(ByVal strDitta As String) As DataTable
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT opdi_amm FROM accdito " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND opdi_opnome = " & CStrSQL(oApp.User.Nome)

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function
  Public Overridable Function PermessiClientiCRM(ByVal strDitta As String, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT an_conto" & _
        " FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto AND 0 = leads.le_coddest" & _
        " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
        " AND an_conto = " & lConto
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    End Try
  End Function

  Public Overridable Function RitornaClasseScontoAnagra(ByVal strDitta As String, ByVal lConto As Integer) As Integer
    Try
      Return RitornaClasseScontoAnagra(strDitta, lConto, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function RitornaClasseScontoAnagra(ByVal strDitta As String, ByVal lConto As Integer, ByRef nListino As Integer) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      nListino = 0

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lConto, nListino})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        nListino = NTSCInt(oIn(2))        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return NTSCInt(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT an_clascon, an_listino FROM anagra" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND an_conto = " & lConto
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        Return 0
      Else
        nListino = NTSCInt(dttTmp.Rows(0)!an_listino)
        Return NTSCInt(dttTmp.Rows(0)!an_clascon)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function RitornaPerqtaArticolo(ByVal strDitta As String, ByVal strCodart As String) As Decimal
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT ar_perqta FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return 0 Else Return NTSCDec(dttTmp.Rows(0)!ar_perqta)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function ListinoEsistentePerConto(ByVal strDitta As String, ByVal lConto As Integer, _
    ByVal strDatagg As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 lc_progr FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_conto = " & lConto & _
        " AND lc_datagg <= " & CDataSQL(strDatagg) & _
        " AND lc_datscad >= " & CDataSQL(strDatagg) & _
        " ORDER BY lc_progr"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function ScontiEsistentiPerConto(ByVal strDitta As String, ByVal lConto As Integer, _
    ByVal strDatagg As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 so_conto FROM sconti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND so_conto = " & lConto & _
        " AND so_datagg <= " & CDataSQL(strDatagg) & _
        " AND so_datscad >= " & CDataSQL(strDatagg) & _
        " ORDER BY codditt, so_codart, so_conto, so_clscan, so_clscar, so_datagg, so_codtpro, so_daquant, so_fase"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function ListinoEsistentePerArticolo(ByVal strDitta As String, ByVal lConto As Integer, _
    ByVal strCodart As String, ByVal strDatagg As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strFiltroArticolo As String = ""
    Try
      strSQL = "SELECT ar_gesvar, ar_prevar, ar_codroot, ar_codvar1, ar_codvar2 FROM artico " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "  AND ar_codart = " & CStrSQL(strCodart)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If NTSCStr(dttTmp.Rows(0)!ar_gesvar).ToUpper = "S" Then
        Select Case NTSCStr(dttTmp.Rows(0)!ar_prevar)
          Case "N" : strFiltroArticolo = NTSCStr(dttTmp.Rows(0)!ar_codroot)
          Case "1" : strFiltroArticolo = NTSCStr(dttTmp.Rows(0)!ar_codroot) & NTSCStr(dttTmp.Rows(0)!ar_codvar1)
          Case Else : strFiltroArticolo = strCodart
        End Select
      Else
        strFiltroArticolo = strCodart
      End If

      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 lc_progr FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strFiltroArticolo) & _
        " AND lc_datagg <= " & CDataSQL(strDatagg) & _
        " AND lc_datscad >= " & CDataSQL(strDatagg) & _
        IIf(lConto <> 0, " AND lc_conto = " & lConto, "").ToString & _
        " ORDER BY lc_progr"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function ScontiEsistentiPerArticolo(ByVal strDitta As String, ByVal lConto As Integer, _
    ByVal strCodart As String, ByVal strDatagg As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 so_conto FROM sconti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND so_codart = " & CStrSQL(strCodart) & _
        " AND so_datagg <= " & CDataSQL(strDatagg) & _
        " AND so_datscad >= " & CDataSQL(strDatagg) & _
        IIf(lConto <> 0, " AND so_conto = " & lConto, "").ToString & _
        " ORDER BY codditt, so_codart, so_conto, so_clscan, so_clscar, so_datagg, so_codtpro, so_daquant, so_fase"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function RiempiTTSTLIDaLISTSES(ByVal strDitta As String, ByVal lSessione As Integer, _
      ByVal lIITtstli As Integer, ByVal lIITTListsar As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strCampiTTSLTI As String = "codditt, instid, sta_progr, sta_codart, sta_scont1, sta_scont2, sta_scont3," & _
                                   " sta_scont4, sta_scont5, sta_scont6, sta_scdaquant, sta_scaquant, sta_scunmis," & _
                                   " sta_prz1, sta_codvalu1, sta_daquant1, sta_aquant1, sta_perqta1, sta_unmis1," & _
                                   " sta_prz2, sta_codvalu2, sta_daquant2, sta_aquant2, sta_perqta2, sta_unmis2," & _
                                   " sta_prz3, sta_codvalu3, sta_daquant3, sta_aquant3, sta_perqta3, sta_unmis3," & _
                                   " sta_prz4, sta_codvalu4, sta_daquant4, sta_aquant4, sta_perqta4, sta_unmis4," & _
                                   " sta_fase, sta_codling, sta_prznet1, sta_prznet2, sta_prznet3, sta_prznet4," & _
                                   " sta_scont1_2, sta_scont2_2, sta_scont3_2, sta_scont4_2, sta_scont5_2, sta_scont6_2," & _
                                   " sta_scdaquant_2, sta_scaquant_2, sta_scunmis_2," & _
                                   " sta_scont1_3, sta_scont2_3, sta_scont3_3, sta_scont4_3, sta_scont5_3, sta_scont6_3," & _
                                   " sta_scdaquant_3, sta_scaquant_3, sta_scunmis_3," & _
                                   " sta_scont1_4, sta_scont2_4, sta_scont3_4, sta_scont4_4, sta_scont5_4, sta_scont6_4," & _
                                   " sta_scdaquant_4, sta_scaquant_4, sta_scunmis_4," & _
                                   " sta_prz5, sta_codvalu5, sta_daquant5, sta_aquant5, sta_perqta5, sta_unmis5," & _
                                   " sta_prznet5, sta_conto"
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM LISTSES" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ls_opnome = " & CStrSQL(oApp.User.Nome) & _
        " AND ls_sessione = " & lSessione
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dttTmp.Rows.Count - 1)
        With dttTmp.Rows(i)
          strSQL = "INSERT INTO TTSTLI (" & strCampiTTSLTI & ")" & _
            " VALUES (" & CStrSQL(!codditt) & ", " & lIITtstli & ", " & (i + 1) & ", " & CStrSQL(NTSCStr(!ls_codart)) & ", " & _
            CDblSQL(NTSCDec(!ls_scont1)) & ", " & CDblSQL(NTSCDec(!ls_scont2)) & ", " & _
            CDblSQL(NTSCDec(!ls_scont3)) & ", " & CDblSQL(NTSCDec(!ls_scont4)) & ", " & _
            CDblSQL(NTSCDec(!ls_scont5)) & ", " & CDblSQL(NTSCDec(!ls_scont6)) & ", " & _
            CDblSQL(NTSCDec(!ls_scdaquant)) & ", " & CDblSQL(NTSCDec(!ls_scaquant)) & ", ' ', " & _
            CDblSQL(NTSCDec(!ls_prz1)) & ", " & NTSCInt(!ls_codvalu1) & ", " & _
            CDblSQL(NTSCDec(!ls_daquant1)) & ", " & CDblSQL(NTSCDec(!ls_aquant1)) & ", 1, " & _
            CStrSQL(NTSCStr(!ls_unmis1)) & ", " & _
            CDblSQL(NTSCDec(!ls_prz2)) & ", " & NTSCInt(!ls_codvalu2) & ", " & _
            CDblSQL(NTSCDec(!ls_daquant2)) & ", " & CDblSQL(NTSCDec(!ls_aquant2)) & ", 1, " & _
            CStrSQL(NTSCStr(!ls_unmis2)) & ", " & _
            CDblSQL(NTSCDec(!ls_prz3)) & ", " & NTSCInt(!ls_codvalu3) & ", " & _
            CDblSQL(NTSCDec(!ls_daquant3)) & ", " & CDblSQL(NTSCDec(!ls_aquant3)) & ", 1, " & _
            CStrSQL(NTSCStr(!ls_unmis3)) & ", " & _
            CDblSQL(NTSCDec(!ls_prz4)) & ", " & NTSCInt(!ls_codvalu4) & ", " & _
            CDblSQL(NTSCDec(!ls_daquant4)) & ", " & CDblSQL(NTSCDec(!ls_aquant4)) & ", 1, " & _
            CStrSQL(NTSCStr(!ls_unmis4)) & ", " & _
            NTSCInt(!ls_fase) & ", " & NTSCInt(!ls_codling) & ", " & _
            CStrSQL(NTSCStr(!ls_prznet1)) & ", " & CStrSQL(NTSCStr(!ls_prznet2)) & ", " & _
            CStrSQL(NTSCStr(!ls_prznet3)) & ", " & CStrSQL(NTSCStr(!ls_prznet4)) & ", " & _
            CDblSQL(NTSCDec(!ls_scont1_2)) & ", " & CDblSQL(NTSCDec(!ls_scont2_2)) & ", " & _
            CDblSQL(NTSCDec(!ls_scont3_2)) & ", " & CDblSQL(NTSCDec(!ls_scont4_2)) & ", " & _
            CDblSQL(NTSCDec(!ls_scont5_2)) & ", " & CDblSQL(NTSCDec(!ls_scont6_2)) & ", " & _
            CDblSQL(NTSCDec(!ls_scdaquant_2)) & ", " & CDblSQL(NTSCDec(!ls_scaquant_2)) & ", ' ', " & _
            CDblSQL(NTSCDec(!ls_scont1_3)) & ", " & CDblSQL(NTSCDec(!ls_scont2_3)) & ", " & _
            CDblSQL(NTSCDec(!ls_scont3_3)) & ", " & CDblSQL(NTSCDec(!ls_scont4_3)) & ", " & _
            CDblSQL(NTSCDec(!ls_scont5_3)) & ", " & CDblSQL(NTSCDec(!ls_scont6_3)) & ", " & _
            CDblSQL(NTSCDec(!ls_scdaquant_3)) & ", " & CDblSQL(NTSCDec(!ls_scaquant_3)) & ", ' ', " & _
            CDblSQL(NTSCDec(!ls_scont1_4)) & ", " & CDblSQL(NTSCDec(!ls_scont2_4)) & ", " & _
            CDblSQL(NTSCDec(!ls_scont3_4)) & ", " & CDblSQL(NTSCDec(!ls_scont4_4)) & ", " & _
            CDblSQL(NTSCDec(!ls_scont5_4)) & ", " & CDblSQL(NTSCDec(!ls_scont6_4)) & ", " & _
            CDblSQL(NTSCDec(!ls_scdaquant_4)) & ", " & CDblSQL(NTSCDec(!ls_scaquant_4)) & ", ' ', " & _
            CDblSQL(NTSCDec(!ls_prz5)) & ", " & NTSCInt(!ls_codvalu5) & ", " & _
            CDblSQL(NTSCDec(!ls_daquant5)) & ", " & CDblSQL(NTSCDec(!ls_aquant5)) & ", 1, " & _
            CStrSQL(NTSCStr(!ls_unmis5)) & ", " & CStrSQL(NTSCStr(!ls_prznet5)) & ", " & NTSCInt(!ls_conto) & ")"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function InsertTTLISTSAR(ByVal strDitta As String, ByVal dtrT As DataRow, _
      ByVal lIITTListsar As Integer) As Boolean
    Dim lProgr As Integer = 1
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT MAX(lsa_riga) AS Riga FROM TTLISTSAR" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTListsar
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        If Not dttTmp.Rows(0)!Riga.Equals(DBNull.Value) Then lProgr = NTSCInt(dttTmp.Rows(0)!Riga) + 1
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO TTLISTSAR (codditt, instid, lsa_codart, lsa_note, lsa_riga, lsa_flag, lsa_descr," & _
        " lsa_desint, lsa_unmis, lsa_fase, lsa_desfase)" & _
        " SELECT codditt, " & lIITTListsar & ", ar_codart, SUBSTRING(ar_note, 1, 50), " & lProgr & "," & _
        " 'N', ar_descr, ar_desint, ar_unmis, " & NTSCInt(dtrT!Fase) & ", NULL " & _
        " FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(NTSCStr(dtrT!ar_codart))
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

#Region "Lise"
  Public Overridable Function GetDataLise(ByVal strDitta As String, ByVal lSessione As Integer, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim strOldCod As String = ""
    Dim strNewcod As String = ""
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable

    Try
      strSQL = "SELECT listses.*, an_descr1 AS xx_desccli, ar_descr AS xx_descart, ar_desint AS xx_desint, af_descr AS xx_fase, " & _
               " tablavo1.tb_deslavo AS xx_desclavo1, tabvalu1.tb_desvalu AS xx_descvalu1, " & _
               " tablavo2.tb_deslavo AS xx_desclavo2, tabvalu2.tb_desvalu AS xx_descvalu2, " & _
               " tablavo3.tb_deslavo AS xx_desclavo3, tabvalu3.tb_desvalu AS xx_descvalu3, " & _
               " tablavo4.tb_deslavo AS xx_desclavo4, tabvalu4.tb_desvalu AS xx_descvalu4, " & _
               " tablavo5.tb_deslavo AS xx_desclavo5, tabvalu5.tb_desvalu AS xx_descvalu5, " & _
               " tabtpro1.tb_destpro AS xx_descpromo, tabtpro2.tb_destpro AS xx_descpromo_2, " & _
               " tabtpro3.tb_destpro AS xx_descpromo_3, tabtpro4.tb_destpro AS xx_descpromo_4, " & _
               " tabtpro5.tb_destpro AS xx_descpromo1, tabtpro6.tb_destpro AS xx_descpromo2, " & _
               " tabtpro7.tb_destpro AS xx_descpromo4, tabtpro8.tb_destpro AS xx_descpromo3, " & _
               " tabtpro9.tb_destpro AS xx_descpromo5, " & _
               " tabcscl.tb_descscl AS xx_clscan, tabcsar.tb_descsar AS xx_clscar, " & _
               " 'N' AS xx_sync1, 'N' AS xx_sync2, 'N' AS xx_sync3, 'N' AS xx_sync4, 'N' AS xx_sync5, " & _
               " 'N' AS xx_syncsc, 'N' AS xx_syncsc_2, 'N' AS xx_syncsc_3, 'N' AS xx_syncsc_4, " & _
               " ar_codart, ar_gesvar, ar_prevar, ar_codroot, ar_codvar1, ar_codvar2, ar_codvar3, " & _
               " dd_nomdest as xx_coddest" & _
               " FROM listses LEFT JOIN anagra ON listses.codditt = anagra.codditt AND an_conto = ls_conto " & _
               " LEFT JOIN destdiv ON listses.codditt = destdiv.codditt AND ls_conto = dd_conto AND ls_coddest = dd_coddest" & _
               " LEFT JOIN artico ON listses.codditt = artico.codditt AND ar_codart = ls_codart " & _
               " LEFT JOIN artfasi ON listses.codditt = artfasi.codditt AND af_codart = ls_codart AND af_fase = ls_fase " & _
               " LEFT JOIN tabvalu AS tabvalu1 ON tabvalu1.tb_codvalu = ls_codvalu1 " & _
               " LEFT JOIN tabvalu AS tabvalu2 ON tabvalu2.tb_codvalu = ls_codvalu2 " & _
               " LEFT JOIN tabvalu AS tabvalu3 ON tabvalu3.tb_codvalu = ls_codvalu3 " & _
               " LEFT JOIN tabvalu AS tabvalu4 ON tabvalu4.tb_codvalu = ls_codvalu4 " & _
               " LEFT JOIN tabvalu AS tabvalu5 ON tabvalu5.tb_codvalu = ls_codvalu5 " & _
               " LEFT JOIN tablavo AS tablavo1 ON tablavo1.codditt = listses.codditt AND tablavo1.tb_codlavo = ls_codlavo1 " & _
               " LEFT JOIN tablavo AS tablavo2 ON tablavo2.codditt = listses.codditt AND tablavo2.tb_codlavo = ls_codlavo2 " & _
               " LEFT JOIN tablavo AS tablavo3 ON tablavo3.codditt = listses.codditt AND tablavo3.tb_codlavo = ls_codlavo3 " & _
               " LEFT JOIN tablavo AS tablavo4 ON tablavo4.codditt = listses.codditt AND tablavo4.tb_codlavo = ls_codlavo4 " & _
               " LEFT JOIN tablavo AS tablavo5 ON tablavo5.codditt = listses.codditt AND tablavo5.tb_codlavo = ls_codlavo5 " & _
               " LEFT JOIN tabtpro AS tabtpro1 ON tabtpro1.codditt = listses.codditt AND tabtpro1.tb_codtpro = ls_codtpro " & _
               " LEFT JOIN tabtpro AS tabtpro2 ON tabtpro2.codditt = listses.codditt AND tabtpro2.tb_codtpro = ls_codtpro_2 " & _
               " LEFT JOIN tabtpro AS tabtpro3 ON tabtpro3.codditt = listses.codditt AND tabtpro3.tb_codtpro = ls_codtpro_3 " & _
               " LEFT JOIN tabtpro AS tabtpro4 ON tabtpro4.codditt = listses.codditt AND tabtpro4.tb_codtpro = ls_codtpro_4 " & _
               " LEFT JOIN tabtpro AS tabtpro5 ON tabtpro5.codditt = listses.codditt AND tabtpro5.tb_codtpro = ls_codpromo1 " & _
               " LEFT JOIN tabtpro AS tabtpro6 ON tabtpro6.codditt = listses.codditt AND tabtpro6.tb_codtpro = ls_codpromo2 " & _
               " LEFT JOIN tabtpro AS tabtpro7 ON tabtpro7.codditt = listses.codditt AND tabtpro7.tb_codtpro = ls_codpromo3 " & _
               " LEFT JOIN tabtpro AS tabtpro8 ON tabtpro8.codditt = listses.codditt AND tabtpro8.tb_codtpro = ls_codpromo4 " & _
               " LEFT JOIN tabtpro AS tabtpro9 ON tabtpro9.codditt = listses.codditt AND tabtpro9.tb_codtpro = ls_codpromo5 " & _
               " LEFT JOIN tabcscl ON tabcscl.codditt = listses.codditt AND tb_codcscl = ls_clscan " & _
               " LEFT JOIN tabcsar ON tabcsar.codditt = listses.codditt AND tb_codcsar = ls_clscar " & _
               " WHERE listses.codditt = " & CStrSQL(strDitta) & _
               " AND ls_sessione = " & lSessione & _
               " AND ls_opnome = " & CStrSQL(oApp.User.Nome)

      'OCCHIO agli articoli a varianti: devo visualizzare solo gli articoli che effettivamente hanno un listino
      'se articolo a varianti con prezzi comuni devo far vedere sol il padre, diversamete solo la prima variante, diversamente tutti
      'stessa cosa per gli articoli TCO.
      'la stessa cosa vale per gli sconti
      'casi possibili
      'descrizione            articolo        ar_gesvar   ar_prevar
      'articolo normale       MP1             N           N        
      'art a var prz comuni   VARLCOM         S           N        
      'art a var prz 1^ var   VARPR1V         S           1        
      'art a var prz diversi  VARPR2V         S           S
      'art TCO prz comuni     TCO             S           N
      'art TCO prz diversi    TCO             S           S
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTSES")

      'sugli articoli con prezzi diversi solo per 1^ variante, devo far vedere solo un articolo per prima variante
      'tenendo conto che in artico quell'ar_codart non esiste mentre in listini e/o sconti si
      strOldCod = ""
      strNewcod = ""
      For Each dtrT As DataRow In dsOut.Tables("LISTSES").Select("ar_gesvar = 'S'", "ar_codart, ar_codroot DESC")
        Select Case NTSCStr(dtrT!ar_prevar)
          Case "N"
            strNewcod = NTSCStr(dtrT!ar_codroot)
          Case "1"
            strNewcod = NTSCStr(dtrT!ar_codroot) & NTSCStr(dtrT!ar_codvar1)
          Case "S"
            strNewcod = NTSCStr(dtrT!ar_codart)
        End Select
        If strOldCod = strNewcod And strNewcod <> "" Then
          'avevo già trattato lo stesso codroot(o codroot+1^variante) nel record precedente: rimuovo il record doppio
          strSQL = "DELETE FROM listses " & _
                   " WHERE listses.codditt = " & CStrSQL(strDitta) & _
                   " AND ls_sessione = " & lSessione & _
                   " AND ls_opnome = " & CStrSQL(oApp.User.Nome) & _
                   " AND ls_codart = " & CStrSQL(NTSCStr(dtrT!ls_codart))
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          dtrT.Delete()
        Else
          dtrT!ls_codart = strNewcod
          If ValCodiceDb(strNewcod, strDitta, "ARTICO", "S", strTmp) = True Then dtrT!xx_descart = strTmp
          strOldCod = strNewcod
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrT As DataRow In dsOut.Tables("LISTSES").Select("ls_codling <> 0")
        strSQL = "SELECT ax_descr, ax_desint FROM artval" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ax_codart = " & CStrSQL(NTSCStr(dtrT!ls_codart)) & _
          " AND ax_codvalu = " & NTSCInt(dtrT!ls_codling) & _
          " AND ax_descr IS NOT NULL"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          If NTSCStr(dttTmp.Rows(0)!ax_descr).Trim <> "" Then dtrT!xx_descart = NTSCStr(dttTmp.Rows(0)!ax_descr).Trim
          If NTSCStr(dttTmp.Rows(0)!ax_desint).Trim <> "" Then dtrT!xx_desint = NTSCStr(dttTmp.Rows(0)!ax_desint).Trim
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      Next
      '--------------------------------------------------------------------------------------------------------------
      dsOut.Tables("LISTSES").AcceptChanges()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function SaveDataLise(ByRef dsIn As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer
    Dim dtrRow() As DataRow
    Try
      dtrRow = dsIn.Tables("LISTSES").Select("", "", DataViewRowState.Deleted)

      For i = 0 To dtrRow.Length - 1
        Try
          strSQL = "DELETE FROM listses WHERE codditt = " & CStrSQL(dtrRow(i)("codditt", DataRowVersion.Original)) & _
                   " AND ls_opnome = " & CStrSQL(dtrRow(i)("ls_opnome", DataRowVersion.Original)) & _
                   " AND ls_sessione = " & NTSCInt(dtrRow(i)("ls_sessione", DataRowVersion.Original)) & _
                   " AND ls_conto = " & NTSCInt(dtrRow(i)("ls_conto", DataRowVersion.Original)) & _
                   " AND ls_coddest = " & NTSCInt(dtrRow(i)("ls_coddest", DataRowVersion.Original)) & _
                   " AND ls_codart = " & CStrSQL(dtrRow(i)("ar_codart", DataRowVersion.Original)) & _
                   " AND ls_fase = " & NTSCInt(dtrRow(i)("ls_fase", DataRowVersion.Original))
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        Catch ex As Exception
        End Try
      Next

      dtrRow = dsIn.Tables("LISTSES").Select("", "", DataViewRowState.ModifiedCurrent)

      For i = 0 To dtrRow.Length - 1
        strSQL = "UPDATE listses SET " & GetQueryUpdate(dsIn.Tables("LISTSES"), dtrRow(i), "ls_", "ls_codart") & _
                 " WHERE codditt = " & CStrSQL(dtrRow(i)!codditt) & _
                 " AND ls_opnome = " & CStrSQL(dtrRow(i)!ls_opnome) & _
                 " AND ls_sessione = " & NTSCInt(dtrRow(i)!ls_sessione) & _
                 " AND ls_conto = " & NTSCInt(dtrRow(i)!ls_conto) & _
                 " AND ls_coddest = " & NTSCInt(dtrRow(i)!ls_coddest) & _
                 " AND ls_codart = " & CStrSQL(dtrRow(i)!ar_codart) & _
                 " AND ls_fase = " & NTSCInt(dtrRow(i)!ls_fase)

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckSyncListino(ByVal strDitta As String, ByVal lProgr As Integer, ByVal dtUltAgg As Date, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT lc_progr FROM listini WHERE codditt = " & CStrSQL(strDitta) & _
               " AND lc_progr = " & lProgr & " AND lc_ultagg > " & CDataOraSQL(dtUltAgg)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckSyncSconti(ByVal dtrRow As DataRow, ByVal strTag As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim nClscan As Integer = NTSCInt(dtrRow!ls_clscan)
    Try
      With dtrRow
        strSQL = "SELECT so_codart FROM sconti WHERE codditt = " & CStrSQL(!codditt) & _
                 " AND so_codart = " & CStrSQL(!ls_codart) & _
                 " AND so_conto = " & NTSCInt(!ls_conto) & _
                 " AND so_coddest = " & IIf(NTSCInt(!ls_conto) <> 0, NTSCInt(!ls_coddest), 0).ToString & _
                 " AND so_clscan = " & NTSCInt(!ls_clscan) & _
                 " AND so_clscar = " & NTSCInt(!ls_clscar) & _
                 " AND so_datagg = " & CDataOraSQL(NTSCDate(dtrRow("ls_dadatasc" & strTag))) & _
                 " AND so_codtpro = " & NTSCInt(dtrRow("ls_codtpro" & strTag)) & _
                 " AND so_daquant = " & CDblSQL(NTSCDec(dtrRow("ls_scdaquant" & strTag))) & _
                 " AND so_fase = " & NTSCInt(!ls_fase) & _
                 " AND so_ultagg > " & CDataOraSQL(NTSCDate(!ls_dtorsync))
      End With

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------
    End Try
  End Function
#End Region

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
