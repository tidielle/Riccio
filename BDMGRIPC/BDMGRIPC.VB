Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGRIPC
  Inherits CLD__BASE

  Public Overridable Function AggiornaListino(ByVal strDitta As String, _
    ByVal strCodart As String, ByVal nCodlavo As Integer, ByVal lConto As Integer, _
    ByVal nCodvalu As Integer, ByVal nCodtpro As Integer, ByVal strTipo As String, _
    ByVal nListino As Integer, ByVal strDatagg As String, ByVal dDaQuant As Decimal, _
    ByVal strUnmis As String) As Boolean
    Dim strSQL As String = ""

    Try
      '----------------------------------------------------------------------------------
      strSQL = "UPDATE listini" & _
        " SET lc_datscad = " & CDataSQL(DateAdd(DateInterval.Day, -1, NTSCDate(strDatagg))) & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codlavo = " & nCodlavo & _
        " AND lc_conto = " & lConto & _
        " AND lc_codvalu = " & nCodvalu & _
        " AND lc_codtpro = " & nCodtpro & _
        " AND lc_tipo = " & CStrSQL(strTipo) & _
        " AND lc_listino = " & nListino & _
        " AND lc_datagg < " & CDataSQL(strDatagg) & _
        " AND lc_datscad >= " & CDataSQL(strDatagg) & _
        " AND lc_daquant <= " & CDblSQL(dDaQuant) & _
        " AND lc_aquant >= " & CDblSQL(dDaQuant)
      If strUnmis.Trim = "" Then
        strSQL += " AND (lc_unmis = ' ' Or lc_unmis = " & CStrSQL(strUnmis) & ")"
      Else
        strSQL += " AND lc_unmis = " & CStrSQL(strUnmis)
      End If
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CancellaListino(ByVal strDitta As String, _
      ByVal strCodart As String, ByVal nCodlavo As Integer, ByVal lConto As Integer, _
      ByVal nCodvalu As Integer, ByVal nCodtpro As Integer, ByVal strTipo As String, _
      ByVal nListino As Integer, ByVal strDatagg As String, ByVal dDaQuant As Decimal, _
      ByVal strUnmis As String) As Boolean
    Dim strSQL As String = ""

    Try
      '----------------------------------------------------------------------------------
      strSQL = "DELETE FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codlavo = " & nCodlavo & _
        " AND lc_conto = " & lConto & _
        " AND lc_codvalu = " & nCodvalu & _
        " AND lc_codtpro = " & nCodtpro & _
        " AND lc_tipo = " & CStrSQL(strTipo) & _
        " AND lc_listino = " & nListino & _
        " AND lc_datagg = " & CDataSQL(strDatagg) & _
        " AND lc_daquant <= " & CDblSQL(dDaQuant) & _
        " AND lc_aquant >= " & CDblSQL(dDaQuant)
      If strUnmis.Trim = "" Then
        strSQL += " AND (lc_unmis = ' ' Or lc_unmis = " & CStrSQL(strUnmis) & ")"
      Else
        strSQL += " AND lc_unmis = " & CStrSQL(strUnmis)
      End If
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function DeterminaQuantita(ByVal strDitta As String, _
    ByVal nCarscar As Integer, ByVal strDaData As String, ByVal strAData As String, _
    ByVal strWhereTipork As String, ByVal strCodart As String, ByVal strUnmis As String, _
    ByVal dConver As Decimal, ByVal bSariaf As Boolean, ByRef dQuant As Decimal) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '----------------------------------------------------------------------------------
      dQuant = 0
      '----------------------------------------------------------------------------------
      'nCarscar = -1 = vendite, 1 = carichi da prod
      strSQL = "SELECT Sum(mm_quant) As Quant" & _
        " FROM ((movmag INNER JOIN keymag ON keymag.codditt = movmag.codditt AND keymag.km_tipork = movmag.mm_tipork AND keymag.km_anno = movmag.mm_anno AND keymag.km_serie = movmag.mm_serie AND keymag.km_numdoc = movmag.mm_numdoc AND keymag.km_riga = movmag.mm_riga)" & _
        " INNER JOIN testmag ON movmag.codditt = testmag.codditt AND movmag.mm_tipork = testmag.tm_tipork AND movmag.mm_anno = testmag.tm_anno AND movmag.mm_serie = testmag.tm_serie AND movmag.mm_numdoc = testmag.tm_numdoc)" & _
        " INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart" & _
        " INNER JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum " & _
        " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
        " AND km_carscar = " & nCarscar & _
        " AND " & IIf(nCarscar = -1, "tb_scacli = 1", " tb_carpro = 1 AND km_tipork = 'T'").ToString & _
        " AND tm_datdoc BETWEEN " & CDataSQL(strDaData) & " AND " & CDataSQL(strAData) & _
        " AND mm_codart = " & CStrSQL(strCodart)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------
      If dttTmp.Rows.Count <> 0 Then
        If Not dttTmp.Rows(0)!Quant.Equals(DBNull.Value) Then
          dQuant = NTSCDec(dttTmp.Rows(0)!Quant.ToString)
          If ((UCase(Left(strUnmis, 2)) = "MT") Or (UCase(Left(strUnmis, 2)) = "ML")) And _
             (bSariaf = True) Then
            dQuant = (NTSCDec(dttTmp.Rows(0)!Quant.ToString) * dConver)
          End If
        End If
      End If
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function DeterminaQtaValoreCarico(ByVal strDitta As String, _
        ByVal strDaData As String, ByVal strAData As String, _
        ByVal strCodart As String, ByRef dQuant As Decimal, ByRef dValore As Decimal) As DataTable
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT SUM(mm_valore) AS xx_valore, SUM(mm_quant) AS xx_quant" & _
                " FROM movmag" & _
                " INNER JOIN testmag ON movmag.codditt = testmag.codditt" & _
                " AND movmag.mm_tipork = testmag.tm_tipork AND movmag.mm_anno = testmag.tm_anno" & _
                " AND movmag.mm_serie = testmag.tm_serie AND movmag.mm_numdoc = testmag.tm_numdoc" & _
                " INNER JOIN keymag ON keymag.codditt = movmag.codditt AND keymag.km_tipork = movmag.mm_tipork" & _
                " AND keymag.km_anno = movmag.mm_anno AND keymag.km_serie = movmag.mm_serie" & _
                " AND keymag.km_numdoc = movmag.mm_numdoc AND keymag.km_riga = movmag.mm_riga" & _
                " INNER JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum " & _
                " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                " AND tm_datdoc BETWEEN " & CDataSQL(strDaData) & " AND " & CDataSQL(strAData) & _
                " AND mm_codart = " & CStrSQL(strCodart) & _
                " AND km_carscar = 1 AND tb_scacli = 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp IsNot Nothing AndAlso dttTmp.Rows.Count > 0 Then
        dQuant = NTSCDec(dttTmp.Rows(0)!xx_quant)
        dValore = NTSCDec(dttTmp.Rows(0)!xx_valore)
      End If
      Return dttTmp
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ICInevasi(ByVal strDitta As String, _
      ByVal strTipork As String, ByVal strCodart As String, ByVal strFlevas As String, _
      ByVal strDaData As String, ByVal strAData As String, _
      ByVal strUnmis As String, ByVal dConver As Decimal, ByVal bSariaf As Boolean, _
      ByRef dQuant As Decimal) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '----------------------------------------------------------------------------------
      dQuant = 0
      '----------------------------------------------------------------------------------
      strSQL = "SELECT (Sum(mo_quant) - Sum(mo_quaeva)) As Quant FROM movord" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mo_codart = " & CStrSQL(strCodart) & _
        " AND mo_datcons BETWEEN " & CDataSQL(strDaData) & " AND " & CDataSQL(strAData) & _
        " AND mo_tipork = " & CStrSQL(strTipork) & _
        " AND mo_flevas = " & CStrSQL(strFlevas)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------
      If dttTmp.Rows.Count <> 0 Then
        If Not dttTmp.Rows(0)!Quant.Equals(DBNull.Value) Then
          dQuant = NTSCDec(dttTmp.Rows(0)!Quant.ToString)
          If ((UCase(Left(strUnmis, 2)) = "MT") Or (UCase(Left(strUnmis, 2)) = "ML")) And _
             (bSariaf = True) Then
            dQuant = (NTSCDec(dttTmp.Rows(0)!Quant.ToString) * dConver)
          End If
        End If
      End If
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function InserisciListino(ByVal strDitta As String, _
    ByVal strCodart As String, ByVal nCodlavo As Integer, ByVal lConto As Integer, _
    ByVal nCodvalu As Integer, ByVal nCodtpro As Integer, ByVal nListino As Integer, _
    ByVal strDatagg As String, ByVal strTipo As String, ByVal dPrezzo As Decimal, _
    ByVal strDatscad As String, ByVal dDaQuant As Decimal, ByVal dAQuant As Decimal, _
    ByVal dPerqta As Decimal, ByVal strUnmis As String, ByVal strNote As String, _
    ByVal strNetto As String, ByVal nFase As Integer, ByVal strUltagg As String, _
    ByVal strCodcas As String) As Boolean
    Dim strSQL As String = ""

    Try
      '----------------------------------------------------------------------------------
      strSQL = "INSERT INTO listini (codditt, lc_codart, lc_codlavo, lc_conto," & _
        " lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_tipo, lc_prezzo, lc_datscad," & _
        " lc_daquant, lc_aquant, lc_perqta, lc_unmis, lc_note, lc_netto, lc_fase," & _
        " lc_ultagg, lc_codcas)" & _
        " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & _
        nCodlavo & ", " & lConto & ", " & nCodvalu & ", " & nCodtpro & ", " & _
        nListino & ", " & CDataSQL(strDatagg) & ", " & CStrSQL(strTipo) & ", " & _
        CDblSQL(dPrezzo) & ", " & CDataSQL(strDatscad) & ", " & CDblSQL(dDaQuant) & ", " & _
        CDblSQL(dAQuant) & ", " & CDblSQL(dPerqta) & ", " & CStrSQL(strUnmis) & ", " & _
        CStrSQL(strNote) & ", " & CStrSQL(strNetto) & ", " & nFase & ", " & _
        CDataSQL(strUltagg) & ", " & CStrSQL(strCodcas) & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function QuantitaABudget(ByVal strDitta As String, _
    ByVal strTipork As String, ByVal strCodart As String, _
    ByVal strDaData As String, ByVal strAData As String, _
    ByVal strUnmis As String, ByVal dConver As Decimal, ByVal bSariaf As Boolean, _
    ByRef dQuant As Decimal) As Boolean
    Dim nCont As Integer = 0
    Dim nMese As Integer = 0
    Dim nAnno As Integer = 0
    Dim nI As Integer = 0
    Dim strSQL As String = ""
    Dim dtCurData As Date
    Dim dttTmp As New DataTable

    Try
      '----------------------------------------------------------------------------------
      dQuant = 0
      '----------------------------------------------------------------------------------
      '--- Prendo sempre il primo giorno del mese della data iniziale
      '--- e l'ultimo giorno di quella finale per determinare le quantità a budget
      '----------------------------------------------------------------------------------
      nCont = NTSCInt(DateDiff(DateInterval.Month, NTSCDate(strDaData), NTSCDate(strAData)))
      nMese = Month(NTSCDate(strDaData))
      nAnno = Year(NTSCDate(strDaData))
      dtCurData = NTSCDate(IntSetDate("01/" & Right("00" & nMese.ToString, 2) & "/" & nAnno.ToString))
      '----------------------------------------------------------------------------------
      For nI = 1 To (nCont + 1)
        '--------------------------------------------------------------------------------
        strSQL = "SELECT Sum(bvd_qtarev) As Qtabud" & _
          " FROM ((budven INNER JOIN budvend ON budven.codditt = budvend.codditt AND budven.bv_tipork = budvend.bvd_tipork AND budven.bv_anno = budvend.bvd_anno AND budven.bv_mese = budvend.bvd_mese AND budven.bv_settim = budvend.bvd_settim AND budven.bv_magaz = budvend.bvd_magaz AND budven.bv_codage = budvend.bvd_codage AND budven.bv_codcfam = budvend.bvd_codcfam)" & _
          " INNER JOIN artico ON budvend.codditt = artico.codditt AND budvend.bvd_codart = artico.ar_codart)" & _
          " WHERE budven.codditt = " & CStrSQL(strDitta) & _
          " AND bv_tipork = " & CStrSQL(strTipork) & _
          " AND bv_anno = " & nAnno & _
          " AND bv_mese = " & nMese & _
          " AND bvd_codart = " & CStrSQL(strCodart)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        '--------------------------------------------------------------------------------
        If dttTmp.Rows.Count <> 0 Then
          If Not dttTmp.Rows(0)!Qtabud.Equals(DBNull.Value) Then
            If ((UCase(Left(strUnmis, 2)) = "MT") Or (UCase(Left(strUnmis, 2)) = "ML")) And _
               (bSariaf = True) Then
              dQuant = (dQuant + (NTSCDec(dttTmp.Rows(0)!Qtabud.ToString) * dConver))
            Else
              dQuant = (dQuant + NTSCDec(dttTmp.Rows(0)!Qtabud.ToString))
            End If
          End If
        End If
        '--------------------------------------------------------------------------------
        dttTmp.Clear()
        dttTmp.Dispose()
        '--------------------------------------------------------------------------------
        '--- Devo fare così per considerare anche budget a cavallo di anno
        '--------------------------------------------------------------------------------
        dtCurData = DateAdd(DateInterval.Month, 1, dtCurData)
        nMese = Month(dtCurData)
        nAnno = Year(dtCurData)
        '--------------------------------------------------------------------------------
      Next
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function QuantitaABudgetAvanzamento(ByVal strDitta As String, _
        ByVal strTipork As String, ByVal strCodart As String, _
        ByVal strDaData As String, ByVal strAData As String, _
        ByVal strUnmis As String, ByVal dConver As Decimal, _
        ByVal bSariaf As Boolean, ByRef dQuant As Decimal) As Boolean
    Dim nCont As Integer = 0
    Dim nMese As Integer = 0
    Dim nAnno As Integer = 0
    Dim nI As Integer = 0
    Dim strSQL As String = ""
    Dim dtCurData As Date
    Dim dttTmp As New DataTable

    Try
      '----------------------------------------------------------------------------------
      dQuant = 0
      '----------------------------------------------------------------------------------
      '--- Considero la quantità a budget dal 1° giorno del mese corrente se il
      '--- giorno di elaborazione è inferiore al 10, altrimenti il 1° giorno
      '--- del mese succesivo, fino alla data finale di elaborazione
      '----------------------------------------------------------------------------------
      dtCurData = NTSCDate(strDaData)
      If Day(dtCurData) > 10 Then dtCurData = DateAdd(DateInterval.Month, 1, dtCurData)
      nMese = Month(NTSCDate(dtCurData))
      nAnno = Year(dtCurData)
      nCont = NTSCInt(DateDiff(DateInterval.Month, NTSCDate(strDaData), NTSCDate(strAData)))
      dtCurData = NTSCDate(IntSetDate("01/" & Right("00" & nMese.ToString, 2) & "/" & nAnno.ToString))
      '----------------------------------------------------------------------------------
      For nI = 1 To (nCont + 1)
        '--------------------------------------------------------------------------------
        strSQL = "SELECT Sum(bvd_qtarev) As Qtabud" & _
          " FROM ((budven INNER JOIN budvend ON budven.codditt = budvend.codditt AND budven.bv_tipork = budvend.bvd_tipork AND budven.bv_anno = budvend.bvd_anno AND budven.bv_mese = budvend.bvd_mese AND budven.bv_settim = budvend.bvd_settim AND budven.bv_magaz = budvend.bvd_magaz AND budven.bv_codage = budvend.bvd_codage AND budven.bv_codcfam = budvend.bvd_codcfam)" & _
          " INNER JOIN artico ON budvend.codditt = artico.codditt AND budvend.bvd_codart = artico.ar_codart)" & _
          " WHERE budven.codditt = " & CStrSQL(strDitta) & _
          " AND bv_tipork = " & CStrSQL(strTipork) & _
          " AND bv_anno = " & nAnno & _
          " AND bv_mese = " & nMese & _
          " AND bvd_codart = " & CStrSQL(strCodart)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        '--------------------------------------------------------------------------------
        If dttTmp.Rows.Count <> 0 Then
          If Not dttTmp.Rows(0)!Qtabud.Equals(DBNull.Value) Then
            If ((UCase(Left(strUnmis, 2)) = "MT") Or (UCase(Left(strUnmis, 2)) = "ML")) And _
               (bSariaf = True) Then
              dQuant = (dQuant + (NTSCDec(dttTmp.Rows(0)!Qtabud.ToString) * dConver))
            Else
              dQuant = (dQuant + NTSCDec(dttTmp.Rows(0)!Qtabud.ToString))
            End If
          End If
        End If
        '--------------------------------------------------------------------------------
        dttTmp.Clear()
        dttTmp.Dispose()
        '--------------------------------------------------------------------------------
        '--- Devo fare così per considerare anche budget a cavallo di anno
        '--------------------------------------------------------------------------------
        dtCurData = DateAdd(DateInterval.Month, 1, dtCurData)
        nMese = Month(dtCurData)
        nAnno = Year(dtCurData)
        '--------------------------------------------------------------------------------
      Next
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function SelezionaArticoli(ByVal strDitta As String, _
    ByVal nCatlifo As Integer, ByVal strGesfasi As String, _
    ByVal strWhereArtico As String, ByRef dttArtico As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strWhere As String = ""

    Try
      '----------------------------------------------------------------------------------
      TraduciWhere(strWhereArtico, strWhere)
      strSQL = "SELECT * FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_catlifo <> " & nCatlifo & _
        " AND ar_gesfasi = " & CStrSQL(strGesfasi) & _
        strWhere
      dttArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------
      If dttArtico.Rows.Count = 0 Then Return False
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function SelezionaListinoValido(ByVal strDitta As String, _
        ByVal strCodart As String, ByVal nCodlavo As Integer, ByVal lConto As Integer, _
        ByVal nCodvalu As Integer, ByVal nCodtpro As Integer, ByVal strTipo As String, _
        ByVal nListino As Integer, ByVal strDatagg As String, _
        ByVal dDaQuant As Decimal, ByVal dAQuant As Decimal, _
        ByVal strUnmis As String) As DataTable
    Dim strSQL As String = ""
    Dim dttList As New DataTable
    Try
      strSQL = "SELECT TOP 1 * FROM listini" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lc_codart = " & CStrSQL(strCodart) & _
              " AND lc_codlavo = " & nCodlavo & _
              " AND lc_conto = " & lConto & _
              " AND lc_codvalu = " & nCodvalu & _
              " AND lc_codtpro = " & nCodtpro & _
              " AND lc_tipo = " & CStrSQL(strTipo) & _
              " AND lc_listino = " & nListino & _
              " AND lc_datagg <= " & CDataSQL(strDatagg) & _
              " AND lc_datscad >= " & CDataSQL(strDatagg) & _
              " AND lc_daquant <= " & CDblSQL(dDaQuant) & _
              " AND lc_aquant >= " & CDblSQL(dAQuant)
      If strUnmis.Trim = "" Then
        strSQL += " AND (lc_unmis = ' ' Or lc_unmis = " & CStrSQL(strUnmis) & ")"
      Else
        strSQL += " AND lc_unmis = " & CStrSQL(strUnmis)
      End If
      dttList = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return dttList
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function SelezionaListinoAggiornato(ByVal strDitta As String, _
    ByVal strCodart As String, ByVal nCodlavo As Integer, ByVal lConto As Integer, _
    ByVal nCodvalu As Integer, ByVal nCodtpro As Integer, ByVal strTipo As String, _
    ByVal nListino As Integer, ByVal strDatagg As String, _
    ByVal dDaQuant As Decimal, ByVal dAQuant As Decimal, _
    ByVal strUnmis As String) As Boolean
    Dim strSQL As String = ""
    Dim dttList As New DataTable

    Try
      '----------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 lc_listino FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codlavo = " & nCodlavo & _
        " AND lc_conto = " & lConto & _
        " AND lc_codvalu = " & nCodvalu & _
        " AND lc_codtpro = " & nCodtpro & _
        " AND lc_tipo = " & CStrSQL(strTipo) & _
        " AND lc_listino = " & nListino & _
        " AND lc_datagg = " & CDataSQL(strDatagg) & _
        " AND lc_daquant <= " & CDblSQL(dDaQuant) & _
        " AND lc_aquant >= " & CDblSQL(dAQuant)
      If strUnmis.Trim = "" Then
        strSQL += " AND (lc_unmis = ' ' Or lc_unmis = " & CStrSQL(strUnmis) & ")"
      Else
        strSQL += " AND lc_unmis = " & CStrSQL(strUnmis)
      End If
      dttList = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------
      If dttList.Rows.Count = 0 Then Return False
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttList.Clear()
      dttList.Dispose()
    End Try
  End Function

  Public Overridable Function SelezionaListinoNonAggiornato(ByVal strDitta As String, _
    ByVal strCodart As String, ByVal nCodlavo As Integer, ByVal lConto As Integer, _
    ByVal nCodvalu As Integer, ByVal nCodtpro As Integer, ByVal strTipo As String, _
    ByVal nListino As Integer, ByVal strDatagg As String, ByVal strDatscad As String, _
    ByVal dDaQuant As Decimal, ByVal dAQuant As Decimal, ByVal strUnmis As String, _
    ByRef dPrezzo As Decimal) As Boolean
    Dim strSQL As String = ""
    Dim dttList As New DataTable

    Try
      '----------------------------------------------------------------------------------
      dPrezzo = 0
      '----------------------------------------------------------------------------------
      strSQL = "SELECT lc_prezzo FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codlavo = " & nCodlavo & _
        " AND lc_conto = " & lConto & _
        " AND lc_codvalu = " & nCodvalu & _
        " AND lc_codtpro = " & nCodtpro & _
        " AND lc_tipo = " & CStrSQL(strTipo) & _
        " AND lc_listino = " & nListino & _
        " AND lc_datagg = " & CDataSQL(strDatagg) & _
        " AND lc_datscad = " & CDataSQL(strDatscad) & _
        " AND lc_daquant = " & CDblSQL(dDaQuant) & _
        " AND lc_aquant = " & CDblSQL(dAQuant)
      If strUnmis.Trim = "" Then
        strSQL += " AND (lc_unmis = ' ' Or lc_unmis = " & CStrSQL(strUnmis) & ")"
      Else
        strSQL += " AND lc_unmis = " & CStrSQL(strUnmis)
      End If
      dttList = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '----------------------------------------------------------------------------------
      If dttList.Rows.Count = 0 Then Return False
      '----------------------------------------------------------------------------------
      dPrezzo = NTSCDec(dttList.Rows(0)!lc_prezzo)
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttList.Clear()
      dttList.Dispose()
    End Try
  End Function
End Class