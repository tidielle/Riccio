Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEXXFILT
  Inherits CLE__BASN

#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
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

  Public oCldFilt As CLDXXFILT
  Public strProgrChiamante As String = ""
  Public arTabelle As New ArrayList
  Public dttCampiTabelle As DataTable = Nothing
  Public dttFilt As DataTable = Nothing

  Public Overrides Function Init(ByRef App As CLE__APP, _
                                    ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                                    ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                                    ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDXXFILT"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldFilt = CType(MyBase.ocldBase, CLDXXFILT)
    oCldFilt.Init(oApp)

    Return True
  End Function

#Region "Gestione Caricamento Tabelle\Campi"
  Public Overridable Function AggiungiTabella(ByVal strTabella As String) As Boolean
    Try
      'La tabella c'era già o se i dati sono già stati caricati non deve fare nulla.
      If Not dttCampiTabelle Is Nothing OrElse arTabelle.Contains(strTabella) Then Return False

      arTabelle.Add(strTabella)

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
  Public Overridable Function RimuoviTabella(ByVal strTabella As String) As Boolean
    Try
      'La tabella non c'è o se i dati sono già stati caricati non deve fare nulla.
      If Not dttCampiTabelle Is Nothing OrElse Not arTabelle.Contains(strTabella) Then Return False

      arTabelle.Remove(strTabella)

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

  Public Overridable Function CaricaCampiTabelle() As Boolean
    Dim strCol As String = ""
    Dim dtrT1() As DataRow = Nothing
    Try
      If Not oCldFilt.CaricaCampiTabelle(strDittaCorrente, arTabelle, dttCampiTabelle) Then Return False

      '---------------------------------------
      'Allargo i campi 'cod. articolo'
      If CLN__STD.CodartMaxLen > 18 Then
        For Each dtrT As DataRow In dttCampiTabelle.Select("(cb_tipocampo = 10 AND cb_size = 18) OR (cb_nomtab = 'allole' AND cb_nomcampo = 'allole.ao_strcod')")

          strCol = NTSCStr(dtrT!cb_nomcampo).ToString.ToLower.Substring(NTSCStr(dtrT!cb_nomcampo).IndexOf("."))
          If strCol.IndexOf("matri") = -1 And strCol.IndexOf("telef") = -1 And strCol.IndexOf("cell") = -1 And strCol.IndexOf("fax") = -1 And _
                         strCol.IndexOf("ubic") = -1 And strCol.IndexOf("barco") = -1 And strCol.IndexOf("bc_code") = -1 And strCol.IndexOf("idpall") = -1 Then

            dtrT!cb_size = CLN__STD.CodartMaxLen
          End If
        Next

        dtrT1 = dttCampiTabelle.Select("(cb_nomtab = 'artico' AND cb_nomcampo = 'artico.ar_codroot')")
        If dtrT1.Length > 0 Then dtrT1(0)!cb_size = CLN__STD.CodartMaxLen

        dtrT1 = dttCampiTabelle.Select("(cb_nomtab = 'artroot' AND cb_nomcampo = 'artroot.arr_codroot')")
        If dtrT1.Length > 0 Then dtrT1(0)!cb_size = CLN__STD.CodartMaxLen

        dtrT1 = dttCampiTabelle.Select("(cb_nomtab = 'artvar' AND cb_nomcampo = 'artvar.arv_codroot')")
        If dtrT1.Length > 0 Then dtrT1(0)!cb_size = CLN__STD.CodartMaxLen

        dtrT1 = dttCampiTabelle.Select("(cb_nomtab = 'TTARTICOX' AND cb_nomcampo = 'TTARTICOX.ar_codroot')")
        If dtrT1.Length > 0 Then dtrT1(0)!cb_size = CLN__STD.CodartMaxLen

      End If    'If CLN__STD.CodartMaxLen > 18 Then
      dttCampiTabelle.AcceptChanges()

      '---------------------------------------
      'allargo i campi serie
      If CLN__STD.SerieMaxLen > 1 Then
        For Each dtrT As DataRow In dttCampiTabelle.Select("(cb_tipocampo = 10 AND cb_size = 1)")
          strCol = NTSCStr(dtrT!cb_nomcampo).ToString.ToLower.Substring(NTSCStr(dtrT!cb_nomcampo).IndexOf("."))
          If strCol.Contains("serie") Or strCol.Contains("_alfp") Or strCol.Contains("_alfd") Then
            dtrT!cb_size = CLN__STD.SerieMaxLen
          End If
        Next
      End If    'If CLN__STD.SerieMaxLen > 1 Then
      dttCampiTabelle.AcceptChanges()

      For Each dtrRow As DataRow In dttCampiTabelle.Rows
        dtrRow!cb_descampo = NTSCStr(dtrRow!cb_descampo).Replace(NTSCStr(dtrRow!cb_nomtab) & " - ", "")
        dtrRow!cb_destab = NTSCStr(dtrRow!cb_destab).Replace(NTSCStr(dtrRow!cb_nomtab) & " - ", "")
      Next

      'Aggiunge la riga che indica che non è selezionato nessun valore.
      dttCampiTabelle.Rows.InsertAt(dttCampiTabelle.NewRow, 0)
      dttCampiTabelle.Rows(0)!cb_nomcampo = "-"
      dttCampiTabelle.Rows(0)!cb_descampo = oApp.Tr(Me, 130415872327135661, "[Filtro non selezionato]")
      dttCampiTabelle.AcceptChanges()

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

  Public Overridable Function PreparaDatiIniziali(ByRef dttFiltOut As DataTable) As Boolean
    Dim lRiga As Integer = 1
    Try
      dttFilt = New DataTable
      dttFilt.Columns.Add("xx_nome", GetType(String))
      dttFilt.Columns.Add("xx_descampo", GetType(String))
      dttFilt.Columns.Add("xx_tipo", GetType(String))
      dttFilt.Columns.Add("xx_valore", GetType(String))
      dttFilt.Columns.Add("xx_valorea", GetType(String))
      dttFilt.Columns.Add("xx_tipocampo", GetType(Integer))
      dttFilt.Columns.Add("xx_size", GetType(Integer))

      'Sempre 20 righe, non si può mettere un numero arbitrario di filtri.
      For lRiga = 1 To 20
        dttFilt.Rows.Add(New Object() {"-", dttCampiTabelle.Rows(0)!cb_descampo, "=", "", "", 0, 0})
      Next

      dttFilt.AcceptChanges()

      dttFiltOut = dttFilt

      AddHandler dttFilt.ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dttFilt.ColumnChanged, AddressOf AfterColUpdate

      'Ora carica i dati salvati precedentemente
      Dim strRecents() As String = NTSCStr(oCldFilt.GetSettingBus(strProgrChiamante, "RECENT", ".", "FiltriExt", "", " ", "")).Split(New String() {"#§#"}, StringSplitOptions.RemoveEmptyEntries)

      lRiga = 0
      For Each strRecent As String In strRecents
        If strRecent.Trim = "" Then Continue For
        Dim strValori() As String = strRecent.Split(New String() {"*|*"}, StringSplitOptions.None)
        If strValori.Length >= 2 Then
          dttFilt.Rows(lRiga)!xx_nome = strValori(0)
          dttFilt.Rows(lRiga)!xx_tipo = strValori(1)
          If strValori.Length >= 4 Then 'Ho salvato anche i valori
            dttFilt.Rows(lRiga)!xx_valore = strValori(2)
            dttFilt.Rows(lRiga)!xx_valorea = strValori(3)
          End If

          'Ricarica la descrizione
          Dim dtrRow() As DataRow = dttCampiTabelle.Select("cb_nomcampo = " & CStrSQL(strValori(0)))
          If dtrRow.Length > 0 Then dttFilt.Rows(lRiga)!xx_descampo = dttCampiTabelle.Rows(0)!cb_descampo
        End If
        lRiga += 1
        If lRiga = 20 Then Exit For 'è arrivato al massimo numero di filtri consentiti
      Next

      dttFilt.AcceptChanges()

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

  Public Overridable Sub SalvaFiltri(ByVal bAncheValori As Boolean)
    Dim dtrFiltri() As DataRow
    Dim strTmp As String = ""
    Try
      dtrFiltri = dttFilt.Select("xx_nome <> '-'")
      '-------------------------------------------------
      'salvo il recent
      For Each dtrFiltro As DataRow In dtrFiltri
        strTmp &= NTSCStr(dtrFiltro!xx_nome) & "*|*" & NTSCStr(dtrFiltro!xx_tipo)
        If bAncheValori Then strTmp &= "*|*" & NTSCStr(dtrFiltro!xx_valore) & "*|*" & NTSCStr(dtrFiltro!xx_valorea)
        strTmp &= "#§#"
      Next
      oCldFilt.SaveSettingBus(strProgrChiamante, "RECENT", ".", "FiltriExt", strTmp, " ", "NS.", "NS.", "...")

      oCldFilt.SaveSettingBus(strProgrChiamante, "RECENT", ".", "AncheValori", NTSCStr(IIf(bAncheValori, "S", "N")), " ", "NS.", "NS.", "...")
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

#Region "Eventi Before\AfterColUpdate"
  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
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
  Public Overridable Sub BeforeColUpdate_xx_descampo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.ProposedValue) = "" Then
        If NTSCStr(e.Row!xx_nome) = "-" Then e.ProposedValue = e.Row!xx_descampo 'Il campo era già vuoto, ripristino il dato precedente.
      Else
        'Se modifico manualmente la descrizione del campo la sovrascrivo
        Dim dtrRow() As DataRow = dttCampiTabelle.Select("cb_nomcampo = " & CStrSQL(e.Row!xx_nome))
        If dtrRow.Length > 0 Then
          If NTSCStr(e.ProposedValue) <> NTSCStr(dtrRow(0)!cb_descampo) Then e.ProposedValue = dtrRow(0)!cb_descampo
        Else
          'Il campo corrente non esiste (strano), quindi lo svuoto
          e.ProposedValue = ""
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_xx_tipo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'La IN è particolare, visto che ha la separazione con le "," il suo valore non sarebbe compatibile con gli altri filtri.
      'Quindi si opta per svuotarlo quando si cambia tipo di filtro
      If NTSCInt(e.Row!xx_tipocampo) <> 10 AndAlso NTSCInt(e.Row!xx_tipocampo) <> 12 AndAlso NTSCStr(e.ProposedValue).Contains("LIKE") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130416115025263770, "Solo i campi stringa accettano come 'Tipo Filtro' i valori 'inizia con', 'non inizia con', 'è simile a', 'non è simile a'.")))
        e.ProposedValue = e.Row!xx_tipo
      End If

      If (NTSCStr(e.Row!xx_tipo) = "IN" OrElse NTSCStr(e.Row!xx_tipo) = "NOT IN") AndAlso _
         (NTSCStr(e.ProposedValue) <> "IN" AndAlso NTSCStr(e.ProposedValue) <> "NOT IN") Then
        e.Row!xx_valore = ""
        e.Row!xx_valorea = ""
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
  End Sub
  Public Overridable Sub BeforeColUpdate_xx_valore(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.ProposedValue = ValidaValoreCampo(NTSCStr(e.ProposedValue), e.Row, "xx_valore")
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
  Public Overridable Sub BeforeColUpdate_xx_valorea(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.ProposedValue = ValidaValoreCampo(NTSCStr(e.ProposedValue), e.Row, "xx_valore")
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
  Public Overridable Function ValidaValoreCampo(ByVal strProposedValue As String, ByVal dtrRow As DataRow, ByVal strColonna As String) As String
    Dim strParti() As String
    Try
      If strProposedValue = "" Then Return ""
      'Il filtro IN è particolare, devo verificare i singoli valori prima e non la formula completa
      If NTSCStr(dtrRow!xx_tipo) = "IN" OrElse NTSCStr(dtrRow!xx_tipo) = "NOT IN" Then
        strParti = strProposedValue.Trim(";"c).Split(";"c) 'Sprima di spezzare tolgo eventuali ; iniziali o finali
      Else
        ReDim strParti(0)
        strParti(0) = strProposedValue
      End If

      strProposedValue = ""
      'Determino il tipo di colonna
      For Each strParte As String In strParti
        Select Case NTSCInt(dtrRow!xx_tipocampo)
          Case 3 'Small Int
            strParte = strParte.Replace(".", ",").Trim
            If IsNumeric(strParte) Then
              Dim lValore As Integer = NTSCInt(strParte)
              If lValore < -9999 OrElse lValore > 9999 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415981653353381, "Indicare un numero intero di massimo 4 cifre.")))
                strProposedValue = NTSCStr(dtrRow(strColonna)) & ";"
                Exit For
              Else
                strProposedValue &= lValore.ToString() & ";"
              End If
            Else
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415980215976239, "Indicare un numero intero.")))
              strProposedValue = NTSCStr(dtrRow(strColonna)) & ";"
              Exit For
            End If
          Case 4 'Int 
            strParte = strParte.Replace(".", ",").Trim
            If IsNumeric(strParte) Then
              Dim lValore As Integer = NTSCInt(strParte)
              If lValore < -999999999 OrElse lValore > 999999999 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415982119414839, "Indicare un numero intero di massimo 9 cifre.")))
                strProposedValue = NTSCStr(dtrRow(strColonna)) & ";"
                Exit For
              Else
                strProposedValue &= lValore.ToString() & ";"
              End If
            Else
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415982135884025, "Indicare un numero intero.")))
              strProposedValue = NTSCStr(dtrRow(strColonna)) & ";"
              Exit For
            End If
          Case 5 'Money
            strParte = strParte.Replace(".", ",").Trim
            If IsNumeric(strParte) Then
              strProposedValue &= NTSCDec(strParte).ToString & ";"
            Else
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415982795062617, "Sono accettati solo numeri.")))
              strProposedValue = NTSCStr(dtrRow(strColonna)) & ";"
              Exit For
            End If
          Case 6, 7 'Real e Float
            strParte = strParte.Replace(".", ",").Trim
            If IsNumeric(strParte) Then
              strProposedValue &= NTSCDec(strParte).ToString & ";"
            Else
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415982937127943, "Sono accettati solo numeri.")))
              strProposedValue = NTSCStr(dtrRow(strColonna)) & ";"
              Exit For
            End If
          Case 8 'Data
            Dim dtDate As Date
            If Date.TryParse(strParte.Trim, dtDate) Then
              strProposedValue &= dtDate.ToShortDateString & ";"
            Else
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415983369916513, "Indicare una data valida.")))
              strProposedValue = NTSCStr(dtrRow(strColonna)) & ";"
              Exit For
            End If
          Case 10 'Stringa
            If strParte.Length > NTSCInt(dtrRow!xx_size) And NTSCInt(dtrRow!xx_size) > 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415985525980709, "Il testo può essere lungo al massimo |" & NTSCInt(dtrRow!xx_size) & "|")))
              strProposedValue = NTSCStr(dtrRow(strColonna)) & ";"
              Exit For
            Else
              strProposedValue &= strParte & ";"
            End If
          Case 12 'Memo
            'Accetta qualsiasi testo, non c'è bisogno di fare controlli
            strProposedValue &= strParte & ";"
        End Select
      Next
      If strProposedValue.Length > 0 Then
        strProposedValue = strProposedValue.Remove(strProposedValue.Length - 1)
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
    Return strProposedValue
  End Function

  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

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
  Public Overridable Sub AfterColUpdate_xx_nome(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'Carica i dati del nuovo campo.
      If NTSCStr(e.ProposedValue) <> "-" Then
        Dim dtrRow() As DataRow = dttCampiTabelle.Select("cb_nomcampo = " & CStrSQL(e.ProposedValue))
        If dtrRow.Length > 0 Then
          e.Row!xx_tipocampo = dtrRow(0)!cb_tipocampo
          e.Row!xx_size = dtrRow(0)!cb_size
          e.Row!xx_descampo = dtrRow(0)!cb_descampo
        End If
      Else
        e.Row!xx_tipocampo = 0
        e.Row!xx_size = 0
        e.Row!xx_descampo = oApp.Tr(Me, 130420270623588797, "[Filtro non selezionato]")
      End If

      'Al cambio di campo ripristina i dati (quasi sicuramente non avrebbero senso, tipo passare da una descrizione ad un importo)
      e.Row!xx_tipo = "="
      e.Row!xx_valore = ""
      e.Row!xx_valorea = ""
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub AfterColUpdate_xx_descampo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'Se non è stato indicato nulla allora potrei voler svuotare il campo.
      If NTSCStr(e.ProposedValue) = "" Then e.Row!xx_nome = "-"
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

#Region "Valori di ritorno"
  Public Overridable Function GeneraQuerySQL(ByVal dtrFiltri() As DataRow) As String
    Try
      If dtrFiltri.Length = 0 Then Return ""

      Return oCldFilt.GeneraQuerySQL(dtrFiltri)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return ""
  End Function

  Public Overridable Function GeneraQueryReport(ByVal dtrFiltri() As DataRow) As String
    Dim strCrpe As String = ""
    Try
      If dtrFiltri.Length = 0 Then Return ""

      'Pepara la query per il report, in base ai filtri selezionati, al tipo di filtro e al report
      For Each dtrFiltro As DataRow In dtrFiltri
        If NTSCStr(dtrFiltro!xx_valore) = "" Then Continue For
        Select Case NTSCStr(dtrFiltro!xx_tipo)
          Case "NOT IN", "NOT BET", "NOT START", "NOT LIKE"
            strCrpe &= " NOT("
        End Select

        strCrpe &= "{" & NTSCStr(dtrFiltro!xx_nome) & "}"

        Select Case NTSCStr(dtrFiltro!xx_tipo)
          Case "=", "<>", "<", "<=", ">", ">="
            strCrpe &= " " & NTSCStr(dtrFiltro!xx_tipo) & " " & ConvertiValoreReport(NTSCStr(dtrFiltro!xx_valore), NTSCInt(dtrFiltro!xx_tipocampo))
          Case "IN", "NOT IN"
            strCrpe &= " " & NTSCStr(dtrFiltro!xx_tipo) & " ["
            For Each strValore As String In NTSCStr(dtrFiltro!xx_valore).Split(";"c)
              strCrpe &= ConvertiValoreReport(strValore.Trim, NTSCInt(dtrFiltro!xx_tipocampo)) & ","
            Next
            strCrpe = strCrpe.Remove(strCrpe.Length - 1) 'Rimuove l'ultima virgoale
            strCrpe &= "]"
          Case "BET", "NOT BET"
            strCrpe &= " IN " & ConvertiValoreReport(NTSCStr(dtrFiltro!xx_valore), NTSCInt(dtrFiltro!xx_tipocampo)) & _
                       " TO " & ConvertiValoreReport(NTSCStr(dtrFiltro!xx_valorea), NTSCInt(dtrFiltro!xx_tipocampo))
          Case "START", "NOT START"
            strCrpe &= " LIKE " & ConvertiValoreReport(NTSCStr(dtrFiltro!xx_valore) & "*", NTSCInt(dtrFiltro!xx_tipocampo))
          Case "LIKE", "NOT LIKE"
            strCrpe &= " LIKE " & ConvertiValoreReport("*" & NTSCStr(dtrFiltro!xx_valore) & "*", NTSCInt(dtrFiltro!xx_tipocampo))
        End Select

        Select Case NTSCStr(dtrFiltro!xx_tipo)
          Case "NOT IN", "NOT BET", "NOT START", "NOT LIKE"
            strCrpe &= ")"
        End Select

        strCrpe &= " AND "
      Next

      If strCrpe.Length > 0 Then strCrpe = strCrpe.Remove(strCrpe.Length - 4)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return strCrpe
  End Function

  Public Overridable Function ConvertiValoreReport(ByVal strValore As String, ByVal lTipo As Integer) As String
    Dim strSQL As String = ""
    Try
      Select Case lTipo
        Case 3, 4 'SmallInt/Int
          Return strValore
        Case 5, 6, 7 'Money,Real e Float
          Return CDblSQL(strValore).ToString
        Case 8 'Data
          Return ConvDataRpt(strValore).ToString
        Case 10, 12 'Stringa, Memo
          Return ConvStrRpt(strValore)
        Case Else
          Return strValore
      End Select
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return ""
  End Function
#End Region
End Class

