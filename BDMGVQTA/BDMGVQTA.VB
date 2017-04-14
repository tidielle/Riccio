Imports NTSInformatica.CLN__STD
Imports System.Data
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGVQTA
  Inherits CLD__BASE

  Public Overridable Function Elabora(ByVal strDitta As String, ByVal strQuery As String, _
    ByVal bAr_perqta As Boolean, ByVal dAr_perqta As Decimal, _
    ByVal bAr_gescomm As Boolean, ByVal strAr_gescommIni As String, ByVal strAr_gescommFin As String, _
    ByVal bAr_geslotti As Boolean, ByVal strAr_geslottiIni As String, ByVal strAr_geslottiFin As String, _
    ByVal bAr_gestmatr As Boolean, ByVal strAr_gestmatrIni As String, ByVal strAr_gestmatrFin As String, _
    ByVal bAr_gesubic As Boolean, ByVal strAr_gesubicIni As String, ByVal strAr_gesubicFin As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return Elabora(strDitta, strQuery, bAr_perqta, dAr_perqta, bAr_gescomm, strAr_gescommIni, strAr_gescommFin, _
        bAr_geslotti, strAr_geslottiIni, strAr_geslottiFin, bAr_gestmatr, strAr_gestmatrIni, strAr_gestmatrFin, _
        bAr_gesubic, strAr_gesubicIni, strAr_gesubicFin, "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function Elabora(ByVal strDitta As String, ByVal strQuery As String, _
    ByVal bAr_perqta As Boolean, ByVal dAr_perqta As Decimal, _
    ByVal bAr_gescomm As Boolean, ByVal strAr_gescommIni As String, ByVal strAr_gescommFin As String, _
    ByVal bAr_geslotti As Boolean, ByVal strAr_geslottiIni As String, ByVal strAr_geslottiFin As String, _
    ByVal bAr_gestmatr As Boolean, ByVal strAr_gestmatrIni As String, ByVal strAr_gestmatrFin As String, _
    ByVal bAr_gesubic As Boolean, ByVal strAr_gesubicIni As String, ByVal strAr_gesubicFin As String, _
    ByRef strLog As String) As Boolean
    Dim strMsgOut As String
    Dim bMovord As Boolean = False
    Dim bMovmag As Boolean = False
    Dim bOrdlist As Boolean = False
    Dim bListprod As Boolean = False
    Dim bArtpro As Boolean = False
    Dim bLotcpro As Boolean = False
    Dim bMatricole As Boolean = False
    Dim i As Integer = 0
    Dim strSQL As String = ""
    Dim strSQLTmp As String = ""
    Dim dsArtico As New DataTable
    Dim dttTmp As New DataTable
    Dim dbConn As DbConnection = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Per compatibilita' con funzioni ereditate da rive in versioni precedenti
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strQuery, bAr_perqta, dAr_perqta, bAr_gescomm, _
        strAr_gescommIni, strAr_gescommFin, bAr_geslotti, strAr_geslottiIni, strAr_geslottiFin, _
        bAr_gestmatr, strAr_gestmatrIni, strAr_gestmatrFin, bAr_gesubic, strAr_gesubicIni, strAr_gesubicFin, _
        strLog})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strLog = NTSCStr(oIn(16))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strLog = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiungo la where dei campi con OR e AND rimappati
      '--------------------------------------------------------------------------------------------------------------
      TraduciWhere(strQuery, strSQLTmp)
      '--------------------------------------------------------------------------------------------------------------

      If bAr_gescomm = True Then
        'UNIFORMO CON LE MODIFICHE FATTE SU ANAGRAFICA ARTICOLI 
        strSQL = " SELECT ar_codart, 1 AS xx_aggiorna " & _
                 " FROM artico " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & strSQLTmp & _
                 " AND ar_gescomm = " & CStrSQL(strAr_gescommIni)
        dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        For i = 0 To (dsArtico.Rows.Count - 1)
          strMsgOut = ""
          If Not IsGescommUpdatable(strDitta, NTSCStr(dsArtico.Rows(i)!ar_codart), False, strAr_gescommIni, strAr_gescommFin, strMsgOut) Then
            strLog += IIf(strLog <> "", vbCrLf, "").ToString & oApp.Tr(Me, 130367842275808647, strMsgOut)
            dsArtico.Rows(i)!xx_aggiorna = 0
            dsArtico.AcceptChanges()
          End If
        Next

        For i = 0 To (dsArtico.Rows.Count - 1)
          '----------------------------------------------------------------------------------------------------------
          If NTSCInt(dsArtico.Rows(i)!xx_aggiorna) = 0 Then GoTo Successivo
          '----------------------------------------------------------------------------------------------------------
          strSQL = " UPDATE artico" & _
                   " SET ar_gescomm = " & CStrSQL(strAr_gescommFin) & "," & _
                   " ar_ultagg = " & CDataSQL(Date.Now) & "," & _
                   " ar_oragg = " & CDblSQL(Format(Date.Now, "hhmm")) & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND ar_codart = " & CStrSQL(dsArtico.Rows(i)!ar_codart.ToString)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          '----------------------------------------------------------------------------------------------------------
Successivo:
          '----------------------------------------------------------------------------------------------------------
        Next
        GoTo esci
      End If

      '--------------------------------------------------------------------------------------------------------------
      '--- Apre il database
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre la transazione
      '--------------------------------------------------------------------------------------------------------------
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      '--- Modifica del Moltiplicatore Quantità/Prezzo
      '--------------------------------------------------------------------------------------------------------------
      If bAr_perqta = True Then
        '------------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE artprox" & _
          " SET apx_ultcos = ROUND((apx_ultcos / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " apx_peucos = ROUND((apx_peucos / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " apx_ultpre = ROUND((apx_ultpre / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")" & _
          " FROM artprox INNER JOIN artico ON artprox.codditt = artico.codditt AND artprox.apx_codart = artico.ar_codart" & _
          " WHERE artprox.codditt = " & CStrSQL(strDitta) & strSQLTmp
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE artdefx" & _
          " SET adx_ultcos = ROUND((adx_ultcos / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " adx_peucos = ROUND((adx_peucos / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " adx_ultpre = ROUND((adx_ultpre / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " adx_lultcos = ROUND((adx_lultcos / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " adx_lpeucos = ROUND((adx_lpeucos / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " adx_lprezzo = ROUND((adx_lprezzo / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")" & _
          " FROM artdefx INNER JOIN artico ON artdefx.codditt = artico.codditt AND artdefx.adx_codart = artico.ar_codart" & _
          " WHERE artdefx.codditt = " & CStrSQL(strDitta) & strSQLTmp
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE movmag" & _
          " SET mm_prezzo = ROUND((mm_prezzo / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mm_preziva = ROUND((mm_preziva / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mm_prezvalc = ROUND((mm_prezvalc / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(1) & ")," & _
          " mm_prelist = ROUND((mm_prelist / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mm_costorel = ROUND((mm_costorel / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mm_perqta = " & CDblSQL(dAr_perqta) & _
          " FROM  movmag INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart" & _
          " WHERE movmag.codditt = " & CStrSQL(strDitta) & strSQLTmp
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE movprb" & _
          " SET mm_prezzo = ROUND((mm_prezzo / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mm_preziva = ROUND((mm_preziva / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mm_prezvalc = ROUND((mm_prezvalc / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(1) & ")," & _
          " mm_prelist = ROUND((mm_prelist / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mm_costorel = ROUND((mm_costorel / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mm_perqta = " & CDblSQL(dAr_perqta) & _
          " FROM movprb INNER JOIN artico ON movprb.codditt = artico.codditt AND movprb.mm_codart = artico.ar_codart" & _
          " WHERE movprb.codditt = " & CStrSQL(strDitta) & strSQLTmp
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE movord" & _
          " SET mo_prezzo = ROUND((mo_prezzo / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mo_preziva = ROUND((mo_preziva / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mo_prezvalc = ROUND((mo_prezvalc / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(1) & ")," & _
          " mo_prelist = ROUND((mo_prelist / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mo_perqta = " & CDblSQL(dAr_perqta) & _
          " FROM movord INNER JOIN artico ON movord.codditt = artico.codditt AND movord.mo_codart = artico.ar_codart" & _
          " WHERE movord.codditt = " & CStrSQL(strDitta) & strSQLTmp
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE ordlist" & _
          " SET ol_prezzo = ROUND((ol_prezzo / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " ol_prezvalc = ROUND((ol_prezvalc / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(1) & ")," & _
          " ol_prelist = ROUND((ol_prelist / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " ol_perqta = " & CDblSQL(dAr_perqta) & _
          " FROM ordlist INNER JOIN artico ON ordlist.codditt = artico.codditt AND ordlist.ol_codart = artico.ar_codart" & _
          " WHERE ordlist.codditt = " & CStrSQL(strDitta) & strSQLTmp
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE listini" & _
          " SET lc_prezzo = ROUND((lc_prezzo / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " lc_perqta = " & CDblSQL(dAr_perqta) & _
          " FROM listini INNER JOIN artico ON listini.codditt = artico.codditt AND listini.lc_codart = artico.ar_codart" & _
          " WHERE listini.codditt = " & CStrSQL(strDitta) & strSQLTmp & _
          " AND lc_codvalu = 0"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE artico SET ar_perqta = " & CDblSQL(dAr_perqta) & "," & _
          " ar_ultagg = " & CDataSQL(Date.Now) & "," & _
          " ar_oragg = " & CDblSQL(Format(Date.Now, "hhmm")) & _
          " WHERE codditt = " & CStrSQL(strDitta) & strSQLTmp
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
        strSQL = "UPDATE movcona" & _
          " SET mma_prezzo = ROUND((mma_prezzo / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(0) & ")," & _
          " mma_prezval = ROUND((mma_prezval / ar_perqta) * " & CDblSQL(dAr_perqta) & ", " & TrovaNdecSuPrzUn(1) & ")," & _
          " mma_perqta = " & CDblSQL(dAr_perqta) & _
          " FROM movcona INNER JOIN artico ON movcona.codditt = artico.codditt AND movcona.mma_codart = artico.ar_codart" & _
          " WHERE movcona.codditt = " & CStrSQL(strDitta) & strSQLTmp
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        '------------------------------------------------------------------------------------------------------------
      Else
        '------------------------------------------------------------------------------------------------------------
        strSQL = "SELECT DISTINCT ar_codart, ar_gestmatr, 1 AS xx_aggiorna FROM artico " & _
          " WHERE codditt = " & CStrSQL(strDitta) & strSQLTmp & _
          IIf(bAr_gescomm = True, " AND ar_gescomm = " & CStrSQL(strAr_gescommIni), "").ToString & _
          IIf(bAr_geslotti = True, " AND ar_geslotti = " & CStrSQL(strAr_geslottiIni), "").ToString & _
          IIf(bAr_gestmatr = True, " AND ar_gestmatr = " & CStrSQL(strAr_gestmatrIni), "").ToString & _
          IIf(bAr_gesubic = True, " AND ar_gesubic = " & CStrSQL(strAr_gesubicIni), "").ToString
        dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        '------------------------------------------------------------------------------------------------------------
        For i = 0 To (dsArtico.Rows.Count - 1)
          '----------------------------------------------------------------------------------------------------------
          bMovord = False
          bMovmag = False
          bOrdlist = False
          bListprod = False
          bArtpro = False
          bLotcpro = False
          bMatricole = False
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT TOP 1 mo_codart FROM movord" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND mo_codart = " & CStrSQL(NTSCStr(dsArtico.Rows(i)!ar_codart))
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then bMovord = True
          dttTmp.Clear()
          dttTmp.Dispose()
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT TOP 1 mm_codart FROM movmag" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND mm_codart = " & CStrSQL(NTSCStr(dsArtico.Rows(i)!ar_codart))
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then bMovmag = True
          dttTmp.Clear()
          dttTmp.Dispose()
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT TOP 1 ol_codart FROM ordlist" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ol_codart = " & CStrSQL(NTSCStr(dsArtico.Rows(i)!ar_codart))
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then bOrdlist = True
          dttTmp.Clear()
          dttTmp.Dispose()
          '----------------------------------------------------------------------------------------------------------
          strSQL = "SELECT TOP 1 lp_codart FROM listprod" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND lp_codart = " & CStrSQL(NTSCStr(dsArtico.Rows(i)!ar_codart))
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then bListprod = True
          dttTmp.Clear()
          dttTmp.Dispose()
          '----------------------------------------------------------------------------------------------------------
          '--- Questo test lo fa solo se l'articolo NON è gestito a 'Matricole'
          '----------------------------------------------------------------------------------------------------------
          If NTSCStr(dsArtico.Rows(i)!ar_gestmatr) = "N" Then
            strSQL = "SELECT TOP 1 ap_codart FROM artpro" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND ap_codart = " & CStrSQL(NTSCStr(dsArtico.Rows(i)!ar_codart))
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            If dttTmp.Rows.Count > 0 Then bArtpro = True
            dttTmp.Clear()
            dttTmp.Dispose()
          End If
          '----------------------------------------------------------------------------------------------------------
          '--- Questo test lo fa solo se l'articolo NON è gestito a 'Matricole'
          '----------------------------------------------------------------------------------------------------------
          If NTSCStr(dsArtico.Rows(i)!ar_gestmatr) = "N" Then
            strSQL = "SELECT TOP 1 lp_codart FROM lotcpro" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lp_codart = " & CStrSQL(NTSCStr(dsArtico.Rows(i)!ar_codart))
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            If dttTmp.Rows.Count > 0 Then bLotcpro = True
            dttTmp.Clear()
            dttTmp.Dispose()
          End If
          '----------------------------------------------------------------------------------------------------------
          '--- Questo test lo fa solo se l'articolo è gestito a 'Matricole'
          '----------------------------------------------------------------------------------------------------------
          If NTSCStr(dsArtico.Rows(i)!ar_gestmatr) = "S" Then
            strSQL = "SELECT TOP 1 rl_codart FROM nnmatrics" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND rl_codart = " & CStrSQL(NTSCStr(dsArtico.Rows(i)!ar_codart))
            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            If dttTmp.Rows.Count > 0 Then bMatricole = True
            dttTmp.Clear()
            dttTmp.Dispose()
          End If
          '----------------------------------------------------------------------------------------------------------
          If (bMovord = True) Or (bMovmag = True) Or (bOrdlist = True) Or (bListprod = True) Or _
             (bArtpro = True) Or (bLotcpro = True) Or (bMatricole = True) Then
            strLog += IIf(strLog <> "", vbCrLf, "").ToString & _
              oApp.Tr(Me, 130367842275808646, _
              " . Articolo '|" & NTSCStr(dsArtico.Rows(i)!ar_codart) & "|' non aggiornato perché già utilizzato in:")
            If bMovord = True Then strLog += vbCrLf & oApp.Tr(Me, 130368398567069047, "   --> Ordini/Impegni")
            If bMovmag = True Then strLog += vbCrLf & oApp.Tr(Me, 130368399035719855, "   --> Documenti di magazzino")
            If bOrdlist = True Then strLog += vbCrLf & oApp.Tr(Me, 130368399350312013, "   --> Proposte d'ordine")
            If bListprod = True Then strLog += vbCrLf & oApp.Tr(Me, 130368399613994013, "   --> Liste di produzione")
            If bArtpro = True Then strLog += vbCrLf & oApp.Tr(Me, 130368399861103111, "   --> Progressivi di magazzino (ARTPRO)")
            If bLotcpro = True Then strLog += vbCrLf & oApp.Tr(Me, 130368400063903867, "   --> Progressivi di magazzino (LOTCPRO)")
            If bMatricole = True Then strLog += vbCrLf & oApp.Tr(Me, 130711493734649278, "   --> Anagrafica Matricole")
            dsArtico.Rows(i)!xx_aggiorna = 0
            dsArtico.AcceptChanges()
          End If
          '----------------------------------------------------------------------------------------------------------
        Next
        '------------------------------------------------------------------------------------------------------------
        For i = 0 To (dsArtico.Rows.Count - 1)
          '----------------------------------------------------------------------------------------------------------
          If NTSCInt(dsArtico.Rows(i)!xx_aggiorna) = 0 Then GoTo ArticoloSuccessivo
          '----------------------------------------------------------------------------------------------------------
          strSQL = "UPDATE artico"
          If bAr_gescomm = True Then
            strSQL += " SET ar_gescomm = " & CStrSQL(strAr_gescommFin) & ","
          End If
          If bAr_geslotti = True Then
            strSQL += " SET ar_geslotti = " & CStrSQL(strAr_geslottiFin) & ","
          End If
          If bAr_gestmatr = True Then
            strSQL += " SET ar_gestmatr = " & CStrSQL(strAr_gestmatrFin) & ","
          End If
          If bAr_gesubic = True Then
            strSQL += " SET ar_gesubic = " & CStrSQL(strAr_gesubicFin) & ","
          End If
          strSQL += " ar_ultagg = " & CDataSQL(Date.Now) & "," & _
            " ar_oragg = " & CDblSQL(Format(Date.Now, "hhmm")) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ar_codart = " & CStrSQL(dsArtico.Rows(i)!ar_codart.ToString)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          '----------------------------------------------------------------------------------------------------------
ArticoloSuccessivo:
          '----------------------------------------------------------------------------------------------------------
        Next
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiude la transazione
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiude il database
      '--------------------------------------------------------------------------------------------------------------
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
Esci:
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      '--- Se si è in transazione, questa viene annullata
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    Finally
      dsArtico.Clear() : dsArtico.Dispose()
      dttTmp.Clear() : dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function GetArtico(ByVal strDitta As String, ByVal strQuery As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------------------------------------------------------------------------
      strSQL = "SELECT Count(ar_codart) As Records FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      '----------------------------------------------------------------------------------
      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strQuery, strSQL)

      '----------------------------------------------------------------------------------
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '----------------------------------------------------------------------------------
    End Try
  End Function

End Class