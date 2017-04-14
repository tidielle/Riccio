Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGELAR
  Inherits CLE__BASE

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

#Region "Variabili"
  Public oCldElar As CLDMGELAR

  Public bElabInCorso As Boolean = False

  Public bSeleziona As Boolean
  Public strWhereFiar As String

  Public strArroPrx As String

  'opzioni
  Public bAggPrezzi0 As Boolean
  Public strTipoArro As String

  'mgadoc
  Public bAdocAnnullato As Boolean
  Public strAdocQuery As String

  'mginea
  Public nRichiesta As Integer
  Public dIneaPrezzoattuale As Decimal
  Public strIneaDataattuale As String
  Public strIneaDatabase As String
  Public strIneaCodart As String
  Public lIneaConto As Integer
  Public lIneaCoddest As Integer
  Public dIneaDaquant As Decimal
  Public dIneaAquant As Decimal
  Public strIneaUnmis As String
  Public nIneaCodvalu As Integer
  Public strIneaCodartListini As String
  Public strIneaNetto As String
  Public nIneaFase As Integer
  Public dIneaPrezzobase As Decimal
  Public dIneaNuovovalore As Decimal

  'mgvris
  Public bVrisHasChanges As Boolean = False
  Public nVrisRichiesta As Integer


  Public strTipoPromo As String = "N"
  Public lPromo As Integer = 0

  Public bScontiDaCondComm As Boolean = False
  Public lContoDaCondComm As Integer = 0
  Public nClasconContoDaCondComm As Integer = 0
#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGELAR"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldElar = CType(MyBase.ocldBase, CLDMGELAR)
    oCldElar.Init(oApp)
    Return True
  End Function

  Public Overridable Function IstanziaNTSCondCommerciali() As NTSCondCommerciali
    Try
      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEMGELAR", "BN__STD.NTSCondCommerciali", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 127791222114531250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return Nothing
      End If
      Return CType(oTmp, NTSCondCommerciali)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return Nothing
  End Function

  Public Overridable Function TestPreElabora(ByVal bopGenerico As Boolean, ByVal stredConto As String, _
                                             ByVal bopClienti As Boolean, ByVal bopGenericoDaClienti As Boolean, _
                                             ByVal bopFornitori As Boolean, ByVal bopGenericoDaFornitori As Boolean, _
                                             ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                             ByVal stredDatagg As String, ByVal stredFase As String, _
                                             ByVal stredListpar As String, ByVal bopUltimaFase As Boolean, _
                                             ByVal bopSoloFase As Boolean, ByVal stredListelab As String, _
                                             ByVal bopQualsiasiFase As Boolean, ByVal stredValelab As String, _
                                             ByVal stredCodtpro As String, ByVal stredCodlavOut As String, _
                                             ByVal stredScadprom As String, ByVal bckAggiornalistini As Boolean, _
                                             ByVal bopValore As Boolean, ByVal stredAppMis As String, _
                                             ByVal bckElaborazione As Boolean, ByVal bckVisRisultati As Boolean, _
                                             ByVal bckAggiungiIVA As Boolean, ByVal stredValore As String, _
                                             ByVal stredCambioelab As String, ByVal stredCambiopar As String, _
                                             ByVal stredRicar1 As String, ByVal stredRicar2 As String, _
                                             ByVal bopPercentuale As Boolean, ByVal bopRicarico As Boolean, _
                                             ByVal bopScorporaIva As Boolean, ByVal stredArrotond As String) As Boolean
    Dim lTipoElaborazione As Integer = 1
    Try
      lTipoElaborazione = oCldElar.ConversioneOptionInCombo(bopGenerico, bopClienti, bopFornitori, bopGenericoDaClienti, bopGenericoDaFornitori)

      Return TestPreElaboraEx(lTipoElaborazione, stredConto, stredCodlavo, stredValpar, stredDatagg, stredFase, _
                              stredListpar, bopUltimaFase, bopSoloFase, stredListelab, bopQualsiasiFase, stredValelab, _
                              stredCodtpro, stredCodlavOut, stredScadprom, bckAggiornalistini, bopValore, stredAppMis, _
                              bckElaborazione, bckVisRisultati, bckAggiungiIVA, stredValore, stredCambioelab, stredCambiopar, _
                              stredRicar1, stredRicar2, bopPercentuale, bopRicarico, bopScorporaIva, stredArrotond, 0)
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

  Public Overridable Function TestPreElaboraEx(ByVal lTipoElaborazione As Integer, ByVal stredConto As String, _
                                             ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                             ByVal stredDatagg As String, ByVal stredFase As String, _
                                             ByVal stredListpar As String, ByVal bopUltimaFase As Boolean, _
                                             ByVal bopSoloFase As Boolean, ByVal stredListelab As String, _
                                             ByVal bopQualsiasiFase As Boolean, ByVal stredValelab As String, _
                                             ByVal stredCodtpro As String, ByVal stredCodlavOut As String, _
                                             ByVal stredScadprom As String, ByVal bckAggiornalistini As Boolean, _
                                             ByVal bopValore As Boolean, ByVal stredAppMis As String, _
                                             ByVal bckElaborazione As Boolean, ByVal bckVisRisultati As Boolean, _
                                             ByVal bckAggiungiIVA As Boolean, ByVal stredValore As String, _
                                             ByVal stredCambioelab As String, ByVal stredCambiopar As String, _
                                             ByVal stredRicar1 As String, ByVal stredRicar2 As String, _
                                             ByVal bopPercentuale As Boolean, ByVal bopRicarico As Boolean, _
                                             ByVal bopScorporaIva As Boolean, ByVal stredArrotond As String) As Boolean
    Try
      'obsoleta
      Return TestPreElaboraEx(lTipoElaborazione, stredConto, stredCodlavo, stredValpar, stredDatagg, stredFase, _
                              stredListpar, bopUltimaFase, bopSoloFase, stredListelab, bopQualsiasiFase, stredValelab, _
                              stredCodtpro, stredCodlavOut, stredScadprom, bckAggiornalistini, bopValore, stredAppMis, _
                              bckElaborazione, bckVisRisultati, bckAggiungiIVA, stredValore, stredCambioelab, stredCambiopar, _
                              stredRicar1, stredRicar2, bopPercentuale, bopRicarico, bopScorporaIva, stredArrotond, 0)

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
  Public Overridable Function TestPreElaboraEx(ByVal lTipoElaborazione As Integer, ByVal stredConto As String, _
                                               ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                               ByVal stredDatagg As String, ByVal stredFase As String, _
                                               ByVal stredListpar As String, ByVal bopUltimaFase As Boolean, _
                                               ByVal bopSoloFase As Boolean, ByVal stredListelab As String, _
                                               ByVal bopQualsiasiFase As Boolean, ByVal stredValelab As String, _
                                               ByVal stredCodtpro As String, ByVal stredCodlavOut As String, _
                                               ByVal stredScadprom As String, ByVal bckAggiornalistini As Boolean, _
                                               ByVal bopValore As Boolean, ByVal stredAppMis As String, _
                                               ByVal bckElaborazione As Boolean, ByVal bckVisRisultati As Boolean, _
                                               ByVal bckAggiungiIVA As Boolean, ByVal stredValore As String, _
                                               ByVal stredCambioelab As String, ByVal stredCambiopar As String, _
                                               ByVal stredRicar1 As String, ByVal stredRicar2 As String, _
                                               ByVal bopPercentuale As Boolean, ByVal bopRicarico As Boolean, _
                                               ByVal bopScorporaIva As Boolean, ByVal stredArrotond As String, _
                                               ByVal lCoddest As Integer) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lTipoElaborazione, stredConto, stredCodlavo, stredValpar, stredDatagg, stredFase, _
                              stredListpar, bopUltimaFase, bopSoloFase, stredListelab, bopQualsiasiFase, stredValelab, _
                              stredCodtpro, stredCodlavOut, stredScadprom, bckAggiornalistini, bopValore, stredAppMis, _
                              bckElaborazione, bckVisRisultati, bckAggiungiIVA, stredValore, stredCambioelab, stredCambiopar, _
                              stredRicar1, stredRicar2, bopPercentuale, bopRicarico, bopScorporaIva, stredArrotond, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      If NTSCDate(stredDatagg) > NTSCDate(stredScadprom) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128697417332656250, "La Data di Scadenza Promozione non può essere inferiore a quella di aggiornamento.")))
        Return False
      End If

      If NTSCDec(stredCambioelab) = 0 And NTSCInt(stredValelab) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130584669926502715, "Il campo 'Cambio' deve contenere un valore diverso da 0")))
        Return False
      End If

      If NTSCDec(stredCambiopar) = 0 And NTSCInt(stredValpar) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130584669926502716, "Il campo 'Cambio' deve contenere un valore diverso da 0")))
        Return False
      End If

      If lCoddest > 0 And NTSCInt(stredConto) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130585226616303042, "Con codice destinazione diversa maggiore di 0 è necessario indicare il cliente/fornitore a cui si riferisce la destinazione diversa")))
        Return False
      End If

      'come nelle versioni precedenti, se non indico il fornitore passerò tutti i fornitori
      'è il caso di prezzi specifici di acquisto da cui parto per generare listino specifico di vendita
      ''se genero listino generico da uno cliente, devo indicare sempre il cliente e la destinazione, diversamente creerei listini doppi!!!!
      'If (lCoddest < 0 Or NTSCInt(stredConto) = 0) And (lTipoElaborazione = 4 Or lTipoElaborazione = 5) Then
      '  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130585352495643328, "Per generare un listino generico da uno specifico è necessario indicare il cod. cliente/fornitore di riferimento e la destinazione diversa deve essere diversa da -1")))
      '  Return False
      'End If

      'se genero listino specifico da generico devo aver indicato cliente e destinazione
      If (lCoddest < 0 Or NTSCInt(stredConto) = 0) And (lTipoElaborazione = 6 Or lTipoElaborazione = 7) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130585352495643329, "Per generare un listino specifico da uno generico è necessario indicare il cod. cliente/fornitore di riferimento e la destinazione diversa deve essere diversa da -1")))
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

  Public Overridable Function Elabora(ByVal bopGenerico As Boolean, ByVal stredConto As String, _
                                      ByVal bopClienti As Boolean, ByVal bopGenericoDaClienti As Boolean, _
                                      ByVal bopFornitori As Boolean, ByVal bopGenericoDaFornitori As Boolean, _
                                      ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                      ByVal stredDatagg As String, ByVal stredFase As String, _
                                      ByVal stredListpar As String, ByVal bopUltimaFase As Boolean, _
                                      ByVal bopSoloFase As Boolean, ByVal stredListelab As String, _
                                      ByVal bopQualsiasiFase As Boolean, ByVal stredValelab As String, _
                                      ByVal stredCodtpro As String, ByVal stredCodlavOut As String, _
                                      ByVal stredScadprom As String, ByVal bckAggiornalistini As Boolean, _
                                      ByVal bopValore As Boolean, ByVal stredAppMis As String, _
                                      ByVal bckElaborazione As Boolean, ByVal bckVisRisultati As Boolean, _
                                      ByVal bckAggiungiIVA As Boolean, ByVal stredValore As String, _
                                      ByVal stredCambioelab As String, ByVal stredCambiopar As String, _
                                      ByVal stredRicar1 As String, ByVal stredRicar2 As String, _
                                      ByVal bopPercentuale As Boolean, ByVal bopRicarico As Boolean, _
                                      ByVal bopScorporaIva As Boolean, ByVal stredArrotond As String, _
                                      ByRef ds As DataSet, ByVal bckNetto As Boolean) As Boolean
    Dim lTipoElaborazione As Integer = 1
    Try
      lTipoElaborazione = oCldElar.ConversioneOptionInCombo(bopGenerico, bopClienti, bopFornitori, bopGenericoDaClienti, bopGenericoDaFornitori)

      Return ElaboraEx(lTipoElaborazione, stredConto, stredCodlavo, stredValpar, stredDatagg, stredFase, stredListpar, bopUltimaFase, _
                       bopSoloFase, stredListelab, bopQualsiasiFase, stredValelab, stredCodtpro, stredCodlavOut, stredScadprom, bckAggiornalistini, _
                       bopValore, stredAppMis, bckElaborazione, bckVisRisultati, bckAggiungiIVA, stredValore, stredCambioelab, stredCambiopar, _
                       stredRicar1, stredRicar2, bopPercentuale, bopRicarico, bopScorporaIva, stredArrotond, ds, bckNetto, 0)
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

  Public Overridable Function ElaboraEx(ByVal lTipoElaborazione As Integer, ByVal stredConto As String, _
                                      ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                      ByVal stredDatagg As String, ByVal stredFase As String, _
                                      ByVal stredListpar As String, ByVal bopUltimaFase As Boolean, _
                                      ByVal bopSoloFase As Boolean, ByVal stredListelab As String, _
                                      ByVal bopQualsiasiFase As Boolean, ByVal stredValelab As String, _
                                      ByVal stredCodtpro As String, ByVal stredCodlavOut As String, _
                                      ByVal stredScadprom As String, ByVal bckAggiornalistini As Boolean, _
                                      ByVal bopValore As Boolean, ByVal stredAppMis As String, _
                                      ByVal bckElaborazione As Boolean, ByVal bckVisRisultati As Boolean, _
                                      ByVal bckAggiungiIVA As Boolean, ByVal stredValore As String, _
                                      ByVal stredCambioelab As String, ByVal stredCambiopar As String, _
                                      ByVal stredRicar1 As String, ByVal stredRicar2 As String, _
                                      ByVal bopPercentuale As Boolean, ByVal bopRicarico As Boolean, _
                                      ByVal bopScorporaIva As Boolean, ByVal stredArrotond As String, _
                                      ByRef ds As DataSet, ByVal bckNetto As Boolean) As Boolean
    Try
      'obsoleta
      Return ElaboraEx(lTipoElaborazione, stredConto, stredCodlavo, stredValpar, stredDatagg, stredFase, _
                       stredListpar, bopUltimaFase, bopSoloFase, stredListelab, bopQualsiasiFase, stredValelab, _
                       stredCodtpro, stredCodlavOut, stredScadprom, bckAggiornalistini, bopValore, stredAppMis, _
                       bckElaborazione, bckVisRisultati, bckAggiungiIVA, stredValore, stredCambioelab, stredCambiopar, _
                       stredRicar1, stredRicar2, bopPercentuale, bopRicarico, bopScorporaIva, stredArrotond, _
                       ds, bckNetto, 0)

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
  Public Overridable Function ElaboraEx(ByVal lTipoElaborazione As Integer, ByVal stredConto As String, _
                                        ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                        ByVal stredDatagg As String, ByVal stredFase As String, _
                                        ByVal stredListpar As String, ByVal bopUltimaFase As Boolean, _
                                        ByVal bopSoloFase As Boolean, ByVal stredListelab As String, _
                                        ByVal bopQualsiasiFase As Boolean, ByVal stredValelab As String, _
                                        ByVal stredCodtpro As String, ByVal stredCodlavOut As String, _
                                        ByVal stredScadprom As String, ByVal bckAggiornalistini As Boolean, _
                                        ByVal bopValore As Boolean, ByVal stredAppMis As String, _
                                        ByVal bckElaborazione As Boolean, ByVal bckVisRisultati As Boolean, _
                                        ByVal bckAggiungiIVA As Boolean, ByVal stredValore As String, _
                                        ByVal stredCambioelab As String, ByVal stredCambiopar As String, _
                                        ByVal stredRicar1 As String, ByVal stredRicar2 As String, _
                                        ByVal bopPercentuale As Boolean, ByVal bopRicarico As Boolean, _
                                        ByVal bopScorporaIva As Boolean, ByVal stredArrotond As String, _
                                        ByRef ds As DataSet, ByVal bckNetto As Boolean, ByVal lCoddest As Integer) As Boolean
    Try
      'obsoleta
      Return ElaboraEx(lTipoElaborazione, stredConto, stredCodlavo, stredValpar, stredDatagg, stredFase, _
                       stredListpar, bopUltimaFase, bopSoloFase, stredListelab, bopQualsiasiFase, stredValelab, _
                       stredCodtpro, stredCodlavOut, stredScadprom, bckAggiornalistini, bopValore, stredAppMis, _
                       bckElaborazione, bckVisRisultati, bckAggiungiIVA, stredValore, stredCambioelab, stredCambiopar, _
                       stredRicar1, stredRicar2, bopPercentuale, bopRicarico, bopScorporaIva, stredArrotond, _
                       ds, bckNetto, lCoddest, "")
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function ElaboraEx(ByVal lTipoElaborazione As Integer, ByVal stredConto As String, _
                                        ByVal stredCodlavo As String, ByVal stredValpar As String, _
                                        ByVal stredDatagg As String, ByVal stredFase As String, _
                                        ByVal stredListpar As String, ByVal bopUltimaFase As Boolean, _
                                        ByVal bopSoloFase As Boolean, ByVal stredListelab As String, _
                                        ByVal bopQualsiasiFase As Boolean, ByVal stredValelab As String, _
                                        ByVal stredCodtpro As String, ByVal stredCodlavOut As String, _
                                        ByVal stredScadprom As String, ByVal bckAggiornalistini As Boolean, _
                                        ByVal bopValore As Boolean, ByVal stredAppMis As String, _
                                        ByVal bckElaborazione As Boolean, ByVal bckVisRisultati As Boolean, _
                                        ByVal bckAggiungiIVA As Boolean, ByVal stredValore As String, _
                                        ByVal stredCambioelab As String, ByVal stredCambiopar As String, _
                                        ByVal stredRicar1 As String, ByVal stredRicar2 As String, _
                                        ByVal bopPercentuale As Boolean, ByVal bopRicarico As Boolean, _
                                        ByVal bopScorporaIva As Boolean, ByVal stredArrotond As String, _
                                        ByRef ds As DataSet, ByVal bckNetto As Boolean, ByVal lCoddest As Integer, _
                                        ByVal strDataListinoRiferim As String) As Boolean
    'per il controllo dei duplicati negli art. a varianti
    Dim bArtVarianti1N As Boolean
    Dim dListDaQuant As Decimal
    Dim strListUnmis As String = ""
    Dim strListDatagg As String = ""
    Dim strCodartListiniLast As String
    Dim bArtVarianti1NLast As Boolean
    Dim dListDaQuantLast As Decimal
    Dim strListUnmisLast As String
    Dim strListDataggLast As String
    Dim bSalta As Boolean
    Dim i As Integer
    Dim j As Integer
    Dim k As Integer
    Dim dPrezzoNew(9) As Decimal
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lTipoElaborazione, stredConto, stredCodlavo, stredValpar, stredDatagg, stredFase, _
                       stredListpar, bopUltimaFase, bopSoloFase, stredListelab, bopQualsiasiFase, stredValelab, _
                       stredCodtpro, stredCodlavOut, stredScadprom, bckAggiornalistini, bopValore, stredAppMis, _
                       bckElaborazione, bckVisRisultati, bckAggiungiIVA, stredValore, stredCambioelab, stredCambiopar, _
                       stredRicar1, stredRicar2, bopPercentuale, bopRicarico, bopScorporaIva, stredArrotond, _
                       ds, bckNetto, lCoddest, strDataListinoRiferim})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(30), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      bElabInCorso = True

      If Not TestPreElaboraEx(lTipoElaborazione, stredConto, stredCodlavo, stredValpar, _
                             stredDatagg, stredFase, stredListpar, bopUltimaFase, _
                             bopSoloFase, stredListelab, bopQualsiasiFase, stredValelab, _
                             stredCodtpro, stredCodlavOut, stredScadprom, bckAggiornalistini, _
                             bopValore, stredAppMis, bckElaborazione, bckVisRisultati, _
                             bckAggiungiIVA, stredValore, stredCambioelab, stredCambiopar, _
                             stredRicar1, stredRicar2, bopPercentuale, bopRicarico, _
                             bopScorporaIva, stredArrotond, lCoddest) Then
        Return False
      End If

      ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 129013709083854110, "Selezione articoli/listini in corso...")))

      strCodartListiniLast = ""
      bArtVarianti1NLast = False
      dListDaQuantLast = -1
      strListUnmisLast = ""
      strListDataggLast = "1/1/1899"

      'ottengo la lista dei listini da elaborare
      oCldElar.GetElaboraEx(strDittaCorrente, lTipoElaborazione, bSeleziona, strAdocQuery, _
                          strWhereFiar, stredConto, stredCodlavo, stredValpar, _
                          strDataListinoRiferim, stredFase, stredListpar, bopUltimaFase, _
                          bopSoloFase, stredListelab, bopQualsiasiFase, strTipoPromo, lPromo, ds, lCoddest)

      AggiungiColonneUnbound(ds)

      dsShared = ds

      If ds.Tables("LISTINI").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128687057290481925, "Non esistono dati con queste caratteristiche.")))
        Return False
      End If

      For i = 0 To ds.Tables("LISTINI").Rows.Count - 1

        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128673200280482132, "Elaborazione in corso... Record |" & (i + 1) & "| di |" & ds.Tables("LISTINI").Rows.Count & "|")))

        With ds.Tables("LISTINI").Rows(i)
          Select Case lTipoElaborazione
            Case 6, 7 : If NTSCInt(stredConto) <> 0 Then !lc_conto = NTSCInt(stredConto)
          End Select
          'Normalizza il codice articolo x il listino
          !xx_codartlistini = !lc_codart
          bArtVarianti1N = False
          If NTSCStr(!ar_gesvar) = "S" Then
            If NTSCStr(!ar_codroot) <> "" Then
              Select Case NTSCStr(!ar_prevar)
                Case "N" : !xx_codartlistini = NTSCStr(!ar_codroot)
                Case "1" : !xx_codartlistini = NTSCStr(!ar_codroot) & NTSCStr(!ar_codvar1)
              End Select
            Else
              'Salta l'articolo ROOT quando i prezzi sono sulla 1. variante o sull'ultima
              If NTSCStr(!ar_prevar) = "1" Or NTSCStr(!ar_prevar) = "S" Then
                bSalta = True
                GoTo Salta
              End If

            End If
            If NTSCStr(!ar_prevar) = "N" Or NTSCStr(!ar_prevar) = "1" Then bArtVarianti1N = True
          End If

          dListDaQuant = NTSCDec(!lc_daquant)
          strListUnmis = NTSCStr(!lc_unmis)
          strListDatagg = NTSCStr(IIf(NTSCDate(!lc_datagg).ToShortDateString = "", "1/1/1900", NTSCDate(!lc_datagg).ToShortDateString))
          'Se trattasi di articolo a varianti come quello precedente salta l'elaborazione
          If bArtVarianti1N Then
            If bArtVarianti1NLast And (NTSCStr(!xx_codartlistini) = strCodartListiniLast) And (dListDaQuant = dListDaQuantLast) And (strListUnmis = strListUnmisLast) And (NTSCDate(strListDatagg) = NTSCDate(strListDataggLast)) Then
              bSalta = True
              GoTo Salta
            End If
          End If

          'listini generici (-2, -1, 0, 1-999)
          If lTipoElaborazione = 1 OrElse lTipoElaborazione = 4 OrElse lTipoElaborazione = 5 OrElse lTipoElaborazione = 6 OrElse lTipoElaborazione = 7 Then
            If Not NTSCStr(!lc_daquant) = "" Then
              !xx_daquant = NTSCDec(!lc_daquant)
              !xx_aquant = NTSCDec(!lc_aquant)
              If Not ((NTSCDate(!lc_datagg) <= NTSCDate(stredDatagg)) And (NTSCDate(!lc_datscad) >= NTSCDate(stredDatagg))) Then
                bSalta = True
                GoTo Salta
              End If
            Else
              !xx_daquant = 0
              !xx_aquant = NTSCDec(9999999999.0)
            End If
          End If

          If bckElaborazione = False Then
            'Elaborazione manuale
            SetPublicVarEx(NTSCStr(!xx_codartlistini), ds.Tables("LISTINI").Rows(i), lTipoElaborazione, stredValelab, _
                                      bopValore, stredAppMis, stredListelab, stredConto, _
                                       strDataListinoRiferim, stredCodlavo, bckAggiungiIVA, stredValore, _
                                       stredListpar, stredCambioelab, stredValpar, stredCambiopar, _
                                       stredRicar1, stredRicar2, bopPercentuale, _
                                       bopRicarico, bopScorporaIva, stredArrotond, bckNetto)

            dIneaNuovovalore = NTSCDec(ds.Tables("LISTINI").Rows(i)!xx_nuovovalore)

            ThrowRemoteEvent(New NTSEventArgs("SALTALIST:", ""))

            Select Case nRichiesta
              Case 0     ' ANNULLATO
                'elimina la riga attuale e successive da non elaborare
                For j = i To ds.Tables("LISTINI").Rows.Count - 1
                  ds.Tables("LISTINI").Rows(j).Delete()
                Next
                GoTo Fine
              Case 1 ' SALTATO
                bSalta = True
                GoTo Salta
            End Select

            ds.Tables("LISTINI").Rows(i)!xx_nuovovalore = dIneaNuovovalore

            'listini generici (-2, -1, 0, 1-999)
            If lTipoElaborazione = 1 OrElse lTipoElaborazione = 4 OrElse lTipoElaborazione = 5 Then
              If ListinoGiaPresente(NTSCStr(!xx_codartlistini), "0", stredValelab, stredCodtpro, stredListelab, stredDatagg, NTSCDec(!xx_daquant), stredCodlavOut, NTSCStr(!lc_unmis), NTSCStr(!lc_fase), NTSCInt(!lc_coddest)) = True Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128687079185190468, "Listino già esistente con queste caratteristiche.")))
                bSalta = True
                GoTo Salta
              End If
            Else ' listini clienti/fornitori (no agg. listini successivi 2-8)
              If ListinoGiaPresente(NTSCStr(!xx_codartlistini), stredConto, stredValelab, stredCodtpro, NTSCStr(!lc_listino), stredDatagg, NTSCDec(!lc_daquant), stredCodlavOut, NTSCStr(!lc_unmis), NTSCStr(!lc_fase), NTSCInt(!lc_coddest)) = True Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128687079106881663, "Listino già esistente con queste caratteristiche.")))
                bSalta = True
                GoTo Salta
              End If
            End If
          Else
            'Elaborazione automatica
            'listini generici (-2, -1, 0, 1-999)
            If lTipoElaborazione = 1 OrElse lTipoElaborazione = 4 OrElse lTipoElaborazione = 5 Then
              If ListinoGiaPresente(NTSCStr(!xx_codartlistini), "0", stredValelab, stredCodtpro, stredListelab, stredDatagg, NTSCDec(!xx_daquant), stredCodlavOut, NTSCStr(!lc_unmis), NTSCStr(!lc_fase), NTSCInt(!lc_coddest)) = True Then
                bSalta = True
                GoTo Salta
              End If
            Else ' listini clienti/fornitori (no agg. listini successivi 2-8)
              If ListinoGiaPresente(NTSCStr(!xx_codartlistini), stredConto, stredValelab, stredCodtpro, NTSCStr(!lc_listino), stredDatagg, NTSCDec(!lc_daquant), stredCodlavOut, NTSCStr(!lc_unmis), NTSCStr(!lc_fase), NTSCInt(!lc_coddest)) = True Then
                bSalta = True
                GoTo Salta
              End If
            End If

            SetPublicVarEx(NTSCStr(!xx_codartlistini), ds.Tables("LISTINI").Rows(i), lTipoElaborazione, stredValelab, _
                                      bopValore, stredAppMis, stredListelab, stredConto, _
                                       strDataListinoRiferim, stredCodlavo, bckAggiungiIVA, stredValore, _
                                       stredListpar, stredCambioelab, stredValpar, stredCambiopar, _
                                       stredRicar1, stredRicar2, bopPercentuale, _
                                       bopRicarico, bopScorporaIva, stredArrotond, bckNetto)
          End If
          !lc_prezzo = dIneaPrezzoattuale
Salta:
          strCodartListiniLast = NTSCStr(!xx_codartlistini)
          If bSalta = True Then
            ds.Tables("LISTINI").Rows(i).Delete()
            bSalta = False
          End If

          bArtVarianti1NLast = bArtVarianti1N
          dListDaQuantLast = dListDaQuant
          strListUnmisLast = strListUnmis
          strListDataggLast = strListDatagg
        End With
      Next

Fine:

      ds.Tables("LISTINI").AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To (ds.Tables("LISTINI").Rows.Count - 1)
        With ds.Tables("LISTINI").Rows(i)
          If NTSCDec(!lc_prezzo) > 0 Then
            !xx_percvar = ArrDbl(((NTSCDec(!xx_nuovovalore) - NTSCDec(!lc_prezzo)) / NTSCDec(!lc_prezzo)) * 100, 2)
          End If
        End With
      Next
      ds.Tables("LISTINI").AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------

      'visualizzo il risultato
      If bckVisRisultati Then
        ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 128696607223447641, "Preelaborazione completata. " & vbCrLf & _
        "Saranno visualizzati gli articoli trattati con vecchio e nuovo listino" & vbCrLf & _
        "e, se confermati, i dati saranno riversati sull'archivio.")))

        If Not VisualizzaRisultati() Then Return False
      End If

      'aggiorno effettimante i record

      For i = 0 To ds.Tables("LISTINI").Rows.Count - 1

        With ds.Tables("LISTINI").Rows(i)
          'Nel caso tutti, deve aggiorna la stessa promozirone associata al listino di partenza
          If strTipoPromo = "T" Then stredCodtpro = NTSCStr(ds.Tables("LISTINI").Rows(i)!lc_codtpro)
          If bckElaborazione = False Then
            'Elaborazione manuale
            ElaborazioneEx(NTSCStr(!xx_codartlistini), ds.Tables("LISTINI").Rows(i), lTipoElaborazione, stredCodlavOut, stredListelab, stredValelab, _
                         stredCodtpro, stredDatagg, stredScadprom)
            'listini generici (-2, -1, 0, 1-999)
            If lTipoElaborazione = 1 OrElse lTipoElaborazione = 4 OrElse lTipoElaborazione = 5 Then
              If NTSCInt(stredListelab) = 1 And (bckAggiornalistini = True) Then
                If NTSCInt(!ar_codpdon) <> 0 Then
                  For k = 1 To 8
                    dPrezzoNew(k) = NTSCDec(!xx_nuovovalore)
                  Next
                  CType(oCleComm, CLELBMENU).AggiornaPrezzi(strDittaCorrente, NTSCInt(!ar_codpdon), NTSCStr(!xx_codartlistini), NTSCInt(!lc_fase), _
                                                             NTSCInt(stredValelab), NTSCStr(!lc_unmis), NTSCDec(!lc_perqta), NTSCDec(!lc_daquant), _
                                                             NTSCDec(!lc_aquant), NTSCDate(stredDatagg), NTSCInt(stredCodlavOut), NTSCStr(!lc_netto), _
                                                             dPrezzoNew, False)
                End If
              End If
            End If
          Else
            'Elaborazione automatica
            'listini generici (-2, -1, 0, 1-999)
            If lTipoElaborazione = 1 OrElse lTipoElaborazione = 4 OrElse lTipoElaborazione = 5 Then
              '--- Aggiorna automaticamente solo se il prezzo calcolato è <> 0
              '--- oppure se il prezzo è = 0 e il valore di registro AggiornaPrezzi0 è True
              If (NTSCDec(!xx_nuovovalore) <> 0) Or ((NTSCDec(!xx_nuovovalore) = 0) And (bAggPrezzi0 = True)) Then
                ElaborazioneEx(NTSCStr(!xx_codartlistini), ds.Tables("LISTINI").Rows(i), lTipoElaborazione, stredCodlavOut, stredListelab, stredValelab, _
                            stredCodtpro, stredDatagg, stredScadprom)
                If NTSCInt(stredListelab) = 1 And (bckAggiornalistini = True) Then
                  If NTSCInt(!ar_codpdon) <> 0 Then
                    For k = 1 To 8
                      dPrezzoNew(k) = NTSCDec(!xx_nuovovalore)
                    Next
                    CType(oCleComm, CLELBMENU).AggiornaPrezzi(strDittaCorrente, NTSCInt(!ar_codpdon), NTSCStr(!xx_codartlistini), NTSCInt(!lc_fase), _
                                                               NTSCInt(stredValelab), NTSCStr(!lc_unmis), NTSCDec(!lc_perqta), NTSCDec(!lc_daquant), _
                                                               NTSCDec(!lc_aquant), NTSCDate(stredDatagg), NTSCInt(stredCodlavOut), NTSCStr(!lc_netto), _
                                                               dPrezzoNew, False)
                  End If
                End If
              End If
            Else
              ' listini clienti/fornitori (no agg. listini successivi 2-8)
              '--- Aggiorna automaticamente solo se il prezzo calcolato è <> 0
              If NTSCDec(!xx_nuovovalore) <> 0 Then
                ElaborazioneEx(NTSCStr(!xx_codartlistini), ds.Tables("LISTINI").Rows(i), lTipoElaborazione, stredCodlavOut, stredListelab, stredValelab, _
                            stredCodtpro, stredDatagg, stredScadprom)
              End If
            End If
          End If
        End With
      Next

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

  Public Overridable Function TrovaFmtPrz(Optional ByVal nCodvalu As Integer = 0) As String
    Dim i As Integer
    Try
      TrovaFmtPrz = "#,##0"
      If nCodvalu = 0 Then
        If oApp.NDecPrzUn > 0 Then TrovaFmtPrz = TrovaFmtPrz & "."
        For i = 1 To oApp.NDecPrzUn
          TrovaFmtPrz = TrovaFmtPrz & "0"
        Next
      Else
        If oApp.NDecPrzUnVal > 0 Then
          TrovaFmtPrz = TrovaFmtPrz & "."
          For i = 1 To oApp.NDecPrzUnVal
            TrovaFmtPrz = TrovaFmtPrz & "0"
          Next
        End If
      End If

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function edCodlavo_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldElar.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABLAVO", "N", strTmp) Then
          strErr = oApp.Tr(Me, 128450577437473574, "Codice lavorazione inesistente.")
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
  Public Overridable Function edCodlavOut_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldElar.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABLAVO", "N", strTmp) Then
          strErr = oApp.Tr(Me, 128686390394284041, "Codice lavorazione inesistente.")
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
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldElar.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABTPRO", "N", strTmp) Then
          strErr = oApp.Tr(Me, 128686390353001833, "Codice promozione inesistente.")
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
  Public Overridable Function edConto_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String, _
                                                ByVal bopFornitori As Boolean, ByVal bopClienti As Boolean) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

      If Not NTSCInt(nCod) = 0 Then
        If Not oCldElar.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "ANAGRA", "N", strTmp, dttTmp) Then
          strErr = oApp.Tr(Me, 128686352583847185, "Codice cliente/fornitore inesistente.")
          Return False
        Else
          Select Case NTSCStr(dttTmp.Rows(0)!an_tipo)
            Case "S"
              strErr = oApp.Tr(Me, 128686360580242149, "Il numero conto non può far parte dei sottoconti.")
              Return False
            Case "C"
              If bopFornitori = True Then
                strErr = oApp.Tr(Me, 128686360737396009, "Fornitore non valido.")
                Return False
              End If
            Case "F"
              If bopClienti = True Then
                strErr = oApp.Tr(Me, 128686360913001765, "Cliente non valido.")
                Return False
              End If
          End Select
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
  Public Overridable Function edValelab_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldElar.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABVALU", "N", strTmp) Then
          strErr = oApp.Tr(Me, 128686390263400677, "Codice valuta inesistente.")
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
  Public Overridable Function edValpar_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldElar.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABVALU", "N", strTmp) Then
          strErr = oApp.Tr(Me, 128686390222587585, "Codice valuta inesistente.")
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
  Public Overridable Function edCodlsar_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldElar.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABLSAR", "N", strTmp) Then
          strErr = oApp.Tr(Me, 130384872942421572, "Codice lista selezionata articoli inesistente.")
          Return False
        Else
          strDescr = strTmp
        End If
      Else
        strDescr = ""
      End If
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

  Public Overridable Function CercaCambioDiOggi(ByVal nCod As Integer, ByVal strValid As String, ByVal strCambio As String) As Boolean
    Dim dCambio As Decimal
    Try
      dCambio = oCldElar.CercaCambioDiOggi(nCod, strValid)
      strCambio = NTSCStr(dCambio)

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

  Public Overridable Sub SetPublicVar(ByVal strCodartList As String, ByVal dtrTmp As DataRow, _
                                      ByVal bopGenerico As Boolean, ByVal stredValelab As String, _
                                      ByVal bopValore As Boolean, ByVal stredAppMis As String, _
                                      ByVal stredListelab As String, ByVal stredConto As String, _
                                      ByVal stredDatagg As String, ByVal stredCodlavo As String, _
                                      ByVal bckAggiungiIVA As Boolean, ByVal stredValore As String, _
                                      ByVal stredListpar As String, ByVal stredCambioelab As String, _
                                      ByVal stredValpar As String, ByVal stredCambiopar As String, _
                                      ByVal stredRicar1 As String, ByVal stredRicar2 As String, _
                                      ByVal bopPercentuale As Boolean, ByVal bopRicarico As Boolean, _
                                      ByVal bopScorporaIva As Boolean, ByVal stredArrotond As String, _
                                      ByVal bopGenericoDaClienti As Boolean, ByVal bopGenericoDaFornitori As Boolean, _
                                      ByVal bckNetto As Boolean, ByVal bopClienti As Boolean, _
                                      ByVal bopFornitori As Boolean)
    Dim lTipoElaborazione As Integer = 0
    Try
      lTipoElaborazione = oCldElar.ConversioneOptionInCombo(bopGenerico, bopClienti, bopFornitori, bopGenericoDaClienti, bopGenericoDaFornitori)

      SetPublicVarEx(strCodartList, dtrTmp, lTipoElaborazione, stredValelab, bopValore, stredAppMis, stredListelab, stredConto, _
                    stredDatagg, stredCodlavo, bckAggiungiIVA, stredValore, stredListpar, stredCambioelab, stredValpar, stredCambiopar, _
                    stredRicar1, stredRicar2, bopPercentuale, bopRicarico, bopScorporaIva, stredArrotond, bckNetto)
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
  Public Overridable Sub SetPublicVarEx(ByVal strCodartList As String, ByVal dtrTmp As DataRow, _
                                        ByVal lTipoElaborazione As Integer, ByVal stredValelab As String, _
                                        ByVal bopValore As Boolean, ByVal stredAppMis As String, _
                                        ByVal stredListelab As String, ByVal stredConto As String, _
                                        ByVal stredDatagg As String, ByVal stredCodlavo As String, _
                                        ByVal bckAggiungiIVA As Boolean, ByVal stredValore As String, _
                                        ByVal stredListpar As String, ByVal stredCambioelab As String, _
                                        ByVal stredValpar As String, ByVal stredCambiopar As String, _
                                        ByVal stredRicar1 As String, ByVal stredRicar2 As String, _
                                        ByVal bopPercentuale As Boolean, ByVal bopRicarico As Boolean, _
                                        ByVal bopScorporaIva As Boolean, ByVal stredArrotond As String, _
                                        ByVal bckNetto As Boolean)
    Dim nDec As Integer
    Dim dsTmp As DataSet = Nothing
    Dim dAliq As Decimal
    Try
      With dtrTmp
        strIneaDatabase = ""
        If lTipoElaborazione = 2 OrElse lTipoElaborazione = 3 OrElse lTipoElaborazione = 4 OrElse _
           lTipoElaborazione = 5 OrElse lTipoElaborazione = 6 OrElse lTipoElaborazione = 7 Then
          strIneaCodart = NTSCStr(!lc_codart)
          lIneaConto = NTSCInt(!lc_conto)
          lIneaCoddest = NTSCInt(!lc_coddest)
          strIneaDatabase = "Dal " & NTSCDate(!lc_datagg).ToShortDateString & " al " & NTSCDate(!lc_datscad).ToShortDateString
          strIneaDataattuale = "Dal " & NTSCDate(!lc_datagg).ToShortDateString & " al " & NTSCDate(!lc_datscad).ToShortDateString
          dIneaDaquant = NTSCDec(!lc_daquant)
          dIneaAquant = NTSCDec(!lc_aquant)
          strIneaUnmis = NTSCStr(!lc_unmis)
          !xx_daquant = NTSCStr(!lc_daquant)
          !xx_aquant = NTSCStr(!lc_aquant)
        Else
          strIneaCodart = NTSCStr(!ar_codart)
          lIneaConto = 0
          lIneaCoddest = 0
          dIneaDaquant = NTSCDec(!xx_daquant)
          dIneaAquant = NTSCDec(!xx_aquant)
          strIneaUnmis = NTSCStr(!lc_unmis)
        End If
        nIneaCodvalu = NTSCInt(stredValelab)
        strIneaCodartListini = strCodartList
        strIneaNetto = NTSCStr(!lc_netto)
        nIneaFase = NTSCInt(!lc_fase)
        PrezzoBaseEx(dtrTmp, stredListpar, stredValelab, stredDatagg, stredCambioelab, lTipoElaborazione, stredConto, stredCodlavo, _
                    stredValpar, stredCambiopar, bckNetto, lIneaCoddest)
        PrezzoAttualeEx(dtrTmp, lTipoElaborazione, stredListelab, stredConto, stredDatagg, stredValelab, stredCodlavo, lIneaCoddest)

        nDec = oCldElar.TrovaNdecSuPrzUn(NTSCInt(stredValelab))
        '-------------------------------------------------------------------------------------
        'Se si è scelto di aggiungere l'IVA la aggiunge ora al prezzo base
        If bckAggiungiIVA = True Then
          dAliq = 0

          oCldElar.GetArtico(strDittaCorrente, NTSCStr(!ar_codart), dsTmp)

          If Not dsTmp.Tables("ARTICO").Rows.Count = 0 Then
            dAliq = NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!tb_aliq)
          End If
          If dAliq > 0 Then
            'incrementa prezzo base
            'volutamente non arrotondo
            !xx_prezzobase = (NTSCDec(!xx_prezzobase) * ((100 + dAliq) / 100))
          End If
        End If
        '-------------------------------------------------------------------------------------
        If bopValore = True Then
          If stredAppMis = "P" Then
            !xx_nuovovalore = ArrDbl(NTSCDec(!xx_prezzobase) + NTSCDec(stredValore), nDec)
          Else
            'Secondaria
            If stredAppMis = "S" Then
              If Trim(NTSCStr(!ar_unmis2)) <> "" Then
                !xx_nuovovalore = ArrDbl(NTSCDec(!xx_prezzobase) + NTSCDec(stredValore) * NTSCDec(!ar_conver), nDec)
              End If
              'Confezione
            Else
              If Trim(NTSCStr(!ar_confez2)) <> "" Then
                !xx_nuovovalore = ArrDbl(NTSCDec(!xx_prezzobase) + NTSCDec(stredValore) / NTSCDec(!ar_qtacon2), nDec)
              End If
            End If
          End If
        Else
          !xx_nuovovalore = ArrDbl(ArrotondamentoPrezzo(NTSCStr(!ar_codart), NTSCDec(!xx_prezzobase), NTSCInt(stredValelab), _
                                                        stredRicar1, stredRicar2, bopPercentuale, _
                                                        bopRicarico, bopScorporaIva, stredArrotond, stredListelab), nDec)
        End If

        Select Case lTipoElaborazione
          Case 6
            If NTSCInt(stredConto) <> 0 Then !lc_conto = stredConto
            !lc_listino = 0
            !lc_tipo = "C"
          Case 7
            If NTSCInt(stredConto) <> 0 Then !lc_conto = stredConto
            !lc_listino = 0
            !lc_tipo = "F"
        End Select
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

  Public Overridable Function ListinoGiaPresente(ByVal strCodart As String, ByVal strConto As String, _
                                               ByVal strCodvalu As String, ByVal strCodtpro As String, _
                                               ByVal strListino As String, ByVal strDatagg As String, _
                                               ByVal dDaQuant As Decimal, ByVal strCodlavo As String, _
                                               ByVal strUnmis As String, ByVal strFase As String) As Boolean
    Try
      'obsoleta
      Return ListinoGiaPresente(strCodart, strConto, strCodvalu, strCodtpro, strListino, strDatagg, dDaQuant, strCodlavo, strUnmis, strFase, 0)

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
  Public Overridable Function ListinoGiaPresente(ByVal strCodart As String, ByVal strConto As String, _
                                                 ByVal strCodvalu As String, ByVal strCodtpro As String, _
                                                 ByVal strListino As String, ByVal strDatagg As String, _
                                                 ByVal dDaQuant As Decimal, ByVal strCodlavo As String, _
                                                 ByVal strUnmis As String, ByVal strFase As String, _
                                                 ByVal lCoddest As Integer) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodart, strConto, strCodvalu, strCodtpro, strListino, strDatagg, dDaQuant, strCodlavo, strUnmis, strFase, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      oCldElar.ListinoGiaPresente(strDittaCorrente, strCodart, strConto, strCodvalu, strCodtpro, _
                                  strListino, strDatagg, dDaQuant, strCodlavo, _
                                  strUnmis, strFase, dsTmp, lCoddest)

      If dsTmp.Tables("LISTINI").Rows.Count = 0 Then
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

  Public Overridable Sub Elaborazione(ByVal strCodart As String, ByVal dtrTmp As DataRow, _
                           ByVal bopGenerico As Boolean, ByVal bopGenericoDaClienti As Boolean, _
                           ByVal bopGenericoDaFornitori As Boolean, ByVal stredCodlavOut As String, _
                           ByVal stredListelab As String, ByVal stredValelab As String, _
                           ByVal stredCodtpro As String, ByVal stredDatagg As String, _
                           ByVal stredScadprom As String)
    Dim lTipoElaborazione As Integer = 1
    Try
      lTipoElaborazione = oCldElar.ConversioneOptionInCombo(bopGenerico, False, False, bopGenericoDaClienti, bopGenericoDaFornitori)

      ElaborazioneEx(strCodart, dtrTmp, lTipoElaborazione, stredCodlavOut, stredListelab, stredValelab, _
                     stredCodtpro, stredDatagg, stredScadprom)
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
  Public Overridable Sub ElaborazioneEx(ByVal strCodart As String, ByVal dtrTmp As DataRow, _
                                        ByVal lTipoElaborazione As Integer, ByVal stredCodlavOut As String, _
                                        ByVal stredListelab As String, ByVal stredValelab As String, _
                                        ByVal stredCodtpro As String, ByVal stredDatagg As String, _
                                        ByVal stredScadprom As String)
    Try
      With dtrTmp
        If lTipoElaborazione = 1 OrElse lTipoElaborazione = 4 OrElse lTipoElaborazione = 5 Then
          CType(oCleComm, CLELBMENU).ScriviPrezzo(strDittaCorrente, strCodart, NTSCInt(!lc_fase), NTSCInt(stredCodlavOut), _
            " ", NTSCInt(stredListelab), 0, NTSCInt(stredValelab), NTSCStr(!lc_unmis), _
            NTSCInt(stredCodtpro), NTSCDec(!xx_daquant), NTSCDate(stredDatagg), NTSCDec(!xx_nuovovalore), _
            NTSCDate(NTSCStr(IIf(NTSCInt(stredCodtpro) = 0, NTSCDate(!lc_datscad).ToShortDateString, stredScadprom))), _
            NTSCDec(!lc_perqta), NTSCDec(!xx_aquant), NTSCStr(!lc_note), NTSCStr(!lc_netto), False, "", NTSCInt(!lc_coddest))
        Else
          CType(oCleComm, CLELBMENU).ScriviPrezzo(strDittaCorrente, strCodart, NTSCInt(!lc_fase), NTSCInt(stredCodlavOut), _
             NTSCStr(!lc_tipo), NTSCInt(!lc_listino), NTSCInt(!lc_conto), NTSCInt(stredValelab), _
             NTSCStr(!lc_unmis), NTSCInt(stredCodtpro), NTSCDec(!lc_daquant), NTSCDate(stredDatagg), _
             NTSCDec(!xx_nuovovalore), _
             NTSCDate(NTSCStr(IIf(NTSCInt(stredCodtpro) = 0, NTSCDate(!lc_datscad).ToShortDateString, stredScadprom))), _
             NTSCDec(!lc_perqta), NTSCDec(!lc_aquant), NTSCStr(!lc_note), NTSCStr(!lc_netto), False, "", NTSCInt(!lc_coddest))
        End If
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

  Public Overridable Sub PrezzoBase(ByVal dtrTmp As DataRow, ByVal stredListpar As String, _
                                    ByVal stredValelab As String, ByVal stredDatagg As String, _
                                    ByVal stredCambioelab As String, ByVal bopGenerico As Boolean, _
                                    ByVal stredConto As String, ByVal stredCodlavo As String, _
                                    ByVal stredValpar As String, ByVal stredCambiopar As String, _
                                    ByVal bopGenericoDaClienti As Boolean, ByVal bopGenericoDaFornitori As Boolean, _
                                    ByVal bckNetto As Boolean)
    Dim lTipoElaborazione As Integer = 0
    Try
      lTipoElaborazione = oCldElar.ConversioneOptionInCombo(bopGenerico, False, False, bopGenericoDaClienti, bopGenericoDaFornitori)

      PrezzoBaseEx(dtrTmp, stredListpar, stredValelab, stredDatagg, stredCambioelab, lTipoElaborazione, stredConto, stredCodlavo, _
                 stredValpar, stredCambiopar, bckNetto, 0)
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
  Public Overridable Sub PrezzoBaseEx(ByVal dtrTmp As DataRow, ByVal stredListpar As String, _
                                    ByVal stredValelab As String, ByVal stredDatagg As String, _
                                    ByVal stredCambioelab As String, ByVal lTipoElaborazione As Integer, _
                                    ByVal stredConto As String, ByVal stredCodlavo As String, _
                                    ByVal stredValpar As String, ByVal stredCambiopar As String, _
                                    ByVal bckNetto As Boolean)
    Try
      'obsoleta
      PrezzoBaseEx(dtrTmp, stredListpar, stredValelab, stredDatagg, stredCambioelab, lTipoElaborazione, _
                   stredConto, stredCodlavo, stredValpar, stredCambiopar, bckNetto, 0)
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
  Public Overridable Sub PrezzoBaseEx(ByVal dtrTmp As DataRow, ByVal stredListpar As String, _
                                      ByVal stredValelab As String, ByVal stredDatagg As String, _
                                      ByVal stredCambioelab As String, ByVal lTipoElaborazione As Integer, _
                                      ByVal stredConto As String, ByVal stredCodlavo As String, _
                                      ByVal stredValpar As String, ByVal stredCambiopar As String, _
                                      ByVal bckNetto As Boolean, ByVal lCoddest As Integer)
    Dim dValore As Decimal
    Dim dsTmp2 As DataSet = Nothing
    Dim strCodart As String = ""
    Dim strCosa As String
    Dim dSc1 As Decimal
    Dim dSc2 As Decimal
    Dim dSc3 As Decimal
    Dim dSc4 As Decimal
    Dim dSc5 As Decimal
    Dim dSc6 As Decimal
    Dim lContoSC As Integer
    Dim strListaSC As String
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrTmp, stredListpar, stredValelab, stredDatagg, stredCambioelab, lTipoElaborazione, _
                   stredConto, stredCodlavo, stredValpar, stredCambiopar, bckNetto, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return
      End If
      '----------------

      With dtrTmp
        '-----------------------------------------------------------------------------------------
        !xx_prezzobase = 0
        '-----------------------------------------------------------------------------------------
        '--- Se 'Ultimo Costo' (listino = 0)
        '--- Se 'Ultimo Costo con Oneri Accessori' (listino = -1)
        '--- Se 'Costo Medio' (listino = -2)
        '-----------------------------------------------------------------------------------------
        If NTSCInt(stredListpar) = 0 Or NTSCInt(stredListpar) = -1 Or NTSCInt(stredListpar) = -2 Then
          'Normalizza la query per gli articoli normali e a varianti

          oCldElar.GetPrezzoBase(strDittaCorrente, dtrTmp, dsTmp2)

          If Not dsTmp2.Tables("LISTINI").Rows.Count = 0 Then
            If NTSCInt(stredListpar) = 0 Then
              'ULTIMO COSTO
              strCosa = "Ultimo costo"
              If NTSCInt(stredValelab) <> 0 Then
                !xx_prezzobase = ConvPrezzoUnLireur(True, NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_ultcos), NTSCInt(stredValelab), stredDatagg, NTSCDec(stredCambioelab))
              Else
                !xx_prezzobase = NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_ultcos)
              End If
            ElseIf NTSCInt(stredListpar) = -1 Then
              'ULTIMO COSTO CON ONERI ACCESSORI
              strCosa = "Ultimo costo con oneri acc."
              If NTSCInt(stredValelab) <> 0 Then
                !xx_prezzobase = ConvPrezzoUnLireur(True, NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_peucos), NTSCInt(stredValelab), stredDatagg, NTSCDec(stredCambioelab))
              Else
                !xx_prezzobase = NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_peucos)
              End If
            Else
              'COSTO MEDIO
              strCosa = "Costo medio"
              If (NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_vqtalif) <> 0) And (NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_qtalif) <> 0) Then
                !xx_prezzobase = (NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_vqtalif) / NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_qtalif))
              Else
                If (NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_vgiaini) <> 0) And (NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_giaini) <> 0) Then
                  !xx_prezzobase = (NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_vgiaini) / NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!apx_giaini))
                End If
              End If
              If NTSCInt(stredValelab) <> 0 Then
                !xx_prezzobase = ConvPrezzoUnLireur(True, NTSCDec(!xx_prezzobase), NTSCInt(stredValelab), stredDatagg, NTSCDec(stredCambioelab))
              End If
              'Applica il moltiplicatore prezzo
              If NTSCDec(!xx_prezzobase) <> 0 Then !xx_prezzobase = (NTSCDec(!xx_prezzobase) * NTSCDec(!lc_perqta))
            End If

            'Compone il messgagio da visualizzare
            If NTSCStr(!ar_gesvar) = "S" And (NTSCStr(!ar_prevar) = "N" Or NTSCStr(!ar_prevar) = "1") Then
              If NTSCStr(dsTmp2.Tables("LISTINI").Rows(0)!apx_dtulcar) = "" Then
                strIneaDatabase = strCosa & " prelevato dai progressivi dell'articolo " & NTSCDate(dsTmp2.Tables("LISTINI").Rows(0)!apx_codart).ToShortDateString & ", data ultimo carico: (non presente)"
              Else
                strIneaDatabase = strCosa & " prelevato dai progressivi dell'articolo " & NTSCDate(dsTmp2.Tables("LISTINI").Rows(0)!apx_codart).ToShortDateString & ", data ultimo carico: " & NTSCDate(dsTmp2.Tables("LISTINI").Rows(0)!apx_dtulcar).ToShortDateString
              End If
            End If
          End If
        End If

        '-----------------------------------------------------------------------------------------
        '--- Se listino definito dall'utente (1-999)
        '-----------------------------------------------------------------------------------------
        If NTSCInt(stredListpar) > 0 Then
          strCodart = NTSCStr(!ar_codart)
          'Rettifica il codice articoli per gli articoli a variante
          If NTSCStr(!ar_gesvar) = "S" Then
            If NTSCStr(!ar_codroot) <> "" Then ' se trattasi dell'articolo ROOT codroot è vuoto
              Select Case NTSCStr(!ar_prevar)
                Case "N" : strCodart = NTSCStr(!ar_codroot)
                Case "1" : strCodart = NTSCStr(!ar_codroot) & NTSCStr(!ar_codvar1)
              End Select
            End If
          End If

          If strTipoPromo = "T" Then lPromo = NTSCInt(dtrTmp!lc_codtpro)

          oCldElar.GetPrezzoBase2(strDittaCorrente, dtrTmp, strCodart, (lTipoElaborazione = 1 OrElse lTipoElaborazione = 6 OrElse lTipoElaborazione = 7), _
                                  stredListpar, stredConto, stredCodlavo, stredValpar, _
                                  stredDatagg, NTSCDec(!xx_daquant), strTipoPromo, lPromo, dsTmp2, lCoddest)

          If Not dsTmp2.Tables("LISTINI").Rows.Count = 0 Then
            If (NTSCInt(stredValpar) = 0) And (NTSCInt(stredValelab) = 0) Then !xx_prezzobase = dsTmp2.Tables("LISTINI").Rows(0)!lc_prezzo
            If (NTSCInt(stredValpar) <> 0) And (NTSCInt(stredValelab) = 0) Then
              dValore = ConvPrezzoUnValuta(True, NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!lc_prezzo), NTSCInt(stredValpar), stredDatagg, NTSCDec(stredCambiopar))
              !xx_prezzobase = dValore
            End If
            If (NTSCInt(stredValpar) = 0) And (NTSCInt(stredValelab) <> 0) Then
              dValore = ConvPrezzoUnLireur(True, NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!lc_prezzo), NTSCInt(stredValelab), stredDatagg, NTSCDec(stredCambioelab))
              !xx_prezzobase = dValore
            End If
            If (NTSCInt(stredValpar) <> 0) And (NTSCInt(stredValelab) <> 0) Then
              dValore = ConvPrezzoUnValuta(True, NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!lc_prezzo), NTSCInt(stredValpar), stredDatagg, NTSCDec(stredCambiopar))
              dValore = ConvPrezzoUnLireur(True, dValore, NTSCInt(stredValelab), stredDatagg, NTSCDec(stredCambioelab))
              !xx_prezzobase = dValore
            End If
            strIneaDatabase = "Dal " & NTSCDate(dsTmp2.Tables("LISTINI").Rows(0)!lc_datagg).ToShortDateString & " al " & NTSCDate(dsTmp2.Tables("LISTINI").Rows(0)!lc_datscad).ToShortDateString
          Else
            !xx_prezzobase = 0
            strIneaDatabase = ""
          End If
        End If

        'Rileva gli sconti speciali x determinare il netto
        If NTSCInt(stredConto) = 0 Then lContoSC = NTSCInt(!lc_conto) Else lContoSC = NTSCInt(stredConto)
        If (lTipoElaborazione = 4 OrElse lTipoElaborazione = 5) And lContoSC > 0 And bckNetto Then
          'legge gli sconti speciali
          Dim oCondCommerciali As NTSCondCommerciali = IstanziaNTSCondCommerciali()
          oCondCommerciali.bCalcolaSconti = True
          With oCondCommerciali.Input
            .strDitta = strDittaCorrente
            .strCodart = strCodart
            .lConto = lContoSC
            .lDestdiv = lCoddest
            .strTipoval = "P"
            .bConspromo = True
            .dtDatdoc = NTSCDate(stredDatagg)
            .dQuant = NTSCDec(dtrTmp!xx_daquant)
          End With
          '--------------
          CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
          '--------------
          If NTSCStr(!lc_netto).ToUpper = "N" Then
            With oCondCommerciali.OutputSconti
              dSc1 = .dSconto1
              dSc2 = .dSconto2
              dSc3 = .dSconto3
              dSc4 = .dSconto4
              dSc5 = .dSconto5
              dSc6 = .dSconto6
            End With
            'determina il nuovo prezzo base al netto degli sconti
            !xx_prezzobase = ArrDbl(NTSCDec(!xx_prezzobase) * (100 - dSc1) / 100 * (100 - dSc2) / 100 * (100 - dSc3) / 100 * (100 - dSc4) / 100 * (100 - dSc5) / 100 * (100 - dSc6) / 100, oCldElar.TrovaNdecSuPrzUn(NTSCInt(stredValpar)))
            strListaSC = ""
            If dSc1 <> 0 Then strListaSC = NTSCStr(dSc1) & "%"
            If dSc2 <> 0 Then strListaSC &= NTSCStr(IIf(strListaSC <> "", " +", "")) & NTSCStr(dSc2) & "%"
            If dSc3 <> 0 Then strListaSC &= NTSCStr(IIf(strListaSC <> "", " +", "")) & NTSCStr(dSc3) & "%"
            If dSc4 <> 0 Then strListaSC &= NTSCStr(IIf(strListaSC <> "", " +", "")) & NTSCStr(dSc4) & "%"
            If dSc5 <> 0 Then strListaSC &= NTSCStr(IIf(strListaSC <> "", " +", "")) & NTSCStr(dSc5) & "%"
            If dSc6 <> 0 Then strListaSC &= NTSCStr(IIf(strListaSC <> "", " +", "")) & NTSCStr(dSc6) & "%"
            If strListaSC <> "" Then strIneaDatabase &= " (netto sconti speciali: " & strListaSC & ")"
          End If
        End If
        If (lTipoElaborazione = 4 OrElse lTipoElaborazione = 5) And _
           (lContoDaCondComm > 0) And (bScontiDaCondComm = True) Then
          '----------------------------------------------------------------------------------------------------------
          Dim dttTmp As New DataTable
          Dim nClscar As Integer = 0
          If oCldElar.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", Nothing, dttTmp) = True Then
            If dttTmp.Rows.Count > 0 Then nClscar = NTSCInt(dttTmp.Rows(0)!ar_clascon)
          End If
          dttTmp.Clear()
          dttTmp.Dispose()
          '----------------------------------------------------------------------------------------------------------
          '--- legge gli sconti speciali
          '----------------------------------------------------------------------------------------------------------
          If CType(oCleComm, CLELBMENU).CercaSconti(strDittaCorrente, strCodart, lContoDaCondComm, _
            nClscar, nClasconContoDaCondComm, "P", True, 0, NTSCDate(stredDatagg), 1, _
            dSc1, dSc2, dSc3, dSc4, dSc5, dSc6, 0, "") Then
          End If
          '----------------------------------------------------------------------------------------------------------
          '--- Determina il nuovo prezzo base al netto degli sconti
          '----------------------------------------------------------------------------------------------------------
          !xx_prezzobase = ArrDbl(NTSCDec(!xx_prezzobase) * (100 - dSc1) / 100 * (100 - dSc2) / 100 * (100 - dSc3) / 100 * (100 - dSc4) / 100 * (100 - dSc5) / 100 * (100 - dSc6) / 100, oCldElar.TrovaNdecSuPrzUn(NTSCInt(stredValpar)))
          strListaSC = ""
          If dSc1 <> 0 Then strListaSC = NTSCStr(dSc1) & "%"
          If dSc2 <> 0 Then strListaSC += NTSCStr(IIf(strListaSC <> "", " +", "")) & NTSCStr(dSc2) & "%"
          If dSc3 <> 0 Then strListaSC += NTSCStr(IIf(strListaSC <> "", " +", "")) & NTSCStr(dSc3) & "%"
          If dSc4 <> 0 Then strListaSC += NTSCStr(IIf(strListaSC <> "", " +", "")) & NTSCStr(dSc4) & "%"
          If dSc5 <> 0 Then strListaSC += NTSCStr(IIf(strListaSC <> "", " +", "")) & NTSCStr(dSc5) & "%"
          If dSc6 <> 0 Then strListaSC += NTSCStr(IIf(strListaSC <> "", " +", "")) & NTSCStr(dSc6) & "%"
          If strListaSC <> "" Then strIneaDatabase += " (netto sconti speciali: " & strListaSC & ")"
        End If
        'Passa il prezzo base alla form
        dIneaPrezzobase = NTSCDec(!xx_prezzobase)
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

  Public Overridable Sub PrezzoAttuale(ByVal dtrTmp As DataRow, ByVal bopGenerico As Boolean, _
                                       ByVal stredListelab As String, ByVal stredConto As String, _
                                       ByVal stredDatagg As String, ByVal stredValelab As String, _
                                       ByVal stredCodlavo As String, ByVal bopClienti As Boolean, _
                                       ByVal bopGenericoDaClienti As Boolean, ByVal bopFornitori As Boolean, _
                                       ByVal bopGenericoDaFornitori As Boolean)
    Dim lTipoElaborazione As Integer = 0
    Try
      lTipoElaborazione = oCldElar.ConversioneOptionInCombo(bopGenerico, False, False, bopGenericoDaClienti, bopGenericoDaFornitori)

      PrezzoAttualeEx(dtrTmp, lTipoElaborazione, stredListelab, stredConto, stredDatagg, stredValelab, stredCodlavo, 0)
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
  Public Overridable Sub PrezzoAttualeEx(ByVal dtrTmp As DataRow, ByVal lTipoElaborazione As Integer, _
                                         ByVal stredListelab As String, ByVal stredConto As String, _
                                         ByVal stredDatagg As String, ByVal stredValelab As String, _
                                         ByVal stredCodlavo As String)
    Try
      'obsoleta
      PrezzoAttualeEx(dtrTmp, lTipoElaborazione, stredListelab, stredConto, stredDatagg, stredValelab, stredCodlavo, 0)
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
  Public Overridable Sub PrezzoAttualeEx(ByVal dtrTmp As DataRow, ByVal lTipoElaborazione As Integer, _
                                         ByVal stredListelab As String, ByVal stredConto As String, _
                                         ByVal stredDatagg As String, ByVal stredValelab As String, _
                                         ByVal stredCodlavo As String, ByVal lCoddest As Integer)
    Dim dsTmp2 As DataSet = Nothing
    Dim strCodart As String
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrTmp, lTipoElaborazione, stredListelab, stredConto, stredDatagg, stredValelab, stredCodlavo, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return
      End If
      '----------------

      With dtrTmp
        strCodart = NTSCStr(!ar_codart)
        'Rettifica il codice articoli per gli articoli a variante
        If NTSCStr(!ar_gesvar) = "S" Then
          If NTSCStr(!ar_codroot) <> "" Then ' se trattasi dell'articolo ROOT codroot è vuoto
            Select Case NTSCStr(!ar_prevar)
              Case "N" : strCodart = NTSCStr(!ar_codroot)
              Case "1" : strCodart = NTSCStr(!ar_codroot) & NTSCStr(!ar_codvar1)
            End Select
          End If
        End If

        If lTipoElaborazione <> 1 AndAlso NTSCInt(stredConto) = 0 Then stredConto = NTSCStr(!lc_conto)

        oCldElar.GetPrezzoAttualeEx(strDittaCorrente, strCodart, lTipoElaborazione, stredListelab, _
                                  stredConto, stredDatagg, stredValelab, stredCodlavo, _
                                  NTSCDec(!xx_daquant), NTSCStr(!lc_unmis), NTSCStr(!lc_fase), dsTmp2, _
                                  strTipoPromo, lPromo, lCoddest)

        If dsTmp2.Tables("LISTINI").Rows.Count > 0 Then
          dIneaPrezzoattuale = NTSCDec(dsTmp2.Tables("LISTINI").Rows(0)!lc_prezzo)
          strIneaDataattuale = "Dal " & NTSCDate(dsTmp2.Tables("LISTINI").Rows(0)!lc_datagg).ToShortDateString & " al " & NTSCDate(dsTmp2.Tables("LISTINI").Rows(0)!lc_datscad).ToShortDateString
        Else
          dIneaPrezzoattuale = 0
          strIneaDataattuale = ""
        End If
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

  Public Overridable Function ConvPrezzoUnLireur(ByVal bPrzun As Boolean, ByVal dImportoin As Decimal, ByVal nCodVal As Integer, ByVal strDatavalidita As String, ByVal dCambiout As Decimal) As Decimal
    Dim dCom As Decimal
    Dim dImpval As Decimal
    Dim strOperatore As String = ""
    Dim dsTmp As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      dCom = 0
      '-----------------------------------------------------------------------------------------
      Select Case nCodVal
        Case 99
          '-------------------------------------------------------------------------------------
          '--- Legge il cambio dell'EURO
          '-------------------------------------------------------------------------------------
          dCom = 1 ' dovrebbe avvertire che non ha senso !!!
          dImpval = dImportoin
        Case 0
          dCom = 0
          dImpval = dImportoin
        Case Else ' codici valuta diversi da 99 e 0, tutte le altre (compresa la 100=LIT dopo EUR)
          oCldElar.GetTabvalu(nCodVal, dsTmp)

          If dsTmp.Tables("TABVALU").Rows.Count = 0 Then
            Return 0
          End If
          strOperatore = NTSCStr(dsTmp.Tables("TABVALU").Rows(0)!tb_moltdiv).ToUpper
          '-------------------------------------------------------------------------------------
          '--- Trovata la valuta, controlla se valuta europea ...
          '--- se si valuta se siamo nel erpiodo transitorio, e se adottiamo lire od euro come
          '--- valuta di default o principale
          '-------------------------------------------------------------------------------------
          If NTSCStr(dsTmp.Tables("TABVALU").Rows(0)!tb_ue) = "S" Then ' est 100=lire  oppre franchi, ecc.
            '--- Caso siamo ancora in lire
            ' noi siamo già in regime EUR, allora, il cambio è dato da 1/cambio euro, 6 cifre significative
            If NTSCDec(dsTmp.Tables("TABVALU").Rows(0)!tb_cambeuro) > 0 Then
              If strOperatore = "D" Then
                dImpval = dImportoin * NTSCDec(dsTmp.Tables("TABVALU").Rows(0)!tb_cambeuro)     ' perchè euro
              Else
                dImpval = dImportoin / NTSCDec(dsTmp.Tables("TABVALU").Rows(0)!tb_cambeuro)     ' perchè euro
              End If
            Else
              dImpval = 0
              dCom = 0 'per dargli un valore: dovrebbe avvertire che non ha senso...
            End If
            End If
          '-------------------------------------------------------------------------------------
          '--- Per tutte le altre valute , quelle non ue, siamo a cambio flutt., pecio cerca in cambi
          '--- tuttavia se dopo 31/12/98 devo passare per il cambio euro (triangolazione)
          '-------------------------------------------------------------------------------------
          If NTSCStr(dsTmp.Tables("TABVALU").Rows(0)!tb_ue) <> "S" Then
            dCom = dCambiout
            'Davide - errore overflow
            If dCambiout <> 0 Then
              If strOperatore = "D" Then
                dImpval = dImportoin * dCambiout
              Else
                dImpval = dImportoin / dCambiout
              End If
            Else
              dImpval = 0
            End If
          End If
      End Select
      ConvPrezzoUnLireur = dImpval

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

  Public Overridable Function ConvPrezzoUnValuta(ByVal bPrzun As Boolean, ByVal dImpval As Decimal, ByVal nCodVal As Integer, ByVal strDatavalidita As String, ByVal dCambiout As Decimal) As Decimal
    ' in bPrzun c'è true se si tratta di prezzo unitario, che va arrotondato in base al valore di gnDecSuPrzUn
    Dim dCom As Decimal
    Dim dImporto As Decimal
    Dim strOperatore As String = ""
    Dim dsTmp As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      dCom = 0
      '-----------------------------------------------------------------------------------------
      Select Case nCodVal
        Case 99
          'Legge il cambio dell'EURO
          dCom = 1 ' dovrebbe avvertire che non ha senso !!!
          dImporto = dImpval
        Case 0
          dCom = 0
          dImporto = dImpval
        Case Else ' codici valuta diversi da 99 e 0, tutte le altre (compresa la 100=LIT dopo EUR)
          oCldElar.GetTabvalu(nCodVal, dsTmp)

          If dsTmp.Tables("TABVALU").Rows.Count = 0 Then
            Return 0
          End If
          strOperatore = NTSCStr(dsTmp.Tables("TABVALU").Rows(0)!tb_moltdiv).ToUpper
          '-------------------------------------------------------------------------------------
          '--- Trovata la valuta, controlla se valuta europea ...
          '--- se si valuta se siamo nel erpiodo transitorio, e se adottiamo lire od euro come
          '--- valuta di default o principale
          '-------------------------------------------------------------------------------------
          If NTSCStr(dsTmp.Tables("TABVALU").Rows(0)!tb_ue) = "S" Then ' est 100=lire  oppre franchi, ecc.
            ' noi siamo già in regime EUR, allora, il cambio è dato da 1/cambio euro, 6 cifre significative
            If NTSCDec(dsTmp.Tables("TABVALU").Rows(0)!tb_cambeuro) > 0 Then
              If strOperatore = "D" Then
                dImporto = dImpval / NTSCDec(dsTmp.Tables("TABVALU").Rows(0)!tb_cambeuro) ' perchè euro
              Else
                dImporto = dImpval * NTSCDec(dsTmp.Tables("TABVALU").Rows(0)!tb_cambeuro) ' perchè euro
              End If
            Else
              dImporto = 0
              dCom = 0 'per dargli un valore: dovrebbe avvertire che non ha senso...
            End If
          End If
          '-------------------------------------------------------------------------------------
          '--- Per tutte le altre valute , quelle non ue, siamo a cambio flutt., pecio cerca in cambi
          '--- tuttavia se dopo 31/12/98 devo passare per il cambio euro (triangolazione)
          '-------------------------------------------------------------------------------------
          If NTSCStr(dsTmp.Tables("TABVALU").Rows(0)!tb_ue) <> "S" Then ' es. USD
            dCom = dCambiout
            If strOperatore = "D" Then
              dImporto = dImpval / dCambiout  ' prima c'era nCodval ma mi sembrava scorretto!!
            Else
              dImporto = dImpval * dCambiout
            End If
          End If
      End Select
      ConvPrezzoUnValuta = dImporto

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

  Public Overridable Function ArrotondamentoPrezzo(ByVal strCodart As String, ByVal dPrezzo1 As Decimal, _
                                                   ByVal nCodvalu As Integer, ByVal stredRicar1 As String, _
                                                   ByVal stredRicar2 As String, ByVal bopPercentuale As Boolean, _
                                                   ByVal bopRicarico As Boolean, ByVal bopScorporaIva As Boolean, _
                                                   ByVal stredArrotond As String, ByVal stredListelab As String) As Decimal
    Dim dPrezzo As Decimal
    Dim dTmp As Decimal
    Dim strRicMar As String
    Dim dsTmp As DataSet = Nothing
    Dim dsTmp1 As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      ArrotondamentoPrezzo = 0
      '-----------------------------------------------------------------------------------------
      oCldElar.GetArticoArrotonda(strDittaCorrente, strCodart, dsTmp)

      If bopPercentuale = True Then
        dPrezzo = dPrezzo1 * ((100 + NTSCDec(stredRicar1)) / 100) * ((100 + NTSCDec(stredRicar2)) / 100)
      End If
      If bopRicarico = True Then
        strRicMar = NTSCStr(dsTmp.Tables("ARTICO").Rows(0)!ar_flricmar)
        If strRicMar = "R" Then ' ricarico
          dPrezzo = dPrezzo1 * ((100 + NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_ricar1)) / 100) * ((100 + NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_ricar2)) / 100)
        Else ' margine
          If (NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_ricar1) - NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_ricar2)) >= 100 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128691624122150550, "Nel caso di 'margine' le percentuali non possono essere superiori o uguali a 100 (articolo |" & strCodart & "|) !")))
            dPrezzo = dPrezzo1
          Else
            dPrezzo = dPrezzo1 / ((100 - NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_ricar1) - NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_ricar2)) / 100)
          End If
        End If
      End If
      If bopScorporaIva = True Then
        If Not (NTSCStr(dsTmp.Tables("ARTICO").Rows(0)!tb_aliq) = "") Then
          dPrezzo = (dPrezzo1 * 100) / (100 + NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!tb_aliq))
        Else
          dPrezzo = dPrezzo1
        End If
      End If
      If (NTSCInt(dsTmp.Tables("ARTICO").Rows(0)!ar_codpdon) = 0) Then
        If NTSCDec(stredArrotond) <> 0 Then
          If NTSCDec(Format((dPrezzo / NTSCDec(stredArrotond)), "#,##0.00000")) = NTSCDec(Format(Fix(dPrezzo / NTSCDec(stredArrotond)), "#,##0.00000")) Then
            ArrotondamentoPrezzo = dPrezzo
          Else
            dTmp = dPrezzo / NTSCDec(stredArrotond)
            If strTipoArro = "E" Then 'eccesso
              dTmp = ArrDblEcc(dTmp, 0)
            Else 'matematico
              dTmp = ArrDbl(dTmp, 0)
            End If
            ArrotondamentoPrezzo = dTmp * NTSCDec(stredArrotond)
          End If
        Else
          ArrotondamentoPrezzo = 0
        End If
      Else
        oCldElar.GetTabpdon(strDittaCorrente, NTSCStr(dsTmp.Tables("ARTICO").Rows(0)!ar_codpdon), stredListelab, dsTmp1)

        If dsTmp1.Tables("TABPDON").Rows.Count > 0 Then
          If (NTSCInt(stredListelab) >= 1) And (NTSCInt(stredListelab) <= 8) Then
            If NTSCDec(dsTmp1.Tables("TABPDON").Rows(0)("tb_arro_" & stredListelab)) <> 0 Then
              If NTSCDec(Format((dPrezzo / NTSCDec(dsTmp1.Tables("TABPDON").Rows(0)("tb_arro_" & stredListelab))), "#,##0.00000")) = NTSCDec(Format(Fix(dPrezzo / NTSCDec(dsTmp1.Tables("TABPDON").Rows(0)("tb_arro_" & stredListelab))), "#,##0.00000")) Then
                ArrotondamentoPrezzo = dPrezzo
              Else
                dTmp = dPrezzo / NTSCDec(dsTmp1.Tables("TABPDON").Rows(0)("tb_arro_" & stredListelab))
                If strTipoArro = "E" Then 'eccesso
                  dTmp = ArrDblEcc(dTmp, 0)
                Else 'matematico
                  dTmp = ArrDbl(dTmp, 0)
                End If
                ArrotondamentoPrezzo = dTmp * NTSCDec(dsTmp1.Tables("TABPDON").Rows(0)("tb_arro_" & stredListelab))
              End If
            Else
              '--- Se in TABPDON tb_arro è = 0 mette di default 1
              If NTSCDec(Format((dPrezzo / 1), "#,##0.00000")) = NTSCDec(Format(Fix(dPrezzo / 1), "#,##0.00000")) Then
                ArrotondamentoPrezzo = dPrezzo
              Else
                dTmp = dPrezzo / 1
                If strTipoArro = "E" Then 'eccesso
                  dTmp = ArrDblEcc(dTmp, oCldElar.TrovaNdecSuPrzUn(nCodvalu))
                Else 'matematico
                  dTmp = ArrDbl(dTmp, oCldElar.TrovaNdecSuPrzUn(nCodvalu))
                End If
                ArrotondamentoPrezzo = dTmp * 1
              End If
            End If
          Else
            If NTSCDec(dsTmp1.Tables("TABPDON").Rows(0)!tb_arro_1) <> 0 Then
              If NTSCDec(Format((dPrezzo / NTSCDec(dsTmp1.Tables("TABPDON").Rows(0)!tb_arro_1)), "#,##0.00000")) = NTSCDec(Format(Fix(dPrezzo / NTSCDec(dsTmp1.Tables("TABPDON").Rows(0)!tb_arro_1)), "#,##0.00000")) Then
                ArrotondamentoPrezzo = dPrezzo
              Else
                dTmp = dPrezzo / NTSCDec(dsTmp1.Tables("TABPDON").Rows(0)!tb_arro_1)
                If strTipoArro = "E" Then 'eccesso
                  dTmp = ArrDblEcc(dTmp, 0)
                Else 'matematico
                  dTmp = ArrDbl(dTmp, 0)
                End If
                ArrotondamentoPrezzo = dTmp * NTSCDec(dsTmp1.Tables("TABPDON").Rows(0)!tb_arro_1)
              End If
            Else
              '--- Se in TABPDON tb_arro è = 0 mette di default 1
              If NTSCDec(Format((dPrezzo / 1), "#,##0.00000")) = NTSCDec(Format(Fix(dPrezzo / 1), "#,##0.00000")) Then
                ArrotondamentoPrezzo = dPrezzo
              Else
                dTmp = dPrezzo / 1
                If strTipoArro = "E" Then 'eccesso
                  dTmp = ArrDblEcc(dTmp, oCldElar.TrovaNdecSuPrzUn(nCodvalu))
                Else 'matematico
                  dTmp = ArrDbl(dTmp, oCldElar.TrovaNdecSuPrzUn(nCodvalu))
                End If
                ArrotondamentoPrezzo = dTmp * 1
              End If
            End If
          End If
        Else
          If NTSCDec(stredArrotond) <> 0 Then
            If NTSCDec(Format((dPrezzo / NTSCDec(stredArrotond)), "#,##0.00000")) = NTSCDec(Format(Fix(dPrezzo / NTSCDec(stredArrotond)), "#,##0.00000")) Then
              ArrotondamentoPrezzo = dPrezzo
            Else
              dTmp = dPrezzo / NTSCDec(stredArrotond)
              If strTipoArro = "E" Then 'eccesso
                dTmp = ArrDblEcc(dTmp, 0)
              Else 'matematico
                dTmp = ArrDbl(dTmp, 0)
              End If
              ArrotondamentoPrezzo = dTmp * NTSCDec(stredArrotond)
            End If
          Else
            ArrotondamentoPrezzo = 0
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

  Public Overridable Function VisualizzaRisultati() As Boolean
    Try
      ThrowRemoteEvent(New NTSEventArgs("VISRISULT:", ""))

      If nVrisRichiesta = 0 Then
        Return False
      Else
        Return True
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

  Public Overridable Sub AggiungiColonneUnbound(ByRef ds As DataSet)
    Dim i As Integer
    Dim strTmp As String = ""
    Try
      'colonne aggiuntive per i campi
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_codartlistini", GetType(String)))
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_nuovovalore", GetType(Decimal)))
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_daquant", GetType(Decimal)))
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_aquant", GetType(Decimal)))
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_prezzobase", GetType(Decimal)))

      'colonne aggiuntive per i campi di decodifica della form modale
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_codart", GetType(String)))
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_conto", GetType(String)))
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_fase", GetType(String)))
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_codlavo", GetType(String)))
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_codvalu", GetType(String)))
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_codtpro", GetType(String)))

      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_percvar", GetType(Decimal)))
      ds.Tables("LISTINI").Columns.Add(New DataColumn("xx_coddest", GetType(String)))

      For i = 0 To ds.Tables("LISTINI").Rows.Count - 1
        With ds.Tables("LISTINI").Rows(i)
          strTmp = ""
          Dim dttTmp As New datatable
          If Not oCldElar.ValCodiceDb(NTSCStr(!lc_codart), strDittaCorrente, "ARTICO", "S", strTmp, dttTmp) Then
            !xx_codart = ""
          Else
            !xx_codart = strTmp & " " & ntscstr(dttTmp.rows(0)!ar_desint)
          End If
          strTmp = ""
          If NTSCInt(!lc_conto) = 0 OrElse Not oCldElar.ValCodiceDb(NTSCStr(!lc_conto), strDittaCorrente, "ANAGRA", "N", strTmp) Then
            !xx_conto = ""
          Else
            !xx_conto = strTmp
          End If
          strTmp = ""
          If NTSCInt(!lc_fase) = 0 OrElse Not oCldElar.ValCodiceDb(NTSCStr(!lc_fase), strDittaCorrente, "ARTFASI", "N", strTmp, Nothing, NTSCStr(!lc_codart)) Then
            !xx_fase = ""
          Else
            !xx_fase = strTmp
          End If
          strTmp = ""
          If NTSCInt(!lc_codlavo) = 0 OrElse Not oCldElar.ValCodiceDb(NTSCStr(!lc_codlavo), strDittaCorrente, "TABLAVO", "N", strTmp) Then
            !xx_codlavo = ""
          Else
            !xx_codlavo = strTmp
          End If
          strTmp = ""
          If NTSCInt(!lc_codvalu) = 0 OrElse Not oCldElar.ValCodiceDb(NTSCStr(!lc_codvalu), strDittaCorrente, "TABVALU", "N", strTmp) Then
            !xx_codvalu = ""
          Else
            !xx_codvalu = strTmp
          End If
          strTmp = ""
          If NTSCInt(!lc_codtpro) = 0 OrElse Not oCldElar.ValCodiceDb(NTSCStr(!lc_codtpro), strDittaCorrente, "TABTPRO", "N", strTmp) Then
            !xx_codtpro = ""
          Else
            !xx_codtpro = strTmp
          End If
          strTmp = ""
          If NTSCInt(!lc_coddest) <= 0 OrElse NTSCInt(!lc_conto) = 0 OrElse Not oCldElar.ValCodiceDb(NTSCStr(!lc_coddest), strDittaCorrente, "DESTDIV", "N", strTmp, Nothing, NTSCInt(!lc_conto).ToString) Then
            !xx_coddest = ""
          Else
            !xx_coddest = strTmp
          End If
          !xx_percvar = 0
        End With
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

#Region "funzioni specifiche per BNMGELAR.BNMGADOC.VB"
  Public Overridable Function ComponiQueryMovmag(ByVal strcbTipork As String, ByVal stredAnno As String, _
                                                 ByVal stredSerieini As String, ByVal stredSeriefin As String, _
                                                 ByVal stredNumdocini As String, ByVal stredNumdocfin As String, _
                                                 ByVal strTestata As String, ByVal strRighe As String) As Boolean
    Try
      oCldElar.ComponiQueryMovmag(strDittaCorrente, strcbTipork, stredAnno, stredSerieini, stredSeriefin, _
                                  stredNumdocini, stredNumdocfin, strTestata, strRighe, strAdocQuery)

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CheckMovimenti(ByVal strcbTipork As String, ByVal stredAnno As String, _
                                             ByVal stredSerieini As String, ByVal stredSeriefin As String, _
                                             ByVal stredNumdocini As String, ByVal stredNumdocfin As String, _
                                             ByVal strTestata As String, ByVal strRighe As String, _
                                             ByRef ds As DataSet) As Boolean
    Try
      oCldElar.CheckMovimenti(strDittaCorrente, strcbTipork, stredAnno, stredSerieini, stredSeriefin, _
                                  stredNumdocini, stredNumdocfin, strTestata, strRighe, ds)

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNMGELAR.BNMGINEA.VB"
  Public Overridable Function edConto_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

      If Not oCldElar.ValCodiceDb(nCod.ToString, strDittaCorrente, "ANAGRA", "N", strDescr) Then
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
  Public Overridable Function edCoddest_Validated(ByVal nCod As Integer, ByVal lConto As Integer, ByRef strDescr As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

      If Not oCldElar.ValCodiceDb(nCod.ToString, strDittaCorrente, "DESTDIV", "N", strDescr, Nothing, lConto.ToString) Then
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
  Public Overridable Function edCodart_Validated(ByVal strCod As String, ByRef strDescr As String, _
                                                 ByRef dttTmp As DataTable) As Boolean
    Try
      If Not oCldElar.ValCodiceDb(strCod, strDittaCorrente, "ARTICO", "S", strDescr, dttTmp) Then
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
  Public Overridable Function Codvalu_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If Not oCldElar.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABVALU", "N", strDescr) Then
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
  Public Overridable Function edFase_Validated(ByVal nCod As Integer, ByVal strCodArt As String, ByRef strDescr As String) As Boolean
    Try
      If Not oCldElar.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "ARTFASI", "N", strDescr, , strCodArt) Then
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

  Public Overridable Function GetEsistenza(ByVal stredCodart As String, ByRef strlbEsist As String, _
                                           ByRef stredEsist As String, ByRef stredDtulcar As String) As Boolean
    Dim dsTmp As DataSet = Nothing
    Dim dsTmp1 As DataSet = Nothing
    Try
      oCldElar.GetEsistenza(strDittaCorrente, stredCodart, dsTmp, dsTmp1)

      If dsTmp.Tables("ARTPRO").Rows.Count > 0 Then
        strlbEsist = "Esistenza magazzino " & NTSCStr(dsTmp.Tables("ARTPRO").Rows(0)!ap_magaz)
        stredEsist = NTSCStr(dsTmp.Tables("ARTPRO").Rows(0)!ap_esist)
      Else
        strlbEsist = "Esistenza"
        stredEsist = "0"
      End If

      If dsTmp1.Tables("ARTPROX").Rows.Count > 0 Then
        stredDtulcar = NTSCStr(IIf(NTSCStr(dsTmp1.Tables("ARTPROX").Rows(0)!apx_dtulcar) = "", "", NTSCDate(dsTmp1.Tables("ARTPROX").Rows(0)!apx_dtulcar).ToShortDateString))
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
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGVRIS.VB"
  Public Overridable Function VrisApri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Try
      ds = dsShared

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("LISTINI").ColumnChanging, AddressOf VrisBeforeColUpdate
      AddHandler dsShared.Tables("LISTINI").ColumnChanged, AddressOf VrisAfterColUpdate

      bVrisHasChanges = False

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
  Public Overridable Function VrisSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not VrisTestPreSalva() Then Return False
      End If

      dsShared.Tables("LISTINI").AcceptChanges()

      bVrisHasChanges = False

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
  Public ReadOnly Property VrisRecordIsChanged() As Boolean
    Get
      Return bVrisHasChanges
    End Get
  End Property
  Public Overridable Function VrisTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsShared.Tables("LISTINI").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1

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
  Public Overridable Function VrisRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("LISTINI").Select(strFilter)(nRow).RejectChanges()
      bVrisHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub VrisBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
      Dim strFunction As String = "VrisBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub VrisAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bVrisHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "VrisAfterColUpdate_" & e.Column.ColumnName.ToLower
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
#End Region

End Class
