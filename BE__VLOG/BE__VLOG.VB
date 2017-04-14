Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports System.IO.File

Public Class CLE__VLOG
  Inherits CLE__BASN

  Public oCldVlog As CLD__VLOG 'oggetto dal
  Public dsShared As DataSet

  Public bHasChanges As Boolean = False

  Public bInApri As Boolean = False
  Public nOrdinamento As Integer
  Public nOrigine As Integer

  Public lStato As Integer

  Public nSvloOrigine As Integer
  Public strSvloOpnome As String
  Public bSvloSelDesogglog As Boolean
  Public strSvloDesogglog As String
  Public bSvloSelCodditt As Boolean
  Public strSvloCodditt As String
  Public strSvloDatini As String
  Public strSvloDatfin As String
  Public strSvloDesogglogLIKE As String
  Public nSvloTipoop As Integer
  Public nSvloTipoogglog As Integer
  Public strSvloSQLQuery As String
  Public bSvloAnnullato As Boolean

  Private Moduli_P As Integer = CLN__STD.bsModAll
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

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__VLOG"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldVlog = CType(MyBase.ocldBase, CLD__VLOG)
    oCldVlog.Init(oApp)
    Return True
  End Function

  Public Overridable Function Apri(ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      dReturn = oCldVlog.GetDataApri(strSvloSQLQuery, nOrdinamento, nSvloOrigine, ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldVlog.SetTableDefaultValueFromDB("ACTLOG", ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("ACTLOG").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("ACTLOG").ColumnChanged, AddressOf AfterColUpdate

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
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(oApp.Tr(Me, 128067822802778673, strErr))
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
  Public Overridable Sub BeforeColUpdate_xx_seleziona(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.Row!al_tipoogglog) = "E" Then
        e.ProposedValue = "N"
        If Not bInApri Then ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131098624009255744, "Attenzione! Non è possibile selezionare più di una riga di tipo 'Elaborazione'.")))
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
  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property

  Public Overridable Sub GetOrdinamento()
    Try
      nOrdinamento = NTSCInt(oCldVlog.GetSettingBus("BS--VLOG", "RECENT", ".", "OrdinamentoRighe", "0", " ", "-1"))
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
  Public Overridable Sub SetOrdinamento()
    Try
      oCldVlog.SaveSettingBus("BS--VLOG", "RECENT", ".", "OrdinamentoRighe", CStr(nOrdinamento), " ", "NS.", "...", "...")
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

  Public Overridable Function FileCancella() As Boolean
    Dim i As Integer
    Dim strCodici As String = ""
    Dim lStep As Integer = 50
    Try
      '-----------------------------------------------------------------------------------------
      '--- Construisce la query con l'elenco dei progressivi visualizzati
      '-----------------------------------------------------------------------------------------
      For i = 0 To dsShared.Tables("ACTLOG").Rows.Count - 1
        If dsShared.Tables("ACTLOG").Rows(i)!al_tipoogglog.ToString = "D" Then
          strCodici = strCodici & NTSCStr(dsShared.Tables("ACTLOG").Rows(i)!al_progr) & ", "
        End If
        If i Mod lStep = 0 And i > 0 Then
          oCldVlog.FileCancella(strSvloCodditt, strCodici, bSvloSelCodditt, nSvloOrigine)
          strCodici = ""
        End If
      Next
      If Len(strCodici) > 0 Then
        oCldVlog.FileCancella(strSvloCodditt, strCodici, bSvloSelCodditt, nSvloOrigine)
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

  Public Overridable Sub StampaSuFile(ByVal dtr As DataRow)
    Dim strPath As String
    Dim strDataTmp As String
    Dim strOraTmp As String
    Dim lPos As Integer
    Dim strDati() As String
    Dim lw1 As StreamWriter = Nothing
    Dim dsActLog As DataSet = Nothing
    Dim i As Integer
    Dim j As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Determina il nome/percorso del file di testo da creare/visualizzare
      '-----------------------------------------------------------------------------------------
      strPath = oApp.AscDir & "\bs--vlog.txt"
      '-----------------------------------------------------------------------------------------
      '--- Cancella il file se già esistente
      '-----------------------------------------------------------------------------------------
      If Exists(strPath) Then
        Kill(strPath)
      End If
      '-----------------------------------------------------------------------------------------
      '--- Apre il file per la scrittura
      '-----------------------------------------------------------------------------------------
      lw1 = New StreamWriter(strPath)
      '-----------------------------------------------------------------------------------------
      '--- Scrive la prima riga per l'intestazione
      '-----------------------------------------------------------------------------------------
      lw1.WriteLine("--------------------------------------------------------------------")
      lw1.WriteLine("---------------- Visualizzazione LOG attività ----------------------")
      lw1.WriteLine("--------------------------------------------------------------------")
      '---------------------------------------------------------------------------------------
      lw1.WriteLine("- Ditta:      " & NTSCStr(dtr!codditt) & " (programma: " & NTSCStr(dtr!al_nomprog) & ")")
      lw1.WriteLine("  L'operatore " & NTSCStr(dtr!al_opnome) & " ha eseguito l'operazione:")
      lw1.WriteLine("              " & NTSCStr(dtr!al_desogglog))
      lPos = InStr(1, NTSCStr(dtr!al_data), " ")
      strDataTmp = Trim(Mid(NTSCStr(dtr!al_data), 1, (lPos - 1)))
      strOraTmp = Trim(Mid(NTSCStr(dtr!al_data), (lPos + 1)))
      lw1.WriteLine("              il " & strDataTmp & ", alle ore " & strOraTmp)
      If NTSCStr(dtr!al_tipoogglog) = "D" Then
        lw1.WriteLine("              " & NTSCStr(IIf(NTSCStr(dtr!al_tipoop) = "M", "modificando", "cancellando")) & " i dati sotto riportati:")
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se la riga è i tipo 'Elaborazione', salta la scrittura delle righe di ACTLOG
      '-----------------------------------------------------------------------------------------
      If NTSCStr(dtr!al_tipoogglog) = "E" Then GoTo SaltaScritturaRigheActlog
      '-----------------------------------------------------------------------------------------
      '--- Apre un recordset su ACTLOG con la chiave della riga di griglia corrente
      '-----------------------------------------------------------------------------------------

      oCldVlog.StampaSuFile(NTSCStr(dtr!codditt), NTSCStr(dtr!al_progr), nSvloOrigine, dsActLog)

      '-----------------------------------------------------------------------------------------
      For i = 0 To dsActLog.Tables("ACTLOG").Rows.Count - 1
        lw1.WriteLine("--------------------------------------------------------------------")
        lw1.WriteLine("--- " & NTSCStr(dsActLog.Tables("ACTLOG").Rows(i)!al_nomtab).ToUpper & ":")
        lw1.WriteLine("--------------------------------------------------------------------")
        '-------------------------------------------------------------------------------------
        '--- Riempie un vettore con i valori memorizzati in tabella
        '-------------------------------------------------------------------------------------
        strDati = Split(NTSCStr(dsActLog.Tables("ACTLOG").Rows(i)!al_dati), "§")
        For j = LBound(strDati) To UBound(strDati)
          If Trim(strDati(j)) <> "" Then lw1.WriteLine(strDati(j))
        Next
      Next
      '-----------------------------------------------------------------------------------------
SaltaScritturaRigheActlog:
      '-----------------------------------------------------------------------------------------
      '--- Scrive una riga come chiusura del file
      '-----------------------------------------------------------------------------------------
      lw1.WriteLine("")
      lw1.WriteLine("--------------------------------------------------------------------")
      lw1.WriteLine("----------------------------- FINE ---------------------------------")
      lw1.WriteLine("--------------------------------------------------------------------")
      '-----------------------------------------------------------------------------------------
      '--- Chiude il file di testo
      '-----------------------------------------------------------------------------------------
      lw1.Flush()
      lw1.Close()
      '-----------------------------------------------------------------------------------------
      '--- Lancia il NotePad per la visualizzazione del file di testo appena creato
      '-----------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.CALL_PROCESSSTART, strPath))
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
  End Sub
  Public Overridable Sub ReportFileSel(ByVal dtrT() As DataRow)
    Dim strPath As String
    Dim strDataTmp As String
    Dim strOraTmp As String
    Dim lPos As Integer
    Dim strDati() As String
    Dim lw1 As StreamWriter = Nothing
    Dim dsActLog As DataSet = Nothing
    Dim i As Integer
    Dim j As Integer
    Dim k As Integer
    Dim strMsg As String
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se la griglia è vuota esce
      '-----------------------------------------------------------------------------------------
      If dsShared.Tables("ACTLOG").Rows.Count = 0 Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Se non sono state selezionate righe da stampare, avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If dtrT.Length = 0 Then
        strMsg = "Attenzione!" & vbCrLf & _
          "Non sono state selezionate righe dalle quali generare il file di testo." & vbCrLf & _
          "Visualizzazione file di testo annullata."
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128342320586939702, strMsg)))
        Exit Sub
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se è stata selezionata una sola riga
      '--- chiama la visualizzazione in file di testo normale
      '-----------------------------------------------------------------------------------------
      If dtrT.Length = 1 Then
        StampaSuFile(dtrT(0))
        Exit Sub
      End If
      '-----------------------------------------------------------------------------------------
      '--- Determina il nome/percorso del file di testo da creare/visualizzare
      '-----------------------------------------------------------------------------------------
      strPath = oApp.AscDir & "\bs--vlogsel.txt"
      '-----------------------------------------------------------------------------------------
      '--- Cancella il file se già esistente
      '-----------------------------------------------------------------------------------------
      If Exists(strPath) Then
        Kill(strPath)
      End If
      '-----------------------------------------------------------------------------------------
      '--- Apre il file per la scrittura
      '-----------------------------------------------------------------------------------------
      lw1 = New StreamWriter(strPath)
      '-----------------------------------------------------------------------------------------
      '--- Scrive la prima riga per l'intestazione
      '-----------------------------------------------------------------------------------------
      lw1.WriteLine("--------------------------------------------------------------------")
      lw1.WriteLine("---------------- Visualizzazione LOG attività ----------------------")
      lw1.WriteLine("--------------------------------------------------------------------")
      '-----------------------------------------------------------------------------------------
      For k = 0 To dtrT.Length - 1
        lw1.WriteLine("")
        lw1.WriteLine("--------------------------------------------------------------------")
        lw1.WriteLine("- Ditta:      " & NTSCStr(dtrT(k)!codditt) & " (programma: " & NTSCStr(dtrT(k)!al_nomprog) & ")")
        lw1.WriteLine("  L'operatore " & NTSCStr(dtrT(k)!al_opnome) & " ha eseguito l'operazione:")
        lw1.WriteLine("              " & NTSCStr(dtrT(k)!al_desogglog))
        lPos = InStr(1, NTSCStr(dtrT(k)!al_data), " ")
        strDataTmp = Trim(Mid(NTSCStr(dtrT(k)!al_data), 1, (lPos - 1)))
        strOraTmp = Trim(Mid(NTSCStr(dtrT(k)!al_data), (lPos + 1)))
        lw1.WriteLine("              il " & strDataTmp & ", alle ore " & strOraTmp)
        If NTSCStr(dtrT(k)!al_tipoogglog) = "D" Then
          lw1.WriteLine("              " & NTSCStr(IIf(NTSCStr(dtrT(k)!al_tipoop) = "M", "modificando", "cancellando")) & " i dati sotto riportati:")
        End If
        '-----------------------------------------------------------------------------------------
        '--- Apre un recordset su ACTLOG con la chiave della riga di griglia corrente
        '-----------------------------------------------------------------------------------------
        oCldVlog.StampaSuFile(NTSCStr(dtrT(k)!codditt), NTSCStr(dtrT(k)!al_progr), nSvloOrigine, dsActLog)
        '-----------------------------------------------------------------------------------------
        For i = 0 To dsActLog.Tables("ACTLOG").Rows.Count - 1
          lw1.WriteLine("--------------------------------------------------------------------")
          lw1.WriteLine("--- " & NTSCStr(dsActLog.Tables("ACTLOG").Rows(i)!al_nomtab).ToUpper & ":")
          lw1.WriteLine("--------------------------------------------------------------------")

          '-------------------------------------------------------------------------------------
          '--- Riempie un vettore con i valori memorizzati in tabella
          '-------------------------------------------------------------------------------------
          strDati = Split(NTSCStr(dsActLog.Tables("ACTLOG").Rows(i)!al_dati), "§")
          For j = LBound(strDati) To UBound(strDati)
            If Trim(strDati(j)) <> "" Then lw1.WriteLine(strDati(j))
          Next
        Next
        '---------------------------------------------------------------------------------------
        '--- Scrive una riga come chiusura del file
        '---------------------------------------------------------------------------------------
        lw1.WriteLine("")
        lw1.WriteLine("--------------------------------------------------------------------")
        lw1.WriteLine("----------------------------- FINE ---------------------------------")
        lw1.WriteLine("(" & NTSCStr(dtrT(k)!al_desogglog) & ")")
        lw1.WriteLine("--------------------------------------------------------------------")
        lw1.WriteLine("")
        '---------------------------------------------------------------------------------------
      Next
      '-----------------------------------------------------------------------------------------
      lw1.WriteLine("")
      lw1.WriteLine("--------------------------------------------------------------------")
      lw1.WriteLine("---------------- FINE VISUALIZZAZIONE LOG ATTIVITA'-----------------")
      lw1.WriteLine("--------------------------------------------------------------------")
      '-----------------------------------------------------------------------------------------
      '--- Chiude il file di testo
      '-----------------------------------------------------------------------------------------
      lw1.Flush()
      lw1.Close()
      '-----------------------------------------------------------------------------------------
      '--- Lancia il NotePad per la visualizzazione del file di testo appena creato
      '-----------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.CALL_PROCESSSTART, strPath))
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
  End Sub

  Public Overridable Function InserisciAttivitaMancanti() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldVlog.InserisciAttivitaMancanti
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

#Region "funzioni specifiche per BN__VLOG.BN__SVLO"

  Public Overridable Sub GetOrigineDati()
    Try
      nOrigine = NTSCInt(oCldVlog.GetSettingBus("BS--SVLO", "RECENT", ".", "OrigineDati", "0", " ", "-1"))
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
  Public Overridable Sub SetOrigineDati()
    Try
      oCldVlog.SaveSettingBus("BS--SVLO", "RECENT", ".", "OrigineDati", CStr(nOrigine), " ", "NS.", "...", "...")
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

  Public Overridable Function GetStructLog(ByRef ds As DataSet) As Boolean
    Try
      Return oCldVlog.GetStructLog(ds)
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

  Public Overridable Function CheckSelectionStructLog(ByVal strDesogglog As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldVlog.CheckSelectionStructLog(strDesogglog, ds)
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
  Public Overridable Function CheckSelectionActLog(ByVal stredOpnome As String, ByVal strNomogglog As String, ByVal stredCodditt As String, _
  ByVal bckSelDesogglog As Boolean, ByVal bckSelCodditt As Boolean, ByVal stredDesogglog As String, ByVal bopTipoop1 As Boolean, ByVal bopTipoop2 As Boolean, _
  ByVal bopTipoogglog0 As Boolean, ByVal bopTipoogglog1 As Boolean, ByVal stredDatini As String, ByVal stredDatfin As String, ByVal bConn As Boolean, _
  ByVal bopOrigine0 As Boolean, ByRef ds As DataSet) As Boolean
    Try
      Return oCldVlog.CheckSelectionActLog(stredOpnome, strNomogglog, stredCodditt, _
       bckSelDesogglog, bckSelCodditt, stredDesogglog, bopTipoop1, bopTipoop2, _
       bopTipoogglog0, bopTipoogglog1, stredDatini, stredDatfin, bConn, _
       bopOrigine0, ds)
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

  Public Overridable Function CheckCoddittSelect(ByVal stredCodditt As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldVlog.CheckCoddittSelect(stredCodditt, ds)
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

  Public Overridable Function CheckOpnomeSelect(ByVal stredOpnome As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldVlog.CheckOpnomeSelect(stredOpnome, ds)
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

  Public Overridable Function CostruisciWhereStructLog(ByVal strDesogglog As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldVlog.CostruisciWhereStructLog(strDesogglog, ds)
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
