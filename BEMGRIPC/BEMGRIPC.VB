Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGRIPC
  Inherits CLE__BASN

  Public oCldRipc As CLDMGRIPC

  Public bSariaf As Boolean

  Private Moduli_P As Integer = bsModMG Or bsModVE
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

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGRIPC"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldRipc = CType(MyBase.ocldBase, CLDMGRIPC)
    oCldRipc.Init(oApp)
    Return True
  End Function

  Public Overridable Function edCodice_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False

    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If
      bOut = oCldRipc.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLAVO", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128759699063668290, "Codice Lavorazione/Tipo costo da aggiornare: |'" & nCod.ToString & "'| inesistente")))
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

  Public Overridable Function Elabora(ByVal dTotale As Decimal, _
          ByVal nListino As Integer, ByVal nCodice As Integer, ByVal nRipart As Integer, _
          ByVal strDataElab As String, ByVal strDaData As String, ByVal strAData As String, _
          ByVal bAggarticoli As Boolean, ByVal strWhereArtico As String, _
          ByVal bAvan0 As Boolean, ByVal bAvan1 As Boolean, ByVal bAvan2 As Boolean) As Boolean
    Try
      Return Elabora(dTotale, nListino, nCodice, nRipart, strDataElab, strDaData, strAData, _
                     bAggarticoli, strWhereArtico, bAvan0, bAvan1, bAvan2, False)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function Elabora(ByVal dTotale As Decimal, _
          ByVal nListino As Integer, ByVal nCodice As Integer, ByVal nRipart As Integer, _
          ByVal strDataElab As String, ByVal strDaData As String, ByVal strAData As String, _
          ByVal bAggarticoli As Boolean, ByVal strWhereArtico As String, _
          ByVal bAvan0 As Boolean, ByVal bAvan1 As Boolean, ByVal bAvan2 As Boolean, _
          ByVal bAggungiAListino As Boolean) As Boolean
    Dim i As Integer = 0
    Dim y As Integer = 0
    Dim dQuant As Decimal = 0
    Dim dSumQta As Decimal = 0
    Dim dValore As Decimal = 0

    Dim dVenQuant As Decimal = 0
    Dim dImpQuant As Decimal = 0
    Dim dBudQuant As Decimal = 0
    Dim E As Decimal = 0
    Dim F As Decimal = 0
    Dim dPrezzo As Decimal = 0
    Dim strMsg As String = ""
    Dim dttArtico As New DataTable
    Dim dttArticolo As New DataTable
    Dim dttArticoloNull As New DataTable

    Try
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dTotale, nListino, nCodice, nRipart, strDataElab, strDaData, strAData, _
                     bAggarticoli, strWhereArtico, bAvan0, bAvan1, bAvan2, bAggungiAListino})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If

      '----------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128759940368922543, "Selezione articoli in corso...")))
      If Not oCldRipc.SelezionaArticoli(strDittaCorrente, 0, "N", strWhereArtico, dttArtico) Then
        strMsg = "Attenzione!" & vbCrLf & _
          "Non esistono articoli con le caratteristiche richieste." & vbCrLf & _
          "Elaborazione annullata."
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128759949196891293, strMsg)))
        Return False
      End If
      '----------------------------------------------------------------------------------
      dttArticolo.Columns.Add("articolo", GetType(String))
      dttArticolo.Columns.Add("quantita", GetType(Decimal))
      dttArticolo.Columns.Add("coeff", GetType(Integer))
      dttArticolo.Columns.Add("unmis", GetType(String))
      dttArticolo.Columns.Add("prezzo_medio", GetType(Decimal))
      dttArticolo.Columns("prezzo_medio").DefaultValue = 1D
      '----------------------------------------------------------------------------------
      dttArticoloNull.Columns.Add("articolo", GetType(String))
      dttArticoloNull.Columns.Add("quantita", GetType(Decimal))
      dttArticoloNull.Columns.Add("coeff", GetType(Integer))
      dttArticoloNull.Columns.Add("unmis", GetType(String))
      '----------------------------------------------------------------------------------
      For i = 0 To (dttArtico.Rows.Count - 1)
        '--------------------------------------------------------------------------------
        strMsg = "Elaborazione articolo '|" & dttArtico.Rows(i)!ar_codart.ToString & "|' in corso..."
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128759950861891293, strMsg)))
        '--------------------------------------------------------------------------------
        Select Case nRipart
          Case 0 '--- Quantità venduta
            If oCldRipc.DeterminaQuantita(strDittaCorrente, -1, strDaData, strAData, _
               "", _
               dttArtico.Rows(i)!ar_codart.ToString, _
               dttArtico.Rows(i)!ar_unmis.ToString, _
               NTSCDec(dttArtico.Rows(i)!ar_conver.ToString), bSariaf, dQuant) Then
              dSumQta = (dSumQta + (dQuant * NTSCInt(dttArtico.Rows(i)!ar_catlifo.ToString)))
            End If
          Case 1 '--- Quantità prodotta
            If oCldRipc.DeterminaQuantita(strDittaCorrente, 1, strDaData, strAData, _
              "", dttArtico.Rows(i)!ar_codart.ToString, _
              dttArtico.Rows(i)!ar_unmis.ToString, _
              NTSCDec(dttArtico.Rows(i)!ar_conver.ToString), bSariaf, dQuant) Then
              dSumQta = (dSumQta + (dQuant * NTSCInt(dttArtico.Rows(i)!ar_catlifo.ToString)))
            End If
          Case 2 '--- Quantità a budget
            If oCldRipc.QuantitaABudget(strDittaCorrente, "M", _
              dttArtico.Rows(i)!ar_codart.ToString, strDaData, strAData, _
              dttArtico.Rows(i)!ar_unmis.ToString, _
              NTSCDec(dttArtico.Rows(i)!ar_conver.ToString), bSariaf, dQuant) Then
              dSumQta = (dSumQta + (dQuant * NTSCInt(dttArtico.Rows(i)!ar_catlifo.ToString)))
            End If
          Case 3 '--- Quantità in stato di avanzamento
            '----------------------------------------------------------------------------
            '--- Prendo la quantità venduta fino alla data di elaborazione
            '----------------------------------------------------------------------------
            oCldRipc.DeterminaQuantita(strDittaCorrente, -1, strDaData, strDataElab, _
              "", _
              dttArtico.Rows(i)!ar_codart.ToString, _
              dttArtico.Rows(i)!ar_unmis.ToString, _
              NTSCDec(dttArtico.Rows(i)!ar_conver.ToString), bSariaf, dVenQuant)
            '----------------------------------------------------------------------------
            '--- Prendo gli IC inevasi dalla data elaborazione fino alla data finale
            '----------------------------------------------------------------------------
            oCldRipc.ICInevasi(strDittaCorrente, "R", _
              dttArtico.Rows(i)!ar_codart.ToString, "C", strDataElab, strAData, _
              dttArtico.Rows(i)!ar_unmis.ToString, _
              NTSCDec(dttArtico.Rows(i)!ar_conver.ToString), bSariaf, dImpQuant)
            '----------------------------------------------------------------------------
            '--- Considero la quantità a budget dal 1° giorno del mese corrente se il
            '--- giorno di elaborazione è inferiore al 10, altrimenti il 1° giorno
            '--- del mese succesivo, fino alla data finale di elaborazione
            '----------------------------------------------------------------------------
            oCldRipc.QuantitaABudgetAvanzamento(strDittaCorrente, "M", _
              dttArtico.Rows(i)!ar_codart.ToString, strDataElab, strAData, _
              dttArtico.Rows(i)!ar_unmis.ToString, _
              NTSCDec(dttArtico.Rows(i)!ar_conver.ToString), bSariaf, dBudQuant)
            '----------------------------------------------------------------------------
            '--- Prendo in considerazione gli OptionButton di maschera
            '----------------------------------------------------------------------------
            Select Case True
              Case bAvan0 '--- Venduto + Budget
                dQuant = (dVenQuant + dBudQuant)
              Case bAvan1 '--- Venduto + IC inevasi
                dQuant = (dVenQuant + dImpQuant)
              Case bAvan2 '--- Venduto + il più alto fra Budget e IC inevasi
                If dBudQuant > dImpQuant Then
                  dQuant = (dVenQuant + dBudQuant)
                Else
                  dQuant = (dVenQuant + dImpQuant)
                End If
            End Select
            dSumQta = (dSumQta + (dQuant * NTSCInt(dttArtico.Rows(i)!ar_catlifo.ToString)))
          Case 4 '--- Valore Carico
            oCldRipc.DeterminaQtaValoreCarico(strDittaCorrente, strDaData, strAData, _
                     dttArtico.Rows(i)!ar_codart.ToString, dQuant, dValore)

            dSumQta = (dSumQta + (dValore * NTSCInt(dttArtico.Rows(i)!ar_catlifo.ToString)))
        End Select
        '--------------------------------------------------------------------------------
        '--- Memorizzo i dati per articolo
        '--------------------------------------------------------------------------------
        Select Case nRipart
          Case 4
            If dQuant > 0 Then
              dttArticolo.Rows.Add(dttArticolo.NewRow())
              With dttArticolo.Rows(dttArticolo.Rows.Count - 1)
                !articolo = dttArtico.Rows(i)!ar_codart.ToString
                !quantita = dQuant
                !prezzo_medio = dValore / dQuant
                !coeff = NTSCInt(dttArtico.Rows(i)!ar_catlifo.ToString)
                !unmis = dttArtico.Rows(i)!ar_unmis.ToString
              End With
            End If
          Case Else
            If dQuant > 0 Then
              dttArticolo.Rows.Add(dttArticolo.NewRow())
              With dttArticolo.Rows(dttArticolo.Rows.Count - 1)
                !articolo = dttArtico.Rows(i)!ar_codart.ToString
                !quantita = dQuant
                !coeff = NTSCInt(dttArtico.Rows(i)!ar_catlifo.ToString)
                !unmis = dttArtico.Rows(i)!ar_unmis.ToString
              End With
            Else
              If bAggarticoli = True Then
                dttArticoloNull.Rows.Add(dttArticoloNull.NewRow())
                With dttArticoloNull.Rows(dttArticoloNull.Rows.Count - 1)
                  !articolo = dttArtico.Rows(i)!ar_codart.ToString
                  !quantita = dQuant
                  !coeff = NTSCInt(dttArtico.Rows(i)!ar_catlifo.ToString)
                  !unmis = dttArtico.Rows(i)!ar_unmis.ToString
                End With
              End If
            End If
        End Select
        '--------------------------------------------------------------------------------
        '--- Passo all'articolo successivo
        '--------------------------------------------------------------------------------
      Next
      '----------------------------------------------------------------------------------
      If dSumQta <> 0 Then
        E = (dTotale / dSumQta)
      Else
        strMsg = "Attenzione!" & vbCrLf & _
          "La quantità equivalente totale è pari a zero." & vbCrLf & _
          "Impossibile aggiornare i listini."
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128762274412675040, strMsg)))
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128762274562987540, "Pronto.")))
        Return False
      End If
      '----------------------------------------------------------------------------------
      dttArtico.Clear()
      dttArtico.Dispose()
      '----------------------------------------------------------------------------------
      '--- Aggiorna i listini MOVIMENTATI
      '----------------------------------------------------------------------------------
      For i = 0 To (dttArticolo.Rows.Count - 1)
        Dim dListinoValido As Decimal = 0D
        Dim dDaQuant As Decimal = 0D
        Dim dAQuant As Decimal = 0D
        Dim dttListinoValido As DataTable = Nothing
        Dim strArticolo As String = NTSCStr(dttArticolo.Rows(i)!articolo)
        Dim strUnmis As String = NTSCStr(dttArticolo.Rows(i)!unmis)
        Dim dPrezzoValido As Decimal = 0D

        Select Case nRipart
          Case 4
            dDaQuant = 0
            dAQuant = 0
          Case Else
            dDaQuant = NTSCDec(dttArticolo.Rows(i)!quantita.ToString)
            dAQuant = NTSCDec(dttArticolo.Rows(i)!quantita.ToString)
        End Select
        '--------------------------------------------------------------------------------
        strMsg = "Aggiornamento listino articolo |" & dttArticolo.Rows(i)!articolo.ToString & "| in corso..."
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128762276051112540, strMsg)))
        '--------------------------------------------------------------------------------
        F = E * NTSCInt(dttArticolo.Rows(i)!coeff) * NTSCDec(dttArticolo.Rows(i)!prezzo_medio)

        'Trova il listino valido
        If bAggungiAListino Then 'Il valore ripartito verrà aggiunto al listino valido
          dttListinoValido = oCldRipc.SelezionaListinoValido(strDittaCorrente, _
                             strArticolo, nCodice, 0, 0, 0, " ", nListino, _
                             strDataElab, dDaQuant, dAQuant, strUnmis)
          If dttListinoValido IsNot Nothing AndAlso dttListinoValido.Rows.Count > 0 Then
            dPrezzoValido = NTSCDec(dttListinoValido.Rows(0)!lc_prezzo)
          End If
        End If
        '--------------------------------------------------------------------------------
        '--- Aggiorno i listini di quell'articolo
        '--- Il listino precedente a questo punto non è più valido
        '--------------------------------------------------------------------------------
        If oCldRipc.SelezionaListinoAggiornato(strDittaCorrente, _
          strArticolo, nCodice, 0, 0, 0, " ", nListino, _
          strDataElab, dDaQuant, dAQuant, strUnmis) Then
          '------------------------------------------------------------------------------
          '--- Cancella il listino con data validità uguale alla data che devo assegnare
          '------------------------------------------------------------------------------
          If Not oCldRipc.CancellaListino(strDittaCorrente, _
            strArticolo, nCodice, 0, 0, 0, " ", nListino, _
            strDataElab, dDaQuant, strUnmis) Then Return False
          '------------------------------------------------------------------------------
        End If
        '--------------------------------------------------------------------------------
        If Not oCldRipc.AggiornaListino(strDittaCorrente, _
          strArticolo, nCodice, 0, 0, 0, " ", nListino, _
          strDataElab, dDaQuant, strUnmis) Then Return False
        '--------------------------------------------------------------------------------
        If Not oCldRipc.InserisciListino(strDittaCorrente, _
          strArticolo, nCodice, 0, 0, 0, nListino, _
          strDataElab, " ", F + dPrezzoValido, _
          IntSetDate("31/12/2099"), 0, 9999999999, 1, _
          strUnmis, "", "N", 0, _
          DateTime.Now.ToShortDateString, " ") Then Return True
        '--------------------------------------------------------------------------------
      Next
      '----------------------------------------------------------------------------------
      '--- Aggiorna i listini degli articoli NON MOVIMENTATI 
      '--- nel caso in cui la base di ripartizione è diversa da 'Valore Carico'
      '----------------------------------------------------------------------------------
      If nRipart <> 4 AndAlso bAggarticoli = True Then
        For i = 0 To (dttArticoloNull.Rows.Count - 1)
          '------------------------------------------------------------------------------
          strMsg = "Aggiornamento listino articolo |" & dttArticoloNull.Rows(i)!articolo.ToString & "| in corso..."
          ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128762473409413290, strMsg)))
          '------------------------------------------------------------------------------
          For y = 0 To (dttArticolo.Rows.Count - 1)
            If NTSCInt(dttArticolo.Rows(y)!coeff.ToString) = NTSCInt(dttArticoloNull.Rows(i)!coeff.ToString) Then
              '--------------------------------------------------------------------------
              '--- Cancella l'eventuale listino esistente
              '--------------------------------------------------------------------------
              If Not oCldRipc.CancellaListino(strDittaCorrente, _
                dttArticoloNull.Rows(i)!articolo.ToString, nCodice, 0, 0, 0, " ", _
                nListino, strDataElab, _
                NTSCDec(dttArticoloNull.Rows(i)!quantita.ToString), _
                dttArticoloNull.Rows(i)!unmis.ToString) Then Return False
              '--------------------------------------------------------------------------
              '--- Aggiorna la data di scadenza dei listini 
              '--- con data aggiornamento inferiore
              '--------------------------------------------------------------------------
              If Not oCldRipc.AggiornaListino(strDittaCorrente, _
                dttArticoloNull.Rows(i)!articolo.ToString, nCodice, 0, 0, 0, " ", _
                nListino, strDataElab, _
                NTSCDec(dttArticoloNull.Rows(i)!quantita.ToString), _
                dttArticoloNull.Rows(i)!unmis.ToString) Then Return False
              '--------------------------------------------------------------------------
              '--- Inserisce il listino aggiornato
              '--------------------------------------------------------------------------
              If oCldRipc.SelezionaListinoNonAggiornato(strDittaCorrente, _
                dttArticoloNull.Rows(i)!articolo.ToString, nCodice, 0, 0, 0, " ", _
                nListino, strDataElab, IntSetDate("31/12/2099"), 0, 9999999999, _
                dttArticoloNull.Rows(i)!unmis.ToString, dPrezzo) Then
                If Not oCldRipc.InserisciListino(strDittaCorrente, _
                  dttArticoloNull.Rows(i)!articolo.ToString, nCodice, 0, 0, 0, _
                  nListino, strDataElab, " ", dPrezzo, IntSetDate("31/12/2099"), _
                  0, 9999999999, 1, dttArticoloNull.Rows(i)!unmis.ToString, _
                  "", "N", 0, DateTime.Now.ToShortDateString, " ") Then Return False
              End If
              '--------------------------------------------------------------------------
              Exit For
              '--------------------------------------------------------------------------
            End If
          Next
        Next
      End If
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      '----------------------------------------------------------------------------------
      dttArtico.Clear() : dttArtico.Dispose()
      dttArticolo.Clear() : dttArticoloNull.Dispose()
      '----------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128759943885172543, "Pronto.")))
      '----------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RitornaListino() As Integer
    Dim dttTABPEAC As New DataTable

    Try
      oCldRipc.ValCodiceDb("1", strDittaCorrente, "TABPEAC", "N", "", dttTABPEAC)
      If dttTABPEAC.Rows.Count = 0 Then Return 0
      Return NTSCInt(dttTABPEAC.Rows(0)!tb_listcstd.ToString)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    Finally
      dttTABPEAC.Clear()
      dttTABPEAC.Dispose()
    End Try
  End Function
End Class