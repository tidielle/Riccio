Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEVEBOTR
  Inherits CLE__BASE

  Public oCldBotr As CLDVEBOTR 'oggetto dal

  Public strOut As String = ""
  Public dttTmp As DataTable = New DataTable
  Public dttAnaz As New DataTable

  Public bOpzioniPerDitta As Boolean = False
  Public bMultiditta As Boolean = False
  Public bCalcolaBolli As Boolean = False
  Public strTipoCalcoloBolli As String = ""

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModVE + CLN__STD.bsModMG
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE + CLN__STD.bsModExtPAR
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

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDVEBOTR"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldBotr = CType(MyBase.ocldBase, CLDVEBOTR)
    oCldBotr.Init(oApp)
    Return True
  End Function

  Public Overridable Function File_Apri(ByRef strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      oCldBotr.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttAnaz)

      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      dReturn = oCldBotr.GetData(strDitta, ds)
      If dReturn = False Then Return False

      '--------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldBotr.SetTableDefaultValueFromDB(strNomeTabella, ds)
      SetDefaultValue(ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanged, AddressOf AfterColUpdate

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
  Public Overridable Function File_Aggiorna(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If

      bResult = oCldBotr.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella), "", "", "")
      If bResult Then
        bHasChanges = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Salva l'opzione di registro "DeterminaBolliSuOperazEsenti"
      '--------------------------------------------------------------------------------------------------------------
      ocldBase.SaveSettingBus("OPZIONI", ".", ".", "DeterminaBolliSuOperazEsenti", _
        IIf(bCalcolaBolli = True, "-1", "0").ToString, " ", "N", _
        IIf(bOpzioniPerDitta = True, "S", "N").ToString, "N")
      '--------------------------------------------------------------------------------------------------------------
      '--- Salva l'opzione di registro "TipoCalcoloBolli"
      '--------------------------------------------------------------------------------------------------------------
      ocldBase.SaveSettingBus("OPZIONI", ".", ".", "TipoCalcoloBolli", strTipoCalcoloBolli, " ", "N", _
        IIf(bOpzioniPerDitta = True, "S", "N").ToString, "N")
      '--------------------------------------------------------------------------------------------------------------
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
  Public Overridable Function RecordRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables(strNomeTabella).Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Sub RecordNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsShared.Tables(strNomeTabella).Rows.Add(dsShared.Tables(strNomeTabella).NewRow)
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
  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      Try
        ds.Tables(strNomeTabella).Columns("tb_codbotr").DefaultValue = 1
      Catch
        'non gestisco l'errore: viene scatenato solo sulle tabelle che non sono per ditta ...
      End Try
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
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If
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
  Public Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
  Public Overrides Function TestPreSalva() As Boolean
    Try
      '-------------------------------------------------------------------------------------
      '--- Eseguo i controlli necessari prima di fare il salvataggio della riga
      '-------------------------------------------------------------------------------------
      Dim dtrTmp As DataRow() = dsShared.Tables("TABBOTR").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      Dim i As Integer = 0
      For i = 0 To dtrTmp.Length - 1
        If NTSCDec(dtrTmp(i)!tb_arrot) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129373161893037500, "Nel campo 'Arrotondamento' il valore deve essere diverso da 0.")))
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
      Return False
    End Try
  End Function

  Public Overridable Function SetIsMultiditta() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      bMultiditta = oCldBotr.IsDbMultiDitta(strDittaCorrente)
      '--------------------------------------------------------------------------------------------------------------
      Return bMultiditta
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Sub OpzioniPerDitta()
    Try
      '--------------------------------------------------------------------------------------------------------------
      bOpzioniPerDitta = oCldBotr.OpzioniPerDitta
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Function CheckEsistenzaOpzioni() As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      CheckEsistenzaOpzioni("")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function CheckEsistenzaOpzioni(ByVal strNomprop As String) As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strNomprop})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCInt(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return oCldBotr.CheckEsistenzaOpzioni(strNomprop)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function RitornaValProp(ByVal strLiv1 As String, ByVal strLiv2 As String, _
    ByVal strLiv3 As String, ByVal strNomprop As String, ByRef dttOut As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldBotr.RitornaValProp(strLiv1, strLiv2, strLiv3, strNomprop, dttOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

#Region "Validazione campi"

  Public Overridable Sub BeforeColUpdate_tb_codesb_1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_codesb_1 = ""
      Else
        If oCldBotr.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "tabciva", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128078851945118783, "Codice Iva esente (1): inserire un codice valido")))
          Return
        Else
          e.Row!xx_codesb_1 = strOut
        End If
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
  Public Overridable Sub BeforeColUpdate_tb_codesb_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_codesb_2 = ""
      Else
        If oCldBotr.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "tabciva", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128078851945118784, "Codice Iva esente (2): inserire un codice valido")))
          Return
        Else
          e.Row!xx_codesb_2 = strOut
        End If
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
  Public Overridable Sub BeforeColUpdate_tb_codesb_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_codesb_3 = ""
      Else
        If oCldBotr.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "tabciva", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128078851945118785, "Codice Iva esente (3): inserire un codice valido")))
          Return
        Else
          e.Row!xx_codesb_3 = strOut
        End If
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
  Public Overridable Sub BeforeColUpdate_tb_codesb_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_codesb_4 = ""
      Else
        If oCldBotr.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "tabciva", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128078851945118786, "Codice Iva esente (4): inserire un codice valido")))
          Return
        Else
          e.Row!xx_codesb_4 = strOut
        End If
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

  Public Overridable Sub BeforeColUpdate_tb_codesb_5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_codesb_5 = ""
      Else
        If oCldBotr.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "tabciva", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130233703327101131, "Codice Iva esente (5): inserire un codice valido")))
          Return
        Else
          e.Row!xx_codesb_5 = strOut
        End If
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

  Public Overridable Sub BeforeColUpdate_tb_codesb_6(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_codesb_6 = ""
      Else
        If oCldBotr.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "tabciva", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130233703212942670, "Codice Iva esente (6): inserire un codice valido")))
          Return
        Else
          e.Row!xx_codesb_6 = strOut
        End If
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

  Public Overridable Sub BeforeColUpdate_tb_codesb_7(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_codesb_7 = ""
      Else
        If oCldBotr.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "tabciva", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130233703091021556, "Codice Iva esente (7): inserire un codice valido")))
          Return
        Else
          e.Row!xx_codesb_7 = strOut
        End If
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

  Public Overridable Sub BeforeColUpdate_tb_codesb_8(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_codesb_8 = ""
      Else
        If oCldBotr.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "tabciva", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130233702974389398, "Codice Iva esente (8): inserire un codice valido")))
          Return
        Else
          e.Row!xx_codesb_8 = strOut
        End If
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

#End Region
End Class
