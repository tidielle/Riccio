Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEVEFADI
    Inherits CLE__BASN

  Private Moduli_P As Integer = bsModVE + bsModMG
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

  Public oCldFadi As CLDVEFADI
  Public oCleFdin As CLEVEFDIN

  Public strActLog As String = ""
  Public strActLogProg As String = ""
  Public strActLogNomOggLog As String = ""
  Public strActLogDesLog As String = ""

  Public strNoteFatture As String = ""

  'opzioni di registro
  'Opzioni registro
  Public strNoteFattDiff As String = ""
  Public bRifBolleInNote As Boolean = False
  Public bUsaContoFatt As Boolean = False
  Public bOrdinaPerNumdoc As Boolean = False
  Public bCeas As Boolean = False 'pers ceas
  Public bAngel As Boolean = False 'pers Angelini Renzo Roberto
  Public bIgnoraTipobf As Boolean = False
  Public bProteggiDocContab As Boolean = True
  Public bRiproponiDataDoc As Boolean = False
  Public bRifBolleInNoteNoHeader As Boolean = False
  Public strUsaContoFattDoc As String = ""
  Public nNumMaxDDT As Integer = 200
  Public strCambioCodiciPagElenco As String = ""
  Public dCambioCodiciPagSoglia As Decimal = 0
  Public nGestScostAcconti As Integer = 0
  Public dImpoScostAcconti As Decimal = 0
  Public bGestPVR As Boolean = False
  Public bCalcolaRagg As Boolean = False
  Public bUsaKeymag As Boolean = False
  Public bCancellaRegCG As Boolean = False
  Public bORTO_ChiamaBsjoboll As Boolean = False
  Public strTipoFatturazione As String = "B"
  Public strVettCodpaga() As String = Nothing ' per codici pag., da split
  Public nDacodpaga(1000) As Integer
  Public nAcodpaga(1000) As Integer
  Public bCONADOmaggiImpIvaInTipocess7 As Boolean = False
  Public bModRSM As Boolean = False
  Public strControlloPIvaCodFis As String = ""
  Public bLogRidotto As Boolean = False

  Public dsShared As DataSet = Nothing
  Public dttDDTs As New DataTable       'elenco dei DDT ce dovranno essere riepilogati (alimentato solo se spuntato Visualizza estremi DDT prima dell'elaborazione)
  Public dsFdin As New DataSet
  Public dttAnaz As New DataTable
  Public lIITtkeys As Integer = 0
  Public strDDTCoinvolti As String = ""

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDVEFADI"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldFadi = CType(MyBase.ocldBase, CLDVEFADI)
    oCldFadi.Init(oApp)

    Return True
  End Function

  Public Overridable Function InitExt() As Boolean
    Dim lTmp As Integer = 0
    Dim ii As Integer = 0
    Dim kk As Integer = 0
    Dim jj As Integer = 0
    Try
      If Not oCldFadi.ValCodiceDb("1", strDittaCorrente, "TABPEVE", "N", "") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101093750, "Tabella 'Personalizzazione Vendite' non compilata. Impostarla prima di proseguire")))
        Return False
      End If

      bModRSM = False
      If CBool(ModuliDittaDitt(strDittaCorrente) And bsModSM) Then bModRSM = True

      '------------------------
      'predispongo l'ambiente
      'Legge il registro
      strNoteFattDiff = oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "NoteNuoviDocumenti", "", " ", "")
      bRifBolleInNote = CBool(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "RiferimBolleSuNote", "0", " ", "0"))
      bUsaContoFatt = CBool(Val(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "UsaContoFatt", "0", " ", "0")))
      bOrdinaPerNumdoc = CBool(Val(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "OrdinaPerNumdoc", "0", " ", "0")))
      bCeas = CBool(Val(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "Ceas", "0", " ", "0"))) 'pers ceas   ' NON DOCUMENTARE
      bAngel = CBool(Val(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "Angel", "0", " ", "0"))) 'pers Angelini Renzo Roberto da non documentare
      bIgnoraTipobf = CBool(Val(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "IgnoraTipobf", "0", " ", "0"))) '-1 = ignora Tipobf 0= Normale
      nGestScostAcconti = NTSCInt(Val(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "GestScostAcconti", "0", " ", "0"))) '0=nessuno, 1=come abbuono, 2=rettifica acconto
      dImpoScostAcconti = NTSCDec(IntSetNum(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "ImpoScostAcconti", IntSetNum("0,05"), " ", IntSetNum("0,05")))) 'soglia massima azione precedente proprietà
      bProteggiDocContab = CBool(Val(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "ProteggiDocContab", "-1", " ", "-1"))) '-1=chiede la password x i documenti contabilizzati
      bRiproponiDataDoc = CBool(Val(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "RiproponiDataDoc", "0", " ", "0"))) '-1=proponi la data del prec. documento creato, 0=proponi sempre la data del sistema
      'leggo da opzioni generali se l'azienda gestisce i PVR
      bGestPVR = CBool(oCldFadi.GetSettingBus("OPZIONI", ".", ".", "GestPVR", "0", " ", "0"))
      bCalcolaRagg = CBool(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "CalcolaTotaliRaggruppandoMM", "0", " ", "0")) '0 default,-1 = calcola i totali raggruppando per movmag iva, controp, ...  ' NON DOCUMENTARE
      bRifBolleInNoteNoHeader = CBool(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "RiferimBolleSuNoteNoHeader", "0", " ", "0")) '0 default= antepone la scritta "Rif. DDT",-1 = non antepone alcuna stringa di riferimento
      strUsaContoFattDoc = oCldFadi.GetSettingBus("BSVEFADI", "OPZIONI", ".", "UsaContoFattDoc", "0", " ", "0") 'Se impostata, preleva il conto fatturazione del documento al posto di quello indicato in anagrafica
      strControlloPIvaCodFis = oCldFadi.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "ControlloPIvaCodFis", NTSCStr(IIf(bModRSM, "0", "1")), " ", NTSCStr(IIf(bModRSM, "0", "1")))
      '-----------------------------------------------------------------------
      bUsaKeymag = CBool(Val(oCldFadi.GetSettingBus("BSVEFADI", "OPZIONI", ".", "UsaKeymag", "0", " ", "0")))
      nNumMaxDDT = 200
      lTmp = NTSCInt(Val(oCldFadi.GetSettingBus("BSVEFADI", "OPZIONI", ".", "NumMaxDDT", "200", " ", "200"))) 'opzione di registro con la quale si parametrizza il num. massimo di DDT gestiti dalla fattura diff. (in apertura lista ddt collegati)
      If lTmp < 200 Or lTmp > 1000 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128747818701712000, "Nella opzione di registro \Bsvefadi\Opzioni\NumMaxDDT è indicato il valore non ammesso |" & lTmp.ToString & "|. Inicare un valore compreso tra 200 a 1000. Si procede con il valore predefinito 200.")))
        nNumMaxDDT = 200
      Else
        nNumMaxDDT = CInt(lTmp)
      End If
      bCancellaRegCG = CBool(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "CancellaRegCG", "0", " ", "0"))
      ' cambio codici pag da certa soglia in poi
      strCambioCodiciPagElenco = oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "CambioCodiciPagElenco", "", " ", "") ' NON DOCUMENTARE
      dCambioCodiciPagSoglia = NTSCDec(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "CambioCodiciPagSoglia", "0", " ", "0")) ' NON DOCUMENTARE
      If strCambioCodiciPagElenco <> "" Then
        strVettCodpaga = strCambioCodiciPagElenco.Split(";"c)
        For ii = 0 To strVettCodpaga.Length - 1
          jj = NTSCInt(Fix((ii + 2) / 2)) ' indice vettore trasformazione codici
          kk = NTSCInt(Fix((ii + 1) / 2)) ' se kk = jj allora  ii è numero pari
          If jj = kk Then
            nAcodpaga(kk) = NTSCInt(strVettCodpaga(ii))
          Else
            nDacodpaga(jj) = NTSCInt(strVettCodpaga(ii))
          End If
        Next ii
      End If
      bORTO_ChiamaBsjoboll = CBool(Val(oCldFadi.GetSettingBus("BSVEFADI", "OPZIONI", ".", "ORTO_ChiamaBsjoboll", "0", " ", "0"))) '-1 = Chiama BSJOBOLL in Modifica documento (anziché BSVEBOLL o BSREGDOC)  ' NON DOCUMENTARE
      strTipoFatturazione = oCldFadi.GetSettingBus("BSVEFADI", "OPZIONI", ".", "TipoFatturazione", "B", " ", "B")  ' NON DOCUMENTARE
      bCONADOmaggiImpIvaInTipocess7 = CBool(oCldFadi.GetSettingBus("BSVEFADI", "Opzioni", ".", "CONADOmaggiImpIvaInTipocess7", "0", " ", "0")) '0 default= le righe omaggio (imp+iva) vengono esportate con TipoCessione=6,-1 = le righe Omaggio (Imp+Iva) vengono esportate con TipoCessione=7
      bLogRidotto = CBool(oCldFadi.GetSettingBus("BSVEFADI", "Opzioni", ".", "LogRidotto", "0", " ", "0"))

      oCldFadi.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttAnaz)
      If CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupCAE) Then lIITtkeys = oCldFadi.GetTblInstId("TTKEYS", False)

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


  Public Overridable Function CreaFdin() As Boolean
    Try
      '------------------------
      'inizializzo BEVEFDIN
      ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 128764816973482000, "Inizializzazione motore per generazione fatture ...")))
      If Not oCleFdin Is Nothing Then Return True

      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEVEFADI", "BEVEFDIN", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleFdin = CType(oTmp, CLEVEFDIN)
      '------------------------------------------------
      AddHandler oCleFdin.RemoteEvent, AddressOf GestisciEventiEntityFdin
      If oCleFdin.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return False
      oCleFdin.bCalcolaRagg = bCalcolaRagg
      oCleFdin.bVefadi = True     'così in vefdin non esegue i test 'VerificaVincoli e VerificaDeroghe'

      If Not oCleFdin.LeggePevePeac Then Return False
      If Not oCleFdin.LeggiRegistro Then Return False
      oCleFdin.bNoUpdateTipobf = True   'da FADI il tipobf del ddt non deve venir mai aggiornato

      If Not oCleFdin.ApriDoc(strDittaCorrente, True, "B", 0, " ", 0, dsFdin) Then Return False

      oCleFdin.strControlloPIvaCodFis = strControlloPIvaCodFis

      ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", ".".PadRight(100)))

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

  Public Overridable Sub GestisciEventiEntityFdin(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      'gli eventuali messaggi dati da BEVEFDIN tramite la ThrowRemoteEvent li passo a BNVEFADI
      'solo se non sono messaggi dove viene chiesta una conferma ...
      If e.TipoEvento = "" Then
        If strDDTCoinvolti.Trim <> "" Then LogWrite(strDDTCoinvolti, True) : strDDTCoinvolti = ""
        LogWrite(oApp.Tr(Me, 128843572533598000, "ERROR: ") & e.Message, True)
      ElseIf e.TipoEvento = CLN__STD.ThMsg.MSG_INFO Then
        If strDDTCoinvolti.Trim <> "" Then LogWrite(strDDTCoinvolti, True) : strDDTCoinvolti = ""
        LogWrite(oApp.Tr(Me, 128843573247444000, "INFO: ") & e.Message, True)
      Else
        ThrowRemoteEvent(e)
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

  Public Overridable Function PrimoNumdoc(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String) As Integer
    Try
      '---------------------
      'propone il primo numero documento
      Select Case strTipork
        Case "D", "£"
          Return oCldFadi.LegNuma(strDittaCorrente, "A", strSerie, nAnno, False)
        Case "P"
          Return oCldFadi.LegNuma(strDittaCorrente, "S", strSerie, nAnno, False)
      End Select

      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return 0
    End Try
  End Function

  Public Overridable Function SefdCheckPreConferma() As Boolean
    Try
      '-------------------------------
      'eseguo i test preliminari pre-conferma filtri di nuovo/rielabora


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

  Public Overridable Function TestPreRielabora() As Boolean
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY
    Try
      If bUsaContoFatt Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128764123122481000, "La rielaborazione non è consentita quando è abilitato l'utilizzo del conto fatturazione (opzione di registro \Bsvefadi\Opzioni\UsaContoFatt = -1). Effettuare la cancellazione delle fatture quindi la successiva nuova creazione.")))
        Return False
      End If
      'Controlla che NON sia abilitata l'opzione UsaContoFattDoc
      If strUsaContoFattDoc <> "0" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128764123365217000, "La rielaborazione non è consentita quando è abilitato l'utilizzo del conto fatturazione documento (opzione di registro \Bsvefadi\Opzioni\UsaContoFattDoc diverso da 0). Effettuare la cancellazione delle fatture quindi la successiva nuova creazione.")))
        Return False
      End If

      oDttgr.NTSGroupBy(dsShared.Tables("FADI"), dttGr, "tm_conto", "xx_seleziona = 'S'", "tm_conto")
      If dttGr.Rows.Count > 1 Then
        dttGr.Clear()
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128764135499477000, "E' possibile rielaborare solo documenti dello stesso cliente.")))
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
    Finally
      dttGr.Clear()
    End Try
  End Function

  Public Overridable Function GettDDTDaElab(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                    ByVal dtDatdoc As DateTime, ByVal lFirstNumdoc As Integer, ByVal bProva As Boolean, _
                                    ByVal strCheckRighe As String, ByVal strWhereTestmag As String, _
                                    ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                    ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                    ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                    ByRef strFatt As String, ByVal bRielabora As Boolean, _
                                    ByVal bForzaDataDiversa As Boolean, ByVal strDataDiversa As String) As Boolean
    'ottengo l'elenco dei DDT che dovranno essere elaborati
    Dim dttTmp As New DataTable
    Try
      '----------------------------
      'test righe con prezzi = 0
      If strCheckRighe <> "0" Then
        If Not oCldFadi.GetRighePrezziZeroNewDoc(strDittaCorrente, strWhereTestmag, strWhereAnagra, nWhereLista, _
                                                lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                                                strCheckRighe, dttTmp) Then Return False
        If dttTmp.Rows.Count > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130003185257548712, "ATTENZIONE: sono presenti delle righe nei documenti da riepilogare con prezzi a 0. Per ottenerne l'elenco rieseguire l'elaborazione senza spuntare 'Visualizza estremi DDT prima dell'elaboraizone'")))
          Return False
        End If
      End If    'If strCheckRighe <> "0" Then


      If Not oCldFadi.GetDDTDaElab(strDittaCorrente, strWhereTestmag, strWhereAnagra, nWhereLista, _
                                   lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                                   strCheckRighe, dttDDTs) Then Return False

      If dttDDTs.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130003185220740345, "Non è stato trovato nessun documento da riepilogare con i criteri di selezione definiti.")))
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


  Public Overridable Function GeneraDoc(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                        ByVal dtDatdoc As DateTime, ByVal lFirstNumdoc As Integer, ByVal bProva As Boolean, _
                                        ByVal strCheckRighe As String, ByVal strWhereTestmag As String, _
                                        ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                        ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                        ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                        ByRef strFatt As String) As Boolean
    Try
      Return GeneraDoc(strTipork, nAnno, strSerie, dtDatdoc, lFirstNumdoc, bProva, strCheckRighe, strWhereTestmag, _
                         strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                         strFatt, False)
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
  Public Overridable Function GeneraDoc(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                      ByVal dtDatdoc As DateTime, ByVal lFirstNumdoc As Integer, ByVal bProva As Boolean, _
                                      ByVal strCheckRighe As String, ByVal strWhereTestmag As String, _
                                      ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                      ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                      ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                      ByRef strFatt As String, ByVal bRielabora As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTipork, nAnno, strSerie, dtDatdoc, lFirstNumdoc, bProva, strCheckRighe, strWhereTestmag, _
                         strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                         strFatt, bRielabora})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strFatt = NTSCStr(oIn(14))
        Return CBool(oOut)
      End If
      '----------------

      Return GeneraDoc(strTipork, nAnno, strSerie, dtDatdoc, lFirstNumdoc, bProva, strCheckRighe, strWhereTestmag, _
                         strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                         strFatt, bRielabora, False, "")
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
  Public Overridable Function GeneraDoc(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                        ByVal dtDatdoc As DateTime, ByVal lFirstNumdoc As Integer, ByVal bProva As Boolean, _
                                        ByVal strCheckRighe As String, ByVal strWhereTestmag As String, _
                                        ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                        ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                        ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                        ByRef strFatt As String, ByVal bRielabora As Boolean, _
                                        ByVal bForzaDataDiversa As Boolean, ByVal strDataDiversa As String) As Boolean
    Dim dttTmp As New DataTable
    Dim dttTesta As New DataTable
    Dim dttDDT As New DataTable
    Dim strRiga As String = ""
    Dim nFat As Integer = 0
    Dim lNumfat As Integer = lFirstNumdoc
    Dim strNoted As String = ""
    Dim i As Integer = 0
    Dim nTmpCodpaga As Integer = 0
    Dim strDesogglog As String = ""
    Dim strTmp As String = ""
    Dim bMostraLogPrezziZero As Boolean = False
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTipork, nAnno, strSerie, dtDatdoc, lFirstNumdoc, bProva, strCheckRighe, strWhereTestmag, _
                         strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                         strFatt, bRielabora, bForzaDataDiversa, strDataDiversa})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strFatt = NTSCStr(oIn(14))
        Return CBool(oOut)
      End If
      '----------------

      If bProva Then lNumfat = -1
      strFatt = ""

      '----------------------------
      'già iniziato a scriviere dalla 'CancellaFatture'
      If Not LogStart("BNVEFADI", oApp.Tr(Me, 130366749106671324, "Fatturazione differita"), bRielabora, CBool(IIf(bRielabora, True, False))) Then Return False

      '----------------------------
      'creo il motore per generare le fatture (se non fatto in precedenza)
      If Not CreaFdin() Then Return False

      '----------------------------
      'test righe con prezzi = 0
      If strCheckRighe <> "0" Then
        If Not oCldFadi.GetRighePrezziZeroNewDoc(strDittaCorrente, strWhereTestmag, strWhereAnagra, nWhereLista, _
                                                lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                                                strCheckRighe, dttTmp) Then Return False
        If dttTmp.Rows.Count > 0 Then
          strRiga = oApp.Tr(Me, 130366749237607986, "------------ CONTROLLO RIGHE CON PREZZI A ZERO -----------------") & vbCrLf
          Select Case strCheckRighe
            Case "1" : strRiga += oApp.Tr(Me, 130366749371044632, "--> ---------- SU TUTTE LE RIGHE TRANNE ARTICOLI 'D' ---------------") & vbCrLf
            Case "2" : strRiga += oApp.Tr(Me, 130366749531981102, "--> ---- SU TUTTE LE RIGHE TRANNE ARTICOLI NON A MAGAZZINO ---------") & vbCrLf
            Case "3" : strRiga += oApp.Tr(Me, 130366749667761483, "--> ----- SU TUTTE LE RIGHE CON QUANTITA' DIVERSA DA ZERO ----------") & vbCrLf
          End Select
          strRiga += oApp.Tr(Me, 130366749813854298, "Tipo / Anno / Serie / Numero documento / Riga / Articolo / Descrizione") & vbCrLf & vbCrLf
          For Each dtrT As DataRow In dttTmp.Rows
            strRiga += dtrT!mm_tipork.ToString & " " & dtrT!mm_anno.ToString & " " & dtrT!mm_serie.ToString & _
                       NTSCInt(dtrT!mm_numdoc).ToString("000000000") & " " & NTSCInt(dtrT!mm_riga).ToString("0000") & _
                       dtrT!mm_codart.ToString & " " & NTSCStr(dtrT!mm_descr) & vbCrLf
          Next
          dttTmp.Clear()
          Dim eaTmp As New NTSEventArgs("PREZZIZERO", "")
          ThrowRemoteEvent(eaTmp)
          If eaTmp.RetValue = ThMsg.RETVALUE_YES Then
            bMostraLogPrezziZero = True
            LogWrite(strRiga, False)
          End If
          Return False
        End If
      End If    'If strCheckRighe <> "0" Then

      '----------------------------
      'ottengo l'elenco delle testate dei documenti da generare
      If Not oCldFadi.GetTestateNewDoc(strDittaCorrente, bUsaContoFatt, strUsaContoFattDoc, bIgnoraTipobf, strTipoFatturazione, _
                                      bOrdinaPerNumdoc, strWhereTestmag, strWhereAnagra, nWhereLista, lWhereNumdocDa, _
                                      lWhereNumdocA, strWhereSerieDa, strWhereSerieA, dttTesta, bForzaDataDiversa, _
                                      strDataDiversa) Then Return False

      If dttTesta.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128758229534996000, "Non è stato trovato nessun documento da riepilogare con i criteri di selezione definiti.")))
        Return False
      End If

      '------------------------------
      'se c'è la CA2 eseguo i test del caso
      If CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupCAE) Then
        '----------------------------
        'non posso modificare/cancellare se in tabanaz la data congelamento CA è successiva alla data del documento
        If NTSCDate(dttAnaz.Rows(0)!tb_dtulaca) >= NTSCDate(dtDatdoc.ToShortDateString) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129276269539743438, "ATTENZIONE: con il modulo della 'Contabilità analitica duplice contabile' attivato non è possibile inserire/modificare/cancellare documenti con data NON SUCCESSIVA alla 'data ultimo aggiornamento CA' indicata in anagrafica ditta(|" & NTSCDate(dttAnaz.Rows(0)!tb_dtulaca).ToShortDateString & "|)")))
          Return False
        End If
      End If

      For nFat = 0 To dttTesta.Rows.Count - 1
        If bUsaContoFatt = False Then
          ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 128762300099410000, "Creazione fattura |" & (nFat + 1).ToString & "| di |" & dttTesta.Rows.Count.ToString & "| in corso ...")))
          If Not bLogRidotto Then LogWrite(oApp.Tr(Me, 128843566209814000, "Creazione fattura |" & (nFat + 1).ToString & "| di |" & dttTesta.Rows.Count.ToString & "| in corso ..."), False)
        Else
          ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 129307606767676421, "Creazione fattura |" & (nFat + 1).ToString & "| in corso ...")))
          If Not bLogRidotto Then LogWrite(oApp.Tr(Me, 129307610322627379, "Creazione fattura |" & (nFat + 1).ToString & "| in corso ..."), False)
        End If

        '-------------------------------
        'ottengo le testate dei DDT della fattura da generare
        'se in prova genero le fatture loopando al contrario, visto che il numero documento è negativo
        'ed in stampa viene ordinato per numero documento
        If Not oCldFadi.GetTestateDDTfattura(strDittaCorrente, _
                                             dttTesta.Rows(NTSCInt(IIf(bProva, dttTesta.Rows.Count - 1 - nFat, nFat))), _
                                             strUsaContoFattDoc, bIgnoraTipobf, _
                                             strWhereTestmag, strWhereAnagra, nWhereLista, lWhereNumdocDa, _
                                             lWhereNumdocA, strWhereSerieDa, strWhereSerieA, dttDDT) Then GoTo Prosegui

        strTmp = ""
        For Each dtrT As DataRow In dttDDT.Rows
          strTmp += ": " & dtrT!tm_tipork.ToString & "-" & dtrT!tm_anno.ToString & "-" & dtrT!tm_serie.ToString & "-" & dtrT!tm_numdoc.ToString
        Next
        If bLogRidotto Then
          strDDTCoinvolti = oApp.Tr(Me, 128843579324644000, "DDT coinvolti") & strTmp
        Else
          LogWrite(oApp.Tr(Me, 128843579324644001, "DDT coinvolti") & strTmp, False)
        End If

        'rimuovo i ddt che sono stati tolti dalla form di pre-elaborazione (FRMVEDDTS)
        If dttDDTs.Rows.Count > 0 Then
          For Each dtrT As DataRow In dttDDT.Rows
            If dttDDTs.Select("tm_tipork = '" & dtrT!tm_tipork.ToString & _
                              "' AND tm_anno = " & dtrT!tm_anno.ToString & _
                              " AND tm_serie = '" & dtrT!tm_serie.ToString & _
                              "' AND tm_numdoc = " & dtrT!tm_numdoc.ToString).Length = 0 Then
              If Not bLogRidotto Then LogWrite(oApp.Tr(Me, 130003204028936336, "DDT rimosso dall'utente: ") & dtrT!tm_tipork.ToString & "-" & dtrT!tm_anno.ToString & "-" & dtrT!tm_serie.ToString & "-" & dtrT!tm_numdoc.ToString, False)
              dtrT.Delete()
            End If
          Next
        End If
        dttDDT.AcceptChanges()

        If dttDDT.Rows.Count = 0 Then
          'evito il messaggio: avvisa che non ci sono ddt, ma all'utente non è che importi molto: se non ci sono ddt non devono venir fatturati ...
          'ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128762339797764000, "Attenzione! non ci sono bolle per una fattura. Impossibile continuare.")))
          GoTo Prosegui
        End If

        '------------------------------
        'Controlla se il numero di documento è già utilizzato
        Do While oCldFadi.IsInTestmagFatt(strDittaCorrente, strTipork, nAnno, strSerie, lNumfat)
          If bProva Then
            lNumfat -= 1
          Else
            lNumfat += 1
          End If
        Loop

        '------------------------------
        'genero la fattura
        If Not oCleFdin.NuovoDocumento(strDittaCorrente, strTipork, nAnno, strSerie, lNumfat) Then GoTo Prosegui
        oCleFdin.dttET.Rows(0)!tm_datdoc = dtDatdoc
        'se in prova genero le fatture loopando al contrario, visto che il numero documento è negativo
        'ed in stampa viene ordinato per numero documento
        oCleFdin.dttET.Rows(0)!tm_conto = dttTesta.Rows(NTSCInt(IIf(bProva, dttTesta.Rows.Count - 1 - nFat, nFat)))!xx_conto
        oCleFdin.dttET.Rows(0)!tm_flscdb = "N"      'mai sblocca castelletti

        '------------------------------
        'verifico se devo aggiornare il numeratore
        'aggnuma solo se progressivo non cambiato e non elaborazione di prova
        If bProva Then
          oCleFdin.bProgrCambiato = True
        Else
          If oCldFadi.LegNuma(strDittaCorrente, IIf(strTipork = "D" Or strTipork = "£", "A", "S").ToString, strSerie, nAnno, False) <= lNumfat Then
            oCleFdin.bProgrCambiato = False
          Else
            oCleFdin.bProgrCambiato = True
          End If
        End If

        oCleFdin.dImpoScostAcconti = dImpoScostAcconti
        oCleFdin.nGestScostAcconti = nGestScostAcconti
        '------------------------------
        'aggiungo i ddt
        oCleFdin.DDTAggiungi(dttDDT)

        '------------------------------
        'calcolo le note 
        If Not bRifBolleInNote Then
          'Se non si richiedono i riferimenti bolle sui doc. stampa le note come da opzione di registro
          strNoted = strNoteFattDiff
        Else
          'Aggiusta le note del documento
          If bRifBolleInNoteNoHeader Then
            strNoted = ""
          Else
            strNoted = "Rif. DDT "
          End If
          For Each dtrT As DataRow In dttDDT.Rows
            strNoted += " N. " & NTSCInt(dtrT!tm_numdoc).ToString("000000") & _
                        IIf(dtrT!tm_serie.ToString = " ", "", "\" & dtrT!tm_serie.ToString).ToString & _
                        " del " & NTSCDate(dtrT!tm_datdoc).ToShortDateString
          Next
        End If

        If oCleFdin.bNonRiportaNote1DDT = True Then
          oCleFdin.dttET.Rows(0)!tm_note = strNoted
        Else
          'lascio le note impostate da vefdin nella 'CaricaTestataBolla'
        End If

        '-------------------------------
        ' test cambio cod. pagamento : ( NB: fatto per Datawork ; non è ammesso/supportato che cambi tipo pagamento, o spese di incasso , o decorrenza, ecc. ma solo il numero delle rate !!!
        If strCambioCodiciPagElenco <> "" Then
          With oCleFdin.dttET.Rows(0)
            nTmpCodpaga = NTSCInt(!tm_codpaga)
            If (NTSCDec(!tm_totdoc) - NTSCDec(!tm_pagato) - NTSCDec(!tm_pagato2) + NTSCDec(!tm_resto) - NTSCDec(!tm_abbuono) - NTSCDec(!tm_totomag)) > dCambioCodiciPagSoglia Then
              For i = 0 To 1000
                If nDacodpaga(i) = nTmpCodpaga And nAcodpaga(i) <> 0 Then
                  nTmpCodpaga = nAcodpaga(i)
                End If
              Next
            End If
            If nTmpCodpaga <> NTSCInt(!tm_codpaga) Then
              !tm_codpaga = nTmpCodpaga
              oCleFdin.dttET.Rows(0).AcceptChanges()
            End If
          End With
        End If

        '------------------------------
        'salvo la nuova fattura
        oCleFdin.strNomeProgrammaChiamante = "BNVEFADI"
        If Not oCleFdin.SalvaDocumento("N") Then
          LogWrite(oApp.Tr(Me, 128843594045134000, "Documento non salvato"), True)
          GoTo Prosegui
        Else
          strFatt += lNumfat.ToString & ","

          '-------------------------------
          'personalizzazione CEAS:
          'se il documento ha una esezione in testata, il tot documento non cambia ma mel castelletto IVA
          'dal cod 5 all'8 ricalcolo 'solo ai fini informativi' il totale imponibile + IVA degli articoli dei ddt
          'con codice IVA preso da artico (aggiungo anche le spese di incasso sul cod IVA = a quello di tabpeve.tb_ivainc)
          'devo dare errore se nella fattura sono presenti più di 4 codici IVA (dal 5 all'8 servono per questo giochino)
          'e anche se nell fattura andrebbero elencati più di 4 codici IVA presi da artico
          If NTSCInt(oCleFdin.dttET.Rows(0)!tm_codese) <> 0 And bCeas Then
            CalcolaCeas(oCleFdin.dttET.Rows(0))
          End If

        End If

Prosegui:
      Next

      ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 129307696611296589, "")))

      '------------------------------
      'memorizzo l'elenco delle fatture appena create
      If strFatt.Trim.Length > 0 Then strFatt = strFatt.Substring(0, strFatt.Length - 1)

      '------------------------------
      'Scrive una riga in ACTLOG, se attiva l'opzione di registro
      strDesogglog = oApp.Tr(Me, 130366750577130663, "Generazione fatture differite relative alla ditta '|" & strDittaCorrente & "|'" & vbCrLf & vbCrLf & _
                     " - documenti da generare.............: ")
      Select Case strTipork
        Case "D" : strDesogglog = strDesogglog & oApp.Tr(Me, 130366750794160524, "Fattura differita emessa") & vbCrLf
        Case "P" : strDesogglog = strDesogglog & oApp.Tr(Me, 130366759545667014, "Fattura/ricevuta fiscale differita") & vbCrLf
        Case "£" : strDesogglog = strDesogglog & oApp.Tr(Me, 130366759688791098, "Nota accredito differita emessa") & vbCrLf
      End Select
      strDesogglog = strDesogglog & oApp.Tr(Me, 130366759855196283, "     . anno........: ") & nAnno.ToString & vbCrLf
      strDesogglog = strDesogglog & oApp.Tr(Me, 130366760065976184, "     . numero/serie: ") & lFirstNumdoc & IIf(Trim(strSerie) <> "", "/" & strSerie, "").ToString & vbCrLf
      strDesogglog = strDesogglog & oApp.Tr(Me, 130366760164100556, "     . data........: ") & dtDatdoc.ToShortDateString & vbCrLf
      strDesogglog = strDesogglog & oApp.Tr(Me, 130366760268631137, "     . note........: ") & strNoteFattDiff & vbCrLf
      strDesogglog = strDesogglog & oApp.Tr(Me, 130366760372380473, " - filtri per selezione documenti...............: ") & vbCrLf
      strDesogglog = strDesogglog & oApp.Tr(Me, 130366760513629569, "     . filtri su anagrafica clienti........: ") & strWhereAnagra & vbCrLf
      strDesogglog = strDesogglog & oApp.Tr(Me, 130366760646753717, "     . filtri su testate DDT...............: ") & strWhereTestmag & vbCrLf
      strDesogglog = strDesogglog & oApp.Tr(Me, 130366760829408798, "     . clienti da lista selezionata numero.: ") & nWhereLista.ToString & vbCrLf
      strDesogglog = strDesogglog & oApp.Tr(Me, 130366760956282986, "     . da numero/serie DDT.................: ") & lWhereNumdocDa.ToString & "/" & strWhereSerieDa & vbCrLf
      strDesogglog = strDesogglog & oApp.Tr(Me, 130366761096125841, "     . a  numero/serie DDT.................: ") & lWhereNumdocA.ToString & "/" & strWhereSerieA & vbCrLf & vbCrLf
      strDesogglog = strDesogglog & oApp.Tr(Me, 130366761223156278, " - elenco numero fatture generate: ") & strFatt & vbCrLf
      If bProva = False Then oCldFadi.ScriviActLog(strDittaCorrente, "BSVEFADI", "BSVEFADI", "", "", "M", "E", strDesogglog, False)

      ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128762340342048000, "Elaborazione completata")))

      GeneraDoc_Pers(strTipork, nAnno, strSerie, dtDatdoc, lFirstNumdoc, bProva, strCheckRighe, strWhereTestmag, strWhereAnagra, nWhereLista, lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, strFatt, bRielabora, bForzaDataDiversa, strDataDiversa)

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
      If bMostraLogPrezziZero Then
        ThrowRemoteEvent(New NTSEventArgs("ASKVISLOG:", LogFileName))
      End If
      dttTmp.Clear()
      dttTesta.Clear()
      dttDDT.Clear()
      ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", ""))
    End Try
  End Function

  Public Overridable Function GeneraDoc_Pers(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                          ByVal dtDatdoc As DateTime, ByVal lFirstNumdoc As Integer, ByVal bProva As Boolean, _
                                          ByVal strCheckRighe As String, ByVal strWhereTestmag As String, _
                                          ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                          ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                          ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                          ByRef strFatt As String, ByVal bRielabora As Boolean, _
                                          ByVal bForzaDataDiversa As Boolean, ByVal strDataDiversa As String) As Boolean
    Try

      Return True

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function CaricaDocGenerati(ByVal strTipork As String, ByVal nAnno As Integer, _
                                                ByVal strSerie As String, ByVal strFattSplit As String, _
                                                ByVal bRielabora As Boolean, ByRef dsOut As DataSet) As Boolean
    Dim bOut As Boolean = False
    Dim dsTmp As New DataSet
    Try
      '------------------------------
      'carico l'elenco delle fatture appena create
      If bRielabora = False Then
        bOut = oCldFadi.CaricaDocFileNuovoRielab(strDittaCorrente, strTipork, nAnno, strSerie, strFattSplit, dsOut)
      Else
        'accodo al dataset esistente
        dsOut.AcceptChanges()
        bOut = oCldFadi.CaricaDocFileNuovoRielab(strDittaCorrente, strTipork, nAnno, strSerie, strFattSplit, dsTmp)
        If bOut Then
          For Each dtrT As DataRow In dsTmp.Tables("FADI").Rows
            dtrT!xx_rielab = "S"
            dsOut.Tables("FADI").ImportRow(dtrT)
          Next
          dsOut.AcceptChanges()
        End If
      End If

      dsShared = dsOut
      Return bOut

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


  Public Overridable Function CaricaDoc(ByVal strWhereTestmag As String, _
                                        ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                        ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                        ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                        ByRef dsOut As DataSet, ByVal bProva As Boolean) As Boolean
    Try
      'obsoleta
      Return CaricaDoc(strWhereTestmag, strWhereAnagra, nWhereLista, _
                       lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                       dsOut, bProva, "T")

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

  Public Overridable Function CaricaDoc(ByVal strWhereTestmag As String, _
                                        ByVal strWhereAnagra As String, ByVal nWhereLista As Integer, _
                                        ByVal lWhereNumdocDa As Integer, ByVal lWhereNumdocA As Integer, _
                                        ByVal strWhereSerieDa As String, ByVal strWhereSerieA As String, _
                                        ByRef dsOut As DataSet, ByVal bProva As Boolean, _
                                        ByVal strContab As String) As Boolean
    Dim bOut As Boolean = False
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strWhereTestmag, strWhereAnagra, nWhereLista, _
                                             lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, _
                                             dsOut, bProva, "T"})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsOut = CType(oIn(7), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------


      bOut = oCldFadi.CaricaDoc(strDittaCorrente, strWhereTestmag, strWhereAnagra, nWhereLista, _
                                lWhereNumdocDa, lWhereNumdocA, strWhereSerieDa, strWhereSerieA, dsOut, _
                                bProva, strContab)
      dsShared = dsOut
      Return bOut

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

  Public Overridable Function CancellaFatture(ByRef dttFadi As DataTable) As Boolean
    Dim strErrLog As String = ""
    Dim bCa2 As Boolean = False
    Dim dttScad As New DataTable
    Dim dttTmp As New DataTable
    Dim lConeff As Integer = 0  'conto effetti attivi
    Dim dttTm As New DataTable

    Try
      If Not LogStart("BNVEFADI", oApp.Tr(Me, 130366761520498125, "Fatturazione differita"), False) Then Return False

      bCa2 = CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupCAE)

      dttTm.Columns.Add("tm_tipork", GetType(String))
      '--------------------
      'cancello le fatture 
      dttFadi.AcceptChanges()
      For Each dtrT As DataRow In dttFadi.Select("xx_seleziona = 'S'", "tm_numdoc DESC")
        dttTm.Clear()
        dttTm.Rows.Add(New Object() {dtrT!tm_tipork})
        dttTm.AcceptChanges()
        'test sulla ca2: la fattura non deve essere in CG e la data del documento deve essere successiva alla data di congelamento ca2 in tabanaz
        If bCa2 Then
          If NTSCDate(dttAnaz.Rows(0)!tb_dtulaca) >= NTSCDate(dtrT!tm_datdoc) Then
            If Not bLogRidotto Then LogWrite(oApp.Tr(Me, 129276341756188750, "Cancellazione fattura |" & dtrT!tm_numdoc.ToString & "| non eseguita: con il modulo della 'Contabilità analitica duplice contabile' attivato non è possibile inserire/modificare/cancellare documenti con data NON SUCCESSIVA alla 'data ultimo aggiornamento CA' indicata in anagrafica ditta(|" & NTSCDate(dttAnaz.Rows(0)!tb_dtulaca).ToShortDateString & "|)"), True)
            Continue For
          End If
          If bCancellaRegCG = False And NTSCStr(dtrT!tm_datregef).Trim <> "" Then
            If Not bLogRidotto Then LogWrite(oApp.Tr(Me, 129276335995788359, "Cancellazione fattura |" & dtrT!tm_numdoc.ToString & "| non eseguita: Con attivo il modulo della 'Contabilità analitica duplice contabile' la fattura non deve risultare contabilizzata per poter essere cancellata. Eventualmente attivare l'opzione di registro di Business/Bsvefadi/CancellaRegCG=-1"), True)
            Continue For
          End If
        End If

        'cancello prima la registrazione contabile, se presente: nell'ordine: abbuono, incasso, fattura
        'questo perche bsvepnfa registra in prima nota sempre prima la fattura, poi l'evenutale incasso, poi l'eventuale abbuono...
        If oCleFdin Is Nothing Then If Not CreaFdin() Then Return False
        oCleFdin.dttET = dttTm
        If (bCancellaRegCG Or oCleFdin.ChiamaPnfa()) And NTSCStr(dtrT!tm_datregef).Trim <> "" Then
          'verifico se posso cancellare: 
          '---------------------------
          'abbuono
          If NTSCInt(dtrT!tm_numregom) <> 0 Then
            dttScad.Clear()
            If Not CType(oCleComm, CLELBMENU).TestPreCancellaRegistrazioneEx(strDittaCorrente, NTSCDate(dtrT!tm_datregef).ToShortDateString, NTSCInt(dtrT!tm_numregom), "N", False, strErrLog, dttScad) Then
              If strErrLog <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrLog))
              Return False
            End If
          End If
          '---------------------------
          'incassato
          If NTSCInt(dtrT!tm_numrgin) <> 0 Then
            dttScad.Clear()
            If Not CType(oCleComm, CLELBMENU).TestPreCancellaRegistrazioneEx(strDittaCorrente, NTSCDate(dtrT!tm_datregef).ToShortDateString, NTSCInt(dtrT!tm_numrgin), "N", False, strErrLog, dttScad) Then
              If strErrLog <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrLog))
              Return False
            End If
          End If
          '---------------------------
          'fattura
          If NTSCInt(dtrT!tm_numregef) <> 0 Then
            dttScad.Clear()
            If Not CType(oCleComm, CLELBMENU).TestPreCancellaRegistrazioneEx(strDittaCorrente, NTSCDate(dtrT!tm_datregef).ToShortDateString, NTSCInt(dtrT!tm_numregef), "N", False, strErrLog, dttScad) Then
              'devo scartare la registrazione degli omaggi e di incasso contestuale
              If NTSCInt(dtrT!tm_numregom) <> 0 Then
                For Each dtrT1 As DataRow In dttScad.Select("sc_flsaldato = 'S' AND sc_dtsaldato = " & CDataSQL(NTSCDate(dtrT!tm_datregef)) & " AND sc_rgsaldato = " & NTSCInt(dtrT!tm_numregom))
                  dtrT1.Delete()
                Next
                dttScad.AcceptChanges()
              End If
              If NTSCInt(dtrT!tm_numrgin) <> 0 Then
                For Each dtrT1 As DataRow In dttScad.Select("sc_flsaldato = 'S' AND sc_dtsaldato = " & CDataSQL(NTSCDate(dtrT!tm_datregef)) & " AND sc_rgsaldato = " & NTSCInt(dtrT!tm_numrgin))
                  dtrT1.Delete()
                Next
                dttScad.AcceptChanges()
              End If

              'scarto le scadenze generate e saldate all'interno della stessa registrazione 'nota accredito emessa che compensa fattura emessa'
              For Each dtrT1 As DataRow In dttScad.Select("sc_flsaldato = 'S' AND sc_dtsaldato = sc_datreg AND sc_rgsaldato = sc_numreg")
                dtrT1.Delete()
              Next
              dttScad.AcceptChanges()

              'se ho scadenze collegate ad incassi differiti o no RB/tratte, avviso subito che non posso cancellare
              If dttScad.Select("sc_codincdiff > 0 or sc_tippaga > 2").Length > 0 Then
                If strErrLog <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrLog))
                Return False
              End If

              'possono essere solo scadenze di effetti: verifico per ogni record se posso cancellare la registrazione:
              'se posso farlo (perchè emesso gli effetti ma non presentati in banca, cancellerò anche queste registrazioni
              'le scadenze, sia con che senza chiusura cliente, sono quelle sul conto EFFETTI ATTIVI con stessi estremi di partita del cliente
              oCldFadi.ValCodiceDb("1", strDittaCorrente, "TABPECG", "N", "", dttTmp)
              If dttTmp.Rows.Count > 0 Then
                lConeff = oCldFadi.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(dttTmp.Rows(0)!tb_contreff), NTSCInt(dttTmp.Rows(0)!tb_coneff))
              End If
              dttTmp.Clear()
              If lConeff = 0 Then
                If strErrLog <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrLog))
                Return False
              End If

              For Each dtrT1 As DataRow In dttScad.Rows
                If Not oCldFadi.GetScadEffetti(strDittaCorrente, lConeff, NTSCInt(dtrT1!sc_annpar), _
                                               NTSCStr(dtrT1!sc_alfpar), NTSCInt(dtrT1!sc_numpar), _
                                               NTSCInt(dtrT1!sc_numrata), dttTmp) Then
                  If strErrLog <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrLog))
                  Return False
                End If
                If dttTmp.Rows.Count = 0 Then
                  'non c'è emissione effetti, ma se sono qui è perchè le scadenze sono saldate: blocco
                  'attenzione: passa di qui anche nel caso di emissione effetti senza chiusura cliente
                  'se genero gli effetti e poi cancello la generazione effetti. sulla scadenza cliente rimane sc_fldis = "S"
                  'stessa cosa se spezzo la scadenza quando sc_fldis = "S"! la scadenza non è saldata e non c'è la generazione effetti
                  'per ora blocco solo se la scadenza è saldata! se non è saldata, potrei aver cancellato la scadenza a mano da prima nota
                  If NTSCStr(dtrT1!sc_flsaldato) = "S" Then
                    If strErrLog <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrLog))
                    Return False
                  Else
                    'così non cercherò di cancellare la registrazione che ha saldato questo record, perchè già non esistente
                    dtrT1!sc_flsaldato = DBNull.Value
                    dtrT1!sc_rgsaldato = 0
                  End If
                Else
                  If NTSCStr(dttTmp.Rows(0)!sc_flsaldato) = "S" Then
                    'già presentata in banca
                    If strErrLog <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrLog))
                    Return False
                  Else
                    'farò cancellare la registrazione
                    dtrT1!sc_dtsaldato = NTSCDate(dttTmp.Rows(0)!sc_datreg)
                    dtrT1!sc_rgsaldato = NTSCInt(dttTmp.Rows(0)!sc_numreg)
                  End If
                End If

                If NTSCInt(dtrT1!sc_rgsaldato) <> 0 Then
                  If Not CType(oCleComm, CLELBMENU).TestPreCancellaRegistrazione(strDittaCorrente, NTSCDate(dtrT1!sc_dtsaldato).ToShortDateString, NTSCInt(dtrT1!sc_rgsaldato), "N", False, "") Then
                    If strErrLog <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrLog))
                    Return False
                  End If
                End If
              Next    'For Each dtrT1 As DataRow In dttScad.Rows
              dttScad.AcceptChanges()
            End If
          End If    'If NTSCInt(dttET.Rows(0)!et_numregef) <> 0 Then

          '---------------------------
          'eseguo la cancellazione
          'prima cancello eventuali registrazioni di emissione effetti
          For Each dtrT1 As DataRow In dttScad.Rows
            If NTSCStr(dtrT1!sc_dtsaldato) <> "" Then
              If Not CType(oCleComm, CLELBMENU).CancellaRegistrazione(strDittaCorrente, NTSCDate(dtrT1!sc_dtsaldato).ToShortDateString, NTSCInt(dtrT1!sc_rgsaldato), "N") Then
                Return False
              End If
            End If
          Next
          'ora cancello le registrazioni effettivamente collegate alla fattura
          If NTSCInt(dtrT!tm_numregom) <> 0 Then
            If Not CType(oCleComm, CLELBMENU).CancellaRegistrazione(strDittaCorrente, NTSCDate(dtrT!tm_datregef).ToShortDateString, NTSCInt(dtrT!tm_numregom), "N") Then
              Return False
            End If
          End If
          If NTSCInt(dtrT!tm_numrgin) <> 0 Then
            If Not CType(oCleComm, CLELBMENU).CancellaRegistrazione(strDittaCorrente, NTSCDate(dtrT!tm_datregef).ToShortDateString, NTSCInt(dtrT!tm_numrgin), "N") Then
              Return False
            End If
          End If
          If NTSCInt(dtrT!tm_numregef) <> 0 Then
            If Not CType(oCleComm, CLELBMENU).CancellaRegistrazione(strDittaCorrente, NTSCDate(dtrT!tm_datregef).ToShortDateString, NTSCInt(dtrT!tm_numregef), "N") Then
              Return False
            End If
          End If
          strErrLog = ""  'pulisco dall'eventuale errore di scadenze saldate
        End If    'If bCancellaRegCG And NTSCStr(dtrT!tm_datregef).Trim <> "" Then

        'cancello la fattura
        If Not oCldFadi.CancellaFattura(strDittaCorrente, dtrT!tm_tipork.ToString, NTSCInt(dtrT!tm_anno), dtrT!tm_serie.ToString, NTSCInt(dtrT!tm_numdoc), bCa2, lIITtkeys) Then Return False

        'rimuovo la riga
        dtrT.Delete()
      Next    'For Each dtrT As DataRow In dttFadi.Select("xx_seleziona = 'S'")

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
      dttTmp.Clear()
      dttScad.Clear()
      LogStop()
      dttFadi.AcceptChanges()
    End Try
  End Function


  Public Overridable Function SerfedLista_ValidatedAndChanged(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If oCldFadi.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLSEL", "N", strDescr) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128749502113084000, "Codice lista selezionata |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function SerfedTipobf_ValidatedAndChanged(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If oCldFadi.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABTPBF", "N", strDescr) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128755531891574000, "Codice tipo bolla/fattura |'" & nCod.ToString & "'| inesistente")))
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

  Public Overridable Function GetQueryStampaPDF(ByRef dttFadi As DataTable, ByRef strQueryGetDocUnico As String) As String
    Try
      Return oCldFadi.GetQueryStampaPDF(strDittaCorrente, dttFadi, strQueryGetDocUnico)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return ""
    End Try
  End Function

  Public Overridable Function GetWhereHltm(ByVal strTipork As String, ByVal nAnno As Integer, _
                                             ByVal strSerie As String, ByVal lNumdoc As Integer) As String
    Try

      Return oCldFadi.GetWhereHltm(strTipork, nAnno, strSerie, lNumdoc)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return ""
    End Try
  End Function

  Public Overridable Function IsDocRetail(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try
      Return oCldFadi.IsDocRetail(strDittaCorrente, strTipork, nAnno, strSerie, lNumdoc)

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
  Public Overridable Function IsDocRetailNew(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try
      Return oCldFadi.IsDocRetailNew(strDittaCorrente, strTipork, nAnno, strSerie, lNumdoc)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsInTestmag(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try

      Return oCldFadi.IsInTestmag(strDittaCorrente, strTipork, nAnno, strSerie, lNumdoc)

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

  Public Overridable Function GeneraFileConad() As Boolean
    Dim bEscludiArtD As Boolean = False
    Dim i As Integer
    Dim lCounterBolle As Integer
    Dim lNumFat As Integer = 0
    Dim lNumBol As Integer = 0
    Dim lXFatt As Integer = 0
    Dim lXBoll As Integer = 0
    Dim lXRighe As Integer = 0
    Dim strCodFornConad As String = ""
    Dim strNomeFileTmp As String = ""
    Dim strLine As String = ""
    Dim dttConad As New DataTable
    Dim w1 As System.IO.StreamWriter = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      strCodFornConad = oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "ConadCodiceFornitore", "", " ", "")
      bEscludiArtD = CBool(Val(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "ConadEscludiArtD", "0", " ", "0")))
      '--------------------------------------------------------------------------------------------------------------
      '--- Se serve crea la directory
      '--------------------------------------------------------------------------------------------------------------
      If System.IO.Directory.Exists(oApp.AscDir) = False Then
        Try
          System.IO.Directory.CreateDirectory(oApp.AscDir)
        Catch ex As Exception
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128752756607102000, "Impossibile creare la directory |" & oApp.AscDir & "| sul disco.")))
          Return False
        End Try
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Assegna un nome di file in Asc
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To 999
        strNomeFileTmp = oApp.AscDir & "\Filconad." & i.ToString("000")
        If Not System.IO.File.Exists(strNomeFileTmp) Then Exit For
        strNomeFileTmp = ""
      Next
      If strNomeFileTmp = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128752758095342000, "Impossibile attribuire un nome di file 'Filconad.*'.")))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Per tutti i documenti selezionati, ottengo l'elenco di testmag/movmag
      '--------------------------------------------------------------------------------------------------------------
      dsShared.Tables("FADI").AcceptChanges()
      If Not oCldFadi.GetTestmagMovmagPerFileConad(strDittaCorrente, dsShared.Tables("FADI"), bEscludiArtD, dttConad) Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre il file temporaneo
      '--------------------------------------------------------------------------------------------------------------
      w1 = New System.IO.StreamWriter(strNomeFileTmp, True)
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrD As DataRow In dttConad.Rows
        '--- Cambio di fattura
        If lNumFat <> NTSCInt(dtrD!tm_numdoc) Then
          lCounterBolle = 1
          lXFatt += 1
          lXBoll += 1
          '--- Scrive il record di testa (FILCONAD 01)
          If Not GeneraFileConad_ScriviRecord01(dtrD, strLine, lCounterBolle, strCodFornConad) Then
            dttConad.Clear()
            Return False
          End If
          If strLine <> "" Then w1.WriteLine(strLine)
        Else
          '--- Cambio di bolla
          If lNumBol <> NTSCInt(dtrD!NumBol) Then
            lCounterBolle += 1
            lXBoll += 1
            '--- Scrive il record di testa (FILCONAD 01)
            If Not GeneraFileConad_ScriviRecord01(dtrD, strLine, lCounterBolle, strCodFornConad) Then
              dttConad.Clear()
              Return False
            End If
            If strLine <> "" Then w1.WriteLine(strLine)
          End If
        End If
        '--- Scrive il record di corpo bolla (FILCONAD 02)
        lXRighe += 1
        If Not GeneraFileConad_ScriviRecord02(dtrD, strLine, lCounterBolle) Then
          dttConad.Clear()
          Return False
        End If
        If strLine <> "" Then w1.WriteLine(strLine)
        lNumFat = NTSCInt(dtrD!tm_numdoc)
        lNumBol = NTSCInt(dtrD!NumBol)
      Next    'For Each dtrD As DataRow In dttConad.Rows
      '--------------------------------------------------------------------------------------------------------------
      w1.Flush()
      w1.Close()
      '--------------------------------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128752799407264000, "Generazione del file |" & UCase$(strNomeFileTmp) & "| terminata; |" & lXFatt & "| fatture, |" & lXBoll & "| bolle/DDT, |" & lXRighe & "| righe elaborate.")))
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      If Not w1 Is Nothing Then w1.Close()
    End Try
  End Function
  Public Overridable Function GeneraFileConad_ScriviRecord01(ByRef dtrD As DataRow, ByRef strLine As String, ByVal lCounterBolle As Integer, ByVal strCodFornConad As String) As Boolean
    Dim strTemp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Tracciato record FILCONAD TIVER 03
      '--------------------------------------------------------------------------------------------------------------
      strLine = "01" 'tipo record
      strLine += lCounterBolle.ToString("00000") 'Num. progr
      If dtrD!tm_serie.ToString = " " Then
        strTemp = "".PadLeft(6) & NTSCStr(dtrD!tm_numdoc)
      Else
        strTemp = "".PadLeft(6) & NTSCStr(dtrD!tm_numdoc) & NTSCStr(dtrD!tm_serie).ToUpper
      End If
      strLine += Right(strTemp, 6) 'Num fattura
      strLine += NTSCDate(dtrD!tm_datdoc).ToString("yyMMdd") ' data fatt
      If NTSCStr(dtrD!AlfBol) = " " Then
        strTemp = "".PadLeft(6) & CStr(dtrD!NumBol)
      Else
        strTemp = "".PadLeft(6) & CStr(dtrD!NumBol) & NTSCStr(dtrD!AlfBol).ToUpper
      End If
      strLine += Right(strTemp, 6)
      strLine += NTSCDate(dtrD!DatBol).ToString("yyMMdd") 'Data bolla
      '--------------------------------------------------------------------------------------------------------------
      '--- Ha priorità l'opzione da registro (per compatibilità all'indietro)
      '--- quindi quanto indicato in 'codice nostro presso di loro'
      '--------------------------------------------------------------------------------------------------------------
      If strCodFornConad.Trim <> "" Then
        strLine += Right("".PadLeft(15) & strCodFornConad.Trim, 15) 'Cod. fornitore attrib. da CONAD
      Else
        strLine += Right("".PadLeft(15) & NTSCStr(dtrD!an_codnscol).Trim, 15) 'Cod. fornitore attrib. da CONAD
      End If
      strLine += "".PadLeft(1) 'tipo fornitore
      strLine += Right("".PadLeft(15) & NTSCStr(dtrD!an_note).Trim, 15) 'Cod cliente
      strLine += Right("".PadLeft(15) & NTSCStr(dtrD!an_note).Trim, 15) 'Cod cooperativa
      strLine += Right("".PadLeft(15) & NTSCStr(dtrD!tm_coddest).Trim, 15) 'Cod destinazione
      strLine += "".PadLeft(1) 'tipo socio
      '--------------------------------------------------------------------------------------------------------------
      If bAngel = True Then 'Pers Angelini Renzo Roberto
        strLine += " " 'filler
      Else
        Select Case NTSCStr(dtrD!tm_tipork)
          Case "£"
            strLine += "N" 'tipo documento nel caso di Nota di accredito differita emessa
          Case Else
            strLine += "F" 'tipo documento nel caso di fattura differita
        End Select
      End If
      strLine += "EUR"
      strLine += "".PadLeft(25) 'filler
      strLine += "".PadLeft(6) 'reserved
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
  Public Overridable Function GeneraFileConad_ScriviRecord02(ByRef dtrD As DataRow, ByRef strLine As String, ByVal lCounterBolle As Integer) As Boolean
    Dim dPrezzo As Decimal = 0
    Dim dImporto As Decimal = 0
    Dim dAliq As Decimal = 0
    Dim strCodart As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Tracciato record FILCONAD TIVER 03
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dtrD!caf_codarfo).Trim = "" Then
        strCodart = NTSCStr(dtrD!mm_codart).Trim
      Else
        strCodart = NTSCStr(dtrD!caf_codarfo).Trim
      End If
      strLine = "02" 'tipo record
      strLine += lCounterBolle.ToString("00000") 'Num. progr
      strLine += Right("".PadLeft(15) & strCodart, 15) 'Cod. articolo
      strLine += Left(NTSCStr(dtrD!mm_descr).Trim & "".PadLeft(30), 30) 'Descrizione
      strLine += Left(NTSCStr(dtrD!mm_unmis).Trim & "  ", 2)  'UM
      strLine += Fix(Math.Abs(NTSCDec(dtrD!mm_quant) * 100)).ToString("0000000") 'Quant
      '--------------------------------------------------------------------------------------------------------------
      '--- EURO
      '--------------------------------------------------------------------------------------------------------------
      If NTSCDec(dtrD!mm_quant) = 0 Then
        dPrezzo = 0
        dImporto = 0
      Else
        '--- FILCONAD in euro
        dPrezzo = ArrDbl((Math.Abs(NTSCDec(dtrD!mm_valore)) / Math.Abs(NTSCDec(dtrD!mm_quant))) * 1000, 0)
        dImporto = ArrDbl(Math.Abs(NTSCDec(dtrD!mm_valore)) * 1000, 0)
      End If
      strLine += Right("".PadLeft(9, "0"c) & (dPrezzo.ToString).Trim, 9) 'Prezzo
      strLine += Right("".PadLeft(9, "0"c) & (dImporto.ToString).Trim, 9) 'Importo
      '--------------------------------------------------------------------------------------------------------------
      strLine += "".PadLeft(4, "0"c) 'Pezzi
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dtrD!tb_tipiva) = "" Then
        strLine += "2" 'tipiva escluso
      Else
        If NTSCInt(dtrD!tb_tipiva) = 1 Then
          strLine += " " 'tipiva imponibile
        Else
          If NTSCInt(dtrD!tb_tipiva) = 2 Then
            strLine += "1" 'tipiva esente
          Else
            strLine += "2" 'tipiva escluso
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      dAliq = NTSCDec(dtrD!tb_aliq)
      If ArrDbl(dAliq, 2) <> ArrDbl(dAliq, 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128979938198794770, "Attenzione! Sul documento è presente un codice IVA con un'aliquota che presenta decimali |" & dAliq.ToString & "| non suppotata dal tracciato FILCONAD. Si procede troncando l'aliquota al valore intero.")))
        dAliq = Int(dAliq)
      End If
      If bAngel = True Then 'Pers Angelini Renzo Roberto
        strLine += Right("".PadLeft(2, "0"c) & NTSCInt(dAliq).ToString, 2)
      Else
        strLine += Right("".PadLeft(2) & NTSCInt(dAliq).ToString, 2)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strLine += " " 'tipo movim
      If bCONADOmaggiImpIvaInTipocess7 Then
        Select Case NTSCStr(dtrD!mm_stasino)
          Case "O" : strLine += "6" 'tipo cessione omaggi
          Case "P", "M", "X" : strLine += "7" 'tipo cessione sconto merce
          Case Else : strLine += "1" 'tipo cessione vendita merce
        End Select
      Else
        Select Case NTSCStr(dtrD!mm_stasino)
          Case "O", "P" : strLine += "6" 'tipo cessione omaggi
          Case "M", "X" : strLine += "7" 'tipo cessione sconto merce
          Case Else : strLine += "1" 'tipo cessione vendita merce
        End Select
      End If
      strLine += "".PadLeft(6) 'numero ordine
      strLine += "".PadLeft(2) 'codice listino
      strLine += "".PadLeft(1) 'tipo codice articolo
      strLine += "".PadLeft(1) 'tipo contratto
      strLine += "".PadLeft(1) 'tipo trattamento
      strLine += "".PadLeft(5, "0"c) 'tipo costo trasporto
      strLine += "".PadLeft(1) 'tipo codice contabile
      'Tipo reso
      Dim strTiporeso As String = ""
      Select Case NTSCStr(dtrD!tm_tipork)
        Case "£"
          strTiporeso = "2"
        Case Else
          If NTSCDec(dtrD!mm_quant) < 0 Then
            strTiporeso = "1"
          Else
            strTiporeso = " "
          End If
      End Select
      strLine += strTiporeso 'tipo reso
      strLine += "".PadLeft(10) 'filler
      strLine += "".PadLeft(6, "0"c) 'data ordine
      strLine += "".PadLeft(6) 'reserved
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

  Public Overridable Function CalcolaCeas(ByRef dtrTm As DataRow) As Boolean
    'se il documento ha una esezione in testata, il tot documento non cambia ma mel castelletto IVA
    'dal cod 5 all'8 ricalcolo 'solo ai fini informativi' il totale imponibile + IVA degli articoli dei ddt
    'con codice IVA preso da artico (aggiungo anche le spese di incasso sul cod IVA = a quello di tabpeve.tb_ivainc)
    'devo dare errore se nella fattura sono presenti più di 4 codici IVA (dal 5 all'8 servono per questo giochino)
    'e anche se nell fattura andrebbero elencati più di 4 codici IVA presi da artico

    'dtrTm = testata della fattura appena generata
    Dim dttDDT As New DataTable
    Dim dttIVA As New DataTable
    Dim dValore As Decimal = 0
    Dim dScontV As Decimal = 0
    Dim i As Integer = 0
    Dim nCodivax As Integer = 0
    Dim dAliqx As Decimal = 0
    Try
      '-------------------------
      'temporaneo per il castelletto IVA
      dttIVA.Columns.Add("codiva", GetType(Integer))
      dttIVA.Columns.Add("imponib", GetType(Decimal))
      dttIVA.Columns.Add("iva", GetType(Decimal))
      dttIVA.AcceptChanges()
      For i = 0 To 3
        dttIVA.Rows.Add(0, 0, 0)
      Next
      dttIVA.AcceptChanges()

      If NTSCInt(dtrTm!tm_codiva_5) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128764904453904000, "Nella fattura appena generata (|" & dtrTm!tm_numdoc.ToString & "|) sono presenti più di 4 codici IVA. Non sarà possibile aggiungere il castelletto IVA per uso interno")))
        Return False
      End If

      oCldFadi.ValCodiceDb("1", strDittaCorrente, "TABPEVE", "N", "", dttDDT)
      nCodivax = NTSCInt(dttDDT.Rows(0)!tb_ivainc)
      dttDDT.Clear()
      If nCodivax <> 0 Then
        oCldFadi.ValCodiceDb(nCodivax.ToString, strDittaCorrente, "TABCIVA", "N", "", dttDDT)
        If dttDDT.Rows.Count > 0 Then dAliqx = NTSCDec(dttDDT.Rows(0)!tb_aliq)
        dttDDT.Clear()
      End If

      '-------------------------
      'ottengo il corpo dei ddt collegati alla fattura
      If Not oCldFadi.GetMovmagDDTCeas(dtrTm!codditt.ToString, dtrTm!tm_tipork.ToString, NTSCInt(dtrTm!tm_anno), _
                                       dtrTm!tm_serie.ToString, NTSCInt(dtrTm!tm_numdoc), dttDDT) Then Return False
      For Each dtrT As DataRow In dttDDT.Rows
        If NTSCDec(dtrT!mm_scontv) <> 0 And NTSCStr(dttDDT.Rows(0)!tm_scorpo) = "S" Then
          oCldFadi.Scorporo(NTSCDec(dtrT!mm_scontv), NTSCInt(dtrT!mm_codiva), dScontV, 0)
        Else
          dScontV = NTSCDec(dtrT!mm_scontv)
        End If
        '-------------------------
        'determino il valore di riga
        dValore = ArrDbl(NTSCDec(dtrT!mm_prezzo) * NTSCDec(dtrT!mm_quant) / NTSCDec(dtrT!mm_perqta) * (100 - NTSCDec(dtrT!mm_scont1)) / 100 * (100 - NTSCDec(dtrT!mm_scont2)) / 100 * (100 - NTSCDec(dtrT!mm_scont3)) / 100 * (100 - NTSCDec(dtrT!mm_scont4)) / 100 * (100 - NTSCDec(dtrT!mm_scont5)) / 100 * (100 - NTSCDec(dtrT!mm_scont6)) / 100 * (100 - NTSCDec(dtrT!mm_scontp)) / 100 - dScontV, oCldFadi.TrovaNdec(0))
        dValore = ArrDbl(dValore * (100 - NTSCDec(dtrTm!tm_scont1)) / 100 * (100 - NTSCDec(dtrTm!tm_scont2)) / 100 * (100 - NTSCDec(dtrTm!tm_scopag)) / 100, oCldFadi.TrovaNdec(0))

        '-------------------------
        'aggiungo al castelletto IVA dal 5 all'8 gli imponibili e imposte calcolati sul cod IVA di artico
        For i = 0 To 4
          If i = 4 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128764916766494000, "Sono gestibili al massimo 4 diverse aliquote IVA per documento.")))
            Return False
          End If
          If NTSCInt(dttIVA.Rows(i)!codiva) = NTSCInt(dtrT!ar_codiva) Or NTSCInt(dttIVA.Rows(i)!codiva) = 0 Then
            dttIVA.Rows(i)!codiva = NTSCInt(dtrT!ar_codiva)
            dttIVA.Rows(i)!imponib = ArrDbl(NTSCDec(dttIVA.Rows(i)!imponib) + dValore, oCldFadi.TrovaNdec(0))
            dttIVA.Rows(i)!iva = ArrDbl((NTSCDec(dttIVA.Rows(i)!imponib) * NTSCDec(dtrT!tb_aliq) / 100), oCldFadi.TrovaNdec(0))
            Exit For
          End If
        Next
      Next    'For Each dtrT As DataRow In dttDDT.Rows
      dttDDT.Clear()

      '-------------------------
      'Aggiunge le spese d'incasso
      If NTSCDec(dtrTm!tm_speinc) <> 0 And nCodivax <> 0 And dAliqx <> 0 Then
        For i = 0 To 3
          If NTSCInt(dttIVA.Rows(i)!codiva) = nCodivax Then
            dttIVA.Rows(i)!imponib = ArrDbl(NTSCDec(dttIVA.Rows(i)!imponib) + NTSCDec(dtrTm!tm_speinc), oCldFadi.TrovaNdec(0))
            dttIVA.Rows(i)!iva = ArrDbl((NTSCDec(dttIVA.Rows(i)!imponib) * dAliqx / 100), oCldFadi.TrovaNdec(0))
            Exit For
          Else
            If NTSCInt(dttIVA.Rows(i)!codiva) = 0 Then
              dttIVA.Rows(i)!codiva = nCodivax
              dttIVA.Rows(i)!imponib = ArrDbl(NTSCDec(dtrTm!tm_speinc), oCldFadi.TrovaNdec(0))
              dttIVA.Rows(i)!iva = ArrDbl((NTSCDec(dttIVA.Rows(i)!imponib) * dAliqx / 100), oCldFadi.TrovaNdec(0))
              Exit For
            End If
          End If
        Next
      End If    'If NTSCDec(dtrTm!tm_speinc) <> 0 Then
      dttIVA.AcceptChanges()

      '-------------------------
      'riverso i dati su testmag fattura
      If Not oCldFadi.UpdateTestmagCeas(dtrTm, dttIVA) Then Return False

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
      dttIVA.Clear()
      dttDDT.Clear()
    End Try
  End Function

  Public Overridable Function IsListaEsiste(ByVal nCodlist As Integer) As Boolean
    Try
      If oCldFadi.ValCodiceDb(NTSCStr(nCodlist), strDittaCorrente, "TABLSEL", "N", "") Then
        Return True
      End If

      Return False
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return True
    End Try
  End Function

  Public Overridable Function GeneraNuovaListaSelezionata(ByVal nCodice As Integer, ByVal strDescr As String, _
                              ByVal dttFatture As DataTable) As Boolean
    Dim dctClienti As New Dictionary(Of Integer, Decimal)
    Dim dtraFattureCliente As DataRow() = Nothing
    Dim lConto As Integer = 0
    Dim dImporto As Decimal = 0
    Try
      For Each dtrTmp As DataRow In dttFatture.Rows
        lConto = NTSCInt(dtrTmp!tm_conto)
        If Not dctClienti.ContainsKey(lConto) Then
          dImporto = 0
          dtraFattureCliente = dttFatture.Select("tm_conto = " & lConto)
          For Each dtrFattura As DataRow In dtraFattureCliente
            dImporto = dImporto + NTSCDec(dtrFattura!tm_totdoc)
          Next
          dctClienti.Add(lConto, Math.Round(dImporto, 2))
        End If
      Next

      oCldFadi.GeneraNuovaListaSelezionata(strDittaCorrente, nCodice, strDescr, dctClienti)

      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function GetNumeroNuovaListaSel() As Integer
    Dim nMaxLista As Integer = 0
    Dim nCodice As Integer = 0
    Try

      nMaxLista = oCldFadi.GetMaxNumeroListaSel(strDittaCorrente)
      If nMaxLista < 9999 Then
        nCodice = nMaxLista + 1
      End If

      Return nCodice
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function TestaBlocchi(ByVal bNew As Boolean, ByVal bRielabora As Boolean, ByRef dttFadi As DataTable) As Boolean
    Dim strPwd As String = ""
    Dim evnt As NTSEventArgs = Nothing
    Dim strMsg As String = ""

    'stampe definitive su registri fiscali
    Dim nEscomp As Integer = 0
    Dim strTipoChiusure As String = "S"
    Dim bOk As Boolean = True
    Dim dttTmp As New DataTable
    Dim i As Integer = 0

    Dim dtrFt() As DataRow = Nothing

    Try
      '----------------------------------------------------
      'test documenti contabilizzati
      dtrFt = dttFadi.Select("xx_seleziona = 'S' AND tm_flcont = 'S'", "tm_datregef ASC, tm_numregef DESC")
      If dtrFt.Length > 0 Then
        'prendo i record ordinati per data reg. asc, num reg disc, così con il primo record riesco a fare i test su stampa LG o RI
        '----------------------------
        'controllo se già stampato su CG o RI
        oCldFadi.EscompFromDate(strDittaCorrente, NTSCDate(dtrFt(0)!tm_datregef), nEscomp, Nothing)
        If nEscomp <> 0 Then
          oCldFadi.ValCodiceDb(strDittaCorrente, strDittaCorrente, "ANADITAC", "S", "", dttTmp)
          strTipoChiusure = dttTmp.Rows(0)!ac_flgiobol.ToString
          dttTmp.Clear()
          'test su libro giornale
          oCldFadi.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttTmp)
          With dttTmp.Rows(0)
            If strTipoChiusure <> "S" And nEscomp <> NTSCInt(dttTmp.Rows(0)!tb_escomp) Then
              If NTSCDate(dtrFt(0)!tm_datregef) < NTSCDate(dttTmp.Rows(0)!tb_dtullgp) Or (NTSCDate(dtrFt(0)!tm_datregef) = NTSCDate(dttTmp.Rows(0)!tb_dtullgp) And NTSCInt(dtrFt(0)!tm_numregef) <= NTSCInt(dttTmp.Rows(0)!tb_rgullgp)) Then
                bOk = False
              End If
            Else
              If NTSCDate(dtrFt(0)!tm_datregef) < NTSCDate(dttTmp.Rows(0)!tb_dtullg) Or (NTSCDate(dtrFt(0)!tm_datregef) = NTSCDate(dttTmp.Rows(0)!tb_dtullg) And NTSCInt(dtrFt(0)!tm_numregef) <= NTSCInt(dttTmp.Rows(0)!tb_rgullg)) Then
                bOk = False
              End If
            End If
          End With
          dttTmp.Clear()
        End If
        If bOk Then
          'test su reg. iva
          'visto che ogni fattura potrebbe andare a finire su un reg. iva diverso, devo fare il test per ogni fattura
          For i = 0 To dtrFt.Length - 1
            If Not oCldFadi.GetTabduri(strDittaCorrente, NTSCDate(dtrFt(i)!tm_datregef).ToShortDateString, _
                                                 NTSCInt(dtrFt(i)!tm_numregef), dttTmp) Then Return False
            'ritorna dei record se la registraz. iva è stata stampata sui registri IVA
            If dttTmp.Rows.Count > 0 Then
              bOk = False
              Exit For
            End If
          Next
        End If
        dttTmp.Clear()

        If bOk = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415926494411213, "Attenzione! Uno o più documenti selezionati sono già stati stampati su Libro Giornale e/o registri IVA. Elaborazione interrotta.")))
          Return False
        End If

        '------------------------------------
        If oCleFdin Is Nothing Then
          If Not CreaFdin() Then Return False
        End If
        If bProteggiDocContab = False Then
          strMsg = oApp.Tr(Me, 130077256913140478, "Attenzione! Uno o più documenti selezionati " & _
                                        "sono già stati contabilizzati: Procedere ugualmente?")
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, strMsg)
          ThrowRemoteEvent(evnt)
          If evnt.RetValue.Trim <> CLN__STD.ThMsg.RETVALUE_YES Then Return False
        Else
Riprova:
          strPwd = UCase(oCldFadi.GetSettingBus("Bsvefadi", "Opzioni", ".", "PwdDocContab", "nts", " ", "nts")).ToUpper

          strMsg = oApp.Tr(Me, 130077256929381678, "Attenzione! Uno o più documenti selezionati sono già stati contabilizzati:" & vbCrLf & vbCrLf & "Inserire la Password per proseguire:")
          evnt = New NTSEventArgs(CLN__STD.ThMsg.INPUTPWD, strMsg)
          ThrowRemoteEvent(evnt)
          If evnt.RetValue.Trim <> "" Then
            'Controlla PWD
            If evnt.RetValue.ToUpper <> strPwd Then GoTo Riprova
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129738611578931495, "Non è stata digitata (o non è valida) la password per lo sblocco documenti contabilizzati. Elaborazione interrotta.")))
            Return False
          End If

        End If
      End If    'If dsFadi.Tables("FADI").Select("xx_seleziona = 'S' AND tm_flcont = 'S'").Length > 0 Then

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
