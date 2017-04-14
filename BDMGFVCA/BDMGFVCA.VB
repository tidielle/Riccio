Imports NTSInformatica.CLN__STD
Imports System.Data
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGFVCA
  Inherits CLD__BASE

  Public Overridable Function Elabora(ByVal strDitta As String, ByVal dttTmp2 As DataTable, ByVal nCommessaPredef As Integer, _
                                      ByVal bAr_gescomm As Boolean, ByVal strAr_gescommIni As String, ByVal strAr_gescommFin As String, _
                                      ByRef strLog As String, ByVal strDtulap As String, ByVal nNdec As Integer, ByVal bModTCO As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim bRicreaKeymag As Boolean = False
    Dim factory As DbProviderFactory = Nothing
    Dim dbConn As DbConnection = Nothing
    Dim dttDef As New DataTable
    Dim sqlCmd As DbCommand
    Dim lResult As Integer = 0
    Try
      '--------------------------------------------------------------------------------------------------------------
      strLog = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre il database
      '--------------------------------------------------------------------------------------------------------------
      factory = GetFactory(CLE__APP.DBTIPO.DBAZI)
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre la transazione
      '--------------------------------------------------------------------------------------------------------------
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrRow As DataRow In dttTmp2.Rows
        If bAr_gescomm = True Then
          If strAr_gescommIni = "N" And strAr_gescommFin = "S" Then
            'ORDLIST
            strSQL = " UPDATE ordlist " & _
                     " SET ol_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ol_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND ol_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'LISTPROD
            strSQL = " UPDATE listprod " & _
                     " SET lp_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND lp_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND lp_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'MOVORD
            strSQL = " UPDATE movord " & _
                     " SET mo_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND mo_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND mo_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'MOVPRB
            strSQL = " UPDATE movprb " & _
                     " SET mm_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND mm_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND mm_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'MOVMAG
            strSQL = " UPDATE movmag " & _
                     " SET mm_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND mm_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND mm_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'LOTCDEF
            strSQL = " UPDATE lotcdef " & _
                     " SET ld_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ld_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND ld_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'ARTICO
            strSQL = " UPDATE artico SET ar_gescomm = 'S' " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ar_codart = " & CStrSQL(dtrRow!ar_codart)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'RICOSTRUZIONE PROGRESSIVI
            bRicreaKeymag = True
            If bRicreaKeymag Then
              sqlCmd = factory.CreateCommand()
              sqlCmd.CommandType = CommandType.StoredProcedure
              sqlCmd.CommandText = "bussp_bsmgrcap_esegui_key_artico"

              sqlCmd.Parameters.Add(CreateParam(factory, "@dinizioesercizio", DbType.DateTime, 0, CDataSQL(strDtulap).Replace("'", "")))
              sqlCmd.Parameters.Add(CreateParam(factory, "@codditt", DbType.AnsiStringFixedLength, 12, strDitta))
              sqlCmd.Parameters.Add(CreateParam(factory, "@strCodice", DbType.AnsiStringFixedLength, 50, dtrRow!ar_codart))

              lResult = ExecuteSP(sqlCmd, CLE__APP.DBTIPO.DBAZI, dbConn)
              If lResult = -1 Then
                If IsInTrans Then AnnullaTrans()
                dbConn.Close()
                Throw (New NTSException(oApp.Tr(Me, 128551575302031250, "Si è verificato un errore durante l'aggiornamento delle tabelle Keymag, Keyord, Keyprb.")))
                Return False
              End If
            End If

            sqlCmd = factory.CreateCommand()
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "bussp_bsmgrcap_esegui_artico"

            sqlCmd.Parameters.Add(CreateParam(factory, "@dinizioesercizio", DbType.DateTime, 0, CDataSQL(strDtulap).Replace("'", "")))
            sqlCmd.Parameters.Add(CreateParam(factory, "@nRound", DbType.Int32, 0, NTSCStr(nNdec)))
            sqlCmd.Parameters.Add(CreateParam(factory, "@codditt", DbType.AnsiStringFixedLength, 12, strDitta))
            sqlCmd.Parameters.Add(CreateParam(factory, "@nModTCO", DbType.Int16, 1, NTSCInt(IIf(bModTCO = True, 1, 0))))
            sqlCmd.Parameters.Add(CreateParam(factory, "@strCodice", DbType.AnsiStringFixedLength, 50, dtrRow!ar_codart))

            lResult = ExecuteSP(sqlCmd, CLE__APP.DBTIPO.DBAZI, dbConn)
            If lResult = -1 Then
              If IsInTrans Then AnnullaTrans()
              dbConn.Close()
              Throw (New NTSException(oApp.Tr(Me, 128551614634531250, "Si è verificato un errore durante la ricostruzione dei progressivi.")))
              Return False
            End If
          End If

          If strAr_gescommIni = "N" And strAr_gescommFin = "O" Then
            'ORDLIST
            strSQL = " UPDATE ordlist " & _
                     " SET ol_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ol_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND ol_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'LISTPROD
            strSQL = " UPDATE listprod " & _
                     " SET lp_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND lp_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND lp_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'MOVORD
            strSQL = " UPDATE movord " & _
                     " SET mo_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND mo_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND mo_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'ARTICO
            strSQL = " UPDATE artico SET ar_gescomm = 'O' " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ar_codart = " & CStrSQL(dtrRow!ar_codart)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If

          If strAr_gescommIni = "O" And strAr_gescommFin = "S" Then
            'MOVPRB
            strSQL = " UPDATE movprb " & _
                     " SET mm_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND mm_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND mm_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'MOVMAG
            strSQL = " UPDATE movmag " & _
                     " SET mm_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND mm_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND mm_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'LOTCDEF
            strSQL = " UPDATE lotcdef " & _
                     " SET ld_commeca = " & nCommessaPredef & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ld_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND ld_commeca = 0"
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'RICOSTRUZIONE PROGRESSIVI
            bRicreaKeymag = True
            If bRicreaKeymag Then
              sqlCmd = factory.CreateCommand()
              sqlCmd.CommandType = CommandType.StoredProcedure
              sqlCmd.CommandText = "bussp_bsmgrcap_esegui_key_artico"

              sqlCmd.Parameters.Add(CreateParam(factory, "@dinizioesercizio", DbType.DateTime, 0, CDataSQL(strDtulap).Replace("'", "")))
              sqlCmd.Parameters.Add(CreateParam(factory, "@codditt", DbType.AnsiStringFixedLength, 12, strDitta))
              sqlCmd.Parameters.Add(CreateParam(factory, "@strCodice", DbType.AnsiStringFixedLength, 50, dtrRow!ar_codart))

              lResult = ExecuteSP(sqlCmd, CLE__APP.DBTIPO.DBAZI, dbConn)
              If lResult = -1 Then
                If IsInTrans Then AnnullaTrans()
                dbConn.Close()
                Throw (New NTSException(oApp.Tr(Me, 128551575302031250, "Si è verificato un errore durante l'aggiornamento delle tabelle Keymag, Keyord, Keyprb.")))
                Return False
              End If
            End If

            sqlCmd = factory.CreateCommand()
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "bussp_bsmgrcap_esegui_artico"

            sqlCmd.Parameters.Add(CreateParam(factory, "@dinizioesercizio", DbType.DateTime, 0, CDataSQL(strDtulap).Replace("'", "")))
            sqlCmd.Parameters.Add(CreateParam(factory, "@nRound", DbType.Int32, 0, NTSCStr(nNdec)))
            sqlCmd.Parameters.Add(CreateParam(factory, "@codditt", DbType.AnsiStringFixedLength, 12, strDitta))
            sqlCmd.Parameters.Add(CreateParam(factory, "@nModTCO", DbType.Int16, 1, NTSCInt(IIf(bModTCO = True, 1, 0))))
            sqlCmd.Parameters.Add(CreateParam(factory, "@strCodice", DbType.AnsiStringFixedLength, 50, dtrRow!ar_codart))

            lResult = ExecuteSP(sqlCmd, CLE__APP.DBTIPO.DBAZI, dbConn)
            If lResult = -1 Then
              If IsInTrans Then AnnullaTrans()
              dbConn.Close()
              Throw (New NTSException(oApp.Tr(Me, 128551614634531250, "Si è verificato un errore durante la ricostruzione dei progressivi.")))
              Return False
            End If
            'ARTICO
            strSQL = " UPDATE artico SET ar_gescomm = 'S' " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ar_codart = " & CStrSQL(dtrRow!ar_codart)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If

          If strAr_gescommIni = "O" And strAr_gescommFin = "N" Then
            'ARTICO
            strSQL = " UPDATE artico SET ar_gescomm = 'N' " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ar_codart = " & CStrSQL(dtrRow!ar_codart)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If

          If strAr_gescommIni = "S" And strAr_gescommFin = "N" Then
            'ATTENZIONE
            'Se l'articolo è gestito a LOTTI e COMMESSA e nei definitivi esistevano giacenze per commessa < 0...
            'fare la sum(ld_valore) sum(ld_vqtacar) sum(ld_vgiaini) cambierà il VALORE (aumentandolo...)
            'D'altra parte le colonne VALORE servono solo per la stampa schede LOTTI o COMMESSE 
            'Se l'articolo è a LOTTI il VALORE ritorna lo stesso!
            'La commessa non è un problema perchè ora l'articolo non è più a commessa!
            'Pertanto il problema di fatto non esiste.
            'C'è comunque da dire che fare le chiusure con giacenze negative è ANOMALO...

            strSQL = " SELECT codditt, ld_codart, ld_fase, ld_magaz, ld_lotto, ld_ubicaz, 0 as COMMESSA, sum(ld_quant) AS GIACENZA, sum(ld_valore) AS VALORE_GIACENZA, min(ld_tipval) as TIPO_VALORIZZAZIONE_GIACENZA, " & _
                     " sum(ld_qtacar) as QTA_TOTALE_CARICHI, sum(ld_vqtacar) as VALORE_TOTALE_CARICHI, sum(ld_giaini) AS GIACENZA_INIZIALE, " & _
                     " sum(ld_vgiaini) AS VALORE_GIACENZA_INIZIALE" & _
                     " FROM lotcdef" & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ld_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " GROUP BY codditt, ld_codart, ld_fase, ld_magaz, ld_lotto, ld_ubicaz"
            dttDef = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            For Each dtrD As DataRow In dttDef.Rows
              'LOTCDEF
              strSQL = " DELETE FROM lotcdef " & _
                       " WHERE codditt = " & CStrSQL(dttDef.Rows(0)!codditt) & _
                       " AND ld_codart = " & CStrSQL(dttDef.Rows(0)!ld_codart) & _
                       " AND ld_fase = " & NTSCInt(dttDef.Rows(0)!ld_fase) & _
                       " AND ld_magaz = " & NTSCInt(dttDef.Rows(0)!ld_magaz) & _
                       " AND ld_lotto = " & NTSCInt(dttDef.Rows(0)!ld_lotto) & _
                       " AND ld_ubicaz = " & CStrSQL(dttDef.Rows(0)!ld_ubicaz)
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              strSQL = " INSERT INTO lotcdef (codditt, ld_codart, ld_fase, ld_magaz, ld_lotto, ld_ubicaz, ld_commeca, " & _
                       " ld_quant, ld_valore, ld_tipval, ld_qtacar, ld_vqtacar, ld_giaini, ld_vgiaini)" & _
                       " VALUES (" & CStrSQL(dttDef.Rows(0)!codditt) & ", " & CStrSQL(dttDef.Rows(0)!ld_codart) & _
                       " ," & NTSCInt(dttDef.Rows(0)!ld_fase) & " ," & NTSCInt(dttDef.Rows(0)!ld_magaz) & _
                       " ," & NTSCInt(dttDef.Rows(0)!ld_lotto) & " , " & CStrSQL(dttDef.Rows(0)!ld_ubicaz) & _
                       " ," & NTSCInt(dttDef.Rows(0)!commessa) & " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!giacenza)) & _
                       " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!valore_giacenza)) & " ," & CStrSQL(dttDef.Rows(0)!tipo_valorizzazione_giacenza) & _
                       " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!qta_totale_carichi)) & " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!valore_totale_carichi)) & _
                       " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!giacenza_iniziale)) & " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!valore_giacenza_iniziale)) & ")"
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            Next
            'ARTICO
            strSQL = " UPDATE artico SET ar_gescomm = 'N' " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ar_codart = " & CStrSQL(dtrRow!ar_codart)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'RICOSTRUZIONE PROGRESSIVI
            bRicreaKeymag = True
            If bRicreaKeymag Then
              sqlCmd = factory.CreateCommand()
              sqlCmd.CommandType = CommandType.StoredProcedure
              sqlCmd.CommandText = "bussp_bsmgrcap_esegui_key_artico"

              sqlCmd.Parameters.Add(CreateParam(factory, "@dinizioesercizio", DbType.DateTime, 0, CDataSQL(strDtulap).Replace("'", "")))
              sqlCmd.Parameters.Add(CreateParam(factory, "@codditt", DbType.AnsiStringFixedLength, 12, strDitta))
              sqlCmd.Parameters.Add(CreateParam(factory, "@strCodice", DbType.AnsiStringFixedLength, 50, dtrRow!ar_codart))

              lResult = ExecuteSP(sqlCmd, CLE__APP.DBTIPO.DBAZI, dbConn)
              If lResult = -1 Then
                If IsInTrans Then AnnullaTrans()
                dbConn.Close()
                Throw (New NTSException(oApp.Tr(Me, 128551575302031250, "Si è verificato un errore durante l'aggiornamento delle tabelle Keymag, Keyord, Keyprb.")))
                Return False
              End If
            End If

            sqlCmd = factory.CreateCommand()
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "bussp_bsmgrcap_esegui_artico"

            sqlCmd.Parameters.Add(CreateParam(factory, "@dinizioesercizio", DbType.DateTime, 0, CDataSQL(strDtulap).Replace("'", "")))
            sqlCmd.Parameters.Add(CreateParam(factory, "@nRound", DbType.Int32, 0, NTSCStr(nNdec)))
            sqlCmd.Parameters.Add(CreateParam(factory, "@codditt", DbType.AnsiStringFixedLength, 12, strDitta))
            sqlCmd.Parameters.Add(CreateParam(factory, "@nModTCO", DbType.Int16, 1, NTSCInt(IIf(bModTCO = True, 1, 0))))
            sqlCmd.Parameters.Add(CreateParam(factory, "@strCodice", DbType.AnsiStringFixedLength, 50, dtrRow!ar_codart))

            lResult = ExecuteSP(sqlCmd, CLE__APP.DBTIPO.DBAZI, dbConn)
            If lResult = -1 Then
              If IsInTrans Then AnnullaTrans()
              dbConn.Close()
              Throw (New NTSException(oApp.Tr(Me, 128551614634531250, "Si è verificato un errore durante la ricostruzione dei progressivi.")))
              Return False
            End If

          End If

          If strAr_gescommIni = "S" And strAr_gescommFin = "O" Then
            'ATTENZIONE
            'Se l'articolo è gestito a LOTTI e COMMESSA e nei definitivi esistevano giacenze per commessa < 0...
            'fare la sum(ld_valore) sum(ld_vqtacar) sum(ld_vgiaini) cambierà il VALORE (aumentandolo...)
            'D'altra parte le colonne VALORE servono solo per la stampa schede LOTTI o COMMESSE 
            'Se l'articolo è a LOTTI il VALORE ritorna lo stesso!
            'La commessa non è un problema perchè ora l'articolo non è più a commessa!
            'Pertanto il problema di fatto non esiste.
            'C'è comunque da dire che fare le chiusure con giacenze negative è ANOMALO...

            strSQL = " SELECT codditt, ld_codart, ld_fase, ld_magaz, ld_lotto, ld_ubicaz, 0 as COMMESSA, sum(ld_quant) AS GIACENZA, sum(ld_valore) AS VALORE_GIACENZA, min(ld_tipval) as TIPO_VALORIZZAZIONE_GIACENZA, " & _
                     " sum(ld_qtacar) as QTA_TOTALE_CARICHI, sum(ld_vqtacar) as VALORE_TOTALE_CARICHI, sum(ld_giaini) AS GIACENZA_INIZIALE, " & _
                     " sum(ld_vgiaini) AS VALORE_GIACENZA_INIZIALE" & _
                     " FROM lotcdef" & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ld_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " GROUP BY codditt, ld_codart, ld_fase, ld_magaz, ld_lotto, ld_ubicaz"
            dttDef = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            For Each dtrD As DataRow In dttDef.Rows
              'LOTCDEF
              strSQL = " DELETE FROM lotcdef " & _
                       " WHERE codditt = " & CStrSQL(dttDef.Rows(0)!codditt) & _
                       " AND ld_codart = " & CStrSQL(dttDef.Rows(0)!ld_codart) & _
                       " AND ld_fase = " & NTSCInt(dttDef.Rows(0)!ld_fase) & _
                       " AND ld_magaz = " & NTSCInt(dttDef.Rows(0)!ld_magaz) & _
                       " AND ld_lotto = " & NTSCInt(dttDef.Rows(0)!ld_lotto) & _
                       " AND ld_ubicaz = " & CStrSQL(dttDef.Rows(0)!ld_ubicaz)
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              strSQL = " INSERT INTO lotcdef (codditt, ld_codart, ld_fase, ld_magaz, ld_lotto, ld_ubicaz, ld_commeca, " & _
                       " ld_quant, ld_valore, ld_tipval, ld_qtacar, ld_vqtacar, ld_giaini, ld_vgiaini)" & _
                       " VALUES (" & CStrSQL(dttDef.Rows(0)!codditt) & ", " & CStrSQL(dttDef.Rows(0)!ld_codart) & _
                       " ," & NTSCInt(dttDef.Rows(0)!ld_fase) & " ," & NTSCInt(dttDef.Rows(0)!ld_magaz) & _
                       " ," & NTSCInt(dttDef.Rows(0)!ld_lotto) & " , " & CStrSQL(dttDef.Rows(0)!ld_ubicaz) & _
                       " ," & NTSCInt(dttDef.Rows(0)!commessa) & " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!giacenza)) & _
                       " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!valore_giacenza)) & " ," & CStrSQL(dttDef.Rows(0)!tipo_valorizzazione_giacenza) & _
                       " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!qta_totale_carichi)) & " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!valore_totale_carichi)) & _
                       " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!giacenza_iniziale)) & " ," & CDblSQL(NTSCDec(dttDef.Rows(0)!valore_giacenza_iniziale)) & ")"
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            Next
            'ARTICO
            strSQL = " UPDATE artico SET ar_gescomm = 'O' " & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ar_codart = " & CStrSQL(dtrRow!ar_codart)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            'RICOSTRUZIONE PROGRESSIVI
            bRicreaKeymag = True
            If bRicreaKeymag Then
              sqlCmd = factory.CreateCommand()
              sqlCmd.CommandType = CommandType.StoredProcedure
              sqlCmd.CommandText = "bussp_bsmgrcap_esegui_key_artico"

              sqlCmd.Parameters.Add(CreateParam(factory, "@dinizioesercizio", DbType.DateTime, 0, CDataSQL(strDtulap).Replace("'", "")))
              sqlCmd.Parameters.Add(CreateParam(factory, "@codditt", DbType.AnsiStringFixedLength, 12, strDitta))
              sqlCmd.Parameters.Add(CreateParam(factory, "@strCodice", DbType.AnsiStringFixedLength, 50, dtrRow!ar_codart))

              lResult = ExecuteSP(sqlCmd, CLE__APP.DBTIPO.DBAZI, dbConn)
              If lResult = -1 Then
                If IsInTrans Then AnnullaTrans()
                dbConn.Close()
                Throw (New NTSException(oApp.Tr(Me, 128551575302031250, "Si è verificato un errore durante l'aggiornamento delle tabelle Keymag, Keyord, Keyprb.")))
                Return False
              End If
            End If

            sqlCmd = factory.CreateCommand()
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "bussp_bsmgrcap_esegui_artico"

            sqlCmd.Parameters.Add(CreateParam(factory, "@dinizioesercizio", DbType.DateTime, 0, CDataSQL(strDtulap).Replace("'", "")))
            sqlCmd.Parameters.Add(CreateParam(factory, "@nRound", DbType.Int32, 0, NTSCStr(nNdec)))
            sqlCmd.Parameters.Add(CreateParam(factory, "@codditt", DbType.AnsiStringFixedLength, 12, strDitta))
            sqlCmd.Parameters.Add(CreateParam(factory, "@nModTCO", DbType.Int16, 1, NTSCInt(IIf(bModTCO = True, 1, 0))))
            sqlCmd.Parameters.Add(CreateParam(factory, "@strCodice", DbType.AnsiStringFixedLength, 50, dtrRow!ar_codart))

            lResult = ExecuteSP(sqlCmd, CLE__APP.DBTIPO.DBAZI, dbConn)
            If lResult = -1 Then
              If IsInTrans Then AnnullaTrans()
              dbConn.Close()
              Throw (New NTSException(oApp.Tr(Me, 128551614634531250, "Si è verificato un errore durante la ricostruzione dei progressivi.")))
              Return False
            End If
          End If
        End If
      Next

      '--------------------------------------------------------------------------------------------------------------
      '--- Chiude la transazione
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiude il database
      '--------------------------------------------------------------------------------------------------------------
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
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

    End Try
  End Function

  Public Overridable Function ValCodart(ByVal strCodditt As String, ByVal strCodice As String, ByRef strError As String, ByRef dttTmp As DataTable, ByRef dttTmp2 As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      strSQL = " SELECT ar_codart, ar_descr, ar_gescomm, ar_gesvar, ar_codroot" & _
               " FROM artico" & _
               " WHERE codditt = " & CStrSQL(strCodditt) & _
               " AND ar_codart = " & CStrSQL(strCodice)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        strError = "ATTENZIONE" & vbCrLf & _
                   "Il codice articolo indicato è inesistente."
        Return False
      Else

        If NTSCStr(dttTmp.Rows(0)!ar_gesvar) = "S" And NTSCStr(dttTmp.Rows(0)!ar_codroot) = "" Then
          strSQL = " SELECT ar_codart, ar_descr" & _
                   " FROM artico" & _
                   " WHERE codditt = " & CStrSQL(strCodditt) & _
                   " AND ar_codroot = " & CStrSQL(strCodice) & _
                   " UNION SELECT ar_codart, ar_descr" & _
                   " FROM artico" & _
                   " WHERE codditt = " & CStrSQL(strCodditt) & _
                   " AND ar_codart = " & CStrSQL(strCodice)
          dttTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If

        If NTSCStr(dttTmp.Rows(0)!ar_gesvar) = "S" And NTSCStr(dttTmp.Rows(0)!ar_codroot) <> "" Then
          strSQL = " SELECT ar_codart, ar_descr" & _
                   " FROM artico" & _
                   " WHERE codditt = " & CStrSQL(strCodditt) & _
                   " AND ar_codroot = " & CStrSQL(dttTmp.Rows(0)!ar_codroot) & _
                   " UNION SELECT ar_codart, ar_descr" & _
                   " FROM artico" & _
                   " WHERE codditt = " & CStrSQL(strCodditt) & _
                   " AND ar_codart = " & CStrSQL(dttTmp.Rows(0)!ar_codroot)
          dttTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If

        If NTSCStr(dttTmp.Rows(0)!ar_gesvar) = "N" Then
          dttTmp2 = dttTmp.Copy
        End If
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function

End Class