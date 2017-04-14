Imports system.data
Imports NTSInformatica.CLN__STD

Public Class CLD__GCTL
  Inherits CLD__BASE

  Public Overridable Function GetTabLingPrc(ByRef dsLingue As DataSet) As Boolean
    Dim strSQL As String = "SELECT * FROM tabling"
    Try

      '-----------------------------
      'chiedo i dati al database
      dsLingue = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "TABLING")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetData(ByVal strChild As String, ByVal strForm As String, ByVal strTipoDoc As String, ByVal strCtrlName As String, ByRef dsUiconf As DataSet) As Boolean
    Dim strSQL As String = ""

    Try

      '-----------------------------
      'chiedo i dati al database
      strSQL = "SELECT ui_db, ui_ditta, ui_child, ui_form, ui_tipodoc, ui_ruolo, ui_opnome, ui_codling, " & _
               " ui_ctrltype, ui_ctrlname, lower(ui_gridcol) as ui_gridcol, ui_comboitem, ui_nomprop, ui_valprop, ui_usascript, " & _
               " ui_script, ui_parent FROM uiconf " & _
               " WHERE ui_form = " & CStrSQL(strForm) & _
               " AND ui_child = " & CStrSQL(strChild) & _
               " AND ui_ctrlname = " & CStrSQL(strCtrlName)
      'faccio vedere sempre tutte le proprietà, indipendentemente dal tipo documento
      '" AND (ui_tipodoc = " & CStrSQL(strTipoDoc) & " OR ui_tipodoc = ' ')" & _

      dsUiconf = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "UICONF")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SaveData(ByRef dsGctl As DataSet) As Boolean
    Dim dtrChange() As DataRow
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer

    Try
      '-----------------------------
      'prima le delete
      dtrChange = dsGctl.Tables("UICONF").Select(Nothing, Nothing, DataViewRowState.Deleted)
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
      dtrChange = dsGctl.Tables("UICONF").Select(Nothing, Nothing, DataViewRowState.Added)
      If dtrChange.Length > 0 Then strSQL = "INSERT INTO UICONF " & GetQueryInsertField(dsGctl.Tables("UICONF"), "ui_")
      For i = 0 To dtrChange.Length - 1
        strSQLVal = GetQueryInsertValue(dsGctl.Tables("UICONF"), dtrChange(i), "ui_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBPRC)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'infine le update
      dtrChange = dsGctl.Tables("UICONF").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1
        strSQL = "UPDATE UICONF SET " & GetQueryUpdate(dsGctl.Tables("UICONF"), dtrChange(i), "ui_")
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

  Public Overridable Function SaveDataPosition(ByVal strChild As String, ByVal strForm As String, ByVal strCtlName As String, ByVal strCtlType As String, _
                                  ByVal strParents As String, ByVal strStandardParents As String, _
                                  ByVal nTop As Integer, ByVal nLeft As Integer, _
                                  ByVal nHeight As Integer, ByVal nWidth As Integer, _
                                  ByVal nAnchor As Integer, ByVal nDock As Integer, _
                                  ByVal bMultil As Boolean, ByVal bAutos As Boolean, _
                                  ByVal bBorder As Boolean, ByVal bCtrlExt As Boolean, _
                                  ByVal bVisLabel As Boolean, ByVal strParams As String) As Boolean

    'multiline gestito/salvato solo per NTSTextBoxStr
    'autosize gestito solo per NTSLabel, NTSCheckBox, NTSRadioButton
    'border gestito solo per NTSLabel
    'bCtrlExt = true vuol dire che il controllo  stato caricato con source ext e, in fase di salvataggio, devonoe 
    '                essere salvati anche ulteriori informazioni per poter precaricare il controllo all'avvio del child

    Dim strSQL As String = ""
    Dim strSQLBase As String = ""
    Dim bForm As Boolean = False

    Try
      If strStandardParents = "*form*" Then
        bForm = True
      End If

      'se il controllo  di tipo EXT, la posizione  -1, -1 e la dimensione  -1, -1 
      'vuol dire che sono in cancellazione del controllo, per cui non devo salvare nulla
      'occhio: per i menu occorre cancellare anche le proprietà inserite nei comboitem
      If bCtrlExt And nTop = -1 And nLeft = -1 And nHeight = -1 And nWidth = -1 Then
        strSQL = "DELETE FROM uiconf WHERE " & _
               "ui_child = '" & strChild & "' AND " & _
               "ui_form = '" & strForm & "' AND " & _
               "( ui_ctrlname = '" & strCtlName & "' OR (ui_gridcol = '.' AND ui_comboitem = '" & strCtlName & "') OR (lower(ui_gridcol) = '" & strCtlName.ToLower & "')) "
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        Return True
      End If

      strSQLBase = "INSERT INTO UICONF (ui_db, ui_ditta, ui_child, ui_form, ui_tipodoc, ui_ruolo, ui_opnome, " & _
            "ui_codling, ui_ctrltype, ui_ctrlname, ui_gridcol, ui_comboitem, ui_nomprop, ui_valprop, " & _
            "ui_usascript, ui_script, ui_parent)"

      'aggiorno il contenitore 'ui_parent di tutte le righe del controllo (bold/visibile/enable/...)
      If strParents <> "" And bForm = False Then
        strSQL = "UPDATE uiconf SET ui_parent = '" & strParents & "' " & _
                 "WHERE ui_child = '" & strChild & "' AND " & _
                 "ui_form = '" & strForm & "' AND " & _
                 "ui_ctrlname = '" & strCtlName & "'"
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      End If
      'aggiorno il contenitore 'ui_parent di tutti i controlli contenuti nel controllo in analisi

      'prima cencello le vecchie impostazioni grafiche (top/left/anchor/...)
      strSQL = "DELETE FROM uiconf WHERE " & _
               "ui_child = '" & strChild & "' AND " & _
               "ui_form = '" & strForm & "' AND " & _
               "ui_ctrlname = '" & strCtlName & "' AND " & _
               "(ui_nomprop = 'ZZPARENT' OR ui_nomprop = 'ZANCHOR' OR ui_nomprop = 'ZDOCK'" & _
               " OR ui_nomprop = 'ZZMULTILINE' OR ui_nomprop = 'ZZAUTOSIZE' Or ui_nomprop = 'ZBORDER' " & _
               " OR ui_nomprop = 'EXT' Or ui_nomprop = 'ZVISLABEL' Or ui_nomprop = 'ZPARAMS'"
      'se cancello e riscrivo la posizione e le dimensioni del controllo quando  dockato verrebbero prese quelle del dock!!!!
      If nDock = 0 Then
        strSQL += "OR ui_nomprop = 'ZTOP' OR ui_nomprop = 'ZLEFT' Or ui_nomprop = 'ZHEIGHT' OR ui_nomprop = 'ZWIDTH') "
      Else
        strSQL += ") "
      End If
      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      'EXT
      If bCtrlExt Then
        strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                "'" & strCtlType & "', " & _
                "'" & strCtlName & "', '.', '.', " & _
                "'EXT', " & CStrSQL(strParams) & ", 'N', ' ', " & _
                "'" & strStandardParents & "')"
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      End If

      If nHeight <> 0 And nWidth <> 0 Then

        'parents
        strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                "'" & strCtlType & "', " & _
                "'" & strCtlName & "', '.', '.', " & _
                "'ZZPARENT', '" & strParents & "', 'N', ' ', " & _
                "'" & strStandardParents & "')"
        If bForm = False Then Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

        'solo per NTSTextBoxStr
        If strCtlType = "NTSInformatica.NTSTextBoxStr" Then
          'multiline
          strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                  "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                  "'" & strCtlType & "', " & _
                  "'" & strCtlName & "', '.', '.', " & _
                  "'ZZMULTILINE', '" & CInt(bMultil) & "', 'N', ' ', " & _
                  "'" & strParents & "')"
          Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        End If

        'solo per NTSComboBox
        If strCtlType = "NTSInformatica.NTSComboBox" Then
          'VisLabel
          strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                  "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                  "'" & strCtlType & "', " & _
                  "'" & strCtlName & "', '.', '.', " & _
                  "'ZVISLABEL', '" & CInt(bVisLabel) & "', 'N', ' ', " & _
                  "'" & strParents & "')"
          Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        End If

        'solo per NTSLabel, NTSPanel, NTSPictureBox
        If strCtlType = "NTSInformatica.NTSLabel" Or strCtlType = "NTSInformatica.NTSPanel" Or strCtlType = "NTSInformatica.NTSPictureBox" Then
          'border
          strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                  "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                  "'" & strCtlType & "', " & _
                  "'" & strCtlName & "', '.', '.', " & _
                  "'ZBORDER', '" & CInt(bBorder) & "', 'N', ' ', " & _
                  "'" & strParents & "')"
          Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        End If

        'solo per NTSLabel o NTSCheckBox o NTSRadioButton
        If strCtlType = "NTSInformatica.NTSLabel" Or _
           strCtlType = "NTSInformatica.NTSCheckBox" Or _
           strCtlType = "NTSInformatica.NTSRadioButton" Then
          'autosize
          strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                  "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                  "'" & strCtlType & "', " & _
                  "'" & strCtlName & "', '.', '.', " & _
                  "'ZZAUTOSIZE', '" & CInt(bAutos) & "', 'N', ' ', " & _
                  "'" & strParents & "')"
          Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        End If

        'salvo le impostazioni top/left/heigth/width solo se non ho dockato il controllo (versamente non manterrebbe le impostazioni iniziali)
        If nDock = 0 Then
          'top
          strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                  "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                  "'" & strCtlType & "', " & _
                  "'" & strCtlName & "', '.', '.', " & _
                  "'ZTOP', '" & nTop.ToString & "', 'N', ' ', " & _
                  "'" & strParents & "')"
          If bForm = False Then Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

          'left
          strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                  "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                  "'" & strCtlType & "', " & _
                  "'" & strCtlName & "', '.', '.', " & _
                  "'ZLEFT', '" & nLeft.ToString & "', 'N', ' ', " & _
                  "'" & strParents & "')"
          If bForm = False Then Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

          'heigth
          strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                  "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                  "'" & strCtlType & "', " & _
                  "'" & strCtlName & "', '.', '.', " & _
                  "'ZHEIGHT', '" & nHeight.ToString & "', 'N', ' ', " & _
                  "'" & strParents & "')"
          Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

          'width
          strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                  "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                  "'" & strCtlType & "', " & _
                  "'" & strCtlName & "', '.', '.', " & _
                  "'ZWIDTH', '" & nWidth.ToString & "', 'N', ' ', " & _
                  "'" & strParents & "')"
          Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        End If    'If nDock = 0 Then

        'anchor (non salvo se il controllo  ancorato nella posizione standard ... 
        'NO diversamente se il controllo, per default,  dockato quando ricarico la form me lo ritrovo ancora dockato)
        'If nAnchor <> 5 Then
        strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                "'" & strCtlType & "', " & _
                "'" & strCtlName & "', '.', '.', " & _
                "'ZANCHOR', '" & nAnchor.ToString & "', 'N', ' ', " & _
                "'" & strParents & "')"
        If bForm = False Then Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        'End If

        'dock (non salvo se il controllo non  dockato, non serve)
        'NO diversamente se il controllo, per default,  dockato quando ricarico la form me lo ritrovo ancora dockato)
        'If nDock <> 0 Then
        strSQL = strSQLBase & " VALUES (' ', ' ', '" & strChild & "', " & _
                "'" & strForm & "', ' ', ' ', ' ', 0, " & _
                "'" & strCtlType & "', " & _
                "'" & strCtlName & "', '.', '.', " & _
                "'ZDOCK', '" & nDock.ToString & "', 'N', ' ', " & _
                "'" & strParents & "')"
        If bForm = False Then Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        'End If
      End If    ' If nHeight <> 0 And nWidth <> 0 Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SaveDataCTRL_ALT_F2(ByVal strOpNome As String, ByVal strGruppo As String, _
                                      ByVal strTipoDoc As String, ByVal bSoloDelete As Boolean, _
                                      ByRef dttConfig As DataTable, ByVal strForm As String, _
                                      ByVal strChild As String, ByVal strGriName As String, _
                                      ByVal strParents As String, ByVal nRowHeight As Integer) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Dim strBase As String = ""
    Try

      '-----------------------------------
      'prima cancello le vecchie impostazioni
      strSQL = "DELETE FROM uiconf WHERE ui_child = " & CStrSQL(strChild) & _
               " AND ui_form = " & CStrSQL(strForm) & _
               " AND ui_tipodoc = " & CStrSQL(strTipoDoc) & _
               " AND ui_opnome = " & CStrSQL(strOpNome) & _
               " AND ui_ruolo = " & CStrSQL(strGruppo) & _
               " AND ui_ctrlname = " & CStrSQL(strGriName)
      If bSoloDelete Then
        'se devo ripristinare le impostazioni iniziali della griglia mantengo larghezza colonne ed alterzza righe
      Else
        'devo mantenere la larghezza delle colonne e l'altezza della riga
        strSQL = strSQL & " AND ui_nomprop <> 'F2WIDTH'"
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      If bSoloDelete Then Return True

      '-----------------------------------
      'adesso salvo il nuovo

      'altezza delle righe
      strSQL = "INSERT INTO uiconf (ui_db, ui_ditta, ui_child, ui_form, ui_tipodoc, ui_ruolo, " & _
               "ui_opnome, ui_codling, ui_ctrltype, ui_ctrlname, ui_gridcol, ui_comboitem, " & _
               "ui_nomprop, ui_valprop, ui_usascript, ui_script, ui_parent) " & _
               "VALUES (' ', ' ', " & CStrSQL(strChild) & ", " & _
               CStrSQL(strForm) & ", " & _
               CStrSQL(strTipoDoc) & ", " & _
               CStrSQL(strGruppo) & ", " & _
               CStrSQL(strOpNome) & ", 0, " & _
               "'NTSInformatica.NTSGridView', " & _
               CStrSQL(strGriName) & ", " & _
               "'.', '.', 'F2HEIGHT', " & _
               CStrSQL(nRowHeight.ToString) & ", 'N', ' ', " & _
               CStrSQL(strParents) & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      For i = 0 To dttConfig.Rows.Count - 1
        'non salvo visible/editable se nel TAG c' 'V' o 'E', altrimenti quando si riabilita la visibilit/editabilit
        'da parte del CTRL+ALT+F4 la colonna rimane ugualmente non visibile/editabile...
        strBase = "INSERT INTO uiconf (ui_db, ui_ditta, ui_child, ui_form, ui_tipodoc, ui_ruolo, " & _
                 "ui_opnome, ui_codling, ui_ctrltype, ui_ctrlname, ui_gridcol, ui_comboitem, " & _
                 "ui_nomprop, ui_valprop, ui_usascript, ui_script, ui_parent) " & _
                 "VALUES (' ', ' ', " & CStrSQL(strChild) & ", " & _
                 CStrSQL(strForm) & ", " & _
                 CStrSQL(strTipoDoc) & ", " & _
                 CStrSQL(strGruppo) & ", " & _
                 CStrSQL(strOpNome) & ", 0, " & _
                 "'NTSInformatica.NTSGridView', " & _
                 CStrSQL(strGriName) & ", " & _
                 CStrSQL(dttConfig.Rows(i)!ui_colname.ToString.ToLower) & ", '.', "
        If dttConfig.Rows(i)!ui_tag.ToString.IndexOf("V") = -1 Then
          strSQL = strBase & "'F2VISIBLE', " & _
                   CStrSQL(dttConfig.Rows(i)!ui_visible.ToString) & ", 'N', ' ', " & _
                   CStrSQL(strParents) & ")"
          Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        End If
        If dttConfig.Rows(i)!ui_tag.ToString.IndexOf("E") = -1 Then
          strSQL = strBase & "'F2ENABLE', " & _
                   CStrSQL(dttConfig.Rows(i)!ui_enable.ToString) & ", 'N', ' ', " & _
                   CStrSQL(strParents) & ")"
          Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        End If
        strSQL = strBase & "'F2ZORDER', " & _
                 CStrSQL(dttConfig.Rows(i)!ui_order.ToString) & ", 'N', ' ', " & _
                 CStrSQL(strParents) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

        If NTSCInt(dttConfig.Rows(i)!ui_colwidth) <> NTSCInt(dttConfig.Rows(i)!ui_colwidthOrig) Then
          strSQL = "DELETE FROM uiconf WHERE ui_db = ' ' AND ui_ditta = ' ' AND ui_child = " & CStrSQL(strChild) & _
                   " AND ui_form = " & CStrSQL(strForm) & " AND ui_tipodoc = " & CStrSQL(strTipoDoc) & _
                   " AND ui_ruolo = " & CStrSQL(strGruppo) & " AND ui_opnome = " & CStrSQL(strOpNome) & _
                   " AND ui_codling = 0 AND ui_ctrltype = 'NTSInformatica.NTSGridView' " & _
                   " AND ui_ctrlname = " & CStrSQL(strGriName) & " AND ui_gridcol = " & CStrSQL(dttConfig.Rows(i)!ui_colname.ToString.ToLower) & _
                   " AND ui_nomprop = 'F2WIDTH'"
          Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

          strSQL = strBase & "'F2WIDTH', " & _
                   CStrSQL(NTSCInt(dttConfig.Rows(i)!ui_colwidth).ToString) & ", 'N', ' ', " & _
                   CStrSQL(strParents) & ")"
          Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        End If
      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return False
    End Try
  End Function


  Public Overridable Function GetDataRemoting(ByRef dsPcconf As DataSet, ByVal nPrior As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      '-----------------------------
      'chiedo i dati al database
      strSQL = "SELECT * FROM pcconf " & _
               " WHERE (pc_nomprop = 'Remoting' OR pc_nomprop = 'RemoteServer' OR pc_nomprop = 'RemotePort') "
      Select Case nPrior
        Case 1          'computer /programma
          strSQL += " AND pc_pcname <> ' ' AND pc_dll <> ' '"
        Case 2          'computer
          strSQL += " AND pc_pcname <> ' ' AND pc_dll = ' '"
        Case 3          'programma
          strSQL += " AND pc_pcname = ' ' AND pc_dll <> ' '"
        Case 4          'generale
          strSQL += " AND pc_pcname = ' ' AND pc_dll = ' '"
      End Select
      dsPcconf = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "PCCONF")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SaveDataRemoting(ByRef dsGctl As DataSet) As Boolean
    Dim dtrChange() As DataRow
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer

    Try
      '-----------------------------
      'prima le delete
      dtrChange = dsGctl.Tables("PCCONF").Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1
        strSQL = "DELETE FROM pcconf WHERE " & _
                 "pc_pcname = '" & dtrChange(i)("pc_pcname", DataRowVersion.Original).ToString() & "' AND " & _
                 "pc_dll = '" & dtrChange(i)("pc_dll", DataRowVersion.Original).ToString() & "' AND " & _
                 "pc_nomprop = '" & dtrChange(i)("pc_nomprop", DataRowVersion.Original).ToString() & "'"
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'adesso le insert
      dtrChange = dsGctl.Tables("PCCONF").Select(Nothing, Nothing, DataViewRowState.Added)
      If dtrChange.Length > 0 Then strSQL = "INSERT INTO PCCONF " & GetQueryInsertField(dsGctl.Tables("PCCONF"), "pc_", "pc_progr")
      For i = 0 To dtrChange.Length - 1
        strSQLVal = GetQueryInsertValue(dsGctl.Tables("PCCONF"), dtrChange(i), "pc_", "pc_progr")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBPRC)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'infine le update
      dtrChange = dsGctl.Tables("PCCONF").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1
        strSQL = "UPDATE PCCONF SET " & GetQueryUpdate(dsGctl.Tables("PCCONF"), dtrChange(i), "pc_", "pc_progr")
        strSQL += " WHERE " & _
                 "pc_pcname = '" & dtrChange(i)("pc_pcname", DataRowVersion.Original).ToString() & "' AND " & _
                 "pc_dll = '" & dtrChange(i)("pc_dll", DataRowVersion.Original).ToString() & "' AND " & _
                 "pc_nomprop = '" & dtrChange(i)("pc_nomprop", DataRowVersion.Original).ToString() & "'"
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

End Class
