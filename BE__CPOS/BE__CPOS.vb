Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLE__CPOS
  Inherits CLE__BASE

  Public oCldCpos As CLD__CPOS

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

  Public Overrides Function Init(ByRef App As CLE__APP, _
                                  ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                                  ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                                  ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__CPOS"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldCpos = CType(MyBase.ocldBase, CLD__CPOS)
    oCldCpos.Init(oApp)

    '-------------------------------------------------
    'gestione di actlog
    'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
    strActLog = ocldBase.GetSettingBus("BS__CPOS", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
    If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
    strActLogProg = "BS__CPOS"
    strActLogNomOggLog = "tabcpos"
    strActLogDesLog = oApp.Tr(Me, 130010942284687500, "Configurazione POS")

    Return True
  End Function

  Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As System.Data.DataSet) As Boolean
    Try
      If Not MyBase.Apri(strDitta, ds) Then Return False

      'aggiunge la descrizione delle forme di pagamento.
      ds.Tables("TABCPOS").Columns.Add("xx_cfgnum1", GetType(String))
      ds.Tables("TABCPOS").Columns.Add("xx_cfgnum2", GetType(String))

      For Each dtrRow As DataRow In ds.Tables("TABCPOS").Rows
        dtrRow!xx_cfgnum1 = TrovaDescrizionePagamento(NTSCInt(dtrRow!tb_cfgnum1))
        dtrRow!xx_cfgnum2 = TrovaDescrizionePagamento(NTSCInt(dtrRow!tb_cfgnum2))
      Next

      ds.AcceptChanges()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function TrovaDescrizionePagamento(ByVal lCodPaga As Integer) As String
    Dim strDescr As String = ""
    Try
      If lCodPaga <> 0 Then oCldCpos.ValCodiceDb(lCodPaga.ToString, strDittaCorrente, "TABPAGA", "N", strDescr)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return strDescr
  End Function


  Public Overridable Function IsPOSDeletable(ByVal lPOS As Integer) As Boolean
    Dim dttTmp As New DataTable
    Try
      If lPOS = 0 Then Return True

      If Not oCldCpos.IsPOSDeletable(strDittaCorrente, lPOS, dttTmp) Then Return False

      If dttTmp.Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129382584696337600, "La configurazione POS è in uso da un punto cassa. Impossibile cancellarlo")))
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

  Public Overrides Function TestPreSalva() As Boolean
    Try
      For Each dtrRow As DataRow In dsShared.Tables("TABCPOS").Select("", "", DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
        If NTSCInt(dtrRow!tb_codcpos) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129382560328593750, "Specificare un codice diverso da 0.")))
          Return False
        End If

        If dsShared.Tables("TABCPOS").Select("tb_codcpos = " & NTSCInt(dtrRow!tb_codcpos)).Length > 1 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129382561529375000, "Il codice è già in uso da un'altro POS.")))
          Return False
        End If

        If NTSCStr(dtrRow!tb_filein).Trim = "" OrElse NTSCStr(dtrRow!tb_fileout).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129382560675781250, "Specificare il percorso dei file di scambio.")))
          Return False
        End If

        If NTSCStr(dtrRow!tb_descpos).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129382565761718750, "Inserire la descrizione.")))
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
  Public Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      e.Row.EndEdit()
      e.Row.EndEdit()

      bHasChanges = True

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

  Public Overridable Sub BeforeColUpdate_tb_cfgnum1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttPaga As New DataTable
    Try
      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xx_cfgnum1 = "" : Return

      If oCldCpos.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABPAGA", "N", , dttPaga) Then
        If NTSCStr(dttPaga.Rows(0)!tb_tipincecr) <> "B" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130918820878099298, "La forma di pagamento deve essere di tipo bancomat.")))
          e.ProposedValue = e.Row!tb_cfgnum1
        Else
          e.Row!xx_cfgnum1 = dttPaga.Rows(0)!tb_despaga
        End If
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130918819674645676, "Pagamento non valido")))
        e.ProposedValue = e.Row!tb_cfgnum1
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
  Public Overridable Sub BeforeColUpdate_tb_cfgnum2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttPaga As New DataTable
    Try
      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xx_cfgnum2 = "" : Return

      If oCldCpos.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABPAGA", "N", , dttPaga) Then
        If NTSCStr(dttPaga.Rows(0)!tb_tipincecr) <> "V" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130918821476813454, "La forma di pagamento deve essere di tipo carta di credito.")))
          e.ProposedValue = e.Row!tb_cfgnum2
        Else
          e.Row!xx_cfgnum2 = dttPaga.Rows(0)!tb_despaga
        End If
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130918821494232028, "Pagamento non valido")))
        e.ProposedValue = e.Row!tb_cfgnum2
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


  Public Overrides Sub SetDefaultValue(ByRef ds As System.Data.DataSet)
    Try
      ds.Tables("TABCPOS").Columns("codditt").DefaultValue = strDittaCorrente
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class
