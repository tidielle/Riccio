Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEVEBANC
  Inherits CLE__BASN
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

  Public bModCG As Boolean = False     'Se True la ditta gestisce la contabilità generale
  Public bModExtCGE As Boolean = False 'Se True la ditta gestisce la contabilità generale easy


  Public oCldBanc As CLDVEBANC 'oggetto dal
  Public dsShared As DataSet

  Public bHasChanges As Boolean = False

  Public bCobaNew As Boolean
  Public bCobaHasChanges As Boolean
  Public dsCobaShared As DataSet
  Public lCodBanc As Integer = 0
  Public strDescrBanc As String
  Public strCobaPrevCelValue As String = ""

  Public dsTassShared As DataSet
  Public bTassHasChanges As Boolean = False

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDVEBANC"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldBanc = CType(MyBase.ocldBase, CLDVEBANC)
    oCldBanc.Init(oApp)
    Return True
  End Function

  Public Overridable Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim strTmp As String = ""
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldBanc.LeggiTabellaSemplice(strDittaCorrente, "TABBANC", ds)
      If dReturn = False Then Return False

      '--------------------------------------
      'aggiungo le descrizioni dei sottoconti
      ds.Tables("TABBANC").Columns.Add("xx_conbanc", GetType(String))
      ds.Tables("TABBANC").Columns.Add("xx_conanticip", GetType(String))
      For Each dtrT As DataRow In ds.Tables("TABBANC").Rows
        If NTSCInt(dtrT!tb_conbanc) <> 0 Then
          oCldBanc.ValCodiceDb(NTSCInt(dtrT!tb_conbanc).ToString, strDittaCorrente, "ANAGRA", "N", strTmp)
          dtrT!xx_conbanc = strTmp
        Else
          dtrT!xx_conbanc = ""
        End If
        If NTSCInt(dtrT!tb_conanticip) <> 0 Then
          oCldBanc.ValCodiceDb(NTSCInt(dtrT!tb_conanticip).ToString, strDittaCorrente, "ANAGRA", "N", strTmp)
          dtrT!xx_conanticip = strTmp
        Else
          dtrT!xx_conanticip = ""
        End If
      Next
      ds.Tables("TABBANC").AcceptChanges()

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldBanc.SetTableDefaultValueFromDB("TABBANC", ds)

      SetDefaultValue(ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("TABBANC").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TABBANC").ColumnChanged, AddressOf AfterColUpdate

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
  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      Else
        'cancello i record dei tassi e condizioni bancarie
        Dim dtrTmp As DataRow() = dsShared.Tables("TABBANC").Select(Nothing, Nothing, DataViewRowState.Deleted)

        For i = 0 To dtrTmp.Length - 1
          oCldBanc.CancellaTassiCondbanc(NTSCInt(dtrTmp(i)("tb_codbanc", DataRowVersion.Original).ToString()), strDittaCorrente)
        Next
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldBanc.ScriviTabellaSemplice(strDittaCorrente, "TABBANC", dsShared.Tables("TABBANC"), "", "", "")

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
  Public Overridable Sub Nuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsShared.Tables("TABBANC").Rows.Add(dsShared.Tables("TABBANC").NewRow)
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
      dsShared.Tables("TABBANC").Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property

  Public Overridable Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      '-------------------------------------------------
      'Imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("TABBANC").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("TABBANC").Columns("tb_accinat").DefaultValue = 6
      ds.Tables("TABBANC").Columns("tb_addimpa").DefaultValue = 6

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

  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper + ";"
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
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
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

  Public Overridable Sub BeforeColUpdate_tb_codbanc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow = Nothing
    Dim strErrore As String = ""
    Try
      If dsShared.Tables("TABBANC").Rows.Count > 1 Then
        dtrTmp = dsShared.Tables("TABBANC").Select("tb_codbanc = " & e.ProposedValue.ToString())
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128171551470156250, "Cod. Banca già esistente: inserire un nuovo Codice Banca.")))
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
  Public Overridable Sub BeforeColUpdate_tb_conbanc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bOk As Boolean = False
    Dim dtrTmp() As DataRow = Nothing
    Dim evt As NTSEventArgs = Nothing
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) > 0 Then

        If bModCG = True Or bModExtCGE = True Then
          bOk = oCldBanc.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRAS", "N", strTmp)
          If bOk = False Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128171551533437500, "Cod. Conto P.d.C. inesistente.")))
            Return
          End If
        End If

        If dsShared.Tables("TABBANC").Rows.Count > 1 Then
          dtrTmp = dsShared.Tables("TABBANC").Select("tb_conbanc = " & e.ProposedValue.ToString())
          If dtrTmp.Length > 0 Then
            evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128171551599843750, _
                                   "Cod. Conto P.d.C. già utilizzato nella Banca n° |" & NTSCInt(dtrTmp(0)!tb_codbanc) & "|." & vbCrLf & _
                                   "Proseguire ugualmente?"))
            ThrowRemoteEvent(evt)
            If evt.RetValue = "NO" Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              Return
            End If
          End If
        End If
        e.Row!xx_conbanc = strTmp
      Else
        e.Row!xx_conbanc = ""
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
  Public Overridable Sub BeforeColUpdate_tb_abi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bOk As Boolean
    Try
      If NTSCInt(e.ProposedValue) > 0 Then
        bOk = oCldBanc.ValCodiceDb(e.ProposedValue.ToString, "", "ABI", "N")
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128171551662812500, "Cod. ABI inesistente.")))
          Return
        Else
          e.Row!tb_cab = 0
          e.Row!tb_swift = ""
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
  Public Overridable Sub BeforeColUpdate_tb_cab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bOk As Boolean
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue) > 0 Then
        If NTSCInt(e.Row.Item("tb_abi")) = 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128171551745156250, "Inserire il Cod. ABI prima di digitare il Cod. CAB.")))
        Else
          bOk = oCldBanc.ValCodiceDb(e.ProposedValue.ToString, "", "CAB", "N", "", dttTmp, e.Row.Item("tb_abi").ToString)
          If bOk = False Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128171551801875000, "Cod. CAB inesistente.")))
            Return
          Else
            e.Row!tb_swift = NTSCStr(dttTmp.Rows(0)!abcswift)
          End If
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
    Finally
      dttTmp.Clear()
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_conanticip(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bOk As Boolean = False
    Dim dtrTmp() As DataRow = Nothing
    Dim evt As NTSEventArgs = Nothing
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) > 0 Then

        If bModCG = True Or bModExtCGE = True Then
          bOk = oCldBanc.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRAS", "N", strTmp)
          If bOk = False Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129555551850683593, "Cod. Conto P.d.C. inesistente.")))
            Return
          Else
            e.Row!xx_conanticip = strTmp
          End If
        End If
      Else
        e.Row!xx_conanticip = ""
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

  Public Overridable Function TestPreSalva() As Boolean
    'Eseguo i controlli necessari prima di fare il salvataggio della riga
    Dim dtrTmp As DataRow() = dsShared.Tables("TABBANC").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
    Dim i As Integer = 0
    Try
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!tb_codbanc) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128171551867500000, "Il Cod. Banca deve essere un valore compreso tra 1 e 9999.")))
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
  Public Overridable Function TestPreCancella(ByVal nCodBanc As Integer, ByRef strMsg As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldBanc.TestPreCancella(strDittaCorrente, nCodBanc, strMsg)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function

  Public Overridable Sub AfterColUpdate_tb_abi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("AggiornaColoreAbiCab", ""))
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub AfterColUpdate_tb_cab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("AggiornaColoreAbiCab", ""))
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub


#Region "FRMTECOBA"
  Public Overridable Sub CobaSetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("CONDBAN").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("CONDBAN").Columns("cb_codbanc").DefaultValue = lCodBanc
      ds.Tables("CONDBAN").Columns("cb_causale").DefaultValue = "0"
      ds.Tables("CONDBAN").Columns("cb_addspop").DefaultValue = "N"
      ds.Tables("CONDBAN").Columns("cb_spesaop").DefaultValue = " 0"
      ds.Tables("CONDBAN").Columns("cb_avaind").DefaultValue = "+"
      ds.Tables("CONDBAN").Columns("cb_ggvalu").DefaultValue = "0"
      ds.Tables("CONDBAN").Columns("cb_valfiss").DefaultValue = "S"
      ds.Tables("CONDBAN").Columns("cb_impboll").DefaultValue = "0"
      ds.Tables("CONDBAN").Columns("xx_desbanc").DefaultValue = strDescrBanc

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

  Public Overridable Function CobaApri(ByRef dsCoba As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      dReturn = oCldBanc.GetCondBan(strDittaCorrente, lCodBanc, dsCoba)
      If dReturn = False Then Return False

      strDescrBanc = ""
      oCldBanc.ValCodiceDb(NTSCStr(lCodBanc), strDittaCorrente, "TABBANC", "N", strDescrBanc)

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldBanc.SetTableDefaultValueFromDB("CONDBAN", dsCoba)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      CobaSetDefaultValue(dsCoba)
      dsCobaShared = dsCoba

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsCobaShared.Tables("CONDBAN").ColumnChanging, AddressOf CobaBeforeColUpdate
      AddHandler dsCobaShared.Tables("CONDBAN").ColumnChanged, AddressOf CobaAfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsCobaShared.Tables("CONDBAN").AcceptChanges()
      bCobaHasChanges = False
      bCobaNew = False

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
  Public Overridable Sub CobaNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsCobaShared.Tables("CONDBAN").Rows.Add(dsCobaShared.Tables("CONDBAN").NewRow)
      bCobaHasChanges = True
      bCobaNew = True
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
  Public Overridable Function CobaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsCobaShared.Tables("CONDBAN").Select(strFilter)(nRow).RejectChanges()
      bCobaHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Function CobaSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not CobaTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldBanc.ScriviTabellaSemplice(strDittaCorrente, "CONDBAN", dsCobaShared.Tables("CONDBAN"), "", "", "")
      If bResult Then
        bCobaHasChanges = False
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
  Public Overridable Function CobaTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim dsTmp As DataSet = Nothing
    Try
      dtrTmp = dsCobaShared.Tables("CONDBAN").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!cb_causale) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222114062500, "Il campo causale banca deve contenere un valore diverso da 0")))
          Return False
        End If
        oCldBanc.GetCheckCondBan(strDittaCorrente, lCodBanc, NTSCInt(dtrTmp(i)!cb_causale), dsTmp)
        If (dsTmp.Tables("CONDBAN").Rows.Count > 0 And bCobaNew) Or (dsTmp.Tables("CONDBAN").Rows.Count > 1 And Not bCobaNew) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129012222715757871, "Condizione bancaria già esistente.")))
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

  Public ReadOnly Property CobaRecordIsChanged() As Boolean
    Get
      Return bCobaHasChanges
    End Get
  End Property

  Public Overridable Sub CobaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strCobaPrevCelValue = e.Column.ColumnName.ToUpper + ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CobaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub CobaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strCobaPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strCobaPrevCelValue = strCobaPrevCelValue.Remove(strCobaPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bCobaHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CobaAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub CobaBeforeColUpdate_cb_causale(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim strDescauc As String
    Try
      strDescauc = NTSCStr(e.Row!xx_descauc)

      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_descauc = ""
      Else
        If Not oCldBanc.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAUC", "N", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128399392980742000, "Codice causale non corretto")))
        Else
          e.Row!xx_descauc = strTmp
        End If
      End If

      oCldBanc.GetCheckCondBan(strDittaCorrente, lCodBanc, NTSCInt(e.ProposedValue.ToString), dsTmp)
      If dsTmp.Tables("CONDBAN").Rows.Count > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        e.Row!xx_descauc = strDescauc
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129012222650912046, "Condizione bancaria già esistente.")))
        Return
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

#Region "FRMTETASS"
  Public Overridable Function TassApri(ByVal strDitta As String, ByRef dsTass As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldBanc.GetTassi(lCodBanc, strDitta, dsTass)
      If dReturn = False Then Return False

      oCldBanc.SetTableDefaultValueFromDB("TASSI", dsTass)

      dsTassShared = dsTass

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsTassShared.Tables("TASSI").ColumnChanging, AddressOf TassBeforeColUpdate
      AddHandler dsTassShared.Tables("TASSI").ColumnChanged, AddressOf TassAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record

      dsTassShared.Tables("TASSI").Columns("tx_codbanc").DefaultValue = lCodBanc
      dsTassShared.Tables("TASSI").Columns("tx_dtvalid").DefaultValue = "01/01/1900"
      dsTassShared.Tables("TASSI").Columns("codditt").DefaultValue = strDittaCorrente

      dsTassShared.Tables("TASSI").AcceptChanges()

      bTassHasChanges = False

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
  Public Overridable Function TassSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not TassTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldBanc.ScriviTabellaSemplice(strDittaCorrente, "TASSI", dsTassShared.Tables("TASSI"), "", "", "")

      If bResult Then
        bTassHasChanges = False
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
  Public ReadOnly Property TassRecordIsChanged() As Boolean
    Get
      Return bTassHasChanges
    End Get
  End Property
  Public Overridable Function TassTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim dsTmp As DataSet = Nothing
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsTassShared.Tables("TASSI").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If dtrCurrRow(i).RowState = DataRowState.Added Then
          oCldBanc.GetDataValidita(NTSCInt(dtrCurrRow(i)!tx_codbanc), NTSCDate(dtrCurrRow(i)!tx_dtvalid).ToShortDateString, strDittaCorrente, dsTmp)
          If dsTmp.Tables("TASSI").Rows.Count > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128263652089290727, "Data validità già esistente.")))
            Return False
          End If
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
  Public Overridable Sub TassNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsTassShared.Tables("TASSI").Rows.Add(dsTassShared.Tables("TASSI").NewRow)
      bTassHasChanges = True

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
  Public Overridable Function TassRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsTassShared.Tables("TASSI").Select(strFilter)(nRow).RejectChanges()
      bTassHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub TassBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "TassBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub TassAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bTassHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "TassAfterColUpdate_" & e.Column.ColumnName.ToLower
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

End Class
