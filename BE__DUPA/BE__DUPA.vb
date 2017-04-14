#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
#End Region

Public Class CLE__DUPA
  Inherits CLE__BASE

#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = 0
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
#End Region

#Region "Variabili"
  Public oCldDupa As CLD__DUPA
  Public strMsg As String
#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, _
                               ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                               ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                               ByVal strRemotePort As String) As Boolean
    MyBase.strNomeDal = "BD__DUPA"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldDupa = CType(MyBase.ocldBase, CLD__DUPA)
    oCldDupa.Init(oApp)
    Return True
  End Function

#Region "DSN"
  <DllImport("ODBCCP32.DLL")> _
  Public Shared Function SQLCreateDataSource(ByVal hwndParent As IntPtr, ByVal lpszDSN As String) As Boolean
  End Function

  Public Declare Function SQLConfigDataSource Lib "ODBCCP32.DLL" (ByVal hwndParent As Integer, ByVal fRequest As Integer, ByVal lpszDriver As String, ByVal lpszAttributes As String) As Integer

  Public Const ODBC_ADD_DSN As Integer = 1
  Public Const ODBC_REMOVE_DSN As Integer = 3
  Public Const ODBC_ADD_SYS_DSN As Integer = 4
  Public Const ODBC_REMOVE_SYS_DSN As Integer = 6

#End Region

#Region "CREA SYSTEM DSN"
  'per far partire il wizard per creare il DSN a mano
  'Dim ODBCSTRING As String = ""
  'CLE_CRPE.SQLCreateDataSource(Me.Handle, ODBCSTRING)

  'per creare il system DNS in modo automatico
  'objCrpe.CreateAccessSystemDSN("prova", "c:\prova.mdb", "admin", "")
  'objCrpe.CreateSQLServerSystemDSN("pr12", "mirto", "pr12", True)

  ''' <summary>
  ''' crea il DSN di sistema per driver SQL SERVER (viene sempre chiesta la PWD dell'USER)
  ''' </summary>
  ''' <param name="DSNName">prova</param>
  ''' <param name="ServerName">SERVER</param>
  ''' <param name="Database">prova</param>
  ''' <param name="bIntegrated">true/false</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overridable Function CreateSQLServerSystemDSN(ByVal DSNName As String, ByVal ServerName As String, ByVal Database As String, ByVal bIntegrated As Boolean) As Integer
    'CREATES A SYSTEM DSN FOR AN SQL SERVER DATABASE
    'EXAMPLE: CreateSQLServerDSN "MyDSN", "MyServer", "MyDatabase"
    'DSNName = prova
    'ServerName = mirto
    'Database = prova
    'bIntegrated = false 
    Dim bOk As Integer = 0
    Dim bSistema As Boolean = True
    Try
      Dim sAttributes As String
      sAttributes = "DSN=" & DSNName & vbNullChar
      sAttributes = sAttributes & "Server=" & ServerName & vbNullChar
      sAttributes = sAttributes & "Database=" & Database & vbNullChar
      sAttributes = sAttributes & "Description=Database " & Database & " in SQL Server" & vbNullChar
      sAttributes = sAttributes & "OemToAnsi=Yes" & vbNullChar
      sAttributes = sAttributes & "UseProcForPrepare=Yes" & vbNullChar
      If bIntegrated Then
        sAttributes = sAttributes & "Trusted_Connection=Yes" & vbNullChar
      Else
        sAttributes = sAttributes & "Trusted_Connection=No" & vbNullChar
      End If
      sAttributes = sAttributes & "Language=us_english" & vbNullChar

      'prima rimuovo sempre l'eventuale datasource UTENTE presente
      SQLConfigDataSource(0, ODBC_REMOVE_DSN, "SQL Server" & Chr(0), sAttributes)

      'creo il nuovo
      If CLN__STD.GetSettingRegPerUser = 0 Then
        bSistema = IsBusHKLM()
      ElseIf CLN__STD.GetSettingRegPerUser = 1 Then
        bSistema = True
      Else
        bSistema = False
      End If
      If bSistema Then
        'se installazione per macchina rimuovo il DSN di sistema
        SQLConfigDataSource(0, ODBC_REMOVE_SYS_DSN, "SQL Server" & Chr(0), sAttributes)
        bOk = SQLConfigDataSource(0, ODBC_ADD_SYS_DSN, "SQL Server" & Chr(0), sAttributes)
        If bOk = 0 Then
          'dovevo crearlo di systema, ma lo uac non me lo ha permesso. creato utente
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 129089775899843203, "Non è stato possibile creare il DSN di sistema (UAC attivo o utente di windows non di tipo Administrator). Verrà creato un DSN di tipo 'utente'.")))
          bOk = SQLConfigDataSource(0, ODBC_ADD_DSN, "SQL Server" & Chr(0), sAttributes)
          If bOk = 1 Then bOk = 3
        End If
      Else
        bOk = SQLConfigDataSource(0, ODBC_ADD_DSN, "SQL Server" & Chr(0), sAttributes)
      End If

      Return bOk
      'UID and PWD are not supported by the API. 
      'Actually all that SQL Config Data Source does is make entries in the registry. 
      'If you look at HKLM_\Software\OBDC\ODBC.INI you will see that the password for the other DSNs is not saved. 
      'When you connect through a DSN you will always be required to present a user ID and password or 
      'else use a trusted connection, i.e., 
      '"DSN=MyDSN;UID=Smith;PWD=Sesame" or "DSN=MyDSN;Trusted_Connection=yes".
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try

  End Function
  Public Overridable Function DeleteSQLServerSystemDSN(ByVal DSNName As String, ByVal ServerName As String, ByVal Database As String, ByVal bIntegrated As Boolean) As Boolean
    Try
      Dim sAttributes As String
      sAttributes = "DSN=" & DSNName & vbNullChar
      sAttributes = sAttributes & "Server=" & ServerName & vbNullChar
      sAttributes = sAttributes & "Database=" & Database & vbNullChar
      sAttributes = sAttributes & "Description=Database " & Database & " in SQL Server" & vbNullChar
      sAttributes = sAttributes & "OemToAnsi=Yes" & vbNullChar
      sAttributes = sAttributes & "UseProcForPrepare=Yes" & vbNullChar
      If bIntegrated Then
        sAttributes = sAttributes & "Trusted_Connection=Yes" & vbNullChar
      Else
        sAttributes = sAttributes & "Trusted_Connection=No" & vbNullChar
      End If
      sAttributes = sAttributes & "Language=us_english" & vbNullChar

      'prima rimuovo l'eventuale datasource presente
      SQLConfigDataSource(0, ODBC_REMOVE_DSN, "SQL Server" & Chr(0), sAttributes)
      SQLConfigDataSource(0, ODBC_REMOVE_SYS_DSN, "SQL Server" & Chr(0), sAttributes)
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try

  End Function
  ''' <summary>
  ''' crea il DSN di sistema per driver MSACCESS
  ''' </summary>
  ''' <param name="DSNName">PROVA</param>
  ''' <param name="DatabaseFullPath">c:\bus\prova.mdb</param>
  ''' <param name="strUser">admin</param>
  ''' <param name="strPwd"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Overridable Function CreateAccessSystemDSN(ByVal DSNName As String, ByVal DatabaseFullPath As String, ByVal strUser As String, ByVal strPwd As String) As Integer
    'CREATES A SYSTEM DSN FOR AN ACCESS DATABASE
    'EXAMPLE:
    'DSNName = PROVA
    'DatabaseFullPath = c:\bus\prova.mdb
    'strUser = admin
    'stPwd = 
    Try
      Dim sAttributes As String
      sAttributes = "DSN=" & DSNName & vbNullChar
      sAttributes = sAttributes & "DBQ=" & DatabaseFullPath & vbNullChar
      sAttributes = sAttributes & "DESCRIPTION=Database " & DatabaseFullPath & vbNullChar
      sAttributes = sAttributes & "UID=" & strUser & vbNullChar
      sAttributes = sAttributes & "PWD=" & strPwd & vbNullChar
      sAttributes = sAttributes & vbNullChar

      'prima rimuovo l'eventuale datasource presente
      SQLConfigDataSource(0, ODBC_REMOVE_DSN, "Microsoft Access Driver (*.mdb)" & Chr(0), sAttributes)
      'creo il nuovo
      Return SQLConfigDataSource(0, ODBC_ADD_DSN, "Microsoft Access Driver (*.mdb)" & Chr(0), sAttributes)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function DeleteAccessSystemDSN(ByVal DSNName As String, ByVal DatabaseFullPath As String, ByVal strUser As String, ByVal strPwd As String) As Boolean
    Try
      Dim sAttributes As String
      sAttributes = "DSN=" & DSNName & vbNullChar
      sAttributes = sAttributes & "DBQ=" & DatabaseFullPath & vbNullChar
      sAttributes = sAttributes & "DESCRIPTION=Database " & DatabaseFullPath & vbNullChar
      sAttributes = sAttributes & "UID=" & strUser & vbNullChar
      sAttributes = sAttributes & "PWD=" & strPwd & vbNullChar
      sAttributes = sAttributes & vbNullChar

      'prima rimuovo l'eventuale datasource presente
      SQLConfigDataSource(0, ODBC_REMOVE_DSN, "Microsoft Access Driver (*.mdb)" & Chr(0), sAttributes)

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
#End Region


  Public Overridable Function IsBusHKLM() As Boolean
    Try
      'verifico se l'installazione è per macchina o per utente
      If CLN__STD.GetSettingRegPerUser = 1 Then
        Return True
      Else
        Return False
      End If

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


  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables(strNomeTabella).Columns("AzCodaz").DefaultValue = ""
      ds.Tables(strNomeTabella).Columns("AzDescr").DefaultValue = ""
      ds.Tables(strNomeTabella).Columns("AzExt").DefaultValue = ""
      ds.Tables(strNomeTabella).Columns("AzPrefix").DefaultValue = ""
      ds.Tables(strNomeTabella).Columns("AzOpgrup").DefaultValue = 0
      ds.Tables(strNomeTabella).Columns("AzDatatype").DefaultValue = "Access"
      ds.Tables(strNomeTabella).Columns("AzSubdatatype").DefaultValue = "SQLServer7"
      ds.Tables(strNomeTabella).Columns("AzConnect").DefaultValue = ""
      ds.Tables(strNomeTabella).Columns("AzCodaz").DefaultValue = ""
      ds.Tables(strNomeTabella).Columns("AzDescr").DefaultValue = ""
      ds.Tables(strNomeTabella).Columns("az_adoprovider").DefaultValue = "SQLOLEDB"
      ds.Tables(strNomeTabella).Columns("az_adoconnect").DefaultValue = ""
      ds.Tables(strNomeTabella).Columns("az_rdsservername").DefaultValue = ""
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

  Public Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
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
  Public Overridable Sub BeforeColUpdate_azcodaz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'Controlla che il codice azienda sia composto solo da lettere e numeri
      Dim bCodiceValido As Boolean = True
      For j As Integer = 1 To NTSCStr(e.ProposedValue).Length
        If Not (Mid(NTSCStr(e.ProposedValue), j, 1) Like "[A-Z,a-z,0-9]") Then
          bCodiceValido = False
        End If
      Next j
      If Not bCodiceValido Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740869960625000, "Il codice azienda può contenere solo lettere e numeri.")))
        Return
      End If


      If oCldDupa.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "AZIENDE", "S") Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740869960625990, "Codice azienda già esistente.")))
        Return
      End If

      If Not UCase(NTSCStr(e.ProposedValue)) = NTSCStr(e.Row(e.Column.ColumnName)) Then
        e.ProposedValue = UCase(NTSCStr(e.ProposedValue))

      End If

      e.Row!AzExt = e.ProposedValue
      e.Row!AzDescr = "Azienda " & NTSCStr(e.ProposedValue)

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

  Public Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

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


  Public Overrides Function TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer
    Try
      dtrTmp = dsShared.Tables(strNomeTabella).Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        'Controlla l'obbligatorieta del codice azienda
        If NTSCStr(dtrTmp(i)!AzCodaz) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740869960625001, "Codice azienda obbligatorio.")))
          Return False
        End If

        'Controlla l'obbligatorieta della descrizione dell'azienda
        If NTSCStr(dtrTmp(i)!AzDescr) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740869960625002, "Descrizione azienda obbligatorio.")))
          Return False
        End If

        'Controlla l'obbligatorieta del codice ditta
        If NTSCStr(dtrTmp(i)!AzExt) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740869960625003, "Codice ditta predefinito obbligatorio.")))
          Return False
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
    End Try
  End Function

  Public Overridable Function EstraiParametroDaConnectionString(ByVal strConnectionString As String, ByVal strParametroDaEstrarre As String) As String
    Dim nPos1 As Integer
    Dim nPos2 As Integer
    Try
      If Not (Len(strConnectionString) = 0 Or Len(strParametroDaEstrarre) = 0) Then
        nPos1 = InStr(1, UCase(strConnectionString), ";" & strParametroDaEstrarre & "=")
        nPos1 = nPos1 + Len(";" & strParametroDaEstrarre & "=")
        nPos2 = InStr(nPos1, UCase(strConnectionString), ";")
        If nPos2 <> 0 And nPos2 > nPos1 Then Return Mid(strConnectionString, nPos1, nPos2 - nPos1)
      End If
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
    Return ""
  End Function

  Public Overridable Function AggInstAnaz(ByVal strConnectionString As String, ByVal strTmp As String, _
                                          ByVal strMod As String, ByVal strModext As String, _
                                          ByVal strDesaz As String, _
                                          ByVal bSvuotaTabelle As Boolean, ByVal bCreaContropartiteAutomatico As Boolean) As Boolean
    Try
      Return oCldDupa.AggInstAnaz(strConnectionString, strTmp, strMod, strModext, strDesaz, bSvuotaTabelle, bCreaContropartiteAutomatico)
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

  Public Overridable Function LeggiDitte(ByVal strConnectionString As String, ByRef dsOut As System.Data.DataSet) As Boolean

    Return oCldDupa.LeggiDitte(strConnectionString, dsOut)

  End Function


  Dim strAziendaOrigine As String = ""
  Dim strAziendaOrigineConnectionString As String = ""
  Dim bAziendaOrigineIsCorrente As Boolean = False

  Dim strAziendaDestinazione As String = ""
  Dim strAziendaDestinazioneConnectionString As String = ""
  Dim bAziendaDestinazioneIsCorrente As Boolean = False

  Public Overridable Function ImportaCheckConnection(ByVal dtrDbOrigine As DataRow, _
                                       ByVal dtrDbDestinazione As DataRow) As Boolean

    Dim ds As DataSet = Nothing
    Try
      strAziendaOrigine = NTSCStr(dtrDbOrigine!AzCodaz)
      strAziendaDestinazione = NTSCStr(dtrDbDestinazione!AzCodaz)

      strAziendaOrigineConnectionString = NTSCStr(dtrDbOrigine!az_adoconnect)
      If strAziendaOrigineConnectionString.ToUpper.IndexOf("SERVER") = -1 Then Decodifica(strAziendaOrigineConnectionString)

      strAziendaDestinazioneConnectionString = NTSCStr(dtrDbDestinazione!az_adoconnect)
      If strAziendaDestinazioneConnectionString.ToUpper.IndexOf("SERVER") = -1 Then Decodifica(strAziendaDestinazioneConnectionString)

      If UCase(NTSCStr(dtrDbOrigine!AzCodaz)) = UCase(oApp.Db.Nome) Then
        bAziendaOrigineIsCorrente = True
      Else
        bAziendaOrigineIsCorrente = False
        If Not oCldDupa.CheckConnectionAltroDB(strAziendaOrigineConnectionString) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128346017755211316, "Impossibile connetersi al database d'origine." & vbCrLf & _
            "Operazione Annullata.")))
          Return False
        End If
      End If
      If UCase(NTSCStr(dtrDbDestinazione!AzCodaz)) = UCase(oApp.Db.Nome) Then
        bAziendaDestinazioneIsCorrente = True
      Else
        bAziendaDestinazioneIsCorrente = False
        If Not oCldDupa.CheckConnectionAltroDB(strAziendaDestinazioneConnectionString) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128346017755211317, "Impossibile connetersi al database di destinazione." & vbCrLf & _
            "Operazione Annullata.")))
          Return False
        End If
      End If

      'Verifica se il database è unicode. Non si possono importare i dati da un database Unicode a uno NON Unicode
      oCldDupa.VerificaDatabaseUnicode(strAziendaOrigineConnectionString, ds)
      If NTSCStr(ds.Tables("RELEASE").Rows(0)!data_type) = "nvarchar" Then 'Database Origine Unicode
        oCldDupa.VerificaDatabaseUnicode(strAziendaDestinazioneConnectionString, ds)
        If NTSCStr(ds.Tables("RELEASE").Rows(0)!data_type) = "varchar" Then 'Database Destinazione NON Unicode
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130582648284968023, "Impossibile procedere!" & vbCrLf & _
                          "Il database di origine è 'Unicode' mentre quello di destinazione non lo è. L'importazione dei dati compoterrebbe la perdita di tutti i caratteri 'Unicode'.")))
          Return False
        End If
      End If

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

  Public Overridable Function ImportaGetOrderTbl(ByRef dsOrderTbl As DataSet) As Boolean
    Try
      Return oCldDupa.ImportaGetOrderTblTutteTabelle(dsOrderTbl, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString)
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
  Public Overridable Function ImportaDeleteTabella(ByVal strot_nometab As String) As Boolean
    Try
      Return oCldDupa.ImportaDeleteTabella(strot_nometab, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString)
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
  Public Overridable Function ImportaDeleteTabellaSingoleRighe(ByVal strot_nometab As String) As Boolean
    Try
      Return oCldDupa.ImportaDeleteTabellaSingoleRighe(strot_nometab, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString)
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
  Public Overridable Function ImportaDaAltroDb(ByVal strTabella As String) As Boolean
    Try

      Return oCldDupa.ImportaDaAltroDb(strTabella, bAziendaOrigineIsCorrente, strAziendaOrigineConnectionString, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString)

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

  Public Overridable Function CheckRelease(ByVal dttDest As DataTable, ByVal dttOrig As DataTable) As Boolean
    Try
      If Not oCldDupa.GetBusRelease(dttDest, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString) Then
        Return False
      End If
      If Not oCldDupa.GetBusRelease(dttOrig, bAziendaOrigineIsCorrente, strAziendaOrigineConnectionString) Then
        Return False
      End If
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function GetValutaContoOrig() As String
    GetValutaContoOrig = "[Indefinita]"
    Try
      Return oCldDupa.GetValutaConto(bAziendaOrigineIsCorrente, strAziendaOrigineConnectionString)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function GetValutaContoDest() As String
    GetValutaContoDest = "[Indefinita]"
    Try
      Return oCldDupa.GetValutaConto(bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ElaboraTabella(ByVal strTabSelected As String, ByVal bSostituisci As Boolean, ByVal bSovrascrivi As Boolean) As Boolean
    Try
      If strTabSelected = "tabpcon" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("anpvric") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("vociric") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabcovp") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("anptric") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("mastric") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabtric") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("anagpc") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabmast") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabclas") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabgruc") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabpcon") Then Return False
        End If
        If Not CopiaDaAltroDb("tabpcon", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabgruc", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabclas", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabmast", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("anagpc", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabtric", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("mastric", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("anptric", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("vociric", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("anpvric", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabcovp", bSovrascrivi Or bSostituisci) Then Return False
        '--- Cancella i numeratori dei CONTI in tabnuma
        If Not oCldDupa.CancellaNumeratori("tabpcon", bAziendaOrigineIsCorrente, strAziendaOrigineConnectionString, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString, bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "tabcauc" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("tabcauc") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("caucsiv") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("caucset") Then Return False
        End If
        If Not CopiaDaAltroDb("tabcauc", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("caucsiv", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("caucset", bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "tabpaga" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("tabpaga") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("pagalin") Then Return False
        End If
        If Not CopiaDaAltroDb("tabpaga", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("pagalin", bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "anagen" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("anagen") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("destgen") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("anasto") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("deststo") Then Return False
        End If
        If Not CopiaDaAltroDb("anagen", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("destgen", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("anasto", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("deststo", bSovrascrivi Or bSostituisci) Then Return False
        '--- Cancella i numeratori dei CONTI in tabnumg
        If Not oCldDupa.CancellaNumeratori("anagen", bAziendaOrigineIsCorrente, strAziendaOrigineConnectionString, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString, bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "tabcace" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("tabpuce") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabspce") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabgrce") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabcace") Then Return False
        End If
        If Not CopiaDaAltroDb("tabcace", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabgrce", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabspce", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabpuce", bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "tabpcca" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("tabpcca") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabgru2") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabclc2") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabmac2") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("anagca2") Then Return False
        End If
        If Not CopiaDaAltroDb("tabpcca", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabgru2", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabclc2", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabmac2", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("anagca2", bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "tabcaca" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("tabcaca") Then Return False
        End If
        If Not CopiaDaAltroDb("tabcaca", bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "tabdriv" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("tabdriv") Then Return False
        End If
        If Not CopiaDaAltroDb("tabdriv", bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "tabscbg" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("tabscbg") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("tabvoca") Then Return False
        End If
        If Not CopiaDaAltroDb("tabscbg", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("tabvoca", bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "tabtcdc" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("tabtcdc") Then Return False
        End If
        If Not CopiaDaAltroDb("tabtcdc", bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "tabpepa" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("tabpepa") Then Return False
        End If
        If Not CopiaDaAltroDb("tabpepa", bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "caschebut" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("caschebut") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("caschebud") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("caschebur") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("calinkpcon") Then Return False
        End If
        If Not CopiaDaAltroDb("caschebut", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("caschebud", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("caschebur", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("calinkpcon", bSovrascrivi Or bSostituisci) Then Return False
      End If

      If strTabSelected = "tabvalu" Then
        If bSostituisci Then
          'Se sostituisci completamente elimina il possibile
          If Not ImportaDeleteTabellaSingoleRighe("tabvalu") Then Return False
          If Not ImportaDeleteTabellaSingoleRighe("cambi") Then Return False
        End If
        If Not CopiaDaAltroDb("tabvalu", bSovrascrivi Or bSostituisci) Then Return False
        If Not CopiaDaAltroDb("cambi", bSovrascrivi Or bSostituisci) Then Return False
      End If
      Select Case strTabSelected
        Case "tabpcon", "tabcauc", "tabpaga", "anagen", "tabcace", "tabvalu"
          'Gruppi multipli (già trattati sopra)
        Case "altretabel"
          'Elenco dei gruppi (singoli!!!) non specificati espressamente nella LISTA
          'ma raccolti sotto la macro-voce Altre Tabelle comuni
          Dim dsOrderTbl As New DataSet
          If Not oCldDupa.ImportaGetOrderTblTabelleComuni(dsOrderTbl, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString) Then Return False

          If dsOrderTbl.Tables("ORDERTBL").Rows.Count > 0 Then
            For i As Integer = 0 To dsOrderTbl.Tables("ORDERTBL").Rows.Count - 1
              If bSostituisci Then
                If Not ImportaDeleteTabellaSingoleRighe(NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab)) Then Return False
              End If
              If Not CopiaDaAltroDb(NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab), bSovrascrivi Or bSostituisci) Then Return False
            Next
          End If
        Case Else
          'Tutti i gruppi singoli specificati nella LISTA
          If bSostituisci Then
            If Not ImportaDeleteTabellaSingoleRighe(strTabSelected) Then Return False
          End If
          If Not CopiaDaAltroDb(strTabSelected, bSovrascrivi Or bSostituisci) Then Return False
      End Select

      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function CopiaDaAltroDb(ByVal strTabella As String, ByVal bOverwrite As Boolean) As Boolean
    Try
      Return oCldDupa.ImportaDaAltroDb(strTabella, bAziendaOrigineIsCorrente, strAziendaOrigineConnectionString, bAziendaDestinazioneIsCorrente, strAziendaDestinazioneConnectionString, "", bOverwrite, False)
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


  Public Overridable Function RicreaStoredProcedure(ByVal strDbconn As String, ByVal bPrc As Boolean) As Boolean
    Try
      Return oCldDupa.RicreaStoredProcedure(strDbconn, bPrc)

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

  Public Overridable Function CancellaDb(ByVal strConn As String) As Boolean
    Try
      Return oCldDupa.CancellaDB(strConn)

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
  Public Overridable Function CollegaDB(ByVal strConn As String, ByVal strFileMdf As String, ByVal strFileLdf As String) As Boolean
    Try

      Return oCldDupa.CollegaDB(strConn, strFileMdf, strFileLdf)

    Catch ex As Exception
      If InStr(ex.Message.ToString.ToLower, "sqlexception") > 0 Then
        Dim strTmp As String = ex.Message.ToString
        strTmp = Mid(strTmp, (InStr(strTmp, "SqlException") + 14))
        Dim lPos As Integer = InStr(strTmp, "-")
        strTmp = Mid(strTmp, 1, (lPos - 1)).Trim
        If NTSCInt(strTmp) = 1801 Then
          strTmp = StrReverse(strFileMdf)
          lPos = InStr(strTmp, "\")
          If lPos <> 0 Then strTmp = Mid(strTmp, 1, (lPos - 1))
          strTmp = Mid(strTmp, 5)
          strTmp = StrReverse(strTmp)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129974643839523223, "Attenzione!" & vbCrLf & _
            "Database '|" & strTmp & "|' già esistente/in uso." & vbCrLf & _
            "Scegliere un Database diverso.")))
          Return False
        End If
      End If
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function GetDefaultDataDir(ByVal strConn As String, ByRef strDirOut As String) As Boolean
    Try
      Return oCldDupa.GetDefaultDataDir(strConn, strDirOut)
    Catch ex As Exception
      ''--------------------------------------------------------------
      'If GestErrorCallThrow() Then
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      'Else
      '  ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      'End If
      ''--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function CheckPermessiSqlServerUser(ByVal strConn As String) As Boolean
    'controllo se l'utente ha i permessi per creare/attaccare il database
    'solo da sql server 2005 in su
    Try

      Return oCldDupa.checkPermessiSqlServerUser(strConn)

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


  Public Overridable Function CheckSQLServerDir(ByVal strConn As String, ByVal strDir As String, ByVal strFile As String) As Boolean
    Try

      Return oCldDupa.CheckSQLServerDir(strConn, strDir, strFile)

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

End Class
