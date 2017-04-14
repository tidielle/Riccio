Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGINVF
  Inherits CLE__BASN

  Private Moduli_P As Integer = bsModMG
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

  Public oCldInvf As CLDMGINVF
  Public lIITTArtinvf As Integer = 0
  Public lIITTArtinvtc As Integer = 0
  Public oCleBoll As CLEVEBOLL
  Public nCodivaStd As Integer = 0        'da utilizzare in fase di creazione documento di magazzino per gli articoli senza codiva indicato in artico
  Public nContropStd As Integer = 0       'da utilizzare in fase di creazione documento di magazzino per gli articoli senza controp indicata in artico
  Public bPrezziAnchesuScarichi As Boolean = False 'se true anche sulle righe di scarico viene inserito il prezzo (con le stesse logiche delle righe di carico)

  Public nCodivaFisso As Integer = 0      'se impostato, utilizza sempre questo, nella della riga documento

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGINVF"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldInvf = CType(MyBase.ocldBase, CLDMGINVF)
    oCldInvf.Init(oApp)


    Return True
  End Function

  Public Overridable Function IstanziaNTSCondCommerciali() As NTSCondCommerciali
    Try
      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEMGINVF", "BN__STD.NTSCondCommerciali", oTmp, strErr, False, "", "") = False Then
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

  Public Overridable Function TrovaNumdoc(ByVal nAnno As Integer, ByVal strSerie As String) As Integer
    Try
      Return oCldInvf.LegNuma(strDittaCorrente, "Z", strSerie, nAnno, False)

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

#Region "Validazione controlli di Form"
  Public Overridable Function edCodmaga_ValidatedAndChanged(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldInvf.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABMAGA", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792570256495000, "Codice magazzino inesistente")))
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
  Public Overridable Function edCodtpbf_ValidatedAndChanged(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldInvf.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABTPBF", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792571912677000, "Codice tipo bolla/fattura inesistente")))
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
  Public Overridable Function edCausale_ValidatedAndChanged(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldInvf.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABCAUM", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792571956357000, "Codice causale inesistente")))
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
  Public Overridable Function edCodconto_ValidatedAndChanged(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldInvf.ValCodiceDb(nCod.ToString, strDittaCorrente, "ANAGRACF", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792573443817000, "Codice cliente/fornitore inesistente")))
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

  Public Overridable Function TestPreElabora(ByVal strDatainv As String, ByVal nCodmaga As Integer, ByVal strOrig As String, _
                                              ByVal nDalistaOrig As Integer, ByVal nAlistaOrig As Integer, _
                                              ByVal nCausInv As Integer, ByVal strDest As String, _
                                              ByVal nDalistaDest As Integer, ByVal nAlistaDest As Integer, _
                                              ByVal strWhereArtico As String, ByVal nAnno As Integer, _
                                              ByVal strSerie As String, ByRef lNumdoc As Integer, _
                                              ByVal lCodconto As Integer, ByVal nCodtpbf As Integer, _
                                              ByVal nCodcauspiu As Integer, ByVal nCodcausmeno As Integer, _
                                              ByVal strTipVal As String, ByVal nListino As Integer, _
                                              ByRef strDtulap As String) As Boolean
    Dim dttTmp As New DataTable
    Dim bDisabilitaCheckLotti As Boolean = False
    Dim bDisabilitaCheckUbicaz As Boolean = False
    Dim bDisabilitaCheckCommesse As Boolean = False
    Dim bDisabilitaCheckFasi As Boolean = False
    Dim bDisabilitaCheckMatricole As Boolean = False
    Dim evnt As NTSEventArgs = Nothing
    Dim dttAccconf As New DataTable
    Dim strMsg As String = ""

    Try
      If nCodmaga = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792652188476000, "Il codice magazzino da elaborare deve essere diverso da 0")))
        Return False
      End If
      If strOrig = "M" And nCausInv = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792652530896000, "Quando l'origine inventario è 'Documento di magazzino' la causale di magazzino deve essere diversa da 0")))
        Return False
      End If
      If strDest = "A" And strWhereArtico = "." Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792653585300000, "Selezionare gli articoli prima di eseguire l'elaborazione")))
        Return False
      End If
      If lNumdoc = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792653884976000, "Il numero documento da generare deve essere diverso da 0")))
        Return False
      End If
      If strTipVal = "L" And nListino <= 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792654288860000, "Quando è selezionato come tipo di valorizzazione 'Listino' il numero di listino deve essere maggiore di 0")))
        Return False
      End If
      If lCodconto = 0 Or nCodtpbf = 0 Or nCodcauspiu = 0 Or nCodcausmeno = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792655066520000, "I seguenti campi devono essere diversi da 0: Codice Cliente/Fornitore, tipo Bolla/fattura, casuale di magazzino per rettifiche positive, causale di magazzino per rettifiche negative")))
        Return False
      End If
      If Not oCldInvf.ValCodiceDb(nCodcauspiu.ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp) Then Return False
      If NTSCInt(dttTmp.Rows(0)!tb_esist) <> 1 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129309133802021485, "Il campo Caus. in rett. + deve contenere una causale di carico.")))
        Return False
      End If
      If Not oCldInvf.ValCodiceDb(nCodcausmeno.ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp) Then Return False
      If NTSCInt(dttTmp.Rows(0)!tb_esist) <> -1 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129309134125224610, "Il campo Caus. in rett. - deve contenere una causale di scarico.")))
        Return False
      End If
      dttTmp.Clear()


      If oCldInvf.LegNuma(strDittaCorrente, "Z", strSerie, nAnno, False) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792667692336000, "La serie |" & strSerie.ToUpper & "| non risulta inizializzata per l'anno |" & nAnno.ToString & "|.")))
        Return False
      End If

      oCldInvf.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttTmp)
      If NTSCDate(strDatainv) <= NTSCDate(dttTmp.Rows(0)!tb_dtulap) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792658212124000, "La data dell'inventario deve essere superiore alla data ultimo aggiornamento progressivi di magazzino (|" & NTSCDate(dttTmp.Rows(0)!tb_dtulap).ToShortDateString & "|)")))
        Return False
      End If
      strDtulap = NTSCDate(dttTmp.Rows(0)!tb_dtulap).ToShortDateString

      '----------------------------
      'controllo se l'utente può generare il documento con i parametri passati
      If oApp.oGvar.bGestAccconf Then
        CType(oCleComm, CLELBMENU).AccconfGetBlocchi(strDittaCorrente, "Z", dttAccconf)

        If oApp.oGvar.nGestAccconfMagaz = 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, "INS", strDittaCorrente, _
                                                         "Z", strSerie, nCodtpbf, 0, 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129543506273583985, "Utente non abilitato ad inserire documenti con tipo B/F |'" & nCodtpbf.ToString & "'|")))
            Return False
          End If
        End If    'If oApp.oGvar.nGestAccconfMagaz = 0 Then

        If oApp.oGvar.nGestAccconfMagaz = 1 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, "INS", strDittaCorrente, _
                                                         "Z", strSerie, 0, 0, nCodcauspiu, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129544212455878906, "Utente non abilitato ad inserire documenti con causale |'" & nCodcauspiu.ToString & "'|")))
            Return False
          End If
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, "INS", strDittaCorrente, _
                                                         "Z", strSerie, 0, 0, nCodcausmeno, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129544212438759765, "Utente non abilitato ad inserire documenti con causale |'" & nCodcausmeno.ToString & "'|")))
            Return False
          End If
        End If    'If oApp.oGvar.nGestAccconfMagaz = 1 Then

        If oApp.oGvar.nGestAccconfMagaz = 2 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, "INS", strDittaCorrente, _
                                                         "Z", strSerie, 0, nCodmaga, 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129544212205400390, "Utente non abilitato ad inserire documenti con magazzino |'" & nCodmaga.ToString & "'|")))
            Return False
          End If
        End If    'If oApp.oGvar.nGestAccconfMagaz = 2 Then
      End If    'If oApp.oGvar.bGestAccconf Then

      bDisabilitaCheckLotti = CBool(Val(oCldInvf.GetSettingBus("Bsveboll", "Opzioni", ".", "DisabilitaCheckLotti", "0", " ", "0"))) '-1=disabilita il controllo di riga e finale sulla obblig. del lotto
      bDisabilitaCheckUbicaz = CBool(Val(oCldInvf.GetSettingBus("Bsveboll", "Opzioni", ".", "DisabilitaCheckUbicaz", "0", " ", "0"))) '-1=disabilita il controllo di riga e finale sulla obblig. dell'ubicazione
      bDisabilitaCheckCommesse = CBool(Val(oCldInvf.GetSettingBus("Bsveboll", "Opzioni", ".", "DisabilitaCheckCommesse", "0", " ", "0"))) '-1=disabilita il controllo finale sulla obblig. del numero commessa
      bDisabilitaCheckFasi = CBool(Val(oCldInvf.GetSettingBus("Bsveboll", "Opzioni", ".", "DisabilitaCheckFasi", "0", " ", "0"))) '-1=disabilita il controllo finale sulla obblig. della fase
      bDisabilitaCheckMatricole = CBool(Val(oCldInvf.GetSettingBus("Bsveboll", "Opzioni", ".", "DisabilitaCheckMatricole", "0", " ", "0"))) '-1=disabilita il controllo finale sulla obblig. della matricola
      If bDisabilitaCheckLotti Or bDisabilitaCheckUbicaz Or bDisabilitaCheckCommesse Or bDisabilitaCheckFasi Or bDisabilitaCheckMatricole Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128867023681174841, _
                  "ATTENZIONE: impostando le opzioni " & vbCrLf & _
                  "Bsveboll/Opzioni/DisabilitaCheckLotti = -1" & vbCrLf & _
                  "Bsveboll/Opzioni/DisabilitaCheckUbicaz = -1" & vbCrLf & _
                  "Bsveboll/Opzioni/DisabilitaCheckCommesse = -1" & vbCrLf & _
                  "Bsveboll/Opzioni/DisabilitaCheckFasi = -1" & vbCrLf & _
                  "Bsveboll/Opzioni/DisabilitaCheckMatricole = -1" & vbCrLf & _
                  "l'elaborazione può portare alla generazione di documenti di rettifica non corretti." & vbCrLf & "Proseguire?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
      End If

      nCodivaStd = NTSCInt(oCldInvf.GetSettingBus("BSMGINVF", "OPZIONI", ".", "CodivaStd", "0", " ", "0"))
      nContropStd = NTSCInt(oCldInvf.GetSettingBus("BSMGINVF", "OPZIONI", ".", "ContropStd", "0", " ", "0"))
      bPrezziAnchesuScarichi = CBool(oCldInvf.GetSettingBus("BSMGINVF", "OPZIONI", ".", "PrezziAnchesuScarichi", "0", " ", "0"))

      If Not oCldInvf.ValCodiceDb(nCodivaStd.ToString, strDittaCorrente, "TABCIVA", "N") Then nCodivaStd = 0
      If Not oCldInvf.ValCodiceDb(nContropStd.ToString, strDittaCorrente, "TABCOVE", "N") Then nContropStd = 0

      If ((nCodivaStd = 0) Or (nContropStd = 0)) And (CLN__STD.FRIENDLY = False) Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 129025077687363047, _
                        "Attenzione!" & vbCrLf & _
                        "Non sono state settate le opzioni di registro:" & vbCrLf & _
                        " . BSMGINVF/OPZIONI/CodivaStd" & vbCrLf & _
                        " . BSMGINVF/OPZIONI/ContropStd" & vbCrLf & _
                        "Tali opzioni servono per poter inserire l'articolo la cui esistenza " & _
                        "è da correggere nel documento di magazzino di rettifica qualora nell'anagrafica articolo Codice IVA " & _
                        "e/o Codice contropartita acquisti è pari a 0." & vbCrLf & _
                        "Proseguire?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If strOrig = "L" Then
        dttTmp.Clear()
        dttTmp.Dispose()
        If oCldInvf.ControllaValiditaLISTSAR(strDittaCorrente, nDalistaOrig, nAlistaOrig, dttTmp) = False Then
          With dttTmp.Rows(0)
            If NTSCStr(!ar_gesfasi) = "S" Then strMsg = oApp.Tr(Me, 130595878064884863, "Fasi")
            If NTSCStr(!ar_gescomm) = "S" Then strMsg = oApp.Tr(Me, 130595878475639842, "Commesse")
            If NTSCStr(!ar_geslotti) = "S" Then strMsg = oApp.Tr(Me, 130595878796071324, "Lotti")
            If NTSCStr(!ar_gesubic) = "S" Then strMsg = oApp.Tr(Me, 130595880181314528, "Ubicazioni")
            If NTSCStr(!ar_gestmatr) = "S" Then strMsg = oApp.Tr(Me, 130595878979474188, "Matricole")
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130595880363838312, "Attenzione!" & vbCrLf & _
              "In Origine inventario, la Lista Selezionata Articoli N° '|" & NTSCStr(!lsa_codlsar) & "|'" & vbCrLf & _
              "contiene l'articolo '|" & NTSCStr(!lsa_codart) & "|'" & IIf(NTSCStr(!ar_descr).Trim <> "", " - |" & NTSCStr(!ar_descr) & "|", "").ToString & vbCrLf & _
              "gestito a |" & strMsg & "|." & vbCrLf & _
              "Inserire dati congruenti nel dettaglio della lista selezionata o eliminare l'articolo dalla lista.")))
          End With
          Return False
        End If
      End If
      If strDest = "L" Then
        dttTmp.Clear()
        dttTmp.Dispose()
        If oCldInvf.ControllaValiditaLISTSAR(strDittaCorrente, nDalistaDest, nAlistaDest, dttTmp) = False Then
          With dttTmp.Rows(0)
            If NTSCStr(!ar_gesfasi) = "S" Then strMsg = oApp.Tr(Me, 130595885590149291, "Fasi")
            If NTSCStr(!ar_gescomm) = "S" Then strMsg = oApp.Tr(Me, 130595885612356285, "Commesse")
            If NTSCStr(!ar_geslotti) = "S" Then strMsg = oApp.Tr(Me, 130595885630612337, "Lotti")
            If NTSCStr(!ar_gesubic) = "S" Then strMsg = oApp.Tr(Me, 130595885677488337, "Ubicazioni")
            If NTSCStr(!ar_gestmatr) = "S" Then strMsg = oApp.Tr(Me, 130595885698790300, "Matricole")
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130595885726417668, "Attenzione!" & vbCrLf & _
              "In Selezione articoli inventariati, la Lista Selezionata Articoli N° '|" & NTSCStr(!lsa_codlsar) & "|'" & vbCrLf & _
              "contiene l'articolo '|" & NTSCStr(!lsa_codart) & "|'" & IIf(NTSCStr(!ar_descr).Trim <> "", " - |" & NTSCStr(!ar_descr) & "|", "").ToString & vbCrLf & _
              "gestito a |" & strMsg & "|." & vbCrLf & _
              "Inserire dati congruenti nel dettaglio della lista selezionata o eliminare l'articolo dalla lista.")))
          End With
          Return False
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
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function Elabora(ByVal strDatainv As String, ByVal nCodmaga As Integer, ByVal strOrig As String, _
                                      ByVal nDalistaOrig As Integer, ByVal nAlistaOrig As Integer, _
                                      ByVal nCausInv As Integer, ByVal strDest As String, _
                                      ByVal nDalistaDest As Integer, ByVal nAlistaDest As Integer, _
                                      ByVal strWhereArtico As String, ByVal nAnno As Integer, _
                                      ByVal strSerie As String, ByRef lNumdoc As Integer, _
                                      ByVal lCodconto As Integer, ByVal nCodtpbf As Integer, _
                                      ByVal nCodcauspiu As Integer, ByVal nCodcausmeno As Integer, _
                                      ByVal strTipVal As String, ByVal nListino As Integer, _
                                      ByVal bElaboraNoInt As Boolean, ByRef bDocCreato As Boolean) As Boolean
    Dim bElabSenzaInterrInverti As Boolean = False        '-1/0: se = -1 flag selezionato nell'elaborazione NON vengono considerati gli articoli presenti in ORIGINE INVENTARIO ma non presenti in SELEZIONE DEGLI ARTICOLI INVETARIATI (in pratica fa lavorare il flag al contrario)
    Dim strSQLArticoliInventariati As String = ""         'contiene la where da utilizzare per avere l'elenco degli articoli inventariati (ovvero la base di partenza degli articoli da elaborare)
    Dim nElabora As Integer = 0   '0 = elab manuale (chiedo se art pres in 'origine' ma non in 'lista inventariati', 1 = aggiungo a 'lista inventariati' arti di 'origine' non presenti, 2 non aggiungo a 'lista inventariati' arti di 'origine' non presenti
    Dim strErr As String = ""
    Dim dttOrig As New DataTable                          'contiene gli articoli con esistenza effettiva riscontrata
    Dim strDtulap As String = ""
    Dim nTipoval As Integer = 0                           'tipo di valorizzazione da applicare per le rettifiche in +
    Dim dttTmp As New DataTable
    Dim dtrT1() As DataRow = Nothing
    Dim evnt As NTSEventArgs = Nothing
    Dim i As Integer = 0
    Dim strTmp As String = ""

    '-------------------
    'LISTSAR CON CODICE -1 contiene gli articoli da trattare (SELEZIONE ARTICOLI INVENTARIATI)
    '                      da lista o da articoli bloccati o da selezione articoli SENZA esistenze effettive

    'TTARTINVF contiene tutti gli articoli da trattare con l'esistenza come da BUS,
    '                      divisa per fase/commessa/lotto/ubicazione/matricola

    Try
      '------------------
      If Not LogStart("BNMGINVF", "Gestione inventario fisico" & vbCrLf) Then Return False
      bDocCreato = False
      strSerie = strSerie.ToUpper
      If strSerie = "" Then strSerie = " "
      If Not TestPreElabora(strDatainv, nCodmaga, strOrig, nDalistaOrig, nAlistaOrig, nCausInv, strDest, _
                            nDalistaDest, nAlistaDest, strWhereArtico, nAnno, strSerie, lNumdoc, _
                            lCodconto, nCodtpbf, nCodcauspiu, nCodcausmeno, strTipVal, nListino, strDtulap) Then Return False

      '-------------------
      'leggo le opzioni di reg.
      ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 128793596940666000, "Predisposizione ambiente in corso ...")))

      bElabSenzaInterrInverti = CBool(oCldInvf.GetSettingBus("BSMGINVF", "OPZIONI", ".", "ElabSenzaInterrInverti", "0", " ", "0"))       '-1/0: se = -1 flag selezionato nell'elaborazione NON vengono considerati gli articoli presenti in ORIGINE INVENTARIO ma non presenti in SELEZIONE DEGLI ARTICOLI INVETARIATI (in pratica fa lavorare il flag al contrario)

      If Not IstanziaVeboll() Then Return False
      oCldInvf.ResetTblInstId("TTARTINVF", False, lIITTArtinvf)
      oCldInvf.ResetTblInstId("TTARTINVTC", False, lIITTArtinvtc)

      '-------------------
      'gestione degli articoli presenti in origine inventario ma non in 'articoli da elaborare'
      If bElaboraNoInt Then
        If bElabSenzaInterrInverti Then
          nElabora = 2      'nell'elaborazione NON tratto gli articoli presenti in origine inventario ma non in selezione articoli inventariati
        Else
          nElabora = 1      'nell'elaborazione tratto anche gli articoli presenti in origine inventario ma non in selezione articoli inventariati
        End If
      Else
        nElabora = 0        'elaborazione manuale
      End If

      '------------------
      'Prima di tutto creo la lista selezionata con tutti i dati da elaborare (SELEZIONE DEGLI ARTICOLI INVENTARIATI)
      'ovvero articoli di Bus la cui esistenza potrebbe essere da correggere
      ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 128792750648230000, "Determinazione articoli da trattare in corso ...")))

      If Not oCldInvf.CreaListaSelezionata(strDittaCorrente, strDest, nDalistaDest, nAlistaDest, nCodmaga, _
                                           strWhereArtico, strSQLArticoliInventariati, strErr, strDatainv, nCausInv) Then
        If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
        Return False
      End If

      '------------------
      'ottengo l'elenco degli articoli con l'esitenza effettiva (DA ORIGINE INVENTARIO)
      ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 128792750669446000, "Determinazione articoli con esistenza accertata in corso ...")))
      If Not oCldInvf.GetOrigineInventario(strDittaCorrente, strDatainv, nCodmaga, strOrig, _
                                           nDalistaOrig, nAlistaOrig, nCausInv, dttOrig) Then Return False
      If dttOrig.Rows.Count = 0 Then
        If strOrig = "L" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792698453736000, _
                           "Origine inventario: Attenzione, non sono stati trovati articoli." & vbCrLf & _
                           "L'origine inventario potrebbe:" & vbCrLf & _
                           " --> non contenere articoli" & vbCrLf & _
                           " --> contenere articoli non reali" & vbCrLf & _
                           " --> non essere di tipo 'Completato'" & vbCrLf & _
                           " --> non appartenere al magazzino '|" & nCodmaga.ToString & "'|")))
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128792698466372000, _
                           "Non esistono documenti di magazzino con le caratteristiche specificate." & vbCrLf & _
                           "Controllare data documento (deve essere uguale alla data inventario), " & vbCrLf & _
                           "magazzino (deve essere uguale al magazzino da elaborare) e " & vbCrLf & _
                           "causale di magazzino (deve essere uguale a quella indicata a video).")))
        End If
        Return False
      Else
        'verifico per gli articoli DA ORIGINE INVENTARIO (ovvero essistenze effettive in magazzino)
        'che se articolo a lotti/commessa/matricola/ubicazione/... siano indicati lotto/matricola/ubicazione/commessa ...
        dtrT1 = dttOrig.Select("am_gescomm = 'S' AND am_commeca = 0 AND am_quant <> 0")
        If dtrT1.Length > 0 Then
          strTmp = ""
          For i = 0 To dtrT1.Length - 1
            strTmp += NTSCStr(dtrT1(i)!am_codart) & ", "
          Next
          strTmp = strTmp.Substring(0, strTmp.Length - 2)
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 129023230760478515, "ATTENZIONE: nell'origine inventario sono presenti |" & dtrT1.Length.ToString & "| articoli gestiti a COMMESSA con codice commessa = 0 e quantità diversa da 0 (|" & strTmp & "|)." & vbCrLf & "Proseguire?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
        End If
        dtrT1 = dttOrig.Select("am_geslotti = 'S' AND am_lotto = 0 AND am_quant <> 0")
        If dtrT1.Length > 0 Then
          strTmp = ""
          For i = 0 To dtrT1.Length - 1
            strTmp += NTSCStr(dtrT1(i)!am_codart) & ", "
          Next
          strTmp = strTmp.Substring(0, strTmp.Length - 2)
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 129023235751308593, "ATTENZIONE: nell'origine inventario sono presenti |" & dtrT1.Length.ToString & "| articoli gestiti a LOTTO con codice lotto = 0 e quantità diversa da 0 (|" & strTmp & "|)." & vbCrLf & "Proseguire?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
        End If
        dtrT1 = dttOrig.Select("am_gesubic = 'S' AND am_ubicaz = ' ' AND am_quant <> 0")
        If dtrT1.Length > 0 Then
          strTmp = ""
          For i = 0 To dtrT1.Length - 1
            strTmp += NTSCStr(dtrT1(i)!am_codart) & ", "
          Next
          strTmp = strTmp.Substring(0, strTmp.Length - 2)
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 129023236388818359, "ATTENZIONE: nell'origine inventario sono presenti |" & dtrT1.Length.ToString & "| articoli gestiti ad UBICAZIONE DINAMICA con codice ubicazione = ' ' e quantità diversa da 0 (|" & strTmp & "|)." & vbCrLf & "Proseguire?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
        End If
        dtrT1 = dttOrig.Select("am_gestmatr = 'S' AND am_matric is null AND am_quant <> 0")
        If dtrT1.Length > 0 Then
          strTmp = ""
          For i = 0 To dtrT1.Length - 1
            strTmp += NTSCStr(dtrT1(i)!am_codart) & ", "
          Next
          strTmp = strTmp.Substring(0, strTmp.Length - 2)
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 129023236842812500, "ATTENZIONE: nell'origine inventario sono presenti |" & dtrT1.Length.ToString & "| articoli gestiti a MATRICOLA con codice matricola = '' e quantità diversa da 0 (|" & strTmp & "|)." & vbCrLf & "Proseguire?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
        End If
      End If

      '------------------
      'Riempio la tabella temporanea delle esistenze TTARTINVF e TTARTINVTC
      'ovvero calcolo l'esistenza degli articoli in Business da artdefx/artdef/lotcdef + i 
      'movim di magazzino fino alla data fine elab
      ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 128792750565394000, "Calcolo dell'esistenza a data in corso ...")))
      If Not oCldInvf.RiempiTTArtinvf(strDittaCorrente, strDatainv, nCodmaga, strDtulap, lIITTArtinvf) Then Return False
      If CBool(ModuliExtDittaDitt(strDittaCorrente) And bsModExtTCO) Then
        ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 128792750615002000, "Calcolo dell'esistenza TCO a data in corso ...")))
        If Not oCldInvf.RiempiTTArtinvtc(strDittaCorrente, strDatainv, nCodmaga, strDtulap, nCausInv, lIITTArtinvtc) Then Return False
      End If

      '------------------
      'Avvio la fase di confronto degli articoli (non delle esistenze) fra l'inventario fisico e quello contabile
      ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 128792785602696000, "Confronto tra esistenza effettiva ed esistenza gestionale in corso ...")))
      Select Case strTipVal
        Case "M" : nTipoval = -2
        Case "U" : nTipoval = 0
        Case "A" : nTipoval = -1 '- ultimo costo compreso oneri accessori
        Case "L" : nTipoval = nListino
      End Select
      If Not ConfrontaListe(dttOrig, nTipoval, nCodmaga, strDatainv, nElabora, strSQLArticoliInventariati, bElaboraNoInt) Then Return False
      dttOrig.Clear()

      '--------------------------------
      'controllo che la sommatoria delle esistenze degli articoli a matricola calcolata su movmag-testmag-keymag dal 01/01/1900 a data fine elab
      'corrisponda a artdef + movmag-keymag-testmag , altrimenti la stampa dell'inventario non sarebbe corretta (visto che parte sempre da artdef ...)
      If Not oCldInvf.CheckEsistArtMatricola(strDittaCorrente, nCodmaga, lIITTArtinvf, strDtulap, strDatainv, dttTmp) Then Return False
      For Each dtrT As DataRow In dttTmp.Rows
        LogWrite(oApp.Tr(Me, 128794449716056000, "L'articolo '|" & dtrT!ap_codart.ToString & "|', fase |" & dtrT!ap_fase.ToString & _
                      "| è gestito a matricola e la sommatoria dei movimenti registrati in Business fino " & vbCrLf & _
                      "alla data fine elaborazione (|" & NTSCDec(dtrT!ap_esist).ToString(oApp.FormatQta) & "|) differisce" & vbCrLf & _
                      "dall'esistenza calcolata dai progressivi definitivi articoli/magazzino + i movimenti dalla data ultima " & _
                      "chisura di magazzino fino alla data fine elaborazione (|" & NTSCDec(dtrT!ap_ordin).ToString(oApp.FormatQta) & "|)." & vbCrLf & _
                      "Il risultato finale per l'articolo in oggetto non sarà corretto!"), True)
      Next
      dttTmp.Clear()

      '--------------------------------
      'verifico se c'è qualche esistenza da correggere: prelevo solo i record con esistenza bus diversa da esistenza effettiva
      If Not oCldInvf.GetArticoliDaCorreggere(strDittaCorrente, lIITTArtinvf, dttTmp) Then Return False
      If dttTmp.Rows.Count = 0 Then
        If CBool(ModuliExtDittaDitt(strDittaCorrente) And bsModExtTCO) Then
          If Not oCldInvf.GetArticoliDaCorreggereTCO(strDittaCorrente, lIITTArtinvf, dttTmp) Then Return False

          If dttTmp.Rows.Count = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129484674539341886, "Tutte le esistenze alla data indicata corrispondono con quelle indicate nell'inventario contabile.")))
            Return True
          End If
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128793527150810000, "Tutte le esistenze alla data indicata corrispondono con quelle indicate nell'inventario contabile.")))
          Return True
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se NON impostata l'opzione di registro "BSMGINVF\OPZIONI\CodivaFisso", controlla che i Codici Iva,
      '--- presenti nella tabella temporanea, non siano superiori a 8
      '--------------------------------------------------------------------------------------------------------------
      If nCodivaFisso = 0 Then
        If oCldInvf.CheckCodiciIvaPerElaborazione(strDittaCorrente, lIITTArtinvf) = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130395374152508392, "Attenzione!" & vbCrLf & _
            "Il documento da creare contiene più di 8 Codici Iva." & vbCrLf & _
            "Elaborazione non possibile.")))
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      'Ora TTARTINVF e' allineata/pronta, procedo generando il documento di magazzino con i relativi movimenti
      ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 128793347668180000, "Generazione documento di magazzino in corso ...")))
      GeneraDocMag(nAnno, strSerie, lNumdoc, dttTmp, bDocCreato, strDatainv, nCodmaga, lCodconto, nCodtpbf, nCodcauspiu, nCodcausmeno)

      ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128792552271809000, _
                        "Elaborazione completata." & vbCrLf & _
                        "Con la stampa sarà possibile visionare il saldo precedente ed il saldo attuale di ogni articolo 'corretto'")))

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
      dttOrig.Clear()
      LogStop()
    End Try
  End Function
  Public Overridable Function ConfrontaListe(ByRef dttOrig As DataTable, ByVal nTipoval As Integer, _
                                        ByVal nCodmaga As Integer, ByVal strDatainv As String, _
                                        ByVal nElabora As Integer, ByVal strSQLArticoliInventariati As String, _
                                        ByVal bElaboraNoInt As Boolean) As Boolean
    '   dttOrig                      contiene gli articoli da trattare con esistenza effettivamente riscontrata
    'nElabora: 0 = elab manuale (chiedo se art pres in 'origine' ma non in 'lista inventariati', 
    '          1 = aggiungo a 'lista inventariati' arti di 'origine' non presenti, 
    '          2 = non aggiungo a 'lista inventariati' arti di 'origine' non presenti
    Dim dttBus As New DataTable
    Dim nElabora1 As Integer = 0      '0 = da settare, -1 corrisponde ad nElabora = 1, 1 corrisponde ad nElabora = 2
    Dim bElabora As Boolean = False
    Dim evnt As NTSEventArgs = Nothing
    Dim strMsg As String = ""
    Dim nRec As Integer = 0
    Dim dttTmp As New DataTable

    Try

      '-------------------
      'per ogni articolo con esistenza effettiva cerco il corrispondente nell'elenco di bus
      For Each dtrOrig As DataRow In dttOrig.Rows
        '-------------------
        'testo se l'articolo esiste in TTARTINVF, ovvero l'elenco degli articoli inventariati con esistenza in BUS
        If Not oCldInvf.GetTTartinvfCodart(strDittaCorrente, lIITTArtinvf, nCodmaga, dtrOrig, dttBus) Then Return False
        If dttBus.Rows.Count = 0 Then 'INSERT
          If NTSCDec(dtrOrig!am_quant) = 0 Then
            ' non faccio niente
          Else
            bElabora = False
            If nElabora > 0 Or nElabora1 <> 0 Then
              If nElabora = 0 Then
                'elaborazione manuale: verifico cosa ho risposto alla precedente domanda con nElabora1
                If nElabora1 = -1 Then bElabora = True Else bElabora = False
              Else
                'elaborazione automatica: nElabora1 viene ignorato
                If nElabora = 1 Then bElabora = True Else bElabora = False
              End If
            Else
              If nElabora1 = 0 Then
                evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128793316395170000, _
                        "Attenzione. Nell'origine inventario sono presenti |" & NTSCDec(dtrOrig!am_quant).ToString(oApp.FormatQta) & _
                        "| unita' dell'articolo |" & dtrOrig!am_codart.ToString & "| fase |" & dtrOrig!am_fase.ToString & "|" & vbCrLf & _
                        " commessa |" & dtrOrig!am_commeca.ToString & "|, lotto |" & dtrOrig!am_lotto.ToString & "|" & _
                        ", ubicazione '|" & NTSCStr(dtrOrig!am_ubicaz) & "|', matricola '|" & NTSCStr(dtrOrig!am_matric) & "|'" & vbCrLf & _
                        "mentre tale articolo NON E' PRESENTE nella selezione degli articoli inventariati. Procedo con un carico contabile?" & vbCrLf & vbCrLf & _
                        "(La risposta verrà utilizzata per tutti gli articoli a parità di problematica durante l'elaborazione corrente)"))
                ThrowRemoteEvent(evnt)
                If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
                  bElabora = True
                  nElabora1 = -1
                Else
                  nElabora1 = 1
                End If
              End If
            End If

            If bElabora = True Then
              'il messaggio potrebbe essere dato anche se l'articolo c'è ma non era presente il lotto ...
              'avviso solo se nella lista articoli da trattare non c'è affatto l'articolo in analisi
              If Not oCldInvf.IsArticoInLista(strSQLArticoliInventariati, NTSCStr(dtrOrig!am_codart), NTSCInt(dtrOrig!am_fase)) Then
                strMsg = oApp.Tr(Me, 128793326727140000, _
                         "L'articolo |" & dtrOrig!am_codart.ToString & "| fase |" & dtrOrig!am_fase.ToString & "|" & _
                         " commessa |" & dtrOrig!am_commeca.ToString & "|, lotto |" & dtrOrig!am_lotto.ToString & "|" & _
                         ", ubicazione '|" & NTSCStr(dtrOrig!am_ubicaz) & "|', matricola '|" & NTSCStr(dtrOrig!am_matric) & "|'" & _
                         "' non è presente nella lista degli articoli da inventariare: " & _
                         "è stato aggiunto con quantità uguale a quella indicata nella lista/documento di " & _
                         "magazzino indicato nell'origine inventario senza tener conto dell'eventuale esistenza " & _
                         "contabile risultante nel magazzino in elaborazione")
                LogWrite(strMsg, True)
              End If

              'aggiungo l'articolo tra quelli da trattare (ma non gli imposto il prezzo, operazione che verrà eseguita in un secondo momento)
              If Not oCldInvf.AddArticoInTTArtinvf(strDittaCorrente, lIITTArtinvf, nCodmaga, dtrOrig, 0) Then Return False
            End If
          End If
        Else 'UPDATE
          'aggiorno quantità, prezzo e marco il record come da inserire nel doc di magazzino (ma non gli imposto il prezzo, operazione che verrà eseguita in un secondo momento)
          If Not oCldInvf.AggArticoInTTArtinvf(strDittaCorrente, lIITTArtinvf, nCodmaga, dtrOrig, 0) Then Return False
        End If    'If dttBus.Rows.Count = 0 Then
      Next    'For Each dtrOrig As DataRow In dttOrig.Rows

    
      '--------------------------------------------------------------------------------------------------------------
      ' Calcola il prezzo da applicare per tutti gli articoli\fasi presenti in TTARTINVF
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrArtico As DataRow In oCldInvf.SelezioneArticoliTTARTINVF(strDittaCorrente, lIITTArtinvf).Rows
        Dim dPrezzo As Decimal = 0
        'cerco il prezzo unitario in base al tipo di valorizzazione (variabile dPrezzo)
        Dim oCondCommerciali As NTSCondCommerciali = IstanziaNTSCondCommerciali()
        oCondCommerciali.bCalcolaPrezzo = True
        With oCondCommerciali.Input
          .strDitta = strDittaCorrente
          .strCodart = NTSCStr(dtrArtico!ap_codart)
          .nListino = nTipoval
          .nFase = NTSCInt(dtrArtico!ap_fase)
          .strTipoval = "P"
          .bConspromo = True
          .dtDatdoc = NTSCDate(strDatainv)
        End With
        CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
        dPrezzo = oCondCommerciali.OutputPrezzo.dPrezzo

        If Not oCldInvf.AggPrezziSuArticoliInTTArtinvf(strDittaCorrente, lIITTArtinvf, dtrArtico, dPrezzo) Then Return False
      Next
      '--------------------------------------------------------------------------------------------------------------

      'Ora controllo se in TTARTINVF ci sono record con il campo ap_vdaordi ancora uguale a 0
      'cioe articoli che non sono presenti nell'origine inventario a parità do lotto/matricola/....,
      'se li trovo chiedo all'utente cosa fare
      nRec = oCldInvf.CountArticoliNonPresentiInOrigInventario(strDittaCorrente, lIITTArtinvf)
      If nRec <> 0 Then
        bElabora = False
        If bElaboraNoInt Then
          If nElabora = 1 Then bElabora = True
        Else
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128793344689160000, _
                  "Attenzione." & vbCrLf & "Nella selezione degli articoli inventariati esistono |" & nRec & "|" & vbCrLf & _
                    " articoli non presenti invece nell'origine inventario (a parità di articolo/fase/lotto/commessa/ubicazione/matricola). " & _
                    vbCrLf & "Procedo con uno scarico contabile per portare l'esistenza = 0?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then bElabora = True
        End If
        If bElabora Then
          If Not oCldInvf.IgnoraArticoliNonPresentiInOrigInventario(strDittaCorrente, lIITTArtinvf) Then Return False
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
      dttBus.Clear()
      dttBus.Dispose()
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

#Region "Funzioni per creare il documento"
  Public Overridable Function IstanziaVeboll() As Boolean
    Dim dsBoll As New DataSet
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    Try
      If Not oCleBoll Is Nothing Then Return True

      '------------------------
      'inizializzo BEVEBOLL
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEMGINVF", "BEVEBOLL", oTmp, strErr, False, "", "") = False Then
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
      oCleBoll.bIgnoraBloccoQtaTaglieInvf = CBool(oCldInvf.GetSettingBus("BSMGINVF", "OPZIONI", ".", "GeneraDocNoBloccoQtaTaglie", "0", ".", "0"))

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
      If e.TipoEvento = "" Or e.TipoEvento.ToUpper = "MSG_INFO" Then
        LogWrite(e.Message, True)
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
  Public Overridable Function GeneraDocMag(ByVal nAnno As Integer, ByVal strSerie As String, _
                                           ByRef lNumdoc As Integer, ByRef dttArt As DataTable, _
                                           ByRef bDocCreato As Boolean, ByVal strDatainv As String, _
                                           ByVal nCodmaga As Integer, ByVal lCodconto As Integer, _
                                           ByVal nCodtpbf As Integer, ByVal nCodcauspiu As Integer, _
                                           ByVal nCodcausmeno As Integer) As Boolean
    Dim i As Integer = 0
    Dim nRow As Integer = -1
    Dim bOk As Boolean = False
    Dim dttArtTc As New DataTable
    Try

      '--------------------
      'cerco/verifico il numero documento
      If oCldInvf.EsisteDoc(strDittaCorrente, "Z", nAnno, strSerie, lNumdoc) Then
        lNumdoc += 1
      End If

      bOk = oCleBoll.bDisabilitaCheckAnnoData
      oCleBoll.bDisabilitaCheckAnnoData = True  'altrimenti se elaboro un documento dell'anno scoro sa inutilmente il mesagdio di data non compresa nell'anno
      If Not oCleBoll.NuovoDocumento(strDittaCorrente, "Z", nAnno, strSerie, lNumdoc) Then Return False
      oCleBoll.bDisabilitaCheckAnnoData = bOk
      bOk = False
      oCleBoll.bInNuovoDocSilent = True
      If Not GeneraDocMag_SettaTestata(strDatainv, nCodmaga, lCodconto, nCodtpbf, nCodcauspiu, nCodcausmeno) Then Return False
      oCleBoll.bInCreaDocDaGnor = True
      '--- Disabilita opzioni di registro per farla lavorare come VB6
      oCleBoll.strTestSottoCosto = "N"
      oCleBoll.strTestEsist = "N"
      oCleBoll.bTestEsistPerComm = False
      oCleBoll.strTestScomin = "N"
      oCleBoll.strTestEsist = "N"

      '--------------------
      For i = 0 To dttArt.Rows.Count - 1
        bOk = False
        nRow += 1
        If NTSCInt(nRow / 50) * 50 = nRow Then
          ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 129025109414238047, "Generazione documento di magazzino in corso (elaborazione record |" & nRow.ToString & "| di |" & dttArt.Rows.Count.ToString & "|) ...")))
        End If

        'carico il dettaglio TCO
        If CBool(ModuliExtDittaDitt(strDittaCorrente) And bsModExtTCO) Then
          If Not oCldInvf.GetTtartinvfTc(strDittaCorrente, lIITTArtinvtc, NTSCStr(dttArt.Rows(i)!ap_codart), _
                                         NTSCInt(dttArt.Rows(i)!ap_fase), nCodmaga, dttArtTc, dttArt.Rows(i)) Then Return False
        End If

        'scrivo la riga solo se c'è una differenza tra le esistenze
        If NTSCDec(dttArt.Rows(i)!ap_esist) <> NTSCDec(dttArt.Rows(i)!ap_daordi) Then
          bOk = True
        Else
          'verifico nel TCO
          If dttArtTc.Rows.Count > 0 Then
            If (NTSCDec(dttArtTc.Rows(0)!quant01) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant02) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant03) <> 0) Or _
              (NTSCDec(dttArtTc.Rows(0)!quant04) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant05) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant06) <> 0) Or _
              (NTSCDec(dttArtTc.Rows(0)!quant07) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant08) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant09) <> 0) Or _
              (NTSCDec(dttArtTc.Rows(0)!quant10) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant11) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant12) <> 0) Or _
              (NTSCDec(dttArtTc.Rows(0)!quant13) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant14) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant15) <> 0) Or _
              (NTSCDec(dttArtTc.Rows(0)!quant16) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant17) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant18) <> 0) Or _
              (NTSCDec(dttArtTc.Rows(0)!quant19) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant20) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant21) <> 0) Or _
              (NTSCDec(dttArtTc.Rows(0)!quant22) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant23) <> 0) Or (NTSCDec(dttArtTc.Rows(0)!quant24) <> 0) Then
              bOk = True
            End If
          End If
        End If
        If bOk Then
          GeneraDocMag_ScriviRigaDoc(i, dttArt.Rows(i), dttArtTc, nCodmaga, nCodcauspiu, nCodcausmeno)
        End If
      Next
      GeneraDocMag_SettaPiede()
      If Not oCleBoll.SalvaDocumento("N") Then Return False

      bDocCreato = True
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
      dttArtTc.Clear()
    End Try
  End Function
  Public Overridable Function GeneraDocMag_SettaTestata(ByVal strDatainv As String, ByVal nCodmaga As Integer, _
                                                        ByVal lCodconto As Integer, ByVal nCodtpbf As Integer, _
                                                        ByVal nCodcauspiu As Integer, ByVal nCodcausmeno As Integer) As Boolean
    Try
      oCleBoll.bDocEmesso = False     'altrimenti da il messaggio di documento emesso a fornitore se (come deve essere) il conto è NS stabilimento
      oCleBoll.bCheckLottiInScarico = False
      oCleBoll.dttET.Rows(0)!et_conto = lCodconto
      oCleBoll.bDocEmesso = True
      oCleBoll.dttET.Rows(0)!et_tipobf = nCodtpbf
      oCleBoll.dttET.Rows(0)!et_causale = nCodcauspiu
      oCleBoll.dttET.Rows(0)!et_magaz = nCodmaga
      oCleBoll.dttET.Rows(0)!et_magaz2 = 0
      oCleBoll.dttET.Rows(0)!et_datdoc = strDatainv
      oCleBoll.dttET.Rows(0)!et_ultagg = Now
      oCleBoll.dttET.Rows(0)!et_opnome = oApp.User.Nome

      If CBool((ModuliDittaDitt(strDittaCorrente) And CLN__STD.bsModExtTCO)) Then
        'con il modulo taglie e colori imposto anno e stagione da opzione di registro
        oCleBoll.dttET.Rows(0)!et_annotco = NTSCInt(oCldInvf.GetSettingBus("BSMGINVF", "OPZIONI", ".", "AnnoTCO", "0", " ", "0"))
        oCleBoll.dttET.Rows(0)!et_codstag = NTSCInt(oCldInvf.GetSettingBus("BSMGINVF", "OPZIONI", ".", "StagioneTCO", "0", " ", "0"))
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
      Return False
    End Try
  End Function
  Public Overridable Function GeneraDocMag_ScriviRigaDoc(ByVal i As Integer, ByVal dtrArt As DataRow, _
                                                         ByRef dttArtTc As DataTable, ByVal nCodmaga As Integer, _
                                                         ByVal nCodcauspiu As Integer, ByVal nCodcausmeno As Integer) As Boolean
    Dim strErr As String = ""
    Dim nRiga As Integer = 0
    Dim dQuantita As Decimal = 0
    Dim dQtaTc As Decimal = 0
    Dim dPrezzo As Decimal = 0
    Dim nCausale As Integer = 0
    Dim strUbicaz As String = ""
    Dim strMatric As String = ""
    Dim j As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      nRiga = i + 1

      dQuantita = NTSCDec(dtrArt!ap_esist) - NTSCDec(dtrArt!ap_daordi) 'contabile - fisico
      If dQuantita > 0 Then
        'contabile > fisico, scarico
        If bPrezziAnchesuScarichi Then
          dPrezzo = NTSCDec(dtrArt!ap_sommat)
        Else
          dPrezzo = 0
        End If
        nCausale = nCodcausmeno
      Else
        'contabile < fisico, carico
        dPrezzo = NTSCDec(dtrArt!ap_sommat)
        nCausale = nCodcauspiu
      End If
      dQuantita = NTSCDec(Math.Abs(dQuantita))

      strUbicaz = NTSCStr(dtrArt!ap_ubicaz)
      If strUbicaz = "" Then strUbicaz = " "
      strMatric = NTSCStr(dtrArt!ap_matric)
      If strMatric = "" Then strMatric = " "


      oCleBoll.bInImportRigheOrd = True
      oCleBoll.dttEC.Rows.Add(oCleBoll.dttEC.NewRow)
      With oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)
        'forzo la MovordOnAddNewRow
        !codditt = "."
        !codditt = strDittaCorrente
        !ec_riga = nRiga
        !ec_magaz = nCodmaga
        !ec_causale = nCausale
        !ec_codart = dtrArt!ap_codart
        !ec_unmis = !ec_ump
        !ec_fase = dtrArt!ap_fase
        !ec_quant = dQuantita
        !ec_prezzo = dPrezzo
        !ec_misura1 = NTSCDec(dtrArt!ap_esist)      'esistenza contabile
        !ec_misura2 = NTSCDec(dtrArt!ap_daordi)      'esistenza fisica
        !ec_lotto = dtrArt!ap_lotto
        !ec_ubicaz = strUbicaz
        !ec_commeca = dtrArt!ap_commeca
        If nCodivaFisso <> 0 Then !ec_codiva = nCodivaFisso
        If NTSCInt(!ec_codiva) = 0 Then !ec_codiva = nCodivaStd
        If NTSCInt(!ec_controp) = 0 Then !ec_controp = nContropStd
        !ec_magaz = nCodmaga  'se ho settato l'opzione di usare in veboll il magaz di stock, rimetto il magaz in elaborazione!!!

        '---------------------------
        'Se l'articolo è gestito a matricole inserisco il record in movmatr
        If NTSCStr(dtrArt!ar_gestmatr) = "S" Then
          oCleBoll.dttMOVMATR.Rows.Add(oCleBoll.dttMOVMATR.NewRow)
          oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!codditt = "."
          oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!codditt = strDittaCorrente
          oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_tipork = !ec_tipork
          oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_anno = !ec_anno
          oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_serie = !ec_serie
          oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_numdoc = !ec_numdoc
          oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_riga = !ec_riga
          oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_rigaa = 1
          oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_matric = strMatric
          oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1)!mma_quant = dQuantita
          oCleBoll.dttMOVMATR.Rows(oCleBoll.dttMOVMATR.Rows.Count - 1).AcceptChanges()
        End If

        '---------------------------
        'se articolo TCO, devo alimentare anche la quantità in CORPOTC
        'la riga è già stata create da beveboll, per cui devo solo inserire le quantità
        If NTSCInt(!xxo_codtagl) <> 0 And CBool(ModuliExtDittaDitt(strDittaCorrente) And bsModExtTCO) And dttArtTc.Rows.Count > 0 Then
          dtrT = oCleBoll.dttECTC.Select(" ec_tipork = " & CStrSQL(!ec_tipork) & " AND ec_anno = " & NTSCStr(!ec_anno) & _
                               " AND ec_serie = " & CStrSQL(!ec_serie) & " AND ec_numdoc = " & NTSCStr(!ec_numdoc) & _
                               " AND ec_riga = " & NTSCStr(!ec_riga))
          For j = 1 To 24
            dQtaTc = NTSCDec(dttArtTc.Rows(0)("quant" & j.ToString.PadLeft(2, "0"c)))
            If nCausale = nCodcauspiu Then
              'contabile < fisico, carico
              If dQtaTc < 0 Then
                'esistenza fisica maggiore e causale carico: devo aggiungere
                dQtaTc = Math.Abs(dQtaTc)
              Else
                'esistenza fisica minore e causale carico: devo togliere
                dQtaTc = Math.Abs(dQtaTc) * -1
              End If
            Else
              'contabile > fisico, scarico
              If dQtaTc < 0 Then
                'esistenza fisica maggiore e causale scarico: devo toglere
                dQtaTc = Math.Abs(dQtaTc) * -1
              Else
                'esistenza fisica minore e causale scarico: devo aggiungere
                dQtaTc = Math.Abs(dQtaTc)
              End If
            End If
            dtrT(0)("ec_quant" & j.ToString.PadLeft(2, "0"c)) = dQtaTc
          Next
        End If

      End With

      If Not GeneraDocMagScriviRigaDoc_Pers(oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1), dtrArt) Then
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
    End Try
  End Function
  Public Overridable Function GeneraDocMagScriviRigaDoc_Pers(ByRef dtrNew As DataRow, ByRef dtrOld As DataRow) As Boolean
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
  Public Overridable Function GeneraDocMag_SettaPiede() As Boolean
    Try
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
#End Region

  Public Overridable Function CheckArticoliTCO(ByVal nMagaz As Integer) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldInvf.CheckArticoliTCO(strDittaCorrente, nMagaz)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

End Class
