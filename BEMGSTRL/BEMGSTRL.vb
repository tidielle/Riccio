Imports System.Data
Imports NTSInformatica.CLN__STD


'PER UN ESEMPIO DI CLASSE EREDITATA CON DAL SPECIFICO VEDI BE__SOTC, BECGPRIV (con cambio ditta), BEVECOVP

Public Class CLEMGSTRL
  Inherits CLE__BASN

  Public oCldStrl As CLDMGSTRL
  Public dttTabanaz As New DataTable
  Public lIITTinvent As Integer = 0
  Public lIITTinvent2 As Integer = 0
  Public lIITTinventTC As Integer = 0
  Public bModTCO As Boolean = False
  Public bCheckArtlif As Boolean = False

  Private Moduli_P As Integer = bsModMG
  Private ModuliExt_P As Integer = bsModExtMGE
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGSTRL"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldStrl = CType(MyBase.ocldBase, CLDMGSTRL)
    oCldStrl.Init(oApp)
    Return True
  End Function

  Public Overridable Function LeggiDatiDitta(ByVal strDitta As String) As Boolean
    Try
      oCldStrl.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttTabanaz)
      strDittaCorrente = strDitta

      lIITTinvent = oCldStrl.GetTblInstId("TTINVENT", False)
      lIITTinvent2 = oCldStrl.GetTblInstId("TTINVEN2", False)
      If bModTCO = True Then lIITTinventTC = oCldStrl.GetTblInstId("TTINVENTTC", False)

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

  Public Overridable Function edNegozio_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldStrl.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABSTAB", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129865746163169853, "Codice stabilimento/filiale/negozio |'" & nCod.ToString & "'| inesistente")))
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
    End Try
  End Function
  Public Overridable Function edMagaz_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldStrl.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABMAGA", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127965883462031250, "Codice magazzino |'" & nCod.ToString & "'| inesistente")))
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
    End Try
  End Function

  Public Overridable Function edCodlsar_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      strDescr = ""
      '--------------------------------------------------------------------------------------------------------------
      If nCod = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      bOut = oCldStrl.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLSAR", "N", strDescr)
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

  Public Overridable Function TestPreElabora(ByVal nTipoElab As Integer, ByVal nValoriz As Integer, _
                                    ByVal nGiacenze As Integer, ByVal nMagazzino As Integer, _
                                    ByVal strDtelab As String, ByVal bInvFinale As Boolean, _
                                    ByVal bUsaCostiglob As Boolean, ByVal bLifoAnniprec As Boolean, _
                                    ByVal bUsaSoloListino As Boolean, ByVal bUsaCostiforn As Boolean, _
                                    ByVal bSalvaListino As Boolean, ByVal nListinoNew As Integer, _
                                    ByVal strDtListinoNew As String, ByVal bListinoNewSave0 As Boolean, _
                                    ByVal bTettaglioTCO As Boolean, ByVal strArticoWhere As String) As Boolean
    Try
      'obsoleta
      Return TestPreElabora(nTipoElab, nValoriz, nGiacenze, nMagazzino, strDtelab, bInvFinale, _
                           bUsaCostiglob, bLifoAnniprec, bUsaSoloListino, bUsaCostiforn, _
                           bSalvaListino, nListinoNew, strDtListinoNew, bListinoNewSave0, _
                           bTettaglioTCO, strArticoWhere, False)
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
  Public Overridable Function TestPreElabora(ByVal nTipoElab As Integer, ByVal nValoriz As Integer, _
                                      ByVal nGiacenze As Integer, ByVal nMagazzino As Integer, _
                                      ByVal strDtelab As String, ByVal bInvFinale As Boolean, _
                                      ByVal bUsaCostiglob As Boolean, ByVal bLifoAnniprec As Boolean, _
                                      ByVal bUsaSoloListino As Boolean, ByVal bUsaCostiforn As Boolean, _
                                      ByVal bSalvaListino As Boolean, ByVal nListinoNew As Integer, _
                                      ByVal strDtListinoNew As String, ByVal bListinoNewSave0 As Boolean, _
                                      ByVal bTettaglioTCO As Boolean, ByVal strArticoWhere As String, _
                                      ByVal bTcoEsitTaglia As Boolean) As Boolean
    'nTipoElab: 0 = attuale, 1 = a data, 2 = a data ultimo aggiornamento
    'nValoriz:  0 = ultimo costo, -1 = lifo, -2 = costo medio dell'anno, -3 = costo medio globale, -4 = fifo, -5 = ultimo costo con oneri accessori, -6 = come da inventario finale, > 0 listino indicato
    'nGiacenze: 0 = > 0,     1 = < 0,    2 = <> 0,      3 = tutte, 4 = '= 0'
    'nMagazzino: 0 = merce propria, > 0 < 1000000 un magazzino (quelli indicato), > 1000000 un negozio, -1 = merce altrui, -2 = tutti i magazzini

    Dim strMagaz As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nTipoElab, nValoriz, nGiacenze, nMagazzino, strDtelab, bInvFinale, _
                           bUsaCostiglob, bLifoAnniprec, bUsaSoloListino, bUsaCostiforn, _
                           bSalvaListino, nListinoNew, strDtListinoNew, bListinoNewSave0, _
                           bTettaglioTCO, strArticoWhere, bTcoEsitTaglia})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '-------------------------
      'elaborazione a data non può essere più vecchia di data ultima elaborazione di tabanaz
      If nTipoElab = 1 Then
        If NTSCDate(strDtelab) <= NTSCDate(dttTabanaz.Rows(0)!tb_dtulap.ToString) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128497827112202000, "La data Elaborazione deve essere maggiore della data 'Ultimo Aggiornamento Progressivi' in 'Anagrafica Azienda' (|" & NTSCDate(dttTabanaz.Rows(0)!tb_dtulap.ToString).ToShortDateString & "|)")))
          Return False
        End If
      End If

      '-------------------------
      'se salvo listino deve esserci numero di listino
      If bSalvaListino Then
        If nListinoNew < 1 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128497828771886000, "Il numero di listino da salvare deve essere maggiore di 0")))
          Return False
        End If
      End If

      '-------------------------
      'se è stato passato uno stabilimento, verifico se ha magazzini associati
      If nMagazzino > 1000000 Then
        If Not oCldStrl.GetMagazFromStab(strDittaCorrente, nMagazzino - 1000000, strMagaz) Then Return False
        If strMagaz.Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129865775771541859, "Allo stabilimento indicato non sono associati magazzini.")))
          Return False
        End If
      End If

      If bTettaglioTCO = False And bTcoEsitTaglia Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130772767549315717, "Con selezionato 'TCO usa giacenza per taglia' deve essere selezionato anche 'Stampa dettaglio quantità per taglie'")))
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
    End Try
  End Function

  Public Overridable Function Elabora(ByVal nTipoElab As Integer, ByVal nValoriz As Integer, _
                                    ByVal nGiacenze As Integer, ByVal nMagazzino As Integer, _
                                    ByVal strDtelab As String, ByVal bInvFinale As Boolean, _
                                    ByVal bUsaCostiglob As Boolean, ByVal bLifoAnniprec As Boolean, _
                                    ByVal bUsaSoloListino As Boolean, ByVal bUsaCostiforn As Boolean, _
                                    ByVal bSalvaListino As Boolean, ByVal nListinoNew As Integer, _
                                    ByVal strDtListinoNew As String, ByVal bListinoNewSave0 As Boolean, _
                                    ByVal bDettaglioTCO As Boolean, ByVal strArticoWhere As String, _
                                    ByVal bEscludiArticoliNonMovimentati As Boolean, _
                                    ByVal bGeneraListaSelezionata As Boolean, _
                                    ByVal nCodlsar As Integer, ByVal strDeslsar As String, _
                                    ByRef dTotalemagazStoricoLifo As Decimal) As Boolean
    Try
      'obsoleta
      Return Elabora(nTipoElab, nValoriz, nGiacenze, nMagazzino, strDtelab, bInvFinale, bUsaCostiglob, bLifoAnniprec, _
                     bUsaSoloListino, bUsaCostiforn, bSalvaListino, nListinoNew, strDtListinoNew, bListinoNewSave0, _
                     bDettaglioTCO, strArticoWhere, bEscludiArticoliNonMovimentati, bGeneraListaSelezionata, _
                     nCodlsar, strDeslsar, dTotalemagazStoricoLifo, False)

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
  Public Overridable Function Elabora(ByVal nTipoElab As Integer, ByVal nValoriz As Integer, _
                                    ByVal nGiacenze As Integer, ByVal nMagazzino As Integer, _
                                    ByVal strDtelab As String, ByVal bInvFinale As Boolean, _
                                    ByVal bUsaCostiglob As Boolean, ByVal bLifoAnniprec As Boolean, _
                                    ByVal bUsaSoloListino As Boolean, ByVal bUsaCostiforn As Boolean, _
                                    ByVal bSalvaListino As Boolean, ByVal nListinoNew As Integer, _
                                    ByVal strDtListinoNew As String, ByVal bListinoNewSave0 As Boolean, _
                                    ByVal bDettaglioTCO As Boolean, ByVal strArticoWhere As String, _
                                    ByVal bEscludiArticoliNonMovimentati As Boolean, _
                                    ByVal bGeneraListaSelezionata As Boolean, _
                                    ByVal nCodlsar As Integer, ByVal strDeslsar As String, _
                                    ByRef dTotalemagazStoricoLifo As Decimal, _
                                    ByVal bTcoEsitTaglia As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nTipoElab, nValoriz, nGiacenze, nMagazzino, strDtelab, bInvFinale, bUsaCostiglob, bLifoAnniprec, _
                     bUsaSoloListino, bUsaCostiforn, bSalvaListino, nListinoNew, strDtListinoNew, bListinoNewSave0, _
                     bDettaglioTCO, strArticoWhere, bEscludiArticoliNonMovimentati, bGeneraListaSelezionata, _
                     nCodlsar, strDeslsar, dTotalemagazStoricoLifo, bTcoEsitTaglia})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dTotalemagazStoricoLifo = CType(oIn(20), Decimal)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      Return Elabora(nTipoElab, nValoriz, nGiacenze, nMagazzino, strDtelab, bInvFinale, bUsaCostiglob, bLifoAnniprec, _
                     bUsaSoloListino, bUsaCostiforn, bSalvaListino, nListinoNew, strDtListinoNew, bListinoNewSave0, _
                     bDettaglioTCO, strArticoWhere, bEscludiArticoliNonMovimentati, bGeneraListaSelezionata, _
                     nCodlsar, strDeslsar, dTotalemagazStoricoLifo, bTcoEsitTaglia, False)
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
  Public Overridable Function Elabora(ByVal nTipoElab As Integer, ByVal nValoriz As Integer, _
                                      ByVal nGiacenze As Integer, ByVal nMagazzino As Integer, _
                                      ByVal strDtelab As String, ByVal bInvFinale As Boolean, _
                                      ByVal bUsaCostiglob As Boolean, ByVal bLifoAnniprec As Boolean, _
                                      ByVal bUsaSoloListino As Boolean, ByVal bUsaCostiforn As Boolean, _
                                      ByVal bSalvaListino As Boolean, ByVal nListinoNew As Integer, _
                                      ByVal strDtListinoNew As String, ByVal bListinoNewSave0 As Boolean, _
                                      ByVal bDettaglioTCO As Boolean, ByVal strArticoWhere As String, _
                                      ByVal bEscludiArticoliNonMovimentati As Boolean, _
                                      ByVal bGeneraListaSelezionata As Boolean, _
                                      ByVal nCodlsar As Integer, ByVal strDeslsar As String, _
                                      ByRef dTotalemagazStoricoLifo As Decimal, _
                                      ByVal bTcoEsitTaglia As Boolean, ByVal bQta0 As Boolean) As Boolean
    '----------------------------
    'nTipoElab: 0 = attuale, 1 = a data, 2 = a data ultimo aggiornamento
    'nValoriz:  0 = ultimo costo, -1 = lifo, -2 = costo medio dell'anno, -3 = costo medio globale, -4 = fifo, -5 = ultimo costo con oneri accessori, -6 = come da inventario finale, > 0 listino indicato
    'nGiacenze: 0 = > 0,     1 = < 0,    2 = <> 0,      3 = tutte, 4 = '= 0'
    'nMagazzino: 0 = merce propria, > 0 < 1000000 un magazzino (quelli indicato), > 1000000 un negozio, -1 = merce altrui, -2 = tutti i magazzini
    Dim dsTmp As New DataSet
    Dim i As Integer = 0
    Dim dValore As Decimal = 0
    Dim iMagaz As Integer = 0
    Dim strErr As String = ""
    Dim strTipoValoriz As String = ""
    Dim strDtcomp As String = ""
    Dim bCheckArtlif As Boolean = False
    Dim bTTINVENTVuoto As Boolean = False
    Dim dttArti As New DataTable

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nTipoElab, nValoriz, nGiacenze, nMagazzino, strDtelab, bInvFinale, bUsaCostiglob, bLifoAnniprec, _
                     bUsaSoloListino, bUsaCostiforn, bSalvaListino, nListinoNew, strDtListinoNew, bListinoNewSave0, _
                     bDettaglioTCO, strArticoWhere, bEscludiArticoliNonMovimentati, bGeneraListaSelezionata, _
                     nCodlsar, strDeslsar, dTotalemagazStoricoLifo, bTcoEsitTaglia, bQta0})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dTotalemagazStoricoLifo = CType(oIn(20), Decimal)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------


      LogStart(oApp.Tr(Me, 130365900515725272, "Stampa Inventario di Magazzino"))

      '----------------------------
      Select Case nValoriz
        Case 0 : strTipoValoriz = "U"
        Case -1 : strTipoValoriz = "L"
        Case -2 : strTipoValoriz = "M"
        Case -3 : strTipoValoriz = "G"
        Case -4 : strTipoValoriz = "F"
        Case -5 : strTipoValoriz = "Z"
        Case -6 : strTipoValoriz = "" 'non possibile (come da inv. finale)
        Case Else : strTipoValoriz = nValoriz.ToString
      End Select

      If bModTCO = False Then
        bDettaglioTCO = False
        bTcoEsitTaglia = False
      End If

      If nTipoElab = 0 Then strDtelab = DateTime.Now.ToShortDateString
      If nTipoElab = 2 Then strDtelab = NTSCDate(dttTabanaz.Rows(0)!tb_dtulap).ToShortDateString

      '----------------------------
      If Not TestPreElabora(nTipoElab, nValoriz, nGiacenze, nMagazzino, strDtelab, bInvFinale, _
                            bUsaCostiglob, bLifoAnniprec, bUsaSoloListino, bUsaCostiforn, _
                            bSalvaListino, nListinoNew, strDtListinoNew, bListinoNewSave0, _
                            bDettaglioTCO, strArticoWhere, bTcoEsitTaglia) Then Return False

      '-------------------------
      'reset instid
      ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128498074754958728, "Pulizia tabelle temporanee ...")))

      oCldStrl.ResetTblInstId("TTINVENT", False, lIITTinvent)
      oCldStrl.ResetTblInstId("TTINVENT", False, lIITTinvent * -1)
      oCldStrl.ResetTblInstId("TTINVEN2", False, lIITTinvent2)
      oCldStrl.ResetTblInstId("TTINVEN2", False, lIITTinvent2 * -1)
      If bModTCO Then
        oCldStrl.ResetTblInstId("TTINVENTTC", False, lIITTinventTC)
        oCldStrl.ResetTblInstId("TTINVENTTC", False, lIITTinventTC * -1)
      End If

      '-------------------------
      'riempio il temporaneo con gli articoli da trattare
      'in caso di elaborazione a data gli articoli potrebbero essere di più ... 
      'li rimuoverò dopo
      ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128498074740138728, "Determinazione articoli da elaborare ...")))
      If Not oCldStrl.RiempiTTinventArticoli(strDittaCorrente, strArticoWhere, nMagazzino, lIITTinvent, CBool(IIf(nTipoElab = 2, True, False))) Then Return False

      '-------------------------
      'calcolo l'esistenza iniziale ed il suo valore, l'incremento/decremento e l'esist finale
      ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128498074726410728, "Calcolo esistenza ...")))
      Select Case nValoriz
        Case -6
          'come da inventario finale: può essere solo a data ultimo agg
          If Not oCldStrl.CalcolaEsistenza(strDittaCorrente, nMagazzino, lIITTinvent, True, False) Then Return False
          If bDettaglioTCO Then
            ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128818689528562271, "Calcolo esistenza per taglie ...")))
            oCldStrl.CalcolaEsistenzaDettaglioTCO(strDittaCorrente, nMagazzino, lIITTinvent, True, False, lIITTinventTC)
          End If

        Case Else
          'ultimo costo/ costo medio dell'anno/ costo medio globale/ fifo/ lifo/ ultimo costo con oneri accessori/ listino
          ': può essere attuale/ a data/ a data ultimo agg
          'LIFO: SEMPRE E SOLO MERCE PROPRIA
          If nTipoElab = 1 Then
            'calcolo esistenze a data: 
            'è come una elaborazione 'a data ultimo agg', poi devo aggiungere i movimenti dalla data ultimo agg alla data fine elab
            If Not oCldStrl.CalcolaEsistenza(strDittaCorrente, nMagazzino, lIITTinvent, True, True) Then Return False
            If bDettaglioTCO Then
              ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128818689457468066, "Calcolo esistenza per taglie ...")))
              oCldStrl.CalcolaEsistenzaDettaglioTCO(strDittaCorrente, nMagazzino, lIITTinvent, True, True, lIITTinventTC)
            End If

            'ora aggiungo i movimenti 
            ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128818689340904820, "Calcolo esistenza fase 2 ...")))
            If Not oCldStrl.CalcolaEsistenzaECostiAData(strDittaCorrente, nMagazzino, lIITTinvent, _
                                                  NTSCDate(dttTabanaz.Rows(0)!tb_dtulap.ToString).AddDays(1).ToShortDateString, _
                                                  strDtelab, bUsaCostiglob, nValoriz) Then Return False
            If bDettaglioTCO Then
              ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128509397354689217, "Calcolo esistenza per taglie fase 2 ...")))
              oCldStrl.CalcolaEsistenzaECostiADataDettaglioTCO(strDittaCorrente, nMagazzino, lIITTinvent, _
                                                                NTSCDate(dttTabanaz.Rows(0)!tb_dtulap.ToString).AddDays(1).ToShortDateString, _
                                                                strDtelab, lIITTinventTC)
            End If

          Else
            If Not oCldStrl.CalcolaEsistenza(strDittaCorrente, nMagazzino, lIITTinvent, CBool(IIf(nTipoElab = 2, True, False)), False) Then Return False
            If bDettaglioTCO Then
              ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128818689496374565, "Calcolo esistenza per taglie ...")))
              oCldStrl.CalcolaEsistenzaDettaglioTCO(strDittaCorrente, nMagazzino, lIITTinvent, CBool(IIf(nTipoElab = 2, True, False)), False, lIITTinventTC)
            End If
          End If

      End Select

      '-------------------------
      'in base nGiacenze tolgo quelle che non servono
      If nGiacenze <> 3 Then
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128498074984434728, "Rimozione articoli non richiesti ...")))
        If Not oCldStrl.RimuoviArticoliGiacenza(strDittaCorrente, lIITTinvent, nGiacenze, bTcoEsitTaglia, lIITTinventTC) Then Return False
        If bDettaglioTCO Then oCldStrl.RimuoviArticoliGiacenzaTCO(strDittaCorrente, lIITTinvent, nGiacenze, lIITTinventTC)
      Else
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128504851779930000, "Aggiunta articoli non movimentati ...")))
        If Not oCldStrl.AggiungiArticoliGiacenza(strDittaCorrente, lIITTinvent, strArticoWhere) Then Return False
        If bDettaglioTCO Then oCldStrl.AggiungiArticoliGiacenzaTCO(strDittaCorrente, lIITTinvent, lIITTinventTC, strArticoWhere)
      End If

      'determino il costo ed il valore finale
      ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128498075161026728, "Valorizzazione magazzino ...")))
      Select Case nValoriz
        Case 0  'ultimo costo
          If Not oCldStrl.CalcolaCostoUltimoCosto(strDittaCorrente, lIITTinvent, nTipoElab, nMagazzino, strDtelab, _
                                                  False, bUsaCostiglob, _
                                                  NTSCDate(dttTabanaz.Rows(0)!tb_dtulap.ToString).AddDays(1).ToShortDateString) Then Return False
          If Not oCldStrl.AggiungiCostoInventariale(strDittaCorrente, lIITTinvent, nValoriz, bUsaCostiglob) Then Return False

        Case -1 'lifo (può essere solo per 'magaz merce propria')
          ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128504999285614000, "Valorizzazione magazzino LIFO in corso ...")))
          'determino gli articoli da trattare
          If Not oCldStrl.CalcolaCostoLifo(strDittaCorrente, lIITTinvent, CBool(IIf(nTipoElab = 1, True, False)), dttArti) Then Return False
          For i = 0 To dttArti.Rows.Count - 1
            'loop su ogni articolo
            If CInt(i / 50) * 50 = i Then
              ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128509396167239217, "Determinazione storico LIFO articolo |" & i.ToString & "| di |" & dttArti.Rows.Count.ToString & "| in corso ...")))
            End If
            oCldStrl.CalcolaCostoLifoFase2(strDittaCorrente, lIITTinvent, CBool(IIf(nTipoElab = 1, True, False)), dttArti.Rows(i))
          Next
          oCldStrl.CalcolaCostoLifoFase3(strDittaCorrente, lIITTinvent)

          If bLifoAnniprec Then
            ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128505019834942000, "Determinazione storico LIFO in corso ...")))
            bCheckArtlif = CBool(oCldStrl.GetSettingBus("BSMGSTRL", "OPZIONI", ".", "CheckArtlif", "0", " ", "0"))
            strErr = ""
            If Not oCldStrl.CaricaDettaglioLifo(strDittaCorrente, lIITTinvent, lIITTinvent2, bCheckArtlif, strErr, dTotalemagazStoricoLifo) Then Return False
            If strErr <> "" Then LogWrite(strErr, True) 'ThrowRemoteEvent(New NTSEventArgs("", strErr))
          End If

        Case -2 'costo medio dell'anno
          If Not oCldStrl.CalcolaCostoMedio(strDittaCorrente, lIITTinvent, nTipoElab, nMagazzino, strDtelab, _
                                            False, bUsaCostiglob, NTSCDate(dttTabanaz.Rows(0)!tb_dtulap.ToString).AddDays(1).ToShortDateString, strDtelab) Then Return False
          If Not oCldStrl.AggiungiCostoInventariale(strDittaCorrente, lIITTinvent, nValoriz, bUsaCostiglob) Then Return False

        Case -3 'costo medio globale
          If Not oCldStrl.CalcolaCostoMedio(strDittaCorrente, lIITTinvent, nTipoElab, nMagazzino, strDtelab, _
                                            True, bUsaCostiglob, NTSCDate(dttTabanaz.Rows(0)!tb_dtulap.ToString).AddDays(1).ToShortDateString, strDtelab) Then Return False
          If Not oCldStrl.AggiungiCostoInventariale(strDittaCorrente, lIITTinvent, nValoriz, bUsaCostiglob) Then Return False

        Case -4 'fifo
          If Not oCldStrl.GetDataTtinvent(strDittaCorrente, lIITTinvent, dsTmp) Then Return False
          iMagaz = -1
          If nTipoElab = 1 Then      'solo nell'elaborazione 'A DATA' ...
            Select Case nMagazzino
              Case 0 : iMagaz = -1
              Case -1 : iMagaz = -2
              Case -2 : iMagaz = 0
              Case Else : iMagaz = nMagazzino
            End Select
          End If

          For i = 0 To dsTmp.Tables("TTINVENT").Rows.Count - 1
            With dsTmp.Tables("TTINVENT").Rows(i)
              !in_val = "I"
              If NTSCDec(!in_esist) > 0 Then
                If CInt(i / 50) * 50 = i Then
                  ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128504200847428000, "Calcolo FIFO articolo |" & i.ToString & "| di |" & dsTmp.Tables("TTINVENT").Rows.Count.ToString & "| in corso ...")))
                End If
                dValore = CType(oCleComm, CLELBMENU).CercaPrezzoFifo(strDittaCorrente, NTSCStr(!in_codart), _
                                                                              NTSCInt(!in_fase), iMagaz, _
                                                                              IIf(nTipoElab <> 0, strDtelab, "").ToString, _
                                                                              NTSCDec(!in_esist), _
                                                                              0, dValore, strErr)
                If strErr <> "" Then LogWrite(strErr, True) 'ThrowRemoteEvent(New NTSEventArgs("", strErr))

                If dValore = 0 Then
                  If ArrDbl(NTSCDec(!in_giaini), 3) <> 0 Then
                    dValore = NTSCDec(!in_vgiaini) / ArrDbl(NTSCDec(!in_giaini), 3)
                  End If
                Else
                  !in_val = "F"
                End If
                !in_costo = ArrDbl(dValore, oCldStrl.TrovaNdecSuPrzUn(0))
              End If
            End With
          Next

          'riverso i dati nel db
          ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128504201024488000, "Scrittura FIFO su temporaneo in corso ...")))
          If Not oCldStrl.CalcolaCostoFifo(strDittaCorrente, dsTmp) Then Return False
          dsTmp.Clear()

        Case -5 'ultimo costo con oneri accessori
          If Not oCldStrl.CalcolaCostoUltimoCosto(strDittaCorrente, lIITTinvent, nTipoElab, nMagazzino, strDtelab, _
                                                  True, bUsaCostiglob, _
                                                  NTSCDate(dttTabanaz.Rows(0)!tb_dtulap.ToString).AddDays(1).ToShortDateString) Then Return False
          If Not oCldStrl.AggiungiCostoInventariale(strDittaCorrente, lIITTinvent, nValoriz, bUsaCostiglob) Then Return False

        Case -6 'come da inventario finale (può essere solo 'a data ultimo agg' e 'magaz merce propria')
          If Not oCldStrl.CalcolaCostoInventarioFinale(strDittaCorrente, lIITTinvent) Then Return False

        Case Else   'listino indicato
          If Not oCldStrl.CalcolaCostoListino(strDittaCorrente, lIITTinvent, nValoriz, strDtelab, bUsaCostiforn, CBool(IIf(nTipoElab = 1, True, False))) Then Return False

          If bUsaSoloListino = False Then
            'aggiungo i costi inventariali
            If Not oCldStrl.AggiungiCostoInventariale(strDittaCorrente, lIITTinvent, nValoriz, False) Then Return False
          End If
      End Select

      '-------------------------
      'aggiorno valore (solo se non è 'come da inventario finale' o 'lifo': non serve)
      If nValoriz <> -6 And nValoriz <> -1 Then
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128502449354862603, "Aggiornamento valore esistenza ...")))
        If Not oCldStrl.AggiornaValore(strDittaCorrente, lIITTinvent, CBool(IIf(nValoriz = -6 Or nTipoElab = 2, True, False))) Then Return False
      End If

      '-------------------------
      'per gli articoli con esistenza <= 0 imposto il valore esistenza = 0
      If CBool(oCldStrl.GetSettingBus("BSMGSTRL", "OPZIONI", ".", "AzzeraValoreEsistenzaNegativa", "-1", " ", "-1")) Then
        If Not oCldStrl.AzzeraValoreEsistenzaNegativa(strDittaCorrente, lIITTinvent) Then Return False
      End If

      '-------------------------
      'segnalo se sono presenti articoli con valore esistenza finale < 0
      If oCldStrl.CheckValoreEsistenzaMinoreZero(strDittaCorrente, lIITTinvent) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128886699225384000, "Attenzione: sono presenti degli articoli con valore esistenza finale minore di zero. Per identificarli si consiglia di eseguire una elaborazione su griglia e successivamente ordinare la colonna 'valore giacenza finale' in ordine crescente")))
      End If

      '-------------------------
      'se inventario finale aggiorno artdefx
      If bInvFinale Then
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128504829614674000, "Aggiornamento progressivi defin. articoli (costo / valore esist.) per 'INVENTARIO FINALE' ...")))
        If Not oCldStrl.InventarioFinale(strDittaCorrente, lIITTinvent, strTipoValoriz) Then Return False
      End If

      '--------------------------------------------------------------------------------------------------------------
      If bEscludiArticoliNonMovimentati Then
        oCldStrl.EscludiArticoliNonMovimentati(strDittaCorrente, lIITTinvent, lIITTinvent2, lIITTinventTC)
      End If
      '--------------------------------------------------------------------------------------------------------------
      'aggiorno il listino
      If bSalvaListino Then
        If Not oCldStrl.GetDataTtinvent(strDittaCorrente, lIITTinvent, dsTmp) Then Return False
        For i = 0 To dsTmp.Tables("TTINVENT").Rows.Count - 1
          With dsTmp.Tables("TTINVENT").Rows(i)
            If NTSCDec(!in_costo) <> 0 Or bListinoNewSave0 = True Then
              ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128498075567874728, "Aggiornamento listino art.|'" & !in_codart.ToString & "'| ...")))
              If Not CType(oCleComm, CLELBMENU).SalvaListino(strDittaCorrente, NTSCStr(!in_codart), NTSCInt(!in_fase), _
                                                              NTSCStr(!in_unmis), " ", nListinoNew, 0, strDtListinoNew, _
                                                              0, 0, 0, NTSCDec(!in_costo), 0) Then Return False
            End If
          End With
        Next
        dsTmp.Clear()
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bGeneraListaSelezionata Then
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 129056230990314175, "Creazione Lista Selezionata Articoli in corso...")))
        Select Case nTipoElab
          Case 1 : strDtcomp = strDtelab
          Case Else : strDtcomp = DateTime.Now.ToShortDateString
        End Select
        If oCldStrl.GeneraListaSelezionata(strDittaCorrente, nCodlsar, strDeslsar, lIITTinvent, lIITTinventTC, _
                                           nMagazzino, strDtcomp, bTTINVENTVuoto, bQta0) Then
          If bTTINVENTVuoto = True Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129056229711636250, "Attenzione!" & vbCrLf & _
              "Non esistono articoli con giacenze diverse da zero, con le caratteristiche richieste." & vbCrLf & _
              "Pertanto non è stato possibile creare la Lista Selezionata Articoli.")))
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
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
      LogStop()
    End Try
  End Function

  Public Overridable Function CaricaTtinvent(ByRef dsOut As DataSet) As Boolean
    '---------------------------------
    'leggo ttinvent per la stampa su griglia
    Try

      Return oCldStrl.LeggiTtinvent(strDittaCorrente, lIITTinvent, dsOut)

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

  Public Overridable Function CountTtinvent(ByRef nRec As Integer, ByRef dTot As Decimal) As Boolean
    '---------------------------------
    'leggo ttinvent per la stampa su griglia: numero di record e totale inventario
    Try

      Return oCldStrl.CountTtinvent(strDittaCorrente, lIITTinvent, nRec, dTot)

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

  Public Overridable Function DeterminaCodiceListaSelezionata() As Integer
    Try
      Return oCldStrl.DeterminaCodiceListaSelezionata(strDittaCorrente)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function IsTablsarDeletable(ByVal nCod As Integer) As Boolean
    Dim strDescr As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If nCod = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If oCldStrl.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLSAR", "N", strDescr, dttTmp) Then
        Select Case NTSCStr(dttTmp.Rows(0)!tb_status)
          Case "P", "S"
            Dim strPrel As String = oApp.Tr(Me, 129077798637289994, "Prelevato")
            Dim strSosp As String = oApp.Tr(Me, 129077798911521029, "Sospeso")

            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129055440557892400, "Attenzione!" & vbCrLf & _
              "La Lista Selezionata Articoli |'" & nCod.ToString & "'| è già esistente" & vbCrLf & _
              "e possiede lo status di '|" & IIf(NTSCStr(dttTmp.Rows(0)!tb_status) = "P", strPrel, strSosp).ToString & "|'" & vbCrLf & _
              "che non ne permette lo svuotamento." & vbCrLf & _
              "Indicare un codice valido o deselezionare la scelta.")))
            Return False
          Case Else
        End Select
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
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
  End Function

  Public Overridable Function ProponiDescrizioneListaSelezionata(ByVal nCodlsar As Integer) As String
    ProponiDescrizioneListaSelezionata = ""
    Try
      Return oCldStrl.ProponiDescrizioneListaSelezionata(strDittaCorrente, nCodlsar)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  '@LM
  Public Overridable Function LoadWarehouse(ByRef pdsOut As DataSet) As Boolean
    Dim bRetVal As Boolean = False
    Try

      bRetVal = oCldStrl.LoadWarehouse(strDittaCorrente, pdsOut)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return bRetVal
  End Function
  '@

#Region "Scrittura LOG"
  Public Overridable Function ScriviActLog(ByVal strDesogglog As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCldStrl.ScriviActLog(strDittaCorrente, "BSMGSTRL", "BSMGSTRL", "", "", "M", "E", strDesogglog, False)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strDesogglog, oApp.InfoError, "", False)))
    End Try
  End Function
#End Region

End Class
