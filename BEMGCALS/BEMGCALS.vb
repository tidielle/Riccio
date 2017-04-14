Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGCALS
  Inherits CLE__BASE

  Public oCldCals As CLDMGCALS

  Public bElabInCorso As Boolean = False

  Public bSeleziona As Boolean
  Public strWhereFiar As String


  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGCALS"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldCals = CType(MyBase.ocldBase, CLDMGCALS)
    oCldCals.Init(oApp)
    Return True
  End Function

  Public Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try

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

  Public Overridable Function TestPreElabora(ByVal bSeleziona As Boolean) As Boolean
    Dim evt As NTSEventArgs = Nothing
    Try

      '-------------------------------------------------------------------------------
      ' Controlla se sono stati selezionati gli articoli
      '-------------------------------------------------------------------------------
      If bSeleziona = False Then
        evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128503253765946573, "Non sono stati selezionati gli articoli." & vbCrLf & _
        "Passare ora alla selezione?"))
        ThrowRemoteEvent(evt)
        If Not evt.RetValue = "YES" Then
          Return False
        Else
          ThrowRemoteEvent(New NTSEventArgs("SELEZIONA:", ""))
          If bSeleziona = False Then Return False
        End If
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

  Public Overridable Function Elabora(ByRef lRecords As Integer, _
                                   ByVal bopTipolist0 As Boolean, ByVal stredListino As String, _
                                   ByVal bopTipolist1 As Boolean, ByVal bopTipolist2 As Boolean, _
                                   ByVal stredConto As String, ByVal stredCodvalu As String, _
                                   ByVal stredCodlavo As String, ByVal stredDtinval As String, _
                                   ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                   ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                   ByVal stredFase As String, ByVal bopListdacanc0 As Boolean, _
                                   ByVal bopListdacanc1 As Boolean, ByVal stredDtfival As String) As Boolean
    Try
      'obsoleta
      Return Elabora(lRecords, bopTipolist0, stredListino, bopTipolist1, bopTipolist2, _
                    stredConto, stredCodvalu, stredCodlavo, stredDtinval, bedCodtpro, stredCodtpro, _
                    bopUltimaFase, bopSoloFase, stredFase, bopListdacanc0, bopListdacanc1, stredDtfival, 0)

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
  Public Overridable Function Elabora(ByRef lRecords As Integer, _
                                     ByVal bopTipolist0 As Boolean, ByVal stredListino As String, _
                                     ByVal bopTipolist1 As Boolean, ByVal bopTipolist2 As Boolean, _
                                     ByVal stredConto As String, ByVal stredCodvalu As String, _
                                     ByVal stredCodlavo As String, ByVal stredDtinval As String, _
                                     ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                     ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                     ByVal stredFase As String, ByVal bopListdacanc0 As Boolean, _
                                     ByVal bopListdacanc1 As Boolean, ByVal stredDtfival As String, _
                                     ByVal lCoddest As Integer) As Boolean

    Dim bRet As Boolean
    Dim dsListini As DataSet = Nothing
    Dim evt As NTSEventArgs = Nothing
    Dim i As Integer
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lRecords, bopTipolist0, stredListino, bopTipolist1, bopTipolist2, _
                    stredConto, stredCodvalu, stredCodlavo, stredDtinval, bedCodtpro, stredCodtpro, _
                    bopUltimaFase, bopSoloFase, stredFase, bopListdacanc0, bopListdacanc1, stredDtfival, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        lRecords = NTSCInt(oIn(0))
        Return CBool(oOut)
      End If
      '----------------

      '-----------------------
      'test di rito
      If Not TestPreElabora(bSeleziona) Then Return False

      lRecords = 0

      bElabInCorso = True

      '-------------------------------------------------------------------------------
      ' Chiede conferma dell'elaborazione
      '-------------------------------------------------------------------------------
      evt = New NTSEventArgs("MSG_NOYES", oApp.Tr(Me, 128539478769166099, "ATTENZIONE! Confermare la CANCELLAZIONE dei listini con i filtri indicati?" & vbCrLf & _
                                                                          "N.B. l'operazione è irreversibile!"))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then Return False



      'listini errori
      If bopListdacanc0 Then
        oCldCals.GetListini(strDittaCorrente, strWhereFiar, bopTipolist0, stredListino, _
                            bopTipolist1, bopTipolist2, stredConto, stredCodvalu, _
                            stredCodlavo, stredDtinval, bedCodtpro, stredCodtpro, _
                            bopUltimaFase, bopSoloFase, stredFase, dsListini, lCoddest)
      Else
        oCldCals.GetListiniVecchi(strDittaCorrente, strWhereFiar, bopTipolist0, stredCodvalu, _
                                  stredCodlavo, stredDtfival, bopTipolist1, stredConto, _
                                  bedCodtpro, stredCodtpro, bopUltimaFase, bopSoloFase, _
                                  stredFase, stredListino, bopTipolist2, dsListini, lCoddest)
      End If


      evt = New NTSEventArgs("MSG_NOYES", oApp.Tr(Me, 130341657785868298, "Verranno trattate |" & dsListini.Tables("LISTINI").Rows.Count & "| righe. Continuare l'elaborazione?"))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then Return False

      If bopListdacanc0 Then
        For i = 0 To dsListini.Tables("LISTINI").Rows.Count - 1
          '------------------------
          'scrivo la registrazione
          bRet = oCldCals.ElaboraListini(strDittaCorrente, dsListini.Tables("LISTINI").Rows(i))
        Next

        ThrowRemoteEvent(New NTSEventArgs("REFREINFO:", "Cancellazione listini generati per errore in corso..."))

        bRet = oCldCals.ElaboraListiniErrori(strDittaCorrente, strWhereFiar, bopTipolist0, stredDtinval, _
                                             stredCodvalu, stredCodlavo, bopTipolist1, stredConto, _
                                             bedCodtpro, stredCodtpro, bopUltimaFase, bopSoloFase, _
                                             stredFase, stredListino, bopTipolist2, lRecords, lCoddest)
      Else



        ThrowRemoteEvent(New NTSEventArgs("REFREINFO:", "Cancellazione listini vecchi in corso..."))

        bRet = oCldCals.ElaboraListiniVecchi(strDittaCorrente, strWhereFiar, bopTipolist0, stredCodvalu, _
                                             stredCodlavo, stredDtfival, bopTipolist1, stredConto, _
                                             bedCodtpro, stredCodtpro, bopUltimaFase, bopSoloFase, _
                                             stredFase, stredListino, bopTipolist2, lRecords, lCoddest)
      End If

      bElabInCorso = False

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
      bElabInCorso = False
    End Try
  End Function

  Public Overridable Function ElaboraAggiornaScadenza(ByVal bopTipolist0 As Boolean, ByVal stredListino As String, _
                                                    ByVal bopTipolist1 As Boolean, ByVal bopTipolist2 As Boolean, _
                                                    ByVal stredConto As String, ByVal stredCodvalu As String, _
                                                    ByVal stredCodlavo As String, ByVal stredDtinval As String, _
                                                    ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                                    ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                                    ByVal stredFase As String, ByVal stredDtfival As String) As Boolean
    Try
      'obsoleta
      Return ElaboraAggiornaScadenza(bopTipolist0, stredListino, bopTipolist1, bopTipolist2, stredConto, stredCodvalu, _
                                     stredCodlavo, stredDtinval, bedCodtpro, stredCodtpro, bopUltimaFase, bopSoloFase, _
                                     stredFase, stredDtfival, 0)

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
  Public Overridable Function ElaboraAggiornaScadenza(ByVal bopTipolist0 As Boolean, ByVal stredListino As String, _
                                                      ByVal bopTipolist1 As Boolean, ByVal bopTipolist2 As Boolean, _
                                                      ByVal stredConto As String, ByVal stredCodvalu As String, _
                                                      ByVal stredCodlavo As String, ByVal stredDtinval As String, _
                                                      ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                                      ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                                      ByVal stredFase As String, ByVal stredDtfival As String, _
                                                      ByVal lCoddest As Integer) As Boolean

    Dim dsListini As DataSet = Nothing
    Dim evt As NTSEventArgs = Nothing
    Dim i As Integer = 0
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bopTipolist0, stredListino, bopTipolist1, bopTipolist2, stredConto, stredCodvalu, _
                                     stredCodlavo, stredDtinval, bedCodtpro, stredCodtpro, bopUltimaFase, bopSoloFase, _
                                     stredFase, stredDtfival, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------


      '-----------------------
      'test di rito
      If Not TestPreElabora(bSeleziona) Then Return False

      bElabInCorso = True

      '-------------------------------------------------------------------------------
      ' Chiede conferma dell'elaborazione
      '-------------------------------------------------------------------------------
      evt = New NTSEventArgs("MSG_NOYES", oApp.Tr(Me, 128539478769166098, "ATTENZIONE! Confermare l'aggiornamento della DATA SCADENZA dei listini con i filtri indicati?" & vbCrLf & _
                                                                          "N.B. l'operazione è irreversibile!"))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then Return False

      'listini errori
      oCldCals.GetListiniAgg(strDittaCorrente, strWhereFiar, bopTipolist0, stredListino, _
                             bopTipolist1, bopTipolist2, stredConto, stredCodvalu, _
                             stredCodlavo, stredDtinval, bedCodtpro, stredCodtpro, _
                             bopUltimaFase, bopSoloFase, stredFase, dsListini, lCoddest)

      'Se la data di inizio è precedente alla data di fine validita escludo il listino trovato
      For Each dtrListino As DataRow In dsListini.Tables("LISTINI").Rows
        If NTSCDate(dtrListino!lc_datagg) > NTSCDate(stredDtfival) Then dtrListino.Delete() : Continue For

        'Verifica se ci sono dei listini uguali con data inizio successiva alla data scadenza da impostare
        If oCldCals.VerificaListiniSuccessivi(dtrListino, NTSCDate(stredDtfival)) Then dtrListino.Delete()
      Next
      dsListini.AcceptChanges()

      evt = New NTSEventArgs("MSG_NOYES", oApp.Tr(Me, 130341657785868299, "Verranno aggiornate |" & dsListini.Tables("LISTINI").Rows.Count & "| righe. Continuare l'elaborazione?"))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then Return False


      ThrowRemoteEvent(New NTSEventArgs("REFREINFO:", "Aggiornamento scadenza listini in corso..."))
      For Each dtrListino As DataRow In dsListini.Tables("LISTINI").Rows
        '------------------------
        'scrivo la registrazione
        oCldCals.PortaAScadenzaListini(strDittaCorrente, dtrListino, NTSCDate(stredDtfival))
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
    Finally
      bElabInCorso = False
    End Try
  End Function

  Public Overridable Function edConto_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String, _
                                                ByVal bopTipolist1 As Boolean, ByVal bopTipolist2 As Boolean) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As DataTable = New DataTable
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldCals.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "ANAGRA", "N", strTmp, dttTmp) Then
          strErr = oApp.Tr(Me, 128540464824491760, "Cliente/Fornitore inesistente.")
          Return False
        Else
          If NTSCStr(dttTmp.Rows(0)!an_tipo) = "S" Then
            strErr = oApp.Tr(Me, 128540464843631832, "Il conto non può far parte dei sottoconti.")
            Return False
          Else
            If (bopTipolist1) And NTSCStr(dttTmp.Rows(0)!an_tipo) <> "C" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128541842697694584, "Il conto deve far parte dei clienti.")))
              Return False
            End If
            If (bopTipolist2) And NTSCStr(dttTmp.Rows(0)!an_tipo) <> "F" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128541842720412314, "Il conto deve far parte dei fornitori.")))
              Return False
            End If
            strDescr = strTmp
          End If
        End If
      Else
        strDescr = ""
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
  Public Overridable Function edCodvalu_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As DataTable = New DataTable
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldCals.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABVALU", "N", strTmp, dttTmp) Then
          strErr = oApp.Tr(Me, 128540464878992304, "Valuta inesistente.")
          Return False
        Else
          strDescr = strTmp
        End If
      Else
        strDescr = ""
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
  Public Overridable Function edCodlavo_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As DataTable = New DataTable
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldCals.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABLAVO", "N", strTmp, dttTmp) Then
          strErr = oApp.Tr(Me, 128540464915326000, "Lavorazione inesistente.")
          Return False
        Else
          strDescr = strTmp
        End If
      Else
        strDescr = ""
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
  Public Overridable Function edCodtpro_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As DataTable = New DataTable
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldCals.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABTPRO", "N", strTmp, dttTmp) Then
          strErr = oApp.Tr(Me, 128540464953119532, "Promozione inesistente.")
          Return False
        Else
          strDescr = strTmp
        End If
      Else
        strDescr = ""
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

End Class
