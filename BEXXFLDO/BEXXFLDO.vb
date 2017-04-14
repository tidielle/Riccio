Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEXXFLDO
  Inherits CLE__BASN

  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModAll
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

  Public oCldFldo As CLDXXFLDO
  Public strChildParent As String = ""
  Public lIITTFldonod As Integer = 0
  Public lIITTFldolin As Integer = 0
  Public lNewProgressivo As Integer = 0  'progressivo per chiave primaria in tabella
  Public strUsaContoFattDoc As String = "0"

  'passo i parametri di base per il calcolo del flusso
  Public strTipork As String = ""
  Public nAnno As Integer = 0
  Public strSerie As String = ""
  Public lNumero As Integer = 0
  Public nVers As Integer = 0
  Public lConto As Integer = 0
  Public strDatreg As String = ""
  Public lNumreg As Integer = 0
  Public nRigareg As Integer = 0
  Public nAnnpar As Integer = 0
  Public strAlfpar As String = ""
  Public lNumpar As Integer = 0
  Public nNumrata As Integer = 0

  'nLivello = disposizione del controllo grafico a partire da sx: -3, -2, -1, 0 (dtt), 1, ...
  'nTipoOgg: 1=ordini  2=ddt 3=veboll no ddt 4=fatt differite 5=prima nota 6=scadenze 7=offerta 8=note prelievo

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDXXFLDO"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldFldo = CType(MyBase.ocldBase, CLDXXFLDO)
    oCldFldo.Init(oApp)

    Return True
  End Function

  Public Overridable Function CalcolaFlusso() As Boolean
    'dati gli estremi di un documento, ne calcolo il flusso
    'strTipork: "1" = CG, "2" = SC, altri tipi rk = come offerte, magazzino, ordini, ...
    Try
      If lIITTFldonod = 0 Then lIITTFldonod = oCldFldo.GetTblInstId("TTFLDONOD", False)
      If lIITTFldolin = 0 Then lIITTFldolin = oCldFldo.GetTblInstId("TTFLDOLIN", False)
      oCldFldo.ResetTblInstId("TTFLDONOD", False, lIITTFldonod)
      oCldFldo.ResetTblInstId("TTFLDOLIN", False, lIITTFldolin)
      lNewProgressivo = 1

      strUsaContoFattDoc = oCldFldo.GetSettingBus("BSVEFADI", "OPZIONI", ".", "UsaContoFattDoc", "0", " ", "0")

      Select Case strTipork
        Case "!" : If Not CalcolaFlusso_Offerte(True, True) Then Return False
        Case "#", "H", "O", "Q", "R", "Y", "X" : If Not CalcolaFlusso_Ordini(True, True) Then Return False
        Case "V", "$" : If Not CalcolaFlusso_OrdiniAperti() Then Return False
        Case "W" : If Not CalcolaFlusso_NotePrelievo(True, True) Then Return False
        Case "B", "M", "T" : If Not CalcolaFlusso_DDT(True, True) Then Return False
        Case "A", "C", "D", "E", "F", "I", "J", "K", "L", "N", "P", "S", "U", "Z", "£", "(" : If Not CalcolaFlusso_Fatture(True, True, True, True) Then Return False
        Case "1" : If Not CalcolaFlusso_PrimaNota(True, True) Then Return False
        Case "2" : If Not CalcolaFlusso_Scadenze(True, True) Then Return False
      End Select

      If Not oCldFldo.AggiornaCampoNote(strDittaCorrente, lIITTFldonod) Then Return False

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
  Public Overridable Function CalcolaFlusso_Offerte(ByVal bMonte As Boolean, ByVal bValle As Boolean) As Boolean
    'se devo aggiornare sia a monte che a valle vuol dire che sono il punto di partenza
    Dim nRec As Integer = 0
    'Dim nLivello As Integer = -3        '-3 : primo controllo da sx
    'Dim nTipoOgg As Integer = 7         'offerte
    Try
      '-------------------------------
      'cerco le offerte
      If bMonte And bValle Then
        'trovo il nodo di partenza in base ai filtri passati in input
        If Not oCldFldo.GetOfferteNodo0(strDittaCorrente, lIITTFldonod, strTipork, nAnno, strSerie, lNumero, nVers, lNewProgressivo, nRec) Then Return False
      ElseIf bMonte Then
        'cerco le offerte dagli ordini
        If Not oCldFldo.GetOfferteDaOrd(strDittaCorrente, lIITTFldonod, lIITTFldolin, lNewProgressivo, nRec) Then Return False
      ElseIf bValle Then
        'caso non possibile
      End If

      If nRec = 0 Then Return True

      '-------------------------------
      'esplodo a monte ed a valle
      If bValle Then
        If Not CalcolaFlusso_Ordini(False, True) Then Return False
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
  Public Overridable Function CalcolaFlusso_Ordini(ByVal bMonte As Boolean, ByVal bValle As Boolean) As Boolean
    'se devo aggiornare sia a monte che a valle vuol dire che sono il punto di partenza
    Dim nRec As Integer = 0
    Dim nRec1 As Integer = 0
    'Dim nLivello As Integer = -2        '-2 : secondo controllo da sx
    'Dim nTipoOgg As Integer = 1         'ordini
    Try
      '-------------------------------
      'cerco gli ordini
      If bMonte And bValle Then
        'trovo il nodo di partenza in base ai filtri passati in input
        If Not oCldFldo.GetOrdiniNodo0(strDittaCorrente, lIITTFldonod, strTipork, nAnno, strSerie, lNumero, lNewProgressivo, nRec) Then Return False
      ElseIf bMonte Then
        'cerco gli ordini da note prelievo, ddt e fatture immediate
        If Not oCldFldo.GetOrdiniDaNote(strDittaCorrente, lIITTFldonod, lIITTFldolin, lNewProgressivo, nRec) Then Return False
        If Not oCldFldo.GetOrdiniDaDDTeFattImm(strDittaCorrente, lIITTFldonod, lIITTFldolin, True, True, lNewProgressivo, nRec1) Then Return False
      ElseIf bValle Then
        'cerco gli ordini dalle offerte
        If Not oCldFldo.GetOrdiniDaOfferte(strDittaCorrente, lIITTFldonod, lIITTFldolin, lNewProgressivo, nRec) Then Return False
      End If

      If nRec = 0 And nRec1 = 0 Then Return True

      '-------------------------------
      'esplodo a monte ed a valle
      If bMonte Then
        If Not CalcolaFlusso_Offerte(True, False) Then Return False
        If Not oCldFldo.GetOrdiniApertiDaOrdini(strDittaCorrente, lIITTFldonod, lIITTFldolin, lNewProgressivo, nRec) Then Return False
      End If

      If bValle Then
        If Not CalcolaFlusso_NotePrelievo(False, True) Then Return False
      End If

      If bValle Then
        If Not CalcolaFlusso_DDT(False, True) Then Return False
      End If

      If bValle Then
        If Not CalcolaFlusso_Fatture(False, True, True, False) Then Return False
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
  Public Overridable Function CalcolaFlusso_OrdiniAperti() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not oCldFldo.GetOrdiniNodo0(strDittaCorrente, lIITTFldonod, strTipork, nAnno, strSerie, lNumero, lNewProgressivo, 0) Then Return False
      '--------------------------------------------------------------------------------------------------------------
      If Not oCldFldo.GetOrdiniDaOrdiniAperti(strDittaCorrente, lIITTFldonod, lIITTFldolin, lNewProgressivo, 0) Then Return False
      '--------------------------------------------------------------------------------------------------------------
      If Not CalcolaFlusso_Ordini(False, True) Then Return False
      If Not CalcolaFlusso_DDT(False, True) Then Return False
      If Not CalcolaFlusso_Fatture(False, True, True, False) Then Return False
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
  Public Overridable Function CalcolaFlusso_NotePrelievo(ByVal bMonte As Boolean, ByVal bValle As Boolean) As Boolean
    'se devo aggiornare sia a monte che a valle vuol dire che sono il punto di partenza
    Dim nRec As Integer = 0
    'Dim nLivello As Integer = -1        '-1 : terzo controllo da sx
    'Dim nTipoOgg As Integer = 8         'note prelievo
    Try
      '-------------------------------
      'cerco le note di prelievo
      If bMonte And bValle Then
        'trovo il nodo di partenza in base ai filtri passati in input
        If Not oCldFldo.GetNoteNodo0(strDittaCorrente, lIITTFldonod, strTipork, nAnno, strSerie, lNumero, lNewProgressivo, nRec) Then Return False
      ElseIf bMonte Then
        'cerco le note da ddt e fatture immediate
        If Not oCldFldo.GetNoteDaDDTeFattImm(strDittaCorrente, lIITTFldonod, lIITTFldolin, True, True, lNewProgressivo, nRec) Then Return False
      ElseIf bValle Then
        'cerco le note dagli ordini
        If Not oCldFldo.GetNoteDaOrdini(strDittaCorrente, lIITTFldonod, lIITTFldolin, lNewProgressivo, nRec) Then Return False
      End If

      If nRec = 0 Then Return True

      '-------------------------------
      'esplodo a monte ed a valle
      If bMonte Then
        If Not CalcolaFlusso_Ordini(True, False) Then Return False
      End If

      If bValle Then
        If Not CalcolaFlusso_DDT(False, True) Then Return False
      End If

      If bValle Then
        If Not CalcolaFlusso_Fatture(False, True, True, False) Then Return False
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
  Public Overridable Function CalcolaFlusso_DDT(ByVal bMonte As Boolean, ByVal bValle As Boolean) As Boolean
    'se devo aggiornare sia a monte che a valle vuol dire che sono il punto di partenza
    Dim nRec As Integer = 0
    Dim nRec1 As Integer = 0
    'Dim nLivello As Integer = 0         '0 :quarto controllo da sx
    'Dim nTipoOgg As Integer = 2         'ddt
    Try
      '-------------------------------
      'cerco i DDT
      If bMonte And bValle Then
        'trovo il nodo di partenza in base ai filtri passati in input
        If Not oCldFldo.GetDDTNodo0(strDittaCorrente, lIITTFldonod, strTipork, nAnno, strSerie, lNumero, lNewProgressivo, nRec) Then Return False
      ElseIf bMonte Then
        'trovo i ddt da fatture diff
        If Not oCldFldo.GetDDTDaFatture(strDittaCorrente, lIITTFldonod, lIITTFldolin, lNewProgressivo, nRec) Then Return False

        'potrebbe essere un DDT RICEVUTO la cui fattura non è stata contabilizzata ma sul DDT è stata 
        'indicata la partita della fattura uguale a quella di prima nota (compatibilità con VB6)
        If Not oCldFldo.GetFattureDaPrinot(strDittaCorrente, lIITTFldonod, lIITTFldolin, strUsaContoFattDoc, lNewProgressivo, nRec1) Then Return False
      ElseIf bValle Then
        'trovo i ddt da ordini non collegati a note e da note prel
        If Not oCldFldo.GetDDTeFatimmDaNote(strDittaCorrente, lIITTFldonod, lIITTFldolin, False, lNewProgressivo, nRec) Then Return False
        If Not oCldFldo.GetDDTeFatimmDaOrdini(strDittaCorrente, lIITTFldonod, lIITTFldolin, False, lNewProgressivo, nRec1) Then Return False
      End If

      'If nRec = 0 And nRec1 = 0 Then Return True 'se da prinot cerco i DDT ric. sicuramente in nRec e nRec1 c'è sempre 0

      '-------------------------------
      'esplodo a monte ed a valle
      If bMonte Then
        If Not CalcolaFlusso_Ordini(True, False) Then Return False
      End If

      If bMonte Then
        If Not CalcolaFlusso_NotePrelievo(True, False) Then Return False
      End If

      If bValle Then
        If Not CalcolaFlusso_Fatture(False, True, False, True) Then Return False

        'potrebbe essere un DDT RICEVUTO la cui fattura non è stata contabilizzata ma sul DDT è stata 
        'indicata la partita della fattura uguale a quella di prima nota (compatibilità con VB6)
        If Not CalcolaFlusso_PrimaNota(False, True) Then Return False
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
  Public Overridable Function CalcolaFlusso_Fatture(ByVal bMonte As Boolean, ByVal bValle As Boolean, _
                                                    ByVal bImmediate As Boolean, ByVal bDifferite As Boolean) As Boolean
    'se devo aggiornare sia a monte che a valle vuol dire che sono il punto di partenza
    Dim nRec As Integer = 0
    Dim nRec1 As Integer = 0
    'Dim nLivello As Integer = 1         '1 :quinto controllo da sx
    'Dim nTipoOgg As Integer = 3         '3 = fatture immediate, 4 = fatture differite (D, P, K)
    Try
      'If bDifferite Then nTipoOgg = 4
      '-------------------------------
      'cerco le fatture 
      If bMonte And bValle Then
        'trovo il nodo di partenza in base ai filtri passati in input
        If Not oCldFldo.GetFattureNodo0(strDittaCorrente, lIITTFldonod, strTipork, nAnno, strSerie, lNumero, lNewProgressivo, nRec) Then Return False
      ElseIf bMonte Then
        'trovo le fatture da prima nota
        If Not oCldFldo.GetFattureDaPrinot(strDittaCorrente, lIITTFldonod, lIITTFldolin, strUsaContoFattDoc, lNewProgressivo, nRec) Then Return False
      ElseIf bValle Then
        If bImmediate Then
          'trovo le fatture da ordini non collegati a note e da note prel
          If Not oCldFldo.GetDDTeFatimmDaNote(strDittaCorrente, lIITTFldonod, lIITTFldolin, True, lNewProgressivo, nRec) Then Return False
          If Not oCldFldo.GetDDTeFatimmDaOrdini(strDittaCorrente, lIITTFldonod, lIITTFldolin, True, lNewProgressivo, nRec1) Then Return False
        End If
        If bDifferite Then
          'trovo le fatture da ddt
          If Not oCldFldo.GetFattureDaDDT(strDittaCorrente, lIITTFldonod, lIITTFldolin, lNewProgressivo, nRec) Then Return False
        End If
      End If

      If nRec = 0 And nRec1 = 0 Then Return True

      '-------------------------------
      'esplodo a monte ed a valle
      If bImmediate Then
        If bMonte Then
          If Not CalcolaFlusso_Ordini(True, False) Then Return False
        End If

        If bMonte Then
          If Not CalcolaFlusso_NotePrelievo(True, False) Then Return False
        End If
      End If

      If bDifferite Then
        If bMonte Then
          If Not CalcolaFlusso_DDT(True, False) Then Return False
        End If
      End If

      If bValle Then
        If Not CalcolaFlusso_PrimaNota(False, True) Then Return False
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
  Public Overridable Function CalcolaFlusso_PrimaNota(ByVal bMonte As Boolean, ByVal bValle As Boolean) As Boolean
    'se devo aggiornare sia a monte che a valle vuol dire che sono il punto di partenza
    Dim nRec As Integer = 0
    'Dim nLivello As Integer = 2         '2 :sesto controllo da sx
    'Dim nTipoOgg As Integer = 5         'prima nota
    Try
      '-------------------------------
      'cerco la prima nota
      If bMonte And bValle Then
        'trovo il nodo di partenza in base ai filtri passati in input
        If Not oCldFldo.GetPrinotNodo0(strDittaCorrente, lIITTFldonod, strDatreg, lNumreg, nRigareg, lNewProgressivo, nRec) Then Return False
      ElseIf bMonte Then
        'trovo la registrazione da scadenza
        If Not oCldFldo.GetPrinotDaScadenze(strDittaCorrente, lIITTFldonod, lIITTFldolin, lNewProgressivo, nRec) Then Return False
      ElseIf bValle Then
        'trovo la registrazione da fatture
        If Not oCldFldo.GetPrinotDaFatture(strDittaCorrente, lIITTFldonod, lIITTFldolin, strUsaContoFattDoc, lNewProgressivo, nRec) Then Return False
      End If

      If nRec = 0 Then Return True

      '-------------------------------
      'esplodo a monte ed a valle
      If bMonte Then
        If Not CalcolaFlusso_Fatture(True, False, True, True) Then Return False

        'potrebbe essere un DDT RICEVUTO la cui fattura non è stata contabilizzata ma sul DDT è stata 
        'indicata la partita della fattura uguale a quella di prima nota (compatibilità con VB6)
        If Not CalcolaFlusso_DDT(True, False) Then Return False
      End If

      If bValle Then
        If Not CalcolaFlusso_Scadenze(False, True) Then Return False
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
  Public Overridable Function CalcolaFlusso_Scadenze(ByVal bMonte As Boolean, ByVal bValle As Boolean) As Boolean
    'se devo aggiornare sia a monte che a valle vuol dire che sono il punto di partenza
    Dim nRec As Integer = 0
    'Dim nLivello As Integer = 3         '3 :settimo controllo da sx
    'Dim nTipoOgg As Integer = 6         'scadenze
    Try
      '-------------------------------
      'cerco le scadenze
      If bMonte And bValle Then
        'trovo il nodo di partenza in base ai filtri passati in input
        If Not oCldFldo.GetScadenNodo0(strDittaCorrente, lIITTFldonod, lConto, nAnnpar, strAlfpar, lNumpar, nNumrata, lNewProgressivo, nRec) Then Return False
      ElseIf bMonte Then
        'caso non possibile
      ElseIf bValle Then
        'trovo la scadenza da prima nota
        If Not oCldFldo.GetScadenzeDaPrinot(strDittaCorrente, lIITTFldonod, lIITTFldolin, lNewProgressivo, nRec) Then Return False
      End If

      If nRec = 0 Then Return True

      '-------------------------------
      'esplodo a monte ed a valle
      If bMonte Then
        If Not CalcolaFlusso_PrimaNota(True, False) Then Return False
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

  Public Overridable Function GetFlusso(ByRef dttOut As DataTable, ByRef dttOutLink As DataTable) As Boolean
    Try

      Return oCldFldo.GetFlusso(strDittaCorrente, lIITTFldonod, lIITTFldolin, dttOut, dttOutLink)

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

  Public Overridable Function GetRigaFlusso(ByVal lNodoId As Integer, ByRef dttOut As DataTable) As Boolean
    Try

      Return oCldFldo.GetRigaFlusso(strDittaCorrente, lIITTFldonod, lNodoId, dttOut)

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


  Public Overridable Function GetValutaScorpo(ByVal strDitta As String, ByVal strTipork As String, _
                                              ByVal nAnno As Integer, ByVal strSerie As String, _
                                              ByVal lNum As Integer, ByVal nRev As Integer, _
                                              ByRef nCodvalu As Integer, ByRef bScorpo As Boolean, _
                                              ByRef strPrzBoll As String) As Boolean
    Try
      Return oCldFldo.GetValutaScorpo(strDitta, strTipork, nAnno, strSerie, lNum, nRev, nCodvalu, bScorpo, strPrzBoll)

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

  Public Overridable Function IsDocRetail(ByVal strTipoDoc As String, ByVal nAnno As Integer, _
                                        ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try

      Return oCldFldo.IsDocRetail(strDittaCorrente, strTipoDoc, nAnno, strSerie, lNumdoc)

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
  Public Overridable Function IsDocRetailNew(ByVal strTipoDoc As String, ByVal nAnno As Integer, _
                                             ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try

      Return oCldFldo.IsDocRetailNew(strDittaCorrente, strTipoDoc, nAnno, strSerie, lNumdoc)

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
