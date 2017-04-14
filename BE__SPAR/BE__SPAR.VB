Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLE__SPAR
  Inherits CLE__BASN

  Public oCldSpar As CLD__SPAR 'oggetto dal
  Public dsShared As DataSet

  Public bHasChanges As Boolean = False


  Public strElencoCodici As String = ""

  Public strHlvlTabella As String
  Public strHlvlCampo As String
  Public strHlvlValore As String

  Public dsHlvlShared As DataSet
  Public dsGrspShared As DataSet

  Public strGrspSQL As String
  Public strGrspTitle As String
  Public nCurrentCodForm As Integer = 0

  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__SPAR"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldSpar = CType(MyBase.ocldBase, CLD__SPAR)
    oCldSpar.Init(oApp)
    Return True
  End Function

  Public Overridable Function LeggiDatiDitta(ByVal strDitta As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      oCldSpar.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttTmp)
      strDittaCorrente = strDitta
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
  Public Overridable Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim strMsg As String
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldSpar.LeggiTabellaSemplice(strDittaCorrente, "PARSTAG", ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldSpar.SetTableDefaultValueFromDB("PARSTAG", ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("PARSTAG").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("PARSTAG").ColumnChanged, AddressOf AfterColUpdate

      bHasChanges = False

      If ds.Tables("PARSTAG").Rows.Count = 0 Then
        strMsg = "Non esistono voci nella lista dei formati di stampa." & vbCrLf & _
          "Impostare almeno una nuova stampa."
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128327012799392116, strMsg)))
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

  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(oApp.Tr(Me, 128326806006903898, strErr))
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
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso

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

  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property

  Public Overridable Function EspandiFiltri(ByVal strBreve As String, _
                ByVal lOperat1 As Integer, ByVal lOperat2 As Integer, ByVal lOperat3 As Integer, ByVal lOperat4 As Integer, ByVal lOperat5 As Integer, _
                ByVal lOperat6 As Integer, ByVal lOperat7 As Integer, ByVal lOperat8 As Integer, ByVal lOperat9 As Integer, ByVal lOperat10 As Integer, _
                ByVal stredPz_Descam1 As String, ByVal stredPz_Descam2 As String, ByVal stredPz_Descam3 As String, ByVal stredPz_Descam4 As String, ByVal stredPz_Descam5 As String, _
                ByVal stredPz_Descam6 As String, ByVal stredPz_Descam7 As String, ByVal stredPz_Descam8 As String, ByVal stredPz_Descam9 As String, ByVal stredPz_Descam10 As String, _
                ByVal stredPz_Val1 As String, ByVal stredPz_Val2 As String, ByVal stredPz_Val3 As String, ByVal stredPz_Val4 As String, ByVal stredPz_Val5 As String, _
                ByVal stredPz_Val6 As String, ByVal stredPz_Val7 As String, ByVal stredPz_Val8 As String, ByVal stredPz_Val9 As String, ByVal stredPz_Val10 As String) As String
    EspandiFiltri = ""
    Try
      Return EspandiFiltri(strBreve, _
                       lOperat1, lOperat2, lOperat3, lOperat4, lOperat5, _
                       lOperat6, lOperat7, lOperat8, lOperat9, lOperat10, _
                       stredPz_Descam1, stredPz_Descam2, stredPz_Descam3, stredPz_Descam4, stredPz_Descam5, _
                       stredPz_Descam6, stredPz_Descam7, stredPz_Descam8, stredPz_Descam9, stredPz_Descam10, _
                       stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, _
                       stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, _
                       -1, -1, -1, -1, -1, "", "", "", "", "", "", "", "", "", "")
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
  Public Overridable Function EspandiFiltri(ByVal strBreve As String, _
                ByVal lOperat1 As Integer, ByVal lOperat2 As Integer, ByVal lOperat3 As Integer, ByVal lOperat4 As Integer, ByVal lOperat5 As Integer, _
                ByVal lOperat6 As Integer, ByVal lOperat7 As Integer, ByVal lOperat8 As Integer, ByVal lOperat9 As Integer, ByVal lOperat10 As Integer, _
                ByVal stredPz_Descam1 As String, ByVal stredPz_Descam2 As String, ByVal stredPz_Descam3 As String, ByVal stredPz_Descam4 As String, ByVal stredPz_Descam5 As String, _
                ByVal stredPz_Descam6 As String, ByVal stredPz_Descam7 As String, ByVal stredPz_Descam8 As String, ByVal stredPz_Descam9 As String, ByVal stredPz_Descam10 As String, _
                ByVal stredPz_Val1 As String, ByVal stredPz_Val2 As String, ByVal stredPz_Val3 As String, ByVal stredPz_Val4 As String, ByVal stredPz_Val5 As String, _
                ByVal stredPz_Val6 As String, ByVal stredPz_Val7 As String, ByVal stredPz_Val8 As String, ByVal stredPz_Val9 As String, ByVal stredPz_Val10 As String, _
                ByVal lOperat11 As Integer, ByVal lOperat12 As Integer, ByVal lOperat13 As Integer, ByVal lOperat14 As Integer, ByVal lOperat15 As Integer, _
                ByVal stredPz_Descam11 As String, ByVal stredPz_Descam12 As String, ByVal stredPz_Descam13 As String, ByVal stredPz_Descam14 As String, ByVal stredPz_Descam15 As String, _
                ByVal stredPz_Val11 As String, ByVal stredPz_Val12 As String, ByVal stredPz_Val13 As String, ByVal stredPz_Val14 As String, ByVal stredPz_Val15 As String) As String

    Dim strEspansa As String = ""
    Dim bFinito As Boolean
    Dim strCarat As String = ""
    Dim i As Integer
    Dim strTmp As String = ""
    Dim lOperat As Integer

    EspandiFiltri = ""

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strBreve, _
                 lOperat1, lOperat2, lOperat3, lOperat4, lOperat5, _
                 lOperat6, lOperat7, lOperat8, lOperat9, lOperat10, _
                 stredPz_Descam1, stredPz_Descam2, stredPz_Descam3, stredPz_Descam4, stredPz_Descam5, _
                 stredPz_Descam6, stredPz_Descam7, stredPz_Descam8, stredPz_Descam9, stredPz_Descam10, _
                 stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, _
                 stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, _
                 lOperat11, lOperat12, lOperat13, lOperat14, lOperat15, _
                 stredPz_Descam11, stredPz_Descam12, stredPz_Descam13, stredPz_Descam14, stredPz_Descam15, _
                 stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '----------------


      bFinito = False
      i = 1
      While Not bFinito
        strCarat = Mid(strBreve, i, 1).ToUpper
        While strCarat <> "1" And strCarat <> "2" And strCarat <> "3" And strCarat <> "4" And strCarat <> "5" And _
        strCarat <> "6" And strCarat <> "7" And strCarat <> "8" And strCarat <> "9" And strCarat <> "0" And i <= Len(strBreve)
          strEspansa = strEspansa & strCarat
          i = i + 1
          strCarat = Mid(strBreve, i, 1).ToUpper
        End While

        'per i numeri superiori a 10
        If i < Len(strBreve) Then
          If IsNumeric(Mid(strBreve, i + 1, 1).ToUpper) Then
            strCarat += Mid(strBreve, i + 1, 1).ToUpper
            i = i + 1
          End If
        End If
        If strCarat = "0" Then strCarat = "10"

        If i <= Len(strBreve) Then
          Select Case CInt(strCarat)
            Case 1 : lOperat = lOperat1
            Case 2 : lOperat = lOperat2
            Case 3 : lOperat = lOperat3
            Case 4 : lOperat = lOperat4
            Case 5 : lOperat = lOperat5
            Case 6 : lOperat = lOperat6
            Case 7 : lOperat = lOperat7
            Case 8 : lOperat = lOperat8
            Case 9 : lOperat = lOperat9
            Case 10 : lOperat = lOperat10
            Case 11 : lOperat = lOperat11
            Case 12 : lOperat = lOperat12
            Case 13 : lOperat = lOperat13
            Case 14 : lOperat = lOperat14
            Case 15 : lOperat = lOperat15
          End Select
          Select Case CInt(lOperat)
            Case 0 : strTmp = "="
            Case 1 : strTmp = "<>"
            Case 2 : strTmp = ">"
            Case 3 : strTmp = ">="
            Case 4 : strTmp = "<"
            Case 5 : strTmp = "<="
            Case 6 : strTmp = "Like"
            Case 7 : strTmp = "IS NULL"
            Case 8 : strTmp = "IS NOT NULL"
          End Select
          Select Case CInt(strCarat)
            Case 1 : strEspansa = strEspansa & " {" & stredPz_Descam1 & "} " & strTmp & " " & stredPz_Val1
            Case 2 : strEspansa = strEspansa & " {" & stredPz_Descam2 & "} " & strTmp & " " & stredPz_Val2
            Case 3 : strEspansa = strEspansa & " {" & stredPz_Descam3 & "} " & strTmp & " " & stredPz_Val3
            Case 4 : strEspansa = strEspansa & " {" & stredPz_Descam4 & "} " & strTmp & " " & stredPz_Val4
            Case 5 : strEspansa = strEspansa & " {" & stredPz_Descam5 & "} " & strTmp & " " & stredPz_Val5
            Case 6 : strEspansa = strEspansa & " {" & stredPz_Descam6 & "} " & strTmp & " " & stredPz_Val6
            Case 7 : strEspansa = strEspansa & " {" & stredPz_Descam7 & "} " & strTmp & " " & stredPz_Val7
            Case 8 : strEspansa = strEspansa & " {" & stredPz_Descam8 & "} " & strTmp & " " & stredPz_Val8
            Case 9 : strEspansa = strEspansa & " {" & stredPz_Descam9 & "} " & strTmp & " " & stredPz_Val9
            Case 10 : strEspansa = strEspansa & " {" & stredPz_Descam10 & "} " & strTmp & " " & stredPz_Val10
            Case 11 : strEspansa = strEspansa & " {" & stredPz_Descam11 & "} " & strTmp & " " & stredPz_Val11
            Case 12 : strEspansa = strEspansa & " {" & stredPz_Descam12 & "} " & strTmp & " " & stredPz_Val12
            Case 13 : strEspansa = strEspansa & " {" & stredPz_Descam13 & "} " & strTmp & " " & stredPz_Val13
            Case 14 : strEspansa = strEspansa & " {" & stredPz_Descam14 & "} " & strTmp & " " & stredPz_Val14
            Case 15 : strEspansa = strEspansa & " {" & stredPz_Descam15 & "} " & strTmp & " " & stredPz_Val15
          End Select
          i = i + 1
        Else
          bFinito = True
        End If
      End While
      EspandiFiltri = strEspansa

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
  Public Overridable Function EspandiFiltriS(ByVal strBreve As String, ByVal lOperat1 As Integer, _
    ByVal lOperat2 As Integer, ByVal lOperat3 As Integer, ByVal lOperat4 As Integer, ByVal lOperat5 As Integer, _
    ByVal stredPz_Descam1 As String, ByVal stredPz_Descam2 As String, ByVal stredPz_Descam3 As String, _
    ByVal stredPz_Descam4 As String, ByVal stredPz_Descam5 As String, _
    ByVal stredPz_Val1 As String, ByVal stredPz_Val2 As String, ByVal stredPz_Val3 As String, _
    ByVal stredPz_Val4 As String, ByVal stredPz_Val5 As String) As String
    Dim bFinito As Boolean = False
    Dim i As Integer = 0
    Dim lOperat As Integer = 0
    Dim strEspansa As String = ""
    Dim strCarat As String = ""
    Dim strTmp As String = ""

    EspandiFiltriS = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bFinito = False
      i = 1
      '--------------------------------------------------------------------------------------------------------------
      While Not bFinito
        '------------------------------------------------------------------------------------------------------------
        strCarat = Mid(strBreve, i, 1).ToUpper
        While strCarat <> "1" And strCarat <> "2" And strCarat <> "3" And strCarat <> "4" And strCarat <> "5" And _
          i <= strBreve.Length
          strEspansa += strCarat
          i += 1
          strCarat = Mid(strBreve, i, 1).ToUpper
        End While
        '------------------------------------------------------------------------------------------------------------
        '--- Per i numeri superiori a 5
        '------------------------------------------------------------------------------------------------------------
        If i < strBreve.Length Then
          If IsNumeric(Mid(strBreve, i + 1, 1).ToUpper) Then
            strCarat += Mid(strBreve, i + 1, 1).ToUpper
            i += 1
          End If
        End If
        If strCarat = "0" Then strCarat = "10"
        '------------------------------------------------------------------------------------------------------------
        If i <= strBreve.Length Then
          Select Case CInt(strCarat)
            Case 1 : lOperat = lOperat1
            Case 2 : lOperat = lOperat2
            Case 3 : lOperat = lOperat3
            Case 4 : lOperat = lOperat4
            Case 5 : lOperat = lOperat5
          End Select
          Select Case CInt(lOperat)
            Case 0 : strTmp = "="
            Case 1 : strTmp = "<>"
            Case 2 : strTmp = ">"
            Case 3 : strTmp = ">="
            Case 4 : strTmp = "<"
            Case 5 : strTmp = "<="
            Case 6 : strTmp = "Like"
            Case 7 : strTmp = "IS NULL"
            Case 8 : strTmp = "IS NOT NULL"
          End Select
          Select Case CInt(strCarat)
            Case 1 : strEspansa += " {" & stredPz_Descam1 & "} " & strTmp & " " & stredPz_Val1
            Case 2 : strEspansa += " {" & stredPz_Descam2 & "} " & strTmp & " " & stredPz_Val2
            Case 3 : strEspansa += " {" & stredPz_Descam3 & "} " & strTmp & " " & stredPz_Val3
            Case 4 : strEspansa += " {" & stredPz_Descam4 & "} " & strTmp & " " & stredPz_Val4
            Case 5 : strEspansa += " {" & stredPz_Descam5 & "} " & strTmp & " " & stredPz_Val5
          End Select
          i += 1
        Else
          bFinito = True
        End If
      End While
      '--------------------------------------------------------------------------------------------------------------
      Return strEspansa
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function PrimaParola(ByVal strPara As String) As String
    'Restituisce la prima parola di strPara compresa fra spazi oppure
    'fra CR oppure fra LF
    Dim i As Integer
    Dim nLungh As Integer
    Dim lIniParola As Integer
    Dim lFinParola As Integer

    PrimaParola = ""

    Try
      nLungh = Len(strPara)
      i = 1
      While i <= nLungh And (Mid(strPara, i, 1) = " " Or _
            Mid(strPara, i, 1) = vbCr Or Mid(strPara, i, 1) = vbLf)
        i = i + 1
      End While
      lIniParola = i
      While i <= nLungh And (Mid(strPara, i, 1) <> " " And Mid(strPara, i, 1) <> vbCr And _
            Mid(strPara, i, 1) <> vbLf)
        i = i + 1
      End While
      lFinParola = i - 1
      PrimaParola = Mid(strPara, lIniParola, lFinParola - lIniParola + 1)

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
  Public Overridable Function LeggiOpzioniUt() As String
    'DA NET 2015 utilizza PARSTAGACC
    LeggiOpzioniUt = ""
    strElencoCodici = ""
    Dim dttTmp As New DataTable
    Try
      ''-----------------------------------------------------------------------------------------
      ''--- Legge l'opzione di registro che contiene l'elenco dei codici che possono apparire
      ''--- nel ComboBox della lista delle stampe impostate
      ''-----------------------------------------------------------------------------------------
      'strElencoCodici = Trim(oCldSpar.GetSettingBus("BS--SPAR", "OPZIONIUT", ".", "CanExecute", "", " ", ""))
      'If strElencoCodici <> "" Then
      '  '--- Sostituisce i punto e virgola con virgole
      '  strElencoCodici = Trim(Replace(strElencoCodici, ";", ","))
      '  '--- Toglie l'eventuale virgola finale
      '  If Right(strElencoCodici, 1) = "," Then
      '    strElencoCodici = Trim(Mid(strElencoCodici, 1, (Len(strElencoCodici) - 1)))
      '  End If
      'End If

      '--------------------------------------
      'copio gli eventuali vecchi valori dall'opzione di registro BS--SPAR/OPZIONIUT/CanExecute
      If Not oCldSpar.RiempiParstagacc() Then Return ""

      'leggo le stampe abilitate per questo utente o ruolo
      If Not oCldSpar.GetParstagacc(dttTmp) Then Return ""
      For Each dtrT As DataRow In dttTmp.Rows
        strElencoCodici += NTSCStr(dtrT!pza_codform) & ","
      Next
      If strElencoCodici.Length > 0 Then
        strElencoCodici = strElencoCodici.Substring(0, strElencoCodici.Length - 1)
      End If

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

  Public Overridable Function GetCodici(ByRef ds As DataSet) As Boolean
    Try
      Return oCldSpar.GetCodici(ds, strElencoCodici)
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

  Public Overridable Function Esegui(ByVal strSQL As String, ByRef nRec As Integer) As Boolean
    Try
      Return oCldSpar.Esegui(strSQL, nRec)
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

  Public Overridable Sub SalvaValore(ByVal i As Integer, ByVal strPz_Codform As String, ByVal strValore As String)
    Try
      Dim dtrTmp As DataRow() = dsShared.Tables("PARSTAG").Select("Pz_Codform = " & strPz_Codform)
      Dim j As Integer = 0
      For j = 0 To dtrTmp.Length - 1
        Select Case i
          Case 1
            dtrTmp(j)!Pz_Val1 = strValore
          Case 2
            dtrTmp(j)!Pz_Val2 = strValore
          Case 3
            dtrTmp(j)!Pz_Val3 = strValore
          Case 4
            dtrTmp(j)!Pz_Val4 = strValore
          Case 5
            dtrTmp(j)!Pz_Val5 = strValore
          Case 6
            dtrTmp(j)!Pz_Val6 = strValore
          Case 7
            dtrTmp(j)!Pz_Val7 = strValore
          Case 8
            dtrTmp(j)!Pz_Val8 = strValore
          Case 9
            dtrTmp(j)!Pz_Val9 = strValore
          Case 10
            dtrTmp(j)!Pz_Val10 = strValore
          Case 11
            dtrTmp(j)!Pz_Val11 = strValore
          Case 12
            dtrTmp(j)!Pz_Val12 = strValore
          Case 13
            dtrTmp(j)!Pz_Val13 = strValore
          Case 14
            dtrTmp(j)!Pz_Val14 = strValore
          Case 15
            dtrTmp(j)!Pz_Val15 = strValore
        End Select
      Next

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

  Public Overridable Function ControllaValoreCampo(ByVal strCbParstag As String, _
            ByVal strcbPz_Op1 As String, ByVal strcbPz_Op2 As String, ByVal strcbPz_Op3 As String, ByVal strcbPz_Op4 As String, ByVal strcbPz_Op5 As String, _
            ByVal strcbPz_Op6 As String, ByVal strcbPz_Op7 As String, ByVal strcbPz_Op8 As String, ByVal strcbPz_Op9 As String, ByVal strcbPz_Op10 As String, _
            ByVal stredPz_Descam1 As String, ByVal stredPz_Descam2 As String, ByVal stredPz_Descam3 As String, ByVal stredPz_Descam4 As String, ByVal stredPz_Descam5 As String, _
            ByVal stredPz_Descam6 As String, ByVal stredPz_Descam7 As String, ByVal stredPz_Descam8 As String, ByVal stredPz_Descam9 As String, ByVal stredPz_Descam10 As String, _
            ByVal stredPz_Val1 As String, ByVal stredPz_Val2 As String, ByVal stredPz_Val3 As String, ByVal stredPz_Val4 As String, ByVal stredPz_Val5 As String, _
            ByVal stredPz_Val6 As String, ByVal stredPz_Val7 As String, ByVal stredPz_Val8 As String, ByVal stredPz_Val9 As String, ByVal stredPz_Val10 As String) As Boolean
    Try
      Return ControllaValoreCampo(strCbParstag, _
                   strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, _
                   strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, _
                   stredPz_Descam1, stredPz_Descam2, stredPz_Descam3, stredPz_Descam4, stredPz_Descam5, _
                   stredPz_Descam6, stredPz_Descam7, stredPz_Descam8, stredPz_Descam9, stredPz_Descam10, _
                   stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, _
                   stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, _
                   "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
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
  Public Overridable Function ControllaValoreCampo(ByVal strCbParstag As String, _
              ByVal strcbPz_Op1 As String, ByVal strcbPz_Op2 As String, ByVal strcbPz_Op3 As String, ByVal strcbPz_Op4 As String, ByVal strcbPz_Op5 As String, _
              ByVal strcbPz_Op6 As String, ByVal strcbPz_Op7 As String, ByVal strcbPz_Op8 As String, ByVal strcbPz_Op9 As String, ByVal strcbPz_Op10 As String, _
              ByVal stredPz_Descam1 As String, ByVal stredPz_Descam2 As String, ByVal stredPz_Descam3 As String, ByVal stredPz_Descam4 As String, ByVal stredPz_Descam5 As String, _
              ByVal stredPz_Descam6 As String, ByVal stredPz_Descam7 As String, ByVal stredPz_Descam8 As String, ByVal stredPz_Descam9 As String, ByVal stredPz_Descam10 As String, _
              ByVal stredPz_Val1 As String, ByVal stredPz_Val2 As String, ByVal stredPz_Val3 As String, ByVal stredPz_Val4 As String, ByVal stredPz_Val5 As String, _
              ByVal stredPz_Val6 As String, ByVal stredPz_Val7 As String, ByVal stredPz_Val8 As String, ByVal stredPz_Val9 As String, ByVal stredPz_Val10 As String, _
              ByVal strcbPz_Op11 As String, ByVal strcbPz_Op12 As String, ByVal strcbPz_Op13 As String, ByVal strcbPz_Op14 As String, ByVal strcbPz_Op15 As String, _
              ByVal stredPz_Descam11 As String, ByVal stredPz_Descam12 As String, ByVal stredPz_Descam13 As String, ByVal stredPz_Descam14 As String, ByVal stredPz_Descam15 As String, _
              ByVal stredPz_Val11 As String, ByVal stredPz_Val12 As String, ByVal stredPz_Val13 As String, ByVal stredPz_Val14 As String, ByVal stredPz_Val15 As String) As Boolean
    Dim i As Integer
    Dim j As Integer
    Dim strMsg As String
    Dim ds As New DataSet
    Dim strCampo As String
    Dim strPz_Descam As String
    Dim strPz_Val As String
    Dim strPz_Op As String

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCbParstag, _
               strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, _
               strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, _
               stredPz_Descam1, stredPz_Descam2, stredPz_Descam3, stredPz_Descam4, stredPz_Descam5, _
               stredPz_Descam6, stredPz_Descam7, stredPz_Descam8, stredPz_Descam9, stredPz_Descam10, _
               stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, _
               stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, _
               strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15, _
               stredPz_Descam11, stredPz_Descam12, stredPz_Descam13, stredPz_Descam14, stredPz_Descam15, _
               stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '-----------------------------------------------------------------------------------------
      ControllaValoreCampo = False
      '-----------------------------------------------------------------------------------------
      oCldSpar.GetParstag(ds, PrimaParola(strCbParstag).Trim())

      For j = 0 To ds.Tables("PARSTAG").Rows.Count - 1
        For i = 1 To 15
          strPz_Descam = getPz_Descam(i, stredPz_Descam1, stredPz_Descam2, stredPz_Descam3, stredPz_Descam4, stredPz_Descam5, stredPz_Descam6, stredPz_Descam7, stredPz_Descam8, stredPz_Descam9, stredPz_Descam10, stredPz_Descam11, stredPz_Descam12, stredPz_Descam13, stredPz_Descam14, stredPz_Descam15)
          strPz_Val = getPz_Val(i, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15)
          strPz_Op = getcbPz_Op(i, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)

          If NTSCStr(strPz_Descam).Trim = "" Then
          Else
            strCampo = "Pz_Tipo" & i
            Select Case NTSCInt(ds.Tables("PARSTAG").Rows(j)(strCampo))
              Case 3 To 7
                '-------------------------------------------------------------------------------
                '--- Non si può indicare un operatore 'Like' per i campi numerici
                '-------------------------------------------------------------------------------
                If strPz_Op.ToUpper = "LIKE" Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128327605473227370, "Tipo operatore non corretto per i campi di tipo 'numerico'.")))
                  Exit Function
                End If
                '-------------------------------------------------------------------------------
                '--- Se l'operatore è 'IS NULL' o 'IS NOT NULL'
                '--- cancella il contenuto del valore campo, perché tanto non verrebbe considerato
                '--- e quindi è inutile testarlo
                '-------------------------------------------------------------------------------
                If (strPz_Op.ToUpper = "IS NULL") Or _
                   (strPz_Op.ToUpper = "IS NOT NULL") Then
                  strPz_Val.ToUpper()
                Else
                  '-----------------------------------------------------------------------------
                  '--- Sostituisce eventuali punti con virgole
                  '-----------------------------------------------------------------------------
                  strPz_Val = Replace(strPz_Val, ".", ",")
                  '-----------------------------------------------------------------------------
                  '--- Controlla che sia numerico
                  '-----------------------------------------------------------------------------
                  If Not IsNumeric(strPz_Val) Then
                    strMsg = "Attenzione!" & vbCrLf & _
                      "Il valore indicato nel campo '" & NTSCStr(i) & "' non è numerico."
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129077636443994140, strMsg)))
                    Exit Function
                  End If
                  '-----------------------------------------------------------------------------
                  '--- Se ci sono virgole, avvisa ed esce,
                  '--- se non si tratta di campi di tipo 'Valuta' o 'Precisione doppia'
                  '--- in quel caso i decimali possono essere al massimo tre
                  '-----------------------------------------------------------------------------
                  strCampo = "Pz_Tipo" & i
                  If (NTSCInt(ds.Tables("PARSTAG").Rows(j)(strCampo)) = 5) _
                    Or (NTSCInt(ds.Tables("PARSTAG").Rows(j)(strCampo)) = 7) Then
                    If InStr(1, strPz_Val, ",") <> 0 Then
                      If Len(Mid(strPz_Val, (InStr(1, strPz_Val, ",") + 1))) > 3 Then
                        strMsg = "Attenzione!" & vbCrLf & _
                          "Il valore indicato nel campo '" & NTSCStr(i) & "' non può contenere più di 3 decimali."
                        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128327608019976701, strMsg)))
                        Exit Function
                      End If
                    End If
                  Else
                    If InStr(1, strPz_Val, ",") <> 0 Then
                      strMsg = "Attenzione!" & vbCrLf & _
                        "Il valore indicato nel campo '" & NTSCStr(i) & "' non può contenere decimali."
                      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128327610814937024, strMsg)))
                      Exit Function
                    End If
                  End If
                End If
              Case 8
                '-------------------------------------------------------------------------------
                '--- Non si può indicare un operatore 'Like' per i campi data
                '-------------------------------------------------------------------------------
                If strPz_Op.ToUpper = "LIKE" Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128327611804940541, "Tipo operatore non corretto per i campi di tipo 'data'.")))
                  Exit Function
                End If
                '-------------------------------------------------------------------------------
                '--- Se l'operatore è 'IS NULL' o 'IS NOT NULL'
                '--- cancella il contenuto del valore campo, perché tanto non verrebbe considerato
                '--- e quindi è inutile testarlo
                '-------------------------------------------------------------------------------
                If (strPz_Op.ToUpper = "IS NULL") Or _
                   (strPz_Op.ToUpper = "IS NOT NULL") Then
                  strPz_Val = ""
                Else
                  If IsNothing(strPz_Val) Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128662859399687500, "Attenzione!" & vbCrLf & _
                              "Indicare una data nel campo '" & NTSCStr(i) & "'.")))
                    Exit Function
                  End If
                  If Not IsDate(strPz_Val) Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128662859146250000, "Attenzione!" & vbCrLf & _
                              "Indicare una data corretta nel campo '" & NTSCStr(i) & "'.")))
                    Exit Function
                  ElseIf ((NTSCDate(strPz_Val) < NTSCDate("01/01/1900")) Or (NTSCDate(strPz_Val) > NTSCDate("31/12/2099"))) Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128668312086298531, "Attenzione!" & vbCrLf & _
                              "Indicare una data corretta nel campo '" & NTSCStr(i) & "'.")))
                    Exit Function
                  End If
                End If
              Case 10
                '-------------------------------------------------------------------------------
                '--- Se l'operatore è 'IS NULL' o 'IS NOT NULL'
                '--- cancella il contenuto del valore campo, perché tanto non verrebbe considerato
                '--- e quindi è inutile testarlo
                '-------------------------------------------------------------------------------
                If (strPz_Op.ToUpper = "IS NULL") Or _
                   (strPz_Op.ToUpper = "IS NOT NULL") Then
                  strPz_Val = ""
                Else
                  If strPz_Val = "" Then
                    strMsg = "Attenzione!" & vbCrLf & _
                      "Indicare un valore nel campo '" & NTSCStr(i) & "'."
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128327616166948719, strMsg)))
                    Exit Function
                  Else
                    '---------------------------------------------------------------------------
                    '--- Non si possono indicare apici semplici (errore n. 515, error in formula)
                    '---------------------------------------------------------------------------
                    If InStr(1, strPz_Val, "'") <> 0 Then
                      strMsg = "Attenzione!" & vbCrLf & _
                        "Non si possono indicare apici semplici nel campo '" & NTSCStr(i) & "'."
                      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128327616466632383, strMsg)))
                      Exit Function
                    End If
                  End If
                End If
            End Select
          End If
        Next
      Next
      '-----------------------------------------------------------------------------------------
      ControllaValoreCampo = True
      '-----------------------------------------------------------------------------------------
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
  Public Overridable Function ControllaValoreCampoS(ByVal strCbParstag As String, _
    ByVal strcbPz_Ops1 As String, ByVal strcbPz_Ops2 As String, ByVal strcbPz_Ops3 As String, _
    ByVal strcbPz_Ops4 As String, ByVal strcbPz_Ops5 As String, _
    ByVal stredPz_Descams1 As String, ByVal stredPz_Descams2 As String, ByVal stredPz_Descams3 As String, _
    ByVal stredPz_Descams4 As String, ByVal stredPz_Descams5 As String, _
    ByVal stredPz_Vals1 As String, ByVal stredPz_Vals2 As String, ByVal stredPz_Vals3 As String, _
    ByVal stredPz_Vals4 As String, ByVal stredPz_Vals5 As String) As Boolean
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim strCampo As String = ""
    Dim strPz_Descam As String = ""
    Dim strPz_Val As String = ""
    Dim strPz_Op As String = ""
    Dim ds As New DataSet

    Try
      '--------------------------------------------------------------------------------------------------------------
      ControllaValoreCampoS = False
      '--------------------------------------------------------------------------------------------------------------
      oCldSpar.GetParstag(ds, PrimaParola(strCbParstag).Trim())
      '--------------------------------------------------------------------------------------------------------------
      For j = 0 To ds.Tables("PARSTAG").Rows.Count - 1
        For i = 1 To 5
          '----------------------------------------------------------------------------------------------------------
          strPz_Descam = getPz_DescamS(i, stredPz_Descams1, stredPz_Descams2, stredPz_Descams3, stredPz_Descams4, stredPz_Descams5)
          strPz_Val = getPz_ValS(i, stredPz_Vals1, stredPz_Vals2, stredPz_Vals3, stredPz_Vals4, stredPz_Vals5)
          strPz_Op = getcbPz_OpS(i, strcbPz_Ops1, strcbPz_Ops2, strcbPz_Ops3, strcbPz_Ops4, strcbPz_Ops5)
          '----------------------------------------------------------------------------------------------------------
          If NTSCStr(strPz_Descam).Trim <> "" Then
            strCampo = "Pz_Tipos" & i
            Select Case NTSCInt(ds.Tables("PARSTAG").Rows(j)(strCampo))
              Case 3 To 7
                '----------------------------------------------------------------------------------------------------
                '--- Non si può indicare un operatore 'Like' per i campi numerici
                '----------------------------------------------------------------------------------------------------
                If strPz_Op.ToUpper = "LIKE" Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130450435261437491, _
                    "Tipo operatore non corretto per i campi di tipo 'numerico'.")))
                  Return False
                End If
                '----------------------------------------------------------------------------------------------------
                '--- Se l'operatore è 'IS NULL' o 'IS NOT NULL'
                '--- cancella il contenuto del valore campo, perché tanto non verrebbe considerato
                '--- e quindi è inutile testarlo
                '----------------------------------------------------------------------------------------------------
                If (strPz_Op.ToUpper = "IS NULL") Or (strPz_Op.ToUpper = "IS NOT NULL") Then
                  strPz_Val.ToUpper()
                Else
                  '--------------------------------------------------------------------------------------------------
                  '--- Sostituisce eventuali punti con virgole
                  '--------------------------------------------------------------------------------------------------
                  strPz_Val = Replace(strPz_Val, ".", ",")
                  '--------------------------------------------------------------------------------------------------
                  '--- Controlla che sia numerico
                  '--------------------------------------------------------------------------------------------------
                  If Not IsNumeric(strPz_Val) Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130450435951105658, "Attenzione!" & vbCrLf & _
                      "Il valore indicato nel campo '|" & NTSCStr(i) & "|' non è numerico.")))
                    Return False
                  End If
                  '--------------------------------------------------------------------------------------------------
                  '--- Se ci sono virgole, avvisa ed esce,
                  '--- se non si tratta di campi di tipo 'Valuta' o 'Precisione doppia'
                  '--- in quel caso i decimali possono essere al massimo tre
                  '--------------------------------------------------------------------------------------------------
                  strCampo = "Pz_Tipo" & i
                  If (NTSCInt(ds.Tables("PARSTAG").Rows(j)(strCampo)) = 5) _
                    Or (NTSCInt(ds.Tables("PARSTAG").Rows(j)(strCampo)) = 7) Then
                    If InStr(1, strPz_Val, ",") <> 0 Then
                      If Len(Mid(strPz_Val, (InStr(1, strPz_Val, ",") + 1))) > 3 Then
                        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130450436202993734, "Attenzione!" & vbCrLf & _
                          "Il valore indicato nel campo '|" & NTSCStr(i) & "|' non può contenere più di 3 decimali.")))
                        Return False
                      End If
                    End If
                  Else
                    If InStr(1, strPz_Val, ",") <> 0 Then
                      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130450436298167072, "Attenzione!" & vbCrLf & _
                        "Il valore indicato nel campo '|" & NTSCStr(i) & "|' non può contenere decimali.")))
                      Return False
                    End If
                  End If
                End If
              Case 8
                '----------------------------------------------------------------------------------------------------
                '--- Non si può indicare un operatore 'Like' per i campi data
                '----------------------------------------------------------------------------------------------------
                If strPz_Op.ToUpper = "LIKE" Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130450436479353833, _
                    "Tipo operatore non corretto per i campi di tipo 'data'.")))
                  Exit Function
                End If
                '----------------------------------------------------------------------------------------------------
                '--- Se l'operatore è 'IS NULL' o 'IS NOT NULL'
                '--- cancella il contenuto del valore campo, perché tanto non verrebbe considerato
                '--- e quindi è inutile testarlo
                '----------------------------------------------------------------------------------------------------
                If (strPz_Op.ToUpper = "IS NULL") Or (strPz_Op.ToUpper = "IS NOT NULL") Then
                  strPz_Val = ""
                Else
                  If IsNothing(strPz_Val) Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130450436646146426, "Attenzione!" & vbCrLf & _
                      "Indicare una data nel campo '|" & NTSCStr(i) & "|'.")))
                    Return False
                  End If
                  If Not IsDate(strPz_Val) Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130450436740343225, "Attenzione!" & vbCrLf & _
                      "Indicare una data corretta nel campo '|" & NTSCStr(i) & "|'.")))
                    Return False
                  ElseIf ((NTSCDate(strPz_Val) < NTSCDate("01/01/1900")) Or (NTSCDate(strPz_Val) > NTSCDate("31/12/2099"))) Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130450436916842606, "Attenzione!" & vbCrLf & _
                      "Indicare una data corretta nel campo '|" & NTSCStr(i) & "|'.")))
                    Return True
                  End If
                End If
              Case 10
                '----------------------------------------------------------------------------------------------------
                '--- Se l'operatore è 'IS NULL' o 'IS NOT NULL'
                '--- cancella il contenuto del valore campo, perché tanto non verrebbe considerato
                '--- e quindi è inutile testarlo
                '----------------------------------------------------------------------------------------------------
                If (strPz_Op.ToUpper = "IS NULL") Or (strPz_Op.ToUpper = "IS NOT NULL") Then
                  strPz_Val = ""
                Else
                  If strPz_Val = "" Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130450437128770751, "Attenzione!" & vbCrLf & _
                      "Indicare un valore nel campo '|" & NTSCStr(i) & "|'.")))
                    Return True
                  Else
                    '------------------------------------------------------------------------------------------------
                    '--- Non si possono indicare apici semplici (errore n. 515, error in formula)
                    '------------------------------------------------------------------------------------------------
                    If InStr(1, strPz_Val, "'") <> 0 Then
                      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130450437296539880, "Attenzione!" & vbCrLf & _
                        "Non si possono indicare apici semplici nel campo '|" & NTSCStr(i) & "|'.")))
                      Return True
                    End If
                  End If
                End If
            End Select
          End If
        Next
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function CreateQuerySql(ByVal strSQL As String, ByVal strOprel As String, ByVal strPz_Sql As String, ByVal strPz_maintab As String, ByVal strPz_Nomrpt As String, _
          ByVal lOperat1 As Integer, ByVal lOperat2 As Integer, ByVal lOperat3 As Integer, ByVal lOperat4 As Integer, ByVal lOperat5 As Integer, _
          ByVal lOperat6 As Integer, ByVal lOperat7 As Integer, ByVal lOperat8 As Integer, ByVal lOperat9 As Integer, ByVal lOperat10 As Integer, _
          ByVal stredPz_Descam1 As String, ByVal stredPz_Descam2 As String, ByVal stredPz_Descam3 As String, ByVal stredPz_Descam4 As String, ByVal stredPz_Descam5 As String, _
          ByVal stredPz_Descam6 As String, ByVal stredPz_Descam7 As String, ByVal stredPz_Descam8 As String, ByVal stredPz_Descam9 As String, ByVal stredPz_Descam10 As String, _
          ByVal stredPz_Val1 As String, ByVal stredPz_Val2 As String, ByVal stredPz_Val3 As String, ByVal stredPz_Val4 As String, ByVal stredPz_Val5 As String, _
          ByVal stredPz_Val6 As String, ByVal stredPz_Val7 As String, ByVal stredPz_Val8 As String, ByVal stredPz_Val9 As String, ByVal stredPz_Val10 As String, _
          ByVal lPz_Tipo1 As Integer, ByVal lPz_Tipo2 As Integer, ByVal lPz_Tipo3 As Integer, ByVal lPz_Tipo4 As Integer, ByVal lPz_Tipo5 As Integer, _
          ByVal lPz_Tipo6 As Integer, ByVal lPz_Tipo7 As Integer, ByVal lPz_Tipo8 As Integer, ByVal lPz_Tipo9 As Integer, ByVal lPz_Tipo10 As Integer, _
          ByVal strPz_Column1 As String, ByVal strPz_Column2 As String, ByVal strPz_Column3 As String, ByVal strPz_Column4 As String, ByVal strPz_Column5 As String, _
          ByVal strPz_Column6 As String, ByVal strPz_Column7 As String, ByVal strPz_Column8 As String, ByVal strPz_Column9 As String, ByVal strPz_Column10 As String, _
          ByVal strcbPz_Op1 As String, ByVal strcbPz_Op2 As String, ByVal strcbPz_Op3 As String, ByVal strcbPz_Op4 As String, ByVal strcbPz_Op5 As String, _
          ByVal strcbPz_Op6 As String, ByVal strcbPz_Op7 As String, ByVal strcbPz_Op8 As String, ByVal strcbPz_Op9 As String, ByVal strcbPz_Op10 As String) As String
    CreateQuerySql = ""
    Try
      Return CreateQuerySql(strSQL, strOprel, strPz_Sql, strPz_maintab, strPz_Nomrpt, _
           lOperat1, lOperat2, lOperat3, lOperat4, lOperat5, _
           lOperat6, lOperat7, lOperat8, lOperat9, lOperat10, _
           stredPz_Descam1, stredPz_Descam2, stredPz_Descam3, stredPz_Descam4, stredPz_Descam5, _
           stredPz_Descam6, stredPz_Descam7, stredPz_Descam8, stredPz_Descam9, stredPz_Descam10, _
           stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, _
           stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, _
           lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, _
           lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, _
           strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, _
           strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, _
           strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, _
           strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, _
           -1, -1, -1, -1, -1, "", "", "", "", "", "", "", "", "", "", 0, 0, 0, 0, 0, _
           "", "", "", "", "", "", "", "", "", "")
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
  Public Overridable Function CreateQuerySql(ByVal strSQL As String, ByVal strOprel As String, ByVal strPz_Sql As String, ByVal strPz_maintab As String, ByVal strPz_Nomrpt As String, _
    ByVal lOperat1 As Integer, ByVal lOperat2 As Integer, ByVal lOperat3 As Integer, ByVal lOperat4 As Integer, ByVal lOperat5 As Integer, _
    ByVal lOperat6 As Integer, ByVal lOperat7 As Integer, ByVal lOperat8 As Integer, ByVal lOperat9 As Integer, ByVal lOperat10 As Integer, _
    ByVal stredPz_Descam1 As String, ByVal stredPz_Descam2 As String, ByVal stredPz_Descam3 As String, ByVal stredPz_Descam4 As String, ByVal stredPz_Descam5 As String, _
    ByVal stredPz_Descam6 As String, ByVal stredPz_Descam7 As String, ByVal stredPz_Descam8 As String, ByVal stredPz_Descam9 As String, ByVal stredPz_Descam10 As String, _
    ByVal stredPz_Val1 As String, ByVal stredPz_Val2 As String, ByVal stredPz_Val3 As String, ByVal stredPz_Val4 As String, ByVal stredPz_Val5 As String, _
    ByVal stredPz_Val6 As String, ByVal stredPz_Val7 As String, ByVal stredPz_Val8 As String, ByVal stredPz_Val9 As String, ByVal stredPz_Val10 As String, _
    ByVal lPz_Tipo1 As Integer, ByVal lPz_Tipo2 As Integer, ByVal lPz_Tipo3 As Integer, ByVal lPz_Tipo4 As Integer, ByVal lPz_Tipo5 As Integer, _
    ByVal lPz_Tipo6 As Integer, ByVal lPz_Tipo7 As Integer, ByVal lPz_Tipo8 As Integer, ByVal lPz_Tipo9 As Integer, ByVal lPz_Tipo10 As Integer, _
    ByVal strPz_Column1 As String, ByVal strPz_Column2 As String, ByVal strPz_Column3 As String, ByVal strPz_Column4 As String, ByVal strPz_Column5 As String, _
    ByVal strPz_Column6 As String, ByVal strPz_Column7 As String, ByVal strPz_Column8 As String, ByVal strPz_Column9 As String, ByVal strPz_Column10 As String, _
    ByVal strcbPz_Op1 As String, ByVal strcbPz_Op2 As String, ByVal strcbPz_Op3 As String, ByVal strcbPz_Op4 As String, ByVal strcbPz_Op5 As String, _
    ByVal strcbPz_Op6 As String, ByVal strcbPz_Op7 As String, ByVal strcbPz_Op8 As String, ByVal strcbPz_Op9 As String, ByVal strcbPz_Op10 As String, _
    ByVal lOperat11 As Integer, ByVal lOperat12 As Integer, ByVal lOperat13 As Integer, ByVal lOperat14 As Integer, ByVal lOperat15 As Integer, _
    ByVal stredPz_Descam11 As String, ByVal stredPz_Descam12 As String, ByVal stredPz_Descam13 As String, ByVal stredPz_Descam14 As String, ByVal stredPz_Descam15 As String, _
    ByVal stredPz_Val11 As String, ByVal stredPz_Val12 As String, ByVal stredPz_Val13 As String, ByVal stredPz_Val14 As String, ByVal stredPz_Val15 As String, _
    ByVal lPz_Tipo11 As Integer, ByVal lPz_Tipo12 As Integer, ByVal lPz_Tipo13 As Integer, ByVal lPz_Tipo14 As Integer, ByVal lPz_Tipo15 As Integer, _
    ByVal strPz_Column11 As String, ByVal strPz_Column12 As String, ByVal strPz_Column13 As String, ByVal strPz_Column14 As String, ByVal strPz_Column15 As String, _
    ByVal strcbPz_Op11 As String, ByVal strcbPz_Op12 As String, ByVal strcbPz_Op13 As String, ByVal strcbPz_Op14 As String, ByVal strcbPz_Op15 As String) As String
    Try
      '-------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strSQL, strOprel, strPz_Sql, strPz_maintab, strPz_Nomrpt, _
           lOperat1, lOperat2, lOperat3, lOperat4, lOperat5, _
           lOperat6, lOperat7, lOperat8, lOperat9, lOperat10, _
           stredPz_Descam1, stredPz_Descam2, stredPz_Descam3, stredPz_Descam4, stredPz_Descam5, _
           stredPz_Descam6, stredPz_Descam7, stredPz_Descam8, stredPz_Descam9, stredPz_Descam10, _
           stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, _
           stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, _
           lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, _
           lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, _
           strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, _
           strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, _
           strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, _
           strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, _
           lOperat11, lOperat12, lOperat13, lOperat14, lOperat15, _
           stredPz_Descam11, stredPz_Descam12, stredPz_Descam13, stredPz_Descam14, stredPz_Descam15, _
           stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15, _
           lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15, _
           strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15, _
           strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If

      '--------------------------------------------------------------------------------------------------------------
      Return CreateQuerySql(strSQL, strOprel, "", strPz_Sql, strPz_maintab, strPz_Nomrpt, _
        lOperat1, lOperat2, lOperat3, lOperat4, lOperat5, lOperat6, lOperat7, lOperat8, lOperat9, lOperat10, _
        stredPz_Descam1, stredPz_Descam2, stredPz_Descam3, stredPz_Descam4, stredPz_Descam5, _
        stredPz_Descam6, stredPz_Descam7, stredPz_Descam8, stredPz_Descam9, stredPz_Descam10, _
        stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, _
        stredPz_Val8, stredPz_Val9, stredPz_Val10, lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, _
        lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, strPz_Column1, strPz_Column2, strPz_Column3, _
        strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, _
        strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, _
        strcbPz_Op9, strcbPz_Op10, lOperat11, lOperat12, lOperat13, lOperat14, lOperat15, stredPz_Descam11, _
        stredPz_Descam12, stredPz_Descam13, stredPz_Descam14, stredPz_Descam15, stredPz_Val11, stredPz_Val12, _
        stredPz_Val13, stredPz_Val14, stredPz_Val15, lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15, _
        strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15, strcbPz_Op11, _
        strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15, _
        0, 0, 0, 0, 0, "", "", "", "", "", "", "", "", "", "", 0, 0, 0, 0, 0, "", "", "", "", "", "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function CreateQuerySql(ByVal strSQL As String, ByVal strOprel As String, ByVal strOprel2 As String, _
    ByVal strPz_Sql As String, ByVal strPz_maintab As String, ByVal strPz_Nomrpt As String, _
    ByVal lOperat1 As Integer, ByVal lOperat2 As Integer, ByVal lOperat3 As Integer, ByVal lOperat4 As Integer, ByVal lOperat5 As Integer, _
    ByVal lOperat6 As Integer, ByVal lOperat7 As Integer, ByVal lOperat8 As Integer, ByVal lOperat9 As Integer, ByVal lOperat10 As Integer, _
    ByVal stredPz_Descam1 As String, ByVal stredPz_Descam2 As String, ByVal stredPz_Descam3 As String, ByVal stredPz_Descam4 As String, ByVal stredPz_Descam5 As String, _
    ByVal stredPz_Descam6 As String, ByVal stredPz_Descam7 As String, ByVal stredPz_Descam8 As String, ByVal stredPz_Descam9 As String, ByVal stredPz_Descam10 As String, _
    ByVal stredPz_Val1 As String, ByVal stredPz_Val2 As String, ByVal stredPz_Val3 As String, ByVal stredPz_Val4 As String, ByVal stredPz_Val5 As String, _
    ByVal stredPz_Val6 As String, ByVal stredPz_Val7 As String, ByVal stredPz_Val8 As String, ByVal stredPz_Val9 As String, ByVal stredPz_Val10 As String, _
    ByVal lPz_Tipo1 As Integer, ByVal lPz_Tipo2 As Integer, ByVal lPz_Tipo3 As Integer, ByVal lPz_Tipo4 As Integer, ByVal lPz_Tipo5 As Integer, _
    ByVal lPz_Tipo6 As Integer, ByVal lPz_Tipo7 As Integer, ByVal lPz_Tipo8 As Integer, ByVal lPz_Tipo9 As Integer, ByVal lPz_Tipo10 As Integer, _
    ByVal strPz_Column1 As String, ByVal strPz_Column2 As String, ByVal strPz_Column3 As String, ByVal strPz_Column4 As String, ByVal strPz_Column5 As String, _
    ByVal strPz_Column6 As String, ByVal strPz_Column7 As String, ByVal strPz_Column8 As String, ByVal strPz_Column9 As String, ByVal strPz_Column10 As String, _
    ByVal strcbPz_Op1 As String, ByVal strcbPz_Op2 As String, ByVal strcbPz_Op3 As String, ByVal strcbPz_Op4 As String, ByVal strcbPz_Op5 As String, _
    ByVal strcbPz_Op6 As String, ByVal strcbPz_Op7 As String, ByVal strcbPz_Op8 As String, ByVal strcbPz_Op9 As String, ByVal strcbPz_Op10 As String, _
    ByVal lOperat11 As Integer, ByVal lOperat12 As Integer, ByVal lOperat13 As Integer, ByVal lOperat14 As Integer, ByVal lOperat15 As Integer, _
    ByVal stredPz_Descam11 As String, ByVal stredPz_Descam12 As String, ByVal stredPz_Descam13 As String, ByVal stredPz_Descam14 As String, ByVal stredPz_Descam15 As String, _
    ByVal stredPz_Val11 As String, ByVal stredPz_Val12 As String, ByVal stredPz_Val13 As String, ByVal stredPz_Val14 As String, ByVal stredPz_Val15 As String, _
    ByVal lPz_Tipo11 As Integer, ByVal lPz_Tipo12 As Integer, ByVal lPz_Tipo13 As Integer, ByVal lPz_Tipo14 As Integer, ByVal lPz_Tipo15 As Integer, _
    ByVal strPz_Column11 As String, ByVal strPz_Column12 As String, ByVal strPz_Column13 As String, ByVal strPz_Column14 As String, ByVal strPz_Column15 As String, _
    ByVal strcbPz_Op11 As String, ByVal strcbPz_Op12 As String, ByVal strcbPz_Op13 As String, ByVal strcbPz_Op14 As String, ByVal strcbPz_Op15 As String, _
    ByVal lOperats1 As Integer, ByVal lOperats2 As Integer, ByVal lOperats3 As Integer, ByVal lOperats4 As Integer, ByVal lOperats5 As Integer, _
    ByVal stredPz_Descams1 As String, ByVal stredPz_Descams2 As String, ByVal stredPz_Descams3 As String, ByVal stredPz_Descams4 As String, ByVal stredPz_Descams5 As String, _
    ByVal stredPz_Vals1 As String, ByVal stredPz_Vals2 As String, ByVal stredPz_Vals3 As String, ByVal stredPz_Vals4 As String, ByVal stredPz_Vals5 As String, _
    ByVal lPz_Tipos1 As Integer, ByVal lPz_Tipos2 As Integer, ByVal lPz_Tipos3 As Integer, ByVal lPz_Tipos4 As Integer, ByVal lPz_Tipos5 As Integer, _
    ByVal strPz_Columns1 As String, ByVal strPz_Columns2 As String, ByVal strPz_Columns3 As String, ByVal strPz_Columns4 As String, ByVal strPz_Columns5 As String, _
    ByVal strcbPz_Ops1 As String, ByVal strcbPz_Ops2 As String, ByVal strcbPz_Ops3 As String, ByVal strcbPz_Ops4 As String, ByVal strcbPz_Ops5 As String) As String
    Dim strSQL1 As String
    Dim strSQL2 As String
    Dim strData As String
    Dim nPos As Integer
    Dim nPos1 As Integer
    Dim nPos2 As Integer
    Dim nPos3 As Integer
    Dim nFil As Integer
    Dim i As Integer
    Dim nFilsaltati As Integer
    Dim nFilrimvuoti As Integer
    Dim bFinito As Boolean
    Dim bNumocc As Boolean
    Dim nCardinale As Integer
    Dim LungstrOprel As Integer
    Dim strRpt As String
    Dim strSubquery As String = ""

    CreateQuerySql = ""
    Try
      '-------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strSQL, strOprel, strPz_Sql, strPz_maintab, strPz_Nomrpt, _
           lOperat1, lOperat2, lOperat3, lOperat4, lOperat5, _
           lOperat6, lOperat7, lOperat8, lOperat9, lOperat10, _
           stredPz_Descam1, stredPz_Descam2, stredPz_Descam3, stredPz_Descam4, stredPz_Descam5, _
           stredPz_Descam6, stredPz_Descam7, stredPz_Descam8, stredPz_Descam9, stredPz_Descam10, _
           stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, _
           stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, _
           lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, _
           lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, _
           strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, _
           strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, _
           strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, _
           strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, _
           lOperat11, lOperat12, lOperat13, lOperat14, lOperat15, _
           stredPz_Descam11, stredPz_Descam12, stredPz_Descam13, stredPz_Descam14, stredPz_Descam15, _
           stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15, _
           lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15, _
           strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15, _
           strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15, _
           lOperats1, lOperats2, lOperats3, lOperats4, lOperats5, _
           stredPz_Descams1, stredPz_Descams2, stredPz_Descams3, stredPz_Descams4, stredPz_Descams5, _
           stredPz_Vals1, stredPz_Vals2, stredPz_Vals3, stredPz_Vals4, stredPz_Vals5, _
           lPz_Tipos1, lPz_Tipos2, lPz_Tipos3, lPz_Tipos4, lPz_Tipos5, _
           strPz_Columns1, strPz_Columns2, strPz_Columns3, strPz_Columns4, strPz_Columns5, _
           strcbPz_Ops1, strcbPz_Ops2, strcbPz_Ops3, strcbPz_Ops4, strcbPz_Ops5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '-------------------------------------------------------------------------------------------------------------
      strRpt = ""
      '-------------------------------------------------------------------------------------------------------------
      If (stredPz_Descam1 = "") And (stredPz_Descam2 = "") And (stredPz_Descam3 = "") And _
         (stredPz_Descam4 = "") And (stredPz_Descam5 = "") And (stredPz_Descam6 = "") And _
         (stredPz_Descam7 = "") And (stredPz_Descam8 = "") And (stredPz_Descam9 = "") And _
         (stredPz_Descam10 = "") And (stredPz_Descam11 = "") And (stredPz_Descam12 = "") And _
         (stredPz_Descam13 = "") And (stredPz_Descam14 = "") And (stredPz_Descam15 = "") Then
        If NTSCStr(strPz_maintab) <> "" Then
          strSQL1 = ""
          strSQL2 = ""
          If Not Len(strPz_Sql) = 0 Then
            nPos = InStr(1, strPz_Sql, "GROUP BY", CompareMethod.Text)
            If nPos <> 0 Then
              strSQL1 = Left(strSQL, (nPos - 1))
              strSQL2 = Mid(strSQL, nPos)
            Else
              nPos = InStr(1, strPz_Sql, "ORDER BY", CompareMethod.Text)
              If nPos <> 0 Then
                strSQL1 = Left(strSQL, (nPos - 1))
                strSQL2 = Mid(strSQL, nPos)
              Else
                strSQL1 = strSQL
                strSQL2 = ""
              End If
            End If
            If InStr(1, (strSQL1).ToUpper, " WHERE ") = 0 Then
              strSQL1 = strSQL1 & " WHERE (" & strPz_maintab & ".codditt = " & CStrSQL(strDittaCorrente) & ")"
            Else
              strSQL1 = strSQL1 & " AND (" & strPz_maintab & ".codditt = " & CStrSQL(strDittaCorrente) & ")"
            End If
            If (NTSCStr(strPz_Nomrpt).ToUpper) = "PAR0010.RPT" Then
              strSQL1 = strSQL1 & " AND ((anagra.codditt IS NULL) OR (anagra.codditt = '" & strDittaCorrente & "'))"
            End If
            strRpt = strSQL1 & " " & strSQL2
          Else
            strRpt = strPz_Sql
          End If
        Else
          strRpt = strPz_Sql
        End If
        GoTo ValutaSubQuery
      End If
      '--------------------------------------------------------------------------------------------------------------
      nPos = InStr(1, ConvEnterInSpace(strSQL), " GROUP BY ", CompareMethod.Text)
      If nPos <> 0 Then
        strSQL1 = Left(strSQL, nPos - 1)
        strSQL2 = Mid(strSQL, nPos)
      Else
        nPos = InStr(1, ConvEnterInSpace(strSQL), " ORDER BY ", CompareMethod.Text)
        If InStr(1, ConvEnterInSpace(strSQL.ToUpper), "PARTITION BY", CompareMethod.Text) > 0 Then
          nPos3 = InStr(nPos + 1, ConvEnterInSpace(strSQL), " ORDER BY ", CompareMethod.Text)
          If nPos3 <> 0 Then
            strSQL1 = Left(strSQL, nPos3 - 1)
            strSQL2 = Mid(strSQL, nPos3)
          Else
            strSQL1 = strSQL
            strSQL2 = ""
          End If
        Else
          If nPos <> 0 Then
            strSQL1 = Left(strSQL, nPos - 1)
            strSQL2 = Mid(strSQL, nPos)
          Else
            strSQL1 = strSQL
            strSQL2 = ""
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      nPos1 = InStr(1, ConvEnterInSpace(strSQL1), " WHERE ", CompareMethod.Text)

      If nPos1 > 0 Then
        strSQL1 = Left(strSQL1, nPos1 - 1) & " WHERE ( " & Mid(strSQL1, nPos1 + 7)
      End If
      nFil = 0
      nFilsaltati = 0
      For i = 0 To 14
        If getPz_Descam(i + 1, stredPz_Descam1, stredPz_Descam2, stredPz_Descam3, stredPz_Descam4, stredPz_Descam5, _
                        stredPz_Descam6, stredPz_Descam7, stredPz_Descam8, stredPz_Descam9, stredPz_Descam10, _
                        stredPz_Descam11, stredPz_Descam12, stredPz_Descam13, stredPz_Descam14, stredPz_Descam15) <> "" Then
          nFil = nFil + 1
          Select Case getPz_Tipo(i + 1, lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15)
            Case 3 To 7 ' Tipo NUMERICO
              If nPos1 = 0 Then
                strSQL1 = strSQL1 & " WHERE "
                If (getPz_Tipo(i + 1, lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15) = 5) Or (getPz_Tipo(i + 1, lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15) = 7) Then
                  strSQL1 = strSQL1 & " " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & CDblSQL(getPz_Val(i + 1, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15))
                Else
                  strSQL1 = strSQL1 & " " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & getPz_Val(i + 1, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15)
                End If
              Else
                If (getPz_Tipo(i + 1, lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15) = 5) Or (getPz_Tipo(i + 1, lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15) = 7) Then
                  If nFil = 1 Then
                    strSQL1 = strSQL1 & " ) AND ( {{ " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & CDblSQL(getPz_Val(i + 1, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15))
                  Else
                    strSQL1 = strSQL1 & " {" & i & "} " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & CDblSQL(getPz_Val(i + 1, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15))
                  End If
                Else
                  If nFil = 1 Then
                    strSQL1 = strSQL1 & " ) AND ( {{ " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & getPz_Val(i + 1, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15)
                  Else
                    strSQL1 = strSQL1 & " {" & i & "} " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & getPz_Val(i + 1, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15)
                  End If
                End If
              End If
            Case 8 'Tipo DATA
              Select Case getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)
                Case "IS NULL", "IS NOT NULL"
                  If nPos1 = 0 Then
                    strSQL1 = strSQL1 & " WHERE " & _
                      getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)
                  Else
                    If nFil = 1 Then
                      strSQL1 = strSQL1 & ") AND ({{" & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)
                    Else
                      strSQL1 = strSQL1 & " {" & i & "} " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)
                    End If
                  End If
                Case Else
                  strData = CDataSQL(NTSCDate(getPz_Val(i + 1, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15)))
                  If nPos1 = 0 Then
                    strSQL1 = strSQL1 & " WHERE "
                    strSQL1 = strSQL1 & " " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & strData
                  Else
                    If nFil = 1 Then
                      strSQL1 = strSQL1 & " ) AND ( {{ " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & strData
                    Else
                      strSQL1 = strSQL1 & " {" & i & "} " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & strData
                    End If
                  End If
              End Select
            Case 10, 11 ' Tipo STRING
              Select Case getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)
                Case "IS NULL", "IS NOT NULL"
                  If nPos1 = 0 Then
                    strSQL1 = strSQL1 & " WHERE " & _
                      getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)
                  Else
                    If nFil = 1 Then
                      strSQL1 = strSQL1 & ") AND ({{" & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)
                    Else
                      strSQL1 = strSQL1 & " {" & i & "} " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)
                    End If
                  End If
                Case Else
                  If nPos1 = 0 Then
                    strSQL1 = strSQL1 & " WHERE "
                    strSQL1 = strSQL1 & " " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " '" & ConvertLikeJolly(getPz_Val(i + 1, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15)) & "'"
                  Else
                    If nFil = 1 Then
                      strSQL1 = strSQL1 & " ) AND ( {{ " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " '" & ConvertLikeJolly(getPz_Val(i + 1, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15)) & "'"
                    Else
                      strSQL1 = strSQL1 & " {" & i & "} " & getPz_Column(i + 1, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & " " & getcbPz_Op(i + 1, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " '" & ConvertLikeJolly(getPz_Val(i + 1, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15)) & "'"
                    End If
                  End If
              End Select
          End Select
        Else
          nFilsaltati = nFilsaltati + 1
        End If
        nPos1 = InStr(1, ConvEnterInSpace(strSQL1), " WHERE ", CompareMethod.Text)
      Next

      Dim nCount As Integer = 0
      Dim strSearch As String = "'('"
      If (strSQL.Contains(strSearch)) Then
        nCount = NumOccorrenzeStringa(strSQL1, strSearch, strSearch.Length)
      End If

      If NumOccorrenze(strSQL1, "(") - nCount - 1 = NumOccorrenze(strSQL1, ")") Then
        strSQL1 = strSQL1 & " )"
      End If

      strOprel = AllTrim(strOprel)
      If (InStr(strOprel, "0") > 0) And (InStr(strOprel, "10") = 0) Then strOprel = strOprel.Replace("0", "10")
      If NumOccorrenze(strSQL1, "{") = NumOccorrenze(strSQL1, "}") Then
        ' Non c'era la clausola WHERE
        nPos = InStr(1, strSQL1, " WHERE ", CompareMethod.Text) + 7 '6 = Lungh. di WHERE + 1 blank
        bNumocc = True
      Else
        ' C'era già una clausola WHERE
        nPos = InStr(1, strSQL1, "{{", CompareMethod.Text) + 3
        bNumocc = False
      End If
      nFilrimvuoti = 0
      i = 14
      bFinito = False
      While i >= 0 And Not bFinito
        If getPz_Descam(i + 1, stredPz_Descam1, stredPz_Descam2, stredPz_Descam3, stredPz_Descam4, stredPz_Descam5, _
                        stredPz_Descam6, stredPz_Descam7, stredPz_Descam8, stredPz_Descam9, stredPz_Descam10, _
                        stredPz_Descam11, stredPz_Descam12, stredPz_Descam13, stredPz_Descam14, stredPz_Descam15) = "" Then
          i = i - 1
          nFilrimvuoti = nFilrimvuoti + 1
        Else
          bFinito = True
        End If
      End While

      nCardinale = 1
      LungstrOprel = Len(strOprel)
      nPos1 = 1
      nPos2 = InStr(strOprel, CStr(nCardinale))

      nPos = InStr(strSQL1, "{{")
      If nPos > 0 Then
        strSQL1 = Left(strSQL1, nPos - 1) & Mid(strOprel, nPos1, nPos2 - 1) & " " & Mid(strSQL1, nPos + 2)
      Else
        nPos = InStr(1, strSQL1, " WHERE ", CompareMethod.Text) + 7
        strSQL1 = Left(strSQL1, nPos - 1) & Mid(strOprel, nPos1, nPos2 - 1) & " " & Mid(strSQL1, nPos)
      End If

      nPos1 = nPos2 + 1
      nCardinale = 2
      While nPos1 <= LungstrOprel
        nPos2 = InStr(strOprel, CStr(nCardinale))
        If nPos2 = 0 Then
          nPos2 = LungstrOprel
        End If
        nPos = InStr(strSQL1, "{" & CStr(nCardinale - 1) & "}")
        If nPos > 0 Then
          strSQL1 = Left(strSQL1, nPos - 1) & _
            Mid(strOprel, nPos1, nPos2 - nPos1) & _
            Mid(strSQL1, nPos + 3 + CInt(IIf(nCardinale > 10, 1, 0)))
        Else
          strSQL1 = Left(strSQL1, Len(strSQL1)) & Mid(strOprel, nPos1, nPos2 - nPos1 + 1)
        End If
        nPos1 = nPos2 + 1 + CInt(IIf(nCardinale > 9, 1, 0))
        nCardinale = nCardinale + 1
      End While
      '---------------------------------------------------------------------------------------
      '--- Se è impostata la tabella principale sulla quale filtrare il codice ditta
      '--- lo concatena alla stringa WHERE
      '---------------------------------------------------------------------------------------
      If NTSCStr(strPz_maintab) <> "" Then
        If InStr(1, (strSQL1).ToUpper, "WHERE") = 0 Then
          strSQL1 = strSQL1 & " WHERE (" & strPz_maintab & ".codditt = " & CStrSQL(strDittaCorrente) & ")"
        Else
          strSQL1 = strSQL1 & " AND (" & strPz_maintab & ".codditt = " & CStrSQL(strDittaCorrente) & ")"
        End If
        If (NTSCStr(strPz_Nomrpt).ToUpper) = "PAR0010.RPT" Then
          strSQL1 = strSQL1 & " AND ((anagra.codditt IS NULL) OR (anagra.codditt = '" & strDittaCorrente & "'))"
        End If
      End If
      '---------------------------------------------------------------------------------------
      strRpt = strSQL1 & " " & strSQL2
      '--------------------------------------------------------------------------------------------------------------
      '--- Se esite la Subquery, la sostituisce con i filtri impostati
      '--------------------------------------------------------------------------------------------------------------
ValutaSubQuery:
      '--------------------------------------------------------------------------------------------------------------
      If (InStr(strRpt.ToUpper, "*SUBQ*") > 0) And (strOprel2.Trim <> "") Then
        nFil = 0
        For i = 0 To 4
          nFil += 1
          If getPz_DescamS(i + 1, stredPz_Descams1, stredPz_Descams2, stredPz_Descams3, stredPz_Descams4, stredPz_Descams5) <> "" Then
            Select Case getPz_TipoS(i + 1, lPz_Tipos1, lPz_Tipos2, lPz_Tipos3, lPz_Tipos4, lPz_Tipos5)
              Case 3 To 7 ' Tipo NUMERICO
                If (getPz_TipoS(i + 1, lPz_Tipos1, lPz_Tipos2, lPz_Tipos3, lPz_Tipos4, lPz_Tipos5) = 5) Or _
                   (getPz_TipoS(i + 1, lPz_Tipos1, lPz_Tipos2, lPz_Tipos3, lPz_Tipos4, lPz_Tipos5) = 7) Then
                  strSubquery += IIf(nFil > 1, " {" & i & "} ", "").ToString & " " & getPz_ColumnS(i + 1, strPz_Columns1, strPz_Columns2, strPz_Columns3, strPz_Columns4, strPz_Columns5) & " " & _
                    getcbPz_OpS(i + 1, strcbPz_Ops1, strcbPz_Ops2, strcbPz_Ops3, strcbPz_Ops4, strcbPz_Ops5) & " " & _
                    CDblSQL(getPz_ValS(i + 1, stredPz_Vals1, stredPz_Vals2, stredPz_Vals3, stredPz_Vals4, stredPz_Vals5))
                Else
                  strSubquery += IIf(nFil > 1, " {" & i & "} ", "").ToString & " " & getPz_ColumnS(i + 1, strPz_Columns1, strPz_Columns2, strPz_Columns3, strPz_Columns4, strPz_Columns5) & " " & _
                    getcbPz_OpS(i + 1, strcbPz_Ops1, strcbPz_Ops2, strcbPz_Ops3, strcbPz_Ops4, strcbPz_Ops5) & " " & _
                    getPz_ValS(i + 1, stredPz_Vals1, stredPz_Vals2, stredPz_Vals3, stredPz_Vals4, stredPz_Vals5)
                End If
              Case 8 'Tipo DATA
                Select Case getcbPz_OpS(i + 1, strcbPz_Ops1, strcbPz_Ops2, strcbPz_Ops3, strcbPz_Ops4, strcbPz_Ops5)
                  Case "IS NULL", "IS NOT NULL"
                    strSubquery += IIf(nFil > 1, " {" & i & "} ", "").ToString & getPz_ColumnS(i + 1, strPz_Columns1, strPz_Columns2, strPz_Columns3, strPz_Columns4, strPz_Columns5) & " " & _
                      getcbPz_OpS(i + 1, strcbPz_Ops1, strcbPz_Ops2, strcbPz_Ops3, strcbPz_Ops4, strcbPz_Ops5)
                  Case Else
                    strData = CDataSQL(NTSCDate(getPz_ValS(i + 1, stredPz_Vals1, stredPz_Vals2, stredPz_Vals3, stredPz_Vals4, stredPz_Vals5)))
                    strSubquery += IIf(nFil > 1, " {" & i & "} ", "").ToString & " " & getPz_ColumnS(i + 1, strPz_Columns1, strPz_Columns2, strPz_Columns3, strPz_Columns4, strPz_Columns5) & " " & _
                      getcbPz_OpS(i + 1, strcbPz_Ops1, strcbPz_Ops2, strcbPz_Ops3, strcbPz_Ops4, strcbPz_Ops5) & " " & strData
                End Select
              Case 10, 11 ' Tipo STRING
                Select Case getcbPz_OpS(i + 1, strcbPz_Ops1, strcbPz_Ops2, strcbPz_Ops3, strcbPz_Ops4, strcbPz_Ops5)
                  Case "IS NULL", "IS NOT NULL"
                    strSubquery += IIf(nFil > 1, " {" & i & "} ", "").ToString & getPz_ColumnS(i + 1, strPz_Columns1, strPz_Columns2, strPz_Columns3, strPz_Columns4, strPz_Columns5) & " " & _
                      getcbPz_OpS(i + 1, strcbPz_Ops1, strcbPz_Ops2, strcbPz_Ops3, strcbPz_Ops4, strcbPz_Ops5)
                  Case Else
                    strSubquery += IIf(nFil > 1, " {" & i & "} ", "").ToString & getPz_ColumnS(i + 1, strPz_Columns1, strPz_Columns2, strPz_Columns3, strPz_Columns4, strPz_Columns5) & " " & _
                      getcbPz_OpS(i + 1, strcbPz_Ops1, strcbPz_Ops2, strcbPz_Ops3, strcbPz_Ops4, strcbPz_Ops5) & _
                      " '" & ConvertLikeJolly(getPz_ValS(i + 1, stredPz_Vals1, stredPz_Vals2, stredPz_Vals3, stredPz_Vals4, stredPz_Vals5)) & "'"
                End Select
            End Select
          Else
            nFilsaltati += 1
          End If
        Next
        '------------------------------------------------------------------------------------------------------------
        strOprel2 = AllTrim(strOprel2)
        nFilrimvuoti = 0
        i = 4
        bFinito = False
        While i >= 0 And Not bFinito
          If getPz_DescamS(i + 1, stredPz_Descams1, stredPz_Descams2, stredPz_Descams3, stredPz_Descams4, stredPz_Descams5) = "" Then
            i -= 1
            nFilrimvuoti += 1
          Else
            bFinito = True
          End If
        End While
        nCardinale = 1
        LungstrOprel = strOprel2.Length
        nPos1 = 1
        nPos2 = InStr(strOprel2, CStr(nCardinale))
        nPos1 = nPos2 + 1
        nCardinale = 2
        While nPos1 <= LungstrOprel
          nPos2 = InStr(strOprel2, CStr(nCardinale))
          If nPos2 = 0 Then nPos2 = LungstrOprel
          nPos = InStr(strSubquery, "{" & CStr(nCardinale - 1) & "}")
          If nPos > 0 Then
            strSubquery = Left(strSubquery, nPos - 1) & _
              Mid(strOprel2, nPos1, nPos2 - nPos1) & _
              Mid(strSubquery, nPos + 3 + CInt(IIf(nCardinale > 10, 1, 0)))
          Else
            strSubquery = Left(strSubquery, strSubquery.Length) & Mid(strOprel2, nPos1, nPos2 - nPos1 + 1)
          End If
          nPos1 = nPos2 + 1 + CInt(IIf(nCardinale > 9, 1, 0))
          nCardinale += 1
        End While
        If strSubquery.Trim <> "" Then
          strRpt = Mid(strRpt, 1, InStr(strRpt, "*SUBQ*") - 1) & strSubquery & _
            Mid(strRpt, InStr(strRpt, "*SUBQ*") + "*SUBQ*".Length)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      'tolgo i caratteri di inizio/fine marcatore aggiunti in fase di lettura query ed aggiunta dei filtri (espandifiltri e espandifiltris)
      strRpt = SostituisciC(SostituisciC(strRpt, "{", " "), "}", " ")

      Return strRpt

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

  Public Overridable Function NumOccorrenze(ByVal strPara As String, ByVal cCar As String) As Integer
    Dim i As Integer
    Dim nOcc As Integer
    Try
      nOcc = 0
      For i = 1 To Len(strPara)
        If Mid(strPara, i, 1) = cCar Then nOcc = nOcc + 1
      Next
      Return nOcc

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

  Public Overridable Function NumOccorrenzeStringa(ByVal pstrSource As String, ByVal pstrSearch As String, ByVal pnSearchLen As Integer) As Integer
    Dim i As Integer
    Dim nOcc As Integer
    Try
      nOcc = 0
      For i = 1 To Len(pstrSource) - pnSearchLen
        If Mid(pstrSource, i, pnSearchLen) = pstrSearch Then nOcc = nOcc + 1
      Next
      Return nOcc

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

  Public Overridable Function SostituisciC(ByVal strPara As String, ByVal cCarV As String, ByVal cCarN As String) As String
    'Sostituisce il carattere cCarV con cCarN nella stringa strPara
    Dim nLungstrPara As Integer
    Dim i As Integer
    SostituisciC = ""
    Try
      nLungstrPara = Len(strPara)
      i = 1
      While i <= nLungstrPara
        If Mid(strPara, i, 1) = cCarV Then
          strPara = Left(strPara, i - 1) & cCarN & Mid(strPara, i + 1)
        End If
        i = i + 1
      End While
      Return strPara

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
  Public Overridable Function ConvEnterInSpace(ByVal strPara As String) As String
    'Funzione che sostitusce nella stringa passata come parametro tutti
    'i caratteri ENTER in spazi
    'stessa cosa con i TAB (esempio query importare da editor di testo con TAB prima di order by, group by, ecc)
    Dim nLungh As Integer
    Dim i As Integer
    ConvEnterInSpace = ""
    Try
      nLungh = Len(strPara)
      i = 1
      While i <= nLungh
        If Mid(strPara, i, 1) = vbCr Or Mid(strPara, i, 1) = vbLf Or Mid(strPara, i, 1) = vbTab Then
          If i > 1 Then
            strPara = Left(strPara, i - 1) & " " & Mid(strPara, i + 1)
          Else
            strPara = " " & Mid(strPara, i + 1)
          End If
        End If
        i = i + 1
      End While
      Return strPara

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
  Public Overridable Function AllTrim(ByVal strP As String) As String
    Dim i As Integer, pos As Integer
    Dim strPara As String
    AllTrim = ""
    Try
      strPara = strP
      i = 1
      While i <= Len(strPara)
        If Mid(strPara, i, 1) = " " Then
          If i = 1 Then
            strPara = Right(strPara, Len(strPara) - 1)
          Else
            If i < Len(strPara) Then
              strPara = Left(strPara, i - 1) & Right(strPara, Len(strPara) - i)
            Else
              strPara = Left(strPara, i - 1)
            End If
          End If
        Else
          i = i + 1
        End If
      End While
      pos = InStr(1, strPara, "ORNOT", CompareMethod.Text)
      While pos > 0
        strPara = Left(strPara, pos + 1) & " " & Right(strPara, Len(strPara) - (pos + 1))
        pos = InStr(1, strPara, "ORNOT", CompareMethod.Text)
      End While
      pos = InStr(1, strPara, "ANDNOT", CompareMethod.Text)
      While pos > 0
        strPara = Left(strPara, pos + 2) & " " & Right(strPara, Len(strPara) - (pos + 2))
        pos = InStr(1, strPara, "ANDNOT", CompareMethod.Text)
      End While
      Return strPara

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

  Public Overridable Function getcbPz_Op(ByVal i As Integer, _
  ByVal strcbPz_Op1 As String, ByVal strcbPz_Op2 As String, ByVal strcbPz_Op3 As String, ByVal strcbPz_Op4 As String, ByVal strcbPz_Op5 As String, _
  ByVal strcbPz_Op6 As String, ByVal strcbPz_Op7 As String, ByVal strcbPz_Op8 As String, ByVal strcbPz_Op9 As String, ByVal strcbPz_Op10 As String, _
  ByVal strcbPz_Op11 As String, ByVal strcbPz_Op12 As String, ByVal strcbPz_Op13 As String, ByVal strcbPz_Op14 As String, ByVal strcbPz_Op15 As String) As String
    getcbPz_Op = ""
    Try
      Select Case i
        Case 1
          Return strcbPz_Op1
        Case 2
          Return strcbPz_Op2
        Case 3
          Return strcbPz_Op3
        Case 4
          Return strcbPz_Op4
        Case 5
          Return strcbPz_Op5
        Case 6
          Return strcbPz_Op6
        Case 7
          Return strcbPz_Op7
        Case 8
          Return strcbPz_Op8
        Case 9
          Return strcbPz_Op9
        Case 10
          Return strcbPz_Op10
        Case 11
          Return strcbPz_Op11
        Case 12
          Return strcbPz_Op12
        Case 13
          Return strcbPz_Op13
        Case 14
          Return strcbPz_Op14
        Case 15
          Return strcbPz_Op15
      End Select
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
  Public Overridable Function getcbPz_OpS(ByVal i As Integer, _
    ByVal strcbPz_Ops1 As String, ByVal strcbPz_Ops2 As String, ByVal strcbPz_Ops3 As String, _
    ByVal strcbPz_Ops4 As String, ByVal strcbPz_Ops5 As String) As String
    getcbPz_OpS = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case i
        Case 1 : Return strcbPz_Ops1
        Case 2 : Return strcbPz_Ops2
        Case 3 : Return strcbPz_Ops3
        Case 4 : Return strcbPz_Ops4
        Case 5 : Return strcbPz_Ops5
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function getPz_Column(ByVal i As Integer, _
  ByVal strPz_Column1 As String, ByVal strPz_Column2 As String, ByVal strPz_Column3 As String, ByVal strPz_Column4 As String, ByVal strPz_Column5 As String, _
  ByVal strPz_Column6 As String, ByVal strPz_Column7 As String, ByVal strPz_Column8 As String, ByVal strPz_Column9 As String, ByVal strPz_Column10 As String, _
  ByVal strPz_Column11 As String, ByVal strPz_Column12 As String, ByVal strPz_Column13 As String, ByVal strPz_Column14 As String, ByVal strPz_Column15 As String) As String
    getPz_Column = ""
    Try
      Select Case i
        Case 1
          Return strPz_Column1
        Case 2
          Return strPz_Column2
        Case 3
          Return strPz_Column3
        Case 4
          Return strPz_Column4
        Case 5
          Return strPz_Column5
        Case 6
          Return strPz_Column6
        Case 7
          Return strPz_Column7
        Case 8
          Return strPz_Column8
        Case 9
          Return strPz_Column9
        Case 10
          Return strPz_Column10
        Case 11
          Return strPz_Column11
        Case 12
          Return strPz_Column12
        Case 13
          Return strPz_Column13
        Case 14
          Return strPz_Column14
        Case 15
          Return strPz_Column15
      End Select
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
  Public Overridable Function getPz_ColumnS(ByVal i As Integer, _
    ByVal strPz_Columns1 As String, ByVal strPz_Columns2 As String, ByVal strPz_Columns3 As String, _
    ByVal strPz_Columns4 As String, ByVal strPz_Columns5 As String) As String
    getPz_ColumnS = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case i
        Case 1 : Return strPz_Columns1
        Case 2 : Return strPz_Columns2
        Case 3 : Return strPz_Columns3
        Case 4 : Return strPz_Columns4
        Case 5 : Return strPz_Columns5
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function getPz_Tipo(ByVal i As Integer, _
    ByVal lPz_Tipo1 As Integer, ByVal lPz_Tipo2 As Integer, ByVal lPz_Tipo3 As Integer, ByVal lPz_Tipo4 As Integer, ByVal lPz_Tipo5 As Integer, _
    ByVal lPz_Tipo6 As Integer, ByVal lPz_Tipo7 As Integer, ByVal lPz_Tipo8 As Integer, ByVal lPz_Tipo9 As Integer, ByVal lPz_Tipo10 As Integer, _
    ByVal lPz_Tipo11 As Integer, ByVal lPz_Tipo12 As Integer, ByVal lPz_Tipo13 As Integer, ByVal lPz_Tipo14 As Integer, ByVal lPz_Tipo15 As Integer) As Integer
    getPz_Tipo = 0
    Try
      Select Case i
        Case 1
          Return lPz_Tipo1
        Case 2
          Return lPz_Tipo2
        Case 3
          Return lPz_Tipo3
        Case 4
          Return lPz_Tipo4
        Case 5
          Return lPz_Tipo5
        Case 6
          Return lPz_Tipo6
        Case 7
          Return lPz_Tipo7
        Case 8
          Return lPz_Tipo8
        Case 9
          Return lPz_Tipo9
        Case 10
          Return lPz_Tipo10
        Case 11
          Return lPz_Tipo11
        Case 12
          Return lPz_Tipo12
        Case 13
          Return lPz_Tipo13
        Case 14
          Return lPz_Tipo14
        Case 15
          Return lPz_Tipo15
      End Select
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
  Public Overridable Function getPz_TipoS(ByVal i As Integer, ByVal lPz_Tipos1 As Integer, _
    ByVal lPz_Tipos2 As Integer, ByVal lPz_Tipos3 As Integer, ByVal lPz_Tipos4 As Integer, ByVal lPz_Tipos5 As Integer) As Integer
    getPz_TipoS = 0
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case i
        Case 1 : Return lPz_Tipos1
        Case 2 : Return lPz_Tipos2
        Case 3 : Return lPz_Tipos3
        Case 4 : Return lPz_Tipos4
        Case 5 : Return lPz_Tipos5
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function getPz_Descam(ByVal i As Integer, _
    ByVal stredPz_Descam1 As String, ByVal stredPz_Descam2 As String, ByVal stredPz_Descam3 As String, ByVal stredPz_Descam4 As String, ByVal stredPz_Descam5 As String, _
    ByVal stredPz_Descam6 As String, ByVal stredPz_Descam7 As String, ByVal stredPz_Descam8 As String, ByVal stredPz_Descam9 As String, ByVal stredPz_Descam10 As String, _
    ByVal stredPz_Descam11 As String, ByVal stredPz_Descam12 As String, ByVal stredPz_Descam13 As String, ByVal stredPz_Descam14 As String, ByVal stredPz_Descam15 As String) As String
    getPz_Descam = ""
    Try
      Select Case i
        Case 1
          Return stredPz_Descam1
        Case 2
          Return stredPz_Descam2
        Case 3
          Return stredPz_Descam3
        Case 4
          Return stredPz_Descam4
        Case 5
          Return stredPz_Descam5
        Case 6
          Return stredPz_Descam6
        Case 7
          Return stredPz_Descam7
        Case 8
          Return stredPz_Descam8
        Case 9
          Return stredPz_Descam9
        Case 10
          Return stredPz_Descam10
        Case 11
          Return stredPz_Descam11
        Case 12
          Return stredPz_Descam12
        Case 13
          Return stredPz_Descam13
        Case 14
          Return stredPz_Descam14
        Case 15
          Return stredPz_Descam15
      End Select
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
  Public Overridable Function getPz_DescamS(ByVal i As Integer, _
    ByVal stredPz_Descams1 As String, ByVal stredPz_Descams2 As String, ByVal stredPz_Descams3 As String, _
    ByVal stredPz_Descams4 As String, ByVal stredPz_Descams5 As String) As String
    getPz_DescamS = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case i
        Case 1 : Return stredPz_Descams1
        Case 2 : Return stredPz_Descams2
        Case 3 : Return stredPz_Descams3
        Case 4 : Return stredPz_Descams4
        Case 5 : Return stredPz_Descams5
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function getPz_Val(ByVal i As Integer, _
    ByVal stredPz_Val1 As String, ByVal stredPz_Val2 As String, ByVal stredPz_Val3 As String, ByVal stredPz_Val4 As String, ByVal stredPz_Val5 As String, _
    ByVal stredPz_Val6 As String, ByVal stredPz_Val7 As String, ByVal stredPz_Val8 As String, ByVal stredPz_Val9 As String, ByVal stredPz_Val10 As String, _
    ByVal stredPz_Val11 As String, ByVal stredPz_Val12 As String, ByVal stredPz_Val13 As String, ByVal stredPz_Val14 As String, ByVal stredPz_Val15 As String) As String
    getPz_Val = ""
    Try
      Select Case i
        Case 1
          Return stredPz_Val1
        Case 2
          Return stredPz_Val2
        Case 3
          Return stredPz_Val3
        Case 4
          Return stredPz_Val4
        Case 5
          Return stredPz_Val5
        Case 6
          Return stredPz_Val6
        Case 7
          Return stredPz_Val7
        Case 8
          Return stredPz_Val8
        Case 9
          Return stredPz_Val9
        Case 10
          Return stredPz_Val10
        Case 11
          Return stredPz_Val11
        Case 12
          Return stredPz_Val12
        Case 13
          Return stredPz_Val13
        Case 14
          Return stredPz_Val14
        Case 15
          Return stredPz_Val15
      End Select
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
  Public Overridable Function getPz_ValS(ByVal i As Integer, _
    ByVal stredPz_Vals1 As String, ByVal stredPz_Vals2 As String, ByVal stredPz_Vals3 As String, _
    ByVal stredPz_Vals4 As String, ByVal stredPz_Vals5 As String) As String
    getPz_ValS = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case i
        Case 1 : Return stredPz_Vals1
        Case 2 : Return stredPz_Vals2
        Case 3 : Return stredPz_Vals3
        Case 4 : Return stredPz_Vals4
        Case 5 : Return stredPz_Vals5
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function getPz_Op(ByVal i As Integer, _
    ByVal lOperat1 As Integer, ByVal lOperat2 As Integer, ByVal lOperat3 As Integer, ByVal lOperat4 As Integer, ByVal lOperat5 As Integer, _
    ByVal lOperat6 As Integer, ByVal lOperat7 As Integer, ByVal lOperat8 As Integer, ByVal lOperat9 As Integer, ByVal lOperat10 As Integer, _
    ByVal lOperat11 As Integer, ByVal lOperat12 As Integer, ByVal lOperat13 As Integer, ByVal lOperat14 As Integer, ByVal lOperat15 As Integer) As Integer
    getPz_Op = 0
    Try
      Select Case i
        Case 1
          Return lOperat1
        Case 2
          Return lOperat2
        Case 3
          Return lOperat3
        Case 4
          Return lOperat4
        Case 5
          Return lOperat5
        Case 6
          Return lOperat6
        Case 7
          Return lOperat7
        Case 8
          Return lOperat8
        Case 9
          Return lOperat9
        Case 10
          Return lOperat10
        Case 11
          Return lOperat11
        Case 12
          Return lOperat12
        Case 13
          Return lOperat13
        Case 14
          Return lOperat14
        Case 15
          Return lOperat15
      End Select
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

  Public Overridable Function CreateSelectionFormula(ByVal strOprel As String, _
    ByVal stredPz_Val1 As String, ByVal stredPz_Val2 As String, ByVal stredPz_Val3 As String, _
    ByVal stredPz_Val4 As String, ByVal stredPz_Val5 As String, ByVal stredPz_Val6 As String, _
    ByVal stredPz_Val7 As String, ByVal stredPz_Val8 As String, ByVal stredPz_Val9 As String, _
    ByVal stredPz_Val10 As String, ByVal stredPz_Val11 As String, ByVal stredPz_Val12 As String, _
    ByVal stredPz_Val13 As String, ByVal stredPz_Val14 As String, ByVal stredPz_Val15 As String, _
    ByVal lPz_Tipo1 As Integer, ByVal lPz_Tipo2 As Integer, ByVal lPz_Tipo3 As Integer, _
    ByVal lPz_Tipo4 As Integer, ByVal lPz_Tipo5 As Integer, ByVal lPz_Tipo6 As Integer, _
    ByVal lPz_Tipo7 As Integer, ByVal lPz_Tipo8 As Integer, ByVal lPz_Tipo9 As Integer, _
    ByVal lPz_Tipo10 As Integer, ByVal lPz_Tipo11 As Integer, ByVal lPz_Tipo12 As Integer, _
    ByVal lPz_Tipo13 As Integer, ByVal lPz_Tipo14 As Integer, ByVal lPz_Tipo15 As Integer, _
    ByVal strPz_Column1 As String, ByVal strPz_Column2 As String, ByVal strPz_Column3 As String, _
    ByVal strPz_Column4 As String, ByVal strPz_Column5 As String, ByVal strPz_Column6 As String, _
    ByVal strPz_Column7 As String, ByVal strPz_Column8 As String, ByVal strPz_Column9 As String, _
    ByVal strPz_Column10 As String, ByVal strPz_Column11 As String, ByVal strPz_Column12 As String, _
    ByVal strPz_Column13 As String, ByVal strPz_Column14 As String, ByVal strPz_Column15 As String, _
    ByVal strcbPz_Op1 As String, ByVal strcbPz_Op2 As String, ByVal strcbPz_Op3 As String, _
    ByVal strcbPz_Op4 As String, ByVal strcbPz_Op5 As String, ByVal strcbPz_Op6 As String, _
    ByVal strcbPz_Op7 As String, ByVal strcbPz_Op8 As String, ByVal strcbPz_Op9 As String, _
    ByVal strcbPz_Op10 As String, ByVal strcbPz_Op11 As String, ByVal strcbPz_Op12 As String, _
    ByVal strcbPz_Op13 As String, ByVal strcbPz_Op14 As String, ByVal strcbPz_Op15 As String) As String

    '----------------------------------------------------------------------------------------------------------------
    CreateSelectionFormula = ""
    '----------------------------------------------------------------------------------------------------------------
    Try
      Return CreateSelectionFormula1(strOprel, _
        stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, _
        stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, _
        stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15, _
        lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, _
        lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, _
        lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15, _
        strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, _
        strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, _
        strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15, _
        strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, _
        strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, _
        strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function CreateSelectionFormula1(ByVal strOprel As String, _
    ByVal stredPz_Val1 As String, ByVal stredPz_Val2 As String, ByVal stredPz_Val3 As String, _
    ByVal stredPz_Val4 As String, ByVal stredPz_Val5 As String, ByVal stredPz_Val6 As String, _
    ByVal stredPz_Val7 As String, ByVal stredPz_Val8 As String, ByVal stredPz_Val9 As String, _
    ByVal stredPz_Val10 As String, ByVal stredPz_Val11 As String, ByVal stredPz_Val12 As String, _
    ByVal stredPz_Val13 As String, ByVal stredPz_Val14 As String, ByVal stredPz_Val15 As String, _
    ByVal lPz_Tipo1 As Integer, ByVal lPz_Tipo2 As Integer, ByVal lPz_Tipo3 As Integer, _
    ByVal lPz_Tipo4 As Integer, ByVal lPz_Tipo5 As Integer, ByVal lPz_Tipo6 As Integer, _
    ByVal lPz_Tipo7 As Integer, ByVal lPz_Tipo8 As Integer, ByVal lPz_Tipo9 As Integer, _
    ByVal lPz_Tipo10 As Integer, ByVal lPz_Tipo11 As Integer, ByVal lPz_Tipo12 As Integer, _
    ByVal lPz_Tipo13 As Integer, ByVal lPz_Tipo14 As Integer, ByVal lPz_Tipo15 As Integer, _
    ByVal strPz_Column1 As String, ByVal strPz_Column2 As String, ByVal strPz_Column3 As String, _
    ByVal strPz_Column4 As String, ByVal strPz_Column5 As String, ByVal strPz_Column6 As String, _
    ByVal strPz_Column7 As String, ByVal strPz_Column8 As String, ByVal strPz_Column9 As String, _
    ByVal strPz_Column10 As String, ByVal strPz_Column11 As String, ByVal strPz_Column12 As String, _
    ByVal strPz_Column13 As String, ByVal strPz_Column14 As String, ByVal strPz_Column15 As String, _
    ByVal strcbPz_Op1 As String, ByVal strcbPz_Op2 As String, ByVal strcbPz_Op3 As String, _
    ByVal strcbPz_Op4 As String, ByVal strcbPz_Op5 As String, ByVal strcbPz_Op6 As String, _
    ByVal strcbPz_Op7 As String, ByVal strcbPz_Op8 As String, ByVal strcbPz_Op9 As String, _
    ByVal strcbPz_Op10 As String, ByVal strcbPz_Op11 As String, ByVal strcbPz_Op12 As String, _
    ByVal strcbPz_Op13 As String, ByVal strcbPz_Op14 As String, ByVal strcbPz_Op15 As String) As String
    Dim strData As String
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim strSelFormula As String = ""
    Dim strTmp As String = ""
    Dim strCar As String = ""

    Try
      CreateSelectionFormula1 = ""
      '--------------------------------------------------------------------------------------------------------------
      If (InStr(strOprel, "0") > 0) And (InStr(strOprel, "10") = 0) Then strOprel = strOprel.Replace("0", "10")
      '--------------------------------------------------------------------------------------------------------------
      strSelFormula = strOprel
      '--------------------------------------------------------------------------------------------------------------
      For i = 15 To 1 Step -1
        If InStr(strSelFormula, NTSCStr(i).ToString) > 0 Then
          strCar = ""
          For j = 1 To 20
            strCar += Chr(64 + i) & Chr(96 + i)
          Next
          strSelFormula = strSelFormula.Replace(NTSCStr(i), strCar)
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      For i = 15 To 1 Step -1
        '------------------------------------------------------------------------------------------------------------
        strTmp = ""
        '------------------------------------------------------------------------------------------------------------
        If InStr(strOprel, NTSCStr(i).ToString) > 0 Then
          '----------------------------------------------------------------------------------------------------------
          Select Case getPz_Tipo(i, lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15)
            Case 3 To 7 'Numerico
              Select Case (getcbPz_Op(i, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)).ToUpper
                Case "IS NULL"
                  strTmp = " IsNull({" & getPz_Column(i, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & "})"
                Case "IS NOT NULL"
                  strTmp = " Not (IsNull({" & getPz_Column(i, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & "}))"
                Case Else
                  If (getPz_Tipo(i, lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15) = 5) Or (getPz_Tipo(i, lPz_Tipo1, lPz_Tipo2, lPz_Tipo3, lPz_Tipo4, lPz_Tipo5, lPz_Tipo6, lPz_Tipo7, lPz_Tipo8, lPz_Tipo9, lPz_Tipo10, lPz_Tipo11, lPz_Tipo12, lPz_Tipo13, lPz_Tipo14, lPz_Tipo15) = 7) Then
                    strTmp = " {" & getPz_Column(i, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & "} " & getcbPz_Op(i, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & CDblSQL(getPz_Val(i, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15))
                  Else
                    strTmp = " {" & getPz_Column(i, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & "} " & getcbPz_Op(i, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & getPz_Val(i, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15)
                  End If
              End Select
            Case 8      'Data
              Select Case (getcbPz_Op(i, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)).ToUpper
                Case "IS NULL"
                  strTmp = " IsNull({" & getPz_Column(i, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & "})"
                Case "IS NOT NULL"
                  strTmp = " Not (IsNull({" & getPz_Column(i, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & "}))"
                Case Else
                  strData = ConvDataRpt(getPz_Val(i, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15))
                  strTmp = " {" & getPz_Column(i, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & "} " & getcbPz_Op(i, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " " & strData
              End Select
            Case 10, 11     'Stringa/Memo
              Select Case (getcbPz_Op(i, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15)).ToUpper
                Case "IS NULL"
                  strTmp = " IsNull({" & getPz_Column(i, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & "})"
                Case "IS NOT NULL"
                  strTmp = " Not (IsNull({" & getPz_Column(i, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & "}))"
                Case Else
                  strTmp = " {" & getPz_Column(i, strPz_Column1, strPz_Column2, strPz_Column3, strPz_Column4, strPz_Column5, strPz_Column6, strPz_Column7, strPz_Column8, strPz_Column9, strPz_Column10, strPz_Column11, strPz_Column12, strPz_Column13, strPz_Column14, strPz_Column15) & "} " & getcbPz_Op(i, strcbPz_Op1, strcbPz_Op2, strcbPz_Op3, strcbPz_Op4, strcbPz_Op5, strcbPz_Op6, strcbPz_Op7, strcbPz_Op8, strcbPz_Op9, strcbPz_Op10, strcbPz_Op11, strcbPz_Op12, strcbPz_Op13, strcbPz_Op14, strcbPz_Op15) & " '" & getPz_Val(i, stredPz_Val1, stredPz_Val2, stredPz_Val3, stredPz_Val4, stredPz_Val5, stredPz_Val6, stredPz_Val7, stredPz_Val8, stredPz_Val9, stredPz_Val10, stredPz_Val11, stredPz_Val12, stredPz_Val13, stredPz_Val14, stredPz_Val15) & "'"
              End Select
          End Select
          '----------------------------------------------------------------------------------------------------------
          strCar = ""
          For j = 1 To 20
            strCar += Chr(64 + i) & Chr(96 + i)
          Next
          strSelFormula = strSelFormula.Replace(strCar, strTmp)
          '----------------------------------------------------------------------------------------------------------
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      strSelFormula = strSelFormula.Replace(vbCrLf, "' + chr(13) + chr(10) + '") 'Rimuove gli andare a capo con i caratteri speciali di crystal
      Return strSelFormula
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function GetConto(ByVal strConto As String) As Boolean
    Try
      If oCldSpar.ValCodiceDb(strConto, strDittaCorrente, "ANAGRA", "N") Then
        Return True
      Else
        Return False
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
  End Function

  Public Overridable Function ConvertLikeJolly(ByVal strValore As String) As String
    Try
      Return strValore.Replace("*", "%")

    Catch ex As Exception
      ConvertLikeJolly = ""
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function ControllaCoerenzaWhere(ByVal strWhere As String) As Boolean
    'i numeri nella where devono essere in progressione, 
    'diversamente le funzioni che sostituiscono i marcatori con i filtri effettivi non funzionano
    Dim i As Integer = 0
    Dim nPos As Integer = -1
    Dim nPosOld As Integer = -1
    Try
      If strWhere.Trim = "" Then Return True

      For i = 1 To 14
        nPos = strWhere.IndexOf(IIf(i = 10, 0, i).ToString)
        If nPos > 0 Then
          If nPos < nPosOld Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129144163271845703, "ATTENZIONE: i marcatori numerici nei filtri utente non sono in progressione. Elaborazione interrotta.")))
            Return False
          End If
          nPosOld = nPos
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


#Region "funzioni specifiche per BN__SPAR.BN__HLVL.VB"
  Public Overridable Function HlvlApri(ByVal strDitta As String, ByRef dsHlvl As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim lpos As Integer
    Dim strCampo As String
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      lpos = InStr(strHlvlCampo, ".")
      strCampo = Mid(strHlvlCampo, lpos + 1, Len(strHlvlCampo))

      dReturn = oCldSpar.GetHlvlData(strDitta, strCampo, strHlvlTabella, dsHlvl)

      If dReturn = False Then Return False

      oCldSpar.SetTableDefaultValueFromDB("TAB", dsHlvl)

      dsHlvlShared = dsHlvl

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsHlvlShared.Tables("TAB").ColumnChanging, AddressOf HlvlBeforeColUpdate
      AddHandler dsHlvlShared.Tables("TAB").ColumnChanged, AddressOf HlvlAfterColUpdate

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
  Public Overridable Sub HlvlBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(oApp.Tr(Me, 128327000178518319, strErr))
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "HlvlBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub HlvlAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "HlvlAfterColUpdate_" & e.Column.ColumnName.ToLower
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
#End Region

#Region "funzioni specifiche per BN__SPAR.BN__GRSP.VB"
  Public Overridable Function GrspApri(ByVal strDitta As String, ByRef dsGrsp As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim strSQL As String = ""
    Dim strErr As String = ""
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldSpar.GetGrspData(strGrspSQL, dsGrsp, strErr)

      If dReturn = False Then
        If strErr <> "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128608422623910116, strErr)))
        End If
        Return False
      End If

      oCldSpar.SetTableDefaultValueFromDB("TAB", dsGrsp)

      dsGrspShared = dsGrsp

      '--------------------------------------
      'se richiesto eseguo l'export su excel
      If NTSCStr(dsShared.Tables("PARSTAG").Select("pz_codform = " & nCurrentCodForm)(0)!pz_xlsfile).Trim <> "" Then
        With dsShared.Tables("PARSTAG").Select("pz_codform = " & nCurrentCodForm)(0)
          ExportExcel(dsGrsp.Tables(0), NTSCStr(!pz_xlsfile), NTSCStr(!pz_xlsfoglio))
        End With
      End If

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsGrspShared.Tables("TAB").ColumnChanging, AddressOf GrspBeforeColUpdate
      AddHandler dsGrspShared.Tables("TAB").ColumnChanged, AddressOf GrspAfterColUpdate

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
  Public Overridable Sub GrspBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(oApp.Tr(Me, 128327667071719583, strErr))
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "GrspBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub GrspAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "GrspAfterColUpdate_" & e.Column.ColumnName.ToLower
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
#End Region

  Public Overridable Function ExportExcel(ByRef dttDati As DataTable, ByVal strFile As String, ByVal strFoglio As String) As Boolean
    '---------------------------
    'inserisco nel file di Excel i salcx precedentemente creati
    Dim oCleExcel As New CLE__EXCE
    Dim i As Integer = 0
    Dim n As Integer = 0

    Try
      oCleExcel.Init2(oApp)

      '------------------------
      'creo il file di Excel
      If Not System.IO.File.Exists(strFile) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130465223655405484, "File di Excel '|" & strFile & "|' non trovato. Export su file annullato.")))
        Return True
      End If

      System.IO.File.SetAttributes(strFile, IO.FileAttributes.Normal)

      '--------------------
      'avvio Excel
      If Not oCleExcel.ApriExcel(strFile, strFoglio) Then Return False

      '--------------------
      'preparo il file di excel 
      If Not oCleExcel.PreparaFile() Then Return False

      '--------------------
      'passo le intestazioni di colonna
      For i = 0 To dttDati.Columns.Count - 1
        If Not oCleExcel.InserisciValore(1, i + 1, "S", dttDati.Columns(i).ColumnName) Then Return False
      Next

      '--------------------
      'passo i valori
      For n = 0 To dttDati.Rows.Count - 1
        For i = 0 To dttDati.Columns.Count - 1
          If Not oCleExcel.InserisciValore(n + 2, i + 1, "S", dttDati.Rows(n)(i)) Then Return False
        Next
      Next

      '--------------------
      oCleExcel.ChiudiExcel()

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
End Class
