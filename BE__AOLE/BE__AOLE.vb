Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp

Public Class CLE__AOLE
  Inherits CLE__BASE

  Public oCldAole As CLD__AOLE  'oggetto DAL
  Public bAllowOperation As Boolean = False
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

  Public strTipoOgg As String = ""
  Public lProgr As Integer = 0
  Public strCod As String = ""
  Public lcod As Integer = 0
  Public strMatr As String = ""
  Public strTipodoc As String = ""
  Public strSerieDoc As String = ""
  Public nAnnoDoc As Integer = 0
  Public lNumdoc As Integer = 0
  Public dRigaDoc As Integer = 0
  Public lCodlead As Integer = 0
  Public lCodoppo As Integer = 0
  Public lCodchia As Integer = 0
  Public lNumcontr As Integer = 0
  Public strQuery As String = "" 'se chiamato da __ROLE filtri ulteriori
  Public dttParam As DataTable 'se chiamato da __ROLE lista filtri e tipoprogr chiamato (NON USATO)
  Public strPrgParent As String = "" 'se chiamato da __ROLE tipoprogr chiamato
  Public strPath As String = ""

  Public bModuloCRM As Boolean = False
  Public bModuloCS As Boolean = False
  Public bIsCRMUser As Boolean = False

  Public bLottoNew As Boolean = False

  Public Overrides Function Init(ByRef App As CLE__APP, _
                            ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                            ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                            ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__AOLE"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldAole = CType(MyBase.ocldBase, CLD__AOLE)
    oCldAole.Init(oApp)

    If oApp.User.Nome.ToUpper = "NTS" And oApp.User.Pwd.ToUpper = "NTS" Then
      bAllowOperation = True
    Else
      bAllowOperation = False
    End If
    Return True
  End Function

  Public Overridable Sub SettaVar(ByVal strCodOgg As String, ByVal strTipoOggetto As String)
    Dim nDimparam As Integer
    Dim strT() As String = Nothing
    Try
      strTipoOgg = strTipoOggetto
      nDimparam = Len(strCodOgg)

      strT = strCodOgg.Split("§"c)

      Select Case strTipoOgg
        Case "A", "D" '"d" da bsdbdiba, "a" da bsmgarti, bsmgartv, bstcartv
          strCod = Trim(Mid(strCodOgg, 1, CLN__STD.CodartMaxLen))
        Case "C"  'da bs--clie
          lcod = NTSCInt(Mid(strCodOgg, 20, 9))
        Case "K"  'da bscicomm
          lcod = NTSCInt(Mid(strCodOgg, 30, 9))
        Case "L"  'da bsmganlo
          strCod = Trim(Mid(strCodOgg, 1, 18))
          lcod = NTSCInt(Mid(strCodOgg, 40, 9))
        Case "N"  'matricole?
          strCod = Trim(Mid(strCodOgg, 1, CLN__STD.CodartMaxLen))
          strMatr = Trim(Mid(strCodOgg, 88, 18))
        Case "M", "O" '"m" da bsveboll e bsvefdin "o" da bsorgsor
          strTipodoc = Mid(strCodOgg, 50, 1)
          nAnnoDoc = CInt(Mid(strCodOgg, 52, 4))
          strSerieDoc = Mid(strCodOgg, 57, 1)
          lNumdoc = NTSCInt(Mid(strCodOgg, 59, 9))
          dRigaDoc = 0
          lcod = NTSCInt(Mid(strCodOgg, 69, 18))
        Case "P"  'da bscgprin
          lcod = NTSCInt(Mid(strCodOgg, 20, 9))
          nAnnoDoc = CInt(Mid(strCodOgg, 52, 4))
          strSerieDoc = Mid(strCodOgg, 57, 1)
          lNumdoc = NTSCInt(Mid(strCodOgg, 59, 9))
        Case "!"  'da bscrglea (griglia offerte), bscrgsof, bscsgser
          strTipodoc = Mid(strCodOgg, 50, 1)
          nAnnoDoc = CInt(Mid(strCodOgg, 52, 4))
          strSerieDoc = Mid(strCodOgg, 57, 1)
          lNumdoc = NTSCInt(Mid(strCodOgg, 59, 9))
          dRigaDoc = NTSCInt(Mid(strCodOgg, 40, 9))
          lcod = NTSCInt(Mid(strCodOgg, 20, 9))
          lCodlead = NTSCInt(Mid(strCodOgg, 30, 9))
        Case "R"  'da bscrcrac, bscrlead, bscsgser, bscslead
          lCodlead = NTSCInt(Mid(strCodOgg, 30, 9))
        Case "J"  'da bscroppo
          lcod = NTSCInt(Mid(strCodOgg, 20, 9))
          lCodlead = NTSCInt(Mid(strCodOgg, 30, 9))
          lCodoppo = NTSCInt(Mid(strCodOgg, 114, 9))
        Case "Y"  'da bscsgchi, bscsgser
          lcod = NTSCInt(Mid(strCodOgg, 20, 9))
          lCodlead = NTSCInt(Mid(strCodOgg, 30, 9))
          lCodchia = NTSCInt(Mid(strCodOgg, 117, 9))
        Case "X"  'da bscsgsco
          lcod = NTSCInt(Mid(strCodOgg, 20, 9))
          lNumcontr = NTSCInt(Mid(strCodOgg, 134, 9))
        Case "V"  'da bn__role
          '
        Case "F"  'DA bncsprob
          lcod = NTSCInt(Mid(strCodOgg, 20, 9))
      End Select

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
  Public Overridable Sub NuovoMetSettaVar(ByVal strT() As String, ByVal strTipoOggetto As String)
    Try
      strTipoOgg = strTipoOggetto

      'nuovo metodo §
      Select Case strTipoOgg
        Case "A", "D" '"d" da bsdbdiba, "a" da bsmgarti, bsmgartv, bstcartv
          strCod = Trim(strT(3))
        Case "C"  'da bs--clie
          lcod = NTSCInt(strT(3))
        Case "K"  'da bscicomm
          lcod = NTSCInt(strT(3))
        Case "L"  'da bsmganlo
          strCod = Trim(strT(3))
          lcod = NTSCInt(strT(4))
        Case "N"  'matricole?
          strCod = Trim(strT(3))
          strMatr = Trim(strT(4))
        Case "M", "O" '"m" da bsveboll e bsvefdin "o" da bsorgsor
          strTipodoc = strT(3)
          nAnnoDoc = NTSCInt(strT(4))
          strSerieDoc = strT(5)
          lNumdoc = NTSCInt(strT(6))
          'Verifico se il numero di riga è valorizzato
          If (strT.Length > 8) Then
            dRigaDoc = NTSCInt(strT(8))
          Else
            dRigaDoc = 0
          End If
          lcod = NTSCInt(strT(7))
        Case "P"  'da bscgprin
          lcod = NTSCInt(strT(3))
          nAnnoDoc = NTSCInt(strT(4))
          strSerieDoc = strT(5)
          lNumdoc = NTSCInt(strT(6))
        Case "!"  'da bscrglea (griglia offerte), bscrgsof, bscsgser
          lcod = NTSCInt(strT(3))
          lCodlead = NTSCInt(strT(4))
          strTipodoc = strT(5)
          nAnnoDoc = NTSCInt(strT(6))
          strSerieDoc = strT(7)
          lNumdoc = NTSCInt(strT(8))
          dRigaDoc = NTSCInt(strT(9))
        Case "R"  'da bscrcrac, bscrlead, bscsgser, bscslead
          lCodlead = NTSCInt(strT(3))
        Case "J"  'da bscroppo
          lcod = NTSCInt(strT(3))
          lCodlead = NTSCInt(strT(4))
          lCodoppo = NTSCInt(strT(5))
        Case "Y"  'da bscsgchi, bscsgser
          lcod = NTSCInt(strT(3))
          lCodlead = NTSCInt(strT(4))
          lCodchia = NTSCInt(strT(5))
        Case "X"  'da bscsgsco
          lcod = NTSCInt(strT(3))
          lNumcontr = NTSCInt(strT(4))
        Case "V"  'da bn__role
          '
        Case "F"  'DA bncsprob
          lcod = NTSCInt(strT(3))
      End Select

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
    Dim lConto As Integer = 0
    Dim dsTmp As New DataSet
    Dim strLeads As String = ""
    Dim bAddConto As Boolean = False
    Dim bAddLeads As Boolean = False
    Try
      '--------------------------------------
      'Tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      '--------------------------------------
      'se c'è il crm o il customer service, se è stato passato il lead cerco anche i record del cliente e viceversa
      'solo se sono chiamato da BSCRGLEA o BS--CLIE
      If bModuloCRM Or bModuloCS Then
        If lCodlead <> 0 And strTipoOgg = "R" Then    'r = bscrglea
          'devo trovare il cliente dal lead
          lConto = oCldAole.CercaContoDaLead(strDittaCorrente, lCodlead)
          If lConto <> 0 Then bAddConto = True
        End If
        If strTipoOgg = "C" Then  'c = bs--clie
          'devo trovare i leads dal cliente, indipendentemente dalla destinazione diversa
          strLeads = oCldAole.CercaLeadsDaConto(strDittaCorrente, lcod)
          If strLeads.Trim <> "" Then bAddLeads = True
        End If
      End If

      '--------------------------------------
      'strQuery <> "" solo se strTipoOgg = "V" ovvero se chiamato da BN__ROLE
      If Not oCldAole.LeggiAllole(strDittaCorrente, ds, lProgr, strCod, lcod, strMatr, strTipodoc, _
                                  strSerieDoc, nAnnoDoc, lNumdoc, dRigaDoc, lCodlead, lCodoppo, _
                                  lCodchia, lNumcontr, strTipoOgg, strQuery, strPrgParent, _
                                  bAddConto, lConto, bAddLeads, strLeads) Then Return False

      '--------------------------------------
      'Imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldAole.SetTableDefaultValueFromDB(strNomeTabella, ds)
      SetDefaultValue(ds)

      RiempiColonneUnbound(ds)

      ds.AcceptChanges()

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
    Finally
      dsTmp.Clear()
    End Try
  End Function

  Public Overridable Sub SvuotaCampi(ByRef ds As DataSet)
    Try
      ds.Tables("ALLOLE").Columns("codditt").DefaultValue = strDittaCorrente
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      ds.Tables("ALLOLE").Columns("codditt").DefaultValue = strDittaCorrente
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

#Region "Eventi datatable"
  Public Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      '-------------------------------------------------------------
      'se non  compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "ao_progress" Then
        If NTSCInt(e.Row!ao_progress) = 0 Then OnAddNew(sender, e)
      End If
      '-------------------------------------------------------------

      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If

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

  Public Overridable Sub OnAddNew(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim lNumOgg As Integer = 0
    Dim strErr As String = ""
    Try
      lNumOgg = oCldAole.LegNuma(strDittaCorrente, "OO", "", 0, True)
      lNumOgg = oCldAole.AggNuma(strDittaCorrente, "OO", "", 0, lNumOgg, False, True, strErr)
      If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
      If lNumOgg = 0 Then
        lNumOgg = oCldAole.GetProgr(strDittaCorrente)
      End If

      e.Row!ao_progress = lNumOgg
      e.Row!ao_datins = Date.Today

      e.Row!ao_TIPORK = "F"
      If strPrgParent = "BN__ROLE" Then
        strTipoOgg = NTSCStr(e.Row!ao_TIPO)
      Else
        e.Row!ao_TIPO = strTipoOgg
      End If

      e.Row!ao_PROGRESL = lNumOgg
      e.Row!ao_ULTAGG = Date.Today
      Select Case strTipoOgg
        Case "A", "D"
          e.Row!ao_strcod = strCod
        Case "C", "F"
          e.Row!ao_codice = lcod
        Case "K"
          e.Row!ao_commeca = lcod
        Case "L"
          e.Row!ao_strcod = strCod
          e.Row!ao_lotto = lcod
        Case "N"
          e.Row!ao_strcod = strCod
          e.Row!ao_matric = strMatr
        Case "M", "O"
          e.Row!ao_tipodoc = strTipodoc
          e.Row!ao_seriedoc = strSerieDoc
          e.Row!ao_annodoc = nAnnoDoc
          e.Row!ao_numdoc = lNumdoc
          e.Row!ao_rigadoc = dRigaDoc
          e.Row!ao_codice = lcod
        Case "P"
          e.Row!ao_codice = lcod
          e.Row!ao_annodoc = nAnnoDoc
          e.Row!ao_seriedoc = strSerieDoc
          e.Row!ao_numdoc = lNumdoc
        Case "!"
          e.Row!ao_tipodoc = strTipodoc
          e.Row!ao_annodoc = nAnnoDoc
          e.Row!ao_seriedoc = strSerieDoc
          e.Row!ao_numdoc = lNumdoc
          e.Row!ao_rigadoc = dRigaDoc
          e.Row!ao_codice = lcod
          e.Row!ao_codlead = lCodlead
        Case "R"
          e.Row!ao_codlead = lCodlead
        Case "J"
          e.Row!ao_codice = lcod
          e.Row!ao_codlead = lCodlead
          e.Row!ao_codoppo = lCodoppo
        Case "Y"
          e.Row!ao_codice = lcod
          e.Row!ao_codlead = lCodlead
          e.Row!ao_codchia = lCodchia
        Case "X"
          e.Row!ao_codice = lcod
          e.Row!ao_numcontr = lNumcontr
      End Select

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

  Public Overridable Sub BeforeColUpdate_ao_progress(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Dim strErrore As String = ""

    Try
      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("ALLOLE").Select("ao_progress = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222086718750, "Il progressivo dell'oggetto OLE è già stato utilizzato. Inserire un codice non utilizzato")))
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

  Public Overridable Sub BeforeColUpdate_ao_codice(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If strTipoOgg.ToUpper = "F" Then Return

      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_descr1 = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509033408203, "Codice cliente/fornitore non corretto")))
          Return
        Else
          e.Row!xx_descr1 = strTmp
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_ao_controp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_descovg = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "COVG", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509051894531, "Codice contropartita non corretto")))
          Return
        Else
          e.Row!xx_descovg = strTmp
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_ao_strcod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!xx_descr = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509066054687, "Codice articolo non corretto")))
          Return
        Else
          e.Row!xx_descr = strTmp
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_ao_commeca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_descr1_commess = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "COMMESS", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509080302734, "Codice commessa non corretto")))
          Return
        Else
          e.Row!xx_descr1_commess = strTmp
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_ao_codlead(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_descr1_lead = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "LEADS", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509094775390, "Codice lead non corretto")))
          Return
        Else
          e.Row!xx_descr1_lead = strTmp
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_ao_codoppo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_oggetto = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "OPPORTUN", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509109736328, "Codice opportunità non corretto")))
          Return
        Else
          e.Row!xx_oggetto = strTmp
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_ao_codchia(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_oggetto_nnchiam = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "NNCHIAM", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509123896484, "Codice chiamata non corretto")))
          Return
        Else
          e.Row!xx_oggetto_nnchiam = strTmp
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_ao_tipo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("ALLOLE").Select(Nothing, Nothing, DataViewRowState.Added)
      For i = 0 To dtrTmp.Length - 1
        If dtrTmp(i)!ao_progress.ToString = "0" Then
          dtrTmp(i)!ao_progress = oCldAole.GetProgr(strDittaCorrente)
        End If
      Next
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

  Public Overridable Sub BeforeColUpdate_ao_nomedoc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strNomedoc As String
    Try
      If e.ProposedValue.ToString = "" Then
        e.ProposedValue = ""
        e.Row!xx_nomedoc = ""
      Else
        strNomedoc = NTSCStr(e.ProposedValue)
        e.ProposedValue = NTSCStr(e.ProposedValue).ToLower
        '-----------------------------------------------------------------------------------
        '--- Aggiungiamo, se necessario, l'estensione .doc
        '-----------------------------------------------------------------------------------
        If InStr(1, NTSCStr(e.ProposedValue), ".") = 0 Then
          If NTSCStr(e.ProposedValue).Substring(NTSCStr(e.ProposedValue).Length - 1, 1) <> "\" And _
             NTSCStr(e.ProposedValue).Substring(NTSCStr(e.ProposedValue).Length - 1, 1) <> "/" Then
            e.ProposedValue = strNomedoc & ".doc"
          Else
            e.ProposedValue = strNomedoc
          End If
        Else
          e.ProposedValue = strNomedoc
        End If
        e.Row!xx_nomedoc = RicostruisciPath(NTSCStr(e.ProposedValue), NTSCStr(e.Row!ao_cartella))
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

  Public Overridable Sub BeforeColUpdate_ao_cartella(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If Trim(e.ProposedValue.ToString) = "" Then
        e.ProposedValue = ""
      End If
      If NTSCStr(e.Row!ao_nomedoc) <> "" Then
        If Mid(NTSCStr(e.Row!ao_nomedoc), 2, 1) = ":" Or _
          Mid(NTSCStr(e.Row!ao_nomedoc), 1, 2) = "\\" Then
        Else
          e.Row!xx_nomedoc = RicostruisciPath(NTSCStr(e.Row!ao_nomedoc), NTSCStr(e.ProposedValue))
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

  Public Overridable Sub BeforeColUpdate_xx_lottox(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      'dato il lotto alfanumerico devo tradure in ID lotto numerico
      If Trim(e.ProposedValue.ToString) = "" Then
        e.ProposedValue = ""
        e.Row!ao_lotto = 0
        Return
      End If

      If bLottoNew = False Then
        'vecchia gestione lotti: il lotto è solo numerico di max 9 char: lo formatto
        e.ProposedValue = NTSCInt(e.ProposedValue).ToString("000000000")
      End If

      'dato il lotto alfanumerico ottengo l'ID numerico
      oCldAole.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANALOTTIX", "S", "", dttTmp, NTSCStr(e.Row!ao_strcod))
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513215192910290, "Codice lotto inesistente")))
        e.ProposedValue = ""
        Return
      End If

      e.Row!ao_lotto = NTSCInt(dttTmp.Rows(0)!alo_lotto)


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
  Public Overridable Sub AfterColUpdate_ao_lotto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'sono stato chiamato da bnmgalo e mi è stato passato il lotto numerico: compilo la colonna con il lotto alfanumerico
    Dim dttTmp As New DataTable
    Try
      'dato il lotto alfanumerico devo tradure in ID lotto numerico
      If NTSCInt(e.ProposedValue.ToString) = 0 Then
        e.Row!xx_lottox = ""
        Return
      End If

      'dato il lotto alfanumerico ottengo l'ID numerico
      oCldAole.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANALOTTI", "S", "", dttTmp, NTSCStr(e.Row!ao_strcod))
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513219111285131, "Codice lotto inesistente")))
        e.Row!ao_lotto = 0
        Return
      End If

      e.Row!xx_lottox = NTSCStr(dttTmp.Rows(0)!alo_lottox)

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

  Public Overridable Function BeforeColUpdate_ao_seriedoc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), False)
      If strTmp <> NTSCStr(e.ProposedValue) Then e.ProposedValue = strTmp

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
#End Region

  Public Overloads Overrides Function TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate
      dtrTmp = dsShared.Tables("ALLOLE").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        dtrTmp(i)!ao_ultagg = Date.Now
        dtrTmp(i)!ao_progresl = dtrTmp(i)!ao_progress.ToString
        dtrTmp(i)!ao_tipork = "F"
        'testo, per estrema sicurezza, che il codice progressivo non sia = 0
        If dtrTmp(i)!ao_progress.ToString = "0" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509218613281, "Inserire un progressivo valido")))
          Return False
        End If
        'testo anche che il Tipo Record sia uniforme ai valori nei vari campi cioè...
        '...se il tipo record è "Cliente"
        If dtrTmp(i)!ao_tipo.ToString = "C" And dtrTmp(i)!ao_codice.ToString = "0" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509233251953, "Inserire un codice cliente/fornitore valido")))
          Return False
        End If
        '...se il tipo record è "Ariticolo" o "Di.ba."
        If (dtrTmp(i)!ao_tipo.ToString = "A" Or dtrTmp(i)!ao_tipo.ToString = "D") And dtrTmp(i)!ao_strcod.ToString = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509248134765, "Inserire un codice articolo valido")))
          Return False
        End If
        '...se il tipo record è "Ordine" o "Documento di magazzino" o "Offerta"
        If (dtrTmp(i)!ao_tipo.ToString = "O" Or dtrTmp(i)!ao_tipo.ToString = "M" Or dtrTmp(i)!ao_tipo.ToString = "!") _
           And (dtrTmp(i)!ao_tipodoc.ToString = "" Or dtrTmp(i)!ao_annodoc.ToString = "0" Or dtrTmp(i)!ao_seriedoc.ToString = "" Or _
           dtrTmp(i)!ao_numdoc.ToString = "0") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509262138672, "Inserire un numero ordine/documento valido")))
          Return False
        End If
        '...se il tipo record è "Commessa"
        If dtrTmp(i)!ao_tipo.ToString = "K" And dtrTmp(i)!ao_commeca.ToString = "0" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509275976562, "Inserire un codice commessa valido")))
          Return False
        End If
        '...se il tipo record è "Matricola"
        If dtrTmp(i)!ao_tipo.ToString = "N" And dtrTmp(i)!ao_matric.ToString = "0" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509289736328, "Inserire un codice matricola valido")))
          Return False
        End If
        '...se il tipo record è "Lotto"
        If dtrTmp(i)!ao_tipo.ToString = "L" And NTSCStr(dtrTmp(i)!xx_lottox) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509303417968, "Inserire un codice lotto valido")))
          Return False
        End If
        '...se il tipo record è "Lead"
        If dtrTmp(i)!ao_tipo.ToString = "R" And dtrTmp(i)!ao_codlead.ToString = "0" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509316611328, "Inserire un codice lead valido")))
          Return False
        End If
        '...se il tipo record è "Opportunità"
        If dtrTmp(i)!ao_tipo.ToString = "J" And dtrTmp(i)!ao_codoppo.ToString = "0" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509330292968, "Inserire un codice opportunità valido")))
          Return False
        End If
        '...se il tipo record è "Chiamata"
        If dtrTmp(i)!ao_tipo.ToString = "Y" And dtrTmp(i)!ao_codchia.ToString = "0" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128963509344931640, "Inserire un codice chiamata valido")))
          Return False
        End If
        '...se il tipo record è "Contratto"
        If dtrTmp(i)!ao_tipo.ToString = "X" And dtrTmp(i)!ao_numcontr.ToString = "0" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222087187500, "Inserire un codice contratto valido")))
          Return False
        End If
      Next

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

  Public Overridable Sub SettaCampi()
    Try
      If dttParam Is Nothing Then Return

      With dttParam.Rows(0)
        strPrgParent = NTSCStr(!prgparent)
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

  Public Overridable Function RicostruisciPath(ByVal strNomedoc As String, ByVal strCartella As String) As String
    Try
      '-----------------------------------------------------------------------------------------
      '--- Controlliamo se percorso fisico o logico
      '-----------------------------------------------------------------------------------------
      If (Mid(strNomedoc, 2, 1) = ":") Or (Mid(strNomedoc, 1, 2) = "\\") Then
        '---------------------------------------------------------------------------------------
        '--- Percorso fisico: siamo a posto!!!!
        '---------------------------------------------------------------------------------------
        RicostruisciPath = strNomedoc
      Else
        '---------------------------------------------------------------------------------------
        '--- Percorso logico: dobbiamo aggiungere il path opportuno
        '--- Costruiamo il path:
        '---------------------------------------------------------------------------------------
        If Not strCartella = "" Then
          '-------------------------------------------------------------------------------------
          '--- Controlliamo se percorso fisico o logico
          '-------------------------------------------------------------------------------------
          If (Mid(strCartella, 2, 1) = ":") Or (Mid(strCartella, 1, 2) = "\\") Then
            '-----------------------------------------------------------------------------------
            '--- Percorso fisico: siamo a posto!!!!
            '-----------------------------------------------------------------------------------
            If Right(strCartella, 1) <> "\" Then
              RicostruisciPath = strCartella & "\" & strNomedoc
            Else
              RicostruisciPath = strCartella & strNomedoc
            End If
          Else
            '-----------------------------------------------------------------------------------
            '--- Percorso logico: dobbiamo aggiungere la dir soprastante
            '-----------------------------------------------------------------------------------
            If Right(strCartella, 1) <> "\" Then
              RicostruisciPath = strCartella & "\" & strNomedoc
            Else
              RicostruisciPath = strCartella & strNomedoc
            End If
            '-----------------------------------------------------------------------------------
            '--- Lo consideriamo un percorso logico: dobbiamo aggiungere la dir soprastante
            '-----------------------------------------------------------------------------------
            If Right(strPath, 1) <> "\" Then
              RicostruisciPath = strPath & "\" & RicostruisciPath
            Else
              RicostruisciPath = strPath & RicostruisciPath
            End If
          End If
        Else
          '-------------------------------------------------------------------------------------
          '--- Lo consideriamo un percorso logico: dobbiamo aggiungere la dir soprastante
          '-------------------------------------------------------------------------------------
          If Right(strPath, 1) <> "\" Then
            RicostruisciPath = strPath & "\" & strNomedoc
          Else
            RicostruisciPath = strPath & strNomedoc
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
  End Function

  Public Overridable Function RiempiColonneUnbound(ByRef ds As DataSet) As Boolean
    Dim i As Integer
    Dim strTmp As String
    Try
      For i = 0 To ds.Tables("ALLOLE").Rows.Count - 1
        With ds.Tables("ALLOLE").Rows(i)
          If Not NTSCStr(!ao_nomedoc) = "" Then
            If NTSCStr(!ao_cartella) = "" Then
              strTmp = ""
            Else
              strTmp = NTSCStr(!ao_cartella)
            End If
            !xx_nomedoc = RicostruisciPath(NTSCStr(!ao_nomedoc), strTmp)
          Else
            !xx_nomedoc = ""
          End If
        End With
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

#Region "FUNZIONI PER ACCESSI CRM"
  Public Overridable Function CercaAccessiDaDocumentiOrdini(ByVal strTipork As String, ByVal nAnno As Integer, _
    ByVal strSerie As String, ByVal nNumero As Integer, ByVal nCodcageAccdito As Integer, _
    ByVal strTipoRicerca As String) As Boolean
    Try
      Select Case strTipoRicerca
        Case "M"
          Return oCldAole.CercaAccessiDaDocumenti(strDittaCorrente, strTipork, nAnno, strSerie, nNumero, nCodcageAccdito)
        Case "O"
          Return oCldAole.CercaAccessiDaOrdini(strDittaCorrente, strTipork, nAnno, strSerie, nNumero, nCodcageAccdito)
      End Select
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function CercaLeadDaContoRiga(ByVal nConto As Integer) As Integer
    Try
      Return oCldAole.CercaLeadDaContoRiga(strDittaCorrente, nConto)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function RitornaAgenteDaAccdito() As Integer
    Try
      Return oCldAole.RitornaAgenteDaAccdito(strDittaCorrente)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
#End Region

End Class
