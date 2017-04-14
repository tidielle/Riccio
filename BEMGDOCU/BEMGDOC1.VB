Imports System.Data
Imports NTSInformatica.CLN__STD

'ATTENZIONE: visto che quando di cambia una colonna in automatico viene scatenata la beforecolupdate di altre colonne, 
'per far cambaire prezo/sconti/provvig/valori riga ecc occorre fare in questo modo:
'al cambio di articoli/fase/promozione/quantità/... viene lanciata a mano la SETTAPREZZO e SETTASCONTI
'in automatico al cambio di quantità/prezzi/sconti/ecc verrà scatenata la SETTAVALORIRIGA
'che cambierà il valore (ec_valore) e in questa routine verrà eseguita la SETTAPROVVIGIONI
'il cambio di sconti di testata/piede lancierà manualmente la SETTAVALORIRIGA per tutte le righe

Imports System
Partial Public Class CLEMGDOCU

  'variabili per gestire inserimento di cod. articolo da EAN128
  Public strEAN128Barcode As String = ""      'settata ad ogni keydown enlla cella 'ec_codart' e svuotata nella 'AfterColUpdate_ec_codart
  Public nFaseArticoloDaBarcode As Integer = -1
  Public strUnMisDaBarcode As String = ""
  Public dColliDaBarcode, dQuantDaBarcode As Decimal
  Public strLottoXDaAnalotti As String = ""
  Public strEAN128Lotto As String = ""
  Public strEAN128Matric As String = ""
  Public strEAN128DtProd As String = ""
  Public strEAN128DtScad As String = ""

  Public bCaclSelArticoli As Boolean = False '--- Impostata a "True" solo se in ARTEST sono stati trovati più records
  Public bCaclAnnullato As Boolean = True
  Public strCaclCodart As String = ""
  Public lCaclForn As Integer = 0
  Public nCaclCodmarc As Integer = 0

  Public Overridable Function RecordRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Dim dtrT As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing

    Try
      'se è una nuova riga cancello il dettaglio TCO: eseguo il test solo se non sto uscendo da bnorgsor
      If bModTCO And bInUnload = False Then
        dtrT = dsShared.Tables("CORPO").Select(strFilter)(nRow)
        If dsShared.Tables("CORPO").Select(strFilter)(nRow).RowState = DataRowState.Added Then
          GestisciDettaglioTCO(3, NTSCInt(dtrT!ec_riga), False)
        Else
          'potrei essere nella seguente situazione: 
          'ero su una riga salvata con articolo TCO, ho cambiato l'articolo ed ora ripristino: dovrei ricaricare il vecchio dettaglio TCO
          'non posso ripristinare righe se ho cambiato il cod articolo su righe già salvate
          If NTSCStr(dtrT("ec_codart", DataRowVersion.Current)) = NTSCStr(dtrT("ec_codart", DataRowVersion.Original)) Then
            'non ho cambiato l'articolo: non devo modificare il dettaglio TCO
            'ma devo ripristinare le qta precedenti
            dtrT1 = dttECTC.Select("ec_riga = " & NTSCInt(dsShared.Tables("CORPO").Select(strFilter)(nRow)!ec_riga).ToString)
            If dtrT1.Length > 0 Then dtrT1(0).RejectChanges()
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036167395000, "Attenzione: con il modulo TAGLIE e COLORI non è possibile ripristinare righe dove è stato cambiato il codice articolo")))
            Return False
          End If
        End If
      End If

      dsShared.Tables("CORPO").Select(strFilter)(nRow).RejectChanges()

      Return True
    Catch ex As Exception
    End Try
  End Function

  '-----------------------

  Public Overridable Function CaricaUnMis() As DataTable
    '----------------------------
    'ottengo l'elenco delle unità di misura utilizzate in artico
    Dim dttTmp As New DataTable
    Try
      oCldDocu.CaricaUmMis(strDittaCorrente, dttTmp)
      Return dttTmp

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function

  Public Overridable Function GetArticoUnMis(ByVal strCodart As String) As String
    '---------------------------------------
    'ritorna le unità di misura dell'articolo passato in input
    Try
      Return CType(oCleComm, CLELBMENU).TrovaArticoUnMis(strDittaCorrente, strCodart)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
        Return ""
      End If
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function TestEsistenza(ByVal strCodart As String, ByVal nMagaz As Integer, ByVal nFase As Integer, _
                                             ByVal dQuantOrd As Decimal, ByVal dQuantAss As Decimal, _
                                             ByVal lCommeca As Integer, ByVal nCausale As Integer) As Boolean
    Try
      'obsoleta
      Return TestEsistenza(strCodart, nMagaz, nFase, dQuantOrd, dQuantAss, lCommeca, nCausale, False)

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
  Public Overridable Function TestEsistenza(ByVal strCodart As String, ByVal nMagaz As Integer, ByVal nFase As Integer, _
                                            ByVal dQuantOrd As Decimal, ByVal dQuantAss As Decimal, _
                                            ByVal lCommeca As Integer, ByVal nCausale As Integer, ByVal bRigaNota As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodart, nMagaz, nFase, dQuantOrd, dQuantAss, lCommeca, nCausale, bRigaNota})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------
      'obsoleta
      Return TestEsistenza(strCodart, nMagaz, nFase, dQuantOrd, dQuantAss, lCommeca, nCausale, bRigaNota, Nothing)
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
  Public Overridable Function TestEsistenza(ByVal strCodart As String, ByVal nMagaz As Integer, ByVal nFase As Integer, _
                                          ByVal dQuantOrd As Decimal, ByVal dQuantAss As Decimal, _
                                          ByVal lCommeca As Integer, ByVal nCausale As Integer, ByVal bRigaNota As Boolean, ByVal dtrEc As DataRow) As Boolean
    Dim dQuant As Decimal = 0
    Dim strToTipork As String = dttET.Rows(0)!et_tipork.ToString
    Dim dttTmp As New DataTable
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodart, nMagaz, nFase, dQuantOrd, dQuantAss, lCommeca, nCausale, bRigaNota, dtrEc})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If

      If bDocDaRetail Then Return True
      If bInImportRigheDiba Then Return True

      'Tolto sugli articoli D/M
      If UCase(strCodart) = "D" Or UCase(strCodart) = "M" Or strCodart.Trim = "" Then Return True

      If strTipoTestEsist = "O" Or BORDINI = False Then
        'Test sulla q.ta ordinata/q.ta ddt
        dQuant = dQuantOrd
      Else
        'Test sulla q.ta assegnata
        dQuant = dQuantAss
      End If

      'Salta il test se la quantità e 0
      If dQuant = 0 Then Return True

      '---------------------
      'SE NON E' UNA REGISTRAZIONE DI SCARICO ESCE
      'Controlla se  una ordine che scarica il magazzino R, X
      If BORDINI Then
        If strToTipork <> "R" AndAlso strToTipork <> "#" AndAlso strToTipork <> "X" AndAlso strToTipork <> "H" Then Return True
      Else
        'solo per veboll Controlla se è una causale di scarico
        oCldDocu.ValCodiceDb(nCausale.ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          If NTSCInt(dttTmp.Rows(0)!tb_esist) >= 0 Then
            oCldDocu.ValCodiceDb(dttTmp.Rows(0)!tb_causec.ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp)
            If dttTmp.Rows.Count = 0 Then Return True
            If NTSCInt(dttTmp.Rows(0)!tb_esist) >= 0 Then Return True
            nMagaz = NTSCInt(dttET.Rows(0)!et_magaz2)
          End If
        Else
          Return True
        End If
      End If

      If strTestEsistPWD = "" Then
        strTestEsistPWD = UCase(oCldDocu.GetSettingBusDitt(strDittaCorrente, IIf(BORDINI, "Bsorgsor", "Bsveboll").ToString, "Opzioni", ".", "EsistPWD", "NTS", " ", "NTS"))
        If bLeggiOpzioniDoc Then strTestEsistPWD = UCase(oCldDocu.GetSettingBusDitt(strDittaCorrente, IIf(BORDINI, "Bsorgsor", "Bsveboll").ToString, "OpzioniDoc", ".", "EsistPWD", "NTS", NTSCStr(dttET.Rows(0)!et_tipork), strTestEsistPWD))
      End If

      Return TestEsistenzaSTD(strCodart, nMagaz, nFase, dQuant, lCommeca, strTestScomin, strTestEsist, _
                              strDispMultiMag, strTestScominCome, bTestEsistPerComm, strTestEsistCome, _
                              strTestEsistPWD, bRigaNota, dtrEc)

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
    End Try
  End Function


  Public Overridable Function TestEsistenzaSTD(ByVal strCodart As String, ByVal nMagaz As Integer, _
                                                 ByVal nFase As Integer, ByVal dQuant As Decimal, _
                                                 ByVal lCommeca As Integer, _
                                                 ByVal strTestScomin As String, ByVal strTestEsist As String, _
                                                 ByVal strDispMultiMag As String, ByVal strTestScominCome As String, _
                                                 ByVal bTestEsistPerComm As Boolean, ByVal strTestEsistCome As String, _
                                                 ByVal strTestEsistPWD As String) As Boolean
    Try
      'obsoleta
      Return TestEsistenzaSTD(strCodart, nMagaz, nFase, dQuant, lCommeca, strTestScomin, _
                              strTestEsist, strDispMultiMag, strTestScominCome, bTestEsistPerComm, _
                              strTestEsistCome, strTestEsistPWD, False)

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
  Public Overridable Function TestEsistenzaSTD(ByVal strCodart As String, ByVal nMagaz As Integer, _
                                                ByVal nFase As Integer, ByVal dQuant As Decimal, _
                                                ByVal lCommeca As Integer, _
                                                ByVal strTestScomin As String, ByVal strTestEsist As String, _
                                                ByVal strDispMultiMag As String, ByVal strTestScominCome As String, _
                                                ByVal bTestEsistPerComm As Boolean, ByVal strTestEsistCome As String, _
                                                ByVal strTestEsistPWD As String, ByVal bRigaNota As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodart, nMagaz, nFase, dQuant, lCommeca, strTestScomin, _
                              strTestEsist, strDispMultiMag, strTestScominCome, bTestEsistPerComm, _
                              strTestEsistCome, strTestEsistPWD, bRigaNota})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------
      'obsoleta
      Return TestEsistenzaSTD(strCodart, nMagaz, nFase, dQuant, lCommeca, strTestScomin, strTestEsist, strDispMultiMag, strTestScominCome, _
                              bTestEsistPerComm, strTestEsistCome, strTestEsistPWD, bRigaNota, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function TestEsistenzaSTD(ByVal strCodart As String, ByVal nMagaz As Integer, _
                                                ByVal nFase As Integer, ByVal dQuant As Decimal, _
                                                ByVal lCommeca As Integer, _
                                                ByVal strTestScomin As String, ByVal strTestEsist As String, _
                                                ByVal strDispMultiMag As String, ByVal strTestScominCome As String, _
                                                ByVal bTestEsistPerComm As Boolean, ByVal strTestEsistCome As String, _
                                                ByVal strTestEsistPWD As String, ByVal bRigaNota As Boolean, ByVal dtrEc As DataRow) As Boolean
    'bRigaNota = riga collegata ad una nota di prelievo

    Dim dttTmp As DataTable = New DataTable
    Dim strStainv As String = "S"
    Dim strGescomm As String = "N"
    Dim dPren As Decimal = 0
    Dim dEsist As Decimal = 0
    Dim dScomin As Decimal = 0
    Dim dImpeg As Decimal = 0
    Dim dOrdin As Decimal = 0
    Dim strComm As String = ""
    Dim strToTipork As String = dttET.Rows(0)!et_tipork.ToString
    Dim dTmp As Decimal = 0
    Dim strTestoMsgTC As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodart, nMagaz, nFase, dQuant, lCommeca, strTestScomin, _
                              strTestEsist, strDispMultiMag, strTestScominCome, bTestEsistPerComm, _
                              strTestEsistCome, strTestEsistPWD, bRigaNota, dtrEc})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      If (strToTipork = "H" And Not bTest_esistenza_duplimpprod) OrElse bDocDaRetail Then Return True

      If bRigaNota And strTestEsist <> " " And strTestEsist <> "N" And strTestEsistCome = "D" Then
        'in evasione di una riga collegata ad una nota di prelievo, se il test esistenza deve essere fatto su esistenza netta (esistenza - prenotato)
        'non faccio il controllo
        Return True
      End If

      If UCase(strTestScomin) = "S" Or (strTestEsist <> " " And UCase(strTestEsist) <> "N") Then
        If GetMemDttArti(strCodart).Rows.Count > 0 Then
          dScomin = NTSCDec(dttArti.Rows(0)!ar_scomin)
          strStainv = dttArti.Rows(0)!ar_stainv.ToString
          strGescomm = dttArti.Rows(0)!ar_gescomm.ToString
        End If
        'Non effettua nessun controllo (sottoscorta/esistenza) sugli articoli da non stam,pare in inventario.
        If UCase(strStainv) <> "S" Then Return True

        'Se esiste il record in ARTMAGA per quell'articolo/magazzino, allora preleva da lì
        'la scorta minima
        dTmp = oCldDocu.GetScominArtmaga(strDittaCorrente, strCodart, nMagaz)
        If dTmp <> -1 Then dScomin = dTmp

        'Controlla la disponibilità
        If strTestEsistArtTc = "T" AndAlso NTSCInt(dtrEc!xxo_codtagl) <> 0 Then
          If oCldDocu.ChekEsistenzaFinaleTC(strDittaCorrente, strCodart, nFase, nMagaz, dttTmp) Then
            Dim dtrRow() As DataRow = dttECTC.Select("ec_riga = " & NTSCInt(dtrEc!ec_riga))

            For i As Integer = 1 To 24
              If NTSCDec(dttTmp.Rows(0)("apt_esist" & i.ToString("00"))) - NTSCDec(dtrRow(0)("ec_quant" & i.ToString("00"))) < 0 Then
                strTestoMsgTC &= oApp.Tr(Me, 129990194412845627, "'|" & GetDescrTaglia(NTSCInt(dtrEc!xxo_codtagl), i) & "'| esist. |" & NTSCDec(dttTmp.Rows(0)("apt_esist" & i.ToString("00"))) & "|, ")
              End If
            Next
            If strTestoMsgTC.Trim.Length <> 0 Then strTestoMsgTC = strTestoMsgTC.Remove(strTestoMsgTC.Length - 2)
          End If
        Else
          If CType(oCleComm, CLELBMENU).LeggiArtpro(strDittaCorrente, strCodart, nFase, nMagaz, strDispMultiMag, dttTmp) Then
            dEsist = NTSCDec(dttTmp.Rows(0)!Esistenza)
            dPren = NTSCDec(dttTmp.Rows(0)!Prenotato)
            dImpeg = NTSCDec(dttTmp.Rows(0)!Impegnato)
            dOrdin = NTSCDec(dttTmp.Rows(0)!Ordinato)
          End If
        End If
        dttTmp.Clear()
      End If    'If UCase(strTestScomin) = "S" Or (strTestEsist <> " " And UCase(strTestEsist) <> "N") Then

      'Test scorta minima
      If UCase(strTestScomin) = "S" And dScomin <> 0 Then
        If strTestScominCome = "A" Then
          'esist-prenot (default)
          If ArrDbl((dEsist - dPren - dQuant), 3) < ArrDbl(dScomin, 3) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222091562500, "Attenzione: la disponibilità netta dell'articolo '|" & strCodart & "|' (|" & (dEsist - dPren).ToString(oApp.FormatQta) & "|) è scesa sotto la scorta minima (|" & dScomin.ToString(oApp.FormatQta) & "|).")))
          End If
        Else
          'esist-impeg
          If ArrDbl((dEsist - dImpeg - dQuant), 3) < ArrDbl(dScomin, 3) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222091718750, "Attenzione: la disponibilità (esistenza meno impegnato) dell'articolo '|" & strCodart & "|' (|" & (dEsist - dImpeg).ToString(oApp.FormatQta) & "|) è scesa sotto la scorta minima (|" & dScomin.ToString(oApp.FormatQta) & "|).")))
          End If
        End If
      End If
      'Legge la disponibilità per commessa
      If strGescomm = "S" And lCommeca > 0 And bTestEsistPerComm Then
        dEsist = CType(oCleComm, CLELBMENU).TrovaEsistDaXpro(strDittaCorrente, strCodart, nMagaz, lCommeca, 0, nFase, "", 0, dImpeg, dPren)
        strComm = oApp.Tr(Me, 130420481437120410, " (commessa |" & lCommeca & "|)")
      End If

      'Test esistenza
      Select Case strTestEsistCome
        Case "D" : dEsist = dEsist - dPren
        Case "I" : dEsist = dEsist - dImpeg + dOrdin
      End Select

      If strTestEsist <> " " And UCase(strTestEsist) <> "N" And strTestEsistQuando <> "F" Then
        If ((strTestEsistArtTc = "T" AndAlso NTSCInt(dtrEc!xxo_codtagl) <> 0) And strTestoMsgTC.Trim <> "") OrElse _
           (Not (strTestEsistArtTc = "T" AndAlso NTSCInt(dtrEc!xxo_codtagl) <> 0) And ArrDbl(dEsist - dQuant, 3) < 0) Then
          Select Case UCase(strTestEsist)
            Case "A" 'Avviso
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222091875000, _
              "Attenzione: l'esistenza/disponibilità dell'articolo '|" & strCodart & "|'|" & strComm & "| è negativa.") & strTestoMsgTC))
            Case "P" 'Sblocco con password
              'la chiamata 'ThrowRemoteEvent' non è asincrona, per cui BNORGSOR fa in tempo ad impostare la variabile restituita da NTSEventArgs
              Dim msg As New NTSEventArgs(CLN__STD.ThMsg.INPUTPWD, oApp.Tr(Me, 127791079255937500, _
              "Articolo |'" & strCodart & "|'|" & strComm & "|:" & vbCrLf & _
              "Inserire la Password per sbloccare i movimenti di articoli con esistenza/disponibilità negativa:") & vbCrLf & strTestoMsgTC)
Riprova:
              ThrowRemoteEvent(msg)
              If msg.RetValue <> "" Then
                'Controlla PWD
                If UCase(msg.RetValue) <> UCase(strTestEsistPWD) Then
                  GoTo Riprova
                End If
              Else
                Return False
              End If
            Case "B" 'Blocco
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222092031250, "Attenzione: l'esistenza/disponibilità dell'articolo |'" & strCodart & "'" & strComm & "| sta diventando negativa. Impossibile continuare |" & strTestoMsgTC & "|.")))
              Return False
          End Select
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

  Public Overridable Function TestSottoCosto(ByVal strCodart As String, ByVal nFase As Integer, ByVal dQuant As Decimal, _
                                             ByVal dPrezzo As Decimal, ByVal dtDatdoc As Date, ByVal nCodValu As Integer, _
                                             ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, ByVal dSc3 As Decimal, _
                                             ByVal dSc4 As Decimal, ByVal dSc5 As Decimal, ByVal dSc6 As Decimal, _
                                             ByVal dSt1 As Decimal, ByVal dSt2 As Decimal, ByVal dScPag As Decimal, _
                                             ByVal strTKit As String, ByVal lRiga As Integer, ByRef dtrEc As DataRow) As Boolean
    Try
      Return TestSottoCosto(strCodart, nFase, dQuant, dPrezzo, dtDatdoc, nCodValu, dSc1, dSc2, dSc3, _
                            dSc4, dSc5, dSc6, dSt1, dSt2, dScPag, strTKit, lRiga, dtrEc, 0, 0)
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
  Public Overridable Function TestSottoCosto(ByVal strCodart As String, ByVal nFase As Integer, ByVal dQuant As Decimal, _
                                              ByVal dPrezzo As Decimal, ByVal dtDatdoc As Date, ByVal nCodValu As Integer, _
                                              ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, ByVal dSc3 As Decimal, _
                                              ByVal dSc4 As Decimal, ByVal dSc5 As Decimal, ByVal dSc6 As Decimal, _
                                              ByVal dSt1 As Decimal, ByVal dSt2 As Decimal, ByVal dScPag As Decimal, _
                                              ByVal strTKit As String, ByVal lRiga As Integer, ByRef dtrEc As DataRow, _
                                              ByVal dScp As Decimal, ByVal dScv As Decimal) As Boolean
    Dim strTestSottoCostoLOC As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodart, nFase, dQuant, dPrezzo, dtDatdoc, nCodValu, dSc1, dSc2, dSc3, _
                            dSc4, dSc5, dSc6, dSt1, dSt2, dScPag, strTKit, lRiga, dtrEc, dScp, dScv})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrEc = CType(oIn(17), DataRow)
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      '--- Se l'articolo è ti tipo "Descrittivo" ('D' o 'M'), ritorna True
      '--------------------------------------------------------------------------------------------------------------
      Select Case strCodart
        Case "D", "M" : Return True
      End Select
      '--------------------------------------------------------------------------------------------------------------
      'TEST VENDITA SOTTOCOSTO
      'Controllo test abilitato
      If ((strTestSottoCosto = "A" Or strTestSottoCosto = "P" Or strTestSottoCosto = "B") And bNew) Or _
         ((strTestSottoCostoModifica = "A" Or strTestSottoCostoModifica = "P" Or strTestSottoCostoModifica = "B") And bNew = False) Then
        'da fare
        'controllo fatto riga per riga
        If strTestSottoCostoQuando <> "A" And strTestSottoCostoQuando <> "C" Then
          'da non fare
          Return True
        Else
          If strTKit <> "A" And strTKit <> "T" Then
            'da fare
          Else
            'da non fare
            Return True
          End If

          If BORDINI Then
            'Esegue il test solo sui documenti R (impegni cliente)
            If dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "Q" Or dttET.Rows(0)!et_tipork.ToString = "#" Then
              'da fare
            Else
              'da non fare
              Return True
            End If
          Else
            'Esegue il test solo sui documenti A,B,C,E,W,F,S
            If dttET.Rows(0)!et_tipork.ToString = "A" Or dttET.Rows(0)!et_tipork.ToString = "B" Or dttET.Rows(0)!et_tipork.ToString = "C" Or _
               dttET.Rows(0)!et_tipork.ToString = "E" Or dttET.Rows(0)!et_tipork.ToString = "W" Or dttET.Rows(0)!et_tipork.ToString = "F" Or _
               dttET.Rows(0)!et_tipork.ToString = "S" Then
              'da fare
            Else
              If (dttET.Rows(0)!et_tipork.ToString = "Z") And (bTestSottoCostoBolMovInt = True) Then
                'da fare
              End If
              'da non fare
              Return True
            End If
          End If

        End If
      Else
        'da non fare
        Return True
      End If

      If strTestSottoPWD = "" Then strTestSottoPWD = UCase(oCldDocu.GetSettingBusDitt(strDittaCorrente, IIf(BORDINI, "Bsorgsor", "Bsveboll").ToString, "Opzioni", ".", "TestSottoCostoPWD", "NTS", " ", "NTS"))
      If bNew Then strTestSottoCostoLOC = strTestSottoCosto Else strTestSottoCostoLOC = strTestSottoCostoModifica
      Return TestSottoCostoSTD(strCodart, nFase, dQuant, dPrezzo, dtDatdoc, nCodValu, _
                                    dSc1, dSc2, dSc3, dSc4, dSc5, dSc6, dSt1, dSt2, dScPag, _
                                    strTKit, strTestSottoPWD, strTestSottoCostoLOC, strTestSottoCostoTipo, _
                                    strTestSottoCostoZero, strTestSottoCostoQuando, False, _
                                    dTestSottoCostoRicaricoMinimo, bTestSottoCostoOscuraCosto, lRiga, _
                                    NTSCInt(dtrEc!ec_codtpro), dtrEc!ec_flprznet.ToString, dScp, dScv)

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
  Public Overridable Function TestSottoCostoSTD(ByVal strCodart As String, ByVal nFase As Integer, ByVal dQuant As Decimal, _
                                            ByVal dPrezzo As Decimal, ByVal dtDatdoc As Date, ByVal nCodValu As Integer, _
                                            ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, ByVal dSc3 As Decimal, _
                                            ByVal dSc4 As Decimal, ByVal dSc5 As Decimal, ByVal dSc6 As Decimal, _
                                            ByVal dSt1 As Decimal, ByVal dSt2 As Decimal, ByVal dScPag As Decimal, _
                                            ByVal strTKit As String, ByVal strTestSottoPWD As String, _
                                            ByVal strTestSottoCosto As String, _
                                            ByVal strTestSottoCostoTipo As String, _
                                            ByVal strTestSottoCostoZero As String, _
                                            ByVal strTestSottoCostoQuando As String, _
                                            ByVal bTestFinale As Boolean, _
                                            ByVal dTestSottoCostoRicaricoMinimo As Decimal, _
                                            ByVal bTestSottoCostoOscuraCosto As Boolean, ByVal nRiga As Integer, _
                                            ByVal nCodtpro As Integer, ByVal strPrzNet As String) As Boolean
    Try
      Return TestSottoCostoSTD(strCodart, nFase, dQuant, dPrezzo, dtDatdoc, nCodValu, _
                               dSc1, dSc2, dSc3, dSc4, dSc5, dSc6, dSt1, dSt2, dScPag, _
                               strTKit, strTestSottoPWD, strTestSottoCosto, strTestSottoCostoTipo, _
                               strTestSottoCostoZero, strTestSottoCostoQuando, bTestFinale, _
                               dTestSottoCostoRicaricoMinimo, bTestSottoCostoOscuraCosto, nRiga, _
                               nCodtpro, strPrzNet, 0, 0)
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
  Public Overridable Function TestSottoCostoSTD(ByVal strCodart As String, ByVal nFase As Integer, ByVal dQuant As Decimal, _
                                              ByVal dPrezzo As Decimal, ByVal dtDatdoc As Date, ByVal nCodValu As Integer, _
                                              ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, ByVal dSc3 As Decimal, _
                                              ByVal dSc4 As Decimal, ByVal dSc5 As Decimal, ByVal dSc6 As Decimal, _
                                              ByVal dSt1 As Decimal, ByVal dSt2 As Decimal, ByVal dScPag As Decimal, _
                                              ByVal strTKit As String, ByVal strTestSottoPWD As String, _
                                              ByVal strTestSottoCosto As String, _
                                              ByVal strTestSottoCostoTipo As String, _
                                              ByVal strTestSottoCostoZero As String, _
                                              ByVal strTestSottoCostoQuando As String, _
                                              ByVal bTestFinale As Boolean, _
                                              ByVal dTestSottoCostoRicaricoMinimo As Decimal, _
                                              ByVal bTestSottoCostoOscuraCosto As Boolean, ByVal nRiga As Integer, _
                                              ByVal nCodtpro As Integer, ByVal strPrzNet As String, _
                                              ByVal dScp As Decimal, ByVal dScv As Decimal) As Boolean
    Dim dOuPrezzo As Decimal
    Dim dOUPrelist As Decimal
    Dim nOUPromo As Integer
    Dim strParteMsg As String = ""
    Dim strMsg As String
    Dim dPrezzoNet As Decimal
    Dim evnt As NTSEventArgs = Nothing
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodart, nFase, dQuant, dPrezzo, dtDatdoc, nCodValu, _
                               dSc1, dSc2, dSc3, dSc4, dSc5, dSc6, dSt1, dSt2, dScPag, _
                               strTKit, strTestSottoPWD, strTestSottoCosto, strTestSottoCostoTipo, _
                               strTestSottoCostoZero, strTestSottoCostoQuando, bTestFinale, _
                               dTestSottoCostoRicaricoMinimo, bTestSottoCostoOscuraCosto, nRiga, _
                               nCodtpro, strPrzNet, dScp, dScv})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '----------------------
      'TEST VENDITA SOTTOCOSTO

      'Controllo test abilitato
      If strTestSottoCosto <> "A" And strTestSottoCosto <> "P" And strTestSottoCosto <> "B" Then Return True
      'Calcolo del prezzo netto
      dPrezzoNet = ArrDbl(dPrezzo * (100 - dSc1) / 100 * (100 - dSc2) / 100 * (100 - dSc3) / 100 * (100 - dSc4) / 100 * (100 - dSc5) / 100 * (100 - dSc6) / 100 * (100 - dScp) / 100 - dScv * (100 - dSt1) / 100 * (100 - dSt2) / 100 * (100 - dScPag) / 100, oApp.NDecPrzUn)
      'Controlla se il test  escluso sui prezzi a 0
      If strTestSottoCostoZero <> "S" Then
        If dPrezzoNet = 0 Then Return True
      End If
      'Inizio controllo
      dOuPrezzo = 0
      dOUPrelist = 0
      nOUPromo = 0
      '----------------------
      ' Preparazione dati per ricerca prezzo
      '----------------------
      SettaCondCommerciali_IstanziaNTSCondCommerciali()
      oCondCommerciali.bCalcolaPrezzo = True
      With oCondCommerciali.Input
        .strDitta = strDittaCorrente
        .strCodart = strCodart
        .nFase = nFase
        .strTipoval = "P"
        .bConspromo = True
        .dtDatdoc = dtDatdoc
        .nCodvalu = nCodValu
        .dQuant = dQuant
      End With
      '----------------------
      Select Case strTestSottoCostoTipo
        Case "U" 'Ultimo costo
          CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
          With oCondCommerciali.OutputPrezzo
            dOuPrezzo = .dPrezzo
            dOUPrelist = .dPrelist
            nOUPromo = .nCodpromo
          End With
          strParteMsg = "l'ultimo costo di acquisto"
        Case "M" 'Costo medio
          oCondCommerciali.Input.nListino = -2
          CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
          With oCondCommerciali.OutputPrezzo
            dOuPrezzo = .dPrezzo
            dOUPrelist = .dPrelist
            nOUPromo = .nCodpromo
          End With
          strParteMsg = " medio "
        Case "S" 'Costo standard
          oCondCommerciali.Input.nListino = nPeacListinCStd
          CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
          With oCondCommerciali.OutputPrezzo
            dOuPrezzo = .dPrezzo
            dOUPrelist = .dPrelist
            nOUPromo = .nCodpromo
          End With
          strParteMsg = " standard " & nPeacListinCStd
        Case Else
          If IsNumeric(strTestSottoCostoTipo) Then
            If CLng(strTestSottoCostoTipo) >= 1 And CLng(strTestSottoCostoTipo) <= 9999 Then
              oCondCommerciali.Input.nListino = NTSCInt(strTestSottoCostoTipo)
              oCondCommerciali.Input.nCodpromo = nCodtpro
              oCondCommerciali.Input.nCodvalu = 0
              CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
              With oCondCommerciali.OutputPrezzo
                dOuPrezzo = .dPrezzo
                dOUPrelist = .dPrelist
                nOUPromo = .nCodpromo
                strPrzNet = .strPrzNet
              End With
              strParteMsg = " listino " & NTSCInt(strTestSottoCostoTipo).ToString
            End If
          End If
      End Select

      'Se presente una % di ricarico alza il prezzo
      If dTestSottoCostoRicaricoMinimo <> 0 Then
        dOuPrezzo = ArrDbl(dOuPrezzo + ((dOuPrezzo * dTestSottoCostoRicaricoMinimo) / 100), oCldDocu.TrovaNdecSuPrzUn(0))
      End If

      'Esegue il controllo
      If dPrezzoNet < dOuPrezzo Then
        strMsg = oApp.Tr(Me, 128913656303904860, "Attenzione! Il prezzo di vendita, al netto degli sconti, dell'articolo |'" & strCodart & "' (" & dPrezzoNet & ")| sulla riga |" & nRiga.ToString & "| è inferiore al listino costo |" & strParteMsg & "| (|" & IIf(bTestSottoCostoOscuraCosto, "*********", dOuPrezzo.ToString(oApp.FormatPrzUn)).ToString & "|" & " ricaricato |" & dTestSottoCostoRicaricoMinimo & "%|).")
        Select Case strTestSottoCosto
          Case "A" 'Avviso
            If bTestFinale Then
              strMsg += vbCrLf & vbCrLf & oApp.Tr(Me, 128526609851280000, "Continuare il controllo?")
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, strMsg)
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
            Else
              ThrowRemoteEvent(New NTSEventArgs("", strMsg))
            End If
          Case "P" 'Sblocco con password
            'la chiamata 'ThrowRemoteEvent' non è asincrona, per cui BNORGSOR fa in tempo ad impostare la variabile restituita da NTSEventArgs
            Dim msg As New NTSEventArgs(CLN__STD.ThMsg.INPUTPWD, strMsg & vbCrLf & vbCrLf & oApp.Tr(Me, 127794831301770525, "Inserire la Password per sbloccare le vendite sotto-costo:"))
Riprova:
            ThrowRemoteEvent(msg)
            If msg.RetValue <> "" Then
              'Controlla PWD
              If UCase(msg.RetValue) <> UCase(strTestSottoPWD) Then
                GoTo Riprova
              End If
            Else
              Return False
            End If
          Case "B" 'Blocco
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 127791222092343750, " Impossibile continuare.")))
            Return False
        End Select
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

  '-----------------------

#Region "Setta Prezzo\Sconti\Provvigioni\Valori Riga"
  Public Overridable Sub SettaPrezzo(ByRef dtrEC As DataRow, ByVal strCodart As String, _
                                     ByVal strVisMemList1 As String, ByVal nVisMemNumList As Integer)
    Dim dPrezzo As Decimal = 0
    Dim dPrelist As Decimal = 0
    Dim nPromo As Integer = 0
    Dim bCanc As Boolean = False
    Dim dImponib As Decimal = 0
    Dim dIva As Decimal = 0
    Dim lCodLavo As Integer = 0
    Dim nList As Integer
    Dim strPrzNet As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim dDaQuantOut As Decimal = 0
    Dim dAQuantOut As Decimal = 0
    Dim dPerqtaOut As Decimal = 0
    Dim strUnmisOut As String = ""
    Dim nCodTpro As Integer = 0
    Dim nTmp As Integer = 0

    Try
      '------------------------------------------
      'esco se  una riga figlia di un kit con prezzo sintetico
      If strCodart.Trim = "" Then Exit Sub
      If BGESTORDLIST = False Then
        If dtrEC!ec_flkit.ToString = "A" Then Exit Sub
        nCodTpro = NTSCInt(dtrEC!ec_codtpro)
      End If

      '------------------------------------------
      'il prezzo viene calcolato sempre e solo sulle nuove righe, 
      'oppure se o settato l'opzione di rilettura prezzi per quantità
      'solo in import_righe_ord o import_righe_nota se settata l'opzione ricalcolo i prezzi
      'solo se cambio l'articolo devo ricalcolare il prezzo, anche se la riga è in modifica
      'oppure se sto duplicando un documento tipo IC/PREV in OF/OP
      'oppure da bnorgsol in righe in modifica quando cambio il fornitore
      If dtrEC.RowState <> DataRowState.Added AndAlso bForzaLetturaPrezziQta = False Then
        If bInValidazCodart = False AndAlso bInDuplicadoc = False AndAlso bCambioContoGsol = False AndAlso bInEsplodiDiba = False AndAlso bRicalcolaValoriRiga = False Then Return
      End If

      If BORDINI = False Then
        'veboll: Esce se la riga deriva da ordine (non rileva + il prezzo)
        'devo ricalcolarlo solo se ho cambiato l'articolo o la fase articolo dopo aver acquisito la riga d'ordine
        If NTSCInt(dtrEC!ec_ornum) > 0 Then
          If bInValidazCodart = False AndAlso bInValidazFaseCodart = False Then Return
        End If

        'se sto importanto dighe d'ordine e devo mantenere il prezzo indicato nell'ordine è inutile che rilacio la cercaprezzo
        'devo ricalcolare il prezzo solo se 'no carico da prod e opzione abilitata'
        If bInImportRigheOrd = True Then
          If nRicalcPrezziOrd <> 0 AndAlso (dtrEC!ec_tipork.ToString <> "T" OrElse dtrEC!ec_tipork.ToString = "U") Then
            'devo ricalcolare il prezzo: non devo prendere quello dell'ordine: proseguo
          Else
            Exit Sub
          End If
        End If
        If bInImportRigheNota = True Then
          If nRicalcPrezziNota <> 0 AndAlso (dtrEC!ec_tipork.ToString <> "T" OrElse dtrEC!ec_tipork.ToString = "U") Then
            'devo ricalcolare il prezzo: non devo prendere quello della nota di prelievo: proseguo
          Else
            Exit Sub
          End If
        End If
      End If

      '------------------------------------------
      'se riga di impegno/scarico a prod calcolo il prezzo e passo
      If dtrEC!ec_tipork.ToString = "Y" OrElse dtrEC!ec_tipork.ToString = "U" Then
        nList = nListinoScarichi

        If Not CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, strCodart, lCodLavo, 0, _
                                nList, "", NTSCInt(dtrEC!ec_fase), "P", True, 0, 0, _
                                NTSCDate(dttET.Rows(0)!et_datdoc), NTSCInt(dttET.Rows(0)!et_valuta), _
                                NTSCDec(dtrEC!ec_quant), dPrezzo, dPrelist, nPromo, strPrzNet, nPrperqta, _
                                dDaQuantOut, dAQuantOut, dPerqtaOut, strUnmisOut, strTipovalOut) Then dPrezzo = 0
        dPrelist = 0
        GoTo PASSA
      End If

      nList = NTSCInt(dttET.Rows(0)!et_listino)
      strPrzNet = NTSCStr(dtrEC!ec_flprznet)

      '------------------------------------------
      'Se è un ordine di prod. terzista e la riga non è nuova legge il cod. lavorazione
      If bTerzista AndAlso dtrEC.RowState <> DataRowState.Added Then
        If dttET.Rows(0)!et_tipork.ToString = "H" Then
          dtrT = dsShared.Tables("ATTIVIT").Select("at_riga = " & dtrEC!ec_riga.ToString, "at_fase")
          If dtrT.Length > 0 Then lCodLavo = NTSCInt(dtrT(0)!at_codlavo)
          nList = nPeacListinCStd
        End If
        If dttET.Rows(0)!et_tipork.ToString = "T" Then
          dtrT = dsShared.Tables("LAVCENT").Select("lce_riga = " & dtrEC!ec_riga.ToString, "lce_rigaa")
          If dtrT.Length > 0 Then lCodLavo = NTSCInt(dtrT(0)!lce_codlavo)
          nList = nPeacListinCStd
        End If
      End If

      '-----------------------------------------------------------
      'determino prezzo e prezzo di listino
      If dtrEC!ec_umprz.ToString <> "S" Then
        If Not CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, strCodart, lCodLavo, NTSCInt(dttET.Rows(0)!et_conto), _
                                nList, NTSCStr(IIf(bGestionePrezzi, NTSCStr(dtrEC!ec_ump), "")), NTSCInt(dtrEC!ec_fase), "P", True, nCodTpro, 0, _
                                NTSCDate(dttET.Rows(0)!et_datdoc), NTSCInt(dttET.Rows(0)!et_valuta), _
                                NTSCDec(dtrEC!ec_quant), dPrezzo, dPrelist, nPromo, strPrzNet, nPrperqta, _
                                dDaQuantOut, dAQuantOut, dPerqtaOut, strUnmisOut, strTipovalOut) Then dPrezzo = 0
      Else
        If Not CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, strCodart, lCodLavo, NTSCInt(dttET.Rows(0)!et_conto), _
                                nList, dtrEC!ec_unmis.ToString, NTSCInt(dtrEC!ec_fase), "P", True, nCodTpro, 0, _
                                NTSCDate(dttET.Rows(0)!et_datdoc), NTSCInt(dttET.Rows(0)!et_valuta), _
                                NTSCDec(dtrEC!ec_colli), dPrezzo, dPrelist, nPromo, strPrzNet, nPrperqta, _
                                dDaQuantOut, dAQuantOut, dPerqtaOut, strUnmisOut, strTipovalOut) Then dPrezzo = 0
        If dPrezzo = 0 Then
          If Not CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, strCodart, lCodLavo, NTSCInt(dttET.Rows(0)!et_conto), _
                                  nList, dtrEC!ec_ump.ToString, NTSCInt(dtrEC!ec_fase), "P", True, nCodTpro, 0, _
                                  NTSCDate(dttET.Rows(0)!et_datdoc), NTSCInt(dttET.Rows(0)!et_valuta), _
                                  NTSCDec(dtrEC!ec_quant), dPrezzo, dPrelist, nPromo, strPrzNet, nPrperqta, _
                                  dDaQuantOut, dAQuantOut, dPerqtaOut, strUnmisOut, strTipovalOut) Then dPrezzo = 0
          If dPrelist <> 0 Then
            'prelist  sempre in valuta 0!!!
            CType(oCleComm, CLELBMENU).ConvertiPrezzoperUM(strDittaCorrente, strCodart, _
                                         dtrEC!ec_unmis.ToString, _
                                         dPrelist, 0)
          End If
          If dPrezzo <> 0 Then
            CType(oCleComm, CLELBMENU).ConvertiPrezzoperUM(strDittaCorrente, strCodart, _
                                         dtrEC!ec_unmis.ToString, _
                                         dPrezzo, _
                                         NTSCInt(dttET.Rows(0)!et_valuta))
          End If
        End If    'If dPrezzo = 0 Then
      End If

PASSA:
      dtrEC!ec_prelist = dPrelist
      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
        dtrEC!ec_prezvalc = dPrezzo
        dtrEC!ec_prezzo = oCldDocu.ConvImpValuta(strDittaCorrente, True, dPrezzo, NTSCInt(dttET.Rows(0)!et_valuta), _
                                                NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
      ElseIf dttET.Rows(0)!et_scorpo.ToString = "S" Then
        dtrEC!ec_preziva = dPrezzo
        'Scorpora il prezzo
        dImponib = 0
        dIva = 0
        oCldDocu.Scorporo(dPrezzo, NTSCInt(dtrEC!ec_codiva), dImponib, dIva, oCldDocu.TrovaNdecSuPrzUn(0))
        dtrEC!ec_prezzo = dImponib
        If Not bPrelistIvato Then
          'Scorporo di prelist
          dImponib = 0
          dIva = 0
          oCldDocu.Scorporo(dPrelist, NTSCInt(dtrEC!ec_codiva), dImponib, dIva, oCldDocu.TrovaNdecSuPrzUn(0))
          dtrEC!ec_prelist = dImponib
        End If
      Else
        dtrEC!ec_prezzo = dPrezzo
      End If

      If dtrEC!ec_tipork.ToString = "Y" OrElse dtrEC!ec_tipork.ToString = "U" Then
        'sugli scarichi non è mai prezzo netto
        dtrEC!ec_flprznet = "N"
      Else
        dtrEC!ec_flprznet = strPrzNet
      End If


      '-----------------------------------------------------------
      'Adesso se necessario legge il listino 1 per memorizzarlo su opzione
      If strVisMemList1 = "S" AndAlso dtrEC!ec_tipork.ToString <> "Y" AndAlso dtrEC!ec_tipork.ToString <> "U" Then
        If dtrEC!ec_umprz.ToString <> "S" Then
          If Not CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, strCodart, 0, 0, nVisMemNumList, _
                                  NTSCStr(IIf(bGestionePrezzi, NTSCStr(dtrEC!ec_ump), "")), _
                                  NTSCInt(dtrEC!ec_fase), "P", True, nCodTpro, 0, NTSCDate(dttET.Rows(0)!et_datdoc), _
                                  NTSCInt(dttET.Rows(0)!et_valuta), NTSCDec(dtrEC!ec_quant), _
                                  dPrezzo, dPrelist, nPromo, strPrzNet, nTmp, _
                                  dDaQuantOut, dAQuantOut, dPerqtaOut, strUnmisOut, strTipovalOut) Then dPrezzo = 0
        Else
          If Not CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, strCodart, 0, 0, nVisMemNumList, _
                                  dtrEC!ec_unmis.ToString, NTSCInt(dtrEC!ec_fase), "P", True, nCodTpro, 0, _
                                  NTSCDate(dttET.Rows(0)!et_datdoc), NTSCInt(dttET.Rows(0)!et_valuta), _
                                  NTSCDec(dtrEC!ec_colli), dPrezzo, dPrelist, nPromo, strPrzNet, nTmp, _
                                  dDaQuantOut, dAQuantOut, dPerqtaOut, strUnmisOut, strTipovalOut) Then dPrezzo = 0
          If dPrezzo = 0 Then
            If Not CType(oCleComm, CLELBMENU).CercaPrezzo(strDittaCorrente, strCodart, 0, 0, nVisMemNumList, _
                                    dtrEC!ec_ump.ToString, NTSCInt(dtrEC!ec_fase), "P", True, nCodTpro, 0, _
                                    NTSCDate(dttET.Rows(0)!et_datdoc), NTSCInt(dttET.Rows(0)!et_valuta), _
                                    NTSCDec(dtrEC!ec_quant), dPrezzo, dPrelist, nPromo, strPrzNet, nTmp, _
                                    dDaQuantOut, dAQuantOut, dPerqtaOut, strUnmisOut, strTipovalOut) Then dPrezzo = 0
            If dPrelist <> 0 Then
              'prelist  sempre in valuta 0!!!
              CType(oCleComm, CLELBMENU).ConvertiPrezzoperUM(strDittaCorrente, strCodart, _
                                           dtrEC!ec_unmis.ToString, _
                                           dPrelist, 0)
            End If
            If dPrezzo <> 0 Then
              CType(oCleComm, CLELBMENU).ConvertiPrezzoperUM(strDittaCorrente, strCodart, _
                                           dtrEC!ec_unmis.ToString, _
                                           dPrezzo, _
                                           NTSCInt(dttET.Rows(0)!et_valuta))
            End If
          End If    'If dPrezzo = 0 Then
        End If
        If dPrelist = 0 Then dPrelist = dPrezzo
        If dttET.Rows(0)!et_scorpo.ToString = "S" AndAlso (Not bPrelistIvato) Then
          dImponib = 0
          dIva = 0
          oCldDocu.Scorporo(dPrelist, NTSCInt(dtrEC!ec_codiva), dImponib, dIva, oCldDocu.TrovaNdecSuPrzUn(0))
          dtrEC!ec_prelist = dImponib
        Else
          dtrEC!ec_prelist = dPrelist
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
      If bForzaLetturaPrezziQta Then nPrperqta = bsPrSetsiqta
    End Try
  End Sub

  Public Overridable Sub SettaSconti(ByRef dtrEC As DataRow, ByVal nClascon As Integer, ByVal nClscan As Integer)
    Dim nPromo As Integer = 0
    Dim dScont1 As Decimal = 0
    Dim dScont2 As Decimal = 0
    Dim dScont3 As Decimal = 0
    Dim dScont4 As Decimal = 0
    Dim dScont5 As Decimal = 0
    Dim dScont6 As Decimal = 0
    Dim strCodart As String = NTSCStr(dtrEC!ec_codart)
    Dim nCodtpro As Integer = 0

    Try
      '--------------------------------------
      'Esce se si tratta di riga padre KIT analitico
      If BGESTORDLIST = False Then
        If dtrEC!ec_flkit.ToString = "A" Then Exit Sub
        nCodtpro = NTSCInt(dtrEC!ec_codtpro)
      End If

      '------------------------------------------
      'lo sconto viene calcolato sempre e solo sulle nuove righe, 
      'oppure se o settato l'opzione di rilettura sconti per quantità
      'solo in import_righe_ord o import_righe_nota se settata l'opzione ricalcolo lo sconto
      'solo se cambio l'articolo devo ricalcolare lo sconto, anche se la riga è in modifica
      'oppure se sto duplicando un documento tipo IC/PREV in OF/OP
      'oppure da bnorgsol in righe in modifica quando cambio il fornitore
      If dtrEC.RowState <> DataRowState.Added And bForzaLetturaScontiQta = False Then
        If bInValidazCodart = False AndAlso bInDuplicadoc = False AndAlso bCambioContoGsol = False AndAlso bInEsplodiDiba = False AndAlso bRicalcolaValoriRiga = False Then Return
      End If

      If strCodart.Trim = "" Then Return
      If dttET.Rows(0)!et_tipork.ToString = "H" Then Exit Sub
      If dttET.Rows(0)!et_tipork.ToString = "T" Then Exit Sub

      If BORDINI = False Then
        'veboll: Esce se la riga deriva da ordine (non rileva + il prezzo)
        'devo ricalcolarlo solo se ho cambiato l'articolo o la fase articolo dopo aver acquisito la riga d'ordine
        If NTSCInt(dtrEC!ec_ornum) > 0 Then
          If bInValidazCodart = False AndAlso bInValidazFaseCodart = False Then Return
        End If

        'se sto importanto righe d'ordine e devo mantenere il prezzo indicato nell'ordine è inutile che rilacio la cercaprezzo
        'devo ricalcolare il prezzo solo se 'no carico da prod e opzione abilitata'
        If bInImportRigheOrd = True Then
          If nRicalcScontiOrd <> 0 AndAlso dtrEC!ec_tipork.ToString <> "T" Then
            'devo ricalcolare gli sconti: non devo prendere quello dell'ordine: proseguo
          Else
            Exit Sub
          End If
        End If
        If bInImportRigheNota = True Then
          If nRicalcScontiNota <> 0 AndAlso dtrEC!ec_tipork.ToString <> "T" Then
            'devo ricalcolare gli sconti: non devo prendere quello della nota di prelievo: proseguo
          Else
            Exit Sub
          End If
        End If
      End If

      If CType(oCleComm, CLELBMENU).CercaSconti(strDittaCorrente, strCodart, _
                              NTSCInt(dttET.Rows(0)!et_conto), nClascon, nClscan, "P", _
                              True, nCodtpro, NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dtrEC!ec_quant), dScont1, dScont2, _
                              dScont3, dScont4, dScont5, dScont6, nPromo, NTSCStr(dtrEC!ec_flprznet), _
                              Nothing, Nothing, nScperqta, 0, 9999999999, "", " ", NTSCInt(dttET.Rows(0)!et_listino)) Then
        dtrEC!ec_scont1 = dScont1
        dtrEC!ec_scont2 = dScont2
        dtrEC!ec_scont3 = dScont3
        dtrEC!ec_scont4 = dScont4
        dtrEC!ec_scont5 = dScont5
        dtrEC!ec_scont6 = dScont6
      End If

      If bForzaLetturaScontiQta Then
        nScperqta = bsPrSetsiqta
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


  Public Overridable Sub SettaCondCommerciali(ByVal bSettaPrezzo As Boolean, ByVal bSettaSconti As Boolean, ByVal bSettaProvvigioni As Boolean, _
                                              ByVal dtrEC As DataRow, ByVal nClascon As Integer, ByVal nClscan As Integer, ByVal bDaCodart As Boolean)
    'Racchiude le funzionalità della vecchia SettaPrezzo e SettaSconti, tutto in una unica chiamata.
    Dim nCodTpro As Integer = 0
    Dim strErr As String = ""
    Try
      'Verifica se chiamare i vecchi metodi personalizzati
      If bSettaPrezzo AndAlso (bUsaVecchiaSettaPrezzo OrElse bUsaVecchiaSettaPrezzoPerDitta) Then SettaPrezzo(dtrEC, NTSCStr(dtrEC!ec_codart), strVisMemList1, nVisMemNumList) : bSettaPrezzo = False
      If bSettaSconti AndAlso (bUsaVecchiaSettaSconti OrElse bUsaVecchiaSettaScontiPerDitta) Then SettaSconti(dtrEC, nClascon, nClscan) : bSettaSconti = False
      If bSettaProvvigioni AndAlso (bUsaVecchiaSettaProvvigioni OrElse bUsaVecchiaSettaProvvigioniPerDitta) Then SettaProvvigioni(dtrEC, bDaCodart) : bSettaProvvigioni = False

      'Se non deve fare niente esce subito
      If Not (bSettaPrezzo OrElse bSettaSconti OrElse bSettaProvvigioni) Then Return

      If BGESTORDLIST = False Then nCodTpro = NTSCInt(dtrEC!ec_codtpro)

      'Verifica se deve calcolare prezzi e sconti
      If Not SettaCondCommerciali_TestPreElabora(dtrEC, bSettaPrezzo, bSettaSconti, bSettaProvvigioni) Then Return 'Non devo calcolare nulla

      If Not SettaCondCommerciali_IstanziaNTSCondCommerciali() Then Return 'Prepara la struttura che dovrà contenere i dati da elaborare

      SettaCondCommerciali_PreparaDatiPrezzo(bSettaPrezzo, dtrEC, nCodTpro)
      SettaCondCommerciali_PreparaDatiSconti(bSettaSconti, dtrEC, nClascon, nClscan, nCodTpro)
      SettaCondCommerciali_PreparaDatiProvvigioni(bSettaProvvigioni, dtrEC, bDaCodart, nCodTpro)

      CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)

      'Li salvo in un oggetto locale, così che non vada sovrascritto da successive elaborazioni
      Dim oPrezzi As NTSCondCommerciali.DatiOutputPrezzo = oCondCommerciali.OutputPrezzo
      Dim oSconti As NTSCondCommerciali.DatiOutputSconti = oCondCommerciali.OutputSconti
      Dim oProvv1 As NTSCondCommerciali.DatiOutputProvvigioni = oCondCommerciali.OutputProvvAgente1
      Dim oProvv2 As NTSCondCommerciali.DatiOutputProvvigioni = oCondCommerciali.OutputProvvAgente2
      Dim lAgente1 As Integer = oCondCommerciali.Input.nCodage1
      Dim lAgente2 As Integer = oCondCommerciali.Input.nCodage2

      strErr = oPrezzi.strErr
      If oProvv1.strError <> "" Then strErr += vbCrLf & oProvv1.strError
      If oProvv2.strError <> "" Then strErr += vbCrLf & oProvv2.strError
      If strErr.replace(vbCrLf, "").trim <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))

      SettaCondCommerciali_ScriviPrezzo(bSettaPrezzo, dtrEC, oPrezzi)
      SettaCondCommerciali_ScriviSconti(bSettaSconti, dtrEC, oSconti)
      SettaCondCommerciali_ScriviProvvigioni(bSettaProvvigioni, dtrEC, lAgente1, oProvv1, lAgente2, oProvv2)

      SettaCondCommerciali_TrovaPrezzoListino(bSettaPrezzo, dtrEC, nCodTpro)

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
  Public Overridable Function SettaCondCommerciali_TestPreElabora(ByVal dtrEc As DataRow, ByRef bSettaPrezzo As Boolean, ByRef bSettaSconti As Boolean, _
                                                                  ByRef bSettaProvvigioni As Boolean) As Boolean
    Dim strTipoRkDoc, strTipoRkRiga As String
    Dim strCodart As String
    Try
      strCodart = NTSCStr(dtrEc!ec_codart)
      strTipoRkDoc = NTSCStr(dttET.Rows(0)!et_tipork)
      strTipoRkRiga = NTSCStr(dtrEc!ec_tipork)
      '------------------------------------------
      'esco se  una riga figlia di un kit con prezzo sintetico
      If strCodart.Trim = "" Then Return False
      If BGESTORDLIST = False AndAlso NTSCStr(dtrEc!ec_flkit) = "A" Then Return False

      '------------------------------------------
      'lo prezzo\sconto viene calcolato sempre e solo sulle nuove righe, 
      'oppure se o settato l'opzione di rilettura prezzi\sconti per quantità
      'solo in import_righe_ord o import_righe_nota se settata l'opzione ricalcolo lo sconto
      'solo se cambio l'articolo devo ricalcolare il prezzo\sconto, anche se la riga è in modifica
      'oppure se sto duplicando un documento tipo IC/PREV in OF/OP
      'oppure da bnorgsol in righe in modifica quando cambio il fornitore
      If dtrEc.RowState <> DataRowState.Added AndAlso Not bCalcolaPrezzoScontoProvvAncheSeRigaEvade Then
        If bInValidazCodart = False AndAlso bInDuplicadoc = False AndAlso bCambioContoGsol = False AndAlso _
           bInEsplodiDiba = False AndAlso bRicalcolaValoriRiga = False Then
          If bForzaLetturaPrezziQta = False Then bSettaPrezzo = False 'Non deve ricalcolare i prezzi
          If bForzaLetturaScontiQta = False Then bSettaSconti = False 'Non deve ricalcolare gli sconti
        End If
      End If

      'Per questi tipi documento non vanno ricalcolati gli sconti e le provvigioni
      If strTipoRkDoc = "H" OrElse strTipoRkDoc = "T" Then bSettaSconti = False : bSettaProvvigioni = False

      If BORDINI = False Then
        'veboll: Esce se la riga deriva da ordine (non rileva + il prezzo)
        'devo ricalcolarlo solo se ho cambiato l'articolo o la fase articolo dopo aver acquisito la riga d'ordine
        If NTSCInt(dtrEc!ec_ornum) > 0 AndAlso Not bCalcolaPrezzoScontoProvvAncheSeRigaEvade Then
          If bInValidazCodart = False AndAlso bInValidazFaseCodart = False Then
            bSettaPrezzo = False 'Non deve ricalcolare i prezzi
            bSettaSconti = False 'Non deve ricalcolare gli sconti
            If Not bRideterminaProvvsuRigheOrd Then bSettaProvvigioni = False
          End If
        End If

        'se sto importanto dighe d'ordine e devo mantenere il prezzo indicato nell'ordine è inutile che rilacio la cercaprezzo
        'devo ricalcolare il prezzo solo se 'no carico da prod e opzione abilitata'
        If bInImportRigheOrd Then
          If strTipoRkRiga = "T" Then
            bSettaPrezzo = False 'Non deve ricalcolare i prezzi (gli sconti non vengo mai ricalcolati per i documenti T, vedi sopra)
          Else
            If nRicalcPrezziOrd = 0 Then bSettaPrezzo = False 'Non deve ricalcolare i prezzi
            If nRicalcScontiOrd = 0 Then bSettaSconti = False 'Non deve ricalcolare gli sconti
            If nRicalcProvvOrd = 0 Then bSettaProvvigioni = False 'Non deve ricalcolare le provvigioni
          End If
        End If
        If bInImportRigheNota Then
          If strTipoRkRiga = "T" Then
            bSettaPrezzo = False 'Non deve ricalcolare i prezzi (gli sconti non vengo mai ricalcolati per i documenti T, vedi sopra)
          Else
            If nRicalcPrezziNota = 0 Then bSettaPrezzo = False 'Non deve ricalcolare i prezzi
            If nRicalcScontiNota = 0 Then bSettaSconti = False 'Non deve ricalcolare gli sconti
            If nRicalcProvvNota = 0 Then bSettaProvvigioni = False 'Non deve ricalcolare le provvigioni
          End If
        End If
      End If

      'Valuta se deve ricalcolare le provvigioni 
      '--------------------------------------
      'se sto lavorando su ordlist non gestisco le provvigioni
      If BGESTORDLIST Then bSettaProvvigioni = False
      '------------------------------------------
      '--- Non ricalcola le provvigioni se si tratta di una riga:
      '--- "Omaggi (imponibile)"
      '--- "Sconto merce"
      '--- "Omaggi (imp. + IVA)"
      '------------------------------------------
      Select Case NTSCStr(dtrEc!ec_stasino)
        Case "O", "M", "P" : bSettaProvvigioni = False
      End Select
      'Se non sono settati gli agenti non devo calcolare le provvigioni
      If NTSCInt(dttET.Rows(0)!et_codagen) = 0 AndAlso NTSCInt(dttET.Rows(0)!et_codagen2) = 0 Then bSettaProvvigioni = False

      Return (bSettaPrezzo OrElse bSettaSconti OrElse bSettaProvvigioni) 'Se non devo calcolare sconti, prezzi e provvigioni allora posso anche uscire.
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
  Public Overridable Function SettaCondCommerciali_IstanziaNTSCondCommerciali() As Boolean
    Try
      If oCondCommerciali IsNot Nothing Then
        oCondCommerciali.SvuotaDati()
        Return True
      End If

      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEMGDOCU", "BN__STD.NTSCondCommerciali", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 127791222114531250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCondCommerciali = CType(oTmp, NTSCondCommerciali)
      '------------------------------------------------
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
  Public Overridable Sub SettaCondCommerciali_PreparaDatiPrezzo(ByVal bSettaPrezzo As Boolean, ByVal dtrEc As DataRow, ByVal nCodtpro As Integer)
    Dim strTipoRk As String
    Try
      If Not bSettaPrezzo Then Return

      strTipoRk = NTSCStr(dtrEc!ec_tipork)

      With oCondCommerciali
        .bCalcolaPrezzo = True

        With .Input
          'Le parti comuni per tutte le richieste alla CercaPrezzo() vecchia
          .strDitta = strDittaCorrente
          .strTipoRk = strTipoRk
          .strCodart = NTSCStr(dtrEc!ec_codart)
          .nFase = NTSCInt(dtrEc!ec_fase)
          .dColli = NTSCDec(dtrEc!ec_colli)
          .dQuant = NTSCDec(dtrEc!ec_quant)
          .strTipoval = "P"
          .bConspromo = True
          .dtDatdoc = NTSCDate(dttET.Rows(0)!et_datdoc)
          .nCodvalu = NTSCInt(dttET.Rows(0)!et_valuta)
          .bNoPrelist = False
          .strUnmis = NTSCStr(dtrEc!ec_unmis)
          .strUmp = NTSCStr(dtrEc!ec_ump)
          .bGestionePrezzi = bGestionePrezzi
          .bConsideraPrezziUnmis = True

          '------------------------------------------
          'se riga di impegno/scarico a prod calcolo il prezzo e passo
          If strTipoRk = "Y" OrElse strTipoRk = "U" Then
            .nListino = nListinoScarichi
            .strPrzNet = ""
          Else
            'Valuta se usare il listino di testata o quello della promozione
            If NTSCInt(dtrEc!xxo_listpromo) = 0 Then
              .nListino = NTSCInt(dttET.Rows(0)!et_listino)
            Else
              .nListino = NTSCInt(dtrEc!xxo_listpromo)
            End If
            .strPrzNet = NTSCStr(dtrEc!ec_flprznet)
            .lConto = NTSCInt(dttET.Rows(0)!et_conto)
            .lDestdiv = NTSCInt(dttET.Rows(0)!et_coddest)
            .nCodpromo = nCodtpro

            '------------------------------------------
            'Se è un ordine di prod. terzista e la riga non è nuova legge il cod. lavorazione
            If bTerzista AndAlso dtrEc.RowState <> DataRowState.Added Then
              If strTipoRk = "H" Then
                Dim dtrT() As DataRow = dsShared.Tables("ATTIVIT").Select("at_riga = " & dtrEc!ec_riga.ToString, "at_fase")
                If dtrT.Length > 0 Then .nCodlavo = NTSCInt(dtrT(0)!at_codlavo)
                .nListino = nPeacListinCStd
              ElseIf strTipoRk = "T" Then
                Dim dtrT() As DataRow = dsShared.Tables("LAVCENT").Select("lce_riga = " & dtrEc!ec_riga.ToString, "lce_rigaa")
                If dtrT.Length > 0 Then .nCodlavo = NTSCInt(dtrT(0)!lce_codlavo)
                .nListino = nPeacListinCStd
              End If
            End If

            'Valuta se il prezzo deve essere per unità di misura o se c'è l'opzione di registro di gestione prezzi
            If NTSCStr(dtrEc!ec_umprz) = "S" Then .bPrezziPerUnmis = True
          End If
        End With
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
  Public Overridable Sub SettaCondCommerciali_PreparaDatiSconti(ByVal bSettaSconti As Boolean, ByVal dtrEc As DataRow, ByVal nClascon As Integer, _
                                                                ByVal nClscan As Integer, ByVal nCodtpro As Integer)
    Try
      If Not bSettaSconti Then Return
      If NTSCStr(dtrEc!ec_tipork) = "Y" OrElse NTSCStr(dtrEc!ec_tipork) = "U" Then Return 'Non devo calcolare gli sconti sugli scarichi collegati

      With oCondCommerciali
        .bCalcolaSconti = True
        With .Input
          .strDitta = strDittaCorrente
          .strTipoRk = NTSCStr(dtrEc!ec_tipork)
          .strCodart = NTSCStr(dtrEc!ec_codart)
          .lConto = NTSCInt(dttET.Rows(0)!et_conto)
          .lDestdiv = NTSCInt(dttET.Rows(0)!et_coddest)
          .nCodvalu = NTSCInt(dttET.Rows(0)!et_valuta)
          .nClscar = nClascon
          .nClscan = nClscan
          .strTipoval = "P"
          .bConspromo = True
          .nCodpromo = nCodtpro
          .dtDatdoc = NTSCDate(dttET.Rows(0)!et_datdoc)
          .dColli = NTSCDec(dtrEc!ec_colli)
          .dQuant = NTSCDec(dtrEc!ec_quant)
          .strPrzNet = NTSCStr(dtrEc!ec_flprznet)
          .nListino = NTSCInt(dttET.Rows(0)!et_listino)
          .strUnmis = NTSCStr(dtrEc!ec_unmis)
          .strUmp = NTSCStr(dtrEc!ec_ump)
        End With
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
  Public Overridable Sub SettaCondCommerciali_PreparaDatiProvvigioni(ByVal bSettaProvvigioni As Boolean, ByVal dtrEc As DataRow, ByVal bDaCodart As Boolean, ByVal nCodtpro As Integer)
    Try
      If Not bSettaProvvigioni Then Return

      GetMemDttArti(NTSCStr(dtrEc!ec_codart))

      With oCondCommerciali
        .bCalcolaProvvigioni = True
        With .Input
          .strDitta = strDittaCorrente
          .strTipoRk = NTSCStr(dtrEc!ec_tipork)
          .strCodart = NTSCStr(dtrEc!ec_codart)
          .lConto = NTSCInt(dttET.Rows(0)!et_conto)
          .lDestdiv = NTSCInt(dttET.Rows(0)!et_coddest)
          .nFase = NTSCInt(dtrEc!ec_fase)
          .dColli = NTSCDec(dtrEc!ec_colli)
          .dQuant = NTSCDec(dtrEc!ec_quant)
          .strTipoval = "P"
          .bConspromo = True
          .nCodpromo = nCodtpro
          .dtDatdoc = NTSCDate(dttET.Rows(0)!et_datdoc)
          .nCodvalu = NTSCInt(dttET.Rows(0)!et_valuta)
          .strUnmis = NTSCStr(dtrEc!ec_unmis)
          .strUmp = NTSCStr(dtrEc!ec_ump)

          .nCodage1 = NTSCInt(dttET.Rows(0)!et_codagen)
          .nCodage2 = NTSCInt(dttET.Rows(0)!et_codagen2)
          .nClprar = NTSCInt(dttArti.Rows(0)!ar_claprov)
          .nClpran = nClpran

          .dSconto1 = NTSCDec(dtrEc!ec_scont1)
          .dSconto2 = NTSCDec(dtrEc!ec_scont2)
          .dSconto3 = NTSCDec(dtrEc!ec_scont3)
          .dSconto4 = NTSCDec(dtrEc!ec_scont4)
          .dSconto5 = NTSCDec(dtrEc!ec_scont5)
          .dSconto6 = NTSCDec(dtrEc!ec_scont6)
          .dScontT1 = NTSCDec(dttET.Rows(0)!et_scont1)
          .dScontT2 = NTSCDec(dttET.Rows(0)!et_scont2)
          .dPrezzo = NTSCDec(dtrEc!ec_prezzo)

          If Not (bDaCodart OrElse bRicalcolaValoriRiga) Then
            If .nCodage1 <> 0 AndAlso Not CType(oCleComm, CLELBMENU).AgenteGestitoSCPR(strDittaCorrente, .nCodage1) Then .nCodage1 = 0
            If .nCodage2 <> 0 AndAlso Not CType(oCleComm, CLELBMENU).AgenteGestitoSCPR(strDittaCorrente, .nCodage2) Then .nCodage2 = 0
          End If
        End With
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
  Public Overridable Sub SettaCondCommerciali_ScriviPrezzo(ByVal bSettaPrezzo As Boolean, ByVal dtrEC As DataRow)
    Try
      SettaCondCommerciali_ScriviPrezzo(bSettaPrezzo, dtrEC, oCondCommerciali.OutputPrezzo)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub SettaCondCommerciali_ScriviPrezzo(ByVal bSettaPrezzo As Boolean, ByVal dtrEC As DataRow, ByVal oPrezzi As NTSCondCommerciali.DatiOutputPrezzo)
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bSettaPrezzo, dtrEC, oPrezzi})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return
      End If
      '----------------
      If Not bSettaPrezzo Then Return

      With oPrezzi
        nPrperqta = .nPerqta
        dtrEC!ec_prelist = .dPrelist
        'Prezzo in valuta, calcola anche il prezzo non in valuta
        If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
          dtrEC!ec_prezvalc = .dPrezzo
          dtrEC!ec_prezzo = oCldDocu.ConvImpValuta(strDittaCorrente, True, .dPrezzo, NTSCInt(dttET.Rows(0)!et_valuta), _
                                                   NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
        ElseIf dttET.Rows(0)!et_scorpo.ToString = "S" Then
          dtrEC!ec_preziva = .dPrezzo
          'Scorpora il prezzo
          Dim dImponib As Decimal = 0
          Dim dIva As Decimal = 0
          oCldDocu.Scorporo(.dPrezzo, NTSCInt(dtrEC!ec_codiva), dImponib, dIva, oCldDocu.TrovaNdecSuPrzUn(0))
          dtrEC!ec_prezzo = dImponib
          If Not bPrelistIvato Then
            'Scorporo di prelist
            dImponib = 0
            dIva = 0
            oCldDocu.Scorporo(.dPrelist, NTSCInt(dtrEC!ec_codiva), dImponib, dIva, oCldDocu.TrovaNdecSuPrzUn(0))
            dtrEC!ec_prelist = dImponib
          End If
        Else
          dtrEC!ec_prezzo = .dPrezzo
        End If

        If Not BGESTORDLIST Then dtrEC!ec_flprzmod = "N" 'Imposto che il prezzo non è stato modificato.

        If NTSCStr(dtrEC!ec_tipork) = "Y" OrElse NTSCStr(dtrEC!ec_tipork) = "U" Then
          'sugli scarichi non è mai prezzo netto
          dtrEC!ec_flprznet = "N"
        Else
          dtrEC!ec_flprznet = .strPrzNet
        End If

        If bForzaLetturaPrezziQta Then nPrperqta = bsPrSetsiqta
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
  Public Overridable Sub SettaCondCommerciali_ScriviSconti(ByVal bSettaSconti As Boolean, ByVal dtrEC As DataRow)
    Try
      SettaCondCommerciali_ScriviSconti(bSettaSconti, dtrEC, oCondCommerciali.OutputSconti)
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
  Public Overridable Sub SettaCondCommerciali_ScriviSconti(ByVal bSettaSconti As Boolean, ByVal dtrEC As DataRow, ByVal oSconti As NTSCondCommerciali.DatiOutputSconti)
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bSettaSconti, dtrEC, oSconti})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return
      End If
      '----------------
      If Not bSettaSconti Then Return

      'Imposta gli sconti ritornati dalla funzione
      With oSconti
        dtrEC!ec_scont1 = .dSconto1
        dtrEC!ec_scont2 = .dSconto2
        dtrEC!ec_scont3 = .dSconto3
        dtrEC!ec_scont4 = .dSconto4
        dtrEC!ec_scont5 = .dSconto5
        dtrEC!ec_scont6 = .dSconto6

        If Not BGESTORDLIST Then
          dtrEC!ec_flsc1mod = "N"
          dtrEC!ec_flsc2mod = "N"
          dtrEC!ec_flsc3mod = "N"
          dtrEC!ec_flsc4mod = "N"
          dtrEC!ec_flsc5mod = "N"
          dtrEC!ec_flsc6mod = "N"
        End If

        If bForzaLetturaScontiQta Then
          nScperqta = bsPrSetsiqta
        Else
          nScperqta = .nPerqta
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
  Public Overridable Sub SettaCondCommerciali_ScriviProvvigioni(ByVal bSettaProvvigioni As Boolean, ByVal dtrEC As DataRow)
    Try
      SettaCondCommerciali_ScriviProvvigioni(bSettaProvvigioni, dtrEC, oCondCommerciali.Input.nCodage1, oCondCommerciali.OutputProvvAgente1, oCondCommerciali.Input.nCodage2, oCondCommerciali.OutputProvvAgente2)
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
  Public Overridable Sub SettaCondCommerciali_ScriviProvvigioni(ByVal bSettaProvvigioni As Boolean, ByVal dtrEC As DataRow, _
                                                                ByVal lAgente1 As Integer, ByVal oProvv1 As NTSCondCommerciali.DatiOutputProvvigioni, _
                                                                ByVal lAgente2 As Integer, ByVal oProvv2 As NTSCondCommerciali.DatiOutputProvvigioni)
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bSettaProvvigioni, dtrEC, lAgente1, oProvv1, lAgente2, oProvv2})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then Return
      '----------------
      If Not bSettaProvvigioni Then Return

      'Imposta gli sconti ritornati dalla funzione
      If lAgente1 <> 0 Then 'Ho chiesto di calcolare le provvigioni per il primo agente, quindi scrivo il risultato
        With oProvv1
          If .dVprovv <> 0 Then
            dtrEC!ec_ricimp = "S"
            dtrEC!ec_provv = .dVprovv '* griRighe.Columns(CODART).Text
          Else
            dtrEC!ec_ricimp = "N"
            dtrEC!ec_provv = .dProvv
          End If

          If Not BGESTORDLIST Then dtrEC!ec_flprov1mod = "N"

          '---------------------------------------------
          'segnalo i porblemi rilevati nella CercaProvv
          If .strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", .strError))
        End With
      End If

      If lAgente2 <> 0 Then 'Ho chiesto di calcolare le provvigioni per il secondo agente, quindi scrivo il risultato
        With oProvv2
          If dtrEC!ec_ricimp.ToString = "S" Then ' !!! dipende dal primo agente ...
            dtrEC!ec_provv2 = .dVprovv
          Else
            'SE LA %/€ provvigione 1 è zero e la provvigione 2 è a valore, la imposta e forza ilf lag 'provvigione a valore' = true 
            If NTSCDec(dtrEC!ec_provv) = 0 AndAlso .dVprovv <> 0 Then
              dtrEC!ec_ricimp = "S"
              dtrEC!ec_provv2 = .dVprovv
            Else
              dtrEC!ec_provv2 = .dProvv
            End If
          End If

          If Not BGESTORDLIST Then dtrEC!ec_flprov2mod = "N"
          '---------------------------------------------
          'segnalo i porblemi rilevati nella CercaProvv
          If .strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", .strError))
        End With
      End If

      CalcolaValoreProvvRiga(dtrEC)
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
  Public Overridable Sub SettaCondCommerciali_TrovaPrezzoListino(ByVal bSettaPrezzo As Boolean, ByVal dtrEc As DataRow, ByVal nCodtpro As Integer)
    Try
      If Not bSettaPrezzo Then Return

      Dim strTipoRk As String = NTSCStr(dtrEc!ec_tipork)

      '-----------------------------------------------------------
      'Adesso se necessario legge il listino 1 per memorizzarlo su opzione
      If strVisMemList1 = "S" AndAlso strTipoRk <> "Y" AndAlso strTipoRk <> "U" Then
        If Not SettaCondCommerciali_IstanziaNTSCondCommerciali() Then Return

        With oCondCommerciali
          .bCalcolaPrezzo = True

          With .Input
            'Le parti comuni per tutte le richieste alla CercaPrezzo() vecchia
            .strDitta = strDittaCorrente
            .strTipoRk = strTipoRk
            .strCodart = NTSCStr(dtrEc!ec_codart)
            .nFase = NTSCInt(dtrEc!ec_fase)
            .dColli = NTSCDec(dtrEc!ec_colli)
            .dQuant = NTSCDec(dtrEc!ec_quant)
            .nListino = nVisMemNumList
            .strTipoval = "P"
            .bConspromo = True
            .dtDatdoc = NTSCDate(dttET.Rows(0)!et_datdoc)
            .nCodvalu = NTSCInt(dttET.Rows(0)!et_valuta)
            .bNoPrelist = False
            .strUnmis = NTSCStr(dtrEc!ec_unmis)
            .strUmp = NTSCStr(dtrEc!ec_ump)
            .bGestionePrezzi = bGestionePrezzi
            .bConsideraPrezziUnmis = True
            .nCodpromo = nCodtpro

            'Valuta se il prezzo deve essere per unità di misura o se c'è l'opzione di registro di gestione prezzi
            If NTSCStr(dtrEc!ec_umprz) = "S" Then .bPrezziPerUnmis = True
          End With

          CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)

          With .OutputPrezzo
            If .dPrelist = 0 Then .dPrelist = .dPrezzo
            If dttET.Rows(0)!et_scorpo.ToString = "S" AndAlso (Not bPrelistIvato) Then
              Dim dImponib As Decimal = 0 : Dim dIva As Decimal = 0
              oCldDocu.Scorporo(.dPrelist, NTSCInt(dtrEc!ec_codiva), dImponib, dIva, oCldDocu.TrovaNdecSuPrzUn(0))
              dtrEc!ec_prelist = dImponib
            Else
              dtrEc!ec_prelist = .dPrelist
            End If
          End With
        End With
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



  Public Overridable Sub SettaProvvigioni(ByRef dtrEC As DataRow, Optional ByVal bDaCodart As Boolean = False)
    '--------------------------------------
    'Legge le provvigioni sui 2 agenti indicati in testata
    'Suppone che edCodagen e edSubage siano gi validati
    'Suppone che la colonna prezzo sia gi impostata
    'Suppone che le colonne sconti siano gi impostate
    'Esce se si tratta di riga padre KIT analitico

    Dim dProvv As Decimal
    Dim dTmpvprovv As Decimal
    Dim nPromo As Integer
    Dim nClaprov As Integer = 0
    Dim nAgente As Integer
    Dim i As Integer
    Dim strCodart As String = dtrEC!ec_codart.ToString
    Dim strError As String = ""   'errori passati dalla CercaProvv

    Try
      '--------------------------------------
      'se sto lavorando su ordlist non gestisco le provvigioni
      If BGESTORDLIST Then Return

      '--------------------------------------
      'Esce se si tratta di riga padre KIT analitico
      If dtrEC!ec_flkit.ToString = "A" Then Exit Sub

      '------------------------------------------
      '--- Non ricalcola le provvigioni se si tratta di una riga:
      '--- "Omaggi (imponibile)"
      '--- "Sconto merce"
      '--- "Omaggi (imp. + IVA)"
      '------------------------------------------
      Select Case NTSCStr(dtrEC!ec_stasino)
        Case "O", "M", "P" : Return
      End Select
      '------------------------------------------
      'la provvigione viene calcolata sempre e solo sulle nuove righe, 
      'solo in import_righe_ord o import_righe_nota se settata l'opzione ricalcolo la provvigione
      'NO: la provvigione, se gestita a relazione sconti/provvigioni, deve venir ricalcolata anche su righe in modifica
      'non deve venir variata se collegata ad un ordine, ma se dopo aver acquistito la riga d'ordine si cambia l'articolo,
      'la provvigoine deve venir ricalcolata. ORA OPERA COME IN VB6
      'If dtrEC.RowState <> DataRowState.Added Then
      '  If bInValidazCodart = False And bInDuplicadoc = False Then Return
      'End If

      If strCodart = "" Then Exit Sub
      If dttET.Rows(0)!et_tipork.ToString = "H" Then Exit Sub
      If dttET.Rows(0)!et_tipork.ToString = "T" Then Exit Sub

      If BORDINI = False Then
        'veboll: Esce se la riga deriva da ordine (non rileva + il prezzo)
        'ma solo se non attiva l'opzione che le ridetermina anche in questo caso
        'devo ricalcolarlo solo se ho cambiato l'articolo o la fase articolo dopo aver acquisito la riga d'ordine
        If Not bRideterminaProvvsuRigheOrd Then
          If NTSCInt(dtrEC!ec_ornum) > 0 Then
            If bInValidazCodart = False And bInValidazFaseCodart = False Then Return
          End If
        End If

        If bInImportRigheOrd = True Then
          If nRicalcProvvOrd <> 0 And dtrEC!ec_tipork.ToString <> "T" Then
            'devo ricalcolare la provv: non devo prendere quello dell'ordine: proseguo
          Else
            Exit Sub
          End If
        End If
        If bInImportRigheNota = True Then
          If nRicalcProvvNota <> 0 And dtrEC!ec_tipork.ToString <> "T" Then
            'devo ricalcolare la provv: non devo prendere quello della nota di prelievo: proseguo
          Else
            Exit Sub
          End If
        End If
      End If

      '----------------------------------------
      'ripeto l'operazione per entrambi gli agenti
      For i = 1 To 2
        If i = 1 Then
          nAgente = NTSCInt(dttET.Rows(0)!et_codagen)
        Else
          nAgente = NTSCInt(dttET.Rows(0)!et_codagen2)
        End If
        '---------------------------------------------
        'AGENTE
        If nAgente > 0 Then
          If bDaCodart OrElse bRicalcolaValoriRiga Then
            'Legge in uscita dalla colonna CODART
            dProvv = 0
            GetMemDttArti(strCodart)
            nClaprov = NTSCInt(dttArti.Rows(0)!ar_claprov)

            If CType(oCleComm, CLELBMENU).CercaProvv(strDittaCorrente, strCodart, NTSCInt(dttET.Rows(0)!et_conto), nClaprov, nClpran, _
                                    "P", True, NTSCInt(dtrEC!ec_codtpro), NTSCDate(dttET.Rows(0)!et_datdoc), nAgente, NTSCDec(dtrEC!ec_scont1), _
                                    NTSCDec(dtrEC!ec_scont2), NTSCDec(dtrEC!ec_scont3), NTSCDec(dtrEC!ec_scont4), _
                                    NTSCDec(dtrEC!ec_scont5), NTSCDec(dtrEC!ec_scont6), NTSCDec(dttET.Rows(0)!et_scont1), _
                                    NTSCDec(dttET.Rows(0)!et_scont2), NTSCDec(dtrEC!ec_prezzo), "", NTSCDec(dtrEC!ec_quant), _
                                    dProvv, strError, nPromo, dTmpvprovv) Then
              If i = 1 Then
                If dTmpvprovv <> 0 Then
                  dtrEC!ec_ricimp = "S" ' provv a valore per qta
                  dtrEC!ec_provv = dTmpvprovv '* griRighe.Columns(CODART).Text
                Else
                  dtrEC!ec_ricimp = "N"
                  dtrEC!ec_provv = dProvv
                End If
              Else
                If dtrEC!ec_ricimp.ToString = "S" Then ' !!! dipende dal primo agente ...
                  dtrEC!ec_provv2 = dTmpvprovv
                Else
                  'SE LA %/€ provvigione 1 è zero e la provvigione 2 è a valore, la imposta e forza ilf lag 'provvigione a valore' = true 
                  If NTSCDec(dtrEC!ec_provv) = 0 And dTmpvprovv <> 0 Then
                    dtrEC!ec_ricimp = "S"
                    dtrEC!ec_provv2 = dTmpvprovv
                  Else
                    dtrEC!ec_provv2 = dProvv
                  End If
                End If
              End If    'If i = 1 Then

            End If    '    'If oCldDocu.CercaProvv(strDittaCo...

            '---------------------------------------------
            'segnalo i porblemi rilevati nella CercaProvv
            If strError <> "" Then
              ThrowRemoteEvent(New NTSEventArgs("", strError))
            End If
          Else
            '---------------------------------------------
            'ricalcolo le provvigioni solo se l'agente  gestito a sconti/provvigioni
            If CType(oCleComm, CLELBMENU).AgenteGestitoSCPR(strDittaCorrente, nAgente) Then

              '---------------------------------------------
              'Cerca la classe provvigione articolo e ricalcola le provvigioni
              If GetMemDttArti(strCodart).Rows.Count > 0 Then
                nClaprov = NTSCInt(dttArti.Rows(0)!ar_claprov)
                dProvv = 0
                If CType(oCleComm, CLELBMENU).CercaProvv(strDittaCorrente, strCodart, NTSCInt(dttET.Rows(0)!et_conto), nClaprov, nClpran, _
                                        "P", True, NTSCInt(dtrEC!ec_codtpro), NTSCDate(dttET.Rows(0)!et_datdoc), nAgente, NTSCDec(dtrEC!ec_scont1), _
                                        NTSCDec(dtrEC!ec_scont2), NTSCDec(dtrEC!ec_scont3), NTSCDec(dtrEC!ec_scont4), _
                                        NTSCDec(dtrEC!ec_scont5), NTSCDec(dtrEC!ec_scont6), NTSCDec(dttET.Rows(0)!et_scont1), _
                                        NTSCDec(dttET.Rows(0)!et_scont2), NTSCDec(dtrEC!ec_prezzo), "", NTSCDec(dtrEC!ec_quant), _
                                        dProvv, strError, nPromo, dTmpvprovv) Then
                  If i = 1 Then
                    If dTmpvprovv <> 0 Then
                      dtrEC!ec_ricimp = "S"
                      dtrEC!ec_provv = dTmpvprovv '* griRighe.Columns(CODART).Text
                    Else
                      dtrEC!ec_ricimp = "N"
                      dtrEC!ec_provv = dProvv
                    End If
                  Else
                    If dtrEC!ec_ricimp.ToString = "S" Then ' !!! dipende dal primo agente ...
                      dtrEC!ec_provv2 = dTmpvprovv
                    Else
                      'SE LA %/€ provvigione 1 è zero e la provvigione 2 è a valore, la imposta e forza ilf lag 'provvigione a valore' = true 
                      If NTSCDec(dtrEC!ec_provv) = 0 And dTmpvprovv <> 0 Then
                        dtrEC!ec_ricimp = "S"
                        dtrEC!ec_provv2 = dTmpvprovv
                      Else
                        dtrEC!ec_provv2 = dProvv
                      End If
                    End If
                  End If    'If i = 1 Then

                End If    'If oCldDocu.CercaProvv(strDittaCo...

                '---------------------------------------------
                'segnalo i porblemi rilevati nella CercaProvv
                If strError <> "" Then
                  ThrowRemoteEvent(New NTSEventArgs("", strError))
                End If

              End If    'If oCldDocu.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
            End If    'If oCldDocu.AgenteGestitoSCPR(strDittaCorrente, NTSCint(dttET.Rows(0)!et_codagen)) Then
          End If    'If bDaCodart Then
        End If    'If NTSCint(dttET.Rows(0)!et_codagen) > 0 Then

      Next

      CalcolaValoreProvvRiga(dtrEC)

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

  Public Overridable Sub SettaValoriRiga(ByVal dtrEC As DataRow)
    'usata sia da corpo che da corpoimp
    Dim dQuaeva As Decimal = 0
    Dim dColeva As Decimal = 0
    Dim strFlevas As String = "C"
    Dim dQuant As Decimal = 0
    'Dim dtrT() As DataRow = Nothing
    'Dim i As Integer = 0

    'IN BEORGSOR ec_valore è il valore resido da evadere
    '            e negli ordini da prod contiene quantità * prezzo ancora da evadere (sia ordini interni che esterni)
    '            ec_valoremm è il valore riga originario totale
    '            e negli ordini da prod contiene costo materiali + (quantità * prezzo - costo lavorazione) (sia ordini interni che esterni)
    '            per ordini di prod. esterni tiene conto anche degli sconti (che possono essere inseriti solo a mano denna riga del doc, non caricati da cercasconti)
    'IN BEVEBOLL ec_valore è il valore riga totale
    '            e nei carichi da prod contiene costo materiali + (quantità * prezzo - costo lavorazione) (sia ordini interni che esterni)
    '            per carichi da prod. esterni tiene conto anche degli sconti (che possono essere inseriti solo a mano denna riga del doc, non caricati da cercasconti)


    Try
      Dim strTipoRk As String = dtrEC!ec_tipork.ToString

      If BGESTORDLIST = False Then
        If BORDINI Then
          dQuaeva = NTSCDec(dtrEC!ec_quaeva)
          dColeva = NTSCDec(dtrEC!ec_coleva)
          strFlevas = dtrEC!ec_flevas.ToString
        End If
      End If

      If dtrEC!ec_umprz.ToString <> "S" OrElse _
         strTipoRk = "Y" OrElse strTipoRk = "U" OrElse _
         (dtrEC!ec_unmis.ToString.ToUpper = dtrEC!ec_ump.ToString.ToUpper) Then
        dQuant = (NTSCDec(dtrEC!ec_quant) - dQuaeva)
      Else
        dQuant = (NTSCDec(dtrEC!ec_colli) - dColeva)
      End If

      If strFlevas = "S" Then
        dtrEC!ec_valore = 0
        If dtrEC.Table.Columns.Contains("xxo_valoreiva") Then dtrEC!xxo_valoreiva = 0
      Else
        'se è un ordine di prod terzista il valore riga totale viene calcolato nella 'ValorizzaProduzione'
        If Not (BORDINI = False AndAlso strTipoRk = "T" AndAlso bTerzista) Then
          dtrEC!ec_valore = CalcolaValoreRiga(dtrEC, NTSCDec(dtrEC!ec_prezzo), dQuant)

          'il prezzo senza iva viene calcolato all'uscita del prezzo con iva prima di arrivare qui 
          If dtrEC.Table.Columns.Contains("xxo_valoreiva") Then
            If dttET.Rows(0)!et_scorpo.ToString = "S" Then
              dtrEC!xxo_valoreiva = CalcolaValoreRiga(dtrEC, NTSCDec(dtrEC!ec_preziva), dQuant)
            Else
              dtrEC!xxo_valoreiva = 0
            End If
          End If
          If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then dtrEC!ec_valorev = CalcolaValoreRiga(dtrEC, NTSCDec(dtrEC!ec_prezvalc), dQuant)
        End If    'If Not (BORDINI = False And dtrEC!ec_tipork.ToString = "T" And bTerzista) Then

        'aggiungo, se ordine di prod, il costo dei materiali ancora da evadere, SEMPRE:
        'DAVIDE: NO: deve rimanere senza il valore dei materiali!!!!
        'If dtrEC!ec_tipork.ToString = "H" Then
        '  dtrT = dttECIMP.Select("ec_rigaor = " & dtrEC!ec_riga.ToString)
        '  For i = 0 To dtrT.Length - 1
        '    dtrEC!ec_valore = NTSCDec(dtrEC!ec_valore) + NTSCDec(dtrT(i)!ec_valore)
        '  Next
        'End If    'If dtrEC!ec_tipork.ToString = "H" Then

      End If    'If strFlevas = "S" Then

      CalcolaPrezzoNetto(dtrEC)

      'se è un ordine di prod terzista il valore riga totale viene calcolato nella 'ValorizzaProduzione'
      'gli scarichi collegato non gestiscono prezzi per unità di misura
      If BGESTORDLIST = False Then
        If BORDINI Then
          If Not (strTipoRk = "H" AndAlso bTerzista) Then
            If dtrEC!ec_umprz.ToString <> "S" OrElse _
               strTipoRk = "Y" OrElse strTipoRk = "U" OrElse _
               (dtrEC!ec_unmis.ToString.ToUpper = dtrEC!ec_ump.ToString.ToUpper) Then
              dtrEC!ec_valoremm = CalcolaValoreRiga(dtrEC, NTSCDec(dtrEC!ec_prezzo), NTSCDec(dtrEC!ec_quant))
            Else
              dtrEC!ec_valoremm = CalcolaValoreRiga(dtrEC, NTSCDec(dtrEC!ec_prezzo), NTSCDec(dtrEC!ec_colli))
            End If
          End If    'If Not (dttET.Rows(0)!et_tipork.ToString = "H" And bTerzista) Then
        End If    'If BORDINI Then
      End If    'If bGestOrdlist = False Then

      If (strTipoRk = "H" OrElse strTipoRk = "T") AndAlso Not bTerzista Then ValorizzaProduzione(dtrEC)

      CalcolaValoreProvvRiga(dtrEC)

      'Se è cambiato il valore della riga allora devo riconsiderare le promozioni applicate
      ModificaRigaImpostaColoriPromozioni(dtrEC)
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
  Public Overridable Sub CalcolaPrezzoNetto(ByVal dtrEc As DataRow)
    Try
      'Calcola il prezzo netto di riga
      With dtrEC
        'il prezzo netto non va calcolato negli scarichi.
        If NTSCStr(!ec_tipork) <> "U" AndAlso NTSCStr(!ec_tipork) <> "Y" Then
          If NTSCDec(!ec_quant) = 0 Then
            Dim dScontV As Decimal = 0
            If NTSCDec(!ec_scontv) <> 0 And NTSCStr(dttET.Rows(0)!et_scorpo) = "S" Then
              oCldDocu.Scorporo(NTSCDec(!ec_scontv), NTSCInt(!ec_codiva), dScontV, 0)
            Else
              dScontV = NTSCDec(!ec_scontv)
            End If

            !xxo_prznet = ArrDbl(ArrDbl(NTSCDec(!ec_prezzo) * (100 - NTSCDec(!ec_scont1)) / 100 * (100 - NTSCDec(!ec_scont2)) / 100 * _
                              (100 - NTSCDec(!ec_scont3)) / 100 * (100 - NTSCDec(!ec_scont4)) / 100 * (100 - NTSCDec(!ec_scont5)) / 100 * _
                              (100 - NTSCDec(!ec_scont6)) / 100 * (100 - NTSCDec(!ec_scontp)) / 100 - dScontV, oCldDocu.TrovaNdec(0)) * _
                              (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, oCldDocu.TrovaNdec(0))
          Else
            !xxo_prznet = NTSCDec(!ec_valore) / NTSCDec(!ec_quant) * NTSCDec(!ec_perqta)
          End If
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
#End Region

  Public Overridable Function CalcolaValoreRigaPerProvv(ByRef dtrEC As DataRow) As Decimal
    CalcolaValoreRigaPerProvv = 0

    Dim dScont1 As Decimal = 0
    Dim dScont2 As Decimal = 0
    Dim dScopag As Decimal = 0

    Dim dValoreOut As Decimal = 0 'valore di riga ricalcolato da utilizzare per il calolo delle provvigioni

    Try
      'nelle proposte d'ordine non sono gestite le provvigioni
      If BGESTORDLIST Then Return 0

      'valore di partenza
      If BORDINI Then
        dValoreOut = NTSCDec(dtrEC!ec_valoremm)
      Else
        dValoreOut = NTSCDec(dtrEC!ec_valore)
      End If

      dScont1 = NTSCDec(dttET.Rows(0)!et_scont1)
      dScont2 = NTSCDec(dttET.Rows(0)!et_scont2)
      dScopag = NTSCDec(dttET.Rows(0)!et_scopag)

      'GESTIONE DI SCONTI DI TESTATA: IGNORO GLI SCONTI COME NELLA ROUTINE 'SettaValoriRiga'
      'negli stessi casi in cui azzero gli sconti di testata le righe 'incriminate' hanno dei
      'controlli per non avere sconti di riga valorizzati
      If BORDINI = False Then
        If NTSCInt(dtrEC!ec_acnum) > 0 Then
          'non fa nulla!
          'Per gli articoli D che rappresentano il riferimento ai doc. di acconto
          'non tiene conto degli sconti di piede/testata
          dScont1 = 0
          dScont2 = 0
          dScopag = 0
        End If
      End If

      'sulle righe di scarico a prod NON CI SONO SCONTI SUI MATERIALI!!! visto che li mettiamo noi
      If dtrEC!ec_tipork.ToString = "Y" OrElse dtrEC!ec_tipork.ToString = "U" Then
        dScont1 = 0
        dScont2 = 0
        dScopag = 0
      End If

      '--------------------------
      'ora calcolo il valore di riga, andando a togliere gli sconti all'indietro
      'se non devo considerare lo sconto pagamento, aggiungo a dValoreOut l'importo di sconto che avevo tolto
      'se nel valore è considerato lo sconto pagamento ed ora lo devo togliere
      If dScopag <> 0 Then
        If dScopag = 100 Then
          dValoreOut = 0
        Else
          If oApp.oGvar.bCalcImponibProvvIgnoraScontoPagam Then dValoreOut = dValoreOut * 100 / (100 - dScopag)
        End If
      End If
      'stessa cosa per sconto 1 e 2 di testata
      If dScont2 <> 0 Then
        If dScont2 = 100 Then
          dValoreOut = 0
        Else
          If oApp.oGvar.bCalcImponibProvvIgnoraScont2Testa Then dValoreOut = dValoreOut * 100 / (100 - dScont2)
        End If
      End If
      If dScont1 <> 0 Then
        If dScont1 = 100 Then
          dValoreOut = 0
        Else
          If oApp.oGvar.bCalcImponibProvvIgnoraScont1Testa Then dValoreOut = dValoreOut * 100 / (100 - dScont1)
        End If
      End If

      'ora gli sconti di riga: sicuramente erano stati considerati nella determinazione del valore.
      'lo devo togliere se l'opzione di registo avvisa di non considerarli
      If NTSCDec(dtrEC!ec_scont6) <> 0 Then
        If NTSCDec(dtrEC!ec_scont6) = 100 Then
          dValoreOut = 0
        Else
          If oApp.oGvar.bCalcImponibProvvIgnoraScont6 Then dValoreOut = dValoreOut * 100 / (100 - NTSCDec(dtrEC!ec_scont6))
        End If
      End If
      If NTSCDec(dtrEC!ec_scont5) <> 0 Then
        If NTSCDec(dtrEC!ec_scont5) = 100 Then
          dValoreOut = 0
        Else
          If oApp.oGvar.bCalcImponibProvvIgnoraScont5 Then dValoreOut = dValoreOut * 100 / (100 - NTSCDec(dtrEC!ec_scont5))
        End If
      End If
      If NTSCDec(dtrEC!ec_scont4) <> 0 Then
        If NTSCDec(dtrEC!ec_scont4) = 100 Then
          dValoreOut = 0
        Else
          If oApp.oGvar.bCalcImponibProvvIgnoraScont4 Then dValoreOut = dValoreOut * 100 / (100 - NTSCDec(dtrEC!ec_scont4))
        End If
      End If
      If NTSCDec(dtrEC!ec_scont3) <> 0 Then
        If NTSCDec(dtrEC!ec_scont3) = 100 Then
          dValoreOut = 0
        Else
          If oApp.oGvar.bCalcImponibProvvIgnoraScont3 Then dValoreOut = dValoreOut * 100 / (100 - NTSCDec(dtrEC!ec_scont3))
        End If
      End If
      If NTSCDec(dtrEC!ec_scont2) <> 0 Then
        If NTSCDec(dtrEC!ec_scont2) = 100 Then
          dValoreOut = 0
        Else
          If oApp.oGvar.bCalcImponibProvvIgnoraScont2 Then dValoreOut = dValoreOut * 100 / (100 - NTSCDec(dtrEC!ec_scont2))
        End If
      End If
      If NTSCDec(dtrEC!ec_scont1) <> 0 Then
        If NTSCDec(dtrEC!ec_scont1) = 100 Then
          dValoreOut = 0
        Else
          If oApp.oGvar.bCalcImponibProvvIgnoraScont1 Then dValoreOut = dValoreOut * 100 / (100 - NTSCDec(dtrEC!ec_scont1))
        End If
      End If

      Return dValoreOut   'NON ARROTONDARE!!!!!

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

  '---------------------- 

  Public Overridable Function ScriviRigheKit(ByRef dtrEC As DataRow) As Boolean
    Dim dttKit As New DataTable
    Dim i As Integer = 0
    Try
      If BGESTORDLIST = True Then Return True 'non eseguo se sto lavorando su ordlist

      If Not oCldDocu.GetArticoFigliKit(strDittaCorrente, dtrEC!ec_codart.ToString, dttKit) Then Return False
      If dttKit.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128552539613529952, "Per l'articolo kit '|" & dtrEC!ec_codart.ToString & "|' non sono stati trovati articoli componenti.")))
        Return True
      Else
        bInImportKit = True
        '------------------------------------------------------------------------------------------------------------
        '--- Se l'articolo padre possiede il flag Kit 'Sintetico' (ARTICO.ar_tipokit = 'S')
        '--- e se attiva l'opzione di registro (relativa al programma chiamante) NonStampareFigliKitSintetici
        '--- sulle righe relative ai figli del Kit, il flag Stampa riga viene forzato a 'No'
        '------------------------------------------------------------------------------------------------------------
        bSetStasinoSuRigheKit = True
        If (bNonStampareFigliKitSintetici = True) And (NTSCStr(dtrEC!ec_flkit) = "S") Then bSetStasinoSuRigheKit = False
        '------------------------------------------------------------------------------------------------------------
        For i = 0 To dttKit.Rows.Count - 1
          ScriviRigaKit(dttKit.Rows(i), dtrEC)
        Next
      End If
      dttKit.Clear()

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
      bInImportKit = False
    End Try
  End Function
  Public Overridable Function ScriviRigaKit(ByRef dtrKit As DataRow, ByRef dtrPadre As DataRow) As Boolean
    'data la riga di ARTKIT in input la scrive in dttEC
    Dim strErrore As String = ""
    Dim dQuant As Decimal = 0
    Try
      If BGESTORDLIST Then Return True

      dttEC.Rows.Add(dttEC.NewRow)
      With dttEC.Rows(dttEC.Rows.Count - 1)
        'forzo la CorpoOnAddNewRow
        !codditt = "."
        !codditt = strDittaCorrente
        !ec_codart = dtrKit!ak_codfigli.ToString
        If BORDINI = True Then !ec_datcons = dtrPadre!ec_datcons
        !ec_flkit = IIf(dtrPadre!ec_flkit.ToString = "A", "B", "T").ToString
        !ec_ktriga = dtrPadre!ec_riga
        !ec_commeca = dtrPadre!ec_commeca
        !ec_subcommeca = dtrPadre!ec_subcommeca
        !ec_magaz = dtrPadre!ec_magaz

        If dtrKit!ak_codfigli.ToString = "D" Then !ec_descr = NTSCStr(dtrKit!ak_desfigli)

        'con prezzo analitico eredito l'eventuale codice promozione dalla riga del padre
        If dtrPadre!ec_flkit.ToString = "A" Then
          !ec_codtpro = NTSCInt(dtrPadre!ec_codtpro)
        End If

        !ec_unmis = NTSCStr(dtrKit!ak_unmis)
        If !ec_ump.ToString.ToUpper <> NTSCStr(dtrKit!ak_ump).ToUpper Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128553323999837500, "Unità di misura principale '|" & NTSCStr(dtrKit!ak_ump) & "|' indicata nel componente kit '|" & dtrKit!ak_codfigli.ToString & "|' differisce da quella indicata sulla sua anagrafica.")))
          .Delete()
          Return False
        End If
        !ec_quant = NTSCDec(dtrKit!ak_quant) * NTSCDec(dtrPadre!ec_quant)
        '------------------------------------------------------------------------------------------------------------
        '--- Deciso da Davide Procucci (Riferimento quesito N°108413 (NTS INFORMATICA MOLISE SRL):
        '--- Se, in Anagrafica Articoli, il campo "Colli", del KIT (tabella ARTKIT) è a zero
        '--- NON ricalcola, all'indietro, i colli sulle righe
        '------------------------------------------------------------------------------------------------------------
        If NTSCDec(dtrKit!ak_colli) <> 0 Then
          If CType(oCleComm, CLELBMENU).ConvQuantUM(strDittaCorrente, !ec_codart.ToString, !ec_ump.ToString, _
                          NTSCDec(!ec_quant), NTSCDec(!ec_misura1), NTSCDec(!ec_misura2), _
                          NTSCDec(!ec_misura3), !ec_unmis.ToString, dQuant, strErrore, oApp.NDecQta) Then
            bInValidazQuant = True      'altrimenti al cambio dei colli viene ricalcolata la quantità
            !ec_colli = dQuant
            bInValidazQuant = False
            If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        'kit con prezzo sintetico: azzero listini/sconti/provvigioni
        If dtrPadre!ec_flkit.ToString = "S" Then
          !ec_prezvalc = 0
          !ec_preziva = 0
          !ec_prezzo = 0
          !ec_prelist = 0
          !ec_flprznet = "N"
          !ec_scont1 = 0
          !ec_scont2 = 0
          !ec_scont3 = 0
          !ec_scont4 = 0
          !ec_scont5 = 0
          !ec_scont6 = 0
          !ec_provv = 0
          !ec_provv2 = 0
          !ec_vprovv = 0
          !ec_vprovv2 = 0
          !ec_codtpro = 0
          !ec_ricimp = "N"  'no provv a valore
          If bSetStasinoSuRigheKit = False Then !ec_stasino = "N"
        End If

        If bDescrAggKitSuNote Then !ec_note = NTSCStr(dtrKit!ak_desfigli)
        If bNoColliSuFigliKit Then
          bInValidazQuant = True      'altrimenti al cambio dei colli viene ricalcolata la quantità
          !ec_colli = 0
          bInValidazQuant = False
        End If

        If Not CorpoCheckRiga(3, dttEC.Rows(dttEC.Rows.Count - 1)) Then
          .Delete()
          Return False
        End If

        If !ec_umprz.ToString = "S" And dtrPadre!ec_flkit.ToString <> "S" Then   'ricalcolo il prezzo se unità di misura su colli
          'devo farlo perchè altrimenti il prezzo viene ricalcolato solo al cambio della quantità
          'ma in questo caso la qta non cambia perchè 'bInValidazQuant = True'
          SettaCondCommerciali(True, False, False, dttEC.Rows(dttEC.Rows.Count - 1), 0, 0, False)
          SettaValoriRiga(dttEC.Rows(dttEC.Rows.Count - 1))
        End If

      End With

      RecordSalva(dttEC.Rows.Count - 1, False, Nothing)

      Return True

    Catch ex As Exception
      bInValidazQuant = False
      dttEC.Rows(dttEC.Rows.Count - 1).Delete()
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      dttEC.AcceptChanges()
    End Try
  End Function

  '---------------------- 

  Public Overridable Function GestisciDettaglioTCO(ByVal nEvento As Integer, ByVal nRiga As Integer, ByVal bImpegni As Boolean) As Boolean
    'SE SONO QUI' L'ARTICOLO DEVE ESSERE STATO INDICATO!!!!
    'nEvento: 1: cambio dell'articolo sulla riga (nuova o salvata): prima cancello il precedente dettaglio (potrei aver cambiato articolo prima di salvare la riga)
    '         3: ripristino riga mai salvata: cancello dettaglio TCO
    '         4: cancello riga salvata: cancello dettaglio TCO
    Try
      If bModTCO = False Then Return True

      Select Case nEvento
        Case 1      'aftercolupdate_corpo
          If Not CancellaRigaTCO(nRiga, bImpegni) Then Return False
          Return AggiungiRigaTCO(nRiga, bImpegni)
        Case 3      'RecordRipristina
          Return CancellaRigaTCO(nRiga, bImpegni)
        Case 4      'CorpoAfterDelete
          Return CancellaRigaTCO(nRiga, bImpegni)
      End Select

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
  Public Overridable Function AggiungiRigaTCO(ByVal nRiga As Integer, ByVal bImpegni As Boolean) As Boolean
    'al cambio di articolo aggiunge una riga per il dettaglio TCO
    Try
      If bModTCO = False Then Return True

      If dttArti.Rows.Count = 0 Then Return True
      If NTSCInt(dttArti.Rows(0)!ar_codtagl) = 0 Then Return True 'se l'articolo non è a TCO esco

      If bImpegni Then
        dttECIMPTC.Rows.Add(dttECIMPTC.NewRow)
        With dttECIMPTC.Rows(dttECIMPTC.Rows.Count - 1)
          'forzo la CorpoOnAddNewRow
          !codditt = "."
          !codditt = strDittaCorrente
          !ec_tipork = IIf(dttET.Rows(0)!et_tipork.ToString = "H", "Y", "U").ToString
          !ec_anno = NTSCInt(dttET.Rows(0)!et_anno)
          !ec_serie = dttET.Rows(0)!et_serie
          !ec_numdoc = NTSCInt(dttET.Rows(0)!et_numdoc)
          !ec_riga = nRiga
        End With
        dttECIMPTC.AcceptChanges()
      Else
        dttECTC.Rows.Add(dttECTC.NewRow)
        With dttECTC.Rows(dttECTC.Rows.Count - 1)
          'forzo la CorpoOnAddNewRow
          !codditt = "."
          !codditt = strDittaCorrente
          !ec_tipork = dttET.Rows(0)!et_tipork
          !ec_anno = NTSCInt(dttET.Rows(0)!et_anno)
          !ec_serie = dttET.Rows(0)!et_serie
          !ec_numdoc = NTSCInt(dttET.Rows(0)!et_numdoc)
          !ec_riga = nRiga
        End With
        dttECTC.AcceptChanges()
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
  Public Overridable Function CancellaRigaTCO(ByVal nRiga As Integer, ByVal bImpegni As Boolean) As Boolean
    'al ripristino di una nuova riga, oppure alla cancellazione di una esistente, oppure al cambio dell'articolo 
    'cancello la vecchia riga del dettaglio TCO
    Dim dtrT() As DataRow = Nothing
    Try
      If bModTCO = False Then Return True

      If bImpegni Then
        dtrT = dttECIMPTC.Select("ec_riga = " & nRiga)
        If dtrT.Length > 0 Then dtrT(0).Delete()
        dttECIMPTC.AcceptChanges()
      Else
        dtrT = dttECTC.Select("ec_riga = " & nRiga)
        If dtrT.Length > 0 Then dtrT(0).Delete()
        dttECTC.AcceptChanges()
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


  Public Overridable Function GetDisponibilitaCommessa(ByVal strCodart As String, ByVal nFase As Integer, _
                                            ByVal nMagaz As Integer, ByVal lRilevaDisponibCodCommessa As Integer) As Boolean
    Dim dttTmp As New DataTable
    Dim strUnmis As String = ""
    Dim dEsist As Decimal = 0
    Dim dPrenot As Decimal = 0
    Dim dImpeg As Decimal = 0
    Dim dOrdin As Decimal = 0
    Dim strMsg As String = ""
    Try
      oCldDocu.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128577271256718750, _
                        "Non è stato possibile determinare la disponibilità per commessa in quanto l'articolo '|" & _
                        strCodart & "|' non esiste nell'anagrafica articoli")))
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!ar_gescomm) <> "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128577272036093750, _
                        "Impossibile visualizzare la disponibilità per la commessa |" & _
                        lRilevaDisponibCodCommessa.ToString & "|: articolo '|" & strCodart & _
                        "|' non gestito a commessa su Ordini/Impegni/Magazzino.")))
        Return False
      End If

      strUnmis = NTSCStr(dttTmp.Rows(0)!ar_unmis)
      dttTmp.Clear()

      'Legge la disponibilità per commessa
      dEsist = CType(oCleComm, CLELBMENU).TrovaEsistDaXpro(strDittaCorrente, strCodart, nMagaz, _
                                          lRilevaDisponibCodCommessa, 0, nFase, " ", dOrdin, dImpeg, dPrenot)
      strMsg = oApp.Tr(Me, 128577280468593750, _
                "Articolo |'" & strCodart & "'|, magazzino |" & nMagaz.ToString & "|, commessa |" & _
                lRilevaDisponibCodCommessa & "|:" & vbCrLf & vbCrLf & "Disponibilità: |" & _
                (dEsist - dOrdin - dImpeg).ToString(oApp.FormatQta) & " " & strUnmis & "|" & vbCrLf & _
                "Disponibilità netta: |" & (dEsist - dPrenot).ToString(oApp.FormatQta) & " " & strUnmis & "|")

      If Not BORDINI Then
        strMsg += oApp.Tr(Me, 128689595183437500, vbCrLf & "Ordinato: |" & _
                dOrdin.ToString(oApp.FormatQta) & " " & strUnmis & "|" & vbCrLf & _
                "Impegnato: |" & dOrdin.ToString(oApp.FormatQta) & " " & strUnmis & "|")
      End If
      ThrowRemoteEvent(New NTSEventArgs("", strMsg))
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
    End Try
  End Function

  Public Overridable Function RicercaImportDaArtest(ByVal strCodArtf As String, ByRef strArtImport As String) As Boolean
    Dim dttArtest As New DataTable
    Dim strTmp As String = ""
    Dim strCodArtImport As String = ""
    Dim strCodartfTmp As String = ""
    Dim strFornTmp As String = ""
    Dim strCodmarcTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bCaclSelArticoli = False
      '--------------------------------------------------------------------------------------------------------------
      If Not oCldDocu.GetArticoFromArttest(strDittaCorrente, strCodArtf, dttArtest) Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non esistono records in ARTEST, esce
      '--------------------------------------------------------------------------------------------------------------
      If dttArtest.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Se l'utente non è autorizzato alla creazione di articoli, dà il messaggio ed esce
      '--------------------------------------------------------------------------------------------------------------
      If CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "OPZIONIUT", ".", ".", "OperatoreAbilCreazArticoli", "-1", " ", "-1")) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129949556238458210, "Attenzione!" & vbCrLf & _
          "L'utente '" & oApp.User.Nome & "'" & vbCrLf & _
          "non è abilitato all'mportazione/creazione degli articoli da catalogo fornitori." & vbCrLf & _
          "Operazione annullata.")))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If dttArtest.Rows.Count = 1 Then
        '------------------------------------------------------------------------------------------------------------
        '--- Se la ricerca su ARTEST restituisce un solo record, chiede subito se importarlo in ARTICO
        '------------------------------------------------------------------------------------------------------------
        Dim evnt As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128578259597343750, _
          "Vuoi importare l'articolo '|" & strCodArtf & "|' dal catalogo fornitori in anagrafica articoli?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
        strCodartfTmp = NTSCStr(dttArtest.Rows(0)!ae_codartf)
        strFornTmp = NTSCStr(dttArtest.Rows(0)!ae_forn)
        strCodmarcTmp = NTSCStr(dttArtest.Rows(0)!ae_codmarc)
      Else
        '------------------------------------------------------------------------------------------------------------
        '--- Se la ricerca su ARTEST restituisce più di records,
        '--- chiede di selezionarne uno attraverso una griglia modale
        '------------------------------------------------------------------------------------------------------------
        ThrowRemoteEvent(New NTSEventArgs("SelCatForn" & strCodArtf, ""))
        '------------------------------------------------------------------------------------------------------------
        '--- Se annullata la selezione da ARTEST, esce
        '------------------------------------------------------------------------------------------------------------
        bCaclSelArticoli = True
        If bCaclAnnullato = True Then Return False
        strCodartfTmp = strCodArtf
        strFornTmp = lCaclForn.ToString
        strCodmarcTmp = nCaclCodmarc.ToString
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Effettuo il controllo prima di importare
      '--------------------------------------------------------------------------------------------------------------
      If Not CType(oCleComm, CLELBMENU).TestPreImportaArticoDaArtest(strDittaCorrente, strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", strTmp))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Importo l’articolo dal record di ARTEST selezionato
      '--------------------------------------------------------------------------------------------------------------
      If Not CType(oCleComm, CLELBMENU).CreaArticoDaArtest(strDittaCorrente, strCodartfTmp, strFornTmp, _
        strCodmarcTmp, strCodArtImport, strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", strTmp))
        strArtImport = ""
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      strArtImport = strCodArtImport
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttArtest.Clear()
      dttArtest.Dispose()
    End Try
  End Function

  '---------------------- 

#Region "BeforeColUpdate_CORPO"
  Public Overridable Sub BeforeColUpdate_CORPO(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If bInUnload Then Return
      If bNonValidare Then Return

      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'se non ho compilato il numero di riga compilo i campi di default
      If BGESTORDLIST Then
        If e.Column.ColumnName <> "ec_riga" Then
          If e.Row!ec_riga.ToString = "0" Then CorpoOnAddNewRow(sender, e)
        End If
      Else
        If e.Column.ColumnName <> "ec_ultagg" Then
          If NTSCDate(e.Row!ec_ultagg) = New Date(1900, 1, 1) Then CorpoOnAddNewRow(sender, e)
        End If
      End If

      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BeforeColUpdate_CORPO_" & e.Column.ColumnName.ToLower
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_riga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Try
      dtrT1 = e.Row.Table.Select("ec_riga = " & NTSCInt(e.ProposedValue).ToString)
      If dtrT1.Length = 0 Then
        If e.Row.Table.TableName = "CORPO" Then
          dtrT1 = dttEC.Select("ec_riga = " & NTSCInt(e.ProposedValue).ToString)
        Else
          dtrT1 = dttECIMP.Select("ec_riga = " & NTSCInt(e.ProposedValue).ToString)
        End If
      End If
      If dtrT1.Length > 0 Or NTSCInt(e.ProposedValue) = 0 Then
        If bInOnAddNewRowImp = False AndAlso bInCalcoloPromozioni = False Then
          'eseguo il test solo se non sono in inserimento riga di impegno/scarico in automatico al salva dela riga di carico
          'questo perchè in questa situazione prima viene assegnato il numero di riga della riga di carico, poi viene rettificata con quella della riga di scarico
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129774058877079536, "Numero di riga già utilizzato o uguale a 0")))
          If e.Row.RowState = DataRowState.Added Then
            If e.Row.Table.TableName = "CORPO" Then
              lCrighe += nIncremContatoreRiga
              e.ProposedValue = lCrighe
            Else
              lCrigheYT += nIncremContatoreRiga
              e.ProposedValue = lCrigheYT
            End If
          Else
            e.ProposedValue = e.Row!ec_riga
          End If
        End If
      Else
        If NTSCInt(e.Row!ec_riga) <> 0 AndAlso bModTCO AndAlso NTSCStr(e.Row!ec_codart).Trim <> "" AndAlso _
           NTSCInt(e.Row!xxo_codtagl) <> 0 AndAlso bInCalcoloPromozioni = False Then
          dtrT = dttECTC.Select("ec_riga = " & NTSCInt(e.Row!ec_riga).ToString)
          If dtrT.Length > 0 Then dtrT(0)!ec_riga = NTSCInt(e.ProposedValue)
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strArtSost As String = ""
    Dim strTmp As String = ""
    Dim nFase1 As Integer = 0
    Dim nOrigine As Integer = 0
    Dim dttTmp As New DataTable
    Dim dtrRow(), dtrSelRow As DataRow

    Try
      '-----------------------------------------------
      nFaseArticoloDaBarcode = -1
      strUnMisDaBarcode = ""
      dQuantDaBarcode = -1
      dColliDaBarcode = -1
      strLottoXDaAnalotti = ""
      strEAN128Lotto = ""
      strEAN128Matric = ""
      strEAN128DtProd = ""
      strEAN128DtScad = ""
      '-----------------------------------------------
      'controllo che il cod articolo esista
      'per ora commentata la riga sottostante, altrimenti se si indica come articolo '.' viene tradotto in 'D'. se si verifificherà il loop invinito la reinseriremo
      'non sono riuscito a capire le cause del perchè era stata messa.
      'If NTSCStr(e.ProposedValue) = "." Then e.ProposedValue = "D" 'altrimenti entra in un loop infinito: l'articolo 'D' deve esistere 
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then e.ProposedValue = e.ProposedValue.ToString.ToUpper
      If e.ProposedValue.ToString.Trim <> "" Then
        If NTSCStr(e.ProposedValue).Substring(NTSCStr(e.ProposedValue).Length - 1) = " " Then
          e.ProposedValue = NTSCStr(e.ProposedValue).TrimEnd
        End If
      End If

      strOldArt = NTSCStr(e.Row!ec_codart)
      dttArti.Clear()
      'dttArti.Clear(): in questo modo forzo la lettura dell'articolo su artico 
      '(diversamente se avevo digitato MP6 bloccato, poi sono andato in artico ed ho tolto il blocco, eidigitando mp6 lo dava ancora bloccato
      strTmp = NTSCStr(e.ProposedValue)
      If Not BeforeColUpdateCodart_TrovaArticolo(strTmp, nOrigine) Then
        e.ProposedValue = e.Row!ec_codart
        Return False
      Else
        If strTmp <> NTSCStr(e.ProposedValue) Then
          e.ProposedValue = strTmp
          ' If nOrigine = 1 Or nOrigine = 4 Then e.Row!ec_quant = 1
        End If
      End If

      '-----------------------------------------------
      'se necessario propongo l'articolo sostitutivo
      If dttArti.Rows.Count > 0 Then
        If dttArti.Rows(0)!ar_sostit.ToString.ToUpper.Trim <> "" AndAlso _
           bInImportRigheOff = False AndAlso bInImportRigheOrd = False AndAlso bInDuplicadoc = False AndAlso _
           e.Row!ec_tipork.ToString <> "Y" AndAlso e.Row!ec_tipork.ToString <> "U" AndAlso bInImportKit = False AndAlso _
           bInImportConai = False AndAlso bInIncollaRiga = False AndAlso bInImportRigheNota = False AndAlso bDocDaRetail = False Then

          If bProponiArtSostitutivo OrElse nProponiArtSostitutivo = -1 Then
            Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 127791959138593750, _
                                       "L'articolo |" & e.ProposedValue.ToString & _
                                       "| è stato sostituito con l'articolo |" & _
                                       dttArti.Rows(0)!ar_sostit.ToString & _
                                       "|. Utilizzare l'articolo sostitutivo?"))
            ThrowRemoteEvent(msg)
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              strArtSost = dttArti.Rows(0)!ar_sostit.ToString.ToUpper.Trim
              If GetMemDttArti(strArtSost).Rows.Count > 0 Then
                e.ProposedValue = strArtSost
              Else
                GetMemDttArti(e.ProposedValue.ToString)
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127826744434687500, "Codice Articolo sostitutivo non presente in archivio. Verrà mantenuto l'articolo inserito manualmente")))
              End If
            End If
          ElseIf nProponiArtSostitutivo = 1 Then
            strArtSost = dttArti.Rows(0)!ar_sostit.ToString.ToUpper.Trim
            If GetMemDttArti(strArtSost).Rows.Count > 0 Then
              e.ProposedValue = strArtSost
            Else
              GetMemDttArti(e.ProposedValue.ToString)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129634267571764059, "Codice Articolo sostitutivo non presente in archivio. Verrà mantenuto l'articolo inserito manualmente")))
            End If
          End If
        End If
      End If

      '-----------------------------------------------
      'Non movimenta articoli bloccati
      If dttArti.Rows(0)!ar_blocco.ToString = "S" AndAlso Not bInImportRigheOrd AndAlso Not bDocDaRetail AndAlso _
        Not bDocDaFattChiamate Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222092968750, "Impossibile movimentare articoli bloccati ") & "(" & NTSCStr(e.ProposedValue) & ")"))
        e.ProposedValue = e.Row!ec_codart
        Return False
      End If
      '-----------------------------------------------
      'Controlla se articoli reali
      If dttET.Rows(0)!et_tipork.ToString <> "$" AndAlso dttET.Rows(0)!et_tipork.ToString <> "V" Then
        If dttArti.Rows(0)!ar_tipoopz.ToString <> " " AndAlso dttArti.Rows(0)!ar_tipoopz.ToString <> "O" Then
          e.ProposedValue = e.Row!ec_codart
          If Not ((e.Row!ec_tipork.ToString = "Y" OrElse e.Row!ec_tipork.ToString = "U") AndAlso dttArti.Rows(0)!ar_tipoopz.ToString = "G") Then  'se è Y oppure U ed è G, è TCP e non voglio il messaggio perchè è la normalità
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222093125000, "Impossibile movimentare articoli non reali")))
          End If
          Return False
        End If
      End If

      '-----------------------------------------------
      'inibisco l'inserimento
      'di un articolo TCO quando non è presente il modulo TCO, 
      'di un articolo a fase/ubicazione se non hai modulo Logistica estesa, 
      'di un articolo a lotti/matricole se non hai EXCELLENCE (solo easy)
      If bModTCO = False AndAlso NTSCInt(dttArti.Rows(0)!ar_codtagl) <> 0 Then
        e.ProposedValue = e.Row!ec_codart
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128878158722556000, "Impossibile movimentare articoli gestiti a Taglie e colori senza aver attivato prima l'apposito modulo")))
        Return False
      End If
      If bModLEX = False Then
        If NTSCStr(dttArti.Rows(0)!ar_gesfasi) <> "N" Then
          e.ProposedValue = e.Row!ec_codart
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128878159793006000, "Impossibile movimentare articoli gestiti a fasi senza aver attivato il modulo 'Logistica estesa'")))
          Return False
        End If
        If NTSCStr(dttArti.Rows(0)!ar_gesubic) <> "N" Then
          Dim bOk As Boolean = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEBOLL", "OPZIONI", ".", "GestUbicSenzaLext", "0", " ", "0"))
          If bOk = False Then
            e.ProposedValue = e.Row!ec_codart
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128878161085136000, "Impossibile movimentare articoli gestiti ad ubicazioni dinamiche senza aver attivato il modulo 'Logistica estesa'")))
            Return False
          End If
        End If
      End If
      If CBool(lModuliExtDittaDitt And bsModExtMGE) Then   'modulo easy attivato
        If NTSCStr(dttArti.Rows(0)!ar_geslotti) <> "N" Then
          e.ProposedValue = e.Row!ec_codart
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128878161065480000, "Impossibile movimentare articoli gestiti a lotti con la versione 'Easy'")))
          Return False
        End If
        If NTSCStr(dttArti.Rows(0)!ar_gestmatr) <> "N" Then
          e.ProposedValue = e.Row!ec_codart
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128878161076400000, "Impossibile movimentare articoli gestiti a matricole con la versione 'Easy'")))
          Return False
        End If
      End If

      '-----------------------------------------------
      'Controlla se articoli KIT
      If dttArti.Rows(0)!ar_tipokit.ToString <> " " AndAlso (e.Row!ec_tipork.ToString = "H" OrElse e.Row!ec_tipork.ToString = "T") Then
        e.ProposedValue = e.Row!ec_codart
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222093281250, "Non è possibile utilizzare articoli kit in DOCUMENTI DI PRODUZIONE")))
        Return False
      End If

      '----------------------------------
      'verifico se c'è l'unità di misura principale per l'articolo
      If dttArti.Rows(0)!ar_unmis.ToString.Trim = " " AndAlso NTSCStr(e.ProposedValue) <> "D" AndAlso NTSCStr(e.ProposedValue) <> "M" Then
        e.ProposedValue = e.Row!ec_codart
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128548022072144000, "L'articolo inserito non ha l'unità di misura principale. Inserirla prima utilizzare l'articolo.")))
        Return False
      End If

      '----------------------------------
      'Setta un flag di form che indica se l'articolo  gestito per varianti taglie e colori
      'N.B.: non si possono indicare tali articoli se il tipo di ordine/impegno 
      '      Ordine fornitore aperto o Impegno cliente aperto

      'ADESSO SI !!! Ma solo se presenti nel Db i nuovi campi per farlo !!!  
      If NTSCInt(dttArti.Rows(0)!ar_codtagl) <> 0 Then
        If dttET.Rows(0)!et_tipork.ToString = "$" OrElse dttET.Rows(0)!et_tipork.ToString = "V" Then
          If Not dttECTC.Columns.Contains("ec_qtadis01") Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222093437500, "Non si possono indicare articoli a varianti 'Taglie e colori' per questo tipo di documento.")))
            Return False
          End If
        End If
      End If

      If BORDINI AndAlso Not BGESTORDLIST Then
        If NTSCDec(e.Row!ec_quaeva) > 0 Then
          Dim evnt As New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 130867167640052483, "Si sta modificando il codice articolo su una riga evasa. Proseguire?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = ThMsg.RETVALUE_NO Then Return False
        End If
      End If

      'Cerco la prima tipologia articolo tipizzata
      If nInibisciTipiArticoliDiversi = 1 AndAlso Not bDocDaRetail AndAlso NTSCStr(dttArti.Rows(0)(strNomeCampoArtico)).Trim <> "" Then
        Dim bEseguiControllo As Boolean = True
        If nInibisciTipiArticoliDiversiTrannePagContanti = -1 Then
          Dim dttPaga As New DataTable
          oCldDocu.ValCodiceDb(NTSCStr(dttET.Rows(0)!et_codpaga), strDittaCorrente, "TABPAGA", "N", "", dttPaga)
          If dttPaga.Rows.Count > 0 AndAlso NTSCInt(dttPaga.Rows(0)!tb_tippaga) = 4 Then bEseguiControllo = False
          dttPaga.Clear()
          dttPaga.Dispose()
        End If

        If bEseguiControllo Then
          For z As Integer = 0 To dttEC.Rows.Count - 1
            'Escludo gli articoli non tipizzati
            If NTSCStr(dttEC.Rows(z)!xxo_art62).Trim = "" Then Continue For

            If NTSCStr(dttEC.Rows(z)!xxo_art62) = NTSCStr(dttArti.Rows(0)(strNomeCampoArtico)) Then Exit For

            If bArticoliDiversiChiediConferma = False Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130021171349136560, "L'articolo |" & NTSCStr(e.ProposedValue) & "| (riga |" & NTSCInt(e.Row!ec_riga) & "|) ha tipoligia |" & NTSCStr(dttArti.Rows(0)(strNomeCampoArtico)) & vbCrLf & _
                                                                                    "| e non è compatibile con l'articolo |" & NTSCStr(dttEC.Rows(z)!ec_codart) & "| (riga |" & NTSCInt(dttEC.Rows(z)!ec_riga) & "|) di tiplogia |" & NTSCStr(dttEC.Rows(z)!xxo_art62) & "|")))
              e.ProposedValue = e.Row!ec_codart
              Return False
            Else
              Dim evnt As NTSEventArgs = Nothing
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 130054027205080520, _
                "Attenzione!" & vbCrLf & _
                "L'articolo |" & NTSCStr(e.ProposedValue) & "| (riga |" & NTSCInt(e.Row!ec_riga) & "|) ha tipoligia |" & NTSCStr(dttArti.Rows(0)(strNomeCampoArtico)) & vbCrLf & _
                "| e non è compatibile con l'articolo |" & NTSCStr(dttEC.Rows(z)!ec_codart) & "| (riga |" & NTSCInt(dttEC.Rows(z)!ec_riga) & "|) di tiplogia |" & NTSCStr(dttEC.Rows(z)!xxo_art62) & "|." & vbCrLf & _
                "Confermare ugualmente l'inserimento dell'articolo?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
                e.ProposedValue = e.Row!ec_codart
                Return False
              Else
                Exit For
              End If
            End If
          Next
        End If

        e.Row!xxo_art62 = dttArti.Rows(0)(strNomeCampoArtico)
      End If

      '--------------------------
      'memorizzo che al cambio di riga devo cancellare gli scarichi collegati
      If NTSCStr(e.ProposedValue) <> NTSCStr(e.Row!ec_codart) AndAlso e.Row.RowState <> DataRowState.Added Then
        If e.Row!ec_tipork.ToString = "H" Or e.Row!ec_tipork.ToString = "T" Then
          bCancellaScarichi = True
        Else
          bCancellaScarichi = False
        End If
      End If

      ' Se l'articolo attuale non è a matricole ma il precedente lo era allora svuota movmatr
      If strOldArt.Trim <> "" Then
        If oCldDocu.ValCodiceDb(strOldArt, strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
          If dttTmp.Rows.Count <> 0 Then
            If BORDINI = False Then
              If NTSCStr(dttArti.Rows(0)!ar_gestmatr) = "N" AndAlso NTSCStr(dttTmp.Rows(0)!ar_gestmatr) = "S" Then
                dtrRow = dttMOVMATR.Select("mma_riga = " & NTSCInt(e.Row!ec_riga))
                For Each dtrSelRow In dtrRow
                  dtrSelRow.Delete()
                Next
              End If
            End If
          End If
        End If
      End If

      Return True
    Catch ex As Exception
      If e.Column.ColumnName = "ec_codart" Then e.ProposedValue = "D"
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function BeforeColUpdate_CORPO_ec_fase(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""

    Try
      '------------------------
      'Prima deve essere indicato l'articolo
      If NTSCStr(e.Row!ec_codart).Trim = "" And NTSCInt(e.ProposedValue) <> 0 Then
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222095468750, "Indicare l'articolo prima di inserire la fase")))
        Return False
      End If

      If NTSCStr(e.Row!ec_codart).Trim = "" Then
        e.ProposedValue = e.Row!ec_fase
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036361809000, "Codice Articolo inserito non presente in archivio")))
        Return False
      Else
        If GetMemDttArti(e.Row!ec_codart.ToString).Rows.Count = 0 Then
          e.ProposedValue = e.Row!ec_fase
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036377253000, "Codice Articolo inserito non presente in archivio")))
          Return False
        End If
      End If

      '------------------------
      'Se l'articolo  gestito a fasi ne controlla l'esistenza in ARTFASI
      'altrimenti deve essere obbligatoriamente a 0
      Select Case dttArti.Rows(0)!ar_gesfasi.ToString
        Case "S"
          If NTSCInt(e.ProposedValue) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222095781250, "L'articolo è gestito per fasi e pertanto deve essere indicato un codice fase valido.")))
            e.ProposedValue = e.Row!ec_fase
          Else
            If Not oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTFASI", "N", strTmp, Nothing, e.Row!ec_codart.ToString) Then
              e.ProposedValue = e.Row!ec_fase
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222095937500, "Codice Fase inserita non presente in archivio per l'articolo '|" & e.Row!ec_codart.ToString & "|'")))
              Return False
            Else
              e.Row!xxo_fase = strTmp
            End If
          End If
        Case "N"
          If NTSCInt(e.ProposedValue) <> 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222096093750, "L'articolo non è gestito per fasi e pertanto non può essere indicato un codice fase.")))
            e.ProposedValue = 0
            Return False
          End If
      End Select

      '--------------------------
      'cambiando articolo e/o fase devo cancellare gli scarichi collegati
      If NTSCInt(e.ProposedValue) <> NTSCInt(e.Row!ec_fase) And e.Row.RowState <> DataRowState.Added Then
        If e.Row!ec_tipork.ToString = "H" Or e.Row!ec_tipork.ToString = "T" Then
          bCancellaScarichi = True
        Else
          bCancellaScarichi = False
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_matric(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    'validazione colonna BARCODE
    Try
      If bInImportRigheNota Then Return True
      If bInImportRigheOrd Then Return True
      If bDisabilitaDecodificaBarcode Then Return True

      If NTSCStr(e.ProposedValue) = "" Then e.ProposedValue = " "
      If NTSCStr(e.ProposedValue) <> " " Then
        If NTSCStr(e.Row!ec_codart).Trim <> "" And NTSCStr(e.Row!ec_codart).Trim <> "D" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222095312500, "Inserire il codice a barre prima di digitare il codice dell'articolo")))
          e.ProposedValue = " "
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_unmis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim bUmprz As Boolean = False
    Try
      If Not BloccoNotaPrelievoEvasa(e.Row) Then e.ProposedValue = e.Row!ec_unmis : Return False

      If BORDINI = False And bCambioArticoloScarico = False Then
        If NTSCInt(e.Row!ec_ornum) > 0 Or NTSCInt(e.Row!ec_npnum) > 0 Then
          'il codice sotto non ha senso: se in un ddt evado l'articolo mp1 collegato all'ordine 1, 
          'poi modifico e da mp1 passo a mat1l che ha un'altroa UMP devo prendere la nuova ump!!!
          'tanto poi nell'esempio viene dato il messaggio che con il nuovo articolo vengono scollegati i riferimenti all'ordine/nota di prelievo!
          'e.ProposedValue = e.Row!ec_unmis
          'Return True
        End If
      End If

      'se sono nella BeforeColUpdateCordart non eseguo la cercaprezzo, devo ancora indicare la qta ...
      'inoltre la 'GestioneUMPrezzo' viene fatta anche nella BeforeColUpdateCordart_STD, così come il settaggio di 'e.Row!ec_umprz = '
      'lo faccio solo per un fatto di prestazioni!!!!!!!!!!!
      If bInValidazCodart Then Return True

      bUmprz = CType(oCleComm, CLELBMENU).GestioneUMPrezzo(bGestionePrezzi, IIf(BORDINI, "BSORGSOR", "BSVEBOLL").ToString, _
                                                          dttET.Rows(0)!et_tipork.ToString, _
                                                          bTerzista, e.Row!ec_codart.ToString, e.ProposedValue.ToString, strDittaCorrente)
      If e.Row!ec_codart.ToString <> "" Then
        'controllo sul PROJECT MANAGEMENT
        If bUmprz And NTSCInt(e.Row!ec_pmtaskid) <> 0 And e.ProposedValue.ToString <> e.Row!ec_ump.ToString Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222093750000, "La presenza di un Task agganciato alla registrazione, non ammette la possibilità, per l'articolo movimentato," & vbCrLf & _
                                                "di utilizzare una gestione dei prezzi riferiti ad una unità di misura diversa dalla principale.")))
          e.ProposedValue = e.Row!ec_ump.ToString
          Return False
        End If
      Else
        e.ProposedValue = " "
      End If    'If e.Row!ec_codart.ToString <> "" Then

      'il prezzo per unità di misura è gestito solo se è stata settata l'apposita opzione di registro e se non sono negli impegni/scarichi collegati
      If bUmprz And Not ((dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "T") And _
                          e.Row.Table.TableName.ToUpper = "CORPOIMP") Then
        e.Row!ec_umprz = "S"
      Else
        e.Row!ec_umprz = "N"
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_colli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dRapporto As Decimal = 0
    Try
      If bInValidazQuant = False Then
        If e.Row!ec_codart.ToString.Trim = "" Or e.Row!ec_unmis.ToString = "" And NTSCDec(e.ProposedValue.ToString) <> 0 Then
          e.ProposedValue = 0
        End If
      End If

      If Not BloccoNotaPrelievoEvasa(e.Row) Then e.ProposedValue = e.Row!ec_colli : Return False

      If Not BORDINI Then e.ProposedValue = ArrDbl(NTSCDec(e.ProposedValue), nCO2_ArrQuant)

      'Negli scarichi collegati, le righe di scarico le modifico del rapporto colli\quantità di riga e non dell'articolo
      If bInValidazQuant = False Then
        If NTSCStr(e.Row!ec_tipork) = "U" And NTSCDec(e.Row!ec_quant) <> 0 And NTSCDec(e.Row!ec_colli) <> 0 Then
          dRapporto = NTSCDec(e.Row!ec_quant) / NTSCDec(e.Row!ec_colli)
          e.Row!ec_quant = NTSCDec(e.ProposedValue) * dRapporto
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_quant(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      bInValidazQuant = True

      If NTSCStr(e.Row!ec_codart).Trim <> "" And e.Row!ec_unmis.ToString <> "" Then
        If GetMemDttArti(e.Row!ec_codart.ToString).Rows.Count = 0 Or e.ProposedValue.ToString.Trim = "" Then
          bInValidazQuant = False
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127989790370468750, "Codice Articolo inserito non presente in archivio")))
          Return False
        End If

      End If    'If e.Row!ec_codart.ToString <> "" And e.Row!ec_unmis.ToString <> "" Then

      If Not BloccoNotaPrelievoEvasa(e.Row) Then e.ProposedValue = e.Row!ec_quant : Return False

      If Not BORDINI Then e.ProposedValue = ArrDbl(NTSCDec(e.ProposedValue), nCO2_ArrQuant)

      If NTSCStr(e.Row!ec_codart).Trim <> "" And e.Row!ec_unmis.ToString <> "" Then
        '----------------------------------
        'Controllo quantità lotto
        If bMovimQtaLotti And e.Row.Table.TableName = "CORPO" Then
          If dttArti.Rows(0)!ar_polriord.ToString = "F" And NTSCDec(dttArti.Rows(0)!ar_minord) <> 0 Then
            'Controlla la quantità
            If (NTSCDec(e.ProposedValue) Mod NTSCDec(dttArti.Rows(0)!ar_minord)) <> 0 Then
              bInValidazQuant = False
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036491047000, "La quantità |" & Format(NTSCDec(e.ProposedValue), oApp.FormatQta) & _
                              "| (in unità di misura principale) indicata per l'articolo |" & UCase(e.Row!ec_codart.ToString) & _
                              "| non è un multiplo del lotto standard |" & NTSCDec(dttArti.Rows(0)!ar_minord) & _
                              "| indicato nella sua anagrafica. quantità non ammessa.")))
              e.ProposedValue = e.Row!ec_quant
              Return False
            End If
          End If
        End If
      End If    'If e.Row!ec_codart.ToString <> "" And e.Row!ec_unmis.ToString <> "" Then

      bInValidazQuant = False
      Return True
    Catch ex As Exception
      bInValidazQuant = False
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function BeforeColUpdate_CORPO_ec_preziva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      '----------------------------
      'Disabilitato se carichi da lav. interne
      If (dttET.Rows(0)!et_tipork.ToString = "H" OrElse dttET.Rows(0)!et_tipork.ToString = "T") _
          AndAlso Not bTerzista _
          AndAlso Not bInImportRigheOrd AndAlso Not bInCreaDocDaGnor _
          AndAlso Not bInImportRigheOff AndAlso Not bInImportRigheNota Then
        e.ProposedValue = NTSCDec(e.Row!ec_preziva)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222094531250, "Prezzo IVA inclusa non modificabile per le righe di documenti di produzione interno.")))
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_prezvalc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      'Disabilitato se carichi da lav. interne
      If (dttET.Rows(0)!et_tipork.ToString = "H" OrElse dttET.Rows(0)!et_tipork.ToString = "T") _
         AndAlso Not bTerzista _
         AndAlso Not bInImportRigheOrd AndAlso Not bInCreaDocDaGnor _
         AndAlso Not bInImportRigheOff AndAlso Not bInImportRigheNota Then
        e.ProposedValue = NTSCDec(e.Row!ec_prezvalc)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222094687500, "Prezzo in valuta non modificabile per le righe di documenti di produzione interno.")))
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_prezzo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If (e.Row!ec_tipork.ToString = "H" Or e.Row!ec_tipork.ToString = "T") And Not bTerzista Then
        e.ProposedValue = NTSCDec(e.Row!ec_prezzo)
        'non posso dare il messaggio, altrimenti verrebbe scatenato anche quando esco ec_codart in un ordine di prod di mp1 o di qualsiasi articolo con prezzo diverso da 0
        'ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590270672968750, "Prezzo non modificabile per le righe di ordine di produzione interno.")))
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_codiva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dtrT As DataRow = Nothing
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xxo_codiva = ""
        Return True
      End If
      dtrT = GetTabcivaRow(NTSCInt(e.ProposedValue.ToString))
      If dtrT Is Nothing Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127989790294843750, "Codice IVA |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")))
        e.ProposedValue = e.Row!ec_codiva
      Else
        e.Row!xxo_codiva = NTSCStr(dtrT!tb_desciva)
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_controp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dtrT As DataRow = Nothing
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xxo_controp = ""
        e.Row!ec_contocontr = 0
        e.Row!xxo_contocon = ""
        Return True
      End If
      dtrT = GetTabcoveRow(NTSCInt(e.ProposedValue.ToString))
      If dtrT Is Nothing Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127989790576250000, "Attenzione!" & vbCrLf & _
          "Il codice contropartita vendite/acquisti '|" & NTSCInt(e.ProposedValue).ToString & "|' indicato" & vbCrLf & _
          "è inesistente oppure NON possiede un sottoconto specifico ditta valido.")))
        e.ProposedValue = e.Row!ec_controp
      Else
        e.Row!xxo_controp = NTSCStr(dtrT!tb_descove)
        e.Row!ec_contocontr = NTSCInt(dtrT!tb_concova)
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_codtpro(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.Row!ec_codart.ToString.Trim = "" Then Return False
      If Not ValCodice(e, "TABTPRO", "xxo_codtpro", oApp.Tr(Me, 128517103606144000, "Codice promozione |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"), "N") Then
        e.ProposedValue = e.Row!ec_codtpro
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_ubicaz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCStr(e.Row!ec_codart).Trim = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222096250000, "Indicare l'articolo prima di inserire l'ubicazione.")))
        e.ProposedValue = e.Row!ec_ubicaz
        Return False
      End If

      If e.ProposedValue.ToString = "" Then
        e.ProposedValue = " "
        Return True
      End If

      If NTSCStr(e.Row!ec_codart).Trim = "" Then
        e.ProposedValue = e.Row!ec_fase
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036391293000, "Codice Articolo inserito non presente in archivio")))
        Return False
      Else
        If GetMemDttArti(e.Row!ec_codart.ToString).Rows.Count = 0 Then
          e.ProposedValue = e.Row!ec_fase
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036406269000, "Codice Articolo inserito non presente in archivio")))
          Return False
        End If
      End If

      Select Case dttArti.Rows(0)!ar_gesubic.ToString
        Case "S"
          If Not bModSupRme Then
            If e.ProposedValue.ToString.Trim = "" And e.ProposedValue.ToString = "".PadRight(12, "-"c) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222096562500, "L'articolo è gestito per ubicazioni e pertanto deve essere indicato un'ubicazione valida.")))
              e.ProposedValue = e.Row!ec_ubicaz
              Return False
            End If
          Else
            Dim strUbicaz As String = NTSCStr(e.ProposedValue)
            If strUbicaz = "".PadLeft(12, "-"c) Then strUbicaz = " " : e.ProposedValue = " "
            If strUbicaz.Trim <> "" Then
              'Verifico sempre sul magazzino 1
              If TrascodificaUbicazione(NTSCInt(e.Row!ec_magaz), strUbicaz, True, True, True) Then
                If strUbicaz <> "".PadRight(12, "-"c) Then e.ProposedValue = strUbicaz
              Else
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128874502996866378, "Ubicazione |" & NTSCStr(e.ProposedValue) & "| non presente nel magazzino |" & NTSCInt(e.Row!ec_magaz) & "|")))
                e.ProposedValue = e.Row!ec_ubicaz
              End If
              'Verifico anche sul magazzino 2 se necessario
              If NTSCInt(e.Row!ec_magaz2) <> 0 Then
                If TrascodificaUbicazione(NTSCInt(e.Row!ec_magaz2), strUbicaz, True, True, True) Then
                  If strUbicaz <> "".PadRight(12, "-"c) Then e.ProposedValue = strUbicaz
                Else
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128874502996866379, "Ubicazione |" & NTSCStr(e.ProposedValue) & "| non presente nel magazzino |" & NTSCInt(e.Row!ec_magaz2) & "|")))
                  e.ProposedValue = e.Row!ec_ubicaz
                End If
              End If
            End If
          End If
        Case "N"
            If e.ProposedValue.ToString.Trim <> "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222096718750, "L'articolo non è gestito per ubicazioni e pertanto non può essere indicato un'ubicazione.")))
              e.ProposedValue = e.Row!ec_ubicaz
              Return False
            End If
      End Select
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_codcena(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dtrT As DataRow = Nothing
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xxo_codcena = ""
        Return True
      End If
      dtrT = GetTabcenaRow(NTSCInt(e.ProposedValue.ToString))
      If dtrT Is Nothing Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127989790955781250, "Codice centro |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")))
        e.ProposedValue = e.Row!ec_codcena
      Else
        e.Row!xxo_codcena = NTSCStr(dtrT!tb_descena)
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_codcfam(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If bInOnAddNewRowImp Then Return True
      If e.ProposedValue.ToString = "" Then
        e.ProposedValue = " "
        e.Row!xxo_codcfam = ""
        Return True
      End If
      ValCodice(e, "CFAM", "xxo_codcfam", oApp.Tr(Me, 127989790731875000, "Codice famiglia '|" & e.ProposedValue.ToString & "|' non corretto"), "S")
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_codclie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "ANAGRAFC", "xxo_codclie", oApp.Tr(Me, 127989791054531250, "Codice cliente/fornitore |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"), "N")
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_stasino(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.Row!ec_tipork.ToString = "U" Then
        If e.ProposedValue.ToString.ToUpper <> "S" Then
          e.ProposedValue = "S"
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128695879776093750, "Il valore immesso per il campo 'Stampa riga' deve essere sempre 'Sì'")))
        End If
      Else
        Select Case e.ProposedValue.ToString
          Case "N", "B", "D"
            If Not (NTSCDec(e.Row!ec_quant) = 0 Or NTSCDec(e.Row!ec_prezzo) = 0) Then
              If Not (dttET.Rows(0)!et_tipork.ToString = "B" And e.ProposedValue.ToString = "D") Then
                e.ProposedValue = "S"
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222094843750, "Una riga da non stampare deve contenere prezzo o quantità pari a 0.")))
              End If
            End If
        End Select
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_magaz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dtrT As DataRow = Nothing
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xxo_magaz = ""
        Return True
      End If
      dtrT = GetTabmagaRow(NTSCInt(e.ProposedValue.ToString))
      If dtrT Is Nothing Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127989789983750000, "Codice magazzino |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")))
        e.ProposedValue = e.Row!ec_magaz
      Else
        e.Row!xxo_magaz = NTSCStr(dtrT!tb_desmaga)
        If e.Row.Table.Columns.Contains("xxo_flclavo") Then e.Row!xxo_flclavo = dtrT!tb_flclavo 'solo per veboll
        ThrowRemoteEvent(New NTSEventArgs("LeggiDispo:" & NTSCStr(e.Row!ec_codart) & "§" & NTSCStr(e.ProposedValue) & "§" & _
                         NTSCStr(e.Row!ec_fase) & "§" & NTSCStr(e.Row!ec_commeca), ""))
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_magaz2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dtrT As DataRow = Nothing
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xxo_magaz2 = ""
        Return True
      Else
        If e.Row!ec_tipork.ToString = "U" Then
          e.ProposedValue = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128695882815156250, "Nei documenti di 'Scarico a produzione' non deve essere indicato il magazzino secondario")))
          Return False
        End If
      End If

      dtrT = GetTabmagaRow(NTSCInt(e.ProposedValue.ToString))
      If dtrT Is Nothing Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127989790414531250, "Codice magazzino 2 |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")))
        e.ProposedValue = e.Row!ec_magaz2
      Else
        e.Row!xxo_magaz2 = NTSCStr(dtrT!tb_desmaga)
        If e.Row.Table.Columns.Contains("xxo_flclavo2") Then e.Row!xxo_flclavo2 = dtrT!tb_flclavo 'solo per veboll
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_provv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.Row!ec_ricimp.ToString <> "S" Then ' provv a valore anzich percentuale
        If NTSCDec(e.ProposedValue) < -100 Or NTSCDec(e.ProposedValue) > 100 Then
          e.ProposedValue = e.Row!ec_provv
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222094375000, "Nel campo 'Provvigione' il valore ammesso deve essere un numero compreso tra -100 e 100")))
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_provv2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.Row!ec_ricimp.ToString <> "S" Then ' provv a valore anzich percentuale
        If NTSCDec(e.ProposedValue) < -100 Or NTSCDec(e.ProposedValue) > 100 Then
          e.ProposedValue = e.Row!ec_provv2
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222095000000, "Nel campo 'Provvigione 2' il valore ammesso deve essere un numero compreso tra -100 e 100")))
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_commeca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As DataTable = New DataTable
    Try
      If bInOnAddNewRow Then Return True
      If bInOnAddNewRowImp Then Return True
      If bInSalvaOrdine Then Return True
      If ValCodice(e, "COMMESS", "xxo_commeca", oApp.Tr(Me, 127989790849531250, "Codice commessa |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"), "N", dttTmp) Then
        If NTSCInt(e.ProposedValue) > 0 And dttTmp.Rows.Count > 0 And bControllaCommessaChiusa Then
          If Year(NTSCDate(dttTmp.Rows(0)!co_dtchiu)) <> 2099 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130420443680160017, "Attenzione!" & vbCrLf & "La commessa |" & NTSCInt(e.ProposedValue).ToString & "| è già stata chiusa.")))
          End If
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_subcommeca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As DataTable = New DataTable

    Try
      If bInOnAddNewRow Then Return True
      If bInOnAddNewRowImp Then Return True
      If NTSCStr(e.ProposedValue) = "" Then e.ProposedValue = " "
      If bInSalvaOrdine = True Then Return True
      If bDisabilitaControlliSubcomm = False Then
        If NTSCStr(e.ProposedValue) <> " " Then
          oCldDocu.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "SUBCOMM", "S", "", dttTmp, e.Row!ec_commeca.ToString)
          If dttTmp.Rows.Count = 0 Then
            e.ProposedValue = " "
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036550085000, "Codice sottocommessa inesistente")))
          End If
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_tctaglia(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If
      If e.ProposedValue.ToString = "" Then e.ProposedValue = " "

      If NTSCStr(e.Row!ec_codart).Trim = "" Then
        e.ProposedValue = " "
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128587714708125001, "Inserire prima il codice articolo")))
        Return False
      End If

      '---------------------
      'se non sono su un impegno/scarico a prod non devo indciare nulla
      If e.Row!ec_tipork.ToString <> "Y" And e.Row!ec_tipork.ToString <> "U" Then
        e.ProposedValue = " "
        Return True
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

  Public Overridable Function BeforeColUpdate_CORPO_xxo_tctagliaf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If
      If e.ProposedValue.ToString = "" Then e.ProposedValue = " "

      If NTSCStr(e.Row!ec_codart).Trim = "" Then
        e.ProposedValue = " "
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128587714708125002, "Inserire prima il codice articolo")))
        Return False
      End If

      '---------------------
      'se non sono su un impegno/scarico a prod non devo indciare nulla
      If e.Row!ec_tipork.ToString <> "Y" And e.Row!ec_tipork.ToString <> "U" Then
        e.ProposedValue = " "
        Return True
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_pmtaskid(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Dim strSummary As String = ""
    Dim strRilasciato As String = ""
    Dim strDesta As String = ""
    Dim dWorkres As Decimal = 0
    Dim dQtares As Decimal = 0
    Dim dCostores As Decimal = 0
    Dim strTmp As String = ""
    Try
      If bModPM = False Then
        'valido solo il task\idwbs in comwbs
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xxo_pmtaskid = ""
          Return True
        End If
        If Not oCldDocu.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "COMWBS", "N", strTmp, Nothing, e.Row!ec_commeca.ToString, e.Row!ec_subcommeca.ToString) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129830372048887648, "Codice ID WBS '|" & e.ProposedValue.ToString & "|' non corretto per la commessa e subcommessa indicata sulla riga |" & e.Row!ec_riga.ToString & "|")))
          e.ProposedValue = e.Row!ec_pmtaskid.ToString
          Return False
        End If
        e.Row!xxo_pmtaskid = strTmp

        Return True
      End If    'If bModPM = False Then

      If NTSCInt(e.ProposedValue) <> 0 Then
        If NTSCStr(e.Row!ec_umprz) = "S" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443251508330079, _
                  "La presenza, per l'articolo movimentato, di una gestione dei prezzi riferiti ad una unità di misura diversa dalla principale, " & vbCrLf & _
                  "non ammette la possibilità di agganciare un Task alla registrazione.")))
          e.ProposedValue = 0
          Return False
        End If

        '-----
        If BORDINI Then
          If dttET.Rows(0)!et_tipork.ToString = "O" Or dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "X" Then
            e.Row!xxo_darave = "D"
          End If
          If dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#" Then
            e.Row!xxo_darave = "A"
          End If
          If Not (dttET.Rows(0)!et_tipork.ToString = "O" Or dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#" Or dttET.Rows(0)!et_tipork.ToString = "X") Then
            e.Row!xxo_darave = " "
          End If
        Else
          oCldDocu.ValCodiceDb(NTSCInt(e.Row!ec_causale).ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            Select Case NTSCStr(dttTmp.Rows(0)!tb_testci)
              Case " " : e.Row!xxo_darave = " "
              Case "A", "B" : e.Row!xxo_darave = "A"
              Case "D", "E" : e.Row!xxo_darave = "D"
            End Select
          Else
            'per sicurezza...
            e.Row!xxo_darave = " "
          End If
        End If

        dttTmp.Clear()
        '-----
        'Controllo su mm_darave <> " "
        If NTSCStr(e.Row!xxo_darave) = " " Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443286144931641, "Per indicare un Task occorre prima inserire un Segno Dare/avere ammissibile.")))
          e.ProposedValue = 0
          Return False
        End If

        If Not oCldDocu.LegTaskGr(strDittaCorrente, NTSCStr(e.Row!ec_commeca), NTSCStr(e.ProposedValue), "TASKS", 0, False, strOut, strDaraveout, strTipoTask, strGestcost, strPrevGrup, strSummary, Nothing) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443286111015625, "Task inesistente")))
          e.ProposedValue = 0
          Return False
        Else
          'Rimappiamo il Segno Dare/Avere per le righe di Ricavo...
          Select Case strDaraveout
            Case "R", "S"
              strDaravedef = "A"
            Case "T", "U"
              strDaravedef = "D"
            Case Else
              strDaravedef = strDaraveout
          End Select
          '...e per i Task di tipo 'Spediz a cli./cant.
          'If strTipoTask = "6" Then strDaravedef = "A"
          'Controllo su mm_darave compatibile con il Task
          If Not (strDaravedef = NTSCStr(e.Row!xxo_darave)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443286079570313, "Il Task selezionato ha un segno Dare/avere di C.A. non compatibile con quello indicato nella riga corrente! Task non accettato.")))
            e.ProposedValue = 0
            Return False
          End If
          If (strPrevGrup = "S") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443286067412110, "Task di preventivazione di gruppo non compatibile con il tipo di registrazioni correnti.")))
            e.ProposedValue = 0
            Return False
          End If
          If (strSummary = "S") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443286053486329, "Task di gruppo non compatibile con il tipo di registrazioni correnti.")))
            e.ProposedValue = 0
            Return False
          Else

            e.Row!xxo_pmtaskid = strOut

            If BGESTORDLIST Then Return True 'da gestione prop. d'ordine non devo fare altro

            If Not oCldDocu.Caricattproesebappo(strDittaCorrente, lIIttproesebappo, NTSCInt(e.Row!ec_commeca), _
                                                NTSCInt(e.ProposedValue), Nothing) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443286179326172, "Problemi a caricare i record di proeseb su ttproesebappo  ! ")))
              e.ProposedValue = 0
              Return False
            End If
            ' adesso legge gli altri campi da task
            lEseddaes = 0
            If e.Row.RowState <> DataRowState.Added Then
              If BORDINI Then
                If NTSCInt(e.Row("ec_pmtaskid", DataRowVersion.Original)) <> 0 Then
                  lEseddaes = oCldDocu.TrovaEsecIdDaMovord(strDittaCorrente, "TTPROESEC", lIIttproesebappo, _
                                          NTSCInt(e.Row!ec_commeca), dttET.Rows(0)!et_tipork.ToString, _
                                          NTSCInt(dttET.Rows(0)!et_anno), dttET.Rows(0)!et_serie.ToString, _
                                          NTSCInt(dttET.Rows(0)!et_numdoc), NTSCInt(e.Row!ec_riga))
                End If
              Else
                lEseddaes = oCldDocu.TrovaEsecIdDaMovmag(strDittaCorrente, "TTPROESEC", lIIttproesebappo, _
                                        NTSCInt(e.Row!ec_commeca), dttET.Rows(0)!et_tipork.ToString, _
                                        NTSCInt(dttET.Rows(0)!et_anno), dttET.Rows(0)!et_serie.ToString, _
                                        NTSCInt(dttET.Rows(0)!et_numdoc), NTSCInt(e.Row!ec_riga))
              End If
            End If

            If BORDINI = False Then
              'documenti di magazzino: è appurato che se su una riga si era agganciato un task, poi si è cambiato l'id del task, poi si reinserisce l'id task iniziale, la routine 
              'LegTasksCompleteDao sbaglia a calcolare i residui (sia in net che in vb6). abbiamo analizzato la cosa con Marco P e Roberto C e non sembra ci sia una soluzione 
              'facile per risolvere il problema senza fare danni o dovre cambiare drasticamente diverse parti di codice di PM
              'la soluzoine, per ora, è cancellare la righa di veboll e reinserirla. in questo modo l'errore non si verifica
              'il caso da testare è: se per lo stesso tipo/anno/serie/numdoc/riga/commessa passo da un taskid = 0 ad un taskid <> 0
              'verifico e blocco se ci sono presenti record in TTPROESEC con stessa chiave indicata sopra e taskid = quello che si vuole inserire
              If Not oCldDocu.CheckTaskVebollttproesec(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, _
                                                       NTSCInt(dttET.Rows(0)!et_anno), _
                                                       dttET.Rows(0)!et_serie.ToString, _
                                                       NTSCInt(dttET.Rows(0)!et_numdoc), _
                                                       NTSCInt(e.Row!ec_riga), NTSCInt(e.Row!ec_commeca), _
                                                       NTSCInt(e.ProposedValue), lIIttproesebappo, dttTmp) Then
                e.ProposedValue = 0
                Return False
              Else
                If dttTmp.Rows.Count > 0 Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130089437998733968, "Attenzione: il task indicato è già stato impostato su questa riga di documento e sucessivamente azzerato. Per poterlo reimpostare occorre cancellare la riga del documento e reinserirla con un numero di riga diverso")))
                  dttTmp.Clear()
                  e.ProposedValue = 0
                  Return False
                End If
              End If

              dttTmp.Clear()
            End If    'If BORDINI = False Then


            If bInImportRigheOrd And dttET.Rows(0)!et_tipork.ToString = "T" Then
              'le stesse routine vengono fatte anche in beveboll.ImportaOrdini 
              'vedi dtrPm = dttEC.Select("ec_tipork = 'T' AND ec_riga > " & lRigaIniz.ToString, "ec_riga")
              'se si lancia 2 volte la LegTasksCompleteDao
              'la seconda volta rievade in TTPROESEC la riga d'ordine 2 volte e segnala che il task ha un residuo = 0!!!!
              Return True
            End If

            If Not oCldDocu.LegTasksCompleteDao(strDittaCorrente, "TTPROESEC", lIIttproesebappo, lIItttasks, _
                                              NTSCInt(e.Row!ec_commeca), NTSCInt(e.ProposedValue), lEseddaes, _
                                              IntSetDate("31/12/2099"), _
                                              CBool(IIf(BORDINI, bPMControllaBaseCostOR, bPMControllaBaseCostMG)), _
                                              strDesta, strDaraveout, strTipoTask, strGestcost, dQtares, dWorkres, _
                                              dCostores, dBasecost, dBasecostun, dBasework, dBaseqta, _
                                              strPrevGrup, BORDINI, strRilasciato) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443286205009766, "Sul Task è stato rilevato  un problema in LegTaskCompleteDao ! ")))
              e.ProposedValue = 0
              Return False
            Else
              ' prima testo se si può accettare
              If BORDINI Then
                If Not ((strTipoTask = "1" And dttET.Rows(0)!et_tipork.ToString = "O") Or _
                        (strTipoTask = "3" And dttET.Rows(0)!et_tipork.ToString = "O") Or _
                        (strTipoTask = "5" And (dttET.Rows(0)!et_tipork.ToString = "O" Or dttET.Rows(0)!et_tipork.ToString = "H")) Or _
                        (strTipoTask = "7" And dttET.Rows(0)!et_tipork.ToString = "X") Or _
                        (strTipoTask = "8" And (dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#"))) Then
                  'solo appr. esterni, appr. interni e acq. servizi
                  'oppure spediz. a cant./cli.
                  'oppure spediz. a terzista
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129446611018564453, "Task non compatibile con il tipo di registrazioni correnti")))
                  e.ProposedValue = 0
                  Return False
                End If
              Else
                If Not ((strTipoTask = "1" Or strTipoTask = "3" Or strTipoTask = "5") Or _
                        (strTipoTask = "4" And (dttET.Rows(0)!et_tipork.ToString = "Z" Or dttET.Rows(0)!et_tipork.ToString = "W")) Or _
                        (strTipoTask = "6" And (dttET.Rows(0)!et_tipork.ToString = "B" Or dttET.Rows(0)!et_tipork.ToString = "W")) Or _
                        (strTipoTask = "7" And (dttET.Rows(0)!et_tipork.ToString = "B" Or dttET.Rows(0)!et_tipork.ToString = "W")) Or _
                        (strTipoTask = "B" And (dttET.Rows(0)!et_tipork.ToString = "Z" Or dttET.Rows(0)!et_tipork.ToString = "B" Or dttET.Rows(0)!et_tipork.ToString = "W")) Or _
                        (strTipoTask = "C" And (dttET.Rows(0)!et_tipork.ToString = "B" Or dttET.Rows(0)!et_tipork.ToString = "W")) Or _
                        (strTipoTask = "8" And strDaravedef = "A" And (dttET.Rows(0)!et_tipork.ToString = "A" Or dttET.Rows(0)!et_tipork.ToString = "B" Or dttET.Rows(0)!et_tipork.ToString = "C" Or dttET.Rows(0)!et_tipork.ToString = "W")) Or _
                        (strTipoTask = "8" And strDaravedef = "D" And dttET.Rows(0)!et_tipork.ToString = "N")) Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443286226923829, "Task non compatibile con il tipo di registrazioni correnti")))
                  e.ProposedValue = 0
                  Return False
                End If
              End If

              If Not (strGestcost = "2" Or strGestcost = "3") Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443286242294922, "Task con tipo gest. costi non compatibile con il tipo di registrazioni correnti")))
                e.ProposedValue = 0
                Return False
              End If
              If dCostores = 0 And dQtares = 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443286255732422, "Task già completato (costo residuo = 0), non evadibile")))
                e.ProposedValue = 0
                Return False
              End If
              If BORDINI Then
                If bPMSoloTaskRilasciatiOR Then
                  If Not (strRilasciato = "S") Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129446572239248047, "Task non ancora rilasciato , non evadibile")))
                    e.ProposedValue = 0
                    Return False
                  End If
                End If
              Else
                If bPMSoloTaskRilasciatiMG Then
                  If Not (strRilasciato = "S") Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443286278369141, "Task non ancora rilasciato , non evadibile")))
                    e.ProposedValue = 0
                    Return False
                  End If
                End If
              End If

              'OK, inizializzo gli altri campi
              e.Row!xxo_prevgrup = strPrevGrup
              e.Row!xxo_gestcost = strGestcost
              '***********************************************************
              ' qui ci sarà da rivedere !!!!!!! (riproposta di prezzi ...)
              '***********************************************************
              'If strPrevgrup <> "F" Then  ' qui si può migliorare con opzioni di registro...
              If dttET.Rows(0)!et_tipork.ToString <> "T" And dttET.Rows(0)!et_tipork.ToString <> "H" Then
                'SOLO PER DOCUMENTI NON DI TIPO CARICO DA PRODUZIONE
                'C'E' UNA RIPROPOSIZIONE DI PREZZI....
                If NTSCDec(e.Row!ec_quant) = 0 Then
                  'Andrà così???
                  If strGestcost = "2" Then
                    'solo costo: inizializzo dquant ad 1 altrimenti non fa bene i calcoli!!!
                    e.Row!ec_quant = 1
                    e.Row!ec_valore = dCostores
                    e.Row!ec_prezzo = ArrDbl(dCostores / NTSCDec(e.Row!ec_quant) * NTSCInt(e.Row!ec_perqta), oCldDocu.TrovaNdecSuPrzUn(0)) ' arrotondare a prezzi unitari!!!
                  Else
                    e.Row!ec_quant = dQtares
                    e.Row!ec_valore = dCostores
                    If dQtares <> 0 Then ' else ?????
                      e.Row!ec_prezzo = ArrDbl(dCostores / dQtares * NTSCInt(e.Row!ec_perqta), oCldDocu.TrovaNdecSuPrzUn(0)) ' arrotondare a prezzi unitari!!!
                    End If
                  End If
                  'Forse era meglio così???
                  'If strGestcost = "2" Then
                  '  'solo costo: inizializzo dQtares ad 1 altrimenti non fa bene i calcoli!!!
                  '  dQtares = 1
                  'End If
                  'griRighe.Columns(QUANT).Value = dQtares
                  'griRighe.Columns(VALOREMM).Value = dCostores
                  'If dQtares <> 0 Then ' else ?????
                  '  griRighe.Columns(PREZZO).Value = ArrDbl(dCostores / dQtares * griRighe.Columns(PERQTA).Value, objStd.TrovaNdecSuPrzUn(0)) ' arrotondare a prezzi unitari!!!
                  'End If
                  '-----------------------
                  e.Row!ec_scont1 = 0
                  e.Row!ec_scont2 = 0
                  e.Row!ec_scont3 = 0
                  e.Row!ec_scont4 = 0
                  e.Row!ec_scont5 = 0
                  e.Row!ec_scont6 = 0
                  e.Row!xxo_pmvalres = dCostores
                  e.Row!xxo_pmqtares = dQtares
                  e.Row!xxo_pmvalrim = 0 '
                  e.Row!xxo_pmqtarim = 0
                  e.Row!ec_pmsalcon = "S"
                  e.Row!xxo_pmqtadisda = dQtares
                  e.Row!xxo_pmvaldisda = dCostores
                  SistemaCampiPMdaCampiPMDA(e.Row)
                  ' e poi cos'altro ? impval. ecc !!!!
                Else ' quantità originaria <> 0 ..
                  '
                  ' lasciamo quelli esistenti, se presenti
                  ' non inizializzo valori e qta : allora ricalcolo
                  If NTSCDec(e.Row!ec_prezzo) = 0 And dQtares <> 0 And strGestcost = "3" Then ' else ?????
                    e.Row!ec_prezzo = ArrDbl(dCostores / dQtares * NTSCInt(e.Row!ec_perqta), oCldDocu.TrovaNdecSuPrzUn(0)) ' arrotondare a prezzi unitari!!!
                    e.Row!ec_scont1 = 0
                    e.Row!ec_scont2 = 0
                    e.Row!ec_scont3 = 0
                    e.Row!ec_scont4 = 0
                    e.Row!ec_scont5 = 0
                    e.Row!ec_scont6 = 0
                  End If
                  If NTSCDec(e.Row!ec_prezzo) = 0 And dCostores <> 0 And strGestcost = "2" Then ' else ?????
                    e.Row!ec_prezzo = ArrDbl(dCostores / NTSCDec(e.Row!ec_quant) * NTSCInt(e.Row!ec_perqta), oCldDocu.TrovaNdecSuPrzUn(0))  ' arrotondare a prezzi unitari!!!
                    e.Row!ec_scont1 = 0
                    e.Row!ec_scont2 = 0
                    e.Row!ec_scont3 = 0
                    e.Row!ec_scont4 = 0
                    e.Row!ec_scont5 = 0
                    e.Row!ec_scont6 = 0
                  End If
                  e.Row!xxo_pmvalres = dCostores
                  e.Row!xxo_pmqtares = dQtares
                  'SistemaPMSalconDaQta(e.Row)     'fatto nella AfterColUpdate_CORPO_ec_pmtaskid: qui ancora ec_pmtaskid non è aggiornato
                End If
              Else
                'PER DOCUMENTI DI TIPO CARICO DA PRODUZIONE
                'NON C'E' UNA RIPROPOSIZIONE DI PREZZI....
                e.Row!xxo_pmvalres = dCostores
                e.Row!xxo_pmqtares = dQtares
                'SistemaPMSalconDaQta(e.Row)    'fatto nella AfterColUpdate_CORPO_ec_pmtaskid: qui ancora ec_pmtaskid non è aggiornato
              End If
              If NTSCStr(e.Row!ec_codart) <> "" And Trim(NTSCStr(e.Row!ec_descr)) = "" Then
                e.Row!ec_descr = NTSCStr(e.Row!xxo_pmtaskid).PadRight(40).Substring(0, 40).Trim
                If NTSCStr(e.Row!xxo_pmtaskid).Length > 40 Then
                  e.Row!ec_desint = NTSCStr(e.Row!xxo_pmtaskid).PadRight(80).Substring(40, 40).Trim
                End If
              End If    'If NTSCStr(e.Row!ec_codart) <> "" And Trim(NTSCStr(e.Row!ec_descr)) = "" Then
            End If    'If Not (oCldDocu.LegTasksCompleteDao(strDittaCorrente, "TTPROESEC", lIIttproesebappo, lIItttasks, _
          End If    'If (strSummary = "S") Then
        End If    'If Not oCldDocu.LegTaskGr(strDittaCorrente, NTSCStr(e.Row!ec
      Else
        If bModPM Then
          'ho azzerato il task: pulisco le colonne del PM
          e.Row!xxo_pmvalres = 0
          e.Row!xxo_pmqtares = 0
          e.Row!xxo_pmvalrim = 0
          e.Row!xxo_pmqtarim = 0
          e.Row!ec_pmsalcon = "C"
          e.Row!xxo_pmqtadisda = 0
          e.Row!xxo_pmvaldisda = 0
          SistemaCampiPMdaCampiPMDA(e.Row)
        End If
      End If    'If NTSCInt(e.ProposedValue) <> 0 Then

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
    End Try
  End Function

  Public Overridable Function BeforeColUpdate_CORPO_ec_pmsalcon(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCStr(e.ProposedValue) <> "S" And NTSCStr(e.Row!ec_pmsalcon) = "S" Then
        If NTSCInt(e.Row!ec_pmtaskid) <> 0 And bInImportRigheOrd = False And bInImportRigheNota = False Then
          'non faccio dare il messaggio in inserimento righe d'ordine, visto hce in alcuni casi prima viene 
          'settato il flag  ="S", poi viene corretta la qta effettivamente prelevata dall'ordine 
          '(il tutto in fase di import riga) per cui il messaggio non ha senso
          'ES: indico l'articolo in riga di movmag, poi faccio selez riga ord e cambio la qta da evadere NON evadendo la riga a saldo
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129987406198924796, "Attenzione: il Task è stato riaperto da 'Totalmente evaso'")))
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_massakg(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not bNoPesiSuRigheKitFittizie Then Return True

      If NTSCStr(e.Row!ec_flkit) = "A" OrElse NTSCStr(e.Row!ec_flkit) = "T" Then e.ProposedValue = 0

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
  Public Overridable Function BeforeColUpdate_CORPO_ec_massaum2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not bNoPesiSuRigheKitFittizie Then Return True

      If NTSCStr(e.Row!ec_flkit) = "A" OrElse NTSCStr(e.Row!ec_flkit) = "T" Then e.ProposedValue = 0

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

  Public Overridable Function BeforeColUpdate_CORPO_ec_coddivi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As DataTable = New DataTable
    Try
      If Not ValCodice(e, "TABDIVI", "xxo_coddivi", oApp.Tr(Me, 129270301463810390, "Codice divisione CA |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"), "N", dttTmp) Then
        e.ProposedValue = e.Row!ec_coddivi
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
  Public Overridable Sub BeforeColUpdate_CORPO_ec_lotto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.Row!ec_codart).Trim = "" Then
        'prima di indicare il lotto devo indicare l'articolo 
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130420443871098739, "Prima di indicare il lotto inserire il cod. articolo")))
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
  Public Overridable Sub BeforeColUpdate_CORPO_xxo_lottox(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.Row!ec_codart).Trim = "" Then
        'prima di indicare il lotto devo indicare l'articolo 
        e.ProposedValue = ""
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130420444028599747, "Prima di indicare il lotto inserire il cod. articolo")))
        Return
      End If

      If NTSCStr(e.ProposedValue).Trim = "" And NTSCStr(e.ProposedValue) <> NTSCStr(e.ProposedValue).Trim Then
        e.ProposedValue = ""
        Return
      End If

      If bLottoNew = False And NTSCStr(e.ProposedValue) <> "" Then
        'vecchia gestione lotti: il lotto è solo numerico di max 9 char: lo formatto
        If NTSCStr(e.ProposedValue) <> "" Then
          If Not IsNumeric(e.ProposedValue) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130420445586734719, "Il codice lotto deve essere un numero compreso tra 0 e 999999999")))
            e.ProposedValue = ""
            Return
          End If
        End If

        e.ProposedValue = NTSCInt(e.ProposedValue).ToString("000000000")

        If NTSCInt(e.ProposedValue) = 0 Then e.ProposedValue = ""
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_contocontr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dtrT As DataRow = Nothing
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xxo_contocon = ""
        Return True
      End If

      If Not oCldDocu.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "ANAGCA", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130772026865059777, "Attenzione!" & vbCrLf & _
                      "Il codice contropartita CA '|" & NTSCInt(e.ProposedValue).ToString & "|' indicato è inesistente.")))
        e.ProposedValue = e.Row!ec_contocontr
      Else
        e.Row!xxo_contocon = strTmp
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_codrepr1out(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      RicalcolaScontiSeAnnullaPromozione(e.Row, NTSCInt(e.Row!ec_codrepr1out))
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_codrepr2out(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      RicalcolaScontiSeAnnullaPromozione(e.Row, NTSCInt(e.Row!ec_codrepr2out))
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_codrepr3out(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      RicalcolaScontiSeAnnullaPromozione(e.Row, NTSCInt(e.Row!ec_codrepr3out))
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_codrepr4out(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      RicalcolaScontiSeAnnullaPromozione(e.Row, NTSCInt(e.Row!ec_codrepr4out))
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_codrepr5out(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      RicalcolaScontiSeAnnullaPromozione(e.Row, NTSCInt(e.Row!ec_codrepr5out))
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_codrepr6out(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      RicalcolaScontiSeAnnullaPromozione(e.Row, NTSCInt(e.Row!ec_codrepr6out))
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

#Region "AfterColUpdate_CORPO"
  Public Overridable Sub AfterColUpdate_CORPO(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bInUnload Then Return
      If bNonValidare Then Return

      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChangesET = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      If bEndEditEC Then
        e.Row.EndEdit()
        e.Row.EndEdit()
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_CORPO_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

      '--------------------------------
      If bModTCO AndAlso bInValidazCodart = False AndAlso bInCalcoloPromozioni = False AndAlso e.Column.ColumnName.ToUpper = "EC_CODART" Then
        'sto validando l'articolo: inserisco anche il dettaglio TCO in CORPOTC
        GestisciDettaglioTCO(1, NTSCInt(e.Row!ec_riga), False)
      End If

      '--------------------------------
      'aggiorno i totali della riga
      If Not bInValidazCodart Then
        Select Case e.Column.ColumnName.ToUpper
          Case "EC_CODART", "EC_QUANT", "EC_PREZZO", "EC_CODIVA", _
               "EC_SCONT1", "EC_SCONT2", "EC_SCONT3", "EC_SCONT4", "EC_SCONT5", "EC_SCONT6", _
               "EC_PROVV", "EC_PROVV2", "EC_FLEVAS", "EC_SCONTV", "EC_SCONTP", "EC_ACNUM"
            SettaValoriRiga(e.Row)
          Case "EC_COLLI"
            'se è settata l'opzione per non cambiare la quantità al cambio dei colli 
            'ed il valore è dato dai colli * prezzo, devo ricalcolare il valore di riga
            If bNoModifQuantSuColli And e.Row!ec_umprz.ToString = "S" Then
              SettaValoriRiga(e.Row)
            End If
        End Select
      End If

      '--------------------------------------------------------------
      'scateno l'evento per abilitare/disabilitare il check scorporo e codice valuta
      If dttEC.Rows.Count < 2 Then ThrowRemoteEvent(New NTSEventArgs("GestScoVal", ""))

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

  Public Overridable Function AfterColUpdate_CORPO_ec_riga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If bInOnAddNewRowImp = False Then
        If e.Row.Table.TableName = "CORPO" Then
          If lCrighe < NTSCInt(e.ProposedValue) Then lCrighe = NTSCInt(e.ProposedValue)
        Else
          If lCrigheYT < NTSCInt(e.ProposedValue) Then lCrigheYT = NTSCInt(e.ProposedValue)
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

  Public Overridable Function AfterColUpdate_CORPO_ec_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim bOk As Boolean = False
    Dim dtrRow() As DataRow = Nothing
    Dim dttTmp As New DataTable
    Dim strStatoProv As String = ""
    Dim strErr As String = ""
    Dim lIdLotto As Integer = 0
    Try
      If BGESTORDLIST Then
        bOk = AfterColUpdate_CORPO_ec_codart_STD(sender, e, False, False, False)
      Else
        bOk = AfterColUpdate_CORPO_ec_codart_STD(sender, e, False, bPrioritaCodeseArticolo, False)
      End If
      If bOk = False Then Return False
      '-----------------------------------------------
      'Se  abilitata l'opzione assegna il magazzino di stock
      If bUsaMagStockArticolo AndAlso dttET.Rows(0)!et_tipork.ToString <> "X" AndAlso dttArti.Rows(0)!ar_magstock.ToString <> "0" Then
        e.Row!ec_magaz = dttArti.Rows(0)!ar_magstock
      End If

      '-------------------------------------
      'Elimina ogni riferimento all'ordine quando si cambia l'articolo
      If BORDINI Then
        If BGESTORDLIST = False Then
          If Val(e.Row!ec_oanum) > 0 Then
            Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128740036614357000, _
                                       "Il cambio del codice articolo di questa riga, comporta l'aggiornamento dei progressivi di " & _
                                       "Ordinato/impegnato Aperto su Codice articolo Diverso." & vbCrLf & _
                                       "Procedere con l'eliminazione di ogni riferimento alla riga d'ordine aperto indicata?"))
            ThrowRemoteEvent(msg)
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              e.Row!ec_oatipo = " "
              e.Row!ec_oaanno = 0
              e.Row!ec_oaserie = " "
              e.Row!ec_oanum = 0
              e.Row!ec_oariga = 0
              e.Row!ec_oasalcon = "C"
            End If
          End If    'If Val(e.Row!ec_oanum) > 0 Then
        End If
      Else
        If Val(e.Row!ec_ornum) > 0 Then
          If NTSCStr(e.Row!ec_tipork) <> "U" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128678655797187500, "Il cambio del codice articolo di questa riga, comporta l'eliminazione di ogni riferimento alla riga d'ordine indicata.")))
            e.Row!ec_ortipo = " "
            e.Row!ec_oranno = 0
            e.Row!ec_orserie = " "
            e.Row!ec_ornum = 0
            e.Row!ec_orriga = 0
            e.Row!ec_salcon = "C"
          Else
            Dim evnt As NTSEventArgs = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 129001797464728125, "Visto che si cambia il codice articolo di questa riga, eliminare ogni riferimento alla riga d'impegno indicata?"))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              e.Row!ec_ortipo = " "
              e.Row!ec_oranno = 0
              e.Row!ec_orserie = " "
              e.Row!ec_ornum = 0
              e.Row!ec_orriga = 0
              e.Row!ec_salcon = "C"
            End If
          End If    'If ntscstr(e.row!ec_tipork) <> "U" Then

          'Azzero il numero d'ordine collegato altrimenti salterebbe la settaprezzo, ecc..., poi alla fine lo reimposto.
          Dim lNum As Integer = NTSCInt(e.Row!ec_ornum)
          e.Row!ec_ornum = 0
          '---------------------------
          'Ricerca del prezzo: se l'articolo è gestito a fasi la eseguo solo al cambio di fase, Ricerca gli sconti e riporta le provvigioni
          SettaCondCommerciali(True, True, True, e.Row, NTSCInt(dttArti.Rows(0)!ar_clascon), nClscan, True)

          e.Row!ec_ornum = lNum
        End If    'If Val(e.Row!ec_ornum) > 0 Then

        If Val(e.Row!ec_clnum) > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128678656801406250, "Il cambio del codice articolo di questa riga, comporta l'eliminazione di ogni riferimento alla riga di carico di C/Lavoro indicata.")))
          e.Row!ec_cltipo = DBNull.Value
          e.Row!ec_clanno = 0
          e.Row!ec_clserie = DBNull.Value
          e.Row!ec_clnum = 0
          e.Row!ec_clriga = 0
        End If    'If Val(e.Row!ec_clnum) > 0 Then

        If Val(e.Row!ec_cvnum) > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129605648992988282, "Il cambio del codice articolo di questa riga, comporta l'eliminazione di ogni riferimento alla riga di carico di C/visione indicata.")))
          e.Row!ec_cvtipo = DBNull.Value
          e.Row!ec_cvanno = 0
          e.Row!ec_cvserie = DBNull.Value
          e.Row!ec_cvnum = 0
          e.Row!ec_cvriga = 0
        End If    'If Val(e.Row!ec_cvnum) > 0 Then

        If Val(e.Row!ec_npnum) > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128678657519531250, "Il cambio del codice articolo di questa riga, comporta l'eliminazione di ogni riferimento alla riga di nota indicata.")))
          e.Row!ec_nptipo = DBNull.Value
          e.Row!ec_npanno = 0
          e.Row!ec_npserie = DBNull.Value
          e.Row!ec_npnum = 0
          e.Row!ec_npriga = 0
          e.Row!ec_npsalcon = "C"
        End If    'If Val(e.Row!ec_npnum) > 0 Then

        If Val(e.Row!ec_ccnum) > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128678658208750000, "Il cambio del codice articolo di questa riga, comporta l'eliminazione di ogni riferimento alla riga di Carico di produzione (C/Lav. attivo).")))
          e.Row!ec_cctipo = DBNull.Value
          e.Row!ec_ccanno = 0
          e.Row!ec_ccserie = DBNull.Value
          e.Row!ec_ccnum = 0
          e.Row!ec_ccriga = 0
        End If    'If Val(e.Row!ec_ccnum) > 0 Then

      End If    'If BORDINI Then

      If (strLottoXDaAnalotti.Trim <> "") And (e.Row.Table.Columns.Contains("xxo_lottox")) Then
        e.Row!xxo_lottox = strLottoXDaAnalotti
      End If

      '--------------------------------
      If BORDINI = False Then
        'solo per doc di magaz (per ora) se ho letto come cod. articolo un barcode che conteneva anche lotto
        'matricola, data prod lotto, data scad. lotto compilo anche quelle informazioni
        If strEAN128Matric <> "" And NTSCStr(dttEC.Rows(dttEC.Rows.Count - 1)!xxo_gestmatr) = "S" Then
          dttMOVMATR.Rows.Add(dttMOVMATR.NewRow)
          With dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)
            !codditt = strDittaCorrente
            'Considero che appartengano all'ultima riga importata
            !mma_tipork = dttET.Rows(0)!et_tipork
            !mma_anno = dttET.Rows(0)!et_anno
            !mma_serie = dttET.Rows(0)!et_serie
            !mma_numdoc = dttET.Rows(0)!et_numdoc
            !mma_riga = dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga
            !mma_rigaa = NTSCInt(dttMOVMATR.Compute("MAX(mma_rigaa)", "mma_riga = " & NTSCInt(!mma_riga))) + 1
            !mma_matric = strEAN128Matric
            !mma_quant = NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_quant)
            If NTSCInt(!mma_quant) <= 0 Then !mma_quant = 1
          End With
        End If    'If strEAN128Matric <> "" And NTSCStr(dttEC.Rows(dttEC.Rows.Count - 1)!xxo_gestmatr) = "S" Then

        If strEAN128Lotto <> "" And NTSCStr(dttEC.Rows(dttEC.Rows.Count - 1)!xxo_geslotti) = "S" Then
          'se l'anagrafica lotto esiste già non devo crearla, diversamente devo farlo impostando, se presenti, le date di prod e scad
          oCldDocu.ValCodiceDb(strEAN128Lotto, strDittaCorrente, "ANALOTTIX", "S", "", dttTmp, NTSCStr(e.Row!ec_codart))
          If dttTmp.Rows.Count = 0 Then
            dttTmp.Clear()
            'creo l'anagrafica lotti
            oCldDocu.ValCodiceDb(NTSCInt(dttET.Rows(0)!et_conto).ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
            If dttTmp.Rows.Count > 0 Then strStatoProv = NTSCStr(dttTmp.Rows(0)!an_stato).Trim
            dttTmp.Clear()

            If Not CType(oCleComm, CLELBMENU).CreaAnalottiDaLottox(strDittaCorrente, NTSCStr(e.Row!ec_codart), _
                                              NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                              strEAN128Lotto, strStatoProv, lIdLotto, strErr, strEAN128DtProd, _
                                              strEAN128DtScad) Then
              'ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130420446337989527, "Codice lotto inesistente") & vbCrLf & strErr))
              e.Row!xxo_lottox = ""
            Else
              If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))

              If VisMsg() And oApp.Batch = False And bApriAnalottiDopoNew Then
                ThrowRemoteEvent(New NTSEventArgs("APRIANALO:", NTSCStr(e.Row!ec_codart) & "§" & NTSCStr(lIdLotto)))
              End If
              e.Row!xxo_lottox = strEAN128Lotto
            End If
          Else
            e.Row!xxo_lottox = strEAN128Lotto
          End If
        End If    'If strEAN128Lotto <> "" And NTSCStr(dttEC.Rows(dttEC.Rows.Count - 1)!xxo_geslotti) = "S" Then

      End If    'If BORDINI = False Then

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
      bCambioArticoloScarico = False
    End Try
  End Function

  Public Overridable Function AfterColUpdate_CORPO_ec_codart_STD(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs, _
                                                             ByVal bUsaMagStockArticolo As Boolean, ByVal bPrioritaCodeseArticolo As Boolean, _
                                                             ByVal bBD_CodArfo As Boolean) As Boolean
    Dim dttTmp As DataTable = New DataTable()
    Dim dtrT As DataRow = Nothing
    Dim strThrowEvent As String = ""            'contiene tutti gli eventi che devono essere eseguiti dall'ui
    Dim strUmdapr As String = ""
    Dim dQuant As Decimal = 0
    Dim strError As String = ""                 'utilizzata dal DAL per comunicare messaggi di errore
    Dim strTmp As String = ""

    Dim strVistato As String = ""
    Dim strDescr As String = ""
    Dim strDesint As String = ""
    Dim strNote As String = ""
    Dim dQtaIntra2 As Decimal = 0
    Dim lTmp As Integer = 0

    Try
      strEAN128Barcode = ""
      bInValidazCodart = True

      If NTSCStr(e.Row!ec_codart).Trim = "" Then Return False

      '-----------------------------------------------
      'leggo l'articolo e se non lo trovo vuol dire che 
      'ho già dato il messaggio nella BeforeCollUpdate di Articolo Inesistente
      If GetMemDttArti(e.Row!ec_codart.ToString).Rows.Count = 0 Then Return False

      bEndEditEC = False

      '-----------------------------------------------
      'toglie rif al task se presente
      If NTSCInt(e.Row!ec_pmtaskid) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222096875000, "Attenzione ! il cambio articolo comporta l'eliminazione del riferimento al task!")))
        e.Row!ec_pmtaskid = 0
        e.Row!ec_pmqtadis = 0
        e.Row!ec_pmvaldis = 0
        e.Row!ec_pmsalcon = "C"
        e.Row!xxo_pmtaskid = ""
        e.Row!xxo_pmqtadisda = 0
        e.Row!xxo_pmvaldisda = 0
        e.Row!xxo_pmqtares = 0
        e.Row!xxo_pmvalres = 0
        e.Row!xxo_pmqtarim = 0
        e.Row!xxo_pmvalrim = 0
      End If

      If NTSCStr(e.Row!ec_tipork) = "U" Then
        bCambioArticoloScarico = True
      Else
        bCambioArticoloScarico = False
      End If

      e.Row!ec_ump = dttArti.Rows(0)!ar_unmis
      '-----------------------------------------------
      'propongo l'unità di misura per i colli (solo se l'articolo è diverso da M o D)
      If Not (e.Row!ec_codart.ToString = "D" OrElse e.Row!ec_codart.ToString = "M") Then
        If bDocEmesso OrElse e.Row!ec_tipork.ToString = "Y" OrElse e.Row!ec_tipork.ToString = "U" Then
          strUmdapr = dttArti.Rows(0)!ar_umdapr.ToString
        Else
          strUmdapr = dttArti.Rows(0)!ar_umdapra.ToString
        End If
        Select Case strUmdapr
          Case "P"
            e.Row!ec_unmis = dttArti.Rows(0)!ar_unmis.ToString
          Case "S"
            If dttArti.Rows(0)!ar_unmis2.ToString.Trim <> "" Then e.Row!ec_unmis = dttArti.Rows(0)!ar_unmis2.ToString
          Case "C"
            If dttArti.Rows(0)!ar_confez2.ToString.Trim <> "" Then e.Row!ec_unmis = dttArti.Rows(0)!ar_confez2.ToString
          Case "Q"
            If dttArti.Rows(0)!ar_um4.ToString.Trim <> "" Then e.Row!ec_unmis = dttArti.Rows(0)!ar_um4.ToString
          Case Else
            e.Row!ec_unmis = dttArti.Rows(0)!ar_unmis.ToString
        End Select
      Else
        e.Row!ec_unmis = dttArti.Rows(0)!ar_unmis
      End If
      If NTSCStr(e.Row!ec_unmis) = "" Then e.Row!ec_unmis = dttArti.Rows(0)!ar_unmis

      '---------------------------
      'Riporta il tipo kit: lo imposto qui perchè poi nella settaprezzo deve testare ec_flkit ...
      If dttET.Rows(0)!et_tipork.ToString <> "H" And dttET.Rows(0)!et_tipork.ToString <> "T" And BGESTORDLIST = False Then
        e.Row!ec_flkit = dttArti.Rows(0)!ar_tipokit
        If NTSCStr(dttArti.Rows(0)!ar_tipokit).Trim = "" Then
          e.Row!ec_ktriga = 0
        End If
      End If

      '----------------------------
      'verifico se il prezzo  dipendente dall'unità di misura oppure no
      'il prezzo per unità di misura è gestito solo se è stata settata l'apposita opzione di registro e se non sono negli impegni/scarichi collegati
      If CType(oCleComm, CLELBMENU).GestioneUMPrezzo(bGestionePrezzi, IIf(BORDINI, "BSORGSOR", "BSVEBOLL").ToString, _
                                   dttET.Rows(0)!et_tipork.ToString, _
                                   bTerzista, e.Row!ec_codart.ToString, e.Row!ec_unmis.ToString, _
                                   strDittaCorrente) And _
                                   Not ((dttET.Rows(0)!et_tipork.ToString = "H" Or _
                                   dttET.Rows(0)!et_tipork.ToString = "T") And _
                                   e.Row.Table.TableName.ToUpper = "CORPOIMP") Then
        e.Row!ec_umprz = "S"
      Else
        e.Row!ec_umprz = "N"
      End If

      '---------------------------
      'Rettifica quantità: se gi la riga possiede una quantità nei colli
      'ridetermina la quantità UMP in base all'eventaue nuovo fattore
      'di conversione
      If (bModTCO = False OrElse NTSCInt(dttArti.Rows(0)!ar_codtagl) = 0) AndAlso NTSCDec(e.Row!ec_colli.ToString) <> 0 Then
        If Not bCambioArticoloScarico Then
          If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, e.Row!ec_codart.ToString, e.Row!ec_unmis.ToString, _
                          NTSCDec(e.Row!ec_colli), _
                          NTSCDec(e.Row!ec_misura1), NTSCDec(e.Row!ec_misura2), _
                          NTSCDec(e.Row!ec_misura3), dQuant, False, strError, oApp.NDecQta) Then
            e.Row!ec_quant = dQuant
          Else
            e.Row!ec_quant = 0
          End If
          If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
        End If
      End If

      '---------------------------
      'Setta il codice IVA se non esiste quello di esenzione in testata
      If NTSCInt(dttET.Rows(0)!et_codese.ToString) = 0 Then
        e.Row!ec_codiva = dttArti.Rows(0)!ar_codiva
      Else
        'se attiva l'opzione di registro "PrioritaCodeseArticolo",
        'se codice Esenzione di testata è diversa da zero, se tipo Iva articolo
        'è = 2 (Op. Esenti/Non imp.), propone ARTICO.ar_codiva
        If bPrioritaCodeseArticolo AndAlso NTSCInt(dttArti.Rows(0)!ar_codiva) <> 0 AndAlso e.Row.RowState = DataRowState.Added Then
          dtrT = GetTabcivaRow(NTSCInt(dttArti.Rows(0)!ar_codiva))
          If NTSCInt(dtrT!tb_tipiva) = 2 Then e.Row!ec_codiva = dttArti.Rows(0)!ar_codiva
        Else
          e.Row!ec_codiva = dttET.Rows(0)!et_codese
        End If
      End If

      If strUnMisDaBarcode <> "" Then e.Row!ec_unmis = strUnMisDaBarcode
      If dColliDaBarcode > 0 Then e.Row!ec_colli = dColliDaBarcode
      If dQuantDaBarcode > 0 Then e.Row!ec_quant = dQuantDaBarcode

      '---------------------------
      'Propone la fase da ARTICO (solo se si possiede il modulo logistica extended)
      If dttArti.Rows(0)!ar_gesfasi.ToString = "N" OrElse bModLEX = False Then
        e.Row!ec_fase = 0
      Else
        If NTSCInt(e.Row!ec_fase) = NTSCInt(dttArti.Rows(0)!ar_ultfase) Then e.Row!ec_fase = 0

        If nFaseArticoloDaBarcode > 0 Then
          'se ho letto il barcode al posto del cod. articolo, imposto la fase dal barcode
          e.Row!ec_fase = nFaseArticoloDaBarcode
        Else
          e.Row!ec_fase = dttArti.Rows(0)!ar_ultfase
        End If
      End If

      '---------------------------
      'setta perqta
      e.Row!ec_perqta = dttArti.Rows(0)!ar_perqta
      If NTSCInt(dttArti.Rows(0)!ar_perqta) = 0 Then e.Row!ec_perqta = 1

      '-----------------------------
      'Famiglia articolo
      If dttArti.Rows(0)!ar_famprod.ToString <> "" Then
        If (e.Row!ec_tipork.ToString = "Y" Or e.Row!ec_tipork.ToString = "U") And NTSCStr(e.Row!ec_codcfam).Trim <> "" Then
          'mantengo quello passato dall'ordine di prod
        Else
          e.Row!ec_codcfam = dttArti.Rows(0)!ar_famprod
        End If
      End If

      '---------------------------
      'Centro contabilità analitica
      If NTSCInt(dttArti.Rows(0)!ar_numecr) <> 0 Then
        If (e.Row!ec_tipork.ToString = "Y" Or e.Row!ec_tipork.ToString = "U") And NTSCInt(e.Row!ec_codcena) <> 0 Then
          'mantengo quello passato dall'ordine di prod
        Else
          e.Row!ec_codcena = dttArti.Rows(0)!ar_numecr
        End If
      End If

      '------------------------------------
      'Ricerca la descrizione dell'articolo
      e.Row!xxo_tipoopz = dttArti.Rows(0)!ar_tipoopz
      e.Row!xxo_geslotti = dttArti.Rows(0)!ar_geslotti
      e.Row!xxo_gesubic = dttArti.Rows(0)!ar_gesubic
      e.Row!xxo_gescomm = dttArti.Rows(0)!ar_gescomm
      e.Row!xxo_gestmatr = dttArti.Rows(0)!ar_gestmatr
      e.Row!xxo_gesfasi = dttArti.Rows(0)!ar_gesfasi
      e.Row!xxo_gruppo = dttArti.Rows(0)!ar_gruppo
      e.Row!xxo_sotgru = dttArti.Rows(0)!ar_sotgru
      If dttEC.Columns.Contains("xxo_codalt") Then e.Row!xxo_codalt = NTSCStr(dttArti.Rows(0)!ar_codalt)
      e.Row!ec_descr = dttArti.Rows(0)!ar_descr
      e.Row!ec_desint = dttArti.Rows(0)!ar_desint
      e.Row!ec_note = dttArti.Rows(0)!ar_note
      If nTabling > 0 Then
        If CType(oCleComm, CLELBMENU).TrovaCodartDescrLingua(strDittaCorrente, e.Row!ec_codart.ToString, nTabling, strDescr, strDesint, strNote) Then
          e.Row!ec_descr = strDescr
          e.Row!ec_desint = strDesint
          e.Row!ec_Note = strNote
        End If
      End If
      If dttArti.Rows(0)!ar_pesoca.ToString = "1" Then e.Row!ec_note = ""

      If BORDINI = False Then
        'veboll 'Intrastat
        e.Row!ec_codnomc = dttArti.Rows(0)!ar_codnomc
        'per servizi nella provincia di origine memorizzo lo stato del cliente/fornitore, ovvero 'paese pagamento'
        'i servizi hanno codice nomenclatura che inizia per 'S.'
        If NTSCStr(e.Row!ec_codnomc).PadRight(2).ToUpper.Substring(0, 2) = "S." Then
          oCldDocu.ValCodiceDb(NTSCStr(dttEt_conto.Rows(0)!an_stato), strDittaCorrente, "TABSTAT", "S", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            e.Row!ec_proorig = Left(NTSCStr(dttTmp.Rows(0)!tb_siglaiso), 2)
          Else
            e.Row!ec_proorig = Left(NTSCStr(dttEt_conto.Rows(0)!an_stato), 2)
          End If
        Else
          If bDocEmesso Then
            e.Row!ec_proorig = dttArti.Rows(0)!ar_prorig
          Else
            e.Row!ec_proorig = dttArti.Rows(0)!ar_paeorig
          End If
        End If

        e.Row!ec_percvst = dttArti.Rows(0)!ar_percvst
        e.Row!ec_massakg = NTSCDec(e.Row!ec_quant) * NTSCDec(dttArti.Rows(0)!ar_pesonet)
        If e.ProposedValue.ToString <> "D" And e.ProposedValue.ToString <> "M" Then
          CType(oCleComm, CLELBMENU).ConvQuantUMIntra2(strDittaCorrente, e.ProposedValue.ToString, e.Row!ec_ump.ToString, _
                                                        NTSCDec(e.Row!ec_quant), NTSCDec(e.Row!ec_misura1), NTSCDec(e.Row!ec_misura2), _
                                                        NTSCDec(e.Row!ec_misura3), dQtaIntra2)
          e.Row!ec_massaum2 = dQtaIntra2
        Else
          e.Row!ec_massaum2 = NTSCDec(e.Row!ec_quant)
        End If

      End If    'If BORDINI = False Then

      '------------------------------------
      'Setta la contropartita dell'articolo se  a 0
      If e.Row!ec_tipork.ToString = "Y" Or e.Row!ec_tipork.ToString = "U" Then
        e.Row!ec_controp = NTSCInt(dttArti.Rows(0)!ar_contros)
      Else
        If NTSCInt(dttET.Rows(0)!et_controp) = 0 Then
          If bDocEmesso And dttET.Rows(0)!et_tipork.ToString <> "Z" Then    'le bolle di movimentaz. interna sono doc. emessi ma usano contropartite di acquisto
            'Se un documento di vendita
            If NTSCInt(dttET.Rows(0)!et_tipobf) = NTSCInt(dttPeve.Rows(0)!tb_tpbfresfor) And NTSCInt(dttPeve.Rows(0)!tb_tpbfresfor) <> 0 Then
              'se il tipobf è uquale a RESO A FORNITORE (tabpecg.tb_tpbfresfor) uso la contropartiva acquisti (storno)
              AssegnaContropartitaRiga(e.Row, NTSCInt(dttArti.Rows(0)!ar_controa), lAgControp)
            Else
              'caso standard
              AssegnaContropartitaRiga(e.Row, NTSCInt(dttArti.Rows(0)!ar_controp), lAgControp)
            End If
          Else
            'Se un documento di acquisto
            If NTSCInt(dttET.Rows(0)!et_tipobf) = NTSCInt(dttPeve.Rows(0)!tb_tpbfrescli) And NTSCInt(dttPeve.Rows(0)!tb_tpbfrescli) <> 0 Then
              'se il tipobf è uquale a RESO DA CLIENTE (tabpecg.tb_tpbfrescli) uso la contropartiva vendite (storno)
              AssegnaContropartitaRiga(e.Row, NTSCInt(dttArti.Rows(0)!ar_controp), lAgControp)
            Else
              'caso standard
              AssegnaContropartitaRiga(e.Row, NTSCInt(dttArti.Rows(0)!ar_controa), lAgControp)
            End If
          End If
        Else
          e.Row!ec_controp = dttET.Rows(0)!et_controp
        End If    'If NTSCdec(dttET.Rows(0)!et_controp) = 0 Then
      End If

      '----------------------------------
      'Propone l'ubicazione da ARTMAGA (se non esiste la prende da ARTICO)
      If Not BGESTORDLIST Then
        e.Row!xxo_lottox = ""
        If Not bModSupRme Then
          If dttArti.Rows(0)!ar_gesubic.ToString = "S" Then
            strTmp = CType(oCleComm, CLELBMENU).TrovaUbicazionePerMagazzino(strDittaCorrente, e.Row!ec_codart.ToString, NTSCInt(e.Row!ec_magaz))
            If strTmp = "" Then strTmp = dttArti.Rows(0)!ar_ubicaz.ToString
            If strTmp <> "" Then
              e.Row!ec_ubicaz = strTmp
            Else
              e.Row!ec_ubicaz = " "
            End If
          End If
        Else
          ' Se sono in VEBOLL allora propongo i dati di matricola, ubicazione e lotto
          strTmp = ""
          If BORDINI Then
            strVistato = "." ' se è un ordine passo .
          Else
            strVistato = NTSCStr(dttET.Rows(0)!et_vistato)
          End If

          strTmp = ""

          PropostaUbicazione(IIf(BORDINI, "BNORGSOR", "BNVEBOLL").ToString, ntscstr(e.row!ec_tipork), strVistato, NTSCStr(e.ProposedValue), _
                               NTSCInt(e.Row!ec_fase), NTSCInt(e.Row!ec_magaz), " ", strTmp)
          e.Row!ec_ubicaz = strTmp

          If Not BORDINI AndAlso bDocEmesso Then
            If Not ScaricaLottox_CheckLogistica(e.Row) Then
              'Proposta del lotto se non siamo in gestione ordini
              lTmp = 0
              strTmp = ""
              PropostaLotto("BNVEBOLL", NTSCStr(e.Row!ec_tipork), NTSCStr(dttET.Rows(0)!et_vistato), NTSCStr(e.ProposedValue), _
                            NTSCInt(e.Row!ec_fase), NTSCInt(e.Row!ec_magaz), 0, lTmp, strTmp)
              If lTmp <> 0 Then e.Row!ec_lotto = lTmp 'lotto dall'ordine
              If strTmp <> "" Then e.Row!xxo_lottox = strTmp 'lotto da tabmaga (lotto generico)
            End If

            ' per il momento la proposta matricola non è gestita
            'strTmp = ""
            'PropostaMatricola("BNVEBOLL", NTSCStr(dttET.Rows(0)!et_tipork), NTSCStr(dttET.Rows(0)!et_vistato), NTSCStr(e.ProposedValue), _
            '                  NTSCInt(e.Row!ec_fase), NTSCInt(e.Row!ec_magaz), NTSCStr(e.Row!xxo_matric), strTmp)
          End If
        End If
      End If

      ' Se ho riscritto lo stesso articolo allora non ricalcolo i prezzi
      If strOldArt.Trim <> NTSCStr(e.Row!ec_codart).Trim Then
        '---------------------------
        'Ricerca del prezzo: se l'articolo è gestito a fasi la eseguo solo al cambio di fase + Ricerca gli sconti
        '------------------------------------
        'Riporta le provvigioni
        'solo se il codice promozione è = 0, se è diverso da o le provvigioni sono già state calcolate al cambio del codice promozione
        'poche righe qui sopra ...
        SettaCondCommerciali(dttArti.Rows(0)!ar_gesfasi.ToString = "N", True, (BGESTORDLIST = False AndAlso NTSCInt(e.Row!ec_codtpro) = 0), e.Row, NTSCInt(dttArti.Rows(0)!ar_clascon), nClscan, True)

        '---------------------------
        'Per gli Impegni cliente aperti e non,
        'Se documento emesso, se siamo in nuova riga
        'se siamo in nuova riga e c'è l'opzione di registro abilitata
        'parte il meccanismo automatico sulle promozioni
        If (BORDINI And _
           (dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#" Or _
            dttET.Rows(0)!et_tipork.ToString = "V" Or dttET.Rows(0)!et_tipork.ToString = "Q")) Or _
           (BORDINI = False And bDocEmesso) Then
          If e.Row.RowState = DataRowState.Added Then
            If bAbilitaPromozioni AndAlso bInImportRigheOff = False AndAlso bInImportRigheOrd = False AndAlso _
               bInIncollaRiga = False AndAlso bInDuplicadoc = False AndAlso bInImportKit = False AndAlso _
               bInImportConai = False AndAlso bInIncollaRiga = False AndAlso bInImportRigheNota = False AndAlso _
               bInCalcoloPromozioni = False Then
              'Prima vediamo se esiste una qualche promozione "valida" contenente tale articolo
              If oCldDocu.VerificaPresenzaPromozioni(strDittaCorrente, NTSCStr(e.ProposedValue), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString) Then
                'Se esiste, allora diamo la possibilità di vederla e di sceglierla
                Dim evnt As New NTSEventArgs("LeggiPromo:" & NTSCStr(e.ProposedValue), "")
                ThrowRemoteEvent(evnt)
                e.Row!ec_codtpro = NTSCInt(evnt.RetValue)
              End If
            End If
          End If
        End If    'If dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#" Or dttET.Rows(0)!et_tipork.ToString = "V" Then
      End If    'If strOldArt.trim <> NTSCStr(e.Row!ec_codart).trim Then

      '----------------------------------
      'Legge i progressivi per vedere la disponibilità in magazzino
      If bInImportRigheDiba = False And bInImportRigheOrd = False And _
         bInImportRigheNota = False And bInImportRigheOff = False And _
         bInDuplicadoc = False Then strThrowEvent = strThrowEvent & "LeggiDispo:" & e.Row!ec_codart.ToString & "§" & e.Row!ec_magaz.ToString & "§" & e.Row!ec_fase.ToString & "§" & e.Row!ec_commeca.ToString & "|"

      '----------------------------------
      'Setta un flag di form che indica se l'articolo  gestito per varianti taglie e colori
      'N.B.: non si possono indicare tali articoli se il tipo di ordine/impegno 
      '      Ordine fornitore aperto o Impegno cliente aperto
      If bModTCO = True Then
        e.Row!xxo_codtagl = dttArti.Rows(0)!ar_codtagl
      End If

      If NTSCInt(dttArti.Rows(0)!ar_codtagl) <> 0 Then
        'Azzera le colonne relative alla quantità
        If BORDINI Then
          e.Row!ec_colli = 0
          If BGESTORDLIST = False Then
            e.Row!ec_colpre = 0
            e.Row!ec_coleva = 0
          End If
          e.Row!ec_quant = 0
          If BGESTORDLIST = False Then
            e.Row!ec_quapre = 0
            e.Row!ec_quaeva = 0
            e.Row!xxo_qtadaass = 0
          End If
        Else
          e.Row!ec_colli = 0
          e.Row!ec_coldisimp = 0
          e.Row!ec_quant = 0
          e.Row!ec_qtadisimp = 0
          e.Row!ec_qtafatt = 0
        End If

        If e.Row!ec_tipork.ToString <> "Y" And e.Row!ec_tipork.ToString <> "U" Then strThrowEvent = strThrowEvent & "ArticoloTC:" & "S" & "|"
      Else
        If e.Row!ec_tipork.ToString <> "Y" And e.Row!ec_tipork.ToString <> "U" Then strThrowEvent = strThrowEvent & "ArticoloTC:" & "N" & "|"
      End If


      '---------------------------
      'compila codarfo
      If BGESTORDLIST = False AndAlso e.Row!ec_tipork.ToString <> "Y" AndAlso e.Row!ec_tipork.ToString <> "U" Then
        e.Row!xxo_codarfo = CType(oCleComm, CLELBMENU).TrovaCodarfoDaCodart(e.Row!ec_codart.ToString, NTSCInt(dttET.Rows(0)!et_conto), _
                                                          strDittaCorrente, bBD_CodArfo, e.Row!ec_matric.ToString)
      End If

      RiportaMisuraDaArtico(e.Row, False)

      '-----------------------------------------------
      'avviso l'UI delle operazioni grafiche che devono essere eseguite a causa della validazione del conto
      If strThrowEvent <> "" Then ThrowRemoteEvent(New NTSEventArgs(strThrowEvent.Substring(0, strThrowEvent.Length - 1), ""))

      If BORDINI And strVisNoteArti = "-1" Then
        If NTSCStr(dttArti.Rows(0)!ar_note).Trim <> "" Then
          ThrowRemoteEvent(New NTSEventArgs("VNoteArtic:" & NTSCStr(dttArti.Rows(0)!ar_note), ""))
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
    Finally
      dttTmp.Clear()
      bInValidazCodart = False
      bEndEditEC = True
    End Try
  End Function

  Public Overridable Function AfterColUpdate_CORPO_ec_matric(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    'validazione colonna BARCODE
    Dim strCodart As String = ""
    Dim nFase As Integer = 0
    Dim strUnmis As String = ""
    Dim dColli As Decimal = 0
    Dim dQuant As Decimal = 0
    Dim strErrore As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim dsBarcode As New DataSet
    Dim strTaglia As String = ""

    Try
      If bDisabilitaDecodificaBarcode Then Return True

      If NTSCStr(e.Row!ec_codart).Trim = "" And NTSCStr(e.ProposedValue).Trim <> "" Then
        strCodart = CType(oCleComm, CLELBMENU).TrovaCodartDaBarcode(IIf(BORDINI, "BSORGSOR", "BSVEBOLL").ToString, _
                                                  e.Row!ec_matric.ToString, strDittaCorrente, _
                                                  strUnmis, dQuant, dColli, nFase, strErrore, dsBarcode)
        If dsBarcode.Tables("BARCODE").Rows.Count <> 0 Then strTaglia = NTSCStr(dsBarcode.Tables("BARCODE").Rows(0)!bc_tagl)
        '---------------------------------------
        'ho il codice articolo
        If strCodart <> "" Then
          e.Row!ec_codart = strCodart
          If NTSCInt(e.Row!ec_fase) <> nFase Then e.Row!ec_fase = nFase

          '---------------------------------------
          'adesso forzo le quantità in base alle impostazioni sul barcode
          'tutti i test di modulo TCO, bBarcodeConQta e bBarcodeDerogaQta sono stati eseguiti nella 'TrovaCodartDaBarcode'
          If NTSCStr(e.Row!ec_unmis) <> strUnmis Then e.Row!ec_unmis = strUnmis
          e.Row!ec_colli = dColli
          'If bBarcodeDerogaQta Then e.Row!ec_quant = dQuant
          If NTSCStr(e.Row!ec_unmis) <> NTSCStr(e.Row!ec_ump) Then
            If Not bBarcodeDerogaQta Then
              If NTSCInt(e.Row!xxo_codtagl) = 0 Then
                If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, e.Row!ec_codart.ToString, e.Row!ec_unmis.ToString, _
                                NTSCDec(e.Row!ec_colli), _
                                NTSCDec(e.Row!ec_misura1), NTSCDec(e.Row!ec_misura2), _
                                NTSCDec(e.Row!ec_misura3), dQuant, False, strErrore, NTSCInt(IIf(BORDINI, oApp.NDecQta, nCO2_ArrQuant))) Then
                  e.Row!ec_quant = dQuant
                Else
                  e.Row!ec_quant = 0
                End If
                ThrowRemoteEvent(New NTSEventArgs("", strErrore))
              Else
                e.Row!ec_quant = dQuant
              End If
            Else
              e.Row!ec_quant = dQuant
            End If
          Else
            e.Row!ec_quant = dQuant
          End If

          'per dettaglio TCO:
          'carico la quantità del barcode nel dettaglio TCO (che è stato creato in fase di validazione articolo nella aftercolupdate_corpo)
          If bModTCO Then
            dtrT = dttECTC.Select("ec_riga = " & e.Row!ec_riga.ToString)
            If dtrT.Length > 0 Then
              'in vb6 routine InserisciQuantitaInMOTRANSTC
              'Preleva il codice taglia dall'anagrafica dell'articolo e la posizione nella quale indicare la quantità del BARCODE
              oCldDocu.ValCodiceDb(e.Row!xxo_codtagl.ToString, strDittaCorrente, "TABTAGL", "S", "", dttTmp)
              If dttTmp.Rows.Count > 0 Then
                For i = 1 To 24
                  If strTaglia.ToUpper = NTSCStr(dttTmp.Rows(0)("tb_dest" & i.ToString("00"))).ToUpper Then
                    dtrT(0)("ec_quant" & i.ToString("00")) = NTSCDec(NTSCStr(dsBarcode.Tables("BARCODE").Rows(0)!bc_quant))
                    e.Row!ec_quant = NTSCDec(NTSCStr(dsBarcode.Tables("BARCODE").Rows(0)!bc_quant))
                    Exit For
                  End If
                Next
              End If
              dttTmp.Clear()
            End If    'If dtrT.Length > 0 Then
          End If    'If bModTCO Then

        End If    'If strCodart <> "" Then
      End If    'If e.Row!ec_codart.ToString <> "" Then

      '---------------------------------------
      'solo alla fine do gli eventuali allert sulla decodifica barcode ....
      If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))

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
      dsBarcode.Clear()
    End Try
  End Function

  Public Overridable Function AfterColUpdate_CORPO_ec_fase(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strVistato As String = ""
    Dim strTmp As String = ""

    Try
      bInValidazFaseCodart = True
      '--------------------------
      'Ricerca del prezzo (passo di qui anche quando cambio ec_codart se l'articolo è gestito a fasi)
      SettaCondCommerciali(True, False, False, e.Row, 0, 0, False)

      '--------------------------
      'Elimina ogni riferimento all'ordine quando si cambia la fase
      If BORDINI = False Then
        If Val(e.Row!ec_ornum) > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128679575051406250, "Il cambio della fase articolo di questa riga, comporta l'eliminazione di ogni riferimento alla riga d'ordine indicata.")))
          e.Row!ec_ortipo = " "
          e.Row!ec_oranno = 0
          e.Row!ec_orserie = " "
          e.Row!ec_ornum = 0
          e.Row!ec_orriga = 0
          e.Row!ec_salcon = "C"
        End If    'If Val(e.Row!ec_ornum) > 0 Then

        If Val(e.Row!ec_clnum) > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128679575032187500, "Il cambio della fase articolo di questa riga, comporta l'eliminazione di ogni riferimento alla riga di carico di C/Lavoro indicata.")))
          e.Row!ec_cltipo = DBNull.Value
          e.Row!ec_clanno = 0
          e.Row!ec_clserie = DBNull.Value
          e.Row!ec_clnum = 0
          e.Row!ec_clriga = 0
        End If    'If Val(e.Row!ec_clnum) > 0 Then

        If Val(e.Row!ec_cvnum) > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129605649231269532, "Il cambio della fase articolo di questa riga, comporta l'eliminazione di ogni riferimento alla riga di carico di C/visione indicata.")))
          e.Row!ec_cvtipo = DBNull.Value
          e.Row!ec_cvanno = 0
          e.Row!ec_cvserie = DBNull.Value
          e.Row!ec_cvnum = 0
          e.Row!ec_cvriga = 0
        End If    'If Val(e.Row!ec_cvnum) > 0 Then

        If Val(e.Row!ec_npnum) > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128679575019687500, "Il cambio della fase codice articolo di questa riga, comporta l'eliminazione di ogni riferimento alla riga di nota indicata.")))
          e.Row!ec_nptipo = DBNull.Value
          e.Row!ec_npanno = 0
          e.Row!ec_npserie = DBNull.Value
          e.Row!ec_npnum = 0
          e.Row!ec_npriga = 0
          e.Row!ec_npsalcon = "C"
        End If    'If Val(e.Row!ec_npnum) > 0 Then

        If Val(e.Row!ec_ccnum) > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128679575006093750, "Il cambio della fase codice articolo di questa riga, comporta l'eliminazione di ogni riferimento alla riga di Carico di produzione (C/Lav. attivo).")))
          e.Row!ec_cctipo = DBNull.Value
          e.Row!ec_ccanno = 0
          e.Row!ec_ccserie = DBNull.Value
          e.Row!ec_ccnum = 0
          e.Row!ec_ccriga = 0
        End If    'If Val(e.Row!ec_ccnum) > 0 Then

      End If    'If BORDINI = False Then
      '------------------------------------------------------------------------------------------------------------
      If bModSupRme Then
        If BGESTORDLIST = False Then
          strTmp = ""
          If BORDINI Then
            strVistato = "." ' se è un ordine passo .
          Else
            strVistato = NTSCStr(dttET.Rows(0)!et_vistato)
          End If
          PropostaUbicazione(IIf(BORDINI, "BNORGSOR", "BNVEBOLL").ToString, NTSCStr(e.row!ec_tipork), _
            strVistato, NTSCStr(e.Row!ec_codart), NTSCInt(e.ProposedValue), NTSCInt(e.Row!ec_magaz), " ", strTmp)
          e.Row!ec_ubicaz = strTmp
        End If
      End If
      '------------------------------------------------------------------------------------------------------------
      'Legge i progressivi per vedere la disponibilità in magazzino
      ThrowRemoteEvent(New NTSEventArgs("LeggiDispo:" & e.Row!ec_codart.ToString & "§" & e.Row!ec_magaz.ToString & "§" & e.Row!ec_fase.ToString & "§" & e.Row!ec_commeca.ToString, ""))
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
      bInValidazFaseCodart = False
    End Try
  End Function

  Public Overridable Function AfterColUpdate_CORPO_xxo_codarfo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strCodart As String
    Dim strErrore As String = ""

    Try
      If bInDuplicadoc Then Return True
      'se bInValidazCodart = true vuol dire che sono nella routine BeforeColUpdateMovord_Codart e devo inserire nella griglia il codarfo (quindi l'articolo c'è già)
      If NTSCStr(e.ProposedValue).Trim <> "" Then
        If bInValidazCodart = False Then
          strCodart = CType(oCleComm, CLELBMENU).TrovaCodartDaCodarfo(e.Row!xxo_codarfo.ToString, _
                                                    NTSCInt(dttET.Rows(0)!et_conto), _
                                                    strDittaCorrente, strErrore)
          If strCodart <> "" Then e.Row!ec_codart = strCodart
          If strErrore <> "" Then
            e.Row!xxo_codarfo = ""
            ThrowRemoteEvent(New NTSEventArgs("", strErrore))
          End If
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

  Public Overridable Function AfterColUpdate_CORPO_ec_unmis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dQuant As Decimal = 0
    Dim strErrore As String = ""
    Dim dQtaIntra2 As Decimal = 0

    Try
      If bInValidazCodart Then Return True

      '-------------------------
      'calcola la quantità in UMP
      If (e.Row!ec_codart.ToString = "D" Or e.Row!ec_codart.ToString = "M") Then e.Row!ec_ump = e.Row!ec_unmis

      If lNoModifQuantSuColli = 0 OrElse (lNoModifQuantSuColli = -1 AndAlso NTSCDec(e.Row!ec_quant) = 0) Then

        If (e.Row!ec_codart.ToString = "D" Or e.Row!ec_codart.ToString = "M") Then
          If NTSCDec(e.Row!ec_quant) <> 0 And NTSCDec(e.Row!ec_colli) = 0 Then
            'se ho cambiato l'unità di misura dopoaver impsotato quant e non colli, non ricalcolo la qta dai colli, visto che 
            'le due unità di misura sono sempre identiche ed in pratica ho segnalato di non voler gestire i colli
          Else
            e.Row!ec_quant = e.Row!ec_colli
          End If
        Else
          If NTSCInt(e.Row!xxo_codtagl) = 0 And Not bCambioArticoloScarico Then
            If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, e.Row!ec_codart.ToString, e.Row!ec_unmis.ToString, _
                              NTSCDec(e.Row!ec_colli), _
                              NTSCDec(e.Row!ec_misura1), NTSCDec(e.Row!ec_misura2), _
                              NTSCDec(e.Row!ec_misura3), dQuant, False, strErrore, NTSCInt(IIf(BORDINI, oApp.NDecQta, nCO2_ArrQuant))) Then
              e.Row!ec_quant = dQuant
            Else
              e.Row!ec_quant = 0
            End If
            ThrowRemoteEvent(New NTSEventArgs("", strErrore))
          End If

        End If
      End If    'If bNoModifQuantSuColli = False Or (bNoModifQuantSuColli And NTSCDec(e.Row!ec_quant) = 0) Then

      '-------------------------
      'se prezzi per umità di misura ricalcolo il prezzo
      If Not (e.Row!ec_codart.ToString = "D" Or e.Row!ec_codart.ToString = "M") Then
        'If e.Row!ec_umprz.ToString = "S" Then
        'faccio calcolare i prezzi sempre se è abilitata la gestione, altrimenti se passo da una riga con prezzi per udm ad una senza a volte non ricalcola
        'visto che lo si sa nella beforecolupdate se prima c'era S in ec_umprz, non qui ...
        If bGestionePrezzi Then
          SettaCondCommerciali(True, False, False, e.Row, 0, 0, False)
        End If
      End If

      RiportaMisuraDaArtico(e.Row, True)

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

  Public Overridable Function AfterColUpdate_CORPO_ec_prezzo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      'Aggiorna i prezzi della lavorazione per i terzisti
      If (dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "T") And bTerzista Then
        ValorizzaProduzione(e.Row)
      End If
      If Not BGESTORDLIST Then e.Row!ec_flprzmod = "S"

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

  Public Overridable Function AfterColUpdate_CORPO_ec_colli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dQuant As Decimal = 0
    Dim strErrore As String = ""

    Try
      If bInValidazQuant = False Then
        If e.Row!ec_codart.ToString <> "" And e.Row!ec_unmis.ToString <> "" Then

          If lNoModifQuantSuColli = 0 OrElse (lNoModifQuantSuColli = -1 AndAlso NTSCDec(e.Row!ec_quant) = 0) Then

            'Controlla quale unità di misura  inserita
            '------------------------------------------
            'Influisce sulla quantità solo se l'articolo non è gestito per taglie e colori
            If NTSCStr(e.Row!ec_codart).Trim <> "" AndAlso GetMemDttArti(NTSCStr(e.Row!ec_codart)).Rows.Count > 0 Then
              If bModTCO = False Or NTSCInt(GetMemDttArti(NTSCStr(e.Row!ec_codart)).Rows(0)!ar_codtagl) = 0 Then
                If e.Row!ec_codart.ToString = "D" Or e.Row!ec_codart.ToString = "M" Then
                  e.Row!ec_quant = e.Row!ec_colli
                Else
                  If NTSCStr(e.Row!ec_tipork) <> "U" Or NTSCDec(e.Row!ec_quant) = 0 Or NTSCDec(e.Row!ec_colli) = 0 Then
                    If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, e.Row!ec_codart.ToString, e.Row!ec_unmis.ToString, _
                                    NTSCDec(e.Row!ec_colli), _
                                    NTSCDec(e.Row!ec_misura1), NTSCDec(e.Row!ec_misura2), _
                                    NTSCDec(e.Row!ec_misura3), dQuant, False, strErrore, NTSCInt(IIf(BORDINI, oApp.NDecQta, nCO2_ArrQuant))) Then
                      e.Row!ec_quant = dQuant
                    Else
                      e.Row!ec_quant = 0
                    End If
                    If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
                  End If
                End If
              End If    'If bModTCO = False Or NTSCInt(dttArti.Rows(0)!ar_codtagl) = 0 Then
            End If

          End If    'If bNoModifQuantSuColli = False Or (bNoModifQuantSuColli And NTSCDec(e.Row!ec_quant) = 0) Then

        End If    'If e.Row!ec_codart.ToString <> "" And e.Row!ec_unmis.ToString <> "" Then
      End If    'If dInValidazQuant = False Then

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

  Public Overridable Function AfterColUpdate_CORPO_ec_quant(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dQuant As Decimal = 0
    Dim strErrore As String = ""
    Dim dQtaIntra2 As Decimal = 0

    Try
      bInValidazQuant = True
      If e.Row!ec_codart.ToString <> "" And e.Row!ec_unmis.ToString <> "" Then

        '-------------------------
        If GetMemDttArti(e.Row!ec_codart.ToString).Rows.Count = 0 Or NTSCStr(e.Row!ec_codart).Trim = "" Then
          bInValidazQuant = False
          Return False
        End If

        SettaCondCommerciali(nPrperqta <> CLN__STD.bsPrSetnoqta, nScperqta <> CLN__STD.bsPrSetnoqta, False, e.Row, NTSCInt(dttArti.Rows(0)!ar_clascon), nClscan, False)

        ''Ricalcola le provvigioni
        'non dovrebbe servire: ricalcolo le provvigioni al cambio di ec_valore
        'If nPrperqta <> CLN__STD.bsPrSetnoqta Or nScperqta <> CLN__STD.bsPrSetnoqta Or  e.Row!ec_ricimp.ToString = "S" Then
        '  SettaProvvigioni(e.Row)
        'End If

        '-------------------------
        'Cambio del flag flevas
        If BORDINI And BGESTORDLIST = False Then
          If Val(e.Row!ec_oanum) > 0 Then
            If NTSCDec(e.Row!ec_quant) < NTSCDec(e.Row!ec_oaqtadis) Then e.Row!ec_oasalcon = "C"
          End If
        End If

        '-------------------------
        'per veboll
        If BORDINI = False Then
          'Cambio del flag flevas
          If Val(e.Row!ec_ornum) > 0 Then
            If NTSCDec(e.ProposedValue) < NTSCDec(e.Row!ec_qtadisimp) And NTSCStr(e.Row!ec_salcon) = "S" Then
              e.Row!ec_salcon = "C"
              bCambioFlevas = True
            End If
          End If
          If Val(e.Row!ec_npnum) > 0 Then
            If NTSCDec(e.ProposedValue) < NTSCDec(e.Row!ec_npqtadis) Then e.Row!ec_npsalcon = "C"
          End If
          'Sempre che ar_pesonet sia corretto
          e.Row!ec_massakg = NTSCDec(e.ProposedValue) * NTSCDec(dttArti.Rows(0)!ar_pesonet)
          If e.Row!ec_codart.ToString <> "D" And e.Row!ec_codart.ToString <> "M" Then
            CType(oCleComm, CLELBMENU).ConvQuantUMIntra2(strDittaCorrente, e.Row!ec_codart.ToString, e.Row!ec_ump.ToString, _
                                                         NTSCDec(e.Row!ec_quant), NTSCDec(e.Row!ec_misura1), NTSCDec(e.Row!ec_misura2), _
                                                         NTSCDec(e.Row!ec_misura3), dQtaIntra2)
            e.Row!ec_massaum2 = dQtaIntra2
          Else
            e.Row!ec_massaum2 = e.Row!ec_quant
          End If
        End If

        '-------------------------
        'Se abilitata opzione determina all'indietro i COLLI
        If bModifColliSuQuant Then
          If e.Row!ec_ump.ToString <> e.Row!ec_unmis.ToString And e.Row!ec_unmis.ToString <> "" Then
            If CType(oCleComm, CLELBMENU).ConvQuantUM(strDittaCorrente, e.Row!ec_codart.ToString, e.Row!ec_ump.ToString, _
                                    NTSCDec(e.Row!ec_quant), NTSCDec(e.Row!ec_misura1), NTSCDec(e.Row!ec_misura2), _
                                    NTSCDec(e.Row!ec_misura3), e.Row!ec_unmis.ToString, dQuant, strErrore, NTSCInt(IIf(BORDINI, oApp.NDecQta, nCO2_ArrQuant))) Then
              e.Row!ec_colli = dQuant
            End If
            If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
          End If
        End If

        '-------------------------
        'Su opzione propone già la qta da assegnare (senza flag di evasione)
        If bProponiQtaDaAss And BGESTORDLIST = False And BORDINI Then
          If (dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#") And _
            e.Row!ec_flevas.ToString <> "S" And e.Row!ec_flevapre.ToString <> "S" Then
            If ArrDbl(NTSCDec(e.Row!ec_quant) - NTSCDec(e.Row!ec_quaeva) - NTSCDec(e.Row!ec_quapre), oApp.NDecQta) > 0 Then
              e.Row!xxo_qtadaass = ArrDbl(NTSCDec(e.Row!ec_quant) - NTSCDec(e.Row!ec_quaeva) - NTSCDec(e.Row!ec_quapre), oApp.NDecQta)
            End If
          End If
        End If

      End If    'If e.Row!ec_codart.ToString <> "" And e.Row!ec_unmis.ToString <> "" Then

      If e.Row!ec_codart.ToString <> "" And e.Row!ec_unmis.ToString <> "" Then
        If bModPM Then SistemaPMSalconDaQta(e.Row)
      End If    'If e.Row!ec_codart.ToString <> "" And e.Row!ec_unmis.ToString <> "" Then

      bInValidazQuant = False
      Return True
    Catch ex As Exception
      bInValidazQuant = False
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AfterColUpdate_CORPO_ec_misura1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dQuant As Decimal = 0
    Dim strErrore As String = ""
    Dim bDaMisure As Boolean = False

    Try
      If (e.Row!ec_codart.ToString <> "D" And e.Row!ec_codart.ToString <> "M") Then
        If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, e.Row!ec_codart.ToString, e.Row!ec_unmis.ToString, _
                        NTSCDec(e.Row!ec_colli), _
                        NTSCDec(e.Row!ec_misura1), NTSCDec(e.Row!ec_misura2), _
                        NTSCDec(e.Row!ec_misura3), dQuant, bDaMisure, strErrore, NTSCInt(IIf(BORDINI, oApp.NDecQta, nCO2_ArrQuant))) Then
          If bDaMisure Then e.Row!ec_quant = dQuant
        Else
          e.Row!ec_quant = 0
        End If
        If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
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

  Public Overridable Function AfterColUpdate_CORPO_ec_misura2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dQuant As Decimal = 0
    Dim strErrore As String = ""
    Dim bDaMisure As Boolean = False

    Try
      If (e.Row!ec_codart.ToString <> "D" And e.Row!ec_codart.ToString <> "M") Then
        If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, e.Row!ec_codart.ToString, e.Row!ec_unmis.ToString, _
                        NTSCDec(e.Row!ec_colli), _
                        NTSCDec(e.Row!ec_misura1), NTSCDec(e.Row!ec_misura2), _
                        NTSCDec(e.Row!ec_misura3), dQuant, bDaMisure, strErrore, NTSCInt(IIf(BORDINI, oApp.NDecQta, nCO2_ArrQuant))) Then
          If bDaMisure Then e.Row!ec_quant = dQuant
        Else
          e.Row!ec_quant = 0
        End If
        If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
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

  Public Overridable Function AfterColUpdate_CORPO_ec_misura3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dQuant As Decimal = 0
    Dim strErrore As String = ""
    Dim bDaMisure As Boolean = False

    Try
      If (e.Row!ec_codart.ToString <> "D" And e.Row!ec_codart.ToString <> "M") Then
        If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, e.Row!ec_codart.ToString, e.Row!ec_unmis.ToString, _
                        NTSCDec(e.Row!ec_colli), _
                        NTSCDec(e.Row!ec_misura1), NTSCDec(e.Row!ec_misura2), _
                        NTSCDec(e.Row!ec_misura3), dQuant, bDaMisure, strErrore, NTSCInt(IIf(BORDINI, oApp.NDecQta, nCO2_ArrQuant))) Then
          If bDaMisure Then e.Row!ec_quant = dQuant
        Else
          e.Row!ec_quant = 0
        End If
        If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
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

  Public Overridable Function AfterColUpdate_CORPO_ec_magaz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strVistato As String = ""
    Dim strTmp As String = ""
    Try
      If bModSupRme Then
        If NTSCStr(e.Row!ec_codart).Trim = "" Then Return True
        If bNoModificaUbicCambioMaga Then
          If TrascodificaUbicazione(NTSCInt(e.Row!ec_magaz), NTSCStr(e.Row!ec_ubicaz), True, True, True) Then Return True
        End If


        If BORDINI Then
          strVistato = "." ' se è un ordine passo .
        Else
          strVistato = NTSCStr(dttET.Rows(0)!et_vistato)
        End If

        PropostaUbicazione(IIf(BORDINI, "BNORGSOR", "BNVEBOLL").ToString, NTSCStr(e.Row!ec_tipork), _
                           strVistato, NTSCStr(e.Row!ec_codart), NTSCInt(e.Row!ec_fase), NTSCInt(e.ProposedValue), " ", strTmp)
        If strTmp <> "" Then
          Try
            e.Row!ec_ubicaz = strTmp
          Catch ex As Exception
          End Try
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

  Public Overridable Function AfterColUpdate_CORPO_ec_preziva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dTemp As Decimal
    Dim dTemp2 As Decimal

    Try
      If dttET.Rows(0)!et_scorpo.ToString = "S" Then
        oCldDocu.Scorporo(NTSCDec(e.Row!ec_preziva), NTSCInt(e.Row!ec_codiva), dTemp2, dTemp, oCldDocu.TrovaNdecSuPrzUn(0))
        e.Row!ec_prezzo = dTemp2
      End If

      If Not BGESTORDLIST Then e.Row!ec_flprzmod = "S"

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

  Public Overridable Function AfterColUpdate_CORPO_ec_prezvalc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dPrzOld As Decimal = 0
    Try
      dPrzOld = NTSCDec(e.Row!ec_prezzo)
      If NTSCInt(dttET.Rows(0)!et_valuta) > 0 Then
        e.Row!ec_prezzo = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(e.Row!ec_prezvalc), NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
        If NTSCDec(e.Row!ec_prezzo) = dPrzOld Then
          'forzo la Settavaloririga' per ricalcolare ec_valorev, visto che verrebbe scatenata solo al 
          'cambio del prezzo ma se la modifica al prezzo in valuta è minima il prezzo non cambia mentre il ec_valorev si
          SettaValoriRiga(e.Row)
        End If
      End If

      If Not BGESTORDLIST Then e.Row!ec_flprzmod = "S"

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

  Public Overridable Function AfterColUpdate_CORPO_ec_valore(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dPrezzo As Decimal = 0

    Try
      '-------------------------
      'ricalcolo le provvigioni
      SettaCondCommerciali(False, False, True, e.Row, 0, 0, False)
      If bModPM And BGESTORDLIST = False Then
        bInvalidazEcValore = True
        SistemaPMSalconDaQta(e.Row)
        SistemaCampiRIMDISDaCampiIMPQTA(e.Row)
        SistemaCampiPMdaCampiPMDA(e.Row)
      End If
      '----------------------------------------------------------------------------------------------------------
      '--- Calcola % Ricarico, % Margine e Margine Valore
      '----------------------------------------------------------------------------------------------------------
      If bCaricamentoDocumentoOrdine = True Then Return True
      If e.Row.Table.Columns.Contains("xxo_percricarico") = False Then Return True
      If e.Row.Table.Columns.Contains("xxo_percmargine") = False Then Return True
      If e.Row.Table.Columns.Contains("xxo_marginevalore") = False Then Return True

      CalcolaMargineRicarico(e.Row, NTSCDate(dttET.Rows(0)!et_datdoc))
      '----------------------------------------------------------------------------------------------------------
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
      bInvalidazEcValore = False
    End Try
  End Function

  Public Overridable Function AfterColUpdate_CORPO_ec_codiva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dTemp As Decimal
    Dim dTemp2 As Decimal

    Try
      If dttET.Rows(0)!et_scorpo.ToString = "S" Then
        oCldDocu.Scorporo(NTSCDec(e.Row!ec_preziva), NTSCInt(e.Row!ec_codiva), dTemp2, dTemp, oCldDocu.TrovaNdecSuPrzUn(0))
        e.Row!ec_prezzo = dTemp2
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

  Public Overridable Function AfterColUpdate_CORPO_ec_stasino(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      Select Case e.Row!ec_stasino.ToString
        Case "O", "M", "P" ', "X"
          'Azzera il modo automatico sconti (derogabile su opzione di registro) e provvigioni
          If Not bConsentiScontiSuOmaggi Then
            For i As Integer = 1 To 6
              e.Row("ec_scont" & i) = 0
              If Not BGESTORDLIST Then e.Row("ec_flsc" & i & "mod") = "N"
            Next
            e.Row!ec_scontp = 0
            e.Row!ec_scontv = 0
          End If
          If BGESTORDLIST = False Then
            e.Row!ec_provv = 0
            e.Row!ec_provv2 = 0
          End If
      End Select

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

  Public Overridable Function AfterColUpdate_CORPO_ec_commeca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strThrowEvent As String = ""            'contiene tutti gli eventi che devono essere eseguiti dall'ui
    Dim dttTmp As New DataTable
    Try
      If bInOnAddNewRow Then Return True
      If bInOnAddNewRowImp Then Return True
      If bInSalvaOrdine Then Return True

      '---------------------------------------------
      'pm : cambio commessa su task esistente
      If NTSCInt(e.Row!ec_pmtaskid) <> 0 Then
        e.Row!ec_pmtaskid = 0
        If BGESTORDLIST = False Then
          e.Row!ec_pmqtadis = 0
          e.Row!ec_pmvaldis = 0
          e.Row!ec_pmsalcon = "C"
          e.Row!xxo_pmtaskid = ""
          e.Row!xxo_pmqtadisda = 0
          e.Row!xxo_pmvaldisda = 0
          e.Row!xxo_pmqtares = 0
          e.Row!xxo_pmvalres = 0
          e.Row!xxo_pmqtarim = 0
          e.Row!xxo_pmvalrim = 0
        End If
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222097812500, "Attenzione! cambiando il numero di commessa vengono eliminati riferimenti al task-id della commessa !! ")))
      End If

      '---------------------------------------------
      'Al cambio della commessa, se era già stata indicata una sottocommessa e questa
      'non esiste, azzera il campo
      If bDisabilitaControlliSubcomm = False Then
        If NTSCInt(e.ProposedValue) <> 0 Then
          If NTSCStr(e.Row!ec_subcommeca).Trim <> "" Then
            oCldDocu.ValCodiceDb(NTSCStr(e.Row!ec_subcommeca), strDittaCorrente, "SUBCOMM", "S", "", dttTmp, e.ProposedValue.ToString)
            If dttTmp.Rows.Count = 0 Then
              e.Row!ec_subcommeca = " "
            End If
            dttTmp.Clear()
          End If
        Else
          If NTSCStr(e.Row!ec_subcommeca) <> " " Then e.Row!ec_subcommeca = " "
        End If
      End If

      '----------------------------------
      'Legge i progressivi per vedere la disponibilità in magazzino
      strThrowEvent = strThrowEvent & "LeggiDispo:" & e.Row!ec_codart.ToString & "§" & e.Row!ec_magaz.ToString & "§" & e.Row!ec_fase.ToString & "§" & e.Row!ec_commeca.ToString & "|"

      '-----------------------------------------------
      'avviso l'UI delle operazioni grafiche che devono essere eseguite a causa della validazione della commessa
      If strThrowEvent <> "" Then ThrowRemoteEvent(New NTSEventArgs(strThrowEvent.Substring(0, strThrowEvent.Length - 1), ""))

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

  Public Overridable Function AfterColUpdate_CORPO_ec_flprznet(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      '---------------------------
      'se = 'S' pulisco gli sconti
      'ricalcolo provvigioni e valori
      If e.ProposedValue.ToString = "S" And NTSCStr(e.Row!ec_codart).Trim <> "" Then
        If GetMemDttArti(e.Row!ec_codart.ToString).Rows.Count = 0 Then Return False

        'Pulisce gli sconti
        SettaCondCommerciali(False, True, False, e.Row, NTSCInt(dttArti.Rows(0)!ar_clascon), nClscan, False)
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

  Public Overridable Function AfterColUpdate_CORPO_ec_provv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not BGESTORDLIST Then e.Row!ec_flprov1mod = "S"

      Return CalcolaValoreProvvRiga(e.Row)
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
  Public Overridable Function AfterColUpdate_CORPO_ec_vprovv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not BGESTORDLIST Then e.Row!ec_flprov1mod = "S"
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

  Public Overridable Function AfterColUpdate_CORPO_ec_provv2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not BGESTORDLIST Then e.Row!ec_flprov2mod = "S"

      Return CalcolaValoreProvvRiga(e.Row)
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
  Public Overridable Function AfterColUpdate_CORPO_ec_vprovv2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not BGESTORDLIST Then e.Row!ec_flprov2mod = "S"
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

  Public Overridable Function AfterColUpdate_CORPO_ec_orriga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Dim strTmp As String
    Dim lTmp As Integer
    Try
      If Not BORDINI And bModSupRme Then
        If oCldDocu.TrovaDatiOrdine(strDittaCorrente, NTSCStr(e.Row!ec_ortipo), NTSCInt(e.Row!ec_oranno), NTSCStr(e.Row!ec_orserie), _
                                    NTSCInt(e.Row!ec_ornum), NTSCInt(e.Row!ec_orriga), dttTmp) Then

          ' se non ho un ordine collegato metto dei valori di default
          If dttTmp.Rows.Count = 0 Then
            dttTmp.Rows.Add()
            dttTmp.Rows(0)!mo_ubicaz = ""
            dttTmp.Rows(0)!mo_lotto = 0
          End If

          strTmp = ""
          PropostaUbicazione("BNVEBOLL", NTSCStr(e.row!ec_tipork), NTSCStr(dttET.Rows(0)!et_vistato), NTSCStr(e.Row!ec_codart), _
                             NTSCInt(e.Row!ec_fase), NTSCInt(e.Row!ec_magaz), NTSCStr(dttTmp.Rows(0)!mo_ubicaz), strTmp)
          e.Row!ec_ubicaz = strTmp

          If Not ScaricaLottox_CheckLogistica(e.Row) Then
            lTmp = 0
            PropostaLotto("BNVEBOLL", NTSCStr(e.row!ec_tipork), NTSCStr(dttET.Rows(0)!et_vistato), NTSCStr(e.Row!ec_codart), _
                          NTSCInt(e.Row!ec_fase), NTSCInt(e.Row!ec_magaz), NTSCInt(dttTmp.Rows(0)!mo_lotto), lTmp, strTmp)
            If lTmp <> 0 Then e.Row!ec_lotto = lTmp 'lotto dall'ordine
            If strTmp <> "" Then e.Row!xxo_lottox = strTmp 'lotto da tabmaga (lotto generico)
          End If

          ' per il momento la proposta matricola non è gestita
          'strTmp = ""
          'PropostaMatricola("BNVEBOLL", NTSCStr(dttET.Rows(0)!et_tipork), NTSCStr(dttET.Rows(0)!et_vistato), NTSCStr(e.ProposedValue), _
          '                  NTSCInt(e.Row!ec_fase), NTSCInt(e.Row!ec_magaz), NTSCStr(e.Row!xxo_matric), strTmp)
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

  Public Overridable Function AfterColUpdate_CORPO_ec_npriga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Dim strTmp As String
    Dim lTmp As Integer
    Try
      If Not BORDINI And bModSupRme Then
        If oCldDocu.TrovaDatiNotaDiPrelievo(strDittaCorrente, NTSCStr(e.Row!ec_nptipo), NTSCInt(e.Row!ec_npanno), NTSCStr(e.Row!ec_npserie), _
                                    NTSCInt(e.Row!ec_npnum), NTSCInt(e.Row!ec_npriga), dttTmp) Then

          ' se non ho un ordine collegato metto dei valori di default
          If dttTmp.Rows.Count = 0 Then
            dttTmp.Rows.Add()
            dttTmp.Rows(0)!mm_ubicaz = ""
            dttTmp.Rows(0)!mm_lotto = 0
          End If

          strTmp = ""
          PropostaUbicazione("BNVEBOLL", NTSCStr(e.row!ec_tipork), NTSCStr(dttET.Rows(0)!et_vistato), NTSCStr(e.Row!ec_codart), _
                             NTSCInt(e.Row!ec_fase), NTSCInt(e.Row!ec_magaz), NTSCStr(dttTmp.Rows(0)!mm_ubicaz), strTmp)
          e.Row!ec_ubicaz = strTmp

          If Not ScaricaLottox_CheckLogistica(e.Row) Then
            lTmp = 0
            PropostaLotto("BNVEBOLL", NTSCStr(e.row!ec_tipork), NTSCStr(dttET.Rows(0)!et_vistato), NTSCStr(e.Row!ec_codart), _
                          NTSCInt(e.Row!ec_fase), NTSCInt(e.Row!ec_magaz), NTSCInt(dttTmp.Rows(0)!mm_lotto), lTmp, strTmp)
            If lTmp <> 0 Then e.Row!ec_lotto = lTmp 'lotto dall'ordine
            If strTmp <> "" Then e.Row!xxo_lottox = strTmp 'lotto da tabmaga (lotto generico)
          End If

          ' per il momento la proposta matricola non è gestita
          'strTmp = ""
          'PropostaMatricola("BNVEBOLL", NTSCStr(dttET.Rows(0)!et_tipork), NTSCStr(dttET.Rows(0)!et_vistato), NTSCStr(e.ProposedValue), _
          '                  NTSCInt(e.Row!ec_fase), NTSCInt(e.Row!ec_magaz), NTSCStr(e.Row!xxo_matric), strTmp)
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

  Public Overridable Function AfterColUpdate_CORPO_ec_codtpro(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.Row!ec_codart.ToString.Trim = "" Then Return False
      GetMemDttArti(e.Row!ec_codart.ToString)

      'serve solo per le provviogni con bsveboll/opzioni/opzprc=AgentePromozioneAAAAPPP
      SettaCondCommerciali(True, True, True, e.Row, NTSCInt(dttArti.Rows(0)!ar_clascon), nClscan, True)

      SistemaPMSalconDaQta(e.Row)

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

  Public Overridable Function AfterColUpdate_CORPO_ec_pmtaskid(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If bModPM And NTSCInt(e.ProposedValue) <> 0 Then
        If BGESTORDLIST = False Then
          SistemaPMSalcon(e.Row, False, False)
          SistemaCampiRIMDISDaCampiIMPQTA(e.Row, False, False, False)
          SistemaCampiPMdaCampiPMDA(e.Row)
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

  Public Overridable Function AfterColUpdate_CORPO_xxo_pmqtarim(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If bInSistemaCampiRIMDISDaCampiIMPQTAPM Then Return True

      ' ripropone il valore rimanete in base al residuo...
      If NTSCDec(e.Row!xxo_pmqtares) <> 0 And NTSCDec(e.Row!xxo_pmqtarim) <> 0 Then
        e.Row!xxo_pmvalrim = ArrDbl(NTSCDec(e.Row!xxo_pmvalres) / NTSCDec(e.Row!xxo_pmqtares) * NTSCDec(e.Row!xxo_pmqtarim), oCldDocu.TrovaNdec(0))
      End If

      SistemaPMSalcon(e.Row, True, False)
      SistemaCampiRIMDISDaCampiIMPQTA(e.Row, True, False, False)
      SistemaCampiPMdaCampiPMDA(e.Row)

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

  Public Overridable Function AfterColUpdate_CORPO_xxo_pmvalrim(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If bInSistemaCampiRIMDISDaCampiIMPQTAPM Then Return True
      SistemaPMSalcon(e.Row, True, False)
      SistemaCampiRIMDISDaCampiIMPQTA(e.Row, False, False, True)
      SistemaCampiPMdaCampiPMDA(e.Row)

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

  Public Overridable Function AfterColUpdate_CORPO_ec_pmsalcon(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ' ora accetta solo certe combianzioni con prevgrup:
      If NTSCStr(e.ProposedValue) = "S" Then
        e.Row!xxo_pmvalrim = 0
        e.Row!xxo_pmqtarim = 0
      Else
        If NTSCStr(e.ProposedValue) = "C" Then
          ' non fa niente ...
        Else
          ' rifiuta dati incongruenti
          If (NTSCStr(e.ProposedValue) = "Q" Or NTSCStr(e.ProposedValue) = "V") And NTSCStr(e.Row!xxo_prevgrup) = "N" Then
            e.ProposedValue = "C"
          Else
            If NTSCStr(e.ProposedValue) = "Q" Then
              e.Row!xxo_pmqtarim = 0
            Else
              If NTSCStr(e.ProposedValue) = "V" Then
                e.Row!xxo_pmvalrim = 0
              End If
            End If
          End If
        End If
      End If
      SistemaCampiRIMDISDaCampiIMPQTA(e.Row, False, True, False)
      SistemaCampiPMdaCampiPMDA(e.Row)
vi55:

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

  Public Overridable Sub AfterColUpdate_CORPO_ec_lotto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'sono stato chiamato da bnmgalo e mi è stato passato il lotto numerico: compilo la colonna con il lotto alfanumerico
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue.ToString) = 0 Then
        e.Row!xxo_lottox = ""
        Return
      End If

      oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANALOTTI", "S", "", dttTmp, NTSCStr(e.Row!ec_codart))
      If dttTmp.Rows.Count = 0 Then
        'non posso creare l'anagrafica lotti, visto che non saprei che alo_lottox memorizzare !!!!
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130420445754704544, "Il codice lotto '|" & NTSCStr(e.ProposedValue) & "|' è inesistente.")))
        e.Row!ec_lotto = 0
        Return
      Else
        If nGiorniMargineScadenzaLotto <> 9999 AndAlso Not BORDINI AndAlso bDocEmesso AndAlso Not bInImportRigheOrd AndAlso Not bInImportRigheNota Then
          If NTSCDate(dttET.Rows(0)!et_datdoc).AddDays(nGiorniMargineScadenzaLotto) >= NTSCDate(dttTmp.Rows(0)!alo_dtscad) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131087333539758392, "Impossibile usare il lotto '|" & NTSCStr(dttTmp.Rows(0)!alo_lottox) & "|'." & vbCrLf & _
                                              "La data scadenza |" & NTSCDate(dttTmp.Rows(0)!alo_dtscad).ToShortDateString & "| risulta essere inferiore o uguale alla data documento più |" & nGiorniMargineScadenzaLotto & "| giorni (|" & NTSCDate(dttET.Rows(0)!et_datdoc).AddDays(nGiorniMargineScadenzaLotto) & "|)")))
            e.Row!ec_lotto = 0
            Return
          End If
        End If
      End If

      If NTSCStr(e.Row!xxo_lottox) <> NTSCStr(dttTmp.Rows(0)!alo_lottox) Then e.Row!xxo_lottox = NTSCStr(dttTmp.Rows(0)!alo_lottox)

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
  Public Overridable Sub AfterColUpdate_CORPO_xxo_lottox(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Dim lIdLotto As Integer = 0
    Dim strErr As String = ""
    Dim strStatoProv As String = ""
    Try

      'dato il lotto alfanumerico devo tradure in ID lotto numerico
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!ec_lotto = 0
        Return
      End If

      'dato il lotto alfanumerico ottengo l'ID numerico
      oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANALOTTIX", "S", "", dttTmp, NTSCStr(e.Row!ec_codart))
      If dttTmp.Rows.Count = 0 Then
        dttTmp.Clear()
        'creo l'anagrafica lotti
        oCldDocu.ValCodiceDb(NTSCInt(dttET.Rows(0)!et_conto).ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then strStatoProv = NTSCStr(dttTmp.Rows(0)!an_stato).Trim
        dttTmp.Clear()

        If Not CType(oCleComm, CLELBMENU).CreaAnalottiDaLottox(strDittaCorrente, NTSCStr(e.Row!ec_codart), _
                                          NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                          NTSCStr(e.ProposedValue), strStatoProv, lIdLotto, strErr) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130420446283145426, "Codice lotto inesistente") & vbCrLf & strErr))
          e.Row!xxo_lottox = ""
          Return
        Else
          If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))

          If VisMsg() And oApp.Batch = False And bApriAnalottiDopoNew Then
            ThrowRemoteEvent(New NTSEventArgs("APRIANALO:", NTSCStr(e.Row!ec_codart) & "§" & NTSCStr(lIdLotto)))
          End If
        End If
      Else
        lIdLotto = NTSCInt(dttTmp.Rows(0)!alo_lotto)
      End If

      If NTSCInt(e.Row!ec_lotto) <> lIdLotto Then e.Row!ec_lotto = lIdLotto

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

  Public Overridable Function AfterColUpdate_CORPO_ec_confermato(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dtrT() As DataRow
    Try
      If NTSCStr(e.Row!ec_tipork) = "H" Then
        If dttECIMP.Rows.Count > 0 Then
          Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 129796597142319047, "Modificare il campo 'Confermato' su tutte le righe degli impegni collegati?"))
          ThrowRemoteEvent(msg)
          If msg.RetValue = ThMsg.RETVALUE_YES Then
            dtrT = dttECIMP.Select("ec_rigaor = " & NTSCInt(e.Row!ec_riga))
            For l As Integer = 0 To dtrT.Length - 1
              'aggiorna impegni collegati e lavorazioni
              dtrT(l)!ec_confermato = e.ProposedValue
            Next
          End If
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

  Public Overridable Function AfterColUpdate_CORPO_ec_scont1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not BGESTORDLIST Then e.Row!ec_flsc1mod = "S"
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
  Public Overridable Function AfterColUpdate_CORPO_ec_scont2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not BGESTORDLIST Then e.Row!ec_flsc2mod = "S"
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
  Public Overridable Function AfterColUpdate_CORPO_ec_scont3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not BGESTORDLIST Then e.Row!ec_flsc3mod = "S"
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
  Public Overridable Function AfterColUpdate_CORPO_ec_scont4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not BGESTORDLIST Then e.Row!ec_flsc4mod = "S"
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
  Public Overridable Function AfterColUpdate_CORPO_ec_scont5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not BGESTORDLIST Then e.Row!ec_flsc5mod = "S"
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
  Public Overridable Function AfterColUpdate_CORPO_ec_scont6(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not BGESTORDLIST Then e.Row!ec_flsc6mod = "S"
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

  Public Overridable Function AfterColUpdate_CORPO_ec_flprzmod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ColoraCelle(e.Row)
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
  Public Overridable Function AfterColUpdate_CORPO_ec_flsc1mod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ColoraCelle(e.Row)
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
  Public Overridable Function AfterColUpdate_CORPO_ec_flsc2mod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ColoraCelle(e.Row)
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
  Public Overridable Function AfterColUpdate_CORPO_ec_flsc3mod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ColoraCelle(e.Row)
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
  Public Overridable Function AfterColUpdate_CORPO_ec_flsc4mod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ColoraCelle(e.Row)
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
  Public Overridable Function AfterColUpdate_CORPO_ec_flsc5mod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ColoraCelle(e.Row)
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
  Public Overridable Function AfterColUpdate_CORPO_ec_flsc6mod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ColoraCelle(e.Row)
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
  Public Overridable Function AfterColUpdate_CORPO_ec_flprov1mod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ColoraCelle(e.Row)
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
  Public Overridable Function AfterColUpdate_CORPO_ec_flprov2mod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ColoraCelle(e.Row)
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

  Public Overridable Function AfterColUpdate_CORPO_xxo_listpromo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      'If NTSCInt(e.ProposedValue) <> 0 Then
      bInValidazCodart = True 'Per forzare il ricalcolo del prezzo
      SettaCondCommerciali(True, False, False, e.Row, 0, 0, False)
      bInValidazCodart = False
      'End If

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

  '----------------------

  Public Overridable Sub CorpoOnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrT() As DataRow = Nothing
    Dim strErr As String = ""
    Try
      bInOnAddNewRow = True
      bCancellaScarichi = False
      lDiffDatcons = 0
      dRapportoQta = 0
      bCambioFlevas = False
      bCambioQuantita = False
      bCambioSconti = False
      bCambioCommessa = False
      If bInImportRigheNota = False And bInImportRigheOrd = False And bInImportRigheOff Then
        'diversamente se dopo aver importato righe di ordini/note cambio la quantità il prezzo viene sempre ricalcolato
        nPrperqta = bsPrNoset
        nScperqta = bsPrNoset
      End If
      If BGESTORDLIST Then
        lCrighe = oCldDocu.LegNuma(strDittaCorrente, "OL", " ", 0, True)
        lCrighe = oCldDocu.AggNuma(strDittaCorrente, "OL", " ", 0, lCrighe, True, True, strErr)
        If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
      Else
        e.Row!ec_ultagg = DateTime.Now
        lCrighe = lCrighe + nIncremContatoreRiga
      End If
      e.Row!ec_riga = lCrighe
      bNonValidare = True
      e.Row!codditt = strDittaCorrente
      e.Row!ec_tipork = dttET.Rows(0)!et_tipork
      e.Row!ec_anno = dttET.Rows(0)!et_anno
      e.Row!ec_serie = dttET.Rows(0)!et_serie
      e.Row!ec_numdoc = dttET.Rows(0)!et_numdoc
      e.Row!ec_codiva = dttET.Rows(0)!et_codese
      If Not BGESTORDLIST Then e.Row!xxo_codiva = dttET.Rows(0)!xx_codese
      bNonValidare = False
      If BORDINI = False Then
        'per veboll
        e.Row!ec_causale = dttET.Rows(0)!et_causale
        e.Row!ec_causale2 = e.Row!xxo_causec     'xxo_causec viene impostato nella riga 'e.Row!ec_causale = dttET.Rows(0)!et_causale'
      End If
      If BGESTORDLIST = False Then
        e.Row!ec_coddivi = dttET.Rows(0)!et_coddivi
        e.Row!xxo_coddivi = dttET.Rows(0)!xx_coddivi
      End If
      e.Row!ec_magaz = dttET.Rows(0)!et_magaz
      e.Row!ec_magaz2 = dttET.Rows(0)!et_magaz2
      If BORDINI Then
        e.Row!ec_datcons = dttET.Rows(0)!et_datcons
        If BGESTORDLIST = False Then e.Row!ec_datconsor = dttET.Rows(0)!et_datcons
      End If
      e.Row!ec_controp = dttET.Rows(0)!et_controp
      e.Row!ec_codcena = dttET.Rows(0)!et_codcena
      If BGESTORDLIST = False Then e.Row!xxo_codcena = dttET.Rows(0)!xx_codcena
      e.Row!ec_commeca = dttET.Rows(0)!et_commeca
      If BGESTORDLIST = False Then e.Row!xxo_commeca = dttET.Rows(0)!xx_commeca
      e.Row!ec_subcommeca = dttET.Rows(0)!et_subcommeca
      'e.Row!ec_contocontr = non serve: viene compilato in automatico nella BeforeColUpdate_CORPO_ec_controp
      If nDatiniCompMese = -1 Then
        e.Row!ec_datini = NTSCDate(dttET.Rows(0)!et_datdoc).AddDays((NTSCDate(dttET.Rows(0)!et_datdoc).Day - 1) * -1) 'propongo il primo del mese 
        e.Row!ec_datfin = NTSCDate(dttET.Rows(0)!et_datdoc).AddDays((NTSCDate(dttET.Rows(0)!et_datdoc).Day - 1) * -1) 'propongo il primo del mese 
      Else
        e.Row!ec_datini = NTSCDate(dttET.Rows(0)!et_datdoc)
        e.Row!ec_datfin = NTSCDate(dttET.Rows(0)!et_datdoc)
      End If

      If bModPM And BORDINI Then
        If dttET.Rows(0)!et_tipork.ToString = "O" Or dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "X" Then
          e.Row!xxo_darave = "D"
        End If
        If dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#" Then
          e.Row!xxo_darave = "A"
        End If
        If Not (dttET.Rows(0)!et_tipork.ToString = "O" Or dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#" Or dttET.Rows(0)!et_tipork.ToString = "X") Then
          e.Row!xxo_darave = " "
        End If
      Else
        e.Row!xxo_darave = " "
      End If

      bNonValidare = True
      If BORDINI = False Then
        e.Row!ec_coddivi = dttET.Rows(0)!et_coddivi
        e.Row!xxo_coddivi = dttET.Rows(0)!xx_coddivi
      End If
      bNonValidare = False

    Catch ex As Exception
      bNonValidare = False
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    Finally
      bInOnAddNewRow = False
      bNonValidare = False
    End Try
  End Sub

  Public Overridable Function CorpoBeforeInsert(ByVal nRow As Integer) As Boolean
    Try

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
  Public Overridable Function CorpoAfterInsert(ByVal nRow As Integer) As Boolean
    Dim dtrEC As DataRow
    Dim dtDataVal As DateTime = Nothing

    Try
      If bSaltaAfterInsert Then Return True 'settato da fuori, tipo da BEORGNOR

      dtrEC = dttEC.Rows(nRow)
      '--------------------------------
      'Legge i figli di distinta base
      If dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "T" And bInImportRigheOrd = False Then
        If BORDINI Then
          dtDataVal = NTSCDate(dtrEC!ec_datcons)
        Else
          dtDataVal = NTSCDate(dttET.Rows(0)!et_datdoc)
        End If
        If Not (NTSCStr(dtrEC!ec_codart).ToUpper = "D" And Not bEsplodiD) Then
          ScriviRigheDaDiba(dtrEC, NTSCStr(dtrEC!ec_codart), NTSCInt(dtrEC!ec_fase), NTSCDec(dtrEC!ec_quant), _
                             NTSCInt(dttET.Rows(0)!et_conto), NTSCInt(dtrEC!ec_commeca), NTSCStr(dtrEC!ec_subcommeca), _
                             NTSCInt(dtrEC!ec_codcena), dtDataVal, NTSCStr(dtrEC!ec_codcfam), NTSCInt(dtrEC!ec_perqta), _
                             NTSCStr(dtrEC!ec_umprz), NTSCStr(dtrEC!ec_unmis), NTSCStr(dtrEC!ec_ump), NTSCDec(dtrEC!ec_colli), False)
        End If
        ValorizzaProduzione(dtrEC)
      End If

      '--------------------------------
      'Carica i figli del KIT (solo se le righe non sono caricate da funzioni come 'scrivirigheoff', 'scrivirigheOFA'
      If BGESTORDLIST = False And bInImportRigheOff = False And bInImportRigheOrd = False And bInDuplicadoc = False And bInImportRigheNota = False Then
        If (dtrEC!ec_flkit.ToString = "A" Or dtrEC!ec_flkit.ToString = "S") And dttET.Rows(0)!et_tipork.ToString <> "H" And dttET.Rows(0)!et_tipork.ToString <> "T" Then
          ScriviRigheKit(dtrEC)
          'mi sposto sull'ultima riga della griglia di veboll/gsor 
          '(senza questa riga, con articoli kit con molti figli se l'ultima riga figlia eccedeva l'altezza del corpo
          'del documento bisognava muovere con il muse la scroolbar per vedere la nuova riga dove poter inserire l'articolo)
          ThrowRemoteEvent(New NTSEventArgs("GRINEWROW", "grrighe"))
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

  Public Overridable Function CorpoBeforeUpdate(ByVal nRow As Integer) As Boolean
    Dim dtrEC As DataRow
    Dim dtrT() As DataRow = Nothing
    Try
      dtrEC = dttEC.Rows(nRow)

      If BORDINI And Not BGESTORDLIST Then
        If NTSCStr(dtrEC("ec_flevas", DataRowVersion.Original)) <> NTSCStr(dtrEC!ec_flevas) Then
          If NTSCStr(dtrEC!ec_flevas) = "S" Then
            dtrEC!ec_valore = 0
          Else
            dtrEC!ec_valore = CalcolaValoreRiga(dtrEC, NTSCDec(dtrEC!ec_prezzo), NTSCDec(dtrEC!ec_quant) - NTSCDec(dtrEC!ec_quaeva))
          End If
        End If
      End If

      '-------------------------
      'Calcolo del rapporto cambio quantità/flevas/datcons
      If (dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "T") And dtrEC.RowState <> DataRowState.Added Then
        bCambioQuantita = False
        If NTSCDec(dtrEC!ec_quant) <> 0 Then
          dRapportoQta = NTSCDec(dtrEC("ec_quant", DataRowVersion.Original)) / NTSCDec(dtrEC!ec_quant)
        Else
          dRapportoQta = 0
        End If
        If dRapportoQta <> 1 Then bCambioQuantita = True

        '---
        'memorizzo che ho cambiato uno o più sconti di riga
        'al salvataggio di rigà dovrò ricalcolare il valore di riga selle produz terziste
        bCambioSconti = False
        If NTSCDec(dtrEC!ec_scont1) <> NTSCDec(dtrEC("ec_scont1", DataRowVersion.Original)) Or _
           NTSCDec(dtrEC!ec_scont2) <> NTSCDec(dtrEC("ec_scont2", DataRowVersion.Original)) Or _
           NTSCDec(dtrEC!ec_scont3) <> NTSCDec(dtrEC("ec_scont3", DataRowVersion.Original)) Or _
           NTSCDec(dtrEC!ec_scont4) <> NTSCDec(dtrEC("ec_scont4", DataRowVersion.Original)) Or _
           NTSCDec(dtrEC!ec_scont5) <> NTSCDec(dtrEC("ec_scont5", DataRowVersion.Original)) Or _
           NTSCDec(dtrEC!ec_scont6) <> NTSCDec(dtrEC("ec_scont6", DataRowVersion.Original)) Or _
           NTSCDec(dtrEC!ec_scontp) <> NTSCDec(dtrEC("ec_scontp", DataRowVersion.Original)) Or _
           NTSCDec(dtrEC!ec_scontv) <> NTSCDec(dtrEC("ec_scontv", DataRowVersion.Original)) Then
          bCambioSconti = True
        End If

        '---
        bCambioFlevas = False
        If BGESTORDLIST = False Then
          If BORDINI Then
            If NTSCStr(dtrEC("ec_flevas", DataRowVersion.Original)) <> NTSCStr(dtrEC!ec_flevas) Then
              bCambioFlevas = True
            End If
          Else
            If NTSCStr(dtrEC("ec_salcon", DataRowVersion.Original)) <> NTSCStr(dtrEC!ec_salcon) Then
              bCambioFlevas = True
            End If
          End If
        End If    'If bGestOrdlist = False Then

        bCambioCommessa = False

        If NTSCInt(dtrEC("ec_commeca", DataRowVersion.Original)) <> NTSCInt(dtrEC!ec_commeca) Then
          bCambioCommessa = True
        End If

      End If    'If dttet.rows(0)!et_tipork.ToString = "H" Then

      '-------------------------
      'Calcolo del rapporto cambio quantità x CONAI
      If bConai And BORDINI = False Then
        If dttEt_conto.Rows(0)!an_gescon.ToString <> "N" And bDocEmesso And _
           dttET.Rows(0)!et_tipork.ToString <> "W" And dttET.Rows(0)!et_scorpo.ToString = "N" Then
          If NTSCDec(dtrEC!ec_quant) <> 0 Then
            dRapportoQta = NTSCDec(dtrEC("ec_quant", DataRowVersion.Original)) / NTSCDec(dtrEC!ec_quant)
          Else
            dRapportoQta = 0
          End If
          If dRapportoQta <> 1 Then bCambioQuantita = True
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
  Public Overridable Function CorpoAfterUpdate(ByVal nRow As Integer) As Boolean
    Dim dtrEC As DataRow
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim msg As NTSEventArgs = Nothing
    Dim dRappo As Decimal = 0

    Try
      dtrEC = dttEC.Rows(nRow)

      '------------------------
      'Aggiornamento componenti CONAI
      If bConai And BORDINI = False Then
        If dttEt_conto.Rows(0)!an_gescon.ToString <> "N" And bDocEmesso And _
                 dttET.Rows(0)!et_tipork.ToString <> "W" And dttET.Rows(0)!et_scorpo.ToString = "N" Then
          If bCambioQuantita Then
            bCambioQuantita = False

            dtrT = dttMOVCONA.Select("mma_riga = " & dtrEC!ec_riga.ToString)
            If ArrDbl(dRapportoQta, 10) = 0 Then
              For i = 0 To dtrT.Length - 1
                dtrT(i)!mma_quant = 0
              Next
            Else
              For i = 0 To dtrT.Length - 1
                dtrT(i)!mma_quant = Math.Round(NTSCDec(dtrT(i)!mma_quant) / ArrDbl(dRapportoQta, 10), 3)
              Next
            End If
            dttMOVCONA.AcceptChanges()
          End If
        End If    'If bConai And dttEt_conto.Rows(0)!an_gescon.ToString <> "N" And bDocEmesso And _
      End If

      '------------------------
      'se ho modificato la quantità di un padre riga kit, cambio proporzionalmente anche le quantità delle righe figlio
      'no se docum. di produzione, modulo PM attivo, articoli a lotti e/o matricole e/o conai
      'non opera se in precedenza la quantità del padre e/o dei figli era stata portata a 0
      If bAggiornaQtaFigliKitDaPadre And BGESTORDLIST = False And bInImportRigheOrd = False And _
         bInImportRigheNota = False And bInImportRigheOff = False And bModPM = False And bConai = False And _
         dttET.Rows(0)!et_tipork.ToString <> "H" And dttET.Rows(0)!et_tipork.ToString <> "T" AndAlso _
          (dtrEC!ec_flkit.ToString = "A" Or dtrEC!ec_flkit.ToString = "S") Then
        If dQtaRowEnter <> NTSCDec(dtrEC!ec_quant) Then
          If NTSCDec(dtrEC!ec_quant) <> 0 Then dRappo = dQtaRowEnter / NTSCDec(dtrEC!ec_quant)
        End If
        If dRappo <> 0 Then
          For Each dtrTK As DataRow In dtrEC.Table.Select("xxo_gestmatr = 'N' AND xxo_geslotti = 'N' " & _
                                                          " AND ec_flkit IN ('B', 'T') " & _
                                                          " AND ec_ktriga = " & NTSCInt(dtrEC!ec_riga))
            dtrTK!ec_quant = ArrDbl(NTSCDec(dtrTK!ec_quant) / dRappo, oApp.NDecQta)
            'cerco di aggiornare anche i colli
            If NTSCDec(dtrTK!ec_colli) <> 0 Then
              bInValidazQuant = True      'altrimenti al cambio dei colli viene ricalcolata la quantità
              dtrTK!ec_colli = ArrDbl(NTSCDec(dtrTK!ec_colli) / dRappo, oApp.NDecQta)
              bInValidazQuant = False
            End If
            If Not RecordSalva(dtrTK, False, Nothing) Then
              dtrTK.RejectChanges()
            End If
          Next
        End If

        'comunque aggiorno la data di consegna prendendola dal padre del kit
        If BORDINI Then
          For Each dtrTK As DataRow In dtrEC.Table.Select("xxo_gestmatr = 'N' AND xxo_geslotti = 'N' " & _
                                                          " AND ec_flkit IN ('B', 'T') " & _
                                                          " AND ec_ktriga = " & NTSCInt(dtrEC!ec_riga) & _
                                                          " AND ec_datcons <> " & CDataSQL(NTSCDate(dtrEC!ec_datcons)))
            dtrTK!ec_datcons = dtrEC!ec_datcons
            dtrTK.AcceptChanges()
          Next
        End If
      End If    'If bAggiornaQtaFigliKitDaPadre And BGESTORDLIST = False ...

      '------------------------
      If (dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "T") And _
          Not (dtrEC!ec_codart.ToString.ToUpper = "D" And Not bEsplodiD) Then

        '--------------------------------
        'ho cambiato articolo o fase: devo cancellare gli scarichi collegati
        If bCancellaScarichi Then
          msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128545629707174000, _
                                     "Visto che è stato cambiato il codice dell'articolo di questa riga di ORDINE/DOCUMENTO DI PRODUZIONE," & _
                                      " eliminare tutte le righe di IMPEGNO/SCARICO DI PRODUZIONE associate a questa riga?" & vbCrLf & vbCrLf & _
                                      "ATTENZIONE!" & vbcrlf & _
                                      "Scegliendo si, NON saranno ricreate nuove righe dalla Distinta Base sul nuovo codice articolo sostituito" & _
                                      " negli IMPEGNI/SCARICHI DI PRODUZIONE." & vbcrlf & _
                                      "Scegliendo NO rimarranno inalterati."))
          ThrowRemoteEvent(msg)
          If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
            If BORDINI Then
              'Materiali
              dtrT = dttECIMP.Select("ec_rigaor = " & dtrEC!ec_riga.ToString)
              For i = 0 To dtrT.Length - 1
                'dettaglio tco di materiali
                If bModTCO Then
                  dtrT1 = dttECIMPTC.Select("ec_riga = " & dtrT(i)!ec_riga.ToString)
                  For l = 0 To dtrT1.Length - 1
                    dtrT1(l).Delete()
                  Next
                End If
                dtrT(i).Delete()
              Next
              If bModTCO Then dttECIMPTC.AcceptChanges()
              dttECIMP.AcceptChanges()
              'Lavorazioni
              dtrT = dttATTIVIT.Select("at_riga = " & dtrEC!ec_riga.ToString)
              For i = 0 To dtrT.Length - 1
                dtrT(i).Delete()
              Next
              dttATTIVIT.AcceptChanges()
              dtrT = dttASSRIS.Select("as_riga = " & dtrEC!ec_riga.ToString)
              For i = 0 To dtrT.Length - 1
                dtrT(i).Delete()
              Next
              dttASSRIS.AcceptChanges()
            Else
              'Materiali
              dtrT = dttECIMP.Select("ec_prriga = " & dtrEC!ec_riga.ToString)
              For i = 0 To dtrT.Length - 1
                'matricole
                dtrT1 = dttMOVMATR.Select("mma_riga = " & dtrT(i)!ec_riga.ToString)
                For l = 0 To dtrT1.Length - 1
                  dtrT1(l).Delete()
                Next
                'dettaglio tco di materiali
                If bModTCO Then
                  dtrT1 = dttECIMPTC.Select("ec_riga = " & dtrT(i)!ec_riga.ToString)
                  For l = 0 To dtrT1.Length - 1
                    dtrT1(l).Delete()
                  Next
                End If
                dtrT(i).Delete()
              Next
              If bModTCO Then dttECIMPTC.AcceptChanges()
              dttMOVMATR.AcceptChanges()
              dttECIMP.AcceptChanges()

              'Lavorazioni
              dtrT = dttLAVCENT.Select("lce_riga = " & dtrEC!ec_riga.ToString)
              For i = 0 To dtrT.Length - 1
                dtrT(i).Delete()
              Next
              dttLAVCENT.AcceptChanges()
            End If    'If BORDINI Then

            ValorizzaProduzione(dtrEC)
            SettaValoriRiga(dtrEC)
          Else
            If BORDINI = False Then
              'Lavorazioni
              dtrT = dttLAVCENT.Select("lce_riga = " & dtrEC!ec_riga.ToString)
              For i = 0 To dtrT.Length - 1
                dtrT(i)!lce_codart = dtrEC!ec_codart
                dtrT(i)!lce_ortipo = " "
                dtrT(i)!lce_oranno = 0
                dtrT(i)!lce_orserie = " "
                dtrT(i)!lce_ornum = 0
                dtrT(i)!lce_orriga = 0
                dtrT(i)!lce_orfase = 0
                dtrT(i)!lce_flevas = "C"
                dtrT(i)!lce_tempattees = 0
                dtrT(i)!lce_tempesees = 0
                dtrT(i)!lce_gtipork = " "
                dtrT(i)!lce_ganno = 0
                dtrT(i)!lce_gserie = " "
                dtrT(i)!lce_gnumdoc = 0
                dtrT(i)!lce_griga = 0
                dtrT(i)!lce_grigaa = 0
                dtrT(i)!lce_grflevas = "C"
                dtrT(i)!lce_grqtaeva = 0
                dtrT(i)!lce_grqtaevasca = 0
                dtrT(i)!lce_grtatteva = 0
                dtrT(i)!lce_grteseeva = 0
              Next
              dttLAVCENT.AcceptChanges()

              'Materiali: rimuovo il riferimento ad impegni/note di prelievo
              dtrT = dttECIMP.Select("ec_prriga = " & dtrEC!ec_riga.ToString)
              For i = 0 To dtrT.Length - 1
                dtrT(i)!ec_ortipo = " "
                dtrT(i)!ec_oranno = 0
                dtrT(i)!ec_orserie = " "
                dtrT(i)!ec_ornum = 0
                dtrT(i)!ec_orriga = 0
                dtrT(i)!ec_salcon = "C"
                dtrT(i)!ec_qtadisimp = 0
                dtrT(i)!ec_coldisimp = 0
                dtrT(i)!ec_valdisimp = 0
                dtrT(i)!ec_nptipo = " "
                dtrT(i)!ec_npanno = 0
                dtrT(i)!ec_npserie = " "
                dtrT(i)!ec_npnum = 0
                dtrT(i)!ec_npriga = 0
                dtrT(i)!ec_npsalcon = "C"
                dtrT(i)!ec_npqtadis = 0
                dtrT(i)!ec_npcoldis = 0
                dtrT(i)!ec_npvaldis = 0
              Next
              dttECIMP.AcceptChanges()
            End If
          End If    'If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then

          bCancellaScarichi = False
        End If

        '---------------------------------
        'al cambio della quantità (o flag 'evaso') sulla riga di produzione chiedo se cambiarla sulle righe degli impegni/lavorazioni
        If BORDINI Then
          If bCambioQuantita Or bCambioFlevas Then
            msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128740036701717000, _
                            "Visto che è stata cambiata la quantità ordinata e/o l'opzione di evasione " & _
                            "dell'articolo di questa riga di ORDINE DI PRODUZIONE, " & vbCrLf & _
                            "cambiare proporzionalmente le quantità e/o l'opzione di evasione su tutte le righe di IMPEGNO DI PRODUZIONE " & vbCrLf & _
                            "associate a questa riga?"))
            If BGESTORDLIST Or bInNuovoDocSilent Then
              'nelle proposte d'ordine la modifica della quantità deve esempre essere riversata sui materiali e lavorazioni, diversamente BNORGNNP non lavora correttamente (vb6 si comporta già così)
              msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
            Else
              ThrowRemoteEvent(msg)
            End If

            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              AggImpegniMateriali(dRapportoQta, dtrEC)
              AggImpegniLavoraz(dRapportoQta, dtrEC)
            End If

            '--------------------------------
            'Valorizza la riga: viene rifatto perchè cambiando le quantità in CORPOIMP ec_vamoremm di CORPO deve essere ricalcolato come somma di valori righe di CORPOIMP
            ValorizzaProduzione(dtrEC)
            dtrEC.AcceptChanges()

            If bModPM Then
              If bModPM And NTSCInt(dtrEC!ec_pmtaskid) > 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036757409000, "ATTENZIONE:" & vbCrLf & _
                       "Prima del salvataggio della riga si procede alla rivalorizzazione del 'Ordine di Produzione'." & vbCrLf & _
                       "A causa di ciò verranno ricalcolati anche i valori dei campi relativi al Project Management.")))
              End If
              SistemaPMSalconDaQta(dtrEC)
            End If

            bCambioQuantita = False
            bCambioFlevas = False

          End If  'If bCambioQuantita Or bCambioFlevas Then

          If bCambioCommessa Then
            msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128740036701712000, _
                                        "Visto che è stata cambiata la commessa dell'articolo " & vbCrLf & _
                                        "di questa riga di ORDINE DI PRODUZIONE, " & vbCrLf & _
                                        "cambiare la commessa su tutte le righe di IMPEGNO DI PRODUZIONE" & vbCrLf & _
                                        "associate a questa riga?"))
            If BGESTORDLIST Or bInNuovoDocSilent Then
              'nelle proposte d'ordine la modifica della commessa deve sempre essere riversata sui materiali e lavorazioni, diversamente BNORGNNP non lavora correttamente (vb6 si comporta già così)
              msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
            Else
              ThrowRemoteEvent(msg)
            End If
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              AggCommessaImpegni(dtrEC)
            End If
          End If

        Else
          'veboll
          If bCambioQuantita Then
            msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128552272157516000, _
                            "Visto che è stata cambiata la quantità dell'articolo di questa riga di CARICO DI PRODUZIONE, " & vbCrLf & _
                            "cambiare proporzionalmente le quantità di evasione su tutte le righe di SCARICHI DI PRODUZIONE " & vbCrLf & _
                            "associate a questa riga?"))
            If bInNuovoDocSilent Then
              msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
            Else
              ThrowRemoteEvent(msg)
            End If
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              AggScarichiMateriali(dRapportoQta, dtrEC)
            End If

            '--------------------------------
            'Per il cambio delle lavorazioni controlla che non ci siano riferimenti
            'a LAVCENT tipo G
            If dttLAVCENT.Select("lce_riga = " & dtrEC!ec_riga.ToString & " AND lce_gnumdoc > 0").Length > 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128680301520659954, _
                            "I tempi di esecuzione e le quantità lavorate non possono essere aggiornate " & _
                            "automaticamente quando esistono riferimenti a AVANZAMENTI DI LAVORAZIONE! " & _
                            "Provvedere manualmente ad aggiustare i tempi e le quantità lavorate.")))
              GoTo SaltaAggLav
            End If

            If dRapportoQta > 1 Then
              'La quantità è diminuita
              msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYESCANC, oApp.Tr(Me, 128680303309722454, _
                            "Visto che è stata diminuita la quantità dell'articolo di questa riga di CARICO DI PRODUZIONE, " & vbCrLf & _
                            "considerare scarto la differenza sull'ultima fase LAVORAZIONI?" & _
                            vbCrLf & vbCrLf & "Premere SI per considerare la differenza come scarto," & vbCrLf & _
                            "Premere NO per cambiare proporzionalemte tempi e quantità," & vbCrLf & _
                            "Premere ANNULLA per non effettuare cambiamenti sulle lavorazioni."))
              If bInNuovoDocSilent Then
                msg.RetValue = CLN__STD.ThMsg.RETVALUE_NO
              Else
                ThrowRemoteEvent(msg)
              End If
              Select Case msg.RetValue
                Case CLN__STD.ThMsg.RETVALUE_YES
                  'Considera la differenza come scarto
                  'Trova la riga dell'ultima fase
                  dtrT = dttLAVCENT.Select("lce_riga = " & dtrEC!ec_riga.ToString, "lce_rigaa DESC")
                  If dtrT.Length > 0 Then
                    If NTSCInt(dtrT(0)!lce_rigaa) > 0 Then
                      dtrT(0)!lce_qtascart = NTSCDec(dtrT(0)!lce_qtascart) + (NTSCDec(dtrT(0)!lce_qtaes) - NTSCDec(dtrEC!ec_quant))
                      dtrT(0)!lce_qtaes = NTSCDec(dtrEC!ec_quant)
                      '---------------------------
                      'mantengo il flag di saldo ordine uguale a quello della riga di carico, visto che cambiando i colli potrebbe essere pasato da stato S a C mentre sul carico volgio mantenere l'ordine chiuso ...
                      dtrT(0)!lce_flevas = dtrEC!ec_salcon.ToString
                    End If
                  End If
                Case CLN__STD.ThMsg.RETVALUE_NO
                  'Cambio proporzionale di tempi e qtà
                  AggScarichiLavoraz(dRapportoQta, dtrEC)
              End Select
            Else
              'La quantità è aumentata o è stata azzerata
              msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128680305633784954, _
                            "Visto che è stata cambiata la quantità dell'articolo di questa riga di CARICO DI PRODUZIONE, " & vbCrLf & _
                            "cambiare proporzionalmente i tempi e le quantità su tutte le righe di LAVORAZIONE, associati a questa riga?"))
              If bInNuovoDocSilent Then
                msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
              Else
                ThrowRemoteEvent(msg)
              End If
              If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
                If dRapportoQta = 0 Then
                  'quantità azzerata
                  dtrT = dttLAVCENT.Select("lce_riga = " & dtrEC!ec_riga.ToString)
                  For i = 0 To dtrT.Length - 1
                    dtrT(i)!lce_qtaes = 0
                    dtrT(i)!lce_qtascart = 0
                    dtrT(i)!lce_valore = 0
                    dtrT(i)!lce_valorev = 0
                    dtrT(i)!lce_valmo = 0
                  Next
                Else
                  'Quantità aumentata
                  AggScarichiLavoraz(dRapportoQta, dtrEC)
                End If
              End If
            End If

SaltaAggLav:
            '--------------------------------
            'Valorizza la riga: viene rifatto perchè cambiando le quantità in CORPOIMP ec_vamoremm di CORPO deve essere ricalcolato come somma di valori righe di CORPOIMP
            ValorizzaProduzione(dtrEC)
            dtrEC.AcceptChanges()

            If bModPM Then
              If bModPM And NTSCInt(dtrEC!ec_pmtaskid) > 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128552277849698000, "ATTENZIONE:" & vbCrLf & _
                       "Prima del salvataggio della riga si procede alla rivalorizzazione del 'Carico da Produzione'." & vbCrLf & _
                       "A causa di ciò verranno ricalcolati anche i valori dei campi relativi al Project Management.")))
              End If
              SistemaPMSalconDaQta(dtrEC)
            End If

            bCambioQuantita = False
          End If    'If bCambioQuantita Then

          If bCambioFlevas Then
            msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128680316346441204, _
                          "Visto che è stata cambiata l'opzione Saldo/Conto dell'articolo di questa riga di CARICO DA PRODUZIONE, " & vbCrLf & _
                          "cambiare questa opzione su tutte le righe di SCARICO DA PRODUZIONE e sulle LAVORAZIONI, associate a questa riga?"))
            ThrowRemoteEvent(msg)
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              SettaSaldoRigaScarichiELavorazioni(dtrEC)
            End If
            dttECIMP.AcceptChanges()
            dttLAVCENT.AcceptChanges()

            bCambioFlevas = False
          End If    'If bCambioFlevas Then

          If bCambioCommessa Then
            msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128740036701711000, _
                                        "Visto che è stata cambiata la commessa dell'articolo " & vbCrLf & _
                                        "di questa riga di CARICO DI PRODUZIONE, " & vbCrLf & _
                                        "cambiare la commessa su tutte le righe di SCARICO DI PRODUZIONE" & vbCrLf & _
                                        "associate a questa riga?"))
            If BGESTORDLIST Or bInNuovoDocSilent Then
              'nelle proposte d'ordine la modifica della commessa deve sempre essere riversata sui materiali e lavorazioni, diversamente BNORGNNP non lavora correttamente (vb6 si comporta già così)
              msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
            Else
              ThrowRemoteEvent(msg)
            End If
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              AggCommessaScarichi(dtrEC)
            End If
          End If

        End If    'If BORDINI Then

        'ho cambiato degli sconti di riga in un carico da prdo terzista: ricalcolo il valore di riga
        If bTerzista And bCambioSconti Then
          ValorizzaProduzione(dtrEC)
          SettaValoriRiga(dtrEC)
        End If

      End If    'If (dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "T") And _

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
      bCancellaScarichi = False
      lDiffDatcons = 0
      dRapportoQta = 0
      bCambioFlevas = False
      bCambioQuantita = False
      bCambioSconti = False
      bCambioCommessa = False
    End Try
  End Function

  Public Overridable Function CorpoAfterDelete(ByRef dtrDeleted As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim strNomeRiga As String = ""
    Try

      'colonna degli scarichi che collega alla riga di carico
      If BORDINI Then
        strNomeRiga = "ec_rigaor"
      Else
        strNomeRiga = "ec_prriga"
      End If

      '-------------------------------
      'Se  attivo il modulo Taglie e colori, cancella l'eventuale record associato in MOTRANSTC
      'sia per veboll che per gsor
      If bModTCO = True Then

        If dttET.Rows(0)!et_tipork.ToString = "H" Then
          'cancello il dettaglio TCO degli scarichi collegati. 
          'prima trovo gli scarichi collegati alla riga cancellata, 
          'poi per ogni riga di scarico cancello il dettaglio TCO
          dtrT = dttECIMP.Select(strNomeRiga & " = " & NTSCInt(dtrDeleted!ec_riga).ToString)
          For i = 0 To dtrT.Length - 1
            dtrT1 = dttECIMPTC.Select("ec_riga = " & NTSCInt(dtrT(i)!ec_riga).ToString)
            For l = 0 To dtrT1.Length - 1
              dtrT1(l).Delete()
            Next
          Next
        End If

        GestisciDettaglioTCO(4, NTSCInt(dtrDeleted!ec_riga), False)     'cancello CORPOTC
      End If    'If bModTCO = True Then

      '-------------------------------
      'cancello gli impegni e le lavorazioni collegate
      If dttET.Rows(0)!et_tipork.ToString = "H" Then
        dtrT = dttECIMP.Select("ec_rigaor = " & NTSCInt(dtrDeleted!ec_riga).ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i).Delete()
        Next

        dtrT = dttASSRIS.Select("as_riga = " & NTSCInt(dtrDeleted!ec_riga).ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i).Delete()
        Next

        dtrT = dttATTIVIT.Select("at_riga = " & NTSCInt(dtrDeleted!ec_riga).ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i).Delete()
        Next
      End If

      '-------------------------------
      If bModPM Then
        'da fare
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

  Public Overridable Function CorpoCheckRiga(ByVal lTipo As Integer, ByRef dtrEC As DataRow) As Boolean
    ' ltipo = 1 -> Test esistenza
    '         2 -> Test sotto-costo
    '         3 -> Entrambi
    Dim strFlkit As String = " "
    Dim nCausale As Integer = 0
    Dim dQtaDaAss As Decimal = 0
    Dim bRigaNotaPrel As Boolean = False
    Try
      'il test essitenza ed il test sottocosto in veboll vengono eseguiti anche durante l'importazione delle righe 
      'da ordine (se l'opzione di reg. è settata opportunamente) e, se necessario, la riga non viene importata
      'If bInImportRigheOff Then Return True
      'If bInImportRigheNota Then Return True
      'If bInImportRigheOrd Then Return True

      If BORDINI = False Then
        nCausale = NTSCInt(dtrEC!ec_causale)
        dQtaDaAss = 0
      Else
        dQtaDaAss = NTSCDec(dtrEC!xxo_qtadaass)
      End If

      If BGESTORDLIST = False Then
        strFlkit = dtrEC!ec_flkit.ToString
      End If

      If lTipo = 1 Or lTipo = 3 Then
        '------------------------------
        'TEST ESISTENZA E SOTTOSCORTA
        'If dtrEC!ec_tipork.ToString <> "H" And dtrEC!ec_tipork.ToString <> "T" And dtrEC!ec_tipork.ToString <> "Y" And dtrEC!ec_tipork.ToString <> "U" Then
        If BORDINI = False Then
          bRigaNotaPrel = CBool(IIf(NTSCInt(dtrEC!ec_npnum) <> 0, True, False))
        End If
        If Not TestEsistenza(dtrEC!ec_codart.ToString, NTSCInt(dtrEC!ec_magaz), NTSCInt(dtrEC!ec_fase), _
                             NTSCDec(dtrEC!ec_quant), dQtaDaAss, NTSCInt(dtrEC!ec_commeca), nCausale, _
                             bRigaNotaPrel, dtrEC) Then
          Return False
        End If
        'End If
        'già fatto prima di inserire la riga di scarico
        'If (dtrEC!ec_tipork.ToString = "Y" Or dtrEC!ec_tipork.ToString = "U") And bTest_esistenza_duplimpprod Then
        '  If Not TestEsistenza(dtrEC!ec_codart.ToString, NTSCInt(dtrEC!ec_magaz), NTSCInt(dtrEC!ec_fase), _
        '                       NTSCDec(dtrEC!ec_quant), 0, NTSCInt(dtrEC!ec_commeca), nCausale) Then
        '    Return False
        '  End If
        'End If
      End If


      If lTipo = 2 Or lTipo = 3 Then
        '------------------------------
        'TEST VENDITA SOTTOCOSTO
        If Not TestSottoCosto(dtrEC!ec_codart.ToString, NTSCInt(dtrEC!ec_fase), NTSCDec(dtrEC!ec_quant), _
                              NTSCDec(dtrEC!ec_prezzo), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCInt(dttET.Rows(0)!et_valuta), _
                              NTSCDec(dtrEC!ec_scont1), NTSCDec(dtrEC!ec_scont2), NTSCDec(dtrEC!ec_scont3), _
                              NTSCDec(dtrEC!ec_scont4), NTSCDec(dtrEC!ec_scont5), NTSCDec(dtrEC!ec_scont6), _
                              NTSCDec(dttET.Rows(0)!et_scont1), NTSCDec(dttET.Rows(0)!et_scont2), _
                              NTSCDec(dttET.Rows(0)!et_scopag), strFlkit, NTSCInt(dtrEC!ec_riga), dtrEC, _
                              NTSCDec(dtrEC!ec_scontp), NTSCDec(dtrEC!ec_scontv)) Then
          Return False
        End If

        If dttET.Rows(0)!et_tipork.ToString = "T" Then
          '------------------------------
          'TEST COSTO CARICO
          If Not CheckCostoCaricoRiga(dtrEC) Then
            Return False
          End If
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

  Public Overridable Function CheckCostoCaricoRiga(ByRef dtrEc As DataRow) As Boolean
    Dim bMessaggio As Boolean = False
    Dim dPrezzo1Out As Decimal
    Dim dPrezzo2Out As Decimal
    Dim dPrezzoCalcolato As Decimal
    Dim dPerQta As Decimal
    Dim strMsg As String = ""
    Dim dDiffValore As Decimal
    Dim strTmp As String = ""

    Try
      If NTSCStr(dtrEc!ec_codart).Trim = "" Then Return True
      If dtrEc!ec_tipork.ToString <> "T" Then Return True

      'Se non è un Carico da Produzione esce (non entra neanche)
      'Se le impostazioni di registro sono false esce
      If bTestCostoCaricoRiga = False Then Return True
      'If (nTestCostoCaricoListino1 = 0) And (nTestCostoCaricoListino2 = 0) Then Return True

      'per non dare il messaggio al saltaggio di riga prima di esplodere la DB, non faccio il test se 
      'alla riga corrente non sono presenti record di scarichi o lavorazioni
      If dttECIMP Is Nothing Or dttLAVCENT Is Nothing Then Return True
      If dttECIMP.Select("ec_prtipo = " & CStrSQL(dtrEc!ec_tipork.ToString) & _
                        " AND ec_pranno = " & dtrEc!ec_anno.ToString & _
                        " AND ec_prserie = " & CStrSQL(dtrEc!ec_serie.ToString) & _
                        " AND ec_prnum = " & dtrEc!ec_numdoc.ToString & _
                        " AND ec_prriga = " & dtrEc!ec_riga.ToString).Length = 0 And _
        dttLAVCENT.Select("lce_tipork = " & CStrSQL(dtrEc!ec_tipork.ToString) & _
                        " AND lce_anno = " & dtrEc!ec_anno.ToString & _
                        " AND lce_serie = " & CStrSQL(dtrEc!ec_serie.ToString) & _
                        " AND lce_numdoc = " & dtrEc!ec_numdoc.ToString & _
                        " AND lce_riga = " & dtrEc!ec_riga.ToString).Length = 0 Then Return True

      If dtrEc!ec_umprz.ToString <> "S" Then
        '-----------------------
        'Se la quantità è = 0 esce
        If NTSCDec(dtrEc!ec_quant) = 0 Then Return True
      Else
        '-----------------------
        'Se il numero colli è = 0 esce
        If NTSCDec(dtrEc!ec_colli) = 0 Then Return True
      End If

      '-------------------------
      'Se il Tipo Opzione dell'articolo è diverso da: "Prod.Finito (Reale)" esce
      If Not bTestCostoCaricoAncheSL Then
        If GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_tipoopz.ToString <> "O" Then
          Return True
        End If
      End If

      dPrezzo1Out = 0
      dPrezzo2Out = 0

      SettaCondCommerciali_IstanziaNTSCondCommerciali()
      oCondCommerciali.bCalcolaPrezzo = True
      With oCondCommerciali.Input
        .strDitta = strDittaCorrente
        .strCodart = NTSCStr(dtrEc!ec_codart)
        .strUnmis = NTSCStr(dtrEc!ec_unmis)
        .strUmp = NTSCStr(dtrEc!ec_ump)
        .nFase = NTSCInt(dtrEc!ec_fase)
        .strTipoval = "P"
        .bConspromo = True
        .dtDatdoc = NTSCDate(dttET.Rows(0)!et_datdoc)
        .dColli = NTSCDec(dtrEc!ec_colli)
        .dQuant = NTSCDec(dtrEc!ec_quant)
        .bConsideraPrezziUnmis = True
        .bPrezziPerUnmis = (NTSCStr(dtrEc!ec_umprz) = "S")
      End With
      For lGiro As Integer = 1 To 2
        If lGiro = 1 Then
          If nTestCostoCaricoListino1 = 0 Then Continue For
          oCondCommerciali.Input.nListino = nTestCostoCaricoListino1
        Else
          If nTestCostoCaricoListino2 = 0 Then Continue For
          oCondCommerciali.Input.nListino = nTestCostoCaricoListino2
          oCondCommerciali.SvuotaOutput()
        End If

        CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)

        If lGiro = 1 Then
          dPrezzo1Out = oCondCommerciali.OutputPrezzo.dPrezzo
        Else
          dPrezzo2Out = oCondCommerciali.OutputPrezzo.dPrezzo
        End If
      Next

      If dtrEc!ec_umprz.ToString = "S" Then
        dPrezzoCalcolato = ArrDbl((NTSCDec(dtrEc!ec_valore) / NTSCDec(dtrEc!ec_colli) * dPerQta), oCldDocu.TrovaNdecSuPrzUn(0))
      Else
        dPrezzoCalcolato = ArrDbl((NTSCDec(dtrEc!ec_valore) / NTSCDec(dtrEc!ec_quant) * dPerQta), oCldDocu.TrovaNdecSuPrzUn(0))
      End If

      'Che tipo di controllo è da fare?: SUPERIORE (default) o INFERIORE (su opzione)
      If bTestCostoCaricoInferiore Then
        'Determina lo scarto in base alla opzione di registro
        dDiffValore = 0
        If dTestCostoCaricoPercDiff <> 0 Then
          dDiffValore = ArrDbl((dPrezzoCalcolato * dTestCostoCaricoPercDiff) / 100, oCldDocu.TrovaNdecSuPrzUn(0))
        End If
        If nTestCostoCaricoListino1 > 0 Then
          If ((dPrezzoCalcolato - dDiffValore) < dPrezzo1Out) Then bMessaggio = True
        End If
        If nTestCostoCaricoListino2 > 0 Then
          If ((dPrezzoCalcolato - dDiffValore) < dPrezzo2Out) Then bMessaggio = True
        End If
      Else
        'caso default
        If (dPrezzo1Out + dPrezzo2Out) = 0 Then
          bMessaggio = True
        Else
          If nTestCostoCaricoListino1 > 0 Then
            If (dPrezzoCalcolato > dPrezzo1Out) And (dPrezzo1Out <> 0) Then bMessaggio = True
          End If
          If nTestCostoCaricoListino2 > 0 Then
            If (dPrezzoCalcolato > dPrezzo2Out) And (dPrezzo2Out <> 0) Then bMessaggio = True
          End If
          If ((nTestCostoCaricoListino1 + nTestCostoCaricoListino2) > 0) And ((dPrezzo1Out + dPrezzo2Out) = 0) Then
            bMessaggio = True
          End If
        End If
      End If
      '-----------------------------------------------------------------------------------------
      If bMessaggio = True Then
        If bTestCostoCaricoInferiore Then
          strTmp = oApp.Tr(Me, 128680187078159954, "inferiore")
        Else
          strTmp = oApp.Tr(Me, 128680187374722454, "superiore")
        End If

        If (nTestCostoCaricoListino1 > 0) And (nTestCostoCaricoListino2 > 0) Then
          strMsg = oApp.Tr(Me, 128680187654409954, "Attenzione!" & vbCrLf & _
            "Il costo di carico per l'articolo '|" & NTSCStr(dtrEc!ec_codart) & "|' prezzo: |" & dPrezzoCalcolato.ToString(oApp.FormatPrzUn) & "|" & vbCrLf & _
            "è |" & strTmp & "| al listino '|" & nTestCostoCaricoListino1.ToString & "|' prezzo: |" & dPrezzo1Out.ToString(oApp.FormatPrzUn) & "|" & vbCrLf & _
            "e/o al listino '|" & CStr(nTestCostoCaricoListino2) & "|' prezzo: |" & dPrezzo2Out.ToString(oApp.FormatPrzUn) & "|.")
        End If
        If (nTestCostoCaricoListino1 > 0) And (nTestCostoCaricoListino2 = 0) Then
          strMsg = oApp.Tr(Me, 128680187771284954, "Attenzione!" & vbCrLf & _
            "Il costo di carico per l'articolo '|" & NTSCStr(dtrEc!ec_codart) & "|' prezzo: |" & dPrezzoCalcolato.ToString(oApp.FormatPrzUn) & "|" & vbCrLf & _
            "è |" & strTmp & "| al listino '|" & nTestCostoCaricoListino1.ToString & "|' prezzo: |" & dPrezzo1Out.ToString(oApp.FormatPrzUn) & "|" & ".")
        End If
        If (nTestCostoCaricoListino1 = 0) And (nTestCostoCaricoListino2 > 0) Then
          strMsg = oApp.Tr(Me, 128680187759878704, "Attenzione!" & vbCrLf & _
            "Il costo di carico per l'articolo '|" & NTSCStr(dtrEc!ec_codart) & "|' prezzo: |" & dPrezzoCalcolato.ToString(oApp.FormatPrzUn) & "|" & vbCrLf & _
            "è |" & strTmp & "| al prezzo di listino '|" & nTestCostoCaricoListino2.ToString & "|' prezzo: |" & dPrezzo2Out.ToString(oApp.FormatPrzUn) & "|" & ".")
        End If
        If (nTestCostoCaricoListino1 = 0) And (nTestCostoCaricoListino2 = 0) Then
          strMsg = oApp.Tr(Me, 128680187746753704, "Attenzione!" & vbCrLf & _
            "Il costo di carico per l'articolo '|" & NTSCStr(dtrEc!ec_codart) & "|' prezzo: |" & dPrezzoCalcolato.ToString(oApp.FormatPrzUn) & "|" & vbCrLf & _
            "è |" & strTmp & "| al listino '|" & nTestCostoCaricoListino1.ToString & "|' prezzo: |" & dPrezzo1Out.ToString(oApp.FormatPrzUn) & "|" & vbCrLf & _
            "e al listino '|" & nTestCostoCaricoListino2.ToString & "|' prezzo: |" & dPrezzo2Out.ToString(oApp.FormatPrzUn) & "|.")
        End If

        ThrowRemoteEvent(New NTSEventArgs("", strMsg))
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

  Public Overridable Function RecordSalva(ByRef dtrEc As DataRow, ByVal bDelete As Boolean, ByRef dtrDeleted As DataRow) As Boolean
    Dim i As Integer = 0
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

      For i = 0 To dttEC.Rows.Count - 1
        If NTSCInt(dttEC.Rows(i)!ec_riga) = NTSCInt(dtrEc!ec_riga) Then
          Return RecordSalva(i, bDelete, dtrDeleted)
        End If
      Next

      Return False

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
  Public Overridable Function RecordSalva(ByVal nRow As Integer, ByVal bDelete As Boolean, ByRef dtrDeleted As DataRow) As Boolean
    Dim nStato As Integer       '1 = nuova riga, 2 = riga modificata
    Dim dtrOrig As DataRow = Nothing
    Dim dttClone As DataTable = Nothing
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

      If bInUnload Then Return False
      If nRow < 0 Then Return True
      'attenzione: in fase di cancellazione riga passa di qui 2 volte: la prima per salvare la riga, la seconda per cancellarla!!!!!
      If nRow > dttEC.Rows.Count Then Return False
      If bDelete Then
        bHasChangesET = True
      Else
        If dttEC.Rows(nRow).RowState <> DataRowState.Unchanged Then bHasChangesET = True
      End If

      If nRow > dttEC.Rows.Count Then Return True 'passo di qui anche se sono sull'ultima riga (quella di addnew) 


      '---------------------------------------
      'associo i colori alle righe KIT FIGLI
      If bDelete = False And BGESTORDLIST = False Then
        Dim oState As DataRowState = dttEC.Rows(nRow).RowState
        If oState <> DataRowState.Deleted Then
          ColoraCelle(dttEC.Rows(nRow))

          If oState = DataRowState.Unchanged Then dttEC.Rows(nRow).AcceptChanges()
        End If
      End If

      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If bDelete = False Then
        If nRow >= 0 Then

          If dttEC.Rows(nRow).RowState = DataRowState.Added Then
            nStato = 1
            If Not CorpoTestPreSalva(dttEC, nRow) Then Return False
            If Not CorpoBeforeInsert(nRow) Then Return False
          End If

          If dttEC.Rows(nRow).RowState = DataRowState.Modified Then
            nStato = 2
            If Not CorpoTestPreSalva(dttEC, nRow) Then Return False
            If Not CorpoBeforeUpdate(nRow) Then Return False

            If bModPM Then
              'in modifica memorizzo la riga originaria, mi servirà dopo
              dttClone = dttEC.Clone()
              dttClone.ImportRow(dttEC.Rows(nRow))
              dttClone.Rows(0).RejectChanges()
              dttClone.Rows(0)!ec_riga = dttEC.Rows(nRow)!ec_riga 'Il numero di riga non va ripristinato, altrimenti darebbe errore nella ricerca del record da abbinare
            End If

          End If

          If dttEC.Rows(nRow).RowState = DataRowState.Modified OrElse dttEC.Rows(nRow).RowState = DataRowState.Deleted Then
            dColliRowEnter = NTSCDec(dttEC.Rows(nRow)("ec_colli", DataRowVersion.Original))
            dQtaRowEnter = NTSCDec(dttEC.Rows(nRow)("ec_quant", DataRowVersion.Original))
            Else
            dColliRowEnter = NTSCDec(dttEC.Rows(nRow)("ec_colli"))
            dQtaRowEnter = NTSCDec(dttEC.Rows(nRow)("ec_quant"))
          End If

          '-------------------------------
          'confermo le modifiche apportate alla riga
          dsShared.AcceptChanges()

          If nStato = 1 Then
            CorpoAfterInsert(nRow)

            'se serve assegno il numero di lotto in automatico
            ScaricaLottox(nRow, dttEC)
          End If

          If nStato = 2 Then
            CorpoAfterUpdate(nRow)
          End If

          '-------------------------
          If bModPM And nStato <> 0 Then

            If bInImportRigheOrd = False Or dttET.Rows(0)!et_tipork.ToString <> "T" Then 'Solo se non è evasione di ordini di produzione...

              'SistemaPMSalconDaQta(dttEC.Rows(nRow)) 'NON DEVO RIFARLO QUI: RICALCOLEREBBE LE COLONNE pmqtarim e pmvalrim

              If BORDINI = False Then
                'SE MODIFICO PRIMA DEVO STORNARE LA RIGA IN TTPROESEC
                If nStato = 2 Then
                  'in modifica devo stornare la riga, poi ne inserisco una con i nuovi valori
                  'devo utilizzare il datatable originario!!!! non quello modificato per stornare
                  If Not oCldDocu.AggProesebDaMovmagDao(strDittaCorrente, 1, "MMTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                                dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                                dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                                NTSCInt(dttEC.Rows(nRow)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                                Nothing, dttClone) Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443332571826172, "Attenzione: non è stato possibile aggiornare la commessa (storno in proeseb) sugli archivi !")))
                  End If
                  'agg. altri record di mmtrans, riferiti allo stesso task-id, se presenti
                  dQtadispass = 0 : dValdispass = 0
                  'legge i valori preesistenti
                  AggResAltriRkCorpo(1, 0, dttEC, dttClone.Rows(0), dQtadispass, dValdispass)
                End If

                'ADESSO AGGIORNO TTPROESEC CON LA NUOVA RIGA
                If Not oCldDocu.AggProesebDaMovmagDao(strDittaCorrente, 0, "MMTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                              dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                              dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                              NTSCInt(dttEC.Rows(nRow)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                              Nothing, dttEC) Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443358401240235, "Attenzione: non è stato possibile aggiornare la commessa (storno in proeseb) sugli archivi !")))
                End If
                ' agg altri record di ttpriana, rieriti allao staesso task id, se presenti
                ' scrive completamente i nuovi valori
                AggResAltriRkCorpo(0, 0, dttEC, dttEC.Rows(nRow), dQtadispass, dValdispass)
              Else
                'SE MODIFICO PRIMA DEVO STORNARE LA RIGA IN TTPROESEC
                If nStato = 2 Then
                  'in modifica devo stornare la riga, poi ne inserisco una con i nuovi valori
                  'devo utilizzare il datatable originario!!!! non quello modificato per stornare
                  If Not oCldDocu.AggProesebDaMovordDao(strDittaCorrente, 1, "MOTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                                dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                                dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                                NTSCInt(dttEC.Rows(nRow)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                                Nothing, dttClone) Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129445714204609375, "Attenzione: non è stato possibile aggiornare la commessa (storno in proeseb) sugli archivi !")))
                  End If
                  'agg. altri record di mmtrans, riferiti allo stesso task-id, se presenti
                  dQtadispass = 0 : dValdispass = 0
                  'legge i valori preesistenti
                  AggResAltriRkCorpo(1, 0, dttEC, dttClone.Rows(0), dQtadispass, dValdispass)
                End If

                'ADESSO AGGIORNO TTPROESEC CON LA NUOVA RIGA
                If Not oCldDocu.AggProesebDaMovordDao(strDittaCorrente, 0, "MOTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                              dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                              dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                              NTSCInt(dttEC.Rows(nRow)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                              Nothing, dttEC) Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129445714224365234, "Attenzione: non è stato possibile aggiornare la commessa (storno in proeseb) sugli archivi !")))
                End If
                ' agg altri record di ttpriana, rieriti allao staesso task id, se presenti
                ' scrive completamente i nuovi valori
                AggResAltriRkCorpo(0, 0, dttEC, dttEC.Rows(nRow), dQtadispass, dValdispass)
              End If    'If BORDINI = False Then

            End If
          End If 'If bModPM Then
        End If    'If nRow >= 0 Then
      Else
        If CorpoAfterDelete(dtrDeleted) = False Then Return False

        If bModPM Then
          '-------------------------
          'STORNO TTPROESEC
          'devo lavorare sulla riga che ho cancellato: la metto in un datatable clone
          dttClone = dttEC.Clone()
          dttClone.ImportRow(dtrDeleted)
          dttClone.Rows(0).RejectChanges()
          If BORDINI = False Then
            If Not oCldDocu.AggProesebDaMovmagDao(strDittaCorrente, 1, "MMTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                          dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                          dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                          NTSCInt(dtrDeleted!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                          Nothing, dttClone) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129445685044082031, "Attenzione: non è stato possibile aggiornare la commessa (storno in proeseb) sugli archivi !")))
            End If
          Else
            If Not oCldDocu.AggProesebDaMovordDao(strDittaCorrente, 1, "MOTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                          dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                          dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                          NTSCInt(dtrDeleted!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                          Nothing, dttClone) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129445714110048828, "Attenzione: non è stato possibile aggiornare la commessa (storno in proeseb) sugli archivi !")))
            End If
          End If
          'agg. altri record di mmtrans, riferiti allo stesso task-id, se presenti
          dQtadispass = 0 : dValdispass = 0
          AggResAltriRkCorpo(1, 0, dttEC, dttClone.Rows(0), dQtadispass, dValdispass)
        End If    'If bModPM And BORDINI = False Then

        '-------------------------------
        'confermo le modifiche apportate alla griglia
        dsShared.AcceptChanges()
      End If    'If Not bDelete Then

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

  Public Overridable Function AggResAltriRkCorpo(ByVal nOperaz As Integer, ByVal nTipoLegAgg As Integer, _
                                                 ByRef dttCorpo As DataTable, ByRef dtrCorpo As DataRow, ByVal dQtadispass As Decimal, _
                                                 ByVal dValdispass As Decimal) As Boolean
    'rispetto a VB6, visto che mmtrans e/o motrans sono in memoria, questa funzione è stata modificata appositamente per lavorare in memoria
    'prende il posto della AggResAltriRkMmtrans e AggResAltriRkMotrans
    ' 0=registra, 1 = storna
    'nTipolegagg = 0 legge ed aggiorna 1 = legge solo, 2 aggiorna solo

    Dim strPrevGrup As String = NTSCStr(dtrCorpo!xxo_prevgrup)

    Dim nSegno As Integer
    Dim dQtadis As Decimal, dValdis As Decimal
    Dim lTaskidpadre As Integer
    Dim lCommeca As Integer, lTaskid As Integer
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0

    Try
      If NTSCInt(dtrCorpo!ec_commeca) = 0 Or NTSCInt(dtrCorpo!ec_pmtaskid) = 0 Then
        Return True
      End If

      bInSalvarigaPM = True

      lCommeca = NTSCInt(dtrCorpo!ec_commeca)
      lTaskid = NTSCInt(dtrCorpo!ec_pmtaskid)

      If nTipoLegAgg = 2 Then ' solo scrittura
        dQtadis = dQtadispass
        dValdis = dValdispass

      Else
        dQtadis = NTSCDec(dtrCorpo!xxo_pmqtadisda)
        dValdis = NTSCDec(dtrCorpo!xxo_pmvaldisda)
      End If

      If nTipoLegAgg = 1 Then
        dQtadispass = dQtadis
        dValdispass = dValdis
        GoTo esci
      End If

Fase2:
      If nOperaz = 0 Then
        nSegno = 1
      Else
        nSegno = -1
      End If

      ' fa un recordset
      If strPrevGrup = "F" Then
        ' cerca il taskid del padre
        lTaskidpadre = oCldDocu.GetTaskPadre(strDittaCorrente, lCommeca, lTaskid)
      End If
      If strPrevGrup = "N" Then
        dtrT = dttCorpo.Select("ec_riga <> " & dtrCorpo!ec_riga.ToString & _
                                " AND ec_commeca = " & lCommeca & _
                                " AND ec_pmtaskid = " & lTaskid, "")
      Else ' query con anche dentro il padre... (prevgrup='F')
        ' adesso query per aggiornare i fratelli..
        dtrT = dttCorpo.Select("ec_riga <> " & dtrCorpo!ec_riga.ToString & _
                                " AND ec_commeca = " & lCommeca & _
                                " AND ec_pmtaskid = " & lTaskid, "")
        'in vb6 era in join con tasks: qui non posso fare la join, per cui filtro i record sotto (vedi '***')
      End If
      If dtrT.Length = 0 Then
        GoTo esci
      End If
      ' ha trovato qualcosa : fa gli aggiornamenti del caso
      For i = 0 To dtrT.Length - 1

        If strPrevGrup <> "N" Then
          '***'
          If Not oCldDocu.GetTaskFromTaskIdETaskIdPadre(strDittaCorrente, lCommeca, lTaskid, lTaskidpadre) Then GoTo PASSA
        End If

        If NTSCInt(dtrT(i)!ec_pmtaskid) = lTaskid Or (NTSCInt(dtrT(i)!ec_pmtaskid) <> lTaskid And oCldDocu.LegGestCostPadreDao(strDittaCorrente, lCommeca, lTaskid) <> "2") Then 'stesso task o task fratello ma padre gestito a qta e valore
          dtrT(i)!xxo_pmqtarim = NTSCDec(dtrT(i)!xxo_pmqtares) - (nSegno * dQtadis) - NTSCDec(dtrT(i)!xxo_pmqtadisda)
          dtrT(i)!xxo_pmvalrim = NTSCDec(dtrT(i)!xxo_pmvalres) - (nSegno * dValdis) - NTSCDec(dtrT(i)!xxo_pmvaldisda)
          dtrT(i)!xxo_pmqtares = NTSCDec(dtrT(i)!xxo_pmqtares) - (nSegno * dQtadis)
          dtrT(i)!xxo_pmvalres = NTSCDec(dtrT(i)!xxo_pmvalres) - (nSegno * dValdis)
        Else ' solo valore, task fratello con padre gestito solo a valore
          dtrT(i)!xxo_pmvalrim = NTSCDec(dtrT(i)!xxo_pmvalres) - NTSCDec(dtrT(i)!xxo_pmvaldisda)
          dtrT(i)!xxo_pmvalres = NTSCDec(dtrT(i)!xxo_pmvalres) - (nSegno * dValdis)
        End If
        dtrT(i).AcceptChanges()
        AggResAltriRkCorpo = True
PASSA:
      Next
esci:

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      bInSalvarigaPM = False
    End Try
  End Function

  Public Overridable Function SettaSaldoRigaScarichiELavorazioni(ByVal dtrEc As DataRow) As Boolean
    Dim dtrT() As DataRow
    Dim i As Integer = 0
    Try
      dtrT = dttECIMP.Select("ec_prriga = " & dtrEc!ec_riga.ToString)
      'In VB6: 
      'strSQL = "UPDATE " & strMMtrasca & " SET mm_salcon = '" & griRighe.Columns(SALCON).Value & "',
      '                                         mm_npsalcon = '" & griRighe.Columns(SALCON).Value & "'" & _
      For i = 0 To dtrT.Length - 1
        dtrT(i)!ec_salcon = dtrEc!ec_salcon
        dtrT(i)!ec_npsalcon = dtrEc!ec_salcon
      Next
      'In MMLAVCE non fa il cambio se ci sono LAVCENT G
      'Controlla se ci sono dei LAVCENT G
      If dttLAVCENT.Select("lce_riga = " & dtrEc!ec_riga.ToString & " AND lce_gnumdoc > 0").Length = 0 Then
        dtrT = dttLAVCENT.Select("lce_riga = " & dtrEc!ec_riga.ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i)!lce_flevas = dtrEc!ec_salcon
        Next
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

  Public Overridable Function CalcolaValoreProvvRiga(ByRef dtrEC As DataRow) As Boolean
    Dim strOldValueFlProv1Mod As String = ""
    Dim strOldValueFlProv2Mod As String = ""
    Try
      If Not BGESTORDLIST Then
        strOldValueFlProv1Mod = NTSCStr(dtrEC!ec_flprov1mod)
        strOldValueFlProv2Mod = NTSCStr(dtrEC!ec_flprov2mod)
      End If

      If BORDINI Then
        If BGESTORDLIST = False Then
          If dtrEC!ec_ricimp.ToString = "S" Then
            dtrEC!ec_vprovv = ArrDbl(NTSCDec(dtrEC!ec_quant) * NTSCDec(dtrEC!ec_provv), oCldDocu.TrovaNdec(0))
            dtrEC!ec_vprovv2 = ArrDbl(NTSCDec(dtrEC!ec_quant) * NTSCDec(dtrEC!ec_provv2), oCldDocu.TrovaNdec(0))
          Else
            Dim dValoreRigaProvvTmp As Decimal = CalcolaValoreRigaPerProvv(dtrEC) / 100
            dtrEC!ec_vprovv = ArrDbl(dValoreRigaProvvTmp * NTSCDec(dtrEC!ec_provv), oCldDocu.TrovaNdec(0))
            dtrEC!ec_vprovv2 = ArrDbl(dValoreRigaProvvTmp * NTSCDec(dtrEC!ec_provv2), oCldDocu.TrovaNdec(0))
          End If
        End If
      Else
        If dtrEC!ec_ricimp.ToString = "S" Then
          dtrEC!ec_vprovv = ArrDbl(NTSCDec(dtrEC!ec_quant) * NTSCDec(dtrEC!ec_provv), oCldDocu.TrovaNdec(0))
          dtrEC!ec_vprovv2 = ArrDbl(NTSCDec(dtrEC!ec_quant) * NTSCDec(dtrEC!ec_provv2), oCldDocu.TrovaNdec(0))
        Else
          Dim dValoreRigaProvvTmp As Decimal = CalcolaValoreRigaPerProvv(dtrEC) / 100
          dtrEC!ec_vprovv = ArrDbl(dValoreRigaProvvTmp * NTSCDec(dtrEC!ec_provv), oCldDocu.TrovaNdec(0))
          dtrEC!ec_vprovv2 = ArrDbl(dValoreRigaProvvTmp * NTSCDec(dtrEC!ec_provv2), oCldDocu.TrovaNdec(0))
        End If
        dtrEC!ec_valstat = DeterminaValoreStatistico(dtrEc, 0)
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
      If Not BGESTORDLIST Then
        dtrEC!ec_flprov1mod = strOldValueFlProv1Mod
        dtrEC!ec_flprov2mod = strOldValueFlProv2Mod
      End If
    End Try
  End Function


  Public Overridable Function BeforeColUpdateCodart_TrovaArticolo(ByRef strIn As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return BeforeColUpdateCodart_TrovaArticolo(strIn, 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function BeforeColUpdateCodart_TrovaArticolo(ByRef strIn As String, ByRef nOrigine As Integer) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    '--- nOrigine:
    '----- 1 da Barcode
    '----- 2 da Codarfo
    '----- 3 da Articolo Lotto
    '----- 4 da EAN 128
    '----- 5 da Artest
    '----------------------------------------------------------------------------------------------------------------
    Dim strArtImport As String = ""
    Dim strTmp1 As String = ""
    Dim strTmp As String = ""
    Dim nFase1 As Integer = 0
    Dim strUnMis As String = ""
    Dim dQuant, dColli As Decimal

    'dato il codice articolo indicato in input, lo cerco nelle varie possibili anagrafiche per ritornare il vero cod articolo.
    strCodart = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strIn, nOrigine})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strIn = NTSCStr(oIn(0))
        nOrigine = NTSCInt(oIn(1))
        Return CBool(oOut)
      End If
      '----------------

      strTmp1 = strIn
      nOrigine = 0
      If GetMemDttArti(strIn).Rows.Count = 0 Or strTmp1.Trim = "" Then
        strIn = ""
        If bTableBarcodeEmpty = False And strTmp1.Trim <> "" Then
          '---------------------
          'potrebbe essere un codice a barre
          strTmp = CType(oCleComm, CLELBMENU).TrovaCodartDaBarcode(IIf(BORDINI, "BSORGSOR", "BSVEBOLL").ToString, strTmp1, strDittaCorrente, strUnMis, dQuant, dColli, nFase1, "")
          If strTmp <> "" Then
            strIn = strTmp.ToUpper
            GetMemDttArti(strIn)
            nFaseArticoloDaBarcode = nFase1
            strUnMisDaBarcode = strUnMis
            dColliDaBarcode = dColli
            dQuantDaBarcode = dQuant
            'If nFase1 <> 0 Then e.Row!ec_fase = nFase1  'non si può fare, perchè l'articolo non è ancora validato
          End If
        End If

        If bTableCodarfoEmpty = False And strTmp = "" And strTmp1.Trim <> "" Then
          '---------------------
          'potrebbe essere un codice articolo cliente/fornitore
          strTmp = CType(oCleComm, CLELBMENU).TrovaCodartDaCodarfo(strTmp1, NTSCInt(dttET.Rows(0)!et_conto), strDittaCorrente, "")
          If strTmp <> "" Then
            strIn = strTmp.ToUpper
            GetMemDttArti(strIn)
            nOrigine = 2
          End If
        End If

        If (strTmp = "") And (strTmp1.Trim <> "") Then
          '---------------------
          'potrebbe essere un codice articolo lotto
          If oCldDocu.RitornaArticoloLotto(strDittaCorrente, strTmp1, strTmp) = True Then
            If strTmp <> "" Then
              strIn = strTmp.ToUpper
              strLottoXDaAnalotti = strTmp1
              GetMemDttArti(strIn)
              nOrigine = 3
            End If
          End If
        End If

        '---------------------
        'se è stato inserito un EAN 128 di tipo SSCC restituisco eventualmente anche altre informazioni 
        'quali lotto, data produzione, data scadenza, ...
        'visto che non è possibile determinare il limitatore di fine sezione 10 (lotto) sull'EAN 128 (perchè è una serie di caratteri che non vengono presi dalla e.proposedvalue
        'per ora do la possibilità di ereditare la funzione in modo che ogni rive possa implementare la gestione che vuole
        If (strTmp = "") And (strTmp1.Trim <> "") Then
          If BeforeColUpdateCodart_TrovaArticoloEAN128(strTmp1, strTmp) Then
            If strTmp <> "" Then
              strIn = strTmp.ToUpper
              GetMemDttArti(strIn)
              nOrigine = 4
            End If
          End If
        End If

        '---------------------
        'verifico se l'articolo è tra quelli in catalogo da importare
        If strTmp = "" Then
          If RicercaImportDaArtest(strTmp1, strArtImport) Then
            strIn = strArtImport
            GetMemDttArti(strIn)
            nOrigine = 5
          Else
            '--------------------------------------------------------------------------------------------------------
            '--- Se sono stati strovati più articoli in ARTEST, è stata visualizzata la griglia di selezione
            '--- ed è stato premuto "Annulla", non deve dare il messaggio sotto
            '--------------------------------------------------------------------------------------------------------
            If bCaclSelArticoli = True Then Return False
            '--------------------------------------------------------------------------------------------------------
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222092812500, "Codice Articolo inserito non presente in archivio")))
            Return False
          End If
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

  Public Overridable Function BeforeColUpdateCodart_TrovaArticoloEAN128(ByVal strIn As String, ByRef strCodartOut As String) As Boolean
    'variabili che se settate vengono utilizzate nella AfterColUpdate_ec_codart per alimentare lotto, matricola, 
    'data prep. lotto, data scad. lotto
    'strEAN128Lotto = ""
    'strEAN128Matric = ""
    'strEAN128DtProd = ""
    'strEAN128DtScad = ""
    Dim dttDatiBarcode As DataTable = Nothing
    Try
      If CLN__STD.IsBis Then
        strEAN128Barcode = oApp.IS_BarcodeRead  'passato da SBC come stringa a parte oltre al testo pulito dei caratteri speciali
        oApp.IS_BarcodeRead = ""
      End If
      If Not CType(oCleComm, CLELBMENU).TrascodificaBarcodeEan128(strDittaCorrente, IIf(BORDINI, "BSORGSOR", "BSVEBOLL").ToString, strIn, strEAN128Barcode, dttDatiBarcode) Then Return False

      'è un EAN 128 (o potrebbe esserlo): continuo nella ricerca dei barcatori di campo
      strCodartOut = BeforeColUpdateCodart_TrovaArticoloEAN128_RitornaDato(dttDatiBarcode, "Articolo")
      nFaseArticoloDaBarcode = NTSCInt(BeforeColUpdateCodart_TrovaArticoloEAN128_RitornaDato(dttDatiBarcode, "Fase"))
      strUnMisDaBarcode = BeforeColUpdateCodart_TrovaArticoloEAN128_RitornaDato(dttDatiBarcode, "UnitaMisura")
      dColliDaBarcode = NTSCDec(BeforeColUpdateCodart_TrovaArticoloEAN128_RitornaDato(dttDatiBarcode, "Colli"))
      dQuantDaBarcode = NTSCDec(BeforeColUpdateCodart_TrovaArticoloEAN128_RitornaDato(dttDatiBarcode, "Quantita"))
      strEAN128Lotto = BeforeColUpdateCodart_TrovaArticoloEAN128_RitornaDato(dttDatiBarcode, "Lotto")
      strEAN128DtProd = BeforeColUpdateCodart_TrovaArticoloEAN128_RitornaDato(dttDatiBarcode, "DataProduzione")
      strEAN128DtScad = BeforeColUpdateCodart_TrovaArticoloEAN128_RitornaDato(dttDatiBarcode, "DataScadenza")
      strEAN128Matric = BeforeColUpdateCodart_TrovaArticoloEAN128_RitornaDato(dttDatiBarcode, "Matricola")
      '-------------------------------

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
  Public Overridable Function BeforeColUpdateCodart_TrovaArticoloEAN128_TrovaFineMarcatore(ByVal strEAN128() As String, ByVal nStart As Integer) As Integer
    Dim i As Integer = 0
    Try
      For i = nStart To strEAN128.Length - 1
        If strEAN128(i).ToUpper = "APPS" Or strEAN128(i).ToUpper = "MENU" Then
          Return i
        End If
      Next
      Return -1

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

  Public Overridable Function BeforeColUpdateCodart_TrovaArticoloEAN128_RitornaDato(ByVal dttDatiBarcode As DataTable, ByVal strTipo As String) As String
    Dim dtrDatoCorrente() As DataRow
    Try
      dtrDatoCorrente = dttDatiBarcode.Select("tipo = " & CStrSQL(strTipo))
      If dtrDatoCorrente.Length > 0 Then Return NTSCStr(dtrDatoCorrente(0)!valore)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return ""
  End Function

  Public Overridable Sub AssegnaContropartitaRiga(ByVal dtrRow As DataRow, ByVal lArControp As Integer, ByVal lAgControp As Integer)
    Try
      Dim lContropartita As Integer = lArControp + lAgControp
      Select Case strQuandoAgContropManca
        Case "0" ' Non deve fare nullo nel select, fa tutto dopo
        Case "1"
          'Verifica se la contropartita esiste, se non esiste propone la contropartita dell'articolo
          If GetTabcoveRow(lContropartita) Is Nothing Then
            Dim evnt As New NTSEventArgs(ThMsg.MSG_YESNO, oApp.Tr(Me, 130794562519139579, "La contropartita |" & lContropartita & "| non esiste, utilizzare quella dell'articolo? (contropartita |" & lArControp & "|)"))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = ThMsg.RETVALUE_YES AndAlso lArControp > 0 Then dtrRow!ec_controp = lArControp
            Return
          End If
        Case "2"
          'Verifica se la contropartita esiste, se non esiste assegna la contropartita dell'articolo
          If GetTabcoveRow(lContropartita) Is Nothing Then
            If lArControp > 0 Then dtrRow!ec_controp = lArControp
            Return
          End If
      End Select

      If lContropartita > 0 Then dtrRow!ec_controp = lContropartita
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







  Public Overridable Function CorpoTestPreSalva(ByRef dttCorpo As DataTable, ByVal nRow As Integer) As Boolean
    'UTILIZZATO SIA PER CORPO CHE PER CORPOIMP !!!!!!

    '-------------------------------------------------
    'eseguo i controlli necessari prima di fare il salvataggio della riga
    'Dim dtrTmp As DataRow() = dsShared.Tables("******").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
    Dim i As Integer = 0
    Dim dtrEC As DataRow
    Dim dtrT() As DataRow
    Dim dSomma As Decimal = 0
    Dim dttTmp As New DataTable
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Dim strFlkit As String = " "
    Dim dProvv As Decimal = 0
    Dim dProvv2 As Decimal = 0
    Dim strError As String = ""
    Dim strRetSubCommeca As String = " "
    Dim evnt As NTSEventArgs = Nothing
    Dim strMess10 As String = ""

    Dim bRiganuova As Boolean = False

    Dim dValore As Decimal = 0

    Try
      dtrEC = dttCorpo.Rows(nRow)
      If dtrEC.RowState = DataRowState.Added Then bRiganuova = True

      '--------------------------
      'impegni di produzione / scarichi a produzione: gli sconti sono sempre = 0
      'ordini di prod interni / carichi da prod interni: gli sconti sono sempre = 0
      Select Case NTSCStr(dtrEC!ec_tipork).ToUpper
        Case "U", "Y"
          dtrEC!ec_scont1 = 0
          dtrEC!ec_scont2 = 0
          dtrEC!ec_scont3 = 0
          dtrEC!ec_scont4 = 0
          dtrEC!ec_scont5 = 0
          dtrEC!ec_scont6 = 0
        Case "T", "H"
          If bTerzista = False Then
            dtrEC!ec_scont1 = 0
            dtrEC!ec_scont2 = 0
            dtrEC!ec_scont3 = 0
            dtrEC!ec_scont4 = 0
            dtrEC!ec_scont5 = 0
            dtrEC!ec_scont6 = 0
          End If
      End Select

      '--------------------------
      If BGESTORDLIST = False Then
        strFlkit = dtrEC!ec_flkit.ToString
        dProvv = NTSCDec(dtrEC!ec_provv)
        dProvv2 = NTSCDec(dtrEC!ec_provv2)
      End If

      If dtrEC!ec_codart.ToString.Trim = "" Then
        dtrEC!ec_codart = "D" 'altrimenti se indico un cod articolo sbagliato poi con il mouse clicco sul bottone a fianco di una textbox fuori dalla griglia per lanciare uno zoom entr ain un loop infinito!!!
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222089687500, "Il codice articolo è un campo obbligatorio: indicarlo prima di salvare la riga (per il momento verrà impostato l'articolo 'D')")))
        ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_codart"))   'occhio: non grVrighe
        Return False
      End If
      If NTSCInt(dtrEC!ec_magaz) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222089843750, "Il codice magazzino è un campo obbligatorio: indicarlo prima di salvare la riga (articolo |" & dtrEC!ec_codart.ToString & "|)")))
        ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_magaz"))   'occhio: non grVrighe
        Return False
      End If

      '--------------------------
      'articolo a gestiti a lotto: se non ho indicato il lotto, 
      'cerco di proporlo in base alle regole indicate in anagrafica articoli
      'solo documenti di acquisto/carico, no imp cli, gestione offerte, prop d'ordine, movimenti tra magazzini
      If BGESTORDLIST = False Then
        If Not CalcolaLottox(dtrEC) Then Return False

        If Not bDocEmesso AndAlso NTSCInt(dtrEC!ec_lotto) = 0 AndAlso BORDINI = False Then
          If Not ScaricaLottox_CheckLogistica(dtrEC) Then
            'Proposta del lotto se non siamo in gestione ordini
            Dim lTmp As Integer = 0
            Dim strTmp1 As String = ""
            PropostaLotto("BNVEBOLL", NTSCStr(dtrEC!ec_tipork), NTSCStr(dttET.Rows(0)!et_vistato), NTSCStr(dtrEC!ec_codart), _
                          NTSCInt(dtrEC!ec_fase), NTSCInt(dtrEC!ec_magaz), 0, lTmp, strTmp1)
            If lTmp <> 0 Then dtrEC!ec_lotto = lTmp 'lotto dall'ordine
            If strTmp1 <> "" Then dtrEC!xxo_lottox = strTmp1 'lotto da tabmaga (lotto generico)
          End If
        End If


      End If

      '--------------------------
      'test specifici per VEBOLL
      If BORDINI = False Then
        If NTSCInt(dtrEC!ec_causale) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128695891017500000, "La causale di magazzino è un campo obbligatorio: indicarla prima di salvare la riga  (articolo |" & dtrEC!ec_codart.ToString & "|)")))
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_causale"))   'occhio: non grVrighe
          Return False
        End If

        If NTSCInt(dtrEC!ec_causale2) <> 0 And NTSCInt(dtrEC!ec_magaz2) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128677702569218750, "La causale di magazzino inserita movimenta 2 magazzini; inserire il codice del secondo magazzino sulla riga. (articolo |" & dtrEC!ec_codart.ToString & "|)")))
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_causale2"))   'occhio: non grVrighe
          Return False
        End If

        If VisMsg() Then
          'Se articolo gestito a lotti obbligatorio lotto
          'se fattura proforma no 
          If Not bDisabilitaCheckLotti And Not (dttET.Rows(0)!et_proforma.ToString = "S" And dttET.Rows(0)!et_tipork.ToString = "A") Then
            If NTSCStr(dtrEC!xxo_geslotti) = "S" And NTSCInt(dtrEC!ec_lotto) = 0 Then
              If bRiganuova Then
                'non do il messaggio se lo scarico del lotto deve avvenire in automatico
                If ScaricaLottox_Check(dtrEC) = False Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128677705826250000, "Gestione lotti obbligatoria per questo articolo. (|" & dtrEC!ec_codart.ToString & "|)")))
                  'con la WSS  28027 la riga sotto era stata commentata, ma ora vedo che il problema non si presenta più e la funzionalità è stata chiesta dalla WSS 47490
                  ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.xxo_lottox"))   'occhio: non grVrighe
                  Return False
                End If
              Else
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129527761976806640, "Gestione lotti obbligatoria per questo articolo. (|" & dtrEC!ec_codart.ToString & "|)")))
                'con la WSS  28027 la riga sotto era stata commentata, ma ora vedo che il problema non si presenta più e la funzionalità è stata chiesta dalla WSS 47490
                ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.xxo_lottox"))   'occhio: non grVrighe
                Return False
              End If    'If bRiganuova Then
            End If
          End If

          'Se articolo gestito a ubicazione obbligatoria ubicazione
          'se fattura proforma no 
          If (Not bDisabilitaCheckUbicaz Or CBool(lModuliSupDittaDitt And bsModSupRME)) And Not (dttET.Rows(0)!et_proforma.ToString = "S" And dttET.Rows(0)!et_tipork.ToString = "A") Then
            If NTSCStr(dtrEC!xxo_gesubic) = "S" And NTSCStr(dtrEC!ec_ubicaz).Trim = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128677707096093750, "Gestione ubicazioni obbligatoria per questo articolo. (|" & dtrEC!ec_codart.ToString & "|)")))
              ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_ubicaz"))   'occhio: non grVrighe
              Return False
            Else
              If NTSCStr(dtrEC!ec_ubicaz) = "" Then dtrEC!ec_ubicaz = ""
            End If
          Else
            If NTSCStr(dtrEC!ec_ubicaz) = "" Then dtrEC!ec_ubicaz = ""
          End If

          'Se articolo gestito a commessa
          If Not bDisabilitaCheckCommesse Then
            If NTSCStr(dtrEC!xxo_gescomm) = "S" And NTSCInt(dtrEC!ec_commeca) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036816845000, "Gestione commessa obbligatoria per questo articolo. (|" & dtrEC!ec_codart.ToString & "|)")))
              ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_commeca"))   'occhio: non grVrighe
              Return False
            End If
          End If
        End If    'If VisMSG Then

        If dttET.Rows(0)!et_tipork.ToString = "W" Then
          If dtrEC!ec_nprflevas.ToString = "S" Then
            dtrEC!ec_nprvalore = 0
          Else
            dtrEC!ec_nprvalore = ArrDbl(ArrDbl(NTSCDec(dtrEC!ec_prezzo) * (NTSCDec(dtrEC!ec_quant) - NTSCDec(dtrEC!ec_nprquaeva)) / NTSCDec(dtrEC!ec_perqta) * (100 - NTSCDec(dtrEC!ec_scont1)) / 100 * (100 - NTSCDec(dtrEC!ec_scont2)) / 100 * (100 - NTSCDec(dtrEC!ec_scont3)) / 100 * (100 - NTSCDec(dtrEC!ec_scont4)) / 100 * (100 - NTSCDec(dtrEC!ec_scont5)) / 100 * (100 - NTSCDec(dtrEC!ec_scont6)) / 100, oCldDocu.TrovaNdec(0)) * (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, oCldDocu.TrovaNdec(0))
          End If
        End If    'If dttET.Rows(0)!et_tipork.ToString = "W" Then

        '------------------------------
        'Controlla che:
        'se presente il riferimento, sia a MOVORD che A MOVPRB, se mm_salcon = 'S'
        '(saldo ordine) non possa essere mm_prsalcon = 'C' (in conto della Nota di prelievo)
        If NTSCInt(dtrEC!ec_ornum) <> 0 And NTSCInt(dtrEC!ec_npnum) <> 0 Then
          If dtrEC!ec_salcon.ToString = "S" And dtrEC!ec_npsalcon.ToString = "C" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128680193073316204, "Non è consentita una evasione a saldo riga ordine con evasione in conto su Nota di prelievo. (articolo |" & dtrEC!ec_codart.ToString & "|)")))
            ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_salcon"))   'occhio: non grVrighe
            Return False
          End If
        End If

        '------------------------------
        'Controllo riferimenti conto/lavoro
        If NTSCInt(dtrEC!ec_ccnum) = 0 Then
          If NTSCStr(dtrEC!ec_cctipo) <> "" Or NTSCInt(dtrEC!ec_ccanno) <> 0 Or NTSCStr(dtrEC!ec_ccserie) <> "" Or NTSCInt(dtrEC!ec_ccriga) <> 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128677714082812500, "Quando il riferimento al numero del documento di carico in Conto lavoro è uguale a 0, il tipo e la serie devono essere vuoti e la riga deve essere a 0. (articolo |" & dtrEC!ec_codart.ToString & "|)")))
            Return False
          End If
        Else
          If NTSCStr(dtrEC!ec_cctipo) = "" Or NTSCInt(dtrEC!ec_ccanno) = 0 Or NTSCStr(dtrEC!ec_ccserie) = "" Or NTSCInt(dtrEC!ec_ccriga) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128677715905937500, "Quando il riferimento al numero del documento di carico in Conto lavoro è diverso da 0, il tipo di documento, la serie e la riga devono essere indicati obbligatoriamente. (articolo |" & dtrEC!ec_codart.ToString & "|)")))
            Return False
          End If
        End If

        'Controllo se la causale di riga prevede l'inserimento del centro sulla riga
        If VisMsg() Then
          If (CBool(lModuliSupDittaDitt And bsModSupCAE) = False) And (CBool(lModuliDittaDitt And bsModCI) = True) Then
            If Not CorpoTestPreSalva_CheckCA(dtrEC) Then Return False
          End If
        End If    'If VisMSG Then

      End If    'If BORDINI = False Then

      '----------------------
      'se il barcode non è associato all'articolo (perchè dopo aver digitato il barcode ho cambiato l'articolo (lo cancello)
      If dtrEC.Table.Columns.Contains("ec_matric") Then
        If NTSCStr(dtrEC!ec_matric).Trim <> "" And NTSCStr(dtrEC!ec_codart).Trim <> "" And bDisabilitaDecodificaBarcode = False Then
          oCldDocu.ValCodiceDb(NTSCStr(dtrEC!ec_matric), strDittaCorrente, "BARCODE", "S", "", dttTmp)
          If dttTmp.Rows.Count = 0 Then
            dtrEC!ec_matric = " "
          Else
            If NTSCStr(dttTmp.Rows(0)!bc_codart).ToUpper <> NTSCStr(dtrEC!ec_codart).ToUpper Then
              dtrEC!ec_matric = " "
            End If
          End If
        End If
      End If

      If NTSCInt(dtrEC!ec_codiva) = 0 And NTSCDec(dtrEC!ec_prezzo) <> 0 And NTSCDec(dtrEC!ec_quant) <> 0 Then
        If VisMsg() Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222090000000, "Il codice IVA è un campo obbligatorio: indicarlo prima di salvare la riga (articolo |" & dtrEC!ec_codart.ToString & "|)")))
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_codiva"))   'occhio: non grVrighe
          Return False
        End If
      End If
      If NTSCInt(dtrEC!ec_controp) = 0 And NTSCDec(dtrEC!ec_prezzo) <> 0 And NTSCDec(dtrEC!ec_quant) <> 0 Then
        If VisMsg() Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222090156250, "Il codice Contropartita vendite/acquisti è un campo obbligatorio: indicarlo prima di salvare la riga (articolo |" & dtrEC!ec_codart.ToString & "|)")))
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_controp"))   'occhio: non grVrighe
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se attivo modulo CADC e prezzo e/o quantità sono diversi da zero, 
      '--- la contropartita di riga è obbligatoria
      '--------------------------------------------------------------------------------------------------------------
      If CBool(lModuliSupDittaDitt And bsModSupCAE) Then
        If NTSCInt(dtrEC!ec_controp) = 0 And (NTSCDec(dtrEC!ec_prezzo) <> 0 Or NTSCDec(dtrEC!ec_quant) <> 0) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130106780936850283, "Attenzione!" & vbCrLf & _
            "In presenza di Contabilità Analitica Duplice Contabile" & vbCrLf & _
            "il codice Contropartita vendite/acquisti è un campo obbligatorio." & vbCrLf & _
            "Indicarlo prima di salvare la riga (articolo |" & dtrEC!ec_codart.ToString & "|)")))
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_controp"))   'occhio: non grVrighe
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case dtrEC!ec_stasino.ToString
        Case "N", "B", "D"
          If Not (NTSCDec(dtrEC!ec_quant) = 0 Or NTSCDec(dtrEC!ec_prezzo) = 0) Then
            If Not (dttET.Rows(0)!et_tipork.ToString = "B" And dtrEC!ec_stasino.ToString = "D") Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222090312500, "Una riga da non stampare deve contenere prezzo o quantità pari a 0.")))
              ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_quant"))   'occhio: non grVrighe
              Return False
            End If
          End If
        Case "O", "M", "P" ', "X"
          If bConsentiScontiSuOmaggi Then
            If dProvv <> 0 OrElse dProvv2 <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130652745210719890, "Una riga di omaggi/sconto merce deve contenere provvigioni pari a 0. (articolo |" & dtrEC!ec_codart.ToString & "|)")))
              Return False
            End If
          Else
            If NTSCDec(dtrEC!ec_scont1) <> 0 OrElse NTSCDec(dtrEC!ec_scont2) <> 0 OrElse NTSCDec(dtrEC!ec_scont3) <> 0 OrElse _
               NTSCDec(dtrEC!ec_scont4) <> 0 OrElse NTSCDec(dtrEC!ec_scont5) <> 0 OrElse NTSCDec(dtrEC!ec_scont6) <> 0 OrElse _
               dProvv <> 0 OrElse dProvv2 <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222090468750, "Una riga di omaggi/sconto merce deve contenere sconti e provvigioni pari a 0. (articolo |" & dtrEC!ec_codart.ToString & "|)")))
              Return False
            End If
          End If
      End Select

      If BGESTORDLIST = False Then
        If BORDINI Then
          '--------------------------------
          'Controllo assegnato
          If NTSCDec(dtrEC!xxo_qtadaass) <> 0 Then
            If Not CheckAssegnazione(dtrEC) Then Return False
            If VisMsg() And dtrEC!xxo_geslotti.ToString = "S" And NTSCInt(dtrEC!ec_lotto) = 0 And dtrEC!ec_tipork.ToString <> "Y" And dtrEC!ec_tipork.ToString <> "U" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222091250000, "Gestione lotti obbligatoria per l'articolo |" & dtrEC!ec_codart.ToString & "|.")))
              ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.xxo_lottox"))   'occhio: non grVrighe
              Return False
            End If
          End If
        End If

        'Se articolo gestito a commessa
        If BGESTORDLIST = False Then
          If BORDINI Then
            '--------------------------------
            'genera automaticamente il numero di commessa
            If (bGenNumCommecaAutR = True) And (bGenNumCommecaAutR_AllaFine = False) And _
               (dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#") And _
               (NTSCInt(dtrEC!ec_commeca) = 0) Then
              GetMemDttArti(dtrEC!ec_codart.ToString)
              If dtrEC!xxo_gescomm.ToString <> "N" Then
                dtrEC!ec_commeca = CType(oCleComm, CLELBMENU).GeneraNumCommeca(strDittaCorrente, _
                                    dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                    dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), NTSCInt(dtrEC!ec_riga), _
                                    NTSCInt(dttET.Rows(0)!et_tipobf), NTSCInt(dttET.Rows(0)!et_conto), NTSCStr(dtrEC!ec_codart), _
                                    NTSCStr(dtrEC!ec_descr), NTSCStr(dtrEC!ec_desint), NTSCStr(dtrEC!ec_note), _
                                    NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, strRetSubCommeca, "O", "", strError)

                dtrEC!ec_subcommeca = strRetSubCommeca
                If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
              End If
            End If

            If Not bDisabilitaCheckCommesse Then
              If VisMsg() AndAlso NTSCInt(dtrEC!ec_commeca) = 0 AndAlso NTSCStr(dtrEC!xxo_gescomm) <> "N" AndAlso _
                 dttET.Rows(0)!et_tipork.ToString <> "Q" AndAlso dttET.Rows(0)!et_tipork.ToString <> "$" AndAlso dttET.Rows(0)!et_tipork.ToString <> "V" Then
                If (bGenNumCommecaAutR = True) And (bGenNumCommecaAutR_AllaFine = True) And _
                   ((dttET.Rows(0)!et_tipork.ToString = "R") Or (dttET.Rows(0)!et_tipork.ToString = "#")) Then
                Else
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128717284677968750, "Gestione commessa obbligatoria per questo articolo. (|" & dtrEC!ec_codart.ToString & "|)")))
                  ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_commeca"))   'occhio: non grVrighe
                  Return False
                End If
              End If
            End If
          End If    'If BORDINI Then
        End If    'If BGESTORDLIST = False Then

        If strFlkit = "A" Or strFlkit = "T" Then
          If CDbl(dtrEC!ec_prezzo) <> 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222091406250, "Sulle righe con articoli di tipo 'Kit analitico' o 'Componente sintetico', il Prezzo deve essere a zero. (articolo |" & dtrEC!ec_codart.ToString & "|)")))
            ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_prezzo"))   'occhio: non grVrighe
            Return False
          End If
        End If

        '--------------------------------
        If NTSCDec(dtrEC!ec_prezzo) = 0 And dtrEC!ec_codart.ToString <> "D" And dtrEC!ec_stasino.ToString <> "N" And _
           dtrEC!ec_stasino.ToString <> "B" And dtrEC!ec_stasino.ToString <> "D" And dtrEC!ec_tipork.ToString <> "T" And _
           dtrEC!ec_tipork.ToString <> "U" And dtrEC!ec_tipork.ToString <> "H" And dtrEC!ec_tipork.ToString <> "Y" And strFlkit <> "A" And strFlkit <> "T" Then
          If bDisabilitaMsgPrezzoZero = False AndAlso VisMsg() AndAlso BGESTORDLIST = False AndAlso bInCalcoloPromozioni = False Then
            Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128551619211190000, "Confermi Prezzo uguale a zero per l'articolo |" & dtrEC!ec_codart.ToString & "|?"))
            ThrowRemoteEvent(msg)
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
              'ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "edet_conto")) 
              'ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "edet_codpaga")) 
              If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_prezvalc"))   'occhio: non grVrighe
              Else
                ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_prezzo"))   'occhio: non grVrighe
              End If
              Return False
            End If
          End If
        End If

        If dtrEC!ec_umprz.ToString = "S" Then
          If NTSCDec(dtrEC!ec_colli) = 0 And dtrEC!ec_codart.ToString <> "D" And dtrEC!ec_stasino.ToString <> "N" And dtrEC!ec_stasino.ToString <> "B" And dtrEC!ec_stasino.ToString <> "D" Then
            If bDisabilitaMsgQuantZero = False And VisMsg() And dtrEC!ec_tipork.ToString <> "Y" And dtrEC!ec_tipork.ToString <> "U" AndAlso bInCalcoloPromozioni = False Then
              Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128551624225654000, "Confermi colli uguali a zero per l'articolo |" & dtrEC!ec_codart.ToString & "|?"))
              ThrowRemoteEvent(msg)
              If msg.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
                ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_colli"))   'occhio: non grVrighe
                Return False
              End If
            End If
          End If
        Else
          If NTSCDec(dtrEC!ec_quant) = 0 And dtrEC!ec_codart.ToString <> "D" And dtrEC!ec_stasino.ToString <> "N" And dtrEC!ec_stasino.ToString <> "B" And dtrEC!ec_stasino.ToString <> "D" Then
            If bDisabilitaMsgQuantZero = False And VisMsg() And dtrEC!ec_tipork.ToString <> "Y" And dtrEC!ec_tipork.ToString <> "U" AndAlso bInCalcoloPromozioni = False Then
              Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128551619192470000, "Confermi quantità uguale a zero per l'articolo |" & dtrEC!ec_codart.ToString & "|?"))
              ThrowRemoteEvent(msg)
              If msg.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
                ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_quant"))   'occhio: non grVrighe
                Return False
              End If
            End If
          End If
        End If    'If dtrEC!ec_umprz.ToString = "S" Then

      Else
        'specifico per ordlist
        If dtrEC!ec_umprz.ToString = "S" Then
          If NTSCDec(dtrEC!ec_colli) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128619660662500000, "Il campo colli per l'articolo |" & dtrEC!ec_codart.ToString & "| deve contenere un valore diverso da 0")))
            ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_colli"))   'occhio: non grVrighe
            Return False
          End If
        Else
          If NTSCDec(dtrEC!ec_quant) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128619661040000000, "Il campo quantità per l'articolo |" & dtrEC!ec_codart.ToString & "| deve contenere un valore diverso da 0")))
            ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.SETFOCUS, "grrighe.ec_quant"))   'occhio: non grVrighe
            Return False
          End If
        End If
      End If    'If BGESTORDLIST = False Then

      '--------------------------------
      'niente sconti se prezzo netto
      If NTSCStr(dtrEC!ec_flprznet) = "S" And _
        (NTSCDec(dtrEC!ec_scont1) <> 0 Or NTSCDec(dtrEC!ec_scont2) <> 0 Or _
         NTSCDec(dtrEC!ec_scont3) <> 0 Or NTSCDec(dtrEC!ec_scont4) <> 0 Or _
         NTSCDec(dtrEC!ec_scont5) <> 0 Or NTSCDec(dtrEC!ec_scont6) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128517126976816000, "Una riga a prezzo netto deve avere tutti gli sconti pari a 0.")))
        Return False
      End If

      '----------------------------------
      'Se attivo il modulo Taglie e colori e l'articolo ha ar_codtagl <> 0
      'controlla che la quantità sia = alla sommatoria delle quantità di MOTRANSTC
      If bModTCO = True Then

        '---------------------
        'impegni collegati: determino l'indice della taglia TC (il numero da 1 a 24 ...)
        If dttCorpo.TableName = "CORPOIMP" Then
          bOk = False
          If NTSCInt(dtrHT!xxo_codtagl) <> 0 Then 'TAGLIA DEL PADRE
            oCldDocu.ValCodiceDb(NTSCInt(dtrHT!xxo_codtagl).ToString, strDittaCorrente, "TABTAGL", "N", "", dttTmp)
            For i = 1 To 24
              If NTSCStr(dtrEC!ec_tctaglia).ToUpper.Trim = NTSCStr(dttTmp.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c))).ToUpper.Trim And NTSCStr(dtrEC!ec_tctaglia).ToUpper.Trim <> "" Then
                dtrEC!ec_tcindtagl = i
                bOk = True
                Exit For
              End If
            Next
          Else
            If NTSCStr(dtrEC!ec_tctaglia) <> " " Then dtrEC!ec_tctaglia = " "
            If NTSCInt(dtrEC!ec_tcindtagl) <> 0 Then dtrEC!ec_tcindtagl = 0
            bOk = True
          End If
          If Not bOk Then
            For i = 1 To 24
              If NTSCStr(dttTmp.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c))).Trim <> "" Then
                strTmp += vbCrLf & NTSCStr(dttTmp.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c))).Trim
              End If
            Next
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128587725418281251, _
            "Codice taglia padre inserito non valido. Indicare un codice taglia appartenente alla tabella taglie dell'articolo '|" & _
            dtrHT!ec_codart.ToString & "|'. Taglie valide: |" & strTmp & "|")))
            Return False
          End If
          If bGestioneAbbinamentiTaglie Then
            bOk = False
            If NTSCInt(dtrEC!xxo_codtagl) <> 0 Then 'TAGLIA DEL FIGLIO
              oCldDocu.ValCodiceDb(NTSCInt(dtrEC!xxo_codtagl).ToString, strDittaCorrente, "TABTAGL", "N", "", dttTmp)
              For i = 1 To 24
                If NTSCStr(dtrEC!xxo_tctagliaf).ToUpper.Trim = NTSCStr(dttTmp.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c))).ToUpper.Trim And NTSCStr(dtrEC!xxo_tctagliaf).ToUpper.Trim <> "" Then
                  dtrEC!xxo_tcindtaglf = i
                  bOk = True
                  Exit For
                End If
              Next
            Else
              If NTSCStr(dtrEC!xxo_tctagliaf) <> " " Then dtrEC!xxo_tctagliaf = " "
              If NTSCInt(dtrEC!xxo_tcindtaglf) <> 0 Then dtrEC!xxo_tcindtaglf = 0
              bOk = True
            End If
            If Not bOk Then
              For i = 1 To 24
                If NTSCStr(dttTmp.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c))).Trim <> "" Then
                  strTmp += vbCrLf & NTSCStr(dttTmp.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c))).Trim
                End If
              Next
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128587725418281250, _
              "Codice taglia figlio inserito non valido. Indicare un codice taglia appartenente alla tabella taglie dell'articolo '|" & _
              dtrEC!ec_codart.ToString & "|'. Taglie valide: |" & strTmp & "|")))
              Return False
            End If
          End If
          'impegni collegati: correggo la quantità in CORPOIMPTC come da CORPOIMP
          dtrT = dttECIMPTC.Select("ec_riga = " & NTSCInt(dtrEC!ec_riga).ToString)
          If dtrT.Length > 0 Then
            For i = 1 To 24
              If dtrEC.Table.Columns.Contains("xxo_tcindtaglf") Then
                If dtrEC!xxo_tcindtaglf.Equals(System.DBNull.Value) Then
                  'Riga nuova
                  If i = NTSCInt(dtrEC!ec_tcindtagl) Then
                    dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dtrEC!ec_quant)
                  Else
                    dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = 0
                  End If
                Else
                  'Riga già inserita
                  If i = NTSCInt(dtrEC!xxo_tcindtaglf) Then
                    dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dtrEC!ec_quant)
                  Else
                    dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = 0
                  End If
                End If
              Else
                If i = NTSCInt(dtrEC!ec_tcindtagl) Then
                  dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = NTSCDec(dtrEC!ec_quant)
                Else
                  dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)) = 0
                End If
              End If
            Next
          End If
        End If    'If dttCorpo.TableName = "CORPOIMP" Then

        If NTSCStr(dtrEC!ec_codart).Trim <> "" Then
          If NTSCInt(dtrEC!xxo_codtagl) <> 0 Then
            If dttCorpo.TableName = "CORPO" Then
              dtrT = dttECTC.Select("ec_riga = " & NTSCInt(dtrEC!ec_riga).ToString)
              If dtrT.Length > 0 Then
                dSomma = NTSCDec(dtrT(0)!ec_quant01) + NTSCDec(dtrT(0)!ec_quant02) + NTSCDec(dtrT(0)!ec_quant03) + _
                        NTSCDec(dtrT(0)!ec_quant04) + NTSCDec(dtrT(0)!ec_quant05) + NTSCDec(dtrT(0)!ec_quant06) + _
                        NTSCDec(dtrT(0)!ec_quant07) + NTSCDec(dtrT(0)!ec_quant08) + NTSCDec(dtrT(0)!ec_quant09) + _
                        NTSCDec(dtrT(0)!ec_quant10) + NTSCDec(dtrT(0)!ec_quant11) + NTSCDec(dtrT(0)!ec_quant12) + _
                        NTSCDec(dtrT(0)!ec_quant13) + NTSCDec(dtrT(0)!ec_quant14) + NTSCDec(dtrT(0)!ec_quant15) + _
                        NTSCDec(dtrT(0)!ec_quant16) + NTSCDec(dtrT(0)!ec_quant17) + NTSCDec(dtrT(0)!ec_quant18) + _
                        NTSCDec(dtrT(0)!ec_quant19) + NTSCDec(dtrT(0)!ec_quant20) + NTSCDec(dtrT(0)!ec_quant21) + _
                        NTSCDec(dtrT(0)!ec_quant22) + NTSCDec(dtrT(0)!ec_quant23) + NTSCDec(dtrT(0)!ec_quant24)
              End If
              If (NTSCDec(dtrEC!ec_quant) <> dSomma) And (dttET.Rows(0)!et_tipork.ToString = "T") Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128551666704958000, _
                          "La quantità indicata (|" & NTSCDec(dtrEC!ec_quant).ToString(oApp.FormatQta) & "|) sulla riga |" & NTSCInt(dtrEC!ec_riga).ToString & "| per l'articolo |" & NTSCStr(dtrEC!ec_codart).Trim & "|" & vbCrLf & _
                          "non può essere diversa dalla sommatoria delle quantità inserite in 'Dettaglio quantità' " & _
                          "(|" & dSomma.ToString(oApp.FormatQta) & "|).")))
                Return False
              End If
            End If
          End If    'If NTSCInt(dttArti.Rows(0)!ar_codtagl) <> 0 Then
        End If    'If NTSCStr(dttEC.Rows(nRow)!ec_codart).trim <> "" Then
      End If    ' If bModTCO = True Then

      '--------------------------------
      'Controllo movimentazione a lotti (su apposita opzione di registro)
      If bMovimQtaLotti And dttCorpo.TableName = "CORPO" Then
        GetMemDttArti(dtrEC!ec_codart.ToString)
        If dttArti.Rows(0)!ar_polriord.ToString = "F" And NTSCDec(dttArti.Rows(0)!ar_minord) <> 0 Then
          'Controlla la quantità
          If (NTSCDec(dtrEC!ec_quant) Mod NTSCDec(dttArti.Rows(0)!ar_minord)) <> 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036477631000, "La quantità |" & NTSCDec(dtrEC!ec_quant).ToString(oApp.FormatQta) & _
                            "| (in unità di misura principale) indicata per l'articolo |" & UCase(dtrEC!ec_codart.ToString) & _
                            "| non è un multiplo del lotto standard |" & NTSCDec(dttArti.Rows(0)!ar_minord).ToString(oApp.FormatQta) & _
                            "| indicato nella sua anagrafica. quantità non ammessa.")))
            Return False
          End If
        End If
      End If

      '--------------------------------
      If BloccaEvasione(dtrEC) Then Return False

      '--------------------------------
      'test su esistenza, vendita sottocosto
      If BGESTORDLIST = False Then
        If (bTestSottoCostoModificaRigheEsist AndAlso (dtrEC.RowState = DataRowState.Added OrElse dtrEC.RowState = DataRowState.Modified)) OrElse _
           (Not bTestSottoCostoModificaRigheEsist AndAlso (bNew OrElse (Not bNew AndAlso dtrEC.RowState = DataRowState.Added))) Then
          'sia per ordini che per doc di magaz, eseguo il test esistenza sui nuovi documenti 
          'o sulle nuove righe di quelli in modifica

          'occhio: su documenti in modifica il test esistenza viene eseguito SOLO fino a quando la nuova riga non viene salvata (si passa ad un altra riga)
          If Not CorpoCheckRiga(3, dtrEC) Then Return False
        End If
      End If

      '----------------------------------
      ' controlli per modulo PM
      If bModPM And BGESTORDLIST = False Then
        If NTSCInt(dtrEC!ec_pmtaskid) <> 0 Then
          ' controlla che i dati siano coerenti ...
          If NTSCDec(dtrEC!xxo_pmvalrim) = 0 And dBasecost <> 0 And NTSCStr(dtrEC!ec_pmsalcon) = "C" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443314478876954, "Non è coerente un valore rimanente di zero con una evasione task in conto." & vbCrLf & " Correggere.")))
            Return False
          End If
          If NTSCDec(dtrEC!xxo_pmvalrim) <> 0 And (NTSCStr(dtrEC!ec_pmsalcon) = "S" Or NTSCStr(dtrEC!ec_pmsalcon) = "V") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443316389814454, "Non è coerente un valore rimanente diverso da zero con una evasione task a saldo per valori." & vbCrLf & " Correggere.")))
            Return False
          End If
          If NTSCDec(dtrEC!xxo_pmqtarim) <> 0 And (NTSCStr(dtrEC!ec_pmsalcon) = "S" Or (NTSCStr(dtrEC!ec_pmsalcon) = "Q" And oCldDocu.LegGestCostPadreDao(strDittaCorrente, NTSCInt(dtrEC!ec_commeca), NTSCInt(dtrEC!ec_pmtaskid)) = "2")) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443319396005860, "Non è coerente una qta rimanente diversa da  zero con una evasione task a saldo per quantità." & vbCrLf & " Correggere.")))
            Return False
          End If
          If (NTSCDec(dtrEC!xxo_pmvalrim) = 0 And dBasecost <> 0 And NTSCDec(dtrEC!xxo_pmqtarim) <> 0) And Not (NTSCStr(dtrEC!ec_pmsalcon) = "V") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443319461132813, "Non è coerente un valore rimanente di zero con una qta rimanente diversa da zero." & vbCrLf & " Correggere.")))
            Return False
          End If
          If (NTSCStr(dtrEC!ec_pmsalcon) = "Q" Or NTSCStr(dtrEC!ec_pmsalcon) = "V") And NTSCStr(dtrEC!xxo_prevgrup) = "N" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443319554882813, "Non è coerente un valore SaldoQuantità (Q) o SaldoBudget (B) su flag saldo task su task non Figlio di task preventivazione di gruppo! " & vbCrLf & " Correggere.")))
            Return False
          End If
          If (NTSCStr(dtrEC!ec_pmsalcon) = "Q" And NTSCDec(dtrEC!xxo_pmvalrim) = 0 And dBasecost <> 0 Or NTSCStr(dtrEC!ec_pmsalcon) = "V" And NTSCDec(dtrEC!xxo_pmqtarim) = 0 And NTSCStr(dtrEC!xxo_gestcost) = "3") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443319625927735, "Non è coerente un valore SaldoQuantità (Q) o SaldoBudget (B) con i campi importo rimanente o qta rimanente = 0 ! " & vbCrLf & " Correggere.")))
            Return False
          End If
          If (NTSCStr(dtrEC!ec_pmsalcon) = "V" And NTSCStr(dtrEC!xxo_gestcost) = "2") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443319681923829, "Non è coerente un valore  SaldoBudget (B) con task gestito a solo costo ! " & vbCrLf & " Correggere.")))
            Return False
          End If
          If NTSCDec(dtrEC!xxo_pmvalrim) < 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443319775126954, "Non è ammesso un valore rimanente minore di zero ." & vbCrLf & " Correggere.")))
            Return False
          End If
          '-----------------------------------------------------------------------------------------
          '--- Controlla che la sottocommessa, se indicata, sia valida
          '-----------------------------------------------------------------------------------------
          If (NTSCInt(dtrEC!ec_commeca) <> 0) And (Trim(NTSCStr(dtrEC!ec_subcommeca)) <> "") And (bDisabilitaControlliSubcomm = False) Then
            If Not oCldDocu.CheckSubcomm(strDittaCorrente, NTSCInt(dtrEC!ec_commeca), NTSCStr(dtrEC!ec_subcommeca)) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443328580849610, "Attenzione!" & vbCrLf & _
                                                  "Sottocommessa inesistente per la commessa indicata.")))
              Return False
            End If
          End If
          '-----------------------------------------------------------------------------------------
          If ((bPMControllaBaseCostMG And Not BORDINI) Or (bPMControllaBaseCostOR And BORDINI)) Then

            If BORDINI Then
              dValore = NTSCDec(dtrEC!ec_valoremm)
            Else
              dValore = NTSCDec(dtrEC!ec_valore)
            End If

            If ArrDbl(dValore + NTSCDec(dtrEC!xxo_pmvalrim), oCldDocu.TrovaNdec(0)) > ArrDbl(NTSCDec(dtrEC!xxo_pmvalres), oCldDocu.TrovaNdec(0)) Then
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 129443330097304688, "Superamento costo a budget: impossibile salvare." & vbCrLf & _
                                                                        "Si vuole autorizzare uno sforamento a budget?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
                Return False
              Else
                'CHIAMATA AL CHILD MODALE PER CONSENTIRE LO SFORAMENTO
                'CDbl(griRighe.Columns(VALOREMM).Value) + (CDbl(griRighe.Columns(PMVALRIM).Value)) - CDbl(griRighe.Columns(PMVALRES).Value))
                evnt = New NTSEventArgs("DOAUTS___:", NTSCInt(dtrEC!ec_commeca).ToString & ";" & _
                                                      NTSCInt(dtrEC!ec_pmtaskid).ToString & ";" & _
                                                      dValore + NTSCDec(dtrEC!xxo_pmvalrim) - NTSCDec(dtrEC!xxo_pmvalres) & ";" & _
                                                      NTSCStr(dtrEC!xxo_gestcost))

                ThrowRemoteEvent(evnt)
                If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_OK Then
                  ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 129418289802451171, "Per ricalcolare il budget a disposizione, azzerare e digitare nuovamente il Task-id nella apposita colonna in griglia.")))
                End If
                Return False
              End If
            End If
          End If
          ' controlla se a saldo, e se F , che non ci siano fratelli ancora non chiusi..
          If NTSCStr(dtrEC!xxo_prevgrup) = "F" And NTSCStr(dtrEC!ec_pmsalcon) = "S" Then
            If Not (oCldDocu.ControllaSalconFratelliDao(strDittaCorrente, "TTPROESEC", lIIttproesebappo, "TTTASKS", lIItttasks, NTSCInt(dtrEC!ec_commeca), NTSCInt(dtrEC!ec_pmtaskid), False, strMess10, Nothing)) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443332407197266, " Ci sono altri task facenti capo allo stesso budget di costo che non sono chiusi ! ") & strMess10))
              Return False
            End If
          End If
        End If ' pmtaskid <>0
      End If    'If bModPM Then

      If BGESTORDLIST = False Then dtrEC!ec_ultagg = DateTime.Now

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
  Public Overridable Function CorpoTestPreSalva_CheckCA(ByRef dtrEC As DataRow) As Boolean
    Dim dttTmp As New DataTable
    Try
      'con la nuova analitica i test vengono fatti tutti in fase di salvataggio documento
      If NTSCStr(dtrEC!xxo_testci) <> " " And NTSCInt(dtrEC!ec_contocontr) <> 0 And NTSCStr(dtrEC!ec_codart) <> "D" Then 'Nessun test CI
        If NTSCInt(dtrEC!ec_codcena) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129174621067343750, "Test CA: sulla riga |" & dtrEC!ec_riga.ToString & "|, la causale prevede l'inserimento del centro CA. (articolo |" & dtrEC!ec_codart.ToString & "|)")))
          Return False
        End If
        dttTmp.Clear()

        oCldDocu.ValCodiceDb(NTSCStr(dtrEC!ec_causale), strDittaCorrente, "TABCAUM", "N", , dttTmp)
        If NTSCStr(dttTmp.Rows(0)!tb_testci) = "E" Or NTSCStr(dttTmp.Rows(0)!tb_testci) = "B" Then 'Solo se DARE CG o AVERE CG
          If NTSCInt(dtrEC!ec_commeca) = 0 Then
            oCldDocu.ValCodiceDb(NTSCInt(dtrEC!ec_contocontr).ToString, strDittaCorrente, "ANAGCA", "N", "", dttTmp)
            If dttTmp.Rows.Count > 0 Then
              If NTSCStr(dttTmp.Rows(0)!ac_flci) = "2" Or NTSCStr(dttTmp.Rows(0)!ac_flci) = "4" Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129767246390495313, "Test CA: sulla riga |" & dtrEC!ec_riga.ToString & "|, causale prevede l'inserimento della commessa. (articolo |" & dtrEC!ec_codart.ToString & "|)")))
                Return False
              End If
            End If
          End If
        End If    'If NTSCInt(dtrEC!ec_commeca) = 0 And NTSCStr(dtr

        If NTSCStr(dtrEC!ec_codcfam).Trim = "" Then
          oCldDocu.ValCodiceDb(NTSCInt(dtrEC!ec_contocontr).ToString, strDittaCorrente, "ANAGCA", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            If NTSCStr(dttTmp.Rows(0)!ac_flci) = "3" Or NTSCStr(dttTmp.Rows(0)!ac_flci) = "4" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129864731385120748, "Test CA: sulla riga |" & dtrEC!ec_riga.ToString & "|, causale prevede l'inserimento della linea/famiglia. (articolo |" & dtrEC!ec_codart.ToString & "|)")))
              Return False
            End If
          End If
        End If    'If NTSCInt(dtrEC!ec_commeca) = 0 And NTSCStr(dtr
      End If    'If NTSCStr(dtrEC!tb_testci) <> " " Then

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
    End Try
  End Function

  Public Overridable Function CorpoTestPreCancella(ByVal nRow As Integer) As Boolean
    Try
      If nRow < 0 Then Return False

      If BORDINI Then
        If BGESTORDLIST = False Then
          If NTSCDec(dttEC.Rows(nRow)!ec_quaeva) <> 0 Or NTSCDec(dttEC.Rows(nRow)!ec_quapre) <> 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036871757000, "Impossibile eliminare la riga del documento in quanto possiede quantità evasa o prenotata diversa da 0.")))
            Return False
          End If

          '--------------------------------
          'se  un carico da produzione
          If dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "T" Then
            'Controlla che le righe di impegni collegati non siano evase/prenotate
            If dttECIMP.Select("ec_rigaor = " & NTSCInt(dttEC.Rows(nRow)!ec_riga).ToString & " AND (ec_quaeva > 0 OR ec_quapre > 0)").Length > 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128551422039966000, "Impossibile eliminare la riga del documento in quanto possiede righe di impegni collegati con quantità evase o prenotate maggiori di 0.")))
              Return False
            End If

            'Controlla che non ci siano 'avanzamenti di lav.' associati
            If dttATTIVIT.Select("at_riga = " & NTSCInt(dttEC.Rows(nRow)!ec_riga).ToString & " AND (at_tempattees <> 0 OR at_tempesees <> 0)").Length > 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128551423003110000, "Impossibile eliminare la riga del documento in quanto possiede una o più attività parzialmente o totalmente evasa (in avanzamenti/carichi di prod.)).")))
              Return False
            End If
          End If    'If dttET.Rows(0)!et_tipork.ToString = "H" Then
        End If    'If bGestOrdlist = False Then
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

  Public Overridable Function CheckAssegnazione(ByVal dtrTmp As DataRow) As Boolean
    Try
      'ereditata da beorgsor
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

  Public Overridable Function BloccaEvasione(ByVal dtrTmp As DataRow) As Boolean
    Try
      'ereditata da beorgsor
      Return False
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

  Public Overridable Sub Leggidisponibilita(ByVal strCodart As String, ByVal nMagaz As Integer, _
                              ByVal nFase As Integer, ByVal lCommeca As Integer, ByRef ddisponibilita As Decimal, _
                              ByRef ddisponibilitaNetta As Decimal, ByRef dUltCost As Decimal, _
                              Optional ByVal strGescomm As String = "?")
    Try
      Leggidisponibilita(strCodart, nMagaz, nFase, lCommeca, ddisponibilita, ddisponibilitaNetta, dUltCost, 0, 0, 0, strGescomm)

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
  Public Overridable Sub Leggidisponibilita(ByVal strCodart As String, ByVal nMagaz As Integer, _
                            ByVal nFase As Integer, ByVal lCommeca As Integer, ByRef dDisponibilita As Decimal, _
                            ByRef dDisponibilitaNetta As Decimal, ByRef dUltCost As Decimal, _
                            ByRef dOrdinato As Decimal, ByRef dImpegnato As Decimal, ByRef dPrenotato As Decimal, _
                            Optional ByVal strGescomm As String = "?")
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodart, nMagaz, nFase, lCommeca, dDisponibilita, dDisponibilitaNetta, dUltCost, dOrdinato, dImpegnato, dPrenotato, strGescomm})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dDisponibilita = NTSCDec(oIn(4))
        dDisponibilitaNetta = NTSCDec(oIn(5))
        dUltCost = NTSCDec(oIn(6))
        dOrdinato = NTSCDec(oIn(7))
        dImpegnato = NTSCDec(oIn(8))
        dPrenotato = NTSCDec(oIn(9))
        Return
      End If
      '----------------

      CType(oCleComm, CLELBMENU).LeggiDisponibilita(strDittaCorrente, strCodart, nMagaz, nFase, lCommeca, _
                                  dDisponibilita, dDisponibilitaNetta, dUltCost, bRilevaDisponibPerCommessa, _
                                  strDispMultiMag, strTestScominCome, dOrdinato, dImpegnato, dPrenotato, strGescomm)

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

  '----------------------

  Public Overridable Function AggImpegniMateriali(ByVal dRapportoQta As Decimal, ByRef dtrEc As DataRow) As Boolean
    '---------------------------------
    'al cambio della quantità (o flag 'evaso') sulla riga di produzione chiedo se cambiarla sulle righe degli impegni/lavorazioni
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim dQuaeva As Decimal = 0
    Dim nTmpRrfence As Integer = 0
    Dim dTmpFcorrlt As Decimal = 0
    Dim nRrfenceFase As Integer = 0
    Dim lRRFencePadre As Integer = 0
    Dim dFcorrltFase As Decimal = 0
    Dim dFCorrltPadre As Decimal = 0
    Dim bRRFdaFasi As Boolean = False
    Dim dttTmp As New DataTable
    Dim dQtaOrig As Decimal = 0
    Try
      dtrT = dttECIMP.Select("ec_rigaor = " & dtrEc!ec_riga.ToString)
      For i = 0 To dtrT.Length - 1
        If dRapportoQta = 0 Then
          dtrT(i)!ec_colli = 0
          dtrT(i)!ec_quant = 0
          dtrT(i)!ec_valore = 0
          If BGESTORDLIST = False Then
            dtrT(i)!ec_valoremm = 0
            dtrT(i)!ec_flevas = dtrEc!ec_flevas.ToString
          End If
        Else
          dQtaOrig = NTSCDec(dtrT(i)!ec_quant)
          dtrT(i)!ec_colli = ArrDbl(NTSCDec(dtrT(i)!ec_colli) / dRapportoQta, oApp.NDecQta)
          'If NTSCDec(dtrT(i)!ec_colli) = 0 Or bNoModifQuantSuColli Then
          '  'NON SERVE: GIA' FATTO NELLA BEFORECOLUPDATE DI EC_COLLI
          '  'non è vero: se i colli erano = 0 non viene scatenata la beforecolupdate dei colli e la qta non viene aggiornata
          '  dtrT(i)!ec_quant = ArrDbl(dQtaOrig / dRapportoQta, oApp.NDecQta)
          'End If
          'va sempre ricalcolata la QTA, diversamente se cambio la QTA sulla riga prodotta, 
          'negli scarichi i colli possono venire arrotondati 
          'e la quantità subirebbe un ulteriore arrotondamento, divero dalla proporzione iniziale QTA DA PROD PRIMA / QTA DA PROD DOPO * QTA DA CONSUMARE!
          dtrT(i)!ec_quant = ArrDbl(dQtaOrig / dRapportoQta, oApp.NDecQta)

          If NTSCInt(dtrT(i)!ec_perqta) = 0 Then dtrT(i)!ec_perqta = 1
          'Suppone che gli impegni non abbiano sconti
          If BGESTORDLIST = False Then
            dtrT(i)!ec_valoremm = ArrDbl(NTSCDec(dtrT(i)!ec_prezzo) * NTSCDec(dtrT(i)!ec_quant) / NTSCInt(dtrT(i)!ec_perqta), oCldDocu.TrovaNdec(0))
            If NTSCStr(dtrT(i)!ec_flevas) = "S" Then
              dtrT(i)!ec_valore = 0
            Else
              dQuaeva = NTSCDec(dtrT(i)!ec_quaeva)
              dtrT(i)!ec_valore = ArrDbl(NTSCDec(dtrT(i)!ec_prezzo) * (NTSCDec(dtrT(i)!ec_quant) - dQuaeva) / NTSCInt(dtrT(i)!ec_perqta), oCldDocu.TrovaNdec(0))
            End If
          Else
            dtrT(i)!ec_valore = ArrDbl(NTSCDec(dtrT(i)!ec_prezzo) * (NTSCDec(dtrT(i)!ec_quant)) / NTSCInt(dtrT(i)!ec_perqta), oCldDocu.TrovaNdec(0))
          End If
        End If    'If dRapportoQta = 0 Then
        If BGESTORDLIST = False Then dtrT(i)!ec_flevas = dtrEc!ec_flevas.ToString


        '------------------------------------------
        'Calcola la nuova data consegna del figlio
        '------------------------------------------
        '-!!!!!!!!!!!!!!!ATTENZIONE!!!!!!!!!!!!!!!-
        ' VOLUTAMENTE NON TIENE CONTO DI MD_RITARDO di MOVDIS nel calcolo della data consegna!!
        ' Altrimenti si doveva rifare la TRATTADB, cosa da evitare quando possibile!!!
        '------------------------------------------
        If bAggDatConsFigliSuQtaPadre Then
          If NTSCInt(dtrEc!ec_fase) > 0 Then
            'Rileva RRFence & FCorrlt relativi alla fase corrente del padre
            bRRFdaFasi = False
            oCldDocu.ValCodiceDb(NTSCStr(dtrEc!ec_fase), strDittaCorrente, "ARTFASI", "N", "", dttTmp, NTSCStr(dtrEc!ec_codart))
            If dttTmp.Rows.Count > 0 Then
              nRrfenceFase = NTSCInt(dttTmp.Rows(0)!af_rrfence)
              lRRFencePadre = nRrfenceFase
              dFcorrltFase = NTSCDec(dttTmp.Rows(0)!af_fcorrlt)
              dFCorrltPadre = dFcorrltFase
              bRRFdaFasi = True
            End If

            nTmpRrfence = NTSCInt(IIf(bRRFdaFasi, nRrfenceFase, NTSCDec(GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_rrfence)))
            dTmpFcorrlt = NTSCDec(IIf(bRRFdaFasi, dFcorrltFase, NTSCDec(GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_fcorrlt)))
            If dTmpFcorrlt > 0 Then
              dtrT(i)!ec_datcons = CType(oCleComm, CLELBMENU).DateAddEx(strDittaCorrente, "d", ((nTmpRrfence + Int(dTmpFcorrlt * NTSCDec(dtrEc!ec_quant))) * -1), _
                                   NTSCDate(dtrEc!ec_datcons), 0) ' VEDI COMMENTO SOPRA PRIMA DI MODIFICARE
            End If
          Else
            If NTSCDec(GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_fcorrlt) > 0 Then
              dtrT(i)!ec_datcons = CType(oCleComm, CLELBMENU).DateAddEx(strDittaCorrente, "d", _
                        ((NTSCDec(GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_rrfence) + _
                        Int(NTSCDec(GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_fcorrlt) * NTSCDec(dtrT(i)!ec_quant))) * -1), _
                        NTSCDate(dtrEc!ec_datcons), 0) ' VEDI COMMENTO SOPRA PRIMA DI MODIFICARE
            End If
          End If
        End If
      Next

      dttECIMP.AcceptChanges()

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
  Public Overridable Function AggCommessaImpegni(ByRef dtrEc As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try

      dtrT = dttECIMP.Select("ec_rigaor = " & dtrEc!ec_riga.ToString)
      For i = 0 To dtrT.Length - 1
        dtrT(i)!ec_commeca = dtrEc!ec_commeca
      Next
      dttECIMP.AcceptChanges()

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

  Public Overridable Function AggImpegniLavoraz(ByVal dRapportoQta As Decimal, ByRef dtrEc As DataRow) As Boolean
    '---------------------------------
    'al cambio della quantità (o flag 'evaso') sulla riga di produzione chiedo se cambiarla sulle righe degli impegni/lavorazioni
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim nTmpRrfence As Integer = 0
    Dim dTmpFcorrlt As Decimal = 0
    Dim nRrfenceFase As Integer = 0
    Dim lRRFencePadre As Integer = 0
    Dim dFcorrltFase As Decimal = 0
    Dim dFCorrltPadre As Decimal = 0
    Dim bRRFdaFasi As Boolean = False
    Dim dttTmp As New DataTable
    Try
      If dRapportoQta = 0 Then
        dtrT = dttATTIVIT.Select("at_riga = " & dtrEc!ec_riga.ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i)!at_tempesepr = 0
          dtrT(i)!at_tempattpr = 0
          dtrT(i)!at_qtapr = 0
          If BGESTORDLIST = False Then dtrT(i)!at_flevas = dtrEc!ec_flevas.ToString
        Next
        dttATTIVIT.AcceptChanges()
        dtrT = dttASSRIS.Select("as_riga = " & dtrEc!ec_riga.ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i)!as_valore = 0
          dtrT(i)!as_valorev = 0
          dtrT(i)!as_valmo = 0
        Next
        dttASSRIS.AcceptChanges()

      Else
        dtrT = dttATTIVIT.Select("at_riga = " & dtrEc!ec_riga.ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i)!at_tempesepr = ArrDbl(NTSCDec(dtrT(i)!at_tempesepr) / dRapportoQta, 4)
          dtrT(i)!at_qtapr = NTSCDec(dtrEc!ec_quant)
          If BGESTORDLIST = False Then dtrT(i)!at_flevas = dtrEc!ec_flevas.ToString
        Next
        dttATTIVIT.AcceptChanges()

        'Mette a posto i costi totali in ASSRIS
        AggAssRis(dtrEc)

        '------------------------------------------
        'Calcola la nuova data consegna del figlio
        '------------------------------------------
        For i = 0 To dtrT.Length - 1
          If bAggDatConsFigliSuQtaPadre Then
            If NTSCInt(dtrEc!ec_fase) > 0 Then
              'Rileva RRFence & FCorrlt relativi alla fase corrente del padre
              bRRFdaFasi = False
              oCldDocu.ValCodiceDb(NTSCStr(dtrEc!ec_fase), strDittaCorrente, "ARTFASI", "N", "", dttTmp, NTSCStr(dtrEc!ec_codart))
              If dttTmp.Rows.Count > 0 Then
                nRrfenceFase = NTSCInt(dttTmp.Rows(0)!af_rrfence)
                lRRFencePadre = nRrfenceFase
                dFcorrltFase = NTSCDec(dttTmp.Rows(0)!af_fcorrlt)
                dFCorrltPadre = dFcorrltFase
                bRRFdaFasi = True
              End If

              nTmpRrfence = NTSCInt(IIf(bRRFdaFasi, nRrfenceFase, NTSCDec(GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_rrfence)))
              dTmpFcorrlt = NTSCDec(IIf(bRRFdaFasi, dFcorrltFase, NTSCDec(GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_fcorrlt)))
              If dTmpFcorrlt > 0 Then
                dtrT(i)!at_dtminima = CType(oCleComm, CLELBMENU).DateAddEx(strDittaCorrente, "d", ((nTmpRrfence + Int(dTmpFcorrlt * NTSCDec(dtrT(i)!at_qtapr))) * -1), _
                                     NTSCDate(dtrEc!ec_datcons), 0)
              End If
            Else
              If NTSCDec(GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_fcorrlt) > 0 Then
                dtrT(i)!at_dtminima = CType(oCleComm, CLELBMENU).DateAddEx(strDittaCorrente, "d", _
                          ((NTSCDec(GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_rrfence) + _
                          Int(NTSCDec(GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_fcorrlt) * NTSCDec(dtrT(i)!at_qtapr))) * -1), _
                          NTSCDate(dtrEc!ec_datcons), 0)
              End If
            End If
          End If
        Next
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
  Public Overridable Function AggImpegniDatcons(ByVal lDiffDatcons As Integer, ByRef dtrEc As DataRow) As Boolean
    '----------------------------
    'al cambio della data di consegna del materiale da produrre chiedo se cambiare la data di consegna su impegni/lavorazioni
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128552269021760000, _
                          "Visto che è stata cambiata la data consegna dell'articolo di questa " & _
                          "riga di ORDINE DI PRODUZIONE (differenza |" & lDiffDatcons.ToString & "| giorni), " & _
                          "cambiare le date di consegna su tutte le righe di IMPEGNO DI PRODUZIONE " & _
                          "associate a questa riga?"))
      ThrowRemoteEvent(msg)
      If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
        'Materiali
        dtrT = dttECIMP.Select("ec_rigaor = " & dtrEc!ec_riga.ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i)!ec_datcons = DateAdd("d", NTSCDec(lDiffDatcons), NTSCDate(dtrT(i)!ec_datcons))
        Next
        dttECIMP.AcceptChanges()

        'Lavorazioni
        dtrT = dttATTIVIT.Select("at_riga = " & dtrEc!ec_riga.ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i)!at_dtminima = DateAdd("d", NTSCDec(lDiffDatcons), NTSCDate(dtrT(i)!at_dtminima))
          dtrT(i)!at_datcons = DateAdd("d", NTSCDec(lDiffDatcons), NTSCDate(dtrT(i)!at_datcons))
        Next
        dttATTIVIT.AcceptChanges()
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
  Public Overridable Function AggAssRis(ByRef dtrEc As DataRow) As Boolean
    '---------------------------
    'Metto a posto i costi totali in ASSRIS
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim i As Integer = 0
    Dim l As Integer = 0
    Try

      dtrT = dttATTIVIT.Select("at_riga = " & dtrEc!ec_riga.ToString)

      For i = 0 To dtrT.Length - 1
        dtrT1 = dttASSRIS.Select("as_riga = " & dtrT(i)!at_riga.ToString)
        For l = 0 To dtrT.Length - 1
          dtrT1(l)!as_valore = ArrDbl((NTSCDec(dtrT1(l)!as_cmacora) * NTSCDec(dtrT(i)!at_tempesepr)) + (NTSCDec(dtrT1(l)!as_cmacoratt) * NTSCDec(dtrT(i)!at_tempattpr)), oCldDocu.TrovaNdec(0))
          dtrT1(l)!as_valmo = ArrDbl((NTSCDec(dtrT1(l)!as_pagaora) * NTSCDec(dtrT(i)!at_tempesepr)) + (NTSCDec(dtrT1(l)!as_pagaoratt) * NTSCDec(dtrT(i)!at_tempattpr)), oCldDocu.TrovaNdec(0))
          dtrT1(l)!as_valorev = 0
        Next
      Next
      dttASSRIS.AcceptChanges()

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

  Public Overridable Function AggScarichiMateriali(ByVal dRapportoQta As Decimal, ByRef dtrEc As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim bCancRecord As Boolean = False
    Dim dColli As Decimal = 0
    Dim lQuant As Decimal = 0
    Dim lTotQuant As Decimal = 0
    Dim lTotQuantNew As Decimal = 0
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY
    Dim strCodart As String = ""
    Dim strErr As String = ""
    Dim dQuant As Decimal = 0

    Try

      If dRapportoQta = 0 Then Return True

      dtrT = dttECIMP.Select("ec_prriga = " & dtrEc!ec_riga.ToString, IIf(bAggScarichiMateriali, "ec_codart, ec_riga DESC", "").ToString)
      For i = 0 To dtrT.Length - 1
        If dRapportoQta = 0 Then
          dtrT(i)!ec_colli = 0
          dtrT(i)!ec_quant = 0
          dtrT(i)!ec_valore = 0
          dtrT(i)!ec_valorev = 0
        Else
          lTotQuant = NTSCDec(dtrT(i)!ec_quant)

          If bAggScarichiMateriali = True Then
            lTotQuant = 0
            dttGr.Clear()
            oDttgr.NTSGroupBy(dttECIMP, dttGr, "Sum(ec_quant) As SOMMA", "ec_prriga = " & dtrEc!ec_riga.ToString & " AND ec_codart = " & CStrSQL(dtrT(i)!ec_codart.ToString), "")
            lTotQuant = NTSCDec(dttGr.Rows(0)!SOMMA)
            If strCodart <> dtrT(i)!ec_codart.ToString Then
              lTotQuantNew = ArrDbl(NTSCDec(dttGr.Rows(0)!SOMMA) / dRapportoQta, 3)
              strCodart = dtrT(i)!ec_codart.ToString
              bCancRecord = False
            End If
            dttGr.Clear()
          End If
          'se negli scarichi sono presenti diverse righe con lo stesso articolo, invece di adeguare la quantità su tutte le righe cerco
          'prima di cancellare le righe fino ala raggiungimento della nuova quantità
          If lTotQuant <> NTSCDec(dtrT(i)!ec_quant) Or bCancRecord = True Then
            If lTotQuant - NTSCDec(dtrT(i)!ec_quant) >= lTotQuantNew Then
              dtrT(i).Delete()
              bCancRecord = True
            Else
              lQuant = NTSCDec(dtrT(i)!ec_quant)
              dQuant = NTSCDec(dtrT(i)!ec_quant) - (lTotQuant - lTotQuantNew)
              CType(oCleComm, CLELBMENU).ConvQuantUM(strDittaCorrente, dtrT(i)!ec_codart.ToString, _
                                              dtrT(i)!ec_ump.ToString, dQuant, _
                                              NTSCDec(dtrT(i)!ec_misura1), NTSCDec(dtrT(i)!ec_misura2), _
                                              NTSCDec(dtrT(i)!ec_misura3), dtrT(i)!ec_unmis.ToString, dColli, strErr, 3) 'ArrDbl((!ec_colli * !ec_quant) / lQuant, 3)
              If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
              dtrT(i)!ec_colli = dColli
              dtrT(i)!ec_quant = dQuant
              If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                dtrT(i)!ec_valorev = ArrDbl(NTSCDec(dtrT(i)!ec_prezvalc) * NTSCDec(dtrT(i)!ec_quant) / NTSCDec(dtrT(i)!ec_perqta), oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
              End If
              dtrT(i)!ec_valore = ArrDbl(NTSCDec(dtrT(i)!ec_prezzo) * NTSCDec(dtrT(i)!ec_quant) / NTSCDec(dtrT(i)!ec_perqta), oCldDocu.TrovaNdec(0))
            End If
          Else
            lQuant = NTSCDec(dtrT(i)!ec_quant)
            dtrT(i)!ec_colli = ArrDbl(NTSCDec(dtrT(i)!ec_colli) / dRapportoQta, 3)
            dtrT(i)!ec_quant = ArrDbl(lQuant / dRapportoQta, 3)
            'dtrT(i)!ec_quant = ArrDbl(NTSCDec(dtrT(i)!ec_quant) / dRapportoQta, 3)   'non serve: viene già ricalcolata al  cambio dei colli nella riga sopra
            If NTSCInt(dtrT(i)!ec_perqta) = 0 Then
              dtrT(i)!ec_perqta = 1
            End If
            '---------------------------
            'Suppone che gli scarichi non abbiano sconti
            If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
              dtrT(i)!ec_valorev = ArrDbl(NTSCDec(dtrT(i)!ec_prezvalc) * NTSCDec(dtrT(i)!ec_quant) / NTSCDec(dtrT(i)!ec_perqta), oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
            End If
            dtrT(i)!ec_valore = ArrDbl(NTSCDec(dtrT(i)!ec_prezzo) * NTSCDec(dtrT(i)!ec_quant) / NTSCDec(dtrT(i)!ec_perqta), oCldDocu.TrovaNdec(0))
            '---------------------------
            'mantengo il flag di saldo ordine uguale a quello della riga di carico, visto che cambiando i colli potrebbe essere pasato da stato S a C mentre sul carico volgio mantenere l'ordine chiuso ...
            dtrT(i)!ec_salcon = dtrEc!ec_salcon.ToString
          End If
        End If
      Next
      dttECIMP.AcceptChanges()

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
  Public Overridable Function AggScarichiLavoraz(ByVal dRapportoQta As Decimal, ByRef dtrEc As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0

    Try
      If dRapportoQta = 0 Then Return True
      dtrT = dttLAVCENT.Select("lce_riga = " & dtrEc!ec_riga.ToString)
      For i = 0 To dtrT.Length - 1
        'dtrT(i)!lce_tempese = ArrDbl(NTSCDec(dtrT(i)!lce_tempese) / dRapportoQta, 4)   'viene già fatto in automatico al cambio di lce_qtaes nella riga sotto
        dtrT(i)!lce_qtaes = ArrDbl(NTSCDec(dtrT(i)!lce_qtaes) / dRapportoQta, 3)
        dtrT(i)!lce_qtascart = ArrDbl(NTSCDec(dtrT(i)!lce_qtascart) / dRapportoQta, 3)
        '---------------------------
        'mantengo il flag di saldo ordine uguale a quello della riga di carico, visto che cambiando i colli potrebbe essere pasato da stato S a C mentre sul carico volgio mantenere l'ordine chiuso ...
        dtrT(i)!lce_flevas = dtrEc!ec_salcon.ToString
        If bTerzista = True Then
        Else
          dtrT(i)!lce_valore = ArrDbl((NTSCDec(dtrT(i)!lce_tempese) * NTSCDec(dtrT(i)!lce_cmacora)) + (NTSCDec(dtrT(i)!lce_tempatte) * NTSCDec(dtrT(i)!lce_cmacoratt)), oCldDocu.TrovaNdec(0))
          dtrT(i)!lce_valmo = ArrDbl((NTSCDec(dtrT(i)!lce_tempese) * NTSCDec(dtrT(i)!lce_pagaora)) + (NTSCDec(dtrT(i)!lce_tempatte) * NTSCDec(dtrT(i)!lce_pagaoratt)), oCldDocu.TrovaNdec(0))
          dtrT(i)!lce_valorev = 0
        End If
      Next
      dttLAVCENT.AcceptChanges()

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
  Public Overridable Function AggCommessaScarichi(ByRef dtrEc As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try

      dtrT = dttECIMP.Select("ec_prriga = " & dtrEc!ec_riga.ToString)
      For i = 0 To dtrT.Length - 1
        dtrT(i)!ec_commeca = dtrEc!ec_commeca
      Next
      dttECIMP.AcceptChanges()

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

  '----------------------

  Public Overridable Function ScriviRigheDaDiba(ByRef dtrEC As DataRow, ByVal strCodart As String, _
                                                  ByVal nFase As Integer, ByVal dQuantit As Decimal, _
                                                  ByVal lCodClie As Integer, ByVal lCommeca As Integer, _
                                                  ByVal strSubCommeca As String, ByVal lCodCena As Integer, _
                                                  ByVal dtDatconsRiga As Date, ByVal strFamProd As String, _
                                                  ByVal dPerQta As Decimal, ByVal strUmprz As String, _
                                                  ByVal strUnmis As String, ByVal strUmp As String, _
                                                  ByVal dColli As Decimal, ByVal bDaDuplicaOrdine As Boolean) As Boolean
    'in gsor ScriviRigheYDaDiba
    Dim strDatval As String = ""
    Dim bFasi As Boolean = False
    Dim dQtaTotale As Decimal = 0
    Dim i As Integer = 0
    Dim dttDestagl As New DataTable
    Dim strDescrTagliaTCP As String = ""
    Dim nIndiceTagliaTCP As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dQta As Decimal = 0
    Dim lRigaFigli As Integer = 0
    Dim strCodvar1 As String = ""
    Dim nMagClav As Integer = 0
    Dim bAltruiPressoTerzi As Boolean = False

    Try
      'Chiede la data di validità distinta base
      strDatval = NTSCDate(dttET.Rows(0)!et_datdoc.ToString).ToShortDateString
      If bNoDatValDistinta = False Then
        Dim evnt As New NTSEventArgs("DataValDB.:", "")
        ThrowRemoteEvent(evnt)
        strDatval = evnt.RetValue.Trim
        'Premuto annulla
        If strDatval = "" Then Return True
      End If

      '----------------------------
      'Legge se si tratta di un articolo a fasi
      bFasi = False
      If bModLEX Then
        If GetMemDttArti(strCodart).Rows(0)!ar_gesfasi.ToString = "S" Then bFasi = True
      End If
      strCodvar1 = NTSCStr(GetMemDttArti(strCodart).Rows(0)!ar_codvar1)

      '----------------------------
      If BORDINI = False Then

        '--------------------------------
        'Se l'opzione di registro BSVEBOLL\OPZIONI\NoAttribRifCLav è attiva,
        'azzera il parametro in ingresso
        If bNoAttribRifCLav = True Then
          nMagClav = 0
        Else
          nMagClav = CheckScaricoLavorazione(bAltruiPressoTerzi)
        End If

        '--------------------------------
        'Se siamo su un carico di produzione, l'articolo è a fasi e il magazzino di scarico è di merce altrui (ossia trattasi di conto lavoro attivo)
        'annulla la gestione dei riferimenti al conto/lavoro (campi mm_cl*) tanto non sono gestiti e hanno un comportamento forviante
        If Not bAttribRifCLavAttSuScarProdArtFasi Then
          If NTSCStr(dttET.Rows(0)!et_tipork) = "T" And bFasi And bAltruiPressoTerzi Then nMagClav = 0
        End If
      End If

      '-----------------------------
      'Trova l'ultimo numero di riga usato negli 'Impegni collegati' MOTRAIMP
      lRigaFigli = 0
      dtrT = dttECIMP.Select("", "ec_riga DESC")
      If dtrT.Length > 0 Then lRigaFigli = NTSCInt(dtrT(0)!ec_riga)

      '-----------------------------
      'se articolo Taglie e colori 
      If NTSCInt(dtrEC!xxo_codtagl) <> 0 Then
        dQtaTotale = dQuantit
        nFasi = 0
        'Articolo a Taglie e Colori (esplodere ogni taglia per la quale sia stata indicata una q.tà <> 0)
        oCldDocu.ValCodiceDb(NTSCInt(dtrEC!xxo_codtagl).ToString, strDittaCorrente, "TABTAGL", "N", "", dttDestagl)
        dtrT = dttECTC.Select("ec_riga = " & dtrEC!ec_riga.ToString)  'dettaglio taglie collegate alla riga su cui sono
        For i = 1 To 24
          If NTSCDec(dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c))) <> 0 Then
            nFasi = nFasi + 1
            dQta = NTSCDec(dtrT(0)("ec_quant" & i.ToString.PadLeft(2, "0"c)))
            strDescrTagliaTCP = NTSCStr(dttDestagl.Rows(0)("tb_dest" & i.ToString.PadLeft(2, "0"c))).ToUpper.Trim
            nIndiceTagliaTCP = i

            If Not TrattaDbDoc(dttET.Rows(0), dtrEC, bFasi, NTSCDate(strDatval), lRigaFigli, strDescrTagliaTCP, _
                                strDescrTagliaTCP, nIndiceTagliaTCP, nIndiceTagliaTCP, strCodvar1, strCodvar1, _
                                dQtaTotale, nMagClav) Then
              Return False
            End If

          End If
        Next
      Else
        'Articolo NON a Taglie e Colori
        'parte vecchia (non è cambiata)...
        'Chiama l'elaborazione principale ricorsiva
        If Not TrattaDbDoc(dttET.Rows(0), dtrEC, bFasi, NTSCDate(strDatval), lRigaFigli, "", "", 0, 0, "", "", 0, nMagClav) Then
          Return False
        End If

      End If    'If NTSCInt(dtrEC!xxo_codtagl) <> 0 Then

      '-------------------------
      'Valorizza la riga padre
      'devo rifare la valorizzazione perchè in uscita dal cod articolo il cod lavorazione era = 0 ed è stato preso 
      'il prezzo generico. ora devo impostare il prezzo specifico per lavorazione
      If bTerzista Then
        bInEsplodiDiba = True
        SettaCondCommerciali(True, False, False, dtrEC, 0, 0, False)
        bInEsplodiDiba = False
      End If

      '-------------------------
      If Not bDaDuplicaOrdine Then
        ValorizzaProduzione(dtrEC)
        SettaValoriRiga(dtrEC)
        If dtrEC.RowState <> DataRowState.Unchanged Then
          If bModPM And NTSCInt(dtrEC!ec_pmtaskid) > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128587700421250000, "ATTENZIONE:" & vbCrLf & _
                   "Prima del salvataggio della riga si procede alla rivalorizzazione del 'Carico da Produzione'." & vbCrLf & _
                   "A causa di ciò verranno ricalcolati anche i valori dei campi relativi al Project Management.")))
          End If
          SistemaPMSalconDaQta(dtrEC)

          'è sicuramente l'ultima riga inserita nel corpo del doc
          RecordSalva(dttEC.Rows.Count - 1, False, Nothing)
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

  Public Overridable Function TrattaDbDoc(ByRef dtrEt As DataRow, ByRef dtrEc As DataRow, ByVal bFasi As Boolean, ByVal dtDatval As Date, _
                                          ByVal lRigaFigli As Integer, ByVal strDescrTagliaRoot As String, _
                                          ByVal strDescrTagliaPadr As String, ByVal nIndiceTagliaRoot As Integer, _
                                          ByVal nIndiceTagliaPadr As Integer, ByVal strColoreRoot As String, _
                                          ByVal strColorePadr As String, ByVal dQtaTotale As Decimal, _
                                          ByVal nMagClav1 As Integer) As Boolean
    'TrattaDbDoc unica sia per articoli a fasi che non
    Dim dttImp As New DataTable
    Dim dttLav As New DataTable
    Dim strError As String = ""
    Dim dPrezzo As Decimal = 0
    Dim dtrT As DataRow = Nothing       'riga contenente i figli della distinta base presi da TrattaDbDoc_TrovaFiglieLav
    Dim nMagimp As Integer = 0
    Dim dtrRow() As DataRow

    'per attivit/assris
    Dim nNewCodcent As Integer = 0
    Dim bSostCent As Boolean = False
    Dim nNewCcodcontr As Integer = 0
    Dim nNewCcodcena As Integer = 0
    Dim dQuant As Decimal = 0
    Dim dtDtminima As DateTime
    Dim dDurataEsecPr As Decimal = 0
    Dim lSufase As Integer = 0
    Dim dtValid As DateTime
    Dim dttTmp As New DataTable
    Dim nMagClav As Integer = 0
    Dim bAltrui As Boolean = False
    Dim bScriviRiga As Boolean = False
    Dim bNonRiportareNoteDistintaBase As Boolean = False
    Dim strCartellaTmp As String = ""
    Dim dQta As Decimal
    Dim dQuantFant As Decimal
    Dim dRapp As Decimal
    Dim dttDesTagl As New DataTable
    Try
      'Se attiva, non riporta le note da MOVDIS
      If BGESTORDLIST = True Then
        strCartellaTmp = "BSORGSOL"
      Else
        If BORDINI = True Then strCartellaTmp = "BSORGSOR" Else strCartellaTmp = "BSVEBOLL"
      End If
      bNonRiportareNoteDistintaBase = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, strCartellaTmp, "OPZIONI", ".", "NonRiportareNoteDistintaBase", "0", " ", "0"))

      '--------------------------------
      'chiama la TrattaDbDoc di BELBMENU per farsi ritornare l'elenco dei materiali e delle lavorazioni 
      'che poi dovranno essere scritte in CORPOIMP e ATTIVIT/ASSRIS
      With dtrEc
        If BORDINI Then
          dtValid = NTSCDate(dtrEc!ec_datcons)
        Else
          dtValid = NTSCDate(dttET.Rows(0)!et_datdoc)
        End If
        'per gli articoli TCO non prende la quantità di riga, ma di taglia
        If NTSCInt(dtrEc!xxo_codtagl) = 0 Then
          dQta = NTSCDec(!ec_quant)
        Else
          dtrRow = dttECTC.Select("ec_riga = " & dtrEc!ec_riga.ToString)  'dettaglio taglie collegate alla riga su cui sono
          dQta = NTSCDec(dtrRow(0)("ec_quant" & nIndiceTagliaRoot.ToString.PadLeft(2, "0"c)))
        End If

        If Not CType(oCleComm, CLELBMENU).TrattaDbDoc_TrovaFiglieLav(dttImp, dttLav, gstrTipoArtConf, gbUsaFiltroCommessa, _
                                        gbRiportaFasiFantasmi, gbUsaFiltroPadre, strDittaCorrente, strError, _
                                        dtrEc!ec_codart.ToString, NTSCInt(IIf(bFasi, dtrEc!ec_fase, 0)), _
                                        NTSCInt(dtrEc!ec_riga), dtDatval, _
                                        NTSCInt(dtrEc!ec_commeca), NTSCStr(!ec_subcommeca), NTSCInt(!ec_codclie), _
                                        dtValid, dQta, NTSCDec(!ec_colli), _
                                        NTSCInt(!ec_codcena), NTSCStr(!ec_codcfam), 0, lRigaFigli, dPrezzo, _
                                        NTSCStr(!ec_umprz), NTSCStr(!ec_unmis), NTSCStr(!ec_ump), 0, 0, 0, 0, 0, False, _
                                        strDescrTagliaRoot, strDescrTagliaPadr, nIndiceTagliaRoot, nIndiceTagliaPadr, strColoreRoot, strColorePadr) Then
          If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
          Return False
        End If
      End With
      If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))

      '--------------------------------
      'adesso alimento i datatable attivit, assris e dttecimp
      'prima memorizzo in dtrHT il datarow del padre !!!!!!
      dtrHT = dtrEc

      dRRfenceArtPadre = 0
      dFcorrltArtPadre = 0
      GetMemDttArti(NTSCStr(dtrEc!ec_codart))
      dRRfenceArtPadre = NTSCDec(dttArti.Rows(0)!ar_rrfence)       'utilizzato in esplosione DB per non fare una query su artico per ogni riga di impegno/scarico
      dFcorrltArtPadre = NTSCDec(dttArti.Rows(0)!ar_fcorrlt)       'utilizzato in esplosione DB per non fare una query su artico per ogni riga di impegno/scarico

      For Each dtrT In dttImp.Rows
        '-------------------------------------
        'questa in vb6 è la ScriviRigaDiba
        '-------------------------------------

        'Magazzino impegni
        If bIgnoraMagDistintaBase And dtrT!md_proven.ToString <> "C" Then
          nMagimp = NTSCInt(dttET.Rows(0)!et_magimp)
        Else
          If (dtrT!md_proven.ToString = "C" Or dtrT!md_proven.ToString = "M" Or dtrT!md_proven.ToString = "I" Or dtrT!md_proven.ToString = "E") And NTSCInt(dtrT!md_codmaga) > 0 Then
            nMagimp = NTSCInt(dtrT!md_codmaga)
          Else
            nMagimp = NTSCInt(dttET.Rows(0)!et_magimp)
          End If
        End If

        nMagClav = nMagClav1
        'L'articolo figlio è gestito a fasi quindi se insiste su magazzini 'merce altrui presso terzi'
        'annullo l'attribuzuzione dei riferimenti al c/lav. attivo
        If NTSCInt(dtrT!md_fasefigli) <> 0 And nMagClav <> 0 Then
          oCldDocu.ValCodiceDb(nMagimp.ToString, strDittaCorrente, "TABMAGA", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            If dttTmp.Rows(0)!tb_flclavo.ToString = "X" Or dttTmp.Rows(0)!tb_flclavo.ToString = "C" Then bAltrui = True Else bAltrui = False
          End If
          dttTmp.Clear()
          If bAltrui Then nMagClav = 0
        End If

        '---------------------------
        'se devo fare il test esistenza ... determino se scrivere la riga oppure no
        bScriviRiga = True
        If BORDINI = False Then
          'veboll
          bScriviRiga = TestEsistenza(NTSCStr(dtrT!md_codfigli), nMagimp, NTSCInt(dtrT!md_fasefigli), NTSCDec(dtrT!md_quant), _
                                      0, NTSCInt(dtrT!md_commeca), NTSCInt(dttET.Rows(0)!et_causcap), False, dtrT)

          If bScriviRiga Then
            If nMagClav = 0 Or (nMagClav <> 0 And nMagClav <> nMagimp) Or NTSCStr(dtrT!md_codfigli) = "D" Or NTSCStr(dtrT!md_codfigli) = "M" Or bRifClavManuale Then
              'Scrive la riga (senza rif. conto lavoro)
            Else
              'Scrive la riga (con rif. conto/lavoro)
              'non scrivo qui la riga: lo faccio nella TrattaDbDoc_ScriviRigaClav con routine specifica
              bScriviRiga = False
              TrattaDbDoc_ScriviRigaClav(dtrEt, dtrEc, dtrT, nMagimp, nMagClav)
            End If
          End If    'If bScriviRiga Then
        Else
          'gsor
          If bTest_esistenza_duplimpprod Then
            bScriviRiga = TestEsistenza(NTSCStr(dtrT!md_codfigli), nMagimp, NTSCInt(dtrT!md_fasefigli), NTSCDec(dtrT!md_quant), _
                                        0, NTSCInt(dtrT!md_commeca), 0, False, dtrT)
          End If
        End If    'If BORDINI = False Then

        If bScriviRiga Then
          bInImportRigheDiba = True
          dttECIMP.Rows.Add(dttECIMP.NewRow)
          With dttECIMP.Rows(dttECIMP.Rows.Count - 1)
            '--------------------
            'forzo la CorpoOnAddNewRow
            !codditt = "."
            !codditt = strDittaCorrente
            !ec_magaz = nMagimp     'sempre prima di impostare l'articolo, altrimenti non riesce a proporre l'ubicazione dinamica dal magazzino in validaz articolo
            !ec_magaz2 = 0
            !ec_codart = NTSCStr(dtrT!md_codfigli)
            If NTSCStr(!ec_codart).Trim = "" Then
              'l'articolo è bloccato: salto la riga
              bInImportRigheDiba = False
              dttECIMP.Rows(dttECIMP.Rows.Count - 1).Delete()
              dttECIMP.AcceptChanges()
              GoTo PASSA
            End If
            !ec_flprznet = "N"
            !ec_umprz = "N"
            !ec_fase = dtrT!md_fasefigli
            If BORDINI Then !ec_datcons = NTSCDate(dtrT!md_datcons)
            !ec_misura1 = dtrT!md_misura1
            !ec_misura2 = dtrT!md_misura2
            !ec_misura3 = dtrT!md_misura3
            !ec_unmis = dtrT!md_unmis
            !ec_ump = dtrT!md_ump
            !ec_colli = NTSCDec(dtrT!md_colli)
            !ec_quant = NTSCDec(dtrT!md_quant)
            'If NTSCStr(!ec_note).Trim = "" Then !ec_note = dtrT!md_note

            'COME NELL'MRP PER GLI IMPEGNI/SCARICHI LE NOTE DI DIBA PREVALGONO SU QUELLE DELL'ARTICOLO 
            If NTSCDec(dttArti.Rows(0)!ar_pesoca) = 1 Then
              !ec_note = ""
            Else
              If BGESTORDLIST = True Then !ec_note = NTSCStr(dttArti.Rows(0)!ar_note)
            End If

            If bNonRiportareNoteDistintaBase = False Then
              !ec_note = dtrT!md_note
            End If

            If BORDINI Then
              !ec_rigaor = dtrT!md_rigah
            Else
              !ec_prriga = dtrT!md_rigah
            End If
            !ec_codcfam = IIf(dtrT!md_famprod.ToString = "", " ", dtrT!md_famprod).ToString
            !ec_commeca = dtrT!md_commeca
            !ec_subcommeca = dtrT!md_subc
            !ec_codcena = dtrT!md_codcena
            If BORDINI And BGESTORDLIST = False Then !ec_datconsor = NTSCDate(dtrT!md_datcons)
            !ec_codclie = dtrT!md_codclie
            !ec_tctaglia = IIf(dtrT!md_descrtagliaroot.ToString = "", " ", dtrT!md_descrtagliaroot)
            !ec_tcindtagl = dtrT!md_indicetagliaroot
            If dttECIMP.Columns.Contains("xxo_tcindtaglf") Then
              !xxo_tcindtaglf = dtrT!md_indicetagliapadr
            End If
            If dttECIMP.Columns.Contains("xxo_tctagliaf") Then
              If NTSCInt(dttArti.Rows(0)!ar_codtagl) > 0 Then
                oCldDocu.ValCodiceDb(NTSCInt(dttArti.Rows(0)!ar_codtagl).ToString, strDittaCorrente, "TABTAGL", "N", "", dttDesTagl)
                !xxo_tctagliaf = NTSCStr(dttDesTagl.Rows(0)("tb_dest" & NTSCStr(dtrT!md_indicetagliapadr).PadLeft(2, "0"c))).ToUpper.Trim
              Else
                !xxo_tctagliaf = " "
              End If
            End If
            If Not dttECIMP.Columns.Contains("xxmd_interr") Then dttECIMP.Columns.Add("xxmd_interr", GetType(String))
            !xxmd_interr = dtrT!md_interr
          End With    'With dttECIMP.Rows(dttECIMP.Rows.Count - 1)

          'salvo eseguendo tutti i test del caso
          If Not CorpoImpRecordSalva(dttECIMP.Rows.Count - 1, False, Nothing) Then
            'cancello la riga su cui sono
            bInImportRigheDiba = False
            dttECIMP.Rows(dttECIMP.Rows.Count - 1).Delete()
            dttECIMP.AcceptChanges()
          End If
        End If    'If bScriviRiga Then
PASSA:
        bInImportRigheDiba = False
      Next    'For Each dtrT In dttImp.Rows


      If BGESTORDLIST Then
        'devo rideterminare se il magazzino impegni di testata è terzista, visto che in questa fase il dato non è corretto
        bTerzista = CType(oCleComm, CLELBMENU).IsTerzista(strDittaCorrente, NTSCInt(dtrHT!ec_magimp.ToString))
      End If

      '-------------------------------
      For Each dtrT In dttLav.Rows
        dQuant = NTSCDec(dtrT!dd_quant)
        dQuantFant = NTSCDec(dtrT!dd_quantfant)
        'TCP
        If NTSCInt(dtrHT!xxo_codtagl) <> 0 Then
          If nFasi <> 1 Then
            GoTo Salta
          End If
          If dQuant = 0 Then 'non dovrebbe succedere mai...
            dQuant = dQtaTotale
            dQuantFant = dQtaTotale
          Else
            dRapp = dQtaTotale / dQuant
            dQuant = dQtaTotale
            dQuantFant = ArrDbl(dQuantFant * dRapp, 3)
          End If
        End If

        nNewCodcent = NTSCInt(dtrT!dd_codcent)
        nNewCcodcontr = NTSCInt(dtrT!tb_ccodcontr)
        nNewCcodcena = NTSCInt(dtrT!tb_ccodcena)
        bSostCent = False

        'Se documento terzista e edMagimp di testata <> !tb_magterz legge un centro corretto
        If bTerzista And (NTSCInt(dttET.Rows(0)!et_magimp) <> NTSCInt(dtrT!tb_magterz)) Then
          oCldDocu.GetTabcentFromMagaz(strDittaCorrente, NTSCInt(dttET.Rows(0)!et_magimp), dttTmp)
          If dttTmp.Rows.Count > 0 Then
            bSostCent = True
            nNewCodcent = NTSCInt(dttTmp.Rows(0)!tb_codcent)
            nNewCcodcontr = NTSCInt(dttTmp.Rows(0)!tb_ccodcontr)
            nNewCcodcena = NTSCInt(dttTmp.Rows(0)!tb_ccodcena)
          End If
          dttTmp.Clear()
        End If
        'Acquisisce solo la prima fase
        If bTerzista And NTSCInt(dtrT!dd_numlav) >= 2 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128601502951718750, "Attenzione! Per l'articolo |" & dtrT!dd_codartpadre.ToString & "|, essendo prodotto da un terzista, indicare una sola fase di lavorazione in Distinta base. Le fasi successive alla prima verranno ignorate.")))
          Exit For
        End If
        'Sul flag dd_terzista ha prevalenza il tipo (tb_flclavo) di magimp
        If ((dtrT!dd_terzista.ToString = "S" Or dtrT!dd_terzista.ToString = "E") And (Not bTerzista)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128601503713593750, "In distinta base, per l'articolo |" & dtrT!dd_codartpadre.ToString & "| (fase |" & dtrT!dd_fase.ToString & "|), è indicato che il ciclo di lavorazione è svolto da un terzista mentre il magazzino degli impegni, dove viene svolta la lavorazione, indica che si tratta di lavorazione interna. Verrà considerata una lavorazione interna.")))
        End If
        If ((dtrT!dd_terzista.ToString = "N" Or dtrT!dd_terzista.ToString = "I") And bTerzista) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128601504265312500, "In distinta base, per l'articolo |" & dtrT!dd_codartpadre.ToString & "| (fase |" & dtrT!dd_fase.ToString & "|), è indicato che il ciclo di lavorazione è svolto internamente mentre il magazzino degli impegni, dove viene svolta la lavorazione, indica che si tratta di una lavorazione terzista. Verrà considerata una lavorazione terzista, ed utilizzato il centro |" & nNewCodcent.ToString & "|.")))
        End If
        If ((dtrT!dd_terzista.ToString = "S" Or dtrT!dd_terzista.ToString = "E") And bTerzista) And bSostCent Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128601504316250000, "In distinta base, per l'articolo |" & dtrT!dd_codartpadre.ToString & "| (fase |" & dtrT!dd_fase.ToString & "|), è indicato un centro di lavorazione terzista diverso da quello corrente; verrà utilizzato, in sostituzione al centro |" & dtrT!dd_codcent.ToString & "|, il centro |" & nNewCodcent.ToString & "|.")))
        End If
        If bTerzista Then
          If bNoTempiSuTerzisti Then
            dDurataEsecPr = 0
          Else
            dDurataEsecPr = (NTSCInt(dtrT!dd_rrfencepadre) + Int(NTSCDec(dtrT!dd_fcorrltpadre) * dQuant)) * 8 'supponendo che i terzisti abbiano giorni di 8 ore ....
          End If
        Else
          If NTSCDec(dtrT!dd_perqta) <> 0 Then
            dDurataEsecPr = ArrDbl(NTSCDec(dtrT!dd_tempese) * dQuantFant / NTSCDec(dtrT!dd_perqta), 4)
          Else
            dDurataEsecPr = ArrDbl(NTSCDec(dtrT!dd_tempese) * dQuantFant, 4)
          End If
        End If
        dtDtminima = CType(oCleComm, CLELBMENU).DateAddEx(strDittaCorrente, "d", ((NTSCInt(dtrT!dd_rrfencepadre) + Int(NTSCDec(dtrT!dd_fcorrltpadre) * dQuant)) * -1), NTSCDate(dtrT!dd_datcons))

        '-------------------------------------
        'questa in vb6 è la ScriviRigaFase
        '-------------------------------------
        If BORDINI Then
          If dttATTIVIT.Select("at_riga = " & NTSCInt(dtrT!dd_rigah).ToString & " AND at_fase = " & NTSCInt(dtrT!dd_fase).ToString).Length > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131014022360868117, "Attenzione: tra le lavorazioni collegate è già presente la fase |" & NTSCInt(dtrT!dd_fase).ToString & "| (probabile presenza di Fasi duplicate in Distinta Base o di Semilavorati 'Fantasma'). Quella associata alla distinta |" & NTSCStr(dtrT!dd_coddb) & "| verrà scartata.")))
            GoTo Salta1
          End If

          dttATTIVIT.Rows.Add(dttATTIVIT.NewRow)
          With dttATTIVIT.Rows(dttATTIVIT.Rows.Count - 1)
            '--------------------
            'forzo la AttivitOnAddNewRow
            !codditt = "."
            !codditt = strDittaCorrente
            !at_riga = dtrT!dd_rigah
            !at_fase = dtrT!dd_fase
            !at_coddb = dtrT!dd_codartpadre
            !at_codlavo = dtrT!dd_codlavo
            !at_tempattpr = dtrT!dd_tempatt
            !at_tempesepr = dDurataEsecPr
            !at_dtminima = dtDtminima
            If NTSCInt(dtrT!dd_numlav) = 1 Then
              If BGESTORDLIST Then !at_suolprogr = 0
              !at_sutipork = "H"
              !at_suanno = 0
              !at_suserie = " "
              !at_sunumord = 0
              !at_suriga = 0
              !at_sufase = 0
            Else
              If BGESTORDLIST Then
                !at_suolprogr = dtrT!dd_rigah
              Else
                !at_sutipork = "H"
                !at_suanno = dttET.Rows(0)!et_anno
                !at_suserie = dttET.Rows(0)!et_serie
                !at_sunumord = dttET.Rows(0)!et_numdoc
                !at_suriga = dtrT!dd_rigah
              End If
              !at_sufase = lSufase
            End If
            !at_relazsuc = dtrT!dd_relazsuc
            !at_qtapr = dQuant
            !at_datcons = dtrT!dd_datcons
            !at_terzista = IIf(bTerzista, "S", "N").ToString
            !at_note = dtrT!dd_note

            'salvo eseguendo tutti i test del caso
            If Not AttivitRecordSalva(dttATTIVIT.Rows.Count - 1, False, Nothing) Then
              'cancello la riga su cui sono
              dttATTIVIT.Rows(dttATTIVIT.Rows.Count - 1).Delete()
              dttATTIVIT.AcceptChanges()
            Else
              dttASSRIS.Rows.Add(dttASSRIS.NewRow)
              '--------------------
              dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!codditt = strDittaCorrente
              'forzo la AssrisOnAddNewRow (non si può fare con il codditt, perchè testa che i primi 3 caratteri siano "as_"
              dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_percent = 1
              dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_percent = 100
              dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_riga = dtrT!dd_rigah
              dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_fase = dtrT!dd_fase
              dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_codcent = nNewCodcent
              dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_controp = nNewCcodcontr
              dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_codcena = nNewCcodcena
              If bTerzista Then
                dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_cmacora = 0
                dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_pagaora = 0
                dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_cmacoratt = 0
                dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_pagaoratt = 0
              Else
                dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_cmacora = dtrT!tb_cmacora
                dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_pagaora = dtrT!tb_pagaora
                dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_cmacoratt = dtrT!tb_cmacoratt
                dttASSRIS.Rows(dttASSRIS.Rows.Count - 1)!as_pagaoratt = dtrT!tb_pagaoratt
              End If

              dttASSRIS.AcceptChanges()
            End If
          End With    'With dttATTIVIT.Rows(dttATTIVIT.Rows.Count - 1)

        Else
          'veboll
          dttLAVCENT.Rows.Add(dttLAVCENT.NewRow)
          With dttLAVCENT.Rows(dttLAVCENT.Rows.Count - 1)
            '--------------------
            'forzo la AttivitOnAddNewRow
            !codditt = "."
            !codditt = strDittaCorrente
            !lce_riga = dtrT!dd_rigah
            '!lce_rigaa = dtrT!dd_riga      'impostata in automatico nella OnAddNew
            !lce_codart = dtrEc!ec_codart
            !lce_codlavo = dtrT!dd_codlavo
            !lce_tempatte = dtrT!dd_tempatt
            !lce_tempese = NTSCDec(dDurataEsecPr)
            !lce_codcent = nNewCodcent
            !lce_controp = nNewCcodcontr
            !lce_codcena = nNewCcodcena
            !lce_qtaes = dQuant
            !lce_commeca = dtrEc!ec_commeca
            !lce_subcommeca = dtrEc!ec_subcommeca
            !lce_codcfam = dtrEc!ec_codcfam
            !lce_note = dtrT!dd_note

            If bTerzista Then
              !lce_cmacora = 0
              !lce_pagaora = 0
              !lce_cmacoratt = 0
              !lce_pagaoratt = 0
            Else
              !lce_cmacora = dtrT!tb_cmacora
              !lce_pagaora = dtrT!tb_pagaora
              !lce_cmacoratt = dtrT!tb_cmacoratt
              !lce_pagaoratt = dtrT!tb_pagaoratt
            End If
          End With    'With dttLAVCENT.Rows(dttLAVCENT.Rows.Count - 1)
        End If
Salta:
        lSufase = NTSCInt(dtrT!dd_fase)
Salta1:
      Next

      If Not TrattaDbDoc_EsplosioneRicorsiva(dtrEc) Then Return False

      If Not dttATTIVIT Is Nothing Then dttATTIVIT.AcceptChanges()
      If Not dttASSRIS Is Nothing Then dttASSRIS.AcceptChanges()
      If Not dttLAVCENT Is Nothing Then dttLAVCENT.AcceptChanges()

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
      dtrHT = Nothing
      bInImportRigheDiba = False
    End Try
  End Function
  Public Overridable Function TrattaDbDoc_ScriviRigaClav(ByRef dtrEt As DataRow, ByRef dtrEc As DataRow, _
                                                        ByRef dtrT As DataRow, ByVal nMagimp As Integer, _
                                                        ByVal nMagClav As Integer) As Boolean
    'dtrT = riga contenente i figli della distinta base presi da TrattaDbDoc_TrovaFiglieLav
    Dim dQuantRes As Decimal = 0
    Dim dColliRes As Decimal = 0
    Dim dttMcla As New DataTable
    Dim nRow As Integer = 0
    Dim dtrTmp() As DataRow = Nothing
    Dim dQuant As Decimal = 0
    Dim dColli As Decimal = 0
    Dim strErrore As String = ""
    Dim lLotto As Integer = 0

    Try
      dQuantRes = NTSCDec(dtrT!md_quant)
      dColliRes = NTSCDec(dtrT!md_colli)

      '---------------------
      'creo i saldi del c/to lavoro
      If Not ApriSaldiClav(dtrEt, NTSCStr(dtrT!md_codfigli), NTSCInt(dtrT!md_fasefigli), nMagClav, dttMcla, _
                           NTSCInt(dtrT!md_commeca), bTMclav, lTmNumIT) Then GoTo SkipsaldiClav

      dtrTmp = dttMcla.Select("", "Datdoc, mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga")

      Do While nRow < dtrTmp.Length And dQuantRes <> 0
        If ArrDbl(NTSCDec(dtrTmp(nRow)!Saldo), 3) >= dQuantRes Then
          dQuant = dQuantRes
          dColli = dColliRes
          dQuantRes = 0
          dColliRes = 0
        Else
          dQuant = ArrDbl(NTSCDec(dtrTmp(nRow)!Saldo), 3)
          If Not CType(oCleComm, CLELBMENU).ConvQuantUM(strDittaCorrente, dtrT!md_codfigli.ToString, _
                                      dtrT!md_ump.ToString, dQuant, NTSCDec(dtrT!md_misura1), _
                                      NTSCDec(dtrT!md_misura2), NTSCDec(dtrT!md_misura3), _
                                      dtrT!md_unmis.ToString, dColli, strErrore, 3) Then
            Return False
          End If
          If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
          dQuantRes = ArrDbl(dQuantRes - dQuant, 3)
          If Not CType(oCleComm, CLELBMENU).ConvQuantUM(strDittaCorrente, dtrT!md_codfigli.ToString, _
                                      dtrT!md_ump.ToString, dQuantRes, NTSCDec(dtrT!md_misura1), _
                                      NTSCDec(dtrT!md_misura2), NTSCDec(dtrT!md_misura3), _
                                      dtrT!md_unmis.ToString, dColliRes, strErrore, 3) Then
            Return False
          End If
          If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
        End If

        'Il saldo della riga copre la qta da evadere
        'Riporta il lotto (tranne se l'opzione è abilitata)
        lLotto = 0
        If Not bNonRiportareLottiDaCLav Then
          'Legge il lotto dalla riga (solo se la riga non l'ha già attribuito), possono essere nulli...
          lLotto = LeggiLottoRiga(NTSCStr(dtrTmp(nRow)!mm_cltipo), NTSCInt(dtrTmp(nRow)!mm_clanno), _
                                  NTSCStr(dtrTmp(nRow)!mm_clserie), NTSCInt(dtrTmp(nRow)!mm_clnum), NTSCInt(dtrTmp(nRow)!mm_clriga))
        End If

        dttECIMP.Rows.Add(dttECIMP.NewRow)
        With dttECIMP.Rows(dttECIMP.Rows.Count - 1)
          '--------------------
          'forzo la CorpoOnAddNewRow
          !codditt = "."
          !codditt = strDittaCorrente
          !ec_magaz = nMagimp     'sempre prima di impostare l'articolo, altrimenti non riesce a proporre l'ubicazione dinamica dal magazzino in validaz articolo
          !ec_magaz2 = 0
          !ec_codart = NTSCStr(dtrT!md_codfigli)
          !ec_fase = dtrT!md_fasefigli
          !ec_misura1 = dtrT!md_misura1
          !ec_misura2 = dtrT!md_misura2
          !ec_misura3 = dtrT!md_misura3
          !ec_unmis = dtrT!md_unmis
          !ec_ump = dtrT!md_ump
          !ec_colli = dColli
          !ec_quant = dQuant
          !ec_note = dtrT!md_note
          !ec_prriga = dtrT!md_rigah
          !ec_codcfam = IIf(dtrT!md_famprod.ToString = "", " ", dtrT!md_famprod).ToString
          !ec_commeca = dtrT!md_commeca
          !ec_subcommeca = dtrT!md_subc
          !ec_codcena = dtrT!md_codcena
          !ec_codclie = dtrT!md_codclie
          !ec_tctaglia = IIf(dtrT!md_descrtagliaroot.ToString = "", " ", dtrT!md_descrtagliaroot)
          !ec_tcindtagl = dtrT!md_indicetagliaroot

          !ec_lotto = lLotto
          !ec_cltipo = NTSCStr(dtrTmp(nRow)!mm_cltipo)
          !ec_clanno = NTSCInt(dtrTmp(nRow)!mm_clanno)
          !ec_clserie = NTSCStr(dtrTmp(nRow)!mm_clserie)
          !ec_clnum = NTSCInt(dtrTmp(nRow)!mm_clnum)
          !ec_clriga = NTSCInt(dtrTmp(nRow)!mm_clriga)

          If Not dttECIMP.Columns.Contains("xxmd_interr") Then dttECIMP.Columns.Add("xxmd_interr", GetType(String))
          !xxmd_interr = dtrT!md_interr
        End With    'With dttECIMP.Rows(dttECIMP.Rows.Count - 1)

        'salvo eseguendo tutti i test del caso
        bInImportRigheDiba = True
        If Not CorpoImpRecordSalva(dttECIMP.Rows.Count - 1, False, Nothing) Then
          bInImportRigheDiba = False
          'cancello la riga su cui sono
          dttECIMP.Rows(dttECIMP.Rows.Count - 1).Delete()
          dttECIMP.AcceptChanges()
        End If
        bInImportRigheDiba = False
        nRow += 1
      Loop    'Do While nRow < dtrTmp.Length And dQuantRes <> 0

SkipsaldiClav:
      If dQuantRes <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128681236227500000, "Articolo |" & dtrT!md_codfigli.ToString & "|, magazzino |" & nMagimp.ToString & "|: non ci sono sufficienti carichi di conto lavorazione nel magazzino indicato. Quantità mancante: |" & dQuantRes.ToString(oApp.FormatQta) & "|")))
        dQuant = dQuantRes
        dColli = dColliRes

        dttECIMP.Rows.Add(dttECIMP.NewRow)
        With dttECIMP.Rows(dttECIMP.Rows.Count - 1)
          '--------------------
          'forzo la CorpoOnAddNewRow
          !codditt = "."
          !codditt = strDittaCorrente
          !ec_magaz = nMagimp     'sempre prima di impostare l'articolo, altrimenti non riesce a proporre l'ubicazione dinamica dal magazzino in validaz articolo
          !ec_magaz2 = 0
          !ec_codart = NTSCStr(dtrT!md_codfigli)
          !ec_fase = dtrT!md_fasefigli
          !ec_misura1 = dtrT!md_misura1
          !ec_misura2 = dtrT!md_misura2
          !ec_misura3 = dtrT!md_misura3
          !ec_unmis = dtrT!md_unmis
          !ec_ump = dtrT!md_ump
          !ec_colli = dColliRes
          !ec_quant = dQuantRes
          !ec_note = dtrT!md_note
          !ec_prriga = dtrT!md_rigah
          !ec_codcfam = IIf(dtrT!md_famprod.ToString = "", " ", dtrT!md_famprod).ToString
          !ec_commeca = dtrT!md_commeca
          !ec_subcommeca = dtrT!md_subc
          !ec_codcena = dtrT!md_codcena
          !ec_codclie = dtrT!md_codclie
          !ec_tctaglia = IIf(dtrT!md_descrtagliaroot.ToString = "", " ", dtrT!md_descrtagliaroot)
          !ec_tcindtagl = dtrT!md_indicetagliaroot

          If Not dttECIMP.Columns.Contains("xxmd_interr") Then dttECIMP.Columns.Add("xxmd_interr", GetType(String))
          !xxmd_interr = dtrT!md_interr
        End With    'With dttECIMP.Rows(dttECIMP.Rows.Count - 1)

        'salvo eseguendo tutti i test del caso
        bInImportRigheDiba = True
        If Not CorpoImpRecordSalva(dttECIMP.Rows.Count - 1, False, Nothing) Then
          'cancello la riga su cui sono
          bInImportRigheDiba = False
          dttECIMP.Rows(dttECIMP.Rows.Count - 1).Delete()
          dttECIMP.AcceptChanges()
        End If
        bInImportRigheDiba = False
      End If    'If dQuantRes <> 0 Then

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
      bInImportRigheDiba = False
      dttMcla.Clear()
    End Try
  End Function
  Public Overridable Function TrattaDbDoc_EsplosioneRicorsiva(ByRef dtrEc As DataRow) As Boolean
    'se richiesto aggiungo tra i prodotti da produrre i semilavorati consumati
    Dim evnt As NTSEventArgs = Nothing
    Dim dttTmp As New DataTable
    Dim bFirstLoop As Boolean = False

    Try
      'ora l'opzione di registro opera anche in duplicazione documento
      'ma solo se il doc di origine non è un ORDINE DI PROD / CARICO DA PROD
      If bInDuplicadoc Then
        If strInDuplicadocTiporkOrig = "H" Or strInDuplicadocTiporkOrig = "T" Then Return True
      End If

      If Not ((nEsplosioneDbRicorsiva = 1 Or nEsplosioneDbRicorsiva = 2 Or nEsplosioneDbRicorsiva = -1) And bNoDatValDistinta) Then Return True

      If nEsplosioneDbRicorsiva > 0 And nInEsplodiDbRicorsiva = 0 Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 129615254818173828, "Esplodo ricorsivamente la distinta base '|" & NTSCStr(dtrEc!ec_codart) & "|'?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then GoTo FINE_ELAB
        If nEsplosioneDbRicorsiva = 2 Then
          nInEsplodiDbRicorsiva = 1
          bFirstLoop = True
        End If
      End If

      For Each dtrSl As DataRow In dttECIMP.Select(IIf(BORDINI, "ec_rigaor = ", "ec_prriga = ").ToString & dtrEc!ec_riga.ToString)
        'Il figlio non deve essere aggiunto al corpo se in 
        'distinta base il campo 'Interr. albero' ha il valore 'interrompi albero'
        If NTSCStr(dtrSl!xxmd_interr) <> "S" Then
          oCldDocu.ValCodiceDb(NTSCStr(dtrSl!ec_codart), strDittaCorrente, "ARTICO", "S", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            If NTSCStr(dttTmp.Rows(0)!ar_coddb).Trim <> "" And NTSCStr(dttTmp.Rows(0)!ar_makebuy).Trim <> "B" Then
              bInImportRigheDiba = True
              dttEC.Rows.Add(dttEC.NewRow)
              With dttEC.Rows(dttEC.Rows.Count - 1)
                '--------------------
                'forzo la CorpoOnAddNewRow
                !codditt = "."
                !codditt = strDittaCorrente
                !ec_magaz = dtrEc!ec_magaz
                !ec_magaz2 = 0
                !ec_codart = dtrSl!ec_codart
                !ec_fase = dtrSl!ec_fase
                !ec_misura1 = dtrSl!ec_misura1
                !ec_misura2 = dtrSl!ec_misura2
                !ec_misura3 = dtrSl!ec_misura3
                !ec_unmis = dtrSl!ec_unmis
                !ec_ump = dtrSl!ec_ump
                !ec_colli = dtrSl!ec_colli
                !ec_quant = dtrSl!ec_quant
                !ec_codcfam = dtrSl!ec_codcfam
                !ec_commeca = dtrSl!ec_commeca
                !ec_subcommeca = dtrSl!ec_subcommeca
                !ec_codcena = dtrSl!ec_codcena
                !ec_lotto = dtrSl!ec_lotto
                !ec_coddivi = dtrSl!ec_coddivi
                If BORDINI Then !ec_datcons = dtrEc!ec_datcons
              End With    'With dttEC.Rows(dttEC.Rows.Count - 1)
              bInImportRigheDiba = False
              If Not RecordSalva(dttEC.Rows.Count - 1, False, Nothing) Then
                dttEC.Rows(dttEC.Rows.Count - 1).Delete()
              End If
            End If    'If NTSCStr(dttTmp.Rows(0)!ar_coddb).Trim <> "" Then
          End If    'If dttTmp.Rows.Count > 0 Then
        End If    'NTSCStr(dtrSl!xxmd_interr) <> "S"
      Next    'For Each dtrSl As DataRow In dttECIMP.Select(IIf(BORDINI, "ec_rigaor = ", "

FINE_ELAB:
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
      If bFirstLoop Then nInEsplodiDbRicorsiva = 0
      bInImportRigheDiba = False
    End Try
  End Function

  Public Overridable Function CaricaCorpoDaDB(ByVal strCodart As String, ByVal nFase As Integer, _
                                              ByVal strUnmis As String, ByVal dColli As Decimal, _
                                              ByVal strDataDb As String) As Boolean
    'data una data validità, un articolo + fase + UM + Colli
    'esplode la DB ed inserisce nel corpo del documento le righe figlie
    'come per fare un documento di trasferimento materiali a terzista a mano
    Dim dttImp As New DataTable
    Dim dttLav As New DataTable
    Dim strError As String = ""
    Dim bFasi As Boolean = False
    Dim dQta As Decimal = 0
    Dim bDaMisure As Boolean = False
    Dim dttTmp As New DataTable
    Try

      If dttET.Rows(0)!et_tipork.ToString = "T" Or dttET.Rows(0)!et_tipork.ToString = "H" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129502921368330078, "Funzione non abilitata su ordini o carichi da produzione")))
        Return False
      End If
      oCldDocu.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129502917437998047, "Articolo '|" & strCodart & "|' non presente in anagrafica")))
        Return False
      End If
      If NTSCStr(dttTmp.Rows(0)!ar_um4).ToUpper = strUnmis.ToUpper Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129502918057744141, "Funzione non abilitata con unità di misura 'formula'")))
        Return False
      End If
      If NTSCInt(dttTmp.Rows(0)!ar_codtagl) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129502910482675781, "Funzione non abilitata con articoli TCO")))
        Return False
      End If


      bFasi = False
      If bModLEX Then If dttTmp.Rows(0)!ar_gesfasi.ToString = "S" Then bFasi = True

      CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, strCodart, strUnmis, dColli, 0, 0, 0, dQta, bDaMisure, strError)

      'ottengo i figli della DB
      If Not CType(oCleComm, CLELBMENU).TrattaDbDoc_TrovaFiglieLav(dttImp, dttLav, gstrTipoArtConf, gbUsaFiltroCommessa, _
                                        gbRiportaFasiFantasmi, gbUsaFiltroPadre, strDittaCorrente, strError, _
                                        strCodart, NTSCInt(IIf(bFasi, nFase, 0)), _
                                        0, NTSCDate(strDataDb), _
                                        NTSCInt(dttET.Rows(0)!et_commeca), NTSCStr(dttET.Rows(0)!et_subcommeca), 0, _
                                        NTSCDate(strDataDb), dQta, dColli, _
                                        NTSCInt(dttET.Rows(0)!et_codcena), NTSCStr(dttET.Rows(0)!et_codcfam), 0, 0, 0, _
                                        "N", strUnmis, NTSCStr(dttTmp.Rows(0)!ar_unmis), 0, 0, 0, 0, 0, False, _
                                        "", "", 0, 0, "", "") Then
        If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
        Return False
      End If
      dttTmp.Clear()

      If dttImp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129502929698271485, "Non sono stati trovati figli validi per la distinta base '|" & strCodart & "|' alla data indicata")))
        Return True
      End If

      'inserisco i figli
      bInImportRigheDiba = True
      For Each dtrT As DataRow In dttImp.Rows
        dttEC.Rows.Add(dttEC.NewRow)
        With dttEC.Rows(dttEC.Rows.Count - 1)
          'forzo la MovordOnAddNewRow
          !codditt = "."
          !codditt = strDittaCorrente
          !ec_codart = NTSCStr(dtrT!md_codfigli)
          !ec_fase = NTSCInt(dtrT!md_fasefigli)
          !ec_unmis = NTSCStr(dtrT!md_unmis)
          !ec_colli = NTSCDec(dtrT!md_colli)
          !ec_quant = NTSCDec(dtrT!md_quant)
        End With
        If Not RecordSalva(dttEC.Rows.Count - 1, False, Nothing) Then
          dttEC.Rows(dttEC.Rows.Count - 1).Delete()
        End If
      Next
      bInImportRigheDiba = False

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
      bInImportRigheDiba = False
      dttTmp.Clear()
      dttImp.Clear()
      dttLav.Clear()
    End Try
  End Function

  Public Overridable Function LeggiLottoRiga(ByVal strTipoDoc As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumdoc As Integer, ByVal lRiga As Integer) As Integer
    Try

      Return oCldDocu.LeggiLottoRiga(strDittaCorrente, strTipoDoc, nAnno, strSerie, lNumdoc, lRiga)

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


  Public Overridable Function CalcolaLottox(ByRef dtrEc As DataRow) As Boolean
    'articolo a gestiti a lotto: se non ho indicato il lotto, 
    'cerco di proporlo in base alle regole indicate in anagrafica articoli
    'deve scatenarsi SOLO sulle righe di carico da produzione 
    'o nei documenti di carico che non movimentano 2 magazzini 
    'quindi gli unici documenti gestiti sono ORDINI FORNITORI, ORDINI FORNITORI APERTI, 
    'RIGHE DI CARICO DI ORDINI DI PRODUZIONE, DDT RICEVUTI CHE NON MOVIMENTANO 2 MAGAZZINI, 
    'RIGHE DI CARICO DI CARICHI DA PRODUZIONE, FATTURE IMMEDIATE RICEVUTE
    Dim strLottox As String = ""
    Dim strErr As String = ""
    Try
      If BGESTORDLIST Then Return True

      If BORDINI Then
        If bAbilitaGenerazLottoSuOrdini = False Then Return True
      End If

      If bDocEmesso Then Return True

      Select Case dtrEc!ec_tipork.ToString
        Case "O", "$", "H", "T", "L"
          'posso proseguire
        Case "M"
          If NTSCInt(dtrEc!ec_magaz2) <> 0 Then Return True
        Case Else
          Return True
      End Select

      'test per determinare se dover creare l'anagrafica lotto in automatico
      If Not (NTSCStr(dtrEc!xxo_geslotti) = "S" And NTSCInt(dtrEc!ec_lotto) = 0 And bLottoNew) Then Return True

      'chiamo la routine passandogli tutto quello che potrebbe servire
      If Not CType(oCleComm, CLELBMENU).CreaAnalottiDaDoc(strDittaCorrente, dttET.Rows(0), dtrEc, strLottox, strErr) Then
        If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
        Return False
      End If

      If strLottox.Trim <> "" Then
        dtrEc!xxo_lottox = strLottox
        'se articolo gestito a lotti, lotto già inserito, gestione lotti alfanumerica
        'e nuova riga su opzione chiede se aprire l'anagrafica lotto per impostare la data di scadenza e/o altri parametri
        'solo se non in import dati massivo o business in modalità BATCH
        If NTSCStr(dtrEc!xxo_geslotti) = "S" And NTSCInt(dtrEc!ec_lotto) <> 0 And bLottoNew Then
          If VisMsg() And oApp.Batch = False And bApriAnalottiDopoNew Then
            ThrowRemoteEvent(New NTSEventArgs("APRIANALO:", NTSCStr(dtrEc!ec_codart) & "§" & NTSCStr(dtrEc!ec_lotto)))
          End If
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

  Public Overridable Function ScaricaLottox(ByVal nRow As Integer, ByRef dttCorpo As DataTable) As Boolean
    'scarico in automatico i lotti

    'se la quantità del lotto è inferiore alla quantità di riga, devo spezzare ed aggiungere un'altra riga.
    'tutte le righe verranno salvate con .AcceptChanges, visto che sicuramente la riga è corretta
    'se la riga è aggancata ad un ordine/nota di prelievo verificare il flag 'evaso C/S' (dovrebbe essere solo sull'ultima riga,
    'ma forse è durante il TestPreSalva del documento che quel flag viene sistemato
    'se i lotti non bastano per coprire la quantità che avevo dichiarato, aggiungo una riga senza lotto
    'se ho inserito la riga nel DDT ed al cambio di riga ho visualizzato lo zoom per selezionare le righe d'ordine
    'e ne ho acquistite diverse, verificare che il comportamento di attribuzione lotto sia corretto
    'provare anche multiselezione articoli gestiti a lotti da ZOOM articoli
    'provare con articoli KIT, evasi da ordine o no

    Dim dtrEc As DataRow = Nothing      'riga appena inserita, validata e salvata
    Dim dttLotti As New DataTable
    Dim dQtares As Decimal = 0
    Dim dQtanewrow As Decimal = 0
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim bModifColliSuQuantTmp As Boolean = bModifColliSuQuant
    Dim strFlevas As String = ""
    Dim strFlevaspr As String = ""
    Try
      dtrEc = dttCorpo.Rows(nRow)

      If ScaricaLottox_Check(dtrEc) = False Then Return True


      If Not CType(oCleComm, CLELBMENU).ScaricaLottox_CercaLotti(strDittaCorrente, dtrEc!ec_codart.ToString, _
                                       NTSCInt(dtrEc!ec_fase), NTSCInt(dttET.Rows(0)!et_conto), NTSCStr(dttArti.Rows(0)!ar_tipscarlotx), _
                                       NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                       NTSCInt(IIf(NTSCInt(dtrEc!xxo_esist) = -1, NTSCInt(dtrEc!ec_magaz), NTSCInt(dtrEc!ec_magaz2))), _
                                       NTSCDec(dtrEc!ec_quant), NTSCStr(dttET.Rows(0)!et_tipork), _
                                       NTSCInt(dttET.Rows(0)!et_tipobf), _
                                       NTSCInt(IIf(NTSCInt(dtrEc!xxo_esist) = -1, NTSCInt(dtrEc!ec_causale), NTSCInt(dtrEc!ec_causale2))), _
                                       dtrEc.Table, dttLotti) Then Return False
      If dttLotti.Rows.Count = 0 Then
        'Non ho trovato lotti, se c'è la logistica attiva allora provo ad assegnare un lotto con le regole della logistica.
        If bModSupRme Then
          Dim lTmp As Integer = 0
          Dim strTmp As String = ""
          PropostaLotto("BNVEBOLL", NTSCStr(dtrEc!ec_tipork), NTSCStr(dttET.Rows(0)!et_vistato), NTSCStr(dtrEc!ec_codart), _
                        NTSCInt(dtrEc!ec_fase), NTSCInt(dtrEc!ec_magaz), 0, lTmp, strTmp)
          If lTmp <> 0 Then dtrEc!ec_lotto = lTmp 'lotto dall'ordine
          If strTmp <> "" Then dtrEc!xxo_lottox = strTmp 'lotto da tabmaga (lotto generico)
        End If
        Return True
      End If

      'tratto la riga passatami in input
      If NTSCDec(dttLotti.Rows(0)!lp_esist) >= NTSCDec(dtrEc!ec_quant) Then
        'quantità residua lotto > di quantità indicata in riga: correggo il lotto ed esco
        dtrEc!ec_lotto = NTSCInt(dttLotti.Rows(0)!alo_lotto)
        dtrEc.AcceptChanges()
        Return True
      Else
        dQtares = NTSCDec(dtrEc!ec_quant)
        If NTSCDec(dtrEc!ec_colli) <> 0 Then bModifColliSuQuant = True 'così al cambio della qta cambio anche i colli (anche se non è stata settata l'opzione di registro)

        'quantità residua lotto minore di quantità di riga: devo spezzare
        'predispongo la riga MODELLO (identica a quella di partenza)
        dttTmp = dtrEc.Table.Clone
        dttTmp.Clear()
        dttTmp.ImportRow(dtrEc)
        dttTmp.Rows(0)!ec_salcon = "C"
        dttTmp.Rows(0)!ec_npsalcon = "C"

        'flag evaso di ordine/nota di prelievo: se la riga era evasa totale, mantengo il flag 'evaso solo sulla prima riga'
        'al cambio della quantità verrebbe impostato come 'non evaso totalmente'
        strFlevas = NTSCStr(dtrEc!ec_salcon)
        strFlevaspr = NTSCStr(dtrEc!ec_npsalcon)

        'correggo la quantità sulla riga esistente
        dtrEc!ec_quant = NTSCDec(dttLotti.Rows(0)!lp_esist)
        If dtrEc!ec_ump.ToString = dtrEc!ec_unmis.ToString And NTSCDec(dtrEc!ec_colli) <> 0 Then
          bInValidazQuant = True  'altrimenti ricalcola la qta dai colli!!!!
          dtrEc!ec_colli = NTSCDec(dttLotti.Rows(0)!lp_esist)
          bInValidazQuant = False 'rimetto com'era
        End If
        dtrEc!ec_salcon = strFlevas
        dtrEc!ec_npsalcon = strFlevaspr
        dtrEc!ec_lotto = NTSCInt(dttLotti.Rows(0)!alo_lotto)
        dtrEc.AcceptChanges()
        dQtares -= NTSCDec(dttLotti.Rows(0)!lp_esist)

        'devo aggiungere le righe per arrivare ad avere una qunatità = a quella che avovo impostato
        i = 0
        While dQtares > 0
          i += 1
          dQtanewrow = dQtares
          If i < dttLotti.Rows.Count Then
            dQtanewrow = NTSCDec(dttLotti.Rows(i)!lp_esist)
            If dQtanewrow > dQtares Then dQtanewrow = dQtares
          End If

          'aggiungo la riga
          If NTSCStr(dtrEc!ec_tipork) = "U" Or NTSCStr(dtrEc!ec_tipork) = "Y" Then
            lCrigheYT = lCrigheYT + nIncremContatoreRiga
            dttTmp.Rows(0)!ec_riga = lCrigheYT
          Else
            lCrighe = lCrighe + nIncremContatoreRiga
            dttTmp.Rows(0)!ec_riga = lCrighe
          End If
          dtrEc.Table.ImportRow(dttTmp.Rows(0))

          With dtrEc.Table.Rows(dtrEc.Table.Rows.Count - 1)
            'correggo il lotto
            If i < dttLotti.Rows.Count Then !ec_lotto = NTSCInt(dttLotti.Rows(i)!alo_lotto)
            .AcceptChanges()  'salvo la riga, così quando cambierò la quantità sicuramente non verranno ricalcolati i prezzi/sconti

            'correggo la quantità
            !ec_quant = dQtanewrow
            If !ec_ump.ToString = !ec_unmis.ToString And NTSCDec(!ec_colli) <> 0 Then
              bInValidazQuant = True  'altrimenti ricalcola la qta dai colli!!!!
              !ec_colli = dQtanewrow
              bInValidazQuant = False 'rimetto com'era
            End If
            .AcceptChanges()
          End With
          dQtares -= dQtanewrow
        End While     'While dQtanewrow > 0
      End If    'If NTSCDec(dttLotti.Rows(0)!lp_esist) >= NTSCDec(dtrEc!ec_quant) Then

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
      bModifColliSuQuant = bModifColliSuQuantTmp
      dttTmp.Clear()
      dttLotti.Clear()
    End Try
  End Function
  Public Overridable Function ScaricaLottox_Check(ByRef dtrEc As DataRow) As Boolean
    'controlla se il lotto può/deve venir scaricato in automatico

    'solo su nuovi documenti
    'solo su articoli gestiti a lotti con indicato in anagrafica di scaricare in automatico il lotto
    'solo su nuove righe
    'solo su righe di scarico
    'no su ordini, si magazzino e note di prelievo
    'no con modulo project management attivo
    'no articoli TCO
    'sono gestiti anche i documenti che muovono 2 magazzini

    'restituisce TRUE se il lotto dovrà venir scaricato in automatico, diversamente FALSE
    Try
      'If bNew = False Then Return False ora è possibile scaricare in automatico lotti anche in documenti in modifica, purchè la riga sia nuova
      If BORDINI Then Return False
      If bModPM Then Return False

      'Dim s As String = NTSCStr(dtrEc!ec_codart)
      If NTSCStr(dtrEc!xxo_geslotti) = "N" Then Return False
      If NTSCInt(dtrEc!ec_lotto) <> 0 Then Return False
      If NTSCDec(dtrEc!ec_quant) = 0 Then Return False
      If NTSCInt(dtrEc!xxo_esist) <> -1 And NTSCInt(dtrEc!xxo_esist2) <> -1 Then Return False
      If GetMemDttArti(dtrEc!ec_codart.ToString).Rows.Count = 0 Then Return True
      If NTSCStr(dttArti.Rows(0)!ar_tipscarlotx) = "M" Then Return False
      If NTSCInt(dttArti.Rows(0)!ar_codtagl) <> 0 Then Return False

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
  Public Overridable Function ScaricaLottox_CheckLogistica(ByRef dtrEc As DataRow) As Boolean
    'controlla se il lotto può/deve venir scaricato in automatico

    'solo su nuovi documenti
    'solo su articoli gestiti a lotti con indicato in anagrafica di scaricare in automatico il lotto
    'solo su nuove righe
    'solo su righe di scarico
    'no su ordini, si magazzino e note di prelievo
    'no con modulo project management attivo
    'no articoli TCO
    'sono gestiti anche i documenti che muovono 2 magazzini

    'restituisce TRUE se il lotto dovrà venir scaricato in automatico, diversamente FALSE
    Try
      'If bNew = False Then Return False ora è possibile scaricare in automatico lotti anche in documenti in modifica, purchè la riga sia nuova
      If BORDINI Then Return False
      If bModPM Then Return False

      'Dim s As String = NTSCStr(dtrEc!ec_codart)
      If NTSCStr(dtrEc!xxo_geslotti) = "N" Then Return False
      If NTSCInt(dtrEc!ec_lotto) <> 0 Then Return False
      'If NTSCDec(dtrEc!ec_quant) = 0 Then Return False 'Per verificare se va applicata la proposta logistica su palmare oppure no

      If NTSCInt(dtrEc!xxo_esist) <> -1 And NTSCInt(dtrEc!xxo_esist2) <> -1 Then Return False
      If GetMemDttArti(dtrEc!ec_codart.ToString).Rows.Count = 0 Then Return True
      If NTSCStr(dttArti.Rows(0)!ar_tipscarlotx) = "M" Then Return False
      If NTSCInt(dttArti.Rows(0)!ar_codtagl) <> 0 Then Return False

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



  Public Overridable Function ApriSaldiClav(ByRef dtrEt As DataRow, _
                                            ByVal strCodart As String, ByVal nFase As Integer, _
                                            ByVal nMagClav As Integer, ByRef dttOut As DataTable, _
                                            Optional ByVal bTMclav As Boolean = False, _
                                            Optional ByVal lTMnumit As Integer = 0) As Boolean
    Try
      'non più utilizzata. mantenuta solo per compatibilità con versioni precedenti
      Return ApriSaldiClav(dtrEt, strCodart, nFase, nMagClav, dttOut, 0, bTMclav, lTMnumit)
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
  Public Overridable Function ApriSaldiClav(ByRef dtrEt As DataRow, _
                                            ByVal strCodart As String, ByVal nFase As Integer, _
                                            ByVal nMagClav As Integer, ByRef dttOut As DataTable, _
                                            ByVal lCommeca As Integer, _
                                            Optional ByVal bTMclav As Boolean = False, _
                                            Optional ByVal lTMnumit As Integer = 0) As Boolean
    'UTILIZZATA PER CONTO LAVORO PASSIVO:
    'fa la stessa cosa della stored procedure bussp_bsveboll9_creasalcl
    'fa la stessa cosa della bussp_bsveboll9_aprisalcl

    'riga contenente i figli della distinta base presi da TrattaDbDoc_TrovaFiglieLav

    'da utilizzarsi poi con il seguente comando:
    'dtrTmp = dttOk.Select("Datdoc, mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga")

    'nei carichi da produzione viene sempre passato dtrEt con tipork = 'T': è nel dal che viene fatta la conversione in 'U'
    'dtrT = riga contenente i figli della distinta base presi da TrattaDbDoc_TrovaFiglieLav
    Dim dttMcla As New DataTable
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY
    Dim dtrTmp() As DataRow = Nothing
    Dim i As Integer = 0
    Dim dtTmp As DateTime
    Dim strTold As String = ""
    Dim nAold As Integer = 0
    Dim strSold As String = ""
    Dim lNold As Integer = 0
    Dim lRold As Integer = 0
    Dim dSomma As Decimal = 0
    Dim dttOut1 As New DataTable
    Dim dttScar As DataTable = Nothing
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrEt, strCodart, nFase, nMagClav, dttOut, lCommeca, bTMclav, lTMnumit})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrEt = CType(oIn(0), DataRow)
        dttOut = CType(oIn(4), DataTable)
        Return CBool(oOut)
      End If
      '----------------


      '---------------------------
      'fa la stessa cosa della stored procedure bussp_bsveboll9_creasalcl
      'crea i saldi di c/to alvoro escludendo il documento in fase di modifica


      'solo per c/to lavoro passivo con articoli non a fasi e articoli gestiti a commessa in ordini e magazzino:
      'cerco le bolle di c/to lavoro aperte sulla commessa indicata sullo scarico a produzione
      If lCommeca <> 0 And nFase = 0 Then
        If CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEBOLL", "OPZIONI", ".", "CtoLavPassXComm", "0", " ", "0")) Then
          oCldDocu.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then If NTSCStr(dttTmp.Rows(0)!ar_gescomm) <> "S" Then lCommeca = 0
          dttTmp.Clear()
        Else
          lCommeca = 0
        End If
      End If

      If Not oCldDocu.ApriSaldiClav(strDittaCorrente, strCodart, bNew, nMagClav, _
                                    dtrEt!et_tipork.ToString, NTSCInt(dtrEt!et_anno), _
                                    dtrEt!et_serie.ToString, NTSCInt(dtrEt!et_numdoc), _
                                    nFase, dttMcla, lCommeca) Then
        'Si è verificato un errore determ. i saldi di c/lav
        'continuo non considerando carichi di c/lav
        Return False
      Else
        'Tira su i saldi di ogni singola riga di movimenti locali di c/to lavoro: causale 1
        'dalle righe di c/to lavoro tolgo i saldi già utilizzati nel documento corrente
        strTold = "."
        dtrTmp = dttEC.Select("xxo_esist <> 0 AND ec_quant <> 0 AND " & _
                        " ec_codart = " & CStrSQL(strCodart) & " AND ec_fase = " & nFase.ToString & _
                        " AND ec_magaz = " & nMagClav.ToString & _
                        " AND ec_clnum <> 0 AND (xxo_flclavo = 'C' OR xxo_flclavo = 'F' OR xxo_flclavo = 'X') AND " & _
                        "(xxo_tipcaum = 'S' OR xxo_tipcaum = 'F' OR xxo_tipcaum = 'Y')", _
                        "ec_cltipo, ec_clanno, ec_clserie, ec_clnum, ec_clriga")
        For i = 0 To dtrTmp.Length - 1
          If dtrTmp(i)!ec_cltipo.ToString <> strTold Or NTSCInt(dtrTmp(i)!ec_clanno) <> nAold Or _
             dtrTmp(i)!ec_clserie.ToString <> strSold Or NTSCInt(dtrTmp(i)!ec_clnum) <> lNold Or _
             NTSCInt(dtrTmp(i)!ec_clriga) <> lRold Then
            If dSomma <> 0 Then
              dtTmp = oCldDocu.ClavGetTmDatdoc(strDittaCorrente, strTold, nAold, strSold, lNold)
              dttMcla.Rows.Add(New Object() {strCodart, nMagClav, strTold, _
                                             nAold, strSold, lNold, lRold, _
                                             IIf(dtTmp = Nothing, DBNull.Value, dtTmp), dSomma, 0})
              dSomma = 0
            End If
          End If
          strTold = dtrTmp(i)!ec_cltipo.ToString
          nAold = NTSCInt(dtrTmp(i)!ec_clanno)
          strSold = dtrTmp(i)!ec_clserie.ToString
          lNold = NTSCInt(dtrTmp(i)!ec_clnum)
          lRold = NTSCInt(dtrTmp(i)!ec_clriga)
          dSomma += (NTSCDec(dtrTmp(i)!ec_quant) * NTSCDec(dtrTmp(i)!xxo_esist))
        Next
        If dSomma <> 0 Then
          dtTmp = oCldDocu.ClavGetTmDatdoc(strDittaCorrente, strTold, nAold, strSold, lNold)
          dttMcla.Rows.Add(New Object() {strCodart, nMagClav, strTold, _
                                         nAold, strSold, lNold, lRold, _
                                         IIf(dtTmp = Nothing, DBNull.Value, dtTmp), dSomma, 0})
          dSomma = 0
        End If

        'Tira su i saldi di ogni singola riga di movimenti locali: causale 2
        'dalle righe di c/to lavoro tolgo i saldi già utilizzati nel documento corrente
        strTold = "."
        dtrTmp = dttEC.Select("xxo_esist2 <> 0 AND ec_quant <> 0 AND " & _
                " ec_codart = " & CStrSQL(strCodart) & " AND ec_fase = " & nFase.ToString & _
                " AND ec_magaz2 <> 0 AND ec_magaz2 = " & nMagClav.ToString & _
                " AND ec_clnum <> 0 AND (xxo_flclavo2 = 'C' OR xxo_flclavo2 = 'F' OR xxo_flclavo2 = 'X') AND " & _
                "(xxo_tipcaum2 = 'S' OR xxo_tipcaum2 = 'F' OR xxo_tipcaum2 = 'Y')", _
                "ec_cltipo, ec_clanno, ec_clserie, ec_clnum, ec_clriga")
        For i = 0 To dtrTmp.Length - 1
          If dtrTmp(i)!ec_cltipo.ToString <> strTold Or NTSCInt(dtrTmp(i)!ec_clanno) <> nAold Or _
             dtrTmp(i)!ec_clserie.ToString <> strSold Or NTSCInt(dtrTmp(i)!ec_clnum) <> lNold Or _
             NTSCInt(dtrTmp(i)!ec_clriga) <> lRold Then
            If dSomma <> 0 Then
              dtTmp = oCldDocu.ClavGetTmDatdoc(strDittaCorrente, strTold, nAold, strSold, lNold)
              dttMcla.Rows.Add(New Object() {strCodart, nMagClav, strTold, _
                                             nAold, strSold, lNold, lRold, _
                                             IIf(dtTmp = Nothing, DBNull.Value, dtTmp), dSomma, 0})
              dSomma = 0
            End If
          End If
          strTold = dtrTmp(i)!ec_cltipo.ToString
          nAold = NTSCInt(dtrTmp(i)!ec_clanno)
          strSold = dtrTmp(i)!ec_clserie.ToString
          lNold = NTSCInt(dtrTmp(i)!ec_clnum)
          lRold = NTSCInt(dtrTmp(i)!ec_clriga)
          dSomma += (NTSCDec(dtrTmp(i)!ec_quant) * NTSCDec(dtrTmp(i)!xxo_esist2))
        Next
        If dSomma <> 0 Then
          dtTmp = oCldDocu.ClavGetTmDatdoc(strDittaCorrente, strTold, nAold, strSold, lNold)
          dttMcla.Rows.Add(New Object() {strCodart, nMagClav, strTold, _
                                         nAold, strSold, lNold, lRold, _
                                         IIf(dtTmp = Nothing, DBNull.Value, dtTmp), dSomma, 0})
          dSomma = 0
        End If

        'Tira su i saldi di ogni singola riga di movimenti locali: scarichi
        'dalle righe di c/to lavoro tolgo i saldi già utilizzati nel documento corrente
        'fa il giro 2 volte, la prima sugli scarichi in dttECIMP, la seconda sugli scarichi se è aperta la form modale degli scarichi collegati
        '(in quel caso le righe di scarico sono solo in dsImpe, e non in dttECIMP)
        dttScar = dttECIMP
SCARICHI:
        strTold = "."
        dtrTmp = dttScar.Select("xxo_esist <> 0 AND ec_quant <> 0 AND " & _
                " ec_codart = " & CStrSQL(strCodart) & " AND ec_fase = " & nFase.ToString & _
                " AND ec_magaz = " & nMagClav.ToString & _
                " AND ec_clnum <> 0 AND (xxo_flclavo = 'C' OR xxo_flclavo = 'F' OR xxo_flclavo = 'X') AND " & _
                "(xxo_tipcaum = 'S' OR xxo_tipcaum = 'F' OR xxo_tipcaum = 'Y')", _
                "ec_cltipo, ec_clanno, ec_clserie, ec_clnum, ec_clriga")
        For i = 0 To dtrTmp.Length - 1
          If dtrTmp(i)!ec_cltipo.ToString <> strTold Or NTSCInt(dtrTmp(i)!ec_clanno) <> nAold Or _
             dtrTmp(i)!ec_clserie.ToString <> strSold Or NTSCInt(dtrTmp(i)!ec_clnum) <> lNold Or _
             NTSCInt(dtrTmp(i)!ec_clriga) <> lRold Then
            If dSomma <> 0 Then
              dtTmp = oCldDocu.ClavGetTmDatdoc(strDittaCorrente, strTold, nAold, strSold, lNold)
              dttMcla.Rows.Add(New Object() {strCodart, nMagClav, strTold, _
                                             nAold, strSold, lNold, lRold, _
                                             IIf(dtTmp = Nothing, DBNull.Value, dtTmp), dSomma, 0})
              dSomma = 0
            End If
          End If
          strTold = dtrTmp(i)!ec_cltipo.ToString
          nAold = NTSCInt(dtrTmp(i)!ec_clanno)
          strSold = dtrTmp(i)!ec_clserie.ToString
          lNold = NTSCInt(dtrTmp(i)!ec_clnum)
          lRold = NTSCInt(dtrTmp(i)!ec_clriga)
          dSomma += (NTSCDec(dtrTmp(i)!ec_quant) * NTSCDec(dtrTmp(i)!xxo_esist))
        Next
        If dSomma <> 0 Then
          dtTmp = oCldDocu.ClavGetTmDatdoc(strDittaCorrente, strTold, nAold, strSold, lNold)
          dttMcla.Rows.Add(New Object() {strCodart, nMagClav, strTold, _
                                         nAold, strSold, lNold, lRold, _
                                         IIf(dtTmp = Nothing, DBNull.Value, dtTmp), dSomma, 0})
          dSomma = 0
        End If
      End If   'If Not oCldDocu.ApriSaldiClav(strDittaCorrente, NTSCStr(dtrT!md_codfigli), bNew, nMagClav, _

      If bOk = False Then
        'processo le righe di scarico presenti nella form modale degli scarichi collegati, se aperta
        bOk = True
        If Not dsImpe Is Nothing Then
          If dsImpe.Tables.Count > 0 Then
            If dsImpe.Tables("CORPOIMP").Rows.Count > 0 Then
              dttScar = dsImpe.Tables("CORPOIMP")
              GoTo SCARICHI
            End If
          End If
        End If
      End If

      '---------------------------
      'fa la stessa cosa della bussp_bsveboll9_aprisalcl
      'da utilizzarsi con il seguente comando:
      'dtrTmp = dttOk.Select("Saldo > 0.0001", "Datdoc, mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga")
      If bTMclav = False Then
        If Not oDttgr.NTSGroupBy(dttMcla, dttOut, _
                        "mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga, Sum(somma) as Saldo, Min(Datdoc) as Datdoc", _
                        "Datdoc is null OR Datdoc <= " & CDataSQL(NTSCDate(dtrEt!et_datdoc)), _
                        "Datdoc, mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga") Then
          Return False
        End If

        dtrTmp = dttOut.Select("Saldo < 0.0001")
        For i = 0 To dtrTmp.Length - 1
          dtrTmp(i).Delete()
        Next
      Else
        'personalizzazione TM: do preferenza all'impegno di trasferimento passato in input ...
        If Not oDttgr.NTSGroupBy(dttMcla, dttOut1, _
                        "mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga, Sum(somma) as Saldo, Min(Datdoc) as Datdoc, Max(NumIT) As NumIT", _
                        "Datdoc is null OR Datdoc <= " & CDataSQL(NTSCDate(dtrEt!et_datdoc)), _
                        "Datdoc, mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga") Then

          Return False
        End If
        dttOut1.Columns.Add("xx_order")
        dttOut1.Columns("xx_order").DefaultValue = 0
        For Each dtrRow As DataRow In dttOut1.Rows
          If NTSCInt(dtrRow!Numit) = lTMnumit Then
            dtrRow!xx_order = 1
          Else
            dtrRow!xx_order = 0
          End If
        Next
        dttOut1.AcceptChanges()

        dttOut = dttOut1.Clone
        For Each dtrRow As DataRow In dttOut1.Select("", "xx_order DESC, Datdoc, mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga")
          dttOut.ImportRow(dtrRow)
        Next

        dtrTmp = dttOut.Select("Saldo < 0.0001")
        For i = 0 To dtrTmp.Length - 1
          dtrTmp(i).Delete()
        Next
      End If

      dttOut.AcceptChanges()

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
    End Try
  End Function
  Public Overridable Function ApriSaldiClavProd(ByRef dtrEt As DataRow, _
                                          ByVal strCodart As String, ByVal nFase As Integer, _
                                          ByVal nMagClav As Integer, ByRef dttOut As DataTable) As Boolean
    'CTO LAVORO ATTIVO
    'SCOPIAZZATA E MODIFICATA RISPETTO A 'ApriSaldiClav' (in vb6 'ApriSaldiClat')

    'fa la stessa cosa della stored procedure bussp_bsveboll9_creasalcp
    'fa la stessa cosa della bussp_bsveboll9_aprisalcp

    'da utilizzarsi poi con il seguente comando:
    'dtrTmp = dttOk.Select("Datdoc, mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga")

    Dim dttMcla As New DataTable
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY
    Dim dtrTmp() As DataRow = Nothing
    Dim i As Integer = 0
    Dim dtTmp As DateTime
    Dim strTold As String = ""
    Dim nAold As Integer = 0
    Dim strSold As String = ""
    Dim lNold As Integer = 0
    Dim lRold As Integer = 0
    Dim dSomma As Decimal = 0
    Dim dttOut1 As New DataTable

    Try
      '---------------------------
      'fa la stessa cosa della stored procedure bussp_bsveboll9_creasalcp
      If Not oCldDocu.ApriSaldiClavProd(strDittaCorrente, strCodart, bNew, nMagClav, _
                                    dtrEt!et_tipork.ToString, NTSCInt(dtrEt!et_anno), _
                                    dtrEt!et_serie.ToString, NTSCInt(dtrEt!et_numdoc), _
                                    nFase, dttMcla) Then
        'Si è verificato un errore determ. i saldi di c/lav
        'continuo non considerando carichi di c/lav
        Return False
      Else
        strTold = "."
        dtrTmp = dttEC.Select("xxo_esist <> 0 AND ec_quant <> 0 AND " & _
                        " ec_codart = " & CStrSQL(strCodart) & " AND ec_fase = " & nFase.ToString & _
                        " AND ec_magaz = " & nMagClav.ToString & _
                        " AND ec_ccnum <> 0 AND (xxo_flclavo = 'C' OR xxo_flclavo = 'X') AND " & _
                        " xxo_tipcaum = 'E'", _
                        "ec_cctipo, ec_ccanno, ec_ccserie, ec_ccnum, ec_ccriga")
        For i = 0 To dtrTmp.Length - 1
          If dtrTmp(i)!ec_cctipo.ToString <> strTold Or NTSCInt(dtrTmp(i)!ec_ccanno) <> nAold Or _
             dtrTmp(i)!ec_ccserie.ToString <> strSold Or NTSCInt(dtrTmp(i)!ec_ccnum) <> lNold Or _
             NTSCInt(dtrTmp(i)!ec_ccriga) <> lRold Then
            If dSomma <> 0 Then
              dtTmp = oCldDocu.ClavGetTmDatdoc(strDittaCorrente, strTold, nAold, strSold, lNold)
              dttMcla.Rows.Add(New Object() {strCodart, nMagClav, strTold, _
                                             nAold, strSold, lNold, lRold, _
                                             IIf(dtTmp = Nothing, DBNull.Value, dtTmp), dSomma})
            End If
            dSomma = 0
          End If
          strTold = dtrTmp(i)!ec_cctipo.ToString
          nAold = NTSCInt(dtrTmp(i)!ec_ccanno)
          strSold = dtrTmp(i)!ec_ccserie.ToString
          lNold = NTSCInt(dtrTmp(i)!ec_ccnum)
          lRold = NTSCInt(dtrTmp(i)!ec_ccriga)
          dSomma += (NTSCDec(dtrTmp(i)!ec_quant) * NTSCDec(dtrTmp(i)!xxo_esist))
        Next
        If dSomma <> 0 Then
          dtTmp = oCldDocu.ClavGetTmDatdoc(strDittaCorrente, strTold, nAold, strSold, lNold)
          dttMcla.Rows.Add(New Object() {strCodart, nMagClav, strTold, _
                                         nAold, strSold, lNold, lRold, _
                                         IIf(dtTmp = Nothing, DBNull.Value, dtTmp), dSomma})
        End If

        'NON C'E' MAGAZZINO 2

        'NON CI SONO SCARICHI COLLEGATI

      End If   'If Not oCldDocu.ApriSaldiClav(strDittaCorrente, NTSCStr(dtrT!md_codfigli), bNew, nMagClav, _

      '---------------------------
      'fa la stessa cosa della bussp_bsveboll9_aprisalcp
      'da utilizzarsi con il seguente comando:
      'dtrTmp = dttOk.Select("Saldo > 0.0001", "Datdoc, mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga")
      If Not oDttgr.NTSGroupBy(dttMcla, dttOut, _
                        "mm_cctipo, mm_ccanno, mm_ccserie, mm_ccnum, mm_ccriga, Sum(somma) as Saldo, Min(Datdoc) as Datdoc", _
                        "Datdoc is null OR Datdoc <= " & CDataSQL(NTSCDate(dtrEt!et_datdoc)), _
                        "Datdoc, mm_cctipo, mm_ccanno, mm_ccserie, mm_ccnum, mm_ccriga") Then
        Return False
      End If

      dtrTmp = dttOut.Select("Saldo < 0.0001")
      For i = 0 To dtrTmp.Length - 1
        dtrTmp(i).Delete()
      Next

      '---------------------
      'rinomino le colonne per poter utilizzare la funzione come la ApriSaldiClav
      dttOut.Columns("mm_cctipo").ColumnName = "mm_cltipo"
      dttOut.Columns("mm_ccanno").ColumnName = "mm_clanno"
      dttOut.Columns("mm_ccserie").ColumnName = "mm_clserie"
      dttOut.Columns("mm_ccnum").ColumnName = "mm_clnum"
      dttOut.Columns("mm_ccriga").ColumnName = "mm_clriga"

      dttOut.AcceptChanges()

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


  Public Overridable Function CheckScaricoLavorazione(ByRef bAltruiPressoTerzi As Boolean) As Integer
    'Controlla se ci sono le condizioni per effettuare lo scarico di produzione
    Dim nMaga As Integer = 0
    Dim nCau As Integer
    Dim bOk1 As Boolean = False
    Dim bOK2 As Boolean = False
    Dim dttTmp As New DataTable
    Dim nCauSec As Integer = 0
    Dim dtrT() As DataRow = Nothing

    Try
      CheckScaricoLavorazione = 0

      If dttET.Rows(0)!et_tipork.ToString = "T" Then
        'Per i carichi di produzione
        nMaga = NTSCInt(dttET.Rows(0)!et_magimp)
        nCau = NTSCInt(dttET.Rows(0)!et_causcap)
        dtrT = dsTabmaga.Tables("TABMAGA").Select("tb_codmaga = " & nMaga)
        If dtrT.Length > 0 Then
          If dtrT(0)!tb_flclavo.ToString <> "C" And dtrT(0)!tb_flclavo.ToString <> "F" And dtrT(0)!tb_flclavo.ToString <> "X" Then
            Return 0
          Else
            bOk1 = True
            If dtrT(0)!tb_flclavo.ToString = "X" Or dtrT(0)!tb_flclavo.ToString = "C" Then bAltruiPressoTerzi = True Else bAltruiPressoTerzi = False
          End If
        End If

        oCldDocu.ValCodiceDb(nCau.ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          If dttTmp.Rows(0)!tb_tipcaum.ToString <> "S" Then
            Return 0
          Else
            bOK2 = True
          End If
        End If
        dttTmp.Clear()
        CheckScaricoLavorazione = nMaga
      Else
        'Per tutti gli altri documenti
        'Conrollo sul magazzino 1
        nMaga = NTSCInt(dttET.Rows(0)!et_magaz)
        nCau = NTSCInt(dttET.Rows(0)!et_causale)
        dtrT = dsTabmaga.Tables("TABMAGA").Select("tb_codmaga = " & nMaga)
        If dtrT.Length > 0 Then
          If dtrT(0)!tb_flclavo.ToString <> "C" And dtrT(0)!tb_flclavo.ToString <> "F" And dtrT(0)!tb_flclavo.ToString <> "X" Then
            bOk1 = False
          Else
            bOk1 = True
            If dtrT(0)!tb_flclavo.ToString = "X" Or dtrT(0)!tb_flclavo.ToString = "C" Then bAltruiPressoTerzi = True Else bAltruiPressoTerzi = False
          End If
        End If

        oCldDocu.ValCodiceDb(nCau.ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          nCauSec = NTSCInt(dttTmp.Rows(0)!tb_causec) 'dttEc.Rows(xx)!xxo_causec
          'scarichi su CL
          If dttTmp.Rows(0)!tb_tipcaum.ToString <> "S" And dttTmp.Rows(0)!tb_tipcaum.ToString <> "F" And dttTmp.Rows(0)!tb_tipcaum.ToString <> "Y" Then
            bOK2 = False
          Else
            bOK2 = True
          End If
        End If
        dttTmp.Clear()

        'Controllo sul magazzino 2
        If Not (bOk1 And bOK2) Then
          nMaga = NTSCInt(dttET.Rows(0)!et_magaz2)
          nCau = nCauSec

          oCldDocu.ValCodiceDb(nMaga.ToString, strDittaCorrente, "TABMAGA", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            If dttTmp.Rows(0)!tb_flclavo.ToString <> "C" And dttTmp.Rows(0)!tb_flclavo.ToString <> "F" And dttTmp.Rows(0)!tb_flclavo.ToString <> "X" Then
              Return 0
            Else
              bOk1 = True
              If dttTmp.Rows(0)!tb_flclavo.ToString = "X" Or dttTmp.Rows(0)!tb_flclavo.ToString = "C" Then bAltruiPressoTerzi = True Else bAltruiPressoTerzi = False
            End If
          End If
          dttTmp.Clear()

          oCldDocu.ValCodiceDb(nCau.ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            If dttTmp.Rows(0)!tb_tipcaum.ToString <> "S" And dttTmp.Rows(0)!tb_tipcaum.ToString <> "F" And dttTmp.Rows(0)!tb_tipcaum.ToString <> "Y" Then
              Return 0
            Else
              bOK2 = True
            End If
          End If
          dttTmp.Clear()
          CheckScaricoLavorazione = nMaga
        Else
          CheckScaricoLavorazione = nMaga
        End If
      End If    'If dttET.Rows(0)!et_tipork.ToString = "T" Then

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return 0
    Finally
      dttTmp.Clear()
    End Try
  End Function

  '----------------------
  Public Overridable Function SistemaPMSalconDaQta() As Boolean
    'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
    'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

    'mai usata
    If bModPM = False Then Return True
    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443294892734375, "NOTA PER PROGRAMMATORE: chiamare la SistemaPMSalconDaQta(ByRef dtrIn As DataRow) al posto della SistemaPMSalconDaQta()")))
    Return True
  End Function
  Public Overridable Function SistemaPMSalconDaQta(ByRef dtrIn As DataRow) As Boolean
    Try
      If bModPM = False Then Return True
      If BGESTORDLIST Then Return True
      SistemaPMSalcon(dtrIn)
      ' se qtares = 0 invece , supponiamo... che non sia gestito a qta..??
      SistemaCampiRIMDISDaCampiIMPQTA(dtrIn)
      SistemaCampiPMdaCampiPMDA(dtrIn)

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
  Public Overridable Sub SistemaPMSalcon()
    'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
    'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

    If bModPM = False Then Return
    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443295502011719, "NOTA PER PROGRAMMATORE: chiamare la SistemaPMSalcon(ByRef dtrIn As DataRow) al posto della SistemaPMSalcon()")))
    'mai usata
  End Sub
  Public Overridable Sub SistemaPMSalcon(ByRef dtrIn As DataRow, _
                                         Optional ByVal bChiamatoDaPmqtarim As Boolean = False, _
                                         Optional ByVal bChiamatoDaPmvalrim As Boolean = False)
    Dim dValore As Decimal = 0
    Try
      If bModPM = False Then Return
      If BGESTORDLIST Then Return

      If BORDINI Then
        dValore = NTSCDec(dtrIn!ec_valoremm)
      Else
        dValore = NTSCDec(dtrIn!ec_valore)
      End If

      If NTSCDec(dtrIn!xxo_pmqtares) <> 0 Or NTSCDec(dtrIn!xxo_pmvalres) <> 0 Then ' solo se <> da zero ( ma non lo sarà mai .. per ora...)
        'in base ai vari casi propone PMSALCON
        'ROBY:
        'ricalcolo valore : è necessario (da vedere per gli ordini di lavorazione...)
        '****************************************************************************
        'MARCO:
        'Per gli Ordini di lavorazione non va bene
        'griRighe.Columns(VALORE).Text = ArrDbl(ArrDbl(CDbl(griRighe.Columns(PREZZO).Text) * CDbl(griRighe.Columns(QUANT).Text) / CDbl(griRighe.Columns(PERQTA).Text) * (100 - CDbl(griRighe.Columns(SCONT1).Text)) / 100 * (100 - CDbl(griRighe.Columns(SCONT2).Text)) / 100 * (100 - CDbl(griRighe.Columns(SCONT3).Text)) / 100 * (100 - CDbl(griRighe.Columns(SCONT4).Text)) / 100 * (100 - CDbl(griRighe.Columns(SCONT5).Text)) / 100 * (100 - CDbl(griRighe.Columns(SCONT6).Text)) / 100, objStd.TrovaNdec(0)) * (100 - edScont1) / 100 * (100 - edScont2) / 100 * (100 - edScopag) / 100, objStd.TrovaNdec(0))
        If dttET.Rows(0)!et_tipork.ToString <> "T" Then
          'negli ordini non eseguo più l'aggiornamento di mo_valore, altrimenti da problemi nel seguente caso
          'ordine di 100 evaso per 60 collegato a task: sul PM aggiungo una riga di incremento di valore residuo ordine di 300
          'poi vengo nell'ordine ed aggiungo una nuova riga per 300 e la collego al task: al salvataggio di riga 
          'aggiorna anche il valore da evadere della riga DIVERSA da quella che ho inserito!!!!
          If bInvalidazEcValore = False And BORDINI = False Then
            dtrIn!ec_valore = CalcolaValoreRiga(dtrIn, NTSCDec(dtrIn!ec_prezzo), NTSCDec(dtrIn!ec_quant))
          End If
        End If
        'per gli ordini di lavorazione cerco di fare in modo che il valore sia già a posto

        ' non proponiamo mai il valore V su salcon (devbe essere esplicitamente settato dall'utente !!!)
        If bChiamatoDaPmqtarim Then
          If NTSCDec(dtrIn!xxo_pmqtarim) > 0 Then
            dtrIn!ec_pmsalcon = "C"
          End If
          GoTo pros2
        End If
        If bChiamatoDaPmvalrim Then
          If NTSCDec(dtrIn!xxo_pmvalrim) > 0 Then
            If NTSCDec(dtrIn!xxo_pmqtarim) = 0 And NTSCStr(dtrIn!xxo_prevgrup) = "F" Then
              dtrIn!ec_pmsalcon = "Q"
            Else
              dtrIn!ec_pmsalcon = "C"
            End If
          Else
            dtrIn!ec_pmsalcon = "S"
          End If
          GoTo pros2
        End If
        ' non è chiamato da qtarim o valrim : lo risetta in base ai soli valori di quat/valoremm, ignorando qtarim e valrim che verranno poi ricalcolati
        ' caso generale qta e valore maggiori o uguali al residuo
        If NTSCDec(dtrIn!ec_quant) >= NTSCDec(dtrIn!xxo_pmqtares) And dValore >= NTSCDec(dtrIn!xxo_pmvalres) Then
          dtrIn!ec_pmsalcon = "S"
        End If
        ' caso generale qta e valori inferiori al residuo
        If NTSCDec(dtrIn!ec_quant) < NTSCDec(dtrIn!xxo_pmqtares) And dValore < NTSCDec(dtrIn!xxo_pmvalres) Then
          dtrIn!ec_pmsalcon = "C"
        End If
        ' caso particoalre qta inferiore e valore uguale : genererà un warning o sarà necessaria una correzione dell'utente !
        If NTSCDec(dtrIn!ec_quant) < NTSCDec(dtrIn!xxo_pmqtares) And dValore = NTSCDec(dtrIn!xxo_pmvalres) Then
          dtrIn!ec_pmsalcon = "C" ' ...
        End If
        ' caso particolare qta uguale o superiore e valore inferiore : mette Q in salcon se prevgrup='F'
        If NTSCStr(dtrIn!xxo_gestcost) = "3" Then ' cioè costi e qta ...
          If NTSCDec(dtrIn!ec_quant) >= NTSCDec(dtrIn!xxo_pmqtares) And dValore < NTSCDec(dtrIn!xxo_pmvalres) Then
            If NTSCStr(dtrIn!xxo_prevgrup) = "F" Then
              dtrIn!ec_pmsalcon = "Q"
            Else
              dtrIn!ec_pmsalcon = "S"
            End If
          End If
        End If
        ' caso solo costo , non qta
        If NTSCStr(dtrIn!xxo_gestcost) = "2" Then ' solo costi  !!
          If dValore < NTSCDec(dtrIn!xxo_pmvalres) Then
            If NTSCStr(dtrIn!xxo_prevgrup) = "N" Then
              dtrIn!ec_pmsalcon = "C" '"C"
            Else
              dtrIn!ec_pmsalcon = "Q"
            End If
          End If
        End If
      End If    'If NTSCDec(dtrIn!xxo_pmqtares) <> 0 Or NTSCDec(dtrIn!xxo_pmvalres) <> 0 Then
pros2:
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
  Public Overridable Function SistemaCampiRIMDISDaCampiIMPQTA() As Boolean
    'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
    'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

    'mai usata
    If bModPM = False Then Return True
    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443295765361329, "NOTA PER PROGRAMMATORE: chiamare la SistemaCampiRIMDISDaCampiIMPQTA(ByRef dtrIn As DataRow) al posto della SistemaCampiRIMDISDaCampiIMPQTA()")))
    Return True
  End Function
  Public Overridable Function SistemaCampiRIMDISDaCampiIMPQTA(ByRef dtrIn As DataRow, _
                                                    Optional ByVal bChiamatoDaPmqtarim As Boolean = False, _
                                                    Optional ByVal bChiamatoDaPmSalcon As Boolean = False, _
                                                    Optional ByVal bChiamatoDaPmvalrim As Boolean = False) As Boolean
    Dim dMemqtarim As Decimal = 0
    Dim strMemsalcon As String = ""
    Dim dValore As Decimal = 0
    Try
      If bModPM = False Then Return True
      If bInSalvarigaPM Then Return True
      If BGESTORDLIST Then Return True
      bInSistemaCampiRIMDISDaCampiIMPQTAPM = True

      If BORDINI Then
        dValore = NTSCDec(dtrIn!ec_valoremm)
      Else
        dValore = NTSCDec(dtrIn!ec_valore)
      End If

      If NTSCInt(dtrIn!ec_pmtaskid) <> 0 Then 'solo se presente il taskid
        ' memorizza salcon
        strMemsalcon = NTSCStr(dtrIn!ec_pmsalcon)
        dMemqtarim = NTSCDec(dtrIn!xxo_pmqtarim)
        ' quantità
        If Not (bChiamatoDaPmqtarim Or bChiamatoDaPmvalrim Or bChiamatoDaPmSalcon) Then ' ricalcola il quantitativo  rimanente, se non inserito a mano dall'utente..
          dtrIn!xxo_pmqtarim = NTSCDec(dtrIn!xxo_pmqtares) - NTSCDec(dtrIn!ec_quant) '
          If (NTSCStr(dtrIn!ec_pmsalcon) = "Q" And oCldDocu.LegGestCostPadreDao(strDittaCorrente, NTSCInt(dtrIn!ec_commeca), NTSCInt(dtrIn!ec_pmtaskid)) = "2") Or NTSCStr(dtrIn!ec_pmsalcon) = "S" Then
            dtrIn!xxo_pmqtarim = 0
          End If
        End If
        ' adesso setto opportunamente salcon...
        If NTSCStr(dtrIn!ec_pmsalcon) = "S" Then
          dtrIn!xxo_pmqtadisda = NTSCDec(dtrIn!xxo_pmqtares)
        Else
          If NTSCStr(dtrIn!ec_pmsalcon) = "Q" Then
            If oCldDocu.LegGestCostPadreDao(strDittaCorrente, NTSCInt(dtrIn!ec_commeca), NTSCInt(dtrIn!ec_pmtaskid)) = "2" Then 'il padre è solo a costo
              dtrIn!xxo_pmqtadisda = NTSCDec(dtrIn!xxo_pmqtares)
            Else
              dtrIn!xxo_pmqtadisda = NTSCDec(dtrIn!xxo_pmqtares) - NTSCDec(dtrIn!xxo_pmqtarim)
            End If
          Else
            dtrIn!xxo_pmqtadisda = NTSCDec(dtrIn!xxo_pmqtares) - NTSCDec(dtrIn!xxo_pmqtarim)
          End If
        End If

        'If NTSCStr(dtrIn!ec_pmsalcon) = "S" Or NTSCStr(dtrIn!ec_pmsalcon) = "Q" Then
        '  griRighe.Columns(PMQTADISDA).Value = NTSCDec(dtrIn!xxo_pmqtares)
        'Else
        '  griRighe.Columns(PMQTADISDA).Value = CDbl(griRighe.Columns(PMQTARES).Text) - CDbl(griRighe.Columns(PMQTARIM).Text)
        'End If

        '********************************************************************
        'ROBY:
        'Sono costretto a rivalorizzare la riga VALORE per l'istruzione sottostante!!!
        'Non sono sicuro infatti, che sia stato eseguito prima settavaloririga (anche per i gli ordini di produzione!!!!
        'non so se OK con Ordini di produzione !!!
        'griRighe.Columns(VALORE).Text = ArrDbl(ArrDbl(CDbl(griRighe.Columns(PREZZO).Text) * CDbl(griRighe.Columns(QUANT).Text) / CDbl(griRighe.Columns(PERQTA).Text) * (100 - CDbl(griRighe.Columns(SCONT1).Text)) / 100 * (100 - CDbl(griRighe.Columns(SCONT2).Text)) / 100 * (100 - CDbl(griRighe.Columns(SCONT3).Text)) / 100 * (100 - CDbl(griRighe.Columns(SCONT4).Text)) / 100 * (100 - CDbl(griRighe.Columns(SCONT5).Text)) / 100 * (100 - CDbl(griRighe.Columns(SCONT6).Text)) / 100, objStd.TrovaNdec(0)) * (100 - edScont1) / 100 * (100 - edScont2) / 100 * (100 - edScopag) / 100, objStd.TrovaNdec(0))
        '********************************************************************
        '********************************************************************
        'MARCO:
        'Per Ordini di produzione non va bene!!!
        If dttET.Rows(0)!et_tipork.ToString <> "T" Then
          'negli ordini non eseguo più l'aggiornamento di mo_valore, altrimenti da problemi nel seguente caso
          'ordine di 100 evaso per 60 collegato a task: sul PM aggiungo una riga di incremento di valore residuo ordine di 300
          'poi vengo nell'ordine ed aggiungo una nuova riga per 300 e la collego al task: al salvataggio di riga 
          'aggiorna anche il valore da evadere della riga DIVERSA da quella che ho inserito!!!!
          If bInvalidazEcValore = False And BORDINI = False Then
            'se dto validando ec_valore non devo ricalcolarlo, altrimenti non lavora nel caso:
            'ordine qta 100 evasa per 60, residuo 40: cambio sull'ordine la qta da 100 a 60 invece di portare il valore da evadere a 0 il valore viene ricalcolato e rimane 60!!!!
            dtrIn!ec_valore = CalcolaValoreRiga(dtrIn, NTSCDec(dtrIn!ec_prezzo), NTSCDec(dtrIn!ec_quant))
          End If
        End If
        'per gli Ordini di produzione, faccio in modo che a monte venga sempre eseguita
        'la settavaloririga
        '********************************************************************

        ' valori/costi
        If Not (bChiamatoDaPmvalrim Or bChiamatoDaPmSalcon) Then
          dtrIn!xxo_pmvalrim = NTSCDec(dtrIn!xxo_pmvalres) - dValore
          If NTSCStr(dtrIn!ec_pmsalcon) = "V" Or NTSCStr(dtrIn!ec_pmsalcon) = "S" Then
            dtrIn!xxo_pmvalrim = 0
          End If
        End If
        If Not (bChiamatoDaPmSalcon) Then ' lo fa solo se chiamato da altro punto
          ' l'impostazione di salcon è l'ultima , sempre !!!
          'If griRighe.Columns(PMVALRIM).Value <= 0 Then ' costo superiore al previsto
          '  NTSCStr(dtrIn!ec_pmsalcon) = "S"
          '  griRighe.Columns(PMVALRIM).Value = 0
          '  dtrIn!xxo_pmqtarim = 0
          '
          'Else
          '  NTSCStr(dtrIn!ec_pmsalcon) = "C"
          '  ' qui controllo su sformaneto budget !!!
          'End If
        End If
        If NTSCStr(dtrIn!ec_pmsalcon) = "S" Or NTSCStr(dtrIn!ec_pmsalcon) = "V" Then
          dtrIn!xxo_pmvaldisda = NTSCDec(dtrIn!xxo_pmvalres)
        Else
          dtrIn!xxo_pmvaldisda = NTSCDec(dtrIn!xxo_pmvalres) - NTSCDec(dtrIn!xxo_pmvalrim)
        End If
        ' se è cambiato nel frattempo salcon rivede anche i dati su quantità ... (ripete il pezzo sopra...)
        'If (strMemsalcon <> NTSCStr(dtrIn!ec_pmsalcon)) Or (dMemqtarim <> dtrIn!xxo_pmqtarim) Then
        '  If NTSCStr(dtrIn!ec_pmsalcon) = "S" Then
        '    griRighe.Columns(PMQTADISDA).Value = NTSCDec(dtrIn!xxo_pmqtares)
        '  Else
        '    griRighe.Columns(PMQTADISDA).Value = CDbl(griRighe.Columns(PMQTARES).Text) - CDbl(griRighe.Columns(PMQTARIM).Text)
        '  End If
        'End If
        'alla fine.... per tutti
        'bDatiPMVariati = True
      End If ' c'è il taskid

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
      bInSistemaCampiRIMDISDaCampiIMPQTAPM = False
    End Try
  End Function
  Public Overridable Function SistemaCampiPMdaCampiPMDA(ByRef dtrIn As DataRow) As Boolean
    Try
      If bModPM = False Then Return True
      If BGESTORDLIST Then Return True
      If NTSCInt(dtrIn!ec_pmtaskid) <> 0 Then
        strFDarave = NTSCStr(dtrIn!xxo_darave) ' D o A dare o avere sulla riga
        dQuant1 = NTSCDec(dtrIn!xxo_pmvaldisda)
        If strFDarave = "A" Then
          dtrIn!ec_pmvaldis = dQuant1 * -1
        Else
          dtrIn!ec_pmvaldis = dQuant1
        End If
        dQuant2 = NTSCDec(dtrIn!xxo_pmqtadisda)
        If strFDarave = "A" Then
          dtrIn!ec_pmqtadis = dQuant2 * -1
        Else
          dtrIn!ec_pmqtadis = dQuant2
        End If
      End If ' c'è taskid sulla riga..

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
  Public Overridable Sub SistemaPMSalconDaQta_VarF(ByVal dQuant As Decimal, ByVal dmmvalore As Decimal)
    Dim dMemqtarim As Decimal
    Dim strMemsalcon As String
    Try
      If bModPM = False Then Return

      ' sistema pmsalcon
      If dFQtares <> 0 Or dFCostores <> 0 Then ' solo se <> da zero ( ma non lo sarà mai .. per ora...)
        'in base ai vari casi propone PMSALCON
        ' caso generale qta e valore maggiori o uguali al residuo
        If dQuant >= dFQtares And dmmvalore >= dFCostores Then
          strFSalcon = "S"
        End If
        ' caso generale qta e valori inferiori al residuo
        If dQuant < dFQtares And dmmvalore < dFCostores Then
          strFSalcon = "C"
        End If
        ' caso particoalre qta inferiore e valore uguale : genererà un warning o sarà necessaria una correzione dell'utente !
        If dQuant < dFQtares And dmmvalore = dFCostores Then
          strFSalcon = "C" ' ...
        End If
        ' caso particolare qta uguale o superiore e valore inferiore : mette Q in salcon se prevgrup='F'
        If strFGestcost = "3" Then ' cioè costi e qta ...
          If dQuant >= dFQtares And dmmvalore < dFCostores Then
            If strFPrevgrup = "F" Then
              strFSalcon = "Q"
            Else
              strFSalcon = "S"
            End If
          End If
        End If
        ' caso solo costo , non qta
        If strFGestcost = "2" Then ' solo costi  !!
          If dmmvalore < dFCostores Then
            If strFPrevgrup = "N" Then
              strFSalcon = "C" '"C"
            Else
              strFSalcon = "Q"
            End If
          End If
        End If
      End If
      ' adesso parte 2 :
      ' memorizza salcon
      strMemsalcon = strFSalcon
      dMemqtarim = dFQtarim
      ' quantità
      dFQtarim = dFQtares - dQuant
      If strFSalcon = "Q" Or strFSalcon = "S" Then
        dFQtarim = 0
      End If
      ' adesso setto opportunamente salcon...

      If strFSalcon = "S" Or strFSalcon = "Q" Then
        dFQtadisda = dFQtares
      Else
        dFQtadisda = dFQtares - dFQtarim
      End If

      ' valori/costi
      dFCostorim = dFCostores - dmmvalore '
      If strFSalcon = "V" Or strFSalcon = "S" Then
        dFCostorim = 0
      End If

      If strFSalcon = "S" Or strFSalcon = "V" Then
        dFValdisda = dFCostores
      Else
        dFValdisda = dFCostores - dFCostorim
      End If
      ' terza parte :
      dQuant1 = dFValdisda
      If strFDarave = "A" Then
        dFValdis = dQuant1 * -1
      Else
        dFValdis = dQuant1
      End If
      dQuant2 = dFQtadisda
      If strFDarave = "A" Then
        dFQtadis = dQuant2 * -1
      Else
        dFQtadis = dQuant2
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

  Public Overridable Function AbilitaDisabilitaRigaNum(ByRef dtrIn As DataRow) As Boolean
    Try
      If dtrIn Is Nothing Then
        Return True
      Else
        If dtrIn.RowState = DataRowState.Added Then
          Return True
        Else
          If BORDINI Then
            If dttECIMP.Select("ec_rigaor = " & dtrIn!ec_riga.ToString).Length = 0 And _
               dttATTIVIT.Select("at_riga = " & dtrIn!ec_riga.ToString).Length = 0 And _
               NTSCStr(dtrIn!ec_flkit) = " " And _
               NTSCDec(dtrIn!ec_quaeva) = 0 And _
               NTSCDec(dtrIn!ec_quapre) = 0 Then
              Return True
            End If
          Else
            If dttECIMP.Select("ec_prriga = " & dtrIn!ec_riga.ToString).Length = 0 And _
               dttLAVCENT.Select("lce_riga = " & dtrIn!ec_riga.ToString).Length = 0 And _
               dttMOVMATR.Select("mma_riga = " & dtrIn!ec_riga.ToString).Length = 0 And _
               dttMOVCONA.Select("mma_riga = " & dtrIn!ec_riga.ToString).Length = 0 And _
               NTSCStr(dtrIn!ec_flkit) = " " And _
               NTSCInt(dtrIn!ec_clriga) = 0 And _
               NTSCInt(dtrIn!ec_cvriga) = 0 And _
               NTSCDec(dtrIn!ec_nprquaeva) = 0 And _
               NTSCStr(dtrIn!xxo_flclavo).Trim = "" And _
               NTSCStr(dtrIn!xxo_flclavo2).Trim = "" And _
               NTSCStr(dtrIn!ec_flcom) = "N" Then
              Return True
            End If

          End If    'If BORDINI Then
        End If
      End If

      Return False

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

  '----------------------

  Public Overridable Function CalcolaValoreRiga(ByVal dtrRow As DataRow, ByVal dPrezzo As Decimal, ByVal dQta As Decimal) As Decimal
    Dim dValore As Decimal = 0
    Dim dScontV As Decimal = 0
    Try
      With dtrRow
        If NTSCDec(!ec_scontv) <> 0 And NTSCStr(dttET.Rows(0)!et_scorpo) = "S" Then
          oCldDocu.Scorporo(NTSCDec(!ec_scontv), NTSCInt(!ec_codiva), dScontV, 0)
        Else
          dScontV = NTSCDec(!ec_scontv)
        End If

        dValore = ArrDbl(dPrezzo * dQta / NTSCDec(!ec_perqta) * (100 - NTSCDec(!ec_scont1)) / 100 * (100 - NTSCDec(!ec_scont2)) / 100 * (100 - NTSCDec(!ec_scont3)) / 100 * _
                  (100 - NTSCDec(!ec_scont4)) / 100 * (100 - NTSCDec(!ec_scont5)) / 100 * (100 - NTSCDec(!ec_scont6)) / 100 * (100 - NTSCDec(!ec_scontp)) / 100 - dScontV, oCldDocu.TrovaNdec(0))
      End With

      'sulle righe di scarico a prod NON CI SONO SCONTI SUI MATERIALI!!! visto che li mettiamo noi
      'Per gli articoli CONAI non deve mai considerare gli sconti di testata
      'Per gli articoli D che rappresentano il riferimento ai doc. di acconto non tiene conto degli sconti di piede/testata
      If dtrRow!ec_tipork.ToString <> "Y" AndAlso dtrRow!ec_tipork.ToString <> "U" AndAlso _
         Not NTSCStr(dtrRow!ec_codart).StartsWith("CONAI", StringComparison.CurrentCultureIgnoreCase) AndAlso _
         (BORDINI OrElse NTSCInt(dtrRow!ec_acnum) = 0) Then
        With dttET.Rows(0)
          dValore = ArrDbl(dValore * (100 - NTSCDec(!et_scont1)) / 100 * (100 - NTSCDec(!et_scont2)) / 100 * (100 - NTSCDec(!et_scopag)) / 100, oCldDocu.TrovaNdec(0))
        End With
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
    Return dValore
  End Function

  Public Overridable Sub RiportaMisuraDaArtico(ByVal dtrEc As DataRow, ByVal bDaUnMis As Boolean)
    Try
      RiportaMisuraDaArtico_ImpostaMisura(dtrEc, lRiportaMisura1DaArtico, 1, bDaUnMis)
      RiportaMisuraDaArtico_ImpostaMisura(dtrEc, lRiportaMisura2DaArtico, 2, bDaUnMis)
      RiportaMisuraDaArtico_ImpostaMisura(dtrEc, lRiportaMisura3DaArtico, 3, bDaUnMis)
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
  Public Overridable Sub RiportaMisuraDaArtico_ImpostaMisura(ByVal dtrEc As DataRow, _
    ByVal lRiportaMisuraDaArtico As Integer, ByVal lNumMisura As Integer, ByVal bDaUnMis As Boolean)
    Dim dttArt As DataTable = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Opzione di registro non impostata
      '--------------------------------------------------------------------------------------------------------------
      If lRiportaMisuraDaArtico = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Se la riga proviene da una evasione, la ignora
      '--------------------------------------------------------------------------------------------------------------
      If BORDINI = False Then
        If NTSCInt(dtrEc!ec_ornum) <> 0 OrElse _
           NTSCInt(dtrEc!ec_npnum) <> 0 OrElse _
           NTSCInt(dtrEc!ec_prnum) <> 0 Then Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se la misura è già impostata non la riporta
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCDec(dtrEc("ec_misura" & lNumMisura)) <> 0) And _
         (lRiportaMisuraDaArtico = 1 Or lRiportaMisuraDaArtico = -1) Then Return
      '--------------------------------------------------------------------------------------------------------------
      dttArt = GetMemDttArti(NTSCStr(dtrEc!ec_codart))
      '--------------------------------------------------------------------------------------------------------------
      Select Case lRiportaMisuraDaArtico
        Case 1, 2 '--- Solo se unmis = formula
          If NTSCStr(dtrEc!ec_unmis) = NTSCStr(dttArt.Rows(0)!ar_um4) Then
            dtrEc("ec_misura" & lNumMisura) = dttArt.Rows(0)("ar_misura" & lNumMisura)
          End If
        Case -1, -2 '--- Sempre
          If bDaUnMis = False Then dtrEc("ec_misura" & lNumMisura) = dttArt.Rows(0)("ar_misura" & lNumMisura)
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  '----------------------


  Public Overridable Function BloccoNotaPrelievoEvasa(ByVal dtrEc As DataRow) As Boolean
    Try
      If NTSCStr(dtrEc!ec_tipork) <> "W" Then Return True
      If NTSCDec(dtrEc!ec_nprquaeva) = 0 OrElse NTSCStr(dtrEc!ec_nprflevas) = "C" Then Return True

      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130536942331068820, "Attezione! Sulle note di prelievo evase a saldo da un documento non è possibile modificare l'unità di misura, i colli e la quantità.")))

      Return False
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




#Region "IMPEGNI/SCARICHI COLLEGATI"
  Public Overridable Sub BeforeColUpdate_CORPOIMP(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'QUESTA ROUTINE VIENE CHIAMATA SIA DA dsShared.Tables("CORPOIMP") 
    'CHE DA dsImpe (dataset compilato solo quando si è dentro alla form degli impegni collegati)
    Dim strErr As String = ""

    Try
      If bInUnload Then Return
      If bNonValidare Then Return

      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValueImp = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      If dtrHT Is Nothing Then
        If BORDINI Then
          If NTSCInt(e.Row!ec_rigaor) > 0 Then
            Dim dtrT() As DataRow = dttEC.Select("ec_riga = " & NTSCInt(e.Row!ec_rigaor))
            If dtrT.Length > 0 Then dtrHT = dtrT(0)
          End If
        Else
          'veboll
          If NTSCInt(e.Row!ec_prriga) > 0 Then
            Dim dtrT() As DataRow = dttEC.Select("ec_riga = " & NTSCInt(e.Row!ec_prriga))
            If dtrT.Length > 0 Then dtrHT = dtrT(0)
          End If
        End If

        If dtrHT Is Nothing Then Throw New NTSException(oApp.Tr(Me, 128740036929009000, "Si sta validando una riga di impegno di produzione senza aver indicato la riga di ordine di produzione a cui deve essere collegata"))
      End If

      '-------------------------------------------------------------
      'se non ho compilato il numero di riga compilo i campi di default
      If BGESTORDLIST Then
        If e.Column.ColumnName <> "ec_riga" Then
          If e.Row!ec_riga.ToString = "0" Then CorpoImpOnAddNewRow(sender, e)
        End If
      Else
        If e.Column.ColumnName <> "ec_ultagg" Then
          If NTSCDate(e.Row!ec_ultagg) = New Date(1900, 1, 1) Then CorpoImpOnAddNewRow(sender, e)
        End If
      End If

      If dtrHT Is Nothing Then
        Throw New NTSException(oApp.Tr(Me, 128587631722968750, "Si sta gestendo una validazione di riga di impegno/scarico a produzione senza aver indicato la rispettiva riga di ordine/carico da produzione"))
      End If

      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BeforeColUpdate_CORPO_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

      strFunction = "BeforeColUpdate_CORPOIMP_" & e.Column.ColumnName.ToLower
      fun = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})


    Catch ex As Exception
      e.ProposedValue = "D"
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Function BeforeColUpdate_CORPOIMP_ec_orserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), True)
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
  Public Overridable Function BeforeColUpdate_CORPOIMP_ec_prserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), True)
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
  Public Overridable Function BeforeColUpdate_CORPOIMP_ec_clserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), True)
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
  Public Overridable Function BeforeColUpdate_CORPOIMP_ec_npserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), True)
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

  Public Overridable Sub AfterColUpdate_CORPOIMP(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'QUESTA ROUTINE VIENE CHIAMATA SIA DA dsShared.Tables("CORPOIMP") 
    'CHE DA dsImpe (dataset compilato solo quando si è dentro alla form degli impegni collegati)
    Try
      If bInUnload Then Return
      If bNonValidare Then Return

      'non valido la colonna se il dato non è cambiato
      If strPrevCelValueImp.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValueImp = strPrevCelValueImp.Remove(strPrevCelValueImp.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChangesET = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_CORPO_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

      strFunction = "AfterColUpdate_CORPOIMP_" & e.Column.ColumnName.ToLower
      fun = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

      '--------------------------------
      If bModTCO And bInValidazCodart = False And e.Column.ColumnName.ToUpper = "EC_CODART" Then
        'sto validando l'articolo: inserisco anche il dettaglio TCO in CORPOTC
        GestisciDettaglioTCO(1, NTSCInt(e.Row!ec_riga), True)
      End If

      '--------------------------------
      'aggiorno i totali della riga
      If bInValidazCodart = False Then
        Select Case e.Column.ColumnName.ToUpper
          Case "EC_CODART", "EC_QUANT", "EC_PREZZO", "EC_CODIVA", _
               "EC_SCONT1", "EC_SCONT2", "EC_SCONT3", "EC_SCONT4", "EC_SCONT5", "EC_SCONT6", _
               "EC_PROVV", "EC_PROVV2", "EC_FLEVAS"
            SettaValoriRiga(e.Row)
          Case "EC_COLLI"
            'se è settata l'opzione per non cambiare la quantità al cambio dei colli 
            'ed il valore è dato dai colli * prezzo, devo ricalcolare il valore di riga
            If bNoModifQuantSuColli And e.Row!ec_umprz.ToString = "S" Then
              SettaValoriRiga(e.Row)
            End If
        End Select
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


  Public Overridable Sub CorpoImpOnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttArt As New DataTable
    Dim bOk As Boolean = False
    Try
      bInOnAddNewRowImp = True
      'prima la nuova riga, come standard
      CorpoOnAddNewRow(sender, e)

      'adesso le specifiche per righe T/Y (scarichi da prod)
      If Not BGESTORDLIST Then
        lCrighe = lCrighe - nIncremContatoreRiga
        lCrigheYT = lCrigheYT + nIncremContatoreRiga
        e.Row!ec_riga = lCrigheYT
      End If

      '--------------------------------
      'se scarichi collegati, memorizzo gli estremi della riga H
      e.Row!ec_tipork = IIf(dttET.Rows(0)!et_tipork.ToString = "H", "Y", "U").ToString
      e.Row!ec_tctaglia = " "
      e.Row!ec_tcindtagl = 0
      If BORDINI = False Then
        'per veboll
        e.Row!ec_prtipo = dttET.Rows(0)!et_tipork
        e.Row!ec_pranno = dttET.Rows(0)!et_anno
        e.Row!ec_prserie = dttET.Rows(0)!et_serie
        e.Row!ec_prnum = dttET.Rows(0)!et_numdoc
        e.Row!ec_prriga = dtrHT!ec_riga
        e.Row!ec_causale = dttET.Rows(0)!et_causcap
        e.Row!ec_causale2 = 0

      Else
        e.Row!ec_datcons = dtrHT!ec_datcons
        e.Row!ec_datcons = CType(oCleComm, CLELBMENU).DateAddEx(strDittaCorrente, "d", ((dRRfenceArtPadre + Int(dFcorrltArtPadre * NTSCDec(dtrHT!ec_quant))) * -1), NTSCDate(dtrHT!ec_datcons), 0)
        e.Row!ec_tiporkor = dttET.Rows(0)!et_tipork
        e.Row!ec_annoor = dttET.Rows(0)!et_anno
        e.Row!ec_serieor = dttET.Rows(0)!et_serie
        e.Row!ec_numordor = dttET.Rows(0)!et_numdoc
        e.Row!ec_rigaor = dtrHT!ec_riga
        If BGESTORDLIST = False Then e.Row!ec_datconsor = dtrHT!ec_datconsor
      End If
      e.Row!ec_magaz = dttET.Rows(0)!et_magimp
      e.Row!ec_codcena = dtrHT!ec_codcena
      e.Row!xxo_codcena = dtrHT!xxo_codcena
      If BORDINI = False Then
        e.Row!ec_coddivi = dtrHT!ec_coddivi
        e.Row!xxo_coddivi = dtrHT!xxo_coddivi
      End If
      e.Row!ec_codcfam = dtrHT!ec_codcfam
      e.Row!xxo_codcfam = dtrHT!xxo_codcfam
      e.Row!ec_commeca = dtrHT!ec_commeca
      e.Row!xxo_commeca = dtrHT!xxo_commeca
      e.Row!ec_subcommeca = dtrHT!ec_subcommeca
      If nDatiniCompMese = -1 Then
        e.Row!ec_datini = NTSCDate(dttET.Rows(0)!et_datdoc).AddDays((NTSCDate(dttET.Rows(0)!et_datdoc).Day - 1) * -1) 'propongo il primo del mese 
        e.Row!ec_datfin = NTSCDate(dttET.Rows(0)!et_datdoc).AddDays((NTSCDate(dttET.Rows(0)!et_datdoc).Day - 1) * -1) 'propongo il primo del mese 
      Else
        e.Row!ec_datini = NTSCDate(dttET.Rows(0)!et_datdoc)
        e.Row!ec_datfin = NTSCDate(dttET.Rows(0)!et_datdoc)
      End If

    Catch ex As Exception
      bNonValidare = False
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    Finally
      bInOnAddNewRowImp = False
    End Try
  End Sub

  Public Overridable Function CorpoImpSetDataTable(ByVal strDitta As String, ByRef dttCorpoImp As DataTable) As Boolean
    Try
      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dttCorpoImp.ColumnChanging, AddressOf BeforeColUpdate_CORPOIMP
      AddHandler dttCorpoImp.ColumnChanged, AddressOf AfterColUpdate_CORPOIMP

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

  Public Overridable Function CorpoImpRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Dim dtrT As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim dttCorpo As DataTable = Nothing

    Try
      'determino se sono stato chiamato da BNORIMPE o no
      If dtrHT Is Nothing Then
        dttCorpo = dsShared.Tables("CORPOIMP")
      Else
        If dsImpe.Tables.Count = 0 Then
          dttCorpo = dsShared.Tables("CORPOIMP")
        Else
          dttCorpo = dsImpe.Tables("CORPOIMP")
        End If
      End If

      'se è una nuova riga cancello il dettaglio TCO: eseguo il test solo se non sto uscendo da bnorgsor
      If bModTCO And bInUnload = False Then
        dtrT = dttCorpo.Select(strFilter)(nRow)
        If dttCorpo.Select(strFilter)(nRow).RowState = DataRowState.Added Then
          GestisciDettaglioTCO(3, NTSCInt(dtrT!ec_riga), True)
        Else
          'potrei essere nella seguente situazione: 
          'ero su una riga salvata con articolo TCO, ho cambiato l'articolo ed ora ripristino: dovrei ricaricare il vecchio dettaglio TCO
          'non posso ripristinare righe se ho cambiato il cod articolo su righe già salvate
          If NTSCStr(dtrT("ec_codart", DataRowVersion.Current)) = NTSCStr(dtrT("ec_codart", DataRowVersion.Original)) Then
            'non ho cambiato l'articolo: non devo modificare il dettaglio TCO
            'ma devo ripristinare le qta precedenti
            If dtrHT Is Nothing Then
              dtrT1 = dttECTC.Select("ec_riga = " & NTSCInt(dttCorpo.Select(strFilter)(nRow)!ec_riga).ToString)
            Else
              dtrT1 = dttECIMPTC.Select("ec_riga = " & NTSCInt(dttCorpo.Select(strFilter)(nRow)!ec_riga).ToString)
            End If
            If dtrT1.Length > 0 Then dtrT1(0).RejectChanges()
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128551595020124000, "Attenzione: con il modulo TAGLIE e COLORI non è possibile ripristinare righe dove è stato cambiato il codice articolo")))
            Return False
          End If
        End If
      End If

      dttCorpo.Select(strFilter)(nRow).RejectChanges()

      Return True
    Catch ex As Exception
    End Try
  End Function


  Public Overridable Function CorpoImpRecordSalva(ByVal nRow As Integer, ByVal bDelete As Boolean, ByRef dtrDeleted As DataRow) As Boolean
    Dim nStato As Integer       '1 = nuova riga, 2 = riga modificata
    Dim dtrT() As DataRow = Nothing
    Dim dttCorpo As DataTable = Nothing

    Try
      If bInUnload Then Return False
      If nRow < 0 Then Return True

      'determino se sono stato chiamato da BNORIMPE o no
      If dtrHT Is Nothing Then
        dttCorpo = dsShared.Tables("CORPOIMP")
      Else
        If dsImpe.Tables.Count = 0 Then
          dttCorpo = dsShared.Tables("CORPOIMP")
        Else
          dttCorpo = dsImpe.Tables("CORPOIMP")
        End If
      End If

      If nRow >= dttCorpo.Rows.Count Then Return False

      If dttCorpo.Rows(nRow).RowState <> DataRowState.Unchanged Then bHasChangesET = True
      If nRow >= dttCorpo.Rows.Count Then Return True 'passo di qui anche se sono sull'ultima riga (quella di addnew) 

      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If bDelete = False Then
        If nRow >= 0 Then

          If dttCorpo.Rows(nRow).RowState = DataRowState.Added Then
            nStato = 1
            If Not CorpoTestPreSalva(dttCorpo, nRow) Then Return False
            If Not CorpoImpBeforeInsert(dttCorpo, nRow) Then Return False
          End If

          If dttCorpo.Rows(nRow).RowState = DataRowState.Modified Then
            nStato = 2
            If Not CorpoTestPreSalva(dttCorpo, nRow) Then Return False
            If Not CorpoImpBeforeUpdate(dttCorpo, nRow) Then Return False
          End If

          'se no delete, fare sempre update se modulo tco per riversare in corpoimptc la qta e la tagli di 
          'corpoimp se in corpoimp c'è articolo tco!!!

          '-------------------------------
          'confermo le modifiche apportate alla riga
          dttCorpo.AcceptChanges()
          dsShared.Tables("CORPOIMP").AcceptChanges()

          If nStato = 1 Then
            CorpoImpAfterInsert(dttCorpo, nRow)

            'se serve assegno il numero di lotto in automatico
            ScaricaLottox(nRow, dttCorpo)
          End If

          If nStato = 2 Then
            CorpoImpAfterUpdate(dttCorpo, nRow)
          End If

        End If
      Else
        If CorpoImpAfterDelete(dtrDeleted) = False Then Return False

        '-------------------------------
        'confermo le modifiche apportate alla griglia
        dttCorpo.AcceptChanges()
        dsShared.Tables("CORPOIMP").AcceptChanges()
      End If    'If Not bDelete Then

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

  Public Overridable Function CorpoImpBeforeInsert(ByRef dttCorpo As DataTable, ByVal nRow As Integer) As Boolean
    Try

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

  Public Overridable Function CorpoImpBeforeUpdate(ByRef dttCorpo As DataTable, ByVal nRow As Integer) As Boolean
    Dim dtrEC As DataRow
    Try
      dtrEC = dttCorpo.Rows(nRow)

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

  Public Overridable Function CorpoImpAfterInsert(ByRef dttCorpo As DataTable, ByVal nRow As Integer) As Boolean
    Try

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

  Public Overridable Function CorpoImpAfterUpdate(ByRef dttCorpo As DataTable, ByVal nRow As Integer) As Boolean
    Try

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


  Public Overridable Function CorpoImpTestPreCancella(ByVal nRow As Integer) As Boolean
    Dim dttCorpo As DataTable = Nothing

    Try
      If nRow < 0 Then Return False

      If BORDINI And BGESTORDLIST = False Then
        'determino se sono stato chiamato da BNORIMPE o no
        If dtrHT Is Nothing Then
          dttCorpo = dsShared.Tables("CORPOIMP")
        Else
          If dsImpe.Tables.Count = 0 Then
            dttCorpo = dsShared.Tables("CORPOIMP")
          Else
            dttCorpo = dsImpe.Tables("CORPOIMP")
          End If
        End If

        If NTSCDec(dttCorpo.Rows(nRow)!ec_quaeva) <> 0 Or NTSCDec(dttCorpo.Rows(nRow)!ec_quapre) <> 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222089531250, "Impossibile eliminare la riga del documento in quanto possiede quantità evasa o prenotata diversa da 0.")))
          Return False
        End If
      End If    'If bGestOrdlist = False Then

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

  Public Overridable Function CorpoImpAfterDelete(ByRef dtrDeleted As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim i As Integer = 0
    Dim l As Integer = 0
    Try
      '-------------------------------
      'Se  attivo il modulo Taglie e colori, cancella l'eventuale record associato in MOTRANSTC
      If bModTCO = True Then
        GestisciDettaglioTCO(4, NTSCInt(dtrDeleted!ec_riga), True)     'cancello CORPOTC
      End If    'If bModTCO = True Then

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


#Region "LAVORAZIONI COLLEGATE (ATTIVIT)"
  Public Overridable Function AttivitSetDataTable(ByVal strDitta As String, ByRef dttAtti As DataTable) As Boolean
    Try
      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dttAtti.ColumnChanging, AddressOf BeforeColUpdate_ATTIVIT
      AddHandler dttAtti.ColumnChanged, AddressOf AfterColUpdate_ATTIVIT

      AddHandler dttAtti.ColumnChanging, AddressOf BeforeColUpdate_ASSRIS
      AddHandler dttAtti.ColumnChanged, AddressOf AfterColUpdate_ASSRIS

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

  Public Overridable Sub BeforeColUpdate_ATTIVIT(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'QUESTA ROUTINE VIENE CHIAMATA SIA DA dsShared.Tables("ATTIVIT") 
    'CHE DA dsTtat (dataset compilato solo quando si è dentro alla form delle lavorazioni collegate)
    Dim strErr As String = ""
    Try
      If bInUnload Then Return
      If bNonValidare Then Return

      If Left(e.Column.ColumnName, 3).ToLower = "as_" Then Return

      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValueAtt = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      If dtrHT Is Nothing Then
        If NTSCInt(e.Row!at_riga) > 0 Then
          Dim dtrT() As DataRow = dttEC.Select("ec_riga = " & NTSCInt(e.Row!at_riga))
          If dtrT.Length > 0 Then dtrHT = dtrT(0)
        End If
        If dtrHT Is Nothing Then Throw New NTSException(oApp.Tr(Me, 128590298522031250, "Si sta validando una riga lavorazione senza aver indicato la riga di ordine di produzione a cui deve essere collegata"))
      End If

      '-------------------------------------------------------------
      'se non ho compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "at_riga" Then
        If NTSCInt(e.Row!at_riga) = 0 Then AttivitOnAddNewRow(sender, e)
      End If

      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BeforeColUpdate_ATTIVIT_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub BeforeColUpdate_ATTIVIT_at_fase(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow

    Try
      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = e.Row.Table.Select("at_riga = " & dtrHT!ec_riga.ToString & " AND at_fase = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128589699265468750, "Fase già esistente.")))
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
  Public Overridable Sub BeforeColUpdate_ATTIVIT_at_codlavo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      BeforeColUpdate_ATTIVIT_LAVCENT_codlavo(sender, e)
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
  Public Overridable Sub BeforeColUpdate_ATTIVIT_at_suserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), False)
      If strTmp <> NTSCStr(e.ProposedValue) Then e.ProposedValue = strTmp

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

  Public Overridable Sub AfterColUpdate_ATTIVIT(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'QUESTA ROUTINE VIENE CHIAMATA SIA DA dsShared.Tables("ATTIVIT") 
    'CHE DA dsTtat (dataset compilato solo quando si è dentro alla form delle lavorazioni collegate)
    If bInUnload Then Return
    If bNonValidare Then Return

    Try
      If Left(e.Column.ColumnName, 3).ToLower = "as_" Then Return

      'non valido la colonna se il dato non è cambiato
      If strPrevCelValueAtt.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValueAtt = strPrevCelValueAtt.Remove(strPrevCelValueAtt.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChangesET = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_ATTIVIT_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

      Select Case e.Column.ColumnName.ToLower
        Case "at_tempesepr", "at_tempattpr"
          CalcolaValoreAssris(e.Row)
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

  Public Overridable Sub AfterColUpdate_ATTIVIT_at_tempattpr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      bInCalcoloTempi60DaTempi100 = True
      If NTSCDec(e.Row!xx_tempattpr) <> ConvOra100Ora60(NTSCDec(e.ProposedValue)) Then e.Row!xx_tempattpr = ConvOra100Ora60(NTSCDec(e.ProposedValue))
      If NTSCDec(e.Row!xx_tempattprm) <> ConvOra60Minuti(ConvOra100Ora60(NTSCDec(e.ProposedValue))) Then e.Row!xx_tempattprm = ConvOra60Minuti(ConvOra100Ora60(NTSCDec(e.ProposedValue)))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------  
    Finally
      bInCalcoloTempi60DaTempi100 = False
    End Try
  End Sub
  Public Overridable Sub AfterColUpdate_ATTIVIT_at_tempesepr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      bInCalcoloTempi60DaTempi100 = True
      If NTSCDec(e.Row!xx_tempesepr) <> ConvOra100Ora60(NTSCDec(e.ProposedValue)) Then e.Row!xx_tempesepr = ConvOra100Ora60(NTSCDec(e.ProposedValue))
      If NTSCDec(e.Row!xx_tempeseprm) <> ConvOra60Minuti(ConvOra100Ora60(NTSCDec(e.ProposedValue))) Then e.Row!xx_tempeseprm = ConvOra60Minuti(ConvOra100Ora60(NTSCDec(e.ProposedValue)))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '-------------------------------------------------------------- 
    Finally
      bInCalcoloTempi60DaTempi100 = False
    End Try
  End Sub
  Public Overridable Sub AfterColUpdate_ATTIVIT_at_tempattees(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      bInCalcoloTempi60DaTempi100 = True
      If NTSCDec(e.Row!xx_tempattees) <> ConvOra100Ora60(NTSCDec(e.ProposedValue)) Then e.Row!xx_tempattees = ConvOra100Ora60(NTSCDec(e.ProposedValue))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '-------------------------------------------------------------- 
    Finally
      bInCalcoloTempi60DaTempi100 = False
    End Try
  End Sub
  Public Overridable Sub AfterColUpdate_ATTIVIT_at_tempesees(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      bInCalcoloTempi60DaTempi100 = True
      If NTSCDec(e.Row!xx_tempesees) <> ConvOra100Ora60(NTSCDec(e.ProposedValue)) Then e.Row!xx_tempesees = ConvOra100Ora60(NTSCDec(e.ProposedValue))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------   
    Finally
      bInCalcoloTempi60DaTempi100 = False
    End Try
  End Sub

  Public Overridable Sub AfterColUpdate_ATTIVIT_xx_tempattpr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bInCalcoloTempi60DaTempi100 Then Return
      If NTSCDec(e.Row!at_tempattpr) <> ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba) Then e.Row!at_tempattpr = ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba)

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
  Public Overridable Sub AfterColUpdate_ATTIVIT_xx_tempesepr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bInCalcoloTempi60DaTempi100 Then Return
      If NTSCDec(e.Row!at_tempesepr) <> ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba) Then e.Row!at_tempesepr = ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba)

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
  Public Overridable Sub AfterColUpdate_ATTIVIT_xx_tempattees(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bInCalcoloTempi60DaTempi100 Then Return
      If NTSCDec(e.Row!at_tempattees) <> ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba) Then e.Row!at_tempattees = ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba)

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
  Public Overridable Sub AfterColUpdate_ATTIVIT_xx_tempesees(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bInCalcoloTempi60DaTempi100 Then Return
      If NTSCDec(e.Row!at_tempesees) <> ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba) Then e.Row!at_tempesees = ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba)

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
  Public Overridable Sub AfterColUpdate_ATTIVIT_xx_tempattprm(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bInCalcoloTempi60DaTempi100 Then Return
      If NTSCDec(e.Row!at_tempattpr) <> ConvOra60Ora100(ConvMinutiOra60(NTSCDec(e.ProposedValue)), oApp.NDecSuTempiDiba) Then e.Row!at_tempattpr = ConvOra60Ora100(ConvMinutiOra60(NTSCDec(e.ProposedValue)), oApp.NDecSuTempiDiba)

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
  Public Overridable Sub AfterColUpdate_ATTIVIT_xx_tempeseprm(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bInCalcoloTempi60DaTempi100 Then Return
      If NTSCDec(e.Row!at_tempesepr) <> ConvOra60Ora100(ConvMinutiOra60(NTSCDec(e.ProposedValue)), oApp.NDecSuTempiDiba) Then e.Row!at_tempesepr = ConvOra60Ora100(ConvMinutiOra60(NTSCDec(e.ProposedValue)), oApp.NDecSuTempiDiba)

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

  Public Overridable Sub AttivitOnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!at_riga = dtrHT!ec_riga
      e.Row!codditt = strDittaCorrente
      e.Row!at_tipork = dttET.Rows(0)!et_tipork
      e.Row!at_anno = dttET.Rows(0)!et_anno
      e.Row!at_serie = dttET.Rows(0)!et_serie
      e.Row!at_numord = dttET.Rows(0)!et_numdoc
      e.Row!at_coddb = dtrHT!ec_codart
      If BGESTORDLIST Then
        e.Row!at_suolprogr = dtrHT!ec_riga
      Else
        e.Row!at_sutipork = dttET.Rows(0)!et_tipork
        e.Row!at_suanno = dttET.Rows(0)!et_anno
        e.Row!at_suserie = dttET.Rows(0)!et_serie
        e.Row!at_sunumord = dttET.Rows(0)!et_numdoc
        e.Row!at_suriga = dtrHT!ec_riga
      End If
      e.Row!at_qtapr = dtrHT!ec_quant
      e.Row!at_dtminima = NTSCDate(IntSetDate("31/12/2099"))
      e.Row!at_datcons = NTSCDate(IntSetDate("31/12/2099"))
      e.Row!xx_tempattpr = 0
      e.Row!xx_tempesepr = 0
      e.Row!xx_tempattprm = 0
      e.Row!xx_tempeseprm = 0
      e.Row!xx_tempattees = 0
      e.Row!xx_tempesees = 0

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

  Public Overridable Function AttivitRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Dim dtrT As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim dttAtti As DataTable = Nothing

    Try
      'determino se sono stato chiamato da BNORTTAT o no
      If dtrHT Is Nothing Then
        dttAtti = dsShared.Tables("ATTIVIT")
      Else
        If dsTtat.Tables.Count = 0 Then
          dttAtti = dsShared.Tables("ATTIVIT")
        Else
          dttAtti = dsTtat.Tables("ATTIVIT")
        End If
      End If

      dttAtti.Select(strFilter)(nRow).RejectChanges()

      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Function AttivitRecordSalva(ByVal nRow As Integer, ByVal bDelete As Boolean, ByRef dtrDeleted As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim dttAtti As DataTable = Nothing

    Try
      If bInUnload Then Return False
      If nRow < 0 Then Return True

      'determino se sono stato chiamato da BNORTTAT o no
      If dtrHT Is Nothing Then
        dttAtti = dsShared.Tables("ATTIVIT")
      Else
        If dsTtat.Tables.Count = 0 Then
          dttAtti = dsShared.Tables("ATTIVIT")
        Else
          dttAtti = dsTtat.Tables("ATTIVIT")
        End If
      End If

      If dttAtti.Rows(nRow).RowState <> DataRowState.Unchanged And _
         dttAtti.Rows(nRow).RowState <> DataRowState.Deleted Then
        If dttAtti.Select("at_tipork = " & CStrSQL(dttAtti.Rows(nRow)!at_tipork) & _
                           " AND at_anno = " & NTSCInt(dttAtti.Rows(nRow)!at_anno) & _
                           " AND at_serie = " & CStrSQL(dttAtti.Rows(nRow)!at_serie) & _
                           " AND at_numord = " & NTSCInt(dttAtti.Rows(nRow)!at_numord) & _
                           " AND at_riga = " & NTSCInt(dttAtti.Rows(nRow)!at_riga) & _
                           " AND at_fase=" & NTSCInt(dttAtti.Rows(nRow)!at_fase), "", DataViewRowState.Unchanged).Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129179712453710999, _
          "Impossibile importare la fase di lavorazione |" & NTSCInt(dttAtti.Rows(nRow)!at_fase) & _
          "| a causa di duplicazione del numero di fase.")))
          Return False
        End If
      End If

      If dttAtti.Rows(nRow).RowState <> DataRowState.Unchanged Then bHasChangesET = True
      If nRow >= dttAtti.Rows.Count Then Return True 'passo di qui anche se sono sull'ultima riga (quella di addnew) 


      If bDelete = False Then
        '----------------------------------------
        'controlli pre-salvataggio (solo se non è una delete)

        Try
          If dttAtti.Columns.Contains("as_fase") Then dttAtti.Rows(nRow)!as_fase = dttAtti.Rows(nRow)!at_fase 'devono sempre esere uguali!!!
        Catch ex As Exception
          'se si verifica l'errore è perchè non sono stato chiamato da BNORTTAT: il cambio della fase su assris dovrà essere fatto da codice a mano!!!!
        End Try
      Else
        If AttivitAfterDelete(dtrDeleted) = False Then Return False
      End If    'If Not bDelete Then

      '-------------------------------
      'confermo le modifiche apportate alla griglia
      dttAtti.AcceptChanges()
      dsShared.AcceptChanges()

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
  Public Overridable Function AttivitTestPreCancella(ByVal nRow As Integer) As Boolean
    Dim dttAtti As DataTable = Nothing

    Try
      If nRow < 0 Then Return False

      'determino se sono stato chiamato da BNORTTAT o no
      If dtrHT Is Nothing Then
        dttAtti = dsShared.Tables("ATTIVIT")
      Else
        If dsTtat.Tables.Count = 0 Then
          dttAtti = dsShared.Tables("ATTIVIT")
        Else
          dttAtti = dsTtat.Tables("ATTIVIT")
        End If
      End If

      If Math.Round(NTSCDec(dttAtti.Rows(nRow)!at_qtaes), 3) <> 0 Or _
         Math.Round(NTSCDec(dttAtti.Rows(nRow)!at_tempattees), 4) <> 0 Or _
         Math.Round(NTSCDec(dttAtti.Rows(nRow)!at_tempesees), 4) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128589668028593750, "Impossibile eliminare la riga del documento in quanto possiede quantità evasa o prenotata diversa da 0.")))
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
  Public Overridable Function AttivitAfterDelete(ByRef dtrDeleted As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dttAssi As DataTable
    Try
      '-----------------------
      'devo cancellare le assris collegate alla riga di attivit cancellata
      'determino se sono stato chiamato da BNORTTAT o no
      dttAssi = dsTtas.Tables("ATTIVIT")
      If dttAssi Is Nothing Then dttAssi = dsShared.Tables("ASSRIS")
      If Not dttAssi Is Nothing Then
        dtrT = dttAssi.Select("as_riga = " & dtrDeleted!at_riga.ToString & " AND as_fase = " & dtrDeleted!at_fase.ToString)
        If dtrT.Length > 0 Then
          dtrT(0).Delete()
        End If
        dttAssi.AcceptChanges()
        dsTtas.AcceptChanges()
        dsTtat.AcceptChanges()
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

#Region "CENTRI DI LAVORO (ASSRIS)"
  Public Overridable Sub BeforeColUpdate_ASSRIS(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return
    Dim strErr As String = ""
    Try
      If Left(e.Column.ColumnName, 3).ToLower <> "as_" Then Return

      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValueAss = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'se non ho compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "as_riga" Then
        If NTSCInt(e.Row!as_riga) = 0 Then AssrisOnAddNewRow(sender, e)
      End If

      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BeforeColUpdate_ASSRIS_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub BeforeColUpdate_ASSRIS_as_codcent(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Dim dtrT() As DataRow = Nothing

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcent = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCENT", "N", strTmp, dttTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740036978305000, "Codice centro di lavoro inesistente")))
          Return
        Else
          e.Row!xx_codcent = strTmp
          e.Row!as_controp = dttTmp.Rows(0)!tb_ccodcontr
          e.Row!as_codcena = dttTmp.Rows(0)!tb_ccodcena

          If Not bTerzista Then
            e.Row!as_cmacora = dttTmp.Rows(0)!tb_cmacora
            e.Row!as_pagaora = dttTmp.Rows(0)!tb_pagaora
            e.Row!as_pagaoratt = dttTmp.Rows(0)!tb_pagaoratt
            e.Row!as_cmacoratt = dttTmp.Rows(0)!tb_cmacoratt
            e.Row!as_valorev = 0
            Try
              e.Row!as_valmo = (NTSCDec(e.Row!at_tempattpr) * NTSCDec(dttTmp.Rows(0)!tb_pagaoratt)) + (NTSCDec(e.Row!at_tempesepr) * NTSCDec(dttTmp.Rows(0)!tb_pagaora))
              e.Row!as_valore = (NTSCDec(e.Row!at_tempattpr) * NTSCDec(dttTmp.Rows(0)!tb_cmacoratt)) + (NTSCDec(e.Row!at_tempesepr) * NTSCDec(dttTmp.Rows(0)!tb_cmacora))
            Catch ex As Exception
              'se da errore non sono stato chiamato da BNORTTATT devo trovare la riga di attivit e farmi restituire i tempi
              dtrT = dsShared.Tables("ATTIVIT").Select("at_riga = " & e.Row!as_riga.ToString & " AND at_fase = " & e.Row!as_fase.ToString)
              If dtrT.Length > 0 Then
                e.Row!as_valmo = (NTSCDec(dtrT(0)!at_tempattpr) * NTSCDec(dttTmp.Rows(0)!tb_pagaoratt)) + (NTSCDec(dtrT(0)!at_tempesepr) * NTSCDec(dttTmp.Rows(0)!tb_pagaora))
                e.Row!as_valore = (NTSCDec(dtrT(0)!at_tempattpr) * NTSCDec(dttTmp.Rows(0)!tb_cmacoratt)) + (NTSCDec(dtrT(0)!at_tempesepr) * NTSCDec(dttTmp.Rows(0)!tb_cmacora))
              End If
            End Try
          Else
            e.Row!as_cmacora = 0
            e.Row!as_pagaora = 0
            e.Row!as_pagaoratt = 0
            e.Row!as_cmacoratt = 0
            e.Row!as_valorev = 0
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
  Public Overridable Sub BeforeColUpdate_ASSRIS_as_controp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_controp = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOVE", "N", strTmp, dttTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740037043201000, "Codice contropartita inesistente")))
          Return
        Else
          e.Row!xx_controp = strTmp
          e.Row!as_contocontr = dttTmp.Rows(0)!tb_concova
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
  Public Overridable Sub BeforeColUpdate_ASSRIS_as_codcena(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcena = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCENA", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740037094525000, "Codice centro C.A. inesistente")))
          Return
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

  Public Overridable Sub BeforeColUpdate_ASSRIS_as_valmo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bTerzista And NTSCDec(e.ProposedValue) <> 0 Then
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740037148813000, "Il costo tot. della manodopera dei terzisti deve essere sempre uguale a 0.")))
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

  Public Overridable Sub BeforeColUpdate_ASSRIS_as_cmacora(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bTerzista Then
        If NTSCDec(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590468004218750, "Il costo macchina orario per i terzisti deve essere sempre uguale a 0.")))
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
  Public Overridable Sub BeforeColUpdate_ASSRIS_as_pagaora(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bTerzista Then
        If NTSCDec(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590467809218750, "Il costo manodopera orario per i terzisti deve essere sempre uguale a 0.")))
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
  Public Overridable Sub BeforeColUpdate_ASSRIS_as_pagaoratt(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bTerzista Then
        If NTSCDec(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590467669218750, "Il costo manodopera orario per i terzisti deve essere sempre uguale a 0.")))
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
  Public Overridable Sub BeforeColUpdate_ASSRIS_as_cmacoratt(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bTerzista Then
        If NTSCDec(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590467571406250, "Il costo macchina orario per i terzisti deve essere sempre uguale a 0.")))
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

  Public Overridable Sub AfterColUpdate_ASSRIS(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return
    Try
      If Left(e.Column.ColumnName, 3).ToLower <> "as_" Then Return

      'non valido la colonna se il dato non è cambiato
      If strPrevCelValueAss.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValueAss = strPrevCelValueAss.Remove(strPrevCelValueAss.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChangesET = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_ASSRIS_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

      Select Case e.Column.ColumnName.ToLower
        Case "as_cmacora", "as_pagaora", "as_pagaoratt", "as_cmacoratt"
          CalcolaValoreAssris(e.Row)
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

  Public Overridable Sub AssrisOnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!as_riga = dtrHT!ec_riga
      e.Row!codditt = strDittaCorrente
      e.Row!as_tipork = dttET.Rows(0)!et_tipork
      e.Row!as_anno = dttET.Rows(0)!et_anno
      e.Row!as_serie = dttET.Rows(0)!et_serie
      e.Row!as_numord = dttET.Rows(0)!et_numdoc
      Try
        e.Row!as_fase = e.Row!at_fase
      Catch ex As Exception
        'se non si verifica errore vuol dire che sono stato chiamato da BNORTTAT, dove su una griglia c'è si attivit che assris
        'altrimenti la fase deve essere inserita manualmente (ad esempio dalla scrivirighedadiba)
      End Try
      e.Row!as_percent = 100
      If bTerzista Then
        CalcolaValoreAssris(e.Row)
      Else
        e.Row!as_valore = 0
        e.Row!as_valorev = 0
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

  Public Overridable Function CalcolaValoreAssris(ByRef dtrIn As DataRow) As Boolean
    Dim dtrAtti As DataRow
    Dim dtrT() As DataRow = Nothing
    Try
      'Chiamato sia dalla TrattaDBDoc che dalla maschera delle lavorazioni collegate (sempre tramite la AfterColUpdate).
      'Dalla trattaDBDoc mancano dei campi e quindi non va fatto il calcolo, se i campi mancano evito di procedere con l'operazione.
      If Not dtrIn.Table.Columns.Contains("as_cmacoratt") Then Return True

      If bTerzista Then
        dtrIn!as_valorev = 0
        If dtrHT!ec_umprz.ToString <> "S" Then
          dtrIn!as_valore = ArrDbl(NTSCDec(dtrHT!ec_prezzo) * NTSCDec(dtrHT!ec_quant) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(0))
          dtrIn!as_valorev = ArrDbl(NTSCDec(dtrHT!ec_prezvalc) * NTSCDec(dtrHT!ec_quant) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(0))
        Else
          dtrIn!as_valore = ArrDbl(NTSCDec(dtrHT!ec_prezzo) * NTSCDec(dtrHT!ec_colli) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(0))
          dtrIn!as_valorev = ArrDbl(NTSCDec(dtrHT!ec_prezvalc) * NTSCDec(dtrHT!ec_colli) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(0))
        End If
      Else
        Try
          Dim i As Integer = NTSCInt(dtrIn!at_fase)
          dtrAtti = dtrIn
        Catch ex As Exception
          'se non si verifica errore vuol dire che sono stato chiamato da BNORTTAT, dove su una griglia c'è si attivit che assris
          'altrimenti la fase deve essere inserita manualmente (ad esempio dalla scrivirighedadiba)
          dtrAtti = dsShared.Tables("ATTIVIT").Select("at_riga = " & dtrIn!as_riga.ToString & " AND at_fase = " & dtrIn!as_fase.ToString)(0)
        End Try

        dtrIn!as_valore = (NTSCDec(dtrAtti!at_tempattpr) * NTSCDec(dtrIn!as_cmacoratt)) + (NTSCDec(dtrAtti!at_tempesepr) * NTSCDec(dtrIn!as_cmacora))
        dtrIn!as_valmo = (NTSCDec(dtrAtti!at_tempattpr) * NTSCDec(dtrIn!as_pagaoratt)) + (NTSCDec(dtrAtti!at_tempesepr) * NTSCDec(dtrIn!as_pagaora))
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

#Region "LAVORAZIONI COLLEGATE (LAVCENT)"
  Public Overridable Function LavcentSetDataTable(ByVal strDitta As String, ByRef dttLavc As DataTable) As Boolean
    Try
      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dttLavc.ColumnChanging, AddressOf BeforeColUpdate_LAVCENT
      AddHandler dttLavc.ColumnChanged, AddressOf AfterColUpdate_LAVCENT

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

  Public Overridable Sub BeforeColUpdate_LAVCENT(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'QUESTA ROUTINE VIENE CHIAMATA SIA DA dsShared.Tables("ATTIVIT") 
    'CHE DA dsTtat (dataset compilato solo quando si è dentro alla form delle lavorazioni collegate)
    Dim strErr As String = ""
    Try
      If bInUnload Then Return
      If bNonValidare Then Return

      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValueLav = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      If dtrHT Is Nothing Then
        If NTSCInt(e.Row!lce_riga) > 0 Then
          Dim dtrT() As DataRow = dttEC.Select("ec_riga = " & NTSCInt(e.Row!lce_riga))
          If dtrT.Length > 0 Then dtrHT = dtrT(0)
        End If
        If dtrHT Is Nothing Then Throw New NTSException(oApp.Tr(Me, 128656807385312500, "Si sta validando una riga lavorazione senza aver indicato la riga di ordine di produzione a cui deve essere collegata"))
      End If

      '-------------------------------------------------------------
      'se non ho compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "lce_riga" Then
        If NTSCInt(e.Row!lce_riga) = 0 Then LavcentOnAddNewRow(sender, e)
      End If

      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BeforeColUpdate_LAVCENT_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_rigaa(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim lRow As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim bOk As Boolean = False
    Try
      dtrT1 = e.Row.Table.Select("lce_rigaa = " & NTSCInt(e.ProposedValue).ToString)
      If dtrT1.Length = 0 Then
        dtrT1 = dttLAVCENT.Select("lce_rigaa = " & NTSCInt(e.ProposedValue).ToString)
      End If
      If dtrT1.Length > 0 Or NTSCInt(e.ProposedValue) = 0 Then
        If bInOnAddNewRowLavcent = False Then
          If NTSCInt(e.ProposedValue) <> 0 And dtrT1.Length = 1 Then
            If dtrT1(0).Equals(e.Row) Then bOk = True
          End If
          If bOk = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129774215666940388, "Numero di riga già utilizzato o uguale a 0")))
            If e.Row.RowState = DataRowState.Added Then
              'ridetermino il numero di riga
              dtrT = e.Row.Table.Select("", "lce_rigaa DESC")
              If dtrT.Length > 0 Then lRow = NTSCInt(dtrT(0)!lce_rigaa)
              lRow += 1
              e.ProposedValue = lRow
            Else
              e.ProposedValue = e.Row!lce_rigaa
            End If
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
  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_codcent(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Dim dtrT() As DataRow = Nothing

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcent = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCENT", "N", strTmp, dttTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590444369062500, "Codice centro di lavoro inesistente")))
          Return
        Else
          'Se terzista controlla che sia conguente
          If bTerzista Then
            If NTSCInt(dttTmp.Rows(0)!tb_magterz) <> NTSCInt(dttET.Rows(0)!et_magimp) Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128691620309843750, "Il centro deve riferirsi al terzista |" & NTSCInt(dttET.Rows(0)!et_conto).ToString & "| che esegue la lavorazione.")))
              Return
            End If
          End If
          e.Row!xx_codcent = strTmp
          e.Row!xx_magterz = dttTmp.Rows(0)!tb_magterz
          e.Row!lce_controp = dttTmp.Rows(0)!tb_ccodcontr
          e.Row!lce_codcena = dttTmp.Rows(0)!tb_ccodcena

          If Not bTerzista Then
            e.Row!lce_cmacora = dttTmp.Rows(0)!tb_cmacora
            e.Row!lce_pagaora = dttTmp.Rows(0)!tb_pagaora
            e.Row!lce_pagaoratt = dttTmp.Rows(0)!tb_pagaoratt
            e.Row!lce_cmacoratt = dttTmp.Rows(0)!tb_cmacoratt
            e.Row!lce_valorev = 0
            e.Row!lce_valmo = (NTSCDec(e.Row!lce_tempatte) * NTSCDec(dttTmp.Rows(0)!tb_pagaoratt)) + (NTSCDec(e.Row!lce_tempese) * NTSCDec(dttTmp.Rows(0)!tb_pagaora))
            e.Row!lce_valore = (NTSCDec(e.Row!lce_tempatte) * NTSCDec(dttTmp.Rows(0)!tb_cmacoratt)) + (NTSCDec(e.Row!lce_tempese) * NTSCDec(dttTmp.Rows(0)!tb_cmacora))
          Else
            e.Row!lce_cmacora = 0
            e.Row!lce_pagaora = 0
            e.Row!lce_pagaoratt = 0
            e.Row!lce_cmacoratt = 0
            e.Row!lce_valorev = 0
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
  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_codlavo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      BeforeColUpdate_ATTIVIT_LAVCENT_codlavo(sender, e)
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
  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_controp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_controp = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOVE", "N", strTmp, dttTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590444783125000, "Codice contropartita inesistente")))
          Return
        Else
          e.Row!xx_controp = strTmp
          e.Row!lce_contocontr = dttTmp.Rows(0)!tb_concova
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
  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_codcena(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcena = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCENA", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590461738437500, "Codice centro C.A. inesistente")))
          Return
        Else
          e.Row!xx_codcena = strTmp
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
  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_codcfam(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      If NTSCStr(e.ProposedValue) = "" Then e.ProposedValue = " "
      If NTSCStr(e.ProposedValue) = " " Then
        e.Row!xx_codcfam = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCFAM", "S", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128691588093593750, "Codice linea/famiglia inesistente")))
          Return
        Else
          e.Row!xx_codcfam = strTmp
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
  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!xx_codart = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128691588426406250, "Codice articolo inesistente")))
          Return
        Else
          e.Row!xx_codart = strTmp
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
  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_codcope(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcope = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOPE", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128691589380312500, "Codice operaio inesistente")))
          Return
        Else
          e.Row!xx_codcope = strTmp
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
  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_commeca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_commeca = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "COMMESS", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128691590397343750, "Codice commessa inesistente")))
          Return
        Else
          e.Row!xx_commeca = strTmp
        End If
      End If

      If bDisabilitaControlliSubcomm = False Then
        'al cambio di commessa assero sempre la subcomm
        e.Row!lce_subcommeca = ""
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
  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_subcommeca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As DataTable = New DataTable
    Try
      If NTSCStr(e.ProposedValue) = "" Then e.ProposedValue = " "
      If bDisabilitaControlliSubcomm = False Then
        If NTSCStr(e.ProposedValue) <> " " Then
          oCldDocu.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "SUBCOMM", "S", "", dttTmp, e.Row!lce_commeca.ToString)
          If dttTmp.Rows.Count = 0 Then
            e.ProposedValue = " "
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130420451077238608, "Codice sottocommessa inesistente")))
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

  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_valmo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bTerzista And NTSCDec(e.ProposedValue) <> 0 Then
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590466017031250, "Il costo tot. della manodopera dei terzisti deve essere sempre uguale a 0.")))
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

  Public Overridable Sub AfterColUpdate_LAVCENT_lce_tempatte(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      bInCalcoloTempi60DaTempi100 = True
      If NTSCDec(e.Row!xx_tempatte) <> ConvOra100Ora60(NTSCDec(e.ProposedValue)) Then e.Row!xx_tempatte = ConvOra100Ora60(NTSCDec(e.ProposedValue))
      If NTSCDec(e.Row!xx_tempattem) <> ConvOra60Minuti(ConvOra100Ora60(NTSCDec(e.ProposedValue))) Then e.Row!xx_tempattem = ConvOra60Minuti(ConvOra100Ora60(NTSCDec(e.ProposedValue)))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------    
    Finally
      bInCalcoloTempi60DaTempi100 = False
    End Try
  End Sub
  Public Overridable Sub AfterColUpdate_LAVCENT_lce_tempese(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      bInCalcoloTempi60DaTempi100 = True
      If NTSCDec(e.Row!xx_tempese) <> ConvOra100Ora60(NTSCDec(e.ProposedValue)) Then e.Row!xx_tempese = ConvOra100Ora60(NTSCDec(e.ProposedValue))
      If NTSCDec(e.Row!xx_tempesem) <> ConvOra60Minuti(ConvOra100Ora60(NTSCDec(e.ProposedValue))) Then e.Row!xx_tempesem = ConvOra60Minuti(ConvOra100Ora60(NTSCDec(e.ProposedValue)))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------    
    Finally
      bInCalcoloTempi60DaTempi100 = False
    End Try
  End Sub
  Public Overridable Sub AfterColUpdate_LAVCENT_xx_tempatte(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bInCalcoloTempi60DaTempi100 Then Return
      If NTSCDec(e.Row!lce_tempatte) <> ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba) Then e.Row!lce_tempatte = ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba)

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
  Public Overridable Sub AfterColUpdate_LAVCENT_xx_tempese(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bInCalcoloTempi60DaTempi100 Then Return
      If NTSCDec(e.Row!lce_tempese) <> ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba) Then e.Row!lce_tempese = ConvOra60Ora100(NTSCDec(e.ProposedValue), oApp.NDecSuTempiDiba)

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
  Public Overridable Sub AfterColUpdate_LAVCENT_xx_tempattem(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bInCalcoloTempi60DaTempi100 Then Return
      If NTSCDec(e.Row!lce_tempatte) <> ConvOra60Ora100(ConvMinutiOra60(NTSCDec(e.ProposedValue)), oApp.NDecSuTempiDiba) Then e.Row!lce_tempatte = ConvOra60Ora100(ConvMinutiOra60(NTSCDec(e.ProposedValue)), oApp.NDecSuTempiDiba)

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
  Public Overridable Sub AfterColUpdate_LAVCENT_xx_tempesem(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If bInCalcoloTempi60DaTempi100 Then Return
      If NTSCDec(e.Row!lce_tempese) <> ConvOra60Ora100(ConvMinutiOra60(NTSCDec(e.ProposedValue)), oApp.NDecSuTempiDiba) Then e.Row!lce_tempese = ConvOra60Ora100(ConvMinutiOra60(NTSCDec(e.ProposedValue)), oApp.NDecSuTempiDiba)

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

  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_qtaes(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dOldTotQuantLav As Decimal = 0
    Dim dNewTotQuantLav As Decimal = 0
    Try
      If bModifTempiLavSuQtaScart Then
        dOldTotQuantLav = NTSCDec(e.Row!lce_qtaes) + NTSCDec(e.Row!lce_qtascart)
        dNewTotQuantLav = NTSCDec(e.ProposedValue) + NTSCDec(e.Row!lce_qtascart)
      Else
        dOldTotQuantLav = NTSCDec(e.Row!lce_qtaes)
        dNewTotQuantLav = NTSCDec(e.ProposedValue)
      End If
      If dOldTotQuantLav <> 0 Then
        'Cambio della quantità non ha effetto sull'attrezzaggio
        e.Row!lce_tempese = ArrDbl((NTSCDec(e.Row!lce_tempese) / dOldTotQuantLav) * dNewTotQuantLav, 4)
      End If

      'Rettifica il flag evasione se si cambia la qtà
      If NTSCInt(e.Row!lce_ornum) > 0 Then
        If ArrDbl(NTSCDec(e.ProposedValue) + NTSCDec(e.Row!lce_qtascart), 3) >= ArrDbl(NTSCDec(e.Row!lce_qtadisimp), 3) Then
          '
        Else
          e.Row!lce_flevas = "C"
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
  Public Overridable Sub BeforeColUpdate_LAVCENT_lce_qtascart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dOldTotQuantLav As Decimal = 0
    Dim dNewTotQuantLav As Decimal = 0
    Try
      If bModifTempiLavSuQtaScart Then
        dOldTotQuantLav = NTSCDec(e.Row!lce_qtaes) + NTSCDec(e.Row!lce_qtascart)
        dNewTotQuantLav = NTSCDec(e.Row!lce_qtaes) + NTSCDec(e.ProposedValue)

        If dOldTotQuantLav <> 0 Then
          'Cambio della quantità non ha effetto sull'attrezzaggio
          e.Row!lce_tempese = ArrDbl((NTSCDec(e.Row!lce_tempese) / dOldTotQuantLav) * dNewTotQuantLav, 4)
        End If

      End If

      'Rettifica il flag evasione se si cambia la qtà
      If NTSCInt(e.Row!lce_ornum) > 0 Then
        If ArrDbl(NTSCDec(e.Row!lce_qtaes) + NTSCDec(e.ProposedValue), 3) >= ArrDbl(NTSCDec(e.Row!lce_qtadisimp), 3) Then
          '
        Else
          e.Row!lce_flevas = "C"
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

  Public Overridable Sub AfterColUpdate_LAVCENT(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'QUESTA ROUTINE VIENE CHIAMATA SIA DA dsShared.Tables("ATTIVIT") 
    'CHE DA dsTtat (dataset compilato solo quando si è dentro alla form delle lavorazioni collegate)
    If bInUnload Then Return
    If bNonValidare Then Return

    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValueLav.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValueLav = strPrevCelValueLav.Remove(strPrevCelValueLav.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChangesET = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_LAVCENT_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

      Select Case e.Column.ColumnName.ToLower
        Case "lce_tempese", "lce_tempatte", "lce_cmacora", "lce_pagaora", "lce_pagaoratt", "lce_cmacoratt"
          CalcolaValoreLavcent(e.Row)
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

  Public Overridable Sub AfterColUpdate_LAVCENT_lce_commeca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try

      If bDisabilitaControlliSubcomm = False Then
        'al cambio di commessa assero sempre la subcomm
        e.Row!lce_subcommeca = " "
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

  Public Overridable Function BeforeColUpdate_LAVCENT_lce_orserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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
  Public Overridable Function BeforeColUpdate_LAVCENT_lce_gserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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

  Public Overridable Sub LavcentOnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrT() As DataRow = Nothing
    Dim lRow As Integer = 0
    Try
      bInOnAddNewRowLavcent = True
      '---------------------------
      'determino il numero di riga
      dtrT = e.Row.Table.Select("", "lce_rigaa DESC")
      If dtrT.Length > 0 Then lRow = NTSCInt(dtrT(0)!lce_rigaa)
      lRow += 1

      e.Row!lce_riga = dtrHT!ec_riga
      e.Row!codditt = strDittaCorrente
      e.Row!lce_tipork = dttET.Rows(0)!et_tipork
      e.Row!lce_anno = dttET.Rows(0)!et_anno
      e.Row!lce_serie = dttET.Rows(0)!et_serie
      e.Row!lce_numdoc = dttET.Rows(0)!et_numdoc
      e.Row!lce_rigaa = lRow
      e.Row!lce_codart = dtrHT!ec_codart
      e.Row!lce_qtaes = dtrHT!ec_quant

      If bTerzista Then
        CalcolaValoreLavcent(e.Row)
      Else
        e.Row!lce_valore = 0
        e.Row!lce_valorev = 0
      End If
      e.Row!lce_commeca = dtrHT!ec_commeca
      e.Row!lce_subcommeca = dtrHT!ec_subcommeca

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    Finally
      bInOnAddNewRowLavcent = False
    End Try
  End Sub

  Public Overridable Function LavcentRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Dim dtrT As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim dttLavc As DataTable = Nothing

    Try
      'determino se sono stato chiamato da BNORTTAT o no
      If dtrHT Is Nothing Then
        dttLavc = dsShared.Tables("LAVCENT")
      Else
        If dsLavc.Tables.Count = 0 Then
          dttLavc = dsShared.Tables("LAVCENT")
        Else
          dttLavc = dsLavc.Tables("LAVCENT")
        End If
      End If

      dttLavc.Select(strFilter)(nRow).RejectChanges()

      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Function LavcentRecordSalva(ByVal nRow As Integer, ByVal bDelete As Boolean, ByRef dtrDeleted As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim dttLavc As DataTable = Nothing

    Try
      If bInUnload Then Return False
      If nRow < 0 Then Return True

      'determino se sono stato chiamato da BNORTTAT o no
      If dtrHT Is Nothing Then
        dttLavc = dsShared.Tables("LAVCENT")
      Else
        If dsLavc.Tables.Count = 0 Then
          dttLavc = dsShared.Tables("LAVCENT")
        Else
          dttLavc = dsLavc.Tables("LAVCENT")
        End If
      End If

      If dttLavc.Rows(nRow).RowState <> DataRowState.Unchanged Then bHasChangesET = True
      If nRow >= dttLavc.Rows.Count Then Return True 'passo di qui anche se sono sull'ultima riga (quella di addnew) 


      If bDelete = False Then
        '----------------------------------------
        'controlli pre-salvataggio (solo se non è una delete)
        dtrT = dttLavc.Select("", "", DataViewRowState.ModifiedCurrent Or DataViewRowState.Added)
        If dtrT.Length > 0 Then If Not LavcentTestPreSalva(dtrT(0)) Then Return False
      Else
        If LavcentAfterDelete(dtrDeleted) = False Then Return False
      End If    'If Not bDelete Then

      '-------------------------------
      'confermo le modifiche apportate alla griglia
      dttLavc.AcceptChanges()
      dsShared.AcceptChanges()

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
  Public Overridable Function LavcentTestPreSalva(ByRef dtrT As DataRow) As Boolean
    Dim dttLavc As DataTable = Nothing

    Try
      If NTSCInt(dtrT!lce_codcent) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128691612324843750, "Codice centro obbligatorio")))
        Return False
      End If
      If dtrT.Table.Select("", "", DataViewRowState.CurrentRows).Length > 1 And bTerzista Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128691607884843750, "Per i terzisti viene gestita una sola fase di lavorazione.")))
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
  Public Overridable Function LavcentTestPreCancella(ByVal nRow As Integer) As Boolean
    Dim dttLavc As DataTable = Nothing

    Try
      If nRow < 0 Then Return False

      'determino se sono stato chiamato da BNVELAVC o no
      If dtrHT Is Nothing Then
        dttLavc = dsShared.Tables("LAVCENT")
      Else
        If dsLavc.Tables.Count = 0 Then
          dttLavc = dsShared.Tables("LAVCENT")
        Else
          dttLavc = dsLavc.Tables("LAVCENT")
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
  Public Overridable Function LavcentAfterDelete(ByRef dtrDeleted As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dttLavc As DataTable = Nothing
    Try

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


  Public Overridable Function CalcolaValoreLavcent(ByRef dtrIn As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Try

      If bTerzista Then
        dtrIn!lce_valorev = 0
        If dtrHT!ec_umprz.ToString <> "S" Then
          dtrIn!lce_valore = ArrDbl(NTSCDec(dtrHT!ec_prezzo) * NTSCDec(dtrHT!ec_quant) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(0))
          dtrIn!lce_valorev = ArrDbl(NTSCDec(dtrHT!ec_prezvalc) * NTSCDec(dtrHT!ec_quant) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(0))
        Else
          dtrIn!lce_valore = ArrDbl(NTSCDec(dtrHT!ec_prezzo) * NTSCDec(dtrHT!ec_colli) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(0))
          dtrIn!lce_valorev = ArrDbl(NTSCDec(dtrHT!ec_prezvalc) * NTSCDec(dtrHT!ec_colli) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(0))
        End If
      Else
        dtrIn!lce_valore = ArrDbl((NTSCDec(dtrIn!lce_tempese) * NTSCDec(dtrIn!lce_cmacora)) + (NTSCDec(dtrIn!lce_tempatte) * NTSCDec(dtrIn!lce_cmacoratt)), oCldDocu.TrovaNdec(0))
        dtrIn!lce_valmo = ArrDbl((NTSCDec(dtrIn!lce_tempese) * NTSCDec(dtrIn!lce_pagaora)) + (NTSCDec(dtrIn!lce_tempatte) * NTSCDec(dtrIn!lce_pagaoratt)), oCldDocu.TrovaNdec(0))
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

  Public Overridable Sub BeforeColUpdate_ATTIVIT_LAVCENT_codlavo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Dim dPrezzo As Decimal = 0
    Dim dPrezvalc As Decimal = 0
    Dim dValore As Decimal = 0
    Dim dValorev As Decimal = 0
    Dim dPrelist As Decimal = 0
    Dim bCanc As Boolean
    Dim dtrT() As DataRow
    Dim dDaQuantOut As Decimal = 0
    Dim dAQuantOut As Decimal = 0
    Dim dPerqtaOut As Decimal = 0
    Dim strUnmisOut As String = ""
    Dim strTipovalOut As String = ""
    Dim dttAssi As DataTable

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codlavo = ""
      Else
        bOk = ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABLAVO", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590223820937500, "Codice lavorazione inesistente")))
          Return
        Else
          e.Row!xx_codlavo = strTmp

          '------------------------------
          'Rileva il prezzo della della lavorazione terzista
          If bTerzista Then
            SettaCondCommerciali_IstanziaNTSCondCommerciali()
            oCondCommerciali.bCalcolaPrezzo = True
            With oCondCommerciali.Input
              .strDitta = strDittaCorrente
              .strCodart = NTSCStr(dtrHT!ec_codart)
              .nCodlavo = NTSCInt(e.ProposedValue)
              .lConto = NTSCInt(dttET.Rows(0)!et_conto)
              .lDestdiv = NTSCInt(dttET.Rows(0)!et_coddest)
              .nListino = nPeacListin
              .strUnmis = NTSCStr(dtrHT!ec_unmis)
              .strUmp = NTSCStr(dtrHT!ec_ump)
              .nFase = NTSCInt(dtrHT!ec_fase)
              .strTipoval = "P"
              .bConspromo = True
              .dtDatdoc = NTSCDate(dttET.Rows(0)!et_datdoc)
              .nCodvalu = NTSCInt(dttET.Rows(0)!et_valuta)
              .dColli = NTSCDec(dtrHT!ec_colli)
              .dQuant = NTSCDec(dtrHT!ec_quant)
              .bConsideraPrezziUnmis = True
              .bPrezziPerUnmis = (NTSCStr(dtrHT!ec_umprz) = "S")
            End With
            'Cerca le condizioni commerciali
            CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
            'Valori di ritorno
            With oCondCommerciali.OutputPrezzo
              dPrezzo = .dPrezzo
              dPrelist = .dPrelist
              dDaQuantOut = .dDaQuant
              dAQuantOut = .dAquant
              dPerqtaOut = .dPerqta
              strUnmisOut = .strUnmis
              strTipovalOut = .strTipo
            End With

            'Se rileva un prezzo = 0 lascia quello che ha
            If dPrezzo <> 0 Then
              If NTSCInt(dttET.Rows(0)!et_valuta) > 0 Then
                dPrezvalc = dPrezzo
                dPrezzo = oCldDocu.ConvImpValuta(strDittaCorrente, True, dPrezvalc, NTSCInt(dttET.Rows(0)!et_valuta), _
                                                NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio), bCanc)
              Else
                dPrezvalc = 0
              End If

              'Usa comunque la quantità caricata
              If dtrHT!ec_umprz.ToString <> "S" Then
                dValore = ArrDbl((dPrezzo * NTSCDec(dtrHT!ec_quant)) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(0))
                dValorev = ArrDbl((dPrezvalc * NTSCDec(dtrHT!ec_quant)) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
              Else
                dValore = ArrDbl((dPrezzo * NTSCDec(dtrHT!ec_colli)) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(0))
                dValorev = ArrDbl((dPrezvalc * NTSCDec(dtrHT!ec_colli)) / NTSCInt(dtrHT!ec_perqta), oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
              End If

              If e.Row.Table.TableName = "LAVCENT" Then
                e.Row!lce_valore = dValore
                e.Row!lce_valorev = dValorev
                e.Row!lce_valmo = 0
              Else
                'Ora aggiorna ASSRIS
                'determino se sono stato chiamato da BNORTTAT o no
                dttAssi = dsTtas.Tables("ATTIVIT")
                If dttAssi Is Nothing Then dttAssi = dsShared.Tables("ASSRIS")
                If Not dttAssi Is Nothing Then
                  dtrT = dttAssi.Select("as_riga = " & e.Row!at_riga.ToString & " AND as_fase = " & e.Row!at_fase.ToString)
                  If dtrT.Length > 0 Then
                    dtrT(0)!as_valore = dValore
                    dtrT(0)!as_valorev = dValorev
                    dtrT(0)!as_valmo = 0
                  End If
                  dttAssi.AcceptChanges()
                End If
              End If

              'ora aggiorno il prezzo della riga dell'ordine di prod
              dtrHT!ec_prezvalc = dPrezvalc
              dtrHT!ec_prezzo = dPrezzo
            End If    'If dPrezzo <> 0 Then
          End If    'If bTerzista Then
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

  Public Overridable Function ColoraCelle(ByVal dtrRow As DataRow) As Boolean
    Dim lColorAzure As Integer = System.Drawing.Color.LightBlue.ToArgb
    Try
      If dtrRow.Table.TableName <> "CORPO" Then Return True

      Dim bHasChangesETTMP As Boolean
      bHasChangesETTMP = bHasChangesET

      'Aggiungo le colonne per la colorazione delle celle
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_row") Then dsShared.Tables("CORPO").Columns.Add("backcolor_row")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_prezzo") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_prezzo")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_preziva") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_preziva")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_prezvalc") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_prezvalc")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_scont1") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_scont1")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_scont2") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_scont2")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_scont3") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_scont3")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_scont4") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_scont4")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_scont5") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_scont5")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_scont6") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_scont6")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_provv") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_provv")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_vprovv") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_vprovv")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_provv2") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_provv2")
      If Not dsShared.Tables("CORPO").Columns.Contains("backcolor_ec_vprovv2") Then dsShared.Tables("CORPO").Columns.Add("backcolor_ec_vprovv2")

      dtrRow!backcolor_row = -1

      Select Case NTSCStr(dtrRow!ec_flkit)
        Case "B", "T" : dtrRow!backcolor_row = -3604536 'verde       'figlio analitico o sintetico
      End Select

      Select Case NTSCStr(dtrRow!ec_tiporiga)
        Case "M" : dtrRow!backcolor_row = System.Drawing.Color.LightGreen.ToArgb
        Case "S" : dtrRow!backcolor_row = System.Drawing.Color.DodgerBlue.ToArgb
        Case "O" : dtrRow!backcolor_row = System.Drawing.Color.MediumSeaGreen.ToArgb
      End Select

      If Not BGESTORDLIST Then
        'Prezzo
        If NTSCStr(dtrRow!ec_flprzmod) = "S" Then
          dtrRow!backcolor_ec_prezzo = lColorAzure
          dtrRow!backcolor_ec_preziva = lColorAzure
          dtrRow!backcolor_ec_prezvalc = lColorAzure
        Else
          dtrRow!backcolor_ec_prezzo = -1
          dtrRow!backcolor_ec_preziva = -1
          dtrRow!backcolor_ec_prezvalc = -1
        End If

        'Sconti
        If NTSCStr(dtrRow!ec_flsc1mod) = "S" Then
          dtrRow!backcolor_ec_scont1 = lColorAzure
        Else
          dtrRow!backcolor_ec_scont1 = -1
        End If
        If NTSCStr(dtrRow!ec_flsc2mod) = "S" Then
          dtrRow!backcolor_ec_scont2 = lColorAzure
        Else
          dtrRow!backcolor_ec_scont2 = -1
        End If
        If NTSCStr(dtrRow!ec_flsc3mod) = "S" Then
          dtrRow!backcolor_ec_scont3 = lColorAzure
        Else
          dtrRow!backcolor_ec_scont3 = -1
        End If
        If NTSCStr(dtrRow!ec_flsc4mod) = "S" Then
          dtrRow!backcolor_ec_scont4 = lColorAzure
        Else
          dtrRow!backcolor_ec_scont4 = -1
        End If
        If NTSCStr(dtrRow!ec_flsc5mod) = "S" Then
          dtrRow!backcolor_ec_scont5 = lColorAzure
        Else
          dtrRow!backcolor_ec_scont5 = -1
        End If
        If NTSCStr(dtrRow!ec_flsc6mod) = "S" Then
          dtrRow!backcolor_ec_scont6 = lColorAzure
        Else
          dtrRow!backcolor_ec_scont6 = -1
        End If

        'Provvigioni
        If NTSCStr(dtrRow!ec_flprov1mod) = "S" Then
          dtrRow!backcolor_ec_provv = lColorAzure
          dtrRow!backcolor_ec_vprovv = lColorAzure
        Else
          dtrRow!backcolor_ec_provv = -1
          dtrRow!backcolor_ec_vprovv = -1
        End If
        If NTSCStr(dtrRow!ec_flprov2mod) = "S" Then
          dtrRow!backcolor_ec_provv2 = lColorAzure
          dtrRow!backcolor_ec_vprovv2 = lColorAzure
        Else
          dtrRow!backcolor_ec_provv2 = -1
          dtrRow!backcolor_ec_vprovv2 = -1
        End If
      End If

      bHasChangesET = bHasChangesETTMP

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

  Public Overridable Sub ModificaRigaImpostaColoriPromozioni(ByVal dtrEc As DataRow)
    Try
      If Not bModCCC Then Return
      If BGESTORDLIST Then Return

      'Se è stata modificata\cancellata una riga associata ad una promozione, allora il programma imposta quella promozione come da riapplicare.
      For i As Integer = 1 To 6
        Dim lPromo As Integer = NTSCInt(dtrEc("ec_codrepr" & i, DataRowVersion.Current))
        If lPromo = 0 Then Continue For

        'Se la riga fa parte di una evasione, allora la riga deve rimanere verde
        Dim dtrPromo() As DataRow = dttPromozioni.Select("tb_codrepr = " & lPromo)
        If dtrPromo.Length > 0 Then
          If bInImportRigheOrd OrElse bInImportRigheNota OrElse bInImportRigheOff Then
            If NTSCInt(dtrPromo(0)!backcolor_row) = -1 Then dtrPromo(0)!backcolor_row = System.Drawing.Color.LightGreen.ToArgb
          Else
            dtrPromo(0)!backcolor_row = System.Drawing.Color.MistyRose.ToArgb
          End If
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

  Public Overridable Function DeterminaValoreStatistico(ByVal dtrRow As datarow, ByVal dIncrementoValore As Decimal) As Decimal
    Try
      If bValoreStatisticoNoScontoPagamento Then
        Return ArrDbl(((NTSCDec(dtrRow!ec_valore) / ((100 - NTSCDec(dttEt.rows(0)!et_scopag)) / 100) + dIncrementoValore) * NTSCDec(dtrRow!ec_percvst)) / 100, oClddocu.TrovaNdec(0))
      Else
        Return ArrDbl(((NTSCDec(dtrRow!ec_valore) + dIncrementoValore) * NTSCDec(dtrRow!ec_percvst)) / 100, oClddocu.TrovaNdec(0))
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
    Return 0
  End Function
End Class
