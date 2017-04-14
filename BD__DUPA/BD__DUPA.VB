Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
Imports System.Data.SqlClient

Imports System
Public Class CLD__DUPA
  Inherits CLD__BASE
  Public Overrides Function LeggiTabellaSemplice(ByVal strDitta As String, ByVal strNomeTabella As String, ByRef dsOut As System.Data.DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      If strNomeTabella <> "AZIENDE" Then
        Return MyBase.LeggiTabellaSemplice(strDitta, strNomeTabella, dsOut)
      End If

      strSQL = "SELECT * FROM " & strNomeTabella & _
               " WHERE AzCodaz NOT IN (SELECT opaz_codazi FROM accazi WHERE opaz_opnome = " & CStrSQL(oApp.User) & " AND opaz_abilit = 'N') ORDER BY AzCodaz"

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, strNomeTabella)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overrides Function ScriviTabellaSemplice(ByVal strDitta As String, ByVal strNomeTabella As String, ByRef dttTable As System.Data.DataTable, ByVal strActLogProg As String, ByVal strActLogNomOggLog As String, ByVal strActLogDesLog As String) As Boolean
    Dim dtrChange() As DataRow = Nothing
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""

    Dim i As Integer

    Dim strPrefissoCampo As String = ""
    Dim bInDelete As Boolean = False

    Try
      If strNomeTabella <> "AZIENDE" Then
        Return MyBase.ScriviTabellaSemplice(strDitta, strNomeTabella, dttTable, strActLogProg, strActLogNomOggLog, strActLogDesLog)
      End If

      '-----------------------------
      'prima le delete
      bInDelete = True
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        strSQL = "DELETE FROM " & strNomeTabella & _
                 " WHERE AzCodaz = " & CStrSQL(dtrChange(i)("AzCodaz", DataRowVersion.Original).ToString())
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        dtrChange(i).AcceptChanges()
      Next
      bInDelete = False

      '-----------------------------
      'adesso le insert
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Added)
      strSQL = "INSERT INTO " & strNomeTabella & " " & GetQueryInsertField(dttTable, strPrefissoCampo)
      For i = 0 To dtrChange.Length - 1
        strSQLVal = GetQueryInsertValue(dttTable, dtrChange(i), strPrefissoCampo)
        Me.Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBPRC)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'infine le update
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1
        strSQL = "UPDATE " & strNomeTabella & " SET " & GetQueryUpdate(dttTable, dtrChange(i), strPrefissoCampo) & _
                 " WHERE AzCodaz = " & CStrSQL(dtrChange(i)("AzCodaz", DataRowVersion.Original).ToString())
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        dtrChange(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Try
        'se in fase di cancellaz di una riga si verifica un errore ripristino la riga corrente
        If bInDelete Then dtrChange(i).RejectChanges()
      Catch
      End Try
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try


  End Function

  Public Overridable Function AggInstAnaz(ByVal strConnectionString As String, ByVal strTmp As String, _
                                          ByVal strMod As String, ByVal strModext As String, _
                                          ByVal strDesaz As String, _
                                          ByVal bSvuotaTabelle As Boolean, ByVal bCreaContropartiteAutomatico As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim traDbMy As SqlTransaction = Nothing
    Dim dbCmdMy As SqlCommand
    Dim bConnMy As Boolean = False
    Dim bInTransMy As Boolean = False
    Dim dbConnMy As SqlConnection = Nothing
    Dim lResult As Integer = 0
    Try
      '---------------------------------
      'Apertura della connessione con altro server/db
      '---------------------------------
      dbConnMy = New SqlConnection(strConnectionString)
      dbConnMy.Open()
      bConnMy = True
      traDbMy = dbConnMy.BeginTransaction()
      bInTransMy = True

      '-----------------------------------------------------------------------------------------
      '--- Crea un record in TABINSG
      '-----------------------------------------------------------------------------------------
      strSQL = "INSERT INTO tabinsg (tb_codinsg, tb_opz_1, tb_mod_1, tb_mod_2, tb_mod_3," & _
        " tb_mod_4, tb_mod_5, tb_mod_6, tb_mod_7, tb_mod_8, tb_mod_9, tb_mod_10, tb_mod_11," & _
        " tb_mod_12, tb_mod_13, tb_mod_14, tb_mod_15, tb_mod2_1, tb_mod2_2, tb_mod2_3," & _
        " tb_mod2_4, tb_mod2_5, tb_mod2_6, tb_mod2_7, tb_mod2_8, tb_mod2_9, tb_mod2_10," & _
        " tb_mod2_11, tb_mod2_12, tb_mod2_13, tb_mod2_14, tb_mod2_15, tb_mod3_1, tb_mod3_2," & _
        " tb_mod3_3, tb_mod3_4, tb_mod3_5, tb_mod3_6, tb_mod3_7, tb_mod3_8, tb_mod3_9," & _
        " tb_mod3_10, tb_mod3_11, tb_mod3_12, tb_mod3_13, tb_mod3_14, tb_mod3_15, tb_mod4_1," & _
        " tb_mod4_2, tb_mod4_3, tb_mod4_4, tb_mod4_5, tb_mod4_6, tb_mod4_7, tb_mod4_8," & _
        " tb_mod4_9, tb_mod4_10, tb_mod4_11, tb_mod4_12, tb_mod4_13, tb_mod4_14, tb_mod4_15," & _
        " tb_destcorr, tb_destsedel, tb_destdomf, tb_destresan, tb_annodich, tb_anno770," & _
        " tb_annopaghe, tb_codditcsc)"
      strSQL = strSQL & " VALUES (1, " & CStrSQL(strTmp) & ", " & CStrSQL(Mid(strMod, 1, 1)) & ", " & _
        CStrSQL(Mid(strMod, 2, 1)) & ", " & CStrSQL(Mid(strMod, 3, 1)) & ", " & CStrSQL(Mid(strMod, 4, 1)) & ", " & _
        CStrSQL(Mid(strMod, 5, 1)) & ", " & CStrSQL(Mid(strMod, 6, 1)) & ", " & CStrSQL(Mid(strMod, 7, 1)) & ", " & _
        CStrSQL(Mid(strMod, 8, 1)) & ", " & CStrSQL(Mid(strMod, 9, 1)) & ", " & CStrSQL(Mid(strMod, 10, 1)) & ", " & _
        CStrSQL(Mid(strMod, 11, 1)) & ", " & CStrSQL(Mid(strMod, 12, 1)) & ", " & CStrSQL(Mid(strMod, 13, 1)) & ", " & _
        CStrSQL(Mid(strMod, 14, 1)) & ", " & CStrSQL(Mid(strMod, 15, 1)) & ", " & CStrSQL(Mid(strMod, 16, 1)) & ", " & _
        CStrSQL(Mid(strMod, 17, 1)) & ", " & CStrSQL(Mid(strMod, 18, 1)) & ", " & CStrSQL(Mid(strMod, 19, 1)) & ", " & _
        CStrSQL(Mid(strMod, 20, 1)) & ", " & CStrSQL(Mid(strMod, 21, 1)) & ", " & CStrSQL(Mid(strMod, 22, 1)) & ", " & _
        CStrSQL(Mid(strMod, 23, 1)) & ", " & CStrSQL(Mid(strMod, 24, 1)) & ", " & CStrSQL(Mid(strMod, 25, 1)) & ", " & _
        CStrSQL(Mid(strMod, 26, 1)) & ", " & CStrSQL(Mid(strMod, 27, 1)) & ", " & CStrSQL(Mid(strMod, 28, 1)) & ", " & _
        CStrSQL(Mid(strMod, 29, 1)) & ", " & CStrSQL(Mid(strMod, 30, 1)) & ", " & CStrSQL(Mid(strModext, 1, 1)) & ", " & _
        CStrSQL(Mid(strModext, 2, 1)) & ", " & CStrSQL(Mid(strModext, 3, 1)) & ", " & CStrSQL(Mid(strModext, 4, 1)) & ", " & _
        CStrSQL(Mid(strModext, 5, 1)) & ", " & CStrSQL(Mid(strModext, 6, 1)) & ", " & CStrSQL(Mid(strModext, 7, 1)) & ", " & _
        CStrSQL(Mid(strModext, 8, 1)) & ", " & CStrSQL(Mid(strModext, 9, 1)) & ", " & CStrSQL(Mid(strModext, 10, 1)) & ", " & _
        CStrSQL(Mid(strModext, 11, 1)) & ", " & CStrSQL(Mid(strModext, 12, 1)) & ", " & CStrSQL(Mid(strModext, 13, 1)) & ", " & _
        CStrSQL(Mid(strModext, 14, 1)) & ", " & CStrSQL(Mid(strModext, 15, 1)) & ", " & CStrSQL(Mid(strModext, 16, 1)) & ", " & _
        CStrSQL(Mid(strModext, 17, 1)) & ", " & CStrSQL(Mid(strModext, 18, 1)) & ", " & CStrSQL(Mid(strModext, 19, 1)) & ", " & _
        CStrSQL(Mid(strModext, 20, 1)) & ", " & CStrSQL(Mid(strModext, 21, 1)) & ", " & CStrSQL(Mid(strModext, 22, 1)) & ", " & _
        CStrSQL(Mid(strModext, 23, 1)) & ", " & CStrSQL(Mid(strModext, 24, 1)) & ", " & CStrSQL(Mid(strModext, 25, 1)) & ", " & _
        CStrSQL(Mid(strModext, 26, 1)) & ", " & CStrSQL(Mid(strModext, 27, 1)) & ", " & CStrSQL(Mid(strModext, 28, 1)) & ", " & _
        CStrSQL(Mid(strModext, 29, 1)) & ", " & CStrSQL(Mid(strModext, 30, 1)) & ", 4, 2, 1, 3, " & _
        Date.Now.Year & ", " & Date.Now.Year & ", " & Date.Now.Year & ", ' ')"
      dbCmdMy = New SqlCommand(strSQL, dbConnMy)
      dbCmdMy.Transaction = traDbMy
      dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
      lResult = dbCmdMy.ExecuteNonQuery()
      If lResult = -1 Then
        If bInTransMy Then traDbMy.Rollback()
        'If bConnMy Then dbConnMy.Close()
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      '--- Crea un record in ANAGEN/ANASTO solo se c'è il modulo di anagrafiche generali
      '-----------------------------------------------------------------------------------------
      If Mid(strModext, 10, 1) = "S" Then

        strSQL = " INSERT INTO tabnumg (tb_numtipog,tb_numserieg,tb_numcodlg,tb_numprogg)" & _
                 " VALUES ('AG', ' ', 0, 1)"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          'If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "INSERT INTO anagen (ag_codanag, ag_descr1, ag_descr2, ag_indir, ag_cap," & _
          " ag_citta, ag_prov, ag_stato, ag_codfis, ag_pariva, ag_telef, ag_faxtlx, ag_valuta," & _
          " ag_codling, ag_ultagg, ag_destin, ag_destpag, ag_note, ag_note2, ag_email," & _
          " ag_website, ag_usaem, ag_opnome, ag_webuid, ag_webpwd, ag_sesso, ag_datnasc," & _
          " ag_citnasc, ag_pronasc, ag_stanasc, ag_codfisest, ag_cell, ag_titolo, ag_persfg," & _
          " ag_profes, ag_condom, ag_tpsogiva, ag_codcomu, ag_destcorr, ag_destsedel," & _
          " ag_destdomf, ag_destresan, ag_siglaric, ag_cognome, ag_nome, ag_codcomn," & _
          " ag_nazion1, ag_nazion2, ag_intgen, ag_statofed, ag_soggresi, ag_omocodice," & _
          " ag_estcodiso, ag_estpariva, ag_codrtac) " & _
          "VALUES(1, " & CStrSQL(Left(strDesaz, 30)) & ", null, null, null, null, null," & _
          " null, null, null, null, null, 0, 0, " & CDataSQL(Date.Now) & ", 0, 0," & _
          " null, null, null, null, 'N', " & CStrSQL(oApp.User.Nome) & ", null, null, 'S'," & _
          " null, null, null, null, null, null, null, 'G', 'N', 'N', 'N', null, 0, 0, 0, 0," & _
          " null, null, null, null, null, null, 'N', null, 'S', 'N', null, null, 0)"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          'If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "INSERT INTO anasto (as_codanag, as_datini, as_datfin, as_descr1, as_descr2," & _
          " as_indir, as_cap, as_citta, as_prov, as_stato, as_codfis, as_pariva, as_telef," & _
          " as_faxtlx, as_valuta, as_codling, as_destin, as_destpag, as_note, as_note2," & _
          " as_email, as_website, as_usaem, as_webuid, as_webpwd, as_sesso, as_datnasc," & _
          " as_citnasc, as_pronasc, as_stanasc, as_codfisest, as_cell, as_titolo, as_persfg," & _
          " as_profes, as_condom, as_tpsogiva, as_codcomu, as_destcorr, as_destsedel," & _
          " as_destdomf, as_destresan, as_siglaric, as_cognome, as_nome, as_codcomn," & _
          " as_nazion1, as_nazion2, as_intgen, as_statofed, as_soggresi, as_omocodice," & _
          " as_estcodiso, as_estpariva, as_codrtac) " & _
          "VALUES(1, " & CDataSQL(Date.Now) & "," & CDataSQL("31/12/2099") & ", " & _
          CStrSQL(Left(strDesaz, 30)) & ", null, null, null, null, null," & _
          " null, null, null, null, null, 0, 0, 0, 0," & _
          " null, null, null, null, 'N', null, null, 'S'," & _
          " null, null, null, null, null, null, null, 'G', 'N', 'N', 'N', null, 0, 0, 0, 0," & _
          " null, null, null, null, null, null, 'N', null, 'S', 'N', null, null, 0)"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          ' If bConnMy Then dbConnMy.Close()
          Return False
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Crea un record in TABTIPA
      '-----------------------------------------------------------------------------------------
      strSQL = "INSERT INTO tabtipa (tb_codtipa, tb_destipa, tb_dlldialcs, tb_clsdialcs," & _
        " tb_dllzoom, tb_clszoom, tb_dllgesans, tb_clsgesans, tb_dlldialdoc, tb_clsdialdoc," & _
        " tb_nomtabcs)" & _
        " VALUES (1, 'Tipologia unica', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)"
      dbCmdMy = New SqlCommand(strSQL, dbConnMy)
      dbCmdMy.Transaction = traDbMy
      dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
      lResult = dbCmdMy.ExecuteNonQuery()
      If lResult = -1 Then
        If bInTransMy Then traDbMy.Rollback()
        ' If bConnMy Then dbConnMy.Close()
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      '--- Se non si vogliono i dati standard, svuoto le tabelle distribuite
      '-----------------------------------------------------------------------------------------
      If bSvuotaTabelle Then
        strSQL = "DELETE FROM TABPECE"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '  If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM TABCOCE"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          ' If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM TABPECG"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '  If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM TABPEPA"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '   If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM TABTPCO"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '   If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM anpvric"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          ' If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM vociric"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '    If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabcoce"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '   If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabcamc"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '     If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabcace"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '   If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM caucset"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '   If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM caucsiv"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '  If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabcauc"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '  If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabvalu"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '    If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabcovp"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          ' If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabcovg"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '   If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabciva"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '    If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabrtac"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '    If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM anptric"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          ' If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM mastric"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '    If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabtric"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '   If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM anagpc"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '   If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabstat"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '   If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabmast"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          ' If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabclas"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          ' If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabgruc"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '  If bConnMy Then dbConnMy.Close()
          Return False
        End If

        strSQL = "DELETE FROM tabpcon"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '    If bConnMy Then dbConnMy.Close()
          Return False
        End If

      End If
      '-----------------------------------------------------------------------------------------
      '--- chiedo se nel nuovo database quando creo un sottoconto pdc nuovo devo creare anceh la rispettiva contropartita in automatico
      '-----------------------------------------------------------------------------------------
      If bCreaContropartiteAutomatico Then
        strSQL = "UPDATE tabinsg SET tb_codditcsc = 'S' WHERE tb_codinsg = 1"
        dbCmdMy = New SqlCommand(strSQL, dbConnMy)
        dbCmdMy.Transaction = traDbMy
        dbCmdMy.CommandTimeout = oApp.Db.QueryTimeOut
        lResult = dbCmdMy.ExecuteNonQuery()
        If lResult = -1 Then
          If bInTransMy Then traDbMy.Rollback()
          '     If bConnMy Then dbConnMy.Close()
          Return False
        End If
      End If

      'Chiudo il database
      traDbMy.Commit()
      bInTransMy = False
      '    If bConnMy Then dbConnMy.Close()
      Return True

    Catch ex As Exception
      If bInTransMy Then traDbMy.Rollback()
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      'Chiudo il database
      If bConnMy Then dbConnMy.Close()
    End Try
  End Function

  Public Overridable Function LeggiDitte(ByVal strConnectionString As String, ByRef dsOut As System.Data.DataSet) As Boolean
    Try
      Return ImportaOpenRecordset("tabanaz", dsOut, False, strConnectionString, "SELECT codditt, tb_azrags1 FROM tabanaz ORDER BY codditt")
    Catch ex As Exception
      Return False
    End Try
  End Function

  '#Region "funzioni specifiche per importazione"
  Public Overridable Function ImportaConnetti(ByVal strConnect As String, _
                                              ByRef dbImportaConn As SqlConnection) As Boolean
    Try

      dbImportaConn = New SqlConnection(strConnect)
      dbImportaConn.Open()
      Return True

    Catch ex As Exception
      '---------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '---------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckConnectionAltroDB(ByVal strAziendaConnectionString As String) As Boolean
    Dim dbImportConn As SqlClient.SqlConnection = Nothing
    Try
      If ImportaConnetti(strAziendaConnectionString, dbImportConn) Then
        dbImportConn.Close()
        Return True
      End If
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function VerificaDatabaseUnicode(ByVal strAziendaConnectionString As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = "SELECT data_type FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'RELEASE' AND column_name = 'rel_dbpers'"
    Try
      ds = OpenRecordsetAltroDB(strSQL, "RELEASE", strAziendaConnectionString)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ImportaOpenRecordset(ByVal strTabella As String, _
                                                   ByRef ds As DataSet, _
                                                   ByVal bAziendaIsCorrente As Boolean, _
                                                   ByVal strAziendaConnectionString As String, _
                                                   ByVal strSQL As String) As Boolean
    Try
      If bAziendaIsCorrente Then
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strTabella)
      Else
        ds = OpenRecordsetAltroDB(strSQL, strTabella, strAziendaConnectionString)
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ImportaGetOrderTblTabelleComuni(ByRef ds As DataSet, _
                                                              ByVal bAziendaIsCorrente As Boolean, _
                                                              ByVal strAziendaConnectionString As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ot_order, ot_nometab, ot_descr, ot_sw FROM ordertbl" & _
               " WHERE (ot_perditta = 'N')" & _
               " AND (ot_istmp = 'N')" & _
               " AND (ot_nometab NOT IN ('ordertbl', 'release', 'distrdati', 'tabpcon', 'tabgruc', 'tabclas', 'tabmast'," & _
               " 'tabling', 'tabstat', 'comuni', 'anagpc', 'tabtric', 'mastric', 'anptric', 'tabrtac', 'anagen', 'destgen', 'anasto', 'deststo'," & _
               " 'tabtacq', 'tabciva', 'tabgrua', 'tabstud', 'tabruaz', 'tabinsg', 'tabnumg', 'tabcovg', 'tabcovp', 'tabpaga', 'pagalin', 'tabpecg', 'tabpece'," & _
               " 'tabvalu', 'cambi', 'tabcauc', 'caucsiv', 'caucset', 'tabtpco', 'tablmup', 'tabcace', 'tabcamc', 'tabgrce', 'tabspce', 'tabpuce'," & _
               " 'tabcoce', 'tabmpor', 'tabntra', 'tabrsta', 'tabcaum', 'tabbotr', 'tabenas', 'tabpeve', 'tabpeac', 'tabcaus', 'vociric', 'anpvric', 'tabpepa'))" & _
               " ORDER BY ot_order"
      Return ImportaOpenRecordset("ordertbl", ds, bAziendaIsCorrente, strAziendaConnectionString, strSQL)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ImportaGetOrderTblTutteTabelle(ByRef ds As DataSet, _
                                                             ByVal bAziendaIsCorrente As Boolean, _
                                                             ByVal strAziendaConnectionString As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ot_order, ot_nometab FROM ordertbl ORDER BY ot_order"
      Return ImportaOpenRecordset("ordertbl", ds, bAziendaIsCorrente, strAziendaConnectionString, strSQL)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ImportaDeleteTabella(ByVal strTabella As String, _
                                                   ByVal bAziendaIsCorrente As Boolean, _
                                                   ByVal strAziendaConnectionString As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM " & strTabella

      If bAziendaIsCorrente Then
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Else
        ExecuteAltroDB(strSQL, strAziendaConnectionString)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ImportaDeleteTabellaSingoleRighe(ByVal strTabella As String, _
                                                               ByVal bAziendaIsCorrente As Boolean, _
                                                               ByVal strAziendaConnectionString As String, _
                                                               Optional ByVal strSQL As String = "") As Boolean
    Dim dsRecordDaEliminare As New DataSet
    Dim dtrRowKeyDestinazione() As DataRow = Nothing
    Try

      If strSQL = "" Then
        strSQL = " SELECT * FROM " & strTabella
      Else
        strSQL = " SELECT * FROM " & strTabella & " WHERE " & strSQL
      End If

      If Not ImportaOpenRecordset(strTabella, dsRecordDaEliminare, bAziendaIsCorrente, strAziendaConnectionString, strSQL) Then
        Return False
      End If
      If dsRecordDaEliminare.Tables(strTabella).Rows.Count = 0 Then Return True

      Dim dttTabella As New DataTable
      ElencoCampiStructDb(strTabella, strAziendaConnectionString, 1, dttTabella)
      If dtrRowKeyDestinazione Is Nothing Then
        dtrRowKeyDestinazione = dttTabella.Select("IsKey = True")
      End If

      For i As Integer = 0 To dsRecordDaEliminare.Tables(strTabella).Rows.Count - 1
        '-----------------------------------------------------------------------------------------
        '--- Ricerca la chiave primaria costruzione where
        '-----------------------------------------------------------------------------------------

        strSQL = ""
        For j As Integer = 0 To dtrRowKeyDestinazione.Length - 1
          Dim strDataType As String = NTSCStr(dtrRowKeyDestinazione(j).Item("DataType"))
          Select Case strDataType
            Case "System.Int32", "System.Int16"
              strSQL = strSQL & " AND " & NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")) & " = " & _
              CDblSQL(NTSCStr(dsRecordDaEliminare.Tables(strTabella).Rows(i)(NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")))))
            Case "System.String"
              strSQL = strSQL & " AND " & NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")) & " = " & _
              CStrSQL(NTSCStr(dsRecordDaEliminare.Tables(strTabella).Rows(i)(NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")))))
            Case "System.DateTime"
              strSQL = strSQL & " AND " & NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")) & " = " & _
              CDataSQL(NTSCStr(dsRecordDaEliminare.Tables(strTabella).Rows(i)(NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")))))
          End Select
        Next

        If Len(strSQL) > 0 Then
          strSQL = Mid(strSQL, 6)
        End If
        strSQL = "DELETE FROM " & strTabella & " WHERE " & strSQL

        If Not ImportaDeleteRiga(strTabella, strSQL, strAziendaConnectionString) Then
          Return False
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ImportaDeleteRiga(ByVal strNomeTabella As String, _
                                                ByVal strSQL As String, _
                                                ByVal strAziendaConnectionString As String) As Boolean
    Try

      ExecuteAltroDB(strSQL, strAziendaConnectionString, , , True)
      Return True

    Catch ex As Exception
      If InStr(ex.ToString(), "SqlException") > 0 Then
        Dim exSQL As SqlException = CType(ex, SqlException)
        If exSQL.Number = 547 Then Return True
      End If
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function OpenRecordsetAltroDB(ByVal strSQL As String, _
                                                   ByVal strTabella As String, _
                                                   ByVal strConnect As String, _
                                                   Optional ByRef dbImportConn As SqlClient.SqlConnection = Nothing, _
                                                   Optional ByRef traDbMy As SqlTransaction = Nothing) As DataSet
    Dim bImportConn As Boolean = False
    Dim dbImportCmd As SqlClient.SqlCommand = Nothing
    Dim dbImportAdapt As SqlClient.SqlDataAdapter = Nothing
    Dim dsImportOut As New DataSet
    Try
      'apro il database
      If dbImportConn Is Nothing Then
        If Not ImportaConnetti(strConnect, dbImportConn) Then Return New DataSet
        bImportConn = True
      End If

      dbImportCmd = New SqlCommand(strSQL, dbImportConn)
      If Not traDbMy Is Nothing Then dbImportCmd.Transaction = traDbMy
      dbImportCmd.CommandTimeout = oApp.Db.QueryTimeOut
      dbImportAdapt = New SqlDataAdapter(dbImportCmd)
      'ottengo i dati
      dbImportAdapt.SelectCommand = dbImportCmd
      dbImportAdapt.Fill(dsImportOut)
      dsImportOut.Tables(dsImportOut.Tables.Count - 1).TableName = strTabella

      '------------------------------------
      'traduco i nomi dell'intestazioni delle colonne tutto in minuscolo.
      'diversamente se devo collegare la tabella ad una griglia ed i nomi colonne sono diversi (case sensitive) 
      'con i FieldName delle colonne della griglia la colonna rimane vuota
      For i As Integer = 0 To dsImportOut.Tables(strTabella).Columns.Count - 1
        dsImportOut.Tables(strTabella).Columns(i).ColumnName = dsImportOut.Tables(strTabella).Columns(i).ColumnName.ToLower
      Next

      Return dsImportOut

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return New DataSet
    Finally
      If bImportConn Then
        'chiudo il database
        dbImportConn.Close()
      End If
    End Try
  End Function
  Public Overridable Function ExecuteAltroDB(ByVal strSQL As String, _
                                 ByVal strConnect As String, _
                                 Optional ByRef dbImportConn As SqlClient.SqlConnection = Nothing, _
                                 Optional ByRef traDbMy As SqlTransaction = Nothing, _
                                 Optional ByVal bReturnOriginalException As Boolean = False) As Integer

    Dim dbImportCmd As SqlClient.SqlCommand = Nothing
    Dim bImportConn As Boolean = False
    Dim lResult As Integer

    Try
      If dbImportConn Is Nothing Then
        If Not ImportaConnetti(strConnect, dbImportConn) Then Return -1
        bImportConn = True
      End If

      dbImportCmd = New SqlCommand(strSQL, dbImportConn)
      If InStr(strSQL, "@pic") > 0 Then dbImportCmd.Parameters.AddWithValue("@pic", pic)

      If Not traDbMy Is Nothing Then
        dbImportCmd.Transaction = traDbMy
      Else
        If IsInTrans Then dbImportCmd.Transaction = CType(traDb, SqlTransaction)
      End If

      dbImportCmd.CommandTimeout = oApp.Db.QueryTimeOut
      lResult = dbImportCmd.ExecuteNonQuery()

      Return lResult

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      If bReturnOriginalException = True Then
        Throw (ex)
      Else
        Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return -1
    Finally
      If bImportConn Then
        dbImportConn.Close()
      End If
    End Try
  End Function

  Public Overridable Function ImportaDaAltroDb(ByVal strTabella As String, _
                                               ByVal bAziendaOrigineIsCorrente As Boolean, _
                                               ByVal strAziendaOrigineConnectionString As String, _
                                               ByVal bAziendaDestinazioneIsCorrente As Boolean, _
                                               ByVal strAziendaDestinazioneConnectionString As String, _
                                               Optional ByVal strSQL As String = "", _
                                               Optional ByVal bOverwrite As Boolean = False, _
                                               Optional ByVal bTutti As Boolean = True) As Boolean
    Dim dsOrigine As DataSet = Nothing
    Dim dttTabellaOrigine As DataTable = Nothing
    Dim dsDestinazione As DataSet = Nothing
    Dim dttTabellaDestinazione As DataTable = Nothing
    Dim dtrRowKeyDestinazione() As DataRow = Nothing
    Dim i As Integer
    Dim strValues As String
    Dim dbImportConn As SqlClient.SqlConnection = Nothing
    Dim traDbMy As SqlTransaction = Nothing

    Dim bImportConn As Boolean = False
    Dim bInTransMy As Boolean = False
    Dim bReturn As Boolean = True

    Dim strElenco As String = ""
    Dim strElenco1 As String = ""
    Try

      'ottengo un datatable con l'elenco delle colonne della tabella di origine e quella di destinazione
      ElencoCampiGetStruttura(strTabella, dsOrigine, bAziendaOrigineIsCorrente, strAziendaOrigineConnectionString)
      ElencoCampiGetStruttura(strTabella, dsDestinazione, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString)

      'ottengo l'elenco dei campi contenuti nella tabella di origine e in quella di destinazione
      ElencoCampiStructDb(strTabella, strAziendaOrigineConnectionString, 1, dttTabellaOrigine)
      ElencoCampiStructDb(strTabella, strAziendaDestinazioneConnectionString, 1, dttTabellaDestinazione)

      'dalla tabella di origine tolgo i campi non presenti nella tabella di destinazione
      Dim bTrovato As Boolean
      For Each dtrT As DataRow In dttTabellaOrigine.Rows
        bTrovato = False
        For b As Integer = 0 To dttTabellaDestinazione.Rows.Count - 1
          If NTSCStr(dtrT!ColumnName) = NTSCStr(dttTabellaDestinazione.Rows(b)!ColumnName) Then
            bTrovato = True
            Exit For
          End If
        Next
        If bTrovato = False Then
          dsOrigine.Tables(strTabella).Columns.Remove(NTSCStr(dtrT!ColumnName))
          dtrT.Delete()
        End If
      Next
      dttTabellaOrigine.AcceptChanges()
      dsOrigine.AcceptChanges()

      'dalla tabella di destinazione tolgo i campi non presenti nella tabella di origine
      For Each dtrT As DataRow In dttTabellaDestinazione.Rows
        bTrovato = False
        For b As Integer = 0 To dttTabellaOrigine.Rows.Count - 1
          If NTSCStr(dtrT!ColumnName) = NTSCStr(dttTabellaOrigine.Rows(b)!ColumnName) Then
            bTrovato = True
            Exit For
          End If
        Next
        If bTrovato = False Then
          dsDestinazione.Tables(strTabella).Columns.Remove(NTSCStr(dtrT!ColumnName))
          dtrT.Delete()
        End If
      Next
      dttTabellaDestinazione.AcceptChanges()
      dsDestinazione.AcceptChanges()

      'A QUESTO PUNTO NELLA TABELLA DI ORIGINE ED IN QUELLA DI DESTINAZIONE SONO PRESENTI LE STESSE COLONNE
      'la il loro ordinamento potrebbe essere diverso


      'compongo l'elenco dei campi per la SELECT nella tabella di origine, scartando il 'TS' ed i campi COUNTER
      'With dsOrigine.Tables(strTabella)
      '  For y As Integer = 0 To .Columns.Count - 1
      '    If (CBool(dttTabellaOrigine.Rows(y)("IsIdentity")) = True) Or ((Trim(.Columns(y).ColumnName)).ToUpper = "TS") Then
      '      'scarto
      '    Else
      '      strElenco = strElenco & .Columns(y).ColumnName & ", "
      '    End If
      '  Next
      'End With
      'strElenco = " " & Mid(strElenco, 1, Len(strElenco) - 2)

      'ottengo l'elenco dei campi per la SELECT da utilizzare per prelevare i dati dalla tabella di origine, 
      'scartando il 'TS' ed i campi COUNTER
      With dsDestinazione.Tables(strTabella)
        For y As Integer = 0 To .Columns.Count - 1
          If (CBool(dttTabellaDestinazione.Rows(y)("IsIdentity")) = True) Or ((Trim(.Columns(y).ColumnName)).ToUpper = "TS") Then
            'scarto
          Else
            strElenco = strElenco & .Columns(y).ColumnName & ", "
            If (dttTabellaDestinazione.Rows(y)("DataTypeName").ToString = "varchar") And (CBool(dttTabellaDestinazione.Rows(y)("AllowDBNull")) = True) Then
              strElenco1 = strElenco1 & "CASE WHEN " & .Columns(y).ColumnName & " = '' and len(" & .Columns(y).ColumnName & " )=0 THEN '' ELSE " & .Columns(y).ColumnName & " END AS " & .Columns(y).ColumnName & ", "
            Else
              If (dttTabellaDestinazione.Rows(y)("DataTypeName").ToString = "varchar") And (CBool(dttTabellaDestinazione.Rows(y)("AllowDBNull")) = False) And ((.Columns(y).DefaultValue Is """ """) Or (.Columns(y).DefaultValue Is System.DBNull.Value)) Then
                strElenco1 = strElenco1 & "CASE WHEN " & .Columns(y).ColumnName & " = '' THEN ' ' ELSE " & .Columns(y).ColumnName & " END AS " & .Columns(y).ColumnName & ", "
              Else
                strElenco1 = strElenco1 & .Columns(y).ColumnName & ", "
              End If
            End If
          End If
        Next
      End With
      strElenco = " " & Mid(strElenco, 1, Len(strElenco) - 2)
      strElenco1 = Mid(strElenco1, 1, Len(strElenco1) - 2)

      If strSQL = "" And (bOverwrite = True Or bTutti = True) Then
        Try
          Dim strSqlTmp As String = ""
          Dim dbImportConnOrig As SqlClient.SqlConnection
          Dim dbImportConnDest As SqlClient.SqlConnection
          If ImportaConnetti(strAziendaOrigineConnectionString, dbImportConnOrig) Then
            If ImportaConnetti(strAziendaDestinazioneConnectionString, dbImportConnDest) Then
              If dbImportConnDest.DataSource.ToLower = dbImportConnOrig.DataSource.ToLower Then
                strSqlTmp = "INSERT INTO " & strTabella & " (" & strElenco & ") SELECT " & strElenco & " FROM [" & dbImportConnOrig.Database & "].[dbo].[" & strTabella & "]"
                If ExecuteAltroDB(strSqlTmp, strAziendaDestinazioneConnectionString, dbImportConn, traDbMy) <> -1 Then
                  Try
                    dbImportConnDest.Close()
                    dbImportConnDest.Dispose()
                  Catch exA As Exception
                  End Try
                  Try
                    dbImportConnOrig.Close()
                    dbImportConnOrig.Dispose()
                  Catch exB As Exception
                  End Try
                  Return True
                End If
              End If
            End If
          End If
          Try
            dbImportConnDest.Close()
            dbImportConnDest.Dispose()
          Catch exC As Exception
          End Try
          Try
            dbImportConnOrig.Close()
            dbImportConnOrig.Dispose()
          Catch exD As Exception
          End Try
        Catch ex As Exception
        End Try
      End If

      'ottengo i dati da importare
      If strSQL = "" Then
        strSQL = " SELECT " & strElenco1 & " FROM " & strTabella
      Else
        strSQL = " SELECT " & strElenco1 & " FROM " & strTabella & " WHERE " & strSQL
      End If
      ImportaOpenRecordset(strTabella, dsOrigine, bAziendaOrigineIsCorrente, strAziendaOrigineConnectionString, strSQL)


      If Not ImportaConnetti(strAziendaDestinazioneConnectionString, dbImportConn) Then Return False
      bImportConn = True
      If bAziendaDestinazioneIsCorrente And (strTabella <> "allole") Then
        ApriTrans(dbImportConn)
      Else
        traDbMy = dbImportConn.BeginTransaction()
      End If
      bInTransMy = True

      For i = 0 To dsOrigine.Tables(strTabella).Rows.Count - 1
        If strTabella = "allole" Then
          strValues = GetQueryInsertValueAllOLE(dsOrigine.Tables(strTabella), dsOrigine.Tables(strTabella).Rows(i), "", "", "", Nothing)
        Else
          strValues = GetQueryInsertValue(dsOrigine.Tables(strTabella), dsOrigine.Tables(strTabella).Rows(i), "")
        End If

        'compongo la query di INSERT INTO 
        strSQL = "INSERT INTO " & strTabella & " (" & strElenco & ") VALUES " & strValues

        If bTutti = True Then
          If bAziendaDestinazioneIsCorrente And (strTabella <> "allole") Then
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbImportConn)
          Else
            ExecuteAltroDB(strSQL, strAziendaDestinazioneConnectionString, dbImportConn, traDbMy)
          End If
        Else

          If Not CopiaDatiEsegui(strSQL, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString, dbImportConn, traDbMy) Then
            If bOverwrite = True Then

              'Se la insert ha fallito e sono in copia alcuni dati
              '(opzione sostituisci completamente o sovrascrivi a parità di codice)
              'provo a fare un'update

              strSQL = ""

              If dtrRowKeyDestinazione Is Nothing Then
                dtrRowKeyDestinazione = dttTabellaDestinazione.Select("IsKey = True")
              End If
              For j As Integer = 0 To dtrRowKeyDestinazione.Length - 1
                Dim strDataType As String = NTSCStr(dtrRowKeyDestinazione(j).Item("DataType"))
                Select Case strDataType
                  Case "System.Int32", "System.Int16"
                    strSQL = strSQL & " AND " & NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")) & " = " & _
                    CDblSQL(NTSCStr(dsOrigine.Tables(strTabella).Rows(i)(NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")))))
                  Case "System.String"
                    strSQL = strSQL & " AND " & NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")) & " = " & _
                    CStrSQL(NTSCStr(dsOrigine.Tables(strTabella).Rows(i)(NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")))))
                  Case "System.DateTime"
                    strSQL = strSQL & " AND " & NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")) & " = " & _
                    CDataSQL(NTSCStr(dsOrigine.Tables(strTabella).Rows(i)(NTSCStr(dtrRowKeyDestinazione(j).Item("ColumnName")))))
                End Select
              Next

              If Len(strSQL) > 0 Then
                strSQL = " WHERE " & Mid(strSQL, 6)
              End If

              strSQL = "UPDATE " & strTabella & " SET " & GetQueryUpdate(dsOrigine.Tables(strTabella), dsOrigine.Tables(strTabella).Rows(i), "") & strSQL

              If bAziendaDestinazioneIsCorrente And (strTabella <> "allole") Then
                Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbImportConn)
              Else
                ExecuteAltroDB(strSQL, strAziendaDestinazioneConnectionString, dbImportConn, traDbMy)
              End If

            End If
          End If

        End If
      Next

      'transazione commit
      If bAziendaDestinazioneIsCorrente And (strTabella <> "allole") Then
        ChiudiTrans()
      Else
        traDbMy.Commit()
      End If
      bInTransMy = False

      Return bReturn

    Catch ex As Exception
      If bInTransMy Then
        If bAziendaDestinazioneIsCorrente Then
          AnnullaTrans()
        Else
          traDbMy.Rollback()
        End If
      End If
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      If bImportConn Then
        dbImportConn.Close()
      End If
    End Try
  End Function

  Public pic() As Byte
  Public Overridable Function GetQueryInsertValueAllOLE(ByRef dttTabella As DataTable, ByRef dtrRiga As DataRow, _
                            ByVal strPrefissoCampo As String, ByVal strColonnaCounter As String, _
                            ByVal strAliasPrefissoCampo As String, ByRef dttFieldAlias As DataTable) As String
    'ritorna la parte finale di una query di insert (quella contenente il valore da assegnare ai campi)
    Dim e As Integer
    Dim strSQL As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim bSalta As Boolean = False

    Try
      If strAliasPrefissoCampo = "" Then strAliasPrefissoCampo = strPrefissoCampo
      For e = 0 To dttTabella.Columns.Count - 1
        With dttTabella.Columns(e)
          If .ColumnName.ToLower() = "ts" Then
            'scarto
          ElseIf .AutoIncrement() Then
            'scarto
          ElseIf .ColumnName.ToString.ToUpper = strColonnaCounter.ToUpper Then
            'scarto colonna counter
          ElseIf .ColumnName.Length < strAliasPrefissoCampo.Length Then
            'scarto
          ElseIf .ColumnName.ToLower = "instid" Then
            'colonna instid
            strSQL += CDblSQL(dtrRiga(.ColumnName).ToString()) & ", "
          ElseIf .ColumnName.ToLower = "codditt" Then
            'colonna codice ditta
            strSQL += CStrSQL(dtrRiga(.ColumnName).ToString()) + ", "
          ElseIf .ColumnName.ToLower = "ao_ole" Then
            If dtrRiga(.ColumnName) Is System.DBNull.Value Then
              strSQL += "null, "
            Else
              strSQL += "@pic, "
              pic = CType(dtrRiga(.ColumnName), Byte())
            End If
          ElseIf .ColumnName.ToLower().Substring(0, strAliasPrefissoCampo.Length).ToLower = strAliasPrefissoCampo.ToLower Or _
                 .ColumnName.ToLower().Substring(0, 3).ToLower = "hh_" Or _
                 .ColumnName.ToLower().Substring(0, 3).ToLower = "qq_" Then

            bSalta = False
            If Not dttFieldAlias Is Nothing Then
              dtrT = dttFieldAlias.Select("datatable = '" & dttTabella.Columns(e).ColumnName.ToLower() & "'")
              If dtrT.Length > 0 Then
                If dtrT(0)!database.ToString.ToLower().Trim = "" Then
                  bSalta = True
                End If
              End If
            End If

            If bSalta = False Then
              If .AllowDBNull = True And dtrRiga(.ColumnName).ToString() = "" Then
                strSQL += "null"
              Else
                Select Case .DataType.ToString()
                  Case "System.String"
                    strSQL += CStrSQL(dtrRiga(.ColumnName).ToString())
                  Case "System.DateTime"
                    If .ColumnName.ToUpper.IndexOf("ULTAGG") > -1 Or _
                       .ColumnName.ToUpper.IndexOf("_START") > -1 Or _
                       .ColumnName.ToUpper.IndexOf("_STOP") > -1 Or _
                       .ColumnName.ToUpper.IndexOf("DTOR") > -1 Then
                      'distinguo tra datetime o solo date
                      'start/stop per lavcent-avlavp
                      Dim dtTmp As DateTime = NTSCDate(NTSCStr(dtrRiga(.ColumnName)))
                      If dtTmp.Hour <> 0 Or dtTmp.Minute <> 0 Or dtTmp.Second <> 0 Then
                        strSQL += CDataOraSQL(dtrRiga(.ColumnName).ToString())
                      Else
                        strSQL += CDataSQL(dtrRiga(.ColumnName).ToString())
                      End If
                    Else
                      'sempre e solo data 
                      strSQL += CDataSQL(dtrRiga(.ColumnName).ToString())
                    End If
                  Case Else
                    'sono sempre numeri
                    strSQL += CDblSQL(dtrRiga(.ColumnName).ToString())
                End Select
              End If    'If .AllowDBNull = True And dtrChange(i)(.ColumnName).ToString() = "" Then
              strSQL += ", "
            End If    'If bSalta = False Then

          End If    'If .ColumnName.ToLower().Substring(2, 1) = "_" And .ColumnName.ToLower() <> "ts" Then
        End With
      Next    'For i = 1 To dttRiga.Columns.Count - 1

      If strSQL.Length > 2 Then strSQL = " ( " & strSQL.Substring(0, strSQL.Length - 2) & " ) "

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ElencoCampiGetStruttura(ByVal strTabella As String, _
                                                      ByRef ds As DataSet, _
                                                      ByVal bAziendaDestinazioneIsCorrente As Boolean, _
                                                      ByVal strAziendaDestinazioneConnectionString As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 * FROM " & strTabella

      If bAziendaDestinazioneIsCorrente Then
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strTabella)
      Else
        ds = OpenRecordsetAltroDB(strSQL, strTabella, strAziendaDestinazioneConnectionString)
      End If
      Return True


    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ElencoCampiStructDb(ByVal strTabella As String, _
                                                  ByVal strConnect As String, _
                                                  ByVal lTipoReader As Integer, _
                                                  ByRef dttImport As DataTable) As Boolean
    Dim strSQL As String = ""

    Dim drRead As SqlDataReader
    Dim dbImportConn As SqlClient.SqlConnection = Nothing
    Dim bImportConn As Boolean = False
    Dim dbImportCmd As SqlClient.SqlCommand = Nothing
    Dim dbImportAdapt As SqlDataAdapter = Nothing
    Try
      strSQL = "SELECT * FROM " & strTabella
      If Not ImportaConnetti(strConnect, dbImportConn) Then Return False
      bImportConn = True
      dbImportCmd = New SqlCommand(strSQL, dbImportConn)
      dbImportCmd.CommandTimeout = oApp.Db.QueryTimeOut
      If lTipoReader = 1 Then
        drRead = dbImportCmd.ExecuteReader(CommandBehavior.KeyInfo) 'attenzione restituisce chiave primaria forse rallentare
      Else
        drRead = dbImportCmd.ExecuteReader()
      End If
      dttImport = drRead.GetSchemaTable()


      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      If bImportConn = True Then
        dbImportConn.Close()
      End If
    End Try
  End Function

  Public Overloads Function GetBusRelease(ByRef dttOut As DataTable, _
                                          ByVal bAziendaIsCorrente As Boolean, _
                                          ByVal strAziendaConnectionString As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT rel_maior, rel_minor, rel_pers FROM release WHERE UPPER(rel_sw) = 'BUSINESS'"

      If bAziendaIsCorrente Then
        dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Else
        dttOut = OpenRecordsetAltroDB(strSQL, "release", strAziendaConnectionString).Tables("release")
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetValutaConto(ByVal bAziendaIsCorrente As Boolean, _
                                             ByVal strAziendaConnectionString As String) As String
    Dim strSQL As String = ""
    Dim dttOut As New DataTable
    Try
      GetValutaConto = "[Indefinita]"

      strSQL = "SELECT tb_opz_1 FROM tabinsg"

      If bAziendaIsCorrente Then
        dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Else
        dttOut = OpenRecordsetAltroDB(strSQL, "tabinsg", strAziendaConnectionString).Tables("tabinsg")
      End If

      If dttOut.Rows.Count > 0 Then
        Select Case UCase(NTSCStr(dttOut.Rows(0)!tb_opz_1))
          Case "E"
            GetValutaConto = "Euro"
          Case "L"
            GetValutaConto = "Lire italiane"
          Case Else
            GetValutaConto = "[Indefinita]"
        End Select
      End If
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CopiaDatiEsegui(ByVal strSQL As String, _
                                              ByVal bAziendaDestinazioneIsCorrente As Boolean, _
                                              ByVal strAziendaDestinazioneConnectionString As String, _
                                              Optional ByRef dbImportConn As SqlClient.SqlConnection = Nothing, _
                                              Optional ByRef traDbMy As SqlTransaction = Nothing) As Boolean
    Try

      ExecuteAltroDB(strSQL, strAziendaDestinazioneConnectionString, dbImportConn, traDbMy, True)
      Return True

    Catch ex As Exception
      If InStr(ex.ToString(), "SqlException") > 0 Then
        Dim exSQL As SqlException = CType(ex, SqlException)
        If exSQL.Number = 2627 Then Return False
      End If
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaNumeratori(ByVal strTabella As String, _
                                                 ByVal bAziendaOrigineIsCorrente As Boolean, _
                                                 ByVal strAziendaOrigineConnectionString As String, _
                                                 ByVal bAziendaDestinazioneIsCorrente As Boolean, _
                                                 ByVal strAziendaDestinazioneConnectionString As String, _
                                                 ByVal bOverwrite As Boolean) As Boolean
    Dim strSQL As String = ""
    Try
      If strTabella = "anagen" Then
        If Not ImportaDeleteTabellaSingoleRighe("tabnumg", _
                                                bAziendaDestinazioneIsCorrente, _
                                                strAziendaDestinazioneConnectionString, _
                                                "tb_numtipog = 'AG' AND tb_numserieg = ' '") Then
          Return False
        End If
      Else
        If Not ImportaDeleteTabellaSingoleRighe("tabnumg", _
                                                bAziendaDestinazioneIsCorrente, _
                                                strAziendaDestinazioneConnectionString, _
                                                "tb_numtipog = 'SS' AND tb_numserieg = ' '") Then
          Return False
        End If
      End If

      If strTabella = "anagen" Then
        strSQL = "tb_numtipog = 'AG' AND tb_numserieg = ' '"
      Else
        strSQL = "tb_numtipog = 'SS' AND tb_numserieg = ' '"
      End If
      If Not ImportaDaAltroDb("tabnumg", bAziendaOrigineIsCorrente, strAziendaOrigineConnectionString, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString, strSQL, bOverwrite, False) Then
        Return False
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function RicreaStoredProcedure(ByVal strDbconn As String, ByVal bPrc As Boolean) As Boolean
    'aggiorno le stored procedure/viste
    'cosa succede se una sp chiama una sp che ancora non è stata creata?
    'dovrei cancellare anche le stored procedure vecchie? BUSCONVS non lo ha mai fatto!!!
    'DA NET 2013 le stored procedure sono in PROVA.MODX + PROVA.MODXV + PROVA.MODXP, non in combo.mdb!!!
    Dim sqlConn As SqlConnection = Nothing
    Dim da As SqlDataAdapter = Nothing
    Dim cmdConn As SqlCommand = Nothing
    Dim strSQLs() As String = Nothing
    Dim strSQL As String = ""
    'Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim dsMod As New DataSet
    Dim dsTmp As New DataSet
    Try
      sqlConn = New SqlConnection(strDbconn)
      sqlConn.Open()
      cmdConn = New SqlCommand("", sqlConn)

      dsMod.ReadXml(My.Application.Info.DirectoryPath & "\" & IIf(bPrc, "ARCPROC", "PROVA").ToString & ".MODX")
      If System.IO.File.Exists(My.Application.Info.DirectoryPath & "\" & IIf(bPrc, "ARCPROC", "PROVA").ToString & ".MODXV") Then
        dsTmp.ReadXml(My.Application.Info.DirectoryPath & "\" & IIf(bPrc, "ARCPROC", "PROVA").ToString & ".MODXV")
        If dsTmp.Tables.Contains("STPROC") Then
          For Each dtrT As DataRow In dsTmp.Tables("STPROC").Rows
            'se ho cambiato una stored procedure, prima rimuovo la vecchia
            For Each dtrTx As DataRow In dsMod.Tables("STPROC").Select("sp_nome = '" & dtrT!sp_nome.ToString & "'")
              dtrTx.Delete()
            Next
            dsMod.Tables("STPROC").ImportRow(dtrT)
          Next
        End If
      End If
      If System.IO.File.Exists(My.Application.Info.DirectoryPath & "\" & IIf(bPrc, "ARCPROC", "PROVA").ToString & ".MODXP") Then
        dsTmp.ReadXml(My.Application.Info.DirectoryPath & "\" & IIf(bPrc, "ARCPROC", "PROVA").ToString & ".MODXP")
        If dsTmp.Tables.Contains("STPROC") Then
          For Each dtrT As DataRow In dsTmp.Tables("STPROC").Rows
            'se ho cambiato una stored procedure, prima rimuovo la vecchia
            For Each dtrTx As DataRow In dsMod.Tables("STPROC").Select("sp_nome = '" & dtrT!sp_nome.ToString & "'")
              dtrTx.Delete()
            Next
            dsMod.Tables("STPROC").ImportRow(dtrT)
          Next
        End If
      End If

      'strSQL = "SELECT * FROM stproc"
      'da = New SqlDataAdapter(strSQL, sqlConn)
      'da.Fill(dttTmp)

      For Each dtrT As DataRow In dsMod.Tables("STPROC").Rows
        strSQLs = SostituisciGo(dtrT!sp_testo.ToString).Split("¢"c)
        For i = 0 To strSQLs.Length - 1
          cmdConn.CommandText = strSQLs(i)
          If cmdConn.CommandText.Trim <> "" Then cmdConn.ExecuteNonQuery()
        Next
      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dsTmp.Clear()
      dsMod.Clear()
      'dttTmp.Clear()
      If Not cmdConn Is Nothing Then cmdConn.Dispose()

      If sqlConn.State = ConnectionState.Open Then sqlConn.Close()
      sqlConn.Dispose()
    End Try
  End Function
  Public Overridable Function SostituisciGo(ByVal strIn As String) As String
    Return strIn.Trim.Replace(vbCrLf & "GO", "¢").Replace(vbCrLf & "go", "¢").Replace(vbCrLf & "Go", "¢").Replace(vbCrLf & "gO", "¢")
  End Function

  Public Overridable Function CancellaDB(ByVal strConn As String) As Boolean
    Dim strSQL As String = ""
    Dim strTmp As String = ""
    Dim strDbName As String = ""
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim cn As SqlConnection = Nothing
    Dim cmd As SqlCommand = Nothing

    Try
      'dev aprire il DB master, non il db effettivo da cancellare
      strTmp = strConn
      i = strConn.ToUpper.IndexOf("DATABASE=")
      l = strConn.IndexOf(";", i + 1)
      strTmp = strConn.Substring(0, i + 9) & "master" & strConn.Substring(l)
      strDbName = strConn.Substring(i + 9, l - i - 9).Trim

      cn = New SqlConnection(strTmp)
      cn.Open()
      cmd = New SqlCommand(strSQL, cn)

      'se ero entrato in bus sul db da cancellare, poi avevo fatto il cambio del DB ed ora cerco di cancellarlo
      'darebbe errore di DB ancora in uso. lo imposto in single user così chiudo tutte le connessioni aperte in cache sul DB
      strSQL = "ALTER DATABASE [" & strDbName & "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE "
      cmd.CommandText = strSQL
      cmd.ExecuteNonQuery()

      strSQL = "ALTER DATABASE [" & strDbName & "] SET MULTI_USER "
      cmd.CommandText = strSQL
      cmd.ExecuteNonQuery()

      strSQL = "DROP DATABASE [" & strDbName & "]"
      cmd.CommandText = strSQL
      cmd.ExecuteNonQuery()
      cmd.Dispose()
      cn.Close()
      cn = Nothing

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      If Not cn Is Nothing Then
        If cn.State = ConnectionState.Open Then cn.Close()
        cn = Nothing
      End If
    End Try
  End Function
  Public Overridable Function CollegaDB(ByVal strConn As String, ByVal strFileMdf As String, ByVal strFileLdf As String) As Boolean
    Dim strSQL As String = ""
    Dim strTmp As String = ""
    Dim strDbName As String = ""
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim cn As SqlConnection = Nothing
    Dim cmd As SqlCommand = Nothing

    Try
      'deve aprire il DB master, non il db effettivo
      strTmp = strConn
      i = strConn.ToUpper.IndexOf("DATABASE=")
      l = strConn.IndexOf(";", i + 1)
      strTmp = strConn.Substring(0, i + 9) & "master" & strConn.Substring(l)
      strDbName = strConn.Substring(i + 9, l - i - 9).Trim

      cn = New SqlConnection(strTmp)

      strSQL = "sp_attach_db N'" & strDbName & "', N'" & strFileMdf & "'"
      If strFileLdf.Trim <> "" Then strSQL += ", N'" & strFileLdf & "' "

      cn.Open()
      cmd = New SqlCommand(strSQL, cn)
      cmd.ExecuteNonQuery()
      cmd.Dispose()
      cn.Close()
      cn = Nothing

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      If Not cn Is Nothing Then
        If cn.State = ConnectionState.Open Then cn.Close()
        cn = Nothing
      End If
    End Try
  End Function

  Public Overridable Function GetDefaultDataDir(ByVal strConn As String, ByRef strDirOut As String) As Boolean
    'dato il server sql ottengo la direcotry predefinita per il database
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strDbName As String = ""
    Dim cn As SqlConnection = Nothing
    Dim da As SqlDataAdapter = Nothing
    Dim strTmp() As String = Nothing
    Dim i As Integer = 0

    Try
      strDirOut = ""

      'sostituisco il database con 'MASTER'
      strTmp = strConn.Split(";"c)
      strConn = ""
      For i = 0 To strTmp.Length - 1
        If strTmp(i).PadRight(9).Substring(0, 9).ToUpper = "DATABASE=" Then
          strConn += "Database=master;"
        Else
          strConn += strTmp(i) & ";"
        End If
      Next

      cn = New SqlConnection(strConn)
      cn.Open()
      strSQL = "master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'DefaultData'"
      da = New SqlDataAdapter(strSQL, cn)
      da.Fill(dttTmp)
      If dttTmp.Rows.Count = 0 Then
        'su alcuni PC non viene restituito nulla. uso il vecchio sistema
        strSQL = "SELECT * FROM master..sysdatabases WHERE name LIKE 'master'"
        da = New SqlDataAdapter(strSQL, cn)
        da.Fill(dttTmp)
        strDirOut = dttTmp.Rows(0)!filename.ToString.ToUpper.Replace("MASTER.MDF", "")
      Else
        strDirOut = NTSCStr(dttTmp.Rows(0)!data)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      If Not da Is Nothing Then
        da.Dispose()
        da = Nothing
      End If
      If Not cn Is Nothing Then
        If cn.State = ConnectionState.Open Then cn.Close()
        cn = Nothing
      End If
    End Try
  End Function


  Public Overridable Function CheckPermessiSqlServerUser(ByVal strConn As String) As Boolean
    Dim strSQL As String = ""
    Dim strTmp As String = ""
    Dim strDbName As String = ""
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim cn As SqlConnection = Nothing
    Dim da As SqlDataAdapter = Nothing
    Dim dttTmp As New DataTable

    Try
      'deve aprire il DB master, non il db effettivo
      strTmp = strConn
      i = strConn.ToUpper.IndexOf("DATABASE=")
      l = strConn.IndexOf(";", i + 1)
      strTmp = strConn.Substring(0, i + 9) & "master" & strConn.Substring(l)
      strDbName = strConn.Substring(i + 9, l - i - 9).Trim

      cn = New SqlConnection(strTmp)
      cn.Open()

      'verifico se sql server 2000
      strSQL = "SELECT cmptlevel FROM master.dbo.sysdatabases WHERE name = 'msdb'"
      da = New SqlDataAdapter(strSQL, cn)
      da.Fill(dttTmp)

      If dttTmp.Rows.Count > 0 Then
        If Not dttTmp.Rows(0)!cmptlevel Is Nothing Then
          If NTSCInt(dttTmp.Rows(0)!cmptlevel) = 80 Then
            'sql server 2000
            cn.Close()
            Return True
          End If
        End If
      End If
      dttTmp.Clear()

      strSQL = "SELECT HAS_PERMS_BY_NAME(null, null, 'CREATE ANY DATABASE') AS Permesso"
      da.SelectCommand.CommandText = strSQL
      da.Fill(dttTmp)
      If dttTmp.Rows.Count > 0 Then
        If NTSCInt(dttTmp.Rows(0)!Permesso) = 0 Then
          cn.Close()
          Return False
        End If
      End If
      dttTmp.Clear()

      cn.Close()
      cn = Nothing

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      If Not cn Is Nothing Then
        If cn.State = ConnectionState.Open Then cn.Close()
        cn = Nothing
      End If
    End Try
  End Function


  Public Overridable Function CheckSQLServerDir(ByVal strConn As String, ByVal strDir As String, ByVal strFile As String) As Boolean
    'dato il server sql verifico se la directory (o il file) esiste sul server sql
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim strDbName As String = ""
    Dim cn As SqlConnection = Nothing
    Dim da As SqlDataAdapter = Nothing
    Dim strTmp() As String = Nothing
    Dim i As Integer = 0

    Try
      'sostituisco il database con 'MASTER'
      strTmp = strConn.Split(";"c)
      strConn = ""
      For i = 0 To strTmp.Length - 1
        If strTmp(i).PadRight(9).Substring(0, 9).ToUpper = "DATABASE=" Then
          strConn += "Database=master;"
        Else
          strConn += strTmp(i) & ";"
        End If
      Next

      cn = New SqlConnection(strConn)
      cn.Open()
      If strFile = "" Then
        'controllo se la dir esiste
        If strDir.EndsWith("\") = False Then strDir += "\"
        strSQL = "master.dbo.xp_fileexist " & CStrSQL(strDir)
      Else
        'controllo se il file esiste
        strSQL = "master.dbo.xp_fileexist " & CStrSQL(strFile)
      End If

      da = New SqlDataAdapter(strSQL, cn)
      da.Fill(dttTmp)
      If dttTmp.Rows.Count = 0 Then Return False
      If strFile = "" Then
        If NTSCInt(dttTmp.Rows(0)(1)) <> 1 Then Return False 'dir inesistente
      Else
        If NTSCInt(dttTmp.Rows(0)(0)) <> 1 Then Return False 'file inesistente
      End If
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      If Not da Is Nothing Then
        da.Dispose()
        da = Nothing
      End If
      If Not cn Is Nothing Then
        If cn.State = ConnectionState.Open Then cn.Close()
        cn = Nothing
      End If
    End Try
  End Function

End Class
