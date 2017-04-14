Imports System.Data
Imports NTSInformatica.CLN__STD

'ATTENZIONE: visto che quando di cambia una colonna in automatico viene scatenata la beforecolupdate di altre colonne, 
'per far cambaire prezzo/sconti/provvig/valori riga ecc occorre fare in questo modo:
'al cambio di articoli/fase/promozione/quantità/... viene lanciata a mano la SETTAPREZZO e SETTASCONTI
'in automatico al cambio di quantità/prezzi/sconti/ecc verrà scatenata la SETTAVALORIRIGA
'che cambierà il valore (ec_valore) e in questa routine verrà eseguita la SETTAPROVVIGIONI
'il cambio di sconti di testata/piede lancierà manualmente la SETTAVALORIRIGA per tutte le righe

Partial Public Class CLEVEBOLL

  Public Overrides Function CorpoBeforeUpdate(ByVal nRow As Integer) As Boolean
    Dim dtrEC As DataRow
    Dim dtrT() As DataRow = Nothing
    Try
      dtrEC = dttEC.Rows(nRow)
      If Not MyBase.CorpoBeforeUpdate(nRow) Then Return False

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

  Public Overrides Function CorpoAfterUpdate(ByVal nRow As Integer) As Boolean
    Dim dtrEC As DataRow
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0

    Try
      dtrEC = dttEC.Rows(nRow)

      If Not MyBase.CorpoAfterUpdate(nRow) Then Return False

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
      bCambioCommessa = False
    End Try
  End Function

  Public Overrides Sub SettaValoriRiga(ByVal dtrEC As DataRow)
    Try
      MyBase.SettaValoriRiga(dtrEC)

      If dttET.Rows(0)!et_scorpo.ToString = "S" Then
        'calcolo il valore riga iva compresa
        dtrEC!xxo_valrigaic = ArrDbl(ArrDbl(NTSCDec(dtrEC!ec_preziva) * NTSCDec(dtrEC!ec_quant) / _
                              NTSCDec(dtrEC!ec_perqta) * (100 - NTSCDec(dtrEC!ec_scont1)) / 100 * _
                              (100 - NTSCDec(dtrEC!ec_scont2)) / 100 * _
                              (100 - NTSCDec(dtrEC!ec_scont3)) / 100 * _
                              (100 - NTSCDec(dtrEC!ec_scont4)) / 100 * _
                              (100 - NTSCDec(dtrEC!ec_scont5)) / 100 * _
                              (100 - NTSCDec(dtrEC!ec_scont6)) / 100 * _
                              (100 - NTSCDec(dtrEC!ec_scontp)) / 100 - _
                              NTSCDec(dtrEC!ec_scontv), oCldDocu.TrovaNdec(0)) * _
                              (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * _
                              (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * _
                              (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, oCldDocu.TrovaNdec(0))
      Else
        dtrEC!xxo_valrigaic = 0
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


  Public Overrides Function CorpoTestPreCancella(ByVal nRow As Integer) As Boolean
    Dim evnt As NTSEventArgs = Nothing
    Dim strMsg As String = ""
    Dim dttTmp As New DataTable
    Try
      If Not MyBase.CorpoTestPreCancella(nRow) Then Return False

      If dttET.Rows(0)!et_tipork.ToString = "W" Then
        If NTSCDec(dttEC.Rows(nRow)!ec_nprquaeva) <> 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128678477954531250, "Impossibile eliminare la riga di Nota di prelievo in quanto possiede quantità evasa diversa da 0.")))
          Return False
        End If
      End If

      '------------------------
      'Controllo cancellabilità riga per il c/lavoro, produzione
      'in vb6 CheckCancelDocRiga
      If bNew = False Then
        '------------------------
        'Controlla che non sia già usato su qualche documento di scarico (in MOVCLA)
        If Not oCldBoll.CheckCancelDocRiga_movcla(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                                  dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                                  NTSCInt(dttEC.Rows(nRow)!ec_riga), dttTmp) Then Return False
        If dttTmp.Rows.Count > 0 Then
          strMsg = oApp.Tr(Me, 128734886038207839, "Attenzione!" & vbCrLf & vbCrLf & _
                                        "Questo documento/riga di carico in conto/lavoro ATTIVO è già utilizzato " & _
                                        "in altri documenti di scarico (primo documento rilevato da MOVCLA: tipo |" & _
                                        DescrTipork(NTSCStr(dttTmp.Rows(0)!mc_outipo)) & "|, anno |" & _
                                        NTSCStr(dttTmp.Rows(0)!mc_ouanno) & "|, serie '|" & NTSCStr(dttTmp.Rows(0)!mc_ouserie) & _
                                        "|', numero |" & NTSCStr(dttTmp.Rows(0)!mc_ounum) & "|, riga |" & _
                                        NTSCStr(dttTmp.Rows(0)!mc_ouriga) & "|).")
          If bRendiBloccantiAlertCLavoro Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & vbCrLf & vbCrLf & oApp.Tr(Me, 128734886187569839, "Impossibile procedere alla cancellazione.")))
            Return False
          Else
            evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, strMsg & vbCrLf & vbCrLf & oApp.Tr(Me, 128734886351907839, "Procedere ugualmente alla cancellazione?"))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
          End If
        End If    'If dttTmp.Rows.Count > 0 Then

        '------------------------
        'Controlla che non sia già usato su qualche documento di scarico (campo mm_cl*)
        If Not oCldBoll.CheckCancelDocRiga_movcla2(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                                  dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                                  NTSCInt(dttEC.Rows(nRow)!ec_riga), dttTmp) Then Return False
        If dttTmp.Rows.Count > 0 Then
          strMsg = oApp.Tr(Me, 128734886062699839, "Attenzione!" & vbCrLf & vbCrLf & "Questo documento/riga di carico " & _
                                          "in conto/lavoro è già utilizzato in altri documenti di scarico " & _
                                          "(primo documento rilevato: tipo |" & _
                                          DescrTipork(NTSCStr(dttTmp.Rows(0)!mm_tipork)) & "|, anno |" & _
                                          NTSCStr(dttTmp.Rows(0)!mm_anno) & "|, serie '|" & NTSCStr(dttTmp.Rows(0)!mm_serie) & _
                                          "|', numero |" & NTSCStr(dttTmp.Rows(0)!mm_numdoc) & "|, riga |" & _
                                          NTSCStr(dttTmp.Rows(0)!mm_riga) & "|).")
          If bRendiBloccantiAlertCLavoro Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & vbCrLf & vbCrLf & oApp.Tr(Me, 128734886207381839, "Impossibile procedere alla cancellazione.")))
            Return False
          Else
            evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, strMsg & vbCrLf & vbCrLf & oApp.Tr(Me, 128734886370627839, "Procedere ugualmente alla cancellazione?"))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
          End If
        End If    'If dttTmp.Rows.Count > 0 Then

        '------------------------
        'Se trattasi di una carico di produzione controlla che non sia già usato su qualche
        'documento (campo mm_cc*)
        If dttET.Rows(0)!et_tipork.ToString = "T" Then
          If Not oCldBoll.CheckCancelDocRiga_movcla3(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                            dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                            NTSCInt(dttEC.Rows(nRow)!ec_riga), dttTmp) Then Return False
          If dttTmp.Rows.Count > 0 Then
            strMsg = oApp.Tr(Me, 128678566341562500, "Attenzione!" & vbCrLf & vbCrLf & "Questo documento/riga di " & _
                                          "carico (produzione) in conto/lavoro è già utilizzato in altri documenti di scarico " & _
                                          "(primo documento rilevato: tipo |" & _
                                          DescrTipork(NTSCStr(dttTmp.Rows(0)!mm_tipork)) & "|, anno |" & _
                                          NTSCStr(dttTmp.Rows(0)!mm_anno) & "|, serie '|" & NTSCStr(dttTmp.Rows(0)!mm_serie) & _
                                          "|', numero |" & NTSCStr(dttTmp.Rows(0)!mm_numdoc) & "|, riga |" & _
                                          NTSCStr(dttTmp.Rows(0)!mm_riga) & "|).")
            If bRendiBloccantiAlertCLavoro Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & vbCrLf & vbCrLf & oApp.Tr(Me, 128734886227193839, "Impossibile procedere alla cancellazione.")))
              Return False
            Else
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, strMsg & vbCrLf & vbCrLf & oApp.Tr(Me, 128734886334435839, "Procedere ugualmente alla cancellazione?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
            End If
          End If    'If dttTmp.Rows.Count > 0 Then
        End If    'If dttET.Rows(0)!et_tipork.ToString = "T" Then

      End If    'If bNew = False Then

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

  Public Overrides Function CorpoAfterDelete(ByRef dtrDeleted As DataRow) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim i As Integer = 0
    Dim l As Integer = 0
    Try
      '-------------------------------
      'Cancella le matricole degli scarichi collegati
      If dttET.Rows(0)!et_tipork.ToString = "T" Then
        dtrT = dttECIMP.Select("ec_prriga = " & NTSCInt(dtrDeleted!ec_riga).ToString)
        For i = 0 To dtrT.Length - 1
          dtrT1 = dttMOVMATR.Select("mma_tipork = 'U' AND mma_riga = " & NTSCInt(dtrT(i)!ec_riga).ToString)
          For l = 0 To dtrT1.Length - 1
            dtrT1(l).Delete()
          Next
        Next

        '-------------------------------
        'cancello gli scarichi e le lavorazioni collegati
        dtrT = dttECIMP.Select("ec_prriga = " & NTSCInt(dtrDeleted!ec_riga).ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i).Delete()
        Next

        dtrT = dttLAVCENT.Select("lce_riga = " & NTSCInt(dtrDeleted!ec_riga).ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i).Delete()
        Next
      End If

      '-------------------------------
      'Cancella le matricole griglia principale
      dtrT = dttMOVMATR.Select("mma_tipork = '" & NTSCStr(dtrDeleted!ec_tipork) & "' AND mma_riga = " & NTSCInt(dtrDeleted!ec_riga).ToString)
      For i = 0 To dtrT.Length - 1
        dtrT(i).Delete()
      Next

      '-------------------------------
      'Cancella dettaglio conai
      dtrT = dttMOVCONA.Select("mma_riga = " & NTSCInt(dtrDeleted!ec_riga).ToString)
      For i = 0 To dtrT.Length - 1
        dtrT(i).Delete()
      Next

      '-------------------------------
      'cancello dettaglio tco scarichi e dettaglio tco griglia principale
      If Not MyBase.CorpoAfterDelete(dtrDeleted) Then Return False

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

  Public Overrides Function CorpoAfterInsert(ByVal nRow As Integer) As Boolean
    Try
      If bSaltaAfterInsert Then Return True 'settato da fuori, tipo da BEORGNOR

      If Not MyBase.CorpoAfterInsert(nRow) Then Return False

      '--------------------------------
      'Carica i materiali CONAI (NO SUI DOCUMENTI IN SCORPORO)
      If bConai And dttEt_conto.Rows(0)!an_gescon.ToString <> "N" And bDocEmesso And _
               dttET.Rows(0)!et_tipork.ToString <> "W" And dttET.Rows(0)!et_scorpo.ToString = "N" Then
        ScriviRigheConai(dttEC.Rows(nRow))
      End If    'If bConai And dttEt_conto.Rows(0)!an_gescon.ToString <> "N" And bDocEmesso And _

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

  Public Overridable Function ScriviRigheConai(ByRef dtrEC As DataRow) As Boolean
    Dim dttCona As New DataTable
    Dim i As Integer = 0
    Try
      '---------------------------
      'Esce se l'articolo non ha una gestione conai come 'prima cessione'
      If GetMemDttArti(dtrEC!ec_codart.ToString).Rows(0)!ar_gescon.ToString <> "P" Then Return True

      If Not oCldBoll.GetArticoFigliConai(strDittaCorrente, dtrEC!ec_codart.ToString, dttCona) Then Return False
      If dttCona.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128680368700972454, "Per l'articolo '|" & dtrEC!ec_codart.ToString & "|' non sono stati trovati articoli componenti CONAI, pur possedendo una gestione CONAI di tipo 'Prima cessione'.")))
        Return True
      Else
        bInImportConai = True
        For i = 0 To dttCona.Rows.Count - 1
          ScriviRigaConai(dttCona.Rows(i), dtrEC)
        Next
      End If
      dttCona.Clear()

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
      bInImportConai = False
    End Try
  End Function
  Public Overridable Function ScriviRigaConai(ByRef dtrCona As DataRow, ByRef dtrPadre As DataRow) As Boolean
    'data la riga di ARTKIT in input la scrive in dttEC
    Dim strErrore As String = ""
    Dim dQuant As Decimal = 0
    Dim dPrezzo As Decimal = 0
    Dim dPrezvalc As Decimal = 0
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim dPerc As Decimal = 0

    Try
      'Prezzi (sui doc. in valuta, in deroga a tutti gli altri casi, prima
      'rilevo il contributo CONAI in moneta di conto EURO e poi lo trasformo
      'nella valuta del documento all'indietro.
      ' fase =0 , non gestita fase su materilai conai...
      SettaCondCommerciali_IstanziaNTSCondCommerciali()
      oCondCommerciali.bCalcolaPrezzo = True
      With oCondCommerciali.Input
        .strDitta = strDittaCorrente
        .strCodart = dtrCona!ak_codfigli.ToString
        .nListino = nConaiListinoContributo
        .strTipoval = "P"
        .bConspromo = True
        .dtDatdoc = NTSCDate(dttET.Rows(0)!et_datdoc)
        .dQuant = dQuant
      End With
      '-----------------------------
      CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
      '-----------------------------
      dPrezzo = oCondCommerciali.OutputPrezzo.dPrezzo
      '-----------------------------
      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
        dPrezvalc = oCldDocu.ConvImpEur(True, dPrezzo, NTSCInt(dttET.Rows(0)!et_valuta), _
                    NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
      End If

      oCldBoll.ValCodiceDb(dtrCona!ak_codfigli.ToString, strDittaCorrente, "ARTICO", "S", "", dttTmp)
      dPerc = NTSCDec(dttEt_conto.Rows(0)!an_perescon)  '% esenzione conai generale presa da anagra

      dttTmp1.Clear()
      If NTSCInt(dttTmp.Rows(0)!ar_sotgru) > 0 Then
        oCldBoll.GetAnaCona(strDittaCorrente, NTSCInt(dttET.Rows(0)!et_conto), NTSCInt(dttTmp.Rows(0)!ar_sotgru), dttTmp1)
        If dttTmp1.Rows.Count > 0 Then
          '% esenzione conai specifica per cliente / sottogruppo merceologico a cui appartiene l'articolo CONAI
          dPerc = NTSCDec(dttTmp1.Rows(0)!acn_perescon)
        End If
        dttTmp1.Clear()
      End If

      dttMOVCONA.Rows.Add(dttMOVCONA.NewRow)
      With dttMOVCONA.Rows(dttMOVCONA.Rows.Count - 1)
        !codditt = strDittaCorrente
        !mma_tipork = dttET.Rows(0)!et_tipork
        !mma_anno = dttET.Rows(0)!et_anno
        !mma_serie = dttET.Rows(0)!et_serie
        !mma_numdoc = dttET.Rows(0)!et_numdoc
        !mma_riga = dtrPadre!ec_riga
        !mma_rigaa = dtrCona!ak_riga
        !mma_codart = dtrCona!ak_codfigli.ToString
        !mma_quant = ArrDbl(NTSCDec(dtrCona!ak_quant) * NTSCDec(dtrPadre!ec_quant), 3)
        !mma_prezzo = dPrezzo
        !mma_prezval = dPrezvalc
        !mma_perescon = dPerc
        !mma_note = DBNull.Value
        !mma_perqta = dttTmp.Rows(0)!ar_perqta
        !mma_composiz = dtrCona!ak_composiz
        !xx_codart = dttTmp.Rows(0)!ar_descr.ToString
        !xx_unmis = dttTmp.Rows(0)!ar_unmis.ToString
      End With
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      dttEC.Rows(dttEC.Rows.Count - 1).Delete()
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      dttMOVCONA.AcceptChanges()
    End Try
  End Function

  Public Overridable Function CreaSalClPerZoom(ByRef dtrEc As DataRow, ByRef dttMcla As DataTable, ByRef nMagaz As Integer) As Boolean
    '------------------------
    'crea i saldi di c/to lavoro per poi visualizzarli in FRMMGHLCL
    Dim dttTmp As New DataTable
    Dim bAltrui As Boolean = False
    Dim bAltruiPressoTerzi As Boolean = False
    Dim dtrTPadre() As DataRow = Nothing

    Try
      '----------------------
      ' cerca qul'è il magazzino interessato (da priorità al secondo mag. se esiste ...
      bAltrui = False
      nMagaz = NTSCInt(dtrEc!ec_magaz)
      If dtrEc!xxo_flclavo.ToString = "C" Or dtrEc!xxo_flclavo.ToString = "X" Then bAltrui = True

      If NTSCInt(dtrEc!ec_magaz2) <> 0 Then
        If dtrEc!xxo_flclavo2.ToString = "C" Or dtrEc!xxo_flclavo2.ToString = "F" Or dtrEc!xxo_flclavo2.ToString = "X" Then
          If dtrEc!xxo_flclavo2.ToString = "C" Or dtrEc!xxo_flclavo2.ToString = "X" Then bAltrui = True Else bAltrui = False
          'Il magazzino su cui vedere i carichi giacenti è
          'sempre quello di scarico
          If NTSCInt(dtrEc!xxo_esist2) = -1 Then
            nMagaz = NTSCInt(dtrEc!ec_magaz2)
          End If
        End If
      End If    'If NTSCInt(dtrEc!ec_magaz2) <> 0 Then

      'L'articolo non deve essere gestito a fasi su magazzini merce altrui
      If NTSCInt(dtrEc!ec_fase) <> 0 And bAltrui Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128798556640804000, "Impossibile attivare questa funzione per articoli gestiti a fase, in magazzini di tipo merce altrui.")))
        Return False
      End If

      If Not bAttribRifCLavAttSuScarProdArtFasi And NTSCStr(dtrEc!ec_tipork) = "U" Then
        'Anche se l'articolo PADRE è a fasi non si devono selezionare le bolle di c/lavoro aperte
        'solo se sono stato chiamato dalla form degli scarichi collegati
        'devo trovare l'articolo PADRE 
        dtrTPadre = dttEC.Select("ec_tipork = " & CStrSQL(dtrEc!ec_prtipo) & " AND ec_anno = " & NTSCInt(dtrEc!ec_pranno).ToString & _
                                 " AND ec_serie = " & CStrSQL(dtrEc!ec_prserie) & " AND ec_numdoc = " & NTSCInt(dtrEc!ec_prnum).ToString & _
                                 " AND ec_riga = " & NTSCInt(dtrEc!ec_prriga).ToString)
        'eseguo il test
        If dtrTPadre.Length > 0 Then
          If NTSCInt(dtrTPadre(0)!ec_fase) <> 0 And bAltrui Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129077028827900390, "Impossibile attivare questa funzione quando l'articolo padre è gestiti a fase, in magazzini di tipo merce altrui (conto/lavoro attivo).")))
            Return False
          End If
        End If
      End If

      oCldBoll.ValCodiceDb(NTSCStr(dtrEc!ec_codart), strDittaCorrente, "ARTICO", "S", "", dttTmp)
      '---------------------
      'creo i saldi del c/to lavoro
      If Not ApriSaldiClav(dttET.Rows(0), NTSCStr(dtrEc!ec_codart), NTSCInt(dtrEc!ec_fase), _
                           nMagaz, dttMcla, NTSCInt(IIf(NTSCStr(dttTmp.Rows(0)!ar_gescomm) = "S", NTSCInt(dtrEc!ec_commeca), 0)), bTMclav, lTmNumIT) Then Return False
      'dtrTmp = dttMcla.Select("", "Datdoc, mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga")

      '---------------------
      'compilo le colonne unbound
      dttMcla.Columns.Add("xx_riferim", GetType(String))
      dttMcla.Columns.Add("xx_annpar", GetType(Integer))
      dttMcla.Columns.Add("xx_alfpar", GetType(String))
      dttMcla.Columns.Add("xx_numpar", GetType(Integer))
      dttMcla.Columns.Add("xx_datpar", GetType(DateTime))
      dttMcla.Columns.Add("xx_prznet", GetType(Decimal))
      dttMcla.Columns.Add("mm_commeca", GetType(Integer))
      dttMcla.Columns.Add("xx_commeca", GetType(String))
      dttMcla.Columns.Add("mm_subcommeca", GetType(String))
      dttMcla.Columns.Add("xx_lottox", GetType(String))
      dttMcla.Columns.Add("xx_conto", GetType(Integer))
      dttMcla.Columns.Add("xx_desconto", GetType(String))
      dttMcla.Columns.Add("mm_descr", GetType(String))
      dttMcla.Columns.Add("mm_desint", GetType(String))
      dttMcla.Columns.Add("mm_note", GetType(String))

      dttMcla.Columns("Saldo").ColumnName = "xx_saldo"
      dttMcla.Columns("Datdoc").ColumnName = "xx_datdoc"
      For Each dtrT As DataRow In dttMcla.Rows
        oCldBoll.ClavGetTmDatdoc(strDittaCorrente, dtrT!mm_cltipo.ToString, NTSCInt(dtrT!mm_clanno), _
                                 dtrT!mm_clserie.ToString, NTSCInt(dtrT!mm_clnum), NTSCInt(dtrT!mm_clriga), _
                                 dttTmp)
        If dttTmp.Rows.Count > 0 Then
          dtrT!mm_commeca = dttTmp.Rows(0)!mm_commeca
          dtrT!xx_commeca = dttTmp.Rows(0)!xx_commeca
          dtrT!mm_subcommeca = dttTmp.Rows(0)!mm_subcommeca
          dtrT!xx_lottox = dttTmp.Rows(0)!xx_lottox
          dtrT!xx_riferim = dttTmp.Rows(0)!tm_riferim
          dtrT!xx_annpar = dttTmp.Rows(0)!tm_annpar
          dtrT!xx_alfpar = dttTmp.Rows(0)!tm_alfpar
          dtrT!xx_numpar = dttTmp.Rows(0)!tm_numpar
          dtrT!xx_datpar = dttTmp.Rows(0)!tm_datpar
          dtrT!xx_conto = dttTmp.Rows(0)!tm_conto
          dtrT!xx_desconto = dttTmp.Rows(0)!xx_desconto
          dtrT!mm_descr = dttTmp.Rows(0)!mm_descr
          dtrT!mm_desint = dttTmp.Rows(0)!mm_desint
          dtrT!mm_note = dttTmp.Rows(0)!mm_note
        End If
        dttTmp.Clear()
        dtrT!xx_prznet = oCldBoll.GetPrezzoNetto(strDittaCorrente, dtrT!mm_cltipo.ToString, NTSCInt(dtrT!mm_clanno), dtrT!mm_clserie.ToString, NTSCInt(dtrT!mm_clnum), NTSCInt(dtrT!mm_clriga))
      Next
      dttMcla.AcceptChanges()

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
  Public Overridable Function CreaSalCPPerZoom(ByRef dtrEc As DataRow, ByRef dttMcla As DataTable, ByRef nMagaz As Integer) As Boolean
    '------------------------
    'crea i saldi di c/to lavoro -carichi produzoine per poi visualizzarli in bnmgdocu.frm....
    Dim dttTmp As New DataTable

    Try
      '--------------------------------
      'un po' di controlli
      If dtrEc!xxo_flclavo.ToString <> "C" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128699410970468750, "Il magazzino deve essere di tipo 'Merce altrui' per selezionare carichi di produzione (conto lavoro attivo).")))
        Return False
      End If

      If dtrEc!xxo_tipcaum.ToString <> "E" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128699411727031250, "La causale di magazzino deve essere di tipo 'E' (Reso lavorato con trasf. su conto lavoro attivo) per selezionare carichi di produzione.")))
        Return False
      End If

      If NTSCInt(dtrEc!ec_fase) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128699412109375000, "Impossibile attivare questa funzione per articoli gestiti a fase.")))
        Return False
      End If

      nMagaz = NTSCInt(dtrEc!ec_magaz)

      '---------------------
      'creo i saldi del c/to lavoro CARICHI DA PRODUZIONE
      If Not ApriSaldiClavProd(dttET.Rows(0), NTSCStr(dtrEc!ec_codart), NTSCInt(dtrEc!ec_fase), _
                                NTSCInt(dtrEc!ec_magaz), dttMcla) Then Return False
      'dtrTmp = dttMcla.Select("", "Datdoc, mm_cltipo, mm_clanno, mm_clserie, mm_clnum, mm_clriga")

      '---------------------
      'compilo le colonne unbound
      dttMcla.Columns.Add("xx_riferim", GetType(String))
      dttMcla.Columns.Add("xx_annpar", GetType(Integer))
      dttMcla.Columns.Add("xx_alfpar", GetType(String))
      dttMcla.Columns.Add("xx_numpar", GetType(Integer))
      dttMcla.Columns.Add("xx_datpar", GetType(DateTime))
      dttMcla.Columns.Add("xx_prznet", GetType(Decimal))
      dttMcla.Columns.Add("mm_commeca", GetType(Integer))
      dttMcla.Columns.Add("xx_commeca", GetType(String))
      dttMcla.Columns.Add("mm_subcommeca", GetType(String))
      dttMcla.Columns.Add("xx_lottox", GetType(String))
      dttMcla.Columns("Saldo").ColumnName = "xx_saldo"
      dttMcla.Columns("Datdoc").ColumnName = "xx_datdoc"
      For Each dtrT As DataRow In dttMcla.Rows
        oCldBoll.ClavGetTmDatdoc(strDittaCorrente, dtrT!mm_cltipo.ToString, NTSCInt(dtrT!mm_clanno), dtrT!mm_clserie.ToString, NTSCInt(dtrT!mm_clnum), dttTmp)
        oCldBoll.ClavGetTmDatdoc(strDittaCorrente, dtrT!mm_cltipo.ToString, NTSCInt(dtrT!mm_clanno), dtrT!mm_clserie.ToString, NTSCInt(dtrT!mm_clnum), dttTmp)
        dtrT!xx_riferim = dttTmp.Rows(0)!tm_riferim
        dtrT!xx_annpar = dttTmp.Rows(0)!tm_annpar
        dtrT!xx_alfpar = dttTmp.Rows(0)!tm_alfpar
        dtrT!xx_numpar = dttTmp.Rows(0)!tm_numpar
        dtrT!xx_datpar = dttTmp.Rows(0)!tm_datpar
        dttTmp.Clear()
        dtrT!xx_prznet = oCldBoll.GetPrezzoNetto(strDittaCorrente, dtrT!mm_cltipo.ToString, NTSCInt(dtrT!mm_clanno), dtrT!mm_clserie.ToString, NTSCInt(dtrT!mm_clnum), NTSCInt(dtrT!mm_clriga))
        If oCldBoll.ClavGetDatiRiga(strDittaCorrente, dtrT!mm_cltipo.ToString, NTSCInt(dtrT!mm_clanno), _
                                    dtrT!mm_clserie.ToString, NTSCInt(dtrT!mm_clnum), NTSCInt(dtrT!mm_clriga), _
                                    dttTmp) = True Then
          dtrT!mm_commeca = dttTmp.Rows(0)!mm_commeca
          dtrT!xx_commeca = dttTmp.Rows(0)!xx_commeca
          dtrT!mm_subcommeca = dttTmp.Rows(0)!mm_subcommeca
          dtrT!xx_lottox = dttTmp.Rows(0)!xx_lottox
        End If
        dttTmp.Clear()
      Next
      dttMcla.AcceptChanges()

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


  Public Overridable Function ScorporaPrezzo(ByRef dtrC As DataRow) As Boolean
    Dim nCodiceIva As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dIva As Decimal = 0
    Dim dttTmp As New DataTable
    Try
      '------------
      'Per default utilizza il Codice Iva presente sulla riga
      nCodiceIva = NTSCInt(dtrC!ec_codiva)
      '------------
      'Se in testata:
      'NON è selezionato il CheckBox di scorporo
      'è indicato il codice esenzione Iva con l'aliquota relativa a zero
      'il listino è "ivato"
      'utilizza il codice iva presente nell'anagrafica dell'articolo
      If dttET.Rows(0)!et_tipork.ToString <> "T" And dttET.Rows(0)!et_scorpo.ToString = "N" And _
         NTSCInt(dttET.Rows(0)!et_codese) <> 0 And NTSCInt(dttET.Rows(0)!et_listino) <> 0 Then
        oCldBoll.ValCodiceDb(NTSCInt(dttET.Rows(0)!et_codese).ToString, strDittaCorrente, "TABCIVA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          If NTSCDec(dttTmp.Rows(0)!tb_aliq) = 0 Then
            dttTmp.Clear()

            oCldBoll.ValCodiceDb(dttET.Rows(0)!et_listino.ToString, strDittaCorrente, "TABLIST", "N", "", dttTmp)
            If dttTmp.Rows.Count > 0 Then
              If dttTmp.Rows(0)!tb_ivato.ToString = "S" Then
                dttTmp.Clear()
                oCldBoll.ValCodiceDb(dtrC!ec_codart.ToString, strDittaCorrente, "ARTICO", "S", "", dttTmp)
                If dttTmp.Rows.Count > 0 Then
                  If NTSCInt(dttTmp.Rows(0)!ar_codiva) <> 0 Then nCodiceIva = NTSCInt(dttTmp.Rows(0)!ar_codiva)
                End If
              End If
            End If

          End If
        End If
        dttTmp.Clear()
      End If    'If dttET.Rows(0)!et_tipork.ToString <> "T" And dttET.Rows(0)!et_scorpo.ToString = "N" And _

      '----------------------------------------------------------------------------------------
      oCldBoll.Scorporo(NTSCDec(dtrC!ec_prezzo), nCodiceIva, dImponib, dIva, oCldBoll.TrovaNdecSuPrzUn(0))
      dtrC!ec_prezzo = ArrDbl(dImponib, oCldBoll.TrovaNdecSuPrzUn(0))

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


#Region "BEFORE/AFTER UPDATE"
  Public Overridable Function BeforeColUpdate_CORPO_ec_causale(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Dim strDarave2 As String = ""
    Dim strDescrOld As String = ""
    Try
      If NTSCInt(e.ProposedValue) = NTSCInt(dttET.Rows(0)!et_causale) And bModPM = False Then
        e.Row!xxo_causale = NTSCStr(dttET.Rows(0)!xx_causale)
        e.Row!ec_causale2 = NTSCStr(dttTabucamTesta.Rows(0)!tb_causec)
        e.Row!xxo_causec = NTSCStr(dttTabucamTesta.Rows(0)!tb_causec)
        e.Row!xxo_testci = NTSCStr(dttTabucamTesta.Rows(0)!tb_testci)
        e.Row!xxo_esist = NTSCInt(dttTabucamTesta.Rows(0)!tb_esist)
        e.Row!xxo_tipcaum = dttTabucamTesta.Rows(0)!tb_tipcaum
        e.Row!xxo_codcacadd = NTSCStr(dttTabucamTesta.Rows(0)!tb_codcacadd)
        e.Row!xxo_codcacalv = NTSCStr(dttTabucamTesta.Rows(0)!tb_codcacalv)
      Else
        strDescrOld = NTSCStr(e.Row!xxo_causale)
        If ValCodice(e, "CAUM", "xxo_causale", oApp.Tr(Me, 128669162259062500, "Codice causale di magazzino non corretto"), "N", dttTmp) Then
          If dttTmp.Rows.Count > 0 Then
            If bModPM Then
              'GESTIONE PROJECT MANAGEMENT
              'Test di validità del campo
              Select Case dttTmp.Rows(0)!tb_testci.ToString
                Case " " : strFDarave = " "
                Case "A", "B" : strFDarave = "A"
                Case "D", "E" : strFDarave = "D"
              End Select
              If NTSCInt(e.Row!ec_pmtaskid) <> 0 Then
                If strDarave2 <> NTSCStr(e.Row!xxo_darave) Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129443309829580079, "Causale magazzino avente Segno Dare/avere non compatibile con quello del Task indicato nella riga corrente. Causale non ammessa.")))
                  e.ProposedValue = e.Row!ec_causale
                  e.Row!xxo_causale = strDescrOld
                  Return False
                End If
              End If
            End If    'If bModPM Then
            e.Row!ec_causale2 = NTSCStr(dttTmp.Rows(0)!tb_causec)
            e.Row!xxo_causec = NTSCStr(dttTmp.Rows(0)!tb_causec)
            e.Row!xxo_testci = NTSCStr(dttTmp.Rows(0)!tb_testci)
            e.Row!xxo_esist = NTSCInt(dttTmp.Rows(0)!tb_esist)
            e.Row!xxo_tipcaum = dttTmp.Rows(0)!tb_tipcaum
            e.Row!xxo_codcacadd = NTSCStr(dttTmp.Rows(0)!tb_codcacadd)
            e.Row!xxo_codcacalv = NTSCStr(dttTmp.Rows(0)!tb_codcacalv)

            'c/to visione: se cambio la causale con una NON di c/to visione cancello i riferimenti
            If NTSCStr(e.Row!xxo_tipcaum) <> "V" And NTSCStr(e.Row!xxo_tipcaum) <> "U" And _
               NTSCStr(e.Row!xxo_tipcaum2) <> "V" And NTSCStr(e.Row!xxo_tipcaum2) <> "U" Then
              e.Row!ec_cvtipo = DBNull.Value
              e.Row!ec_cvanno = 0
              e.Row!ec_cvserie = DBNull.Value
              e.Row!ec_cvnum = 0
              e.Row!ec_cvriga = 0
            End If
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
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_CORPO_ec_causale2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Try
      If ValCodice(e, "CAUM", "xxo_causale2", oApp.Tr(Me, 128669162982656250, "Codice causale di magazzino 2 non corretto"), "N", dttTmp) Then
        If dttTmp.Rows.Count > 0 Then
          e.Row!xxo_testci2 = NTSCStr(dttTmp.Rows(0)!tb_testci)
          e.Row!xxo_esist2 = NTSCInt(dttTmp.Rows(0)!tb_esist)
          e.Row!xxo_tipcaum2 = dttTmp.Rows(0)!tb_tipcaum
          e.Row!xxo_codcacadd2 = NTSCStr(dttTmp.Rows(0)!tb_codcacadd)
          e.Row!xxo_codcacalv2 = NTSCStr(dttTmp.Rows(0)!tb_codcacalv)
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
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_CORPO_ec_codnomc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Try
      If NTSCStr(e.ProposedValue).Trim <> "" Then
        If Not oCldDocu.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TARIC", "S", "") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128679562036875000, "Codice nomenclatura combinata |" & e.ProposedValue.ToString & "| non corretto")))
          e.ProposedValue = ""
        End If

        'per servizi nella provincia di origine memorizzo lo stato del cliente/fornitore, ovvero 'paese pagamento'
        'i servizi hanno codice nomenclatura che inizia per 'S.'
        If NTSCStr(e.ProposedValue).PadRight(2).ToUpper.Substring(0, 2) = "S." Then
          oCldBoll.ValCodiceDb(NTSCStr(dttEt_conto.Rows(0)!an_stato), strDittaCorrente, "TABSTAT", "S", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            e.Row!ec_proorig = Left(NTSCStr(dttTmp.Rows(0)!tb_siglaiso), 2)
          Else
            e.Row!ec_proorig = Left(NTSCStr(dttEt_conto.Rows(0)!an_stato), 2)
          End If
        Else
          If bDocEmesso Then
            e.Row!ec_proorig = GetMemDttArti(e.Row!ec_codart.ToString).Rows(0)!ar_prorig
          Else
            e.Row!ec_proorig = GetMemDttArti(e.Row!ec_codart.ToString).Rows(0)!ar_paeorig
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
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function BeforeColUpdate_CORPO_ec_salcon(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.ProposedValue.ToString <> "C" Then
        If NTSCInt(e.Row!ec_ornum) = 0 Then e.ProposedValue = "C"
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_npsalcon(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.ProposedValue.ToString <> "C" Then
        If NTSCInt(e.Row!ec_npnum) = 0 Then e.ProposedValue = "C"
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_clanno(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(e.ProposedValue) <> 0 And NTSCInt(e.ProposedValue) < 1900 Then
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128695881772031250, "Inserire un numero compreso tra 1900 e 2099 (oppure 0).")))
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_orserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_prserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_clserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_npserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_acserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_ccserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_cvserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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

  Public Overridable Function AfterColUpdate_CORPO_ec_percvst(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try

      e.Row!ec_valstat = DeterminaValoreStatistico(e.row, 0)

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

