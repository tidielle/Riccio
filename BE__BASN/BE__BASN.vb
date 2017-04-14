Imports System.Data
Imports System.IO
Imports NTSInformatica.CLN__STD
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Reflection

Public Class CLE__BASN
  Implements IDisposable

  Public oApp As CLE__APP      'oggetto application passato dall'entity
  Public ocldBase As CLD__BASE
  Public strDittaCorrente As String = ""
  Public strPrevCelValue As String = ""
  Public strNomeTabella As String = ""
  Public oScript As INT__SCRIPT = Nothing   'script della UI
  Public oScriptE As INT__SCRIPT = Nothing  'script sidecar dell'entity
  Public oCleComm As Object = Nothing       'puntatore a BELBMENU
  Public strNomeDal As String = "BD__BASE"

  Dim strServer As String = ""              'fisso NTSInformatica
  Dim strClasse As String = ""              'nome della classe FRM__PAGA
  Public strVerFile As String = ""          'versione del file 1.1.0.1234

  Public lw1 As StreamWriter
  Dim strLogFile As String = ""         'nome del file di log
  Private nTestCanExecute As Integer = 0        'per evitare che la canexecute venga chiamata tuttele volte, la eseguo soloper le prime 10 volte, tanto se non ho la chiave di attivazione, con i primi 10 messaggi viene fuori ...
  Public nCountLog As Integer = 0

  Private lDbVersionNeededMajor As Integer = 0         'versione minima di database richiesta per poter far funzionare correttamente il child
  Private lDbVersionNeededMinor As Integer = 0         'versione minima di database richiesta per poter far funzionare correttamente il child

  Public CustomClass As Boolean = False             'se TRUE la classe istanziata non è quella standard NTS ma una ereditata

  Public Sub SetDbVersionNeeded(ByVal lMajor As Integer, ByVal lMinor As Integer)
    'questa funzione viene chiamata solo dai vari entity che in corso d'anno richiedono un DB modificato per operare
    'CASO PARTICOLARE:
    'ipotizziamo di aver modificato beveboll per richiedere almeno un DB di versione 18.1
    'successivamente modifichiamo bemgdocu per richiedere almeno un DB di versione 18.2
    'all’avvio di beveboll andrà verificata ed acquisita la versione più alta tra tutte le dll ereditate!
    If lDbVersionNeededMajor > lMajor Then Return
    If lDbVersionNeededMajor = lMajor AndAlso lDbVersionNeededMinor > lMinor Then Return
    lDbVersionNeededMajor = lMajor
    lDbVersionNeededMinor = lMinor
  End Sub
  Public ReadOnly Property DbVersionNeededMajor() As Integer
    Get
      Return lDbVersionNeededMajor
    End Get
  End Property
  Public ReadOnly Property DbVersionNeededMinor() As Integer
    Get
      Return lDbVersionNeededMinor
    End Get
  End Property


  Public Overridable Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, _
                                    ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                                    ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                                    ByVal strRemotePort As String) As Boolean
    oApp = App
    strDittaCorrente = oApp.Ditta
    oScript = oScriptEngine
    oCleComm = oCleLbmenu
    strNomeTabella = strTabella

    '------------------------------------------------
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    Dim strNomeChiamante As String = Me.GetType.Assembly.GetName.Name
    If Me.GetType.Assembly.Location.Trim = "" Then
      'è una dll compilata al volo e caricata in memoria.
      'può essere solo il caso di BX, BY, BW di Business file (o altro verticale di NTS Project)
      strNomeChiamante = Me.GetType.BaseType.Assembly.GetName.Name
    End If
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, strNomeChiamante, strNomeDal, oTmp, strErr, bRemoting, strRemoteServer, strRemotePort) = False Then
      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128480830865991500, "ERRORE in fase di istanziazione Dll:" & vbCrLf & "|" & strErr & "|")))
      Return False
    End If
    ocldBase = CType(oTmp, CLD__BASE)
    '------------------------------------------------
    ocldBase.Init(oApp)


    '------------------------------------------------
    'verifico se la versione del DB è almeno quella richiesta dalla DLL
    'CASO PARTICOLARE:
    'ipotizziamo di aver modificato beveboll per richiedere almeno un DB di versione 18.1
    'successivamente modifichiamo bemgdocu per richiedere almeno un DB di versione 18.2
    'all’avvio di beveboll andrà verificata ed acquisita la versione più alta tra tutte le dll ereditate!
    If Me.lDbVersionNeededMajor > 0 AndAlso oApp.Db.ServerDB.Trim <> "" Then 'Non ha senso fare i controlli se non è stata impostata una versione richiesta
      Dim lDBMajor As Integer = 0
      Dim lDBMinor As Integer = 0
      Dim strSplit() As String = oApp.Db.Release.Split("."c)
      lDBMajor = NTSCInt(strSplit(0))
      lDBMinor = NTSCInt(strSplit(1))

      If Me.lDbVersionNeededMajor < lDBMajor Then
        'posso continuare: dll di versione precedente a quella del DB (es dll net 2011 su net 2012)
      ElseIf Me.lDbVersionNeededMajor = lDBMajor And _
             Me.lDbVersionNeededMinor <= lDBMinor Then
        'posso ancora continuare
      Else
        'non posso continuare: la dll richiede un DB di versione più aggiornata!
        Throw New NTSException(oApp.Tr(Me, 129796687221992703, "ATTENZIONE: la DLL |'" & Me.GetType.ToString & _
                              "'| (o una sua ereditata) richiede un database azienda di versione almeno |'" & _
                              Me.lDbVersionNeededMajor.ToString("00") & "." & Me.lDbVersionNeededMinor.ToString("00") & "'|"))
        Return False
      End If
    End If

    '------------------------------------------------
    'inizializzo lo script sidecar
    'Dim strServer As String = ""              'fisso NTSInformatica
    'Dim strClasse As String = ""              'nome della classe FRM__PAGA
    'If Me.GetType.ToString.IndexOf(".") > -1 Then
    '  strServer = Me.GetType.ToString.Substring(0, Me.GetType.ToString.IndexOf("."))
    '  strClasse = "B" & Me.GetType.ToString.Substring(strServer.Length + 1).ToUpper.Substring(2)
    'Else
    '  strServer = "xxxxxxxxxx"
    '  strClasse = "B" & Me.GetType.ToString.ToUpper.Substring(2)
    'End If
    'If strClasse <> "BE__MENU" Then
    '  'il menu ha già il suo script ...
    '  Try

    '    oApp.MakeScript(oApp.ServerDir + "\Script\" & strClasse & ".NTS", strClasse & "VBS", oScriptE)
    '    If Not oScriptE Is Nothing Then
    '      oScriptE.Exec("InitEntity", CType(oApp, Object), Me, Nothing)
    '    End If
    '    ocldBase.oScriptEntity = oScriptE

    '  Catch ex As Exception
    '    Throw New NTSException(oApp.Tr(Me, 127791222108125000, "CLE__BASN->Init: Errore in fase di avvio Script contenuto in |" & strClasse & "|.NTS: " & vbCrLf & "|" & ex.ToString() & "|"))
    '  End Try
    'End If

    '------------------------------------------------
    Return True
  End Function

  '----------------------------------------------
  'GESTIONE DEGLI EVENTI PER IL PASSAGGIO DI INFORMAZIONI TRA DAL E ENTITY
  Public Event RemoteEvent As NTSEventHandler
  Protected Sub ThrowRemoteEvent(ByRef e As NTSEventArgs)
    Dim strErr As String = ""
    If Not ocldBase Is Nothing Then
      If nTestCanExecute < 10 Then
        nTestCanExecute += 1

        If oApp.ActKey.MultiKey = "N" Then
          If oApp.CanExecute(Me, "", strErr) = False Then
            oApp = Nothing
            RaiseEvent RemoteEvent(Me, New NTSEventArgs("", strErr))
            Return
          End If
        Else
          If CanExecute(Me, "", strErr) = False Then
            oApp = Nothing
            RaiseEvent RemoteEvent(Me, New NTSEventArgs("", strErr))
            Return
          End If
        End If

      End If
    End If
    If Not oApp Is Nothing Then
      'per migliorare le prestazioni se ho impsotato l'opzione di registro OPZIONI/GRIAGG = -1 non eseguo quell'evento ...
      If oApp.DisableGRIAGG AndAlso e.TipoEvento = "GRIAGG" Then Return
    End If
    RaiseEvent RemoteEvent(Me, e)
  End Sub

  Private Function CanExecute(ByVal obj As Object, ByVal strPrgName As String, ByRef strError As String, _
                             Optional ByRef lModuli As Integer = 0, Optional ByRef lModuliExt As Integer = 0, _
                             Optional ByRef lModuliSup As Integer = 0, Optional ByRef lModuliSupExt As Integer = 0, _
                             Optional ByRef lModuliPtn As Integer = 0, Optional ByRef lModuliPtnExt As Integer = 0) As Boolean
    'nuova gestione: non testo più la chiave di attivazione, ma gli entity sono gestiti solo da insg + anazmod,
    'diversamente sarebbe un problema con le chiavi multiple, dove ad esempio nella chiave base c'è la CA,
    'nella chiave secondaria dove è presente solo il retail deve essere alimentata anche la CA, ma dall'installazione
    'retail non si deve poter accedere ai programmi di CA

    '-------------------------------
    'test se il programma è eseguibile in base ai moduli acquistati
    Dim myObj As Type = obj.GetType()

    Dim myPropertyInfo As PropertyInfo = myObj.GetProperty("Moduli", GetType(Integer))
    Dim myPropertyInfoExt As PropertyInfo = myObj.GetProperty("ModuliExt", GetType(Integer))
    Dim myPropertyInfoSup As PropertyInfo = myObj.GetProperty("ModuliSup", GetType(Integer))
    Dim myPropertyInfoSupExt As PropertyInfo = myObj.GetProperty("ModuliSupExt", GetType(Integer))
    Dim myPropertyInfoPtn As PropertyInfo = myObj.GetProperty("ModuliPtn", GetType(Integer))
    Dim myPropertyInfoPtnExt As PropertyInfo = myObj.GetProperty("ModuliPtnExt", GetType(Integer))

    If strPrgName = "" Then strPrgName = myObj.ToString

    strError = ""
    If Not myPropertyInfo Is Nothing Then
      'Se il metodo è stato ereditato non consento di proseguire.
      If Not (ControlloClasseEreditata(myPropertyInfo.DeclaringType.FullName) AndAlso _
              ControlloClasseEreditata(myPropertyInfoExt.DeclaringType.FullName) AndAlso _
              ControlloClasseEreditata(myPropertyInfoSup.DeclaringType.FullName) AndAlso _
              ControlloClasseEreditata(myPropertyInfoSupExt.DeclaringType.FullName) AndAlso _
              ControlloClasseEreditata(myPropertyInfoPtn.DeclaringType.FullName) AndAlso _
              ControlloClasseEreditata(myPropertyInfoPtnExt.DeclaringType.FullName)) Then
        strError = oApp.TransMsg("BN__CHILD", 130147705396093750, "Nel child '|" & strPrgName & "|' una delle proprietà 'Moduli', 'ModuliExt', ecc... è stata ereditata." & vbCrLf & "Per nessun motivo è possibile ereditare tali proprietà." & vbCrLf & "Il programma verrà chiuso.")
        Return False
      End If

      Try
        lModuli = NTSCInt(myPropertyInfo.GetValue(obj, Nothing))
        lModuliExt = NTSCInt(myPropertyInfoExt.GetValue(obj, Nothing))
        lModuliSup = NTSCInt(myPropertyInfoSup.GetValue(obj, Nothing))
        lModuliSupExt = NTSCInt(myPropertyInfoSupExt.GetValue(obj, Nothing))
        lModuliPtn = NTSCInt(myPropertyInfoPtn.GetValue(obj, Nothing))
        lModuliPtnExt = NTSCInt(myPropertyInfoPtnExt.GetValue(obj, Nothing))
      Catch ex As Exception
        strError = oApp.TransMsg("BN__CHILD", 129540051159462891, "Nel child '|" & strPrgName & "|' le property 'Moduli' e 'ModuliExt' non sono state dichiarate. Il child verrà chiuso")
        Return False
      End Try

      If strDittaCorrente = "" And strPrgName = "NTSInformatica.CLE__ANAG" Then
        'database appena creato e si sta cercando di creare l'anagrafica generale per usarla per creare la ditta
      Else
        If lModuli <> CLN__STD.bsModAll AndAlso lModuliExt <> CLN__STD.bsModExtAll AndAlso _
                 lModuliSup <> CLN__STD.bsModSupAll AndAlso lModuliSupExt <> CLN__STD.bsModSupExtAll AndAlso _
                 lModuliPtn <> CLN__STD.bsModPtnAll AndAlso lModuliPtnExt <> CLN__STD.bsModPtnExtAll Then
          If CBool(ModuliDittaDitt(strDittaCorrente) And lModuli) = False AndAlso _
             CBool(ModuliExtDittaDitt(strDittaCorrente) And lModuliExt) = False AndAlso _
             CBool(ModuliSupDittaDitt(strDittaCorrente) And lModuliSup) = False AndAlso _
             CBool(ModuliSupExtDittaDitt(strDittaCorrente) And lModuliSupExt) = False AndAlso _
             CBool(ModuliPtnDittaDitt(strDittaCorrente) And lModuliPtn) = False AndAlso _
             CBool(ModuliPtnExtDittaDitt(strDittaCorrente) And lModuliPtnExt) = False Then
            strError = oApp.TransMsg("BN__CHILD", 129540052793251953, "Il programma '|" & strPrgName & "|' non è eseguibile in quanto non fa parte di almeno uno dei moduli acquistati")
            Return False
          Else
            'tutto ok: posso continuare
          End If
        End If
      End If
    End If
    Return True
  End Function
  Private Function ControlloClasseEreditata(ByVal strNomeClasse As String) As Boolean
    Return strNomeClasse.StartsWith("NTSInformatica.FRM", StringComparison.CurrentCultureIgnoreCase) OrElse _
           strNomeClasse.StartsWith("NTSInformatica.CLE", StringComparison.CurrentCultureIgnoreCase) OrElse _
           strNomeClasse.StartsWith("NTSInformatica.CLD", StringComparison.CurrentCultureIgnoreCase) OrElse _
           strNomeClasse = "NTSInformatica.fmGriFind"
  End Function


#Region "File di log"
  Public Overridable Sub AddDirSep(ByRef strPathName As String)
    Try
      If Right(Trim(strPathName), Len("/")) <> "/" And _
         Right(Trim(strPathName), Len("\")) <> "\" Then
        strPathName = RTrim$(strPathName) & "\"
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
  End Sub

  Public Overridable Function LogStart(ByVal strDescr As String) As Boolean
    Try
      'assegno il nome 'univoco' del file di log
      Return LogStart(Me.GetType.Name, strDescr, False, False)

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
  Public Overridable Function LogStart(ByVal strFile As String, ByVal strDescr As String) As Boolean
    Try
      '----------------
      'per non far dare il messaggio in testprecompila
      'If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, 

      Return LogStart(strFile, strDescr, False, False)

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
  Public Overridable Function LogStart(ByVal strFile As String, ByVal strDescr As String, ByVal bAppend As Boolean) As Boolean
    Try
      '----------------
      'per non far dare il messaggio in testprecompila
      'If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, 

      Return LogStart(strFile, strDescr, bAppend, False)

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
  Public Overridable Function LogStart(ByVal strFile As String, ByVal strDescr As String, ByVal bAppend As Boolean, ByVal bNoHeader As Boolean) As Boolean
    Try
      '----------------
      'per non far dare il messaggio in testprecompila
      'If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, 

      strLogFile = oApp.Dir & "\" & strFile & "_" & oApp.User.Nome & ".log"
      'se il file è più lungo di 1 mega, cancello (per non riempire il disco)
      If bAppend Then
        Try
          Dim f1 As New FileInfo(strLogFile)
          If f1.Exists Then
            If f1.Length > 1000000 Then f1.Delete()
          End If
        Catch ex As Exception
          'non gestisco l'errore: potrebbe essere il caso di terminal server dove più utenti scrivono sullo stesso file ...
        End Try
      End If

      lw1 = New StreamWriter(strLogFile, bAppend)

      If bNoHeader = False Then
        lw1.WriteLine("--------------------------------------------------------------------")
        If Me.GetType.Assembly.Location.Trim <> "" Then
          lw1.WriteLine(Me.GetType.Assembly.GetName.Name & " Versione " & System.Diagnostics.FileVersionInfo.GetVersionInfo(Me.GetType.Module.FullyQualifiedName).FileVersion)
        Else
          'se è una dll compilata in memoria (può essere solo Business File di NTS Project) prendo la dir della dll standard
          lw1.WriteLine(Me.GetType.BaseType.Assembly.GetName.Name & " Versione " & System.Diagnostics.FileVersionInfo.GetVersionInfo(Me.GetType.BaseType.Module.FullyQualifiedName).FileVersion)
        End If
        If Not ocldBase Is Nothing Then
          If ocldBase.GetType.Assembly.Location.Trim <> "" Then
            lw1.WriteLine(ocldBase.GetType.Assembly.GetName.Name & " Versione " & System.Diagnostics.FileVersionInfo.GetVersionInfo(ocldBase.GetType.Module.FullyQualifiedName).FileVersion)
          Else
            'se è una dll compilata in memoria (può essere solo Business File di NTS Project) prendo la dir della dll standard
            lw1.WriteLine(ocldBase.GetType.BaseType.Assembly.GetName.Name & " Versione " & System.Diagnostics.FileVersionInfo.GetVersionInfo(ocldBase.GetType.BaseType.Module.FullyQualifiedName).FileVersion)
          End If
        End If

        lw1.WriteLine(strDescr)
        lw1.WriteLine("Operazione avviata il " + NTSCStr(Now()))
        lw1.WriteLine("--------------------------------------------------------------------")
        nCountLog = 0
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "File name: '" & strLogFile & "'", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "File name: '" & strLogFile & "'", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function LogWrite(ByVal strMsg As String, ByVal bIncrementaCount As Boolean) As Boolean
    Try
      If bIncrementaCount = True Then nCountLog += 1
      If Not lw1 Is Nothing Then lw1.WriteLine("--> " & strMsg)

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
  Public Overridable Function LogStop() As Boolean
    Try
      Return LogStop(False)
    Catch ex As Exception
      Return True
    End Try
  End Function
  Public Overridable Function LogStop(ByVal bNoMsgChiusura As Boolean) As Boolean
    Try
      '----------------
      'per non far dare il messaggio in testprecompila
      'If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, 

      If bNoMsgChiusura = False Then
        lw1.WriteLine("--------------------------------------------------------------------")
        lw1.WriteLine("Operazione terminata il " + NTSCStr(Now()))
        lw1.WriteLine("--------------------------------------------------------------------")
      End If
      lw1.Flush()
      lw1.Close()
      Return True

    Catch ex As Exception
      Return True
    End Try
  End Function
  Public ReadOnly Property LogError() As Boolean
    Get
      Return (nCountLog <> 0)
    End Get
  End Property
  Public ReadOnly Property LogFileName() As String
    Get
      'ritorno il nome del file di log creato nella LogStart
      Return strLogFile
    End Get
  End Property
#End Region

#Region "moduli abilitati per la ditta passata in input"
  Public Function ModuliDittaDitt(ByVal strCodditt As String) As Integer
    Dim i As Integer
    Dim e As Integer
    Dim strError As String = ""
    Dim dttTmp As DataTable = New DataTable
    Dim lModuliDitta As Integer

    Try
      'Imposta la variable moduli per la ditta
      lModuliDitta = 0

      If ocldBase.LegInstDitt(strCodditt, dttTmp, strError) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", strError))
        lModuliDitta = CLN__STD.bsModAll
      Else

        If dttTmp.Rows.Count >= 0 Then
          For i = 0 To dttTmp.Rows.Count - 1
            Select Case dttTmp.Rows(i)!cod.ToString.Substring(0, 7)
              Case "tb_mod_"
                If dttTmp.Rows(i)!val.ToString = "S" Then
                  e = NTSCInt(dttTmp.Rows(i)!cod.ToString.Substring(dttTmp.Rows(i)!cod.ToString.Length - 2))
                  If oApp.ActKey.MultiKey = "N" Then
                    If CBool(NTSCInt((2 ^ (e - 1))) And oApp.ActKey.ModuliAcquistati) Then lModuliDitta = lModuliDitta + NTSCInt((2 ^ (e - 1)))
                  Else
                    lModuliDitta = lModuliDitta + NTSCInt((2 ^ (e - 1)))
                  End If
                End If
              Case "tb_mod2"
                If dttTmp.Rows(i)!val.ToString = "S" Then
                  e = NTSCInt(dttTmp.Rows(i)!cod.ToString.Substring(dttTmp.Rows(i)!cod.ToString.Length - 2))
                  If oApp.ActKey.MultiKey = "N" Then
                    If CBool(NTSCInt((2 ^ (e + 15 - 1))) And oApp.ActKey.ModuliAcquistati) Then lModuliDitta = lModuliDitta + NTSCInt((2 ^ (e + 15 - 1)))
                  Else
                    lModuliDitta = lModuliDitta + NTSCInt((2 ^ (e + 15 - 1)))
                  End If
                End If
            End Select
          Next
        End If
      End If

      Return lModuliDitta
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Function ModuliExtDittaDitt(ByVal strCodditt As String) As Integer
    Dim i As Integer
    Dim e As Integer
    Dim strError As String = ""
    Dim dttTmp As DataTable = New DataTable
    Dim lModuliExtDitta As Integer

    Try
      'Imposta la variable moduli per la ditta
      lModuliExtDitta = 0

      If ocldBase.LegInstDitt(strCodditt, dttTmp, strError) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", strError))
        lModuliExtDitta = 0
      Else

        If dttTmp.Rows.Count >= 0 Then
          For i = 0 To dttTmp.Rows.Count - 1
            Select Case dttTmp.Rows(i)!cod.ToString.Substring(0, 7)
              Case "tb_mod3"
                If dttTmp.Rows(i)!val.ToString = "S" Then
                  e = NTSCInt(dttTmp.Rows(i)!cod.ToString.Substring(dttTmp.Rows(i)!cod.ToString.Length - 2))
                  If oApp.ActKey.MultiKey = "N" Then
                    If CBool(NTSCInt((2 ^ (e - 1))) And oApp.ActKey.ModuliExtAcquistati) Then lModuliExtDitta = lModuliExtDitta + NTSCInt((2 ^ (e - 1)))
                  Else
                    lModuliExtDitta = lModuliExtDitta + NTSCInt((2 ^ (e - 1)))
                  End If
                End If
              Case "tb_mod4"
                If dttTmp.Rows(i)!val.ToString = "S" Then
                  e = NTSCInt(dttTmp.Rows(i)!cod.ToString.Substring(dttTmp.Rows(i)!cod.ToString.Length - 2))
                  If oApp.ActKey.MultiKey = "N" Then
                    If CBool(NTSCInt((2 ^ (e + 15 - 1))) And oApp.ActKey.ModuliExtAcquistati) Then lModuliExtDitta = lModuliExtDitta + NTSCInt((2 ^ (e + 15 - 1)))
                  Else
                    lModuliExtDitta = lModuliExtDitta + NTSCInt((2 ^ (e + 15 - 1)))
                  End If
                End If
            End Select
          Next
        End If
      End If

      Return lModuliExtDitta
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Function ModuliSupDittaDitt(ByVal strCodditt As String) As Integer
    Dim i As Integer
    Dim e As Integer
    Dim strError As String = ""
    Dim dttTmp As DataTable = New DataTable
    Dim lModuliSupDitta As Integer

    Try
      'Imposta la variable moduli per la ditta
      lModuliSupDitta = 0

      If ocldBase.LegInstDitt(strCodditt, dttTmp, strError) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", strError))
        lModuliSupDitta = 0
      Else
        If dttTmp.Rows.Count >= 0 Then
          For i = 0 To dttTmp.Rows.Count - 1
            Select Case dttTmp.Rows(i)!cod.ToString.PadRight(10).Substring(0, 10)
              Case "tb_modsup_"
                If dttTmp.Rows(i)!val.ToString = "S" Then
                  e = NTSCInt(dttTmp.Rows(i)!cod.ToString.Substring(10))
                  If oApp.ActKey.MultiKey = "N" Then
                    If CBool(NTSCInt((2 ^ (e - 1))) And oApp.ActKey.ModuliSupAcquistati) Then lModuliSupDitta = lModuliSupDitta + NTSCInt((2 ^ (e - 1)))
                  Else
                    lModuliSupDitta = lModuliSupDitta + NTSCInt((2 ^ (e - 1)))
                  End If
                End If
            End Select
          Next
        End If
      End If

      Return lModuliSupDitta
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Function ModuliSupExtDittaDitt(ByVal strCodditt As String) As Integer
    Dim i As Integer
    Dim e As Integer
    Dim strError As String = ""
    Dim dttTmp As DataTable = New DataTable
    Dim lModuliSupExtDitta As Integer

    Try
      'Imposta la variable moduli per la ditta
      lModuliSupExtDitta = 0

      If ocldBase.LegInstDitt(strCodditt, dttTmp, strError) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", strError))
        lModuliSupExtDitta = 0
      Else
        If dttTmp.Rows.Count >= 0 Then
          For i = 0 To dttTmp.Rows.Count - 1
            Select Case dttTmp.Rows(i)!cod.ToString.PadRight(13).Substring(0, 13)
              Case "tb_modsupext_"
                If dttTmp.Rows(i)!val.ToString = "S" Then
                  e = NTSCInt(dttTmp.Rows(i)!cod.ToString.Substring("tb_modsupext_".Length))
                  If oApp.ActKey.MultiKey = "N" Then
                    If CBool(NTSCInt((2 ^ (e - 1))) And oApp.ActKey.ModuliSupAcquistati) Then lModuliSupExtDitta = lModuliSupExtDitta + NTSCInt((2 ^ (e - 1)))
                  Else
                    lModuliSupExtDitta = lModuliSupExtDitta + NTSCInt((2 ^ (e - 1)))
                  End If
                End If
            End Select
          Next
        End If
      End If

      Return lModuliSupExtDitta
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Function ModuliPtnDittaDitt(ByVal strCodditt As String) As Integer
    Dim i As Integer
    Dim e As Integer
    Dim strError As String = ""
    Dim dttTmp As DataTable = New DataTable
    Dim lModuliPtnDitta As Integer

    Try
      'Imposta la variable moduli per la ditta
      lModuliPtnDitta = 0

      If ocldBase.LegInstDitt(strCodditt, dttTmp, strError) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", strError))
        lModuliPtnDitta = 0
      Else

        If dttTmp.Rows.Count >= 0 Then
          For i = 0 To dttTmp.Rows.Count - 1
            Select Case dttTmp.Rows(i)!cod.ToString.PadRight(10).Substring(0, 10)
              Case "tb_modptn_"
                If dttTmp.Rows(i)!val.ToString = "S" Then
                  e = NTSCInt(dttTmp.Rows(i)!cod.ToString.Substring(10))
                  If oApp.ActKey.MultiKey = "N" Then
                    If CBool(NTSCInt((2 ^ (e - 1))) And oApp.ActKey.ModuliPtnAcquistati) Then lModuliPtnDitta = lModuliPtnDitta + NTSCInt((2 ^ (e - 1)))
                  Else
                    lModuliPtnDitta = lModuliPtnDitta + NTSCInt((2 ^ (e - 1)))
                  End If
                End If
            End Select
          Next
        End If
      End If

      Return lModuliPtnDitta
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Function ModuliPtnExtDittaDitt(ByVal strCodditt As String) As Integer
    Dim i As Integer
    Dim e As Integer
    Dim strError As String = ""
    Dim dttTmp As DataTable = New DataTable
    Dim lModuliPtnExtDitta As Integer

    Try
      'Imposta la variable moduli per la ditta
      lModuliPtnExtDitta = 0

      If ocldBase.LegInstDitt(strCodditt, dttTmp, strError) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", strError))
        lModuliPtnExtDitta = 0
      Else
        If dttTmp.Rows.Count >= 0 Then
          For i = 0 To dttTmp.Rows.Count - 1
            Select Case dttTmp.Rows(i)!cod.ToString.PadRight(13).Substring(0, 13)
              Case "tb_modptnext_"
                If dttTmp.Rows(i)!val.ToString = "S" Then
                  e = NTSCInt(dttTmp.Rows(i)!cod.ToString.Substring("tb_modptnext_".Length))
                  If oApp.ActKey.MultiKey = "N" Then
                    If CBool(NTSCInt((2 ^ (e - 1))) And oApp.ActKey.ModuliPtnAcquistati) Then lModuliPtnExtDitta = lModuliPtnExtDitta + NTSCInt((2 ^ (e - 1)))
                  Else
                    lModuliPtnExtDitta = lModuliPtnExtDitta + NTSCInt((2 ^ (e - 1)))
                  End If
                End If
            End Select
          Next
        End If
      End If

      Return lModuliPtnExtDitta
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region


#Region "IDisposable Support"
  Private disposedValue As Boolean = False

  ' IDisposable
  Protected Overridable Sub Dispose(ByVal disposing As Boolean)
    If Not Me.disposedValue Then
      If disposing Then

      End If
    End If
    Me.disposedValue = True
  End Sub

  ' TODO: eseguire l'override di Finalize() solo se Dispose(ByVal disposing As Boolean) dispone del codice per liberare risorse non gestite.
  Protected Overrides Sub Finalize()
    'se non chiamo la dispose dell'oggetto direttamente dal BN (Ad esempio perchè istanziato solo tramite BE)
    'se non eseguo la riga sotto non viene chiamata la dispose!!!
    Dispose(False)
    MyBase.Finalize()
  End Sub

  ' Questo codice è aggiunto da Visual Basic per implementare in modo corretto il modello Disposable.
  Public Sub Dispose() Implements IDisposable.Dispose
    ' Non modificare questo codice. Inserire il codice di pulizia in Dispose(disposing As Boolean).
    Dispose(True)
    GC.SuppressFinalize(Me)
  End Sub
#End Region
End Class
