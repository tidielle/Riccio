Imports System.Data
Imports NTSInformatica.CLN__STD

Imports System
Public Class CLEPRGNPV
  Inherits CLE__BASN
  Public oCldGnpv As CLDPRGNPV

#Region "Variabili"
  Public bConsIvaSuPagamenti As Boolean
  Public bUsaSempreNumdoc As Boolean
  Public bIncassoTotaleUsaDatDocSuDatScad As Boolean
  Public bEsisteProvvig As Boolean

  Public nPv_annpart As Integer
  Public lPv_numpart As Integer
  Public nIndiceTestmag As Integer

  Public dPv_totfatt As Decimal
  Public dPv_vtotfatt As Decimal
  Public dPv_totomag As Decimal
  Public dPv_totomagv As Decimal
  Public dPv_provv As Decimal
  Public dPv_impopv As Decimal
  Public dPv_impvpag As Decimal
  Public dPv_vimpopv As Decimal
  Public dPv_vprovv As Decimal
  'Public dPv_vimpvmat As Decimal
  'Public dPv_vimpvpag As Decimal

  Public strPv_alfpart As String
  Public strPv_flag As String
  Public strUsaContoFattDoc As String = ""

  Public dtPv_datscad As DateTime

  Public dttTestmag As New DataTable
  Public dttProvvigINSERT As New DataTable
  Public dttProvvigUPDATE As New DataTable
  Public DDTEmessiNoSimulazione As Boolean = False  'se true i ddt emessi sono considerati al pari dei corrispettivi (quindi non vengono considerati nell'elaborazione delle simulaizoni - vedi RSM)
  Public bProvvig2 As Boolean = False     'se true il calcolo delle provvigioni su incassato avverrà con il nuovo sistema di collegamento a scaden (da net 2012)
  Public bSommaProvvAg1e2StessoDoc As Boolean = False 'se TRUE e nel documento agente 1 =agente 2 invece di dare il messaggio somma il valore delle 2 provvigioni in un unoco record di provvig
#End Region

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModPR
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
#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDPRGNPV"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldGnpv = CType(MyBase.ocldBase, CLDPRGNPV)
    oCldGnpv.Init(oApp)
    Return True
  End Function

  Public Overridable Function edAgente_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If
      If Not oCldGnpv.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABCAGE", "S", strDescr) Then
        If nCod = 9999 Then
          strDescr = ""
          Return True
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128726890571738651, "Codice agente |'" & nCod.ToString & "'| inesistente")))
          Return False
        End If
      End If
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function edTipoBF_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If
      If Not oCldGnpv.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABTPBF", "S", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128726890698612839, "Tipo Bolla/Fattura |'" & nCod.ToString & "'| inesistente")))
        Return False
      End If
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function AggiornaStato(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                            ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                            ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                            ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                            ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                            ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                            ByVal bTipoDocumentoB As Boolean, _
                                            ByVal bCancellaProvvigioni As Boolean, _
                                            ByVal nTipoBF As Integer) As Boolean
    Try
      'non utilizzata. mantenuta solo per compatibilità con versioni precedenti
      Return AggiornaStato(strDatdocini, strDatdocfin, nAgenteini, nAgentefin, bTipoDocumentoA, bTipoDocumentoD, _
                           bTipoDocumentoE, bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, _
                           bTipoDocumentoP, bTipoDocumentoB, False, bCancellaProvvigioni, nTipoBF, True, "")
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
  Public Overridable Function AggiornaStato(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                          ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                          ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                          ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                          ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                          ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                          ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                          ByVal bCancellaProvvigioni As Boolean, _
                                          ByVal nTipoBF As Integer) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDatdocini, strDatdocfin, nAgenteini, nAgentefin, bTipoDocumentoA, bTipoDocumentoD, _
                           bTipoDocumentoE, bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, _
                           bTipoDocumentoP, bTipoDocumentoB, bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      'non utilizzata. mantenuta solo per compatibilità con versioni precedenti
      Return AggiornaStato(strDatdocini, strDatdocfin, nAgenteini, nAgentefin, bTipoDocumentoA, bTipoDocumentoD, _
                           bTipoDocumentoE, bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, _
                           bTipoDocumentoP, bTipoDocumentoB, bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, True, "")
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
  Public Overridable Function AggiornaStato(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                        ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                        ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                        ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                        ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                        ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                        ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                        ByVal bCancellaProvvigioni As Boolean, _
                                        ByVal nTipoBF As Integer, ByVal bAggTestmag As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDatdocini, strDatdocfin, nAgenteini, nAgentefin, bTipoDocumentoA, bTipoDocumentoD, _
                           bTipoDocumentoE, bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, _
                           bTipoDocumentoP, bTipoDocumentoB, bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, bAggTestmag})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      'non utilizzata. mantenuta solo per compatibilità con versioni precedenti
      Return AggiornaStato(strDatdocini, strDatdocfin, nAgenteini, nAgentefin, bTipoDocumentoA, bTipoDocumentoD, _
                           bTipoDocumentoE, bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, _
                           bTipoDocumentoP, bTipoDocumentoB, bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, bAggTestmag, "")
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
  Public Overridable Function AggiornaStato(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                            ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                            ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                            ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                            ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                            ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                            ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                            ByVal bCancellaProvvigioni As Boolean, ByVal nTipoBF As Integer, _
                                            ByVal bAggTestmag As Boolean, ByVal strWhere As String) As Boolean
    'strWhere = per ora passato solo da BEVEPNFA per filtrare un solo documento da elaborare
    'bAggTestmag = TRUE -> Ovvero rielaborazione: cancello le provvigioni estratte la volta precedente
    Dim nRec As Integer = 0
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDatdocini, strDatdocfin, nAgenteini, nAgentefin, bTipoDocumentoA, bTipoDocumentoD, _
                           bTipoDocumentoE, bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, _
                           bTipoDocumentoP, bTipoDocumentoB, bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, bAggTestmag, strWhere})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '-------------
      'se nuovo sistema, le provvigioni su incassato vanno cancellate solo se non pagate. 
      'se pagate devono essere mantenute perchè dopo la riestrazione il pagato deve essere mantenuto
      'inoltre se sono state pagate ma non c'è più il record in scaden devo cambiare l'origine da 'T' a 'Z'
      If bProvvig2 Then
        If Not oCldGnpv.AggiornaStatoIncassatoNew(strDittaCorrente, strDatdocini, strDatdocfin, _
                                                  nAgenteini, nAgentefin, _
                                                  bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, _
                                                  bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, _
                                                  bTipoDocumentoS, bTipoDocumentoP, bTipoDocumentoB, _
                                                  bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, nRec) Then Return False
        If nRec <> 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129482871384501953, _
                            "Sono state trovate provvigioni operanti sull'Incassato, PAGATE, " & _
                            "dove nell'archivio delle scadenze non è più presente la relativa scadenza. " & _
                            "Verranno marcate con origine 'Z'")))
        End If
      End If

      '-----------------------
      If bProvvig2 = False And bAggTestmag = False Then
        'vecchio sistema di gestione provvigioni e non ho spuntato rielaborazione: non devo cancellare nulla

      Else
        'nuovo sistema di provvigioni: cerco sempre di cancellare per riallineare provvig a scaden per nuovo incassato
        If Not oCldGnpv.AggiornaStato(strDittaCorrente, strDatdocini, strDatdocfin, _
                                      nAgenteini, nAgentefin, _
                                      bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, _
                                      bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, _
                                      bTipoDocumentoS, bTipoDocumentoP, bTipoDocumentoB, _
                                      bTipoDocumentoN1, bCancellaProvvigioni, nTipoBF, _
                                      bAggTestmag, bProvvig2, strWhere) Then Return False
      End If

      Return True

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Sub ChiudiDataTableTestmag()
    Try
      '-----------------------
      If Not dttTestmag Is Nothing Then dttTestmag.Clear()
      dttTestmag = Nothing
      '-----------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Function Elabora(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                      ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                      ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                      ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                      ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                      ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                      ByVal bTipoDocumentoB As Boolean, _
                                      ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                      ByVal nTipoBF As Integer, ByVal strDesTipoBF As String, _
                                      ByRef nNumDocElab As Integer, ByVal strOrigine As String, _
                                      ByVal bDaOrdini As Boolean) As Boolean
    Try
      'non più utilizzata. mantenuta per compatibilità con versioni precedenti
      Return Elabora(strDatdocini, strDatdocfin, nAgenteini, nAgentefin, bTipoDocumentoA, bTipoDocumentoD, _
                     bTipoDocumentoE, bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, _
                     bTipoDocumentoP, bTipoDocumentoB, False, bRielabora, bCancellaProvvigioni, nTipoBF, _
                     strDesTipoBF, nNumDocElab, strOrigine, bDaOrdini, "")
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
  Public Overridable Function Elabora(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                    ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                    ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                    ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                    ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                    ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                    ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                    ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                    ByVal nTipoBF As Integer, ByVal strDesTipoBF As String, _
                                    ByRef nNumDocElab As Integer, ByVal strOrigine As String, _
                                    ByVal bDaOrdini As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDatdocini, strDatdocfin, _
                                             nAgenteini, nAgentefin, _
                                             bTipoDocumentoA, bTipoDocumentoD, _
                                             bTipoDocumentoE, bTipoDocumentoN, _
                                             bTipoDocumentoC, bTipoDocumentoF, _
                                             bTipoDocumentoS, bTipoDocumentoP, _
                                             bTipoDocumentoB, bTipoDocumentoN1, _
                                             bRielabora, bCancellaProvvigioni, _
                                             nTipoBF, strDesTipoBF, _
                                             nNumDocElab, strOrigine, _
                                             bDaOrdini})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        nNumDocElab = NTSCInt(oIn(18))
        Return CBool(oOut)
      End If

      Return Elabora(strDatdocini, strDatdocfin, nAgenteini, nAgentefin, bTipoDocumentoA, bTipoDocumentoD, _
                     bTipoDocumentoE, bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, _
                     bTipoDocumentoP, bTipoDocumentoB, bTipoDocumentoN1, bRielabora, bCancellaProvvigioni, nTipoBF, _
                     strDesTipoBF, nNumDocElab, strOrigine, bDaOrdini, "")
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
  Public Overridable Function Elabora(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                      ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                      ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                      ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                      ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                      ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                      ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                      ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                      ByVal nTipoBF As Integer, ByVal strDesTipoBF As String, _
                                      ByRef nNumDocElab As Integer, ByVal strOrigine As String, _
                                      ByVal bDaOrdini As Boolean, ByVal strWhere As String) As Boolean
    'se strOrigine = 'X' è una simulazione (lavora solo su ddt emessi, DDT ricevuti da CLI -per resi- e imp. clienti)
    'bDaOrdini = true estrae i dati dagli imp. cli.
    '-------------------------
    Dim P As CLELBMENU.ParamCalcScad = New CLELBMENU.ParamCalcScad
    Dim PNew As CLELBMENU.ParamCalcScad = New CLELBMENU.ParamCalcScad
    Dim bOk As Boolean = False
    Dim lPv_conto As Integer = 0, lContoTmp As Integer = 0
    Dim i As Integer = 0, y As Integer = 0
    Dim nRataRd As Integer = 0, nRata As Integer = 0, nTipoPaga As Integer = 0
    Dim nTotRate As Integer = 0, nTotRatenew As Integer = 0
    Dim nTiprata(60) As Integer, nTipratanew(60) As Integer
    Dim nIniAge As Integer = 0, nFinAge As Integer = 0
    Dim nAgente As Integer = 0, nCodAgente As Integer = 0
    Dim dTotProv As Decimal = 0, dImpProv As Decimal = 0
    Dim dPv_totiva As Decimal = 0, dPv_totivav As Decimal = 0
    Dim dTm_totiva As Decimal = 0, dTm_totivav As Decimal = 0
    Dim dPv_totrd As Decimal = 0, dPv_totrdv As Decimal = 0
    Dim dPv_totrb As Decimal = 0, dPv_totrbv As Decimal = 0
    Dim dTotProvCalc As Decimal = 0, dTotProvMatu As Decimal = 0
    Dim dQuozRate(60) As Decimal
    Dim strMsg As String = "", strErr As String = ""
    Dim strTipork As String = ""
    Dim strTipotass As String = "" : Dim strTipoProv As String = ""
    Dim dttTmp As New DataTable
    Dim evt As NTSEventArgs = Nothing

    Dim dtrRow() As DataRow
    Dim bChiesto As Boolean = False
    Dim nSc_numrata As Integer = 0
    Dim nSc_tiprata As Integer = 0
    Dim nSc_segno As Integer = 1    'in questo programma il segno è sempre = 1. può essere 0 solo per le provvigioni insolute RATA ORIGINARIA pe fare in modo che vengano scartate in fase di calcolo maturato. VIENE CAMBIATO DI SEGNO SOLO ALLA FINE PER PROVVIGIONI CON IMPORTO PROVV NEGATIVO!!!!
    Dim dSc_importo As Decimal = 0

    Dim dImpvmat As Decimal = 0
    Dim dImpvpag As Decimal = 0
    Dim strDatmatu As String = ""
    Dim strDatcorr As String = ""
    Dim strPvflag As String = "N"

    Dim dttSc As New DataTable      'datatable contenente i dati di provvig letti da scaden (solo per nuovo 'incassato')

    Dim bProvvigioneEsistente As Boolean = False
    Dim dProvvPrima As Decimal = 0
    Dim dProvvDopo As Decimal = 0
    Dim strTmp As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDatdocini, strDatdocfin, _
                                             nAgenteini, nAgentefin, _
                                             bTipoDocumentoA, bTipoDocumentoD, _
                                             bTipoDocumentoE, bTipoDocumentoN, _
                                             bTipoDocumentoC, bTipoDocumentoF, _
                                             bTipoDocumentoS, bTipoDocumentoP, _
                                             bTipoDocumentoB, bTipoDocumentoN1, _
                                             bRielabora, bCancellaProvvigioni, _
                                             nTipoBF, strDesTipoBF, _
                                             nNumDocElab, strOrigine, _
                                             bDaOrdini, strWhere})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        nNumDocElab = NTSCInt(oIn(18))
        Return CBool(oOut)
      End If
      '----------------

      '-----------------------
      nNumDocElab = 0
      '-----------------------
      PulisciDataTables()
      ChiudiDataTableTestmag()
      '-----------------------
      'Legge le impostazioni di registro
      '-----------------------
      bConsIvaSuPagamenti = CBool(oCldGnpv.GetSettingBus("BSPRGNPV", "OPZIONI", ".", "ConsIvaSuPagamenti", "0", " ", "0"))
      bUsaSempreNumdoc = CBool(oCldGnpv.GetSettingBus("BSPRGNPV", "OPZIONI", ".", "UsaSempreNumdoc", "0", " ", "0"))
      bSommaProvvAg1e2StessoDoc = CBool(oCldGnpv.GetSettingBus("BSPRGNPV", "OPZIONI", ".", "SommaProvvAg1e2StessoDoc", "0", " ", "0"))

      '-----------------------
      bIncassoTotaleUsaDatDocSuDatScad = CBool(oCldGnpv.GetSettingBus("BSPRGNPV", "OPZIONI", ".", "IncassoTotaleUsaDatDocSuDatScad", "0", " ", "0")) 'Se abilitata, nella riga di provvigione generata per gli agenti di tipo 'incasso totale' indica la data scadenza pari alla data del documento invece della data scadenza dell'ultima rata di provvigione
      '-----------------------
      strMsg = "Selezione dati da documenti di magazzino in corso..."
      ThrowRemoteEvent(New NTSEventArgs("INFORMAZIONI:" & strMsg, ""))
      '-----------------------
      'Apre un DataTable su TESTMAG/TESTORD sul quale ciclare 
      If bDaOrdini = False Then
        If oCldGnpv.SelezionaDocumenti(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                                        bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, _
                                        bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, _
                                        bTipoDocumentoS, bTipoDocumentoP, bTipoDocumentoB, _
                                        bTipoDocumentoN1, CBool(IIf(strOrigine = "X", True, False)), _
                                        bRielabora, bCancellaProvvigioni, nTipoBF, strDesTipoBF, strOrigine, _
                                        dttTestmag, bProvvig2, strWhere) = False Then
          strMsg = "Nessuna elaborazione in corso"
          ThrowRemoteEvent(New NTSEventArgs("INFORMAZIONI:" & strMsg, ""))
          Return False
        End If
      Else
        '----------------
        'rispetto a testmag, qui prendo le righe di movord e devo ricalcolare le provvigioni 
        'perchè una parte di ordine potrebbe già essere stata evasa, quindi devo prendere solo il residuo
        'inoltre devo calcolare le date di scadenza
        If oCldGnpv.SelezionaOrdini(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                                    dttTestmag) = False Then
          strMsg = "Nessuna elaborazione in corso"
          ThrowRemoteEvent(New NTSEventArgs("INFORMAZIONI:" & strMsg, ""))
          Return False
        End If
        'aggiusto importo provvigione e calcolo le scadenze
        For Each dtrT As DataRow In dttTestmag.Rows
          If Not Elabora_CalcolaProvvOrdini(dtrT) Then Return False
        Next
        dttTestmag.AcceptChanges()
      End If    'If bDaOrdini = False Then

      'aggiungo in testmag la colonna 'xx_trattato': servirà per sapere se il documento è stato processato oppure no
      dttTestmag.Columns.Add("xx_trattato", GetType(String))

      '-----------------------
      'Apre due DataTable vuoti su PROVVIG
      '(uno per le INSERT INTO, l'altro per le UPDATE)
      bOk = oCldGnpv.ApriStrutturaProvvig(dttProvvigINSERT, dttProvvigUPDATE)
      For nIndiceTestmag = 0 To dttTestmag.Rows.Count - 1
        dttTestmag.Rows(nIndiceTestmag)!xx_trattato = "S" 'default
        strMsg = "Conteggio documento " & (nIndiceTestmag + 1).ToString & " di " & dttTestmag.Rows.Count.ToString & " in corso..."
        ThrowRemoteEvent(New NTSEventArgs("INFORMAZIONI:" & strMsg, ""))

        'se devo elaborare la simulazione scarto i ddt emessi (gli unici documenti in elaborazione) già fatturati
        If strOrigine = "X" And dttTestmag.Rows(nIndiceTestmag)!tm_flfatt.ToString = "S" Then GoTo RecordSuccessivo

        '-----------------------
        lContoTmp = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_conto)
        Select Case strUsaContoFattDoc
          Case "1"
            Select Case NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_tipork)
              Case "A", "C", "D", "E", "J", "K", "L", "N", "£", "R", "M"
                If NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_contfatt) <> 0 Then lContoTmp = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_contfatt)
            End Select
          Case "2"
            Select Case NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_tipork)
              Case "A", "C", "E", "J", "L", "N", "£", "R", "M"
                If NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_contfatt) <> 0 Then lContoTmp = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_contfatt)
            End Select
        End Select
        strTipotass = oCldGnpv.RitornaTipoAss(strDittaCorrente, lContoTmp)
        '-----------------------
        'Verifica degli agenti sui quali generare la provvigione
        nIniAge = 1 : nFinAge = 1
        If NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_codagen2) <> 0 Then
          nFinAge = 2
          If NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_codagen) = 0 Then nIniAge = 2
        End If
        lPv_conto = lContoTmp
        '-----------------------
        'Se:
        'tm_codagen =  0 --> nIniAge = 2 e nFinAge = 2
        'tm_codagen2 = 0 --> nIniAge = 1 e nFinAge = 1
        'entrambe  <>  0 --> nIniAge = 1 e nFinAge = 2 quindi nAgente = 1 poi = 2
        '-----------------------
        For nAgente = nIniAge To nFinAge
          With dttTestmag.Rows(nIndiceTestmag)
            nCodAgente = NTSCInt(IIf(nAgente = 1, NTSCInt(!tm_codagen), NTSCInt(!tm_codagen2)))
            If (nCodAgente < nAgenteini) Or (nCodAgente > nAgentefin) Then GoTo Passa
            dTotProv = NTSCDec(IIf(nAgente = 1, NTSCDec(!tm_totprov), NTSCDec(!tm_totprov2)))
            dImpProv = NTSCDec(!tm_impprov)
          End With
          '-----------------------
          'Preleva il Tipo Provvigione dell'agente corrente in TABCAGE
          '-----------------------
          If Not oCldGnpv.ValCodiceDb(nCodAgente.ToString, strDittaCorrente, "TABCAGE", "N", "", dttTmp) Then
            With dttTestmag.Rows(nIndiceTestmag)
              strMsg = "Attenzione!" & vbCrLf & _
                        "Documento '|" & !tm_tipork.ToString & "|'" & _
                        " n° |" & !tm_numdoc.ToString & _
                        IIf(!tm_serie.ToString.Trim <> "", "/", "").ToString & !tm_serie.ToString.Trim & "|" & _
                        " del |" & !tm_anno.ToString & "|" & _
                        " data |" & !tm_datdoc.ToString & "|" & vbCrLf & _
                        "Codice Agente NON trovato in tabella Agenti (TABCAGE)."
            End With
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128727895849904684, strMsg)))
            dttTmp.Clear()
            dttTmp = Nothing
            GoTo RecordSuccessivo
          Else
            strTipoProv = NTSCStr(dttTmp.Rows(i)!tb_tippro.ToString)
            dttTmp.Clear()
          End If

          '-----------------------
          'elaborazione su incassato totale:
          If strTipoProv = "T" Then
            If Not ElaboraIncassatoTotale(nCodAgente, dTotProv, dImpProv, strTipotass, strOrigine) Then
              GoTo RecordSuccessivo
            Else
              GoTo Passa
            End If
          End If
          '-----------------------
          With dttTestmag.Rows(nIndiceTestmag)
            'calcolo i totali del documento e calcolo le scadenze
            If Not Elabora_CalcolaTotaliDoc(dttTestmag.Rows(nIndiceTestmag), P, dPv_totiva, dPv_totivav, _
                                             dTm_totiva, dTm_totivav, nRataRd, nTotRate, lPv_conto, _
                                             nTipoPaga, dPv_totrd, dPv_totrdv, dPv_totrb, dPv_totrbv, _
                                             strTipoProv, nTotRatenew, PNew, nTiprata, nTipratanew, strOrigine) Then GoTo RecordSuccessivo

            '-----------------------
            'Crea vettore dei quozienti 
            If Not Elabora_CalcolaQuote(dttTestmag.Rows(nIndiceTestmag), nTotRate, dQuozRate, dTm_totiva, P) Then Return False

            If bProvvig2 And strTipoProv = "I" And strOrigine <> "X" Then
              If NTSCDec(dttTestmag.Rows(nIndiceTestmag)!tm_totdoc) - NTSCDec(dttTestmag.Rows(nIndiceTestmag)!tm_totomag) = 0 Then
                'fattura con provvigione indicata MA totale documento = 0
                'proseguo
                nTotRate = 1
                If Not dttSc Is Nothing Then dttSc.Clear()
              Else
                'caso normale
                If Not Elabora_CalcolaRateIncassatoNew(dttTestmag.Rows(nIndiceTestmag), lPv_conto, dTotProv, _
                                                       dImpProv, nTotRate, dttSc) Then
                  dttTestmag.Rows(nIndiceTestmag)!xx_trattato = "N"
                  GoTo Passa
                End If
              End If
            End If

            'Per ogni rata occorre una entry in Provvig
            dTotProvCalc = 0 : dTotProvMatu = 0 : nSc_numrata = 0
            For nRata = 1 To nTotRate
              nSc_numrata += 1
              If NTSCDec(dttTestmag.Rows(nIndiceTestmag)!tm_totomag) <> 0 And nSc_numrata = 1 Then
                'se nel documento ci sono delgi omaggi, la rata 1 è data dagli omaggi (totalmente incassi) e le rate successive partono da 1
                'se lascio come rata 1, da BNCGINSO quando rilevo l'insoluto segnala che la rata 1 non è stata trovata (perchè in effeti il num. di rata deve essere 2)
                nSc_numrata += 1
              End If
              nSc_tiprata = nTiprata(nRata)
              nSc_segno = 1
              dSc_importo = 0
              dImpvmat = 0
              If dttSc.Columns.Contains("xx_maturato") Then
                If dttSc.Rows.Count > 0 Then
                  dImpvmat = NTSCDec(dttSc.Rows(nRata - 1)!xx_maturato)
                End If
              End If
              dImpvpag = 0
              strDatmatu = ""
              If dttSc.Columns.Contains("xx_dtmaturaz") Then
                If dttSc.Rows.Count > 0 Then
                  If NTSCStr(dttSc.Rows(nRata - 1)!xx_dtmaturaz) <> "" Then strDatmatu = NTSCDate(dttSc.Rows(nRata - 1)!xx_dtmaturaz).ToShortDateString
                End If
              End If
              strDatcorr = ""
              strPvflag = "N"

              'Prelevo anno, serie e numero dal documento
              nPv_annpart = NTSCInt(!tm_anno)
              strPv_alfpart = NTSCStr(!tm_serie)
              lPv_numpart = NTSCInt(!tm_numdoc)
              If bUsaSempreNumdoc = False And NTSCInt(!tm_numpar) <> 0 Then
                'Prelevo anno, serie e numero dalla partita
                nPv_annpart = NTSCInt(!tm_annpar)
                strPv_alfpart = NTSCStr(!tm_alfpar)
                lPv_numpart = NTSCInt(!tm_numpar)
              End If
              dPv_totfatt = NTSCDec(!tm_totdoc)
              dPv_vtotfatt = NTSCDec(!tm_totdocv)
              dPv_totomag = NTSCDec(!tm_totomag)
              dPv_totomagv = NTSCDec(!tm_totomagv)
              If NTSCStr(!tm_tipork) = "N" Or NTSCStr(!tm_tipork) = "£" Or NTSCStr(!tm_tipork) = "M" Then
                dPv_totfatt = (dPv_totfatt * -1)
                dPv_vtotfatt = (dPv_vtotfatt * -1)
                dPv_totomag = (dPv_totomag * -1)
                dPv_totomagv = (dPv_totomagv * -1)
              End If
              'Se è la rata RD di un pagamenot misto, mette in totale omaggi
              'l'importo delle RB emesse
              If (nRata = nRataRd) And (nTotRate > 1) Then
                If bProvvig2 = False Then
                  dPv_totomag += (dPv_totrb + dPv_totiva)
                  dPv_totomagv += (dPv_totrbv + dPv_totivav)
                Else
                  'nuovo sistema di calcolo sull'incassato: negli omaggi ci sono solo gli omaggi
                  dPv_totomag += dPv_totiva
                  dPv_totomagv += dPv_totivav
                End If
              End If
              If nTotRate = 1 Then
                dPv_totomag += dPv_totiva
                dPv_totomagv += dPv_totivav
              End If
              If (strTipoProv = "M") Or (strTipoProv = "I" And nTipoPaga <= 2) Then
                dtPv_datscad = NTSCDate(P.strDatsca(nRata))
                If (nRata = nTotRate And nRata > 1) Or nTotRate = 1 Then
                  dPv_provv = (dTotProv - dTotProvCalc)
                  dPv_impopv = (dImpProv - dTotProvMatu)
                Else
                  dPv_provv = ArrDbl(dTotProv * dQuozRate(nRata), oCldGnpv.TrovaNdec(0))
                  dPv_impopv = ArrDbl(dImpProv * dQuozRate(nRata), oCldGnpv.TrovaNdec(0))
                  'dTotProvCalc = (dTotProvCalc + Math.Abs(dPv_provv))
                  'dTotProvMatu = (dTotProvMatu + Math.Abs(dPv_impopv))
                  '--- Il calcolo dell'ultima rata (nel caso di più rate) se, l'importo della provvigione
                  '--- era negativo, veniva sommato manziché sottratto (vedi caso GAIA, QUESITO n°57396)
                  dTotProvCalc = (dTotProvCalc + dPv_provv)
                  dTotProvMatu = (dTotProvMatu + dPv_impopv)
                End If
              Else
                dtPv_datscad = NTSCDate(!tm_datdoc)
                dPv_provv = dTotProv
                dPv_impopv = dImpProv
              End If
              If NTSCStr(!tm_tipork) = "N" Or NTSCStr(!tm_tipork) = "£" Or NTSCStr(!tm_tipork) = "M" Then
                dPv_provv = (dPv_provv * -1)
                dPv_impopv = (dPv_impopv * -1)
              End If
              '--------------------------
              dPv_vimpopv = 0
              dPv_vprovv = 0
              'dPv_vimpvmat = 0
              'dPv_vimpvpag = 0
              If NTSCInt(!tm_valuta) > 0 Then
                dPv_vimpopv = oCldGnpv.ConvImpEur(False, dPv_impopv, NTSCInt(!tm_valuta), NTSCDate(!tm_datdoc), NTSCDec(!tm_cambio))
                dPv_vprovv = oCldGnpv.ConvImpEur(False, dPv_provv, NTSCInt(!tm_valuta), NTSCDate(!tm_datdoc), NTSCDec(!tm_cambio))
                'dPv_vimpvpag = oCldGnpv.ConvImpEur(False, dPv_impvpag, NTSCInt(!tm_valuta), NTSCDate(!tm_datdoc), NTSCDec(!tm_cambio))
              End If

              '--------------------------
              'scriverò i dati prendendoli da 'Elabora_CalcolaRateIncassatoNew'
              If bProvvig2 And strTipoProv = "I" Then
                If strOrigine = "X" Then
                  'simulazione e ordine con pagam. con rimessa diretta
                  If nTotRate = 1 Then dtPv_datscad = NTSCDate(!tm_datsca_1)
                Else

                  If Not dttSc Is Nothing AndAlso dttSc.Rows.Count > 0 Then
                    'caso standard
                    nSc_numrata = NTSCInt(dttSc.Rows(nRata - 1)!sc_numrata)
                    nSc_tiprata = NTSCInt(dttSc.Rows(nRata - 1)!sc_tippaga)
                    dtPv_datscad = NTSCDate(dttSc.Rows(nRata - 1)!sc_datscad)
                    dPv_provv = NTSCDec(dttSc.Rows(nRata - 1)!sc_provv)
                    dPv_impopv = NTSCDec(dttSc.Rows(nRata - 1)!sc_impopv)
                    nSc_segno = NTSCInt(dttSc.Rows(nRata - 1)!sc_segno)
                    dSc_importo = NTSCDec(dttSc.Rows(nRata - 1)!sc_importo)
                  Else
                    'fattura con totale documento = 0 (non ho scadenze)
                  End If

                End If
              End If    'If bProvvig2 And strTipoProv = "I" Then

              '-------------------------
              'Prima di inserire il record nel DataTable, controlla se esiste già
              'nel DataBase.
              'Nel caso chiede conferma nel proseguimento.
              bProvvigioneEsistente = False
              If strOrigine <> "X" Then
                'per cercare la provvigione 
                'nel nuovo incassato, il num di rata corrisponde sempre alla rata di scaden
                'nel vecchio incassato uso nRata. è sempre andato bene e non mi fido di cambiarlo ...
                If oCldGnpv.EsisteProvvigione(strDittaCorrente, nCodAgente, NTSCStr(!tm_tipork), _
                                              NTSCInt(!tm_anno), NTSCStr(!tm_serie), NTSCInt(!tm_numdoc), _
                                              NTSCInt(IIf(bProvvig2 And strTipoProv = "I", nSc_numrata, nRata)), "M", dttTmp) Then

                  If NTSCStr(dttTmp.Rows(0)!pv_origine) = "T" And NTSCDec(dttTmp.Rows(0)!pv_impvpag) <> 0 Then
                    'provvigione già corrisposta su incassato nuovo sistema: sulla scadenza devo mantenere l'importo e la data corresponsione
                    'oltre che allo status della riga
                    dImpvmat = NTSCDec(dttTmp.Rows(0)!pv_impvmat)
                    dImpvpag = NTSCDec(dttTmp.Rows(0)!pv_impvpag)
                    '------------------------------------------------------------------------------------------------
                    '--- Memorizza l'importo della provvigione esistente, sulla riga, prima dell'elaborazione
                    '------------------------------------------------------------------------------------------------
                    bProvvigioneEsistente = True
                    dProvvPrima = NTSCDec(dttTmp.Rows(0)!pv_provv)
                    '------------------------------------------------------------------------------------------------
                    If NTSCStr(dttTmp.Rows(0)!pv_datmatu) <> "" Then strDatmatu = NTSCDate(dttTmp.Rows(0)!pv_datmatu).ToShortDateString
                    If NTSCStr(dttTmp.Rows(0)!pv_datcorr) <> "" Then strDatcorr = NTSCDate(dttTmp.Rows(0)!pv_datcorr).ToShortDateString
                    strPvflag = NTSCStr(dttTmp.Rows(0)!pv_flag)
                    oCldGnpv.CancellaProvvigione(strDittaCorrente, NTSCInt(dttTmp.Rows(0)!pv_progr))
                    dttTmp.Clear()
                  Else
                    Select Case NTSCStr(!tm_tipork)
                      Case "A" : strTipork = "Fattura Immediata Emessa"
                      Case "B" : strTipork = "D.D.T. Emesso"
                      Case "C" : strTipork = "Corrispettivo Emesso"
                      Case "D" : strTipork = "Fattura Differita Emessa"
                      Case "E" : strTipork = "Nota di Addebito emessa"
                      Case "F" : strTipork = "Ricevuta Fiscale Emessa"
                      Case "I" : strTipork = "Riemissione Ricevuta Fiscale"
                      Case "J" : strTipork = "Nota di Accredito Ricevuta"
                      Case "K" : strTipork = "Fattura Differita Ricevuta"
                      Case "L" : strTipork = "Fattura Immediata Ricevuta"
                      Case "M" : strTipork = "D.D.T. Ricevuto"
                      Case "N" : strTipork = "Nota di Accredito Emessa"
                      Case "£" : strTipork = "Nota di Accredito Differita Emessa"
                      Case "P" : strTipork = "Fattura/Ricevuta Fiscale Differita"
                      Case "S" : strTipork = "Fattura/Ricevuta Fiscale Emessa"
                      Case "T" : strTipork = "Carico da Produzione"
                      Case "U" : strTipork = "Scarico a Produzione"
                      Case "Z" : strTipork = "Bolla di Movimentazione Interna"
                    End Select
                    strMsg = "Attenzione!" & vbCrLf & _
                              "Si sta cercando di inserire una provvigione già esistente" & vbCrLf & _
                              "per l'agente n°|" & NTSCInt(!tm_codagen) & "|." & vbCrLf & _
                              "Controllare che '|" & strTipork & "|' n°" & NTSCStr(!tm_numdoc) & "|, serie '|" & NTSCStr(!tm_serie) & "|' del |" & NTSCStr(!tm_datdoc) & "|" & vbCrLf & _
                              "non presenti lo stesso codice per agente e subagente." & vbCrLf & _
                              "Altrimenti agire direttamente dal programma 'Gestione Provvigioni'." & _
                              "Continuare?"
                    evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128729429388949559, strMsg))
                    ThrowRemoteEvent(evt)
                    Select Case evt.RetValue
                      Case "NO"
                        PulisciDataTables()
                        ChiudiDataTableTestmag()
                        Return False
                      Case "YES"
                        GoTo ProssimaRata
                    End Select
                  End If    'If NTSCStr(dttTmp.Rows(0)!pv_origine) = "T" And NTSCDec(dttTmp.Rows(0)!pv_impvpag) <> 0 Then
                End If    'If oCldGnpv.EsisteProvvigione(strDittaCo

                If Not bChiesto Then
                  ' Controllo che non sia presente una riga identica a quella da inserire
                  dtrRow = dttProvvigINSERT.Select("pv_codage = " & nCodAgente & " AND pv_tipdoc = '" & NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_tipork) & _
                                                   "' AND pv_anno = " & NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_anno) & _
                                                   " AND pv_serie = '" & NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_serie) & _
                                                   "' AND pv_numdoc = " & NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_numdoc) & _
                                                   " AND pv_numpart = " & lPv_numpart & " AND pv_numrat = " & nSc_numrata)

                  If dtrRow.Length <> 0 Then
                    If bSommaProvvAg1e2StessoDoc Then
                      'se ho settato l'opzione di registro, sommo la provvigione 2 alla provvigione 1
                      'sempre che i 2 agenti siano uguali
                      If bProvvig2 And dPv_impopv < 0 And nSc_segno = 1 Then  'basarsi su imponibile provv, non su importo!!!!
                        dtrRow(0)!pv_provv = NTSCDec(dtrRow(0)!pv_provv) + (dPv_provv * -1)
                        dtrRow(0)!pv_vprovv = NTSCDec(dtrRow(0)!pv_vprovv) + (dPv_vprovv * -1)
                      Else
                        dtrRow(0)!pv_provv = NTSCDec(dtrRow(0)!pv_provv) + dPv_provv
                        dtrRow(0)!pv_vprovv = NTSCDec(dtrRow(0)!pv_vprovv) + dPv_vprovv
                      End If
                    Else
                      'caso standard:avverto
                      Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128904999670467120, "Attenzione!" & vbCrLf & _
                                                      "Si sta cercando di inserire una provvigione doppia per l'agente n° " & nCodAgente & _
                                                      " controllare che " & NomeTipoRk(NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_tipork)) & _
                                                      " n° " & NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_numdoc) & _
                                                      " serie '" & NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_serie) & "' del " & _
                                                      NTSCDate(dttTestmag.Rows(nIndiceTestmag)!tm_datdoc) & " non presenti lo stesso agente sia in testata che nel piede." & vbCrLf & _
                                                      "Se si scegliere di continuare le provvigioni doppie saranno salvate una sola volta." & vbCrLf & _
                                                      "Altrimenti agire direttamente dal programma 'Gestione Provvigioni'." & vbCrLf & _
                                                      "Continuare?"))
                      ThrowRemoteEvent(msg)

                      If msg.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False

                      bChiesto = True
                    End If

                    GoTo ProssimaRata
                  End If
                End If
              End If    'If strOrigine <> "X" Then

              '---------------------
              'Inserisce nel DataTable di PROVVIG
              dttProvvigINSERT.Rows.Add(dttProvvigINSERT.NewRow)

              With dttProvvigINSERT.Rows(dttProvvigINSERT.Rows.Count - 1)
                !codditt = NTSCStr(strDittaCorrente)
                !pv_codage = nCodAgente
                !pv_tipdoc = NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_tipork)
                !pv_anno = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_anno)
                !pv_serie = NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_serie)
                !pv_numdoc = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_numdoc)
                !pv_numrat = nSc_numrata
                !pv_datdoc = NTSCDate(dttTestmag.Rows(nIndiceTestmag)!tm_datdoc)
                !pv_datscad = NTSCDate(dtPv_datscad)
                !pv_conto = lPv_conto
                !pv_annpart = nPv_annpart
                !pv_alfpart = strPv_alfpart
                !pv_numpart = lPv_numpart
                !pv_totfatt = dPv_totfatt
                !pv_vtotfatt = dPv_vtotfatt
                !pv_totomag = dPv_totomag
                !pv_totomagv = dPv_totomagv

                !pv_impscad = dSc_importo

                !pv_segno = nSc_segno
                !pv_impopv = dPv_impopv
                !pv_provv = dPv_provv
                !pv_vimpopv = dPv_vimpopv
                !pv_vprovv = dPv_vprovv

                !pv_impvmat = dImpvmat
                If strDatmatu <> "" Then !pv_datmatu = NTSCDate(strDatmatu)

                !pv_impvpag = dImpvpag
                If strDatcorr <> "" Then !pv_datcorr = NTSCDate(strDatcorr)

                !pv_vimpvmat = 0 'NTSCDec(dPv_vimpvmat)
                !pv_vimpvpag = 0 'NTSCDec(dPv_vimpvpag)

                'Dim nNumdoc As Integer = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_numdoc)
                'per verificare se invertire di seggno non testo l'importo della provvigione, ma l'imponibile: 
                'questo perchè se il documento è positivo ma la % provv è negativa verrebbe sballato tutto
                If bProvvig2 And dPv_impopv < 0 And nSc_segno = 1 Then
                  !pv_segno = NTSCDec(!pv_segno) * -1
                  !pv_totfatt = dPv_totfatt * -1
                  !pv_vtotfatt = dPv_vtotfatt * -1
                  !pv_totomag = dPv_totomag * -1
                  !pv_totomagv = dPv_totomagv * -1
                  !pv_impopv = dPv_impopv * -1
                  !pv_provv = dPv_provv * -1
                  !pv_vimpopv = dPv_vimpopv * -1
                  !pv_vprovv = dPv_vprovv * -1
                End If

                !pv_codpaga = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_codpaga)
                !pv_tippaga = nSc_tiprata
                !pv_flag = strPvflag
                !pv_origine = IIf(bProvvig2 And strTipoProv = "I" And strOrigine = "M", "T", strOrigine).ToString
                !pv_tipotass = NTSCStr(strTipotass)

                !pv_cambio = NTSCDec(dttTestmag.Rows(nIndiceTestmag)!tm_cambio.ToString)
                !pv_codvalu = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_valuta)
                '----------------------------------------------------------------------------------------------------
                '--- Memorizza l'importo della nuova provvigione calcolata e inserita in riga
                '----------------------------------------------------------------------------------------------------
                dProvvDopo = NTSCDec(!pv_provv)
                '----------------------------------------------------------------------------------------------------
              End With
              dttProvvigINSERT.AcceptChanges()
              '------------------------------------------------------------------------------------------------------
              strTmp = oApp.Tr(Me, 130447058462330835, "ATTENZIONE:" & _
                " Provvigione relativa all'Agente '|" & nCodAgente.ToString & "|'" & _
                " Documento: Tipo |" & NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_tipork) & "| N°|" & NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_numdoc) & IIf(NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_serie).Trim <> "", "/", "").ToString & NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_serie).Trim & "| del |" & NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_anno) & "|" & _
                " Rata: |" & nSc_numrata.ToString & "|" & _
                " Origine: |" & IIf(bProvvig2 And strTipoProv = "I" And strOrigine = "M", "T", strOrigine).ToString & "|" & _
                " Importo Provvigione precedente: |" & dProvvPrima.ToString(oApp.FormatImporti) & "|" & _
                " Nuovo Importo Provvigione: |" & dProvvDopo.ToString(oApp.FormatImporti) & "|")
              '------------------------------------------------------------------------------------------------------
ProssimaRata:
            Next    'For nRata = 1 To nTotRate

          End With
Passa:
        Next nAgente '<-- Ciclo For sugli Agenti del record corrente di TESTMAG
        '-----------------------
        strMsg = "Aggiornamento/Inserimento provvigioni " & (nIndiceTestmag + 1).ToString & " di " & dttTestmag.Rows.Count.ToString & " in corso..."
        ThrowRemoteEvent(New NTSEventArgs("INFORMAZIONI:" & strMsg, ""))
        '----------------------------
        If Not oCldGnpv.AggiornaInserisciProvvigione(dttProvvigINSERT, dttProvvigUPDATE, dttTestmag, _
                                                     NTSCInt(IIf(strOrigine <> "X", nIndiceTestmag, -1))) Then GoTo RecordSuccessivo
        '------------------------------------------------------------------------------------------------------------
        '--- Se la provvigione esisteva già e l'importo provvigione è diverso, lo segnala nel file di LOG
        '------------------------------------------------------------------------------------------------------------
        If (bProvvigioneEsistente = True) And (dProvvPrima <> dProvvDopo) Then LogWrite(strTmp, True)
        '------------------------------------------------------------------------------------------------------------
        If strOrigine <> "X" Then
          ScriviActLog(strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                      bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, _
                      bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, _
                      bTipoDocumentoS, bTipoDocumentoP, bTipoDocumentoB, _
                      bTipoDocumentoN1, bRielabora, bCancellaProvvigioni, _
                      nTipoBF, strDesTipoBF)
        End If
        '-----------------------
        PulisciDataTables()
        '-----------------------
        nNumDocElab += 1
        '-----------------------
RecordSuccessivo:
        '-----------------------
      Next nIndiceTestmag  '<-- Loop su DataTable TESTMAG
      '-----------------------
      PulisciDataTables()
      ChiudiDataTableTestmag()

      strMsg = "Nessuna elaborazione in corso"
      ThrowRemoteEvent(New NTSEventArgs("INFORMAZIONI:" & strMsg, ""))

      Return True

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      strMsg = "Nessuna elaborazione in corso"
      ThrowRemoteEvent(New NTSEventArgs("INFORMAZIONI:" & strMsg, ""))
    End Try
  End Function
  Public Overridable Function Elabora_CalcolaTotaliDoc(ByRef dtrT As DataRow, ByRef P As CLELBMENU.ParamCalcScad, _
                                                       ByRef dPv_totiva As Decimal, ByRef dPv_totivav As Decimal, _
                                                       ByRef dTm_totiva As Decimal, ByRef dTm_totivav As Decimal, _
                                                       ByRef nRataRd As Integer, ByRef nTotRate As Integer, _
                                                       ByRef lPv_conto As Integer, ByRef nTipoPaga As Integer, _
                                                       ByRef dPv_totrd As Decimal, ByRef dPv_totrdv As Decimal, _
                                                       ByRef dPv_totrb As Decimal, ByRef dPv_totrbv As Decimal, _
                                                       ByVal strTipoProv As String, ByRef nTotRatenew As Integer, _
                                                       ByRef PNew As CLELBMENU.ParamCalcScad, _
                                                       ByRef nTiprata() As Integer, ByRef nTipratanew() As Integer, _
                                                       ByVal strOrigine As String) As Boolean
    Dim Cont As Integer
    Dim strErr As String = ""
    Dim nRata As Integer
    Try
      '---------------------
      'Azzera i valori iva (opzione bConsIvaSuPagamenti)
      dPv_totiva = 0 : dPv_totivav = 0 : dTm_totiva = 0 : dTm_totivav = 0
      With dtrT

        P.nCodpaga = NTSCInt(!tm_codpaga)
        P.strDatrif = NTSCStr(!tm_datdoc)
        If (NTSCInt(!tb_decpaga) = 4) And (NTSCStr(!tm_datapag) <> "") Then
          P.strDatrif = NTSCStr(!tm_datapag)
        End If
        P.dTotfat = (NTSCDec(!tm_totdoc) - NTSCDec(!tm_totomag))
        'Calcolo IVA
        P.dIva = 0 : P.dIvaval = 0
        For Cont = 1 To 8
          P.dIva += NTSCDec(dttTestmag.Rows(nIndiceTestmag)("tm_imposta_" & Cont.ToString))
          P.dIvaval += NTSCDec(dttTestmag.Rows(nIndiceTestmag)("tm_impostav_" & Cont.ToString))
          'Se Iva su prima rata e settato il parametro
          If (NTSCInt(!tb_prirata) > 0) And (bConsIvaSuPagamenti = True) Then
            dPv_totiva += NTSCDec(dttTestmag.Rows(nIndiceTestmag)("tm_imposta_" & Cont.ToString))
            dPv_totivav += NTSCDec(dttTestmag.Rows(nIndiceTestmag)("tm_impostav_" & Cont.ToString))
          End If
        Next
        'Aggiusta dpv_totiva e setta dtm_totiva
        dTm_totiva = dPv_totiva
        dTm_totivav = dPv_totivav
        'Inverte il segno su dpv_ se si tratta di Nota di Accredito
        If NTSCStr(!tm_tipork) = "N" Or NTSCStr(!tm_tipork) = "£" Or NTSCStr(!tm_tipork) = "M" Then
          dPv_totiva = (dPv_totiva * -1)
          dPv_totivav = (dPv_totivav * -1)
        End If
        P.dSpese = NTSCDec(!tm_bolli) + NTSCDec(!tm_speinc) + NTSCDec(!tm_speacc)
        P.dSpeseval = NTSCDec(!tm_speaccv)
        'Setta per controllo successivo
        nRataRd = 0
        'Se pagamento speciale o modificato dall'utente
        If (CBool(NTSCInt(!tm_ccambiati) And 1)) Or (NTSCStr(!tb_flcondp) = "S") Then
          'Li legge da TESTMAG o li calcola
          If Not (CBool(NTSCInt(!tm_ccambiati) And 1)) Then
            nTotRate = CType(oCleComm, CLELBMENU).CalcolaScad(strDittaCorrente, _
              P.nCodpaga, P.strDatrif, P.dTotfat, P.dTotfatval, P.dIva, P.dIvaval, _
              P.dSpese, P.dSpeseval, P.strDatsca, P.dImpsca, P.dImpscaval, nTiprata, _
              strErr, NTSCInt(!tm_valuta), lPv_conto)
            If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
          Else   'Cambiati
            For Cont = 1 To 8
              If NTSCInt(dttTestmag.Rows(nIndiceTestmag)("tm_tippaga_" & Cont.ToString)) <> 0 Then
                nTotRate = Cont
                P.strDatsca(Cont) = NTSCStr(dttTestmag.Rows(nIndiceTestmag)("tm_datsca_" & Cont.ToString))
                P.dImpsca(Cont) = NTSCDec(dttTestmag.Rows(nIndiceTestmag)("tm_impsca_" & Cont.ToString))
                P.dImpscaval(Cont) = NTSCDec(dttTestmag.Rows(nIndiceTestmag)("tm_impscav_" & Cont.ToString))
                nTiprata(Cont) = NTSCInt(dttTestmag.Rows(nIndiceTestmag)("tm_tippaga_" & Cont.ToString))
              End If
            Next
          End If
          'Ora sooma RD e RB a parte
          dPv_totrd = 0 : dPv_totrdv = 0 : dPv_totrb = 0 : dPv_totrbv = 0
          For nRata = 1 To nTotRate
            If nTiprata(nRata) < 3 Then
              dPv_totrb += P.dImpsca(nRata)
              dPv_totrbv += P.dImpscaval(nRata)
            Else
              dPv_totrd += P.dImpsca(nRata)
              dPv_totrdv += P.dImpscaval(nRata)
            End If
          Next
          'Adesso spezza, se necessario
          If dPv_totrd = 0 Then
            nTipoPaga = nTiprata(1)
            If strTipoProv = "F" Then nTotRate = 1 'Sul fatturato
            GoTo nRateOK 'Ci sono solo RB
          End If
          'Ci sono solo RD
          If dPv_totrb = 0 Then
            Select Case strTipoProv
              Case "I", "M"
                If strTipoProv = "I" Then 'Incassato
                  If nTiprata(1) > 2 Then
                    nTotRate = 1
                    nTipoPaga = nTiprata(1)
                    GoTo nRateOK
                  Else 'Pagamento RB/tratta, vai oltre
                    nTipoPaga = nTiprata(1)
                    GoTo nRateOK
                  End If
                End If
              Case Else 'Su fatturato
                nTotRate = 1
                nTipoPaga = nTiprata(1)
                GoTo nRateOK
            End Select
          End If
          'Il caso più complicato:
          'ci sono sia RB che RD --> raggruppa le RD e non le RB
          If strTipoProv = "F" Then 'Sul fatturato
            nTotRate = 1
            nTipoPaga = nTiprata(1)
            GoTo nRateOK
          End If
          'NON sul fatturato (inc. o maturato): crea nuovi vettori
          nTotRatenew = 0 : nRataRd = 0
          For nRata = 1 To nTotRate
            If nTiprata(nRata) < 3 Then 'RB
              nTotRatenew += 1
              PNew.strDatsca(nTotRatenew) = P.strDatsca(nRata)
              PNew.dImpsca(nTotRatenew) = P.dImpsca(nRata)
              PNew.dImpscaval(nTotRatenew) = P.dImpscaval(nRata)
              nTipratanew(nTotRatenew) = nTiprata(nRata)
            Else
              'RD (o bonifici, ecc.)
              If nRataRd = 0 Then
                nTotRatenew += 1
                PNew.strDatsca(nTotRatenew) = P.strDatsca(nRata)
                PNew.dImpsca(nTotRatenew) = P.dImpsca(nRata)
                PNew.dImpscaval(nTotRatenew) = P.dImpscaval(nRata)
                nTipratanew(nTotRatenew) = nTiprata(nRata)
                nRataRd = nTotRatenew
              Else 'Aggiunge
                PNew.dImpsca(nRataRd) += P.dImpsca(nRata)
                PNew.dImpscaval(nRataRd) += P.dImpscaval(nRata)
              End If
            End If
          Next
          'Adesso ribalta nel vettore originario
          nTotRate = nTotRatenew
          For nRata = 1 To nTotRate
            P.strDatsca(nRata) = PNew.strDatsca(nRata)
            P.dImpsca(nRata) = PNew.dImpsca(nRata)
            P.dImpscaval(nRata) = PNew.dImpscaval(nRata)
            nTiprata(nRata) = nTipratanew(nRata)
          Next
          nTipoPaga = 2 'Per convenzione (per routine sotto)
          GoTo nRateOK
        Else
          'Pagamento normale, come prima
          nTipoPaga = NTSCInt(!tb_tippaga)
          nTiprata(1) = nTipoPaga
          'Calcolo il numero delle Rate
          If (strTipoProv = "M") Or (strTipoProv = "I") Then
            If strTipoProv = "I" Then
              If nTipoPaga > 2 Then
                nTotRate = 1
                If strOrigine <> "X" Then GoTo nRateOK 'se sono simulazioni devo sempre calcolare le scadenze
              End If
            End If
            nTotRate = CType(oCleComm, CLELBMENU).CalcolaScad(strDittaCorrente, _
              P.nCodpaga, P.strDatrif, P.dTotfat, P.dTotfatval, P.dIva, P.dIvaval, _
              P.dSpese, P.dSpeseval, P.strDatsca, P.dImpsca, P.dImpscaval, nTiprata, _
              strErr, NTSCInt(!tm_valuta), lPv_conto)
            If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
            If strTipoProv = "I" And strOrigine = "X" Then
              dtrT!tm_datsca_1 = NTSCDate(P.strDatsca(1))
            End If
            If nTotRate < 1 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128728578726911205, "Attenzione!" & vbCrLf & _
                                                  "Documento '|" & !tm_tipork.ToString & "|' n° |" & _
                                                  !tm_numdoc.ToString & _
                                                  IIf(!tm_serie.ToString.Trim <> "", "/", "").ToString & _
                                                  !tm_serie.ToString.Trim & "| del |" & _
                                                  !tm_anno.ToString & "|" & _
                                                  "data |" & !tm_datdoc.ToString & "|" & vbCrLf & _
                                                  "NON elaborato." & vbCrLf & _
                                                  "Numero di rate inferiore a 1.")))
              Return False
            End If
          Else
            nTotRate = 1
            GoTo nRateOK
          End If
        End If

      End With    'With dtrT

nRateOK:
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

  Public Overridable Function Elabora_CalcolaQuote(ByRef dtrT As DataRow, ByVal nTotRate As Integer, _
                                                   ByRef dQuozRate() As Decimal, ByVal dTm_totiva As Decimal, _
                                                   ByRef P As CLELBMENU.ParamCalcScad) As Boolean
    'in dQuozRate ci sono le quote (%) che serviranno per calcolare le singole rate di provvigioni (una per ogni scadenza)
    Dim Cont As Integer
    Dim dResiduo1 As Decimal
    Dim dTotcoef As Decimal = 0
    Dim dIvaSplit As Decimal = 0
    Try
      'iva split payment: se la somma delle scadenze è diversa dal resido da pagare, la differenza è iva split payment
      dIvaSplit = NTSCDec(dtrT!tm_totdoc) - NTSCDec(dtrT!tm_totomag)
      For i As Integer = 1 To nTotRate
        dIvaSplit -= P.dImpsca(i)
      Next
      'in dIvaSplit c'è l'eventuale importo di IVA split payment

      dResiduo1 = dTm_totiva
      With dtrT
        For Cont = 1 To nTotRate
          If (NTSCDec(!tm_totdoc) - NTSCDec(!tm_totomag) - dIvaSplit) = 0 Then
            dQuozRate(Cont) = 0
          Else
            'Calcolo normale
            dQuozRate(Cont) = P.dImpsca(Cont) / (NTSCDec(!tm_totdoc) - dIvaSplit - NTSCDec(!tm_totomag))
            'Se però bConsIvaSuPagamenti allora fa un calcolo a parte (ricalcola)
            If (NTSCInt(!tb_prirata) > 0) And (bConsIvaSuPagamenti = True) Then
              If Cont = nTotRate Then
                dQuozRate(Cont) = (100 - dTotcoef)
              Else
                If dResiduo1 = 0 Then
                  dQuozRate(Cont) = P.dImpsca(Cont) / (NTSCDec(!tm_totdoc) - dIvaSplit - NTSCDec(!tm_totomag) - dTm_totiva)
                  dTotcoef = (dTotcoef + dQuozRate(Cont))
                Else 'C'è ancora Iva da asseganre
                  If dResiduo1 <= P.dImpsca(Cont) Then 'La rata è superiore o uguale all'importo dell'Iva
                    dQuozRate(Cont) = (P.dImpsca(Cont) - dResiduo1) / (NTSCDec(!tm_totdoc) - dIvaSplit - NTSCDec(!tm_totomag) - dTm_totiva)
                    dResiduo1 = 0
                    dTotcoef += dQuozRate(Cont)
                  Else 'La rata è inferiore all'Iva
                    dQuozRate(Cont) = 0
                    dResiduo1 -= P.dImpsca(Cont)
                  End If
                End If
              End If
            End If
          End If
        Next
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
  Public Overridable Function Elabora_CalcolaProvvOrdini(ByRef dtrT As DataRow) As Boolean
    'sono qui solo se elaboro gli ordini per 'simulazione'
    'rispetto a testmag, qui prendo le righe di movord e devo ricalcolare le provvigioni 
    'perchè una parte di ordine potrebbe già essere stata evasa, quindi devo prendere solo il residuo
    'inoltre devo calcolare le date di scadenza
    Dim dttTmp As New DataTable
    Try
      '--------------
      'con le due righe sotto imposto che la forma di pagamento è 'standard', per cui verrà chiamata la calcolascad
      dtrT!tm_ccambiati = 0
      dtrT!tb_flcondp = "N"

      '--------------
      'se non ho indicato codpaga sull'ordine, lo prendo dall'anagrafica.
      'se non c'è nelache li, scarto il record
      If NTSCInt(dtrT!tm_codpaga) = 0 Then
        'lo prendo da anagra
        oCldGnpv.ValCodiceDb(NTSCInt(dtrT!tm_conto).ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
        dtrT!tm_codpaga = NTSCInt(dttTmp.Rows(0)!an_codpag)
        dttTmp.Clear()
        If NTSCInt(dtrT!tm_codpaga) = 0 Then dtrT.Delete()
        oCldGnpv.ValCodiceDb(NTSCInt(dtrT!tm_codpaga).ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
        dtrT!tb_tippaga = NTSCInt(dttTmp.Rows(0)!tb_tippaga)
        dtrT!tb_prirata = NTSCInt(dttTmp.Rows(0)!tb_prirata)
        dttTmp.Clear()
      End If

      '--------------
      'devo calcolare il castelletto IVA
      If NTSCInt(dtrT!tm_codiva) <> 0 Then
        oCldGnpv.ValCodiceDb(NTSCInt(dtrT!tm_codiva).ToString, strDittaCorrente, "TABCIVA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          dtrT!tm_imposta_1 = ArrDbl(NTSCDec(dtrT!tm_valore) * NTSCDec(dttTmp.Rows(0)!tb_aliq) / 100, oCldGnpv.TrovaNdec(0))
          dtrT!tm_impostav_1 = ArrDbl(NTSCDec(dtrT!tm_valore) * NTSCDec(dttTmp.Rows(0)!tb_aliq) / 100, oCldGnpv.TrovaNdec(0))
        End If
        dttTmp.Clear()
      End If

      '--------------
      'devo correggere gli importi per considerare solo la parte non evasa
      If NTSCDec(dtrT!tm_valoremm) <> 0 Then
        dtrT!tm_vprovv = ArrDbl(NTSCDec(dtrT!tm_vprovv) / NTSCDec(dtrT!tm_valoremm) * NTSCDec(dtrT!tm_valore), oCldGnpv.TrovaNdec(0))
        dtrT!tm_vprovv2 = ArrDbl(NTSCDec(dtrT!tm_vprovv2) / NTSCDec(dtrT!tm_valoremm) * NTSCDec(dtrT!tm_valore), oCldGnpv.TrovaNdec(0))
      Else
        dtrT!tm_vprovv = 0
        dtrT!tm_vprovv2 = 0
      End If

      If NTSCDec(dtrT!tm_totmerce) <> 0 Then
        dtrT!tm_totomag = ArrDbl(NTSCDec(dtrT!tm_totomag) / NTSCDec(dtrT!tm_totmerce) * NTSCDec(dtrT!tm_valore), oCldGnpv.TrovaNdec(0))
        dtrT!tm_totomagv = ArrDbl(NTSCDec(dtrT!tm_totomagv) / NTSCDec(dtrT!tm_totmerce) * NTSCDec(dtrT!tm_valore), oCldGnpv.TrovaNdec(0))
      Else
        dtrT!tm_totomag = 0
        dtrT!tm_totomagv = 0
      End If

      dtrT!tm_totprov = NTSCDec(dtrT!tm_vprovv)
      dtrT!tm_totprov2 = NTSCDec(dtrT!tm_vprovv2)
      dtrT!tm_totdoc = NTSCDec(dtrT!tm_valore)
      dtrT!tm_totdocv = oCldGnpv.ConvImpEur(False, NTSCDec(dtrT!tm_totdoc), NTSCInt(dtrT!tm_valuta), NTSCDate(dtrT!tm_datdoc), NTSCDec(dtrT!tm_cambio))
      dtrT!tm_impprov = NTSCDec(dtrT!tm_valore)
      '--------------
      'correzioni varie
      dtrT!tm_datapag = dtrT!tm_datcons
      dtrT!tm_datdoc = dtrT!tm_datcons
      dtrT!tm_datsca_1 = dtrT!tm_datcons   'verrà utilizzato utilizzato solo per rimesse dirette

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
  Public Overridable Function Elabora_CalcolaRateIncassatoNew(ByRef dtrTm As DataRow, ByVal lPv_conto As Integer, _
                                                              ByVal dTotProv As Decimal, ByVal dImpProv As Decimal, _
                                                              ByRef nTotRate As Integer, ByRef dttSc As DataTable) As Boolean
    'nuovo sistema di elaborazione sull'incassato: le provvigioni corrispondolo alle righe di scaden
    'ovviamente il documento deve essere contabilizzato; se non lo è lo scrivo nel log e salto
    '
    'nTotRate = numero di record di provvig che dovranno essere scritti

    Dim dttTmp As New DataTable
    Dim dIvaSplit As Decimal = 0
    Dim bOk As Boolean = False
    Try
      '-------------------------
      'preparo il datatable
      dttSc.Clear()
      If dttSc.Columns.Count = 0 Then
        dttSc.Columns.Add("sc_numrata", GetType(Integer))
        dttSc.Columns.Add("sc_tippaga", GetType(Integer))
        dttSc.Columns.Add("sc_datscad", GetType(DateTime))
        dttSc.Columns.Add("sc_provv", GetType(Decimal))
        dttSc.Columns.Add("sc_impopv", GetType(Decimal))
        dttSc.Columns.Add("sc_segno", GetType(Integer))
        dttSc.Columns.Add("sc_importo", GetType(Decimal))
        dttSc.Columns.Add("xx_maturato", GetType(Decimal))
        dttSc.Columns.Add("xx_dtmaturaz", GetType(DateTime))
      End If
      dttSc.AcceptChanges()

      '-------------------------
      'cerco i record in scaden
      If Not oCldGnpv.GetScadenProvvig(strDittaCorrente, lPv_conto, NTSCInt(dtrTm!tm_anno), _
                                       NTSCStr(dtrTm!tm_serie), NTSCInt(dtrTm!tm_numdoc), dttTmp) Then
        Return False
      End If

      'iva split payment: se la somma delle scadenze è diversa dal resido da pagare, la differenza è iva split payment
      dIvaSplit = NTSCDec(dtrTm!tm_totdoc)
      For i As Integer = 0 To dttTmp.Rows.Count - 1
        If NTSCDate(dtrTm!tm_datregef) = NTSCDate(dttTmp.Rows(i)!sc_datreg) And NTSCInt(dtrTm!tm_numregef) = NTSCInt(dttTmp.Rows(i)!sc_numreg) Then
          bOk = True
          If NTSCDec(dttTmp.Rows(i)!sc_importo) < 0 And dIvaSplit > 0 Then
            'note accred
            dIvaSplit += NTSCDec(dttTmp.Rows(i)!sc_importo)
          Else
            'fatture
            dIvaSplit -= NTSCDec(dttTmp.Rows(i)!sc_importo)
          End If
        Else
          'in caosi di insoluti i record di scaden sono più alti del totale fattura!!!
          'prendo su sole lo scadenze con dta/unmero reg. = data/num di contabilizzazione 
        End If
      Next

      If bOk = False Then
        LogWrite(oApp.Tr(Me, 130746051502069927, "ATTENZIONE: per la fattura numero |" & _
                NTSCStr(dtrTm!tm_numdoc) & NTSCStr(dtrTm!tm_serie) & " - " & NTSCDate(dtrTm!tm_datdoc).ToShortDateString & _
                "| non sono state trovate scadenze con data/numero di registrazione uguali a quelle " & _
                "indicate sulla fattura. La fattura non verrà considerata in regime IVA Split Payment, " & _
                "anche se potrebbe esserlo"), True)
        dIvaSplit = 0
      End If
      'in dIvaSplit c'è l'eventuale importo di IVA split payment 

      If dttTmp.Rows.Count = 0 And NTSCDec(dtrTm!tm_pagato) = 0 Then
        'se è una nota di accred dove in pnfa è stato indicato di compensarla direttamente con la fattura 
        'in scaden non c'è nessuna scadenza per la nota di accredito. la carico al volo
        If NTSCStr(dtrTm!tm_tipork) = "N" And NTSCInt(dtrTm!tm_numpar) <> 0 Then
          dttSc.Rows.Add(dttSc.NewRow())
          With dttSc.Rows(dttSc.Rows.Count - 1)
            !sc_numrata = 1
            !sc_tippaga = 3
            !sc_datscad = NTSCDate(dtrTm!tm_datdoc)
            !sc_provv = dTotProv * -1
            !sc_impopv = dImpProv * -1
            !sc_segno = 1
            !sc_importo = NTSCDec(dtrTm!tm_totdoc)
            !xx_maturato = dTotProv
            !xx_dtmaturaz = NTSCDate(dtrTm!tm_datdoc)
          End With
          nTotRate = 1
          GoTo PASSA
        Else
          LogWrite(oApp.Tr(Me, 129483014095654297, "ATTENZIONE: per il documento tipo |" & dtrTm!tm_tipork.ToString & "| con CONTO |" & lPv_conto.ToString & _
                              "|, ANNO |" & dtrTm!tm_anno.ToString & "|, SERIE |'" & NTSCStr(dtrTm!tm_serie) & _
                              "'|, NUMERO |" & dtrTm!tm_numdoc.ToString & "| non sono state trovate scadenze. Probabilmente il documento non è stato contabilizzato. Non verranno generate le provvigioni."), True)
          Return False
        End If    'If NTSCDec(dtrTm!tm_pagato) <> 0 Then
      End If

      nTotRate = dttTmp.Rows.Count
      For Each dtrT As DataRow In dttTmp.Rows
        '-------------------------
        'calcolo la nuova quota provvigione ed alimento il datatable
        'Quando da testmag prelevo i record di scaden per generare provvig, 
        'prelevo sia le righe normali che quelle di insoluto. 
        'Quelle di insoluto originario (‘sc_insolu = ‘O’) avranno come segno ‘0’ 
        '(ovvero ne +1 ne -1) perché ai fini dell’elaborazione del maturato sarà la nuova rata insoluta 
        'che farà maturare/pagare la provvigione
        dttSc.Rows.Add(dttSc.NewRow())
        With dttSc.Rows(dttSc.Rows.Count - 1)
          !sc_numrata = NTSCInt(dtrT!sc_numrata)
          !sc_tippaga = NTSCInt(dtrT!sc_tippaga)
          !sc_datscad = NTSCDate(dtrT!sc_datsca)
          If NTSCInt(dtrT!sc_codpaga) = 0 And NTSCStr(dtrT!sc_flsaldato) = "S" And NTSCInt(dtrT!tm_numregom) <> 0 Then
            'sono omaggi di testmag, contabilizzati da pnfa con cod. pagamento = 0
            'attenzione: anche l'incassato e l'abbuono da pnfa hanno cod. pagamento = 0
            'anche cginso crea delle scadenze con codpaga = 0, ma in sc_numratarif riporta il numero di rata della scad. originaria!!!
            'SONO OMAGGI QUELLI CON DATA REGISTRAZ. SALDO CG = DATA REG. OMAGGI SU TESTMAG
            !sc_provv = 0
            !sc_impopv = 0
          Else
            !sc_provv = ArrDbl(dTotProv / (NTSCDec(dtrTm!tm_totdoc) - dIvaSplit - NTSCDec(dtrTm!tm_totomag)) * NTSCDec(dtrT!sc_importo), oCldGnpv.TrovaNdec(0))
            !sc_impopv = ArrDbl(dImpProv / (NTSCDec(dtrTm!tm_totdoc) - dIvaSplit - NTSCDec(dtrTm!tm_totomag)) * NTSCDec(dtrT!sc_importo), oCldGnpv.TrovaNdec(0))
          End If
          !sc_segno = NTSCInt(IIf(NTSCStr(dtrT!sc_insolu) = "O", 0, 1))
          !sc_importo = NTSCDec(dtrT!sc_importo)
          !xx_maturato = 0
          !xx_dtmaturaz = DBNull.Value
        End With    'With dttSc.Rows(dttSc.Rows.Count - 1)
      Next    'For Each dtrT As DataRow In dttSc.Rows

PASSA:

      dttSc.AcceptChanges()

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



  Public Overridable Function NomeTipoRk(ByVal strTipoRk As String) As String
    Dim strOut As String = ""
    Try
      Select Case strTipoRk
        Case "A" : strOut = "'Fattura Imm. emessa'"
        Case "B" : strOut = "'D.D.T. emesso'"
        Case "C" : strOut = "'Corrispettivo emesso'"
        Case "D" : strOut = "'Fattura Diff. emessa'"
        Case "E" : strOut = "'Nota di Addebito emessa'"
        Case "F" : strOut = "'Ric.Fiscala Emessa'"
        Case "I" : strOut = "'Riemissiona Ric.Fiscale"
        Case "J" : strOut = "'Nota Accr. ricevuta'"
        Case "K" : strOut = "'Fattura Diff. ricevuta'"
        Case "L" : strOut = "'Fattura Imm. ricevuta'"
        Case "M" : strOut = "'D.D.T.ricevuto'"
        Case "N" : strOut = "'Nota Accr. emessa'"
        Case "£" : strOut = "'Nota Accr. Diff. emessa'"
        Case "P" : strOut = "'Fatt.Ric.Fisc.Differita'"
        Case "S" : strOut = "'Fatt.Ric.Fisc.Emessa'"
        Case "T" : strOut = "'Carico da produz.'"
        Case "U" : strOut = "'Scarico a produz.'"
        Case "Z" : strOut = "'Bolla di mov. interna'"
      End Select

      Return strOut
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return ""
    End Try
  End Function


  Public Overridable Function ElaboraIncassatoTotale(ByVal nAgente As Integer, ByVal dTotProv As Decimal, _
                                                     ByVal dImpProv As Decimal, ByVal strTipotass As String) As Boolean
    Try
      'non più utilizzato. mantenuto solo per compatibilità con versioni precedenti
      Return ElaboraIncassatoTotale(nAgente, dTotProv, dImpProv, strTipotass, "M")

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
  Public Overridable Function ElaboraIncassatoTotale(ByVal nAgente As Integer, ByVal dTotProv As Decimal, _
                                                     ByVal dImpProv As Decimal, ByVal strTipotass As String, _
                                                     ByVal strOrigine As String) As Boolean
    'attenzione: non esegue MAI la maturazione e l'incasso: è solo un modo diverso per generare il record di provvig!!!
    'elaborazione su modalità di corresponsione 'incassato totale': se la provvigione esiste già in provvig 
    'e risuta essere stata pagata, fa solo una update NON sugli importi!!!

    Dim i As Integer = 0
    Dim lPv_conto As Integer = 0
    Dim dtrT1() As DataRow = Nothing

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nAgente, dTotProv, dImpProv, strTipotass, strOrigine})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '-----------------------
      With dttTestmag.Rows(nIndiceTestmag)
        nPv_annpart = NTSCInt(!tm_anno)
        strPv_alfpart = !tm_serie.ToString
        lPv_numpart = NTSCInt(!tm_numdoc)
        If (bUsaSempreNumdoc = False) And (NTSCInt(!tm_numpar) <> 0) Then
          nPv_annpart = NTSCInt(!tm_annpar)
          strPv_alfpart = !tm_alfpar.ToString
          lPv_numpart = NTSCInt(!tm_numpar)
        End If
        dPv_totfatt = NTSCDec(!tm_totdoc) : dPv_vtotfatt = NTSCDec(!tm_totdocv)
        dPv_totomag = NTSCDec(!tm_totomag) : dPv_totomagv = NTSCDec(!tm_totomagv)
        dPv_provv = dTotProv : dPv_impopv = dImpProv
        If !tm_tipork.ToString = "N" Or !tm_tipork.ToString = "£" Or !tm_tipork.ToString = "M" Then
          dPv_totfatt = (dPv_totfatt * -1) : dPv_vtotfatt = (dPv_vtotfatt * -1)
          dPv_totomag = (dPv_totomag * -1) : dPv_totomagv = (dPv_totomagv * -1)
          dPv_provv = (dPv_provv * -1) : dPv_impopv = (dPv_impopv * -1)
        End If
        'in dtPv_datscad memorizzo la data di scadenza più lontana
        If NTSCStr(!tm_datsca_1.ToString).Trim <> "" Then dtPv_datscad = NTSCDate(!tm_datsca_1.ToString)
        If NTSCStr(!tm_datsca_2.ToString).Trim <> "" Then dtPv_datscad = NTSCDate(!tm_datsca_2.ToString)
        If NTSCStr(!tm_datsca_3.ToString).Trim <> "" Then dtPv_datscad = NTSCDate(!tm_datsca_3.ToString)
        If NTSCStr(!tm_datsca_4.ToString).Trim <> "" Then dtPv_datscad = NTSCDate(!tm_datsca_4.ToString)
        If NTSCStr(!tm_datsca_5.ToString).Trim <> "" Then dtPv_datscad = NTSCDate(!tm_datsca_5.ToString)
        If NTSCStr(!tm_datsca_6.ToString).Trim <> "" Then dtPv_datscad = NTSCDate(!tm_datsca_6.ToString)
        If NTSCStr(!tm_datsca_7.ToString).Trim <> "" Then dtPv_datscad = NTSCDate(!tm_datsca_7.ToString)
        If NTSCStr(!tm_datsca_8.ToString).Trim <> "" Then dtPv_datscad = NTSCDate(!tm_datsca_8.ToString)
        '-----------------------
        dPv_vimpopv = 0
        dPv_vprovv = 0
        'dPv_vimpvmat = 0
        'dPv_vimpvpag = 0
        If NTSCInt(!tm_valuta) > 0 Then
          dPv_vimpopv = oCldGnpv.ConvImpEur(False, dPv_impopv, NTSCInt(!tm_valuta), NTSCDate(!tm_datdoc), NTSCDec(!tm_cambio))
          dPv_vprovv = oCldGnpv.ConvImpEur(False, dPv_provv, NTSCInt(!tm_valuta), NTSCDate(!tm_datdoc), NTSCDec(!tm_cambio))
          'dPv_vimpvpag = oCldGnpv.ConvImpEur(False, dPv_impvpag, NTSCInt(!tm_valuta), NTSCDate(!tm_datdoc), NTSCDec(!tm_cambio))
        End If
        '-----------------------
        If bIncassoTotaleUsaDatDocSuDatScad = True Then dtPv_datscad = NTSCDate(!tm_datdoc)
        '-----------------------
        strPv_flag = "N"
        '-----------------------
        If strOrigine = "X" Then
          'nelle previsioni non è possibile che la provvigione esista, visto che prima di elaborare devo cancellare l'elaborazione precedente
          bEsisteProvvig = False
        Else
          oCldGnpv.ApriProvvig(strDittaCorrente, nAgente, NTSCStr(!tm_tipork), _
                               NTSCInt(!tm_anno), NTSCStr(!tm_serie), NTSCInt(!tm_numdoc), 1, "M", _
                               bEsisteProvvig, dPv_impvpag)
          If bEsisteProvvig = False Then
            dtrT1 = dttProvvigUPDATE.Select("pv_codage = " & nAgente & _
                                  " AND pv_tipdoc = " & CStrSQL(NTSCStr(!tm_tipork)) & _
                                  " AND pv_anno = " & NTSCInt(!tm_anno) & _
                                  " AND pv_serie = " & CStrSQL(NTSCStr(!tm_serie)) & _
                                  " AND pv_numdoc = " & NTSCInt(!tm_numdoc) & _
                                  " AND pv_numrat = " & 1 & _
                                  " AND pv_origine = 'M'")
            If dtrT1.Length > 0 Then
              bEsisteProvvig = True
              dPv_impvpag = NTSCDec(dtrT1(0)!pv_impvpag)
            End If
          End If
          If bEsisteProvvig = False Then
            dtrT1 = dttProvvigINSERT.Select("pv_codage = " & nAgente & _
                                            " AND pv_tipdoc = " & CStrSQL(NTSCStr(!tm_tipork)) & _
                                            " AND pv_anno = " & NTSCInt(!tm_anno) & _
                                            " AND pv_serie = " & CStrSQL(NTSCStr(!tm_serie)) & _
                                            " AND pv_numdoc = " & NTSCInt(!tm_numdoc) & _
                                            " AND pv_numrat = " & 1 & _
                                            " AND pv_origine = 'M'")
            If dtrT1.Length > 0 Then
              bEsisteProvvig = True
              dPv_impvpag = NTSCDec(dtrT1(0)!pv_impvpag)
            End If
          End If
        End If

        If bEsisteProvvig = True Then
          'la provvigione esiste già: aggiorno solo l'importo della provvigione e della fattura. ignoro il fatto che sia stata maturata e pagata
          If dPv_impvpag = dPv_provv Then strPv_flag = "S"
          Dim dtrT() As DataRow = Nothing
          dtrT = dttProvvigUPDATE.Select("codditt = " & CStrSQL(strDittaCorrente) & _
                                        " AND pv_codage = " & nAgente & _
                                        " AND pv_tipdoc = " & CStrSQL(!tm_tipork) & _
                                        " AND pv_anno = " & NTSCInt(!tm_anno) & _
                                        " AND pv_serie = " & CStrSQL(!tm_serie) & _
                                        " AND pv_numdoc = " & NTSCInt(!tm_numdoc) & _
                                        " AND pv_numrat = 1" & _
                                        " AND pv_origine = 'M'")
          If dtrT.Length <> 0 Then
            For i = 0 To dtrT.Length - 1
              dtrT(i).Delete()
            Next
          End If
          dttProvvigUPDATE.Rows.Add(dttProvvigUPDATE.NewRow)
          With dttProvvigUPDATE.Rows(dttProvvigUPDATE.Rows.Count - 1)
            !codditt = NTSCStr(strDittaCorrente)
            !pv_codage = nAgente
            !pv_tipdoc = NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_tipork)
            !pv_anno = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_anno)
            !pv_serie = NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_serie)
            !pv_numdoc = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_numdoc)
            !pv_numrat = 1
            !pv_origine = strOrigine
            !pv_datscad = NTSCDate(dtPv_datscad)
            !pv_totfatt = NTSCDec(dPv_totfatt)
            !pv_vtotfatt = NTSCDec(dPv_vtotfatt)
            !pv_totomag = NTSCDec(dPv_totomag)
            !pv_segno = 1
            !pv_impopv = NTSCDec(dPv_impopv)
            !pv_provv = NTSCDec(dPv_provv)
            If bProvvig2 And dPv_provv < 0 Then
              !pv_segno = -1
              !pv_totfatt = dPv_totfatt * -1
              !pv_vtotfatt = dPv_vtotfatt * -1
              !pv_totomag = dPv_totomag * -1
              !pv_impopv = dPv_impopv * -1
              !pv_provv = dPv_provv * -1
            End If
            !pv_flag = NTSCStr(strPv_flag)
          End With
          dttProvvigUPDATE.AcceptChanges()
        Else
          'la provvigione non esiste in provvig: la inserisco
          dttProvvigINSERT.Rows.Add(dttProvvigINSERT.NewRow)
          With dttProvvigINSERT.Rows(dttProvvigINSERT.Rows.Count - 1)
            !codditt = NTSCStr(strDittaCorrente)
            !pv_codage = nAgente
            !pv_tipdoc = NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_tipork)
            !pv_anno = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_anno)
            !pv_serie = NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_serie)
            !pv_numdoc = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_numdoc)
            !pv_numrat = 1
            !pv_datdoc = NTSCDate(dttTestmag.Rows(nIndiceTestmag)!tm_datdoc)
            !pv_datscad = NTSCDate(dtPv_datscad)
            '---------------------------------------------
            lPv_conto = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_conto)
            Select Case strUsaContoFattDoc
              Case "1"
                Select Case NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_tipork)
                  Case "A", "C", "D", "E", "J", "K", "L", "N", "£", "M"
                    If NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_contfatt) <> 0 Then lPv_conto = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_contfatt)
                End Select
              Case "2"
                Select Case NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_tipork)
                  Case "A", "C", "E", "J", "L", "N", "£", "M"
                    If NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_contfatt) <> 0 Then lPv_conto = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_contfatt)
                End Select
            End Select
            !pv_conto = lPv_conto
            '---------------------------------------------
            !pv_annpart = nPv_annpart
            !pv_alfpart = strPv_alfpart
            !pv_numpart = lPv_numpart
            !pv_totfatt = dPv_totfatt
            !pv_totomag = dPv_totomag
            !pv_totomagv = dPv_totomagv

            !pv_segno = 1
            !pv_impopv = dPv_impopv
            !pv_provv = dPv_provv
            !pv_vimpopv = dPv_vimpopv
            !pv_vprovv = dPv_vprovv
            !pv_impvmat = 0
            !pv_vimpvmat = 0 'NTSCDec(dPv_vimpvmat)
            !pv_impvpag = dPv_impvpag
            !pv_vimpvpag = 0 'NTSCDec(dPv_vimpvpag)

            If bProvvig2 And dPv_provv < 0 Then
              !pv_segno = -1
              !pv_totfatt = dPv_totfatt * -1
              !pv_totomag = dPv_totomag * -1
              !pv_totomagv = dPv_totomagv * -1
              !pv_impopv = dPv_impopv * -1
              !pv_provv = dPv_provv * -1
              !pv_vimpopv = dPv_vimpopv * -1
              !pv_vprovv = dPv_vprovv * -1
              !pv_impvmat = 0
              !pv_vimpvmat = 0 'NTSCDec(dPv_vimpvmat) * -1
              !pv_impvpag = dPv_impvpag * -1
              !pv_vimpvpag = 0 'NTSCDec(dPv_vimpvpag) * -1
            End If

            !pv_codpaga = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_codpaga)
            !pv_tippaga = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tb_tippaga)
            !pv_flag = NTSCStr(strPv_flag)
            !pv_origine = strOrigine
            !pv_tipotass = NTSCStr(strTipotass)
            !pv_vtotfatt = NTSCDec(dPv_vtotfatt)
            !pv_cambio = NTSCDec(dttTestmag.Rows(nIndiceTestmag)!tm_cambio.ToString)
            !pv_codvalu = NTSCInt(dttTestmag.Rows(nIndiceTestmag)!tm_valuta)
          End With
          dttProvvigINSERT.AcceptChanges()

        End If    'If bEsisteProvvig = True Then

      End With    'With dttTestmag.Rows(nIndiceTestmag)

      Return True

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ProvvigioniPagate(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                              ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                              ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                              ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                              ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                              ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                              ByVal bTipoDocumentoB As Boolean, _
                                              ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                              ByVal nTipoBF As Integer, _
                                              ByRef dttProvvig As DataTable) As Boolean
    Try
      'non più usata. mantenuta per compatibilità con versioni precedenti
      Return ProvvigioniPagate(strDatdocini, strDatdocfin, nAgenteini, nAgentefin, bTipoDocumentoA, bTipoDocumentoD, _
                               bTipoDocumentoE, bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, _
                               bTipoDocumentoP, bTipoDocumentoB, False, bRielabora, bCancellaProvvigioni, nTipoBF, _
                               dttProvvig)
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
  Public Overridable Function ProvvigioniPagate(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                                ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                                ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                                ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                                ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                                ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                                ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                                ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                                ByVal nTipoBF As Integer, _
                                                ByRef dttProvvig As DataTable) As Boolean
    '-------------------------
    Dim bOk As Boolean = False

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDatdocini, strDatdocfin, _
                                             nAgenteini, nAgentefin, _
                                             bTipoDocumentoA, bTipoDocumentoD, _
                                             bTipoDocumentoE, bTipoDocumentoN, _
                                             bTipoDocumentoC, bTipoDocumentoF, _
                                             bTipoDocumentoS, bTipoDocumentoP, _
                                             bTipoDocumentoB, bTipoDocumentoN1, _
                                             bRielabora, bCancellaProvvigioni, _
                                             nTipoBF, _
                                             dttProvvig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttProvvig = CType(oIn(17), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      '-----------------------
      bOk = oCldGnpv.ProvvigioniPagate(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin, _
                                      bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, _
                                      bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, _
                                      bTipoDocumentoS, bTipoDocumentoP, bTipoDocumentoB, _
                                      bTipoDocumentoN1, bRielabora, bCancellaProvvigioni, _
                                      nTipoBF, dttProvvig)
      '-----------------------
      Return True
      '-----------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Sub PulisciDataTables()
    Try
      '-----------------------
      If Not dttProvvigINSERT Is Nothing Then
        dttProvvigINSERT.Clear()
        dttProvvigINSERT.AcceptChanges()
      End If
      '-----------------------
      If Not dttProvvigUPDATE Is Nothing Then
        dttProvvigUPDATE.Clear()
        dttProvvigUPDATE.AcceptChanges()
      End If
      '-----------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Function ScriviActLog(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                        ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                        ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                        ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                        ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                        ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                        ByVal bTipoDocumentoB As Boolean, _
                                        ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                        ByVal nTipoBF As Integer, ByVal strDesTipoBF As String) As Boolean
    Try
      'non più usata. mantenuta solo per compatibilità con versioni precedenti
      Return ScriviActLog(strDatdocini, strDatdocfin, nAgenteini, nAgentefin, bTipoDocumentoA, bTipoDocumentoD, _
                         bTipoDocumentoE, bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, _
                         bTipoDocumentoP, bTipoDocumentoB, False, bRielabora, bCancellaProvvigioni, nTipoBF, _
                         strDesTipoBF)
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
  Public Overridable Function ScriviActLog(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                          ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                          ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                          ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                          ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                          ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                          ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                          ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                          ByVal nTipoBF As Integer, ByVal strDesTipoBF As String) As Boolean
    '-------------------------
    Dim strDesogglog As String = ""

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDatdocini, strDatdocfin, _
                                             nAgenteini, nAgentefin, _
                                             bTipoDocumentoA, bTipoDocumentoD, _
                                             bTipoDocumentoE, bTipoDocumentoN, _
                                             bTipoDocumentoC, bTipoDocumentoF, _
                                             bTipoDocumentoS, bTipoDocumentoP, _
                                             bTipoDocumentoB, bTipoDocumentoN1, _
                                             bRielabora, bCancellaProvvigioni, _
                                             nTipoBF, strDesTipoBF})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '-----------------------
      strDesogglog = "Generazione delle provvigioni" & vbCrLf & vbCrLf & _
        " - Documenti elaborati...............: " & vbCrLf
      '-----------------------
      Select Case NTSCStr(dttTestmag.Rows(nIndiceTestmag)!tm_tipork)
        Case "A" : strDesogglog = strDesogglog & "     . 'Fattura Immediata Emessa'"
        Case "B" : strDesogglog = strDesogglog & "     . 'D.D.T Emesso'"
        Case "C" : strDesogglog = strDesogglog & "     . 'Corrispettivo Emesso'"
        Case "D" : strDesogglog = strDesogglog & "     . 'Fattura Differita Emessa'"
        Case "E" : strDesogglog = strDesogglog & "     . 'Nota di Addebito Emessa'"
        Case "F" : strDesogglog = strDesogglog & "     . 'Ricevuta Fiscale Emessa'"
        Case "I" : strDesogglog = strDesogglog & "     . 'Riemissione Ricevuta Fiscale'"
        Case "J" : strDesogglog = strDesogglog & "     . 'Nota di Accredito Ricevuta'"
        Case "K" : strDesogglog = strDesogglog & "     . 'Fattura Differita Ricevuta'"
        Case "L" : strDesogglog = strDesogglog & "     . 'Fattura Immediata Ricevuta'"
        Case "M" : strDesogglog = strDesogglog & "     . ''D.D.T. Ricevuto"
        Case "N" : strDesogglog = strDesogglog & "     . 'Nota di Accredito Emessa'"
        Case "£" : strDesogglog = strDesogglog & "     . 'Nota di Accredito Differita Emessa'"
        Case "P" : strDesogglog = strDesogglog & "     . 'Fattura/Ricevuta Fiscale Differita'"
        Case "S" : strDesogglog = strDesogglog & "     . 'Fattura/Ricevuta Fiscale Emessa'"
        Case "T" : strDesogglog = strDesogglog & "     . 'Carico da Droduzione'"
        Case "U" : strDesogglog = strDesogglog & "     . 'Scarico a Produzione'"
        Case "W" : strDesogglog = strDesogglog & "     . 'Nota di Prelievo'"
        Case "Z" : strDesogglog = strDesogglog & "     . 'Bolla di Movimentazione Interna'"
      End Select
      '-----------------------
      With dttTestmag.Rows(nIndiceTestmag)
        strDesogglog = strDesogglog & " n° " & !tm_numdoc.ToString & _
          IIf((!tm_serie.ToString.Trim) <> "", "/" & !tm_serie.ToString, "").ToString & _
          " del " & !tm_anno.ToString & vbCrLf
      End With
      '-----------------------
      strDesogglog = strDesogglog & vbCrLf & _
        " - Selezionando......................: " & vbCrLf & _
        " . Da data documento.................: '" & strDatdocini & "'" & vbCrLf & _
        " . A data documento..................: '" & strDatdocfin & "'" & vbCrLf & _
        " . Rielabora documenti già elaborati.: " & IIf(bRielabora = True, "'Sì'", "'No'").ToString & vbCrLf & _
        " . Tipo bolla/fattura (0 = tutte)....: '" & nTipoBF.ToString & "' " & strDesTipoBF & vbCrLf & _
        " . Da codice agente .................: " & nAgenteini.ToString & vbCrLf & _
        " . A codice agente ..................: " & nAgentefin.ToString & vbCrLf & _
        " . Fatture immediate.................: " & IIf(bTipoDocumentoA = True, "'Sì'", "'No'").ToString & vbCrLf & _
        " . Fatture differite.................: " & IIf(bTipoDocumentoD = True, "'Sì'", "'No'").ToString & vbCrLf & _
        " . Note di addebito..................: " & IIf(bTipoDocumentoE = True, "'Sì'", "'No'").ToString & vbCrLf & _
        " . Note di accredito.................: " & IIf(bTipoDocumentoN = True, "'Sì'", "'No'").ToString & vbCrLf & _
        " . Note di accredito differite.......: " & IIf(bTipoDocumentoN1 = True, "'Sì'", "'No'").ToString & vbCrLf & _
        " . Corrispettivi.....................: " & IIf(bTipoDocumentoC = True, "'Sì'", "'No'").ToString & vbCrLf & _
        " . Ricevute fiscali..................: " & IIf(bTipoDocumentoF = True, "'Sì'", "'No'").ToString & vbCrLf & _
        " . Fatture/ricevute fiscali immediate: " & IIf(bTipoDocumentoS = True, "'Sì'", "'No'").ToString & vbCrLf & _
        " . Fatture/ricevute fiscali differite: " & IIf(bTipoDocumentoP = True, "'Sì'", "'No'").ToString & vbCrLf & _
        " . D.D.T. emessi.....................: " & IIf(bTipoDocumentoB = True, "'Sì'", "'No'").ToString & vbCrLf
      '-----------------------
      oCldGnpv.ScriviActLog(strDittaCorrente, "BSPRGNPV", "BSPRGNPV", "", "", "M", "E", strDesogglog, False)
      '-----------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strDesogglog, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function TestPreElabora(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                            ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                            ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                            ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                            ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                            ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                            ByVal bTipoDocumentoB As Boolean, _
                                            ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                            ByVal nTipoBF As Integer) As Boolean
    Try
      'non più usata. mantenuta per compatibilità con versioni precedenti
      Return TestPreElabora(strDatdocini, strDatdocfin, nAgenteini, nAgentefin, bTipoDocumentoA, bTipoDocumentoD, _
                           bTipoDocumentoE, bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, bTipoDocumentoS, _
                           bTipoDocumentoP, bTipoDocumentoB, False, bRielabora, bCancellaProvvigioni, nTipoBF)
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
  Public Overridable Function TestPreElabora(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                            ByVal nAgenteini As Integer, ByVal nAgentefin As Integer, _
                                            ByVal bTipoDocumentoA As Boolean, ByVal bTipoDocumentoD As Boolean, _
                                            ByVal bTipoDocumentoE As Boolean, ByVal bTipoDocumentoN As Boolean, _
                                            ByVal bTipoDocumentoC As Boolean, ByVal bTipoDocumentoF As Boolean, _
                                            ByVal bTipoDocumentoS As Boolean, ByVal bTipoDocumentoP As Boolean, _
                                            ByVal bTipoDocumentoB As Boolean, ByVal bTipoDocumentoN1 As Boolean, _
                                            ByVal bRielabora As Boolean, ByVal bCancellaProvvigioni As Boolean, _
                                            ByVal nTipoBF As Integer) As Boolean
    '-------------------------
    Dim bOk As Boolean = False
    Dim strMsg As String = ""
    Dim dttTestmag As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDatdocini, strDatdocfin, _
                                             nAgenteini, nAgentefin, _
                                             bTipoDocumentoA, bTipoDocumentoD, _
                                             bTipoDocumentoE, bTipoDocumentoN, _
                                             bTipoDocumentoC, bTipoDocumentoF, _
                                             bTipoDocumentoS, bTipoDocumentoP, _
                                             bTipoDocumentoB, bTipoDocumentoN1, _
                                             bRielabora, bCancellaProvvigioni, _
                                             nTipoBF})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '-----------------------
      If Not oCldGnpv.EsistonoDocumenti(strDittaCorrente, _
                                        strDatdocini, strDatdocfin, _
                                        nAgenteini, nAgentefin, _
                                        bTipoDocumentoA, bTipoDocumentoD, bTipoDocumentoE, _
                                        bTipoDocumentoN, bTipoDocumentoC, bTipoDocumentoF, _
                                        bTipoDocumentoS, bTipoDocumentoP, bTipoDocumentoB, _
                                        bTipoDocumentoN1, nTipoBF, dttTestmag, bProvvig2) Then
        strMsg = "Attenzione!" & vbCrLf & _
          "Non esistono documenti da trattare con le caratteristiche richieste." & vbCrLf & _
          "Elaborazione annullata."
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128727026735984124, strMsg)))
        Return False
      End If

      dtrT = dttTestmag.Select("tm_flagiva_1 = 'S'")
      If dtrT.Length > 0 Then
        For i = 0 To dtrT.Length - 1
          strMsg += vbCrLf & dtrT(i)!tm_tipork.ToString & " - " & _
                    dtrT(i)!tm_anno.ToString & " - '" & _
                    dtrT(i)!tm_serie.ToString & "' - " & _
                    dtrT(i)!tm_numdoc.ToString
          If i > 20 Then Exit For
        Next
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129302245721210938, "ATTENZIONE: sono presenti uno o più documenti riepilogativi" & vbCrLf & _
                        "(Fatture differite emesse, Fatture differite ricevute, Note accred. diff. emesse, Note accred. diff. ricevute, Fatt. ric. fisc. differita)" & vbCrLf & _
                        "che devono essere rielaborati perchè contenenti documenti modificati dopo la creazione della fattura/nota accred." & vbCrLf & _
                        "Primi documenti da rielaborare:") & strMsg))
        Return False
      End If

      '-----------------------
      Return True
      '-----------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTestmag.Clear()
    End Try
  End Function

#Region "Simulazioni"
  Public Overridable Function SimCheck(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                       ByVal nAgenteini As Integer, ByVal nAgentefin As Integer) As Boolean
    'ritorna true se nel periodo da elaborare sono presenti delle righe di simulazione
    Try
      Return oCldGnpv.SimCheck(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin)

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function SimDelete(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                        ByVal nAgenteini As Integer, ByVal nAgentefin As Integer) As Boolean
    'cancello la simulazione
    Try
      Return oCldGnpv.SimDelete(strDittaCorrente, strDatdocini, strDatdocfin, nAgenteini, nAgentefin)

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function SimCreate(ByVal strDatdocini As String, ByVal strDatdocfin As String, _
                                        ByVal nAgenteini As Integer, ByVal nAgentefin As Integer) As Boolean
    'creo i record di simulazione
    'in base all'opzione di registro determino se elaborare anche i ddt o solo gli ordini (imp. cli)
    Try
      oCldGnpv.ApriStrutturaProvvig(dttProvvigINSERT, dttProvvigUPDATE)

      '--------------------------------
      'elaboro e scrivo gli impegni clienti
      Elabora(strDatdocini, strDatdocfin, nAgenteini, nAgentefin, False, False, _
              False, False, False, False, False, False, True, False, True, False, 0, "", 0, "X", True)

      '--------------------------------
      'se posso elaboro anche i ddt emessi da cli
      'come documenti previsionali posso prendere DDT emessi, ddt ricevuti per resi da cli
      If DDTEmessiNoSimulazione = False Then
        Elabora(strDatdocini, strDatdocfin, nAgenteini, nAgentefin, False, False, _
                False, False, False, False, False, False, True, False, True, False, 0, "", 0, "X", False)
      End If    'If DDTEmessiNoSimulazione = False Then

      Return True

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttProvvigINSERT.Clear()
      dttProvvigINSERT.AcceptChanges()
    End Try
  End Function
#End Region

End Class