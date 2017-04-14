Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports System.IO.File

Public Class CLE__CEDI
  Inherits CLE__BASN


  Public oCldCedi As CLD__CEDI 'oggetto dal
  Public dsShared As DataSet

  Public bHasChanges As Boolean = False


  Public bMultiditta As Boolean
  Public strCoddittTmp As String
  Public bOkActlog As Boolean
  Public bCG_1 As Boolean
  Public bCG_2 As Boolean
  Public bMG As Boolean
  Public bPRB As Boolean
  Public bOR As Boolean
  Public bBUD As Boolean
  Public bPR As Boolean
  Public bPM_1 As Boolean
  Public bPM_2 As Boolean
  Public bCRM As Boolean
  Public bDWH As Boolean
  Public bStatistiche As Boolean
  Public bTT As Boolean
  Public bAS_1 As Boolean
  Public bAS_2 As Boolean
  Public bMovc As Boolean
  Public bAnac As Boolean
  Public bEmail As Boolean
  Public bMantieni As Boolean
  Public bScadEx As Boolean

  Public strImpdCoddittDest As String
  Public strImpdDescrDittDest As String
  Public strImpdAzcodpcon As String
  Public bImpdRefresh As Boolean
  Public strImpdDesogglog As String

  Public bSelezione As Boolean
  Public bAnagraC As Boolean
  Public bAnagraF As Boolean
  Public bAnalink As Boolean
  Public bTabbanc As Boolean
  Public bTabcove As Boolean
  Public bMessaggio As Boolean
  Public strParam As String

  Public strMsg As String
  Public strPDCOrigine As String
  Public strPDCDestinazione As String
  Public strElenco As String
  Public strElenco1 As String
  Public strTemp As String
  Public strTmp As String
  Public strWhere As String
  Public strConnPwdMdb As String


  Public bEldaCG_1 As Boolean
  Public bEldaCG_2 As Boolean
  Public bEldaMG As Boolean
  Public bEldaMantieni As Boolean
  Public bEldaPRB As Boolean
  Public bEldaOR As Boolean
  Public bEldaBUD As Boolean
  Public bEldaPR As Boolean
  Public bEldaPM_1 As Boolean
  Public bEldaPM_2 As Boolean
  Public bEldaCRM As Boolean
  Public bEldaDWH As Boolean
  Public bEldaStatistiche As Boolean
  Public bEldaTT As Boolean
  Public bEldaAS_1 As Boolean
  Public bEldaAS_2 As Boolean
  Public bEldaMovc As Boolean
  Public bEldaAnac As Boolean
  Public bEldaEmail As Boolean
  Public bEldaScadEx As Boolean
  Public bEldaAnnullato As Boolean
  Public strEldaDesogglog As String

  Public strImportServer As String
  Public strImportDatabase As String
  Public strImportUtente As String
  Public strImportPassword As String

  Public dsDestinazione As DataSet

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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__CEDI"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldCedi = CType(MyBase.ocldBase, CLD__CEDI)
    oCldCedi.Init(oApp)
    Return True
  End Function

  Public Overridable Function Apri(ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      dReturn = oCldCedi.GetDataApri(bMultiditta, strCoddittTmp, ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldCedi.SetTableDefaultValueFromDB("TABANAZ", ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("TABANAZ").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TABANAZ").ColumnChanged, AddressOf AfterColUpdate

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
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(oApp.Tr(Me, 128344957336586147, strErr))
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


  Public Overridable Function SetIsMultiditta() As Boolean
    Try
      bMultiditta = oCldCedi.IsDbMultiDitta(strCoddittTmp)
      Return bMultiditta
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
  Public Overridable Sub ScriviActlog(ByVal nTipoOperazione As Integer, ByVal stredCodditt As String, ByVal stredTb_azrags1 As String, ByVal stredTb_azrags2 As String, ByVal stredTb_azindir As String, _
  ByVal stredTb_azcitta As String, ByVal stredTb_azcap As String, ByVal stredTb_azprov As String, ByVal stredTb_azcodf As String, ByVal stredTb_azpiva As String)
    Dim strDesogglog As String
    Try
      '-----------------------------------------------------------------------------------------
      Select Case nTipoOperazione
        Case 0 : If strImpdDesogglog = "" Then Exit Sub
        Case 1 : If strEldaDesogglog = "" Then Exit Sub
      End Select
      '-----------------------------------------------------------------------------------------
      strDesogglog = "Copia/eliminazione dati ditta" & vbCrLf & vbCrLf & _
        " - Codice ditta.............................: '" & stredCodditt & "'" & vbCrLf & _
        " - Ragione sociale 1........................: '" & stredTb_azrags1 & "'" & vbCrLf & _
        " - Ragione sociale 2........................: '" & stredTb_azrags2 & "'" & vbCrLf & _
        " - Indirizzo................................: '" & stredTb_azindir & "'" & vbCrLf & _
        " - Città/località...........................: '" & stredTb_azcitta & "'" & vbCrLf & _
        " - C.A.P....................................: '" & stredTb_azcap & "'" & vbCrLf & _
        " - Provincia................................: '" & stredTb_azprov & "'" & vbCrLf & _
        " - Codice Fiscale...........................: '" & stredTb_azcodf & "'" & vbCrLf & _
        " - Partita Iva..............................: '" & stredTb_azpiva & "'" & vbCrLf & _
        " - Tipo operazione..........................: "
      Select Case nTipoOperazione
        Case 0
          strDesogglog = strDesogglog & "'Importazione dati da altra ditta'" & vbCrLf & _
            " - Dati importazione........................: " & vbCrLf & _
            strImpdDesogglog
        Case 1
          strDesogglog = strDesogglog & "'Eliminazione dati'" & vbCrLf & _
            " - Dati eliminazione........................: " & vbCrLf & _
            strEldaDesogglog
      End Select
      bOkActlog = oCldCedi.ScriviActLog(strDittaCorrente, "BS--CEDI", "tabanaz", "tabanaz", CStrSQL(strDittaCorrente), "M", "E", strDesogglog, False)
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

  Public Overridable Function EliminaDati(ByVal stredCodditt As String) As Boolean
    Dim strMsg As String
    Dim evt As NTSEventArgs = Nothing
    Try
      bCG_1 = bEldaCG_1
      bCG_2 = bEldaCG_2
      bMG = bEldaMG
      bPRB = bEldaPRB
      bOR = bEldaOR
      bBUD = bEldaBUD
      bPR = bEldaPR
      bPM_1 = bEldaPM_1
      bPM_2 = bEldaPM_2
      bCRM = bEldaCRM
      bDWH = bEldaDWH
      bStatistiche = bEldaStatistiche
      bTT = bEldaTT
      bAS_1 = bEldaAS_1
      bAS_2 = bEldaAS_2
      bAnac = bEldaAnac
      bMovc = bEldaMovc
      bMantieni = bEldaMantieni
      bEmail = bEldaEmail
      bScadEx = bEldaScadEx
      '-----------------------------------------------------------------------------------------
      '--- Chiede conferma della cancellazione dei dati
      '-----------------------------------------------------------------------------------------
      strMsg = "ATTENZIONE!!!" & vbCrLf & vbCrLf & _
        "I dati selezionati saranno eliminati definitivamente dall'archivio della ditta '" & stredCodditt & "'." & vbCrLf & vbCrLf & _
        "Procedere con la cancellazione?"
      evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128344979226586147, strMsg))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then
        Return False
      End If
      'Me.Refresh()
      '-----------------------------------------------------------------------------------------
      '--- Se scelta la cancellazione dei documenti di magazzino e/o degli ordini
      '--- e non è stata selezionata la scelta di mantenere l'ultimo costo d'acquisto
      '--- avvisa di ricostruire i progressivi di magazzino
      '-----------------------------------------------------------------------------------------
      If (bMG = True) Or (bPRB = True) Or (bOR = True) Then
        If (bMantieni = False) Then
          strMsg = "Dopo aver eliminato questi dati ricostruire i Progressivi di Magazzino" & _
            " per la ditta '" & stredCodditt & "' con l'apposita utility."
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128344978430648647, strMsg)))
        End If
      End If
      '-----------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("CANCELDIT:" & "Eliminazione dati relativi alla ditta '" & stredCodditt & "' in corso...", ""))
      '-----------------------------------------------------------------------------------------
      '--- Passa all'eliminazione dei dati
      '-----------------------------------------------------------------------------------------
      If Not EliminaCG_1(stredCodditt) Then Exit Function
      If Not EliminaCG_2(stredCodditt) Then Exit Function
      If Not EliminaMG(stredCodditt) Then Exit Function
      If Not EliminaPRB(stredCodditt) Then Exit Function
      If Not EliminaOR(stredCodditt) Then Exit Function
      If Not EliminaBUD(stredCodditt) Then Exit Function
      If Not EliminaPR(stredCodditt) Then Exit Function
      If Not EliminaPM_1(stredCodditt) Then Exit Function
      If Not EliminaPM_2(stredCodditt) Then Exit Function
      If Not EliminaCRM(stredCodditt) Then Exit Function
      If Not EliminaDWH(stredCodditt) Then Exit Function
      If Not EliminaStatistiche(stredCodditt) Then Exit Function
      If Not EliminaAS_1(stredCodditt) Then Exit Function
      If Not EliminaAS_2(stredCodditt) Then Exit Function
      If Not EliminaTT(stredCodditt) Then Exit Function
      If Not EliminaMovc(stredCodditt) Then Exit Function
      If Not EliminaAnac(stredCodditt) Then Exit Function
      If Not EliminaEmail(stredCodditt) Then Exit Function
      If Not EliminaScadEx(stredCodditt) Then Exit Function

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

  Public Overridable Function EliminaCG_1(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bCG_1 = False Then
        Return True
      End If

      Return oCldCedi.EliminaCG_1(stredCodditt)

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
  Public Overridable Function EliminaCG_2(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bCG_2 = False Then
        Return True
      End If

      Return oCldCedi.EliminaCG_2(stredCodditt)

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
  Public Overridable Function EliminaMG(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bMG = False Then
        Return True
      End If

      '-----------------------------------------------------------------------------------------
      '--- Se si è scelta la cancellazione dei movimenti di magazzino e degli ordini/impegni
      '--- provvede all'azzeramento dei numeratori relativi al RETAIL
      '--- (IP per REINCPAG, SG & NS per la gestione unificata documenti)
      '-----------------------------------------------------------------------------------------
      oCldCedi.EliminaDatiUpdateTabNuma(stredCodditt)
      '-----------------------------------------------------------------------------------------

      Return oCldCedi.EliminaMG(stredCodditt, bMantieni)

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
  Public Overridable Function EliminaPRB(ByVal stredCodditt As String) As Boolean
    Dim strFilesPIK As String
    Dim strFilesPath As String
    Dim strFiles() As String
    Dim i As Integer
    Dim bEsistePik As Boolean = False
    Try
      '-----------------------------------------------------------------------------------------
      If bPRB = False Then
        Return True
      End If

      oCldCedi.EliminaPRB(stredCodditt)

      '-----------------------------------------------------------------------------------------
      '--- Controlla se nella cartella è presente almeno 1 file .PIK altrimenti darebbe errore
      '--- Cancella gli eventuali files *.PIK
      '-----------------------------------------------------------------------------------------
      strFilesPath = Mid(oApp.RptDir, 1, (Len(oApp.RptDir) - 3)) & "asc\"
      strFilesPath = oCldCedi.GetSettingBus("OPZIONI", ".", ".", "PikDir", strFilesPath, ".", strFilesPath)
      strFilesPIK = strFilesPath & "*.PIK"
      strFiles = Directory.GetFiles(strFilesPath)
      If strFiles.Length > 0 Then
        Dim evnt As New NTSEventArgs(ThMsg.MSG_YESNO, oApp.Tr(Me, 128346010632253537, "Procedere con la cancellazione dei file .PIK?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = ThMsg.RETVALUE_YES Then
          For i = 0 To strFiles.Length - 1
            If Right(strFiles(i), 4) = ".PIK" Then
              bEsistePik = True
              Exit For
            End If
          Next
        End If
      End If

      If bEsistePik = True Then
        Kill(strFilesPIK)
      End If
      '-----------------------------------------------------------------------------------------

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
  Public Overridable Function EliminaOR(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bOR = False Then
        Return True
      End If

      Return oCldCedi.EliminaOR(stredCodditt)

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
  Public Overridable Function EliminaBUD(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bBUD = False Then
        Return True
      End If

      Return oCldCedi.EliminaBUD(stredCodditt)

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
  Public Overridable Function EliminaPR(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bPR = False Then
        Return True
      End If

      Return oCldCedi.EliminaPR(stredCodditt)

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
  Public Overridable Function EliminaPM_1(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bPM_1 = False Then
        Return True
      End If

      Return oCldCedi.EliminaPM_1(stredCodditt)

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
  Public Overridable Function EliminaPM_2(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bPM_2 = False Then
        Return True
      End If

      Return oCldCedi.EliminaPM_2(stredCodditt)

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
  Public Overridable Function EliminaCRM(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bCRM = False Then
        Return True
      End If

      Return oCldCedi.EliminaCRM(stredCodditt)

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
  Public Overridable Function EliminaDWH(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bDWH = False Then
        Return True
      End If

      Return oCldCedi.EliminaDWH(stredCodditt)

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
  Public Overridable Function EliminaStatistiche(ByVal stredCodditt As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bStatistiche = False Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return oCldCedi.EliminaStatistiche(stredCodditt)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function EliminaAS_1(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bAS_1 = False Then
        Return True
      End If

      Return oCldCedi.EliminaAS_1(stredCodditt)

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
  Public Overridable Function EliminaAS_2(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bAS_2 = False Then
        Return True
      End If

      Return oCldCedi.EliminaAS_2(stredCodditt)

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
  Public Overridable Function EliminaTT(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bTT = False Then
        Return True
      End If

      Return oCldCedi.EliminaTT(stredCodditt)

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
  Public Overridable Function EliminaMovc(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bMovc = False Then
        Return True
      End If

      Return oCldCedi.EliminaMovc(stredCodditt)

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
  Public Overridable Function EliminaAnac(ByVal stredCodditt As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      If bAnac = False Then
        Return True
      End If

      Return oCldCedi.EliminaAnac(stredCodditt)

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
  Public Overridable Function EliminaEmail(ByVal stredCodditt As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bEmail = False Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return oCldCedi.EliminaEmail(stredCodditt)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function EliminaScadEx(ByVal stredCodditt As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bScadEx = False Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return oCldCedi.EliminaScadex(stredCodditt)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ControllaEsistenzaMovcesp(ByVal strCodditt As String) As Boolean
    Try
      Return oCldCedi.ControllaEsistenzaMovcesp(strCodditt)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

#Region "funzioni specifiche per BN__CEDI.BN__ELDA"

#End Region

#Region "funzioni specifiche per BN__CEDI.BN__IMPD"

  Public Overridable Function CheckCoddittOr(ByVal stredCoddittOr As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldCedi.CheckCoddittOr(stredCoddittOr, ds)
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
  Public Overridable Function CheckDatabase(ByVal strName As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldCedi.CheckDatabase(strName, ds)
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
  Public Overridable Function CheckCoddittOrAltroDb(ByVal strDitta As String, ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, ByVal strImportPassword As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldCedi.CheckCoddittOrAltroDb(strDitta, strImportServer, strImportDatabase, strImportUtente, strImportPassword, ds)
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
  Public Overridable Function CheckConnectionAltroDB(ByVal strServer As String, ByVal strDatabase As String, ByVal strUtente As String, ByVal strPassword As String) As Boolean
    Try
      Return oCldCedi.CheckConnectionAltroDB(strServer, strDatabase, strUtente, strPassword)

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

  Public Overridable Function CopiaDatiTabanaz(ByVal stredCoddittOr As String, ByVal strlbCoddittDest As String, ByVal bopAltrodb As Boolean) As Boolean
    Try
      Return oCldCedi.CopiaDatiTabanaz(stredCoddittOr, strlbCoddittDest, bopAltrodb, strImportServer, strImportDatabase, strImportUtente, strImportPassword)
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

  Public Overridable Function Importa(ByVal bopImporta0 As Boolean, ByVal bopImporta1 As Boolean, ByVal strlbCoddittDest As String, ByVal bopAltrodb As Boolean, _
    ByVal stredCoddittOr As String, ByVal bckScollegaAG As Boolean, ByVal bckAbilPS As Boolean, ByVal bckDisabPS As Boolean, ByVal bckSvuotaPN As Boolean, _
    ByVal bopSostituisci As Boolean, ByVal bopSovrascrivi As Boolean, ByVal bopAccoda As Boolean, ByVal bopStessodb As Boolean, ByVal strlbDesdittDest As String, _
    ByVal strlbDesdittOr As String, ByVal dttTabelleSelect As DataTable, ByVal strServer As String, ByVal strDatabase As String, _
    ByVal strUtente As String, ByVal strPassword As String) As Boolean
    Dim strError As String = ""

    Dim nPos As Integer
    Dim strDbdest As String = ""
    Dim i As Integer
    Dim dsTabAnaz As DataSet = Nothing
    Dim dsOrderTbl As DataSet = Nothing
    Dim evt As NTSEventArgs = Nothing
    Dim dsTmp As DataSet = Nothing
    Dim dsImport As DataSet = Nothing
    Dim strWhere As String
    Dim bArticoListSconProv As Boolean
    Dim bAnagraListSconProv As Boolean
    Try
      strImportServer = strServer
      strImportDatabase = strDatabase
      strImportUtente = strUtente
      strImportPassword = strPassword
      '-----------------------------------------------------------------------------------------
      If Not bopStessodb Then
        Dim ds As DataSet = Nothing
        oCldCedi.VerificaDatabaseUnicode(False, strImportServer, strImportDatabase, strImportUtente, strImportPassword, ds)
        If NTSCStr(ds.Tables("RELEASE").Rows(0)!data_type) = "nvarchar" Then 'database unicode
          oCldCedi.VerificaDatabaseUnicode(True, "", "", "", "", ds)
          If NTSCStr(ds.Tables("RELEASE").Rows(0)!data_type) = "varchar" Then 'database NON unicode
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130582676355167221, "Attenzione, importazione non possibile se il database di partenza è un database 'Unicode' e il database di destinazione non è 'Unicode'.")))
            Return False
          End If
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Controlla se è stata selezionata almeno una voce
      '--- se scelta opzione Importa alcuni dati
      '-----------------------------------------------------------------------------------------
      If bopImporta1 Then
        bSelezione = False
        bAnagraC = False
        bAnagraF = False
        bAnalink = False
        bTabbanc = False
        bTabcove = False
        For Each dtrRow As DataRow In dttTabelleSelect.Rows
          Select Case dtrRow("cod").ToString
            Case "anagraC" : bAnagraC = True
            Case "anagraF" : bAnagraF = True
            Case "analink" : bAnalink = True
            Case "tabbanc" : bTabbanc = True
            Case "tabcove" : bTabcove = True
          End Select
          bSelezione = True
        Next
        '---------------------------------------------------------------------------------------
        '--- Se non sono state selezionate voci nella ListBox avverte ed esce
        '---------------------------------------------------------------------------------------
        If bSelezione = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128346010632253536, "Selezionare almeno una voce prima di passare all'importazione dei dati.")))
          Exit Function
        End If
        '---------------------------------------------------------------------------------------
        '--- Se sono state selezionate una o più tabelle fra:
        '--- anagraC, anagraF, analink, tabbanc, tabcove
        '--- controlla che le due ditte (origine e destinazione) abbiano lo stesso PDC
        '---------------------------------------------------------------------------------------
        If bAnagraC Or bAnagraF Or bAnalink Or bTabbanc Or bTabcove Then
          '-------------------------------------------------------------------------------------
          oCldCedi.ImportaSelectTabanaz(strlbCoddittDest, dsTabAnaz)
          If dsTabAnaz.Tables("TABANAZ").Rows.Count > 0 Then
            strPDCDestinazione = NTSCStr(dsTabAnaz.Tables("TABANAZ").Rows(0)!tb_azcodpcon)
          Else
            strPDCDestinazione = ""
          End If
          '-------------------------------------------------------------------------------------
          oCldCedi.ControllaTabAnaz(stredCoddittOr, bopAltrodb, strImportServer, strImportDatabase, strImportUtente, strImportPassword, dsTmp)

          If dsTmp.Tables("TABELLA").Rows.Count > 0 Then
            strPDCOrigine = NTSCStr(dsTmp.Tables("TABELLA").Rows(0)!tb_azcodpcon)
          End If
          '-------------------------------------------------------------------------------------
          If (strPDCDestinazione).ToLower <> (strPDCOrigine).ToLower Then
            strMsg = "Fra i vari dati sono stati selezionati:" & vbCrLf
            If bAnagraC = True Then strMsg = strMsg & " - Clienti" & vbCrLf
            If bAnagraF = True Then strMsg = strMsg & " - Fornitori" & vbCrLf
            If (bAnalink = True) Then strMsg = strMsg & " - Collegamenti CG/CA" & vbCrLf
            If bTabbanc = True Then strMsg = strMsg & " - Banche" & vbCrLf
            If bTabcove = True Then strMsg = strMsg & " - Contropartite vendite ditta" & vbCrLf
            strMsg = strMsg & "Mentre il Piano dei Conti delle aziende (di origine e di destinazione)" & _
              " non è lo stesso." & vbCrLf & _
              "Elaborazione non possibile."
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128346011873366716, strMsg)))
            Exit Function
          End If
          '-------------------------------------------------------------------------------------
        End If
        '---------------------------------------------------------------------------------------
        '--- Chiede conferma dell'elaborazione
        '---------------------------------------------------------------------------------------
        strMsg = "Procedere con l'importazione dei dati dalla ditta '" & stredCoddittOr & "'" & _
          " sulla ditta '" & strlbCoddittDest & "'?"
        evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128346014398766296, strMsg))
        ThrowRemoteEvent(evt)
        If Not evt.RetValue = "YES" Then
          Return False
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Copia di tutti i dati
      '-----------------------------------------------------------------------------------------
      If bopImporta0 = True Then
        '---------------------------------------------------------------------------------------
        '--- Apre un recordset su ORDERTBL selezionando le tabelle che sono per ditta
        '--- escludendo TABANAZ e anazul (che sono collegate ad anagen, eventulamente) e le tabelle temporanee
        '---------------------------------------------------------------------------------------
        oCldCedi.ImportaGetOrderTbl(dsOrderTbl)
        '---------------------------------------------------------------------------------------
        '--- Se non esistono dati avvisa ed esce
        '---------------------------------------------------------------------------------------
        If dsOrderTbl.Tables("ORDERTBL").Rows.Count = 0 Then
          strMsg = "Tabella 'OrderTbl' vuota. Impossibile stabilire l'ordine delle operazioni da eseguire." & vbCrLf & _
            "Operazione Annullata."
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128346017755211396, strMsg)))
          Exit Function
        End If
        '---------------------------------------------------------------------------------------
        '--- Cancellazione di tutti i dati delle tabelle in ordine inverso
        '---------------------------------------------------------------------------------------
        For i = dsOrderTbl.Tables("ORDERTBL").Rows.Count - 1 To 0 Step -1
          '*********************************** ATTENZIONE **************************************
          ' - Nel 2011 il prova.mod rilasciato, nella tabella ORDERTBL è stato impostato che
          'la tabella DWDATISTO deve essere per ditta mentre nella tabella il codice ditta
          'non è presente. Escludiamo a mano la tabella.
          'In dsOrderTbl.Tables("ORDERTBL") sono presenti solo le tabelle con ot_perditta = 'S'
          ' - Nella 2012 le tabelle "movcassa", "movpunti", "rechiusc" avevano un indice con duplicati non ammessi
          'questo causava errore di chiave duplicata in fase di duplicazione dati
          ' - Le tabelle social, anche quelle per ditta non vanno duplicate. La gestione social è già multiditta.
          Select Case NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab)
            Case "dwdatisto", "movcassa", "movpunti", "rechiusc", "rechiuscd", "supostt", "surelaznoditta", "suconf"
              dsOrderTbl.Tables("ORDERTBL").Rows(i).Delete()
              dsOrderTbl.Tables("ORDERTBL").Rows(i).AcceptChanges()
              Continue For
          End Select
          '*************************************************************************************
          '-------------------------------------------------------------------------------------
          '--- Passa alla cancellazione della tabella in esame
          '-------------------------------------------------------------------------------------
          oCldCedi.ImportaDeleteTabella(strlbCoddittDest, NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab))
        Next
        '---------------------------------------------------------------------------------------
        For i = 0 To dsOrderTbl.Tables("ORDERTBL").Rows.Count - 1
          '-------------------------------------------------------------------------------------
          '--- Preleva l'elenco dei campi della tabella in esame
          '--- strElenco1 contiene l'elenco dei campi della ditta di destinazione senza il campo
          '--- 'codditt', passato invece come valore
          '-------------------------------------------------------------------------------------
          strElenco = ElencoCampi(NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab), False, bopAltrodb)
          strElenco = " " & strElenco
          strElenco1 = ElencoCampi1(NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab), bopAltrodb)
          nPos = InStr(strElenco1, "codditt")
          strElenco1 = Mid(strElenco1, 1, nPos - 1) & CStrSQL(strlbCoddittDest) & Mid(strElenco1, nPos + 7)
          If bopAltrodb = True Then
            '-----------------------------------------------------------------------------------
            '--- Adesso li inserisce
            '-----------------------------------------------------------------------------------
            If Not oCldCedi.ImportaDaAltroDb(stredCoddittOr, NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab), strElenco, strElenco1, strDbdest, strImportServer, strImportDatabase, strImportUtente, strImportPassword, strError) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128357186709601517, "Attenzione ! nel trasferimento dati della tabella " & NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab) & " il trasferimento non è riuscito o non è riuscita completamente per ragioni, probabilmente,  di  integrità referenziale . Proseguo ugualmente. Verificare la coerenza dei dati." & vbCrLf & "Errore: |" & strError & "|")))
            End If
          Else
            If NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab).ToUpper = "TABNUMA" Then
              oCldCedi.ImportaSelectPoiInsertIntoTabella(NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab), strElenco, strElenco1, stredCoddittOr)
            Else
              oCldCedi.ImportaInsertIntoTabella(NTSCStr(dsOrderTbl.Tables("ORDERTBL").Rows(i)!ot_nometab), strElenco, strElenco1, stredCoddittOr)
            End If
          End If
        Next
        '---------------------------------------------------------------------------------------
        '--- Solo in questo caso (copia 'Tutti i dati')
        '--- aggiorna tutti i campi di TABANAZ
        '---------------------------------------------------------------------------------------
        CopiaDatiTabanaz(stredCoddittOr, strlbCoddittDest, bopAltrodb)

        oCldCedi.ImportaElaborazioniParticolari(bckScollegaAG, bckAbilPS, bckDisabPS, bckSvuotaPN, strlbCoddittDest)

        '---------------------------------------------------------------------------------------
        bImpdRefresh = True
        '---------------------------------------------------------------------------------------
        '--- Messaggio su abilit. partite e scadenze
        '---------------------------------------------------------------------------------------
        If bckAbilPS Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128346037514207036, "Ricordarsi di predisporre  un sottoconto 'EFFETTI ATTIVI' gestito a partite e scadenze ! ")))
        End If
        '---------------------------------------------------------------------------------------
      End If
      '-----------------------------------------------------------------------------------------
      '--- Copia solo i dati selezionati
      '-----------------------------------------------------------------------------------------
      If bopImporta1 Then
        '---------------------------------------------------------------------------------------
        For i = 0 To dttTabelleSelect.Rows.Count - 1
          '-----------------------------------------------------------------------------------
          Select Case (dttTabelleSelect.Rows(i)("cod").ToString()).ToLower
            Case "anagrac"
              strParam = "C"
              strNomeTabella = "anagra"
            Case "anagraf"
              strParam = "F"
              strNomeTabella = "anagra"
            Case "anagras"
              strParam = "S"
              strNomeTabella = "anagra"
            Case Else
              strParam = ""
              strNomeTabella = dttTabelleSelect.Rows(i)("cod").ToString()
          End Select
          '-----------------------------------------------------------------------------------
          If bopSostituisci = True Then
            If Not DelData(dttTabelleSelect.Rows(i)("val").ToString(), strlbCoddittDest) Then Exit Function
          End If
          '-----------------------------------------------------------------------------------
          '--- Se ho selezionato il piano dei conti di CA, importo anche gruppi, classi e mastri
          '-----------------------------------------------------------------------------------
          If strNomeTabella = "anagca" Then
            strElenco = ElencoCampi("tabclca", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "tabclca", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("tabmaca", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "tabmaca", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
          End If
          '-----------------------------------------------------------------------------------
          '--- Se selezionato ARTICO, importa anche:
          '--- ARTROOT, ARTVAR, ARTICOTC, ARTACCE, ARTFASI, ARTKIT
          '-----------------------------------------------------------------------------------
          If strNomeTabella = "artico" Then
            strElenco = ElencoCampi("artroot", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "artroot", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("artvar", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "artvar", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("articotc", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "articotc", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("artacce", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "artacce", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("artfasi", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "artfasi", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("artkit", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "artkit", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
          End If
          '-----------------------------------------------------------------------------------
          strElenco = ElencoCampi(strNomeTabella, False, bopAltrodb)
          If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, strNomeTabella, strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
            Exit Function
          End If
          '-----------------------------------------------------------------------------------
          '--- Se selezionato ARTICO, importa anche:
          '--- BARCODE, ARTVAL (lo fa dopo ARTICO per una questione di integrità referenziale)
          '-----------------------------------------------------------------------------------
          If strNomeTabella = "artico" Then
            strElenco = ElencoCampi("barcode", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "barcode", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("artval", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "artval", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("valarti", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "valarti", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("tabtipa", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "tabtipa", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("artcona", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "artcona", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
          End If
          '-----------------------------------------------------------------------------------
          '--- Se selezionato ANAGRA C o F, importa anche:
          '--- LEADS (solo C), DESTDIV (di tipo C/D o F/E)
          '-----------------------------------------------------------------------------------
          If (strNomeTabella = "anagra") And (strParam <> "S") Then
            If strParam = "C" Then
              strElenco = ElencoCampi("leads", False, bopAltrodb)
              If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "leads", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
                Exit Function
              End If
            End If
            strElenco = ElencoCampi("anaext", False, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "anaext", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("destdiv", True, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "destdiv|1", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            strElenco = ElencoCampi("organig", True, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "organig|1", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            If strParam = "C" Then
              strElenco = ElencoCampi("organig", True, bopAltrodb)
              If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "organig|2", "L", stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
                Exit Function
              End If
            End If
          End If
          '-----------------------------------------------------------------------------------
          '--- importa i listini, sconti, provvigioni con articolo valorizzato, con conto valorizzato, con entrambi
          '-----------------------------------------------------------------------------------
          If strNomeTabella = "artico" Then
            strWhere = " lc_codart <> ' ' AND lc_conto = 0"
            If Not CopiaDatiLis(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                strImportServer, strImportDatabase, strImportUtente, strImportPassword, _
                                bopSostituisci) Then Exit Function
            strWhere = " so_codart <> ' ' AND so_conto = 0"
            If Not CopiaDatiScon(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                 strImportServer, strImportDatabase, strImportUtente, strImportPassword, _
                                 bopSostituisci) Then Exit Function
            strWhere = " per_codart <> ' ' AND per_conto = 0"
            If Not CopiaDatiProv(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                 strImportServer, strImportDatabase, strImportUtente, strImportPassword, _
                                 bopSostituisci) Then Exit Function
            bArticoListSconProv = True
          End If
          If (strNomeTabella = "anagra") And ((strParam = "C") Or (strParam = "F")) Then
            strWhere = " lc_codart = ' ' AND lc_conto <> 0" & _
            " AND lc_conto IN (SELECT an_conto FROM anagra WHERE an_tipo = " & CStrSQL(strParam) & ")"
            If Not CopiaDatiLis(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                strImportServer, strImportDatabase, strImportUtente, strImportPassword, _
                                 bopSostituisci) Then Exit Function
            strWhere = " so_codart = ' ' AND so_conto <> 0" & _
            " AND so_conto IN (SELECT an_conto FROM anagra WHERE an_tipo = " & CStrSQL(strParam) & ")"
            If Not CopiaDatiScon(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                 strImportServer, strImportDatabase, strImportUtente, strImportPassword, _
                                 bopSostituisci) Then Exit Function
            strWhere = " per_codart = ' ' AND per_conto <> 0" & _
            " AND per_conto IN (SELECT an_conto FROM anagra WHERE an_tipo = " & CStrSQL(strParam) & ")"
            If Not CopiaDatiProv(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                 strImportServer, strImportDatabase, strImportUtente, strImportPassword, _
                                 bopSostituisci) Then Exit Function
            bAnagraListSconProv = True
          End If
          If bArticoListSconProv = True And bAnagraListSconProv = True Then
            strWhere = " lc_codart <> ' ' AND lc_conto <> 0" & _
              " AND lc_conto IN (SELECT an_conto FROM anagra WHERE an_tipo = 'C' OR an_tipo = 'F')"
            If Not CopiaDatiLis(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                strImportServer, strImportDatabase, strImportUtente, strImportPassword, _
                                 bopSostituisci) Then Exit Function
            strWhere = " so_codart <> ' ' AND so_conto <> 0" & _
              " AND so_conto IN (SELECT an_conto FROM anagra WHERE an_tipo = 'C' OR an_tipo = 'F')"
            If Not CopiaDatiScon(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                 strImportServer, strImportDatabase, strImportUtente, strImportPassword, _
                                 bopSostituisci) Then Exit Function
            strWhere = " per_codart <> ' ' AND per_conto <> 0" & _
              " AND per_conto IN (SELECT an_conto FROM anagra WHERE an_tipo = 'C' OR an_tipo = 'F')"
            If Not CopiaDatiProv(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                 strImportServer, strImportDatabase, strImportUtente, strImportPassword, _
                                 bopSostituisci) Then Exit Function
            'anche codarfo se listini sia per articolo che per cliente
            strElenco = ElencoCampi("codarfo", True, bopAltrodb)
            If Not CopiaDatiDitta(bopSovrascrivi Or bopSostituisci, strElenco, "codarfo", strParam, stredCoddittOr, bopAltrodb, strlbCoddittDest) Then
              Exit Function
            End If
            bArticoListSconProv = False
            bAnagraListSconProv = False
          End If
        Next
        '---------------------------------------------------------------------------------------
      End If
FineImportazioneDati:
      '-----------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 128346044262009876, "Operazione terminata.")))

      '-----------------------------------------------------------------------------------------
      strImpdDesogglog = _
        "          . Ditta di destinazione..............................................: '" & strlbCoddittDest & "' " & strlbDesdittDest & vbCrLf & _
        "          . Importa............................................................: "
      If bopImporta0 = True Then
        strImpdDesogglog = strImpdDesogglog & "'Tutti i dati'" & vbCrLf
      End If
      If bopImporta1 = True Then
        strImpdDesogglog = strImpdDesogglog & "'Alcuni i dati' "
        If bopAccoda = True Then strImpdDesogglog = strImpdDesogglog & "('Accoda nuovi record')" & vbCrLf
        If bopSostituisci = True Then strImpdDesogglog = strImpdDesogglog & "('Sostituisci completamente')" & vbCrLf
        If bopSovrascrivi = True Then strImpdDesogglog = strImpdDesogglog & "('Sovrascrivi a parità di codice')" & vbCrLf
        For i = 0 To dttTabelleSelect.Rows.Count - 1
          strImpdDesogglog = strImpdDesogglog & Space(15) & " . '" & dttTabelleSelect.Rows(i)("cod").ToString & "'" & vbCrLf
        Next
      End If
      strImpdDesogglog = strImpdDesogglog & _
        "          . Ditta di origine...................................................: "
      If bopStessodb = True Then
        strImpdDesogglog = strImpdDesogglog & "'Database corrente'" & vbCrLf
      Else
        strImpdDesogglog = strImpdDesogglog & "'Altro database'( Server: " & strServer & " User: " & strUtente & " Password: " & strPassword & " Database: " & strDatabase & " )" & vbCrLf & _
          "                . Scollega da anagrafica generale..........................: " & NTSCStr(IIf(bckScollegaAG = True, "'Sì'", "'No'")) & vbCrLf & _
          "                . Abilita partite e scadenze su clienti/fornitori..........: " & NTSCStr(IIf(bckAbilPS = True, "'Sì'", "'No'")) & vbCrLf & _
          "                . Disabilita partite e scadenze su clienti/fornitori.......: " & NTSCStr(IIf(bckDisabPS = True, "'Sì'", "'No'")) & vbCrLf & _
          "                . Svuota archivi di prima nota e altri archivi contabili...: " & NTSCStr(IIf(bckSvuotaPN = True, "'Sì'", "'No'")) & vbCrLf & _
          "                . Codice ditta di origine..................................: '" & stredCoddittOr & "' " & strlbDesdittOr & vbCrLf
      End If
      '-----------------------------------------------------------------------------------------

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
  Public Overridable Function CopiaDatiDitta(ByVal bOverwrite As Boolean, ByVal strElenco As String, ByVal strTabella As String, ByVal strTipo As String, ByVal stredCoddittOr As String, ByVal bopAltrodb As Boolean, ByVal strlbCoddittDest As String) As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      CopiaDatiDitta = False
      '-----------------------------------------------------------------------------------------
      If Not oCldCedi.CopiaDati(bOverwrite, strElenco, strTabella, strTipo, stredCoddittOr, bopAltrodb, strlbCoddittDest, strImportServer, strImportDatabase, strImportUtente, strImportPassword) Then Exit Function
      '-----------------------------------------------------------------------------------------
      CopiaDatiDitta = True
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
  Public Overridable Function ElencoCampi(ByVal strModel As String, ByVal bAddTableCodditt As Boolean, ByVal bopAltrodb As Boolean) As String
    Dim dsTabella As DataSet = Nothing
    Dim dttTabella As DataTable = Nothing
    Dim y As Integer
    Dim dtrT() As DataRow = Nothing
    Dim strCampiScartati As String = ""
    ElencoCampi = ""
    Try
      oCldCedi.ElencoCampiGetStruttura(strModel, dsTabella)
      dsTabella.Tables(strModel).TableName = "TABELLA"

      If bopAltrodb = True Then
        oCldCedi.ElencoCampiStructDb(strModel, strImportServer, strImportDatabase, strImportUtente, strImportPassword, 0, dttTabella)
      Else
        oCldCedi.ElencoCampiStructDb(strModel, oApp.Db.ServerDB, oApp.Db.Nome, oApp.Db.NomeUt, oApp.Db.PwdUt, 0, dttTabella)
      End If

      With dsTabella.Tables("TABELLA")
        strTemp = ""
        For y = 0 To .Columns.Count - 1
          dtrT = dttTabella.Select("ColumnName = '" & .Columns(y).ColumnName & "'")
          If dtrT.Length = 0 Then
            strCampiScartati &= .Columns(y).ColumnName & vbCrLf
          Else
            'Ignora il travaso delle colonne TS
            If (CBool(dtrT(0)("IsIdentity"))) OrElse ((Trim(.Columns(y).ColumnName)).ToUpper = "TS") Then Continue For

            If (.Columns(y).ColumnName).ToLower = "codditt" Then
              If bAddTableCodditt Then
                strTemp = strTemp & strModel & "." & .Columns(y).ColumnName & ", "
              Else
                strTemp = strTemp & .Columns(y).ColumnName & ", "
              End If
            Else
              strTemp = strTemp & .Columns(y).ColumnName & ", "
            End If
          End If
        Next
      End With
      ElencoCampi = Mid(strTemp, 1, Len(strTemp) - 2)

      If strCampiScartati.Length > 0 Then
        ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 128344887970970321, "Per la tabella |'" & strModel.ToUpper & "'| le seguenti colonne sono state scartate:|" & vbCrLf & strCampiScartati & "|")))
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
  Public Overridable Function ElencoCampi1(ByVal strModel As String, ByVal bopAltrodb As Boolean) As String
    Dim dsTabella As DataSet = Nothing
    Dim dttTabella As DataTable = Nothing
    Dim y As Integer
    Dim dtrT() As DataRow = Nothing
    ElencoCampi1 = ""
    Try
      oCldCedi.ElencoCampiGetStruttura(strModel, dsTabella)
      oCldCedi.SetTableDefaultValueFromDB(strModel, dsTabella)
      dsTabella.Tables(strModel).TableName = "TABELLA"

      If bopAltrodb = True Then
        oCldCedi.ElencoCampiStructDb(strModel, strImportServer, strImportDatabase, strImportUtente, strImportPassword, 0, dttTabella)
      Else
        oCldCedi.ElencoCampiStructDb(strModel, oApp.Db.ServerDB, oApp.Db.Nome, oApp.Db.NomeUt, oApp.Db.PwdUt, 0, dttTabella)
      End If

      With dsTabella.Tables("TABELLA")
        strTemp = ""
        For y = 0 To .Columns.Count - 1
          'Dim s1 As String = .Columns(y).ColumnName
          dtrT = dttTabella.Select("ColumnName = '" & .Columns(y).ColumnName & "'")
          If dtrT.Length = 0 Then Continue For

          If CBool(dtrT(0)("IsIdentity")) OrElse Trim(.Columns(y).ColumnName).ToUpper = "TS" Then Continue For

          If (dtrT(0)("DataTypeName").ToString = "varchar") And (CBool(dtrT(0)("AllowDBNull")) = True) Then
            strTemp = strTemp & "CASE WHEN " & .Columns(y).ColumnName & " = '' and len(" & .Columns(y).ColumnName & " )=0 THEN '' ELSE " & .Columns(y).ColumnName & " END AS " & .Columns(y).ColumnName & ", "
          Else
            If (dtrT(0)("DataTypeName").ToString = "varchar") And (CBool(dtrT(0)("AllowDBNull")) = False) And ((.Columns(y).DefaultValue Is """ """) Or (.Columns(y).DefaultValue.ToString = " ")) Then
              strTemp = strTemp & "CASE WHEN " & .Columns(y).ColumnName & " = '' THEN ' ' ELSE " & .Columns(y).ColumnName & " END AS " & .Columns(y).ColumnName & ", "
            Else
              strTemp = strTemp & .Columns(y).ColumnName & ", "
            End If
          End If
        Next
      End With
      ElencoCampi1 = Mid(strTemp, 1, Len(strTemp) - 2)
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
  Public Overridable Function DelData(ByVal strDescrTab As String, ByVal strlbCoddittDest As String) As Boolean
    Dim strMsg As String
    Try
      '-----------------------------------------------------------------------------------------
      DelData = False
      '-----------------------------------------------------------------------------------------
      If Not oCldCedi.DelDataGetTabella(strNomeTabella, strlbCoddittDest, strParam) Then
        strMsg = "Impossibile eliminare uno o più dati dalla tabella '" & strNomeTabella & "' "
        If strParam = "" Then
          strMsg = strMsg & " (" & strNomeTabella & ")"
        Else
          Select Case (strNomeTabella).ToLower
            Case "anagra" : strMsg = strMsg & " (" & strNomeTabella & " di tipo '" & NTSCStr(IIf(strParam = "C", "Clienti", "Fornitori")) & "')"
          End Select
        End If
        strMsg = strMsg & " relativa alla ditta '" & strlbCoddittDest & "'" & vbCrLf
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128352647889205006, strMsg)))
      End If
      '-----------------------------------------------------------------------------------------
      DelData = True
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

  Public Overridable Function CopiaDatiLis(ByVal strWhere As String, ByVal strlbCoddittDest As String, _
                                         ByVal bopAltrodb As Boolean, ByVal stredCoddittOr As String, _
                                         ByVal strImportServer As String, ByVal strImportDatabase As String, _
                                         ByVal strImportUtente As String, ByVal strImportPassword As String, _
                                         ByVal bopSostituisci As Boolean) As Boolean
    Try
      If bopSostituisci = False Then Return True
      Return oCldCedi.CopiaDatiLis(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                   strImportServer, strImportDatabase, strImportUtente, strImportPassword)

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
  Public Overridable Function CopiaDatiProv(ByVal strWhere As String, ByVal strlbCoddittDest As String, _
                                           ByVal bopAltrodb As Boolean, ByVal stredCoddittOr As String, _
                                           ByVal strImportServer As String, ByVal strImportDatabase As String, _
                                           ByVal strImportUtente As String, ByVal strImportPassword As String, _
                                           ByVal bopSostituisci As Boolean) As Boolean
    Try
      If bopSostituisci = False Then Return True
      Return oCldCedi.CopiaDatiProv(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                    strImportServer, strImportDatabase, strImportUtente, strImportPassword)

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
  Public Overridable Function CopiaDatiScon(ByVal strWhere As String, ByVal strlbCoddittDest As String, _
                                           ByVal bopAltrodb As Boolean, ByVal stredCoddittOr As String, _
                                           ByVal strImportServer As String, ByVal strImportDatabase As String, _
                                           ByVal strImportUtente As String, ByVal strImportPassword As String, _
                                           ByVal bopSostituisci As Boolean) As Boolean
    Try
      If bopSostituisci = False Then Return True
      Return oCldCedi.CopiaDatiScon(strWhere, strlbCoddittDest, bopAltrodb, stredCoddittOr, _
                                    strImportServer, strImportDatabase, strImportUtente, strImportPassword)

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
