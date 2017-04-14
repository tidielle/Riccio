Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports System.IO.File

Public Class CLE__GREG
  Inherits CLE__BASN


  Public oCldGreg As CLD__GREG 'oggetto dal
  Public dsShared As DataSet

  Public bHasChanges As Boolean = False


  '-------------------------------------------------------------------------------------------
  '--- Variabili per l'eventuale scrittura in ACTLOG
  '-------------------------------------------------------------------------------------------
  Public bOkActlog As Boolean
  Public strLiv1Tmp As String
  Public strLiv2Tmp As String
  Public strLiv3Tmp As String
  Public strIdutTmp As String
  Public strIdazTmp As String
  Public strIdtdocTmp As String
  Public strNompropTmp As String
  Public strValpropTmp As String
  Public strSqlWhere As String
  Public strDesogglog As String
  Public strTipoop As String
  Public strTipoogg As String
  '-------------------------------------------------------------------------------------------

  Public bEsporta As Boolean
  Public strNomeUt As String
  Public strNomeAz As String
  Public strLbPath As String
  Public strCaption As String
  Public strLiv(0 To 2) As String

  Public bCkDip0 As Boolean
  Public bCkDip1 As Boolean
  Public bCkDip2 As Boolean

  Public strGregNomOp As String
  Public strGregCodAz As String
  Public strGregTipoDoc As String

  Public bNoSost As Boolean

  Public strFullPathNewRow As String
  Public bckDip0NewRow As Boolean
  Public bckDip1NewRow As Boolean
  Public bckDip2NewRow As Boolean
  Public stredCode0NewRow As String
  Public stredCode1NewRow As String
  Public stredCode2NewRow As String

  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public strChild As String = ""                              'diverso da '' se bs--greg chiamato con ctrl+g da bn__chil

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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__GREG"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldGreg = CType(MyBase.ocldBase, CLD__GREG)
    oCldGreg.Init(oApp)
    Return True
  End Function

  Public Overridable Function Apri(ByVal strDitta As String, ByVal strFoglie() As String, ByVal strDip() As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldGreg.GetGrigliaRegProp(strFoglie, strDip, ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldGreg.SetTableDefaultValueFromDB("REGPROP", "REGPROP", ds, False)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("REGPROP").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("REGPROP").ColumnChanged, AddressOf AfterColUpdate

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
  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldGreg.ScriviTabellaSemplice(strDittaCorrente, "REGPROP", dsShared.Tables("REGPROP"), "", "", "")

      'se gestisco le tabelle cache devo aggiornare la data di aggiornamento tabelle cache, per farle rileggere dai vari client di business
      If oApp.bDSShared Then
        oCldGreg.DBCacheAggDBCacheDate("BS__GREG")
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
  Public Overridable Function TestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim ds As DataSet = Nothing
    Dim strRp_liv1 As String
    Dim strRp_liv2 As String
    Dim strRp_liv3 As String
    Dim strRp_idut As String
    Dim strRp_idaz As String
    Dim strRp_idtdoc As String
    Dim strRp_nomprop As String
    Dim j As Integer = 0
    Dim dsProp As DataSet = Nothing
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsShared.Tables("REGPROP").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        strTmp = NTSCStr(dtrCurrRow(i)!rp_nomprop)
        If Len(strTmp) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128332815729590051, "Il campo 'Proprietà' non può essere vuoto.")))
          Return False
        End If

        'Mette uno spazio se le colonne sono vuote
        If Len(NTSCStr(dtrCurrRow(i)!rp_valprop)) = 0 Then dtrCurrRow(i)!rp_valprop = " "

        If Len(NTSCStr(dtrCurrRow(i)!rp_idut)) = 0 Then dtrCurrRow(i)!rp_idut = " "
        If Len(NTSCStr(dtrCurrRow(i)!rp_idaz)) = 0 Then dtrCurrRow(i)!rp_idaz = " "
        If Len(NTSCStr(dtrCurrRow(i)!rp_idtdoc)) = 0 Then dtrCurrRow(i)!rp_idtdoc = " "

        If Not Len(NTSCStr(dtrCurrRow(i)!rp_idut)) = 0 Then dtrCurrRow(i)!rp_idut = NTSCStr(dtrCurrRow(i)!rp_idut).ToUpper
        If Not Len(NTSCStr(dtrCurrRow(i)!rp_idaz)) = 0 Then dtrCurrRow(i)!rp_idaz = NTSCStr(dtrCurrRow(i)!rp_idaz).ToUpper
        If Not Len(NTSCStr(dtrCurrRow(i)!rp_idtdoc)) = 0 Then dtrCurrRow(i)!rp_idtdoc = NTSCStr(dtrCurrRow(i)!rp_idtdoc).ToUpper

        'Controllo indicazione dipendenza utente
        If bCkDip0 = True Then
          If NTSCStr(dtrCurrRow(i)!rp_idut) = " " Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128332829353844423, "Obbligatorio indicare l'identificatore operatore in quanto la cartella possiede tale dipendenza impostata.")))
            Return False
          End If
        Else
          If NTSCStr(dtrCurrRow(i)!rp_idut) <> " " Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128332829376481533, "Impossibile indicare l'identificatore operatore quando la cartella non è dipendente da operatore.")))
            Return False
          End If
        End If
        'Controllo indicazione dipendenza azienda
        If bCkDip1 = True Then
          If NTSCStr(dtrCurrRow(i)!rp_idaz) = " " Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128332829401460413, "Obbligatorio indicare l'identificatore azienda/ditta in quanto la cartella possiede tale dipendenza impostata.")))
            Return False
          End If
        Else
          If NTSCStr(dtrCurrRow(i)!rp_idaz) <> " " Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128332829424878113, "Impossibile indicare l'identificatore azienda/ditta quando la cartella non è dipendente da azienda/ditta.")))
            Return False
          End If
        End If
        'Controllo indicazione dipendenza documento
        If bCkDip2 = True Then
          If NTSCStr(dtrCurrRow(i)!rp_idtdoc) = " " Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128332829720721723, "Obbligatorio indicare l'identificatore documento in quanto la cartella possiede tale dipendenza impostata.")))
            Return False
          End If
        Else
          If NTSCStr(dtrCurrRow(i)!rp_idtdoc) <> " " Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128332830221392149, "Impossibile indicare l'identificatore documento quando la cartella non è dipendente da documento.")))
            Return False
          End If
        End If
        '-----------------------------------------------------------------------------------------
        '--- Controlla se esiste già una riga con quella chiave
        '-----------------------------------------------------------------------------------------
        If dtrCurrRow(i).RowState = DataRowState.Added Then
          strRp_liv1 = NTSCStr(dtrCurrRow(i)!rp_liv1)
          strRp_liv2 = NTSCStr(dtrCurrRow(i)!rp_liv2)
          strRp_liv3 = NTSCStr(dtrCurrRow(i)!rp_liv3)
          strRp_idut = NTSCStr(dtrCurrRow(i)!rp_idut)
          strRp_idaz = NTSCStr(dtrCurrRow(i)!rp_idaz)
          strRp_idtdoc = NTSCStr(dtrCurrRow(i)!rp_idtdoc)
          strRp_nomprop = NTSCStr(dtrCurrRow(i)!rp_nomprop)

          oCldGreg.ControllaChiave(strRp_liv1, strRp_liv2, strRp_liv3, strRp_idut, strRp_idaz, strRp_idtdoc, strRp_nomprop, ds)

          If ds.Tables("REGPROP").Rows.Count > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128332833160301151, "Proprietà già esistente.")))
            Return False
          End If
        End If

        '-----------------------------------------------------------------------------------------
        '--- Salva in variabili temporanee il valore della chiave per un'eventuale scrittura
        '--- in ACTLOG, se attiva l'opzione di registro relativa
        '-----------------------------------------------------------------------------------------
        If dtrCurrRow(i).RowState <> DataRowState.Added Then
          strRp_liv1 = NTSCStr(dtrCurrRow(i)!rp_liv1)
          strRp_liv2 = NTSCStr(dtrCurrRow(i)!rp_liv2)
          strRp_liv3 = NTSCStr(dtrCurrRow(i)!rp_liv3)
          strRp_idut = NTSCStr(dtrCurrRow(i)!rp_idut)
          strRp_idaz = NTSCStr(dtrCurrRow(i)!rp_idaz)
          strRp_idtdoc = NTSCStr(dtrCurrRow(i)!rp_idtdoc)
          strRp_nomprop = NTSCStr(dtrCurrRow(i)!rp_nomprop)
          oCldGreg.GetRegPropRiga(strRp_liv1, strRp_liv2, strRp_liv3, strRp_idut, strRp_idaz, strRp_idtdoc, strRp_nomprop, dsProp)

          For j = 0 To dsProp.Tables("REGPROP").Rows.Count - 1
            With dsProp.Tables("REGPROP").Rows(j)
              strLiv1Tmp = NTSCStr(!rp_liv1)
              strLiv2Tmp = NTSCStr(!rp_liv2)
              strLiv3Tmp = NTSCStr(!rp_liv3)
              strIdutTmp = NTSCStr(!rp_idut)
              strIdazTmp = NTSCStr(!rp_idaz)
              strIdtdocTmp = NTSCStr(!rp_idtdoc)
              strNompropTmp = NTSCStr(!rp_nomprop)
            End With
            strSqlWhere = CStrSQL(strLiv1Tmp) & "," & CStrSQL(strLiv2Tmp) & "," & _
              CStrSQL(strLiv3Tmp) & "," & CStrSQL(strIdutTmp) & "," & _
              CStrSQL(strIdazTmp) & "," & CStrSQL(strIdtdocTmp) & "," & _
              CStrSQL(strNompropTmp)
            strDesogglog = "Modifica impostazioni registro di Business " & strLiv1Tmp
            If strLiv2Tmp <> "." Then strDesogglog = strDesogglog & "\" & strLiv2Tmp
            If strLiv3Tmp <> "." Then strDesogglog = strDesogglog & "\" & strLiv3Tmp
            strTipoop = "M"
            strTipoogg = "D"
            ScriviLog()
          Next

        End If
        '-----------------------------------------------------------------------------------------
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
      Return False
    End Try
  End Function
  Public Overridable Function Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("REGPROP").Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Sub Nuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsShared.Tables("PARSTAG").Rows.Add(dsShared.Tables("PARSTAG").NewRow)
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
  Public Overridable Sub OnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strCampi(0 To 2) As String
    Dim strPathCor As String
    Try
      ThrowRemoteEvent(New NTSEventArgs("NUOVARIGA:", ""))

      strPathCor = strFullPathNewRow
      EstraiDaPath(strPathCor, strCampi)

      e.Row!rp_liv1 = strCampi(0)
      e.Row!rp_liv2 = strCampi(1)
      e.Row!rp_liv3 = strCampi(2)

      If bckDip0NewRow = False Then
        e.Row!rp_idut = " "
      Else
        If stredCode0NewRow = "*" Then
          e.Row!rp_idut = " "
        Else
          e.Row!rp_idut = stredCode0NewRow
        End If
      End If
      If bckDip1NewRow = False Then
        e.Row!rp_idaz = " "
      Else
        If stredCode1NewRow = "*" Then
          e.Row!rp_idaz = " "
        Else
          e.Row!rp_idaz = stredCode1NewRow
        End If
      End If
      If bckDip2NewRow = False Then
        e.Row!rp_idtdoc = " "
      Else
        If stredCode2NewRow = "*" Then
          e.Row!rp_idtdoc = " "
        Else
          e.Row!rp_idtdoc = stredCode2NewRow
        End If
      End If
      If NTSCStr(e.Row!rp_valprop) = "" Then e.Row!rp_valprop = " "

      e.Row!xx_seleziona = "N"

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
      'se non  compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName = "rp_nomprop" Then
        If NTSCStr(e.Row!rp_nomprop) = "" Then OnAddNewRow(sender, e)
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

      If e.Column.ColumnName <> "xx_seleziona" Then bHasChanges = True

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
  Public Overridable Sub BeforeColUpdate_rp_nomprop(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dsNomprop As DataSet = Nothing
    Try
      If e.ProposedValue.ToString <> "" Then
        oCldGreg.GetNomprop(NTSCStr(e.Row!rp_liv1), NTSCStr(e.Row!rp_liv2), NTSCStr(e.Row!rp_liv3), _
        NTSCStr(e.ProposedValue.ToString), dsNomprop)

        If dsNomprop.Tables("NOMPROP").Rows.Count > 0 Then
          e.Row!rp_valprop = NTSCStr(dsNomprop.Tables("NOMPROP").Rows(0)!np_valdef)
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
  Public Overridable Sub BeforeColUpdate_rp_valprop(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If (e.ProposedValue.ToString.Trim <> "") And (Microsoft.VisualBasic.Left(e.ProposedValue.ToString, 1) = " ") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129144289403683215, "ATTENZIONE!" & vbCrLf & _
          "Il valore inserito nella colonna 'Valore' inizia con uno spazio.")))
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property

  Public Overridable Function GetRegProp(ByVal strCampi() As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldGreg.GetRegProp(strCampi, ds)
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
  Public Overridable Function DeleteRegprop(ByVal strCampi() As String) As Boolean
    Try
      Return oCldGreg.DeleteRegprop(strCampi)
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

  Public Overridable Sub ScriviLog()
    Try
      bOkActlog = oCldGreg.ScriviActLog(strDittaCorrente, "BS--GREG", "regprop", "regprop", strSqlWhere, strTipoop, strTipoogg, strDesogglog, True)
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
  Public Overridable Sub LogDelete(ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strliv3 As String, _
        ByVal strIdut As String, ByVal strIdaz As String, ByVal strIdtdoc As String, _
        ByVal strNomprop As String)
    Try
      SalvaVariabili(strLiv1, strLiv2, strliv3, strIdut, strIdaz, strIdtdoc, strNomprop)
      strSqlWhere = CStrSQL(strLiv1Tmp) & "," & CStrSQL(strLiv2Tmp) & "," & _
        CStrSQL(strLiv3Tmp) & "," & CStrSQL(strIdutTmp) & "," & _
        CStrSQL(strIdazTmp) & "," & CStrSQL(strIdtdocTmp) & "," & _
        CStrSQL(strNompropTmp)
      strDesogglog = "Cancellazione impostazioni registro di Business " & strLiv1Tmp
      If strLiv2Tmp <> "." Then strDesogglog = strDesogglog & "\" & strLiv2Tmp
      If strLiv3Tmp <> "." Then strDesogglog = strDesogglog & "\" & strLiv3Tmp
      strTipoop = "A"
      strTipoogg = "D"
      ScriviLog()
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

  Public Overridable Sub EstraiDaPath(ByVal strPercorso As String, ByRef strValori() As String)
    Dim nIndice As Integer
    Dim strTmp() As String

    Try
      '--------------------------------------------------------------------------------------------------------------
      If strPercorso = "BUSINESS" Then
        For nIndice = 0 To 2
          strValori(nIndice) = "."
        Next
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      strPercorso = Mid(strPercorso, 10)
      '--------------------------------------------------------------------------------------------------------------
      If InStr(strPercorso, "\") = 0 Then
        strValori(0) = strPercorso
        For nIndice = 1 To 2
          strValori(nIndice) = "."
        Next
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      strTmp = strPercorso.Split("\"c)
      For nIndice = 0 To (strTmp.Length - 1)
        strValori(nIndice) = strTmp(nIndice)
      Next
      '--------------------------------------------------------------------------------------------------------------
      If strPercorso.Length = 3 Then Return
      '--------------------------------------------------------------------------------------------------------------
      For nIndice = (strTmp.Length) To 2
        strValori(nIndice) = "."
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub GuardaDip(ByVal strSiNo() As String, ByVal strDati() As String, ByVal bckDipCheck0 As Boolean, ByVal bckDipCheck1 As Boolean, ByVal bckDipCheck2 As Boolean, ByVal bckDipEnab0 As Boolean, ByVal bckDipEnab1 As Boolean, ByVal bckDipEnab2 As Boolean, ByVal strEdCode0 As String, ByVal strEdCode1 As String, ByVal strEdCode2 As String)
    Dim nIndice As Integer
    Dim strEdCode As String = ""
    Dim bckDipCheck As Boolean
    Dim bckDipEnab As Boolean
    Try
      For nIndice = 0 To 2
        Select Case nIndice
          Case 0
            bckDipCheck = bckDipCheck0
          Case 1
            bckDipCheck = bckDipCheck1
          Case 2
            bckDipCheck = bckDipCheck2
        End Select
        Select Case nIndice
          Case 0
            bckDipEnab = bckDipEnab0
          Case 1
            bckDipEnab = bckDipEnab1
          Case 2
            bckDipEnab = bckDipEnab2
        End Select
        If bckDipEnab Then
          If bckDipCheck = True Then
            strSiNo(nIndice) = "S"
            Select Case nIndice
              Case 0
                strEdCode = strEdCode0
              Case 1
                strEdCode = strEdCode1
              Case 2
                strEdCode = strEdCode2
            End Select
            strDati(nIndice) = strEdCode
          Else
            strSiNo(nIndice) = "N"
            strDati(nIndice) = " "
          End If
        Else
          strDati(nIndice) = ""
          If bckDipCheck = True Then
            strSiNo(nIndice) = "S"
          Else
            strSiNo(nIndice) = "N"
          End If
        End If
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CreaRoot() As Boolean
    Try
      Return oCldGreg.CreaRoot()
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
  Public Overridable Function FileApri(ByVal strCampi() As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldGreg.FileApri(strCampi, ds, strChild)
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
  Public Overridable Function RinominaCartella(ByVal strCampi() As String, ByVal strNewText As String, ByVal nCont As Integer) As Boolean
    Try
      Return oCldGreg.RinominaCartella(strCampi, strNewText, nCont)
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
  Public Overridable Function ApriRamo(ByVal strCampi() As String, ByVal nLivello As Integer, ByRef ds As DataSet) As Boolean
    Try
      Return oCldGreg.ApriRamo(strCampi, nLivello, strChild, ds)
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

  Public Overridable Function CopiaCartella(ByVal nLivDa As Integer, ByVal strCopy() As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldGreg.CopiaCartella(nLivDa, strCopy, ds)
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

  Public Overridable Function IncollaCartellaSelectRegdir(ByVal strCur() As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldGreg.IncollaCartellaSelectRegdir(strCur, ds)
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
  Public Overridable Function IncollaCartellaInsertIntoRegDir(ByVal strCur() As String, ByVal strRd_diput As String, ByVal strRd_dipaz As String, ByVal strRd_diptdoc As String) As Boolean
    Try
      Return oCldGreg.IncollaCartellaInsertIntoRegDir(strCur, strRd_diput, strRd_dipaz, strRd_diptdoc)
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
  Public Overridable Function IncollaCartellaSelectRegprop(ByVal strDip() As String, ByVal strRd_liv1 As String, ByVal strRd_liv2 As String, ByVal strRd_liv3 As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldGreg.IncollaCartellaSelectRegprop(strDip, strRd_liv1, strRd_liv2, strRd_liv3, ds)
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
  Public Overridable Function IncollaCartellaInsertIntoRegprop(ByVal strCur() As String, ByVal newDip() As String, ByVal strRp_nomprop As String, ByVal strRp_valprop As String) As Boolean
    Try
      Return oCldGreg.IncollaCartellaInsertIntoRegprop(strCur, newDip, strRp_nomprop, strRp_valprop)
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
  Public Overridable Function IncollaCartellaUpdate(ByVal strCur() As String, ByVal strRp_nomprop As String, ByVal strRp_valprop As String) As Boolean
    Try
      Return oCldGreg.IncollaCartellaUpdate(strCur, strRp_nomprop, strRp_valprop)
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

  Public Overridable Function FileAggiornaSelectRegdir(ByVal strCampi() As String, ByRef ds As DataSet) As Boolean
    Try
      Return oCldGreg.FileAggiornaSelectRegdir(strCampi, ds)
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
  Public Overridable Function FileAggiornaInsertIntoRegDir(ByVal strCampi() As String, ByVal strDipend() As String) As Boolean
    Try
      Return oCldGreg.FileAggiornaInsertIntoRegDir(strCampi, strDipend)
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
  Public Overridable Function FileAggiornaUpdateRegDir(ByVal strCampi() As String, ByVal strDipend() As String) As Boolean
    Try
      Return oCldGreg.FileAggiornaUpdateRegDir(strCampi, strDipend)
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

#Region "funzioni specifiche per BN__GREG.BN__IMEX"
  Public Overridable Sub Esportazione(ByVal bSingolo As Boolean, ByVal bDipOperatore0 As Boolean, ByVal bDipOperatore1 As Boolean, _
          ByVal bDipAzienda0 As Boolean, ByVal bDipAzienda1 As Boolean, _
          ByVal bImEx0 As Boolean, ByVal bImEx1 As Boolean, ByVal bImEx2 As Boolean, ByVal bImEx3 As Boolean, ByVal bImEx4 As Boolean, _
          ByVal bImEx5 As Boolean, ByVal bImEx6 As Boolean, ByVal bImEx7 As Boolean, ByVal bImEx8 As Boolean, ByVal bImEx9 As Boolean, _
          ByVal strUtenteExp As String, ByVal strAziendaExp As String, ByVal strFile As String)

    Dim dsGregDir As DataSet = Nothing
    Dim dsGregProp As DataSet = Nothing
    Dim i As Integer
    Dim j As Integer
    Dim lw1 As StreamWriter = Nothing
    Dim evt As NTSEventArgs = Nothing
    Dim strNomeFile As String
    Dim strRiga As String = ""
    Try
      strNomeFile = strFile
      If Exists(strNomeFile) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128333740421903437, "File già esistente! Scegliere altro nome!")))
        bNoSost = True
        Exit Sub
      End If

      lw1 = New StreamWriter(strNomeFile)

      oCldGreg.EsportazioneRegDir(strLiv, bSingolo, dsGregDir)

      'Inizia a scrivere il file di esportazione
      For i = 0 To dsGregDir.Tables("REGDIR").Rows.Count - 1
        'Nel caso di dipendenza dall'utente, a fianco del percorso
        'viene anche indicato il corrispondente nome

        'Controllo se devo esportare l'operatore singolo
        If NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_diput) = "S" And (bDipOperatore0 = False) Then
          GoTo Continua
        End If

        If NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_diput) <> "S" And (bDipOperatore1 = False) Then
          GoTo Continua
        End If

        'Controllo se devo esportare l'azienda singola
        If NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_dipaz) = "S" And (bDipAzienda0 = False) Then
          GoTo Continua
        End If

        If NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_dipaz) <> "S" And (bDipAzienda1 = False) Then
          GoTo Continua
        End If

        'Controllo se devo esportare layout di griglia
        If ((Left(NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2), 2)).ToLower = "gr") And (bImEx0 = False) Then
          GoTo Continua
        End If
        'Controllo se devo esportare layout di form
        If ((Left(NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2), 2)).ToLower = "fm") And (NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv3) = ".") And (bImEx1 = False) Then
          GoTo Continua
        End If
        'Controllo se devo esportare le OPZIONI
        If ((NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2)).ToLower = "opzioni") And (bImEx2 = False) Then
          GoTo Continua
        End If
        'Controllo se devo esportare i Recent
        If ((NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2)).ToLower = "recent") And (bImEx3 = False) Then
          GoTo Continua
        End If
        'Controllo se devo esportare DCOM
        If ((NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2)).ToLower = "dcom") And (bImEx4 = False) Then
          GoTo Continua
        End If
        'Controllo se devo esportare i Report
        If ((Left(NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2), 6)).ToLower = "report") And (bImEx5 = False) Then
          GoTo Continua
        End If
        'Controllo se devo esportare le personalizzazioni globali
        If ((Left(NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2), 3)).ToLower = "alp") And (bImEx9 = False) Then
          GoTo Continua
        End If
        'Controllo se devo esportare la gestione eventi
        If ((Left(NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2), 3)).ToLower = "ale") And (bImEx8 = False) Then
          GoTo Continua
        End If
        'Controllo se devo esportare i controlli aggiuntivi
        If ((Left(NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2), 7)).ToLower = "newctrl") Or ((Left(NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2), 7)).ToLower = "newitem") And (bImEx7 = False) Then
          GoTo Continua
        End If
        'Controllo se devo esportare la sicurezza
        If ((Left(NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2), 7)).ToLower = "accessi") And (bImEx6 = False) Then
          GoTo Continua
        End If

        oCldGreg.EsportazioneRegProp(NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv1), NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv2), NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_liv3), NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_diput), strUtenteExp, NTSCStr(dsGregDir.Tables("REGDIR").Rows(i)!rd_dipaz), strAziendaExp, dsGregProp)

        Dim bScriviRigaDir As Boolean = False
        Dim dtrRigaPrecedente As DataRow = Nothing

        For j = 0 To dsGregProp.Tables("REGPROP").Rows.Count - 1
          If ((NTSCStr(dsGregProp.Tables("REGPROP").Rows(j)!rp_nomprop)).ToLower = "editablesecurity" Or (NTSCStr(dsGregProp.Tables("REGPROP").Rows(j)!rp_nomprop)).ToLower = "visiblesecurity") And (bImEx6 = False) Then
          Else
            If dtrRigaPrecedente Is Nothing Then
              lw1.WriteLine(ScriviRigaDir(dsGregDir.Tables("REGDIR").Rows(i), NTSCStr(dsGregProp.Tables("REGPROP").Rows(j)!rp_idut), NTSCStr(dsGregProp.Tables("REGPROP").Rows(j)!rp_idaz)))
              bScriviRigaDir = True
            Else
              If NTSCStr(dtrRigaPrecedente!rp_idut) <> NTSCStr(dsGregProp.Tables("REGPROP").Rows(j)!rp_idut) Or NTSCStr(dtrRigaPrecedente!rp_idaz) <> NTSCStr(dsGregProp.Tables("REGPROP").Rows(j)!rp_idaz) Then
                lw1.WriteLine(ScriviRigaDir(dsGregDir.Tables("REGDIR").Rows(i), NTSCStr(dsGregProp.Tables("REGPROP").Rows(j)!rp_idut), NTSCStr(dsGregProp.Tables("REGPROP").Rows(j)!rp_idaz)))
                bScriviRigaDir = True
              End If
            End If
            lw1.WriteLine(NTSCStr(dsGregProp.Tables("REGPROP").Rows(j)!rp_nomprop) & "=" & NTSCStr(dsGregProp.Tables("REGPROP").Rows(j)!rp_valprop) & " [#" & NTSCStr(dsGregProp.Tables("REGPROP").Rows(j)!rp_idtdoc) & "]")
            dtrRigaPrecedente = dsGregProp.Tables("REGPROP").Rows(j)
          End If
        Next

        If Not bScriviRigaDir Then
          lw1.WriteLine(ScriviRigaDir(dsGregDir.Tables("REGDIR").Rows(i), strUtenteExp, strAziendaExp))
        End If

        lw1.WriteLine("")
Continua:
      Next
      lw1.Flush()
      lw1.Close()

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
  Public Overridable Function ScriviRigaDir(ByRef dtrRiga As DataRow, ByVal strUtenteExp As String, ByVal strAziendaExp As String) As String
    Try
      ScriviRigaDir = "[" & NTSCStr(dtrRiga!rd_liv1) & "\" & NTSCStr(dtrRiga!rd_liv2) & "\" & NTSCStr(dtrRiga!rd_liv3) & "] "

      If NTSCStr(dtrRiga!rd_diput) = "S" Then
        ScriviRigaDir = ScriviRigaDir & "[#" & strUtenteExp & "] "
      End If

      If NTSCStr(dtrRiga!rd_dipaz) = "S" Then
        ScriviRigaDir = ScriviRigaDir & "[*" & strAziendaExp & "] "
      End If

      If NTSCStr(dtrRiga!rd_diptdoc) = "S" Then
        ScriviRigaDir = ScriviRigaDir & "Dip. Doc.= S"
      Else
        ScriviRigaDir = ScriviRigaDir & "Dip. Doc.= N"
      End If

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Sub Importazione(ByVal bDaFile As Boolean, _
          ByVal bImEx0 As Boolean, ByVal bImEx1 As Boolean, ByVal bImEx2 As Boolean, ByVal bImEx3 As Boolean, ByVal bImEx4 As Boolean, _
          ByVal bImEx5 As Boolean, ByVal bImEx6 As Boolean, ByVal bImEx7 As Boolean, ByVal bImEx8 As Boolean, ByVal bImEx9 As Boolean, _
          ByVal strUtenteImp As String, ByVal strFile As String, ByVal bHow As Boolean)

    Dim strRiga As String
    Dim strUt As String = ""
    Dim strAzi As String = ""
    Dim nPos1 As Integer
    Dim nPos2 As Integer
    Dim nPos3 As Integer
    Dim nPos4 As Integer
    Dim nFinePath As Integer
    Dim strLocLiv(0 To 2) As String
    Dim strOP As String
    Dim strDOC As String
    Dim strFlagAzi As String
    Dim strNomprop As String
    Dim strValProp As String
    Dim strTDoc As String
    Dim nPosUt As Integer
    Dim nPosAzi As Integer
    Dim strNomeFile As String
    Dim f1 As System.IO.StreamReader = Nothing
    Dim evt As NTSEventArgs = Nothing
    Dim dsRegDir As DataSet = Nothing
    Dim dsRegProp As DataSet = Nothing
    Dim bRet As Boolean
    Try
      '-----------------------------------------------------------------------------------------
      '--- Controlla l'esistenza del file
      '-----------------------------------------------------------------------------------------
      strNomeFile = strFile
      If Not Exists(strNomeFile) Then
        evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128333829580044417, "File inesistente. Impossibile importare"))
        ThrowRemoteEvent(evt)
        If Not evt.RetValue = "YES" Then
          bNoSost = True
          Exit Sub
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Apre il file
      '-----------------------------------------------------------------------------------------
      f1 = New System.IO.StreamReader(strNomeFile)
      '-----------------------------------------------------------------------------------------
      Do While Not f1.EndOfStream
        '---------------------------------------------------------------------------------------
        '--- Preleva il contenuto della riga
        '---------------------------------------------------------------------------------------
        strRiga = f1.ReadLine
        '---------------------------------------------------------------------------------------
        '--- Se la riga non contiene niente passa all'eventuale riga successiva
        '---------------------------------------------------------------------------------------
        If Trim(strRiga) <> "" Then
          '-------------------------------------------------------------------------------------
          '--- Ha trovato la cartella pricipale
          '-------------------------------------------------------------------------------------
          If Left(strRiga, 1) = "[" Then
            '-----------------------------------------------------------------------------------
            '--- Scrive le cartelle
            '-----------------------------------------------------------------------------------
            nPosUt = InStr(1, strRiga, "[#")
            strUt = " "
            strAzi = " "
            strOP = "N"
            strFlagAzi = "N"
            If nPosUt > 0 Then
              nPos3 = InStr(nPosUt, strRiga, "]")
              strUt = Mid(strRiga, (nPosUt + 2), (nPos3 - nPosUt - 2))
              If Not bDaFile Then strUt = strUtenteImp
              strOP = "S"
            End If
            nPosAzi = InStr(1, strRiga, "[*")
            If nPosAzi > 0 Then
              nPos4 = InStr(nPosAzi, strRiga, "]")
              strAzi = Mid(strRiga, (nPosAzi + 2), (nPos4 - nPosAzi - 2))
              strFlagAzi = "S"
            End If
            nPos1 = InStr(1, strRiga, "\")
            nPos2 = InStr(nPos1 + 1, strRiga, "\")
            nFinePath = InStr(strRiga, "]")
            strLocLiv(0) = Mid(strRiga, 2, (nPos1 - 2))
            strLocLiv(1) = Mid(strRiga, (nPos1 + 1), (nPos2 - nPos1 - 1))
            strLocLiv(2) = Mid(strRiga, (nPos2 + 1), (nFinePath - nPos2 - 1))
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare layout di griglia
            '-----------------------------------------------------------------------------------
            If ((Left(strLocLiv(1), 2)).ToLower = "gr") And (bImEx0 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare layout di form
            '-----------------------------------------------------------------------------------
            If ((Left(strLocLiv(1), 2)).ToLower = "fm") And (strLocLiv(2) = ".") And (bImEx1 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare le OPZIONI
            '-----------------------------------------------------------------------------------
            If ((strLocLiv(1)).ToLower = "opzioni") And (bImEx2 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare i Recent
            '-----------------------------------------------------------------------------------
            If ((strLocLiv(1)).ToLower = "recent") And (bImEx3 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare DCOM
            '-----------------------------------------------------------------------------------
            If ((strLocLiv(1)).ToLower = "dcom") And (bImEx4 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare i Report
            '-----------------------------------------------------------------------------------
            If ((Left(strLocLiv(1), 6)).ToLower = "report") And (bImEx5 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare la sicurezza
            '-----------------------------------------------------------------------------------
            If ((Left(strLocLiv(1), 2)).ToLower = "fm") And (strLocLiv(2) <> ".") And (bImEx6 = False) Then
              GoTo Continua
            End If
            strDOC = Right(strRiga, 1)

            oCldGreg.ImportazioneSelectRegProp(strLocLiv, dsRegDir)

            If dsRegDir.Tables("REGDIR").Rows.Count > 0 Then
              'If bHow = True Then
              '  oCldGreg.ImportazioneUpdateRegProp(strLocLiv, strOP, strDOC, strFlagAzi)
              'End If
            Else
              bRet = CreaCartellaGreg(strLocLiv(0), strLocLiv(1), strLocLiv(2), " ", "." & strOP & strOP, "." & strFlagAzi & strFlagAzi, "." & strDOC & strDOC)
            End If
          Else
            '-----------------------------------------------------------------------------------
            '--- Scrive le proprietà
            '--- Controllo se devo esportare layout di griglia
            '-----------------------------------------------------------------------------------
            If ((Left(strLocLiv(1), 2)).ToLower = "gr") And (bImEx0 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare layout di form
            '-----------------------------------------------------------------------------------
            If ((Left(strLocLiv(1), 2)).ToLower = "fm") And (strLocLiv(2) = ".") And (bImEx1 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare le OPZIONI
            '-----------------------------------------------------------------------------------
            If (strLocLiv(1).ToLower = "opzioni") And (bImEx2 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare i Recent
            '-----------------------------------------------------------------------------------
            If (strLocLiv(1).ToLower = "recent") And (bImEx3 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare DCOM
            '-----------------------------------------------------------------------------------
            If ((strLocLiv(1).ToLower) = "dcom") And (bImEx4 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare i Report
            '-----------------------------------------------------------------------------------
            If ((Left(strLocLiv(1), 6)) = "report") And (bImEx5 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Controllo se devo esportare la sicurezza
            '-----------------------------------------------------------------------------------
            If (((Left(strLocLiv(1), 2)).ToLower) = "fm") And (strLocLiv(2) <> ".") And (bImEx6 = False) Then
              GoTo Continua
            End If
            '-----------------------------------------------------------------------------------
            '--- Se la proprietà e il valore sono tutte su una riga si comporta come sempre
            '-----------------------------------------------------------------------------------
            If (InStr(1, strRiga, "=") > 0) And (InStr(1, strRiga, "[#") > 0) Then
              nPos1 = InStr(1, strRiga, "=")
              nPos2 = InStr(1, strRiga, "[#") + 1
              strTDoc = " "
              If nPos2 > 0 Then
                nPos3 = InStr(1, strRiga, "[#")
                strTDoc = Mid(strRiga, (nPos2 + 1), 1)
              Else
                nPos3 = Len(strRiga)
              End If
              strNomprop = Left(strRiga, (nPos1 - 1))
              strValProp = Mid(strRiga, (nPos1 + 1), (nPos3 - nPos1 - 2))
              If strValProp = "" Then strValProp = " "

              oCldGreg.ImportazioneSelectWhereRegProp(strLocLiv, strUt, strAzi, strTDoc, strNomprop, dsRegProp)

              If dsRegProp.Tables("REGPROP").Rows.Count > 0 Then
                If bHow = True Then
                  '-----------------------------------------------------------------------------
                  '--- Scrive la riga in ACTLOG prima della modifica
                  '-----------------------------------------------------------------------------
                  SalvaVariabili(strLocLiv(0), strLocLiv(1), strLocLiv(2), strUt, strAzi, strTDoc, strNomprop)
                  strSqlWhere = CStrSQL(strLiv1Tmp) & "," & CStrSQL(strLiv2Tmp) & "," & _
                    CStrSQL(strLiv3Tmp) & "," & CStrSQL(strIdutTmp) & "," & _
                    CStrSQL(strIdazTmp) & "," & CStrSQL(strIdtdocTmp) & "," & _
                    CStrSQL(strNompropTmp)
                  strDesogglog = "Modifica impostazioni registro di Business con importazione ramo di registro '" & strFile & "' in cartella: " & strLiv1Tmp
                  If strLiv2Tmp <> "." Then strDesogglog = strDesogglog & "\" & strLiv2Tmp
                  If strLiv3Tmp <> "." Then strDesogglog = strDesogglog & "\" & strLiv3Tmp
                  strTipoop = "M"
                  strTipoogg = "D"
                  ScriviLog()
                  '-----------------------------------------------------------------------------
                  oCldGreg.ImportazioneUpdateWhereRegProp(strLocLiv, strValProp, strUt, strAzi, strTDoc, strNomprop)
                End If
              Else
                oCldGreg.ImportazioneInsertIntoRegProp(strLocLiv, strValProp, strUt, strAzi, strTDoc, strNomprop)
              End If
            Else
              '---------------------------------------------------------------------------------
              '--- Se la proprietà e il valore sono su più righe
              '--- cicla fino all'eventuale proprietà successiva concatenando le righe
              '---------------------------------------------------------------------------------
              nPos1 = InStr(1, strRiga, "=")
              strTDoc = " "
              strNomprop = Left(strRiga, (nPos1 - 1))
              strValProp = Trim(Mid(strRiga, (nPos1 + 1)))
              Do While Not f1.EndOfStream
                strRiga = f1.ReadLine
                If Trim(strRiga) <> "" Then
                  nPos2 = InStr(1, strRiga, "[#")
                  If nPos2 > 0 Then
                    nPos3 = InStr(1, strRiga, "[#")
                    strTDoc = Mid(strRiga, (nPos2 + 2), 1)
                    strValProp = strValProp & Chr(10) & Chr(13) & Trim(Mid(strRiga, 1, (nPos3 - 2)))
                    GoTo SaltaLoopInterno
                  Else
                    strValProp = strValProp & Chr(10) & Chr(13) & Trim(strRiga)
                  End If
                End If
              Loop
SaltaLoopInterno:
              '---------------------------------------------------------------------------------
              '--- Siccome la funzione 'Replace' non riconosce i Tab, ma lo vede come un blank
              '--- allora sostituisco i blank con altri blank, in questo modo sostituisce
              '--- anche i tab
              '---------------------------------------------------------------------------------
              strValProp = strValProp.Replace(Chr(9), " ")
              strValProp = strValProp.Replace(Chr(32), " ")
              '---------------------------------------------------------------------------------
              oCldGreg.ImportazioneSelectWhereRegProp(strLocLiv, strUt, strAzi, strTDoc, strNomprop, dsRegProp)
              If dsRegProp.Tables("REGPROP").Rows().Count > 0 Then
                If bHow = True Then
                  '-----------------------------------------------------------------------------
                  '--- Scrive la riga in ACTLOG prima della modifica
                  '-----------------------------------------------------------------------------
                  SalvaVariabili(strLocLiv(0), strLocLiv(1), strLocLiv(2), strUt, strAzi, strTDoc, strNomprop)
                  strSqlWhere = CStrSQL(strLiv1Tmp) & "," & CStrSQL(strLiv2Tmp) & "," & _
                    CStrSQL(strLiv3Tmp) & "," & CStrSQL(strIdutTmp) & "," & _
                    CStrSQL(strIdazTmp) & "," & CStrSQL(strIdtdocTmp) & "," & _
                    CStrSQL(strNompropTmp)
                  strDesogglog = "Modifica impostazioni registro di Business con importazione ramo di registro '" & strFile & "' in cartella: " & strLiv1Tmp
                  If strLiv2Tmp <> "." Then strDesogglog = strDesogglog & "\" & strLiv2Tmp
                  If strLiv3Tmp <> "." Then strDesogglog = strDesogglog & "\" & strLiv3Tmp
                  strTipoop = "M"
                  strTipoogg = "D"
                  ScriviLog()
                  '-----------------------------------------------------------------------------
                  oCldGreg.ImportazioneUpdateWhereRegProp(strLocLiv, strValProp, strUt, strAzi, strTDoc, strNomprop)
                End If
              Else
                oCldGreg.ImportazioneInsertIntoRegProp(strLocLiv, strValProp, strUt, strAzi, strTDoc, strNomprop)
              End If
            End If
          End If
        End If
Continua:
      Loop
      '-----------------------------------------------------------------------------------------
      '--- Chiude il file
      '-----------------------------------------------------------------------------------------
      f1.Close()
      '-----------------------------------------------------------------------------------------
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
  Public Overridable Sub SalvaVariabili(ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strliv3 As String, _
        ByVal strIdut As String, ByVal strIdaz As String, ByVal strIdtdoc As String, _
        ByVal strNomprop As String)
    Dim dsRegProp As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      oCldGreg.SalvaVariabili(strLiv1, strLiv2, strliv3, strIdut, strIdaz, strIdtdoc, strNomprop, dsRegProp)

      If dsRegProp.Tables("REGPROP").Rows.Count > 0 Then
        With dsRegProp.Tables("REGPROP").Rows(0)
          strLiv1Tmp = NTSCStr(!rp_liv1)
          strLiv2Tmp = NTSCStr(!rp_liv2)
          strLiv3Tmp = NTSCStr(!rp_liv3)
          strIdutTmp = NTSCStr(!rp_idut)
          strIdazTmp = NTSCStr(!rp_idaz)
          strIdtdocTmp = NTSCStr(!rp_idtdoc)
          strNompropTmp = NTSCStr(!rp_nomprop)
          strValpropTmp = NTSCStr(!rp_valprop)
        End With
      End If
      '-----------------------------------------------------------------------------------------
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
  Public Overridable Function CreaCartellaGreg(ByVal strLiv1 As String, ByVal strLiv2 As String, ByVal strLiv3 As String, ByVal strTipoDoc As String, ByVal strDipNomOp As String, ByVal strDipAz As String, ByVal strDipTipDoc As String) As Boolean
    Dim strSQL As String
    Dim strDipCor(0 To 2) As String
    Dim strDip(0 To 2) As String
    Dim strTemp(0 To 2) As String
    Dim strDipPrec(0 To 2) As String
    Dim nLiv As Integer
    Dim nCicla As Integer
    Dim nIndice As Integer
    Dim nI As Integer
    Dim bIsRoot As Boolean
    Dim dsRegDir As DataSet = Nothing
    Try
      CreaCartellaGreg = False

      bIsRoot = False
      If strLiv2 <> "." And strLiv1 = "." Then
        Exit Function
      End If
      If strLiv3 <> "." And strLiv2 = "." Then
        Exit Function
      End If
      If Len(Trim(strTipoDoc)) = 0 Then
        strTipoDoc = " "
      End If
      nLiv = 3
      If strLiv3 = "." Then
        nLiv = 2
        If strLiv2 = "." Then
          nLiv = 1
          If strLiv1 = "." Then
            nLiv = 0
          End If
        End If
      End If
      strTemp(0) = strLiv1
      strTemp(1) = strLiv2
      strTemp(2) = strLiv3
      strDip(0) = strDipNomOp
      strDip(1) = strDipAz
      strDip(2) = strDipTipDoc
      For nIndice = nLiv To 0 Step -1
        For nI = nIndice + 1 To 3
          strTemp(nI - 1) = "."
        Next
        oCldGreg.CreaCartellaGregSelect(strTemp, dsRegDir)

        If dsRegDir.Tables("REGDIR").Rows.Count > 0 Then
          Exit For
        End If
      Next
      For nI = 0 To 2
        strDipPrec(nI) = NTSCStr(dsRegDir.Tables("REGDIR").Rows(0)(nI + 3))
        strDipCor(nI) = strDipPrec(nI)
      Next
      If nIndice = 0 Then
        nIndice = 1
        bIsRoot = True
      Else
        nIndice = nIndice + 1
      End If
      For nCicla = nIndice To nLiv
        If nCicla = 1 Then
          strTemp(0) = strLiv1
        End If
        If nCicla = 2 Then
          strTemp(1) = strLiv2
        End If
        If nCicla = 3 Then
          strTemp(2) = strLiv3
        End If
        strSQL = ""
        For nI = 0 To 2
          If Mid(strDip(nI), nCicla, 1) = "." Then
            strDipCor(nI) = strDipPrec(nI)
          Else
            strDipCor(nI) = Mid(strDip(nI), nCicla, 1)
            strDipPrec(nI) = strDipCor(nI)
          End If
        Next
        If oCldGreg.CreaCartellaGregInsertInto(strTemp, strDipCor) = True Then CreaCartellaGreg = True
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
  End Function
#End Region

#Region "funzioni specifiche per BN__GREG.BN__GRCK"

  Public Overridable Function ControllaRegistro() As Boolean
    Dim lCount As Integer
    Dim lCurr As Integer
    Dim lErr As Integer
    Dim bUt As Boolean
    Dim bAz As Boolean
    Dim bDoc As Boolean
    Dim bErr As Boolean
    Dim bUtLast As Boolean
    Dim bAzLast As Boolean
    Dim bDocLast As Boolean
    Dim strLiv1Last As String = ""
    Dim strLiv2Last As String = ""
    Dim strCart As String
    Dim strProp As String
    Dim i As Integer
    Dim j As Integer
    Dim dsRegDir As DataSet = Nothing
    Dim dsRegProp As DataSet = Nothing
    Dim evt As NTSEventArgs = Nothing
    Try
      ControllaRegistro = False

      'Seleziona le cartelle
      ThrowRemoteEvent(New NTSEventArgs("OPERAZCOR:" & "Lettuta cartelle in corso...", ""))

      oCldGreg.GrckGetRegDir(dsRegDir)

      If dsRegDir.Tables("REGDIR").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128334518219387368, "Nessuna cartella presente.")))
        ThrowRemoteEvent(New NTSEventArgs("OPERAZCOR:" & "Nessuna.", ""))
        ControllaRegistro = True
        Exit Function
      End If
      lCurr = 0
      lErr = 0
      lCount = dsRegDir.Tables("REGDIR").Rows.Count
      For i = 0 To dsRegDir.Tables("REGDIR").Rows.Count - 1
        With dsRegDir.Tables("REGDIR").Rows(i)
          lCurr = lCurr + 1
          strCart = "\" & NTSCStr(!rd_liv1) & "\" & NTSCStr(!rd_liv2) & "\" & NTSCStr(!rd_liv3)
          ThrowRemoteEvent(New NTSEventArgs("OPERAZCOR:" & "Scansione cartella " & strCart & " in corso...", ""))
          'Legge le dipendenze
          If NTSCStr(!rd_diput) = "S" Then bUt = True Else bUt = False
          If NTSCStr(!rd_dipaz) = "S" Then bAz = True Else bAz = False
          If NTSCStr(!rd_diptdoc) = "S" Then bDoc = True Else bDoc = False
          If (Left(NTSCStr(!rd_liv2), 2)).ToUpper = "GR" And NTSCStr(!rd_liv3) = "." And (Not bUt) Then
            'Caso particolare: se la cartella è una di griglia x utente
            'controlla se non ha dipendenza OPERATORE
            lErr = lErr + 1
            ThrowRemoteEvent(New NTSEventArgs("NUMERRORI:" & lErr & " errori.", ""))
            LogWrite(lCurr & ": cartella '" & strCart & "': cartella di personalizzazione griglie SENZA indicazione della dipendenza OPERATORE.", True)
          End If
          If (Left(NTSCStr(!rd_liv2), 5)).ToUpper = "ALPGR" And NTSCStr(!rd_liv3) = "." And bUt Then
            'Caso particolare: se la cartella è una di griglia globale
            'controlla se ha dipendenza OPERATORE
            lErr = lErr + 1
            ThrowRemoteEvent(New NTSEventArgs("NUMERRORI:" & lErr & " errori.", ""))
            LogWrite(lCurr & ": cartella '" & strCart & "': cartella di personalizzazione griglie globale CON indicazione della dipendenza OPERATORE.", True)
          End If
          'Se cartella di secondo livello si salva le impostazioni
          If NTSCStr(!rd_liv1) <> "." And NTSCStr(!rd_liv2) <> "." And NTSCStr(!rd_liv3) = "." Then
            strLiv1Last = NTSCStr(!rd_liv1)
            strLiv2Last = NTSCStr(!rd_liv2)
            bUtLast = bUt : bAzLast = bAz : bDocLast = bDoc
          End If
          If (Left(NTSCStr(!rd_liv2), 2)).ToUpper = "GR" And (Left(NTSCStr(!rd_liv3), 7)).ToUpper = "COLONNA" And (Not bUt) Then
            'Caso particolare: se la cartella è una colonna di griglia x utente
            'controlla se non ha dipendenza OPERATORE
            lErr = lErr + 1
            ThrowRemoteEvent(New NTSEventArgs("NUMERRORI:" & lErr & " errori.", ""))
            LogWrite(lCurr & ": cartella '" & strCart & "': cartella di personalizzazione colonne griglie SENZA indicazione della dipendenza OPERATORE.", True)
          End If
          If (Left(NTSCStr(!rd_liv2), 5)).ToUpper = "ALPGR" And (Left(NTSCStr(!rd_liv3), 7)).ToUpper = "COLONNA" And (bUt) Then
            'Caso particolare: se la cartella è una colonna di griglia globale
            'controlla se ha dipendenza OPERATORE
            lErr = lErr + 1
            ThrowRemoteEvent(New NTSEventArgs("NUMERRORI:" & lErr & " errori.", ""))
            LogWrite(lCurr & ": cartella '" & strCart & "': cartella di personalizzazione colonne griglie globale CON indicazione della dipendenza OPERATORE.", True)
          End If
          If NTSCStr(!rd_liv1) = strLiv1Last And NTSCStr(!rd_liv2) = strLiv2Last And (Left(NTSCStr(!rd_liv3), 7)).ToUpper = "COLONNA" Then
            If (bAz <> bAzLast) Or (bDoc <> bDocLast) Then
              'Caso particolare: le cartelle "ColonnaYY" devono avere la stessa
              'dipendenza azienda/tipodoc come la cartella padre "grXX"
              lErr = lErr + 1
              ThrowRemoteEvent(New NTSEventArgs("NUMERRORI:" & lErr & " errori.", ""))
              LogWrite(lCurr & ": cartella '" & strCart & "': cartella di personalizzazione colonne griglie con dipendenza AZIENDA/DITTA o DOCUMENTO diversa dalla cartella padre.", True)
            End If
          End If
          'Seleziona le proprietà
          oCldGreg.GrckGetRegProp(NTSCStr(!rd_liv1), NTSCStr(!rd_liv2), NTSCStr(!rd_liv3), dsRegProp)

          For j = 0 To dsRegProp.Tables("REGPROP").Rows.Count - 1
            strProp = NTSCStr(dsRegProp.Tables("REGPROP").Rows(j)!rp_nomprop)
            bErr = False
            If bUt And NTSCStr(dsRegProp.Tables("REGPROP").Rows(j)!rp_idut) = " " Then
              bErr = True
              'log
              LogWrite(lCurr & ": cartella '" & strCart & "', proprietà '" & strProp & "': cartella con dipendenza OPERATORE e proprietà senza indicazione dell'operatore.", True)
            End If
            If (Not bUt) And NTSCStr(dsRegProp.Tables("REGPROP").Rows(j)!rp_idut) <> " " Then
              'Non loggare se si tratta di cartella COLONNAxx
              If Not ((Left(NTSCStr(!rd_liv2), 2)).ToUpper = "GR" And (Left(NTSCStr(!rd_liv3), 7)).ToUpper = "COLONNA") Then
                bErr = True
                'log
                LogWrite(lCurr & ": cartella '" & strCart & "', proprietà '" & strProp & "': cartella senza dipendenza OPERATORE e proprietà con indicazione dell'operatore '" & NTSCStr(dsRegProp.Tables("REGPROP").Rows(j)!rp_idut) & "'.", True)
              End If
            End If
            If bAz And NTSCStr(dsRegProp.Tables("REGPROP").Rows(j)!rp_idaz) = " " Then
              bErr = True
              'log
              LogWrite(lCurr & ": cartella '" & strCart & "', proprietà '" & strProp & "': cartella con dipendenza AZIENDA/DITTA e proprietà senza indicazione dell'azienda/ditta.", True)
            End If
            If (Not bAz) And NTSCStr(dsRegProp.Tables("REGPROP").Rows(j)!rp_idaz) <> " " Then
              bErr = True
              'log
              LogWrite(lCurr & ": cartella '" & strCart & "', proprietà '" & strProp & "': cartella senza dipendenza AZIENDA/DITTA e proprietà con indicazione dell'azienda/ditta '" & NTSCStr(dsRegProp.Tables("REGPROP").Rows(j)!rp_idaz) & "'.", True)
            End If
            If bDoc And NTSCStr(dsRegProp.Tables("REGPROP").Rows(j)!rp_idtdoc) = " " Then
              bErr = True
              'log
              LogWrite(lCurr & ": cartella '" & strCart & "', proprietà '" & strProp & "': cartella con dipendenza DOCUMENTO e proprietà senza indicazione del tipo documento.", True)
            End If
            If (Not bDoc) And NTSCStr(dsRegProp.Tables("REGPROP").Rows(j)!rp_idtdoc) <> " " Then
              bErr = True
              'log
              LogWrite(lCurr & ": cartella '" & strCart & "', proprietà '" & strProp & "': cartella senza dipendenza DOCUMENTO e proprietà con indicazione del tipo documento '" & NTSCStr(dsRegProp.Tables("REGPROP").Rows(j)!rp_idtdoc) & "'.", True)
            End If
            If bErr Then
              lErr = lErr + 1
              ThrowRemoteEvent(New NTSEventArgs("NUMERRORI:" & lErr & " errori.", ""))
            End If
          Next
        End With
      Next

      ThrowRemoteEvent(New NTSEventArgs("OPERAZCOR:" & "Nessuna.", ""))
      LogStop()
      
      ThrowRemoteEvent(New NTSEventArgs("NUMERRORI:" & "", ""))
      ControllaRegistro = True

    Catch ex As Exception
      '--------------------------------------------------------------
      LogStop()
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

#End Region

End Class
