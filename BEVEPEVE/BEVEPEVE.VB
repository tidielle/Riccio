Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEVEPEVE
  Inherits CLE__BASN

  Public oCldPeve As CLDVEPEVE 'oggetto dal
  Public dsShared As DataSet
  Public bHasChanges As Boolean = False

  Public strActLog As String = ""
  Public strActLogProg As String = ""
  Public strActLogNomOggLog As String = ""
  Public strActLogDesLog As String = ""
  Public strAzCodpcon As String = ""

  Public strOut As String = ""
  Public dttTmp As DataTable = New DataTable
  Public dttAnaz As New DataTable

  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE + CLN__STD.bsModExtCRM
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDVEPEVE"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldPeve = CType(MyBase.ocldBase, CLDVEPEVE)
    oCldPeve.Init(oApp)
    Return True
  End Function

  Public Overridable Function File_Apri(ByRef strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      oCldPeve.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttAnaz)
      strAzCodpcon = NTSCStr(dttAnaz.Rows(0)!tb_azcodpcon).Trim

      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      dReturn = oCldPeve.GetData(strDitta, ds)
      If dReturn = False Then Return False
      '--------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldPeve.SetTableDefaultValueFromDB("TABPEVE", ds)
      '--------------------------------------
      SetDefaultValue(ds)
      '--------------------------------------
      dsShared = ds
      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("TABPEVE").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TABPEVE").ColumnChanged, AddressOf AfterColUpdate
      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldPeve.GetSettingBus("BSVEPEVE", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      '--------------------------------------
      bHasChanges = False
      '--------------------------------------
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
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSVEPEVE"
        strActLogNomOggLog = "tabpeve"
        strActLogDesLog = oApp.Tr(Me, 128058229647869287, "Personalizzazione Vendite")
      End If
      '----------------------------------------
      bResult = oCldPeve.ScriviTabellaSemplice("", "TABPEVE", dsShared.Tables("TABPEVE"), strActLogProg, strActLogNomOggLog, strActLogDesLog)
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
  Public Overridable Function RecordRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("TABPEVE").Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Sub RecordNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsShared.Tables("TABPEVE").Rows.Add(dsShared.Tables("TABPEVE").NewRow)
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
  Public Overridable Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      Try
        ds.Tables("TABPEVE").Columns("tb_codpeve").DefaultValue = 1
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
  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
  Public Overridable Function TestPreSalva() As Boolean
    Try
      '-------------------------------------------------------------------------------------
      '--- Eseguo i controlli necessari prima di fare il salvataggio della riga
      '-------------------------------------------------------------------------------------
      Dim dtrTmp As DataRow() = dsShared.Tables("TABPEVE").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      Dim i As Integer = 0
      For i = 0 To dtrTmp.Length - 1
        If NTSCDate(dtrTmp(i)!tb_datfinivainc3) > NTSCDate(dtrTmp(i)!tb_datfinivainc2) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129636653075800781, "La data di fine validità Cod. IVA spese 3° scaglione deve essere maggiore della data di fine validità Cod. IVA spese 3° scaglione")))
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
  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property

#Region "Validazione campi"
  '-----------------------------------------------------------------------------------------
  '--- Primo Tab
  '-----------------------------------------------------------------------------------------
  Public Overridable Sub BeforeColUpdate_tb_cauemft(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_cauemft = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989443983738420, "Causale emisione Fatture: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_tipmov) = "P" Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130655293472654273, "Causale emissione Fatture: la causale non può essere di tipo 'Split payment'. Questa causale può essere impostata solo in 'Tipi bolle/fatture'")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048385485068303, "Causale emissione Fatture:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_cauemft = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_cauemfs(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_cauemfs = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989443549659252, "Causale emisione Fatture sospensione: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
               NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048388216429119, "Causale emisione Fatture sospensione:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_cauemfs = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caunoac(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caunoac = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989443618568148, "Causale Note di accredito: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_tipmov) = "P" Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130655294397330155, "Causale Note di accredito: la causale non può essere di tipo 'Split payment'. Questa causale può essere impostata solo in 'Tipi bolle/fatture'")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048389180085561, "Causale Note di accredito:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caunoac = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caunoaf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caunoaf = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447465306769, "Causale Note di accredito sospensione: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048391946621310, "Causale Note di accredito sospensione:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caunoaf = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_cauinc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_cauinc = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447411709932, "Causale Incasso: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048393273651458, "Causale Incasso:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_cauinc = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caucorr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caucorr = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447334049209, "Causale emissione Corrispettivi: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048394317689602, "Causale emissione Corrispettivi:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caucorr = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caustom(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caustom = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447282014962, "Causale storno Omaggi: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048395274662297, "Causale storno Omaggi:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caustom = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_iva15(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_iva15 = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCIVA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447231387046, "Codice Iva escluso articolo 15: inserire un codice valido")))
          Return
        Else
          e.Row!xx_iva15 = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_ivainc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_ivainc = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCIVA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447192478555, "Codice Iva spese incasso: inserire un codice valido")))
          Return
        Else
          e.Row!xx_ivainc = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_ivainc2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_ivainc2 = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCIVA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129636651981328125, "Codice Iva spese incasso 2: inserire un codice valido")))
          Return
        Else
          e.Row!xx_ivainc2 = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_ivainc3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_ivainc3 = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCIVA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129636652199726562, "Codice Iva spese incasso 3: inserire un codice valido")))
          Return
        Else
          e.Row!xx_ivainc3 = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caustib(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caustib = ""
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129205481613232422, "Causale storno incassi su DDT emessi: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048396033887057, "Causale storno incassi su DDT emessi:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caustib = strOut
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
  '-----------------------------------------------------------------------------------------
  '--- Secondo Tab
  '-----------------------------------------------------------------------------------------
  Public Overridable Sub BeforeColUpdate_tb_concas(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_concas = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447885487220, "Conto contropartita Cassa: inserire un codice valido")))
          Return
        Else
          e.Row!xx_concas = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_conabat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_conabat = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448664438335, "Conto contropartita Abbuoni attivi: inserire un codice valido")))
          Return
        Else
          e.Row!xx_conabat = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_conabpa(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_conabpa = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447930489812, "Conto contropartita Abbuoni passivi: inserire un codice valido")))
          Return
        Else
          e.Row!xx_conabpa = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_conomag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_conomag = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448015807226, "Conto contropartita Omaggi (costo): inserire un codice valido")))
          Return
        Else
          e.Row!xx_conomag = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_conribo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_conribo = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448063309962, "Conto contropartita Ricavi da bolli: inserire un codice valido")))
          Return
        Else
          e.Row!xx_conribo = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_conriin(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_conriin = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448100968381, "Conto contropartita Ricavi spese incasso: inserire un codice valido")))
          Return
        Else
          e.Row!xx_conriin = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_conriac(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_conriac = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448151127520, "Conto contropartita Ricavi spese accessorie: inserire un codice valido")))
          Return
        Else
          e.Row!xx_conriac = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_concauz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_concauz = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448191442342, "Conto contropartita Cauzioni vuoti: inserire un codice valido")))
          Return
        Else
          e.Row!xx_concauz = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_conclpriv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_conclpriv = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut, dttTmp) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448327387672, "Codice conto Cliente privato: inserire un codice valido")))
          Return
        Else
          If dttTmp.Rows(0)!an_tipo.ToString <> "C" Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448354264220, "Codice conto Cliente privato: il conto deve far parte dei Clienti")))
            Return
          Else
            If dttTmp.Rows(0)!an_partite.ToString = "S" Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448394266524, "Codice conto Cliente privato: il conto non può essere gestito a partite")))
              Return
            Else
              If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
                e.ProposedValue = e.Row(e.Column.ColumnName)
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448423486957, "Codice conto Cliente privato: il conto non può essere gestito a scadenze")))
                Return
              Else
                e.Row!xx_conclpriv = strOut
              End If
            End If
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
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_contimba(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_contimba = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448471302211, "Conto contropartita Spese imballi: inserire un codice valido")))
          Return
        Else
          e.Row!xx_contimba = strOut
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
  '-----------------------------------------------------------------------------------------
  '--- Terzo Tab
  '-----------------------------------------------------------------------------------------
  Public Overridable Sub BeforeColUpdate_tb_vtipobf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_vtipobf = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447139663013, "Tipo bolla/fattura generico per testata bolla: inserire un codice valido")))
          Return
        Else
          e.Row!xx_vtipobf = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_vcontro(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_vcontro = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOVE", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447074971787, "Codice contropartita testata bolla: inserire un codice valido")))
          Return
        Else
          e.Row!xx_vcontro = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_vmagazz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_vmagazz = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAGA", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445656927668, "Codice magazzino testata bolla: inserire un codice valido")))
          Return
        Else
          e.Row!xx_vmagazz = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_vcaumag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_vcaumag = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUM", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445618332436, "Codice causale magazzino testata bolla: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048398039204761, "Codice causale magazzino testata bolla:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_vcaumag = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_vcautra(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_vcautra = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUM", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445565830420, "Codice causale trasporto piede bolla: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048398849610361, "Codice causale trasporto piede bolla:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_vcautra = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tpbfrescli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tpbfrescli = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445485671092, "Tipo bolla/fattura resi clienti: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tpbfrescli = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tpbfresfor(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tpbfresfor = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445448013396, "Tipo bolla/fattura resi fornitori: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tpbfresfor = strOut
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
  '-----------------------------------------------------------------------------------------
  '--- Quarto Tab
  '-----------------------------------------------------------------------------------------
  Public Overridable Sub BeforeColUpdate_tb_concrrfc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_concrrfc = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut, dttTmp) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448515054731, "Sottoconto crediti da R.F. non incassata (cessioni): inserire un codice valido")))
          Return
        Else
          If dttTmp.Rows(0)!an_tipo.ToString <> "S" Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448554744517, "Sottoconto crediti da R.F. non incassata (cessioni): il conto deve far parte dei Sottoconti")))
            Return
          Else
            e.Row!xx_concrrfc = strOut
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
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_concrrfp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_concrrfp = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut, dttTmp) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448594121785, "Sottoconto crediti da R.F. non incassata (prestazioni): inserire un codice valido")))
          Return
        Else
          If dttTmp.Rows(0)!an_tipo.ToString <> "S" Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989448631780204, "Sottoconto crediti da R.F. non incassata (prestazioni): il conto deve far parte dei Sottoconti")))
            Return
          Else
            e.Row!xx_concrrfp = strOut
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
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_conivrfp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCLng(e.ProposedValue) = 0 Then
        e.Row!xx_conivrfp = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strOut, dttTmp) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447790481748, "Sottoconto Iva da R.F. non incassata (prestazioni): inserire un codice valido")))
          Return
        Else
          If dttTmp.Rows(0)!an_tipo.ToString <> "S" Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447704226780, "Sottoconto Iva da R.F. non incassata (prestazioni): il conto deve far parte dei Sottoconti")))
            Return
          Else
            e.Row!xx_conivrfp = strOut
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
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_caurfincc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caurfincc = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445395980148, "Causale R.F. totale incassata (cessioni): inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048399837949402, "Codice R.F. totale incassata (cessioni):" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caurfincc = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caurfparc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caurfparc = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445251130836, "Causale R.F. parzialmente incassata (cessioni): inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048400753230029, "Codice R.F. parzialmente incassata (cessioni):" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caurfparc = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caurfnonc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caurfnonc = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445210973044, "Causale R.F. non incassata (cessioni): inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048401651154477, "Codice R.F. non incassata (cessioni):" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caurfnonc = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caurfriec(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caurfriec = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445166596340, "Causale R.F. riemissione (cessioni): inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048402481754852, "Codice R.F. riemissione (cessioni):" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caurfriec = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caurfincp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caurfincp = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445126907316, "Causale R.F. totale incassata (prestazioni): inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048403414919624, "Codice R.F. totale incassata (prestazioni):" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caurfincp = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caurfparp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caurfparp = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445055654580, "Causale R.F. parzialmente incassata (prestazioni): inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048404409649196, "Codice R.F. parzialmente incassata (prestazioni):" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caurfparp = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caurfnonp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caurfnonp = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989445019403188, "Causale R.F. non incassata (prestazioni): inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048405174478256, "Codice R.F. non incassata (prestazioni):" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caurfnonp = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caurfriep(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caurfriep = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444949087988, "Causale R.F. riemissione (prestazioni): inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048405947045115, "Codice R.F. riemissione (prestazioni):" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caurfriep = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_cauemftat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_cauemftat = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444901742420, "Causale emissione fattura autotrasportatore: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048406807879664, "Causale emissione fattura autotrasportatore:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_cauemftat = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_caunoacat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_caunoacat = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCAUC", "N", strOut, dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444763299604, "Causale emissione nota di accredito autotrasportatore: inserire un codice valido")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> "" And _
         NTSCStr(dttTmp.Rows(0)!tb_codpcon).Trim <> strAzCodpcon Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131048407587076365, "Causale emissione nota di accredito autotrasportatore:" & vbCrLf & _
          "La causale possiede un codice Piano dei Conti diverso da quello della ditta corrente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_caunoacat = strOut
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
  '-----------------------------------------------------------------------------------------
  '--- Quinto Tab
  '-----------------------------------------------------------------------------------------
  Public Overridable Sub BeforeColUpdate_tb_contrcas(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contrcas = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444718922900, "Contropartita Cassa: inserire un codice valido")))
          Return
        Else
          e.Row!xx_contrcas = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_contrabat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contrabat = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444659076852, "Contropartita Abbuoni attivi: inserire un codice valido")))
          Return
        Else
          e.Row!xx_contrabat = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_contrabpa(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contrabpa = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444572823540, "Contropartita Abbuoni passivi: inserire un codice valido")))
          Return
        Else
          e.Row!xx_contrabpa = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_contromag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contromag = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444514383796, "Contropartita Omaggi (costo): inserire un codice valido")))
          Return
        Else
          e.Row!xx_contromag = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_contrribo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contrribo = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447553905622, "Contropartita Ricavi da bolli: inserire un codice valido")))
          Return
        Else
          e.Row!xx_contrribo = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_contrriin(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contrriin = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444392191604, "Contropartita Ricavi spese incasso: inserire un codice valido")))
          Return
        Else
          e.Row!xx_contrriin = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_contrriac(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contrriac = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989447522497563, "Contropartita Ricavi spese accessorie: inserire un codice valido")))
          Return
        Else
          e.Row!xx_contrriac = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_contrcrrfc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contrcrrfc = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444309688436, "Contropartita sottoconto crediti R.F. non incassata (cessioni): inserire un codice valido")))
          Return
        Else
          e.Row!xx_contrcrrfc = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_contrcrrfp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contrcrrfp = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444248592340, "Contropartita sottoconto crediti R.F. non incassata (prestazioni): inserire un codice valido")))
          Return
        Else
          e.Row!xx_contrcrrfp = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_contrivrfp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contrivrfp = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444189058804, "Contropartita sottoconto Iva da R.F. non incassata: inserire un codice valido")))
          Return
        Else
          e.Row!xx_contrivrfp = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_contrcauz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contrcauz = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444147025940, "Contropartita cauzioni vuoti: inserire un codice valido")))
          Return
        Else
          e.Row!xx_contrcauz = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_contrimba(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contrimba = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, "", "TABCOVG", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989444076241972, "Codice spese imballi: inserire un codice valido")))
          Return
        Else
          e.Row!xx_contrimba = strOut
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
  '-----------------------------------------------------------------------------------------
  '--- Sesto Tab
  '-----------------------------------------------------------------------------------------
  Public Overridable Sub BeforeColUpdate_tb_tipobfrka(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrka = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185200827551719, "Tipo bolla/fattura per FATTURE IMMEDIATE EMESSE: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrka = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tipobfrkb(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrkb = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185221451084543, "Tipo bolla/fattura per DDT EMESSI: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrkb = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tipobfrkc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrkc = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185201747250991, "Tipo bolla/fattura per CORRISPETTIVI EMESSI: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrkc = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tipobfrke(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrke = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185202090380383, "Tipo bolla/fattura per NOTE ADDEBITO EMESSE: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrke = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tipobfrkf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrkf = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185202739919947, "Tipo bolla/fattura per RIC. FISC. EMESSE: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrkf = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tipobfrki(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrki = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185203088830663, "Tipo bolla/fattura per RIEM. RIC FISC EMESSE: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrki = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tipobfrkn(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrkn = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185203427428747, "Tipo bolla/fattura per NOTE ACCRED EMESSE: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrkn = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tipobfrks(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrks = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185203841496547, "Tipo bolla/fattura per FATTURE/RIC FISC EMESSE: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrks = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tipobfrkr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrkr = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185204170563259, "Tipo bolla/fattura per IMPEGNI CLIENTI/IMP CLIENTI APERTI: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrkr = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tipobfrkx(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrkx = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185204490723607, "Tipo bolla/fattura per IMPEGNI DI TRASFERIMENTO: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrkx = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tipobfrkq(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrkq = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185205110575291, "Tipo bolla/fattura per PREVENTIVI: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrkq = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_tipobfrko(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_tipobfrko = ""
      Else
        If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strOut) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130185212078352103, "Tipo bolla/fattura per IMPEGNI DI COMMESSA: inserire un codice valido")))
          Return
        Else
          e.Row!xx_tipobfrko = strOut
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
  Public Overridable Sub BeforeColUpdate_tb_vlistin(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_vlistin = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldPeve.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABLIST", "N", strOut) = False Then
        e.Row!xx_vlistin = ""
      Else
        e.Row!xx_vlistin = strOut
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
#End Region

End Class
