Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGVAAR
  Inherits CLE__BASE

  Public oCldVaar As CLDMGVAAR

  Public bElabInCorso As Boolean = False

  Public lIITTInvent As Integer
  Public bSeleziona As Boolean
  Public strCampo As String
  Public strWhereFiar As String

  Public nTipo1 As Integer
  Public nTipo2 As Integer

  Public strNuovoValore As String
  Public strValore As String

  Public strCodart As String
  Public strDescr As String
  Public nRichiesta As Integer

  Public strTipo As String 'S stringa N numerico

  '__AUTM
  Public bAutmHasChanges As Boolean
  Public dsAutmShared As DataSet = Nothing
  Public strAutmCampo As String
  Public bAutmcbValoreVis As Boolean
  Public bAutmckSeleziona As Boolean
  Public nAutmcbNuovoValore As Integer
  Public strAutmedNuovoValore As String
  Public strAutmedValore As String
  Public bAutmedValoreVis As Boolean
  Public nAutmcbValore As Integer
  Public strAutmcbValore As String
  Public strAutmcbNuovoValore As String

  Private Moduli_P As Integer = bsModMG + bsModVE + bsModPM
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtCRM
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGVAAR"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldVaar = CType(MyBase.ocldBase, CLDMGVAAR)
    oCldVaar.Init(oApp)
    Return True
  End Function

  Public Overridable Function TestPreElabora(ByVal bckSeleziona As Boolean, ByVal bcbValoreVis As Boolean, ByVal strcbValore As String, ByVal strcbNuovoValore As String, ByVal stredValore As String, ByVal stredNuovoValore As String) As Boolean
    Dim evt As NTSEventArgs = Nothing
    Try

      '-------------------------------------------------------------------------------
      ' Controlla la validità della selezione
      '-------------------------------------------------------------------------------
      If bckSeleziona = True Then
        If bcbValoreVis Then
          If strcbValore = strcbNuovoValore Then
            evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128499856127071367, "Il nuovo valore indicato coincide con quello da sostituire." & vbCrLf & "Procedere ugualmente all'aggiornamento degli articoli?"))
            ThrowRemoteEvent(evt)
            If Not evt.RetValue = "YES" Then
              Return False
            End If
          End If
        Else
          If UCase(stredValore) = UCase(stredNuovoValore) Then
            evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 129030102487922801, "Il nuovo valore indicato coincide con quello da sostituire." & vbCrLf & "Procedere ugualmente all'aggiornamento degli articoli?"))
            ThrowRemoteEvent(evt)
            If Not evt.RetValue = "YES" Then
              Return False
            End If
          End If
        End If
      End If
      '-------------------------------------------------------------------------------
      ' Controlla se sono stati selezionati gli articoli
      '-------------------------------------------------------------------------------
      If bSeleziona = False Then
        evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 129030102533861477, "Non sono stati selezionati gli articoli." & vbCrLf & _
        "Passare ora alla selezione?"))
        ThrowRemoteEvent(evt)
        If Not evt.RetValue = "YES" Then
          Return False
        Else
          ThrowRemoteEvent(New NTSEventArgs("SELEZIONA:", ""))
          If bSeleziona = False Then Return False
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

  Public Overridable Function Elabora(ByRef lRecords As Integer, ByVal nTipoElaborazione As Integer, ByVal bckSeleziona As Boolean, ByVal bcbValoreVis As Boolean, ByVal strcbValore As String, ByVal strcbNuovoValore As String, ByVal stredValore As String, ByVal stredNuovoValore As String, ByVal ncbNuovoValore As Integer, ByVal bedValoreVis As Boolean, ByVal ncbValore As Integer, ByVal stredDataelab As String, ByVal strcbCampi As String, ByVal bcbNuovoValoreVis As Boolean, ByVal bckElaborazione As Boolean) As Boolean
    Dim bRet As Boolean
    Dim dsArtico As DataSet = Nothing
    Dim evt As NTSEventArgs = Nothing
    Dim strDesogglog As String = ""
    Dim i As Integer
    Try
      '-----------------------
      'test di rito
      If Not TestPreElabora(bckSeleziona, bcbValoreVis, strcbValore, strcbNuovoValore, stredValore, stredNuovoValore) Then Return False

      lRecords = 0

      bElabInCorso = True

      '-------------------------------------------------------------------------------
      ' Prende il nuovo valore da sostituire
      '-------------------------------------------------------------------------------
      SettaNuoviValori(stredNuovoValore, stredValore, bedValoreVis, bcbValoreVis, ncbNuovoValore, ncbValore)

      '-------------------------------------------------------------------------------
      ' Controlla se esistono articoli da aggiornare
      '-------------------------------------------------------------------------------
      oCldVaar.GetArtico(strDittaCorrente, strCampo, strValore, strWhereFiar, bckSeleziona, nTipo1, nTipo2, ncbNuovoValore, dsArtico)

      If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128503261138392657, "Non esistono articoli da aggiornare con queste caratteristiche.")))
        Return False
      End If

      '-------------------------------------------------------------------------------
      ' Chiede conferma dell'elaborazione
      '-------------------------------------------------------------------------------
      evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128503858759113833, "Procedere con l'aggiornamento degli articoli selezionati?"))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then
        Return False
      End If
      ThrowRemoteEvent(New NTSEventArgs("REFREINFO:", ""))
      '-------------------------------------------------------------------------------
      ' Se nTipoElaborazione <> 0 azzera la tabella temporanea
      '-------------------------------------------------------------------------------
      oCldVaar.ResetTblInstId("TTINVENT", False, lIITTInvent)
      '-------------------------------------------------------------------------------
      '--- Scrive una riga in ACTLOG, se attiva l'opzione di registro relativa
      '-------------------------------------------------------------------------------
      strDesogglog = "Variazione campi Anagrafica Articoli" & vbCrLf & vbCrLf & _
        " - Data di elaborazione..........: '" & stredDataelab & "'" & vbCrLf & _
        " - Campo da variare..............: '" & strcbCampi & "'" & vbCrLf & _
        " - Seleziona valore da sostituire: "
      If bckSeleziona = True Then
        strDesogglog = strDesogglog & "'Sì' ('" & NTSCStr(IIf(bcbValoreVis = True, strcbValore, NTSCStr(stredValore))) & "')" & vbCrLf
      Else
        strDesogglog = strDesogglog & "'No'"
      End If
      strDesogglog = strDesogglog & _
        " - Nuovo valore da inserire......: '" & NTSCStr(IIf(bcbNuovoValoreVis = True, strcbNuovoValore, NTSCStr(stredNuovoValore))) & "'" & vbCrLf & _
        " - Elaborazione manuale..........: " & NTSCStr(IIf(bckElaborazione = True, "'Sì'", "'No'")) & vbCrLf
      oCldVaar.ScriviActLog(strDittaCorrente, "BSMGVAAR", "BSMGVAAR", "", "", "M", "E", strDesogglog, False)
      '-------------------------------------------------------------------------------
      ' Elaborazione automatica senza report
      '-------------------------------------------------------------------------------
      If (nTipoElaborazione = 0) And (bckElaborazione = False) Then

        '------------------------
        'scrivo la registrazione
        bRet = oCldVaar.Elabora(strDittaCorrente, lRecords, strCampo, nTipo1, strNuovoValore, strWhereFiar, ncbNuovoValore, bckSeleziona, nTipo2, strValore)

        bElabInCorso = False
        Return True

      End If
      '-------------------------------------------------------------------------------
      ' Ogni altro tipo di elaborazione
      '-------------------------------------------------------------------------------
      oCldVaar.GetArticoPassoPasso(strDittaCorrente, strCampo, strValore, strWhereFiar, bckSeleziona, nTipo1, nTipo2, ncbNuovoValore, dsArtico)

      If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128503889255724312, "Non esistono articoli da aggiornare con queste caratteristiche.")))
        Return False
      End If

      For i = 0 To dsArtico.Tables("ARTICO").Rows.Count - 1
        If bckElaborazione = True Then
          strCodart = NTSCStr(dsArtico.Tables("ARTICO").Rows(i)!ar_codart)
          strDescr = NTSCStr(dsArtico.Tables("ARTICO").Rows(i)!ar_descr)

          ThrowRemoteEvent(New NTSEventArgs("SALTAARTI:", ""))

          Select Case nRichiesta
            Case 0     ' ANNULLATO
              Return False
            Case 1     ' SALTATO
              GoTo SaltaArticolo
          End Select
        End If

        '------------------------
        'scrivo la registrazione
        bRet = oCldVaar.ElaboraPassoPasso(strDittaCorrente, strCampo, nTipo1, strNuovoValore, lIITTInvent, dsArtico.Tables("ARTICO").Rows(i))
        If bRet Then lRecords = (lRecords + 1)

SaltaArticolo:
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

  Public Overridable Function CheckInTable(ByVal strCampo As String, ByVal strCodice As String) As Boolean
    Dim strTmp As String = ""
    Dim strTabella As String
    Dim strDescampo As String
    Dim strTipocampo As String
    Try
      CheckInTable = True
      strTipocampo = "N"
      Select Case strCampo
        Case "ar_codiva" : strTabella = "TABCIVA" : strDescampo = oApp.Tr(Me, 129030105085634583, "Codice IVA")
        Case "ar_gruppo" : strTabella = "TABGMER" : strDescampo = oApp.Tr(Me, 129030105111572581, "Gruppo merciologico")
        Case "ar_sotgru" : strTabella = "TABSGME" : strDescampo = oApp.Tr(Me, 129030105137979338, "Sottogruppo merciologico")
        Case "ar_controp" : strTabella = "TABCOVE" : strDescampo = oApp.Tr(Me, 129030105169854950, "Contropartita vendite")
        Case "ar_claprov" : strTabella = "TABCPAR" : strDescampo = oApp.Tr(Me, 129030105195167936, "Classe provvigione")
        Case "ar_clascon" : strTabella = "TABCSAR" : strDescampo = oApp.Tr(Me, 129030105234231186, "Classe sconto")
        Case "ar_controa" : strTabella = "TABCOVE" : strDescampo = oApp.Tr(Me, 129030105259075413, "Contropartita acquisti")
        Case "ar_codpdon" : strTabella = "TABPDON" : strDescampo = oApp.Tr(Me, 129030105285013411, "Relazione listini")
        Case "ar_contros" : strTabella = "TABCOVE" : strDescampo = oApp.Tr(Me, 129030105309232626, "Contropartita scarico produzione")
        Case "ar_magstock" : strTabella = "TABMAGA" : strDescampo = oApp.Tr(Me, 129030105332514323, "Magazzino stoccaggio")
        Case "ar_magprod" : strTabella = "TABMAGA" : strDescampo = oApp.Tr(Me, 129030105354233490, "Magazzino produzione")
        Case "ar_codappr" : strTabella = "TABAPPR" : strDescampo = oApp.Tr(Me, 129030105376577669, "Approvvigionatore")
        Case "ar_codmarc" : strTabella = "TABMARC" : strDescampo = oApp.Tr(Me, 129030105400640631, "Marca")
        Case "ar_forn" : strTabella = "ANAGRA" : strDescampo = oApp.Tr(Me, 129030105422203545, "Fornitore 1")
        Case "ar_forn2" : strTabella = "ANAGRA" : strDescampo = oApp.Tr(Me, 129030105447829037, "Fornitore 2")
        Case "ar_codnomc" : strTabella = "TARIC" : strDescampo = oApp.Tr(Me, 129030105469391951, "Codice nomenclatura combinata")
        Case "ar_famprod" : strTabella = "TABCFAM" : strDescampo = oApp.Tr(Me, 129030105491892383, "Famiglia") : strTipocampo = "S"
        Case "ar_codtcdc" : strTabella = "TABTCDC" : strDescampo = oApp.Tr(Me, 129321245386041611, "Tipologia entità")
        Case "ar_coddica" : strTabella = "TABDICA" : strDescampo = oApp.Tr(Me, 129321245394166715, "Aggregazione budget") : strTipocampo = "S"
        Case Else : Exit Function
      End Select
      Select Case strTabella
        Case "ANAGRA"
          If oCldVaar.ValCodiceDb(strCodice, strDittaCorrente, "ANAGRA", "N", strTmp) = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030105525955537, "|" & strDescampo & "| inesistente.")))
            Return False
          End If
        Case "TARIC"
          strTipocampo = "S"
          If oCldVaar.ValCodiceDb(strCodice, strDittaCorrente, "TARIC", strTipocampo, strTmp) = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128503212251975517, "|" & strDescampo & "| inesistente.")))
            Return False
          End If
        Case "TABTCDC"
          strTipocampo = "N"
          If oCldVaar.ValCodiceDb(strCodice, strDittaCorrente, "TABTCDC", strTipocampo, strTmp, , "A") = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129321255562656373, "|" & strDescampo & "| inesistente.")))
            Return False
          End If
        Case "TABDICA"
          strTipocampo = "S"
          Dim dttTmp As New DataTable
          If oCldVaar.ValCodiceDb(strCodice, strDittaCorrente, "TABDICA", strTipocampo, strTmp, dttTmp, "A") = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129321255571250123, "|" & strDescampo & "| inesistente.")))
            Return False
          End If
          If dttTmp.Rows.Count <> 0 Then
            If NTSCInt(dttTmp.Rows(0)!tb_liv) = 1 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129321256903281373, "|" & strDescampo & "| non deve essere di primo livello.")))
              Return False
            End If
          End If
        Case Else
          If oCldVaar.ValCodiceDb(strCodice, strDittaCorrente, strTabella, strTipocampo, strTmp) = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128503211924981237, "|" & strDescampo & "| inesistente.")))
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

  Public Overridable Sub SettaNuoviValori(ByVal stredNuovoValore As String, ByVal stredValore As String, ByVal bedValoreVis As Boolean, ByVal bcbValoreVis As Boolean, ByVal ncbNuovoValore As Integer, ByVal ncbValore As Integer)
    Try
      strNuovoValore = "" : strValore = ""
      nTipo1 = 0 : nTipo2 = 0
      Select Case strCampo
        Case "ar_codappr", "ar_claprov", "ar_clascon", "ar_codiva", "ar_controa", "ar_contros", _
          "ar_controp", "ar_fpfence", "ar_ggrior", "ar_magstock", "ar_magprod", "ar_codmarc", _
          "ar_codpdon", "ar_reparto", "ar_rrfence", "ar_gruppo", "ar_sotgru", "ar_ggant", "ar_ggpost", _
          "ar_ggragg", "ar_codtcdc", "ar_codseat"
          strNuovoValore = stredNuovoValore : nTipo1 = 2
          If bedValoreVis Then strValore = stredValore : nTipo2 = 2
        Case "ar_forn", "ar_forn2"
          strNuovoValore = stredNuovoValore : nTipo1 = 3
          If bedValoreVis Then strValore = stredValore : nTipo2 = 3
        Case "ar_fcorrlt", "ar_ricar1", "ar_ricar2", "ar_percvst", _
          "ar_misura1", "ar_misura2", "ar_misura3", "ar_volume", _
          "ar_minord", "ar_scomin", "ar_scomax", "ar_sublotto", "ar_maxlotto", _
          "ar_pesolor", "ar_pesonet"
          strNuovoValore = stredNuovoValore : nTipo1 = 5
          If bedValoreVis Then strValore = stredValore : nTipo2 = 5
        Case "ar_codnomc", "ar_famprod", "ar_tipo", "ar_ubicaz", "ar_coddica", "ar_coddicv", _
          "ar_codcla1", "ar_codcla2", "ar_codcla3", "ar_codcla4", "ar_codcla5"
          strNuovoValore = stredNuovoValore : nTipo1 = 8
          If bedValoreVis Then strValore = stredValore : nTipo2 = 8
        Case "ar_blocco", "ar_stainv", "ar_webvis", "ar_webvend", "ar_consmrp"
          Select Case ncbNuovoValore
            Case 0 : strNuovoValore = "S"
            Case 1 : strNuovoValore = "N"
          End Select
          nTipo1 = 8
          If bcbValoreVis Then
            Select Case ncbValore
              Case 0 : strValore = "S"
              Case 1 : strValore = "N"
            End Select
            nTipo2 = 8
          End If
        Case "ar_pesoca"
          Select Case ncbNuovoValore
            Case 0 : strNuovoValore = "1"
            Case 1 : strNuovoValore = "0"
          End Select
          nTipo1 = 5
          If bcbValoreVis Then
            Select Case ncbValore
              Case 0 : strValore = "1"
              Case 1 : strValore = "0"
            End Select
            nTipo2 = 5
          End If
        Case "ar_polriord"
          Select Case ncbNuovoValore
            Case 0 : strNuovoValore = "F"
            Case 1 : strNuovoValore = "G"
            Case 2 : strNuovoValore = "M"
            Case 3 : strNuovoValore = "N"
            Case 4 : strNuovoValore = "O"
          End Select
          nTipo1 = 8
          If bcbValoreVis Then
            Select Case ncbValore
              Case 0 : strValore = "F"
              Case 1 : strValore = "G"
              Case 2 : strValore = "M"
              Case 3 : strValore = "N"
              Case 4 : strValore = "O"
            End Select
            nTipo2 = 8
          End If
        Case "ar_tipoopz"
          Select Case ncbNuovoValore
            Case 0 : strNuovoValore = " "
            Case 1 : strNuovoValore = "B"
            Case 2 : strNuovoValore = "C"
            Case 3 : strNuovoValore = "G"
            Case 4 : strNuovoValore = "O"
            Case 5 : strNuovoValore = "Q"
          End Select
          nTipo1 = 8
          If bcbValoreVis Then
            Select Case ncbValore
              Case 0 : strValore = " "
              Case 1 : strValore = "B"
              Case 2 : strValore = "C"
              Case 3 : strValore = "G"
              Case 4 : strValore = "O"
              Case 5 : strValore = "Q"
            End Select
            nTipo2 = 8
          End If
        Case "ar_umdapr", "ar_umdapra", "ar_umpdapr", "ar_umpdapra"
          Select Case ncbNuovoValore
            Case 0 : strNuovoValore = "P"
            Case 1 : strNuovoValore = "C"
            Case 2 : strNuovoValore = "S"
            Case 3 : strNuovoValore = "Q"
          End Select
          nTipo1 = 8
          If bcbValoreVis Then
            Select Case ncbValore
              Case 0 : strValore = "P"
              Case 1 : strValore = "C"
              Case 2 : strValore = "S"
              Case 3 : strValore = "Q"
            End Select
            nTipo2 = 8
          End If
        Case "ar_perragg"
          Select Case ncbNuovoValore
            Case 0 : strNuovoValore = "G"
            Case 1 : strNuovoValore = "S"
            Case 2 : strNuovoValore = "D"
            Case 3 : strNuovoValore = "Q"
            Case 4 : strNuovoValore = "M"
            Case 5 : strNuovoValore = "B"
            Case 6 : strNuovoValore = "T"
            Case 7 : strNuovoValore = "R"
            Case 8 : strNuovoValore = "U"
            Case 9 : strNuovoValore = "A"
          End Select
          nTipo1 = 8
          If bcbValoreVis Then
            Select Case ncbValore
              Case 0 : strValore = "G"
              Case 1 : strValore = "S"
              Case 2 : strValore = "D"
              Case 3 : strValore = "Q"
              Case 4 : strValore = "M"
              Case 5 : strValore = "B"
              Case 6 : strValore = "T"
              Case 7 : strValore = "R"
              Case 8 : strValore = "U"
              Case 9 : strValore = "A"
            End Select
            nTipo2 = 8
          End If
        Case "ar_deterior"
          Select Case ncbNuovoValore
            Case 0 : strNuovoValore = "S"
            Case 1 : strNuovoValore = "N"
            Case 2 : strNuovoValore = " "
          End Select
          nTipo1 = 8
          If bcbValoreVis Then
            Select Case ncbValore
              Case 0 : strValore = "S"
              Case 1 : strValore = "N"
              Case 2 : strValore = " "
            End Select
            nTipo2 = 8
          End If
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

#Region "funzioni specifiche per BN__VAAN.BN__AUT1.VB"
  Public Overridable Function AutmApri(ByVal strDitta As String, ByRef dsAutm As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '-------------------------------------------------------------------------------
      ' Prende il nuovo valore da sostituire
      '-------------------------------------------------------------------------------
      SettaNuoviValori(strAutmedNuovoValore, strAutmedValore, bAutmedValoreVis, bAutmcbValoreVis, nAutmcbNuovoValore, nAutmcbValore)

      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      oCldVaar.GetArtico(strDittaCorrente, strCampo, strValore, strWhereFiar, bAutmckSeleziona, nTipo1, nTipo2, nAutmcbNuovoValore, dsAutm, True, strNuovoValore)

      If dsAutm.Tables("ARTICO").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129828573756456116, "Non esistono articoli da aggiornare con queste caratteristiche.")))
        Return False
      End If

      dsAutmShared = dsAutm

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsAutmShared.Tables("ARTICO").ColumnChanging, AddressOf AutmBeforeColUpdate
      AddHandler dsAutmShared.Tables("ARTICO").ColumnChanged, AddressOf AutmAfterColUpdate

      bAutmHasChanges = False

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
  Public Overridable Function AutmSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not AutmTestPreSalva() Then Return False
      End If

      dsAutmShared.AcceptChanges()
      bAutmHasChanges = False

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
  Public ReadOnly Property AutmRecordIsChanged() As Boolean
    Get
      Return bAutmHasChanges
    End Get
  End Property
  Public Overridable Function AutmTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsAutmShared.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        'If NTSCStr(dtrCurrRow(i)!apa_codartas) = " " Then
        '  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128562881000035201, "Codice conto obbligatorio.")))
        '  Return False
        'End If
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
  Public Overridable Function AutmRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsAutmShared.Tables("ARTICO").Select(strFilter)(nRow).RejectChanges()
      bAutmHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub AutmBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AutmBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub AutmAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bAutmHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AutmAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub AutmBeforeColUpdate_xx_valorenew(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If Not CheckValore(e) Then Return
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

  Public Overridable Function CheckValore(ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strLabel As String
    Try
      CheckValore = False
      strLabel = "Nuovo valore da inserire"
      Select Case strCampo
        '--- Campi stringa
        Case "ar_famprod", "ar_coddica", "ar_coddicv"
          If NTSCStr(e.ProposedValue) <> "" Then
            If Not CheckInTable(strCampo, NTSCStr(e.ProposedValue)) Then
              Exit Function
            End If
          End If
        Case "ar_codnomc"
          If NTSCStr(e.ProposedValue) <> "" Then
            If Not CheckInTable(strCampo, NTSCStr(e.ProposedValue)) Then
              Exit Function
            End If
          End If
          '--- Campi interi (da 0 a 99)
        Case "ar_gruppo"
          If NTSCInt(NTSCStr(e.ProposedValue)) > 0 Then
            If Not CheckInTable(strCampo, NTSCStr(e.ProposedValue)) Then
              e.ProposedValue = NTSCStr(e.Row!xx_valorenew)
              Exit Function
            End If
          End If
          '--- Campi interi (da 0 a 999)
        Case "ar_claprov", "ar_clascon", "ar_ggrior", "ar_reparto", "ar_numecr", "ar_codvuo", _
             "ar_codpdon", "ar_garacq", "ar_garven", "ar_livmindb", "ar_fpfence", "ar_rrfence", _
             "ar_codappr", "ar_codmarc", "ar_ggant", "ar_ggpost", "ar_codimba", "ar_ggragg"
          If NTSCInt(NTSCStr(e.ProposedValue)) > 0 Then
            If Not CheckInTable(strCampo, NTSCStr(e.ProposedValue)) Then
              e.ProposedValue = NTSCStr(e.Row!xx_valorenew)
              Exit Function
            End If
          End If
          '--- Campi interi (da 0 a 9999)
        Case "ar_codiva", "ar_sotgru", "ar_controp", "ar_catlifo", "ar_controa", "ar_contros", _
             "ar_magstock", "ar_magprod", "ar_verdb", "ar_ultfase", "ar_codtagl", "ar_codstag", "ar_codtcdc"
          If NTSCInt(NTSCStr(e.ProposedValue)) > 0 Then
            If Not CheckInTable(strCampo, NTSCStr(e.ProposedValue)) Then
              e.ProposedValue = NTSCStr(e.Row!xx_valorenew)
              Exit Function
            End If
          End If
          '--- Campi long (da 0 a 999999999)
        Case "ar_forn", "ar_forn2", "ar_percvst"
          If NTSCInt(NTSCStr(e.ProposedValue)) > 0 Then
            If Not CheckInTable(strCampo, NTSCStr(e.ProposedValue)) Then
              e.ProposedValue = NTSCStr(e.Row!xx_valorenew)
              Exit Function
            End If
          End If
      End Select
      CheckValore = True

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

  Public Overridable Function ElaboraGriglia() As Boolean
    Dim i As Integer
    Dim strIn As String = ""
    Dim lRecords As Integer
    Dim strPrecVal As String = ""
    Dim lRecordsTot As Integer
    Dim j As Integer
    Dim evt As NTSEventArgs = Nothing
    Dim bPrecVal As Boolean
    Try
      '-------------------------------------------------------------------------------
      ' Chiede conferma dell'elaborazione
      '-------------------------------------------------------------------------------
      evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 129832925992041498, "Procedere con l'aggiornamento degli articoli selezionati?"))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then
        Return False
      End If

      'riga per riga al cambio di codice aggiorno i dati ogni n conti elabora
      For i = 0 To dsAutmShared.Tables("ARTICO").Rows.Count - 1
        'se il valore da modificare è uguale a quello su db salto
        If NTSCStr(dsAutmShared.Tables("ARTICO").Rows(i)!xx_valorenew) = NTSCStr(dsAutmShared.Tables("ARTICO").Rows(i)!xx_campoold) Then Continue For

        If bPrecVal = False Then
          strPrecVal = NTSCStr(dsAutmShared.Tables("ARTICO").Rows(i)!xx_valorenew)
          bPrecVal = True
        End If

        If strPrecVal <> NTSCStr(dsAutmShared.Tables("ARTICO").Rows(i)!xx_valorenew) Or j >= 100 Then
          ElaboraGrigliaAggiorna(strIn, lRecords, _
                                 strPrecVal)

          lRecordsTot = lRecordsTot + lRecords
          strIn = ""
          strPrecVal = NTSCStr(dsAutmShared.Tables("ARTICO").Rows(i)!xx_valorenew)
          j = 0
        Else
          j = j + 1
        End If
        strIn = strIn & CStrSQL(NTSCStr(dsAutmShared.Tables("ARTICO").Rows(i)!xx_cod)) & " ,"
      Next

      'elaboro i record rimasti
      If strIn <> "" Then
        ElaboraGrigliaAggiorna(strIn, lRecords, _
                               strPrecVal)

        lRecordsTot = lRecordsTot + lRecords
      End If

      ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 129832683132656250, "Aggiornati |" & lRecordsTot & "| record")))

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
  Public Overridable Function ElaboraGrigliaAggiorna(ByRef strIn As String, ByRef lRecords As Integer, _
                                                     ByRef strPrecVal As String) As Boolean
    Dim strTmp As String = ""
    Try
      strIn = Left(strIn, Len(strIn) - 2)
      strTmp = strWhereFiar & oCldVaar.GetIn(strIn)

      oCldVaar.Elabora(strDittaCorrente, lRecords, strCampo, nTipo1, _
                       strPrecVal, strTmp, nAutmcbNuovoValore, bAutmckSeleziona, _
                       nTipo2, strValore)

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
