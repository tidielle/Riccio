Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp

Public Class CLEVEPACK
  Inherits CLE__BASE

  Public oCldPack As CLDVEPACK
  Public bNoModifQuantSuColli As Boolean
  Public lNoModifQuantSuColli As Integer
  Public lNumNuovo As Integer = 0

  Public bInDuplica As Boolean = False
  Public lIncremContatoreRiga As Decimal = 0
  Public lNumPack As Integer = 0
  Public lAnnoPack As Integer = 0
  Public strSeriePack As String = ""
  Public dtDataPack As Date
  Public strTp_mmtipork As String = ""
  Public nTp_mmanno As Integer = 0
  Public strTp_mmserie As String = ""
  Public lTp_mmnumdoc As Integer = 0
  Public bLottoNew As Boolean = False     'se true (letto da anaditac) alo_lotto è calcolato sempre in automatico, se false alo_lotto è uguale a alo_lottox e alo_lottx deve essere numerico di max 9 char
  Public nTabling As Integer
  Public bNumChanged As Boolean = False

  Public strTipoRilevaMisure As String = ""
  Public bAggiungiRighe As Boolean
  Public dIndiceMemo As Decimal
  Public dMaxRiga As Decimal
  Public bInDividi As Boolean
  Public bDopoDividiMantieniSelezione As Boolean = False

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModVE
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDVEPACK"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldPack = CType(MyBase.ocldBase, CLDVEPACK)
    oCldPack.Init(oApp)

    Return True
  End Function

  Public Overridable Function TestPreSalvaTestPac(ByVal bNuovo As Boolean) As Boolean
    '-------------------------------------------------
    'eseguo i controlli necessari prima di fare il salvataggio della riga
    Dim i As Integer = 0
    Try
      If dsShared.Tables("MOVPAC").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128707820893809548, "Inserire almeno una riga nel corpo.")))
        Return False
      End If

      If Not bNuovo Then
        If NTSCStr(dsShared.Tables("TESTPAC").Rows(0)!tp_ultagg) <> NTSCStr(ControllaUltAgg(NTSCInt(dsShared.Tables("TESTPAC").Rows(0)!tp_anno), NTSCStr(dsShared.Tables("TESTPAC").Rows(0)!tp_serie), NTSCInt(dsShared.Tables("TESTPAC").Rows(0)!tp_numdoc))) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128708074611176055, "La Packing List che si cerca di salvare è stata modificata da un altro utente o sessione. E' possibile solo ripristinare.")))
          Return False
        End If
      End If

      If NTSCInt(dsShared.Tables("TESTPAC").Rows(0)!tp_anno) <> NTSCInt(NTSCDate(dsShared.Tables("TESTPAC").Rows(0)!tp_datdoc).Year) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128794399113589815, "La data documento non è compresa nell'anno indicato.")))
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
  Public Overridable Function SalvaTestPac(ByVal bDelete As Boolean, ByVal bNuovo As Boolean) As Boolean
    Dim strErr As String = ""
    Dim lNum As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalvaTestPac(bNuovo) Then Return False
      End If

      dsShared.Tables("TESTPAC").Rows(0)!tp_ultagg = Now
      dsShared.Tables("TESTPAC").Rows(0)!codditt = strDittaCorrente

      '----------------------------------------
      'chiamo il dal per salvare o cancellare la riga
      If bNuovo Then
        bNumChanged = False
        If lNumNuovo = NTSCInt(dsShared.Tables("TESTPAC").Rows(0)!tp_numdoc) Then
          lNumNuovo = oCldPack.AggNuma(strDittaCorrente, "V", NTSCStr(dsShared.Tables("TESTPAC").Rows(0)!tp_serie), NTSCInt(dsShared.Tables("TESTPAC").Rows(0)!tp_anno), NTSCInt(dsShared.Tables("TESTPAC").Rows(0)!tp_numdoc), True, True, strErr)
          If strErr <> "" Then
            ThrowRemoteEvent(New NTSEventArgs("", strErr))
            If lNumNuovo <> 0 Then
              bNumChanged = True
              dsShared.Tables("TESTPAC").Rows(0)!tp_numdoc = lNumNuovo

              For z As Integer = 0 To dsShared.Tables("MOVPAC").Rows.Count - 1
                dsShared.Tables("MOVPAC").Rows(z)!mp_numdoc = lNumNuovo
              Next
            Else
              Return False
            End If
          End If
        End If
        If Not oCldPack.ScriviSuTestPac(dsShared.Tables("TESTPAC"), "A") Then Return False
      Else
        If bDelete Then
          If Not oCldPack.ScriviSuTestPac(dsShared.Tables("TESTPAC"), "D") Then Return False
        Else
          If Not oCldPack.ScriviSuTestPac(dsShared.Tables("TESTPAC"), "M") Then Return False
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

  Public Overridable Function TestPreSalvaMovPac() As Boolean
    '-------------------------------------------------
    'eseguo i controlli necessari prima di fare il salvataggio della riga
    Dim dtrCurrRow As DataRow() = dsShared.Tables("MOVPAC").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim strTmp As String = ""
    Dim strCol As String()
    Try
      For i = 0 To dtrCurrRow.Length - 1
        If NTSCStr(dtrCurrRow(i)!mp_codart).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128707819115516917, "Il codice articolo è un campo obbligatorio.")))
          Return False
        End If

        If NTSCStr(dtrCurrRow(i)!mp_codart) <> NTSCStr(dtrCurrRow(i)!mp_codart).Trim Then
          dtrCurrRow(i)!mp_codart = NTSCStr(dtrCurrRow(i)!mp_codart).Trim
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
  Public Overridable Function SalvaMovpac(ByVal bDelete As Boolean) As Boolean
    Try
      If bInDuplica Then Return True
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalvaMovPac() Then Return False
      End If

      AggiornaData()

      '----------------------------------------
      'chiamo il dal per salvare
      If Not oCldPack.ScriviSuMovPac(strDittaCorrente, dsShared.Tables("MOVPAC")) Then Return False

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
  
  Public Overridable Function CreaMovPac(ByRef dttMovPac As DataTable) As Boolean
    Try
      Return oCldPack.LeggiTabellaMovPac(dttMovPac)
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
  Public Overridable Function ApriNuovoPac(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      If Not oCldPack.LeggiTabellaTestPac(ds) Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      ocldBase.SetTableDefaultValueFromDB("MOVPAC", ds)
      ocldBase.SetTableDefaultValueFromDB("TESTPAC", ds)

      SetDefaultValue(ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("MOVPAC").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("MOVPAC").ColumnChanged, AddressOf AfterColUpdate
      AddHandler dsShared.Tables("TESTPAC").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TESTPAC").ColumnChanged, AddressOf AfterColUpdate

      bHasChanges = False

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

  Public Overridable Function ApriPac(ByVal strDitta As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNum As Integer, _
                                      ByRef dsPack As DataSet) As Boolean
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      If Not oCldPack.CaricaPac(strDitta, lAnno, strSerie, lNum, dsPack) Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      ocldBase.SetTableDefaultValueFromDB("MOVPAC", dsPack)
      ocldBase.SetTableDefaultValueFromDB("TESTPAC", dsPack)

      SetDefaultValue(dsPack)

      If dsPack.Tables("TESTPAC").Rows.Count > 0 Then
        lNumPack = NTSCInt(dsPack.Tables("TESTPAC").Rows(0)!tp_numdoc)
        lAnnoPack = NTSCInt(dsPack.Tables("TESTPAC").Rows(0)!tp_anno)
        strSeriePack = NTSCStr(dsPack.Tables("TESTPAC").Rows(0)!tp_serie)
        dtDataPack = NTSCDate(dsPack.Tables("TESTPAC").Rows(0)!tp_datdoc)
        strTp_mmtipork = NTSCStr(dsPack.Tables("TESTPAC").Rows(0)!tp_mmtipork)
        nTp_mmanno = NTSCInt(dsPack.Tables("TESTPAC").Rows(0)!tp_mmanno)
        strTp_mmserie = NTSCStr(dsPack.Tables("TESTPAC").Rows(0)!tp_mmserie)
        lTp_mmnumdoc = NTSCInt(dsPack.Tables("TESTPAC").Rows(0)!tp_mmnumdoc)
      End If

      dsShared = dsPack

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("MOVPAC").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("MOVPAC").ColumnChanged, AddressOf AfterColUpdate
      AddHandler dsShared.Tables("TESTPAC").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TESTPAC").ColumnChanged, AddressOf AfterColUpdate

      bHasChanges = False

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

  Public Overridable Function CancellaPac(ByRef dttTestPac As DataTable) As Boolean
    Try
      Return oCldPack.ScriviSuTestPac(dttTestPac, "D")
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

  Public Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Dim dttTmp As New DataTable
    Try
      'Altrimenti non aggiorna i pesi reinserendo lo stesso articolo
      If e.Column.ColumnName = "mp_codart" Then
        oCldPack.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "ARTICO", "S", , dttTmp)

        If dttTmp.Rows.Count > 0 Then
          e.Row!mp_pesonetto = NTSCDec(dttTmp.Rows(0)!ar_pesonet) * NTSCDec(e.Row!mp_quant)
          e.Row!mp_peso = NTSCDec(dttTmp.Rows(0)!ar_pesolor) * NTSCDec(e.Row!mp_quant)
        End If
      End If


      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If

      '-------------------------------------------------------------
      'se non ho compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "mp_riga" And e.Column.ColumnName.Substring(0, 2).ToLower = "mp" Then
        If NTSCDec(e.Row!mp_riga) = 0 Then OnAddNewRow(sender, e)
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
  Public Overridable Sub BeforeColUpdate_mp_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttArtico, dttTmp As New DataTable
    Dim bRiportaNote As Boolean = True

    Try
      If NTSCStr(e.ProposedValue) <> NTSCStr(e.ProposedValue).ToUpper.Trim Then
        e.ProposedValue = NTSCStr(e.ProposedValue).ToUpper.Trim
      End If

      If NTSCStr(e.ProposedValue).Trim <> "" And oCldPack.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "ARTICO", "S", , dttArtico) Then

        If NTSCInt(dttArtico.Rows(0)!ar_codtagl) <> 0 And bInDividi = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130409190504038290, "Articolo taglia e colori non inseribile manualmente.")))
          Return
        End If

        bRiportaNote = CBool(IIf(NTSCDec(dttArtico.Rows(0)!ar_pesoca) = 0, True, False))
        With dttArtico.Rows(0)
          If nTabling = 0 Then
            e.Row!mp_descr = !ar_descr
            If bRiportaNote = True Then e.Row!mp_note = !ar_note
            e.Row!mp_desint = !ar_desint
          Else
            oCldPack.ArticoloInLingua(strDittaCorrente, NTSCStr(e.ProposedValue), nTabling, dttTmp)
            If dttTmp.Rows.Count > 0 Then
              e.Row!mp_descr = dttTmp.Rows(0)!ax_descr
              If bRiportaNote = True Then e.Row!mp_note = dttTmp.Rows(0)!ax_note
              e.Row!mp_desint = dttTmp.Rows(0)!ax_desint
            Else
              e.Row!mp_descr = !ar_descr
              If bRiportaNote = True Then e.Row!mp_note = !ar_note
              e.Row!mp_desint = !ar_desint
            End If
          End If
          e.Row!mp_ump = !ar_unmis
          e.Row!mp_unmis = !ar_unmis
          e.Row!mp_pesonetto = NTSCDec(!ar_pesonet) * NTSCDec(e.Row!mp_quant)
          e.Row!mp_peso = NTSCDec(!ar_pesolor) * NTSCDec(e.Row!mp_quant)
          '----------------------------------------------------------------------------------------------------------
          If strTipoRilevaMisure = "-1" Then
            e.Row!mp_misura1 = !ar_misura1
            e.Row!mp_misura2 = !ar_misura2
            e.Row!mp_misura3 = !ar_misura3
          End If
          '----------------------------------------------------------------------------------------------------------
        End With
      Else
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128707830558558902, "Codice articolo inesistente.")))
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
  Public Overridable Sub BeforeColUpdate_mp_colli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dQuantUMP As Decimal = 0
    Dim bDaMisurare As Boolean = False
    Dim strErr As String = ""
    Try
      If lNoModifQuantSuColli = 0 OrElse (lNoModifQuantSuColli = -1 AndAlso NTSCDec(e.Row!mp_quant) = 0) Then
        If NTSCStr(e.Row!mp_codart) = "M" Or NTSCStr(e.Row!mp_codart) = "D" Then
          e.Row!mp_quant = e.ProposedValue
        Else
          If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, NTSCStr(e.Row!mp_codart), NTSCStr(e.Row!mp_unmis), NTSCDec(e.ProposedValue), _
                                                     NTSCDec(e.Row!mp_misura1), NTSCDec(e.Row!mp_misura2), NTSCDec(e.Row!mp_misura3), dQuantUMP, bDaMisurare, strErr) Then
            e.Row!mp_quant = dQuantUMP
          Else
            e.Row!mp_quant = 0
          End If
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
  Public Overridable Sub BeforeColUpdate_mp_numpack(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.ProposedValue) = "" Then e.ProposedValue = " "
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
  Public Overridable Sub BeforeColUpdate_mp_quant(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttArtico As New DataTable
    Try
      If Not oCldPack.ValCodiceDb(NTSCStr(e.Row!mp_codart), strDittaCorrente, "ARTICO", "S", , dttArtico) Then Return

      If dttArtico.Rows.Count <> 0 Then
        e.Row!mp_pesonetto = NTSCDec(e.ProposedValue) * NTSCDec(dttArtico.Rows(0)!ar_pesonet)
        e.Row!mp_peso = NTSCDec(e.ProposedValue) * NTSCDec(dttArtico.Rows(0)!ar_pesolor)
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
  Public Overridable Sub BeforeColUpdate_mp_unmis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dQuantUMP As Decimal = 0
    Dim bDaMisure As Boolean = False
    Dim strErr As String = ""
    Try
      If NTSCStr(e.Row!mp_codart) <> "" Then
        If lNoModifQuantSuColli = 0 OrElse (lNoModifQuantSuColli = -1 AndAlso NTSCDec(e.Row!mp_quant) = 0) Then
          If NTSCStr(e.Row!mp_codart) = "D" Or NTSCStr(e.Row!mp_codart) = "M" Then
            e.Row!mp_quant = e.Row!mp_colli
            e.Row!mp_ump = e.Row!mp_unmis
          Else
            If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, NTSCStr(e.Row!mp_codart), NTSCStr(e.ProposedValue), NTSCDec(e.Row!mp_colli), _
                                                       NTSCDec(e.Row!mp_misura1), NTSCDec(e.Row!mp_misura2), NTSCDec(e.Row!mp_misura3), dQuantUMP, bDaMisure, strErr) Then
              e.Row!mp_quant = dQuantUMP
            Else
              e.Row!mp_quant = 0
            End If
          End If
        Else
          'Se anche l'opzione di non modificare le quantità in seguito alla modifica dei colli abilitata...
          'devo comunque valutare se il cambio della u.d.m. abilita la gestione dei prezzi riferiti ad una u.d.m. <> dalla principale...
          'perciò non toccherò le quantità...ma devo comunque fare delle cose
          If (NTSCStr(e.Row!mp_codart) = "D" Or NTSCStr(e.Row!mp_codart) = "M") Then
            e.Row!mp_ump = e.Row!mp_unmis
          End If
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
  Public Overridable Sub BeforeColUpdate_mp_misura1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dQuantUMP As Decimal = 0
    Dim bDaMisure As Boolean = False
    Dim strErr As String = ""
    Try
      If NTSCStr(e.Row!mp_codart) <> "D" And NTSCStr(e.Row!mp_codart) <> "M" Then
        If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, NTSCStr(e.Row!mp_codart), NTSCStr(e.Row!mp_unmis), NTSCDec(e.Row!mp_colli), _
                                      NTSCDec(e.Row!mp_misura1), NTSCDec(e.Row!mp_misura2), NTSCDec(e.Row!mp_misura3), dQuantUMP, bDaMisure, strErr) Then
          If bDaMisure Then e.Row!mp_quant = dQuantUMP
        Else
          e.Row!mp_quant = 0
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
  Public Overridable Sub BeforeColUpdate_mp_misura2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dQuantUMP As Decimal = 0
    Dim bDaMisure As Boolean = False
    Dim strErr As String = ""
    Try
      If NTSCStr(e.Row!mp_codart) <> "D" And NTSCStr(e.Row!mp_codart) <> "M" Then
        If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, NTSCStr(e.Row!mp_codart), NTSCStr(e.Row!mp_unmis), NTSCDec(e.Row!mp_colli), _
                                      NTSCDec(e.Row!mp_misura1), NTSCDec(e.Row!mp_misura2), NTSCDec(e.Row!mp_misura3), dQuantUMP, bDaMisure, strErr) Then
          If bDaMisure Then e.Row!mp_quant = dQuantUMP
        Else
          e.Row!mp_quant = 0
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
  Public Overridable Sub BeforeColUpdate_mp_misura3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dQuantUMP As Decimal = 0
    Dim bDaMisure As Boolean = False
    Dim strErr As String = ""
    Try
      If NTSCStr(e.Row!mp_codart) <> "D" And NTSCStr(e.Row!mp_codart) <> "M" Then
        If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, NTSCStr(e.Row!mp_codart), NTSCStr(e.Row!mp_unmis), NTSCDec(e.Row!mp_colli), _
                                      NTSCDec(e.Row!mp_misura1), NTSCDec(e.Row!mp_misura2), NTSCDec(e.Row!mp_misura3), dQuantUMP, bDaMisure, strErr) Then
          If bDaMisure Then e.Row!mp_quant = dQuantUMP
        Else
          e.Row!mp_quant = 0
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
  Public Overridable Sub BeforeColUpdate_mp_code(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If NTSCStr(e.Row!mp_codart) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128709536952399619, "Inserire l'articolo prima di indicare il codice a barre relativo.")))
        Return
      End If

      If NTSCStr(e.ProposedValue) <> NTSCStr(e.ProposedValue).ToUpper Then
        e.ProposedValue = NTSCStr(e.ProposedValue).ToUpper
      End If

      If NTSCStr(e.Row!mp_codart).Trim = "" Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129435287335583155, "Inserire l'articolo prima di indicare il codice a barre relativo.")))
        Return
      End If

      If oCldPack.LeggiBarcode(strDittaCorrente, NTSCStr(e.Row!mp_codart), NTSCStr(e.ProposedValue), dttTmp) Then
        If dttTmp.Rows.Count = 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128709547206068640, "Codice a barre non valido per l'articolo '|" & NTSCStr(e.Row!mp_codart) & "|'.")))
        End If
      Else
        e.ProposedValue = e.Row(e.Column.ColumnName)
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
  'Public Overridable Sub BeforeColUpdate_mp_lotto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
  '  Dim dttTmp As New DataTable
  '  Try
  '    If NTSCStr(e.Row!mp_codart).Trim = "" Then
  '      'prima di indicare il lotto devo indicare l'articolo 
  '      e.ProposedValue = 0
  '      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513888485432635, "Prima di indicare il lotto inserire il cod. articolo")))
  '      Return
  '    End If

  '    If oCldPack.ValCodiceDb(NTSCStr(e.Row!mp_codart), strDittaCorrente, "ANALOTTI", "N", , dttTmp, NTSCStr(e.ProposedValue)) Then
  '      If dttTmp.Rows.Count <> 0 Then
  '        e.Row!mp_datascad = dttTmp.Rows(0)!alo_dtscad
  '      End If
  '    Else
  '      e.ProposedValue = e.Row(e.Column.ColumnName)
  '    End If

  '  Catch ex As Exception
  '    '--------------------------------------------------------------
  '    If GestErrorCallThrow() Then
  '      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
  '    Else
  '      ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
  '    End If
  '    '--------------------------------------------------------------
  '  Finally
  '    dttTmp.Clear()
  '  End Try
  'End Sub
  Public Overridable Sub BeforeColUpdate_xx_lottox(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.Row!mp_codart).Trim = "" Then
        'prima di indicare il lotto devo indicare l'articolo 
        e.ProposedValue = ""
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513889258401769, "Prima di indicare il lotto inserire il cod. articolo")))
        Return
      End If

      If NTSCStr(e.ProposedValue).Trim = "" And NTSCStr(e.ProposedValue) <> NTSCStr(e.ProposedValue).Trim Then
        e.ProposedValue = ""
        Return
      End If

      If bLottoNew = False And NTSCStr(e.ProposedValue) <> "" Then
        'vecchia gestione lotti: il lotto è solo numerico di max 9 char: lo formatto
        If NTSCStr(e.ProposedValue) <> "" Then
          If Not IsNumeric(e.ProposedValue) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513977264885199, "Il codice lotto deve essere un numero compreso tra 0 e 999999999")))
            e.ProposedValue = ""
            Return
          End If
        End If

        e.ProposedValue = NTSCInt(e.ProposedValue).ToString("000000000")

        If NTSCInt(e.ProposedValue) = 0 Then e.ProposedValue = ""
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

  Public Overridable Sub OnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dRiga As Decimal = 1
    Dim dtrT() As DataRow = Nothing
    Try
      If bInDuplica Then Return

      dtrT = dsShared.Tables("MOVPAC").Select("", "mp_riga DESC")
      If dtrT.Length > 0 Then dRiga = NTSCDec(dtrT(0)!mp_riga) + lIncremContatoreRiga

      e.Row!mp_riga = dRiga
      e.Row!codditt = strDittaCorrente
      e.Row!mp_mmanno = nTp_mmanno
      e.Row!mp_mmserie = IIf(strTp_mmserie = "", " ", strTp_mmserie)
      e.Row!mp_mmnumdoc = lTp_mmnumdoc
      e.Row!mp_mmtipork = strTp_mmtipork
      e.Row!mp_anno = lAnnoPack
      e.Row!mp_numdoc = lNumPack
      e.Row!mp_serie = strSeriePack
      e.Row!mp_datscad = DBNull.Value
      e.Row!xx_selez = "N"

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
  'Public Overridable Sub AfterColUpdate_mp_lotto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
  '  'sono stato chiamato da bnmgalo e mi è stato passato il lotto numerico: compilo la colonna con il lotto alfanumerico
  '  Dim dttTmp As New DataTable
  '  Try
  '    If NTSCInt(e.ProposedValue.ToString) = 0 Then
  '      e.Row!xx_lottox = ""
  '      Return
  '    End If

  '    oCldPack.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANALOTTI", "S", "", dttTmp, NTSCStr(e.Row!mp_codart))
  '    If dttTmp.Rows.Count = 0 Then
  '      'non posso creare l'anagrafica lotti, visto che non saprei che alo_lottox memorizzare !!!!
  '      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513219111285131, "Codice lotto inesistente")))
  '      e.Row!mp_lotto = 0
  '      Return
  '    End If

  '    If NTSCStr(e.Row!xx_lottox) <> NTSCStr(dttTmp.Rows(0)!alo_lottox) Then e.Row!xx_lottox = NTSCStr(dttTmp.Rows(0)!alo_lottox)

  '  Catch ex As Exception
  '    '--------------------------------------------------------------
  '    If GestErrorCallThrow() Then
  '      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
  '    Else
  '      ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
  '    End If
  '    '--------------------------------------------------------------
  '  Finally
  '    dttTmp.Clear()
  '  End Try
  'End Sub
  Public Overridable Sub AfterColUpdate_xx_lottox(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Dim lIdLotto As Integer = 0
    Dim strErr As String = ""

    Try
      'dato il lotto alfanumerico devo tradure in ID lotto numerico
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!mp_lotto = 0
        Return
      End If

      'dato il lotto alfanumerico ottengo l'ID numerico
      oCldPack.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANALOTTIX", "S", "", dttTmp, NTSCStr(e.Row!mp_codart))
      If dttTmp.Rows.Count = 0 Then
        'creo l'anagrafica lotti
        If Not CType(oCleComm, CLELBMENU).CreaAnalottiDaLottox(strDittaCorrente, NTSCStr(e.Row!mp_codart), _
                                          NTSCDate(dsShared.Tables("TESTPAC").Rows(0)!tp_datdoc).ToShortDateString, _
                                          NTSCStr(e.ProposedValue), "", lIdLotto, strErr) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513215192910290, "Codice lotto inesistente") & vbCrLf & strErr))
          e.Row!xx_lottox = ""
          Return
        Else
          If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
        End If
      Else
        lIdLotto = NTSCInt(dttTmp.Rows(0)!alo_lotto)
      End If

      If NTSCInt(e.Row!mp_lotto) <> lIdLotto Then e.Row!mp_lotto = lIdLotto

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
  End Sub

  Public Overridable Sub AggiornaData()
    ' Modifica la data dell'ultimo aggiornamento
    Dim i As Integer
    Dim dtrCurrRow As DataRow() = dsShared.Tables("MOVPAC").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
    Try
      For i = 0 To dtrCurrRow.Length - 1
        dtrCurrRow(i)!mp_ultagg = Now
        dtrCurrRow(i)!codditt = strDittaCorrente
      Next
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

  Public Overridable Function LeggiDescrizioneConto(ByVal lNumConto As Integer, ByRef strDesc As String) As Boolean
    Dim dttOut As New DataTable
    Try
      oCldPack.LeggiDescrizioneConto(strDittaCorrente, lNumConto, dttOut)

      strDesc = NTSCStr(dttOut.Rows(0)!an_descr1)
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

    Public Overridable Function CaricaDaDocu(ByVal strTipork As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumero As Integer, _
                                             ByVal lOrdinamentoNuoveRighe As Integer, ByVal lIncremContatoreRiga As Decimal, _
                                             ByVal bRilevaDaArtico As Boolean, ByRef dttDocu As DataTable, ByVal lAnnoPack As Integer, ByVal strSeriePack As String, _
                                             ByVal lNumPack As Integer, ByRef dttMovPac As DataTable) As Boolean
        Dim dttTmp As New DataTable
        Dim dttArti As New DataTable
        Dim lIndice As Decimal = 0
        Dim i As Integer
        Dim x As Integer
        Try
            If bAggiungiRighe Then lIndice = dIndiceMemo

            If Not oCldPack.SelezionaDocumento(strDittaCorrente, strTipork, lAnno, strSerie, lNumero, dttDocu) Then Return False

            If dttDocu.Rows.Count = 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698288498276768, "Documento di magazzino inesistente.")))
                Return False
            End If

            If lOrdinamentoNuoveRighe = 1 Then
                For x = 1 To 2
                    If x = 1 Then
                        oCldPack.SelezionaMovmagJoinArtico(strDittaCorrente, strTipork, lAnno, strSerie, lNumero, 0, dttTmp)
                    Else
                        oCldPack.SelezionaMovmagJoinMovord(strDittaCorrente, strTipork, lAnno, strSerie, lNumero, 0, dttTmp)
                    End If


                    For i = 0 To dttTmp.Rows.Count - 1
                        lIndice = lIndice + lIncremContatoreRiga

                        oCldPack.ValCodiceDb(NTSCStr(dttTmp.Rows(i)!mm_codart), strDittaCorrente, "ARTICO", "S", , dttArti)

                        If Not InserisciDaDocu(dttTmp, dttArti, i, lAnnoPack, strSeriePack, lNumPack, lIndice, bRilevaDaArtico, dttMovPac) Then Return False

                        dIndiceMemo = lIndice
                    Next
                Next
            Else
                If Not oCldPack.SelezionaMovmag(strDittaCorrente, strTipork, lAnno, strSerie, lNumero, 0, dttTmp) Then Return False

                For i = 0 To dttTmp.Rows.Count - 1
                    lIndice = lIndice + lIncremContatoreRiga

                    oCldPack.ValCodiceDb(NTSCStr(dttTmp.Rows(i)!mm_codart), strDittaCorrente, "ARTICO", "S", , dttArti)

                    If Not InserisciDaDocu(dttTmp, dttArti, i, lAnnoPack, strSeriePack, lNumPack, lIndice, bRilevaDaArtico, dttMovPac) Then Return False

                    dIndiceMemo = lIndice
                Next
            End If

            AttribuisciTaglia(dttTmp, dttMovPac, True)

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
  Public Overridable Function CaricaDaOrdine(ByVal strTipo As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                             ByVal bRileva As Boolean, ByVal bOrdMatrMultiRow As Boolean, ByVal lIncremContatoreRiga As Decimal, _
                                             ByVal lAnnoPack As Integer, ByVal strSeriePack As String, ByVal lNumPack As Integer, _
                                             ByRef dttMovPac As DataTable, ByRef dttOrd As DataTable) As Boolean
    Dim dttTmp As New DataTable
    Dim dttArti As New DataTable
    Dim lIndice As Decimal = 0
    Dim i As Integer
    Dim x As Integer
    Try
      If bAggiungiRighe Then lIndice = dIndiceMemo

      If Not oCldPack.SelezionaOrdine(strDittaCorrente, strTipo, lAnno, strSerie, lNumdoc, dttOrd) Then Return False

      If dttOrd.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698581254343225, "Ordine inesistente.")))
        Return False
      End If

      If Not oCldPack.SelezionaMovord(strDittaCorrente, strTipo, lAnno, strSerie, lNumdoc, 0, dttTmp) Then Return False

      For i = 0 To dttTmp.Rows.Count - 1
        oCldPack.ValCodiceDb(NTSCStr(dttTmp.Rows(i)!mo_codart), strDittaCorrente, "ARTICO", "S", , dttArti)

        If NTSCStr(dttArti.Rows(0)!ar_gestmatr) = "S" And bOrdMatrMultiRow And NTSCInt(dttTmp.Rows(i)!mo_quant) > 0 Then
          For x = 1 To NTSCInt(dttTmp.Rows(i)!mo_quant)
            lIndice = lIndice + lIncremContatoreRiga
            If Not InserisciDaOrd(dttTmp, dttArti, i, lAnnoPack, strSeriePack, lNumPack, lIndice, bRileva, dttMovPac, True) Then Return False

            dIndiceMemo = lIndice
          Next
        Else
          lIndice = lIndice + lIncremContatoreRiga
          If Not InserisciDaOrd(dttTmp, dttArti, i, lAnnoPack, strSeriePack, lNumPack, lIndice, bRileva, dttMovPac, False) Then Return False

          dIndiceMemo = lIndice
        End If
      Next
      AttribuisciTaglia(dttTmp, dttMovPac, False)

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
  Public Overridable Function CaricaDaPacking(ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal bEredita As Boolean, _
                                              ByVal lIncremContatoreRiga As Decimal, ByVal bRileva As Boolean, ByRef dttPack As DataTable, _
                                              ByVal lAnnoPack As Integer, ByVal strSeriePack As String, ByVal lNumPack As Integer, _
                                              ByRef dttMovpac As DataTable) As Boolean
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Dim lIndice As Decimal = 0
    Dim i As Integer

    Try
      If bAggiungiRighe Then lIndice = dIndiceMemo

      If Not oCldPack.SelezionaPacking(strDittaCorrente, lAnno, strSerie, lNumdoc, dttPack) Then Return False

      If dttPack.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128698581136632006, "Packing list inesistente.")))
        Return False
      End If

      If Not oCldPack.SelezionaMovpac(strDittaCorrente, lAnno, strSerie, lNumdoc, 0, dttTmp) Then Return False

      For i = 0 To dttTmp.Rows.Count - 1
        lIndice = lIndice + lIncremContatoreRiga
        If Not InserisciDaPack(dttTmp, bEredita, i, lAnnoPack, strSeriePack, lNumPack, lIndice, bRileva, dttMovpac) Then Return False

        dIndiceMemo = lIndice
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

  Public Overridable Function LeggiDescr(ByVal strCodice As String, ByVal strTabella As String, ByRef strDesc As String, Optional ByVal strCod2 As String = "") As Boolean
    Try
      Return oCldPack.ValCodiceDb(strCodice, strDittaCorrente, strTabella, "N", strDesc, , strCod2)
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
  Public Overridable Function ComponiDescrizione(ByVal lConto As Integer, ByRef strDescr As String, ByRef lCodling As Integer) As Boolean
    Dim dttTmp As New DataTable
    Try
      If lConto = 0 Then Return True

      If Not oCldPack.LeggiDatiConto(strDittaCorrente, lConto, dttTmp) Then Return False

      If dttTmp.Rows.Count <> 0 Then
        With dttTmp.Rows(0)
          If ntscstr(!an_tipo) = "S" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128699177178907058, "Il codice inserito appartiene ai sottoconti, impossibile continuare.")))
            Return False
          End If

          strDescr = " "
          If NTSCStr(!an_descr1) <> "" Then strDescr &= NTSCStr(!an_descr1)
          If NTSCStr(!an_descr2) <> "" Then strDescr &= vbCrLf & " " & NTSCStr(!an_descr2)
          If NTSCStr(!an_indir) <> "" Then strDescr &= vbCrLf & " " & NTSCStr(!an_indir)
          If NTSCStr(!an_cap) <> "" Then strDescr &= vbCrLf & " " & NTSCStr(!an_cap)
          If NTSCStr(!an_citta) <> "" Then strDescr &= "  " & NTSCStr(!an_citta)
          If NTSCStr(!an_prov) <> "" Then strDescr &= "  (" & NTSCStr(!an_prov) & ")"
          lCodling = NTSCInt(!an_codling)
          nTabling = lCodling
        End With
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128699177178907059, "Il codice del Cliente o Fornitore inserito non è valido.")))
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

    Public Overridable Function InserisciDaDocu(ByVal dttTmp As DataTable, ByVal dttArti As DataTable, ByVal i As Integer, ByVal lAnno As Integer, _
                                                ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lIndice As Decimal, ByVal bRilevaDaArtico As Boolean, _
                                                ByRef dttMovPac As DataTable) As Boolean
        Try
            dttMovPac.Rows.Add()

            With dttMovPac.Rows(dttMovPac.Rows.Count - 1)
                If dttTmp.Rows(i)!mma_rigaa.ToString.Trim() = "" Then
                    '--- La riga NON ha matricole
                    !codditt = strDittaCorrente
                    !mp_anno = lAnno
                    !mp_serie = strSerie
                    !mp_numdoc = lNumdoc
                    !mp_riga = lIndice
                    !mp_mmtipork = dttTmp.Rows(i)!mm_tipork
                    !mp_mmanno = dttTmp.Rows(i)!mm_anno
                    !mp_mmserie = dttTmp.Rows(i)!mm_serie
                    !mp_mmnumdoc = dttTmp.Rows(i)!mm_numdoc
                    !mp_mmriga = dttTmp.Rows(i)!mm_riga
                    !mp_codart = dttTmp.Rows(i)!mm_codart
                    !mp_unmis = dttTmp.Rows(i)!mm_unmis
                    !mp_descr = dttTmp.Rows(i)!mm_descr
                    !mp_colli = dttTmp.Rows(i)!mm_colli
                    !mp_quant = dttTmp.Rows(i)!mm_quant
                    !mp_peso = NTSCDec(dttArti.Rows(0)!ar_pesolor) * NTSCDec(dttTmp.Rows(i)!mm_quant)
                    !mp_pesonetto = NTSCDec(dttArti.Rows(0)!ar_pesonet) * NTSCDec(dttTmp.Rows(i)!mm_quant)

                    !mp_note = dttTmp.Rows(i)!mm_note
                    !mp_desint = dttTmp.Rows(i)!mm_desint
                    !mp_ump = dttTmp.Rows(i)!mm_ump
                    !mp_matric = " "
                    !mp_lotto = dttTmp.Rows(i)!mm_lotto
                    !xx_lottox = dttTmp.Rows(i)!xx_lottox

                    RilevaMisureVolume(dttMovPac.Rows(dttMovPac.Rows.Count - 1), dttArti.Rows(0), dttTmp.Rows(i), "mm_", strTipoRilevaMisure)

                    !mp_code = " "
                Else
                    !codditt = strDittaCorrente
                    !mp_anno = lAnno
                    !mp_serie = strSerie
                    !mp_numdoc = lNumdoc
                    !mp_riga = lIndice
                    !mp_mmtipork = dttTmp.Rows(i)!mm_tipork
                    !mp_mmanno = dttTmp.Rows(i)!mm_anno
                    !mp_mmserie = dttTmp.Rows(i)!mm_serie
                    !mp_mmnumdoc = dttTmp.Rows(i)!mm_numdoc
                    !mp_mmriga = dttTmp.Rows(i)!mm_riga
                    !mp_codart = dttTmp.Rows(i)!mm_codart
                    !mp_unmis = dttTmp.Rows(i)!mm_ump
                    !mp_descr = dttTmp.Rows(i)!mm_descr
                    !mp_colli = dttTmp.Rows(i)!mma_quant
                    !mp_quant = dttTmp.Rows(i)!mma_quant
                    !mp_peso = NTSCDec(dttArti.Rows(0)!ar_pesolor) * NTSCDec(dttTmp.Rows(i)!mma_quant)
                    !mp_pesonetto = NTSCDec(dttArti.Rows(0)!ar_pesonet) * NTSCDec(dttTmp.Rows(i)!mma_quant)

                    !mp_note = dttTmp.Rows(i)!mma_note
                    !mp_desint = dttTmp.Rows(i)!mm_desint
                    !mp_ump = dttTmp.Rows(i)!mm_ump
                    !mp_matric = dttTmp.Rows(i)!mma_matric
                    !mp_lotto = dttTmp.Rows(i)!mm_lotto

                    RilevaMisureVolume(dttMovPac.Rows(dttMovPac.Rows.Count - 1), dttArti.Rows(0), dttTmp.Rows(i), "mm_", strTipoRilevaMisure)

                    !mp_code = " "
                End If
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
  Public Overridable Function InserisciDaOrd(ByVal dttTmp As DataTable, ByVal dttArti As DataTable, ByVal i As Integer, ByVal lAnno As Integer, _
                                             ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lIndice As Decimal, ByVal bRileva As Boolean, _
                                             ByRef dttMovPac As DataTable) As Boolean
    Try
      Return InserisciDaOrd(dttTmp, dttArti, i, lAnno, strSerie, lNumdoc, lIndice, bRileva, dttMovPac, False)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function InserisciDaOrd(ByVal dttTmp As DataTable, ByVal dttArti As DataTable, ByVal i As Integer, ByVal lAnno As Integer, _
                                             ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lIndice As Decimal, ByVal bRileva As Boolean, _
                                             ByRef dttMovPac As DataTable, ByVal bColliQuant1 As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttTmp, dttArti, i, lAnno, strSerie, lNumdoc, lIndice, bRileva, dttMovPac, bColliQuant1})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttMovPac = CType(oIn(8), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      dttMovPac.Rows.Add()

      With dttMovPac.Rows(dttMovPac.Rows.Count - 1)
        !codditt = strDittaCorrente
        !mp_anno = lAnno
        !mp_serie = strSerie
        !mp_numdoc = lNumdoc
        !mp_riga = lIndice
        !mp_mmtipork = dttTmp.Rows(i)!mo_tipork
        !mp_mmanno = dttTmp.Rows(i)!mo_anno
        !mp_mmserie = dttTmp.Rows(i)!mo_serie
        !mp_mmnumdoc = dttTmp.Rows(i)!mo_numord
        !mp_mmriga = dttTmp.Rows(i)!mo_riga
        !mp_codart = dttTmp.Rows(i)!mo_codart
        !mp_unmis = dttTmp.Rows(i)!mo_unmis
        !mp_descr = dttTmp.Rows(i)!mo_descr
        !mp_colli = IIf(bColliQuant1 = False, dttTmp.Rows(i)!mo_colli, 1)
        !mp_quant = IIf(bColliQuant1 = False, dttTmp.Rows(i)!mo_quant, 1)
        !mp_peso = NTSCDec(dttArti.Rows(0)!ar_pesolor) * NTSCDec(dttTmp.Rows(i)!mo_quant)
        !mp_pesonetto = NTSCDec(dttArti.Rows(0)!ar_pesonet) * NTSCDec(dttTmp.Rows(i)!mo_quant)
        !mp_note = dttTmp.Rows(i)!mo_note
        !mp_desint = dttTmp.Rows(i)!mo_desint
        !mp_ump = dttTmp.Rows(i)!mo_ump
        !mp_matric = " "
        !mp_lotto = dttTmp.Rows(i)!mo_lotto
        !xx_lottox = dttTmp.Rows(i)!xx_lottox

       RilevaMisureVolume(dttMovPac.Rows(dttMovPac.Rows.Count - 1), dttArti.Rows(0), dttTmp.Rows(i), "mo_", strTipoRilevaMisure)

        !mp_code = " "
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
  Public Overridable Function InserisciDaPack(ByVal dttPack As DataTable, ByVal bEredita As Boolean, ByVal i As Integer, ByVal lAnno As Integer, _
                                              ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lIndice As Decimal, ByVal bRileva As Boolean, _
                                              ByRef dttMovPac As DataTable) As Boolean
    Dim dttArti As New DataTable
    Try

      dttMovPac.Rows.Add()

      With dttMovPac.Rows(dttMovPac.Rows.Count - 1)
        !codditt = strDittaCorrente
        !mp_anno = lAnno
        !mp_serie = strSerie
        !mp_numdoc = lNumdoc
        !mp_riga = lIndice
        !mp_mmtipork = dttPack.Rows(i)!mp_mmtipork
        !mp_mmanno = dttPack.Rows(i)!mp_mmanno
        !mp_mmserie = dttPack.Rows(i)!mp_mmserie
        !mp_mmnumdoc = dttPack.Rows(i)!mp_mmnumdoc
        !mp_mmriga = dttPack.Rows(i)!mp_mmriga
        !mp_codart = dttPack.Rows(i)!mp_codart
        !mp_unmis = dttPack.Rows(i)!mp_unmis
        !mp_descr = dttPack.Rows(i)!mp_descr
        !mp_colli = dttPack.Rows(i)!mp_colli
        !mp_quant = dttPack.Rows(i)!mp_quant
        !mp_peso = NTSCDec(dttPack.Rows(i)!mp_peso)
        !mp_pesonetto = NTSCDec(dttPack.Rows(i)!mp_pesonetto)
        !mp_note = dttPack.Rows(i)!mp_note
        !mp_desint = dttPack.Rows(i)!mp_desint
        !mp_ump = dttPack.Rows(i)!mp_ump
        !mp_matric = dttPack.Rows(i)!mp_matric
        !mp_lotto = dttPack.Rows(i)!mp_lotto
        !xx_lottox = dttPack.Rows(i)!xx_lottox

        oCldPack.ValCodiceDb(NTSCStr(dttPack.Rows(i)!mp_codart), strDittaCorrente, "ARTICO", "S", "", dttArti)
        RilevaMisureVolume(dttMovPac.Rows(dttMovPac.Rows.Count - 1), dttArti.Rows(0), dttPack.Rows(i), "mp_", strTipoRilevaMisure)

        !mp_code = dttPack.Rows(i)!mp_code
        !mp_datscad = dttPack.Rows(i)!mp_datscad

        If bEredita Then
          !mp_numpack = dttPack.Rows(i)!mp_numpack
          !mp_despack = dttPack.Rows(i)!mp_despack
        Else
          !mp_numpack = " "
          !mp_despack = " "
        End If

        !mp_tctaglia = dttPack.Rows(i)!mp_tctaglia
        !mp_indtagl = dttPack.Rows(i)!mp_indtagl
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

  Public Overridable Sub RilevaMisureVolume(ByVal dtrRow As DataRow, ByVal dtrArti As DataRow, ByVal dtrDoc As DataRow, ByVal strPrefix As String, _
                                            ByVal strTipoRileva As String)
    Try
      With dtrRow
            Select Case strTipoRileva
          Case "-1"
            !mp_misura1 = dtrArti!ar_misura1
            !mp_misura2 = dtrArti!ar_misura2
            !mp_misura3 = dtrArti!ar_misura3
            !mp_volume = dtrArti!ar_volume
          Case "1"
            !mp_misura1 = dtrDoc(strPrefix & "misura1")
            !mp_misura2 = dtrDoc(strPrefix & "misura2")
            !mp_misura3 = dtrDoc(strPrefix & "misura3")
            !mp_volume = 0
          Case "2"
            !mp_misura1 = 0
            !mp_misura2 = 0
            !mp_misura3 = 0
            !mp_volume = 0
          Case Else
            !mp_misura1 = 1
            !mp_misura2 = 1
            !mp_misura3 = 1
            !mp_volume = 0
        End Select
      End With

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

  Public Overridable Function CaricaUnMis() As DataTable
    '----------------------------
    'ottengo l'elenco delle unità di misura utilizzate in artico
    Dim dttTmp As New DataTable
    Try
      oCldPack.CaricaUmMis(strDittaCorrente, dttTmp)
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

  Public Overridable Function ControllaUltAgg(ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNum As Integer) As Date
    Dim dttTmp As New DataTable
    Try
      If Not oCldPack.LeggiUltAgg(strDittaCorrente, lAnno, strSerie, lNum, dttTmp) Then Return NTSCDate(IntSetDate("01/01/1900"))

      If dttTmp.Rows.Count <> 0 Then
        Return NTSCDate(dttTmp.Rows(0)!tp_ultagg)
      Else
        Return NTSCDate(IntSetDate("01/01/1900"))
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

  Public Overridable Sub Dividi()
    Dim i As Integer = 0
    Dim x As Integer = 0
    Dim dRiga As Decimal = 0
    Dim dQuant As Decimal = 0
    Dim dQuant2 As Decimal = 0
    Dim dColli As Decimal = 0
    Dim dtrRow(), dtrSel() As DataRow
    Dim dttTmp As New DataTable
    Dim dVol1 As Decimal = 0
    Dim dVol2 As Decimal = 0
    Dim dPeso As Decimal = 0
    Dim dPesoNetto As Decimal = 0
    Dim dPeso2 As Decimal = 0
    Dim dPesoNetto2 As Decimal = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttTmp = dsShared.Tables("MOVPAC").Clone
      '--------------------------------------------------------------------------------------------------------------
      dtrSel = dsShared.Tables("MOVPAC").Select("xx_selez = 'S'")
      '--------------------------------------------------------------------------------------------------------------
      If dtrSel.Length > 0 Then
        For i = 0 To dtrSel.Length - 1
          With dtrSel(i)
            '--------------------------------------------------------------------------------------------------------
            dtrRow = dsShared.Tables("MOVPAC").Select("mp_riga < " & CDblSQL(NTSCDec(!mp_riga)), "mp_riga DESC")
            '--------------------------------------------------------------------------------------------------------
            If dtrRow.Length = 0 Then
              dRiga = ArrDbl(NTSCDec(!mp_riga) / 2, 2)
            Else
              dRiga = ArrDbl((NTSCDec(!mp_riga) + NTSCDec(dtrRow(0)!mp_riga)) / 2, 2)
            End If

            '--------------------------------------------------------------------------------------------------------
            dtrRow = dsShared.Tables("MOVPAC").Select("mp_riga = " & CDblSQL(dRiga))
            If dtrRow.Length <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711247503675037, "Non si possono inserire più righe in questo punto")))
              Return
            End If
            '--------------------------------------------------------------------------------------------------------
            Dim msg As New NTSEventArgs(CLN__STD.ThMsg.INPUTBOX, oApp.Tr(Me, 128711309870251230, "Inserire la quantità da estrarre dall'articolo '|" & NTSCStr(!mp_codart) & "|'"))
            ThrowRemoteEvent(msg)
            dQuant = NTSCDec(msg.RetValue)
            If dQuant = 0 Then Continue For

            If dQuant < NTSCDec(!mp_quant) Then
              '------------------------------------------------------------------------------------------------------
              '--- Ricalcolo il nuovo volume
              '------------------------------------------------------------------------------------------------------
              dVol1 = (dQuant * NTSCDec(!mp_volume)) / NTSCDec(!mp_quant)
              dVol2 = NTSCDec(!mp_volume) - dVol1
              dPeso = (dQuant * NTSCDec(!mp_peso)) / NTSCDec(!mp_quant)
              dPesoNetto = (dQuant * NTSCDec(!mp_pesonetto)) / NTSCDec(!mp_quant)
              dPeso2 = NTSCDec(!mp_peso) - dPeso
              dPesoNetto2 = NTSCDec(!mp_pesonetto) - dPesoNetto
              dColli = dQuant * NTSCDec(!mp_colli) / NTSCDec(!mp_quant)
              !mp_volume = dVol2
              dQuant2 = NTSCDec(!mp_quant) - dQuant
              !mp_colli = NTSCDec(!mp_colli) - dColli
              !mp_quant = dQuant2
              !mp_peso = dPeso2
              !mp_pesonetto = dPesoNetto2
              dttTmp.ImportRow(dtrSel(i))
              dttTmp.Rows(dttTmp.Rows.Count - 1)!xx_selez = "N"
              dttTmp.Rows(dttTmp.Rows.Count - 1)!mp_colli = dColli
              dttTmp.Rows(dttTmp.Rows.Count - 1)!mp_quant = dQuant
              dttTmp.Rows(dttTmp.Rows.Count - 1)!mp_riga = dRiga
              dttTmp.Rows(dttTmp.Rows.Count - 1)!mp_volume = dVol1
              dttTmp.Rows(dttTmp.Rows.Count - 1)!mp_peso = dPeso
              dttTmp.Rows(dttTmp.Rows.Count - 1)!mp_pesonetto = dPesoNetto
            Else
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711328690746836, "La quantità da estrarre deve essere minore della totale. " & vbCrLf & _
                                                                                    "L'articolo |" & NTSCStr(!mp_codart) & "| non verrà diviso.")))
            End If
          End With
        Next
        '------------------------------------------------------------------------------------------------------------
        RemoveHandler dsShared.Tables("MOVPAC").ColumnChanging, AddressOf BeforeColUpdate
        RemoveHandler dsShared.Tables("MOVPAC").ColumnChanged, AddressOf AfterColUpdate
        For i = 0 To dttTmp.Rows.Count - 1
          For x = 0 To dsShared.Tables("MOVPAC").Rows.Count - 1
            If NTSCDec(dttTmp.Rows(i)!mp_riga) < NTSCDec(dsShared.Tables("MOVPAC").Rows(x)!mp_riga) Then
              bInDuplica = True
              dsShared.Tables("MOVPAC").Rows.Add(dsShared.Tables("MOVPAC").NewRow())
              For k As Integer = dsShared.Tables("MOVPAC").Rows.Count - 1 To x + 1 Step -1
                dsShared.Tables("MOVPAC").Rows(k).ItemArray = dsShared.Tables("MOVPAC").Rows(k - 1).ItemArray
                dsShared.Tables("MOVPAC").Rows(k).AcceptChanges()
                Select Case dsShared.Tables("MOVPAC").Rows(k - 1).RowState
                  Case DataRowState.Added : dsShared.Tables("MOVPAC").Rows(k).SetAdded()
                  Case DataRowState.Modified : dsShared.Tables("MOVPAC").Rows(k).SetModified()
                End Select
              Next
              dsShared.Tables("MOVPAC").Rows(x).ItemArray = dttTmp.Rows(i).ItemArray
              dsShared.Tables("MOVPAC").Rows(x).AcceptChanges()
              dsShared.Tables("MOVPAC").Rows(x).SetAdded()
              bInDuplica = False
              Exit For
            End If
          Next
        Next
        AddHandler dsShared.Tables("MOVPAC").ColumnChanging, AddressOf BeforeColUpdate
        AddHandler dsShared.Tables("MOVPAC").ColumnChanged, AddressOf AfterColUpdate
        '------------------------------------------------------------------------------------------------------------
        If Not bDopoDividiMantieniSelezione Then 'Tramite opzione di registro valuta se mantenere la selezione o no.
          For i = 0 To dsShared.Tables("MOVPAC").Rows.Count - 1
            dsShared.Tables("MOVPAC").Rows(i)!xx_selez = "N"
          Next
        End If
        'dsShared.Tables("MOVPAC").AcceptChanges()
        '------------------------------------------------------------------------------------------------------------
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Function GetQueryStampaPdf(ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNum As Integer) As String
    Try
      Return oCldPack.GetQueryStampaPdf(strDittaCorrente, lAnno, strSerie, lNum)
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

  Public Overridable Function RipristinaMov(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    Try
      If nRow < 0 Then Return True

      dsShared.Tables("MOVPAC").Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
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

  Public Overridable Function AttribuisciTaglia(ByRef dttTmp As DataTable, ByRef dttMovPac As DataTable, ByVal bDaMovmag As Boolean) As Boolean
    Dim i As Integer
    Dim j As Integer
    Dim dttArti As New DataTable
    Dim dttTabtagl As New DataTable
    Dim dttOut As New DataTable
    Dim dtrT() As DataRow = Nothing
    Try
      dttOut = dttMovPac.Clone
      For i = 0 To dttMovPac.Rows.Count - 1
        If oCldPack.ValCodiceDb(NTSCStr(dttMovPac.Rows(i)!mp_codart), strDittaCorrente, "ARTICO", "S", , dttArti) Then

          If NTSCInt(dttArti.Rows(0)!ar_codtagl) <> 0 Then
            For j = 1 To 24
              If bDaMovmag Then
                If NTSCDec(dttTmp.Rows(i)("mm_quant" & NTSCStr(IIf(j < 10, "0" & j, j)))) <> 0 Then
                  dttOut.ImportRow(dttMovPac.Rows(i))
                  dttOut.Rows(dttOut.Rows.Count - 1)!mp_indtagl = j
                  dttOut.Rows(dttOut.Rows.Count - 1)!mp_quant = NTSCDec(dttTmp.Rows(i)("mm_quant" & NTSCStr(IIf(j < 10, "0" & j, j))))
                  dttOut.Rows(dttOut.Rows.Count - 1)!mp_colli = NTSCDec(dttTmp.Rows(i)("mm_quant" & NTSCStr(IIf(j < 10, "0" & j, j))))

                  If oCldPack.ValCodiceDb(NTSCStr(dttArti.Rows(0)!ar_codtagl), strDittaCorrente, "TABTAGL", "S", , dttTabtagl) Then
                    dttOut.Rows(dttOut.Rows.Count - 1)!mp_tctaglia = NTSCStr(dttTabtagl.Rows(0)("tb_dest" & NTSCStr(IIf(j < 10, "0" & j, j))))
                  End If
                  Continue For
                End If
              Else
                If NTSCDec(dttTmp.Rows(i)("mo_quant" & NTSCStr(IIf(j < 10, "0" & j, j)))) <> 0 Then
                  dttOut.ImportRow(dttMovPac.Rows(i))
                  dttOut.Rows(dttOut.Rows.Count - 1)!mp_indtagl = j
                  dttOut.Rows(dttOut.Rows.Count - 1)!mp_quant = NTSCDec(dttTmp.Rows(i)("mo_quant" & NTSCStr(IIf(j < 10, "0" & j, j))))
                  dttOut.Rows(dttOut.Rows.Count - 1)!mp_colli = NTSCDec(dttTmp.Rows(i)("mo_quant" & NTSCStr(IIf(j < 10, "0" & j, j))))

                  If oCldPack.ValCodiceDb(NTSCStr(dttArti.Rows(0)!ar_codtagl), strDittaCorrente, "TABTAGL", "S", , dttTabtagl) Then
                    dttOut.Rows(dttOut.Rows.Count - 1)!mp_tctaglia = NTSCStr(dttTabtagl.Rows(0)("tb_dest" & NTSCStr(IIf(j < 10, "0" & j, j))))
                  End If
                  Continue For
                End If
              End If
            Next
          Else
            dttOut.ImportRow(dttMovPac.Rows(i))
            dttOut.Rows(dttOut.Rows.Count - 1)!mp_indtagl = 0
            dttOut.Rows(dttOut.Rows.Count - 1)!mp_tctaglia = " "
          End If
        End If
      Next

      If bAggiungiRighe Then
        dtrT = dsShared.Tables("MOVPAC").Select("", "mp_riga DESC")
        If dtrT.Length > 0 Then dMaxRiga = NTSCDec(dtrT(0)!mp_riga)
      End If

      For i = 0 To dttOut.Rows.Count - 1
        If i = 0 Then
          dttOut.Rows(i)!mp_riga = NTSCInt(dMaxRiga) + lIncremContatoreRiga
        Else
          dttOut.Rows(i)!mp_riga = NTSCDec(dttOut.Rows(i - 1)!mp_riga) + lIncremContatoreRiga
        End If
      Next

      dttMovPac = dttOut

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

#Region "Sepa"
  Public Overridable Function AnnoCompetenza() As String
    Dim dttTable As New DataTable()
    Try
      oCldPack.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttTable)

      Return NTSCStr(dttTable.Rows(0)!tb_escomp)
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

  Public Overridable Function LeggiNumerazione(ByVal strSerie As String, ByVal lAnno As Integer) As Integer
    Try
      Return oCldPack.LegNuma(strDittaCorrente, "V", UCase(strSerie), lAnno, True)
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

  Public Overridable Function EsistePacking(ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumero As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      Return oCldPack.EsistePacking(strDittaCorrente, lAnno, strSerie, lNumero, dttOut)
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

#Region "Pror"
  Public Overridable Function EsisteOrdine(ByVal strTipo As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumero As Integer, _
                                           ByRef dttOut As DataTable) As Boolean
    Try
      Return oCldPack.EsisteOrdine(strDittaCorrente, strTipo, lAnno, strSerie, lNumero, dttOut)
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

#Region "Seba"
  Public Overridable Function EsisteTestmag(ByVal strTipo As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumero As Integer, _
                                            ByRef dttOut As DataTable) As Boolean
    Try
      Return oCldPack.EsisteTestmag(strDittaCorrente, strTipo, lAnno, strSerie, lNumero, dttOut)
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

#Region "Impa"
  Public Overridable Function NumaImpa(ByRef lCod As Integer) As Boolean
    Dim strErr As String = ""
    Try
      lCod = oCldPack.LegNuma(strDittaCorrente, "V1", " ", 0, True)
      lCod = oCldPack.AggNuma(strDittaCorrente, "V1", " ", 0, lCod, True, True, strErr)

      If strErr <> "" Then
        ThrowRemoteEvent(New NTSEventArgs("", strErr))
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

#Region "Sed1"
  Public Overridable Function ZoomSeor(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                       ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                                       ByVal strDataA As String, ByVal strDatConsDa As String, ByVal strDatConsA As String, _
                                       ByVal strEvaso As String, ByVal strRiferim As String, ByVal strDitta As String) As DataSet
    Try
      Return oCldPack.ZoomSeor(strTipork, nAnno, strSerie, lContoDa, lContoA, strDataDa, strDataA, strDatConsDa, strDatConsA, strEvaso, strRiferim, strDitta)

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
#End Region

#Region "Sep1"
  Public Overridable Function ZoomSepa(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                       ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                                       ByVal strDataA As String, ByVal strEvaso As String, ByVal strRiferim As String, _
                                       ByVal strDitta As String) As DataSet
    Try
      Return oCldPack.ZoomSepa(strTipork, nAnno, strSerie, lContoDa, lContoA, strDataDa, strDataA, strEvaso, strRiferim, strDitta)
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
#End Region
End Class