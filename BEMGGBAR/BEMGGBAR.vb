Imports System.Data
Imports NTSInformatica.CLN__STD


Public Class CLEMGGBAR
  Inherits CLE__BASN

  Public oCldGbar As CLDMGGBAR
  Public strWhereArti As String = ""

  'Primo digit di EAN8 ed EAN13
  Public Const PRIMODGT As String = "2"
  Public Const EAN8 As Integer = 1
  Public Const EAN13 As Integer = 2

  Public bIndicod, bAbilBarcodeVariabili As Boolean
  Public strPrefixEAN13 As String

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE
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
#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGGBAR"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldGbar = CType(MyBase.ocldBase, CLDMGGBAR)
    oCldGbar.Init(oApp)
    Return True
  End Function


  Public Overridable Function LeggiOpzioni() As Boolean
    Try
      '----------------------------------------------------------------
      bIndicod = CBool(oCldGbar.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Indicod", "0", " ", "0"))
      strPrefixEAN13 = Trim(oCldGbar.GetSettingBus("BSMGARTI", "OPZIONI", ".", "PrefixEAN13", "", " ", ""))
      bAbilBarcodeVariabili = CBool(NTSCInt(oCldGbar.GetSettingBus("BSREGDOC", "Opzioni", ".", "AbilBarcodeVariabili", "0", " ", "0"))) 'se attiva (-1) abilita la getsione scrittura e lettura dei codici barcode ean13 a peso e/o valore
      If bIndicod = True Then
        Select Case Len(strPrefixEAN13)
          Case 0
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128570376414368522, "L'impostazione di registro 'PrefixEAN13' non è corretta," & vbCrLf & _
              "pertanto l'eventuale creazione di Barcode EAN13" & _
              "sarà fatta seguendo i normali standard.")))
            bIndicod = False
          Case 1 To 11
            'OK
          Case 12
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128570376560307890, "L'impostazione di registro 'PrefixEAN13' è di 12 caratteri," & vbCrLf & _
              "pertanto l'eventuale creazione di Barcode EAN13" & _
              "sarà fatta seguendo i normali standard.")))
            bIndicod = False
          Case Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128570376692184578, "L'impostazione di registro 'PrefixEAN13' supera i 12 caratteri," & vbCrLf & _
              "pertanto l'eventuale creazione di Barcode EAN13" & _
              "sarà fatta seguendo i normali standard.")))
            bIndicod = False
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


  Public Overridable Function Genera(ByVal bAncheConBC As Boolean, ByVal strTipoBC As String, ByVal bUmp As Boolean, ByVal bUmSec As Boolean, _
                                     ByVal bUmConf As Boolean, ByVal strVariabili As String) As Boolean
    Dim dttArti As DataTable = Nothing
    Dim i As Integer
    Try
      If Not oCldGbar.GetArticoli(strDittaCorrente, strWhereArti, bAncheConBC, bUmp, bUmSec, bUmConf, dttArti) Then Return False

      For i = 0 To dttArti.Rows.Count - 1
        If NTSCInt(dttArti.Rows(i)!ar_codtagl) = 0 Then
          Select Case strVariabili
            Case "N"
              If Not GeneraEAN(NTSCInt(strTipoBC), dttArti.Rows(i)) Then Return False
            Case "Q"
              If Not GeneraEAN13Variabili(1, dttArti.Rows(i)) Then Return False
            Case "P"
              If Not GeneraEAN13Variabili(2, dttArti.Rows(i)) Then Return False
          End Select
        Else
          If Not GeneraEANPerTaglia(NTSCInt(strTipoBC), bAncheConBC, dttArti.Rows(i)) Then Return False
        End If
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

  Public Overridable Function CheckDigit(ByVal strCodice As String) As String
    Dim nI As Integer
    Dim nCurNum As Integer
    Dim nSumDisp As Integer
    Dim nSumPari As Integer
    Dim nSumTot As Integer
    Try
      CheckDigit = ""
      nSumPari = 0
      nSumDisp = 0
      If Len(strCodice) <> 8 And Len(strCodice) <> 13 Then
        Exit Function
      End If
      'Per adesso il calcolo dell'ultima cifra fa riferimento a codici che comprendono
      'solo numeri
      For nI = 1 To Len(strCodice)
        If Not ((Asc(Mid(strCodice, nI, 1)) >= 48) And (Asc(Mid(strCodice, nI, 1)) <= 57)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571232711073609, "I codici EAN8 ed EAN13 non possono contenere caratteri")))
          Return ""
        End If
      Next
      If Len(strCodice) = 8 Then
        strCodice = "00000" & strCodice
      End If

      For nI = 1 To Len(strCodice) - 1
        nCurNum = NTSCInt(Mid(strCodice, nI, 1))
        'Verifico che il numero corrente sia pari o dispari
        If (nI Mod 2) = 0 Then
          nSumPari = nSumPari + nCurNum
        Else
          nSumDisp = nSumDisp + nCurNum
        End If
      Next
      nSumPari = nSumPari * 3
      nSumTot = nSumPari + nSumDisp

      Return Right(NTSCStr(10 - NTSCInt(Right(NTSCStr(nSumTot), 1))), 1)
    Catch ex As Exception
      CheckDigit = ""
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GeneraEAN(ByVal nQuale As Integer, ByVal dtrRow As DataRow) As Boolean
    Dim strDummy As String
    Dim lProgr As Integer

    Try
      '-----------------------------------------------------------------------------------------
      '--- Legge l'ultimo progressivo da TABNUMA
      '-----------------------------------------------------------------------------------------
      lProgr = oCldGbar.LegNuma(strDittaCorrente, "BC", " ", 0, True)
      '-----------------------------------------------------------------------------------------
      If nQuale <> EAN8 Then
        If bIndicod = False Then
          If lProgr > 99999 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129179715365733730, "Attenzione!" & vbCrLf & _
              "Il progressivo per la generazione del Barcode, indicato in tabella 'Numerazioni', supera i 99999." & vbCrLf & _
              "Generazione EAN13 non possibile.")))
            Return False
          End If
        Else
          If (lProgr.ToString & strPrefixEAN13).Length > 12 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129183139072752460, "Attenzione!" & vbCrLf & _
              "Il progressivo per la generazione del Barcode, indicato in tabella 'Numerazioni' " & _
              "insieme al valore indicato in 'BSMGARTI/OPZIONI/PrefixEAN13', nel Registro di Business, " & _
              "supera i 12 caratteri." & vbCrLf & _
              "Generazione EAN13 non possibile.")))
            Return False
          End If
        End If
      End If

      strDummy = GeneraCodice(lProgr, nQuale)
      '-----------------------------------------------------------------------------------
      '--- Inserisce la riga in BARCODE
      '-----------------------------------------------------------------------------------
      With dtrRow
        oCldGbar.InsertBarcode(strDittaCorrente, NTSCStr(!ar_codart), strDummy, NTSCStr(!xx_unmis), _
                               GetOra(), NTSCStr(IIf(nQuale = EAN8, "8", "E")), _
                               "", NTSCInt(!ar_ultfase))
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
  Public Overridable Function GeneraEANPerTaglia(ByVal nQuale As Integer, ByVal bAncheConBC As Boolean, ByVal dtrRow As DataRow) As Boolean
    ' Dim bRigheInserite As Boolean
    Dim i As Integer
    Dim lProgr As Integer
    Dim strCampo As String
    Dim strDummy As String
    Dim dttTmp, dttCheck As New DataTable

    Try
      oCldGbar.ValCodiceDb(NTSCStr(dtrRow!ar_codtagl), strDittaCorrente, "TABTAGL", "N", , dttTmp)

      If dttTmp.Rows.Count > 0 Then
        For i = 1 To 24
          strCampo = "tb_dest" & i.ToString("00")
          If Trim(NTSCStr(dttTmp.Rows(0)(strCampo))) <> "" Then
            'Se esiste già quella combinazione articolo\taglia salta al prossimo barcode
            If Not bAncheConBC Then
              If Not oCldGbar.CheckBCTaglie(strDittaCorrente, NTSCStr(dtrRow!ar_codart), NTSCStr(dtrRow!xx_unmis), NTSCStr(dttTmp.Rows(0)(strCampo)), dttCheck) Then Return False

              If dttCheck.Rows.Count > 0 Then Continue For
            End If
            '-----------------------------------------------------------------------------------
            '--- Legge l'ultimo progressivo da TABNUMA
            '-----------------------------------------------------------------------------------
            lProgr = oCldGbar.LegNuma(strDittaCorrente, "BC", " ", 0, True)
            '-----------------------------------------------------------------------------------
            strDummy = GeneraCodice(lProgr, nQuale)
            '-----------------------------------------------------------------------------------
            '--- Inserisce la riga in BARCODE
            '-----------------------------------------------------------------------------------
            With dtrRow
              oCldGbar.InsertBarcode(strDittaCorrente, NTSCStr(!ar_codart), strDummy, NTSCStr(!xx_unmis), _
                                     GetOra(), NTSCStr(IIf(nQuale = EAN8, "8", "E")), _
                                     NTSCStr(dttTmp.Rows(0)(strCampo)), NTSCInt(!ar_ultfase))
            End With
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
  Public Overridable Function GeneraEAN13Variabili(ByVal nTipo As Integer, ByVal dtrRow As DataRow) As Boolean
    'dove nTipo=1 se a peso, nTipo=2 se valore
    Dim strDummy As String
    Dim lProgr As Integer
    Dim lProgrTmp As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Legge l'ultimo progressivo da TABNUMA
      '-----------------------------------------------------------------------------------------
      lProgr = oCldGbar.LegNuma(strDittaCorrente, "BC", " ", 0, True)
      lProgrTmp = lProgr
      '-----------------------------------------------------------------------------------------
      '--- Lo genera a seconda se è a peso o valore
      '-----------------------------------------------------------------------------------------
      If nTipo = 1 Then
        strDummy = PRIMODGT & NTSCStr(Format(lProgr, "00000")) & "QQQQQQ0"
      Else
        strDummy = PRIMODGT & NTSCStr(Format(lProgr, "00000")) & "VVVVVV0"
      End If
      '-----------------------------------------------------------------------------------------
      '--- aggiorna il numeratore in TABNUMA
      '-----------------------------------------------------------------------------------------
      oCldGbar.AggNuma(strDittaCorrente, "BC", " ", 0, lProgr, True, True, "")

      '-----------------------------------------------------------------------------------
      '--- Inserisce la riga in BARCODE
      '-----------------------------------------------------------------------------------
      With dtrRow
        oCldGbar.InsertBarcode(strDittaCorrente, NTSCStr(!ar_codart), strDummy, NTSCStr(!xx_unmis), _
                               GetOra, "E", "", NTSCInt(!ar_ultfase))
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

  Public Overridable Function GetOra() As Integer
    Dim strOra As String = ""
    Dim lOrins As Integer = 0
    Try
      '-----------------------------------------------------------------------------------------
      '--- Determina l'ora da inserire nelle nuove righe generate
      '-----------------------------------------------------------------------------------------
      lOrins = Now.Hour * 10000 + Now.Minute * 100 + Now.Second
      '-----------------------------------------------------------------------------------------
      Return lOrins
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return 0
    End Try
  End Function


  Public Overridable Function GeneraCodice(ByVal lProgr As Integer, ByVal nQuale As Integer) As String
    Dim strPrefixEAN13Tmp, strLastDigit As String
    Dim lProgrTmp As Integer = lProgr
    Dim strDummy As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- Controlla quale tipo di EAN è stato scelto di creare (EAN8 o EAN13)
      '-----------------------------------------------------------------------------------------
      If nQuale = EAN8 Then
        strDummy = PRIMODGT & NTSCStr(Format(lProgr, "000000"))
      Else
        '--- Se non c'è l'ipostazione di registro per la creazione da Indicod
        '--- funziona come prima
        If bIndicod = False Then
          strDummy = PRIMODGT & NTSCStr(Format(lProgr, "00000000000"))
        Else
          If Len(strPrefixEAN13) < 12 Then
            If Len(strPrefixEAN13 & NTSCStr(lProgrTmp)) < 12 Then
              strPrefixEAN13Tmp = strPrefixEAN13
              Do
                strPrefixEAN13Tmp += "0"
              Loop Until Len(strPrefixEAN13Tmp & NTSCStr(lProgrTmp)) = 12
              strDummy = strPrefixEAN13Tmp & NTSCStr(lProgrTmp)
            Else
              If Len(strPrefixEAN13 & NTSCStr(lProgrTmp)) > 12 Then
                If Len(NTSCStr(lProgrTmp)) > 1 Then
                  Do
                    lProgrTmp = NTSCInt(Right(NTSCStr(lProgrTmp), (Len(NTSCStr(lProgrTmp)) - 1)))
                  Loop Until Len(strPrefixEAN13 & NTSCStr(lProgrTmp)) = 12
                  strDummy = strPrefixEAN13 & NTSCStr(lProgrTmp)
                Else
                  strDummy = Left(strPrefixEAN13, 12)
                End If
              Else
                strDummy = strPrefixEAN13 & NTSCStr(lProgrTmp)
              End If
            End If
          Else
            If Len(strPrefixEAN13) > 12 Then
              strDummy = Left(strPrefixEAN13, 12)
            Else
              strDummy = strPrefixEAN13
            End If
          End If
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Determina il CheckDigit finale
      '-----------------------------------------------------------------------------------------
      strLastDigit = CheckDigit(strDummy & "0")
      '-----------------------------------------------------------------------------------------
      '--- Se andato a buon fine aggiorna il numeratore in TABNUMA
      '-----------------------------------------------------------------------------------------
      If strLastDigit <> "" Then
        strDummy = strDummy & strLastDigit
        oCldGbar.AggNuma(strDittaCorrente, "BC", " ", 0, lProgr, True, True, "")
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
    Return strDummy
  End Function

End Class
