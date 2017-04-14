Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLE__NUME
  Inherits CLE__BASN

  Public oCldNume As CLD__NUME 'oggetto dal
  Public dsShared As DataSet

  Public bHasChanges As Boolean = False


  Public strCodpcon As String ' codice piano dei conti

  Public strTipoSelected As String

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

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__NUME"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldNume = CType(MyBase.ocldBase, CLD__NUME)
    oCldNume.Init(oApp)
    Return True
  End Function

  Public Overridable Function Apri(ByVal strDitta As String, ByVal strFiltroTipo As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try

      strDittaCorrente = strDitta
      dReturn = oCldNume.LeggiTabella(strDittaCorrente, strFiltroTipo, ds, strCodpcon)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldNume.SetTableDefaultValueFromDB("TABNUMA", ds)

      SetDefaultValue(strFiltroTipo, ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("TABNUMA").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TABNUMA").ColumnChanged, AddressOf AfterColUpdate

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
  Public Overridable Sub SetDefaultValue(ByVal strFiltroTipo As String, ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("TABNUMA")
        .Columns("codditt").DefaultValue = strDittaCorrente
        .Columns("tb_numtipo").DefaultValue = oCldNume.RimappaNomiUguali(strFiltroTipo)
        Select Case strFiltroTipo
          Case "CC", "FF", "SS", "SA"
            .Columns("xx_mastro").DefaultValue = "0"
          Case "R", "O", "Q", "Y", "H", "X", "$", "VV", "#"
            .Columns("tb_numcodl").DefaultValue = 1900
            .Columns("tb_numdata").DefaultValue = "01/01/1900"
            .Columns("tb_numdata2").DefaultValue = "01/01/1900"
          Case "A", "L", "B", "M", "C", "Z", "W", "T", "U", "G", "F", "S", "V", "P1", "P2", "P3"
            .Columns("tb_numcodl").DefaultValue = 1900
            .Columns("tb_numdata").DefaultValue = "01/01/1900"
            .Columns("tb_numdata2").DefaultValue = "01/01/1900"
            If (strFiltroTipo = "C") Then
              .Columns("tb_numflag").DefaultValue = "C"
            ElseIf (strFiltroTipo = "A") Then
              .Columns("tb_numflag").DefaultValue = "V"
            ElseIf (strFiltroTipo = "L") Then
              .Columns("tb_numflag").DefaultValue = "A"
            Else
              .Columns("tb_numflag").DefaultValue = "N"
            End If
          Case "P"
            .Columns("xx_tipo").DefaultValue = "A"
            .Columns("xx_num").DefaultValue = "0"
            .Columns("xx_numserie2").DefaultValue = " "
            .Columns("tb_numcodl").DefaultValue = DateTime.Now.Year
          Case "EA", "DA"
            .Columns("tb_numcodl").DefaultValue = 1900
            .Columns("xx_numserie").DefaultValue = "0"
          Case "DI"
            .Columns("tb_numcodl").DefaultValue = 1900
            .Columns("xx_numserie3").DefaultValue = "F"
          Case "SM"
            .Columns("xx_numserie1").DefaultValue = "0"
            .Columns("tb_numcodl").DefaultValue = 1900
          Case "CP"
            .Columns("tb_numserie").DefaultValue = ""
            .Columns("xx_codfam").DefaultValue = ""
            .Columns("tb_numflag").DefaultValue = ""
            .Columns("tb_numflag2").DefaultValue = ""
          Case "CQ"
            .Columns("tb_numserie").DefaultValue = ""
            .Columns("xx_codart").DefaultValue = ""
            .Columns("tb_numflag").DefaultValue = ""
            .Columns("tb_numflag2").DefaultValue = ""
          Case "AA"
            .Columns("xx_codroot").DefaultValue = ""
          Case "CL", "NC"
            .Columns("tb_numcodl").DefaultValue = 1900
          Case "RO"
            .Columns("tb_numcodl").DefaultValue = 1900
          Case "!"
            .Columns("tb_numcodl").DefaultValue = 1900
          Case "PD"
            .Columns("tb_numcodl").DefaultValue = 1900
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
  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      ThrowRemoteEvent(New NTSEventArgs("GETVARIAB:", ""))

      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldNume.ScriviTabella(strDittaCorrente, dsShared.Tables("TABNUMA"))

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
  Public Overridable Function Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("TABNUMA").Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Function TestPreSalva() As Boolean
    Dim i As Integer = 0
    Dim dtrTmp() As DataRow
    Dim strTmp As String
    Dim bTipoconto As Boolean
    Dim dsTmp As DataSet = Nothing
    Dim strDescr As String = ""
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate

      dtrTmp = dsShared.Tables("TABNUMA").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1

        Select Case strTipoSelected
          Case "CC"
            If NTSCStr(dtrTmp(i)!xx_mastro) = "0" Then
              Return False
            End If
            bTipoconto = oCldNume.EstMastroCFSDitt(strDittaCorrente, "C", NTSCInt(dtrTmp(i)!xx_mastro))
            If bTipoconto Then
              oCldNume.GetMastro(strCodpcon, NTSCInt(dtrTmp(i)!xx_mastro), dsTmp)
              If dsTmp.Tables("TABMAST").Rows.Count > 0 Then
                dtrTmp(i)!xx_numcodl = NTSCStr(dsTmp.Tables("TABMAST").Rows(0)!tb_desmast)
                If NTSCStr(dtrTmp(i)!tb_numcodl) <> NTSCStr(dtrTmp(i)!xx_mastro) Then
                  dtrTmp(i)!tb_numcodl = NTSCStr(dtrTmp(i)!xx_mastro)
                End If
              Else
                dtrTmp(i)!xx_mastro = "0"
                Return False
              End If
            Else
              dtrTmp(i)!xx_mastro = "0"
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128359570296151708, "Inserire un codice mastro di tipo Cliente.")))
              Return False
            End If
          Case "FF"
            If NTSCStr(dtrTmp(i)!xx_mastro) = "0" Then
              Return False
            End If
            bTipoconto = oCldNume.EstMastroCFSDitt(strDittaCorrente, "F", NTSCInt(dtrTmp(i)!xx_mastro))
            If bTipoconto Then
              oCldNume.GetMastro(strCodpcon, NTSCInt(dtrTmp(i)!xx_mastro), dsTmp)
              If dsTmp.Tables("TABMAST").Rows.Count > 0 Then
                dtrTmp(i)!xx_numcodl = NTSCStr(dsTmp.Tables("TABMAST").Rows(0)!tb_desmast)
                If NTSCStr(dtrTmp(i)!tb_numcodl) <> NTSCStr(dtrTmp(i)!xx_mastro) Then
                  dtrTmp(i)!tb_numcodl = NTSCStr(dtrTmp(i)!xx_mastro)
                End If
              Else
                dtrTmp(i)!xx_mastro = "0"
                Return False
              End If
            Else
              dtrTmp(i)!xx_mastro = "0"
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128360753225995137, "Inserire un codice mastro di tipo Fornitore.")))
              Return False
            End If
          Case "SS"
            If NTSCStr(dtrTmp(i)!xx_mastro) = "0" Then
              Return False
            End If
            bTipoconto = oCldNume.EstMastroCFSDitt(strDittaCorrente, "S", NTSCInt(dtrTmp(i)!xx_mastro))
            If bTipoconto Then
              oCldNume.GetMastro(strCodpcon, NTSCInt(dtrTmp(i)!xx_mastro), dsTmp)
              If dsTmp.Tables("TABMAST").Rows.Count > 0 Then
                dtrTmp(i)!xx_numcodl = NTSCStr(dsTmp.Tables("TABMAST").Rows(0)!tb_desmast)
                If NTSCStr(dtrTmp(i)!tb_numcodl) <> NTSCStr(dtrTmp(i)!xx_mastro) Then
                  dtrTmp(i)!tb_numcodl = NTSCStr(dtrTmp(i)!xx_mastro)
                End If
              Else
                dtrTmp(i)!xx_mastro = "0"
                Return False
              End If
            Else
              dtrTmp(i)!xx_mastro = "0"
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129945184717197259, "Inserire un codice mastro di tipo Sottoconto.")))
              Return False
            End If
          Case "SA"
            If NTSCStr(dtrTmp(i)!xx_mastro) = "0" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128302749174062500, "Il codice mastro non può essere vuoto.")))
              Return False
            Else
              If ocldBase.ValCodiceDb(NTSCStr(dtrTmp(i)!xx_mastro), strDittaCorrente, "TABMACA", "N", strDescr) Then
                If NTSCStr(dtrTmp(i)!tb_numcodl) <> NTSCStr(dtrTmp(i)!xx_mastro) Then
                  dtrTmp(i)!tb_numcodl = NTSCStr(dtrTmp(i)!xx_mastro)
                  dtrTmp(i)!xx_numcodl = strDescr
                End If
              Else
                dtrTmp(i)!xx_mastro = "0"
                dtrTmp(i)!xx_numcodl = ""
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128360753192088453, "Inserire un valido codice mastro.")))
                Return False
              End If
            End If

          Case "CQ"
            If NTSCStr(dtrTmp(i)!xx_codart) = "" Then
              Return False
            End If
            If oCldNume.ValCodiceDb(NTSCStr(dtrTmp(i)!xx_codart), strDittaCorrente, "ARTICO", "S", strDescr) Then
              If NTSCStr(dtrTmp(i)!tb_numserie) <> NTSCStr(dtrTmp(i)!xx_codart) Then
                dtrTmp(i)!tb_numserie = NTSCStr(dtrTmp(i)!xx_codart)
                dtrTmp(i)!xx_numcodl = strDescr
              End If
            Else
              dtrTmp(i)!xx_codart = ""
              dtrTmp(i)!xx_numcodl = ""
              Return False
            End If
          Case "CP"
            If NTSCStr(dtrTmp(i)!xx_codfam) = "" Then
              Return False
            End If
            If oCldNume.ValCodiceDb(NTSCStr(dtrTmp(i)!xx_codfam), strDittaCorrente, "TABCFAM", "S", strDescr) Then
              If NTSCStr(dtrTmp(i)!tb_numserie) <> NTSCStr(dtrTmp(i)!xx_codfam) Then
                dtrTmp(i)!tb_numserie = NTSCStr(dtrTmp(i)!xx_codfam)
                dtrTmp(i)!xx_numcodl = strDescr
              End If
            Else
              dtrTmp(i)!xx_codfam = ""
              dtrTmp(i)!xx_numcodl = ""
              Return False
            End If
          Case "P"
            strTmp = NTSCStr(dtrTmp(i)!xx_tipo) & NTSCStr(dtrTmp(i)!xx_numserie2) & "".PadLeft(3 - Len(NTSCStr(dtrTmp(i)!xx_num)), CChar("0")) & NTSCStr(dtrTmp(i)!xx_num)
            dtrTmp(i)!tb_numserie = strTmp
            If NTSCInt(dtrTmp(i)!tb_numcodl) < 1900 Or NTSCInt(dtrTmp(i)!tb_numcodl) > 2100 Then
              dtrTmp(i)!tb_numcodl = 1900
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780449793222011, "Inserire un anno compreso tra 1900 e 2100")))
              Return False
            End If
          Case "R", "O", "Q", "A", "L", "B", "M", "C", "Z", "Y", "H", "W", "X", "T", "U", "G", "F", "S", "V", "CL", "NC", "$", "VV", "#", "P1", "P2", "P3"
            If NTSCInt(dtrTmp(i)!tb_numcodl) < 1900 Or NTSCInt(dtrTmp(i)!tb_numcodl) > 2100 Then
              dtrTmp(i)!tb_numcodl = 1900
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780449829816991, "Inserire un anno compreso tra 1900 e 2100")))
              Return False
            End If
          Case "SM"
            If NTSCStr(dtrTmp(i)!xx_numserie1) = "0" Then
              Return False
            End If
            If NTSCStr(dtrTmp(i)!tb_numserie) <> NTSCStr(dtrTmp(i)!xx_numserie1) Then
              dtrTmp(i)!tb_numserie = NTSCStr(dtrTmp(i)!xx_numserie1)
            End If
            If NTSCInt(dtrTmp(i)!tb_numcodl) < 1900 Or NTSCInt(dtrTmp(i)!tb_numcodl) > 2100 Then
              dtrTmp(i)!tb_numcodl = 1900
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780449861857231, "Inserire un anno compreso tra 1900 e 2100")))
              Return False
            End If
          Case "EA", "DA"
            If NTSCStr(dtrTmp(i)!xx_numserie) = "0" Then
              Return False
            End If
            If NTSCStr(dtrTmp(i)!tb_numserie) <> NTSCStr(dtrTmp(i)!xx_numserie) Then
              dtrTmp(i)!tb_numserie = NTSCStr(dtrTmp(i)!xx_numserie)
            End If
            If NTSCInt(dtrTmp(i)!tb_numcodl) < 1900 Or NTSCInt(dtrTmp(i)!tb_numcodl) > 2100 Then
              dtrTmp(i)!tb_numcodl = 1900
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780449899208031, "Inserire un anno compreso tra 1900 e 2100")))
              Return False
            End If
          Case "DI"
            If NTSCStr(dtrTmp(i)!xx_numserie3) <> "C" And NTSCStr(dtrTmp(i)!xx_numserie3) <> "F" Then
              Return False
            End If
            If NTSCStr(dtrTmp(i)!tb_numserie) <> NTSCStr(dtrTmp(i)!xx_numserie3) Then
              dtrTmp(i)!tb_numserie = NTSCStr(dtrTmp(i)!xx_numserie3)
            End If
            If NTSCInt(dtrTmp(i)!tb_numcodl) < 1900 Or NTSCInt(dtrTmp(i)!tb_numcodl) > 2100 Then
              dtrTmp(i)!tb_numcodl = 1900
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780449899208032, "Inserire un anno compreso tra 1900 e 2100")))
              Return False
            End If
          Case "RO"
            If NTSCInt(dtrTmp(i)!tb_numcodl) < 1900 Or NTSCInt(dtrTmp(i)!tb_numcodl) > 2100 Then
              dtrTmp(i)!tb_numcodl = 1900
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780449928841719, "Inserire un anno compreso tra 1900 e 2100")))
              Return False
            End If
          Case "!"
            If NTSCInt(dtrTmp(i)!tb_numcodl) < 1900 Or NTSCInt(dtrTmp(i)!tb_numcodl) > 2100 Then
              dtrTmp(i)!tb_numcodl = 1900
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780449948597511, "Inserire un anno compreso tra 1900 e 2100")))
              Return False
            End If
          Case "PD"
            If NTSCInt(dtrTmp(i)!tb_numcodl) < 1900 Or NTSCInt(dtrTmp(i)!tb_numcodl) > 2100 Then
              dtrTmp(i)!tb_numcodl = 1900
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780449963806335, "Inserire un anno compreso tra 1900 e 2100")))
              Return False
            End If
          Case "AA"
            If NTSCStr(dtrTmp(i)!xx_codroot) = "" Then
              Return False
            End If
            If NTSCStr(dtrTmp(i)!tb_numserie) <> NTSCStr(dtrTmp(i)!xx_codroot) Then
              dtrTmp(i)!tb_numserie = NTSCStr(dtrTmp(i)!xx_codroot)
            End If
        End Select

        If IsInCodiceVarie(strTipoSelected) = False Then ' se non varie
          Dim strTipo As String = ""
          Select Case strTipoSelected
            Case "RO"
              strTipo = NTSCStr(dtrTmp(i)!tb_numtipo)
            Case Else
              strTipo = strTipoSelected
          End Select
          If Not oCldNume.CheckTable(strDittaCorrente, strTipo, dtrTmp(i).Item("tb_numserie").ToString(), NTSCInt(dtrTmp(i).Item("tb_numcodl")), dtrTmp(i).RowState) Then
            Select Case strTipoSelected
              Case "CC", "FF", "SS", "SA"
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129820680407678955, "Mastro già esistente.")))
                Return False
              Case Else
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128919520353664126, "Serie e anno già esistenti.")))
                Return False
            End Select
          End If
        End If

        If IsInCodiceVarie(strTipoSelected) Then
          If dsShared.Tables("TABNUMA").Rows.Count > 1 Then
            dtrTmp(i).Delete()
            Return False
          End If
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
      Return False
    End Try
  End Function
  Public Overridable Function LeggiDatiDitta(ByVal strDitta As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not oCldNume.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttTmp) Then Return False

      '--- Prende il codice Piano dei Conti dall'Anagrafica Ditta
      strCodpcon = NTSCStr(dttTmp.Rows(0).Item("tb_azcodpcon"))

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

#Region "Before e AfterColUpdate"
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

  Public Overridable Function BeforeColUpdate_tb_numserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), False)
      If strTmp <> NTSCStr(e.ProposedValue) Then e.ProposedValue = strTmp

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
  Public Overridable Function BeforeColUpdate_xx_numserie2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), False)
      If strTmp <> NTSCStr(e.ProposedValue) Then e.ProposedValue = strTmp

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
  Public Overridable Function BeforeColUpdate_tb_alfpro(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), False)
      If strTmp <> NTSCStr(e.ProposedValue) Then e.ProposedValue = strTmp

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
#End Region

  Public Overridable Sub FileElabora()
    Dim i As Integer
    Dim strAnnoIni As String
    Dim strAnnoFin As String
    Dim evt As NTSEventArgs = Nothing
    Dim dsTmp As DataSet = Nothing
    Try
      evt = New NTSEventArgs(CLN__STD.ThMsg.INPUTBOX, oApp.Tr(Me, 128358959589645091, "Inserire l'anno di riferimento"))
      evt.InputValue = "0"
      ThrowRemoteEvent(evt)
      strAnnoIni = evt.RetValue
      If Not (IsNumeric(strAnnoIni) And Len(strAnnoIni) = 4) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780450084536175, "L'anno inserito non è corretto")))
        Return
      Else
        If Not (CDbl(strAnnoIni) >= 1900 And CDbl(strAnnoIni) <= 2099) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780450118560039, "L'anno inserito non è corretto")))
          Return
        End If
      End If
      evt = New NTSEventArgs(CLN__STD.ThMsg.INPUTBOX, oApp.Tr(Me, 128358964017095307, "Inserire l'anno da generare"))
      evt.InputValue = (NTSCInt(strAnnoIni) + 1).ToString
      ThrowRemoteEvent(evt)
      strAnnoFin = evt.RetValue
      If Not (IsNumeric(strAnnoFin) And Len(strAnnoFin) = 4) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780450149448063, "L'anno inserito non è corretto")))
        Return
      Else
        If Not (CDbl(strAnnoFin) >= 1900 And CDbl(strAnnoFin) <= 2099) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128780450186764559, "L'anno inserito non è corretto")))
          Return
        End If
      End If

      '--- Chiede conferma della generazione
      evt = (New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128358974178658613, "Procedere con la generazione dell'anno |" & strAnnoFin & "|" & vbCrLf & _
        "dall'anno |" & strAnnoIni & "|?")))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then
        Exit Sub
      End If

      '--- Apre uno dataset sui records di riferimento
      oCldNume.FileElaboraGetTabNuma(strDittaCorrente, strAnnoIni, dsTmp)

      '--- Se non essitono dati di origine avvisa ed esce
      If dsTmp.Tables("TABNUMA").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128358978768446589, "Non esistono numerazioni di riferimento nell'anno |" & strAnnoIni & "|." & vbCrLf & _
          "Elaborazione non possibile.")))
        Exit Sub
        '--- Procede con l'elaborazione
      Else
        For i = 0 To dsTmp.Tables("TABNUMA").Rows.Count - 1
          With dsTmp.Tables("TABNUMA").Rows(i)
            oCldNume.FileElaboraSetTabNuma(strDittaCorrente, strAnnoIni, strAnnoFin, NTSCStr(!tb_numtipo), NTSCStr(!tb_numserie))
          End With
        Next
        ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 128358977559030277, "Inizializzazione nuovo anno terminata!")))
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

  Public Overridable Function IsInCodice(ByVal strTipoSelected As String) As Boolean
    Try
      Select Case strTipoSelected
        'non varie e non varie BS--NUMX
        Case "CC", "FF", "SS", "SA", "R", "O", "Q", "A", "L", "B", "M", "C", "Z", "Y", "H", "P", "EA", "DA", "W", "X", "T", "U", "G", "SM", "F", "S", "V", "CP", "CQ", "AA", "CL", "NC", "$", "VV", "#", "RO", "P1", "P2", "P3", "!", "PD", "DI"
          Return True
          'varie e varie BS--NUMX
        Case "PA", "OP", "OL", "OO", "CO", "LO", "BC", "BG", "RT", "CE", "IC", "EL", "MF", "FL", "MT", "CP2", "DT", "OG", "CQ2", "SP", "AP", "TP", "MA", "IM", "F4", "V1", "PK", "C8", "C7", "C9", "C6", "C3", "C4", "C5", "C2", "C1", "GP", "PR", "UC", "ID"
          Return True
          'NTS
        Case "AD", "AF"
          Return True
      End Select

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
  Public Overridable Function IsInCodiceVarie(ByVal strTipoSelected As String) As Boolean
    Try
      Select Case strTipoSelected
        'varie e varie BS--NUMX
        Case "PA", "OP", "OL", "OO", "CO", "LO", "BC", "BG", "RT", "CE", "IC", "EL", "MF", "FL", "MT", "CP2", "DT", "OG", "CQ2", "SP", "AP", "TP", "MA", "IM", "F4", "V1", "PK", "C8", "C7", "C9", "C6", "C3", "C4", "C5", "C2", "C1", "GP", "PR", "UC", "ID"
          Return True
          'NTS
        Case "AD", "AF"
          Return True
      End Select

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

End Class
