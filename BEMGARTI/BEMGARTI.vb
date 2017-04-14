Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO

Public Class CLEMGARTI
  Inherits CLE__BASE

  Public oCldArti As CLDMGARTI

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
  Public nCodArtDaCat As Integer
  Public nCodArtDaCatNListPubb As Integer
  Public nCodArtDaCatNListIngr As Integer
  Public bDuplicaListini As Boolean
  Public bDuplicaSconti As Boolean
  Public bDuplicaProvvigioni As Boolean
  Public bDuplicaDescrLingua As Boolean
  Public bDuplicaKit As Boolean
  Public bDuplicaConai As Boolean
  Public bDuplicaFasi As Boolean
  Public bAbilitaPrezzoUM As Boolean
  Public bValidaNomenclCombin As Boolean
  Public strAltezzaMaxImg As String
  Public bRicreaSuccedaneiAccessori As Boolean

  Public strTipoConfiguratore As String
  Public bServer As Boolean
  Public bCP2 As Boolean
  Public bLogisticaEstesa As Boolean
  Public strUnmisOrigine As String
  Public strUnmis2Origine As String
  Public strConfez2Origine As String
  Public strUm4Origine As String
  Public bDuplicaArt As Boolean
  Public strWhereFiar As String
  Public strOrderBy As String = ""
  Public strCodart As String
  Public strModello As String
  Public bValarti As Boolean
  Public bNew As Boolean = False
  Public bDuplicazioneInCorso As Boolean
  Public bDaGest As Boolean

  Public strCodartDelete As String    'codice articolo per cancellazione
  Public strTipoOpzDelete As String
  Public nCodtipaDelete As Integer
  Public strImageDir As String

  Public strArtiDescr As String
  Public strArtiDesint As String
  Public lArtiForn As Integer
  Public strArtiNote As String

  Public bGestUbicSenzaLext As Boolean

  Public bCampiCAEAttivi As Boolean
  'generazione barcode
  Public bCreaBarcodeE13 As Boolean = False
  Public bIndicod As Boolean = False
  Public strPrefixEAN13 As String = ""

  'mgcoar
  Public bCoarArticoloGenerato As Boolean
  Public lCoarProgr As Integer
  Public strCoarRoot As String
  Public nCoarEditmode As Integer
  Public strGeneraCodice As String
  Public nLungRoot As Integer
  Public nLungExt As Integer
  Public bFocusCodArtCodBar As Boolean
  Public strCodBarArt As String
  Public bCoarGeneraArticoli As Boolean
  Public lProgr As Integer

  'mgduar
  Public strCodartDuar As String
  Public bDuarArticoloGenerato As Boolean
  Public lDuarProgr As Integer
  Public strDuarRoot As String
  Public bDuarGeneraArticoli As Boolean
  Public dtrTmpDuar As DataRow

  'mgapro
  Public dsAproShared As DataSet
  Public bAproHasChanges As Boolean = False

  'mgarta
  Public dsArtaShared As DataSet
  Public bArtaHasChanges As Boolean = False
  Public bSucc, bAcc As Boolean

  'mgcona
  Public dsConaShared As DataSet
  Public bConaHasChanges As Boolean = False

  'mgvala
  Public dsValaAll As DataSet 'dataset contenente sia i dati di vala che di val2
  Public dsValaShared As DataSet
  Public bValaHasChanges As Boolean = False
  Public strValaCodart As String
  Public strValaDesart As String
  Public nValaCodtipa As Integer

  'mgval2
  Public dsVal2Shared As DataSet
  Public bVal2HasChanges As Boolean = False
  Public strVal2Campo As String
  Public strVal2CodLing As String
  Public strVal2XxCodLing As String
  Public strVal2Tipcamp As String

  Public bNoMsgCongruenzaPolSconte As Boolean = False
  Public bModuloORTO As Boolean = False

  Public strArtAccSuccCodart As String = ""

  Public bSelCodiceNoApri As Boolean = False

  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR + bsModPM
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE + bsModExtCRM
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
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGARTI"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldArti = CType(MyBase.ocldBase, CLDMGARTI)
    oCldArti.Init(oApp)
    Return True
  End Function

  Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim dttTmp As New DataTable
    Try
      If strCodart <> "" Then
        If oCldArti.ValCodiceDb(strCodart, strDitta, "ARTICO", "S", "", dttTmp) Then
          If NTSCStr(dttTmp.Rows(0)!ar_gesvar) = "S" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128553295125591424, "Non si può aprire una variante.")))
            strCodart = ""
            Return False
          End If
        End If
      End If

      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldArti.GetDataApri(strDittaCorrente, strWhereFiar, strCodart, ds, strOrderBy)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldArti.SetTableDefaultValueFromDB("ARTICO", ds)

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

  Public Overridable Shadows Sub Nuovo(ByRef ds As DataSet)
    Dim dReturn As Boolean = False
    Try
      '----------------------------------------
      dReturn = oCldArti.GetDataNuovo(strDittaCorrente, ds)
      If dReturn = False Then Return

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldArti.SetTableDefaultValueFromDB("ARTICO", ds)

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
      If dsShared IsNot Nothing Then dsShared.Tables(strNomeTabella).Select(strFilter)(nRow).RejectChanges()
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
      strActLog = ocldBase.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BSMGARTI"
      strActLogNomOggLog = "ARTICO"
      strActLogDesLog = oApp.Tr(Me, 128550728330311752, "Anagrafica Articoli")

      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("ARTICO").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("ARTICO").Columns("ar_codart").DefaultValue = strCodart
      If strCodart = "D" Or strCodart = "M" Then
        ds.Tables("ARTICO").Columns("ar_unmis").DefaultValue = " "
      Else
        ds.Tables("ARTICO").Columns("ar_unmis").DefaultValue = strUnmis
      End If
      ds.Tables("ARTICO").Columns("ar_conver").DefaultValue = 1
      ds.Tables("ARTICO").Columns("ar_codiva").DefaultValue = nCodiva
      ds.Tables("ARTICO").Columns("ar_controp").DefaultValue = nControp
      ds.Tables("ARTICO").Columns("ar_controa").DefaultValue = nControa
      ds.Tables("ARTICO").Columns("ar_contros").DefaultValue = nContros
      ds.Tables("ARTICO").Columns("ar_ultagg").DefaultValue = Now.ToShortDateString
      ds.Tables("ARTICO").Columns("ar_oragg").DefaultValue = (Now.Hour * 100) + Now.Minute
      ds.Tables("ARTICO").Columns("ar_datins").DefaultValue = Now.ToShortDateString
      ds.Tables("ARTICO").Columns("ar_orins").DefaultValue = (Now.Hour * 100) + Now.Minute
      ds.Tables("ARTICO").Columns("ar_perragg").DefaultValue = "G"
      ds.Tables("ARTICO").Columns("ar_webvis").DefaultValue = "N"
      ds.Tables("ARTICO").Columns("ar_webusat").DefaultValue = "N"
      ds.Tables("ARTICO").Columns("ar_webvend").DefaultValue = "N"

      DecodCodciva(NTSCStr(nCodiva), strDescr)
      ds.Tables("ARTICO").Columns("xx_codiva").DefaultValue = strDescr
      DecodCodcove(NTSCStr(nControp), strDescr)
      ds.Tables("ARTICO").Columns("xx_controp").DefaultValue = strDescr
      DecodCodcove(NTSCStr(nControa), strDescr)
      ds.Tables("ARTICO").Columns("xx_controa").DefaultValue = strDescr
      DecodCodcove(NTSCStr(nContros), strDescr)
      ds.Tables("ARTICO").Columns("xx_contros").DefaultValue = strDescr
      '--------------------------------------------------------------------------------------------------------------
      '--- Se è attivo il modulo ORTO, inizializza il campo "Tipo" con "C"
      '--------------------------------------------------------------------------------------------------------------
      If bModuloORTO = True Then ds.Tables("ARTICO").Columns("ar_tipo").DefaultValue = "C"
      '--------------------------------------------------------------------------------------------------------------
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
      'nel caso specifico di 'ar_codnomc' la funzione ValoriUguali di BN__STD
      'non funziona correttamente (per esempio: i codici 38089390 e 3808.93.90 sono considerati uguali) 
      If e.Column.ColumnName.ToLower.Equals("ar_codnomc") Then
        If NTSCStr(e.ProposedValue).Equals(NTSCStr(e.Row!ar_codnomc)) Then
          Return
        End If
      Else
        'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
        'solo se il dato e uguale a quello precedentemente contenuto nella cella
        If ValoriUguali(NTSCStr(e.ProposedValue), e.Row(e.Column.ColumnName).ToString) Then
          strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
          Return
        End If
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABUMIS", "S", "", dttTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129034655766461449, "Unità di misura principale inesistente.")))
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
  Public Overridable Sub BeforeColUpdate_ar_cartric(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.ProposedValue = ""
        e.Row!xx_cartric = ""
        Exit Sub
      Else
        e.ProposedValue = UCase(NTSCStr(e.ProposedValue))
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128443711511277373, "Articolo vendita ricambio inesistente.")))
          Return
        Else
          e.Row!xx_cartric = strOut
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
  Public Overridable Sub BeforeColUpdate_ar_cartcanas(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!xx_cartcanas = ""
      Else
        e.ProposedValue = UCase(NTSCStr(e.ProposedValue))
        Dim dttCodArtCano As New DataTable
        If ControllaArticoloCanone(NTSCStr(e.ProposedValue), dttCodArtCano) Then
          e.Row!xx_cartcanas = dttCodArtCano.Rows(0)!ar_descr
        Else
          e.ProposedValue = e.Row(e.Column.ColumnName)
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
  Public Overridable Sub BeforeColUpdate_ar_cartcanol(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!xx_cartcanol = ""
      Else
        e.ProposedValue = UCase(NTSCStr(e.ProposedValue))
        Dim dttCodArtCano As New DataTable
        If ControllaArticoloCanone(NTSCStr(e.ProposedValue), dttCodArtCano) Then
          e.Row!xx_cartcanol = dttCodArtCano.Rows(0)!ar_descr
        Else
          e.ProposedValue = e.Row(e.Column.ColumnName)
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
  Public Overridable Function ControllaArticoloCanone(ByVal strCodArtCano As String, ByRef dttCodArtCano As DataTable) As Boolean
    '---------------------------------------------------------------------------------------
    '--- Controllo che l'articolo canone non sia gestito a fasi/lotti/ubic/t&c
    '--- (permessa solo gestione a commessa e matricole)
    '---------------------------------------------------------------------------------------
    Try
      If oCldArti.ValCodiceDb(strCodArtCano, strDittaCorrente, "ARTICO", "S", "", dttCodArtCano) AndAlso (Not dttCodArtCano Is Nothing) AndAlso (dttCodArtCano.Rows.Count > 0) Then
        Dim strGest As String = ""
        With dttCodArtCano.Rows(0)
          If Not NTSCStr(!ar_geslotti) = "N" Then strGest = oApp.Tr(Me, 128556843301152237, "- lotti") & vbCrLf
          'If Not NTSCStr(!ar_gestmatr) = "N" Then strGest &= oApp.Tr(Me, 128556843301152238, "- matricole") & vbCrLf
          If Not NTSCStr(!ar_gesubic) = "N" Then strGest &= oApp.Tr(Me, 128556843301152239, "- ubicazioni") & vbCrLf
          If Not NTSCStr(!ar_gesfasi) = "N" Then strGest &= oApp.Tr(Me, 128556843301152231, "- fasi") & vbCrLf
          If Not NTSCStr(!ar_codtagl) = "0" Then strGest &= oApp.Tr(Me, 128556843301152232, "- taglie e colori") & vbCrLf
          If strGest <> "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556843301152236, "L'articolo |" & strCodArtCano & "| selezionato ha le seguenti gestioni attive:|" & vbCrLf & vbCrLf & strGest & vbCrLf & "|e non può essere indicato nel campo articolo canone." & vbCrLf & "L'unica gestione eventualmente permessa è quella a commessa o a matricole.")))
          Else
            Return True
          End If
        End With
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556843301152235, "L'articolo |" & strCodArtCano & "| indicato nel campo articolo canone è inesistente.")))
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

  Public Overridable Sub BeforeColUpdate_ar_codimba(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strOut As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.ProposedValue = 0
        e.Row!xx_codimba = ""
        Exit Sub
      Else
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABIMBA", "N", strOut) Then
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABUMIS", "S", "", dttTmp) Then
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
          If Not (NTSCInt(e.ProposedValue) >= 0 And NTSCInt(e.ProposedValue) <= 999) Then
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABUMIS", "S", "", dttTmp) Then
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABUMIS", "S", "", dttTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812443851959536, "Unità di misura secondaria inesistente.")))
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABAPPR", "N", strOut) Then
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

      If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "DISTBAS", "S") Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812443888522270, "Non esiste distinta base per questo articolo")))
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCIVA", "N", strOut) Then
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMARC", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444017585596, "Codice marca inesistente")))
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPDON", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444049148298, "Codice relaz. listini inesistente")))
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCVUO", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444160711512, "Codice vuoto inesistente")))
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOVE", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444191649210, "Codice contropartita acq. inesistente")))
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOVE", "N", strOut) Then
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOVE", "N", strOut) Then
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
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.ProposedValue = " "
        e.Row!xx_famprod = ""
        Exit Sub
      Else
        e.ProposedValue = UCase(NTSCStr(e.ProposedValue))
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCFAM", "S", strOut) Then
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
      If oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp) = False Then
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
      If oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp) = False Then
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABGMER", "N", strOut) Then
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAGA", "N", strOut) Then
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAGA", "N", strOut) Then
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCENA", "N", strOut) Then
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
  Public Overridable Sub BeforeColUpdate_ar_sostit(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If (NTSCStr(e.ProposedValue) <> "") And (InStr(NTSCStr(e.ProposedValue), "'") <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444435869523, "L'articolo sostitutivo non può contenere apici semplici.")))
        Exit Sub
      End If
      If NTSCStr(e.ProposedValue) <> "" Then e.ProposedValue = UCase(NTSCStr(e.ProposedValue))

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
  Public Overridable Sub BeforeColUpdate_ar_sostituito(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If (NTSCStr(e.ProposedValue) <> "") And (InStr(NTSCStr(e.ProposedValue), "'") <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444474307269, "L'articolo sostituito non può contenere apici semplici.")))
        Exit Sub
      End If
      If NTSCStr(e.ProposedValue) <> "" Then e.ProposedValue = UCase(NTSCStr(e.ProposedValue))

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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSGME", "N", strOut) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128557501483916812, "Codice sottogruppo inesistente.")))
          Return
        Else
          If NTSCInt(e.Row!ar_gruppo) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444589464256, "Indicare il gruppo prima di inserire il sottogruppo.")))
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCSAR", "N", strOut) Then
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
        If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCPAR", "N", strOut) Then
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

      If oCldArti.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABTCDC", "N", , dttTmp) Then
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

      If oCldArti.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABDICA", "S", , dttTmp) Then
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

      If oCldArti.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABDICV", "S", , dttTmp, NTSCStr(e.Row!ar_coddica)) Then
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

      If oCldArti.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABLOTX", "N", strTmp) Then
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
  Public Overridable Sub BeforeColUpdate_ar_ubicst(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
  Public Overridable Sub BeforeColUpdate_ar_ubicpr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
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
  Public Overridable Sub BeforeColUpdate_ar_ubicri(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
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
  Public Overridable Sub BeforeColUpdate_ar_ubicus(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
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
  Public Overridable Sub BeforeColUpdate_ar_reparto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strDescr As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xx_reparto = "" : Return

      oCldArti.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABREAR", "N", strDescr)
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
      If oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSEAT", "N", strOut) = False Then
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
  Public Overridable Sub BeforeColUpdate_ar_tipo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bModuloORTO = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      e.ProposedValue = e.ProposedValue.ToString.ToUpper
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCStr(e.ProposedValue).ToUpper
        Case "C", "P"
        Case Else
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130628641124592843, "Attenzione!" & vbCrLf & _
            "Tipo articolo NON corretto." & vbCrLf & _
            "Valori ammessi:" & vbCrLf & _
            " . C" & vbCrLf & _
            " . P")))
          Return
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
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
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444709152522, "Il Codice articolo non può contenere apici semplici.")))
            Return False
          End If

          '-----------------------------------------------------------------------------------------
          If NTSCStr(dtrTmp(i)!ar_tipitemcp3) = "T" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559379257914972, "Non è possibile impostare un articolo come 'Configurato tecnico'.")))
            Return False
          End If
          If NTSCStr(dtrTmp(i)!ar_tipitemcp3) = "P" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559379236909512, "Non è possibile impostare un articolo come 'Configurato principale'.")))
            Return False
          End If
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
        If Trim(NTSCStr(dtrTmp(i)!ar_unmis)) = "" And Not (NTSCStr(dtrTmp(i)!ar_codart) = "D" Or NTSCStr(dtrTmp(i)!ar_codart) = "M") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556784482247544, "Unità di misura principale obbligatoria.")))
          Return False
        End If

        If Not strTipoConfiguratore > "2" Then
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
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444798059341, "Attenzione!" & vbCrLf & _
              "La 'Scorta minima' deve essere maggiore di 0 quando la Politica di Riordino è di tipo:" & vbCrLf & _
              " --> A punto di riordino con lotto" & vbCrLf & _
              " --> A punto di riordino a ricostruzione scorta")))
            Return False
          End If
        End If

        If Not strTipoConfiguratore > "2" Then
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
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444844622139, "Attenzione!" & vbCrLf & _
              "La 'Scorta massima' deve essere maggiore di 0 quando la Politica di Riordino è di tipo:" & vbCrLf & _
              " --> A punto di riordino a ricostruzione scorta")))
            Return False
          End If
        End If

        If Not strTipoConfiguratore > "2" Then
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
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444863997263, "Attenzione!" & vbCrLf & _
              "La 'Quantità lotto standard' deve essere maggiore di 0 quando la Politica di Riordino è di tipo:" & vbCrLf & _
              " --> A punto di riordino con lotto" & vbCrLf & _
              " --> Su fabbisogno con lotto" & vbCrLf & _
              " --> Su fabbisogno con lotto minimo")))
            Return False
          End If
        End If

        If NTSCDec(dtrTmp(i)!ar_perqta) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559254390025223, "Il Molt.qtà/prezzo deve essere maggiore di 0")))
          Return False
        End If

        If (NTSCStr(dtrTmp(i)!ar_sostit) <> "") And (InStr(NTSCStr(dtrTmp(i)!ar_sostit), "'") <> 0) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556788587788107, "L'articolo sostitutivo non può contenere apici semplici.")))
          Return False
        End If
        If (NTSCStr(dtrTmp(i)!ar_sostituito) <> "") And (InStr(NTSCStr(dtrTmp(i)!ar_sostituito), "'") <> 0) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556788822904747, "L'articolo sostituito non può contenere apici semplici.")))
          Return False
        End If

        If bNoMsgCongruenzaPolSconte = False Then
          If (NTSCStr(dtrTmp(i)!ar_polriord) = "N") And (NTSCDec(dtrTmp(i)!ar_scomin) >= NTSCDec(dtrTmp(i)!ar_scomax)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556790657517179, "La scorta massima deve essere superiore a quella minima.")))
            Return False
          End If
        Else
          If (NTSCStr(dtrTmp(i)!ar_polriord) = "N") And (NTSCDec(dtrTmp(i)!ar_scomin) > NTSCDec(dtrTmp(i)!ar_scomax)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319511346826004, "La scorta massima deve essere superiore a quella minima.")))
            Return False
          End If
        End If
        If (NTSCStr(dtrTmp(i)!ar_coddb) <> "") And (InStr(NTSCStr(dtrTmp(i)!ar_coddb), "'") <> 0) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556790763029467, "Il Codice distinta base non può contenere apici semplici.")))
          Return False
        End If

        If Trim(NTSCStr(dtrTmp(i)!ar_um4)) = "" And Trim(NTSCStr(dtrTmp(i)!ar_formula)) <> "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812444991966832, "Se è indicata la formula di trasformazione in UMP, l'unità di misura relativa non può essere nulla.")))
          Return False
        End If

        If Trim(NTSCStr(dtrTmp(i)!ar_um4)) <> "" And Trim(NTSCStr(dtrTmp(i)!ar_formula)) = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559387522405278, "Se è indicata l'unità di misura formula, la formula di trasformazione in UMP non può essere nulla.")))
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

        '-----------------------------------------------------------------------------------
        '--- Se la fase indicata in ulfase è diversa da 0 e non esiste nella tabella
        '--- ARTFASI avverte
        '-----------------------------------------------------------------------------------
        If (NTSCStr(dtrTmp(i)!ar_gesfasi) = "S") And (bDuplicazioneInCorso = False) Then

          oCldArti.GetArtfasi(strDittaCorrente, NTSCStr(dtrTmp(i)!ar_codart), dsTmp)

          If dsTmp.Tables("ARTFASI").Rows.Count = 0 And NTSCInt(dtrTmp(i)!ar_ultfase) <> 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812445102436289, "Articoli a fasi: l'ultima fase deve essere 0 se per l'articolo non è stata impostata almeno una fase")))
            Return False
          End If
          If dsTmp.Tables("ARTFASI").Rows.Count > 0 And NTSCInt(dtrTmp(i)!ar_ultfase) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128560224705251471, "Articoli a fasi: l'ultima fase deve essere diversa da 0 se per l'articolo sono state impostate delle fasi")))
            Return False
          End If
          If NTSCInt(dtrTmp(i)!ar_ultfase) > 0 Then
            'ricerca se presente la fase
            If Not oCldArti.ValCodiceDb(NTSCStr(dtrTmp(i)!ar_ultfase), strDittaCorrente, "ARTFASI", "N", "", , NTSCStr(dtrTmp(i)!ar_codart)) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812445224937073, "Articoli a fasi: Codice ultima fase inesistente per l'articolo corrente.")))
              Return False
            End If
          End If
        End If

        '--------------------------------------------------------------------------------------
        '--- Se il tipo kit è <> da (Nessuno) il flag relativo alla gestione per fasi
        '--- non può essere selezionato
        '--------------------------------------------------------------------------------------
        If NTSCStr(dtrTmp(i)!ar_gesfasi) = "S" Then
          If NTSCStr(dtrTmp(i)!ar_tipokit) <> " " Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128559410292814136, "Se il tipo kit è quello indicato in anagrafica articoli, l'articolo non può essere gestito a fasi.")))
            Return False
          End If
          If NTSCStr(dtrTmp(i)!ar_gescon) <> "N" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812445328218984, "Se applica CONAI è quello indicato in anagrafica articoli, l'articolo non può essere gestito a fasi.")))
            Return False
          End If
        End If

        If NTSCStr(dtrTmp(i)!ar_codnomc) <> "" And bValidaNomenclCombin Then
          If Not oCldArti.ValCodiceDb(NTSCStr(dtrTmp(i)!ar_codnomc), strDittaCorrente, "TARIC", "S", "", dttTmp) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812445372438017, "Codice nomenclatura combinata '|" & NTSCStr(dtrTmp(i)!ar_codnomc) & "|' non valida.")))
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
            If Not oCldArti.IsGescommUpdatable(strDittaCorrente, NTSCStr(dtrTmp(i)!ar_codart), False, NTSCStr(dtrTmp(i)("ar_gescomm", DataRowVersion.Original)), NTSCStr(dtrTmp(i)!ar_gescomm), strMsgOut) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812445372438018, strMsgOut)))
              Return False
            End If
          End If
        End If
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
          If oCldArti.ValCodiceDb(NTSCStr(dtrTmp(i)!ar_codart), strDittaCorrente, "ARTICO", "S", "", dttTmp) = True Then
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

  Public Overridable Function TestPreCancella(ByVal strCodart As String, ByVal strTipoOpz As String, _
                                              ByVal nCodtipa As Integer) As Boolean
    Dim strOut As String = ""
    Try
      strCodartDelete = strCodart
      strTipoOpzDelete = strTipoOpz
      nCodtipaDelete = nCodtipa
      '-----------------------------------
      ' viene eseguito un controllo sulla possibile cancellazione del record
      ' se questo articolo non e' gia' referenziato in altre tabelle
      If Not oCldArti.IsArtiDeletable(strDittaCorrente, strCodartDelete, strOut) Then
        ThrowRemoteEvent(New NTSEventArgs("", strOut))
        Return False
      End If

      'controllo che se l'articolo è neutro, non esistano articoli configurati
      'che puntano ad esso
      If strTipoOpzDelete = "B" Or strTipoOpzDelete = "Q" Then
        If Not ControllaConfigurazioni(strCodartDelete) Then Return False
      End If
      'controllo che l'articolo non sia intestatario di D.B.
      If Not ControllaArticolo(strCodartDelete) Then Return False

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

  Public Overridable Function GetTabTipa1() As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCldArti.GetTabTipa1(strDittaCorrente, dsTmp)

      If dsTmp.Tables("TABTIPA").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128552276734078758, "Inserire in 'Tipologie Articoli' Codice = 1" & vbCrLf & "Descrizione = 'Articoli normali'" & vbCrLf & _
          "Prima di passare all'apertura o alla creazione di articoli.")))
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

  Public Overridable Function GetTabTipa(ByRef ds As DataSet) As Boolean
    Try
      oCldArti.GetTabTipa(strDittaCorrente, ds)

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
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog <> "-1" Then
        bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella), "", "", "")
      Else
        bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella), _
                  strActLogProg, strActLogNomOggLog, strActLogDesLog)
      End If

      If bResult Then
        If bDelete Then
          oCldArti.CancellaListini(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaSconti(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaProvvigioni(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaCodarfo(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaBarcode(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaArtmaga(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaKit(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaConai(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaTipologia(strDittaCorrente, nCodtipaDelete, strCodartDelete)
          oCldArti.CancellaArtfasi(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaConfigurazioniCP2(strDittaCorrente, strCodartDelete, strTipoOpzDelete)
          If strTipoConfiguratore > "2" Then
            oCldArti.CancellaConfigurazioniCP3(strDittaCorrente, strCodartDelete)
          End If
          oCldArti.CancellaLotcpro(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaLotcdef(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaArtacce(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaArtprom(strDittaCorrente, strCodartDelete)
          oCldArti.CancellaArtval(strDittaCorrente, strCodartDelete)
          oCldArti.UpdateArtest(strDittaCorrente, strCodartDelete)
        ElseIf (bNew OrElse bDuplicazioneInCorso) AndAlso bCreaBarcodeE13 Then
          oCldArti.GeneraBarcode(strDittaCorrente, _
                   NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_codart).Trim, _
                   NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_unmis), strPrefixEAN13, bIndicod)

        End If
        bHasChanges = False
      End If

      bNew = False

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

  Public Overridable Function ControllaConfigurazioni(ByVal strCodart As String) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCldArti.ControllaConfigurazioni(strDittaCorrente, strCodart, dsTmp)
      If dsTmp.Tables("COMPOSIX").Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128556579589971421, "Esistono articoli configurati generati da questo articolo." & vbCrLf & "Cancellazione non possibile.")))
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

  Public Overridable Function ControllaArticolo(ByVal strCodart As String) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCldArti.ControllaArticolo(strDittaCorrente, strCodart, dsTmp)
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

  'Obsoleto
  Public Overridable Function DuplicaArticolo(ByVal strCodartNew As String, _
                                              ByVal bListini As Boolean, ByVal bSconti As Boolean, _
                                              ByVal bProvv As Boolean, ByVal bArtval As Boolean, _
                                              ByVal bKit As Boolean, ByVal bConai As Boolean, _
                                              ByVal bArtfasi As Boolean, ByVal bDuplEster As Boolean) As Boolean

    Try
      Return DuplicaArticolo(strCodartNew, bListini, bSconti, bProvv, bArtval, bKit, bConai, bArtfasi, bDuplEster, False)
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
  'obsoleto
  Public Overridable Function DuplicaArticolo(ByVal strCodartNew As String, _
                                            ByVal bListini As Boolean, ByVal bSconti As Boolean, _
                                            ByVal bProvv As Boolean, ByVal bArtval As Boolean, _
                                            ByVal bKit As Boolean, ByVal bConai As Boolean, _
                                            ByVal bArtfasi As Boolean, ByVal bDuplEster As Boolean, ByVal bDuplValoriAttributi As Boolean) As Boolean

    Try
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodartNew, bListini, bSconti, bProvv, bArtval, bKit, bConai, bArtfasi, bDuplEster})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      Return DuplicaArticolo(strCodartNew, bListini, bSconti, bProvv, bArtval, bKit, bConai, bArtfasi, bDuplEster, bDuplValoriAttributi, False)
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

  Public Overridable Function DuplicaArticolo(ByVal strCodartNew As String, _
                                            ByVal bListini As Boolean, ByVal bSconti As Boolean, _
                                            ByVal bProvv As Boolean, ByVal bArtval As Boolean, _
                                            ByVal bKit As Boolean, ByVal bConai As Boolean, _
                                            ByVal bArtfasi As Boolean, ByVal bDuplEster As Boolean, _
                                            ByVal bDuplValoriAttributi As Boolean, ByVal bArtMaga As Boolean) As Boolean

    Dim i As Integer = 0
    Dim strCodartOld As String = ""
    Dim evnt As NTSEventArgs
    Dim nCurRow As Integer
    Dim bNoValarti As Boolean = False
    Dim lsOldUnMis, lsNewUnMis As New List(Of String)

    Try
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodartNew, bListini, bSconti, bProvv, bArtval, bKit, bConai, bArtfasi, bDuplEster, bDuplValoriAttributi, bArtMaga})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If

      strCodartOld = NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_codart)

      'Memorizzo le UM dell'articolo sorgente
      lsOldUnMis.Add(NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_unmis))
      lsOldUnMis.Add(NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_unmis2))
      lsOldUnMis.Add(NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_confez2))
      lsOldUnMis.Add(NTSCStr(dsShared.Tables("ARTICO").Rows(0)!ar_um4))

      dsShared.Tables("ARTICO").AcceptChanges()

      dsShared.Tables("ARTICO").Rows(0).SetAdded()
      nCurRow = 0

      '---------------------
      'correggo artico
      With dsShared.Tables("ARTICO").Rows(nCurRow)
        !ar_codart = strCodartNew

        !ar_ultagg = Now.ToShortDateString
        !ar_oragg = (Now.Hour * 100) + Now.Minute
        !ar_datins = Now.ToShortDateString

        If bDuplEster = True Then 'se richiamato da programma esterno
          !ar_descr = strArtiDescr
          !ar_desint = strArtiDesint
          !ar_note = strArtiNote
          !ar_forn = lArtiForn
        End If

        If Not (UCase(NTSCStr(!ar_gesvar)) <> "J" And UCase(NTSCStr(!ar_gesvar)) <> "H") Then
          'Articolo configurato (CP2-CP3-CP4)
          !ar_gesvar = "N"
        End If

        If (Not (NTSCStr(!ar_coddb) = "")) Then
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128560066739608267, "L'articolo '|" & strCodartOld & "|' che si sta duplicando" & vbCrLf & _
            "possiede un Codice Distinta Base." & vbCrLf & _
            "Ereditarlo insieme agli altri dati?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
            !ar_coddb = "" 'Null
          End If
        End If

        If Not dtrTmpDuar Is Nothing Then 'ricopio i campi dalla form duar
          !ar_unmis = dtrTmpDuar!ar_unmis
          !ar_confez2 = dtrTmpDuar!ar_confez2
          If NTSCStr(!ar_confez2) = "" Then
            !ar_qtacon2 = 1
          End If
          !ar_unmis2 = dtrTmpDuar!ar_unmis2
          If NTSCStr(!ar_unmis2) = "" Then
            !ar_conver = 1
          End If
          !ar_um4 = dtrTmpDuar!ar_um4
          !ar_formula = dtrTmpDuar!ar_formula
          !ar_perqta = dtrTmpDuar!ar_perqta
          'se la tipologia articolo è diversa, non dovrò copiare valarti
          If NTSCStr(!ar_codtipa) <> NTSCStr(dtrTmpDuar!ar_codtipa) Then
            !ar_codtipa = dtrTmpDuar!ar_codtipa
            bNoValarti = True
          End If
          !ar_gesfasi = dtrTmpDuar!ar_gesfasi
          !ar_gesubic = dtrTmpDuar!ar_gesubic
          !ar_geslotti = dtrTmpDuar!ar_geslotti
          !ar_gestmatr = dtrTmpDuar!ar_gestmatr
          !ar_gescomm = dtrTmpDuar!ar_gescomm

          'Memorizzo le UM dell'articolo duplicato
          lsNewUnMis.Add(NTSCStr(dtrTmpDuar!ar_unmis))
          lsNewUnMis.Add(NTSCStr(dtrTmpDuar!ar_unmis2))
          lsNewUnMis.Add(NTSCStr(dtrTmpDuar!ar_confez2))
          lsNewUnMis.Add(NTSCStr(dtrTmpDuar!ar_um4))
        End If

      End With

      evnt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128473835841698662, "Salvare l'articolo appena creato?"))
      ThrowRemoteEvent(evnt)
      If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False

      If Not Salva(False) Then Return False
      dsShared.AcceptChanges()

      'duplico le tabelle collegate se spuntate nella form
      If bListini = True Then
        oCldArti.AggiornaListini(strDittaCorrente, strCodartNew, strCodartOld)
        Dim strEliminato As String = ""
        'Se elimina veramente un listino, allora lo aggiunge alla lista delle righe da escludere
        For Each strUnMis As String In lsOldUnMis
          If strUnMis.Trim = "" Then Continue For 'Esclude le unità di misura non impostate
          If lsNewUnMis.Contains(strUnMis) Then Continue For 'L'unità di misura era presente anche nel vecchio articolo. Non devo fare nulla.
          If oCldArti.EliminaListiniNonValidiPerUnMis(strDittaCorrente, strCodartNew, strUnMis) Then strEliminato &= " - " & strUnMis & vbCrLf
        Next
        If strEliminato.Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 130789196644814616, _
                    "Attenzione! I listini riferiti alle unità di misura: |" & vbCrLf & strEliminato & "|Non saranno duplicati in quanto non presenti sul nuovo articolo.")))
        End If
      End If
      If bSconti = True Then oCldArti.AggiornaSconti(strDittaCorrente, strCodartNew, strCodartOld)
      If bProvv = True Then oCldArti.AggiornaProvvigioni(strDittaCorrente, strCodartNew, strCodartOld)
      If bArtval = True Then oCldArti.AggiornaArtval(strDittaCorrente, strCodartNew, strCodartOld)
      If bKit = True Then oCldArti.AggiornaKit(strDittaCorrente, strCodartNew, strCodartOld)
      If bConai = True Then oCldArti.AggiornaConai(strDittaCorrente, strCodartNew, strCodartOld)
      If bArtMaga Then oCldArti.AggiornaArtMaga(strDittaCorrente, strCodartNew, strCodartOld)
      'ricopio le fasi soltanto se l'articolo di destinazione è a fasi
      If bArtfasi = True And CBool(IIf(NTSCStr(dtrTmpDuar!ar_gesfasi) = "S", True, False)) Then oCldArti.AggiornaArtfasi(strDittaCorrente, strCodartNew, strCodartOld)

      If Not bNoValarti Then 'Ho cambiato tipologia articolo, non ha senso copiare gli altri dati.
        If bDuplValoriAttributi Then
          oCldArti.AggiornaValarti_ParametriValori(strDittaCorrente, strCodartNew, strCodartOld)
        Else
          oCldArti.AggiornaValarti(strDittaCorrente, strCodartNew, strCodartOld)
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

  Public Overridable Function GetArtfasi(ByVal strCodart As String, ByRef dsTmp As DataSet) As Boolean
    Try
      Return oCldArti.GetArtfasi(strDittaCorrente, strCodart, dsTmp)

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

  Public Overridable Function ImportaCatalogoFornitori(ByVal dttTmp As DataTable) As Boolean
    Dim strCodArtImport As String = ""
    Dim i As Integer
    Dim strTmp As String = ""
    Try
      '--------------------------------------------------
      'Apre il recordset
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128576409305508990, "Nessun articolo selezionato.")))
        'esci acquisizione
        Return False
      End If

      'effettuo il controllo prima di importare
      If Not CType(oCleComm, CLELBMENU).TestPreImportaArticoDaArtest(strDittaCorrente, strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", strTmp))
        Return False
      End If

      'ciclo per gli articoli da importare
      For i = 0 To dttTmp.Rows.Count - 1
        If Not CType(oCleComm, CLELBMENU).CreaArticoDaArtest(strDittaCorrente, NTSCStr(dttTmp.Rows(i)!ae_codartf), _
                                    NTSCStr(dttTmp.Rows(i)!ae_forn), NTSCStr(dttTmp.Rows(i)!ae_codmarc), _
                                    strCodArtImport, strTmp) Then
          ThrowRemoteEvent(New NTSEventArgs("", strTmp))
        End If
        strCodArtImport = ""
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

  Public Overridable Function CheckArtVarianti(ByVal strCodart As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not oCldArti.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", , dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128578441820612837, "Articolo inesistente")))
        Return False
      Else
        If NTSCStr(dttTmp.Rows(0)!ar_gesvar) = "S" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128578440790458302, "Non si può aprire una variante.")))
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
    End Try
  End Function

  Public Overridable Function CheckAttributi(ByVal strCodtipa As String) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCldArti.CheckAttributi(strDittaCorrente, strCodtipa, dsTmp)
      If dsTmp.Tables("CAMTIPA").Rows.Count = 0 Then
        Return False
      Else
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
    End Try
  End Function

  Public Overridable Function DecodCodcove(ByVal strCodcove As String, ByRef strDescr As String) As Boolean
    Dim strOut As String = ""
    Try
      If NTSCInt(strCodcove) = 0 Then
        strDescr = ""
        Exit Function
      End If
      If oCldArti.ValCodiceDb(strCodcove, strDittaCorrente, "TABCOVE", "N", strOut) Then
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
      If oCldArti.ValCodiceDb(strCodciva, strDittaCorrente, "TABCIVA", "N", strOut) Then
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

  Public Overridable Function AggiornaListini(ByVal strCodart As String, ByVal nCodvalu As Integer, _
    ByVal strDatavalidita As String, ByVal nListino As Integer, ByVal bQuant As Boolean, ByVal dQuant As Decimal, _
    ByVal strDtinvigore As String, ByVal nCodpdon As Integer, ByVal nCodlavo As Integer, ByVal nFase As Integer, _
    Optional ByVal strUnmis As String = "", Optional ByVal strNetto As String = "", _
    Optional ByVal bSilent As Boolean = True) As Boolean
    Try
      Return CType(oCleComm, CLELBMENU).AggiornaListini(strDittaCorrente, strCodart, nCodvalu, strDatavalidita, _
        nListino, bQuant, dQuant, strDtinvigore, nCodpdon, nCodlavo, nFase, strUnmis, strNetto)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function

  Public Overridable Function GetArtclasDescr(ByVal strClas1 As String, _
                                              ByVal strClas2 As String, ByVal strClas3 As String, _
                                              ByVal strClas4 As String, ByVal strClas5 As String) As String
    Try
      Return oCldArti.GetArtclasDescr(strDittaCorrente, strClas1, strClas2, strClas3, strClas4, strClas5)
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

  Public Overridable Function ArticoliDeteriorabili() As Boolean
    Try
      Return oCldArti.ArticoliDeteriorabili(strDittaCorrente)
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

#Region "funzioni specifiche per BNMGARTI.BNMGCOAR.VB"
  Public Overridable Function GetArticoCodarfo(ByVal strSecfCodarfo As String, ByRef ds As DataSet) As Boolean
    Try
      oCldArti.GetArticoCodarfo(strDittaCorrente, strSecfCodarfo, ds)

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

  Public Overridable Function CheckCodbar(ByVal strCodbar As String, ByRef ds As DataSet) As Boolean
    Try
      oCldArti.CheckCodbar(strDittaCorrente, strCodbar, ds)

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
    Try
      '-----------------------------------------------------
      If nCoarEditmode <> 2 Then
        CheckCodart = True
        Exit Function
      End If
      If bCoarGeneraArticoli = False Then
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

      bCoarArticoloGenerato = False
      If Not nLungRoot > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128552558999983295, "Valore nel Registro di Business: BSMGARTI-OPZIONI-GeneraCodiceLungRoot" & vbCrLf & "non valido." & vbCrLf & _
          "Generazione articolo non possibile!")))
        Exit Function
      End If
      If Not nLungExt > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128552558769422118, "Valore nel Registro di Business: BSMGARTI-OPZIONI-GeneraCodiceLungExt" & vbCrLf & "non valido." & vbCrLf & _
          "Generazione articolo non possibile!")))
        Exit Function
      Else
        If nLungRoot + nLungExt > CLN__STD.CodartMaxLen Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128552558750377796, "Somma dei valori nel Registro di Business:" & vbCrLf & _
            "BSMGARTI-OPZIONI-GeneraCodiceLungRoot e BSMGARTI-OPZIONI-GeneraCodiceLungExt" & vbCrLf & _
            "superiore a |" & CLN__STD.CodartMaxLen.ToString & "|. Generazione articolo non possibile!")))
          Exit Function
        End If
        strZero = ""
        For i = 1 To nLungExt
          strZero = strZero & "0"
        Next
      End If
      If ((Len(strCodart) = (nLungRoot + nLungExt)) And IsNumeric(Microsoft.VisualBasic.Right(strCodart, nLungExt))) Then
        lProgr = oCldArti.LegNuma(strDittaCorrente, "AA", UCase(Microsoft.VisualBasic.Left(strCodart, nLungRoot)), 0, True)
      Else
        If Len(strCodart) = nLungRoot Then
          lProgr = oCldArti.LegNuma(strDittaCorrente, "AA", UCase(strCodart), 0, True)
          strCodart = UCase(strCodart) & Microsoft.VisualBasic.Right(strZero & NTSCStr(lProgr), nLungExt)
        End If
      End If
      bCoarArticoloGenerato = True

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

  Public Overridable Function CheckArticoloBarcode(ByRef strCodart As String, ByRef strCodbar As String) As Boolean
    Dim evt As NTSEventArgs = Nothing
    Dim lcod As Integer
    Dim dsTabNuma As DataSet = Nothing
    Dim dsArticoTmp As DataSet = Nothing
    Dim dsArtest As DataSet = Nothing
    Try
      lCoarProgr = 0
      If bCoarGeneraArticoli Then
        If (nCoarEditmode = 2) And (bCoarArticoloGenerato) Then
          If Len(strCodart) = (nLungRoot + nLungExt) Then
            If IsNumeric(Right(strCodart, nLungExt)) Then
              If lProgr = CLng(Right(strCodart, nLungExt)) Then
                lCoarProgr = lProgr
                strCoarRoot = Left(strCodart, nLungRoot)
              Else

                oCldArti.GetTabNuma(strDittaCorrente, strCodart, nLungRoot, dsTabNuma)

                If dsTabNuma.Tables("TABNUMA").Rows.Count > 0 Then
                  If NTSCInt(Right(strCodart, nLungExt)) > NTSCInt(dsTabNuma.Tables("TABNUMA").Rows(0)!tb_numprog) Then
                    evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128812445538532830, "Numero progressivo superiore a quello relativo nella tebella delle numerazioni." & vbCrLf & _
                      "Aggiornare la numerazione una volta salvato l'articolo?"))
                    ThrowRemoteEvent(evt)
                    If evt.RetValue = "YES" Then
                      lCoarProgr = NTSCInt(Right(strCodart, nLungExt))
                      strCoarRoot = Left(strCodart, nLungRoot)
                    End If
                  End If
                End If
              End If
            End If
          Else
            If Len(strCodart) = nLungRoot Then
              CheckCodart(strCodart)
              lCoarProgr = lProgr
              strCoarRoot = Left(strCodart, nLungRoot)
            End If
          End If
        End If
      End If
      If strCodart = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128553116542074389, "Codice articolo obbligatorio.")))
        Return False
      End If

      oCldArti.GetArtico(strDittaCorrente, strCodart, dsArticoTmp)

      If dsArticoTmp.Tables("ARTICO").Rows.Count = 0 Then
        If nCoarEditmode = 1 Then   'Se in apertura
          'se in apertura non trovato cerca in artest
          oCldArti.GetArtest(strDittaCorrente, strCodart, dsArtest)

          If dsArtest.Tables("ARTEST").Rows.Count > 0 Then

            ThrowRemoteEvent(New NTSEventArgs("MGCTFOVIS:" & Microsoft.VisualBasic.Right("                              " & NTSCStr(dsArtest.Tables("ARTEST").Rows(0)!ae_codartf), 30) & _
              Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsArtest.Tables("ARTEST").Rows(0)!ae_forn), 9) & _
              Microsoft.VisualBasic.Right("0000" & NTSCStr(dsArtest.Tables("ARTEST").Rows(0)!ae_codmarc), 4), ""))

            Return False
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128553126713693836, "Codice articolo '|" & strCodart & "|' inesistente.")))
            Return False
          End If
        End If
      Else                           'Se nuovo
        If nCoarEditmode = 2 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812445737596604, "Codice articolo '|" & UCase(strCodart) & "|' già esistente.")))
          If (bCoarGeneraArticoli) And (bCoarArticoloGenerato) And (Len(strCodart) = (nLungRoot + nLungExt)) And (IsNumeric(Right(strCodart, nLungExt))) Then
            strCodart = Left(strCodart, nLungRoot)
          End If
          Return False
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se si è in inserimento di un nuovo articolo, controlla che non ci siano spazi
      '--- in fondo ed in testa, nel caso chiede di proseguire eliminandoli
      '-----------------------------------------------------------------------------------------
      If (nCoarEditmode = 2) Then
        If Trim(strCodart) <> strCodart Then
          evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128812445460251079, "Attenzione!" & vbCrLf & _
            "Il codice articolo indicato contiene degli spazi all'inizio o alla fine." & vbCrLf & _
            "Eliminarli e proseguire con l'inserimento?"))
          ThrowRemoteEvent(evt)
          If Not evt.RetValue = "YES" Then
            Return False
          End If
          strCodart = Trim(strCodart)
        End If
      End If
      '-----------------------------------------------------------------------------------------
      'solo sui nuovi articoli e se opzione abilitata provvedo ad aggiornare il progressivo articolo
      If nCoarEditmode = 2 And bCoarGeneraArticoli And strCoarRoot <> "" And lCoarProgr > 0 Then
        lcod = oCldArti.AggNuma(strDittaCorrente, "AA", strCoarRoot, 0, lCoarProgr, True, True, "")
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

  Public Overridable Function ListaArticoliModello() As DataTable
    Dim dttOut As DataTable
    Try
      dttOut = oCldArti.ListaArticoliModello(strDittaCorrente)
      If dttOut Is Nothing OrElse dttOut.Rows.Count = 0 Then Return dttOut

      dttOut.Rows.InsertAt(dttOut.NewRow, 0)
      dttOut.Rows(0)!cod = " "
      dttOut.Rows(0)!val = oApp.Tr(Me, 130372716136286191, "(Nessun modello selezionato)")
      dttOut.AcceptChanges()

      Return dttOut
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return Nothing
  End Function
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGDUAR.VB"
  Public Overridable Function CheckCodartDuar(ByRef strCodart As String) As Boolean
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
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128560103555589691, "Valore nel Registro di Business: BSMGARTI-OPZIONI-GeneraCodiceLungRoot" & vbCrLf & "non valido." & vbCrLf & _
          "Generazione articolo non possibile!")))
        Exit Function
      End If
      If Not nLungExt > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128560103585421981, "Valore nel Registro di Business: BSMGARTI-OPZIONI-GeneraCodiceLungExt" & vbCrLf & "non valido." & vbCrLf & _
          "Generazione articolo non possibile!")))
        Exit Function
      Else
        If nLungRoot + nLungExt > CLN__STD.CodartMaxLen Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128560103610880951, "Somma dei valori nel Registro di Business:" & vbCrLf & _
            "BSMGARTI-OPZIONI-GeneraCodiceLungRoot e BSMGARTI-OPZIONI-GeneraCodiceLungExt" & vbCrLf & _
            "superiore a |" & CLN__STD.CodartMaxLen.ToString & "|. Generazione articolo non possibile!")))
          Exit Function
        End If
        strZero = ""
        For i = 1 To nLungExt
          strZero = strZero & "0"
        Next
      End If
      If ((Len(strCodart) = (nLungRoot + nLungExt)) And IsNumeric(Microsoft.VisualBasic.Right(strCodart, nLungExt))) Then
        lProgr = oCldArti.LegNuma(strDittaCorrente, "AA", UCase(Microsoft.VisualBasic.Left(strCodart, nLungRoot)), 0, True)
      Else
        If Len(strCodart) = nLungRoot Then
          lProgr = oCldArti.LegNuma(strDittaCorrente, "AA", UCase(strCodart), 0, True)
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

  Public Overridable Function CheckArticoloDuar(ByRef strCodart As String, ByVal stredAr_unmis As String, _
                                                ByVal stredAr_confez2 As String, ByVal stredAr_unmis2 As String, _
                                                ByVal stredAr_perqta As String, ByVal strcbAr_codtipa As String, _
                                                ByVal strckAr_gesfasi As String, ByVal strckAr_gesubic As String, _
                                                ByVal strckAr_geslotti As String, ByVal strckAr_gestmatr As String, _
                                                ByVal strcbAr_gescomm As String, ByVal stredAr_um4 As String, _
                                                ByVal stredAr_formula As String) As Boolean
    Dim evt As NTSEventArgs = Nothing
    Dim lcod As Integer
    Dim dsTabNuma As DataSet = Nothing
    Dim dsArticoTmp As DataSet = Nothing
    Try
      lDuarProgr = 0
      'If bDuarGeneraArticoli Then
      If (bDuarArticoloGenerato) Then
        If Len(strCodart) = (nLungRoot + nLungExt) Then
          If IsNumeric(Right(strCodart, nLungExt)) Then
            If lProgr = CLng(Right(strCodart, nLungExt)) Then
              lDuarProgr = lProgr
              strDuarRoot = Left(strCodart, nLungRoot)
            Else

              oCldArti.GetTabNuma(strDittaCorrente, strCodart, nLungRoot, dsTabNuma)

              If dsTabNuma.Tables("TABNUMA").Rows.Count > 0 Then
                If NTSCInt(Right(strCodart, nLungExt)) > NTSCInt(dsTabNuma.Tables("TABNUMA").Rows(0)!tb_numprog) Then
                  evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128812445501501343, "Numero progressivo superiore a quello relativo nella tebella delle numerazioni." & vbCrLf & _
                    "Aggiornare la numerazione una volta salvato l'articolo?"))
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
      'End If
      If strCodart = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812445649783542, "Codice articolo obbligatorio.")))
        Return False
      End If

      oCldArti.GetArtico(strDittaCorrente, strCodart, dsArticoTmp)

      If dsArticoTmp.Tables("ARTICO").Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128553117843845967, "Codice articolo '|" & UCase(strCodart) & "|' già esistente.")))
        If (bDuarGeneraArticoli) And (bDuarArticoloGenerato) And (Len(strCodart) = (nLungRoot + nLungExt)) And (IsNumeric(Right(strCodart, nLungExt))) Then
          strCodart = Left(strCodart, nLungRoot)
        End If
        Return False
      End If

      'compilo il datarow con i campi del interfaccia grafica
      dtrTmpDuar!ar_unmis = stredAr_unmis
      dtrTmpDuar!ar_confez2 = stredAr_confez2
      dtrTmpDuar!ar_unmis2 = stredAr_unmis2
      dtrTmpDuar!ar_perqta = stredAr_perqta
      dtrTmpDuar!ar_codtipa = strcbAr_codtipa
      dtrTmpDuar!ar_gesfasi = strckAr_gesfasi
      dtrTmpDuar!ar_gesubic = strckAr_gesubic
      dtrTmpDuar!ar_geslotti = strckAr_geslotti
      dtrTmpDuar!ar_gestmatr = strckAr_gestmatr
      dtrTmpDuar!ar_gescomm = strcbAr_gescomm
      dtrTmpDuar!ar_um4 = stredAr_um4
      dtrTmpDuar!ar_formula = stredAr_formula

      'controllo unita di misura
      If Trim(NTSCStr(dtrTmpDuar!ar_unmis)) = "" And Not (strCodart = "D" Or strCodart = "M") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812402174709623, "Unità di misura principale obbligatoria.")))
        Return False
      End If
      If Not ControllaUnitaDiMisura(dtrTmpDuar) Then
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      'solo sui nuovi articoli e se opzione abilitata provvedo ad aggiornare il progressivo articolo
      If bDuarArticoloGenerato And strDuarRoot <> "" And lDuarProgr > 0 Then ' bDuarGeneraArticoli And
        lcod = oCldArti.AggNuma(strDittaCorrente, "AA", strDuarRoot, 0, lDuarProgr, True, True, "")
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

  Public Overridable Function GetUnitaMisDuar() As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not oCldArti.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", , dttTmp) Then
        dtrTmpDuar = Nothing
      Else
        dtrTmpDuar = dttTmp.Rows(0)
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

#Region "funzioni specifiche per BNMGARTI.BNMGAPRO.VB"
  Public Overridable Function AproApri(ByVal strDitta As String, ByVal strAproCodart As String, ByRef dsApro As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArti.GetDataApro(strDitta, strAproCodart, dsApro)
      If dReturn = False Then Return False

      oCldArti.SetTableDefaultValueFromDB("ARTPROM", dsApro)

      dsAproShared = dsApro

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsAproShared.Tables("ARTPROM").ColumnChanging, AddressOf AproBeforeColUpdate
      AddHandler dsAproShared.Tables("ARTPROM").ColumnChanged, AddressOf AproAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsAproShared.Tables("ARTPROM").Columns("codditt").DefaultValue = strDittaCorrente
      dsAproShared.Tables("ARTPROM").Columns("apr_codart").DefaultValue = strAproCodart
      dsAproShared.Tables("ARTPROM").Columns("apr_ultagg").DefaultValue = Now.ToShortDateString
      dsAproShared.Tables("ARTPROM").Columns("apr_opnome").DefaultValue = oApp.User.Nome

      dsAproShared.Tables("ARTPROM").AcceptChanges()

      bAproHasChanges = False

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
  Public Overridable Function AproSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not AproTestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "artprom"
        strActLogDesLog = oApp.Tr(Me, 128574789210752807, "Promozioni articolo")
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArti.ScriviTabellaSemplice(strDittaCorrente, "ARTPROM", dsAproShared.Tables("ARTPROM"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bAproHasChanges = False
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
  Public ReadOnly Property AproRecordIsChanged() As Boolean
    Get
      Return bAproHasChanges
    End Get
  End Property
  Public Overridable Function AproTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsAproShared.Tables("ARTPROM").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCInt(dtrCurrRow(i)!apr_codtpro) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128562811152841708, "Codice tipo promozione obbligatorio.")))
          Return False
        End If
      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsAproShared.Tables("ARTPROM").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND apr_codart = " & CStrSQL(dtrCurrRow(0)!apr_codart.ToString) & " AND apr_codtpro = " & dtrCurrRow(0)!apr_codtpro.ToString)
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812445890722584, "Esiste gia una riga con le stesse caratteristiche")))
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
  Public Overridable Sub AproNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsAproShared.Tables("ARTPROM").Rows.Add(dsAproShared.Tables("ARTPROM").NewRow)
      bAproHasChanges = True

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
  Public Overridable Function AproRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsAproShared.Tables("ARTPROM").Select(strFilter)(nRow).RejectChanges()
      bAproHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub AproBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AproBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub AproAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bAproHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AproAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub AproBeforeColUpdate_apr_codtpro(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codtpro = ""
        Return
      End If

      If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPRO", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812445979316901, "Codice tipo promozione non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
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
  Public Overridable Sub AproBeforeColUpdate_apr_coddpro(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_coddpro = ""
        Return
      End If

      If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABDPRO", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128868670519232907, "Codice dettaglio prom. non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else
        e.Row!xx_coddpro = strTmp
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
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGARTA.VB"
  Public Overridable Function ArtaApri(ByVal strDitta As String, ByVal strArtaCodart As String, ByRef dsArta As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArti.GetDataArta(strDitta, strArtaCodart, dsArta)
      If dReturn = False Then Return False

      oCldArti.SetTableDefaultValueFromDB("ARTACCE", dsArta)

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

      bSucc = CBool(oCldArti.GetSettingBus("BSMGARTI", "OPZIONI", ".", "RicreaAncheSuccedaneiCollegati", "0", " ", "0"))
      bAcc = CBool(oCldArti.GetSettingBus("BSMGARTI", "OPZIONI", ".", "RicreaAncheAccessoriCollegati", "0", " ", "0"))
      bRicreaSuccedaneiAccessori = CBool(oCldArti.GetSettingBus("BSMGARTI", "OPZIONI", ".", "RicreaSuccedaneiAccessori", "0", " ", "0"))

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
      bResult = oCldArti.ScriviTabellaSemplice(strDittaCorrente, "ARTACCE", dsArtaShared.Tables("ARTACCE"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bArtaHasChanges = False

        'Se il salvataggio è andato a buon fine modifico anche gli accessori e succedanei
        If bRicreaSuccedaneiAccessori Then
          If Not oCldArti.RicreaSuccedaneiAccessori(strDittaCorrente, strArtAccSuccCodart, bSucc, bAcc, dsArtaShared.Tables("ARTACCE")) Then Return False
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
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = " " Then
        e.Row!xx_codartas = ""
        Return
      End If

      e.ProposedValue = e.ProposedValue.ToString.ToUpper
      If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128562880138157291, "Codice articolo non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else
        e.Row!xx_codartas = strTmp
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
      oCldArti.CompletaSuccedaneiAccessori(strDittaCorrente, strQuery.ToString, bAcc, bSucc, dttTmp)

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
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGCONA.VB"
  Public Overridable Sub ConaOnAddNew(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dReturn As Boolean = False
    Dim nNcampo As Integer
    Dim dsMaxArtcona As DataSet = Nothing
    Try
      If e.Row!ak_riga Is Nothing Then Exit Sub
      dReturn = oCldArti.GetMaxArtcona(strDittaCorrente, NTSCStr(e.Row!ak_codart), dsMaxArtcona)

      If dsMaxArtcona.Tables("ARTCONA").Rows.Count > 0 Then
        nNcampo = NTSCInt(dsMaxArtcona.Tables("ARTCONA").Rows(0)!Riga)
      Else
        nNcampo = 0
      End If

      e.Row!ak_riga = NTSCStr(nNcampo + 1)

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
  Public Overridable Function ConaApri(ByVal strDitta As String, ByVal strConaCodart As String, ByRef dsCona As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArti.GetDataCona(strDitta, strConaCodart, dsCona)
      If dReturn = False Then Return False

      oCldArti.SetTableDefaultValueFromDB("ARTCONA", dsCona)

      dsConaShared = dsCona

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsConaShared.Tables("ARTCONA").ColumnChanging, AddressOf ConaBeforeColUpdate
      AddHandler dsConaShared.Tables("ARTCONA").ColumnChanged, AddressOf ConaAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsConaShared.Tables("ARTCONA").Columns("codditt").DefaultValue = strDittaCorrente
      dsConaShared.Tables("ARTCONA").Columns("ak_codart").DefaultValue = strConaCodart

      dsConaShared.Tables("ARTCONA").AcceptChanges()

      bConaHasChanges = False

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
  Public Overridable Function ConaSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not ConaTestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "artcona"
        strActLogDesLog = oApp.Tr(Me, 128574790653782948, "Composizione Conai")
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArti.ScriviTabellaSemplice(strDittaCorrente, "ARTCONA", dsConaShared.Tables("ARTCONA"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bConaHasChanges = False
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
  Public ReadOnly Property ConaRecordIsChanged() As Boolean
    Get
      Return bConaHasChanges
    End Get
  End Property
  Public Overridable Function ConaTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsConaShared.Tables("ARTCONA").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCStr(dtrCurrRow(i)!ak_codfigli) = " " Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128563510511861225, "Codice figli obbligatorio.")))
          Return False
        End If
      Next

      '-------------------------------------------------
      'controlla che il figlio sia gia presente nella composizione corrente
      dtrTmp = dsConaShared.Tables("ARTCONA").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND ak_codart = " & CStrSQL(dtrCurrRow(0)!ak_codart.ToString) & " AND ak_codfigli = " & CStrSQL(dtrCurrRow(0)!ak_codfigli.ToString))
      If dtrTmp.Length > 1 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128563555295890783, "Articolo già presente nella composizione")))
        Return False
      End If

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsConaShared.Tables("ARTCONA").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND ak_codart = " & CStrSQL(dtrCurrRow(0)!ak_codart.ToString) & " AND ak_riga = " & dtrCurrRow(0)!ak_riga.ToString)
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128563510535459203, "Esiste gia una riga con le stesse caratteristiche")))
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
  Public Overridable Sub ConaNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsConaShared.Tables("ARTCONA").Rows.Add(dsConaShared.Tables("ARTCONA").NewRow)
      bConaHasChanges = True

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
  Public Overridable Function ConaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsConaShared.Tables("ARTCONA").Select(strFilter)(nRow).RejectChanges()
      bConaHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub ConaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '-------------------------------------------------------------
      'se non  compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "ak_riga" Then
        If e.Row!ak_riga.ToString = "0" Then ConaOnAddNew(sender, e)
      End If

      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "ConaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub ConaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bConaHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "ConaAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub ConaBeforeColUpdate_ak_codfigli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim strCart As String
    Try
      If NTSCStr(e.ProposedValue) = " " Then
        e.Row!xx_codfigli = ""
        Return
      End If

      e.ProposedValue = UCase(e.ProposedValue.ToString)

      If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", strTmp, dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128563510774251987, "Codice figli non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else

        If NTSCStr(dttTmp.Rows(0)!ar_tipokit) <> " " Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128563544389205013, "Non è possibile indicare un articolo di tipo KIT.")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
          Return
        End If
        If NTSCStr(dttTmp.Rows(0)!ar_gesfasi) = "S" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128563544602071993, "Non è possibile indicare un articolo gestito a fasi.")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
          Return
        End If

        ' il cod. articolo figlio deve iniziare con CONAI
        If Len(UCase(e.ProposedValue.ToString)) < 5 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128563547153193663, "Codice Articolo inserito deve iniziare con 'CONAI' .")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
          Return
        End If
        If Left(UCase(e.ProposedValue.ToString), 5) <> "CONAI" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128563547329019913, "Codice Articolo inserito deve iniziare con 'CONAI' .")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
          Return
        End If

        strCart = UCase(e.ProposedValue.ToString)
        If (Trim(NTSCStr(dttTmp.Rows(0)!ar_unmis)) = "" And Not (strCart = "D" Or strCart = "M")) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128563548487910263, "L'articolo inserito non ha l'unità di misura principale. Inserirla prima utilizzare l'articolo.")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
          Return
        Else
          e.Row!ak_ump = NTSCStr(dttTmp.Rows(0)!ar_unmis)
        End If

        e.Row!xx_codfigli = strTmp
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
#End Region

#Region "funzioni specifiche per BNMGTIPA.BNMGVALA.VB"
  Public Overridable Function ValaApri(ByVal strDitta As String, ByRef dsVala As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim strError As String = ""
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldArti.GetDataVala(strDitta, strValaCodart, nValaCodtipa, strError, dsVala)
      If dReturn = False Then
        ThrowRemoteEvent(New NTSEventArgs("", strError))
        Return False
      End If

      'ricopio nel datatable della griglia i dati relativi alla form mgvala
      'dsVala = dsValaAll.Clone
      'dsVala.Clear()
      'dtrCurrRow = dsValaAll.Tables("VALARTI").Select("alv_multi <> 'S'")
      'For i = 0 To dtrCurrRow.Length - 1
      '  dsVala.Tables("VALARTI").ImportRow(dtrCurrRow(i))
      'Next

      oCldArti.SetTableDefaultValueFromDB("VALARTI", dsVala)

      CaricaTipoValore(dsVala)

      dsValaShared = dsVala

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsValaShared.Tables("VALARTI").ColumnChanging, AddressOf ValaBeforeColUpdate
      AddHandler dsValaShared.Tables("VALARTI").ColumnChanged, AddressOf ValaAfterColUpdate

      bValaHasChanges = False

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
  Public Overridable Function ValaSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Dim dReturn As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not ValaTestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "valarti"
        strActLogDesLog = oApp.Tr(Me, 128812446068379971, "Attributi articoli")
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArti.ScriviTabellaSemplice(strDittaCorrente, "VALARTI", dsValaShared.Tables("VALARTI"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bValaHasChanges = False
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
  Public ReadOnly Property ValaRecordIsChanged() As Boolean
    Get
      Return bValaHasChanges
    End Get
  End Property
  Public Overridable Function ValaTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim dReturn As Boolean = False
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsValaShared.Tables("VALARTI").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1

      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsValaShared.Tables("VALARTI").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND alv_codart = " & CStrSQL(dtrCurrRow(0)!alv_codart.ToString) & " AND alv_ncampo = " & dtrCurrRow(0)!alv_ncampo.ToString & _
      " AND alv_index = " & dtrCurrRow(0)!alv_index.ToString)
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812445854003599, "Esiste gia una riga con le stesse caratteristiche")))
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
  Public Overridable Sub ValaNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsValaShared.Tables("VALARTI").Rows.Add(dsValaShared.Tables("VALARTI").NewRow)
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
  Public Overridable Function ValaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsValaShared.Tables("VALARTI").Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub ValaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "ValaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub ValaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bValaHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "ValaAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub ValaBeforeColUpdate_alv_codartl(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!xx_codartl = ""
        Return
      End If

      If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128581704558030149, "Codice articolo non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else
        e.Row!xx_codartl = strTmp
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
  Public Overridable Sub ValaBeforeColUpdate_alv_codling(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codling = ""
        Return
      End If

      If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABLINGP", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812446234787286, "Codice lingua non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else
        e.Row!xx_codling = strTmp
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
  Public Overridable Sub ValaBeforeColUpdate_xx_valore(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      Select Case NTSCInt(e.Row!alv_tipcamp)
        Case 7
          If NTSCDec(e.ProposedValue) = 0 Then
            e.ProposedValue = 0
          End If
          e.Row!alv_valdouble = e.ProposedValue
        Case 8
          If Not IsDate(e.ProposedValue) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
          Else
            e.Row!alv_valdata = e.ProposedValue
          End If
        Case 10
          e.Row!alv_valtext = e.ProposedValue
        Case 20
          'rispetto a vb6 rendo visibile alv_valcombo non uso xx_valore per il caso tipo combo
        Case 30
          ' verifico se la variante è corretta
          If NTSCStr(e.ProposedValue).Trim = "" Then
            e.Row!alv_desvvar = ""
            e.Row!alv_valvari = e.ProposedValue
            Return
          End If

          If Not oCldArti.ValidaValVari(strDittaCorrente, NTSCStr(e.ProposedValue), NTSCStr(e.Row!alv_codvari), dttTmp) Then Return
          If dttTmp.Rows.Count = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129019951502569743, "Codice variante non corretto")))
            e.ProposedValue = e.Row(e.Column.ColumnName)
            e.Row!xx_valore = e.Row!alv_valvari  ' ripristino anche il campo valore 
          Else
            e.Row!alv_valvari = e.ProposedValue
            e.Row!alv_desvvar = NTSCStr(dttTmp.Rows(0)!vv_desvvar)
          End If
      End Select

      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codling = ""
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

  Public Overridable Sub ValaBeforeColUpdate_xx_valcombo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      Select Case NTSCInt(e.Row!alv_tipcamp)
        Case 20
          'aggiorno la colonna effettiva con il valore da memorizzare sul db
          e.Row!alv_valcombo = NTSCStr(e.ProposedValue).Substring(NTSCStr(e.ProposedValue).IndexOf(".") + 1)
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
  Public Overridable Sub ValaBeforeColUpdate_alv_valcombo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      Select Case NTSCInt(e.Row!alv_tipcamp)
        Case 20
          oCldArti.GetComtipa(strDittaCorrente, NTSCInt(e.Row!alv_ncampo), _
                              NTSCInt(e.Row!alv_codtipa), e.ProposedValue.ToString, _
                              dttTmp)
          If dttTmp.Rows.Count > 0 Then
            e.Row!alv_desval = NTSCStr(dttTmp.Rows(0)!vt_desval)
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

  Public Overridable Sub CaricaTipoValore(ByRef dsVala As DataSet)
    Dim i As Integer
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Dim strTmp As String = ""
    Try
      For i = 0 To dsVala.Tables("VALARTI").Rows.Count - 1
        With dsVala.Tables("VALARTI").Rows(i)
          Select Case NTSCInt(dsVala.Tables("VALARTI").Rows(i)!alv_tipcamp)
            Case 7
              !xx_tipovalore = "Numerico"
              !xx_valore = dsVala.Tables("VALARTI").Rows(i)!alv_valdouble
              !xx_valorecmb = ""
            Case 8
              !xx_tipovalore = "Data"
              !xx_valore = dsVala.Tables("VALARTI").Rows(i)!alv_valdata
              !xx_valorecmb = ""
            Case 10
              !xx_tipovalore = "Testo"
              !xx_valore = dsVala.Tables("VALARTI").Rows(i)!alv_valtext
              !xx_valorecmb = ""
            Case 20
              !xx_tipovalore = "Combo"
              !xx_valore = ""
              !xx_valorecmb = dsVala.Tables("VALARTI").Rows(i)!alv_valcombo

              oCldArti.GetComtipa(strDittaCorrente, NTSCInt(dsVala.Tables("VALARTI").Rows(i)!alv_ncampo), _
                                  NTSCInt(dsVala.Tables("VALARTI").Rows(i)!alv_codtipa), NTSCStr(dsVala.Tables("VALARTI").Rows(i)!alv_valcombo), _
                                  dttTmp)
              If dttTmp.Rows.Count = 0 Then
                oCldArti.GetComtipa2(strDittaCorrente, NTSCInt(dsVala.Tables("VALARTI").Rows(i)!alv_ncampo), _
                         NTSCInt(dsVala.Tables("VALARTI").Rows(i)!alv_codtipa), dttTmp2)
                If dttTmp2.Rows.Count > 0 Then
                  !alv_valcombo = NTSCStr(dttTmp2.Rows(0)!vt_valore)
                  !alv_desval = NTSCStr(dttTmp2.Rows(0)!vt_desval)
                End If
              Else
                !alv_valcombo = NTSCStr(dttTmp.Rows(0)!vt_valore)
                !alv_desval = NTSCStr(dttTmp.Rows(0)!vt_desval)
              End If
            Case 30
              !xx_tipovalore = "Tabella"
              !xx_valore = dsVala.Tables("VALARTI").Rows(i)!alv_valvari
              !xx_valorecmb = ""
          End Select
          strTmp = ""
          If Not oCldArti.ValCodiceDb(NTSCStr(dsVala.Tables("VALARTI").Rows(i)!alv_codling), strDittaCorrente, "TABLINGP", "N", strTmp) Then
            !xx_codling = ""
          Else
            !xx_codling = strTmp
          End If
        End With
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

  Public Overridable Function CaricaTipoCombo(ByVal lTipa As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      If Not oCldArti.CaricaTipoCombo(strDittaCorrente, lTipa, dttOut) Then Return False

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

  Public Overridable Function GetArticoCmb(ByVal lCampo As Integer, ByVal lTipa As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      Return oCldArti.GetArticoCmb(strDittaCorrente, lCampo, lTipa, dttOut)
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

  Public Overridable Function AggiornaElencoAttributi(ByVal dsVala As DataSet) As Boolean
    Dim dttVala As DataTable = Nothing
    Dim strDescr As String = ""
    Dim i As Integer
    Try
      If Not oCldArti.ElencoAttributi(strDittaCorrente, nValaCodtipa, dttVala) Then Return False

      For i = 0 To dttVala.Rows.Count - 1
        'E' una riga nuova di camptipa che va aggiunta in valarti
        'L'aggiungo alla temporanea, poi nell'unload verrÃ  aggiunta a valarti
        If NTSCStr(dttVala.Rows(i)!ct_multi) = "P" Then Continue For

        If dsVala.Tables("VALARTI").Select("alv_ncampo = " & NTSCInt(dttVala.Rows(i)!ct_ncampo)).Length = 0 Then
          oCldArti.ValCodiceDb(NTSCStr(dttVala.Rows(0)!ct_codvari), strDittaCorrente, "TABVARI", "N", strDescr)

          dsVala.Tables("VALARTI").Rows.Add()
          With dsVala.Tables("VALARTI").Rows(dsVala.Tables("VALARTI").Rows.Count - 1)
            !codditt = strDittaCorrente
            !alv_codart = strValaCodart
            !alv_ncampo = dttVala.Rows(i)!ct_ncampo
            !alv_index = 0
            !alv_valdouble = 0
            !alv_tipcamp = dttVala.Rows(i)!ct_tipcamp
            !alv_codtipa = nValaCodtipa
            !alv_descamp = dttVala.Rows(i)!ct_descamp
            !alv_multi = dttVala.Rows(i)!ct_multi
            !alv_codvari = dttVala.Rows(i)!ct_codvari
            !alv_linkpr = dttVala.Rows(i)!ct_linkpr
            !alv_linkric = dttVala.Rows(i)!ct_linkric
            !alv_gestling = dttVala.Rows(i)!ct_gestling
            !alv_codling = 0
            !xx_codvari = strDescr
            !alv_flcamporic = dttVala.Rows(i)!ct_flcamporic
            !alv_flurl = dttVala.Rows(i)!ct_flurl
          End With
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

#End Region

#Region "funzioni specifiche per BNMGTIPA.BNMGVAL2.VB"
  Public Overridable Sub Val2OnAddNew(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dReturn As Boolean = False
    Dim nNcampo As Integer
    Dim dsMaxValarti As DataSet = Nothing
    Try
      If e.Row!alv_index Is Nothing Then Exit Sub
      dReturn = oCldArti.GetMaxValarti(strDittaCorrente, NTSCStr(e.Row!alv_codart), strVal2Campo, dsMaxValarti)

      If dsMaxValarti.Tables("VALARTI").Rows.Count > 0 Then
        nNcampo = NTSCInt(dsMaxValarti.Tables("VALARTI").Rows(0)!Indice)
      Else
        nNcampo = 0
      End If

      e.Row!alv_index = NTSCStr(nNcampo + 1)

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
  Public Overridable Function Val2Apri(ByVal strDitta As String, ByRef dsVal2 As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim dtrCurrRow() As DataRow
    Dim i As Integer
    Dim dsTmp As DataSet = Nothing
    Dim strError As String = ""
    Dim dsValaLocale As DataSet = Nothing 'rileggo tutto soltanto per filtrare multi S
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      oCldArti.GetDataVala(strDitta, strValaCodart, nValaCodtipa, strError, dsValaLocale)

      'ricopio nel datatable della griglia i dati relativi alla form mgvala
      dsVal2 = dsValaLocale.Clone
      dsVal2.Clear()
      dtrCurrRow = dsValaLocale.Tables("VALARTI").Select("alv_multi = 'S' AND alv_ncampo = " & strVal2Campo)
      For i = 0 To dtrCurrRow.Length - 1
        dsVal2.Tables("VALARTI").ImportRow(dtrCurrRow(i))
      Next

      oCldArti.SetTableDefaultValueFromDB("VALARTI", dsVal2)

      CaricaTipoValore2(dsVal2)

      dsVal2Shared = dsVal2

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsVal2Shared.Tables("VALARTI").ColumnChanging, AddressOf Val2BeforeColUpdate
      AddHandler dsVal2Shared.Tables("VALARTI").ColumnChanged, AddressOf Val2AfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsVal2Shared.Tables("VALARTI").Columns("codditt").DefaultValue = strDittaCorrente
      dsVal2Shared.Tables("VALARTI").Columns("alv_ncampo").DefaultValue = strVal2Campo
      dsVal2Shared.Tables("VALARTI").Columns("alv_codling").DefaultValue = strVal2CodLing
      dsVal2Shared.Tables("VALARTI").Columns("xx_codling").DefaultValue = strVal2XxCodLing
      dsVal2Shared.Tables("VALARTI").Columns("alv_codart").DefaultValue = strValaCodart
      dsVal2Shared.Tables("VALARTI").Columns("alv_tipcamp").DefaultValue = strVal2Tipcamp

      oCldArti.GetCamtipa(strDittaCorrente, strVal2Campo, NTSCStr(nValaCodtipa), dsTmp)

      With dsTmp.Tables("CAMTIPA").Rows(0)
        dsVal2Shared.Tables("VALARTI").Columns("alv_codvari").DefaultValue = !ct_codvari
        dsVal2Shared.Tables("VALARTI").Columns("alv_multi").DefaultValue = !ct_multi
        dsVal2Shared.Tables("VALARTI").Columns("alv_linkpr").DefaultValue = !ct_linkpr
        dsVal2Shared.Tables("VALARTI").Columns("alv_linkric").DefaultValue = !ct_linkric
        dsVal2Shared.Tables("VALARTI").Columns("alv_gestling").DefaultValue = !ct_gestling
        dsVal2Shared.Tables("VALARTI").Columns("alv_codtipa").DefaultValue = !ct_codtipa
        dsVal2Shared.Tables("VALARTI").Columns("alv_descamp").DefaultValue = !ct_descamp
        dsVal2Shared.Tables("VALARTI").Columns("alv_flcamporic").DefaultValue = !ct_flcamporic
        dsVal2Shared.Tables("VALARTI").Columns("alv_flurl").DefaultValue = !ct_flurl
      End With
      dsVal2Shared.Tables("VALARTI").AcceptChanges()

      bVal2HasChanges = False

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
  Public Overridable Function Val2Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not Val2TestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog = "-1" Then
        strActLogProg = "BSMGARTI"
        strActLogNomOggLog = "valarti"
        strActLogDesLog = oApp.Tr(Me, 128581631666006872, "Attributi articoli")
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldArti.ScriviTabellaSemplice(strDittaCorrente, "VALARTI", dsVal2Shared.Tables("VALARTI"), strActLogProg, strActLogNomOggLog, strActLogDesLog)

      If bResult Then
        bVal2HasChanges = False
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
  Public ReadOnly Property Val2RecordIsChanged() As Boolean
    Get
      Return bVal2HasChanges
    End Get
  End Property
  Public Overridable Function Val2TestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsVal2Shared.Tables("VALARTI").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1


      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsVal2Shared.Tables("VALARTI").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND alv_codart = " & CStrSQL(dtrCurrRow(0)!alv_codart.ToString) & " AND alv_ncampo = " & dtrCurrRow(0)!alv_ncampo.ToString & _
      " AND alv_index = " & dtrCurrRow(0)!alv_index.ToString)
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128582685033457494, "Esiste gia una riga con le stesse caratteristiche")))
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
  Public Overridable Sub Val2Nuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsVal2Shared.Tables("VALARTI").Rows.Add(dsVal2Shared.Tables("VALARTI").NewRow)
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
  Public Overridable Function Val2Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsVal2Shared.Tables("VALARTI").Select(strFilter)(nRow).RejectChanges()
      bHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub Val2BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '-------------------------------------------------------------
      'se non  compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "alv_index" Then
        If e.Row!alv_index.ToString = "0" Then Val2OnAddNew(sender, e)
      End If

      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "Val2BeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub Val2AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bVal2HasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "Val2AfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub CaricaTipoValore2(ByRef dsVala As DataSet)
    Dim i As Integer
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Dim strTmp As String = ""
    Try
      For i = 0 To dsVala.Tables("VALARTI").Rows.Count - 1
        With dsVala.Tables("VALARTI").Rows(i)
          Select Case NTSCInt(dsVala.Tables("VALARTI").Rows(i)!alv_tipcamp)
            Case 7
              !xx_tipovalore = "Numerico"
              !xx_valore2 = dsVala.Tables("VALARTI").Rows(i)!alv_valdouble
              !xx_valorecmb = ""
            Case 8
              !xx_tipovalore = "Data"
              !xx_valore2 = dsVala.Tables("VALARTI").Rows(i)!alv_valdata
              !xx_valorecmb = ""
            Case 10
              !xx_tipovalore = "Testo"
              !xx_valore2 = dsVala.Tables("VALARTI").Rows(i)!alv_valtext
              !xx_valorecmb = ""
            Case 20
              !xx_tipovalore = "Combo"
              !xx_valore2 = ""
              !xx_valorecmb = dsVala.Tables("VALARTI").Rows(i)!alv_valcombo

              oCldArti.GetComtipa(strDittaCorrente, NTSCInt(dsVala.Tables("VALARTI").Rows(i)!alv_ncampo), _
                                  NTSCInt(dsVala.Tables("VALARTI").Rows(i)!alv_codtipa), NTSCStr(dsVala.Tables("VALARTI").Rows(i)!alv_valcombo), _
                                  dttTmp)
              If dttTmp.Rows.Count = 0 Then
                oCldArti.GetComtipa2(strDittaCorrente, NTSCInt(dsVala.Tables("VALARTI").Rows(i)!alv_ncampo), _
                         NTSCInt(dsVala.Tables("VALARTI").Rows(i)!alv_codtipa), dttTmp2)
                If dttTmp2.Rows.Count > 0 Then
                  !alv_valcombo = NTSCStr(dttTmp2.Rows(0)!vt_valore)
                  !alv_desval = NTSCStr(dttTmp2.Rows(0)!vt_desval)
                End If
              Else
                !alv_valcombo = NTSCStr(dttTmp.Rows(0)!vt_valore)
                !alv_desval = NTSCStr(dttTmp.Rows(0)!vt_desval)
              End If
            Case 30
              !xx_tipovalore = "Tabella"
              !xx_valore2 = dsVala.Tables("VALARTI").Rows(i)!alv_valvari
              !xx_valorecmb = ""
          End Select
          strTmp = ""
          If Not oCldArti.ValCodiceDb(NTSCStr(dsVala.Tables("VALARTI").Rows(i)!alv_codling), strDittaCorrente, "TABLINGP", "N", strTmp) Then
            !xx_codling = ""
          Else
            !xx_codling = strTmp
          End If
        End With
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

  Public Overridable Sub Val2BeforeColUpdate_alv_codartl(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!xx_codartl = ""
        Return
      End If

      If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812446169161866, "Codice articolo non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else
        e.Row!xx_codartl = strTmp
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
  Public Overridable Sub Val2BeforeColUpdate_alv_codling(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codling = ""
        Return
      End If

      If Not oCldArti.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABLINGP", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128812446201037070, "Codice lingua non corretto")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
      Else
        e.Row!xx_codling = strTmp
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
  Public Overridable Sub Val2BeforeColUpdate_xx_valore2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      Select Case NTSCInt(e.Row!alv_tipcamp)
        Case 7
          If NTSCDec(e.ProposedValue) = 0 Then
            e.ProposedValue = 0
          End If
          e.Row!alv_valdouble = e.ProposedValue
        Case 8
          If Not IsDate(e.ProposedValue) Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
          Else
            e.Row!alv_valdata = e.ProposedValue
          End If
        Case 10
          e.Row!alv_valtext = e.ProposedValue
        Case 20
          'rispetto a vb6 rendo visibile alv_valcombo non uso xx_valore per il caso tipo combo
        Case 30
          ' verifico se la variante è corretta
          If NTSCInt(e.ProposedValue) = 0 Then
            e.Row!alv_desvvar = ""
            e.Row!alv_valvari = e.ProposedValue
            Return
          End If

          If Not oCldArti.ValidaValVari(strDittaCorrente, NTSCStr(e.ProposedValue), NTSCStr(e.Row!alv_codvari), dttTmp) Then Return
          If dttTmp.Rows.Count = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128897868051818974, "Codice variante non corretto")))
            e.ProposedValue = e.Row(e.Column.ColumnName)
            e.Row!xx_valore = e.Row!alv_valvari  ' ripristino anche il campo valore 
          Else
            e.Row!alv_valvari = e.ProposedValue
            e.Row!alv_desvvar = NTSCStr(dttTmp.Rows(0)!vv_desvvar)
          End If
      End Select

      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codling = ""
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
#End Region

#Region "funzioni specifiche per BNMGARTI.BNMGANEX.VB"
  Public Overridable Function GetTabaext(ByRef dttTmp As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldArti.GetTabaext(strDittaCorrente, dttTmp)
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

#Region "Logistica su palmare"
  Public Overridable Function GetRelease(ByVal strDitta As String, ByRef bIs15 As Boolean) As Boolean
    bIs15 = True
  End Function
#End Region
End Class
