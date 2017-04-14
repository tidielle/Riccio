Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGCAEM
  Inherits CLE__BASE

  Public oCldCaem As CLDMGCAEM

  Public bElabInCorso As Boolean = False
  Public bModTCO As Boolean

  Public strDtineser As String
  Public strDtfieser As String
  Public strEscompAnaz As String
  Public strDtulapAnaz As String

  Private Moduli_P As Integer = bsModMG
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

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGCAEM"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldCaem = CType(MyBase.ocldBase, CLDMGCAEM)
    oCldCaem.Init(oApp)
    Return True
  End Function

#Region "Validazione campi"
  Public Overridable Function edEscomp_Validated(ByVal nEscomp As Integer, ByRef strDesEscomp As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not oCldCaem.ValCodiceDb(nEscomp.ToString, strDittaCorrente, "TABESCO", "N", strDesEscomp, dttTmp) Then
        strDtineser = IntSetDate("01/01/" & Year(Now))
        strDtfieser = IntSetDate("31/12/" & Year(Now))
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127972619762343750, "Esercizio contabile |" & nEscomp.ToString & "| inesistente")))
        Return False
      Else
        strDtineser = NTSCStr(dttTmp.Rows(0)!tb_dtineser)
        strDtfieser = NTSCStr(dttTmp.Rows(0)!tb_dtfieser)
        strEscompAnaz = NTSCStr(nEscomp)
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

  Public Overridable Function Apri(ByVal strDitta As String) As Boolean
    Dim dttAnaz As New DataTable
    Try
      strDittaCorrente = strDitta
      If oCldCaem.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttAnaz) Then
        strEscompAnaz = NTSCStr(NTSCInt(dttAnaz.Rows(0)!tb_escomp))
        strDtulapAnaz = NTSCStr(NTSCDate(dttAnaz.Rows(0)!tb_dtulap))
      Else
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

  Public Overridable Function TestPreElabora(ByVal strDtulap As String, ByVal strEscomp As String) As Boolean
    Dim evt As NTSEventArgs = Nothing
    Dim dsArtlif As DataSet = Nothing
    Try

      evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128499856127071367, "Prima di procedere con l'elaborazione si consiglia di effettuare le copie" & vbCrLf & _
            "e verificare sul manuale la correttezza del procedimento eseguito." & vbCrLf & vbCrLf & _
            "Nello specifico occorre aver eseguito ALMENO LE SEGUENTI OPERAZIONI:" & vbCrLf & _
            "- Aggiornamento progressivi di magazzino alla data di fine dell'esercizio da chiudere (|" & NTSCDate(strDtfieser).ToShortDateString & "|)" & vbCrLf & _
            "- Stampa inventario di magazzino di tipo 'A data ultimo aggiornamento' e spuntato 'Inventario finale'" & vbCrLf & vbCrLf & _
            "Procedere con l'elaborazione?"))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then
        Return False
      End If

      If CInt(strEscomp) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128499856009241207, "Indicare esercizio competenza !")))
        Return False
      End If
      If Not edEscomp_Validated(NTSCInt(strEscomp), "") Then
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      If (NTSCDate(strDtfieser).ToShortDateString <> NTSCDate(strDtulap).ToShortDateString) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130257852824978508, "La data aggiornamento progressivi di magazzino (|" & NTSCDate(strDtulap).ToShortDateString & "|) deve essere uguale alla data di fine dell'esercizio da chiudere (|" & NTSCDate(strDtfieser).ToShortDateString & "|)")))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Controlla che non ci siano record in ARTLIF per l'esercizio da chiusere
      '-----------------------------------------------------------------------------------------
      oCldCaem.GetArtlif(strDittaCorrente, strEscomp, dsArtlif)
      If dsArtlif.Tables("ARTLIF").Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128502351781972975, "Esistono gia' dati storici LIFO per l'anno in chiusura!")))
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

  Public Overridable Function Elabora(ByVal strDtulap As String, ByVal strEscomp As String) As Boolean
    Dim bRet As Boolean
    Dim nNdec As Integer
    Dim strDesogglog As String = ""

    Try
      '-----------------------
      'test di rito
      If Not TestPreElabora(strDtulap, strEscomp) Then Return False

      bElabInCorso = True

      nNdec = oCldCaem.TrovaNdecSuPrzUn(0)
      If CBool(oApp.ActKey.ModuliExtAcquistati And bsModExtTCO) Then
        bModTCO = True
      Else
        bModTCO = False
      End If

      '--------------------------------------------------------------------------------------------------------------
      '--- Scrive una riga in ACTLOG, se attiva l'opzione di registro relativa
      '--------------------------------------------------------------------------------------------------------------
      strDesogglog = "Cambio Esercizio Magazzino" & vbCrLf & vbCrLf & _
              " - Esercizio in chiusura...........................: '" & strEscomp & "'" & vbCrLf & _
              " - Data ultimo aggiornamento progressivi definitivi: '" & NTSCDate(strDtulap).ToShortDateString & "'" & vbCrLf
      oCldCaem.ScriviActLog(strDittaCorrente, "BSMGCAEM", "BSMGCAEM", "", "", "M", "E", strDesogglog, False)
      '--------------------------------------------------------------------------------------------------------------
      'scrivo la registrazione
      bRet = oCldCaem.Elabora(strDittaCorrente, bModTCO, nNdec, strEscomp, strDtfieser)
      '--------------------------------------------------------------------------------------------------------------
      '--- Avvia la ricostruzione dei progressivi di magazzino 
      '--- SOLO se l'opzione di regitstro: BSMGCAEM/Opzioni/SaltaRicostrProgress (-1/0 <-- default) NON è attiva.
      '--- Altrimenti salta e va alla fine dell'elaborazione
      '--------------------------------------------------------------------------------------------------------------
      If CBool(oCldCaem.GetSettingBusDitt(strDittaCorrente, "BSMGCAEM", "OPZIONI", ".", _
        "SaltaRicostrProgress", "0", " ", "0")) = True Then GoTo FineElaborazione
      '--------------------------------------------------------------------------------------------------------------
      AvviaRicostruzioneProgressivi(strDtfieser)
      '--------------------------------------------------------------------------------------------------------------
FineElaborazione:
      '--------------------------------------------------------------------------------------------------------------
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

#Region "Ricostruzione progressivi"
  Public Overridable Function AvviaRicostruzioneProgressivi(ByVal strDtfieser As String) As Boolean
    Dim oCleRcap As CLEMGRCAP = Nothing
    Try
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEMGCAEM", "BEMGRCAP", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 129413126807028159, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      End If
      oCleRcap = CType(oTmp, CLEMGRCAP)
      '------------------------------------------------
      AddHandler oCleRcap.RemoteEvent, AddressOf GestisciEventiEntityGrap
      If oCleRcap.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return False

      Return oCleRcap.Elabora(False, strDtfieser)
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
  Public Overridable Sub GestisciEventiEntityGrap(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      ThrowRemoteEvent(e)
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
End Class
