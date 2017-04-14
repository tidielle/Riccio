Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEXXPROV
  Inherits CLE__BASN
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = 0
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


  Public oCldProv As CLDXXPROV



  Public strActLog As String = ""
  Public strActLogProg As String = ""

  Public bHasChanges As Boolean
  Public dsShared As DataSet

  Public nTipork As Integer = 0               'tipo di provvigione: vedi dttTipoprov in bnxxprovv
  Public strCodart As String = ""             'articolo per visualizzaz articoli da bnmgarti
  Public strCodartRoot As String = ""         'cod articolo root per ottenere info in caso richiamato da BNMGARTV BNTCARTV
  Public lConto As Integer = 0                'conto cliente/fornitore 
  Public lCoddest As Integer = 0              'destinazione diversa
  Public nClasseArt As Integer = 0            'classe provvigione cli/forn
  Public nClasseCli As Integer = 0            'classe provvigione articolo
  Public nCodcage As Integer = 0              'codice agente
  Public strChildParent As String = ""        'nome del child chiamante (BN__CLIE, BNMGARTI, ...)
  Public nValuta As Integer = 0               'valuta impostata in UI
  Public bInPromozione As Boolean = False     'flag PROMOZIONE passato da UI
  Public bNoTestValuta As Boolean = False     'flag tutte le valute passato da UI
  Public bVariantiLiv1 As Boolean = False     'Per articoli a varianti con prezzi sulla prima variante
  Public dtInizioValProvv As DateTime = DateTime.Now

  Public Overrides Function Init(ByRef App As CLE__APP, _
                                    ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                                    ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                                    ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDXXPROV"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldProv = CType(MyBase.ocldBase, CLDXXPROV)
    oCldProv.Init(oApp)
    Return True
  End Function

  Public Overridable Function Apri(ByVal strDitta As String, ByVal nAgente As Integer, ByVal nClasseArticolo As Integer, _
                                   ByVal nClasseCliforn As Integer, ByVal bInpromo As Boolean, ByVal bNoTestValidoil As Boolean, _
                                   ByVal strValidoil As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      nCodcage = nAgente
      nClasseArt = nClasseArticolo
      bInPromozione = bInpromo
      nClasseCli = nClasseCliforn

      dtInizioValProvv = NTSCDate(oCldProv.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DataInizioValProvv", DateTime.Now.ToShortDateString, " ", DateTime.Now.ToShortDateString))

      dReturn = oCldProv.GetData(strDittaCorrente, nTipork, nCodcage, lConto, strCodart, nClasseCli, _
                                 nClasseArt, bInpromo, bNoTestValidoil, strValidoil, ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldProv.SetTableDefaultValueFromDB("PERPROV", ds)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      SetDefaultValue(ds)
      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("PERPROV").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("PERPROV").ColumnChanged, AddressOf AfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsShared.Tables("PERPROV").AcceptChanges()
      bHasChanges = False

      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldProv.GetSettingBus("BSXXPROV", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BS" & strChildParent.Substring(2).Replace("_", "-")

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

  Public Overridable Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("PERPROV").Columns("codditt").DefaultValue = ".,"      'per far scatenare la onaddnew 

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


  Public Overridable Sub Nuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("PERPROV").Rows.Add(dsShared.Tables("PERPROV").NewRow)
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
      If dsShared IsNot Nothing Then dsShared.Tables("PERPROV").Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
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
        bResult = oCldProv.Salva(strDittaCorrente, dsShared.Tables("PERPROV"), "")
      Else
        bResult = oCldProv.Salva(strDittaCorrente, dsShared.Tables("PERPROV"), strActLogProg)
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

  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property

  Public Overridable Sub OnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttArti As New DataTable
    Try
      e.Row!codditt = strDittaCorrente
      If strCodartRoot.Trim = "" Then
        e.Row!per_codart = IIf(strCodart = "", " ", strCodart).ToString
      Else
        oCldProv.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)

        Select Case NTSCStr(dttArti.Rows(0)!ar_prevar)
          Case "N" : e.Row!per_codart = Trim(strCodartRoot)
          Case "S" : e.Row!per_codart = strCodart
          Case "1" : bVariantiLiv1 = True : e.Row!per_codart = Trim(strCodartRoot) & Trim(NTSCStr(dttArti.Rows(0)!ar_codvar1))
        End Select
      End If
      e.Row!per_codcage = nCodcage
      e.Row!per_conto = lConto
      If lConto > 0 Then e.Row!per_coddest = lCoddest
      e.Row!per_clprar = nClasseArt
      e.Row!per_clpran = nClasseCli
      e.Row!per_datagg = dtInizioValProvv
      e.Row!per_datscad = NTSCDate(IntSetDate("31/12/2099"))
      e.Row!per_ultagg = DateTime.Now

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


#Region "Before / AfterColUpdate"
  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try

      '-------------------------------------------------------------
      'se non  compilato il numero di riga compilo i campi di default
      'Spostato sopra alla "ValoriUguali", per consentire l'inserimento di righe con tutti i valori a zero (come VB6)
      If e.Column.ColumnName <> "codditt" Then
        If e.Row!codditt.ToString = ".," Then OnAddNewRow(sender, e)
      End If

      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
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

  Public Overridable Sub BeforeColUpdate_per_codcage(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcage = ""
      Else
        If nCodcage <> 0 And NTSCInt(e.ProposedValue) <> nCodcage Then
          e.ProposedValue = nCodcage
          e.Row!per_codcage = nCodcage
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993841389980588, "Il codice agente non può essere variato con questo programma")))
        Else
          If Not oCldProv.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABCAGE", "N", strTmp) Then
            e.ProposedValue = NTSCInt(e.Row!per_conto)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993841512644688, "Codice agente inesistente")))
          Else
            e.Row!xx_codcage = strTmp
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


  Public Overridable Sub BeforeColUpdate_per_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If

      If nTipork = 3 Or nTipork = 4 Or nTipork = 5 Then

        If Not bVariantiLiv1 Then
          If (strCodart.Trim <> "" And NTSCStr(e.ProposedValue).Trim.ToUpper <> strCodart.Trim.ToUpper) And (strCodartRoot.Trim = "" Or _
             (strCodartRoot.Trim <> "" And NTSCStr(e.ProposedValue).Trim.ToUpper <> strCodartRoot.Trim.ToUpper)) Then
            'sono nell'anagrafica articoli: non posso cambiare il codice 
            e.Row!per_codart = strCodart.Trim.ToUpper
            e.ProposedValue = strCodart.Trim.ToUpper
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472859405283164, "Il codice articolo non puÃ² essere cambiato con qeusto programma")))
          End If

          oCldProv.ValCodiceDb(SettaVarValidazioni(e.ProposedValue.ToString), strDittaCorrente, "ARTICO", "S", "", dttTmp)
          If dttTmp.Rows.Count = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472781229643164, "Articolo inesistente")))
            e.ProposedValue = NTSCStr(e.Row!per_codart)
            Return
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Se chiamato da Gestione Agenti (BNPRCAGE), fa ulteriori controlli sull'articolo
        '------------------------------------------------------------------------------------------------------------
        If strChildParent.ToUpper = "BNPRCAGE" Then
          'Trattamento particolare articoli a varianti
          If dttTmp.Rows(0)!ar_gesvar.ToString = "S" Then
            If dttTmp.Rows(0)!ar_prevar.ToString = "N" Then
              'articolo con prezzi comuni (se si sceglie la root va bene cosÃ¬)
              If NTSCStr(dttTmp.Rows(0)!ar_codroot).Trim <> "" Then
                'trattasi della variante
                e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_codroot)
                Return
              End If
            ElseIf dttTmp.Rows(0)!ar_prevar.ToString = "1" Then
              'articoli con prezzi sulla 1. variante
              If NTSCStr(dttTmp.Rows(0)!ar_codroot) <> "" Then
                'trattasi della variante
                e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_codroot) & NTSCStr(dttTmp.Rows(0)!ar_codvar1).Trim
                Return
              Else
                'trattasi dell'articolo root (non ammesso)
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472785697093164, "Non Ã¨ possibile selezionare il codice root di articoli a varianti il cui prezzo dipende dalla 1Â° variante")))
                e.ProposedValue = NTSCStr(e.Row!per_codart)
                Return
              End If
            Else
              'articoli con prezzi diversi per ogni variante
              If NTSCStr(dttTmp.Rows(0)!ar_codroot) = "" Then
                'trattasi dell'articolo root (non ammesso)
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472786104903164, "Non Ã¨ possibile selezionare il codice root di articoli a varianti il cui prezzo dipende da tutte le varianti")))
                e.ProposedValue = NTSCStr(e.Row!per_codart)
                Return
              End If
            End If
          End If    'If dttTmp.Rows(0)!ar_gesvar.ToString = "S" Then
        End If
        '------------------------------------------------------------------------------------------------------------
      Else
        If NTSCStr(e.ProposedValue).Trim <> "" Then
          e.ProposedValue = " "
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128478184397857665, "Il codice articolo deve essere indicato solo in provvigioni generiche articolo, specifiche articolo / classe cliente o specifiche cliente / articolo")))
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

  Public Overridable Sub BeforeColUpdate_per_unmis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp, dttArti As New DataTable
    Dim strTmp As String = ""
    Try

      If NTSCStr(e.Row!per_codart).Trim = "" And NTSCInt(e.ProposedValue) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472818736213164, "Prima di impostare l'unitÃ  di misura indicare un codice articolo valido")))
        e.ProposedValue = NTSCStr(e.Row!per_unmis)
        Return
      End If

      If strCodartRoot.Trim <> "" Then
        oCldProv.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)

        Select Case NTSCStr(dttArti.Rows(0)!ar_prevar)
          Case "N" : e.Row!per_codart = Trim(strCodartRoot)
          Case "S" : e.Row!per_codart = strCodart
          Case "1" : bVariantiLiv1 = True : e.Row!per_codart = Trim(strCodartRoot) & Trim(NTSCStr(dttArti.Rows(0)!ar_codvar1))
        End Select
      End If

      If Not oCldProv.ValCodiceDb(SettaVarValidazioni(NTSCStr(e.Row!per_codart)), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
        dttTmp = dttArti
      End If

      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129132024826580756, "Articolo per unità di misura inesistente")))
        e.ProposedValue = NTSCStr(e.Row!per_unmis)
        Return
      End If

      If NTSCStr(e.ProposedValue).ToUpper <> NTSCStr(dttTmp.Rows(0)!ar_unmis).ToUpper Then
        strTmp = " '" & NTSCStr(dttTmp.Rows(0)!ar_unmis) & "'"
        If NTSCStr(dttTmp.Rows(0)!ar_confez2).Trim <> "" Then
Confez2:
          If NTSCStr(e.ProposedValue).ToUpper <> NTSCStr(dttTmp.Rows(0)!ar_confez2).ToUpper Then
            strTmp += " '" & NTSCStr(dttTmp.Rows(0)!ar_confez2) & "'"
            If NTSCStr(dttTmp.Rows(0)!ar_unmis2).Trim <> "" Then
Unmis2:
              If NTSCStr(e.ProposedValue).ToUpper <> NTSCStr(dttTmp.Rows(0)!ar_unmis2).ToUpper Then
                strTmp += " '" & NTSCStr(dttTmp.Rows(0)!ar_unmis2) & "'"
                If NTSCStr(dttTmp.Rows(0)!ar_um4).Trim <> "" Then
Um4:
                  If NTSCStr(e.ProposedValue).ToUpper <> NTSCStr(dttTmp.Rows(0)!ar_um4).ToUpper Then
                    strTmp += " '" & NTSCStr(dttTmp.Rows(0)!ar_um4) & "'"

                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472874655213164, "Unità di misura non presente nell'anagrafica articolo. Le unità di misura ammesse sono: |" & strTmp & "|")))
                    e.ProposedValue = NTSCStr(e.Row!per_unmis)

                  Else
                    e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_um4)
                  End If
                Else
                  GoTo Um4 'controlla le ulteriori unita di misura per validazioni
                End If
              Else
                e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_unmis2)
              End If
            Else
              GoTo Unmis2 'controlla le ulteriori unita di misura per validazioni
            End If
          Else
            e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_confez2)
          End If
        Else
          GoTo Confez2 'controlla le ulteriori unita di misura per validazioni
        End If
      Else
        e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_unmis)
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

  Public Overridable Sub BeforeColUpdate_per_conto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If nTipork = 1 Or nTipork = 2 Or nTipork = 3 Then
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xx_conto = ""
          e.Row!per_coddest = 0
        Else
          If lConto <> 0 And NTSCInt(e.ProposedValue) <> lConto Then
            e.ProposedValue = lConto
            e.Row!per_conto = lConto
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993841340446168, "Il codice cliente/fornitore non può essere variato con questo programma")))
          Else
            If Not oCldProv.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "ANAGRACF", "N", strTmp) Then
              e.ProposedValue = NTSCInt(e.Row!per_conto)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993841552959768, "Codice cliente/fornitore inesistente")))
            Else
              e.Row!xx_conto = strTmp
            End If
            e.Row!per_coddest = 0
          End If    'If lConto <> 0 And NTSCInt(e.ProposedValue) <> lConto Then
        End If
      Else
        If NTSCInt(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          e.Row!xx_conto = ""
          e.Row!per_coddest = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472841012413164, "Il codice cliente/fornitore deve essere indicato solo in provvigioni generiche cli/forn, specifiche cliente / classe articolo o specifiche cliente / articolo")))
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

  Public Overridable Sub BeforeColUpdate_per_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If nTipork = 1 Or nTipork = 2 Or nTipork = 3 Then
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xx_coddest = ""
        Else
          If NTSCInt(e.Row!per_conto) = 0 Then
            e.ProposedValue = 0
            e.Row!xx_coddest = ""
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130584390586375908, "Prima di impostare la destinazione diversa occorre indicare il cod. cliente/fornitore")))
          Else
            If Not oCldProv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "DESTDIV", "N", strTmp, Nothing, NTSCInt(e.Row!per_conto).ToString) Then
              e.ProposedValue = NTSCInt(e.Row!per_coddest)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130584391515832715, "Codice destinazione diversa inesistente")))
            Else
              e.Row!xx_coddest = strTmp
            End If
          End If
        End If
      Else
        If NTSCInt(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          e.Row!xx_coddest = ""
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130584389852803145, "Il codice destinazione deve essere indicato solo in provvigioni generiche cli/forn, specifiche cliente / classe articolo o specifiche cliente / articolo")))
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

  Public Overridable Sub BeforeColUpdate_per_codtpro(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codtpro = ""
        Return
      End If

      If Not oCldProv.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABTPRO", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472852639903164, "Codice promozione '|" & NTSCInt(e.ProposedValue).ToString & "|' inesistente ")))
        e.ProposedValue = NTSCInt(e.Row!per_codtpro)
      Else
        e.Row!xx_codtpro = strTmp
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

  Public Overridable Sub BeforeColUpdate_per_clprar(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If nTipork = 2 Or nTipork = 6 Then
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xx_clprar = ""
        Else
          If nClasseArt <> 0 And NTSCInt(e.ProposedValue) <> nClasseArt Then
            e.ProposedValue = nClasseArt
            e.Row!per_clprar = nClasseArt
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474965143267348, "Il codice classe provvigione articolo non può essere variato con questo programma")))
          Else
            If Not oCldProv.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABCPAR", "N", strTmp) Then
              e.ProposedValue = NTSCInt(e.Row!per_clprar)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474965104423348, "Codice classe provvigione articolo inesistente")))
            Else
              e.Row!xx_clprar = strTmp
            End If
          End If
        End If
      Else
        If NTSCInt(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          e.Row!xx_clprar = ""
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474965053723348, "La classe provvigione articolo può essere indicata solo in provvigioni per cliente/classe articolo o in classe cliente/classe articolo")))
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

  Public Overridable Sub BeforeColUpdate_per_clpran(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If nTipork = 5 Or nTipork = 6 Then
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xx_clpran = ""
        Else
          If nClasseCli <> 0 And NTSCInt(e.ProposedValue) <> nClasseCli Then
            e.ProposedValue = nClasseCli
            e.Row!per_clpran = nClasseCli
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474968137531348, "Il codice classe provvigione cliente/fornitore non può essere variato con questo programma")))
          Else
            If Not oCldProv.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABCPCL", "N", strTmp) Then
              e.ProposedValue = NTSCInt(e.Row!per_clpran)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474968100871348, "Codice classe provvigione cliente/fornitore inesistente")))
            Else
              e.Row!xx_clpran = strTmp
            End If
          End If
        End If
      Else
        If NTSCInt(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          e.Row!xx_clpran = ""
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474968068891348, "La classe provvigione cliente/fornitore può essere indicata solo in provvigione per articolo/classe cliente o in classe cliente/classe articolo")))
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

  Public Overridable Sub AfterColUpdate_per_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp, dttArti As New DataTable
    Try
      If e.ProposedValue.ToString.Trim = "" Then
        e.Row!xx_codart = ""
        e.Row!per_unmis = " "
        Return
      End If

      If strCodartRoot.Trim <> "" Then
        oCldProv.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)

        Select Case NTSCStr(dttArti.Rows(0)!ar_prevar)
          Case "N" : e.Row!per_codart = Trim(strCodartRoot)
          Case "S" : e.Row!per_codart = strCodart
          Case "1" : bVariantiLiv1 = True : e.Row!per_codart = Trim(strCodartRoot) & Trim(NTSCStr(dttArti.Rows(0)!ar_codvar1))
        End Select
      End If

      If Not oCldProv.ValCodiceDb(SettaVarValidazioni(e.ProposedValue.ToString), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
        dttTmp = dttArti
      End If
      e.Row!xx_codart = dttTmp.Rows(0)!ar_descr.ToString
      e.Row!per_unmis = dttTmp.Rows(0)!ar_unmis.ToString

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
#End Region


  Public Overridable Function TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("PERPROV").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1

        Select Case nTipork
          Case 0
            'agente
            If NTSCInt(dtrTmp(i)!per_codcage) = 0 Or _
               NTSCInt(dtrTmp(i)!per_conto) <> 0 Or NTSCStr(dtrTmp(i)!per_codart).Trim <> "" Or _
               NTSCInt(dtrTmp(i)!per_clpran) <> 0 Or NTSCInt(dtrTmp(i)!per_clprar) <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474829736250000, "Nelle provvigioni generiche agente è necessario avere: agente diverso da 0, conto = 0, articolo = '', classe articolo = 0, classe cliente = 0")))
              Return False
            End If
          Case 1
            'agente + cliente
            If NTSCInt(dtrTmp(i)!per_conto) = 0 Or NTSCStr(dtrTmp(i)!per_codart).Trim <> "" Or _
               NTSCInt(dtrTmp(i)!per_clpran) <> 0 Or NTSCInt(dtrTmp(i)!per_clprar) <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474829720494000, "Nelle provvigioni generiche cliente/fornitore è necessario avere: conto diverso da 0, articolo = '', classe articolo = 0, classe cliente = 0")))
              Return False
            End If
          Case 2
            'agente + cliente + classe articolo
            If NTSCInt(dtrTmp(i)!per_conto) = 0 Or NTSCStr(dtrTmp(i)!per_codart).Trim <> "" Or _
               NTSCInt(dtrTmp(i)!per_clpran) <> 0 Or NTSCInt(dtrTmp(i)!per_clprar) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128478173619887665, "Nelle provvigioni per cliente / classe articolo è necessario avere: conto diverso da 0, articolo = '', classe articolo diversa da 0, classe cliente = 0")))
              Return False
            End If
          Case 3
            'agente + cliente + articolo
            If NTSCInt(dtrTmp(i)!per_conto) = 0 Or NTSCStr(dtrTmp(i)!per_codart).Trim = "" Or _
               NTSCInt(dtrTmp(i)!per_clpran) <> 0 Or NTSCInt(dtrTmp(i)!per_clprar) <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128478174172127665, "Nelle provvigioni specifiche cliente / articolo è necessario avere: conto diverso da 0, articolo diverso da '', classe articolo = 0, classe cliente = 0")))
              Return False
            End If
          Case 4
            'agente + articolo
            If NTSCInt(dtrTmp(i)!per_conto) <> 0 Or NTSCStr(dtrTmp(i)!per_codart).Trim = "" Or _
               NTSCInt(dtrTmp(i)!per_clpran) <> 0 Or NTSCInt(dtrTmp(i)!per_clprar) <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474829674318000, "Nelle provvigioni generiche articolo è necessario avere: conto= 0, articolo diverso da '', classe articolo = 0, classe cliente = 0")))
              Return False
            End If
          Case 5
            'agente + articolo + classe cliente
            If NTSCInt(dtrTmp(i)!per_conto) <> 0 Or NTSCStr(dtrTmp(i)!per_codart).Trim = "" Or _
               NTSCInt(dtrTmp(i)!per_clpran) = 0 Or NTSCInt(dtrTmp(i)!per_clprar) <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474829657938000, "Nelle provvigioni per articolo / classe cliente è necessario avere: conto = 0, articolo diverso da '', classe = 0, classe cliente diversa da 0")))
              Return False
            End If
          Case 6
            'agente + classe cliente + classe articolo
            If strChildParent = "BNPRCLPR" Then
              If NTSCInt(dtrTmp(i)!per_codcage) <> 0 Or _
                 NTSCInt(dtrTmp(i)!per_conto) <> 0 Or NTSCStr(dtrTmp(i)!per_codart).Trim <> "" Or _
                 NTSCInt(dtrTmp(i)!per_clpran) = 0 Or NTSCInt(dtrTmp(i)!per_clprar) = 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128478176673899665, "Nelle provvigioni per classe articolo / classe cliente è necessario avere: agente = 0, conto = 0, articolo = '', classe diversa da 0, classe cliente diversa da 0")))
                Return False
              End If
            ElseIf strChildParent = "BNPRCAGE" Then
              If NTSCInt(dtrTmp(i)!per_codcage) = 0 Or _
                NTSCInt(dtrTmp(i)!per_conto) <> 0 Or NTSCStr(dtrTmp(i)!per_codart).Trim <> "" Or _
                NTSCInt(dtrTmp(i)!per_clpran) = 0 Or NTSCInt(dtrTmp(i)!per_clprar) = 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128722747720398140, "Nelle provvigioni per classe articolo / classe cliente è necessario avere: agente diverso da 0, conto = 0, articolo = '', classe diversa da 0, classe cliente diversa da 0")))
                Return False
              End If
            End If
        End Select

        If NTSCDate(dtrTmp(i)!per_datagg) > NTSCDate(dtrTmp(i)!per_datscad) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472865610923164, "La data di fine validità non può essere minore della data di inizio validità")))
          Return False
        End If

        If NTSCInt(dtrTmp(i)!per_codtpro) = 0 And bInPromozione Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472867741843164, "Indicare un codice promozione diverso da 0 prima di salvare")))
          Return False
        End If

        If NTSCInt(dtrTmp(i)!per_codtpro) <> 0 And bInPromozione = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472868110623164, "Il codice promozione deve essere uguale a 0")))
          Return False
        End If

        dtrTmp(i)!per_ultagg = DateTime.Now

        '-------------------------------
        'test provvigione già esistente
        If oCldProv.CheckProvvigioneEsistente(strDittaCorrente, dtrTmp(i)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472870473923164, "Provvigione già presente in questa data e con queste caratteristiche")))
          Return False
        End If

        If (NTSCDec(dtrTmp(i)!per_provv) <> 0) And (NTSCDec(dtrTmp(i)!per_vprovv) <> 0) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130471425509736508, "Attenzione!" & vbCrLf & _
            "Sono stati indicati valori sia in colonna 'Percentuale' che 'Valore' provvigione." & vbCrLf & _
            "Le percentuali provvigione non saranno utilizzate.")))
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

  Public Overridable Function SettaVarValidazioni(ByVal strCodartPass As String) As String
    Try
      'Nel caso richiamato da BNMGARTV BNTCARTV
      'nella creazione di un articolo a 2-3 livelli con prezzi per 1'a variante
      'il listino viene salvato alla 1'a variante utilizzando la var strCodart
      'mentre deve essere validato come il root utilizzando la var strCodartRoot
      If strCodartRoot.Trim <> "" Then
        If strCodartPass = "D" Then
          Return "D"
        Else
          Return strCodartRoot
        End If
      Else
        Return strCodartPass
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return ""
      '--------------------------------------------------------------
    End Try
  End Function

End Class
