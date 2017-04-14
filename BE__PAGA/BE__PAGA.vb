Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLE__PAGA
  Inherits CLE__BASE

  Public oCldPaga As CLD__PAGA

#Region "Moduli"
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
#End Region
  
#Region "Inizializzazione"
  Public Overrides Function Init(ByRef App As CLE__APP, _
                                    ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                                    ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                                    ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__PAGA"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldPaga = CType(MyBase.ocldBase, CLD__PAGA)
    oCldPaga.Init(oApp)

    Return True
  End Function
#End Region

  Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldPaga.GetData(strDittaCorrente, ds)

      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldPaga.SetTableDefaultValueFromDB(strNomeTabella, ds)

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

  Public Overridable Sub AfterColUpdate_tb_flcondp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bOk As Boolean = False
    Dim strTmp As String = ""

    Try
      If NTSCStr(e.ProposedValue) = "N" Then
        e.Row!tb_tipp_1 = 4
        e.Row!tb_tipp_2 = 4
        e.Row!tb_tipp_3 = 4
        e.Row!tb_tipp_4 = 4
        e.Row!tb_tipp_5 = 4
        e.Row!tb_tipp_6 = 4

        e.Row!tb_base_1 = "T"
        e.Row!tb_base_2 = "T"
        e.Row!tb_base_3 = "T"
        e.Row!tb_base_4 = "T"
        e.Row!tb_base_5 = "T"
        e.Row!tb_base_6 = "T"

        e.Row!tb_quota_1 = 0
        e.Row!tb_quota_2 = 0
        e.Row!tb_quota_3 = 0
        e.Row!tb_quota_4 = 0
        e.Row!tb_quota_5 = 0
        e.Row!tb_quota_6 = 0

        e.Row!tb_giorni_1 = "0"
        e.Row!tb_giorni_2 = 0
        e.Row!tb_giorni_3 = 0
        e.Row!tb_giorni_4 = 0
        e.Row!tb_giorni_5 = 0
        e.Row!tb_giorni_6 = 0

        e.Row!tb_desp_1 = ""
        e.Row!tb_desp_2 = ""
        e.Row!tb_desp_3 = ""
        e.Row!tb_desp_4 = ""
        e.Row!tb_desp_5 = ""
        e.Row!tb_desp_6 = ""
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

  Public Overridable Sub AfterColUpdate_tb_decpaga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) <> 6 Then
        e.Row!tb_datafdec = DBNull.Value
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

  Public Overridable Sub BeforeColUpdate_tb_codpaga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Dim strErrore As String = ""

    Try
      If dsShared.Tables("TABPAGA").Rows.Count > 1 Then
        dtrTmp = dsShared.Tables("TABPAGA").Select("tb_codpaga = " & e.ProposedValue.ToString())
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222113437500, "Codice pagamento già esistente: inserire un nuovo codice pagamento")))
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

  Public Overridable Sub BeforeColUpdate_tb_prirata(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) <> 0 And NTSCInt(e.Row!tb_numrate) < 2 Then
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222113593750, "Se 'Eccezione 1a rata' è diversa da 'Nessuna eccez.' il numero delle rate deve essere superiore a 1")))
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

  Public Overridable Sub BeforeColUpdate_tb_gioiniz2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) <> 0 And NTSCInt(e.Row!tb_numrate) < 3 Then
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222113750000, "La decorrenza della seconda rata non può essere indicata se il numero rate è inferiore a 3.")))
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

  Public Overridable Sub BeforeColUpdate_tb_concassp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bOk As Boolean = False
    Dim strTmp As String = ""

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_concassp = ""
      Else
        bOk = oCldPaga.ValCodiceDb(e.ProposedValue.ToString, "", "COVG", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222113906250, "Codice contropartita non corretto")))
          Return
        Else
          e.Row!xx_concassp = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_codstpg(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codstpg = ""
      Else
        If oCldPaga.ValCodiceDb(e.ProposedValue.ToString, "", "TABSTPG", "N", "", dttTmp) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129436405259274420, "Codice sottotipo pagamento inesistente.")))
          Return
        Else
          If NTSCInt(dttTmp.Rows(0)!tb_tippaga) <> NTSCInt(e.Row!tb_tippaga) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129436406375557518, "Attenzione!" & vbCrLf & _
              "Il sottotipo pagamento selezionato deve possedere lo stesso 'Tipo pagamento'" & vbCrLf & _
              "della forma di pagamento corrente.")))
            Return
          Else
            e.Row!xx_codstpg = NTSCStr(dttTmp.Rows(0)!tb_desstpg)
          End If
        End If
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

  Public Overridable Sub BeforeColUpdate_tb_tippaga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.Row!tb_codstpg) <> 0 Then
        If oCldPaga.ValCodiceDb(e.Row!tb_codstpg.ToString, "", "TABSTPG", "N", "", dttTmp) = True Then
          If NTSCInt(e.ProposedValue) <> NTSCInt(dttTmp.Rows(0)!tb_tippaga) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129436459136661755, "Attenzione!" & vbCrLf & _
              "Il tipo pagamento selezionato deve essere dello stesso tipo relativo al sottotipo pagamento corrente.")))
          End If
        End If
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

  Public Overridable Sub BeforeColUpdate_tb_codmodi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_codmodi = ""
      Else
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
        If oCldPaga.ValCodiceDb(e.ProposedValue.ToString, "", "TABMODI", "S", strTmp) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129453733638466797, "Codice modalità di incasso intrastat servizi inesistente.")))
        Else
          e.Row!xx_codmodi = strTmp
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub CalcolaScadenze(ByVal nCodpaga As Integer, ByVal strData As String, ByVal dTotdoc As Decimal, _
                                            ByVal dIva As Decimal, ByVal dSpese As Decimal, ByRef dttOut As DataTable)
    Dim P As CLELBMENU.ParamCalcScad = New CLELBMENU.ParamCalcScad
    Dim nTiprata(60) As Integer ' integra paramcalcscad...
    Dim nRate As Integer = 0
    Dim i As Integer = 0
    Dim dImpv As Decimal = 0
    Dim dImport As Decimal = 0
    Dim strErr As String = ""

    Try

      P.nCodpaga = nCodpaga
      P.strDatrif = strData
      P.dTotfat = dTotdoc
      P.dTotfatval = 0
      P.dIva = dIva
      P.dIvaval = 0
      P.dSpese = dSpese
      P.dSpeseval = 0

      dttOut.Clear()
      dttOut.Columns.Clear()
      dttOut.Columns.Add("rata", GetType(Integer))
      dttOut.Columns.Add("data", GetType(String))
      dttOut.Columns.Add("importo", GetType(Decimal))
      dttOut.Columns.Add("tiprata", GetType(String))

      nRate = CType(oCleComm, CLELBMENU).CalcolaScad(strDittaCorrente, P.nCodpaga, P.strDatrif, P.dTotfat, P.dTotfatval, P.dIva, _
                          P.dIvaval, P.dSpese, P.dSpeseval, P.strDatsca, P.dImpsca, P.dImpscaval, nTiprata, strErr, 0)
      If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
      If nRate < 1 Then Return
      For i = 1 To nRate
        ' aggiunge una scadenza
        dttOut.Rows.Add(dttOut.NewRow)
        With dttOut.Rows(dttOut.Rows.Count - 1)
          !rata = i
          !data = NTSCDate(P.strDatsca(i))
          !importo = P.dImpsca(i)
          !tiprata = nTiprata(i)
        End With
      Next
      dttOut.AcceptChanges()

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

  Public Overridable Function IsCodpagaDeletable(ByVal nCodpaga As Integer) As Boolean
    Dim strMsg As String = ""

    Try
      If oCldPaga.IsCodpagaDeletable(strDittaCorrente, nCodpaga, strMsg) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", strMsg))
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

  Public Overrides Sub Nuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      Dim dtrN As DataRow

      MyBase.Nuovo()

      '----------------------------------------
      'propongo il codice pagamento
      dtrN = dsShared.Tables("TABPAGA").Rows(dsShared.Tables("TABPAGA").Rows.Count - 1)
      If dsShared.Tables("TABPAGA").Rows.Count > 1 Then
        dsShared.Tables("TABPAGA").Select(Nothing, "tb_codpaga DESC")
        dtrN("tb_codpaga") = NTSCInt(dsShared.Tables("TABPAGA").Rows(dsShared.Tables("TABPAGA").Rows.Count - 2)!tb_codpaga.ToString()) + 1
      Else
        dtrN("tb_codpaga") = 1
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

  Public Overrides Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog <> "-1" Then
        bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella), "", "", "")
      Else
        bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella), _
                  strActLogProg, strActLogNomOggLog, strActLogDesLog)
      End If

      If bResult Then
        'cancello i record delle descrizioni in lingua
        oCldPaga.CalcellaPagalin()
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

  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldPaga.GetSettingBus("BS--PAGA", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BS--PAGA"
      strActLogNomOggLog = "tabpaga"
      strActLogDesLog = oApp.Tr(Me, 128058229647869287, "dati relativi alla forma di pagamento")

      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("TABPAGA").Columns("tb_despaga").DefaultValue = " "
      ds.Tables("TABPAGA").Columns("tb_numrate").DefaultValue = 1
      ds.Tables("TABPAGA").Columns("tb_tippaga").DefaultValue = 3
      ds.Tables("TABPAGA").Columns("tb_decpaga").DefaultValue = 1
      ds.Tables("TABPAGA").Columns("tb_codstpg").DefaultValue = 0

      ds.Tables("TABPAGA").Columns("tb_shiftm").DefaultValue = "N"
      ds.Tables("TABPAGA").Columns("tb_flcondp").DefaultValue = "N"
      ds.Tables("TABPAGA").Columns("tb_webvis").DefaultValue = "N"

      ds.Tables("TABPAGA").Columns("tb_desp_1").DefaultValue = ""
      ds.Tables("TABPAGA").Columns("tb_desp_2").DefaultValue = ""
      ds.Tables("TABPAGA").Columns("tb_desp_3").DefaultValue = ""
      ds.Tables("TABPAGA").Columns("tb_desp_4").DefaultValue = ""
      ds.Tables("TABPAGA").Columns("tb_desp_5").DefaultValue = ""
      ds.Tables("TABPAGA").Columns("tb_desp_6").DefaultValue = ""

      ds.Tables("TABPAGA").Columns("tb_tipp_1").DefaultValue = 4
      ds.Tables("TABPAGA").Columns("tb_tipp_2").DefaultValue = 4
      ds.Tables("TABPAGA").Columns("tb_tipp_3").DefaultValue = 4
      ds.Tables("TABPAGA").Columns("tb_tipp_4").DefaultValue = 4
      ds.Tables("TABPAGA").Columns("tb_tipp_5").DefaultValue = 4
      ds.Tables("TABPAGA").Columns("tb_tipp_6").DefaultValue = 4

      ds.Tables("TABPAGA").Columns("tb_base_1").DefaultValue = "T"
      ds.Tables("TABPAGA").Columns("tb_base_2").DefaultValue = "T"
      ds.Tables("TABPAGA").Columns("tb_base_3").DefaultValue = "T"
      ds.Tables("TABPAGA").Columns("tb_base_4").DefaultValue = "T"
      ds.Tables("TABPAGA").Columns("tb_base_5").DefaultValue = "T"
      ds.Tables("TABPAGA").Columns("tb_base_6").DefaultValue = "T"

      ds.Tables("TABPAGA").Columns("tb_tipincecr").DefaultValue = " "
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
    Dim dtrTmp As DataRow() = dsShared.Tables("TABPAGA").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dtrTmp.Length - 1)
        '------------------------------------------------------------------------------------------------------------
        If NTSCInt(dtrTmp(i)!tb_codpaga) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128913471507988415, "Attenzione!" & vbCrLf & _
            "Il campo codice pagamento deve contenere un valore compreso tra 1 e 9999.")))
          Return False
        End If
        '------------------------------------------------------------------------------------------------------------
        If NTSCInt(dtrTmp(i)!tb_codstpg) <> 0 Then
          If ocldBase.ValCodiceDb(dtrTmp(i)!tb_codstpg.ToString, strDittaCorrente, "TABSTPG", "N", "", dttTmp) = True Then
            If NTSCInt(dtrTmp(i)!tb_tippaga) <> NTSCInt(dttTmp.Rows(0)!tb_tippaga) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129436454321433098, "Attenzione!" & vbCrLf & _
                "Il tipo pagamento relativo al codice 'Sottotipo pagamento' deve essere uguale a quella della forma di pagamento corrente.")))
              Return False
            End If
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        If (NTSCInt(dtrTmp(i)!tb_decpaga) = 6) And (NTSCStr(dtrTmp(i)!tb_datafdec).Trim = "") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129652983954221539, "Attenzione!" & vbCrLf & _
            "Se il 'Tipo scadenza' indicato è 'Data fissa'" & vbCrLf & _
            "la 'Data fissa di decorrenza' è obbligatoria.")))
          Return False
        End If
        '------------------------------------------------------------------------------------------------------------
        If NTSCStr(dtrTmp(i)!tb_ggcalend) = "A" Or NTSCStr(dtrTmp(i)!tb_ggcalend) = "B" Then
          If NTSCInt(dtrTmp(i)!tb_decpaga) <> 4 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129985771274789189, "Con 'data massima scadenza' impostata su 30 o 60 GG da FM data ricev. fattura la forma di pagamento deve essere di tipo 'data diversa'")))
            Return False
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        If (NTSCInt(dtrTmp(i)!tb_numrate) > 6) And (NTSCStr(dtrTmp(i)!tb_flcondp) = "S") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130105127316479290, "Attenzione!" & vbCrLf & _
            "Se selezionate le 'Condizioni particolari' il numero rate non può essere superiore a 6." & vbCrLf & _
            "Deselezionare la scelta o diminuire il numero rate.")))
          Return False
        End If
        '------------------------------------------------------------------------------------------------------------
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
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

  Public Overridable Function CheckCod(ByVal strCod As String) As Boolean
    Try
      If strCod = "0" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130722814774277525, "Codice pagamento obbligatorio.")))
        Return False
      End If

      If oCldPaga.ValCodiceDb(strCod, strDittaCorrente, "TABPAGA", "N") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130722814796309339, "Codice pagamento '|" & strCod & "|' già esistente.")))
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
      Return False
    End Try
  End Function
  Public Overridable Function Duplica(ByVal strCodNew As String, ByVal strCodOld As String, _
                                      ByVal bLing As Boolean) As Boolean
    Dim i As Integer
    Dim strColname As String = ""
    Dim dtrTmp() As DataRow = Nothing
    Dim dReturn As Boolean = False
    Dim dsTmp As DataSet = Nothing
    Dim dsPaliTmp As DataSet = Nothing
    Try
      dsShared.Tables("TABPAGA").AcceptChanges()

      If dsShared.Tables("TABPAGA").Rows.Count > 0 Then
        dtrTmp = dsShared.Tables("TABPAGA").Select("tb_codpaga = " & strCodOld)
        If dtrTmp.Length = 0 Then
          Return False
        End If
      Else
        Return False
      End If

      dsShared.Tables("TABPAGA").Rows.Add()
      dsShared.Tables("TABPAGA").Rows(dsShared.Tables("TABPAGA").Rows.Count - 1)("tb_codpaga") = strCodNew

      For i = 0 To dsShared.Tables("TABPAGA").Columns.Count - 1
        strColname = dsShared.Tables("TABPAGA").Columns(i).ColumnName
        If strColname = "tb_codpaga" Or strColname = "ts" Then Continue For

        dsShared.Tables("TABPAGA").Rows(dsShared.Tables("TABPAGA").Rows.Count - 1)(strColname) = dtrTmp(0)(strColname)
      Next

      If Not Salva(False) Then Return False
      dsShared.AcceptChanges()

      If bLing Then
        dReturn = oCldPaga.GetPagalin(NTSCInt(strCodOld), dsTmp)
        If dReturn = False Then Return False

        dsPaliTmp = dsPaliShared
        dsPaliShared = Nothing
        dsPaliShared = dsTmp.Copy

        For i = 0 To dsTmp.Tables("PAGALIN").Rows.Count - 1
          dsPaliShared.Tables("PAGALIN").Rows(i)("tl_codpaga") = strCodNew

          dsPaliShared.Tables("PAGALIN").Rows(i).AcceptChanges()
          dsPaliShared.Tables("PAGALIN").Rows(i).SetAdded()
        Next

        If Not PaliSalva(False) Then Return False

        dsPaliShared = dsPaliTmp
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

#Region "FRM__PALI"
  Public bPaliHasChanges As Boolean
  Public dsPaliShared As DataSet
  Public lCodPaga As Integer = 0
  Dim strPaliPrevCelValue As String = ""

  Public Overridable Function PaliApri(ByRef dsPali As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      dReturn = oCldPaga.GetPagalin(lCodPaga, dsPali)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldPaga.SetTableDefaultValueFromDB("PAGALIN", dsPali)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      PaliSetDefaultValue(dsPali)
      dsPaliShared = dsPali

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsPaliShared.Tables("PAGALIN").ColumnChanging, AddressOf PaliBeforeColUpdate
      AddHandler dsPaliShared.Tables("PAGALIN").ColumnChanged, AddressOf PaliAfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsPaliShared.Tables("PAGALIN").AcceptChanges()
      bPaliHasChanges = False

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

  Public Overridable Sub PaliSetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("PAGALIN").Columns("tl_codpaga").DefaultValue = lCodPaga
      ds.Tables("PAGALIN").Columns("tl_codling").DefaultValue = 0
      ds.Tables("PAGALIN").Columns("tl_despaga").DefaultValue = ""

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


  Public Overridable Sub PaliNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsPaliShared.Tables("PAGALIN").Rows.Add(dsPaliShared.Tables("PAGALIN").NewRow)
      bPaliHasChanges = True
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


  Public Overridable Function PaliRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsPaliShared.Tables("PAGALIN").Select(strFilter)(nRow).RejectChanges()
      bPaliHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function PaliSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not PaliTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldPaga.ScriviTabellaSemplice(strDittaCorrente, "PAGALIN", dsPaliShared.Tables("PAGALIN"), "", "", "")
      If bResult Then
        bPaliHasChanges = False
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

  Public ReadOnly Property PaliRecordIsChanged() As Boolean
    Get
      Return bPaliHasChanges
    End Get
  End Property


  Public Overridable Sub PaliBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPaliPrevCelValue = e.Column.ColumnName.ToUpper + ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "PaliBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub PaliBeforeColUpdate_tl_codling(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""

    Try
      If dsPaliShared.Tables("PAGALIN").Rows.Count > 1 Then
        dtrTmp = dsPaliShared.Tables("PAGALIN").Select("tl_codling = " & e.ProposedValue.ToString())
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128399392549558000, "Codice lingua già esistente: inserire un nuovo codice")))
          Return
        End If
      End If

      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codling = ""
      Else
        If Not ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABLING", "N", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128399392980742000, "Codice lingua non corretto")))
        Else
          e.Row!xx_codling = strTmp
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


  Public Overridable Sub PaliAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPaliPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPaliPrevCelValue = strPaliPrevCelValue.Remove(strPaliPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bPaliHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "PaliAfterColUpdate_" & e.Column.ColumnName.ToLower
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


  Public Overridable Function PaliTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsPaliShared.Tables("PAGALIN").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!tl_codling) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128913471483429440, "Il campo lingua deve contenere un valore diverso da 0")))
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

#End Region

End Class
