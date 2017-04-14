Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO

Public Class CLEMGARTV
  Inherits CLE__BASE

  Public oCldArtv As CLDMGARTV

  'opzioni registro
  Public nCodiva As Integer
  Public nControa As Integer
  Public nControp As Integer
  Public nContros As Integer
  Public strUnmis As String
  Public strInizioValListini As String
  Public strInizioValSconti As String
  Public strInizioValProvv As String
  Public bConver9Dec As Boolean
  Public bSbloccaFLotto As Boolean
  Public strListiniAbilitati As String
  Public bGestTabUnmis As Boolean
  Public bAbilitaPrezzoUM As Boolean
  Public bCreaBarcodeE13 As Boolean
  Public bSecondaDescrizione As Boolean
  Public bIndicod As Boolean
  Public strPrefixEAN13 As String
  Public bValidaNomenclCombin As Boolean
  Public bDuplicaListini As Boolean
  Public bDuplicaSconti As Boolean
  Public bDuplicaProvvigioni As Boolean
  Public bDuplicaFasi As Boolean
  Public bDuplicazioneInCorso As Boolean
  Public bDuplica As Boolean

  Public bServer As Boolean
  Public strUnmisOrigine As String
  Public strUnmis2Origine As String
  Public strConfez2Origine As String
  Public strUm4Origine As String
  Public strWhereFiar As String
  Public strCodart As String
  Public bNew As Boolean = False
  Public bVarNew As Boolean = False
  Public bArticoNew As Boolean = False
  Public bDaGest As Boolean

  Public strArtvCodrootDelete As String    'codice articolo root per cancellazione
  Public strTipoOpzDelete As String
  Public nCodtipaDelete As Integer
  Public strImageDir As String

  Public strArtvDescr As String
  Public strArtvDesint As String
  Public lArtvForn As Integer
  Public strArtvNote As String

  Public strListaFieldNameArtico() As String

  Public nArtvNumliv As Integer
  Public nArtvLungroot As Integer
  Public nArtvLungvar1 As Integer
  Public nArtvLungvar2 As Integer
  Public nArtvLungvar3 As Integer
  Public strArtvCodroot As String
  Public strArtvPrevar As String
  Public strNarvCodvari1 As String
  Public strNarvCodvari2 As String
  Public strNarvCodvari3 As String
  Public nNumliv As Integer
  Public nLungvar1 As Integer
  Public nLungvar2 As Integer
  Public nLungvar3 As Integer
  Public strPrevar As String

  Public dsVar1Shared As DataSet
  Public dsVar2Shared As DataSet
  Public dsVar3Shared As DataSet
  Public dsArticoShared As DataSet
  Public bVar1HasChanges As Boolean
  Public bVar2HasChanges As Boolean
  Public bVar3HasChanges As Boolean
  Public bArticoHasChanges As Boolean

  Public bLogisticaEstesa As Boolean
  Public bGestUbicSenzaLext As Boolean

  Public bCampiCAEAttivi As Boolean

  Public nArtvLungrootOld As Integer

  'mgaarv
  Public bModTCO As Boolean

  'mgnarv
  Public strGeneraCodice As String
  Public nLungPreRoot As Integer
  Public bArticoloGenerato As Boolean
  Public lProgressivo As Integer
  Public strRadice As String
  Public nLungExt As Integer
  Public lProgr As Integer
  Public nLungRoot As Integer

  'tcduar
  Public strCodartDuar As String
  Public bDuarArticoloGenerato As Boolean
  Public lDuarProgr As Integer
  Public strDuarRoot As String
  Public bDuarGeneraArticoli As Boolean

  Public bNoMsgCongruenzaPolSconte As Boolean = False

  Public lCountLog As Integer = 0

  'mgarta
  Public dsArtaShared As DataSet
  Public bArtaHasChanges As Boolean = False
  Public bSucc, bAcc As Boolean
  Public bRicreaSuccedaneiAccessori As Boolean

  Public bSelCodiceNoApri As Boolean = False

  Private Moduli_P As Integer = bsModMG + bsModVE + bsModPM
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtCRM
  Private ModuliSup_P As Integer = bsModSupWCR
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGARTV"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldArtv = CType(MyBase.ocldBase, CLDMGARTV)
    oCldArtv.Init(oApp)
    Return True
  End Function

  Public Overridable Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldArtv.GetDataApri(strDittaCorrente, strArtvCodroot, ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldArtv.SetTableDefaultValueFromDB("ARTICO", ds)

      CaricaColonneUnbound(ds.Tables("ARTICO").Rows(0))

      SetDefaultValue(ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("ARTICO").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("ARTICO").ColumnChanged, AddressOf AfterColUpdate

      bHasChanges = False

      bNew = False

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

  Public Overridable Sub Nuovo(ByRef ds As DataSet)
    Dim dReturn As Boolean = False
    Try
      '----------------------------------------
      dReturn = oCldArtv.GetDataNuovo(strDittaCorrente, ds)
      If dReturn = False Then Return

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldArtv.SetTableDefaultValueFromDB("ARTICO", ds)

      SetDefaultValue(ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("ARTICO").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("ARTICO").ColumnChanged, AddressOf AfterColUpdate

      'inserisco una nuova riga
      ds.Tables("ARTICO").Rows.Clear()
      ds.Tables("ARTICO").Rows.Add(ds.Tables("ARTICO").NewRow)

      bHasChanges = True

      bNew = True

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

  Public Overrides Function Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables(strNomeTabella).Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      bNew = False

      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Dim strDescr As String = ""
    Try
      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = ocldBase.GetSettingBus("BSMGARTV", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BSMGARTV"
      strActLogNomOggLog = "ARTICO"
      strActLogDesLog = oApp.Tr(Me, 128550728330311752, "Anagrafica Articoli a varianti")

      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("ARTICO").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("ARTICO").Columns("ar_codart").DefaultValue = strArtvCodroot
      ds.Tables("ARTICO").Columns("ar_unmis").DefaultValue = strUnmis
      ds.Tables("ARTICO").Columns("ar_conver").DefaultValue = 1
      ds.Tables("ARTICO").Columns("ar_codiva").DefaultValue = nCodiva
      ds.Tables("ARTICO").Columns("ar_controp").DefaultValue = nControp
      ds.Tables("ARTICO").Columns("ar_controa").DefaultValue = nControa
      ds.Tables("ARTICO").Columns("ar_contros").DefaultValue = nContros
      ds.Tables("ARTICO").Columns("ar_ultagg").DefaultValue = Now.ToShortDateString
      ds.Tables("ARTICO").Columns("ar_oragg").DefaultValue = (Now.Hour * 100) + Now.Minute
      ds.Tables("ARTICO").Columns("ar_datins").DefaultValue = Now.ToShortDateString
      ds.Tables("ARTICO").Columns("ar_orins").DefaultValue = (Now.Hour * 100) + Now.Minute
      ds.Tables("ARTICO").Columns("ar_gesvar").DefaultValue = "S"
      ds.Tables("ARTICO").Columns("ar_prevar").DefaultValue = strPrevar
      ds.Tables("ARTICO").Columns("ar_perragg").DefaultValue = "G"
      ds.Tables("ARTICO").Columns("ar_webvis").DefaultValue = "N"
      ds.Tables("ARTICO").Columns("ar_webusat").DefaultValue = "N"
      ds.Tables("ARTICO").Columns("ar_webvend").DefaultValue = "N"

      If strNarvCodvari1 <> "" Then
        ds.Tables("ARTICO").Columns("ar_flmod").DefaultValue = strNarvCodvari1
        DecodCodvari1(strNarvCodvari1, strDescr)
        ds.Tables("ARTICO").Columns("xx_flmod").DefaultValue = strDescr
      End If

      DecodCodciva(NTSCStr(nCodiva), strDescr)
      ds.Tables("ARTICO").Columns("xx_codiva").DefaultValue = strDescr
      DecodCodcove(NTSCStr(nControp), strDescr)
      ds.Tables("ARTICO").Columns("xx_controp").DefaultValue = strDescr
      DecodCodcove(NTSCStr(nControa), strDescr)
      ds.Tables("ARTICO").Columns("xx_controa").DefaultValue = strDescr
      DecodCodcove(NTSCStr(nContros), strDescr)
      ds.Tables("ARTICO").Columns("xx_contros").DefaultValue = strDescr

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
      If ValoriUguali(NTSCStr(e.ProposedValue), e.Row(e.Column.ColumnName).ToString) Then
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

  Public Overridable Sub BeforeColUpdate_ar_unmis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.ProposedValue = ""
        Exit Sub
      Else
        If bGestTabUnmis = False Then Return
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABUMIS", "S", "", dttTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129034655766461449, "Unità  di misura principale inesistente.")))
          Return
        Else
          If NTSCStr(dttTmp.Rows(0)!tb_codumis) <> NTSCStr(e.ProposedValue) Then e.ProposedValue = NTSCStr(dttTmp.Rows(0)!tb_codumis)
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
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_ar_gif1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.ProposedValue) <> "" Then
        If TrovaGif(NTSCStr(e.ProposedValue)) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556840111394682, "Il file immagine indicato non esiste nella cartella di Business.")))
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
  Public Overridable Sub BeforeColUpdate_ar_gif2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.ProposedValue) <> "" Then
        If TrovaGif(NTSCStr(e.ProposedValue)) = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556841197891679, "Il file immagine indicato non esiste nella cartella di Business.")))
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
  Public Overridable Sub BeforeColUpdate_ar_flmod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.ProposedValue = ""
        e.Row!xx_flmod = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABVARI", "S", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556843657949781, "Cod. variante 1 inesistente.")))
          Return
        Else
          e.Row!xx_flmod = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_codimba(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_codimba = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABIMBA", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556846711523871, "Codice imballo inesistente")))
          Return
        Else
          e.Row!xx_codimba = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_confez2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.ProposedValue = ""
        Exit Sub
      Else
        If bGestTabUnmis = False Then Return
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABUMIS", "S", "", dttTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556848809468425, "Unità di misura secondaria inesistente.")))
          Return
        Else
          If NTSCStr(dttTmp.Rows(0)!tb_codumis) <> NTSCStr(e.ProposedValue) Then e.ProposedValue = NTSCStr(dttTmp.Rows(0)!tb_codumis)
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
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_ar_ggragg(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      Select Case NTSCStr(e.Row!ar_perragg)
        Case "G"
          If Not (NTSCInt(e.ProposedValue) >= 1 And NTSCInt(e.ProposedValue) <= 999) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "S"
          If Not (NTSCInt(e.ProposedValue) >= 1 And NTSCInt(e.ProposedValue) <= 7) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "D"
          If Not (NTSCInt(e.ProposedValue) >= 1 And NTSCInt(e.ProposedValue) <= 10) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "Q"
          If Not (NTSCInt(e.ProposedValue) >= 1 And NTSCInt(e.ProposedValue) <= 15) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "M"
          If Not (NTSCInt(e.ProposedValue) >= 1 And NTSCInt(e.ProposedValue) <= 31) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "B"
          If Not (NTSCInt(e.ProposedValue) >= 1 And NTSCInt(e.ProposedValue) <= 62) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "T"
          If Not (NTSCInt(e.ProposedValue) >= 1 And NTSCInt(e.ProposedValue) <= 93) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "R"
          If Not (NTSCInt(e.ProposedValue) >= 1 And NTSCInt(e.ProposedValue) <= 124) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "U"
          If Not (NTSCInt(e.ProposedValue) >= 1 And NTSCInt(e.ProposedValue) <= 186) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
        Case "A"
          If Not (NTSCInt(e.ProposedValue) >= 1 And NTSCInt(e.ProposedValue) <= 366) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Exit Sub
          End If
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
  End Sub
  Public Overridable Sub BeforeColUpdate_ar_um4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.ProposedValue = ""
        Exit Sub
      Else
        If bGestTabUnmis = False Then Return
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABUMIS", "S", "", dttTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556870497974820, "Unità di misura formula inesistente.")))
          Return
        Else
          If NTSCStr(dttTmp.Rows(0)!tb_codumis) <> NTSCStr(e.ProposedValue) Then e.ProposedValue = NTSCStr(dttTmp.Rows(0)!tb_codumis)
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
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_ar_unmis2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.ProposedValue = ""
        Exit Sub
      Else
        If bGestTabUnmis = False Then Return
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABUMIS", "S", "", dttTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556871493161665, "Unità di misura secondaria inesistente.")))
          Return
        Else
          If NTSCStr(dttTmp.Rows(0)!tb_codumis) <> NTSCStr(e.ProposedValue) Then e.ProposedValue = NTSCStr(dttTmp.Rows(0)!tb_codumis)
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
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_ar_codalt(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.ProposedValue) <> "" And InStr(NTSCStr(e.ProposedValue), "'") <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556873874417855, "Il Codice alternativo non può contenere apici semplici.")))
        Exit Sub
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
  Public Overridable Sub BeforeColUpdate_ar_codappr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_codappr = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABAPPR", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556875298168273, "Codice approvigionatore inesistente")))
          Return
        Else
          e.Row!xx_codappr = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Dim dtrTmp() As DataRow
    Try
      If Trim(NTSCStr(e.ProposedValue)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557399501323084, "Codice articolo inesistente")))
        Return
      End If
      If InStr(NTSCStr(e.ProposedValue), "'") <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557399756949464, "Il Codice articolo non può contenere apici semplici.")))
        Return
      End If
      e.ProposedValue = UCase(NTSCStr(e.ProposedValue))

      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("ARTICO").Select("ar_codart = " & CStrSQL(e.ProposedValue.ToString), Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128550728330467928, "Il codice inserito è già stato utilizzato. Inserire un codice non utilizzato")))
        Return
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
  Public Overridable Sub BeforeColUpdate_ar_coddb(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If Trim(NTSCStr(e.ProposedValue)) = "" And InStr(NTSCStr(e.ProposedValue), "'") <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557402032095452, "Il Codice distinta base non può contenere apici semplici.")))
        Return
      End If
      e.ProposedValue = UCase(NTSCStr(e.ProposedValue))

      If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "DISTBAS", "S") Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734634581281849, "Non esiste distinta base per questo articolo")))
        Return
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
  Public Overridable Sub BeforeColUpdate_ar_codiva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_codiva = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCIVA", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557467865369227, "Codice iva inesistente")))
          Return
        Else
          e.Row!xx_codiva = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_codmarc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_codmarc = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMARC", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557469014562244, "Codice marca inesistente")))
          Return
        Else
          e.Row!xx_codmarc = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_codpdon(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_codpdon = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPDON", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734634419718315, "Codice relaz. listini inesistente")))
          Return
        Else
          e.Row!xx_codpdon = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_codvuo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_codvuo = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCVUO", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557470380215526, "Codice vuoto inesistente")))
          Return
        Else
          e.Row!xx_codvuo = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_controa(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_controa = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOVE", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734634285654309, "Codice contropartita acq. inesistente")))
          Return
        Else
          e.Row!xx_controa = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_controp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_controp = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOVE", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557471927476626, "Codice contropartita vend. inesistente")))
          Return
        Else
          e.Row!xx_controp = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_contros(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_contros = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOVE", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557472245212163, "Codice contropartita scarico produzione inesistente")))
          Return
        Else
          e.Row!xx_contros = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_famprod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCStr(e.ProposedValue) = " " Then
        e.ProposedValue = " "
        e.Row!xx_famprod = ""
        Exit Sub
      Else
        e.ProposedValue = UCase(NTSCStr(e.ProposedValue))
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCFAM", "S", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557473902813297, "Codice famiglia inesistente.")))
          Return
        Else
          e.Row!xx_famprod = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_forn(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_forn = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444317587516, "Attenzione!" & vbCrLf & _
          "Codice fornitore 1 inesistente.")))
        Return
      Else
        If NTSCStr(dttTmp.Rows(0)!an_tipo) = "S" Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129501230547666016, "Attenzione!" & vbCrLf & _
            "Non è possibile indicare un Sottoconto nel campo 'Codice fornitore 1'.")))
          Return
        Else
          e.Row!xx_forn = NTSCStr(dttTmp.Rows(0)!an_descr1)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_ar_forn2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_forn2 = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129501232083281250, "Attenzione!" & vbCrLf & _
          "Codice fornitore 2 inesistente.")))
        Return
      Else
        If NTSCStr(dttTmp.Rows(0)!an_tipo) = "S" Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129501232252099610, "Attenzione!" & vbCrLf & _
            "Non è possibile indicare un Sottoconto nel campo 'Codice fornitore 2'.")))
          Return
        Else
          e.Row!xx_forn2 = NTSCStr(dttTmp.Rows(0)!an_descr1)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub BeforeColUpdate_ar_gruppo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_gruppo = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABGMER", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557489107544951, "Codice gruppo inesistente")))
          Return
        Else
          e.Row!xx_gruppo = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_magprod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_magprod = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAGA", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557491225104612, "Codice magazzino produzione inesistente")))
          Return
        Else
          e.Row!xx_magprod = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_magstock(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_magstock = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAGA", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557491645522022, "Codice magazzino stoccaggio inesistente")))
          Return
        Else
          e.Row!xx_magstock = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_numecr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_numecr = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCENA", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557494091444872, "Codice centro C.A. inesistente")))
          Return
        Else
          e.Row!xx_numecr = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_sotgru(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Dim nGrup As Integer
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_sotgru = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSGME", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557501483916812, "Codice sottogruppo inesistente.")))
          Return
        Else
          If NTSCInt(e.Row!ar_gruppo) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734633954554202, "Indicare il gruppo prima di inserire il sottogruppo.")))
            e.ProposedValue = 0
            e.Row!xx_sotgru = ""
            Exit Sub
          Else
            nGrup = NTSCInt(Fix(NTSCInt(e.ProposedValue) / 100))
            If NTSCInt(e.ProposedValue) > 0 And (nGrup <> NTSCInt(e.Row!ar_gruppo)) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557504221967804, "Sottogruppo non appartenente al gruppo selezionato")))
              e.ProposedValue = 0
              e.Row!xx_sotgru = ""
              Exit Sub
            End If
          End If
          e.Row!xx_sotgru = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_clascon(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_clascon = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCSAR", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128599081731858860, "Codice classe sconto inesistente")))
          Return
        Else
          e.Row!xx_clascon = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_claprov(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_claprov = ""
        Exit Sub
      Else
        If Not oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCPAR", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128599082411189660, "Codice classe provvigione inesistente")))
          Return
        Else
          e.Row!xx_claprov = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_codtcdc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xx_codtcdc = "" : Return

      If oCldArtv.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABTCDC", "N", , dttTmp) Then
        If NTSCStr(dttTmp.Rows(0)!tb_tipork) = "A" Then
          e.Row!xx_codtcdc = dttTmp.Rows(0)!tb_destcdc
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701344910501, "La Tipologia entità deve essere di tipo 'Articolo\Fase'")))
          e.ProposedValue = e.Row!ar_codtcdc
        End If
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701332254575, "Tipologia entità inesistente")))
        e.ProposedValue = e.Row!ar_codtcdc
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
  Public Overridable Sub BeforeColUpdate_ar_coddica(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If NTSCStr(e.ProposedValue).Trim = "" Then e.ProposedValue = " " : e.Row!xx_coddica = "" : e.Row!ar_coddicv = "" : Return

      e.ProposedValue = NTSCStr(e.ProposedValue).ToUpper

      If oCldArtv.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABDICA", "S", , dttTmp) Then
        If NTSCStr(dttTmp.Rows(0)!tb_applicaa) = "A" Then
          If NTSCInt(dttTmp.Rows(0)!tb_liv) <> 1 Then
            e.Row!xx_coddica = dttTmp.Rows(0)!tb_desdica
            e.Row!ar_coddicv = ""
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701312880071, "Il codice di aggregazione budget non deve essere di primo livello")))
            e.ProposedValue = e.Row!ar_coddica
          End If
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701302567835, "Il codice di aggregazione budget deve essere per 'Articolo\Fase'")))
          e.ProposedValue = e.Row!ar_coddica
        End If
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701289911909, "Aggregazione budget inesistente")))
        e.ProposedValue = e.Row!ar_coddica
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
  Public Overridable Sub BeforeColUpdate_ar_coddicv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If NTSCStr(e.ProposedValue).Trim = "" Then e.Row!xx_coddicv = "" : e.ProposedValue = " " : Return

      If NTSCStr(e.Row!ar_coddica).Trim = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701049449315, "Prima inserire un codice di aggregazione budget.")))
        e.ProposedValue = " "
        Return
      End If

      e.ProposedValue = NTSCStr(e.ProposedValue).ToUpper

      If oCldArtv.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABDICV", "S", , dttTmp, NTSCStr(e.Row!ar_coddica)) Then
        e.Row!xx_coddicv = dttTmp.Rows(0)!tb_desdicv
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701272568603, "Valore aggregazione budget inesistente")))
        e.ProposedValue = e.Row!ar_coddicv
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
  Public Overridable Sub BeforeColUpdate_ar_codtlox(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_codtlox = ""
        Return
      End If

      If oCldArtv.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABLOTX", "N", strTmp) Then
        e.Row!xx_codtlox = strTmp
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129512424011131653, "Codice modalità creazione lotto inesistente")))
        e.ProposedValue = e.Row!ar_ar_codtlox
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
  Public Overridable Sub BeforeColUpdate_ar_reparto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strDescr As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xx_reparto = "" : Return

      oCldArtv.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABREAR", "N", strDescr)
      e.Row!xx_reparto = strDescr
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
  Public Overridable Sub BeforeColUpdate_ar_codseat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_codseat = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSEAT", "N", strOut) = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415074997357338, "Codice Set di Attributi inesistente.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_codseat = strOut
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
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

  Public Overrides Function TestPreSalva() As Boolean
    Dim bDtorgif1 As Boolean = False
    Dim bDtorgif2 As Boolean = False
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim nGrup As Integer
    Dim dValmin As Decimal
    Dim dttTmp As New DataTable
    Dim dsTmp As DataSet = Nothing
    Dim strMsgOut As String = ""
    Try
      If dsArticoShared.Tables("ARTICO").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128623987314606352, "Inserire almeno una riga nell'analitico varianti.")))
        Return False
      End If

      dtrTmp = dsShared.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If dtrTmp(i).RowState = DataViewRowState.Added Then
          '------------------------------------------------------------------------
          'Controlli sui campi
          If NTSCStr(dtrTmp(i)!ar_codart) = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556778973785080, "Codice articolo obbligatorio.")))
            Return False
          End If
          If InStr(NTSCStr(dtrTmp(i)!ar_codart), "'") <> 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734633818770345, "Il Codice articolo non può contenere apici semplici.")))
            Return False
          End If
        End If

        If NTSCDec(dtrTmp(i)!ar_qtacon2) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129521045093356227, "Attenzione!" & vbCrLf & _
            "Indicare una quantitÃ Â relativa a Unita di mMisura 'Confezione' valida.")))
          Return False
        End If

        If NTSCInt(dtrTmp(i)!ar_gruppo) <> 0 Then
          nGrup = NTSCInt(Fix(NTSCInt(dtrTmp(i)!ar_sotgru) / 100))
          If NTSCInt(dtrTmp(i)!ar_sotgru) > 0 And (nGrup <> NTSCInt(dtrTmp(i)!ar_gruppo)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128558584717772870, "Sottogruppo non appartenente al gruppo selezionato")))
            Return False
          End If
        End If
        If Not ControllaUnitaDiMisura(dtrTmp(i)) Then
          Return False
        End If
        If NTSCStr(dtrTmp(i)!ar_codalt) <> "" And (InStr(NTSCStr(dtrTmp(i)!ar_codalt), "'") <> 0) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556782912234801, "Il Codice alternativo non può contenere apici semplici.")))
          Return False
        End If
        If NTSCStr(dtrTmp(i)!ar_descr) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556784148132291, "Descrizione articolo obbligatorio.")))
          Return False
        End If
        If Trim(NTSCStr(dtrTmp(i)!ar_unmis)) = "" And NTSCStr(dtrTmp(i)!ar_codart) <> "D" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556784482247544, "Unità di misura principale obbligatoria.")))
          Return False
        End If

        Select Case NTSCStr(dtrTmp(i)!ar_polriord)
          Case "M", "N"
            If bNoMsgCongruenzaPolSconte = True Then
              dValmin = 0
            Else
              dValmin = NTSCDec(0.001)
            End If
          Case Else : dValmin = 0
        End Select
        If NTSCDec(dtrTmp(i)!ar_scomin) < dValmin Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734633571734352, "Attenzione!" & vbCrLf & _
            "La 'Scorta minima' deve essere maggiore di 0 quando la Politica di Riordino è di tipo:" & vbCrLf & _
            " --> A punto di riordino con lotto" & vbCrLf & _
            " --> A punto di riordino a ricostruzione scorta")))
          Return False
        End If

        Select Case NTSCStr(dtrTmp(i)!ar_polriord)
          Case "N"
            If bNoMsgCongruenzaPolSconte = True Then
              dValmin = 0
            Else
              dValmin = NTSCDec(0.001)
            End If
          Case Else : dValmin = 0
        End Select
        If NTSCDec(dtrTmp(i)!ar_scomax) < dValmin Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734633619860276, "Attenzione!" & vbCrLf & _
            "La 'Scorta massima' deve essere maggiore di 0 quando la Politica di Riordino è di tipo:" & vbCrLf & _
            " --> A punto di riordino a ricostruzione scorta")))
          Return False
        End If

        Select Case NTSCStr(dtrTmp(i)!ar_polriord)
          Case "F", "M", "O"
            If bNoMsgCongruenzaPolSconte = True Then
              dValmin = 0
            Else
              dValmin = NTSCDec(0.001)
            End If
          Case Else : dValmin = 0
        End Select
        If NTSCDec(dtrTmp(i)!ar_minord) < dValmin Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734633641423190, "Attenzione!" & vbCrLf & _
            "La 'Quantità lotto standard' deve essere maggiore di 0 quando la Politica di Riordino è di tipo:" & vbCrLf & _
            " --> A punto di riordino con lotto" & vbCrLf & _
            " --> Su fabbisogno con lotto" & vbCrLf & _
            " --> Su fabbisogno con lotto minimo")))
          Return False
        End If

        If NTSCDec(dtrTmp(i)!ar_perqta) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734633684549018, "Il Molt.qtà/prezzo deve essere maggiore di 0")))
          Return False
        End If
        If bNoMsgCongruenzaPolSconte = False Then
          If (NTSCStr(dtrTmp(i)!ar_polriord) = "N") And (NTSCInt(dtrTmp(i)!ar_scomin) >= NTSCInt(dtrTmp(i)!ar_scomax)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556790657517179, "La scorta massima deve essere superiore a quella minima.")))
            Return False
          End If
        Else
          If (NTSCStr(dtrTmp(i)!ar_polriord) = "N") And (NTSCInt(dtrTmp(i)!ar_scomin) > NTSCInt(dtrTmp(i)!ar_scomax)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319526712213651, "La scorta massima deve essere superiore a quella minima.")))
            Return False
          End If
        End If
        If (NTSCStr(dtrTmp(i)!ar_coddb) <> "") And (InStr(NTSCStr(dtrTmp(i)!ar_coddb), "'") <> 0) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556790763029467, "Il Codice distinta base non può contenere apici semplici.")))
          Return False
        End If

        If Trim(NTSCStr(dtrTmp(i)!ar_um4)) = "" And Trim(NTSCStr(dtrTmp(i)!ar_formula)) <> "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559387522405278, "Se è indicata la formula di trasformazione in UMP, l'unità di misura relativa non può essere nulla.")))
          Return False
        End If

        If Trim(NTSCStr(dtrTmp(i)!ar_um4)) <> "" And Trim(NTSCStr(dtrTmp(i)!ar_formula)) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734633408918726, "Se è indicata l'unità di misura formula, la formula di trasformazione in UMP non può essere nulla.")))
          Return False
        End If

        Select Case NTSCStr(dtrTmp(i)!ar_umdapr)
          Case "C"
            If Trim(NTSCStr(dtrTmp(i)!ar_confez2)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559397233522474, "Se è in Unità di Misura vendite è indicata 'Confezione', la Confezione non può essere nulla.")))
              Return False
            End If
          Case "Q"
            If Trim(NTSCStr(dtrTmp(i)!ar_um4)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559397255860174, "Se è in Unità di Misura vendite è indicata 'Formula', l'Unità di Misura Formula non può essere nulla.")))
              Return False
            End If
          Case "S"
            If Trim(NTSCStr(dtrTmp(i)!ar_unmis2)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559397274847219, "Se è in Unità di Misura vendite è indicata 'Secondaria', l'Unità di Misura Secondaria non può essere nulla.")))
              Return False
            End If
        End Select

        Select Case NTSCStr(dtrTmp(i)!ar_umdapra)
          Case "C"
            If Trim(NTSCStr(dtrTmp(i)!ar_confez2)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559398259939789, "Se è in Unità di Misura carichi è indicata 'Confezione', la Confezione non può essere nulla.")))
              Return False
            End If
          Case "Q"
            If Trim(NTSCStr(dtrTmp(i)!ar_um4)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559398284511259, "Se è in Unità di Misura carichi è indicata 'Formula', l'Unità di Misura Formula non può essere nulla.")))
              Return False
            End If
          Case "S"
            If Trim(NTSCStr(dtrTmp(i)!ar_unmis2)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559398308763619, "Se è in Unità di Misura carichi è indicata 'Secondaria', l'Unità di Misura Secondaria non può essere nulla.")))
              Return False
            End If
        End Select

        Select Case NTSCStr(dtrTmp(i)!ar_umpdapr)
          Case "C"
            If Trim(NTSCStr(dtrTmp(i)!ar_confez2)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559399596053359, "Se è in Unità di Misura prezzo di vendita è indicata 'Confezione', la Confezione non può essere nulla.")))
              Return False
            End If
          Case "Q"
            If Trim(NTSCStr(dtrTmp(i)!ar_um4)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559399574832544, "Se è in Unità di Misura prezzo di vendita è indicata 'Formula', l'Unità di Misura Formula non può essere nulla.")))
              Return False
            End If
          Case "S"
            If Trim(NTSCStr(dtrTmp(i)!ar_unmis2)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559399539251779, "Se è in Unità di Misura prezzo di vendita è indicata 'Secondaria', l'Unità di Misura Secondaria non può essere nulla.")))
              Return False
            End If
        End Select

        Select Case NTSCStr(dtrTmp(i)!ar_umpdapra)
          Case "C"
            If Trim(NTSCStr(dtrTmp(i)!ar_confez2)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559400286512569, "Se è in Unità di Misura prezzo di acquisto è indicata 'Confezione', la Confezione non può essere nulla.")))
              Return False
            End If
          Case "Q"
            If Trim(NTSCStr(dtrTmp(i)!ar_um4)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559400302754536, "Se è in Unità di Misura prezzo di acquisto è indicata 'Formula', l'Unità di Misura Formula non può essere nulla.")))
              Return False
            End If
          Case "S"
            If Trim(NTSCStr(dtrTmp(i)!ar_unmis2)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559400321519527, "Se è in Unità di Misura prezzo di acquisto è indicata 'Secondaria', l'Unità di Misura Secondaria non può essere nulla.")))
              Return False
            End If
        End Select

        Select Case NTSCStr(dtrTmp(i)!ar_perragg)
          Case "G"
            If Not (NTSCInt(dtrTmp(i)!ar_ggragg) >= 1 And NTSCInt(dtrTmp(i)!ar_ggragg) <= 999) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377646247523, "Se il Periodo di Raggruppamento indicato è 'Giorno', i giorni di raggruppamento possono essere maggiori di '1'")))
              Return False
            End If
          Case "S"
            If Not (NTSCInt(dtrTmp(i)!ar_ggragg) >= 1 And NTSCInt(dtrTmp(i)!ar_ggragg) <= 7) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377619100298, "Se il Periodo di Raggruppamento indicato è 'Settimana', i giorni di raggruppamento possono valere da '1' a '7'.")))
              Return False
            End If
          Case "D"
            If Not (NTSCInt(dtrTmp(i)!ar_ggragg) >= 1 And NTSCInt(dtrTmp(i)!ar_ggragg) <= 10) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377598933788, "Se il Periodo di Raggruppamento indicato è 'Decade', i giorni di raggruppamento possono valere da '1' a '10'.")))
              Return False
            End If
          Case "Q"
            If Not (NTSCInt(dtrTmp(i)!ar_ggragg) >= 1 And NTSCInt(dtrTmp(i)!ar_ggragg) <= 15) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377570235293, "Se il Periodo di Raggruppamento indicato è 'Quindicina', i giorni di raggruppamento possono valere da '1' a '15'.")))
              Return False
            End If
          Case "M"
            If Not (NTSCInt(dtrTmp(i)!ar_ggragg) >= 1 And NTSCInt(dtrTmp(i)!ar_ggragg) <= 31) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377544639338, "Se il Periodo di Raggruppamento indicato è 'Mese', i giorni di raggruppamento possono valere da '1' a '31'.")))
              Return False
            End If
          Case "B"
            If Not (NTSCInt(dtrTmp(i)!ar_ggragg) >= 1 And NTSCInt(dtrTmp(i)!ar_ggragg) <= 62) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377511442160, "Se il Periodo di Raggruppamento indicato è 'Bimestre', i giorni di raggruppamento possono valere da '1' a '62'.")))
              Return False
            End If
          Case "T"
            If Not (NTSCInt(dtrTmp(i)!ar_ggragg) >= 1 And NTSCInt(dtrTmp(i)!ar_ggragg) <= 93) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377490655142, "Se il Periodo di Raggruppamento indicato è 'Trimestre', i giorni di raggruppamento possono valere da '1' a '93'.")))
              Return False
            End If
          Case "R"
            If Not (NTSCInt(dtrTmp(i)!ar_ggragg) >= 1 And NTSCInt(dtrTmp(i)!ar_ggragg) <= 124) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377470954013, "Se il Periodo di Raggruppamento indicato è 'Quadrimestre', i giorni di raggruppamento possono valere da '1' a '124'.")))
              Return False
            End If
          Case "U"
            If Not (NTSCInt(dtrTmp(i)!ar_ggragg) >= 1 And NTSCInt(dtrTmp(i)!ar_ggragg) <= 186) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377445823439, "Se il Periodo di Raggruppamento indicato è 'Semestre', i giorni di raggruppamento possono valere da '1' a '186'.")))
              Return False
            End If
          Case "A"
            If Not (NTSCInt(dtrTmp(i)!ar_ggragg) >= 1 And NTSCInt(dtrTmp(i)!ar_ggragg) <= 366) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559377423019770, "Se il Periodo di Raggruppamento indicato è 'Anno', i giorni di raggruppamento possono valere da '1' a '366'.")))
              Return False
            End If
        End Select

        If NTSCStr(dtrTmp(i)!ar_codnomc) <> "" And bValidaNomenclCombin Then
          If Not oCldArtv.ValCodiceDb(NTSCStr(dtrTmp(i)!ar_codnomc), strDittaCorrente, "TARIC", "S", "", dttTmp) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559410292814136, "Codice nomenclatura combinata '|" & NTSCStr(dtrTmp(i)!ar_codnomc) & "|' non valida.")))
            Return False
          Else
            If NTSCStr(dttTmp.Rows(0)!ta_um2).Trim <> "" And NTSCStr(dtrTmp(i)!ar_umintra2) = "N" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129454259176240234, "Il codice nomenclatura combinata '|" & NTSCStr(dtrTmp(i)!ar_codnomc) & "|' richiede che il campo 'Unità di misura secondaria per INTRASTAT abbia un valore diverso da (Nessuna)")))
              Return False
            End If
          End If
        End If

        If dtrTmp(i).RowState = DataViewRowState.ModifiedCurrent Then
          If NTSCStr(dtrTmp(i)!ar_gescomm) <> NTSCStr(dtrTmp(i)("ar_gescomm", DataRowVersion.Original)) Then
            If Not oCldArtv.IsGescommUpdatable(strDittaCorrente, NTSCStr(dtrTmp(i)!ar_codart), True, NTSCStr(dtrTmp(i)("ar_gescomm", DataRowVersion.Original)), NTSCStr(dtrTmp(i)!ar_gescomm), strMsgOut) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812445372438018, strMsgOut)))
              Return False
            End If
          End If
        End If

        If CheckPolriordDettaglio() = False Then Return False
        '------------------------------------------------------------------------------------------------------------
        '--- Toglie gli spazi a destra e a sinistra, delle unità di misura (tranne quella principale), se:
        '----- si è in inserimento di un nuovo articolo
        '----- NON è un articolo descrittivo ('D' o 'M')
        '----- NON è attiva l'opzione di registro BSMGARTI\OPZIONI\GestTabunmis
        '------------------------------------------------------------------------------------------------------------
        If (dtrTmp(i).RowState = DataViewRowState.Added) And _
           (NTSCStr(dtrTmp(i)!ar_codart).ToUpper <> "D") And _
           (NTSCStr(dtrTmp(i)!ar_codart).ToUpper <> "M") And _
           (bGestTabUnmis = False) Then
          dtrTmp(i)!ar_unmis = NTSCStr(dtrTmp(i)!ar_unmis).Trim
          dtrTmp(i)!ar_unmis2 = NTSCStr(dtrTmp(i)!ar_unmis2).Trim
          dtrTmp(i)!ar_confez2 = NTSCStr(dtrTmp(i)!ar_confez2).Trim
          dtrTmp(i)!ar_um4 = NTSCStr(dtrTmp(i)!ar_um4).Trim
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Salva in ogni caso il nome dell'operatore, l'ora e la data della modifica
        '------------------------------------------------------------------------------------------------------------
        dtrTmp(i)!ar_ultagg = Now.ToShortDateString
        dtrTmp(i)!ar_oragg = (Now.Hour * 100) + Now.Minute
        dtrTmp(i)!ar_opnome = oApp.User.Nome
        '------------------------------------------------------------------------------------------------------------
        '--- Salva i campi ar_dtorgif1, ar_dtorgif2
        '------------------------------------------------------------------------------------------------------------
        If dtrTmp(i).RowState = DataViewRowState.Added Then
          If NTSCStr(dtrTmp(i)!ar_gif1).Trim <> "" Then bDtorgif1 = True
          If NTSCStr(dtrTmp(i)!ar_gif2).Trim <> "" Then bDtorgif2 = True
        Else
          If oCldArtv.ValCodiceDb(NTSCStr(dtrTmp(i)!ar_codart), strDittaCorrente, "ARTICO", "S", "", dttTmp) = True Then
            If NTSCStr(dtrTmp(i)!ar_gif1).Trim.ToLower <> NTSCStr(dttTmp.Rows(0)!ar_gif1).Trim.ToLower Then
              bDtorgif1 = True
            End If
            If NTSCStr(dtrTmp(i)!ar_gif2).Trim.ToLower <> NTSCStr(dttTmp.Rows(0)!ar_gif2).Trim.ToLower Then
              bDtorgif2 = True
            End If
          End If
        End If
        If bDtorgif1 = True Then dtrTmp(i)!ar_dtorgif1 = Now
        If bDtorgif2 = True Then dtrTmp(i)!ar_dtorgif2 = Now
        '------------------------------------------------------------------------------------------------------------
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

  Public Overridable Function TestPreCancella(ByVal strArtvCodroot As String, ByVal strTipoOpz As String, _
                                              ByVal nCodtipa As Integer) As Boolean
    Dim strOut As String = ""
    Try
      strArtvCodrootDelete = strArtvCodroot
      strTipoOpzDelete = strTipoOpz
      nCodtipaDelete = nCodtipa
      '-----------------------------------
      ' viene eseguito un controllo sulla possibile cancellazione del record
      ' se questo articolo non e' gia' referenziato in altre tabelle
      If Not oCldArtv.IsArtiDeletable(strDittaCorrente, strArtvCodrootDelete, strOut) Then
        ThrowRemoteEvent(New NTSEventArgs("", strOut))
        Return False
      End If

      'controllo che l'articolo non sia intestatario di D.B.
      If Not ControllaArticolo(strArtvCodrootDelete) Then Return False

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

  Public Overrides Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Dim strError As String = ""
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If

      If Not oCldArtv.Salva(strDittaCorrente, strArtvCodroot, nNumliv, strPrevar, strError, dsShared, dsArticoShared) Then
        If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
        Return False
      End If

      If bDelete = True Then
        For i As Integer = 0 To (dsArticoShared.Tables("ARTICO").Rows.Count - 1)
          oCldArtv.CancellaArtacce(strDittaCorrente, NTSCStr(dsArticoShared.Tables("ARTICO").Rows(i)!ar_codart))
        Next
      End If

      bHasChanges = False

      bNew = False

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

  Public Overridable Function ControllaArticolo(ByVal strArtvCodroot As String) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCldArtv.ControllaArticolo(strDittaCorrente, strArtvCodroot, dsTmp)
      If dsTmp.Tables("DISTBAS").Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556580334658921, "Articolo presente in Distinta Base." & vbCrLf & "Cancellazione non possibile.")))
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

  Public Overridable Function ControllaUnitaDiMisura(ByVal dtrTmp As DataRow) As Boolean
    Try
      '--- Confronta l'unità di misurta principale con le altre
      If NTSCStr(dtrTmp!ar_confez2) <> "" Then
        If UCase(NTSCStr(dtrTmp!ar_unmis)) = UCase(NTSCStr(dtrTmp!ar_confez2)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556804568462997, "Confezione uguale all'unità di misura principale." & vbCrLf & "Salvataggio non possibile.")))
          Return False
        End If
      End If
      If NTSCStr(dtrTmp!ar_unmis2) <> "" Then
        If UCase(NTSCStr(dtrTmp!ar_unmis)) = UCase(NTSCStr(dtrTmp!ar_unmis2)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556804829065423, "Unità di misura secondaria uguale all'unità di misura principale." & vbCrLf & "Salvataggio non possibile.")))
          Return False
        End If
      End If
      If NTSCStr(dtrTmp!ar_um4) <> "" Then
        If UCase(NTSCStr(dtrTmp!ar_unmis)) = UCase(NTSCStr(dtrTmp!ar_um4)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556805100468191, "Unità di misura formula uguale all'unità di misura principale." & vbCrLf & "Salvataggio non possibile.")))
          Return False
        End If
      End If
      '--- Confronta la confezione con le altre unità di misura
      If NTSCStr(dtrTmp!ar_confez2) <> "" Then
        If UCase(NTSCStr(dtrTmp!ar_confez2)) = UCase(NTSCStr(dtrTmp!ar_unmis)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556805318247025, "Confezione uguale all'unità di misura principale." & vbCrLf & "Salvataggio non possibile.")))
          Return False
        End If
        If NTSCStr(dtrTmp!ar_unmis2) <> "" Then
          If UCase(NTSCStr(dtrTmp!ar_confez2)) = UCase(NTSCStr(dtrTmp!ar_unmis2)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556805527114593, "Unità di misura secondaria uguale alla confezione." & vbCrLf & "Salvataggio non possibile.")))
            Return False
          End If
        End If
        If NTSCStr(dtrTmp!ar_um4) <> "" Then
          If UCase(NTSCStr(dtrTmp!ar_confez2)) = UCase(NTSCStr(dtrTmp!ar_um4)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556805770376521, "Unità di misura formula uguale all'unità di misura secondaria." & vbCrLf & "Salvataggio non possibile.")))
            Return False
          End If
        End If
      End If
      '--- Confronta l'unità di misura secondaria con le altre unità di misura
      If NTSCStr(dtrTmp!ar_unmis2) <> "" Then
        If UCase(NTSCStr(dtrTmp!ar_unmis)) = UCase(NTSCStr(dtrTmp!ar_unmis2)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556805985966623, "Unità di misura secondaria uguale all'unità di misura principale." & vbCrLf & "Salvataggio non possibile.")))
          Return False
        End If
        If NTSCStr(dtrTmp!ar_confez2) <> "" Then
          If UCase(NTSCStr(dtrTmp!ar_unmis2)) = UCase(NTSCStr(dtrTmp!ar_confez2)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556806177167997, "Confezione uguale all'unità di misura secondaria." & vbCrLf & "Salvataggio non possibile.")))
            Return False
          End If
        End If
        If NTSCStr(dtrTmp!ar_um4) <> "" Then
          If UCase(NTSCStr(dtrTmp!ar_unmis2)) = UCase(NTSCStr(dtrTmp!ar_um4)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556806396666549, "Unità di misura secondaria uguale all'unità di misura formula." & vbCrLf & "Salvataggio non possibile.")))
            Return False
          End If
        End If
      End If
      '--- Confronta l'unità di misura formula con le altre unità di misura
      If NTSCStr(dtrTmp!ar_um4) <> "" Then
        If UCase(NTSCStr(dtrTmp!ar_um4)) = UCase(NTSCStr(dtrTmp!ar_unmis)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556806646181997, "Unità di misura formula uguale all'unità di misura principale." & vbCrLf & "Salvataggio non possibile.")))
          Return False
        End If
        If NTSCStr(dtrTmp!ar_confez2) <> "" Then
          If UCase(NTSCStr(dtrTmp!ar_um4)) = UCase(NTSCStr(dtrTmp!ar_confez2)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556806837227033, "Unità di misura formula uguale all'unità di misura secondaria." & vbCrLf & "Salvataggio non possibile.")))
            Return False
          End If
        End If
        If NTSCStr(dtrTmp!ar_unmis2) <> "" Then
          If UCase(NTSCStr(dtrTmp!ar_um4)) = UCase(NTSCStr(dtrTmp!ar_unmis2)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556807045312911, "Unità di misura formula uguale all'unità di misura secondaria." & vbCrLf & "Salvataggio non possibile.")))
            Return False
          End If
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
      Return False
    End Try
  End Function

  Public Overridable Function TrovaGif(ByVal strNomeFile As String) As Boolean
    Try
      If Not File.Exists(strImageDir & strNomeFile) Then
        ' il file non è stato trovato
        Return False
      Else
        ' il file è stato trovato
        Return True
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

  Public Overridable Function SetImagesDir() As Boolean
    Try
      'prelevo la dir delle imaggini sul server
      Dim i As Integer = oApp.RptDir.Length - 1
      Do While oApp.RptDir.Substring(i, 1) <> "\"
        i -= 1
      Loop
      strImageDir = oApp.RptDir.Substring(0, i) & "\Images\"

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

  Public Overridable Function CheckCodroot(ByVal strCodroot As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not oCldArtv.ValCodiceDb(strCodroot, strDittaCorrente, "ARTROOT", "S", , dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128587880069321355, "Codice root inesistente.")))
        Return False
      Else
        nNumliv = NTSCInt(dttTmp.Rows(0)!arr_numliv)
        nLungvar1 = NTSCInt(dttTmp.Rows(0)!arr_lungvar1)
        nLungvar2 = NTSCInt(dttTmp.Rows(0)!arr_lungvar2)
        nLungvar3 = NTSCInt(dttTmp.Rows(0)!arr_lungvar3)
        strPrevar = NTSCStr(dttTmp.Rows(0)!arr_prevar1)
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

  Public Overridable Function GetArtpro(ByVal strCodart As String, ByRef ds As DataSet) As Boolean
    Dim dttTmp As New DataTable
    Try
      Return oCldArtv.GetArtpro(strDittaCorrente, strCodart, ds)
      
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

  Public Overridable Function GetArtprox(ByVal strCodart As String, ByRef ds As DataSet) As Boolean
    Dim dttTmp As New DataTable
    Try
      Return oCldArtv.GetArtprox(strDittaCorrente, strCodart, ds)

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

  Public Overridable Function Var1Aggiungi(ByRef dtrVar1Tmp As DataRow) As Boolean
    Dim bResult As Boolean = False
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldArtv.Var1Aggiungi(strDittaCorrente, strPrefixEAN13, bIndicod, strArtvCodroot, _
        bCreaBarcodeE13, dsShared.Tables("ARTICO").Rows(0), dtrVar1Tmp, dsShared, strMsg)
      '--------------------------------------------------------------------------------------------------------------
      If bResult = True Then VisualizzaFileDiLOG(strMsg)
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var2Aggiungi(ByRef dtrVarTmp As DataRow, ByVal nVar As Integer) As Boolean
    Dim bResult As Boolean = False
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldArtv.Var2Aggiungi(strDittaCorrente, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
        dsShared.Tables("ARTICO").Rows(0), dtrVarTmp, nVar, dsShared, strMsg)
      '--------------------------------------------------------------------------------------------------------------
      If bResult = True Then VisualizzaFileDiLOG(strMsg)
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var3Aggiungi(ByRef dtrVarTmp As DataRow, ByVal nVar As Integer) As Boolean
    Dim bResult As Boolean = False
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldArtv.Var3Aggiungi(strDittaCorrente, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
        dsShared.Tables("ARTICO").Rows(0), dtrVarTmp, nVar, dsShared, strMsg)
      '--------------------------------------------------------------------------------------------------------------
      If bResult = True Then VisualizzaFileDiLOG(strMsg)
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function Var1Esplodi(ByRef dtrVar1Tmp As DataRow) As Boolean
    Dim bResult As Boolean = False
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldArtv.Var1Esplodi(strDittaCorrente, strPrefixEAN13, bIndicod, strArtvCodroot, _
        bCreaBarcodeE13, dsShared.Tables("ARTICO").Rows(0), dtrVar1Tmp, dsShared, strMsg)
      '--------------------------------------------------------------------------------------------------------------
      If bResult = True Then VisualizzaFileDiLOG(strMsg)
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var2Esplodi(ByRef dtrVarTmp As DataRow) As Boolean
    Dim bResult As Boolean = False
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldArtv.Var2Esplodi(strDittaCorrente, strPrefixEAN13, bIndicod, strArtvCodroot, _
        bCreaBarcodeE13, dsShared.Tables("ARTICO").Rows(0), dtrVarTmp, dsShared, strMsg)
      '--------------------------------------------------------------------------------------------------------------
      If bResult = True Then VisualizzaFileDiLOG(strMsg)
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var3Esplodi(ByRef dtrVarTmp As DataRow) As Boolean
    Dim bResult As Boolean = False
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldArtv.Var3Esplodi(strDittaCorrente, strPrefixEAN13, bIndicod, strArtvCodroot, _
        bCreaBarcodeE13, dsShared.Tables("ARTICO").Rows(0), dtrVarTmp, dsShared, strMsg)
      '--------------------------------------------------------------------------------------------------------------
      If bResult = True Then VisualizzaFileDiLOG(strMsg)
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function Var1EspSel(ByRef dtrVar1Tmp As DataRow) As Boolean
    Dim bResult As Boolean = False
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldArtv.Var1EspSel(strDittaCorrente, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
        dsShared.Tables("ARTICO").Rows(0), dtrVar1Tmp, dsVar1Shared.Tables("ARTVAR"), dsShared, strMsg)
      '--------------------------------------------------------------------------------------------------------------
      If bResult = True Then VisualizzaFileDiLOG(strMsg)
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var2EspSel(ByRef dtrVarTmp As DataRow) As Boolean
    Dim bResult As Boolean = False
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldArtv.Var2EspSel(strDittaCorrente, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
        dsShared.Tables("ARTICO").Rows(0), dtrVarTmp, dsVar1Shared.Tables("ARTVAR"), _
        dsVar2Shared.Tables("ARTVAR"), dsShared, strMsg)
      '--------------------------------------------------------------------------------------------------------------
      If bResult = True Then VisualizzaFileDiLOG(strMsg)
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var3EspSel(ByRef dtrVarTmp As DataRow) As Boolean
    Dim bResult As Boolean = False
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldArtv.Var3EspSel(strDittaCorrente, strPrefixEAN13, bIndicod, strArtvCodroot, bCreaBarcodeE13, _
        dsShared.Tables("ARTICO").Rows(0), dtrVarTmp, dsVar1Shared.Tables("ARTVAR"), dsVar2Shared.Tables("ARTVAR"), _
        dsVar3Shared.Tables("ARTVAR"), dsShared, strMsg)
      '--------------------------------------------------------------------------------------------------------------
      If bResult = True Then VisualizzaFileDiLOG(strMsg)
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return False
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ArticoRipristinaCancella() As Boolean
    Dim strError As String = ""
    Try
      Return oCldArtv.ArticoRipristinaCancella(strDittaCorrente, strArtvCodroot, _
                                               dsVar1Shared, dsVar2Shared, dsVar3Shared, dsArticoShared, dsShared, strError)

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

  Public Overridable Function DecodCodvari1(ByVal strCodvar As String, ByRef strDescr As String) As Boolean
    Dim strOut As String = ""
    Try
      If NTSCStr(strCodvar) = "" Then
        strDescr = ""
        Exit Function
      End If
      If oCldArtv.ValCodiceDb(strCodvar, strDittaCorrente, "TABVARI", "S", strOut) Then
        strDescr = strOut
      End If

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

  Public Overridable Function DecodCodcove(ByVal strCodcove As String, ByRef strDescr As String) As Boolean
    Dim strOut As String = ""
    Try
      If NTSCInt(strCodcove) = 0 Then
        strDescr = ""
        Exit Function
      End If
      If oCldArtv.ValCodiceDb(strCodcove, strDittaCorrente, "TABCOVE", "N", strOut) Then
        strDescr = strOut
      End If

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
  Public Overridable Function DecodCodciva(ByVal strCodciva As String, ByRef strDescr As String) As Boolean
    Dim strOut As String = ""
    Try
      If NTSCInt(strCodciva) = 0 Then
        strDescr = ""
        Exit Function
      End If
      If oCldArtv.ValCodiceDb(strCodciva, strDittaCorrente, "TABCIVA", "N", strOut) Then
        strDescr = strOut
      End If

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

  Public Overridable Function GetCodroot(ByVal strCodart As String, ByRef strCodroot As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not oCldArtv.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", , dttTmp) Then
        Return False
      Else
        If NTSCStr(dttTmp.Rows(0)!ar_codroot) <> "" Then strCodroot = NTSCStr(dttTmp.Rows(0)!ar_codroot)
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

  Public Overridable Function CaricaColonneUnbound(ByRef dtrIn As DataRow) As Boolean
    Dim dtrS As DataRowState = Nothing
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim dsTmp As DataSet = Nothing
    Try
      dtrS = dtrIn.RowState

      '----------------------
      'tipoopz
      oCldArtv.GetArticoTipoopz(strDittaCorrente, NTSCStr(dtrIn!ar_codart), dsTmp)
      If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
        dtrIn!ar_tipoopz = NTSCStr(dsTmp.Tables("ARTICO").Rows(0)!ar_tipoopz)
      Else
        dtrIn!ar_tipoopz = " "
      End If

      '----------------------
      'rimetto a posto il datarowstate della riga
      Select Case dtrS
        Case DataRowState.Added : If dtrIn.RowState <> DataRowState.Added Then dtrIn.SetAdded()
        Case DataRowState.Modified : If dtrIn.RowState <> DataRowState.Modified Then dtrIn.SetModified()
        Case DataRowState.Unchanged
          dtrIn.AcceptChanges()
          bHasChanges = False
      End Select

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function AggiornaListini(ByVal strCodart As String, ByVal nCodvalu As Integer, _
    ByVal strDatavalidita As String, ByVal nListino As Integer, ByVal bQuant As Boolean, ByVal dQuant As Decimal, _
    ByVal strDtinvigore As String, ByVal nCodpdon As Integer, ByVal nCodlavo As Integer, ByVal nFase As Integer, _
    Optional ByVal strUnmis As String = "", Optional ByVal strNetto As String = "", _
    Optional ByVal bSilent As Boolean = True) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return CType(oCleComm, CLELBMENU).AggiornaListini(strDittaCorrente, strCodart, nCodvalu, strDatavalidita, _
        nListino, bQuant, dQuant, strDtinvigore, nCodpdon, nCodlavo, nFase, strUnmis, strNetto)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function

  Public Overridable Function CheckNumaArtico() As Boolean
    Try
      If oCldArtv.LegNuma(strDittaCorrente, "BC", " ", 0, True) > 99999 And bCreaBarcodeE13 Then
        ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 129561640221391206, "Attenzione! Il progressivo dei barcode ha superato 99999. Non verranno generati nuovi barcode in automatico.")))
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

  Public Overrides Function LogWrite(ByVal strMsg As String, ByVal bIncrementaCount As Boolean) As Boolean
    Dim lw1 As StreamWriter = Nothing
    Dim strVerFile As String = ""
    Dim strNomeFile As String = "VARIANTI.txt"

    Try
      '--------------------------------------------------------------------------------------------------------------
      If strMsg = "START" Then
        strVerFile = Me.GetType.AssemblyQualifiedName.ToString
        strVerFile = strVerFile.Substring(strVerFile.IndexOf("Version=") + 8)
        strVerFile = strVerFile.Substring(0, strVerFile.IndexOf(","))
        lw1 = New StreamWriter(strNomeFile, False)
        lw1.WriteLine("--------------------------------------------------------------------")
        lw1.WriteLine(oApp.Tr(Me, 130427309300197331, "Elenco varianti per le quali non è stato possibile generare i barcode relativi"))
        Select Case bIndicod
          Case True
            lw1.WriteLine(oApp.Tr(Me, 130427309427698147, "perché il progressivo per la generazione del Barcode, indicato in tabella 'Numerazioni'" & vbCrLf & _
              "insieme al valore indicato in 'BSMGARTI/OPZIONI/PrefixEAN13', nel Registro di Business," & vbCrLf & _
              "supera i 12 caratteri."))
          Case False
            lw1.WriteLine(oApp.Tr(Me, 130427309697699875, "perché il progressivo per la generazione del Barcode, indicato in tabella 'Numerazioni'," & vbCrLf & _
              "supera i 99999."))
        End Select
        lw1.WriteLine("--------------------------------------------------------------------")
        lw1.WriteLine("")
        lCountLog = 0
      ElseIf strMsg = "STOP" Then
        lw1 = New StreamWriter(strNomeFile, True)
        lw1.WriteLine("")
        lw1.WriteLine("--------------------------------------------------------------------")
      Else
        lw1 = New StreamWriter(strNomeFile, True)
        If bIncrementaCount = True Then lCountLog += 1
        lw1.WriteLine(strMsg)
      End If
      '--------------------------------------------------------------------------------------------------------------
      lw1.Flush()
      lw1.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function
  Public Overridable Sub VisualizzaFileDiLOG(ByVal strMsg As String)
    Dim evt As NTSEventArgs = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If strMsg.Trim <> "" Then
        If LogWrite("START", False) = True Then LogWrite(strMsg, True)
        LogWrite("STOP", False)
        If lCountLog > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("ASKVISLOG:", ""))
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function DuplicaArticolo(ByVal strCodartNew As String, _
                                              ByVal bListini As Boolean, ByVal bSconti As Boolean, _
                                              ByVal bProvv As Boolean, ByVal bArtfasi As Boolean) As Boolean

    Dim i As Integer = 0
    Dim strCodartOld As String = ""
    Dim evnt As NTSEventArgs
    Dim strMsg As String
    Dim nCurRow As Integer
    Dim strVariante As String = ""
    Try
      strCodartOld = NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_codart)

      dsShared.Tables("ARTICO").AcceptChanges()

      dsShared.Tables("ARTICO").Rows(0).SetAdded()
      nCurRow = 0

      For i = 0 To dsArticoShared.Tables("ARTICO").Rows.Count - 1
        dsArticoShared.Tables("ARTICO").Rows(i).SetAdded()
      Next

      For i = 0 To dsVar1Shared.Tables("ARTVAR").Rows.Count - 1
        dsVar1Shared.Tables("ARTVAR").Rows(i).SetAdded()
      Next
      If nNumliv >= 2 Then
        For i = 0 To dsVar2Shared.Tables("ARTVAR").Rows.Count - 1
          dsVar2Shared.Tables("ARTVAR").Rows(i).SetAdded()
        Next
      End If
      If nNumliv = 3 Then
        For i = 0 To dsVar3Shared.Tables("ARTVAR").Rows.Count - 1
          dsVar3Shared.Tables("ARTVAR").Rows(i).SetAdded()
        Next
      End If

      '---------------------
      'correggo artico
      With dsShared.Tables("ARTICO").Rows(nCurRow)
        !ar_codart = strCodartNew

        !ar_ultagg = Now.ToShortDateString
        !ar_oragg = (Now.Hour * 100) + Now.Minute
        !ar_datins = Now.ToShortDateString

        If (Not (NTSCStr(!ar_coddb) = "")) Then
          strMsg = oApp.Tr(Me, 130427303811412203, "L'articolo '|" & strCodartOld & "|' che si sta duplicando" & vbCrLf & _
            "possiede un Codice Distinta Base." & vbCrLf & _
            "Ereditarlo insieme agli altri dati?")
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, strMsg)
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
            !ar_coddb = "" 'Null
          End If
        End If
        If NTSCDec(!ar_qtacon2) = 0 Then !ar_qtacon2 = 1
      End With

      '---------------------
      'correggo artico root + varianti
      For i = 0 To dsArticoShared.Tables("ARTICO").Rows.Count - 1
        With dsArticoShared.Tables("ARTICO").Rows(i)
          strVariante = Mid(NTSCStr(!ar_codart), Len(NTSCStr(!ar_codart)))
          If strVariante = "" Then Return False
          !ar_codart = strCodartNew & strVariante
          !ar_codroot = strCodartNew
        End With
      Next

      evnt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128473835841698662, "Salvare l'articolo appena creato?"))
      ThrowRemoteEvent(evnt)
      If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False

      strArtvCodroot = strCodartNew 'modifico il nuovo root

      If Not Var1Salva(False) Then Return False
      dsVar1Shared.AcceptChanges()

      If nNumliv >= 2 Then
        If Not Var2Salva(False) Then Return False
        dsVar2Shared.AcceptChanges()
      End If
      If nNumliv = 3 Then
        If Not Var3Salva(False) Then Return False
        dsVar3Shared.AcceptChanges()
      End If
      If Not ArticoSalva(False) Then Return False
      dsArticoShared.AcceptChanges()

      If Not Salva(False) Then Return False
      dsShared.AcceptChanges()

      'duplico le tabelle collegate
      oCldArtv.AggiornaTabelleVarianti(strDittaCorrente, strCodartNew, strCodartOld, bListini, _
                                       bSconti, bProvv, bArtfasi, nArtvLungrootOld)

      If bCreaBarcodeE13 Then
        strMsg = ""
        For i = 0 To dsArticoShared.Tables("ARTICO").Rows.Count - 1
          With dsArticoShared.Tables("ARTICO").Rows(i)
            If Not oCldArtv.GeneraBarcode(strDittaCorrente, NTSCStr(!ar_codart).Trim, _
                  NTSCStr(!ar_unmis), strPrefixEAN13, bIndicod, Nothing) Then
              strMsg += " --> " & CStrSQL(NTSCStr(!ar_codart).Trim) & vbCrLf
            End If
          End With
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
    End Try
  End Function

  Public Overridable Function GetArtclasDescr(ByVal strClas1 As String, _
                                          ByVal strClas2 As String, ByVal strClas3 As String, _
                                          ByVal strClas4 As String, ByVal strClas5 As String) As String
    Try
      Return oCldArtv.GetArtclasDescr(strDittaCorrente, strClas1, strClas2, strClas3, strClas4, strClas5)
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

  Public Overridable Function CheckPolriordDettaglio() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bNoMsgCongruenzaPolSconte = True Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_polriord)
        Case "M", "N"
          If dsArticoShared.Tables("ARTICO").Select("ar_scomin = 0").Length > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130513713306559305, "Attenzione!" & vbCrLf & _
              "La 'Scorta minima', nelle righe dell'ANALITICO VARIANTI, deve essere maggiore di 0 quando la Politica di Riordino Ã¨ di tipo:" & vbCrLf & _
              " --> A punto di riordino con lotto" & vbCrLf & _
              " --> A punto di riordino a ricostruzione scorta")))
            Return False
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_polriord)
        Case "N"
          If dsArticoShared.Tables("ARTICO").Select("ar_scomax = 0").Length > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130513715575035083, "Attenzione!" & vbCrLf & _
              "La 'Scorta massima', nelle righe dell'ANALITICO VARIANTI, deve essere maggiore di 0 quando la Politica di Riordino Ã¨ di tipo:" & vbCrLf & _
              " --> A punto di riordino con lotto" & vbCrLf & _
              " --> A punto di riordino a ricostruzione scorta")))
            Return False
          End If
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_polriord)
        Case "F", "M", "O"
          If dsArticoShared.Tables("ARTICO").Select("ar_minord = 0").Length > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130513716903640787, "Attenzione!" & vbCrLf & _
              "La 'QuantitÃ  lotto standard', nelle righe dell'ANALITICO VARIANTI, deve essere maggiore di 0 quando la Politica di Riordino Ã¨ di tipo:" & vbCrLf & _
              " --> A punto di riordino con lotto" & vbCrLf & _
            " --> Su fabbisogno con lotto" & vbCrLf & _
            " --> Su fabbisogno con lotto minimo")))
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

  Public Overridable Function ArticoliDeteriorabili() As Boolean
    Try
      Return oCldArtv.ArticoliDeteriorabili(strDittaCorrente)
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

#Region "funzioni specifiche per griglie Var"
  Public Overridable Function VarNuovo(ByRef ds1 As DataSet, ByRef ds2 As DataSet, ByRef ds3 As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim strCodvar As String = ""
    Try
      '----------------------------------------
      If strNarvCodvari1 <> "" Then
        strCodvar = strNarvCodvari1
      Else
        strCodvar = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
      End If

      dReturn = oCldArtv.VarGetDataNuovo(strDittaCorrente, strCodvar, "1", ds1)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldArtv.SetTableDefaultValueFromDB("ARTVAR", ds1)
      VarSetDefaultValue(ds1, "1")
      dsVar1Shared = ds1

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsVar1Shared.Tables("ARTVAR").ColumnChanging, AddressOf Var1BeforeColUpdate
      AddHandler dsVar1Shared.Tables("ARTVAR").ColumnChanged, AddressOf Var1AfterColUpdate

      If nNumliv >= 2 Then
        '----------------------------------------
        If strNarvCodvari2 <> "" Then
          strCodvar = strNarvCodvari2
        Else
          strCodvar = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
        End If

        dReturn = oCldArtv.VarGetDataNuovo(strDittaCorrente, strCodvar, "2", ds2)
        If dReturn = False Then Return False

        oCldArtv.SetTableDefaultValueFromDB("ARTVAR", ds2)
        VarSetDefaultValue(ds2, "2")
        dsVar2Shared = ds2

        '--------------------------------------
        'creo gli eventi per la gestione del datatable dentro l'entity
        AddHandler dsVar2Shared.Tables("ARTVAR").ColumnChanging, AddressOf Var2BeforeColUpdate
        AddHandler dsVar2Shared.Tables("ARTVAR").ColumnChanged, AddressOf Var2AfterColUpdate

      End If

      If nNumliv = 3 Then
        '----------------------------------------
        If strNarvCodvari3 <> "" Then
          strCodvar = strNarvCodvari3
        Else
          strCodvar = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
        End If

        dReturn = oCldArtv.VarGetDataNuovo(strDittaCorrente, strCodvar, "3", ds3)
        If dReturn = False Then Return False

        oCldArtv.SetTableDefaultValueFromDB("ARTVAR", ds3)
        VarSetDefaultValue(ds3, "3")
        dsVar3Shared = ds3

        '--------------------------------------
        'creo gli eventi per la gestione del datatable dentro l'entity
        AddHandler dsVar3Shared.Tables("ARTVAR").ColumnChanging, AddressOf Var3BeforeColUpdate
        AddHandler dsVar3Shared.Tables("ARTVAR").ColumnChanged, AddressOf Var3AfterColUpdate

      End If

      bVar1HasChanges = True
      bVar2HasChanges = True
      bVar3HasChanges = True

      bVarNew = True

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
  Public Overridable Function VarApri(ByVal strDitta As String, ByRef dsVar1 As DataSet, _
                                   ByRef dsVar2 As DataSet, ByRef dsVar3 As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim dttTmp As New DataTable
    Try
      'GRIGLIA VAR1
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldArtv.VarGetDataApri(strDittaCorrente, strArtvCodroot, "1", dsVar1)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldArtv.SetTableDefaultValueFromDB("ARTVAR", dsVar1)

      VarSetDefaultValue(dsVar1, "1")

      dsVar1Shared = dsVar1

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsVar1Shared.Tables("ARTVAR").ColumnChanging, AddressOf Var1BeforeColUpdate
      AddHandler dsVar1Shared.Tables("ARTVAR").ColumnChanged, AddressOf Var1AfterColUpdate

      'GRIGLIA VAR2
      If nNumliv >= 2 Then
        '--------------------------------------
        'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
        strDittaCorrente = strDitta
        dReturn = oCldArtv.VarGetDataApri(strDittaCorrente, strArtvCodroot, "2", dsVar2)
        If dReturn = False Then Return False

        '--------------------------------------------------------------
        'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
        oCldArtv.SetTableDefaultValueFromDB("ARTVAR", dsVar2)

        VarSetDefaultValue(dsVar2, "2")

        dsVar2Shared = dsVar2

        '--------------------------------------
        'creo gli eventi per la gestione del datatable dentro l'entity
        AddHandler dsVar2Shared.Tables("ARTVAR").ColumnChanging, AddressOf Var2BeforeColUpdate
        AddHandler dsVar2Shared.Tables("ARTVAR").ColumnChanged, AddressOf Var2AfterColUpdate
      End If

      'GRIGLIA VAR3
      If nNumliv = 3 Then
        '--------------------------------------
        'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
        strDittaCorrente = strDitta
        dReturn = oCldArtv.VarGetDataApri(strDittaCorrente, strArtvCodroot, "3", dsVar3)
        If dReturn = False Then Return False

        '--------------------------------------------------------------
        'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
        oCldArtv.SetTableDefaultValueFromDB("ARTVAR", dsVar3)

        VarSetDefaultValue(dsVar3, "3")

        dsVar3Shared = dsVar3

        '--------------------------------------
        'creo gli eventi per la gestione del datatable dentro l'entity
        AddHandler dsVar3Shared.Tables("ARTVAR").ColumnChanging, AddressOf Var3BeforeColUpdate
        AddHandler dsVar3Shared.Tables("ARTVAR").ColumnChanged, AddressOf Var3AfterColUpdate
      End If

      bVar1HasChanges = False
      bVar2HasChanges = False
      bVar3HasChanges = False

      bVarNew = False

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
  Public Overridable Sub VarSetDefaultValue(ByRef ds As DataSet, ByVal strLivello As String)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      If strLivello = "1" Then
        ds.Tables("ARTVAR").Columns("xx_codditt1").DefaultValue = strDittaCorrente
        ds.Tables("ARTVAR").Columns("xx_seleziona1").DefaultValue = "N"
      ElseIf strLivello = "2" Then
        ds.Tables("ARTVAR").Columns("xx_codditt2").DefaultValue = strDittaCorrente
        ds.Tables("ARTVAR").Columns("xx_seleziona2").DefaultValue = "N"
      ElseIf strLivello = "3" Then
        ds.Tables("ARTVAR").Columns("xx_codditt3").DefaultValue = strDittaCorrente
        ds.Tables("ARTVAR").Columns("xx_seleziona3").DefaultValue = "N"
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

  Public Overridable Function VarNuovoSalva() As Boolean
    Try
      'se richiamata da nuovo passandogli il codice variante salva subito i record
      If strNarvCodvari1 <> "" Then
        oCldArtv.VarNuovoSalva(strDittaCorrente, strArtvCodroot, "1", nNumliv, strPrevar, _
                                nArtvLungroot, nLungvar1, nLungvar2, nLungvar3, NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_descr), _
                                dsVar1Shared)
        bVar1HasChanges = False
        bVarNew = False
      End If
      If nNumliv >= 2 Then
        If strNarvCodvari2 <> "" Then
          oCldArtv.VarNuovoSalva(strDittaCorrente, strArtvCodroot, "2", nNumliv, strPrevar, _
                                nArtvLungroot, nLungvar1, nLungvar2, nLungvar3, NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_descr), _
                                dsVar2Shared)
          bVar2HasChanges = False
          bVarNew = False
        End If
      End If
      If nNumliv = 3 Then
        If strNarvCodvari3 <> "" Then
          oCldArtv.VarNuovoSalva(strDittaCorrente, strArtvCodroot, "3", nNumliv, strPrevar, _
                                nArtvLungroot, nLungvar1, nLungvar2, nLungvar3, NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_descr), _
                                dsVar3Shared)
          bVar3HasChanges = False
          bVarNew = False
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

  Public Overridable Function Var1Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsVar1Shared.Tables("ARTVAR").Select(strFilter)(nRow).RejectChanges()
      bVar1HasChanges = False
      bVarNew = False
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Function Var2Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsVar2Shared.Tables("ARTVAR").Select(strFilter)(nRow).RejectChanges()
      bVar2HasChanges = False
      bVarNew = False
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Function Var3Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsVar3Shared.Tables("ARTVAR").Select(strFilter)(nRow).RejectChanges()
      bVar3HasChanges = False
      bVarNew = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function Var1TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim dtrCurrRow() As DataRow
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate
      dtrCurrRow = dsVar1Shared.Tables("ARTVAR").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_descr) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128593083423988751, "Inserire una descrizione articolo valida")))
          Return False
        End If

        If Trim(dtrCurrRow(i)!xx_codvar1.ToString) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222087187500, "Inserire un codice variante valido")))
          Return False
        End If
      Next

      dtrTmp = dsVar1Shared.Tables("ARTVAR").Select("xx_codditt1 = " & CStrSQL(dtrCurrRow(0)!xx_codditt1.ToString) & _
        " AND xx_codvar1 = " & CStrSQL(dtrCurrRow(0)!xx_codvar1.ToString))
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128588578680192307, "Esiste gia una riga con le stesse caratteristiche")))
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
  Public Overridable Function Var2TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim dtrCurrRow() As DataRow
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate
      dtrCurrRow = dsVar2Shared.Tables("ARTVAR").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_descr) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128593083299740315, "Inserire una descrizione articolo valida")))
          Return False
        End If

        If Trim(dtrCurrRow(i)!xx_codvar2.ToString) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128588569659061682, "Inserire un codice variante valido")))
          Return False
        End If
      Next

      dtrTmp = dsVar2Shared.Tables("ARTVAR").Select("xx_codditt2 = " & CStrSQL(dtrCurrRow(0)!xx_codditt2.ToString) & _
        " AND xx_codvar2 = " & CStrSQL(dtrCurrRow(0)!xx_codvar2.ToString))
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128588589297760259, "Esiste gia una riga con le stesse caratteristiche")))
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
  Public Overridable Function Var3TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim dtrCurrRow() As DataRow
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate
      dtrCurrRow = dsVar3Shared.Tables("ARTVAR").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_descr) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128593083533564633, "Inserire una descrizione articolo valida")))
          Return False
        End If

        If Trim(dtrCurrRow(i)!xx_codvar3.ToString) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128588569692499182, "Inserire un codice variante valido")))
          Return False
        End If
      Next

      dtrTmp = dsVar3Shared.Tables("ARTVAR").Select("xx_codditt3 = " & CStrSQL(dtrCurrRow(0)!xx_codditt3.ToString) & _
        " AND xx_codvar3 = " & CStrSQL(dtrCurrRow(0)!xx_codvar3.ToString))
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128588590322610568, "Esiste gia una riga con le stesse caratteristiche")))
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

  Public Overridable Function Var1TestPreCancella(ByVal dtrTmp As DataRow) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      '--------------------------------------------------------------------------------------
      '--- Se una variante collegata al primo livello è presente in ARTDEFX avvisa ed esce
      '--------------------------------------------------------------------------------------
      If Not oCldArtv.VarGetartdefx(strDittaCorrente, strArtvCodroot, "1", dtrTmp, dsTmp) Then Return False

      If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734632899221440, "Nell'analitico delle varianti esistono articoli, collegati a questo livello di variante, nei 'Progressivi definitivi articoli'.")))
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
  Public Overridable Function Var2TestPreCancella(ByVal dtrTmp As DataRow) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      '--------------------------------------------------------------------------------------
      '--- Se una variante collegata al secondo livello è presente in ARTDEFX avvisa ed esce
      '--------------------------------------------------------------------------------------
      If Not oCldArtv.VarGetartdefx(strDittaCorrente, strArtvCodroot, "2", dtrTmp, dsTmp) Then Return False

      If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734632977347940, "Nell'analitico delle varianti esistono articoli, collegati a questo livello di variante, nei 'Progressivi definitivi articoli'.")))
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
  Public Overridable Function Var3TestPreCancella(ByVal dtrTmp As DataRow) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      '--------------------------------------------------------------------------------------
      '--- Se una variante collegata al primo terzo è presente in ARTDEFX avvisa ed esce
      '--------------------------------------------------------------------------------------
      If Not oCldArtv.VarGetartdefx(strDittaCorrente, strArtvCodroot, "3", dtrTmp, dsTmp) Then Return False

      If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734633005629733, "Nell'analitico delle varianti esistono articoli, collegati a questo livello di variante, nei 'Progressivi definitivi articoli'.")))
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

  Public Overridable Function Var1Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not Var1TestPreSalva() Then Return False
      End If

      If Not oCldArtv.VarSalva(strDittaCorrente, strArtvCodroot, "1", nNumliv, strPrevar, _
        nArtvLungroot, nLungvar1, nLungvar2, nLungvar3, NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_descr), _
        dsVar1Shared) Then Return False

      bVar1HasChanges = False
      bVarNew = False

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
  Public Overridable Function Var2Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not Var2TestPreSalva() Then Return False
      End If

      If Not oCldArtv.VarSalva(strDittaCorrente, strArtvCodroot, "2", nNumliv, strPrevar, _
        nArtvLungroot, nLungvar1, nLungvar2, nLungvar3, NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_descr), _
        dsVar2Shared) Then Return False

      bVar2HasChanges = False
      bVarNew = False

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
  Public Overridable Function Var3Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not Var3TestPreSalva() Then Return False
      End If

      If Not oCldArtv.VarSalva(strDittaCorrente, strArtvCodroot, "3", nNumliv, strPrevar, _
        nArtvLungroot, nLungvar1, nLungvar2, nLungvar3, NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_descr), _
        dsVar3Shared) Then Return False

      bVar3HasChanges = False
      bVarNew = False

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

  Public Overridable Sub Var1BeforeColUpdate_xx_codvar1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Dim dsTmp As DataSet = Nothing
    Try
      If Not (InStr(1, e.ProposedValue.ToString, "'") = 0) Then
        e.ProposedValue = " "
        Exit Sub
      End If

      e.ProposedValue = UCase(e.ProposedValue.ToString)

      If Not strNarvCodvari1 = "" Then
        oCldArtv.GetValvari(strDittaCorrente, strNarvCodvari1, e.ProposedValue.ToString, dsTmp)
        If dsTmp.Tables("VALVARI").Rows.Count > 0 Then
          e.Row!xx_descr1 = dsTmp.Tables("VALVARI").Rows(0)!vv_desvvar
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
  Public Overridable Sub Var2BeforeColUpdate_xx_codvar2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Dim dsTmp As DataSet = Nothing
    Try
      If Not InStr(1, e.ProposedValue.ToString, "'") = 0 Then
        e.ProposedValue = " "
        Exit Sub
      End If

      e.ProposedValue = UCase(e.ProposedValue.ToString)

      If Not strNarvCodvari2 = "" Then
        oCldArtv.GetValvari(strDittaCorrente, strNarvCodvari2, e.ProposedValue.ToString, dsTmp)
        If dsTmp.Tables("VALVARI").Rows.Count > 0 Then
          e.Row!xx_descr2 = dsTmp.Tables("VALVARI").Rows(0)!vv_desvvar
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
  Public Overridable Sub Var3BeforeColUpdate_xx_codvar3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Dim dsTmp As DataSet = Nothing
    Try
      If Not InStr(1, e.ProposedValue.ToString, "'") = 0 Then
        e.ProposedValue = " "
        Exit Sub
      End If

      e.ProposedValue = UCase(e.ProposedValue.ToString)

      If Not strNarvCodvari3 = "" Then
        oCldArtv.GetValvari(strDittaCorrente, strNarvCodvari3, e.ProposedValue.ToString, dsTmp)
        If dsTmp.Tables("VALVARI").Rows.Count > 0 Then
          e.Row!xx_descr3 = dsTmp.Tables("VALVARI").Rows(0)!vv_desvvar
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

  Public Overridable Sub Var1BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
      Dim strFunction As String = "Var1BeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub Var1AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bVar1HasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "Var1AfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub Var2BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
      Dim strFunction As String = "Var2BeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub Var2AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bVar2HasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "Var2AfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub Var3BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
      Dim strFunction As String = "Var3BeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub Var3AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bVar3HasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "Var3AfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public ReadOnly Property Var1RecordIsChanged() As Boolean
    Get
      Return bVar1HasChanges
    End Get
  End Property
  Public ReadOnly Property Var2RecordIsChanged() As Boolean
    Get
      Return bVar2HasChanges
    End Get
  End Property
  Public ReadOnly Property Var3RecordIsChanged() As Boolean
    Get
      Return bVar3HasChanges
    End Get
  End Property

#End Region

#Region "funzioni specifiche per griglia Artico"
  Public Overridable Sub ArticoOnAddNew(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.Row!ar_codart Is Nothing Then Exit Sub
      'modifico il codice articolo per non far scattare più la ArticoBeforeColUpdate
      'il codice articolo verra caricato in testpresalva
      e.Row!ar_codart = "  "

      e.Row!ar_descr = NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_descr)
      e.Row!ar_desint = NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_desint)
      e.Row!ar_scomin = NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_scomin)
      e.Row!ar_scomax = NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_scomax)
      e.Row!ar_minord = NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_minord)
      e.Row!ar_note = NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_note)
      e.Row!ar_formula = NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_formula)

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
  Public Overridable Function ArticoNuovo(ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '----------------------------------------
      dReturn = oCldArtv.ArticoGetDataNuovo(strDittaCorrente, "".PadLeft(CLN__STD.CodartMaxLen, "z"c), ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldArtv.SetTableDefaultValueFromDB("ARTICO", ds)

      ArticoSetDefaultValue(ds)

      dsArticoShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsArticoShared.Tables("ARTICO").ColumnChanging, AddressOf ArticoBeforeColUpdate
      AddHandler dsArticoShared.Tables("ARTICO").ColumnChanged, AddressOf ArticoAfterColUpdate

      bArticoHasChanges = True

      bArticoNew = True

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
  Public Overridable Function ArticoApri(ByVal strDitta As String, ByRef dsArtico As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim dttTmp As New DataTable
    Try
      'GRIGLIA ARTICO
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldArtv.ArticoGetDataApri(strDittaCorrente, strArtvCodroot, dsArtico)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldArtv.SetTableDefaultValueFromDB("ARTICO", dsArtico)

      ArticoSetDefaultValue(dsArtico)

      dsArticoShared = dsArtico

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsArticoShared.Tables("ARTICO").ColumnChanging, AddressOf ArticoBeforeColUpdate
      AddHandler dsArticoShared.Tables("ARTICO").ColumnChanged, AddressOf ArticoAfterColUpdate

      bArticoHasChanges = False

      bArticoNew = False

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
  Public Overridable Sub ArticoSetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("ARTICO").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("ARTICO").Columns("ar_inesaur").DefaultValue = "N"

      If nNumliv = 1 Then
        ds.Tables("ARTICO").Columns("ar_codvar2").DefaultValue = " "
        ds.Tables("ARTICO").Columns("ar_codvar3").DefaultValue = " "
      ElseIf nNumliv = 2 Then
        ds.Tables("ARTICO").Columns("ar_codvar3").DefaultValue = " "
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

  Public Overridable Function ArticoRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsArticoShared.Tables("ARTICO").Select(strFilter)(nRow).RejectChanges()
      bArticoHasChanges = False
      bArticoNew = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function ArticoTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim dValmin As Decimal
    Dim dtrCurrRow() As DataRow
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate
      dtrTmp = dsArticoShared.Tables("ARTICO").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1

        If Trim(dtrTmp(i)!ar_codvar1.ToString) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590270706060086, "Codice variante 1 obbligatoria")))
          Return False
        End If
        If nNumliv >= 2 Then
          If Trim(dtrTmp(i)!ar_codvar2.ToString) = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590270721219634, "Codice variante 2 obbligatoria")))
            Return False
          End If
        End If
        If nNumliv = 3 Then
          If Trim(dtrTmp(i)!ar_codvar3.ToString) = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590270751226162, "Codice variante 3 obbligatoria")))
            Return False
          End If
        End If

        If Trim(dtrTmp(i)!ar_descr.ToString) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734633121569459, "Descrizione obbligatoria")))
          Return False
        End If

        If Trim(NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_unmis)) = "" And NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_codart) <> "D" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129035351739988106, "Unità di misura principale obbligatoria.")))
          Return False
        End If

        'Select Case NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_polriord)
        '  Case "M", "N" : dValmin = 1
        '  Case Else : dValmin = 0
        'End Select
        'If NTSCDec(dtrTmp(i)!ar_scomin) < dValmin Then
        '  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559254390025223, "Scorta minima deve essere maggiore di 0 (quando la Politica di Riordino è con Lotto)")))
        '  Return False
        'End If
        Select Case NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_polriord)
          Case "N" : dValmin = 1
          Case Else : dValmin = 0
        End Select
        If NTSCDec(dtrTmp(i)!ar_scomax) < dValmin Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590267584287186, "Attenzione!" & vbCrLf & _
            "La 'Scorta massima' deve essere maggiore di 0 quando la Politica di Riordino è di tipo:" & vbCrLf & _
            " --> A punto di riordino con lotto" & vbCrLf & _
            " --> A punto di riordino a ricostruzione scorta")))
          Return False
        End If
        Select Case NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_polriord)
          Case "F", "M", "O" : dValmin = 1
          Case Else : dValmin = 0
        End Select
        If NTSCDec(dtrTmp(i)!ar_minord) < dValmin Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734632775781570, "Attenzione!" & vbCrLf & _
            "La 'Quantità lotto standard' deve essere maggiore di 0 quando la Politica di Riordino è di tipo:" & vbCrLf & _
            " --> A punto di riordino con lotto" & vbCrLf & _
            " --> A punto di riordino a ricostruzione scorta")))
          Return False
        End If

        'scrivo il codice articolo
        dtrTmp(i)!ar_codart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrTmp(i)!ar_codvar1))
        If nNumliv >= 2 Then
          dtrTmp(i)!ar_codart = NTSCStr(dtrTmp(i)!ar_codart) & Trim(NTSCStr(dtrTmp(i)!ar_codvar2))
        End If
        If nNumliv = 3 Then
          dtrTmp(i)!ar_codart = NTSCStr(dtrTmp(i)!ar_codart) & Trim(NTSCStr(dtrTmp(i)!ar_codvar3))
        End If
      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrCurrRow = dsArticoShared.Tables("ARTICO").Select("codditt = " & CStrSQL(dtrTmp(0)!codditt.ToString) & _
      " AND ar_codvar1 = " & CStrSQL(dtrTmp(0)!ar_codvar1.ToString) & " AND ar_codvar2 = " & CStrSQL(dtrTmp(0)!ar_codvar2.ToString) & _
      " AND ar_codvar3 = " & CStrSQL(dtrTmp(0)!ar_codvar3.ToString))
      If dtrCurrRow.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128590275742468270, "Esiste gia una riga con le stesse caratteristiche")))
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

  Public Overridable Function ArticoTestPreCancella(ByVal dtrTmp As DataRow) As Boolean
    Dim dsTmp As DataSet = Nothing
    Dim strCodart As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se esiste l'articolo in ARTDEFX avvisa e annulla la cancellazione
      '-----------------------------------------------------------------------------------------
      Select Case nNumliv
        Case 1 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrTmp!ar_codvar1))
        Case 2 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrTmp!ar_codvar1)) & _
                             Trim(NTSCStr(dtrTmp!ar_codvar2))
        Case 3 : strCodart = Trim(strArtvCodroot) & Trim(NTSCStr(dtrTmp!ar_codvar1)) & _
                             Trim(NTSCStr(dtrTmp!ar_codvar2)) & _
                             Trim(NTSCStr(dtrTmp!ar_codvar3))
      End Select

      If Not oCldArtv.ArticoGetartdefx(strDittaCorrente, strCodart, dsTmp) Then Return False

      If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734632943128533, "La variante non può essere cancellata in quanto esiste nei 'Progressivi definitivi articoli'.")))
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

  Public Overridable Function ArticoSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Dim strError As String = ""

    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non Ã¨ una delete)
      If Not bDelete Then
        If Not ArticoTestPreSalva() Then Return False
      End If

      If Not oCldArtv.ArticoSalva(strDittaCorrente, strArtvCodroot, nNumliv, strPrevar, strError, _
            nArtvLungroot, nLungvar1, nLungvar2, nLungvar3, NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_descr), _
            dsArticoShared, dsShared) Then
        If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
        Return False
      End If

      bArticoHasChanges = False
      bArticoNew = False

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

  Public Overridable Sub ArticoBeforeColUpdate_ar_codvar1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim strTmp1 As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Dim strDescr As String = ""
    Try
      e.ProposedValue = UCase(e.ProposedValue.ToString)

      If dsVar1Shared.Tables("ARTVAR").Rows.Count > 0 Then
        For i = 0 To dsVar1Shared.Tables("ARTVAR").Rows.Count - 1
          If NTSCStr(dsVar1Shared.Tables("ARTVAR").Rows(i)!xx_codvar1) = e.ProposedValue.ToString Then
            strDescr = NTSCStr(dsVar1Shared.Tables("ARTVAR").Rows(i)!xx_descr1)
            Exit For
          End If
        Next
        strTmp = NTSCStr(e.Row!ar_descr) & " " & strDescr
        If Len(strTmp) > 40 Then
          e.Row!ar_descr = Mid(strTmp, 1, 40)
          strTmp1 = NTSCStr(e.Row!ar_desint) & " " & Mid(strTmp, 41)
          If Len(strTmp1) > 40 Then
            e.Row!ar_desint = Mid(strTmp1, 1, 40)
          Else
            e.Row!ar_desint = strTmp1
          End If
        Else
          e.Row!ar_descr = strTmp
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
  Public Overridable Sub ArticoBeforeColUpdate_ar_codvar2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim strTmp1 As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Dim strDescr As String = ""
    Try
      e.ProposedValue = UCase(e.ProposedValue.ToString)

      If dsVar2Shared.Tables("ARTVAR").Rows.Count > 0 Then
        For i = 0 To dsVar2Shared.Tables("ARTVAR").Rows.Count - 1
          If NTSCStr(dsVar2Shared.Tables("ARTVAR").Rows(i)!xx_codvar2) = e.ProposedValue.ToString Then
            strDescr = NTSCStr(dsVar2Shared.Tables("ARTVAR").Rows(i)!xx_descr2)
            Exit For
          End If
        Next
        strTmp = NTSCStr(e.Row!ar_descr) & " " & strDescr
        If Len(strTmp) > 40 Then
          e.Row!ar_descr = Mid(strTmp, 1, 40)
          strTmp1 = NTSCStr(e.Row!ar_desint) & " " & Mid(strTmp, 41)
          If Len(strTmp1) > 40 Then
            e.Row!ar_desint = Mid(strTmp1, 1, 40)
          Else
            e.Row!ar_desint = strTmp1
          End If
        Else
          e.Row!ar_descr = strTmp
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
  Public Overridable Sub ArticoBeforeColUpdate_ar_codvar3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim strTmp1 As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Dim strDescr As String = ""
    Try
      e.ProposedValue = UCase(e.ProposedValue.ToString)

      If dsVar3Shared.Tables("ARTVAR").Rows.Count > 0 Then
        For i = 0 To dsVar3Shared.Tables("ARTVAR").Rows.Count - 1
          If NTSCStr(dsVar3Shared.Tables("ARTVAR").Rows(i)!xx_codvar3) = e.ProposedValue.ToString Then
            strDescr = NTSCStr(dsVar3Shared.Tables("ARTVAR").Rows(i)!xx_descr3)
            Exit For
          End If
        Next
        strTmp = NTSCStr(e.Row!ar_descr) & " " & strDescr
        If Len(strTmp) > 40 Then
          e.Row!ar_descr = Mid(strTmp, 1, 40)
          strTmp1 = NTSCStr(e.Row!ar_desint) & " " & Mid(strTmp, 41)
          If Len(strTmp1) > 40 Then
            e.Row!ar_desint = Mid(strTmp1, 1, 40)
          Else
            e.Row!ar_desint = strTmp1
          End If
        Else
          e.Row!ar_descr = strTmp
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

  Public Overridable Sub ArticoBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      '-------------------------------------------------------------
      'se non  compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "ar_codart" Then
        If e.Row!ar_codart.ToString = " " Then ArticoOnAddNew(sender, e)
      End If

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
      Dim strFunction As String = "ArticoBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub ArticoAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bArticoHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "ArticoAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public ReadOnly Property ArticoRecordIsChanged() As Boolean
    Get
      Return bArticoHasChanges
    End Get
  End Property
#End Region

#Region "funzioni specifiche per BNMGARTV.BNMGAARV.VB"
  Public Overridable Function AarvCheckCodbar(ByVal strCodbar As String) As Boolean
    Dim dttTmp As New DataTable
    Dim strCodart As String
    Dim strTmp As String = ""
    Try
      If strCodbar = "" Then
        Return True
      End If
      If Not oCldArtv.ValCodiceDb(strCodbar, strDittaCorrente, "BARCODE", "S", , dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128586778832968750, "Codice a barre inesistente.")))
        Return False
      Else
        strCodart = NTSCStr(dttTmp.Rows(0)!bc_codart)
      End If

      If Not oCldArtv.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", strTmp, dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128586771278906250, "L'articolo '|" & strCodart & "|' " & vbCrLf & _
          "associato al codice a barre '|" & strCodbar & "|'" & vbCrLf & _
          "è inesistente.")))
        Return False
      Else
        If NTSCStr(dttTmp.Rows(0)!ar_gesvar) = "N" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128586782253437500, "L'articolo '|" & strCodart & "|' - |" & strTmp & vbCrLf & _
            "|associato al codice a barre '|" & strCodbar & "|'" & vbCrLf & _
            "non è gestito a varianti." & vbCrLf & _
            "Ripetere l'inserimento.")))
          Return False
        Else
          strArtvCodroot = NTSCStr(dttTmp.Rows(0)!ar_codroot)
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
      Return False
    End Try
  End Function

  Public Overridable Function AarvCheckCodroot(ByRef strCodroot As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      If strCodroot = "" Then
        Return True
      End If
      If Not InStr(1, strCodroot, "'") = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128586772985937500, "Il codice root non può contenere apici semplici.")))
        Return False
      End If
      If Not oCldArtv.ValCodiceDb(strCodroot, strDittaCorrente, "ARTROOT", "S") Then
        oCldArtv.ValCodiceDb(strCodroot, strDittaCorrente, "ARTICO", "S", , dttTmp)

        If dttTmp.Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129449121126622379, "Codice root inesistente.")))
          Return False
        ElseIf NTSCStr(dttTmp.Rows(0)!ar_codroot) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129449121110695997, "Codice root inesistente.")))
          Return False
        Else
          If Not oCldArtv.ValCodiceDb(NTSCStr(dttTmp.Rows(0)!ar_codroot), strDittaCorrente, "ARTROOT", "S", , dttTmp) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129449121118034624, "Codice root inesistente.")))
            Return False
          Else
            strCodroot = NTSCStr(dttTmp.Rows(0)!arr_codroot)
          End If
        End If
      Else
        If oCldArtv.GetArtRootFromArtico(strDittaCorrente, strCodroot) = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129999667149829114, "Attenzione!" & vbCrLf & _
            "Codice root esistente in tabella ARTROOT" & vbCrLf & _
            "ma NON in 'Anagrafica Articoli' (tabella ARTROOT)." & vbCrLf & _
            "Probabile anomalia nel Database.")))
          Return False
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
      Return False
    End Try
  End Function

  Public Overridable Function CheckArticoloTC() As Boolean
    Dim dttTmp As New DataTable
    Try
      If bModTCO = False Then
        Return True
      End If
      If oCldArtv.ValCodiceDb(strArtvCodroot, strDittaCorrente, "ARTICO", "S", , dttTmp) Then
        If NTSCInt(dttTmp.Rows(0)!ar_codtagl) <> 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734632650154158, "Articolo creato con 'Anagrafica articoli - Taglie e colori -'." & vbCrLf & _
            "Pertanto non può essere gestito con il programma 'Anagrafica articoli a varianti'.")))
          Return False
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
      Return False
    End Try
  End Function

  Public Overridable Function AarvCheckVarianti(ByVal strCodart As String, ByRef strCodroot As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not oCldArtv.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", , dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734632478119605, "Codice articolo inesistente.")))
        Return False
      Else
        strArtvCodroot = NTSCStr(dttTmp.Rows(0)!ar_codroot)
        strCodroot = strArtvCodroot
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
#End Region

#Region "funzioni specifiche per BNMGARTV.BNMGNARV.VB"
  Public Overridable Function NarvCheckCodroot(ByRef strCodroot As String, ByVal strLungroot As String) As Boolean
    Dim i As Integer
    Dim strZero As String
    Try
      If Not InStr(1, strCodroot, "'") = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128586880316090373, "Il codice articolo non può contenere apici semplici.")))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Controlla che il codice root indicato non contanga spazi a destra e/o a sinistra
      '-----------------------------------------------------------------------------------------
      If (Left(strCodroot, 1) = " ") Or (Right(strCodroot, 1) = " ") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128586880344708645, "Il codice articolo non può iniziare e/o terminare con spazi.")))
        Return False
      End If
      If UCase(strCodroot) <> "" Then
        If oCldArtv.ValCodiceDb(UCase(strCodroot), strDittaCorrente, "ARTROOT", "S") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128586880365038565, "Codice root già esistente.")))
          Return False
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Controlla in ARTICO che non esista già un codice articolo
      '--- uguale al codice root indicato in maschera
      '-----------------------------------------------------------------------------------------
      If UCase(strCodroot) <> "" Then
        If oCldArtv.ValCodiceDb(UCase(strCodroot), strDittaCorrente, "ARTICO", "S") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128586880387245093, "Codice articolo già esistente in Anagrafica Articoli." & vbCrLf & "Inserimento non possibile.")))
          Return False
        End If
      End If
      '-----------------------------------------------------------------------------------------
      'Viene eseguito il calcolo degli articoli generati in automatico solo se c'e' il flag
      'di registro "Generacodice" = -1 e la lunghezza del flag di registro "GeneraCodiceLungPreRoot" =
      'lunghezza dell'articolo digitato.

      If strGeneraCodice = "-1" And nLungPreRoot = Len(strCodroot) Then
        '-----------------------------------------------------
        bArticoloGenerato = False
        If Not nLungPreRoot > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128586880689535365, "Valore nel Registro di Business: BSMGARTV-OPZIONI-GeneraCodiceLungPreRoot" & vbCrLf & "non valido." & vbCrLf & _
            "Generazione articolo non possibile!")))
          Return False
        End If
        nLungExt = NTSCInt(strLungroot) - nLungPreRoot
        If Not nLungExt > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734632350304651, "Valore nel Registro di Business: BSMGARTV-OPZIONI-GeneraCodiceLungPreRoot" & vbCrLf & "non valido.E' maggiore o uguale della lunghezza del codice root!" & vbCrLf & _
            "Generazione articolo non possibile!")))
          Return False
        Else
          If nLungPreRoot + nLungExt > 12 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128734632226708528, "Somma del valori nel Registro di Business:" & vbCrLf & _
              "BSMGARTV-OPZIONI-GeneraCodiceLungPreRoot e della lunghezza codice root" & vbCrLf & _
              "superiore a 12. Generazione articolo non possibile!")))
            Return False
          End If
          strZero = ""
          For i = 1 To nLungExt
            strZero = strZero & "0"
          Next i
        End If
        If ((Len(strCodroot) = (nLungPreRoot + nLungExt)) And IsNumeric(Right(strCodroot, nLungExt))) Then
          lProgr = oCldArtv.LegNuma(strDittaCorrente, "AA", UCase(Left(strCodroot, nLungPreRoot)), 0, True)
        Else
          If Len(strCodroot) = nLungPreRoot Then
            lProgr = oCldArtv.LegNuma(strDittaCorrente, "AA", UCase(strCodroot), 0, True)
            strCodroot = UCase(strCodroot) & Right(strZero & CStr(lProgr), nLungExt)
          Else
            GoTo CheckCodrootGenerato
          End If
        End If
        bArticoloGenerato = True

CheckCodrootGenerato:
        If bArticoloGenerato = False Then
          If Not (Len(strCodroot) = NTSCInt(strLungroot)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128586881193404613, "Numero di caratteri valido per codice articolo : '|" & strLungroot & "|'.")))
            Return False
          End If
        End If
      Else
        If Not (Len(strCodroot) = NTSCInt(strLungroot)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128586882493168268, "Numero di caratteri valido per codice articolo : '|" & strLungroot & "|'.")))
          Return False
        Else
          Return True
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
      Return False
    End Try
  End Function

  Public Overridable Function NarvGeneraCodice(ByRef strCodroot As String, ByVal strLungroot As String) As Boolean
    Dim dsTmp As DataSet = Nothing
    Dim nProgr As Integer
    Dim strMsg As String = ""
    Dim evt As NTSEventArgs = Nothing
    Dim lcod As Integer
    Try
      lProgressivo = 0
      If (bArticoloGenerato) Then
        If Len(strCodroot) = (nLungPreRoot + nLungExt) Then
          If IsNumeric(Right(strCodroot, nLungExt)) Then
            If lProgr = NTSCInt(Right(strCodroot, nLungExt)) Then
              lProgressivo = lProgr
              strRadice = Left(strCodroot, nLungPreRoot)
            Else
              oCldArtv.GetTabNuma(strDittaCorrente, strCodroot, nLungPreRoot, dsTmp)
              If dsTmp.Tables("TABNUMA").Rows.Count > 0 Then
                nProgr = NTSCInt(dsTmp.Tables("TABNUMA").Rows(0)!tb_numprog)
              End If

              If NTSCInt(Right(strCodroot, nLungExt)) > nProgr Then
                strMsg = oApp.Tr(Me, 130427306331272080, "Numero progressivo superiore a quello relativo nella tebella delle numerazioni." & vbCrLf & _
                  "Aggiornare la numerazione una volta salvato l'articolo?")
                evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, strMsg)
                ThrowRemoteEvent(evt)
                If evt.RetValue = "YES" Then
                  lProgressivo = NTSCInt(Right(strCodroot, nLungExt))
                  strRadice = Left(strCodroot, nLungPreRoot)
                End If
              End If
            End If
          End If
        Else
          If Len(strCodroot) = nLungPreRoot Then
            NarvCheckCodroot(strCodroot, strLungroot)
            lProgressivo = lProgr
            strRadice = Left(strCodroot, nLungPreRoot)
          End If
        End If
      End If

      'solo sui nuovi articoli e se opzione abilitata provvedo ad aggiornare il progressivo articolo
      If strRadice <> "" And lProgressivo > 0 Then
        lcod = oCldArtv.AggNuma(strDittaCorrente, "AA", strRadice, 0, lProgressivo, True, True, "")
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

  Public Overridable Function edCodvari1_Validated(ByVal strCod As String, ByRef strLung As String, ByRef strErr As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not NTSCStr(strCod) = "" Then
        If Not oCldArtv.ValCodiceDb(NTSCStr(strCod), strDittaCorrente, "TABVARI", "S", , dttTmp) Then
          strErr = oApp.Tr(Me, 128734632044517530, "Codice variante insistente.")
          Return False
        Else
          strLung = NTSCStr(dttTmp.Rows(0)!tb_lung)
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
  Public Overridable Function edCodvari2_Validated(ByVal strCod As String, ByRef strLung As String, ByRef strErr As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not NTSCStr(strCod) = "" Then
        If Not oCldArtv.ValCodiceDb(NTSCStr(strCod), strDittaCorrente, "TABVARI", "S", , dttTmp) Then
          strErr = oApp.Tr(Me, 128587057142432326, "Codice variante insistente.")
          Return False
        Else
          strLung = NTSCStr(dttTmp.Rows(0)!tb_lung)
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
  Public Overridable Function edCodvari3_Validated(ByVal strCod As String, ByRef strLung As String, ByRef strErr As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not NTSCStr(strCod) = "" Then
        If Not oCldArtv.ValCodiceDb(NTSCStr(strCod), strDittaCorrente, "TABVARI", "S", , dttTmp) Then
          strErr = oApp.Tr(Me, 128734632092174695, "Codice variante insistente.")
          Return False
        Else
          strLung = NTSCStr(dttTmp.Rows(0)!tb_lung)
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
#End Region

#Region "funzioni specifiche per BNMGARTV.BNMGDUAR.VB"
  Public Overridable Function CheckCodartDuar(ByRef strCodart As String) As Boolean
    Dim strMsg As String
    Try
      '-----------------------------------------------------
      If strGeneraCodice <> "-1" Then
        CheckCodartDuar = True
        Exit Function
      End If
      '-----------------------------------------------------
      Dim i As Integer
      Dim strZero As String

      bDuarArticoloGenerato = False
      If Not nLungRoot > 0 Then
        strMsg = oApp.Tr(Me, 130427306749868509, "Valore nel Registro di Business: BSMGARTV-OPZIONI-GeneraCodiceLungRoot" & _
                 vbCrLf & "non valido." & vbCrLf & _
                 "Generazione articolo non possibile!")
        ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        Exit Function
      End If
      If Not nLungExt > 0 Then
        strMsg = oApp.Tr(Me, 130427308461441963, "Valore nel Registro di Business: BSMGARTV-OPZIONI-GeneraCodiceLungExt" & vbCrLf & "non valido." & vbCrLf & _
          "Generazione articolo non possibile!")
        ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        Exit Function
      Else
        If nLungRoot + nLungExt > CLN__STD.CodartMaxLen Then
          strMsg = oApp.Tr(Me, 130427308550817535, "Somma dei valori nel Registro di Business:" & vbCrLf & _
            "BSMGARTI-OPZIONI-GeneraCodiceLungRoot e BSMGARTV-OPZIONI-GeneraCodiceLungExt" & vbCrLf & _
            "superiore a |" & CLN__STD.CodartMaxLen.ToString & "|. Generazione articolo non possibile!")
          ThrowRemoteEvent(New NTSEventArgs("", strMsg))
          Exit Function
        End If
        strZero = ""
        For i = 1 To nLungExt
          strZero = strZero & "0"
        Next
      End If
      If ((Len(strCodart) = (nLungRoot + nLungExt)) And IsNumeric(Microsoft.VisualBasic.Right(strCodart, nLungExt))) Then
        lProgr = oCldArtv.LegNuma(strDittaCorrente, "AA", UCase(Microsoft.VisualBasic.Left(strCodart, nLungRoot)), 0, True)
      Else
        If Len(strCodart) = nLungRoot Then
          lProgr = oCldArtv.LegNuma(strDittaCorrente, "AA", UCase(strCodart), 0, True)
          strCodart = UCase(strCodart) & Microsoft.VisualBasic.Right(strZero & NTSCStr(lProgr), nLungExt)
        End If
      End If
      bDuarArticoloGenerato = True

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

  Public Overridable Function CheckArticoloDuar(ByRef strCodart As String) As Boolean
    Dim strMsg As String
    Dim evt As NTSEventArgs = Nothing
    Dim lcod As Integer
    Dim dsTabNuma As DataSet = Nothing
    Dim dsArticoTmp As DataSet = Nothing
    Dim dsTmp As DataSet = Nothing
    Try
      lDuarProgr = 0
      If bDuarGeneraArticoli Then
        If (bDuarArticoloGenerato) Then
          If Len(strCodart) = (nLungRoot + nLungExt) Then
            If IsNumeric(Right(strCodart, nLungExt)) Then
              If lProgr = CLng(Right(strCodart, nLungExt)) Then
                lDuarProgr = lProgr
                strDuarRoot = Left(strCodart, nLungRoot)
              Else

                oCldArtv.GetTabNuma(strDittaCorrente, strCodart, nLungRoot, dsTabNuma)

                If dsTabNuma.Tables("TABNUMA").Rows.Count > 0 Then
                  If NTSCInt(Right(strCodart, nLungExt)) > NTSCInt(dsTabNuma.Tables("TABNUMA").Rows(0)!tb_numprog) Then
                    strMsg = oApp.Tr(Me, 130427310891926268, "Numero progressivo superiore a quello relativo nella tebella delle numerazioni." & vbCrLf & _
                      "Aggiornare la numerazione una volta salvato l'articolo?")
                    evt = New NTSEventArgs("MSG_YESNO", strMsg)
                    ThrowRemoteEvent(evt)
                    If evt.RetValue = "YES" Then
                      lDuarProgr = NTSCInt(Right(strCodart, nLungExt))
                      strDuarRoot = Left(strCodart, nLungRoot)
                    End If
                  End If
                End If
              End If
            End If
          Else
            If Len(strCodart) = nLungRoot Then
              CheckCodart(strCodart)
              lDuarProgr = lProgr
              strDuarRoot = Left(strCodart, nLungRoot)
            End If
          End If
        End If
      Else
        oCldArtv.GetArticoRoot(strDittaCorrente, strArtvCodroot, dsTmp)
        If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
          If Len(strCodart) + _
            NTSCStr(dsTmp.Tables("ARTICO").Rows(0)!ar_codvar1).Trim.Length + _
            NTSCStr(dsTmp.Tables("ARTICO").Rows(0)!ar_codvar2).Trim.Length + _
            NTSCStr(dsTmp.Tables("ARTICO").Rows(0)!ar_codvar3).Trim.Length > CLN__STD.CodartMaxLen Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128925531376921182, "Codice articolo + codice variante > di |" & CLN__STD.CodartMaxLen.ToString & "| caratteri.")))
            Return False
          End If
        End If
      End If
      If strCodart = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128553116542074389, "Codice articolo obbligatorio.")))
        Return False
      End If

      oCldArtv.GetArtico(strDittaCorrente, strCodart, dsArticoTmp)

      If dsArticoTmp.Tables("ARTICO").Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128553117843845967, "Codice articolo '|" & UCase(strCodart) & "|' già esistente.")))
        If (bDuarGeneraArticoli) And (bDuarArticoloGenerato) And (Len(strCodart) = (nLungRoot + nLungExt)) And (IsNumeric(Right(strCodart, nLungExt))) Then
          strCodart = Left(strCodart, nLungRoot)
        End If
        Return False
      End If

      'Controlla che il codice root indicato non contanga spazi a destra e/o a sinistra
      If (Left(strCodart, 1) = " ") Or (Right(strCodart, 1) = " ") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128667354553928063, "Il codice articolo nuovo: |" & strCodart & "| non può iniziare e/o terminare con spazi.")))
        Return False
      End If

      'controllo che la lunghezza del codice root sia identica a quella da copiare
      If Len(strArtvCodroot) <> Len(strCodart) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128667348787557985, "La lunghezza del nuovo root articolo: |" & strCodart & "| deve essere uguale a quella vecchia |" & strArtvCodroot & "|.")))
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      'solo sui nuovi articoli e se opzione abilitata provvedo ad aggiornare il progressivo articolo
      If bDuarGeneraArticoli And strDuarRoot <> "" And lDuarProgr > 0 Then
        lcod = oCldArtv.AggNuma(strDittaCorrente, "AA", strDuarRoot, 0, lDuarProgr, True, True, "")
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

  Public Overridable Function CheckCodart(ByRef strCodart As String) As Boolean
    Dim strMsg As String
    Try
      '-----------------------------------------------------
      If bDuarGeneraArticoli = False Then
        CheckCodart = True
        Exit Function
      End If
      If strGeneraCodice <> "-1" Then
        CheckCodart = True
        Exit Function
      End If
      '-----------------------------------------------------
      Dim i As Integer
      Dim strZero As String

      bDuarArticoloGenerato = False
      If Not nLungRoot > 0 Then
        strMsg = oApp.Tr(Me, 130427311111615174, "Valore nel Registro di Business: BSTCARTV-OPZIONI-GeneraCodiceLungRoot" & vbCrLf & "non valido." & vbCrLf & _
          "Generazione articolo non possibile!")
        ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        Exit Function
      End If
      If Not nLungExt > 0 Then
        strMsg = oApp.Tr(Me, 130427311220209619, "Valore nel Registro di Business: BSTCARTV-OPZIONI-GeneraCodiceLungExt" & vbCrLf & "non valido." & vbCrLf & _
          "Generazione articolo non possibile!")
        ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        Exit Function
      Else
        If nLungRoot + nLungExt > CLN__STD.CodartMaxLen Then
          strMsg = oApp.Tr(Me, 130427311306616422, "Somma dei valori nel Registro di Business:" & vbCrLf & _
            "BSMGARTI-OPZIONI-GeneraCodiceLungRoot e BSTCARTV-OPZIONI-GeneraCodiceLungExt" & vbCrLf & _
            "superiore a |" & CLN__STD.CodartMaxLen.ToString & "|. Generazione articolo non possibile!")
          ThrowRemoteEvent(New NTSEventArgs("", strMsg))
          Exit Function
        End If
        strZero = ""
        For i = 1 To nLungExt
          strZero = strZero & "0"
        Next
      End If
      If ((Len(strCodart) = (nLungRoot + nLungExt)) And IsNumeric(Microsoft.VisualBasic.Right(strCodart, nLungExt))) Then
        lProgr = oCldArtv.LegNuma(strDittaCorrente, "AA", UCase(Microsoft.VisualBasic.Left(strCodart, nLungRoot)), 0, True)
      Else
        If Len(strCodart) = nLungRoot Then
          lProgr = oCldArtv.LegNuma(strDittaCorrente, "AA", UCase(strCodart), 0, True)
          strCodart = UCase(strCodart) & Microsoft.VisualBasic.Right(strZero & NTSCStr(lProgr), nLungExt)
        End If
      End If
      bDuarArticoloGenerato = True

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
#End Region

#Region "funzioni specifiche per BNMGARTV.BNMGARTA.VB"
  Public Overridable Function ArtaApri(ByVal strDitta As String, ByVal strArtaCodart As String, ByRef dsArta As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArtv.GetDataArta(strDitta, strArtaCodart, dsArta)
      If dReturn = False Then Return False

      oCldArtv.SetTableDefaultValueFromDB("ARTACCE", dsArta)

      dsArtaShared = dsArta

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsArtaShared.Tables("ARTACCE").ColumnChanging, AddressOf ArtaBeforeColUpdate
      AddHandler dsArtaShared.Tables("ARTACCE").ColumnChanged, AddressOf ArtaAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsArtaShared.Tables("ARTACCE").Columns("codditt").DefaultValue = strDittaCorrente
      dsArtaShared.Tables("ARTACCE").Columns("apa_codart").DefaultValue = strArtaCodart
      dsArtaShared.Tables("ARTACCE").Columns("apa_ultagg").DefaultValue = Now.ToShortDateString
      dsArtaShared.Tables("ARTACCE").Columns("apa_opnome").DefaultValue = oApp.User.Nome

      dsArtaShared.Tables("ARTACCE").AcceptChanges()

      bArtaHasChanges = False

      bSucc = CBool(oCldArtv.GetSettingBus("BSMGARTI", "OPZIONI", ".", "RicreaAncheSuccedaneiCollegati", "0", " ", "0"))
      bAcc = CBool(oCldArtv.GetSettingBus("BSMGARTI", "OPZIONI", ".", "RicreaAncheAccessoriCollegati", "0", " ", "0"))
      bRicreaSuccedaneiAccessori = CBool(oCldArtv.GetSettingBus("BSMGARTI", "OPZIONI", ".", "RicreaSuccedaneiAccessori", "0", " ", "0"))

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
  Public Overridable Function ArtaSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Dim dttSucc As New DataTable
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not ArtaTestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "artacce"
        strActLogDesLog = oApp.Tr(Me, 128574789938005106, "Articoli accessori/succedanei")
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArtv.ScriviTabellaSemplice(strDittaCorrente, "ARTACCE", dsArtaShared.Tables("ARTACCE"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bArtaHasChanges = False

        'Se il salvataggio è andato a buon fine modifico anche gli accessori e succedanei
        If bRicreaSuccedaneiAccessori Then
          If Not oCldArtv.RicreaSuccedaneiAccessori(strDittaCorrente, NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_codart), bSucc, bAcc, dsArtaShared.Tables("ARTACCE")) Then Return False
        End If

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
  Public ReadOnly Property ArtaRecordIsChanged() As Boolean
    Get
      Return bArtaHasChanges
    End Get
  End Property
  Public Overridable Function ArtaTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsArtaShared.Tables("ARTACCE").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCStr(dtrCurrRow(i)!apa_codartas) = " " Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128562881000035201, "Codice articolo obbligatorio.")))
          Return False
        End If

        If NTSCStr(dtrCurrRow(i)!apa_codart) = NTSCStr(dtrCurrRow(i)!apa_codartas) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128562884465715601, "L'articolo indicato, non può essere uguale all'articolo principale.")))
          Return False
        End If
      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      If dsArtaShared.Tables("ARTACCE").Select(Nothing, Nothing, DataViewRowState.Added).Length > 0 Then
        dtrTmp = dsArtaShared.Tables("ARTACCE").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
          " AND apa_codart = " & CStrSQL(dtrCurrRow(0)!apa_codart.ToString) & _
          " AND apa_codartas = " & CStrSQL(dtrCurrRow(0)!apa_codartas.ToString))
        If dtrTmp.Length > 1 Then
          ' una nuova riga uguale ad un'altra precedentemente inserita
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128562881169627001, "Esiste gia una riga con le stesse caratteristiche")))
          Return False
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
      Return False
    End Try
  End Function
  Public Overridable Sub ArtaNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsArtaShared.Tables("ARTACCE").Rows.Add(dsArtaShared.Tables("ARTACCE").NewRow)
      bArtaHasChanges = True

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
  Public Overridable Function ArtaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsArtaShared.Tables("ARTACCE").Select(strFilter)(nRow).RejectChanges()
      bArtaHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub ArtaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "ArtaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub ArtaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bArtaHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "ArtaAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub ArtaBeforeColUpdate_apa_codartas(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(e.ProposedValue) = " " Then
        e.Row!xx_codartas = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.ProposedValue = e.ProposedValue.ToString.ToUpper
      '--------------------------------------------------------------------------------------------------------------
      If oCldArtv.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", "", dttTmp) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128562880138157291, "Codice articolo non corretto.")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(dttTmp.Rows(0)!ar_gesvar) = "S") And (NTSCStr(dttTmp.Rows(0)!ar_codroot).Trim = "") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415125521034892, "Indicare solo articoli reali.")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      e.Row!xx_codartas = NTSCStr(dttTmp.Rows(0)!ar_descr)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub CompletaSuccedaneiAccessori()
    Dim dttTmp As New DataTable
    Dim strQuery As New Text.StringBuilder
    Dim dtrRow() As DataRow
    Dim i As Integer = 0
    Try
      If Not bRicreaSuccedaneiAccessori Then Return

      For i = 0 To dsArtaShared.Tables("ARTACCE").Rows.Count - 1
        strQuery.Append(CStrSQL(dsArtaShared.Tables("ARTACCE").Rows(i)!apa_codartas) & ", ")
      Next

      strQuery.Remove(strQuery.Length - 2, 2)

      'cerco tutti i succedani\accessori collegati agli articoli accessori\succedanei dell'articolo attuale
      oCldArtv.CompletaSuccedaneiAccessori(strDittaCorrente, strQuery.ToString, bAcc, bSucc, dttTmp)

      For i = 0 To dttTmp.Rows.Count - 1
        dtrRow = dsArtaShared.Tables("ARTACCE").Select("apa_codartas = '" & NTSCStr(dttTmp.Rows(i)!apa_codart) & _
                                                       "' AND apa_tipo = '" & NTSCStr(dttTmp.Rows(i)!apa_tipo) & "'")

        'Se non c'è lo aggiungo
        If dtrRow.Length = 0 Then
          If NTSCStr(dttTmp.Rows(i)!apa_codart) = NTSCStr(dsArtaShared.Tables("ARTACCE").Rows(0)!apa_codart) Then Continue For
          dsArtaShared.Tables("ARTACCE").Rows.Add()
          With dsArtaShared.Tables("ARTACCE").Rows(dsArtaShared.Tables("ARTACCE").Rows.Count - 1)
            !codditt = strDittaCorrente
            !apa_codart = dsArtaShared.Tables("ARTACCE").Rows(0)!apa_codart
            !apa_codartas = dttTmp.Rows(i)!apa_codart
            !apa_tipo = dttTmp.Rows(i)!apa_tipo
            !apa_ultagg = Now
            !apa_opnome = oApp.User.Nome
          End With
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

  Public Overridable Function CancellaArtacce(ByVal strCodart As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldArtv.CancellaArtacce(strDittaCorrente, strCodart)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNMGARTV.BNMGANEX.VB"
  Public Overridable Function GetTabaext(ByRef dttTmp As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldArtv.GetTabaext(strDittaCorrente, dttTmp)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function
#End Region

End Class
