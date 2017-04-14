Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp

Public Class CLEMGCMMA
  Inherits CLE__BASE

  Public oCldCmma As CLDMGCMMA

  Public bFerma As Boolean = True
  Public bCancellaPicking As Boolean = False

#Region "Moduli"
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
#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, _
                                  ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                                  ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                                  ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGCMMA"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldCmma = CType(MyBase.ocldBase, CLDMGCMMA)
    oCldCmma.Init(oApp)

    Return True
  End Function

  Public Overridable Function ComponiTestoDaCk(ByVal bFie As Boolean, ByVal bDe As Boolean, ByVal bCe As Boolean, _
                                             ByVal bFde As Boolean, ByVal bNdae As Boolean, ByVal bNar As Boolean, _
                                             ByVal bFdr As Boolean, ByVal bFir As Boolean, ByVal bDr As Boolean, _
                                             ByVal bNae As Boolean, ByVal bCp As Boolean, ByVal bSp As Boolean, _
                                             ByVal bBmi As Boolean, ByVal bRf As Boolean, ByVal bRrf As Boolean, _
                                             ByVal bFrf As Boolean, ByVal bFrfd As Boolean, ByRef strWhere As String, _
                                             ByRef bAggiornaAnaz As Boolean) As Boolean
    Try
      Return ComponiTestoDaCk(bFie, bDe, bCe, bFde, bNdae, bNar, bFdr, bFir, bDr, bNae, bCp, bSp, _
                              bBmi, bRf, bRrf, bFrf, bFrfd, strWhere, bAggiornaAnaz, False, False)
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
  Public Overridable Function ComponiTestoDaCk(ByVal bFie As Boolean, ByVal bDe As Boolean, ByVal bCe As Boolean, _
                                               ByVal bFde As Boolean, ByVal bNdae As Boolean, ByVal bNar As Boolean, _
                                               ByVal bFdr As Boolean, ByVal bFir As Boolean, ByVal bDr As Boolean, _
                                               ByVal bNae As Boolean, ByVal bCp As Boolean, ByVal bSp As Boolean, _
                                               ByVal bBmi As Boolean, ByVal bRf As Boolean, ByVal bRrf As Boolean, _
                                               ByVal bFrf As Boolean, ByVal bFrfd As Boolean, ByRef strWhere As String, _
                                               ByRef bAggiornaAnaz As Boolean, ByVal bNade As Boolean, ByVal bNadr As Boolean) As Boolean
    Dim i As Integer = 0
    ' Compone la stringa where in base alle scelta degli cmb
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bFie, bDe, bCe, bFde, bNdae, bNar, bFdr, bFir, bDr, bNae, bCp, bSp, _
                              bBmi, bRf, bRrf, bFrf, bFrfd, strWhere, bAggiornaAnaz, bNade, bNadr})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strWhere = NTSCStr(oIn(17))
        bAggiornaAnaz = CBool(oIn(18))
        Return CBool(oOut)
      End If
      '----------------

      strWhere = "("

      ' Fatture imm. emesse
      If bFie Then
        strWhere &= " tm_tipork = " & CStrSQL("A") & " OR"
        i += 1
      End If
      ' DDT Emessi
      If bDe Then
        strWhere &= " tm_tipork = " & CStrSQL("B") & " OR"
        i += 1
      End If
      ' Corrispettivi emessi
      If bCe Then
        strWhere &= " tm_tipork = " & CStrSQL("C") & " OR"
        i += 1
      End If
      ' Fatture differite emesse
      If bFde Then
        strWhere &= " tm_tipork = " & CStrSQL("D") & " OR"
        i += 1
      End If
      ' Note di addebito emesse
      If bNdae Then
        strWhere &= " tm_tipork = " & CStrSQL("E") & " OR"
        i += 1
      End If
      ' note accredito ricevute
      If bNar Then
        strWhere &= " tm_tipork = " & CStrSQL("J") & " OR"
        i += 1
      End If
      ' fatture differite ricevute
      If bFdr Then
        strWhere &= " tm_tipork = " & CStrSQL("K") & " OR"
        i += 1
      End If
      ' fattire immediate ricetue
      If bFir Then
        strWhere &= " tm_tipork = " & CStrSQL("L") & " OR"
        i += 1
      End If
      ' DDT ricevuti
      If bDr Then
        strWhere &= " tm_tipork = " & CStrSQL("M") & " OR"
        i += 1
      End If
      ' Note accredito emesse
      If bNae Then
        strWhere &= " tm_tipork = " & CStrSQL("N") & " OR"
        i += 1
      End If
      ' Carico produzione
      If bCp Then
        strWhere &= " tm_tipork = " & CStrSQL("T") & " OR"
        i += 1
      End If
      ' scarico produzione
      If bSp Then
        strWhere &= " tm_tipork = " & CStrSQL("U") & " OR"
        i += 1
      End If
      ' Bolle mov. interna
      If bBmi Then
        strWhere &= " tm_tipork = " & CStrSQL("Z") & " OR"
        i += 1
      End If
      ' Ricevute fiscali
      If bRf Then
        strWhere &= " tm_tipork = " & CStrSQL("F") & " OR"
        i += 1
      End If
      ' riem.ricev.fiscali
      If bRrf Then
        strWhere &= " tm_tipork = " & CStrSQL("I") & " OR"
        i += 1
      End If
      ' fatt/ricev. fiscali
      If bFrf Then
        strWhere &= " tm_tipork = " & CStrSQL("S") & " OR"
        i += 1
      End If
      ' fatt/ricev.fisc.differ.
      If bFrfd Then
        strWhere &= " tm_tipork = " & CStrSQL("P") & " OR"
        i += 1
      End If
      'note accred differite emesse
      If bNade Then
        strWhere &= " tm_tipork = " & CStrSQL("£") & " OR"
        i += 1
      End If
      'note accred differite ricevute
      If bNadr Then
        strWhere &= " tm_tipork = " & CStrSQL("(") & " OR"
        i += 1
      End If

      If strWhere = "(" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128625930710312500, "Selezionare almeno un tipo di documento da cancellare.")))
        Return False
      Else
        strWhere = Left(strWhere, strWhere.Length - 3) & ")"
      End If

      If i = 17 Then bAggiornaAnaz = True

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

  Public Overridable Function ControllaMaggioreNum(ByVal lMin As Integer, ByVal lMax As Integer) As Boolean
    Try
      If lMin > lMax Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128624199165937500, "'Da numero ordine' deve essere inferiore a 'a numero ordine'")))
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

  Public Overridable Sub ElaboraSerie(ByVal bSerie As Boolean, ByRef strSerie As String)
    Try
      If bSerie And strSerie = "" Then strSerie = " "
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

  Public Overridable Sub LeggiDate(ByRef strUltagg As String, ByRef strUltcan As String)
    Dim dsDate As DataSet = Nothing
    Try
      oCldCmma.LeggiDate(strDittaCorrente, dsDate)

      strUltagg = NTSCDate(dsDate.Tables("TABANAZ").Rows(0)!tb_dtulap).ToShortDateString
      strUltcan = NTSCDate(dsDate.Tables("TABANAZ").Rows(0)!tb_dtulst).ToShortDateString
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

  Public Overridable Function EseguiNote(ByVal strSerie As String, ByVal strData As String, ByVal lAnno As Integer, _
                                         ByVal lDanumord As Integer, ByVal lAnumord As Integer) As Boolean
    Dim dsTest As DataSet = Nothing
    Dim i As Integer = 0
    Try
      ThrowRemoteEvent(New NTSEventArgs("LABEL", oApp.Tr(Me, 128624270248750000, "Selezione ordini in corso ...")))

      oCldCmma.SelezionaDati(strDittaCorrente, strData, strSerie, lDanumord, lAnumord, lAnno, dsTest)

      If dsTest.Tables("TESTPRB").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128626521807343750, "Non esistono note di prelievo da cancellare!")))
        Return False
      End If

      For i = 0 To dsTest.Tables("TESTPRB").Rows.Count - 1
        With dsTest.Tables("TESTPRB").Rows(i)
          If NTSCInt(i Mod 50) = 0 Or i = dsTest.Tables("TESTPRB").Rows.Count - 1 Then
            ThrowRemoteEvent(New NTSEventArgs("LABEL", oApp.Tr(Me, 129055340692290175, "|" & i + 1 & " / " & dsTest.Tables("TESTPRB").Rows.Count & _
                                              "| Eliminazione Nota di Prelievo n° |" & NTSCInt(!tm_numdoc) & "| del |" & NTSCDate(!tm_datdoc) & "| ...")))
          End If

          oCldCmma.EseguiNote(strDittaCorrente, i, dsTest, bCancellaPicking)
        End With
      Next

      Return True
    Catch ex As Exception
      ThrowRemoteEvent(New NTSEventArgs("LABEL", oApp.Tr(Me, 129055340738541655, "|" & i + 1 & " / " & dsTest.Tables("TESTPRB").Rows.Count & _
                                                 "| Eliminazione Nota di Prelievo n° |" & NTSCInt(dsTest.Tables("TESTPRB").Rows(i)!tm_numdoc) & _
                                                 "| del |" & NTSCDate(dsTest.Tables("TESTPRB").Rows(i)!tm_datdoc) & "| ...")))
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

  Public Overridable Function EseguiDoc(ByVal strWhere As String, ByVal strData As String, ByVal bAggiornaAnaz As Boolean) As Boolean
    Dim dsTest As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing
    Dim bContinua As Boolean = True
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim bModuloTCO As Boolean
    Try
      oCldCmma.SelezionaTestmag(strDittaCorrente, strWhere, strData, dsTest)

      If dsTest.Tables("TESTMAG").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128626564889687500, "Non esistono movimenti da cancellare con queste caratteristiche!")))
        Return False
      End If

      If CBool((ModuliExtDittaDitt(strDittaCorrente) And bsModExtTCO)) Then
        bModuloTCO = True
      Else
        bModuloTCO = False
      End If

      For i = 0 To dsTest.Tables("TESTMAG").Rows.Count - 1
        bContinua = True
        With dsTest.Tables("TESTMAG").Rows(i)
          If NTSCInt(i Mod 50) = 0 Or i = dsTest.Tables("TESTMAG").Rows.Count - 1 Then
            ThrowRemoteEvent(New NTSEventArgs("LABEL", oApp.Tr(Me, 128626568779843750, "|" & i + 1 & " / " & dsTest.Tables("TESTMAG").Rows.Count & _
                                              "| Eliminazione del documento n° |" & NTSCInt(!tm_numdoc) & "| del |" & NTSCDate(!tm_datdoc) & "| ...")))
          End If

          oCldCmma.SelezionaTestmagJArtMovm(strDittaCorrente, i, dsTest, dsTmp)

          For j = 0 To dsTmp.Tables("TESTMAG").Rows.Count - 1
            'c/to lavoro
            If NTSCInt(dsTmp.Tables("TESTMAG").Rows(j)!mm_clnum) <> 0 Then
              bContinua = False
              Exit For
            End If
            'c/to visione
            If NTSCInt(dsTmp.Tables("TESTMAG").Rows(j)!mm_cvnum) <> 0 Then
              bContinua = False
              Exit For
            End If
            'matricole
            If NTSCStr(dsTmp.Tables("TESTMAG").Rows(j)!ar_gestmatr) = "S" Then
              bContinua = False
              Exit For
            End If
          Next

          'If NTSCStr(!tm_tipork) = "U" Then
          '  oCldCmma.SelezionaTestmag2(strDittaCorrente, i, dsTest, dsTmp)

          '  If dsTmp.Tables("TESTMAG").Rows.Count <> 0 Then
          '    bContinua = False
          '  End If
          'End If

          If bContinua Then oCldCmma.EseguiDoc(strDittaCorrente, i, dsTest, bModuloTCO, CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupCAE))
        End With
      Next

      If bAggiornaAnaz Then
        oCldCmma.AggiornaAnaz(strDittaCorrente, strData)
      End If

      Return True
    Catch ex As Exception
      ' Se si verifica un errore visualizzo nella label con quale documento è avvenuto
      If Not dsTest Is Nothing Then
        ThrowRemoteEvent(New NTSEventArgs("LABEL", oApp.Tr(Me, 128626568802187500, "|" & i + 1 & " / " & dsTest.Tables("TESTMAG").Rows.Count & _
                                                "| Eliminazione del documento n° |" & NTSCInt(dsTest.Tables("TESTMAG").Rows(i)!tm_numdoc) & _
                                                "| del |" & NTSCDate(dsTest.Tables("TESTMAG").Rows(i)!tm_datdoc) & "| ...")))
      End If
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
End Class