Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGAGAD
  Inherits CLE__BASE

  Public oCldAgad As CLDMGAGAD

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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGAGAD"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldAgad = CType(MyBase.ocldBase, CLDMGAGAD)
    oCldAgad.Init(oApp)
    Return True
  End Function

#Region "Validazione campi"
  Public Overridable Function edEscomp_Validated(ByVal nEscomp As Integer, ByRef strDesEscomp As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not oCldAgad.ValCodiceDb(nEscomp.ToString, strDittaCorrente, "TABESCO", "N", strDesEscomp, dttTmp) Then
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
      If oCldAgad.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttAnaz) Then
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

  Public Overridable Function TestPreElabora(ByVal bckStoricizza As Boolean, ByVal strDtulap As String, ByVal strFinoAl As String, ByVal strEscomp As String) As Boolean
    Dim strErr As String = ""
    Dim evt As NTSEventArgs = Nothing
    Dim dttCausali As DataTable = Nothing
    Try

      If CInt(strEscomp) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128499856009241207, "Indicare esercizio competenza !")))
        Return False
      End If
      If Not edEscomp_Validated(NTSCInt(strEscomp), "") Then
        Return False
      End If
      If strFinoAl = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128499856028723707, "Data finale obbligatoria !")))
        Return False
      End If
      If Not (NTSCDate(strFinoAl) > NTSCDate(strDtulap)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128499856071117627, "Data finale sempre maggiore della data precedente aggiornamento!")))
        Return False
      End If
      If (NTSCDate(strFinoAl) > NTSCDate(strDtfieser)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128499856090444267, "La data finale deve sempre essere minore o uguale della data di fine esercizio!")))
        Return False
      End If
      oCldAgad.ControllaCausaliMagazzino(dttCausali)
      If dttCausali.Rows.Count > 0 Then
        Dim strMessaggio As String = vbCrLf
        For Each dtrCausali As DataRow In dttCausali.Rows
          strMessaggio &= NTSCStr(dtrCausali!tb_codcaum) & " - " & NTSCStr(dtrCausali!tb_descaum) & vbCrLf
        Next
        strMessaggio = oApp.Tr(Me, 130431456320490902, "Attenzione! Sono state trovate delle causali di magazzino dove il flag di 'Ultimo Costo' e 'Data Ultimo Carico' non sono allineati." & vbCrLf & _
                                                       "L'elaborazione potrebbe portare a una errata valorizzazione durante il calcolo dell'ultimo costo se sono state utilizzate su magazzini di 'Merce Propria' o 'Merce Propria Presso Terzi'." & vbCrLf & _
                                                       "Le causali sono:|" & strMessaggio & "|" & vbCrLf & _
                                                       "Continuare con l'elaborazione?")
        evt = New NTSEventArgs(ThMsg.MSG_NOYES, strMessaggio)
        ThrowRemoteEvent(evt)
        If evt.RetValue <> ThMsg.RETVALUE_YES Then Return False
      End If
      If bckStoricizza = False Then
        evt = New NTSEventArgs(ThMsg.MSG_YESNO, oApp.Tr(Me, 128499856127071367, "Attenzione! togliendo la spunta sul campo 'storicizza'  non sarà possibile utilizzare il programma 'Reimposta progressivi per rielaborazioni'in caso di errore. Si consiglia pertanto sempre di storicizzare i progressivi ! Prosegui ugualmente senza storicizzazione ? "))
        ThrowRemoteEvent(evt)
       If evt.RetValue <> ThMsg.RETVALUE_YES Then Return False
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

  Public Overridable Function Elabora(ByVal bckStoricizza As Boolean, ByVal strDtulap As String, ByVal strFinoAl As String, ByVal strUltElab As String, ByVal strEscomp As String, ByVal strDesescomp As String) As Boolean
    Dim strErr As String = ""
    Dim strDesogglog As String = ""
    Dim bRet As Boolean
    Dim nNdec As Integer
    Dim strFineesercp As String
    Dim strIniEser As String

    Try
      '-----------------------
      'test di rito
      If Not TestPreElabora(bckStoricizza, strDtulap, strFinoAl, strEscomp) Then Return False

      bElabInCorso = True

      nNdec = oCldAgad.TrovaNdecSuPrzUn(0)
      If CBool(oApp.ActKey.ModuliExtAcquistati And bsModExtTCO) Then
        bModTCO = True
      Else
        bModTCO = False
      End If

      strIniEser = NTSCStr(DateAdd("d", -1, NTSCDate(strDtineser)))

      '------------------------
      'scrivo la registrazione
      bRet = oCldAgad.Elabora(strDittaCorrente, bckStoricizza, bModTCO, strDtulap, strIniEser, strFinoAl, strUltElab, nNdec, strFineesercp)

      '------------------------
      'scrivo il log
      If bRet = True Then
        strDesogglog = "Aggiornamento progressivi definitivi di magazzino" & vbCrLf & vbCrLf & _
          " - Esercizio di competenza...............: '" & strEscomp & "' " & strDesescomp & vbCrLf & _
          " - Elaborazione fino al..................: '" & strFinoAl & "'" & vbCrLf & _
          " - Storicizzazione progressivi precedenti: " & NTSCStr(IIf(bckStoricizza = True, "'Sì'", "'No'")) & vbCrLf & _
          " - Data precedente di aggiornamento......: '" & strDtulap & "'" & vbCrLf
        oCldAgad.ScriviActLog(strDittaCorrente, "BSMGAGAD", "BSMGAGAD", "", "", "M", "E", strDesogglog, False)
      End If

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

  Public Overridable Function CheckCoerenzaTaglieQtaTCO(ByRef strErr As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldAgad.CheckCoerenzaTaglieQtaTCOPeriodo(strDittaCorrente, NTSCDate(strDtineser), NTSCDate(strDtfieser), strErr)
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