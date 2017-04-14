Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLE__UICF
  Inherits CLE__BASN
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property


  Public oCldUicf As CLD__UICF


  Public bHasChanges As Boolean
  Public dsShared As DataSet
  Public dttTrv As New DataTable        'tabella contenente i childs del treeview(programma, form)
  Public nTipo As Integer = 1

  Public dttFileOrigMancanti As New DataTable

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__UICF"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldUicf = CType(MyBase.ocldBase, CLD__UICF)
    oCldUicf.Init(oApp)
    Return True
  End Function

  Public Overridable Function CreaTreeview() As Boolean
    Try
      If Not CreaTreeview("*") Then Return False

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function CreaTreeview(ByVal strChild As String) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strChild})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      If Not oCldUicf.GetChilds(nTipo, dttTrv, strChild) Then Return False

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function Apri(ByVal strChild As String, ByVal strForm As String, _
                                  ByVal strCtrl As String, ByVal strGridCol As String, _
                                  ByVal strCmbItem As String, ByRef dsUicf As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      If Not oCldUicf.GetData(strChild, strForm, strCtrl, strGridCol, strCmbItem, nTipo, dsUicf) Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldUicf.SetTableDefaultValueFromDB("UICONF", dsUicf)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      SetDefaultValue(dsUicf, strChild, strForm, strCtrl, strGridCol, strCmbItem)
      dsShared = dsUicf

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("UICONF").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("UICONF").ColumnChanged, AddressOf AfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsShared.Tables("UICONF").AcceptChanges()
      bHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SetDefaultValue(ByRef ds As DataSet, _
                                  ByVal strChild As String, ByVal strForm As String, _
                                  ByVal strCtrl As String, ByVal strGridCol As String, _
                                  ByVal strCmbItem As String)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database

      'ds.Tables("UICONF").Columns("ui_db").DefaultValue = 
      'ds.Tables("UICONF").Columns("ui_ditta").DefaultValue = 
      ds.Tables("UICONF").Columns("ui_child").DefaultValue = strChild
      ds.Tables("UICONF").Columns("ui_form").DefaultValue = strForm
      'ds.Tables("UICONF").Columns("ui_tipodoc").DefaultValue = 
      'ds.Tables("UICONF").Columns("ui_ruolo").DefaultValue = 
      'ds.Tables("UICONF").Columns("ui_opnome").DefaultValue = 
      'ds.Tables("UICONF").Columns("ui_codling").DefaultValue = 
      'ds.Tables("UICONF").Columns("ui_ctrltype").DefaultValue = 
      ds.Tables("UICONF").Columns("ui_ctrlname").DefaultValue = strCtrl
      ds.Tables("UICONF").Columns("ui_gridcol").DefaultValue = strGridCol
      ds.Tables("UICONF").Columns("ui_comboitem").DefaultValue = strCmbItem
      'ds.Tables("UICONF").Columns("ui_nomprop").DefaultValue = 
      'ds.Tables("UICONF").Columns("ui_valprop").DefaultValue = 
      'ds.Tables("UICONF").Columns("ui_usascript").DefaultValue = 
      'ds.Tables("UICONF").Columns("ui_script").DefaultValue = 
      'ds.Tables("UICONF").Columns("ui_parent").DefaultValue = 

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub Nuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("UICONF").Rows.Add(dsShared.Tables("UICONF").NewRow)
      bHasChanges = True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Function Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("UICONF").Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldUicf.SaveData(dsShared)
      If bResult Then
        bHasChanges = False
      End If

      Return bResult
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property


  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BeforeColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Function TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("UICONF").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1

      Next
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function CancellaCartella(ByVal strChild As String, ByVal strForm As String, _
                                              ByVal strCtrl As String, ByVal strGridCol As String, _
                                              ByVal strCmbItem As String) As Boolean

    Try
      Return oCldUicf.CancellaCartella(strChild, strForm, strCtrl, strGridCol, strCmbItem)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try

  End Function

  Public Overridable Function EsportaCartella(ByVal strChild As String, ByVal strForm As String, _
                                              ByVal strCtrl As String, ByVal strGridCol As String, _
                                              ByVal strCmbItem As String, ByVal strFile As String) As Boolean
    'esporto le directory
    Dim dttOut As New DataTable
    Dim strOut As String = ""
    Try
      If Not oCldUicf.GetCartellaPerExport(strChild, strForm, strCtrl, strGridCol, strCmbItem, dttOut) Then Return False
      If dttOut.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128407232259704000, "Nessuna impostazione da esportare")))
        Return True
      End If

      If dttOut.Columns.Contains("ts") Then dttOut.Columns.Remove("ts")


      strFile = oapp.ascdir & "\" & strFile & ".xml"
      dttOut.WriteXml(strFile, XmlWriteMode.WriteSchema)
      ThrowRemoteEvent(New NTSEventArgs("MSG_INFO", oApp.Tr(Me, 128407235395804000, "Generato file '|" & strFile & "|'")))

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try

  End Function
  Public Overridable Function EsportaCartellaAgg(ByVal strChild As String, ByVal strForm As String, _
                                                 ByVal strCtrl As String, ByVal strGridCol As String, _
                                                 ByVal strCmbItem As String, ByVal strFile As String) As Boolean
    'esporto le directory
    Dim dttOut As New DataTable
    Dim strOut As String = ""
    Dim strWhere As New Text.StringBuilder
    Dim dtrRow() As DataRow
    Dim i As Integer
    Try
      If Not oCldUicf.GetCartellaPerExport(strChild, strForm, strCtrl, strGridCol, strCmbItem, dttOut) Then Return False
      If dttOut.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129163066875625000, "Nessuna impostazione da esportare")))
        Return True
      End If

      If dttOut.Columns.Contains("ts") Then dttOut.Columns.Remove("ts")

      dtrRow = dttOut.Select("ui_nomprop = 'EXT'")

      If dtrRow.Length = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129163061984062500, "Nessuna impostazione da esportare")))
        Return True
      End If

      For i = 0 To dtrRow.Length - 1
        strWhere.Append("ui_ctrlname <> '")
        strWhere.Append(dtrRow(i)!ui_ctrlname)
        strWhere.Append("' AND ")
      Next

      strWhere.Remove(strWhere.Length - 5, 5)

      dtrRow = dttOut.Select(strWhere.ToString)

      For i = 0 To dtrRow.Length - 1
        dtrRow(i).Delete()
      Next

      dttOut.AcceptChanges()

      strFile = oapp.ascdir & "\" & strFile & ".xml"
      dttOut.WriteXml(strFile, XmlWriteMode.WriteSchema)
      ThrowRemoteEvent(New NTSEventArgs("MSG_INFO", oApp.Tr(Me, 129163066917812500, "Generato file '|" & strFile & "|'")))

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try

  End Function
  Public Overridable Function EsportaGriglia(ByVal strChild As String, ByVal strForm As String, _
                                                ByVal strCtrl As String, ByVal strGridCol As String, _
                                                ByVal strCmbItem As String, ByVal strFile As String) As Boolean
    'esporto le directory
    Dim dttOut As New DataTable
    Dim strOut As String = ""
    Try
      If Not oCldUicf.GetCartellaGriglia(strChild, strForm, strCtrl, strGridCol, strCmbItem, dttOut) Then Return False
      If dttOut.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129411103892875558, "Nessuna impostazione da esportare")))
        Return True
      End If

      If dttOut.Columns.Contains("ts") Then dttOut.Columns.Remove("ts")


      strFile = oApp.AscDir & "\" & strFile & ".xml"
      dttOut.WriteXml(strFile, XmlWriteMode.WriteSchema)
      ThrowRemoteEvent(New NTSEventArgs("MSG_INFO", oApp.Tr(Me, 129411103910844538, "Generato file '|" & strFile & "|'")))

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try

  End Function

  Public Overridable Function ImportaFile(ByVal strFile As String) As Boolean
    Dim dttIn As New DataTable
    Try
      dttIn.ReadXml(strFile)
      If dttIn.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("MSG_INFO", oApp.Tr(Me, 128407290465614000, "Nessuna impostazione da importare")))
        Return True
      End If

      If Not oCldUicf.ImportaImpostazioni(dttIn) Then Return False

      ThrowRemoteEvent(New NTSEventArgs("MSG_INFO", oApp.Tr(Me, 128407290450638000, "Importazione terminata")))
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function TrasferisciConfigurazione(ByVal strNomePrg As String) As Boolean
    Try
      If Not strNomePrg.StartsWith("BO", StringComparison.CurrentCultureIgnoreCase) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130353785627614069, "Posizionarsi sul nodo principale di un programma personalizzato (BOyyxxxx)")))
        Return False
      End If

      Return oCldUicf.TrasferisciConfigurazione("BN" & strNomePrg.Substring(2), strNomePrg)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


#Region "Import/export/delete personalizzazioni"
  Public Overridable Function GetFilePers(ByRef arDll As List(Of String)) As Boolean
    'ritorno tutti i files personalizzati
    Dim s1 As System.IO.StreamReader = Nothing
    Dim strLine As String = ""
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Dim dttTmp As New DataTable
    Try
      'leggo dllmap.ini (se c'è)
      Try
        s1 = New System.IO.StreamReader(oApp.ServerDir + "\Script\DLLMAP.INI")
        strLine = s1.ReadLine.ToUpper  'scarto la prima riga (le note)
        While Not s1.EndOfStream
          strLine = s1.ReadLine.ToUpper
          If strLine.Trim <> "" Then
            strT = strLine.Split(CType("|", Char))
            arDll.Add(strT(2))
          End If
        End While
        s1.Close()
      Catch
      End Try

      'aggiungo eventuali BO,BF,BH
      strT = System.IO.Directory.GetFiles(oApp.NetDir, "BO*.dll")
      For i = 0 To strT.Length - 1
        arDll.Add(strT(i).Substring(oApp.NetDir.Length + 1).Replace(".dll", ""))
      Next
      strT = System.IO.Directory.GetFiles(oApp.NetDir, "BF*.dll")
      For i = 0 To strT.Length - 1
        arDll.Add(strT(i).Substring(oApp.NetDir.Length + 1).Replace(".dll", ""))
      Next
      strT = System.IO.Directory.GetFiles(oApp.NetDir, "BH*.dll")
      For i = 0 To strT.Length - 1
        arDll.Add(strT(i).Substring(oApp.NetDir.Length + 1).Replace(".dll", ""))
      Next
      strT = System.IO.Directory.GetFiles(oApp.NetDir, "B?HH*.dll")
      For i = 0 To strT.Length - 1
        arDll.Add(strT(i).Substring(oApp.NetDir.Length + 1).Replace(".dll", ""))
      Next
      strT = System.IO.Directory.GetFiles(oApp.NetDir, "B?QQ*.dll")
      For i = 0 To strT.Length - 1
        arDll.Add(strT(i).Substring(oApp.NetDir.Length + 1).Replace(".dll", ""))
      Next

      'aggiungo eventuali dll da reg. di business (BO, zoom personalizzati)
      If Not oCldUicf.GetDllPersGreg(dttTmp) Then Return False
      For i = 0 To dttTmp.Rows.Count - 1
        If dttTmp.Rows(i)!rp_nomprop.ToString.Substring(0, 4).ToUpper = "ZOOM" Then
          strT = NTSCStr(dttTmp.Rows(i)!rp_valprop).Split(";"c)
          arDll.Add(strT(0).Split("."c)(0))
          If strT.Length > 1 Then arDll.Add(strT(1))
        Else
          strT = NTSCStr(dttTmp.Rows(i)!rp_valprop).Split(";"c)
          arDll.Add(strT(0))
        End If
      Next

      'aggiungo eventuali dll da voci di menu 17
      If Not oCldUicf.GetDllPersMenupop(dttTmp) Then Return False
      For i = 0 To dttTmp.Rows.Count - 1
        If dttTmp.Rows(i)!mnProgr.ToString.PadRight(2).Substring(0, 2).ToUpper <> "BS" Then
          If dttTmp.Rows(i)!mnProgr.ToString.ToLower.IndexOf(".exe") = -1 And _
             dttTmp.Rows(i)!mnProgr.ToString.IndexOf("\") = -1 And _
             dttTmp.Rows(i)!mnProgr.ToString.IndexOf(" ") = -1 And _
             dttTmp.Rows(i)!mnProgr.ToString.ToLower.IndexOf("subm") = -1 Then
            arDll.Add(dttTmp.Rows(i)!mnProgr.ToString)
          End If
        End If
      Next

      'tolgo i files standard
      For i = 0 To arDll.Count - 1
        'NON DEVO MAI CANCELLARE I FILES CHE INIZIANO PER BS
        If arDll(i).PadRight(2).Substring(0, 2).ToUpper = "BS" Then
          arDll(i) = "x"
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function PersDelete() As Boolean
    Dim arDll As New List(Of String)
    Dim i As Integer = 0
    Dim strT() As String = Nothing
    Try
      If Not GetFilePers(arDll) Then Return False

      'cancello i files
      For i = 0 To arDll.Count - 1
        'tolgo l'attributo di sola lettura
        Try
          System.IO.File.SetAttributes(oApp.NetDir & "\" & arDll(i) & ".dll", System.IO.FileAttributes.Archive)
          System.IO.File.SetAttributes(oApp.NetDir & "\" & arDll(i) & ".pdb", System.IO.FileAttributes.Archive)
        Catch
        End Try
        Try
          System.IO.File.Delete(oApp.NetDir & "\" & arDll(i) & ".pdb")
          System.IO.File.Delete(oApp.NetDir & "\" & arDll(i) & ".dll")
        Catch ex As Exception
          'il file potrebbe non esistere
        End Try
      Next

      'svuoto la 'script' del server
      strT = System.IO.Directory.GetFiles(oApp.ServerDir + "\Script")
      For i = 0 To strT.Length - 1
        System.IO.File.SetAttributes(strT(i), System.IO.FileAttributes.Archive)
        System.IO.File.Delete(strT(i))
      Next

      'cencello le voci dal reg. di Business e da uicf, eccetto il CTRL+ALT+F2
      oCldUicf.DeletePersGregUiConf()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function PersExport(ByVal strDirOut As String) As Boolean
    Dim arDll As New List(Of String)
    Dim i As Integer = 0
    Dim strT() As String = Nothing
    Dim dttGreg As New DataTable
    Dim dttUicf As New DataTable
    Dim dttMenu As New DataTable
    Try
      If System.IO.Directory.Exists(strDirOut) Then System.IO.Directory.Delete(strDirOut, True)
      System.IO.Directory.CreateDirectory(strDirOut)

      If Not GetFilePers(arDll) Then Return False

      'copio i files
      For i = 0 To arDll.Count - 1
        Try
          System.IO.File.SetAttributes(oApp.NetDir & "\" & arDll(i) & ".dll", System.IO.FileAttributes.Archive)
          System.IO.File.SetAttributes(oApp.NetDir & "\" & arDll(i) & ".pdb", System.IO.FileAttributes.Archive)
          System.IO.File.Copy(oApp.NetDir & "\" & arDll(i) & ".dll", strDirOut & "\" & arDll(i) & ".dll")
          System.IO.File.Copy(oApp.NetDir & "\" & arDll(i) & ".pdb", strDirOut & "\" & arDll(i) & ".pdb")
        Catch
        End Try
      Next

      'copio la 'script' del server
      strT = System.IO.Directory.GetFiles(oApp.ServerDir + "\Script")
      For i = 0 To strT.Length - 1
        System.IO.File.SetAttributes(strT(i), System.IO.FileAttributes.Archive)
        System.IO.File.Copy(strT(i), strDirOut & strT(i).Substring((oApp.ServerDir + "\Script").Length))
      Next

      'esporto le voci dal reg. di Business e da uicf, eccetto il CTRL+ALT+F2
      oCldUicf.GetPersGregUiconf(dttGreg, dttUicf, dttMenu)
      dttGreg.WriteXml(strDirOut & "\greg.xml")
      dttUicf.WriteXml(strDirOut & "\uiconf.xml")
      dttMenu.WriteXml(strDirOut & "\menupop.xml")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function PersImport(ByVal strDirIn As String) As Boolean
    'importo TUTTE le personalizzazioni del cliente
    Dim arDll As New List(Of String)
    Dim i As Integer = 0
    Dim strT() As String = Nothing
    Dim dttGreg As New DataTable
    Dim dttUicf As New DataTable
    Dim dttMenu As New DataTable
    Dim strFile As String = ""
    Dim strFileTMP As String = ""
    Dim bOk As Boolean = False
    Dim msg As NTSEventArgs

    Try


      If Not System.IO.Directory.Exists(strDirIn) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129416378368134766, "Directory '|" & strDirIn & "|' inesistente. Elaborazione interrotta. ")))
        Return False
      End If
      strT = System.IO.Directory.GetFiles(strDirIn)
      If strT.Length = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129416387208134766, "Directory '|" & strDirIn & "|' vuota. Elaborazione interrotta.")))
        Return False
      End If

      'controllo che non siano presenti altre personalizzazioni
      If System.IO.Directory.GetFiles(oApp.ServerDir + "\Script").Length > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129416379174833985, "In Business sono presenti altre personalizzazioni (directory 'Script' non vuota). Elaborazione interrotta.")))
        Return False
      End If
      oCldUicf.GetPersGregUiconf(dttGreg, dttUicf, dttMenu)
      If dttGreg.Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129416379766201172, "In Business sono presenti altre personalizzazioni (opzioni di registro di business BUSINESS/OPZIONI/CHILD_xxx o BUSINESS/OPZIONI/ZOOMxxxx). Elaborazione interrotta.")))
        Return False
      End If
      If dttUicf.Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129416380580517579, "In Business sono presenti altre personalizzazioni (righe in 'configurazione user interface' diverse da configurazione di griglia). Elaborazione interrotta.")))
        Return False
      End If
      If dttMenu.Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129422597232050781, "In Business sono presenti altre personalizzazioni (menu personalizzato di primo livello 'H'). Elaborazione interrotta.")))
        Return False
      End If

      If Not GetFilePers(arDll) Then Return False
      For i = 0 To arDll.Count - 1
        If arDll(i).Replace("x", "").Replace("-", "").Trim <> "" Then
          If System.IO.File.Exists(oApp.NetDir & "\" & arDll(i) & ".dll") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129416381500751954, "In Business sono presenti altre personalizzazioni (dll personalizzata |" & oApp.NetDir & "\" & arDll(i) & ".dll" & "|). Elaborazione interrotta.")))
            Return False
          End If
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Prima di copiare i files se, per ogni file "BO*", "BF*", "BD*", 
      '--- non esiste il corrispondente "BN*", "BE*", "BD*", avvisa e chiede se proseguire o annullare l'importazione
      '--------------------------------------------------------------------------------------------------------------
      dttFileOrigMancanti.Clear()
      dttFileOrigMancanti.Dispose()
      dttFileOrigMancanti.Columns.Clear()
      dttFileOrigMancanti.AcceptChanges()
      dttFileOrigMancanti.Columns.Add("NomeFile", GetType(String))
      For i = 0 To strT.Length - 1
        strFile = strT(i).Substring(strDirIn.Length).Replace("\", "").ToLower
        If (strFile.Substring(strFile.Length - 4).ToLower = ".pdb") Or _
           (strFile.Substring(strFile.Length - 4).ToLower = ".dll") Then
          Select Case Mid(strFile, 1, 2).ToUpper
            Case "BO" : strFileTMP = "BN" & Mid(strFile, 3)
            Case "BF" : strFileTMP = "BE" & Mid(strFile, 3)
            Case "BH" : strFileTMP = "BD" & Mid(strFile, 3)
          End Select
          bOk = True
          If System.IO.File.Exists(oApp.NetDir & "\" & strFileTMP) = False Then
            If System.IO.File.Exists(oApp.NetDir & "\PERS\" & strFileTMP) = False Then bOk = False
          End If
          If bOk = False Then
            '--------------------------------------------------------------------------------------------------------
            If strFile.Substring(strFile.Length - 4).ToLower = ".dll" Then
              dttFileOrigMancanti.Rows.Add(dttFileOrigMancanti.NewRow)
              dttFileOrigMancanti.Rows(dttFileOrigMancanti.Rows.Count - 1)!NomeFile = strFile
              dttFileOrigMancanti.AcceptChanges()
            End If
            '--------------------------------------------------------------------------------------------------------
            msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 130819423261573214, "ATTENZIONE!" & vbCrLf & _
              "Si sta importando il file:" & vbCrLf & _
              " . '|" & strFile.ToUpper & "|'" & vbCrLf & _
              "mentre NON esiste il suo corrispondente:" & vbCrLf & _
              " . '|" & strFileTMP.ToUpper & "|'" & vbCrLf & _
              "Premendo 'No', l'elaborazione sarà interrotta." & vbCrLf & _
              "Proseguire?"))
            ThrowRemoteEvent(msg)
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130819427958724990, "Elaborazione interrotta.")))
              Return False
            End If
          End If
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      'copio i files e/o collego i dati in valprop e uiconf
      For i = 0 To strT.Length - 1
        strFile = strT(i).Substring(strDirIn.Length).Replace("\", "").ToLower
        System.IO.File.SetAttributes(strT(i), System.IO.FileAttributes.Archive)
        Select Case strFile.Substring(strFile.Length - 4)
          Case ".pdb", ".dll"
            Try
              System.IO.File.Copy(strT(i), oApp.NetDir & "\" & strFile)
            Catch
              'ignoro l'errore se il file esiste già
            End Try
          Case ".ini", ".nts"
            System.IO.File.Copy(strT(i), oApp.ServerDir & "\Script\" & strFile, True)
          Case ".gif", ".jpg", ".ico"
            System.IO.File.Copy(strT(i), oApp.NetDir & "\BNImages\" & strFile, True)
          Case ".xml"
            'devo caricarli in datatable, poi salvarli in uiconf e/o regprop (forse devo creare anche regdir)
            If strFile = "greg.xml" Then
              dttGreg.ReadXml(strT(i))
            ElseIf strFile = "uiconf.xml" Then
              dttUicf.ReadXml(strT(i))
            ElseIf strFile = "menupop.xml" Then
              dttMenu.ReadXml(strT(i))
            Else
              System.IO.File.Copy(strT(i), oApp.ServerDir & "\Script\" & strFile, True)
            End If
        End Select
      Next

      'salvo regdir, regprop, uiconf
      If Not oCldUicf.SavePresGregUiconf(dttGreg, dttUicf, dttMenu) Then Return False

      TestFirmeEreditate(strDirIn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function TestFirmeEreditate(ByVal strDirIn As String) As Boolean
    'per dll personalizzate, verifico le firme 'ereditate', le confronto con quelle del padre e se nel padre sono presenti
    'più firme per la stessa funzione avviso che la personalizzazion dovrà essere provata, perchè non è detto che la firma
    'ereditata (ad esempio in un BD) sia ancora chiamata da in BE
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim l As Integer = 0
    Dim k As Integer = 0
    Dim strT() As String = Nothing
    Dim strFile As String = ""

    Dim assem As System.Reflection.Assembly       'assembly dll personalizzata
    Dim childType() As System.Type = Nothing      'classi contenute nella dll pers
    Dim mods() As System.Reflection.MethodInfo = Nothing  'nomi delle funzioni contenute nella classe ereditata
    'Dim nParams As Integer = 0                            'numero di parametri della funzione 

    Dim assemP As System.Reflection.Assembly      'assembly parent
    Dim childTypeP As System.Type = Nothing       'classe padre
    Dim modsP() As System.Reflection.MethodInfo = Nothing 'nomi delle funzioni contenute nella classe padre
    'Dim nParamsP As Integer = 0                   'numero di parametri della funzione padre

    Dim nOccorrenze As Integer = 0
    Try
      '----------------------------------------------------
      'adesso devo controllare le firme delle funzioni ereditate
      strT = System.IO.Directory.GetFiles(strDirIn)
      For i = 0 To strT.Length - 1
        strFile = strT(i).Substring(strDirIn.Length).Replace("\", "").ToLower
        If strFile.Substring(strFile.Length - 4) = ".dll" Then
          '----------------------------------------------------------------------------------------------------------
          If dttFileOrigMancanti.Rows.Count > 0 Then
            If dttFileOrigMancanti.Select("NomeFile = " & CStrSQL(strFile)).Length > 0 Then GoTo ProssimoFile
          End If
          '----------------------------------------------------------------------------------------------------------
          assem = System.Reflection.Assembly.LoadFrom(strT(i))
          childType = assem.GetTypes
          For n = 0 To childType.Length - 1
            If childType(n).Name.PadRight(2).Substring(0, 2) <> "My" Then
              If childType(n).Name.Substring(3) = childType(n).BaseType.Name.Substring(3) Then
                If childType(n).Namespace = "NTSInformatica" Then

                  Select Case childType(n).BaseType.Name.Substring(0, 3).ToLower
                    Case "frm", "cle", "cld"

                      'memorizzo assembly e classe padre
                      assemP = System.Reflection.Assembly.LoadFrom(childType(n).BaseType.Assembly.Location)
                      childTypeP = assemP.GetType("NTSInformatica." & childType(n).BaseType.Name)
                      modsP = childTypeP.GetMethods((System.Reflection.BindingFlags.Public Or _
                                                      System.Reflection.BindingFlags.Instance Or _
                                                      System.Reflection.BindingFlags.DeclaredOnly))

                      'è una classe ereditata: per ogni funzione della classe ereditata verifico se nella classe base la stessa funzione ha più firme
                      mods = childType(n).GetMethods((System.Reflection.BindingFlags.Public Or _
                                                      System.Reflection.BindingFlags.Instance Or _
                                                      System.Reflection.BindingFlags.DeclaredOnly))
                      'ora ho l'elenco delle funzioni contenute nella classe ereditata: verifico sulla classe di origine
                      For l = 0 To mods.Length - 1      'loop su funzioni classe ereditata
                        nOccorrenze = 0
                        For k = 0 To modsP.Length - 1     'verifico se nella classe base esistono più funzioni con nome = a quello della classe ereditata
                          If mods(l).Name = modsP(k).Name And _
                             mods(l).GetParameters().Length < modsP(k).GetParameters().Length Then
                            'incremento il numero di versioni della funzione ereditata solo se i parametri 
                            'passati nella funzione ereditata sono minori di quella della funzione padre.
                            nOccorrenze += 1
                          End If
                        Next
                        If nOccorrenze > 0 Then
                          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 129416522258701172, _
                                           "ATTENZIONE nota per il programmatore che ha realizzato la personalizzazione:" & vbCrLf & vbCrLf & _
                                           "nel file '|" & strT(i) & "|'" & vbCrLf & _
                                           "è contenuta la classe ereditata '|" & childType(n).Name & "|'" & vbCrLf & _
                                           "che contiene la funzione '|" & mods(l).Name & "|' con passaggio di |" & mods(l).GetParameters().Length.ToString & "| parametri:" & vbCrLf & _
                                           "Nella classe PADRE '|" & childType(n).BaseType.Name & "|' la stessa funzione possiede anche firme con numero di parametri maggiore." & vbCrLf & vbCrLf & _
                                           "Si consiglia di testare la personalizzazione in quanto la chiamata con i solo parametri passati potrebbe essere NON PIU' UTILIZZATA " & vbCrLf & _
                                           "ma mantenuta da Business NET per compatibilità")))
                        End If
                      Next
                  End Select
                End If    'If childType(n).Namespace = "NTSInformatica" Then
              End If    'If childType(n).Name.Substring(3) = childType(n).BaseType.Name.Substring(3) Then
            End If    'If childType(n).Name.PadRight(2).Substring(0, 2) <> "My" Then
          Next    'For n = 0 To childType.Length - 1


        End If
ProssimoFile:
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

#End Region

End Class
