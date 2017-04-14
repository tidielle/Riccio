Imports System.Data
Imports NTSInformatica.CLN__STD


'PER UN ESEMPIO DI CLASSE EREDITATA CON DAL SPECIFICO VEDI BE__SOTC, BECGPRIV (con cambio ditta), BEVECOVP

Public Class CLEORGNOR
  Inherits CLE__BASN

  Public oCldGnor As CLDORGNOR
  Public oCleGsor As CLEORGSOR
  Public bModRA As Boolean = False
  Public lGiafat As Integer = 0               'numero di documenti generati
  Public nDefaultCodPaga As Integer = 0

  'filtri passati da frmSeol
  Public strOrdlistTipork As String = ""      'tipork da generare
  Public strWhereArtico As String = ""        'where articoli la selezione delle proposte da elaborare
  Public bOpInterni As Boolean = False        'se TRUE devo elaborare solo gli Ordini di prod. interni
  Public lConto As Integer = 0
  Public lCommeca As Integer = 0
  Public nMagaz As Integer = 0
  Public strDatconsini As String = ""
  Public strDatconsfin As String = ""
  Public strDatordini As String = ""
  Public strDatordfin As String = ""
  Public bGenerato As Boolean = False
  Public bConfermato As Boolean = False
  Public bEmRDA As Boolean = False
  Public bAppRDA As Boolean = False
  Public bEmRDO As Boolean = False
  Public bCongelato As Boolean = False
  Public bEmOrdine As Boolean = False

  Public strOrdlistProgressiviDaElab As String = ""    'elenco di ol_progr da elaborare, passati dal chiamante bnorgsol

  Public dtDatCons As Date = New Date(1900, 1, 1)

  'estremi nuovo documento (da frmSeos)
  Public nAnnoNewDoc As Integer = 0
  Public strSerieNewDoc As String = " "
  Public lNumNewDoc As Integer = 0
  Public nTipoBfNewDoc As Integer = 0
  Public nAnnoTcNewDoc As Integer = 0
  Public nStagioneNewDoc As Integer = 0
  Public strDataNewDoc As String = ""
  Public nSvalOpzione As Integer = 3            'di ritorno da FRM__SVAL 3 = usa cambio alla data di elabor e, in mancanza, il cambio più vicino

  'opzioni di registro
  Public bRaggrRigheComm As Boolean            'Per il raggruppamento di righe di commessa diversa
  Public strRaggruppareH As String             ' S=sempre (default), M=mai, I=solo interni, E=solo esterni)
  Public bOrdinaperprogrRDO As Boolean         'In presenza di modulo RDA, genererà gli ordini in ordine di progressivo, articolo, data consegna; altrimenti (default) ordina per articolo, data di consegna
  Public fRicalcPrez As Integer
  Public fRicalcScon As Integer
  Public nCodStabilimento As Integer = 0

  Public dttTTOLTOMO As New DataTable

  Public strDatini As String = ""
  Public strDatfin As String = ""

  Public bPrivilegiaTipoBF_CF As Boolean = False

  Private Moduli_P As Integer = bsModOR
  Private ModuliExt_P As Integer = bsModExtORE
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDORGNOR"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldGnor = CType(MyBase.ocldBase, CLDORGNOR)
    oCldGnor.Init(oApp)
    Return True
  End Function

  Public Overridable Function InitExt() As Boolean
    Try
      If Not oCldGnor.ValCodiceDb("1", strDittaCorrente, "TABPEVE", "N", "") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101093750, "Tabella 'Personalizzazione Vendite' non compilata. Impostarla prima di proseguire")))
        Return False
      End If

      If Not oCldGnor.ValCodiceDb("1", strDittaCorrente, "TABPEAC", "N", "") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101250000, "Tabella 'Personalizzazione Acquisti' non compilata. Impostarla prima di proseguire")))
        Return False
      End If

      '------------------------
      'inizializzo BEORGSOR
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEORGNOR", "BEORGSOR", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleGsor = CType(oTmp, CLEORGSOR)
      '------------------------------------------------
      AddHandler oCleGsor.RemoteEvent, AddressOf GestisciEventiEntityGsor
      If oCleGsor.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return False
      If Not oCleGsor.InitExt() Then Return False
      oCleGsor.bModuloCRM = False
      oCleGsor.bIsCRMUser = False

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

  Public Overridable Sub GestisciEventiEntityGsor(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      'gli eventuali messaggi dati da BEORGSOR tramite la ThrowRemoteEvent li passo a BNORGNOR
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


  Public Overridable Function CheckPropValuta(ByVal strDitta As String, ByVal strTipork As String) As Boolean
    Try

      Return oCldGnor.CheckPropValuta(strDitta, strTipork)

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

  Public Overridable Function LegNuma(ByVal strTipo As String, ByVal strSerie As String, ByVal nAnno As Integer) As Integer
    Try
      Return oCldGnor.LegNuma(strDittaCorrente, strTipo, strSerie, nAnno, False)
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

  Public Overridable Function RitornaCambio(ByVal nValuta As Integer, ByVal nSvalOpzione As Integer, ByVal strDataElab As String) As Decimal
    Dim dsTmp As New DataSet
    Dim dtrT() As DataRow = Nothing
    Dim strDesvalu As String = ""
    Dim evnt As NTSEventArgs = Nothing
    Dim bChiediCambio As Boolean = False
    Try
      oCldGnor.ValCodiceDb(nValuta.ToString, strDittaCorrente, "TABVALU", strDesvalu)
      RitornaCambio = oCldGnor.CercaCambioDiOggi(nValuta, strDataElab)

      '----------------------
      If RitornaCambio = 0 And nSvalOpzione = 3 Then
        oCldGnor.LeggiTabellaSemplice(strDittaCorrente, "CAMBI", dsTmp)
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
        evnt = New NTSEventArgs(CLN__STD.ThMsg.INPUTBOX, oApp.Tr(Me, 100000000000000000, "Inserire il cambio per la valuta '|" & strDesvalu & "|':"))
        ThrowRemoteEvent(evnt)
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
        oCldGnor.AggiornaCambio(nValuta, strDataElab, NTSCDec(evnt.RetValue), True)
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

  Public Overridable Function Elabora(ByRef dsGnor As DataSet) As Boolean
    Dim bRaggruppa As Boolean = True
    Dim dttTmp As New DataTable
    Dim bPOPInterna As Boolean = True
    Dim nMagazTmp As Integer = 0
    Dim strDesogglog As String = ""
    Dim dttOl As New DataTable              'datatable delle righe di ordlist da generare
    Try
      lGiafat = 0
      dsGnor.Tables.Clear()

      dsGnor.Tables.Add(New DataTable("GNOR"))
      dsGnor.Tables("GNOR").Columns.Add("xx_seleziona", GetType(String))
      dsGnor.Tables("GNOR").Columns.Add("td_anno", GetType(Integer))
      dsGnor.Tables("GNOR").Columns.Add("td_numord", GetType(String))
      dsGnor.Tables("GNOR").Columns.Add("td_serie", GetType(String))
      dsGnor.Tables("GNOR").Columns.Add("td_datord", GetType(DateTime))
      dsGnor.Tables("GNOR").Columns.Add("td_totdoc", GetType(Decimal))
      dsGnor.Tables("GNOR").Columns.Add("td_conto", GetType(Integer))
      dsGnor.Tables("GNOR").Columns.Add("xx_conto", GetType(String))
      dsGnor.Tables("GNOR").Columns.Add("td_valuta", GetType(Integer))
      dsGnor.Tables("GNOR").Columns.Add("td_scorpo", GetType(String))
      dsGnor.Tables("GNOR").AcceptChanges()


      '----------------------------
      dttTTOLTOMO.Columns.Clear()
      dttTTOLTOMO.Clear()
      dttTTOLTOMO.Columns.Add("ol_progr", GetType(Integer))
      dttTTOLTOMO.Columns.Add("ol_ortipork", GetType(String))
      dttTTOLTOMO.Columns.Add("ol_oranno", GetType(Integer))
      dttTTOLTOMO.Columns.Add("ol_orserie", GetType(String))
      dttTTOLTOMO.Columns.Add("ol_ornum", GetType(Integer))
      dttTTOLTOMO.Columns.Add("ol_orriga", GetType(Integer))
      dttTTOLTOMO.AcceptChanges()

      '-------------------------
      'Controlla: se il mag. impegni è un magazzino nostro li cons. ordini di lav. interni
      'Per default raggruppa
      If strOrdlistTipork = "H" Then
        If strRaggruppareH = "M" Then ' mai
          bRaggruppa = False
          GoTo Continua1
        End If
      End If

      oCldGnor.ValCodiceDb(nTipoBfNewDoc.ToString, strDittaCorrente, "TABTPBF", "N", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128607846006093750, "Tipo bolla/fattura |" & nTipoBfNewDoc.ToString & "| inesistente")))
        Return False
      End If
      nMagazTmp = NTSCInt(dttTmp.Rows(0)!tb_tmagimp)
      dttTmp.Clear()
      If nMagazTmp = 0 Then GoTo Continua1

      '--------------------------
      'Legge mag. per vedere se interno
      oCldGnor.ValCodiceDb(nMagazTmp.ToString, strDittaCorrente, "TABMAGA", "N", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128607846835000000, "Magazzino |" & nMagazTmp.ToString & "| inesistente")))
        Return False
      End If
      If strOrdlistTipork = "H" Then
        If strRaggruppareH = "I" And (dttTmp.Rows(0)!tb_flclavo.ToString = "F" Or dttTmp.Rows(0)!tb_flclavo.ToString = "X") Then bRaggruppa = False
        If strRaggruppareH = "E" And Not (dttTmp.Rows(0)!tb_flclavo.ToString = "F" Or dttTmp.Rows(0)!tb_flclavo.ToString = "X") Then bRaggruppa = False
      End If
Continua1:
      dttTmp.Clear()

      strDesogglog = "Generazione Ordini da Proposte d'Ordine" & vbCrLf & vbCrLf
      '--------------------------
      'Chiama la routine che looppa chiamando generadoc
      If Not oCldGnor.GetData(strDittaCorrente, strOrdlistTipork, strWhereArtico, bModRA, _
                               strOrdlistProgressiviDaElab, lConto, lCommeca, nMagaz, _
                               strDatordini, strDatordfin, strDatconsini, strDatconsfin, _
                               bGenerato, bConfermato, bCongelato, bEmRDA, bAppRDA, bEmRDO, _
                               bRaggruppa, bRaggrRigheComm, nCodStabilimento, dttOl, nSvalOpzione) Then Return False
      If dttOl.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128608450007812500, "Non sono stati generati ordini con le caratteristiche richieste")))
        Return True
      End If
      For Each dtrOl As DataRow In dttOl.Rows

        ' --------
        oCldGnor.ValCodiceDb(NTSCStr(dtrOl!ol_magimp), strDittaCorrente, "TABMAGA", "N", "", dttTmp)
        If dttTmp.Rows.Count = 0 Then
          bPOPInterna = True
        Else
          If NTSCStr(dttTmp.Rows(0)!tb_flclavo) = " " Or NTSCStr(dttTmp.Rows(0)!tb_flclavo) = "C" Then
            bPOPInterna = True
          Else
            bPOPInterna = False
          End If
        End If
        '----------
        'creo l'ordine
        If CBool((ModuliDittaDitt(strDittaCorrente) And bsModRA)) And (bOpInterni) Then
          ' Se si ha il modulo RDA/RDO attivo e si è scelto di elaborare solo O.P. Interni non genero gli esterni
          If bPOPInterna Then
            GeneraDoc(strDesogglog, bRaggruppa, dtrOl, dsGnor)
          End If
        Else
          GeneraDoc(strDesogglog, bRaggruppa, dtrOl, dsGnor)
        End If
        'GeneraDoc(strDesogglog, bRaggruppa, dtrOl, dsGnor)
      Next    'For Each dtrOl As DataRow In dttOl.Rows

      '--------------------------
      'ora aggiorno i priferimenti attivit.at_su.... sui documenti generati.
      'quelle colonne non servono a nulla, ma in vb6 lo faceva ed un rive le usava ...
      If dttTTOLTOMO.Rows.Count > 0 Then
        oCldGnor.AggAttivitColonneSu(strDittaCorrente, dttTTOLTOMO)
      End If

      '--------------------------
      If dttOl.Rows.Count > 0 Then
        strDesogglog = strDesogglog & vbCrLf & _
          " - Selezionando i dati seguenti...............: "
        Select Case strOrdlistTipork
          Case "O" : strDesogglog = strDesogglog & "'Ordini a fornitori'" & vbCrLf
          Case "H" : strDesogglog = strDesogglog & "'Ordini di produzione' " & IIf(bOpInterni = True, "(CON ordini interni di produzione)", "(SENZA ordini interni di produzione)").ToString & vbCrLf
          Case "X" : strDesogglog = strDesogglog & "'Impegni di trasferimento'" & vbCrLf
        End Select
        strDesogglog = strDesogglog & _
          " - Codice fornitore...........................: '" & lConto.ToString & "'" & vbCrLf & _
          " - Commessa...................................: '" & lCommeca.ToString & "'" & vbCrLf & _
          " - Magazzino..................................: '" & nMagaz.ToString & "'" & vbCrLf & _
          " - Dalla data di consegna.....................: '" & strDatconsini & "'" & vbCrLf & _
          " - Alla data di consegna......................: '" & strDatconsfin & "'" & vbCrLf & _
          " - Dalla data massimo ordine..................: '" & strDatordini & "'" & vbCrLf & _
          " - Alla data massimo ordine...................: '" & strDatordfin & "'" & vbCrLf & _
          " - Stato riga.................................: " & vbCrLf & _
          "                                   . Generato................: " & IIf(bGenerato = True, "'Sì'", "'No'").ToString & vbCrLf & _
          "                                   . Emissione RDA...........: " & IIf(bEmRDA = True, "'Sì'", "'No'").ToString & vbCrLf & _
          "                                   . Approvazione RDA........: " & IIf(bAppRDA = True, "'Sì'", "'No'").ToString & vbCrLf & _
          "                                   . Emissione RDO...........: " & IIf(bEmRDO = True, "'Sì'", "'No'").ToString & vbCrLf & _
          "                                   . Confermato/approvato RDO: " & IIf(bConfermato = True, "'Sì'", "'No'").ToString & vbCrLf & _
          "                                   . Congelato...............: " & IIf(bCongelato = True, "'Sì'", "'No'").ToString & vbCrLf & _
          " - Opzioni....................................: "
        Select Case nSvalOpzione
          Case 1
            strDesogglog = strDesogglog & "'Considerando il cambio della poposta d'ordine'" & vbCrLf
          Case 2, 3
            strDesogglog = strDesogglog & "'Considerando il cambio alla data di elaborazione'" & vbCrLf & _
              "                                   . In deroga considerato il cambio più vicino a tale data: " & IIf(nSvalOpzione = 3, "'Sì'", "'No'").ToString & vbCrLf
        End Select
        oCldGnor.ScriviActLog(strDittaCorrente, "BSORGNOR", "BSORGNOR", "", "", "M", "E", strDesogglog, False)
      End If    'If bDocGenerati = True Then

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
      dttTTOLTOMO.Columns.Clear()
      dttTTOLTOMO.Clear()
    End Try
  End Function

  Public Overridable Function GeneraDoc(ByRef strDesogglog As String, ByVal bRaggruppa As Boolean, ByRef dtrOl As DataRow, ByRef dsGnor As DataSet) As Boolean
    Dim ds As New DataSet
    Dim dttTmp As New DataTable
    Dim dttOrdl As New DataTable
    Dim dttOrdlY As New DataTable
    Dim dttAtt As New DataTable
    Dim dttAss As New DataTable
    Dim dttArti As New DataTable
    Dim lNumTmp As Integer = 0
    Dim lProgr As Integer = 0
    Dim lConto As Integer = 0
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dtrRow() As DataRow = Nothing
    Dim dtrNewRow As DataRow = Nothing
    Dim dttRefer As New DataTable       'contiene i collegamenti tra ordlist ed il nuovo ordine generato, per poter aggiornare poi ordlist in BDORGSOR
    Dim nCodpagaStd As Integer = 0
    Dim dPrezzo As Decimal
    Try
      'oCldGnor.ValCodiceDb("1", strDittaCorrente, "TABPAGA", "N", "", dttTmp)
      'If dttTmp.Rows.Count > 0 Then nCodpagaStd = 1
      'dttTmp.Clear()

      nCodpagaStd = nDefaultCodPaga

      '----------------------------
      'sul forn deve esserci il cod. pagamento
      oCldGnor.ValCodiceDb(NTSCInt(dtrOl!ol_conto).ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
      If NTSCInt(dttTmp.Rows(0)!an_codpag) = 0 And nCodpagaStd = 0 Then
        dttTmp.Clear()
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128608626815781250, "Manca codice pagamento nell'anagrafica del cliente/fornitore |" & NTSCInt(dtrOl!ol_conto).ToString & "|")))
        Return False
      End If
      dttTmp.Clear()

      '----------------------------
      'ottengo gli estremi del documento da generare: cerco fino a quando non trovo un numero libero
      If lNumNewDoc = 0 Then lNumNewDoc = 1
      Do While oCldGnor.EsisteOrdine(strDittaCorrente, strOrdlistTipork, nAnnoNewDoc, strSerieNewDoc, lNumNewDoc)
        lNumNewDoc += 1
      Loop

      '----------------------------
      'Ed aggiona il progressivo anche degli impegni di produzione (Y) stesso anno/serie/numero
      If strOrdlistTipork = "H" Then
        If oCldGnor.EsisteOrdine(strDittaCorrente, "Y", nAnnoNewDoc, strSerieNewDoc, lNumNewDoc) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128608586495468750, "Problema nelle numerazioni degli impegni di produzione rispetto a numerazione ordini di produzione: operazione annullata.")))
          lNumNewDoc -= 1
          Return False
        End If
      End If

      '----------------------------
      'ottengo l'elenco delle rige di ordlist/movrdo da elaborare
      If Not oCldGnor.GetOrdlistPerDoc(strDittaCorrente, strOrdlistTipork, strWhereArtico, bModRA, _
                                       strOrdlistProgressiviDaElab, lConto, lCommeca, nMagaz, _
                                       bRaggruppa, strDatordini, strDatordfin, strDatconsini, _
                                       strDatconsfin, bGenerato, bConfermato, bCongelato, bEmRDA, _
                                       bAppRDA, bEmRDO, bRaggrRigheComm, dtrOl, bOrdinaperprogrRDO, dttOrdl, _
                                       nSvalOpzione) Then
        lNumNewDoc -= 1
        Return False
      End If

      '----------------------------
      'preparo l'ambiente
      If Not oCleGsor.ApriOrdine(strDittaCorrente, False, strOrdlistTipork, nAnnoNewDoc, strSerieNewDoc, lNumNewDoc, ds) Then Return False
      oCleGsor.bInApriDocSilent = True
      If oCleGsor.dsShared.Tables("TESTA").Rows.Count > 0 Then
        'documento già esistente!!!!!!!!
        lNumNewDoc -= 1
        Return False
      End If
      oCleGsor.ResetVar()

      oCleGsor.bNew = True
      oCleGsor.bHasChangesET = True
      oCleGsor.bInCreaDocDaGnor = True
      oCleGsor.bInDuplicadoc = True     'tolgo un po' di messaggi tipo 'confermi riga con qta = 0, con prezzo = 0, non faccio esplodere righe kit, oppure gestione articoli accessori/succedanei, ...
      oCleGsor.bSaltaAfterInsert = True 'non fa esplodere la diba e le righe kit 

      dttRefer.Columns.Add("ol_progr", GetType(Integer))
      dttRefer.Columns.Add("ol_stato", GetType(String))
      dttRefer.Columns.Add("ol_ortipork", GetType(String))
      dttRefer.Columns.Add("ol_oranno", GetType(Integer))
      dttRefer.Columns.Add("ol_orserie", GetType(String))
      dttRefer.Columns.Add("ol_ornum", GetType(Integer))
      dttRefer.Columns.Add("ol_orriga", GetType(Integer))

      '---------------------------
      'verifico se devo aggiornare tabnuma al salvataggio
      oCleGsor.bProgrCambiato = False
      lNumTmp = LegNuma(IIf(strOrdlistTipork = "V", "VV", strOrdlistTipork).ToString, strSerieNewDoc, nAnnoNewDoc)
      If lNumTmp <> lNumNewDoc Then oCleGsor.bProgrCambiato = True

      '---------------------------
      'pulisco i datatable
      oCleGsor.dttET.Rows.Clear()
      oCleGsor.dttEC.Rows.Clear()
      oCleGsor.dttECIMP.Rows.Clear()
      If Not oCleGsor.dttECTC Is Nothing Then
        oCleGsor.dttECTC.Rows.Clear()
        oCleGsor.dttECIMPTC.Rows.Clear()
      End If
      oCleGsor.dttATTIVIT.Rows.Clear()
      oCleGsor.dttASSRIS.Rows.Clear()

      'verifica la data consegla
      'If strOrdlistTipork = "O" Then
      dtrRow = dttOrdl.Select("MIN(ol_datcons) = ol_datcons")

      If dtrRow.Length <> 0 Then dtDatCons = NTSCDate(dtrRow(0)!ol_datcons)
      'End If

      '---------------------------
      'creo testa del documento
      oCleGsor.dttET.Rows.Add(oCleGsor.dttET.NewRow())

      If Not GeneraDoc_Testa(oCleGsor.dttET.Rows(0), dtrOl, bRaggruppa, nCodpagaStd) Then
        lNumNewDoc -= 1
        Return False
      End If

      If Not oCleGsor.OkTestata Then Return False

      '---------------------------
      'creo il corpo del documento
      For Each dtrMo As DataRow In dttOrdl.Rows
        If bModRA Then
          lProgr = NTSCInt(dtrMo!rdo_progr)
          lConto = NTSCInt(dtrMo!rdo_conto)
        Else
          lProgr = NTSCInt(dtrMo!ol_progr)
          lConto = NTSCInt(dtrMo!ol_conto)
        End If
        'Verifico che l'articolo non sia bloccato, altrimenti passa alla riga successiva.
        oCldGnor.ValCodiceDb(NTSCStr(dtrMo!ol_codart), strDittaCorrente, "ARTICO", "S", , dttArti)
        If NTSCStr(dttArti.Rows(0)!ar_blocco) = "S" Then
          LogWrite(oApp.Tr(Me, 129527095357530696, "L'articolo '|" & NTSCStr(dtrMo!ol_codart) & "|' risulta bloccato e sarà ignorato"), True)
          Continue For
        End If

        oCleGsor.dttEC.Rows.Add(oCleGsor.dttEC.NewRow())
        With oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1)
          If Not GeneraDoc_Corpo(oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1), dtrMo) Then
            lNumNewDoc -= 1
            Return False
          End If

          '--------------------------
          'memorizzo gli estremi del documento generato collegato al record di ordlist, 
          'visto che dopo dovrò aggiornare i campi di attivit at_su... con gli estremi del documento successivo 
          '(non servono a niente, ma vb6 lo faceva ...)
          dttTTOLTOMO.Rows.Add(dttTTOLTOMO.NewRow)
          dttTTOLTOMO.Rows(dttTTOLTOMO.Rows.Count - 1)!ol_progr = NTSCInt(dtrMo!ol_progr)
          dttTTOLTOMO.Rows(dttTTOLTOMO.Rows.Count - 1)!ol_ortipork = strOrdlistTipork
          dttTTOLTOMO.Rows(dttTTOLTOMO.Rows.Count - 1)!ol_oranno = nAnnoNewDoc
          dttTTOLTOMO.Rows(dttTTOLTOMO.Rows.Count - 1)!ol_orserie = strSerieNewDoc
          dttTTOLTOMO.Rows(dttTTOLTOMO.Rows.Count - 1)!ol_ornum = lNumNewDoc
          dttTTOLTOMO.Rows(dttTTOLTOMO.Rows.Count - 1)!ol_orriga = NTSCInt(!ec_riga)

          '---------------------------
          'memorizzo gli stremi dell'ordine, così in fase di aggiornam ordine in ordlist vengono memorizzati i riferimenti
          dttRefer.Rows.Add(dttRefer.NewRow)
          dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_progr = dtrMo!ol_progr
          dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_stato = "T"
          dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_ortipork = strOrdlistTipork
          dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_oranno = nAnnoNewDoc
          dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_orserie = strSerieNewDoc
          dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_ornum = lNumNewDoc
          dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_orriga = NTSCInt(!ec_riga)

          '---------------------------
          'se ordine di prod creo impegni/attivit/assiris
          dPrezzo = NTSCDec(!ec_prezzo)
          If strOrdlistTipork = "H" Then
            If Not oCldGnor.GetOrdlistY(strDittaCorrente, NTSCInt(dtrMo!ol_progr), dttOrdlY, dttAtt, dttAss) Then
              lNumNewDoc -= 1
              Return False
            End If

            '-----------------------
            'scrivo le righe degli impegni
            For Each dtrImp As DataRow In dttOrdlY.Rows
              oCleGsor.dtrHT = oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1)
              oCleGsor.dttECIMP.Rows.Add(oCleGsor.dttECIMP.NewRow())
              dtrNewRow = oCleGsor.dttECIMP.Rows(oCleGsor.dttECIMP.Rows.Count - 1)

              If Not GeneraDoc_CorpoY(dtrNewRow, dtrImp) Then
                lNumNewDoc -= 1
                Return False
              End If

              If Not oCleGsor.CorpoImpRecordSalva(oCleGsor.dttECIMP.Rows.Count - 1, False, Nothing) Then
                lNumNewDoc -= 1
                Return False
              End If

              '---------------------------
              'memorizzo gli stremi dell'ordine, così in fase di aggiornam ordine in ordlist vengono memorizzati i riferimenti
              dttRefer.Rows.Add(dttRefer.NewRow)
              dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_progr = NTSCInt(dtrImp!ol_progr)
              dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_stato = "T"
              dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_ortipork = "Y"
              dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_oranno = nAnnoNewDoc
              dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_orserie = strSerieNewDoc
              dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_ornum = lNumNewDoc
              dttRefer.Rows(dttRefer.Rows.Count - 1)!ol_orriga = NTSCInt(dtrNewRow!ec_riga)
            Next    'For Each dtrImp As DataRow In dttOrdlY.Rows

            '-----------------------
            'scrivo le righe di attivit
            For Each dtrAtt As DataRow In dttAtt.Rows
              oCleGsor.dtrHT = oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1)
              oCleGsor.dttATTIVIT.Rows.Add(oCleGsor.dttATTIVIT.NewRow())
              dtrNewRow = oCleGsor.dttATTIVIT.Rows(oCleGsor.dttATTIVIT.Rows.Count - 1)

              If Not GeneraDoc_Attivit(dtrNewRow, dtrAtt) Then
                lNumNewDoc -= 1
                Return False
              End If

              If Not oCleGsor.AttivitRecordSalva(oCleGsor.dttATTIVIT.Rows.Count - 1, False, Nothing) Then
                lNumNewDoc -= 1
                Return False
              End If

              '--------------------------------------
              'In ZZATTIVIT setto nuovi valori per quelli rimasti che debbono puntare
              'a dei record di ATTIVIT
              dtrAtt!at_sunumord = lNumNewDoc
              dtrAtt!at_suriga = NTSCInt(!ec_riga)
              dtrAtt!at_sutipork = "H"
              dtrAtt!at_suserie = strSerieNewDoc
              dtrAtt!at_suanno = nAnnoNewDoc
              dtrAtt!at_suolprogr = 0
              dtrAtt.AcceptChanges()

              If NTSCInt(dtrNewRow!at_suolprogr) <> 0 Then
                dtrNewRow!at_sunumord = lNumNewDoc
                dtrNewRow!at_suriga = NTSCInt(!ec_riga)
                dtrNewRow!at_sutipork = "H"
                dtrNewRow!at_suserie = strSerieNewDoc
                dtrNewRow!at_suanno = nAnnoNewDoc
              End If
            Next    'For Each dtrAtt As DataRow In dttAtt.Rows

            '-----------------------
            'scrivo le righe di assris
            For Each dtrAss As DataRow In dttAss.Rows
              oCleGsor.dtrHT = oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1)
              oCleGsor.dttASSRIS.Rows.Add(oCleGsor.dttASSRIS.NewRow())
              dtrNewRow = oCleGsor.dttASSRIS.Rows(oCleGsor.dttASSRIS.Rows.Count - 1)

              If Not GeneraDoc_Assris(dtrNewRow, dtrAss) Then
                lNumNewDoc -= 1
                Return False
              End If

              oCleGsor.dttASSRIS.AcceptChanges()
            Next    'For Each dtrAss As DataRow In dttAss.Rows
          End If    'If strOrdlistTipork = "H" Then
          If fRicalcPrez = 0 Then !ec_prezzo = dPrezzo
        End With    'With oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1)
        dttRefer.AcceptChanges()


        If strOrdlistTipork = "H" Then
          'ricalcolo i valori di riga degli impegni collegati e delle lavorazioni
          oCleGsor.ValorizzaProduzione(oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1))
        End If
        'ricalcolo il valore delle righe
        oCleGsor.SettaValoriRiga(oCleGsor.dttEC.Rows(oCleGsor.dttEC.Rows.Count - 1))
        oCleGsor.RecordSalva(oCleGsor.dttEC.Rows.Count - 1, False, Nothing)
      Next    'for each dtrC as DataRow in dttOrdl.Rows

      '---------------------------
      'aggiungo le informazioni che serviranno a bdorgsor per aggiornare ordlist/movrdo
      dttRefer.TableName = "ORDLISTTMP"
      If oCleGsor.dsShared.Tables.Contains("ORDLISTTMP") Then oCleGsor.dsShared.Tables.Remove("ORDLISTTMP")
      oCleGsor.dsShared.Tables.Add(dttRefer)
      oCleGsor.dsShared.Tables("ORDLISTTMP").AcceptChanges()

      '---------------------------
      'salvo il documento
      If Not oCleGsor.SalvaOrdine("N") Then
        lNumNewDoc -= 1
        Return False
      End If

      '----------------------------
      Select Case strOrdlistTipork
        Case "H" : strDesogglog = strDesogglog & " - 'Ordine di produzione'"
        Case "O" : strDesogglog = strDesogglog & " - 'Ordine fornitore'"
        Case "X" : strDesogglog = strDesogglog & " - 'Impegno di trasferimento'"
      End Select
      strDesogglog = strDesogglog & " n° " & lNumNewDoc.ToString & IIf(Trim(strSerieNewDoc) <> "", "/" & strSerieNewDoc, "").ToString & " del " & nAnnoNewDoc.ToString & vbCrLf

      '--------------------------
      'predispongo il numeratore per il prossimo documento
      lNumNewDoc += 1

      '--------------------------
      'numero dei documenti generati e griglia per la stampa
      lGiafat += 1
      dsGnor.Tables("GNOR").Rows.Add(dsGnor.Tables("GNOR").NewRow)
      With dsGnor.Tables("GNOR").Rows(dsGnor.Tables("GNOR").Rows.Count - 1)
        !xx_seleziona = "S"
        !td_anno = oCleGsor.dttET.Rows(0)!et_anno
        !td_numord = oCleGsor.dttET.Rows(0)!et_numdoc
        !td_serie = oCleGsor.dttET.Rows(0)!et_serie
        !td_datord = oCleGsor.dttET.Rows(0)!et_datdoc
        !td_totdoc = oCleGsor.dttET.Rows(0)!et_totdoc
        !td_conto = oCleGsor.dttET.Rows(0)!et_conto
        !xx_conto = NTSCStr(oCleGsor.dttET.Rows(0)!xx_conto).Substring(0, NTSCStr(oCleGsor.dttET.Rows(0)!xx_conto).IndexOf(vbCrLf)).Trim
        !td_valuta = oCleGsor.dttET.Rows(0)!et_valuta
        !td_scorpo = oCleGsor.dttET.Rows(0)!et_scorpo
      End With
      dsGnor.Tables("GNOR").AcceptChanges()

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
      dttOrdl.Clear()
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GeneraDoc_Testa(ByRef dtrT As DataRow, ByRef dtrOl As DataRow, ByVal bRaggruppa As Boolean, ByVal nCodpagaStd As Integer) As Boolean
    Dim nTipoBF As Integer = nTipoBfNewDoc
    Dim dCambio As Decimal = 0
    Dim strAcuradi As String = " "
    Dim dttTmp As New DataTable

    Try
      LogWrite(oApp.Tr(Me, 129151181192578125, "Creazione documento numero |" & lNumNewDoc & "| intestato a |" & dtrOl!ol_conto.ToString & "|"), False)
      '--------------------------------------------------------------------------------------------------------------
      If oCldGnor.ValCodiceDb(dtrOl!ol_conto.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp) = True Then
        Select Case NTSCInt(dttTmp.Rows(0)!an_vett)
          Case 0 : strAcuradi = " "
          Case Else : strAcuradi = "V"
        End Select
        If (bPrivilegiaTipoBF_CF = True) And _
           (NTSCInt(dttTmp.Rows(0)!an_codtpbf) > 0) Then nTipoBF = NTSCInt(dttTmp.Rows(0)!an_codtpbf)
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      With dtrT
        'faccio scatenare la onaddnew della testata dell'ordine
        !codditt = "."
        !codditt = strDittaCorrente
        !et_tipork = strOrdlistTipork
        !et_anno = nAnnoNewDoc
        !et_serie = strSerieNewDoc
        !et_numdoc = lNumNewDoc
        !et_datdoc = strDataNewDoc
        If dtDatCons <> New Date(1900, 1, 1) Then
          !et_datcons = dtDatCons
        Else
          !et_datcons = strDataNewDoc
        End If
        !et_conto = dtrOl!ol_conto
        !et_tipobf = nTipoBF
        !et_acuradi = strAcuradi
        !et_magaz = dtrOl!ol_magaz
        !et_magaz2 = dtrOl!ol_magaz2
        !et_magimp = dtrOl!ol_magimp
        !et_valuta = dtrOl!ol_codvalu
        '----------------------------
        'Operazione sul cambio
        If NTSCInt(dtrOl!ol_codvalu) <> 0 Then
          dCambio = 1
          If nSvalOpzione = 1 Then
            'usa il cambio di ordlist
            dCambio = NTSCDec(dtrOl!ol_cambio)
          Else
            dCambio = RitornaCambio(NTSCInt(dtrOl!ol_codvalu), nSvalOpzione, strDataNewDoc)
          End If
        Else
          dCambio = 0
        End If
        !et_cambio = dCambio
        !et_scorpo = "N"
        If bRaggrRigheComm = False Or bRaggruppa = False Then
          !et_commeca = dtrOl!ol_commeca
          !et_subcommeca = dtrOl!ol_subcommeca
        End If
        !et_annotco = nAnnoTcNewDoc
        !et_codstag = nStagioneNewDoc
        If NTSCInt(!et_codpaga) = 0 Then !et_codpaga = nCodpagaStd
      End With    'With oCleGsor.dttET.Rows(0)
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
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function GeneraDoc_Corpo(ByRef dtrEC As DataRow, ByRef dtrMo As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim dttTmp As New DataTable

    Try
      LogWrite(oApp.Tr(Me, 129151182011767578, "Aggiunta dell'articolo |" & dtrMo!ol_codart.ToString & "| collegato alla proposta d'ordine numero |" & dtrMo!ol_progr.ToString & "|"), False)

      With dtrEC
        'forzo la MovordOnAddNewRow
        !codditt = "."
        !codditt = strDittaCorrente
        !ec_codart = dtrMo!ol_codart.ToString
        !ec_fase = NTSCInt(dtrMo!ol_fase)
        !ec_descr = dtrMo!ol_descr
        !ec_desint = dtrMo!ol_desint
        !ec_magaz = dtrMo!ol_magaz
        !ec_magaz2 = dtrMo!ol_magaz2
        !ec_flkit = " "         'non gestisce articoli KIT, neanche in VB6!!!
        If NTSCInt(oCleGsor.dttET.Rows(0)!et_controp) = 0 Then !ec_controp = dtrMo!ol_controp
        '------------------------------------------------------------------------------------------------------------
        '--- Determina la gestione delle date dell'eventuale sottoconto legato alla contropartita di riga
        '------------------------------------------------------------------------------------------------------------
        DeterminaDateInizioFine(NTSCInt(!ec_controp), NTSCDate(dtrMo!ol_datini).ToShortDateString, _
          NTSCDate(dtrMo!ol_datfin).ToShortDateString)
        '------------------------------------------------------------------------------------------------------------
        If NTSCInt(oCleGsor.dttET.Rows(0)!et_codese) = 0 Then !ec_codiva = dtrMo!ol_codiva
        If bModRA Then
          If dtrMo!ol_note.Equals(DBNull.Value) Then
            !ec_note = dtrMo!rdo_note
          Else
            If Not dtrMo!rdo_note.Equals(DBNull.Value) Then
              !ec_note = NTSCStr(dtrMo!ol_note) & " " & NTSCStr(dtrMo!rdo_note)
            Else
              !ec_note = dtrMo!ol_note
            End If
          End If
          !ec_datcons = dtrMo!rdo_datcons
          !ec_datconsor = dtrMo!rdo_datcons
        Else
          !ec_note = dtrMo!ol_note
          !ec_datcons = dtrMo!ol_datcons
          !ec_datconsor = dtrMo!ol_datcons
        End If
        !ec_dtpianini = NTSCDate(dtrMo!ol_dtpianini)
        !ec_dtpianfin = NTSCDate(dtrMo!ol_dtpianfin)
        !ec_dtrichini = NTSCDate(dtrMo!ol_dtrichini)
        !ec_dtrichfin = NTSCDate(dtrMo!ol_dtrichfin)
        !ec_stasino = dtrMo!ol_stasino
        !ec_codcfam = NTSCStr(dtrMo!ol_codcfam)
        !ec_commeca = dtrMo!ol_commeca
        !ec_subcommeca = dtrMo!ol_subcommeca
        If NTSCInt(dtrMo!ol_codcena) > 0 Then !ec_codcena = dtrMo!ol_codcena
        !ec_codvuo = dtrMo!ol_codvuo
        !ec_lotto = dtrMo!ol_lotto
        !ec_codclie = dtrMo!ol_codclie
        !ec_datini = strDatini
        !ec_datfin = strDatfin
        '------------------
        !ec_misura1 = NTSCDec(dtrMo!ol_misura1)
        !ec_misura2 = NTSCDec(dtrMo!ol_misura2)
        !ec_misura3 = NTSCDec(dtrMo!ol_misura3)
        !ec_unmis = dtrMo!ol_unmis
        !ec_colli = dtrMo!ol_colli
        !ec_quant = dtrMo!ol_quant

        'se non dovevo ricalcolare i prezzi rimetto quello della prop d'ordine (li ha ricalcolati all'inserimento dell'articolo)
        !ec_prelist = dtrMo!ol_prelist
        If fRicalcPrez = 0 Then
          If bModRA Then
            !ec_flprznet = dtrMo!rdo_flprznet.ToString
            !ec_umprz = dtrMo!ol_umprz
            If NTSCInt(oCleGsor.dttET.Rows(0)!et_valuta) = 0 Then
              !ec_prezzo = NTSCDec(dtrMo!rdo_prezzo)
            Else
              !ec_prezvalc = NTSCDec(dtrMo!rdo_prezvalc)
            End If
            !ec_prelist = NTSCDec(dtrMo!ol_prelist)
          Else
            !ec_flprznet = dtrMo!ol_flprznet.ToString
            !ec_umprz = dtrMo!ol_umprz
            If NTSCInt(oCleGsor.dttET.Rows(0)!et_valuta) = 0 Then
              !ec_prezzo = NTSCDec(dtrMo!ol_prezzo)
            Else
              !ec_prezvalc = NTSCDec(dtrMo!ol_prezvalc)
            End If
            !ec_prelist = NTSCDec(dtrMo!ol_prelist)
          End If
        End If    'If fRicalcPrez = 0 Then

        'se non dovevo ricalcolare gli sconti rimetto quelli dell'ordine aperto (li ha ricalcolati all'inserimento dell'articolo)
        If fRicalcScon = 0 Then
          If bModRA Then
            !ec_scont1 = NTSCDec(dtrMo!rdo_scont1)
            !ec_scont2 = NTSCDec(dtrMo!rdo_scont2)
            !ec_scont3 = NTSCDec(dtrMo!rdo_scont3)
            !ec_scont4 = NTSCDec(dtrMo!rdo_scont4)
            !ec_scont5 = NTSCDec(dtrMo!rdo_scont5)
            !ec_scont6 = NTSCDec(dtrMo!rdo_scont6)
          Else
            !ec_scont1 = NTSCDec(dtrMo!ol_scont1)
            !ec_scont2 = NTSCDec(dtrMo!ol_scont2)
            !ec_scont3 = NTSCDec(dtrMo!ol_scont3)
            !ec_scont4 = NTSCDec(dtrMo!ol_scont4)
            !ec_scont5 = NTSCDec(dtrMo!ol_scont5)
            !ec_scont6 = NTSCDec(dtrMo!ol_scont6)
          End If
        End If    'If fRicalcScon = 0 Then

        !ec_pmtaskid = NTSCInt(dtrMo!ol_pmtaskid)
        !ec_netpid = NTSCStr(dtrMo!ol_netpid)
        If NTSCStr(dtrMo!ol_netpid) <> "" Then !ec_netpstatus = "REL"

        '---------------------------
        'se articolo TCO, devo alimentare anche la quantità in CORPOTC
        'la riga è già stata create da beorgsor, per cui devo solo inserire le quantità
        If NTSCInt(!xxo_codtagl) <> 0 And CBool((ModuliExtDittaDitt(strDittaCorrente) And bsModExtTCO)) Then
          If Not oCldGnor.GetOrdlistTc(strDittaCorrente, NTSCInt(dtrMo!ol_progr), dttTmp) Then
            lNumNewDoc -= 1
            Return False
          End If
          dtrT = oCleGsor.dttECTC.Select("ec_riga = " & NTSCInt(!ec_riga).ToString)
          For i = 1 To 24
            dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dttTmp.Rows(0)("ol_quant" & i.ToString.PadLeft(2, "0"c)))
          Next
        End If    'If NTSCInt(!xxo_codtagl) <> 0 Then

      End With

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
  Public Overridable Function GeneraDoc_CorpoY(ByRef dtrNewRow As DataRow, ByRef dtrImp As DataRow) As Boolean
    Dim dttDesTagl As New DataTable
    Try
      'forzo la MovordOnAddNewRow
      dtrNewRow!codditt = "."
      dtrNewRow!codditt = strDittaCorrente
      dtrNewRow!ec_codart = dtrImp!ol_codart.ToString
      dtrNewRow!ec_fase = NTSCInt(dtrImp!ol_fase)
      dtrNewRow!ec_descr = dtrImp!ol_descr
      dtrNewRow!ec_desint = dtrImp!ol_desint
      dtrNewRow!ec_magaz = dtrImp!ol_magaz
      dtrNewRow!ec_magaz2 = dtrImp!ol_magaz2
      dtrNewRow!ec_flkit = " "
      dtrNewRow!ec_controp = dtrImp!ol_controp
      '--------------------------------------------------------------------------------------------------------------
      DeterminaDateInizioFine(NTSCInt(dtrImp!ol_controp), NTSCDate(dtrImp!ol_datini).ToShortDateString, _
        NTSCDate(dtrImp!ol_datfin).ToShortDateString)
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(oCleGsor.dttET.Rows(0)!et_codese) = 0 Then dtrNewRow!ec_codiva = dtrImp!ol_codiva
      dtrNewRow!ec_note = dtrImp!ol_note
      dtrNewRow!ec_datcons = dtrImp!ol_datcons
      dtrNewRow!ec_dtpianini = NTSCDate(dtrImp!ol_dtpianini)
      dtrNewRow!ec_dtpianfin = NTSCDate(dtrImp!ol_dtpianfin)
      dtrNewRow!ec_dtrichini = NTSCDate(dtrImp!ol_dtrichini)
      dtrNewRow!ec_dtrichfin = NTSCDate(dtrImp!ol_dtrichfin)
      dtrNewRow!ec_datconsor = dtrImp!ol_datcons
      dtrNewRow!ec_stasino = dtrImp!ol_stasino
      dtrNewRow!ec_codcfam = NTSCStr(dtrImp!ol_codcfam)
      dtrNewRow!ec_commeca = dtrImp!ol_commeca
      dtrNewRow!ec_subcommeca = dtrImp!ol_subcommeca
      If NTSCInt(dtrImp!ol_codcena) > 0 Then dtrNewRow!ec_codcena = dtrImp!ol_codcena
      dtrNewRow!ec_codvuo = dtrImp!ol_codvuo
      dtrNewRow!ec_lotto = dtrImp!ol_lotto
      dtrNewRow!ec_codclie = dtrImp!ol_codclie
      dtrNewRow!ec_datini = strDatini
      dtrNewRow!ec_datfin = strDatfin
      dtrNewRow!ec_tctaglia = dtrImp!ol_tctaglia
      dtrNewRow!ec_tcindtagl = dtrImp!ol_tcindtagl

      If dtrIMP.table.Columns.Contains("xxo_tcindtaglf") Then
        dtrNewRow!xxo_tcindtaglf = dtrIMP!xxo_tcindtaglf
      End If
      If dtrIMP.table.Columns.Contains("xxo_tctagliaf") Then
        If NTSCInt(dtrIMP!xxo_codtagl) > 0 Then
          oCldgnor.ValCodiceDb(NTSCInt(dtrIMP!xxo_codtagl).ToString, strDittaCorrente, "TABTAGL", "N", "", dttDesTagl)
          dtrNewRow!xxo_tctagliaf = NTSCStr(dttDesTagl.Rows(0)("tb_dest" & NTSCStr(dtrIMP!xxo_tcindtaglf).PadLeft(2, "0"c))).ToUpper.Trim
        Else
          dtrNewRow!xxo_tctagliaf = " "
        End If
      End If
      '------------------
      dtrNewRow!ec_misura1 = NTSCDec(dtrImp!ol_misura1)
      dtrNewRow!ec_misura2 = NTSCDec(dtrImp!ol_misura2)
      dtrNewRow!ec_misura3 = NTSCDec(dtrImp!ol_misura3)
      dtrNewRow!ec_unmis = dtrImp!ol_unmis
      dtrNewRow!ec_colli = dtrImp!ol_colli
      dtrNewRow!ec_quant = dtrImp!ol_quant
      dtrNewRow!ec_netpid = NTSCStr(dtrImp!ol_netpid)
      If NTSCStr(dtrImp!ol_netpid) <> "" Then dtrNewRow!ec_netpstatus = "REL"

      'se non dovevo ricalcolare i prezzi rimetto quello della prop d'ordine (li ha ricalcolati all'inserimento dell'articolo)
      dtrNewRow!ec_prelist = dtrImp!ol_prelist
      If fRicalcPrez = 0 Then
        dtrNewRow!ec_flprznet = dtrImp!ol_flprznet.ToString
        dtrNewRow!ec_umprz = dtrImp!ol_umprz
        dtrNewRow!ec_prezvalc = NTSCDec(dtrImp!ol_prezvalc)
        If NTSCInt(oCleGsor.dttET.Rows(0)!et_valuta) = 0 Then dtrNewRow!ec_prezzo = NTSCDec(dtrImp!ol_prezzo)
        dtrNewRow!ec_prelist = NTSCDec(dtrImp!ol_prelist)
      End If    'If fRicalcPrez = 0 Then

      'se non dovevo ricalcolare gli sconti rimetto quelli dell'ordine aperto (li ha ricalcolati all'inserimento dell'articolo)
      If fRicalcScon = 0 Then
        dtrNewRow!ec_scont1 = NTSCDec(dtrImp!ol_scont1)
        dtrNewRow!ec_scont2 = NTSCDec(dtrImp!ol_scont2)
        dtrNewRow!ec_scont3 = NTSCDec(dtrImp!ol_scont3)
        dtrNewRow!ec_scont4 = NTSCDec(dtrImp!ol_scont4)
        dtrNewRow!ec_scont5 = NTSCDec(dtrImp!ol_scont5)
        dtrNewRow!ec_scont6 = NTSCDec(dtrImp!ol_scont6)
      End If    'If fRicalcScon = 0 Then

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
  Public Overridable Function GeneraDoc_Attivit(ByRef dtrNewRow As DataRow, ByRef dtrAtt As DataRow) As Boolean
    Try
      With dtrNewRow
        'forzo la MovordOnAddNewRow
        dtrNewRow!codditt = "."
        dtrNewRow!codditt = strDittaCorrente
        dtrNewRow!at_fase = dtrAtt!at_fase
        dtrNewRow!at_coddb = dtrAtt!at_coddb
        dtrNewRow!at_codlavo = dtrAtt!at_codlavo
        dtrNewRow!at_idproj = dtrAtt!at_idproj
        dtrNewRow!at_tempattpr = dtrAtt!at_tempattpr
        dtrNewRow!at_tempesepr = dtrAtt!at_tempesepr
        dtrNewRow!at_tempattees = dtrAtt!at_tempattees
        dtrNewRow!at_tempesees = dtrAtt!at_tempesees
        dtrNewRow!at_flevas = dtrAtt!at_flevas
        dtrNewRow!at_dtminima = dtrAtt!at_dtminima
        dtrNewRow!at_sutipork = "H"
        dtrNewRow!at_suanno = dtrAtt!at_suanno
        dtrNewRow!at_suserie = dtrAtt!at_suserie
        dtrNewRow!at_sunumord = dtrAtt!at_sunumord
        dtrNewRow!at_suriga = dtrAtt!at_suriga
        dtrNewRow!at_sufase = dtrAtt!at_sufase
        dtrNewRow!at_relazsuc = dtrAtt!at_relazsuc
        dtrNewRow!at_qtapr = dtrAtt!at_qtapr
        dtrNewRow!at_qtaes = dtrAtt!at_qtaes
        dtrNewRow!at_datcons = dtrAtt!at_datcons
        dtrNewRow!at_dtpianini = NTSCDate(dtrAtt!at_dtpianini)
        dtrNewRow!at_dtpianfin = NTSCDate(dtrAtt!at_dtpianfin)
        dtrNewRow!at_dtrichini = NTSCDate(dtrAtt!at_dtrichini)
        dtrNewRow!at_dtrichfin = NTSCDate(dtrAtt!at_dtrichfin)
        dtrNewRow!at_terzista = dtrAtt!at_terzista
        dtrNewRow!at_suolprogr = dtrAtt!at_suolprogr
        dtrNewRow!at_note = dtrAtt!at_note
        dtrNewRow!at_netpid = dtrAtt!at_netpid
      End With

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
  Public Overridable Function GeneraDoc_Assris(ByRef dtrNewRow As DataRow, ByRef dtrAss As DataRow) As Boolean
    Try
      With dtrNewRow
        'forzo la MovordOnAddNewRow
        dtrNewRow!codditt = "."
        dtrNewRow!codditt = strDittaCorrente
        dtrNewRow!as_fase = dtrAss!as_fase
        dtrNewRow!as_codcent = dtrAss!as_codcent
        dtrNewRow!as_percent = dtrAss!as_percent
        dtrNewRow!as_controp = dtrAss!as_controp
        dtrNewRow!as_contocontr = dtrAss!as_contocontr
        dtrNewRow!as_codcena = dtrAss!as_codcena
        dtrNewRow!as_cmacora = dtrAss!as_cmacora
        dtrNewRow!as_pagaora = dtrAss!as_pagaora
        dtrNewRow!as_pagaoratt = dtrAss!as_pagaoratt
        dtrNewRow!as_cmacoratt = dtrAss!as_cmacoratt
        'dtrNewRow!as_valore = dtrAss!as_valore     'calcolato in automatico
        'dtrNewRow!as_valmo = dtrAss!as_valmo       'calcolato in automatico
      End With

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

  Public Overridable Sub DeterminaDateInizioFine(ByVal nControp As Integer, _
    ByRef strDatiniIn As String, ByRef strDatfinIn As String)
    Dim lConcova As Integer = 0
    Dim strCodpcon As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strDatini = strDatiniIn
      strDatfin = strDatfinIn
      '--------------------------------------------------------------------------------------------------------------
      If oCldGnor.ValCodiceDb(nControp.ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp) = False Then Return
      If Not dttTmp Is Nothing Then
        If dttTmp.Rows.Count > 0 Then lConcova = NTSCInt(dttTmp.Rows(0)!tb_concova)
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      If lConcova = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oCldGnor.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttTmp) = False Then Return
      If Not dttTmp Is Nothing Then
        If dttTmp.Rows.Count > 0 Then strCodpcon = NTSCStr(dttTmp.Rows(0)!tb_azcodpcon)
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      If oCldGnor.ValCodiceDb(lConcova.ToString, strDittaCorrente, "ANAGCA2", "N", "", dttTmp, strCodpcon) = False Then Return
      If Not dttTmp Is Nothing Then
        If dttTmp.Rows.Count > 0 Then
          Select Case NTSCStr(dttTmp.Rows(0)!ac_accperi)
            Case "D"
              strDatini = strDataNewDoc
              strDatfin = strDataNewDoc
            Case "S"
              strDatini = strDataNewDoc
              strDatfin = DateAdd(DateInterval.Day, 1, NTSCDate(strDataNewDoc)).ToShortDateString
          End Select
        End If
      End If

      dttTmp.Clear()
      dttTmp.Dispose()
      '------------------------------------------------------------------------------------------------------------
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
  End Sub

#Region "FORM BNORSEOS"
  Public Overridable Function edTipoBf_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGnor.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABTPBF", "N", strDescr)
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
  Public Overridable Function edStagione_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGnor.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABSTAG", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128607806571093750, "Codice stagione |'" & nCod.ToString & "'| inesistente")))
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
