Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEXXSCON
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

  Public oCldScon As CLDXXSCON

  Public strActLog As String = ""
  Public strActLogProg As String = ""

  Public bHasChanges As Boolean
  Public dsShared As DataSet

  Public nTipork As Integer = 0               'tipo di listino: vedi dttTiposconti in bnxxsconti
  Public strCodart As String = ""             'articolo per visualizzaz articoli da bnmgarti
  Public strCodartRoot As String = ""         'cod articolo root per ottenere info in caso richiamato da BNMGARTV BNTCARTV
  Public lConto As Integer = 0                'conto cliente/fornitore 
  Public lCoddest As Integer = 0              'destinazione diversa
  Public nClasseArt As Integer = 0            'classe sconto cli/forn
  Public nClasseCli As Integer = 0            'classe sconto articolo
  Public strChildParent As String = ""        'nome del child chiamante (BN__CLIE, BNMGARTI, ...)
  Public bInPromozione As Boolean = False     'flag PROMOZIONE passato da UI
  Public bVariantiLiv1 As Boolean = False     'Se gestito a varianti di livello 1
  Public dtInizioValSconti As DateTime = DateTime.Now


  Public Overrides Function Init(ByRef App As CLE__APP, _
                                    ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                                    ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                                    ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDXXSCON"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldScon = CType(MyBase.ocldBase, CLDXXSCON)
    oCldScon.Init(oApp)
    Return True
  End Function


  Public Overridable Function Apri(ByVal strDitta As String, ByVal nClasseArticolo As Integer, ByVal nClasseCliforn As Integer, _
                                   ByVal bInpromo As Boolean, ByVal bNoTestValidoil As Boolean, ByVal strValidoil As String, _
                                   ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      nClasseArt = nClasseArticolo
      bInPromozione = bInpromo
      nClasseCli = nClasseCliforn

      dtInizioValSconti = NTSCDate(oCldScon.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DataInizioValSconti", DateTime.Now.ToShortDateString, " ", DateTime.Now.ToShortDateString))

      dReturn = oCldScon.GetData(strDittaCorrente, nTipork, lConto, strCodart, nClasseCli, _
                                 nClasseArt, bInpromo, bNoTestValidoil, strValidoil, ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldScon.SetTableDefaultValueFromDB("SCONTI", ds)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      SetDefaultValue(ds)
      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("SCONTI").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("SCONTI").ColumnChanged, AddressOf AfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsShared.Tables("SCONTI").AcceptChanges()
      bHasChanges = False

      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldScon.GetSettingBus("BSXXSCON", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
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
      ds.Tables("SCONTI").Columns("codditt").DefaultValue = ".,"      'per far scatenare la onaddnew 

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
      dsShared.Tables("SCONTI").Rows.Add(dsShared.Tables("SCONTI").NewRow)
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
      If dsShared IsNot Nothing Then dsShared.Tables("SCONTI").Select(strFilter)(nRow).RejectChanges()
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
        bResult = oCldScon.Salva(strDittaCorrente, dsShared.Tables("SCONTI"), "")
      Else
        bResult = oCldScon.Salva(strDittaCorrente, dsShared.Tables("SCONTI"), strActLogProg)
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
        e.Row!so_codart = IIf(strCodart = "", " ", strCodart).ToString
      Else
        oCldScon.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)

        Select Case NTSCStr(dttArti.Rows(0)!ar_prevar)
          Case "N" : e.Row!so_codart = Trim(strCodartRoot)
          Case "S" : e.Row!so_codart = strCodart
          Case "1" : bVariantiLiv1 = True : e.Row!so_codart = Trim(strCodartRoot) & Trim(NTSCStr(dttArti.Rows(0)!ar_codvar1))
        End Select
      End If
      e.Row!so_conto = lConto
      If lConto > 0 Then e.Row!so_coddest = lCoddest
      e.Row!so_clscar = nClasseArt
      e.Row!so_clscan = nClasseCli
      e.Row!so_datagg = dtInizioValSconti
      e.Row!so_datscad = NTSCDate(IntSetDate("31/12/2099"))
      e.Row!so_ultagg = DateTime.Now
      e.Row!so_fase = 0

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

  Public Overridable Sub BeforeColUpdate_so_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If

      If Not bVariantiLiv1 Then
        If (strCodart.Trim <> "" And NTSCStr(e.ProposedValue).Trim.ToUpper <> strCodart.Trim.ToUpper) And (strCodartRoot.Trim = "" Or _
           (strCodartRoot.Trim <> "" And NTSCStr(e.ProposedValue).Trim.ToUpper <> strCodartRoot.Trim.ToUpper)) Then
          'sono nell'anagrafica articoli: non posso cambiare il codice 
          e.Row!so_codart = strCodart.Trim.ToUpper
          e.ProposedValue = strCodart.Trim.ToUpper
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472859405283164, "Il codice articolo non puÃ² essere cambiato con qeusto programma")))
        End If

        oCldScon.ValCodiceDb(SettaVarValidazioni(e.ProposedValue.ToString), strDittaCorrente, "ARTICO", "S", "", dttTmp)
        If dttTmp.Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472781229643164, "Articolo inesistente")))
          e.ProposedValue = NTSCStr(e.Row!so_codart)
          Return
        End If
      End If

      '----------------------------------------------------------------------
      'Trattamento particolare articoli a varianti
      'If dttTmp.Rows(0)!ar_gesvar.ToString = "S" Then
      '  If dttTmp.Rows(0)!ar_prevar.ToString = "N" Then
      '    'articolo con prezzi comuni (se si sceglie la root va bene così)
      '    If NTSCStr(dttTmp.Rows(0)!ar_codroot).Trim <> "" Then
      '      'trattasi della variante
      '      e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_codroot)
      '      Return
      '    End If
      '  ElseIf dttTmp.Rows(0)!ar_prevar.ToString = "1" Then
      '    'articoli con prezzi sulla 1. variante
      '    If NTSCStr(dttTmp.Rows(0)!ar_codroot) <> "" Then
      '      'trattasi della variante
      '      e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_codroot) & NTSCStr(dttTmp.Rows(0)!ar_codvar1).Trim
      '      Return
      '    Else
      '      'trattasi dell'articolo root (non ammesso)
      '      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472785697093164, "Non è possibile selezionare il codice root di articoli a varianti il cui prezzo dipende dalla 1° variante")))
      '      e.ProposedValue = NTSCStr(e.Row!so_codart)
      '      Return
      '    End If
      '  Else
      '    'articoli con prezzi diversi per ogni variante
      '    If NTSCStr(dttTmp.Rows(0)!ar_codroot) = "" Then
      '      'trattasi dell'articolo root (non ammesso)
      '      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472786104903164, "Non è possibile selezionare il codice root di articoli a varianti il cui prezzo dipende da tutte le varianti")))
      '      e.ProposedValue = NTSCStr(e.Row!so_codart)
      '      Return
      '    End If
      '  End If
      'End If    'If dttTmp.Rows(0)!ar_gesvar.ToString = "S" Then


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

  Public Overridable Sub BeforeColUpdate_so_unmis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp, dttArti As New DataTable
    Dim strTmp As String = ""
    Try

      If NTSCStr(e.Row!so_codart).Trim = "" And NTSCInt(e.ProposedValue) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472818736213164, "Prima di impostare l'unitÃ  di misura indicare un codice articolo valido")))
        e.ProposedValue = NTSCStr(e.Row!so_unmis)
        Return
      End If

      If strCodartRoot.Trim <> "" Then
        oCldScon.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)

        Select Case NTSCStr(dttArti.Rows(0)!ar_prevar)
          Case "N" : e.Row!so_codart = Trim(strCodartRoot)
          Case "S" : e.Row!so_codart = strCodart
          Case "1" : bVariantiLiv1 = True : e.Row!so_codart = Trim(strCodartRoot) & Trim(NTSCStr(dttArti.Rows(0)!ar_codvar1))
        End Select
      End If

      If Not oCldScon.ValCodiceDb(SettaVarValidazioni(NTSCStr(e.Row!so_codart)), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
        dttTmp = dttArti
      End If

      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129132024687518256, "Articolo per unità di misura inesistente")))
        e.ProposedValue = NTSCStr(e.Row!so_unmis)
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
                    e.ProposedValue = NTSCStr(e.Row!so_unmis)

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

  Public Overridable Sub BeforeColUpdate_so_conto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If nTipork <> 1 And nTipork <> 3 And nTipork <> 5 Then
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xx_conto = ""
          e.Row!so_coddest = 0
        Else
          If lConto <> 0 And NTSCInt(e.ProposedValue) <> lConto Then
            e.ProposedValue = lConto
            e.Row!so_conto = lConto
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472857554103164, "Il codice cliente/fornitore non può essere variato con questo programma")))
          Else
            If Not oCldScon.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "ANAGRACF", "N", strTmp) Then
              e.ProposedValue = NTSCInt(e.Row!so_conto)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472842562643164, "Codice cliente/fornitore inesistente")))
            Else
              e.Row!xx_conto = strTmp
            End If
            e.Row!so_coddest = 0
          End If    'If lConto <> 0 And NTSCInt(e.ProposedValue) <> lConto Then

        End If
      Else
        If NTSCInt(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          e.Row!xx_conto = ""
          e.Row!so_coddest = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472841012413164, "Negli sconti generici articolo per classe cliente/fornitore il codice conto deve essere 0")))
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
  Public Overridable Sub BeforeColUpdate_so_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If nTipork <> 1 And nTipork <> 3 And nTipork <> 5 Then
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xx_coddest = ""
        Else
          If NTSCInt(e.Row!so_conto) = 0 Then
            e.ProposedValue = 0
            e.Row!xx_coddest = ""
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130584390586375908, "Prima di impostare la destinazione diversa occorre indicare il cod. cliente/fornitore")))
          Else
            If Not oCldScon.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "DESTDIV", "N", strTmp, Nothing, NTSCInt(e.Row!so_conto).ToString) Then
              e.ProposedValue = NTSCInt(e.Row!so_coddest)
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
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130584389852803145, "Negli sconti non specifici CLIENTI/FORNITORI il codice destinazione diversa deve essere 0")))
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

  Public Overridable Sub BeforeColUpdate_so_codtpro(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codtpro = ""
        Return
      End If

      If Not oCldScon.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABTPRO", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472852639903164, "Codice promozione '|" & NTSCInt(e.ProposedValue).ToString & "|' inesistente ")))
        e.ProposedValue = NTSCInt(e.Row!so_codtpro)
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

  Public Overridable Sub BeforeColUpdate_so_clscar(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If nTipork = 2 Or nTipork = 5 Then
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xx_clscar = ""
        Else
          If nClasseArt <> 0 And NTSCInt(e.ProposedValue) <> nClasseArt Then
            e.ProposedValue = nClasseArt
            e.Row!so_clscar = nClasseArt
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474965143267348, "Il codice classe articolo non può essere variato con questo programma")))
          Else
            If Not oCldScon.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABCSAR", "N", strTmp) Then
              e.ProposedValue = NTSCInt(e.Row!so_clscar)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474965104423348, "Codice classe articolo inesistente")))
            Else
              e.Row!xx_clscar = strTmp
            End If
          End If
        End If
      Else
        If NTSCInt(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          e.Row!xx_clscar = ""
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474965053723348, "La classe articolo può essere indicata solo in sconti per cliente/classe articolo o in classe cliente/classe articolo")))
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

  Public Overridable Sub BeforeColUpdate_so_clscan(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If nTipork <> 3 Or nTipork <> 5 Then
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xx_clscan = ""
        Else
          If nClasseCli <> 0 And NTSCInt(e.ProposedValue) <> nClasseCli Then
            e.ProposedValue = nClasseCli
            e.Row!so_clscan = nClasseCli
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474968137531348, "Il codice classe cliente/fornitore non può essere variato con questo programma")))
          Else
            If oApp.oGvar.strSconClCliDaList = "S" Then
              'classe sconto cli presa da num. listino
              If Not oCldScon.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABLIST", "N", strTmp) Then
                e.ProposedValue = NTSCInt(e.Row!so_clscan)
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130438477252150089, "Codice listino inesistente")))
              Else
                e.Row!xx_clscan = strTmp
              End If
            Else
              'caso normale
              If Not oCldScon.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABCSCL", "N", strTmp) Then
                e.ProposedValue = NTSCInt(e.Row!so_clscan)
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474968100871348, "Codice classe cliente/fornitore inesistente")))
              Else
                e.Row!xx_clscan = strTmp
              End If
            End If
          End If
        End If
      Else
        If NTSCInt(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          e.Row!xx_clscan = ""
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474968068891348, "La classe cliente/fornitore può essere indicata solo in sconti per articolo/classe cliente o in classe cliente/classe articolo")))
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

  Public Overridable Sub AfterColUpdate_so_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp, dttArti As New DataTable
    Try
      If e.ProposedValue.ToString.Trim = "" Then
        e.Row!xx_codart = ""
        e.Row!so_unmis = " "
        Return
      End If

      If strCodartRoot.Trim <> "" Then
        oCldScon.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)

        Select Case NTSCStr(dttArti.Rows(0)!ar_prevar)
          Case "N" : e.Row!so_codart = Trim(strCodartRoot)
          Case "S" : e.Row!so_codart = strCodart
          Case "1" : bVariantiLiv1 = True : e.Row!so_codart = Trim(strCodartRoot) & Trim(NTSCStr(dttArti.Rows(0)!ar_codvar1))
        End Select
      End If

      If Not oCldScon.ValCodiceDb(SettaVarValidazioni(e.ProposedValue.ToString), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
        dttTmp = dttArti
      End If

      e.Row!xx_codart = dttTmp.Rows(0)!ar_descr.ToString
      e.Row!so_unmis = dttTmp.Rows(0)!ar_unmis.ToString

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
      dtrTmp = dsShared.Tables("SCONTI").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1

        Select Case nTipork
          Case 0
            'Generico cliente/fornitore
            If NTSCInt(dtrTmp(i)!so_conto) = 0 Or NTSCStr(dtrTmp(i)!so_codart).Trim <> "" Or _
               NTSCInt(dtrTmp(i)!so_clscan) <> 0 Or NTSCInt(dtrTmp(i)!so_clscar) <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474829736250000, "Negli sconti generici cliente/fornitore è necessario avere: conto diverso da 0, articolo = '', classe articolo = 0, classe cliente = 0")))
              Return False
            End If
            If oApp.oGvar.strSconGenex.ToUpper = "S" Then
              'gestione di sconti generici articoli con sconti generici cliente:
              'gli sconti articolo sono sempre dal cod 1 al 4, mentre i generici cliente sono sempre il 5 ed il 6
              If NTSCDec(dtrTmp(i)!so_scont1) <> 0 Or NTSCDec(dtrTmp(i)!so_scont2) <> 0 Or _
                 NTSCDec(dtrTmp(i)!so_scont3) <> 0 Or NTSCDec(dtrTmp(i)!so_scont4) <> 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130431665944429728, "Con abilitato il flag per gestire Sconti generici cliente con Sconti generici articoli" & vbCrLf & "sullo sconto generico cliente gli sconti da 1 a 4 devono essere uguali a 0")))
                Return False
              End If
            End If
          Case 1
            'Generico articolo
            If NTSCInt(dtrTmp(i)!so_conto) <> 0 Or NTSCStr(dtrTmp(i)!so_codart).Trim = "" Or _
               NTSCInt(dtrTmp(i)!so_clscan) <> 0 Or NTSCInt(dtrTmp(i)!so_clscar) <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474829720494000, "Negli sconti generici articolo è necessario avere: conto = 0, articolo diverso da '', classe articolo = 0, classe cliente = 0")))
              Return False
            End If
            If oApp.oGvar.strSconGenex.ToUpper = "S" Then
              'gestione di sconti generici articoli con sconti generici cliente:
              'gli sconti articolo sono sempre dal cod 1 al 4, metnre i generici cliente sono sempre il 5 ed il 6
              If NTSCDec(dtrTmp(i)!so_scont5) <> 0 Or NTSCDec(dtrTmp(i)!so_scont6) <> 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130431664189445578, "Con abilitato il flag per gestire Sconti generici cliente con Sconti generici articoli" & vbCrLf & "sullo sconto generico articolo gli sconti 5 e 6 devono essere uguali a 0")))
                Return False
              End If
            End If
          Case 2
            'Per classe articolo
            If NTSCInt(dtrTmp(i)!so_conto) = 0 Or NTSCStr(dtrTmp(i)!so_codart).Trim <> "" Or _
               NTSCInt(dtrTmp(i)!so_clscan) <> 0 Or NTSCInt(dtrTmp(i)!so_clscar) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474829700682000, "Negli sconti per cliente / classe articolo è necessario avere: conto diverso da 0, articolo = '', classe articolo diversa da 0, classe cliente = 0")))
              Return False
            End If
          Case 3
            'Per classe cliente/fornitore
            If NTSCInt(dtrTmp(i)!so_conto) <> 0 Or NTSCStr(dtrTmp(i)!so_codart).Trim = "" Or _
               NTSCInt(dtrTmp(i)!so_clscan) = 0 Or NTSCInt(dtrTmp(i)!so_clscar) <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474829687578000, "Negli sconti per articolo / classe cliente è necessario avere: conto = 0, articolo diverso da '', classe articolo = 0, classe cliente diversa da 0")))
              Return False
            End If
            If oApp.oGvar.strSconGenex.ToUpper = "S" Then
              'gestione di sconti generici articoli con sconti generici cliente:
              'gli sconti articolo sono sempre dal cod 1 al 4, metnre i generici cliente sono sempre il 5 ed il 6
              If NTSCDec(dtrTmp(i)!so_scont5) <> 0 Or NTSCDec(dtrTmp(i)!so_scont6) <> 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130438465925478206, "Con abilitato il flag per gestire Sconti generici cliente con Sconti generici articoli" & vbCrLf & "sullo sconto generico articolo gli sconti 5 e 6 devono essere uguali a 0")))
                Return False
              End If
            End If
          Case 4
            'Specifico Articolo cli/forn
            If NTSCInt(dtrTmp(i)!so_conto) = 0 Or NTSCStr(dtrTmp(i)!so_codart).Trim = "" Or _
               NTSCInt(dtrTmp(i)!so_clscan) <> 0 Or NTSCInt(dtrTmp(i)!so_clscar) <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474829674318000, "Negli sconti specifici cliente (o fornitore) / articolo è necessario avere: conto diverso da 0, articolo diverso da '', classe articolo = 0, classe cliente = 0")))
              Return False
            End If
          Case 5
            'Per classe art / classe cli/for
            If NTSCInt(dtrTmp(i)!so_conto) <> 0 Or NTSCStr(dtrTmp(i)!so_codart).Trim <> "" Or _
               NTSCInt(dtrTmp(i)!so_clscan) = 0 Or NTSCInt(dtrTmp(i)!so_clscar) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128474829657938000, "Negli sconti per classe cliente (o fornitore) / classe articolo è necessario avere: conto = 0, articolo = '', classe articolo diversa da 0, classe cliente diversa da 0")))
              Return False
            End If
        End Select

        If NTSCDate(dtrTmp(i)!so_datagg) > NTSCDate(dtrTmp(i)!so_datscad) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472865610923164, "La data di fine validità non può essere minore della data di inizio validità")))
          Return False
        End If

        If NTSCInt(dtrTmp(i)!so_codtpro) = 0 And bInPromozione Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472867741843164, "Indicare un codice promozione diverso da 0 prima di salvare")))
          Return False
        End If

        If NTSCInt(dtrTmp(i)!so_codtpro) <> 0 And bInPromozione = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472868110623164, "Il codice promozione deve essere uguale a 0")))
          Return False
        End If

        dtrTmp(i)!so_ultagg = DateTime.Now

        '-------------------------------
        'test sconto già esistente
        If oCldScon.CheckScontoEsistente(strDittaCorrente, dtrTmp(i)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472870473923164, "Sconto già presente in questa data e con queste caratteristiche")))
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
