Imports System.Data
Imports NTSInformatica.CLN__STD

'ATTENZIONE: visto che quando di cambia una colonna in automatico viene scatenata la beforecolupdate di altre colonne, 
'per far cambaire prezzo/sconti/provvig/valori riga ecc occorre fare in questo modo:
'al cambio di articoli/fase/promozione/quantità/... viene lanciata a mano la SETTAPREZZO e SETTASCONTI
'in automatico al cambio di quantità/prezzi/sconti/ecc verrà scatenata la SETTAVALORIRIGA
'che cambierà il valore (ec_valore) e in questa routine verrà eseguita la SETTAPROVVIGIONI
'il cambio di sconti di testata/piede lancierà manualmente la SETTAVALORIRIGA per tutte le righe

Partial Public Class CLEORGSOR

  Public Overrides Function CorpoBeforeUpdate(ByVal nRow As Integer) As Boolean
    Dim dtrEC As DataRow
    Dim dtrT() As DataRow = Nothing
    Try
      dtrEC = dttEC.Rows(nRow)
      If Not MyBase.CorpoBeforeUpdate(nRow) Then Return False

      '---------------------------------------------------------------------------------------
      'Calcolo del rapporto cambio quantità/flevas/datcons
      If dttET.Rows(0)!et_tipork.ToString = "H" And dtrEC.RowState <> DataRowState.Added Then
        'data consegna
        lDiffDatcons = NTSCInt(DateDiff("d", NTSCDate(dtrEC("ec_datcons", DataRowVersion.Original)), NTSCDate(dtrEC!ec_datcons)))
      End If    'If dttet.rows(0)!et_tipork.ToString = "H" Then

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

      If dttET.Rows(0)!et_tipork.ToString = "H" Then
        '---------------------------------
        'al cambio della data di consegna sulla riga di produzione chiedo se cambiarla sulle righe degli impegni/lavorazioni
        If lDiffDatcons <> 0 Then
          AggImpegniDatcons(lDiffDatcons, dtrEC)
        End If
        lDiffDatcons = 0
      End If

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

  Public Overrides Function CheckAssegnazione(ByVal dtrTmp As DataRow) As Boolean
    '------------------------------
    'sulle righe da assegnare controllo che tutto sia OK
    Try
      'Non ammette evasioni in negativo
      If NTSCDec(dtrTmp!xxo_qtadaass) < 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527365669096000, "Controllo assegnazione quantità per generazione note di prelievo, riga |" & NTSCInt(dtrTmp!ec_riga) & "|: è indicata una quantità da assegnare negativa.")))
        Return False
      End If
      'Controlla che non siano già evase saldo
      If NTSCStr(dtrTmp!ec_flevas) = "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527365649128000, "Controllo assegnazione quantità per generazione note di prelievo, riga |" & NTSCInt(dtrTmp!ec_riga) & "| impossibile assegnare quantità su righe già completamente evase.")))
        Return False
      End If
      'Controlla che non siano già prenotato a saldo
      If NTSCStr(dtrTmp!ec_flevapre) = "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527365635868000, "Controllo assegnazione quantità per generazione note di prelievo, riga |" & NTSCInt(dtrTmp!ec_riga) & "|: impossibile assegnare quantità su righe già completamente prenotate.")))
        Return False
      End If
      'Controlla che sia assegnato a saldo se la qtà è superiore alla residua
      If NTSCStr(dtrTmp!xxo_flevasass) = "C" And ArrDbl(NTSCDec(dtrTmp!xxo_qtadaass), 3) >= ArrDbl(NTSCDec(dtrTmp!ec_quant) - NTSCDec(dtrTmp!ec_quaeva) - NTSCDec(dtrTmp!ec_quapre), 3) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527365623076000, "Controllo assegnazione quantità per generazione note di prelievo, riga |" & NTSCInt(dtrTmp!ec_riga) & "|: impossibile assegnare una quantità in conto, che supera o è uguale alla quantità residua (ordinato - prenotato - evaso) della riga.")))
        Return False
      End If
      'Controllo indicazione lotti
      If NTSCStr(dtrTmp!xxo_geslotti) = "S" And NTSCInt(dtrTmp!ec_lotto) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527365609972000, "Controllo assegnazione quantità per generazione note di prelievo, riga |" & NTSCInt(dtrTmp!ec_riga) & "|: indicazione del numero lotto obbligatoria per questo articolo.")))
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

  Public Overrides Function BloccaEvasione(ByVal dtrTmp As DataRow) As Boolean
    '------------------------
    'In 'Impegni Clienti', 'Impegni di Trasferimento', 'Ordini di produzione':
    'se alla riga corrente sono associate delle Note di Prelievo NON ancora evase
    'il programma segnala e blocca la modifica manuale del saldo
    Dim dttTmp As New DataTable
    Dim dtrT() As DataRow = Nothing
    Try
      BloccaEvasione = False

      If bNew Then Return False 'Ha senso solo sui documenti presi in modifica non quelli nuovi, ovviamente
      Select Case dttET.Rows(0)!et_tipork.ToString
        Case "R", "X", "H"
        Case Else : Return False
      End Select

      '------------------------
      'Se il flag di evasione totale è passato da "C" a "S" fa il controllo
      If dtrTmp.RowState <> DataRowState.Added Then
        If Not (dtrTmp!ec_flevas.ToString = "S" And NTSCStr(dtrTmp("ec_flevas", DataRowVersion.Original)) = "C") Then
          Return False
        End If
      End If

      '------------------------
      'se è una nuova riga non faccio nulla
      If dtrTmp.RowState = DataRowState.Added Then Return False

      '------------------------
      'se ci sono note di prelievo evase in conto e la riga dell'ordine corrente ha evasione 'a saldo' test ed eventualmente blocco
      'per velocizzare faccio il test solo se la qta prenotata è diversa da 0, oppure se il prenotato è evaso a saldo
      If NTSCDec(dtrTmp!ec_quapre) <> 0 Or NTSCStr(dtrTmp!ec_flevapre) = "S" Then
        If Not oCldGsor.BloccaEvasione(strDittaCorrente, dtrTmp!ec_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                        dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                        NTSCInt(dtrTmp!ec_riga), dttTmp) Then Return True
        If dttTmp.Rows.Count > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128551683855636000, "Attenzione! " & _
                      "La riga corrente n°|" & NTSCInt(dtrTmp!ec_riga).ToString & "| risulta essere evasa " & _
                      "con la Nota di Prelievo n°|" & dttTmp.Rows(0)!mm_numdoc.ToString & _
                      IIf(NTSCStr(dttTmp.Rows(0)!mm_serie) <> " ", "/" & NTSCStr(dttTmp.Rows(0)!mm_serie), "").ToString & _
                      "| del |" & dttTmp.Rows(0)!mm_anno.ToString & "|, nella riga |" & dttTmp.Rows(0)!mm_riga.ToString & "|: tale riga di nota di prelievo è ancora inevasa. " & _
                      "Modifica manuale del saldo (Evasione totale) non possibile.")))
          dttTmp.Clear()
          Return True
        End If
        dttTmp.Clear()
      End If    'If NTSCDec(dtrTmp!ec_quapre) <> 0 Or NTSCStr(dtrTmp!ec_flevapre) = "S" Then

      'anche negli impegni collegati
      'per velocizzare faccio il test solo se la qta prenotata è diversa da 0, oppure se il prenotato è evaso a saldo
      If dttET.Rows(0)!et_tipork.ToString = "H" Then
        dtrT = dttECIMP.Select("ec_quapre <> 0 or ec_flevapre = 'S'")
        If dtrT.Length > 0 Then
          If Not oCldGsor.BloccaEvasioneY(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                          dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                          NTSCInt(dtrTmp!ec_riga), dttTmp) Then Return True
          If dttTmp.Rows.Count > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128551685805948000, "Attenzione! " & _
                        "La riga corrente n°|" & NTSCInt(dtrTmp!ec_riga).ToString & "| possiede righe negli Impegni collegati " & _
                        "che risultano essere evase con la Nota di Prelievo n°|" & dttTmp.Rows(0)!mm_numdoc.ToString & _
                        IIf(NTSCStr(dttTmp.Rows(0)!mm_serie) <> " ", "/" & NTSCStr(dttTmp.Rows(0)!mm_serie), "").ToString & _
                        "| del |" & dttTmp.Rows(0)!mm_anno.ToString & "|, nella riga |" & dttTmp.Rows(0)!mm_riga.ToString & "|: tale riga di nota di prelievo è ancora inevasa. " & _
                        "Modifica manuale del saldo (Evasione totale) non possibile.")))
            dttTmp.Clear()
            Return True
          End If
          dttTmp.Clear()
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_datcons(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bNew = True Then e.Row!ec_datconsor = e.ProposedValue
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

  Public Overridable Function BeforeColUpdate_CORPO_ec_oaserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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
  Public Overridable Function BeforeColUpdate_CORPO_ec_oqserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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

End Class

