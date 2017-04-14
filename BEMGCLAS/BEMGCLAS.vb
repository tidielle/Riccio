Imports System.Data
Imports System.Text
Imports NTSInformatica.CLN__STD

Public Class CLEMGCLAS
  Inherits CLE__BASN

#Region "Moduli"
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
#End Region

  Public oCldClas As CLDMGCLAS
  Public bHasChanges As Boolean
  Public bHasChangesArtico As Boolean
  Public dsShared As DataSet
  Public dsSharedArtico As DataSet

  Public nLivello As Integer = 0    'livello collegato alla griglia (da 1 a 5) su cui si sta lavorando
  Public strCodcla1 As String = ""
  Public strCodcla2 As String = ""
  Public strCodcla3 As String = ""
  Public strCodcla4 As String = ""
  Public strCodcla5 As String = ""

  Public strCodcla1Ass As String = ""
  Public strCodcla2Ass As String = ""
  Public strCodcla3Ass As String = ""
  Public strCodcla4Ass As String = ""
  Public strCodcla5Ass As String = ""

  Public bGrigliaArticoli As Boolean = False

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    SetDbVersionNeeded(21, 8)
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGCLAS"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldClas = CType(MyBase.ocldBase, CLDMGCLAS)
    oCldClas.Init(oApp)
    Return True
  End Function

  Public Overridable Function ApriLivello(ByVal nLivello As Integer, ByRef dsOut As DataSet) As Boolean
    Try
      If Not dsShared Is Nothing AndAlso dsShared.Tables.Contains("ARTCLAS") Then
        RemoveHandler dsShared.Tables("ARTCLAS").ColumnChanging, AddressOf BeforeColUpdate
        RemoveHandler dsShared.Tables("ARTCLAS").ColumnChanged, AddressOf AfterColUpdate
      End If

      If Not oCldClas.GetDataLivello(strDittaCorrente, nLivello, dsOut) Then Return False
      dsOut.Tables("ARTCLAS" & nLivello.ToString).TableName = "ARTCLAS"
      dsOut.Tables("ARTCLAS").Columns("acl_codcla" & nLivello.ToString).ColumnName = "acl_codcla"
      dsOut.Tables("ARTCLAS").Columns("acl_descla" & nLivello.ToString).ColumnName = "acl_descla"
      dsOut.Tables("ARTCLAS").Columns("acl_note" & nLivello.ToString).ColumnName = "acl_note"
      dsOut.Tables("ARTCLAS").Columns("acl_gif" & nLivello.ToString).ColumnName = "acl_gif"
      dsOut.Tables("ARTCLAS").Columns("acl_ordin" & nLivello.ToString).ColumnName = "acl_ordin"

      dsOut.AcceptChanges()
      dsShared = dsOut

      AddHandler dsShared.Tables("ARTCLAS").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("ARTCLAS").ColumnChanged, AddressOf AfterColUpdate

      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ApriGriglia(ByVal strCodclas As String, ByRef dsOut As DataSet) As Boolean
    Try
      If Not dsShared Is Nothing AndAlso dsShared.Tables.Contains("ARTCLAS") Then
        RemoveHandler dsShared.Tables("ARTCLAS").ColumnChanging, AddressOf BeforeColUpdate
        RemoveHandler dsShared.Tables("ARTCLAS").ColumnChanged, AddressOf AfterColUpdate
      End If

      If Not oCldClas.GetData(strDittaCorrente, nLivello, strCodcla1, strCodcla2, strCodcla3, strCodcla4, dsOut) Then Return False
      dsOut.Tables("ARTCLAS" & nLivello.ToString).TableName = "ARTCLAS"
      dsOut.Tables("ARTCLAS").Columns("acl_codcla" & nLivello.ToString).ColumnName = "acl_codcla"
      dsOut.Tables("ARTCLAS").Columns("acl_descla" & nLivello.ToString).ColumnName = "acl_descla"
      dsOut.Tables("ARTCLAS").Columns("acl_note" & nLivello.ToString).ColumnName = "acl_note"
      dsOut.Tables("ARTCLAS").Columns("acl_gif" & nLivello.ToString).ColumnName = "acl_gif"
      dsOut.Tables("ARTCLAS").Columns("acl_ordin" & nLivello.ToString).ColumnName = "acl_ordin"

      SetDefaultValue(dsOut, strCodclas)

      dsOut.AcceptChanges()
      dsShared = dsOut

      AddHandler dsShared.Tables("ARTCLAS").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("ARTCLAS").ColumnChanged, AddressOf AfterColUpdate

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
  Public Overridable Sub SetDefaultValue(ByRef dsClas As DataSet, ByVal strCodclas As String)
    Try
      With dsClas.Tables("ARTCLAS")
        .Columns("codditt").DefaultValue = strDittaCorrente
        .Columns("acl_codcla").DefaultValue = " "
        .Columns("acl_descla").DefaultValue = ""
        .Columns("acl_ordin").DefaultValue = "0"
        If .Columns.Contains("acl_codcla1") Then .Columns("acl_codcla1").DefaultValue = " "
        If .Columns.Contains("acl_codcla2") Then .Columns("acl_codcla2").DefaultValue = " "
        If .Columns.Contains("acl_codcla3") Then .Columns("acl_codcla3").DefaultValue = " "
        If .Columns.Contains("acl_codcla4") Then .Columns("acl_codcla4").DefaultValue = " "
        If .Columns.Contains("acl_codcla5") Then .Columns("acl_codcla5").DefaultValue = " "
        If nLivello > 1 Then .Columns("acl_codcla1").DefaultValue = strCodcla1
        If nLivello > 2 Then .Columns("acl_codcla2").DefaultValue = strCodcla2
        If nLivello > 3 Then .Columns("acl_codcla3").DefaultValue = strCodcla3
        If nLivello > 4 Then .Columns("acl_codcla4").DefaultValue = strCodcla4
      End With
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
    Dim dtrTmp() As DataRow = Nothing
    Dim dsTmp As New DataSet
    Dim i As Integer = 0
    Try
      ' prendo le righe modificate o aggiunte
      dtrTmp = dsShared.Tables("ARTCLAS").Select(Nothing, Nothing, DataViewRowState.Added)
      For i = 0 To dtrTmp.Length - 1
        If NTSCStr(dtrTmp(i)!acl_codcla).Trim.Length = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129534010138679922, "Inserire un valore nel campo 'Codice'.")))
          Return False
        End If
        If NTSCStr(dtrTmp(i)!acl_codcla).Contains(" ") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130223250655314544, "Nel campo 'Codice' non possono essere presenti degli spazi.")))
          Return False
        End If
        If NTSCStr(dtrTmp(i)!acl_codcla).Contains("|") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130223281312416748, "Nel campo 'Codice' non può essere presenti il carattere 'pipe'.")))
          Return False
        End If
        If NTSCStr(dtrTmp(i)!acl_descla).Trim.Length = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130223249849259684, "Inserire un valore nel campo 'Descrizione'.")))
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
  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If bGrigliaArticoli = False Then
        '------------------------------------------------------------------------------------------------------------
        If Not bDelete Then
          If Not TestPreSalva() Then Return False
        End If
        '------------------------------------------------------------------------------------------------------------
        dttTmp = dsShared.Tables("ARTCLAS").Copy
        dttTmp.TableName = "ARTCLAS" & nLivello.ToString
        For Each dtrC As DataColumn In dttTmp.Columns
          Select Case dtrC.ColumnName
            Case "acl_codcla", "acl_descla", "acl_note", "acl_gif", "acl_ordin"
              If Not dttTmp.Columns.Contains(dtrC.ColumnName & nLivello.ToString) Then dtrC.ColumnName &= nLivello.ToString
          End Select
        Next
        If Not oCldClas.ScriviTabellaSemplice(strDittaCorrente, dttTmp.TableName, dttTmp, "", "", "") Then Return False
        '------------------------------------------------------------------------------------------------------------
        '--- Se ho cancellato, devo cancellare anche i figli sottostanti
        '------------------------------------------------------------------------------------------------------------
        If nLivello <> 5 And bDelete Then
          For Each dtrT As DataRow In dsShared.Tables("ARTCLAS").Select("", "", DataViewRowState.Deleted)
            oCldClas.CancellaLivelliSotto(nLivello, dtrT)
          Next
        End If
        '------------------------------------------------------------------------------------------------------------
      Else
        '------------------------------------------------------------------------------------------------------------
        If bDelete = False Then
          If TestPreSalvaArtico() = False Then Return False
        End If
        '------------------------------------------------------------------------------------------------------------
        Dim dtrTmp() As DataRow = Nothing
        If bDelete = False Then
          dtrTmp = dsSharedArtico.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Added)
        Else
          dtrTmp = dsSharedArtico.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Deleted)
        End If
        If dtrTmp.Length > 0 Then
          oCldClas.SaveDataArtAssociati(strDittaCorrente, strCodcla1Ass, strCodcla2Ass, strCodcla3Ass, _
            strCodcla4Ass, strCodcla5Ass, NTSCStr(dtrTmp(0)!ar_codart))
        End If
        '------------------------------------------------------------------------------------------------------------
      End If
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function TestPreCancella() As Boolean
    Try

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

  Public Overridable Function Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bGrigliaArticoli = False Then
        dsShared.Tables("ARTCLAS").Select(strFilter)(nRow).RejectChanges()
        bHasChanges = False
      Else
        dsSharedArtico.Tables("ARTICO").Select(strFilter)(nRow).RejectChanges()
        bHasChangesArtico = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
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
  Public Overridable Sub BeforeColUpdate_acl_codcla(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      e.ProposedValue = NTSCStr(e.ProposedValue).ToUpper
      If dsShared.Tables("ARTCLAS").Rows.Count > 1 Then
        dtrTmp = dsShared.Tables("ARTCLAS").Select("acl_codcla = " & CStrSQL(e.ProposedValue.ToString()))
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222113437500, "Codice già esistente: inserire un nuovo codice")))
          Return
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

  Public Overridable Function EliminaNodo(ByVal strTag As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldClas.EliminaNodo(strDittaCorrente, strTag)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

#Region "Griglia Articoli Associati"
  Public Overridable Function ApriGrigliaArtico(ByRef dsOut As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not dsSharedArtico Is Nothing AndAlso dsSharedArtico.Tables.Contains("ARTICO") Then
        RemoveHandler dsSharedArtico.Tables("ARTICO").ColumnChanging, AddressOf BeforeColUpdateArtico
        RemoveHandler dsSharedArtico.Tables("ARTICO").ColumnChanged, AddressOf AfterColUpdateArtico
        dsSharedArtico.Tables("ARTICO").Clear()
        dsSharedArtico.Tables("ARTICO").Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not oCldClas.GetDataArtAssociati(strDittaCorrente, strCodcla1Ass, strCodcla2Ass, strCodcla3Ass, _
        strCodcla4Ass, strCodcla5Ass, dsOut) Then Return False
      '--------------------------------------------------------------------------------------------------------------
      SetDefaultValueArtico(dsOut)
      '--------------------------------------------------------------------------------------------------------------
      dsOut.AcceptChanges()
      dsSharedArtico = dsOut
      '--------------------------------------------------------------------------------------------------------------
      AddHandler dsSharedArtico.Tables("ARTICO").ColumnChanging, AddressOf BeforeColUpdateArtico
      AddHandler dsSharedArtico.Tables("ARTICO").ColumnChanged, AddressOf AfterColUpdateArtico
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

  Public Overridable Function AssociaArticoli(ByVal dttTmp As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldClas.AssociaArticoli(strDittaCorrente, nLivello, strCodcla1Ass, strCodcla2Ass, strCodcla3Ass, _
        strCodcla4Ass, strCodcla5Ass, dttTmp)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function EliminaAssociazione() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldClas.EliminaAssociazione(strDittaCorrente, strCodcla1Ass, strCodcla2Ass, strCodcla3Ass, _
        strCodcla4Ass, strCodcla5Ass)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

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

  Public Overridable Sub BeforeColUpdateArtico(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(oApp.Tr(Me, 130421927722858399, strErr))
      '--------------------------------------------------------------------------------------------------------------
      Dim strFunction As String = "BeforeColUpdateArtico_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdateArtico_ar_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strDescrClass As String = ""
    Dim dtrTmp() As DataRow
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.ProposedValue.ToString.Trim = "" Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130421944373546874, "Attenzione!" & vbCrLf & _
          "Inserire un codice articolo valido.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.ProposedValue = NTSCStr(e.ProposedValue).ToUpper
      '--------------------------------------------------------------------------------------------------------------
      If dsSharedArtico.Tables("ARTICO").Rows.Count > 1 Then
        dtrTmp = dsSharedArtico.Tables("ARTICO").Select("ar_codart = " & CStrSQL(e.ProposedValue.ToString()))
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130421943686550270, "Attenzione!" & vbCrLf & _
            "Codice articolo associato già esistente." & vbCrLf & _
            "Inserire un nuovo codice articolo.")))
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldClas.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", "", dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130421945574614901, "Attenzione!" & vbCrLf & _
          "Codice articolo associato inesistente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then
        If NTSCStr(dttTmp.Rows(0)!ar_codcla1).Trim <> "" Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          strDescrClass = oCldClas.RitornaDescrClass(strDittaCorrente, dttTmp.Rows(0))
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130422026726497340, "Attenzione!" & vbCrLf & _
            "L'articolo selezionato risulta già associato alla classificazione:" & vbCrLf & _
            " . '|" & strDescrClass & "|'")))
          Return
        End If
        e.Row!ar_descr = NTSCStr(dttTmp.Rows(0)!ar_descr)
        e.Row!ar_desint = NTSCStr(dttTmp.Rows(0)!ar_desint)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub AfterColUpdateArtico(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      bHasChangesArtico = True
      '--------------------------------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      '--------------------------------------------------------------------------------------------------------------
      e.Row.EndEdit()
      e.Row.EndEdit()
      '--------------------------------------------------------------------------------------------------------------
      Dim strFunction As String = "AfterColUpdateArtico_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Function DeleteDataArtAssociati(ByVal strCodart As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldClas.DeleteDataArtAssociati(strDittaCorrente, strCodart)
      '------------------------------------------------------------------------------------------------------------      
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public ReadOnly Property RecordIsChangedArtico() As Boolean
    Get
      Return bHasChangesArtico
    End Get
  End Property

  Public Overridable Function RipristinaArtico(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      dsSharedArtico.Tables("ARTICO").Select(strFilter)(nRow).RejectChanges()
      '--------------------------------------------------------------------------------------------------------------
      bHasChangesArtico = False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub SetDefaultValueArtico(ByRef dsArtico As DataSet)
    Try
      '--------------------------------------------------------------------------------------------------------------
      With dsArtico.Tables("ARTICO")
        .Columns("codditt").DefaultValue = strDittaCorrente
        .Columns("xx_seleziona").DefaultValue = "N"
      End With
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Function TestPreSalvaArtico() As Boolean
    Dim dtrTmp() As DataRow = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      dtrTmp = dsSharedArtico.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Added)
      For i As Integer = 0 To (dtrTmp.Length - 1)
        If NTSCStr(dtrTmp(i)!ar_codart).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130422043224724280, "Attenzione!" & vbCrLf & _
            "Inserire un articolo valido.")))
          Return False
        End If
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
#End Region

#Region "Descrizioni in Lingua"
  Public Overridable Function ApriGrigliaLingue(ByRef dttLingue As DataTable) As Boolean
    Dim bResult As Boolean = True
    Try
      If dsShared IsNot Nothing AndAlso dsShared.Tables.Contains("ARTCLAS") AndAlso nLivello > 1 Then
        If dsShared.Tables.Contains("ARTCLASLIN") Then
          RemoveHandler dsShared.Tables("ARTCLASLIN").ColumnChanging, AddressOf BeforeColUpdate
          RemoveHandler dsShared.Tables("ARTCLASLIN").ColumnChanged, AddressOf AfterColUpdate
          dsShared.Tables.Remove("ARTCLASLIN")
        End If


        oCldClas.GetDataInLingua(strDittaCorrente, nLivello - 1, strCodcla1, strCodcla2, strCodcla3, strCodcla4, strCodcla5, dsShared)
        dttLingue = dsShared.Tables("ARTCLASLIN")
        SetDefaultValueLingue(dttLingue)

        dttLingue.AcceptChanges()

        AddHandler dsShared.Tables("ARTCLASLIN").ColumnChanging, AddressOf BeforeColUpdate
        AddHandler dsShared.Tables("ARTCLASLIN").ColumnChanged, AddressOf AfterColUpdate
      Else
        bResult = False
      End If

      Return bResult
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Sub SetDefaultValueLingue(ByVal dttLingue As DataTable)
    Try
      With dttLingue
        .Columns("codditt").DefaultValue = strDittaCorrente
        If nLivello - 1 >= 1 Then .Columns("acx_codcla1").DefaultValue = strCodcla1
        If nLivello - 1 >= 2 Then .Columns("acx_codcla2").DefaultValue = strCodcla2
        If nLivello - 1 >= 3 Then .Columns("acx_codcla3").DefaultValue = strCodcla3
        If nLivello - 1 >= 4 Then .Columns("acx_codcla4").DefaultValue = strCodcla4
        If nLivello - 1 = 5 Then .Columns("acx_codcla5").DefaultValue = strCodcla5
      End With
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub CancellaDescrizioneInLingua(ByVal nLingua As Integer)
    Try
      oCldClas.CancellaDescrizioneInLingua(strDittaCorrente, nLivello - 1, strCodcla1, strCodcla2, strCodcla3, strCodcla4, strCodcla5, nLingua)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Function SalvaDescrizioneInLingua(ByVal dtrN As DataRow, ByRef strMess As String) As Boolean
    Dim bResult As Boolean = False
    Try
      If TestPreSalvaDescrizioneInLingua(dtrN, strMess) Then
        dtrN!acx_ultagg = Now
        oCldClas.SalvaDescrizioneInLingua(strDittaCorrente, nLivello - 1, dtrN)
        dtrN.AcceptChanges()
        bResult = True
      End If

      Return bResult
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function TestPreSalvaDescrizioneInLingua(ByVal dtrN As DataRow, ByRef strMess As String) As Boolean
    Dim strbMess As New StringBuilder
    Dim bResult As Boolean = True
    Try
      If NTSCInt(dtrN!acx_codvalu) = 0 Then
        strbMess.AppendLine(oApp.Tr(Me, 129985771274789182, "Il codice lingua è obbligatorio."))
        bResult = False
      End If
      If NTSCStr(dtrN!acx_descla).Length = 0 Then
        strbMess.AppendLine(oApp.Tr(Me, 129985771274789183, "La descrizione in lingua è obbligatoria."))
        bResult = False
      End If

      strMess = strbMess.ToString
      Return bResult
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Sub BeforeColUpdate_acx_codvalu(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strDescr As String = ""
    Dim dtrTmp() As DataRow = Nothing
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        If dsShared.Tables("ARTCLASLIN").Rows.Count > 1 Then
          dtrTmp = dsShared.Tables("ARTCLASLIN").Select("acx_codvalu = " & NTSCInt(e.ProposedValue))
          If dtrTmp.Length > 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222113437501, "Per questo codice lingua è già stata inserita una descrizione.")))
            Return
          End If
        End If
        If ocldBase.ValCodiceDb(NTSCStr(e.ProposedValue), "", "TABLING", "N", strDescr) Then
          e.Row!xx_codvalu = strDescr
        Else
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222113437502, "Il codice lingua non è esistente.")))
          Return
        End If
      Else
        e.Row!xx_codvalu = ""
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
#End Region
End Class