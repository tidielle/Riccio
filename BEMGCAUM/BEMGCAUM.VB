Imports System.Data
Imports System.Text
Imports NTSInformatica.CLN__STD

Public Class CLEMGCAUM
  Inherits CLE__BASE

  Public oCldCaum As CLDMGCAUM

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModMG + CLN__STD.bsModVE
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE
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
  
#Region "Inizializzazione"
  Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, _
    ByRef oCleLbmenu As Object, ByVal strTabella As String, ByVal bRemoting As Boolean, _
    ByVal strRemoteServer As String, ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGCAUM"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldCaum = CType(MyBase.ocldBase, CLDMGCAUM)
    oCldCaum.Init(oApp)
    Return True
  End Function
#End Region

#Region "TextBox"
  Public Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      bHasChanges = True
      '--------------------------------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      '--------------------------------------------------------------------------------------------------------------
      e.Row.EndEdit()
      e.Row.EndEdit()
      '--------------------------------------------------------------------------------------------------------------
      Dim strFunction As String = "AfterColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If
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
  Public Overridable Sub BeforeColUpdate_tb_codcaum(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Dim strErrore As String = ""

    Try
      If dsShared.Tables(strNomeTabella).Rows.Count > 1 Then
        dtrTmp = dsShared.Tables(strNomeTabella).Select("tb_codcaum = " & e.ProposedValue.ToString())
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128167225177499338, "Codice causale di magazzino già esistente: inserire un nuovo codice.")))
          Return
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_descaum(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.ProposedValue.ToString = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129127718177262010, "Descrizione causale di magazzino obbligatoria.")))
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_causec(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim strErr As String = ""

    Try
      If e.ProposedValue.ToString <> "0" Then
        If oCldCaum.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAUM", "N", strTmp) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129127718218668260, "Codice causale secondaria inesistente.")))
          Return
        Else
          e.Row!xx_descaum = strTmp
        End If
      Else
        e.Row!xx_descaum = ""
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_codcacalv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim strErr As String = ""

    Try
      If e.ProposedValue.ToString <> "0" Then
        If oCldCaum.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCACA", "N", strTmp) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129127718307418260, "Caus CA costo lavoraz. (su C.P.)")))
          Return
        Else
          e.Row!xx_codcacalv = strTmp
        End If
      Else
        e.Row!xx_codcacalv = ""
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_tb_codcacadd(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim strErr As String = ""

    Try
      If e.ProposedValue.ToString <> "0" Then
        If oCldCaum.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCACA", "N", strTmp) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129127718350543260, "Caus CA DDT e Scarico materiale")))
          Return
        Else
          e.Row!xx_codcacadd = strTmp
        End If
      Else
        e.Row!xx_codcacadd = ""
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
#End Region

  Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      strDittaCorrente = strDitta
      dReturn = oCldCaum.GetData(strDitta, ds)
      If dReturn = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      oCldCaum.SetTableDefaultValueFromDB(strNomeTabella, ds)
      SetDefaultValue(ds)
      dsShared = ds
      '--------------------------------------------------------------------------------------------------------------
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanged, AddressOf AfterColUpdate
      '--------------------------------------------------------------------------------------------------------------
      strActLog = oCldCaum.GetSettingBus("BSMGCAUM", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      If strActLog = "-1" Then
        strActLogProg = "BSMGCAUM"
        strActLogNomOggLog = strNomeTabella
        strActLogDesLog = oApp.Tr(Me, 128085965824171721, "Causali di magazzino")
      End If
      '--------------------------------------------------------------------------------------------------------------
      bHasChanges = False
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

  Public Overridable Function ControlliPreSalva(ByVal bUltcos As Boolean, ByVal bDtultcar As Boolean, _
    ByVal strTipcaum As String, ByVal strDestipcaum As String, ByVal strEsist As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not (bUltcos = bDtultcar) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128169627739632751, "Attenzione: per le causali di acquisto/carico da produzione normalmente il flag 'Data ultimo carico'" & vbCrLf & "deve essere impostato come il falg 'Ultimo costo'" & vbCrLf & _
          "diversamente le varie procedure non saranno in grado di rilevare a quale data è riferito l'ultimo costo")))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case UCase(strTipcaum)
        Case "S", "E", "F", "Y"
          If strEsist <> "-" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128169703886169678, "Se il tipo causale è '|" & strDestipcaum & "|' l'Esistenza Attuale deve essere negativa.")))
            Return False
          End If
        Case "C", "K", "P", "X"
          If strEsist <> "+" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128169704127155654, "Se il tipo causale è '|" & strDestipcaum & "|' l'Esistenza Attuale deve essere positiva.")))
            Return False
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
    End Try
  End Function

  Public Overrides Sub Nuovo()
    Try
      dsShared.Tables(strNomeTabella).Rows.Add(dsShared.Tables(strNomeTabella).NewRow)
      bHasChanges = True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overrides Function Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    Try
      dsShared.Tables(strNomeTabella).Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overrides Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldCaum.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella), strActLogProg, strActLogNomOggLog, strActLogDesLog)
      If bResult Then bHasChanges = False
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      Try
        ds.Tables(strNomeTabella).Columns("codditt").DefaultValue = strDittaCorrente
      Catch
      End Try
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  
  Public Overrides Function TestPreSalva() As Boolean
    Try
      Dim dtrTmp As DataRow() = dsShared.Tables(strNomeTabella).Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      Dim i As Integer = 0
      For i = 0 To dtrTmp.Length - 1
        If dtrTmp(i)!tb_descaum.ToString = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128484105244687501, "Descrizione causale di magazzino obbligatoria.")))
          Return False
        End If

        'If NTSCInt(dtrTmp(i)!tb_esist) = 0 Then
        '  'se la causale è nulla non può muovere l'analitica duplice contabile
        '  'ora può farlo. sembra un test 'vecchio' non usato in veboll/gsor
        '  If NTSCInt(dtrTmp(i)!tb_codcacadd) <> 0 Or NTSCInt(dtrTmp(i)!tb_codcacalv) <> 0 Then
        '    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129272802542539062, "Con causali che NON movimentano l'ESISTENZA ATTUALE non è possibile indicare causali che operano sulla Contabilità analitica duplice contabile")))
        '    Return False
        '  End If
        'End If

      Next
      If Not TestMovimentaContemporaneamenteAcquistiVendite() Then
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If FlagModificati() = False Then Return False
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

  Public Overridable Function TestMovimentaContemporaneamenteAcquistiVendite() As Boolean
    Dim nCarichiFornitoriMovimentati As Integer = 0
    Dim nScarichiClientiMovimentati As Integer = 0
    Dim nResiClientiMovimentati As Integer = 0
    Dim nResiFornitoriMovimentati As Integer = 0
    Dim nRisultatoMovimentazioneCausale As Integer = 0

    Dim strbMessMovimentato As New StringBuilder
    Dim strbMess As New StringBuilder
    Try
      Dim dtrT As DataRow() = dsShared.Tables(strNomeTabella).Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrT.Length <> 0 Then
        If NTSCInt(dtrT(0)!tb_carfor) <> 0 OrElse NTSCInt(dtrT(0)!tb_vcarfor) <> 0 Then
          nCarichiFornitoriMovimentati = 1
          strbMessMovimentato.AppendLine(oApp.Tr(Me, 130096234501155807, _
                              "-->Carichi Fornitori"))
        End If

        If NTSCInt(dtrT(0)!tb_scacli) <> 0 OrElse NTSCInt(dtrT(0)!tb_vscacli) <> 0 Then
          nScarichiClientiMovimentati = 1
          strbMessMovimentato.AppendLine(oApp.Tr(Me, 130096234501155808, _
                              "-->Scarichi Clienti"))
        End If

        If NTSCInt(dtrT(0)!tb_rescli) <> 0 OrElse NTSCInt(dtrT(0)!tb_vrescli) <> 0 Then
          nResiClientiMovimentati = 1
          strbMessMovimentato.AppendLine(oApp.Tr(Me, 130096234501155809, _
                              "-->Resi Clienti"))
        End If

        If NTSCInt(dtrT(0)!tb_resfor) <> 0 OrElse NTSCInt(dtrT(0)!tb_vresfor) <> 0 Then
          nResiFornitoriMovimentati = 1
          strbMessMovimentato.AppendLine(oApp.Tr(Me, 130096234501155810, _
                              "-->Resi Fornitori"))
        End If
      End If

      nRisultatoMovimentazioneCausale = nCarichiFornitoriMovimentati + nScarichiClientiMovimentati _
                                      + nResiClientiMovimentati + nResiFornitoriMovimentati
      If nRisultatoMovimentazioneCausale >= 2 Then
        strbMess.Append(oApp.Tr(Me, 130096234501155811, _
                "Si sta salvando la causale ")) _
                .Append(NTSCStr(dtrT(0)!tb_codcaum)) _
                .AppendLine(oApp.Tr(Me, 130096234501155812, " che movimenta contemporaneamente il seguente:"))

        strbMess.AppendLine(strbMessMovimentato.ToString)
        strbMess.Append(oApp.Tr(Me, 130096234501155813, _
                 "Questa impostazione può portare a non corrette esistenze o valorizzazioni di magazzino. Proseguire comunque?"))

        Dim evt As NTSEventArgs = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, strbMess.ToString)
        ThrowRemoteEvent(evt)
        If evt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
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

  Public Overridable Function FlagModificati() As Boolean
    Dim bModificaEsist As Boolean = False
    Dim strMsg As String = ""
    Dim msg As NTSEventArgs

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim dtrT As DataRow() = dsShared.Tables(strNomeTabella).Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      If dtrT.Length = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(dtrT(0)!tb_esist) <> NTSCInt(dtrT(0)("tb_esist", DataRowVersion.Original)) Then
        bModificaEsist = True
        strMsg += oApp.Tr(Me, 130096216194349336, " . Esistenza attuale") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_carfor) <> NTSCInt(dtrT(0)("tb_carfor", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096221374919356, " . Carichi da fornitore (Quantità)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_vcarfor) <> NTSCInt(dtrT(0)("tb_vcarfor", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096221998878862, " . Carichi da fornitore (Valore)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_carpro) <> NTSCInt(dtrT(0)("tb_carpro", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096222520275836, " . Carichi da produzione (Quantità)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_vcarpro) <> NTSCInt(dtrT(0)("tb_vcarpro", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096222729844430, " . Carichi da produzione (Valore)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_carvar) <> NTSCInt(dtrT(0)("tb_carvar", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096223046858556, " . Carichi vari (Quantità)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_vcarvar) <> NTSCInt(dtrT(0)("tb_vcarvar", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096223224609522, " . Carichi vari (Valore)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_giaini) <> NTSCInt(dtrT(0)("tb_giaini", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096223599545794, " . Giacenza iniziale (Quantità)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_vgiaini) <> NTSCInt(dtrT(0)("tb_vgiaini", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096223823216492, " . Giacenza iniziale (Valore)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_rescli) <> NTSCInt(dtrT(0)("tb_rescli", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096224272452492, " . Resi da clienti (Quantità)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_vrescli) <> NTSCInt(dtrT(0)("tb_vrescli", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096224496543128, " . Resi da clienti (Valore)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_scacli) <> NTSCInt(dtrT(0)("tb_scacli", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096224958181948, " . Scarichi per vendite (Quantità)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_vscacli) <> NTSCInt(dtrT(0)("tb_vscacli", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096225190710408, " . Scarichi per vendite (Valore)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_scapro) <> NTSCInt(dtrT(0)("tb_scapro", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096225512568470, " . Scarichi a produzione (Quantità)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_vscapro) <> NTSCInt(dtrT(0)("tb_vscapro", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096225690387798, " . Scarichi a produzione (Valore)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_scavar) <> NTSCInt(dtrT(0)("tb_scavar", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096225972927944, " . Altri scarichi (Quantità)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_vscavar) <> NTSCInt(dtrT(0)("tb_vscavar", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096226160278888, " . Altri scarichi (Valore)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_resfor) <> NTSCInt(dtrT(0)("tb_resfor", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096226514726092, " . Resi a fornitori (Quantità)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_vresfor) <> NTSCInt(dtrT(0)("tb_vresfor", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096226691852034, " . Resi a fornitori (Valore)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_valoriz) <> NTSCInt(dtrT(0)("tb_valoriz", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096227464401230, " . Valorizzazione (Quantità)") & vbCrLf
      End If
      If NTSCInt(dtrT(0)!tb_vvaloriz) <> NTSCInt(dtrT(0)("tb_vvaloriz", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096227659828656, " . Valorizzazione (Valore)") & vbCrLf
      End If
      If NTSCStr(dtrT(0)!tb_ultcos) <> NTSCStr(dtrT(0)("tb_ultcos", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096233535835536, " . Ultimo costo") & vbCrLf
      End If
      If NTSCStr(dtrT(0)!tb_ultpre) <> NTSCStr(dtrT(0)("tb_ultpre", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096233824811476, " . Ultimo prezzo") & vbCrLf
      End If
      If NTSCStr(dtrT(0)!tb_dtulcar) <> NTSCStr(dtrT(0)("tb_dtulcar", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096234233000978, " . Data ultimo carico") & vbCrLf
      End If
      If NTSCStr(dtrT(0)!tb_dtulsca) <> NTSCStr(dtrT(0)("tb_dtulsca", DataRowVersion.Original)) Then
        strMsg += oApp.Tr(Me, 130096234501155806, " . Data ultimo scarico") & vbCrLf
      End If
      '--------------------------------------------------------------------------------------------------------------
      If strMsg.Trim = "" Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If oCldCaum.CausaleMovimentata(strDittaCorrente, NTSCInt(dtrT(0)!tb_codcaum)) = False Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Dim strIIf1 As String = IIf(bModificaEsist = True, _
                              oApp.Tr(Me, 130427318594163062, " --> Movimenti di magazzino") & vbCrLf, "").ToString
      Dim strIIF2 As String = IIf(bModificaEsist = True, vbCrLf & _
                              oApp.Tr(Me, 130427319064322321, "selezionando il flag 'Ricrea tabelle keymag, keyord, keyprb'"), "").ToString
      strMsg = oApp.Tr(Me, 130096265471772292, "ATTENZIONE!" & vbCrLf & vbCrLf & _
        "La causale risulta movimentata" & vbCrLf & _
        "mentre sono stati modificati i seguenti campi:" & vbCrLf & "|" & strMsg & "|" & vbCrLf & _
        "L'operazione potrebbe determinare il 'disallineamento' di:" & vbCrLf & "|" & strIIf1 & "|" & _
        " --> Progressivi correnti articoli" & vbCrLf & _
        " --> Progressivi definitivi articoli" & vbCrLf & _
        " --> Valorizzazioni" & vbCrLf & _
        "Sarà, quindi, necessario provvedere alla ricostruzione dei progressivi attraverso il programma apposito" & "|" & _
         strIIF2 & "|." & vbCrLf & vbCrLf & _
        "Proseguire?")
      msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, strMsg)
      ThrowRemoteEvent(msg)
      If msg.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
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

  Public Overridable Function CheckCod(ByVal strCod As String) As Boolean
    Try
      If strCod = "0" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130722814774277525, "Codice causale obbligatorio.")))
        Return False
      End If

      If oCldCaum.ValCodiceDb(strCod, strDittaCorrente, "TABCAUM", "N") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130722814796309339, "Codice causale '|" & strCod & "|' già esistente.")))
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
  Public Overridable Function Duplica(ByVal strCodNew As String, ByVal strCodOld As String) As Boolean
    Dim i As Integer
    Dim strColname As String = ""
    Dim dtrTmp() As DataRow = Nothing
    Try
      dsShared.Tables("TABCAUM").AcceptChanges()

      If dsShared.Tables("TABCAUM").Rows.Count > 0 Then
        dtrTmp = dsShared.Tables("TABCAUM").Select("tb_codcaum = " & strCodOld)
        If dtrTmp.Length = 0 Then
          Return False
        End If
      Else
        Return False
      End If

      dsShared.Tables("TABCAUM").Rows.Add()
      dsShared.Tables("TABCAUM").Rows(dsShared.Tables("TABCAUM").Rows.Count - 1)("tb_codcaum") = strCodNew

      For i = 0 To dsShared.Tables("TABCAUM").Columns.Count - 1
        strColname = dsShared.Tables("TABCAUM").Columns(i).ColumnName
        If strColname = "tb_codcaum" Or strColname = "ts" Then Continue For

        dsShared.Tables("TABCAUM").Rows(dsShared.Tables("TABCAUM").Rows.Count - 1)(strColname) = dtrTmp(0)(strColname)
      Next

      If Not Salva(False) Then Return False
      dsShared.AcceptChanges()

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

End Class
