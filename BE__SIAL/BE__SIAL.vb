Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLE__SIAL
  Inherits CLE__BASN

  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
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

  Public oCldSial As CLD__SIAL
  Public oMenu As Object

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__SIAL"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldSial = CType(MyBase.ocldBase, CLD__SIAL)
    oCldSial.Init(oApp)

    Return True
  End Function

  Public Overridable Function Verifica_Genera_Alert(ByVal nTipoOperazione As Integer, ByVal strDitta As String, _
                                                     ByVal strProgramma As String, ByVal lIdEvento As Integer, _
                                                     ByRef lIdAlert As Integer, ByVal dttMsgOutParam As DataTable) As Boolean
    ' CONTROLLA SE C'E' UN ALERT DA GENERARE, E NEL CASO LO GENERA
    '
    ' IN: nTipoOperazione -> 0 = solo verifica
    '                     -> 1 = solo genera
    '                     -> 2 = verifica e genera
    '
    '     strCodditt      -> Ditta del programma chiamante
    '
    '     strProgramma    -> Programma chiamante
    '     lIdEvento       -> Id. dell'evento all'interno del programma chiamante
    '     objfmChiamante  -> riferimento al form del programma chiamante
    '
    '     lIdAlert        -> Alesets da verificare
    '     dttMsgOutParam  -> recordset dei messaggi ed altri eventuali parametri
    '                        per ogni alert da generare
    ' OU:
    '     Verifica_Genera_Alert ->  = True  significa Alert verificato/generato
    '                               = False significa Alert non verificato/non generato
    Try

      'If oApp.TipoProgramma = CLE__APP.TIPOPROGR.Winfowm Then
      '  ThrowRemoteEvent(New NTSEventArgs("", "asdfasdf"))
      'Else
      '  'errore 
      '  Throw New NTSException("aaaaaaaaa")
      'End If

      If oApp.Batch Then CLN__STD.WriteMsgBoxToLog(oApp.Tr(Me, 128745773912955000, "ALERT BATCH: verifica_esecuzione alert da programma '|" & strProgramma & "|', evento '|" & lIdEvento.ToString & "|', ID alert |" & lIdAlert & "|"))

      'preparo l'ambiente VB6
      'If oApp.User.NetOnly = False Then
      '  oCleScrt.CreaMsscript(strDittaCorrente, False, scrCtl, False, New Object)
      'End If

      '  If Not dttMsgOutParam Is Nothing Then dttMsgOutParam.Clear()
      If nTipoOperazione <> 1 Then
        If Not Verifica(strDitta, strProgramma, lIdEvento, lIdAlert, dttMsgOutParam) Then
          'da non generare
          Return False
        End If
      End If

      If nTipoOperazione <> 0 Then
        If dttMsgOutParam.Rows.Count > 0 Then
          If Not Genera(strDitta, strProgramma, lIdEvento, lIdAlert, dttMsgOutParam) Then
            'non generato, errore
            Return False
          End If
        Else
          'non c'era nulla da generare: farò aggiornare solo la data di ultima verifica
          Return True
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
      ''distruggo l'ambiente VB6
      'Try
      '  If oApp.User.NetOnly = False Then
      '    If Not oCleScrt Is Nothing Then oCleScrt.DistruggiMsscript()
      '    scrCtl = Nothing
      '  End If
      'Catch
      'End Try
    End Try
  End Function

  Public Overridable Function Verifica(ByVal strDitta As String, ByVal strProgramma As String, _
                                     ByVal lIdEvento As Integer, ByRef lIdAlert As Integer, _
                                     ByVal dttMsgOutParam As DataTable) As Boolean
    'INPUT
    'strCodditt        --> ditta del programma chiamante (= ' ' se tutte)
    '
    'strProgramma      --> programma chiamante (= '' se nessuno)
    'lIdEvento         --> id. evento del programma chiamante
    'objFormChiamante  --> riferimento al form chiamante (per vb-script)
    '
    'OUTPUT
    'lIdAlert       --> alesets da verificare
    '                   può arrivare già riempito [da TIMER]
    '                   OPPURE
    '                   essere individuato nella routine [da PROGRAMMA]
    '
    'dynMsgOutParam --> recordset dei messaggi ed altri eventuali parametri
    '                   per ogni alert da generare
    '                   può arrivare vuoto [da TIMER]
    '                   OPPURE
    '                   può arrivare già riempito [da PROGRAMMA]
    '
    'VERIFICA       --> True alert VERIFICATO
    '               --> False alert NON VERIFICATO (oppure ERRORE)
    Dim dttAle As New DataTable

    Try

      If strProgramma <> "" Then
        'Se chiamato da programma:
        'cerca il primo ALESETS (dovrebbe essercene 1 solo) con ID evento uguale a quello passatomi
        '[se nè trova per ditta e non per ditta, prende quello per ditta]
        oCldSial.GetAlertsFromProgrAndEvento(strDitta, strProgramma, lIdEvento, dttAle)
        If dttAle.Rows.Count = 0 Then Return False
        If NTSCStr(dttAle.Rows(0)!als_attivo) <> "S" Then Return False
        lIdAlert = NTSCInt(dttAle.Rows(0)!als_id)
      Else
        'Se chiamato da TIMER
        oCldSial.GetAlertsFromId(strDitta, lIdAlert, dttAle)
        If dttAle.Rows.Count = 0 Then Return False
        If NTSCStr(dttAle.Rows(0)!als_attivo) <> "S" Then Return False
      End If    'If strProgramma <> "" Then

      Return EseguiProcedura(strDitta, NTSCStr(dttAle.Rows(0)!als_codditt), NTSCInt(dttAle.Rows(0)!als_codprocc), dttMsgOutParam, True)

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
      dttAle.Clear()
    End Try
  End Function

  Public Overridable Function Genera(ByVal strDitta As String, ByVal strProgramma As String, _
                                       ByVal lIdEvento As Integer, ByRef lIdAlert As Integer, _
                                       ByVal dttMsgOutParam As DataTable) As Boolean
    'INPUT
    'strCodditt        --> ditta del programma chiamante (= ' ' se tutte)
    '
    'strProgramma      --> programma chiamante (= '' se nessuno)
    'lIdEvento         --> id. evento del programma chiamante
    'objFormChiamante  --> riferimento al form chiamante (per vb-script)
    '
    'lIdAlert       --> alesets da generare
    'dynMsgOutParam --> recordset dei messaggi ed altri eventuali parametri
    '                   per ogni alert da generare
    '
    'GENERA         --> True alert GENERATO
    '               --> False alert NON GENERATO/ERRORE
    Dim dttAlesets As New DataTable
    Dim dttOpSend As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim n As Integer = 0
    Dim strStandardMsg As String = ""
    Dim dttTmp As New DataTable
    Dim dttAlerts As New DataTable      'per insert into su alerts
    Dim dttCract As New DataTable       'per insert into su cract
    Dim dttCractopp As New DataTable    'per insert into su cractopp
    Dim lProgrId As Integer = 0
    Dim strOperatore As String = ""
    Dim strDestinatario As String = ""
    Dim strRagSoc As String = ""
    Dim strConto As String = ""
    Dim strOggetto As String = ""
    Dim strMitt As String = ""
    Dim strDest As String = ""
    Dim strEmail As String = ""
    Dim strRicevuto As String = "N"
    Dim strT() As String = Nothing
    Dim oSend As CLE__SEND = Nothing
    Dim strTmp As String = ""
    Dim bInviato As Boolean = False
    Dim strTesto As String = ""
    Dim lIndex As Integer = 0
    Dim oTmp As Object = Nothing
    Dim strError As String = ""
    Dim nRiga As Integer = 0
    '--Gestione buffer
    Dim Lnstr, nLoop, nRemain As Integer
    Dim strBuffer, strBuffer2 As String
    Dim strLogInUser As String = oApp.User.Nome
    Try
      dttMsgOutParam.AcceptChanges()

      '------------------------------
      'determino i destinatari
      If dttMsgOutParam.Rows.Count > 0 Then
        oCldSial.ValCodiceDb(lIdAlert.ToString, "", "ALESETS", "N", "", dttAlesets)
      End If
      If dttAlesets.Rows.Count = 0 Then Return True

      'determino la modalità di invio standard
      If NTSCStr(dttAlesets.Rows(0)!als_tipop) = "S" Then strStandardMsg += "1" 'popup
      If NTSCStr(dttAlesets.Rows(0)!als_tipom) = "S" Then strStandardMsg += "2" 'email
      If NTSCStr(dttAlesets.Rows(0)!als_tipoc) = "S" Then strStandardMsg += "3" 'attività CRM
      'If NTSCStr(dttAlesets.Rows(0)!als_tipoa) = "S" Then strStandardMsg += "4" 'attivita customer service
      If NTSCStr(dttAlesets.Rows(0)!als_tipoe) = "S" Then strStandardMsg += "5" 'proceudra
      'If NTSCStr(dttAlesets.Rows(0)!als_tipoz) = "S" Then strStandardMsg += "6" 'altro

      dttOpSend.Columns.Add("operat", GetType(String))
      dttOpSend.Columns.Add("tiposend", GetType(String))
      dttOpSend.Columns.Add("pcname", GetType(String))
      dttOpSend.Columns.Add("email", GetType(String))
      dttOpSend.AcceptChanges()

      '------------------------------
      'ottengo la struttura della tabella per l'insert into su alerts
      If Not oCldSial.GetTableStructure("ALERTS", False, dttAlerts) Then Return False
      If Not oCldSial.SetTableDefaultValueFromDB("ALERTS", dttAlerts.DataSet) Then Return False

      'ottengo la struttura della tabella per l'insert into su cract
      If Not oCldSial.GetTableStructure("CRACT", False, dttCract) Then Return False
      If Not oCldSial.SetTableDefaultValueFromDB("CRACT", dttCract.DataSet) Then Return False
      'ottengo la struttura della tabella per l'insert into su cractopp
      If Not oCldSial.GetTableStructure("CRACTOPP", False, dttCractopp) Then Return False
      If Not oCldSial.SetTableDefaultValueFromDB("CRACTOPP", dttCractopp.DataSet) Then Return False


      '  oSend = New CLE__SEND
      If Not CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BE__SIAL", "BE__SEND", oTmp, strError, False, "", "") Then
        If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
        Return False
      End If
      oSend = CType(oTmp, CLE__SEND)
      oSend.Init(oApp, Nothing, Nothing, "", False, "", "")
      AddHandler oSend.RemoteEvent, AddressOf GestisciEventiEntity_CLE__SEND

      '------------------------------
      'INIZIO LA GENERAZIONE
      For Each dtrMsg As DataRow In dttMsgOutParam.Rows
        bInviato = False

        'la ditta è sempre quella presa dal messaggio!!!
        strDitta = NTSCStr(dtrMsg!codditt)
        If strDitta.Trim = "" Then
          If dtrMsg(1).ToString.Trim = "" Then
            GoTo Prossimo
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129043948833320312, "L'evento/procedura |" & lIdEvento & "/" & lIdAlert & "| doveva inviare un messaggio ma nello script non è stato indicato il codice ditta. Il messaggio verrà scartato")))
          End If
        End If

        'ottengo i destinatari a cui inviare il messaggio
        If Not CercaDestinatari(strDitta, strStandardMsg, dttAlesets, dttOpSend) Then Return False

        'Mittente
        If NTSCStr(dttAlesets.Rows(0)!als_mitt).ToUpper = "CURRENT" Then
          strOperatore = oApp.User.Nome
        Else
          strOperatore = NTSCStr(dttAlesets.Rows(0)!als_mitt)
        End If
        'Nome e Cognome Mittente
        strMitt = strOperatore
        If Not oCldSial.GetOrganigMittente(dtrMsg!codditt.ToString, strOperatore, dttTmp) Then GoTo Mail
        If dttTmp.Rows.Count > 0 Then
          strMitt = NTSCStr(dttTmp.Rows(0)!og_descont).Trim & " " & NTSCStr(dttTmp.Rows(0)!og_descont2).Trim
        End If
        dttTmp.Clear()

Popup:
        '------------------------------
        '(1) POPUP
        dttAlerts.Clear()
        Dim arUser As New ArrayList
        For Each dtrOp As DataRow In dttOpSend.Select("tiposend like '%1%'")
          'prendo solo gli operatori che sono appartenenti all'organizzazione della ditta
          If NTSCStr(dtrOp!Operat).ToUpper.PadRight(5).Substring(0, 5) <> "CONTO" Then
            'Numeratore da TABNUMA
            strRicevuto = "N"
            lProgrId = oCldSial.LegNuma(dtrMsg!codditt.ToString, "AL", " ", 0, True)
            lProgrId = oCldSial.AggNuma(dtrMsg!codditt.ToString, "AL", " ", 0, lProgrId, True, False, "")

            'POST
            If CBool(oCldSial.GetSettingBus("OPZIONI", ".", ".", "SocialAbilitaAlertPost", "0", ".", "0")) AndAlso oCldSial.VerificaUtenteSocial(strOperatore) Then
              If oCldSial.VerificaUtenteSocial(NTSCStr(dtrOp!operat)) Then
                arUser.add(NTSCStr(dtrOp!operat))
                strRicevuto = "S"
              End If
            Else
              'POPUP
              If NTSCStr(dtrOp!pcname).Trim = "" Then Continue For
Riprova:
              Dim tcpClient As New System.Net.Sockets.TcpClient()
              Dim bConnected As Boolean = False
              n = 0
              Try
                tcpClient.Connect(NTSCStr(dtrOp!pcname), 1001)
              Catch ex As Exception
                GoTo passa
              End Try

              'Aspettiamo che si connetta
              i = 1
              While tcpClient.Connected = False And i < 32000
                i = i + 1
                Threading.Thread.Sleep(10)
              End While

              'Se connesso, manda l'alert
              Try
                If tcpClient.Connected Then
                  Dim netStream As System.Net.Sockets.NetworkStream = tcpClient.GetStream
                  If netStream.CanWrite Then
                    bConnected = True
                    If strMitt.Trim = "" Then strMitt = oApp.User.Nome.Replace(";", "")
                    strBuffer = "--*BS__SIAL;" & strMitt & ";" & NTSCStr(dttAlesets.Rows(0)!als_descale).Replace(";", "") & ";" & dtrMsg!strMsg.ToString.Replace(";", "") & vbCrLf & "[Ditta: '" & dtrMsg!codditt.ToString & "'  Id. Alert: " & lProgrId & "  Data/Ora: " & DateTime.Now & "]" & ";"
                    'parte prima del testo 289 char, per sicurezza spacchiamo in pacchetti da 7900 (infatti: 8192-290=7902)

                    'parte prima del testo 289 char, per sicurezza spacchiamo in pacchetti da 7900 (infatti: 8192-290=7902)
                    Lnstr = Len(strBuffer)
                    If Lnstr > 7900 Then
                      nLoop = NTSCInt(Math.Floor(Lnstr / 7900)) 'totale +
                      nRemain = Lnstr Mod 7900 'rimanenza
                    Else
                      nLoop = 0       'se + corto di 7900 o =
                      nRemain = Lnstr 'questa è la lunghezza del file
                    End If

                    For cn As Integer = 1 To nLoop
                      If cn = 1 Then
                        strBuffer2 = Mid(strBuffer, 7900 * (cn - 1) + 1, 7900) & ";"
                      Else
                        strBuffer2 = "--*;" & Mid(strBuffer, 7900 * (cn - 1) + 1, 7900) & ";"
                      End If
                      Dim sendBytes() As Byte = System.Text.Encoding.UTF8.GetBytes(strBuffer2)
                      netStream.Write(sendBytes, 0, sendBytes.Length)
                      'stai inviando 7900 bytes alla volta
                      'al ricevente che ricostruisce il
                      'pacchetto in output
                      Threading.Thread.Sleep(5000)
                    Next

                    If nRemain > 0 Then
                      If nLoop = 0 Then
                        'qui invii la rimanenza, ovvero l'ultimo pacchetto di bytes
                        strBuffer2 = Mid(strBuffer, (7900 * nLoop) + 1, Lnstr) & "*--"
                      Else
                        strBuffer2 = "--*;" & Mid(strBuffer, 7900 * nLoop + 1, nRemain) & "*--"
                      End If
                      Dim sendBytes() As Byte = System.Text.Encoding.UTF8.GetBytes(strBuffer2)
                      netStream.Write(sendBytes, 0, sendBytes.Length)
                    End If
                  End If
                Else
                  If bConnected Then tcpClient.Close()
                  bConnected = False
                  n = n + 1
                  If n <= 5 Then
                    Threading.Thread.Sleep(3000)
                    GoTo Riprova
                  End If
                End If
                If bConnected Then tcpClient.Close()
                strRicevuto = "S"
              Catch ex As Exception

              End Try
            End If

Passa:
            dttAlerts.Rows.Add(dttAlerts.NewRow())
            With dttAlerts.Rows(dttAlerts.Rows.Count - 1)
              !codditt = strDitta
              !ale_id = lProgrId
              !ale_mitt = strOperatore
              !ale_dest = dtrOp!operat
              !ale_destmach = dtrOp!pcname
              !ale_dataora = DateTime.Now
              !ale_alsid = dttAlesets.Rows(0)!als_id
              !ale_descr = dtrMsg!strMsg
              !ale_tipoal = "P"
              !ale_prior = dttAlesets.Rows(0)!als_prior
              !ale_ricevuto = strRicevuto
              !ale_email = DBNull.Value
            End With    'With dttAlerts.Rows(dttAlerts.Rows.Count - 1)
            bInviato = True
          End If    'If NTSCStr(dtrOp!Operat).ToUpper.PadRight(5).Substring(0, 5) <> "CONTO" Then
        Next    'For Each dtrOp As DataRow In dttOpSend.Select("tiposend like '%1%'")
        If arUser.Count > 0 Then
          'Ho la lista degli utenti social ai quali inviare il post
          Dim dsTopic As New DataSet
          'Sostituisco l'operatore corrente per registrare correttamente il post
          oApp.User.Nome = strOperatore

          dsTopic.Tables.Add(CreaStrutturaTabella("SUPOSTT"))
          dsTopic.Tables.Add(CreaStrutturaTabella("SUPOSTD"))
          dsTopic.Tables.Add(CreaStrutturaTabella("SUPOSTAL"))
          dsTopic.Tables.Add(CreaStrutturaTabella("SUPOSTAR"))
          dsTopic.Tables.Add(CreaStrutturaTabella("SUPOSTC"))
          dsTopic.Tables.Add(CreaStrutturaTabella("SUPOSTN"))

          Dim edMessaggio As New System.Windows.Forms.RichTextBox
          edMessaggio.ForeColor = Drawing.Color.White
          edMessaggio.Text = NTSCStr(dttAlesets.Rows(0)!als_descale) & vbCrLf & _
                             dtrMsg!strMsg.ToString & vbCrLf & _
                             "[Ditta: '" & dtrMsg!codditt.ToString & "'  Id. Alert: " & lProgrId & "  Data/Ora: " & DateTime.Now & "]"

          dsTopic.Tables("SUPOSTT").Rows.Add()
          dsTopic.Tables("SUPOSTT").Rows(0)!spt_testo = edMessaggio.Rtf
          dsTopic.Tables("SUPOSTT").Rows(0)!spt_nts = "N"

          oCldSial.GetDefaultSocial(strOperatore, dttTmp)
          If dttTmp.rows.count > 0 Then
            dsTopic.Tables("SUPOSTT").Rows(0)!spt_cancomm = dttTmp.Rows(0)!spt_cancomm
            dsTopic.Tables("SUPOSTT").Rows(0)!spt_canilike = dttTmp.Rows(0)!spt_canilike
            dsTopic.Tables("SUPOSTT").Rows(0)!spt_canmodi = dttTmp.Rows(0)!spt_canmodi
          Else
            dsTopic.Tables("SUPOSTT").Rows(0)!spt_cancomm = "S"
            dsTopic.Tables("SUPOSTT").Rows(0)!spt_canilike = "S"
            dsTopic.Tables("SUPOSTT").Rows(0)!spt_canmodi = "S"
          End If

          For i = 0 To arUser.Count - 1
            Dim lRelazione As Integer = oCldSial.GetIdRelazione(strOperatore, NTSCStr(arUser(i)))
            ' La relazione è = 0 quando uno dei 2 operatori non è un utente social o se l'operatore mittente e quello destinario sono uguali
            If lRelazione = 0 Then Continue For
            dsTopic.Tables("SUPOSTAR").Rows.Add()
            With dsTopic.Tables("SUPOSTAR").Rows(dsTopic.Tables("SUPOSTAR").Rows.Count - 1)
              !spar_relid = lRelazione
              !spar_cancomm = "D"
              !spar_canilike = "D"
              !spar_canmodi = "D"
            End With
          Next

          If dsTopic.Tables("SUPOSTAR").Rows.Count > 0 Then
            Dim oCleSoci As New CLEXXSOCI
            oCleSoci.Init(oApp, Nothing, oCleComm, "", False, "", "")
            oCleSoci.SalvaTopic(dsTopic)
          End If

          oApp.User.Nome = strLogInUser
        End If

        If dttAlerts.Rows.Count > 0 Then
          If Not oCldSial.ScriviAlerts(dttAlerts) Then Return False
          dttAlerts.Clear()
        End If
Mail:
        '------------------------------
        '(2) EMAIL
        dttAlerts.Clear()
        For Each dtrOp As DataRow In dttOpSend.Select("tiposend like '%2%'")
          strDest = NTSCStr(dtrOp!operat)
          strEmail = NTSCStr(dtrOp!email)
          If strEmail = "*" Then strEmail = "" 'l'* è stato messo solo sulle righe di tipo 'CONTO' per superare i test nella routine 'CercaDestinatari'
          strRagSoc = ""
          strConto = ""
          If NTSCStr(dtrOp!operat).ToUpper.PadRight(5).Substring(0, 5) = "CONTO" Then
            'potrebbe essere CONTO/RUOLO o solo CONTO:
            'devo trovare l'e-mail da anagra o organig del cliente ...
            'dtrMsg!strConto contiene il codice del cliente
            If NTSCInt(dtrMsg!strConto) <> 0 Then
              oCldSial.ValCodiceDb(NTSCInt(dtrMsg!strConto).ToString, strDitta, "ANAGRA", "N", "", dttTmp)
              If dttTmp.Rows.Count > 0 Then
                strConto = NTSCStr(dtrMsg!strConto)
                strRagSoc = NTSCStr(dttTmp.Rows(0)!an_descr1)
                strDest = NTSCStr(dttTmp.Rows(0)!an_descr1).PadRight(20).Substring(0, 20).Trim
                strEmail = NTSCStr(dttTmp.Rows(0)!an_email)
              End If
              dttTmp.Clear()
              'se ho indicato il ruolo, cerco in organig del cliente la prima persona con ruolo = quello passato
              'CONTO/AMCO oppure CONTO/RACQ
              If NTSCStr(dtrOp!operat).ToUpper.IndexOf("/") > -1 Then
                If Not oCldSial.GetOrganigDestinatariCliente(strDitta, NTSCInt(dtrMsg!strConto), NTSCStr(dtrOp!operat).ToUpper.Substring(NTSCStr(dtrOp!operat).ToUpper.IndexOf("/") + 1), dttTmp) Then Return False
                For i = 0 To dttTmp.Rows.Count - 1
                  If NTSCStr(dttTmp.Rows(i)!og_email).Trim <> "" Then
                    'strDest = NTSCStr(dttTmp.Rows(0)!og_descont).PadRight(20).Substring(0, 20).Trim
                    'no: come descrizione nella tabella alerts meto solo la descr. del cliente, non la persona del ruolo. 
                    'ci sono solo 20 caratteri, se metto la persona si potrebbe non capire a che cliente appartiene
                    strEmail += ";" & NTSCStr(dttTmp.Rows(i)!og_email)
                  End If
                Next
              End If
            Else
              'non posso inviare nulla
              strDest = ""
              strEmail = ""
            End If    'If NTSCInt(dtrMsg!strConto) <> 0 Then
          Else
            If NTSCInt(dtrMsg!strConto) <> 0 Then
              oCldSial.ValCodiceDb(NTSCInt(dtrMsg!strConto).ToString, strDitta, "ANAGRA", "N", "", dttTmp)
              If dttTmp.Rows.Count > 0 Then
                strConto = NTSCStr(dtrMsg!strConto)
                strRagSoc = NTSCStr(dttTmp.Rows(0)!an_descr1)
              End If
              dttTmp.Clear()
            End If
            strDest = NTSCStr(dtrOp!operat)
            strEmail = NTSCStr(dtrOp!email)
          End If

          strOggetto = NTSCStr(dttAlesets.Rows(0)!als_descale)
          strOggetto = strOggetto.Replace("[#RAGSOC]", strRagSoc)
          strOggetto = strOggetto.Replace("[#CONTO]", strConto)
          strOggetto = strOggetto.Replace("[#DATA]", Now.ToShortDateString)

          If strDest.Trim <> "" Then
            'se i destinatari sono più di uno (strDest separato da ;)
            'può essere solo il caso di destinatario cliente/ruolo 
            'scarto il primo, visto che è l'e-mail generica del cliente che mi ero memorizzato qualora non avessi trovato
            'in organig nessun record con ruolo = a quello richiesto a cui mandare l'e-mail
            'Numeratore da TABNUMA
            If NTSCStr(dtrOp!operat).ToUpper.PadRight(6).Substring(0, 6) = "CONTO/" Then
              strEmail = strEmail.Substring(strEmail.IndexOf(";") + 1)
            End If
            strT = strEmail.Split(";"c)
            For i = 0 To strT.Length - 1
              If strT(i).Trim <> "" Then
                strRicevuto = "N"
                lProgrId = oCldSial.LegNuma(dtrMsg!codditt.ToString, "AL", " ", 0, True)
                lProgrId = oCldSial.AggNuma(dtrMsg!codditt.ToString, "AL", " ", 0, lProgrId, True, False, "")

                Try
                  strTmp = ""  'errori di ritorno

                  strTesto = dtrMsg!strMsg.ToString
                  'Se è HTML lo deve aggiuengere dentro il body, altrimenti alla fine
                  If strTesto.ToLower.IndexOf("<html>") < strTesto.ToLower.IndexOf("<body>") And strTesto.ToLower.IndexOf("<body>") < strTesto.ToLower.IndexOf("</body>") And _
                     strTesto.ToLower.IndexOf("</body>") < strTesto.ToLower.IndexOf("</html>") And strTesto.ToLower.IndexOf("<html>") <> -1 Then

                    lIndex = strTesto.ToLower.IndexOf("</body>")
                    strTesto = strTesto.Substring(0, lIndex) & vbCrLf & _
                               "[Ditta: '" & dtrMsg!codditt.ToString & "'  Id. Alert: " & lProgrId & "  Data/Ora: " & DateTime.Now & "]" & _
                               strTesto.Substring(lIndex)
                  Else
                    strTesto &= vbCrLf & "[Ditta: '" & dtrMsg!codditt.ToString & "'  Id. Alert: " & lProgrId & "  Data/Ora: " & DateTime.Now & "]"
                  End If

                  Dim strTmpUser As String = oApp.User.Nome
                  oApp.User.Nome = strOperatore ' Così con business e-mail l'e-mail viene inviata dall'utente selezionato nell'alert
                  oSend.InviaMail(dtrMsg!codditt.ToString, strT(i), "", "", strOggetto, Nothing, False, strTesto, strTmp, "", 0, 0, "BN__SIAL", False)
                  oApp.User.Nome = strTmpUser
                  If strTmp = "" Then strRicevuto = "S"
                Catch ex As Exception
                End Try

                dttAlerts.Rows.Add(dttAlerts.NewRow())
                With dttAlerts.Rows(dttAlerts.Rows.Count - 1)
                  !codditt = strDitta
                  !ale_id = lProgrId
                  !ale_mitt = strOperatore
                  !ale_dest = strDest
                  !ale_destmach = DBNull.Value
                  !ale_dataora = DateTime.Now
                  !ale_alsid = dttAlesets.Rows(0)!als_id
                  !ale_descr = dtrMsg!strMsg
                  !ale_tipoal = "M"
                  !ale_prior = dttAlesets.Rows(0)!als_prior
                  !ale_ricevuto = strRicevuto
                  !ale_email = strT(i)
                End With    'With dttAlerts.Rows(dttAlerts.Rows.Count - 1)
                bInviato = True
              End If    'If strT(i).Trim <> "" Then
            Next    'For i = 0 To strT.Length - 1
          End If    'If strDest.Trim <> "" Then

        Next    'For Each dtrOp As DataRow In dttOpSend.Select("tiposend like '%2%'")
        If dttAlerts.Rows.Count > 0 Then
          If Not oCldSial.ScriviAlerts(dttAlerts) Then Return False
          dttAlerts.Clear()
        End If
Crm:
          '------------------------------
          '(3) ATTIVITA' CRM
          dttAlerts.Clear()
          For Each dtrOp As DataRow In dttOpSend.Select("tiposend like '%3%'")
            'prendo solo gli operatori che sono appartenenti all'organizzazione della ditta
            If NTSCStr(dtrOp!Operat).ToUpper.PadRight(5).Substring(0, 5) <> "CONTO" Then
              'Numeratore da TABNUMA
              strRicevuto = "N"
              lProgrId = oCldSial.LegNuma(dtrMsg!codditt.ToString, "AL", " ", 0, True)
              lProgrId = oCldSial.AggNuma(dtrMsg!codditt.ToString, "AL", " ", 0, lProgrId, True, False, "")

              strDestinatario = dtrOp!operat.ToString

              Try
                If strDestinatario.Trim <> "" Then
                  'Progressivo
                  l = oCldSial.LegNuma(dtrMsg!codditt.ToString, "C7", " ", 0, True)
                  l = oCldSial.AggNuma(dtrMsg!codditt.ToString, "C7", " ", 0, l, True, False, "")

                  'Calcolo della data prevista e dell'ora prevista
                  Dim dtTmp As DateTime = DateTime.Now
                  Dim dtDate As DateTime = NTSCDate(dtTmp.ToShortDateString)
                  Dim nOre As Integer = dtTmp.Hour
                  Dim nMinuti As Integer = dtTmp.Minute
                  Dim nSecondi As Integer = dtTmp.Second
                  Dim dDecimalidiOra As Decimal = ArrDbl(NTSCDec(((nMinuti * 60) + nSecondi) / 3600), 2)
                  Dim dOreAttuali As Decimal = nOre + dDecimalidiOra
                  Dim dOreTotali As Decimal = dOreAttuali + NTSCDec(dttAlesets.Rows(0)!als_addore)
                Dim nAddGiorni As Integer = NTSCInt(Math.Floor(dOreTotali / 24))

                  Dim dtDateNew As DateTime = DateAdd("d", nAddGiorni, dtDate)
                  Dim dOreNew As Decimal = dOreTotali - (nAddGiorni * 24)
                  If dOreNew < 0 Then dOreNew = 0

                  dttCract.Clear()
                  dttCract.Rows.Add(dttCract.NewRow())
                  With dttCract.Rows(dttCract.Rows.Count - 1)
                    !codditt = dtrMsg!codditt
                    !ca_codcrac = l
                    !ca_codlead = NTSCInt(dtrMsg!strCodlead)
                    !ca_oggetto = NTSCStr(dttAlesets.Rows(0)!als_oggetto)
                    !ca_codtaco = NTSCInt(dttAlesets.Rows(0)!als_codtaco)
                    !ca_status = "D"
                    !ca_note = dtrMsg!strMsg.ToString & vbCrLf & "[Ditta: '" & dtrMsg!codditt.ToString & "'  Id. Alert: " & lProgrId & "  Data/Ora: " & DateTime.Now & "]"
                    !ca_ultagg = DateTime.Now
                    !ca_opnome = strOperatore
                    !ca_dataprev = dtDateNew
                    !ca_oraprev = dOreNew
                    !ca_opnomeinc = NTSCStr(strDestinatario)
                  End With
                  If oCldSial.ScriviTabellaSemplice(dtrMsg!codditt.ToString, "CRACT", dttCract, "", "", "") Then
                    strRicevuto = "S"
                  End If
                  dttCract.Clear()

                  dttCractopp.Clear()
                  dttCractopp.Rows.Add(dttCractopp.NewRow())
                  With dttCractopp.Rows(dttCractopp.Rows.Count - 1)
                    !codditt = dtrMsg!codditt
                    !cap_codcrac = l
                    !cap_opcrmincpr = NTSCStr(strDestinatario)
                    !cap_status = "D"
                  End With
                  oCldSial.ScriviTabellaSemplice(dtrMsg!codditt.ToString, "CRACTOPP", dttCractopp, "", "", "")
                  dttCractopp.Clear()
                End If
              Catch ex As Exception
              End Try

              dttAlerts.Rows.Add(dttAlerts.NewRow())
              With dttAlerts.Rows(dttAlerts.Rows.Count - 1)
                !codditt = strDitta
                !ale_id = lProgrId
                !ale_mitt = strOperatore
                !ale_dest = dtrOp!operat
                !ale_destmach = DBNull.Value
                !ale_dataora = DateTime.Now
                !ale_alsid = dttAlesets.Rows(0)!als_id
                !ale_descr = dtrMsg!strMsg
                !ale_tipoal = "C"
                !ale_prior = dttAlesets.Rows(0)!als_prior
                !ale_ricevuto = strRicevuto
                !ale_email = DBNull.Value
              End With    'With dttAlerts.Rows(dttAlerts.Rows.Count - 1)
              bInviato = True
            End If    'If NTSCStr(dtrOp!Operat).ToUpper.PadRight(5).Substring(0, 5) <> "CONTO" Then
          Next    'For Each dtrOp As DataRow In dttOpSend.Select("tiposend like '%3%'")
          If dttAlerts.Rows.Count > 0 Then
            If Not oCldSial.ScriviAlerts(dttAlerts) Then Return False
            dttAlerts.Clear()
          End If
Customer:
          '------------------------------
          '(4) ATTIVITA Customer service: non gestito
          'dttAlerts.Clear()
          'For Each dtrOp As DataRow In dttOpSend.Select("tiposend like '%4%'")

          'Next     'For Each dtrOp As DataRow In dttOpSend.Select("tiposend like '%4%'")
          'If dttAlerts.Rows.Count > 0 Then
          '  If Not oCldSial.ScriviAlerts(strDitta, dttAlerts) Then Return False
          '  dttAlerts.Clear()
          'End If
Procedura:
          '------------------------------
          '(5) PROCEDURA
          dttAlerts.Clear()
          If dttAlesets.Rows(0)!als_tipoe.ToString = "S" Then
            Try
              'Progressivo
              lProgrId = oCldSial.LegNuma(dtrMsg!codditt.ToString, "AL", " ", 0, True)
              lProgrId = oCldSial.AggNuma(dtrMsg!codditt.ToString, "AL", " ", 0, lProgrId, True, False, "")

              'Esecuzione della procedura
              strRicevuto = "N"
              If NTSCInt(dttAlesets.Rows(0)!als_codproce) > 0 Then
                If EseguiProcedura(strDitta, NTSCStr(dttAlesets.Rows(0)!als_codditt), NTSCInt(dttAlesets.Rows(0)!als_codproce), dttMsgOutParam, False, nRiga) Then
                  strRicevuto = "S"
                End If
              End If
            Catch ex As Exception
            End Try
            dttAlerts.Rows.Add(dttAlerts.NewRow())
            With dttAlerts.Rows(dttAlerts.Rows.Count - 1)
              !codditt = strDitta
              !ale_id = lProgrId
              !ale_mitt = strOperatore
              !ale_dest = DBNull.Value
              !ale_destmach = DBNull.Value
              !ale_dataora = DateTime.Now
              !ale_alsid = dttAlesets.Rows(0)!als_id
              !ale_descr = dtrMsg!strMsg
              !ale_tipoal = "E"
              !ale_prior = dttAlesets.Rows(0)!als_prior
              !ale_ricevuto = strRicevuto
              !ale_email = DBNull.Value
            End With    'With dttAlerts.Rows(dttAlerts.Rows.Count - 1)
            bInviato = True
          End If    'If dttAlesets.Rows(0)!als_tipoe.ToString = "S" Then
          If dttAlerts.Rows.Count > 0 Then
            If Not oCldSial.ScriviAlerts(dttAlerts) Then Return False
            dttAlerts.Clear()
          End If
Altro:
          '------------------------------
          '(6) ALTRO: non gestito
          'dttAlerts.Clear()
          'For Each dtrOp As DataRow In dttOpSend.Select("tiposend like '%6%'")

          'Next     'For Each dtrOp As DataRow In dttOpSend.Select("tiposend like '%6%'")
          'If dttAlerts.Rows.Count > 0 Then
          '  If Not oCldSial.ScriviAlerts(strDitta, dttAlerts) Then Return False
          '  dttAlerts.Clear()
          'End If
Prossimo:
          If bInviato = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129043944765664062, "L'evento/procedura |" & lIdEvento & "/" & lIdAlert & "| doveva inviare un messaggio ma non è stato trovato nessun operatore a cui inviarlo (le cause potrebbero essere la mancanza dell'indirizzo e-mail, una non corretta configurazione degli operatori tra i ruoli dell'organizzazione, una non corretta impostazione dello script che genera il messaggio da inviare, ...)" & vbCrLf & "Testo del messaggio: |" & vbCrLf & NTSCStr(dtrMsg(1)) & "|")))
          End If

          nRiga += 1
      Next    'For Each dtrMsg As DataRow In dttMsgOutParam.Rows

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
      oApp.User.Nome = strLogInUser
      If Not oSend Is Nothing Then
        RemoveHandler oSend.RemoteEvent, AddressOf GestisciEventiEntity_CLE__SEND
        oSend = Nothing
      End If
      dttAlesets.Clear()
      dttOpSend.Clear()
      dttTmp.Clear()
      dttAlerts.Clear()
      dttCract.Clear()
    End Try
  End Function

  Public Overridable Sub GestisciEventiEntity_CLE__SEND(ByVal sender As Object, ByRef e As NTSEventArgs)
    'giro i messaggi di CLE__SEND alla mia ui ...
    ThrowRemoteEvent(e)
  End Sub


  Public Overridable Function CercaDestinatari(ByVal strDitta As String, ByVal strStandardMsg As String, ByRef dttAlesets As DataTable, ByRef dttOpSend As DataTable) As Boolean
    Dim ds As New DataSet
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim n As Integer = 0
    Dim strT() As String = Nothing
    Try
      dttOpSend.Clear()


      If NTSCStr(dttAlesets.Rows(0)!als_dest).ToUpper = "ALL" Then
        'non è prevista la possibilità di inviare informazioni a soggetti che non appartengono all'organizzazione della ditta
        oCldSial.LeggiTabellaSemplice(strDitta, "OPERAT", ds)
        For i = 0 To ds.Tables(0).Rows.Count - 1
          dttOpSend.Rows.Add(New Object() {NTSCStr(ds.Tables(0).Rows(i)!opnome).Trim, strStandardMsg, NTSCStr(ds.Tables(0).Rows(i)!opdefprinter), ""})
        Next
        ds.Tables(0).Clear()
        ds.Clear()
      Else
        strT = NTSCStr(dttAlesets.Rows(0)!als_dest).Split(";"c)
        For l = 0 To strT.Length - 1
          If strT(l).Trim <> "" Then
            n = strT(l).IndexOf("|")
            If n > -1 Then
              'ho specificato con quale sistema voglio inviare il messaggio
              dttOpSend.Rows.Add(New Object() {strT(l).Substring(0, n).Trim, strT(l).Substring(n + 1).Trim, "", ""})
            Else
              'invio mediante i flag standard
              dttOpSend.Rows.Add(New Object() {strT(l).Trim, strStandardMsg, "", ""})
            End If
          End If
        Next
        '-------------------------
        'se tra le modalità di invio c'è popup devo leggere il nome del pc
        If strStandardMsg.IndexOf("1") > -1 Or dttOpSend.Select("tiposend = '1'").Length > -1 Then
          For Each dtrT As DataRow In dttOpSend.Rows
            oCldSial.ValCodiceDb(dtrT!operat.ToString, strDitta, "OPERAT", "S", "", dttTmp)
            If dttTmp.Rows.Count > 0 Then dtrT!pcname = NTSCStr(dttTmp.Rows(0)!opdefprinter)
            dttTmp.Clear()
          Next
        End If
      End If

      '-------------------------
      'determino l'indirizzo e-mail
      For Each dtrT As DataRow In dttOpSend.Select("tiposend like '%2%'")
        'posso prendere l'e-mail solo degli ooperatori della ditta. per i clienti/fornitori lo farò dopo
        If NTSCStr(dtrT!operat).ToUpper.PadRight(5).Substring(0, 5) <> "CONTO" Then
          oCldSial.GetEmailBusUser(strDitta, NTSCStr(dtrT!operat))
          dtrT!email = oCldSial.GetEmailBusUser(strDitta, NTSCStr(dtrT!operat))
        Else
          dtrT!email = "*"
        End If
      Next

      '-------------------------
      'per tutti gli operatori che non hanno nomepc rimuovo tiposend POPUP (viene fatto dopo, perchè ora popup può essere anche post)
      'For Each dtrT As DataRow In dttOpSend.Select("tiposend like '%1%' AND pcname = ''")
      '  dtrT!tiposend = NTSCStr(dtrT!tiposend).Replace("1", "")
      '  dttTmp.Clear()
      'Next
      'per tutti gli operatori che non hanno email rimuovo tiposend EMAIL
      For Each dtrT As DataRow In dttOpSend.Select("tiposend like '%2%' AND email = ''")
        dtrT!tiposend = NTSCStr(dtrT!tiposend).Replace("2", "")
        dttTmp.Clear()
      Next
      'per se non c'è la procedura da eseguire rimuovo da tiposend
      If NTSCInt(dttAlesets.Rows(0)!als_codproce) = 0 Then
        For Each dtrT As DataRow In dttOpSend.Select("tiposend like '%5%'")
          dtrT!tiposend = NTSCStr(dtrT!tiposend).Replace("5", "")
          dttTmp.Clear()
        Next
      End If
      'rimuovo gli utenti senza alcun tipo di invio
      For Each dtrT As DataRow In dttOpSend.Select("tiposend = ''")
        dtrT.Delete()
      Next

      dttOpSend.AcceptChanges()

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
      dttTmp = Nothing
      ds.Tables.Clear()
      ds.Clear()
      ds = Nothing
    End Try
  End Function


  Public Overridable Function VisualizzaPopup(ByVal strNomePc As String, ByVal strCodditt As String, ByVal lIdAlert As Integer, ByVal strDataOra As String, ByVal strMitt As String, ByVal strDesAlert As String, ByVal strTesto As String) As Boolean
    Dim n As Integer = 0
    Dim i As Integer = 0
    Dim strRicevuto As String = "N"
    Try
Riprova:
      Dim tcpClient As New System.Net.Sockets.TcpClient()
      Dim bConnected As Boolean = False
      n = 0
      Try
        tcpClient.Connect(strNomePc, 1001)
      Catch ex As Exception
        Return False
      End Try

      'Aspettiamo che si connetta
      i = 1
      While tcpClient.Connected = False And i < 32000
        i = i + 1
        Threading.Thread.Sleep(10)
      End While

      'Se connesso, manda l'alert
      Try
        If tcpClient.Connected Then
          Dim netStream As System.Net.Sockets.NetworkStream = tcpClient.GetStream
          If netStream.CanWrite Then
            bConnected = True
            Dim sendBytes() As Byte = System.Text.Encoding.UTF8.GetBytes("--*BS__SIAL;" & strMitt & ";" & strDesAlert & ";" & strTesto & vbCrLf & "[Ditta: '" & strCodditt & "'  Id. Alert: " & lIdAlert & "  Data/Ora: " & strDataOra & "]")
            netStream.Write(sendBytes, 0, sendBytes.Length)
          End If
        Else
          If bConnected Then tcpClient.Close()
          bConnected = False
          n = n + 1
          If n <= 5 Then
            Threading.Thread.Sleep(3000)
            GoTo Riprova
          End If
        End If
        If bConnected Then tcpClient.Close()

        strRicevuto = "S"
      Catch ex As Exception

      End Try

      If strRicevuto = "S" Then
        If Not oCldSial.AggiornaInviatoPopup(strCodditt, lIdAlert) Then Return False
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

  Public Overridable Function EseguiProcedura(ByVal strCodditt As String, ByVal strAlesetDitta As String, ByVal lCodProcedura As Integer, ByRef dttMsgOutParam As DataTable, ByVal bVerifica As Boolean) As Boolean
    Try
      Return EseguiProcedura(strCodditt, strAlesetDitta, lCodProcedura, dttMsgOutParam, bVerifica, 0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function EseguiProcedura(ByVal strCodditt As String, ByVal strAlesetDitta As String, _
                                              ByVal lCodProcedura As Integer, ByRef dttMsgOutParam As DataTable, _
                                              ByVal bVerifica As Boolean, ByVal nRiga As Integer) As Boolean
    Dim dttProc As New DataTable
    Dim bOut As Boolean = False

    Dim strMsg(0) As String
    Dim strDitta(0) As String
    Dim strCodLead(0) As String
    Dim strConto(0) As String
    Dim strParam1(0) As String
    Dim strParam2(0) As String
    Dim strParam3(0) As String
    Dim strParam4(0) As String
    Dim strParam5(0) As String
    Dim strParam6(0) As String
    Dim strParam7(0) As String
    Dim strParam8(0) As String
    Dim strParam9(0) As String
    Dim strParam10(0) As String
    Dim strParam11(0) As String
    Dim strParam12(0) As String
    Dim strParam13(0) As String
    Dim strParam14(0) As String
    Dim strParam15(0) As String
    Dim strParam16(0) As String
    Dim strParam17(0) As String
    Dim strParam18(0) As String
    Dim strParam19(0) As String
    Dim strParam20(0) As String
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim oScriptMenu As INT__SCRIPT = Nothing
    Dim strProc As String = ""
    Dim strFile As String = ""
    Dim bProcInterna As Boolean = False

    Dim dttTmp As New DataTable

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodditt, strAlesetDitta, lCodProcedura, dttMsgOutParam, bVerifica, nRiga})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttMsgOutParam = CType(oIn(3), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      'Se non ho indicato una procedura, significa verificato
      If lCodProcedura = 0 Then Return True

      oCldSial.ValCodiceDb(lCodProcedura.ToString, "", "PROCED", "N", "", dttProc)
      'Se non ho trovato la procedura,
      'mi comporto come se non l'avessi indicata, quindi verificato
      If dttProc.Rows.Count = 0 Then Return True

      If NTSCStr(dttProc.Rows(0)!pr_nome).ToUpper.PadRight(10).Substring(0, 6) = "VBNET." Or _
         NTSCStr(dttProc.Rows(0)!pr_scriptnet).ToUpper = "S" Then
        'proceudra in vb.net
        Try
          If NTSCStr(dttProc.Rows(0)!pr_testo).ToUpper.PadRight(5).Substring(0, 5) = "FILE:" And _
             NTSCStr(dttProc.Rows(0)!pr_nome).ToUpper.PadRight(10).Substring(0, 6) = "VBNET." Then
            '-------------------------------
            'sistema su file esterno (presente da busnet 2010)
            'procedura che rimanda ad un file .NTS
            'la procedura deve iniziare per VBNET. (es VBNET.ProvaAlert) ed il testo della procedura deve essere:
            'FILE:BECGDCST.NTS
            'FUNCTION:ProvaAlert
            i = NTSCStr(dttProc.Rows(0)!pr_testo).ToUpper.IndexOf("FILE:")
            If i > -1 Then
              i += 5
              l = NTSCStr(dttProc.Rows(0)!pr_testo).ToUpper.IndexOf(vbCrLf, i + 1)
              If l = -1 Then l = NTSCStr(dttProc.Rows(0)!pr_testo).ToUpper.Length
              strFile = NTSCStr(dttProc.Rows(0)!pr_testo).ToUpper.Substring(i, l - i).Trim
            End If
            i = NTSCStr(dttProc.Rows(0)!pr_testo).ToUpper.IndexOf("FUNCTION:")
            If i > -1 Then
              i += 9
              l = NTSCStr(dttProc.Rows(0)!pr_testo).ToUpper.IndexOf(vbCrLf, i + 1)
              If l = -1 Then l = NTSCStr(dttProc.Rows(0)!pr_testo).ToUpper.Length
              strProc = NTSCStr(dttProc.Rows(0)!pr_testo).ToUpper.Substring(i, l - i).Trim
            End If
            If strFile = "" Then Throw New NTSException(oApp.Tr(Me, 128738459116204000, "Nella funzione '|" & NTSCStr(dttProc.Rows(0)!pr_nome) & "|' non è stato indicato il nome del file .NTS da compilare (es: FILE:BECGDCST.NTS). La procedura di ALERT verrà ignorata"))
            If strProc = "" Then Throw New NTSException(oApp.Tr(Me, 128738458179892000, "Nella funzione '|" & NTSCStr(dttProc.Rows(0)!pr_nome) & "|' non è stato indicato il nome della procedura da eseguire (es: FUNCTION:ProvaAlert). La procedura di ALERT verrà ignorata"))
            If Not System.IO.File.Exists(oApp.ServerDir + "\Script\" & strFile.ToUpper) Then
              Throw New NTSException(oApp.Tr(Me, 128745407868746000, "File |" & oApp.ServerDir + "\Script\" & strFile.ToUpper & "| non trovato"))
            End If
            oApp.MakeScript(oApp.ServerDir + "\Script\" & strFile.ToUpper, strFile.ToUpper.Replace(".NTS", "VBS"), oScriptMenu)
            bProcInterna = False
          Else
            '-------------------------------
            'procedura contenuta direttamente in arcproc.proced (solo da busnet 2011)
            strProc = NTSCStr(dttProc.Rows(0)!pr_nome)
            CreaAssemblyScriptDaProced(NTSCInt(dttProc.Rows(0)!pr_codice), oScriptMenu)
            bProcInterna = True
          End If

        Catch ex As Exception
          Throw ex
        End Try

        If Not oScriptMenu Is Nothing Then
          'solo se sono stato chiamato dalla routine di verifica devo ritornare il datatable modificato ...
          If bVerifica = False Then
            dttTmp = dttMsgOutParam.Clone
            For Each dtrT As DataRow In dttMsgOutParam.Rows
              dttTmp.ImportRow(dtrT)
            Next
            dttTmp.AcceptChanges()
            If bProcInterna Then
              Dim fun As System.Reflection.MethodInfo = CType(oScriptMenu, Object).GetType.GetMethod(strProc)  'occhio: è case_sensitive!!!!
              If Not fun Is Nothing Then bOut = CBool(fun.Invoke(CType(oScriptMenu, Object), New Object() {CType(oApp, Object), CType(IIf(strCodditt.Trim <> "", strCodditt, strAlesetDitta), Object), CType(dttTmp, Object)}))
            Else
              bOut = CBool(oScriptMenu.Exec(strProc, CType(oApp, Object), CType(IIf(strCodditt.Trim <> "", strCodditt, strAlesetDitta), Object), CType(dttTmp, Object)))
            End If
            dttTmp.Clear()
          Else
            If bProcInterna Then
              Dim fun As System.Reflection.MethodInfo = CType(oScriptMenu, Object).GetType.GetMethod(strProc)  'occhio: è case_sensitive!!!!
              If Not fun Is Nothing Then bOut = CBool(fun.Invoke(CType(oScriptMenu, Object), New Object() {CType(oApp, Object), CType(IIf(strCodditt.Trim <> "", strCodditt, strAlesetDitta), Object), CType(dttMsgOutParam, Object)}))
            Else
              bOut = CBool(oScriptMenu.Exec(strProc, CType(oApp, Object), CType(IIf(strCodditt.Trim <> "", strCodditt, strAlesetDitta), Object), CType(dttMsgOutParam, Object)))
            End If
          End If
          dttMsgOutParam.AcceptChanges()
          Return True   'restituisco true anche se la funzione, in fase di verifica, segnala che non deve fare nulla: true significa che la EseguiProcedura non ha dato errore. diversamente si comporterebbe in modo diverso rispetto a vb6!!!
        Else
          dttMsgOutParam.AcceptChanges()
          Return False
        End If
      Else
        'procedura in msscript (come in vb6)
        If oApp.User.NetOnly Then
          Throw New NTSException(oApp.Tr(Me, 129346609806826171, "Errore eseguendo la procedura '|" & lCodProcedura.ToString & "|': L'installazione è stata eseguita in modalità 'NET only', oppure l'utente con cui ci si è loggati è di tipo 'Net only' mentre la procedura da eseguire richiede il framework COM. Procedura non eseguita."))
          Return False
        End If    'If oApp.User.NetOnly Then
        If dttMsgOutParam.Rows.Count > 0 Then
          With dttMsgOutParam.Rows(nRiga)
            strMsg(0) = NTSCStr(!strMsg)
            strDitta(0) = NTSCStr(!codditt)
            strCodLead(0) = NTSCStr(!strCodLead)
            strConto(0) = NTSCStr(!strConto)
            strParam1(0) = NTSCStr(!strParam1)
            strParam2(0) = NTSCStr(!strParam2)
            strParam3(0) = NTSCStr(!strParam3)
            strParam4(0) = NTSCStr(!strParam4)
            strParam5(0) = NTSCStr(!strParam5)
            strParam6(0) = NTSCStr(!strParam6)
            strParam7(0) = NTSCStr(!strParam7)
            strParam8(0) = NTSCStr(!strParam8)
            strParam9(0) = NTSCStr(!strParam9)
            strParam10(0) = NTSCStr(!strParam10)
            strParam11(0) = NTSCStr(!strParam11)
            strParam12(0) = NTSCStr(!strParam12)
            strParam13(0) = NTSCStr(!strParam13)
            strParam14(0) = NTSCStr(!strParam14)
            strParam15(0) = NTSCStr(!strParam15)
            strParam16(0) = NTSCStr(!strParam16)
            strParam17(0) = NTSCStr(!strParam17)
            strParam18(0) = NTSCStr(!strParam18)
            strParam19(0) = NTSCStr(!strParam19)
            strParam20(0) = NTSCStr(!strParam20)
          End With
        End If    'If dttMsgOutParam.Rows.Count > 0 Then

        'vecchio sistema che pasa per bs--menu (comunque funzionante!)
        bOut = CType(oMenu, CLE__MENU).RunAlertProced(strDitta, lCodProcedura, strMsg, strCodLead, _
                                                     strParam1, strParam2, strParam3, strParam4, _
                                                     strParam5, strParam6, strParam7, strParam8, _
                                                     strParam9, strParam10, strParam11, strParam12, _
                                                     strParam13, strParam14, strParam15, strParam16, _
                                                     strParam17, strParam18, strParam19, strParam20, _
                                                     strConto, IIf(strCodditt.Trim <> "", strCodditt, strAlesetDitta).ToString)
        'nuovo sistema che si basa su bemsscrt -> bsmsscrt
        'TOLTO: il problema è che in VB6 in debug funziona tutto, mentre in compilato da errori strani perchè non riesce a distruggere gli oggetti, quindi quando li ricrea lancia la terminate della classe.
        'mi spiego meglio:
        'bsmsscrt crea std, crtb, crpe, libp, gctl, poi esegue la procedura
        'nella Distruggiambiente fa la distruzione inversa, ovvero gctl, libp, crpe, crtb, std (in debug si ma a runtime non distrugge l'std)
        'nell'esecuzione della procedura successiva ricrea std, crtb, crpe, libp, ma quando deve ricreare la gctl distrugge l'std e non lo ricrea (questo perchè non aveva fatto la terminate dell'std prima ...)
        'bOut = oCleScrt.RunAlertProced(strDitta, lCodProcedura, strMsg, strCodLead, _
        '                               strParam1, strParam2, strParam3, strParam4, _
        '                               strParam5, strParam6, strParam7, strParam8, _
        '                               strParam9, strParam10, strParam11, strParam12, _
        '                               strParam13, strParam14, strParam15, strParam16, _
        '                               strParam17, strParam18, strParam19, strParam20, _
        '                               strConto, IIf(strCodditt.Trim <> "", strCodditt, strAlesetDitta).ToString)

        If bOut = False Then
          'Non verificato
          Throw New NTSException(oApp.Tr(Me, 129043927587470703, "Errore eseguendo la procedura '|" & lCodProcedura.ToString & "|' (l'errore è contenuto nella procedura): errore" & vbCrLf) & strMsg(0))
          Return False
        Else
          If bVerifica Then
            'solo se sono stato chiamato dalla routine di verfiica devo ritornare il datatable modificato ...
            dttMsgOutParam.Clear()
            For i = 0 To strMsg.Length - 1
              If NTSCStr(strMsg(i)).Trim <> "" Then
                dttMsgOutParam.Rows.Add(dttMsgOutParam.NewRow())
                With dttMsgOutParam.Rows(dttMsgOutParam.Rows.Count - 1)
                  !codditt = strDitta(i)
                  !strMsg = strMsg(i)
                  !strCodLead = strCodLead(i)
                  !strConto = strConto(i)
                  !strParam1 = strParam1(i)
                  !strParam2 = strParam2(i)
                  !strParam3 = strParam3(i)
                  !strParam4 = strParam4(i)
                  !strParam5 = strParam5(i)
                  !strParam6 = strParam6(i)
                  !strParam7 = strParam7(i)
                  !strParam8 = strParam8(i)
                  !strParam9 = strParam9(i)
                  !strParam10 = strParam10(i)
                  !strParam11 = strParam11(i)
                  !strParam12 = strParam12(i)
                  !strParam13 = strParam13(i)
                  !strParam14 = strParam14(i)
                  !strParam15 = strParam15(i)
                  !strParam16 = strParam16(i)
                  !strParam17 = strParam17(i)
                  !strParam18 = strParam18(i)
                  !strParam19 = strParam19(i)
                  !strParam20 = strParam20(i)
                End With
              End If
            Next
            dttMsgOutParam.AcceptChanges()
          End If    'If bVerifica Then

          Return True
        End If    'If bOut = False Then
      End If    'If NTSCStr(dttProc.Rows(0)!pr_nome).ToUpper.PadRight(10).Substring(0, 6) = "VBNET." Then

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
      dttProc.Clear()
    End Try
  End Function

  Public Overridable Function CreaAssemblyScriptDaProced(ByVal lProced As Integer, ByRef oScriptMenu As INT__SCRIPT) As Boolean
    'creo 'assembly per gli script in .NET
    'devo mettere nell'assembly solo le proceudre net da precaricare e quella da richiamare
    Dim strScriptSource As String = ""
    Dim strSource As String = ""
    Dim strReference As String = ""
    Dim strImports As String = ""
    Dim strGlobals As String = ""
    Dim i As Integer = 0
    Dim e As Integer = 0
    Dim dttProced As New DataTable
    Try
      '----------------------------------------
      'infrastruttura del file di script
      strSource = "<nts>" & vbCrLf & _
            "*REFERENCE*" & vbCrLf & _
            "<scriptCode><![CDATA[" & vbCrLf & _
            "Imports Microsoft.VisualBasic" & vbCrLf & _
            "Imports System" & vbCrLf & _
            "Imports System.Collections" & vbCrLf & _
            "Imports System.Data" & vbCrLf & _
            "Imports NTSInformatica" & vbCrLf & _
            "Imports NTSInformatica.CLN__STD" & vbCrLf & _
            "*IMPORTS*" & vbCrLf & _
            " Public Class *CLASSNAME*" & vbCrLf & _
            "             Implements INT__SCRIPT" & vbCrLf & _
            "*GLOBALS*" & vbCrLf & _
            "   Public Function Exec(ByVal strCommand As String, byref oApp as Object, ByRef oIn As Object, ByRef oParam As Object) as Object Implements INT__SCRIPT.Exec" & vbCrLf & _
            "     Try" & vbCrLf & _
            "       Return Nothing" & vbCrLf & _
            "     Catch ex As Exception" & vbCrLf & _
            "       Dim strErr As String = CLN__STD.GestError(ex, Nothing, """", oApp.InfoError, oApp.ErrorLogFile, True)" & vbCrLf & _
            "       Return Nothing" & vbCrLf & _
            "     End Try" & vbCrLf & _
            "   End Function" & vbCrLf & _
            "   *CODE*" & vbCrLf & _
            " End Class" & vbCrLf & _
            "]]></scriptCode>" & vbCrLf & _
            "</nts>" & vbCrLf

      '----------------------------------------
      'ottengo il testo delle procedure. oltre a quelle sopra carico anche le procedure da precaricare sempre
      If Not oCldSial.GetProced(lProced, dttProced) Then Return False

      '----------------------------------------
      'creo il sorgente dell'assembly

      'funzioni da tabella proced
      'non devo caricare le funzioni con script di alert presenti su file esterni, utilizzati da BE__SIAL
      'dove il testo della funzione è:
      'FILE:BECGDCST.NTS
      'FUNCTION:ProvaAlert
      For Each dtrT As DataRow In dttProced.Rows
        If NTSCStr(dtrT!pr_testo).ToUpper.IndexOf("FILE:") = -1 And _
           NTSCStr(dtrT!pr_testo).ToUpper.IndexOf("FUNCTION:") = -1 Then
          strScriptSource += NTSCStr(dtrT!pr_testo) & vbCrLf
        End If
      Next

      'se nelle procedure (fuori dal testo) ho aggiunto delle reference o degli imports li includo
      '<reference assembly="BN__STD.dll" />
      i = strScriptSource.ToUpper.IndexOf("<REFERENCE ASSEMBLY=")
      While i > -1
        e = strScriptSource.ToUpper.IndexOf(vbCrLf, i + 1)
        strReference += strScriptSource.Substring(i, e - i) & vbCrLf
        strScriptSource = strScriptSource.Substring(0, i) & strScriptSource.Substring(e)
        i = strScriptSource.ToUpper.IndexOf("<REFERENCE ASSEMBLY=")
      End While
      strReference += "<reference assembly=""BEIEIMEX.DLL"" />" & vbCrLf
      If strReference <> "" Then RimuoviDuplicati(strReference)

      'Imports NTSInformatica.CLN__STD
      i = strScriptSource.ToUpper.IndexOf("IMPORTS ")
      While i > -1
        e = strScriptSource.ToUpper.IndexOf(vbCrLf, i + 1)
        strImports += strScriptSource.Substring(i, e - i) & vbCrLf
        strScriptSource = strScriptSource.Substring(0, i) & strScriptSource.Substring(e)
        i = strScriptSource.ToUpper.IndexOf("IMPORTS ")
      End While
      If strImports <> "" Then RimuoviDuplicati(strImports)

      'funzioni standard sempre caricate
      '  N.B. TUTTE le variabili dichiarate di sotto devono essere presenti anche nella funzione 
      '  BE__SIAL.CreaAssemblyScriptDaProced (destinata a creare gli script degli alert)
      '  in quanto carica anche gli script delle procedure precaricate e quindi le compila
      '--------------VARIABILI per IMEX/SIAL---------------------------------------------------------
      strGlobals += "Public oApp as CLE__APP = Nothing" & vbCrLf
      strGlobals += "Public oMenu as CLE__MENU = Nothing" & vbCrLf
      strGlobals += "Public oCleComm as CLELBMENU = Nothing" & vbCrLf
      strGlobals += "Public oCleImex as CLEIEIMEX = Nothing" & vbCrLf   'puntatore a nothing, però ta le funzioni precaricate potrebbero essercene qualcuna che usa l'oggetto imex e se non c'è questo darebbe errore in compilazione
      strGlobals += "Public DATA as New DataSet" & vbCrLf               'non utilizzato: vedi il commento della dichiarazione sopra
      strGlobals += "Public DATAIN as New DataSet" & vbCrLf             'non utilizzato: vedi il commento della dichiarazione sopra
      strGlobals += "Public DATAOU as New DataSet" & vbCrLf             'non utilizzato: vedi il commento della dichiarazione sopra
      strGlobals += "Public VARS As New Hashtable" & vbCrLf             'non utilizzato: vedi il commento della dichiarazione sopra
      '-------------------------------------------------------------------------------------------------------

      strSource = strSource.Replace("*REFERENCE*", strReference)
      strSource = strSource.Replace("*IMPORTS*", strImports)
      strSource = strSource.Replace("*GLOBALS*", strGlobals)
      strSource = strSource.Replace("*CLASSNAME*", "PROCED_" & lProced & "VBS")
      strSource = strSource.Replace("*CODE*", strScriptSource.Trim)

      'creo l'assembly con nome PROCED_ + codice profilo (es PROCED_1973)
      If Not oApp.MakeScript("", "PROCED_" & lProced & "VBS", oScriptMenu, strSource) Then Return False

      '----------------------------------------
      'passo alla classe le variabili globali
      Dim fi As System.Reflection.FieldInfo = Nothing
      fi = CType(oScriptMenu, Object).GetType.GetField("oApp")
      If Not fi Is Nothing Then fi.SetValue(oScriptMenu, oApp)
      'fi = CType(oScriptMenu, Object).GetType.GetField("oMenu")
      'If Not fi Is Nothing Then fi.SetValue(oScriptMenu, oMenu)
      fi = CType(oScriptMenu, Object).GetType.GetField("oCleComm")
      If Not fi Is Nothing Then fi.SetValue(oScriptMenu, oCleComm)
      'fi = CType(oScriptMenu, Object).GetType.GetField("oCleImex")
      'If Not fi Is Nothing Then fi.SetValue(oScriptMenu, Nothing) 'in questo contesto l'oggetto BEIEIMEX non è istanziato ...

      '----------------------------------------
      'memorizzo il puntatore alla collezione di variabili globali condivise tra me e oScrIe
      'fi = CType(oScriptMenu, Object).GetType.GetField("VARS")
      'VARS = CType(fi.GetValue(oScriptMenu), Hashtable)

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
      dttProced.Clear()
    End Try
  End Function
  Public Overridable Function RimuoviDuplicati(ByRef strIn As String) As Boolean
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Dim e As Integer = 0
    Dim strTmp As String = ""
    Try
      strT = strIn.Replace(vbCrLf, "§").Split("§"c)
      For i = 0 To strT.Length - 1
        If strT(i) <> "" Then
          For e = i + 1 To strT.Length - 1
            If strT(i).ToUpper = strT(e).ToUpper Then
              strT(e) = ""
            End If
          Next
          strTmp += strT(i) & vbCrLf
        End If
      Next
      strIn = strTmp.Trim
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


  Public Overridable Function CreaStrutturaTabella(ByVal strTabella As String) As DataTable
    Dim dttTmp As New DataTable
    Try
      oCldSial.GetTableStructure(strTabella, False, dttTmp)
      dttTmp.TableName = strTabella
      Return dttTmp.Copy
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
End Class
