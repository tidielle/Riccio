Imports System.Data
Imports NTSInformatica.CLN__STD


Public Class CLE__CIVA
  Inherits CLE__BASE

  Public oCldCiva As CLD__CIVA  'oggetto DAL
  
  Public bSpesometro As Boolean = False

  Public bAllowOperation As Boolean = False
  Public nMinCodiva As Integer = 0
  Public nMaxCodiva As Integer = 0

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
    SetDbVersionNeeded(21, 6)
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__CIVA"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldCiva = CType(MyBase.ocldBase, CLD__CIVA)
    oCldCiva.Init(oApp)

    If oApp.User.Nome.ToUpper = "NTS" And oApp.User.Pwd.ToUpper = "NTS" Then
      bAllowOperation = True
    Else
      bAllowOperation = False
    End If

    LeggiRegistro()
    Return True
  End Function

  Public Overridable Sub LeggiRegistro()
    Try

      'Leggo le Opzioni di Registro globali
      nMinCodiva = NTSCInt(oCldCiva.GetSettingBus("Bs--civa", "Opzioni", ".", "MinCodiva", "1000", " ", "1000"))
      nMaxCodiva = NTSCInt(oCldCiva.GetSettingBus("Bs--civa", "Opzioni", ".", "MaxCodiva", "5000", " ", "5000"))
      
      
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

  Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As System.Data.DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'Tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldCiva.LeggiTabCiva(strDittaCorrente, ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'Imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldCiva.SetTableDefaultValueFromDB(strNomeTabella, ds)

      SetDefaultValue(ds)

      dsShared = ds

      '--------------------------------------
      'Creo gli eventi per la gestione del Datatable dentro l'Entity
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

  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      
      'Imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("TABCIVA").Columns("tb_desciva").DefaultValue = " "
      'ds.Tables("TABCIVA").Columns("xx_codtpel").DefaultValue = ""
      ds.Tables("TABCIVA").Columns("xx_tipacq").DefaultValue = "Non interessa"
      ds.Tables("TABCIVA").Columns("tb_rilanacq").DefaultValue = " "

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

  Public Overridable Sub BeforeColUpdate_tb_codciva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow = Nothing
    Dim strErrore As String = ""

    Try

      If bAllowOperation = False And NTSCInt(e.ProposedValue.ToString()) >= nMinCodiva And NTSCInt(e.ProposedValue.ToString()) <= nMaxCodiva Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182575911250000, "Codice IVA non valido: è consentito l'inserimento solo di codici" & vbCrLf & "NON compresi fra |" & nMinCodiva & "| e |" & nMaxCodiva & "|.")))
        Return
      End If

      If dsShared.Tables("TABCIVA").Rows.Count > 1 Then
        dtrTmp = dsShared.Tables("TABCIVA").Select("tb_codciva = " & e.ProposedValue.ToString())
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182575973437500, "Codice IVA già esistente: inserire un nuovo Codice IVA.")))
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

  'Public Overridable Sub BeforeColUpdate_tb_desciva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
  '  Try

  '    If e.ProposedValue.ToString().Trim = "" Then
  '      e.ProposedValue = e.Row(e.Column.ColumnName)
  '      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128171551470156250, "Descr. IVA obbligatoria.")))
  '      Return
  '    End If

  '  Catch ex As Exception
  '    '--------------------------------------------------------------
  '    If GestErrorCallThrow() Then
  '      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
  '    Else
  '      ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
  '    End If
  '    '--------------------------------------------------------------    
  '  End Try
  'End Sub 

  Public Overridable Sub BeforeColUpdate_tb_tipacq(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If e.ProposedValue.ToString.Trim = "" Then
        e.ProposedValue = " "
      Else
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If
      
      bOk = oCldCiva.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTACQ", "S", strTmp)
      If bOk = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182576594687500, "Tipo operazione IVA inesistente.")))
        Return
      Else
        e.Row!xx_tipacq = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_tipel(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!tb_codtpel = 0
      e.Row!xx_codtpel = ""
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

  Public Overridable Sub BeforeColUpdate_tb_codtpel(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codtpel = ""
      Else
        bOk = oCldCiva.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSMEL", "N", strTmp, Nothing, e.Row!tb_tipel.ToString)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182576654218750, "N° elenco inesistente.")))
          Return
        Else
          e.Row!xx_codtpel = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_mvcodiva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_mvcodiva = ""
      Else
        bOk = oCldCiva.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "CIVA", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182576702500000, "Cod. IVA per saldo aliquota inesistente.")))
          Return
        Else
          e.Row!xx_mvcodiva = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_codindetr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codindetr = ""
      Else
        bOk = oCldCiva.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "CIVA", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182576758593750, "Cod. IVA indetraibile corrispondente inesistente.")))
          Return
        Else
          e.Row!xx_codindetr = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_coddetr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_coddetr = ""
      Else
        bOk = oCldCiva.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "CIVA", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182576805156250, "Cod. IVA detraibile corrispondente inesistente.")))
          Return
        Else
          e.Row!xx_coddetr = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_codagr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codagr = ""
      Else
        bOk = oCldCiva.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "CIVA", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182576845156250, "Cod. IVA agricola corrispondente inesistente.")))
          Return
        Else
          e.Row!xx_codagr = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_codteor(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codteor = ""
      Else
        bOk = oCldCiva.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "CIVA", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182576905625000, "Cod. IVA per calcolo teorico inesistente.")))
          Return
        Else
          e.Row!xx_codteor = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_reparto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strDescr As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xx_reparto = "" : Return

      oCldCiva.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABREAR", "N", strDescr)
      e.Row!xx_reparto = strDescr
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
    'Eseguo i controlli necessari prima di fare il salvataggio della riga
    Dim dtrTmp As DataRow() = dsShared.Tables("TABCIVA").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
    Dim i As Integer = 0
    Dim bOk As Boolean = False

    Try
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!tb_codciva) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182577065937500, "Il Codice IVA deve essere un valore compreso tra 1 e 9999.")))
          Return False
        End If

        If bAllowOperation = False And NTSCInt(dtrTmp(i)!tb_codciva) >= nMinCodiva And NTSCInt(dtrTmp(i)!tb_codciva) <= nMaxCodiva Then
          'posso cambiare solo 'tb_estrdat2' se non sono loggato come 'NTS' - 'NTS'
          For Each oColumn As DataColumn In dsShared.Tables("TABCIVA").Columns
            If dtrTmp(i)(oColumn.ColumnName).Equals(dtrTmp(i)(oColumn.ColumnName, DataRowVersion.Original)) = False Then
              If oColumn.ColumnName.ToLower <> "tb_rilanacq" AndAlso _
                 oColumn.ColumnName.ToLower <> "tb_estrdat1" AndAlso _
                 oColumn.ColumnName.ToLower <> "tb_estrdat2" AndAlso _
                 oColumn.ColumnName.ToLower <> "tb_reparto" AndAlso _
                 Not oColumn.ColumnName.StartsWith("xx_") Then bOk = True : Exit For
            End If
          Next

          If bOk Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128981061616657168, "Codice IVA non inseribile/modificabile: l'operazione è consentita solo per codici" & vbCrLf & _
                                              "NON compresi fra |" & nMinCodiva & "| e |" & nMaxCodiva & "| " & _
                                              "(nei codici di range riservato è possibile cambiare solo la colonna 'Tipo Operazione', 'Elenchi IVA', 'Sigla per estrazione dati (1)', 'Sigla per estrazione dati (2)', 'Reparto scontrino').")))
            Return False
          End If

        End If

        If NTSCStr(dtrTmp(i)!tb_desciva).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128182577117500000, "Descrizione IVA obbligatoria.")))
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
        bResult = oCldCiva.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella), "", "", "")
      Else
        bResult = oCldCiva.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella), _
                  strActLogProg, strActLogNomOggLog, strActLogDesLog)
      End If

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

  Public Overridable Function TestPreCancella(ByRef dtrOld As DataRow) As Boolean
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If bAllowOperation = False And _
        NTSCInt(dtrOld!tb_codciva) >= nMinCodiva And NTSCInt(dtrOld!tb_codciva) <= nMaxCodiva Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128981065345811035, "Attenzione!" & vbCrLf & _
          "Il Codice IVA non cancellabile!" & vbCrLf & _
          "E' consentita la cancellazione solo di codici" & vbCrLf & _
          "NON compresi fra |" & nMinCodiva & "| e |" & nMaxCodiva & "|.")))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldCiva.TestPreCancella(strDittaCorrente, NTSCInt(dtrOld!tb_codciva), strMsg) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130931098895495651, "Attenzione!" & vbCrLf & _
          "Il Codice IVA non cancellabile, perché presente in:" & vbCrLf & _
          oApp.Tr(Me, 130931099332850036, strMsg))))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function

  Public Overridable Function CheckCod(ByVal strCod As String) As Boolean
    Try
      If strCod = "0" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130722814774277525, "Codice iva obbligatorio.")))
        Return False
      End If

      If oCldCiva.ValCodiceDb(strCod, strDittaCorrente, "TABCIVA", "N") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130722814796309339, "Codice iva '|" & strCod & "|' già esistente.")))
        Return False
      End If

      If bAllowOperation = False And NTSCInt(strCod) >= nMinCodiva And NTSCInt(strCod) <= nMaxCodiva Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130723689905004733, "Codice IVA non inseribile: l'operazione è consentita solo per codici" & vbCrLf & "NON compresi fra |" & nMinCodiva & "| e |" & nMaxCodiva & "|.")))
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
  Public Overridable Function Duplica(ByVal strCodNew As String, ByVal strCodOld As String) As Boolean
    Dim i As Integer
    Dim strColname As String = ""
    Dim dtrTmp() As DataRow = Nothing
    Try
      dsShared.Tables("TABCIVA").AcceptChanges()

      If dsShared.Tables("TABCIVA").Rows.Count > 0 Then
        dtrTmp = dsShared.Tables("TABCIVA").Select("tb_codciva = " & strCodOld)
        If dtrTmp.Length = 0 Then
          Return False
        End If
      Else
        Return False
      End If

      dsShared.Tables("TABCIVA").Rows.Add()
      dsShared.Tables("TABCIVA").Rows(dsShared.Tables("TABCIVA").Rows.Count - 1)("tb_codciva") = strCodNew

      For i = 0 To dsShared.Tables("TABCIVA").Columns.Count - 1
        strColname = dsShared.Tables("TABCIVA").Columns(i).ColumnName
        If strColname = "tb_codciva" Or strColname = "ts" Then Continue For

        dsShared.Tables("TABCIVA").Rows(dsShared.Tables("TABCIVA").Rows.Count - 1)(strColname) = dtrTmp(0)(strColname)
      Next

      If Not Salva(False) Then Return False
      dsShared.AcceptChanges()

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
