Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGMAGA
  Inherits CLE__BASE

  Public oCldMaga As CLDMGMAGA
  Public lCodStab As Integer = 0
  Public bModRME As Boolean = False

  Private Moduli_P As Integer = CLN__STD.bsModMG + CLN__STD.bsModVE
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGMAGA"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldMaga = CType(MyBase.ocldBase, CLDMGMAGA)
    oCldMaga.Init(oApp)

    Return True
  End Function

  Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldMaga.GetData(strDittaCorrente, ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      ocldBase.SetTableDefaultValueFromDB(strNomeTabella, ds)

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

  Public Overloads Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Dim strDescr As String = ""
    Try
      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = ocldBase.GetSettingBus("BSMGMAGA", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BSMGMAGA"
      strActLogNomOggLog = "tabmaga"
      strActLogDesLog = oApp.Tr(Me, 128058229647869287, "dati relativi al magazzino")

      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("TABMAGA").Columns("tb_desmaga").DefaultValue = " "
      ds.Tables("TABMAGA").Columns("tb_flclavo").DefaultValue = " "
      ds.Tables("TABMAGA").Columns("tb_appscmin").DefaultValue = "S"
      ds.Tables("TABMAGA").Columns("tb_consmrp").DefaultValue = "S"
      ds.Tables("TABMAGA").Columns("tb_gescl").DefaultValue = "N"
      ds.Tables("TABMAGA").Columns("tb_applotti").DefaultValue = "S"
      'fuori griglia
      ds.Tables("TABMAGA").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("TABMAGA").Columns("tb_progr").DefaultValue = 0

      If lCodStab > 0 Then
        ds.Tables("TABMAGA").Columns("tb_codstab").DefaultValue = lCodStab
        oCldMaga.ValCodiceDb(lCodStab.ToString, strDittaCorrente, "TABSTAB", "N", strDescr)
        ds.Tables("TABMAGA").Columns("xx_desstab").DefaultValue = strDescr
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

  Public Overloads Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
  Public Overridable Sub BeforeColUpdate_tb_codmaga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Dim strErrore As String = ""

    Try
      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("TABMAGA").Select("tb_codmaga = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222086718750, "Il codice magazzino inserito è già stato utilizzato. Inserire un codice non utilizzato")))
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
  Public Overridable Sub BeforeColUpdate_tb_magconto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_magconto = ""
        e.Row!tb_coddest = 0
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRACF", "N", strTmp)
        If bOk = False Then
          e.Row!tb_coddest = 0
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222086875000, "Codice cliente/fornitore non corretto")))
          Return
        Else
          e.Row!xx_magconto = strTmp
          e.Row!tb_coddest = 0
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
  Public Overridable Sub BeforeColUpdate_tb_magass(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_magass = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "MAGA", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222087031250, "Codice magazzino assimilato non corretto")))
          Return
        Else
          e.Row!xx_magass = strTmp
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
  Public Overridable Sub BeforeColUpdate_tb_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_coddest = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "DESTDIV", "N", strTmp, Nothing, e.Row!tb_magconto.ToString)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127898309239122236, "Codice destinazione diversa non corretta")))
          Return
        Else
          e.Row!xx_coddest = strTmp
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
  Public Overridable Sub BeforeColUpdate_tb_codstab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_desstab = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAB", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128788229466088982, "Codice stabilimento inesistente")))
          Return
        Else
          e.Row!xx_desstab = strTmp
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
  Public Overridable Sub BeforeColUpdate_tb_dlottox(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      'se vecchia gestione lotti, può essere solo un lotto numerico
      If NTSCStr(e.ProposedValue) <> "" Then
        oCldMaga.ValCodiceDb(strDittaCorrente, strDittaCorrente, "ANADITAC", "S", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          If NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "N" Then
            If Not IsNumeric(e.ProposedValue) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129519888747214310, "Il lotto deve un numero compreso tra 0 e 999999999 (oppure lasciare il campo vuoto)")))
              e.ProposedValue = ""
            Else
              e.ProposedValue = NTSCInt(e.ProposedValue).ToString("000000000")
            End If
          End If
        End If    'If dttTmp.Rows.Count > 0 Then
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
  Public Overridable Sub BeforeColUpdate_tb_dubicaz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strUbic As String
    Try
      strUbic = NTSCStr(e.ProposedValue)
      If Not ValidaUbicazione(NTSCInt(e.Row!tb_codmaga), strUbic) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129844955606766488, "Ubicazione inesistente")))
        e.ProposedValue = e.Row(e.Column.Caption)
      Else
        e.ProposedValue = strUbic
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
  Public Overridable Sub BeforeColUpdate_tb_dubicst(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strUbic As String
    Try
      strUbic = NTSCStr(e.ProposedValue)
      If Not ValidaUbicazione(NTSCInt(e.Row!tb_codmaga), strUbic) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129844955592716768, "Ubicazione inesistente")))
        e.ProposedValue = e.Row(e.Column.Caption)
      Else
        e.ProposedValue = strUbic
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
  Public Overridable Sub BeforeColUpdate_tb_dubicpr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strUbic As String
    Try
      strUbic = NTSCStr(e.ProposedValue)
      If Not ValidaUbicazione(NTSCInt(e.Row!tb_codmaga), strUbic) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129844955579759804, "Ubicazione inesistente")))
        e.ProposedValue = e.Row(e.Column.Caption)
      Else
        e.ProposedValue = strUbic
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
  Public Overridable Sub BeforeColUpdate_tb_dubicri(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strUbic As String
    Try
      strUbic = NTSCStr(e.ProposedValue)
      If Not ValidaUbicazione(NTSCInt(e.Row!tb_codmaga), strUbic) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129844955563524572, "Ubicazione inesistente")))
        e.ProposedValue = e.Row(e.Column.Caption)
      Else
        e.ProposedValue = strUbic
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
  Public Overridable Sub BeforeColUpdate_tb_dubicus(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strUbic As String
    Try
      strUbic = NTSCStr(e.ProposedValue)
      If Not ValidaUbicazione(NTSCInt(e.Row!tb_codmaga), strUbic) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129844955545416044, "Ubicazione inesistente")))
        e.ProposedValue = e.Row(e.Column.Caption)
      Else
        e.ProposedValue = strUbic
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

  Public Overloads Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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

  Public Overloads Overrides Function TestPreSalva() As Boolean
    Dim i As Integer = 0
    Dim dtrTmp() As DataRow
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      dtrTmp = dsShared.Tables("TABMAGA").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If dtrTmp(i)!tb_codmaga.ToString = "0" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222087187500, "Inserire un codice magazzino compreso tra 1 e 999")))
          Return False
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      dtrTmp = dsShared.Tables("TABMAGA").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If oCldMaga.ValCodiceDb(dtrTmp(i)!tb_codmaga.ToString, strDittaCorrente, "TABMAGA", "N", "", dttTmp) = True Then
          With dttTmp.Rows(0)
            If (((NTSCStr(!tb_flclavo) = " ") Or (NTSCStr(!tb_flclavo) = "F")) And _
               ((dtrTmp(i)!tb_flclavo.ToString = "C") Or (dtrTmp(i)!tb_flclavo.ToString = "X"))) Or _
               (((NTSCStr(!tb_flclavo) = "C") Or (NTSCStr(!tb_flclavo) = "X")) And _
               ((dtrTmp(i)!tb_flclavo.ToString = " ") Or (dtrTmp(i)!tb_flclavo.ToString = "F"))) Then
              If oCldMaga.CheckEsistenzaARTDEF(strDittaCorrente, NTSCInt(dtrTmp(i)!tb_codmaga)) = True Then
                Dim msg As NTSEventArgs
                Dim strTipoPrima As String = ""
                Dim strTipoDopo As String = ""
                Select Case NTSCStr(!tb_flclavo).Trim
                  Case "" : strTipoPrima = "Propria"
                  Case "C" : strTipoPrima = "Altrui"
                  Case "F" : strTipoPrima = "Propria presso terzi"
                  Case "X" : strTipoPrima = "Altrui presso terzi"
                End Select
                Select Case NTSCStr(dtrTmp(i)!tb_flclavo).Trim
                  Case "" : strTipoDopo = "Propria"
                  Case "C" : strTipoDopo = "Altrui"
                  Case "F" : strTipoDopo = "Propria presso terzi"
                  Case "X" : strTipoDopo = "Altrui presso terzi"
                End Select
                msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 130814259864064702, "Attenzione!" & vbCrLf & _
                  "Si sta cambiando la tipologia di magazzino da:" & vbCrLf & _
                  " . '|" & strTipoPrima & "|'" & vbCrLf & _
                  "a:" & vbCrLf & _
                  " . '|" & strTipoDopo & "|'" & vbCrLf & _
                  "in presenza di Chiusure di Magazzino che coinvolgono questo magazzino." & vbCrLf & _
                  "Questo determina incongruenze e disallineamenti nella valorizzazioni dei magazzini di merce propria." & vbCrLf & _
                  "Procedere?"))
                ThrowRemoteEvent(msg)
                If msg.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
              End If
            End If
          End With
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
      Return False
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function ValidaUbicazione(ByVal lMaga As Integer, ByRef strUbic As String) As Boolean
    Dim dttUbic As DataTable = Nothing
    Try
      If strUbic.Trim = "" Or Not bModRME Then Return True

      If Not oCldMaga.ValidaUbicazione(strDittaCorrente, lMaga, strUbic, dttUbic) Then Return False

      If dttUbic.Rows.Count = 0 Then Return False

      strUbic = NTSCStr(dttUbic.Rows(0)!au_ubicaz)

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
