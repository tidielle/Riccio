Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGSCHE
  Inherits CLE__BASN

  Public oCldSche As CLDMGSCHE
  Public oCleBoll As CLEVEBOLL = Nothing
  Public dttTabanaz As New DataTable
  Public bModTCO As Boolean = False

  Public strTTStloco As String
  Public lIITTStloco As Integer
  Public strTTStlocs As String
  Public lIITTStlocs As Integer
  Public strTTStschea As String
  Public lIITTStschea As Integer
  Public strTTStMatr As String
  Public lIITTStMatr As Integer
  Public strTTStMats As String
  Public lIITTStMats As Integer
  Public lIITTStlocu As Integer

  Public strDtulap As String

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

  'modali
  Public strAltriFiltri As String
  Public bScarStorico As Boolean
  Public strScarDatini As String
  Public strScarDatfin As String
  Public nScarDamagaz As Integer
  Public nScarAmagaz As Integer
  Public strScarDacodart As String
  Public strScarAcodart As String
  Public lScarDaconto As Integer
  Public lScarAconto As Integer
  Public nScarCodmarcini As Integer
  Public nScarCodmarcfin As Integer
  Public nScarFaseini As Integer
  Public nScarFasefin As Integer
  Public strScarTipodoc As String
  Public strScarSerie As String
  Public nScarCausale As Integer
  Public nScarGruppo As Integer
  Public nScarSottogr As Integer
  Public strScarClassLivello1 As String = ""
  Public strScarClassLivello2 As String = ""
  Public strScarClassLivello3 As String = ""
  Public strScarClassLivello4 As String = ""
  Public strScarClassLivello5 As String = ""
  Public nScarOrdin As Integer
  Public strScarOrdin As String
  Public lScarDacomme As Integer
  Public lScarAcomme As Integer
  Public lScarDalotto As Integer = 0 'obsoleto
  Public lScarAlotto As Integer = 999999999 'obsoleto
  Public strScarDalotto As String = ""
  Public strScarAlotto As String = ""
  Public bRigheInevase As Boolean
  Public bScarDaveboll As Boolean
  Public strScarCodart As String = ""
  Public nScarCodlsar As Integer = 0
  Public strScarConto As String = ""
  Public nScarCodlsel As Integer = 0

  '--- Flag per sapere se il programma è chiamato dall'estarno
  Public bScheDagest As Boolean
  Public bScheGestDaCons As Boolean
  Public bScheStampaSuGriglia As Boolean = False
  Public strScheTipork As String
  Public strScheDatini As String
  Public strScheDatfin As String
  Public bScheFiltri As Boolean
  Public nScheFase As Integer
  Public strScheCodart As String
  Public nScheMagaz As Integer
  Public lScheConto As Integer
  Public strScheOrdin As String

  Public bLogisticaEstesa As Boolean

  'grsc
  Public dsGridShared As DataSet
  Public dsGrscShared As DataSet
  Public dEsist() As Decimal
  Public strDatini As String
  Public strDatinimeno1 As String
  Public strDatfin As String
  Public strGrscCodcfam As String
  Public nGrscAnnotco As Integer
  Public nGrscCodstag As Integer
  Public bGrscSaldiIniziali As Boolean
  Public bGrscArticoloTCO As Boolean = False
  Public bGrscArticoloMatricola As Boolean = False

  'grnp
  Public strGrnpCodcfam As String
  Public nGrnpAnnotco As Integer
  Public nGrnpCodstag As Integer

  'grma
  Public strGrmaCodcfam As String
  Public nGrmaAnnotco As Integer
  Public nGrmaCodstag As Integer
  Public strGrmaDamatric As String
  Public strGrmaAmatric As String

  Public bLottoNew As Boolean = False     'se true (letto da anaditac) alo_lotto è calcolato sempre in automatico, se false alo_lotto è uguale a alo_lottox e alo_lottx deve essere numerico di max 9 char

  Private Moduli_P As Integer = bsModMG + bsModVE
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGSCHE"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldSche = CType(MyBase.ocldBase, CLDMGSCHE)
    oCldSche.Init(oApp)
    Return True
  End Function

  Public Overridable Function LeggiDatiDitta(ByVal strDitta As String) As Boolean
    Try
      oCldSche.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttTabanaz)
      strDittaCorrente = strDitta

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

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABSTAG", "N", strDescr) Then
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

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABGMER", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128783198935965862, "Codice gruppo |'" & nCod.ToString & "'| inesistente")))
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

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABSGME", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128783198976435130, "Codice sottogruppo |'" & nCod.ToString & "'| inesistente")))
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

      If Not oCldSche.ValCodiceDb(strCod, strDittaCorrente, "TABCFAM", "S", strDescr) Then
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
  Public Overridable Function edCausale_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABCAUM", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128866743976899168, "Codice causale |'" & nCod.ToString & "'| inesistente")))
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
      If oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLSAR", "N", strDescr) = False Then
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
      If oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLSEL", "N", strDescr, dttTmp) = False Then
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

  Public Overridable Function RiempiTTStloco(ByVal bSuTTlocu As Boolean, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByRef strError As String, ByVal strQuery As String) As Boolean
    Try
      Return oCldSche.RiempiTTStloco(strDittaCorrente, bSuTTlocu, lIITTStlocu, lIITTStloco, _
                                     strTTStloco, stredDatini, stredDatfin, stredDamagaz, _
                                     stredAmagaz, stredDacodart, stredAcodart, stredDaconto, _
                                     stredAconto, stredFaseini, stredFasefin, bckStorico, _
                                     bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                     bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                     bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                     bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                                     stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                                     stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                     stredAnnotco, stredCodstag, NTSCStr(dttTabanaz.Rows(0)!tb_dtulap), bModuloCRM, _
                                     bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                     bAmm, strError, strQuery)
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
  Public Overridable Function RiempiTTStloco(ByVal bSuTTlocu As Boolean, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByRef strError As String, ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bSuTTlocu, stredDatini, stredDatfin, stredDamagaz, stredAmagaz, _
                                             stredDacodart, stredAcodart, stredDaconto, stredAconto, _
                                             stredFaseini, stredFasefin, bckStorico, bopLapchiu, bopLaperti, _
                                             stredInizio, stredFine, bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                             bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, bopPrelevaSolo, _
                                             strcbTipodoc, bckSerie, stredSerie, stredCausale, stredGruppo, stredSottogr, _
                                             stredCodmarcini, stredCodmarcfin, stredCodcfam, bckSelAnnoStag, _
                                             stredAnnotco, stredCodstag, strError, strQuery, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(37))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return RiempiTTStloco(bSuTTlocu, stredDatini, stredDatfin, stredDamagaz, stredAmagaz, stredDacodart, stredAcodart, _
                            stredDaconto, stredAconto, stredFaseini, stredFasefin, bckStorico, bopLapchiu, bopLaperti, _
                            stredInizio, stredFine, bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                            bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, _
                            bckSerie, stredSerie, stredCausale, stredGruppo, stredSottogr, _
                            stredCodmarcini, stredCodmarcfin, stredCodcfam, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                            strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                            strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                            "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function
  Public Overridable Function RiempiTTStloco(ByVal bSuTTlocu As Boolean, ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByRef strError As String, ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String, _
                                             ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                             ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bSuTTlocu, stredDatini, stredDatfin, stredDamagaz, stredAmagaz, _
                                             stredDacodart, stredAcodart, stredDaconto, stredAconto, _
                                             stredFaseini, stredFasefin, bckStorico, bopLapchiu, bopLaperti, _
                                             stredInizio, stredFine, bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                             bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, bopPrelevaSolo, _
                                             strcbTipodoc, bckSerie, stredSerie, stredCausale, stredGruppo, stredSottogr, _
                                             stredCodmarcini, stredCodmarcfin, stredCodcfam, bckSelAnnoStag, _
                                             stredAnnotco, stredCodstag, strError, strQuery, _
                                             strClassificazioneLivello1, strClassificazioneLivello2, _
                                             strClassificazioneLivello3, strClassificazioneLivello4, _
                                             strClassificazioneLivello5, strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(37))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return oCldSche.RiempiTTStloco(strDittaCorrente, bSuTTlocu, lIITTStlocu, lIITTStloco, _
                                     strTTStloco, stredDatini, stredDatfin, stredDamagaz, _
                                     stredAmagaz, stredDacodart, stredAcodart, stredDaconto, _
                                     stredAconto, stredFaseini, stredFasefin, bckStorico, _
                                     bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                     bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                     bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                     bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                                     stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                                     stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                     stredAnnotco, stredCodstag, NTSCStr(dttTabanaz.Rows(0)!tb_dtulap), bModuloCRM, _
                                     bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                     bAmm, strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                     strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                     strCodart, nCodlsar, strConto, nCodlsel)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function
  Public Overridable Function RiempiTTStlocs(ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal bopLsaldo As Boolean, _
                                             ByVal bopCsaldo As Boolean, ByVal bopUsaldo As Boolean, _
                                             ByRef strError As String, ByVal strQuery As String) As Boolean
    Try
      Return oCldSche.RiempiTTStlocs(strDittaCorrente, lIITTStlocs, _
                                      strTTStlocs, stredDatini, stredDatfin, stredDamagaz, _
                                      stredAmagaz, stredDacodart, stredAcodart, stredDaconto, _
                                      stredAconto, stredFaseini, stredFasefin, bckStorico, _
                                      bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                      bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                      bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                      bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                                      stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                                      stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                      stredAnnotco, stredCodstag, NTSCStr(dttTabanaz.Rows(0)!tb_dtulap), bModuloCRM, _
                                      bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                      bAmm, bopLsaldo, bopCsaldo, bopUsaldo, _
                                      strError, strQuery, strScarDalotto, strScarAlotto)
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
  Public Overridable Function RiempiTTStlocs(ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal bopLsaldo As Boolean, _
                                             ByVal bopCsaldo As Boolean, ByVal bopUsaldo As Boolean, _
                                             ByRef strError As String, ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldSche.RiempiTTStlocs(strDittaCorrente, lIITTStlocs, _
                                      strTTStlocs, stredDatini, stredDatfin, stredDamagaz, _
                                      stredAmagaz, stredDacodart, stredAcodart, stredDaconto, _
                                      stredAconto, stredFaseini, stredFasefin, bckStorico, _
                                      bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                      bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                      bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                      bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                                      stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                                      stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                      stredAnnotco, stredCodstag, NTSCStr(dttTabanaz.Rows(0)!tb_dtulap), bModuloCRM, _
                                      bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                      bAmm, bopLsaldo, bopCsaldo, bopUsaldo, _
                                      strError, strQuery, strScarDalotto, strScarAlotto, _
                                      strClassificazioneLivello1, strClassificazioneLivello2, _
                                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)
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
  Public Overridable Function RiempiTTStlocs(ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal bopLsaldo As Boolean, _
                                             ByVal bopCsaldo As Boolean, ByVal bopUsaldo As Boolean, _
                                             ByRef strError As String, ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String, _
                                             ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                             ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldSche.RiempiTTStlocs(strDittaCorrente, lIITTStlocs, _
                                      strTTStlocs, stredDatini, stredDatfin, stredDamagaz, _
                                      stredAmagaz, stredDacodart, stredAcodart, stredDaconto, _
                                      stredAconto, stredFaseini, stredFasefin, bckStorico, _
                                      bopLapchiu, bopLaperti, stredInizio, stredFine, _
                                      bopCapchiu, bopCaperte, stredDacomme, stredAcomme, _
                                      bopUapchiu, bopUaperte, stredUbicazini, stredUbicazfin, _
                                      bopPrelevaSolo, strcbTipodoc, bckSerie, stredSerie, _
                                      stredCausale, stredGruppo, stredSottogr, stredCodmarcini, _
                                      stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                      stredAnnotco, stredCodstag, NTSCStr(dttTabanaz.Rows(0)!tb_dtulap), bModuloCRM, _
                                      bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                      bAmm, bopLsaldo, bopCsaldo, bopUsaldo, _
                                      strError, strQuery, strScarDalotto, strScarAlotto, _
                                      strClassificazioneLivello1, strClassificazioneLivello2, _
                                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                      strCodart, nCodlsar, strConto, nCodlsel)
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

  Public Overridable Function SaldoStorico(ByVal strCodart As String, ByVal strMagaz As String, _
                                         ByVal strFase As String, ByVal strDatini As String, _
                                         ByVal strData As String, ByRef ds As DataSet) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

      Return oCldSche.SaldoStorico(strDittaCorrente, strCodart, strMagaz, strFase, _
                                             strDatini, strData, ds)

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
  Public Overridable Function SaldoStorico(ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal bckSaldiIniziali As Boolean, ByRef strError As String, _
                                             ByVal strQuery As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

      Return oCldSche.SaldoStorico(strDittaCorrente, lIITTStschea, strTTStschea, stredDatini, _
                                             stredDatfin, stredDamagaz, stredAmagaz, stredDacodart, _
                                             stredAcodart, stredDaconto, stredAconto, stredFaseini, _
                                             stredFasefin, bckStorico, bopLapchiu, bopLaperti, _
                                             stredInizio, stredFine, bopCapchiu, bopCaperte, _
                                             stredDacomme, stredAcomme, bopUapchiu, bopUaperte, _
                                             stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, _
                                             bckSerie, stredSerie, stredCausale, stredGruppo, _
                                             stredSottogr, stredCodmarcini, stredCodmarcfin, stredCodcfam, _
                                             bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                             strDtulap, bModuloCRM, bIsCRMUser, strAccvis, _
                                             lCodorgaOperat, strRegvis, bAmm, bckSaldiIniziali, _
                                             strError, strQuery)
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
  Public Overridable Function SaldoStorico(ByVal stredDatini As String, _
                                           ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                           ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                           ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                           ByVal stredAconto As String, ByVal stredFaseini As String, _
                                           ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                           ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                           ByVal stredInizio As String, ByVal stredFine As String, _
                                           ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                           ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                           ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                           ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                           ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                           ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                           ByVal stredCausale As String, ByVal stredGruppo As String, _
                                           ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                           ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                           ByVal bckSelAnnoStag As Boolean, _
                                           ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                           ByVal bckSaldiIniziali As Boolean, ByRef strError As String, _
                                           ByVal strQuery As String, _
                                           ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                           ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                           ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

      Return oCldSche.SaldoStorico(strDittaCorrente, lIITTStschea, strTTStschea, stredDatini, _
                                   stredDatfin, stredDamagaz, stredAmagaz, stredDacodart, _
                                   stredAcodart, stredDaconto, stredAconto, stredFaseini, _
                                   stredFasefin, bckStorico, bopLapchiu, bopLaperti, _
                                   stredInizio, stredFine, bopCapchiu, bopCaperte, _
                                   stredDacomme, stredAcomme, bopUapchiu, bopUaperte, _
                                   stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, _
                                   bckSerie, stredSerie, stredCausale, stredGruppo, _
                                   stredSottogr, stredCodmarcini, stredCodmarcfin, stredCodcfam, _
                                   bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                   strDtulap, bModuloCRM, bIsCRMUser, strAccvis, _
                                   lCodorgaOperat, strRegvis, bAmm, bckSaldiIniziali, _
                                   strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                   strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)
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
  Public Overridable Function SaldoStorico(ByVal stredDatini As String, _
                                           ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                           ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                           ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                           ByVal stredAconto As String, ByVal stredFaseini As String, _
                                           ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                           ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                           ByVal stredInizio As String, ByVal stredFine As String, _
                                           ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                           ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                           ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                           ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                           ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                           ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                           ByVal stredCausale As String, ByVal stredGruppo As String, _
                                           ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                           ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                           ByVal bckSelAnnoStag As Boolean, _
                                           ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                           ByVal bckSaldiIniziali As Boolean, ByRef strError As String, _
                                           ByVal strQuery As String, _
                                           ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                           ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                           ByVal strClassificazioneLivello5 As String, _
                                           ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                           ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldSche.SaldoStorico(strDittaCorrente, lIITTStschea, strTTStschea, stredDatini, stredDatfin, _
                                   stredDamagaz, stredAmagaz, stredDacodart, stredAcodart, stredDaconto, stredAconto, _
                                   stredFaseini, stredFasefin, bckStorico, bopLapchiu, bopLaperti, stredInizio, _
                                   stredFine, bopCapchiu, bopCaperte, stredDacomme, stredAcomme, bopUapchiu, _
                                   bopUaperte, stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, _
                                   bckSerie, stredSerie, stredCausale, stredGruppo, stredSottogr, _
                                   stredCodmarcini, stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                   stredAnnotco, stredCodstag, strDtulap, bModuloCRM, bIsCRMUser, strAccvis, _
                                   lCodorgaOperat, strRegvis, bAmm, bckSaldiIniziali, strError, strQuery, _
                                   strClassificazioneLivello1, strClassificazioneLivello2, _
                                   strClassificazioneLivello3, strClassificazioneLivello4, _
                                   strClassificazioneLivello5, strCodart, nCodlsar, strConto, nCodlsel)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function
  Public Overridable Function SaldoStoricoNonMov(ByVal stredDatini As String, _
                                             ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                             ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                             ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                             ByVal stredAconto As String, ByVal stredFaseini As String, _
                                             ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                             ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                             ByVal stredInizio As String, ByVal stredFine As String, _
                                             ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                             ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                             ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                             ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                             ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                             ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                             ByVal stredCausale As String, ByVal stredGruppo As String, _
                                             ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                             ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                             ByVal bckSelAnnoStag As Boolean, _
                                             ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                             ByVal bckSaldiIniziali As Boolean, _
                                             ByVal bopEsist As Boolean, ByRef strError As String, _
                                             ByVal strQuery As String) As Boolean
    Try
      Return oCldSche.SaldoStoricoNonMov(strDittaCorrente, lIITTStschea, strTTStschea, stredDatini, _
                                       stredDatfin, stredDamagaz, stredAmagaz, stredDacodart, _
                                       stredAcodart, stredDaconto, stredAconto, stredFaseini, _
                                       stredFasefin, bckStorico, bopLapchiu, bopLaperti, _
                                       stredInizio, stredFine, bopCapchiu, bopCaperte, _
                                       stredDacomme, stredAcomme, bopUapchiu, bopUaperte, _
                                       stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, _
                                       bckSerie, stredSerie, stredCausale, stredGruppo, _
                                       stredSottogr, stredCodmarcini, stredCodmarcfin, stredCodcfam, _
                                       bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                       strDtulap, bModuloCRM, bIsCRMUser, strAccvis, _
                                       lCodorgaOperat, strRegvis, bAmm, bckSaldiIniziali, _
                                       bopEsist, strError, strQuery)
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
  Public Overridable Function SaldoStoricoNonMov(ByVal stredDatini As String, _
                                            ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                            ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                            ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                            ByVal stredAconto As String, ByVal stredFaseini As String, _
                                            ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                            ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                            ByVal stredInizio As String, ByVal stredFine As String, _
                                            ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                            ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                            ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                            ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                            ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                            ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                            ByVal stredCausale As String, ByVal stredGruppo As String, _
                                            ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                            ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                            ByVal bckSelAnnoStag As Boolean, _
                                            ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                            ByVal bckSaldiIniziali As Boolean, _
                                            ByVal bopEsist As Boolean, ByRef strError As String, _
                                            ByVal strQuery As String, _
                                            ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                            ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                            ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldSche.SaldoStoricoNonMov(strDittaCorrente, lIITTStschea, strTTStschea, stredDatini, _
                                       stredDatfin, stredDamagaz, stredAmagaz, stredDacodart, _
                                       stredAcodart, stredDaconto, stredAconto, stredFaseini, _
                                       stredFasefin, bckStorico, bopLapchiu, bopLaperti, _
                                       stredInizio, stredFine, bopCapchiu, bopCaperte, _
                                       stredDacomme, stredAcomme, bopUapchiu, bopUaperte, _
                                       stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, _
                                       bckSerie, stredSerie, stredCausale, stredGruppo, _
                                       stredSottogr, stredCodmarcini, stredCodmarcfin, stredCodcfam, _
                                       bModTCO, bckSelAnnoStag, stredAnnotco, stredCodstag, _
                                       strDtulap, bModuloCRM, bIsCRMUser, strAccvis, _
                                       lCodorgaOperat, strRegvis, bAmm, bckSaldiIniziali, _
                                       bopEsist, strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                       strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)
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
  Public Overridable Function SaldoStoricoNonMov(ByVal stredDatini As String, _
                                                 ByVal stredDatfin As String, ByVal stredDamagaz As String, _
                                                 ByVal stredAmagaz As String, ByVal stredDacodart As String, _
                                                 ByVal stredAcodart As String, ByVal stredDaconto As String, _
                                                 ByVal stredAconto As String, ByVal stredFaseini As String, _
                                                 ByVal stredFasefin As String, ByVal bckStorico As Boolean, _
                                                 ByVal bopLapchiu As Boolean, ByVal bopLaperti As Boolean, _
                                                 ByVal stredInizio As String, ByVal stredFine As String, _
                                                 ByVal bopCapchiu As Boolean, ByVal bopCaperte As Boolean, _
                                                 ByVal stredDacomme As String, ByVal stredAcomme As String, _
                                                 ByVal bopUapchiu As Boolean, ByVal bopUaperte As Boolean, _
                                                 ByVal stredUbicazini As String, ByVal stredUbicazfin As String, _
                                                 ByVal bopPrelevaSolo As Boolean, ByVal strcbTipodoc As String, _
                                                 ByVal bckSerie As Boolean, ByVal stredSerie As String, _
                                                 ByVal stredCausale As String, ByVal stredGruppo As String, _
                                                 ByVal stredSottogr As String, ByVal stredCodmarcini As String, _
                                                 ByVal stredCodmarcfin As String, ByVal stredCodcfam As String, _
                                                 ByVal bckSelAnnoStag As Boolean, _
                                                 ByVal stredAnnotco As String, ByVal stredCodstag As String, _
                                                 ByVal bckSaldiIniziali As Boolean, _
                                                 ByVal bopEsist As Boolean, ByRef strError As String, _
                                                 ByVal strQuery As String, _
                                                 ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                                 ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                                 ByVal strClassificazioneLivello5 As String, _
                                                 ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                                 ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldSche.SaldoStoricoNonMov(strDittaCorrente, lIITTStschea, strTTStschea, stredDatini, stredDatfin, _
                                         stredDamagaz, stredAmagaz, stredDacodart, stredAcodart, _
                                         stredDaconto, stredAconto, stredFaseini, stredFasefin, bckStorico, _
                                         bopLapchiu, bopLaperti, stredInizio, stredFine, bopCapchiu, bopCaperte, _
                                         stredDacomme, stredAcomme, bopUapchiu, bopUaperte, _
                                         stredUbicazini, stredUbicazfin, bopPrelevaSolo, strcbTipodoc, bckSerie, _
                                         stredSerie, stredCausale, stredGruppo, stredSottogr, _
                                         stredCodmarcini, stredCodmarcfin, stredCodcfam, bModTCO, bckSelAnnoStag, _
                                         stredAnnotco, stredCodstag, strDtulap, bModuloCRM, bIsCRMUser, strAccvis, _
                                         lCodorgaOperat, strRegvis, bAmm, bckSaldiIniziali, bopEsist, strError, _
                                         strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                         strClassificazioneLivello3, strClassificazioneLivello4, _
                                         strClassificazioneLivello5, strCodart, nCodlsar, strConto, nCodlsel)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function

  Public Overridable Function CheckArtico(ByVal strCodart As String) As Boolean
    Try
      If Not oCldSche.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "") Then
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
      Return False
    End Try
  End Function

  Public Overridable Function RiempiTTStMatr(ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                            ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                            ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                            ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                            ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                            ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                            ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                            ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                            ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                            ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                            ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                            ByVal strQuery As String) As Boolean
    Try
      Return CType(oCleComm, CLELBMENU).RiempiTTStMatr(strDittaCorrente, strTTStMatr, lIITTStMatr, _
                                      strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                      nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                      nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                      strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                      nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                      nSTMQualeMatr, strError, strQuery)

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
  Public Overridable Function RiempiTTStMatr(ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                            ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                            ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                            ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                            ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                            ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                            ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                            ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                            ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                            ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                            ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                            ByVal strQuery As String, _
                                            ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                            ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                            ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return CType(oCleComm, CLELBMENU).RiempiTTStMatr(strDittaCorrente, strTTStMatr, lIITTStMatr, _
                                      strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                      nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                      nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                      strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                      nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                      nSTMQualeMatr, strError, strQuery, _
                                      strClassificazioneLivello1, strClassificazioneLivello2, _
                                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)

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
  Public Overridable Function RiempiTTStMatr(ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                             ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                             ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                             ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                             ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                             ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                             ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                             ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                             ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                             ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                             ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                             ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String, _
                                             ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                             ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return CType(oCleComm, CLELBMENU).RiempiTTStMatr(strDittaCorrente, strTTStMatr, lIITTStMatr, _
                                                       strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                                       nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                                       nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                                       strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                                       nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                                       nSTMQualeMatr, strError, strQuery, _
                                                       strClassificazioneLivello1, strClassificazioneLivello2, _
                                                       strClassificazioneLivello3, strClassificazioneLivello4, _
                                                       strClassificazioneLivello5, _
                                                       strCodart, nCodlsar, strConto, nCodlsel)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function
  Public Overridable Function RiempiTTStMats(ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                              ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                              ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                              ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                              ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                              ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                              ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                              ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                              ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                              ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                              ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                              ByVal strQuery As String) As Boolean
    Try
      Return CType(oCleComm, CLELBMENU).RiempiTTStMats(strDittaCorrente, strTTStMats, lIITTStMats, _
                                      strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                      nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                      nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                      strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                      nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                      nSTMQualeMatr, strError, strQuery)

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
  Public Overridable Function RiempiTTStMats(ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                             ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                             ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                             ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                             ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                             ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                             ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                             ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                             ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                             ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                             ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                             ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return CType(oCleComm, CLELBMENU).RiempiTTStMats(strDittaCorrente, strTTStMats, lIITTStMats, _
                                      strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                      nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                      nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                      strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                      nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                      nSTMQualeMatr, strError, strQuery, _
                                      strClassificazioneLivello1, strClassificazioneLivello2, _
                                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)

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
  Public Overridable Function RiempiTTStMats(ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                             ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                             ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                             ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                             ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                             ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                             ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                             ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                             ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                             ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                             ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                             ByVal strQuery As String, _
                                             ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String, _
                                             ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                             ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return CType(oCleComm, CLELBMENU).RiempiTTStMats(strDittaCorrente, strTTStMats, lIITTStMats, _
                                                       strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                                       nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                                       nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                                       strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                                       nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                                       nSTMQualeMatr, strError, strQuery, _
                                                       strClassificazioneLivello1, strClassificazioneLivello2, _
                                                       strClassificazioneLivello3, strClassificazioneLivello4, _
                                                       strClassificazioneLivello5, _
                                                       strCodart, nCodlsar, strConto, nCodlsel)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function

  Public Overridable Function TrovaFmtPrz(Optional ByVal nCodvalu As Integer = 0) As String
    Dim i As Integer
    Try
      TrovaFmtPrz = "#,##0"
      If nCodvalu = 0 Then
        If oApp.NDecPrzUn > 0 Then TrovaFmtPrz = TrovaFmtPrz & "."
        For i = 1 To oApp.NDecPrzUn
          TrovaFmtPrz = TrovaFmtPrz & "0"
        Next
      Else
        If oApp.NDecPrzUnVal > 0 Then
          TrovaFmtPrz = TrovaFmtPrz & "."
          For i = 1 To oApp.NDecPrzUnVal
            TrovaFmtPrz = TrovaFmtPrz & "0"
          Next
        End If
      End If

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CheckEsistCollaudo(ByVal strTipork As String, ByVal nAnno As Integer, _
      ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lRiga As Integer) As Boolean

    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldSche.CheckEsistCollaudo(strDittaCorrente, strTipork, nAnno, strSerie, lNumdoc, lRiga)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function SoloNonMovimentati(ByVal bNoteDiPrelievo As Boolean, _
    ByVal strNonMovimentati As String, ByVal bPerMagazzino As Boolean, _
    ByVal bTTSTSCHEA As Boolean, ByVal bTTSTMATR As Boolean, ByVal bTTSTLOCO As Boolean) As Boolean

    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldSche.SoloNonMovimentati(strDittaCorrente, bNoteDiPrelievo, strNonMovimentati, bPerMagazzino, _
       bTTSTSCHEA, bTTSTMATR, bTTSTLOCO, _
       lIITTStschea, lIITTStMatr, lIITTStMats, lIITTStloco, lIITTStlocs, lIITTStlocu)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function RitornaLISTSAR(ByVal nCodlsar As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldSche.RitornaLISTSAR(strDittaCorrente, nCodlsar, dttOut)
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
      Return oCldSche.RitornaLISTSEL(strDittaCorrente, nCodlsel, dttOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function SvuotaTmpTable() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCldSche.ResetTblInstId("TTSTSCHEA", False, lIITTStschea)
      oCldSche.ResetTblInstId("TTSTLOCO", False, lIITTStloco)
      oCldSche.ResetTblInstId("TTSTLOCS", False, lIITTStlocs)
      oCldSche.ResetTblInstId("TTSTMATR", False, lIITTStMatr)
      oCldSche.ResetTblInstId("TTSTMATS", False, lIITTStMats)
      oCldSche.ResetTblInstId("TTSTLOCU", False, lIITTStlocu)
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

#Region "funzioni specifiche per BNMGSCHE.BNMGGRSC.VB"
  Public Overridable Function CheckArticotaglie(ByVal strCodart As String) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCldSche.CheckArticotaglie(strDittaCorrente, strCodart, dsTmp)

      If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128783199010498066, "L'articolo non è gestito per taglie e colori." & vbCrLf & _
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
      oCldSche.GetRelease(dsTmp)

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
      lbArticolo_Validated(strCod, strDescr, Nothing)
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
  Public Overridable Function lbArticolo_Validated(ByVal strCod As String, ByRef strDescr As String, ByRef dttOut As DataTable) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCod, strDescr, dttOut})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strDescr = NTSCStr(oIn(1))
        dttOut = CType(oIn(2), DataTable)
        Return CBool(oOut)
      End If
      '----------------


      dttOut = New DataTable
      '--------------------------------------------------------------------------------------------------------------
      bGrscArticoloTCO = False
      bGrscArticoloMatricola = False
      '--------------------------------------------------------------------------------------------------------------
      If strCod.Trim = "" Then
        strDescr = ""
        Return True
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldSche.ValCodiceDb(strCod, strDittaCorrente, "ARTICO", "S", strDescr, dttOut) = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      With dttOut.Rows(0)
        If (NTSCStr(!ar_gesvar) = "S") And (NTSCInt(!ar_codtagl) <> 0) And _
           (NTSCInt(!ar_anno) <> 0) And (NTSCInt(!ar_codstag) <> 0) Then
          bGrscArticoloTCO = True
        End If
        If NTSCStr(!ar_gestmatr) = "S" Then bGrscArticoloMatricola = True
      End With
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
  Public Overridable Function lbConto_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "ANAGRA", "N", strDescr) Then
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

  Public Overridable Function ComponiStringaSQL(ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Dim dsTmp As DataSet = Nothing
    Dim nDaGruppo As Integer
    Dim nAGruppo As Integer
    Dim nDaSottogr As Integer
    Dim nASottogr As Integer
    Dim lDaConto As Integer
    Dim lAConto As Integer
    Dim nDaCausale As Integer
    Dim nACausale As Integer
    Dim strDatainizio As String
    Dim strDatafine As String
    Try
      '----------------------------------------------------------------------------------
      '--- Passa i parametri per la StoreProcedure
      strDatainizio = strScarDatini
      strDatafine = strScarDatfin

      If strScarOrdin = "C" Then
        lDaConto = NTSCInt(dtrTmp!km_conto)
        lAConto = NTSCInt(dtrTmp!km_conto)
      Else
        lDaConto = lScarDaconto
        lAConto = lScarAconto
      End If
      If nScarCausale > 0 Then
        nDaCausale = nScarCausale
        nACausale = nScarCausale
      Else
        nDaCausale = 0
        nACausale = 999
      End If
      If nScarGruppo > 0 Then
        nDaGruppo = nScarGruppo
        nAGruppo = nScarGruppo
      Else
        nDaGruppo = 0
        nAGruppo = 99
      End If
      If nScarSottogr > 0 Then
        nDaSottogr = nScarSottogr
        nASottogr = nScarSottogr
      Else
        nDaSottogr = 0
        nASottogr = 9999
      End If

      oCldSche.ComponiStringaSQL(strDittaCorrente, dtrTmp, ds, lDaConto, lAConto, _
                                lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                                strDatainizio, strDatafine, strScarTipodoc, strScarSerie, _
                                nDaCausale, nACausale, nDaGruppo, nAGruppo, _
                                nDaSottogr, nASottogr, strScarOrdin, nScarCodmarcini, _
                                nScarCodmarcfin, strAltriFiltri, strScarDalotto, strScarAlotto, _
                                strScarClassLivello1, strScarClassLivello2, strScarClassLivello3, _
                                strScarClassLivello4, strScarClassLivello5, strScarConto, nScarCodlsel)

      dsGridShared = ds
      '--------------------------------------------------------------------------------------------------------------
      AddHandler ds.Tables("MOVMAG").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler ds.Tables("MOVMAG").ColumnChanged, AddressOf AfterColUpdate
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
    End Try
  End Function

  Public Overridable Function ComponiStringa(ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCldSche.ComponiStringa(strDittaCorrente, dtrTmp, ds, lScarDaconto, lScarAconto, _
                              strScarTipodoc, strScarSerie, nScarGruppo, nScarSottogr, _
                              strScarOrdin, lScarDalotto, lScarAlotto, lScarDacomme, _
                              lScarAcomme, strDatini, strDatfin, nScarCausale, _
                              nScarCodmarcini, nScarCodmarcfin, bModuloCRM, _
                              bIsCRMUser, strAccvis, lCodorgaOperat, _
                              strRegvis, bAmm, strAltriFiltri, strScarDalotto, strScarAlotto, _
                              strScarClassLivello1, strScarClassLivello2, strScarClassLivello3, _
                              strScarClassLivello4, strScarClassLivello5, _
                              strScarCodart, nScarCodlsar, strScarConto, nScarCodlsel)
      '--------------------------------------------------------------------------------------------------------------
      dsGridShared = ds
      '--------------------------------------------------------------------------------------------------------------
      AddHandler ds.Tables("MOVMAG").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler ds.Tables("MOVMAG").ColumnChanged, AddressOf AfterColUpdate
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

  Public Overridable Function Apri(ByRef ds As DataSet, ByRef strError As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCldSche.Apri(strDittaCorrente, strScarOrdin, strDatini, strDatfin, _
                    nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                    strScarDacodart, strScarAcodart, lScarDacomme, lScarAcomme, _
                    lScarDalotto, lScarAlotto, nScarFaseini, nScarFasefin, _
                    strScarTipodoc, strScarSerie, nScarCausale, nScarGruppo, _
                    nScarSottogr, nScarCodmarcini, nScarCodmarcfin, strGrscCodcfam, _
                    nGrscAnnotco, nGrscCodstag, bModuloCRM, bIsCRMUser, _
                    strAccvis, bAmm, lCodorgaOperat, strRegvis, _
                    bScarDaveboll, strError, ds, strAltriFiltri, _
                    strScarDalotto, strScarAlotto, strScarClassLivello1, _
                    strScarClassLivello2, strScarClassLivello3, strScarClassLivello4, _
                    strScarClassLivello5, strScarCodart, nScarCodlsar, strScarConto, nScarCodlsel)
      dsGrscShared = ds
      '--------------------------------------------------------------------------------------------------------------
      If ds.Tables("MOVMAG").Rows.Count = 0 Then
        strError = "Non esistono dati con queste caratteristiche."
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '----- per BSVEBOLL
      '--------------------------------------------------------------------------------------------------------------
      If ds.Tables("MOVMAG").Rows.Count = 0 And bScarDaveboll Then
        strError = "Non esistono precedenti movimenti riguardanti il Conto, l'Articolo e il Magazzino selezionati."
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

  Public Overridable Function GetTaglie(ByVal strCodart As String, ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GetTaglie(strDittaCorrente, strCodart, ds)

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

  Public Overridable Function IsDocRetail(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try
      Return oCldSche.IsDocRetail(strDitta, strTipork, nAnno, strSerie, lNumdoc)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function IsDocRetailNew(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try
      Return oCldSche.IsDocRetailNew(strDitta, strTipork, nAnno, strSerie, lNumdoc)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArtdef(ByVal strCodart As String, ByVal strMagaz As String, _
                                        ByVal strFase As String, ByVal nOrdin As Integer, _
                                        ByRef ds As DataSet) As Boolean
    Try
      Return oCldSche.GetArtdef(strDittaCorrente, strCodart, strMagaz, strFase, _
                                nOrdin, ds)
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

  Public Overridable Function CalcolaRettificheElab(ByRef strRettificheAcq As String, ByRef strRettificheVen As String, _
                                                ByVal dtrTmp As DataRow) As Boolean
    Dim ds1 As DataSet = Nothing
    Dim ds2 As DataSet = Nothing
    Try
      oCldSche.CalcolaRettifiche(strDittaCorrente, strScarOrdin, strScarDatini, strScarDatfin, _
                                lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                                lScarDaconto, lScarAconto, strScarTipodoc, strScarSerie, _
                                nScarCausale, nScarGruppo, nScarSottogr, nScarCodmarcini, _
                                nScarCodmarcfin, dtrTmp, ds1, ds2, strScarDalotto, strScarAlotto, _
                                strScarClassLivello1, strScarClassLivello2, strScarClassLivello3, _
                                strScarClassLivello4, strScarClassLivello5, _
                                strScarCodart, nScarCodlsar, strScarConto, nScarCodlsel)
      If ds1 Is Nothing Then
        strRettificheAcq = "0"
      Else
        If ds1.Tables("TESTMAG").Rows.Count = 0 Then
          strRettificheAcq = "0"
        Else
          If NTSCInt(ds1.Tables("TESTMAG").Rows(0)!VALORE) = 0 Then
            strRettificheAcq = "0"
          Else
            strRettificheAcq = NTSCStr(ds1.Tables("TESTMAG").Rows(0)!VALORE)
          End If
        End If
      End If

      If ds2 Is Nothing Then
        strRettificheVen = "0"
      Else
        If ds2.Tables("TESTMAG").Rows.Count = 0 Then
          strRettificheVen = "0"
        Else
          If NTSCInt(ds2.Tables("TESTMAG").Rows(0)!VALORE) = 0 Then
            strRettificheVen = "0"
          Else
            strRettificheVen = NTSCStr(ds2.Tables("TESTMAG").Rows(0)!VALORE)
          End If
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

  Public Overridable Function GetCollaudi(ByVal strTipork As String, ByVal strAnno As String, _
                                          ByVal strSerie As String, ByVal strNumdoc As String, _
                                          ByRef ds As DataSet) As Boolean
    Try
      Return oCldSche.GetCollaudi(strDittaCorrente, strTipork, strAnno, strSerie, strNumdoc, ds)
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

  Public Overridable Sub SaldoEsistenza(ByRef stredEsistfi As String, ByRef stredEsistpr As String, _
                                        ByRef stredTotvcarichi As String, ByRef stredTotvscarichi As String, _
                                        ByRef stredTotcarichi As String, ByRef stredTotscarichi As String, _
                                        ByVal ndcGrscPos As Integer)
    Dim strData As String
    Dim lGiorni As Integer
    Try
      If NTSCDec(dsGrscShared.Tables("MOVMAG").Rows.Count) = 0 Then
        stredEsistfi = "0"
        stredEsistpr = "0"
        stredTotvcarichi = "0"
        stredTotvscarichi = "0"
        stredTotcarichi = "0"
        stredTotscarichi = "0"
        Exit Sub
      End If
      If bScarStorico = False Then
        NoSaldoStorico(ndcGrscPos, stredEsistpr, stredTotvcarichi, stredTotvscarichi, _
                       stredTotcarichi, stredTotscarichi, stredEsistfi)
      Else
        strData = NTSCStr(DateAdd("d", 1, strDtulap))
        lGiorni = NTSCInt(DateDiff("d", NTSCDate(strData), NTSCStr(strScarDatini)))
        If lGiorni <> 0 Then
          If lGiorni < 0 Then 'data inizio anteriore a data ult agg + 1
            SaldoStoricoPrecedente(stredEsistpr, stredEsistfi, stredTotvcarichi, stredTotvscarichi, _
                                   stredTotcarichi, stredTotscarichi, ndcGrscPos)
          Else ' data inizio posteriore a data ult. agg +1
            SaldoStoricoSuccessivo(stredEsistpr, stredEsistfi, stredTotvcarichi, stredTotvscarichi, _
                                   stredTotcarichi, stredTotscarichi, ndcGrscPos)
          End If
        Else
          NoSaldoStorico(ndcGrscPos, stredEsistpr, stredTotvcarichi, stredTotvscarichi, _
                         stredTotcarichi, stredTotscarichi, stredEsistfi)
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub TotValore(ByRef stredTotvcarichi As String, ByRef stredTotvscarichi As String, _
                                   ByRef stredTotcarichi As String, ByRef stredTotscarichi As String)
    ' questa routine viene chiamata solo per ordinamento = Conto
    Dim dValoreCarichi As Decimal
    Dim dValoreScarichi As Decimal
    Dim dCarichi As Decimal
    Dim dScarichi As Decimal
    Dim i As Integer
    Try
      For i = 0 To dsGridShared.Tables("MOVMAG").Rows.Count - 1
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = 1 Then dValoreCarichi = dValoreCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_valore)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = -1 Then dValoreScarichi = dValoreScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_valore)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = 1 Then dCarichi = dCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_quant)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = -1 Then dScarichi = dScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_quant)
      Next

      If strScarOrdin = "A" Then
        stredTotvcarichi = NTSCStr(dValoreCarichi)
        stredTotvscarichi = NTSCStr(dValoreScarichi)
      Else
        stredTotvcarichi = "0"
        stredTotvscarichi = "0"
      End If
      stredTotcarichi = NTSCStr(dCarichi)
      stredTotscarichi = NTSCStr(dScarichi)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub NoSaldoStorico(ByVal ndcGrscPos As Integer, ByRef stredEsistpr As String, _
                                        ByRef stredTotvcarichi As String, ByRef stredTotvscarichi As String, _
                                        ByRef stredTotcarichi As String, ByRef stredTotscarichi As String, _
                                        ByRef stredEsistfi As String)
    Dim i As Integer
    Dim dValoreCarichi As Decimal
    Dim dValoreScarichi As Decimal
    Dim dCarichi As Decimal
    Dim dScarichi As Decimal
    Dim dsArtdef As DataSet = Nothing
    Dim lRecords As Integer
    Try
      lRecords = NTSCInt(dsGridShared.Tables("MOVMAG").Rows.Count)
      ReDim dEsist(lRecords)
      '---------------------------------------------------------------
      If bGrscSaldiIniziali = True Then

        GetArtdef(NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_codart), _
                                  NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_magaz), _
                                  NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_fase), 0, dsArtdef)

        If NTSCInt(dsArtdef.Tables("ARTDEF").Rows.Count) > 0 Then
          stredEsistpr = NTSCStr(NTSCDec(dsArtdef.Tables("ARTDEF").Rows(0)!ad_esist))
          dEsist(0) = (NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_quant) * NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar)) + NTSCDec(dsArtdef.Tables("ARTDEF").Rows(0)!ad_esist)
        Else
          stredEsistpr = IntSetNum("0,0000")
          dEsist(0) = (NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_quant) * NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar))
        End If
      End If
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = 1 Then dValoreCarichi = dValoreCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_valore)
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = -1 Then dValoreScarichi = dValoreScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_valore)
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = 1 Then dCarichi = dCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_quant)
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = -1 Then dScarichi = dScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_quant)
      '---------------------------------------------------------------
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows.Count) = 1 Then
        If bGrscSaldiIniziali = True Then stredEsistfi = NTSCStr(dEsist(0))
        GoTo Fine
      End If
      For i = 1 To dsGridShared.Tables("MOVMAG").Rows.Count - 1
        If bGrscSaldiIniziali = True Then dEsist(i) = dEsist(i - 1) + (NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_quant) * NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar))
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = 1 Then dValoreCarichi = dValoreCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_valore)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = -1 Then dValoreScarichi = dValoreScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_valore)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = 1 Then dCarichi = dCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_quant)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = -1 Then dScarichi = dScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_quant)
      Next
      If bGrscSaldiIniziali = True Then stredEsistfi = NTSCStr(dEsist(dsGridShared.Tables("MOVMAG").Rows.Count - 1))
Fine:
      stredTotvcarichi = NTSCStr(dValoreCarichi)
      stredTotvscarichi = NTSCStr(dValoreScarichi)
      stredTotcarichi = NTSCStr(dCarichi)
      stredTotscarichi = NTSCStr(dScarichi)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub SaldoStoricoPrecedente(ByRef stredEsistpr As String, ByRef stredEsistfi As String, _
                                                ByRef stredTotvcarichi As String, ByRef stredTotvscarichi As String, _
                                                ByRef stredTotcarichi As String, ByRef stredTotscarichi As String, _
                                                ByVal ndcGrscPos As Integer)
    Dim i As Integer
    Dim strData As String
    Dim dSaldo As Decimal
    Dim dSaldo1 As Decimal
    Dim dValoreCarichi As Decimal
    Dim dValoreScarichi As Decimal
    Dim dCarichi As Decimal
    Dim dScarichi As Decimal
    Dim dsStorico As DataSet = Nothing
    Dim dsArtdef As DataSet = Nothing
    Try
      If bGrscSaldiIniziali = True Then
        strData = CDataSQL(strDtulap)

        'CALCOLA SALDO ESISTENZA DA DATA INIZIALE A (tb_dtulap + 1)...
        SaldoStorico(NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_codart), _
                          NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_magaz), _
                          NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_fase), _
                          strDatini, strData, dsStorico)

        If dsStorico.Tables("MOVMAG").Rows.Count = 0 Then
          dSaldo1 = 0
        Else
          If Not (NTSCDec(dsStorico.Tables("MOVMAG").Rows(0)!saldop) = 0) Then dSaldo1 = NTSCDec(dsStorico.Tables("MOVMAG").Rows(0)!saldop) Else dSaldo1 = 0
        End If
        '----------------------------------------------------------------------
        GetArtdef(NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_codart), _
                          NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_magaz), _
                          NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_fase), 1, dsArtdef)

        If NTSCInt(dsArtdef.Tables("ARTDEF").Rows.Count) > 0 Then
          dSaldo = NTSCDec(dsArtdef.Tables("ARTDEF").Rows(0)!ad_esist) - dSaldo1
          stredEsistpr = NTSCStr(dSaldo)
        Else
          dSaldo = dSaldo1 * -1
          stredEsistpr = NTSCStr(dSaldo)
        End If
      End If
      'RIEMPIE UN VETTORE CALCOLANDO ESISTENZA DA (tb_dtulap + 1) A DATA FINALE...
      If bGrscSaldiIniziali = True Then
        ReDim dEsist(NTSCInt(dsGridShared.Tables("MOVMAG").Rows.Count))
        dEsist(0) = dSaldo + (NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_quant) * NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar))
      End If
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = 1 Then dValoreCarichi = dValoreCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_valore)
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = -1 Then dValoreScarichi = dValoreScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_valore)
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = 1 Then dCarichi = dCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_quant)
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = -1 Then dScarichi = dScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_quant)
      For i = 1 To dsGridShared.Tables("MOVMAG").Rows.Count - 1
        If bGrscSaldiIniziali = True Then dEsist(i) = dEsist(i - 1) + (NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_quant) * NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar))
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = 1 Then dValoreCarichi = dValoreCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_valore)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = -1 Then dValoreScarichi = dValoreScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_valore)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = 1 Then dCarichi = dCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_quant)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = -1 Then dScarichi = dScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_quant)
      Next
      If bGrscSaldiIniziali = True Then stredEsistfi = NTSCStr(dEsist(dsGridShared.Tables("MOVMAG").Rows.Count - 1))
      stredTotvcarichi = NTSCStr(dValoreCarichi)
      stredTotvscarichi = NTSCStr(dValoreScarichi)
      stredTotcarichi = NTSCStr(dCarichi)
      stredTotscarichi = NTSCStr(dScarichi)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub SaldoStoricoSuccessivo(ByRef stredEsistpr As String, ByRef stredEsistfi As String, _
                                                ByRef stredTotvcarichi As String, ByRef stredTotvscarichi As String, _
                                                ByRef stredTotcarichi As String, ByRef stredTotscarichi As String, _
                                                ByVal ndcGrscPos As Integer)
    Dim i As Integer
    Dim strData As String
    Dim dSaldo As Decimal
    Dim dSaldo1 As Decimal
    Dim dValoreCarichi As Decimal
    Dim dValoreScarichi As Decimal
    Dim dCarichi As Decimal
    Dim dScarichi As Decimal
    Dim dsArtdef As DataSet = Nothing
    Dim dsStorico As DataSet = Nothing
    Try
      If bGrscSaldiIniziali = True Then
        strData = cdatasql((NTSCDate(strDtulap).AddDays(1).ToShortDateString))

        'CALCOLA SALDO ESISTENZA DA DATA INIZIALE A (tb_dtulap + 1)...
        SaldoStorico(NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_codart), _
                  NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_magaz), _
                  NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_fase), _
                  strData, strDatinimeno1, dsStorico)
        If dsStorico.Tables("MOVMAG").Rows.Count = 0 Then
          dSaldo1 = 0
        Else
          If Not (NTSCInt(dsStorico.Tables("MOVMAG").Rows(0)!saldop) = 0) Then dSaldo1 = NTSCDec(dsStorico.Tables("MOVMAG").Rows(0)!saldop) Else dSaldo1 = 0
        End If
        '----------------------------------------------------------------------
        GetArtdef(NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_codart), _
                  NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_magaz), _
                  NTSCStr(dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)!km_fase), 2, dsArtdef)

        If dsArtdef.Tables("ARTDEF").Rows.Count > 0 Then
          dSaldo = dSaldo1 + NTSCDec(dsArtdef.Tables("ARTDEF").Rows(0)!ad_esist)
          stredEsistpr = NTSCStr(dSaldo)
        Else
          dSaldo = dSaldo1
          stredEsistpr = NTSCStr(dSaldo)
        End If
      End If
      'RIEMPIE UN VETTORE CALCOLANDO ESISTENZA DA (tb_dtulap + 1) A DATA FINALE...
      If bGrscSaldiIniziali = True Then
        ReDim dEsist(dsGridShared.Tables("MOVMAG").Rows.Count)
        dEsist(0) = dSaldo + (NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_quant) * NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar))
      End If
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = 1 Then dValoreCarichi = dValoreCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_valore)
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = -1 Then dValoreScarichi = dValoreScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_valore)
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = 1 Then dCarichi = dCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_quant)
      If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(0)!km_carscar) = -1 Then dScarichi = dScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(0)!mm_quant)
      For i = 1 To dsGridShared.Tables("MOVMAG").Rows.Count - 1
        If bGrscSaldiIniziali = True Then dEsist(i) = dEsist(i - 1) + (NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_quant) * NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar))
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = 1 Then dValoreCarichi = dValoreCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_valore)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = -1 Then dValoreScarichi = dValoreScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_valore)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = 1 Then dCarichi = dCarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_quant)
        If NTSCInt(dsGridShared.Tables("MOVMAG").Rows(i)!km_carscar) = -1 Then dScarichi = dScarichi + NTSCDec(dsGridShared.Tables("MOVMAG").Rows(i)!mm_quant)
      Next
      If bGrscSaldiIniziali = True Then stredEsistfi = NTSCStr(dEsist(dsGridShared.Tables("MOVMAG").Rows.Count - 1))
      stredTotvcarichi = NTSCStr(dValoreCarichi)
      stredTotvscarichi = NTSCStr(dValoreScarichi)
      stredTotcarichi = NTSCStr(dCarichi)
      stredTotscarichi = NTSCStr(dScarichi)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub CalcolaRettifiche(ByRef stredRettificheAcq As String, ByRef stredRettificheVen As String, _
                                           ByVal ndcGrscPos As Integer)
    Dim strRettificheAcq As String = ""
    Dim strRettificheVen As String = ""
    Try
      If Not CalcolaRettificheElab(strRettificheAcq, strRettificheVen, _
                                 dsGrscShared.Tables("MOVMAG").Rows(ndcGrscPos)) Then Return

      stredRettificheAcq = strRettificheAcq
      stredRettificheVen = strRettificheVen

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      '--------------------------------------------------------------------------------------------------------------
      e.Row.EndEdit()
      e.Row.EndEdit()
      '--------------------------------------------------------------------------------------------------------------
      Dim strFunction As String = "ArticoAfterColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '--------------------------------------------------------------------------------------------------------------
      Dim strFunction As String = "BeforeColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_mm_quant(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      e.Row!carichi = 0
      e.Row!scarichi = 0
      '--------------------------------------------------------------------------------------------------------------
      If NTSCDec(e.ProposedValue) <> 0 Then
        Select Case NTSCInt(e.Row!km_carscar)
          Case 1 : e.Row!carichi = e.ProposedValue
          Case -1 : e.Row!scarichi = e.ProposedValue
        End Select
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Function ModificaRigaVeboll(ByRef dtrT As DataRow) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    '--- Al cambio delle righe di veboll, nella griglia, riaggiorno il documento di magazzino
    '----------------------------------------------------------------------------------------------------------------
    Dim bOk As Boolean = False
    Dim nRow As Integer = 0
    Dim ds As New DataSet

    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

      '--------------------------------------------------------------------------------------------------------------
      '--- Se serve istanzio BEVEBOLL
      '--------------------------------------------------------------------------------------------------------------
      If oCleBoll Is Nothing Then
        Dim strErr As String = ""
        Dim oTmp As Object = Nothing
        If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEMGSCHE", "BEVEBOLL", oTmp, strErr, False, "", "") = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129684375964333710, "ERRORE in fase di creazione Entity 'BEVEBOLL', l'opzione di registro \Bsvefdin\Opzioni\ConsentiModifCodPagaSc è stata disabilitata." & vbCrLf & "Errore:" & vbCrLf) & strErr))
          Return False
        End If
        oCleBoll = CType(oTmp, CLEVEBOLL)
        AddHandler oCleBoll.RemoteEvent, AddressOf VebollGestisciEventiEntity
        If oCleBoll.Init(oApp, MyBase.oScript, MyBase.oCleComm, "", False, "", "") = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129684376145818037, "Impossibile caricare il componente che effettua il ricolacolo dei documenti (""BEVEBOLL""), l'opzione di registro \Bsvefdin\Opzioni\ConsentiModifCodPagaSc è stata disabilitata.")))
          Return False
        End If
        If Not oCleBoll.InitExt() Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129684376166626079, "Impossibile caricare il componente che effettua il ricolacolo dei documenti (""BEVEBOLL""), l'opzione di registro \Bsvefdin\Opzioni\ConsentiModifCodPagaSc è stata disabilitata.")))
          Return False
        End If
        oCleBoll.bIsCRMUser = False     'se posso fare la fattura posso anche modificare il ddt!!!!
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Apro il documento, cambio il dato e lo salvo
      '--------------------------------------------------------------------------------------------------------------
      If oCleBoll.ApriDoc(strDittaCorrente, False, dtrT!km_tipork.ToString, NTSCInt(dtrT!km_anno), _
                          dtrT!km_serie.ToString, NTSCInt(dtrT!km_numdoc), ds) Then
        For nRow = 0 To (oCleBoll.dttEC.Rows.Count - 1)
          If NTSCInt(oCleBoll.dttEC.Rows(nRow)!ec_riga) = NTSCInt(dtrT!km_riga) Then
            bOk = True
            If Not ModificaRigaVeboll(dtrT, oCleBoll.dttEC.Rows(nRow)) Then Return False
            If Not oCleBoll.RecordSalva(nRow, False, Nothing) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129684387010817143, "La modifica del documento numero |" & dtrT!km_numdoc.ToString & "|, serie '|" & dtrT!km_serie.ToString & "|' non è avvenuta. Si è verificato un errore durante il salvataggio della riga nel documento.")))
              Return False
            End If
            Exit For
          End If
        Next
        '------------------------------------------------------------------------------------------------------------
        If bOk = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740018036457000, "La modifica del documento numero |" & dtrT!km_numdoc.ToString & "|, serie '|" & dtrT!km_serie.ToString & "|' non è avvenuta: non è stata trovata la riga |" & dtrT!km_riga.ToString & "|")))
          Return False
        End If
        '------------------------------------------------------------------------------------------------------------
        If Not oCleBoll.SalvaDocumento("U") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129684381376447238, "La modifica del documento numero |" & dtrT!km_numdoc.ToString & "|, serie '|" & dtrT!km_serie.ToString & "|' non è avvenuta.")))
          Return False
        End If
        '------------------------------------------------------------------------------------------------------------
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129684381432563349, "La modifica del documento numero |" & dtrT!km_numdoc.ToString & "|, serie '|" & dtrT!km_serie.ToString & "|' non è avvenuta.")))
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
      ds.Clear()
      ds.Dispose()
    End Try
  End Function
  Public Overridable Function ModificaRigaVeboll(ByRef dtrT As DataRow, ByRef dtrEC As DataRow) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    'dtrT = riga di griglia di BNMGRGSC
    'dtrEC = riga del documento di magazzino aperto
    '----------------------------------------------------------------------------------------------------------------
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then

      '--------------------------------------------------------------------------------------------------------------
      dtrEC!ec_quant = dtrT!mm_quant
      dtrEC!ec_colli = dtrT!mm_colli
      dtrEC!ec_prezzo = dtrT!mm_prezzo
      dtrEC!ec_scont1 = dtrT!mm_scont1
      dtrEC!ec_scont2 = dtrT!mm_scont2
      dtrEC!ec_scont3 = dtrT!mm_scont3
      dtrEC!ec_scont4 = dtrT!mm_scont4
      dtrEC!ec_scont5 = dtrT!mm_scont5
      dtrEC!ec_scont6 = dtrT!mm_scont6
      dtrEC!ec_provv = dtrT!mm_provv
      dtrEC!ec_provv2 = dtrT!mm_provv2
      dtrEC!ec_codiva = dtrT!mm_codiva
      dtrEC!ec_controp = dtrT!mm_controp
      dtrEC!ec_codcena = dtrT!mm_codcena
      dtrEC!ec_codcfam = dtrT!mm_codcfam
      dtrEC!ec_commeca = dtrT!mm_commeca
      dtrEC!ec_datini = dtrT!mm_datini
      dtrEC!ec_datfin = dtrT!mm_datfin
      '--------------------------------------------------------------------------------------------------------------
      'se dopo aver assegnato il valore non è stato recepito dal documento, ritorno false
      '--------------------------------------------------------------------------------------------------------------
      If NTSCDec(dtrEC!ec_quant) <> NTSCDec(dtrT!mm_quant) Then Return False
      If NTSCDec(dtrEC!ec_colli) <> NTSCDec(dtrT!mm_colli) Then Return False
      If NTSCDec(dtrEC!ec_prezzo) <> NTSCDec(dtrT!mm_prezzo) Then Return False
      If NTSCDec(dtrEC!ec_scont1) <> NTSCDec(dtrT!mm_scont1) Then Return False
      If NTSCDec(dtrEC!ec_scont2) <> NTSCDec(dtrT!mm_scont2) Then Return False
      If NTSCDec(dtrEC!ec_scont3) <> NTSCDec(dtrT!mm_scont3) Then Return False
      If NTSCDec(dtrEC!ec_scont4) <> NTSCDec(dtrT!mm_scont4) Then Return False
      If NTSCDec(dtrEC!ec_scont5) <> NTSCDec(dtrT!mm_scont5) Then Return False
      If NTSCDec(dtrEC!ec_scont6) <> NTSCDec(dtrT!mm_scont6) Then Return False
      If NTSCDec(dtrEC!ec_provv) <> NTSCDec(dtrT!mm_provv) Then Return False
      If NTSCDec(dtrEC!ec_provv2) <> NTSCDec(dtrT!mm_provv2) Then Return False
      If NTSCInt(dtrEC!ec_codiva) <> NTSCInt(dtrT!mm_codiva) Then Return False
      If NTSCInt(dtrEC!ec_controp) <> NTSCInt(dtrT!mm_controp) Then Return False
      If NTSCInt(dtrEC!ec_codcena) <> NTSCInt(dtrT!mm_codcena) Then Return False
      If NTSCStr(dtrEC!ec_codcfam) <> NTSCStr(dtrT!mm_codcfam) Then Return False
      If NTSCInt(dtrEC!ec_commeca) <> NTSCInt(dtrT!mm_commeca) Then Return False
      If NTSCStr(dtrEC!ec_datini) <> NTSCStr(dtrT!mm_datini) Then Return False
      If NTSCStr(dtrEC!ec_datfin) <> NTSCStr(dtrT!mm_datfin) Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Riaggiorno indietro la riga di bndkkons
      '--------------------------------------------------------------------------------------------------------------
      dtrT!mm_valore = dtrEC!ec_valore
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
  Public Overridable Sub VebollGestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.TipoEvento <> "GRIAGG" Then ThrowRemoteEvent(e)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
#End Region

#Region "funzioni specifiche per BNMGSCHE.BNMGGRNP.VB"
  Public Overridable Function GrnplbArticolo_Validated(ByVal strCod As String, ByRef strDescr As String) As Boolean
    Try
      Return GrnplbArticolo_Validated(strCod, strDescr, Nothing)
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
  Public Overridable Function GrnplbArticolo_Validated(ByVal strCod As String, ByRef strDescr As String, ByRef dttOut As DataTable) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCod, strDescr, dttOut})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strDescr = NTSCStr(oIn(1))
        dttOut = CType(oIn(2), DataTable)
        Return CBool(oOut)
      End If
      '----------------
      If Trim(strCod) = "" Then
        strDescr = ""
        dttOut = Nothing
        Return True
      End If

      dttOut = New DataTable
      Return Not oCldSche.ValCodiceDb(strCod, strDittaCorrente, "ARTICO", "S", strDescr, dttOut)
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
  Public Overridable Function GrnplbConto_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "ANAGRA", "N", strDescr) Then
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
  Public Overridable Function GrnpCausale_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABCAUM", "N", strDescr) Then
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

  Public Overridable Function GrnpComponiStringa(ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Try
      '----------------------------------------------------------------------------------
      '--- Passa i parametri per la query
      oCldSche.GrnpComponiStringa(strDittaCorrente, dtrTmp, ds, strScarOrdin, strDatini, strDatfin, _
                                  nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                                  lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                                  strScarDacodart, strScarAcodart, nScarCodmarcini, nScarCodmarcfin, _
                                  nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, _
                                  nScarCausale, nScarGruppo, nScarSottogr, bRigheInevase, _
                                  strGrnpCodcfam, nGrnpAnnotco, nGrnpCodstag, bModuloCRM, _
                                  bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                  bAmm, strAltriFiltri, strScarDalotto, strScarAlotto, _
                                  strScarClassLivello1, strScarClassLivello2, strScarClassLivello3, _
                                  strScarClassLivello4, strScarClassLivello5, _
                                  strScarCodart, nScarCodlsar, strScarConto, nScarCodlsar)

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

  Public Overridable Function GrnpApri(ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrnpApri(strDittaCorrente, strScarOrdin, strDatini, strDatfin, _
                        nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                        lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                        strScarDacodart, strScarAcodart, nScarCodmarcini, nScarCodmarcfin, _
                        nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, _
                        nScarCausale, nScarGruppo, nScarSottogr, bRigheInevase, _
                        strGrnpCodcfam, nGrnpAnnotco, nGrnpCodstag, bModuloCRM, _
                        bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                        bAmm, ds, strAltriFiltri, strScarDalotto, strScarAlotto, _
                        strScarClassLivello1, strScarClassLivello2, strScarClassLivello3, _
                        strScarClassLivello4, strScarClassLivello5, _
                        strScarCodart, nScarCodlsar, strScarConto, nScarCodlsel)

      If ds.Tables("MOVPRB").Rows.Count = 0 Then
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

  Public Overridable Function GrnpGetArtpro(ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrnpGetArtpro(strDittaCorrente, dtrTmp, ds)

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

#Region "funzioni specifiche per BNMGSCHE.BNMGGRMA.VB"
  Public Overridable Function GrmalbConto_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "ANAGRA", "N", strDescr) Then
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
  Public Overridable Function GrmaCausale_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABCAUM", "N", strDescr) Then
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

  Public Overridable Function GrmaComponiStringa(ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Try
      '----------------------------------------------------------------------------------
      '--- Passa i parametri per la query
      oCldSche.GrmaComponiStringa(strDittaCorrente, dtrTmp, ds, lScarDaconto, lScarAconto, _
                                            lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                                            strDatini, strDatfin, strScarOrdin, strScarTipodoc, _
                                            strScarSerie, nScarCausale, bRigheInevase, bModuloCRM, _
                                            bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                                            bAmm, strAltriFiltri, strScarDalotto, strScarAlotto)

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

  Public Overridable Function GrmaApri(ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrmaApri(strDittaCorrente, strScarOrdin, strDatini, strDatfin, _
                        nScarDamagaz, nScarAmagaz, lScarDaconto, lScarAconto, _
                        lScarDalotto, lScarAlotto, lScarDacomme, lScarAcomme, _
                        strScarDacodart, strScarAcodart, nScarCodmarcini, nScarCodmarcfin, _
                        nScarFaseini, nScarFasefin, strScarTipodoc, strScarSerie, _
                        nScarCausale, nScarGruppo, nScarSottogr, bRigheInevase, _
                        strGrmaCodcfam, nGrmaAnnotco, nGrmaCodstag, bModuloCRM, _
                        bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, _
                        bAmm, strGrmaDamatric, strGrmaAmatric, ds, strAltriFiltri, _
                        strScarDalotto, strScarAlotto, _
                        strScarClassLivello1, strScarClassLivello2, strScarClassLivello3, _
                        strScarClassLivello4, strScarClassLivello5, _
                        strScarCodart, nScarCodlsar, strScarConto, nScarCodlsel)

      If ds.Tables("MOVPRB").Rows.Count = 0 Then
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

  Public Overridable Function GrmaGetArtpro(ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrmaGetArtpro(strDittaCorrente, dtrTmp, ds)

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

#Region "funzioni specifiche per BNMGSCHE.BNMGGRLO.VB"
  Public Overridable Function GrlolbMaga_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABMAGA", "N", strDescr) Then
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
  Public Overridable Function GrlolbConto_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "ANAGRA", "N", strDescr) Then
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
  Public Overridable Function GrloCausale_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABCAUM", "N", strDescr) Then
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
  Public Overridable Function GrloCommessa_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldSche.ValCodiceDb(nCod.ToString, strDittaCorrente, "COMMESS", "N", strDescr) Then
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

  Public Overridable Function GrloComponiStringa(ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Try
      '----------------------------------------------------------------------------------
      '--- Passa i parametri per la query
      oCldSche.GrloComponiStringa(strDittaCorrente, dtrTmp, ds, nScarOrdin, strTTStloco, strTTStlocs, _
                                  lIITTStloco, lIITTStlocs, lIITTStlocu)

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

  Public Overridable Function GrloApri(ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloApri(strDittaCorrente, nScarOrdin, strTTStloco, strTTStlocs, _
                        lIITTStloco, lIITTStlocs, lIITTStlocu, ds)

      If ds.Tables("TTLOTTI").Rows.Count = 0 Then
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

  Public Overridable Function GrloGetAnalotti(ByVal strCodart As String, ByVal strLotto As String, _
                                              ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloGetAnalotti(strDittaCorrente, strCodart, strLotto, ds)

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

  Public Overridable Function GrloGetRimLotto(ByVal dtrTmp As DataRow, ByVal lLotto As Integer, _
                                               ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloGetRimLotto(strDittaCorrente, strTTStloco, lIITTStloco, dtrTmp, lLotto, ds)

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
  Public Overridable Function GrloGetRimLotto2(ByVal dtrTmp As DataRow, ByVal strUbicaz As String, _
                                               ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloGetRimLotto2(strDittaCorrente, lIITTStlocu, dtrTmp, strUbicaz, ds)

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

  Public Overridable Function GrloGetCostoMedio(ByVal dtrTmp As DataRow, ByVal lLotto As Integer, _
                                             ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloGetCostoMedio(strDittaCorrente, strTTStloco, lIITTStloco, dtrTmp, _
                                lLotto, ds)

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
  Public Overridable Function GrloGetCostoMedio2(ByVal dtrTmp As DataRow, ByVal strUbicaz As String, _
                                               ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloGetCostoMedio2(strDittaCorrente, lIITTStlocu, dtrTmp, strUbicaz, ds)

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

  Public Overridable Function GrloGetValLotto(ByVal dtrTmp As DataRow, ByVal lLotto As Integer, _
                                           ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloGetValLotto(strDittaCorrente, strTTStloco, lIITTStloco, dtrTmp, _
                                lLotto, ds)

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
  Public Overridable Function GrloGetValLotto2(ByVal dtrTmp As DataRow, ByVal strUbicaz As String, _
                                               ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloGetValLotto2(strDittaCorrente, lIITTStlocu, dtrTmp, strUbicaz, ds)

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

  Public Overridable Function GrloTotCostoUnitario(ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloTotCostoUnitario(strDittaCorrente, strTTStloco, lIITTStloco, dtrTmp, _
                                    ds)

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
  Public Overridable Function GrloTotCostoUnitario2(ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloTotCostoUnitario2(strDittaCorrente, strTTStlocs, lIITTStlocs, dtrTmp, _
                                ds)

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
  Public Overridable Function GrloTotCostoUnitario3(ByVal dtrTmp As DataRow, _
                                       ByRef ds As DataSet, ByRef ds2 As DataSet) As Boolean
    Try
      oCldSche.GrloTotCostoUnitario3(strDittaCorrente, lIITTStlocu, dtrTmp, _
                                ds, ds2)

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
  Public Overridable Function GrloTotCostoUnitario4(ByVal dtrTmp As DataRow, ByVal dtrTmp1 As DataRow, _
                                       ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloTotCostoUnitario4(strDittaCorrente, lIITTStlocu, dtrTmp, dtrTmp1, _
                                ds)

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

  Public Overridable Function GrloTotRimanenzeArticoli(ByVal dtrTmp As DataRow, ByRef ds As DataSet) As Boolean
    Try
      oCldSche.GrloTotRimanenzeArticoli(strDittaCorrente, nScarOrdin, strTTStloco, lIITTStloco, _
                                        strTTStlocs, lIITTStlocs, lIITTStlocu, _
                                        dtrTmp, ds)

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
      Return oCldSche.GetTableStructure("MOVIFIL", False, dttTable)
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
      If Not oCldSche.CaricaFiltri(strDittaCorrente, "BNMGSCHE", dttOut) Then Return False

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
      If Not oCldSche.LeggiFiltro(lCod, strChild, strForm, dttOut) Then Return False

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
