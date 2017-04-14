Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEVEGNBF
  Inherits CLE__BASN

  Public oCldGnbf As CLDVEGNBF
  Public oCleBoll As CLEVEBOLL
  Public dsShared As DataSet
  Public bHasChanges As Boolean

  Public bModExtTCO As Boolean
  Public bModPM As Boolean
  Public lIIGegnbf As Integer
  Public lIIGegnbmm As Integer
  Public lIIGegnbmo As Integer
  Public lIIGegnbtd As Integer
  Public bRistampato As Boolean
  Public bVal As Boolean
  Public bScorp As Boolean
  Public lANumero As Integer
  Public nSvalOpzione As Integer = 3            'di ritorno da FRM__SVAL 3 = usa cambio alla data di elabor e, in mancanza, il cambio più vicino
  Public bRigaFiglio As Boolean
  Public lRigaPadre As Integer

  'opzioni di registro
  Public bRicalcPrez As Boolean
  Public bRicalcScon As Boolean
  Public bRicalcProv As Boolean
  Public bSpeseIncSoloSuPrimaFatt As Boolean
  Public strCalcPesi As String
  Public bPMControllaBaseCostMG As Boolean
  Public bPMSoloTaskRilasciatiMG As Boolean
  Public bScriviRigheZero As Boolean
  Public bEreditaMatricole As Boolean
  Public bDeterminaBolliSuOperazEsenti As Boolean
  Public bReprintDoc As Boolean
  Public bPrimo As Boolean
  Public bValMinSoloImponib As Boolean
  Public bSommaPesoColli As Boolean
  Public lIncrementoContatoreRiga As Integer

  'sebp
  Public bSebpConssoloasa As Boolean
  Public strSebpQuery As String
  Public nSepbEscomp As Integer

  'dtac
  Public nDtacEscomp As Integer
  Public strDtacTipork As String
  Public lDtacNumdoc As Integer
  Public strDtacSerie As String
  Public nDtacAnno As Integer
  Public dDtacDatdoc As Date
  Public strDtacDtiniz As String
  Public dDtacOriniz As Decimal
  Public nDtacCodvett As Integer
  Public strDtacAcuradi As String
  Public bDtacVariaAcc As Boolean
  Public bDtacRaggruppa As Boolean = False
  Public dDtacValoreMinimo As Decimal = 0

  Private Moduli_P As Integer = bsModVE + bsModMG
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDVEGNBF"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldGnbf = CType(MyBase.ocldBase, CLDVEGNBF)
    oCldGnbf.Init(oApp)
    Return True
  End Function
  Public Overridable Function IstanziaNTSCondCommerciali() As NTSCondCommerciali
    Try
      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEVEGNBF", "BN__STD.NTSCondCommerciali", oTmp, strErr, False, "", "") = False Then
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

  Public Overridable Function InitExt() As Boolean
    Dim dsBoll As New DataSet
    Try
      If Not oCldGnbf.ValCodiceDb("1", strDittaCorrente, "TABPEVE", "N", "") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101093750, "Tabella 'Personalizzazione Vendite' non compilata. Impostarla prima di proseguire")))
        Return False
      End If

      If Not oCldGnbf.ValCodiceDb("1", strDittaCorrente, "TABPEAC", "N", "") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101250000, "Tabella 'Personalizzazione Acquisti' non compilata. Impostarla prima di proseguire")))
        Return False
      End If

      '------------------------
      'inizializzo BEVEBOLL
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEVEGNBF", "BEVEBOLL", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleBoll = CType(oTmp, CLEVEBOLL)
      '------------------------------------------------
      AddHandler oCleBoll.RemoteEvent, AddressOf GestisciEventiEntityBoll
      If oCleBoll.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return False
      If Not oCleBoll.InitExt() Then Return False
      oCleBoll.ApriDoc(strDittaCorrente, True, "B", 0, " ", 0, dsBoll)
      oCleBoll.ResetVar()
      oCleBoll.bModuloCRM = False
      oCleBoll.bIsCRMUser = False
      oCleBoll.strProgChiamante = "BNVEGNBF"

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

  Public Overridable Sub GestisciEventiEntityBoll(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      'gli eventuali messaggi dati da BEVEBOLL tramite la ThrowRemoteEvent li passo a BNVEGNBF
      'solo se non sono messaggi dove viene chiesta una conferma ...
      If e.TipoEvento = "" Then
        If e.Message <> "" Then LogWrite(oApp.Tr(Me, 128843572533598000, "ERROR: ") & e.Message, True)
      ElseIf e.TipoEvento = CLN__STD.ThMsg.MSG_INFO Then
        If e.Message <> "" Then LogWrite(oApp.Tr(Me, 128843573247444000, "INFO: ") & e.Message, True)
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

  Public Overridable Function LegNuma(ByVal strTipo As String, ByVal strSerie As String, ByVal nAnno As Integer) As Integer
    Try
      Return oCldGnbf.LegNuma(strDittaCorrente, strTipo, strSerie, nAnno, False)
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

  Public Overridable Function CreaGegnbmm() As Boolean
    Try
      oCldGnbf.CreaGegnbmm(strDittaCorrente, lIIGegnbmm, strSebpQuery)

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

  Public Overridable Function SettaOrdiniNonASaldo() As Boolean
    Try
      oCldGnbf.SettaOrdiniNonASaldo(strDittaCorrente, lIIGegnbmm, strSebpQuery, lIIGegnbtd, _
                                    lIIGegnbf, lIIGegnbmo, bSebpConssoloasa)

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

  Public Overridable Function FileNuovo() As Boolean
    Try
      oCleBoll.bSpeseIncSoloSuPrimaFatt = CBool(NTSCInt(oCldGnbf.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "SpeseIncSoloSuPrimaFatt", "0", " ", "0"))) 'Se abilitata, non applica spese di incasso, sulle fatture immediatedalla seconda in poi dello stesso mese per lo stesso conto  ' NON DOCUMENTARE
      oCleBoll.strCalcPesi = oCldGnbf.GetSettingBus("Bsveboll", "Opzioni", ".", "Calc_pesi_in_doc", "N", " ", "N")
      'oCleBoll.bPMControllaBaseCostMG = CBool(oCldGnbf.GetSettingBus("OPZIONI", ".", ".", "PMControllaBaseCostMG", "0", " ", "0"))
      'oCleBoll.bPMSoloTaskRilasciatiMG = CBool(oCldGnbf.GetSettingBus("OPZIONI", ".", ".", "PMSoloTaskRilasciatiMG", "0", " ", "0"))
      oCleBoll.bDeterminaBolliSuOperazEsenti = CBool(NTSCInt(oCldGnbf.GetSettingBus("Opzioni", ".", ".", "DeterminaBolliSuOperazEsenti", "0", " ", "0"))) 'Se attiva il bollo non viene determinato solo se in testata vi è il codice di esenzione, ma se la somma delle operazioni esenti del documenti (righe e spese di piede) supera la soglia minima in TABBOTR  ' NON DOCUMENTARE

      oCldGnbf.FileNuovo(strDittaCorrente, lIIGegnbf, strSebpQuery)

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

  Public Overridable Function Apri(ByRef ds As DataSet) As Boolean
    Dim bRes As Boolean = False
    Try
      bRes = oCldGnbf.GetData(strDittaCorrente, lIIGegnbf, strSebpQuery, ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("GEGNBF").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("GEGNBF").ColumnChanged, AddressOf AfterColUpdate

      bHasChanges = False

      Return bRes
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

  Public Overridable Function GetTestateTemp(ByRef dttTmp As DataTable) As Boolean
    Try
      '-------------------------------------------------------------------------------------------------------------
      oCldGnbf.GetTestateTemp(strDittaCorrente, lIIGegnbf, lIIGegnbmm, dttTmp, bDtacRaggruppa)
      '-------------------------------------------------------------------------------------------------------------
      Return True
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '-------------------------------------------------------------------------------------------------------------
      Return False
      '-------------------------------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CreaDocDaNotaPre(ByVal dtrNotaPre As DataRow) As Boolean
    Dim i As Integer = 0
    Dim lNumDoc As Integer = 0
    Dim dSommaValoreRighe As Decimal = 0
    Dim strTmp As String = ""
    Dim strSelect As String = "mma_codart, Sum(mma_quant) AS QuantConai, Min(mma_prezzo) AS MinContrib," & _
                              " Max(mma_prezzo) AS MaxContrib, Min(mma_perescon) AS MinPerescon," & _
                              " Max(mma_perescon) AS MaxPerescon"
    Dim dttTestMag As New DataTable
    Dim dttMovMag As New DataTable
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY

    Try
      '--------------------------------------------------------------------------------------------------------------
      oCldGnbf.GetTestmag(strDittaCorrente, dtrNotaPre, dttTestMag)
      oCldGnbf.GetMovmag(strDittaCorrente, dttTestMag.Rows(0), lIIGegnbmm, bScriviRigheZero, dttMovMag)
      '--------------------------------------------------------------------------------------------------------------
      strTmp = ""
      For Each dtrT As DataRow In dttTestMag.Rows
        strTmp += ": " & dtrT!tm_tipork.ToString & "-" & dtrT!tm_anno.ToString & "-" & dtrT!tm_serie.ToString & "-" & dtrT!tm_numdoc.ToString
      Next
      LogWrite(oApp.Tr(Me, 128843579324644000, "Note di prelievo coinvolte") & strTmp, False)
      '--------------------------------------------------------------------------------------------------------------
      lNumDoc = LegNuma(strDtacTipork, strDtacSerie, nDtacAnno)
      If lNumDoc = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128699231787692325, "Prima di creare un nuovo documento è necessario attivare la numerazione del documento")))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      lNumDoc = lDtacNumdoc     '--- Numero impostato nella form modale per la creazione di nuovi documenti
      Do While oCldGnbf.EsisteDoc(strDittaCorrente, strDtacTipork, nDtacAnno, strDtacSerie, lNumDoc)
        lNumDoc += 1
      Loop
      If bPrimo = True Then
        lDtacNumdoc = lNumDoc
        bPrimo = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCleBoll.NuovoDocumento(strDittaCorrente, strDtacTipork, nDtacAnno, strDtacSerie, lNumDoc)
      oCleBoll.bInNuovoDocSilent = True
      '--------------------------------------------------------------------------------------------------------------
      SettaTestata(dttTestMag.Rows(0))
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To (dttMovMag.Rows.Count - 1)
        '------------------------------------------------------------------------------------------------------------
        ScriviRigaDocDaNotaPre(i, dttMovMag.Rows(i), dttTestMag.Rows(0))
        '------------------------------------------------------------------------------------------------------------
        '--- Somma il valore (+ Iva), di ogni riga coinvolta nel documento da generato
        '------------------------------------------------------------------------------------------------------------
        If dDtacValoreMinimo > 0 Then
          With oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)
            dSommaValoreRighe += NTSCDec(!ec_valore)
            If Not bValMinSoloImponib Then
              dSommaValoreRighe += ArrDbl((NTSCDec(!ec_valore) * oCldGnbf.AliquotaIva(NTSCInt(!ec_codiva)) / 100), oApp.NDecImporti)
            End If
          End With
        End If
        '------------------------------------------------------------------------------------------------------------
      Next
      '--------------------------------------------------------------------------------------------------------------
      SettaPiede(dttTestMag.Rows(0))
      '--------------------------------------------------------------------------------------------------------------
      If (oCleBoll.bConai = True) And (oCleBoll.dttEt_conto.Rows(0)!an_gescon.ToString <> "N") And _
         (oCleBoll.bDocEmesso = True And oCleBoll.dttET.Rows(0)!et_tipork.ToString <> "W") And _
         (oCleBoll.dttET.Rows(0)!et_scorpo.ToString = "N") Then
        If Not oDttgr.NTSGroupBy(oCleBoll.dttMOVCONA, dttGr, strSelect, "", "mma_codart") Then Return False
        If dttGr.Rows.Count > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128830516239134838, "Non è possibile generare il documento " & vbCrLf & _
            "Tipo doc.:|" & dttTestMag.Rows(0)!tm_tipork.ToString & "|" & _
            " Anno:|" & dttTestMag.Rows(0)!tm_anno.ToString & "|" & _
            " Serie:|" & dttTestMag.Rows(0)!tm_serie.ToString & "|" & _
            " Num. doc.:| " & dttTestMag.Rows(0)!tm_numdoc.ToString & "|" & _
            vbCrLf & " perché sono presenti articoli con gestione conai e che per poterlo creare occorre utilizzare il programma 'Gestione documenti di magazzino'")))
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (dDtacValoreMinimo > 0) And (dSommaValoreRighe < dDtacValoreMinimo) Then
        '------------------------------------------------------------------------------------------------------------
        strTmp = oApp.Tr(Me, 130367767384110804, "La Nota di Prelievo" & vbCrLf & _
          "   N° |" & dttTestMag.Rows(0)!tm_numdoc.ToString & _
          IIf(dttTestMag.Rows(0)!tm_serie.ToString.Trim <> "", "/", "").ToString & _
          dttTestMag.Rows(0)!tm_serie.ToString.Trim & _
          "| del |" & dttTestMag.Rows(0)!tm_anno.ToString & "|" & vbCrLf & _
          "   NON è stata considerata nella generazione dei documenti di magazzino," & vbCrLf & _
          "   perché il valore totale delle righe (|" & dSommaValoreRighe.ToString(oApp.FormatImporti) & "|)" & vbCrLf & _
          "   non è uguale o superiore al valore minimo indicato (|" & dDtacValoreMinimo.ToString(oApp.FormatImporti) & "|)." & vbCrLf)
        LogWrite(strTmp, True)
        '------------------------------------------------------------------------------------------------------------
        Return False
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
      CorrezioneDatiPreSalvataggio()
      If Not oCleBoll.SalvaDocumento("N") Then
        LogWrite(oApp.Tr(Me, 128843594045134000, "Documento non salvato"), True)
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCldGnbf.UpdateGegnbf(strDittaCorrente, lIIGegnbf, strDtacTipork, NTSCStr(nDtacAnno), strDtacSerie, lNumDoc)
      '--------------------------------------------------------------------------------------------------------------
      lANumero = lNumDoc
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CreaDocDaNotaPreRagg(ByVal dttNote As DataTable) As Boolean
    'dttNote contiene l'elenco delle note di prelievo visibili nella griglia di BNVEGNBF
    '        deve già essere ordinato in modo da generare il minor numero di documenti 
    '        a parità di raggruppamento (agente, tipobf, tm_scont1, tm_scont2, ...)
    Dim bScriviPiede As Boolean = False
    Dim bOk As Boolean = True
    Dim nNota As Integer = 0
    Dim y As Integer = 0
    Dim lNumDoc As Integer = 0
    Dim dSommaValoreRighe As Decimal = 0
    Dim strTmp As String = ""
    Dim strCHIAVE As String = ""            'chiave del documento in fase di creazione
    Dim strCHIAVECorrente As String = ""    'chiave del documento precedente a quello in analisi
    Dim strCHIAVESuccessiva As String = ""  'chiave del documento successovo a quello in analisi che dovrà essere creato
    Dim strSelect As String = "mma_codart, Sum(mma_quant) AS QuantConai, Min(mma_prezzo) AS MinContrib," & _
                              " Max(mma_prezzo) AS MaxContrib, Min(mma_perescon) AS MinPerescon," & _
                              " Max(mma_perescon) AS MaxPerescon"
    Dim dttTestPrb As New DataTable
    Dim dttMovPrb As New DataTable
    Dim dttGr As New DataTable
    Dim dttNoteCoinvolte As New DataTable
    Dim oDttgr As New CLEGROUPBY
    Dim nRigaNewDoc As Integer = -1             'numero di riga all'interno del documento in creazione

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttNoteCoinvolte.Columns.Add("anno", GetType(Integer))
      dttNoteCoinvolte.Columns.Add("serie", GetType(String))
      dttNoteCoinvolte.Columns.Add("numero", GetType(Integer))
      '--------------------------------------------------------------------------------------------------------------
      For nNota = 0 To (dttNote.Rows.Count - 1)
        '------------------------------------------------------------------------------------------------------------
        With dttNote.Rows(nNota)
          dttNoteCoinvolte.Rows.Add(New Object() {NTSCInt(!tm_anno), NTSCStr(!tm_serie), NTSCInt(!tm_numdoc)})
        End With
        '------------------------------------------------------------------------------------------------------------
        'prelevo testprb.* della nota di prelievo da convertire in documento
        oCldGnbf.GetTestmag(strDittaCorrente, dttNote.Rows(nNota), dttTestPrb)
        '------------------------------------------------------------------------------------------------------------
        strTmp = ""
        For Each dtrT As DataRow In dttTestPrb.Rows
          strTmp += ": " & dtrT!tm_tipork.ToString & "-" & dtrT!tm_anno.ToString & "-" & dtrT!tm_serie.ToString & "-" & dtrT!tm_numdoc.ToString
        Next
        LogWrite(oApp.Tr(Me, 129648712299839768, "Note di prelievo coinvolte") & strTmp, False)
        '------------------------------------------------------------------------------------------------------------
        strCHIAVECorrente = CostriusciChiave(dttNote.Rows(nNota))
        If strCHIAVECorrente <> strCHIAVE Then
          lNumDoc = LegNuma(strDtacTipork, strDtacSerie, nDtacAnno)
          If lNumDoc = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129648712331683433, "Attenzione!" & vbCrLf & _
                            "Prima di creare un nuovo documento è necessario attivare la numerazione del documento.")))
            Return False
          End If
          lNumDoc = lDtacNumdoc     '--- Numero impostato nella form modale per la creazione di nuovi documenti
          Do While oCldGnbf.EsisteDoc(strDittaCorrente, strDtacTipork, nDtacAnno, strDtacSerie, lNumDoc)
            lNumDoc += 1
          Loop
          If bPrimo = True Then
            lDtacNumdoc = lNumDoc
            bPrimo = False
          End If
          '---------------------------------------------------------------------------------------------------------
          LogWrite(oApp.Tr(Me, 129648712363566158, "Creazione documento in corso..."), False)
          '---------------------------------------------------------------------------------------------------------
          oCleBoll.NuovoDocumento(strDittaCorrente, strDtacTipork, nDtacAnno, strDtacSerie, lNumDoc)
          oCleBoll.bInNuovoDocSilent = True
          nRigaNewDoc = -1
          '---------------------------------------------------------------------------------------------------------
          SettaTestata(dttTestPrb.Rows(0))
          '---------------------------------------------------------------------------------------------------------
          dSommaValoreRighe = 0
          '---------------------------------------------------------------------------------------------------------
        ElseIf bSommaPesoColli Then
          oCleBoll.dttET.Rows(0)!et_peso = NTSCDec(oCleBoll.dttET.Rows(0)!et_peso) + NTSCDec(dttTestPrb.Rows(0)!tm_peso)
          oCleBoll.dttET.Rows(0)!et_pesonetto = NTSCDec(oCleBoll.dttET.Rows(0)!et_pesonetto) + NTSCDec(dttTestPrb.Rows(0)!tm_pesonetto)
          oCleBoll.dttET.Rows(0)!et_totcoll = NTSCDec(oCleBoll.dttET.Rows(0)!et_totcoll) + NTSCDec(dttTestPrb.Rows(0)!tm_totcoll)
        End If
        '------------------------------------------------------------------------------------------------------------
        oCldGnbf.GetMovmag(strDittaCorrente, dttTestPrb.Rows(0), lIIGegnbmm, bScriviRigheZero, dttMovPrb)
        For y = 0 To (dttMovPrb.Rows.Count - 1)
          nRigaNewDoc += 1
          '----------------------------------------------------------------------------------------------------------
          ScriviRigaDocDaNotaPre(nRigaNewDoc, dttMovPrb.Rows(y), dttTestPrb.Rows(0))
          '----------------------------------------------------------------------------------------------------------
          '--- Somma il valore (+ Iva), di ogni riga coinvolta nel documento da generato
          '----------------------------------------------------------------------------------------------------------
          If dDtacValoreMinimo > 0 Then
            With oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)
              dSommaValoreRighe += ArrDbl(NTSCDec(!ec_valore) + (NTSCDec(!ec_valore) * NTSCInt(!ec_codiva) / 100), 2)
            End With
          End If
          '----------------------------------------------------------------------------------------------------------
        Next
        '------------------------------------------------------------------------------------------------------------
        strCHIAVE = CostriusciChiave(dttNote.Rows(nNota))
        '------------------------------------------------------------------------------------------------------------
        bScriviPiede = False
        If (nNota = (dttNote.Rows.Count - 1)) Then
          'se è l'ultimo doc da generare, salvo il documento
          bScriviPiede = True
        Else
          'se la chiave successiva del documento da generare è diversa da quella corrente, salvo il documento
          strCHIAVESuccessiva = CostriusciChiave(dttNote.Rows(nNota + 1))
          If strCHIAVE <> strCHIAVESuccessiva Then bScriviPiede = True
        End If
        If bScriviPiede = True Then
          '----------------------------------------------------------------------------------------------------------
          SettaPiede(dttTestPrb.Rows(0))
          '----------------------------------------------------------------------------------------------------------
          If (oCleBoll.bConai = True) And (oCleBoll.dttEt_conto.Rows(0)!an_gescon.ToString <> "N") And _
             (oCleBoll.bDocEmesso = True And oCleBoll.dttET.Rows(0)!et_tipork.ToString <> "W") And _
             (oCleBoll.dttET.Rows(0)!et_scorpo.ToString = "N") Then
            If Not oDttgr.NTSGroupBy(oCleBoll.dttMOVCONA, dttGr, strSelect, "", "mma_codart") Then Return False
            If dttGr.Rows.Count > 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129648712392695153, "Attenzione!" & vbCrLf & _
                              "Non è possibile generare il documento:" & vbCrLf & _
                              "   Tipo: |" & dttTestPrb.Rows(0)!tm_tipork.ToString & "|" & vbCrLf & _
                              "   Anno: |" & dttTestPrb.Rows(0)!tm_anno.ToString & "|" & vbCrLf & _
                              "   Serie: |" & dttTestPrb.Rows(0)!tm_serie.ToString & "|" & vbCrLf & _
                              "   Numero: | " & dttTestPrb.Rows(0)!tm_numdoc.ToString & "|" & vbCrLf & _
                              "perché sono presenti articoli con gestione CONAI" & vbCrLf & _
                              "e per poterlo creare occorre utilizzare il programma 'Gestione Documenti di Magazzino'.")))
              Return False
            End If
            '--------------------------------------------------------------------------------------------------------
          End If
          '----------------------------------------------------------------------------------------------------------
          bOk = True
          '----------------------------------------------------------------------------------------------------------
          If (dDtacValoreMinimo > 0) And (dSommaValoreRighe < dDtacValoreMinimo) Then
            '--------------------------------------------------------------------------------------------------------
            bOk = False
            '--------------------------------------------------------------------------------------------------------
            Select Case dttNoteCoinvolte.Rows.Count
              Case 1
                strTmp = oApp.Tr(Me, 130367768299417446, "La Nota di Prelievo" & vbCrLf & _
                          "   N° |" & dttNoteCoinvolte.Rows(0)!numero.ToString & _
                          IIf(dttNoteCoinvolte.Rows(0)!serie.ToString.Trim <> "", "/", "").ToString & _
                          dttNoteCoinvolte.Rows(0)!serie.ToString.Trim & _
                          "| del |" & dttNoteCoinvolte.Rows(0)!anno.ToString & "|" & vbCrLf & _
                          "   NON è stata considerata nella generazione dei documenti di magazzino," & vbCrLf)
              Case Else
                strTmp = oApp.Tr(Me, 130367768746758333, "Le seguenti Note di Prelievo:") & vbCrLf
                For y = 0 To (dttNoteCoinvolte.Rows.Count - 1)
                  strTmp += oApp.Tr(Me, 130367769997531578, "   N° |" & dttNoteCoinvolte.Rows(y)!numero.ToString & _
                            IIf(dttNoteCoinvolte.Rows(y)!serie.ToString.Trim <> "", "/", "").ToString & _
                            dttNoteCoinvolte.Rows(y)!serie.ToString.Trim & _
                            "| del |" & dttNoteCoinvolte.Rows(y)!anno.ToString & "|" & vbCrLf)
                Next
                strTmp += oApp.Tr(Me, 130367770442216232, "   NON sono state considerate nella generazione dei documenti di magazzino,") & vbCrLf
            End Select
            strTmp += oApp.Tr(Me, 130367770589559039, "   perché il valore totale delle righe (|" & _
                      dSommaValoreRighe.ToString(oApp.FormatImporti) & "|)" & vbCrLf & _
                      "   non è uguale o superiore al valore minimo indicato (|" & dDtacValoreMinimo.ToString(oApp.FormatImporti) & "|).") & vbCrLf
            '--------------------------------------------------------------------------------------------------------
            LogWrite(strTmp, True)
            '--------------------------------------------------------------------------------------------------------
          End If
          '----------------------------------------------------------------------------------------------------------
          If bOk = True Then
            CorrezioneDatiPreSalvataggio()
            If oCleBoll.SalvaDocumento("N") = False Then
              LogWrite(oApp.Tr(Me, 129648712432116458, "Documento non salvato."), True)
            End If
          End If
          '----------------------------------------------------------------------------------------------------------
          dttNoteCoinvolte.Clear()
          '----------------------------------------------------------------------------------------------------------
        End If
        '------------------------------------------------------------------------------------------------------------
        oCldGnbf.UpdateGegnbf(strDittaCorrente, lIIGegnbf, strDtacTipork, NTSCStr(nDtacAnno), strDtacSerie, lNumDoc)
        '------------------------------------------------------------------------------------------------------------
        lANumero = lNumDoc
        '------------------------------------------------------------------------------------------------------------
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
      '--------------------------------------------------------------------------------------------------------------
    Finally
      dttTestPrb.Clear() : dttTestPrb.Dispose()
      dttMovPrb.Clear() : dttMovPrb.Dispose()
      dttGr.Clear() : dttGr.Dispose()
    End Try
  End Function

  Public Overridable Function CostriusciChiave(ByRef dtrT As DataRow) As String
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      With dtrT
        strTmp = NTSCStr(!CHIAVE) & _
          !tm_codagen.ToString.PadLeft(4, "0"c) & !tm_codagen2.ToString.PadLeft(4, "0"c) & _
          !tm_tipobf.ToString.PadLeft(4, "0"c) & !tm_codpaga.ToString.PadLeft(4, "0"c) & _
          !tm_codese.ToString.PadLeft(4, "0"c) & _
          !tm_scont1.ToString.PadLeft(6, "0"c) & !tm_scont2.ToString.PadLeft(6, "0"c) & _
          !tm_scopag.ToString.PadLeft(6, "0"c) & !tm_valuta.ToString.PadLeft(4, "0"c) & _
          !tm_scorpo.ToString & !tm_listino.ToString.PadLeft(4, "0"c) & _
          NTSCStr(!datapag).PadRight(10) & !tm_flspinc.ToString & !tm_flbolli.ToString & _
          !tm_coddest2.ToString.PadLeft(9, "0"c) & _
          NTSCStr(!tm_cup).PadRight(15) & _
          NTSCStr(!tm_cig).PadRight(10) & _
          NTSCStr(!tm_riferimpa).PadRight(100)
      End With
      '--------------------------------------------------------------------------------------------------------------
      Return strTmp
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return ""
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SettaTestata(ByVal dtrNotaPre As DataRow) As Boolean
    Dim dCambio As Decimal
    Try
      'non faccio ricalcolare colli pesi
      oCleBoll.bCalcolaColli = False
      oCleBoll.bCalcolaPesoLordo = False
      oCleBoll.bCalcolaPesoNetto = False

      oCleBoll.dttET.Rows(0)!et_conto = dtrNotaPre!tm_conto
      oCleBoll.dttET.Rows(0)!et_tipobf = dtrNotaPre!tm_tipobf
      oCleBoll.dttET.Rows(0)!et_magaz = dtrNotaPre!tm_magaz
      oCleBoll.dttET.Rows(0)!et_magaz2 = dtrNotaPre!tm_magaz2
      oCleBoll.dttET.Rows(0)!et_codese = dtrNotaPre!tm_codese
      oCleBoll.dttET.Rows(0)!et_flspinc = dtrNotaPre!tm_flspinc
      oCleBoll.dttET.Rows(0)!et_codpaga = dtrNotaPre!tm_codpaga
      If NTSCStr(dtrNotaPre!tm_datapag) = "" Then
        oCleBoll.dttET.Rows(0)!et_datapag = NTSCStr(dDtacDatdoc)
      Else
        oCleBoll.dttET.Rows(0)!et_datapag = dtrNotaPre!tm_datapag
      End If
      oCleBoll.dttET.Rows(0)!et_codagen = dtrNotaPre!tm_codagen
      oCleBoll.dttET.Rows(0)!et_listino = dtrNotaPre!tm_listino
      oCleBoll.dttET.Rows(0)!et_scont1 = dtrNotaPre!tm_scont1
      oCleBoll.dttET.Rows(0)!et_scont2 = dtrNotaPre!tm_scont2
      oCleBoll.dttET.Rows(0)!et_scopag = dtrNotaPre!tm_scopag
      oCleBoll.dttET.Rows(0)!et_porto = dtrNotaPre!tm_porto
      oCleBoll.dttET.Rows(0)!et_coddest = dtrNotaPre!tm_coddest
      oCleBoll.dttET.Rows(0)!et_coddest2 = dtrNotaPre!tm_coddest2
      oCleBoll.dttET.Rows(0)!et_codagen2 = dtrNotaPre!tm_codagen2
      oCleBoll.dttET.Rows(0)!et_vettor = dtrNotaPre!tm_vettor
      oCleBoll.dttET.Rows(0)!et_abi = dtrNotaPre!tm_abi
      oCleBoll.dttET.Rows(0)!et_cab = dtrNotaPre!tm_cab
      oCleBoll.dttET.Rows(0)!et_banc1 = dtrNotaPre!tm_banc1
      oCleBoll.dttET.Rows(0)!et_banc2 = dtrNotaPre!tm_banc2
      oCleBoll.dttET.Rows(0)!et_datdoc = dDtacDatdoc
      oCleBoll.dttET.Rows(0)!et_riferim = dtrNotaPre!tm_riferim
      oCleBoll.dttET.Rows(0)!et_codrsta = dtrNotaPre!tm_codrsta
      oCleBoll.dttET.Rows(0)!et_codport = dtrNotaPre!tm_codport
      oCleBoll.dttET.Rows(0)!et_codntra = dtrNotaPre!tm_codntra
      oCleBoll.dttET.Rows(0)!et_controp = dtrNotaPre!tm_controp
      oCleBoll.dttET.Rows(0)!et_valuta = dtrNotaPre!tm_valuta
      oCleBoll.dttET.Rows(0)!et_scorpo = dtrNotaPre!tm_scorpo
      oCleBoll.dttET.Rows(0)!et_acuradi = dtrNotaPre!tm_acuradi
      oCleBoll.dttET.Rows(0)!et_dtiniz = NTSCDate(strDtacDtiniz)
      oCleBoll.dttET.Rows(0)!et_oriniz = dDtacOriniz
      oCleBoll.dttET.Rows(0)!et_totcoll = dtrNotaPre!tm_totcoll
      oCleBoll.dttET.Rows(0)!et_caustra = dtrNotaPre!tm_caustra
      oCleBoll.dttET.Rows(0)!et_peso = dtrNotaPre!tm_peso
      oCleBoll.dttET.Rows(0)!et_flboll = dtrNotaPre!tm_flbolli
      oCleBoll.dttET.Rows(0)!et_note = dtrNotaPre!tm_note
      oCleBoll.dttET.Rows(0)!et_causale = dtrNotaPre!tm_causale
      oCleBoll.dttET.Rows(0)!et_codcena = dtrNotaPre!tm_codcena
      oCleBoll.dttET.Rows(0)!et_codaspe = dtrNotaPre!tm_codaspe
      oCleBoll.dttET.Rows(0)!et_aspetto = dtrNotaPre!tm_aspetto
      oCleBoll.dttET.Rows(0)!et_pesonetto = dtrNotaPre!tm_pesonetto
      oCleBoll.dttET.Rows(0)!et_commeca = dtrNotaPre!tm_commeca
      oCleBoll.dttET.Rows(0)!et_subcommeca = dtrNotaPre!tm_subcommeca
      oCleBoll.dttET.Rows(0)!et_coddivi = dtrNotaPre!tm_coddivi
      oCleBoll.dttET.Rows(0)!et_codcli = dtrNotaPre!tm_codcli
      oCleBoll.dttET.Rows(0)!et_ultagg = Now
      oCleBoll.dttET.Rows(0)!et_vettor2 = dtrNotaPre!tm_vettor2
      oCleBoll.dttET.Rows(0)!et_codbanc = dtrNotaPre!tm_codbanc
      oCleBoll.dttET.Rows(0)!et_opnome = oApp.User.Nome
      oCleBoll.dttET.Rows(0)!et_autpag = dtrNotaPre!tm_autpag
      oCleBoll.dttET.Rows(0)!et_annotco = dtrNotaPre!tm_annotco
      oCleBoll.dttET.Rows(0)!et_codstag = dtrNotaPre!tm_codstag
      oCleBoll.dttET.Rows(0)!et_codcfam = dtrNotaPre!tm_codcfam
      oCleBoll.dttET.Rows(0)!et_contfatt = dtrNotaPre!tm_contfatt

      '----------------------------
      'Operazione sul cambio
      If NTSCInt(dtrNotaPre!tm_valuta) <> 0 Then
        dCambio = 1 'default (in futuro usare ORDLIST.ol_cambio)
        If (nSvalOpzione = 2) Or (nSvalOpzione = 3) Then
          dCambio = RitornaCambio(NTSCInt(dtrNotaPre!tm_valuta), nSvalOpzione, NTSCStr(dDtacDatdoc))
        End If
      Else
        dCambio = 0
      End If
      oCleBoll.dttET.Rows(0)!et_cambio = dCambio

      ' se necessario aggiorna i dati accompagnatori
      If bDtacVariaAcc Then
        oCleBoll.dttET.Rows(0)!et_acuradi = strDtacAcuradi
        oCleBoll.dttET.Rows(0)!et_vettor = nDtacCodvett
      End If

      'setto le var per la stampa
      If NTSCInt(oCleBoll.dttET.Rows(0)!et_valuta) > 0 Then bVal = True Else bVal = False
      If NTSCStr(oCleBoll.dttET.Rows(0)!et_scorpo) = "S" Then bScorp = True Else bScorp = False

      oCleBoll.dttET.Rows(0)!et_cup = dtrNotaPre!tm_cup
      oCleBoll.dttET.Rows(0)!et_cig = dtrNotaPre!tm_cig
      oCleBoll.dttET.Rows(0)!et_riferimpa = dtrNotaPre!tm_riferimpa

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

  Public Overridable Function SettaPiede(ByVal dtrNotaPre As DataRow) As Boolean
    Try
      oCleBoll.dttET.Rows(0)!et_speacc = dtrNotaPre!tm_speacc
      oCleBoll.dttET.Rows(0)!et_pagato = dtrNotaPre!tm_pagato
      oCleBoll.dttET.Rows(0)!et_abbuono = dtrNotaPre!tm_abbuono
      oCleBoll.dttET.Rows(0)!et_speimb = dtrNotaPre!tm_speimb

      oCleBoll.dttET.Rows(0)!et_speaccv = dtrNotaPre!tm_speaccv
      oCleBoll.dttET.Rows(0)!et_pagatov = dtrNotaPre!tm_pagatov
      oCleBoll.dttET.Rows(0)!et_abbuonov = dtrNotaPre!tm_abbuonov
      oCleBoll.dttET.Rows(0)!et_speimbv = dtrNotaPre!tm_speimbv

      'devo prendere totale colli, peso lordo e netto della nota di prelievo
      oCleBoll.CalcolaTotali()

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

  Public Overridable Function ScriviRigaDocDaNotaPre(ByVal i As Integer, ByVal dtrMm As DataRow, _
                                                     ByVal dtrTm As DataRow) As Boolean
    Dim dQuant As Decimal = 0
    Dim dColli As Decimal = 0
    Dim strErr As String = ""
    Dim nRiga As Integer
    Dim dttTmp As New DataTable
    Dim dPrezzo As Decimal
    Dim dPrezvalc As Decimal
    Dim dtrT() As DataRow = Nothing
    Dim j As Integer = 0
    Dim dttTmp2 As New DataTable
    Dim strPrzNet As String = ""
    Dim dCambio As Decimal = 0
    Dim dPreziva As Decimal = 0
    Dim nClscar As Integer = 0
    Dim nClprar As Integer = 0
    Dim nClscan As Integer = 0
    Dim nClpran As Integer = 0
    Dim dttTEMP As New DataTable
    Dim dScont1 As Decimal = 0
    Dim dScont2 As Decimal = 0
    Dim dScont3 As Decimal = 0
    Dim dScont4 As Decimal = 0
    Dim dScont5 As Decimal = 0
    Dim dScont6 As Decimal = 0
    Dim dProvv As Decimal = 0
    Dim dProvv2 As Decimal = 0
    Dim dTmpvprovvun As Decimal = 0
    Dim dTmpvprovvun2 As Decimal = 0
    Dim strFlstat As String = ""

    Try
      nRiga = (i + 1) * lIncrementoContatoreRiga

      oCleBoll.bInImportRigheOrd = True

      Select Case NTSCStr(dtrMm!mm_flkit)
        Case " "
          lRigaPadre = 0
          bRigaFiglio = False
        Case "A", "S"
          lRigaPadre = nRiga
          bRigaFiglio = False
        Case "B", "T"
          bRigaFiglio = True
      End Select

      'Creo una nuova riga di corpo setto i principali campi poi setto tutti gli altri
      oCleBoll.AggiungiRigaCorpo(False, NTSCStr(dtrMm!mm_codart), NTSCInt(dtrMm!mm_fase), nRiga, _
                                  NTSCInt(dtrMm!mm_causale), NTSCInt(dtrMm!mm_magaz))

      With oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)
        !ec_magaz2 = dtrMm!mm_magaz2
        !ec_causale2 = dtrMm!mm_causale2
        !ec_descr = dtrMm!mm_descr
        !ec_desint = dtrMm!mm_desint
        !ec_controp = dtrMm!mm_controp
        !ec_contocontr = dtrMm!mm_contocontr
        !ec_unmis = dtrMm!mm_unmis
        !ec_colli = dtrMm!mm_colli
        !ec_quant = dtrMm!mm_quant
        !ec_prezzo = dtrMm!mm_prezzo
        !ec_scont1 = dtrMm!mm_scont1
        !ec_scont2 = dtrMm!mm_scont2
        !ec_scont3 = dtrMm!mm_scont3
        !ec_codiva = dtrMm!mm_codiva
        !ec_preziva = dtrMm!mm_preziva
        !ec_prezvalc = dtrMm!mm_prezvalc
        !ec_provv = dtrMm!mm_provv
        !ec_commen = dtrMm!mm_commen
        !ec_flelab = dtrMm!mm_flelab
        !ec_flcom = dtrMm!mm_flcom
        !ec_ricimp = dtrMm!mm_flstat
        !ec_ortipo = dtrMm!mm_ortipo
        !ec_oranno = dtrMm!mm_oranno
        !ec_orserie = dtrMm!mm_orserie
        !ec_ornum = dtrMm!mm_ornum
        !ec_orriga = dtrMm!mm_orriga
        !ec_salcon = dtrMm!mm_salcon
        !ec_matric = dtrMm!mm_matric
        !ec_stasino = dtrMm!mm_stasino
        !ec_prelist = dtrMm!mm_prelist
        !ec_codnomc = dtrMm!mm_codnomc
        !ec_massakg = dtrMm!mm_massakg
        !ec_massaum2 = dtrMm!mm_massaum2
        !ec_valstat = dtrMm!mm_valstat
        !ec_proorig = dtrMm!mm_proorig
        !ec_provv2 = dtrMm!mm_provv2
        !ec_percvst = dtrMm!mm_percvst
        !ec_codcfam = dtrMm!mm_codcfam
        !ec_commeca = dtrMm!mm_commeca
        !ec_subcommeca = dtrMm!mm_subcommeca
        !ec_coddivi = dtrMm!mm_coddivi
        '!ec_valore = dtrMm!mm_valore se il documento è in valuta ed il valore del cambio è diverso tra nota di prelievo e ddt in generazione non va bene! deve prendere il valore in euro con il cambio del ddt (come già fa VEBOLL) e come è già stato calcolato correttamente in bemgdocu prima di eseguire questa riga
        !ec_qtadisimp = dtrMm!mm_qtadisimp
        !ec_coldisimp = dtrMm!mm_coldisimp
        !ec_valdisimp = dtrMm!mm_valdisimp
        !ec_lotto = dtrMm!mm_lotto
        !ec_qtafatt = dtrMm!mm_qtafatt
        !ec_codcena = dtrMm!mm_codcena
        !ec_codvuo = dtrMm!mm_codvuo
        !ec_vprovv = dtrMm!mm_vprovv
        !ec_vprovv2 = dtrMm!mm_vprovv2
        !ec_ump = dtrMm!mm_ump
        !ec_note = dtrMm!mm_note
        !ec_misura1 = dtrMm!mm_misura1
        !ec_misura2 = dtrMm!mm_misura2
        !ec_misura3 = dtrMm!mm_misura3
        !ec_quant = dtrMm!mm_quant 'Risetto la quantità, perchè se unità di misura formula, con il cambio delle unità di misura può cambiare la quantità
        !ec_numpac = dtrMm!mm_numpac
        !ec_codclie = dtrMm!mm_codclie
        !ec_prtipo = dtrMm!mm_prtipo
        !ec_pranno = dtrMm!mm_pranno
        !ec_prserie = dtrMm!mm_prserie
        !ec_prnum = dtrMm!mm_prnum
        !ec_prriga = dtrMm!mm_prriga
        !ec_cltipo = dtrMm!mm_cltipo
        !ec_clanno = dtrMm!mm_clanno
        !ec_clserie = dtrMm!mm_clserie
        !ec_clnum = dtrMm!mm_clnum
        !ec_clriga = dtrMm!mm_clriga
        !ec_nptipo = dtrMm!mm_nptipo
        !ec_npanno = dtrMm!mm_npanno
        !ec_npserie = dtrMm!mm_npserie
        !ec_npnum = dtrMm!mm_npnum
        !ec_npriga = dtrMm!mm_npriga
        !ec_npqtadis = dtrMm!mm_npqtadis
        !ec_npcoldis = dtrMm!mm_npcoldis
        !ec_npvaldis = dtrMm!mm_npvaldis
        !ec_npsalcon = dtrMm!mm_npsalcon
        !ec_nprcoleva = dtrMm!mm_nprcoleva
        !ec_nprquaeva = dtrMm!mm_nprquaeva
        !ec_nprflevas = dtrMm!mm_nprflevas
        !ec_nprvalore = dtrMm!mm_nprvalore
        !ec_ultagg = Now
        !xxo_npultagg = dtrMm!mm_ultagg
        !ec_perqta = dtrMm!mm_perqta
        !ec_scont4 = dtrMm!mm_scont4
        !ec_scont5 = dtrMm!mm_scont5
        !ec_scont6 = dtrMm!mm_scont6
        !ec_scontp = dtrMm!mm_scontp
        !ec_scontv = 0 'FISSO 0: se nell'impegno ho fatto un abbuono a valore e la quantità di riga è dviersa da 1, quando evado parzialmente la quantità andrebbe riproporzionalizzato. questo sconto è compilato solo da veboll/retail su documenti no IC o note di prel
        !ec_pmtaskid = dtrMm!mm_pmtaskid
        '!ec_pmsalcon = strPmsalcon 'per modulo pm
        '!ec_pmqtadis = dPmqtadis
        '!ec_pmvaldis = dPmvaldis
        !ec_ubicaz = dtrMm!mm_ubicaz
        !ec_flkit = dtrMm!mm_flkit
        !ec_ktriga = IIf(bRigaFiglio = True, lRigaPadre, 0)
        !ec_valorev = dtrMm!mm_valorev
        !ec_codtpro = dtrMm!mm_codtpro
        !ec_flprznet = dtrMm!mm_flprznet
        If oCleBoll.bNonEreditareDateCompDaOrd = True Then
          !ec_datini = oCleBoll.dttET.Rows(0)!et_datdoc
          !ec_datfin = oCleBoll.dttET.Rows(0)!et_datdoc
        Else
          !ec_datini = dtrMm!mm_datini
          !ec_datfin = dtrMm!mm_datfin
        End If

        '----------------------------------------------------------------------------------
        'Se utilizzata l'opzione di registro EREDITAMATRICOLE, in presenza di una riga della nota
        'di prelievo non affatto evasa con matricole associate => viene generata una riga di
        'documento a saldo ereditando le matricole della nota
        If bEreditaMatricole Then
          'In presenza di una riga non affatto evasa
          If NTSCStr(dtrMm!mm_nprflevas) = "C" And NTSCInt(dtrMm!mm_nprquaeva) = 0 Then
            'con matricole associate

            oCldGnbf.GetMatricole(strDittaCorrente, dtrMm, dttTmp)

            If Not dttTmp.Rows.Count = 0 Then
              For j = 0 To dttTmp.Rows.Count - 1
                'Eredito le matricole per la riga di documento a saldo
                oCleBoll.dttMOVMATR.Rows.Add(oCleBoll.dttMOVMATR.NewRow)
                oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!codditt = strDittaCorrente
                oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_tipork = !ec_tipork
                oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_anno = !ec_anno
                oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_serie = !ec_serie
                oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_numdoc = !ec_numdoc
                oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_riga = !ec_riga
                oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_rigaa = j + 1
                oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_quant = dttTmp.Rows(j)!mma_quant
                oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_matric = dttTmp.Rows(j)!mma_matric
                oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_note = dttTmp.Rows(j)!mma_note
              Next

              'aggiorno il campo di movmag
              !ec_npsalcon = "S"
            End If
          End If
        End If

        If oCldGnbf.ValCodiceDb(NTSCStr(dtrMm!mm_codart), strDittaCorrente, "ARTICO", "S", "", dttTEMP) Then
          nClscar = NTSCInt(dttTEMP.Rows(0)!ar_clascon)
          nClprar = NTSCInt(dttTEMP.Rows(0)!ar_claprov)
        End If
        If oCldGnbf.ValCodiceDb(NTSCStr(oCleBoll.dttET.Rows(0)!et_conto), strDittaCorrente, "ANAGRA", "N", "", dttTEMP) Then
          nClscan = NTSCInt(dttTEMP.Rows(0)!an_clascon)
          nClpran = NTSCInt(dttTEMP.Rows(0)!an_claprov)
        End If

        '-----------------------------------------------------------
        Dim oCondCommerciali As NTSCondCommerciali = IstanziaNTSCondCommerciali()
        oCondCommerciali.bCalcolaPrezzo = bRicalcPrez
        oCondCommerciali.bCalcolaSconti = bRicalcScon
        oCondCommerciali.bCalcolaProvvigioni = bRicalcProv
        With oCondCommerciali.Input
          .strDitta = strDittaCorrente
          .strCodart = NTSCStr(dtrMm!mm_codart)
          .lConto = NTSCInt(oCleBoll.dttET.Rows(0)!et_conto)
          .lDestdiv = NTSCInt(oCleBoll.dttET.Rows(0)!et_coddest)
          .nListino = NTSCInt(oCleBoll.dttET.Rows(0)!et_listino)
          .nFase = NTSCInt(dtrMm!mm_fase)
          .strTipoval = "P"
          .bConspromo = True
          .nCodpromo = NTSCInt(dtrMm!mm_codtpro)
          .dtDatdoc = NTSCDate(oCleBoll.dttET.Rows(0)!et_datdoc)
          .nCodvalu = NTSCInt(oCleBoll.dttET.Rows(0)!et_valuta)
          .dQuant = NTSCDec(dtrMm!mm_quant)

          .nClscar = nClscar
          .nClscan = nClscan
          .strPrzNet = strPrzNet

          .nClprar = nClprar
          .nClpran = nClpran
          .nCodage1 = NTSCInt(oCleBoll.dttET.Rows(0)!et_codagen)
          .nCodage2 = NTSCInt(oCleBoll.dttET.Rows(0)!et_codagen2)
          .dSconto1 = NTSCDec(oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)!ec_scont1)
          .dSconto2 = NTSCDec(oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)!ec_scont2)
          .dSconto3 = NTSCDec(oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)!ec_scont3)
          .dSconto4 = NTSCDec(oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)!ec_scont4)
          .dSconto5 = NTSCDec(oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)!ec_scont5)
          .dSconto6 = NTSCDec(oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)!ec_scont6)
          .dScontT1 = NTSCDec(oCleBoll.dttET.Rows(0)!et_scont1)
          .dScontT2 = NTSCDec(oCleBoll.dttET.Rows(0)!et_scont2)
          .dPrezzo = NTSCDec(dtrMm!mm_prezzo)
        End With
        '----------------------------------
        CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
        '----------------------------------
        If bRicalcScon Then
          !ec_scont1 = oCondCommerciali.OutputSconti.dSconto1
          !ec_scont2 = oCondCommerciali.OutputSconti.dSconto2
          !ec_scont3 = oCondCommerciali.OutputSconti.dSconto3
          !ec_scont4 = oCondCommerciali.OutputSconti.dSconto4
          !ec_scont5 = oCondCommerciali.OutputSconti.dSconto5
          !ec_scont6 = oCondCommerciali.OutputSconti.dSconto6
        End If
        If bRicalcPrez Then
          If NTSCInt(oCleBoll.dttET.Rows(0)!et_valuta) <> 0 Then
            If (nSvalOpzione = 2) Or (nSvalOpzione = 3) Then
              dCambio = RitornaCambio(NTSCInt(oCleBoll.dttET.Rows(0)!et_valuta), nSvalOpzione, NTSCStr(oCleBoll.dttET.Rows(0)!et_datdoc))
            End If
            dPrezvalc = oCondCommerciali.OutputPrezzo.dPrezzo
            'Questo serve se il cambio del documento è diverso da quello dell'ordine
            dPrezzo = oCldGnbf.ConvImpValuta(strDittaCorrente, True, dPrezvalc, NTSCInt(oCleBoll.dttET.Rows(0)!et_valuta), NTSCDate(oCleBoll.dttET.Rows(0)!et_datdoc), NTSCDec(oCleBoll.dttET.Rows(0)!et_cambio))
          Else
            If bScorp Then
              dPreziva = oCondCommerciali.OutputPrezzo.dPrezzo
              oCldGnbf.Scorporo(dPreziva, NTSCInt(dtrMm!mm_codiva), dPrezzo, 0)
            Else
              dPrezzo = oCondCommerciali.OutputPrezzo.dPrezzo
            End If
          End If
          'Per sicurezza, se il prezzo è diventato netto, ma gli sconti non li deve ricalcolare !!!!
          'infatti non posso avere prezzo netto e sconti <> 0 !!!
          If (bRicalcScon = False) AndAlso (oCondCommerciali.OutputPrezzo.strPrzNet = "S") Then
            !ec_scont1 = 0
            !ec_scont2 = 0
            !ec_scont3 = 0
            !ec_scont4 = 0
            !ec_scont5 = 0
            !ec_scont6 = 0
          End If
          !ec_preziva = dPreziva
          !ec_prezvalc = dPrezvalc
          !ec_prezzo = dPrezzo
        End If
        If bRicalcProv = True Then
          strFlstat = NTSCStr(dtrMm!mm_flstat)
          If strFlstat.Trim = "" Then strFlstat = "N"
          'Agente1
          If NTSCInt(oCleBoll.dttET.Rows(0)!et_codagen) <> 0 Then
            dTmpvprovvun = oCondCommerciali.OutputProvvAgente1.dVprovv
            dProvv = oCondCommerciali.OutputProvvAgente1.dProvv
            If dTmpvprovvun <> 0 Then
              strFlstat = "S"
              dProvv = dTmpvprovvun
            End If
          End If
          'Agente2
          If NTSCInt(oCleBoll.dttET.Rows(0)!et_codagen2) <> 0 Then
            dTmpvprovvun2 = oCondCommerciali.OutputProvvAgente2.dVprovv
            dProvv2 = oCondCommerciali.OutputProvvAgente2.dProvv
            If strFlstat = "S" Then 'dipende sempre dal primo agente
              dProvv2 = dTmpvprovvun2
            Else
              'SE LA %/€ provvigione 1 è zero e la provvigione 2 è a valore, la imposta
              If (dProvv = 0) And (dTmpvprovvun2 <> 0) Then
                strFlstat = "S"
                dProvv2 = dTmpvprovvun2
              End If
            End If
          End If
          !ec_provv = dProvv
          !ec_provv2 = dProvv2
        End If

        '---------------------------
        'se articolo TCO, devo alimentare anche la quantità in CORPOTC
        'la riga è già stata create da beorgsor, per cui devo solo inserire le quantità
        If NTSCInt(!xxo_codtagl) <> 0 And CBool((ModuliExtDittaDitt(strDittaCorrente) And bsModExtTCO)) Then
          If Not oCldGnbf.GetMovmagtc(strDittaCorrente, NTSCStr(dtrMm!mm_tipork), NTSCStr(dtrMm!mm_anno), NTSCStr(dtrMm!mm_serie), _
                                      NTSCStr(dtrMm!mm_numdoc), NTSCStr(dtrMm!mm_riga), dttTmp2) Then
            Return False
          End If
          dtrT = oCleBoll.dttECTC.Select(" ec_tipork = " & CStrSQL(!ec_tipork) & " AND ec_anno = " & NTSCStr(!ec_anno) & _
                                         " AND ec_serie = " & CStrSQL(!ec_serie) & " AND ec_numdoc = " & NTSCStr(!ec_numdoc) & _
                                         " AND ec_riga = " & NTSCStr(!ec_riga))
          For j = 1 To 24
            dtrT(0)("ec_quant" & j.ToString.PadLeft(2, "0"c)) = NTSCDec(dttTmp2.Rows(0)("mm_quant" & j.ToString.PadLeft(2, "0"c)))
            dtrT(0)("ec_qtadis" & j.ToString.PadLeft(2, "0"c)) = NTSCDec(dttTmp2.Rows(0)("mm_qtadis" & j.ToString.PadLeft(2, "0"c)))
            dtrT(0)("ec_npqtadis" & j.ToString.PadLeft(2, "0"c)) = NTSCDec(dttTmp2.Rows(0)("mm_npqtadis" & j.ToString.PadLeft(2, "0"c)))
            dtrT(0)("ec_nprquae" & j.ToString.PadLeft(2, "0"c)) = NTSCDec(dttTmp2.Rows(0)("mm_nprquae" & j.ToString.PadLeft(2, "0"c)))
          Next
        End If    'If NTSCInt(!xxo_codtagl) <> 0 Then

      End With

      If Not ScriviRigaDocDaNotaPre_Pers(oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1), dtrMm) Then
        oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
        Return False
      End If

      If Not oCleBoll.RecordSalva(oCleBoll.dttEC.Rows.Count - 1, False, Nothing) Then
        oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
        Return False
      End If

      Return True

    Catch ex As Exception
      oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      oCleBoll.bInImportRigheOrd = False
      dttTEMP.Clear()
      dttTEMP.Dispose()
    End Try
  End Function

  Public Overridable Function ScriviRigaDocDaNotaPre_Pers(ByRef dtrNew As DataRow, ByRef dtrOld As DataRow) As Boolean
    Try
      'a disposizione per rive per caricare campi personalizzati di offerta in fase di import righe da offerta
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

  Public Overridable Function CorrezioneDatiPreSalvataggio() As Boolean
    Try
      'Se ci sono più righe con riferimento allo stesso impegno e con flag saldato, allora lascio il flag solo sull'ultima riga
      For Each dtrEc As DataRow In oCleBoll.dttEC.Rows
        If NTSCStr(dtrEc!ec_salcon) = "N" Then Continue For 'La nota di prelievo non è agganciata all'impegno cliente o non è evasa a saldo

        If oCleBoll.dttEC.Select("ec_ortipo = " & CStrSQL(dtrEc!ec_ortipo) & " AND ec_oranno = " & NTSCInt(dtrEc!ec_oranno) & _
                            " AND ec_orserie = " & CStrSQL(dtrEc!ec_orserie) & " AND ec_ornum = " & NTSCInt(dtrEc!ec_ornum) & _
                            " AND ec_orriga =  " & NTSCInt(dtrEc!ec_orriga) & " AND ec_salcon = 'S'").Length <= 1 Then Continue For 'c'è solo quella riga con quei riferimenti all'ordine e flag saldato

        dtrEc!ec_salcon = "C"
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

  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, "GEGNBF", dsShared.Tables("GEGNBF"), "", "", "")

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

  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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

  Public Overridable Function RitornaCambio(ByVal nValuta As Integer, ByVal nSvalOpzione As Integer, ByVal strDataElab As String) As Decimal
    Dim dsTmp As New DataSet
    Dim dtrT() As DataRow = Nothing
    Dim strDesvalu As String = ""
    Dim evnt As NTSEventArgs = Nothing
    Dim bChiediCambio As Boolean = False
    Try
      oCldGnbf.ValCodiceDb(nValuta.ToString, strDittaCorrente, "TABVALU", strDesvalu)
      RitornaCambio = oCldGnbf.CercaCambioDiOggi(nValuta, strDataElab)

      '----------------------
      If RitornaCambio = 0 And nSvalOpzione = 3 Then
        oCldGnbf.LeggiTabellaSemplice(strDittaCorrente, "CAMBI", dsTmp)
        dtrT = dsTmp.Tables("CAMBI").Select("wx_codvalu = " & nValuta & " AND wx_dtvalid <= " & CDataSQL(strDataElab), "wx_dtvalid DESC")
        If dtrT.Length > 0 Then
          RitornaCambio = NTSCDec(dtrT(0)!wx_cambio)
        Else
          bChiediCambio = True
        End If
        dsTmp.Clear()
      End If

      '----------------------
      If RitornaCambio = 0 And nSvalOpzione = 2 Then
        bChiediCambio = True
      End If

      If bChiediCambio Then
Continua:
        evnt = New NTSEventArgs(CLN__STD.ThMsg.INPUTBOX, oApp.Tr(Me, 128608418924375000, ""))
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 100000000000000000, "Inserire il cambio per la valuta '|" & strDesvalu & "|':")))
        If evnt.RetValue = "" Then
          GoTo Continua
        End If
        If Not IsNumeric(evnt.RetValue) Then
          GoTo Continua
        End If
        If NTSCDec(evnt.RetValue) <= 0 Or NTSCDec(evnt.RetValue) > 999999999 Then
          GoTo Continua
        End If
        RitornaCambio = NTSCDec(evnt.RetValue)
        oCldGnbf.AggiornaCambio(nValuta, strDataElab, NTSCDec(evnt.RetValue), True)
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

#Region "FORM BNVESEBP"
  Public Overridable Function edTipoBf_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGnbf.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABTPBF", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128607750870156250, "Codice bolla/fattura |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function edCodcage_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGnbf.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABCAGE", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711257209836831, "Codice agente |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function edCodzona_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGnbf.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABZONE", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711257411394171, "Codice zona |'" & nCod.ToString & "'| inesistente")))
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

  Public Overridable Function ComponiQuerySebp(ByVal stredAnnoBolle As String, ByVal bckTutto As Boolean, _
                                               ByVal bopTuttiClienti As Boolean, ByVal stredDalcliente As String, _
                                               ByVal stredAlcliente As String, ByVal bopTutteBolle As Boolean, _
                                               ByVal stredTipobf As String, ByVal bopTuttiAgenti As Boolean, _
                                               ByVal stredCodcage As String, ByVal bopTutteZone As Boolean, _
                                               ByVal stredCodzona As String, ByVal bckVistate As Boolean, _
                                               ByVal stredNumpar As String, ByVal bopTutteDate As Boolean, _
                                               ByVal bckAnchealtre As Boolean, ByVal stredDalladata As String, _
                                               ByVal stredAlladata As String, ByVal bckConssoloasa As Boolean) As Boolean
    Try
      Return ComponiQuerySebp(stredAnnoBolle, bckTutto, bopTuttiClienti, stredDalcliente, stredAlcliente, bopTutteBolle, _
                              stredTipobf, bopTuttiAgenti, stredCodcage, bopTutteZone, stredCodzona, bckVistate, _
                              stredNumpar, bopTutteDate, bckAnchealtre, stredDalladata, stredAlladata, bckConssoloasa, _
                              0, 9999, 0, 9999)
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
  Public Overridable Function ComponiQuerySebp(ByVal stredAnnoBolle As String, ByVal bckTutto As Boolean, _
                                               ByVal bopTuttiClienti As Boolean, ByVal stredDalcliente As String, _
                                               ByVal stredAlcliente As String, ByVal bopTutteBolle As Boolean, _
                                               ByVal stredTipobf As String, ByVal bopTuttiAgenti As Boolean, _
                                               ByVal stredCodcage As String, ByVal bopTutteZone As Boolean, _
                                               ByVal stredCodzona As String, ByVal bckVistate As Boolean, _
                                               ByVal stredNumpar As String, ByVal bopTutteDate As Boolean, _
                                               ByVal bckAnchealtre As Boolean, ByVal stredDalladata As String, _
                                               ByVal stredAlladata As String, ByVal bckConssoloasa As Boolean, _
                                               ByVal lDaVett1 As Integer, ByVal lAVett1 As Integer, _
                                               ByVal lDaVett2 As Integer, ByVal lAVett2 As Integer) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {stredAnnoBolle, bckTutto, bopTuttiClienti, stredDalcliente, stredAlcliente, bopTutteBolle, _
                                             stredTipobf, bopTuttiAgenti, stredCodcage, bopTutteZone, stredCodzona, bckVistate, _
                                             stredNumpar, bopTutteDate, bckAnchealtre, stredDalladata, stredAlladata, bckConssoloasa, _
                                             lDaVett1, lAVett1, lDaVett2, lAVett2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------


      oCldGnbf.ComponiQuerySebp(stredAnnoBolle, bckTutto, bopTuttiClienti, stredDalcliente, _
                                stredAlcliente, bopTutteBolle, stredTipobf, bopTuttiAgenti, _
                                stredCodcage, bopTutteZone, stredCodzona, bckVistate, _
                                stredNumpar, bopTutteDate, bckAnchealtre, stredDalladata, _
                                stredAlladata, bckConssoloasa, bSebpConssoloasa, strSebpQuery, lDaVett1, lAVett1, lDaVett2, lAVett2)

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

  Public Overridable Function CheckSelection(ByVal stredDalcliente As String, ByVal stredAlcliente As String, _
                                             ByVal stredDalladata As String, ByVal stredAlladata As String) As Boolean
    Dim lGiorni As Integer
    Try
      If NTSCInt(stredDalcliente) > NTSCInt(stredAlcliente) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711317823753041, "Il codice cliente iniziale non può essere superiore a quello finale")))
        Return False
      End If

      lGiorni = NTSCInt(DateDiff("d", NTSCDate(stredDalladata), NTSCDate(stredAlladata)))
      If lGiorni <> 0 Then
        If lGiorni < 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711317512657300, "La data impegno di iniziale non può essere superiore a quella finale")))
          Return False
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
#End Region

#Region "FORM BNVEDTAC"
  Public Overridable Function edVettor_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGnbf.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABVETT", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711505311389814, "Codice vettore |'" & nCod.ToString & "'| inesistente")))
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

  Public Overridable Function DtacCheckSelection(ByVal stredAnnofat As String, ByVal stredDatfat As String) As Boolean
    Try
      If NTSCInt(stredAnnofat) <> Year(NTSCDate(stredDatfat)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711506082478629, "La data attribuita ai nuovi documenti non è inclusa nell'anno indicato.")))
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
#End Region

End Class
