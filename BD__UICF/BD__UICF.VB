Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLD__UICF
  Inherits CLD__BASE

  Public Overridable Function GetChilds(ByVal nTipo As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      Return GetChilds(nTipo, dttOut, "*")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetChilds(ByVal nTipo As Integer, ByRef dttOut As DataTable, ByVal strChild As String) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nTipo, dttOut, strChild})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(1), DataTable)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------


      If strChild = "" Then
        'carico solo l'elenco dei programmi (il primo ramo del treeview)
      Else
        'se '*' carico tutti i programmi, tutti i child, tutto di tutto (vecchio sistema fino a net 2013)
        'altrimenti carico solo i sottonodi del child passato in input
      End If

      If strChild = "" Then
        strSQL = "SELECT ui_child, '' as ui_form, '' as ui_ctrlname, '' as ui_gridcol, '' as ui_comboitem "
      Else
        strSQL = "SELECT ui_child, ui_form, ui_ctrlname, ui_gridcol, ui_comboitem "
      End If
      strSQL += " FROM uiconf "
      If nTipo <> 0 Then
        Select Case nTipo
          Case 1 'solo controlli aggiunti e posizionamento in form
            strSQL += "WHERE ui_nomprop IN ('EXT', 'ZTOP', 'ZLEFT', 'ZHEIGHT', 'ZWIDTH', 'ZDOCK', 'ZANCHOR', 'ZZMULTILINE', 'ZZAUTOSIZE', 'ZBORDER', 'ZVISLABEL', 'ZPARAMS', 'ZZPARENT') "
          Case 2 'solo traduzioni in lingua
            strSQL += "WHERE ui_codling <> 0"
          Case 3 'solo Text, Bold, Out-not-equal
            strSQL += "WHERE ui_nomprop IN ('BOLD', 'TEXT', 'ERRORTEXT', 'OUTNOTEQUAL')"
          Case 4 'solo Visible, Enable
            strSQL += "WHERE ui_nomprop IN ('ENABLE', 'VISIBLE')"
          Case 5 'solo Default, Checked
            strSQL += "WHERE ui_nomprop IN ('DEFAULT', 'CHECKED')"
        End Select
      End If
      If strChild <> "" And strChild <> "*" Then
        strSQL += IIf(nTipo <> 0, " AND", " WHERE").ToString & " ui_child = " & CStrSQL(strChild)
      End If
      If strChild = "" Then
        strSQL += " GROUP BY ui_child" & _
                  " ORDER BY ui_child"
      Else
        strSQL += " GROUP BY ui_child, ui_form, ui_ctrlname, ui_gridcol, ui_comboitem " & _
                  " ORDER BY ui_child, ui_form, ui_ctrlname, ui_gridcol, ui_comboitem "
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetData(ByVal strChild As String, ByVal strForm As String, _
                                      ByVal strCtrl As String, ByVal strGridCol As String, _
                                      ByVal strCmbItem As String, ByVal nTipo As Integer, _
                                      ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try

      'non devo tirare su la colonna ui_progr (contatore) diversamente da errore nella query di update
      strSQL = "SELECT ui_db, ui_ditta, ui_child, ui_form, ui_tipodoc, ui_ruolo, " & _
               "ui_opnome, ui_codling, ui_ctrltype, ui_ctrlname, ui_gridcol, " & _
               "ui_comboitem, ui_nomprop, ui_valprop, ui_usascript, ui_script, ui_parent " & _
               " FROM uiconf " & _
               " WHERE ui_child = " & CStrSQL(strChild) & _
               " AND ui_form = " & CStrSQL(strForm) & _
               " AND ui_ctrlname = " & CStrSQL(strCtrl) & _
               " AND ui_gridcol = " & CStrSQL(strGridCol) & _
               " AND ui_comboitem = " & CStrSQL(strCmbItem)
      If nTipo <> 0 Then
        Select Case nTipo
          Case 1 'solo controlli aggiunti e posizionamento in form
            strSQL += "AND ui_nomprop IN ('EXT', 'ZTOP', 'ZLEFT', 'ZHEIGHT', 'ZWIDTH', 'ZDOCK', 'ZANCHOR', 'ZZMULTILINE', 'ZZAUTOSIZE', 'ZBORDER', 'ZVISLABEL', 'ZPARAMS', 'ZZPARENT') "
          Case 2 'solo traduzioni in lingua
            strSQL += "AND ui_codling <> 0"
          Case 3 'solo Text, Bold, Out-not-equal
            strSQL += "AND ui_nomprop IN ('BOLD', 'TEXT', 'ERRORTEXT', 'OUTNOTEQUAL')"
          Case 4 'solo Visible, Enable
            strSQL += "AND ui_nomprop IN ('ENABLE', 'VISIBLE')"
          Case 5 'solo Default, Checked
            strSQL += "AND ui_nomprop IN ('DEFAULT', 'CHECKED')"
        End Select
      End If

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "UICONF")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SaveData(ByRef dsUiconf As DataSet) As Boolean
    Dim dtrChange() As DataRow
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer

    Try
      '-----------------------------
      'prima le delete
      dtrChange = dsUiconf.Tables("UICONF").Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1
        strSQL = "DELETE FROM uiconf WHERE " & _
                 "ui_child = '" & dtrChange(i)("ui_child", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_form = '" & dtrChange(i)("ui_form", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_ctrlname = '" & dtrChange(i)("ui_ctrlname", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_gridcol = '" & dtrChange(i)("ui_gridcol", DataRowVersion.Original).ToString().ToLower & "' AND " & _
                 "ui_comboitem = '" & dtrChange(i)("ui_comboitem", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_codling = " & dtrChange(i)("ui_codling", DataRowVersion.Original).ToString() & " AND " & _
                 "ui_nomprop = '" & dtrChange(i)("ui_nomprop", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_db = '" & dtrChange(i)("ui_db", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_ditta = '" & dtrChange(i)("ui_ditta", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_tipodoc = '" & dtrChange(i)("ui_tipodoc", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_ruolo = '" & dtrChange(i)("ui_ruolo", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_opnome = '" & dtrChange(i)("ui_opnome", DataRowVersion.Original).ToString() & "'"
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'adesso le insert
      dtrChange = dsUiconf.Tables("UICONF").Select(Nothing, Nothing, DataViewRowState.Added)
      If dtrChange.Length > 0 Then strSQL = "INSERT INTO UICONF " & GetQueryInsertField(dsUiconf.Tables("UICONF"), "ui_")
      For i = 0 To dtrChange.Length - 1
        strSQLVal = GetQueryInsertValue(dsUiconf.Tables("UICONF"), dtrChange(i), "ui_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBPRC)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'infine le update
      dtrChange = dsUiconf.Tables("UICONF").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1
        strSQL = "UPDATE UICONF SET " & GetQueryUpdate(dsUiconf.Tables("UICONF"), dtrChange(i), "ui_")
        strSQL += " WHERE " & _
                 "ui_child = '" & dtrChange(i)("ui_child", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_form = '" & dtrChange(i)("ui_form", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_ctrlname = '" & dtrChange(i)("ui_ctrlname", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_gridcol = '" & dtrChange(i)("ui_gridcol", DataRowVersion.Original).ToString().ToLower & "' AND " & _
                 "ui_comboitem = '" & dtrChange(i)("ui_comboitem", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_codling = " & dtrChange(i)("ui_codling", DataRowVersion.Original).ToString() & " AND " & _
                 "ui_nomprop = '" & dtrChange(i)("ui_nomprop", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_db = '" & dtrChange(i)("ui_db", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_ditta = '" & dtrChange(i)("ui_ditta", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_tipodoc = '" & dtrChange(i)("ui_tipodoc", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_ruolo = '" & dtrChange(i)("ui_ruolo", DataRowVersion.Original).ToString() & "' AND " & _
                 "ui_opnome = '" & dtrChange(i)("ui_opnome", DataRowVersion.Original).ToString() & "'"
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        dtrChange(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaCartella(ByVal strChild As String, ByVal strForm As String, _
                                                ByVal strCtrl As String, ByVal strGridCol As String, _
                                                ByVal strCmbItem As String) As Boolean
    Dim strSQL As String = ""
    Try
      If strChild = "" Then Return True
      strSQL = "DELETE FROM uiconf WHERE ui_child = " & CStrSQL(strChild)
      If strForm <> "" Then strSQL += " AND ui_form = " & CStrSQL(strForm)
      If strCtrl <> "" Then strSQL += " AND ui_ctrlname = " & CStrSQL(strCtrl)
      If strGridCol <> "" Then strSQL += " AND ui_gridcol = " & CStrSQL(strGridCol)
      If strCmbItem <> "" Then strSQL += " AND ui_comboitem = " & CStrSQL(strCmbItem)

      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetCartellaPerExport(ByVal strChild As String, ByVal strForm As String, _
                                              ByVal strCtrl As String, ByVal strGridCol As String, _
                                              ByVal strCmbItem As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      If strChild = "" Then Return True
      strSQL = "SELECT * FROM uiconf " & _
               " WHERE substring(ui_nomprop, 1, 2) <> 'F2'" & _
               " AND ui_child = " & CStrSQL(strChild)
      If strForm <> "" Then strSQL += " AND ui_form = " & CStrSQL(strForm)
      If strCtrl <> "" Then strSQL += " AND ui_ctrlname = " & CStrSQL(strCtrl)
      If strGridCol <> "" Then strSQL += " AND ui_gridcol = " & CStrSQL(strGridCol)
      If strCmbItem <> "" Then strSQL += " AND ui_comboitem = " & CStrSQL(strCmbItem)

      strSQL &= " ORDER BY ui_ctrlname, ui_gridcol, ui_nomprop"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetCartellaGriglia(ByVal strChild As String, ByVal strForm As String, _
                                              ByVal strCtrl As String, ByVal strGridCol As String, _
                                              ByVal strCmbItem As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      If strChild = "" Then Return True
      strSQL = "SELECT * FROM uiconf " & _
               " WHERE substring(ui_nomprop, 1, 2) = 'F2'" & _
               " AND ui_child = " & CStrSQL(strChild)
      If strForm <> "" Then strSQL += " AND ui_form = " & CStrSQL(strForm)
      If strCtrl <> "" Then strSQL += " AND ui_ctrlname = " & CStrSQL(strCtrl)
      If strGridCol <> "" Then strSQL += " AND ui_gridcol = " & CStrSQL(strGridCol)
      If strCmbItem <> "" Then strSQL += " AND ui_comboitem = " & CStrSQL(strCmbItem)

      strSQL &= " ORDER BY ui_ctrlname, ui_gridcol, ui_nomprop"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function ImportaImpostazioni(ByRef dttIn As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim dbConn As DbConnection = Nothing
    Dim oDttgr As New CLEGROUPBY
    Dim dttGr As New DataTable
    Dim strSQLVal As String = ""
    Dim i As Integer = 0

    Try
      If dttIn.Columns.Contains("ui_progr") Then
        dttIn.Columns("ui_progr").AutoIncrement = True
        dtrT = dttIn.Select("", "ui_progr")
      Else
        dtrT = dttIn.Select("")
      End If


      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBPRC)
      ApriTrans(dbConn)

      '--------------------------------
      'prima cancello le impostazioni
      'non cancello le impostazioni di griglia (CTRL+ALT+F2) e i salvataggi dei formati di raggruppameto griglia
      'F2ZORDER, F2SORT, F2WIDTH, F2VISIBLE, F2ENABLE, F2HEIGHT
      'dovrei cancellare anche le impostazioni delle colonne di tipo EXT, visto che poi verrano cancellate ... per adesso ignoro
      'cchio: con questa query non cancello le precedenti creazioni di colonne di griglia EXT
      oDttgr.NTSGroupBy(dttIn, dttGr, "ui_child, ui_form, ui_ctrlname", "", "ui_child, ui_form, ui_ctrlname")
      For i = 0 To dttGr.Rows.Count - 1
        With dttGr.Rows(i)
          strSQL = "DELETE FROM uiconf  " & _
                   " WHERE ui_child = " & CStrSQL(!ui_child) & _
                   " AND ui_form = " & CStrSQL(!ui_form) & _
                   " AND ui_ctrlname = " & CStrSQL(!ui_ctrlname) & _
                   " AND ui_nomprop NOT LIKE 'GROUPBOX%' " & _
                   " AND ui_nomprop NOT LIKE 'F2%'"
        End With
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC, dbConn)
      Next
      dttGr.Clear()

      '--------------------------------
      'ora inserisco tutto
      strSQL = "INSERT INTO UICONF " & GetQueryInsertField(dttIn, "ui_")
      For i = 0 To dtrT.Length - 1
        If NTSCStr(dtrT(i)!ui_db) = "" Then dtrT(i)!ui_db = " "
        If NTSCStr(dtrT(i)!ui_ditta) = "" Then dtrT(i)!ui_ditta = " "
        If NTSCStr(dtrT(i)!ui_tipodoc) = "" Then dtrT(i)!ui_tipodoc = " "
        If NTSCStr(dtrT(i)!ui_ruolo) = "" Then dtrT(i)!ui_ruolo = " "
        If NTSCStr(dtrT(i)!ui_opnome) = "" Then dtrT(i)!ui_opnome = " "
        If NTSCStr(dtrT(i)!ui_valprop) = "" Then dtrT(i)!ui_valprop = " "
        If NTSCStr(dtrT(i)!ui_script) = "" Then dtrT(i)!ui_script = " "
        If NTSCStr(dtrT(i)!ui_parent) = "" Then dtrT(i)!ui_parent = " "

        strSQLVal = GetQueryInsertValue(dttIn, dtrT(i), "ui_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBPRC, dbConn)
      Next

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()

      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDllPersMenupop(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mnProgr FROM menupop WHERE mnLiv1 = 17"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDllPersGreg(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT rp_nomprop, rp_valprop FROM regprop " & _
               " WHERE rp_liv1 = 'OPZIONI' " & _
               " AND (rp_nomprop LIKE 'CHILD_%' OR rp_nomprop LIKE 'ZOOM%')"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function DeletePersGregUiConf() As Boolean
    'cancello tutte le personalizzazioni: mantengo solo il vecchi ctrl+alt+f2

    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "DELETE FROM regprop " & _
               " WHERE rp_liv1 = 'OPZIONI' " & _
               " AND (rp_nomprop LIKE 'CHILD_%' OR rp_nomprop LIKE 'ZOOM%')"
      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      'per le colonne di griglia aggiunte, devo cancellare anche la personalizzaz. di griglia CTRL+ALT+F2
      strSQL = "SELECT * FROM uiconf WHERE ui_nomprop = 'EXT' AND ui_ctrltype = 'NTSInformatica.NTSGridColumn'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      For Each dtrT As DataRow In dttTmp.Rows
        strSQL = "DELETE FROM uiconf WHERE ui_ctrlname = " & CStrSQL(dtrT!ui_ctrlname)
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      Next

      strSQL = "DELETE FROM uiconf WHERE substring(ui_nomprop, 1, 2) <> 'F2' AND ui_nomprop NOT LIKE 'GROUPBOX%'"
      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      strSQL = "DELETE FROM menupop WHERE mnLiv1 = 17"
      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetPersGregUiconf(ByRef dttGreg As DataTable, ByRef dttUicf As DataTable, ByRef dttMenu As DataTable) As Boolean
    'esporto tutte le personalizzazioni: mantengo solo il vecchi ctrl+alt+f2
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM regprop " & _
               " WHERE rp_liv1 = 'OPZIONI' " & _
               " AND (rp_nomprop LIKE 'CHILD_%' OR rp_nomprop LIKE 'ZOOM%')"
      dttGreg = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      strSQL = "SELECT * FROM uiconf WHERE substring(ui_nomprop, 1, 2) <> 'F2' AND ui_nomprop NOT LIKE 'GROUPBOX%'"
      dttUicf = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      strSQL = "SELECT * FROM menupop WHERE mnLiv1 = 17"
      dttMenu = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SavePresGregUiconf(ByRef dttGreg As DataTable, ByRef dttUicf As DataTable, ByRef dttMenu As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer = 0
    Try
      If dttGreg.Rows.Count > 0 Then
        Try
          strSQL = "INSERT INTO regdir (rd_liv1, rd_liv2, rd_liv3, rd_diput, rd_dipaz, rd_diptdoc) VALUES ('OPZIONI', '.', '.', 'N', 'N', 'N')"
          Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        Catch
          'il record potrebbe già esserci: ignoro l'errore
        End Try
        strSQL = "INSERT INTO REGPROP " & GetQueryInsertField(dttGreg, "rp_")
        For i = 0 To dttGreg.Rows.Count - 1
          strSQLVal = GetQueryInsertValue(dttGreg, dttGreg.Rows(i), "rp_")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBPRC)
        Next
      End If    'If dttGreg.Rows.Count > 0 Then

      If dttUicf.Rows.Count > 0 Then
        strSQL = "INSERT INTO UICONF " & GetQueryInsertField(dttUicf, "ui_", "ui_progr")
        For i = 0 To dttUicf.Rows.Count - 1
          strSQLVal = GetQueryInsertValue(dttUicf, dttUicf.Rows(i), "ui_", "ui_progr")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBPRC)
        Next
      End If    'If dttUicf.Rows.Count > 0 Then

      If dttMenu.Rows.Count > 0 Then
        strSQL = "INSERT INTO MENUPOP " & GetQueryInsertField(dttMenu, "mn", "")
        For i = 0 To dttMenu.Rows.Count - 1
          strSQLVal = GetQueryInsertValue(dttMenu, dttMenu.Rows(i), "mn", "")
          Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBPRC)
        Next
      End If    'If dttMenu.Rows.Count > 0 Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrasferisciConfigurazione(ByVal strPrgDa As String, ByVal strPrgA As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO UICONF (ui_db, ui_ditta, ui_child, ui_form, ui_tipodoc, ui_ruolo, ui_opnome, ui_codling, ui_ctrltype, ui_ctrlname, " & _
               "                    ui_gridcol, ui_comboitem, ui_nomprop, ui_valprop, ui_usascript, ui_script, ui_parent)" & _
               " SELECT ui_db, ui_ditta, " & CStrSQL(strPrgA) & ", ui_form, ui_tipodoc, ui_ruolo, ui_opnome, ui_codling, ui_ctrltype, ui_ctrlname, ui_gridcol, " & _
               "        ui_comboitem, ui_nomprop, ui_valprop, ui_usascript, ui_script, ui_parent " & _
               " FROM UICONF " & _
               " WHERE ui_child = " & CStrSQL(strPrgDa) & _
               " AND NOT EXISTS (SELECT ui_child FROM UICONF AS UICONF2 " & _
               "				         WHERE UICONF2.ui_db = UICONF.ui_db AND " & _
               "		        		 UICONF2.ui_ditta = UICONF.ui_ditta AND " & _
               "		        		 UICONF2.ui_child = " & CStrSQL(strPrgA) & " AND " & _
               "		        		 UICONF2.ui_form = UICONF.ui_form AND " & _
               "		        		 UICONF2.ui_tipodoc = UICONF.ui_tipodoc AND " & _
               "		        		 UICONF2.ui_ruolo = UICONF.ui_ruolo AND " & _
               "		        		 UICONF2.ui_opnome = UICONF.ui_opnome AND " & _
               "		        		 UICONF2.ui_codling = UICONF.ui_codling AND " & _
               "		        		 UICONF2.ui_ctrltype = UICONF.ui_ctrltype AND " & _
               "		        		 UICONF2.ui_ctrlname = UICONF.ui_ctrlname AND " & _
               "		        		 UICONF2.ui_gridcol = UICONF.ui_gridcol AND " & _
               "		        		 UICONF2.ui_comboitem = UICONF.ui_comboitem AND " & _
               "		        		 UICONF2.ui_nomprop = UICONF.ui_nomprop) "

      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
End Class
