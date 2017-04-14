Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEORSCHO
  Inherits CLE__BASN

  Public oCldScho As CLDORSCHO
  Public dttTabanaz As New DataTable
  Public lIITTScho As Integer = 0
  Public bModTCO As Boolean = False
  Public bModAS As Boolean = False

  Public dDisp() As Decimal

  'per accessi CRM
  Public bModuloCRM As Boolean = False
  Public bIsCRMUser As Boolean = False
  Public bAmm As Boolean = False
  Public strAccvis As String = ""
  Public strAccmod As String = ""
  Public strRegvis As String = ""
  Public strRegmod As String = ""
  Public lCodorgaOperat As Integer = 0
  Public nCodcageoperat As Integer
  Public bStampaWordRaggruppata As Boolean

  Public strAltriFiltri As String

  'opzioni
  Public nMagazInternoTransito As Integer
  Public nAltezzaGif As Integer

  '--- Flag per sapere se il programma è chiamato dall'estarno
  Public bSchoDagest As Boolean
  'variabili passate alla finestra modale
  Public nSchoDamagaz As Integer
  Public nSchoAmagaz As Integer
  Public strSchoDadatord As String
  Public strSchoAdatord As String
  Public strSchoDadatcons As String
  Public strSchoAdatcons As String
  Public lSchoDaconto As Integer
  Public lSchoAconto As Integer
  Public strSchoDacodart As String
  Public strSchoAcodart As String
  Public lSchoCommecaini As Integer
  Public lSchoCommecafin As Integer
  Public nSchoGruppo As Integer
  Public nSchoSottogr As Integer
  Public strSchoClassLivello1 As String = ""
  Public strSchoClassLivello2 As String = ""
  Public strSchoClassLivello3 As String = ""
  Public strSchoClassLivello4 As String = ""
  Public strSchoClassLivello5 As String = ""
  Public strSchoSerie As String
  Public strSchoTipork As String
  Public strSchoOrdin As String
  Public bSchoEvasi As Boolean
  Public nSchoDaagente As Integer
  Public nSchoAagente As Integer
  Public strSchoRilasciato As String
  Public bSchoFiltri As Boolean
  Public strSchoDatordini As String
  Public strSchoDatordfin As String
  Public nSchoFaseini As Integer
  Public nSchoFasefin As Integer
  Public strSchoConto As String
  Public nSchoCodlsel As Integer
  Public strSchoCodart As String
  Public nSchoCodlsar As Integer

  Public strSch1Dadatcons As String
  Public strSch1Adatcons As String
  Public bSch1AggiornaOrdine As Boolean
  Public strSch1Codart As String
  Public nSch1Magaz As Integer
  Public bGrs1ModTCO As Boolean
  Public bGrs1Accorpa As Boolean
  Public strGrsoCodcfam As String
  Public bGrsoModTCO As Boolean
  Public nGrsoAnnotco As Integer
  Public nGrsoCodstag As Integer
  Public strGrs2Codcfam As String
  Public bGrs2ModTCO As Boolean
  Public nGrs2Annotco As Integer
  Public nGrs2Codstag As Integer

  'orsch1
  Public bApertaGestione As Boolean
  Public nSch1Fase As Integer

  'orgrs1
  Public bArticoloTC As Boolean
  Public dEsistTC() As Decimal

  Public dDispTC1() As Decimal
  Public dDispTC2() As Decimal
  Public dDispTC3() As Decimal
  Public dDispTC4() As Decimal
  Public dDispTC5() As Decimal
  Public dDispTC6() As Decimal
  Public dDispTC7() As Decimal
  Public dDispTC8() As Decimal
  Public dDispTC9() As Decimal
  Public dDispTC10() As Decimal
  Public dDispTC11() As Decimal
  Public dDispTC12() As Decimal
  Public dDispTC13() As Decimal
  Public dDispTC14() As Decimal
  Public dDispTC15() As Decimal
  Public dDispTC16() As Decimal
  Public dDispTC17() As Decimal
  Public dDispTC18() As Decimal
  Public dDispTC19() As Decimal
  Public dDispTC20() As Decimal
  Public dDispTC21() As Decimal
  Public dDispTC22() As Decimal
  Public dDispTC23() As Decimal
  Public dDispTC24() As Decimal

  Public dEsistTC1() As Decimal
  Public dEsistTC2() As Decimal
  Public dEsistTC3() As Decimal
  Public dEsistTC4() As Decimal
  Public dEsistTC5() As Decimal
  Public dEsistTC6() As Decimal
  Public dEsistTC7() As Decimal
  Public dEsistTC8() As Decimal
  Public dEsistTC9() As Decimal
  Public dEsistTC10() As Decimal
  Public dEsistTC11() As Decimal
  Public dEsistTC12() As Decimal
  Public dEsistTC13() As Decimal
  Public dEsistTC14() As Decimal
  Public dEsistTC15() As Decimal
  Public dEsistTC16() As Decimal
  Public dEsistTC17() As Decimal
  Public dEsistTC18() As Decimal
  Public dEsistTC19() As Decimal
  Public dEsistTC20() As Decimal
  Public dEsistTC21() As Decimal
  Public dEsistTC22() As Decimal
  Public dEsistTC23() As Decimal
  Public dEsistTC24() As Decimal

  Private Moduli_P As Integer = bsModOR
  Private ModuliExt_P As Integer = bsModExtORE
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDORSCHO"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldScho = CType(MyBase.ocldBase, CLDORSCHO)
    oCldScho.Init(oApp)
    Return True
  End Function

  Public Overridable Function LeggiDatiDitta(ByVal strDitta As String) As Boolean
    Try
      oCldScho.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttTabanaz)
      strDittaCorrente = strDitta

      lIITTScho = oCldScho.GetTblInstId("TTSCHO", False)

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

  Public Overridable Function edCodstag_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldScho.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABSTAG", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128595749233917773, "Codice stagione |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function edGruppo_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldScho.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABGMER", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128595753418613531, "Codice gruppo |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function edSottogr_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldScho.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABSGME", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128595753418619932, "Codice sottogruppo |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function edCodcfam_Validated(ByVal strCod As String, ByRef strDescr As String) As Boolean
    Try
      If Trim(strCod) = "" Then
        strDescr = ""
        Return True
      End If

      If Not oCldScho.ValCodiceDb(strCod, strDittaCorrente, "TABCFAM", "S", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128595758013999311, "Codice linea/famiglia |'" & strCod & "'| inesistente")))
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
  Public Overridable Function edCodlsar_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldScho.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLSAR", "N", strDescr) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130378888178742579, "Codice lista selezionata articoli |'" & nCod.ToString & "'| inesistente.")))
        Return False
      End If
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
  Public Overridable Function edCodlsel_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldScho.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLSEL", "N", strDescr, dttTmp) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130378894806719101, "Codice lista selezionata clienti/fornitori |'" & nCod.ToString & "'| inesistente.")))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_tipocl).ToUpper <> "C" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130378896322266232, "Il codice lista selezionata clienti/fornitori |'" & nCod.ToString & "'| selezionato, deve essere di tipo 'Clienti/Fornitori'.")))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function TestPreElabora() As Boolean
    Try

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

  Public Overridable Function Elabora(ByVal strDacodart As String, ByVal strAcodart As String, _
                                      ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                      ByVal stredDaagente As String, ByVal stredAagente As String, _
                                      ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                      ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                      ByVal stredDaconto As String, ByVal stredAconto As String, _
                                      ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                      ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                      ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                      ByVal stredSottogr As String, ByVal stredSerie As String, _
                                      ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                      ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                      ByVal bckSelAnnoStag As Boolean, _
                                      ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                      ByVal bckMovord As Boolean, ByVal bckMovord4 As Boolean, _
                                      ByVal bckMovord1 As Boolean, ByVal bckMovord2 As Boolean, _
                                      ByVal bckMovord3 As Boolean, ByVal bckOrdlist As Boolean, _
                                      ByVal bckZzdispsca As Boolean, ByVal bckTaskPM As Boolean, _
                                      ByVal bckSolotaskril As Boolean, ByVal strQuery As String, _
                                      ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                      ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                      ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Elabora(strDacodart, strAcodart, stredDamagaz, stredAmagaz, stredDaagente, stredAagente, _
              stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
              stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, bckEvasi, stredGruppo, stredSottogr, _
              stredSerie, bcbTipork, strTipork, strcbRilasciato, stredCodcfam, bckSelAnnoStag, stredAnnotco, _
              stredCodstag, bckMovord, bckMovord4, bckMovord1, bckMovord2, bckMovord3, bckOrdlist, bckZzdispsca, _
              bckTaskPM, bckSolotaskril, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
              strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function Elabora(ByVal strDacodart As String, ByVal strAcodart As String, _
                                      ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                      ByVal stredDaagente As String, ByVal stredAagente As String, _
                                      ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                      ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                      ByVal stredDaconto As String, ByVal stredAconto As String, _
                                      ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                      ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                      ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                      ByVal stredSottogr As String, ByVal stredSerie As String, _
                                      ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                      ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                      ByVal bckSelAnnoStag As Boolean, _
                                      ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                      ByVal bckMovord As Boolean, ByVal bckMovord4 As Boolean, _
                                      ByVal bckMovord1 As Boolean, ByVal bckMovord2 As Boolean, _
                                      ByVal bckMovord3 As Boolean, ByVal bckOrdlist As Boolean, _
                                      ByVal bckZzdispsca As Boolean, ByVal bckTaskPM As Boolean, _
                                      ByVal bckSolotaskril As Boolean, ByVal strQuery As String, _
                                      ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                      ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                      ByVal strClassificazioneLivello5 As String, _
                                      ByVal strConto As String, ByVal nCodlsel As Integer, _
                                      ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Try

      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                                             stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                             stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                             stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                                             bckEvasi, stredGruppo, stredSottogr, stredSerie, bcbTipork, strTipork, _
                                             strcbRilasciato, stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                             bckMovord, bckMovord4, bckMovord1, bckMovord2, bckMovord3, bckOrdlist, _
                                             bckZzdispsca, bckTaskPM, bckSolotaskril, strQuery, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, strConto, nCodlsel, strCodart, nCodlsar})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If

      '--------------------------------------------------------------------------------------------------------------
      Elabora(strDacodart, strAcodart, stredDamagaz, stredAmagaz, stredDaagente, stredAagente, _
              stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
              stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, bckEvasi, stredGruppo, stredSottogr, _
              stredSerie, bcbTipork, strTipork, strcbRilasciato, stredCodcfam, bckSelAnnoStag, stredAnnotco, _
              stredCodstag, bckMovord, bckMovord4, bckMovord1, bckMovord2, bckMovord3, bckOrdlist, bckZzdispsca, _
              bckTaskPM, bckSolotaskril, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
              strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, strConto, nCodlsel, strCodart, nCodlsar, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function Elabora(ByVal strDacodart As String, ByVal strAcodart As String, _
                                     ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                     ByVal stredDaagente As String, ByVal stredAagente As String, _
                                     ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                     ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                     ByVal stredDaconto As String, ByVal stredAconto As String, _
                                     ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                     ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                     ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                     ByVal stredSottogr As String, ByVal stredSerie As String, _
                                     ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                     ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                     ByVal bckSelAnnoStag As Boolean, _
                                     ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                     ByVal bckMovord As Boolean, ByVal bckMovord4 As Boolean, _
                                     ByVal bckMovord1 As Boolean, ByVal bckMovord2 As Boolean, _
                                     ByVal bckMovord3 As Boolean, ByVal bckOrdlist As Boolean, _
                                     ByVal bckZzdispsca As Boolean, ByVal bckTaskPM As Boolean, _
                                     ByVal bckSolotaskril As Boolean, ByVal strQuery As String, _
                                     ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                     ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                     ByVal strClassificazioneLivello5 As String, _
                                     ByVal strConto As String, ByVal nCodlsel As Integer, _
                                     ByVal strCodart As String, ByVal nCodlsar As Integer, ByVal bAccorpa As Boolean) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                                             stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                             stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                             stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                                             bckEvasi, stredGruppo, stredSottogr, stredSerie, bcbTipork, strTipork, _
                                             strcbRilasciato, stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                             bckMovord, bckMovord4, bckMovord1, bckMovord2, bckMovord3, bckOrdlist, _
                                             bckZzdispsca, bckTaskPM, bckSolotaskril, strQuery, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, strConto, nCodlsel, strCodart, nCodlsar, bAccorpa})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not TestPreElabora() Then Return False
      '--------------------------------------------------------------------------------------------------------------
      If Not oCldScho.Elabora(strDittaCorrente, strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                              stredDaagente, stredAagente, stredDadatord, stredAdatord, stredDadatcons, stredAdatcons, _
                              stredDaconto, stredAconto, stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                              bckEvasi, stredGruppo, stredSottogr, stredSerie, bcbTipork, strTipork, strcbRilasciato, _
                              stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, bModuloCRM, _
                              bIsCRMUser, strAccvis, bAmm, lCodorgaOperat, strRegvis, lIITTScho, bckMovord, _
                              bModAS, bckMovord4, bckMovord1, bckMovord2, bckMovord3, bckOrdlist, bckZzdispsca, _
                              bckTaskPM, bckSolotaskril, nMagazInternoTransito, strQuery, _
                              strClassificazioneLivello1, strClassificazioneLivello2, _
                              strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                              strConto, nCodlsel, strCodart, nCodlsar, bAccorpa) Then Return False
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

  Public Overridable Function AggiornaRilasci(ByVal strDacodart As String, ByVal strAcodart As String, _
                                              ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                              ByVal stredDaagente As String, ByVal stredAagente As String, _
                                              ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                              ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                              ByVal stredDaconto As String, ByVal stredAconto As String, _
                                              ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                              ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                              ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                              ByVal stredSottogr As String, ByVal stredSerie As String, _
                                              ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                              ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                              ByVal bckSelAnnoStag As Boolean, _
                                              ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                              ByVal strQuery As String) As Boolean
    Try
      Return oCldScho.AggiornaRilasci(strDittaCorrente, _
                                      strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                                      stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                      stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                      stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                                      bckEvasi, stredGruppo, stredSottogr, stredSerie, _
                                      bcbTipork, strTipork, strcbRilasciato, stredCodcfam, _
                                      bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                      strQuery)

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
  Public Overridable Function AggiornaRilasci(ByVal strDacodart As String, ByVal strAcodart As String, _
                                             ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                             ByVal stredDaagente As String, ByVal stredAagente As String, _
                                             ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                             ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                             ByVal stredDaconto As String, ByVal stredAconto As String, _
                                             ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                             ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                             ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredSerie As String, _
                                             ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                             ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                             ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldScho.AggiornaRilasci(strDittaCorrente, _
                                      strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                                      stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                      stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                      stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                                      bckEvasi, stredGruppo, stredSottogr, stredSerie, _
                                      bcbTipork, strTipork, strcbRilasciato, stredCodcfam, _
                                      bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                      strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)

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
  Public Overridable Function AggiornaRilasci(ByVal strDacodart As String, ByVal strAcodart As String, _
                                             ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                             ByVal stredDaagente As String, ByVal stredAagente As String, _
                                             ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                             ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                             ByVal stredDaconto As String, ByVal stredAconto As String, _
                                             ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                             ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                             ByVal bckEvasi As Boolean, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredSerie As String, _
                                             ByVal bcbTipork As Boolean, ByVal strTipork As String, _
                                             ByVal strcbRilasciato As String, ByVal stredCodcfam As String, _
                                             ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String, _
                                             ByVal strConto As String, ByVal nCodlsel As Integer, _
                                             ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldScho.AggiornaRilasci(strDittaCorrente, strDacodart, strAcodart, stredDamagaz, stredAmagaz, _
                                      stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                      stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                      stredCommecaini, stredCommecafin, stredFaseini, stredFasefin, _
                                      bckEvasi, stredGruppo, stredSottogr, stredSerie, bcbTipork, strTipork, _
                                      strcbRilasciato, stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, _
                                      stredCodstag, strQuery, strClassificazioneLivello1, _
                                      strClassificazioneLivello2, strClassificazioneLivello3, _
                                      strClassificazioneLivello4, strClassificazioneLivello5, _
                                      strConto, nCodlsel, strCodart, nCodlsar)

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

  Public Overridable Function CheckTmpTable() As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCldScho.CheckTmpTable(strDittaCorrente, lIITTScho, dsTmp)

      If dsTmp.Tables("TTSCHO").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128595655962662684, "Non esistono dati da stampare con queste caratteristiche.")))
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

  Public Overridable Function GetQueryStampaWord(ByVal bckEvasi As Boolean, ByVal stredDamagaz As String, _
                                                 ByVal stredAmagaz As String, ByVal stredDaagente As String, _
                                                 ByVal stredAagente As String, ByVal stredDadatord As String, _
                                                 ByVal stredAdatord As String, ByVal stredDadatcons As String, _
                                                 ByVal stredAdatcons As String, ByVal stredDaconto As String, _
                                                 ByVal stredAconto As String, ByVal stredDacodart As String, _
                                                 ByVal stredAcodart As String, ByVal stredCommecaini As String, _
                                                 ByVal stredCommecafin As String, ByVal stredFaseini As String, _
                                                 ByVal stredFasefin As String, ByVal stredGruppo As String, _
                                                 ByVal stredSottogr As String, ByVal stredSerie As String, _
                                                 ByVal strcbRilasciato As String, ByVal bckTipork As Boolean, _
                                                 ByVal stredCodcfam As String, _
                                                 ByVal bckSelAnnoStag As Boolean, ByVal stredAnnotco As String, _
                                                 ByVal stredCodstag As String, ByVal strcbTipork As String, _
                                                 ByVal strQuery As String) As String
    Try
      Return oCldScho.GetQueryStampaWord(strDittaCorrente, bStampaWordRaggruppata, bckEvasi, stredDamagaz, _
                                                  stredAmagaz, stredDaagente, stredAagente, stredDadatord, _
                                                  stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, _
                                                  stredAconto, stredDacodart, stredAcodart, stredCommecaini, _
                                                  stredCommecafin, stredFaseini, stredFasefin, stredGruppo, _
                                                  stredSottogr, stredSerie, strcbRilasciato, bckTipork, _
                                                  stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, _
                                                  stredCodstag, bModuloCRM, bIsCRMUser, strAccvis, _
                                                  lCodorgaOperat, strRegvis, bAmm, strcbTipork, _
                                                  strQuery)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Function GetQueryStampaWord(ByVal bckEvasi As Boolean, ByVal stredDamagaz As String, _
                                                 ByVal stredAmagaz As String, ByVal stredDaagente As String, _
                                                 ByVal stredAagente As String, ByVal stredDadatord As String, _
                                                 ByVal stredAdatord As String, ByVal stredDadatcons As String, _
                                                 ByVal stredAdatcons As String, ByVal stredDaconto As String, _
                                                 ByVal stredAconto As String, ByVal stredDacodart As String, _
                                                 ByVal stredAcodart As String, ByVal stredCommecaini As String, _
                                                 ByVal stredCommecafin As String, ByVal stredFaseini As String, _
                                                 ByVal stredFasefin As String, ByVal stredGruppo As String, _
                                                 ByVal stredSottogr As String, ByVal stredSerie As String, _
                                                 ByVal strcbRilasciato As String, ByVal bckTipork As Boolean, _
                                                 ByVal stredCodcfam As String, _
                                                 ByVal bckSelAnnoStag As Boolean, ByVal stredAnnotco As String, _
                                                 ByVal stredCodstag As String, ByVal strcbTipork As String, _
                                                 ByVal strQuery As String, _
                                                 ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String) As String
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldScho.GetQueryStampaWord(strDittaCorrente, bStampaWordRaggruppata, bckEvasi, stredDamagaz, _
                                                  stredAmagaz, stredDaagente, stredAagente, stredDadatord, _
                                                  stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, _
                                                  stredAconto, stredDacodart, stredAcodart, stredCommecaini, _
                                                  stredCommecafin, stredFaseini, stredFasefin, stredGruppo, _
                                                  stredSottogr, stredSerie, strcbRilasciato, bckTipork, _
                                                  stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, _
                                                  stredCodstag, bModuloCRM, bIsCRMUser, strAccvis, _
                                                  lCodorgaOperat, strRegvis, bAmm, strcbTipork, _
                                                  strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                                  strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Function GetQueryStampaWord(ByVal bckEvasi As Boolean, ByVal stredDamagaz As String, _
                                                 ByVal stredAmagaz As String, ByVal stredDaagente As String, _
                                                 ByVal stredAagente As String, ByVal stredDadatord As String, _
                                                 ByVal stredAdatord As String, ByVal stredDadatcons As String, _
                                                 ByVal stredAdatcons As String, ByVal stredDaconto As String, _
                                                 ByVal stredAconto As String, ByVal stredDacodart As String, _
                                                 ByVal stredAcodart As String, ByVal stredCommecaini As String, _
                                                 ByVal stredCommecafin As String, ByVal stredFaseini As String, _
                                                 ByVal stredFasefin As String, ByVal stredGruppo As String, _
                                                 ByVal stredSottogr As String, ByVal stredSerie As String, _
                                                 ByVal strcbRilasciato As String, ByVal bckTipork As Boolean, _
                                                 ByVal stredCodcfam As String, _
                                                 ByVal bckSelAnnoStag As Boolean, ByVal stredAnnotco As String, _
                                                 ByVal stredCodstag As String, ByVal strcbTipork As String, _
                                                 ByVal strQuery As String, _
                                                 ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String, _
                                                 ByVal strConto As String, ByVal nCodlsel As Integer, _
                                                 ByVal strCodart As String, ByVal nCodlsar As Integer) As String
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldScho.GetQueryStampaWord(strDittaCorrente, bStampaWordRaggruppata, bckEvasi, stredDamagaz, _
                                                  stredAmagaz, stredDaagente, stredAagente, stredDadatord, _
                                                  stredAdatord, stredDadatcons, stredAdatcons, stredDaconto, _
                                                  stredAconto, stredDacodart, stredAcodart, stredCommecaini, _
                                                  stredCommecafin, stredFaseini, stredFasefin, stredGruppo, _
                                                  stredSottogr, stredSerie, strcbRilasciato, bckTipork, _
                                                  stredCodcfam, bModTCO, bckSelAnnoStag, stredAnnotco, _
                                                  stredCodstag, bModuloCRM, bIsCRMUser, strAccvis, _
                                                  lCodorgaOperat, strRegvis, bAmm, strcbTipork, _
                                                  strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                                  strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                                  strConto, nCodlsel, strCodart, nCodlsar)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Function RipartisciTaglieQuantitaRagg(ByVal bckEvasi As Boolean, _
                                                          ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                          ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                          ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                          ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                          ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                          ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                          ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                          ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                          ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                          ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                          ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                          ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                          ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                          ByRef strSQLTaglieRagr As String) As Boolean
    Try
      Return oCldScho.RipartisciTaglieQuantitaRagg(strDittaCorrente, bckEvasi, stredDamagaz, stredAmagaz, _
                                                  stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                                  stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                                  stredDacodart, stredAcodart, stredCommecaini, stredCommecafin, _
                                                  stredFaseini, stredFasefin, stredGruppo, stredSottogr, _
                                                  stredSerie, strcbRilasciato, bckTipork, strcbTipork, _
                                                  stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                                  bModTCO, strSQLTaglieRagr)

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
  Public Overridable Function RipartisciTaglieQuantitaRagg(ByVal bckEvasi As Boolean, _
                                                          ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                          ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                          ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                          ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                          ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                          ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                          ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                          ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                          ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                          ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                          ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                          ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                          ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                          ByRef strSQLTaglieRagr As String, _
                                                          ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                          ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                          ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldScho.RipartisciTaglieQuantitaRagg(strDittaCorrente, bckEvasi, stredDamagaz, stredAmagaz, _
                                                  stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                                  stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                                  stredDacodart, stredAcodart, stredCommecaini, stredCommecafin, _
                                                  stredFaseini, stredFasefin, stredGruppo, stredSottogr, _
                                                  stredSerie, strcbRilasciato, bckTipork, strcbTipork, _
                                                  stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                                  bModTCO, strSQLTaglieRagr, strClassificazioneLivello1, strClassificazioneLivello2, _
                                                  strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)

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
  Public Overridable Function RipartisciTaglieQuantitaRagg(ByVal bckEvasi As Boolean, _
                                                          ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                          ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                          ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                          ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                          ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                          ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                          ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                          ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                          ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                          ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                          ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                          ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                          ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                          ByRef strSQLTaglieRagr As String, _
                                                          ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                          ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                          ByVal strClassificazioneLivello5 As String, _
                                                          ByVal strConto As String, ByVal nCodlsel As Integer, _
                                                          ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldScho.RipartisciTaglieQuantitaRagg(strDittaCorrente, bckEvasi, stredDamagaz, stredAmagaz, _
                                                  stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                                  stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                                  stredDacodart, stredAcodart, stredCommecaini, stredCommecafin, _
                                                  stredFaseini, stredFasefin, stredGruppo, stredSottogr, _
                                                  stredSerie, strcbRilasciato, bckTipork, strcbTipork, _
                                                  stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                                  bModTCO, strSQLTaglieRagr, strClassificazioneLivello1, strClassificazioneLivello2, _
                                                  strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                                  strConto, nCodlsel, strCodart, nCodlsar)

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

  Public Overridable Function TaglieQuantitaRagg(ByVal bckEvasi As Boolean, _
                                                        ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                        ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                        ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                        ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                        ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                        ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                        ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                        ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                        ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                        ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                        ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                        ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                        ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                        ByRef strSQLTaglieRagr As String) As Boolean
    Try
      Return oCldScho.TaglieQuantitaRagg(strDittaCorrente, bckEvasi, stredDamagaz, stredAmagaz, _
                                                  stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                                  stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                                  stredDacodart, stredAcodart, stredCommecaini, stredCommecafin, _
                                                  stredFaseini, stredFasefin, stredGruppo, stredSottogr, _
                                                  stredSerie, strcbRilasciato, bckTipork, strcbTipork, _
                                                  stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                                  bModTCO, strSQLTaglieRagr)

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
  Public Overridable Function TaglieQuantitaRagg(ByVal bckEvasi As Boolean, _
                                                        ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                        ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                        ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                        ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                        ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                        ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                        ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                        ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                        ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                        ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                        ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                        ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                        ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                        ByRef strSQLTaglieRagr As String, _
                                                        ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                        ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                        ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldScho.TaglieQuantitaRagg(strDittaCorrente, bckEvasi, stredDamagaz, stredAmagaz, _
                                                  stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                                  stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                                  stredDacodart, stredAcodart, stredCommecaini, stredCommecafin, _
                                                  stredFaseini, stredFasefin, stredGruppo, stredSottogr, _
                                                  stredSerie, strcbRilasciato, bckTipork, strcbTipork, _
                                                  stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                                  bModTCO, strSQLTaglieRagr, strClassificazioneLivello1, strClassificazioneLivello2, _
                                                  strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)

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
  Public Overridable Function TaglieQuantitaRagg(ByVal bckEvasi As Boolean, _
                                                        ByVal stredDamagaz As String, ByVal stredAmagaz As String, _
                                                        ByVal stredDaagente As String, ByVal stredAagente As String, _
                                                        ByVal stredDadatord As String, ByVal stredAdatord As String, _
                                                        ByVal stredDadatcons As String, ByVal stredAdatcons As String, _
                                                        ByVal stredDaconto As String, ByVal stredAconto As String, _
                                                        ByVal stredDacodart As String, ByVal stredAcodart As String, _
                                                        ByVal stredCommecaini As String, ByVal stredCommecafin As String, _
                                                        ByVal stredFaseini As String, ByVal stredFasefin As String, _
                                                        ByVal stredGruppo As String, ByVal stredSottogr As String, _
                                                        ByVal stredSerie As String, ByVal strcbRilasciato As String, _
                                                        ByVal bckTipork As Boolean, ByVal strcbTipork As String, _
                                                        ByVal stredCodcfam As String, ByVal bckSelAnnoStag As Boolean, _
                                                        ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                        ByRef strSQLTaglieRagr As String, _
                                                        ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                        ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                        ByVal strClassificazioneLivello5 As String, _
                                                        ByVal strConto As String, ByVal nCodlsel As Integer, _
                                                        ByVal strCodart As String, ByVal nCodlsar As Integer) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldScho.TaglieQuantitaRagg(strDittaCorrente, bckEvasi, stredDamagaz, stredAmagaz, _
                                                  stredDaagente, stredAagente, stredDadatord, stredAdatord, _
                                                  stredDadatcons, stredAdatcons, stredDaconto, stredAconto, _
                                                  stredDacodart, stredAcodart, stredCommecaini, stredCommecafin, _
                                                  stredFaseini, stredFasefin, stredGruppo, stredSottogr, _
                                                  stredSerie, strcbRilasciato, bckTipork, strcbTipork, _
                                                  stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                                  bModTCO, strSQLTaglieRagr, strClassificazioneLivello1, strClassificazioneLivello2, _
                                                  strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                                  strConto, nCodlsel, strCodart, nCodlsar)

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

  Public Overridable Function RitornaLISTSAR(ByVal nCodlsar As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldScho.RitornaLISTSAR(strDittaCorrente, nCodlsar, dttOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function RitornaLISTSEL(ByVal nCodlsel As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldScho.RitornaLISTSEL(strDittaCorrente, nCodlsel, dttOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

#Region "funzioni specifiche per BNORGSCHO.BNORGRSO.VB"
  Public Overridable Function CheckArticotaglie(ByVal strCodart As String) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCldScho.CheckArticotaglie(strDittaCorrente, strCodart, dsTmp)

      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128595753414613531, "L'articolo non è gestito per taglie e colori." & vbCrLf & _
            "Visualizzazione dettaglio quantità per taglia non possibile.")))
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

  Public Overridable Function GetRelease() As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCldScho.GetRelease(dsTmp)

      If (NTSCInt(dsTmp.Tables("RELEASE").Rows(0)!rel_maior) = 10 And Not NTSCStr(dsTmp.Tables("RELEASE").Rows(0)!rel_pers) = "") _
        Or CInt(dsTmp.Tables("RELEASE").Rows(0)!rel_maior) > 10 Then
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

  Public Overridable Function lbArticolo_Validated(ByVal strCod As String, ByRef strDescr As String) As Boolean
    Try
      If Trim(strCod) = "" Then
        strDescr = ""
        Return True
      End If

      If Not oCldScho.ValCodiceDb(strCod, strDittaCorrente, "ARTICO", "S", strDescr) Then
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
  Public Overridable Function lbConto_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldScho.ValCodiceDb(nCod.ToString, strDittaCorrente, "ANAGRA", "N", strDescr) Then
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
  Public Overridable Function lbMagaz_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldScho.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABMAGA", "N", strDescr) Then
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
  Public Overridable Function lbCommeca_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldScho.ValCodiceDb(nCod.ToString, strDittaCorrente, "COMMESS", "N", strDescr) Then
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

  Public Overridable Function ComponiStringaSQL(ByVal dtrTmp As DataRow, ByRef ds As DataSet, ByRef dEsist As Decimal) As Boolean
    Dim dsTmp As DataSet = Nothing
    Dim nDaGruppo As Integer
    Dim nAGruppo As Integer
    Dim nDaSottogr As Integer
    Dim nASottogr As Integer
    Dim lDaConto As Integer
    Dim lAConto As Integer
    Dim lDaCommessa As Integer
    Dim lACommessa As Integer
    Dim strDaDatord As String
    Dim strADatord As String
    Dim strDaDatcons As String
    Dim strADatcons As String
    Dim strDaFlevas As String
    Dim strAFlevas As String
    Try
      '----------------------------------------------------------------------------------
      '--- Passa i parametri per la StoreProcedure
      strDaDatord = strSchoDadatord
      strADatord = strSchoAdatord
      strDaDatcons = strSchoDadatcons
      strADatcons = strSchoAdatcons
      If strSchoOrdin = "C" Then
        lDaConto = NTSCInt(dtrTmp!ko_conto) : lAConto = NTSCInt(dtrTmp!ko_conto)
      Else
        lDaConto = lSchoDaconto : lAConto = lSchoAconto
      End If
      If nSchoGruppo > 0 Then
        nDaGruppo = nSchoGruppo : nAGruppo = nSchoGruppo
      Else
        nDaGruppo = 0 : nAGruppo = 999
      End If
      If nSchoSottogr > 0 Then
        nDaSottogr = nSchoSottogr : nASottogr = nSchoSottogr
      Else
        nDaSottogr = 0 : nASottogr = 9999
      End If
      If bSchoEvasi Then
        strDaFlevas = "C" : strAFlevas = "C"
      Else
        strDaFlevas = "C" : strAFlevas = "S"
      End If
      If strSchoOrdin = "X" Then
        lDaCommessa = NTSCInt(dtrTmp!ko_commecap) : lACommessa = NTSCInt(dtrTmp!ko_commecap)
      Else
        lDaCommessa = lSchoCommecaini : lACommessa = lSchoCommecafin
      End If

      '--- Apre uno snapshot per calcolare l'esistenza
      Select Case strSchoOrdin
        Case "A"
          oCldScho.GetArtpro(strDittaCorrente, NTSCStr(dtrTmp!ko_codart), NTSCStr(dtrTmp!ko_fase), NTSCStr(dtrTmp!ko_magaz), dsTmp)
          dEsist = 0
          If dsTmp.Tables("ARTPRO").Rows.Count > 0 Then
            dEsist = NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!ap_esist)
          End If
        Case "X"
          dEsist = CType(oCleComm, CLELBMENU).TrovaEsistDaXpro(strDittaCorrente, NTSCStr(dtrTmp!ko_codart), NTSCInt(dtrTmp!ko_magaz), _
          NTSCInt(dtrTmp!ko_commecap), 0, NTSCInt(dtrTmp!ko_fase))
      End Select

      oCldScho.ComponiStringaSQL(strDittaCorrente, dtrTmp, ds, lDaConto, lAConto, _
                                 lDaCommessa, lACommessa, strDaDatord, strADatord, _
                                 strDaDatcons, strADatcons, strSchoTipork, strSchoSerie, _
                                 nDaGruppo, nAGruppo, nDaSottogr, nASottogr, _
                                 strDaFlevas, strAFlevas, nSchoDaagente, nSchoAagente, _
                                 strSchoRilasciato, strSchoOrdin, dEsist, dDisp, strAltriFiltri, _
                                 strSchoClassLivello1, strSchoClassLivello2, strSchoClassLivello3, _
                                 strSchoClassLivello4, strSchoClassLivello5, _
                                 strSchoConto, nSchoCodlsel, strSchoCodart, nSchoCodlsar)

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

  Public Overridable Function ComponiStringa(ByVal dtrTmp As DataRow, ByRef ds As DataSet, ByRef dEsist As Decimal) As Boolean
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Try
      '----------------------------------------------------------------------------------
      '--- Passa i parametri per la query

      '--- Apre uno snapshot per calcolare l'esistenza
      Select Case strSchoOrdin
        Case "A"
          oCldScho.GetArtpro(strDittaCorrente, NTSCStr(dtrTmp!ko_codart), NTSCStr(dtrTmp!ko_fase), NTSCStr(dtrTmp!ko_magaz), dsTmp)
          dEsist = 0
          If dsTmp.Tables("ARTPRO").Rows.Count > 0 Then
            dEsist = NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!ap_esist)
          End If
        Case "X"
          dEsist = CType(oCleComm, CLELBMENU).TrovaEsistDaXpro(strDittaCorrente, NTSCStr(dtrTmp!ko_codart), NTSCInt(dtrTmp!ko_magaz), _
          NTSCInt(dtrTmp!ko_commecap), 0, NTSCInt(dtrTmp!ko_fase))
      End Select

      oCldScho.ComponiStringa(strDittaCorrente, dtrTmp, ds, lSchoDaconto, lSchoAconto, _
                                 lSchoCommecaini, lSchoCommecafin, strSchoDadatord, strSchoAdatord, _
                                 strSchoDadatcons, strSchoAdatcons, strSchoTipork, strSchoSerie, _
                                 nSchoGruppo, nSchoSottogr, nSchoDaagente, nSchoAagente, _
                                 strSchoRilasciato, strSchoOrdin, dEsist, _
                                 nSchoFaseini, nSchoFasefin, bSchoEvasi, _
                                 bModuloCRM, bIsCRMUser, strAccvis, _
                                 lCodorgaOperat, strRegvis, bAmm, strAltriFiltri, _
                                 strSchoClassLivello1, strSchoClassLivello2, strSchoClassLivello3, _
                                 strSchoClassLivello4, strSchoClassLivello5, _
                                 strSchoConto, nSchoCodlsel, strSchoCodart, nSchoCodlsar)

      '----------------------------------------------------------------------------------
      If strSchoOrdin <> "C" Then
        'Riempie l'array col le disponibilità scalari
        If ds.Tables("MOVORD").Rows.Count > 0 Then
          ReDim dDisp(ds.Tables("MOVORD").Rows.Count)
          If Not (NTSCStr(ds.Tables("MOVORD").Rows(0)!mo_flevas) = "S") Then
            If NTSCInt(ds.Tables("MOVORD").Rows(0)!ko_ordin) = 1 Then
              dDisp(0) = dEsist + (NTSCDec(ds.Tables("MOVORD").Rows(0)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(0)!mo_quaeva))
            Else
              dDisp(0) = dEsist + ((NTSCDec(ds.Tables("MOVORD").Rows(0)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(0)!mo_quaeva)) * (-1))
            End If
            If (NTSCInt(ds.Tables("MOVORD").Rows(0)!ko_ordin) = 0) And (NTSCInt(ds.Tables("MOVORD").Rows(0)!ko_impeg) = 0) Then
              dDisp(0) = dEsist
            End If
          Else
            dDisp(0) = dEsist
          End If
          For i = 1 To ds.Tables("MOVORD").Rows.Count - 1
            If Not (NTSCStr(ds.Tables("MOVORD").Rows(i)!mo_flevas) = "S") Then
              If NTSCInt(ds.Tables("MOVORD").Rows(i)!ko_ordin) = 1 Then
                dDisp(i) = dDisp(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva))
              End If
              If NTSCInt(ds.Tables("MOVORD").Rows(i)!ko_impeg) = 1 Then
                dDisp(i) = dDisp(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva)) * (-1))
              End If
              If (NTSCInt(ds.Tables("MOVORD").Rows(i)!ko_ordin) = 0) And (NTSCInt(ds.Tables("MOVORD").Rows(i)!ko_impeg) = 0) Then
                dDisp(i) = dDisp(i - 1)
              End If
            Else
              dDisp(i) = dDisp(i - 1)
            End If
          Next
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

  Public Overridable Function Apri(ByRef ds As DataSet) As Boolean
    Try
      oCldScho.Apri(strDittaCorrente, ds, lSchoDaconto, lSchoAconto, _
                    lSchoCommecaini, lSchoCommecafin, strSchoDadatord, strSchoAdatord, _
                    strSchoDadatcons, strSchoAdatcons, strSchoTipork, strSchoSerie, _
                    nSchoGruppo, nSchoSottogr, nSchoDaagente, nSchoAagente, _
                    strSchoRilasciato, strSchoOrdin, nSchoDamagaz, nSchoAmagaz, _
                    strSchoDacodart, strSchoAcodart, nSchoFaseini, nSchoFasefin, _
                    bSchoEvasi, strGrsoCodcfam, bGrsoModTCO, nGrsoAnnotco, _
                    nGrsoCodstag, bModuloCRM, bIsCRMUser, strAccvis, _
                    lCodorgaOperat, strRegvis, bAmm, strAltriFiltri, _
                    strSchoClassLivello1, strSchoClassLivello2, strSchoClassLivello3, _
                    strSchoClassLivello4, strSchoClassLivello5, _
                    strSchoConto, nSchoCodlsel, strSchoCodart, nSchoCodlsar)

      If ds.Tables("MOVORD").Rows.Count = 0 Then
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

  Public Overridable Function GetTaglie(ByVal strCodart As String, ByRef ds As DataSet) As Boolean
    Try
      oCldScho.GetTaglie(strDittaCorrente, strCodart, ds)

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

#Region "funzioni specifiche per BNORGSCHO.BNORGRS1.VB"
  Public Overridable Function Grs1GetRelease() As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCldScho.Grs1GetRelease(dsTmp)

      If (NTSCInt(dsTmp.Tables("RELEASE").Rows(0)!rel_maior) = 10 And Not NTSCStr(dsTmp.Tables("RELEASE").Rows(0)!rel_pers) = "") _
        Or CInt(dsTmp.Tables("RELEASE").Rows(0)!rel_maior) > 10 Then
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

  Public Overridable Function Grs1lbArticolo_Validated(ByVal strCod As String, ByRef strDescr As String, ByRef dttTmp As DataTable) As Boolean
    Try
      If Trim(strCod) = "" Then
        strDescr = ""
        Return True
      End If

      If Not oCldScho.ValCodiceDb(strCod, strDittaCorrente, "ARTICO", "S", strDescr, dttTmp) Then
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
  Public Overridable Function Grs1lbMagaz_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldScho.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABMAGA", "N", strDescr) Then
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

  Public Overridable Function Grs1ComponiStringa(ByVal dtrTmp As DataRow, ByRef ds As DataSet, _
                                                 ByRef dEsist As Decimal) As Boolean
    Dim i As Integer
    Dim lRecords As Integer
    Try
      '----------------------------------------------------------------------------------
      '--- Passa i parametri per la query
      oCldScho.Grs1ComponiStringa(strDittaCorrente, dtrTmp, ds, lIITTScho, bGrs1Accorpa)

      If ds.Tables("MOVORD").Rows.Count > 0 Then

        'Carica l'array delle disponibilità
        lRecords = NTSCInt(ds.Tables("MOVORD").Rows.Count)

        ReDim dDisp(lRecords - 1)
        If bArticoloTC Then
          ReDim dDispTC1(lRecords - 1)
          ReDim dDispTC2(lRecords - 1)
          ReDim dDispTC3(lRecords - 1)
          ReDim dDispTC4(lRecords - 1)
          ReDim dDispTC5(lRecords - 1)
          ReDim dDispTC6(lRecords - 1)
          ReDim dDispTC7(lRecords - 1)
          ReDim dDispTC8(lRecords - 1)
          ReDim dDispTC9(lRecords - 1)
          ReDim dDispTC10(lRecords - 1)
          ReDim dDispTC11(lRecords - 1)
          ReDim dDispTC12(lRecords - 1)
          ReDim dDispTC13(lRecords - 1)
          ReDim dDispTC14(lRecords - 1)
          ReDim dDispTC15(lRecords - 1)
          ReDim dDispTC16(lRecords - 1)
          ReDim dDispTC17(lRecords - 1)
          ReDim dDispTC18(lRecords - 1)
          ReDim dDispTC19(lRecords - 1)
          ReDim dDispTC20(lRecords - 1)
          ReDim dDispTC21(lRecords - 1)
          ReDim dDispTC22(lRecords - 1)
          ReDim dDispTC23(lRecords - 1)
          ReDim dDispTC24(lRecords - 1)
        End If
        If Not NTSCStr(ds.Tables("MOVORD").Rows(i)!MO_ORIGINE) = "A" Then
          If Not (NTSCStr(ds.Tables("MOVORD").Rows(i)!mo_flevas) = "S") Then
            If NTSCInt(ds.Tables("MOVORD").Rows(i)!mo_ordin) = 1 Then
              dDisp(0) = dEsist + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva))
              If bArticoloTC Then
                dDispTC1(0) = dEsistTC(0) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant01) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva01))
                dDispTC2(0) = dEsistTC(1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant02) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva02))
                dDispTC3(0) = dEsistTC(2) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant03) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva03))
                dDispTC4(0) = dEsistTC(3) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant04) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva04))
                dDispTC5(0) = dEsistTC(4) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant05) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva05))
                dDispTC6(0) = dEsistTC(5) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant06) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva06))
                dDispTC7(0) = dEsistTC(6) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant07) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva07))
                dDispTC8(0) = dEsistTC(7) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant08) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva08))
                dDispTC9(0) = dEsistTC(8) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant09) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva09))
                dDispTC10(0) = dEsistTC(9) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant10) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva10))
                dDispTC11(0) = dEsistTC(10) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant11) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva11))
                dDispTC12(0) = dEsistTC(11) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant12) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva12))
                dDispTC13(0) = dEsistTC(12) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant13) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva13))
                dDispTC14(0) = dEsistTC(13) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant14) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva14))
                dDispTC15(0) = dEsistTC(14) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant15) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva15))
                dDispTC16(0) = dEsistTC(15) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant16) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva16))
                dDispTC17(0) = dEsistTC(16) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant17) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva17))
                dDispTC18(0) = dEsistTC(17) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant18) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva18))
                dDispTC19(0) = dEsistTC(18) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant19) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva19))
                dDispTC20(0) = dEsistTC(19) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant20) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva20))
                dDispTC21(0) = dEsistTC(20) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant21) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva21))
                dDispTC22(0) = dEsistTC(21) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant22) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva22))
                dDispTC23(0) = dEsistTC(22) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant23) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva23))
                dDispTC24(0) = dEsistTC(23) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant24) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva24))
              End If
            Else
              dDisp(0) = dEsist + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva)) * (-1))
              If bArticoloTC Then
                dDispTC1(0) = dEsistTC(0) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant01) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva01)) * (-1))
                dDispTC2(0) = dEsistTC(1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant02) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva02)) * (-1))
                dDispTC3(0) = dEsistTC(2) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant03) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva03)) * (-1))
                dDispTC4(0) = dEsistTC(3) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant04) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva04)) * (-1))
                dDispTC5(0) = dEsistTC(4) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant05) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva05)) * (-1))
                dDispTC6(0) = dEsistTC(5) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant06) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva06)) * (-1))
                dDispTC7(0) = dEsistTC(6) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant07) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva07)) * (-1))
                dDispTC8(0) = dEsistTC(7) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant08) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva08)) * (-1))
                dDispTC9(0) = dEsistTC(8) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant09) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva09)) * (-1))
                dDispTC10(0) = dEsistTC(9) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant10) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva10)) * (-1))
                dDispTC11(0) = dEsistTC(10) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant11) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva11)) * (-1))
                dDispTC12(0) = dEsistTC(11) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant12) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva12)) * (-1))
                dDispTC13(0) = dEsistTC(12) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant13) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva13)) * (-1))
                dDispTC14(0) = dEsistTC(13) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant14) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva14)) * (-1))
                dDispTC15(0) = dEsistTC(14) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant15) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva15)) * (-1))
                dDispTC16(0) = dEsistTC(15) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant16) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva16)) * (-1))
                dDispTC17(0) = dEsistTC(16) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant17) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva17)) * (-1))
                dDispTC18(0) = dEsistTC(17) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant18) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva18)) * (-1))
                dDispTC19(0) = dEsistTC(18) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant19) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva19)) * (-1))
                dDispTC20(0) = dEsistTC(19) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant20) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva20)) * (-1))
                dDispTC21(0) = dEsistTC(20) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant21) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva21)) * (-1))
                dDispTC22(0) = dEsistTC(21) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant22) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva22)) * (-1))
                dDispTC23(0) = dEsistTC(22) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant23) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva23)) * (-1))
                dDispTC24(0) = dEsistTC(23) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant24) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva24)) * (-1))
              End If
            End If
            If (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_ordin) = 0) And (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_impeg) = 0) Then
              dDisp(0) = dEsist
              If bArticoloTC Then
                dDispTC1(0) = dEsistTC(0)
                dDispTC2(0) = dEsistTC(1)
                dDispTC3(0) = dEsistTC(2)
                dDispTC4(0) = dEsistTC(3)
                dDispTC5(0) = dEsistTC(4)
                dDispTC6(0) = dEsistTC(5)
                dDispTC7(0) = dEsistTC(6)
                dDispTC8(0) = dEsistTC(7)
                dDispTC9(0) = dEsistTC(8)
                dDispTC10(0) = dEsistTC(9)
                dDispTC11(0) = dEsistTC(10)
                dDispTC12(0) = dEsistTC(11)
                dDispTC13(0) = dEsistTC(12)
                dDispTC14(0) = dEsistTC(13)
                dDispTC15(0) = dEsistTC(14)
                dDispTC16(0) = dEsistTC(15)
                dDispTC17(0) = dEsistTC(16)
                dDispTC18(0) = dEsistTC(17)
                dDispTC19(0) = dEsistTC(18)
                dDispTC20(0) = dEsistTC(19)
                dDispTC21(0) = dEsistTC(20)
                dDispTC22(0) = dEsistTC(21)
                dDispTC23(0) = dEsistTC(22)
                dDispTC24(0) = dEsistTC(23)
              End If
            End If
          Else
            dDisp(0) = dEsist
            If bArticoloTC Then
              dDispTC1(0) = dEsistTC(0)
              dDispTC2(0) = dEsistTC(1)
              dDispTC3(0) = dEsistTC(2)
              dDispTC4(0) = dEsistTC(3)
              dDispTC5(0) = dEsistTC(4)
              dDispTC6(0) = dEsistTC(5)
              dDispTC7(0) = dEsistTC(6)
              dDispTC8(0) = dEsistTC(7)
              dDispTC9(0) = dEsistTC(8)
              dDispTC10(0) = dEsistTC(9)
              dDispTC11(0) = dEsistTC(10)
              dDispTC12(0) = dEsistTC(11)
              dDispTC13(0) = dEsistTC(12)
              dDispTC14(0) = dEsistTC(13)
              dDispTC15(0) = dEsistTC(14)
              dDispTC16(0) = dEsistTC(15)
              dDispTC17(0) = dEsistTC(16)
              dDispTC18(0) = dEsistTC(17)
              dDispTC19(0) = dEsistTC(18)
              dDispTC20(0) = dEsistTC(19)
              dDispTC21(0) = dEsistTC(20)
              dDispTC22(0) = dEsistTC(21)
              dDispTC23(0) = dEsistTC(22)
              dDispTC24(0) = dEsistTC(23)
            End If
          End If
        Else
          dDisp(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant)
          If bArticoloTC Then
            dDispTC1(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant01)
            dDispTC2(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant02)
            dDispTC3(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant03)
            dDispTC4(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant04)
            dDispTC5(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant05)
            dDispTC6(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant06)
            dDispTC7(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant07)
            dDispTC8(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant08)
            dDispTC9(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant09)
            dDispTC10(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant10)
            dDispTC11(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant11)
            dDispTC12(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant12)
            dDispTC13(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant13)
            dDispTC14(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant14)
            dDispTC15(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant15)
            dDispTC16(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant16)
            dDispTC17(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant17)
            dDispTC18(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant18)
            dDispTC19(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant19)
            dDispTC20(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant20)
            dDispTC21(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant21)
            dDispTC22(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant22)
            dDispTC23(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant23)
            dDispTC24(0) = NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant24)
          End If
        End If
        i = i + 1
        If ds.Tables("MOVORD").Rows.Count = i Then
          Return True
        End If
        For i = 1 To ds.Tables("MOVORD").Rows.Count - 1
          If Not (NTSCStr(ds.Tables("MOVORD").Rows(i)!MO_ORIGINE) = "A") Then
            If Not (NTSCStr(ds.Tables("MOVORD").Rows(i)!mo_flevas) = "S") Then
              If NTSCInt(ds.Tables("MOVORD").Rows(i)!mo_ordin) = 1 Then
                dDisp(i) = dDisp(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva))
                If bArticoloTC Then
                  dDispTC1(i) = dDispTC1(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant01) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva01))
                  dDispTC2(i) = dDispTC2(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant02) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva02))
                  dDispTC3(i) = dDispTC3(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant03) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva03))
                  dDispTC4(i) = dDispTC4(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant04) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva04))
                  dDispTC5(i) = dDispTC5(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant05) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva05))
                  dDispTC6(i) = dDispTC6(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant06) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva06))
                  dDispTC7(i) = dDispTC7(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant07) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva07))
                  dDispTC8(i) = dDispTC8(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant08) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva08))
                  dDispTC9(i) = dDispTC9(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant09) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva09))
                  dDispTC10(i) = dDispTC10(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant10) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva10))
                  dDispTC11(i) = dDispTC11(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant11) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva11))
                  dDispTC12(i) = dDispTC12(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant12) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva12))
                  dDispTC13(i) = dDispTC13(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant13) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva13))
                  dDispTC14(i) = dDispTC14(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant14) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva14))
                  dDispTC15(i) = dDispTC15(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant15) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva15))
                  dDispTC16(i) = dDispTC16(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant16) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva16))
                  dDispTC17(i) = dDispTC17(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant17) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva17))
                  dDispTC18(i) = dDispTC18(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant18) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva18))
                  dDispTC19(i) = dDispTC19(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant19) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva19))
                  dDispTC20(i) = dDispTC20(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant20) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva20))
                  dDispTC21(i) = dDispTC21(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant21) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva21))
                  dDispTC22(i) = dDispTC22(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant22) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva22))
                  dDispTC23(i) = dDispTC23(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant23) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva23))
                  dDispTC24(i) = dDispTC24(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant24) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva24))
                End If
              End If
              If NTSCInt(ds.Tables("MOVORD").Rows(i)!mo_impeg) = 1 Then
                dDisp(i) = dDisp(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva)) * (-1))
                If bArticoloTC Then
                  dDispTC1(i) = dDispTC1(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant01) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva01)) * (-1))
                  dDispTC2(i) = dDispTC2(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant02) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva02)) * (-1))
                  dDispTC3(i) = dDispTC3(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant03) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva03)) * (-1))
                  dDispTC4(i) = dDispTC4(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant04) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva04)) * (-1))
                  dDispTC5(i) = dDispTC5(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant05) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva05)) * (-1))
                  dDispTC6(i) = dDispTC6(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant06) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva06)) * (-1))
                  dDispTC7(i) = dDispTC7(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant07) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva07)) * (-1))
                  dDispTC8(i) = dDispTC8(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant08) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva08)) * (-1))
                  dDispTC9(i) = dDispTC9(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant09) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva09)) * (-1))
                  dDispTC10(i) = dDispTC10(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant10) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva10)) * (-1))
                  dDispTC11(i) = dDispTC11(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant11) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva11)) * (-1))
                  dDispTC12(i) = dDispTC12(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant12) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva12)) * (-1))
                  dDispTC13(i) = dDispTC13(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant13) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva13)) * (-1))
                  dDispTC14(i) = dDispTC14(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant14) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva14)) * (-1))
                  dDispTC15(i) = dDispTC15(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant15) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva15)) * (-1))
                  dDispTC16(i) = dDispTC16(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant16) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva16)) * (-1))
                  dDispTC17(i) = dDispTC17(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant17) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva17)) * (-1))
                  dDispTC18(i) = dDispTC18(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant18) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva18)) * (-1))
                  dDispTC19(i) = dDispTC19(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant19) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva19)) * (-1))
                  dDispTC20(i) = dDispTC20(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant20) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva20)) * (-1))
                  dDispTC21(i) = dDispTC21(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant21) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva21)) * (-1))
                  dDispTC22(i) = dDispTC22(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant22) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva22)) * (-1))
                  dDispTC23(i) = dDispTC23(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant23) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva23)) * (-1))
                  dDispTC24(i) = dDispTC24(i - 1) + ((NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant24) - NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quaeva24)) * (-1))
                End If
              End If
              If (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_ordin) = 0) And (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_impeg) = 0) Then
                dDisp(i) = dDisp(i - 1)
                If bArticoloTC Then
                  dDispTC1(i) = dDispTC1(i - 1)
                  dDispTC2(i) = dDispTC2(i - 1)
                  dDispTC3(i) = dDispTC3(i - 1)
                  dDispTC4(i) = dDispTC4(i - 1)
                  dDispTC5(i) = dDispTC5(i - 1)
                  dDispTC6(i) = dDispTC6(i - 1)
                  dDispTC7(i) = dDispTC7(i - 1)
                  dDispTC8(i) = dDispTC8(i - 1)
                  dDispTC9(i) = dDispTC9(i - 1)
                  dDispTC10(i) = dDispTC10(i - 1)
                  dDispTC11(i) = dDispTC11(i - 1)
                  dDispTC12(i) = dDispTC12(i - 1)
                  dDispTC13(i) = dDispTC13(i - 1)
                  dDispTC14(i) = dDispTC14(i - 1)
                  dDispTC15(i) = dDispTC15(i - 1)
                  dDispTC16(i) = dDispTC16(i - 1)
                  dDispTC17(i) = dDispTC17(i - 1)
                  dDispTC18(i) = dDispTC18(i - 1)
                  dDispTC19(i) = dDispTC19(i - 1)
                  dDispTC20(i) = dDispTC20(i - 1)
                  dDispTC21(i) = dDispTC21(i - 1)
                  dDispTC22(i) = dDispTC22(i - 1)
                  dDispTC23(i) = dDispTC23(i - 1)
                  dDispTC24(i) = dDispTC24(i - 1)
                End If
              End If
            Else
              dDisp(i) = dDisp(i - 1)
              If bArticoloTC Then
                dDispTC1(i) = dDispTC1(i - 1)
                dDispTC2(i) = dDispTC2(i - 1)
                dDispTC3(i) = dDispTC3(i - 1)
                dDispTC4(i) = dDispTC4(i - 1)
                dDispTC5(i) = dDispTC5(i - 1)
                dDispTC6(i) = dDispTC6(i - 1)
                dDispTC7(i) = dDispTC7(i - 1)
                dDispTC8(i) = dDispTC8(i - 1)
                dDispTC9(i) = dDispTC9(i - 1)
                dDispTC10(i) = dDispTC10(i - 1)
                dDispTC11(i) = dDispTC11(i - 1)
                dDispTC12(i) = dDispTC12(i - 1)
                dDispTC13(i) = dDispTC13(i - 1)
                dDispTC14(i) = dDispTC14(i - 1)
                dDispTC15(i) = dDispTC15(i - 1)
                dDispTC16(i) = dDispTC16(i - 1)
                dDispTC17(i) = dDispTC17(i - 1)
                dDispTC18(i) = dDispTC18(i - 1)
                dDispTC19(i) = dDispTC19(i - 1)
                dDispTC20(i) = dDispTC20(i - 1)
                dDispTC21(i) = dDispTC21(i - 1)
                dDispTC22(i) = dDispTC22(i - 1)
                dDispTC23(i) = dDispTC23(i - 1)
                dDispTC24(i) = dDispTC24(i - 1)
              End If
            End If
          Else
            dDisp(i) = dDisp(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant))
            If bArticoloTC Then
              dDispTC1(i) = dDispTC1(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant01))
              dDispTC2(i) = dDispTC2(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant02))
              dDispTC3(i) = dDispTC3(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant03))
              dDispTC4(i) = dDispTC4(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant04))
              dDispTC5(i) = dDispTC5(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant05))
              dDispTC6(i) = dDispTC6(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant06))
              dDispTC7(i) = dDispTC7(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant07))
              dDispTC8(i) = dDispTC8(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant08))
              dDispTC9(i) = dDispTC9(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant09))
              dDispTC10(i) = dDispTC10(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant10))
              dDispTC11(i) = dDispTC11(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant11))
              dDispTC12(i) = dDispTC12(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant12))
              dDispTC13(i) = dDispTC13(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant13))
              dDispTC14(i) = dDispTC14(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant14))
              dDispTC15(i) = dDispTC15(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant15))
              dDispTC16(i) = dDispTC16(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant16))
              dDispTC17(i) = dDispTC17(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant17))
              dDispTC18(i) = dDispTC18(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant18))
              dDispTC19(i) = dDispTC19(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant19))
              dDispTC20(i) = dDispTC20(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant20))
              dDispTC21(i) = dDispTC21(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant21))
              dDispTC22(i) = dDispTC22(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant22))
              dDispTC23(i) = dDispTC23(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant23))
              dDispTC24(i) = dDispTC24(i - 1) + (NTSCDec(ds.Tables("MOVORD").Rows(i)!mo_quant24))
            End If
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

  Public Overridable Function Grs1Apri(ByRef ds As DataSet) As Boolean
    Try
      oCldScho.Grs1Apri(strDittaCorrente, ds, lIITTScho, bGrs1Accorpa)

      If ds.Tables("MOVORD").Rows.Count = 0 Then
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

  Public Overridable Function Grs1GetTaglie(ByVal strCodart As String, ByRef ds As DataSet) As Boolean
    Try
      oCldScho.Grs1GetTaglie(strDittaCorrente, strCodart, ds)

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

  Public Overridable Function Grs1GetOrdList(ByRef lOlprogr As Integer, ByRef strTipork As String) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      Return oCldScho.Grs1GetOrdList(strDittaCorrente, lOlprogr, strTipork, dsTmp)

      If dsTmp.Tables("ORDLIST").Rows.Count > 0 Then
        strTipork = "H"
        lOlprogr = NTSCInt(dsTmp.Tables("ORDLIST").Rows(0)!ol_progr)
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
  End Function

  Public Overridable Function IsDocRetail(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try
      Return oCldScho.IsDocRetail(strDitta, strTipork, nAnno, strSerie, lNumdoc)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function IsDocRetailNew(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try
      Return oCldScho.IsDocRetailNew(strDitta, strTipork, nAnno, strSerie, lNumdoc)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNORGSCHO.BNORGRS2.VB"
  Public Overridable Function Grs2ComponiStringa(ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Try
      '----------------------------------------------------------------------------------
      '--- Passa i parametri per la query

      oCldScho.Grs2ComponiStringa(strDittaCorrente, dtrTmp, ds, lSchoDaconto, lSchoAconto, _
                                 lSchoCommecaini, lSchoCommecafin, strSchoDadatord, strSchoAdatord, _
                                 strSchoDadatcons, strSchoAdatcons, strSchoTipork, strSchoSerie, _
                                 nSchoGruppo, nSchoSottogr, nSchoDaagente, nSchoAagente, _
                                 strSchoRilasciato, strSchoOrdin, _
                                 nSchoFaseini, nSchoFasefin, bSchoEvasi, _
                                 bModuloCRM, bIsCRMUser, strAccvis, _
                                 lCodorgaOperat, strRegvis, bAmm, strAltriFiltri, _
                                 strSchoClassLivello1, strSchoClassLivello2, strSchoClassLivello3, _
                                 strSchoClassLivello4, strSchoClassLivello5, _
                                 strSchoConto, nSchoCodlsel, strSchoCodart, nSchoCodlsar)

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

  Public Overridable Function Grs2Apri(ByRef ds As DataSet) As Boolean
    Try
      oCldScho.Grs2Apri(strDittaCorrente, ds, lSchoDaconto, lSchoAconto, _
                    lSchoCommecaini, lSchoCommecafin, strSchoDadatord, strSchoAdatord, _
                    strSchoDadatcons, strSchoAdatcons, strSchoTipork, strSchoSerie, _
                    nSchoGruppo, nSchoSottogr, nSchoDaagente, nSchoAagente, _
                    strSchoRilasciato, strSchoOrdin, nSchoDamagaz, nSchoAmagaz, _
                    strSchoDacodart, strSchoAcodart, nSchoFaseini, nSchoFasefin, _
                    bSchoEvasi, strGrs2Codcfam, bGrsoModTCO, nGrsoAnnotco, _
                    nGrsoCodstag, bModuloCRM, bIsCRMUser, strAccvis, _
                    lCodorgaOperat, strRegvis, bAmm, strAltriFiltri, _
                    strSchoClassLivello1, strSchoClassLivello2, strSchoClassLivello3, _
                    strSchoClassLivello4, strSchoClassLivello5, _
                    strSchoConto, nSchoCodlsel, strSchoCodart, nSchoCodlsar)

      If ds.Tables("MOVORD").Rows.Count = 0 Then
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

#Region "Filtri"
  Public Overridable Function GetTableStructMovIfil(ByRef dttTable As DataTable) As Boolean
    Try
      Return oCldScho.GetTableStructure("MOVIFIL", False, dttTable)
    Catch ex As Exception
      '----------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaFiltri(ByRef dttOut As DataTable) As Boolean
    Try
      If Not oCldScho.CaricaFiltri(strDittaCorrente, "BNORSCHO", dttOut) Then Return False

      dttOut.Rows.InsertAt(dttOut.NewRow, 0)
      dttOut.Rows(0)!cod = 0
      dttOut.Rows(0)!val = " -- Nessuno -- "

      Return True
    Catch ex As Exception
      '----------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiFiltro(ByVal lCod As Integer, ByVal strChild As String, ByVal strForm As String, ByRef dttOut As DataTable) As Boolean
    Try
      If Not oCldScho.LeggiFiltro(lCod, strChild, strForm, dttOut) Then Return False

      Return True
    Catch ex As Exception
      '----------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------------
    End Try
  End Function
#End Region
End Class
