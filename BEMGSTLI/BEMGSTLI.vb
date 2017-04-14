Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGSTLI
  Inherits CLE__BASN

  Public oCldStli As CLDMGSTLI
  Public lIITTStli As Integer = 0
  Public lIITTListsar As Integer = 0

  Public strWhereClie As String
  Public strWhereFiar As String
  Public strOrderBy As String = " ORDER BY artico.ar_codart"
  Public bSeleziona As Boolean

  Public dsSharedLise As DataSet

  Public bStampaWordRaggruppata As Boolean
  Public bHasChanges As Boolean

  'opzioni
  Public nAltezzaGif As Integer

  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  '--- CRM
  Public bIsCRMUser As Boolean = False
  Public bModuloCRM As Boolean = False

  Public bNessunoClie As Boolean = False
  Public bSoloClie As Boolean = False
  Public bMultiClie As Boolean = False

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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGSTLI"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldStli = CType(MyBase.ocldBase, CLDMGSTLI)
    oCldStli.Init(oApp)
    Return True
  End Function

  Public Overridable Function IstanziaNTSCondCommerciali() As NTSCondCommerciali
    Try
      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEMGSTLI", "BN__STD.NTSCondCommerciali", oTmp, strErr, False, "", "") = False Then
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

  Public Overridable Function edConto_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim dttAnagra As New DataTable
    Dim dttForn As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not oCldStli.ValCodiceDb(nCod.ToString, strDittaCorrente, "ANAGRA", "N", strDescr, dttAnagra) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128595749233917773, "Attenzione!" & vbCrLf & _
          "Codice conto |'" & nCod.ToString & "'| inesistente.")))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(dttAnagra.Rows(0)!an_tipo)
        Case "S"
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130609531103096036, "Attenzione!" & vbCrLf & _
            "Il conto indicato non può far parte dei sottoconti.")))
          Return False
        Case "F"
          If (bIsCRMUser = True) And (bModuloCRM = True) Then
            dttForn = oCldStli.PermessiFornitoriCRM(strDittaCorrente)
            If dttForn.Rows.Count = 0 OrElse NTSCStr(dttForn.Rows(0)!opdi_amm) = "N" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128595749233917774, "Attenzione!" & vbCrLf & _
                "Il codice conto |'" & nCod.ToString & "'| non può essere quello di un fornitore.")))
              Return False
            End If
          End If
        Case Else
          If (bIsCRMUser = True) And (bModuloCRM = True) Then
            If oCldStli.PermessiClientiCRM(strDittaCorrente, nCod) = False Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130609570281884787, "Attenzione!" & vbCrLf & _
                "Cliente non visibile a causa di blocco da modulo CRM.")))
              Return False
            End If
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttAnagra.Clear() : dttAnagra.Dispose()
      dttForn.Clear() : dttForn.Dispose()
    End Try
  End Function
  Public Overridable Function edCodling_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldStli.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLING", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128672398639822421, "Codice lingua |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function edCodlavo_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldStli.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLAVO", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128672399091240507, "Codice lavorazione |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function edCodvalu_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldStli.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABVALU", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128672399559536551, "Codice valuta |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function edCodpromo_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldStli.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABTPRO", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129024857822892772, "Codice promozione |'" & nCod.ToString & "'| inesistente")))
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
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldStli.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLSAR", "N", strDescr) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128672570946100328, "Codice lista selezionata |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function edListino_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      If Not oCldStli.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLIST", "N", strDescr) Then
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
  Public Overridable Function edCodlsel_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not oCldStli.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABLSEL", "N", "", dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130384198775518613, "Attenzione!" & vbCrLf & _
          "Codice lista selezionata Clienti/Fornitori|'" & nCod.ToString & "'| inesistente.")))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dttTmp.Rows(0)!tb_tipocl) <> "C" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130384199567850713, "Attenzione!" & vbCrLf & _
          "La lista selezionata selezionata deve essere di tipo 'Clienti/Fornitori'.")))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      strDescr = NTSCStr(dttTmp.Rows(0)!tb_deslsel)
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

  Public Overridable Function TestPreElabora(ByVal stredConto As String) As Boolean
    Dim dttTmp As DataTable = New DataTable
    Try
      If Not NTSCInt(stredConto) = 0 Then
        If oCldStli.ValCodiceDb(stredConto, strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
          If NTSCStr(dttTmp.Rows(0)!an_tipo) = "S" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128674284348663618, "Il conto non può far parte dei sottoconti.")))
            Return False
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

  Public Overridable Function Elabora(ByVal bopSelezione0 As Boolean, ByVal bopSelezione1 As Boolean, _
                                      ByVal stredCodlsar As String, _
                                      ByVal stredConto As String, ByVal stredDatagg As String, _
                                      ByVal strcbTipo1 As String, ByVal stredCodpromo1 As String, _
                                      ByVal stredCodlavo1 As String, ByVal stredListino1 As String, _
                                      ByVal stredCodvalu1 As String, ByVal stredQuant1 As String, _
                                      ByVal bckSecondoPrezzo As Boolean, ByVal strcbTipo2 As String, _
                                      ByVal stredCodpromo2 As String, ByVal stredCodlavo2 As String, _
                                      ByVal stredListino2 As String, ByVal stredCodvalu2 As String, _
                                      ByVal stredQuant2 As String, ByVal bckterzoPrezzo As Boolean, _
                                      ByVal strcbTipo3 As String, ByVal stredCodpromo3 As String, _
                                      ByVal stredCodlavo3 As String, ByVal stredListino3 As String, _
                                      ByVal stredCodvalu3 As String, ByVal stredQuant3 As String, _
                                      ByVal bckQuartoPrezzo As Boolean, ByVal strcbTipo4 As String, _
                                      ByVal stredCodpromo4 As String, ByVal stredCodlavo4 As String, _
                                      ByVal stredListino4 As String, ByVal stredCodvalu4 As String, _
                                      ByVal stredQuant4 As String, _
                                      ByVal bckQuintoPrezzo As Boolean, ByVal strcbTipo5 As String, _
                                      ByVal stredCodpromo5 As String, ByVal stredCodlavo5 As String, _
                                      ByVal stredListino5 As String, ByVal stredCodvalu5 As String, _
                                      ByVal stredQuant5 As String, ByVal bcksconti1 As Boolean, _
                                      ByVal strcbTipoSconto1 As String, ByVal stredCodtpro1 As String, _
                                      ByVal stredQuantScont1 As String, ByVal bcksconti2 As Boolean, _
                                      ByVal strcbTipoSconto2 As String, ByVal stredCodtpro2 As String, _
                                      ByVal stredQuantScont2 As String, ByVal bcksconti3 As Boolean, _
                                      ByVal strcbTipoSconto3 As String, ByVal stredCodtpro3 As String, _
                                      ByVal stredQuantScont3 As String, ByVal bcksconti4 As Boolean, _
                                      ByVal strcbTipoSconto4 As String, ByVal stredCodtpro4 As String, _
                                      ByVal stredQuantScont4 As String, ByVal stredCodling As String, _
                                      ByVal bckSeleziona As Boolean, ByVal bckNoSoloSconti As Boolean, _
                                      ByVal bGriglia As Boolean, ByRef strError As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return Elabora(bopSelezione0, bopSelezione1, stredCodlsar, stredConto, stredDatagg, strcbTipo1, _
                     stredCodpromo1, stredCodlavo1, stredListino1, stredCodvalu1, stredQuant1, _
                     bckSecondoPrezzo, strcbTipo2, stredCodpromo2, stredCodlavo2, stredListino2, _
                     stredCodvalu2, stredQuant2, bckterzoPrezzo, strcbTipo3, stredCodpromo3, _
                     stredCodlavo3, stredListino3, stredCodvalu3, stredQuant3, bckQuartoPrezzo, _
                     strcbTipo4, stredCodpromo4, stredCodlavo4, stredListino4, stredCodvalu4, stredQuant4, _
                     bckQuintoPrezzo, strcbTipo5, stredCodpromo5, stredCodlavo5, stredListino5, stredCodvalu5, _
                     stredQuant5, bcksconti1, strcbTipoSconto1, stredCodtpro1, stredQuantScont1, bcksconti2, _
                     strcbTipoSconto2, stredCodtpro2, stredQuantScont2, bcksconti3, strcbTipoSconto3, stredCodtpro3, _
                     stredQuantScont3, bcksconti4, strcbTipoSconto4, stredCodtpro4, stredQuantScont4, stredCodling, _
                     bckSeleziona, bckNoSoloSconti, bGriglia, strError, 0, 0, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function Elabora(ByVal bopSelezione0 As Boolean, ByVal bopSelezione1 As Boolean, _
                                    ByVal stredCodlsar As String, _
                                    ByVal stredConto As String, ByVal stredDatagg As String, _
                                    ByVal strcbTipo1 As String, ByVal stredCodpromo1 As String, _
                                    ByVal stredCodlavo1 As String, ByVal stredListino1 As String, _
                                    ByVal stredCodvalu1 As String, ByVal stredQuant1 As String, _
                                    ByVal bckSecondoPrezzo As Boolean, ByVal strcbTipo2 As String, _
                                    ByVal stredCodpromo2 As String, ByVal stredCodlavo2 As String, _
                                    ByVal stredListino2 As String, ByVal stredCodvalu2 As String, _
                                    ByVal stredQuant2 As String, ByVal bckterzoPrezzo As Boolean, _
                                    ByVal strcbTipo3 As String, ByVal stredCodpromo3 As String, _
                                    ByVal stredCodlavo3 As String, ByVal stredListino3 As String, _
                                    ByVal stredCodvalu3 As String, ByVal stredQuant3 As String, _
                                    ByVal bckQuartoPrezzo As Boolean, ByVal strcbTipo4 As String, _
                                    ByVal stredCodpromo4 As String, ByVal stredCodlavo4 As String, _
                                    ByVal stredListino4 As String, ByVal stredCodvalu4 As String, _
                                    ByVal stredQuant4 As String, _
                                    ByVal bckQuintoPrezzo As Boolean, ByVal strcbTipo5 As String, _
                                    ByVal stredCodpromo5 As String, ByVal stredCodlavo5 As String, _
                                    ByVal stredListino5 As String, ByVal stredCodvalu5 As String, _
                                    ByVal stredQuant5 As String, ByVal bcksconti1 As Boolean, _
                                    ByVal strcbTipoSconto1 As String, ByVal stredCodtpro1 As String, _
                                    ByVal stredQuantScont1 As String, ByVal bcksconti2 As Boolean, _
                                    ByVal strcbTipoSconto2 As String, ByVal stredCodtpro2 As String, _
                                    ByVal stredQuantScont2 As String, ByVal bcksconti3 As Boolean, _
                                    ByVal strcbTipoSconto3 As String, ByVal stredCodtpro3 As String, _
                                    ByVal stredQuantScont3 As String, ByVal bcksconti4 As Boolean, _
                                    ByVal strcbTipoSconto4 As String, ByVal stredCodtpro4 As String, _
                                    ByVal stredQuantScont4 As String, ByVal stredCodling As String, _
                                    ByVal bckSeleziona As Boolean, ByVal bckNoSoloSconti As Boolean, _
                                    ByVal bGriglia As Boolean, ByRef strError As String, _
                                    ByVal nCodlsel As Integer) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bopSelezione0, bopSelezione1, stredCodlsar, stredConto, stredDatagg, strcbTipo1, _
                     stredCodpromo1, stredCodlavo1, stredListino1, stredCodvalu1, stredQuant1, _
                     bckSecondoPrezzo, strcbTipo2, stredCodpromo2, stredCodlavo2, stredListino2, _
                     stredCodvalu2, stredQuant2, bckterzoPrezzo, strcbTipo3, stredCodpromo3, _
                     stredCodlavo3, stredListino3, stredCodvalu3, stredQuant3, bckQuartoPrezzo, _
                     strcbTipo4, stredCodpromo4, stredCodlavo4, stredListino4, stredCodvalu4, stredQuant4, _
                     bckQuintoPrezzo, strcbTipo5, stredCodpromo5, stredCodlavo5, stredListino5, stredCodvalu5, _
                     stredQuant5, bcksconti1, strcbTipoSconto1, stredCodtpro1, stredQuantScont1, bcksconti2, _
                     strcbTipoSconto2, stredCodtpro2, stredQuantScont2, bcksconti3, strcbTipoSconto3, stredCodtpro3, _
                     stredQuantScont3, bcksconti4, strcbTipoSconto4, stredCodtpro4, stredQuantScont4, stredCodling, _
                     bckSeleziona, bckNoSoloSconti, bGriglia, strError, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(37))
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      Return Elabora(bopSelezione0, bopSelezione1, stredCodlsar, stredConto, stredDatagg, strcbTipo1, _
                     stredCodpromo1, stredCodlavo1, stredListino1, stredCodvalu1, stredQuant1, _
                     bckSecondoPrezzo, strcbTipo2, stredCodpromo2, stredCodlavo2, stredListino2, _
                     stredCodvalu2, stredQuant2, bckterzoPrezzo, strcbTipo3, stredCodpromo3, _
                     stredCodlavo3, stredListino3, stredCodvalu3, stredQuant3, bckQuartoPrezzo, _
                     strcbTipo4, stredCodpromo4, stredCodlavo4, stredListino4, stredCodvalu4, stredQuant4, _
                     bckQuintoPrezzo, strcbTipo5, stredCodpromo5, stredCodlavo5, stredListino5, stredCodvalu5, _
                     stredQuant5, bcksconti1, strcbTipoSconto1, stredCodtpro1, stredQuantScont1, bcksconti2, _
                     strcbTipoSconto2, stredCodtpro2, stredQuantScont2, bcksconti3, strcbTipoSconto3, stredCodtpro3, _
                     stredQuantScont3, bcksconti4, strcbTipoSconto4, stredCodtpro4, stredQuantScont4, stredCodling, _
                     bckSeleziona, bckNoSoloSconti, bGriglia, strError, nCodlsel, 0, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function Elabora(ByVal bopSelezione0 As Boolean, ByVal bopSelezione1 As Boolean, _
                                      ByVal stredCodlsar As String, _
                                      ByVal stredConto As String, ByVal stredDatagg As String, _
                                      ByVal strcbTipo1 As String, ByVal stredCodpromo1 As String, _
                                      ByVal stredCodlavo1 As String, ByVal stredListino1 As String, _
                                      ByVal stredCodvalu1 As String, ByVal stredQuant1 As String, _
                                      ByVal bckSecondoPrezzo As Boolean, ByVal strcbTipo2 As String, _
                                      ByVal stredCodpromo2 As String, ByVal stredCodlavo2 As String, _
                                      ByVal stredListino2 As String, ByVal stredCodvalu2 As String, _
                                      ByVal stredQuant2 As String, ByVal bckterzoPrezzo As Boolean, _
                                      ByVal strcbTipo3 As String, ByVal stredCodpromo3 As String, _
                                      ByVal stredCodlavo3 As String, ByVal stredListino3 As String, _
                                      ByVal stredCodvalu3 As String, ByVal stredQuant3 As String, _
                                      ByVal bckQuartoPrezzo As Boolean, ByVal strcbTipo4 As String, _
                                      ByVal stredCodpromo4 As String, ByVal stredCodlavo4 As String, _
                                      ByVal stredListino4 As String, ByVal stredCodvalu4 As String, _
                                      ByVal stredQuant4 As String, _
                                      ByVal bckQuintoPrezzo As Boolean, ByVal strcbTipo5 As String, _
                                      ByVal stredCodpromo5 As String, ByVal stredCodlavo5 As String, _
                                      ByVal stredListino5 As String, ByVal stredCodvalu5 As String, _
                                      ByVal stredQuant5 As String, ByVal bcksconti1 As Boolean, _
                                      ByVal strcbTipoSconto1 As String, ByVal stredCodtpro1 As String, _
                                      ByVal stredQuantScont1 As String, ByVal bcksconti2 As Boolean, _
                                      ByVal strcbTipoSconto2 As String, ByVal stredCodtpro2 As String, _
                                      ByVal stredQuantScont2 As String, ByVal bcksconti3 As Boolean, _
                                      ByVal strcbTipoSconto3 As String, ByVal stredCodtpro3 As String, _
                                      ByVal stredQuantScont3 As String, ByVal bcksconti4 As Boolean, _
                                      ByVal strcbTipoSconto4 As String, ByVal stredCodtpro4 As String, _
                                      ByVal stredQuantScont4 As String, ByVal stredCodling As String, _
                                      ByVal bckSeleziona As Boolean, ByVal bckNoSoloSconti As Boolean, _
                                      ByVal bGriglia As Boolean, ByRef strError As String, _
                                      ByVal nCodlsel As Integer, ByVal lCoddest As Integer, _
                                      ByVal bNoDestdiv As Boolean) As Boolean
    Dim dsTmp As DataSet = Nothing
    Dim dsTTstli As DataSet = Nothing
    Dim dsTTlistsar As DataSet = Nothing
    Dim i As Integer = 0
    Dim z As Integer = 0
    Dim lPlusProgr As Integer
    Dim dttClie As New DataTable
    Dim strWhere As String = ""
    Dim strListCodart As String = ""

    Dim strWhereArtico As String = ""
    '--- Variabili per restituzione CercaPrezzo
    Dim dttListini1 As DataTable = Nothing, dttListini2 As DataTable = Nothing, dttListini3 As DataTable = Nothing, dttListini4 As DataTable = Nothing, dttListini5 As DataTable = Nothing
    Dim strTipoOut1 As String = " ", strTipoOut2 As String = " ", strTipoOut3 As String = " ", strTipoOut4 As String = " ", strTipoOut5 As String = " "
    Dim dPrezzo1, dPrezzo2, dPrezzo3, dPrezzo4, dPrezzo5 As Decimal
    Dim dDaQuant1, dDaQuant2, dDaQuant3, dDaQuant4, dDaQuant5 As Decimal
    Dim dAQuant1, dAQuant2, dAQuant3, dAQuant4, dAQuant5 As Decimal
    Dim strUnmis1 As String = "", strUnmis2 As String = "", strUnmis3 As String = "", strUnmis4 As String = "", strUnmis5 As String = ""
    Dim lProgr1, lProgr2, lProgr3, lProgr4, lProgr5 As Integer
    Dim strPrzNet1 As String = "", strPrzNet2 As String = "", strPrzNet3 As String = "", strPrzNet4 As String = "", strPrzNet5 As String = ""
    Dim dPerQta As Decimal
    '--- Variabili per restituzione CercaSconti
    Dim dSc1 As Decimal, dSc2 As Decimal, dSc3 As Decimal
    Dim dSc4 As Decimal, dSc5 As Decimal, dSc6 As Decimal
    Dim dDaQuantsc As Decimal, dAQuantsc As Decimal
    Dim strUnmissc As String = "", strTipoOut, strNuovoSc As String
    Dim nPromosc As Integer
    Dim dSc1_2 As Decimal, dSc2_2 As Decimal, dSc3_2 As Decimal
    Dim dSc4_2 As Decimal, dSc5_2 As Decimal, dSc6_2 As Decimal
    Dim dDaQuantsc_2 As Decimal, dAQuantsc_2 As Decimal
    Dim strUnmissc_2 As String = "", strTipoOut_2, strNuovoSc_2 As String
    Dim nPromosc_2 As Integer
    Dim dSc1_3 As Decimal, dSc2_3 As Decimal, dSc3_3 As Decimal
    Dim dSc4_3 As Decimal, dSc5_3 As Decimal, dSc6_3 As Decimal
    Dim dDaQuantsc_3 As Decimal, dAQuantsc_3 As Decimal
    Dim strUnmissc_3 As String = "", strTipoOut_3, strNuovoSc_3 As String
    Dim nPromosc_3 As Integer
    Dim dSc1_4 As Decimal, dSc2_4 As Decimal, dSc3_4 As Decimal
    Dim dSc4_4 As Decimal, dSc5_4 As Decimal, dSc6_4 As Decimal
    Dim dDaQuantsc_4 As Decimal, dAQuantsc_4 As Decimal
    Dim strUnmissc_4 As String = "", strTipoOut_4, strNuovoSc_4 As String
    Dim nPromosc_4 As Integer
    '---
    Dim nClscan As Integer
    Dim nListinoAnagra As Integer
    Dim nListinoAnagra1 As Integer = 0
    Dim nListinoAnagra2 As Integer = 0
    Dim nListinoAnagra3 As Integer = 0
    Dim nListinoAnagra4 As Integer = 0
    '---
    Dim dtDaData1, dtDaData2, dtDaData3, dtDaData4, dtDaData5, dtDaDataSc, dtDaDataSc2, dtDaDataSc3, dtDaDataSc4 As Date
    Dim dtAData1, dtAData2, dtAData3, dtAData4, dtAData5, dtADataSc, dtADataSc2, dtADataSc3, dtADataSc4 As Date

    Dim lCoddestTmp As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bopSelezione0, bopSelezione1, stredCodlsar, stredConto, _
                                             stredDatagg, strcbTipo1, stredCodpromo1, stredCodlavo1, stredListino1, _
                                             stredCodvalu1, stredQuant1, bckSecondoPrezzo, strcbTipo2, stredCodpromo2, _
                                             stredCodlavo2, stredListino2, stredCodvalu2, stredQuant2, bckterzoPrezzo, _
                                             strcbTipo3, stredCodpromo3, stredCodlavo3, stredListino3, stredCodvalu3, _
                                             stredQuant3, bckQuartoPrezzo, strcbTipo4, stredCodpromo4, stredCodlavo4, _
                                             stredListino4, stredCodvalu4, stredQuant4, bckQuintoPrezzo, strcbTipo5, _
                                             stredCodpromo5, stredCodlavo5, stredListino5, stredCodvalu5, _
                                             stredQuant5, bcksconti1, strcbTipoSconto1, stredCodtpro1, _
                                             stredQuantScont1, bcksconti2, strcbTipoSconto2, stredCodtpro2, _
                                             stredQuantScont2, bcksconti3, strcbTipoSconto3, stredCodtpro3, _
                                             stredQuantScont3, bcksconti4, strcbTipoSconto4, stredCodtpro4, _
                                             stredQuantScont4, stredCodling, bckSeleziona, bckNoSoloSconti, _
                                             bGriglia, strError, nCodlsel, lCoddest, bNoDestdiv})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(37))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se è impostato strWhereClie allora devo eseguire la sua query, altrimenti uso il conto passato.
      If strWhereClie = "" Then
        If Not TestPreElabora(stredConto) Then Return False
        dttClie.Columns.Add("an_conto")
        dttClie.Columns.Add("xx_coddest")
        dttClie.Rows.Add(New Object() {stredConto, 0})
      Else
        If nCodlsel <> 0 Then
          If strWhereClie = "-1" Then strWhereClie = ""
          strWhereClie += IIf(strWhereClie = "", "{", "§").ToString & _
            "an_conto IN (SELECT lse_conto FROM listsel" & _
            " WHERE codditt = " & CStrSQL(strDittaCorrente) & _
            " AND lse_codlsel = " & nCodlsel & ")"
        End If
        oCldStli.GetClie(strDittaCorrente, strWhereClie, dttClie, bIsCRMUser)
      End If
      '--------------------------------------------------------------------------------------------------------------
      dttClie.AcceptChanges()

      'per ogni cliente, se necessario devo aggiungere le destinazioni diverse
      If bNoDestdiv = False And dttClie.Select("an_conto > 0").Length > 0 Then
        If lCoddest < 0 Then
          'aggiungo tutte le destinazioni aventi un listino (in vigore oppure no)
          If Not oCldStli.AggiungiDestdiv(strDittaCorrente, dttClie) Then Return False
        ElseIf lCoddest > 0 Then
          'solo una destinazione: la forzo sul cliente
          For Each dtrT As DataRow In dttClie.Rows
            dtrT!xx_coddest = lCoddest
          Next
        End If
      End If
      dttClie.AcceptChanges()

      'il test sotto non lavora se ho solo listini generici e sconti specifici e/o per classi sc. clienti
      '      If dttClie.Select("an_conto <> 0").Length > 0 Then
      '        For i = (dttClie.Rows.Count - 1) To 0 Step -1
      '          If oCldStli.ListinoEsistentePerConto(strDittaCorrente, NTSCInt(dttClie.Rows(i)!an_conto), stredDatagg) = False Then
      '            'dttClie.Rows(i).Delete()
      '            GoTo ValutazioneContoSuccessivo
      '          End If
      '          If (bcksconti1 = True) Or (bcksconti2 = True) Or (bcksconti3 = True) Or (bcksconti4 = True) Then
      '            If oCldStli.ScontiEsistentiPerConto(strDittaCorrente, NTSCInt(dttClie.Rows(i)!an_conto), stredDatagg) = False Then
      '            End If
      '          End If
      'ValutazioneContoSuccessivo:
      '        Next
      '        dttClie.AcceptChanges()
      '        If dttClie.Rows.Count = 0 Then
      '          strError = "Non esistono dati con queste caratteristiche."
      '          Return False
      '        End If
      '      End If
      '--------------------------------------------------------------------------------------------------------------
      oCldStli.ResetTblInstId("TTSTLI", False, lIITTStli)
      oCldStli.ResetTblInstId("TTLISTSAR", False, lIITTListsar)

      oCldStli.GetDataElabora(strDittaCorrente, bopSelezione0, bopSelezione1, strWhereFiar, strOrderBy, _
                              stredCodlsar, dsTmp)

      strWhereArtico = oCldStli.GetQueryArtElab(strDittaCorrente, bopSelezione0, strWhereFiar)
      If bopSelezione1 = True Then strWhereArtico = " AND lsa_codlsar = " & stredCodlsar

      Dim dtrClie() As DataRow = dttClie.Select("", "an_conto, xx_coddest")
      For z = 0 To dtrClie.Length - 1
        stredConto = NTSCStr(dtrClie(z)!an_conto)
        lCoddestTmp = NTSCInt(dtrClie(z)!xx_coddest)
        lPlusProgr = z * dsTmp.Tables("TTSTLI").Rows.Count - 1 ' in caso di + clienti per non dare chiave duplicata
        '------------------------------------------------------------------------------------------------------------
        If NTSCInt(stredConto) = 0 Then
          nClscan = 0
          nListinoAnagra = 0
        Else
          nClscan = oCldStli.RitornaClasseScontoAnagra(strDittaCorrente, NTSCInt(stredConto), nListinoAnagra)
        End If
        '----------------------------------------------------------------------------
        '--- CREA IL DYNASET A SECONDA DELLA SELEZIONE PRESCELTA
        '----------------------------------------------------------------------------
        If dsTmp.Tables("TTSTLI").Rows.Count = 0 Then
          strError = "Non esistono dati con queste caratteristiche."
          Return False
        Else
          ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128673202081579462, "Elaborazione in corso...")))
        End If
        '----------------------------------------------------------------------------
        '--- RIEMPIE LA TABELLA TEMPORANEA CON UN CICLO
        '----------------------------------------------------------------------------
        '----------- PREZZI -----------------
        Elabora_Listini(NTSCInt(stredCodpromo1), NTSCInt(stredCodlavo1), NTSCInt(stredConto), NTSCInt(stredListino1), strcbTipo1, NTSCDate(stredDatagg), _
                        NTSCInt(stredCodvalu1), NTSCDec(stredQuant1), NTSCInt(stredCodlsar), strWhereArtico, dttListini1, lCoddestTmp)

        If bckSecondoPrezzo Then
          Elabora_Listini(NTSCInt(stredCodpromo2), NTSCInt(stredCodlavo2), NTSCInt(stredConto), NTSCInt(stredListino2), strcbTipo2, NTSCDate(stredDatagg), _
                         NTSCInt(stredCodvalu2), NTSCDec(stredQuant2), NTSCInt(stredCodlsar), strWhereArtico, dttListini2, lCoddestTmp)
        End If
        If bckterzoPrezzo Then
          Elabora_Listini(NTSCInt(stredCodpromo3), NTSCInt(stredCodlavo3), NTSCInt(stredConto), NTSCInt(stredListino3), strcbTipo3, NTSCDate(stredDatagg), _
                        NTSCInt(stredCodvalu3), NTSCDec(stredQuant3), NTSCInt(stredCodlsar), strWhereArtico, dttListini3, lCoddestTmp)
        End If
        If bckQuartoPrezzo Then
          Elabora_Listini(NTSCInt(stredCodpromo4), NTSCInt(stredCodlavo4), NTSCInt(stredConto), NTSCInt(stredListino4), strcbTipo4, NTSCDate(stredDatagg), _
                          NTSCInt(stredCodvalu4), NTSCDec(stredQuant4), NTSCInt(stredCodlsar), strWhereArtico, dttListini4, lCoddestTmp)
        End If
        If bckQuintoPrezzo Then
          Elabora_Listini(NTSCInt(stredCodpromo5), NTSCInt(stredCodlavo5), NTSCInt(stredConto), NTSCInt(stredListino5), strcbTipo5, NTSCDate(stredDatagg), _
                         NTSCInt(stredCodvalu5), NTSCDec(stredQuant5), NTSCInt(stredCodlsar), strWhereArtico, dttListini5, lCoddestTmp)
        End If

        For i = 0 To dsTmp.Tables("TTSTLI").Rows.Count - 1
          If (i Mod 50) = 0 Then ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128673200280482132, "Elaborazione in corso... Record |" & i & "| di |" & dsTmp.Tables("TTSTLI").Rows.Count & "|, cliente\fornitore: |" & stredConto & "|")))
          '----------------------------------------------------------------------------------------------------------
          '--- Controlla che l'articolo sia presente in LISTINI/SCONTI (a seconda delle opzioni)
          '----------------------------------------------------------------------------------------------------------
          'If bckSeleziona = True Then
          '  If oCldStli.ListinoEsistentePerArticolo(strDittaCorrente, NTSCInt(stredConto), _
          '    NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_codart), stredDatagg) = False Then
          '    If (bcksconti1 = True) Or (bcksconti2 = True) Or (bcksconti3 = True) Or (bcksconti4 = True) Then
          '      If oCldStli.ScontiEsistentiPerArticolo(strDittaCorrente, NTSCInt(stredConto), _
          '        NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_codart), stredDatagg) = False Then GoTo SaltaInserimentoArticolo
          '    End If
          '    GoTo SaltaInserimentoArticolo
          '  End If
          'End If
          '---------------------------------------------------------------------------------------
          '--- Variabili per restituzione CercaSconti
          dSc1 = 0 : dSc2 = 0 : dSc3 = 0
          dSc4 = 0 : dSc5 = 0 : dSc6 = 0
          dDaQuantsc = 0 : dAQuantsc = 0
          strUnmissc = "" : strTipoOut = " " : strNuovoSc = "N"
          nPromosc = 0
          dSc1_2 = 0 : dSc2_2 = 0 : dSc3_2 = 0
          dSc4_2 = 0 : dSc5_2 = 0 : dSc6_2 = 0
          dDaQuantsc_2 = 0 : dAQuantsc_2 = 0
          strUnmissc_2 = "" : strTipoOut_2 = " " : strNuovoSc_2 = "N"
          nPromosc_2 = 0
          dSc1_3 = 0 : dSc2_3 = 0 : dSc3_3 = 0
          dSc4_3 = 0 : dSc5_3 = 0 : dSc6_3 = 0
          dDaQuantsc_3 = 0 : dAQuantsc_3 = 0
          strUnmissc_3 = "" : strTipoOut_3 = " " : strNuovoSc_3 = "N"
          nPromosc_3 = 0
          dSc1_4 = 0 : dSc2_4 = 0 : dSc3_4 = 0
          dSc4_4 = 0 : dSc5_4 = 0 : dSc6_4 = 0
          dDaQuantsc_4 = 0 : dAQuantsc_4 = 0
          strUnmissc_4 = "" : strTipoOut_4 = " " : strNuovoSc_4 = "N"
          nPromosc_4 = 0
          dtDaDataSc = Nothing : dtDaDataSc2 = Nothing : dtDaDataSc3 = Nothing : dtDaDataSc4 = Nothing
          dtADataSc = Nothing : dtADataSc2 = Nothing : dtADataSc3 = Nothing : dtADataSc4 = Nothing

          '------------------ SCONTI ----------------
          If bcksconti1 Then
            nListinoAnagra1 = nListinoAnagra
            If NTSCInt(stredConto) = 0 And oApp.oGvar.strSconClCliDaList = "S" Then
              'se la classe di sconto cli deve venir presa dal listino, se non ho indicato il cliente di riferimento
              'il listino viene preso dal n. di listino della cercaprezzo corrispondente
              nListinoAnagra1 = NTSCInt(stredListino1)
            End If
            Elabora_Sconti(dsTmp.Tables("TTSTLI").Rows(i), NTSCInt(stredConto), nClscan, strcbTipoSconto1, _
                           NTSCInt(stredCodtpro1), NTSCDate(stredDatagg), NTSCDec(stredQuantScont1), dSc1, dSc2, _
                           dSc3, dSc4, dSc5, dSc6, nPromosc, dtDaDataSc, dtADataSc, dDaQuantsc, dAQuantsc, strUnmissc, _
                           strTipoOut, nListinoAnagra1, lCoddestTmp)
          Else
            dSc1 = 0 : dSc2 = 0 : dSc3 = 0 : dSc4 = 0 : dSc5 = 0 : dSc6 = 0
            dDaQuantsc = 0 : dAQuantsc = 0 : strUnmissc = " "
          End If
          If bcksconti2 Then
            nListinoAnagra2 = nListinoAnagra
            If NTSCInt(stredConto) = 0 And oApp.oGvar.strSconClCliDaList = "S" Then
              'se la classe di sconto cli deve venir presa dal listino, se non ho indicato il cliente di riferimento
              'il listino viene preso dal n. di listino della cercaprezzo corrispondente
              nListinoAnagra2 = NTSCInt(stredListino1)
            End If
            Elabora_Sconti(dsTmp.Tables("TTSTLI").Rows(i), NTSCInt(stredConto), nClscan, strcbTipoSconto2, _
                           NTSCInt(stredCodtpro2), NTSCDate(stredDatagg), NTSCDec(stredQuantScont2), dSc1_2, dSc2_2, _
                           dSc3_2, dSc4_2, dSc5_2, dSc6_2, nPromosc_2, dtDaDataSc2, dtADataSc2, dDaQuantsc_2, dAQuantsc_2, _
                           strUnmissc_2, strTipoOut_2, nListinoAnagra2, lCoddestTmp)
          Else
            dSc1_2 = 0 : dSc2_2 = 0 : dSc3_2 = 0 : dSc4_2 = 0 : dSc5_2 = 0 : dSc6_2 = 0
            dDaQuantsc_2 = 0 : dAQuantsc_2 = 0 : strUnmissc_2 = " "
          End If
          If bcksconti3 Then
            nListinoAnagra3 = nListinoAnagra
            If NTSCInt(stredConto) = 0 And oApp.oGvar.strSconClCliDaList = "S" Then
              'se la classe di sconto cli deve venir presa dal listino, se non ho indicato il cliente di riferimento
              'il listino viene preso dal n. di listino della cercaprezzo corrispondente
              nListinoAnagra3 = NTSCInt(stredListino1)
            End If
            Elabora_Sconti(dsTmp.Tables("TTSTLI").Rows(i), NTSCInt(stredConto), nClscan, strcbTipoSconto3, _
                          NTSCInt(stredCodtpro3), NTSCDate(stredDatagg), NTSCDec(stredQuantScont3), dSc1_3, dSc2_3, _
                          dSc3_3, dSc4_3, dSc5_3, dSc6_3, nPromosc_3, dtDaDataSc3, dtADataSc3, dDaQuantsc_3, _
                          dAQuantsc_3, strUnmissc_3, strTipoOut_3, nListinoAnagra3, lCoddestTmp)
          Else
            dSc1_3 = 0 : dSc2_3 = 0 : dSc3_3 = 0 : dSc4_3 = 0 : dSc5_3 = 0 : dSc6_3 = 0
            dDaQuantsc_3 = 0 : dAQuantsc_3 = 0 : strUnmissc_3 = " "
          End If
          If bcksconti4 Then
            nListinoAnagra4 = nListinoAnagra
            If NTSCInt(stredConto) = 0 And oApp.oGvar.strSconClCliDaList = "S" Then
              'se la classe di sconto cli deve venir presa dal listino, se non ho indicato il cliente di riferimento
              'il listino viene preso dal n. di listino della cercaprezzo corrispondente
              nListinoAnagra4 = NTSCInt(stredListino1)
            End If
            Elabora_Sconti(dsTmp.Tables("TTSTLI").Rows(i), NTSCInt(stredConto), nClscan, strcbTipoSconto4, _
                           NTSCInt(stredCodtpro4), NTSCDate(stredDatagg), NTSCDec(stredQuantScont4), dSc1_4, dSc2_4, _
                           dSc3_4, dSc4_4, dSc5_4, dSc6_4, nPromosc_4, dtDaDataSc4, dtADataSc4, dDaQuantsc_4, _
                           dAQuantsc_4, strUnmissc_4, strTipoOut_4, nListinoAnagra4, lCoddestTmp)
          Else
            dSc1_4 = 0 : dSc2_4 = 0 : dSc3_4 = 0 : dSc4_4 = 0 : dSc5_4 = 0 : dSc6_4 = 0
            dDaQuantsc_4 = 0 : dAQuantsc_4 = 0 : strUnmissc_4 = " "
          End If

          If Trim(strUnmissc) = "" Then strUnmissc = " "

          'Se è " " vuol dire che non ha trovato nessuno sconto, allora lo metto ad un valore accettato e imposto il flag di nuovo sconto
          'Serve solo per la tabella listses
          If strTipoOut.Trim = "" Then strTipoOut = strcbTipoSconto1 : strNuovoSc = "S"
          If strTipoOut_2.Trim = "" Then strTipoOut_2 = strcbTipoSconto2 : strNuovoSc_2 = "S"
          If strTipoOut_3.Trim = "" Then strTipoOut_3 = strcbTipoSconto3 : strNuovoSc_3 = "S"
          If strTipoOut_4.Trim = "" Then strTipoOut_4 = strcbTipoSconto4 : strNuovoSc_4 = "S"

          GetDatiListino(dttListini1, NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_codart), NTSCInt(dsTmp.Tables("TTSTLI").Rows(i)!fase), strTipoOut1, _
                         dPrezzo1, dDaQuant1, dAQuant1, strUnmis1, lProgr1, strPrzNet1, dPerQta, dtDaData1, dtAData1)
          GetDatiListino(dttListini2, NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_codart), NTSCInt(dsTmp.Tables("TTSTLI").Rows(i)!fase), strTipoOut2, _
                         dPrezzo2, dDaQuant2, dAQuant2, strUnmis2, lProgr2, strPrzNet2, 0, dtDaData2, dtAData2)
          GetDatiListino(dttListini3, NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_codart), NTSCInt(dsTmp.Tables("TTSTLI").Rows(i)!fase), strTipoOut3, _
                         dPrezzo3, dDaQuant3, dAQuant3, strUnmis3, lProgr3, strPrzNet3, 0, dtDaData3, dtAData3)
          GetDatiListino(dttListini4, NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_codart), NTSCInt(dsTmp.Tables("TTSTLI").Rows(i)!fase), strTipoOut4, _
                         dPrezzo4, dDaQuant4, dAQuant4, strUnmis4, lProgr4, strPrzNet4, 0, dtDaData4, dtAData4)
          GetDatiListino(dttListini5, NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_codart), NTSCInt(dsTmp.Tables("TTSTLI").Rows(i)!fase), strTipoOut5, _
                         dPrezzo5, dDaQuant5, dAQuant5, strUnmis5, lProgr5, strPrzNet5, 0, dtDaData5, dtAData5)

          'La stampa su griglia lavora su una tabella residente (LISTSES), le altre stampe su una temporanea (TTSTLI)
          If (bckSeleziona = True) And _
             (dSc1 = 0) And (dSc2 = 0) And (dSc3 = 0) And (dSc4 = 0) And (dSc5 = 0) And (dSc6 = 0) And _
             (dSc1_2 = 0) And (dSc2_2 = 0) And (dSc3_2 = 0) And (dSc4_2 = 0) And (dSc5_2 = 0) And (dSc6_2 = 0) And _
             (dSc1_3 = 0) And (dSc2_3 = 0) And (dSc3_3 = 0) And (dSc4_3 = 0) And (dSc5_3 = 0) And (dSc6_3 = 0) And _
             (dSc1_4 = 0) And (dSc2_4 = 0) And (dSc3_4 = 0) And (dSc4_4 = 0) And (dSc5_4 = 0) And (dSc6_4 = 0) And _
             (dPrezzo1 = 0) And (dPrezzo2 = 0) And (dPrezzo3 = 0) And (dPrezzo4 = 0) And (dPrezzo5 = 0) Then
            GoTo SaltaInserimentoArticolo
          End If
          If bGriglia Then
            oCldStli.InsertListSes(strDittaCorrente, 1, NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_codart), _
                                   nClscan, NTSCInt(dsTmp.Tables("TTSTLI").Rows(i)!ar_clascon), _
                                   dSc1, dSc2, dSc3, dSc4, _
                                   dSc5, dSc6, dDaQuantsc, dAQuantsc, _
                                   strTipoOut, dtDaDataSc, dtADataSc, nPromosc, _
                                   strNuovoSc, dSc1_2, dSc2_2, dSc3_2, dSc4_2, _
                                   dSc5_2, dSc6_2, dDaQuantsc_2, dAQuantsc_2, _
                                   strTipoOut_2, dtDaDataSc2, dtADataSc2, nPromosc_2, _
                                   strNuovoSc_2, dSc1_3, dSc2_3, dSc3_3, dSc4_3, _
                                   dSc5_3, dSc6_3, dDaQuantsc_3, dAQuantsc_3, _
                                   strTipoOut_3, dtDaDataSc3, dtADataSc3, nPromosc_3, _
                                   strNuovoSc_3, dSc1_4, dSc2_4, dSc3_4, dSc4_4, _
                                   dSc5_4, dSc6_4, dDaQuantsc_4, dAQuantsc_4, _
                                   strTipoOut_4, dtDaDataSc4, dtADataSc4, nPromosc_4, _
                                   strNuovoSc_4, NTSCInt(stredListino1), dtDaData1, dtAData1, strTipoOut1, NTSCInt(stredCodlavo1), dPrezzo1, dDaQuant1, _
                                   dAQuant1, strUnmis1, NTSCInt(stredCodvalu1), lProgr1, _
                                   NTSCInt(stredListino2), dtDaData2, dtAData2, strTipoOut2, NTSCInt(stredCodlavo2), dPrezzo2, _
                                   dDaQuant2, dAQuant2, _
                                   strUnmis2, NTSCInt(stredCodvalu2), lProgr2, _
                                   NTSCInt(stredListino3), dtDaData3, dtAData3, strTipoOut3, NTSCInt(stredCodlavo3), dPrezzo3, _
                                   dDaQuant3, dAQuant3, _
                                   strUnmis3, NTSCInt(stredCodvalu3), lProgr3, _
                                   NTSCInt(stredListino4), dtDaData4, dtAData4, strTipoOut4, NTSCInt(stredCodlavo4), dPrezzo4, _
                                   dDaQuant4, dAQuant4, _
                                   strUnmis4, NTSCInt(stredCodvalu4), lProgr4, _
                                   NTSCInt(stredListino5), dtDaData5, dtAData5, strTipoOut5, NTSCInt(stredCodlavo5), dPrezzo5, _
                                   dDaQuant5, dAQuant5, _
                                   strUnmis5, NTSCInt(stredCodvalu5), lProgr5, _
                                   NTSCDec(dsTmp.Tables("TTSTLI").Rows(i)!fase), stredCodling, dPerQta, strPrzNet1, _
                                   strPrzNet2, strPrzNet3, strPrzNet4, strPrzNet5, _
                                   NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_note), NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_descr), _
                                   NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_desint), NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_unmis), stredConto, _
                                   NTSCInt(stredCodpromo1), NTSCInt(stredCodpromo2), NTSCInt(stredCodpromo3), _
                                   NTSCInt(stredCodpromo4), NTSCInt(stredCodpromo5), lCoddestTmp)
            oCldStli.InsertTTLISTSAR(strDittaCorrente, dsTmp.Tables("TTSTLI").Rows(i), lIITTListsar)
          Else
            oCldStli.InsertTemp(strDittaCorrente, lIITTStli, NTSCInt(i + 1) + lPlusProgr, NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_codart), _
                                dSc1, dSc2, dSc3, dSc4, _
                                dSc5, dSc6, dDaQuantsc, dAQuantsc, _
                                strUnmissc, dSc1_2, dSc2_2, dSc3_2, dSc4_2, _
                                dSc5_2, dSc6_2, dDaQuantsc_2, dAQuantsc_2, _
                                strUnmissc_2, dSc1_3, dSc2_3, dSc3_3, dSc4_3, _
                                dSc5_3, dSc6_3, dDaQuantsc_3, dAQuantsc_3, _
                                strUnmissc_3, dSc1_4, dSc2_4, dSc3_4, dSc4_4, _
                                dSc5_4, dSc6_4, dDaQuantsc_4, dAQuantsc_4, _
                                strUnmissc_4, dPrezzo1, stredCodvalu1, dDaQuant1, _
                                dAQuant1, dPerQta, strUnmis1, dPrezzo2, _
                                stredCodvalu2, dDaQuant2, dAQuant2, dPerQta, _
                                strUnmis2, dPrezzo3, stredCodvalu3, dDaQuant3, _
                                dAQuant3, dPerQta, strUnmis3, dPrezzo4, _
                                stredCodvalu4, dDaQuant4, dAQuant4, dPerQta, _
                                strUnmis4, dPrezzo5, _
                                stredCodvalu5, dDaQuant5, dAQuant5, dPerQta, _
                                strUnmis5, NTSCDec(dsTmp.Tables("TTSTLI").Rows(i)!fase), stredCodling, strPrzNet1, _
                                strPrzNet2, strPrzNet3, strPrzNet4, strPrzNet5, lIITTListsar, _
                                NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_note), NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_descr), _
                                NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_desint), NTSCStr(dsTmp.Tables("TTSTLI").Rows(i)!ar_unmis), stredConto, lCoddestTmp)
          End If
SaltaInserimentoArticolo:
        Next
      Next
      '----------------------------------------------------------------------------
      ' Cancella i dati dal temporaneo che hanno tutti i valori a zero
      '----------------------------------------------------------------------------
      If bckSeleziona Then
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128673202561023278, "Cancellazione articoli con dati a zero in corso...")))

        If bGriglia Then
          oCldStli.DeleteListSes(strDittaCorrente)

          oCldStli.GetListSes(strDittaCorrente, dsTTstli)
        Else
          oCldStli.DeleteTemp(strDittaCorrente, lIITTStli, lIITTListsar)

          oCldStli.GetTTstli(strDittaCorrente, dsTTstli)
        End If

        If dsTTstli.Tables("TTSTLI").Rows.Count = 0 Then
          strError = "Non esistono dati con queste caratteristiche."
          Return False
        End If
      End If
      '----------------------------------------------------------------------------
      '--- Cancella i records dal temporaneo che hanno almeno uno dei sei sconti <> 0
      '--- e tutti e quattro i prezzi = 0
      '--- (funziona solo se è selezionato il CheckBox relativo agli sconti)
      '----------------------------------------------------------------------------
      If (bcksconti1 Or bcksconti2 Or bcksconti3 Or bcksconti4) And (bckNoSoloSconti) Then
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128673202711122236, "Cancellazione articoli in presenza di soli sconti in corso...")))

        If bGriglia Then
          oCldStli.DeleteListSesSconti(strDittaCorrente)

          oCldStli.GetListSes(strDittaCorrente, dsTTstli)
        Else
          oCldStli.DeleteTempSconti(strDittaCorrente, lIITTStli, lIITTListsar)

          oCldStli.GetTTstli(strDittaCorrente, dsTTstli)
        End If

        If dsTTstli.Tables("TTSTLI").Rows.Count = 0 Then
          strError = "Non esistono dati con queste caratteristiche."
          Return False
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se indicato il codice lingua, preleva i dati descrittivi di quegli articoli che hanno
      '--- descrizioni in lingua nella tabella relativa (ARTVAL)
      '-----------------------------------------------------------------------------------------
      If NTSCInt(stredCodling) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128673209415703547, "Importazione descrizioni in lingua in corso...")))

        oCldStli.ElaboraArtval(strDittaCorrente, lIITTStli, lIITTListsar, stredCodling)
      End If
      '-----------------------------------------------------------------------------------------
      If (bSoloClie = True) Or (bMultiClie = True) Then
        If oCldStli.RiempiTTSTLIDaLISTSES(strDittaCorrente, 1, lIITTStli, lIITTListsar) = False Then Return False
      End If
      '-----------------------------------------------------------------------------------------
      Return True

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      If Not dsTmp Is Nothing Then
        dsTmp.Clear() : dsTmp.Dispose()
      End If
      If Not dsTTstli Is Nothing Then
        dsTTstli.Clear() : dsTTstli.Dispose()
      End If
    End Try
  End Function
  Public Overridable Function Elabora_Listini(ByVal lCodpromo As Integer, ByVal lCodlavo As Integer, ByVal lConto As Integer, ByVal lListino As Integer, _
                                            ByVal strTipo As String, ByVal dtDatagg As Date, ByVal lCodvalu As Integer, _
                                            ByVal dQuant As Decimal, ByVal lListaSel As Integer, ByVal strWhereArtico As String, ByRef dttListini As DataTable) As Boolean
    Try
      'obsoleta
      Return Elabora_Listini(lCodpromo, lCodlavo, lConto, lListino, strTipo, dtDatagg, lCodvalu, dQuant, lListaSel, strWhereArtico, dttListini, 0)

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
  Public Overridable Function Elabora_Listini(ByVal lCodpromo As Integer, ByVal lCodlavo As Integer, ByVal lConto As Integer, ByVal lListino As Integer, _
                                              ByVal strTipo As String, ByVal dtDatagg As Date, ByVal lCodvalu As Integer, _
                                              ByVal dQuant As Decimal, ByVal lListaSel As Integer, ByVal strWhereArtico As String, ByRef dttListini As DataTable, _
                                              ByVal lCoddest As Integer) As Boolean
    Dim bConspromo As Boolean = (lCodpromo <> 0)
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lCodpromo, lCodlavo, lConto, lListino, strTipo, dtDatagg, lCodvalu, dQuant, lListaSel, strWhereArtico, dttListini, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttListini = CType(oIn(10), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      '---------------------------------------------------------------------------------------
      Dim oCondCommerciali As NTSCondCommerciali = IstanziaNTSCondCommerciali()
      oCondCommerciali.bCalcolaPrezzo = True
      With oCondCommerciali.Input
        .strDitta = strDittaCorrente
        .nCodlavo = lCodlavo
        .lConto = lConto
        .lDestdiv = lCoddest
        .nListino = lListino
        .strTipoval = strTipo
        .bConspromo = bConspromo
        .nCodpromo = lCodpromo
        .dtDatdoc = dtDatagg
        .nCodvalu = lCodvalu
        .dQuant = dQuant
        .bNoPrelist = True
        .lListaSel = lListaSel
        .strWhereArtico = strWhereArtico
      End With
      '---------------------------------------------------------------------------------------
      CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
      '---------------------------------------------------------------------------------------
      With oCondCommerciali.OutputPrezzo
        dttListini = .dttListini
      End With
      '---------------------------------------------------------------------------------------
      For Each dtrList As DataRow In dttListini.Rows
        If NTSCDec(dtrList!dPrezzo) = 0 Then dtrList!lProgr = 0
        If NTSCDate(dtrList!dtAData) = New Date(1900, 1, 1) Then dtrList!dtAData = New Date(2099, 12, 31)
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
  Public Overridable Function Elabora_Sconti(ByVal dtrRow As DataRow, ByVal lConto As Integer, ByVal nClscan As Integer, ByVal strTipoSconto As String, _
                                            ByVal lCodtpro As Integer, ByVal dtDatagg As Date, ByVal dQuantScont As Decimal, ByRef dSc1 As Decimal, _
                                            ByRef dSc2 As Decimal, ByRef dSc3 As Decimal, ByRef dSc4 As Decimal, ByRef dSc5 As Decimal, ByRef dSc6 As Decimal, _
                                            ByRef nPromosc As Integer, ByRef dtDaDataSc As Date, ByRef dtADataSc As Date, ByRef dDaQuantsc As Decimal, _
                                            ByRef dAQuantsc As Decimal, ByRef strUnmissc As String, ByRef strTipoOut As String) As Boolean
    Try
      'obsoleta
      Return Elabora_Sconti(dtrRow, lConto, nClscan, strTipoSconto, lCodtpro, dtDatagg, dQuantScont, dSc1, _
                            dSc2, dSc3, dSc4, dSc5, dSc6, nPromosc, dtDaDataSc, dtADataSc, dDaQuantsc, _
                            dAQuantsc, strUnmissc, strTipoOut, 0, 0)
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
  Public Overridable Function Elabora_Sconti(ByVal dtrRow As DataRow, ByVal lConto As Integer, ByVal nClscan As Integer, ByVal strTipoSconto As String, _
                                             ByVal lCodtpro As Integer, ByVal dtDatagg As Date, ByVal dQuantScont As Decimal, ByRef dSc1 As Decimal, _
                                             ByRef dSc2 As Decimal, ByRef dSc3 As Decimal, ByRef dSc4 As Decimal, ByRef dSc5 As Decimal, ByRef dSc6 As Decimal, _
                                             ByRef nPromosc As Integer, ByRef dtDaDataSc As Date, ByRef dtADataSc As Date, ByRef dDaQuantsc As Decimal, _
                                             ByRef dAQuantsc As Decimal, ByRef strUnmissc As String, ByRef strTipoOut As String, ByVal nListino As Integer) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrRow, lConto, nClscan, strTipoSconto, lCodtpro, dtDatagg, dQuantScont, dSc1, _
                            dSc2, dSc3, dSc4, dSc5, dSc6, nPromosc, dtDaDataSc, dtADataSc, dDaQuantsc, _
                            dAQuantsc, strUnmissc, strTipoOut, nListino})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dSc1 = NTSCDec(oIn(7))
        dSc2 = NTSCDec(oIn(8))
        dSc3 = NTSCDec(oIn(9))
        dSc4 = NTSCDec(oIn(10))
        dSc5 = NTSCDec(oIn(11))
        dSc6 = NTSCDec(oIn(12))
        nPromosc = NTSCInt(oIn(13))
        dtDaDataSc = NTSCDate(oIn(14))
        dtADataSc = NTSCDate(oIn(15))
        dDaQuantsc = NTSCDec(oIn(16))
        dAQuantsc = NTSCDec(oIn(17))
        strUnmissc = NTSCStr(oIn(18))
        strTipoOut = NTSCStr(oIn(19))
        Return CBool(oOut)
      End If

      'obsoleta
      Return Elabora_Sconti(dtrRow, lConto, nClscan, strTipoSconto, lCodtpro, dtDatagg, dQuantScont, dSc1, _
                                  dSc2, dSc3, dSc4, dSc5, dSc6, nPromosc, dtDaDataSc, dtADataSc, dDaQuantsc, _
                                  dAQuantsc, strUnmissc, strTipoOut, nListino, 0)
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
  Public Overridable Function Elabora_Sconti(ByVal dtrRow As DataRow, ByVal lConto As Integer, ByVal nClscan As Integer, ByVal strTipoSconto As String, _
                                             ByVal lCodtpro As Integer, ByVal dtDatagg As Date, ByVal dQuantScont As Decimal, ByRef dSc1 As Decimal, _
                                             ByRef dSc2 As Decimal, ByRef dSc3 As Decimal, ByRef dSc4 As Decimal, ByRef dSc5 As Decimal, ByRef dSc6 As Decimal, _
                                             ByRef nPromosc As Integer, ByRef dtDaDataSc As Date, ByRef dtADataSc As Date, ByRef dDaQuantsc As Decimal, _
                                             ByRef dAQuantsc As Decimal, ByRef strUnmissc As String, ByRef strTipoOut As String, ByVal nListino As Integer, _
                                             ByVal lCoddest As Integer) As Boolean
    Dim bConspromoSc As Boolean = (lCodtpro <> 0)
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrRow, lConto, nClscan, strTipoSconto, lCodtpro, dtDatagg, dQuantScont, dSc1, _
                            dSc2, dSc3, dSc4, dSc5, dSc6, nPromosc, dtDaDataSc, dtADataSc, dDaQuantsc, _
                            dAQuantsc, strUnmissc, strTipoOut, nListino, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dSc1 = NTSCDec(oIn(7))
        dSc2 = NTSCDec(oIn(8))
        dSc3 = NTSCDec(oIn(9))
        dSc4 = NTSCDec(oIn(10))
        dSc5 = NTSCDec(oIn(11))
        dSc6 = NTSCDec(oIn(12))
        nPromosc = NTSCInt(oIn(13))
        dtDaDataSc = NTSCDate(oIn(14))
        dtADataSc = NTSCDate(oIn(15))
        dDaQuantsc = NTSCDec(oIn(16))
        dAQuantsc = NTSCDec(oIn(17))
        strUnmissc = NTSCStr(oIn(18))
        strTipoOut = NTSCStr(oIn(19))
        Return CBool(oOut)
      End If

      '--- Preparazione input ---
      Dim oCondCommerciali As NTSCondCommerciali = IstanziaNTSCondCommerciali()
      oCondCommerciali.bCalcolaSconti = True
      With oCondCommerciali.Input
        .strDitta = strDittaCorrente
        .strCodart = NTSCStr(dtrRow!ar_codart)
        .lConto = lConto
        .lDestdiv = lCoddest
        .nClscan = nClscan
        .nClscar = NTSCInt(dtrRow!ar_clascon)
        .strTipoval = strTipoSconto
        .bConspromo = bConspromoSc
        .nCodpromo = lCodtpro
        .dtDatdoc = dtDatagg
        .dQuant = dQuantScont
        .nListino = nListino
      End With
      '--- Elaborazione ---
      CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
      '--- Spostamento dati output ---
      With oCondCommerciali.OutputSconti
        dSc1 = .dSconto1
        dSc2 = .dSconto2
        dSc3 = .dSconto3
        dSc4 = .dSconto4
        dSc5 = .dSconto5
        dSc6 = .dSconto6
        nPromosc = .nPromo
        dtDaDataSc = .dtDaData
        dtADataSc = .dtAData
        dDaQuantsc = .dDaQuant
        dAQuantsc = .dAquant
        strUnmissc = .strUnmis
        strTipoOut = .strTipoval
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

  Public Overridable Function GetDatiListino(ByVal dttListini As DataTable, ByVal strCodart As String, ByVal nFase As Integer, _
                                             ByRef strTipoOut As String, ByRef dPrezzo As Decimal, ByRef dDaQuant As Decimal, _
                                             ByRef dAQuant As Decimal, ByRef strUnmis As String, ByRef lProgr As Integer, _
                                             ByRef strPrzNet As String, ByRef dPerQta As Decimal) As Boolean
    Try

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
  Public Overridable Function GetDatiListino(ByVal dttListini As DataTable, ByVal strCodart As String, ByVal nFase As Integer, _
                                             ByRef strTipoOut As String, ByRef dPrezzo As Decimal, ByRef dDaQuant As Decimal, _
                                             ByRef dAQuant As Decimal, ByRef strUnmis As String, ByRef lProgr As Integer, _
                                             ByRef strPrzNet As String, ByRef dPerQta As Decimal, ByRef dtDaData As Date, _
                                             ByRef dtAData As Date) As Boolean
    Dim strSQL As String = ""
    Dim dtrRow() As DataRow
    Dim dttTmp As New DataTable
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttListini, strCodart, nFase, strTipoOut, dPrezzo, dDaQuant, dAQuant, strUnmis, lProgr, _
                                             strPrzNet, dPerQta, dtDaData, dtAData})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strTipoOut = CType(oIn(3), String)
        dPrezzo = CType(oIn(4), Decimal)
        dDaQuant = CType(oIn(5), Decimal)
        dAQuant = CType(oIn(6), Decimal)
        strUnmis = CType(oIn(7), String)
        lProgr = CType(oIn(8), Integer)
        strPrzNet = CType(oIn(9), String)
        dPerQta = CType(oIn(10), Decimal)
        dtDaData = CType(oIn(11), Date)
        dtAData = CType(oIn(12), Date)
        Return CBool(oOut)
      End If
      '----------------


      If dttListini Is Nothing Then
        strTipoOut = " "
        dPrezzo = 0
        dDaQuant = 0
        dAQuant = 9999999999
        strUnmis = " "
        lProgr = 0
        dPerQta = 1
        oCldStli.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then dPerQta = NTSCDec(dttTmp.Rows(0)!ar_perqta)
        dttTmp.Clear()
        strPrzNet = "N"
        dtDaData = New Date(1900, 1, 1)
        dtAData = New Date(2099, 12, 31)
        Return True
      End If

      dtrRow = dttListini.Select("strCodart = " & CStrSQL(strCodart) & " AND nFase = " & NTSCInt(nFase))

      If dtrRow.Length > 0 Then
        strTipoOut = NTSCStr(dtrRow(0)!strTipoOut)
        dPrezzo = NTSCDec(dtrRow(0)!dPrezzo)
        dDaQuant = NTSCDec(dtrRow(0)!dDaQuant)
        dAQuant = NTSCDec(dtrRow(0)!dAQuant)
        strUnmis = NTSCStr(IIf(NTSCStr(dtrRow(0)!strUnmisout) = "", " ", NTSCStr(dtrRow(0)!strUnmisout)))
        lProgr = NTSCInt(dtrRow(0)!lProgr)
        dPerQta = NTSCDec(dtrRow(0)!dPerQta)
        strPrzNet = NTSCStr(dtrRow(0)!strPrzNet)
        dtDaData = NTSCDate(dtrRow(0)!dtDatAggOut)
        dtAData = NTSCDate(dtrRow(0)!dtAData)
      Else
        strTipoOut = " "
        dPrezzo = 0
        dDaQuant = 0
        dAQuant = 9999999999
        strUnmis = " "
        lProgr = 0
        dPerQta = 1
        oCldStli.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then dPerQta = NTSCDec(dttTmp.Rows(0)!ar_perqta)
        dttTmp.Clear()
        strPrzNet = "N"
        dtDaData = New Date(1900, 1, 1)
        dtAData = New Date(2099, 12, 31)
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetQueryStampaWord() As String
    Try
      Return oCldStli.GetQueryStampaWord(strDittaCorrente, lIITTStli, lIITTListsar, bStampaWordRaggruppata, _
                                         strWhereFiar, strOrderBy)
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

  Public Overridable Function GetListsar(ByVal stredCodlsar As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldStli.GetListsar(strDittaCorrente, stredCodlsar, ds)
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

  Public Overridable Function GetListSes(ByRef ds As DataSet) As Boolean
    Try
      Return oCldStli.GetListSes(strDittaCorrente, ds)
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

  Public Overridable Function CancellaListSes() As Boolean
    Try
      Return oCldStli.CancellaListSes(strDittaCorrente)
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

#Region "Lise"
  Public Overridable Function GetDataLise(ByRef ds As DataSet) As Boolean
    Try
      oCldStli.GetDataLise(strDittaCorrente, 1, ds)

      dsSharedLise = ds

      AddHandler dsSharedLise.Tables("LISTSES").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsSharedLise.Tables("LISTSES").ColumnChanged, AddressOf AfterColUpdate

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

  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If

      bHasChanges = True

      '----- --------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      'ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub BeforeColUpdate_ls_prz1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato1 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_dadata1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato1 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_adata1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato1 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_unmis1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato1 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_daquant1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato1 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_aquant1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato1 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_tipo1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bMody As Boolean = True
    Try
      Select Case e.ProposedValue.ToString
        Case " " ' Generico art.
          bMody = True
        Case "C" ' Specifico clie.
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", "è possibile selezionare 'Specifico cliente' solo se il conto è diverso da 0"))
            bMody = False
          End If
        Case "F" ' Specifico forn.
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", "è possibile selezionare 'Specifico fornitore' solo se il conto è diverso da 0"))
            bMody = True
          End If
        Case Else
          bMody = False
      End Select

      If bMody Then e.Row!ls_modificato1 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_prznet1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato1 = "S"
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

  Public Overridable Sub BeforeColUpdate_ls_prz2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_dadata2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_adata2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_unmis2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_daquant2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_aquant2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_tipo2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bMody As Boolean = True
    Try
      Select Case e.ProposedValue.ToString
        Case " " ' Generico art.
          bMody = True
        Case "C" ' Specifico clie.
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", "è possibile selezionare 'Specifico cliente' solo se il conto è diverso da 0"))
            bMody = False
          End If
        Case "F" ' Specifico forn.
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", "è possibile selezionare 'Specifico fornitore' solo se il conto è diverso da 0"))
            bMody = True
          End If
        Case Else
          bMody = False
      End Select

      If bMody Then e.Row!ls_modificato2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_prznet2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato2 = "S"
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

  Public Overridable Sub BeforeColUpdate_ls_prz3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_dadata3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_adata3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_unmis3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_daquant3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_aquant3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_tipo3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bMody As Boolean = True
    Try
      Select Case e.ProposedValue.ToString
        Case " " ' Generico art.
          bMody = True
        Case "C" ' Specifico clie.
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", "è possibile selezionare 'Specifico cliente' solo se il conto è diverso da 0"))
            bMody = False
          End If
        Case "F" ' Specifico forn.
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", "è possibile selezionare 'Specifico fornitore' solo se il conto è diverso da 0"))
            bMody = True
          End If
        Case Else
          bMody = False
      End Select

      If bMody Then e.Row!ls_modificato3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_prznet3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato3 = "S"
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

  Public Overridable Sub BeforeColUpdate_ls_prz4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_dadata4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_adata4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_unmis4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_daquant4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_aquant4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_tipo4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bMody As Boolean = True
    Try
      Select Case e.ProposedValue.ToString
        Case " " ' Generico art.
          bMody = True
        Case "C" ' Specifico clie.
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", "è possibile selezionare 'Specifico cliente' solo se il conto è diverso da 0"))
            bMody = False
          End If
        Case "F" ' Specifico forn.
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", "è possibile selezionare 'Specifico fornitore' solo se il conto è diverso da 0"))
            bMody = True
          End If
        Case Else
          bMody = False
      End Select

      If bMody Then e.Row!ls_modificato4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_prznet4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato4 = "S"
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

  Public Overridable Sub BeforeColUpdate_ls_prz5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato5 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_dadata5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato5 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_adata5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato5 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_unmis5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato5 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_daquant5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato5 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_aquant5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato5 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_tipo5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bMody As Boolean = True
    Try
      Select Case e.ProposedValue.ToString
        Case " " ' Generico art.
          bMody = True
        Case "C" ' Specifico clie.
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", "è possibile selezionare 'Specifico cliente' solo se il conto è diverso da 0"))
            bMody = False
          End If
        Case "F" ' Specifico forn.
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", "è possibile selezionare 'Specifico fornitore' solo se il conto è diverso da 0"))
            bMody = True
          End If
        Case Else
          bMody = False
      End Select

      If bMody Then e.Row!ls_modificato5 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_prznet5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificato5 = "S"
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

  Public Overridable Sub BeforeColUpdate_ls_scont1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont6(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scdaquant(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scaquant(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_dadatasc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_adatasc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_tiposc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      Select Case NTSCStr(e.ProposedValue)
        Case "F"
          'Generico cliente/fornitore
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507917581449, "Negli sconti generici cliente/fornitore è necessario avere il conto diverso da 0")))
            e.ProposedValue = e.Row!ls_tiposc
            Return
          End If
        Case "E"
          'Generico articolo
          If NTSCStr(e.Row!ls_codart).Trim = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507931175808, "Negli sconti generici articolo è necessario avere l'articolo diverso da ''")))
            e.ProposedValue = e.Row!ls_tiposc
            Return
          End If
        Case "B"
          'Per classe articolo
          If NTSCInt(e.Row!ls_conto) = 0 Or NTSCInt(e.Row!ls_clscar) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507956489442, "Negli sconti per cliente / classe articolo è necessario avere: conto diverso da 0, classe articolo diversa da 0")))
            e.ProposedValue = e.Row!ls_tiposc
            Return
          End If
        Case "C"
          'Per classe cliente/fornitore
          If oApp.oGvar.strSconClCliDaList = "S" Then
            If NTSCStr(e.Row!ls_codart).Trim = "" Or NTSCInt(e.Row!ls_listino1) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507973052689, "Negli sconti per articolo / listino è necessario avere: articolo diverso da '', primo listino diverso da 0")))
              e.ProposedValue = e.Row!ls_tiposc
              Return
            End If
          Else
            If NTSCStr(e.Row!ls_codart).Trim = "" Or NTSCInt(e.Row!ls_clscan) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507973052684, "Negli sconti per articolo / classe cliente è necessario avere: articolo diverso da '', classe cliente diversa da 0")))
              e.ProposedValue = e.Row!ls_tiposc
              Return
            End If
          End If
        Case "A"
          'Specifico Articolo cli/forn
          If NTSCInt(e.Row!ls_conto) = 0 Or NTSCStr(e.Row!ls_codart).Trim = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507987428328, "Negli sconti specifici cliente (o fornitore) / articolo è necessario avere: conto diverso da 0, articolo diverso da ''")))
            e.ProposedValue = e.Row!ls_tiposc
            Return
          End If
        Case "D"
          'Per classe art / classe cli/for
          If NTSCInt(e.Row!ls_clscan) = 0 Or NTSCInt(e.Row!ls_clscar) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219508002116486, "Negli sconti per classe cliente (o fornitore) / classe articolo è necessario avere: classe articolo diversa da 0, classe cliente diversa da 0")))
            e.ProposedValue = e.Row!ls_tiposc
            Return
          End If
      End Select

      e.Row!ls_modificatosc = "S"

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

  Public Overridable Sub BeforeColUpdate_ls_scont1_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont2_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont3_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont4_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont5_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont6_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scdaquant_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scaquant_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_dadatasc_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_adatasc_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_2 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_tiposc_2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      Select Case NTSCStr(e.ProposedValue)
        Case "F"
          'Generico cliente/fornitore
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507820077081, "Negli sconti generici cliente/fornitore è necessario avere il conto diverso da 0")))
            e.ProposedValue = e.Row!ls_tiposc_2
            Return
          End If
        Case "E"
          'Generico articolo
          If NTSCStr(e.Row!ls_codart).Trim = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507837265351, "Negli sconti generici articolo è necessario avere l'articolo diverso da ''")))
            e.ProposedValue = e.Row!ls_tiposc_2
            Return
          End If
        Case "B"
          'Per classe articolo
          If NTSCInt(e.Row!ls_conto) = 0 Or NTSCInt(e.Row!ls_clscar) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507845546972, "Negli sconti per cliente / classe articolo è necessario avere: conto diverso da 0, classe articolo diversa da 0")))
            e.ProposedValue = e.Row!ls_tiposc_2
            Return
          End If
        Case "C"
          'Per classe cliente/fornitore
          If oApp.oGvar.strSconClCliDaList = "S" Then
            If NTSCStr(e.Row!ls_codart).Trim = "" Or NTSCInt(e.Row!ls_listino1) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507973052685, "Negli sconti per articolo / listino è necessario avere: articolo diverso da '', primo listino diverso da 0")))
              e.ProposedValue = e.Row!ls_tiposc_2
              Return
            End If
          Else
            If NTSCStr(e.Row!ls_codart).Trim = "" Or NTSCInt(e.Row!ls_clscan) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507855547420, "Negli sconti per articolo / classe cliente è necessario avere: articolo diverso da '', classe cliente diversa da 0")))
              e.ProposedValue = e.Row!ls_tiposc_2
              Return
            End If
          End If
        Case "A"
          'Specifico Articolo cli/forn
          If NTSCInt(e.Row!ls_conto) = 0 Or NTSCStr(e.Row!ls_codart).Trim = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507869298036, "Negli sconti specifici cliente (o fornitore) / articolo è necessario avere: conto diverso da 0, articolo diverso da ''")))
            e.ProposedValue = e.Row!ls_tiposc_2
            Return
          End If
        Case "D"
          'Per classe art / classe cli/for
          If NTSCInt(e.Row!ls_clscan) = 0 Or NTSCInt(e.Row!ls_clscar) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507883673680, "Negli sconti per classe cliente (o fornitore) / classe articolo è necessario avere: classe articolo diversa da 0, classe cliente diversa da 0")))
            e.ProposedValue = e.Row!ls_tiposc_2
            Return
          End If
      End Select

      e.Row!ls_modificatosc_2 = "S"

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

  Public Overridable Sub BeforeColUpdate_ls_scont1_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont2_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont3_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont4_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont5_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont6_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scdaquant_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scaquant_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_dadatasc_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_adatasc_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_3 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_tiposc_3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      Select Case NTSCStr(e.ProposedValue)
        Case "F"
          'Generico cliente/fornitore
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507705071929, "Negli sconti generici cliente/fornitore è necessario avere il conto diverso da 0")))
            e.ProposedValue = e.Row!ls_tiposc_3
            Return
          End If
        Case "E"
          'Generico articolo
          If NTSCStr(e.Row!ls_codart).Trim = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507719291316, "Negli sconti generici articolo è necessario avere l'articolo diverso da ''")))
            e.ProposedValue = e.Row!ls_tiposc_3
            Return
          End If
        Case "B"
          'Per classe articolo
          If NTSCInt(e.Row!ls_conto) = 0 Or NTSCInt(e.Row!ls_clscar) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507727729194, "Negli sconti per cliente / classe articolo è necessario avere: conto diverso da 0, classe articolo diversa da 0")))
            e.ProposedValue = e.Row!ls_tiposc_3
            Return
          End If
        Case "C"
          'Per classe cliente/fornitore
          If oApp.oGvar.strSconClCliDaList = "S" Then
            If NTSCStr(e.Row!ls_codart).Trim = "" Or NTSCInt(e.Row!ls_listino1) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507973052686, "Negli sconti per articolo / listino è necessario avere: articolo diverso da '', primo listino diverso da 0")))
              e.ProposedValue = e.Row!ls_tiposc
              Return
            End If
          Else
            If NTSCStr(e.Row!ls_codart).Trim = "" Or NTSCInt(e.Row!ls_clscan) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507744136179, "Negli sconti per articolo / classe cliente (o listino) è necessario avere: articolo diverso da '', classe cliente diversa da 0")))
              e.ProposedValue = e.Row!ls_tiposc_3
              Return
            End If
          End If
        Case "A"
          'Specifico Articolo cli/forn
          If NTSCInt(e.Row!ls_conto) = 0 Or NTSCStr(e.Row!ls_codart).Trim = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507764293332, "Negli sconti specifici cliente (o fornitore) / articolo è necessario avere: conto diverso da 0, articolo diverso da ''")))
            e.ProposedValue = e.Row!ls_tiposc_3
            Return
          End If
        Case "D"
          'Per classe art / classe cli/for
          If NTSCInt(e.Row!ls_clscan) = 0 Or NTSCInt(e.Row!ls_clscar) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507782887915, "Negli sconti per classe cliente (o fornitore) / classe articolo è necessario avere: classe articolo diversa da 0, classe cliente diversa da 0")))
            e.ProposedValue = e.Row!ls_tiposc_3
            Return
          End If
      End Select

      e.Row!ls_modificatosc_3 = "S"

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


  Public Overridable Sub BeforeColUpdate_ls_scont1_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont2_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont3_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont4_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont5_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scont6_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scdaquant_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_scaquant_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_dadatasc_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_adatasc_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!ls_modificatosc_4 = "S"
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
  Public Overridable Sub BeforeColUpdate_ls_tiposc_4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      Select Case NTSCStr(e.ProposedValue)
        Case "F"
          'Generico cliente/fornitore
          If NTSCInt(e.Row!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507591160576, "Negli sconti generici cliente/fornitore è necessario avere il conto diverso da 0")))
            e.ProposedValue = e.Row!ls_tiposc_4
            Return
          End If
        Case "E"
          'Generico articolo
          If NTSCStr(e.Row!ls_codart).Trim = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507604129907, "Negli sconti generici articolo è necessario avere l'articolo diverso da ''")))
            e.ProposedValue = e.Row!ls_tiposc_4
            Return
          End If
        Case "B"
          'Per classe articolo
          If NTSCInt(e.Row!ls_conto) = 0 Or NTSCInt(e.Row!ls_clscar) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507614599126, "Negli sconti per cliente / classe articolo è necessario avere: conto diverso da 0, classe articolo diversa da 0")))
            e.ProposedValue = e.Row!ls_tiposc_4
            Return
          End If
        Case "C"
          'Per classe cliente/fornitore
          If oApp.oGvar.strSconClCliDaList = "S" Then
            If NTSCStr(e.Row!ls_codart).Trim = "" Or NTSCInt(e.Row!ls_listino1) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507973052687, "Negli sconti per articolo / listino è necessario avere: articolo diverso da '', primo listino diverso da 0")))
              e.ProposedValue = e.Row!ls_tiposc
              Return
            End If
          Else
            If NTSCStr(e.Row!ls_codart).Trim = "" Or NTSCInt(e.Row!ls_clscan) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507630381083, "Negli sconti per articolo / classe cliente (o listino) è necessario avere: articolo diverso da '', classe cliente diversa da 0")))
              e.ProposedValue = e.Row!ls_tiposc_4
              Return
            End If
          End If
        Case "A"
          'Specifico Articolo cli/forn
          If NTSCInt(e.Row!ls_conto) = 0 Or NTSCStr(e.Row!ls_codart).Trim = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507659913656, "Negli sconti specifici cliente (o fornitore) / articolo è necessario avere: conto diverso da 0, articolo diverso da ''")))
            e.ProposedValue = e.Row!ls_tiposc_4
            Return
          End If
        Case "D"
          'Per classe art / classe cli/for
          If NTSCInt(e.Row!ls_clscan) = 0 Or NTSCInt(e.Row!ls_clscar) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219507668820305, "Negli sconti per classe cliente (o fornitore) / classe articolo è necessario avere: classe articolo diversa da 0, classe cliente diversa da 0")))
            e.ProposedValue = e.Row!ls_tiposc_4
            Return
          End If
      End Select

      e.Row!ls_modificatosc_4 = "S"

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


  Public Overridable Function Salva() As Boolean
    Dim dtrRow() As DataRow
    Try
      dtrRow = dsSharedLise.Tables("LISTSES").Select("", "", DataViewRowState.ModifiedCurrent)

      For z As Integer = 0 To dtrRow.Length - 1
        dtrRow(z)!ls_ultagg = Now
      Next

      oCldStli.SaveDataLise(dsSharedLise)

      dsSharedLise.AcceptChanges()
      bHasChanges = False
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

  Public Overridable Function CheckSync() As Boolean
    Dim dtrRow() As DataRow
    Try
      'Controllo se i listini sono sincronizzati
      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_progr1 <> 0")

      CheckSyncListino(dtrRow, 1)

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_progr2 <> 0")

      CheckSyncListino(dtrRow, 2)

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_progr3 <> 0")

      CheckSyncListino(dtrRow, 3)

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_progr4 <> 0")

      CheckSyncListino(dtrRow, 4)

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_progr5 <> 0")

      CheckSyncListino(dtrRow, 5)

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_nuovosc <> 'S'")

      CheckSyncSconti(dtrRow, "")

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_nuovosc_2 <> 'S'")

      CheckSyncSconti(dtrRow, "_2")

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_nuovosc_3 <> 'S'")

      CheckSyncSconti(dtrRow, "_3")

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_nuovosc_4 <> 'S'")

      CheckSyncSconti(dtrRow, "_4")

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
  Public Overridable Function CheckSyncListino(ByRef dtrRow() As DataRow, ByVal lPos As Integer) As Boolean
    Dim dttTmp As New DataTable
    Try
      For z As Integer = 0 To dtrRow.Length - 1
        oCldStli.CheckSyncListino(NTSCStr(dtrRow(z)!codditt), NTSCInt(dtrRow(z)("ls_progr" & lPos)), NTSCDate(dtrRow(z)!ls_dtorsync), dttTmp)

        If dttTmp.Rows.Count <> 0 Then
          dtrRow(z)("xx_sync" & lPos) = "S"
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
  Public Overridable Function CheckSyncSconti(ByRef dtrRow() As DataRow, ByVal strTag As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      For z As Integer = 0 To dtrRow.Length - 1
        oCldStli.CheckSyncSconti(dtrRow(z), strTag, dttTmp)

        If dttTmp.Rows.Count <> 0 Then
          dtrRow(z)("xx_syncsc" & strTag) = "S"
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

  Public Overridable Function Aggiorna() As Boolean
    Dim dtrRow() As DataRow
    Dim EventArgs As NTSEventArgs
    Try
      CheckSync()

      dtrRow = dsSharedLise.Tables("LISTSES").Select("(ls_modificato1 = 'S' AND xx_sync1 = 'S') OR (ls_modificato2 = 'S' AND xx_sync2 = 'S') OR " & _
                                                     "(ls_modificato3 = 'S' AND xx_sync3 = 'S') OR (ls_modificato4 = 'S' AND xx_sync4 = 'S') OR " & _
                                                     "(ls_modificato5 = 'S' AND xx_sync5 = 'S')")
      If dtrRow.Length > 0 Then
        EventArgs = New NTSEventArgs(ThMsg.MSG_NOYES, oApp.Tr(Me, 129217592270665265, "Dei listini\sconti che si stanno per aggiornare sono stati modificati con altri programmi." & vbCrLf & _
                                                                         "Procedere ugualmente con l'elaborazione?"))
        ThrowRemoteEvent(EventArgs)
        If EventArgs.RetValue.ToLower = "no" Then Return False
      End If

      'Controllo se i listini sono sincronizzati
      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_modificato1 = 'S'")

      If Not AggiornaListino(dtrRow, 1) Then Return False

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_modificato2 = 'S'")

      If Not AggiornaListino(dtrRow, 2) Then Return False

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_modificato3 = 'S'")

      If Not AggiornaListino(dtrRow, 3) Then Return False

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_modificato4 = 'S'")

      If Not AggiornaListino(dtrRow, 4) Then Return False

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_modificato5 = 'S'")

      If Not AggiornaListino(dtrRow, 5) Then Return False

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_modificatosc = 'S'")

      If Not AggiornaSconti(dtrRow, "") Then Return False

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_modificatosc_2 = 'S'")

      If Not AggiornaSconti(dtrRow, "_2") Then Return False

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_modificatosc_3 = 'S'")

      If Not AggiornaSconti(dtrRow, "_3") Then Return False

      dtrRow = dsSharedLise.Tables("LISTSES").Select("ls_modificatosc_4 = 'S'")

      If Not AggiornaSconti(dtrRow, "_4") Then Return False

      CancellaListSes()

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
  Public Overridable Function AggiornaListino(ByRef dtrRow() As DataRow, ByVal lPos As Integer) As Boolean
    Dim dttList As New DataTable
    Dim lConto As Integer = 0
    Dim lCoddest As Integer = 0
    Dim lListino As Integer = 0
    Try
      For z As Integer = 0 To dtrRow.Length - 1
        If Not TestPreScriviPrezzo(dtrRow(z), lPos) Then Return False

        lListino = NTSCInt(dtrRow(z)("ls_listino" & lPos))
        Select Case NTSCStr(dtrRow(z)("ls_tipo" & lPos))
          Case "A", "C", "F"
            lConto = NTSCInt(dtrRow(z)!ls_conto)
            lCoddest = NTSCInt(dtrRow(z)!ls_coddest)
            lListino = 0
        End Select

        CType(oCleComm, CLELBMENU).ScriviPrezzo(strDittaCorrente, NTSCStr(dtrRow(z)!ls_codart), NTSCInt(dtrRow(z)!ls_fase), _
                                                NTSCInt(dtrRow(z)("ls_codlavo" & lPos)), NTSCStr(dtrRow(z)("ls_tipo" & lPos)), _
                                                lListino, lConto, _
                                                NTSCInt(dtrRow(z)("ls_codvalu" & lPos)), NTSCStr(dtrRow(z)("ls_unmis" & lPos)), _
                                                NTSCInt(dtrRow(z)("ls_codpromo" & lPos)), NTSCDec(dtrRow(z)("ls_daquant" & lPos)), _
                                                NTSCDate(dtrRow(z)("ls_dadata" & lPos)), NTSCDec(dtrRow(z)("ls_prz" & lPos)), _
                                                NTSCDate(dtrRow(z)("ls_adata" & lPos)), NTSCDec(dtrRow(z)!ls_perqta), _
                                                NTSCDec(dtrRow(z)("ls_aquant" & lPos)), "", NTSCStr(dtrRow(z)("ls_prznet" & lPos)), _
                                                True, "", lCoddest)
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
  Public Overridable Function AggiornaSconti(ByRef dtrRow() As DataRow, ByVal strTag As String) As Boolean
    Dim lCoddest As Integer = 0
    Dim nClscan As Integer = 0
    Try
      For z As Integer = 0 To dtrRow.Length - 1
        lCoddest = NTSCInt(dtrRow(z)!ls_coddest)
        If Not TestPreScriviSconto(dtrRow(z), strTag) Then Return False

        'in base al tipo devo gestire quale parametri passare alla Scrivi sconto
        Select Case NTSCStr(dtrRow(z)("ls_tiposc" & strTag))
          Case "F"
            'Generico cliente/fornitore
            CType(oCleComm, CLELBMENU).ScriviSconto(strDittaCorrente, "", 0, _
                                                  NTSCInt(dtrRow(z)!ls_conto), 0, _
                                                  0, NTSCInt(dtrRow(z)("ls_codtpro" & strTag)), _
                                                  NTSCDec(dtrRow(z)("ls_scdaquant" & strTag)), NTSCDec(dtrRow(z)("ls_scaquant" & strTag)), _
                                                  NTSCDate(dtrRow(z)("ls_dadatasc" & strTag)), NTSCDate(dtrRow(z)("ls_adatasc" & strTag)), _
                                                  "", NTSCDec(dtrRow(z)("ls_scont1" & strTag)), _
                                                  NTSCDec(dtrRow(z)("ls_scont2" & strTag)), NTSCDec(dtrRow(z)("ls_scont3" & strTag)), _
                                                  NTSCDec(dtrRow(z)("ls_scont4" & strTag)), NTSCDec(dtrRow(z)("ls_scont5" & strTag)), _
                                                  NTSCDec(dtrRow(z)("ls_scont6" & strTag)), True, lCoddest)
          Case "E"
            'Generico articolo
            CType(oCleComm, CLELBMENU).ScriviSconto(strDittaCorrente, NTSCStr(dtrRow(z)!ls_codart), NTSCInt(dtrRow(z)!ls_fase), _
                                                 0, 0, 0, NTSCInt(dtrRow(z)("ls_codtpro" & strTag)), _
                                                 NTSCDec(dtrRow(z)("ls_scdaquant" & strTag)), NTSCDec(dtrRow(z)("ls_scaquant" & strTag)), _
                                                 NTSCDate(dtrRow(z)("ls_dadatasc" & strTag)), NTSCDate(dtrRow(z)("ls_adatasc" & strTag)), _
                                                 "", NTSCDec(dtrRow(z)("ls_scont1" & strTag)), _
                                                 NTSCDec(dtrRow(z)("ls_scont2" & strTag)), NTSCDec(dtrRow(z)("ls_scont3" & strTag)), _
                                                 NTSCDec(dtrRow(z)("ls_scont4" & strTag)), NTSCDec(dtrRow(z)("ls_scont5" & strTag)), _
                                                 NTSCDec(dtrRow(z)("ls_scont6" & strTag)), True, 0)
          Case "B"
            'Per articolo / classe cliente/fornitore (o listino)
            nClscan = NTSCInt(dtrRow(z)!ls_clscan)
            If oApp.oGvar.strSconClCliDaList = "S" Then nClscan = NTSCInt(dtrRow(z)!ls_listino1)
            CType(oCleComm, CLELBMENU).ScriviSconto(strDittaCorrente, NTSCStr(dtrRow(z)!ls_codart), 0, 0, nClscan, _
                                                0, NTSCInt(dtrRow(z)("ls_codtpro" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scdaquant" & strTag)), NTSCDec(dtrRow(z)("ls_scaquant" & strTag)), _
                                                NTSCDate(dtrRow(z)("ls_dadatasc" & strTag)), NTSCDate(dtrRow(z)("ls_adatasc" & strTag)), _
                                                "", NTSCDec(dtrRow(z)("ls_scont1" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont2" & strTag)), NTSCDec(dtrRow(z)("ls_scont3" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont4" & strTag)), NTSCDec(dtrRow(z)("ls_scont5" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont6" & strTag)), True, 0)
          Case "C"
            'Per classe articolo
            CType(oCleComm, CLELBMENU).ScriviSconto(strDittaCorrente, "", 0, 0, 0, _
                                                NTSCInt(dtrRow(z)!ls_clscar), NTSCInt(dtrRow(z)("ls_codtpro" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scdaquant" & strTag)), NTSCDec(dtrRow(z)("ls_scaquant" & strTag)), _
                                                NTSCDate(dtrRow(z)("ls_dadatasc" & strTag)), NTSCDate(dtrRow(z)("ls_adatasc" & strTag)), _
                                                "", NTSCDec(dtrRow(z)("ls_scont1" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont2" & strTag)), NTSCDec(dtrRow(z)("ls_scont3" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont4" & strTag)), NTSCDec(dtrRow(z)("ls_scont5" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont6" & strTag)), True, 0)
          Case "A"
            'Specifico Articolo cli/forn
            CType(oCleComm, CLELBMENU).ScriviSconto(strDittaCorrente, NTSCStr(dtrRow(z)!ls_codart), NTSCInt(dtrRow(z)!ls_fase), _
                                                NTSCInt(dtrRow(z)!ls_conto), 0, 0, NTSCInt(dtrRow(z)("ls_codtpro" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scdaquant" & strTag)), NTSCDec(dtrRow(z)("ls_scaquant" & strTag)), _
                                                NTSCDate(dtrRow(z)("ls_dadatasc" & strTag)), NTSCDate(dtrRow(z)("ls_adatasc" & strTag)), _
                                                "", NTSCDec(dtrRow(z)("ls_scont1" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont2" & strTag)), NTSCDec(dtrRow(z)("ls_scont3" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont4" & strTag)), NTSCDec(dtrRow(z)("ls_scont5" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont6" & strTag)), True, lCoddest)
          Case "D"
            'Per classe art / classe cli/for
            CType(oCleComm, CLELBMENU).ScriviSconto(strDittaCorrente, "", 0, 0, NTSCInt(dtrRow(z)!ls_clscan), _
                                                NTSCInt(dtrRow(z)!ls_clscar), NTSCInt(dtrRow(z)("ls_codtpro" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scdaquant" & strTag)), NTSCDec(dtrRow(z)("ls_scaquant" & strTag)), _
                                                NTSCDate(dtrRow(z)("ls_dadatasc" & strTag)), NTSCDate(dtrRow(z)("ls_adatasc" & strTag)), _
                                                "", NTSCDec(dtrRow(z)("ls_scont1" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont2" & strTag)), NTSCDec(dtrRow(z)("ls_scont3" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont4" & strTag)), NTSCDec(dtrRow(z)("ls_scont5" & strTag)), _
                                                NTSCDec(dtrRow(z)("ls_scont6" & strTag)), True, 0)
        End Select
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

  Public Overridable Function TestPreScriviPrezzo(ByVal dtrRow As DataRow, ByVal lPos As Integer) As Boolean
    Try
      'Se è di tipo specifico fornitore devo esserci il conto
      If (NTSCStr(dtrRow("ls_tipo" & lPos)) = "C" Or NTSCStr(dtrRow("ls_tipo" & lPos)) = "F") And NTSCInt(dtrRow!ls_conto) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129218361366250000, "Si può scegliere un listino specifico per cliente\fornitore solo se è specificato il conto.")))
        Return False
      End If

      'La data scadenza deve essere maggiore o uguale della data di partenza (se è presente la promozione)
      If NTSCInt(dtrRow("ls_codpromo" & lPos)) <> 0 And (NTSCDate(dtrRow("ls_dadata" & lPos)) > NTSCDate(dtrRow("ls_adata" & lPos))) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129218363598750000, "Se è specificato il codice promozione è necessario che 'Da Data' sia minore di 'a data'")))
        Return False
      End If

      If (NTSCInt(dtrRow("ls_listino" & lPos)) = 0) And (NTSCInt(dtrRow!ls_conto) <> 0) And _
         (NTSCStr(dtrRow("ls_tipo" & lPos)) = "C" Or NTSCStr(dtrRow("ls_tipo" & lPos)) = "F") Then
        '...
      Else
        If NTSCInt(dtrRow("ls_listino" & lPos)) < 1 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129218366347187500, "Non si possono modificare i listini -3, -2, -1 e 0")))
          Return False
        End If
      End If

      If NTSCStr(dtrRow("ls_unmis" & lPos)).Trim = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129218367261093750, "Specificare l'unità di misura")))
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
  Public Overridable Function TestPreScriviSconto(ByVal dtrRow As DataRow, ByVal strTag As String) As Boolean
    Try
      Select Case NTSCStr(dtrRow("ls_tiposc" & strTag))
        Case "F"
          'Generico cliente/fornitore
          If NTSCInt(dtrRow!ls_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219464148042234, "Negli sconti generici cliente/fornitore è necessario avere il conto diverso da 0")))
            Return False
          End If
        Case "E"
          'Generico articolo
          If NTSCStr(dtrRow!ls_codart).Trim = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219464159763384, "Negli sconti generici articolo è necessario avere l'articolo diverso da ''")))
            Return False
          End If
        Case "B"
          'Per articolo classe cli/forn
          If oApp.oGvar.strSconClCliDaList = "S" Then
            If NTSCStr(dtrRow!ls_codart).Trim = "" Or NTSCInt(dtrRow!ls_listino1) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219464188519273, "Negli sconti per articolo / listino è necessario avere: articolo diverso da '', classe cliente diversa da 0")))
              Return False
            End If
          Else
            If NTSCStr(dtrRow!ls_codart).Trim = "" Or NTSCInt(dtrRow!ls_clscan) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219464188519272, "Negli sconti per articolo / classe cliente è necessario avere: articolo diverso da '', classe cliente diversa da 0")))
              Return False
            End If
          End If
        Case "C"
          'Per classe articolo
          If NTSCInt(dtrRow!ls_conto) = 0 Or NTSCInt(dtrRow!ls_clscar) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219464169609150, "Negli sconti per cliente / classe articolo è necessario avere: conto diverso da 0, classe articolo diversa da 0")))
            Return False
          End If
        Case "A"
          'Specifico Articolo cli/forn
          If NTSCInt(dtrRow!ls_conto) = 0 Or NTSCStr(dtrRow!ls_codart).Trim = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219464197739910, "Negli sconti specifici cliente (o fornitore) / articolo è necessario avere: conto diverso da 0, articolo diverso da ''")))
            Return False
          End If
        Case "D"
          'Per classe art / classe cli/for
          If NTSCInt(dtrRow!ls_clscan) = 0 Or NTSCInt(dtrRow!ls_clscar) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129219464211336444, "Negli sconti per classe cliente (o fornitore) / classe articolo è necessario avere: classe articolo diversa da 0, classe cliente diversa da 0")))
            Return False
          End If
      End Select

      'La data scadenza deve essere maggiore o uguale della data di partenza (se è presente la promozione)
      If NTSCInt(dtrRow("ls_codtpro" & strTag)) <> 0 And (NTSCDate(dtrRow("ls_dadatasc" & strTag)) < NTSCDate(dtrRow("ls_adatasc" & strTag))) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129218564114843750, "Se è specificato il codice promozione è necessario che 'Da Data' sia minore di 'a data'")))
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

  Public Overridable Function CaricaUnMis() As DataTable
    '----------------------------
    'ottengo l'elenco delle unità di misura utilizzate in artico
    Dim dttTmp As New DataTable
    Try
      oCldStli.CaricaUmMis(strDittaCorrente, dttTmp)

      Return dttTmp
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
  Public Overridable Function GetArticoUnMis(ByVal strCodart As String) As String
    '---------------------------------------
    'ritorna le unità di misura dell'articolo passato in input
    Try
      Return CType(oCleComm, CLELBMENU).TrovaArticoUnMis(strDittaCorrente, strCodart)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
        Return ""
      End If
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Filtri"
  Public Overridable Function GetTableStructMovIfil(ByRef dttTable As DataTable) As Boolean
    Try
      Return oCldStli.GetTableStructure("MOVIFIL", False, dttTable)
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
      If Not oCldStli.CaricaFiltri(strDittaCorrente, "BNMGSTLI", dttOut) Then Return False

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
      If Not oCldStli.LeggiFiltro(lCod, strChild, strForm, dttOut) Then Return False

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
