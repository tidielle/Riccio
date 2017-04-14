Imports NTSInformatica.CLN__STD
Imports System.IO

Public Class CLE__GPOS
  Inherits CLE__BASE

  Public dttPOS As New DataTable
  Protected lUltimoTipoPagamentoPOS As TipoPagamentoPOS
  Protected _strUltimoComando As String 'Riporta l'ultimo comando inviato al POS
  Public ReadOnly Property strUltimoComando() As String
    Get
      Return _strUltimoComando
    End Get
  End Property

  Public Enum TipoPagamentoPOS
    GestitoDaPOS = 0
    ForzaCartaDiDebito = 1
    ForzaCartaDiCredito = 2
    ForzaAltreCarte = 3
  End Enum

  'Verifica che il POS esista e carica le informazioni necessarie per le operazioni successive
  Public Overridable Function InizializzaPOS(ByVal strDitta As String, ByVal lCodicePOS As Integer) As Boolean
    Try
      If Not ocldBase.ValCodiceDb(lCodicePOS.ToString, strDitta, "TABCPOS", "N", , dttPOS) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130906733978759174, "POS non codificato. Impossibile procedere con l'inizializzazione.")))
        dttPOS = New DataTable
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

#Region "Invio Comandi POS"
  'Richiesta di pagamento al POS
  Public Overridable Function InviaPagamento(ByVal dImporto As Decimal, ByVal lTipoPagamento As TipoPagamentoPOS) As Boolean
    Try
      'Verifica che il POS sia stato inizializzato
      If Not VerificaPosInizializzato() Then Return False

      'L'ultimo tipo pagamento POS passato
      lUltimoTipoPagamentoPOS = lTipoPagamento
      'Se ho impostato nella configurazione che non deve bloccare il tipo pagamento, allora forzo sempre il tipo pagamento = Gestito Da POS
      lTipoPagamento = VerificheTipoPagamento(lTipoPagamento)

      'Scrive il file di transito
      'A = Importo in centesimi (attenzione, in modalità DEMO è possibile effettuare transazioni di massimo 10 centesimi)
      'P = Tipo pagamento, vedi enumerazione
      'T = ID terminale (opzionale), attualmente non gestito
      'ESEMPIO: POS_PAGAME,A3000,P2
      If Not InviaComando("POS_PAGAME,A" & (dImporto * 100).ToString("0") & ",P" & lTipoPagamento) Then Return False

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

  'Richiesta di storno
  Public Overridable Function InviaStorno(ByVal dImporto As Decimal, ByVal lTipoPagamento As TipoPagamentoPOS) As Boolean
    Try
      'Verifica che il POS sia stato inizializzato
      If Not VerificaPosInizializzato() Then Return False

      'L'ultimo tipo pagamento POS passato
      lUltimoTipoPagamentoPOS = lTipoPagamento
      'Se ho impostato nella configurazione che non deve bloccare il tipo pagamento, allora forzo sempre il tipo pagamento = Gestito Da POS
      lTipoPagamento = VerificheTipoPagamento(lTipoPagamento)

      'Scrive il file di transito
      'A = Importo in centesimi (attenzione, in modalità DEMO è possibile effettuare transazioni di massimo 10 centesimi)
      'P = Tipo pagamento, vedi enumerazione
      'T = ID terminale (opzionale), attualmente non gestito
      'ESEMPIO: POS_STORNO,A3000,P2
      If Not InviaComando("POS_STORNO,A" & (dImporto * 100).ToString("0") & ",P" & lTipoPagamento.ToString("0")) Then Return False

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

  'Richiesta totali terminale
  Public Overridable Function RichiestaTotaliTerminale() As Boolean
    Try
      'Verifica che il POS sia stato inizializzato
      If Not VerificaPosInizializzato() Then Return False

      'Scrive il file di transito
      'ESEMPIO: POS_TOTTER
      If Not InviaComando("POS_TOTTER") Then Return False

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

  'Richiesta Chiusura Cassa
  Public Overridable Function RichiestaChiusuraCassa() As Boolean
    Try
      'Verifica che il POS sia stato inizializzato
      If Not VerificaPosInizializzato() Then Return False

      'Scrive il file di transito
      'ESEMPIO: POS_CHCASS
      If Not InviaComando("POS_CHCASS") Then Return False

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

  'Richiesta Stato Cassa
  Public Overridable Function RichiestaStatoCassa() As Boolean
    Try
      'Verifica che il POS sia stato inizializzato
      If Not VerificaPosInizializzato() Then Return False

      'Scrive il file di transito
      'ESEMPIO: POS_STATUS
      If Not InviaComando("POS_STATUS") Then Return False

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

#Region "Ricezione Esito Comunicazione"
  Protected dtStopTimer As Date 'Quando terminare la ricerca del file in caso non sia stata ricevuta nessuna risposta

  'Avvia un timer per effettuare la ricerca del file di risposta al comando inviato
  Protected Overridable Function AvviaRicezioneEsito() As Boolean
    Try
      'Imposta il tempo massimo di attesa del timer
      dtStopTimer = Now.AddSeconds(NTSCInt(dttPOS.Rows(0)!tb_timeout))

      'verifica la presenza del file ogni 250 millisecondi
      Dim tmTimerAs As New Timers.Timer(250)
      AddHandler tmTimerAs.Elapsed, AddressOf VerificaEsitoPos
      tmTimerAs.Start()

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

  'Verifica sul disco se il file di OutPut esiste e se è relativo all'ultimo comando inviato.
  Protected Overridable Sub VerificaEsitoPos(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
    Try
      'Timeout scaduto. Termino l'operazione
      If Now > dtStopTimer Then
        'Blocco il timer
        With CType(sender, Timers.Timer)
          .Stop()
          .Dispose()
        End With

        'Invia la segnalazione di timeout di business scaduto
        ThrowRemoteEvent(New NTSEventArgs("POS_ERROR", oApp.Tr(Me, 130906769696985340, "Il POS non ha risposto entro il tempo previsto.")))
        Return
      End If

      'Se il file di output non esiste attendo ulteriormente 
      If Not File.Exists(NTSCStr(dttPOS.Rows(0)!tb_fileout)) Then Return

      'Il file esiste, lo carico e valuto se è la risposta all'ultima richiesta effettuata.
      Dim srFile As New StreamReader(NTSCStr(dttPOS.Rows(0)!tb_fileout))
      Dim strEsito() As String = srFile.ReadToEnd().Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
      srFile.Close() 'chiudo il file che ho aperto in sola lettura

      'Elimina il file di risposta, ho in memoria quello che mi serve.
      File.Delete(NTSCStr(dttPOS.Rows(0)!tb_fileout))

      'Scompongo le risposte in un dictionary per averle disponibili in maniera più semplice
      Dim dicEsito As New Dictionary(Of String, String)
      For Each strRisposta As String In strEsito
        Dim strParti() As String = strRisposta.Split(New Char() {"="c}, 2)
        If strParti.Length > 1 Then dicEsito.Add(strParti(0).Trim, strParti(1).Trim) 'Prende la parte dopo l'uguale come risposta
      Next

      'Se non è la risposta all'ultimo comando inviato ingnoro il file
      If dicEsito("TIPO_COMANDO").IndexOf(strUltimoComando, StringComparison.CurrentCultureIgnoreCase) = -1 Then Return

      'Ho trovato un messaggio per me. Disattivo il timer
      With CType(sender, Timers.Timer)
        .Stop()
        .Dispose()
      End With

      'Scatena un evento per avvisare che l'operazione è terminata.
      If dicEsito("ESITO_COMANDO") <> "OK" OrElse dicEsito("ESITO_TRANSAZ") <> "00" Then
        ThrowRemoteEvent(New NTSEventArgs("POS_ERROR", dicEsito("ERROR_DESCRIZ")))
      Else
        Dim lCodPagamento As Integer = 0
        'Se la forma di pagamento non è bloccata, la ritorna.
        If NTSCStr(dttPOS.Rows(0)!tb_bloccapaga) = "N" Then
          'Se richiesto, ritorna la forma di pagamento utilizzata per la transazione, da utilizzare per aggiornare la forza pagamento del documento
          Select Case NTSCInt(dicEsito("POSDATA.CardType"))
            Case 1 'Carta di debito (solo se avevo indicato un tipo pagamento diverso forzo che deve comunicare il nuovo tipo pagamento)
              If lUltimoTipoPagamentoPOS <> TipoPagamentoPOS.ForzaCartaDiDebito Then lCodPagamento = NTSCInt(dttPOS.Rows(0)!tb_cfgnum1)
            Case 2 'Carta di credito (solo se avevo indicato un tipo pagamento diverso forzo che deve comunicare il nuovo tipo pagamento)
              If lUltimoTipoPagamentoPOS <> TipoPagamentoPOS.ForzaCartaDiCredito Then lCodPagamento = NTSCInt(dttPOS.Rows(0)!tb_cfgnum2)
          End Select
        End If

        ThrowRemoteEvent(New NTSEventArgs("POS_OK", "CodPaga=" & lCodPagamento & ";"))
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
#End Region

#Region "Metodi Interni"
  'è un messaggio interno, a regime non dovrebbe mai capitare. Se capita è colpa del programma che non chiama InizializzaPOS prima di inviare un comando
  Protected Overridable Function VerificaPosInizializzato() As Boolean
    Try
      If dttPOS.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130906739825587709, "Prima inizializzare il POS.")))
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

  'Se si è impostato di non forzare il tipo pagamento, allora indica sempre che deve essere gestito da POS, indifferentemente dalla richiesta a monte
  Protected Overridable Function VerificheTipoPagamento(ByVal lTipoPagamento As TipoPagamentoPOS) As TipoPagamentoPOS
    Try
      If NTSCStr(dttPOS.Rows(0)!tb_bloccapaga) = "N" Then Return TipoPagamentoPOS.GestitoDaPOS

      Return lTipoPagamento
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

  'Invia il comando al registratore
  Protected Overridable Function InviaComando(ByVal strComando As String) As Boolean
    Try
      'scrive il file nella cartella di transito
      Dim swFile As New StreamWriter(NTSCStr(dttPOS.Rows(0)!tb_filein))
      swFile.Write(strComando)
      swFile.Flush()
      swFile.Close()

      'Mantiene in memoria l'ultimo comando, utile per le verifiche successive nel file di output
      _strUltimoComando = strComando

      'Avvia la verifica dell'esito dell'operazione
      Return AvviaRicezioneEsito()
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
