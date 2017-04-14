Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEXXLIST
  Inherits CLE__BASN
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


  Public oCldList As CLDXXLIST



  Public strActLog As String = ""
  Public strActLogProg As String = ""

  Public bHasChanges As Boolean
  Public dsShared As DataSet

  Public strCasella As String = " "
  Public dsListConf As DataSet
  Public nCount As Integer = 0

  Public strTipork As String = " "            'corrisponde a lc_tipo
  Public strCodart As String = ""             'articolo per visualizzaz articoli da bnmgarti
  Public strCodartRoot As String = ""         'cod articolo root per ottenere info in caso richiamato da BNMGARTV BNTCARTV
  Public nFase As Integer = -1                'fase articolo
  Public lConto As Integer = 0                'conto cliente/fornitore per visualizz listini specifici cli/forn da bn__clie
  Public lCoddest As Integer = 0              'destinazione diversa
  Public strChildParent As String = ""        'nome del child chiamante (BN__CLIE, BNMGARTI, ...)
  Public nValuta As Integer = 0               'valuta impostata in UI
  Public bInPromozione As Boolean = False     'flag PROMOZIONE passato da UI
  Public bNoTestValuta As Boolean = False     'flag tutte le valute passato da UI
  Public bVariantiLiv1 As Boolean = False     'Per articoli a varianti con prezzi sulla prima variante
  Public dtInizioValListini As DateTime = DateTime.Now

  Public bLeggiCodarfoSingolarmente As Boolean = False

  Public Overrides Function Init(ByRef App As CLE__APP, _
                                    ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                                    ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                                    ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDXXLIST"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldList = CType(MyBase.ocldBase, CLDXXLIST)
    oCldList.Init(oApp)

    bLeggiCodarfoSingolarmente = (CBool(oCldList.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "LeggiCodarfoSingolarmente", "0", " ", "0")))

    Return True
  End Function

  Public Overridable Function Apri(ByVal strDitta As String, ByVal bNoTestCodvalu As Boolean, ByVal nCodvalu As Integer, _
                                   ByVal bInpromo As Boolean, ByVal bNoTestValidoil As Boolean, ByVal strValidoil As String, _
                                   ByRef ds As DataSet, ByVal bVisCodarfo As Boolean) As Boolean
    Dim dReturn As Boolean = False
    Dim strListiniAbilitati As String = ""
    Dim strListiniDaVisualizzare As String = ""
    Dim bEscludiCosti As Boolean = True
    Dim bVisTuttiListiniSpeciali As Boolean = False
    Dim bVisUltcost As Boolean = False
    Dim bVisUltcostoneri As Boolean = False
    Dim bVisCostomedio As Boolean = False
    Dim bVisspeciale As Boolean = False
    Dim i As Integer = 0
    Dim strTmp() As String = Nothing
    Dim dtrT() As DataRow = Nothing

    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      nValuta = nCodvalu
      bInPromozione = bInpromo
      bNoTestValuta = bNoTestCodvalu


      '------------------------------
      'limitazioni sui listini generici
      If lConto = 0 Then
        strListiniAbilitati = oCldList.GetSettingBus("BSMGARTI", "OPZIONIUT", ".", "ListiniAbilitati", "", " ", "")
        strListiniAbilitati = strListiniAbilitati.Replace(";", ",")
      End If

      If strChildParent = "BNMGARTI" Then
        dtInizioValListini = NTSCDate(oCldList.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DataInizioValListini", DateTime.Now.ToShortDateString, " ", DateTime.Now.ToShortDateString))
      ElseIf strChildParent = "BNMGARTV" Then
        dtInizioValListini = NTSCDate(oCldList.GetSettingBus("BSMGARTV", "OPZIONI", ".", "DataInizioValListini", DateTime.Now.ToShortDateString, " ", DateTime.Now.ToShortDateString))
      ElseIf strChildParent = "BNTCARTV" Then
        dtInizioValListini = NTSCDate(oCldList.GetSettingBus("BSTCARTV", "OPZIONI", ".", "DataInizioValListini", DateTime.Now.ToShortDateString, " ", DateTime.Now.ToShortDateString))
      End If

      If strChildParent = "BNMGHLAR" Or strChildParent = "BNVEBOLX" Then
        bVisUltcost = True
        bVisUltcostoneri = True
        bVisCostomedio = True
        bVisspeciale = True
        strListiniDaVisualizzare = oCldList.GetSettingBus("BSVEBOLX", "Opzioni", ".", "ListiniDaVisualizzare", " ", " ", " ")
        strListiniDaVisualizzare = strListiniDaVisualizzare.Replace(";", ",")
        bEscludiCosti = CBool(oCldList.GetSettingBus("BSVEBOLX", "OPZIONI", ".", "EscludiCosti", "0", " ", "0"))
        bVisTuttiListiniSpeciali = CBool(oCldList.GetSettingBus("BSVEBOLX", "OPZIONI", ".", "VisTuttiListiniSpeciali", "0", " ", "0"))

        i = strListiniDaVisualizzare.ToUpper.IndexOf("S")
        If i = -1 And strListiniDaVisualizzare.Trim <> "" Then
          bVisspeciale = False
        Else
          If lConto = 0 Then bVisspeciale = False
          'devo togliere il listini "S"
          strTmp = strListiniDaVisualizzare.Split(","c)
          strListiniDaVisualizzare = ""
          For i = 0 To strTmp.Length - 1
            If IsNumeric(strTmp(i)) Then
              strListiniDaVisualizzare += strTmp(i) & ", "
            End If
          Next
          If strListiniDaVisualizzare.Length > 1 Then strListiniDaVisualizzare = strListiniDaVisualizzare.Substring(0, strListiniDaVisualizzare.Length - 2)
        End If

      End If

      dReturn = oCldList.GetData(strDittaCorrente, lConto, strCodart, nFase, bNoTestCodvalu, _
                                 nCodvalu, bInpromo, bNoTestValidoil, strValidoil, strTipork, ds, _
                                 bVisUltcost, bVisUltcostoneri, bVisCostomedio, bVisspeciale, _
                                 strListiniAbilitati, strListiniDaVisualizzare, bEscludiCosti, _
                                 bVisTuttiListiniSpeciali, bVisCodarfo, bLeggiCodarfoSingolarmente)
      If dReturn = False Then Return False

      '--------------------------------------
      'carico codarfo
      If (bVisCodarfo = True) And (bLeggiCodarfoSingolarmente = True) Then
        dtrT = ds.Tables("LISTINI").Select("lc_conto <> 0")
        For i = 0 To dtrT.Length - 1
          dtrT(i)!xx_codarfo = oCldList.GetCodarfo(strDittaCorrente, NTSCStr(dtrT(i)!lc_codart), NTSCInt(dtrT(i)!lc_conto))
        Next
      End If

      '--------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldList.SetTableDefaultValueFromDB("LISTINI", ds)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      SetDefaultValue(ds)
      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("LISTINI").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("LISTINI").ColumnChanged, AddressOf AfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsShared.Tables("LISTINI").AcceptChanges()
      bHasChanges = False

      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldList.GetSettingBus("BSXXLIST", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BS" & strChildParent.Substring(2).Replace("_", "-")

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

  Public Overridable Function ApriListiniConf(ByVal strDitta As String, ByVal strArticolo As String, ByVal strCodcas As String, ByVal strListino As String, ByVal bNoTestCodvalu As Boolean, ByVal nCodvalu As Integer, _
                                             ByVal bInpromo As Boolean, ByRef ds As DataSet) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer
    Try

      strDittaCorrente = strDitta
      nValuta = nCodvalu
      bInPromozione = bInpromo
      bNoTestValuta = bNoTestCodvalu
      strCasella = strCodcas
      'strCodart = strArticolo
      strTipork = strListino

      dtInizioValListini = NTSCDate(DateTime.Now.ToShortDateString)

      '--------------------------------------
      'carico codarfo
      dtrT = ds.Tables("LISTINI").Select("lc_conto <> 0")
      For i = 0 To dtrT.Length - 1
        dtrT(i)!xx_codarfo = oCldList.GetCodarfo(strDittaCorrente, NTSCStr(dtrT(i)!lc_codart), NTSCInt(dtrT(i)!lc_conto))
      Next

      '--------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldList.SetTableDefaultValueFromDB("LISTINI", ds)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      SetDefaultValue(ds)
      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("LISTINI").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("LISTINI").ColumnChanged, AddressOf AfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsShared.Tables("LISTINI").AcceptChanges()
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

  Public Overridable Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("LISTINI").Columns("codditt").DefaultValue = ".,"      'per far scatenare la onaddnew 

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


  Public Overridable Sub Nuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("LISTINI").Rows.Add(dsShared.Tables("LISTINI").NewRow)
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


  Public Overridable Function Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      If dsShared IsNot Nothing Then dsShared.Tables("LISTINI").Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If


      If strChildParent <> "BNCPIMCO" Then
        '----------------------------------------
        'chiamo il dal per salvare
        If strActLog <> "-1" Then
          bResult = oCldList.Salva(strDittaCorrente, dsShared.Tables("LISTINI"), "")
        Else
          bResult = oCldList.Salva(strDittaCorrente, dsShared.Tables("LISTINI"), strActLogProg)
        End If
      Else
        'Copiare da dsShared.Tables.listini in ds
        bResult = SalvaListiniConf()
      End If

      If bResult Then
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

  Public Overridable Function SalvaListiniConf() As Boolean
    Dim dtrChange() As DataRow = Nothing
    Dim i As Integer = 0
    Dim dtrK() As DataRow = Nothing
    Dim k As Integer = 0
    Dim dtrU() As DataRow = Nothing
    Try

      'Delete
      dtrChange = dsShared.Tables("LISTINI").Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        dtrK = dsListConf.Tables("LISTINI").Select("lc_conto = " & dtrChange(i)("lc_conto", DataRowVersion.Original).ToString & _
                                                   " AND lc_coddest = " & dtrChange(i)("lc_coddest", DataRowVersion.Original).ToString & _
                                                   " AND lc_codlavo = " & dtrChange(i)("lc_codlavo", DataRowVersion.Original).ToString & _
                                                   " AND lc_codvalu = " & dtrChange(i)("lc_codvalu", DataRowVersion.Original).ToString & _
                                                   " AND lc_listino = " & dtrChange(i)("lc_listino", DataRowVersion.Original).ToString & _
                                                   " AND lc_codtpro = " & dtrChange(i)("lc_codtpro", DataRowVersion.Original).ToString & _
                                                   " AND lc_datagg = " & CDataSQL(NTSCDate(dtrChange(i)("lc_datagg", DataRowVersion.Original).ToString)) & _
                                                   " AND lc_daquant = " & CDblSQL(dtrChange(i)("lc_daquant", DataRowVersion.Original).ToString) & _
                                                   " AND lc_unmis = " & CStrSQL(dtrChange(i)("lc_unmis", DataRowVersion.Original).ToString) & _
                                                   " AND lc_fase = " & dtrChange(i)("lc_fase", DataRowVersion.Original).ToString & _
                                                   " AND lc_codcas = " & CStrSQL(dtrChange(i)("lc_codcas", DataRowVersion.Original).ToString))
        If dtrK.Length > 0 Then
          dtrK(0).Delete()
          dtrK(0).AcceptChanges()
        End If

        dtrChange(i).AcceptChanges()
      Next

      'Insert
      dtrChange = dsShared.Tables("LISTINI").Select(Nothing, Nothing, DataViewRowState.Added)
      For i = 0 To dtrChange.Length - 1
        dtrChange(i).AcceptChanges()

        dsListConf.Tables("LISTINI").NewRow()
        dsListConf.Tables("LISTINI").ImportRow(dtrChange(i))
        dsListConf.Tables("LISTINI").AcceptChanges()

        'Aggiorna la data di scadenza dell'eventuale listino immediatamente precedente
        dtrK = dsShared.Tables("LISTINI").Select("lc_conto = " & dtrChange(i)!lc_conto.ToString & _
                                                 " AND lc_coddest = " & dtrChange(i)!lc_coddest.ToString & _
                                                 " AND lc_codlavo = " & dtrChange(i)!lc_codlavo.ToString & _
                                                 " AND lc_codvalu = " & dtrChange(i)!lc_codvalu.ToString & _
                                                 " AND lc_listino = " & dtrChange(i)!lc_listino.ToString & _
                                                 " AND lc_codtpro = " & dtrChange(i)!lc_codtpro.ToString & _
                                                 " AND lc_datagg < " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                                                 " AND lc_daquant = " & CDblSQL(dtrChange(i)!lc_daquant.ToString) & _
                                                 " AND lc_datscad >= " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                                                 " AND lc_unmis = " & CStrSQL(dtrChange(i)!lc_unmis.ToString) & _
                                                 " AND lc_fase = " & dtrChange(i)!lc_fase.ToString & _
                                                 " AND lc_codcas = " & CStrSQL(dtrChange(i)!lc_codcas.ToString))
        For k = 0 To dtrK.Length - 1
          dtrK(k)!lc_datscad = DateAdd("d", -1, dtrChange(i)!lc_datagg)
          dtrK(k)!lc_ultagg = DateTime.Now
          dtrK(k).AcceptChanges()
        Next

        dtrK = dsListConf.Tables("LISTINI").Select("lc_conto = " & dtrChange(i)!lc_conto.ToString & _
                                                   " AND lc_coddest = " & dtrChange(i)!lc_coddest.ToString & _
                                                   " AND lc_codlavo = " & dtrChange(i)!lc_codlavo.ToString & _
                                                   " AND lc_codvalu = " & dtrChange(i)!lc_codvalu.ToString & _
                                                   " AND lc_listino = " & dtrChange(i)!lc_listino.ToString & _
                                                   " AND lc_codtpro = " & dtrChange(i)!lc_codtpro.ToString & _
                                                   " AND lc_datagg < " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                                                   " AND lc_daquant = " & CDblSQL(dtrChange(i)!lc_daquant.ToString) & _
                                                   " AND lc_datscad >= " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                                                   " AND lc_unmis = " & CStrSQL(dtrChange(i)!lc_unmis.ToString) & _
                                                   " AND lc_fase = " & dtrChange(i)!lc_fase.ToString & _
                                                   " AND lc_codcas = " & CStrSQL(dtrChange(i)!lc_codcas.ToString))
        For k = 0 To dtrK.Length - 1
          dtrK(k)!lc_datscad = DateAdd("d", -1, dtrChange(i)!lc_datagg)
          dtrK(k)!lc_ultagg = DateTime.Now
          dtrK(k).AcceptChanges()
        Next

        'Aggiorna la data di scadenza del record appena inserito se esiste un record con data di aggiornamento posteriore
        dtrK = dsListConf.Tables("LISTINI").Select("lc_conto = " & dtrChange(i)!lc_conto.ToString & _
                                                   " AND lc_coddest = " & dtrChange(i)!lc_coddest.ToString & _
                                                   " AND lc_codlavo = " & dtrChange(i)!lc_codlavo.ToString & _
                                                   " AND lc_codvalu = " & dtrChange(i)!lc_codvalu.ToString & _
                                                   " AND lc_listino = " & dtrChange(i)!lc_listino.ToString & _
                                                   " AND lc_codtpro = " & dtrChange(i)!lc_codtpro.ToString & _
                                                   " AND lc_datagg > " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                                                   " AND lc_daquant = " & CDblSQL(dtrChange(i)!lc_daquant.ToString) & _
                                                   " AND lc_unmis = " & CStrSQL(dtrChange(i)!lc_unmis.ToString) & _
                                                   " AND lc_fase = " & dtrChange(i)!lc_fase.ToString & _
                                                   " AND lc_codcas = " & CStrSQL(dtrChange(i)!lc_codcas.ToString))
        If dtrK.Length > 0 Then
          dtrChange(i)!lc_datscad = DateAdd("d", -1, dtrK(0)!lc_datagg)
          dtrChange(i)!lc_ultagg = DateTime.Now
          dtrChange(i).AcceptChanges()
          dtrU = dsListConf.Tables("LISTINI").Select("lc_conto = " & dtrChange(i)!lc_conto.ToString & _
                                                     " AND lc_coddest = " & dtrChange(i)!lc_coddest.ToString & _
                                                     " AND lc_codlavo = " & dtrChange(i)!lc_codlavo.ToString & _
                                                     " AND lc_codvalu = " & dtrChange(i)!lc_codvalu.ToString & _
                                                     " AND lc_listino = " & dtrChange(i)!lc_listino.ToString & _
                                                     " AND lc_codtpro = " & dtrChange(i)!lc_codtpro.ToString & _
                                                     " AND lc_datagg = " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                                                     " AND lc_daquant = " & CDblSQL(dtrChange(i)!lc_daquant.ToString) & _
                                                     " AND lc_unmis = " & CStrSQL(dtrChange(i)!lc_unmis.ToString) & _
                                                     " AND lc_fase = " & dtrChange(i)!lc_fase.ToString & _
                                                     " AND lc_codcas = " & CStrSQL(dtrChange(i)!lc_codcas.ToString))
          If dtrU.Length > 0 Then
            dtrU(0)!lc_datscad = DateAdd("d", -1, dtrK(0)!lc_datagg)
            dtrU(0)!lc_ultagg = DateTime.Now
            dtrU(0).AcceptChanges()
          End If
        End If
      Next

      'Update
      dtrChange = dsShared.Tables("LISTINI").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1
        dtrChange(i).AcceptChanges()

        dtrK = dsListConf.Tables("LISTINI").Select("lc_conto = " & dtrChange(i)!lc_conto.ToString & _
                                                   " AND lc_coddest = " & dtrChange(i)!lc_coddest.ToString & _
                                                   " AND lc_codlavo = " & dtrChange(i)!lc_codlavo.ToString & _
                                                   " AND lc_codvalu = " & dtrChange(i)!lc_codvalu.ToString & _
                                                   " AND lc_listino = " & dtrChange(i)!lc_listino.ToString & _
                                                   " AND lc_codtpro = " & dtrChange(i)!lc_codtpro.ToString & _
                                                   " AND lc_datagg = " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                                                   " AND lc_daquant = " & CDblSQL(dtrChange(i)!lc_daquant.ToString) & _
                                                   " AND lc_unmis = " & CStrSQL(dtrChange(i)!lc_unmis.ToString) & _
                                                   " AND lc_fase = " & dtrChange(i)!lc_fase.ToString & _
                                                   " AND lc_codcas = " & CStrSQL(dtrChange(i)!lc_codcas.ToString))
        If dtrK.Length > 0 Then
          dtrK(0).Delete()
          dtrK(0).AcceptChanges()
          dsListConf.Tables("LISTINI").NewRow()
          dsListConf.Tables("LISTINI").ImportRow(dtrChange(i))
          dsListConf.Tables("LISTINI").AcceptChanges()
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

  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property


  Public Overridable Sub OnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttArti As New DataTable
    Try
      e.Row!codditt = strDittaCorrente
      e.Row!lc_tipo = strTipork
      e.Row!lc_perqta = 1
      If strCodartRoot.Trim = "" Then
        e.Row!lc_codart = IIf(strCodart = "", " ", strCodart).ToString
      Else
        oCldList.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)

        Select Case NTSCStr(dttArti.Rows(0)!ar_prevar)
          Case "N" : e.Row!lc_codart = Trim(strCodartRoot).toupper
          Case "S" : e.Row!lc_codart = strCodart.toupper
          Case "1" : bVariantiLiv1 = True : e.Row!lc_codart = (Trim(strCodartRoot) & Trim(NTSCStr(dttArti.Rows(0)!ar_codvar1))).toupper
        End Select
      End If
      e.Row!lc_conto = lConto
      If lConto > 0 Then e.Row!lc_coddest = lCoddest
      e.Row!lc_codvalu = nValuta
      e.Row!xx_codlavo = oApp.Tr(Me, 128472860573293164, "Acquisto/Cessione")
      e.Row!lc_listino = NTSCInt(IIf(strTipork = "C" Or strTipork = "F" Or strTipork = "J", 0, 1))
      e.Row!lc_datagg = dtInizioValListini
      e.Row!lc_datscad = NTSCDate(IntSetDate("31/12/2099"))
      e.Row!lc_codcas = strCasella
      e.Row!lc_ultagg = DateTime.Now

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

  Public Overridable Function CheckListinoEsistenteConf(ByRef dtrIn As DataRow) As Boolean
    Dim dtrK() As DataRow = Nothing
    Try

      dtrK = dsListConf.Tables("LISTINI").Select("lc_conto = " & dtrIn!lc_conto.ToString & _
                                                 " AND lc_coddest = " & dtrIn!lc_coddest.ToString & _
                                                 " AND lc_codlavo = " & dtrIn!lc_codlavo.ToString & _
                                                 " AND lc_codvalu = " & dtrIn!lc_codvalu.ToString & _
                                                 " AND lc_listino = " & dtrIn!lc_listino.ToString & _
                                                 " AND lc_codtpro = " & dtrIn!lc_codtpro.ToString & _
                                                 " AND lc_datagg = " & CDataSQL(NTSCDate(dtrIn!lc_datagg)) & _
                                                 " AND lc_daquant = " & CDblSQL(dtrIn!lc_daquant.ToString) & _
                                                 " AND lc_unmis = " & CStrSQL(dtrIn!lc_unmis.ToString) & _
                                                 " AND lc_fase = " & dtrIn!lc_fase.ToString & _
                                                 " AND lc_codcas = " & CStrSQL(dtrIn!lc_codcas.ToString) & _
                                                 " AND xx_progr <> " & dtrIn!xx_progr.ToString)
      If dtrK.Length > 0 Then Return True

      Return False
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



#Region "Before / AfterColUpdate"
  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'se non  compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "codditt" Then
        If e.Row!codditt.ToString = ".," Then OnAddNewRow(sender, e)
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

  Public Overridable Sub BeforeColUpdate_lc_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If

      If Not bVariantiLiv1 Then
        If (strCodart.Trim <> "" And NTSCStr(e.ProposedValue).Trim.ToUpper <> strCodart.Trim.ToUpper) And (strCodartRoot.Trim = "" Or _
           (strCodartRoot.Trim <> "" And NTSCStr(e.ProposedValue).Trim.ToUpper <> strCodartRoot.Trim.ToUpper)) Then
          'ono nell'anagrafica articoli: non posso cambiare il codice 
          e.Row!lc_codart = strCodart.Trim.ToUpper
          e.ProposedValue = strCodart.Trim.ToUpper
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472859405283164, "Il codice articolo non può essere cambiato con qeusto programma")))
        End If
      End If

      oCldList.ValCodiceDb(SettaVarValidazioni(e.ProposedValue.ToString), strDittaCorrente, "ARTICO", "S", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128908276764281994, "Articolo inesistente")))
        e.ProposedValue = NTSCStr(e.Row!lc_codart)
        Return
      End If


      '----------------------------------------------------------------------
      'Trattamento particolare articoli a varianti
      If (dttTmp.Rows(0)!ar_gesvar.ToString = "S") And _
         (strChildParent.ToUpper <> "BNMGARTI") And _
         (strChildParent.ToUpper <> "BNMGARTV") And _
         (strChildParent.ToUpper <> "BNTCARTV") Then
        If dttTmp.Rows(0)!ar_prevar.ToString = "N" Then
          'articolo con prezzi comuni (se si sceglie la root va bene così)
          If NTSCStr(dttTmp.Rows(0)!ar_codroot).Trim <> "" Then
            'trattasi della variante
            e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_codroot)
            Return
          End If
        ElseIf dttTmp.Rows(0)!ar_prevar.ToString = "1" Then
          'articoli con prezzi sulla 1. variante
          If NTSCStr(dttTmp.Rows(0)!ar_codroot) <> "" Then
            'trattasi della variante
            e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_codroot) & NTSCStr(dttTmp.Rows(0)!ar_codvar1).Trim
            Return
          Else
            'trattasi dell'articolo root (non ammesso)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472785697093164, "Non è possibile selezionare il codice root di articoli a varianti il cui prezzo dipende dalla 1° variante")))
            e.ProposedValue = NTSCStr(e.Row!lc_codart)
            Return
          End If
        Else
          'articoli con prezzi diversi per ogni variante
          If NTSCStr(dttTmp.Rows(0)!ar_codroot) = "" Then
            'trattasi dell'articolo root (non ammesso)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472786104903164, "Non è possibile selezionare il codice root di articoli a varianti il cui prezzo dipende da tutte le varianti")))
            e.ProposedValue = NTSCStr(e.Row!lc_codart)
            Return
          End If
        End If
      End If    'If dttTmp.Rows(0)!ar_gesvar.ToString = "S" Then

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

  Public Overridable Sub BeforeColUpdate_lc_fase(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Dim nUltfase As Integer = 0
    Dim bGesfasi As Boolean = False
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.Row!lc_codart) = "" And NTSCInt(e.ProposedValue) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128908276902879292, "Prima di impostare la fase indicare un codice articolo valido")))
        e.ProposedValue = NTSCInt(e.Row!lc_fase)
        Return
      End If

      oCldList.ValCodiceDb(SettaVarValidazioni(NTSCStr(e.Row!lc_codart)), strDittaCorrente, "ARTICO", "S", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128908276709436840, "Articolo inesistente")))
        e.ProposedValue = NTSCInt(e.Row!lc_fase)
        Return
      End If

      If dttTmp.Rows(0)!ar_gesfasi.ToString = "S" Then
        bGesfasi = True
        nUltfase = NTSCInt(dttTmp.Rows(0)!ar_ultfase)
      Else
        bGesfasi = False
        nUltfase = 0
      End If

      dttTmp.Clear()

      If bGesfasi Then
        If NTSCInt(e.ProposedValue) = 0 Then
          e.ProposedValue = NTSCInt(e.Row!lc_fase)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472833823223164, "L'articolo è gestito a fasi, per cui occorre indicare una fase diversa da 0")))
        Else
          If Not oCldList.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "ARTFASI", "N", strTmp, Nothing, NTSCStr(e.Row!lc_codart)) Then
            e.ProposedValue = NTSCInt(e.Row!lc_fase)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472823370383164, "Fase articolo inesistente")))
          Else
            e.Row!xx_fase = strTmp
          End If
        End If
      Else
        If NTSCInt(e.ProposedValue) <> 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472822298123164, "L'articolo non è gestito a FASI")))
          e.ProposedValue = 0
          e.Row!xx_fase = ""
        End If
      End If    'If bGesfasi Then

      If NTSCInt(e.ProposedValue) = 0 And NTSCStr(e.Row!xx_fase) <> "" Then e.Row!xx_fase = ""

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

  Public Overridable Sub BeforeColUpdate_lc_unmis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp, dttArti As New DataTable
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.Row!lc_codart) = "" And NTSCInt(e.ProposedValue) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128908276936317648, "Prima di impostare l'unità di misura indicare un codice articolo valido")))
        e.ProposedValue = NTSCStr(e.Row!lc_unmis)
        Return
      End If

      If strCodartRoot.Trim <> "" Then
        oCldList.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)

        Select Case NTSCStr(dttArti.Rows(0)!ar_prevar)
          Case "N" : e.Row!lc_codart = Trim(strCodartRoot).toupper
          Case "S" : e.Row!lc_codart = strCodart.toupper
          Case "1" : bVariantiLiv1 = True : e.Row!lc_codart = (Trim(strCodartRoot) & Trim(NTSCStr(dttArti.Rows(0)!ar_codvar1))).toupper
        End Select
      End If

      If Not oCldList.ValCodiceDb(SettaVarValidazioni(NTSCStr(e.Row!lc_codart)), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
        dttTmp = dttArti
      End If

      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129131996151327382, "Articolo per unità di misura inesistente")))
        e.ProposedValue = NTSCStr(e.Row!lc_unmis)
        Return
      End If

      If NTSCStr(e.ProposedValue).ToUpper <> NTSCStr(dttTmp.Rows(0)!ar_unmis).ToUpper Then
        strTmp = " '" & NTSCStr(dttTmp.Rows(0)!ar_unmis) & "'"
        If NTSCStr(dttTmp.Rows(0)!ar_confez2).Trim <> "" Then
Confez2:
          If NTSCStr(e.ProposedValue).ToUpper <> NTSCStr(dttTmp.Rows(0)!ar_confez2).ToUpper Then
            strTmp += " '" & NTSCStr(dttTmp.Rows(0)!ar_confez2) & "'"
            If NTSCStr(dttTmp.Rows(0)!ar_unmis2).Trim <> "" Then
Unmis2:
              If NTSCStr(e.ProposedValue).ToUpper <> NTSCStr(dttTmp.Rows(0)!ar_unmis2).ToUpper Then
                strTmp += " '" & NTSCStr(dttTmp.Rows(0)!ar_unmis2) & "'"
                If NTSCStr(dttTmp.Rows(0)!ar_um4).Trim <> "" Then
Um4:
                  If NTSCStr(e.ProposedValue).ToUpper <> NTSCStr(dttTmp.Rows(0)!ar_um4).ToUpper Then
                    strTmp += " '" & NTSCStr(dttTmp.Rows(0)!ar_um4) & "'"

                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472874655213164, "Unità di misura non presente nell'anagrafica articolo. Le unità di misura ammesse sono: |" & strTmp & "|")))
                    e.ProposedValue = NTSCStr(e.Row!lc_unmis)

                  Else
                    e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_um4)
                  End If
                Else
                  GoTo Um4 'controlla le ulteriori unita di misura per validazioni
                End If
              Else
                e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_unmis2)
              End If
            Else
              GoTo Unmis2 'controlla le ulteriori unita di misura per validazioni
            End If
          Else
            e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_confez2)
          End If
        Else
          GoTo Confez2 'controlla le ulteriori unita di misura per validazioni
        End If
      Else
        e.ProposedValue = NTSCStr(dttTmp.Rows(0)!ar_unmis)
      End If

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

  Public Overridable Sub BeforeColUpdate_lc_conto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.Row!lc_tipo) = "C" Or NTSCStr(e.Row!lc_tipo) = "F" Or NTSCStr(e.Row!lc_tipo) = "J" Then

        'listino specifico cli/forn
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xx_conto = ""
          e.Row!lc_coddest = 0
        Else
          If lConto <> 0 And NTSCInt(e.ProposedValue) <> lConto Then
            'sono dentro all'anagrafica clienti/forn_ non posso cambiare il cli/forn
            e.ProposedValue = lConto
            e.Row!lc_conto = lConto
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472857554103164, "Il codice cliente/fornitore non può essere variato con questo programma")))
          Else
            'sono dentro a bsmgarti
            If Not oCldList.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "ANAGRA" & NTSCStr(IIf(NTSCStr(e.Row!lc_tipo) = "F", "F", "C")), "N", strTmp) Then
              e.ProposedValue = NTSCInt(e.Row!lc_conto)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472842562643164, "Codice cliente/fornitore inesistente o non di tipo uguale a quello richiesto (cliente/fornitore)")))
            Else
              e.Row!xx_conto = strTmp
              If NTSCStr(e.Row!lc_codart).Trim <> "" Then
                e.Row!xx_codarfo = oCldList.GetCodarfo(strDittaCorrente, NTSCStr(e.Row!lc_codart), NTSCInt(e.ProposedValue))
              Else
                e.Row!xx_codarfo = ""
              End If
              e.Row!lc_coddest = 0
            End If
          End If    'If lConto <> 0 And NTSCInt(e.ProposedValue) <> lConto Then

        End If
      Else
        'listino generico
        If NTSCInt(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          e.Row!lc_coddest = 0
          e.Row!xx_conto = ""
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472841012413164, "Nei listini non specifici CLIENTI/FORNITORI il codice conto deve essere 0")))
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

  Public Overridable Sub BeforeColUpdate_lc_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.Row!lc_tipo) = "C" Or NTSCStr(e.Row!lc_tipo) = "F" Or NTSCStr(e.Row!lc_tipo) = "J" Then
        'listino specifico cli/forn
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xx_coddest = ""
        Else
          If NTSCInt(e.Row!lc_conto) = 0 Then
            e.ProposedValue = 0
            e.Row!xx_coddest = ""
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130584390586375908, "Prima di impostare la destinazione diversa occorre indicare il cod. cliente/fornitore")))
          Else
            If Not oCldList.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "DESTDIV", "N", strTmp, Nothing, NTSCInt(e.Row!lc_conto).ToString) Then
              e.ProposedValue = NTSCInt(e.Row!lc_coddest)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130584391515832715, "Codice destinazione diversa inesistente")))
            Else
              e.Row!xx_coddest = strTmp
            End If
          End If
        End If
      Else
        'listino generico
        If NTSCInt(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          e.Row!xx_coddest = ""
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130584389852803145, "Nei listini non specifici CLIENTI/FORNITORI il codice destinazione diversa deve essere 0")))
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

  Public Overridable Sub BeforeColUpdate_lc_listino(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.Row!lc_tipo) = "C" Or NTSCStr(e.Row!lc_tipo) = "F" Or NTSCStr(e.Row!lc_tipo) = "J" Then
        'listino specifico cli/forn
        If NTSCInt(e.ProposedValue) <> 0 Then
          e.ProposedValue = 0
          e.Row!xx_listino = ""
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472846613453164, "Nei listini specifici CLIENTI/FORNITORI il nunero di listino deve essere 0")))
        End If
      Else
        'listino generico
        If NTSCInt(e.ProposedValue) = 0 Then
          e.Row!xx_listino = ""
        Else
          oCldList.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABLIST", "N", strTmp)
          e.Row!xx_listino = strTmp
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

  Public Overridable Sub BeforeColUpdate_lc_codvalu(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codvalu = ""
        Return
      End If

      If Not oCldList.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABVALU", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472851508773164, "Codice valuta '|" & NTSCInt(e.ProposedValue).ToString & "|' inesistente ")))
        e.ProposedValue = NTSCInt(e.Row!lc_codvalu)
      Else
        e.Row!xx_codvalu = strTmp
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

  Public Overridable Sub BeforeColUpdate_lc_codtpro(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codtpro = ""
        Return
      End If

      If Not oCldList.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABTPRO", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128908277155698264, "Codice promozione '|" & NTSCInt(e.ProposedValue).ToString & "|' inesistente ")))
        e.ProposedValue = NTSCInt(e.Row!lc_codtpro)
      Else
        e.Row!xx_codtpro = strTmp
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

  Public Overridable Sub BeforeColUpdate_lc_codlavo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codlavo = oApp.Tr(Me, 128908276557557952, "Acquisto/Cessione")
        Return
      End If

      If Not oCldList.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABLAVO", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472852639903164, "Codice lavorazione '|" & NTSCInt(e.ProposedValue).ToString & "|' inesistente ")))
        e.ProposedValue = NTSCInt(e.Row!lc_codlavo)
      Else
        e.Row!xx_codlavo = strTmp
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



  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

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

  Public Overridable Sub AfterColUpdate_lc_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp, dttArti As New DataTable
    Try
      If e.ProposedValue.ToString.Trim = "" Then
        e.Row!xx_codart = ""
        e.Row!xx_desint = ""
        e.Row!lc_fase = 0
        e.Row!xx_fase = ""
        e.Row!lc_perqta = 1
        e.Row!lc_unmis = " "
        e.Row!xx_codarfo = ""
        Return
      End If

      If strCodartRoot.Trim <> "" Then
        oCldList.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)

        Select Case NTSCStr(dttArti.Rows(0)!ar_prevar)
          Case "N" : e.Row!lc_codart = Trim(strCodartRoot).toupper
          Case "S" : e.Row!lc_codart = strCodart.toupper
          Case "1" : bVariantiLiv1 = True : e.Row!lc_codart = (Trim(strCodartRoot) & Trim(NTSCStr(dttArti.Rows(0)!ar_codvar1))).toupper
        End Select
      End If

      If Not oCldList.ValCodiceDb(SettaVarValidazioni(e.ProposedValue.ToString), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
        dttTmp = dttArti
      End If

      If dttTmp.Rows.Count <> 0 Then
        e.Row!xx_codart = dttTmp.Rows(0)!ar_descr.ToString
        e.Row!xx_desint = NTSCStr(dttTmp.Rows(0)!ar_desint.ToString)
        e.Row!lc_fase = NTSCInt(dttTmp.Rows(0)!ar_ultfase)
        e.Row!lc_perqta = NTSCInt(dttTmp.Rows(0)!ar_perqta)
        e.Row!lc_unmis = dttTmp.Rows(0)!ar_unmis.ToString
        If NTSCInt(e.Row!lc_conto) = 0 Then
          e.Row!xx_codarfo = ""
        Else
          e.Row!xx_codarfo = oCldList.GetCodarfo(strDittaCorrente, e.ProposedValue.ToString, NTSCInt(e.Row!lc_conto))
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
    Finally
      dttTmp.Clear()
    End Try
  End Sub
#End Region


  Public Overridable Function TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim msg As NTSEventArgs
    Dim dttTmp As New DataTable

    Try
      dtrTmp = dsShared.Tables("LISTINI").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If (NTSCStr(dtrTmp(i)!lc_tipo) = "C" Or NTSCStr(dtrTmp(i)!lc_tipo) = "F" Or NTSCStr(dtrTmp(i)!lc_tipo) = "J") And NTSCInt(dtrTmp(i)!lc_conto) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472863961103164, "Nei listini specifici cliente/fornitore è necessario indicare un codice conto diverso da 0")))
          Return False
        End If

        If (NTSCStr(dtrTmp(i)!lc_tipo) = "C" Or NTSCStr(dtrTmp(i)!lc_tipo) = "F" Or NTSCStr(dtrTmp(i)!lc_tipo) = "J") And NTSCInt(dtrTmp(i)!lc_listino) <> 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472864505223164, "Nei listini specifici cliente/fornitore il numero di listino deve essere 0")))
          Return False
        End If

        If NTSCStr(dtrTmp(i)!lc_codart).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472864935363164, "Codice articolo obbligatorio")))
          Return False
        End If

        If NTSCDate(dtrTmp(i)!lc_datagg) > NTSCDate(dtrTmp(i)!lc_datscad) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472865610923164, "La data di fine validità non può essere minore della data di inizio validità")))
          Return False
        End If

        If NTSCInt(dtrTmp(i)!lc_codtpro) = 0 And bInPromozione Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472867741843164, "Indicare un codice promozione diverso da 0 prima di salvare")))
          Return False
        End If

        If NTSCInt(dtrTmp(i)!lc_codtpro) <> 0 And bInPromozione = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472868110623164, "Il codice promozione deve essere uguale a 0")))
          Return False
        End If

        If nValuta <> 0 And NTSCInt(dtrTmp(i)!lc_codvalu) <> nValuta And bNoTestValuta = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472869178963164, "Il codice valuta indicato in griglia è diverso da quello richiesto (|" & nValuta.ToString & "|)")))
          Return False
        End If

        If NTSCInt(dtrTmp(i)!lc_codvalu) = 0 And bNoTestValuta = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129470912442913595, "Il codice valuta indicato in griglia deve essere diverso da 0")))
          Return False
        End If

        dtrTmp(i)!lc_ultagg = DateTime.Now

        '-------------------------------
        'test listino già esistente
        If strChildParent <> "BNCPIMCO" Then
          If oCldList.CheckListinoEsistente(strDittaCorrente, dtrTmp(i)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128472870473923164, "Prezzo di listino già presente in questa data e con queste caratteristiche")))
            Return False
          End If
        Else
          If dtrTmp(i).RowState = DataRowState.Added Then
            dtrTmp(i)!xx_progr = nCount
            nCount = nCount + 1
          End If
          If CheckListinoEsistenteConf(dtrTmp(i)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129968442394984896, "Prezzo di listino già presente in questa data e con queste caratteristiche")))
            Return False
          End If
        End If

      Next

      If strChildParent <> "BNCPIMCO" Then
        dtrTmp = dsShared.Tables("LISTINI").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
        For i = 0 To dtrTmp.Length - 1
          '----------------------------------------------------------------------------------------------------------
          '--- Se esiste un listino con le stesse caratteristiche di chiave virtuale, con data di aggiornamento
          '--- diversa, avvisa chiedendo di proseguire/confermare
          '----------------------------------------------------------------------------------------------------------
          If oCldList.TrovaListinoEsistente(strDittaCorrente, dtrTmp(i), dttTmp) = True Then
            With dttTmp.Rows(0)
              msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 130794372971115665, "ATTENZIONE!" & vbCrLf & _
                "Esiste già un listino con queste caratteristiche," & vbCrLf & _
                "valido dal '|" & NTSCDate(!lc_datagg).ToShortDateString & "|' al '|" & NTSCDate(!lc_datscad).ToShortDateString & "|'" & vbCrLf & _
                " . Listino N°|" & NTSCStr(!lc_listino) & "|" & vbCrLf & _
                " . Articolo: '|" & NTSCStr(!lc_codart) & "|'" & _
                IIf(NTSCInt(!lc_codlavo) <> 0, vbCrLf & " . Lavorazione: '|" & NTSCStr(!lc_codlavo) & "|'", "").ToString & _
                IIf(NTSCInt(!lc_conto) <> 0, vbCrLf & " . Conto: '|" & NTSCStr(!lc_conto) & "|'", "").ToString & _
                IIf(NTSCInt(!lc_coddest) <> 0, vbCrLf & " . Destinazione: '|" & NTSCStr(!lc_coddest) & "|'", "").ToString & _
                IIf(NTSCInt(!lc_codvalu) <> 0, vbCrLf & " . Valuta: '|" & NTSCStr(!lc_codvalu) & "|'", "").ToString & _
                IIf(NTSCInt(!lc_codtpro) <> 0, vbCrLf & " . Promozione: '|" & NTSCStr(!lc_codtpro) & "|'", "").ToString & _
                vbCrLf & " . Data inizio validità: '|" & NTSCDate(!lc_datagg).ToShortDateString & "|'" & _
                vbCrLf & " . Da quantità: '|" & NTSCStr(!lc_daquant) & "|'" & _
                IIf(NTSCStr(!lc_unmis).Trim <> "", vbCrLf & " . Unità di misura: '|" & NTSCStr(!lc_unmis) & "|'", "").ToString & _
                IIf(NTSCInt(!lc_fase) <> 0, vbCrLf & " . Fase: '|" & NTSCStr(!lc_fase) & "|'", "").ToString & _
                vbCrLf & "Confermare ugualmente l'inserimento/modifica del listino?"))
              ThrowRemoteEvent(msg)
              If msg.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
            End With
          End If
          '----------------------------------------------------------------------------------------------------------
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
      Return False
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function


  Public Overridable Function edCodvalu_Validated(ByVal nCodvalu As Integer, ByRef strDesvaluta As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCodvalu = 0 Then
        strDesvaluta = ""
        Return True
      End If

      bOut = oCldList.ValCodiceDb(nCodvalu.ToString, strDittaCorrente, "TABVALU", "N", strDesvaluta)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127965883462031250, "Codice valuta |'" & nCodvalu.ToString & "'| inesistente")))
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
      oCldList.CaricaUmMis(strDittaCorrente, dttTmp)

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
  Public Overridable Function GetArticoUnMis(ByVal strCodartPass As String) As String
    '---------------------------------------
    'ritorna le unità di misura dell'articolo passato in input
    Try
      Return CType(oCleComm, CLELBMENU).TrovaArticoUnMis(strDittaCorrente, SettaVarValidazioni(strCodartPass))
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

  Public Overridable Function SettaVarValidazioni(ByVal strCodartPass As String) As String
    Try
      'Nel caso richiamato da BNMGARTV BNTCARTV
      'nella creazione di un articolo a 2-3 livelli con prezzi per 1'a variante
      'il listino viene salvato alla 1'a variante utilizzando la var strCodart
      'mentre deve essere validato come il root utilizzando la var strCodartRoot
      If strCodartRoot.Trim <> "" Then
        If strCodartPass = "D" Then
          Return "D"
        Else
          Return strCodartRoot
        End If
      Else
        Return strCodartPass
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return ""
      '--------------------------------------------------------------
    End Try
  End Function

End Class
