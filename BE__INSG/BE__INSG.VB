Imports System.Data
Imports NTSInformatica.CLN__STD


Public Class CLE__INSG
  Inherits CLE__BASN

  Public oCldInsg As CLD__INSG 'oggetto dal
  Public dsShared As DataSet

  Public bHasChanges As Boolean = False

  Dim strModuliApri As String
  Public bInternazionale As Boolean

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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
  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__INSG"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldInsg = CType(MyBase.ocldBase, CLD__INSG)
    oCldInsg.Init(oApp)
    Return True
  End Function
#End Region

  Public Overridable Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldInsg.LeggiTabellaSemplice(strDittaCorrente, "TABINSG", ds)
      If dReturn = False Then Return False

      RiempicolonneUnbound(ds)

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldInsg.SetTableDefaultValueFromDB("TABINSG", ds)

      SetDefaultValue(ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("TABINSG").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TABINSG").ColumnChanged, AddressOf AfterColUpdate

      bHasChanges = False

      memoModuli(strModuliApri)

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
  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldInsg.SalvaInsg(strDittaCorrente, dsShared)

      If bResult Then
        bHasChanges = False
      End If

      memoModuli(strModuliApri)

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
  Public Overridable Function TestPreSalva() As Boolean
    Try
      '-------------------------------------------------------------------------------------
      '--- Eseguo i controlli necessari prima di fare il salvataggio della riga
      '-------------------------------------------------------------------------------------
      Dim dtrTmp As DataRow() = dsShared.Tables("TABINSG").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      Dim i As Integer = 0
      'For i = 0 To dtrTmp.Length - 1
      '  If Not CheckInternazionale(dtrTmp(i)!tb_opz_1.ToString()) Then
      '    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128302747854038602, "Non è possibile selezionare un tipo di valuta diverso da:" & vbCrLf & vbCrLf & "   1] (indefinita)" & vbCrLf & "   2] EUR Euro" & vbCrLf & "   3] LIT Lire italiane")))
      '    Return False
      '  End If
      'Next

      If NTSCInt(dsShared.Tables("TABINSG").Rows(0)!tb_destdomf) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131038872835430411, "All'interno del tab 'Altro', il campo 'Domicilio Fiscale' deve essere diverso da 0.")))
        Return False
      End If
      If NTSCInt(dsShared.Tables("TABINSG").Rows(0)!tb_destsedel) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131038872835430419, "All'interno del tab 'Altro', il campo 'Residenza / Domicilio / Sede legale italiana' deve essere diverso da 0.")))
        Return False
      End If
      If NTSCInt(dsShared.Tables("TABINSG").Rows(0)!tb_destresan) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131038872835430418, "All'interno del tab 'Altro', il campo 'Residenza / Sede legale estera' deve essere diverso da 0.")))
        Return False
      End If
      If NTSCInt(dsShared.Tables("TABINSG").Rows(0)!tb_destcorr) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131038872835430416, "All'interno del tab 'Altro', il campo 'Luogo esercizio attività all'estero' deve essere diverso da 0.")))
        Return False
      End If


      For i = 0 To dtrTmp.Length - 1
        dtrTmp(i)!tb_codditcsc = NTSCStr(dtrTmp(i)!xx_codditcsc) & Mid(NTSCStr(dtrTmp(i)!tb_codditcsc), 2)
      Next

      checkModuli()

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
  Public Overridable Sub Nuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsShared.Tables("TABINSG").Rows.Add(dsShared.Tables("TABINSG").NewRow)
      bHasChanges = True

      Dim dtrTmp As DataRow() = dsShared.Tables("TABINSG").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      Dim i As Integer = 0
      For i = 0 To dtrTmp.Length - 1
        If Not bInternazionale Then
          dtrTmp(i)!tb_opz_1 = "E"
        Else
          dtrTmp(i)!tb_opz_1 = " "
        End If
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
  Public Overridable Function Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("TABINSG").Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      memoModuli(strModuliApri)
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      Try
        ds.Tables("TABINSG").Columns("codditt").DefaultValue = strDittaCorrente
        ds.Tables("TABINSG").Columns("tb_codditcsc").DefaultValue = "".PadLeft(12, " "c)
      Catch
        'non gestisco l'errore: viene scatenato solo sulle tabelle che non sono per ditta ...
      End Try

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
  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property

  Public Overridable Function DESMODULO(ByVal nModulo As Integer) As String
    DESMODULO = "......................................"
    Try
      Select Case nModulo
        Case 1 : DESMODULO = "Contabilità Generale/sempl."
        Case 2 : DESMODULO = "Ordini"
        Case 3 : DESMODULO = "Vendite"
        Case 4 : DESMODULO = "Provvigioni"
        Case 5 : DESMODULO = "Statistiche"
        Case 6 : DESMODULO = "Magazzino"
        Case 7 : DESMODULO = "Distinta Base"
        Case 8 : DESMODULO = "Contabilità Analitica"
        Case 9 : DESMODULO = "Cespiti"
        Case 10 : DESMODULO = "Verticale 1"
        Case 11 : DESMODULO = "Source Extender"
        Case 12 : DESMODULO = "Conai"
        Case 13 : DESMODULO = "Customer Service"
        Case 14 : DESMODULO = "Produzione"
        Case 15 : DESMODULO = "Import/Export dati"
        Case 16 : DESMODULO = "Richiesta d'acquisto"
        Case 17 : DESMODULO = "Tentata vendita"
        Case 18 : DESMODULO = "Project Management"
        Case 19 : DESMODULO = "Intrastat"
        Case 20 : DESMODULO = "Generazione PDF da documenti"
        Case 21 : DESMODULO = "Ritenute d'acconto"
        Case 22 : DESMODULO = "(a disposizione - 3)"
        Case 23 : DESMODULO = "(a disposizione - 4)"
        Case 24 : DESMODULO = "Tesoreria"
        Case 25 : DESMODULO = "(a disposizione - 5)"
        Case 26 : DESMODULO = "RSM"
        Case 27 : DESMODULO = "Configuratore di prodotto"
        Case 28 : DESMODULO = "Sistema Qualità"
        Case 29 : DESMODULO = "(a disposizione - 1)"
        Case 30 : DESMODULO = "(a disposizione - 2)"
        Case 31 : DESMODULO = "Configuratore su distinta neutra"
        Case 32 : DESMODULO = "Retail"
        Case 33 : DESMODULO = "Taglie e colori"
        Case 34 : DESMODULO = "Bilancio UE e nota integrativa"
        Case 35 : DESMODULO = "Black List"
        Case 36 : DESMODULO = "CRM"
        Case 37 : DESMODULO = "Picking"
        Case 38 : DESMODULO = "Parcellazione"
        Case 39 : DESMODULO = "Telematico Oper. Rilevanti Iva"
        Case 40 : DESMODULO = "Anagrafiche Generali"
        Case 41 : DESMODULO = "Contabilità generale EASY"
        Case 42 : DESMODULO = "Ordini EASY"
        Case 43 : DESMODULO = "Magazzino EASY"
        Case 44 : DESMODULO = "Contabilità  professionisti"
        Case 45 : DESMODULO = "F24 BUSINESS"
        Case 46 : DESMODULO = "Logistica Extended"
        Case 47 : DESMODULO = "Verticale 2"
        Case 48 : DESMODULO = "International"
        Case 49 : DESMODULO = "Non Solo Shop"
        Case 50 : DESMODULO = "Commesse light"
          'Case 51: DESMODULO = "Controllo di gestione"
          'Case 52: DESMODULO = "770"
          'Case 53: DESMODULO = "ex Assistenza Tecnica"
          'Case 54: DESMODULO = "740 + ICI + IRAP"
          'Case 55: DESMODULO = "750 + ICI + IRAP"
          'Case 56: DESMODULO = "760 + ICI + IRAP"
        Case 51 : DESMODULO = "Elenchi Iva Cli-for"
        Case 52 : DESMODULO = "Produzione Taglie colori"
        Case 55 : DESMODULO = "BUSNET Frame"
        Case 56 : DESMODULO = "Moduli Supplementari NTS"
        Case 57 : DESMODULO = "Data-Warehouse"
        Case 58 : DESMODULO = "(a disposizione - 6)"
        Case 59 : DESMODULO = "(a disposizione - 7)"
        Case 60 : DESMODULO = "Moduli Supplementari Partner"
        Case 101 : DESMODULO = "Inventari su palmare"
        Case 102 : DESMODULO = "Controllo ric. merce su palmare"
        Case 103 : DESMODULO = "Logistica su palmari"
          'Case 104 : DESMODULO = "Web-CRM"
          'Case 105 : DESMODULO = "Web Soft Service"
        Case 106 : DESMODULO = "E-mail integrata"
        Case 107 : DESMODULO = "Datawarehouse Extended"
        Case 108 : DESMODULO = "Contabilità Analitica DC"
        Case 109 : DESMODULO = "Budget e controllo di gestione"
        Case 110 : DESMODULO = "Desktop Consolle"
        Case 111 : DESMODULO = "Tesoreria e flussi finanziari"
        Case 112 : DESMODULO = "Tesoreria Extended"
        Case 113 : DESMODULO = "Gestione punti vendita"
        Case 114 : DESMODULO = "Gestione punti vendita extended"
        Case 115 : DESMODULO = "Gestione punti vendita recovery"
        Case 116 : DESMODULO = "Gestione inc/pag differiti"
        Case 118 : DESMODULO = "Collegamento a NetPro Classic"
        Case 119 : DESMODULO = "Bus4People Extented"
        Case 120 : DESMODULO = "Analisi Fattibilità Impegni"
        Case 121 : DESMODULO = "Pec-mail"
        Case 122 : DESMODULO = "Dichiarazione di intenti"
        Case 123 : DESMODULO = "Collegamento a e-Commerce"
        Case 124 : DESMODULO = "Business For People Guest Ext."
        Case 125 : DESMODULO = "Customer Service Map View"
        Case 126 : DESMODULO = "Contratti e Condizioni Commerciali"
        Case 128 : DESMODULO = "Crystal Reports 64bit server"
        Case 129 : DESMODULO = "Smart Business Framework"
        Case 131 : DESMODULO = "Gest. Mercati Ortofrutta"
        Case 135 : DESMODULO = "Analisi di bilancio"
        Case 201 To 230 : DESMODULO = RicercaNomePartner(nModulo)
        Case 231 To 260 : DESMODULO = RicercaNomePartner(nModulo)
      End Select
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

  Public Overridable Sub AssegnaNomiPartner(ByVal dttPtn As DataTable, ByVal dttPtnExt As DataTable)
    Dim strNomePartner As String
    Dim strRootOpzReg As String
    Dim strOpzReg As String
    Dim i As Integer

    Try
      strRootOpzReg = "NomeModuloPtn"
      dttPtn.Columns.Add("cod", GetType(String))
      For i = 1 To 30
        strOpzReg = strRootOpzReg & Format(i, "00")
        strNomePartner = NTSCStr(oCldInsg.GetSettingBus("OPZIONI", ".", ".", strOpzReg, "......................................", " ", "......................................"))

        dttPtn.Rows.Add(New Object() {strNomePartner})
        dttPtn.AcceptChanges()
      Next

      strRootOpzReg = "NomeModuloPtnExt"
      dttPtnExt.Columns.Add("cod", GetType(String))
      For i = 1 To 30
        strOpzReg = strRootOpzReg & Format(i, "00")
        strNomePartner = NTSCStr(oCldInsg.GetSettingBus("OPZIONI", ".", ".", strOpzReg, "......................................", " ", "......................................"))

        dttPtnExt.Rows.Add(New Object() {strNomePartner})
        dttPtnExt.AcceptChanges()
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
  Public Overridable Function RicercaNomePartner(ByVal nModulo As Integer) As String
    Dim strNomePartner As String
    Dim strRootOpzReg As String
    Dim strOpzReg As String
    RicercaNomePartner = "......................................"
    Try
      If nModulo >= 201 And nModulo <= 230 Then
        strRootOpzReg = "NomeModuloPtn"
        strOpzReg = strRootOpzReg & Format(nModulo - 200, "00")
        strNomePartner = NTSCStr(oCldInsg.GetSettingBus("OPZIONI", ".", ".", strOpzReg, "......................................", " ", "......................................"))
      ElseIf nModulo >= 231 And nModulo <= 260 Then
        strRootOpzReg = "NomeModuloPtnExt"
        strOpzReg = strRootOpzReg & Format(nModulo - 230, "00")
        strNomePartner = NTSCStr(oCldInsg.GetSettingBus("OPZIONI", ".", ".", strOpzReg, "......................................", " ", "......................................"))
      Else
        strNomePartner = "......................................"
      End If

      RicercaNomePartner = strNomePartner
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
  Public Overridable Function CheckInternazionale(ByVal strOpz As String) As Boolean
    Try
      If Not bInternazionale Then
        Select Case strOpz
          Case " ", "E", "L"
          Case Else
            Return False
        End Select
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

  Public Overridable Sub memoModuli(ByRef strModuli As String)
    Try
      strModuli = ""
      Dim dtrTmp As DataRow() = dsShared.Tables("TABINSG").Select()
      Dim i As Integer = 0
      For i = 0 To dtrTmp.Length - 1
        strModuli = strModuli & dtrTmp(i)!tb_mod_1.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_2.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_3.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_4.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_5.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_6.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_7.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_8.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_9.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_10.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_11.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_12.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_13.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_14.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod_15.ToString()

        strModuli = strModuli & dtrTmp(i)!tb_mod2_1.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_2.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_3.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_4.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_5.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_6.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_7.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_8.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_9.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_10.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_11.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_12.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_13.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_14.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod2_15.ToString()

        strModuli = strModuli & dtrTmp(i)!tb_mod3_1.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_2.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_3.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_4.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_5.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_6.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_7.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_8.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_9.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_10.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_11.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_12.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_13.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_14.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod3_15.ToString()

        strModuli = strModuli & dtrTmp(i)!tb_mod4_1.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_2.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_3.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_4.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_5.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_6.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_7.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_8.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_9.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_10.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_11.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_12.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_13.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_14.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_mod4_15.ToString()

        strModuli = strModuli & dtrTmp(i)!tb_modsup_1.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_2.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_3.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_4.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_5.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_6.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_7.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_8.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_9.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_10.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_11.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_12.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_13.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_14.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_15.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_16.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_17.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_18.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_19.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_20.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_21.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_22.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_23.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_24.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_25.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_26.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_27.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_28.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_29.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsup_30.ToString()

        strModuli = strModuli & dtrTmp(i)!tb_modsupext_1.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_2.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_3.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_4.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_5.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_6.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_7.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_8.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_9.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_10.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_11.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_12.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_13.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_14.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_15.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_16.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_17.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_18.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_19.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_20.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_21.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_22.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_23.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_24.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_25.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_26.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_27.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_28.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_29.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modsupext_30.ToString()

        strModuli = strModuli & dtrTmp(i)!tb_modptn_1.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_2.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_3.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_4.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_5.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_6.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_7.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_8.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_9.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_10.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_11.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_12.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_13.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_14.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_15.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_16.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_17.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_18.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_19.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_20.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_21.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_22.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_23.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_24.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_25.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_26.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_27.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_28.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_29.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptn_30.ToString()

        strModuli = strModuli & dtrTmp(i)!tb_modptnext_1.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_2.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_3.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_4.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_5.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_6.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_7.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_8.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_9.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_10.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_11.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_12.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_13.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_14.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_15.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_16.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_17.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_18.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_19.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_20.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_21.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_22.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_23.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_24.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_25.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_26.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_27.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_28.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_29.ToString()
        strModuli = strModuli & dtrTmp(i)!tb_modptnext_30.ToString()
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
  Public Overridable Sub checkModuli()
    Dim strModuliSalva As String = ""
    Try
      memoModuli(strModuliSalva)

      ControllaModuliAbilitati(strModuliApri, strModuliSalva)
      ControllaModuliDisabilitati(strModuliApri, strModuliSalva)

      oCldInsg.updateAnazmod40(Mid(strModuliSalva, 40, 1))

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

  Public Overridable Sub ControllaModuliAbilitati(ByVal strModuliA As String, ByVal strModuliS As String)
    Dim i As Integer
    Dim strSQL As String = ""
    Dim strSQLWhere As String = ""
    Try
      For i = 1 To 60
        If i <> 40 Then
          If Mid(strModuliS, i, 1) = "S" And Mid(strModuliA, i, 1) = "N" Then
            If MsgAdd(i) Then
              AddSQL(i, strSQL)
            End If
          End If
        End If
      Next
      For i = 61 To 120
        If Mid(strModuliS, i, 1) = "S" And Mid(strModuliA, i, 1) = "N" Then
          If MsgAdd(i + 40) Then
            AddSQL(i + 40, strSQL)
          End If
        End If
      Next
      For i = 121 To 180
        If Mid(strModuliS, i, 1) = "S" And Mid(strModuliA, i, 1) = "N" Then
          If MsgAdd(i + 80) Then
            AddSQL(i + 80, strSQL)
          End If
        End If
      Next

      If strSQL <> "" Then
        strSQLWhere = Left(strSQL, Len(strSQL) - 2)
        oCldInsg.updateAnazmodAbilitati(strSQLWhere)
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
  Public Overridable Sub ControllaModuliDisabilitati(ByVal strModuliA As String, ByVal strModuliS As String)
    Dim i As Integer
    Dim strSQL As String = ""
    Dim strSQLWhere As String = ""
    Try
      For i = 1 To 60
        If i <> 40 Then
          If Mid(strModuliS, i, 1) = "N" And Mid(strModuliA, i, 1) = "S" Then
            If Msg(i) Then
              AddSQL(i, strSQL)
            End If
          End If
        End If
      Next
      For i = 61 To 120
        If Mid(strModuliS, i, 1) = "N" And Mid(strModuliA, i, 1) = "S" Then
          If Msg(i + 40) Then
            AddSQL(i + 40, strSQL)
          End If
        End If
      Next
      For i = 121 To 180
        If Mid(strModuliS, i, 1) = "N" And Mid(strModuliA, i, 1) = "S" Then
          If Msg(i + 80) Then
            AddSQL(i + 80, strSQL)
          End If
        End If
      Next

      If strSQL <> "" Then
        strSQLWhere = Left(strSQL, Len(strSQL) - 2)
        oCldInsg.updateAnazmodDisabilitati(strSQLWhere)
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

  Public Overridable Function MsgAdd(ByVal nModulo As Integer) As Boolean
    Dim ev As NTSEventArgs
    Try
      ev = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128303433659357134, "ATTENZIONE" & vbCrLf & _
             "Si è scelto di abilitare, a livello di database-azienda," & vbCrLf & _
             "il modulo '|" & DESMODULO(nModulo) & "|'." & vbCrLf & _
             "Abilitarlo anche su tutte le ditte facenti parte del database-azienda stesso?"))
      ThrowRemoteEvent(ev)
      If (ev.RetValue = "NO") Then
        Return False
      Else
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
  Public Overridable Function Msg(ByVal nModulo As Integer) As Boolean
    Dim ev As NTSEventArgs
    Try
      ev = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128303433696068364, "ATTENZIONE" & vbCrLf & _
               "Si è scelto di disabilitare, a livello di database-azienda," & vbCrLf & _
               "il modulo '|" & DESMODULO(nModulo) & "|'." & vbCrLf & _
               "Disabilitarlo anche su tutte le ditte facenti parte del database-azienda stesso?"))
      ThrowRemoteEvent(ev)
      If (ev.RetValue = "NO") Then
        Return False
      Else
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

  Public Overridable Sub AddSQL(ByVal nModulo As Integer, ByRef strSQL As String)
    Try
      strSQL = strSQL & nModulo & ", "
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

  Public Overridable Sub DisabilitaSupl()
    Try
      Dim dtrTmp As DataRow() = dsShared.Tables("TABINSG").Select()
      Dim i As Integer = 0
      For i = 0 To dtrTmp.Length - 1
        dtrTmp(i)!tb_modsup_1 = "N"
        dtrTmp(i)!tb_modsup_2 = "N"
        dtrTmp(i)!tb_modsup_3 = "N"
        dtrTmp(i)!tb_modsup_4 = "N"
        dtrTmp(i)!tb_modsup_5 = "N"
        dtrTmp(i)!tb_modsup_6 = "N"
        dtrTmp(i)!tb_modsup_7 = "N"
        dtrTmp(i)!tb_modsup_8 = "N"
        dtrTmp(i)!tb_modsup_9 = "N"
        dtrTmp(i)!tb_modsup_10 = "N"
        dtrTmp(i)!tb_modsup_11 = "N"
        dtrTmp(i)!tb_modsup_12 = "N"
        dtrTmp(i)!tb_modsup_13 = "N"
        dtrTmp(i)!tb_modsup_14 = "N"
        dtrTmp(i)!tb_modsup_15 = "N"
        dtrTmp(i)!tb_modsup_16 = "N"
        dtrTmp(i)!tb_modsup_17 = "N"
        dtrTmp(i)!tb_modsup_18 = "N"
        dtrTmp(i)!tb_modsup_19 = "N"
        dtrTmp(i)!tb_modsup_20 = "N"
        dtrTmp(i)!tb_modsup_21 = "N"
        dtrTmp(i)!tb_modsup_22 = "N"
        dtrTmp(i)!tb_modsup_23 = "N"
        dtrTmp(i)!tb_modsup_24 = "N"
        dtrTmp(i)!tb_modsup_25 = "N"
        dtrTmp(i)!tb_modsup_26 = "N"
        dtrTmp(i)!tb_modsup_27 = "N"
        dtrTmp(i)!tb_modsup_28 = "N"
        dtrTmp(i)!tb_modsup_29 = "N"
        dtrTmp(i)!tb_modsup_30 = "N"

        dtrTmp(i)!tb_modsupext_1 = "N"
        dtrTmp(i)!tb_modsupext_2 = "N"
        dtrTmp(i)!tb_modsupext_3 = "N"
        dtrTmp(i)!tb_modsupext_4 = "N"
        dtrTmp(i)!tb_modsupext_5 = "N"
        dtrTmp(i)!tb_modsupext_6 = "N"
        dtrTmp(i)!tb_modsupext_7 = "N"
        dtrTmp(i)!tb_modsupext_8 = "N"
        dtrTmp(i)!tb_modsupext_9 = "N"
        dtrTmp(i)!tb_modsupext_10 = "N"
        dtrTmp(i)!tb_modsupext_11 = "N"
        dtrTmp(i)!tb_modsupext_12 = "N"
        dtrTmp(i)!tb_modsupext_13 = "N"
        dtrTmp(i)!tb_modsupext_14 = "N"
        dtrTmp(i)!tb_modsupext_15 = "N"
        dtrTmp(i)!tb_modsupext_16 = "N"
        dtrTmp(i)!tb_modsupext_17 = "N"
        dtrTmp(i)!tb_modsupext_18 = "N"
        dtrTmp(i)!tb_modsupext_19 = "N"
        dtrTmp(i)!tb_modsupext_20 = "N"
        dtrTmp(i)!tb_modsupext_21 = "N"
        dtrTmp(i)!tb_modsupext_22 = "N"
        dtrTmp(i)!tb_modsupext_23 = "N"
        dtrTmp(i)!tb_modsupext_24 = "N"
        dtrTmp(i)!tb_modsupext_25 = "N"
        dtrTmp(i)!tb_modsupext_26 = "N"
        dtrTmp(i)!tb_modsupext_27 = "N"
        dtrTmp(i)!tb_modsupext_28 = "N"
        dtrTmp(i)!tb_modsupext_29 = "N"
        dtrTmp(i)!tb_modsupext_30 = "N"
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
  Public Overridable Sub DisabilitaPartner()
    Try
      Dim dtrTmp As DataRow() = dsShared.Tables("TABINSG").Select()
      Dim i As Integer = 0
      For i = 0 To dtrTmp.Length - 1
        dtrTmp(i)!tb_modptn_1 = "N"
        dtrTmp(i)!tb_modptn_2 = "N"
        dtrTmp(i)!tb_modptn_3 = "N"
        dtrTmp(i)!tb_modptn_4 = "N"
        dtrTmp(i)!tb_modptn_5 = "N"
        dtrTmp(i)!tb_modptn_6 = "N"
        dtrTmp(i)!tb_modptn_7 = "N"
        dtrTmp(i)!tb_modptn_8 = "N"
        dtrTmp(i)!tb_modptn_9 = "N"
        dtrTmp(i)!tb_modptn_10 = "N"
        dtrTmp(i)!tb_modptn_11 = "N"
        dtrTmp(i)!tb_modptn_12 = "N"
        dtrTmp(i)!tb_modptn_13 = "N"
        dtrTmp(i)!tb_modptn_14 = "N"
        dtrTmp(i)!tb_modptn_15 = "N"
        dtrTmp(i)!tb_modptn_16 = "N"
        dtrTmp(i)!tb_modptn_17 = "N"
        dtrTmp(i)!tb_modptn_18 = "N"
        dtrTmp(i)!tb_modptn_19 = "N"
        dtrTmp(i)!tb_modptn_20 = "N"
        dtrTmp(i)!tb_modptn_21 = "N"
        dtrTmp(i)!tb_modptn_22 = "N"
        dtrTmp(i)!tb_modptn_23 = "N"
        dtrTmp(i)!tb_modptn_24 = "N"
        dtrTmp(i)!tb_modptn_25 = "N"
        dtrTmp(i)!tb_modptn_26 = "N"
        dtrTmp(i)!tb_modptn_27 = "N"
        dtrTmp(i)!tb_modptn_28 = "N"
        dtrTmp(i)!tb_modptn_29 = "N"
        dtrTmp(i)!tb_modptn_30 = "N"

        dtrTmp(i)!tb_modptnext_1 = "N"
        dtrTmp(i)!tb_modptnext_2 = "N"
        dtrTmp(i)!tb_modptnext_3 = "N"
        dtrTmp(i)!tb_modptnext_4 = "N"
        dtrTmp(i)!tb_modptnext_5 = "N"
        dtrTmp(i)!tb_modptnext_6 = "N"
        dtrTmp(i)!tb_modptnext_7 = "N"
        dtrTmp(i)!tb_modptnext_8 = "N"
        dtrTmp(i)!tb_modptnext_9 = "N"
        dtrTmp(i)!tb_modptnext_10 = "N"
        dtrTmp(i)!tb_modptnext_11 = "N"
        dtrTmp(i)!tb_modptnext_12 = "N"
        dtrTmp(i)!tb_modptnext_13 = "N"
        dtrTmp(i)!tb_modptnext_14 = "N"
        dtrTmp(i)!tb_modptnext_15 = "N"
        dtrTmp(i)!tb_modptnext_16 = "N"
        dtrTmp(i)!tb_modptnext_17 = "N"
        dtrTmp(i)!tb_modptnext_18 = "N"
        dtrTmp(i)!tb_modptnext_19 = "N"
        dtrTmp(i)!tb_modptnext_20 = "N"
        dtrTmp(i)!tb_modptnext_21 = "N"
        dtrTmp(i)!tb_modptnext_22 = "N"
        dtrTmp(i)!tb_modptnext_23 = "N"
        dtrTmp(i)!tb_modptnext_24 = "N"
        dtrTmp(i)!tb_modptnext_25 = "N"
        dtrTmp(i)!tb_modptnext_26 = "N"
        dtrTmp(i)!tb_modptnext_27 = "N"
        dtrTmp(i)!tb_modptnext_28 = "N"
        dtrTmp(i)!tb_modptnext_29 = "N"
        dtrTmp(i)!tb_modptnext_30 = "N"
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

  Public Overridable Sub GetModuliAcq(ByRef strModuliAcq As String)
    Dim strModuli As String = ""
    Dim strModuliExt As String = ""
    Dim strModuliSup As String = ""
    Dim strModuliSupExt As String = ""
    Dim strModuliPtn As String = ""
    Dim strModuliPtnExt As String = ""
    Try
      strModuliAcq = ""

      strModuli = NTSCStr(GetSettingReg("Business", oApp.Profilo & "\ActKey", "Moduli", ""))
      strModuliExt = NTSCStr(GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliExt", ""))
      strModuliSup = NTSCStr(GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliSup", ""))
      strModuliSupExt = NTSCStr(GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliSupExt", ""))
      strModuliPtn = NTSCStr(GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliPtn", ""))
      strModuliPtnExt = NTSCStr(GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliPtnExt", ""))

      strModuliAcq = strModuli & strModuliExt & _
                     strModuliSup & strModuliSupExt & _
                     strModuliPtn & strModuliPtnExt

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

  Public Overridable Function OttieniDesModuloAcq(ByVal i As Integer) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case i
        Case 61 To 120 : Return DESMODULO(i + 40)
        Case 121 To 180 : Return DESMODULO(i + 80)
        Case Else : Return DESMODULO(i)
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function RiempicolonneUnbound(ByRef ds As DataSet) As Boolean
    Dim i As Integer
    Try
      ds.Tables("TABINSG").Columns.Add("xx_codditcsc", GetType(String))
      For i = 0 To ds.Tables("TABINSG").Rows.Count - 1
        ds.Tables("TABINSG").Rows(i)!xx_codditcsc = Mid(NTSCStr(ds.Tables("TABINSG").Rows(i)!tb_codditcsc), 1, 1)
        If NTSCInt(ds.Tables("TABINSG").Rows(i)!tb_maxlencodart) = 0 Then ds.Tables("TABINSG").Rows(i)!tb_maxlencodart = 18
      Next
      ds.Tables("TABINSG").AcceptChanges()

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

  Public Overridable Function VerificaLenSerie(ByVal lSerieLen As Integer) As Boolean
    Try
      Return oCldInsg.VerificaLenSerie(lSerieLen)
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
  Public Overridable Function VerificaLenCodart(ByVal lSerieCodart As Integer) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldInsg.VerificaLenCodart(lSerieCodart)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function AggiornanDecSuPrzNum(ByVal nValore As Integer) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldInsg.AggiornanDecSuPrzNum(nValore)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function


  Public Overridable Function GetMaxCodartLen() As Integer
    Try

      Return oCldInsg.GetMaxCodartLen

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
