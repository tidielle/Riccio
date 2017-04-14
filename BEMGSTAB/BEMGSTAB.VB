Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGSTAB
  Inherits CLE__BASE

  Public oCldStab As CLDMGSTAB
  Public bNuovaAnalitica As Boolean = False

  Public Const DEFAULT_tb_coddivi As String = " "
  Public Const DEFAULT_TB_CODDICA As String = " "
  Public Const DEFAULT_XX_CODDICA As String = ""
  Public Const DEFAULT_TB_CODDICV As String = " "
  Public Const DEFAULT_XX_CODDICV As String = ""
  Public Const DEFAULT_XX_CODTCDC As String = ""

  Public Property IsNuovaAnalitica() As Boolean
    Get
      Return bNuovaAnalitica
    End Get
    Set(ByVal value As Boolean)
      bNuovaAnalitica = value
    End Set
  End Property

  Public Shared ReadOnly Property TIPO_ENTITY() As String
    Get
      Return CLDMGSTAB.TIPO_ENTITY
    End Get
  End Property

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModMG + CLN__STD.bsModVE
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE
  Private ModuliSup_P As Integer = CLN__STD.bsModSupCAE
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
  Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, _
    ByRef oCleLbmenu As Object, ByVal strTabella As String, ByVal bRemoting As Boolean, _
    ByVal strRemoteServer As String, ByVal strRemotePort As String) As Boolean

    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGSTAB"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldStab = CType(MyBase.ocldBase, CLDMGSTAB)
    oCldStab.Init(oApp)
    Return True
  End Function
#End Region


  Public Overridable Sub OnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If dsShared.Tables("TABSTAB").Rows.Count > 1 Then
        dsShared.Tables("TABSTAB").Select(Nothing, "tb_codstab DESC")
        e.Row!tb_codstab = NTSCInt(dsShared.Tables("TABSTAB").Rows(dsShared.Tables("TABSTAB").Rows.Count - 2)!tb_codstab.ToString()) + 1
      Else
        e.Row!tb_codstab = 1
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

  Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      strDittaCorrente = strDitta
      '--------------------------------------------------------------------------------------------------------------
      dReturn = ocldBase.LeggiTabellaSemplice(strDitta, "TABSTAB", ds)
      If dReturn = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      ocldBase.SetTableDefaultValueFromDB("TABSTAB", ds)
      '--------------------------------------------------------------------------------------------------------------
      SetDefaultValue(ds)
      '--------------------------------------------------------------------------------------------------------------
      dsShared = ds
      '--------------------------------------------------------------------------------------------------------------
      AddHandler dsShared.Tables("TABSTAB").ColumnChanging, AddressOf BeforeColUpdate_P
      AddHandler dsShared.Tables("TABSTAB").ColumnChanged, AddressOf AfterColUpdate_P
      '--------------------------------------------------------------------------------------------------------------
      dsShared.Tables("TABSTAB").Columns.Add("xx_coddica", GetType(String))
      dsShared.Tables("TABSTAB").Columns.Add("xx_coddicv", GetType(String))
      dsShared.Tables("TABSTAB").Columns.Add("xx_codtcdc", GetType(String))
      dsShared.Tables("TABSTAB").Columns.Add("xx_contocorrinc", GetType(String))
      dsShared.Tables("TABSTAB").Columns.Add("xx_cliepriv", GetType(String))
      '--------------------------------------------------------------------------------------------------------------
      RiempiColonneUnbound()
      '--------------------------------------------------------------------------------------------------------------
      dsShared.Tables("TABSTAB").AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      bHasChanges = False
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

  Public Overridable Function RiempiColonneUnbound() As Boolean
    Dim i As Integer = 0
    Dim dtrTmp As DataRow
    Dim strTmp As String = ""
    Try
      For i = 0 To dsShared.Tables("TABSTAB").Rows.Count - 1
        dtrTmp = dsShared.Tables("TABSTAB").Rows(i)
        If bNuovaAnalitica Then
          If NTSCStr(dtrTmp!tb_coddica).Trim.Length <> 0 Then
            ocldBase.ValCodiceDb(dtrTmp!tb_coddica.ToString, strDittaCorrente, "TABDICA", "S", strTmp)
            dtrTmp!xx_coddica = strTmp
            If NTSCStr(dtrTmp!tb_coddicv).Trim.Length <> 0 Then
              dtrTmp!xx_coddicv = oCldStab.GetDicvDescr(strDittaCorrente, dtrTmp!tb_coddica.ToString, dtrTmp!tb_coddicv.ToString)
            End If
          End If
          If NTSCInt(dtrTmp!tb_codtcdc) <> 0 Then
            ocldBase.ValCodiceDb(dtrTmp!tb_codtcdc.ToString, strDittaCorrente, "TABTCDC", "N", strTmp)
            dtrTmp!xx_codtcdc = strTmp
          End If
          If NTSCInt(dtrTmp!tb_codtcdc) <> 0 Then
            ocldBase.ValCodiceDb(dtrTmp!tb_codtcdc.ToString, strDittaCorrente, "TABTCDC", "N", strTmp)
            dtrTmp!xx_codtcdc = strTmp
          End If
        End If

        If NTSCInt(dtrTmp!tb_contocorrinc) <> 0 Then
          ocldBase.ValCodiceDb(NTSCStr(dtrTmp!tb_contocorrinc), strDittaCorrente, "ANAGRA", "N", strTmp)
          dtrTmp!xx_contocorrinc = strTmp
        End If

        If NTSCInt(dtrTmp!tb_cliepriv) <> 0 Then
          ocldBase.ValCodiceDb(NTSCStr(dtrTmp!tb_cliepriv), strDittaCorrente, "ANAGRA", "N", strTmp)
          dtrTmp!xx_cliepriv = strTmp
        End If
      Next


      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overloads Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      '--------------------------------------------------------------------------------------------------------------
      strActLog = ocldBase.GetSettingBus("BSMGSTAB", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BSMGSTAB"
      strActLogNomOggLog = "tabstab"
      strActLogDesLog = oApp.Tr(Me, 128058229647869287, "Dati relativi allo stabilimento")
      '--------------------------------------------------------------------------------------------------------------
      ds.Tables("TABSTAB").Columns("tb_coddica").DefaultValue = DEFAULT_TB_CODDICA
      ds.Tables("TABSTAB").Columns("tb_coddicv").DefaultValue = DEFAULT_TB_CODDICV
      ds.Tables("TABSTAB").Columns("tb_codtcdc").DefaultValue = 0
      ds.Tables("TABSTAB").Columns("tb_tipostab").DefaultValue = TIPO_ENTITY
      '--------------------------------------------------------------------------------------------------------------
      ds.Tables("TABSTAB").Columns("codditt").DefaultValue = strDittaCorrente
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_P(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If

      'lancio la routine standard 
      BeforeColUpdate(sender, e)

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
      '--------------------------------------------------------------------------------------------------------------
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If

      If e.Column.ColumnName <> "tb_codstab" Then
        If NTSCInt(e.Row!tb_codstab) = 0 Then OnAddNewRow(sender, e)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '--------------------------------------------------------------------------------------------------------------
      Dim strFunction As String = "BeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub BeforeColUpdate_tb_codstab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Dim intCodiceDaSalvare As Integer = 0
    Try
      intCodiceDaSalvare = NTSCInt(e.ProposedValue)
      If intCodiceDaSalvare <> 0 Then
        'verifico se il codice è univoco
        dtrTmp = dsShared.Tables("TABSTAB").Select("tb_codstab = " & intCodiceDaSalvare, Nothing, DataViewRowState.OriginalRows)
        If dtrTmp.Length > 0 Then
          e.ProposedValue = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222086718750, "Il codice inserito è già stato utilizzato. Inserire un codice non utilizzato.")))
        End If
       End If
      '-------------------------------------------------

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

  Public Overridable Sub BeforeColUpdate_tb_contocorrinc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xx_contocorrinc = "" : Return

      If oCldStab.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
        If NTSCStr(dttTmp.Rows(0)!an_tipo) = "F" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129955466958682162, "Il conto indicato non deve essere un fornitore")))
          e.ProposedValue = e.Row!tb_contocorrinc
          Return
        End If

        If NTSCStr(dttTmp.Rows(0)!an_partite) = "S" Or NTSCStr(dttTmp.Rows(0)!an_scaden) = "S" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129955466949462055, "Il conto non deve essere gestito a partite o scadenze.")))
          e.ProposedValue = e.Row!tb_contocorrinc
          Return
        End If

        e.Row!xx_contocorrinc = dttTmp.Rows(0)!an_descr1
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129955466936960215, "Cliente/Sottoconto non valido.")))
        e.ProposedValue = e.Row!tb_contocorrinc
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

  Public Overridable Sub BeforeColUpdate_tb_cliepriv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xx_cliepriv = "" : Return

      If oCldStab.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
        If NTSCStr(dttTmp.Rows(0)!an_tipo) <> "C" Or NTSCStr(dttTmp.Rows(0)!an_privato) <> "S" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129400725530781250, "Nell'anagrafica del cliente selezionato è impostato come tipo cliente 'Azienda'." & vbCrLf & _
                                                         "I Clienti Privati Generici possono essere solo di tipo 'Privato'.")))
          e.ProposedValue = e.Row!tb_cliepriv
          Return
        End If

        e.Row!xx_cliepriv = dttTmp.Rows(0)!an_descr1
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129592760237252307, "Cliente non valido.")))
        e.ProposedValue = e.Row!tb_cliepriv
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

  Public Overridable Sub BeforeColUpdate_tb_coddica(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErrore As String = ""
    Dim strCodiceDaSalvare As String = ""
    Dim dttTmp As New DataTable
    Try
      strCodiceDaSalvare = e.ProposedValue.ToString.Trim.ToUpper
      If strCodiceDaSalvare.Length <> 0 Then
        '-------------------------------------------------
        'verifico se il codice esiste in tabdica e si applica ai stabilimenti
        If ocldBase.ValCodiceDb(strCodiceDaSalvare, strDittaCorrente, "TABDICA", "S", "", dttTmp) = True AndAlso _
        NTSCStr(dttTmp.Rows(0)!tb_applicaa).ToUpper = TIPO_ENTITY Then
          If NTSCInt(dttTmp.Rows(0)!tb_liv) <> 1 Then
            e.ProposedValue = strCodiceDaSalvare
            e.Row!xx_coddica = dttTmp.Rows(0)!tb_desdica
            ImpostaDefaultDicv(e)
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319659477373639, "Il codice di aggregazione budget non può essere di primo livello.")))
          End If
        Else
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129140114647207715, "Codice aggregazione budget non è corretto.")))
        End If
      Else
        e.ProposedValue = DEFAULT_TB_CODDICA
        e.Row!xx_coddica = DEFAULT_XX_CODDICA
        ImpostaDefaultDicv(e)
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

  Public Overridable Sub ImpostaDefaultDicv(ByVal e As DataColumnChangeEventArgs)
    e.Row!tb_coddicv = DEFAULT_TB_CODDICV
    e.Row!xx_coddicv = DEFAULT_XX_CODDICV
  End Sub

  Public Overridable Sub BeforeColUpdate_tb_coddicv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErrore As String = ""
    Dim strCodiceDaSalvare As String = ""
    Dim strDescr As String = ""
    Dim dttTmp As New DataTable
    Try
      strCodiceDaSalvare = e.ProposedValue.ToString.Trim.ToUpper
      If strCodiceDaSalvare.Length <> 0 Then
        '-------------------------------------------------
        'verifico se il codice esiste in tabdicv
        strDescr = oCldStab.GetDicvDescr(strDittaCorrente, NTSCStr(e.Row!tb_coddica), strCodiceDaSalvare)
        If strDescr.Length = 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129140114545485710, "Valore dimensione C.A. non corretto.")))
        Else
          e.ProposedValue = strCodiceDaSalvare
          e.Row!xx_coddicv = strDescr
        End If
      Else
        e.ProposedValue = DEFAULT_TB_CODDICV
        e.Row!xx_coddicv = DEFAULT_XX_CODDICV
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

  Public Overridable Sub BeforeColUpdate_tb_codtcdc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim intCodiceDaSalvare As Integer = 0
    Dim strDescr As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      intCodiceDaSalvare = NTSCInt(e.ProposedValue)

      If intCodiceDaSalvare <> 0 Then
        '-------------------------------------------------
        'verifico se il codice esiste in tabtcdc
        strDescr = oCldStab.GetCtdcDescr(intCodiceDaSalvare)
        If strDescr.Length = 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129140739917683140, "Tipologia centro di costo non corretta.")))
        Else
          e.Row!xx_codtcdc = strDescr
        End If
      Else
        e.Row!xx_codtcdc = DEFAULT_XX_CODTCDC
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

  Public Overridable Sub BeforeColUpdate_tb_seriedef(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), False)
      If strTmp <> NTSCStr(e.ProposedValue) Then e.ProposedValue = strTmp

      If Not oCldStab.CheckSerieBloccata(strDittaCorrente, NTSCInt(e.Row!tb_codstab)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129881297371554154, "Il negozio è configurato per la modalità disconessa. La serie non può essere cambiata.")))
        e.ProposedValue = e.Row!tb_seriedef
        Return
      End If

      If Not oCldStab.CheckSerieInUsoOffline(strDittaCorrente, NTSCStr(e.ProposedValue)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129881300434559050, "La serie indicata è già in uso da un altro negozio configurato per la modalità disconnessa.")))
        e.ProposedValue = e.Row!tb_seriedef
        Return
      End If
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub AfterColUpdate_P(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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

      'lancio la routine standard
      AfterColUpdate(sender, e)

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

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      bHasChanges = True
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
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim bEsito As Boolean = True
    Dim strCodiceStabilimento As String = oApp.Tr(Me, 129140663094216172, "codice stabilimento")
    Dim strCodiceDimensione As String = oApp.Tr(Me, 129151886687444804, "codice dimensione C.A.")
    Dim strValoreDimensione As String = oApp.Tr(Me, 129151887845732879, "valore dimensione C.A.")
    Dim strCodiceCentro As String = oApp.Tr(Me, 129151889041521782, "codice tipologia entità")

    Try
      dtrTmp = dsShared.Tables("TABSTAB").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCStr(dtrTmp(i)!tb_desstab).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129618425703650398, "Indicare la descrizione dello stabilimento")))
        End If
      Next

      If bNuovaAnalitica Then
        For i = 0 To dtrTmp.Length - 1
          TestPreSalvaCodice(CInt(dtrTmp(i)!tb_codstab), strCodiceStabilimento)
          If CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupBUD) Then
            TestPreSalvaCodice(dtrTmp(i)!tb_coddica.ToString, strCodiceDimensione)
            TestPreSalvaCodice(dtrTmp(i)!tb_coddicv.ToString, strValoreDimensione)
          End If
          TestPreSalvaCodice(CInt(dtrTmp(i)!tb_codtcdc), strCodiceCentro)
        Next
      Else
        For i = 0 To dtrTmp.Length - 1
          TestPreSalvaCodice(CInt(dtrTmp(i)!tb_codstab), strCodiceStabilimento)
        Next
      End If
    Catch ex As NTSException
      ThrowRemoteEvent(New NTSEventArgs("", ex.Message))
      bEsito = False
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
        bEsito = False
      End If
    End Try
    Return bEsito
  End Function

  Public Overridable Sub TestPreSalvaCodice(ByVal codice As String, ByVal strNomeCodice As String)
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      If NTSCStr(codice.Trim) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129151935550320747, "Inserire un |" & strNomeCodice & "| valido.")))
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
  Public Overridable Sub TestPreSalvaCodice(ByVal codice As Integer, ByVal strNomeCodice As String)
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      If codice = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129152721455765751, "Inserire un |" & strNomeCodice & "| diverso da 0.")))
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

End Class
