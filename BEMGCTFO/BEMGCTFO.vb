#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class CLEMGCTFO
  Inherits CLE__BASE

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG
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

#Region "Variabili"
  Public oCldCtfo As CLDMGCTFO

  Public bServer As Boolean

  Public lInstid As Integer
  Public nCodiva As Integer
  Public bGestTabUnmis As Boolean
  Public strUnmis As String
  Public nCodArtDaCatNListPubb As Integer
  Public nCodArtDaCatNListIngr As Integer
  Public bProponiSiglaForn As Boolean
  Public nCodArtDaCat As Integer

#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGCTFO"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldCtfo = CType(MyBase.ocldBase, CLDMGCTFO)
    oCldCtfo.Init(oApp)
    Return True
  End Function

  Public Overridable Function ApriDettaglio(ByVal strDitta As String, ByRef ds As System.Data.DataSet, ByVal nPosition As Integer) As Boolean
    Try
      If oCldCtfo.LeggiDettaglio(strDitta, strNomeTabella, ds, nPosition) Then
        '--------------------------------------------------------------
        'Imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
        oCldCtfo.SetTableDefaultValueFromDB(strNomeTabella, ds)
        oCldCtfo.SetTableDefaultValueFromDB(strNomeTabella, strNomeTabella & "Dettaglio", ds)
        SetDefaultValue(ds)
        dsShared = ds
        '--------------------------------------
        'Creo gli eventi per la gestione del Datatable dentro l'Entity
        AddHandler dsShared.Tables(strNomeTabella).ColumnChanging, AddressOf BeforeColUpdate
        AddHandler dsShared.Tables(strNomeTabella).ColumnChanged, AddressOf AfterColUpdate
        AddHandler dsShared.Tables(strNomeTabella & "Dettaglio").ColumnChanging, AddressOf BeforeColUpdate
        AddHandler dsShared.Tables(strNomeTabella & "Dettaglio").ColumnChanged, AddressOf AfterColUpdate
        bHasChanges = False
        Return True
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
  Public Overridable Function ApriLayout(ByVal strDitta As String, ByRef ds As System.Data.DataSet) As Boolean
    Try
      If oCldCtfo.LeggiLayout(strDitta, strNomeTabella, ds) Then
        '--------------------------------------------------------------
        'Imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
        oCldCtfo.SetTableDefaultValueFromDB(strNomeTabella, ds)
        oCldCtfo.SetTableDefaultValueFromDB(strNomeTabella, strNomeTabella & "Dettaglio", ds)
        SetDefaultValue(ds)
        dsShared = ds
        '--------------------------------------
        'Creo gli eventi per la gestione del Datatable dentro l'Entity
        AddHandler dsShared.Tables(strNomeTabella).ColumnChanging, AddressOf BeforeColUpdate
        AddHandler dsShared.Tables(strNomeTabella).ColumnChanged, AddressOf AfterColUpdate
        AddHandler dsShared.Tables(strNomeTabella & "Dettaglio").ColumnChanging, AddressOf BeforeColUpdate
        AddHandler dsShared.Tables(strNomeTabella & "Dettaglio").ColumnChanged, AddressOf AfterColUpdate
        bHasChanges = False
        Return True
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
  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables(strNomeTabella & "Dettaglio").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables(strNomeTabella & "Dettaglio").Columns("ae_descr").DefaultValue = ""
      ds.Tables(strNomeTabella & "Dettaglio").Columns("ae_desint").DefaultValue = ""
      ds.Tables(strNomeTabella & "Dettaglio").Columns("ae_unmis").DefaultValue = strUnmis
      ds.Tables(strNomeTabella & "Dettaglio").Columns("ae_confez2").DefaultValue = ""
      ds.Tables(strNomeTabella & "Dettaglio").Columns("ae_codiva").DefaultValue = nCodiva
      ds.Tables(strNomeTabella & "Dettaglio").Columns("ae_status").DefaultValue = " "
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

  Public Overrides Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Dim dtrTmp() As DataRow

    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False

        'Aggiorno la data ultimo aggiornamento
        dtrTmp = dsShared.Tables(strNomeTabella & "Dettaglio").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
        For i As Integer = 0 To dtrTmp.Length - 1
          dtrTmp(i)!ae_ultagg = Date.Now
        Next
      End If


      '----------------------------------------
      'chiamo il dal per salvare
      bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella & "Dettaglio"), "", "", "")

      If bResult Then
        If bDelete Then dsShared.AcceptChanges()
        bHasChanges = False
      End If

      Return bResult
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
  Public Overrides Function TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables(strNomeTabella & "Dettaglio").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!ae_forn) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698513480312500, "Inserire il codice fornitore.")))
          Return False
        End If
        If Trim(NTSCStr(dtrTmp(i)!ae_codartf)) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698513244843750, "Inserire il codice articolo fornitore.")))
          Return False
        End If
        If InStr(NTSCStr(dtrTmp(i)!ae_codartf), "'") <> 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698517631875000, "Il codice articolo fornitore non può contenere apici semplici.")))
          Return False
        End If
        If NTSCStr(dtrTmp(i)!ae_descr) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698518506562500, "Descrizione articolo obbligatoria.")))
          Return False
        End If
        If NTSCStr(dtrTmp(i)!ae_unmis) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698518545625000, "Unità di misura principale obbligatoria.")))
          Return False
        End If
        If NTSCInt(dtrTmp(i)!ae_codiva) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698519512343750, "Codice iva obbligatorio.")))
          Return False
        End If
        If NTSCStr(dtrTmp(i)!ae_siglaforn) = "" Or ((nCodArtDaCat = 0 Or nCodArtDaCat = 3) And NTSCStr(dtrTmp(i)!ae_siglaforn) = " ") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698519540000000, "Sigla fornitore obbligatoria.")))
          Return False
        End If
      Next

      dtrTmp = dsShared.Tables(strNomeTabella & "Dettaglio").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCDate(dtrTmp(i)!ae_ultagg).ToString <> NTSCDate(oCldCtfo.GetTimeStampArtest(strDittaCorrente, _
          NTSCStr(dtrTmp(i)!ae_codartf), NTSCInt(dtrTmp(i)!ae_forn), NTSCInt(dtrTmp(i)!ae_codmarc))).ToString Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523810613114000, "Attenzione!" & vbCrLf & _
            "L'articolo è stato modificato da un altro utente o sessione." & vbCrLf & _
            "Possibile solo il ripristino.")))
          Return False
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

  Public Overloads Sub Nuovo(ByVal strForn As String, ByVal strCodartf As String, ByVal strCodmarc As String)
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsShared.Tables(strNomeTabella).Columns("ae_forn").DefaultValue = strForn
      dsShared.Tables(strNomeTabella & "Dettaglio").Columns("ae_forn").DefaultValue = strForn
      dsShared.Tables(strNomeTabella).Columns("ae_codartf").DefaultValue = strCodartf
      dsShared.Tables(strNomeTabella & "Dettaglio").Columns("ae_codartf").DefaultValue = strCodartf
      dsShared.Tables(strNomeTabella).Columns("ae_codmarc").DefaultValue = strCodmarc
      dsShared.Tables(strNomeTabella & "Dettaglio").Columns("ae_codmarc").DefaultValue = strCodmarc
      dsShared.Tables(strNomeTabella & "Dettaglio").Columns("ae_ultagg").DefaultValue = NTSCDate(DateTime.Now.ToString)
      If bProponiSiglaForn Then
        Dim strDescrForn As String = ""
        VerificaFornitore(strForn, strDescrForn)
        dsShared.Tables(strNomeTabella & "Dettaglio").Columns("ae_siglaforn").DefaultValue = Left(strDescrForn, 3)
      Else
        dsShared.Tables(strNomeTabella & "Dettaglio").Columns("ae_siglaforn").DefaultValue = " "
      End If
      dsShared.Tables(strNomeTabella).Rows.Add(dsShared.Tables(strNomeTabella).NewRow)
      dsShared.Tables(strNomeTabella & "Dettaglio").Rows.Add(dsShared.Tables(strNomeTabella & "Dettaglio").NewRow)
      bHasChanges = True

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

  Public Overloads Function Ripristina(ByVal nRow As Integer, _
                                       ByVal strFilter As String, _
                                       ByVal nRowDettaglio As Integer, _
                                       ByVal strFilterDettaglio As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables(strNomeTabella).Select(strFilter)(nRow).RejectChanges()
      dsShared.Tables(strNomeTabella & "Dettaglio").Select(strFilterDettaglio)(nRowDettaglio).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function LeggiDatiDitta() As Boolean
    Try
      'Creo l'instid per TTSELARTE
      lInstid = oCldCtfo.GetTblInstId("TTSELARTE", False)
      'Leggo le opzioni
      nCodiva = NTSCInt(oCldCtfo.GetSettingBus("BSMGCTFO", "OPZIONI", ".", "CodIva", "0", " ", "0"))
      bGestTabUnmis = CBool(oCldCtfo.GetSettingBus("BSMGCTFO", "OPZIONI", ".", "GestTabUnmis", "0", " ", "0"))
      strUnmis = oCldCtfo.GetSettingBus("BSMGCTFO", "OPZIONI", ".", "UnMis", "", " ", "")
      nCodArtDaCatNListPubb = NTSCInt(oCldCtfo.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListPubb", "11", " ", "11"))
      nCodArtDaCatNListIngr = NTSCInt(oCldCtfo.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListIngr", "12", " ", "12"))
      bProponiSiglaForn = CBool(oCldCtfo.GetSettingBus("BSMGCTFO", "OPZIONI", ".", "ProponiSiglaForn", "0", " ", "0"))
      nCodArtDaCat = NTSCInt(oCldCtfo.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoTipoGen", "0", " ", "0"))

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
  Public Overridable Function CaricaLabelTabling(ByRef strLing() As String) As Boolean
    Try
      Return oCldCtfo.CaricaLabelTabling(strDittaCorrente, "TABLING", strLing)
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

  Public Overridable Sub ResetTblInstId()
    Try
      oCldCtfo.ResetTblInstId("TTSELARTE", False, lInstid)
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

  Public Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
  Public Overridable Sub BeforeColUpdate_ae_gruppo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If Not NTSCInt(e.ProposedValue) = 0 Then

        If Not oCldCtfo.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABGMER", "N", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128691392524375000, "Gruppo inesistente.")))
          Return
        End If

      End If

      e.Row()!tb_desgmer = strTmp
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
  Public Overridable Sub BeforeColUpdate_ae_sotgru(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Dim nGrup As Integer
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!tb_dessgme = ""
        Exit Sub
      Else
        If Not oCldCtfo.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSGME", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557501483916812, "Codice sottogruppo inesistente.")))
          Return
        Else
          If NTSCInt(e.Row!ae_gruppo) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128729686830156250, "Indicare il gruppo prima di inserire il sottogruppo.")))
            e.ProposedValue = 0
            e.Row!tb_dessgme = ""
            Exit Sub
          Else
            nGrup = NTSCInt(Fix(NTSCInt(e.ProposedValue) / 100))
            If NTSCInt(e.ProposedValue) > 0 And (nGrup <> NTSCInt(e.Row!ae_gruppo)) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557504221967804, "Sottogruppo non appartenente al gruppo selezionato.")))
              e.ProposedValue = 0
              e.Row!tb_dessgme = ""
              Exit Sub
            End If
          End If
          e.Row!tb_dessgme = strOut
        End If
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
  Public Overridable Sub BeforeColUpdate_ae_clascon(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        If Not oCldCtfo.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCSAR", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698503810625000, "Classe di sconto inesistente.")))
          Return
        End If
      End If
      e.Row!tb_descsar = strOut
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
  Public Overridable Sub BeforeColUpdate_ae_codiva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        If Not oCldCtfo.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCIVA", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698506222656250, "Codice IVA inesistente.")))
          Return
        End If
      End If
      e.Row!tb_desciva = strOut
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
  Public Overridable Sub BeforeColUpdate_ae_famprod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = " "
    Try
      If NTSCStr(e.ProposedValue) = "" Then e.ProposedValue = " "
      If NTSCStr(e.ProposedValue) <> " " Then
        If Not oCldCtfo.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCFAM", "S", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698508075000000, "Codice IVA inesistente.")))
          Return
        End If
      End If
      e.Row!tb_descfam = strOut
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
  Public Overridable Sub BeforeColUpdate_ae_unmis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If Not bGestTabUnmis Then Return

      If NTSCStr(e.ProposedValue) <> "" Then
        If Not oCldCtfo.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABUMIS", "S") Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698539329843750, "L'unità di misura principale inserita non è valida.")))
          Return
        End If
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
  Public Overridable Sub BeforeColUpdate_ae_confez2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If Not bGestTabUnmis Then Return

      If NTSCStr(e.ProposedValue) <> "" Then
        If Not oCldCtfo.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABUMIS", "S") Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698540379375000, "L'unità di misura confezione inserita non è valida.")))
          Return
        End If
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

  Public Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If
      bHasChanges = True
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

  Public Overridable Function VerificaArticolo(ByRef dttTmp As DataTable, ByVal strForn As String, ByVal strCodartf As String, ByVal strCodmarc As String) As Boolean
    Try
      If oCldCtfo.VerificaArticolo(strDittaCorrente, strNomeTabella, dttTmp, strForn, strCodartf, strCodmarc) Then
        If dttTmp.Rows.Count > 0 Then
          Return True

        End If

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
  Public Overridable Function VerificaFornitore(ByVal strForn As String, ByRef strDescrForn As String) As Boolean
    Try
      Return oCldCtfo.ValCodiceDb(strForn, strDittaCorrente, "ANAGRA", "N", strDescrForn)
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
  Public Overridable Function VerificaMarca(ByVal Marca As String, ByRef strDescrMarca As String) As Boolean
    Try
      Return oCldCtfo.ValCodiceDb(Marca, strDittaCorrente, "TABMARC", "N", strDescrMarca)
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

  Public Overridable Function GetTimeStampArtest(ByVal strCodartf As String, ByVal lForn As Integer, ByVal nCodmarc As Integer) As DateTime
    Try
      Return oCldCtfo.GetTimeStampArtest(strDittaCorrente, strCodartf, lForn, nCodmarc)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function RitornaArtest(ByVal strCodartf As String, ByVal lForn As Integer, _
    ByVal nCodmarc As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldCtfo.RitornaArtest(strDittaCorrente, strCodartf, lForn, nCodmarc, dttOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function ImportaCatalogoFornitori(ByVal dttTmp As DataTable) As Boolean
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If CType(oCleComm, CLELBMENU).TestPreImportaArticoDaArtest(strDittaCorrente, strTmp) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", strTmp))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      With dttTmp.Rows(0)
        If CType(oCleComm, CLELBMENU).CreaArticoDaArtest(strDittaCorrente, NTSCStr(!ae_codartf), _
          NTSCStr(!ae_forn), NTSCStr(!ae_codmarc), "", strTmp) = False Then
          ThrowRemoteEvent(New NTSEventArgs("", strTmp))
          Return False
        End If
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

End Class
